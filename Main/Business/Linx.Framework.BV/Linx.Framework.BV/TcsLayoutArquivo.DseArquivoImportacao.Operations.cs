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
using System.IO;
using System.Xml.Serialization;

namespace Linx.TCS0101.BO.TcsLayoutArquivo
{
	
	////////////////////////////////////////////////////////////////////////////
	////////////////////////// Domain Service Extension ////////////////////////
	////////////////////////////////////////////////////////////////////////////
	public partial class TcsLayoutArquivoDomainService
	{
        [Invoke()]
        public void fvExportarLayout(int pintIdArquivo)
        {
            ExtendedControleSistemaContext edm = new ExtendedControleSistemaContext();
            string strArquivoLayout;

            TCS_ARQUIVO varArquivoBase = (from arq in edm.TCS_ARQUIVO
                                          where arq.ID_ARQUIVO == pintIdArquivo
                                          select arq).First<TCS_ARQUIVO>();

            TempTcsArquivo lstColArquivos = fcRetornaLayoutArquivo(varArquivoBase);

            TempTcsArquivoGrupo tmpArquivoGrupo = new TempTcsArquivoGrupo() { ListTempTcsArquivo = new List<TempTcsArquivo>() { lstColArquivos } };

            //serializa o objeto numa string com formato em XML
            strArquivoLayout = SerializeToString(tmpArquivoGrupo);

            string strNomeArquivo = frRetornaNomeCompletoArquivo(lstColArquivos.CaminhoArquivo, "Layout_Arquivo_" + lstColArquivos.CodArquivo);
            StreamWriter writer = new StreamWriter(strNomeArquivo);
            writer.Write(strArquivoLayout);
            writer.Close();
        }

        [Invoke()]
        public void fvImportarLayout(byte[] pbytFileStream)
        {
            ExtendedControleSistemaContext edm = new ExtendedControleSistemaContext();
            TempTcsArquivoGrupo tmpArquivoGrupo = (TempTcsArquivoGrupo)DeSerializeFromString<TempTcsArquivoGrupo>(ConverteByteArrayToString(pbytFileStream));

            int IdArquivoGrupo = 0;
            if(!string.IsNullOrWhiteSpace(tmpArquivoGrupo.CodArquivoGrupo))
            {
                var varGrupoArquivo = from grp in edm.TCS_ARQUIVO_GRUPO
                                        where grp.COD_ARQUIVO_GRUPO == tmpArquivoGrupo.CodArquivoGrupo
                                        select grp.ID_ARQUIVO_GRUPO;

                if (varGrupoArquivo.Count() == 0)
                {
                    TCS_ARQUIVO_GRUPO clsArquivoGrupo = new TCS_ARQUIVO_GRUPO()
                    {
                        COD_ARQUIVO_GRUPO = tmpArquivoGrupo.CodArquivoGrupo,
                        DESC_ARQUIVO_GRUPO = tmpArquivoGrupo.DescArquivoGrupo
                    };

                    edm.TCS_ARQUIVO_GRUPO.Add(clsArquivoGrupo);
                    edm.SaveChanges();

                    IdArquivoGrupo = clsArquivoGrupo.ID_ARQUIVO_GRUPO;
                }
                else
                {
                    IdArquivoGrupo = varGrupoArquivo.First();
                }
            
            }
            foreach (var tcsArquivo in tmpArquivoGrupo.ListTempTcsArquivo)
            {
                if (tcsArquivo.Inativo == false)
                {
                    //Inativa os registros que já existem, para não haver layouts ativos com o mesmo nome
                    var varArquivoExistente = from arq in edm.TCS_ARQUIVO
                                              where arq.NOME_ARQUIVO == tcsArquivo.NomeArquivo
                                              && arq.INATIVO == false
                                              select arq;

                    if (varArquivoExistente.Count() > 0)
                    {
                        foreach (var layout in varArquivoExistente)
                        {
                            layout.INATIVO = true;
                        }
                        edm.SaveChanges();
                    }
                }

                TcsLayoutArquivo.TcsArquivo objTcsArquivo = new TcsLayoutArquivo.TcsArquivo
                {
                    CaminhoArquivo = tcsArquivo.CaminhoArquivo,
                    CodArquivo = tcsArquivo.CodArquivo,
                    DescArquivo = tcsArquivo.DescArquivo,
                    DetalheArquivo = tcsArquivo.DetalheArquivo,
                    NomeArquivo = tcsArquivo.NomeArquivo,
                    TagMestre = tcsArquivo.TagMestre,
                    Xmlns = tcsArquivo.Xmlns,
                    LxTipoArquivo = tcsArquivo.LxTipoArquivo,
                    Inativo = tcsArquivo.Inativo,
                    ArquivoDll = tcsArquivo.ArquivoDll,
                    Classe = tcsArquivo.Classe,
                    Delimitador = tcsArquivo.Delimitador,
                    LxFormatoData = tcsArquivo.LxFormatoData,
                    Metodo = tcsArquivo.Metodo,
                    TcsArquivoItemList = from item in tcsArquivo.ListTempTcsArquivoItem
                                         select new TcsArquivoItem
                                         {
                                             TagItemPai = item.TagItemPai,
                                             IndicaNotnull = item.IndicaNotNull,
                                             Ordem = item.Ordem,
                                             TagItem = item.TagItem,
                                             Xmlns = item.Xmlns,
                                             TcsArquivoItemCampoList = from campo in item.ListTempTcsArquivoItemCampo
                                                                       select new TcsArquivoItemCampo
                                                                       {
                                                                           Decimais = campo.Decimais,
                                                                           IndicaNotnull = campo.IndicaNotNull,
                                                                           LxTipoDado = campo.LxTipoDado,
                                                                           IndicaPk = campo.IndicaPk,
                                                                           Ordem = campo.Ordem,
                                                                           TagCampo = campo.TagCampo,
                                                                           Tamanho = campo.Tamanho,
                                                                           ChaveIdentificacao = campo.ChaveIdentificacao,
                                                                           LxFormatoData = campo.LxFormatoData
                                                                       }
                                         },

                };

                TcsArquivoGrupoVinculo clsArquivoVinculo = null;
                if (!string.IsNullOrWhiteSpace(tmpArquivoGrupo.CodArquivoGrupo))
                {
                    clsArquivoVinculo = new TcsArquivoGrupoVinculo()
                    {
                        IdArquivo = objTcsArquivo.IdArquivo,
                        IdArquivoGrupo = IdArquivoGrupo,
                        Inativo = tcsArquivo.VinculoInativo,
                        Ordem = tcsArquivo.Ordem,
                        CodArquivoGrupo = tmpArquivoGrupo.CodArquivoGrupo,
                        DescArquivoGrupo = tmpArquivoGrupo.DescArquivoGrupo
                    };

                    objTcsArquivo.TcsArquivoGrupoVinculoList = new List<TcsArquivoGrupoVinculo>() { clsArquivoVinculo };
                }

                objTcsArquivo.OnSavingChanges(this, ChangeOperation.Insert);
                
                if(clsArquivoVinculo != null)
                    this.InsertTcsArquivoGrupoVinculo(clsArquivoVinculo);

                this.InsertTcsArquivo(objTcsArquivo);

                this.SaveCustomChanges();
                objTcsArquivo.RefreshKeys();

                if (clsArquivoVinculo != null)
                    clsArquivoVinculo.IdArquivo = objTcsArquivo.IdArquivo;

                List<TcsArquivoItem> lstItem = new List<TcsArquivoItem>();
                foreach (var item in objTcsArquivo.TcsArquivoItemList)
                {
                    List<TcsArquivoItemCampo> lstCampo = new List<TcsArquivoItemCampo>();
                    
                    item.IdArquivoFk = objTcsArquivo.IdArquivo;
                    this.InsertTcsArquivoItem(item);
                    this.SaveCustomChanges();
                    item.RefreshKeys();

                    foreach (var campo in item.TcsArquivoItemCampoList)
                    {
                        campo.IdArquivoItemFk = item.IdArquivoItem;
                        this.InsertTcsArquivoItemCampo(campo);
                        this.SaveCustomChanges();
                        campo.RefreshKeys();
                        lstCampo.Add(campo);
                    }
                    item.TcsArquivoItemCampoList = lstCampo.AsEnumerable();
                    lstItem.Add(item);
                }
                objTcsArquivo.TcsArquivoItemList = lstItem.AsEnumerable();

                foreach (var item in objTcsArquivo.TcsArquivoItemList)
                {
                    if (!item.TagItemPai.IsNullOrEmpty())
                    {
                        item.IdArquivoItem = edm.TCS_ARQUIVO_ITEM.Where(w => w.ID_ARQUIVO_FK == objTcsArquivo.IdArquivo && w.TAG_ITEM == item.TagItem).Select(s => s.ID_ARQUIVO_ITEM).First();
                        item.IdArquivoItemPai = edm.TCS_ARQUIVO_ITEM.Where(w => w.ID_ARQUIVO_FK == objTcsArquivo.IdArquivo && w.TAG_ITEM == item.TagItemPai).Select(s => s.ID_ARQUIVO_ITEM).First();
                        this.UpdateTcsArquivoItem(item);
                    }
                }

                // this.SaveCustomChanges();

                string strSucess = String.Format("Sucesso: Arquivo {0} importado com êxito!", objTcsArquivo.NomeArquivo);
                Linx.TCS0101.BO.LinxBusinessImportFile.fvWriteLog(objTcsArquivo.CodArquivo.ToString(), Convert.ToInt32(Linx.TCS0101.BO.Domains.TipoLog.ImportacaoLayout.Value), strSucess);
                
            }
        }

        [Invoke()]
        public string[] frVerificaArquivosExistentes(byte[] pbytFileStream)
        {
            ExtendedControleSistemaContext edm = new ExtendedControleSistemaContext();
            string[] arrArquivosExistentes = null;

            TempTcsArquivoGrupo tmpLayoutArquivo = (TempTcsArquivoGrupo)DeSerializeFromString<TempTcsArquivoGrupo>(ConverteByteArrayToString(pbytFileStream));
            List<string> lstNomeArquivosEnv = tmpLayoutArquivo.ListTempTcsArquivo.Where(w=> w.Inativo == false).Select(s => s.NomeArquivo).ToList();

            var varArquivoExistente = from arq in edm.TCS_ARQUIVO
                                      where lstNomeArquivosEnv.Contains(arq.NOME_ARQUIVO)
                                      && arq.INATIVO == false
                                      select arq.NOME_ARQUIVO;

            if (varArquivoExistente.Count() > 0)
                arrArquivosExistentes = varArquivoExistente.ToArray();

            return arrArquivosExistentes;
        }

        #region Exportar Layout XML

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
        /// Converte um FileStream para String
        /// </summary>
        /// <param name="pFileStream">Objeto contendo os dados para conversão</param>
        /// <returns>Texto contendo o conteúdo do FileStream</returns>
        private string ConverteByteArrayToString(byte[] pbytFileStream)
        {
            return System.Text.UTF8Encoding.UTF8.GetString(pbytFileStream);
        }

        /// <summary>
        /// Converte as informações do banco de dados numa classe espelho
        /// </summary>
        /// <param name="pedmTcsArquivo">Classe do banco de dados para conversão</param>
        /// <returns>Classe espelho contendo os dados do banco de dados</returns>
        private TempTcsArquivo fcRetornaLayoutArquivo(TCS_ARQUIVO pedmTcsArquivo)
        {
            TempTcsArquivo lstColXML = new TempTcsArquivo()
                                              {
                                                  idArquivo = pedmTcsArquivo.ID_ARQUIVO,
                                                  CaminhoArquivo = pedmTcsArquivo.CAMINHO_ARQUIVO,
                                                  CodArquivo = pedmTcsArquivo.COD_ARQUIVO,
                                                  DescArquivo = pedmTcsArquivo.DESC_ARQUIVO,
                                                  DetalheArquivo = pedmTcsArquivo.DETALHE_ARQUIVO,
                                                  NomeArquivo = pedmTcsArquivo.NOME_ARQUIVO,
                                                  TagMestre = pedmTcsArquivo.TAG_MESTRE,
                                                  Xmlns = pedmTcsArquivo.XMLNS,
                                                  LxTipoArquivo = pedmTcsArquivo.LX_TIPO_ARQUIVO,
                                                  Inativo = pedmTcsArquivo.INATIVO,
                                                  ArquivoDll = pedmTcsArquivo.ARQUIVO_DLL,
                                                  Classe = pedmTcsArquivo.CLASSE,
                                                  Metodo = pedmTcsArquivo.METODO,
                                                  LxFormatoData = pedmTcsArquivo.LX_FORMATO_DATA,
                                                  Delimitador = pedmTcsArquivo.DELIMITADOR,
                                                  ListTempTcsArquivoItem =
                                                          (from item in pedmTcsArquivo.TCS_ARQUIVO_ITEM_LISTA
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
                                                                            LxFormatoData = campo.LX_FORMATO_DATA,
                                                                            Ordem = campo.ORDEM,
                                                                            ChaveIdentificacao = campo.CHAVE_IDENTIFICACAO
                                                                        }).ToList<TempTcsArquivoItemCampo>()
                                                           }).ToList<TempTcsArquivoItem>()
                                              };

            return lstColXML;
        }
        #endregion

        #region Serialização e Deserialização

        private object DeSerializeFromString<T>(string stream)
        {

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

            StringReader stringReader = new StringReader(stream);

            return xmlSerializer.Deserialize(stringReader);

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

        #endregion

    }

    [Serializable]
    public class TempTcsArquivo
    {
        public int idArquivo { get; set; }
        public string CodArquivo { get; set; }
        public string DescArquivo { get; set; }
        public string DetalheArquivo { get; set; }
        public string NomeArquivo { get; set; }
        public string TagMestre { get; set; }
        public string Xmlns { get; set; }
        public string CaminhoArquivo { get; set; }
        public string LxTipoArquivo { get; set; }
        public bool Inativo { get; set; }
        public bool VinculoInativo { get; set; }
        public int Ordem { get; set; }
        public string ArquivoDll { get; set; }
        public string Classe { get; set; }
        public string Metodo { get; set; }
        public string LxFormatoData { get; set; }
        public string Delimitador { get; set; }

        private List<TempTcsArquivoItem> _ListTempTcsArquivoItem = new List<TempTcsArquivoItem>();
        public List<TempTcsArquivoItem> ListTempTcsArquivoItem
        {
            get { return _ListTempTcsArquivoItem; }
            set { _ListTempTcsArquivoItem = value; }
        }
    }

    [Serializable]
    public class TempTcsArquivoItem
    {
        public string TagItemPai { get; set; }
        public string TagItem { get; set; }
        public string Xmlns { get; set; }
        public bool IndicaNotNull { get; set; }
        public int Ordem { get; set; }

        private List<TempTcsArquivoItemCampo> _ListTempTcsArquivoItemCampo = new List<TempTcsArquivoItemCampo>();
        public List<TempTcsArquivoItemCampo> ListTempTcsArquivoItemCampo
        {
            get { return _ListTempTcsArquivoItemCampo; }
            set { _ListTempTcsArquivoItemCampo = value; }
        }
    }

    [Serializable]
    public class TempTcsArquivoItemCampo
    {
        public string TagCampo { get; set; }
        public string LxTipoDado { get; set; }
        public int Tamanho { get; set; }
        public byte Decimais { get; set; }
        public bool IndicaNotNull { get; set; }
        public bool IndicaPk { get; set; }
        public int Ordem { get; set; }
        public string LxFormatoData { get; set; }
        public string ChaveIdentificacao { get; set; }
    }

    [Serializable]
    public class TempTcsArquivoGrupo
    {
        public int idArquivoGrupo { get; set; }
        public string CodArquivoGrupo { get; set; }
        public string DescArquivoGrupo { get; set; }

        private List<TempTcsArquivo> _ListTempTcsArquivo = new List<TempTcsArquivo>();
        public List<TempTcsArquivo> ListTempTcsArquivo
        {
            get { return _ListTempTcsArquivo; }
            set { _ListTempTcsArquivo = value; }
        }
    }

    //[Serializable]
    //public class TempTcsArquivoVinculo
    //{
    //    public int idArquivo { get; set; }
    //    public int Ordem { get; set; }
    //    public bool Inativo { get; set; }

    //    private TempTcsArquivo _TempTcsArquivo = null;
    //    public TempTcsArquivo TempTcsArquivo
    //    {
    //        get { return _TempTcsArquivo; }
    //        set { _TempTcsArquivo = value; }
    //    }
    //}
}
