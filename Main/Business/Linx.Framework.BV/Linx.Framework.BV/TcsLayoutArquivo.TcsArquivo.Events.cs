using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Linx.LinqExtensions.Query;
using Linx.LinqExtensions.Functional;
using Linx.LinqExtensions.Expressions;
using Linx;
using Linx.Tools;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
#if !SILVERLIGHT
using System.ServiceModel.DomainServices.Server;
using Linx.Data;
#endif
using System.Text;
using System.Data.Objects;
using System.Data.Common;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Data.Linq.SqlClient;
using System.Reflection;
using System.Data.Objects.DataClasses;
using Linx.Framework.ControleSistema.BM;
using System.Xml.Schema;
using System.Xml;
using System.IO;

namespace Linx.TCS0101.BO.TcsLayoutArquivo
{
	
	////////////////////////////////////////////////////////////////////////////
	////////////////////////// Business Events Definition //////////////////////
	////////////////////////////////////////////////////////////////////////////
	public partial class TcsArquivo
	{
        #region Propriedades

        /// <summary>
        /// Namespace padrao para ser inserido no arquivo XSD, caso não seja informado um namespace.
        /// </summary>
        private const string strNamespacePadrao = "http://www.w3.org/2001/XMLSchema";

        #endregion

        /// Execute before save changes.
        public void OnSavingChanges(Linx.TCS0101.BO.TcsLayoutArquivo.TcsLayoutArquivoDomainService context, ChangeOperation changeOperation)
        {
            if (changeOperation != ChangeOperation.Delete)
            {
                fvValidaArquivo(changeOperation);

                if (String.IsNullOrWhiteSpace(this.Xmlns))
                    this.Xmlns = strNamespacePadrao;

                //Gera o XSD a partir das definições de estrutura definidas na interface
                this.Xsd = frRetornaXSD();
            }
        }

        /// <summary>
        /// Valida as informações enviadas da interface para construção da estrutura do XML
        /// </summary>
        /// <param name="pchangeoperation">Operação executada</param>
        private void fvValidaArquivo(ChangeOperation pchangeoperation)
        {
            ExtendedControleSistemaContext edm = new ExtendedControleSistemaContext();

            #region Valida Arquivo
            if (this.Inativo == false)
            {
                //Valida se já existe um layout ativo com o mesmo nome.
                var varNomeExistente = (from arq in edm.TCS_ARQUIVO
                                        where arq.NOME_ARQUIVO == this.NomeArquivo
                                        && arq.ID_ARQUIVO != this.IdArquivo
                                        && arq.INATIVO == false
                                        select new { arq.NOME_ARQUIVO }).ToList();

                if (varNomeExistente.Count() > 0)
                    throw new Exception("Nome do arquivo já cadastrado.");

                //Valida a order de execução pelo grupo do XML
                //var varOrdemExistente = (from arq in edm.TCS_ARQUIVO
                //                         where arq.ID_ARQUIVO_GRUPO_FK == this.IdArquivoGrupo
                //                         && arq.ORDEM == this.Ordem
                //                         && arq.ID_ARQUIVO != this.IdArquivo
                //                         && arq.INATIVO == false
                //                         select new { arq.ID_ARQUIVO, arq.DESC_ARQUIVO, arq.TCS_ARQUIVO_GRUPO.COD_ARQUIVO_GRUPO, arq.TCS_ARQUIVO_GRUPO.DESC_ARQUIVO_GRUPO }).ToList();

                //if (varOrdemExistente.Count() > 0)
                //    throw new Exception(String.Format("Ordem do arquivo {0} - {1} já está associada ao arquivo {2} - {3}.", this.CodArquivo, this.DescArquivo, varOrdemExistente.First().ID_ARQUIVO, varOrdemExistente.First().DESC_ARQUIVO));
            }

            //Valida o tipo de arquivo
            Dictionary<string, string>.KeyCollection keyTipoArquivo = Domains.TipoArquivo.GetValues().Keys;
            if (!this.LxTipoArquivo.InList(keyTipoArquivo.ToArray()))
                throw new Exception("Campo tipo de arquivo inválido ou em branco.");

            if (pchangeoperation == ChangeOperation.Update)
            {
                if (!this.Inativo)
                {
                    var varArquivoAtivo = (from arq in edm.TCS_ARQUIVO
                                           where arq.NOME_ARQUIVO == this.NomeArquivo
                                           && arq.ID_ARQUIVO != this.IdArquivo
                                           && arq.INATIVO == false
                                           select new { arq.ID_ARQUIVO, arq.NOME_ARQUIVO }).ToList();

                    if (varArquivoAtivo.Count() > 0)
                        throw new Exception(String.Format("Nome do arquivo {0} já está ativo para o arquivo de ID {1}.", varArquivoAtivo.First().NOME_ARQUIVO, varArquivoAtivo.First().ID_ARQUIVO));
                }
            }

            if (this.TcsArquivoItemList.Count() <= 0)
                throw new Exception("É necessário informar ao menos um elemento do arquivo.");
            
            #endregion

            #region Valida Elementos

            //Verifica se existe ao menos um elemento para ser associado ao elemento raiz
            List<TcsArquivoItem> lstElemRaiz = (from elem in this.TcsArquivoItemList
                                                where elem.IdArquivoItemPai == null || elem.IdArquivoItemPai == 0
                                                select elem).ToList();

            if (lstElemRaiz.Count() <= 0)
                throw new Exception("Erro na estrutura do arquivo. Ao menos um elemento não deve possuir o elemento Pai para poder ser associado ao elemento raiz.");

            //Verifica se ordem dos elementos foi informada
            List<TcsArquivoItem> lstElemOrdemZero = (from elem in this.TcsArquivoItemList
                                                     where elem.Ordem == 0
                                                     select elem).ToList();

            if (lstElemOrdemZero.Count() > 0)
                throw new Exception(String.Format("Ordem do elemento {0} não pode ser 0 ou nulo.", lstElemOrdemZero[0].TagItem));


            //Verifica se possui duplicidade de ordens nos elementos
            var varElemGrupoPaiOrdem = (from elem1 in this.TcsArquivoItemList.GroupBy(g => new { g.IdArquivoItemPai, g.TagItemPai, g.Ordem })
                                        select new { IdArquivoItemPai = elem1.Key.IdArquivoItemPai, TagItemPai = elem1.Key.TagItemPai, Ordem = elem1.Key.Ordem, Quantidade = elem1.Count() }).Where(w => w.Quantidade > 1).ToList();

            if (varElemGrupoPaiOrdem.Count() > 0)
            {
                if (varElemGrupoPaiOrdem.First().IdArquivoItemPai == null)
                    throw new Exception("Existe duplicidade de ordem dos elementos.");
                else
                    throw new Exception(String.Format("Existe duplicidade de ordem no elemento {0}.", varElemGrupoPaiOrdem.First().TagItemPai));
            }

            //Verifica se possui elementos com o mesmo nome
            var varMesmoNome = (from elem1 in this.TcsArquivoItemList.GroupBy(g => new { g.TagItem })
                                select new { TagItem = elem1.Key.TagItem, Quantidade = elem1.Count() }).Where(w => w.Quantidade > 1).ToList();

            if (varMesmoNome.Count() > 0)
                throw new Exception(String.Format("Existe duplicidade do elemento de nome {0}.",varMesmoNome.First().TagItem));

            #endregion

            #region Valida Campos dos Elementos

            foreach (var elemento in this.TcsArquivoItemList.ToList())
            {
                List<TcsArquivoItemCampo> lstCampoOrdemZero = (from campo in elemento.TcsArquivoItemCampoList
                                                               where campo.Ordem == 0
                                                               select campo).ToList();

                //Verifica se ordem dos campos do elemento foi informada
                if (lstCampoOrdemZero.Count() > 0)
                    throw new Exception(String.Format("Ordem do campo {0} no elemento {1} não pode ser 0 ou nulo.", lstCampoOrdemZero[0].TagCampo, elemento.TagItem ));

                var varCamposOrdemIgual = (from campo1 in elemento.TcsArquivoItemCampoList.GroupBy(g => g.Ordem)
                                           select new { campo1.Key }).ToList();

                //Verifica se possui duplicidade de ordens nos campos do elemento
                if (varCamposOrdemIgual.Count() < elemento.TcsArquivoItemCampoList.Count())
                    throw new Exception(String.Format("Existe duplicidade de ordem de campos no elemento {0}.", elemento.TagItem));

                if (this.LxTipoArquivo == Domains.TipoArquivo.Todos.Value.ToString() ||
                    this.LxTipoArquivo == Domains.TipoArquivo.Excel.Value.ToString() ||
                    this.LxTipoArquivo == Domains.TipoArquivo.Text.Value.ToString())
                {
                    var varChavePk = from chave in elemento.TcsArquivoItemCampoList
                                        where chave.IndicaPk == true
                                        select chave.TagCampo;

                    //Verifica se elemento possui chave primaria
                    if (varChavePk.Count() <= 0)
                        throw new Exception(String.Format("Elemento {0} não possui nenhum campo como chave primária.", elemento.TagItem));

                    //Verifica se elemento possui somente um campo como chave primaria
                    if (varChavePk.Count() > 1)
                        throw new Exception(String.Format("Elemento {0} deve conter somente um campo como chave primária.", elemento.TagItem));

                    if (!elemento.IdArquivoItemPai.IsNullOrEmpty())
                    {
                        var varchavePai = from ch in this.TcsArquivoItemList
                                            where ch.IdArquivoItem == (int)elemento.IdArquivoItemPai
                                            select ch.TcsArquivoItemCampoList.Where(w => w.IndicaPk == true).Select(s => s.TagCampo).First();

                        var varcampoFilha = from ch in elemento.TcsArquivoItemCampoList
                                            where ch.TagCampo == varchavePai.First()
                                            select ch.IdArquivoItemCampo;

                        if (varcampoFilha.Count() <= 0)
                            throw new Exception(String.Format("Elemento {0} deve conter o campo chave {1}.", elemento.TagItem, varchavePai.First()));
                    }

                    if (this.LxTipoArquivo == Domains.TipoArquivo.Text.Value.ToString() ||
                        this.LxTipoArquivo == Domains.TipoArquivo.Todos.Value.ToString())
                    {
                        var varChaveID = from chave in elemento.TcsArquivoItemCampoList
                                         where chave.ChaveIdentificacao != null
                                         && chave.ChaveIdentificacao.Trim() != ""
                                         select new
                                            {
                                                TagCampo = chave.TagCampo,
                                                Order = chave.Ordem
                                            };

                        if (this.TcsArquivoItemList.Count() > 1)
                        {
                            //Verifica se elemento possui chave primaria
                            if (varChaveID.Count() <= 0)
                                throw new Exception(string.Format("Elemento {0} não possui nenhum campo com a chave de identificação.", elemento.TagItem));

                            //Verifica se elemento possui somente um campo como chave primaria
                            if (varChaveID.Count() > 1)
                                throw new Exception(string.Format("Elemento {0} deve conter somente um campo com a chave de identificação.", elemento.TagItem));
                        }
                    }
                }

                var campoDataNull = from cmp in elemento.TcsArquivoItemCampoList
                                    where cmp.LxTipoDado == Linx.TCS0101.BO.Domains.TipoDado.DATE.Value
                                    && string.IsNullOrWhiteSpace(cmp.LxFormatoData)
                                    select cmp.TagCampo;

                if (campoDataNull.Count() > 0)
                    throw new Exception(string.Format("Formato de data não informado no campo {0}.", campoDataNull.First()));

                var campoDataInvalido = from cmp in elemento.TcsArquivoItemCampoList
                                        where cmp.LxTipoDado == Linx.TCS0101.BO.Domains.TipoDado.DATE.Value
                                        && !Domains.FormatoData.GetValues().Validate(cmp.LxFormatoData)
                                        select cmp.TagCampo;

                if (campoDataInvalido.Count() > 0)
                    throw new Exception(string.Format("Formato de data inválido no campo {0}.", campoDataInvalido.First()));

                if (this.TcsArquivoItemList.Count() > 1)
                {
                    var ordemChaveID = (from ordem in this.TcsArquivoItemList
                                        select ordem.TcsArquivoItemCampoList.Where(w => w.ChaveIdentificacao != null && w.ChaveIdentificacao.Trim() != "").Select(s => s.Ordem).First()).Distinct();

                    if (ordemChaveID != null && ordemChaveID.Count() > 1)
                        throw new Exception(String.Format("Ordem dos campos com a chave de identificação devem ser iguais em todos os elementos.", elemento.TagItem));
                }

                foreach (var campo in elemento.TcsArquivoItemCampoList)
                {
                    //Valida o tipo de dado
                    Dictionary<string, string>.KeyCollection keyTipoDado = Domains.TipoDado.GetValues().Keys;
                    if (!campo.LxTipoDado.InList(keyTipoDado.ToArray()))
                        throw new Exception(String.Format("Tipo de Dado inválido ou em branco do campo {0} no elemento {1}", campo.TagCampo, elemento.TagItem));

                    if (string.IsNullOrWhiteSpace(this.Delimitador) && campo.Tamanho <= 0 && (this.LxTipoArquivo == Domains.TipoArquivo.Text.Value.ToString() ||
                                                                                              this.LxTipoArquivo == Domains.TipoArquivo.Todos.Value.ToString()))
                        throw new Exception(String.Format("Tamanho do campo {0} no elemento {1} não informado", campo.TagCampo, elemento.TagItem));
                }
            }

            #endregion
        }

        #region Metodos para criação do XSD

        /// <summary>
        /// Retorna o XSD a partir das definições de estrutura do XML definidas na interface
        /// </summary>
        /// <param name="pcolXML">definição dos campos do arquivo XML</param>
        /// <returns>String contendo o XSD para validação do arquivo XML</returns>
        private string frRetornaXSD()
        {
            XmlSchema xmlschema = new XmlSchema();

            string strNamespace = this.Xmlns;

            XmlSchemaElement xmlschemaElement = new XmlSchemaElement()
            {
                Name = "Organization",
                SchemaTypeName = new XmlQualifiedName("string", strNamespace)
            };

            xmlschema.Items.Add(xmlschemaElement);

            XmlSchemaElement xmlTagMestre = new XmlSchemaElement();

            List<TcsArquivoItem> lstElementos = (from pai in this.TcsArquivoItemList
                                                 where pai.IdArquivoItemPai == null
                                                 select pai).ToList<TcsArquivoItem>();

            //Cria o emelento raiz
            XmlSchemaElement elementoMestre = new XmlSchemaElement();
            elementoMestre.Name = this.TagMestre;

            XmlSchemaComplexType complexType = new XmlSchemaComplexType();
            XmlSchemaSequence schemaSequence = new XmlSchemaSequence();

            //insere todos os itens no elemento raiz
            foreach (var tag in lstElementos)
            {
                schemaSequence.Items.Add(frRetornaElementos(tag, this.TcsArquivoItemList.ToList<TcsArquivoItem>()));
            }

            complexType.Particle = schemaSequence;
            elementoMestre.SchemaType = complexType;

            xmlschema.Items.Add(elementoMestre);

            XmlNamespaceManager xmlNamespace = new XmlNamespaceManager(new NameTable());
            xmlNamespace.AddNamespace("xs", strNamespace);

            StringWriterWithEncoding sw = new StringWriterWithEncoding(Encoding.UTF8);

            xmlschema.Write(sw, xmlNamespace);

            return sw.ToString();
        }

        /// <summary>
        /// Retorna um elemento complexo, contendo todos seus campos e elementos
        /// </summary>
        /// <param name="pXMLTagPai">Contém as especificações de campos e elementos filhos</param>
        /// <param name="plstXMLTagElementos">Todas as Tags definidas na interface</param>
        /// <returns>Elemento complexo, contendo todos seus campos e elementos</returns>
        private XmlSchemaElement frRetornaElementos(TcsArquivoItem pXMLTagPai, List<TcsArquivoItem> plstXMLTagElementos)
        {
            XmlSchemaElement elementoComplexo = new XmlSchemaElement();
            elementoComplexo.Name = pXMLTagPai.TagItem;

            //Insere a quantidade mínima de ocorrencias permitidas
            if (pXMLTagPai.IndicaNotnull)
                elementoComplexo.MinOccurs = 1;
            else
            {
                elementoComplexo.IsNillable = true;
                elementoComplexo.MinOccurs = 0;
            }
            elementoComplexo.MaxOccurs = decimal.MaxValue;

            XmlSchemaComplexType complexType = new XmlSchemaComplexType();

            List<TcsArquivoItem> lstXMLTagFilhas = (from filha in plstXMLTagElementos
                                                    where filha.IdArquivoItemPai == pXMLTagPai.IdArquivoItem
                                                    select filha).OrderBy(o => o.Ordem).ToList<TcsArquivoItem>();

            if (pXMLTagPai.TcsArquivoItemCampoList.Count() > 0 || lstXMLTagFilhas.Count > 0)
            {
                XmlSchemaSequence schemaSequence = new XmlSchemaSequence();

                List<TcsArquivoItemCampo> lstXMLcamposPai = pXMLTagPai.TcsArquivoItemCampoList.OrderBy(o => o.Ordem).ToList<TcsArquivoItemCampo>();
                if (lstXMLcamposPai.Count() > 0)
                {
                    foreach (var tagCampo in lstXMLcamposPai)
                    {
                        XmlSchemaElement xmlCampoElement = null;
                        
                        //string
                        if(tagCampo.LxTipoDado.Trim() == Domains.TipoDado.STRING.Value)
                            xmlCampoElement = frRetornaElementoString(tagCampo);
                        //integer
                        else if (tagCampo.LxTipoDado.Trim() == Domains.TipoDado.INTEGER.Value)
                            xmlCampoElement = frRetornaElementoInteger(tagCampo, true);
                        //positive integer
                        else if (tagCampo.LxTipoDado.Trim() == Domains.TipoDado.POSITIVEINTEGER.Value)
                            xmlCampoElement = frRetornaElementoInteger(tagCampo, false);
                        //boolean
                        else if (tagCampo.LxTipoDado.Trim() == Domains.TipoDado.BOOLEAN.Value)
                            xmlCampoElement = frRetornaElementoBoolean(tagCampo);
                        //byte
                        else if (tagCampo.LxTipoDado.Trim() == Domains.TipoDado.BYTE.Value)
                            xmlCampoElement = frRetornaElementoByte(tagCampo);
                        //date
                        else if (tagCampo.LxTipoDado.Trim() == Domains.TipoDado.DATE.Value)
                            xmlCampoElement = frRetornaElementoDate(tagCampo);
                        //decimal
                        else if (tagCampo.LxTipoDado.Trim() == Domains.TipoDado.DECIMAL.Value)
                            xmlCampoElement = frRetornaElementoDecimal(tagCampo);
                        //double
                        else if (tagCampo.LxTipoDado.Trim() == Domains.TipoDado.DOUBLE.Value)
                            xmlCampoElement = frRetornaElementoDouble(tagCampo);
                        //long
                        else if (tagCampo.LxTipoDado.Trim() == Domains.TipoDado.LONG.Value)
                            xmlCampoElement = frRetornaElementoLong(tagCampo);
                        //time
                        else if (tagCampo.LxTipoDado.Trim() == Domains.TipoDado.TIME.Value)
                            xmlCampoElement = frRetornaElementoTime(tagCampo);

                        schemaSequence.Items.Add(xmlCampoElement);
                    }
                }

                if (lstXMLTagFilhas.Count > 0)
                {
                    foreach (var TagFilha in lstXMLTagFilhas)
                    {
                        schemaSequence.Items.Add(frRetornaElementos(TagFilha, plstXMLTagElementos));
                    }
                }

                complexType.Particle = schemaSequence;
            }
            elementoComplexo.SchemaType = complexType;

            return elementoComplexo;
        }

        /// <summary>
        /// Retorna um elemento de validação de uma Tag String
        /// </summary>
        /// <param name="pXmlItemCampo">Campo string para montagem do elemento de validação</param>
        /// <returns>Schema de validação de um campo string</returns>
        private XmlSchemaElement frRetornaElementoString(TcsArquivoItemCampo pXmlItemCampo)
        {
            XmlSchemaElement elementString = new XmlSchemaElement();
            XmlSchemaSimpleType simpleType = new XmlSchemaSimpleType();
            XmlSchemaSimpleTypeRestriction XmlRestriction = new XmlSchemaSimpleTypeRestriction();

            //Insere nome da TAG
            elementString.Name = pXmlItemCampo.TagCampo;

            //Insere a quantidade mínima de ocorrencias permitidas
            if (pXmlItemCampo.IndicaNotnull)
            {
                elementString.MinOccurs = 1;

                XmlSchemaMinLengthFacet minLength = new XmlSchemaMinLengthFacet();
                minLength.Value = "1";
                XmlRestriction.Facets.Add(minLength);
            }
            else
            {
                elementString.IsNillable = true;
                elementString.MinOccurs = 0;
            }
            //Insere a quantidade máxima de ocorrencias permitidas
            elementString.MaxOccurs = 1;

            //Insere o tipo de dado aceito
            XmlRestriction.BaseTypeName = new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");

            //Insere a quantidade máxima de caracteres
            if (pXmlItemCampo.Tamanho > 0)
            {
                XmlSchemaMaxLengthFacet maxLength = new XmlSchemaMaxLengthFacet();
                maxLength.Value = pXmlItemCampo.Tamanho.ToString();
                XmlRestriction.Facets.Add(maxLength);
            }

            simpleType.Content = XmlRestriction;
            elementString.SchemaType = simpleType;

            return elementString;
        }

        /// <summary>
        /// Retorna um elemento de validação de uma Tag Double
        /// </summary>
        /// <param name="pXmlItemCampo">Campo Double para montagem do elemento de validação</param>
        /// <returns>Schema de validação de um campo Double</returns>
        private XmlSchemaElement frRetornaElementoDouble(TcsArquivoItemCampo pXmlItemCampo)
        {
            XmlSchemaElement elementDouble = new XmlSchemaElement();
            XmlSchemaSimpleType simpleType = new XmlSchemaSimpleType();
            XmlSchemaSimpleTypeRestriction XmlRestriction = new XmlSchemaSimpleTypeRestriction();

            //Insere nome da TAG
            elementDouble.Name = pXmlItemCampo.TagCampo;

            //Insere a quantidade mínima de ocorrencias permitidas
            if (pXmlItemCampo.IndicaNotnull)
                elementDouble.MinOccurs = 1;
            else
            {
                elementDouble.IsNillable = true;
                elementDouble.MinOccurs = 0;
            }

            //Insere a quantidade máxima de ocorrencias permitidas
            elementDouble.MaxOccurs = 1;

            //Insere o tipo de dado aceito
            XmlRestriction.BaseTypeName = new XmlQualifiedName("double", "http://www.w3.org/2001/XMLSchema");

            simpleType.Content = XmlRestriction;
            elementDouble.SchemaType = simpleType;

            return elementDouble;
        }

        /// <summary>
        /// Retorna um elemento de validação de uma Tag Interger
        /// </summary>
        /// <param name="pXmlItemCampo">Campo Interger para montagem do elemento de validação</param>
        /// <returns>Schema de validação de um campo Interger</returns>
        private XmlSchemaElement frRetornaElementoInteger(TcsArquivoItemCampo pXmlItemCampo, bool pblnAceitaNegativo)
        {
            XmlSchemaElement elementInterger = new XmlSchemaElement();
            XmlSchemaSimpleType simpleType = new XmlSchemaSimpleType();
            XmlSchemaSimpleTypeRestriction XmlRestriction = new XmlSchemaSimpleTypeRestriction();

            //Insere nome da TAG
            elementInterger.Name = pXmlItemCampo.TagCampo;

            //Insere a quantidade mínima de ocorrencias permitidas
            if (pXmlItemCampo.IndicaNotnull)
                elementInterger.MinOccurs = 1;
            else
            {
                elementInterger.IsNillable = true;
                elementInterger.MinOccurs = 0;
            }

            //Insere a quantidade máxima de ocorrencias permitidas
            elementInterger.MaxOccurs = 1;

            //Insere o tipo de dado aceito
            if (pblnAceitaNegativo)
                XmlRestriction.BaseTypeName = new XmlQualifiedName("integer", "http://www.w3.org/2001/XMLSchema");
            else
                XmlRestriction.BaseTypeName = new XmlQualifiedName("positiveInteger", "http://www.w3.org/2001/XMLSchema");

            simpleType.Content = XmlRestriction;
            elementInterger.SchemaType = simpleType;

            return elementInterger;
        }

        /// <summary>
        /// Retorna um elemento de validação de uma Tag Decimal
        /// </summary>
        /// <param name="pXmlItemCampo">Campo Decimal para montagem do elemento de validação</param>
        /// <returns>Schema de validação de um campo Decimal</returns>
        private XmlSchemaElement frRetornaElementoDecimal(TcsArquivoItemCampo pXmlItemCampo)
        {
            XmlSchemaElement elementNumeric = new XmlSchemaElement();
            XmlSchemaSimpleType simpleType = new XmlSchemaSimpleType();
            XmlSchemaSimpleTypeRestriction XmlRestriction = new XmlSchemaSimpleTypeRestriction();

            //Insere nome da TAG
            elementNumeric.Name = pXmlItemCampo.TagCampo;

            //Insere a quantidade mínima de ocorrencias permitidas
            if (pXmlItemCampo.IndicaNotnull)
                elementNumeric.MinOccurs = 1;
            else
            {
                elementNumeric.IsNillable = true;
                elementNumeric.MinOccurs = 0;
            }

            //Insere a quantidade máxima de ocorrencias permitidas
            elementNumeric.MaxOccurs = 1;

            //Insere o tipo de dado aceito
            XmlRestriction.BaseTypeName = new XmlQualifiedName("decimal", "http://www.w3.org/2001/XMLSchema");

            //Insere o tamanho total de dígitos
            if (pXmlItemCampo.Tamanho > 0)
            {
                XmlSchemaTotalDigitsFacet totalDigits = new XmlSchemaTotalDigitsFacet();
                totalDigits.Value = pXmlItemCampo.Tamanho.ToString();
                XmlRestriction.Facets.Add(totalDigits);
            }

            //Insere a quantidade de casas deciais
            if (pXmlItemCampo.Decimais > 0)
            {
                XmlSchemaFractionDigitsFacet fractionDigits = new XmlSchemaFractionDigitsFacet();
                fractionDigits.Value = pXmlItemCampo.Decimais.ToString();
                XmlRestriction.Facets.Add(fractionDigits);
            }

            simpleType.Content = XmlRestriction;
            elementNumeric.SchemaType = simpleType;

            return elementNumeric;
        }

        /// <summary>
        /// Retorna um elemento de validação de uma Tag DateTime
        /// </summary>
        /// <param name="pXmlItemCampo">Campo DateTime para montagem do elemento de validação</param>
        /// <returns>Schema de validação de um campo DateTime</returns>
        private XmlSchemaElement frRetornaElementoDateTime(TcsArquivoItemCampo pXmlItemCampo)
        {
            XmlSchemaElement elementDateTime = new XmlSchemaElement();
            XmlSchemaSimpleType simpleType = new XmlSchemaSimpleType();
            XmlSchemaSimpleTypeRestriction XmlRestriction = new XmlSchemaSimpleTypeRestriction();

            //Insere nome da TAG
            elementDateTime.Name = pXmlItemCampo.TagCampo;

            //Insere a quantidade mínima de ocorrencias permitidas
            if (pXmlItemCampo.IndicaNotnull)
                elementDateTime.MinOccurs = 1;
            else
            {
                elementDateTime.IsNillable = true;
                elementDateTime.MinOccurs = 0;
            }

            //Insere a quantidade máxima de ocorrencias permitidas
            elementDateTime.MaxOccurs = 1;

            //Insere o tipo de dado aceito
            XmlRestriction.BaseTypeName = new XmlQualifiedName("dateTime", "http://www.w3.org/2001/XMLSchema");

            simpleType.Content = XmlRestriction;
            elementDateTime.SchemaType = simpleType;

            return elementDateTime;
        }

        /// <summary>
        /// Retorna um elemento de validação de uma Tag Date
        /// </summary>
        /// <param name="pXmlItemCampo">Campo Date para montagem do elemento de validação</param>
        /// <returns>Schema de validação de um campo Date</returns>
        private XmlSchemaElement frRetornaElementoDate(TcsArquivoItemCampo pXmlItemCampo)
        {
            XmlSchemaElement elementDate = new XmlSchemaElement();
            XmlSchemaSimpleType simpleType = new XmlSchemaSimpleType();
            XmlSchemaSimpleTypeRestriction XmlRestriction = new XmlSchemaSimpleTypeRestriction();

            //Insere nome da TAG
            elementDate.Name = pXmlItemCampo.TagCampo;

            //Insere a quantidade mínima de ocorrencias permitidas
            if (pXmlItemCampo.IndicaNotnull)
            {
                elementDate.MinOccurs = 1;

                XmlSchemaMinLengthFacet minLength = new XmlSchemaMinLengthFacet();
                minLength.Value = "1";
                XmlRestriction.Facets.Add(minLength);
            }
            else
            {
                elementDate.IsNillable = true;
                elementDate.MinOccurs = 0;
            }
            //Insere a quantidade máxima de ocorrencias permitidas
            elementDate.MaxOccurs = 1;

            //Insere o tipo de dado aceito
            XmlRestriction.BaseTypeName = new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");

            XmlSchemaPatternFacet FormatDatePattern = SetDateFormatPattern(pXmlItemCampo.LxFormatoData);
            XmlRestriction.Facets.Add(FormatDatePattern);

            //Insere a quantidade máxima de caracteres
            if (pXmlItemCampo.Tamanho > 0)
            {
                XmlSchemaMaxLengthFacet maxLength = new XmlSchemaMaxLengthFacet();
                maxLength.Value = pXmlItemCampo.Tamanho.ToString();
                XmlRestriction.Facets.Add(maxLength);
            }

            simpleType.Content = XmlRestriction;
            elementDate.SchemaType = simpleType;

            return elementDate;

            //XmlSchemaElement elementDate = new XmlSchemaElement();
            //XmlSchemaSimpleType simpleType = new XmlSchemaSimpleType();
            //XmlSchemaSimpleTypeRestriction XmlRestriction = new XmlSchemaSimpleTypeRestriction();

            ////Insere nome da TAG
            //elementDate.Name = pXmlItemCampo.TagCampo;

            ////Insere a quantidade mínima de ocorrencias permitidas
            //if (pXmlItemCampo.IndicaNotnull)
            //    elementDate.MinOccurs = 1;
            //else
            //{
            //    elementDate.IsNillable = true;
            //    elementDate.MinOccurs = 0;
            //}

            ////Insere a quantidade máxima de ocorrencias permitidas
            //elementDate.MaxOccurs = 1;

            ////Insere o tipo de dado aceito
            //XmlRestriction.BaseTypeName = new XmlQualifiedName("date", "http://www.w3.org/2001/XMLSchema");

            //simpleType.Content = XmlRestriction;
            //elementDate.SchemaType = simpleType;

            //return elementDate;
        }

        /// <summary>
        /// Insert data format validate
        /// </summary>
        /// <param name="pFormatDate"></param>
        /// <returns></returns>
        private XmlSchemaPatternFacet SetDateFormatPattern(string pFormatDate)
        {
            XmlSchemaPatternFacet XmlPattern = new XmlSchemaPatternFacet();

            if(pFormatDate == Domains.FormatoData.AAAAMMDD.Value)
                XmlPattern.Value = @"^(((((19)|(20))([\d][\d]))(((((0[13578])|(1[02]))((0[1-9])|([12]\d)|(3[01])))|(((0[469])|(11))((0[1-9])|([12]\d)|(30)))|((02)((0[1-9])|(1\d)|(2[0-8]))))))|((((19)|(20))(([02468][048])|([13579][26]))))(02)(29))$";
            else if (pFormatDate == Domains.FormatoData.DDMMAAAA.Value)
                XmlPattern.Value = @"^(((((((0[1-9])|([12]\d)|(3[01]))((0[13578])|(1[02])))|(((0[1-9])|([12]\d)|(30))((0[469])|(11)))|(((0[1-9])|(1\d)|(2[0-8]))(02)))(((19)|(20))([\d][\d]))))|((29)(02)(((19)|(20))(([02468][048])|([13579][26])))))$";
            else if (pFormatDate == Domains.FormatoData.MMDDAAAA.Value)
                XmlPattern.Value = @"^(((((((0[13578])|(1[02]))((0[1-9])|([12]\d)|(3[01])))|(((0[469])|(11))((0[1-9])|([12]\d)|(30)))|((02)((0[1-9])|(1\d)|(2[0-8]))))(((19)|(20))([\d][\d]))))|((02)(29)(((19)|(20))(([02468][048])|([13579][26])))))$";
            else if (pFormatDate == Domains.FormatoData.AAMMDD.Value)
                XmlPattern.Value = @"^(((([\d][\d]))(((((0[13578])|(1[02]))((0[1-9])|([12]\d)|(3[01])))|(((0[469])|(11))((0[1-9])|([12]\d)|(30)))|((02)((0[1-9])|(1\d)|(2[0-8]))))))|(((([02468][048])|([13579][26]))))(02)(29))$";
            else if (pFormatDate == Domains.FormatoData.DDMMAA.Value)
                XmlPattern.Value = @"^(((((((0[1-9])|([12]\d)|(3[01]))((0[13578])|(1[02])))|(((0[1-9])|([12]\d)|(30))((0[469])|(11)))|(((0[1-9])|(1\d)|(2[0-8]))(02)))(([\d][\d]))))|((29)(02)((([02468][048])|([13579][26])))))$";
            else if (pFormatDate == Domains.FormatoData.MMDDAA.Value)
                XmlPattern.Value = @"^(((((((0[13578])|(1[02]))((0[1-9])|([12]\d)|(3[01])))|(((0[469])|(11))((0[1-9])|([12]\d)|(30)))|((02)((0[1-9])|(1\d)|(2[0-8]))))(([\d][\d]))))|((02)(29)(([02468][048])|([13579][26]))))$";
            
            return XmlPattern;
        }

        /// <summary>
        /// Retorna um elemento de validação de uma Tag Time
        /// </summary>
        /// <param name="pXmlItemCampo">Campo Time para montagem do elemento de validação</param>
        /// <returns>Schema de validação de um campo Time</returns>
        private XmlSchemaElement frRetornaElementoTime(TcsArquivoItemCampo pXmlItemCampo)
        {
            XmlSchemaElement elementTime = new XmlSchemaElement();
            XmlSchemaSimpleType simpleType = new XmlSchemaSimpleType();
            XmlSchemaSimpleTypeRestriction XmlRestriction = new XmlSchemaSimpleTypeRestriction();

            //Insere nome da TAG
            elementTime.Name = pXmlItemCampo.TagCampo;

            //Insere a quantidade mínima de ocorrencias permitidas
            if (pXmlItemCampo.IndicaNotnull)
                elementTime.MinOccurs = 1;
            else
            {
                elementTime.IsNillable = true;
                elementTime.MinOccurs = 0;
            }

            //Insere a quantidade máxima de ocorrencias permitidas
            elementTime.MaxOccurs = 1;

            //Insere o tipo de dado aceito
            XmlRestriction.BaseTypeName = new XmlQualifiedName("time", "http://www.w3.org/2001/XMLSchema");

            simpleType.Content = XmlRestriction;
            elementTime.SchemaType = simpleType;

            return elementTime;
        }

        /// <summary>
        /// Retorna um elemento de validação de uma Tag Boolean
        /// </summary>
        /// <param name="pXmlItemCampo">Campo Boolean para montagem do elemento de validação</param>
        /// <returns>Schema de validação de um campo Boolean</returns>
        private XmlSchemaElement frRetornaElementoBoolean(TcsArquivoItemCampo pXmlItemCampo)
        {
            XmlSchemaElement elementBoolean = new XmlSchemaElement();
            XmlSchemaSimpleType simpleType = new XmlSchemaSimpleType();
            XmlSchemaSimpleTypeRestriction XmlRestriction = new XmlSchemaSimpleTypeRestriction();

            //Insere nome da TAG
            elementBoolean.Name = pXmlItemCampo.TagCampo;

            //Insere a quantidade mínima de ocorrencias permitidas
            if (pXmlItemCampo.IndicaNotnull)
                elementBoolean.MinOccurs = 1;
            else
            {
                elementBoolean.IsNillable = true;
                elementBoolean.MinOccurs = 0;
            }

            //Insere a quantidade máxima de ocorrencias permitidas
            elementBoolean.MaxOccurs = 1;

            //Insere o tipo de dado aceito
            XmlRestriction.BaseTypeName = new XmlQualifiedName("boolean", "http://www.w3.org/2001/XMLSchema");

            simpleType.Content = XmlRestriction;
            elementBoolean.SchemaType = simpleType;

            return elementBoolean;
        }

        /// <summary>
        /// Retorna um elemento de validação de uma Tag Byte
        /// </summary>
        /// <param name="pXmlItemCampo">Campo Byte para montagem do elemento de validação</param>
        /// <returns>Schema de validação de um campo Byte</returns>
        private XmlSchemaElement frRetornaElementoByte(TcsArquivoItemCampo pXmlItemCampo)
        {
            XmlSchemaElement elementByte = new XmlSchemaElement();
            XmlSchemaSimpleType simpleType = new XmlSchemaSimpleType();
            XmlSchemaSimpleTypeRestriction XmlRestriction = new XmlSchemaSimpleTypeRestriction();

            //Insere nome da TAG
            elementByte.Name = pXmlItemCampo.TagCampo;

            //Insere a quantidade mínima de ocorrencias permitidas
            if (pXmlItemCampo.IndicaNotnull)
                elementByte.MinOccurs = 1;
            else
            {
                elementByte.IsNillable = true;
                elementByte.MinOccurs = 0;
            }

            //Insere a quantidade máxima de ocorrencias permitidas
            elementByte.MaxOccurs = 1;

            //Insere o tipo de dado aceito
            XmlRestriction.BaseTypeName = new XmlQualifiedName("byte", "http://www.w3.org/2001/XMLSchema");

            //Insere o tamanho total de dígitos
            if (pXmlItemCampo.Tamanho > 0)
            {
                XmlSchemaTotalDigitsFacet totalDigits = new XmlSchemaTotalDigitsFacet();
                totalDigits.Value = pXmlItemCampo.Tamanho.ToString();
                XmlRestriction.Facets.Add(totalDigits);
            }

            simpleType.Content = XmlRestriction;
            elementByte.SchemaType = simpleType;

            return elementByte;
        }

        /// <summary>
        /// Retorna um elemento de validação de uma Tag Long
        /// </summary>
        /// <param name="pXmlItemCampo">Campo Long para montagem do elemento de validação</param>
        /// <returns>Schema de validação de um campo Long</returns>
        private XmlSchemaElement frRetornaElementoLong(TcsArquivoItemCampo pXmlItemCampo)
        {
            XmlSchemaElement elementLong = new XmlSchemaElement();
            XmlSchemaSimpleType simpleType = new XmlSchemaSimpleType();
            XmlSchemaSimpleTypeRestriction XmlRestriction = new XmlSchemaSimpleTypeRestriction();

            //Insere nome da TAG
            elementLong.Name = pXmlItemCampo.TagCampo;

            //Insere a quantidade mínima de ocorrencias permitidas
            if (pXmlItemCampo.IndicaNotnull)
                elementLong.MinOccurs = 1;
            else
            {
                elementLong.IsNillable = true;
                elementLong.MinOccurs = 0;
            }

            //Insere a quantidade máxima de ocorrencias permitidas
            elementLong.MaxOccurs = 1;

            //Insere o tipo de dado aceito
            XmlRestriction.BaseTypeName = new XmlQualifiedName("long", "http://www.w3.org/2001/XMLSchema");

            simpleType.Content = XmlRestriction;
            elementLong.SchemaType = simpleType;

            return elementLong;
        }

        #endregion

        public IEnumerable<TcsArquivoItemCampo> GetDateFields(string pFileCode)
        {
            ExtendedControleSistemaContext edm = new ExtendedControleSistemaContext();

            IEnumerable<TcsArquivoItemCampo> DateFields = from fil in edm.TCS_ARQUIVO_ITEM_CAMPO
                                                          where fil.TCS_ARQUIVO_ITEM.TCS_ARQUIVO.COD_ARQUIVO == pFileCode
                                                          && fil.TCS_ARQUIVO_ITEM.TCS_ARQUIVO.INATIVO == false
                                                          && fil.LX_TIPO_DADO == Linx.TCS0101.BO.Domains.TipoDado.DATE.Value
                                                          select new TcsArquivoItemCampo
                                                          {
                                                              LxFormatoData = fil.LX_FORMATO_DATA,
                                                              TagCampo = fil.TAG_CAMPO,
                                                              TcsArquivoItem = new TcsArquivoItem() { TagItem = fil.TCS_ARQUIVO_ITEM.TAG_ITEM }
                                                          };

            return DateFields;
        }
    }

    /// <summary>
    /// Classe implementada para poder escrever um documento XML ou XSD com o tipo de encoding desejado.
    /// </summary>
    public class StringWriterWithEncoding : StringWriter
    {
        Encoding encoding;

        public StringWriterWithEncoding(Encoding encoding)
        {
            this.encoding = encoding;
        }

        public override Encoding Encoding
        {
            get { return encoding; }
        }
    }
}
