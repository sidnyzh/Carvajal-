using static System.Net.WebRequestMethods;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Carvajal.Services.Client.Pages;

namespace Carvajal.Services.Client.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;
        public PedidoService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }
        public ICollection<Pedido> Pedidos { get; set; }
        public Pedido Pedido { get; set; }
        public Response<object> Respuesta { get; set; }

        public async Task<Response<object>> ActualizarPedido(Pedido pedido)
        {
            var result = await _http.PutAsJsonAsync<Pedido>($"api/Pedido/", pedido);
            return await SetPedido(result);
        }

        public async Task<Response<object>> EliminarPedido(int id)
        {
            var result = await _http.DeleteAsync($"api/Pedido/{id}");
            return await SetPedido(result);
        }

        public async Task<Response<object>> InsertarPedido(Pedido pedido)
        {
            var result = await _http.PostAsJsonAsync<Pedido>("api/Pedido/", pedido);
            return await SetPedido(result);
        }

        public async Task<Pedido> ObtenerPedido(int id)
        {
            var result = await _http.GetFromJsonAsync<Response<Pedido>>($"api/Pedido/{id}");
            if (result != null)
                Pedido = result.Data;
            return Pedido;
        }

        public async Task ObtenerPedidos()
        {
            var result = await _http.GetFromJsonAsync<Response<ICollection<Pedido>>>("api/Pedido");
            if (result != null)
                Pedidos = result.Data;
        }

        private async Task<Response<object>> SetPedido(HttpResponseMessage result)
        {
            var response = await result.Content.ReadFromJsonAsync<Response<object>>();
            Respuesta = response;
            _navigationManager.NavigateTo("Pedidos");
            return Respuesta;
        }
    }
}
