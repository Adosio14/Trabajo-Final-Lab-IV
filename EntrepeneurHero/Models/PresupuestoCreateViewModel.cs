public class PresupuestoCreateViewModel
{
    public DateTime Fecha { get; set; }
    public int ClienteId { get; set; }
    public List<ProductoSelectionViewModel> Productos { get; set; }
}

public class ProductoSelectionViewModel
{
    public int ProductoId { get; set; }
    public string Nombre { get; set; }
    public decimal Precio { get; set; }
    public bool IsSelected { get; set; }
    public int Cantidad { get; set; }
}