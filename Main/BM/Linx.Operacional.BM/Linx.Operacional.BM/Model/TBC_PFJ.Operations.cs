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
    public partial class TBC_PFJ
    {
        /// <summary>
        /// Cria, Atualiza ou Busca TbcPfj
        /// </summary>
        /// <param name="tbcPfj"></param>
        /// <returns></returns>
        public static TBC_PFJ ResolvePfj(TBC_PFJ tbcPfj)
        {
            if (tbcPfj == null)
                throw new Exception("[TBC_PFJ] não encontrado no contexto de atualização. \n ***Crítica gerada por [Linx.Operacional.BM.TBC_PFJ.ResolvePfj]***");

            LinxOperacional context = new LinxOperacional();
            TBC_PFJ pfj = context.TBC_PFJ.Where(w => w.CNPJ_CPF == tbcPfj.CNPJ_CPF).FirstOrDefault();

            if (pfj.IsNull() && tbcPfj != null)
            {
                string mensagem = "";

                if (tbcPfj.ID_LINX.IsNullOrEmpty())
                    mensagem += "[ID_LINX] - Campo requerido não informado na origem \n";

                if (tbcPfj.CNPJ_CPF.IsNullOrEmpty())
                    mensagem += "[CNPJ_CPF] - Campo requerido não informado na origem \n";

                if (tbcPfj.NOME_FANTASIA_APELIDO.IsNullOrEmpty())
                    mensagem += "[NOME_FANTASIA_APELIDO] - Campo requerido não informado na origem \n";

                if (tbcPfj.ID_REGIME_TRIBUTARIO == null)
                    mensagem += "[ID_REGIME_TRIBUTARIO] - Campo requerido não informado na origem \n";

                if (tbcPfj.ID_INDICADOR_FISCAL_PFJ == null)
                    mensagem += "[ID_INDICADOR_FISCAL_PFJ] - Campo requerido não informado na origem \n";

                if (!mensagem.IsNullOrEmpty())
                    throw new Exception(mensagem + "***Crítica gerada por [Linx.Operacional.BM.TBC_PFJ.ResolvePfj]***");

                tbcPfj.CODIGO_PFJ = tbcPfj.CODIGO_PFJ.IsNullOrEmpty() ? tbcPfj.CNPJ_CPF : tbcPfj.CODIGO_PFJ;
                tbcPfj.RAZAO_SOCIAL_NOME_COMPLETO = tbcPfj.RAZAO_SOCIAL_NOME_COMPLETO.IsNullOrEmpty() ? tbcPfj.NOME_FANTASIA_APELIDO : tbcPfj.RAZAO_SOCIAL_NOME_COMPLETO;
                tbcPfj.DATA_ALTERACAO = System.DateTime.Now;
                tbcPfj.DATA_CADASTRO = System.DateTime.Now;

                context.TBC_PFJ.Add(tbcPfj);
                context.SaveChanges();
                pfj = tbcPfj;
            }
            else
            {
                if ((!String.IsNullOrEmpty(tbcPfj.LOGRADOURO) && pfj.LOGRADOURO != tbcPfj.LOGRADOURO)
                    || (!String.IsNullOrEmpty(tbcPfj.NUMERO) && pfj.NUMERO != tbcPfj.NUMERO))
                {
                    pfj.LOGRADOURO = tbcPfj.LOGRADOURO;
                    pfj.NUMERO = tbcPfj.NUMERO;
                    pfj.COMPLEMENTO = tbcPfj.COMPLEMENTO;
                    pfj.BAIRRO = tbcPfj.BAIRRO;
                    pfj.ID_MUNICIPIO = tbcPfj.ID_MUNICIPIO;
                    pfj.MUNICIPIO = tbcPfj.MUNICIPIO;
                    pfj.ID_UF = tbcPfj.ID_UF;
                    pfj.UF = tbcPfj.UF;
                    pfj.ID_CEP = tbcPfj.ID_CEP;
                    pfj.CEP = tbcPfj.CEP;
                    pfj.ID_PAIS = tbcPfj.ID_PAIS;
                    pfj.OBS_ENDERECO = tbcPfj.OBS_ENDERECO;
                    pfj.DATA_ALTERACAO = DateTime.Now;

                    if (ValidaPreenchimentoEnderecoCompletoTBC(pfj))
                        context.SaveChanges();
                    else
                        throw new Exception("Endereço pfj não foi preenchido corretamente!");

                    context.SaveChanges();
                }
            }

            return pfj;
        }

        public static bool ValidaPreenchimentoEnderecoCompletoTBC(TBC_PFJ pfj)
        {
            if (!String.IsNullOrEmpty(pfj.LOGRADOURO)
                && !String.IsNullOrEmpty(pfj.NUMERO)
                && !String.IsNullOrEmpty(pfj.MUNICIPIO)
                && pfj.ID_MUNICIPIO != null
                && !String.IsNullOrEmpty(pfj.UF)
                && pfj.ID_UF != null
                && pfj.ID_PAIS != null
                && !String.IsNullOrEmpty(pfj.BAIRRO)
                )
                return true;
            else
                return false;
        }
    }
}
