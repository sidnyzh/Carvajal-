using Application.Interfaces;
using Application.Response;
using Carvajal.Services.Server.Helpers;
//using Carvajal.Services.Shared;
using Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Carvajal.Services.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioApplication _UsuarioApplication;
        private readonly AppSettings _appSettings;

        public UsuarioController(IUsuarioApplication UsuarioApplication, IOptions<AppSettings> appSettings)
        {
            _UsuarioApplication = UsuarioApplication;
            _appSettings = appSettings.Value;
        }

        [HttpPost]
        public async Task<IActionResult> AutenticarUsuario([FromBody] Usuario Usuario)
        {

            var response = await _UsuarioApplication.AutenticarUsuario(Usuario);

            if (response.IsSuccess)
            {
                response.Data = BuildToken();
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }

        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> CrearUsuario([FromBody] Usuario Usuario)
        {

            var response = await _UsuarioApplication.CreateUsuario(Usuario);

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
        public async Task<IActionResult> ActualizarUsuario([FromBody] Usuario Usuario)
        {
            var response = await _UsuarioApplication.UpdateUsuario(Usuario);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }

        }

        [HttpDelete]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            var response = await _UsuarioApplication.DeleteUsuario(id);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        #region Private methods
        private string BuildToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddDays(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature),
                Issuer = _appSettings.IsSuer,
                Audience = _appSettings.Audience
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
        #endregion
    }
}
