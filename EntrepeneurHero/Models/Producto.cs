namespace EntrepeneurHero.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }

        // Relación con Ventas
        public ICollection<ProductoVenta> ProductosVenta { get; set; }

        // Relación con Presupuestos
        public ICollection<ProductoPresupuesto> ProductosPresupuesto { get; set; }
    }

}
