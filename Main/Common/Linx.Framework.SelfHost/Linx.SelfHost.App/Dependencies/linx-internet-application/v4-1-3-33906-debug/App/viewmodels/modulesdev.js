define(['durandal/system', 'services/logger', 'managers/user', 'plugins/router', 'common', 'managers/__auth'],
    function (system, logger, managerUser, router, common, managerAuth) {
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
        var VM = function () {
            var self = this;
            self.MODULES_VM = ko.observableArray();

            // events
            this.UI_linkFavoritar_Click = function () {
                var msg = confirm('Favoritar Módulo?');

                if (msg == true) {
                    $('.tile').addClass('addBord');
                    $('i.icon-star-empty').css('display', 'block');
                    $('.corner ul li:first-child span').html('Remover favorito?');
                }
            },

            // events
            this.UI_linkTornarTelaInicial_Click = function () {
                var msg = confirm('Definir a tela como inicial');

                if (msg == true) {
                    alert("Ele definiu a tela como inicial");
                    $('.tile').addClass('addBord');
                }
            },

            // Method: activate()
            this.activate = function () {
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
            };

            //    $('#applicationHost').addClass('page-sidebar-closed');
            //$('#applicationHost').removeClass('page-full-width');

            this.compositionComplete = function () {
                $(".page-container").show();
                common.showModalReport("#modal");
                App.fixContentHeight();

                //$(".gridster ul").gridster({
                //    widget_selector: 'div',
                //    widget_margins: [10, 10],
                //    widget_base_dimensions: [140, 140]
                //});

                //console.warn('Alerta de teste 1');
                //console.warn('Alerta de teste 2');
            };

            this.canDeactivate = function () {
                //$("#applicationHost").block({ message: null });
                //alert('canDeactivate')
                return true;
            };

            this.detached = function (view) {
                $(view).empty();
                $(view).remove();

                //view = null;
                delete view;

                //requirejs.undef('viewmodels/modulesdev');
            };

            this.BuildClassType = function (record) {
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
            }

            this.BuildIconName = function (record) {
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

