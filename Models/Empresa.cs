using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Control_Impressora.Models
{
    public class Empresa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string NomeFantasia { get; set; }
        public string Cnpj { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }

        public int EnderecoId { get; set; }
        public Endereco Endereco { get; set; }

        public ICollection<Impressora> impressoras { get; set; }
    }
}
