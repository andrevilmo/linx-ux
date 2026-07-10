define(['durandal/system', 'durandal/app', 'plugins/router', 'managers/__auth', 'managers/__route'],
    function (system, app, router, managerAuth, managerRoute) {

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

                // #reportviewer
                router.map({
                    route: ["loginPOS", ''],
                    moduleId: 'viewmodels/loginPOS',
                    title: "Login",
                    nav: true,
                    type: 'transaction',
                    //lxAssemblyName: "Relatorios",
                });


                //// #reports (BreadCrumb)
                //var arrBreadCrumb = [];
                //arrBreadCrumb.push({
                //    order: 0,
                //    moduleKey: '',
                //    displayName: managerAuth.isShellDevMode ? "Developer Mode" : "Setup Mode",
                //    urlRoute: ''
                //});

                //if (managerAuth.isShellDevMode || managerAuth.isShellSetupMode) {
                //    // #modulesdev
                //    router.map({
                //        route: "modulesdev", ''],
                //        moduleId: 'viewmodels/modulesdev',
                //        title: managerAuth.isShellDevMode ? "Developer Mode" : "Setup Mode",
                //        nav: true,
                //        type: 'module',
                //        lxExtractView: '',
                //        lxDownloadModule: 'tools/downloadmodules'
                //    });

                //}

                return true;
            }

        };
    });
