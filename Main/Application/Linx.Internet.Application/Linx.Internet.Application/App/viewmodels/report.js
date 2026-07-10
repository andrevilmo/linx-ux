define(['durandal/app', 'durandal/system', 'knockout', 'plugins/router', 'common', 'managers/__auth'],
    function (app, system, ko, router, common, managerAuth) {
        //////////////////////
        // class: VM
        //////////////////////
        var VM = function () {
            var self = this;

            // Method: activate()
            this.activate = function () {
            };

            this.binding = function () {
                return { cacheViews: false };
            };

            this.bindingComplete = function () {
            };

            this.attached = function () {
            };

            this.compositionComplete = function () {
                common.showModalReport("#link", true, managerAuth.getParameter("REPORTING_SERVICES_URL") + "reports");
                $("#link").click();
            };

            this.canDeactivate = function () {
                return true;
            };
            
            this.deactivate = function () {
            };

            this.detached = function () {
            };

        };

        return VM;
    });
