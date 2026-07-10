define(['durandal/app', 'knockout', 'plugins/router', 'managers/window'],
    function (app, ko, router, managerWindow) {
        //////////////////////
        // class: VM
        //////////////////////
        var VM = function () {
            var self = this;
            self.managerWindow = managerWindow;

            // Method: activate()
            this.UICloseAll = function () {
                managerWindow.closeAll();
            };

        };

        return VM;
    });

