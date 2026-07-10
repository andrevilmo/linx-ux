using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linx.Framework.BV.Multimidia;

namespace Linx.Business.Tools
{
    public class MediaHelper
    {
        public static void SyncMedia(string tableName, long? idKey, Guid? uidKey, List<Guid> media)
        { 
            MultimidiaDomainService ds = new MultimidiaDomainService();
            ds.SyncMedia(new DocTabelaSync() { NomeTabela = tableName, IdChave = idKey, UidChave = uidKey, Midias = media });
        }
    }
}
