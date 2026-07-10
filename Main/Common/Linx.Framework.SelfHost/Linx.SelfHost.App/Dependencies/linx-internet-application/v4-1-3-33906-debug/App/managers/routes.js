define(['durandal/system', 'durandal/app', 'plugins/router', 'managers/user', 'managers/__auth', 'managers/__route'],
    function (system, app, router, managerUser, managerAuth, managerRoute) {

        return {
            ///////////////////////
            // method: registerAll()
            ///////////////////////
            reloadRoutesFromServer: function () {
                requirejs.undef('managers/__route');
                require(['managers/__route'], function () { });
            },

            ///////////////////////
            // method: registerAll()
            ///////////////////////
            registerAll: function () {
                router.reset();

                // #rotes
                router.map({
                    route: "routes",
                    moduleId: 'viewmodels/routes',
                    title: "Routes",
                    nav: true,
                    type: 'system'
                });

                // #clear
                router.map({
                    route: "clear",
                    moduleId: 'viewmodels/tools',
                    title: "Tools:clean",
                    nav: true,
                    type: 'system-hidden'
                });

                // #404
                router.map({
                    route: "404",
                    moduleId: 'viewmodels/404',
                    title: "Linx Sistemas",
                    nav: true,
                    type: 'system-hidden'
                });

                if (!managerAuth.isShellSetupMode) {
                    // rotas dos relatorios
                    for (var i = 0; i < managerUser.REPORTS.length; i++) {
                        var item = managerUser.REPORTS[i];

                        router.map({
                            route: item.NomeRelatorio.toLowerCase(),
                            moduleId: 'viewmodels/reportviewer',
                            title: item.DescricaoRelatorio,
                            nav: true,
                            type: 'transaction-report',
                            currentData: item,
                            lxTransactionTitle: item.NomeRelatorio
                        });
                    }
                }

                // #reporting_services
                //router.map({
                //    route: "reporting_services",
                //    moduleId: 'viewmodels/report',
                //    title: "Reporting Services",
                //    nav: true,
                //    type: 'menu-report-modal',
                //    lxAssemblyName: "Reporting Services",
                //});


                // #reports (BreadCrumb)
                var arrBreadCrumb = [];
                arrBreadCrumb.push({
                    order: 0,
                    moduleKey: '',
                    displayName: managerAuth.isShellDevMode ? "Developer Mode" : "Setup Mode",
                    urlRoute: ''
                });

                if (!managerAuth.isShellSetupMode) {
                    // #reports
                    router.map({
                        route: "reports",
                        moduleId: 'viewmodels/menusdev',
                        title: "Relatórios",
                        nav: true,
                        type: 'menu-report',
                        lxAssemblyName: "Relatórios",
                        lxCount: managerUser.REPORTS.length,
                        BreadCrumb: arrBreadCrumb
                    });
                }

                if (managerAuth.isShellDevMode || managerAuth.isShellSetupMode) {
                    // #modulesdev
                    router.map({
                        route: ["modulesdev", ''],
                        moduleId: 'viewmodels/modulesdev',
                        title: managerAuth.isShellDevMode ? "Developer Mode" : "Setup Mode",
                        nav: true,
                        type: 'module',
                        lxExtractView: '',
                        lxDownloadModule: 'tools/downloadmodules'
                    });

                    // #reportviewer
                    router.map({
                        route: "reportviewer",
                        moduleId: 'viewmodels/reportviewer',
                        title: "Relatorios",
                        nav: true,
                        type: 'report',
                        //lxAssemblyName: "Relatorios",
                    });

                    // telas dos modulos
                    router.map(managerRoute.createTransactionRoutes());
                }
                else {
                    // #modulos
                    router.map({
                        route: ["modules", ''],
                        moduleId: 'viewmodels/modules',
                        title: "Módulos",
                        nav: true,
                        type: 'module',
                        lxExtractView: '',
                        lxDownloadModule: 'tools/downloadmodules',
                        lxHash: '#modules'
                    });

                    for (var i = 0; i < managerUser.MODULES_PLAIN.length; i++) {
                        var item = managerUser.MODULES_PLAIN[i];

                        // rotas dos menus
                        if (item.IsTransaction === undefined || item.IsTransaction == false) {
                            router.map({
                                route: item.UrlRoute,
                                moduleId: 'viewmodels/menus',
                                title: item.DisplayName,
                                nav: true,
                                type: 'menu',
                                currentData: item,
                                lxHash: '#' + item.UrlRoute
                            });
                        }
                        else {
                            if (item.Type == 4)
                                continue;

                            // rotas das transacoes
                            var routeAssembly = managerRoute.searchByModuleId(item.Module);

                            if (routeAssembly != null) {
                                router.map({
                                    //route: "transaction-" + item.Id.toString(),
                                    route: item.UrlRoute,
                                    moduleId: routeAssembly.moduleId,
                                    title: item.DisplayName,
                                    nav: true,
                                    type: 'transaction',
                                    currentData: item,
                                    lxShellCompiledVersion: routeAssembly.lxShellCompiledVersion,
                                    lxHash: '#' + routeAssembly.route
                                });
                            }
                        }
                    }

                    // varre todas as rotas registradas (modulos, menus e transacoes)
                    for (var r = 0; r < router.routes.length; r++) {
                        var currentRoute = router.routes[r];

                        if (currentRoute.currentData == null || currentRoute.currentData.Menus == null)
                            continue;

                        // varre todos os menus das rotas
                        for (var m = 0; m < currentRoute.currentData.Menus.length; m++) {
                            var currentMenu = currentRoute.currentData.Menus[m];

                            if (currentMenu.IsTransaction == true) {

                                // procura em todas as rotas o rota da TRANSACAO
                                for (var r1 = 0; r1 < router.routes.length; r1++) {

                                    // testa se a rota existe
                                    currentMenu.lxRouteExists = (router.routes[r1].hash == "#" + currentMenu.UrlRoute);

                                    // rota encontrada
                                    if (currentMenu.lxRouteExists) {
                                        currentMenu.lxShellCompiledVersion = router.routes[r1].lxShellCompiledVersion;
                                        break;
                                    }
                                }
                            }
                            else
                                currentMenu.lxRouteExists = true;
                        }

                    }
                }

                return true;
            }

        };
    });
