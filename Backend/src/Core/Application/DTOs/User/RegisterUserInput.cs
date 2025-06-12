using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;

namespace DonghuaFlix.Backend.src.Core.Application.DTOs.User;

public class RegisterUserInput
{
    [Required(ErrorMessage ="Nome é obrigatorio")]
    [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Email é obrigatorio")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Senha é obrigatorio")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 100 caracteres.")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Confirmação de senha é obrigatorio")]
    [Compare("Password", ErrorMessage = "A confirmação de senha não corresponde à senha.")]
    public string ConfirmPassword { get; set; }
}