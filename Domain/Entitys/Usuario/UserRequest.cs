
using Domain.Entitys.Base;
using Domain.Interfaces;
using Domain.Validation;

namespace Domain.Entitys.Usuario
{
    public class UserRequest
    {
        public string Name { get; set; } = string.Empty;
        public DateTime DataDeNacimento { get; set; }
        public string Sexo { get; set; } = string.Empty;
        public string Setor { get; set; } = string.Empty;
    }
}
