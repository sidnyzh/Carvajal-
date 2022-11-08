namespace Carvajal.Services.Client.Services
{
    public interface IUsuarioService
    {
        ICollection<Usuario> Usuarios { get; set; }
        Usuario Usuario { get; set; }
        Response<object> Respuesta { get; set; }
        Task ObtenerUsuarios();
        Task<Response<object>> InsertarUsuario(Usuario usuario);
        Task<Usuario> ObtenerUsuario(int id);
        Task<Response<object>> ActualizarUsuario(Usuario usuario);
        Task<Response<object>> EliminarUsuario(int id);
        Task<Response<object>> AutenticarUsuario(Usuario usuario);

    }
}
