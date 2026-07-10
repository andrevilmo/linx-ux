using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Linx.Data;
using Linx.Tools;
using System.Data.Objects;
using System.ComponentModel;
using System.Data.Common;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ComponentModel.DataAnnotations;
using System.ServiceModel.DomainServices.Server;
using System.ServiceModel.DomainServices.Hosting;
using System.ServiceModel.DomainServices;
using Linx;
using Linx.Framework.ControleSistema.BM;
using Linx.TCS0101.BO.TcsLayoutArquivo;
using System.IO;
using System.Xml.Serialization;

namespace Linx.TCS0101.BO.TcsGrupoLayoutArquivo
{
	
	////////////////////////////////////////////////////////////////////////////
	////////////////////////// Domain Service Extension ////////////////////////
	////////////////////////////////////////////////////////////////////////////
	public partial class TcsGrupoLayoutArquivoDomainService
	{
        [Invoke()]
        public void fvExportarLayoutGrupo(int pintIdArquivoGrupo)
        {
            ExtendedControleSistemaContext edm = new ExtendedControleSistemaContext();
            string strArquivoLayout;

            TempTcsArquivoGrupo tmpArquivoGrupo = (from grp in edm.TCS_ARQUIVO_GRUPO
                                                   where grp.ID_ARQUIVO_GRUPO == pintIdArquivoGrupo
                                                   select new TempTcsArquivoGrupo
                                                   {
                                                       CodArquivoGrupo = grp.COD_ARQUIVO_GRUPO,
                                                       DescArquivoGrupo = grp.DESC_ARQUIVO_GRUPO,
                                                       idArquivoGrupo = grp.ID_ARQUIVO_GRUPO
                                                   }).First<TempTcsArquivoGrupo>();


            IQueryable<TCS_ARQUIVO> lstColTcsArquivos = from fil in edm.TCS_ARQUIVO
                                                        join vinc in edm.TCS_ARQUIVO_GRUPO_VINCULO
                                                        on fil.ID_ARQUIVO equals vinc.ID_ARQUIVO
                                                        where vinc.ID_ARQUIVO_GRUPO == pintIdArquivoGrupo
                                                        select fil;

            if (lstColTcsArquivos != null && lstColTcsArquivos.Count() > 0)
            {
                List<TCS_ARQUIVO_GRUPO_VINCULO> lstColTcsArquivoVinculo = (from vinc in edm.TCS_ARQUIVO_GRUPO_VINCULO
                                                                           join arq in lstColTcsArquivos
                                                                           on vinc.ID_ARQUIVO equals arq.ID_ARQUIVO
                                                                           where vinc.ID_ARQUIVO_GRUPO == tmpArquivoGrupo.idArquivoGrupo
                                                                           select vinc).ToList<TCS_ARQUIVO_GRUPO_VINCULO>();

                List<TempTcsArquivo> lstColArquivos = fcRetornaLayoutArquivos(lstColTcsArquivos.ToList(), lstColTcsArquivoVinculo);


                tmpArquivoGrupo.ListTempTcsArquivo = lstColArquivos;

                //serializa o objeto numa string com formato em XML
                strArquivoLayout = SerializeToString(tmpArquivoGrupo);

                string strNomeArquivo = frRetornaNomeCompletoArquivo(tmpArquivoGrupo.ListTempTcsArquivo[0].CaminhoArquivo, "Layout_Grupo_" + tmpArquivoGrupo.DescArquivoGrupo);
                StreamWriter writer = new StreamWriter(strNomeArquivo);
                writer.Write(strArquivoLayout);
                writer.Close();
            }
            else
            {
                throw new Exception("O grupo não possui arquivos vinculados para exportação");
            }
        }

        /// <summary>
        /// Valida e retorna o diretório juntamente com o nome do arquivo
        /// </summary>
        /// <param name="pstrNomeDiretorio">Diretório do Arquivo</param>
        /// <param name="pstrNomeArquivo">Nome do Arquivo</param>
        /// <returns>Diretório juntamente com o nome do arquivo</returns>
        public string frRetornaNomeCompletoArquivo(string pstrNomeDiretorio, string pstrNomeArquivo)
        {
            //Verifica se o diretório existe
            if (!Directory.Exists(pstrNomeDiretorio))
                throw new Exception(String.Format("Caminho especificado do arquivo {0} não encontrado", pstrNomeArquivo));

            //Define o nome completo
            string strArquivo = string.Empty;
            if ((pstrNomeDiretorio.Substring(pstrNomeDiretorio.Length - 1) != @"\"))
                strArquivo = pstrNomeDiretorio + @"\" + pstrNomeArquivo + ".xml";
            else
                strArquivo = pstrNomeDiretorio + pstrNomeArquivo + ".xml";

            return strArquivo;
        }

        /// <summary>
        /// Converte as informações do banco de dados numa classe espelho
        /// </summary>
        /// <param name="plstTcsArquivo">Classe do banco de dados para conversão</param>
        /// <returns>Classe espelho contendo os dados do banco de dados</returns>
        private List<TempTcsArquivo> fcRetornaLayoutArquivos(List<TCS_ARQUIVO> plstTcsArquivo, List<TCS_ARQUIVO_GRUPO_VINCULO> plstTcsArquivoVinculo)
        {
            List<TempTcsArquivo> lstColXML = (from arq in plstTcsArquivo
                                              join vinc in plstTcsArquivoVinculo
                                              on arq.ID_ARQUIVO equals vinc.ID_ARQUIVO
                                              select new TempTcsArquivo
                                              {
                                                  CaminhoArquivo = arq.CAMINHO_ARQUIVO,
                                                  CodArquivo = arq.COD_ARQUIVO,
                                                  DescArquivo = arq.DESC_ARQUIVO,
                                                  DetalheArquivo = arq.DETALHE_ARQUIVO,
                                                  NomeArquivo = arq.NOME_ARQUIVO,
                                                  TagMestre = arq.TAG_MESTRE,
                                                  Xmlns = arq.XMLNS,
                                                  LxTipoArquivo = arq.LX_TIPO_ARQUIVO,
                                                  Inativo = arq.INATIVO,
                                                  Ordem = vinc.ORDEM,
                                                  VinculoInativo = vinc.INATIVO,
                                                  ArquivoDll = arq.ARQUIVO_DLL,
                                                  Metodo = arq.METODO,
                                                  LxFormatoData = arq.LX_FORMATO_DATA,
                                                  Delimitador = arq.DELIMITADOR,
                                                  Classe = arq.CLASSE,
                                                  ListTempTcsArquivoItem =
                                                          (from item in arq.TCS_ARQUIVO_ITEM_LISTA
                                                           select new TempTcsArquivoItem
                                                           {
                                                               TagItemPai = item.ARQUIVO_ITEM_PAI == null ? null : item.ARQUIVO_ITEM_PAI.TAG_ITEM,
                                                               IndicaNotNull = item.INDICA_NOTNULL,
                                                               Ordem = item.ORDEM,
                                                               TagItem = item.TAG_ITEM,
                                                               Xmlns = item.XMLNS,
                                                               ListTempTcsArquivoItemCampo =
                                                                       (from campo in item.TCS_ARQUIVO_ITEM_CAMPO_LISTA
                                                                        select new TempTcsArquivoItemCampo
                                                                        {
                                                                            Decimais = campo.DECIMAIS,
                                                                            LxTipoDado = campo.LX_TIPO_DADO,
                                                                            TagCampo = campo.TAG_CAMPO,
                                                                            Tamanho = campo.TAMANHO,
                                                                            IndicaNotNull = campo.INDICA_NOTNULL,
                                                                            IndicaPk = campo.INDICA_PK,
                                                                            Ordem = campo.ORDEM,
                                                                            LxFormatoData = campo.LX_FORMATO_DATA,
                                                                            ChaveIdentificacao = campo.CHAVE_IDENTIFICACAO
                                                                        }).ToList<TempTcsArquivoItemCampo>()
                                                           }).ToList<TempTcsArquivoItem>()
                                              }).ToList<TempTcsArquivo>();

            return lstColXML;
        }

        private string SerializeToString(object instance)
        {
            if (instance == null)
                return "";

            XmlSerializer xmlSerializer = new XmlSerializer(instance.GetType());

            StringWriter stringWriter = new StringWriterWithEncoding(Encoding.UTF8);

            xmlSerializer.Serialize(stringWriter, instance);

            return stringWriter.ToString();

        }

    }
}
