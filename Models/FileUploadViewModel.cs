using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Control_Impressora.Models
{
    public class FileUploadViewModel
    {
        public virtual List<FileOnDatabaseModel> FilesOnDatabase { get; set; }
    }
}
