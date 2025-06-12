using System.ComponentModel.DataAnnotations;

namespace DonghuaFlix.Backend.src.Core.Application.DTOs.User;   


    public class UpdateUserInput
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, ErrorMessage = "Nome deve ter no máximo 100 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email é obrigatório")]
        public string Email { get; set; }
    }