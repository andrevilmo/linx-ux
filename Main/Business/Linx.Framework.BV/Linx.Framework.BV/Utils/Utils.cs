using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Linx.Tools;
using System.Web;
using Linx.Framework.BV.Domains;
using System.IO;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using ImageResizer;
using Linx.Framework.BV.Autorizacao;

namespace Linx.Framework.BV
{
    public class Utils
    {
        public static string GetDescModulo(Int64 idModulo)
        {
            string descModulo = string.Empty;

            ModuloAutorizacao.ModuloAutorizacaoDomainService ds = new ModuloAutorizacao.ModuloAutorizacaoDomainService();

            //TcsModuloAutorizacao
            var moduloAutorizacao =
                (from result in ds.GetTcsModuloAutorizacaoNoAssociations().Where(i => i.IdModulo == idModulo)
                 select new { result.DescModulo, result.DescricaoAplicativo }).FirstOrDefault();

            //TcsModulo
            if (moduloAutorizacao.IsNullOrEmpty())
            {
                Modulo.ModuloDomainService moduloDs = new Modulo.ModuloDomainService();
                var moduloLocal =
                    (from result in moduloDs.GetTcsModuloNoAssociations().Where(i => i.IdModulo == idModulo)
                     select new { DescModulo = result.DescModulo, IdTcsAplicativo = result.IdTcsAplicativo }).FirstOrDefault();

                if (!moduloLocal.IsNullOrEmpty())
                    descModulo = string.Format("[{0}],[{1}],[{2}]", moduloLocal.DescModulo, GetDescAplicativo(moduloLocal.IdTcsAplicativo), "Local");

            }
            else
                descModulo = string.Format("[{0}],[{1}],[{2}]", moduloAutorizacao.DescModulo, moduloAutorizacao.DescricaoAplicativo, "Portal");

            return descModulo.IsNullOrEmpty() ? string.Empty : descModulo;
        }

        public static string GetDescTransacao(Int64 idTransacao)
        {
            TransacaoAutorizacao.TransacaoAutorizacaoDomainService ds = new TransacaoAutorizacao.TransacaoAutorizacaoDomainService();

            string descTransacao = string.Empty;

            //TcsTransacaoAutorizacao
            var tcsTransacaoAutorizacao =
               (from result in ds.GetTcsTransacaoAutorizacaoNoAssociations().Where(i => i.IdTransacao == idTransacao)
                select new { result.DescTransacao, result.ClasseNome, result.CodTransacao }).FirstOrDefault();

            if (!tcsTransacaoAutorizacao.IsNull())
                descTransacao = string.Format("[{0}],[{1}],[{2}],[{3}]", tcsTransacaoAutorizacao.DescTransacao, tcsTransacaoAutorizacao.ClasseNome, tcsTransacaoAutorizacao.CodTransacao, "Portal");

            //TcsTransacao
            if (descTransacao.IsNullOrEmpty())
            {
                Transacao.TransacaoDomainService transacaoDs = new Transacao.TransacaoDomainService();
                var tcsTransacao =
                    (from result in transacaoDs.GetTcsTransacaoNoAssociations().Where(i => i.IdTransacao == idTransacao)
                     select new { result.DescTransacao, result.ClasseNome, result.CodTransacao }).FirstOrDefault();

                if (!tcsTransacao.IsNull())
                    descTransacao = string.Format("[{0}],[{1}],[{2}],[{3}]", tcsTransacao.DescTransacao, tcsTransacao.ClasseNome, tcsTransacao.CodTransacao, "Local");

            }
            return descTransacao.IsNullOrEmpty() ? string.Empty : descTransacao;
        }

        public static string GetDescObjeto(Int64 idObjeto)
        {
            ObjetoAutorizacao.ObjetoAutorizacaoDomainService ds = new ObjetoAutorizacao.ObjetoAutorizacaoDomainService();

            string descObjeto = string.Empty;

            //TcsObjetoAutorizacao
            var tcsObjetoAutorizacao =
                (from result in ds.GetTcsObjetoAutorizacaoNoAssociations().Where(i => i.IdObjeto == idObjeto)
                 select new { result.DescObjeto, result.ClasseNome, result.LxTipoObjeto }).FirstOrDefault();

            if (!tcsObjetoAutorizacao.IsNull())
                descObjeto = string.Format("[{0}],[{1}],[{2}]", tcsObjetoAutorizacao.DescObjeto, tcsObjetoAutorizacao.ClasseNome, tcsObjetoAutorizacao.LxTipoObjeto);

            if (descObjeto.IsNullOrEmpty())
            {
                //TcsObjeto
                Objeto.ObjetoDomainService dsObjeto = new Objeto.ObjetoDomainService();
                var tcsObjeto =
                    (from result in dsObjeto.GetTcsObjetoNoAssociations().Where(i => i.IdObjeto == idObjeto)
                     select new { result.DescObjeto, result.ClasseNome, result.LxTipoObjeto }).FirstOrDefault();

                if (!tcsObjeto.IsNull())
                    descObjeto = string.Format("[{0}],[{1}],[{2}]", tcsObjeto.DescObjeto, tcsObjeto.ClasseNome, tcsObjeto.LxTipoObjeto);
            }
            return descObjeto.IsNullOrEmpty() ? string.Empty : descObjeto;
        }

        public static string GetDescAplicativo(int idTcsAplicativo)
        {
            string cacheKey = string.Format("DescricaoAplicativo_{0}", idTcsAplicativo);
            string descAplicativo = WebCacheHelper.GetWebCache<string>(cacheKey);

            if (descAplicativo.IsNullOrEmpty())
            {
                Aplicativo.AplicativoDomainService ds = new Aplicativo.AplicativoDomainService();
                descAplicativo = ds.GetTcsAplicativoNoAssociations().Where(i => i.IdTcsAplicativo == idTcsAplicativo).Select(i => i.DescricaoAplicativo).FirstOrDefault();

                if (!descAplicativo.IsNullOrEmpty())
                    WebCacheHelper.UpdateWebCache(cacheKey, descAplicativo, 720); //Expiração em 30 dias

            }
            return descAplicativo.IsNullOrEmpty() ? string.Empty : descAplicativo.ToString();
        }

        public static IQueryable<LookUpTransacao> GetLookupTransacao(EntitySearch entitySearch)
        {
            entitySearch.EntityName = string.Empty;
            TransacaoAutorizacao.TransacaoAutorizacaoDomainService dsAutorizacao = new TransacaoAutorizacao.TransacaoAutorizacaoDomainService();

            Transacao.TransacaoDomainService dsTransacao = new Transacao.TransacaoDomainService();

            List<Int64> modulosPermitidos = GetModulosPermitidos();

            List<LookUpTransacao> transacoesAut = (from result in dsAutorizacao.GetTcsTransacaoMenuAutorizacaoByEntitySearchNoAssociations(SerializationManager<List<EntitySearch>>.ObjectToString(new List<EntitySearch>() { entitySearch }))
                                                   where modulosPermitidos.Contains(result.IdModulo)
                                                   select new LookUpTransacao()
                                                   {
                                                       IdTransacao = result.IdTransacao,
                                                       ClasseNome = result.ClasseNome,
                                                       DescTransacao = result.DescTransacao,
                                                       CodTransacao = result.CodTransacao
                                                   }).Distinct().ToList();

            List<LookUpTransacao> transacoes = (from result in dsTransacao.GetTcsTransacaoByEntitySearchNoAssociations(SerializationManager<List<EntitySearch>>.ObjectToString(new List<EntitySearch>() { entitySearch }))
                                                select new LookUpTransacao()
                                                {
                                                    IdTransacao = result.IdTransacao,
                                                    ClasseNome = result.ClasseNome,
                                                    DescTransacao = result.DescTransacao,
                                                    CodTransacao = result.CodTransacao
                                                }).Distinct().ToList();

            return transacoesAut.Union(transacoes).Distinct().AsQueryable();
        }

        public static IQueryable<LookUpTransacao> GetLookUpTransacaoModulo(EntitySearch entitySearch)
        {
            entitySearch.EntityName = string.Empty;

            Int64 idUsuario = 0;
            List<Int64> lstModulos = new List<Int64>();

            //IdUsuario
            EntitySearchExpression expression = entitySearch.Expressions.Where(i => i.Name == "Field" && i.Value.ToString() == "IdUsuario").FirstOrDefault();
            if (!expression.IsNull())
            {
                int fieldPos = entitySearch.Expressions.IndexOf(expression);
                idUsuario = Convert.ToInt64(entitySearch.Expressions[fieldPos + 2].Value.ToString());

                RemoveExpressionFromEntitySearh(entitySearch, expression, fieldPos);
            }

            if (idUsuario.IsNullOrEmpty())
            {
                return GetLookupTransacao(entitySearch);
            }

            //Modulos
            expression = entitySearch.Expressions.Where(i => i.Name == "Field" && i.Value.ToString() == "IdModulo").FirstOrDefault();
            if (!expression.IsNull())
            {
                int fieldPos = entitySearch.Expressions.IndexOf(expression);
                var values = entitySearch.Expressions[fieldPos + 2].Value.ToString().Split(new string[] { "," }, StringSplitOptions.None);

                foreach (var item in values)
                {
                    lstModulos.Add(Convert.ToInt64(item));
                }

                RemoveExpressionFromEntitySearh(entitySearch, expression, fieldPos);
            }

            Transacao.TransacaoDomainService dsTransacao = new Transacao.TransacaoDomainService();
            Perfil.PerfilDomainService dsPerfil = new Perfil.PerfilDomainService();

            //Módulos do perfil
            List<Int64> lstPerfil = dsTransacao.GetTcsPerfilUsuario(idUsuario);
            return GetTransacaoList(lstPerfil, lstModulos, entitySearch);
        }

        public static IQueryable<LookUpTransacao> GetLookupTransacaoPerfil(EntitySearch entitySearch)
        {
            entitySearch.EntityName = string.Empty;

            List<Int64> lstModulos = new List<Int64>();
            List<Int64> lstPerfil = new List<Int64>();

            //IdPerfil
            EntitySearchExpression expression = entitySearch.Expressions.Where(i => i.Name == "Field" && i.Value.ToString() == "IdPerfil").FirstOrDefault();
            if (!expression.IsNull())
            {
                int fieldPos = entitySearch.Expressions.IndexOf(expression);

                lstPerfil.Add(Convert.ToInt32(entitySearch.Expressions[fieldPos + 2].Value));

                RemoveExpressionFromEntitySearh(entitySearch, expression, fieldPos);
            }

            if (lstPerfil.Count() == 0)
            {
                return GetLookupTransacao(entitySearch);
            }

            //Modulos
            expression = entitySearch.Expressions.Where(i => i.Name == "Field" && i.Value.ToString() == "IdModulo").FirstOrDefault();
            if (!expression.IsNull())
            {
                int fieldPos = entitySearch.Expressions.IndexOf(expression);
                var values = entitySearch.Expressions[fieldPos + 2].Value.ToString().Split(new string[] { "," }, StringSplitOptions.None);

                foreach (var item in values)
                {
                    lstModulos.Add(Convert.ToInt64(item));
                }
                RemoveExpressionFromEntitySearh(entitySearch, expression, fieldPos);
            }
            return GetTransacaoList(lstPerfil, lstModulos, entitySearch);
        }

        private static IQueryable<LookUpTransacao> GetTransacaoList(List<Int64> lstPerfil, List<Int64> lstModulos, EntitySearch entitySearch)
        {
            Autorizacao.AutorizacaoDomainService dsAutorizacao = new Autorizacao.AutorizacaoDomainService();
            Modulo.ModuloDomainService dsModulo = new Modulo.ModuloDomainService();
            Transacao.TransacaoDomainService dsTransacao = new Transacao.TransacaoDomainService();
            Perfil.PerfilDomainService dsPerfil = new Perfil.PerfilDomainService();
            ModuloAutorizacao.ModuloAutorizacaoDomainService dsModuloAut = new ModuloAutorizacao.ModuloAutorizacaoDomainService();
            TransacaoAutorizacao.TransacaoAutorizacaoDomainService dsTransacaoAut = new TransacaoAutorizacao.TransacaoAutorizacaoDomainService();

            lstModulos = ((from result in lstModulos select result).ToList().Union(
                (from result in dsPerfil.GetTcsPerfilRegraModuloNoAssociations().Where(i => lstPerfil.Contains(i.IdPerfil) && i.LxRegraAcessoModulo != 1) select result.IdModulo).ToList())
                ).Distinct().ToList();

            //Transações autorização
            List<Int64> lstTransacoesAut = (from result in dsTransacaoAut.GetTcsTransacaoMenuAutorizacaoNoAssociations().Where(i => lstModulos.Contains(i.IdModulo)) select result.IdTransacao).Distinct().ToList();

            List<Int64> modulosPermitidos = GetModulosPermitidos();
            List<Int64> modulosAut = lstModulos.Where(i => modulosPermitidos.Contains(i)).ToList();
            List<Int64> lstModuloMenuAut = (from result in dsModuloAut.GetTcsModuloMenuAutorizacaoNoAssociations().Where(i => modulosAut.Contains(i.IdModulo)) select result.IdModuloMenu).Distinct().ToList();

            //Transações locais
            List<Int64> lstModuloMenuLocal = (from result in dsModulo.GetTcsModuloMenuNoAssociations().Where(i => lstModulos.Contains(i.IdModulo)) select result.IdModuloMenu).Distinct().ToList();

            List<Int64> lstModuloMenu = ((from result in lstModuloMenuAut select result).Union(from result in lstModuloMenuLocal select result)).Distinct().ToList();

            List<Int64> lstTransacoesLocal = (from result in dsTransacao.GetTcsTransacaoMenuNoAssociations().Where(i => lstModuloMenu.Contains(i.IdModuloMenu)) select result.IdTransacao).Distinct().ToList();

            //Geral
            List<Int64> lstTransacoes = ((from result in lstTransacoesAut select result).Union(from result in lstTransacoesLocal select result)).Distinct().ToList();

            return ((from result in dsTransacaoAut.GetTcsTransacaoAutorizacaoByEntitySearchNoAssociations(SerializationManager<List<EntitySearch>>.ObjectToString(new List<EntitySearch>() { entitySearch }))
                     where lstTransacoes.Contains(result.IdTransacao) && !result.Inativo
                     select new LookUpTransacao() { IdTransacao = result.IdTransacao, ClasseNome = result.ClasseNome, DescTransacao = result.DescTransacao, Origem = "Portal" }).Distinct().ToList().Union
                    (from result in dsTransacao.GetTcsTransacaoByEntitySearchNoAssociations(SerializationManager<List<EntitySearch>>.ObjectToString(new List<EntitySearch>() { entitySearch }))
                     where lstTransacoes.Contains(result.IdTransacao) && !result.Inativo
                     select new LookUpTransacao() { IdTransacao = result.IdTransacao, ClasseNome = result.ClasseNome, DescTransacao = result.DescTransacao, Origem = "Local" }).Distinct().ToList()).Distinct().AsQueryable();
        }

        public static void RemoveExpressionFromEntitySearh(EntitySearch entitySearch, EntitySearchExpression expression, int fieldPos)
        {
            //remove expressions from list
            entitySearch.Expressions.RemoveAt(fieldPos + 2);
            entitySearch.Expressions.RemoveAt(fieldPos + 1);
            entitySearch.Expressions.RemoveAt(fieldPos);

            if (entitySearch.Expressions.Count() > 0)
            {
                if (fieldPos - 1 > 0 && entitySearch.Expressions[fieldPos - 1].Value.ToString() == "&&")
                {
                    entitySearch.Expressions.RemoveAt(fieldPos - 1);
                }
                else if (entitySearch.Expressions[fieldPos].Value.ToString() == "&&")
                {
                    entitySearch.Expressions.RemoveAt(fieldPos);
                }
            }
        }

        public static List<Int64> GetModulosPermitidos(bool hasAppFilter = false, Dictionary<string, string> headers = null)
        {
            List<Int64> modulosPermitidos = new List<Int64>();
            Empresa.EmpresaDomainService dsEmpresa = new Empresa.EmpresaDomainService();
            int idLinx = BusinessUserServiceHelper.GetCurrentIdLinx("ControleSistema", headers).GetValueOrDefault();

            if (hasAppFilter)
            {
                int? idTcsAplicativo = BusinessUserServiceHelper.GetCurrentApplicativeId(headers);
                modulosPermitidos = dsEmpresa.GetTcsEmpresaModuloNoAssociations().Where(i => i.IdLinx == idLinx && i.IdTcsAplicativo == idTcsAplicativo).Select(i => i.IdModulo).ToList();
            }
            else
                modulosPermitidos = dsEmpresa.GetTcsEmpresaModuloNoAssociations().Where(i => i.IdLinx == idLinx).Select(i => i.IdModulo).ToList();

            return modulosPermitidos;
        }

        public static IQueryable<LookUpModulo> GetLookUpModulo(EntitySearch entitySearch)
        {
            entitySearch.EntityName = string.Empty;
            ModuloAutorizacao.ModuloAutorizacaoDomainService ModuloAutorizacaoDs = new ModuloAutorizacao.ModuloAutorizacaoDomainService();
            Modulo.ModuloDomainService dsModulo = new Modulo.ModuloDomainService();

            List<Int64> modulosPermitidos = GetModulosPermitidos();

            List<LookUpModulo> moduloAut = (from result in ModuloAutorizacaoDs.GetTcsModuloAutorizacaoByEntitySearchNoAssociations(SerializationManager<List<EntitySearch>>.ObjectToString(new List<EntitySearch>() { entitySearch }))
                                            select new LookUpModulo()
                                            {
                                                IdModulo = result.IdModulo,
                                                DescModulo = result.DescModulo,
                                                IdTcsAplicativo = result.IdTcsAplicativo,
                                                DescAplicativo = result.DescricaoAplicativo
                                            }).Where(i => modulosPermitidos.Contains(i.IdModulo)).ToList();

            List<LookUpModulo> moduloLoc = (from result in dsModulo.GetTcsModuloByEntitySearchNoAssociations(SerializationManager<List<EntitySearch>>.ObjectToString(new List<EntitySearch>() { entitySearch }))
                                            select new LookUpModulo()
                                            {
                                                IdModulo = result.IdModulo,
                                                DescModulo = result.DescModulo,
                                                IdTcsAplicativo = result.IdTcsAplicativo
                                            }).ToList();

            moduloLoc = (from result in moduloLoc
                         select new LookUpModulo()
                         {
                             IdModulo = result.IdModulo,
                             DescModulo = result.DescModulo,
                             IdTcsAplicativo = result.IdTcsAplicativo,
                             DescAplicativo = GetDescAplicativo(result.IdTcsAplicativo)
                         }).ToList();

            return moduloAut.Union(moduloLoc).Distinct().AsQueryable();
        }

        public static IQueryable<LookupObjeto> GetLookUpObjeto(EntitySearch entitySearch)
        {
            entitySearch.EntityName = string.Empty;
            Autorizacao.AutorizacaoDomainService dsAutorizacao = new Autorizacao.AutorizacaoDomainService();
            ObjetoAutorizacao.ObjetoAutorizacaoDomainService dsObjetoAut = new ObjetoAutorizacao.ObjetoAutorizacaoDomainService();
            Objeto.ObjetoDomainService dsObjeto = new Objeto.ObjetoDomainService();

            return
                ((from result in dsObjetoAut.GetTcsObjetoAutorizacaoByEntitySearchNoAssociations(SerializationManager<List<EntitySearch>>.ObjectToString(new List<EntitySearch>() { entitySearch }))
                  select new LookupObjeto()
                  {
                      IdObjeto = result.IdObjeto,
                      DescObjeto = result.DescObjeto,
                      ClasseNome = result.ClasseNome,
                      LxTipoObjeto = result.LxTipoObjeto
                  }).Distinct().ToList().Union
                (from result in dsObjeto.GetTcsObjetoByEntitySearchNoAssociations(SerializationManager<List<EntitySearch>>.ObjectToString(new List<EntitySearch>() { entitySearch }))
                 select new LookupObjeto()
                 {
                     IdObjeto = result.IdObjeto,
                     DescObjeto = result.DescObjeto,
                     ClasseNome = result.ClasseNome,
                     LxTipoObjeto = result.LxTipoObjeto
                 }).Distinct().ToList()).Distinct().AsQueryable();
        }

        public static IQueryable<LookupModuloMenu> GetLookUpModuloMenu(EntitySearch entitySearch)
        {
            entitySearch.EntityName = string.Empty;
            ModuloAutorizacao.ModuloAutorizacaoDomainService ModuloAutorizacaoDs = new ModuloAutorizacao.ModuloAutorizacaoDomainService();
            Transacao.TransacaoDomainService dsTransacao = new Transacao.TransacaoDomainService();

            List<Int64> modulosPermitidos = GetModulosPermitidos();

            List<LookupModuloMenu> modulosAut = (from result in ModuloAutorizacaoDs.GetTcsModuloMenuAutorizacaoByEntitySearch(SerializationManager<List<EntitySearch>>.ObjectToString(new List<EntitySearch>() { entitySearch }))
                                                 select new LookupModuloMenu()
                                                 {
                                                     IdModulo = result.IdModulo,
                                                     IdModuloMenu = result.IdModuloMenu,
                                                     DescModulo = result.DescModulo,
                                                     DescModuloMenu = result.DescModuloMenu,
                                                     DescAplicativo = result.DescricaoAplicativo
                                                 }
                    ).Where(i => modulosPermitidos.Contains(i.IdModulo)).ToList();

            var modulosLoc1 = (from result in dsTransacao.GetTcsModuloMenuPByEntitySearchNoAssociations(SerializationManager<List<EntitySearch>>.ObjectToString(new List<EntitySearch>() { entitySearch }))
                               select new
                               {
                                   IdModulo = result.IdModulo,
                                   IdModuloMenu = result.IdModuloMenu,
                                   DescModulo = result.DescModulo,
                                   DescModuloMenu = result.DescModuloMenu,
                                   IdTcsAplicativo = result.IdTcsAplicativo
                               }).ToList();

            List<LookupModuloMenu> modulosLoc = (from result in modulosLoc1
                                                 select new LookupModuloMenu()
                                                 {
                                                     IdModulo = result.IdModulo,
                                                     IdModuloMenu = result.IdModuloMenu,
                                                     DescModulo = result.DescModulo,
                                                     DescModuloMenu = result.DescModuloMenu,
                                                     DescAplicativo = GetDescAplicativo(result.IdTcsAplicativo)
                                                 }).ToList();

            return modulosAut.Union(modulosLoc).Distinct().AsQueryable();
        }

        public static IQueryable<LookupTcsAplicativo> GetLookupTcsAplicativo(EntitySearch entitySearch)
        {
            entitySearch.EntityName = string.Empty;
            Aplicativo.AplicativoDomainService ds = new Aplicativo.AplicativoDomainService();

            return (from result in ds.GetTcsAplicativoByEntitySearchNoAssociations(SerializationManager<List<EntitySearch>>.ObjectToString(new List<EntitySearch>() { entitySearch }))
                    select new LookupTcsAplicativo() { IdTcsAplicativo = result.IdTcsAplicativo, DescricaoAplicativo = result.DescricaoAplicativo });
        }

        public static IQueryable<LookupTcsAmbienteRelacionado> GetLookupTcsAmbienteRelacionado(EntitySearch entitySearch)
        {
            Empresa.EmpresaDomainService ds = new Empresa.EmpresaDomainService();
            entitySearch.EntityName = "";
            int fieldPos = 0;
            int idTcsUsuarioAcesso = 0;

            List<int> lstIdLinxGpecon = new List<int>();

            //IdLinxGpecon
            EntitySearchExpression expression = entitySearch.Expressions.Where(i => i.Name == "Field" && i.Value.ToString() == "IdLinxGpecon").FirstOrDefault();
            if (!expression.IsNull())
            {
                fieldPos = entitySearch.Expressions.IndexOf(expression);
                int idLinxEnvironment = Convert.ToInt32(entitySearch.Expressions[fieldPos + 2].Value);

                //remove expressions from list
                Utils.RemoveExpressionFromEntitySearh(entitySearch, expression, fieldPos);
                lstIdLinxGpecon = ds.GetTcsEmpresaGpeconNoAssociations().Where(i => i.IdLinx == idLinxEnvironment).Select(i => i.IdLinxGpecon).Distinct().ToList();

                if (!lstIdLinxGpecon.Contains(idLinxEnvironment))
                    lstIdLinxGpecon.Add(idLinxEnvironment);

            }

            //IdTcsUsuarioAcesso
            expression = entitySearch.Expressions.Where(i => i.Name == "Field" && i.Value.ToString() == "IdTcsUsuarioAcesso").FirstOrDefault();
            if (!expression.IsNull())
            {
                fieldPos = entitySearch.Expressions.IndexOf(expression);
                idTcsUsuarioAcesso = Convert.ToInt32(entitySearch.Expressions[fieldPos + 2].Value);
                Utils.RemoveExpressionFromEntitySearh(entitySearch, expression, fieldPos);
            }

            Ambiente.AmbienteDomainService dsAmbiente = new Ambiente.AmbienteDomainService();

            var query = (from result in dsAmbiente.GetTcsAmbienteRelacionadoByEntitySearchNoAssociations(SerializationManager<List<EntitySearch>>.ObjectToString(new List<EntitySearch> { entitySearch }))
                         select result).ToList();

            return from result in dsAmbiente.GetTcsAmbienteRelacionadoByEntitySearchNoAssociations(SerializationManager<List<EntitySearch>>.ObjectToString(new List<EntitySearch> { entitySearch }))
                   where result.IdTcsAplicativo == 2 && result.IdTcsUsuarioAcesso != idTcsUsuarioAcesso && lstIdLinxGpecon.Contains(result.IdLinxAmbienteRelacionado)
                   select new LookupTcsAmbienteRelacionado
                   {
                       IdTcsAmbienteRelacionado = result.IdTcsAmbienteRelacionado,
                       IdLinxAmbienteRelacionado = result.IdLinxAmbienteRelacionado,
                       DescricaoAmbienteRelacionado = result.DescricaoAmbienteRelacionado,
                       DescricaoAplicacaoAmbienteRelacionado = result.DescricaoAplicacaoAmbienteRelacionado,
                       NomeEmpresaAmbienteRelacionado = result.NomeEmpresaAmbienteRelacionado
                   };
        }

        public static List<int> GetCompanyGpeconList(Dictionary<string, string> headers = null)
        {
            int idLinx = BusinessUserServiceHelper.GetCurrentIdLinxEnvironment(headers).GetValueOrDefault();
            List<int> gpeconList = new List<int> { idLinx };

            Empresa.EmpresaDomainService ds = new Empresa.EmpresaDomainService();
            gpeconList = gpeconList.Union(from result in ds.GetTcsEmpresaGpeconNoAssociations().Where(i => i.IdLinx == idLinx) select result.IdLinxGpecon).ToList();

            return gpeconList;
        }

        public class LookUpTransacao
        {
            public Int64 IdTransacao { get; set; }
            public String DescTransacao { get; set; }
            public String ClasseNome { get; set; }
            public String CodTransacao { get; set; }
            public string Origem { get; set; }
        }

        public class LookUpModulo
        {
            public Int64 IdModulo { get; set; }
            public String DescModulo { get; set; }
            public int IdTcsAplicativo { get; set; }
            public string DescAplicativo { get; set; }
        }

        public class LookupObjeto
        {
            public Int64 IdObjeto { get; set; }
            public String DescObjeto { get; set; }
            public String ClasseNome { get; set; }
            public byte LxTipoObjeto { get; set; }
        }

        public class LookupModuloMenu
        {
            public Int64 IdModuloMenu { get; set; }
            public Int64 IdModulo { get; set; }
            public string DescModuloMenu { get; set; }
            public String DescModulo { get; set; }
            public string DescAplicativo { get; set; }
            public string DescModuloMenuSuperior { get; set; }
            public bool Inativo { get; set; }
        }

        public class LookupTcsAplicativo
        {
            public int IdTcsAplicativo { get; set; }
            public string DescricaoAplicativo { get; set; }
        }

        public class LookupTcsAmbienteRelacionado
        {
            public int IdTcsAmbienteRelacionado { get; set; }
            public int IdLinxAmbienteRelacionado { get; set; }
            public string DescricaoAmbienteRelacionado { get; set; }
            public string DescricaoAplicacaoAmbienteRelacionado { get; set; }
            public string NomeEmpresaAmbienteRelacionado { get; set; }
        }

        public static List<string> GetObjectClassName(string accessList)
        {
            return accessList.IsNull() ? new List<string>() : accessList.Split(new string[] { "#" }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        #region : Multimidia

        public static string GetUrl()
        {
            if (HttpContext.Current != null && HttpContext.Current.Request != null)
            {
                string partialUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath;
                return string.Format("{0}{1}", partialUrl, partialUrl.Right(1) == "/" ? "" : "/");
            }
            else if (LinxHttpContext.HttpContext.Current != null && LinxHttpContext.HttpContext.Current.Request != null)
            {
                var baseUrl = LinxHttpContext.HttpContext.Current.Request.Inner.GetPropertyValue("RequestUri").GetPropertyValue("Authority");
                var pathUrl = (LinxHttpContext.HttpContext.Current.Request.Inner.GetPropertyValue("Properties") as Dictionary<string, object>).Where(x => x.Key == "MS_OwinContext").FirstOrDefault();
                baseUrl += (pathUrl.IsNullOrEmpty() ? "" : pathUrl.Value.GetPropertyValue("Request").GetPropertyValue("PathBase").ToString());
                baseUrl = (baseUrl.ToString().ToUpper().StartsWith("HTTP") ? "" : @"http://") + baseUrl;
                return string.Format("{0}{1}", baseUrl, baseUrl.ToString().Right(1) == "/" ? "" : "/");
            }
            else
                return String.Empty;
        }

        public static string GetMediaUrl(Guid uidDocumento, Dictionary<string, string> headers)
        {
            return String.Format(@"{0}LinxFrameworkMultimidia/GetMedia?uidDocumento={1}&uidGrupoAcesso={2}&uidEmpresa={3}&uidGrupoEconomico={4}&idAmbiente={5}&uidUsuario={6}", GetUrl(), uidDocumento, BusinessUserServiceHelper.GetCurrentAccessGroupId(headers).GetValueOrDefault().ToString(), BusinessUserServiceHelper.GetCurrentCompanyId(headers).GetValueOrDefault().ToString(), BusinessUserServiceHelper.GetCurrentEconomicGroupId(headers).GetValueOrDefault().ToString(), BusinessUserServiceHelper.GetCurrentEnvironmentId(headers).GetValueOrDefault().ToString(), BusinessUserServiceHelper.GetCurrentUserUid(headers).GetValueOrDefault().ToString());
        }

        public static string GetMediaThumbnailUrl(Guid uidDocumento, Dictionary<string, string> headers)
        {
            return String.Format(@"{0}LinxFrameworkMultimidia/GetMediaThumbnail?uidDocumento={1}&uidGrupoAcesso={2}&uidEmpresa={3}&uidGrupoEconomico={4}&idAmbiente={5}&uidUsuario={6}", GetUrl(), uidDocumento, BusinessUserServiceHelper.GetCurrentAccessGroupId(headers).GetValueOrDefault().ToString(), BusinessUserServiceHelper.GetCurrentCompanyId(headers).GetValueOrDefault().ToString(), BusinessUserServiceHelper.GetCurrentEconomicGroupId(headers).GetValueOrDefault().ToString(), BusinessUserServiceHelper.GetCurrentEnvironmentId(headers).GetValueOrDefault().ToString(), BusinessUserServiceHelper.GetCurrentUserUid(headers).GetValueOrDefault().ToString());
        }

        public static string GetMediaServiceBusUrl(Guid uidDocumento, string nomeArquivo)
        {
            string url = string.Empty;
            string serviceBusAddress = System.Configuration.ConfigurationManager.AppSettings["ImageServiceBus"];
            string extensao = !nomeArquivo.IsNullOrEmpty() ? Path.GetExtension(nomeArquivo) : ".png";

            if (!serviceBusAddress.IsNullOrEmpty())
            {
                url = string.Format(@"{0}ux-id-{1}/{2}{3}", serviceBusAddress, BusinessUserServiceHelper.GetCurrentEnvironmentId().GetValueOrDefault(), uidDocumento, extensao);
            }
            return url;
        }

        public static byte GetTipoMidia(string nomeArquivo)
        {
            //Image
            if (Regex.IsMatch(nomeArquivo, "\\.(gif|jpg|jpeg|png|bmp|tiff)", RegexOptions.IgnoreCase))
                return 1;
            //Video
            else if (Regex.IsMatch(nomeArquivo, "\\.(avi|mpeg|mp4|wmv|wma|flv|divx|wav|mkv|ogg|rm|3gp)", RegexOptions.IgnoreCase))
                return 2;
            //Document
            else if (Regex.IsMatch(nomeArquivo, "\\.(txt|csv|doc|docx|xls|xlsx|pdf|rtf)", RegexOptions.IgnoreCase))
                return 3;
            else
                //Other
                return 4;
        }

        public static byte[] CreateThumbnail(byte[] conteudo, string nomeArquivo)
        {
            byte[] thumb;

            var settings = new ResizeSettings
            {
                MaxWidth = 140,
                MaxHeight = 140
            };

            using (MemoryStream outStream = new MemoryStream())
            {
                using (MemoryStream inStream = new MemoryStream(conteudo))
                {
                    inStream.Position = 0;
                    ImageBuilder.Current.Build(inStream, outStream, settings, true);
                    outStream.Position = 0;
                    thumb = outStream.ToArray();
                }
            }
            return thumb;
        }

        #endregion

        # region : Cache

        public static void RemoveTcsAplicativoFromCache(int idTcsAplicativo)
        {
            WebCacheHelper.RemoveWebCache(string.Format("DescricaoAplicativo_{0}", idTcsAplicativo));
        }

        public static void RemoveUserModulesFromCache(Guid uidUsuario, int idAmbiente)
        {
            //Remove user module info from cache.
            WebCacheHelper.RemoveWebCache(string.Format("UserModules_{0}_{1}", idAmbiente, uidUsuario));
            WebCacheHelper.InvalidateCache(string.Format("UserAccess_{0}_{1}_", idAmbiente, uidUsuario));

            //Ambientes Relacionados
            List<Acesso> cache = WebCacheHelper.GetWebCache<List<Acesso>>(uidUsuario.ToString());
            if (!cache.IsNull())
            {
                List<Acesso> acessos = cache as List<Acesso>;
                if (!acessos.IsNullOrEmpty())
                {
                    acessos = acessos.Where(i => (i.IdTcsAmbiente == idAmbiente && !i.IdAmbienteRelacionado.IsNull()) || i.IdAmbienteRelacionado == idAmbiente).ToList();
                    foreach (Acesso item in acessos)
                    {
                        WebCacheHelper.RemoveWebCache(string.Format("UserModules_{0}_{1}_{2}", item.IdTcsAmbiente, item.IdAmbienteRelacionado, uidUsuario));
                    }
                }
            }
        }

        public static void RemoveUserBandeiraRedeFromCache(Guid uidUsuario, int idAmbiente)
        {
            //Remove user BandeiraRede info from cache.
            WebCacheHelper.RemoveWebCache(string.Format("UserBandeiraRede_{0}_{1}", uidUsuario, idAmbiente));

            if (!idAmbiente.IsNull())
            {
                Autorizacao.AutorizacaoDomainService dsAutorizacao = new Autorizacao.AutorizacaoDomainService();
                List<TcsUsuarioAcesso> ambienteInfo = dsAutorizacao.GetTcsUsuarioAcessoNoAssociations().Where(i => (i.IdTcsAmbiente == idAmbiente && i.IdTcsAmbienteRelacionado != null) || i.IdTcsAmbienteRelacionado == idAmbiente).ToList();

                foreach (TcsUsuarioAcesso item in ambienteInfo)
                {
                    WebCacheHelper.RemoveWebCache(string.Format("UserBandeiraRede_{0}_{1}", uidUsuario, item.IdTcsAmbiente + "_" + item.IdTcsAmbienteRelacionado));
                }
            }
        }

        public static void RemoveModulesFromCache(int? idAmbiente = null)
        {
            //Remove all Modules info from cache.
            WebCacheHelper.InvalidateCache(String.Format("UserModules_{0}", idAmbiente.IsNull() ? "" : idAmbiente.ToString()));
            WebCacheHelper.InvalidateCache(String.Format("UserAccess_{0}", idAmbiente.IsNull() ? "" : idAmbiente.ToString()));

            if (!idAmbiente.IsNull())
            {
                Autorizacao.AutorizacaoDomainService dsAutorizacao = new Autorizacao.AutorizacaoDomainService();
                List<TcsUsuarioAcesso> ambienteInfo = dsAutorizacao.GetTcsUsuarioAcessoNoAssociations().Where(i => (i.IdTcsAmbiente == idAmbiente && i.IdTcsAmbienteRelacionado != null) || i.IdTcsAmbienteRelacionado == idAmbiente).ToList();

                foreach (TcsUsuarioAcesso item in ambienteInfo)
                {
                    WebCacheHelper.InvalidateCache(String.Format("UserModules_{0}", item.IdTcsAmbiente + "_" + item.IdTcsAmbienteRelacionado));
                }
            }
        }

        public static void RemoveBandeiraRedeFromCache()
        {
            //Remove all BandeiraRede info from cache.
            WebCacheHelper.InvalidateCache("UserBandeiraRede_");
        }

        public static void RemoveUserInfoFromCache(Guid uidUsuario)
        {
            //Remove UserInfo from cache.
            WebCacheHelper.InvalidateCache(String.Format("UserInfo_{0}", uidUsuario.ToString()));
        }

        public static void CleanCache()
        {
            //Remove todas as chaves do cache.
            WebCacheHelper.CleanCache();
        }

        public static void RemoveConnectionsFromCache()
        {
            Conexao.ConexaoDomainService ds = new Conexao.ConexaoDomainService();

            var connections = (from result in ds.GetTcsConexaoDbNoAssociations()
                               select result.NomeConexao
                               ).ToList();

            //Parallel.ForEach(connections, connection =>
            //    {
            //        WebCacheHelper.InvalidateCache(connection);
            //    });

            connections.ForEach(connection =>
            {
                WebCacheHelper.InvalidateCache(connection);
            });

        }

        public static void RemoveConnectionFromCache(string nomeConexao, int idTcsAmbiente)
        {
            WebCacheHelper.RemoveWebCache(string.Format("{0}-{1}", nomeConexao, idTcsAmbiente));
        }

        public static void RemoveTelerikReportsFromCache()
        {
            WebCacheHelper.RemoveWebCache("TelerikReportsList");
        }

        public static void RemoveEnvironentInfoFromCache(int idTcsAmbiente)
        {
            WebCacheHelper.RemoveWebCache(string.Format("EnvironmentInfo_{0}", idTcsAmbiente));
        }

        public static void RemoveServiceInfoFromCache(int idTcsAmbiente)
        {
            WebCacheHelper.RemoveWebCache(string.Format("EnvironmentAlternativeServices_{0}", idTcsAmbiente));

            Autorizacao.AutorizacaoDomainService dsAutorizacao = new Autorizacao.AutorizacaoDomainService();

            List<TcsUsuarioAcesso> ambienteInfo = dsAutorizacao.GetTcsUsuarioAcessoNoAssociations().Where(i => (i.IdTcsAmbiente == idTcsAmbiente && i.IdTcsAmbienteRelacionado != null) || i.IdTcsAmbienteRelacionado == idTcsAmbiente).ToList();

            foreach (TcsUsuarioAcesso item in ambienteInfo)
            {
                WebCacheHelper.RemoveWebCache(string.Format("EnvironmentAlternativeServices_{0}", item.IdTcsAmbiente + "_" + item.IdTcsAmbienteRelacionado));
            }
        }

        public static void RemoveBrandInfoFromCache(Guid? uidUsuario)
        {
            WebCacheHelper.InvalidateCache(String.Format("BrandInfo_{0}", uidUsuario.IsNull() ? "" : uidUsuario.ToString()));
        }

        public static void RemoveGpeconInfoFromCache(Guid? uidUsuario)
        {
            if (uidUsuario.IsNullOrEmpty()) {
                WebCacheHelper.InvalidateCache("GpeconInfo_");
            }
            else
            {
                WebCacheHelper.RemoveWebCache(string.Format("GpeconInfo_{0}", uidUsuario));
            }
        }

        #endregion

        public static string ChangeSpecialCharacters(string input)
        {
            string[] GroupNames = new string[] {
                "grupo_a", "grupo_e", "grupo_i", "grupo_o", "grupo_u", "grupo_n", "grupo_y", "grupo_ç",
                "grupo_A", "grupo_E", "grupo_I", "grupo_O", "grupo_U", "grupo_N", "grupo_Ç"
            };

            Dictionary<string, string> Replaces = new Dictionary<string, string>(15);
            Replaces.Add("grupo_a", "a");
            Replaces.Add("grupo_e", "e");
            Replaces.Add("grupo_i", "i");
            Replaces.Add("grupo_o", "o");
            Replaces.Add("grupo_u", "u");
            Replaces.Add("grupo_n", "n");
            Replaces.Add("grupo_y", "y");
            Replaces.Add("grupo_ç", "c");

            Replaces.Add("grupo_A", "A");
            Replaces.Add("grupo_E", "E");
            Replaces.Add("grupo_I", "I");
            Replaces.Add("grupo_O", "O");
            Replaces.Add("grupo_U", "U");
            Replaces.Add("grupo_N", "N");
            Replaces.Add("grupo_Ç", "C");

            Regex reg = new Regex(@"(?<grupo_e>[éÉèÈêÊëË])|(?<grupo_u>[úùûü])|(?<grupo_i>[íìîï])|(?<grupo_o>[óòôõö])|(?<grupo_a>[áàâãä])|(?<grupo_E>[ÉÈÊË])|(?<grupo_U>[ÚÙÛÜ])|(?<grupo_I>[ÍÌÎÏ])|(?<grupo_O>[ÓÒÔÕÖ])|(?<grupo_A>[ÁÀÂÃÄ])|(?<grupo_n>[ñ])|(?<grupo_N>[Ñ])|(?<grupo_y>[ÿ])|(?<grupo_ç>[ç])|(?<grupo_Ç>[Ç])");

            var str = reg.Replace(input, delegate (Match match)
            {
                foreach (var groupName in GroupNames)
                {
                    if (match.Groups[groupName].Length > 0)
                    {
                        return Replaces[groupName];
                    }
                }

                return string.Empty;
            });

            return Regex.Replace(Regex.Replace(str, @"[^0-9a-zA-Z/s]+?", " "), @"-{2,}", " ").Trim();
        }

        public static string GetSHA1Hash(string key, string input)
        {
            byte[] encondedKey = Encoding.ASCII.GetBytes(key);
            byte[] value = Encoding.ASCII.GetBytes(input);
            System.Security.Cryptography.HMACSHA1 hma = new System.Security.Cryptography.HMACSHA1(encondedKey);
            return hma.ComputeHash(value).Aggregate("", (s, e) => s + String.Format("{0:x2}", e), s => s);
        }
    }
}
