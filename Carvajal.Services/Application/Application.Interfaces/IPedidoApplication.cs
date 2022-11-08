using Application.Response;
using ApplicationDTO;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPedidoApplication
    {
        Task<Response<Pedido>> GetPedido(int id);

        Task<Response<IEnumerable<Pedido>>> GetPedidos();

        Task<Response<bool>> InsertPedido(CreatePedidoDTO pedido);

        Task<Response<bool>> Updatepedido(Pedido pedido);
        Task<Response<bool>> DeletePedido(int id);

    }
}
