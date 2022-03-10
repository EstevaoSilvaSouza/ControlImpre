using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Control_Impressora.Models
{
    public class FileModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string TipoArquivo { get; set; }
        public string Extensao { get; set; }
        public string Descricao { get; set; }
        public int ImpressoraId { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime? CriadoEm { get; set; }
    }
}
