using Domain.Entitys.Base;
using Domain.Entitys.Login;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Entitys.Usuario
{
    public class User
    {
        public User(string name, DateTime dataDeNacimento, string sexo, string setor)
        {
            Name = name;
            DataDeNacimento = dataDeNacimento;
            Sexo = sexo;
            Idade = CalcularIdade(dataDeNacimento);
            Setor = setor;
        }

        public static explicit operator User(UserBase dto)
        {
            return new User(dto.Name, dto.DataDeNacimento, dto.Sexo, dto.Setor);
        }


        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public DateTime DataDeNacimento { get; set; }
        [Required]
        public string Sexo { get; set; } = string.Empty;
        public int Idade { get; set; }
        public string Setor { get; set; }
        [JsonIgnore]
        public Log Log { get; set; }
        public bool Ativo { get; set; } = true;

        private int CalcularIdade(DateTime Nacimento)
        {
            var idade = DateTime.Now.Year - Nacimento.Year;
            if (Nacimento > DateTime.Now.AddYears(-idade)) idade--;

            return idade;
        }

    }
}
