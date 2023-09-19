using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using webapi.event_.tarde.Domains;
using webapi.event_.tarde.Interfaces;
using webapi.event_.tarde.Repositories;
using webapi.event_.tarde.ViewModels;

namespace webapi.event_.tarde.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("applications/json")]
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public LoginController()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel usuario)
        {
            try
            {
                Usuario usuarioBuscado = _usuarioRepository.BuscarPorEmailESenha(usuario.Email!, usuario.Senha!);

                if (usuarioBuscado == null)
                {
                    return NotFound("Email ou senha inválidos. Tente novamente!");
                }

                // Caso encontre o usuário buscado, prossegue para a criação do Token.

                // 1 - Definir as informações ( Claims ) que serão fornecidos no Token ( Payload ):
                var claims = new[]
                {
                    // Formato da claim ( Tipo, valor ).
                    new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.IdUsuario.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email!),
                    new Claim(ClaimTypes.Role, usuarioBuscado.IdTipoUsuario.ToString()),
    

                    // Existe a possibilidade de criar uma claim personalizada.
                    new Claim("Claim Personalziada", "Valor Personalizado")
                };

                // 2 - Definir a chave de acesso ao Token:
                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("event.chave.autenticacao-webapi-dev"));


                // 3 - Definir as credenciais do Token ( Header ):
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                // 4 - Gerar o token:
                var token = new JwtSecurityToken
                (

                // Emissor do token:
                issuer: "webapi.event+.tarde",

                // Destinatário:
                audience: "webapi.event+.tarde",

                //Dados definidos nas claims:
                claims: claims,

                // Tempo de expiração:
                expires: DateTime.Now.AddMinutes(5),

                // Credênciais do Token:
                signingCredentials: creds

                );

                // 5 - Retornar o token criado:
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                });
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);

            }
        }

    }
}

