using System.ComponentModel.DataAnnotations;

namespace Filme.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [MaxLength(20, ErrorMessage = "Este campo deve conter no máximo 20 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter no mínimo 3 caracteres")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [MaxLength(20, ErrorMessage = "Este campo deve conter no máximo 20 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter no mínimo 3 caracteres")]
        public string Password { get; set; }

        public string Role { get; set; }
    }

}