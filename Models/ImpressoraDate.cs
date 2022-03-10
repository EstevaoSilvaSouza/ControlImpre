using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Control_Impressora.Models
{
    public class ImpressoraDate
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Contador 1")]
        public int NumberOne { get; set; }

        [Display(Name = "Contador 2")]
        public int NumberTwo { get; set; }
        public int Volume { get; set; }
        public int QtdPapel { get; set; }
        public int QtdPapelUtil { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime Date { get; set; }
        public int ImpressoraId { get; set; }
    }
}
