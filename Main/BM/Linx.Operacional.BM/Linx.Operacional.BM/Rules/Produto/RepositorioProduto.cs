using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linx.Operacional.BM.Rules.Produto
{
    public class RepositorioProduto
    {
        private LinxOperacional contexto = null;

        public RepositorioProduto(LinxOperacional contexto)
        {
            this.contexto = contexto; 
        }

        public LinxOperacional GetContexto()
        {
            return contexto;
        }

        public void Alter(PRD_SKU_PRODUTO produto)
        {
            this.contexto.Entry(produto).State = EntityState.Modified;
        }

        public PRD_SKU_PRODUTO GetProduto(int idSku)
        {
            return this.contexto.PRD_SKU_PRODUTO
                .Where(p => p.ID_SKU == idSku)
                .ToList()
                .FirstOrDefault();
        }

        public PRD_SKU_PRODUTO GetProduto(string codSku)
        {
            return this.contexto.PRD_SKU_PRODUTO
                .Where(p => p.COD_SKU == codSku)
                .ToList()
                .FirstOrDefault();
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
