define(['durandal/app', 'durandal/system', 'knockout', 'plugins/router', 'viewmodels/shared/modal', 'managers/__auth'],
    function (app, system, ko, router, modal, managerAuth) {
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
            self.lxShellCompiledVersion = p.lxShellCompiledVersion;
        };

        //////////////////////
        // class: VM
        //////////////////////
        var VM = function () {
            var self = this;
            self.MENUS_VM = ko.observableArray();

            // events
            this.UI_linkModoInclusao_Click = function (urlLink) {
                router.navigate('#' + urlLink + "?action=new")
            },

            // Method: activate()
            this.activate = function () {
                router.activeInstruction();
                //$(".page-content").block({ message: null });

                var data = router.activeInstruction().config

                var menusVM = [];
                for (var i = 0; i < router.routes.length; i++) {
                    var record = router.routes[i];

                    if ((record.lxModule != data.lxModule) || (record.type != "transaction-assembly" && record.type != "transaction-report"))
                        continue;

                    menusVM.push(new MenuItemVM({
                        classType: this.BuildClassType(record),
                        moduleKey: '',
                        displayName: record.lxTransactionTitle,
                        imagePath: '',
                        description: record.hash,
                        iconName: this.BuildIconName(record),
                        urlLink: record.hash,
                        isTransaction: true,
                        lxShellCompiledVersion: record.lxShellCompiledVersion
                    }));

                }

                this.MENUS_VM(menusVM);
            };

            this.binding = function () {
                return { cacheViews: false };
            };

            this.bindingComplete = function () {
            };

            this.attached = function () {
                //nosuchobject_2.fakemethod();       //intentionally cause major error
            };

            this.compositionComplete = function () {
                //nosuchobject_1.fakemethod();       //intentionally cause major error

                $(".page-container").show();
                App.fixContentHeight();
                App.initScroller();
            };

            this.canDeactivate = function () {
                //$("#applicationHost").block({ message: null });
                return true;
            };
            
            this.deactivate = function (view) {
            };

            this.detached = function (view) {
                $(view).empty();
                $(view).remove();

                //view = null;
                delete view;
            };

            this.BuildClassType = function (record) {

                if (record.type.indexOf("transaction-assembly") >= 0) {
                    var validShellVersion = (record.lxShellCompiledVersion == managerAuth.shellVersion);

                    if (validShellVersion == true)
                        return "tile bg-green";
                    else
                        return "tile bg-red";
                }

                else if (record.type.indexOf("transaction-report") >= 0)
                    return "tile double bg-green"

                else
                    return "tile bg-dark";
            }

            this.BuildIconName = function (record) {
                if (record.type.indexOf("transaction-assembly") >= 0)
                    return "fa fa-square-o";

                else if (record.type.indexOf("transaction-report") >= 0)
                    return "fa fa-file-o"

                else
                    return "fa fa-cogs";
            }

        };

        return VM;
    });
