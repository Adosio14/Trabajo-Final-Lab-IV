public class VentaCreateViewModel
{
    public DateTime Fecha { get; set; } = DateTime.Now;
    public int ClienteId { get; set; }
    public string MetodoDePago { get; set; }
    public decimal MontoTotal { get; set; }
    public List<ProductoSeleccionViewModel> Productos { get; set; }
}

public class ProductoSeleccionViewModel
{
    public int ProductoId { get; set; }
    public string Nombre { get; set; }
    public decimal Precio { get; set; }
    public int Cantidad { get; set; }
    public bool IsSelected { get; set; }
}