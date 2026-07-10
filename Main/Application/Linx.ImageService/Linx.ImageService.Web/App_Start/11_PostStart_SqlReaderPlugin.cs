using System;
using System.Web;
using System.Linq;
using Microsoft.Web.Infrastructure;
using System.Web.Security;
using NLog;
using System.Configuration;
using WebActivatorEx;
using ImageResizer.Plugins.SqlReader;
using RestSharp;
using System.Net;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Dynamic;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

[assembly: PostApplicationStartMethod(typeof(Linx.ImageService.Web.PostStart_SqlReaderPlugin), "PostStart", Order = 11)]


namespace Linx.ImageService.Web  
{
    public static class PostStart_SqlReaderPlugin
    {
        public static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static void PostStart()
        {
            Logger.Info("Configurando o plugin 'SqlReaderPlugin'");
            var ConnectionStrings = GetApiConnectionStrings();
            var TableCodes = GetApiTableCodes();

            foreach (var item in ConnectionStrings)
            {
                // por UID_DOCUMENTO (http://localhost:59914/ux-id-2/633660C0-CF02-4C96-9E93-005DCC1B86CF.jpg)
                // ex: http://localhost:59914/ux-id-2/633660C0-CF02-4C96-9E93-005DCC1B86CF.jpg
                SqlReaderSettings settingByUidDocumento = new SqlReaderSettings();

                settingByUidDocumento.ConnectionString = item.Value.Replace("Persist Security Info=True; Trusted_Connection=True;", "User ID=fernando.chaves;Password=S0eusei@2022;");
                //settingByUidDocumento.ConnectionString = item.Value;
                settingByUidDocumento.PathPrefix = string.Concat("~/ux-id-", item.Key, "/");
                settingByUidDocumento.ImageIdType = System.Data.SqlDbType.UniqueIdentifier;
                settingByUidDocumento.ImageBlobQuery = "SELECT CONTEUDO as [content] FROM LX_DOC.DOC_MULTIMIDIA WHERE UID_DOCUMENTO = @id";
                settingByUidDocumento.ModifiedDateQuery = "SELECT DATA_CRIACAO as [ModifiedDate], DATA_CRIACAO as [CreatedDate] FROM LX_DOC.DOC_MULTIMIDIA WHERE UID_DOCUMENTO = @id";
                settingByUidDocumento.ImageExistsQuery = "SELECT COUNT(UID_DOCUMENTO) From LX_DOC.DOC_MULTIMIDIA WHERE  UID_DOCUMENTO = @id";
                settingByUidDocumento.CacheUnmodifiedFiles = true;
                settingByUidDocumento.RequireImageExtension = true;
                settingByUidDocumento.CheckForModifiedFiles = true;
                //Add plugin

                Logger.Trace("adicionando ambiente por UidDocumento'{0}' '{1}' ", settingByUidDocumento.PathPrefix, settingByUidDocumento.ConnectionString);
                new SqlReaderPlugin(settingByUidDocumento).Install(ImageResizer.Configuration.Config.Current);

                foreach (var table in TableCodes)
                {
                    // por int (http://localhost:59914/ux-id-2/nome_tabela/id-pk/10000.jpg)
                    // ex: http://localhost:59914/ux-id-2/PRD_SKU_PRODUTO/id-pk/141083.jpg

                    SqlReaderSettings settingByInt = new SqlReaderSettings();
                    settingByInt.ConnectionString = settingByUidDocumento.ConnectionString;
                    settingByInt.PathPrefix = string.Concat("~/ux-id-", item.Key, "/", table.Value, "/id-pk/");
                    settingByInt.ImageIdType = System.Data.SqlDbType.BigInt;
                    settingByInt.ImageBlobQuery = string.Concat("SELECT TOP 1 dm.CONTEUDO as [content] FROM LX_DOC.DOC_MULTIMIDIA_TABELA dmt INNER JOIN LX_DOC.DOC_MULTIMIDIA dm ON dm.UID_DOCUMENTO = dmt.UID_DOCUMENTO WHERE dmt.UID_TABELA = '", table.Key, "' and dmt.ID_CHAVE = @id ORDER BY dmt.ORDEM_APRESENTACAO");
                    settingByInt.ModifiedDateQuery = string.Concat("SELECT TOP 1 dm.DATA_CRIACAO as [ModifiedDate], dm.DATA_CRIACAO as [CreatedDate] FROM LX_DOC.DOC_MULTIMIDIA_TABELA dmt INNER JOIN LX_DOC.DOC_MULTIMIDIA dm ON dm.UID_DOCUMENTO = dmt.UID_DOCUMENTO WHERE dmt.UID_TABELA = '", table.Key, "' and dmt.ID_CHAVE = @id ORDER BY dmt.ORDEM_APRESENTACAO");
                    settingByInt.ImageExistsQuery = string.Concat("SELECT TOP 1 COUNT(dm.UID_DOCUMENTO) FROM LX_DOC.DOC_MULTIMIDIA_TABELA dmt INNER JOIN LX_DOC.DOC_MULTIMIDIA dm ON dm.UID_DOCUMENTO = dmt.UID_DOCUMENTO WHERE dmt.UID_TABELA = '", table.Key, "' and dmt.ID_CHAVE = @id ");
                    settingByInt.CacheUnmodifiedFiles = true;
                    settingByInt.RequireImageExtension = true;
                    settingByInt.CheckForModifiedFiles = true;

                    Logger.Trace("adicionando ambiente por tabela/id '{0}' '{1}' ", settingByInt.PathPrefix, settingByInt.ConnectionString);
                    new SqlReaderPlugin(settingByInt).Install(ImageResizer.Configuration.Config.Current);

                    // por guid (http://localhost:59914/ux-id-2/nome_tabela/id-pk/016f991e-3295-4e61-8c9c-9335e5068ad4.jpg)
                    // ex: http://localhost:59914/ux-id-2/PRD_ARTIGO/uid-pk/00000000-0000-0000-0000-000000000000.jpg
                    SqlReaderSettings settingByGuid = new SqlReaderSettings();
                    settingByGuid.ConnectionString = settingByUidDocumento.ConnectionString;
                    settingByGuid.PathPrefix = string.Concat("~/ux-id-", item.Key, "/", table.Value, "/uid-pk/");
                    settingByGuid.ImageIdType = System.Data.SqlDbType.UniqueIdentifier;
                    settingByGuid.ImageBlobQuery = string.Concat("SELECT TOP 1 dm.CONTEUDO as [content] FROM LX_DOC.DOC_MULTIMIDIA_TABELA dmt INNER JOIN LX_DOC.DOC_MULTIMIDIA dm ON dm.UID_DOCUMENTO = dmt.UID_DOCUMENTO WHERE dmt.UID_TABELA = '", table.Key, "' and dmt.UID_CHAVE = @id ORDER BY dmt.ORDEM_APRESENTACAO");
                    settingByGuid.ModifiedDateQuery = string.Concat("SELECT TOP 1 dm.DATA_CRIACAO as [ModifiedDate], dm.DATA_CRIACAO as [CreatedDate] FROM LX_DOC.DOC_MULTIMIDIA_TABELA dmt INNER JOIN LX_DOC.DOC_MULTIMIDIA dm ON dm.UID_DOCUMENTO = dmt.UID_DOCUMENTO WHERE dmt.UID_TABELA = '", table.Key, "' and dmt.ID_CHAVE = @id ORDER BY dmt.ORDEM_APRESENTACAO");
                    settingByGuid.ImageExistsQuery = string.Concat("SELECT TOP 1 COUNT(dm.UID_DOCUMENTO) FROM LX_DOC.DOC_MULTIMIDIA_TABELA dmt INNER JOIN LX_DOC.DOC_MULTIMIDIA dm ON dm.UID_DOCUMENTO = dmt.UID_DOCUMENTO WHERE dmt.UID_TABELA = '", table.Key, "' and dmt.UID_CHAVE = @id ");
                    settingByGuid.CacheUnmodifiedFiles = true;
                    settingByGuid.RequireImageExtension = true;
                    settingByGuid.CheckForModifiedFiles = true;

                    Logger.Trace("adicionando ambiente por tabela/uid'{0}' '{1}' ", settingByGuid.PathPrefix, settingByGuid.ConnectionString);
                    new SqlReaderPlugin(settingByGuid).Install(ImageResizer.Configuration.Config.Current);
                }
            }

            ////Configure Sql Backend
            //SqlReaderSettings s = new SqlReaderSettings();
            ////s.ConnectionString = @"Data Source=a-srv111.linx-inves.com.br\inovacao;Initial Catalog=Development-UX-Application;User ID=fernando.chaves;Password=S0eusei@2022;";
            //s.ConnectionString = @"Data Source=hyfng608s0.database.windows.net;Initial Catalog=Development-UX-Application;User ID=linxsa@hyfng608s0;Password=02.sistemas@01;";
            //s.PathPrefix = "~/multimidiaaz/";
            //s.ImageIdType = System.Data.SqlDbType.UniqueIdentifier;
            //s.ImageBlobQuery = "SELECT CONTEUDO as content  FROM LX_DOC.DOC_MULTIMIDIA WHERE UID_DOCUMENTO = @id";
            //s.ModifiedDateQuery = "SELECT CAST('2000-01-01 00:00:00' AS DATETIME) AS ModifiedDate, CAST('2000-01-01 00:00:00' AS DATETIME) AS CreatedDate From LX_DOC.DOC_MULTIMIDIA WHERE  UID_DOCUMENTO = @id";
            //s.ImageExistsQuery = "SELECT COUNT(UID_DOCUMENTO) From LX_DOC.DOC_MULTIMIDIA WHERE  UID_DOCUMENTO = @id";
            //s.CacheUnmodifiedFiles = true;
            //s.RequireImageExtension = false;
            ////Add plugin
            //new SqlReaderPlugin(s).Install(ImageResizer.Configuration.Config.Current);

            ////http://localhost:59914/multimidiaux/3B68F807-CDB1-4B51-AB38-01BC3C7FC368.jpg
            //SqlReaderSettings s2 = new SqlReaderSettings();
            //s2.ConnectionString = @"Data Source=a-srv111.linx-inves.com.br\inovacao;Initial Catalog=Development-UX-Application;User ID=fernando.chaves;Password=S0eusei@2022;";
            //s2.PathPrefix = "~/multimidiaux/";
            //s2.ImageIdType = System.Data.SqlDbType.UniqueIdentifier;
            //s2.ImageBlobQuery = "SELECT CONTEUDO as content  FROM LX_DOC.DOC_MULTIMIDIA WHERE UID_DOCUMENTO = @id";
            //s2.ModifiedDateQuery = "SELECT CAST('2000-01-01 00:00:00' AS DATETIME) AS ModifiedDate, CAST('2000-01-01 00:00:00' AS DATETIME) AS CreatedDate From LX_DOC.DOC_MULTIMIDIA WHERE UID_DOCUMENTO = @id";
            //s2.ImageExistsQuery = "SELECT COUNT(UID_DOCUMENTO) From LX_DOC.DOC_MULTIMIDIA WHERE UID_DOCUMENTO = @id";
            //s2.CacheUnmodifiedFiles = true;
            //s2.RequireImageExtension = false;
            ////Add plugin
            //new SqlReaderPlugin(s2).Install(ImageResizer.Configuration.Config.Current);
        }

        private static Dictionary<string, string> GetApiConnectionStrings()
        {
            Logger.Info("Buscando strings de conexao...");
            Dictionary<string, string> retorno = new Dictionary<string, string>();
            try
            {
                var _serviceBus = System.Configuration.ConfigurationManager.AppSettings.GetValue("ServiceBus", "http://localhost:1710");
                var client = new RestClient(_serviceBus);
                
                var request = new RestRequest("Linx-Framework-BV-Autorizacao-AutorizacaoDomainService.svc/Json/GetJsonTcsAmbienteAcesso");
                request.AddParameter("$where", string.Format("NomeConexao=\"{0}\"", "connMediaService"));

                var url = client.BuildUri(request);
                Logger.Trace("Chamando api '{0}'", url.ToString());

                var response = client.ExecuteAsGet(request, "GET");

                if (response.ErrorException != null)
                    throw new Exception(response.ErrorException.Message);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    /* exemplo de saida
                        {
                            "GetJsonTcsAmbienteAcessoResult": {
                                "TotalCount": -1,
                                "IncludedResults": [],
                                "RootResults": [
                                    {
                                        "DescStringConexao": "Server: A-SRV111\\inovacao / Database: Development-UX-Application",
                                        "EntityKeyLocalRelation": 3,
                                        "IdLinx": 1,
                                        "IdTcsAmbiente": 2,
                                        "IdTcsAmbienteAcesso": 3,
                                        "IdTcsConexaoBancoServidor": 3,
                                        "NomeConexao": "ControleSistema",
                                        "NomeEmpresa": "Matriz Franqueador - Id_Linx 1 ",
                                        "StringConexao": "Data Source=a-srv111\\inovacao; Initial Catalog=Development-UX-Application ; Persist Security Info=True; Trusted_Connection=True;",
                                        "UidConexaoDb": "c96872d2-99f0-4a1d-a29b-0120690a35de",
                                        "UidGrupoAcesso": "f09bbc01-ce40-456d-a284-41a51745c576"
                                    },
                     */
                    var converter = new ExpandoObjectConverter();
                    dynamic responseObj = JsonConvert.DeserializeObject<ExpandoObject>(response.Content, converter);

                    // converte para dicionario
                    var RootResults = responseObj.GetJsonTcsAmbienteAcessoResult.RootResults;
                    foreach (var item in RootResults)
                    {
                        retorno.Add(item.IdTcsAmbiente.ToString(), item.StringConexao);
                    }


                    //dynamic d = JObject.Parse("{number:1000, str:'string', array: [1,2,3,4,5,6]}");
                    //dynamic o = JObject.Parse(response.Content);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retorno;
        }

        private static Dictionary<string, string> GetApiTableCodes()
        {
            Logger.Info("Buscando codigos das tabelas...");
            Dictionary<string, string> retorno = new Dictionary<string, string>();
            try
            {
                var _serviceBus = System.Configuration.ConfigurationManager.AppSettings.GetValue("ServiceBus", "http://localhost:1710");
                var client = new RestClient(_serviceBus);

                var request = new RestRequest("Linx-Framework-BV-Autorizacao-AutorizacaoDomainService.svc/Json/GetJsonTcsTabelaAutorizacao");

                var url = client.BuildUri(request);
                Logger.Trace("Chamando api '{0}'", url.ToString());

                var response = client.ExecuteAsGet(request, "GET");

                if (response.ErrorException != null)
                    throw new Exception(response.ErrorException.Message);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    /* exemplo de saida
                        {
                            "GetJsonTcsTabelaAutorizacaoResult": {
                                "TotalCount": -1,
                                "IncludedResults": [],
                                "RootResults": [
                                    {
                                        "DescStringConexao": "Server: A-SRV111\\inovacao / Database: Development-UX-Application",
                                        "EntityKeyLocalRelation": 3,
                                        "IdLinx": 1,
                                        "IdTcsAmbiente": 2,
                                        "IdTcsAmbienteAcesso": 3,
                                        "IdTcsConexaoBancoServidor": 3,
                                        "NomeConexao": "ControleSistema",
                                        "NomeEmpresa": "Matriz Franqueador - Id_Linx 1 ",
                                        "StringConexao": "Data Source=a-srv111\\inovacao; Initial Catalog=Development-UX-Application ; Persist Security Info=True; Trusted_Connection=True;",
                                        "UidConexaoDb": "c96872d2-99f0-4a1d-a29b-0120690a35de",
                                        "UidGrupoAcesso": "f09bbc01-ce40-456d-a284-41a51745c576"
                                    },
                     */
                    var converter = new ExpandoObjectConverter();
                    dynamic responseObj = JsonConvert.DeserializeObject<ExpandoObject>(response.Content, converter);

                    // converte para dicionario
                    var RootResults = responseObj.GetJsonTcsTabelaAutorizacaoResult.RootResults;
                    foreach (var item in RootResults)
                    {
                        retorno.Add(item.UidTabela, item.NomeTabela);
                    }


                    //dynamic d = JObject.Parse("{number:1000, str:'string', array: [1,2,3,4,5,6]}");
                    //dynamic o = JObject.Parse(response.Content);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retorno;
        }
    }   
}

