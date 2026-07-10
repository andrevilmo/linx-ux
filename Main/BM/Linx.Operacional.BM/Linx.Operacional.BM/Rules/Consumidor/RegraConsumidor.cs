using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linx.Operacional.BM.Rules.Consumidor
{
    public class RegraConsumidor
    {
        private RepositorioConsumidor repositorioConsumidor = null;
        private LinxOperacional context = null;

        public RegraConsumidor()
        {
            this.context = new LinxOperacional();
            this.repositorioConsumidor = new RepositorioConsumidor(this.context);
        }

        public CRM_PFJ ResolveConsumidor(string nomeCliente, string codigoPFJ, string cnpjCPF, bool indicaPessoaFisica)
        {
            CRM_PFJ consumidor = null;

            try
            {
                consumidor = repositorioConsumidor.GetConsumidor(cnpjCPF);

                if (consumidor != null)
                    return consumidor;
                else
                {
                    consumidor = new CRM_PFJ()
                    {
                        CNPJ_CPF = cnpjCPF,
                        NOME_CLIENTE = nomeCliente,
                        CODIGO_PFJ = codigoPFJ,
                        INATIVO = false,
                        INDICA_CLIENTE_LOJA = true,
                        PF_PJ = indicaPessoaFisica,
                        DATA_CADASTRO = DateTime.Now,
                        DATA_ALTERACAO = DateTime.Now,
                        INDICA_PROSPECT = false,
                        INDICA_VENDEDOR_LOJA = false,
                        INDICA_FUNCIONARIO = false,
                        INDICA_LOYALTY = false,
                        INATIVO_PARA_CRM = false,
                        INDICA_DIVERGENCIA = false,
                        ENDERECO_ELETRONICO = String.Empty,
                        FID_INTENCAO_ADERIR = false,
                        QTDE_DEPENDENTES = 0,
                        OPTOUT_ENDERECO = false,
                        OPTOUT_TELEFONE = false,
                        OPTOUT_EMAIL = false,
                    };

                    repositorioConsumidor.Add(consumidor);
                    repositorioConsumidor.SaveChanges();
                    return consumidor;
                }
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }


    }
}