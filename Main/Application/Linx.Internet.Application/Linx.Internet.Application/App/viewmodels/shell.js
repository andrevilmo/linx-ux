define(['durandal/system', 'durandal/app', 'services/logger', 'plugins/router', 'managers/__auth', 'managers/window', 'managers/globalevent', 'common', 'managers/routes'],
    function (system, app, logger, router, managerAuth, managerWindow, managerGlobalEvent, common, managerRoutes) {
        return {
            router: router,
            activate: function () {
                system.log('Shell: activate');

                managerGlobalEvent.register();

                if (managerAuth.isShellProdMode === true && managerAuth.expiracao === true) {
                    return true;
                }

                if (router.routes.length > 0)
                    return router.activate();

                router.autoConvertRouteToModuleId = true;

                if (managerRoutes.registerAll()) {
                    managerWindow.registerManager();

                    //builds an observable model from the 
                    //mapping to bind your UI to
                    router.buildNavigationModel();

                    //sets up conventional mapping for 
                    //unrecognized routes
                    router.mapUnknownRoutes('viewmodels/404', '404');

                    $(".page-container").html('')

                    //activates the router
                    router.activate();
                };
            },

            compositionComplete: function () {
                common.setWindowMessage(true);
                App.init();

                if (managerAuth.configCheckVersion == true && managerAuth.expiracao !== true) {
                    require(['managers/hub'], function (managerHub) {
                        managerHub.init();
                    });
                }

                if (managerAuth.isShellProdMode == true && managerAuth.expiracao == true) {
                    require(['viewmodels/shared/modalChangePassword'], function (modalChangePassword) {
                        modalChangePassword.show(false);
                    });
                }
            },

            binding: function () {
                return { cacheViews: false };
            },

            showError: function (error) {
                alert(showError)
            },

        };
    });