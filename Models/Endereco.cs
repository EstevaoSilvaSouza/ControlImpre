using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Control_Impressora.Models
{
    public class Endereco
    {
        public  int Id { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Base { get; set; }

        public virtual ICollection<Empresa> empresas { get; set; }
        public virtual ICollection<Impressora> impressoras { get; set; }
        public virtual ICollection<ApplicationUser> applicationUser { get; set; }

    }
}
