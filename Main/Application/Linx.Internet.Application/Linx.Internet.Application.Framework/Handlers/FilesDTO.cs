using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linx.Internet.Application.Framework.Handlers
{
    public class FilesDTO
    {
        public List<FilesStatusDTO> files = new List<FilesStatusDTO>();

        public FilesDTO(List<FilesStatusDTO> files) 
        {
            this.files = files; 
        }
    }
}
