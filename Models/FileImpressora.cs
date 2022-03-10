using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Control_Impressora.Models
{
    public class FileImpressora
    {
        public ICollection<FileOnDatabaseModel> FileOnDatabaseModel { get; set; }
        public ICollection<ImpressoraDate> ImpressoraDate { get; set; }
        public ImpressoraDate impressoraDate { get; set; }
        public Impressora Impressora { get; set; }
    }
}
