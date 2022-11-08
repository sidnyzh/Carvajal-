using Application.Interfaces;
using Application.Main;
using Domain.Repository;
using Entity.Models;
using NSubstitute;
using NSubstitute.Extensions;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Unit_Testing
{
    public class PedidoUnitTest
    {
        private readonly IPedidoApplication _pedidoApplication;
        private readonly IRepository<Pedido> _pedido = Substitute.For<IRepository<Pedido>>();
        private readonly IRepository<Producto> _producto = Substitute.For<IRepository<Producto>>();
        private readonly PedidosContext _context = Substitute.For<PedidosContext>();

        public PedidoUnitTest()
        {
            _pedidoApplication = new PedidoApplication(_pedido, _producto, _context);
        }

        [Fact]
        public async void GetPedidoExistenteDebeRetornarTrue()
        {

            Pedido pedido = new Pedido();

            pedido.Id = 1;
            pedido.Usuario = 1;

            _pedido.GetById(pedido.Id).Returns(pedido);
            var pedidoObtenido = await _pedidoApplication.GetPedido(pedido.Id);

            Assert.True(pedidoObtenido.IsSuccess);

            Assert.Equal("Pedido obtenido exitosamente", pedidoObtenido.Message);

        }

        [Fact]
        public async void GetPedidoInexistenteDebeRetornarFalse()
        {
            await _pedido.GetById(1);

            var pedidoObtenido = await _pedidoApplication.GetPedido(1);

            Assert.False(pedidoObtenido.IsSuccess);

            Assert.Equal("El pedido no existe", pedidoObtenido.Message);
        }

        [Fact]
        public async void GetPedidosInexistentesDebeRetornarFalse()
        {
            _pedido.GetAllAsync().ReturnsNull();
            var pedidos = await _pedidoApplication.GetPedidos();
            Assert.False(pedidos.IsSuccess);
            Assert.Equal("No hay pedidos en la base de datos", pedidos.Message);
        }

        [Fact]
        public async void GetPedidosExistentesDebeRetornarTrue()
        {
          
            IEnumerable<Pedido> pedidos = new List<Pedido>();

             _pedido.GetAllAsync().Returns(pedidos);
            var pedidosObtenidos = await _pedidoApplication.GetPedidos();

            Assert.True(pedidosObtenidos.IsSuccess);
            Assert.Equal("Lista de pedidios btenida exitosamente", pedidosObtenidos.Message);
        }
       

    }

    
}   
