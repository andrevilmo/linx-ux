define(['durandal/app', 'durandal/system', 'knockout', 'plugins/router', 'managers/__auth', 'managers/user', 'common'],
    function (app, system, ko, router, managerAuth, managerUser, common) {
        //////////////////////
        // class: MenuItemVM
        //////////////////////
        var MenuItemVM = function (p) {
            var self = this;
            self.objRef = p.objRef,
            self.moduleKey = p.moduleKey;
            self.classType = p.classType;
            self.displayName = p.displayName;
            self.imagePath = p.imagePath;
            self.description = p.description;
            self.iconName = p.iconName;
            self.urlLink = p.urlLink;
            self.isTransaction = p.isTransaction;
            self.target = p.target;
            self.nameLink = p.nameLink;
            self.uidModule = p.uidModule;
            self.isFavorite = p.isFavorite;
            self.isModuleFavorite = p.isModuleFavorite;
            self.isModule = p.isModule;
            self.isHome = p.isHome;

        };

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

            //ko.computed(function () {
            //    alert(root.BREADCRUMB_VM.length)
            //    return false;
            //});
        };

        //////////////////////
        // class: VM
        //////////////////////
        var VM = function () {
            var self = this;
            self.MENUS_VM = ko.observableArray();
            self.BREADCRUMB_VM = ko.observableArray();
            self.managerAuth = managerAuth;
            self.managerUser = managerUser;

            //// events
            this.UI_linkModoInclusao_Click = function (urlLink) {
                router.navigate('#' + urlLink + "?action=new")
            },

            this.UI_linkFavAdd_Click = function (id, uidModule, isTransaction, objRef) {
                common.showProcess('#main');

                managerUser.UIAddItemFav(id, uidModule, isTransaction, objRef).then(function () {
                    self.buildVM();
                    common.closeProcess('#main');
                    $(".closeSidebarClick").hide();
                });
            },

            this.UI_linkFavDel_Click = function (id, uidModule, isTransaction, objRef) {
                common.showProcess('#main');

                managerUser.UIRemoveItemFav(id, uidModule, isTransaction, objRef).then(function () {
                    self.buildVM();
                    router.trigger('module:refresh');
                    common.closeProcess('#main');
                    $(".closeSidebarClick").hide();
                });
            },

            this.UI_linkHomeAdd_Click = function (objRef) {
                common.showProcess('#main');

                common.saveStartPage(objRef.UrlRoute).then(function () {
                    self.buildVM();
                    router.trigger('module:refresh');
                    common.closeProcess('#main');
                    $(".closeSidebarClick").hide();
                });
            },

            this.UI_linkHomeDel_Click = function (objRef) {
                common.showProcess('#main');

                common.saveStartPage('').then(function () {
                    router.trigger('module:refresh');
                    common.closeProcess('#main');
                    $(".closeSidebarClick").hide();
                });
            },

            // Função: Adicionar aos Favoritos/Adicionar Home
            this.UI_openListOptions = function (data, e) {
                // esconde a lista | Adicionar aos Favoritos/Adicionar Home
                $('.corner ul').css('display', 'none');

                // exibe tela escura
                $('.closeSidebarClick').toggle();

                // zera qualquer propriedade de estilo chumbada para class .tiles
                $('.tiles').removeAttr('style');

                // mostra lista de opções Adicionar/Adicionar Home
                var $el = $(e.target);
                $(e.target).parent().find('.corner ul').show();

                // aplica z-index no menus maior do que da tela escura
                var $tiles = $(e.target).parent().parent().parent();
                $('.col-md-12').find($tiles).css('z-index', '100002');


                // Ação ao clicar na tela escura
                $('.closeSidebarClick').click(function () {
                    // fecha tela escura
                    $(this).hide();

                    // fecha lista de opções Adicionar/Adicionar Home
                    var $tiles = $('.tiles');
                    $(e.target).parent().find('.corner ul').hide();

                    // zera qualquer propriedade de estilo chumbada para class .tiles
                    $('.tiles').removeAttr('style');
                });
            },

            // Method: buildVM()
            this.buildVM = function () {
                var data = router.activeInstruction().config.currentData.Menus;
                var IsModuleFavorite = (router.activeInstruction().config.currentData.Id == "00000000-0000-0000-0000-000000000000" ? true : false);

                // copia os items para classe MenuItemVM
                var menusVM = [];
                for (var i = 0; i < data.length; i++) {
                    var record = data[i];

                    var itemVM = new MenuItemVM({
                        objRef: record,
                        classType: '',
                        moduleKey: record.Id.toString(),
                        displayName: record.DisplayName,
                        imagePath: (isNullOrEmpty(record.Midia) ? '' : record.Midia.Url),
                        description: record.DisplayName,
                        iconName: '',
                        urlLink: '#' + record.UrlRoute,
                        isModuleFavorite: IsModuleFavorite,
                        isFavorite: (IsModuleFavorite ? true : record.IsFavorite),
                        isTransaction: record.IsTransaction,
                        target: "_self",
                        uidModule: record.UidModule,
                        isModule: record.lxIsModule,
                        isHome: (common.getStartPage() == record.UrlRoute)
                    });

                    if (record.IsTransaction == true) {
                        var validShellVersion = (record.lxShellCompiledVersion == managerAuth.shellVersion);

                        itemVM.nameLink = "link_transaction_" + record.Id.toString()
                        itemVM.classType = "tile bg-dark";

                        if (record.Type == 4) { // excel
                            itemVM.classType = "tile bg-green";
                            itemVM.iconName = "fa fa-file-excel-o";
                            itemVM.urlLink = record.UrlRoute;
                            itemVM.target = "_blank";
                        }
                        else {

                            if (record.lxRouteExists == true && validShellVersion == true) {
                                itemVM.iconName = (isNullOrEmpty(itemVM.imagePath) ? (record.ClassIcon.length > 0 ? record.ClassIcon : "fa fa-list-alt") : '');
                            }
                            else {
                                itemVM.iconName = 'fa fa-exclamation-triangle';

                                if (record.lxRouteExists == false)
                                    itemVM.displayName = "*404* " + itemVM.displayName;

                                else if (validShellVersion == false)
                                    itemVM.displayName = "* " + itemVM.displayName;

                            }

                        }
                    }
                    else {
                        if (record.lxIsModule) {
                            itemVM.imagePath = (isNullOrEmpty(record.Midia) ? 'lib/linx/img/modules/' + record.Image : record.Midia.Url)
                        }
                        else
                            {
                            itemVM.nameLink = "link_menu_" + record.Id.toString()
                            itemVM.classType = "tile bg-blue";
                            itemVM.iconName = (isNullOrEmpty(itemVM.imagePath) ? (record.UidModule == record.Id ? "fa fa-cube" : "fa fa-folder-o") : '');
                            record.ClassSize = '';
                        }
                    }

                    // Background
                    if (record.ClassBackground.length > 0) {
                        if (record.IsTransaction == true) {
                            itemVM.classType = 'tile ' + record.ClassBackground;
                        }
                        else {
                            itemVM.classType = 'tile ' + record.ClassBackground;
                        }
                    }

                    // Size
                    if (record.ClassSize.length > 0) {
                        itemVM.classType += ' ' + record.ClassSize;
                    }

                    menusVM.push(itemVM);

                    // copia os items para classe BreadCrumbItemVM
                    var breadVM = [];
                    for (var y = 0; y < record.BreadCrumb.length; y++) {
                        var item = record.BreadCrumb[y];

                        breadVM.push(new BreadCrumbItemVM({
                            order: item.order,
                            moduleKey: item.moduleKey,
                            displayName: item.displayName,
                            IsLast: ((record.BreadCrumb.length - 1) <= y),
                            urlLink: (y == 0 ? '#' : '#' + item.urlRoute)
                        }));
                    }

                    this.BREADCRUMB_VM(breadVM);
                }

                this.MENUS_VM(menusVM);
            };

            // Method: activate()
            this.activate = function () {
                this.buildVM();
            };

        };

        return VM;
    });
