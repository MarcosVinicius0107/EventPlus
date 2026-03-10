using EventPlus.WebAPI.BdcontextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Repositories;

public class TipoUsuarioRepository : ITipoUsuarioRepository
{
    private readonly EventContext _context;
    public TipoUsuarioRepository(EventContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Atualiza um tipo de usuário existente no banco de dados.
    /// </summary>
    /// <param name="id">identificação do tipo de usuário a ser atualizado.</param>
    /// <param name="tipoUsuario">atualização do tipo de usuário com as novas informações.</param> 
    public void Atualizar(Guid id, TipoUsuario tipoUsuario)
    {
        var tipoUsuarioBuscado = _context.TipoUsuarios.Find(id);
        if (tipoUsuarioBuscado != null)
        {
            tipoUsuarioBuscado.Titulo = tipoUsuario.Titulo;
            _context.SaveChanges();
        }
    }

    /// <summary>
    /// Busca um tipo de usuário no banco de dados com base em seu ID.
    /// </summary>
    /// <param name="id">busca o tipo de usuário correspondente ao ID fornecido.</param>
    /// <returns>retorna o tipo de usuário encontrado ou null se não for encontrado.</returns>
    public TipoUsuario BuscarPorId(Guid id)
    {
        return _context.TipoUsuarios.Find(id)!;
    }

    /// <summary>
    /// cadastra um novo tipo de usuário no banco de dados
    /// </summary>
    /// <param name="tipoUsuario">cadastro do tipo de usuário com as informações fornecidas</param>
    public void Cadastrar(TipoUsuario tipoUsuario)
    {
        _context.TipoUsuarios.Add(tipoUsuario);
        _context.SaveChanges();
    }

    /// <summary>
    /// deleta um tipo de usuário do banco de dados com base em seu ID.
    /// </summary>
    /// <param name="id"></param>
    public void Deletar(Guid id)
    {
        var tipoUsuarioBuscado = _context.TipoUsuarios.Find(id);
        if (tipoUsuarioBuscado != null)
        {
            _context.TipoUsuarios.Remove(tipoUsuarioBuscado);
            _context.SaveChanges();
        }
    }

    /// <summary>
    /// Lista todas os TipoUsuarios cadastradas no banco de dados
    /// </summary>
    /// <returns>Lista de TipoUsuario encontrados</returns>
    public List<TipoUsuario> Listar()
    {
        return _context.TipoUsuarios.OrderBy(TipoUsuario => TipoUsuario.Titulo).ToList();
    }
}
