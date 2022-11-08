using Application.Interfaces;
using Application.Main;
using Domain.Repository;
using Entity.Models;
using NSubstitute;
using Xunit;


namespace UnitTesting
{
    public class ProductoUnitTest
    {

        private readonly IProductoApplication _productoApplication;
        private readonly IRepository<Producto> _producto = Substitute.For<IRepository<Producto>>();


        public ProductoUnitTest()
        {
            _productoApplication = new ProductoApplication(_producto);
        }

        [Fact]
        public async void InsertarProductoExistenteDebeRetornarFalse()
        {
            //Arrange
            int id = 1;
            string descripcion = "Dados";
            decimal valor = 2000;
            Producto producto = new Producto()
            {
                Id = id,
                Descripcion = descripcion,
                Valor = valor
            };
            _producto.GetById(id).Returns(producto);

            //Act
            var insertarProducto = await _productoApplication.InsertProducto(producto);
            Assert.False(insertarProducto.IsSuccess);

            //Assert
            Assert.Equal("El producto ya existe", insertarProducto.Message);
        }
      

        [Fact]
        public async void ObtenerProductoInexistenteDebeRetornarFalse()
        {
            //Arrange
            int id = 1;

            //Act
            var producto = await _productoApplication.GetproductoById(id);

            //Assert
            Assert.False(producto.IsSuccess);
            Assert.Equal("El producto no existe", producto.Message);
        }

        [Fact]
        public async void EliminarProductoExistenteDebeRetornarTrue()
        {
            //Arrange
            int id = 1;
            string descripcion = "Dados";
            decimal valor = 2000;
            Producto producto = new Producto()
            {
                Id = id,
                Descripcion = descripcion,
                Valor = valor
            };

            _producto.GetById(1).Returns(producto);

            //Act
            var productoEliminado = await _productoApplication.DeleteProducto(1);

            //Assert
            Assert.True(productoEliminado.IsSuccess);
            Assert.Equal("Producto eliminado exitosamente", productoEliminado.Message);
        }

        [Fact]

        public async void EliminarProductoinexisteDebeRetornarFalse()
        {
            //Act
            var producto = await  _productoApplication.DeleteProducto(1);

            Assert.True(!producto.IsSuccess);
            Assert.Equal("El producto no existe", producto.Message);

        }

       
        [Fact]

        public async void ActualizarProductoExistenteDebeRetornarTrue()
        {
            //Arrange
            int id = 1;
            string descripcion = "Dados";
            decimal valor = 2000;
            Producto producto = new Producto()
            {
                Id = id,
                Descripcion = descripcion,
                Valor = valor
            };

            _producto.GetById(1).Returns(producto);
             _producto.Update(producto).Returns(true);

            //Act
            var productoActualizado = await _productoApplication.UpdateProducto(producto);

            //Assert
            Assert.Equal("El producto fue actualizado exitosalmente", productoActualizado.Message);
            Assert.True(productoActualizado.IsSuccess);
        }

        [Fact]

        public async void ActualizarProductoInexistenteDebeRetornarFalse()
        {
            //Arrange
            Producto producto = new Producto();

            //Act
            var productoActualizado = await _productoApplication.UpdateProducto(producto);

            //Assert
            Assert.Equal("El producto no existe", productoActualizado.Message);
            Assert.False(productoActualizado.IsSuccess);
        }
    }

}