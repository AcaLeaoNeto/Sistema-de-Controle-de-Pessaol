using Domain.Enums;
using Domain.Notifications;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entitys.Usuario
{
    public class Usuario
    {

        public Usuario(string name, DateTime dataDeNacimento, string sexo)
        {
            Name = name;
            DataDeNacimento = dataDeNacimento;
            Sexo = sexo;
            Idade = CalcularIdade(dataDeNacimento);
        }

        public static explicit operator Usuario(UsuarioDto dto)
        {
            return new Usuario(dto.Name, dto.DataDeNacimento, dto.Sexo);
        }

        [Required]
        public int Id { get; set; }
        [Required, MinLength(1), MaxLength(85)]
        public string Name { get; set; } = string.Empty;
        [Required]
        public DateTime DataDeNacimento { get; set; }
        [Required]
        public string Sexo { get; set; } = string.Empty;
        public int Idade { get; set; }
        public bool Ativo { get; set; } = true;


        public bool Validation(INotification notification)
        {
            if (!ValidarData())
                notification.AddMessage(" Erro, Data Inválida ");

            if (!ValidarIdade())
                notification.AddMessage(" Erro, Usuario precisa ser maior de idade ");

            if (!ValidarSexo())
                notification.AddMessage(" Erro, Os generos incorreto");

            return notification.Valid;
        }

        private bool ValidarData()
        {
            return DataDeNacimento <= DateTime.Now;
        }

        private bool ValidarIdade()
        {
            return CalcularIdade(DataDeNacimento) >= 18;
        }

        private bool ValidarSexo()
        {
            return new List<string>(Enum.GetNames(typeof(Generos))).Contains(Sexo);
        }

        private int CalcularIdade(DateTime Nacimento)
        {
            var idade = DateTime.Now.Year - Nacimento.Year;
            if (Nacimento > DateTime.Now.AddYears(-idade)) idade--;

            return idade;
        }

    }
}
