namespace EntrepeneurHero.Models
{
    public class Presupuesto
    {

        public Presupuesto()
        {
            ProductosPresupuesto = [];
        }

        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal TotalEstimado { get; set; }
        public bool Aprobado { get; set; }

        // Relación con Cliente
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        // Relación con Productos
        public ICollection<ProductoPresupuesto> ProductosPresupuesto { get; set; }
    }

}
