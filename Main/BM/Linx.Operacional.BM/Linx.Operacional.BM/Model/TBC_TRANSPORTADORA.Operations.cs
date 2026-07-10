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
    public partial class TBC_TRANSPORTADORA
    {
        /// <summary>
        /// Cria, atualiza ou busca TbcTransportadora
        /// </summary>
        /// <param name="tbcFornecedor"></param>
        /// <returns></returns>
        public static TBC_TRANSPORTADORA ResolveTransportadora(TBC_TRANSPORTADORA tbcTransportadora)
        {
            if (tbcTransportadora.TBC_PFJ == null)
                throw new Exception("[TBC_PFJ] não encontrado no contexto de atualização. \n ***Crítica gerada por [Linx.Operacional.BM.TBC_FORNECEDOR.ResolveTransportadora]***");

            LinxOperacional context = new LinxOperacional();
            TBC_TRANSPORTADORA transportadora = context.TBC_TRANSPORTADORA.Include("TBC_PFJ").Where(r => r.TBC_PFJ.CNPJ_CPF == tbcTransportadora.TBC_PFJ.CNPJ_CPF && r.ID_TRANSPORTADORA != null).FirstOrDefault();

            if (transportadora.IsNull() && tbcTransportadora != null)
            {
                string mensagem = "";

                if (tbcTransportadora.ID_LINX.IsNullOrEmpty())
                    mensagem += "[ID_LINX] - Campo requerido não informado na origem \n";

                if (tbcTransportadora.TBC_PFJ.CNPJ_CPF.IsNullOrEmpty())
                    mensagem += "[CNPJ_CPF] - Campo requerido não informado na origem \n";

                if (tbcTransportadora.TBC_PFJ.NOME_FANTASIA_APELIDO.IsNullOrEmpty())
                    mensagem += "[NOME_FANTASIA_APELIDO] - Campo requerido não informado na origem \n";

                if (!mensagem.IsNullOrEmpty())
                    throw new Exception(mensagem + "***Crítica gerada por [Linx.Operacional.BM.TBC_FORNECEDOR.ResolveTransportadora]***");

                tbcTransportadora.TBC_PFJ.ID_LINX = tbcTransportadora.ID_LINX;
                tbcTransportadora.TBC_PFJ.CODIGO_PFJ = tbcTransportadora.TBC_PFJ.CODIGO_PFJ.IsNullOrEmpty() ? tbcTransportadora.TBC_PFJ.CNPJ_CPF : tbcTransportadora.TBC_PFJ.CODIGO_PFJ;
                tbcTransportadora.CODIGO_TRANSPORTADORA = tbcTransportadora.TBC_PFJ.CODIGO_PFJ.IsNullOrEmpty() ? String.Empty : tbcTransportadora.TBC_PFJ.CODIGO_PFJ;
                tbcTransportadora.NOME_TRANSPORTADORA = tbcTransportadora.TBC_PFJ.NOME_FANTASIA_APELIDO.IsNullOrEmpty() ? String.Empty : tbcTransportadora.TBC_PFJ.NOME_FANTASIA_APELIDO;
                tbcTransportadora.TBC_PFJ.RAZAO_SOCIAL_NOME_COMPLETO = tbcTransportadora.TBC_PFJ.RAZAO_SOCIAL_NOME_COMPLETO.IsNullOrEmpty() ? tbcTransportadora.NOME_TRANSPORTADORA : tbcTransportadora.TBC_PFJ.RAZAO_SOCIAL_NOME_COMPLETO;
                tbcTransportadora.TBC_PFJ.DATA_ALTERACAO = System.DateTime.Now;
                tbcTransportadora.TBC_PFJ.DATA_CADASTRO = System.DateTime.Now;
                tbcTransportadora.TBC_PFJ.INDICA_TRANSPORTADORA = true;
                tbcTransportadora.INATIVO = false;

                context.TBC_TRANSPORTADORA.Add(tbcTransportadora);
                context.SaveChanges();
                transportadora = tbcTransportadora;
            }
            else
            {
                if ((!String.IsNullOrEmpty(tbcTransportadora.TBC_PFJ.LOGRADOURO) && transportadora.TBC_PFJ.LOGRADOURO != tbcTransportadora.TBC_PFJ.LOGRADOURO)
                    || (!String.IsNullOrEmpty(tbcTransportadora.TBC_PFJ.NUMERO) && transportadora.TBC_PFJ.NUMERO != tbcTransportadora.TBC_PFJ.NUMERO))
                {

                    transportadora.TBC_PFJ.LOGRADOURO = !String.IsNullOrEmpty(tbcTransportadora.TBC_PFJ.LOGRADOURO) ? tbcTransportadora.TBC_PFJ.LOGRADOURO : null;
                    transportadora.TBC_PFJ.NUMERO = !String.IsNullOrEmpty(tbcTransportadora.TBC_PFJ.NUMERO) ? tbcTransportadora.TBC_PFJ.NUMERO : null;
                    transportadora.TBC_PFJ.COMPLEMENTO = !String.IsNullOrEmpty(tbcTransportadora.TBC_PFJ.COMPLEMENTO) ? tbcTransportadora.TBC_PFJ.COMPLEMENTO : null;
                    transportadora.TBC_PFJ.BAIRRO = !String.IsNullOrEmpty(tbcTransportadora.TBC_PFJ.BAIRRO) ? tbcTransportadora.TBC_PFJ.BAIRRO : null;
                    transportadora.TBC_PFJ.ID_MUNICIPIO = tbcTransportadora.TBC_PFJ.ID_MUNICIPIO;
                    transportadora.TBC_PFJ.MUNICIPIO = tbcTransportadora.TBC_PFJ.MUNICIPIO;
                    transportadora.TBC_PFJ.ID_UF = tbcTransportadora.TBC_PFJ.ID_UF;
                    transportadora.TBC_PFJ.UF = tbcTransportadora.TBC_PFJ.UF;
                    transportadora.TBC_PFJ.ID_CEP = tbcTransportadora.TBC_PFJ.ID_CEP;
                    transportadora.TBC_PFJ.CEP = tbcTransportadora.TBC_PFJ.CEP;
                    transportadora.TBC_PFJ.ID_PAIS = tbcTransportadora.TBC_PFJ.ID_PAIS;
                    transportadora.TBC_PFJ.OBS_ENDERECO = tbcTransportadora.TBC_PFJ.OBS_ENDERECO;
                    transportadora.TBC_PFJ.DATA_ALTERACAO = DateTime.Now;

                    if (TBC_PFJ.ValidaPreenchimentoEnderecoCompletoTBC(transportadora.TBC_PFJ))
                        context.SaveChanges();
                    else
                        throw new Exception("Endereço da transportadora não foi preenchido corretamente!");
                }
            }

            return transportadora;
        }
    }
}
