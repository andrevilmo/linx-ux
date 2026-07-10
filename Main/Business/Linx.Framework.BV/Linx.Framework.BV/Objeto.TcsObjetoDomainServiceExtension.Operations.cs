using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Animation;
using Linx.Data;
using Linx.Tools;
using System.Data.Entity.Core.Objects;
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
using Linx.Framework.BV.Autorizacao;
using Newtonsoft.Json.Linq;

namespace Linx.Framework.BV.Objeto
{

    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////// Domain Service Extension ////////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class ObjetoDomainService
    {
        [Query(HasSideEffects = true)]
        public IQueryable<TcsObjetoConteudoMnt> GetTcsObjetoConteudoMnt_Limpo()
        {
            IQueryable<TcsObjetoConteudoMnt> result =
                from entity0 in this.DbContext.TCS_OBJETO_CONTEUDO
                    //where entity0.Type == 1//tipo excel
                select new TcsObjetoConteudoMnt()
                {
                    IdObjetoConteudo = entity0.ID_OBJETO_CONTEUDO,
                    ConteudoXml = null,
                    IdObjeto = entity0.ID_OBJETO // entity0.TCS_OBJETO.UID_OBJETO
                };

            return result;
        }

        [Query(HasSideEffects = true)]
        public IEnumerable<TcsObjeto> GetTcsObjetoLayout(string serializedEntitySearch)
        {

            return null;

            ////Local Layouts - Customer Database
            //List<TcsObjeto> tcsObjeto = this.GetTcsObjetoByEntitySearch(serializedEntitySearch).ToList();

            ////Linx Layouts - Autorization Database
            //AutorizacaoDomainService ds = new AutorizacaoDomainService();
            //List<TcsObjeto> tcsObjetoAutorizacao =
            //    (from result in ds.GetTcsObjetoAutorizacaoByEntitySearch(serializedEntitySearch)
            //     select new TcsObjeto
            //     {
            //         ClasseNome = result.ClasseNome,
            //         DescObjeto = result.DescObjeto,
            //         LxTipoObjeto = result.LxTipoObjeto,
            //         PathObjeto = result.PathObjeto,
            //         UidObjeto = result.UidObjeto,
            //         ObjetoLinx = result.ObjetoLinx,
            //         TcsObjetoConteudoList =
            //         (
            //             from result1 in result.TcsObjetoConteudoAutorizacaoList
            //             select new TcsObjetoConteudo
            //             {
            //                 ConteudoXml = result1.ConteudoXml,
            //                 UidObjeto = result1.UidObjeto,
            //                 UidObjetoConteudo = result1.UidObjetoConteudo
            //             }
            //         ),
            //         TcsLayoutList =
            //         (
            //         from result2 in result.TcsObjetoConteudoAutorizacaoList
            //         select new TcsLayout
            //         {
            //             ConteudoXml = result2.ConteudoXml,
            //             DescLayout = result2.DescLayout,
            //             Detalhes = result2.Detalhes,
            //             Idioma = result2.Idioma,
            //             Inativo = result2.Inativo,
            //             LayoutPadrao = result2.LayoutPadrao,
            //             LxConteudoObjeto = result2.LxConteudoObjeto,
            //             LxTipoLayout = result2.LxTipoLayout,
            //             NomeUsuario = null,
            //             PossuiFiltro = result2.PossuiFiltro,
            //             Publico = result2.Publico,
            //             UidLayout = result2.UidObjetoConteudo,
            //             UidObjeto = result2.UidObjeto,
            //             UidObjetoConteudo = result2.UidObjetoConteudo,
            //             UidUsuario = null,
            //             UltAtualizacao = result2.UltAtualizacao,
            //             LayoutLinx = result2.LayoutLinx
            //         }
            //         )
            //     }).ToList();

            //return
            //    (from result in tcsObjeto
            //     select result).Union
            //    (from result1 in tcsObjetoAutorizacao
            //     select result1);
        }

        [Query(HasSideEffects = false)]
        public IQueryable<TcsObjetoConteudoMnt> GetTcsObjetoConteudoMnt_LimpoJson()
        {
            return GetTcsObjetoConteudoMnt_Limpo();
        }

        [Invoke(HasSideEffects = true)]
        public IEnumerable<ConfiguracaoExportacao> GetConfiguracaoExportacao(bool isExcel, string parentFullName, Guid uidUsuario)
        {
            int idGpecon = BusinessUserServiceHelper.GetCurrentIdGpecon().GetValueOrDefault();
            byte lxObjCnt = byte.Parse(isExcel ? Domains.TipoConteudoObjeto.ConfigExportExcel.Value : Domains.TipoConteudoObjeto.ConfigExportReport.Value);
            byte lxObj = byte.Parse(Domains.TipoObjeto.BO.Value);

            var confs =
                from oc in this.DbContext.TCS_OBJETO_CONTEUDO
                join o in this.DbContext.TCS_OBJETO on oc.ID_OBJETO equals o.ID_OBJETO
                join ol in this.DbContext.TCS_LAYOUT_USUARIO on oc.ID_OBJETO_CONTEUDO equals ol.ID_OBJETO_CONTEUDO
                join lt in this.DbContext.TCS_LAYOUT on oc.ID_OBJETO_CONTEUDO equals lt.ID_OBJETO_CONTEUDO
                where oc.LX_CONTEUDO_OBJETO == lxObjCnt &&
                //oc.TCS_LAYOUT_LISTA.TCS_LAYOUT_USUARIO_LISTA.Any(u => u.UID_USUARIO == uidUsuario) && /*Filtra somente os que layouts que o usuário criou */
                o.PATH_OBJETO == parentFullName &&
                o.LX_TIPO_OBJETO == lxObj &&
                (oc.ID_GPECON == idGpecon || oc.ID_GPECON == null)
                select new
                {
                    id = oc.ID_OBJETO_CONTEUDO,
                    name = oc.TCS_LAYOUT_LISTA.DESC_LAYOUT,
                    conteudo = oc.CONTEUDO_XML,
                    idUsuario = ol.ID_USUARIO,
                    nomeUsuario = ol.TCS_USUARIO.NOME_USUARIO,
                    idGpecon = oc.ID_GPECON
                };

            long idUsuario = BusinessUserServiceHelper.GetCurrentUserId().GetValueOrDefault();

            List<ConfiguracaoExportacao> confList = new List<ConfiguracaoExportacao>();
            foreach (var oc in confs)
            {
                var conf = JObject.Parse(oc.conteudo);
                confList.Add(new ConfiguracaoExportacao
                {
                    Id = oc.id,
                    Name = oc.name,
                    Adapter = conf.Value<string>("Adapter"),
                    Columns = conf.Value<string>("Columns"),
                    JEntitySearch = conf.Value<string>("JEntitySearch"),
                    TranslatedJEntitySearch = conf.Value<string>("TranslatedJEntitySearch"),
                    BasicFeedUrl = conf.Value<string>("BasicFeedUrl"),
                    ExportMedia = conf.Value<bool>("ExportMedia"),
                    ParentFullTypeName = parentFullName,
                    IsExcelDataSource = isExcel,
                    UserId = oc.idUsuario,
                    UserName = oc.nomeUsuario,
                    IsUserLayout = (oc.idUsuario == idUsuario),
                    AllowMultipleGpecon = oc.idGpecon == null
                });
            }

            return confList.AsQueryable();
        }

        [Invoke(HasSideEffects = true)]
        public void SaveConfiguracaoExportacao(ConfiguracaoExportacao configuracaoExportacao, string jsonContent, Guid uidUsuario)
        {
            if (configuracaoExportacao == null) throw new ArgumentNullException("configuracaoExportacao");
            if (configuracaoExportacao.ParentFullTypeName == null) throw new NullReferenceException("configuracaoExportacao.ParentFullTypeName");
            if (configuracaoExportacao.Adapter == null) throw new NullReferenceException("configuracaoExportacao.Adapter");
            if (configuracaoExportacao.Name == null) throw new NullReferenceException("configuracaoExportacao.Name");

            try
            {
                byte lxObjCnt = byte.Parse(configuracaoExportacao.IsExcelDataSource ? Domains.TipoConteudoObjeto.ConfigExportExcel.Value : Domains.TipoConteudoObjeto.ConfigExportReport.Value);
                byte lxObj = byte.Parse(Domains.TipoObjeto.BO.Value);
                byte lxLayout = byte.Parse(Domains.TipoLayout.UserLayout.Value);

                Usuario.UsuarioDomainService dsUsuario = new Usuario.UsuarioDomainService();
                Int64 idUsuario = dsUsuario.GetUserId(uidUsuario);

                TCS_OBJETO objeto = null;
                TCS_OBJETO_CONTEUDO objetoConteudo = null;
                TCS_LAYOUT layout = null;

                Int64 idObjeto = -1;

                objeto = this.DbContext.TCS_OBJETO.FirstOrDefault(o => o.PATH_OBJETO == configuracaoExportacao.ParentFullTypeName && o.LX_TIPO_OBJETO == lxObj);

                if (objeto.IsNull())
                {
                    idObjeto = -1;
                    TCS_OBJETO tcsObjeto = new TCS_OBJETO
                    {
                        DESC_OBJETO = "Obj Negócio(" + (configuracaoExportacao.IsExcelDataSource ? "Excel" : "Report") + ")-" + Guid.NewGuid().ToString(),
                        ID_OBJETO = idObjeto,
                        LX_TIPO_OBJETO = lxObj,
                        PATH_OBJETO = configuracaoExportacao.ParentFullTypeName
                    };

                    this.DbContext.TCS_OBJETO.Add(tcsObjeto);
                    this.DbContext.SaveChanges();
                    idObjeto = tcsObjeto.ID_OBJETO;
                }
                else
                    idObjeto = objeto.ID_OBJETO;

                if (!configuracaoExportacao.Id.IsNullOrEmpty())
                {
                    objetoConteudo = this.DbContext.TCS_OBJETO_CONTEUDO.FirstOrDefault(oc => oc.ID_OBJETO_CONTEUDO == configuracaoExportacao.Id && oc.LX_CONTEUDO_OBJETO == lxObjCnt);
                    layout = this.DbContext.TCS_LAYOUT.Include("TCS_LAYOUT_USUARIO_LISTA").FirstOrDefault(oc => oc.ID_OBJETO_CONTEUDO == configuracaoExportacao.Id);
                }

                int? idGpecon = (configuracaoExportacao.AllowMultipleGpecon ? null : BusinessUserServiceHelper.GetCurrentIdGpecon());

                if (objetoConteudo.IsNull())
                {
                    objetoConteudo = new TCS_OBJETO_CONTEUDO
                    {
                        ID_OBJETO_CONTEUDO = -1,
                        ID_OBJETO = idObjeto,
                        LX_CONTEUDO_OBJETO = lxObjCnt,
                        CONTEUDO_XML = jsonContent,
                        ID_GPECON = idGpecon
                    };
                    this.DbContext.TCS_OBJETO_CONTEUDO.Add(objetoConteudo);
                }
                else
                {
                    if (objetoConteudo.ID_OBJETO != idObjeto)
                        objetoConteudo.ID_OBJETO = idObjeto;
                    objetoConteudo.CONTEUDO_XML = jsonContent;
                    objetoConteudo.ID_GPECON = idGpecon;

                    this.DbContext.Entry(objetoConteudo).State = System.Data.Entity.EntityState.Modified;
                }

                if (layout.IsNull())
                {
                    layout = new TCS_LAYOUT
                    {
                        DESC_LAYOUT = configuracaoExportacao.Name,
                        LAYOUT_PADRAO = false,
                        LX_TIPO_LAYOUT = lxLayout,
                        ULT_ATUALIZACAO = DateTime.Now,
                        TCS_OBJETO_CONTEUDO = objetoConteudo
                    };
                    if (layout.TCS_LAYOUT_USUARIO_LISTA.IsNull())
                        layout.TCS_LAYOUT_USUARIO_LISTA = new List<TCS_LAYOUT_USUARIO>();
                    layout.TCS_LAYOUT_USUARIO_LISTA.Add(new TCS_LAYOUT_USUARIO() { ID_USUARIO = idUsuario });
                    this.DbContext.TCS_LAYOUT.Add(layout);
                }
                else
                {
                    if (layout.DESC_LAYOUT != configuracaoExportacao.Name)
                        layout.DESC_LAYOUT = configuracaoExportacao.Name;
                    layout.ULT_ATUALIZACAO = DateTime.Now;
                    var layoutUsuario = layout.TCS_LAYOUT_USUARIO_LISTA.FirstOrDefault();
                    if (layoutUsuario == null)
                    {
                        if (layout.TCS_LAYOUT_USUARIO_LISTA.IsNull())
                            layout.TCS_LAYOUT_USUARIO_LISTA = new List<TCS_LAYOUT_USUARIO>();
                        layout.TCS_LAYOUT_USUARIO_LISTA.Add(new TCS_LAYOUT_USUARIO() { ID_USUARIO = idUsuario });
                    }
                    else
                    {
                        layoutUsuario.ID_USUARIO = idUsuario;
                    }

                    this.DbContext.Entry(layout).State = System.Data.Entity.EntityState.Modified;
                }

                this.DbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao salvar a configuração: " + e.ToString());
            }
        }

        [Invoke(HasSideEffects = true)]
        public void DeleteConfiguracaoExportacao(Int64 idConfiguracaoExportacao)
        {
            var lxObjCnts = new List<byte> { byte.Parse(Domains.TipoConteudoObjeto.ConfigExportExcel.Value), byte.Parse(Domains.TipoConteudoObjeto.ConfigExportReport.Value) };
            bool save = false;

            var objetoConteudo = this.DbContext.TCS_OBJETO_CONTEUDO.FirstOrDefault(oc => oc.ID_OBJETO_CONTEUDO == idConfiguracaoExportacao && lxObjCnts.Contains(oc.LX_CONTEUDO_OBJETO));
            var objetoLayout = this.DbContext.TCS_LAYOUT.FirstOrDefault(oc => oc.ID_OBJETO_CONTEUDO == idConfiguracaoExportacao);

            if (this.DbContext.TCS_LAYOUT_USUARIO.Any(u => u.ID_OBJETO_CONTEUDO == idConfiguracaoExportacao))
            {
                this.DbContext.TCS_LAYOUT_USUARIO
                    .Where(u => u.ID_OBJETO_CONTEUDO == idConfiguracaoExportacao)
                    .Foreach(u => DbContext.Entry(u).State = System.Data.Entity.EntityState.Deleted);
                save = true;
            }

            if (this.DbContext.TCS_LAYOUT_USUARIO.Any(u => u.ID_OBJETO_CONTEUDO == idConfiguracaoExportacao))
            {
                this.DbContext.TCS_LAYOUT_USUARIO
                    .Where(u => u.ID_OBJETO_CONTEUDO == idConfiguracaoExportacao)
                    .Foreach(u => DbContext.Entry(u).State = System.Data.Entity.EntityState.Deleted);
                save = true;
            }

            if (objetoLayout != null)
            {
                this.DbContext.TCS_LAYOUT.Remove(objetoLayout);
                save = true;
            }

            if (objetoConteudo != null)
            {
                this.DbContext.TCS_OBJETO_CONTEUDO.Remove(objetoConteudo);
                save = true;
            }
            if (save)
                this.DbContext.SaveChanges();
        }

        [Invoke(HasSideEffects = true)]
        public bool CanDeleteConfiguracaoExportacao(Int64 idConfiguracaoExportacao, Guid uidUsuario)
        {
            Usuario.UsuarioDomainService dsUsuario = new Usuario.UsuarioDomainService();
            Int64 idUsuario = dsUsuario.GetUserId(uidUsuario);
            return this.DbContext.TCS_LAYOUT_USUARIO.Any(u => u.ID_OBJETO_CONTEUDO == idConfiguracaoExportacao && u.ID_USUARIO == idUsuario);
        }

        [Invoke(HasSideEffects = true)]
        public IEnumerable<ConfiguracaoExportacao> GetPivotLayouts(string rootNameSpace, string viewName, string pivotName, string pivotDataSource, long? userId)
        {
            byte lxObjCnt = byte.Parse(Domains.TipoConteudoObjeto.SaveLayoutPivotTable.Value);
            byte lxObj = byte.Parse(Domains.TipoObjeto.BO.Value);

            var classNome = String.Format("{0}.{1}.{2}.{3}", rootNameSpace, viewName, pivotName, pivotDataSource);

            var confs =
                from oc in this.DbContext.TCS_OBJETO_CONTEUDO
                join o in this.DbContext.TCS_OBJETO on oc.ID_OBJETO equals o.ID_OBJETO
                join ol in this.DbContext.TCS_LAYOUT_USUARIO on oc.ID_OBJETO_CONTEUDO equals ol.ID_OBJETO_CONTEUDO
                where oc.LX_CONTEUDO_OBJETO == lxObjCnt &&
                      o.CLASSE_NOME == classNome &&
                      o.LX_TIPO_OBJETO == lxObj
                select new
                {
                    uid = oc.ID_OBJETO_CONTEUDO,
                    name = oc.TCS_LAYOUT_LISTA.DESC_LAYOUT,
                    classNome = o.CLASSE_NOME,
                    idUsuario = ol.ID_USUARIO,
                    nomeUsuario = ol.TCS_USUARIO.NOME_USUARIO
                };
            if (!confs.Any())
                return Enumerable.Empty<ConfiguracaoExportacao>();

            List<ConfiguracaoExportacao> confList = new List<ConfiguracaoExportacao>();

            foreach (var oc in confs)
            {
                var splittedClassNome = oc.classNome.Split(new string[] { ".SPA." }, StringSplitOptions.RemoveEmptyEntries);

                if (CanSeeLayout(oc.uid, userId))
                    confList.Add(new ConfiguracaoExportacao
                    {
                        Id = oc.uid,
                        Name = oc.name,
                        ProjectName = splittedClassNome.GetValue(0) + ".SPA",
                        ViewName = splittedClassNome.GetValue(1).ToString().Split('.').GetValue(0).ToString(),
                        PivotName = splittedClassNome.GetValue(1).ToString().Split('.').GetValue(1).ToString(),
                        Adapter = splittedClassNome.GetValue(1).ToString().Split('.').GetValue(2).ToString(),
                        UserId = oc.idUsuario,
                        UserName = oc.nomeUsuario,
                        IsUserLayout = (oc.idUsuario == userId)
                    });
            }

            return confList.AsQueryable();
        }

        private bool CanSeeLayout(long idObjetoConteudo, long? userId)
        {
            ObjetoDomainService ds = new ObjetoDomainService();
            var qry = ds.GetTcsObjetoPermissaoNoAssociations().Where(i => i.IdObjetoConteudo == idObjetoConteudo).ToList();

            if (qry.Count() == 0)
                return true;

            if (qry.Any(x => x.IdUsuario == userId))
                return true;

            Usuario.UsuarioDomainService dsUsuario = new Usuario.UsuarioDomainService();
            var perfilLst = dsUsuario.GetTcsUsuarioPerfilNoAssociations().Where(i => !i.Inativo && i.IdUsuario == userId).Select(i => i.IdPerfil).ToList();

            foreach (var item in perfilLst)
            {
                if (qry.Any(x => x.IdPerfil == item))
                    return true;
            }

            return false;
        }

        //not implemented
        [Invoke(HasSideEffects = true)]
        public ConfiguracaoExportacao GetPivotLayout(Int64 idObjetoConteudo)
        {
            long userId = BusinessUserServiceHelper.GetCurrentUserId().GetValueOrDefault();
            var configuracaoExportacao = new ConfiguracaoExportacao();
            var confs =
                from oc in this.DbContext.TCS_OBJETO_CONTEUDO
                join ol in this.DbContext.TCS_LAYOUT_USUARIO on oc.ID_OBJETO_CONTEUDO equals ol.ID_OBJETO_CONTEUDO
                where oc.ID_OBJETO_CONTEUDO == idObjetoConteudo
                select new
                {
                    uid = oc.ID_OBJETO_CONTEUDO,
                    name = oc.TCS_LAYOUT_LISTA.DESC_LAYOUT,
                    conteudo = oc.CONTEUDO_XML,
                    idUsuario = ol.ID_USUARIO,
                    nomeUsuario = ol.TCS_USUARIO.NOME_USUARIO,
                    isUserLayout = (ol.ID_USUARIO == userId)
                };

            var result = confs.FirstOrDefault();
            if (result == null) return configuracaoExportacao;

            var conf = JObject.Parse(result.conteudo);
            configuracaoExportacao = new ConfiguracaoExportacao
            {
                Id = result.uid,
                Name = result.name,
                Content = conf.Value<string>("Content"),
                IsUserLayout = result.isUserLayout,
                UserId = result.idUsuario,
                UserName = result.nomeUsuario
            };

            return configuracaoExportacao;
        }


        [Invoke(HasSideEffects = true)]
        public long SavePivotLayout(ConfiguracaoExportacao configuracaoExportacao, string jsonContent, Guid uidUsuario)
        {
            try
            {

                Usuario.UsuarioDomainService dsUsuario = new Usuario.UsuarioDomainService();
                Int64 idUsuario = dsUsuario.GetUserId(uidUsuario);

                if (configuracaoExportacao == null) throw new ArgumentNullException("configuracaoExportacao");

                var jsonContentParsed = JObject.Parse(jsonContent);
                var classeNome = String.Format("{0}.{1}.{2}.{3}",
                                                jsonContentParsed.Value<string>("RootNameSpace"),
                                                jsonContentParsed.Value<string>("ViewName"),
                                                jsonContentParsed.Value<string>("PivotName"),
                                                jsonContentParsed.Value<string>("PivotDataSource"));

                byte lxObjCnt = byte.Parse(Domains.TipoConteudoObjeto.SaveLayoutPivotTable.Value);
                byte lxObj = byte.Parse(Domains.TipoObjeto.BO.Value);
                byte lxLayout = byte.Parse(Domains.TipoLayout.UserLayout.Value);

                TCS_OBJETO objeto = null;
                TCS_OBJETO_CONTEUDO objetoConteudo = null;
                TCS_LAYOUT layout = null;
                TCS_OBJETO_PERMISSAO permissao = null;

                Int64 idObjeto = 0;

                objeto = this.DbContext.TCS_OBJETO.FirstOrDefault(o => o.CLASSE_NOME == classeNome && o.LX_TIPO_OBJETO == lxObj);

                if (objeto.IsNull())
                {
                    idObjeto = -1;

                    var obj = new TCS_OBJETO
                    {
                        DESC_OBJETO = "Obj Negócio(Report)-" + Guid.NewGuid().ToString(),
                        ID_OBJETO = idObjeto,
                        LX_TIPO_OBJETO = lxObj,
                        PATH_OBJETO = configuracaoExportacao.ParentFullTypeName,
                        CLASSE_NOME = classeNome
                    };

                    this.DbContext.TCS_OBJETO.Add(obj);

                    this.DbContext.SaveChanges();
                    idObjeto = obj.ID_OBJETO;
                }
                else
                    idObjeto = objeto.ID_OBJETO;

                if (!configuracaoExportacao.Id.IsNullOrEmpty())
                {
                    objetoConteudo = this.DbContext.TCS_OBJETO_CONTEUDO.FirstOrDefault(oc => oc.ID_OBJETO_CONTEUDO == configuracaoExportacao.Id && oc.LX_CONTEUDO_OBJETO == lxObjCnt);
                    layout = this.DbContext.TCS_LAYOUT.Include("TCS_LAYOUT_USUARIO_LISTA").FirstOrDefault(oc => oc.ID_OBJETO_CONTEUDO == configuracaoExportacao.Id);
                }
                if (objetoConteudo.IsNull())
                {
                    objetoConteudo = new TCS_OBJETO_CONTEUDO
                    {
                        ID_OBJETO_CONTEUDO = -1,
                        ID_OBJETO = idObjeto,
                        LX_CONTEUDO_OBJETO = lxObjCnt,
                        CONTEUDO_XML = jsonContent
                    };
                    this.DbContext.TCS_OBJETO_CONTEUDO.Add(objetoConteudo);
                }
                else
                {
                    if (objetoConteudo.ID_OBJETO != idObjeto)
                        objetoConteudo.ID_OBJETO = idObjeto;
                    objetoConteudo.CONTEUDO_XML = jsonContent;

                    this.DbContext.Entry(objetoConteudo).State = System.Data.Entity.EntityState.Modified;
                }

                if (layout.IsNull())
                {
                    layout = new TCS_LAYOUT
                    {
                        DESC_LAYOUT = jsonContentParsed.Value<string>("LayoutName"),
                        LAYOUT_PADRAO = false,
                        LX_TIPO_LAYOUT = lxLayout,
                        ULT_ATUALIZACAO = DateTime.Now,
                        TCS_OBJETO_CONTEUDO = objetoConteudo
                    };
                    if (layout.TCS_LAYOUT_USUARIO_LISTA.IsNull())
                        layout.TCS_LAYOUT_USUARIO_LISTA = new List<TCS_LAYOUT_USUARIO>();
                    layout.TCS_LAYOUT_USUARIO_LISTA.Add(new TCS_LAYOUT_USUARIO() { ID_USUARIO = idUsuario });
                    this.DbContext.TCS_LAYOUT.Add(layout);
                }
                else
                {
                    if (layout.DESC_LAYOUT != jsonContentParsed.Value<string>("LayoutName"))
                        layout.DESC_LAYOUT = jsonContentParsed.Value<string>("LayoutName");
                    layout.ULT_ATUALIZACAO = DateTime.Now;
                    var layoutUsuario = layout.TCS_LAYOUT_USUARIO_LISTA.FirstOrDefault();
                    if (layoutUsuario == null)
                    {
                        if (layout.TCS_LAYOUT_USUARIO_LISTA.IsNull())
                            layout.TCS_LAYOUT_USUARIO_LISTA = new List<TCS_LAYOUT_USUARIO>();
                        layout.TCS_LAYOUT_USUARIO_LISTA.Add(new TCS_LAYOUT_USUARIO() { ID_USUARIO = idUsuario });
                    }
                    else
                    {
                        layoutUsuario.ID_USUARIO = idUsuario;
                    }

                    this.DbContext.Entry(layout).State = System.Data.Entity.EntityState.Modified;
                }

                foreach (var item in this.DbContext.TCS_OBJETO_PERMISSAO.Where(x => x.ID_OBJETO_CONTEUDO == objetoConteudo.ID_OBJETO_CONTEUDO))
                {
                    this.DbContext.TCS_OBJETO_PERMISSAO.Remove(item);
                }

                this.DbContext.SaveChanges();

                if (!configuracaoExportacao.Profiles.IsNullOrEmpty() || !configuracaoExportacao.Users.IsNullOrEmpty())
                {

                    //Usuários
                    foreach (var item in configuracaoExportacao.Users.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        var obj = new TCS_OBJETO_PERMISSAO
                        {
                            ID_OBJETO = idObjeto,
                            ID_OBJETO_CONTEUDO = objetoConteudo.ID_OBJETO_CONTEUDO,
                            ID_USUARIO = Convert.ToInt64(item)
                        };
                        this.DbContext.TCS_OBJETO_PERMISSAO.Add(obj);
                    }

                    //Perfis
                    foreach (var item in configuracaoExportacao.Profiles.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        var obj = new TCS_OBJETO_PERMISSAO
                        {
                            ID_OBJETO = idObjeto,
                            ID_OBJETO_CONTEUDO = objetoConteudo.ID_OBJETO_CONTEUDO,
                            ID_PERFIL = Convert.ToInt64(item)
                        };
                        this.DbContext.TCS_OBJETO_PERMISSAO.Add(obj);
                    }

                    this.DbContext.SaveChanges();
                }

                if (DbContext.TCS_OBJETO_PERMISSAO.Where(i => i.ID_OBJETO_CONTEUDO == objetoConteudo.ID_OBJETO_CONTEUDO && i.ID_USUARIO == idUsuario).Count() == 0)
                {
                    this.DbContext.TCS_OBJETO_PERMISSAO.Add(new TCS_OBJETO_PERMISSAO
                    {
                        ID_OBJETO = idObjeto,
                        ID_OBJETO_CONTEUDO = objetoConteudo.ID_OBJETO_CONTEUDO,
                        ID_USUARIO = idUsuario
                    });

                    this.DbContext.SaveChanges();
                }
                return objetoConteudo.ID_OBJETO_CONTEUDO;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao salvar a configuração: " + e.ToString());
            }
        }



        [Invoke(HasSideEffects = true)]
        public IEnumerable<LayoutInfo> GetAllLayoutGenericos(string modulo, string nomeObjeto, long idUsuario)
        {
            string moduleId_objeto = modulo + "#" + nomeObjeto;
            long[] perfilList;

            byte lxObjCnt = byte.Parse(Domains.TipoConteudoObjeto.Layout.Value);
            byte lxObj = byte.Parse(Domains.TipoObjeto.BO.Value);

            using (Usuario.UsuarioDomainService dsUsuario = new Usuario.UsuarioDomainService())
            {
                perfilList = dsUsuario.GetTcsUsuarioPerfilNoAssociations().Where(i => !i.Inativo && i.IdUsuario == idUsuario).
                    Select(i => i.IdPerfil).ToArray();
            }

            var layouts =
                from oc in this.DbContext.TCS_OBJETO_CONTEUDO
                join o in this.DbContext.TCS_OBJETO on oc.ID_OBJETO equals o.ID_OBJETO
                //left join with permission
                join op in this.DbContext.TCS_OBJETO_PERMISSAO on new { o.ID_OBJETO, oc.ID_OBJETO_CONTEUDO } equals new { op.ID_OBJETO, op.ID_OBJETO_CONTEUDO }


                where o.LX_TIPO_OBJETO == lxObj &&
                o.PATH_OBJETO == moduleId_objeto &&
                oc.LX_CONTEUDO_OBJETO == lxObjCnt &&
                !oc.TCS_LAYOUT_LISTA.INATIVO &&
                //access control
                (op.ID_USUARIO == idUsuario || perfilList.Any(pl => pl == op.ID_PERFIL))


                select new LayoutInfo
                {
                    Id = oc.ID_OBJETO_CONTEUDO,
                    NomeLayout = oc.TCS_LAYOUT_LISTA.DESC_LAYOUT,
                    Modulo = o.PATH_OBJETO.Substring(0, o.PATH_OBJETO.IndexOf("#")),
                    NomeObjeto = o.PATH_OBJETO.Substring(o.PATH_OBJETO.IndexOf("#") + 1, o.PATH_OBJETO.Length),
                    LayoutPadrao = oc.TCS_LAYOUT_LISTA.LAYOUT_PADRAO
                };


            return layouts.Distinct();

        }

        [Invoke(HasSideEffects = true)]
        public LayoutInfo GetLayoutGenerico(long IdLayout)
        {
            var layout =
             (from oc in this.DbContext.TCS_OBJETO_CONTEUDO
              join o in this.DbContext.TCS_OBJETO on oc.ID_OBJETO equals o.ID_OBJETO
              where oc.ID_OBJETO_CONTEUDO == IdLayout
              select new LayoutInfo
              {
                  Id = oc.ID_OBJETO_CONTEUDO,
                  NomeLayout = oc.TCS_LAYOUT_LISTA.DESC_LAYOUT,
                  Modulo = o.PATH_OBJETO.Substring(0, o.PATH_OBJETO.IndexOf("#")),
                  NomeObjeto = o.PATH_OBJETO.Substring(o.PATH_OBJETO.IndexOf("#") + 1, o.PATH_OBJETO.Length),
                  ConteudoJson = oc.CONTEUDO_XML
              }).FirstOrDefault();

            var perm = this.DbContext.TCS_OBJETO_PERMISSAO.Where(p => p.ID_OBJETO_CONTEUDO == IdLayout).ToArray();

            if (perm != null & perm.Length > 0)
            {
                layout.PermissaoUsuario = string.Join(",", perm.Where(p => p.ID_USUARIO.HasValue).Select(p => p.ID_USUARIO).ToArray());
                layout.PermissaoPerfil = string.Join(",", perm.Where(p => p.ID_PERFIL.HasValue).Select(p => p.ID_PERFIL).ToArray());
            }

            return layout;
        }

        [Invoke(HasSideEffects = true)]
        public LayoutInfo SaveLayoutGenerico(LayoutInfo layoutInfo, long idUsuario)
        {
            if (layoutInfo == null) throw new ArgumentNullException("layoutInfo");
            if (layoutInfo.Modulo == null) throw new NullReferenceException("layoutInfo.Modulo");
            if (layoutInfo.NomeObjeto == null) throw new NullReferenceException("layoutInfo.NomeObjeto");
            if (layoutInfo.NomeLayout == null) throw new NullReferenceException("layoutInfo.NomeLayout");
            byte lxObjCnt = byte.Parse(Domains.TipoConteudoObjeto.Layout.Value);
            byte lxObj = byte.Parse(Domains.TipoObjeto.BO.Value);
            byte lxLayout = byte.Parse(Domains.TipoLayout.UserLayout.Value);
            bool isInsert = layoutInfo.Id == 0;

            TCS_OBJETO objeto = null;
            TCS_LAYOUT layout = null;

            if (isInsert && this.DbContext.TCS_LAYOUT.Any(l => l.DESC_LAYOUT == layoutInfo.NomeLayout && !l.INATIVO))
                throw new Exception(string.Format("Já existe um Layout com o mesmo nome. [{0}]", layoutInfo.NomeLayout));



            if (!isInsert)
            {
                layout = this.DbContext.TCS_LAYOUT.FirstOrDefault(l => l.ID_OBJETO_CONTEUDO == layoutInfo.Id);
                objeto = this.DbContext.TCS_OBJETO.FirstOrDefault(o => o.ID_OBJETO == layout.TCS_OBJETO_CONTEUDO.ID_OBJETO);
            }

            if (objeto == null) objeto = new TCS_OBJETO();
            objeto.DESC_OBJETO = "LayoutGrid-" + Guid.NewGuid().ToString();
            objeto.PATH_OBJETO = layoutInfo.Modulo + "#" + layoutInfo.NomeObjeto;
            objeto.LX_TIPO_OBJETO = lxObj;
            if (objeto.ID_OBJETO == 0)
            {
                this.DbContext.TCS_OBJETO.Add(objeto);
                this.DbContext.SaveChanges();
            }

            if (layout == null)
            {
                layout = new TCS_LAYOUT();
            }

            layout.UID_OBJETO_CONTEUDO = Guid.Empty;

            layout.LX_TIPO_LAYOUT = lxLayout;
            layout.LAYOUT_PADRAO = layoutInfo.LayoutPadrao;
            layout.DESC_LAYOUT = layoutInfo.NomeLayout;
            layout.ULT_ATUALIZACAO = DateTime.Now;
            if (layout.TCS_OBJETO_CONTEUDO == null) layout.TCS_OBJETO_CONTEUDO = new TCS_OBJETO_CONTEUDO();
            layout.TCS_OBJETO_CONTEUDO.CONTEUDO_XML = layoutInfo.ConteudoJson;
            layout.TCS_OBJETO_CONTEUDO.ID_OBJETO = objeto.ID_OBJETO;
            layout.TCS_OBJETO_CONTEUDO.LX_CONTEUDO_OBJETO = lxObjCnt;

            if (layout.ID_OBJETO_CONTEUDO == 0)
                this.DbContext.TCS_LAYOUT.Add(layout);

            this.DbContext.SaveChanges();
            layoutInfo.Id = layout.ID_OBJETO_CONTEUDO;


            if (!this.DbContext.TCS_LAYOUT_USUARIO.Any(lu => lu.ID_USUARIO == idUsuario && lu.ID_OBJETO_CONTEUDO == layoutInfo.Id))
            {
                this.DbContext.TCS_LAYOUT_USUARIO.Add(new TCS_LAYOUT_USUARIO() { ID_USUARIO = idUsuario, ID_OBJETO_CONTEUDO = layout.ID_OBJETO_CONTEUDO });
                this.DbContext.SaveChanges();
            }


            if (isInsert)
            {
                this.DbContext.TCS_OBJETO_PERMISSAO.Add(new TCS_OBJETO_PERMISSAO
                {
                    ID_OBJETO = objeto.ID_OBJETO,
                    ID_OBJETO_CONTEUDO = layoutInfo.Id,
                    ID_USUARIO = idUsuario
                });
            }

            if (!layoutInfo.PermissaoPerfil.IsNullOrEmpty() || !layoutInfo.PermissaoUsuario.IsNullOrEmpty())
            {
                foreach (var item in this.DbContext.TCS_OBJETO_PERMISSAO.Where(x => x.ID_OBJETO_CONTEUDO == layoutInfo.Id))
                {
                    this.DbContext.TCS_OBJETO_PERMISSAO.Remove(item);
                }
                if (!layoutInfo.PermissaoUsuario.IsNullOrEmpty())
                {
                    foreach (var item in layoutInfo.PermissaoUsuario.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Where(i => int.Parse(i) != idUsuario))
                    {
                        var obj = new TCS_OBJETO_PERMISSAO
                        {
                            ID_OBJETO = objeto.ID_OBJETO,
                            ID_OBJETO_CONTEUDO = layoutInfo.Id,
                            ID_USUARIO = long.Parse(item)
                        };
                        this.DbContext.TCS_OBJETO_PERMISSAO.Add(obj);
                    }
                }
                if (!layoutInfo.PermissaoPerfil.IsNullOrEmpty())
                {
                    foreach (var item in layoutInfo.PermissaoPerfil.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
                    {
                        var obj = new TCS_OBJETO_PERMISSAO
                        {
                            ID_OBJETO = objeto.ID_OBJETO,
                            ID_OBJETO_CONTEUDO = layoutInfo.Id,
                            ID_PERFIL = long.Parse(item)
                        };
                        this.DbContext.TCS_OBJETO_PERMISSAO.Add(obj);
                    }
                }
            }

            this.DbContext.SaveChanges();

            return layoutInfo;
        }

        [Invoke(HasSideEffects = true)]
        public void DeleteLayoutGenerico(long IdLayout, string modulo, string nomeObjeto, long idUsuario)
        {
            string moduleId_objeto = modulo + "#" + nomeObjeto;
            long[] perfilList;
            using (Usuario.UsuarioDomainService dsUsuario = new Usuario.UsuarioDomainService())
            {
                perfilList = dsUsuario.GetTcsUsuarioPerfilNoAssociations().Where(i => !i.Inativo && i.IdUsuario == idUsuario).
                    Select(i => i.IdPerfil).ToArray();
            }

            var query = (from l in this.DbContext.TCS_LAYOUT
                         join o in this.DbContext.TCS_OBJETO on l.TCS_OBJETO_CONTEUDO.ID_OBJETO equals o.ID_OBJETO

                         join op in this.DbContext.TCS_OBJETO_PERMISSAO on new { o.ID_OBJETO, l.ID_OBJETO_CONTEUDO } equals new { op.ID_OBJETO, op.ID_OBJETO_CONTEUDO } into gp
                         from perm in gp.DefaultIfEmpty()

                         where l.ID_OBJETO_CONTEUDO == IdLayout
                         && o.PATH_OBJETO == moduleId_objeto &&

                         (perm == null || perm.ID_USUARIO == idUsuario || perfilList.Any(pl => pl == perm.ID_PERFIL))

                         select l.ID_OBJETO_CONTEUDO);

            if (query.Any())
            {
                var layout = this.DbContext.TCS_LAYOUT.First(l => l.ID_OBJETO_CONTEUDO == IdLayout);
                layout.INATIVO = true;
                this.DbContext.Entry(layout).State = System.Data.Entity.EntityState.Modified;
                this.DbContext.SaveChanges();
            }

        }

        [Invoke(HasSideEffects = true)]
        public bool CanDeleteLayoutPivot(long idLayout, Guid uidUsuario)
        {
            Usuario.UsuarioDomainService dsUsuario = new Usuario.UsuarioDomainService();
            Int64 idUsuario = dsUsuario.GetUserId(uidUsuario);
            return DbContext.TCS_LAYOUT_USUARIO.Where(i => i.ID_OBJETO_CONTEUDO == idLayout && i.ID_USUARIO == idUsuario).Count() > 0;

            /*
            var perfil = DbContext.TCS_USUARIO_PERFIL.Where(i => i.ID_USUARIO == idUsuario).FirstOrDefault();
            long idPerfil = !perfil.IsNullOrEmpty() ? perfil.ID_PERFIL : -1;

            return DbContext.TCS_OBJETO_PERMISSAO.Any(o => o.ID_OBJETO_CONTEUDO == idLayout && (o.ID_PERFIL == idPerfil || o.ID_USUARIO == idUsuario));
            */
        }

        [Invoke(HasSideEffects = true)]
        public void DeleteLayoutPivot(long IdLayout)
        {
            var layoutUsuario = DbContext.TCS_LAYOUT_USUARIO.Where(x => x.ID_OBJETO_CONTEUDO == IdLayout).FirstOrDefault();
            DbContext.Entry(layoutUsuario).State = System.Data.Entity.EntityState.Deleted;

            var layout = DbContext.TCS_LAYOUT.Where(x => x.ID_OBJETO_CONTEUDO == IdLayout).FirstOrDefault();
            DbContext.Entry(layout).State = System.Data.Entity.EntityState.Deleted;

            var objetoConteudo = DbContext.TCS_OBJETO_CONTEUDO.Where(x => x.ID_OBJETO_CONTEUDO == IdLayout).FirstOrDefault();
            DbContext.Entry(objetoConteudo).State = System.Data.Entity.EntityState.Deleted;

            if (DbContext.TCS_OBJETO_PERMISSAO.Any(x => x.ID_OBJETO_CONTEUDO == IdLayout))
            {
                DbContext.TCS_OBJETO_PERMISSAO
                    .Where(x => x.ID_OBJETO_CONTEUDO == IdLayout)
                    .Foreach(u => DbContext.Entry(u).State = System.Data.Entity.EntityState.Deleted);
            }

            DbContext.SaveChanges();

        }

        [Invoke(HasSideEffects = true)]
        public LayoutInfo GetLayoutPadrao(string modulo, string nomeObjeto, long idUsuario)
        {
            string moduleId_objeto = modulo + "#" + nomeObjeto;
            long[] perfilList;

            byte lxObjCnt = byte.Parse(Domains.TipoConteudoObjeto.Layout.Value);
            byte lxObj = byte.Parse(Domains.TipoObjeto.BO.Value);

            using (Usuario.UsuarioDomainService dsUsuario = new Usuario.UsuarioDomainService())
            {
                perfilList = dsUsuario.GetTcsUsuarioPerfilNoAssociations().Where(i => !i.Inativo && i.IdUsuario == idUsuario).
                    Select(i => i.IdPerfil).ToArray();
            }

            var layout =
             (from oc in this.DbContext.TCS_OBJETO_CONTEUDO
              join o in this.DbContext.TCS_OBJETO on oc.ID_OBJETO equals o.ID_OBJETO
              join op in this.DbContext.TCS_OBJETO_PERMISSAO on new { o.ID_OBJETO, oc.ID_OBJETO_CONTEUDO } equals new { op.ID_OBJETO, op.ID_OBJETO_CONTEUDO }
              where o.LX_TIPO_OBJETO == lxObj &&
                o.PATH_OBJETO == moduleId_objeto &&
                oc.LX_CONTEUDO_OBJETO == lxObjCnt &&
                !oc.TCS_LAYOUT_LISTA.INATIVO &&
                oc.TCS_LAYOUT_LISTA.LAYOUT_PADRAO == true &&
                //access control
                (op.ID_USUARIO == idUsuario || perfilList.Any(pl => pl == op.ID_PERFIL))
              select new LayoutInfo
              {
                  Id = oc.ID_OBJETO_CONTEUDO,
                  NomeLayout = oc.TCS_LAYOUT_LISTA.DESC_LAYOUT,
                  Modulo = o.PATH_OBJETO.Substring(0, o.PATH_OBJETO.IndexOf("#")),
                  NomeObjeto = o.PATH_OBJETO.Substring(o.PATH_OBJETO.IndexOf("#") + 1, o.PATH_OBJETO.Length),
                  ConteudoJson = oc.CONTEUDO_XML,
                  LayoutPadrao = oc.TCS_LAYOUT_LISTA.LAYOUT_PADRAO
              }).OrderByDescending(p => p.Id).FirstOrDefault();

            if (!layout.IsNullOrEmpty())
            {
                var perm = this.DbContext.TCS_OBJETO_PERMISSAO.Where(p => p.ID_OBJETO_CONTEUDO == layout.Id).ToArray();

                if (perm != null & perm.Length > 0)
                {
                    layout.PermissaoUsuario = string.Join(",", perm.Where(p => p.ID_USUARIO.HasValue).Select(p => p.ID_USUARIO).ToArray());
                    layout.PermissaoPerfil = string.Join(",", perm.Where(p => p.ID_PERFIL.HasValue).Select(p => p.ID_PERFIL).ToArray());
                }
            }


            return layout;
        }
    }
}
