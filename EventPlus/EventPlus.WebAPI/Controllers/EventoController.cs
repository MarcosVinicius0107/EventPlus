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

    /// <summary>
    /// Endpoint da API que faz chamada para um método de cadastrar um novo evento
    /// </summary>
    /// <param name="eventoDTO">Objeto com os dados do evento a ser cadastrado</param>
    /// <returns>Status code 201 indicando que o evento foi criado</returns>
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

    /// <summary>
    /// Endpoint da API que faz chamada para um método de buscar um evento pelo seu id
    /// </summary>
    /// <param name="id">Id do evento a ser buscado</param>
    /// <returns>Status code 200 com o evento encontrado ou 404 caso não exista</returns>
    [HttpGet("{id}")]
    public IActionResult BuscarEventos(Guid id)
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

    /// <summary>
    /// Endpoint da API que faz chamada para um método de atualizar um evento existente
    /// </summary>
    /// <param name="id">Id do evento que será atualizado</param>
    /// <param name="eventoDTO">Objeto com os novos dados do evento</param>
    /// <returns>Status code 201 com o evento atualizado</returns>
    [HttpPut("{id}")]
    public IActionResult AtualizarProximosEventos(Guid id, EventoDTO eventoDTO)
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

            _eventoRepository.Atualizar(id, novoEvento);
            return StatusCode(201, _eventoRepository.BuscarPorId(id));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que faz chamada para um método de deletar um evento pelo id
    /// </summary>
    /// <param name="id">Id do evento que será deletado</param>
    /// <returns>Status code 204 indicando que o evento foi removido</returns>
    [HttpDelete("{id}")]
    public IActionResult DeletarProximosEventos(Guid id)
    {
        try
        {
            _eventoRepository.Deletar(id);
            return NoContent();
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
}
