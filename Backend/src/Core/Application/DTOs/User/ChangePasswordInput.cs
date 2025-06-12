using System.ComponentModel.DataAnnotations;

namespace DonghuaFlix.Backend.src.Core.Application.DTOs.User;


    public class ChangePasswordInput
    {
        [Required(ErrorMessage = "Senha atual é obrigatória")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "Nova senha é obrigatória")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirmação da nova senha é obrigatória")]
        [Compare("NewPassword", ErrorMessage = "Senhas não coincidem")]
        public string ConfirmNewPassword { get; set; }
    }