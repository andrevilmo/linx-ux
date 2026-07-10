using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Linx.Tools;
using System.Linq;
using System.ComponentModel.Composition;
using Linx.Framework.ControleSistema.BM;


namespace Linx.Framework.ControleSistema.BM.Implementations
{

    ////////////////////////////////////////////////////////////////////////////
    //////////////////////////// Business Implementation ///////////////////////
    ////////////////////////////////////////////////////////////////////////////
    [Export(typeof(IAuditHelper))]
    [ExportMetadata("ImplementationName", "AuditHelper")]
    public partial class AuditHelper : IAuditHelper
    {
        private ControleSistemaContext _ctx;
        private ControleSistemaContext Context
        {
            get
            {
                if (this._ctx == null) _ctx = new ControleSistemaContext();
                return _ctx;
            }
        }
        public long Audit(string assemblyName)
        {
            var adt = new ADT_AUDITORIA()
            {
                ID_USUARIO = ControleSistemaContext.SecurityHelper.GetCurrentUserId().Value,
                ID_LINX = ControleSistemaContext.SecurityHelper.GetCurrentIdLinxEnvironment().Value,
                ASSEMBLY_NAME = assemblyName,
                DATA_HORA = DateTime.Now,
                CONNECTION_STRING = Context.Database.Connection.ConnectionString
            };

            Context.ADT_AUDITORIA.Add(adt);

            Context.SaveChanges();

            return adt.ID_ADT_AUDITORIA;
        }

        public long AuditItem(long auditId, string schemaTabela, string nomeTabela, string operation)
        {
            var adt = new ADT_AUDITORIA_ITEM()
            {
                ID_ADT_AUDITORIA = auditId,
                SCHEMA_TABELA = schemaTabela,
                NOME_TABELA = nomeTabela,
                TIPO_OPERACAO = operation
            };

            Context.ADT_AUDITORIA_ITEM.Add(adt);

            Context.SaveChanges();

            return adt.ID_ADT_AUDITORIA_ITEM;
        }

        public long AuditItemDetalhe(long auditItem, string nomePropriedade, string valorAntigo, string valorNovo)
        {
            var adt = new ADT_AUDITORIA_ITEM_DETALHE()
            {
                ID_ADT_AUDITORIA_ITEM = auditItem,
                PROPRIEDADE = nomePropriedade,
                VALOR_ANTIGO = valorAntigo,
                VALOR_NOVO = valorNovo
            };

            Context.ADT_AUDITORIA_ITEM_DETALHE.Add(adt);

            Context.SaveChanges();

            return adt.ID_ADT_AUDITORIA_ITEM_DETALHE;
        }
    }
}
