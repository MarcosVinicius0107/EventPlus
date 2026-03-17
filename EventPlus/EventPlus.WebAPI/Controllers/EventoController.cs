using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventoController : ControllerBase
{
    private readonly IEventoRepository _eventoRepository;

    public EventoController(IEventoRepository eventoRepository)
    {
        _eventoRepository = eventoRepository;
    }

    /// <summary>
    /// endpoint da API que faz chamada para um método de listar eventos filtrando pelo id do usuário
    /// </summary>
    /// <param name="IdUsuario">Id do usuário para filtragem</param>
    /// <returns>Status code 200 e uma lista de eventos</returns>
    [HttpGet("Usuario/{IdUsuario}")]
    public IActionResult ListarPorId(Guid IdUsuario)
    {
        try
        {
            return Ok(_eventoRepository.ListarPorId(IdUsuario));
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }

    }

    /// <summary>
    /// Endpoint da API que faz chamada para um método de listar os próximos eventos
    /// </summary>
    /// <returns>Status code 200 e a lista dos próximos eventos</returns>
    [HttpGet("ListarProximos")]
    public IActionResult BuscarProximosEventos()
    {
        try
        {
            return Ok(_eventoRepository.ProximosEventos());
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }
    }

    [HttpPost("CadastrarProximosEventos")]
    public IActionResult CadastrarProximosEventos(EventoDTO eventoDTO)
    { 
       
        try
        {
        var novoEvento = new Evento
        {
            Nome = eventoDTO.Nome,
            Descricao = eventoDTO.Descricao,
            DataEvento = eventoDTO.DataEvento,
            IdTipoEvento = eventoDTO.IdTipoEvento,
            IdInstituicao = eventoDTO.IdInstituicao
        };
        _eventoRepository.Cadastrar(novoEvento);
            return StatusCode(201);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    [HttpGet("BuscarProximosEventos/{id}")]
    public IActionResult BuscarProximosEventos(Guid id)
    {
        try
        {
            var evento = _eventoRepository.BuscarPorId(id);
            if (evento == null)
            {
                return StatusCode(404);
            }
            return Ok(evento);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    [HttpPut("AtualizarProximosEventos/{id}")]
    public IActionResult AtualizarProximosEventos(Guid id, EventoDTO eventoDTO)
    {
        try
        {
            var eventoExistente = _eventoRepository.BuscarPorId(id);
            if (eventoExistente == null)
            {
                return StatusCode(404);
            }
            eventoExistente.Nome = eventoDTO.Nome;
            eventoExistente.Descricao = eventoDTO.Descricao;
            eventoExistente.DataEvento = eventoDTO.DataEvento;
            eventoExistente.IdTipoEvento = eventoDTO.IdTipoEvento;
            eventoExistente.IdInstituicao = eventoDTO.IdInstituicao;

            _eventoRepository.BuscarPorId(id);
            return StatusCode(201);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    [HttpDelete("DeletarProximosEventos/{id}")]
    public IActionResult DeletarProximosEventos(Guid id)
    {
        try
        {
            var eventoExistente = _eventoRepository.BuscarPorId(id);
            return StatusCode(201);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
}
