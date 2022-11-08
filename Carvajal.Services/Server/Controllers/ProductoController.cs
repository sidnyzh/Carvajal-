using Application.Interfaces;
//using Carvajal.Services.Shared;
using Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Carvajal.Services.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoApplication _productoApplication;

        public ProductoController(IProductoApplication productoApplication)
        {
            _productoApplication = productoApplication;
        }

        [HttpPost]

        public async Task<IActionResult> InsertarProducto([FromBody] Producto producto)
        {

            var response = await _productoApplication.InsertProducto(producto);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }

        }

        [HttpGet("{id}")]

        public async Task<IActionResult> ObtenerProducto(int id)
        {
            var response = await _productoApplication.GetproductoById(id);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpGet]

        public async Task<IActionResult> ObtenerProductos()
        {
            var response = await _productoApplication.GetProductos();

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpPut]

        public async Task<IActionResult> ActualizarProducto([FromBody] Producto producto)
        {
            var response = await _productoApplication.UpdateProducto(producto);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }

        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> EliminarProducto(int Id)
        {
            var response = await _productoApplication.DeleteProducto(Id);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }

        }
            
    }
}
