define(['durandal/system', 'plugins/widget', 'services/logger', 'managers/user', 'plugins/router', 'common', 'managers/__auth', 'viewmodels/shell/_header', 'viewmodels/shell/_menu'],
    function (system, widget, logger, managerUser, router, common, managerAuth, _header, _menu) {
        var _modules = null;
               

        //////////////////////
        // class: ModuleItemVM
        //////////////////////
        var ModuleItemVM = function (p) {
            this.moduleKey = p.moduleKey,
            this.classType = p.classType,
            this.displayName = p.displayName,
            this.imagePath = p.imagePath,
            this.description = p.description,
            this.iconName = p.iconName,
            this.urlLink = p.urlLink,
            this.count = p.count,
            this.name = p.name
        }

        //////////////////////
        // class: VM
        //////////////////////
        var VM = {

            //vm fake
            currentDataItem: ko.observable({ IdBandeiraRede: ko.observable(null) }),
            status: ko.observable('Q'),
            setBandeiraRede: function () { },
            currentBrands: null,
            internalUIs: [],

            _header: _header,

            managerAuth: managerAuth,

            MODULES_VM: ko.observableArray(),

            _menu: _menu,

            Swiper: ko.observable(null),

            WizardSwiper: ko.observable(null),

            // events
            UI_linkFavoritar_Click: function () {
                var msg = confirm('Favoritar Módulo?');

                if (msg == true) {
                    $('.tile').addClass('addBord');
                    $('i.icon-star-empty').css('display', 'block');
                    $('.corner ul li:first-child span').html('Remover favorito?');
                }
            },

            UI_linkTornarTelaInicial_Click: function () {
                var msg = confirm('Definir a tela como inicial');

                if (msg == true) {
                    alert("Ele definiu a tela como inicial");
                    $('.tile').addClass('addBord');
                }
            },

            // Method: activate()
            activate: function () {
                if (_modules == null) {
                    _modules = [];
                    var data = router.activeInstruction().config;

                    for (var i = 0; i < router.routes.length; i++) {
                        var record = router.routes[i];

                        if ((record.type == "system" && managerAuth.isShellDevMode) || record.type == "menu-assembly" || record.type == "menu-report-modal" || record.type == "menu-report") {
                            _modules.push(new ModuleItemVM({
                                classType: this.BuildClassType(record),
                                moduleKey: '',
                                displayName: record.lxAssemblyName,
                                imagePath: '',
                                description: record.hash,
                                iconName: this.BuildIconName(record),
                                urlLink: record.hash,
                                isTransaction: false,
                                count: (record.type === "system" ? router.routes.length : record.lxCount),
                                name: (record.type === "menu-report-modal" ? "#modal" : "link_menudev_")
                            }));
                        }
                    }
                }

                
                
                this.MODULES_VM(_modules);

               

            },
          
            compositionComplete: function () {
                $(".page-container").show();
                common.showModalReport("#modal");
                App.fixContentHeight();
                
                var _swiper = new Swiper('.mainSwiperContainer', { onlyExternal: true, initialSlide: _header.activeMode() });
                this.Swiper(_swiper);

                _header.activeMode.subscribe(function (newValue) {
                    if (newValue !== null || newValue !== undefined)
                        _swiper.slideTo(newValue);
                });
                
                this.WizardSwiper(new Swiper('.wizardSwiperContainer', {
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

            },

            canDeactivate: function () {
                //$("#applicationHost").block({ message: null });
                //alert('canDeactivate')
                return true;
            },

            detached: function (view) {
                $(view).empty();
                $(view).remove();

                //view = null;
                delete view;

                //requirejs.undef('viewmodels/modulesdev');
            },

            BuildClassType: function (record) {
                if (record.type.indexOf("menu-assembly") >= 0) {
                    var validShellVersion = (record.lxShellCompiledVersion == managerAuth.shellVersion);

                    if (validShellVersion == true)
                        return "tile double bg-blue";
                    else
                        return "tile double bg-red";
                }

                else if (record.type.indexOf("menu-report") >= 0)
                    return "tile bg-yellow"

                else
                    return "tile bg-dark";
            },

            card_click: function (url) {
                router.navigate(url);
            },
            
            BuildIconName: function (record) {
                if (record.type.indexOf("menu-assembly") >= 0)
                    return "fa fa-cube";

                else if (record.type.indexOf("menu-report") >= 0)
                    return "fa fa-print"

                else
                    return "fa fa-cogs";
            }
        };

        return VM;
    });

