namespace EntrepeneurHero.Models
{
    public class PresupuestoEditViewModel
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int ClienteId { get; set; }
        public List<ProductoSelectionViewModel> Productos { get; set; }
    }
}
