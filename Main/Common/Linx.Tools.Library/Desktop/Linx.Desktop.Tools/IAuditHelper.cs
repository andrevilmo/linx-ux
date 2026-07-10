using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linx.Tools
{
    public interface IAuditHelper
    {
        long Audit(string assemblyName);
        long AuditItem(long auditId, string schemaTabela, string nomeTabela, string operation);
        long AuditItemDetalhe(long auditItem, string nomePropriedade, string valorAntigo, string valorNovo);
    }
}
