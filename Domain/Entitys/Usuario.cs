using Domain.Notifications;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entitys
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



        [Required]
        public int Id { get; set; }
        [Required, MinLength(5), MaxLength(45)]
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
                notification.AddMessage("Erro, Data Inválida");

            if (!ValidarIdade())
                notification.AddMessage("Erro, Usuario precisa ser maior de idade");

            return notification.Valid;
        }

        public bool ValidarData()
        {
            return DataDeNacimento <= DateTime.Now;
        }

        public bool ValidarIdade()
        {
            return CalcularIdade(DataDeNacimento) >= 18;
        }

        public int CalcularIdade(DateTime Nacimento)
        {
            var idade = DateTime.Now.Year - Nacimento.Year;
            if (Nacimento > DateTime.Now.AddYears(-idade)) idade--;

            return idade;
        }

    }
}
