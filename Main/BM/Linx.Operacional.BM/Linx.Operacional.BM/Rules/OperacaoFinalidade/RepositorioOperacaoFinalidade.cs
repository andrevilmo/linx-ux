using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linx.Operacional.BM.Rules.OperacaoFinalidade
{
    public class RepositorioOperacaoFinalidade
    {
        private LinxOperacional contexto = null;

        public RepositorioOperacaoFinalidade(LinxOperacional contexto)
        {
            this.contexto = contexto;
            this.contexto.Configuration.AutoDetectChangesEnabled = true;
        }

        public LCF_OPERACAO_FINALIDADE GetOperacaoFinalidadeById(int IdOperacaoFinalidade) 
        {
            var query = this.contexto.LCF_OPERACAO_FINALIDADE.FirstOrDefault(p=>p.ID_OPERACAO_FINALIDADE == IdOperacaoFinalidade);

            return query;
        }        
    }
}
