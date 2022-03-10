using Control_Impressora.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Control_Impressora.ViewModels
{
    public class EditUserViewModel
    {
        public EditUserViewModel()
        {
            Claims = new List<string>();
            Roles = new List<string>();
        }
        public string Id { get; set; }

        [Required(ErrorMessage = "O nome do usuário é obrigatório")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "O nome email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string Cargo { get; set; }
        public string Matricula { get; set; }
        public int? EnderecoId { get; set; }
        public Endereco Endereco { get; set; }
        public List<string> Claims { get; set; }
        public IList<string> Roles { get; set; }
    }
}
