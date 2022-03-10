using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Control_Impressora.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string Cargo { get; set; }
        public string Matricula { get; set; }
        public int? EnderecoId { get; set; }
        public Endereco Endereco { get; set; }
        public ICollection<Impressora> impressoras { get; set; }
    }
}
