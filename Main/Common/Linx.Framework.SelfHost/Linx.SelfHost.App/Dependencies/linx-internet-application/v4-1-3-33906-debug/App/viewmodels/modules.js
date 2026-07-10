define(['services/logger', 'plugins/router', 'durandal/app', 'knockout', 'managers/user', 'managers/__auth', 'common'],
    function (logger, router, app, ko, managerUser, managerAuth, common) {
        var _modules = null;
        var _redirectHome = false;
        var event_fav_removed_registered = false;

        //////////////////////
        // class: ModuleItemVM
        //////////////////////
        var ModuleItemVM = function (p) {
            this.objRef = p.objRef,
            this.moduleKey = p.moduleKey,
            this.classType = p.classType,
            this.displayName = p.displayName,
            this.imagePath = p.imagePath,
            this.description = p.description,
            this.iconName = p.iconName,
            this.urlLink = p.urlLink,
            this.nameLink = p.nameLink,
            this.nameContainer = p.nameContainer,
            this.isFavorite = p.isFavorite,
            this.isModuleFavorite = p.isModuleFavorite,
            this.isHome = p.isHome,
            this.UI_openListOptions = p.UI_openListOptions
        }

        //////////////////////
        // class: VM
        //////////////////////
        var VM = function () {
            var self = this;
            self.MODULES_VM = ko.observableArray();
            self.managerUser = managerUser;
            self.managerAuth = managerAuth;


            //// events
            this.UI_linkFavAdd_Click = function (id, uidModule, isTransaction, objRef) {
                common.showProcess('#main');

                managerUser.UIAddItemFav(id, uidModule, isTransaction, objRef).then(function () {
                    _modules = null;
                    self.buildVM();
                    common.closeProcess('#main');
                    $(".closeSidebarClick").hide();
                });
            },

            this.UI_linkFavDel_Click = function (id, uidModule, isTransaction, objRef) {
                common.showProcess('#main');

                managerUser.UIRemoveItemFav(id, uidModule, isTransaction, objRef).then(function () {
                    _modules = null;
                    self.buildVM();
                    common.closeProcess('#main');
                    $(".closeSidebarClick").hide();
                });
            },

            this.UI_linkHomeAdd_Click = function (objRef) {
                common.saveStartPage(objRef.UrlRoute).then(function () {
                    _modules = null;
                    self.buildVM();
                    common.closeProcess('#main');
                    $(".closeSidebarClick").hide();
                });


            },

            this.UI_linkHomeDel_Click = function (objRef) {
                common.saveStartPage('').then(function () {
                    _modules = null;
                    self.buildVM();
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

                // aplica z-index no modulo maior do que da tela escura
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

            ///////////////////////
            // method: DURANDAL: activate()
            ///////////////////////
            this.canActivate = function () {
                // previne que o redirecionamento fique em loop
                if (_redirectHome == false) {
                    _redirectHome = true;
                    var url = common.getStartPage();

                    if (url.length > 0) {
                        url = '#' + url;

                        // verifica se a rota existe
                        for (var i = 0; i < router.routes.length; i++) {
                            if (router.routes[i].hash == url)
                            {
                                router.navigate(url);
                                return false;
                            }
                        }
                    }
                }

                return true;
            };

            // Method: activate()
            this.activate = function () {

                if (event_fav_removed_registered == false) {
                    event_fav_removed_registered = true;
                    router.on('module:refresh').then(function () {
                        _modules = null;
                    });
                }

                this.buildVM();
            };

            // Method: buildVM()
            this.buildVM = function () {
                if (_modules == null) {
                    _modules = [];
                    for (var i = 0; i < managerUser.MODULES.length; i++) {
                        var record = managerUser.MODULES[i];

                        var itemVM = new ModuleItemVM({
                            objRef: record,
                            classType: (record.ClassType == '' ? "tile bg-green" : record.ClassType),
                            moduleKey: record.Id.toString(),
                            displayName: record.DisplayName,
                            imagePath: (isNullOrEmpty(record.Midia) ? 'lib/linx/img/modules/' + record.Image : record.Midia.Url),
                            description: record.DisplayName,
                            iconName: '',
                            urlLink: '#' + record.UrlRoute,
                            nameLink: ("link_menu_" + record.Id.toString()),
                            nameContainer: ("container_menu_" + record.Id.toString()),
                            isFavorite: record.IsFavorite,
                            isModuleFavorite: record.lxIsModuleFavorite,
                            isHome: (common.getStartPage() == record.UrlRoute)
                        })

                        // Background
                        if (record.ClassBackground.length > 0) {
                            itemVM.classType = 'tile ' + record.ClassBackground;
                        }

                        // Size
                        if (record.ClassSize.length > 0) {
                            itemVM.classType += ' ' + record.ClassSize;
                        }

                        // iconName or image
                        if (record.ClassIcon.length > 0) {
                            itemVM.iconName = record.ClassIcon;
                        }

                        _modules.push(itemVM);
                    }
                }

                this.MODULES_VM(_modules);
            };
        };

        return VM;
    });

