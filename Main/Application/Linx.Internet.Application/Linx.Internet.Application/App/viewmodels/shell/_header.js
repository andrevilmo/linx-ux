define(['services/logger', 'plugins/router', 'durandal/app', 'knockout', 'managers/user', 'managers/__auth', 'common', 'managers/window', 'viewmodels/shell/_menu', 'managers/message' ],
    function (logger, router, app, ko, managerUser, managerAuth, common, managerWindow, _menu, managerMessage) {

        
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

        //#region Fullscreen
       var makeFullScreen = function () {
            // API resquestFullscreen
            if (!document.fullscreenElement && !document.mozFullScreenElement && !document.webkitFullscreenElement && !document.msFullscreenElement) {
                vm.isFullScreen(true);
                if (document.documentElement.requestFullscreen) {
                    document.documentElement.requestFullscreen();
                } else if (document.documentElement.msRequestFullscreen) {
                    // Acertando o posicionamento
                    $('body').css({ 'position': 'fixed' });
                    $('#main').css({ 'position': 'fixed', 'width': '100%' });
                    document.documentElement.msRequestFullscreen();
                } else if (document.documentElement.mozRequestFullScreen) {
                    document.documentElement.mozRequestFullScreen();
                } else if (document.documentElement.webkitRequestFullscreen) {
                    document.documentElement.webkitRequestFullscreen(Element.ALLOW_KEYBOARD_INPUT);
                }

            } else {
                vm.isFullScreen(false);
                if (document.exitFullscreen) {
                    document.exitFullscreen();
                } else if (document.msExitFullscreen) {
                    document.msExitFullscreen();
                    // Acertando o posicionamento
                    $('body').css({ 'position': 'absolute', 'overflow': 'hidden' });
                    $('#main').css({ 'position': 'static' });
                } else if (document.mozCancelFullScreen) {
                    document.mozCancelFullScreen();
                } else if (document.webkitExitFullscreen) {
                    document.webkitExitFullscreen();
                }

            }
        }
        //#endregion


       var vm = {

           _menu: _menu,

            activate: activate,
            attached: attached,
            beforeBind: beforeBind,
            afterBind: afterBind,
            canDeactivate: canDeactivate,
            canActivate: canActivate,
            deactivate: deactivate,
            compositionComplete: compositionComplete,

            parentVM: null,
            title: '',

            router: router,
            managerUser: managerUser,
            managerAuth: managerAuth,
            managerMessage: managerMessage,

            UIOpenSideBar: UIOpenSideBar,
            UIOpenNotificationBar: UIOpenNotificationBar,
            UIRefresh: UIRefresh,
            common: common,
            makeFullScreen: makeFullScreen,
            isFullScreen: ko.observable(false),
            buildBreadCrumb: buildBreadCrumb,
            BREADCRUMB_VM: ko.observableArray(),

            activeMode: _menu.currentHeaderMode,
            changeMode: changeMode 
        };

        return vm;             
        
        function changeMode(mode) {
            if (router.activeInstruction().config.type == 'module'){
                var aModes = ["Assistentes de utilização", "Dashboards", "Módulos"];
                $('#mainHeader .title').html(aModes[mode]);
            }           
            this.activeMode(mode);
        }

        function buildBreadCrumb() {

            var self = this;
            if (router.activeInstruction().config.type == 'transaction-assembly' || router.activeInstruction().config.type == 'transaction') {
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
                }
            }
            else {
                self.BREADCRUMB_VM(null);
            }

                        
        }

        //#region Internal Methods
        function canActivate() {
            return true;
        }

        function canDeactivate() {
            return true;
        }

        function beforeBind() {
            return true;
        }

        function afterBind() {
            return true;
        }

        function attached() {
            return true;
        }

        function deactivate() {
            return true;
        }

        function activate(settings) {
            var self = this;

            if (common.getBarraNavegacao()) {
                self.buildBreadCrumb();
            }

            if ((typeof settings === 'object') && (settings != null) && settings.parentVM) {
                vm.parentVM = settings.parentVM;
                var t = settings.parentVM.dataToolbar.title();
                vm.title = (t === '' ? settings.parentVM.viewName : t)
            }
            else {
                vm.parentVM = null;
            }
            return true;
        }

       
        
        function compositionComplete() {
            //QuickSidebar.init(); // Handles quick sidebar toggler

            //if ($.cookie('style_mode')) {
            //    $('#selModeOption').val($.cookie('style_mode'));
            //}

            //$("#selModeOption").change(function () {
            //    var val = $('#selModeOption').val();
            //    $.cookie('style_mode', val)

            //    if (val == 'mouse') {
            //        $("body").removeClass("touch")
            //    }
            //    else {
            //        $("body").addClass("touch")
            //    }

            //});
            App.init();

            // Aplica sombra no cabeçalho quando necessario
            $("#main").scroll(function () {
                if ($('#main').scrollTop() > 30) {
                    $('#mainHeader').addClass('box-shadow-bottom');
                } else {
                    $('#mainHeader').removeClass('box-shadow-bottom');
                }
            });

            if (_menu.currentHeaderMode() == null) {
                if (this._menu.wizardTransactions().length == 0 && this._menu.dashboardTransactions().length == 0) {
                    this.changeMode(2);
                    }
                else if (this._menu.wizardTransactions().length > 0 && this._menu.dashboardTransactions().length == 0) {
                    this.changeMode(0);
                }
                else {
                    this.changeMode(1);
                }
            }
            

            // Mostra/Oculta migalha de pão.
            //$('a.breadcrumb').on('click', function () {
            //    $('#mainBreadCrumd').toggle('slow');
            //}); 

        }

        // Method: UIOpenSideBar()
        function UIOpenSideBar() {
            //alert("UIOpenSideBar");
            //QuickSidebar.init();
            $('body').toggleClass('page-quick-sidebar-open', 'closeSidebarClick');
            $('.closeSidebarClick').show();

            // Se clicar em qualquer area do sidebar, o Drop deve ser fechado
            $('.page-quick-sidebar-alerts-list').on('click', function () {
                $('#select2-drop').css('display', 'none');
            });

            // Ao clicar na ABA tab_1
            $('a[href=#quick_sidebar_tab_1]').on('click', function () {
                $('.popover').css('display', 'none');
                $('.select2-drop').css('display', 'none');
            });

            // Ao clicar na ABA tab_2
            $('a[href=#quick_sidebar_tab_2]').on('click', function () {
                CloseSelect2();
                //$("#select2_brandUser").editable().toggle();
                $('#select2_brandUser').select2("close");
            });

            // Ao clicar na ABA tab_3
            //$('a[href=#quick_sidebar_tab_3]').on('click', function () {
            //    CloseSelect2();
            //});

            //managerWindow.closeAll();

            // Ao clicar em outro lugar da tela
            $('.closeSidebarClick').click(function () {
                CloseSelect2();
                //$("#select2_brandUser").editable().toggle();
                $('#select2_brandUser').select2("close");
                $(this).hide();
                $('body').removeClass('page-quick-sidebar-open');
            });

            // Ao clicar no icone back para mobile
            //$('#icon-back i').click(function () {
            //    $('.closeSidebarClick').hide();
            //    $('body').removeClass('page-quick-sidebar-open');
            //});
        };

        function UIOpenNotificationBar() {
            var eleNotification = $('#notificationBarContent');
            eleNotification.toggle();
        };

        function CloseSelect2() {
            $('.select2-dropdown-open').each(function (index) {
                $(this).select2("close");
            });
        };
        //#endregion

        function UIRefresh() {
            var currentModule = router.activeItem().__moduleId__;

            requirejs.undef(currentModule)
            requirejs.undef(currentModule + 'Complement')
            requirejs.undef('text!' + currentModule.replace('viewmodels', 'views') + '.html')

            router.deactivate();
            router.activate();
        };
    });