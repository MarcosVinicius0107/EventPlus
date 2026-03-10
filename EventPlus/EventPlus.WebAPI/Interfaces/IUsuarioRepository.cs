using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Interfaces;

public interface IUsuarioRepository
{
    void Cadastrar(Usuario usuario);
    Usuario BuscarPorId(Guid id);
    Usuario BUscarPorEmailESenha(string email, string senha);
}
