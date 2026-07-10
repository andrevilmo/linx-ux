using AttributeRouting;
using AttributeRouting.Web.Mvc;
using System.Web;
using System.Web.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Linx.Internet.Application.Common.Filters;
using Linx.Internet.Application.Framework.Web;
using RestSharp;
using System.Configuration;
using Linx.Internet.Application.Models;
using Linx.Internet.Application.Helpers;


namespace Linx.Internet.Application.Controllers
{
    public class ScriptsController : Controller
    {
        //[OutputCache(CacheProfile = "ProfileCacheScript")]
        //[GET("{moduleName}/{assemblyVersion}/scripts/requirejs/__config.js")]
        //public ActionResult RequireJSConfig()
        //{
        //    List<dynamic> packages = new List<dynamic>();
        //    var root = Linx.Internet.Application.Helpers.HtmlHelper.GetRoot();
        //    var moduleId = Linx.Internet.Application.Helpers.HtmlHelper.ModuleId();

        //    //if (root.Length > 0)
        //    //    root = root.Substring(1);

        //    //var index = root.LastIndexOf(moduleId);

        //    //if (index > -1)
        //    //    root = root.Substring(1, index);

        //    foreach (var module in PluginConfig.CurrentModules.OrderBy(o => o.Key))
        //    {
        //        if (module.Value.IsModuleShell)
        //        {
        //            continue;
        //        }

        //        var moduleItem = new
        //        {
        //            name = string.Concat("pkg_", module.Value.ModuleName),
        //            main = "main",
        //            location = string.Concat("../../../", module.Value.ModuleName, "/v", module.Value.AssemblyVersionURL, "-", module.Value.AssemblyType, "/App")
        //        };
        //        packages.Add(moduleItem);
        //    }

        //    ViewBag.packages = packages;

        //    return View();
        //}

        [OutputCache(CacheProfile = "ProfileCacheScript")]
        [GET("{moduleName}/{assemblyVersion}/app/managers/__route.js")]
        [GET("{moduleName}/{assemblyVersion}/applogin/managers/__route.js")]
        public ActionResult AppManagersRoute()
        {
            return View();
        }

        [NoCache]
        [GET("{moduleName}/{assemblyVersion}/app/managers/__auth.js")]
        [GET("{moduleName}/{assemblyVersion}/applogin/managers/__auth.js")]
        public ActionResult AppManagersAuth()
        {
            var _assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var _shellMode = BaseHelpers.GetShellMode();
            var _loginMode = BaseHelpers.GetLoginMode();
            //var _urlServiceBus = ConfigurationManager.AppSettings.GetValue("ServiceBus", "");
            //var _urlPortal = ConfigurationManager.AppSettings.GetValue("Portal", "");
            //var _urlImageServiceBus = ConfigurationManager.AppSettings.GetValue("ImageServiceBus", "");
            //var _configCheckVersion = ConfigurationManager.AppSettings.GetValue<bool>("Shell.CheckVersion.Enabled", false);
            //var _handleErrorJavascript = ConfigurationManager.AppSettings.GetValue<bool>("Shell.HandleErrorJavascript.Enabled", true);
            //var _flexMonsterLicenseKey = ConfigurationManager.AppSettings.GetValue("Shell.FlexMonsterLicenseKey", string.Empty);
            //var _customSearch = ConfigurationManager.AppSettings.GetValue<bool>("CustomSearch", false);
            //var _startUrl = ConfigurationManager.AppSettings.GetValue("Shell.StartUrl", "");

            LoginInfo loginInfo = this.Session["loginInfo"] as LoginInfo;

            var _loginUrl = (this.Session["loginUrl"] == null ? string.Empty : this.Session["loginUrl"].ToString());
            var _transaction = (this.Session["transaction"] == null ? string.Empty : this.Session["transaction"].ToString());
            var _expiracao = (this.Session["Expiracao"] == null ? false : (this.Session["Expiracao"].ToString().ToLower().Equals("true")));

            if (_shellMode == "DEV" || _shellMode == "SETUP")
            {
                if (loginInfo == null)
                    loginInfo = new LoginInfo();

                _expiracao = false;

                loginInfo.UidUsuario = Guid.Parse(ConfigurationManager.AppSettings.GetValue("DEV.UserId", string.Empty));
                loginInfo.NomeUsuario = ConfigurationManager.AppSettings.GetValue("DEV.AuthenticatedUser", "Sem nome");
                loginInfo.NomeCurtoUsuario = ConfigurationManager.AppSettings.GetValue("DEV.AuthenticatedUser", "Sem nome");
                loginInfo.UsuarioAutenticacao = ConfigurationManager.AppSettings.GetValue("DEV.AuthenticatedUser", "Sem nome");
                loginInfo.AutenticacaoWindows = false;
                loginInfo.DataExpiracaoSenha = DateTime.Now.AddYears(1);
                loginInfo.IdLinxGrupoEconomico = Convert.ToInt32(ConfigurationManager.AppSettings.GetValue("DEV.IdGpecon", string.Empty));
                loginInfo.UidGrupoEconomico = Guid.Parse(ConfigurationManager.AppSettings.GetValue("DEV.EconomicGroup", string.Empty));
                loginInfo.DescricaoGrupoEconomico = "DEV";
                loginInfo.IdTcsAmbienteDefault = Convert.ToInt32(ConfigurationManager.AppSettings.GetValue("DEV.EnvironmentId", string.Empty));
                loginInfo.CacheKey = loginInfo.IdTcsAmbienteDefault.ToString();
                loginInfo.GruposEconomicos = new List<GpeconInfo>() { new GpeconInfo { IdGpecon = loginInfo.IdLinxGrupoEconomico, Descricao = loginInfo.DescricaoGrupoEconomico } };

                loginInfo.Ambientes = new List<AmbienteInfo>() { new AmbienteInfo
                    {
                        IdTcsAmbiente = loginInfo.IdTcsAmbienteDefault,
                        UidEmpresa = Guid.Parse(ConfigurationManager.AppSettings.GetValue("DEV.CurrentCompany", string.Empty)),
                        DescricaoEmpresa = "DEV",
                        DescricaoAmbiente = "DEV",
                        UidAplicacao = Guid.Parse(ConfigurationManager.AppSettings.GetValue("DEV.ApplicationId", string.Empty)),
                        Token = Guid.Empty,
                        IdTcsAplicativo = Convert.ToInt32(ConfigurationManager.AppSettings.GetValue("DEV.IdTcsAplicativo", string.Empty)),
                        DescricaoAplicativo = ConfigurationManager.AppSettings.GetValue("DEV.DescricaoAplicativo", string.Empty),
                        UrlAplicativo = ConfigurationManager.AppSettings.GetValue("DEV.DescricaoAplicativo", string.Empty),
                        IndicaAdministrador = false,
                        IndicaMultiGpecon = false
                    }};

            }

            var formAccess = ConfigurationManager.AppSettings.GetValue("DEV.FormAccess", "{'AcessoTotal': true, 'Incluir': true, 'PesquisaEspecial': true, 'Excluir': true, 'Alterar': true, 'Layout': true, 'Imprimir': true, 'Pesquisar': true, 'Exportar': true}");
            FormAccess fAccess = new FormAccess() { AcessoTotal = true, Incluir = true, PesquisaEspecial = true, Excluir = true, Alterar = true, Layout = true, Imprimir = true, Pesquisar = true, Exportar = true };
            try
            {
                fAccess = Newtonsoft.Json.JsonConvert.DeserializeObject<FormAccess>(formAccess);
            }
            catch { }

            var model = new
            {
                sessionID = Session.SessionID,
                //serviceBus = _urlServiceBus,
                //imageServiceBus = _urlImageServiceBus,
                //portal = _urlPortal,
                //configCheckVersion = (_shellMode == "SETUP" ? false : _configCheckVersion),
                //handleErrorJavascript = _handleErrorJavascript,
                //flexMonsterLicenseKey = _flexMonsterLicenseKey,
                shellMode = _shellMode,
                isShellDevMode = (_shellMode == "DEV"),
                isShellProdMode = (_shellMode == "PROD"),
                isShellSetupMode = (_shellMode == "SETUP"),

                loginMode = _loginMode,
                isLoginPOSUXMode = (_loginMode == "POSUX"),
                isLoginPORTALUXMode = (_loginMode == "PORTALUX"),
                isLoginTRUSTEDMode = (_loginMode == "TRUSTED"),

                compilationMode = Linx.Internet.Application.Framework.Common.IsAssemblyDebugBuild(_assembly),
                isDebugMode = (Linx.Internet.Application.Framework.Common.IsAssemblyDebugBuild(_assembly) == "debug"),
                profilerEnabled = ConfigurationManager.AppSettings.GetValue<bool>("Shell.MiniProfiler.Enabled", false),
                isAuthenticated = (_shellMode == "DEV") || (_shellMode == "SETUP") || (User.Identity.IsAuthenticated && loginInfo != null),
                loginUrl = _loginUrl,
                transaction = _transaction,
                shellVersion = Linx.Internet.Application.Helpers.BaseHelpers.NumeroVersaoReduzida,
                expiracao = _expiracao,
                //customSearch = _customSearch,
                //startUrl = _startUrl

                idVendedor = -1,
                nomeVendedor = string.Empty,
                idLoja = -1,
                indicaGerente = false,
                indicaOperadorCaixa = false,
                idFilialPfj = -1,

                economicGroupId = (loginInfo == null ? Guid.Empty : loginInfo.UidGrupoEconomico),
                idGpecon = (loginInfo == null ? 0 : loginInfo.IdLinxGrupoEconomico),
                userId = (loginInfo == null ? Guid.Empty : loginInfo.UidUsuario),
                authenticatedUser = (loginInfo == null ? ConfigurationManager.AppSettings.GetValue("DEV.AuthenticatedUser", "Sem nome") : loginInfo.UsuarioAutenticacao),
                pathLanguageResource = ConfigurationManager.AppSettings.GetValue("PathLanguageResource", "")
            };
            // remove da sessao, para ser utilizado somente a primeira vez
            this.Session["transaction"] = string.Empty;

            //string jsonResponse = Newtonsoft.Json.JsonConvert.SerializeObject(model, Newtonsoft.Json.Formatting.Indented);
            ViewBag.Model = model;
            ViewBag.loginInfo = loginInfo;
            ViewBag.devFormAccess = fAccess;

            return View();
        }

        [OutputCache(CacheProfile = "ProfileCacheScript")]
        [GET("config.json")]
        public ActionResult AppConfig()
        {
            var clienteConfig = new
            {
                serviceBus = ConfigurationManager.AppSettings.GetValue("ServiceBus", ""),
                imageServiceBus = ConfigurationManager.AppSettings.GetValue("ImageServiceBus", ""),
                portal = ConfigurationManager.AppSettings.GetValue("Portal", ""),
                startUrl = ConfigurationManager.AppSettings.GetValue("Shell.StartUrl", ""),
                loginMode = ConfigurationManager.AppSettings.GetValue("Shell.LoginMode", "PORTALUX"),

                handleErrorJavascript = ConfigurationManager.AppSettings.GetValue<bool>("Shell.HandleErrorJavascript.Enabled", true),
                flexMonsterLicenseKey = ConfigurationManager.AppSettings.GetValue("Shell.FlexMonsterLicenseKey", string.Empty),
                customSearch = ConfigurationManager.AppSettings.GetValue<bool>("CustomSearch", false),
                profilerEnabled = false,
                configCheckVersion = false,
                messageCheckInterval = ConfigurationManager.AppSettings.GetValue<int>("Message.CheckInterval", 0),
                messageServiceEnable = ConfigurationManager.AppSettings.GetValue<bool>("Message.Service.Enabled", false)
            };

            return View(clienteConfig);
        }

        [OutputCache(CacheProfile = "ProfileCacheScript")]
        [GET("routes.json")]
        public ActionResult AppRoutes()
        {
            List<dynamic> routesAssembly = new List<dynamic>();
            List<dynamic> routesVersion = new List<dynamic>();

            foreach (var module in PluginConfig.CurrentModules.OrderBy(o => o.Key))
            {
                #region processamento: routesversion
                var moduleItem = new
                {
                    moduleUId = module.Value.ModuleUId.ToString(),
                    moduleId = string.Concat("pkg_", module.Value.ModuleName),
                    moduleName = module.Value.ModuleName,

                    assemblyName = module.Value.AssemblyName,
                    assemblyType = module.Value.AssemblyType,
                    assemblyVersion = module.Value.AssemblyVersion,
                    assemblyVersionFormated = string.Concat("v", module.Value.AssemblyVersion, "-", module.Value.AssemblyType).ToLower(),
                    requireId = string.Concat("v", module.Value.AssemblyVersion, "-", module.Value.AssemblyType).Replace(".", "-").ToLower(),
                    shellAssemblyVersion = module.Value.ShellAssemblyVersion,

                    buildDate = module.Value.BuildDate.ToString("dd/MM/yyyy HH:mm"),
                    CRC32 = module.Value.CRC32
                };
                routesVersion.Add(moduleItem);

                if (module.Value.IsModuleShell)
                {
                    continue;
                }
                #endregion

                var ShellCompiledVersion = new Version(module.Value.ShellAssemblyVersion);
                var ShellCompiledVersionString = string.Format("{0}.{1}.{2}", ShellCompiledVersion.Major, ShellCompiledVersion.Minor, ShellCompiledVersion.Build);

                #region processamento: ROTAS DAS TELAS
                var itens = PluginConfig.EmbeddedResources
                    .Where(w => w.Key.StartsWith("~." + module.Value.ModuleName, StringComparison.InvariantCultureIgnoreCase)
                        && w.Key.IndexOf(".app.viewmodels.", StringComparison.InvariantCultureIgnoreCase) > -1
                        && w.Key.IndexOf("custom", StringComparison.InvariantCultureIgnoreCase) == -1
                        && w.Key.IndexOf("Complement.js", StringComparison.InvariantCultureIgnoreCase) == -1)
                    .OrderBy(o => o.Key);

                itens = itens.Union(PluginConfig.EmbeddedResources
                    .Where(w => w.Key.StartsWith("~." + module.Value.ModuleName, StringComparison.InvariantCultureIgnoreCase)
                        && w.Key.IndexOf(".app.viewmodels.", StringComparison.InvariantCultureIgnoreCase) > -1
                        && w.Key.IndexOf("-custom-bv", StringComparison.InvariantCultureIgnoreCase) > -1
                        && w.Key.IndexOf("Complement.js", StringComparison.InvariantCultureIgnoreCase) == -1))
                    .OrderBy(o => o.Key);

                //parentBreadCrumb = new Array(new BreadCrumbItem({
                //    order: 0,
                //    moduleKey: '',
                //    displayName: 'Módulos',
                //    urlRoute: ''
                //}));


                #region processamento: ROTA DO MODULO
                var itemRouteMenu = new
                {
                    route = string.Concat(module.Value.ModuleName.ToLowerInvariant(), "(/:action)"),
                    moduleId = "viewmodels/menusdev",
                    title = string.Concat(module.Value.AssemblyName, " [v", module.Value.AssemblyVersion, " - ", module.Value.BuildDate.ToString("dd/MM/yyyy HH:mm"), "]"),
                    nav = true,
                    hash = string.Concat("#", module.Value.ModuleName.ToLowerInvariant()),
                    type = "menu-assembly",
                    lxAssemblyName = module.Value.AssemblyName,
                    lxModule = module.Value.ModuleName,
                    lxTransaction = "",
                    lxCount = itens.Count(),
                    lxTransactionTitle = "",
                    lxShellCompiledVersion = ShellCompiledVersionString,
                    lxExtractModule = string.Concat("tools/extractfiles?modulename=", module.Value.ModuleName),
                    lxExtractView = string.Concat("tools/extractfiles?modulename=", module.Value.ModuleName),
                    lxDownloadModule = string.Concat("tools/downloadmodules?modulename=", module.Value.ModuleName),
                    BreadCrumb = new List<BreadCrumbItem>()
                };

                itemRouteMenu.BreadCrumb.Add(new BreadCrumbItem()
                {
                    order = 0,
                    moduleKey = string.Empty,
                    displayName = BaseHelpers.GetShellMode() == "DEV" ? "Developer Mode" : "Setup Mode",
                    urlRoute = string.Empty
                });

                //itemRouteMenu.BreadCrumb.Add(new BreadCrumbItem()
                //{
                //    order = 1,
                //    moduleKey = string.Empty,
                //    displayName = itemRouteMenu.titleVersion,
                //    urlRoute = string.Empty + itemRouteMenu.route
                //});

                routesAssembly.Add(itemRouteMenu);
                #endregion

                foreach (var file in itens)
                {
                    //if (file.Value.FileName.ToLower().Contains("custom.js"))
                    //    continue;

                    var itemRouteTransaction = new
                    {
                        route = string.Concat(module.Value.ModuleName, "-", file.Value.FileNameFlat).ToLowerInvariant(),
                        moduleId = string.Concat("pkg_", module.Value.ModuleName, "/viewmodels/", file.Value.FileNameFlat),
                        title = file.Value.FileNameFlat,
                        titleVersion = file.Value.FileNameFlat,
                        nav = true,
                        type = "transaction-assembly",
                        lxAssemblyName = module.Value.AssemblyName,
                        lxModule = module.Value.ModuleName,
                        lxTransaction = file.Value.FileName,
                        lxCount = 0,
                        lxTransactionTitle = file.Value.FileNameFlat,
                        lxShellCompiledVersion = ShellCompiledVersionString,
                        lxExtractModule = string.Concat("tools/extractfiles?modulename=", module.Value.ModuleName),
                        lxExtractView = string.Concat("tools/extractfiles?modulename=", module.Value.ModuleName, "&viewname=", file.Value.FileNameFlat.ToLowerInvariant()),
                        lxDownloadModule = string.Concat("tools/downloadmodules?modulename=", module.Value.ModuleName),
                        BreadCrumb = new List<BreadCrumbItem>()
                    };

                    itemRouteTransaction.BreadCrumb.Add(itemRouteMenu.BreadCrumb[0]);
                    itemRouteTransaction.BreadCrumb.Add(new BreadCrumbItem()
                    {
                        order = 1,
                        moduleKey = string.Empty,
                        displayName = itemRouteMenu.title,
                        urlRoute = string.Empty + itemRouteMenu.hash
                    });

                    //itemRouteTransaction.BreadCrumb.Add(itemRouteMenu.BreadCrumb[1]);
                    //itemRouteTransaction.BreadCrumb.Add(new BreadCrumbItem()
                    //{
                    //    order = 2,
                    //    moduleKey = itemRouteTransaction.route,
                    //    displayName = itemRouteTransaction.title,
                    //    urlRoute = itemRouteTransaction.route
                    //});

                    routesAssembly.Add(itemRouteTransaction);
                }
                #endregion

            }

            List<dynamic> packages = new List<dynamic>();
            var root = Linx.Internet.Application.Helpers.HtmlHelper.GetRoot();
            var moduleId = Linx.Internet.Application.Helpers.HtmlHelper.ModuleId();

            //if (root.Length > 0)
            //    root = root.Substring(1);

            //var index = root.LastIndexOf(moduleId);

            //if (index > -1)
            //    root = root.Substring(1, index);

            foreach (var module in PluginConfig.CurrentModules.OrderBy(o => o.Key))
            {
                if (module.Value.IsModuleShell)
                {
                    continue;
                }

                var moduleItem = new
                {
                    name = string.Concat("pkg_", module.Value.ModuleName),
                    main = "main",
                    location = string.Concat("../../../", module.Value.ModuleName, "/v", module.Value.AssemblyVersionURL, "-", module.Value.AssemblyType, "/App")
                };
                packages.Add(moduleItem);
            }

            var routes = new
            {
                MODULES_ASSEMBLY = routesAssembly,
                MODULES_VERSION = routesVersion,
                MODULES_PKG = BuildModulesPkgs(),
                REQUIRE_PACKAGES = packages
            };

            return View(routes);
        }

        private List<dynamic> BuildModulesPkgs()
        {
            List<dynamic> packages = new List<dynamic>();

            foreach (var module in PluginConfig.CurrentModules.OrderBy(o => o.Key))
            {
                if (module.Value.IsModuleShell)
                {
                    continue;
                }

                var files = new List<string>();
                var ModuleFiles = PluginConfig.EmbeddedResources
                    .Where(w => w.Key.StartsWith("~." + module.Value.ModuleName, StringComparison.InvariantCultureIgnoreCase)
                        && (w.Key.IndexOf(".app.", StringComparison.InvariantCultureIgnoreCase) > -1)
                    )
                    .OrderBy(o => o.Key);

                foreach (var file in ModuleFiles)
                {
                    files.Add(file.Value.RequireId);
                }

                packages.Add(new
                {
                    moduleName = module.Value.ModuleName,
                    requireId = string.Concat("pkg_", module.Value.ModuleName),
                    files = files.ToArray()
                });
            }

            return packages;
        }
    }
}
