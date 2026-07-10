define(['services/logger', 'plugins/router', 'durandal/app', 'knockout', 'managers/user', 'managers/__auth', 'common'],
    function (logger, router, app, ko, managerUser, managerAuth, common) {

        return {
            menuItems: ko.observable(""),
            menuArray: ko.observableArray(),
            currentEnviroment: managerAuth.loginInfo.Ambientes[0].IdTcsAmbiente.toString(),
            currentHeaderMode: ko.observable(null),

            wizardTransactions: ko.observableArray(),
            dashboardTransactions: ko.observableArray(),

            linkHome: function () {
                return (managerAuth.isShellDevMode ? '#modulesdev' : '#modules')
            },

            //home_click: function () {
            //    router.navigate((managerAuth.isShellDevMode ? '#modulesdev' : '#modules'));
            //},

            //linkClick: function (url) {
            //    router.navigate(url);
            //},

            getBreadcrumb: function (route, title) {
                if (title == undefined) title = 'title';
                if (route.BreadCrumb) {
                    return route.BreadCrumb.map(function (e) { return e.displayName }).join(' > ');
                }
                else {
                    return route[title];
                };
            },

            findRouteInRouter: function (route) {
                var found = router.routes.filter(function (r) { return r.route == route.UrlRoute; });
                if (found.length > 0)
                    return found[0];
            },

            loadSearch: function () {

                var self = this;

                var dashboardTransactionsIds = [], wizardTransactionsIds = [];

                function buildItem(route, children, title, hash, childrenExp, addHash, icon) {
                    if (title == undefined) title = 'title';
                    if (hash == undefined) hash = 'hash';
                    var breadcrumb = self.getBreadcrumb(route, title);

                    if (children.length > 0) {
                        for (var i = 0; i < children.length; i++) {
                            var child = children[i];
                            buildItem(child, (childrenExp == undefined ? [] : child[childrenExp]), title, hash, childrenExp, addHash);
                        }
                    }

                    if (route.IsTransaction == true || ((managerAuth.isShellDevMode || managerAuth.isShellSetupMode) && (route.type == "transaction-assembly"))) {
                        if (route.IsTransaction && (route.Type === 7 || route.Type === 8)) {
                            if (route.Type === 7) {
                                if (wizardTransactionsIds.indexOf(route.Id) == -1) {
                                    var routerRoute = self.findRouteInRouter(route);
                                    if (routerRoute) {
                                        self.wizardTransactions.push({
                                            title: route.ShortDisplayName,
                                            pkg: routerRoute.moduleId,
                                            ambiente: route.IdTcsAmbiente
                                        });
                                        wizardTransactionsIds.push(route.Id);
                                    }
                                    else {
                                        console.warn('Tela wizard não encontrada', route.ShortDisplayName);
                                    }
                                };
                            }
                            else {
                                if (dashboardTransactionsIds.indexOf(route.Id) == -1) {
                                    var routerRoute = self.findRouteInRouter(route);
                                    if (routerRoute) {
                                        self.dashboardTransactions.push({
                                            title: route.ShortDisplayName,
                                            pkg: routerRoute.moduleId,
                                            ambiente: route.IdTcsAmbiente
                                        });
                                        dashboardTransactionsIds.push(route.Id);
                                    }
                                    else {
                                        console.warn('Tela dashboard não encontrada', route.ShortDisplayName);
                                    }

                                };
                            }
                        }
                        else {
                            if (self.menuArray().filter(function (e) { return (e.transactionId == route.Id && e.environment == route.IdTcsAmbiente) ; }).length == 0 ) {
                                self.menuArray.push({
                                    id: route.$id,
                                    title: route[title],
                                    breadcrumb: breadcrumb,
                                    route: (addHash == true ? '#' : '') + route[hash],
                                    transactionId : route.Id,
                                    environment: route.IdTcsAmbiente,
                                    tags :  isNullOrEmpty(route.Tags) ? '' : route.Tags.replaceAll(',', ' | ')
                                });
                            }
                        }
                    }
                }

                if (managerAuth.isShellDevMode || managerAuth.isShellSetupMode) {
                    var parentItems = router.routes.filter(function (record) {
                        return (record.type == "transaction-assembly" && managerAuth.isShellDevMode) || record.type == "transaction-report";
                    });
                    for (var i = 0; i < parentItems.length; i++) {
                        var item = parentItems[i];
                        var id = item.currentData ? item.currentData.$id : item.nav;
                        var breadcrumb = self.getBreadcrumb(item, 'title');
                        if (self.menuArray().filter(function (e) { return e.id == id; }).length == 0) {
                            self.menuArray.push({
                                id: id,
                                title: item['title'],
                                breadcrumb: breadcrumb,
                                route: item['hash'],
                                tags : ''
                            });
                        }
                    }
                }
                else {
                    var modules = managerUser.MODULES;
                    var ambientes = managerAuth.loginInfo.Ambientes;
                    for (var j = 0; j < ambientes.length; j++) {
                        var ambiente = ambientes[j];
                        for (var k = 0; k < modules.length; k++) {
                            var menu = modules[k];
                            buildItem(menu, menu.Menus, 'DisplayName', 'UrlRoute', 'Menus', null, menu.ClassIcon);
                        }
                    }
                };
            },

            loadMenu: function (IdTcsAmbiente) {

                var self = this;

                var menuString = '';

                //this.dashboardTransactions.push({ pkg: 'pkg_linx-dashboard-spa/viewmodels/DashBoard_Atendimento_Loja', title: 'Dashboard Loja' });

                function buildItem(route, children, title, hash, childrenExp, addHash, icon, isFavorite) {
                    if (title == undefined) title = 'title';
                    if (hash == undefined) hash = 'hash';

                    var breadcrumb = self.getBreadcrumb(route, title);

                    if (route.IsTransaction && (route.Type === 7 || route.Type === 8)) return; //Assistente e Dashboard

                    if (route.lxIsModuleFavorite) {
                        icon = 'star';
                        menuString += '<li class="list-item' + (!icon ? '-dropdown' : '') + '" tabindex="1">';
                        menuString += '     <a title="' + route[title] + '">';
                        if (icon) menuString += '    <i class="fa fa-' + icon + '" aria-hidden="true"></i>';
                        menuString += '    </a>';
                        menuString += '    <ul class="list-dropdown' + (!icon ? '-inside list-dropdown-inside--visible' : '') + '">';
                        if (icon) menuString += '        <h3 class="truncate"> ' + route[title] + ' </h3>';
                        for (var i = 0; i < children.length; i++) {
                            var child = children[i];
                            buildItem(child, (childrenExp == undefined ? [] : child[childrenExp]), title, hash, childrenExp, addHash, null, true);
                        }
                        menuString += '     </ul>';
                        menuString += '</li>';
                    }
                    else {
                        if (children.length > 0) {
                            var keepLevel = (route[title] == ".\\");
                            if (!keepLevel) {
                                menuString += '<li class="list-item' + (!icon ? '-dropdown' : '') + '" tabindex="1">';
                                menuString += '     <a title="' + route[title] + '" data-route="' + route[hash].replace('#', '') + '">';
                                if (icon) menuString += '    <i class="fa fa-' + icon + '" aria-hidden="true"></i>';
                                if (!icon) menuString += route[title];
                                if (!icon) menuString += '    <i class="fa fa-chevron-left" aria-hidden="true"></i>';
                                menuString += '    </a>';
                                menuString += '    <ul class="list-dropdown' + (!icon ? '-inside list-dropdown-inside--visible' : '') + '">';
                                if (icon) menuString += '        <h3 onclick="location.hash=\'' + (addHash == true ? '#' : '') + route[hash] + '\'; return false;" class="truncate"> ' + route[title] + ' </h3>';
                            }
                            for (var i = 0; i < children.length; i++) {
                                var child = children[i];
                                buildItem(child, (childrenExp == undefined ? [] : child[childrenExp]), title, hash, childrenExp, addHash);
                            }
                            if (!keepLevel) {
                                menuString += '     </ul>';
                                menuString += '</li>';
                            }
                        }
                        else {
                            if (isFavorite) {
                                menuString += '<li class="list-item-dropdown list-item-result" onclick="location.hash=\'' + (addHash == true ? '#' : '') + route[hash] + '\'; return false;" >' +
                                    '<h3>' + route[title] + '</h3>' +
                                    '<p>' + breadcrumb + '</p>' +
                                    '</li>';
                            }
                            else {
                                menuString += '<li class="list-item' + (!icon ? '-dropdown' : '') + '">' +
                                    '<a data-route="' + route[hash].replace('#', '') + '" onclick="location.hash=\'' + (addHash == true ? '#' : '') + route[hash] + '\'; return false;" href="#" title="' + route[title] + '">' +
                                    (icon ? '<i class="fa fa-' + icon + '"></i>' : '') +
                                    (icon ? '' : route[title]) + '</a>' +
                                    '</li>';
                            }
                        }
                    }



                }

                if (managerAuth.isShellDevMode || managerAuth.isShellSetupMode) {
                    var parentItems = router.routes.filter(function (record) {
                        return (record.type == "system" && managerAuth.isShellDevMode) || record.type == "menu-assembly" || record.type == "menu-report-modal" || record.type == "menu-report";
                    });
                    for (var i = 0; i < parentItems.length; i++) {
                        var parent = parentItems[i];
                        var children = [];
                        var icon = "";

                        if (parent.type == "system") icon = "sitemap";

                        if (parent.type == "menu-assembly") {
                            icon = "cube";
                            children = router.routes.filter(function (record) {
                                return (record.lxAssemblyName == parent.lxAssemblyName && record.type == "transaction-assembly");
                            });
                        }
                        else if (parent.type == "menu-report") {
                            icon = "print";
                            children = router.routes.filter(function (record) {
                                return (record.type == "transaction-report");
                            });
                        }
                        buildItem(parent, children, null, null, null, null, icon);
                    }
                }
                else {
                    var modules = managerUser.MODULES;
                    var ambientes = managerAuth.loginInfo.Ambientes.filter(function (amb) { return amb.IdTcsAmbiente == IdTcsAmbiente; });
                    for (var j = 0; j < ambientes.length; j++) {
                        var ambiente = ambientes[j];
                        var menus = modules.filter(function (record) { return record.IdTcsAmbiente == ambiente.IdTcsAmbiente });
                        for (var k = 0; k < menus.length; k++) {
                            var menu = menus[k];
                            buildItem(menu, menu.Menus, 'DisplayName', 'UrlRoute', 'Menus', true, menu.ClassIcon);
                        }
                    }
                }

                self.menuItems(menuString);

            },

            bindEvents: function () {

                $('#menu-search-clear').hide();
                $('#menu-search-loading').hide();
                var self = this;

                var menuIcon = $('.list-item');
                menuIcon.on('hover', function (e) {
                    $('.list-dropdown-inside').not('.list-dropdown-inside--visible').addClass('list-dropdown-inside--visible');
                    $(this).find('.fa-chevron-down').removeClass('fa-chevron-down').removeClass('fa-chevron-left').addClass('fa-chevron-left');
                });

                var element = $('.list-item-dropdown');
                $(element).on('click', function (e) {
                    e.preventDefault();
                    e.stopPropagation();

                    var item = $(this).find('.fa-chevron-left').first();
                    if (item.length > 0) {
                        item.toggleClass('fa-chevron-down');

                        var ul = $(this).children().next();
                        if (!ul.hasClass('list-dropdown-inside--visible') && ul.is('ul')) {
                            ul.find('.list-dropdown-inside').not('.list-dropdown-inside--visible').addClass('list-dropdown-inside--visible');
                        }
                        ul.toggleClass('list-dropdown-inside--visible');
                    }
                });

                function search(force) {

                    var me = this;

                    $(".list-item-input .list-dropdown").html('');

                    var existingString = $("#menu-search-input").val();

                    if (!force && existingString.length === 0) {
                        $('#menu-search-clear').hide();
                        return;
                    }

                    $('#menu-search-clear').hide();
                    $('#menu-search-loading').show();

                    setTimeout(function () {

                        var sifter = new Sifter(self.menuArray());

                        var result = sifter.search(existingString, {
                            fields: ['title', 'tags'],
                            sort: [{ field: 'title', direction: 'asc' }],
                            limit: 100,
                            conjunction: 'and'
                        });

                        var results = [];
                        for (var idx in result.items) {
                            var item = result.items[idx];
                            results.push(self.menuArray()[item.id]);
                        };

                        if (results.length > 0) {

                            for (var idx in results) {
                                var record = results[idx];
                                var li = '<li class="list-item-dropdown list-item-result" onclick="location.hash=\'' + record.route + '\'; return false;" >' +
                                    '<h3>' + record.title + '</h3>' +
                                    '<p>' + record.breadcrumb + '</p>' +
                                    '<p>' + record.tags + '</p>' +
                                    '</li>';
                                $(".list-item-input .list-dropdown").append(li);
                            }
                        }
                        else {
                            var li = '<li class="list-item-dropdown list-item-result">' +
                                '<p>Não foram encontrados resultados para os termos pesquisados.</p>' +
                                '</li>';
                            $(".list-item-input .list-dropdown").append(li);
                        }

                        $(".list-item-input .list-dropdown").mark(existingString, { element: 'span', className: 'result' });

                        $('#menu-search-loading').hide();
                        $('#menu-search-clear').show();

                    }, 200);
                }

                $('#menu-search-input').keyup(function (e) {
                    clearTimeout($.data(this, 'searchTimer'));
                    if (e.keyCode == 13)
                        search(true);
                    else
                        $(this).data('searchTimer', setTimeout(search, 800));
                });

                $('#menu-search-clear').click(function (e) {
                    $('#menu-search-input').val('').focus();
                    $('#menu-search-clear').hide();
                    $(".list-item-input .list-dropdown").html('');
                });
            },

            compositionComplete: function () {
                this.bindEvents();

                if (router.activeInstruction() && router.activeInstruction().fragment !== '') {
                    $('#sideMenu .list-item.active').removeClass('active');
                    $('#sideMenu a[data-route="' + router.activeInstruction().fragment + '"]').parents('.list-item').addClass('active');
                }

                router.activeInstruction.subscribe(function (newValue) {
                    $('#sideMenu .list-item.active').removeClass('active');
                    if (newValue && newValue.fragment && newValue.fragment !== '') {
                        $('#sideMenu a[data-route="' + newValue.fragment + '"]').parents('.list-item').addClass('active');
                    }
                });

                $('#main').on('click', function () {
                    $('#menu-search').removeClass('active');
                });

                $('#menu-search-input').on('focus', function () {
                    $('#menu-search').addClass('active');
                });

                $('ul.nav__list').on('hover', function () {
                    $('#menu-search').removeClass('active');
                });


            },

            activate: function () {
                this.loadMenu(this.currentEnviroment);
                this.loadSearch();
            },

            changeEnviroment: function (IdTcsAmbiente) {
                if (IdTcsAmbiente != this.currentEnviroment) {
                    this.currentEnviroment = IdTcsAmbiente;
                    this.loadMenu(IdTcsAmbiente);
                    this.bindEvents();
                }
            }


        };

    });

