define(['durandal/app', 'durandal/system', 'knockout', 'plugins/router', 'common'],
    function (app, system, ko, router, common) {
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
                common.showModalReport("#link", true, globalDataParameters.parameters["REPORTING_SERVICES_URL"] + "reports");
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
