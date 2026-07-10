define(['services/logger', 'plugins/router', 'durandal/app', 'knockout', 'managers/user', 'managers/__auth', 'managers/brand', 'common', 'managers/window'],
    function (logger, router, app, ko, managerUser, managerAuth, managerBrand, common, managerWindow) {
               
        var VM = function () {

            var self = this;
            
            self.router =  router;
            self.managerWindow = managerWindow;
            self.managerUser = managerUser;
            self.managerAuth = managerAuth;
            self.managerBrand = managerBrand;
            self.activate = activate;
            self.compositionComplete = compositionComplete;
            
            self.paginaInicial = ko.observable("");
            self.menuItems = ko.observableArray();

            self.UIClearStorage = UIClearStorage;
            self.UIClearHome = UIClearHome;
            self.SupportRequest = SupportRequest;
            self.UIRefresh = UIRefresh;           
            self.UILogOut = UILogOut;
            self.UIChangeEnvironment = UIChangeEnvironment;
            self.UIChangePassword = UIChangePassword;

            self.loadModules = loadModules;
            self.closeAllPopovers = closeAllPopovers;
            self.renderBrands = renderBrands;
            self.bindEventsConfig = bindEventsConfig;
            self.bindEventsUser = bindEventsUser;
            self.closeMenu = closeMenu;

            function activate () {
                self.loadModules();
                var value = common.getStartPage();
                self.paginaInicial(value);
                return true;
            };

            function closeMenu() {
                self.closeAllPopovers();
                var API = $("#mmenu").data("mmenu");
                API.close();
            }

            function bindEventsUser() {
                $('.popover .mm-user-enviroments').one('click', self.UIChangeEnvironment);
                $('.popover .mm-user-changepassword').one('click', self.UIChangePassword);
                $('.popover .mm-user-logout').one('click', self.UILogOut);
            }

            function bindEventsConfig() {

                $('.popover .UIClearHome').on('click', self.UIClearHome);
                $('.popover .UIClearStorage').on('click', self.UIClearStorage);
                $('.popover .SupportRequest').on('click', self.SupportRequest);

                $(".popover .chkTrace").bootstrapSwitch('state', (managerAuth.traceMode));

                if (managerAuth.profilerEnabled == false) {
                    $(".popover .chkTrace").on('switchChange.bootstrapSwitch', function (event, state) {

                        if (state) {
                            app.showMessage('Impossível habilitar o trace!<BR><BR>1. Configure a chave <b>Shell.MiniProfiler.Enabled</b> para "true" no web.config <br> <br> 2. Configure a chave <b>MiniProfiler.Enabled</b> para "true" no web.config do serviço.<BR>', 'Linx UX', ['Reiniciar', 'Cancelar']).then(function (dialogResult) {
                                if (dialogResult != "Cancelar") {
                                    common.showProcessFull();
                                    window.location.reload();
                                }
                            });

                            $(".popover .chkTrace").bootstrapSwitch('state', false);
                        }
                    });
                }
                else {
                    $(".popover .chkTrace").on('switchChange.bootstrapSwitch', function (event, state) {
                        var url = '';

                        if (state)
                            url = (window.location.origin + window.location.pathname + "?tracemode=on" + window.location.search.replace("?", "&") + window.location.hash);
                        else
                            url = (window.location.origin + window.location.pathname + window.location.hash);
                        common.showProcessFull();
                        window.location.href = url;
                    });
                }

                $(".popover .chkLastFilterMode").bootstrapSwitch('state', common.getLastFilterMode());

                $(".popover .chkLastFilterMode").on('switchChange.bootstrapSwitch', function (event, state) {
                    common.saveLastFilterMode(state);
                });

                $(".popover .chkBarraNavegacao").bootstrapSwitch('state', common.getBarraNavegacao());

                $(".popover .chkBarraNavegacao").on('switchChange.bootstrapSwitch', function (event, state) {
                    common.saveBarraNavegacao(state);
                    UIRefresh();
                });

                $(".popover .chkGridMode").bootstrapSwitch('state', (common.getGridMode() == 'G'));

                $(".popover .chkGridMode").on('switchChange.bootstrapSwitch', function (event, state) {
                    common.saveGridMode(state)
                });

            }

            function compositionComplete () {
                       
                router.on('saveStartPage:changed').then(function (newValue) {
                    self.paginaInicial(newValue);
                });

                var uniq = function (array) {
                    for (var i = 0, l = array.length; i < l; ++i) {
                        var item = array[i];
                        var dublicateIdx = array.indexOf(item, i + 1);
                        while (dublicateIdx != -1) {
                            array.splice(dublicateIdx, 1);
                            dublicateIdx = array.indexOf(item, dublicateIdx);
                            l--;
                        }
                    }

                    return array;
                }

                $('#mmenu [data-toggle="tooltip"]').tooltip();
                
                var menuContent = [];
                menuContent.push("<a class='menu-bottom-user' href='javascript:void(0)' data-container='body' data-toggle='popover' data-placement='top'><i class='fa fa-user'></i><span>" + managerUser.USER_NAME + "</span></a>");
                if (managerAuth.isShellProdMode && !managerAuth.isLoginPOSUXMode)
                    menuContent.push("<a class='menu-bottom-enviroments' href='javascript:void(0)' data-container='body' data-toggle='popover' data-placement='top'><i class='fa fa-flag'></i><span>Ambientes</span></a>");
                menuContent.push("<a class='menu-bottom-config' href='javascript:void(0)' data-container='body' data-toggle='popover' data-placement='top'><i class='fa fa-cog'></i><span>Configurações</span></a>");

                var $menu = $("#mmenu").mmenu({
                    offCanvas: {
                        moveBackground: false,
                        zposition: 'front'
                    },
                    navbar: {
                        titleLink: 'none'
                    },
                    "navbars": [
                        {
                            "position": "bottom",
                            "content": menuContent
                        }
                    ],
                    extensions: [
                        "border-full"
                    ],
                    onClick: {
                        close: true
                    }
                });

                var API = $("#mmenu").data("mmenu");


                var routeClick = function () {
                    self.closeAllPopovers();
                    $menu.find('.mm-resultspanel').addClass('mm-hidden').removeClass('mm-highest mm-current mm-opened');
                    API.close();
                }

                var itemClick = function () {
                    self.closeAllPopovers();
                    var anchor;
                    if ($(this).hasClass('site-menu-item') || $(this).hasClass('mm-navbar')) {
                        anchor = $(this).find('a[data-target]');
                    }

                    var target = $(anchor).data('target');

                    if (target.length > 1 && target.slice(0, 1) == '#') {
                        try {
                            var $h = $(target);
                            if ($h.is('.mm-panel')) {
                                API.openPanel($h);
                            }
                        }
                        catch (err) { }
                    }
                };

                API.bind("openedPanel", function ($panel) {
                    $menu.find('a.mm-btn.mm-prev').prop('href', 'javascript:void(0)');
                    $menu.find('.mm-navbar:has(".mm-prev")').on('click', itemClick);
                });

                $('.menu-bottom-user').popover({
                    html: true,
                    content: function () {
                        return $(".user-popover").html();
                    }
                }).on('shown.bs.popover', function () {
                    $('.menu-bottom-enviroments').popover('hide');
                    $('.menu-bottom-config').popover('hide');
                    self.bindEventsUser();
                   
                });

                $('.menu-bottom-enviroments').popover({
                    html: true,
                    content: function () {
                        return $(".ambientes-popover").html();
                    }
                }).on('shown.bs.popover', function () {
                    $('.menu-bottom-user').popover('hide');
                    $('.menu-bottom-config').popover('hide');
                    self.renderBrands();
                });

                $('.menu-bottom-config').popover({
                    html: true,
                    content: function () {
                        return $(".config-popover").html();
                    }
                }).on('shown.bs.popover', function () {
                    $('.menu-bottom-user').popover('hide');
                    $('.menu-bottom-enviroments').popover('hide');
                    self.bindEventsConfig();
                });


                API.bind("init", function ($panel) {

                });

                API.bind("closing", function ($panel) {
                    $menu.find('.mm-resultspanel').addClass('mm-hidden').removeClass('mm-highest mm-current mm-opened');
                    $('#mmenu-search').removeClass('open'); $inpt.val('');
                    self.closeAllPopovers();
                });

                $menu.find('li:has(".mm-next") a').prop('href', 'javascript:void(0)');
                $menu.find('li:has(".mm-next")').on('click', itemClick);
                $menu.find('li:not(:has(".mm-next"))').on('click', routeClick);

                $menu.prepend(
                '<div id="mmenu-search">' +
                    '<input placeholder="Digite sua busca aqui..." class="search-input">' +
                    '<button class="search-button"><i class="fa fa-search"></i></button>' +
                    '<button class="close-button"><i class="fa fa-times"></i></button>' +
                '</div>');

                $menu
                .find('.mm-panels')
                .append('<div class="mm-panel mm-resultspanel mm-hidden" id="mm-999999">' +
                            '<ul class="mm-listview">' +
                            '</ul>' +
                            '<div class="mm-noresultsmsg mm-hidden">No results found.</div>' +
                        '</div>');

                var $inpt = $('#mmenu-search input');

                $("#mmenu-search .close-button").on('click', function () {
                    $menu.find('.mm-resultspanel').addClass('mm-hidden').removeClass('mm-highest mm-current mm-opened');
                    $('#mmenu-search').removeClass('open'); $inpt.val('');
                });

                $("#mmenu-search .search-button").on('click', function () { $('#mmenu-search').addClass('open'); $inpt.focus(); });
                
                $inpt
				.off('keyup change')
				.on('keyup',
					function (e) {
					    if (!preventKeypressSearch(e.keyCode)) {
					        search.call($inpt);
					    }
					}
				)
				.on('change',
					function (e) {
					    search.call($inpt);
					}
				);

                function preventKeypressSearch(c) {
                    switch (c) {
                        case 9:		//	tab
                        case 16:	//	shift
                        case 17:	//	control
                        case 18:	//	alt
                        case 37:	//	left
                        case 38:	//	top
                        case 39:	//	right
                        case 40:	//	bottom
                            return true;
                    }
                    return false;
                }

                var list = $menu.find('li:not(:has(".mm-next"))');
                var query = '';
                var search = function () {

                    var q = $inpt.val().toLowerCase();
                    if (q == query) {
                        return;
                    }
                    query = q;

                    if (query == '') {
                        $menu.find('.mm-resultspanel').addClass('mm-hidden').removeClass('mm-highest mm-current mm-opened');
                        return;
                    }

                    $menu.find('.mm-resultspanel .mm-listview').html('');

                    var items = list.filter(function (i, e) { return $(e).text().toLowerCase().indexOf(query) >= 0; }).map(function (i, e) { var item = { parent: $(e).data('bc'), title: $(e).text(), url: $(e).find('a').attr('href') }; return item; });
                    var parentItems = uniq(items.map(function (i, e) { return e.parent }).toArray());

                    var resultString = '';
                    for (var i = 0; i < parentItems.length; i++) {
                        var parent = parentItems[i];
                        resultString += '<li class="mm-divider">' + parent + '</li>';
                        var children = items.filter(function (i, e) { return e.parent == parent; });
                        for (var j = 0; j < children.length; j++) {
                            var child = children[j];
                            resultString += '<li class="site-menu-item">' +
                                                '<a href="' + child.url + '">' + child.title + '</a>' +
                                            '</li>';
                        }
                    }

                    $menu.find('.mm-resultspanel .mm-listview').html(resultString);

                    $menu.find('.mm-resultspanel').removeClass('mm-hidden').addClass('mm-highest mm-current mm-opened');
                };

            };
            
            function closeAllPopovers () {
                $('.menu-bottom-user').popover('hide');
                $('.menu-bottom-enviroments').popover('hide');
                $('.menu-bottom-config').popover('hide');
            };

            function renderBrands () {

                var brands = $('.ambiente-brand');

                function format(sourceData) {
                    return sourceData.html_select2;
                }

                for (var i = 0; i < brands.length; i++) {
                    var brand = brands[i];
                    var idTcsAmbiente = $(brand).attr('brand');
                    
                    $(brand).editable({
                        inputclass: 'form-control input-large select2',
                        select2: {
                            minimumResultsForSearch: -1,
                            allowClear: true,
                            formatResult: format,
                            formatSelection: format,
                            escapeMarkup: function (m) {
                                return m;
                            }
                        },
                        type: 'select2',
                        value: managerBrand.getDefaultBrand(idTcsAmbiente),
                        url: '',
                        source: managerBrand.getBrandVM(idTcsAmbiente),
                        title: 'Bandeira/Rede',
                        placement: 'left',
                        onblur: 'submit',
                        highlight: false,
                        showbuttons: false,
                        emptytext: managerBrand.getDefaultBrand(idTcsAmbiente),
                        mode: 'popup',
                        error: function (data) {
                        },
                        success: function (response, newValue) {
                            managerBrand.saveDefaultBrand($(this).attr('brand'), newValue);
                        },
                        validate: function (value) {
                            if ($.trim(value) == '')
                                return 'Seleção obrigatória!';
                        },
                        display: function (value, sourceData) {
                            var val = managerBrand.searchBrandsVM(value, $(this).attr('brand'));
                            if (!value || val == "") {
                                $(this).empty();
                                return;
                            }

                            $(this).html(val.text);
                        }

                    });

                }
                


            }

            function UIClearHome(color) {               
                common.saveStartPage('');
            };

            function UILogOut () {

                $('.menu-bottom-user').popover('hide');

                if (managerAuth.isLoginPOSUXMode) {
                    $.ezstorage.remove('Hash_Login');
                    window.location.href = window.location.origin + window.location.pathname;
                }
                else {
                    $.sessionStorage.removeAll();
                    window.location.href = managerAuth.buildRoot('logoff');
                }
            };

            function UIChangeEnvironment () {
                $('.menu-bottom-user').popover('hide');
                $.sessionStorage.removeAll();
                window.location.href = managerAuth.buildRoot('ChangeEnvironment');
            }

            function UIChangePassword () {
                $('.menu-bottom-user').popover('hide');
                require(['viewmodels/shared/modalChangePassword'], function (modalChangePassword) {
                    modalChangePassword.show(true);
                });

            };
            
            function loadModules () {

                var menuString = '';
                var menuArray = [];
                     
                function getBreadcrumb(route, title) {
                    if (title == undefined) title = 'title';
                    if (route.BreadCrumb) {
                        return route.BreadCrumb.map(function (e) { return e.displayName }).join(' > ');
                    }
                    else {
                        return route[title];
                    }
                }

                function buildItem(route, children, title, hash, childrenExp, addHash, icon) {
                    if (title == undefined) title = 'title';
                    if (hash == undefined) hash = 'hash';
                    
                    if (children.length > 0) {
                        menuString += '<li class="site-menu-item" data-bc="' + getBreadcrumb(route, title) + '" data-toggle="tooltip" data-container="body" data-delay=\'{"show": "1000", "hide":"0"}\' data-placement="right" title="' + route[title] + '">';
                        menuString += '     <a>' + (icon ? '<i class="fa fa-' + icon + '"></i>' : '') + route[title] + '</a>';
                        menuString += '     <ul>';
                        for (var i = 0; i < children.length; i++) {
                            var child = children[i];
                            buildItem(child, (childrenExp == undefined ? [] : child[childrenExp]), title, hash, childrenExp, addHash);
                        }
                        menuString += '     </ul>';
                        menuString += '</li>';
                    }
                    else {
                        menuString += '<li class="site-menu-item" data-toggle="tooltip" data-bc="' + getBreadcrumb(route, title) + '" data-container="body" data-delay=\'{"show": "1000", "hide":"0"}\' data-placement="right" title="' + route[title] + '"><a href="' + (addHash == true ? '#' : '') + route[hash] + '">' + (icon ? '<i class="fa fa-' + icon + '"></i>' : '') + route[title] + '</a></li>';
                    }
                }

                buildItem({ DisplayName: 'Home', UrlRoute: '#' }, [], 'DisplayName', 'UrlRoute', undefined, true, 'home');

                if (managerAuth.isShellDevMode || managerAuth.isShellSetupMode) {
                    var parentItems = router.routes.filter(function (record) {
                        return (record.type == "system" && managerAuth.isShellDevMode) || record.type == "menu-assembly" || record.type == "menu-report-modal" || record.type == "menu-report";
                    });
                    for (var i = 0; i < parentItems.length; i++) {
                        var parent = parentItems[i];
                        var children = [];
                        if (parent.type == "menu-assembly") {
                            children = router.routes.filter(function (record) {
                                return (record.lxAssemblyName == parent.lxAssemblyName && record.type == "transaction-assembly");
                            });                            
                        }
                        else if (parent.type == "menu-report") {
                            children = router.routes.filter(function (record) {
                                return (record.type == "transaction-report");
                            });                            
                        }                        
                        buildItem(parent, children);
                    }
                }
                else {
                    var modules = managerUser.MODULES;
                    var ambientes = managerAuth.loginInfo.Ambientes;                    
                    for (var j = 0; j < ambientes.length; j++) {
                        var ambiente = ambientes[j];
                        var menus = modules.filter(function (record) { return record.IdTcsAmbiente == ambiente.IdTcsAmbiente });

                        var icon = null;
                        if (ambiente.IdTcsAplicativo == 3) icon = 'cogs'; //Op
                        if (ambiente.IdTcsAplicativo == 2) icon = 'bank'; //Adm

                        buildItem({ DisplayName: ambiente.DescricaoAmbiente, UrlRoute: '' }, menus, 'DisplayName', 'UrlRoute', 'Menus', true, icon);                        
                    }                    
                }

                this.menuItems(menuString);      
            }
                   
            function UIRefresh() {
                var currentModule = router.activeItem().__moduleId__;

                requirejs.undef(currentModule)
                requirejs.undef(currentModule + 'Complement')
                requirejs.undef('text!' + currentModule.replace('viewmodels', 'views') + '.html')

                router.deactivate();
                router.activate();
            }

            function UIClearStorage() {
                self.closeMenu();
                common.showProcess('#main');

                //Cache Geral
                $.ajax({
                    type: 'POST',
                    globalError: true,
                    message: "Limpando cache",
                    messageUser: "Limpando cache",
                    headers: managerAuth.getHeaders(managerAuth.loginInfo.IdTcsAmbienteDefault),
                    url: managerAuth.getServiceAddress('LinxFrameworkUtilitarios', 'Linx.Framework.BV') + '/CleanCache',
                    data: JSON.stringify({
                        UidUsuario: managerAuth.loginInfo.UidUsuario,
                        BandeiraRede: true,
                        Modulo: true,
                        Conexao: false,
                        Geral: false,
                        Relatorio: false
                    }),
                    contentType: "application/json",
                    async: true,
                    cache: false,

                    error: function (jqXHR, textStatus, errorThrown) {
                        common.closeProcess('#main');
                    },

                    success: function (data) {
                        $.localStorage.removeAll();
                        $.sessionStorage.removeAll();
                        window.location.reload();
                        common.closeProcess('#main');
                    }
                });

            };
           
            function SupportRequest() {
                
                self.closeMenu();
                common.showProcess('#main');

                $.ajax({
                    type: 'POST',
                    globalError: true,
                    message: "Requisitando Url para suporte",
                    messageUser: "Requisitando Url para suporte",
                    headers: managerAuth.getHeaders(managerAuth.loginInfo.IdTcsAmbienteDefault),
                    url: managerAuth.getServiceAddress('LinxFrameworkUsuarioAutorizacao', 'Linx.Framework.BV') + '/SupportRequest',
                    data: JSON.stringify({
                        UidUsuario: managerAuth.loginInfo.UidUsuario,
                        IdTcsAmbiente: managerAuth.loginInfo.IdTcsAmbienteDefault,
                        UrlPortal: managerAuth.loginUrl
                    }),
                    contentType: "application/json",
                    async: true,
                    cache: false,
                    error: function (jqXHR, textStatus, errorThrown) {
                        common.closeProcess('#main');
                    },
                    success: function (data) {
                        common.closeProcess('#main');
                        require(['viewmodels/shared/modalSupportRequestUrl'], function (modalSupportRequest) {
                            modalSupportRequest.show(data);
                        });
                    }
                });
            };

            
        };

        return VM;
    });

