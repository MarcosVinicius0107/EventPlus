using Azure;
using Azure.AI.ContentSafety;
using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ComentarioEventoController : ControllerBase
{
    private readonly ContentSafetyClient _contentSafetyClient;
    private readonly IComentarioEventoRepository _comentarioEventoRepository;

    public ComentarioEventoController(ContentSafetyClient contentSafetyClient, IComentarioEventoRepository comentarioEventoRepository)
    {
        _contentSafetyClient = contentSafetyClient;
        _comentarioEventoRepository = comentarioEventoRepository;
    }

    /// <summary>
    /// Endpoint da API que cadastra e modera um comentário
    /// </summary>
    /// <param name="comentarioEvento">Comentário a ser moderado</param>
    /// <returns>Status Code 201 e o comentário criado</returns>
    [HttpPost]
    public async Task<IActionResult> Cadastrar(ComentarioEventoDTO comentarioEvento)
        {
        try
        {
            if (string.IsNullOrEmpty(comentarioEvento.Descricao))
            {
                return BadRequest("A descrição do comentário não pode ser vazia.");
            }

            //criar objeto de análise
            var request = new AnalyzeTextOptions(comentarioEvento.Descricao);
            
            //chamar a API do Azure Cntext Safety
            Response<AnalyzeTextResult> response = await _contentSafetyClient.AnalyzeTextAsync(request);

            //Verificar se o texto tem alguma severidade maior que 0
            bool temConteudoImproprio = response.Value.CategoriesAnalysis.Any(comentario => comentario.Severity > 0);

            var novoComentario = new ComentarioEvento
            {
                Descricao = comentarioEvento.Descricao,
                IdUsuario = comentarioEvento.IdUsuario,
                IdEvento = comentarioEvento.IdEvento,
                DataComentarioEvento = DateTime.Now,
                //Define se o comentario vai ser exibido
                Exibe = !temConteudoImproprio
            };

            //Cadastrar o comentário
            _comentarioEventoRepository.Cadastrar(novoComentario);

            return StatusCode(201, novoComentario);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{idUsuario}/{idEvento}")]
    public IActionResult ComentarioUsuarioEvento(Guid idUsuario, Guid idEvento)
    {
        try
        { var comentario = _comentarioEventoRepository.BuscarPorIdUsuario(idUsuario, idEvento);
            if (comentario == null)
            {
                return NotFound("Comentário não encontrado para o usuário e evento especificados.");
            }
            return Ok(comentario);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("evento/{idEvento}")]
    public IActionResult Listar(Guid idEvento)
    {
        try
        {   
            return Ok(_comentarioEventoRepository.Listar(idEvento));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("ExibeTrue{IdEvento}")]
    public IActionResult ListarSomenteExibe(Guid IdEvento)
    {
        try
        {
            return Ok(_comentarioEventoRepository.ListarSomenteExibe(IdEvento));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }




    [HttpDelete]
    public IActionResult DeletarComentario(Guid idComentario)
    {
        try
        {
            _comentarioEventoRepository.Deletar(idComentario);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
