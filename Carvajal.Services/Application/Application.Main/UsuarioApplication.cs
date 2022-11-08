using Application.Interfaces;
using Application.Response;
using Domain.Repository;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Main
{
    public class UsuarioApplication : IUsuarioApplication
    {
        private readonly IRepository<Usuario> _usuario;
        private readonly PedidosContext _context;

        public UsuarioApplication(IRepository<Usuario> usuario, PedidosContext context)
        {
            _usuario = usuario;
            _context = context;
        }

        public async Task<Response<bool>> CreateUsuario(Usuario usuario)
        {
            var response = new Response<bool>();

            try
            {
                var existeUsuario = await _usuario.GetById(usuario.Id);
                if (existeUsuario != null)
                {
                    usuario.Password = SHA512(usuario.Password);
                    var usuarioCreado = await _usuario.Insert(usuario);
                    if (usuarioCreado)
                    {
                        response.IsSuccess = true;
                        response.Message = "El usuario se creó exitosamente";
                    }
                }
                else
                {
                    response.Message = $"Ya existe un usuario con el nombre {usuario.Nombre}";
                    response.IsError = true;
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

        public async Task<Response<string>> AutenticarUsuario(Usuario Usuario)
        {
            var response = new Response<string>();
            try
            {
                var usuario = await _context.Usuarios.AnyAsync(x => x.Nombre == Usuario.Nombre && x.Password == SHA512(Usuario.Password));
                if (usuario)
                {
                    response.Message = "Autenticación exitosa";
                    response.IsSuccess = true;
                }
                else
                {
                    response.Message = "Verifique los datos nuevamente";
                    response.IsError = true;
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

        public async Task<Response<bool>> DeleteUsuario(int id)
        {
            var response = new Response<bool>();
            try
            {
                var existeUsuario = await _usuario.GetById(id);

                if (existeUsuario != null)
                {
                    var deleteUser = await _usuario.Delete(id);
                    response.IsSuccess = true;
                    response.Message = "El usuario fue eliminado exitosamente";
                }
                else
                {
                    response.Message = "El usuario no existe";
                    response.IsError = true;
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



        public async Task<Response<bool>> UpdateUsuario(Usuario usuario)
        {
            var response = new Response<bool>();
            try
            {
                var existeUsuario = _usuario.GetById(usuario.Id);
                if (existeUsuario != null)
                {
                    usuario.Password = SHA512(usuario.Password);
                    var usuarioActualizado = await _usuario.Update(usuario);

                    if (usuarioActualizado)
                    {
                        response.IsSuccess = true;
                        response.Message = "El usuario fue actualizado exitosalmente";
                    }
                }
                else
                {
                    response.Message = "El usuario no existe";
                    response.IsError = true;


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

        private string SHA512(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            using (var hash = System.Security.Cryptography.SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);
                var hashedInputStringBuilder = new StringBuilder(128);
                foreach (var b in hashedInputBytes)
                    hashedInputStringBuilder.AppendFormat("{0:x2}", b);
                return hashedInputStringBuilder.ToString();
            }
        }

    }

}
