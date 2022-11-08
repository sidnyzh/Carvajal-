namespace Carvajal.Services.Client.Services
{
    public interface IPedidoService
    {
        ICollection<Pedido> Pedidos { get; set; }
        Pedido Pedido { get; set; }
        Response<object> Respuesta { get; set; }
        Task ObtenerPedidos();
        Task<Response<object>> InsertarPedido(Pedido pedido);
        Task<Pedido> ObtenerPedido(int id);
        Task<Response<object>> ActualizarPedido(Pedido pedido);
        Task<Response<object>> EliminarPedido(int id);
    }
}
