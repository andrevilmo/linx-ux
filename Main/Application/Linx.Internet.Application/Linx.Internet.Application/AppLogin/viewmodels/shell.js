define(['durandal/system', 'durandal/app', 'services/logger', 'plugins/router', 'managers/__auth', 'common', 'managers/routes'],
    function (system, app, logger, router, managerAuth, common, managerRoutes) {
        return {
            router: router,
            activate: function () {
                system.log('Shell: activate');

                router.autoConvertRouteToModuleId = true;
                
                if (managerRoutes.registerAll()){
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
                //aqui
                //common.setWindowMessage(true);
                App.init();
            },

            binding: function () {
                return { cacheViews: false };
            },

            showError: function (error) {
                alert(showError)
            },

        };
    });