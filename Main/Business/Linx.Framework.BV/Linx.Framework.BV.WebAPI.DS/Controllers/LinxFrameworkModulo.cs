using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Linx.Tools;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Composition;
using System.Net;
using System.Net.Http;
using System.Web.Http;


using Linx.Framework.BV.Modulo;
using Linx.Framework.BV.Transacao;
using System.ServiceModel.DomainServices.Server;
using Linx.Framework.BV.Multimidia;

namespace Linx.Framework.BV.WebAPI.DS.Controllers
{

    ////////////////////////////////////////////////////////////////////////////
    /////////////////////////// Business Api Controller ////////////////////////
    ////////////////////////////////////////////////////////////////////////////
    public partial class LinxFrameworkModuloController
    {

        private string excelFolderUrl = null;

        private string GetExcelFolderUrl()
        {
            if (excelFolderUrl.IsNull())
            {
                try
                {
                    excelFolderUrl = LinxBusinessParameters.GetParameter<string>("URL_PASTA_ARQUIVOS_EXCEL", new Dictionary<string, string>());
                    if (!excelFolderUrl.ToLower().Contains("http://"))
                        excelFolderUrl = string.Format("{0}{1}", Utils.GetUrl(), excelFolderUrl.Left(1) == "/" ? excelFolderUrl.Substring(2) : excelFolderUrl);
                }
                catch
                {
                    excelFolderUrl = string.Empty;
                }
            }
            return excelFolderUrl;
        }

        private void AddFavoritos(List<AppModule> modules, int idTcsAplicativo, int idTcsAmbiente, Dictionary<string, string> headers)
        {
            if (LocalServiceBus.Enabled && BusinessUserServiceHelper.GetCurrentLoginMode(headers) == "POSUX")
                return;

            AppModule favorito = new AppModule()
            {
                Id = 0,
                DisplayName = "Favoritos",
                ShortDisplayName = "Favoritos",
                Image = "module" + Guid.Empty + ".png",
                ClassIcon = "",
                ClassBackground = "",
                ClassSize = "",
                Menus = new List<AppMenu>(),
                FriendlyUrl = "Favoritos",
                IdTcsAplicativo = idTcsAplicativo,
                IdTcsAmbiente = idTcsAmbiente
            };

            ModuloDomainService ds = new ModuloDomainService(headers);
            Int64 currentUser = BusinessUserServiceHelper.GetCurrentUserId().GetValueOrDefault();
            var favoritoList = (from result in ds.GetTcsUsuarioFavoritoNoAssociations().Where(i => i.IdUsuario == currentUser)
                                select result).OrderBy(i => i.OrdemNavegacao).ToList();

            foreach (var item in favoritoList)
            {
                AppModule module = modules.Where(i => i.Id == item.IdModulo).FirstOrDefault();

                if (!module.IsNull())
                {
                    AppMenu menu = new AppMenu();
                    menu.IdModule = item.IdModulo;

                    if (item.IdTransacao.IsNullOrEmpty() && item.IdModuloMenu.IsNullOrEmpty())
                    {
                        module.IsFavorite = true;
                        menu.Id = item.IdModulo;
                        menu.DisplayName = module.DisplayName;
                        menu.ShortDisplayName = module.ShortDisplayName;
                        menu.IsTransaction = false;
                        menu.Image = module.Image;
                        menu.ClassIcon = module.ClassIcon;
                        menu.ClassBackground = module.ClassBackground;
                        menu.ClassSize = module.ClassSize;
                        menu.ModuleDescription = module.DisplayName;
                        menu.FriendlyUrl = module.FriendlyUrl;
                        menu.Midia = null;
                        menu.IdTcsAplicativo = module.IdTcsAplicativo;
                        menu.IdTcsAmbiente = module.IdTcsAmbiente;
                        menu.MenusCount = module.MenusCount;
                    }
                    else
                    {
                        Int64 uidItem = item.IdTransacao.IsNullOrEmpty() ? item.IdModuloMenu.GetValueOrDefault() : item.IdTransacao.GetValueOrDefault();
                        AppMenu menuItem = GetAppMenu(uidItem, module);
                        if (!menuItem.IsNullOrEmpty())
                        {
                            menuItem.IsFavorite = true;
                            menu.Id = menuItem.Id;
                            menu.DisplayName = menuItem.DisplayName;
                            menu.ShortDisplayName = menuItem.ShortDisplayName;
                            menu.Module = menuItem.Module;
                            menu.IsTransaction = menuItem.IsTransaction;
                            menu.ClassIcon = menuItem.ClassIcon;
                            menu.ClassBackground = menuItem.ClassBackground;
                            menu.ClassSize = menuItem.ClassSize;
                            menu.ModuleDescription = menuItem.ModuleDescription;
                            menu.FriendlyUrl = menuItem.FriendlyUrl;
                            menu.Midia = null;
                            menu.IdTcsAplicativo = menuItem.IdTcsAplicativo;
                            menu.IdTcsAmbiente = menuItem.IdTcsAmbiente;
                            menu.MenusCount = menuItem.MenusCount;
                        }
                    }

                    if (!menu.DisplayName.IsNullOrEmpty())
                    {
                        menu.Order = item.OrdemNavegacao;
                        favorito.Menus.Add(menu);
                    }
                }
            }

            favorito.MenusCount = favorito.Menus.Count();
            modules.Insert(0, favorito);

        }

        private AppMenu GetAppMenu(Int64 uidItem, AppModule module)
        {
            AppMenu menu = null;

            Action<AppMenu> teste = null;

            teste = (appMenu) =>
            {
                if (appMenu.Id == uidItem)
                    menu = appMenu;

                if (menu.IsNull())
                    appMenu.Menus.ForEach(teste);

            };

            module.Menus.ForEach(teste);

            return menu;
        }

        //Utilizado pelo Shell até versão 5.1
        //private static DocMultimidiaInfo GetMidia(string nomeTabela, Int64 idChave, Dictionary<string, string> headers)
        //{
        //    MultimidiaDomainService ds = new MultimidiaDomainService(headers);
        //    return ds.GetMultimedia(nomeTabela, idChave, null, null, null, headers).FirstOrDefault();
        //}

        [Route("Modules"), System.Web.Http.HttpGet()]
        public IEnumerable<AppModule> Modules()
        {
            Guid? currentUser = BusinessUserServiceHelper.GetCurrentUserUid();
            int idTcsAmbiente = BusinessUserServiceHelper.GetCurrentEnvironmentId().GetValueOrDefault();

            return GetModules(currentUser, idTcsAmbiente, null);
        }

        private IEnumerable<AppModule> GetModules(Guid? userUid, int environmentId, Dictionary<string, string> headers)
        {
            List<AppModule> modulos = new List<AppModule>();

            if (LocalServiceBus.Enabled && BusinessUserServiceHelper.GetCurrentLoginMode(headers) == "POSUX")
            {
                ModuloLoja.ModuloLojaDomainService dsModuloLoja = new ModuloLoja.ModuloLojaDomainService();
                modulos = (from result in dsModuloLoja.GetLjvModuloNoAssociations().Where(i => !i.Inativo).ToList()
                           orderby result.OrdemNavegacao, result.DescModulo
                           select new AppModule()
                           {
                               Id = result.IdModulo,
                               DisplayName = result.DescModulo,
                               ShortDisplayName = result.NomeCurto,
                               Image = "module" + result.IdModulo.ToString() + ".png",
                               ClassIcon = result.Icone,
                               ClassBackground = GetClassBackground(result.LxCorFundo),
                               FriendlyUrl = Utils.ChangeSpecialCharacters(result.DescModulo),
                               IdTcsAplicativo = result.IdTcsAplicativo,
                               IdTcsAmbiente = environmentId
                           }).ToList();
            }
            else
            {
                Dictionary<string, string> variacaoGrupoModulo = new Dictionary<string, string>();
                variacaoGrupoModulo.Add("TCS_USUARIO", userUid.ToString());
                var parameterValue = LinxBusinessParameters.GetParameter<string>("GRUPO_MODULO", variacaoGrupoModulo, headers);

                if (parameterValue.IsNullOrEmpty())
                {
                    throw new Exception("Não foi encontrado valor para o parâmetro 'GRUPO_MODULO'.");
                }

                ModuloDomainService context = new ModuloDomainService(headers);
                Int64 idGrupoModulo = Convert.ToInt64(parameterValue);

                Int32 idAplicativoModulo = context.GetTcsModuloGrupoNoAssociations().Where(i => i.IdGrupoModulo == idGrupoModulo).Select(i => i.IdTcsAplicativo).FirstOrDefault();
                Int32 idTcsAplicativo = BusinessUserServiceHelper.GetCurrentApplicativeId(headers).GetValueOrDefault();

                if (idAplicativoModulo != idTcsAplicativo)
                {
                    throw new Exception("Aplicativo do Grupo de Módulo não é compatível com essa Aplicação.\nGrupo Módulo : '" + idGrupoModulo + "'.");
                }

                if (!userUid.IsNull())
                {
                    modulos = (from r in context.GetTcsModuloByUserAccess(userUid.Value, idGrupoModulo, headers)
                               select new AppModule
                               {
                                   Id = r.IdModulo,
                                   DisplayName = r.DescModulo,
                                   ShortDisplayName = r.NomeCurto,
                                   Image = "module" + r.IdModulo.ToString() + ".png",
                                   ClassIcon = r.Icone,
                                   ClassBackground = GetClassBackground(r.LxCorFundo),
                                   FriendlyUrl = Utils.ChangeSpecialCharacters(r.DescModulo),
                                   IdTcsAplicativo = r.IdTcsAplicativo,
                                   IdTcsAmbiente = environmentId
                               }).ToList();
                }
            }
            return modulos;
        }

        [Route("Menus"), System.Web.Http.HttpGet()]
        public IEnumerable<AppMenu> Menus(Int64 moduleId)
        {
            Guid? currentUser = Linx.Business.Tools.UserServiceHelper.GetCurrentUserUid();
            int idTcsAmbiente = BusinessUserServiceHelper.GetCurrentEnvironmentId().GetValueOrDefault();
            return GetMenus(moduleId, currentUser, idTcsAmbiente, null);
        }

        private IEnumerable<AppMenu> GetMenus(Int64 moduleId, Guid? userUid, int environmentId, Dictionary<string, string> headers = null)
        {
            List<AppMenu> menus = new List<AppMenu>();

            if (LocalServiceBus.Enabled && BusinessUserServiceHelper.GetCurrentLoginMode(headers) == "POSUX")
            {
                ModuloLoja.ModuloLojaDomainService dsModuloLoja = new ModuloLoja.ModuloLojaDomainService();
                var allMenus = (from result in dsModuloLoja.GetLjvModuloMenuNoAssociations().Where(i => i.IdModulo == moduleId)
                                orderby result.OrdemNavegacao, result.DescModuloMenu
                                select new TcsModuloMenu()
                                {
                                    DescModuloMenu = result.DescModuloMenu,
                                    DescModuloMenuSuperior = result.DescModuloMenuSuperior,
                                    OrdemNavegacao = result.OrdemNavegacao,
                                    IdModulo = result.IdModulo,
                                    IdModuloMenu = result.IdModuloMenu,
                                    IdModuloMenuSuperior = result.IdModuloMenuSuperior,
                                    Icone = result.Icone,
                                    LxCorFundo = result.LxCorFundo,
                                    DescModulo = result.DescModulo,
                                    NomeTabela = "LJV_MODULO",
                                    IdTcsAplicativo = result.IdTcsAplicativo,
                                    NomeCurto = result.NomeCurto
                                }).ToList();
                menus.AddRange(PopulateSubmenus(null, allMenus, userUid, environmentId, headers));
            }
            else
            {
                if (!userUid.IsNull())
                {
                    ModuloDomainService context = new ModuloDomainService(headers);
                    var allMenus = context.GetUserTcsModuloMenu(moduleId, headers).ToList();
                    menus.AddRange(PopulateSubmenus(null, allMenus, userUid, environmentId, headers));
                }
            }
            return menus;
        }

        [Route("FullModules"), System.Web.Http.HttpGet()]
        public IEnumerable<AppModule> FullModules(string cacheHash)
        {

            int idTcsAplicativo = BusinessUserServiceHelper.GetCurrentApplicativeId().GetValueOrDefault();
            int idTcsAmbiente = BusinessUserServiceHelper.GetCurrentEnvironmentId().GetValueOrDefault();

            List<AppModule> modules = new List<AppModule>();

            //UserUid + EnvironmentId
            string cacheKey = string.Format("UserModules_{0}_{1}", BusinessUserServiceHelper.GetCurrentEnvironmentId().GetValueOrDefault(), BusinessUserServiceHelper.GetCurrentUserUid().GetValueOrDefault());
            UserModules cache = WebCacheHelper.GetWebCache<UserModules>(cacheKey);

            if (cache.IsNull())
            {
                modules = this.Modules().ToList();

                foreach (AppModule module in modules)
                {
                    module.Menus = this.Menus(module.Id).ToList();
                }

                //remove módulos sem menu
                var modulesToRemove = modules.Where(i => i.Menus.Count() == 0).ToList();
                foreach (AppModule module in modulesToRemove)
                {
                    modules.Remove(module);
                }

                //adiciona favoritos
                this.AddFavoritos(modules, idTcsAplicativo, idTcsAmbiente, null);

                cache = new UserModules() { Hash = Guid.NewGuid().ToString(), Modules = modules };
                WebCacheHelper.UpdateWebCache(cacheKey, cache, 720); //Expiração em 30 dias
            }

            if (cacheHash.IsNullOrEmpty() || cacheHash != cache.Hash)
                modules = cache.Modules;
            else
                modules = null;

            if (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.Response != null)
                System.Web.HttpContext.Current.Response.AddHeader("cacheHash", cache.Hash);

            return modules;
        }

        private IEnumerable<AppMenu> PopulateSubmenus(Int64? menuSuperior, IEnumerable<TcsModuloMenu> allMenus, Guid? currentUser, int idTcsAmbiente, Dictionary<string, string> headers)
        {
            List<AppMenu> emptyMenus = new List<AppMenu>();
            List<AppMenu> menus = new List<AppMenu>();

            if (LocalServiceBus.Enabled && BusinessUserServiceHelper.GetCurrentLoginMode(headers) == "POSUX")
            {
                menus = (from r in allMenus
                         where r.IdModuloMenuSuperior == menuSuperior
                         select new AppMenu
                         {
                             Id = r.IdModuloMenu,
                             DisplayName = r.DescModuloMenu,
                             ShortDisplayName = r.NomeCurto,
                             Module = "",
                             IsTransaction = false,
                             ClassIcon = r.Icone,
                             ClassBackground = GetClassBackground(r.LxCorFundo),
                             Order = r.OrdemNavegacao,
                             Menus = new List<AppMenu>(),
                             IdModule = r.IdModulo,
                             ModuleDescription = r.DescModulo,
                             FriendlyUrl = Utils.ChangeSpecialCharacters(r.DescModuloMenu),
                             Midia = null,
                             IdTcsAplicativo = r.IdTcsAplicativo,
                             IdTcsAmbiente = idTcsAmbiente
                         }).OrderBy(i => i.Order).ThenBy(i => i.DisplayName).ToList();
            }
            else
            {

                menus = (from r in allMenus
                         orderby r.DescModuloMenu
                         where r.IdModuloMenuSuperior == menuSuperior
                         select new AppMenu
                         {
                             Id = r.IdModuloMenu,
                             DisplayName = r.DescModuloMenu,
                             ShortDisplayName = r.NomeCurto,
                             Module = "",
                             IsTransaction = false,
                             ClassIcon = r.Icone,
                             ClassBackground = GetClassBackground(r.LxCorFundo),
                             Order = r.OrdemNavegacao,
                             Menus = new List<AppMenu>(),
                             IdModule = r.IdModulo,
                             ModuleDescription = r.DescModulo,
                             FriendlyUrl = Utils.ChangeSpecialCharacters(r.DescModuloMenu),
                             IdTcsAplicativo = r.IdTcsAplicativo,
                             IdTcsAmbiente = idTcsAmbiente
                         }).OrderBy(i => i.Order).ThenBy(i => i.DisplayName).ToList();
            }


            foreach (var menu in menus)
            {
                var subMenus = PopulateSubmenus(menu.Id, allMenus, currentUser, idTcsAmbiente, headers);
                menu.Menus.AddRange(subMenus);

                var tcsTransacao = GetTransacoes(currentUser, menu, headers);

                if (subMenus.ToList().Count == 0 && tcsTransacao.Count == 0)
                    emptyMenus.Add(menu);
                else
                    menu.Menus.AddRange(tcsTransacao);

                menu.MenusCount = menu.Menus.Count();
            }

            //remove empty menus
            foreach (AppMenu menu in emptyMenus)
            {
                menus.Remove(menu);
            }

            return menus;
        }

        private List<AppMenu> GetTransacoes(Guid? currentUser, AppMenu menu, Dictionary<string, string> headers)
        {
            List<AppMenu> transacoes = new List<AppMenu>();

            if (LocalServiceBus.Enabled && BusinessUserServiceHelper.GetCurrentLoginMode(headers) == "POSUX")
            {
                ModuloLoja.ModuloLojaDomainService dsModuloLoja = new ModuloLoja.ModuloLojaDomainService();
                transacoes = (from r in dsModuloLoja.GetLjvTransacaoMenuNoAssociations().Where(i => i.IdModuloMenu == menu.Id && !i.Inativo && !i.InativoMenu).ToList()
                              select new AppMenu
                              {
                                  Id = r.IdTransacao,
                                  DisplayName = r.DescTransacao,
                                  ShortDisplayName = r.NomeCurto,
                                  Module = r.ClasseNome,
                                  IsTransaction = true,
                                  ClassIcon = r.Icone,
                                  ClassBackground = GetClassBackground(r.LxCorFundo),
                                  Order = r.OrdemNavegacao,
                                  Type = r.LxTipoTransacao,
                                  UrlRoute = GetUrlRoute(r.LxTipoTransacao, r.ClasseNome),
                                  Menus = new List<AppMenu>(),
                                  IdModule = menu.IdModule,
                                  ModuleDescription = menu.ModuleDescription,
                                  TransactionCode = r.CodTransacao,
                                  FriendlyUrl = Utils.ChangeSpecialCharacters(r.DescTransacao),
                                  Midia = null,
                                  IdTcsAplicativo = menu.IdTcsAplicativo,
                                  IdTcsAmbiente = menu.IdTcsAmbiente
                              }).OrderBy(i => i.Order).ThenBy(i => i.DisplayName).ToList();
            }
            else
            {
                TransacaoDomainService context = new TransacaoDomainService(headers);
                transacoes = (from r in context.GetTcsTransacaoByUserAccess(currentUser.Value, menu.Id, headers)
                              orderby r.DescTransacao
                              select new AppMenu
                              {
                                  Id = r.IdTransacao,
                                  DisplayName = r.DescTransacao,
                                  ShortDisplayName = r.NomeCurto,
                                  Module = r.ClasseNome,
                                  IsTransaction = true,
                                  ClassIcon = r.Icone,
                                  ClassBackground = GetClassBackground(r.LxCorFundo),
                                  Order = r.OrdemNavegacao,
                                  Type = r.LxTipoTransacao,
                                  UrlRoute = GetUrlRoute(r.LxTipoTransacao, r.ClasseNome),
                                  Menus = new List<AppMenu>(),
                                  IdModule = menu.IdModule,
                                  ModuleDescription = menu.ModuleDescription,
                                  TransactionCode = r.CodTransacao,
                                  FriendlyUrl = Utils.ChangeSpecialCharacters(r.DescTransacao),
                                  IdTcsAplicativo = menu.IdTcsAplicativo,
                                  IdTcsAmbiente = menu.IdTcsAmbiente,
                                  Tags = r.Tags
                              }).OrderBy(i => i.Order).ThenBy(i => i.DisplayName).ToList();
            }
            return transacoes;
        }

        private string GetClassBackground(int? lxCorFundo)
        {
            //Utilizado pelo Shell até versão 5.1
            //return lxCorFundo.IsNullOrEmpty() ? string.Empty : "bg-" + Domains.CorFundo.GetNames()[lxCorFundo.ToString()];
            return lxCorFundo.IsNullOrEmpty() || ! Domains.CorFundo.GetValues().ContainsKey(lxCorFundo.ToString()) ? string.Empty : Domains.CorFundo.GetNames()[lxCorFundo.ToString()];

        }

        //private string GetClassSize(int? lxTamanhoApresentacao)
        //{
        //    //Utilizado pelo Shell até versão 5.1
        //    return lxTamanhoApresentacao.IsNullOrEmpty() || lxTamanhoApresentacao == 1 ? string.Empty : Domains.TamanhoApresentacao.GetValues()[lxTamanhoApresentacao.ToString()].ToLower();
        //}

        private string GetUrlRoute(int? lxTipoTransacao, string classeNome)
        {
            string excelFolder = GetExcelFolderUrl();
            return lxTipoTransacao == 4 ? string.Format("{0}{1}{2}", excelFolder, excelFolder.Right(1) == "/" ? string.Empty : "/", classeNome) : string.Empty;
        }

        [Route("CleanUserModulesCache"), System.Web.Http.HttpGet()]
        public void CleanUserModulesCache()
        {
            this.repository.Context.CleanUserModulesCache();
        }

        [Route("SyncFavorites"), System.Web.Http.HttpPost()]
        public bool SyncFavorites(List<AppMenu> favorites)
        {
            return repository.Context.SyncFavorites(favorites);
        }

        [Route("AddUserFavorite"), System.Web.Http.HttpPost()]
        public bool AddUserFavorite(AppMenu favorite)
        {
            return repository.Context.AddUserFavorite(favorite);
        }

        [Route("DeleteUserFavorite"), System.Web.Http.HttpPost()]
        public bool DeleteUserFavorite(AppMenu favorite)
        {
            return repository.Context.DeleteUserFavorite(favorite);
        }

        [HttpPost()]
        [Route("FullModulesMultiEnvironment")]
        public IEnumerable<AppModule> FullModulesMultiEnvironment(EnvironmentInfo[] environments)
        {
            List<AppModule> fullModules = new List<AppModule>();
            string cacheHash = string.Empty;
            string ambientes = string.Empty;
            Guid? currentUser = BusinessUserServiceHelper.GetCurrentUserUid();
            Guid? economicGroup = BusinessUserServiceHelper.GetCurrentEconomicGroupId();

            foreach (EnvironmentInfo item in environments)
            {
                ambientes = ambientes + (ambientes.IsNullOrEmpty() ? string.Empty : "_") + item.EnvironmentId.ToString();
                cacheHash = item.Hash.ToString();
            }

            //UserUid + EnvironmentId
            string cacheKey = string.Format("UserModules_{0}_{1}", ambientes, currentUser.GetValueOrDefault());
            UserModules cache = WebCacheHelper.GetWebCache<UserModules>(cacheKey);

            if (cache.IsNull())
            {
                foreach (EnvironmentInfo item in environments)
                {
                    Dictionary<string, string> headers = new Dictionary<string, string>();
                    headers.Add("CurrentUser", currentUser.ToString());
                    headers.Add("EconomicGroup", economicGroup.ToString());
                    headers.Add("Environment", item.EnvironmentId.ToString());
                    headers.Add("CurrentCompany", item.CompanyUid.ToString());
                    headers.Add("Application", item.ApplicationUid.ToString());
                    headers.Add("LoginMode", BusinessUserServiceHelper.GetCurrentLoginMode());

                    List<AppModule> modules = GetModules(currentUser, item.EnvironmentId, headers).ToList();

                    foreach (AppModule module in modules)
                    {
                        module.Menus = GetMenus(module.Id, currentUser, item.EnvironmentId, headers).ToList();
                        module.MenusCount = module.Menus.Count();
                    }

                    //adiciona favoritos
                    this.AddFavoritos(modules, item.AplicativeId, item.EnvironmentId, headers);

                    fullModules.AddRange(modules);
                }

                //remove módulos sem menu
                var modulesToRemove = fullModules.Where(i => i.Id != 0 && i.Menus.Count() == 0).ToList();
                foreach (AppModule module in modulesToRemove)
                {
                    fullModules.Remove(module);
                }

                cache = new UserModules() { Hash = Guid.NewGuid().ToString(), Modules = fullModules };
                WebCacheHelper.UpdateWebCache(cacheKey, cache, 720); //Expiração em 30 dias
            }

            if (cacheHash.IsNullOrEmpty() || cacheHash != cache.Hash)
            {
                cacheHash = cache.Hash;
                fullModules = cache.Modules;
            }
            else
                fullModules = null;

            if (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.Response != null)
                System.Web.HttpContext.Current.Response.AddHeader("cacheHash", cacheHash);

            return fullModules;
        }

        /// <summary>
        /// Highest release version from LX_TCS.TCS_VERSAO.VERSAO (digits and dots only).
        /// </summary>
        [Route("GetHighestReleaseVersion"), System.Web.Http.HttpGet()]
        public string GetHighestReleaseVersion()
        {
            return repository.Context.GetHighestReleaseVersion();
        }
    }
}
