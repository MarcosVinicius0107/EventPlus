using EventPlus.WebAPI.BdcontextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories;

public class PresencaRepository : IPresencaRepository
{
    private readonly EventContext _eventContext;

    public PresencaRepository(EventContext eventContext)
    {
        _eventContext = eventContext;
    }

    /// <summary>
    /// Metodo para atualizar uma presença por id
    /// </summary>
    /// <param name="id">id do método que será atualizado</param>
    /// <param name="presenca">Retorna a presença buscada</param>
    public void Atualizar(Guid IdPresencaEvento)
    {
        var presencaBuscada = _eventContext.Presencas.Find(IdPresencaEvento);

        if (presencaBuscada != null)
        {
            presencaBuscada.Situacao = !presencaBuscada.Situacao;
            _eventContext.SaveChanges();
        }
    }

    public void Atualizar(Guid id, Presenca presenca)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Busca uma presença por id
    /// </summary>
    /// <param name="id">id da presença a ser buscada</param>
    /// <returns>presença buscada</returns>
    public Presenca BuscarPorId(Guid id)
    {
        return _eventContext.Presencas.Include(p => p.IdEventoNavigation).ThenInclude(e => e!.IdInstituicaoNavigation).FirstOrDefault(p => p.IdPresenca == id)!;
    }

    /// <summary>
    /// Método para deletar uma presença por id
    /// </summary>
    /// <param name="id">id do método que será deletado</param>
    public void Deletar(Guid id)
    {
        var presencaBuscada = _eventContext.Presencas.Find(id);
        if (presencaBuscada != null)
        {
            _eventContext.Presencas.Remove(presencaBuscada);
            _eventContext.SaveChanges();
        }
    }

    /// <summary>
    /// Método para inscrever um usuário em um evento, criando uma nova presença
    /// </summary>
    /// <param name="Inscricao">Tipo de presença a ser cadastrado</param>
    public void Inscrever(Presenca Inscricao)
    {
        var novaInscricao = new Presenca
        {
            Situacao = Inscricao.Situacao,
            IdUsuario = Inscricao.IdUsuario,
            IdEvento = Inscricao.IdEvento
        };
        _eventContext.Presencas.Add(novaInscricao);
        _eventContext.SaveChanges();
    }

    /// <summary>
    /// Método para listar as presenças cadastradas no sistema
    /// </summary>
    /// <returns>Retorna uma lista com as presenças</returns>
    public List<Presenca> Listar()
    {
        return _eventContext.Presencas.OrderBy(Presenca => Presenca.Situacao).ToList();
    }

    /// <summary>
    /// Lista as presenças de um usuário específico
    /// </summary>
    /// <param name="IdUsuario">id do usuário para filtragem</param>
    /// <returns>Uma lista de presenças de um usuário específico</returns>
    public List<Presenca> ListarMinhas(Guid IdUsuario)
    {
        return _eventContext.Presencas.Include(p => p.IdEventoNavigation).ThenInclude(e => e!.IdInstituicaoNavigation).Where(p => p.IdUsuario == IdUsuario).ToList();
    }
}
