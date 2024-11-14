namespace EntrepeneurHero.Models
{
    public class Venta
    {
        public Venta()
        {
            ProductosVenta = [];
        }

        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal MontoTotal { get; set; }
        public string MetodoDePago { get; set; }

        // Relación con Cliente
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        // Relación con Productos
        public ICollection<ProductoVenta> ProductosVenta { get; set; }
    }

}
