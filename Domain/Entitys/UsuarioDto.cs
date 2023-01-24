
using System.ComponentModel.DataAnnotations;

namespace Domain.Entitys
{
    public class UsuarioDto
    {
        [Required, MinLength(1), MaxLength(85)]
        public string Name { get; set; } = string.Empty;
        [Required]
        public DateTime DataDeNacimento { get; set; }
        [Required]
        public string Sexo { get; set; } = string.Empty;

        //public static explicit operator Usuario(UsuarioDto dto)
        //{
        //    return new Usuario(dto.Name, dto.DataDeNacimento, dto.Sexo);
        //}
    }
}
