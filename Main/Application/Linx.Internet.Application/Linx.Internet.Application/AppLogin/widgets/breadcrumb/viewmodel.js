define(['services/logger', 'plugins/router', 'durandal/app', 'knockout', 'managers/__auth', 'common'],
    function (logger, router, app, ko, managerAuth, common) {
        //////////////////////
        // class: BreadCrumbItemVM
        //////////////////////
        var BreadCrumbItemVM = function (p) {
            var self = this;
            self.order = p.order;
            self.moduleKey = p.moduleKey;
            self.displayName = p.displayName;
            self.IsLast = p.IsLast;
            self.urlLink = p.urlLink;

            self.IsFirst = ko.computed(function () {
                return self.order == 0 ? true : false;
            });
        };

        //////////////////////
        // class: VM
        //////////////////////
        var VM = function () {
            var self = this;
            self.BREADCRUMB_VM = ko.observableArray();
            self.managerAuth = managerAuth;

            // Method: activate()
            this.activate = function () {
                if (common.getBarraNavegacao()) {
                    self.buildBreadCrumb();
                }
            };

            // Method: buildBreadCrumb()
            this.buildBreadCrumb = function () {
                if (managerAuth.isShellDevMode || managerAuth.isShellSetupMode) {
                    var breadVM = [];

                    if (router.activeInstruction() != null) {
                        var data = router.activeInstruction().config;

                        if (data.BreadCrumb != null) {
                            for (var y = 0; y < data.BreadCrumb.length; y++) {
                                var item = data.BreadCrumb[y];

                                breadVM.push(new BreadCrumbItemVM({
                                    order: item.order,
                                    moduleKey: item.moduleKey,
                                    displayName: item.displayName,
                                    IsLast: ((data.BreadCrumb.length - 1) <= y),
                                    urlLink: (y == 0 ? '#' : item.urlRoute)
                                }));
                            }
                        }
                    }

                    self.BREADCRUMB_VM(breadVM);
                }
                else {
                    if (router.activeInstruction().config.currentData == null) {
                        self.BREADCRUMB_VM.removeAll();
                        return;
                    }
                    var data = router.activeInstruction().config.currentData.BreadCrumb;

                    if (data != null) {
                        // copia os items para classe BreadCrumbItemVM
                        var breadVM = [];
                        for (var y = 0; y < data.length; y++) {
                            var item = data[y];

                            breadVM.push(new BreadCrumbItemVM({
                                order: item.order,
                                moduleKey: item.moduleKey,
                                displayName: item.displayName,
                                IsLast: ((data.length - 1) <= y),
                                urlLink: (y == 0 ? '#' : '#' + item.urlRoute)
                            }));
                        }

                        self.BREADCRUMB_VM(breadVM);
                    }


                    //var data = router.activeInstruction().config.currentData.BreadCrumb;

                    //if (data != null) {
                    //    // copia os items para classe MenuItemVM
                    //    for (var i = 0; i < data.length; i++) {
                    //        var record = data[i];

                    //        // copia os items para classe BreadCrumbItemVM
                    //        var breadVM = [];
                    //        for (var y = 0; y < record.BreadCrumb.length; y++) {
                    //            var item = record.BreadCrumb[y];

                    //            breadVM.push(new BreadCrumbItemVM({
                    //                order: item.order,
                    //                moduleKey: item.moduleKey,
                    //                displayName: item.displayName,
                    //                IsLast: ((record.BreadCrumb.length - 1) <= y),
                    //                urlLink: (y == 0 ? '#' : '#' + item.urlRoute)
                    //            }));
                    //        }

                    //        self.BREADCRUMB_VM(breadVM);
                    //    }
                    //}
                }
            };
           
        };

        return VM;
    });