define(['durandal/app', 'durandal/system', 'knockout', 'plugins/router'],
    function (app, system, ko, router) {
        //////////////////////
        // class: MenuItemVM
        //////////////////////
        var MenuItemVM = function (p) {
            var self = this;
            self.moduleKey = p.moduleKey;
            self.classType = p.classType;
            self.displayName = p.displayName;
            self.imagePath = p.imagePath;
            self.description = p.description;
            self.iconName = p.iconName;
            self.urlLink = p.urlLink;
            self.isTransaction = p.isTransaction;

        };

        //////////////////////
        // class: VM
        //////////////////////
        var VM = function () {
            var self = this;
            self.MENSAGEM = "Clean storage!";

            // Method: activate()
            this.activate = function () {
                $.localStorage.removeAll();
                $.sessionStorage.removeAll();
            };

            this.binding = function () {
                return { cacheViews: false };
            };

            this.bindingComplete = function () {
            };

            this.attached = function () {
            };

            this.compositionComplete = function () {
                //window.location.href = "#";
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
