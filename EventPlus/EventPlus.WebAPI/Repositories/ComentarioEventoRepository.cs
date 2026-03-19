using EventPlus.WebAPI.BdcontextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories;

public class ComentarioEventoRepository : IComentarioEventoRepository
{
    private readonly EventContext _context;
    public ComentarioEventoRepository(EventContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Método para buscar um comentário de evento específico com base no ID do usuário e no ID do evento
    /// </summary>
    /// <param name="IdUsuario">Id do usuário a ser buscado</param>
    /// <param name="Evento">Evento a ser buscado</param>
    /// <returns>Status code 204 e a lista dos Usuários</returns>
    public ComentarioEvento BuscarPorIdUsuario(Guid IdUsuario, Guid IdEvento)
    {
        return _context.ComentarioEventos.Include(c => c.IdUsuarioNavigation).Include(c => c.IdEventoNavigation).FirstOrDefault(c => c.IdUsuario == IdUsuario && c.IdEvento == IdEvento)!;
    }

    /// <summary>
    /// Método para cadastrar um novo comentário de evento no banco de dado. Ele recebe um objeto ComentarioEvento como parâmetro, adiciona esse objeto ao contexto do banco de dados e salva as alterações para persistir o novo comentário.
    /// </summary>
    /// <param name="comentarioEvento">Cadastro de um comentário</param>
    public void Cadastrar(ComentarioEvento comentarioEvento)
    {
        _context.ComentarioEventos.Add(comentarioEvento);
        _context.SaveChanges();
    }

    /// <summary>
    /// Método para deletar um comentário de evento específico com base no ID do comentário
    /// </summary>
    /// <param name="id">Id do comentário a ser deletado</param>
    public void Deletar(Guid id)
    {
        var comentarioBuscado = _context.ComentarioEventos.Find(id);
        if (comentarioBuscado != null)
        {
            _context.ComentarioEventos.Remove(comentarioBuscado);
            _context.SaveChanges();
        }
    }

    /// <summary>
    /// método para listar os comentários de um evento específico com base no ID do evento
    /// </summary>
    /// <param name="IdEvento">Id do evento a ser listado</param>
    /// <returns>Status code 204 e a lista de comentário</returns>
    public List<ComentarioEvento> Listar(Guid IdEvento)
    {
        return _context.ComentarioEventos.OrderBy(comentarioEventoBuscado => comentarioEventoBuscado.Descricao).ToList();
    }

    /// <summary>
    /// Método para listar somente os comentários de um evento específico que estão marcados para exibição, com base no ID do evento
    /// </summary>
    /// <param name="IdEvento">Lista somente um id específico do evento</param>
    /// <returns>Status code 204 e lista específica do id</returns>
    public List<ComentarioEvento> ListarSomenteExibe(Guid IdEvento)
    {
        return _context.ComentarioEventos
            .Where(comentarioEventoBuscado => comentarioEventoBuscado.Exibe == true)
            .OrderBy(comentarioEventoBuscado => comentarioEventoBuscado.Descricao)
            .ToList();
    }
}

