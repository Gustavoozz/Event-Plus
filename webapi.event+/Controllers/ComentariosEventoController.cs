using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Azure.CognitiveServices.ContentModerator;
using System.Text;
using webapi.event_.Domains;
using webapi.event_.Repositories;

namespace webapi.event_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ComentariosEventoController : ControllerBase
    {
        // Acesso aos metodos do repositorio.
        ComentariosEventoRepository comentario = new ComentariosEventoRepository();

        // Armazena dados da API externa ( IA - Azure ).
        private readonly ContentModeratorClient _contentModeratorClient;

        // Construtor que recebe os dados necessarios para o acesso ao serviço externo.
        public ComentariosEventoController(ContentModeratorClient contentModeratorClient)
        {
            _contentModeratorClient = contentModeratorClient;
        }

        [HttpPost("CadastroIA")]
        public async Task<IActionResult> PostIa(ComentariosEvento comentariosEvento)
        {
            try
            {
                // Se a descrição do comentário não for passada no objeto:
                if(string.IsNullOrEmpty(comentariosEvento.Descricao))
                {
                    return BadRequest("O texto a ser analisado não pode ser vazio!");
                }

                // Converte a string ( descrição do comentário ) em um MemoryStream.
                using var stream = new MemoryStream(Encoding.UTF8.GetBytes(comentariosEvento.Descricao));

                // Realiza a moderação do conteúdo ( descrição do comentário ).
                var moderationResult = await _contentModeratorClient.TextModeration.ScreenTextAsync("text/plain", stream, "por", false, false, null,
                    true);


                // Se existir termos ofensivos:
                if (moderationResult.Terms != null) 
                {
                    // Atribuir false para Exibe.
                    comentariosEvento.Exibe = false;

                    // Cadastra o comentario.
                    comentario.Cadastrar(comentariosEvento);
                }

                else
                {
                    comentariosEvento.Exibe= true;

                    comentario.Cadastrar(comentariosEvento);
                }

                return StatusCode(201, comentariosEvento);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                throw;
            }
        }


        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(comentario.Listar());
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);   
            }
        }


        [HttpGet("ListarSomenteExibe")]
        public IActionResult GetIa()
        {
            try
            {
                return Ok(comentario.ListarSomenteExibe());
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("BuscarPorIdUsuario")]
        public IActionResult GetByIdUser(Guid idUsuario, Guid idEvento)
        {
            try
            {
                return Ok(comentario.BuscarPorIdUsuario(idUsuario, idEvento));   
            }

            catch (Exception e) 
            { 
                return BadRequest(e.Message);
            }
        }


        [HttpPost]
        public IActionResult Post(ComentariosEvento novoComentario)
        {
            try
            {
                comentario.Cadastrar(novoComentario);
                return StatusCode(201, novoComentario);
            }

            catch (Exception e) 
            { 
            return BadRequest(e.Message);
            }
        }



        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                comentario.Deletar(id);
                return NoContent();
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}
