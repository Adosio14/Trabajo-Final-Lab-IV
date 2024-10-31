namespace EntrepeneurHero.Models
{
    public class ProductoVenta
    {
        public int VentaId { get; set; }
        public Venta Venta { get; set; }

        public int ProductoId { get; set; }
        public Producto Producto { get; set; }

        public int Cantidad { get; set; }
    }

}
