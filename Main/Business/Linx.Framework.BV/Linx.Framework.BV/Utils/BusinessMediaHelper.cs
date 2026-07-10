using Linx.Framework.BV.Multimidia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linx.Framework.BV
{
    class BusinessMediaHelper
    {
        public static void SyncMedia(string tableName, Int64? idKey, Guid? uidKey, List<Guid> media)
        {
            MultimidiaDomainService ds = new MultimidiaDomainService();
            ds.SyncMedia(new DocTabelaSync() { NomeTabela = tableName, IdChave = idKey, UidChave = uidKey, Midias = media });
        }
    }
}
