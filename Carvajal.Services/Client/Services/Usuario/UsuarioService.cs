using Microsoft.AspNetCore.Components;
using System;
using System.Net.Http.Json;

namespace Carvajal.Services.Client.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;
        public UsuarioService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }
        public ICollection<Usuario> Usuarios { get; set; }
        public Usuario Usuario { get; set; }
        public bool Exitoso { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Response<object> Respuesta { get; set; }

        public async Task<Response<object>> ActualizarUsuario(Usuario usuario)
        {
            var result = await _http.PutAsJsonAsync<Usuario>($"api/Usuario/", usuario);
            return await SetUsuario(result);
        }

        public async Task<Response<object>> AutenticarUsuario(Usuario usuario)
        {
            var result = await _http.PostAsJsonAsync<Usuario>($"api/Usuario/", usuario);
            return await SetUsuario(result);
        }

        public async Task<Response<object>> EliminarUsuario(int id)
        {
            var result = await _http.DeleteAsync($"api/Usuario/{id}");
            return await SetUsuario(result);
        }

        public async Task<Response<object>> InsertarUsuario(Usuario usuario)
        {
            var result = await _http.PostAsJsonAsync<Usuario>("api/Usuario/CrearUsuario", usuario);
            return await SetUsuario(result);
        }

        public async Task<Usuario> ObtenerUsuario(int id)
        {
            var result = await _http.GetFromJsonAsync<Response<Usuario>>($"api/Usuario/{id}");
            if (result != null)
                Usuario = result.Data;
            return Usuario;
        }

        public async Task ObtenerUsuarios()
        {
            var result = await _http.GetFromJsonAsync<Response<ICollection<Usuario>>>("api/Usuario");
            if (result != null)
                Usuarios = result.Data;
        }

        private async Task<Response<object>> SetUsuario(HttpResponseMessage result)
        {
            var response = await result.Content.ReadFromJsonAsync<Response<object>>();
            Respuesta = response;
            _navigationManager.NavigateTo("Login");

            return Respuesta;
        }
    }
}
