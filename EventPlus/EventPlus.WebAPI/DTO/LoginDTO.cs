using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPI.DTO;

public class LoginDTO
{
    [Required(ErrorMessage = "O Email do usuário é obrigatório!")]
    public string? Email { get; set; }
    [Required(ErrorMessage = "O Senha do usuário é obrigatório!")]
    public string? Senha { get; set; }
}
