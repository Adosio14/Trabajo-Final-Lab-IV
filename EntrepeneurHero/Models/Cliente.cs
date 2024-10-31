namespace EntrepeneurHero.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string CorreoElectronico { get; set; }
        public string Telefono { get; set; }
        public string Empresa { get; set; }
        public string Direccion { get; set; }

        // Relación con Ventas y Presupuestos
        public ICollection<Venta> Ventas { get; set; }
        public ICollection<Presupuesto> Presupuestos { get; set; }
    }

}
