using EventPlus.WebAPI.BdcontextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Repositories;

public class InstituicaoRepository : IInstituicao
{
    private readonly EventContext _context;
    public InstituicaoRepository(EventContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Atualiza os dados de uma instituição existente no banco de dados
    /// </summary>
    /// <param name="id">id da instituição a ser atualizada</param>
    /// <param name="instituicao">Instituição com os dados atualizados</param>
    public void Atualizar(Guid id, Instituicao instituicao)
    {
        var instituicaoBuscada = _context.Instituicaos.Find(id);
        if (instituicaoBuscada != null)
        {
            instituicaoBuscada.NomeFantasia = instituicao.NomeFantasia;
            instituicaoBuscada.Cnpj = instituicao.Cnpj;
            instituicaoBuscada.Endereco = instituicao.Endereco;
            _context.SaveChanges();
        }
    }

    /// <summary>
    /// Busca uma instituição no banco de dados pelo seu id e retorna os dados encontrados
    /// </summary>
    /// <param name="id">id da instituição a ser buscada</param>
    /// <returns>instituição encontrada ou null caso não exista</returns>
    public Instituicao BuscarPorId(Guid id)
    {
        return _context.Instituicaos.Find(id)!;

    }

    public void Cadastrar(Instituicao instituicao)
    {
        _context.Instituicaos.Add(instituicao);
        _context.SaveChanges();
    }

    /// <summary>
    /// Deleta uma instituição do banco de dados
    /// </summary>
    /// <param name="id"></param>id da instituição a ser deletada
    public void Deletar(Guid id)
    {
        var instituicaoBuscada = _context.Instituicaos.Find(id);
        if (instituicaoBuscada != null)
        {
            _context.Instituicaos.Remove(instituicaoBuscada);
            _context.SaveChanges();
        }   
    }

    /// <summary>
    /// Lista todas as instituições cadastradas no banco de dados
    /// </summary>
    /// <returns>Lista de instituições encontradas</returns>
    public List<Instituicao> Listar()
    {
        return _context.Instituicaos.OrderBy(Instituicao => Instituicao .NomeFantasia).ToList();

    }
}
