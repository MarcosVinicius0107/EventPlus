using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Interfaces;

public interface IComentarioEventoRepository
{
    void Cadastrar(ComentarioEvento comentarioEvento);
    void Deletar(Guid id);
    List<ComentarioEvento> Listar(Guid IdEvento);
    ComentarioEvento BuscarPorIdUsuario(Guid IdUsuario, Guid Evento);
    List<ComentarioEvento> ListarSomenteExibe(Guid IdEvento);
    object? ListarComentariosPorEvento(Guid idEvento);
    object? ComentarioUsuarioEvento(Guid idUsuario, Guid idEvento);
}
