		

using System;
using System.IO;
using System.Configuration;
using System.Collections.Generic;
using Linx.Tools; 
    
namespace Linx.Dashboard.Domains
{

	public partial class DomainHelper
    {
		public static string[] GetDomainsInfo(string domainNames)
        {
            List<string> result = new List<string>();

            foreach (string domainName in domainNames.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                var values = GetDomainValues(domainName);
                if (values.Count > 0)
                {
                    foreach(var value in values)
                    {
                        result.Add(domainName + "#" + value.Key + "#" + value.Value.Replace("\"", "").Replace("'", ""));
                    }
                }
            }

            return result.ToArray();
        }

		public static Dictionary<string, Dictionary<string, string>> GetAllDomainsInfo()
        {
            Dictionary<string, Dictionary<string, string>> result = new Dictionary<string, Dictionary<string, string>>();
			Dictionary<string, string> values;
            values = GetDomainValues("LX_TIPO_CARTAO");
            if (values.Count > 0)
            {
            	result.Add("LX_TIPO_CARTAO", values);                    
            }
            values = GetDomainValues("LX_STATUS_ARTIGO");
            if (values.Count > 0)
            {
            	result.Add("LX_STATUS_ARTIGO", values);                    
            }
            values = GetDomainValues("LX_CODIGO_ITEM_FISCAL");
            if (values.Count > 0)
            {
            	result.Add("LX_CODIGO_ITEM_FISCAL", values);                    
            }
            values = GetDomainValues("LX_GRUPO_RELACAO_TIPO");
            if (values.Count > 0)
            {
            	result.Add("LX_GRUPO_RELACAO_TIPO", values);                    
            }
            values = GetDomainValues("LX_STATUS_LOJA");
            if (values.Count > 0)
            {
            	result.Add("LX_STATUS_LOJA", values);                    
            }
            values = GetDomainValues("LX_STATUS_ACAO");
            if (values.Count > 0)
            {
            	result.Add("LX_STATUS_ACAO", values);                    
            }
            values = GetDomainValues("LX_STATUS_COLETA");
            if (values.Count > 0)
            {
            	result.Add("LX_STATUS_COLETA", values);                    
            }
            values = GetDomainValues("LX_STATUS_COLETA_ITEM");
            if (values.Count > 0)
            {
            	result.Add("LX_STATUS_COLETA_ITEM", values);                    
            }
            values = GetDomainValues("dmvDias");
            if (values.Count > 0)
            {
            	result.Add("dmvDias", values);                    
            }
            values = GetDomainValues("dmvLxTipoCartao");
            if (values.Count > 0)
            {
            	result.Add("dmvLxTipoCartao", values);                    
            }
            values = GetDomainValues("GiftTipoRequisicao");
            if (values.Count > 0)
            {
            	result.Add("GiftTipoRequisicao", values);                    
            }
            values = GetDomainValues("ProvedorGiftCard");
            if (values.Count > 0)
            {
            	result.Add("ProvedorGiftCard", values);                    
            }
            values = GetDomainValues("DmvPjpf");
            if (values.Count > 0)
            {
            	result.Add("DmvPjpf", values);                    
            }
            values = GetDomainValues("LX_TABELA_ATRIBUTO");
            if (values.Count > 0)
            {
            	result.Add("LX_TABELA_ATRIBUTO", values);                    
            }
            values = GetDomainValues("LX_TIPO_LOGRADOURO");
            if (values.Count > 0)
            {
            	result.Add("LX_TIPO_LOGRADOURO", values);                    
            }
            values = GetDomainValues("LX_TIPO_TELEFONE");
            if (values.Count > 0)
            {
            	result.Add("LX_TIPO_TELEFONE", values);                    
            }
            values = GetDomainValues("LX_PFJ_FISICA_JURIDICA");
            if (values.Count > 0)
            {
            	result.Add("LX_PFJ_FISICA_JURIDICA", values);                    
            }
            values = GetDomainValues("LX_SEXO");
            if (values.Count > 0)
            {
            	result.Add("LX_SEXO", values);                    
            }
            values = GetDomainValues("LX_TIPO_PRECO");
            if (values.Count > 0)
            {
            	result.Add("LX_TIPO_PRECO", values);                    
            }
            values = GetDomainValues("LX_VIA_TRANSPORTE");
            if (values.Count > 0)
            {
            	result.Add("LX_VIA_TRANSPORTE", values);                    
            }
            values = GetDomainValues("LX_TIPO_END_ELETRONICO");
            if (values.Count > 0)
            {
            	result.Add("LX_TIPO_END_ELETRONICO", values);                    
            }
            values = GetDomainValues("LX_TIPO_ATENDIMENTO");
            if (values.Count > 0)
            {
            	result.Add("LX_TIPO_ATENDIMENTO", values);                    
            }
            values = GetDomainValues("LX_STATUS_REGISTRO");
            if (values.Count > 0)
            {
            	result.Add("LX_STATUS_REGISTRO", values);                    
            }
            values = GetDomainValues("LX_STATUS_OPERACAO");
            if (values.Count > 0)
            {
            	result.Add("LX_STATUS_OPERACAO", values);                    
            }
            values = GetDomainValues("LX_TIPO_ITEM");
            if (values.Count > 0)
            {
            	result.Add("LX_TIPO_ITEM", values);                    
            }
            values = GetDomainValues("LX_TIPO_RESGATE");
            if (values.Count > 0)
            {
            	result.Add("LX_TIPO_RESGATE", values);                    
            }
            values = GetDomainValues("LX_FILTRO_TIPO_LOJA");
            if (values.Count > 0)
            {
            	result.Add("LX_FILTRO_TIPO_LOJA", values);                    
            }
            values = GetDomainValues("LX_FILTRO_TIPO_OPERACAO");
            if (values.Count > 0)
            {
            	result.Add("LX_FILTRO_TIPO_OPERACAO", values);                    
            }
            values = GetDomainValues("LX_FILTRO_TIPO_PGTO");
            if (values.Count > 0)
            {
            	result.Add("LX_FILTRO_TIPO_PGTO", values);                    
            }
            values = GetDomainValues("LX_STATUS_PEDIDO");
            if (values.Count > 0)
            {
            	result.Add("LX_STATUS_PEDIDO", values);                    
            }
            values = GetDomainValues("LX_TIPO_PEDIDO");
            if (values.Count > 0)
            {
            	result.Add("LX_TIPO_PEDIDO", values);                    
            }
            values = GetDomainValues("LX_TIPO_LISTA");
            if (values.Count > 0)
            {
            	result.Add("LX_TIPO_LISTA", values);                    
            }
            values = GetDomainValues("LxTipoLogradouro");
            if (values.Count > 0)
            {
            	result.Add("LxTipoLogradouro", values);                    
            }
            values = GetDomainValues("LX_ESTADO_CIVIL");
            if (values.Count > 0)
            {
            	result.Add("LX_ESTADO_CIVIL", values);                    
            }
            values = GetDomainValues("LX_TIPO_VALIDACAO");
            if (values.Count > 0)
            {
            	result.Add("LX_TIPO_VALIDACAO", values);                    
            }
            values = GetDomainValues("LX_PRIORIDADE_PROMOCAO");
            if (values.Count > 0)
            {
            	result.Add("LX_PRIORIDADE_PROMOCAO", values);                    
            }
            values = GetDomainValues("LX_CODIGO_IDIOMA");
            if (values.Count > 0)
            {
            	result.Add("LX_CODIGO_IDIOMA", values);                    
            }
            values = GetDomainValues("LX_TIPO_OFERTA");
            if (values.Count > 0)
            {
            	result.Add("LX_TIPO_OFERTA", values);                    
            }
            values = GetDomainValues("LX_MODALIDADE_FRETE");
            if (values.Count > 0)
            {
            	result.Add("LX_MODALIDADE_FRETE", values);                    
            }
            values = GetDomainValues("LX_STATUS_LGE_PEDIDO");
            if (values.Count > 0)
            {
            	result.Add("LX_STATUS_LGE_PEDIDO", values);                    
            }
            values = GetDomainValues("LX_STATUS_ATENDIMENTO");
            if (values.Count > 0)
            {
            	result.Add("LX_STATUS_ATENDIMENTO", values);                    
            }
            values = GetDomainValues("LX_FATOR_STK_MOV");
            if (values.Count > 0)
            {
            	result.Add("LX_FATOR_STK_MOV", values);                    
            }
            values = GetDomainValues("LX_TIPO_OPERACAO");
            if (values.Count > 0)
            {
            	result.Add("LX_TIPO_OPERACAO", values);                    
            }
            values = GetDomainValues("LX_COND_PAGTO_STATUS");
            if (values.Count > 0)
            {
            	result.Add("LX_COND_PAGTO_STATUS", values);                    
            }
            values = GetDomainValues("LX_TIPO_COND_PAGTO");
            if (values.Count > 0)
            {
            	result.Add("LX_TIPO_COND_PAGTO", values);                    
            }
            values = GetDomainValues("LX_STATUS_NFE");
            if (values.Count > 0)
            {
            	result.Add("LX_STATUS_NFE", values);                    
            }
            values = GetDomainValues("LX_TIPO_PARCEIRO");
            if (values.Count > 0)
            {
            	result.Add("LX_TIPO_PARCEIRO", values);                    
            }
            values = GetDomainValues("LX_STATUS_INTEGRACAO_FISCAL");
            if (values.Count > 0)
            {
            	result.Add("LX_STATUS_INTEGRACAO_FISCAL", values);                    
            }
            values = GetDomainValues("LX_ORIGEM_DESCONTO");
            if (values.Count > 0)
            {
            	result.Add("LX_ORIGEM_DESCONTO", values);                    
            }
            values = GetDomainValues("LX_OPERADOR");
            if (values.Count > 0)
            {
            	result.Add("LX_OPERADOR", values);                    
            }
            values = GetDomainValues("LX_TIPO_CODIGO_BARRA");
            if (values.Count > 0)
            {
            	result.Add("LX_TIPO_CODIGO_BARRA", values);                    
            }
            values = GetDomainValues("LX_TIPO_FISCAL");
            if (values.Count > 0)
            {
            	result.Add("LX_TIPO_FISCAL", values);                    
            }
            values = GetDomainValues("LX_TIPO_ENDERECO");
            if (values.Count > 0)
            {
            	result.Add("LX_TIPO_ENDERECO", values);                    
            }
            values = GetDomainValues("LX_PEDIDO_ORIGEM");
            if (values.Count > 0)
            {
            	result.Add("LX_PEDIDO_ORIGEM", values);                    
            }
            values = GetDomainValues("LX_ATENDIMENTO_ORIGEM");
            if (values.Count > 0)
            {
            	result.Add("LX_ATENDIMENTO_ORIGEM", values);                    
            }
            values = GetDomainValues("TipoTransacao");
            if (values.Count > 0)
            {
            	result.Add("TipoTransacao", values);                    
            }
            values = GetDomainValues("RegraAcesso");
            if (values.Count > 0)
            {
            	result.Add("RegraAcesso", values);                    
            }
            values = GetDomainValues("RegraAcessoColuna");
            if (values.Count > 0)
            {
            	result.Add("RegraAcessoColuna", values);                    
            }
            values = GetDomainValues("TipoObjeto");
            if (values.Count > 0)
            {
            	result.Add("TipoObjeto", values);                    
            }
            values = GetDomainValues("TipoValidacaoParametro");
            if (values.Count > 0)
            {
            	result.Add("TipoValidacaoParametro", values);                    
            }
            values = GetDomainValues("TipoValorParametro");
            if (values.Count > 0)
            {
            	result.Add("TipoValorParametro", values);                    
            }
            values = GetDomainValues("TipoDocumento");
            if (values.Count > 0)
            {
            	result.Add("TipoDocumento", values);                    
            }
            values = GetDomainValues("TipoExtensao");
            if (values.Count > 0)
            {
            	result.Add("TipoExtensao", values);                    
            }
            values = GetDomainValues("TipoArquivo");
            if (values.Count > 0)
            {
            	result.Add("TipoArquivo", values);                    
            }
            values = GetDomainValues("TipoDado");
            if (values.Count > 0)
            {
            	result.Add("TipoDado", values);                    
            }
            values = GetDomainValues("TipoLog");
            if (values.Count > 0)
            {
            	result.Add("TipoLog", values);                    
            }
            values = GetDomainValues("FormatoData");
            if (values.Count > 0)
            {
            	result.Add("FormatoData", values);                    
            }
            values = GetDomainValues("TipoConteudoObjeto");
            if (values.Count > 0)
            {
            	result.Add("TipoConteudoObjeto", values);                    
            }
            values = GetDomainValues("TipoLayout");
            if (values.Count > 0)
            {
            	result.Add("TipoLayout", values);                    
            }
            values = GetDomainValues("PosicaoDaTransacao");
            if (values.Count > 0)
            {
            	result.Add("PosicaoDaTransacao", values);                    
            }
            values = GetDomainValues("TipoLayoutDependente");
            if (values.Count > 0)
            {
            	result.Add("TipoLayoutDependente", values);                    
            }
            values = GetDomainValues("IdAplicativo");
            if (values.Count > 0)
            {
            	result.Add("IdAplicativo", values);                    
            }
            values = GetDomainValues("UsoMultimidia");
            if (values.Count > 0)
            {
            	result.Add("UsoMultimidia", values);                    
            }
            values = GetDomainValues("TipoFiltro");
            if (values.Count > 0)
            {
            	result.Add("TipoFiltro", values);                    
            }
            values = GetDomainValues("FilterOperator");
            if (values.Count > 0)
            {
            	result.Add("FilterOperator", values);                    
            }
            values = GetDomainValues("FilterCondition");
            if (values.Count > 0)
            {
            	result.Add("FilterCondition", values);                    
            }
            values = GetDomainValues("TipoVerboHttp");
            if (values.Count > 0)
            {
            	result.Add("TipoVerboHttp", values);                    
            }
            values = GetDomainValues("TipoProcedimento");
            if (values.Count > 0)
            {
            	result.Add("TipoProcedimento", values);                    
            }
            values = GetDomainValues("OrigemValorParametro");
            if (values.Count > 0)
            {
            	result.Add("OrigemValorParametro", values);                    
            }
            values = GetDomainValues("LX_INDICADOR_MERCADORIA");
            if (values.Count > 0)
            {
            	result.Add("LX_INDICADOR_MERCADORIA", values);                    
            }
            values = GetDomainValues("LX_EMBALAGEM");
            if (values.Count > 0)
            {
            	result.Add("LX_EMBALAGEM", values);                    
            }
            values = GetDomainValues("LX_TIPO_MENSAGEM");
            if (values.Count > 0)
            {
            	result.Add("LX_TIPO_MENSAGEM", values);                    
            }
            values = GetDomainValues("LX_TIPO_CTRL_TIPO_PGTO");
            if (values.Count > 0)
            {
            	result.Add("LX_TIPO_CTRL_TIPO_PGTO", values);                    
            }
            values = GetDomainValues("LX_STATUS_CONFERENCIA_CTRL");
            if (values.Count > 0)
            {
            	result.Add("LX_STATUS_CONFERENCIA_CTRL", values);                    
            }
            values = GetDomainValues("LX_TIPO_CONFERENCIA");
            if (values.Count > 0)
            {
            	result.Add("LX_TIPO_CONFERENCIA", values);                    
            }
            values = GetDomainValues("LX_TIPO_OCORRENCIA");
            if (values.Count > 0)
            {
            	result.Add("LX_TIPO_OCORRENCIA", values);                    
            }
            values = GetDomainValues("LX_LOJA_TIPO_AGRUPAMENTO");
            if (values.Count > 0)
            {
            	result.Add("LX_LOJA_TIPO_AGRUPAMENTO", values);                    
            }
            values = GetDomainValues("LX_STATUS_CENARIO");
            if (values.Count > 0)
            {
            	result.Add("LX_STATUS_CENARIO", values);                    
            }
            values = GetDomainValues("ParametroHierarquia");
            if (values.Count > 0)
            {
            	result.Add("ParametroHierarquia", values);                    
            }
            values = GetDomainValues("LX_STATUS_PROCESSO");
            if (values.Count > 0)
            {
            	result.Add("LX_STATUS_PROCESSO", values);                    
            }
            values = GetDomainValues("LX_TIPO_REMARCACAO");
            if (values.Count > 0)
            {
            	result.Add("LX_TIPO_REMARCACAO", values);                    
            }
            values = GetDomainValues("LX_ORIGEM_MOVIMENTO");
            if (values.Count > 0)
            {
            	result.Add("LX_ORIGEM_MOVIMENTO", values);                    
            }
            values = GetDomainValues("LX_STATUS_REDUCAO");
            if (values.Count > 0)
            {
            	result.Add("LX_STATUS_REDUCAO", values);                    
            }
            values = GetDomainValues("LX_STATUS_REQUISICAO_ITEM");
            if (values.Count > 0)
            {
            	result.Add("LX_STATUS_REQUISICAO_ITEM", values);                    
            }
            values = GetDomainValues("LX_STATUS_REQUISICAO");
            if (values.Count > 0)
            {
            	result.Add("LX_STATUS_REQUISICAO", values);                    
            }
            values = GetDomainValues("LX_TIPO_AJUSTE");
            if (values.Count > 0)
            {
            	result.Add("LX_TIPO_AJUSTE", values);                    
            }
            values = GetDomainValues("LX_METODO_RECONTAGEM");
            if (values.Count > 0)
            {
            	result.Add("LX_METODO_RECONTAGEM", values);                    
            }
            values = GetDomainValues("LX_STATUS_INVENTARIO");
            if (values.Count > 0)
            {
            	result.Add("LX_STATUS_INVENTARIO", values);                    
            }
            values = GetDomainValues("LX_STATUS_INVENTARIO_SETOR");
            if (values.Count > 0)
            {
            	result.Add("LX_STATUS_INVENTARIO_SETOR", values);                    
            }
            values = GetDomainValues("LX_STATUS_CONFERENCIA");
            if (values.Count > 0)
            {
            	result.Add("LX_STATUS_CONFERENCIA", values);                    
            }
            values = GetDomainValues("LX_STATUS_CONFRONTO");
            if (values.Count > 0)
            {
            	result.Add("LX_STATUS_CONFRONTO", values);                    
            }
            values = GetDomainValues("LX_STATUS_ROMANEIO");
            if (values.Count > 0)
            {
            	result.Add("LX_STATUS_ROMANEIO", values);                    
            }
            values = GetDomainValues("LX_TIPO_EMISSAO");
            if (values.Count > 0)
            {
            	result.Add("LX_TIPO_EMISSAO", values);                    
            }
            values = GetDomainValues("LX_OCORRENCIA_CUSTO");
            if (values.Count > 0)
            {
            	result.Add("LX_OCORRENCIA_CUSTO", values);                    
            }
            values = GetDomainValues("LX_INDICADOR_PRESENCA");
            if (values.Count > 0)
            {
            	result.Add("LX_INDICADOR_PRESENCA", values);                    
            }
            values = GetDomainValues("LX_PROPRIEDADE_STK");
            if (values.Count > 0)
            {
            	result.Add("LX_PROPRIEDADE_STK", values);                    
            }
            values = GetDomainValues("LX_LOJA_SORTIMENTO_METODO");
            if (values.Count > 0)
            {
            	result.Add("LX_LOJA_SORTIMENTO_METODO", values);                    
            }
            values = GetDomainValues("LX_STATUS_GERACAO_COMPRA");
            if (values.Count > 0)
            {
            	result.Add("LX_STATUS_GERACAO_COMPRA", values);                    
            }
            values = GetDomainValues("LX_STATUS_NF_DOC_FISCAL");
            if (values.Count > 0)
            {
            	result.Add("LX_STATUS_NF_DOC_FISCAL", values);                    
            }
            values = GetDomainValues("LX_TIPO_VALOR_ATENDIMENTO_ATRIBUTO");
            if (values.Count > 0)
            {
            	result.Add("LX_TIPO_VALOR_ATENDIMENTO_ATRIBUTO", values);                    
            }
            values = GetDomainValues("LX_ORIGEM_ATENDIMENTO");
            if (values.Count > 0)
            {
            	result.Add("LX_ORIGEM_ATENDIMENTO", values);                    
            }
            values = GetDomainValues("LX_STATUS_COMISSAO_PERIODO");
            if (values.Count > 0)
            {
            	result.Add("LX_STATUS_COMISSAO_PERIODO", values);                    
            }
            values = GetDomainValues("LX_COMISSAO_PROCESSO_TIPO");
            if (values.Count > 0)
            {
            	result.Add("LX_COMISSAO_PROCESSO_TIPO", values);                    
            }
            values = GetDomainValues("LX_FUNCAO_VENDEDOR");
            if (values.Count > 0)
            {
            	result.Add("LX_FUNCAO_VENDEDOR", values);                    
            }
            values = GetDomainValues("LX_TIPO_COMISSAO");
            if (values.Count > 0)
            {
            	result.Add("LX_TIPO_COMISSAO", values);                    
            }
            values = GetDomainValues("LX_TIPO_NF_RELACAO");
            if (values.Count > 0)
            {
            	result.Add("LX_TIPO_NF_RELACAO", values);                    
            }
            return result;
        }

        public static Dictionary<string, string> GetDomainValues(string domainName)
        {
            Dictionary<string, string> result;
            switch (domainName)
            {


                case "LX_TIPO_CARTAO":
                    result = LX_TIPO_CARTAO.GetValues();
                    break;

                case "LX_STATUS_ARTIGO":
                    result = LX_STATUS_ARTIGO.GetValues();
                    break;

                case "LX_CODIGO_ITEM_FISCAL":
                    result = LX_CODIGO_ITEM_FISCAL.GetValues();
                    break;

                case "LX_GRUPO_RELACAO_TIPO":
                    result = LX_GRUPO_RELACAO_TIPO.GetValues();
                    break;

                case "LX_STATUS_LOJA":
                    result = LX_STATUS_LOJA.GetValues();
                    break;

                case "LX_STATUS_ACAO":
                    result = LX_STATUS_ACAO.GetValues();
                    break;

                case "LX_STATUS_COLETA":
                    result = LX_STATUS_COLETA.GetValues();
                    break;

                case "LX_STATUS_COLETA_ITEM":
                    result = LX_STATUS_COLETA_ITEM.GetValues();
                    break;

                case "dmvDias":
                    result = dmvDias.GetValues();
                    break;

                case "dmvLxTipoCartao":
                    result = dmvLxTipoCartao.GetValues();
                    break;

                case "GiftTipoRequisicao":
                    result = GiftTipoRequisicao.GetValues();
                    break;

                case "ProvedorGiftCard":
                    result = ProvedorGiftCard.GetValues();
                    break;

                case "DmvPjpf":
                    result = DmvPjpf.GetValues();
                    break;

                case "LX_TABELA_ATRIBUTO":
                    result = LX_TABELA_ATRIBUTO.GetValues();
                    break;

                case "LX_TIPO_LOGRADOURO":
                    result = LX_TIPO_LOGRADOURO.GetValues();
                    break;

                case "LX_TIPO_TELEFONE":
                    result = LX_TIPO_TELEFONE.GetValues();
                    break;

                case "LX_PFJ_FISICA_JURIDICA":
                    result = LX_PFJ_FISICA_JURIDICA.GetValues();
                    break;

                case "LX_SEXO":
                    result = LX_SEXO.GetValues();
                    break;

                case "LX_TIPO_PRECO":
                    result = LX_TIPO_PRECO.GetValues();
                    break;

                case "LX_VIA_TRANSPORTE":
                    result = LX_VIA_TRANSPORTE.GetValues();
                    break;

                case "LX_TIPO_END_ELETRONICO":
                    result = LX_TIPO_END_ELETRONICO.GetValues();
                    break;

                case "LX_TIPO_ATENDIMENTO":
                    result = LX_TIPO_ATENDIMENTO.GetValues();
                    break;

                case "LX_STATUS_REGISTRO":
                    result = LX_STATUS_REGISTRO.GetValues();
                    break;

                case "LX_STATUS_OPERACAO":
                    result = LX_STATUS_OPERACAO.GetValues();
                    break;

                case "LX_TIPO_ITEM":
                    result = LX_TIPO_ITEM.GetValues();
                    break;

                case "LX_TIPO_RESGATE":
                    result = LX_TIPO_RESGATE.GetValues();
                    break;

                case "LX_FILTRO_TIPO_LOJA":
                    result = LX_FILTRO_TIPO_LOJA.GetValues();
                    break;

                case "LX_FILTRO_TIPO_OPERACAO":
                    result = LX_FILTRO_TIPO_OPERACAO.GetValues();
                    break;

                case "LX_FILTRO_TIPO_PGTO":
                    result = LX_FILTRO_TIPO_PGTO.GetValues();
                    break;

                case "LX_STATUS_PEDIDO":
                    result = LX_STATUS_PEDIDO.GetValues();
                    break;

                case "LX_TIPO_PEDIDO":
                    result = LX_TIPO_PEDIDO.GetValues();
                    break;

                case "LX_TIPO_LISTA":
                    result = LX_TIPO_LISTA.GetValues();
                    break;

                case "LxTipoLogradouro":
                    result = LxTipoLogradouro.GetValues();
                    break;

                case "LX_ESTADO_CIVIL":
                    result = LX_ESTADO_CIVIL.GetValues();
                    break;

                case "LX_TIPO_VALIDACAO":
                    result = LX_TIPO_VALIDACAO.GetValues();
                    break;

                case "LX_PRIORIDADE_PROMOCAO":
                    result = LX_PRIORIDADE_PROMOCAO.GetValues();
                    break;

                case "LX_CODIGO_IDIOMA":
                    result = LX_CODIGO_IDIOMA.GetValues();
                    break;

                case "LX_TIPO_OFERTA":
                    result = LX_TIPO_OFERTA.GetValues();
                    break;

                case "LX_MODALIDADE_FRETE":
                    result = LX_MODALIDADE_FRETE.GetValues();
                    break;

                case "LX_STATUS_LGE_PEDIDO":
                    result = LX_STATUS_LGE_PEDIDO.GetValues();
                    break;

                case "LX_STATUS_ATENDIMENTO":
                    result = LX_STATUS_ATENDIMENTO.GetValues();
                    break;

                case "LX_FATOR_STK_MOV":
                    result = LX_FATOR_STK_MOV.GetValues();
                    break;

                case "LX_TIPO_OPERACAO":
                    result = LX_TIPO_OPERACAO.GetValues();
                    break;

                case "LX_COND_PAGTO_STATUS":
                    result = LX_COND_PAGTO_STATUS.GetValues();
                    break;

                case "LX_TIPO_COND_PAGTO":
                    result = LX_TIPO_COND_PAGTO.GetValues();
                    break;

                case "LX_STATUS_NFE":
                    result = LX_STATUS_NFE.GetValues();
                    break;

                case "LX_TIPO_PARCEIRO":
                    result = LX_TIPO_PARCEIRO.GetValues();
                    break;

                case "LX_STATUS_INTEGRACAO_FISCAL":
                    result = LX_STATUS_INTEGRACAO_FISCAL.GetValues();
                    break;

                case "LX_ORIGEM_DESCONTO":
                    result = LX_ORIGEM_DESCONTO.GetValues();
                    break;

                case "LX_OPERADOR":
                    result = LX_OPERADOR.GetValues();
                    break;

                case "LX_TIPO_CODIGO_BARRA":
                    result = LX_TIPO_CODIGO_BARRA.GetValues();
                    break;

                case "LX_TIPO_FISCAL":
                    result = LX_TIPO_FISCAL.GetValues();
                    break;

                case "LX_TIPO_ENDERECO":
                    result = LX_TIPO_ENDERECO.GetValues();
                    break;

                case "LX_PEDIDO_ORIGEM":
                    result = LX_PEDIDO_ORIGEM.GetValues();
                    break;

                case "LX_ATENDIMENTO_ORIGEM":
                    result = LX_ATENDIMENTO_ORIGEM.GetValues();
                    break;

                case "TipoTransacao":
                    result = TipoTransacao.GetValues();
                    break;

                case "RegraAcesso":
                    result = RegraAcesso.GetValues();
                    break;

                case "RegraAcessoColuna":
                    result = RegraAcessoColuna.GetValues();
                    break;

                case "TipoObjeto":
                    result = TipoObjeto.GetValues();
                    break;

                case "TipoValidacaoParametro":
                    result = TipoValidacaoParametro.GetValues();
                    break;

                case "TipoValorParametro":
                    result = TipoValorParametro.GetValues();
                    break;

                case "TipoDocumento":
                    result = TipoDocumento.GetValues();
                    break;

                case "TipoExtensao":
                    result = TipoExtensao.GetValues();
                    break;

                case "TipoArquivo":
                    result = TipoArquivo.GetValues();
                    break;

                case "TipoDado":
                    result = TipoDado.GetValues();
                    break;

                case "TipoLog":
                    result = TipoLog.GetValues();
                    break;

                case "FormatoData":
                    result = FormatoData.GetValues();
                    break;

                case "TipoConteudoObjeto":
                    result = TipoConteudoObjeto.GetValues();
                    break;

                case "TipoLayout":
                    result = TipoLayout.GetValues();
                    break;

                case "PosicaoDaTransacao":
                    result = PosicaoDaTransacao.GetValues();
                    break;

                case "TipoLayoutDependente":
                    result = TipoLayoutDependente.GetValues();
                    break;

                case "IdAplicativo":
                    result = IdAplicativo.GetValues();
                    break;

                case "UsoMultimidia":
                    result = UsoMultimidia.GetValues();
                    break;

                case "TipoFiltro":
                    result = TipoFiltro.GetValues();
                    break;

                case "FilterOperator":
                    result = FilterOperator.GetValues();
                    break;

                case "FilterCondition":
                    result = FilterCondition.GetValues();
                    break;

                case "TipoVerboHttp":
                    result = TipoVerboHttp.GetValues();
                    break;

                case "TipoProcedimento":
                    result = TipoProcedimento.GetValues();
                    break;

                case "OrigemValorParametro":
                    result = OrigemValorParametro.GetValues();
                    break;

                case "LX_INDICADOR_MERCADORIA":
                    result = LX_INDICADOR_MERCADORIA.GetValues();
                    break;

                case "LX_EMBALAGEM":
                    result = LX_EMBALAGEM.GetValues();
                    break;

                case "LX_TIPO_MENSAGEM":
                    result = LX_TIPO_MENSAGEM.GetValues();
                    break;

                case "LX_TIPO_CTRL_TIPO_PGTO":
                    result = LX_TIPO_CTRL_TIPO_PGTO.GetValues();
                    break;

                case "LX_STATUS_CONFERENCIA_CTRL":
                    result = LX_STATUS_CONFERENCIA_CTRL.GetValues();
                    break;

                case "LX_TIPO_CONFERENCIA":
                    result = LX_TIPO_CONFERENCIA.GetValues();
                    break;

                case "LX_TIPO_OCORRENCIA":
                    result = LX_TIPO_OCORRENCIA.GetValues();
                    break;

                case "LX_LOJA_TIPO_AGRUPAMENTO":
                    result = LX_LOJA_TIPO_AGRUPAMENTO.GetValues();
                    break;

                case "LX_STATUS_CENARIO":
                    result = LX_STATUS_CENARIO.GetValues();
                    break;

                case "ParametroHierarquia":
                    result = ParametroHierarquia.GetValues();
                    break;

                case "LX_STATUS_PROCESSO":
                    result = LX_STATUS_PROCESSO.GetValues();
                    break;

                case "LX_TIPO_REMARCACAO":
                    result = LX_TIPO_REMARCACAO.GetValues();
                    break;

                case "LX_ORIGEM_MOVIMENTO":
                    result = LX_ORIGEM_MOVIMENTO.GetValues();
                    break;

                case "LX_STATUS_REDUCAO":
                    result = LX_STATUS_REDUCAO.GetValues();
                    break;

                case "LX_STATUS_REQUISICAO_ITEM":
                    result = LX_STATUS_REQUISICAO_ITEM.GetValues();
                    break;

                case "LX_STATUS_REQUISICAO":
                    result = LX_STATUS_REQUISICAO.GetValues();
                    break;

                case "LX_TIPO_AJUSTE":
                    result = LX_TIPO_AJUSTE.GetValues();
                    break;

                case "LX_METODO_RECONTAGEM":
                    result = LX_METODO_RECONTAGEM.GetValues();
                    break;

                case "LX_STATUS_INVENTARIO":
                    result = LX_STATUS_INVENTARIO.GetValues();
                    break;

                case "LX_STATUS_INVENTARIO_SETOR":
                    result = LX_STATUS_INVENTARIO_SETOR.GetValues();
                    break;

                case "LX_STATUS_CONFERENCIA":
                    result = LX_STATUS_CONFERENCIA.GetValues();
                    break;

                case "LX_STATUS_CONFRONTO":
                    result = LX_STATUS_CONFRONTO.GetValues();
                    break;

                case "LX_STATUS_ROMANEIO":
                    result = LX_STATUS_ROMANEIO.GetValues();
                    break;

                case "LX_TIPO_EMISSAO":
                    result = LX_TIPO_EMISSAO.GetValues();
                    break;

                case "LX_OCORRENCIA_CUSTO":
                    result = LX_OCORRENCIA_CUSTO.GetValues();
                    break;

                case "LX_INDICADOR_PRESENCA":
                    result = LX_INDICADOR_PRESENCA.GetValues();
                    break;

                case "LX_PROPRIEDADE_STK":
                    result = LX_PROPRIEDADE_STK.GetValues();
                    break;

                case "LX_LOJA_SORTIMENTO_METODO":
                    result = LX_LOJA_SORTIMENTO_METODO.GetValues();
                    break;

                case "LX_STATUS_GERACAO_COMPRA":
                    result = LX_STATUS_GERACAO_COMPRA.GetValues();
                    break;

                case "LX_STATUS_NF_DOC_FISCAL":
                    result = LX_STATUS_NF_DOC_FISCAL.GetValues();
                    break;

                case "LX_TIPO_VALOR_ATENDIMENTO_ATRIBUTO":
                    result = LX_TIPO_VALOR_ATENDIMENTO_ATRIBUTO.GetValues();
                    break;

                case "LX_ORIGEM_ATENDIMENTO":
                    result = LX_ORIGEM_ATENDIMENTO.GetValues();
                    break;

                case "LX_STATUS_COMISSAO_PERIODO":
                    result = LX_STATUS_COMISSAO_PERIODO.GetValues();
                    break;

                case "LX_COMISSAO_PROCESSO_TIPO":
                    result = LX_COMISSAO_PROCESSO_TIPO.GetValues();
                    break;

                case "LX_FUNCAO_VENDEDOR":
                    result = LX_FUNCAO_VENDEDOR.GetValues();
                    break;

                case "LX_TIPO_COMISSAO":
                    result = LX_TIPO_COMISSAO.GetValues();
                    break;

                case "LX_TIPO_NF_RELACAO":
                    result = LX_TIPO_NF_RELACAO.GetValues();
                    break;

                default:
                    result = new Dictionary<string, string>();
                    break;
            }

            return result;
        }
    }

	//<LX_TIPO_CARTAO>((#LxExpr#) == [-1-] ? "Cartão de Crédito" : ((#LxExpr#) == [-2-] ? "Cartão de Débito" : ""))</LX_TIPO_CARTAO>	
    public partial class LX_TIPO_CARTAO
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Cartão de Crédito"); 
				    
					result.Add("2", "Cartão de Débito"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "CartaoCredito"); 
				    
					result.Add("2", "CartaoDebito"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _CartaoCredito = new DomainKeyPair() { Value = "1", DisplayName = "Cartão de Crédito" };
					[FunctionalPoint("Value[1];DisplayName[Cartão de Crédito]")]
					public static DomainKeyPair CartaoCredito { get { return _CartaoCredito; } }
				    
					private static DomainKeyPair _CartaoDebito = new DomainKeyPair() { Value = "2", DisplayName = "Cartão de Débito" };
					[FunctionalPoint("Value[2];DisplayName[Cartão de Débito]")]
					public static DomainKeyPair CartaoDebito { get { return _CartaoDebito; } }
				    
			#endregion properties

			

	}    
	//<LX_STATUS_ARTIGO>((#LxExpr#) == [-3-] ? "Aguardando Liberação" : ((#LxExpr#) == [-2-] ? "Em Desenvolvimento" : ((#LxExpr#) == [-1-] ? "Inativo" : ((#LxExpr#) == [-4-] ? "Liberado" : ""))))</LX_STATUS_ARTIGO>	
    public partial class LX_STATUS_ARTIGO
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("3", "Aguardando Liberação"); 
				    
					result.Add("2", "Em Desenvolvimento"); 
				    
					result.Add("1", "Inativo"); 
				    
					result.Add("4", "Liberado"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("3", "AguardandoLiberacao"); 
				    
					result.Add("2", "EmDesenvolvimento"); 
				    
					result.Add("1", "Inativo"); 
				    
					result.Add("4", "Liberado"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _AguardandoLiberacao = new DomainKeyPair() { Value = "3", DisplayName = "Aguardando Liberação" };
					[FunctionalPoint("Value[3];DisplayName[Aguardando Liberação]")]
					public static DomainKeyPair AguardandoLiberacao { get { return _AguardandoLiberacao; } }
				    
					private static DomainKeyPair _EmDesenvolvimento = new DomainKeyPair() { Value = "2", DisplayName = "Em Desenvolvimento" };
					[FunctionalPoint("Value[2];DisplayName[Em Desenvolvimento]")]
					public static DomainKeyPair EmDesenvolvimento { get { return _EmDesenvolvimento; } }
				    
					private static DomainKeyPair _Inativo = new DomainKeyPair() { Value = "1", DisplayName = "Inativo" };
					[FunctionalPoint("Value[1];DisplayName[Inativo]")]
					public static DomainKeyPair Inativo { get { return _Inativo; } }
				    
					private static DomainKeyPair _Liberado = new DomainKeyPair() { Value = "4", DisplayName = "Liberado" };
					[FunctionalPoint("Value[4];DisplayName[Liberado]")]
					public static DomainKeyPair Liberado { get { return _Liberado; } }
				    
			#endregion properties

			

	}    
	//<LX_CODIGO_ITEM_FISCAL>((#LxExpr#) == [-1-] ? "Por Artigo" : ((#LxExpr#) == [-3-] ? "Por GTIN" : ((#LxExpr#) == [-2-] ? "Por Sku" : "")))</LX_CODIGO_ITEM_FISCAL>	
    public partial class LX_CODIGO_ITEM_FISCAL
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Por Artigo"); 
				    
					result.Add("3", "Por GTIN"); 
				    
					result.Add("2", "Por Sku"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Artigo"); 
				    
					result.Add("3", "GTIN"); 
				    
					result.Add("2", "Sku"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Artigo = new DomainKeyPair() { Value = "1", DisplayName = "Por Artigo" };
					[FunctionalPoint("Value[1];DisplayName[Por Artigo]")]
					public static DomainKeyPair Artigo { get { return _Artigo; } }
				    
					private static DomainKeyPair _GTIN = new DomainKeyPair() { Value = "3", DisplayName = "Por GTIN" };
					[FunctionalPoint("Value[3];DisplayName[Por GTIN]")]
					public static DomainKeyPair GTIN { get { return _GTIN; } }
				    
					private static DomainKeyPair _Sku = new DomainKeyPair() { Value = "2", DisplayName = "Por Sku" };
					[FunctionalPoint("Value[2];DisplayName[Por Sku]")]
					public static DomainKeyPair Sku { get { return _Sku; } }
				    
			#endregion properties

			

	}    
	//<LX_GRUPO_RELACAO_TIPO>((#LxExpr#) == [-45-] ? "Comercial Compra" : ((#LxExpr#) == [-40-] ? "Comercial Venda" : ((#LxExpr#) == [-20-] ? "Relação Empresarial" : ((#LxExpr#) == [-25-] ? "Relação Financeira" : ((#LxExpr#) == [-15-] ? "Relação Funcional" : ((#LxExpr#) == [-30-] ? "Relação Logística " : ((#LxExpr#) == [-10-] ? "Relação Pessoal" : ((#LxExpr#) == [-35-] ? "Relação Transporte" : ""))))))))</LX_GRUPO_RELACAO_TIPO>	
    public partial class LX_GRUPO_RELACAO_TIPO
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("45", "Comercial Compra"); 
				    
					result.Add("40", "Comercial Venda"); 
				    
					result.Add("20", "Relação Empresarial"); 
				    
					result.Add("25", "Relação Financeira"); 
				    
					result.Add("15", "Relação Funcional"); 
				    
					result.Add("30", "Relação Logística "); 
				    
					result.Add("10", "Relação Pessoal"); 
				    
					result.Add("35", "Relação Transporte"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("45", "ComercialEntrada"); 
				    
					result.Add("40", "ComercialSaida"); 
				    
					result.Add("20", "Empresarial"); 
				    
					result.Add("25", "Financeira"); 
				    
					result.Add("15", "Funcional"); 
				    
					result.Add("30", "Logistica"); 
				    
					result.Add("10", "Pessoal"); 
				    
					result.Add("35", "Transportador"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _ComercialEntrada = new DomainKeyPair() { Value = "45", DisplayName = "Comercial Compra" };
					[FunctionalPoint("Value[45];DisplayName[Comercial Compra]")]
					public static DomainKeyPair ComercialEntrada { get { return _ComercialEntrada; } }
				    
					private static DomainKeyPair _ComercialSaida = new DomainKeyPair() { Value = "40", DisplayName = "Comercial Venda" };
					[FunctionalPoint("Value[40];DisplayName[Comercial Venda]")]
					public static DomainKeyPair ComercialSaida { get { return _ComercialSaida; } }
				    
					private static DomainKeyPair _Empresarial = new DomainKeyPair() { Value = "20", DisplayName = "Relação Empresarial" };
					[FunctionalPoint("Value[20];DisplayName[Relação Empresarial]")]
					public static DomainKeyPair Empresarial { get { return _Empresarial; } }
				    
					private static DomainKeyPair _Financeira = new DomainKeyPair() { Value = "25", DisplayName = "Relação Financeira" };
					[FunctionalPoint("Value[25];DisplayName[Relação Financeira]")]
					public static DomainKeyPair Financeira { get { return _Financeira; } }
				    
					private static DomainKeyPair _Funcional = new DomainKeyPair() { Value = "15", DisplayName = "Relação Funcional" };
					[FunctionalPoint("Value[15];DisplayName[Relação Funcional]")]
					public static DomainKeyPair Funcional { get { return _Funcional; } }
				    
					private static DomainKeyPair _Logistica = new DomainKeyPair() { Value = "30", DisplayName = "Relação Logística " };
					[FunctionalPoint("Value[30];DisplayName[Relação Logística ]")]
					public static DomainKeyPair Logistica { get { return _Logistica; } }
				    
					private static DomainKeyPair _Pessoal = new DomainKeyPair() { Value = "10", DisplayName = "Relação Pessoal" };
					[FunctionalPoint("Value[10];DisplayName[Relação Pessoal]")]
					public static DomainKeyPair Pessoal { get { return _Pessoal; } }
				    
					private static DomainKeyPair _Transportador = new DomainKeyPair() { Value = "35", DisplayName = "Relação Transporte" };
					[FunctionalPoint("Value[35];DisplayName[Relação Transporte]")]
					public static DomainKeyPair Transportador { get { return _Transportador; } }
				    
			#endregion properties

			

	}    
	//<LX_STATUS_LOJA>((#LxExpr#) == [-1-] ? "Em fase de Abertura" : ((#LxExpr#) == [-3-] ? "Fechamento Temporário" : ((#LxExpr#) == [-2-] ? "Loja Aberta" : ((#LxExpr#) == [-4-] ? "Loja Fechada" : ""))))</LX_STATUS_LOJA>	
    public partial class LX_STATUS_LOJA
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Em fase de Abertura"); 
				    
					result.Add("3", "Fechamento Temporário"); 
				    
					result.Add("2", "Loja Aberta"); 
				    
					result.Add("4", "Loja Fechada"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "FaseAbertura"); 
				    
					result.Add("3", "FechamentoTemporario"); 
				    
					result.Add("2", "LojaAberta"); 
				    
					result.Add("4", "LojaFechada"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _FaseAbertura = new DomainKeyPair() { Value = "1", DisplayName = "Em fase de Abertura" };
					[FunctionalPoint("Value[1];DisplayName[Em fase de Abertura]")]
					public static DomainKeyPair FaseAbertura { get { return _FaseAbertura; } }
				    
					private static DomainKeyPair _FechamentoTemporario = new DomainKeyPair() { Value = "3", DisplayName = "Fechamento Temporário" };
					[FunctionalPoint("Value[3];DisplayName[Fechamento Temporário]")]
					public static DomainKeyPair FechamentoTemporario { get { return _FechamentoTemporario; } }
				    
					private static DomainKeyPair _LojaAberta = new DomainKeyPair() { Value = "2", DisplayName = "Loja Aberta" };
					[FunctionalPoint("Value[2];DisplayName[Loja Aberta]")]
					public static DomainKeyPair LojaAberta { get { return _LojaAberta; } }
				    
					private static DomainKeyPair _LojaFechada = new DomainKeyPair() { Value = "4", DisplayName = "Loja Fechada" };
					[FunctionalPoint("Value[4];DisplayName[Loja Fechada]")]
					public static DomainKeyPair LojaFechada { get { return _LojaFechada; } }
				    
			#endregion properties

			

	}    
	//<LX_STATUS_ACAO>((#LxExpr#) == [-2-] ? "Aguardando Data de Execução" : ((#LxExpr#) == [-5-] ? "Cancelada" : ((#LxExpr#) == [-3-] ? "Em Execução" : ((#LxExpr#) == [-1-] ? "Em Planejamento" : ((#LxExpr#) == [-4-] ? "Encerrada" : "")))))</LX_STATUS_ACAO>	
    public partial class LX_STATUS_ACAO
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "Aguardando Data de Execução"); 
				    
					result.Add("5", "Cancelada"); 
				    
					result.Add("3", "Em Execução"); 
				    
					result.Add("1", "Em Planejamento"); 
				    
					result.Add("4", "Encerrada"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "Aguardando_Data_Execucao"); 
				    
					result.Add("5", "Cancelada"); 
				    
					result.Add("3", "Em_Execucao"); 
				    
					result.Add("1", "Em_Planejamento"); 
				    
					result.Add("4", "Encerrada"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Aguardando_Data_Execucao = new DomainKeyPair() { Value = "2", DisplayName = "Aguardando Data de Execução" };
					[FunctionalPoint("Value[2];DisplayName[Aguardando Data de Execução]")]
					public static DomainKeyPair Aguardando_Data_Execucao { get { return _Aguardando_Data_Execucao; } }
				    
					private static DomainKeyPair _Cancelada = new DomainKeyPair() { Value = "5", DisplayName = "Cancelada" };
					[FunctionalPoint("Value[5];DisplayName[Cancelada]")]
					public static DomainKeyPair Cancelada { get { return _Cancelada; } }
				    
					private static DomainKeyPair _Em_Execucao = new DomainKeyPair() { Value = "3", DisplayName = "Em Execução" };
					[FunctionalPoint("Value[3];DisplayName[Em Execução]")]
					public static DomainKeyPair Em_Execucao { get { return _Em_Execucao; } }
				    
					private static DomainKeyPair _Em_Planejamento = new DomainKeyPair() { Value = "1", DisplayName = "Em Planejamento" };
					[FunctionalPoint("Value[1];DisplayName[Em Planejamento]")]
					public static DomainKeyPair Em_Planejamento { get { return _Em_Planejamento; } }
				    
					private static DomainKeyPair _Encerrada = new DomainKeyPair() { Value = "4", DisplayName = "Encerrada" };
					[FunctionalPoint("Value[4];DisplayName[Encerrada]")]
					public static DomainKeyPair Encerrada { get { return _Encerrada; } }
				    
			#endregion properties

			

	}    
	//<LX_STATUS_COLETA>((#LxExpr#) == [-4-] ? "Coleta Eleita" : ((#LxExpr#) == [-2-] ? "Coleta Finalizada" : ((#LxExpr#) == [-1-] ? "Coleta em Andamento" : "")))</LX_STATUS_COLETA>	
    public partial class LX_STATUS_COLETA
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("4", "Coleta Eleita"); 
				    
					result.Add("2", "Coleta Finalizada"); 
				    
					result.Add("1", "Coleta em Andamento"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("4", "ColetaEleita"); 
				    
					result.Add("2", "ColetaFinalizada"); 
				    
					result.Add("1", "EmAndamento"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _ColetaEleita = new DomainKeyPair() { Value = "4", DisplayName = "Coleta Eleita" };
					[FunctionalPoint("Value[4];DisplayName[Coleta Eleita]")]
					public static DomainKeyPair ColetaEleita { get { return _ColetaEleita; } }
				    
					private static DomainKeyPair _ColetaFinalizada = new DomainKeyPair() { Value = "2", DisplayName = "Coleta Finalizada" };
					[FunctionalPoint("Value[2];DisplayName[Coleta Finalizada]")]
					public static DomainKeyPair ColetaFinalizada { get { return _ColetaFinalizada; } }
				    
					private static DomainKeyPair _EmAndamento = new DomainKeyPair() { Value = "1", DisplayName = "Coleta em Andamento" };
					[FunctionalPoint("Value[1];DisplayName[Coleta em Andamento]")]
					public static DomainKeyPair EmAndamento { get { return _EmAndamento; } }
				    
			#endregion properties

			

	}    
	//<LX_STATUS_COLETA_ITEM>((#LxExpr#) == [-1-] ? "Coleta em Andamento" : ((#LxExpr#) == [-2-] ? "Coleta Finalizada" : ((#LxExpr#) == [-9-] ? "Coleta Processada" : ((#LxExpr#) == [-3-] ? "Coleta Rejeitada" : ""))))</LX_STATUS_COLETA_ITEM>	
    public partial class LX_STATUS_COLETA_ITEM
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Coleta em Andamento"); 
				    
					result.Add("2", "Coleta Finalizada"); 
				    
					result.Add("9", "Coleta Processada"); 
				    
					result.Add("3", "Coleta Rejeitada"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "AguardandoComparacao"); 
				    
					result.Add("2", "ColetaEleita"); 
				    
					result.Add("9", "ColetaProcessada"); 
				    
					result.Add("3", "ColetaRejeitada"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _AguardandoComparacao = new DomainKeyPair() { Value = "1", DisplayName = "Coleta em Andamento" };
					[FunctionalPoint("Value[1];DisplayName[Coleta em Andamento]")]
					public static DomainKeyPair AguardandoComparacao { get { return _AguardandoComparacao; } }
				    
					private static DomainKeyPair _ColetaEleita = new DomainKeyPair() { Value = "2", DisplayName = "Coleta Finalizada" };
					[FunctionalPoint("Value[2];DisplayName[Coleta Finalizada]")]
					public static DomainKeyPair ColetaEleita { get { return _ColetaEleita; } }
				    
					private static DomainKeyPair _ColetaProcessada = new DomainKeyPair() { Value = "9", DisplayName = "Coleta Processada" };
					[FunctionalPoint("Value[9];DisplayName[Coleta Processada]")]
					public static DomainKeyPair ColetaProcessada { get { return _ColetaProcessada; } }
				    
					private static DomainKeyPair _ColetaRejeitada = new DomainKeyPair() { Value = "3", DisplayName = "Coleta Rejeitada" };
					[FunctionalPoint("Value[3];DisplayName[Coleta Rejeitada]")]
					public static DomainKeyPair ColetaRejeitada { get { return _ColetaRejeitada; } }
				    
			#endregion properties

			

	}    
	//<dmvDias>((#LxExpr#) == [-1-] ? "Domingo" : ((#LxExpr#) == [-8-] ? "Quarta" : ((#LxExpr#) == [-16-] ? "Quinta" : ((#LxExpr#) == [-64-] ? "Sábado" : ((#LxExpr#) == [-2-] ? "Segunda" : ((#LxExpr#) == [-32-] ? "Sexta" : ((#LxExpr#) == [-4-] ? "Terça" : "")))))))</dmvDias>	
    public partial class dmvDias
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Domingo"); 
				    
					result.Add("8", "Quarta"); 
				    
					result.Add("16", "Quinta"); 
				    
					result.Add("64", "Sábado"); 
				    
					result.Add("2", "Segunda"); 
				    
					result.Add("32", "Sexta"); 
				    
					result.Add("4", "Terça"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Domingo"); 
				    
					result.Add("8", "Quarta"); 
				    
					result.Add("16", "Quinta"); 
				    
					result.Add("64", "Sabado"); 
				    
					result.Add("2", "Segunda"); 
				    
					result.Add("32", "Sexta"); 
				    
					result.Add("4", "Terca"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Domingo = new DomainKeyPair() { Value = "1", DisplayName = "Domingo" };
					[FunctionalPoint("Value[1];DisplayName[Domingo]")]
					public static DomainKeyPair Domingo { get { return _Domingo; } }
				    
					private static DomainKeyPair _Quarta = new DomainKeyPair() { Value = "8", DisplayName = "Quarta" };
					[FunctionalPoint("Value[8];DisplayName[Quarta]")]
					public static DomainKeyPair Quarta { get { return _Quarta; } }
				    
					private static DomainKeyPair _Quinta = new DomainKeyPair() { Value = "16", DisplayName = "Quinta" };
					[FunctionalPoint("Value[16];DisplayName[Quinta]")]
					public static DomainKeyPair Quinta { get { return _Quinta; } }
				    
					private static DomainKeyPair _Sabado = new DomainKeyPair() { Value = "64", DisplayName = "Sábado" };
					[FunctionalPoint("Value[64];DisplayName[Sábado]")]
					public static DomainKeyPair Sabado { get { return _Sabado; } }
				    
					private static DomainKeyPair _Segunda = new DomainKeyPair() { Value = "2", DisplayName = "Segunda" };
					[FunctionalPoint("Value[2];DisplayName[Segunda]")]
					public static DomainKeyPair Segunda { get { return _Segunda; } }
				    
					private static DomainKeyPair _Sexta = new DomainKeyPair() { Value = "32", DisplayName = "Sexta" };
					[FunctionalPoint("Value[32];DisplayName[Sexta]")]
					public static DomainKeyPair Sexta { get { return _Sexta; } }
				    
					private static DomainKeyPair _Terca = new DomainKeyPair() { Value = "4", DisplayName = "Terça" };
					[FunctionalPoint("Value[4];DisplayName[Terça]")]
					public static DomainKeyPair Terca { get { return _Terca; } }
				    
			#endregion properties

			

	}    
	//<dmvLxTipoCartao>((#LxExpr#) == [-1-] ? "Cartão de Crédito" : ((#LxExpr#) == [-2-] ? "Cartão de débito" : ""))</dmvLxTipoCartao>	
    public partial class dmvLxTipoCartao
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Cartão de Crédito"); 
				    
					result.Add("2", "Cartão de débito"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Cartaodecredito"); 
				    
					result.Add("2", "Cartaodedebito"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Cartaodecredito = new DomainKeyPair() { Value = "1", DisplayName = "Cartão de Crédito" };
					[FunctionalPoint("Value[1];DisplayName[Cartão de Crédito]")]
					public static DomainKeyPair Cartaodecredito { get { return _Cartaodecredito; } }
				    
					private static DomainKeyPair _Cartaodedebito = new DomainKeyPair() { Value = "2", DisplayName = "Cartão de débito" };
					[FunctionalPoint("Value[2];DisplayName[Cartão de débito]")]
					public static DomainKeyPair Cartaodedebito { get { return _Cartaodedebito; } }
				    
			#endregion properties

			

	}    
	//<GiftTipoRequisicao>((#LxExpr#) == [-2-] ? "Carga" : ((#LxExpr#) == [-4-] ? "Confirmacao" : ((#LxExpr#) == [-5-] ? "Desfazimento" : ((#LxExpr#) == [-6-] ? "Estorno" : ((#LxExpr#) == [-3-] ? "Resgate" : ((#LxExpr#) == [-1-] ? "Saldo" : ""))))))</GiftTipoRequisicao>	
    public partial class GiftTipoRequisicao
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "Carga"); 
				    
					result.Add("4", "Confirmacao"); 
				    
					result.Add("5", "Desfazimento"); 
				    
					result.Add("6", "Estorno"); 
				    
					result.Add("3", "Resgate"); 
				    
					result.Add("1", "Saldo"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "Carga"); 
				    
					result.Add("4", "Confirmacao"); 
				    
					result.Add("5", "Desfazimento"); 
				    
					result.Add("6", "Estorno"); 
				    
					result.Add("3", "Resgate"); 
				    
					result.Add("1", "Saldo"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Carga = new DomainKeyPair() { Value = "2", DisplayName = "Carga" };
					[FunctionalPoint("Value[2];DisplayName[Carga]")]
					public static DomainKeyPair Carga { get { return _Carga; } }
				    
					private static DomainKeyPair _Confirmacao = new DomainKeyPair() { Value = "4", DisplayName = "Confirmacao" };
					[FunctionalPoint("Value[4];DisplayName[Confirmacao]")]
					public static DomainKeyPair Confirmacao { get { return _Confirmacao; } }
				    
					private static DomainKeyPair _Desfazimento = new DomainKeyPair() { Value = "5", DisplayName = "Desfazimento" };
					[FunctionalPoint("Value[5];DisplayName[Desfazimento]")]
					public static DomainKeyPair Desfazimento { get { return _Desfazimento; } }
				    
					private static DomainKeyPair _Estorno = new DomainKeyPair() { Value = "6", DisplayName = "Estorno" };
					[FunctionalPoint("Value[6];DisplayName[Estorno]")]
					public static DomainKeyPair Estorno { get { return _Estorno; } }
				    
					private static DomainKeyPair _Resgate = new DomainKeyPair() { Value = "3", DisplayName = "Resgate" };
					[FunctionalPoint("Value[3];DisplayName[Resgate]")]
					public static DomainKeyPair Resgate { get { return _Resgate; } }
				    
					private static DomainKeyPair _Saldo = new DomainKeyPair() { Value = "1", DisplayName = "Saldo" };
					[FunctionalPoint("Value[1];DisplayName[Saldo]")]
					public static DomainKeyPair Saldo { get { return _Saldo; } }
				    
			#endregion properties

			

	}    
	//<ProvedorGiftCard>((#LxExpr#) == [-1-] ? "Peela" : ((#LxExpr#) == [-2-] ? "Unik" : ""))</ProvedorGiftCard>	
    public partial class ProvedorGiftCard
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Peela"); 
				    
					result.Add("2", "Unik"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Peela"); 
				    
					result.Add("2", "Unik"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Peela = new DomainKeyPair() { Value = "1", DisplayName = "Peela" };
					[FunctionalPoint("Value[1];DisplayName[Peela]")]
					public static DomainKeyPair Peela { get { return _Peela; } }
				    
					private static DomainKeyPair _Unik = new DomainKeyPair() { Value = "2", DisplayName = "Unik" };
					[FunctionalPoint("Value[2];DisplayName[Unik]")]
					public static DomainKeyPair Unik { get { return _Unik; } }
				    
			#endregion properties

			

	}    
	//<DmvPjpf>((#LxExpr#) == [-0-] ? "Pessoa Física" : ((#LxExpr#) == [-1-] ? "Pesoa Jurídica" : ""))</DmvPjpf>	
    public partial class DmvPjpf
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("0", "Pessoa Física"); 
				    
					result.Add("1", "Pesoa Jurídica"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("0", "PF"); 
				    
					result.Add("1", "PJ"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _PF = new DomainKeyPair() { Value = "0", DisplayName = "Pessoa Física" };
					[FunctionalPoint("Value[0];DisplayName[Pessoa Física]")]
					public static DomainKeyPair PF { get { return _PF; } }
				    
					private static DomainKeyPair _PJ = new DomainKeyPair() { Value = "1", DisplayName = "Pesoa Jurídica" };
					[FunctionalPoint("Value[1];DisplayName[Pesoa Jurídica]")]
					public static DomainKeyPair PJ { get { return _PJ; } }
				    
			#endregion properties

			

	}    
	//<LX_TABELA_ATRIBUTO>((#LxExpr#) == [-1-] ? "Artigo" : ((#LxExpr#) == [-2-] ? "Cliente" : ((#LxExpr#) == [-4-] ? "LOJA" : ((#LxExpr#) == [-3-] ? "Variante Sku" : ""))))</LX_TABELA_ATRIBUTO>	
    public partial class LX_TABELA_ATRIBUTO
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Artigo"); 
				    
					result.Add("2", "Cliente"); 
				    
					result.Add("4", "LOJA"); 
				    
					result.Add("3", "Variante Sku"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "ARTIGO"); 
				    
					result.Add("2", "CLIENTE"); 
				    
					result.Add("4", "LOJA"); 
				    
					result.Add("3", "VARIANTE_SKU"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _ARTIGO = new DomainKeyPair() { Value = "1", DisplayName = "Artigo" };
					[FunctionalPoint("Value[1];DisplayName[Artigo]")]
					public static DomainKeyPair ARTIGO { get { return _ARTIGO; } }
				    
					private static DomainKeyPair _CLIENTE = new DomainKeyPair() { Value = "2", DisplayName = "Cliente" };
					[FunctionalPoint("Value[2];DisplayName[Cliente]")]
					public static DomainKeyPair CLIENTE { get { return _CLIENTE; } }
				    
					private static DomainKeyPair _LOJA = new DomainKeyPair() { Value = "4", DisplayName = "LOJA" };
					[FunctionalPoint("Value[4];DisplayName[LOJA]")]
					public static DomainKeyPair LOJA { get { return _LOJA; } }
				    
					private static DomainKeyPair _VARIANTE_SKU = new DomainKeyPair() { Value = "3", DisplayName = "Variante Sku" };
					[FunctionalPoint("Value[3];DisplayName[Variante Sku]")]
					public static DomainKeyPair VARIANTE_SKU { get { return _VARIANTE_SKU; } }
				    
			#endregion properties

			

	}    
	//<LX_TIPO_LOGRADOURO>((#LxExpr#) == [-2-] ? "Avenida" : ((#LxExpr#) == [-1-] ? "Rua" : ""))</LX_TIPO_LOGRADOURO>	
    public partial class LX_TIPO_LOGRADOURO
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "Avenida"); 
				    
					result.Add("1", "Rua"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "AVENIDA"); 
				    
					result.Add("1", "RUA"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _AVENIDA = new DomainKeyPair() { Value = "2", DisplayName = "Avenida" };
					[FunctionalPoint("Value[2];DisplayName[Avenida]")]
					public static DomainKeyPair AVENIDA { get { return _AVENIDA; } }
				    
					private static DomainKeyPair _RUA = new DomainKeyPair() { Value = "1", DisplayName = "Rua" };
					[FunctionalPoint("Value[1];DisplayName[Rua]")]
					public static DomainKeyPair RUA { get { return _RUA; } }
				    
			#endregion properties

			

	}    
	//<LX_TIPO_TELEFONE>((#LxExpr#) == [-3-] ? "Celular" : ((#LxExpr#) == [-2-] ? "Comercial" : ((#LxExpr#) == [-1-] ? "Residencial" : "")))</LX_TIPO_TELEFONE>	
    public partial class LX_TIPO_TELEFONE
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("3", "Celular"); 
				    
					result.Add("2", "Comercial"); 
				    
					result.Add("1", "Residencial"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("3", "CELULAR"); 
				    
					result.Add("2", "COMERCIAL"); 
				    
					result.Add("1", "RESIDENCIAL"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _CELULAR = new DomainKeyPair() { Value = "3", DisplayName = "Celular" };
					[FunctionalPoint("Value[3];DisplayName[Celular]")]
					public static DomainKeyPair CELULAR { get { return _CELULAR; } }
				    
					private static DomainKeyPair _COMERCIAL = new DomainKeyPair() { Value = "2", DisplayName = "Comercial" };
					[FunctionalPoint("Value[2];DisplayName[Comercial]")]
					public static DomainKeyPair COMERCIAL { get { return _COMERCIAL; } }
				    
					private static DomainKeyPair _RESIDENCIAL = new DomainKeyPair() { Value = "1", DisplayName = "Residencial" };
					[FunctionalPoint("Value[1];DisplayName[Residencial]")]
					public static DomainKeyPair RESIDENCIAL { get { return _RESIDENCIAL; } }
				    
			#endregion properties

			

	}    
	//<LX_PFJ_FISICA_JURIDICA>((#LxExpr#) == [-1-] ? "Pessoa Física" : ((#LxExpr#) == [-2-] ? "Pessoa Jurídica" : ""))</LX_PFJ_FISICA_JURIDICA>	
    public partial class LX_PFJ_FISICA_JURIDICA
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Pessoa Física"); 
				    
					result.Add("2", "Pessoa Jurídica"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "FISICA"); 
				    
					result.Add("2", "JURIDICA"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _FISICA = new DomainKeyPair() { Value = "1", DisplayName = "Pessoa Física" };
					[FunctionalPoint("Value[1];DisplayName[Pessoa Física]")]
					public static DomainKeyPair FISICA { get { return _FISICA; } }
				    
					private static DomainKeyPair _JURIDICA = new DomainKeyPair() { Value = "2", DisplayName = "Pessoa Jurídica" };
					[FunctionalPoint("Value[2];DisplayName[Pessoa Jurídica]")]
					public static DomainKeyPair JURIDICA { get { return _JURIDICA; } }
				    
			#endregion properties

			

	}    
	//<LX_SEXO>((#LxExpr#) == [-2-] ? "Feminino" : ((#LxExpr#) == [-1-] ? "Masculino" : ((#LxExpr#) == [-3-] ? "Não Informado" : "")))</LX_SEXO>	
    public partial class LX_SEXO
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "Feminino"); 
				    
					result.Add("1", "Masculino"); 
				    
					result.Add("3", "Não Informado"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "Feminino"); 
				    
					result.Add("1", "Masculino"); 
				    
					result.Add("3", "NaoInformado"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Feminino = new DomainKeyPair() { Value = "2", DisplayName = "Feminino" };
					[FunctionalPoint("Value[2];DisplayName[Feminino]")]
					public static DomainKeyPair Feminino { get { return _Feminino; } }
				    
					private static DomainKeyPair _Masculino = new DomainKeyPair() { Value = "1", DisplayName = "Masculino" };
					[FunctionalPoint("Value[1];DisplayName[Masculino]")]
					public static DomainKeyPair Masculino { get { return _Masculino; } }
				    
					private static DomainKeyPair _NaoInformado = new DomainKeyPair() { Value = "3", DisplayName = "Não Informado" };
					[FunctionalPoint("Value[3];DisplayName[Não Informado]")]
					public static DomainKeyPair NaoInformado { get { return _NaoInformado; } }
				    
			#endregion properties

			

	}    
	//<LX_TIPO_PRECO>((#LxExpr#) == [-5-] ? "Concorrência" : ((#LxExpr#) == [-2-] ? "Custo Médio" : ((#LxExpr#) == [-1-] ? "Custo Reposição" : ((#LxExpr#) == [-4-] ? "Preço de Venda Atacado" : ((#LxExpr#) == [-3-] ? "Preço de Venda Varejo" : "")))))</LX_TIPO_PRECO>	
    public partial class LX_TIPO_PRECO
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("5", "Concorrência"); 
				    
					result.Add("2", "Custo Médio"); 
				    
					result.Add("1", "Custo Reposição"); 
				    
					result.Add("4", "Preço de Venda Atacado"); 
				    
					result.Add("3", "Preço de Venda Varejo"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("5", "Concorrencia"); 
				    
					result.Add("2", "Custo_Medio"); 
				    
					result.Add("1", "Custo_Reposicao"); 
				    
					result.Add("4", "Preco_Venda_Atacado"); 
				    
					result.Add("3", "Preco_Venda_Varejo"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Concorrencia = new DomainKeyPair() { Value = "5", DisplayName = "Concorrência" };
					[FunctionalPoint("Value[5];DisplayName[Concorrência]")]
					public static DomainKeyPair Concorrencia { get { return _Concorrencia; } }
				    
					private static DomainKeyPair _Custo_Medio = new DomainKeyPair() { Value = "2", DisplayName = "Custo Médio" };
					[FunctionalPoint("Value[2];DisplayName[Custo Médio]")]
					public static DomainKeyPair Custo_Medio { get { return _Custo_Medio; } }
				    
					private static DomainKeyPair _Custo_Reposicao = new DomainKeyPair() { Value = "1", DisplayName = "Custo Reposição" };
					[FunctionalPoint("Value[1];DisplayName[Custo Reposição]")]
					public static DomainKeyPair Custo_Reposicao { get { return _Custo_Reposicao; } }
				    
					private static DomainKeyPair _Preco_Venda_Atacado = new DomainKeyPair() { Value = "4", DisplayName = "Preço de Venda Atacado" };
					[FunctionalPoint("Value[4];DisplayName[Preço de Venda Atacado]")]
					public static DomainKeyPair Preco_Venda_Atacado { get { return _Preco_Venda_Atacado; } }
				    
					private static DomainKeyPair _Preco_Venda_Varejo = new DomainKeyPair() { Value = "3", DisplayName = "Preço de Venda Varejo" };
					[FunctionalPoint("Value[3];DisplayName[Preço de Venda Varejo]")]
					public static DomainKeyPair Preco_Venda_Varejo { get { return _Preco_Venda_Varejo; } }
				    
			#endregion properties

			

	}    
	//<LX_VIA_TRANSPORTE>((#LxExpr#) == [-4-] ? "Aérea" : ((#LxExpr#) == [-8-] ? "Conduto rede transmissão" : ((#LxExpr#) == [-11-] ? "Courier" : ((#LxExpr#) == [-10-] ? "Entrada e Saida ficta" : ((#LxExpr#) == [-6-] ? "Ferroviária" : ((#LxExpr#) == [-2-] ? "Fluvial" : ((#LxExpr#) == [-12-] ? "Hand Carry" : ((#LxExpr#) == [-3-] ? "Lacustre" : ((#LxExpr#) == [-1-] ? "Marítima" : ((#LxExpr#) == [-9-] ? "Meios Próprios" : ((#LxExpr#) == [-5-] ? "Postal" : ((#LxExpr#) == [-7-] ? "Rodoviária" : ""))))))))))))</LX_VIA_TRANSPORTE>	
    public partial class LX_VIA_TRANSPORTE
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("4", "Aérea"); 
				    
					result.Add("8", "Conduto rede transmissão"); 
				    
					result.Add("11", "Courier"); 
				    
					result.Add("10", "Entrada e Saida ficta"); 
				    
					result.Add("6", "Ferroviária"); 
				    
					result.Add("2", "Fluvial"); 
				    
					result.Add("12", "Hand Carry"); 
				    
					result.Add("3", "Lacustre"); 
				    
					result.Add("1", "Marítima"); 
				    
					result.Add("9", "Meios Próprios"); 
				    
					result.Add("5", "Postal"); 
				    
					result.Add("7", "Rodoviária"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("4", "Aerea"); 
				    
					result.Add("8", "Conduto_rede_transmissao"); 
				    
					result.Add("11", "Courier"); 
				    
					result.Add("10", "EntradaSaidaficta"); 
				    
					result.Add("6", "Ferroviaria"); 
				    
					result.Add("2", "Fluvial"); 
				    
					result.Add("12", "HandCarry"); 
				    
					result.Add("3", "Lacustre"); 
				    
					result.Add("1", "Maritima"); 
				    
					result.Add("9", "MeiosProprios"); 
				    
					result.Add("5", "Postal"); 
				    
					result.Add("7", "Rodoviaria"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Aerea = new DomainKeyPair() { Value = "4", DisplayName = "Aérea" };
					[FunctionalPoint("Value[4];DisplayName[Aérea]")]
					public static DomainKeyPair Aerea { get { return _Aerea; } }
				    
					private static DomainKeyPair _Conduto_rede_transmissao = new DomainKeyPair() { Value = "8", DisplayName = "Conduto rede transmissão" };
					[FunctionalPoint("Value[8];DisplayName[Conduto rede transmissão]")]
					public static DomainKeyPair Conduto_rede_transmissao { get { return _Conduto_rede_transmissao; } }
				    
					private static DomainKeyPair _Courier = new DomainKeyPair() { Value = "11", DisplayName = "Courier" };
					[FunctionalPoint("Value[11];DisplayName[Courier]")]
					public static DomainKeyPair Courier { get { return _Courier; } }
				    
					private static DomainKeyPair _EntradaSaidaficta = new DomainKeyPair() { Value = "10", DisplayName = "Entrada e Saida ficta" };
					[FunctionalPoint("Value[10];DisplayName[Entrada e Saida ficta]")]
					public static DomainKeyPair EntradaSaidaficta { get { return _EntradaSaidaficta; } }
				    
					private static DomainKeyPair _Ferroviaria = new DomainKeyPair() { Value = "6", DisplayName = "Ferroviária" };
					[FunctionalPoint("Value[6];DisplayName[Ferroviária]")]
					public static DomainKeyPair Ferroviaria { get { return _Ferroviaria; } }
				    
					private static DomainKeyPair _Fluvial = new DomainKeyPair() { Value = "2", DisplayName = "Fluvial" };
					[FunctionalPoint("Value[2];DisplayName[Fluvial]")]
					public static DomainKeyPair Fluvial { get { return _Fluvial; } }
				    
					private static DomainKeyPair _HandCarry = new DomainKeyPair() { Value = "12", DisplayName = "Hand Carry" };
					[FunctionalPoint("Value[12];DisplayName[Hand Carry]")]
					public static DomainKeyPair HandCarry { get { return _HandCarry; } }
				    
					private static DomainKeyPair _Lacustre = new DomainKeyPair() { Value = "3", DisplayName = "Lacustre" };
					[FunctionalPoint("Value[3];DisplayName[Lacustre]")]
					public static DomainKeyPair Lacustre { get { return _Lacustre; } }
				    
					private static DomainKeyPair _Maritima = new DomainKeyPair() { Value = "1", DisplayName = "Marítima" };
					[FunctionalPoint("Value[1];DisplayName[Marítima]")]
					public static DomainKeyPair Maritima { get { return _Maritima; } }
				    
					private static DomainKeyPair _MeiosProprios = new DomainKeyPair() { Value = "9", DisplayName = "Meios Próprios" };
					[FunctionalPoint("Value[9];DisplayName[Meios Próprios]")]
					public static DomainKeyPair MeiosProprios { get { return _MeiosProprios; } }
				    
					private static DomainKeyPair _Postal = new DomainKeyPair() { Value = "5", DisplayName = "Postal" };
					[FunctionalPoint("Value[5];DisplayName[Postal]")]
					public static DomainKeyPair Postal { get { return _Postal; } }
				    
					private static DomainKeyPair _Rodoviaria = new DomainKeyPair() { Value = "7", DisplayName = "Rodoviária" };
					[FunctionalPoint("Value[7];DisplayName[Rodoviária]")]
					public static DomainKeyPair Rodoviaria { get { return _Rodoviaria; } }
				    
			#endregion properties

			

	}    
	//<LX_TIPO_END_ELETRONICO>((#LxExpr#) == [-1-] ? "E-mail" : ((#LxExpr#) == [-3-] ? "FaceBook" : ((#LxExpr#) == [-4-] ? "LinkedIn" : ((#LxExpr#) == [-2-] ? "Site" : ((#LxExpr#) == [-5-] ? "Skype" : "")))))</LX_TIPO_END_ELETRONICO>	
    public partial class LX_TIPO_END_ELETRONICO
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "E-mail"); 
				    
					result.Add("3", "FaceBook"); 
				    
					result.Add("4", "LinkedIn"); 
				    
					result.Add("2", "Site"); 
				    
					result.Add("5", "Skype"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "EMAIL"); 
				    
					result.Add("3", "FACEBOOK"); 
				    
					result.Add("4", "LINKEDIN"); 
				    
					result.Add("2", "SITE"); 
				    
					result.Add("5", "SKYPE"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _EMAIL = new DomainKeyPair() { Value = "1", DisplayName = "E-mail" };
					[FunctionalPoint("Value[1];DisplayName[E-mail]")]
					public static DomainKeyPair EMAIL { get { return _EMAIL; } }
				    
					private static DomainKeyPair _FACEBOOK = new DomainKeyPair() { Value = "3", DisplayName = "FaceBook" };
					[FunctionalPoint("Value[3];DisplayName[FaceBook]")]
					public static DomainKeyPair FACEBOOK { get { return _FACEBOOK; } }
				    
					private static DomainKeyPair _LINKEDIN = new DomainKeyPair() { Value = "4", DisplayName = "LinkedIn" };
					[FunctionalPoint("Value[4];DisplayName[LinkedIn]")]
					public static DomainKeyPair LINKEDIN { get { return _LINKEDIN; } }
				    
					private static DomainKeyPair _SITE = new DomainKeyPair() { Value = "2", DisplayName = "Site" };
					[FunctionalPoint("Value[2];DisplayName[Site]")]
					public static DomainKeyPair SITE { get { return _SITE; } }
				    
					private static DomainKeyPair _SKYPE = new DomainKeyPair() { Value = "5", DisplayName = "Skype" };
					[FunctionalPoint("Value[5];DisplayName[Skype]")]
					public static DomainKeyPair SKYPE { get { return _SKYPE; } }
				    
			#endregion properties

			

	}    
	//<LX_TIPO_ATENDIMENTO>((#LxExpr#) == [-2-] ? "Devolução" : ((#LxExpr#) == [-1-] ? "Venda" : ""))</LX_TIPO_ATENDIMENTO>	
    public partial class LX_TIPO_ATENDIMENTO
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "Devolução"); 
				    
					result.Add("1", "Venda"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "Devolucao"); 
				    
					result.Add("1", "Venda"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Devolucao = new DomainKeyPair() { Value = "2", DisplayName = "Devolução" };
					[FunctionalPoint("Value[2];DisplayName[Devolução]")]
					public static DomainKeyPair Devolucao { get { return _Devolucao; } }
				    
					private static DomainKeyPair _Venda = new DomainKeyPair() { Value = "1", DisplayName = "Venda" };
					[FunctionalPoint("Value[1];DisplayName[Venda]")]
					public static DomainKeyPair Venda { get { return _Venda; } }
				    
			#endregion properties

			

	}    
	//<LX_STATUS_REGISTRO>((#LxExpr#) == [-3-] ? "Enviado" : ((#LxExpr#) == [-2-] ? "Enviando" : ((#LxExpr#) == [-1-] ? "Pendente" : "")))</LX_STATUS_REGISTRO>	
    public partial class LX_STATUS_REGISTRO
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("3", "Enviado"); 
				    
					result.Add("2", "Enviando"); 
				    
					result.Add("1", "Pendente"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("3", "Enviado"); 
				    
					result.Add("2", "Enviando"); 
				    
					result.Add("1", "Pendente"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Enviado = new DomainKeyPair() { Value = "3", DisplayName = "Enviado" };
					[FunctionalPoint("Value[3];DisplayName[Enviado]")]
					public static DomainKeyPair Enviado { get { return _Enviado; } }
				    
					private static DomainKeyPair _Enviando = new DomainKeyPair() { Value = "2", DisplayName = "Enviando" };
					[FunctionalPoint("Value[2];DisplayName[Enviando]")]
					public static DomainKeyPair Enviando { get { return _Enviando; } }
				    
					private static DomainKeyPair _Pendente = new DomainKeyPair() { Value = "1", DisplayName = "Pendente" };
					[FunctionalPoint("Value[1];DisplayName[Pendente]")]
					public static DomainKeyPair Pendente { get { return _Pendente; } }
				    
			#endregion properties

			

	}    
	//<LX_STATUS_OPERACAO>((#LxExpr#) == [-1-] ? "Loja Aberta" : ((#LxExpr#) == [-3-] ? "Loja Fechada" : ((#LxExpr#) == [-4-] ? "Movimento Integrado" : ((#LxExpr#) == [-2-] ? "Venda Encerrada" : ""))))</LX_STATUS_OPERACAO>	
    public partial class LX_STATUS_OPERACAO
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Loja Aberta"); 
				    
					result.Add("3", "Loja Fechada"); 
				    
					result.Add("4", "Movimento Integrado"); 
				    
					result.Add("2", "Venda Encerrada"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "LojaAberta"); 
				    
					result.Add("3", "LojaFechada"); 
				    
					result.Add("4", "MovimentoIntegrado"); 
				    
					result.Add("2", "VendaEncerrada"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _LojaAberta = new DomainKeyPair() { Value = "1", DisplayName = "Loja Aberta" };
					[FunctionalPoint("Value[1];DisplayName[Loja Aberta]")]
					public static DomainKeyPair LojaAberta { get { return _LojaAberta; } }
				    
					private static DomainKeyPair _LojaFechada = new DomainKeyPair() { Value = "3", DisplayName = "Loja Fechada" };
					[FunctionalPoint("Value[3];DisplayName[Loja Fechada]")]
					public static DomainKeyPair LojaFechada { get { return _LojaFechada; } }
				    
					private static DomainKeyPair _MovimentoIntegrado = new DomainKeyPair() { Value = "4", DisplayName = "Movimento Integrado" };
					[FunctionalPoint("Value[4];DisplayName[Movimento Integrado]")]
					public static DomainKeyPair MovimentoIntegrado { get { return _MovimentoIntegrado; } }
				    
					private static DomainKeyPair _VendaEncerrada = new DomainKeyPair() { Value = "2", DisplayName = "Venda Encerrada" };
					[FunctionalPoint("Value[2];DisplayName[Venda Encerrada]")]
					public static DomainKeyPair VendaEncerrada { get { return _VendaEncerrada; } }
				    
			#endregion properties

			

	}    
	//<LX_TIPO_ITEM>((#LxExpr#) == [-6-] ? "Correspondente Bancário" : ((#LxExpr#) == [-1-] ? "Mercadoria" : ((#LxExpr#) == [-7-] ? "Outros Recebimentos Financeiros" : ((#LxExpr#) == [-3-] ? "Pedido" : ((#LxExpr#) == [-5-] ? "Recarga Celular" : ((#LxExpr#) == [-2-] ? "Serviço" : ((#LxExpr#) == [-4-] ? "Vale Presente" : "")))))))</LX_TIPO_ITEM>	
    public partial class LX_TIPO_ITEM
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("6", "Correspondente Bancário"); 
				    
					result.Add("1", "Mercadoria"); 
				    
					result.Add("7", "Outros Recebimentos Financeiros"); 
				    
					result.Add("3", "Pedido"); 
				    
					result.Add("5", "Recarga Celular"); 
				    
					result.Add("2", "Serviço"); 
				    
					result.Add("4", "Vale Presente"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("6", "CorrespondenteBancario"); 
				    
					result.Add("1", "Mercadoria"); 
				    
					result.Add("7", "OutrosRecebimentosFinanceiros"); 
				    
					result.Add("3", "Pedido"); 
				    
					result.Add("5", "RecargaCelular"); 
				    
					result.Add("2", "Servico"); 
				    
					result.Add("4", "ValePresente"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _CorrespondenteBancario = new DomainKeyPair() { Value = "6", DisplayName = "Correspondente Bancário" };
					[FunctionalPoint("Value[6];DisplayName[Correspondente Bancário]")]
					public static DomainKeyPair CorrespondenteBancario { get { return _CorrespondenteBancario; } }
				    
					private static DomainKeyPair _Mercadoria = new DomainKeyPair() { Value = "1", DisplayName = "Mercadoria" };
					[FunctionalPoint("Value[1];DisplayName[Mercadoria]")]
					public static DomainKeyPair Mercadoria { get { return _Mercadoria; } }
				    
					private static DomainKeyPair _OutrosRecebimentosFinanceiros = new DomainKeyPair() { Value = "7", DisplayName = "Outros Recebimentos Financeiros" };
					[FunctionalPoint("Value[7];DisplayName[Outros Recebimentos Financeiros]")]
					public static DomainKeyPair OutrosRecebimentosFinanceiros { get { return _OutrosRecebimentosFinanceiros; } }
				    
					private static DomainKeyPair _Pedido = new DomainKeyPair() { Value = "3", DisplayName = "Pedido" };
					[FunctionalPoint("Value[3];DisplayName[Pedido]")]
					public static DomainKeyPair Pedido { get { return _Pedido; } }
				    
					private static DomainKeyPair _RecargaCelular = new DomainKeyPair() { Value = "5", DisplayName = "Recarga Celular" };
					[FunctionalPoint("Value[5];DisplayName[Recarga Celular]")]
					public static DomainKeyPair RecargaCelular { get { return _RecargaCelular; } }
				    
					private static DomainKeyPair _Servico = new DomainKeyPair() { Value = "2", DisplayName = "Serviço" };
					[FunctionalPoint("Value[2];DisplayName[Serviço]")]
					public static DomainKeyPair Servico { get { return _Servico; } }
				    
					private static DomainKeyPair _ValePresente = new DomainKeyPair() { Value = "4", DisplayName = "Vale Presente" };
					[FunctionalPoint("Value[4];DisplayName[Vale Presente]")]
					public static DomainKeyPair ValePresente { get { return _ValePresente; } }
				    
			#endregion properties

			

	}    
	//<LX_TIPO_RESGATE>((#LxExpr#) == [-1-] ? "Desconto" : ((#LxExpr#) == [-2-] ? "Pagamento" : ""))</LX_TIPO_RESGATE>	
    public partial class LX_TIPO_RESGATE
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Desconto"); 
				    
					result.Add("2", "Pagamento"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Desconto"); 
				    
					result.Add("2", "Pagamento"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Desconto = new DomainKeyPair() { Value = "1", DisplayName = "Desconto" };
					[FunctionalPoint("Value[1];DisplayName[Desconto]")]
					public static DomainKeyPair Desconto { get { return _Desconto; } }
				    
					private static DomainKeyPair _Pagamento = new DomainKeyPair() { Value = "2", DisplayName = "Pagamento" };
					[FunctionalPoint("Value[2];DisplayName[Pagamento]")]
					public static DomainKeyPair Pagamento { get { return _Pagamento; } }
				    
			#endregion properties

			

	}    
	//<LX_FILTRO_TIPO_LOJA>((#LxExpr#) == [-2-] ? "Lojas não participantes" : ((#LxExpr#) == [-1-] ? "Lojas participantes" : ((#LxExpr#) == [-3-] ? "Todas as lojas" : "")))</LX_FILTRO_TIPO_LOJA>	
    public partial class LX_FILTRO_TIPO_LOJA
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "Lojas não participantes"); 
				    
					result.Add("1", "Lojas participantes"); 
				    
					result.Add("3", "Todas as lojas"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "LojasNaoSelecionadas"); 
				    
					result.Add("1", "LojasSelecionadas"); 
				    
					result.Add("3", "TodasLojas"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _LojasNaoSelecionadas = new DomainKeyPair() { Value = "2", DisplayName = "Lojas não participantes" };
					[FunctionalPoint("Value[2];DisplayName[Lojas não participantes]")]
					public static DomainKeyPair LojasNaoSelecionadas { get { return _LojasNaoSelecionadas; } }
				    
					private static DomainKeyPair _LojasSelecionadas = new DomainKeyPair() { Value = "1", DisplayName = "Lojas participantes" };
					[FunctionalPoint("Value[1];DisplayName[Lojas participantes]")]
					public static DomainKeyPair LojasSelecionadas { get { return _LojasSelecionadas; } }
				    
					private static DomainKeyPair _TodasLojas = new DomainKeyPair() { Value = "3", DisplayName = "Todas as lojas" };
					[FunctionalPoint("Value[3];DisplayName[Todas as lojas]")]
					public static DomainKeyPair TodasLojas { get { return _TodasLojas; } }
				    
			#endregion properties

			

	}    
	//<LX_FILTRO_TIPO_OPERACAO>((#LxExpr#) == [-2-] ? "Operações não participantes" : ((#LxExpr#) == [-1-] ? "Operações participantes" : ((#LxExpr#) == [-3-] ? "Todas as operações" : "")))</LX_FILTRO_TIPO_OPERACAO>	
    public partial class LX_FILTRO_TIPO_OPERACAO
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "Operações não participantes"); 
				    
					result.Add("1", "Operações participantes"); 
				    
					result.Add("3", "Todas as operações"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "OperacoesNaoSelecionadas"); 
				    
					result.Add("1", "OperacoesSelecionadas"); 
				    
					result.Add("3", "TodasOperacoes"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _OperacoesNaoSelecionadas = new DomainKeyPair() { Value = "2", DisplayName = "Operações não participantes" };
					[FunctionalPoint("Value[2];DisplayName[Operações não participantes]")]
					public static DomainKeyPair OperacoesNaoSelecionadas { get { return _OperacoesNaoSelecionadas; } }
				    
					private static DomainKeyPair _OperacoesSelecionadas = new DomainKeyPair() { Value = "1", DisplayName = "Operações participantes" };
					[FunctionalPoint("Value[1];DisplayName[Operações participantes]")]
					public static DomainKeyPair OperacoesSelecionadas { get { return _OperacoesSelecionadas; } }
				    
					private static DomainKeyPair _TodasOperacoes = new DomainKeyPair() { Value = "3", DisplayName = "Todas as operações" };
					[FunctionalPoint("Value[3];DisplayName[Todas as operações]")]
					public static DomainKeyPair TodasOperacoes { get { return _TodasOperacoes; } }
				    
			#endregion properties

			

	}    
	//<LX_FILTRO_TIPO_PGTO>((#LxExpr#) == [-2-] ? "Tipos não participantes" : ((#LxExpr#) == [-1-] ? "Tipos participantes" : ((#LxExpr#) == [-3-] ? "Todos os tipos" : "")))</LX_FILTRO_TIPO_PGTO>	
    public partial class LX_FILTRO_TIPO_PGTO
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "Tipos não participantes"); 
				    
					result.Add("1", "Tipos participantes"); 
				    
					result.Add("3", "Todos os tipos"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "TiposNaoSelecionados"); 
				    
					result.Add("1", "TiposSelecionados"); 
				    
					result.Add("3", "TodoTipos"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _TiposNaoSelecionados = new DomainKeyPair() { Value = "2", DisplayName = "Tipos não participantes" };
					[FunctionalPoint("Value[2];DisplayName[Tipos não participantes]")]
					public static DomainKeyPair TiposNaoSelecionados { get { return _TiposNaoSelecionados; } }
				    
					private static DomainKeyPair _TiposSelecionados = new DomainKeyPair() { Value = "1", DisplayName = "Tipos participantes" };
					[FunctionalPoint("Value[1];DisplayName[Tipos participantes]")]
					public static DomainKeyPair TiposSelecionados { get { return _TiposSelecionados; } }
				    
					private static DomainKeyPair _TodoTipos = new DomainKeyPair() { Value = "3", DisplayName = "Todos os tipos" };
					[FunctionalPoint("Value[3];DisplayName[Todos os tipos]")]
					public static DomainKeyPair TodoTipos { get { return _TodoTipos; } }
				    
			#endregion properties

			

	}    
	//<LX_STATUS_PEDIDO>((#LxExpr#) == [-5-] ? "Faturamento iniciado" : ((#LxExpr#) == [-3-] ? "Pedido cancelado" : ((#LxExpr#) == [-1-] ? "Pedido em confecção" : ((#LxExpr#) == [-4-] ? "Pedido encerrado" : ((#LxExpr#) == [-2-] ? "Pedido publicado" : "")))))</LX_STATUS_PEDIDO>	
    public partial class LX_STATUS_PEDIDO
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("5", "Faturamento iniciado"); 
				    
					result.Add("3", "Pedido cancelado"); 
				    
					result.Add("1", "Pedido em confecção"); 
				    
					result.Add("4", "Pedido encerrado"); 
				    
					result.Add("2", "Pedido publicado"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("5", "FaturamentoIniciado"); 
				    
					result.Add("3", "PedidoCancelado"); 
				    
					result.Add("1", "PedidoConfeccao"); 
				    
					result.Add("4", "PedidoEncerrado"); 
				    
					result.Add("2", "PedidoPublicado"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _FaturamentoIniciado = new DomainKeyPair() { Value = "5", DisplayName = "Faturamento iniciado" };
					[FunctionalPoint("Value[5];DisplayName[Faturamento iniciado]")]
					public static DomainKeyPair FaturamentoIniciado { get { return _FaturamentoIniciado; } }
				    
					private static DomainKeyPair _PedidoCancelado = new DomainKeyPair() { Value = "3", DisplayName = "Pedido cancelado" };
					[FunctionalPoint("Value[3];DisplayName[Pedido cancelado]")]
					public static DomainKeyPair PedidoCancelado { get { return _PedidoCancelado; } }
				    
					private static DomainKeyPair _PedidoConfeccao = new DomainKeyPair() { Value = "1", DisplayName = "Pedido em confecção" };
					[FunctionalPoint("Value[1];DisplayName[Pedido em confecção]")]
					public static DomainKeyPair PedidoConfeccao { get { return _PedidoConfeccao; } }
				    
					private static DomainKeyPair _PedidoEncerrado = new DomainKeyPair() { Value = "4", DisplayName = "Pedido encerrado" };
					[FunctionalPoint("Value[4];DisplayName[Pedido encerrado]")]
					public static DomainKeyPair PedidoEncerrado { get { return _PedidoEncerrado; } }
				    
					private static DomainKeyPair _PedidoPublicado = new DomainKeyPair() { Value = "2", DisplayName = "Pedido publicado" };
					[FunctionalPoint("Value[2];DisplayName[Pedido publicado]")]
					public static DomainKeyPair PedidoPublicado { get { return _PedidoPublicado; } }
				    
			#endregion properties

			

	}    
	//<LX_TIPO_PEDIDO>((#LxExpr#) == [-3-] ? "Lista de presente" : ((#LxExpr#) == [-2-] ? "Pedido ecommerce" : ((#LxExpr#) == [-1-] ? "Pré venda" : "")))</LX_TIPO_PEDIDO>	
    public partial class LX_TIPO_PEDIDO
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("3", "Lista de presente"); 
				    
					result.Add("2", "Pedido ecommerce"); 
				    
					result.Add("1", "Pré venda"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("3", "ListaPresente"); 
				    
					result.Add("2", "PedidoEcommerce"); 
				    
					result.Add("1", "PreVenda"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _ListaPresente = new DomainKeyPair() { Value = "3", DisplayName = "Lista de presente" };
					[FunctionalPoint("Value[3];DisplayName[Lista de presente]")]
					public static DomainKeyPair ListaPresente { get { return _ListaPresente; } }
				    
					private static DomainKeyPair _PedidoEcommerce = new DomainKeyPair() { Value = "2", DisplayName = "Pedido ecommerce" };
					[FunctionalPoint("Value[2];DisplayName[Pedido ecommerce]")]
					public static DomainKeyPair PedidoEcommerce { get { return _PedidoEcommerce; } }
				    
					private static DomainKeyPair _PreVenda = new DomainKeyPair() { Value = "1", DisplayName = "Pré venda" };
					[FunctionalPoint("Value[1];DisplayName[Pré venda]")]
					public static DomainKeyPair PreVenda { get { return _PreVenda; } }
				    
			#endregion properties

			

	}    
	//<LX_TIPO_LISTA>((#LxExpr#) == [-2-] ? "Casamento" : ((#LxExpr#) == [-1-] ? "Presente" : ""))</LX_TIPO_LISTA>	
    public partial class LX_TIPO_LISTA
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "Casamento"); 
				    
					result.Add("1", "Presente"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "Casamento"); 
				    
					result.Add("1", "Presente"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Casamento = new DomainKeyPair() { Value = "2", DisplayName = "Casamento" };
					[FunctionalPoint("Value[2];DisplayName[Casamento]")]
					public static DomainKeyPair Casamento { get { return _Casamento; } }
				    
					private static DomainKeyPair _Presente = new DomainKeyPair() { Value = "1", DisplayName = "Presente" };
					[FunctionalPoint("Value[1];DisplayName[Presente]")]
					public static DomainKeyPair Presente { get { return _Presente; } }
				    
			#endregion properties

			

	}    
	//<LxTipoLogradouro>((#LxExpr#) == [-1-] ? "Aeroporto" : ((#LxExpr#) == [-2-] ? "Alameda" : ((#LxExpr#) == [-3-] ? "Apartamento" : ((#LxExpr#) == [-4-] ? "Avenida" : ((#LxExpr#) == [-5-] ? "Beco" : ((#LxExpr#) == [-6-] ? "Bloco" : ((#LxExpr#) == [-7-] ? "Caminho" : ((#LxExpr#) == [-8-] ? "Escadinha" : ((#LxExpr#) == [-9-] ? "Estação" : ((#LxExpr#) == [-10-] ? "Estrada" : ((#LxExpr#) == [-11-] ? "Fazenda" : ((#LxExpr#) == [-12-] ? "Fortaleza" : ((#LxExpr#) == [-13-] ? "Galeria" : ((#LxExpr#) == [-14-] ? "Ladeira" : ((#LxExpr#) == [-15-] ? "Largo" : ((#LxExpr#) == [-17-] ? "Parque" : ((#LxExpr#) == [-16-] ? "Praça" : ((#LxExpr#) == [-18-] ? "Praia" : ((#LxExpr#) == [-19-] ? "Quadra" : ((#LxExpr#) == [-20-] ? "Quilômetro" : ((#LxExpr#) == [-21-] ? "Quinta" : ((#LxExpr#) == [-22-] ? "Rodovia" : ((#LxExpr#) == [-23-] ? "Rua" : ((#LxExpr#) == [-24-] ? "Super Quadra" : ((#LxExpr#) == [-25-] ? "Travessa" : ((#LxExpr#) == [-26-] ? "Viaduto" : ((#LxExpr#) == [-27-] ? "Vila" : "")))))))))))))))))))))))))))</LxTipoLogradouro>	
    public partial class LxTipoLogradouro
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Aeroporto"); 
				    
					result.Add("2", "Alameda"); 
				    
					result.Add("3", "Apartamento"); 
				    
					result.Add("4", "Avenida"); 
				    
					result.Add("5", "Beco"); 
				    
					result.Add("6", "Bloco"); 
				    
					result.Add("7", "Caminho"); 
				    
					result.Add("8", "Escadinha"); 
				    
					result.Add("9", "Estação"); 
				    
					result.Add("10", "Estrada"); 
				    
					result.Add("11", "Fazenda"); 
				    
					result.Add("12", "Fortaleza"); 
				    
					result.Add("13", "Galeria"); 
				    
					result.Add("14", "Ladeira"); 
				    
					result.Add("15", "Largo"); 
				    
					result.Add("17", "Parque"); 
				    
					result.Add("16", "Praça"); 
				    
					result.Add("18", "Praia"); 
				    
					result.Add("19", "Quadra"); 
				    
					result.Add("20", "Quilômetro"); 
				    
					result.Add("21", "Quinta"); 
				    
					result.Add("22", "Rodovia"); 
				    
					result.Add("23", "Rua"); 
				    
					result.Add("24", "Super Quadra"); 
				    
					result.Add("25", "Travessa"); 
				    
					result.Add("26", "Viaduto"); 
				    
					result.Add("27", "Vila"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Aeroporto"); 
				    
					result.Add("2", "Alameda"); 
				    
					result.Add("3", "Apartamento"); 
				    
					result.Add("4", "Avenida"); 
				    
					result.Add("5", "Beco"); 
				    
					result.Add("6", "Bloco"); 
				    
					result.Add("7", "Caminho"); 
				    
					result.Add("8", "Escadinha"); 
				    
					result.Add("9", "Estacao"); 
				    
					result.Add("10", "Estrada"); 
				    
					result.Add("11", "Fazenda"); 
				    
					result.Add("12", "Fortaleza"); 
				    
					result.Add("13", "Galeria"); 
				    
					result.Add("14", "Ladeira"); 
				    
					result.Add("15", "Largo"); 
				    
					result.Add("17", "Parque"); 
				    
					result.Add("16", "Praca"); 
				    
					result.Add("18", "Praia"); 
				    
					result.Add("19", "Quadra"); 
				    
					result.Add("20", "Quilometro"); 
				    
					result.Add("21", "Quinta"); 
				    
					result.Add("22", "Rodovia"); 
				    
					result.Add("23", "Rua"); 
				    
					result.Add("24", "SuperQuadra"); 
				    
					result.Add("25", "Travessa"); 
				    
					result.Add("26", "Viaduto"); 
				    
					result.Add("27", "Vila"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Aeroporto = new DomainKeyPair() { Value = "1", DisplayName = "Aeroporto" };
					[FunctionalPoint("Value[1];DisplayName[Aeroporto]")]
					public static DomainKeyPair Aeroporto { get { return _Aeroporto; } }
				    
					private static DomainKeyPair _Alameda = new DomainKeyPair() { Value = "2", DisplayName = "Alameda" };
					[FunctionalPoint("Value[2];DisplayName[Alameda]")]
					public static DomainKeyPair Alameda { get { return _Alameda; } }
				    
					private static DomainKeyPair _Apartamento = new DomainKeyPair() { Value = "3", DisplayName = "Apartamento" };
					[FunctionalPoint("Value[3];DisplayName[Apartamento]")]
					public static DomainKeyPair Apartamento { get { return _Apartamento; } }
				    
					private static DomainKeyPair _Avenida = new DomainKeyPair() { Value = "4", DisplayName = "Avenida" };
					[FunctionalPoint("Value[4];DisplayName[Avenida]")]
					public static DomainKeyPair Avenida { get { return _Avenida; } }
				    
					private static DomainKeyPair _Beco = new DomainKeyPair() { Value = "5", DisplayName = "Beco" };
					[FunctionalPoint("Value[5];DisplayName[Beco]")]
					public static DomainKeyPair Beco { get { return _Beco; } }
				    
					private static DomainKeyPair _Bloco = new DomainKeyPair() { Value = "6", DisplayName = "Bloco" };
					[FunctionalPoint("Value[6];DisplayName[Bloco]")]
					public static DomainKeyPair Bloco { get { return _Bloco; } }
				    
					private static DomainKeyPair _Caminho = new DomainKeyPair() { Value = "7", DisplayName = "Caminho" };
					[FunctionalPoint("Value[7];DisplayName[Caminho]")]
					public static DomainKeyPair Caminho { get { return _Caminho; } }
				    
					private static DomainKeyPair _Escadinha = new DomainKeyPair() { Value = "8", DisplayName = "Escadinha" };
					[FunctionalPoint("Value[8];DisplayName[Escadinha]")]
					public static DomainKeyPair Escadinha { get { return _Escadinha; } }
				    
					private static DomainKeyPair _Estacao = new DomainKeyPair() { Value = "9", DisplayName = "Estação" };
					[FunctionalPoint("Value[9];DisplayName[Estação]")]
					public static DomainKeyPair Estacao { get { return _Estacao; } }
				    
					private static DomainKeyPair _Estrada = new DomainKeyPair() { Value = "10", DisplayName = "Estrada" };
					[FunctionalPoint("Value[10];DisplayName[Estrada]")]
					public static DomainKeyPair Estrada { get { return _Estrada; } }
				    
					private static DomainKeyPair _Fazenda = new DomainKeyPair() { Value = "11", DisplayName = "Fazenda" };
					[FunctionalPoint("Value[11];DisplayName[Fazenda]")]
					public static DomainKeyPair Fazenda { get { return _Fazenda; } }
				    
					private static DomainKeyPair _Fortaleza = new DomainKeyPair() { Value = "12", DisplayName = "Fortaleza" };
					[FunctionalPoint("Value[12];DisplayName[Fortaleza]")]
					public static DomainKeyPair Fortaleza { get { return _Fortaleza; } }
				    
					private static DomainKeyPair _Galeria = new DomainKeyPair() { Value = "13", DisplayName = "Galeria" };
					[FunctionalPoint("Value[13];DisplayName[Galeria]")]
					public static DomainKeyPair Galeria { get { return _Galeria; } }
				    
					private static DomainKeyPair _Ladeira = new DomainKeyPair() { Value = "14", DisplayName = "Ladeira" };
					[FunctionalPoint("Value[14];DisplayName[Ladeira]")]
					public static DomainKeyPair Ladeira { get { return _Ladeira; } }
				    
					private static DomainKeyPair _Largo = new DomainKeyPair() { Value = "15", DisplayName = "Largo" };
					[FunctionalPoint("Value[15];DisplayName[Largo]")]
					public static DomainKeyPair Largo { get { return _Largo; } }
				    
					private static DomainKeyPair _Parque = new DomainKeyPair() { Value = "17", DisplayName = "Parque" };
					[FunctionalPoint("Value[17];DisplayName[Parque]")]
					public static DomainKeyPair Parque { get { return _Parque; } }
				    
					private static DomainKeyPair _Praca = new DomainKeyPair() { Value = "16", DisplayName = "Praça" };
					[FunctionalPoint("Value[16];DisplayName[Praça]")]
					public static DomainKeyPair Praca { get { return _Praca; } }
				    
					private static DomainKeyPair _Praia = new DomainKeyPair() { Value = "18", DisplayName = "Praia" };
					[FunctionalPoint("Value[18];DisplayName[Praia]")]
					public static DomainKeyPair Praia { get { return _Praia; } }
				    
					private static DomainKeyPair _Quadra = new DomainKeyPair() { Value = "19", DisplayName = "Quadra" };
					[FunctionalPoint("Value[19];DisplayName[Quadra]")]
					public static DomainKeyPair Quadra { get { return _Quadra; } }
				    
					private static DomainKeyPair _Quilometro = new DomainKeyPair() { Value = "20", DisplayName = "Quilômetro" };
					[FunctionalPoint("Value[20];DisplayName[Quilômetro]")]
					public static DomainKeyPair Quilometro { get { return _Quilometro; } }
				    
					private static DomainKeyPair _Quinta = new DomainKeyPair() { Value = "21", DisplayName = "Quinta" };
					[FunctionalPoint("Value[21];DisplayName[Quinta]")]
					public static DomainKeyPair Quinta { get { return _Quinta; } }
				    
					private static DomainKeyPair _Rodovia = new DomainKeyPair() { Value = "22", DisplayName = "Rodovia" };
					[FunctionalPoint("Value[22];DisplayName[Rodovia]")]
					public static DomainKeyPair Rodovia { get { return _Rodovia; } }
				    
					private static DomainKeyPair _Rua = new DomainKeyPair() { Value = "23", DisplayName = "Rua" };
					[FunctionalPoint("Value[23];DisplayName[Rua]")]
					public static DomainKeyPair Rua { get { return _Rua; } }
				    
					private static DomainKeyPair _SuperQuadra = new DomainKeyPair() { Value = "24", DisplayName = "Super Quadra" };
					[FunctionalPoint("Value[24];DisplayName[Super Quadra]")]
					public static DomainKeyPair SuperQuadra { get { return _SuperQuadra; } }
				    
					private static DomainKeyPair _Travessa = new DomainKeyPair() { Value = "25", DisplayName = "Travessa" };
					[FunctionalPoint("Value[25];DisplayName[Travessa]")]
					public static DomainKeyPair Travessa { get { return _Travessa; } }
				    
					private static DomainKeyPair _Viaduto = new DomainKeyPair() { Value = "26", DisplayName = "Viaduto" };
					[FunctionalPoint("Value[26];DisplayName[Viaduto]")]
					public static DomainKeyPair Viaduto { get { return _Viaduto; } }
				    
					private static DomainKeyPair _Vila = new DomainKeyPair() { Value = "27", DisplayName = "Vila" };
					[FunctionalPoint("Value[27];DisplayName[Vila]")]
					public static DomainKeyPair Vila { get { return _Vila; } }
				    
			#endregion properties

			

	}    
	//<LX_ESTADO_CIVIL>((#LxExpr#) == [-2-] ? "Casado(a)" : ((#LxExpr#) == [-4-] ? "Divorciado(a)" : ((#LxExpr#) == [-6-] ? "Outros" : ((#LxExpr#) == [-3-] ? "Separado(a)" : ((#LxExpr#) == [-1-] ? "Solteiro(a)" : ((#LxExpr#) == [-5-] ? "Viúvo(a)" : ""))))))</LX_ESTADO_CIVIL>	
    public partial class LX_ESTADO_CIVIL
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "Casado(a)"); 
				    
					result.Add("4", "Divorciado(a)"); 
				    
					result.Add("6", "Outros"); 
				    
					result.Add("3", "Separado(a)"); 
				    
					result.Add("1", "Solteiro(a)"); 
				    
					result.Add("5", "Viúvo(a)"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "Casado"); 
				    
					result.Add("4", "Divorciado"); 
				    
					result.Add("6", "Outros"); 
				    
					result.Add("3", "Separado"); 
				    
					result.Add("1", "Solteiro"); 
				    
					result.Add("5", "Viuvo"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Casado = new DomainKeyPair() { Value = "2", DisplayName = "Casado(a)" };
					[FunctionalPoint("Value[2];DisplayName[Casado(a)]")]
					public static DomainKeyPair Casado { get { return _Casado; } }
				    
					private static DomainKeyPair _Divorciado = new DomainKeyPair() { Value = "4", DisplayName = "Divorciado(a)" };
					[FunctionalPoint("Value[4];DisplayName[Divorciado(a)]")]
					public static DomainKeyPair Divorciado { get { return _Divorciado; } }
				    
					private static DomainKeyPair _Outros = new DomainKeyPair() { Value = "6", DisplayName = "Outros" };
					[FunctionalPoint("Value[6];DisplayName[Outros]")]
					public static DomainKeyPair Outros { get { return _Outros; } }
				    
					private static DomainKeyPair _Separado = new DomainKeyPair() { Value = "3", DisplayName = "Separado(a)" };
					[FunctionalPoint("Value[3];DisplayName[Separado(a)]")]
					public static DomainKeyPair Separado { get { return _Separado; } }
				    
					private static DomainKeyPair _Solteiro = new DomainKeyPair() { Value = "1", DisplayName = "Solteiro(a)" };
					[FunctionalPoint("Value[1];DisplayName[Solteiro(a)]")]
					public static DomainKeyPair Solteiro { get { return _Solteiro; } }
				    
					private static DomainKeyPair _Viuvo = new DomainKeyPair() { Value = "5", DisplayName = "Viúvo(a)" };
					[FunctionalPoint("Value[5];DisplayName[Viúvo(a)]")]
					public static DomainKeyPair Viuvo { get { return _Viuvo; } }
				    
			#endregion properties

			

	}    
	//<LX_TIPO_VALIDACAO>((#LxExpr#) == [-1-] ? "Atributos" : ((#LxExpr#) == [-2-] ? "Faixa de Valores" : ((#LxExpr#) == [-4-] ? "Livre" : ((#LxExpr#) == [-3-] ? "Tabelas de Sistemas" : ""))))</LX_TIPO_VALIDACAO>	
    public partial class LX_TIPO_VALIDACAO
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Atributos"); 
				    
					result.Add("2", "Faixa de Valores"); 
				    
					result.Add("4", "Livre"); 
				    
					result.Add("3", "Tabelas de Sistemas"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Atributos"); 
				    
					result.Add("2", "FaixaValores"); 
				    
					result.Add("4", "Livre"); 
				    
					result.Add("3", "TabelasSistemas"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Atributos = new DomainKeyPair() { Value = "1", DisplayName = "Atributos" };
					[FunctionalPoint("Value[1];DisplayName[Atributos]")]
					public static DomainKeyPair Atributos { get { return _Atributos; } }
				    
					private static DomainKeyPair _FaixaValores = new DomainKeyPair() { Value = "2", DisplayName = "Faixa de Valores" };
					[FunctionalPoint("Value[2];DisplayName[Faixa de Valores]")]
					public static DomainKeyPair FaixaValores { get { return _FaixaValores; } }
				    
					private static DomainKeyPair _Livre = new DomainKeyPair() { Value = "4", DisplayName = "Livre" };
					[FunctionalPoint("Value[4];DisplayName[Livre]")]
					public static DomainKeyPair Livre { get { return _Livre; } }
				    
					private static DomainKeyPair _TabelasSistemas = new DomainKeyPair() { Value = "3", DisplayName = "Tabelas de Sistemas" };
					[FunctionalPoint("Value[3];DisplayName[Tabelas de Sistemas]")]
					public static DomainKeyPair TabelasSistemas { get { return _TabelasSistemas; } }
				    
			#endregion properties

			

	}    
	//<LX_PRIORIDADE_PROMOCAO>((#LxExpr#) == [-4-] ? "Alta" : ((#LxExpr#) == [-2-] ? "Baixa" : ((#LxExpr#) == [-3-] ? "Média" : ((#LxExpr#) == [-5-] ? "Muito Alta" : ((#LxExpr#) == [-1-] ? "Muito Baixa" : "")))))</LX_PRIORIDADE_PROMOCAO>	
    public partial class LX_PRIORIDADE_PROMOCAO
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("4", "Alta"); 
				    
					result.Add("2", "Baixa"); 
				    
					result.Add("3", "Média"); 
				    
					result.Add("5", "Muito Alta"); 
				    
					result.Add("1", "Muito Baixa"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("4", "Alta"); 
				    
					result.Add("2", "Baixa"); 
				    
					result.Add("3", "Media"); 
				    
					result.Add("5", "MuitoAlta"); 
				    
					result.Add("1", "MuitoBaixa"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Alta = new DomainKeyPair() { Value = "4", DisplayName = "Alta" };
					[FunctionalPoint("Value[4];DisplayName[Alta]")]
					public static DomainKeyPair Alta { get { return _Alta; } }
				    
					private static DomainKeyPair _Baixa = new DomainKeyPair() { Value = "2", DisplayName = "Baixa" };
					[FunctionalPoint("Value[2];DisplayName[Baixa]")]
					public static DomainKeyPair Baixa { get { return _Baixa; } }
				    
					private static DomainKeyPair _Media = new DomainKeyPair() { Value = "3", DisplayName = "Média" };
					[FunctionalPoint("Value[3];DisplayName[Média]")]
					public static DomainKeyPair Media { get { return _Media; } }
				    
					private static DomainKeyPair _MuitoAlta = new DomainKeyPair() { Value = "5", DisplayName = "Muito Alta" };
					[FunctionalPoint("Value[5];DisplayName[Muito Alta]")]
					public static DomainKeyPair MuitoAlta { get { return _MuitoAlta; } }
				    
					private static DomainKeyPair _MuitoBaixa = new DomainKeyPair() { Value = "1", DisplayName = "Muito Baixa" };
					[FunctionalPoint("Value[1];DisplayName[Muito Baixa]")]
					public static DomainKeyPair MuitoBaixa { get { return _MuitoBaixa; } }
				    
			#endregion properties

			

	}    
	//<LX_CODIGO_IDIOMA>((#LxExpr#) == [-1-] ? "Português" : "")</LX_CODIGO_IDIOMA>	
    public partial class LX_CODIGO_IDIOMA
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Português"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Portugues"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Portugues = new DomainKeyPair() { Value = "1", DisplayName = "Português" };
					[FunctionalPoint("Value[1];DisplayName[Português]")]
					public static DomainKeyPair Portugues { get { return _Portugues; } }
				    
			#endregion properties

			

	}    
	//<LX_TIPO_OFERTA>((#LxExpr#) == [-7-] ? "Brinde Próprio" : ((#LxExpr#) == [-8-] ? "Brinde Terceiro" : ((#LxExpr#) == [-11-] ? "Campanha Estacionamento" : ((#LxExpr#) == [-10-] ? "Campanha Frete" : ((#LxExpr#) == [-14-] ? "Comissão Extra" : ((#LxExpr#) == [-17-] ? "Cupom Promocional" : ((#LxExpr#) == [-2-] ? "Desconto Item" : ((#LxExpr#) == [-1-] ? "Desconto Subtotal" : ((#LxExpr#) == [-3-] ? "Desconto Subtotal Cupom" : ((#LxExpr#) == [-4-] ? "Desconto Subtotal Gift" : ((#LxExpr#) == [-9-] ? "Gift Terceiro" : ((#LxExpr#) == [-15-] ? "Outras Ofertas" : ((#LxExpr#) == [-12-] ? "Pontuação Fidelidade" : ((#LxExpr#) == [-13-] ? "Pontuação Vendedor" : ((#LxExpr#) == [-16-] ? "Sem Benefício" : ((#LxExpr#) == [-6-] ? "Vale Produto Outra Venda" : ((#LxExpr#) == [-5-] ? "Vale Produto Venda Atual" : "")))))))))))))))))</LX_TIPO_OFERTA>	
    public partial class LX_TIPO_OFERTA
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("7", "Brinde Próprio"); 
				    
					result.Add("8", "Brinde Terceiro"); 
				    
					result.Add("11", "Campanha Estacionamento"); 
				    
					result.Add("10", "Campanha Frete"); 
				    
					result.Add("14", "Comissão Extra"); 
				    
					result.Add("17", "Cupom Promocional"); 
				    
					result.Add("2", "Desconto Item"); 
				    
					result.Add("1", "Desconto Subtotal"); 
				    
					result.Add("3", "Desconto Subtotal Cupom"); 
				    
					result.Add("4", "Desconto Subtotal Gift"); 
				    
					result.Add("9", "Gift Terceiro"); 
				    
					result.Add("15", "Outras Ofertas"); 
				    
					result.Add("12", "Pontuação Fidelidade"); 
				    
					result.Add("13", "Pontuação Vendedor"); 
				    
					result.Add("16", "Sem Benefício"); 
				    
					result.Add("6", "Vale Produto Outra Venda"); 
				    
					result.Add("5", "Vale Produto Venda Atual"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("7", "BrindeProprio"); 
				    
					result.Add("8", "BrindeTerceiro"); 
				    
					result.Add("11", "CampanhaEstacionamento"); 
				    
					result.Add("10", "CampanhaFrete"); 
				    
					result.Add("14", "ComissaoExtra"); 
				    
					result.Add("17", "CupomPromocional"); 
				    
					result.Add("2", "DescontoItem"); 
				    
					result.Add("1", "DescontoSubtotal"); 
				    
					result.Add("3", "DescontoSubtotalCupom"); 
				    
					result.Add("4", "DescontoSubtotalGift"); 
				    
					result.Add("9", "GiftTerceiro"); 
				    
					result.Add("15", "OutrasOfertas"); 
				    
					result.Add("12", "PontuacaoFidelidade"); 
				    
					result.Add("13", "PontuacaoVendedor"); 
				    
					result.Add("16", "SemBeneficio"); 
				    
					result.Add("6", "ValeProdutoOutraVenda"); 
				    
					result.Add("5", "ValeProdutoVendaAtual"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _BrindeProprio = new DomainKeyPair() { Value = "7", DisplayName = "Brinde Próprio" };
					[FunctionalPoint("Value[7];DisplayName[Brinde Próprio]")]
					public static DomainKeyPair BrindeProprio { get { return _BrindeProprio; } }
				    
					private static DomainKeyPair _BrindeTerceiro = new DomainKeyPair() { Value = "8", DisplayName = "Brinde Terceiro" };
					[FunctionalPoint("Value[8];DisplayName[Brinde Terceiro]")]
					public static DomainKeyPair BrindeTerceiro { get { return _BrindeTerceiro; } }
				    
					private static DomainKeyPair _CampanhaEstacionamento = new DomainKeyPair() { Value = "11", DisplayName = "Campanha Estacionamento" };
					[FunctionalPoint("Value[11];DisplayName[Campanha Estacionamento]")]
					public static DomainKeyPair CampanhaEstacionamento { get { return _CampanhaEstacionamento; } }
				    
					private static DomainKeyPair _CampanhaFrete = new DomainKeyPair() { Value = "10", DisplayName = "Campanha Frete" };
					[FunctionalPoint("Value[10];DisplayName[Campanha Frete]")]
					public static DomainKeyPair CampanhaFrete { get { return _CampanhaFrete; } }
				    
					private static DomainKeyPair _ComissaoExtra = new DomainKeyPair() { Value = "14", DisplayName = "Comissão Extra" };
					[FunctionalPoint("Value[14];DisplayName[Comissão Extra]")]
					public static DomainKeyPair ComissaoExtra { get { return _ComissaoExtra; } }
				    
					private static DomainKeyPair _CupomPromocional = new DomainKeyPair() { Value = "17", DisplayName = "Cupom Promocional" };
					[FunctionalPoint("Value[17];DisplayName[Cupom Promocional]")]
					public static DomainKeyPair CupomPromocional { get { return _CupomPromocional; } }
				    
					private static DomainKeyPair _DescontoItem = new DomainKeyPair() { Value = "2", DisplayName = "Desconto Item" };
					[FunctionalPoint("Value[2];DisplayName[Desconto Item]")]
					public static DomainKeyPair DescontoItem { get { return _DescontoItem; } }
				    
					private static DomainKeyPair _DescontoSubtotal = new DomainKeyPair() { Value = "1", DisplayName = "Desconto Subtotal" };
					[FunctionalPoint("Value[1];DisplayName[Desconto Subtotal]")]
					public static DomainKeyPair DescontoSubtotal { get { return _DescontoSubtotal; } }
				    
					private static DomainKeyPair _DescontoSubtotalCupom = new DomainKeyPair() { Value = "3", DisplayName = "Desconto Subtotal Cupom" };
					[FunctionalPoint("Value[3];DisplayName[Desconto Subtotal Cupom]")]
					public static DomainKeyPair DescontoSubtotalCupom { get { return _DescontoSubtotalCupom; } }
				    
					private static DomainKeyPair _DescontoSubtotalGift = new DomainKeyPair() { Value = "4", DisplayName = "Desconto Subtotal Gift" };
					[FunctionalPoint("Value[4];DisplayName[Desconto Subtotal Gift]")]
					public static DomainKeyPair DescontoSubtotalGift { get { return _DescontoSubtotalGift; } }
				    
					private static DomainKeyPair _GiftTerceiro = new DomainKeyPair() { Value = "9", DisplayName = "Gift Terceiro" };
					[FunctionalPoint("Value[9];DisplayName[Gift Terceiro]")]
					public static DomainKeyPair GiftTerceiro { get { return _GiftTerceiro; } }
				    
					private static DomainKeyPair _OutrasOfertas = new DomainKeyPair() { Value = "15", DisplayName = "Outras Ofertas" };
					[FunctionalPoint("Value[15];DisplayName[Outras Ofertas]")]
					public static DomainKeyPair OutrasOfertas { get { return _OutrasOfertas; } }
				    
					private static DomainKeyPair _PontuacaoFidelidade = new DomainKeyPair() { Value = "12", DisplayName = "Pontuação Fidelidade" };
					[FunctionalPoint("Value[12];DisplayName[Pontuação Fidelidade]")]
					public static DomainKeyPair PontuacaoFidelidade { get { return _PontuacaoFidelidade; } }
				    
					private static DomainKeyPair _PontuacaoVendedor = new DomainKeyPair() { Value = "13", DisplayName = "Pontuação Vendedor" };
					[FunctionalPoint("Value[13];DisplayName[Pontuação Vendedor]")]
					public static DomainKeyPair PontuacaoVendedor { get { return _PontuacaoVendedor; } }
				    
					private static DomainKeyPair _SemBeneficio = new DomainKeyPair() { Value = "16", DisplayName = "Sem Benefício" };
					[FunctionalPoint("Value[16];DisplayName[Sem Benefício]")]
					public static DomainKeyPair SemBeneficio { get { return _SemBeneficio; } }
				    
					private static DomainKeyPair _ValeProdutoOutraVenda = new DomainKeyPair() { Value = "6", DisplayName = "Vale Produto Outra Venda" };
					[FunctionalPoint("Value[6];DisplayName[Vale Produto Outra Venda]")]
					public static DomainKeyPair ValeProdutoOutraVenda { get { return _ValeProdutoOutraVenda; } }
				    
					private static DomainKeyPair _ValeProdutoVendaAtual = new DomainKeyPair() { Value = "5", DisplayName = "Vale Produto Venda Atual" };
					[FunctionalPoint("Value[5];DisplayName[Vale Produto Venda Atual]")]
					public static DomainKeyPair ValeProdutoVendaAtual { get { return _ValeProdutoVendaAtual; } }
				    
			#endregion properties

			

	}    
	//<LX_MODALIDADE_FRETE>((#LxExpr#) == [-1-] ? "Por Conta do Destinatário/Remetente" : ((#LxExpr#) == [-100-] ? "Por Conta do Emitente" : ((#LxExpr#) == [-2-] ? "Por Conta de Terceiros" : ((#LxExpr#) == [-9-] ? "Sem Frete" : ""))))</LX_MODALIDADE_FRETE>	
    public partial class LX_MODALIDADE_FRETE
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Por Conta do Destinatário/Remetente"); 
				    
					result.Add("100", "Por Conta do Emitente"); 
				    
					result.Add("2", "Por Conta de Terceiros"); 
				    
					result.Add("9", "Sem Frete"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "PorContaDestinatário"); 
				    
					result.Add("100", "PorContaEmitente"); 
				    
					result.Add("2", "PorContaTerceiros"); 
				    
					result.Add("9", "SemFrete"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _PorContaDestinatário = new DomainKeyPair() { Value = "1", DisplayName = "Por Conta do Destinatário/Remetente" };
					[FunctionalPoint("Value[1];DisplayName[Por Conta do Destinatário/Remetente]")]
					public static DomainKeyPair PorContaDestinatário { get { return _PorContaDestinatário; } }
				    
					private static DomainKeyPair _PorContaEmitente = new DomainKeyPair() { Value = "100", DisplayName = "Por Conta do Emitente" };
					[FunctionalPoint("Value[100];DisplayName[Por Conta do Emitente]")]
					public static DomainKeyPair PorContaEmitente { get { return _PorContaEmitente; } }
				    
					private static DomainKeyPair _PorContaTerceiros = new DomainKeyPair() { Value = "2", DisplayName = "Por Conta de Terceiros" };
					[FunctionalPoint("Value[2];DisplayName[Por Conta de Terceiros]")]
					public static DomainKeyPair PorContaTerceiros { get { return _PorContaTerceiros; } }
				    
					private static DomainKeyPair _SemFrete = new DomainKeyPair() { Value = "9", DisplayName = "Sem Frete" };
					[FunctionalPoint("Value[9];DisplayName[Sem Frete]")]
					public static DomainKeyPair SemFrete { get { return _SemFrete; } }
				    
			#endregion properties

			

	}    
	//<LX_STATUS_LGE_PEDIDO>((#LxExpr#) == [-4-] ? "Aguardando Recebimento" : ((#LxExpr#) == [-3-] ? "Aprovação Externa" : ((#LxExpr#) == [-2-] ? "Aprovação Interna" : ((#LxExpr#) == [-9-] ? "Cancelado" : ((#LxExpr#) == [-1-] ? "Em Elaboração" : ((#LxExpr#) == [-5-] ? "Já Faturado e em trânsito" : ((#LxExpr#) == [-6-] ? "Encerrado" : ((#LxExpr#) == [-7-] ? "Não Aprovado Internamente" : ""))))))))</LX_STATUS_LGE_PEDIDO>	
    public partial class LX_STATUS_LGE_PEDIDO
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("4", "Aguardando Recebimento"); 
				    
					result.Add("3", "Aprovação Externa"); 
				    
					result.Add("2", "Aprovação Interna"); 
				    
					result.Add("9", "Cancelado"); 
				    
					result.Add("1", "Em Elaboração"); 
				    
					result.Add("5", "Já Faturado e em trânsito"); 
				    
					result.Add("6", "Encerrado"); 
				    
					result.Add("7", "Não Aprovado Internamente"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("4", "AguardandoRecebimento"); 
				    
					result.Add("3", "AprovacaoExterna"); 
				    
					result.Add("2", "AprovacaoInterna"); 
				    
					result.Add("9", "Cancelado"); 
				    
					result.Add("1", "EmElaboracao"); 
				    
					result.Add("5", "EmTransito"); 
				    
					result.Add("6", "Encerrado"); 
				    
					result.Add("7", "NaoAprovadoInternamente"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _AguardandoRecebimento = new DomainKeyPair() { Value = "4", DisplayName = "Aguardando Recebimento" };
					[FunctionalPoint("Value[4];DisplayName[Aguardando Recebimento]")]
					public static DomainKeyPair AguardandoRecebimento { get { return _AguardandoRecebimento; } }
				    
					private static DomainKeyPair _AprovacaoExterna = new DomainKeyPair() { Value = "3", DisplayName = "Aprovação Externa" };
					[FunctionalPoint("Value[3];DisplayName[Aprovação Externa]")]
					public static DomainKeyPair AprovacaoExterna { get { return _AprovacaoExterna; } }
				    
					private static DomainKeyPair _AprovacaoInterna = new DomainKeyPair() { Value = "2", DisplayName = "Aprovação Interna" };
					[FunctionalPoint("Value[2];DisplayName[Aprovação Interna]")]
					public static DomainKeyPair AprovacaoInterna { get { return _AprovacaoInterna; } }
				    
					private static DomainKeyPair _Cancelado = new DomainKeyPair() { Value = "9", DisplayName = "Cancelado" };
					[FunctionalPoint("Value[9];DisplayName[Cancelado]")]
					public static DomainKeyPair Cancelado { get { return _Cancelado; } }
				    
					private static DomainKeyPair _EmElaboracao = new DomainKeyPair() { Value = "1", DisplayName = "Em Elaboração" };
					[FunctionalPoint("Value[1];DisplayName[Em Elaboração]")]
					public static DomainKeyPair EmElaboracao { get { return _EmElaboracao; } }
				    
					private static DomainKeyPair _EmTransito = new DomainKeyPair() { Value = "5", DisplayName = "Já Faturado e em trânsito" };
					[FunctionalPoint("Value[5];DisplayName[Já Faturado e em trânsito]")]
					public static DomainKeyPair EmTransito { get { return _EmTransito; } }
				    
					private static DomainKeyPair _Encerrado = new DomainKeyPair() { Value = "6", DisplayName = "Encerrado" };
					[FunctionalPoint("Value[6];DisplayName[Encerrado]")]
					public static DomainKeyPair Encerrado { get { return _Encerrado; } }
				    
					private static DomainKeyPair _NaoAprovadoInternamente = new DomainKeyPair() { Value = "7", DisplayName = "Não Aprovado Internamente" };
					[FunctionalPoint("Value[7];DisplayName[Não Aprovado Internamente]")]
					public static DomainKeyPair NaoAprovadoInternamente { get { return _NaoAprovadoInternamente; } }
				    
			#endregion properties

			

	}    
	//<LX_STATUS_ATENDIMENTO>((#LxExpr#) == [-3-] ? "Atendimento Concluído" : ((#LxExpr#) == [-1-] ? "Atendimento em Andamento" : ((#LxExpr#) == [-2-] ? "Atendimento Suspenso" : ((#LxExpr#) == [-0-] ? "Indefinido" : ""))))</LX_STATUS_ATENDIMENTO>	
    public partial class LX_STATUS_ATENDIMENTO
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("3", "Atendimento Concluído"); 
				    
					result.Add("1", "Atendimento em Andamento"); 
				    
					result.Add("2", "Atendimento Suspenso"); 
				    
					result.Add("0", "Indefinido"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("3", "AtendimentoConcluido"); 
				    
					result.Add("1", "AtendimentoEmAndamento"); 
				    
					result.Add("2", "AtendimentoSuspenso"); 
				    
					result.Add("0", "Indefinido"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _AtendimentoConcluido = new DomainKeyPair() { Value = "3", DisplayName = "Atendimento Concluído" };
					[FunctionalPoint("Value[3];DisplayName[Atendimento Concluído]")]
					public static DomainKeyPair AtendimentoConcluido { get { return _AtendimentoConcluido; } }
				    
					private static DomainKeyPair _AtendimentoEmAndamento = new DomainKeyPair() { Value = "1", DisplayName = "Atendimento em Andamento" };
					[FunctionalPoint("Value[1];DisplayName[Atendimento em Andamento]")]
					public static DomainKeyPair AtendimentoEmAndamento { get { return _AtendimentoEmAndamento; } }
				    
					private static DomainKeyPair _AtendimentoSuspenso = new DomainKeyPair() { Value = "2", DisplayName = "Atendimento Suspenso" };
					[FunctionalPoint("Value[2];DisplayName[Atendimento Suspenso]")]
					public static DomainKeyPair AtendimentoSuspenso { get { return _AtendimentoSuspenso; } }
				    
					private static DomainKeyPair _Indefinido = new DomainKeyPair() { Value = "0", DisplayName = "Indefinido" };
					[FunctionalPoint("Value[0];DisplayName[Indefinido]")]
					public static DomainKeyPair Indefinido { get { return _Indefinido; } }
				    
			#endregion properties

			

	}    
	//<LX_FATOR_STK_MOV>((#LxExpr#) == [-1-] ? "Entrada" : ((#LxExpr#) == [--1-] ? "Saída" : ""))</LX_FATOR_STK_MOV>	
    public partial class LX_FATOR_STK_MOV
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Entrada"); 
				    
					result.Add("-1", "Saída"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Entrada"); 
				    
					result.Add("-1", "Saida"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Entrada = new DomainKeyPair() { Value = "1", DisplayName = "Entrada" };
					[FunctionalPoint("Value[1];DisplayName[Entrada]")]
					public static DomainKeyPair Entrada { get { return _Entrada; } }
				    
					private static DomainKeyPair _Saida = new DomainKeyPair() { Value = "-1", DisplayName = "Saída" };
					[FunctionalPoint("Value[-1];DisplayName[Saída]")]
					public static DomainKeyPair Saida { get { return _Saida; } }
				    
			#endregion properties

			

	}    
	//<LX_TIPO_OPERACAO>((#LxExpr#) == [-5-] ? "Ajuste de Estoque" : ((#LxExpr#) == [-7-] ? "Loja Devolução" : ((#LxExpr#) == [-3-] ? "Entrada no Estoque" : ((#LxExpr#) == [-1-] ? "Entrada de Nota Fiscal" : ((#LxExpr#) == [-50-] ? "Financeiro a Pagar" : ((#LxExpr#) == [-60-] ? "Financeiro a Receber" : ((#LxExpr#) == [-4-] ? "Saída do Estoque" : ((#LxExpr#) == [-2-] ? "Saida de Nota Fiscal" : ((#LxExpr#) == [-6-] ? "Loja Venda" : "")))))))))</LX_TIPO_OPERACAO>	
    public partial class LX_TIPO_OPERACAO
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("5", "Ajuste de Estoque"); 
				    
					result.Add("7", "Loja Devolução"); 
				    
					result.Add("3", "Entrada no Estoque"); 
				    
					result.Add("1", "Entrada de Nota Fiscal"); 
				    
					result.Add("50", "Financeiro a Pagar"); 
				    
					result.Add("60", "Financeiro a Receber"); 
				    
					result.Add("4", "Saída do Estoque"); 
				    
					result.Add("2", "Saida de Nota Fiscal"); 
				    
					result.Add("6", "Loja Venda"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("5", "AjusteEstoque"); 
				    
					result.Add("7", "DevoluçãoVarejo"); 
				    
					result.Add("3", "EntradaEstoque"); 
				    
					result.Add("1", "EntradaNotaFiscal"); 
				    
					result.Add("50", "FinanceiroPagar"); 
				    
					result.Add("60", "FinanceiroReceber"); 
				    
					result.Add("4", "SaidaEstoque"); 
				    
					result.Add("2", "SaidaNotaFiscal"); 
				    
					result.Add("6", "VendaVarejo"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _AjusteEstoque = new DomainKeyPair() { Value = "5", DisplayName = "Ajuste de Estoque" };
					[FunctionalPoint("Value[5];DisplayName[Ajuste de Estoque]")]
					public static DomainKeyPair AjusteEstoque { get { return _AjusteEstoque; } }
				    
					private static DomainKeyPair _DevoluçãoVarejo = new DomainKeyPair() { Value = "7", DisplayName = "Loja Devolução" };
					[FunctionalPoint("Value[7];DisplayName[Loja Devolução]")]
					public static DomainKeyPair DevoluçãoVarejo { get { return _DevoluçãoVarejo; } }
				    
					private static DomainKeyPair _EntradaEstoque = new DomainKeyPair() { Value = "3", DisplayName = "Entrada no Estoque" };
					[FunctionalPoint("Value[3];DisplayName[Entrada no Estoque]")]
					public static DomainKeyPair EntradaEstoque { get { return _EntradaEstoque; } }
				    
					private static DomainKeyPair _EntradaNotaFiscal = new DomainKeyPair() { Value = "1", DisplayName = "Entrada de Nota Fiscal" };
					[FunctionalPoint("Value[1];DisplayName[Entrada de Nota Fiscal]")]
					public static DomainKeyPair EntradaNotaFiscal { get { return _EntradaNotaFiscal; } }
				    
					private static DomainKeyPair _FinanceiroPagar = new DomainKeyPair() { Value = "50", DisplayName = "Financeiro a Pagar" };
					[FunctionalPoint("Value[50];DisplayName[Financeiro a Pagar]")]
					public static DomainKeyPair FinanceiroPagar { get { return _FinanceiroPagar; } }
				    
					private static DomainKeyPair _FinanceiroReceber = new DomainKeyPair() { Value = "60", DisplayName = "Financeiro a Receber" };
					[FunctionalPoint("Value[60];DisplayName[Financeiro a Receber]")]
					public static DomainKeyPair FinanceiroReceber { get { return _FinanceiroReceber; } }
				    
					private static DomainKeyPair _SaidaEstoque = new DomainKeyPair() { Value = "4", DisplayName = "Saída do Estoque" };
					[FunctionalPoint("Value[4];DisplayName[Saída do Estoque]")]
					public static DomainKeyPair SaidaEstoque { get { return _SaidaEstoque; } }
				    
					private static DomainKeyPair _SaidaNotaFiscal = new DomainKeyPair() { Value = "2", DisplayName = "Saida de Nota Fiscal" };
					[FunctionalPoint("Value[2];DisplayName[Saida de Nota Fiscal]")]
					public static DomainKeyPair SaidaNotaFiscal { get { return _SaidaNotaFiscal; } }
				    
					private static DomainKeyPair _VendaVarejo = new DomainKeyPair() { Value = "6", DisplayName = "Loja Venda" };
					[FunctionalPoint("Value[6];DisplayName[Loja Venda]")]
					public static DomainKeyPair VendaVarejo { get { return _VendaVarejo; } }
				    
			#endregion properties

			

	}    
	//<LX_COND_PAGTO_STATUS>((#LxExpr#) == [-4-] ? "Aprovada" : ((#LxExpr#) == [-3-] ? "Em Aprovação Externa" : ((#LxExpr#) == [-2-] ? "Em Aprovação Interna" : ((#LxExpr#) == [-1-] ? "Em Elaboração" : ((#LxExpr#) == [-5-] ? "Reprovada" : "")))))</LX_COND_PAGTO_STATUS>	
    public partial class LX_COND_PAGTO_STATUS
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("4", "Aprovada"); 
				    
					result.Add("3", "Em Aprovação Externa"); 
				    
					result.Add("2", "Em Aprovação Interna"); 
				    
					result.Add("1", "Em Elaboração"); 
				    
					result.Add("5", "Reprovada"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("4", "Aprovada"); 
				    
					result.Add("3", "EmAprovacaoExterna"); 
				    
					result.Add("2", "EmAprovacaoInterna"); 
				    
					result.Add("1", "EmElaboracao"); 
				    
					result.Add("5", "Reprovada"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Aprovada = new DomainKeyPair() { Value = "4", DisplayName = "Aprovada" };
					[FunctionalPoint("Value[4];DisplayName[Aprovada]")]
					public static DomainKeyPair Aprovada { get { return _Aprovada; } }
				    
					private static DomainKeyPair _EmAprovacaoExterna = new DomainKeyPair() { Value = "3", DisplayName = "Em Aprovação Externa" };
					[FunctionalPoint("Value[3];DisplayName[Em Aprovação Externa]")]
					public static DomainKeyPair EmAprovacaoExterna { get { return _EmAprovacaoExterna; } }
				    
					private static DomainKeyPair _EmAprovacaoInterna = new DomainKeyPair() { Value = "2", DisplayName = "Em Aprovação Interna" };
					[FunctionalPoint("Value[2];DisplayName[Em Aprovação Interna]")]
					public static DomainKeyPair EmAprovacaoInterna { get { return _EmAprovacaoInterna; } }
				    
					private static DomainKeyPair _EmElaboracao = new DomainKeyPair() { Value = "1", DisplayName = "Em Elaboração" };
					[FunctionalPoint("Value[1];DisplayName[Em Elaboração]")]
					public static DomainKeyPair EmElaboracao { get { return _EmElaboracao; } }
				    
					private static DomainKeyPair _Reprovada = new DomainKeyPair() { Value = "5", DisplayName = "Reprovada" };
					[FunctionalPoint("Value[5];DisplayName[Reprovada]")]
					public static DomainKeyPair Reprovada { get { return _Reprovada; } }
				    
			#endregion properties

			

	}    
	//<LX_TIPO_COND_PAGTO>((#LxExpr#) == [-1-] ? "À Vista" : ((#LxExpr#) == [-2-] ? "Parcela Fixa" : ((#LxExpr#) == [-3-] ? "Parcela Variável" : "")))</LX_TIPO_COND_PAGTO>	
    public partial class LX_TIPO_COND_PAGTO
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "À Vista"); 
				    
					result.Add("2", "Parcela Fixa"); 
				    
					result.Add("3", "Parcela Variável"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "AVista"); 
				    
					result.Add("2", "ParcelaFixa"); 
				    
					result.Add("3", "ParcelaVariavel"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _AVista = new DomainKeyPair() { Value = "1", DisplayName = "À Vista" };
					[FunctionalPoint("Value[1];DisplayName[À Vista]")]
					public static DomainKeyPair AVista { get { return _AVista; } }
				    
					private static DomainKeyPair _ParcelaFixa = new DomainKeyPair() { Value = "2", DisplayName = "Parcela Fixa" };
					[FunctionalPoint("Value[2];DisplayName[Parcela Fixa]")]
					public static DomainKeyPair ParcelaFixa { get { return _ParcelaFixa; } }
				    
					private static DomainKeyPair _ParcelaVariavel = new DomainKeyPair() { Value = "3", DisplayName = "Parcela Variável" };
					[FunctionalPoint("Value[3];DisplayName[Parcela Variável]")]
					public static DomainKeyPair ParcelaVariavel { get { return _ParcelaVariavel; } }
				    
			#endregion properties

			

	}    
	//<LX_STATUS_NFE>((#LxExpr#) == [-14-] ? "Aguardando Informações" : ((#LxExpr#) == [-8-] ? "Autorizado" : ((#LxExpr#) == [-5-] ? "Autorizando" : ((#LxExpr#) == [-11-] ? "Cancelado" : ((#LxExpr#) == [-7-] ? "Consultando" : ((#LxExpr#) == [-13-] ? "Denegado" : ((#LxExpr#) == [-9-] ? "Erro na Comunicação com o MID-e" : ((#LxExpr#) == [-10-] ? "Erro da SEFAZ" : ((#LxExpr#) == [-2-] ? "Erro na Solicitação do XML" : ((#LxExpr#) == [-12-] ? "Inutilizado" : ((#LxExpr#) == [-16-] ? "NF não Eletrônica" : ((#LxExpr#) == [-4-] ? "Pendente de Autorização" : ((#LxExpr#) == [-6-] ? "Pendente de Consulta" : ((#LxExpr#) == [-15-] ? "Recebimento de NF-e de Entrada" : ((#LxExpr#) == [-3-] ? "Solicitando XML" : ((#LxExpr#) == [-1-] ? "Solicitar XML" : ""))))))))))))))))</LX_STATUS_NFE>	
    public partial class LX_STATUS_NFE
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("14", "Aguardando Informações"); 
				    
					result.Add("8", "Autorizado"); 
				    
					result.Add("5", "Autorizando"); 
				    
					result.Add("11", "Cancelado"); 
				    
					result.Add("7", "Consultando"); 
				    
					result.Add("13", "Denegado"); 
				    
					result.Add("9", "Erro na Comunicação com o MID-e"); 
				    
					result.Add("10", "Erro da SEFAZ"); 
				    
					result.Add("2", "Erro na Solicitação do XML"); 
				    
					result.Add("12", "Inutilizado"); 
				    
					result.Add("16", "NF não Eletrônica"); 
				    
					result.Add("4", "Pendente de Autorização"); 
				    
					result.Add("6", "Pendente de Consulta"); 
				    
					result.Add("15", "Recebimento de NF-e de Entrada"); 
				    
					result.Add("3", "Solicitando XML"); 
				    
					result.Add("1", "Solicitar XML"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("14", "AguardandoInformacoes"); 
				    
					result.Add("8", "Autorizado"); 
				    
					result.Add("5", "Autorizando"); 
				    
					result.Add("11", "Cancelado"); 
				    
					result.Add("7", "Consultando"); 
				    
					result.Add("13", "Denegado"); 
				    
					result.Add("9", "ErroComunicacaoMID"); 
				    
					result.Add("10", "ErroSEFAZ"); 
				    
					result.Add("2", "ErroSolicitarXML"); 
				    
					result.Add("12", "Inutilizado"); 
				    
					result.Add("16", "NfNaoEletronica"); 
				    
					result.Add("4", "PendenteAutorizacao"); 
				    
					result.Add("6", "PendenteConsulta"); 
				    
					result.Add("15", "RecebimentoNFeEntrada"); 
				    
					result.Add("3", "SolicitandoXML"); 
				    
					result.Add("1", "SolicitarXML"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _AguardandoInformacoes = new DomainKeyPair() { Value = "14", DisplayName = "Aguardando Informações" };
					[FunctionalPoint("Value[14];DisplayName[Aguardando Informações]")]
					public static DomainKeyPair AguardandoInformacoes { get { return _AguardandoInformacoes; } }
				    
					private static DomainKeyPair _Autorizado = new DomainKeyPair() { Value = "8", DisplayName = "Autorizado" };
					[FunctionalPoint("Value[8];DisplayName[Autorizado]")]
					public static DomainKeyPair Autorizado { get { return _Autorizado; } }
				    
					private static DomainKeyPair _Autorizando = new DomainKeyPair() { Value = "5", DisplayName = "Autorizando" };
					[FunctionalPoint("Value[5];DisplayName[Autorizando]")]
					public static DomainKeyPair Autorizando { get { return _Autorizando; } }
				    
					private static DomainKeyPair _Cancelado = new DomainKeyPair() { Value = "11", DisplayName = "Cancelado" };
					[FunctionalPoint("Value[11];DisplayName[Cancelado]")]
					public static DomainKeyPair Cancelado { get { return _Cancelado; } }
				    
					private static DomainKeyPair _Consultando = new DomainKeyPair() { Value = "7", DisplayName = "Consultando" };
					[FunctionalPoint("Value[7];DisplayName[Consultando]")]
					public static DomainKeyPair Consultando { get { return _Consultando; } }
				    
					private static DomainKeyPair _Denegado = new DomainKeyPair() { Value = "13", DisplayName = "Denegado" };
					[FunctionalPoint("Value[13];DisplayName[Denegado]")]
					public static DomainKeyPair Denegado { get { return _Denegado; } }
				    
					private static DomainKeyPair _ErroComunicacaoMID = new DomainKeyPair() { Value = "9", DisplayName = "Erro na Comunicação com o MID-e" };
					[FunctionalPoint("Value[9];DisplayName[Erro na Comunicação com o MID-e]")]
					public static DomainKeyPair ErroComunicacaoMID { get { return _ErroComunicacaoMID; } }
				    
					private static DomainKeyPair _ErroSEFAZ = new DomainKeyPair() { Value = "10", DisplayName = "Erro da SEFAZ" };
					[FunctionalPoint("Value[10];DisplayName[Erro da SEFAZ]")]
					public static DomainKeyPair ErroSEFAZ { get { return _ErroSEFAZ; } }
				    
					private static DomainKeyPair _ErroSolicitarXML = new DomainKeyPair() { Value = "2", DisplayName = "Erro na Solicitação do XML" };
					[FunctionalPoint("Value[2];DisplayName[Erro na Solicitação do XML]")]
					public static DomainKeyPair ErroSolicitarXML { get { return _ErroSolicitarXML; } }
				    
					private static DomainKeyPair _Inutilizado = new DomainKeyPair() { Value = "12", DisplayName = "Inutilizado" };
					[FunctionalPoint("Value[12];DisplayName[Inutilizado]")]
					public static DomainKeyPair Inutilizado { get { return _Inutilizado; } }
				    
					private static DomainKeyPair _NfNaoEletronica = new DomainKeyPair() { Value = "16", DisplayName = "NF não Eletrônica" };
					[FunctionalPoint("Value[16];DisplayName[NF não Eletrônica]")]
					public static DomainKeyPair NfNaoEletronica { get { return _NfNaoEletronica; } }
				    
					private static DomainKeyPair _PendenteAutorizacao = new DomainKeyPair() { Value = "4", DisplayName = "Pendente de Autorização" };
					[FunctionalPoint("Value[4];DisplayName[Pendente de Autorização]")]
					public static DomainKeyPair PendenteAutorizacao { get { return _PendenteAutorizacao; } }
				    
					private static DomainKeyPair _PendenteConsulta = new DomainKeyPair() { Value = "6", DisplayName = "Pendente de Consulta" };
					[FunctionalPoint("Value[6];DisplayName[Pendente de Consulta]")]
					public static DomainKeyPair PendenteConsulta { get { return _PendenteConsulta; } }
				    
					private static DomainKeyPair _RecebimentoNFeEntrada = new DomainKeyPair() { Value = "15", DisplayName = "Recebimento de NF-e de Entrada" };
					[FunctionalPoint("Value[15];DisplayName[Recebimento de NF-e de Entrada]")]
					public static DomainKeyPair RecebimentoNFeEntrada { get { return _RecebimentoNFeEntrada; } }
				    
					private static DomainKeyPair _SolicitandoXML = new DomainKeyPair() { Value = "3", DisplayName = "Solicitando XML" };
					[FunctionalPoint("Value[3];DisplayName[Solicitando XML]")]
					public static DomainKeyPair SolicitandoXML { get { return _SolicitandoXML; } }
				    
					private static DomainKeyPair _SolicitarXML = new DomainKeyPair() { Value = "1", DisplayName = "Solicitar XML" };
					[FunctionalPoint("Value[1];DisplayName[Solicitar XML]")]
					public static DomainKeyPair SolicitarXML { get { return _SolicitarXML; } }
				    
			#endregion properties

			

	}    
	//<LX_TIPO_PARCEIRO>((#LxExpr#) == [-1-] ? "Envio de e-mails" : ((#LxExpr#) == [-3-] ? "Envio de mala direta" : ((#LxExpr#) == [-2-] ? "Envio de SMS" : ((#LxExpr#) == [-6-] ? "Fornecedor de Brinde" : ((#LxExpr#) == [-4-] ? "Fornecedor de  Gift Card de conteúdo" : ((#LxExpr#) == [-5-] ? "Limpeza de dados" : ""))))))</LX_TIPO_PARCEIRO>	
    public partial class LX_TIPO_PARCEIRO
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Envio de e-mails"); 
				    
					result.Add("3", "Envio de mala direta"); 
				    
					result.Add("2", "Envio de SMS"); 
				    
					result.Add("6", "Fornecedor de Brinde"); 
				    
					result.Add("4", "Fornecedor de  Gift Card de conteúdo"); 
				    
					result.Add("5", "Limpeza de dados"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "EnvioEmail"); 
				    
					result.Add("3", "EnvioMalaDireta"); 
				    
					result.Add("2", "EnvioSMS"); 
				    
					result.Add("6", "FornecedorBrinde"); 
				    
					result.Add("4", "FornecedorGiftCard"); 
				    
					result.Add("5", "LimpezaDados"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _EnvioEmail = new DomainKeyPair() { Value = "1", DisplayName = "Envio de e-mails" };
					[FunctionalPoint("Value[1];DisplayName[Envio de e-mails]")]
					public static DomainKeyPair EnvioEmail { get { return _EnvioEmail; } }
				    
					private static DomainKeyPair _EnvioMalaDireta = new DomainKeyPair() { Value = "3", DisplayName = "Envio de mala direta" };
					[FunctionalPoint("Value[3];DisplayName[Envio de mala direta]")]
					public static DomainKeyPair EnvioMalaDireta { get { return _EnvioMalaDireta; } }
				    
					private static DomainKeyPair _EnvioSMS = new DomainKeyPair() { Value = "2", DisplayName = "Envio de SMS" };
					[FunctionalPoint("Value[2];DisplayName[Envio de SMS]")]
					public static DomainKeyPair EnvioSMS { get { return _EnvioSMS; } }
				    
					private static DomainKeyPair _FornecedorBrinde = new DomainKeyPair() { Value = "6", DisplayName = "Fornecedor de Brinde" };
					[FunctionalPoint("Value[6];DisplayName[Fornecedor de Brinde]")]
					public static DomainKeyPair FornecedorBrinde { get { return _FornecedorBrinde; } }
				    
					private static DomainKeyPair _FornecedorGiftCard = new DomainKeyPair() { Value = "4", DisplayName = "Fornecedor de  Gift Card de conteúdo" };
					[FunctionalPoint("Value[4];DisplayName[Fornecedor de  Gift Card de conteúdo]")]
					public static DomainKeyPair FornecedorGiftCard { get { return _FornecedorGiftCard; } }
				    
					private static DomainKeyPair _LimpezaDados = new DomainKeyPair() { Value = "5", DisplayName = "Limpeza de dados" };
					[FunctionalPoint("Value[5];DisplayName[Limpeza de dados]")]
					public static DomainKeyPair LimpezaDados { get { return _LimpezaDados; } }
				    
			#endregion properties

			

	}    
	//<LX_STATUS_INTEGRACAO_FISCAL>((#LxExpr#) == [-2-] ? "Integrado" : ((#LxExpr#) == [-1-] ? "Não Integrado" : ((#LxExpr#) == [-3-] ? "Reintegrar" : ((#LxExpr#) == [-9-] ? "Trânsito Integração" : ""))))</LX_STATUS_INTEGRACAO_FISCAL>	
    public partial class LX_STATUS_INTEGRACAO_FISCAL
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "Integrado"); 
				    
					result.Add("1", "Não Integrado"); 
				    
					result.Add("3", "Reintegrar"); 
				    
					result.Add("9", "Trânsito Integração"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "Integrado"); 
				    
					result.Add("1", "NaoIntegrado"); 
				    
					result.Add("3", "Reintegrar"); 
				    
					result.Add("9", "TransitoIntegracao"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Integrado = new DomainKeyPair() { Value = "2", DisplayName = "Integrado" };
					[FunctionalPoint("Value[2];DisplayName[Integrado]")]
					public static DomainKeyPair Integrado { get { return _Integrado; } }
				    
					private static DomainKeyPair _NaoIntegrado = new DomainKeyPair() { Value = "1", DisplayName = "Não Integrado" };
					[FunctionalPoint("Value[1];DisplayName[Não Integrado]")]
					public static DomainKeyPair NaoIntegrado { get { return _NaoIntegrado; } }
				    
					private static DomainKeyPair _Reintegrar = new DomainKeyPair() { Value = "3", DisplayName = "Reintegrar" };
					[FunctionalPoint("Value[3];DisplayName[Reintegrar]")]
					public static DomainKeyPair Reintegrar { get { return _Reintegrar; } }
				    
					private static DomainKeyPair _TransitoIntegracao = new DomainKeyPair() { Value = "9", DisplayName = "Trânsito Integração" };
					[FunctionalPoint("Value[9];DisplayName[Trânsito Integração]")]
					public static DomainKeyPair TransitoIntegracao { get { return _TransitoIntegracao; } }
				    
			#endregion properties

			

	}    
	//<LX_ORIGEM_DESCONTO>((#LxExpr#) == [-6-] ? "Fidelidade" : ((#LxExpr#) == [-4-] ? "Manual" : ((#LxExpr#) == [-5-] ? "Operação" : ((#LxExpr#) == [-1-] ? "Pagamento" : ((#LxExpr#) == [-2-] ? "Promoção" : ((#LxExpr#) == [-3-] ? "Promoção Brinde" : ((#LxExpr#) == [-7-] ? "Tabela de Preço" : "")))))))</LX_ORIGEM_DESCONTO>	
    public partial class LX_ORIGEM_DESCONTO
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("6", "Fidelidade"); 
				    
					result.Add("4", "Manual"); 
				    
					result.Add("5", "Operação"); 
				    
					result.Add("1", "Pagamento"); 
				    
					result.Add("2", "Promoção"); 
				    
					result.Add("3", "Promoção Brinde"); 
				    
					result.Add("7", "Tabela de Preço"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("6", "Fidelidade"); 
				    
					result.Add("4", "Manual"); 
				    
					result.Add("5", "Operacao"); 
				    
					result.Add("1", "Pagamento"); 
				    
					result.Add("2", "Promocao"); 
				    
					result.Add("3", "PromocaoBrinde"); 
				    
					result.Add("7", "TabelaPreco"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Fidelidade = new DomainKeyPair() { Value = "6", DisplayName = "Fidelidade" };
					[FunctionalPoint("Value[6];DisplayName[Fidelidade]")]
					public static DomainKeyPair Fidelidade { get { return _Fidelidade; } }
				    
					private static DomainKeyPair _Manual = new DomainKeyPair() { Value = "4", DisplayName = "Manual" };
					[FunctionalPoint("Value[4];DisplayName[Manual]")]
					public static DomainKeyPair Manual { get { return _Manual; } }
				    
					private static DomainKeyPair _Operacao = new DomainKeyPair() { Value = "5", DisplayName = "Operação" };
					[FunctionalPoint("Value[5];DisplayName[Operação]")]
					public static DomainKeyPair Operacao { get { return _Operacao; } }
				    
					private static DomainKeyPair _Pagamento = new DomainKeyPair() { Value = "1", DisplayName = "Pagamento" };
					[FunctionalPoint("Value[1];DisplayName[Pagamento]")]
					public static DomainKeyPair Pagamento { get { return _Pagamento; } }
				    
					private static DomainKeyPair _Promocao = new DomainKeyPair() { Value = "2", DisplayName = "Promoção" };
					[FunctionalPoint("Value[2];DisplayName[Promoção]")]
					public static DomainKeyPair Promocao { get { return _Promocao; } }
				    
					private static DomainKeyPair _PromocaoBrinde = new DomainKeyPair() { Value = "3", DisplayName = "Promoção Brinde" };
					[FunctionalPoint("Value[3];DisplayName[Promoção Brinde]")]
					public static DomainKeyPair PromocaoBrinde { get { return _PromocaoBrinde; } }
				    
					private static DomainKeyPair _TabelaPreco = new DomainKeyPair() { Value = "7", DisplayName = "Tabela de Preço" };
					[FunctionalPoint("Value[7];DisplayName[Tabela de Preço]")]
					public static DomainKeyPair TabelaPreco { get { return _TabelaPreco; } }
				    
			#endregion properties

			

	}    
	//<LX_OPERADOR>((#LxExpr#) == [-3-] ? "(" : ((#LxExpr#) == [-1-] ? "E" : ((#LxExpr#) == [-4-] ? "E (" : ((#LxExpr#) == [-6-] ? ")" : ((#LxExpr#) == [-7-] ? ") E" : ((#LxExpr#) == [-8-] ? ") OU" : ((#LxExpr#) == [-2-] ? "OU" : ((#LxExpr#) == [-5-] ? "OU (" : ""))))))))</LX_OPERADOR>	
    public partial class LX_OPERADOR
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("3", "("); 
				    
					result.Add("1", "E"); 
				    
					result.Add("4", "E ("); 
				    
					result.Add("6", ")"); 
				    
					result.Add("7", ") E"); 
				    
					result.Add("8", ") OU"); 
				    
					result.Add("2", "OU"); 
				    
					result.Add("5", "OU ("); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("3", "OperadorAbreParentese"); 
				    
					result.Add("1", "OperadorE"); 
				    
					result.Add("4", "OperadorE_AbreParentese"); 
				    
					result.Add("6", "OperadorFechaParentese"); 
				    
					result.Add("7", "OperadorFechaParentese_E"); 
				    
					result.Add("8", "OperadorFechaParentese_OU"); 
				    
					result.Add("2", "OperadorOU"); 
				    
					result.Add("5", "OperadorOU_AbreParentese"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _OperadorAbreParentese = new DomainKeyPair() { Value = "3", DisplayName = "(" };
					[FunctionalPoint("Value[3];DisplayName[(]")]
					public static DomainKeyPair OperadorAbreParentese { get { return _OperadorAbreParentese; } }
				    
					private static DomainKeyPair _OperadorE = new DomainKeyPair() { Value = "1", DisplayName = "E" };
					[FunctionalPoint("Value[1];DisplayName[E]")]
					public static DomainKeyPair OperadorE { get { return _OperadorE; } }
				    
					private static DomainKeyPair _OperadorE_AbreParentese = new DomainKeyPair() { Value = "4", DisplayName = "E (" };
					[FunctionalPoint("Value[4];DisplayName[E (]")]
					public static DomainKeyPair OperadorE_AbreParentese { get { return _OperadorE_AbreParentese; } }
				    
					private static DomainKeyPair _OperadorFechaParentese = new DomainKeyPair() { Value = "6", DisplayName = ")" };
					[FunctionalPoint("Value[6];DisplayName[)]")]
					public static DomainKeyPair OperadorFechaParentese { get { return _OperadorFechaParentese; } }
				    
					private static DomainKeyPair _OperadorFechaParentese_E = new DomainKeyPair() { Value = "7", DisplayName = ") E" };
					[FunctionalPoint("Value[7];DisplayName[) E]")]
					public static DomainKeyPair OperadorFechaParentese_E { get { return _OperadorFechaParentese_E; } }
				    
					private static DomainKeyPair _OperadorFechaParentese_OU = new DomainKeyPair() { Value = "8", DisplayName = ") OU" };
					[FunctionalPoint("Value[8];DisplayName[) OU]")]
					public static DomainKeyPair OperadorFechaParentese_OU { get { return _OperadorFechaParentese_OU; } }
				    
					private static DomainKeyPair _OperadorOU = new DomainKeyPair() { Value = "2", DisplayName = "OU" };
					[FunctionalPoint("Value[2];DisplayName[OU]")]
					public static DomainKeyPair OperadorOU { get { return _OperadorOU; } }
				    
					private static DomainKeyPair _OperadorOU_AbreParentese = new DomainKeyPair() { Value = "5", DisplayName = "OU (" };
					[FunctionalPoint("Value[5];DisplayName[OU (]")]
					public static DomainKeyPair OperadorOU_AbreParentese { get { return _OperadorOU_AbreParentese; } }
				    
			#endregion properties

			

	}    
	//<LX_TIPO_CODIGO_BARRA>((#LxExpr#) == [-2-] ? "GTIN-12" : ((#LxExpr#) == [-3-] ? "GTIN-13" : ((#LxExpr#) == [-4-] ? "GTIN-14" : ((#LxExpr#) == [-1-] ? "GTIN-8" : ((#LxExpr#) == [-9-] ? "Próprio" : "")))))</LX_TIPO_CODIGO_BARRA>	
    public partial class LX_TIPO_CODIGO_BARRA
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "GTIN-12"); 
				    
					result.Add("3", "GTIN-13"); 
				    
					result.Add("4", "GTIN-14"); 
				    
					result.Add("1", "GTIN-8"); 
				    
					result.Add("9", "Próprio"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "GTIN12"); 
				    
					result.Add("3", "GTIN13"); 
				    
					result.Add("4", "GTIN14"); 
				    
					result.Add("1", "GTIN8"); 
				    
					result.Add("9", "Proprio"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _GTIN12 = new DomainKeyPair() { Value = "2", DisplayName = "GTIN-12" };
					[FunctionalPoint("Value[2];DisplayName[GTIN-12]")]
					public static DomainKeyPair GTIN12 { get { return _GTIN12; } }
				    
					private static DomainKeyPair _GTIN13 = new DomainKeyPair() { Value = "3", DisplayName = "GTIN-13" };
					[FunctionalPoint("Value[3];DisplayName[GTIN-13]")]
					public static DomainKeyPair GTIN13 { get { return _GTIN13; } }
				    
					private static DomainKeyPair _GTIN14 = new DomainKeyPair() { Value = "4", DisplayName = "GTIN-14" };
					[FunctionalPoint("Value[4];DisplayName[GTIN-14]")]
					public static DomainKeyPair GTIN14 { get { return _GTIN14; } }
				    
					private static DomainKeyPair _GTIN8 = new DomainKeyPair() { Value = "1", DisplayName = "GTIN-8" };
					[FunctionalPoint("Value[1];DisplayName[GTIN-8]")]
					public static DomainKeyPair GTIN8 { get { return _GTIN8; } }
				    
					private static DomainKeyPair _Proprio = new DomainKeyPair() { Value = "9", DisplayName = "Próprio" };
					[FunctionalPoint("Value[9];DisplayName[Próprio]")]
					public static DomainKeyPair Proprio { get { return _Proprio; } }
				    
			#endregion properties

			

	}    
	//<LX_TIPO_FISCAL>((#LxExpr#) == [-104-] ? "Desenvolvimento" : ((#LxExpr#) == [-100-] ? "ECF" : ((#LxExpr#) == [-99-] ? "Não se Aplica" : ((#LxExpr#) == [-101-] ? "NFCe" : ((#LxExpr#) == [-102-] ? "NFe" : ((#LxExpr#) == [-103-] ? "Pré Venda" : ((#LxExpr#) == [-105-] ? "SAT" : "")))))))</LX_TIPO_FISCAL>	
    public partial class LX_TIPO_FISCAL
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("104", "Desenvolvimento"); 
				    
					result.Add("100", "ECF"); 
				    
					result.Add("99", "Não se Aplica"); 
				    
					result.Add("101", "NFCe"); 
				    
					result.Add("102", "NFe"); 
				    
					result.Add("103", "Pré Venda"); 
				    
					result.Add("105", "SAT"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("104", "Desenvolvimento"); 
				    
					result.Add("100", "ECF"); 
				    
					result.Add("99", "NaoSeAplica"); 
				    
					result.Add("101", "NFCe"); 
				    
					result.Add("102", "NFe"); 
				    
					result.Add("103", "PreVenda"); 
				    
					result.Add("105", "SAT"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Desenvolvimento = new DomainKeyPair() { Value = "104", DisplayName = "Desenvolvimento" };
					[FunctionalPoint("Value[104];DisplayName[Desenvolvimento]")]
					public static DomainKeyPair Desenvolvimento { get { return _Desenvolvimento; } }
				    
					private static DomainKeyPair _ECF = new DomainKeyPair() { Value = "100", DisplayName = "ECF" };
					[FunctionalPoint("Value[100];DisplayName[ECF]")]
					public static DomainKeyPair ECF { get { return _ECF; } }
				    
					private static DomainKeyPair _NaoSeAplica = new DomainKeyPair() { Value = "99", DisplayName = "Não se Aplica" };
					[FunctionalPoint("Value[99];DisplayName[Não se Aplica]")]
					public static DomainKeyPair NaoSeAplica { get { return _NaoSeAplica; } }
				    
					private static DomainKeyPair _NFCe = new DomainKeyPair() { Value = "101", DisplayName = "NFCe" };
					[FunctionalPoint("Value[101];DisplayName[NFCe]")]
					public static DomainKeyPair NFCe { get { return _NFCe; } }
				    
					private static DomainKeyPair _NFe = new DomainKeyPair() { Value = "102", DisplayName = "NFe" };
					[FunctionalPoint("Value[102];DisplayName[NFe]")]
					public static DomainKeyPair NFe { get { return _NFe; } }
				    
					private static DomainKeyPair _PreVenda = new DomainKeyPair() { Value = "103", DisplayName = "Pré Venda" };
					[FunctionalPoint("Value[103];DisplayName[Pré Venda]")]
					public static DomainKeyPair PreVenda { get { return _PreVenda; } }
				    
					private static DomainKeyPair _SAT = new DomainKeyPair() { Value = "105", DisplayName = "SAT" };
					[FunctionalPoint("Value[105];DisplayName[SAT]")]
					public static DomainKeyPair SAT { get { return _SAT; } }
				    
			#endregion properties

			

	}    
	//<LX_TIPO_ENDERECO>((#LxExpr#) == [-3-] ? "Coleta" : ((#LxExpr#) == [-2-] ? "Comercial" : ((#LxExpr#) == [-4-] ? "Entrega" : ((#LxExpr#) == [-1-] ? "Residencial" : ""))))</LX_TIPO_ENDERECO>	
    public partial class LX_TIPO_ENDERECO
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("3", "Coleta"); 
				    
					result.Add("2", "Comercial"); 
				    
					result.Add("4", "Entrega"); 
				    
					result.Add("1", "Residencial"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("3", "Coleta"); 
				    
					result.Add("2", "Comercial"); 
				    
					result.Add("4", "Entrega"); 
				    
					result.Add("1", "Residencial"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Coleta = new DomainKeyPair() { Value = "3", DisplayName = "Coleta" };
					[FunctionalPoint("Value[3];DisplayName[Coleta]")]
					public static DomainKeyPair Coleta { get { return _Coleta; } }
				    
					private static DomainKeyPair _Comercial = new DomainKeyPair() { Value = "2", DisplayName = "Comercial" };
					[FunctionalPoint("Value[2];DisplayName[Comercial]")]
					public static DomainKeyPair Comercial { get { return _Comercial; } }
				    
					private static DomainKeyPair _Entrega = new DomainKeyPair() { Value = "4", DisplayName = "Entrega" };
					[FunctionalPoint("Value[4];DisplayName[Entrega]")]
					public static DomainKeyPair Entrega { get { return _Entrega; } }
				    
					private static DomainKeyPair _Residencial = new DomainKeyPair() { Value = "1", DisplayName = "Residencial" };
					[FunctionalPoint("Value[1];DisplayName[Residencial]")]
					public static DomainKeyPair Residencial { get { return _Residencial; } }
				    
			#endregion properties

			

	}    
	//<LX_PEDIDO_ORIGEM>((#LxExpr#) == [-1-] ? "Caixa" : ((#LxExpr#) == [-0-] ? "Indefinido" : ((#LxExpr#) == [-2-] ? "Microterminal" : ((#LxExpr#) == [-3-] ? "Mobile" : ""))))</LX_PEDIDO_ORIGEM>	
    public partial class LX_PEDIDO_ORIGEM
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Caixa"); 
				    
					result.Add("0", "Indefinido"); 
				    
					result.Add("2", "Microterminal"); 
				    
					result.Add("3", "Mobile"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "CAIXA"); 
				    
					result.Add("0", "INDEFINIDO"); 
				    
					result.Add("2", "MICROTERMINAL"); 
				    
					result.Add("3", "MOBILE"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _CAIXA = new DomainKeyPair() { Value = "1", DisplayName = "Caixa" };
					[FunctionalPoint("Value[1];DisplayName[Caixa]")]
					public static DomainKeyPair CAIXA { get { return _CAIXA; } }
				    
					private static DomainKeyPair _INDEFINIDO = new DomainKeyPair() { Value = "0", DisplayName = "Indefinido" };
					[FunctionalPoint("Value[0];DisplayName[Indefinido]")]
					public static DomainKeyPair INDEFINIDO { get { return _INDEFINIDO; } }
				    
					private static DomainKeyPair _MICROTERMINAL = new DomainKeyPair() { Value = "2", DisplayName = "Microterminal" };
					[FunctionalPoint("Value[2];DisplayName[Microterminal]")]
					public static DomainKeyPair MICROTERMINAL { get { return _MICROTERMINAL; } }
				    
					private static DomainKeyPair _MOBILE = new DomainKeyPair() { Value = "3", DisplayName = "Mobile" };
					[FunctionalPoint("Value[3];DisplayName[Mobile]")]
					public static DomainKeyPair MOBILE { get { return _MOBILE; } }
				    
			#endregion properties

			

	}    
	//<LX_ATENDIMENTO_ORIGEM>((#LxExpr#) == [-1-] ? "Caixa" : ((#LxExpr#) == [-0-] ? "Indefinido" : ((#LxExpr#) == [-2-] ? "Mobile" : "")))</LX_ATENDIMENTO_ORIGEM>	
    public partial class LX_ATENDIMENTO_ORIGEM
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Caixa"); 
				    
					result.Add("0", "Indefinido"); 
				    
					result.Add("2", "Mobile"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "CAIXA"); 
				    
					result.Add("0", "INDEFINIDO"); 
				    
					result.Add("2", "MOBILE"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _CAIXA = new DomainKeyPair() { Value = "1", DisplayName = "Caixa" };
					[FunctionalPoint("Value[1];DisplayName[Caixa]")]
					public static DomainKeyPair CAIXA { get { return _CAIXA; } }
				    
					private static DomainKeyPair _INDEFINIDO = new DomainKeyPair() { Value = "0", DisplayName = "Indefinido" };
					[FunctionalPoint("Value[0];DisplayName[Indefinido]")]
					public static DomainKeyPair INDEFINIDO { get { return _INDEFINIDO; } }
				    
					private static DomainKeyPair _MOBILE = new DomainKeyPair() { Value = "2", DisplayName = "Mobile" };
					[FunctionalPoint("Value[2];DisplayName[Mobile]")]
					public static DomainKeyPair MOBILE { get { return _MOBILE; } }
				    
			#endregion properties

			

	}    
	//<TipoTransacao>((#LxExpr#) == [-2-] ? "ERP" : ((#LxExpr#) == [-4-] ? "Excel" : ((#LxExpr#) == [-3-] ? "Loja" : ((#LxExpr#) == [-5-] ? "Mobile" : ((#LxExpr#) == [-1-] ? "Todos" : "")))))</TipoTransacao>	
    public partial class TipoTransacao
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "ERP"); 
				    
					result.Add("4", "Excel"); 
				    
					result.Add("3", "Loja"); 
				    
					result.Add("5", "Mobile"); 
				    
					result.Add("1", "Todos"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "ERP"); 
				    
					result.Add("4", "Excel"); 
				    
					result.Add("3", "Loja"); 
				    
					result.Add("5", "Mobile"); 
				    
					result.Add("1", "Todos"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _ERP = new DomainKeyPair() { Value = "2", DisplayName = "ERP" };
					[FunctionalPoint("Value[2];DisplayName[ERP]")]
					public static DomainKeyPair ERP { get { return _ERP; } }
				    
					private static DomainKeyPair _Excel = new DomainKeyPair() { Value = "4", DisplayName = "Excel" };
					[FunctionalPoint("Value[4];DisplayName[Excel]")]
					public static DomainKeyPair Excel { get { return _Excel; } }
				    
					private static DomainKeyPair _Loja = new DomainKeyPair() { Value = "3", DisplayName = "Loja" };
					[FunctionalPoint("Value[3];DisplayName[Loja]")]
					public static DomainKeyPair Loja { get { return _Loja; } }
				    
					private static DomainKeyPair _Mobile = new DomainKeyPair() { Value = "5", DisplayName = "Mobile" };
					[FunctionalPoint("Value[5];DisplayName[Mobile]")]
					public static DomainKeyPair Mobile { get { return _Mobile; } }
				    
					private static DomainKeyPair _Todos = new DomainKeyPair() { Value = "1", DisplayName = "Todos" };
					[FunctionalPoint("Value[1];DisplayName[Todos]")]
					public static DomainKeyPair Todos { get { return _Todos; } }
				    
			#endregion properties

			

	}    
	//<RegraAcesso>((#LxExpr#) == [-1-] ? "Acesso Bloqueado" : ((#LxExpr#) == [-2-] ? "Acesso Total" : ((#LxExpr#) == [-5-] ? "Alterar" : ((#LxExpr#) == [-12-] ? "Criar Pesquisa" : ((#LxExpr#) == [-10-] ? "Criar Relatório" : ((#LxExpr#) == [-6-] ? "Excluir" : ((#LxExpr#) == [-9-] ? "Exportar" : ((#LxExpr#) == [-8-] ? "Imprimir" : ((#LxExpr#) == [-4-] ? "Incluir" : ((#LxExpr#) == [-11-] ? "Layout" : ((#LxExpr#) == [-7-] ? "Pesquisa Especial" : ((#LxExpr#) == [-3-] ? "Pesquisar" : ((#LxExpr#) == [-99-] ? "Regra Transação" : "")))))))))))))</RegraAcesso>	
    public partial class RegraAcesso
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Acesso Bloqueado"); 
				    
					result.Add("2", "Acesso Total"); 
				    
					result.Add("5", "Alterar"); 
				    
					result.Add("12", "Criar Pesquisa"); 
				    
					result.Add("10", "Criar Relatório"); 
				    
					result.Add("6", "Excluir"); 
				    
					result.Add("9", "Exportar"); 
				    
					result.Add("8", "Imprimir"); 
				    
					result.Add("4", "Incluir"); 
				    
					result.Add("11", "Layout"); 
				    
					result.Add("7", "Pesquisa Especial"); 
				    
					result.Add("3", "Pesquisar"); 
				    
					result.Add("99", "Regra Transação"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "AcessoBloqueado"); 
				    
					result.Add("2", "AcessoTotal"); 
				    
					result.Add("5", "Alterar"); 
				    
					result.Add("12", "CriarPesquisa"); 
				    
					result.Add("10", "CriarRelatorio"); 
				    
					result.Add("6", "Excluir"); 
				    
					result.Add("9", "Exportar"); 
				    
					result.Add("8", "Imprimir"); 
				    
					result.Add("4", "Incluir"); 
				    
					result.Add("11", "Layout"); 
				    
					result.Add("7", "PesquisaEspecial"); 
				    
					result.Add("3", "Pesquisar"); 
				    
					result.Add("99", "RegraTransacao"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _AcessoBloqueado = new DomainKeyPair() { Value = "1", DisplayName = "Acesso Bloqueado" };
					[FunctionalPoint("Value[1];DisplayName[Acesso Bloqueado]")]
					public static DomainKeyPair AcessoBloqueado { get { return _AcessoBloqueado; } }
				    
					private static DomainKeyPair _AcessoTotal = new DomainKeyPair() { Value = "2", DisplayName = "Acesso Total" };
					[FunctionalPoint("Value[2];DisplayName[Acesso Total]")]
					public static DomainKeyPair AcessoTotal { get { return _AcessoTotal; } }
				    
					private static DomainKeyPair _Alterar = new DomainKeyPair() { Value = "5", DisplayName = "Alterar" };
					[FunctionalPoint("Value[5];DisplayName[Alterar]")]
					public static DomainKeyPair Alterar { get { return _Alterar; } }
				    
					private static DomainKeyPair _CriarPesquisa = new DomainKeyPair() { Value = "12", DisplayName = "Criar Pesquisa" };
					[FunctionalPoint("Value[12];DisplayName[Criar Pesquisa]")]
					public static DomainKeyPair CriarPesquisa { get { return _CriarPesquisa; } }
				    
					private static DomainKeyPair _CriarRelatorio = new DomainKeyPair() { Value = "10", DisplayName = "Criar Relatório" };
					[FunctionalPoint("Value[10];DisplayName[Criar Relatório]")]
					public static DomainKeyPair CriarRelatorio { get { return _CriarRelatorio; } }
				    
					private static DomainKeyPair _Excluir = new DomainKeyPair() { Value = "6", DisplayName = "Excluir" };
					[FunctionalPoint("Value[6];DisplayName[Excluir]")]
					public static DomainKeyPair Excluir { get { return _Excluir; } }
				    
					private static DomainKeyPair _Exportar = new DomainKeyPair() { Value = "9", DisplayName = "Exportar" };
					[FunctionalPoint("Value[9];DisplayName[Exportar]")]
					public static DomainKeyPair Exportar { get { return _Exportar; } }
				    
					private static DomainKeyPair _Imprimir = new DomainKeyPair() { Value = "8", DisplayName = "Imprimir" };
					[FunctionalPoint("Value[8];DisplayName[Imprimir]")]
					public static DomainKeyPair Imprimir { get { return _Imprimir; } }
				    
					private static DomainKeyPair _Incluir = new DomainKeyPair() { Value = "4", DisplayName = "Incluir" };
					[FunctionalPoint("Value[4];DisplayName[Incluir]")]
					public static DomainKeyPair Incluir { get { return _Incluir; } }
				    
					private static DomainKeyPair _Layout = new DomainKeyPair() { Value = "11", DisplayName = "Layout" };
					[FunctionalPoint("Value[11];DisplayName[Layout]")]
					public static DomainKeyPair Layout { get { return _Layout; } }
				    
					private static DomainKeyPair _PesquisaEspecial = new DomainKeyPair() { Value = "7", DisplayName = "Pesquisa Especial" };
					[FunctionalPoint("Value[7];DisplayName[Pesquisa Especial]")]
					public static DomainKeyPair PesquisaEspecial { get { return _PesquisaEspecial; } }
				    
					private static DomainKeyPair _Pesquisar = new DomainKeyPair() { Value = "3", DisplayName = "Pesquisar" };
					[FunctionalPoint("Value[3];DisplayName[Pesquisar]")]
					public static DomainKeyPair Pesquisar { get { return _Pesquisar; } }
				    
					private static DomainKeyPair _RegraTransacao = new DomainKeyPair() { Value = "99", DisplayName = "Regra Transação" };
					[FunctionalPoint("Value[99];DisplayName[Regra Transação]")]
					public static DomainKeyPair RegraTransacao { get { return _RegraTransacao; } }
				    
			#endregion properties

			

	}    
	//<RegraAcessoColuna>((#LxExpr#) == [-1-] ? "Acesso Bloqueado" : ((#LxExpr#) == [-2-] ? "Acesso Total" : ((#LxExpr#) == [-4-] ? "Alterar" : ((#LxExpr#) == [-5-] ? "Pesquisar" : ((#LxExpr#) == [-99-] ? "Regra Transação" : ((#LxExpr#) == [-3-] ? "Visualizar" : ""))))))</RegraAcessoColuna>	
    public partial class RegraAcessoColuna
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Acesso Bloqueado"); 
				    
					result.Add("2", "Acesso Total"); 
				    
					result.Add("4", "Alterar"); 
				    
					result.Add("5", "Pesquisar"); 
				    
					result.Add("99", "Regra Transação"); 
				    
					result.Add("3", "Visualizar"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "AcessoBloqueado"); 
				    
					result.Add("2", "AcessoTotal"); 
				    
					result.Add("4", "Alterar"); 
				    
					result.Add("5", "Pesquisar"); 
				    
					result.Add("99", "RegraTransacao"); 
				    
					result.Add("3", "Visualizar"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _AcessoBloqueado = new DomainKeyPair() { Value = "1", DisplayName = "Acesso Bloqueado" };
					[FunctionalPoint("Value[1];DisplayName[Acesso Bloqueado]")]
					public static DomainKeyPair AcessoBloqueado { get { return _AcessoBloqueado; } }
				    
					private static DomainKeyPair _AcessoTotal = new DomainKeyPair() { Value = "2", DisplayName = "Acesso Total" };
					[FunctionalPoint("Value[2];DisplayName[Acesso Total]")]
					public static DomainKeyPair AcessoTotal { get { return _AcessoTotal; } }
				    
					private static DomainKeyPair _Alterar = new DomainKeyPair() { Value = "4", DisplayName = "Alterar" };
					[FunctionalPoint("Value[4];DisplayName[Alterar]")]
					public static DomainKeyPair Alterar { get { return _Alterar; } }
				    
					private static DomainKeyPair _Pesquisar = new DomainKeyPair() { Value = "5", DisplayName = "Pesquisar" };
					[FunctionalPoint("Value[5];DisplayName[Pesquisar]")]
					public static DomainKeyPair Pesquisar { get { return _Pesquisar; } }
				    
					private static DomainKeyPair _RegraTransacao = new DomainKeyPair() { Value = "99", DisplayName = "Regra Transação" };
					[FunctionalPoint("Value[99];DisplayName[Regra Transação]")]
					public static DomainKeyPair RegraTransacao { get { return _RegraTransacao; } }
				    
					private static DomainKeyPair _Visualizar = new DomainKeyPair() { Value = "3", DisplayName = "Visualizar" };
					[FunctionalPoint("Value[3];DisplayName[Visualizar]")]
					public static DomainKeyPair Visualizar { get { return _Visualizar; } }
				    
			#endregion properties

			

	}    
	//<TipoObjeto>((#LxExpr#) == [-1-] ? "BO" : ((#LxExpr#) == [-3-] ? "Campo" : ((#LxExpr#) == [-10-] ? "Filtro" : ((#LxExpr#) == [-9-] ? "Layout" : ((#LxExpr#) == [-6-] ? "Relatório" : ((#LxExpr#) == [-5-] ? "Stored Procedure" : ((#LxExpr#) == [-8-] ? "Template de ação de Workflow" : ((#LxExpr#) == [-2-] ? "Transação" : ((#LxExpr#) == [-4-] ? "Trigger" : ((#LxExpr#) == [-11-] ? "Extensão (Objeto de entrada)" : ((#LxExpr#) == [-7-] ? "Workflow" : "")))))))))))</TipoObjeto>	
    public partial class TipoObjeto
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "BO"); 
				    
					result.Add("3", "Campo"); 
				    
					result.Add("10", "Filtro"); 
				    
					result.Add("9", "Layout"); 
				    
					result.Add("6", "Relatório"); 
				    
					result.Add("5", "Stored Procedure"); 
				    
					result.Add("8", "Template de ação de Workflow"); 
				    
					result.Add("2", "Transação"); 
				    
					result.Add("4", "Trigger"); 
				    
					result.Add("11", "Extensão (Objeto de entrada)"); 
				    
					result.Add("7", "Workflow"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "BO"); 
				    
					result.Add("3", "Campo"); 
				    
					result.Add("10", "Filtro"); 
				    
					result.Add("9", "Layout"); 
				    
					result.Add("6", "Relatorio"); 
				    
					result.Add("5", "StoredProcedure"); 
				    
					result.Add("8", "TemplateAcaoWF"); 
				    
					result.Add("2", "Transacao"); 
				    
					result.Add("4", "Trigger"); 
				    
					result.Add("11", "UserExtension"); 
				    
					result.Add("7", "Workflow"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _BO = new DomainKeyPair() { Value = "1", DisplayName = "BO" };
					[FunctionalPoint("Value[1];DisplayName[BO]")]
					public static DomainKeyPair BO { get { return _BO; } }
				    
					private static DomainKeyPair _Campo = new DomainKeyPair() { Value = "3", DisplayName = "Campo" };
					[FunctionalPoint("Value[3];DisplayName[Campo]")]
					public static DomainKeyPair Campo { get { return _Campo; } }
				    
					private static DomainKeyPair _Filtro = new DomainKeyPair() { Value = "10", DisplayName = "Filtro" };
					[FunctionalPoint("Value[10];DisplayName[Filtro]")]
					public static DomainKeyPair Filtro { get { return _Filtro; } }
				    
					private static DomainKeyPair _Layout = new DomainKeyPair() { Value = "9", DisplayName = "Layout" };
					[FunctionalPoint("Value[9];DisplayName[Layout]")]
					public static DomainKeyPair Layout { get { return _Layout; } }
				    
					private static DomainKeyPair _Relatorio = new DomainKeyPair() { Value = "6", DisplayName = "Relatório" };
					[FunctionalPoint("Value[6];DisplayName[Relatório]")]
					public static DomainKeyPair Relatorio { get { return _Relatorio; } }
				    
					private static DomainKeyPair _StoredProcedure = new DomainKeyPair() { Value = "5", DisplayName = "Stored Procedure" };
					[FunctionalPoint("Value[5];DisplayName[Stored Procedure]")]
					public static DomainKeyPair StoredProcedure { get { return _StoredProcedure; } }
				    
					private static DomainKeyPair _TemplateAcaoWF = new DomainKeyPair() { Value = "8", DisplayName = "Template de ação de Workflow" };
					[FunctionalPoint("Value[8];DisplayName[Template de ação de Workflow]")]
					public static DomainKeyPair TemplateAcaoWF { get { return _TemplateAcaoWF; } }
				    
					private static DomainKeyPair _Transacao = new DomainKeyPair() { Value = "2", DisplayName = "Transação" };
					[FunctionalPoint("Value[2];DisplayName[Transação]")]
					public static DomainKeyPair Transacao { get { return _Transacao; } }
				    
					private static DomainKeyPair _Trigger = new DomainKeyPair() { Value = "4", DisplayName = "Trigger" };
					[FunctionalPoint("Value[4];DisplayName[Trigger]")]
					public static DomainKeyPair Trigger { get { return _Trigger; } }
				    
					private static DomainKeyPair _UserExtension = new DomainKeyPair() { Value = "11", DisplayName = "Extensão (Objeto de entrada)" };
					[FunctionalPoint("Value[11];DisplayName[Extensão (Objeto de entrada)]")]
					public static DomainKeyPair UserExtension { get { return _UserExtension; } }
				    
					private static DomainKeyPair _Workflow = new DomainKeyPair() { Value = "7", DisplayName = "Workflow" };
					[FunctionalPoint("Value[7];DisplayName[Workflow]")]
					public static DomainKeyPair Workflow { get { return _Workflow; } }
				    
			#endregion properties

			

	}    
	//<TipoValidacaoParametro>((#LxExpr#) == [-8-] ? "Sem Validação" : ((#LxExpr#) == [-2-] ? "Validação Contra Tabela (Combo)" : ((#LxExpr#) == [-3-] ? "Validação Contra Faixa" : ((#LxExpr#) == [-4-] ? "Validação Contra Objeto CRM" : ((#LxExpr#) == [-1-] ? "Validação Contra Tabela (Valida)" : "")))))</TipoValidacaoParametro>	
    public partial class TipoValidacaoParametro
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("8", "Sem Validação"); 
				    
					result.Add("2", "Validação Contra Tabela (Combo)"); 
				    
					result.Add("3", "Validação Contra Faixa"); 
				    
					result.Add("4", "Validação Contra Objeto CRM"); 
				    
					result.Add("1", "Validação Contra Tabela (Valida)"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("8", "SemValidacao"); 
				    
					result.Add("2", "ValidacaoCombo"); 
				    
					result.Add("3", "ValidacaoFaixa"); 
				    
					result.Add("4", "ValidacaoObjetoCRM"); 
				    
					result.Add("1", "ValidacaoTabela"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _SemValidacao = new DomainKeyPair() { Value = "8", DisplayName = "Sem Validação" };
					[FunctionalPoint("Value[8];DisplayName[Sem Validação]")]
					public static DomainKeyPair SemValidacao { get { return _SemValidacao; } }
				    
					private static DomainKeyPair _ValidacaoCombo = new DomainKeyPair() { Value = "2", DisplayName = "Validação Contra Tabela (Combo)" };
					[FunctionalPoint("Value[2];DisplayName[Validação Contra Tabela (Combo)]")]
					public static DomainKeyPair ValidacaoCombo { get { return _ValidacaoCombo; } }
				    
					private static DomainKeyPair _ValidacaoFaixa = new DomainKeyPair() { Value = "3", DisplayName = "Validação Contra Faixa" };
					[FunctionalPoint("Value[3];DisplayName[Validação Contra Faixa]")]
					public static DomainKeyPair ValidacaoFaixa { get { return _ValidacaoFaixa; } }
				    
					private static DomainKeyPair _ValidacaoObjetoCRM = new DomainKeyPair() { Value = "4", DisplayName = "Validação Contra Objeto CRM" };
					[FunctionalPoint("Value[4];DisplayName[Validação Contra Objeto CRM]")]
					public static DomainKeyPair ValidacaoObjetoCRM { get { return _ValidacaoObjetoCRM; } }
				    
					private static DomainKeyPair _ValidacaoTabela = new DomainKeyPair() { Value = "1", DisplayName = "Validação Contra Tabela (Valida)" };
					[FunctionalPoint("Value[1];DisplayName[Validação Contra Tabela (Valida)]")]
					public static DomainKeyPair ValidacaoTabela { get { return _ValidacaoTabela; } }
				    
			#endregion properties

			

	}    
	//<TipoValorParametro>((#LxExpr#) == [-2-] ? "Caractere" : ((#LxExpr#) == [-3-] ? "Data" : ((#LxExpr#) == [-4-] ? "Lógico" : ((#LxExpr#) == [-1-] ? "Numérico" : ""))))</TipoValorParametro>	
    public partial class TipoValorParametro
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "Caractere"); 
				    
					result.Add("3", "Data"); 
				    
					result.Add("4", "Lógico"); 
				    
					result.Add("1", "Numérico"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "Caractere"); 
				    
					result.Add("3", "Data"); 
				    
					result.Add("4", "Logico"); 
				    
					result.Add("1", "Numerico"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Caractere = new DomainKeyPair() { Value = "2", DisplayName = "Caractere" };
					[FunctionalPoint("Value[2];DisplayName[Caractere]")]
					public static DomainKeyPair Caractere { get { return _Caractere; } }
				    
					private static DomainKeyPair _Data = new DomainKeyPair() { Value = "3", DisplayName = "Data" };
					[FunctionalPoint("Value[3];DisplayName[Data]")]
					public static DomainKeyPair Data { get { return _Data; } }
				    
					private static DomainKeyPair _Logico = new DomainKeyPair() { Value = "4", DisplayName = "Lógico" };
					[FunctionalPoint("Value[4];DisplayName[Lógico]")]
					public static DomainKeyPair Logico { get { return _Logico; } }
				    
					private static DomainKeyPair _Numerico = new DomainKeyPair() { Value = "1", DisplayName = "Numérico" };
					[FunctionalPoint("Value[1];DisplayName[Numérico]")]
					public static DomainKeyPair Numerico { get { return _Numerico; } }
				    
			#endregion properties

			

	}    
	//<TipoDocumento>((#LxExpr#) == [-3-] ? "Detalhe/Estampa" : ((#LxExpr#) == [-4-] ? "360°" : ((#LxExpr#) == [-2-] ? "Matriz Para Transformação" : ((#LxExpr#) == [-1-] ? "Normal" : ((#LxExpr#) == [-5-] ? "Vídeos" : "")))))</TipoDocumento>	
    public partial class TipoDocumento
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("3", "Detalhe/Estampa"); 
				    
					result.Add("4", "360°"); 
				    
					result.Add("2", "Matriz Para Transformação"); 
				    
					result.Add("1", "Normal"); 
				    
					result.Add("5", "Vídeos"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("3", "Detalhe_Estampa"); 
				    
					result.Add("4", "Imagem_360"); 
				    
					result.Add("2", "Matriz_Transformacao"); 
				    
					result.Add("1", "Normal"); 
				    
					result.Add("5", "Vídeos"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Detalhe_Estampa = new DomainKeyPair() { Value = "3", DisplayName = "Detalhe/Estampa" };
					[FunctionalPoint("Value[3];DisplayName[Detalhe/Estampa]")]
					public static DomainKeyPair Detalhe_Estampa { get { return _Detalhe_Estampa; } }
				    
					private static DomainKeyPair _Imagem_360 = new DomainKeyPair() { Value = "4", DisplayName = "360°" };
					[FunctionalPoint("Value[4];DisplayName[360°]")]
					public static DomainKeyPair Imagem_360 { get { return _Imagem_360; } }
				    
					private static DomainKeyPair _Matriz_Transformacao = new DomainKeyPair() { Value = "2", DisplayName = "Matriz Para Transformação" };
					[FunctionalPoint("Value[2];DisplayName[Matriz Para Transformação]")]
					public static DomainKeyPair Matriz_Transformacao { get { return _Matriz_Transformacao; } }
				    
					private static DomainKeyPair _Normal = new DomainKeyPair() { Value = "1", DisplayName = "Normal" };
					[FunctionalPoint("Value[1];DisplayName[Normal]")]
					public static DomainKeyPair Normal { get { return _Normal; } }
				    
					private static DomainKeyPair _Vídeos = new DomainKeyPair() { Value = "5", DisplayName = "Vídeos" };
					[FunctionalPoint("Value[5];DisplayName[Vídeos]")]
					public static DomainKeyPair Vídeos { get { return _Vídeos; } }
				    
			#endregion properties

			

	}    
	//<TipoExtensao>((#LxExpr#) == [-1-] ? "JPEG" : ((#LxExpr#) == [-2-] ? "JPG" : ((#LxExpr#) == [-3-] ? "PNG" : ((#LxExpr#) == [-4-] ? "WMV" : ""))))</TipoExtensao>	
    public partial class TipoExtensao
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "JPEG"); 
				    
					result.Add("2", "JPG"); 
				    
					result.Add("3", "PNG"); 
				    
					result.Add("4", "WMV"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "JPEG"); 
				    
					result.Add("2", "JPG"); 
				    
					result.Add("3", "PNG"); 
				    
					result.Add("4", "WMV"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _JPEG = new DomainKeyPair() { Value = "1", DisplayName = "JPEG" };
					[FunctionalPoint("Value[1];DisplayName[JPEG]")]
					public static DomainKeyPair JPEG { get { return _JPEG; } }
				    
					private static DomainKeyPair _JPG = new DomainKeyPair() { Value = "2", DisplayName = "JPG" };
					[FunctionalPoint("Value[2];DisplayName[JPG]")]
					public static DomainKeyPair JPG { get { return _JPG; } }
				    
					private static DomainKeyPair _PNG = new DomainKeyPair() { Value = "3", DisplayName = "PNG" };
					[FunctionalPoint("Value[3];DisplayName[PNG]")]
					public static DomainKeyPair PNG { get { return _PNG; } }
				    
					private static DomainKeyPair _WMV = new DomainKeyPair() { Value = "4", DisplayName = "WMV" };
					[FunctionalPoint("Value[4];DisplayName[WMV]")]
					public static DomainKeyPair WMV { get { return _WMV; } }
				    
			#endregion properties

			

	}    
	//<TipoArquivo>((#LxExpr#) == [-E-] ? "Excel" : ((#LxExpr#) == [-T-] ? "Text" : ((#LxExpr#) == [-G-] ? "Todos" : ((#LxExpr#) == [-X-] ? "XML" : ""))))</TipoArquivo>	
    public partial class TipoArquivo
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("E", "Excel"); 
				    
					result.Add("T", "Text"); 
				    
					result.Add("G", "Todos"); 
				    
					result.Add("X", "XML"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("E", "Excel"); 
				    
					result.Add("T", "Text"); 
				    
					result.Add("G", "Todos"); 
				    
					result.Add("X", "Xml"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Excel = new DomainKeyPair() { Value = "E", DisplayName = "Excel" };
					[FunctionalPoint("Value[E];DisplayName[Excel]")]
					public static DomainKeyPair Excel { get { return _Excel; } }
				    
					private static DomainKeyPair _Text = new DomainKeyPair() { Value = "T", DisplayName = "Text" };
					[FunctionalPoint("Value[T];DisplayName[Text]")]
					public static DomainKeyPair Text { get { return _Text; } }
				    
					private static DomainKeyPair _Todos = new DomainKeyPair() { Value = "G", DisplayName = "Todos" };
					[FunctionalPoint("Value[G];DisplayName[Todos]")]
					public static DomainKeyPair Todos { get { return _Todos; } }
				    
					private static DomainKeyPair _Xml = new DomainKeyPair() { Value = "X", DisplayName = "XML" };
					[FunctionalPoint("Value[X];DisplayName[XML]")]
					public static DomainKeyPair Xml { get { return _Xml; } }
				    
			#endregion properties

			

	}    
	//<TipoDado>((#LxExpr#) == [-BLN-] ? "Boolean" : ((#LxExpr#) == [-BYT-] ? "Byte" : ((#LxExpr#) == [-DTE-] ? "Date" : ((#LxExpr#) == [-DEC-] ? "Decimal" : ((#LxExpr#) == [-DBL-] ? "Double" : ((#LxExpr#) == [-INT-] ? "Integer" : ((#LxExpr#) == [-LNG-] ? "Long" : ((#LxExpr#) == [-POS-] ? "PositiveInteger" : ((#LxExpr#) == [-STR-] ? "String" : ((#LxExpr#) == [-TME-] ? "Time" : ""))))))))))</TipoDado>	
    public partial class TipoDado
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("BLN", "Boolean"); 
				    
					result.Add("BYT", "Byte"); 
				    
					result.Add("DTE", "Date"); 
				    
					result.Add("DEC", "Decimal"); 
				    
					result.Add("DBL", "Double"); 
				    
					result.Add("INT", "Integer"); 
				    
					result.Add("LNG", "Long"); 
				    
					result.Add("POS", "PositiveInteger"); 
				    
					result.Add("STR", "String"); 
				    
					result.Add("TME", "Time"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("BLN", "BOOLEAN"); 
				    
					result.Add("BYT", "BYTE"); 
				    
					result.Add("DTE", "DATE"); 
				    
					result.Add("DEC", "DECIMAL"); 
				    
					result.Add("DBL", "DOUBLE"); 
				    
					result.Add("INT", "INTEGER"); 
				    
					result.Add("LNG", "LONG"); 
				    
					result.Add("POS", "POSITIVEINTEGER"); 
				    
					result.Add("STR", "STRING"); 
				    
					result.Add("TME", "TIME"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _BOOLEAN = new DomainKeyPair() { Value = "BLN", DisplayName = "Boolean" };
					[FunctionalPoint("Value[BLN];DisplayName[Boolean]")]
					public static DomainKeyPair BOOLEAN { get { return _BOOLEAN; } }
				    
					private static DomainKeyPair _BYTE = new DomainKeyPair() { Value = "BYT", DisplayName = "Byte" };
					[FunctionalPoint("Value[BYT];DisplayName[Byte]")]
					public static DomainKeyPair BYTE { get { return _BYTE; } }
				    
					private static DomainKeyPair _DATE = new DomainKeyPair() { Value = "DTE", DisplayName = "Date" };
					[FunctionalPoint("Value[DTE];DisplayName[Date]")]
					public static DomainKeyPair DATE { get { return _DATE; } }
				    
					private static DomainKeyPair _DECIMAL = new DomainKeyPair() { Value = "DEC", DisplayName = "Decimal" };
					[FunctionalPoint("Value[DEC];DisplayName[Decimal]")]
					public static DomainKeyPair DECIMAL { get { return _DECIMAL; } }
				    
					private static DomainKeyPair _DOUBLE = new DomainKeyPair() { Value = "DBL", DisplayName = "Double" };
					[FunctionalPoint("Value[DBL];DisplayName[Double]")]
					public static DomainKeyPair DOUBLE { get { return _DOUBLE; } }
				    
					private static DomainKeyPair _INTEGER = new DomainKeyPair() { Value = "INT", DisplayName = "Integer" };
					[FunctionalPoint("Value[INT];DisplayName[Integer]")]
					public static DomainKeyPair INTEGER { get { return _INTEGER; } }
				    
					private static DomainKeyPair _LONG = new DomainKeyPair() { Value = "LNG", DisplayName = "Long" };
					[FunctionalPoint("Value[LNG];DisplayName[Long]")]
					public static DomainKeyPair LONG { get { return _LONG; } }
				    
					private static DomainKeyPair _POSITIVEINTEGER = new DomainKeyPair() { Value = "POS", DisplayName = "PositiveInteger" };
					[FunctionalPoint("Value[POS];DisplayName[PositiveInteger]")]
					public static DomainKeyPair POSITIVEINTEGER { get { return _POSITIVEINTEGER; } }
				    
					private static DomainKeyPair _STRING = new DomainKeyPair() { Value = "STR", DisplayName = "String" };
					[FunctionalPoint("Value[STR];DisplayName[String]")]
					public static DomainKeyPair STRING { get { return _STRING; } }
				    
					private static DomainKeyPair _TIME = new DomainKeyPair() { Value = "TME", DisplayName = "Time" };
					[FunctionalPoint("Value[TME];DisplayName[Time]")]
					public static DomainKeyPair TIME { get { return _TIME; } }
				    
			#endregion properties

			

	}    
	//<TipoLog>((#LxExpr#) == [-2-] ? "Geração de Arquivo" : ((#LxExpr#) == [-3-] ? "Importação de Layout" : ((#LxExpr#) == [-1-] ? "Leitura de Arquivo" : "")))</TipoLog>	
    public partial class TipoLog
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "Geração de Arquivo"); 
				    
					result.Add("3", "Importação de Layout"); 
				    
					result.Add("1", "Leitura de Arquivo"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "GeracaoArquivo"); 
				    
					result.Add("3", "ImportacaoLayout"); 
				    
					result.Add("1", "LeituraArquivo"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _GeracaoArquivo = new DomainKeyPair() { Value = "2", DisplayName = "Geração de Arquivo" };
					[FunctionalPoint("Value[2];DisplayName[Geração de Arquivo]")]
					public static DomainKeyPair GeracaoArquivo { get { return _GeracaoArquivo; } }
				    
					private static DomainKeyPair _ImportacaoLayout = new DomainKeyPair() { Value = "3", DisplayName = "Importação de Layout" };
					[FunctionalPoint("Value[3];DisplayName[Importação de Layout]")]
					public static DomainKeyPair ImportacaoLayout { get { return _ImportacaoLayout; } }
				    
					private static DomainKeyPair _LeituraArquivo = new DomainKeyPair() { Value = "1", DisplayName = "Leitura de Arquivo" };
					[FunctionalPoint("Value[1];DisplayName[Leitura de Arquivo]")]
					public static DomainKeyPair LeituraArquivo { get { return _LeituraArquivo; } }
				    
			#endregion properties

			

	}    
	//<FormatoData>((#LxExpr#) == [-1-] ? "AAAAMMDD" : ((#LxExpr#) == [-4-] ? "AAMMDD" : ((#LxExpr#) == [-5-] ? "DDMMAA" : ((#LxExpr#) == [-2-] ? "DDMMAAAA" : ((#LxExpr#) == [-6-] ? "MMDDAA" : ((#LxExpr#) == [-3-] ? "MMDDAAAA" : ""))))))</FormatoData>	
    public partial class FormatoData
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "AAAAMMDD"); 
				    
					result.Add("4", "AAMMDD"); 
				    
					result.Add("5", "DDMMAA"); 
				    
					result.Add("2", "DDMMAAAA"); 
				    
					result.Add("6", "MMDDAA"); 
				    
					result.Add("3", "MMDDAAAA"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "AAAAMMDD"); 
				    
					result.Add("4", "AAMMDD"); 
				    
					result.Add("5", "DDMMAA"); 
				    
					result.Add("2", "DDMMAAAA"); 
				    
					result.Add("6", "MMDDAA"); 
				    
					result.Add("3", "MMDDAAAA"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _AAAAMMDD = new DomainKeyPair() { Value = "1", DisplayName = "AAAAMMDD" };
					[FunctionalPoint("Value[1];DisplayName[AAAAMMDD]")]
					public static DomainKeyPair AAAAMMDD { get { return _AAAAMMDD; } }
				    
					private static DomainKeyPair _AAMMDD = new DomainKeyPair() { Value = "4", DisplayName = "AAMMDD" };
					[FunctionalPoint("Value[4];DisplayName[AAMMDD]")]
					public static DomainKeyPair AAMMDD { get { return _AAMMDD; } }
				    
					private static DomainKeyPair _DDMMAA = new DomainKeyPair() { Value = "5", DisplayName = "DDMMAA" };
					[FunctionalPoint("Value[5];DisplayName[DDMMAA]")]
					public static DomainKeyPair DDMMAA { get { return _DDMMAA; } }
				    
					private static DomainKeyPair _DDMMAAAA = new DomainKeyPair() { Value = "2", DisplayName = "DDMMAAAA" };
					[FunctionalPoint("Value[2];DisplayName[DDMMAAAA]")]
					public static DomainKeyPair DDMMAAAA { get { return _DDMMAAAA; } }
				    
					private static DomainKeyPair _MMDDAA = new DomainKeyPair() { Value = "6", DisplayName = "MMDDAA" };
					[FunctionalPoint("Value[6];DisplayName[MMDDAA]")]
					public static DomainKeyPair MMDDAA { get { return _MMDDAA; } }
				    
					private static DomainKeyPair _MMDDAAAA = new DomainKeyPair() { Value = "3", DisplayName = "MMDDAAAA" };
					[FunctionalPoint("Value[3];DisplayName[MMDDAAAA]")]
					public static DomainKeyPair MMDDAAAA { get { return _MMDDAAAA; } }
				    
			#endregion properties

			

	}    
	//<TipoConteudoObjeto>((#LxExpr#) == [-1-] ? "Layout" : ((#LxExpr#) == [-2-] ? "Mídia" : ""))</TipoConteudoObjeto>	
    public partial class TipoConteudoObjeto
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Layout"); 
				    
					result.Add("2", "Mídia"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Layout"); 
				    
					result.Add("2", "Media"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Layout = new DomainKeyPair() { Value = "1", DisplayName = "Layout" };
					[FunctionalPoint("Value[1];DisplayName[Layout]")]
					public static DomainKeyPair Layout { get { return _Layout; } }
				    
					private static DomainKeyPair _Media = new DomainKeyPair() { Value = "2", DisplayName = "Mídia" };
					[FunctionalPoint("Value[2];DisplayName[Mídia]")]
					public static DomainKeyPair Media { get { return _Media; } }
				    
			#endregion properties

			

	}    
	//<TipoLayout>((#LxExpr#) == [-1-] ? "Layout do Sistema" : ((#LxExpr#) == [-2-] ? "Layout do Usuário" : ""))</TipoLayout>	
    public partial class TipoLayout
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Layout do Sistema"); 
				    
					result.Add("2", "Layout do Usuário"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "SystemLayout"); 
				    
					result.Add("2", "UserLayout"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _SystemLayout = new DomainKeyPair() { Value = "1", DisplayName = "Layout do Sistema" };
					[FunctionalPoint("Value[1];DisplayName[Layout do Sistema]")]
					public static DomainKeyPair SystemLayout { get { return _SystemLayout; } }
				    
					private static DomainKeyPair _UserLayout = new DomainKeyPair() { Value = "2", DisplayName = "Layout do Usuário" };
					[FunctionalPoint("Value[2];DisplayName[Layout do Usuário]")]
					public static DomainKeyPair UserLayout { get { return _UserLayout; } }
				    
			#endregion properties

			

	}    
	//<PosicaoDaTransacao>((#LxExpr#) == [-5-] ? "Painel Inferior" : ((#LxExpr#) == [-6-] ? "Painel Flutuante" : ((#LxExpr#) == [-2-] ? "Painel à Esquerda" : ((#LxExpr#) == [-1-] ? "Página" : ((#LxExpr#) == [-4-] ? "Painel à Direita" : ((#LxExpr#) == [-3-] ? "Painel Superior" : ""))))))</PosicaoDaTransacao>	
    public partial class PosicaoDaTransacao
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("5", "Painel Inferior"); 
				    
					result.Add("6", "Painel Flutuante"); 
				    
					result.Add("2", "Painel à Esquerda"); 
				    
					result.Add("1", "Página"); 
				    
					result.Add("4", "Painel à Direita"); 
				    
					result.Add("3", "Painel Superior"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("5", "Bottom"); 
				    
					result.Add("6", "Floating"); 
				    
					result.Add("2", "Left"); 
				    
					result.Add("1", "None"); 
				    
					result.Add("4", "Right"); 
				    
					result.Add("3", "Top"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Bottom = new DomainKeyPair() { Value = "5", DisplayName = "Painel Inferior" };
					[FunctionalPoint("Value[5];DisplayName[Painel Inferior]")]
					public static DomainKeyPair Bottom { get { return _Bottom; } }
				    
					private static DomainKeyPair _Floating = new DomainKeyPair() { Value = "6", DisplayName = "Painel Flutuante" };
					[FunctionalPoint("Value[6];DisplayName[Painel Flutuante]")]
					public static DomainKeyPair Floating { get { return _Floating; } }
				    
					private static DomainKeyPair _Left = new DomainKeyPair() { Value = "2", DisplayName = "Painel à Esquerda" };
					[FunctionalPoint("Value[2];DisplayName[Painel à Esquerda]")]
					public static DomainKeyPair Left { get { return _Left; } }
				    
					private static DomainKeyPair _None = new DomainKeyPair() { Value = "1", DisplayName = "Página" };
					[FunctionalPoint("Value[1];DisplayName[Página]")]
					public static DomainKeyPair None { get { return _None; } }
				    
					private static DomainKeyPair _Right = new DomainKeyPair() { Value = "4", DisplayName = "Painel à Direita" };
					[FunctionalPoint("Value[4];DisplayName[Painel à Direita]")]
					public static DomainKeyPair Right { get { return _Right; } }
				    
					private static DomainKeyPair _Top = new DomainKeyPair() { Value = "3", DisplayName = "Painel Superior" };
					[FunctionalPoint("Value[3];DisplayName[Painel Superior]")]
					public static DomainKeyPair Top { get { return _Top; } }
				    
			#endregion properties

			

	}    
	//<TipoLayoutDependente>((#LxExpr#) == [-6-] ? "Grade de Dados em Baixo/Formulário em Cima" : ((#LxExpr#) == [-2-] ? "Formulário" : ((#LxExpr#) == [-7-] ? "Padrão" : ((#LxExpr#) == [-1-] ? "Grade de Dados" : ((#LxExpr#) == [-3-] ? "Grade de Dados à Esquerda/Formulário à Direita" : ((#LxExpr#) == [-5-] ? "Grade de Dados à Direita/Formulário à Esquerda" : ((#LxExpr#) == [-4-] ? "Grade de Dados em Cima/Formulário em Baixo" : "")))))))</TipoLayoutDependente>	
    public partial class TipoLayoutDependente
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("6", "Grade de Dados em Baixo/Formulário em Cima"); 
				    
					result.Add("2", "Formulário"); 
				    
					result.Add("7", "Padrão"); 
				    
					result.Add("1", "Grade de Dados"); 
				    
					result.Add("3", "Grade de Dados à Esquerda/Formulário à Direita"); 
				    
					result.Add("5", "Grade de Dados à Direita/Formulário à Esquerda"); 
				    
					result.Add("4", "Grade de Dados em Cima/Formulário em Baixo"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("6", "BottomGridLayout_TopColumnsLayout"); 
				    
					result.Add("2", "ColumnsLayout"); 
				    
					result.Add("7", "Default"); 
				    
					result.Add("1", "GridLayout"); 
				    
					result.Add("3", "LeftGridLayout_RightColumnsLayout"); 
				    
					result.Add("5", "RightGridLayout_LeftColumnsLayout"); 
				    
					result.Add("4", "TopGridLayout_BottomColumnsLayout"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _BottomGridLayout_TopColumnsLayout = new DomainKeyPair() { Value = "6", DisplayName = "Grade de Dados em Baixo/Formulário em Cima" };
					[FunctionalPoint("Value[6];DisplayName[Grade de Dados em Baixo/Formulário em Cima]")]
					public static DomainKeyPair BottomGridLayout_TopColumnsLayout { get { return _BottomGridLayout_TopColumnsLayout; } }
				    
					private static DomainKeyPair _ColumnsLayout = new DomainKeyPair() { Value = "2", DisplayName = "Formulário" };
					[FunctionalPoint("Value[2];DisplayName[Formulário]")]
					public static DomainKeyPair ColumnsLayout { get { return _ColumnsLayout; } }
				    
					private static DomainKeyPair _Default = new DomainKeyPair() { Value = "7", DisplayName = "Padrão" };
					[FunctionalPoint("Value[7];DisplayName[Padrão]")]
					public static DomainKeyPair Default { get { return _Default; } }
				    
					private static DomainKeyPair _GridLayout = new DomainKeyPair() { Value = "1", DisplayName = "Grade de Dados" };
					[FunctionalPoint("Value[1];DisplayName[Grade de Dados]")]
					public static DomainKeyPair GridLayout { get { return _GridLayout; } }
				    
					private static DomainKeyPair _LeftGridLayout_RightColumnsLayout = new DomainKeyPair() { Value = "3", DisplayName = "Grade de Dados à Esquerda/Formulário à Direita" };
					[FunctionalPoint("Value[3];DisplayName[Grade de Dados à Esquerda/Formulário à Direita]")]
					public static DomainKeyPair LeftGridLayout_RightColumnsLayout { get { return _LeftGridLayout_RightColumnsLayout; } }
				    
					private static DomainKeyPair _RightGridLayout_LeftColumnsLayout = new DomainKeyPair() { Value = "5", DisplayName = "Grade de Dados à Direita/Formulário à Esquerda" };
					[FunctionalPoint("Value[5];DisplayName[Grade de Dados à Direita/Formulário à Esquerda]")]
					public static DomainKeyPair RightGridLayout_LeftColumnsLayout { get { return _RightGridLayout_LeftColumnsLayout; } }
				    
					private static DomainKeyPair _TopGridLayout_BottomColumnsLayout = new DomainKeyPair() { Value = "4", DisplayName = "Grade de Dados em Cima/Formulário em Baixo" };
					[FunctionalPoint("Value[4];DisplayName[Grade de Dados em Cima/Formulário em Baixo]")]
					public static DomainKeyPair TopGridLayout_BottomColumnsLayout { get { return _TopGridLayout_BottomColumnsLayout; } }
				    
			#endregion properties

			

	}    
	//<IdAplicativo>((#LxExpr#) == [-10-] ? "Carga Dados CRM" : ((#LxExpr#) == [-14-] ? "Ensemble" : ((#LxExpr#) == [-5-] ? "CRM Mobile" : ((#LxExpr#) == [-6-] ? "ETL" : ((#LxExpr#) == [-8-] ? "Excel" : ((#LxExpr#) == [-7-] ? "Mobile" : ((#LxExpr#) == [-3-] ? "POS" : ((#LxExpr#) == [-13-] ? "Linx Shop" : ((#LxExpr#) == [-1-] ? "UX" : ((#LxExpr#) == [-9-] ? "Sites Loyalty" : ((#LxExpr#) == [-12-] ? "Serviço de Mídias" : ((#LxExpr#) == [-11-] ? "MID" : ((#LxExpr#) == [-15-] ? "Linx Services" : "")))))))))))))</IdAplicativo>	
    public partial class IdAplicativo
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("10", "Carga Dados CRM"); 
				    
					result.Add("14", "Ensemble"); 
				    
					result.Add("5", "CRM Mobile"); 
				    
					result.Add("6", "ETL"); 
				    
					result.Add("8", "Excel"); 
				    
					result.Add("7", "Mobile"); 
				    
					result.Add("3", "POS"); 
				    
					result.Add("13", "Linx Shop"); 
				    
					result.Add("1", "UX"); 
				    
					result.Add("9", "Sites Loyalty"); 
				    
					result.Add("12", "Serviço de Mídias"); 
				    
					result.Add("11", "MID"); 
				    
					result.Add("15", "Linx Services"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("10", "CargaDados"); 
				    
					result.Add("14", "Ensemble"); 
				    
					result.Add("5", "LINXCRMMOBILE"); 
				    
					result.Add("6", "LINXETL"); 
				    
					result.Add("8", "LinxExcel"); 
				    
					result.Add("7", "LinxMobile"); 
				    
					result.Add("3", "LINXPOS"); 
				    
					result.Add("13", "LinxShop"); 
				    
					result.Add("1", "LINXUX"); 
				    
					result.Add("9", "Loyalty"); 
				    
					result.Add("12", "MediaService"); 
				    
					result.Add("11", "MID"); 
				    
					result.Add("15", "MIDServices"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _CargaDados = new DomainKeyPair() { Value = "10", DisplayName = "Carga Dados CRM" };
					[FunctionalPoint("Value[10];DisplayName[Carga Dados CRM]")]
					public static DomainKeyPair CargaDados { get { return _CargaDados; } }
				    
					private static DomainKeyPair _Ensemble = new DomainKeyPair() { Value = "14", DisplayName = "Ensemble" };
					[FunctionalPoint("Value[14];DisplayName[Ensemble]")]
					public static DomainKeyPair Ensemble { get { return _Ensemble; } }
				    
					private static DomainKeyPair _LINXCRMMOBILE = new DomainKeyPair() { Value = "5", DisplayName = "CRM Mobile" };
					[FunctionalPoint("Value[5];DisplayName[CRM Mobile]")]
					public static DomainKeyPair LINXCRMMOBILE { get { return _LINXCRMMOBILE; } }
				    
					private static DomainKeyPair _LINXETL = new DomainKeyPair() { Value = "6", DisplayName = "ETL" };
					[FunctionalPoint("Value[6];DisplayName[ETL]")]
					public static DomainKeyPair LINXETL { get { return _LINXETL; } }
				    
					private static DomainKeyPair _LinxExcel = new DomainKeyPair() { Value = "8", DisplayName = "Excel" };
					[FunctionalPoint("Value[8];DisplayName[Excel]")]
					public static DomainKeyPair LinxExcel { get { return _LinxExcel; } }
				    
					private static DomainKeyPair _LinxMobile = new DomainKeyPair() { Value = "7", DisplayName = "Mobile" };
					[FunctionalPoint("Value[7];DisplayName[Mobile]")]
					public static DomainKeyPair LinxMobile { get { return _LinxMobile; } }
				    
					private static DomainKeyPair _LINXPOS = new DomainKeyPair() { Value = "3", DisplayName = "POS" };
					[FunctionalPoint("Value[3];DisplayName[POS]")]
					public static DomainKeyPair LINXPOS { get { return _LINXPOS; } }
				    
					private static DomainKeyPair _LinxShop = new DomainKeyPair() { Value = "13", DisplayName = "Linx Shop" };
					[FunctionalPoint("Value[13];DisplayName[Linx Shop]")]
					public static DomainKeyPair LinxShop { get { return _LinxShop; } }
				    
					private static DomainKeyPair _LINXUX = new DomainKeyPair() { Value = "1", DisplayName = "UX" };
					[FunctionalPoint("Value[1];DisplayName[UX]")]
					public static DomainKeyPair LINXUX { get { return _LINXUX; } }
				    
					private static DomainKeyPair _Loyalty = new DomainKeyPair() { Value = "9", DisplayName = "Sites Loyalty" };
					[FunctionalPoint("Value[9];DisplayName[Sites Loyalty]")]
					public static DomainKeyPair Loyalty { get { return _Loyalty; } }
				    
					private static DomainKeyPair _MediaService = new DomainKeyPair() { Value = "12", DisplayName = "Serviço de Mídias" };
					[FunctionalPoint("Value[12];DisplayName[Serviço de Mídias]")]
					public static DomainKeyPair MediaService { get { return _MediaService; } }
				    
					private static DomainKeyPair _MID = new DomainKeyPair() { Value = "11", DisplayName = "MID" };
					[FunctionalPoint("Value[11];DisplayName[MID]")]
					public static DomainKeyPair MID { get { return _MID; } }
				    
					private static DomainKeyPair _MIDServices = new DomainKeyPair() { Value = "15", DisplayName = "Linx Services" };
					[FunctionalPoint("Value[15];DisplayName[Linx Services]")]
					public static DomainKeyPair MIDServices { get { return _MIDServices; } }
				    
			#endregion properties

			

	}    
	//<UsoMultimidia>((#LxExpr#) == [-1-] ? "Catálogo" : ((#LxExpr#) == [-2-] ? "Detalhe" : ((#LxExpr#) == [-9-] ? "Look View" : ((#LxExpr#) == [-8-] ? "Matriz Mínima" : ((#LxExpr#) == [-3-] ? "Miniatura" : ((#LxExpr#) == [-5-] ? "Zoom Ampliado" : ((#LxExpr#) == [-4-] ? "Zoom de Lente" : "")))))))</UsoMultimidia>	
    public partial class UsoMultimidia
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Catálogo"); 
				    
					result.Add("2", "Detalhe"); 
				    
					result.Add("9", "Look View"); 
				    
					result.Add("8", "Matriz Mínima"); 
				    
					result.Add("3", "Miniatura"); 
				    
					result.Add("5", "Zoom Ampliado"); 
				    
					result.Add("4", "Zoom de Lente"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Catalogo"); 
				    
					result.Add("2", "Detalhe"); 
				    
					result.Add("9", "LookView"); 
				    
					result.Add("8", "MatrizMinima"); 
				    
					result.Add("3", "Miniatura"); 
				    
					result.Add("5", "ZoomAmpliado"); 
				    
					result.Add("4", "ZoomLente"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Catalogo = new DomainKeyPair() { Value = "1", DisplayName = "Catálogo" };
					[FunctionalPoint("Value[1];DisplayName[Catálogo]")]
					public static DomainKeyPair Catalogo { get { return _Catalogo; } }
				    
					private static DomainKeyPair _Detalhe = new DomainKeyPair() { Value = "2", DisplayName = "Detalhe" };
					[FunctionalPoint("Value[2];DisplayName[Detalhe]")]
					public static DomainKeyPair Detalhe { get { return _Detalhe; } }
				    
					private static DomainKeyPair _LookView = new DomainKeyPair() { Value = "9", DisplayName = "Look View" };
					[FunctionalPoint("Value[9];DisplayName[Look View]")]
					public static DomainKeyPair LookView { get { return _LookView; } }
				    
					private static DomainKeyPair _MatrizMinima = new DomainKeyPair() { Value = "8", DisplayName = "Matriz Mínima" };
					[FunctionalPoint("Value[8];DisplayName[Matriz Mínima]")]
					public static DomainKeyPair MatrizMinima { get { return _MatrizMinima; } }
				    
					private static DomainKeyPair _Miniatura = new DomainKeyPair() { Value = "3", DisplayName = "Miniatura" };
					[FunctionalPoint("Value[3];DisplayName[Miniatura]")]
					public static DomainKeyPair Miniatura { get { return _Miniatura; } }
				    
					private static DomainKeyPair _ZoomAmpliado = new DomainKeyPair() { Value = "5", DisplayName = "Zoom Ampliado" };
					[FunctionalPoint("Value[5];DisplayName[Zoom Ampliado]")]
					public static DomainKeyPair ZoomAmpliado { get { return _ZoomAmpliado; } }
				    
					private static DomainKeyPair _ZoomLente = new DomainKeyPair() { Value = "4", DisplayName = "Zoom de Lente" };
					[FunctionalPoint("Value[4];DisplayName[Zoom de Lente]")]
					public static DomainKeyPair ZoomLente { get { return _ZoomLente; } }
				    
			#endregion properties

			

	}    
	//<TipoFiltro>((#LxExpr#) == [-1-] ? "Filtro BO" : ((#LxExpr#) == [-2-] ? "Filtro EDM (Entity SQL)" : ""))</TipoFiltro>	
    public partial class TipoFiltro
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Filtro BO"); 
				    
					result.Add("2", "Filtro EDM (Entity SQL)"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "FiltroBO"); 
				    
					result.Add("2", "FiltroEdm"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _FiltroBO = new DomainKeyPair() { Value = "1", DisplayName = "Filtro BO" };
					[FunctionalPoint("Value[1];DisplayName[Filtro BO]")]
					public static DomainKeyPair FiltroBO { get { return _FiltroBO; } }
				    
					private static DomainKeyPair _FiltroEdm = new DomainKeyPair() { Value = "2", DisplayName = "Filtro EDM (Entity SQL)" };
					[FunctionalPoint("Value[2];DisplayName[Filtro EDM (Entity SQL)]")]
					public static DomainKeyPair FiltroEdm { get { return _FiltroEdm; } }
				    
			#endregion properties

			

	}    
	//<FilterOperator>((#LxExpr#) == [-BETWEEN-] ? "Between" : ((#LxExpr#) == [->-] ? ">" : ((#LxExpr#) == [->=-] ? ">=" : ((#LxExpr#) == [-IN-] ? "In" : ((#LxExpr#) == [-=-] ? "=" : ((#LxExpr#) == [-IS NOT NULL-] ? "Not Null" : ((#LxExpr#) == [-IS NULL-] ? "Null" : ((#LxExpr#) == [-<-] ? "<" : ((#LxExpr#) == [-<=-] ? "<=" : ((#LxExpr#) == [-LIKE-] ? "Like" : ((#LxExpr#) == [-NOT BETWEEN-] ? "Not Between" : ((#LxExpr#) == [-!=-] ? "!=" : ((#LxExpr#) == [-NOT IN-] ? "Not In" : ((#LxExpr#) == [-NOT LIKE-] ? "Not Like" : ""))))))))))))))</FilterOperator>	
    public partial class FilterOperator
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("BETWEEN", "Between"); 
				    
					result.Add(">", ">"); 
				    
					result.Add(">=", ">="); 
				    
					result.Add("IN", "In"); 
				    
					result.Add("=", "="); 
				    
					result.Add("IS NOT NULL", "Not Null"); 
				    
					result.Add("IS NULL", "Null"); 
				    
					result.Add("<", "<"); 
				    
					result.Add("<=", "<="); 
				    
					result.Add("LIKE", "Like"); 
				    
					result.Add("NOT BETWEEN", "Not Between"); 
				    
					result.Add("!=", "!="); 
				    
					result.Add("NOT IN", "Not In"); 
				    
					result.Add("NOT LIKE", "Not Like"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("BETWEEN", "Between"); 
				    
					result.Add(">", "GreaterThen"); 
				    
					result.Add(">=", "GreaterThenOrEqualTo"); 
				    
					result.Add("IN", "InList"); 
				    
					result.Add("=", "IsEquals"); 
				    
					result.Add("IS NOT NULL", "IsNotNull"); 
				    
					result.Add("IS NULL", "IsNull"); 
				    
					result.Add("<", "LessThen"); 
				    
					result.Add("<=", "LessThenOrEqualTo"); 
				    
					result.Add("LIKE", "Like"); 
				    
					result.Add("NOT BETWEEN", "NotBetween"); 
				    
					result.Add("!=", "NotEqual"); 
				    
					result.Add("NOT IN", "NotIn"); 
				    
					result.Add("NOT LIKE", "NotLike"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Between = new DomainKeyPair() { Value = "BETWEEN", DisplayName = "Between" };
					[FunctionalPoint("Value[BETWEEN];DisplayName[Between]")]
					public static DomainKeyPair Between { get { return _Between; } }
				    
					private static DomainKeyPair _GreaterThen = new DomainKeyPair() { Value = ">", DisplayName = ">" };
					[FunctionalPoint("Value[>];DisplayName[>]")]
					public static DomainKeyPair GreaterThen { get { return _GreaterThen; } }
				    
					private static DomainKeyPair _GreaterThenOrEqualTo = new DomainKeyPair() { Value = ">=", DisplayName = ">=" };
					[FunctionalPoint("Value[>=];DisplayName[>=]")]
					public static DomainKeyPair GreaterThenOrEqualTo { get { return _GreaterThenOrEqualTo; } }
				    
					private static DomainKeyPair _InList = new DomainKeyPair() { Value = "IN", DisplayName = "In" };
					[FunctionalPoint("Value[IN];DisplayName[In]")]
					public static DomainKeyPair InList { get { return _InList; } }
				    
					private static DomainKeyPair _IsEquals = new DomainKeyPair() { Value = "=", DisplayName = "=" };
					[FunctionalPoint("Value[=];DisplayName[=]")]
					public static DomainKeyPair IsEquals { get { return _IsEquals; } }
				    
					private static DomainKeyPair _IsNotNull = new DomainKeyPair() { Value = "IS NOT NULL", DisplayName = "Not Null" };
					[FunctionalPoint("Value[IS NOT NULL];DisplayName[Not Null]")]
					public static DomainKeyPair IsNotNull { get { return _IsNotNull; } }
				    
					private static DomainKeyPair _IsNull = new DomainKeyPair() { Value = "IS NULL", DisplayName = "Null" };
					[FunctionalPoint("Value[IS NULL];DisplayName[Null]")]
					public static DomainKeyPair IsNull { get { return _IsNull; } }
				    
					private static DomainKeyPair _LessThen = new DomainKeyPair() { Value = "<", DisplayName = "<" };
					[FunctionalPoint("Value[<];DisplayName[<]")]
					public static DomainKeyPair LessThen { get { return _LessThen; } }
				    
					private static DomainKeyPair _LessThenOrEqualTo = new DomainKeyPair() { Value = "<=", DisplayName = "<=" };
					[FunctionalPoint("Value[<=];DisplayName[<=]")]
					public static DomainKeyPair LessThenOrEqualTo { get { return _LessThenOrEqualTo; } }
				    
					private static DomainKeyPair _Like = new DomainKeyPair() { Value = "LIKE", DisplayName = "Like" };
					[FunctionalPoint("Value[LIKE];DisplayName[Like]")]
					public static DomainKeyPair Like { get { return _Like; } }
				    
					private static DomainKeyPair _NotBetween = new DomainKeyPair() { Value = "NOT BETWEEN", DisplayName = "Not Between" };
					[FunctionalPoint("Value[NOT BETWEEN];DisplayName[Not Between]")]
					public static DomainKeyPair NotBetween { get { return _NotBetween; } }
				    
					private static DomainKeyPair _NotEqual = new DomainKeyPair() { Value = "!=", DisplayName = "!=" };
					[FunctionalPoint("Value[!=];DisplayName[!=]")]
					public static DomainKeyPair NotEqual { get { return _NotEqual; } }
				    
					private static DomainKeyPair _NotIn = new DomainKeyPair() { Value = "NOT IN", DisplayName = "Not In" };
					[FunctionalPoint("Value[NOT IN];DisplayName[Not In]")]
					public static DomainKeyPair NotIn { get { return _NotIn; } }
				    
					private static DomainKeyPair _NotLike = new DomainKeyPair() { Value = "NOT LIKE", DisplayName = "Not Like" };
					[FunctionalPoint("Value[NOT LIKE];DisplayName[Not Like]")]
					public static DomainKeyPair NotLike { get { return _NotLike; } }
				    
			#endregion properties

			

	}    
	//<FilterCondition>((#LxExpr#) == [-&&-] ? "And" : ((#LxExpr#) == [-!-] ? "Not" : ((#LxExpr#) == [-||-] ? "Or" : "")))</FilterCondition>	
    public partial class FilterCondition
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("&&", "And"); 
				    
					result.Add("!", "Not"); 
				    
					result.Add("||", "Or"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("&&", "And"); 
				    
					result.Add("!", "Not"); 
				    
					result.Add("||", "Or"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _And = new DomainKeyPair() { Value = "&&", DisplayName = "And" };
					[FunctionalPoint("Value[&&];DisplayName[And]")]
					public static DomainKeyPair And { get { return _And; } }
				    
					private static DomainKeyPair _Not = new DomainKeyPair() { Value = "!", DisplayName = "Not" };
					[FunctionalPoint("Value[!];DisplayName[Not]")]
					public static DomainKeyPair Not { get { return _Not; } }
				    
					private static DomainKeyPair _Or = new DomainKeyPair() { Value = "||", DisplayName = "Or" };
					[FunctionalPoint("Value[||];DisplayName[Or]")]
					public static DomainKeyPair Or { get { return _Or; } }
				    
			#endregion properties

			

	}    
	//<TipoVerboHttp>((#LxExpr#) == [-6-] ? "Copy" : ((#LxExpr#) == [-5-] ? "Delete" : ((#LxExpr#) == [-1-] ? "Get" : ((#LxExpr#) == [-7-] ? "Head" : ((#LxExpr#) == [-9-] ? "Link" : ((#LxExpr#) == [-8-] ? "Options" : ((#LxExpr#) == [-4-] ? "Patch" : ((#LxExpr#) == [-2-] ? "Post" : ((#LxExpr#) == [-11-] ? "Purge" : ((#LxExpr#) == [-3-] ? "Put" : ((#LxExpr#) == [-10-] ? "Unlink" : "")))))))))))</TipoVerboHttp>	
    public partial class TipoVerboHttp
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("6", "Copy"); 
				    
					result.Add("5", "Delete"); 
				    
					result.Add("1", "Get"); 
				    
					result.Add("7", "Head"); 
				    
					result.Add("9", "Link"); 
				    
					result.Add("8", "Options"); 
				    
					result.Add("4", "Patch"); 
				    
					result.Add("2", "Post"); 
				    
					result.Add("11", "Purge"); 
				    
					result.Add("3", "Put"); 
				    
					result.Add("10", "Unlink"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("6", "HttpCopy"); 
				    
					result.Add("5", "HttpDelete"); 
				    
					result.Add("1", "HttpGet"); 
				    
					result.Add("7", "HttpHead"); 
				    
					result.Add("9", "HttpLink"); 
				    
					result.Add("8", "HttpOptions"); 
				    
					result.Add("4", "HttpPatch"); 
				    
					result.Add("2", "HttpPost"); 
				    
					result.Add("11", "HttpPurge"); 
				    
					result.Add("3", "HttpPut"); 
				    
					result.Add("10", "HttpUnlink"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _HttpCopy = new DomainKeyPair() { Value = "6", DisplayName = "Copy" };
					[FunctionalPoint("Value[6];DisplayName[Copy]")]
					public static DomainKeyPair HttpCopy { get { return _HttpCopy; } }
				    
					private static DomainKeyPair _HttpDelete = new DomainKeyPair() { Value = "5", DisplayName = "Delete" };
					[FunctionalPoint("Value[5];DisplayName[Delete]")]
					public static DomainKeyPair HttpDelete { get { return _HttpDelete; } }
				    
					private static DomainKeyPair _HttpGet = new DomainKeyPair() { Value = "1", DisplayName = "Get" };
					[FunctionalPoint("Value[1];DisplayName[Get]")]
					public static DomainKeyPair HttpGet { get { return _HttpGet; } }
				    
					private static DomainKeyPair _HttpHead = new DomainKeyPair() { Value = "7", DisplayName = "Head" };
					[FunctionalPoint("Value[7];DisplayName[Head]")]
					public static DomainKeyPair HttpHead { get { return _HttpHead; } }
				    
					private static DomainKeyPair _HttpLink = new DomainKeyPair() { Value = "9", DisplayName = "Link" };
					[FunctionalPoint("Value[9];DisplayName[Link]")]
					public static DomainKeyPair HttpLink { get { return _HttpLink; } }
				    
					private static DomainKeyPair _HttpOptions = new DomainKeyPair() { Value = "8", DisplayName = "Options" };
					[FunctionalPoint("Value[8];DisplayName[Options]")]
					public static DomainKeyPair HttpOptions { get { return _HttpOptions; } }
				    
					private static DomainKeyPair _HttpPatch = new DomainKeyPair() { Value = "4", DisplayName = "Patch" };
					[FunctionalPoint("Value[4];DisplayName[Patch]")]
					public static DomainKeyPair HttpPatch { get { return _HttpPatch; } }
				    
					private static DomainKeyPair _HttpPost = new DomainKeyPair() { Value = "2", DisplayName = "Post" };
					[FunctionalPoint("Value[2];DisplayName[Post]")]
					public static DomainKeyPair HttpPost { get { return _HttpPost; } }
				    
					private static DomainKeyPair _HttpPurge = new DomainKeyPair() { Value = "11", DisplayName = "Purge" };
					[FunctionalPoint("Value[11];DisplayName[Purge]")]
					public static DomainKeyPair HttpPurge { get { return _HttpPurge; } }
				    
					private static DomainKeyPair _HttpPut = new DomainKeyPair() { Value = "3", DisplayName = "Put" };
					[FunctionalPoint("Value[3];DisplayName[Put]")]
					public static DomainKeyPair HttpPut { get { return _HttpPut; } }
				    
					private static DomainKeyPair _HttpUnlink = new DomainKeyPair() { Value = "10", DisplayName = "Unlink" };
					[FunctionalPoint("Value[10];DisplayName[Unlink]")]
					public static DomainKeyPair HttpUnlink { get { return _HttpUnlink; } }
				    
			#endregion properties

			

	}    
	//<TipoProcedimento>((#LxExpr#) == [-2-] ? "Função" : ((#LxExpr#) == [-1-] ? "Procedure" : ""))</TipoProcedimento>	
    public partial class TipoProcedimento
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "Função"); 
				    
					result.Add("1", "Procedure"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "Function"); 
				    
					result.Add("1", "StoredProcedure"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Function = new DomainKeyPair() { Value = "2", DisplayName = "Função" };
					[FunctionalPoint("Value[2];DisplayName[Função]")]
					public static DomainKeyPair Function { get { return _Function; } }
				    
					private static DomainKeyPair _StoredProcedure = new DomainKeyPair() { Value = "1", DisplayName = "Procedure" };
					[FunctionalPoint("Value[1];DisplayName[Procedure]")]
					public static DomainKeyPair StoredProcedure { get { return _StoredProcedure; } }
				    
			#endregion properties

			

	}    
	//<OrigemValorParametro>((#LxExpr#) == [-1-] ? "Informação da Origem" : ((#LxExpr#) == [-2-] ? "Parâmetro do Sistema" : ""))</OrigemValorParametro>	
    public partial class OrigemValorParametro
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Informação da Origem"); 
				    
					result.Add("2", "Parâmetro do Sistema"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Origem"); 
				    
					result.Add("2", "Parametro"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Origem = new DomainKeyPair() { Value = "1", DisplayName = "Informação da Origem" };
					[FunctionalPoint("Value[1];DisplayName[Informação da Origem]")]
					public static DomainKeyPair Origem { get { return _Origem; } }
				    
					private static DomainKeyPair _Parametro = new DomainKeyPair() { Value = "2", DisplayName = "Parâmetro do Sistema" };
					[FunctionalPoint("Value[2];DisplayName[Parâmetro do Sistema]")]
					public static DomainKeyPair Parametro { get { return _Parametro; } }
				    
			#endregion properties

			

	}    
	//<LX_INDICADOR_MERCADORIA>((#LxExpr#) == [-A-] ? "Armamento" : ((#LxExpr#) == [-C-] ? "Combustível e Lubrificante" : ((#LxExpr#) == [-K-] ? "Kit / Embalagem / Lista" : ((#LxExpr#) == [-L-] ? "Look" : ((#LxExpr#) == [-M-] ? "Medicamento" : ((#LxExpr#) == [-P-] ? "Produto" : ((#LxExpr#) == [-S-] ? "Serviço" : ((#LxExpr#) == [-V-] ? "Veículo" : ""))))))))</LX_INDICADOR_MERCADORIA>	
    public partial class LX_INDICADOR_MERCADORIA
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("A", "Armamento"); 
				    
					result.Add("C", "Combustível e Lubrificante"); 
				    
					result.Add("K", "Kit / Embalagem / Lista"); 
				    
					result.Add("L", "Look"); 
				    
					result.Add("M", "Medicamento"); 
				    
					result.Add("P", "Produto"); 
				    
					result.Add("S", "Serviço"); 
				    
					result.Add("V", "Veículo"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("A", "Armamento"); 
				    
					result.Add("C", "Combustivel"); 
				    
					result.Add("K", "KitLista"); 
				    
					result.Add("L", "Look"); 
				    
					result.Add("M", "Medicamento"); 
				    
					result.Add("P", "Produto"); 
				    
					result.Add("S", "Servico"); 
				    
					result.Add("V", "Veiculo"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Armamento = new DomainKeyPair() { Value = "A", DisplayName = "Armamento" };
					[FunctionalPoint("Value[A];DisplayName[Armamento]")]
					public static DomainKeyPair Armamento { get { return _Armamento; } }
				    
					private static DomainKeyPair _Combustivel = new DomainKeyPair() { Value = "C", DisplayName = "Combustível e Lubrificante" };
					[FunctionalPoint("Value[C];DisplayName[Combustível e Lubrificante]")]
					public static DomainKeyPair Combustivel { get { return _Combustivel; } }
				    
					private static DomainKeyPair _KitLista = new DomainKeyPair() { Value = "K", DisplayName = "Kit / Embalagem / Lista" };
					[FunctionalPoint("Value[K];DisplayName[Kit / Embalagem / Lista]")]
					public static DomainKeyPair KitLista { get { return _KitLista; } }
				    
					private static DomainKeyPair _Look = new DomainKeyPair() { Value = "L", DisplayName = "Look" };
					[FunctionalPoint("Value[L];DisplayName[Look]")]
					public static DomainKeyPair Look { get { return _Look; } }
				    
					private static DomainKeyPair _Medicamento = new DomainKeyPair() { Value = "M", DisplayName = "Medicamento" };
					[FunctionalPoint("Value[M];DisplayName[Medicamento]")]
					public static DomainKeyPair Medicamento { get { return _Medicamento; } }
				    
					private static DomainKeyPair _Produto = new DomainKeyPair() { Value = "P", DisplayName = "Produto" };
					[FunctionalPoint("Value[P];DisplayName[Produto]")]
					public static DomainKeyPair Produto { get { return _Produto; } }
				    
					private static DomainKeyPair _Servico = new DomainKeyPair() { Value = "S", DisplayName = "Serviço" };
					[FunctionalPoint("Value[S];DisplayName[Serviço]")]
					public static DomainKeyPair Servico { get { return _Servico; } }
				    
					private static DomainKeyPair _Veiculo = new DomainKeyPair() { Value = "V", DisplayName = "Veículo" };
					[FunctionalPoint("Value[V];DisplayName[Veículo]")]
					public static DomainKeyPair Veiculo { get { return _Veiculo; } }
				    
			#endregion properties

			

	}    
	//<LX_EMBALAGEM>((#LxExpr#) == [-2-] ? "Embalagem" : ((#LxExpr#) == [-3-] ? "Kit" : ((#LxExpr#) == [-4-] ? "Lista de Produtos" : ((#LxExpr#) == [-1-] ? "Único" : ""))))</LX_EMBALAGEM>	
    public partial class LX_EMBALAGEM
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "Embalagem"); 
				    
					result.Add("3", "Kit"); 
				    
					result.Add("4", "Lista de Produtos"); 
				    
					result.Add("1", "Único"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "Embalagem"); 
				    
					result.Add("3", "Kit"); 
				    
					result.Add("4", "Lista"); 
				    
					result.Add("1", "Unico"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Embalagem = new DomainKeyPair() { Value = "2", DisplayName = "Embalagem" };
					[FunctionalPoint("Value[2];DisplayName[Embalagem]")]
					public static DomainKeyPair Embalagem { get { return _Embalagem; } }
				    
					private static DomainKeyPair _Kit = new DomainKeyPair() { Value = "3", DisplayName = "Kit" };
					[FunctionalPoint("Value[3];DisplayName[Kit]")]
					public static DomainKeyPair Kit { get { return _Kit; } }
				    
					private static DomainKeyPair _Lista = new DomainKeyPair() { Value = "4", DisplayName = "Lista de Produtos" };
					[FunctionalPoint("Value[4];DisplayName[Lista de Produtos]")]
					public static DomainKeyPair Lista { get { return _Lista; } }
				    
					private static DomainKeyPair _Unico = new DomainKeyPair() { Value = "1", DisplayName = "Único" };
					[FunctionalPoint("Value[1];DisplayName[Único]")]
					public static DomainKeyPair Unico { get { return _Unico; } }
				    
			#endregion properties

			

	}    
	//<LX_TIPO_MENSAGEM>((#LxExpr#) == [-3-] ? "Cliente não Fidelidade" : ((#LxExpr#) == [-4-] ? "Cliente não identificado na venda" : ((#LxExpr#) == [-1-] ? "Cliente Fidelidade que pontuou na venda" : ((#LxExpr#) == [-2-] ? "Cliente Fidelidade que resgatou pontos na venda" : ""))))</LX_TIPO_MENSAGEM>	
    public partial class LX_TIPO_MENSAGEM
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("3", "Cliente não Fidelidade"); 
				    
					result.Add("4", "Cliente não identificado na venda"); 
				    
					result.Add("1", "Cliente Fidelidade que pontuou na venda"); 
				    
					result.Add("2", "Cliente Fidelidade que resgatou pontos na venda"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("3", "ClienteNaoFidelizado"); 
				    
					result.Add("4", "ClienteNaoIdentificado"); 
				    
					result.Add("1", "PontuouVenda"); 
				    
					result.Add("2", "ResgatePontos"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _ClienteNaoFidelizado = new DomainKeyPair() { Value = "3", DisplayName = "Cliente não Fidelidade" };
					[FunctionalPoint("Value[3];DisplayName[Cliente não Fidelidade]")]
					public static DomainKeyPair ClienteNaoFidelizado { get { return _ClienteNaoFidelizado; } }
				    
					private static DomainKeyPair _ClienteNaoIdentificado = new DomainKeyPair() { Value = "4", DisplayName = "Cliente não identificado na venda" };
					[FunctionalPoint("Value[4];DisplayName[Cliente não identificado na venda]")]
					public static DomainKeyPair ClienteNaoIdentificado { get { return _ClienteNaoIdentificado; } }
				    
					private static DomainKeyPair _PontuouVenda = new DomainKeyPair() { Value = "1", DisplayName = "Cliente Fidelidade que pontuou na venda" };
					[FunctionalPoint("Value[1];DisplayName[Cliente Fidelidade que pontuou na venda]")]
					public static DomainKeyPair PontuouVenda { get { return _PontuouVenda; } }
				    
					private static DomainKeyPair _ResgatePontos = new DomainKeyPair() { Value = "2", DisplayName = "Cliente Fidelidade que resgatou pontos na venda" };
					[FunctionalPoint("Value[2];DisplayName[Cliente Fidelidade que resgatou pontos na venda]")]
					public static DomainKeyPair ResgatePontos { get { return _ResgatePontos; } }
				    
			#endregion properties

			

	}    
	//<LX_TIPO_CTRL_TIPO_PGTO>((#LxExpr#) == [-2-] ? "Caixa" : ((#LxExpr#) == [-1-] ? "Venda" : ""))</LX_TIPO_CTRL_TIPO_PGTO>	
    public partial class LX_TIPO_CTRL_TIPO_PGTO
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "Caixa"); 
				    
					result.Add("1", "Venda"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "Caixa"); 
				    
					result.Add("1", "Venda"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Caixa = new DomainKeyPair() { Value = "2", DisplayName = "Caixa" };
					[FunctionalPoint("Value[2];DisplayName[Caixa]")]
					public static DomainKeyPair Caixa { get { return _Caixa; } }
				    
					private static DomainKeyPair _Venda = new DomainKeyPair() { Value = "1", DisplayName = "Venda" };
					[FunctionalPoint("Value[1];DisplayName[Venda]")]
					public static DomainKeyPair Venda { get { return _Venda; } }
				    
			#endregion properties

			

	}    
	//<LX_STATUS_CONFERENCIA_CTRL>((#LxExpr#) == [-2-] ? "Conferido" : ((#LxExpr#) == [-3-] ? "Com Divergência" : ((#LxExpr#) == [-4-] ? "Financeiro Gerado" : ((#LxExpr#) == [-1-] ? "Não Conferido" : ""))))</LX_STATUS_CONFERENCIA_CTRL>	
    public partial class LX_STATUS_CONFERENCIA_CTRL
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "Conferido"); 
				    
					result.Add("3", "Com Divergência"); 
				    
					result.Add("4", "Financeiro Gerado"); 
				    
					result.Add("1", "Não Conferido"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "Conferido"); 
				    
					result.Add("3", "Divergencia"); 
				    
					result.Add("4", "Integrado"); 
				    
					result.Add("1", "NaoAplicaNaoConferido"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Conferido = new DomainKeyPair() { Value = "2", DisplayName = "Conferido" };
					[FunctionalPoint("Value[2];DisplayName[Conferido]")]
					public static DomainKeyPair Conferido { get { return _Conferido; } }
				    
					private static DomainKeyPair _Divergencia = new DomainKeyPair() { Value = "3", DisplayName = "Com Divergência" };
					[FunctionalPoint("Value[3];DisplayName[Com Divergência]")]
					public static DomainKeyPair Divergencia { get { return _Divergencia; } }
				    
					private static DomainKeyPair _Integrado = new DomainKeyPair() { Value = "4", DisplayName = "Financeiro Gerado" };
					[FunctionalPoint("Value[4];DisplayName[Financeiro Gerado]")]
					public static DomainKeyPair Integrado { get { return _Integrado; } }
				    
					private static DomainKeyPair _NaoAplicaNaoConferido = new DomainKeyPair() { Value = "1", DisplayName = "Não Conferido" };
					[FunctionalPoint("Value[1];DisplayName[Não Conferido]")]
					public static DomainKeyPair NaoAplicaNaoConferido { get { return _NaoAplicaNaoConferido; } }
				    
			#endregion properties

			

	}    
	//<LX_TIPO_CONFERENCIA>((#LxExpr#) == [-3-] ? "Período" : ((#LxExpr#) == [-2-] ? "Terminal" : ((#LxExpr#) == [-1-] ? "Tipo de Pagamento" : "")))</LX_TIPO_CONFERENCIA>	
    public partial class LX_TIPO_CONFERENCIA
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("3", "Período"); 
				    
					result.Add("2", "Terminal"); 
				    
					result.Add("1", "Tipo de Pagamento"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("3", "Periodo"); 
				    
					result.Add("2", "Terminal"); 
				    
					result.Add("1", "TipoPgto"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Periodo = new DomainKeyPair() { Value = "3", DisplayName = "Período" };
					[FunctionalPoint("Value[3];DisplayName[Período]")]
					public static DomainKeyPair Periodo { get { return _Periodo; } }
				    
					private static DomainKeyPair _Terminal = new DomainKeyPair() { Value = "2", DisplayName = "Terminal" };
					[FunctionalPoint("Value[2];DisplayName[Terminal]")]
					public static DomainKeyPair Terminal { get { return _Terminal; } }
				    
					private static DomainKeyPair _TipoPgto = new DomainKeyPair() { Value = "1", DisplayName = "Tipo de Pagamento" };
					[FunctionalPoint("Value[1];DisplayName[Tipo de Pagamento]")]
					public static DomainKeyPair TipoPgto { get { return _TipoPgto; } }
				    
			#endregion properties

			

	}    
	//<LX_TIPO_OCORRENCIA>((#LxExpr#) == [-3-] ? "Exclusão" : ((#LxExpr#) == [-1-] ? "Inclusão" : ((#LxExpr#) == [-2-] ? "Manutenção" : "")))</LX_TIPO_OCORRENCIA>	
    public partial class LX_TIPO_OCORRENCIA
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("3", "Exclusão"); 
				    
					result.Add("1", "Inclusão"); 
				    
					result.Add("2", "Manutenção"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("3", "ExclusaoInativacao"); 
				    
					result.Add("1", "Inclusao"); 
				    
					result.Add("2", "Manutencao"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _ExclusaoInativacao = new DomainKeyPair() { Value = "3", DisplayName = "Exclusão" };
					[FunctionalPoint("Value[3];DisplayName[Exclusão]")]
					public static DomainKeyPair ExclusaoInativacao { get { return _ExclusaoInativacao; } }
				    
					private static DomainKeyPair _Inclusao = new DomainKeyPair() { Value = "1", DisplayName = "Inclusão" };
					[FunctionalPoint("Value[1];DisplayName[Inclusão]")]
					public static DomainKeyPair Inclusao { get { return _Inclusao; } }
				    
					private static DomainKeyPair _Manutencao = new DomainKeyPair() { Value = "2", DisplayName = "Manutenção" };
					[FunctionalPoint("Value[2];DisplayName[Manutenção]")]
					public static DomainKeyPair Manutencao { get { return _Manutencao; } }
				    
			#endregion properties

			

	}    
	//<LX_LOJA_TIPO_AGRUPAMENTO>((#LxExpr#) == [-2-] ? "Agrupamento Comercial" : ((#LxExpr#) == [-1-] ? "Agrupamento Sortimento" : ""))</LX_LOJA_TIPO_AGRUPAMENTO>	
    public partial class LX_LOJA_TIPO_AGRUPAMENTO
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "Agrupamento Comercial"); 
				    
					result.Add("1", "Agrupamento Sortimento"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "Comercial"); 
				    
					result.Add("1", "Sortimento"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Comercial = new DomainKeyPair() { Value = "2", DisplayName = "Agrupamento Comercial" };
					[FunctionalPoint("Value[2];DisplayName[Agrupamento Comercial]")]
					public static DomainKeyPair Comercial { get { return _Comercial; } }
				    
					private static DomainKeyPair _Sortimento = new DomainKeyPair() { Value = "1", DisplayName = "Agrupamento Sortimento" };
					[FunctionalPoint("Value[1];DisplayName[Agrupamento Sortimento]")]
					public static DomainKeyPair Sortimento { get { return _Sortimento; } }
				    
			#endregion properties

			

	}    
	//<LX_STATUS_CENARIO>((#LxExpr#) == [-2-] ? "Ativo" : ((#LxExpr#) == [-1-] ? "Em Elaboração" : ((#LxExpr#) == [-3-] ? "Inativo" : "")))</LX_STATUS_CENARIO>	
    public partial class LX_STATUS_CENARIO
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "Ativo"); 
				    
					result.Add("1", "Em Elaboração"); 
				    
					result.Add("3", "Inativo"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "Ativo"); 
				    
					result.Add("1", "EmElaboracao"); 
				    
					result.Add("3", "Inativo"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Ativo = new DomainKeyPair() { Value = "2", DisplayName = "Ativo" };
					[FunctionalPoint("Value[2];DisplayName[Ativo]")]
					public static DomainKeyPair Ativo { get { return _Ativo; } }
				    
					private static DomainKeyPair _EmElaboracao = new DomainKeyPair() { Value = "1", DisplayName = "Em Elaboração" };
					[FunctionalPoint("Value[1];DisplayName[Em Elaboração]")]
					public static DomainKeyPair EmElaboracao { get { return _EmElaboracao; } }
				    
					private static DomainKeyPair _Inativo = new DomainKeyPair() { Value = "3", DisplayName = "Inativo" };
					[FunctionalPoint("Value[3];DisplayName[Inativo]")]
					public static DomainKeyPair Inativo { get { return _Inativo; } }
				    
			#endregion properties

			

	}    
	//<ParametroHierarquia>((#LxExpr#) == [-100-] ? "Obrigatório" : ((#LxExpr#) == [-1-] ? "Variação Nível 1" : ((#LxExpr#) == [-2-] ? "Variação Nível 2" : ((#LxExpr#) == [-3-] ? "Variação Nível 3" : ((#LxExpr#) == [-4-] ? "Variação Nível 4" : ((#LxExpr#) == [-5-] ? "Variação Nível 5" : ""))))))</ParametroHierarquia>	
    public partial class ParametroHierarquia
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("100", "Obrigatório"); 
				    
					result.Add("1", "Variação Nível 1"); 
				    
					result.Add("2", "Variação Nível 2"); 
				    
					result.Add("3", "Variação Nível 3"); 
				    
					result.Add("4", "Variação Nível 4"); 
				    
					result.Add("5", "Variação Nível 5"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("100", "Obrigatorio"); 
				    
					result.Add("1", "VariacaoNivel1"); 
				    
					result.Add("2", "VariacaoNivel2"); 
				    
					result.Add("3", "VariacaoNivel3"); 
				    
					result.Add("4", "VariacaoNivel4"); 
				    
					result.Add("5", "VariacaoNivel5"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Obrigatorio = new DomainKeyPair() { Value = "100", DisplayName = "Obrigatório" };
					[FunctionalPoint("Value[100];DisplayName[Obrigatório]")]
					public static DomainKeyPair Obrigatorio { get { return _Obrigatorio; } }
				    
					private static DomainKeyPair _VariacaoNivel1 = new DomainKeyPair() { Value = "1", DisplayName = "Variação Nível 1" };
					[FunctionalPoint("Value[1];DisplayName[Variação Nível 1]")]
					public static DomainKeyPair VariacaoNivel1 { get { return _VariacaoNivel1; } }
				    
					private static DomainKeyPair _VariacaoNivel2 = new DomainKeyPair() { Value = "2", DisplayName = "Variação Nível 2" };
					[FunctionalPoint("Value[2];DisplayName[Variação Nível 2]")]
					public static DomainKeyPair VariacaoNivel2 { get { return _VariacaoNivel2; } }
				    
					private static DomainKeyPair _VariacaoNivel3 = new DomainKeyPair() { Value = "3", DisplayName = "Variação Nível 3" };
					[FunctionalPoint("Value[3];DisplayName[Variação Nível 3]")]
					public static DomainKeyPair VariacaoNivel3 { get { return _VariacaoNivel3; } }
				    
					private static DomainKeyPair _VariacaoNivel4 = new DomainKeyPair() { Value = "4", DisplayName = "Variação Nível 4" };
					[FunctionalPoint("Value[4];DisplayName[Variação Nível 4]")]
					public static DomainKeyPair VariacaoNivel4 { get { return _VariacaoNivel4; } }
				    
					private static DomainKeyPair _VariacaoNivel5 = new DomainKeyPair() { Value = "5", DisplayName = "Variação Nível 5" };
					[FunctionalPoint("Value[5];DisplayName[Variação Nível 5]")]
					public static DomainKeyPair VariacaoNivel5 { get { return _VariacaoNivel5; } }
				    
			#endregion properties

			

	}    
	//<LX_STATUS_PROCESSO>((#LxExpr#) == [-4-] ? "Em Processamento" : ((#LxExpr#) == [-3-] ? "Erro" : ((#LxExpr#) == [-1-] ? "Não Processado" : ((#LxExpr#) == [-2-] ? "Processado" : ""))))</LX_STATUS_PROCESSO>	
    public partial class LX_STATUS_PROCESSO
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("4", "Em Processamento"); 
				    
					result.Add("3", "Erro"); 
				    
					result.Add("1", "Não Processado"); 
				    
					result.Add("2", "Processado"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("4", "EmProcessamento"); 
				    
					result.Add("3", "Erro"); 
				    
					result.Add("1", "NaoProcessado"); 
				    
					result.Add("2", "Processado"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _EmProcessamento = new DomainKeyPair() { Value = "4", DisplayName = "Em Processamento" };
					[FunctionalPoint("Value[4];DisplayName[Em Processamento]")]
					public static DomainKeyPair EmProcessamento { get { return _EmProcessamento; } }
				    
					private static DomainKeyPair _Erro = new DomainKeyPair() { Value = "3", DisplayName = "Erro" };
					[FunctionalPoint("Value[3];DisplayName[Erro]")]
					public static DomainKeyPair Erro { get { return _Erro; } }
				    
					private static DomainKeyPair _NaoProcessado = new DomainKeyPair() { Value = "1", DisplayName = "Não Processado" };
					[FunctionalPoint("Value[1];DisplayName[Não Processado]")]
					public static DomainKeyPair NaoProcessado { get { return _NaoProcessado; } }
				    
					private static DomainKeyPair _Processado = new DomainKeyPair() { Value = "2", DisplayName = "Processado" };
					[FunctionalPoint("Value[2];DisplayName[Processado]")]
					public static DomainKeyPair Processado { get { return _Processado; } }
				    
			#endregion properties

			

	}    
	//<LX_TIPO_REMARCACAO>((#LxExpr#) == [-2-] ? "Promoção" : ((#LxExpr#) == [-1-] ? "Remarcação" : ""))</LX_TIPO_REMARCACAO>	
    public partial class LX_TIPO_REMARCACAO
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "Promoção"); 
				    
					result.Add("1", "Remarcação"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "Promocao"); 
				    
					result.Add("1", "Remarcacao"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Promocao = new DomainKeyPair() { Value = "2", DisplayName = "Promoção" };
					[FunctionalPoint("Value[2];DisplayName[Promoção]")]
					public static DomainKeyPair Promocao { get { return _Promocao; } }
				    
					private static DomainKeyPair _Remarcacao = new DomainKeyPair() { Value = "1", DisplayName = "Remarcação" };
					[FunctionalPoint("Value[1];DisplayName[Remarcação]")]
					public static DomainKeyPair Remarcacao { get { return _Remarcacao; } }
				    
			#endregion properties

			

	}    
	//<LX_ORIGEM_MOVIMENTO>((#LxExpr#) == [-4-] ? "Capturada do Resumo da NFCe" : ((#LxExpr#) == [-5-] ? "Capturada do Resumo do SAT" : ((#LxExpr#) == [-1-] ? "Capturada pelo Sistema" : ((#LxExpr#) == [-2-] ? "Digitada na Loja" : ((#LxExpr#) == [-3-] ? "Digitada na Retaguarda" : "")))))</LX_ORIGEM_MOVIMENTO>	
    public partial class LX_ORIGEM_MOVIMENTO
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("4", "Capturada do Resumo da NFCe"); 
				    
					result.Add("5", "Capturada do Resumo do SAT"); 
				    
					result.Add("1", "Capturada pelo Sistema"); 
				    
					result.Add("2", "Digitada na Loja"); 
				    
					result.Add("3", "Digitada na Retaguarda"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("4", "CapturadaNFCe"); 
				    
					result.Add("5", "CapturadaSAT"); 
				    
					result.Add("1", "CapturadaSistema"); 
				    
					result.Add("2", "DigitadaLoja"); 
				    
					result.Add("3", "DigitadaRetaguarda"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _CapturadaNFCe = new DomainKeyPair() { Value = "4", DisplayName = "Capturada do Resumo da NFCe" };
					[FunctionalPoint("Value[4];DisplayName[Capturada do Resumo da NFCe]")]
					public static DomainKeyPair CapturadaNFCe { get { return _CapturadaNFCe; } }
				    
					private static DomainKeyPair _CapturadaSAT = new DomainKeyPair() { Value = "5", DisplayName = "Capturada do Resumo do SAT" };
					[FunctionalPoint("Value[5];DisplayName[Capturada do Resumo do SAT]")]
					public static DomainKeyPair CapturadaSAT { get { return _CapturadaSAT; } }
				    
					private static DomainKeyPair _CapturadaSistema = new DomainKeyPair() { Value = "1", DisplayName = "Capturada pelo Sistema" };
					[FunctionalPoint("Value[1];DisplayName[Capturada pelo Sistema]")]
					public static DomainKeyPair CapturadaSistema { get { return _CapturadaSistema; } }
				    
					private static DomainKeyPair _DigitadaLoja = new DomainKeyPair() { Value = "2", DisplayName = "Digitada na Loja" };
					[FunctionalPoint("Value[2];DisplayName[Digitada na Loja]")]
					public static DomainKeyPair DigitadaLoja { get { return _DigitadaLoja; } }
				    
					private static DomainKeyPair _DigitadaRetaguarda = new DomainKeyPair() { Value = "3", DisplayName = "Digitada na Retaguarda" };
					[FunctionalPoint("Value[3];DisplayName[Digitada na Retaguarda]")]
					public static DomainKeyPair DigitadaRetaguarda { get { return _DigitadaRetaguarda; } }
				    
			#endregion properties

			

	}    
	//<LX_STATUS_REDUCAO>((#LxExpr#) == [-3-] ? "Ausência de Redução Anterior" : ((#LxExpr#) == [-9-] ? "Erro" : ((#LxExpr#) == [-1-] ? "Não Processado" : ((#LxExpr#) == [-2-] ? "Validado" : ""))))</LX_STATUS_REDUCAO>	
    public partial class LX_STATUS_REDUCAO
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("3", "Ausência de Redução Anterior"); 
				    
					result.Add("9", "Erro"); 
				    
					result.Add("1", "Não Processado"); 
				    
					result.Add("2", "Validado"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("3", "AusenciaReducaoAnterior"); 
				    
					result.Add("9", "Erro"); 
				    
					result.Add("1", "NaoProcessado"); 
				    
					result.Add("2", "Validado"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _AusenciaReducaoAnterior = new DomainKeyPair() { Value = "3", DisplayName = "Ausência de Redução Anterior" };
					[FunctionalPoint("Value[3];DisplayName[Ausência de Redução Anterior]")]
					public static DomainKeyPair AusenciaReducaoAnterior { get { return _AusenciaReducaoAnterior; } }
				    
					private static DomainKeyPair _Erro = new DomainKeyPair() { Value = "9", DisplayName = "Erro" };
					[FunctionalPoint("Value[9];DisplayName[Erro]")]
					public static DomainKeyPair Erro { get { return _Erro; } }
				    
					private static DomainKeyPair _NaoProcessado = new DomainKeyPair() { Value = "1", DisplayName = "Não Processado" };
					[FunctionalPoint("Value[1];DisplayName[Não Processado]")]
					public static DomainKeyPair NaoProcessado { get { return _NaoProcessado; } }
				    
					private static DomainKeyPair _Validado = new DomainKeyPair() { Value = "2", DisplayName = "Validado" };
					[FunctionalPoint("Value[2];DisplayName[Validado]")]
					public static DomainKeyPair Validado { get { return _Validado; } }
				    
			#endregion properties

			

	}    
	//<LX_STATUS_REQUISICAO_ITEM>((#LxExpr#) == [-1-] ? "Aguardando Pedido" : ((#LxExpr#) == [-3-] ? "Cancelado" : ((#LxExpr#) == [-2-] ? "Pedido Gerado" : "")))</LX_STATUS_REQUISICAO_ITEM>	
    public partial class LX_STATUS_REQUISICAO_ITEM
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Aguardando Pedido"); 
				    
					result.Add("3", "Cancelado"); 
				    
					result.Add("2", "Pedido Gerado"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "AGUARDANDO_PEDIDO"); 
				    
					result.Add("3", "CANCELADO"); 
				    
					result.Add("2", "PEDIDO_GERADO"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _AGUARDANDO_PEDIDO = new DomainKeyPair() { Value = "1", DisplayName = "Aguardando Pedido" };
					[FunctionalPoint("Value[1];DisplayName[Aguardando Pedido]")]
					public static DomainKeyPair AGUARDANDO_PEDIDO { get { return _AGUARDANDO_PEDIDO; } }
				    
					private static DomainKeyPair _CANCELADO = new DomainKeyPair() { Value = "3", DisplayName = "Cancelado" };
					[FunctionalPoint("Value[3];DisplayName[Cancelado]")]
					public static DomainKeyPair CANCELADO { get { return _CANCELADO; } }
				    
					private static DomainKeyPair _PEDIDO_GERADO = new DomainKeyPair() { Value = "2", DisplayName = "Pedido Gerado" };
					[FunctionalPoint("Value[2];DisplayName[Pedido Gerado]")]
					public static DomainKeyPair PEDIDO_GERADO { get { return _PEDIDO_GERADO; } }
				    
			#endregion properties

			

	}    
	//<LX_STATUS_REQUISICAO>((#LxExpr#) == [-1-] ? "Aguardando Pedido" : ((#LxExpr#) == [-3-] ? "Cancelado" : ((#LxExpr#) == [-2-] ? "Finalizado" : "")))</LX_STATUS_REQUISICAO>	
    public partial class LX_STATUS_REQUISICAO
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Aguardando Pedido"); 
				    
					result.Add("3", "Cancelado"); 
				    
					result.Add("2", "Finalizado"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "AGUARDANDO_PEDIDO"); 
				    
					result.Add("3", "CANCELADO"); 
				    
					result.Add("2", "FINALIZADO"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _AGUARDANDO_PEDIDO = new DomainKeyPair() { Value = "1", DisplayName = "Aguardando Pedido" };
					[FunctionalPoint("Value[1];DisplayName[Aguardando Pedido]")]
					public static DomainKeyPair AGUARDANDO_PEDIDO { get { return _AGUARDANDO_PEDIDO; } }
				    
					private static DomainKeyPair _CANCELADO = new DomainKeyPair() { Value = "3", DisplayName = "Cancelado" };
					[FunctionalPoint("Value[3];DisplayName[Cancelado]")]
					public static DomainKeyPair CANCELADO { get { return _CANCELADO; } }
				    
					private static DomainKeyPair _FINALIZADO = new DomainKeyPair() { Value = "2", DisplayName = "Finalizado" };
					[FunctionalPoint("Value[2];DisplayName[Finalizado]")]
					public static DomainKeyPair FINALIZADO { get { return _FINALIZADO; } }
				    
			#endregion properties

			

	}    
	//<LX_TIPO_AJUSTE>((#LxExpr#) == [-1-] ? "Completo" : ((#LxExpr#) == [-2-] ? "Parcial" : ""))</LX_TIPO_AJUSTE>	
    public partial class LX_TIPO_AJUSTE
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Completo"); 
				    
					result.Add("2", "Parcial"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "COMPLETO"); 
				    
					result.Add("2", "PARCIAL"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _COMPLETO = new DomainKeyPair() { Value = "1", DisplayName = "Completo" };
					[FunctionalPoint("Value[1];DisplayName[Completo]")]
					public static DomainKeyPair COMPLETO { get { return _COMPLETO; } }
				    
					private static DomainKeyPair _PARCIAL = new DomainKeyPair() { Value = "2", DisplayName = "Parcial" };
					[FunctionalPoint("Value[2];DisplayName[Parcial]")]
					public static DomainKeyPair PARCIAL { get { return _PARCIAL; } }
				    
			#endregion properties

			

	}    
	//<LX_METODO_RECONTAGEM>((#LxExpr#) == [-2-] ? "Somente com Divergência" : ((#LxExpr#) == [-1-] ? "Todos os Itens do Setor" : ""))</LX_METODO_RECONTAGEM>	
    public partial class LX_METODO_RECONTAGEM
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "Somente com Divergência"); 
				    
					result.Add("1", "Todos os Itens do Setor"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "SomenteComDivergência"); 
				    
					result.Add("1", "TodosOsItens"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _SomenteComDivergência = new DomainKeyPair() { Value = "2", DisplayName = "Somente com Divergência" };
					[FunctionalPoint("Value[2];DisplayName[Somente com Divergência]")]
					public static DomainKeyPair SomenteComDivergência { get { return _SomenteComDivergência; } }
				    
					private static DomainKeyPair _TodosOsItens = new DomainKeyPair() { Value = "1", DisplayName = "Todos os Itens do Setor" };
					[FunctionalPoint("Value[1];DisplayName[Todos os Itens do Setor]")]
					public static DomainKeyPair TodosOsItens { get { return _TodosOsItens; } }
				    
			#endregion properties

			

	}    
	//<LX_STATUS_INVENTARIO>((#LxExpr#) == [-3-] ? "Aguardando Ajuste" : ((#LxExpr#) == [-2-] ? "Aguardando Coleta" : ((#LxExpr#) == [-1-] ? "Em Definição" : ((#LxExpr#) == [-4-] ? "Finalizado" : ""))))</LX_STATUS_INVENTARIO>	
    public partial class LX_STATUS_INVENTARIO
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("3", "Aguardando Ajuste"); 
				    
					result.Add("2", "Aguardando Coleta"); 
				    
					result.Add("1", "Em Definição"); 
				    
					result.Add("4", "Finalizado"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("3", "AguardandoAjuste"); 
				    
					result.Add("2", "AguardandoColeta"); 
				    
					result.Add("1", "EmSetorizacao"); 
				    
					result.Add("4", "InventarioFinalizado"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _AguardandoAjuste = new DomainKeyPair() { Value = "3", DisplayName = "Aguardando Ajuste" };
					[FunctionalPoint("Value[3];DisplayName[Aguardando Ajuste]")]
					public static DomainKeyPair AguardandoAjuste { get { return _AguardandoAjuste; } }
				    
					private static DomainKeyPair _AguardandoColeta = new DomainKeyPair() { Value = "2", DisplayName = "Aguardando Coleta" };
					[FunctionalPoint("Value[2];DisplayName[Aguardando Coleta]")]
					public static DomainKeyPair AguardandoColeta { get { return _AguardandoColeta; } }
				    
					private static DomainKeyPair _EmSetorizacao = new DomainKeyPair() { Value = "1", DisplayName = "Em Definição" };
					[FunctionalPoint("Value[1];DisplayName[Em Definição]")]
					public static DomainKeyPair EmSetorizacao { get { return _EmSetorizacao; } }
				    
					private static DomainKeyPair _InventarioFinalizado = new DomainKeyPair() { Value = "4", DisplayName = "Finalizado" };
					[FunctionalPoint("Value[4];DisplayName[Finalizado]")]
					public static DomainKeyPair InventarioFinalizado { get { return _InventarioFinalizado; } }
				    
			#endregion properties

			

	}    
	//<LX_STATUS_INVENTARIO_SETOR>((#LxExpr#) == [-1-] ? "Aguardando Coleta" : ((#LxExpr#) == [-2-] ? "Coleta Finalizada" : ""))</LX_STATUS_INVENTARIO_SETOR>	
    public partial class LX_STATUS_INVENTARIO_SETOR
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Aguardando Coleta"); 
				    
					result.Add("2", "Coleta Finalizada"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "AguardandoColeta"); 
				    
					result.Add("2", "ColetaFinalizada"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _AguardandoColeta = new DomainKeyPair() { Value = "1", DisplayName = "Aguardando Coleta" };
					[FunctionalPoint("Value[1];DisplayName[Aguardando Coleta]")]
					public static DomainKeyPair AguardandoColeta { get { return _AguardandoColeta; } }
				    
					private static DomainKeyPair _ColetaFinalizada = new DomainKeyPair() { Value = "2", DisplayName = "Coleta Finalizada" };
					[FunctionalPoint("Value[2];DisplayName[Coleta Finalizada]")]
					public static DomainKeyPair ColetaFinalizada { get { return _ColetaFinalizada; } }
				    
			#endregion properties

			

	}    
	//<LX_STATUS_CONFERENCIA>((#LxExpr#) == [-2-] ? "Aguardando Confronto" : ((#LxExpr#) == [-3-] ? "Conferência em Análise" : ((#LxExpr#) == [-1-] ? "Em Contagem" : ((#LxExpr#) == [-4-] ? "Conferência Finalizada" : ""))))</LX_STATUS_CONFERENCIA>	
    public partial class LX_STATUS_CONFERENCIA
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "Aguardando Confronto"); 
				    
					result.Add("3", "Conferência em Análise"); 
				    
					result.Add("1", "Em Contagem"); 
				    
					result.Add("4", "Conferência Finalizada"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "AguardandoConfronto"); 
				    
					result.Add("3", "EmConfronto"); 
				    
					result.Add("1", "EmContagem"); 
				    
					result.Add("4", "Finalizada"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _AguardandoConfronto = new DomainKeyPair() { Value = "2", DisplayName = "Aguardando Confronto" };
					[FunctionalPoint("Value[2];DisplayName[Aguardando Confronto]")]
					public static DomainKeyPair AguardandoConfronto { get { return _AguardandoConfronto; } }
				    
					private static DomainKeyPair _EmConfronto = new DomainKeyPair() { Value = "3", DisplayName = "Conferência em Análise" };
					[FunctionalPoint("Value[3];DisplayName[Conferência em Análise]")]
					public static DomainKeyPair EmConfronto { get { return _EmConfronto; } }
				    
					private static DomainKeyPair _EmContagem = new DomainKeyPair() { Value = "1", DisplayName = "Em Contagem" };
					[FunctionalPoint("Value[1];DisplayName[Em Contagem]")]
					public static DomainKeyPair EmContagem { get { return _EmContagem; } }
				    
					private static DomainKeyPair _Finalizada = new DomainKeyPair() { Value = "4", DisplayName = "Conferência Finalizada" };
					[FunctionalPoint("Value[4];DisplayName[Conferência Finalizada]")]
					public static DomainKeyPair Finalizada { get { return _Finalizada; } }
				    
			#endregion properties

			

	}    
	//<LX_STATUS_CONFRONTO>((#LxExpr#) == [-1-] ? "Aguarda Confronto" : ((#LxExpr#) == [-2-] ? "Conferência = NF" : ((#LxExpr#) == [-3-] ? "Conferência > NF " : ((#LxExpr#) == [-5-] ? "Conferência > NF - Op1-Devolução" : ((#LxExpr#) == [-6-] ? "Conferência > NF - Op2-Complementar" : ((#LxExpr#) == [-4-] ? "Conferência < NF " : ((#LxExpr#) == [-7-] ? "Conferência < NF - Op1-Dev.Simbolica" : "")))))))</LX_STATUS_CONFRONTO>	
    public partial class LX_STATUS_CONFRONTO
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Aguarda Confronto"); 
				    
					result.Add("2", "Conferência = NF"); 
				    
					result.Add("3", "Conferência > NF "); 
				    
					result.Add("5", "Conferência > NF - Op1-Devolução"); 
				    
					result.Add("6", "Conferência > NF - Op2-Complementar"); 
				    
					result.Add("4", "Conferência < NF "); 
				    
					result.Add("7", "Conferência < NF - Op1-Dev.Simbolica"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "AguardaConfronto"); 
				    
					result.Add("2", "ConfIgualNf"); 
				    
					result.Add("3", "ConfMaiorQueNF"); 
				    
					result.Add("5", "ConfMaiorQueNfOp1"); 
				    
					result.Add("6", "ConfMaiorQueNfOp2"); 
				    
					result.Add("4", "ConfMenorQueNF"); 
				    
					result.Add("7", "ConfMenorQueNfOp1"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _AguardaConfronto = new DomainKeyPair() { Value = "1", DisplayName = "Aguarda Confronto" };
					[FunctionalPoint("Value[1];DisplayName[Aguarda Confronto]")]
					public static DomainKeyPair AguardaConfronto { get { return _AguardaConfronto; } }
				    
					private static DomainKeyPair _ConfIgualNf = new DomainKeyPair() { Value = "2", DisplayName = "Conferência = NF" };
					[FunctionalPoint("Value[2];DisplayName[Conferência = NF]")]
					public static DomainKeyPair ConfIgualNf { get { return _ConfIgualNf; } }
				    
					private static DomainKeyPair _ConfMaiorQueNF = new DomainKeyPair() { Value = "3", DisplayName = "Conferência > NF " };
					[FunctionalPoint("Value[3];DisplayName[Conferência > NF ]")]
					public static DomainKeyPair ConfMaiorQueNF { get { return _ConfMaiorQueNF; } }
				    
					private static DomainKeyPair _ConfMaiorQueNfOp1 = new DomainKeyPair() { Value = "5", DisplayName = "Conferência > NF - Op1-Devolução" };
					[FunctionalPoint("Value[5];DisplayName[Conferência > NF - Op1-Devolução]")]
					public static DomainKeyPair ConfMaiorQueNfOp1 { get { return _ConfMaiorQueNfOp1; } }
				    
					private static DomainKeyPair _ConfMaiorQueNfOp2 = new DomainKeyPair() { Value = "6", DisplayName = "Conferência > NF - Op2-Complementar" };
					[FunctionalPoint("Value[6];DisplayName[Conferência > NF - Op2-Complementar]")]
					public static DomainKeyPair ConfMaiorQueNfOp2 { get { return _ConfMaiorQueNfOp2; } }
				    
					private static DomainKeyPair _ConfMenorQueNF = new DomainKeyPair() { Value = "4", DisplayName = "Conferência < NF " };
					[FunctionalPoint("Value[4];DisplayName[Conferência < NF ]")]
					public static DomainKeyPair ConfMenorQueNF { get { return _ConfMenorQueNF; } }
				    
					private static DomainKeyPair _ConfMenorQueNfOp1 = new DomainKeyPair() { Value = "7", DisplayName = "Conferência < NF - Op1-Dev.Simbolica" };
					[FunctionalPoint("Value[7];DisplayName[Conferência < NF - Op1-Dev.Simbolica]")]
					public static DomainKeyPair ConfMenorQueNfOp1 { get { return _ConfMenorQueNfOp1; } }
				    
			#endregion properties

			

	}    
	//<LX_STATUS_ROMANEIO>((#LxExpr#) == [-2-] ? "Aguardando NF" : ((#LxExpr#) == [-9-] ? "Cancelado" : ((#LxExpr#) == [-3-] ? "Finalizado" : ((#LxExpr#) == [-1-] ? "Em Elaboração" : ""))))</LX_STATUS_ROMANEIO>	
    public partial class LX_STATUS_ROMANEIO
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "Aguardando NF"); 
				    
					result.Add("9", "Cancelado"); 
				    
					result.Add("3", "Finalizado"); 
				    
					result.Add("1", "Em Elaboração"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "AguardandoNF"); 
				    
					result.Add("9", "Cancelado"); 
				    
					result.Add("3", "Finalizado"); 
				    
					result.Add("1", "Pendente"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _AguardandoNF = new DomainKeyPair() { Value = "2", DisplayName = "Aguardando NF" };
					[FunctionalPoint("Value[2];DisplayName[Aguardando NF]")]
					public static DomainKeyPair AguardandoNF { get { return _AguardandoNF; } }
				    
					private static DomainKeyPair _Cancelado = new DomainKeyPair() { Value = "9", DisplayName = "Cancelado" };
					[FunctionalPoint("Value[9];DisplayName[Cancelado]")]
					public static DomainKeyPair Cancelado { get { return _Cancelado; } }
				    
					private static DomainKeyPair _Finalizado = new DomainKeyPair() { Value = "3", DisplayName = "Finalizado" };
					[FunctionalPoint("Value[3];DisplayName[Finalizado]")]
					public static DomainKeyPair Finalizado { get { return _Finalizado; } }
				    
					private static DomainKeyPair _Pendente = new DomainKeyPair() { Value = "1", DisplayName = "Em Elaboração" };
					[FunctionalPoint("Value[1];DisplayName[Em Elaboração]")]
					public static DomainKeyPair Pendente { get { return _Pendente; } }
				    
			#endregion properties

			

	}    
	//<LX_TIPO_EMISSAO>((#LxExpr#) == [-1-] ? "Própria" : ((#LxExpr#) == [-2-] ? "Terceiro" : ""))</LX_TIPO_EMISSAO>	
    public partial class LX_TIPO_EMISSAO
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Própria"); 
				    
					result.Add("2", "Terceiro"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Propria"); 
				    
					result.Add("2", "Terceiro"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Propria = new DomainKeyPair() { Value = "1", DisplayName = "Própria" };
					[FunctionalPoint("Value[1];DisplayName[Própria]")]
					public static DomainKeyPair Propria { get { return _Propria; } }
				    
					private static DomainKeyPair _Terceiro = new DomainKeyPair() { Value = "2", DisplayName = "Terceiro" };
					[FunctionalPoint("Value[2];DisplayName[Terceiro]")]
					public static DomainKeyPair Terceiro { get { return _Terceiro; } }
				    
			#endregion properties

			

	}    
	//<LX_OCORRENCIA_CUSTO>((#LxExpr#) == [-1-] ? "Processado" : ((#LxExpr#) == [-2-] ? "Saldo Negativo" : ((#LxExpr#) == [-3-] ? "Saldo sem composição " : "")))</LX_OCORRENCIA_CUSTO>	
    public partial class LX_OCORRENCIA_CUSTO
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Processado"); 
				    
					result.Add("2", "Saldo Negativo"); 
				    
					result.Add("3", "Saldo sem composição "); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Processado"); 
				    
					result.Add("2", "SaldoNegativo"); 
				    
					result.Add("3", "SaldoSemComposicao"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Processado = new DomainKeyPair() { Value = "1", DisplayName = "Processado" };
					[FunctionalPoint("Value[1];DisplayName[Processado]")]
					public static DomainKeyPair Processado { get { return _Processado; } }
				    
					private static DomainKeyPair _SaldoNegativo = new DomainKeyPair() { Value = "2", DisplayName = "Saldo Negativo" };
					[FunctionalPoint("Value[2];DisplayName[Saldo Negativo]")]
					public static DomainKeyPair SaldoNegativo { get { return _SaldoNegativo; } }
				    
					private static DomainKeyPair _SaldoSemComposicao = new DomainKeyPair() { Value = "3", DisplayName = "Saldo sem composição " };
					[FunctionalPoint("Value[3];DisplayName[Saldo sem composição ]")]
					public static DomainKeyPair SaldoSemComposicao { get { return _SaldoSemComposicao; } }
				    
			#endregion properties

			

	}    
	//<LX_INDICADOR_PRESENCA>((#LxExpr#) == [-100-] ? "Nao se aplica" : ((#LxExpr#) == [-4-] ? "NFC-e com Entrega em Domicílio" : ((#LxExpr#) == [-2-] ? "Operação pela Internet" : ((#LxExpr#) == [-9-] ? "Operação não presencial / Outros" : ((#LxExpr#) == [-1-] ? "Operação Presencial" : ((#LxExpr#) == [-3-] ? "Operação por Teleatendimento" : ""))))))</LX_INDICADOR_PRESENCA>	
    public partial class LX_INDICADOR_PRESENCA
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("100", "Nao se aplica"); 
				    
					result.Add("4", "NFC-e com Entrega em Domicílio"); 
				    
					result.Add("2", "Operação pela Internet"); 
				    
					result.Add("9", "Operação não presencial / Outros"); 
				    
					result.Add("1", "Operação Presencial"); 
				    
					result.Add("3", "Operação por Teleatendimento"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("100", "NaoSeAplica"); 
				    
					result.Add("4", "OperacaoEntegraDomicilio"); 
				    
					result.Add("2", "OperacaoInternet"); 
				    
					result.Add("9", "OperacaoNaoPresencial"); 
				    
					result.Add("1", "OperacaoPresencial"); 
				    
					result.Add("3", "OperacaoTeleatendimento"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _NaoSeAplica = new DomainKeyPair() { Value = "100", DisplayName = "Nao se aplica" };
					[FunctionalPoint("Value[100];DisplayName[Nao se aplica]")]
					public static DomainKeyPair NaoSeAplica { get { return _NaoSeAplica; } }
				    
					private static DomainKeyPair _OperacaoEntegraDomicilio = new DomainKeyPair() { Value = "4", DisplayName = "NFC-e com Entrega em Domicílio" };
					[FunctionalPoint("Value[4];DisplayName[NFC-e com Entrega em Domicílio]")]
					public static DomainKeyPair OperacaoEntegraDomicilio { get { return _OperacaoEntegraDomicilio; } }
				    
					private static DomainKeyPair _OperacaoInternet = new DomainKeyPair() { Value = "2", DisplayName = "Operação pela Internet" };
					[FunctionalPoint("Value[2];DisplayName[Operação pela Internet]")]
					public static DomainKeyPair OperacaoInternet { get { return _OperacaoInternet; } }
				    
					private static DomainKeyPair _OperacaoNaoPresencial = new DomainKeyPair() { Value = "9", DisplayName = "Operação não presencial / Outros" };
					[FunctionalPoint("Value[9];DisplayName[Operação não presencial / Outros]")]
					public static DomainKeyPair OperacaoNaoPresencial { get { return _OperacaoNaoPresencial; } }
				    
					private static DomainKeyPair _OperacaoPresencial = new DomainKeyPair() { Value = "1", DisplayName = "Operação Presencial" };
					[FunctionalPoint("Value[1];DisplayName[Operação Presencial]")]
					public static DomainKeyPair OperacaoPresencial { get { return _OperacaoPresencial; } }
				    
					private static DomainKeyPair _OperacaoTeleatendimento = new DomainKeyPair() { Value = "3", DisplayName = "Operação por Teleatendimento" };
					[FunctionalPoint("Value[3];DisplayName[Operação por Teleatendimento]")]
					public static DomainKeyPair OperacaoTeleatendimento { get { return _OperacaoTeleatendimento; } }
				    
			#endregion properties

			

	}    
	//<LX_PROPRIEDADE_STK>((#LxExpr#) == [-3-] ? "Estoque com Terceiro" : ((#LxExpr#) == [-2-] ? "Estoque de Terceiro" : ((#LxExpr#) == [-4-] ? "Estoque de Terceiro com Terceiro" : ((#LxExpr#) == [-1-] ? "Estoque Próprio" : ""))))</LX_PROPRIEDADE_STK>	
    public partial class LX_PROPRIEDADE_STK
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("3", "Estoque com Terceiro"); 
				    
					result.Add("2", "Estoque de Terceiro"); 
				    
					result.Add("4", "Estoque de Terceiro com Terceiro"); 
				    
					result.Add("1", "Estoque Próprio"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("3", "ComTerceiro"); 
				    
					result.Add("2", "DeTerceiro"); 
				    
					result.Add("4", "DeTerceiroComTerceiro"); 
				    
					result.Add("1", "Proprio"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _ComTerceiro = new DomainKeyPair() { Value = "3", DisplayName = "Estoque com Terceiro" };
					[FunctionalPoint("Value[3];DisplayName[Estoque com Terceiro]")]
					public static DomainKeyPair ComTerceiro { get { return _ComTerceiro; } }
				    
					private static DomainKeyPair _DeTerceiro = new DomainKeyPair() { Value = "2", DisplayName = "Estoque de Terceiro" };
					[FunctionalPoint("Value[2];DisplayName[Estoque de Terceiro]")]
					public static DomainKeyPair DeTerceiro { get { return _DeTerceiro; } }
				    
					private static DomainKeyPair _DeTerceiroComTerceiro = new DomainKeyPair() { Value = "4", DisplayName = "Estoque de Terceiro com Terceiro" };
					[FunctionalPoint("Value[4];DisplayName[Estoque de Terceiro com Terceiro]")]
					public static DomainKeyPair DeTerceiroComTerceiro { get { return _DeTerceiroComTerceiro; } }
				    
					private static DomainKeyPair _Proprio = new DomainKeyPair() { Value = "1", DisplayName = "Estoque Próprio" };
					[FunctionalPoint("Value[1];DisplayName[Estoque Próprio]")]
					public static DomainKeyPair Proprio { get { return _Proprio; } }
				    
			#endregion properties

			

	}    
	//<LX_LOJA_SORTIMENTO_METODO>((#LxExpr#) == [-9-] ? "Digitação Mínimo, Máximo e Ideal" : ((#LxExpr#) == [-3-] ? "Estoque Ideal" : ((#LxExpr#) == [-4-] ? "Calcular Estoque Ideal" : ((#LxExpr#) == [-2-] ? "Estoque Mínimo" : ((#LxExpr#) == [-8-] ? "Importação" : ((#LxExpr#) == [-1-] ? "Venda Período" : ""))))))</LX_LOJA_SORTIMENTO_METODO>	
    public partial class LX_LOJA_SORTIMENTO_METODO
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("9", "Digitação Mínimo, Máximo e Ideal"); 
				    
					result.Add("3", "Estoque Ideal"); 
				    
					result.Add("4", "Calcular Estoque Ideal"); 
				    
					result.Add("2", "Estoque Mínimo"); 
				    
					result.Add("8", "Importação"); 
				    
					result.Add("1", "Venda Período"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("9", "Digitacao"); 
				    
					result.Add("3", "EstoqueIdeal"); 
				    
					result.Add("4", "EstoqueIdealCalculado"); 
				    
					result.Add("2", "EstoqueMinimo"); 
				    
					result.Add("8", "Importacao"); 
				    
					result.Add("1", "VendaPeriodo"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Digitacao = new DomainKeyPair() { Value = "9", DisplayName = "Digitação Mínimo, Máximo e Ideal" };
					[FunctionalPoint("Value[9];DisplayName[Digitação Mínimo, Máximo e Ideal]")]
					public static DomainKeyPair Digitacao { get { return _Digitacao; } }
				    
					private static DomainKeyPair _EstoqueIdeal = new DomainKeyPair() { Value = "3", DisplayName = "Estoque Ideal" };
					[FunctionalPoint("Value[3];DisplayName[Estoque Ideal]")]
					public static DomainKeyPair EstoqueIdeal { get { return _EstoqueIdeal; } }
				    
					private static DomainKeyPair _EstoqueIdealCalculado = new DomainKeyPair() { Value = "4", DisplayName = "Calcular Estoque Ideal" };
					[FunctionalPoint("Value[4];DisplayName[Calcular Estoque Ideal]")]
					public static DomainKeyPair EstoqueIdealCalculado { get { return _EstoqueIdealCalculado; } }
				    
					private static DomainKeyPair _EstoqueMinimo = new DomainKeyPair() { Value = "2", DisplayName = "Estoque Mínimo" };
					[FunctionalPoint("Value[2];DisplayName[Estoque Mínimo]")]
					public static DomainKeyPair EstoqueMinimo { get { return _EstoqueMinimo; } }
				    
					private static DomainKeyPair _Importacao = new DomainKeyPair() { Value = "8", DisplayName = "Importação" };
					[FunctionalPoint("Value[8];DisplayName[Importação]")]
					public static DomainKeyPair Importacao { get { return _Importacao; } }
				    
					private static DomainKeyPair _VendaPeriodo = new DomainKeyPair() { Value = "1", DisplayName = "Venda Período" };
					[FunctionalPoint("Value[1];DisplayName[Venda Período]")]
					public static DomainKeyPair VendaPeriodo { get { return _VendaPeriodo; } }
				    
			#endregion properties

			

	}    
	//<LX_STATUS_GERACAO_COMPRA>((#LxExpr#) == [-3-] ? "Compra Gerada" : ((#LxExpr#) == [-1-] ? "Gerar Compra" : ((#LxExpr#) == [-2-] ? "Não Gerar Compra" : "")))</LX_STATUS_GERACAO_COMPRA>	
    public partial class LX_STATUS_GERACAO_COMPRA
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("3", "Compra Gerada"); 
				    
					result.Add("1", "Gerar Compra"); 
				    
					result.Add("2", "Não Gerar Compra"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("3", "CompraGerada"); 
				    
					result.Add("1", "GerarCompra"); 
				    
					result.Add("2", "NaoGerarCompra"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _CompraGerada = new DomainKeyPair() { Value = "3", DisplayName = "Compra Gerada" };
					[FunctionalPoint("Value[3];DisplayName[Compra Gerada]")]
					public static DomainKeyPair CompraGerada { get { return _CompraGerada; } }
				    
					private static DomainKeyPair _GerarCompra = new DomainKeyPair() { Value = "1", DisplayName = "Gerar Compra" };
					[FunctionalPoint("Value[1];DisplayName[Gerar Compra]")]
					public static DomainKeyPair GerarCompra { get { return _GerarCompra; } }
				    
					private static DomainKeyPair _NaoGerarCompra = new DomainKeyPair() { Value = "2", DisplayName = "Não Gerar Compra" };
					[FunctionalPoint("Value[2];DisplayName[Não Gerar Compra]")]
					public static DomainKeyPair NaoGerarCompra { get { return _NaoGerarCompra; } }
				    
			#endregion properties

			

	}    
	//<LX_STATUS_NF_DOC_FISCAL>((#LxExpr#) == [-2-] ? "Autorizado" : ((#LxExpr#) == [-3-] ? "Cancelado" : ((#LxExpr#) == [-6-] ? "Contingência Offline" : ((#LxExpr#) == [-5-] ? "Denegado" : ((#LxExpr#) == [-4-] ? "Inutilizado" : ((#LxExpr#) == [-1-] ? "Pendente" : ""))))))</LX_STATUS_NF_DOC_FISCAL>	
    public partial class LX_STATUS_NF_DOC_FISCAL
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "Autorizado"); 
				    
					result.Add("3", "Cancelado"); 
				    
					result.Add("6", "Contingência Offline"); 
				    
					result.Add("5", "Denegado"); 
				    
					result.Add("4", "Inutilizado"); 
				    
					result.Add("1", "Pendente"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "Autorizado"); 
				    
					result.Add("3", "Cancelado"); 
				    
					result.Add("6", "ContingenciaOffLine"); 
				    
					result.Add("5", "Denegado"); 
				    
					result.Add("4", "Inutilizado"); 
				    
					result.Add("1", "Pendente"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Autorizado = new DomainKeyPair() { Value = "2", DisplayName = "Autorizado" };
					[FunctionalPoint("Value[2];DisplayName[Autorizado]")]
					public static DomainKeyPair Autorizado { get { return _Autorizado; } }
				    
					private static DomainKeyPair _Cancelado = new DomainKeyPair() { Value = "3", DisplayName = "Cancelado" };
					[FunctionalPoint("Value[3];DisplayName[Cancelado]")]
					public static DomainKeyPair Cancelado { get { return _Cancelado; } }
				    
					private static DomainKeyPair _ContingenciaOffLine = new DomainKeyPair() { Value = "6", DisplayName = "Contingência Offline" };
					[FunctionalPoint("Value[6];DisplayName[Contingência Offline]")]
					public static DomainKeyPair ContingenciaOffLine { get { return _ContingenciaOffLine; } }
				    
					private static DomainKeyPair _Denegado = new DomainKeyPair() { Value = "5", DisplayName = "Denegado" };
					[FunctionalPoint("Value[5];DisplayName[Denegado]")]
					public static DomainKeyPair Denegado { get { return _Denegado; } }
				    
					private static DomainKeyPair _Inutilizado = new DomainKeyPair() { Value = "4", DisplayName = "Inutilizado" };
					[FunctionalPoint("Value[4];DisplayName[Inutilizado]")]
					public static DomainKeyPair Inutilizado { get { return _Inutilizado; } }
				    
					private static DomainKeyPair _Pendente = new DomainKeyPair() { Value = "1", DisplayName = "Pendente" };
					[FunctionalPoint("Value[1];DisplayName[Pendente]")]
					public static DomainKeyPair Pendente { get { return _Pendente; } }
				    
			#endregion properties

			

	}    
	//<LX_TIPO_VALOR_ATENDIMENTO_ATRIBUTO>((#LxExpr#) == [-1-] ? "Saldo de Pontos" : "")</LX_TIPO_VALOR_ATENDIMENTO_ATRIBUTO>	
    public partial class LX_TIPO_VALOR_ATENDIMENTO_ATRIBUTO
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Saldo de Pontos"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "SaldoPontos"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _SaldoPontos = new DomainKeyPair() { Value = "1", DisplayName = "Saldo de Pontos" };
					[FunctionalPoint("Value[1];DisplayName[Saldo de Pontos]")]
					public static DomainKeyPair SaldoPontos { get { return _SaldoPontos; } }
				    
			#endregion properties

			

	}    
	//<LX_ORIGEM_ATENDIMENTO>((#LxExpr#) == [-3-] ? "Ecommerce" : ((#LxExpr#) == [-4-] ? "Televenda" : ((#LxExpr#) == [-2-] ? "Venda Direta" : ((#LxExpr#) == [-1-] ? "Venda Loja Física" : ""))))</LX_ORIGEM_ATENDIMENTO>	
    public partial class LX_ORIGEM_ATENDIMENTO
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("3", "Ecommerce"); 
				    
					result.Add("4", "Televenda"); 
				    
					result.Add("2", "Venda Direta"); 
				    
					result.Add("1", "Venda Loja Física"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("3", "Ecommerce"); 
				    
					result.Add("4", "Televenda"); 
				    
					result.Add("2", "VendaDireta"); 
				    
					result.Add("1", "VendaLojaFisica"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Ecommerce = new DomainKeyPair() { Value = "3", DisplayName = "Ecommerce" };
					[FunctionalPoint("Value[3];DisplayName[Ecommerce]")]
					public static DomainKeyPair Ecommerce { get { return _Ecommerce; } }
				    
					private static DomainKeyPair _Televenda = new DomainKeyPair() { Value = "4", DisplayName = "Televenda" };
					[FunctionalPoint("Value[4];DisplayName[Televenda]")]
					public static DomainKeyPair Televenda { get { return _Televenda; } }
				    
					private static DomainKeyPair _VendaDireta = new DomainKeyPair() { Value = "2", DisplayName = "Venda Direta" };
					[FunctionalPoint("Value[2];DisplayName[Venda Direta]")]
					public static DomainKeyPair VendaDireta { get { return _VendaDireta; } }
				    
					private static DomainKeyPair _VendaLojaFisica = new DomainKeyPair() { Value = "1", DisplayName = "Venda Loja Física" };
					[FunctionalPoint("Value[1];DisplayName[Venda Loja Física]")]
					public static DomainKeyPair VendaLojaFisica { get { return _VendaLojaFisica; } }
				    
			#endregion properties

			

	}    
	//<LX_STATUS_COMISSAO_PERIODO>((#LxExpr#) == [-2-] ? "Aguardando Processamento" : ((#LxExpr#) == [-1-] ? "Em preparação" : ((#LxExpr#) == [-9-] ? "Finalizado" : ((#LxExpr#) == [-3-] ? "Processado" : ""))))</LX_STATUS_COMISSAO_PERIODO>	
    public partial class LX_STATUS_COMISSAO_PERIODO
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "Aguardando Processamento"); 
				    
					result.Add("1", "Em preparação"); 
				    
					result.Add("9", "Finalizado"); 
				    
					result.Add("3", "Processado"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("2", "AguardandoProcessamento"); 
				    
					result.Add("1", "EmPreparacao"); 
				    
					result.Add("9", "Finalizado"); 
				    
					result.Add("3", "Processado"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _AguardandoProcessamento = new DomainKeyPair() { Value = "2", DisplayName = "Aguardando Processamento" };
					[FunctionalPoint("Value[2];DisplayName[Aguardando Processamento]")]
					public static DomainKeyPair AguardandoProcessamento { get { return _AguardandoProcessamento; } }
				    
					private static DomainKeyPair _EmPreparacao = new DomainKeyPair() { Value = "1", DisplayName = "Em preparação" };
					[FunctionalPoint("Value[1];DisplayName[Em preparação]")]
					public static DomainKeyPair EmPreparacao { get { return _EmPreparacao; } }
				    
					private static DomainKeyPair _Finalizado = new DomainKeyPair() { Value = "9", DisplayName = "Finalizado" };
					[FunctionalPoint("Value[9];DisplayName[Finalizado]")]
					public static DomainKeyPair Finalizado { get { return _Finalizado; } }
				    
					private static DomainKeyPair _Processado = new DomainKeyPair() { Value = "3", DisplayName = "Processado" };
					[FunctionalPoint("Value[3];DisplayName[Processado]")]
					public static DomainKeyPair Processado { get { return _Processado; } }
				    
			#endregion properties

			

	}    
	//<LX_COMISSAO_PROCESSO_TIPO>((#LxExpr#) == [-36-] ? "Funcionário - Adicional - Governo" : ((#LxExpr#) == [-34-] ? "Grupo Econômico - Adicional - Governo" : ((#LxExpr#) == [-35-] ? "Loja - Adicional - Governo" : ((#LxExpr#) == [-53-] ? "UF - Adicional - Governo" : ((#LxExpr#) == [-3-] ? "Funcionário - Comissão Base" : ((#LxExpr#) == [-1-] ? "Grupo Econômico - Comissão Base" : ((#LxExpr#) == [-2-] ? "Loja - Comissão Base" : ((#LxExpr#) == [-4-] ? "Grupo Econômico - Comissão Base - Operação" : ((#LxExpr#) == [-5-] ? "Loja - Comissão Base - Operação" : ((#LxExpr#) == [-42-] ? "UF - Comissão Base - Operação" : ((#LxExpr#) == [-6-] ? "Grupo Econômico - Comissão Base - Promoção" : ((#LxExpr#) == [-7-] ? "Loja - Comissão Base - Promoção" : ((#LxExpr#) == [-43-] ? "UF - Comissão Base - Promoção" : ((#LxExpr#) == [-41-] ? "UF - Comissão Base" : ((#LxExpr#) == [-98-] ? "Fonte Externa de Valores" : ((#LxExpr#) == [-99-] ? "Não Elegível" : ((#LxExpr#) == [-10-] ? "Funcionário - Prêmio - Meta Loja" : ((#LxExpr#) == [-8-] ? "Grupo Econômico - Prêmio - Meta Loja" : ((#LxExpr#) == [-9-] ? "Loja - Prêmio - Meta Loja" : ((#LxExpr#) == [-44-] ? "UF - Prêmio - Meta Loja" : ((#LxExpr#) == [-13-] ? "Funcionário - Prêmio - Meta Vendedor" : ((#LxExpr#) == [-11-] ? "Grupo Econômico - Prêmio - Meta Vendedor" : ((#LxExpr#) == [-12-] ? "Loja - Prêmio - Meta Vendedor" : ((#LxExpr#) == [-40-] ? "Funcionário - Prêmio - Adicional de Superação de Meta" : ((#LxExpr#) == [-38-] ? "Grupo Econômico - Prêmio - Adicional de Superação de Meta" : ((#LxExpr#) == [-39-] ? "Loja - Prêmio - Adicional de Superação de Meta" : ((#LxExpr#) == [-54-] ? "UF - Prêmio - Adicional de Superação de Meta" : ((#LxExpr#) == [-33-] ? "Funcionário - Prêmio - Superação de Meta" : ((#LxExpr#) == [-31-] ? "Grupo Econômico - Prêmio - Superação de Meta" : ((#LxExpr#) == [-32-] ? "Loja - Prêmio - Superação de Meta" : ((#LxExpr#) == [-52-] ? "UF - Prêmio - Superação de Meta" : ((#LxExpr#) == [-45-] ? "UF - Prêmio - Meta Vendedor" : ((#LxExpr#) == [-25-] ? "Funcionário - Prêmio - Quantidade de Cupons" : ((#LxExpr#) == [-23-] ? "Grupo Econômico - Prêmio - Quantidade de Cupons" : ((#LxExpr#) == [-24-] ? "Loja - Prêmio - Quantidade de Cupons" : ((#LxExpr#) == [-49-] ? "UF - Prêmio - Quantidade de Cupons" : ((#LxExpr#) == [-28-] ? "Funcionário - Prêmio - Valor de Cupom" : ((#LxExpr#) == [-26-] ? "Grupo Econômico - Prêmio - Valor de Cupom" : ((#LxExpr#) == [-27-] ? "Loja - Prêmio - Valor de Cupom" : ((#LxExpr#) == [-50-] ? "UF - Prêmio - Valor de Cupom" : ((#LxExpr#) == [-16-] ? "Funcionário - Prêmio - Ticket Médio" : ((#LxExpr#) == [-14-] ? "Grupo Econômico - Prêmio - Ticket Médio" : ((#LxExpr#) == [-15-] ? "Loja - Prêmio - Ticket Médio" : ((#LxExpr#) == [-46-] ? "UF - Prêmio - Ticket Médio" : ((#LxExpr#) == [-29-] ? "Grupo Econômico - Prêmio - Tipo Pagamento" : ((#LxExpr#) == [-30-] ? "Loja - Prêmio - Tipo Pagamento" : ((#LxExpr#) == [-51-] ? "UF - Prêmio - Tipo Pagamento" : ((#LxExpr#) == [-19-] ? "Funcionário - Prêmio - Venda Cartão Presente" : ((#LxExpr#) == [-17-] ? "Grupo Econômico - Prêmio - Venda Cartão Presente" : ((#LxExpr#) == [-18-] ? "Loja - Prêmio - Venda Cartão Presente" : ((#LxExpr#) == [-47-] ? "UF - Prêmio - Venda Cartão Presente" : ((#LxExpr#) == [-20-] ? "Grupo Econômico - Prêmio - Venda Produto" : ((#LxExpr#) == [-21-] ? "Loja - Prêmio - Venda Produto" : ((#LxExpr#) == [-48-] ? "UF - Prêmio - Venda Produto" : ((#LxExpr#) == [-37-] ? "Vendedor - Prêmio - Venda Produto" : "")))))))))))))))))))))))))))))))))))))))))))))))))))))))</LX_COMISSAO_PROCESSO_TIPO>	
    public partial class LX_COMISSAO_PROCESSO_TIPO
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("36", "Funcionário - Adicional - Governo"); 
				    
					result.Add("34", "Grupo Econômico - Adicional - Governo"); 
				    
					result.Add("35", "Loja - Adicional - Governo"); 
				    
					result.Add("53", "UF - Adicional - Governo"); 
				    
					result.Add("3", "Funcionário - Comissão Base"); 
				    
					result.Add("1", "Grupo Econômico - Comissão Base"); 
				    
					result.Add("2", "Loja - Comissão Base"); 
				    
					result.Add("4", "Grupo Econômico - Comissão Base - Operação"); 
				    
					result.Add("5", "Loja - Comissão Base - Operação"); 
				    
					result.Add("42", "UF - Comissão Base - Operação"); 
				    
					result.Add("6", "Grupo Econômico - Comissão Base - Promoção"); 
				    
					result.Add("7", "Loja - Comissão Base - Promoção"); 
				    
					result.Add("43", "UF - Comissão Base - Promoção"); 
				    
					result.Add("41", "UF - Comissão Base"); 
				    
					result.Add("98", "Fonte Externa de Valores"); 
				    
					result.Add("99", "Não Elegível"); 
				    
					result.Add("10", "Funcionário - Prêmio - Meta Loja"); 
				    
					result.Add("8", "Grupo Econômico - Prêmio - Meta Loja"); 
				    
					result.Add("9", "Loja - Prêmio - Meta Loja"); 
				    
					result.Add("44", "UF - Prêmio - Meta Loja"); 
				    
					result.Add("13", "Funcionário - Prêmio - Meta Vendedor"); 
				    
					result.Add("11", "Grupo Econômico - Prêmio - Meta Vendedor"); 
				    
					result.Add("12", "Loja - Prêmio - Meta Vendedor"); 
				    
					result.Add("40", "Funcionário - Prêmio - Adicional de Superação de Meta"); 
				    
					result.Add("38", "Grupo Econômico - Prêmio - Adicional de Superação de Meta"); 
				    
					result.Add("39", "Loja - Prêmio - Adicional de Superação de Meta"); 
				    
					result.Add("54", "UF - Prêmio - Adicional de Superação de Meta"); 
				    
					result.Add("33", "Funcionário - Prêmio - Superação de Meta"); 
				    
					result.Add("31", "Grupo Econômico - Prêmio - Superação de Meta"); 
				    
					result.Add("32", "Loja - Prêmio - Superação de Meta"); 
				    
					result.Add("52", "UF - Prêmio - Superação de Meta"); 
				    
					result.Add("45", "UF - Prêmio - Meta Vendedor"); 
				    
					result.Add("25", "Funcionário - Prêmio - Quantidade de Cupons"); 
				    
					result.Add("23", "Grupo Econômico - Prêmio - Quantidade de Cupons"); 
				    
					result.Add("24", "Loja - Prêmio - Quantidade de Cupons"); 
				    
					result.Add("49", "UF - Prêmio - Quantidade de Cupons"); 
				    
					result.Add("28", "Funcionário - Prêmio - Valor de Cupom"); 
				    
					result.Add("26", "Grupo Econômico - Prêmio - Valor de Cupom"); 
				    
					result.Add("27", "Loja - Prêmio - Valor de Cupom"); 
				    
					result.Add("50", "UF - Prêmio - Valor de Cupom"); 
				    
					result.Add("16", "Funcionário - Prêmio - Ticket Médio"); 
				    
					result.Add("14", "Grupo Econômico - Prêmio - Ticket Médio"); 
				    
					result.Add("15", "Loja - Prêmio - Ticket Médio"); 
				    
					result.Add("46", "UF - Prêmio - Ticket Médio"); 
				    
					result.Add("29", "Grupo Econômico - Prêmio - Tipo Pagamento"); 
				    
					result.Add("30", "Loja - Prêmio - Tipo Pagamento"); 
				    
					result.Add("51", "UF - Prêmio - Tipo Pagamento"); 
				    
					result.Add("19", "Funcionário - Prêmio - Venda Cartão Presente"); 
				    
					result.Add("17", "Grupo Econômico - Prêmio - Venda Cartão Presente"); 
				    
					result.Add("18", "Loja - Prêmio - Venda Cartão Presente"); 
				    
					result.Add("47", "UF - Prêmio - Venda Cartão Presente"); 
				    
					result.Add("20", "Grupo Econômico - Prêmio - Venda Produto"); 
				    
					result.Add("21", "Loja - Prêmio - Venda Produto"); 
				    
					result.Add("48", "UF - Prêmio - Venda Produto"); 
				    
					result.Add("37", "Vendedor - Prêmio - Venda Produto"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("36", "AdicionalGovernoFuncionario"); 
				    
					result.Add("34", "AdicionalGovernoGrupoEconomico"); 
				    
					result.Add("35", "AdicionalGovernoLoja"); 
				    
					result.Add("53", "AdicionalGovernoUF"); 
				    
					result.Add("3", "ComissaoBaseFuncionario"); 
				    
					result.Add("1", "ComissaoBaseGrupoEconomico"); 
				    
					result.Add("2", "ComissaoBaseLoja"); 
				    
					result.Add("4", "ComissaoBaseOperacaoGrupoEconomico"); 
				    
					result.Add("5", "ComissaoBaseOperacaoLoja"); 
				    
					result.Add("42", "ComissaoBaseOperacaoUF"); 
				    
					result.Add("6", "ComissaoBasePromocaoGrupoEconomico"); 
				    
					result.Add("7", "ComissaoBasePromocaoLoja"); 
				    
					result.Add("43", "ComissaoBasePromocaoUF"); 
				    
					result.Add("41", "ComissaoBaseUF"); 
				    
					result.Add("98", "FonteExternaValores"); 
				    
					result.Add("99", "NaoElegivel"); 
				    
					result.Add("10", "PremioMetaLojaFuncionario"); 
				    
					result.Add("8", "PremioMetaLojaGrupoEconomico"); 
				    
					result.Add("9", "PremioMetaLojaLoja"); 
				    
					result.Add("44", "PremioMetaLojaUF"); 
				    
					result.Add("13", "PremioMetaVendedorFuncionario"); 
				    
					result.Add("11", "PremioMetaVendedorGrupoEconomico"); 
				    
					result.Add("12", "PremioMetaVendedorLoja"); 
				    
					result.Add("40", "PremioMetaVendedorSuperarAdicionalFuncionario"); 
				    
					result.Add("38", "PremioMetaVendedorSuperarAdicionalGrupoEconomico"); 
				    
					result.Add("39", "PremioMetaVendedorSuperarAdicionalLoja"); 
				    
					result.Add("54", "PremioMetaVendedorSuperarAdicionalUF"); 
				    
					result.Add("33", "PremioMetaVendedorSuperarFuncionario"); 
				    
					result.Add("31", "PremioMetaVendedorSuperarGrupoEconomico"); 
				    
					result.Add("32", "PremioMetaVendedorSuperarLoja"); 
				    
					result.Add("52", "PremioMetaVendedorSuperarUF"); 
				    
					result.Add("45", "PremioMetaVendedorUF"); 
				    
					result.Add("25", "PremioMinimoQtdeCupomFuncionario"); 
				    
					result.Add("23", "PremioMinimoQtdeCupomGrupoEconomico"); 
				    
					result.Add("24", "PremioMinimoQtdeCupomLoja"); 
				    
					result.Add("49", "PremioMinimoQtdeCupomUF"); 
				    
					result.Add("28", "PremioMinimoValorCupomFuncionario"); 
				    
					result.Add("26", "PremioMinimoValorCupomGrupoEconomico"); 
				    
					result.Add("27", "PremioMinimoValorCupomLoja"); 
				    
					result.Add("50", "PremioMinimoValorCupomUF"); 
				    
					result.Add("16", "PremioTicketMedioFuncionario"); 
				    
					result.Add("14", "PremioTicketMedioGrupoEconomico"); 
				    
					result.Add("15", "PremioTicketMedioLoja"); 
				    
					result.Add("46", "PremioTicketMedioUF"); 
				    
					result.Add("29", "PremioTipoPagamentoGrupoEconomico"); 
				    
					result.Add("30", "PremioTipoPagamentoLoja"); 
				    
					result.Add("51", "PremioTipoPagamentoUF"); 
				    
					result.Add("19", "PremioVendaCartaoPresenteFuncionario"); 
				    
					result.Add("17", "PremioVendaCartaoPresenteGrupoEconomico"); 
				    
					result.Add("18", "PremioVendaCartaoPresenteLoja"); 
				    
					result.Add("47", "PremioVendaCartaoPresenteUF"); 
				    
					result.Add("20", "PremioVendaProdutoGrupoEconomico"); 
				    
					result.Add("21", "PremioVendaProdutoLoja"); 
				    
					result.Add("48", "PremioVendaProdutoUF"); 
				    
					result.Add("37", "PremioVendaProdutoVendedor"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _AdicionalGovernoFuncionario = new DomainKeyPair() { Value = "36", DisplayName = "Funcionário - Adicional - Governo" };
					[FunctionalPoint("Value[36];DisplayName[Funcionário - Adicional - Governo]")]
					public static DomainKeyPair AdicionalGovernoFuncionario { get { return _AdicionalGovernoFuncionario; } }
				    
					private static DomainKeyPair _AdicionalGovernoGrupoEconomico = new DomainKeyPair() { Value = "34", DisplayName = "Grupo Econômico - Adicional - Governo" };
					[FunctionalPoint("Value[34];DisplayName[Grupo Econômico - Adicional - Governo]")]
					public static DomainKeyPair AdicionalGovernoGrupoEconomico { get { return _AdicionalGovernoGrupoEconomico; } }
				    
					private static DomainKeyPair _AdicionalGovernoLoja = new DomainKeyPair() { Value = "35", DisplayName = "Loja - Adicional - Governo" };
					[FunctionalPoint("Value[35];DisplayName[Loja - Adicional - Governo]")]
					public static DomainKeyPair AdicionalGovernoLoja { get { return _AdicionalGovernoLoja; } }
				    
					private static DomainKeyPair _AdicionalGovernoUF = new DomainKeyPair() { Value = "53", DisplayName = "UF - Adicional - Governo" };
					[FunctionalPoint("Value[53];DisplayName[UF - Adicional - Governo]")]
					public static DomainKeyPair AdicionalGovernoUF { get { return _AdicionalGovernoUF; } }
				    
					private static DomainKeyPair _ComissaoBaseFuncionario = new DomainKeyPair() { Value = "3", DisplayName = "Funcionário - Comissão Base" };
					[FunctionalPoint("Value[3];DisplayName[Funcionário - Comissão Base]")]
					public static DomainKeyPair ComissaoBaseFuncionario { get { return _ComissaoBaseFuncionario; } }
				    
					private static DomainKeyPair _ComissaoBaseGrupoEconomico = new DomainKeyPair() { Value = "1", DisplayName = "Grupo Econômico - Comissão Base" };
					[FunctionalPoint("Value[1];DisplayName[Grupo Econômico - Comissão Base]")]
					public static DomainKeyPair ComissaoBaseGrupoEconomico { get { return _ComissaoBaseGrupoEconomico; } }
				    
					private static DomainKeyPair _ComissaoBaseLoja = new DomainKeyPair() { Value = "2", DisplayName = "Loja - Comissão Base" };
					[FunctionalPoint("Value[2];DisplayName[Loja - Comissão Base]")]
					public static DomainKeyPair ComissaoBaseLoja { get { return _ComissaoBaseLoja; } }
				    
					private static DomainKeyPair _ComissaoBaseOperacaoGrupoEconomico = new DomainKeyPair() { Value = "4", DisplayName = "Grupo Econômico - Comissão Base - Operação" };
					[FunctionalPoint("Value[4];DisplayName[Grupo Econômico - Comissão Base - Operação]")]
					public static DomainKeyPair ComissaoBaseOperacaoGrupoEconomico { get { return _ComissaoBaseOperacaoGrupoEconomico; } }
				    
					private static DomainKeyPair _ComissaoBaseOperacaoLoja = new DomainKeyPair() { Value = "5", DisplayName = "Loja - Comissão Base - Operação" };
					[FunctionalPoint("Value[5];DisplayName[Loja - Comissão Base - Operação]")]
					public static DomainKeyPair ComissaoBaseOperacaoLoja { get { return _ComissaoBaseOperacaoLoja; } }
				    
					private static DomainKeyPair _ComissaoBaseOperacaoUF = new DomainKeyPair() { Value = "42", DisplayName = "UF - Comissão Base - Operação" };
					[FunctionalPoint("Value[42];DisplayName[UF - Comissão Base - Operação]")]
					public static DomainKeyPair ComissaoBaseOperacaoUF { get { return _ComissaoBaseOperacaoUF; } }
				    
					private static DomainKeyPair _ComissaoBasePromocaoGrupoEconomico = new DomainKeyPair() { Value = "6", DisplayName = "Grupo Econômico - Comissão Base - Promoção" };
					[FunctionalPoint("Value[6];DisplayName[Grupo Econômico - Comissão Base - Promoção]")]
					public static DomainKeyPair ComissaoBasePromocaoGrupoEconomico { get { return _ComissaoBasePromocaoGrupoEconomico; } }
				    
					private static DomainKeyPair _ComissaoBasePromocaoLoja = new DomainKeyPair() { Value = "7", DisplayName = "Loja - Comissão Base - Promoção" };
					[FunctionalPoint("Value[7];DisplayName[Loja - Comissão Base - Promoção]")]
					public static DomainKeyPair ComissaoBasePromocaoLoja { get { return _ComissaoBasePromocaoLoja; } }
				    
					private static DomainKeyPair _ComissaoBasePromocaoUF = new DomainKeyPair() { Value = "43", DisplayName = "UF - Comissão Base - Promoção" };
					[FunctionalPoint("Value[43];DisplayName[UF - Comissão Base - Promoção]")]
					public static DomainKeyPair ComissaoBasePromocaoUF { get { return _ComissaoBasePromocaoUF; } }
				    
					private static DomainKeyPair _ComissaoBaseUF = new DomainKeyPair() { Value = "41", DisplayName = "UF - Comissão Base" };
					[FunctionalPoint("Value[41];DisplayName[UF - Comissão Base]")]
					public static DomainKeyPair ComissaoBaseUF { get { return _ComissaoBaseUF; } }
				    
					private static DomainKeyPair _FonteExternaValores = new DomainKeyPair() { Value = "98", DisplayName = "Fonte Externa de Valores" };
					[FunctionalPoint("Value[98];DisplayName[Fonte Externa de Valores]")]
					public static DomainKeyPair FonteExternaValores { get { return _FonteExternaValores; } }
				    
					private static DomainKeyPair _NaoElegivel = new DomainKeyPair() { Value = "99", DisplayName = "Não Elegível" };
					[FunctionalPoint("Value[99];DisplayName[Não Elegível]")]
					public static DomainKeyPair NaoElegivel { get { return _NaoElegivel; } }
				    
					private static DomainKeyPair _PremioMetaLojaFuncionario = new DomainKeyPair() { Value = "10", DisplayName = "Funcionário - Prêmio - Meta Loja" };
					[FunctionalPoint("Value[10];DisplayName[Funcionário - Prêmio - Meta Loja]")]
					public static DomainKeyPair PremioMetaLojaFuncionario { get { return _PremioMetaLojaFuncionario; } }
				    
					private static DomainKeyPair _PremioMetaLojaGrupoEconomico = new DomainKeyPair() { Value = "8", DisplayName = "Grupo Econômico - Prêmio - Meta Loja" };
					[FunctionalPoint("Value[8];DisplayName[Grupo Econômico - Prêmio - Meta Loja]")]
					public static DomainKeyPair PremioMetaLojaGrupoEconomico { get { return _PremioMetaLojaGrupoEconomico; } }
				    
					private static DomainKeyPair _PremioMetaLojaLoja = new DomainKeyPair() { Value = "9", DisplayName = "Loja - Prêmio - Meta Loja" };
					[FunctionalPoint("Value[9];DisplayName[Loja - Prêmio - Meta Loja]")]
					public static DomainKeyPair PremioMetaLojaLoja { get { return _PremioMetaLojaLoja; } }
				    
					private static DomainKeyPair _PremioMetaLojaUF = new DomainKeyPair() { Value = "44", DisplayName = "UF - Prêmio - Meta Loja" };
					[FunctionalPoint("Value[44];DisplayName[UF - Prêmio - Meta Loja]")]
					public static DomainKeyPair PremioMetaLojaUF { get { return _PremioMetaLojaUF; } }
				    
					private static DomainKeyPair _PremioMetaVendedorFuncionario = new DomainKeyPair() { Value = "13", DisplayName = "Funcionário - Prêmio - Meta Vendedor" };
					[FunctionalPoint("Value[13];DisplayName[Funcionário - Prêmio - Meta Vendedor]")]
					public static DomainKeyPair PremioMetaVendedorFuncionario { get { return _PremioMetaVendedorFuncionario; } }
				    
					private static DomainKeyPair _PremioMetaVendedorGrupoEconomico = new DomainKeyPair() { Value = "11", DisplayName = "Grupo Econômico - Prêmio - Meta Vendedor" };
					[FunctionalPoint("Value[11];DisplayName[Grupo Econômico - Prêmio - Meta Vendedor]")]
					public static DomainKeyPair PremioMetaVendedorGrupoEconomico { get { return _PremioMetaVendedorGrupoEconomico; } }
				    
					private static DomainKeyPair _PremioMetaVendedorLoja = new DomainKeyPair() { Value = "12", DisplayName = "Loja - Prêmio - Meta Vendedor" };
					[FunctionalPoint("Value[12];DisplayName[Loja - Prêmio - Meta Vendedor]")]
					public static DomainKeyPair PremioMetaVendedorLoja { get { return _PremioMetaVendedorLoja; } }
				    
					private static DomainKeyPair _PremioMetaVendedorSuperarAdicionalFuncionario = new DomainKeyPair() { Value = "40", DisplayName = "Funcionário - Prêmio - Adicional de Superação de Meta" };
					[FunctionalPoint("Value[40];DisplayName[Funcionário - Prêmio - Adicional de Superação de Meta]")]
					public static DomainKeyPair PremioMetaVendedorSuperarAdicionalFuncionario { get { return _PremioMetaVendedorSuperarAdicionalFuncionario; } }
				    
					private static DomainKeyPair _PremioMetaVendedorSuperarAdicionalGrupoEconomico = new DomainKeyPair() { Value = "38", DisplayName = "Grupo Econômico - Prêmio - Adicional de Superação de Meta" };
					[FunctionalPoint("Value[38];DisplayName[Grupo Econômico - Prêmio - Adicional de Superação de Meta]")]
					public static DomainKeyPair PremioMetaVendedorSuperarAdicionalGrupoEconomico { get { return _PremioMetaVendedorSuperarAdicionalGrupoEconomico; } }
				    
					private static DomainKeyPair _PremioMetaVendedorSuperarAdicionalLoja = new DomainKeyPair() { Value = "39", DisplayName = "Loja - Prêmio - Adicional de Superação de Meta" };
					[FunctionalPoint("Value[39];DisplayName[Loja - Prêmio - Adicional de Superação de Meta]")]
					public static DomainKeyPair PremioMetaVendedorSuperarAdicionalLoja { get { return _PremioMetaVendedorSuperarAdicionalLoja; } }
				    
					private static DomainKeyPair _PremioMetaVendedorSuperarAdicionalUF = new DomainKeyPair() { Value = "54", DisplayName = "UF - Prêmio - Adicional de Superação de Meta" };
					[FunctionalPoint("Value[54];DisplayName[UF - Prêmio - Adicional de Superação de Meta]")]
					public static DomainKeyPair PremioMetaVendedorSuperarAdicionalUF { get { return _PremioMetaVendedorSuperarAdicionalUF; } }
				    
					private static DomainKeyPair _PremioMetaVendedorSuperarFuncionario = new DomainKeyPair() { Value = "33", DisplayName = "Funcionário - Prêmio - Superação de Meta" };
					[FunctionalPoint("Value[33];DisplayName[Funcionário - Prêmio - Superação de Meta]")]
					public static DomainKeyPair PremioMetaVendedorSuperarFuncionario { get { return _PremioMetaVendedorSuperarFuncionario; } }
				    
					private static DomainKeyPair _PremioMetaVendedorSuperarGrupoEconomico = new DomainKeyPair() { Value = "31", DisplayName = "Grupo Econômico - Prêmio - Superação de Meta" };
					[FunctionalPoint("Value[31];DisplayName[Grupo Econômico - Prêmio - Superação de Meta]")]
					public static DomainKeyPair PremioMetaVendedorSuperarGrupoEconomico { get { return _PremioMetaVendedorSuperarGrupoEconomico; } }
				    
					private static DomainKeyPair _PremioMetaVendedorSuperarLoja = new DomainKeyPair() { Value = "32", DisplayName = "Loja - Prêmio - Superação de Meta" };
					[FunctionalPoint("Value[32];DisplayName[Loja - Prêmio - Superação de Meta]")]
					public static DomainKeyPair PremioMetaVendedorSuperarLoja { get { return _PremioMetaVendedorSuperarLoja; } }
				    
					private static DomainKeyPair _PremioMetaVendedorSuperarUF = new DomainKeyPair() { Value = "52", DisplayName = "UF - Prêmio - Superação de Meta" };
					[FunctionalPoint("Value[52];DisplayName[UF - Prêmio - Superação de Meta]")]
					public static DomainKeyPair PremioMetaVendedorSuperarUF { get { return _PremioMetaVendedorSuperarUF; } }
				    
					private static DomainKeyPair _PremioMetaVendedorUF = new DomainKeyPair() { Value = "45", DisplayName = "UF - Prêmio - Meta Vendedor" };
					[FunctionalPoint("Value[45];DisplayName[UF - Prêmio - Meta Vendedor]")]
					public static DomainKeyPair PremioMetaVendedorUF { get { return _PremioMetaVendedorUF; } }
				    
					private static DomainKeyPair _PremioMinimoQtdeCupomFuncionario = new DomainKeyPair() { Value = "25", DisplayName = "Funcionário - Prêmio - Quantidade de Cupons" };
					[FunctionalPoint("Value[25];DisplayName[Funcionário - Prêmio - Quantidade de Cupons]")]
					public static DomainKeyPair PremioMinimoQtdeCupomFuncionario { get { return _PremioMinimoQtdeCupomFuncionario; } }
				    
					private static DomainKeyPair _PremioMinimoQtdeCupomGrupoEconomico = new DomainKeyPair() { Value = "23", DisplayName = "Grupo Econômico - Prêmio - Quantidade de Cupons" };
					[FunctionalPoint("Value[23];DisplayName[Grupo Econômico - Prêmio - Quantidade de Cupons]")]
					public static DomainKeyPair PremioMinimoQtdeCupomGrupoEconomico { get { return _PremioMinimoQtdeCupomGrupoEconomico; } }
				    
					private static DomainKeyPair _PremioMinimoQtdeCupomLoja = new DomainKeyPair() { Value = "24", DisplayName = "Loja - Prêmio - Quantidade de Cupons" };
					[FunctionalPoint("Value[24];DisplayName[Loja - Prêmio - Quantidade de Cupons]")]
					public static DomainKeyPair PremioMinimoQtdeCupomLoja { get { return _PremioMinimoQtdeCupomLoja; } }
				    
					private static DomainKeyPair _PremioMinimoQtdeCupomUF = new DomainKeyPair() { Value = "49", DisplayName = "UF - Prêmio - Quantidade de Cupons" };
					[FunctionalPoint("Value[49];DisplayName[UF - Prêmio - Quantidade de Cupons]")]
					public static DomainKeyPair PremioMinimoQtdeCupomUF { get { return _PremioMinimoQtdeCupomUF; } }
				    
					private static DomainKeyPair _PremioMinimoValorCupomFuncionario = new DomainKeyPair() { Value = "28", DisplayName = "Funcionário - Prêmio - Valor de Cupom" };
					[FunctionalPoint("Value[28];DisplayName[Funcionário - Prêmio - Valor de Cupom]")]
					public static DomainKeyPair PremioMinimoValorCupomFuncionario { get { return _PremioMinimoValorCupomFuncionario; } }
				    
					private static DomainKeyPair _PremioMinimoValorCupomGrupoEconomico = new DomainKeyPair() { Value = "26", DisplayName = "Grupo Econômico - Prêmio - Valor de Cupom" };
					[FunctionalPoint("Value[26];DisplayName[Grupo Econômico - Prêmio - Valor de Cupom]")]
					public static DomainKeyPair PremioMinimoValorCupomGrupoEconomico { get { return _PremioMinimoValorCupomGrupoEconomico; } }
				    
					private static DomainKeyPair _PremioMinimoValorCupomLoja = new DomainKeyPair() { Value = "27", DisplayName = "Loja - Prêmio - Valor de Cupom" };
					[FunctionalPoint("Value[27];DisplayName[Loja - Prêmio - Valor de Cupom]")]
					public static DomainKeyPair PremioMinimoValorCupomLoja { get { return _PremioMinimoValorCupomLoja; } }
				    
					private static DomainKeyPair _PremioMinimoValorCupomUF = new DomainKeyPair() { Value = "50", DisplayName = "UF - Prêmio - Valor de Cupom" };
					[FunctionalPoint("Value[50];DisplayName[UF - Prêmio - Valor de Cupom]")]
					public static DomainKeyPair PremioMinimoValorCupomUF { get { return _PremioMinimoValorCupomUF; } }
				    
					private static DomainKeyPair _PremioTicketMedioFuncionario = new DomainKeyPair() { Value = "16", DisplayName = "Funcionário - Prêmio - Ticket Médio" };
					[FunctionalPoint("Value[16];DisplayName[Funcionário - Prêmio - Ticket Médio]")]
					public static DomainKeyPair PremioTicketMedioFuncionario { get { return _PremioTicketMedioFuncionario; } }
				    
					private static DomainKeyPair _PremioTicketMedioGrupoEconomico = new DomainKeyPair() { Value = "14", DisplayName = "Grupo Econômico - Prêmio - Ticket Médio" };
					[FunctionalPoint("Value[14];DisplayName[Grupo Econômico - Prêmio - Ticket Médio]")]
					public static DomainKeyPair PremioTicketMedioGrupoEconomico { get { return _PremioTicketMedioGrupoEconomico; } }
				    
					private static DomainKeyPair _PremioTicketMedioLoja = new DomainKeyPair() { Value = "15", DisplayName = "Loja - Prêmio - Ticket Médio" };
					[FunctionalPoint("Value[15];DisplayName[Loja - Prêmio - Ticket Médio]")]
					public static DomainKeyPair PremioTicketMedioLoja { get { return _PremioTicketMedioLoja; } }
				    
					private static DomainKeyPair _PremioTicketMedioUF = new DomainKeyPair() { Value = "46", DisplayName = "UF - Prêmio - Ticket Médio" };
					[FunctionalPoint("Value[46];DisplayName[UF - Prêmio - Ticket Médio]")]
					public static DomainKeyPair PremioTicketMedioUF { get { return _PremioTicketMedioUF; } }
				    
					private static DomainKeyPair _PremioTipoPagamentoGrupoEconomico = new DomainKeyPair() { Value = "29", DisplayName = "Grupo Econômico - Prêmio - Tipo Pagamento" };
					[FunctionalPoint("Value[29];DisplayName[Grupo Econômico - Prêmio - Tipo Pagamento]")]
					public static DomainKeyPair PremioTipoPagamentoGrupoEconomico { get { return _PremioTipoPagamentoGrupoEconomico; } }
				    
					private static DomainKeyPair _PremioTipoPagamentoLoja = new DomainKeyPair() { Value = "30", DisplayName = "Loja - Prêmio - Tipo Pagamento" };
					[FunctionalPoint("Value[30];DisplayName[Loja - Prêmio - Tipo Pagamento]")]
					public static DomainKeyPair PremioTipoPagamentoLoja { get { return _PremioTipoPagamentoLoja; } }
				    
					private static DomainKeyPair _PremioTipoPagamentoUF = new DomainKeyPair() { Value = "51", DisplayName = "UF - Prêmio - Tipo Pagamento" };
					[FunctionalPoint("Value[51];DisplayName[UF - Prêmio - Tipo Pagamento]")]
					public static DomainKeyPair PremioTipoPagamentoUF { get { return _PremioTipoPagamentoUF; } }
				    
					private static DomainKeyPair _PremioVendaCartaoPresenteFuncionario = new DomainKeyPair() { Value = "19", DisplayName = "Funcionário - Prêmio - Venda Cartão Presente" };
					[FunctionalPoint("Value[19];DisplayName[Funcionário - Prêmio - Venda Cartão Presente]")]
					public static DomainKeyPair PremioVendaCartaoPresenteFuncionario { get { return _PremioVendaCartaoPresenteFuncionario; } }
				    
					private static DomainKeyPair _PremioVendaCartaoPresenteGrupoEconomico = new DomainKeyPair() { Value = "17", DisplayName = "Grupo Econômico - Prêmio - Venda Cartão Presente" };
					[FunctionalPoint("Value[17];DisplayName[Grupo Econômico - Prêmio - Venda Cartão Presente]")]
					public static DomainKeyPair PremioVendaCartaoPresenteGrupoEconomico { get { return _PremioVendaCartaoPresenteGrupoEconomico; } }
				    
					private static DomainKeyPair _PremioVendaCartaoPresenteLoja = new DomainKeyPair() { Value = "18", DisplayName = "Loja - Prêmio - Venda Cartão Presente" };
					[FunctionalPoint("Value[18];DisplayName[Loja - Prêmio - Venda Cartão Presente]")]
					public static DomainKeyPair PremioVendaCartaoPresenteLoja { get { return _PremioVendaCartaoPresenteLoja; } }
				    
					private static DomainKeyPair _PremioVendaCartaoPresenteUF = new DomainKeyPair() { Value = "47", DisplayName = "UF - Prêmio - Venda Cartão Presente" };
					[FunctionalPoint("Value[47];DisplayName[UF - Prêmio - Venda Cartão Presente]")]
					public static DomainKeyPair PremioVendaCartaoPresenteUF { get { return _PremioVendaCartaoPresenteUF; } }
				    
					private static DomainKeyPair _PremioVendaProdutoGrupoEconomico = new DomainKeyPair() { Value = "20", DisplayName = "Grupo Econômico - Prêmio - Venda Produto" };
					[FunctionalPoint("Value[20];DisplayName[Grupo Econômico - Prêmio - Venda Produto]")]
					public static DomainKeyPair PremioVendaProdutoGrupoEconomico { get { return _PremioVendaProdutoGrupoEconomico; } }
				    
					private static DomainKeyPair _PremioVendaProdutoLoja = new DomainKeyPair() { Value = "21", DisplayName = "Loja - Prêmio - Venda Produto" };
					[FunctionalPoint("Value[21];DisplayName[Loja - Prêmio - Venda Produto]")]
					public static DomainKeyPair PremioVendaProdutoLoja { get { return _PremioVendaProdutoLoja; } }
				    
					private static DomainKeyPair _PremioVendaProdutoUF = new DomainKeyPair() { Value = "48", DisplayName = "UF - Prêmio - Venda Produto" };
					[FunctionalPoint("Value[48];DisplayName[UF - Prêmio - Venda Produto]")]
					public static DomainKeyPair PremioVendaProdutoUF { get { return _PremioVendaProdutoUF; } }
				    
					private static DomainKeyPair _PremioVendaProdutoVendedor = new DomainKeyPair() { Value = "37", DisplayName = "Vendedor - Prêmio - Venda Produto" };
					[FunctionalPoint("Value[37];DisplayName[Vendedor - Prêmio - Venda Produto]")]
					public static DomainKeyPair PremioVendaProdutoVendedor { get { return _PremioVendaProdutoVendedor; } }
				    
			#endregion properties

			

	}    
	//<LX_FUNCAO_VENDEDOR>((#LxExpr#) == [-3-] ? "Caixa" : ((#LxExpr#) == [-2-] ? "Gerente" : ((#LxExpr#) == [-1-] ? "Vendedor" : "")))</LX_FUNCAO_VENDEDOR>	
    public partial class LX_FUNCAO_VENDEDOR
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("3", "Caixa"); 
				    
					result.Add("2", "Gerente"); 
				    
					result.Add("1", "Vendedor"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("3", "Caixa"); 
				    
					result.Add("2", "Gerente"); 
				    
					result.Add("1", "Vendedor"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Caixa = new DomainKeyPair() { Value = "3", DisplayName = "Caixa" };
					[FunctionalPoint("Value[3];DisplayName[Caixa]")]
					public static DomainKeyPair Caixa { get { return _Caixa; } }
				    
					private static DomainKeyPair _Gerente = new DomainKeyPair() { Value = "2", DisplayName = "Gerente" };
					[FunctionalPoint("Value[2];DisplayName[Gerente]")]
					public static DomainKeyPair Gerente { get { return _Gerente; } }
				    
					private static DomainKeyPair _Vendedor = new DomainKeyPair() { Value = "1", DisplayName = "Vendedor" };
					[FunctionalPoint("Value[1];DisplayName[Vendedor]")]
					public static DomainKeyPair Vendedor { get { return _Vendedor; } }
				    
			#endregion properties

			

	}    
	//<LX_TIPO_COMISSAO>((#LxExpr#) == [-1-] ? "Oficial" : ((#LxExpr#) == [-2-] ? "Simulação" : ""))</LX_TIPO_COMISSAO>	
    public partial class LX_TIPO_COMISSAO
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Oficial"); 
				    
					result.Add("2", "Simulação"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("1", "Oficial"); 
				    
					result.Add("2", "Simulação"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _Oficial = new DomainKeyPair() { Value = "1", DisplayName = "Oficial" };
					[FunctionalPoint("Value[1];DisplayName[Oficial]")]
					public static DomainKeyPair Oficial { get { return _Oficial; } }
				    
					private static DomainKeyPair _Simulação = new DomainKeyPair() { Value = "2", DisplayName = "Simulação" };
					[FunctionalPoint("Value[2];DisplayName[Simulação]")]
					public static DomainKeyPair Simulação { get { return _Simulação; } }
				    
			#endregion properties

			

	}    
	//<LX_TIPO_NF_RELACAO>((#LxExpr#) == [-60-] ? "À Ordem" : ((#LxExpr#) == [-91-] ? "À Retornar" : ((#LxExpr#) == [-99-] ? "Complementar" : ((#LxExpr#) == [-70-] ? "CTRC de NF" : ((#LxExpr#) == [-20-] ? "Devolução" : ((#LxExpr#) == [-51-] ? "Devolução de Transferência" : ((#LxExpr#) == [-81-] ? "Entrega Futura" : ((#LxExpr#) == [-71-] ? "Fatura Frete" : ((#LxExpr#) == [-10-] ? "Não Relacionada" : ((#LxExpr#) == [-90-] ? "NF de Cupom Fiscal" : ((#LxExpr#) == [-82-] ? "Recebimento Futuro" : ((#LxExpr#) == [-30-] ? "Retorno Físico" : ((#LxExpr#) == [-31-] ? "Retorno Simbólico" : ((#LxExpr#) == [-80-] ? "Simples Faturamento" : ((#LxExpr#) == [-50-] ? "Transferência" : "")))))))))))))))</LX_TIPO_NF_RELACAO>	
    public partial class LX_TIPO_NF_RELACAO
    {
					
			public static Dictionary<string, string> GetValues()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("60", "À Ordem"); 
				    
					result.Add("91", "À Retornar"); 
				    
					result.Add("99", "Complementar"); 
				    
					result.Add("70", "CTRC de NF"); 
				    
					result.Add("20", "Devolução"); 
				    
					result.Add("51", "Devolução de Transferência"); 
				    
					result.Add("81", "Entrega Futura"); 
				    
					result.Add("71", "Fatura Frete"); 
				    
					result.Add("10", "Não Relacionada"); 
				    
					result.Add("90", "NF de Cupom Fiscal"); 
				    
					result.Add("82", "Recebimento Futuro"); 
				    
					result.Add("30", "Retorno Físico"); 
				    
					result.Add("31", "Retorno Simbólico"); 
				    
					result.Add("80", "Simples Faturamento"); 
				    
					result.Add("50", "Transferência"); 
				    
					return result;
			}

			public static Dictionary<string, string> GetNames()
			{
					Dictionary<string, string> result = new Dictionary<string, string>();				
				
					result.Add("60", "aOrdem"); 
				    
					result.Add("91", "aRetornar"); 
				    
					result.Add("99", "Complementar"); 
				    
					result.Add("70", "CTRCdeNF"); 
				    
					result.Add("20", "Devolucao"); 
				    
					result.Add("51", "DevolucaoTransferencia"); 
				    
					result.Add("81", "EntregaFutura"); 
				    
					result.Add("71", "FaturaFrete"); 
				    
					result.Add("10", "NaoRelacionada"); 
				    
					result.Add("90", "NFdeCupomFiscal"); 
				    
					result.Add("82", "RecebimentoFuturo"); 
				    
					result.Add("30", "RetornoFisico"); 
				    
					result.Add("31", "RetornoSimbolico"); 
				    
					result.Add("80", "SimplesFaturamento"); 
				    
					result.Add("50", "Transferencia"); 
				    
					return result;
			}

			#region properties
			
					private static DomainKeyPair _aOrdem = new DomainKeyPair() { Value = "60", DisplayName = "À Ordem" };
					[FunctionalPoint("Value[60];DisplayName[À Ordem]")]
					public static DomainKeyPair aOrdem { get { return _aOrdem; } }
				    
					private static DomainKeyPair _aRetornar = new DomainKeyPair() { Value = "91", DisplayName = "À Retornar" };
					[FunctionalPoint("Value[91];DisplayName[À Retornar]")]
					public static DomainKeyPair aRetornar { get { return _aRetornar; } }
				    
					private static DomainKeyPair _Complementar = new DomainKeyPair() { Value = "99", DisplayName = "Complementar" };
					[FunctionalPoint("Value[99];DisplayName[Complementar]")]
					public static DomainKeyPair Complementar { get { return _Complementar; } }
				    
					private static DomainKeyPair _CTRCdeNF = new DomainKeyPair() { Value = "70", DisplayName = "CTRC de NF" };
					[FunctionalPoint("Value[70];DisplayName[CTRC de NF]")]
					public static DomainKeyPair CTRCdeNF { get { return _CTRCdeNF; } }
				    
					private static DomainKeyPair _Devolucao = new DomainKeyPair() { Value = "20", DisplayName = "Devolução" };
					[FunctionalPoint("Value[20];DisplayName[Devolução]")]
					public static DomainKeyPair Devolucao { get { return _Devolucao; } }
				    
					private static DomainKeyPair _DevolucaoTransferencia = new DomainKeyPair() { Value = "51", DisplayName = "Devolução de Transferência" };
					[FunctionalPoint("Value[51];DisplayName[Devolução de Transferência]")]
					public static DomainKeyPair DevolucaoTransferencia { get { return _DevolucaoTransferencia; } }
				    
					private static DomainKeyPair _EntregaFutura = new DomainKeyPair() { Value = "81", DisplayName = "Entrega Futura" };
					[FunctionalPoint("Value[81];DisplayName[Entrega Futura]")]
					public static DomainKeyPair EntregaFutura { get { return _EntregaFutura; } }
				    
					private static DomainKeyPair _FaturaFrete = new DomainKeyPair() { Value = "71", DisplayName = "Fatura Frete" };
					[FunctionalPoint("Value[71];DisplayName[Fatura Frete]")]
					public static DomainKeyPair FaturaFrete { get { return _FaturaFrete; } }
				    
					private static DomainKeyPair _NaoRelacionada = new DomainKeyPair() { Value = "10", DisplayName = "Não Relacionada" };
					[FunctionalPoint("Value[10];DisplayName[Não Relacionada]")]
					public static DomainKeyPair NaoRelacionada { get { return _NaoRelacionada; } }
				    
					private static DomainKeyPair _NFdeCupomFiscal = new DomainKeyPair() { Value = "90", DisplayName = "NF de Cupom Fiscal" };
					[FunctionalPoint("Value[90];DisplayName[NF de Cupom Fiscal]")]
					public static DomainKeyPair NFdeCupomFiscal { get { return _NFdeCupomFiscal; } }
				    
					private static DomainKeyPair _RecebimentoFuturo = new DomainKeyPair() { Value = "82", DisplayName = "Recebimento Futuro" };
					[FunctionalPoint("Value[82];DisplayName[Recebimento Futuro]")]
					public static DomainKeyPair RecebimentoFuturo { get { return _RecebimentoFuturo; } }
				    
					private static DomainKeyPair _RetornoFisico = new DomainKeyPair() { Value = "30", DisplayName = "Retorno Físico" };
					[FunctionalPoint("Value[30];DisplayName[Retorno Físico]")]
					public static DomainKeyPair RetornoFisico { get { return _RetornoFisico; } }
				    
					private static DomainKeyPair _RetornoSimbolico = new DomainKeyPair() { Value = "31", DisplayName = "Retorno Simbólico" };
					[FunctionalPoint("Value[31];DisplayName[Retorno Simbólico]")]
					public static DomainKeyPair RetornoSimbolico { get { return _RetornoSimbolico; } }
				    
					private static DomainKeyPair _SimplesFaturamento = new DomainKeyPair() { Value = "80", DisplayName = "Simples Faturamento" };
					[FunctionalPoint("Value[80];DisplayName[Simples Faturamento]")]
					public static DomainKeyPair SimplesFaturamento { get { return _SimplesFaturamento; } }
				    
					private static DomainKeyPair _Transferencia = new DomainKeyPair() { Value = "50", DisplayName = "Transferência" };
					[FunctionalPoint("Value[50];DisplayName[Transferência]")]
					public static DomainKeyPair Transferencia { get { return _Transferencia; } }
				    
			#endregion properties

			

	}    

}