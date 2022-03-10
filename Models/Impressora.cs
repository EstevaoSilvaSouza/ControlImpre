using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Control_Impressora.Models
{
    public class Impressora
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string VersaoDrive { get; set; }
        public string Mac { get; set; }
        public string IpImpre { get; set; }
        public int QtdEstoqueToner { get; set; }
        //public string Status { get; set; }
        //public string Departamento { get; set; }
        public int QtdEstoqueUsoToner { get; set; }

        // ATRIBUTOS PARA MAPEAR NAS EMPRESAS
        public int? EmpresaId { get; set; }
        public Empresa Empresa { get; set; }

        // ATRIBUTOS PARA MAPEAR O ENDERECO
        public int? EnderecoId { get; set; }
        public Endereco Endereco { get; set; }

        ////// ATRIBUTOS PARA MAPEAR O TECNICO
        //public int? TecnicoId { get; set; }
        //public Tecnico Tecnico { get; set; }

        public string? UsuarioId { get; set; }
        public ApplicationUser applicationUser { get; set; }
        



    }
}
