
using Application.Interfaces;
using Application.Response;
using Domain.Repository;
using Entity.Models;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Main
{
    public class ProductoApplication : IProductoApplication
    {
        private readonly IRepository<Producto> _producto;

        public ProductoApplication(IRepository<Producto> producto)
        {
            _producto = producto;
        }


        public async Task<Response<bool>> DeleteProducto (int id)
        {
            var response = new Response<bool>();

            try
            {

                var producto = await _producto.GetById(id);

                if (producto != null)
                {
                    await _producto.Delete(id);
                    response.IsSuccess = true;
                    response.Message = "Producto eliminado exitosamente";

                }
                else
                {
                    response.IsError = true;
                    response.Message = "El producto no existe";
                }
            }
            
            catch (Exception ex) 
            {
                response.IsSuccess = false;
                response.IsError = true;
                response.Message = "Error: " + ex.Message;
            }
            return response;
        }

        public async Task<Response<Producto>> GetproductoById(int id) 
        {
            var response = new Response<Producto>();
            try
            {
                var producto = await _producto.GetById(id);

                if (producto != null)
                {
                    response.IsSuccess = true;
                    response.Data = producto;
                    response.Message = "El producto fue obtenido exitosamente";
                }
                else
                {
                    response.IsError = true;
                    response.Message = "El producto no existe";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.IsError = true;
                response.Message = "Error: " + ex.Message;
            }
            return response;
        }

        public async Task<Response<IEnumerable<Producto>>> GetProductos()
        {
            Response<IEnumerable<Producto>> response = new();
            var productos = await _producto.GetAllAsync();

            try
            {
                if (productos != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Lista de prpductos obtenida exitosamente";
                    response.Data = productos;
                }
                else
                {
                    response.IsError = true;
                    response.Message = "No hay productos en la base de datos";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.IsError = true;
                response.Message = "Error: " + ex.Message;
            }
            return response;
        } 

        public async Task<Response<bool>> InsertProducto (Producto producto)
        {
            var response = new Response<bool>();

            try
            {
                {
                    var productoExiste = await _producto.GetById(producto.Id);

                        var ProductoInsertado = await _producto.Insert(producto);

                        if (ProductoInsertado)
                        {
                            response.IsSuccess = true;
                            response.Message = "El producto se insertó exitosamente";
                        }                    
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.IsError = true;
                response.Message = "Error: " + ex.Message;
            }
                return response;

        }

        public async Task<Response<bool>> UpdateProducto(Producto producto)
        {
            var response = new Response<bool>();

            try
            {
                var existeProducto = await _producto.GetById(producto.Id); 

                if (existeProducto != null)
                {
                    var productoActulizado = await _producto.Update(producto);

                    if (productoActulizado)
                    {
                        response.IsSuccess = true;
                        response.Message = "El producto fue actualizado exitosalmente";
                    }
                }
                
                else 
                {
                    response.IsError = true;
                    response.Message = "El producto no existe";
                    
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.IsError = true;
                response.Message = "Error: " + ex.Message;
            }
            return response;
        }
    }
}
