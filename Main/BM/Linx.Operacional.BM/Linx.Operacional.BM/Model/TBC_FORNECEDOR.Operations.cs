using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Linx.Tools;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Linx.Operacional.BM
{
	
	////////////////////////////////////////////////////////////////////////////
	//////////////////////// Business Operations Definition ////////////////////
	////////////////////////////////////////////////////////////////////////////
    public partial class TBC_FORNECEDOR : ILinx
    {
        /// <summary>
        /// Cria, atualiza ou busca fornecedor 
        /// </summary>
        /// <param name="tbcFornecedor"></param>
        /// <returns></returns>
        public static TBC_FORNECEDOR ResolveFornecedor(TBC_FORNECEDOR tbcFornecedor)
        {
            if (tbcFornecedor.TBC_PFJ == null)
                throw new Exception("[TBC_PFJ] não encontrado no contexto de atualização. \n ***Crítica gerada por [Linx.Operacional.BM.TBC_FORNECEDOR.ResolveFornecedor]***");

            LinxOperacional context = new LinxOperacional();
            TBC_FORNECEDOR fornecedor = context.TBC_FORNECEDOR.Include("TBC_PFJ").Where(r => r.TBC_PFJ.CNPJ_CPF == tbcFornecedor.TBC_PFJ.CNPJ_CPF && r.ID_FORNECEDOR != null).FirstOrDefault();
            TBC_PFJ pfj = context.TBC_PFJ.FirstOrDefault(p => p.CNPJ_CPF == tbcFornecedor.TBC_PFJ.CNPJ_CPF);


            if (fornecedor.IsNull() && tbcFornecedor != null)
            {
                string mensagem = "";

                if (tbcFornecedor.ID_LINX.IsNullOrEmpty())
                    mensagem += "[ID_LINX] - Campo requerido não informado na origem \n";

                if (tbcFornecedor.TBC_PFJ.CNPJ_CPF.IsNullOrEmpty())
                    mensagem += "[CNPJ_CPF] - Campo requerido não informado na origem \n";

                if (tbcFornecedor.TBC_PFJ.NOME_FANTASIA_APELIDO.IsNullOrEmpty())
                    mensagem += "[NOME_FANTASIA_APELIDO] - Campo requerido não informado na origem \n";


                //COMENTADO POIS NO PROCESSO DE ENTRADA DE NOTA ESSAS INFORMAÇÕES NÃO ESTÃO DISPONÍVEIS
                //FAVOR NÃO DESCOMENTAR SEM AVISAR ALGUÉM DO FFC

                //if (tbcFornecedor.TBC_PFJ.ID_REGIME_TRIBUTARIO == null)
                //    mensagem += "[ID_REGIME_TRIBUTARIO] - Campo requerido não informado na origem \n";

                //if (tbcFornecedor.TBC_PFJ.ID_INDICADOR_FISCAL_PFJ == null)
                //    mensagem += "[ID_INDICADOR_FISCAL_PFJ] - Campo requerido não informado na origem \n";

                if (!mensagem.IsNullOrEmpty())
                    throw new Exception(mensagem + "***Crítica gerada por [Linx.Operacional.BM.TBC_FORNECEDOR.ResolveFornecedor]***");

                fornecedor = context.TBC_FORNECEDOR.Create();

                if (pfj != null)
                    fornecedor.TBC_PFJ = pfj;
                else
                    fornecedor.TBC_PFJ = context.TBC_PFJ.Create();

                //fornecedor.TBC_PFJ.ID_LINX = tbcFornecedor.ID_LINX;
                fornecedor.TBC_PFJ.CODIGO_PFJ = tbcFornecedor.TBC_PFJ.CODIGO_PFJ.IsNullOrEmpty() ? tbcFornecedor.TBC_PFJ.CNPJ_CPF : tbcFornecedor.TBC_PFJ.CODIGO_PFJ;
                fornecedor.TBC_PFJ.RAZAO_SOCIAL_NOME_COMPLETO = tbcFornecedor.TBC_PFJ.RAZAO_SOCIAL_NOME_COMPLETO.IsNullOrEmpty() ? tbcFornecedor.NOME_FORNECEDOR : tbcFornecedor.TBC_PFJ.RAZAO_SOCIAL_NOME_COMPLETO;
                fornecedor.TBC_PFJ.DATA_ALTERACAO = System.DateTime.Now;
                fornecedor.TBC_PFJ.DATA_CADASTRO = System.DateTime.Now;

                fornecedor.CODIGO_FORNECEDOR = tbcFornecedor.TBC_PFJ.CODIGO_PFJ.IsNullOrEmpty() ? String.Empty : tbcFornecedor.TBC_PFJ.CODIGO_PFJ;
                fornecedor.NOME_FORNECEDOR = tbcFornecedor.TBC_PFJ.NOME_FANTASIA_APELIDO.IsNullOrEmpty() ? String.Empty : tbcFornecedor.TBC_PFJ.NOME_FANTASIA_APELIDO;
                fornecedor.INDICA_LICENCIADO = tbcFornecedor.INDICA_LICENCIADO.IsNull() ? false : tbcFornecedor.INDICA_LICENCIADO;
                fornecedor.LX_TIPO_FRETE = tbcFornecedor.LX_TIPO_FRETE.IsNull() ? (short)0 : tbcFornecedor.LX_TIPO_FRETE;
                fornecedor.INATIVO = false;

                context.TBC_FORNECEDOR.Add(fornecedor);
                context.SaveChanges();
            }
            else
            {
                if ((!String.IsNullOrEmpty(tbcFornecedor.TBC_PFJ.LOGRADOURO) && fornecedor.TBC_PFJ.LOGRADOURO != tbcFornecedor.TBC_PFJ.LOGRADOURO)
                    || (!String.IsNullOrEmpty(tbcFornecedor.TBC_PFJ.NUMERO) && fornecedor.TBC_PFJ.NUMERO != tbcFornecedor.TBC_PFJ.NUMERO))
                {
                    fornecedor.TBC_PFJ.LOGRADOURO = tbcFornecedor.TBC_PFJ.LOGRADOURO;
                    fornecedor.TBC_PFJ.NUMERO = tbcFornecedor.TBC_PFJ.NUMERO;
                    fornecedor.TBC_PFJ.COMPLEMENTO = tbcFornecedor.TBC_PFJ.COMPLEMENTO;
                    fornecedor.TBC_PFJ.BAIRRO = tbcFornecedor.TBC_PFJ.BAIRRO;
                    fornecedor.TBC_PFJ.ID_MUNICIPIO = tbcFornecedor.TBC_PFJ.ID_MUNICIPIO;
                    fornecedor.TBC_PFJ.MUNICIPIO = tbcFornecedor.TBC_PFJ.MUNICIPIO;
                    fornecedor.TBC_PFJ.ID_UF = tbcFornecedor.TBC_PFJ.ID_UF;
                    fornecedor.TBC_PFJ.UF = tbcFornecedor.TBC_PFJ.UF;
                    fornecedor.TBC_PFJ.ID_CEP = tbcFornecedor.TBC_PFJ.ID_CEP;
                    fornecedor.TBC_PFJ.CEP = tbcFornecedor.TBC_PFJ.CEP;
                    fornecedor.TBC_PFJ.ID_PAIS = tbcFornecedor.TBC_PFJ.ID_PAIS;
                    fornecedor.TBC_PFJ.OBS_ENDERECO = tbcFornecedor.TBC_PFJ.OBS_ENDERECO;
                    fornecedor.TBC_PFJ.DATA_ALTERACAO = DateTime.Now;

                    if (TBC_PFJ.ValidaPreenchimentoEnderecoCompletoTBC(fornecedor.TBC_PFJ))
                        context.SaveChanges();
                    else
                        throw new Exception("Endereço do fornecedor não foi preenchido corretamente!");
                }
            }

            return fornecedor;
        }

        public static TBC_FORNECEDOR ValidaFornecedor(int idFornecedor)
        {
            LinxOperacional context = new LinxOperacional();

            TBC_FORNECEDOR fornecedorValida = context.TBC_FORNECEDOR.Where(r => r.ID_FORNECEDOR == idFornecedor).FirstOrDefault();

            return fornecedorValida;

        }

        public static TBC_FORNECEDOR ValidaFornecedor(string cnpjCpf)
        {
            LinxOperacional context = new LinxOperacional();
            TBC_FORNECEDOR fornecedorValida = new TBC_FORNECEDOR();

            if (!cnpjCpf.IsNullOrEmpty())
                fornecedorValida = context.TBC_FORNECEDOR.Include("TBC_PFJ").Where(r => r.TBC_PFJ.CNPJ_CPF == cnpjCpf).FirstOrDefault();

            return fornecedorValida;

        }

        public static bool Exists(string cnpj)
        {
            LinxOperacional context = new LinxOperacional();
            return context.TBC_FORNECEDOR.Include("TBC_PFJ")
                .Any(f => f.TBC_PFJ.CNPJ_CPF.Equals(cnpj));
        }
    }
}
