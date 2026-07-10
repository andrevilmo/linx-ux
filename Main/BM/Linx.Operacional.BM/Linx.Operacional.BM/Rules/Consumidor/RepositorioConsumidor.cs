using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linx.Operacional.BM.Rules.Consumidor
{
    public class RepositorioConsumidor
    {
        private LinxOperacional contexto = null;

        public RepositorioConsumidor(LinxOperacional contexto)
        {
            this.contexto = contexto;
        }

        public CRM_PFJ GetConsumidor(int idCrmPfj)
        {
            return contexto.CRM_PFJ
                .Where(w => w.ID_CRM_PFJ == idCrmPfj)
                .ToList().FirstOrDefault();
        }

        public CRM_PFJ GetConsumidor(string cnpjCpf)
        {
            return contexto.CRM_PFJ
                .Where(w => w.CNPJ_CPF == cnpjCpf)
                .ToList().FirstOrDefault();
        }
        
        public void Add(CRM_PFJ crmpfj)
        {
            this.contexto.CRM_PFJ.Add(crmpfj);
        }
        public void SaveChanges()
        {
            this.contexto.SaveChanges();
        }
        public void Dispose()
        {
            if (this.contexto != null)
                this.contexto.Dispose();
        }
    }
}

