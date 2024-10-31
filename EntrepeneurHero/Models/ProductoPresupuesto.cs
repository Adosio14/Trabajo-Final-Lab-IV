namespace EntrepeneurHero.Models
{
    public class ProductoPresupuesto
    {
        public int PresupuestoId { get; set; }
        public Presupuesto Presupuesto { get; set; }

        public int ProductoId { get; set; }
        public Producto Producto { get; set; }

        public int Cantidad { get; set; }
    }

}
