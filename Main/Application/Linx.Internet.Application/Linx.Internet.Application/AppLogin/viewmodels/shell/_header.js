define(['services/logger', 'plugins/router', 'durandal/app', 'knockout', 'managers/__auth', 'common'],
    function (logger, router, app, ko, managerAuth, common) {

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
            managerAuth: managerAuth,
            UIOpenSideBar: UIOpenSideBar,
            UIRefresh: UIRefresh,
            common: common,
            makeFullScreen: makeFullScreen,
            isFullScreen: ko.observable(false)
            //OpenSiderBar: OpenSidebar

        };

        return vm;

       











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
            })

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