using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TipoEventoController : ControllerBase
{
    private ITipoEventoRepository _tipoEventoRepository;

    public TipoEventoController(ITipoEventoRepository tipoEventoRepository)
    {
        _tipoEventoRepository = tipoEventoRepository;
    }
    /// <summary>
    /// Endpoint da API que faz chamada para o método de listar os tipos de evento
    /// </summary>
    /// <returns>Status code 200 e a lista de tipos de evento</returns>
    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_tipoEventoRepository.Listar());
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que faz a chamada para um método de busca um tipo de evento específico
    /// </summary>
    /// <param name="id"> Id do tipo de evento buscado</param>
    /// <returns>Status code 200 e tipo de evento buscado</returns>
    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_tipoEventoRepository.BuscarPorId(id));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que faz a chamada para o método de cadastrar um tipo de evento
    /// </summary>
    /// <param name="tipoEvento">Tipo de evento a ser cadastrado</param>
    /// <returns>Status code 201 e o tipo de evento cadastrado</returns>
    [HttpPost]
    public IActionResult Cadastrar(TipoEventoDTO tipoEvento)
    {
        try
        {
            var novoTipoEvento = new TipoEvento
            {
                Titulo = tipoEvento.Titulo!
            };

            _tipoEventoRepository.Cadastrar(novoTipoEvento);

            return StatusCode(201, novoTipoEvento);//Retorna o status code 201 (Created) e o tipo de evento cadastrado
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que faz a chamada para o método de atualizar um tipo de evento
    /// </summary>
    /// <param name="id">Id do tipo de evento a ser atualizado</param>
    /// <param name="tipoEvento"> Tipo de evento com os dados atualizado</param>
    /// <returns>Status code 204 e o tipo de evento atualizado</returns>
    [HttpPut("{id}")]
    public IActionResult Put(Guid id, TipoEvento tipoEvento)
    {
        try
        {
            _tipoEventoRepository.Atualizar( id, tipoEvento );
            return StatusCode(204, tipoEvento); //Retorna o status code 204 (No Content) indicando que a atualização foi bem-sucedida, mas não há conteúdo para retornar
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que faz a chamada para o método de deletar um tipo de evento
    /// </summary>
    /// <param name="id">Id do tipo do evento a ser excluído</param>
    /// <returns>Status code 204</returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        try
        {
            _tipoEventoRepository.Deletar(id);
            return StatusCode(204); //Retorna o status code 204 (No Content) indicando que a exclusão foi bem-sucedida, mas não há conteúdo para retornar
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }
    }
}
