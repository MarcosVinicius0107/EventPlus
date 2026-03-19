using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PresencaController : ControllerBase
{
    private readonly IPresencaRepository _presencaRepository;   
    public PresencaController(IPresencaRepository presencaRepository)
    {
        _presencaRepository = presencaRepository;
    }

    /// <summary>
    /// Endpoint da API que retorna uma presenç por id
    /// </summary>
    /// <param name="id">id da pesença a ser buscada</param>
    /// <returns>Status code 200 e presenca buscada</returns>
    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_presencaRepository.BuscarPorId(id));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que retorna uma lista de presenças filtrando pelo id do usuário
    /// </summary>
    /// <param name="idUsuario">id do usuário para filtragem</param>
    /// <returns>Lista de presença filtrada pelo usuário</returns>
    [HttpGet("ListarMinhas/{idUsuario}")]
     public IActionResult BuscarPorUsuario(Guid idUsuario)
     {
        try
        {
            return Ok(_presencaRepository.ListarMinhas(idUsuario));
        }
        catch (Exception e)
        {

            return BadRequest(e.Message);  
        }
     }

    /// <summary>
    /// Endpoint da API que faz chamada para o método de listar as presenças
    /// </summary>
    /// <returns>Status code 200 e a lista de tipos de presenças</returns>
    [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                return Ok(_presencaRepository.Listar());
            }
            catch (Exception e)
            {
    
                return BadRequest(e.Message);
            }
    }

    /// <summary>
    /// Endpoint da API que Inscreve/Cadastra uma nova presença
    /// </summary>
    /// <param name="presenca">Nome da presença cadastrada</param>
    /// <returns>Presença Buscada</returns>
    [HttpPost]
    public IActionResult Inscrever(PresencaDTO presenca)
    {
        try
        {
            var novaPresenca = new Presenca
            {
                Situacao = presenca.Situacao,
                IdUsuario = presenca.IdUsuario,
                IdEvento = presenca.IdEvento
            };
            _presencaRepository.Inscrever(novaPresenca);
            return Ok(novaPresenca);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que atualiza uma presença por id
    /// </summary>
    /// <param name="id">Busca o Id da presença a ser atualizado</param>
    /// <param name="presenca">retorna Status code 204 e lista a presenças atualizadas</param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public IActionResult Atualizar(Guid id, PresencaDTO presencaDto)
    {
        try
        {
            var presencaAtualizada = new Presenca
            {
                Situacao = presencaDto.Situacao,
                IdUsuario = presencaDto.IdUsuario,
                IdEvento = presencaDto.IdEvento
            };
            _presencaRepository.Atualizar(id, presencaAtualizada);
            return StatusCode(204, presencaAtualizada);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
     }
     /// <summary>
     /// Endpoint da API que deleta uma presença por id
     /// </summary>
     /// <param name="id">id da presença a ser deletada</param>
     /// <returns>Status code 204 indicando que a presença foi deletada</returns>
     [HttpDelete("{id}")]
     public IActionResult Deletar(Guid id)
     {
        try
        {
            _presencaRepository.Deletar(id);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


}
