using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EntrepeneurHero.Data;
using EntrepeneurHero.Models;

namespace EntrepeneurHero.Controllers
{
    public class VentaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VentaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Venta
        public async Task<IActionResult> Index()
        {
            var ventas = _context.Ventas
                .Include(v => v.Cliente)
                .OrderByDescending(v => v.Fecha);
            return View(await ventas.ToListAsync());
        }

        // GET: Venta/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.ProductosVenta)
                    .ThenInclude(pv => pv.Producto)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }

        // GET: Venta/Create
        public IActionResult Create()
        {
            var viewModel = new VentaCreateViewModel
            {
                Fecha = DateTime.Now,
                MetodoDePago = "Efectivo", // Default value
                Productos = _context.Productos
                    .Select(p => new ProductoSeleccionViewModel
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

        // POST: Venta/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VentaCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var venta = new Venta
                {
                    Fecha = viewModel.Fecha,
                    ClienteId = viewModel.ClienteId,
                    MetodoDePago = viewModel.MetodoDePago,
                    MontoTotal = viewModel.MontoTotal,
                    ProductosVenta = new List<ProductoVenta>()
                };

                // Add selected products to ProductoVenta
                foreach (var producto in viewModel.Productos.Where(p => p.IsSelected && p.Cantidad > 0))
                {
                    venta.ProductosVenta.Add(new ProductoVenta
                    {
                        ProductoId = producto.ProductoId,
                        Cantidad = producto.Cantidad
                    });
                }

                _context.Ventas.Add(venta);
                await _context.SaveChangesAsync();


                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = venta.Id });
            }

            ViewBag.Clientes = new SelectList(_context.Clientes, "Id", "Nombre");
            return View(viewModel);
        }
        // GET: Venta/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas
                .Include(v => v.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }

        // POST: Venta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venta = await _context.Ventas.FindAsync(id);
            if (venta != null)
            {
                _context.Ventas.Remove(venta);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VentaExists(int id)
        {
            return _context.Ventas.Any(e => e.Id == id);
        }
    }
}
