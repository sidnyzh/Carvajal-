using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Carvajal.Services.Client.Services
{
    public class ProductoService : IProductoService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;
        public ProductoService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }
        public ICollection<Producto> Productos { get; set; }
        public Producto Producto { get; set; }
        public Response<object> Respuesta { get; set; }

        public async Task<Response<object>> ActualizarProducto(Producto producto)
        {
            var result = await _http.PutAsJsonAsync<Producto>($"api/Producto/", producto);
            return await SetProducto(result);
        }

        public async Task<Response<object>> EliminarProducto(int id)
        {
            var result = await _http.DeleteAsync($"api/Producto/{id}");
            return await SetProducto(result);
        }

        public async Task<Response<object>> InsertarProducto(Producto producto)
        {
            var result = await _http.PostAsJsonAsync<Producto>("api/Producto/", producto);
            return await SetProducto(result);
        }

        public async Task<Producto> ObtenerProducto(int id)
        {
            var result = await _http.GetFromJsonAsync<Response<Producto>>($"api/Producto/{id}");
            if (result != null)
                Producto = result.Data;
            return Producto;
        }

        public async Task ObtenerProductos()
        {
            var result = await _http.GetFromJsonAsync<Response<ICollection<Producto>>>("api/Producto");
            if (result != null)
                Productos = result.Data;
        }

        private async Task<Response<object>> SetProducto(HttpResponseMessage result)
        {
            var response = await result.Content.ReadFromJsonAsync<Response<object>>();
            Respuesta = response;
            _navigationManager.NavigateTo("Productos");
            return Respuesta;
        }
    }
}
