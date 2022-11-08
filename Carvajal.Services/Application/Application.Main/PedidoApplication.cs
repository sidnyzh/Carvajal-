using Application.Interfaces;
using Application.Response;
using ApplicationDTO;
using Domain.Repository;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Main
{
    public class PedidoApplication : IPedidoApplication
    {
        private readonly IRepository<Pedido> _pedido;
        private readonly IRepository<Producto> _producto;
        private readonly PedidosContext _context;

        private const double IVA = 0.19;


        public PedidoApplication(IRepository<Pedido> pedido, IRepository<Producto> producto, PedidosContext context)
        {
            _producto = producto;
            _pedido = pedido;
            _context = context;
        }

        public async Task<Response<bool>> InsertPedido(CreatePedidoDTO Pedido)
        {
            var response = new Response<bool>();
            try
            { 
                var usuario = await _context.Usuarios.AnyAsync(x => x.Id == Pedido.Usuario);
                if (usuario)
                {
                    var producto = await _producto.GetById(Pedido.Producto);

                    if (producto != null)
                    {
                        decimal subtotal = producto.Valor * ((decimal)Pedido.Cantidad);
                        double totalIVA = ((double)subtotal) * IVA;
                        decimal total = (decimal)totalIVA + subtotal;

                        Pedido pedido = new Pedido()
                        {
                            Usuario = Pedido.Usuario,
                            Producto = Pedido.Producto,
                            Cantidad = Pedido.Cantidad,
                            Subtotal = subtotal,
                            Iva = totalIVA,
                            Total = total,
                        };

                        var pedidoCreado = await _pedido.Insert(pedido);

                        if (pedidoCreado)
                        {
                            response.IsSuccess = true;
                            response.Message = "Pedidio creado exitosamente";
                        }
                    }
                    else
                    {
                        response.Message = "El producto no existe";
                    }
                }
                else
                {
                    response.Message = "El usuario no existe";
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
        public async Task<Response<Pedido>> GetPedido(int id)
        {
            var response = new Response<Pedido>();
            try
            {
                var pedido = await _pedido.GetById(id); 

                if (pedido != null)
                {
                    response.IsSuccess = true;
                    response.Data = pedido;
                    response.Message = "Pedido obtenido exitosamente";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "El pedido no existe";

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

        public async Task<Response<IEnumerable<Pedido>>> GetPedidos()
        {
            Response<IEnumerable<Pedido>> response = new();

            try
            {
                var pedido = await _pedido.GetAllAsync();

                if (pedido != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Lista de pedidios btenida exitosamente";
                    response.Data = pedido;
                }
                else
                {
                    response.Message = "No hay pedidos en la base de datos";
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
        public async Task<Response<bool>> DeletePedido(int id)
        {
            var response = new Response<bool>();
            
            try
            {
                var existePedido = await _context.Pedidos.AnyAsync(x => x.Id == id);

                if (existePedido)
                {
                    await _pedido.Delete(id);
                    response.IsSuccess = true;
                    response.Message = "Pedido eliminado exitosamente";
                }
                else
                {
                    response.Message = "El pedido no existe";
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
        public async Task<Response<bool>> Updatepedido(Pedido pedido)
        {
           
            var response = new Response<bool>();
            try
            {
                var existeUsuario = await _context.Usuarios.AnyAsync(x => x.Id == pedido.Usuario);
                if (existeUsuario)
                {
                    Producto producto = await _producto.GetById(pedido.Producto);

                    if (producto != null)
                    {
                        pedido.Subtotal = producto.Valor * ((decimal)pedido.Cantidad);
                        pedido.Iva = ((double)pedido.Subtotal) * IVA;
                        pedido.Total = (decimal)pedido.Iva + pedido.Subtotal;

                        var pedidoActualizado = await _pedido.Update(pedido);

                        if (pedidoActualizado)
                        {
                            response.IsSuccess = true;
                            response.Message = "Pedidio actualizado exitosamente";

                        }
                    }
                    else
                    {
                        response.Message = "El producto no existe";
                    }
                }
                else
                {
                    response.Message = "El usuario no existe";
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

