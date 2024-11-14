using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EntrepeneurHero.Data;
using EntrepeneurHero.Models;
using Microsoft.AspNetCore.Authorization;

namespace EntrepeneurHero.Controllers
{
    [Authorize]
    public class PresupuestoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PresupuestoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Presupuesto
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Presupuestos.Include(p => p.Cliente);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Presupuesto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var presupuesto = await _context.Presupuestos
                .Include(p => p.Cliente)
                .Include(p => p.ProductosPresupuesto)
                    .ThenInclude(pp => pp.Producto)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (presupuesto == null)
            {
                return NotFound();
            }

            return View(presupuesto);
        }

        public IActionResult Create()
        {
            var viewModel = new PresupuestoCreateViewModel
            {
                Fecha = DateTime.Today,
                Productos = _context.Productos
                    .Select(p => new ProductoSelectionViewModel
                    {
                        ProductoId = p.Id,
                        Nombre = p.Nombre,
                        Precio = p.Precio,
                        IsSelected = false,
                        Cantidad = 0
                    })
                    .ToList()
            };

            ViewBag.Clientes = new SelectList(_context.Clientes, "Id", "Nombre");
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PresupuestoCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var presupuesto = new Presupuesto
                {
                    Fecha = viewModel.Fecha,
                    ClienteId = viewModel.ClienteId,
                    Aprobado = false,
                    TotalEstimado = viewModel.Productos
                        .Where(p => p.IsSelected)
                        .Sum(p => p.Precio * p.Cantidad)
                };

                _context.Presupuestos.Add(presupuesto);
                await _context.SaveChangesAsync();

                // Añado los productos a ProductoPresupuesto
                foreach (var producto in viewModel.Productos.Where(p => p.IsSelected))
                {
                    var productoPresupuesto = new ProductoPresupuesto
                    {
                        PresupuestoId = presupuesto.Id,
                        ProductoId = producto.ProductoId,
                        Cantidad = producto.Cantidad
                    };
                    _context.ProductosPresupuestos.Add(productoPresupuesto);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = presupuesto.Id });
            }

            ViewBag.Clientes = new SelectList(_context.Clientes, "Id", "Nombre");
            return View(viewModel);
        }

        // GET: Presupuesto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var presupuesto = await _context.Presupuestos.FindAsync(id);
            if (presupuesto == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Id", presupuesto.ClienteId);
            return View(presupuesto);
        }

        // POST: Presupuesto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha,TotalEstimado,Aprobado,ClienteId")] Presupuesto presupuesto)
        {
            if (id != presupuesto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(presupuesto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PresupuestoExists(presupuesto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Id", presupuesto.ClienteId);
            return View(presupuesto);
        }

        // GET: Presupuesto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var presupuesto = await _context.Presupuestos
                .Include(p => p.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (presupuesto == null)
            {
                return NotFound();
            }

            return View(presupuesto);
        }

        // POST: Presupuesto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var presupuesto = await _context.Presupuestos.FindAsync(id);
            if (presupuesto != null)
            {
                _context.Presupuestos.Remove(presupuesto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PresupuestoExists(int id)
        {
            return _context.Presupuestos.Any(e => e.Id == id);
        }
    }
}
