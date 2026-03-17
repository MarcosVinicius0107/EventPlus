using EventPlus.WebAPI.BdcontextEvent;
using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories;

public class EventoRepository : IEventoRepository
{
    private readonly EventContext _context;

    public EventoRepository(EventContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Atualiza um evento existente
    /// </summary>
    /// <param name="eventoDto">Objeto com os dados atualizados</param>
    public void Atualizar(Guid Id, Evento evento)
    {
        var eventoBuscado = _context.Eventos.Find(Id);
        if (eventoBuscado != null)
        {
            eventoBuscado.Nome = evento.Nome;
            eventoBuscado.Descricao = evento.Descricao;
            eventoBuscado.DataEvento =  evento.DataEvento;
            eventoBuscado.IdTipoEvento =  evento.IdTipoEvento;
            eventoBuscado.IdInstituicao =  evento.IdInstituicao;


            _context.SaveChanges();
        }
    }

    /// <summary>
    /// Busca um evento pelo seu ID
    /// </summary>
    /// <param name="id">ID do evento</param>
    /// <returns>Evento encontrado ou null</returns>
    public Evento BuscarPorId(Guid id)
    {
        return _context.Eventos.Include(e => e.IdTipoEventoNavigation).Include(e => e.IdInstituicaoNavigation).FirstOrDefault(e => e.IdEvento == id);
    }

    /// <summary>
    /// Cadastra um novo evento
    /// </summary>
    /// <param name="evento">Objeto evento</param>
    public void Cadastrar(Evento evento)
    {
        _context.Eventos.Add(evento);
        _context.SaveChanges();
    }

    /// <summary>
    /// Deleta um evento pelo ID
    /// </summary>
    /// <param name="IdEvento">ID do evento</param>
    public void Deletar(Guid IdEvento)
    {
        var eventoBuscado = _context.Eventos.Find(IdEvento);

        if (eventoBuscado != null)
        {
            _context.Eventos.Remove(eventoBuscado);
            _context.SaveChanges();
        }
    }

    /// <summary>
    /// Lista todos os eventos
    /// </summary>
    /// <returns>Lista de eventos</returns>
    public List<Evento> Listar()
    {
        return _context.Eventos
            .OrderBy(eventoBuscado => eventoBuscado.Nome)
            .ToList();
    }

    /// <summary>
    /// Método que busca eventos na qual o usuário confirmou presença
    /// </summary>
    /// <param name="IdUsuario">Id do usuário a ser buscado</param>
    /// <returns>Lista de eventos</returns>
    public List<Evento> ListarPorId(Guid IdUsuario)
    {
        return _context.Eventos
        .Include(e => e.IdTipoEventoNavigation)
        .Include(e => e.IdInstituicaoNavigation)
        .Where(e => e.Presencas.Any(p => p.IdUsuario == IdUsuario && p.Situacao == true))
        .ToList();

    }

    /// <summary>
    /// Método que traz a lista de próximos eventos
    /// </summary>
    /// <returns>Uma lista de eventos</returns>
    public List<Evento> ProximosEventos()
    {
        return _context.Eventos
            .Include(e => e.IdTipoEventoNavigation)
            .Include(e => e.IdInstituicaoNavigation)
            .Where(e => e.DataEvento >= DateTime.Now)
            .OrderBy(e => e.DataEvento)
            .ToList();
    }
}
