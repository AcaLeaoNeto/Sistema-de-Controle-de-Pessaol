using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entitys.Base
{
    public class UserBase
    {
        public string Name { get; set; } = string.Empty;
        public DateTime DataDeNacimento { get; set; }
        public string Sexo { get; set; } = string.Empty;
        public string Setor { get; set; } = string.Empty;
    }
}
