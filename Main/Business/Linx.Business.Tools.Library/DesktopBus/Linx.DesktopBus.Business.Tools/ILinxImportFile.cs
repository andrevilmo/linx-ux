using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Linx.Business.Tools
{
    public interface ILinxImportFile
    {
        object frImportarArquivo(int pUserID, DataSet pDataFile);
    }
}
