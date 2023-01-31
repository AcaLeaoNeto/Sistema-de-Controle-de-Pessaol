
using System.ComponentModel.DataAnnotations;

namespace Domain.Entitys.Usuario
{
    public class UsuarioDto
    {
        [Required, MaxLength(85, ErrorMessage = "Nome no maximo 85 caracteres")]
        public string Name { get; set; } = string.Empty;

        [Required]
        public DateTime DataDeNacimento { get; set; }

        [Required]
        public string Sexo { get; set; } = string.Empty;
    }
}
