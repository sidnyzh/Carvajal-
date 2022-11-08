namespace Carvajal.Services.Client.Services
{
    public interface IProductoService
    {
        ICollection<Producto> Productos { get; set; }
        Producto Producto { get; set; }
        Response<object> Respuesta { get; set; }
        Task ObtenerProductos();
        Task<Response<object>> InsertarProducto(Producto producto);
        Task<Producto> ObtenerProducto(int id);
        Task<Response<object>> ActualizarProducto(Producto producto);
        Task<Response<object>> EliminarProducto(int id);

    }
}
