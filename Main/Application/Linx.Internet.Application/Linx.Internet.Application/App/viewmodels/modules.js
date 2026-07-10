define(['services/logger', 'plugins/widget', 'plugins/router', 'durandal/app', 'knockout', 'managers/user', 'managers/__auth', 'managers/brand', 'common', 'viewmodels/shell/_menu', 'viewmodels/shell/_header'],
    function (logger, widget, router, app, ko, managerUser, managerAuth, managerBrand, common, _menu, _header) {
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
                this.shortName = p.shortName,
                this.imagePath = p.imagePath,
                this.description = p.description,
                this.iconName = p.iconName,
                this.urlLink = p.urlLink,
                this.nameLink = p.nameLink,
                this.nameContainer = p.nameContainer,
                this.isFavorite = p.isFavorite,
                this.isModuleFavorite = p.isModuleFavorite,
                this.isHome = p.isHome,
                this.UI_openListOptions = p.UI_openListOptions,
                this.card_click = p.card_click
        }

        //////////////////////
        // class: VM
        //////////////////////
        var VM = {

            _header: _header,

            MODULES_VM: ko.observableArray(),

            _menu: _menu,

            managerUser: managerUser,
            managerAuth: managerAuth,

            Swiper: ko.observable(null),

            WizardSwiper: ko.observable(null),

            currentDashboards: ko.observableArray(),
            currentWizards: ko.observableArray(),

            getDashboardsByEnviroment: function (ambiente) {
                var self = this;
                var dashs = $.grep(_menu.dashboardTransactions(), function (element, index) { return element.ambiente === parseInt(ambiente) });
                setTimeout(function () {
                    self.currentDashboards(dashs);
                    $('#dashboards .dash-body').slimScroll({ height: 400, alwaysVisible: true });
                }, 200);
            },

            getWizardsByEnviroment: function (ambiente) {
                var self = this;
                var wizards = $.grep(_menu.wizardTransactions(), function (element, index) { return element.ambiente === parseInt(ambiente) });
                setTimeout(function () {
                    self.currentWizards(wizards);
                    if (self.WizardSwiper() && self.WizardSwiper().update) {
                        self.WizardSwiper().update(true);
                        $('.wizardSwiperContainer .post-body').slimScroll({ height: 'calc(100% - 30px)' });
                    }
                }, 200);
            },

            setActiveEnviroment: function (ambiente) {
                router.activeInstruction().config.currentData = {
                    IdTcsAmbiente: ambiente
                };

                if (_header.activeMode() == 0) {
                    this.getWizardsByEnviroment(ambiente);
                }
                else if (_header.activeMode() == 1) {
                    this.getDashboardsByEnviroment(ambiente);
                }
            },

            UI_linkFavAdd_Click: function (id, uidModule, isTransaction, objRef) {
                common.showProcess('#main');
                managerUser.UIAddItemFav(id, uidModule, isTransaction, objRef).then(function () {
                    _modules = null;
                    this.buildVM();
                    common.closeProcess('#main');
                    $(".closeSidebarClick").hide();
                });
            },

            UI_linkFavDel_Click: function (id, uidModule, isTransaction, objRef) {
                common.showProcess('#main');

                managerUser.UIRemoveItemFav(id, uidModule, isTransaction, objRef).then(function () {
                    _modules = null;
                    this.buildVM();
                    common.closeProcess('#main');
                    $(".closeSidebarClick").hide();
                });
            },
            UI_linkHomeAdd_Click: function (objRef) {
                common.saveStartPage(objRef.UrlRoute).then(function () {
                    _modules = null;
                    this.buildVM();
                    common.closeProcess('#main');
                    $(".closeSidebarClick").hide();
                });


            },

            UI_linkHomeDel_Click: function (objRef) {
                common.saveStartPage('').then(function () {
                    _modules = null;
                    this.buildVM();
                    common.closeProcess('#main');
                    $(".closeSidebarClick").hide();
                });
            },

            // Função: Adicionar aos Favoritos/Adicionar Home
            UI_openListOptions: function (data, e) {
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
            canActivate: function () {

                if ((window.location.hash === '' || window.location.hash === '#modules') && managerAuth.loginInfo && !isNullOrEmpty(managerAuth.loginInfo.UrlWorkArea)) {
                    router.navigate("#embeddedApp?url=" + managerAuth.loginInfo.UrlWorkArea);
                    return true;
                }


                // previne que o redirecionamento fique em loop
                if (_redirectHome == false) {
                    _redirectHome = true;
                    var url = common.getStartPage();

                    if (url.length > 0) {
                        url = '#' + url;

                        // verifica se a rota existe
                        for (var i = 0; i < router.routes.length; i++) {
                            if (router.routes[i].hash == url) {
                                router.navigate(url);
                                return false;
                            }
                        }
                    }
                }

                return true;
            },

            // Method: activate()
            activate: function () {

                if (event_fav_removed_registered == false) {
                    event_fav_removed_registered = true;
                    router.on('module:refresh').then(function () {
                        _modules = null;
                    });
                }

                if (managerAuth.loginInfo.Ambientes.length > 0)
                    this.setActiveEnviroment(managerAuth.loginInfo.Ambientes[0].IdTcsAmbiente);

                this.buildVM();


            },

            // Method: buildVM()
            buildVM: function () {
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
                            shortName: record.ShortDisplayName,
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

                        if (record.lxIsModuleFavorite) {
                            itemVM.iconName = 'star';
                        }
                        _modules.push(itemVM);
                    }
                }

                this.MODULES_VM(_modules);
            },

            card_click: function (url) {
                router.navigate(url);
            },

            getVMByEnvironment: function (idEnvironment) {
                var vm = $.grep(this.MODULES_VM(), function (element, index) { return element.objRef.IdTcsAmbiente == idEnvironment });
                return vm;
            },

            compositionComplete: function () {

                var self = this;

                $("#content article:nth-child(1)").show();

                $(".aba-item:first").addClass("aba--selected");

                self.WizardSwiper(new Swiper('.wizardSwiperContainer', {
                    pagination: '.swiper-pagination',
                    paginationClickable: true,
                    spaceBetween: 30,
                    //effect: 'coverflow',
                    //grabCursor: true,
                    //centeredSlides: true,
                    //slidesPerView: 1,
                    //coverflow: {
                    //    rotate: 50,
                    //    stretch: 0,
                    //    depth: 100,
                    //    modifier: 1,
                    //    slideShadows: false
                    //}
                }));

                $(window).resize(function () {
                    if (self.WizardSwiper() && self.WizardSwiper().update) {
                        setTimeout(function () {
                            self.WizardSwiper().update(true);
                        }, 100);
                    }
                });

                $(".aba-item").click(function () {
                    var ambienteId = $(this).data('id');
                    _menu.changeEnviroment(ambienteId);
                    self.setActiveEnviroment(ambienteId);

                    $(".aba-item").removeClass("aba--selected");
                    $(this).addClass("aba--selected");

                    var indice = $(this).index();
                    indice++;

                    $("#content article").hide();
                    $("#content article:nth-child(" + indice + ")").show();

                    $("#wizard article").hide();
                    $("#wizard article:nth-child(" + indice + ")").show();

                    if (self.WizardSwiper() && self.WizardSwiper().update)
                        self.WizardSwiper().update(true);
                });

                if (_menu.currentEnviroment) {
                    var aba = $(".aba-item[data-id='" + _menu.currentEnviroment + "']");

                    self.setActiveEnviroment(_menu.currentEnviroment);

                    $(".aba-item").removeClass("aba--selected");
                    $(aba).addClass("aba--selected");

                    var indice = $(aba).index();
                    indice++;

                    $("#content article").hide();
                    $("#content article:nth-child(" + indice + ")").show();

                    $("#wizard article").hide();
                    $("#wizard article:nth-child(" + indice + ")").show();

                    if (self.WizardSwiper() && self.WizardSwiper().update)
                        self.WizardSwiper().update(true);
                }

                this.Swiper(new Swiper('.mainSwiperContainer', { onlyExternal: true, initialSlide: _header.activeMode() }));

                _header.activeMode.subscribe(function (newValue) {
                    if (newValue !== null || newValue !== undefined) {
                        self.Swiper().slideTo(newValue);

                        var ambiente = router.activeInstruction().config.currentData.IdTcsAmbiente;

                        if (newValue == 0) {
                            self.getWizardsByEnviroment(ambiente);
                        }
                        else if (newValue == 1) {
                            self.getDashboardsByEnviroment(ambiente);
                        }

                    }
                });


            }
        };

        return VM;
    });

