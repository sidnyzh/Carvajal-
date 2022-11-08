using Application.Response;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProductoApplication
    {
        Task<Response<Producto>> GetproductoById(int id);

        Task<Response<bool>> InsertProducto(Producto producto);

        Task<Response<bool>> UpdateProducto(Producto producto);

       Task<Response<bool>> DeleteProducto(int id);
       Task<Response<IEnumerable<Producto>>> GetProductos();

    }
}
