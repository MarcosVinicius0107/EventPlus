using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InstituicaoController : ControllerBase
{
    private IInstituicao _instituicaoRepository;

    public InstituicaoController(IInstituicao instituicaoRepository)
    {
        _instituicaoRepository = instituicaoRepository;
    }

    /// <summary>
    /// Endpoint da API que faz chamada para o método de listar as instituições
    /// </summary>
    /// <returns>Status code 200 e a lista de tipos de instituições</returns>
    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_instituicaoRepository.Listar());
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que faz a chamada para um método de busca um tipo de instituição específico
    /// </summary>
    /// <param name="id"> Id do tipo de instituição buscado</param>
    /// <returns>Status code 200 e tipo de instituição buscado</returns>
    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_instituicaoRepository.BuscarPorId(id));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que faz a chamada para o método de cadastrar um tipo de instituição
    /// </summary>
    /// <param name="tipoEvento">Tipo de instituição a ser cadastrado</param>
    /// <returns>Status code 201 e o tipo de instituição cadastrado</returns>
    [HttpPost]
    public IActionResult Cadastrar(InstituicaoDTO instituicao)
    {
        try
        {
            var novaInstituicao = new Instituicao
            {
                NomeFantasia = instituicao.NomeFantasia!,
                Cnpj = instituicao.Cnpj!,
                Endereco = instituicao.Endereco!
            };

            _instituicaoRepository.Cadastrar(novaInstituicao);
            return StatusCode(201);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que faz a chamada para o método de atualizar um tipo de instituição
    /// </summary>
    /// <param name="id">Id do tipo de instituicão a ser atualizado</param>
    /// <param name="tipoEvento"> Tipo de instituição com os dados atualizado</param>
    /// <returns>Status code 204 e o tipo de instituição atualizado</returns>
    [HttpPut("{id}")]
        public IActionResult Atualizar(Guid id, InstituicaoDTO instituicao)
        {
            try
            {
                var instituicaoAtualizada = new Instituicao
                {
                    NomeFantasia = instituicao.NomeFantasia!,
                    Cnpj = instituicao.Cnpj!,
                    Endereco = instituicao.Endereco!
                };
                _instituicaoRepository.Atualizar(id, instituicaoAtualizada);
                return StatusCode(200, _instituicaoRepository.BuscarPorId(id));
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

    /// <summary>
    /// Endpoint da API que faz a chamada para o método de deletar um tipo de instituição
    /// </summary>
    /// <param name="id">Id do tipo do instituição a ser excluído</param>
    /// <returns>Status code 204</returns>
    [HttpDelete("{id}")]
    public IActionResult Deletar(Guid id)
    {
        try
        {
            _instituicaoRepository.Deletar(id);
            return StatusCode(204);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

}

