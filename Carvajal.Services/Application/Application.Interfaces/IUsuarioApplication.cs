using Application.Response;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUsuarioApplication
    {

        Task<Response<bool>> CreateUsuario(Usuario usuario);

        Task<Response<string>> AutenticarUsuario(Usuario usuario);
        Task<Response<bool>> UpdateUsuario(Usuario usuario);
        Task<Response<bool>> DeleteUsuario(int id);

    }
}
