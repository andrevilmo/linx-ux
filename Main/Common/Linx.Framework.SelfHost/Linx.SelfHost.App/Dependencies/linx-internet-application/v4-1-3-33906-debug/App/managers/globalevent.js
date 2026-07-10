define(['durandal/system', 'durandal/app', 'services/logger', 'plugins/router', 'common'],
    function (system, app, logger, router, common) {

        return {
            ///////////////////////
            // method: activate()
            ///////////////////////
            register: function () {
                router.on('router:route:activating').then(function (instance, instruction, router) {
                    common.closeProcess();
                    $('body').removeClass("page-quick-sidebar-open");
                    $('.closeSidebarClick').hide();
                });

            }

        };
    });
