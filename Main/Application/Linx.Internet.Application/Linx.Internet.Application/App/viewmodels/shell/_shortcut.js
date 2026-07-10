define(['services/logger', 'plugins/router', 'durandal/app', 'knockout', 'managers/user', 'managers/__auth'],
    function (logger, router, app, ko, managerUser, managerAuth) {
        var _modules = null;

        //////////////////////
        // class: ShortCutItemVM
        //////////////////////
        var ShortCutItemVM = function (p) {
            this.moduleKey = p.moduleKey,
            this.displayName = p.displayName,
            this.urlLink = p.urlLink
            this.level = p.level
        }

        //////////////////////
        // class: VM
        //////////////////////
        var VM = function () {
            var self = this;
            self.MODULES_VM = ko.observableArray();

            // Method: activate()
            self.loadModules = function () {
                if (_modules == null) {
                    _modules = [];

                    _modules.push(new ShortCutItemVM({
                        moduleKey: '-2',
                        displayName: '',
                        urlLink: '',
                        level: 0
                    }));

                    _modules.push(new ShortCutItemVM({
                        moduleKey: '-1',
                        displayName: '::: Módulos :::',
                        urlLink: '#',
                        level: 0
                    }));

                    if (managerAuth.isShellDevMode || managerAuth.isShellSetupMode) {
                        // varre a colecao de rotas
                        for (var i = 0; i < router.routes.length; i++) {
                            var routeItem = router.routes[i];

                            var vmItem = new ShortCutItemVM({
                                moduleKey: 0,
                                displayName: routeItem.title,
                                urlLink: routeItem.hash,
                                level: 0
                            });

                            if (routeItem.type != "menu-assembly" && routeItem.type != "transaction-assembly")
                                continue;

                            if (routeItem.type == "transaction-assembly")
                                // Tela
                                vmItem.displayName = Array(2).join("&nbsp;&nbsp;&nbsp;&nbsp;") + routeItem.lxTransactionTitle.toString().trim();
                            else
                                // Modulo
                                vmItem.displayName = "&lt;b&gt;" + routeItem.title + "&lt;/b&gt;";

                            _modules.push(vmItem);
                        }
                    }
                    else {
                        // varre a colecao de modulos
                        for (var i = 0; i < managerUser.MODULES_PLAIN.length; i++) {
                            var moduleItem = managerUser.MODULES_PLAIN[i];
                            self.buildVM(moduleItem)

                            if (moduleItem.lxIsModuleFavorite) {
                                for (var y = 0; y < moduleItem.Menus.length; y++) {
                                    self.buildVM(moduleItem.Menus[y])
                                }
                            }
                        }
                    }

                }
                self.MODULES_VM(_modules);
            }

            this.activate = function () {
                self.loadModules();
                $(managerUser).on('moduleChanged', function () {
                    _modules = null;
                    self.loadModules();
                });
            };

            this.buildVM = function (moduleItem) {
                var vmItem = new ShortCutItemVM({
                    moduleKey: moduleItem.Id.toString(),
                    displayName: '',
                    urlLink: '#' + moduleItem.UrlRoute,
                    level: moduleItem.lxLevel
                });


                if (moduleItem.lxLevel == 1)
                    // Modulo
                    vmItem.displayName = "&lt;b&gt;" + moduleItem.DisplayName.trim() + "&lt;/b&gt;";
                else if (moduleItem.IsTransaction == false)
                    // Menu
                    vmItem.displayName = Array(moduleItem.lxLevel).join("&nbsp;&nbsp;&nbsp;&nbsp;") + "[" + moduleItem.DisplayName.trim() + "]"
                else
                    // Tela
                    vmItem.displayName = Array(moduleItem.lxLevel).join("&nbsp;&nbsp;&nbsp;&nbsp;") + moduleItem.DisplayName.trim();

                _modules.push(vmItem);
            };

            this.compositionComplete = function () {
                //App.init();
                App.inithandleHorizontalMenu();
                //App.inithandleSidebarToggler();

                //FormComponents.init();
                //FormComponents.initSelect2();
                //FormComponents.initSelect2Modal();

                var sel = $("select[id='select2_shortcut']");
                var header = $('.header');

                $(sel).select2({
                    placeholder: "Módulos / Telas",
                    openOnEnter: true,
                    width: "off",
                    escapeMarkup: function (m) {
                        return m;
                    }
                });

                $(sel).select2("val", "");

                $(sel).on("select2-selecting", function(e) {
                    router.navigate(e.val);
                });

                $(sel).on("select2-selected", function(e) {
                    $(sel).select2("val", "");
                    $(sel).select2("close");
                    $('#span_shortcut').click();

                    $('body').removeClass("page-quick-sidebar-open");
                    $('.closeSidebarClick').hide();
                });

                $(sel).on("select2-close", function() {
                    $('.header .hor-menu .hor-menu-search-form-toggler2').removeClass('off');
                    $('.header .hor-menu .search-form2').hide();

                    //$('a[href=#quick_sidebar_tab_2]').on('click', function(){ $('#select2_shortcut').select2("close"); });
                });

                $("#span_shortcut").on('click', function (e) {
                    $(".search-form2").show();
                    $(sel).select2("open");
                });
            };

        };

        return VM;
    });

