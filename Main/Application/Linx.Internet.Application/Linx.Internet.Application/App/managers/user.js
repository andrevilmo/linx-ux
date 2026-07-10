define(['durandal/system', 'durandal/app', 'services/logger', 'managers/__auth', 'common'],
    function (system, app, logger, managerAuth, common) {
        //////////////////////////
        // class: BreadCrumbItem
        //////////////////////////
        var BreadCrumbItem = function (p) {
            var self = this;
            self.order = p.order;
            self.moduleKey = p.moduleKey;
            self.displayName = p.displayName;
            self.urlRoute = p.urlRoute;
        };

        var ChangeSpecialCaracters = function (input) {
            return input.replace(/[ ]/g, "-").trim();
        };

        return {
            USER_NAME: managerAuth.loginInfo.NomeCurtoUsuario,
            MODULES: [],
            MODULES_PLAIN: [],
            REPORTS: [],
            SERVICES: [],


            ///////////////////////
            // method: loadModules()
            ///////////////////////
            loadModules: function () {
                var that = this;
                var dfd = $.Deferred();

                var cacheKey = common.getCachePrefixEnvironment('API', 'LinxFrameworkModulo/fullmodules', managerAuth.loginInfo.CacheKey);
                var cacheObj = $.ezstorage.get(cacheKey);

                var cacheKeyHash = common.getCachePrefixEnvironment('HASH', 'LinxFrameworkModulo/fullmodules', managerAuth.loginInfo.CacheKey);
                var cacheValueHash = $.ezstorage.get(cacheKeyHash);

                if (cacheValueHash == null || cacheObj == null) {
                    system.log('Main: Loading Modules and Menus...');

                    // dados vazio
                    if (cacheObj == null)
                        cacheValueHash = null;
                    else
                        // contem dados mas o "hash" expirou, forca chamar a api novamente
                        cacheValueHash = cacheObj.hash;

                    var environmentInfo = [];
                    for (var i = 0; i < managerAuth.loginInfo.Ambientes.length; i++) {
                        var item = managerAuth.loginInfo.Ambientes[i];
                        environmentInfo.push({ Hash: cacheValueHash, EnvironmentId: item.IdTcsAmbiente, ApplicationUid: item.UidAplicacao, CompanyUid: item.UidEmpresa, AplicativeId: item.IdTcsAplicativo });
                    }

                    return $.ajax({
                        type: 'POST',
                        message: "Buscando módulos",
                        messageUser: "Acesso aos módulos/menus/transações configurados",
                        headers: managerAuth.getHeaders(),
                        globalError: true,
                        url: managerAuth.getServiceAddress('LinxFrameworkModulo', 'Linx.Framework.BV') + '/fullmodulesMultiEnvironment',
                        data: JSON.stringify(environmentInfo),
                        contentType: "application/json",
                        async: true,
                        cache: false,
                        success: function (data, textStatus, response) {
                            var cacheHeaderHash = (response.getResponseHeader('cacheHash') == null ? '' : response.getResponseHeader('cacheHash'));
                            var obj = { hash: cacheHeaderHash, value: data };

                            if (cacheHeaderHash == cacheValueHash) {
                                // conteudo vazio vindo da api
                                obj.value = cacheObj.value;
                            }

                            // armazena em cache os dados e o hash
                            $.ezstorage.set(cacheKeyHash, cacheHeaderHash, { expires: 1 })
                            $.ezstorage.set(cacheKey, obj, { expires: 90 })

                            that.configureClassType(obj.value);
                            that.createBreadCrumb(obj.value, null, null, '', 0);
                            that.createModulesTable(obj.value, 0, '', '');
                            that.MODULES = obj.value;
                            that.buildModuleFavorites();

                            dfd.promise();
                        }
                    });
                }
                else {
                    system.log('Main: Loading Modules and Menus... [Storage]');

                    //that.configureClassType(cacheObj.value);
                    that.createBreadCrumb(cacheObj.value, null, null, '');
                    that.createModulesTable(cacheObj.value, 0, '', '');
                    that.MODULES = cacheObj.value;
                    that.buildModuleFavorites();

                    return dfd.resolve();
                }

            },

            ///////////////////////
            // method: createBreadCrumb()
            ///////////////////////
            createBreadCrumb: function (modules, parentBreadCrumb, parentModule, parentDisplayName, idAmbiente) {
                if (modules.length == 0)
                    return;

                for (var i = 0; i < modules.length; i++) {
                    var item = modules[i];

                    // tratammento BreadCrumb
                    if (modules[i].BreadCrumb == null) {
                        modules[i].BreadCrumb = new Array();

                        if ((parentBreadCrumb == null && !managerAuth.isLoginPOSUXMode) || item.IdTcsAmbiente != idAmbiente) {
                            idAmbiente = item.IdTcsAmbiente;

                            var descTcsAplicativo = managerAuth.getUrlTcsAplicativo(item.IdTcsAplicativo);

                            if (!isNullOrEmpty(descTcsAplicativo)) {

                                parentBreadCrumb = new Array(new BreadCrumbItem({
                                    order: 0,
                                    moduleKey: '',
                                    displayName: descTcsAplicativo,
                                    urlRoute: ''
                                }));

                                parentBreadCrumb.push(new BreadCrumbItem({
                                    order: 0,
                                    moduleKey: '',
                                    displayName: 'Módulos',
                                    urlRoute: ''
                                }));
                            }
                            else {
                                parentBreadCrumb = new Array(new BreadCrumbItem({
                                    order: 0,
                                    moduleKey: '',
                                    displayName: 'Módulos',
                                    urlRoute: ''
                                }));
                            }
                        }

                        //// adiciona os itens ja existentes
                        var y = 0;
                        for (y = 0; y < parentBreadCrumb.length; y++) {
                            modules[i].BreadCrumb.push(parentBreadCrumb[y])
                        }

                        // adiciona o item atual
                        if (parentModule != null && parentModule.DisplayName != ".\\") {

                            modules[i].BreadCrumb.push(new BreadCrumbItem({
                                order: y,
                                moduleKey: parentModule.Id,
                                displayName: parentModule.DisplayName,
                                //urlRoute: 'menu-' + parentModule.Id.toString()
                                urlRoute: ChangeSpecialCaracters(parentDisplayName)
                            }));
                        }
                    }

                    // existem menus
                    if (item.Menus.length > 0) {
                        this.createBreadCrumb(item.Menus, modules[i].BreadCrumb, modules[i], ((parentDisplayName == '' && !managerAuth.isLoginPOSUXMode ? managerAuth.getUrlTcsAplicativo(item.IdTcsAplicativo) : parentDisplayName) + '/' + modules[i].FriendlyUrl), idAmbiente)
                    }
                }
            },

            ///////////////////////
            // method: configureClassType()
            ///////////////////////
            configureClassType: function (modules) {
                if (modules.length == 0)
                    return;

                for (var i = 0; i < modules.length; i++) {

                    if (modules[i].ClassType == null) {
                        modules[i].ClassType = '';
                    }

                    if (modules[i].ClassBackground == null) {
                        modules[i].ClassBackground = '';
                    }

                    if (modules[i].ClassSize == null) {
                        modules[i].ClassSize = '';
                    }

                    if (modules[i].ClassIcon == null) {
                        modules[i].ClassIcon = '';
                    }
                }
            },

            /////////////////////////
            // method: createModulesTable()
            /////////////////////////
            createModulesTable: function (modules, level, parentId, parentDisplayName) {
                if (modules.length == 0)
                    return;

                level++;

                for (var i = 0; i < modules.length; i++) {
                    var item = modules[i];
                    item.lxLevel = level;
                    item.lxIsModule = (level == 1 ? true : false);
                    item.lxIsModuleFavorite = (item.Id.toString() == "0" ? true : false);

                    if (item.IsTransaction == true) {
                        if (item.Type != 4) {
                            //item.UrlRoute = "transaction-" + item.Id.toString();
                            item.UrlRoute = ChangeSpecialCaracters(parentDisplayName + item.FriendlyUrl);
                        }
                    }
                    else {
                        //item.UrlRoute = "menu-" + item.Id.toString();
                        item.UrlRoute = ChangeSpecialCaracters((parentDisplayName == '' && !managerAuth.isLoginPOSUXMode ? managerAuth.getUrlTcsAplicativo(item.IdTcsAplicativo) + '/' : parentDisplayName) + item.FriendlyUrl);
                    }

                    if (modules[i].ClassType == null) {
                        modules[i].ClassType = '';
                    }

                    if (modules[i].ClassBackground == null) {
                        modules[i].ClassBackground = '';
                    }

                    if (modules[i].ClassSize == null) {
                        modules[i].ClassSize = '';
                    }

                    if (modules[i].ClassIcon == null) {
                        modules[i].ClassIcon = '';
                    }

                    if (parentId != "0") {
                        this.MODULES_PLAIN.push(item);
                    }

                    // existem menus
                    if (item.Menus.length > 0) {
                        if (item.Id.toString() == "0") {
                            item.ClassIcon = "star";
                            this.createModulesTable(item.Menus, level, item.Id.toString(), '')
                        }
                        else
                            this.createModulesTable(item.Menus, level, item.Id.toString(), item.UrlRoute + '/')
                    }
                }
            },

            /////////////////////////
            // method: buildModuleFavorites()
            /////////////////////////
            buildModuleFavorites: function () {

                var modules = $.grep(this.MODULES_PLAIN, function (element, index) { return element.Id == 0 });

                for (var ii = 0; ii < modules.length; ii++) {

                    var moduleFavorite = modules[ii];
                    var moduleFavoriteResult = [];

                    //if (moduleFavorite.Id.toString() != "0")
                    //    return;

                    for (var m = 0; m < moduleFavorite.Menus.length; m++) {
                        var menuFavorite = moduleFavorite.Menus[m];

                        // varre todos os modulos
                        for (var i = 1; i < this.MODULES_PLAIN.length; i++) {
                            var item = this.MODULES_PLAIN[i];

                            if (isNullOrEmpty(menuFavorite.Midia) && menuFavorite.Id == item.Id) {
                                //    // copia a instancia principal por causa do link recorrente
                                //    //moduleFavoriteResult.push(menu);
                                //    moduleFavorite.UrlRoute = item.UrlRoute;
                                menuFavorite.Midia = item.Midia;
                                menuFavorite.ClassIcon = item.ClassIcon;
                                menuFavorite.ClassSize = item.ClassSize;
                                menuFavorite.ClassBackground = item.ClassBackground;
                                menuFavorite.ClassType = item.ClassType;
                                menuFavorite.lxIsModule = item.lxIsModule;
                                menuFavorite.Image = item.Image;
                                //break;
                            }

                            //varre todos os menus
                            for (var y = 0; y < item.Menus.length; y++) {
                                var menu = item.Menus[y];

                                if (menuFavorite.Id == menu.Id) {
                                    // copia a instancia principal por causa do link recorrente
                                    //moduleFavoriteResult.push(menu);
                                    moduleFavorite.Menus[m] = menu;
                                    break;
                                }
                            }
                        }
                    }
                }

                //this.MODULES_PLAIN[0].Menus = moduleFavoriteResult;

            },

            ///////////////////////
            // method: loadReports()
            ///////////////////////
            loadReports: function () {
                var that = this;
                var dfd = $.Deferred();

                var cacheKey = common.getCachePrefixGlobal('API', 'LinxReportAccessReportAccess/GetTelerikReportsFullList');
                var cacheObj = $.ezstorage.get(cacheKey);

                var cacheKeyHash = common.getCachePrefixGlobal('HASH', 'LinxReportAccessReportAccess/GetTelerikReportsFullList');
                var cacheValueHash = $.ezstorage.get(cacheKeyHash);

                if (cacheValueHash == null || cacheObj == null) {
                    system.log('Main: Loading Reports...');

                    // dados vazio
                    if (cacheObj == null)
                        cacheValueHash = null;
                    else
                        // contem dados mas o "hash" expirou, forca chamar a api novamente
                        cacheValueHash = cacheObj.hash;

                    return $.ajax({
                        type: 'GET',
                        message: "Buscando relatórios",
                        messageUser: "Acesso aos relatórios configurados",
                        headers: managerAuth.getHeaders(),
                        globalError: true,
                        url: managerAuth.getServiceAddress('LinxReportAccessReportAccess', 'Linx.Framework.BV') + '/GetTelerikReportsFullList?cacheHash=' + cacheValueHash,
                        dataType: 'json',
                        async: true,
                        cache: false,
                        success: function (data, textStatus, response) {
                            var cacheHeaderHash = (response.getResponseHeader('cacheHash') == null ? '' : response.getResponseHeader('cacheHash'));
                            var obj = { hash: cacheHeaderHash, value: data };

                            if (cacheHeaderHash == cacheValueHash) {
                                // conteudo vazio vindo da api
                                obj.value = cacheObj.value;
                            }

                            // armazena em cache os dados e o hash
                            $.ezstorage.set(cacheKeyHash, cacheHeaderHash, { expires: 1 })
                            $.ezstorage.set(cacheKey, obj, { expires: 90 })

                            that.REPORTS = obj.value;
                            dfd.promise();
                        }
                    });
                }
                else {
                    system.log('Main: Loading Reports... [Storage]');
                    that.REPORTS = cacheObj.value;

                    return dfd.resolve();
                }
            },

            ///////////////////////
            // method: searchReports()
            ///////////////////////
            searchReports: function (nomeRelatorio) {
                // rotas por assembly
                for (var i = 0; i < this.REPORTS.length; i++) {
                    var item = this.REPORTS[i];

                    if (item.NomeRelatorio === undefined || item.NomeRelatorio.toLowerCase() == nomeRelatorio.toLowerCase()) {
                        return item.IdRelatorio;
                    }
                }
                return null;
            },


            ///////////////////////
            // method: UIAddItemFav()
            ///////////////////////
            UIAddItemFav: function (id, uidModule, isTransaction, objRef) {
                var that = this;
                var dfd = $.Deferred();

                return $.ajax({
                    globalError: true,
                    message: "Gravando favorito",
                    messageUser: "Gravando favorito",
                    headers: managerAuth.getHeaders(),
                    type: 'post',
                    contentType: "application/json",
                    data: JSON.stringify({
                        Id: id,
                        IdModule: uidModule,
                        IsTransaction: isTransaction
                    }),
                    url: managerAuth.getServiceAddress('LinxFrameworkModulo', 'Linx.Framework.BV') + '/AddUserFavorite',
                    async: true,
                    cache: false,
                    success: function (data, textStatus, response) {
                        var cacheKey = common.getCachePrefixEnvironment('API', 'LinxFrameworkModulo/fullmodules', managerAuth.loginInfo.CacheKey);
                        var cacheKeyHash = common.getCachePrefixEnvironment('HASH', 'LinxFrameworkModulo/fullmodules', managerAuth.loginInfo.CacheKey);

                        $.sessionStorage.remove(cacheKey);
                        $.localStorage.remove(cacheKeyHash);

                        objRef.IsFavorite = true;
                        objRef.UidModule = uidModule;

                        var favorite = $.grep(that.MODULES, function (element, index) { return element.Id == 0 && element.IdTcsAmbiente == objRef.IdTcsAmbiente });

                        that.MODULES_PLAIN.push(objRef);

                        $(that).trigger('moduleChanged');




                        if (favorite.length > 0) {
                            favorite[0].Menus.push(objRef);
                        }
                        return dfd.promise();
                    }
                });

            },

            ///////////////////////
            // method: UIRemoveItemFav()
            ///////////////////////
            UIRemoveItemFav: function (id, uidModule, isTransaction, objRef) {
                var that = this;
                var dfd = $.Deferred();

                return $.ajax({
                    globalError: true,
                    message: "Gravando favorito",
                    messageUser: "Gravando favorito",
                    headers: managerAuth.getHeaders(),
                    type: 'post',
                    contentType: "application/json",
                    data: JSON.stringify({
                        Id: id,
                        IdModule: uidModule,
                        IsTransaction: isTransaction
                    }),
                    url: managerAuth.getServiceAddress('LinxFrameworkModulo', 'Linx.Framework.BV') + '/DeleteUserFavorite',
                    async: true,
                    cache: false,
                    success: function (data, textStatus, response) {
                        var cacheKey = common.getCachePrefixEnvironment('API', 'LinxFrameworkModulo/fullmodules', managerAuth.loginInfo.CacheKey);
                        var cacheKeyHash = common.getCachePrefixEnvironment('HASH', 'LinxFrameworkModulo/fullmodules', managerAuth.loginInfo.CacheKey);

                        $.sessionStorage.remove(cacheKey);
                        $.localStorage.remove(cacheKeyHash);

                        objRef.IsFavorite = false;

                        var favorite = $.grep(that.MODULES, function (element, index) { return element.Id == 0 && element.IdTcsAmbiente == objRef.IdTcsAmbiente });
                        if (favorite.length > 0) {
                            var item;

                            ko.utils.arrayForEach(favorite[0].Menus, function (obj) {
                                if (obj.Id == id) {
                                    item = obj;
                                }
                            });
                            ko.utils.arrayRemoveItem(favorite[0].Menus, item);
                        }

                        ko.utils.arrayForEach(that.MODULES, function (v) {
                            if (v.Id == id) {
                                v.IsFavorite = false;
                            }
                        });

                        //that._removeFavInModulesPlain(objRef);
                        $(that).trigger('moduleChanged');

                        return dfd.promise();
                    }
                });
            },

            ///////////////////////
            // method: LoadServices()
            ///////////////////////
            loadServices: function () {
                var that = this;
                var dfd = $.Deferred();

                var cacheKey = common.getCachePrefixEnvironment('API', 'LinxFrameworkAmbiente/GetServicoExcecaoMultiEnvironment', managerAuth.loginInfo.CacheKey);
                var cacheObj = $.ezstorage.get(cacheKey);

                var cacheKeyHash = common.getCachePrefixEnvironment('HASH', 'LinxFrameworkAmbiente/GetServicoExcecaoMultiEnvironment', managerAuth.loginInfo.CacheKey);
                var cacheValueHash = $.ezstorage.get(cacheKeyHash);

                if (cacheValueHash == null || cacheObj == null) {
                    system.log('Main: Loading Services...');

                    // dados vazio
                    if (cacheObj == null)
                        cacheValueHash = null;
                    else
                        // contem dados mas o "hash" expirou, forca chamar a api novamente
                        cacheValueHash = cacheObj.hash;

                    var environmentInfo = [];
                    for (var i = 0; i < managerAuth.loginInfo.Ambientes.length; i++) {
                        var item = managerAuth.loginInfo.Ambientes[i];
                        environmentInfo.push({ Hash: cacheValueHash, EnvironmentId: item.IdTcsAmbiente, ApplicationUid: item.UidAplicacao, CompanyUid: item.UidEmpresa, AplicativeId: item.IdTcsAplicativo });
                    }

                    return $.ajax({
                        type: 'POST',
                        message: "Buscando serviços",
                        messageUser: "Buscando serviços",
                        headers: managerAuth.getHeaders(),
                        globalError: true,
                        url: managerAuth.getServiceAddress('LinxFrameworkAmbiente', 'Linx.Framework.BV') + '/GetServicoExcecaoMultiEnvironment',
                        data: JSON.stringify(environmentInfo),
                        contentType: "application/json",
                        async: true,
                        cache: false,
                        success: function (data, textStatus, response) {
                            var cacheHeaderHash = (response.getResponseHeader('cacheHash') == null ? '' : response.getResponseHeader('cacheHash'));
                            var obj = { hash: cacheHeaderHash, value: data };

                            if (cacheHeaderHash == cacheValueHash) {
                                // conteudo vazio vindo da api
                                obj.value = cacheObj.value;
                            }

                            // armazena em cache os dados e o hash
                            $.ezstorage.set(cacheKeyHash, cacheHeaderHash, { expires: 1 })
                            $.ezstorage.set(cacheKey, obj, { expires: 90 })

                            that.SERVICES = obj.value;
                            dfd.promise();
                        }
                    });
                }
                else {
                    system.log('Main: Loading Services... [Storage]');
                    that.SERVICES = cacheObj.value;
                    return dfd.resolve();
                }
            },

            ///////////////////////
            // method: getAllGridLayouts()
            ///////////////////////
            getAllGridLayouts: function (moduleId, gridName) {
                var dfd = $.Deferred();

                //No FrameworkBV
                //if (managerAuth.isShellDevMode) {
                //    return dfd.resolve([]);
                //}

                return $.ajax({
                    type: 'GET',
                    message: "Buscando layouts grid",
                    messageUser: "Buscando layouts salvos",
                    headers: managerAuth.getHeaders(),
                    globalError: true,
                    url: managerAuth.getServiceAddress('LinxFrameworkObjeto', 'Linx.Framework.BV') + '/GetAllLayoutGenericos?modulo=' + moduleId + '&nomeObjeto=' + gridName,
                    contentType: "application/json",
                    async: true,
                    cache: false,
                    success: function (data, textStatus, response) {
                        dfd.resolve(data);
                    }
                });
            },

            ///////////////////////
            // method: getGridLayout(idLayout)
            ///////////////////////
            getGridLayout: function (idLayout) {
                var dfd = $.Deferred();

                return $.ajax({
                    type: 'GET',
                    message: "Buscando layouts grid",
                    messageUser: "Buscando layouts salvos",
                    headers: managerAuth.getHeaders(),
                    globalError: true,
                    url: managerAuth.getServiceAddress('LinxFrameworkObjeto', 'Linx.Framework.BV') + '/GetLayoutGenerico?idLayout=' + idLayout,
                    contentType: "application/json",
                    async: true,
                    cache: false,
                    success: function (data, textStatus, response) {
                        dfd.resolve(data);
                    }
                });
            },

            ///////////////////////
            // method: getLayoutPadrao()
            ///////////////////////
            getLayoutPadrao: function (moduleId, layoutName) {
                var dfd = $.Deferred();

                return $.ajax({
                    type: 'GET',
                    message: "Buscando layout Padrão",
                    messageUser: "Buscando layout Padrão",
                    headers: managerAuth.getHeaders(),
                    globalError: true,
                    url: managerAuth.getServiceAddress('LinxFrameworkObjeto', 'Linx.Framework.BV') + '/GetLayoutPadrao?modulo=' + moduleId + '&nomeObjeto=' + layoutName,
                    contentType: "application/json",
                    async: true,
                    cache: false,
                    success: function (data, textStatus, response) {
                        dfd.resolve(data);
                    }
                });
            },

            ///////////////////////
            // method: saveGridLayout()
            ///////////////////////
            saveGridLayout: function (layoutGridInfo) {
                var dfd = $.Deferred();
                return $.ajax({
                    type: 'POST',
                    message: "Salvando layout",
                    messageUser: "Salvando o layout selecionado",
                    headers: managerAuth.getHeaders(),
                    globalError: false,
                    url: managerAuth.getServiceAddress('LinxFrameworkObjeto', 'Linx.Framework.BV') + '/SaveLayoutGenerico',
                    data: JSON.stringify(layoutGridInfo),
                    contentType: "application/json",
                    async: true,
                    cache: false,
                    error: function (jqXHR, textStatus, errorThrown) {
                        dfd.reject(jqXHR, textStatus, errorThrown);
                    },
                    success: function (data, textStatus, response) {
                        dfd.resolve(data);
                    }
                });
            },

            ///////////////////////
            // method: deleteGridLayout()
            ///////////////////////
            deleteGridLayout: function (idLayout, moduleId, gridName) {
                var dfd = $.Deferred();
                return $.ajax({
                    type: 'DELETE',
                    message: "Removendo layout grid",
                    messageUser: "Removendo o layout selecionado",
                    headers: managerAuth.getHeaders(),
                    globalError: true,
                    url: managerAuth.getServiceAddress('LinxFrameworkObjeto', 'Linx.Framework.BV') + '/deleteLayoutGenerico?IdLayout=' + idLayout + '&modulo=' + moduleId + '&nomeObjeto=' + gridName,
                    contentType: "application/json",
                    async: true,
                    cache: false,
                    success: function (data, textStatus, response) {
                        dfd.resolve();
                    }
                });
            },

            getAllUserPermission: function (idObjetoConteudo) {
                var dfd = $.Deferred();

                if (isNullOrEmpty(idObjetoConteudo) || idObjetoConteudo < 0)
                    idObjetoConteudo = 0;

                return $.ajax({
                    async: false,
                    cache: false,
                    type: 'GET',
                    dataType: 'json',
                    data: "idObjetoConteudo=" + idObjetoConteudo,
                    headers: managerAuth.getHeaders(),
                    globalError: true,
                    contentType: 'application/json; charset=UTF-8',
                    url: managerAuth.getServiceAddress('LinxFrameworkObjeto', 'Linx.Framework.BV') + '/GetUsersPermissionLayout',
                    success: function (data, textStatus, response) {                      
                        dfd.resolve(data);
                    }
                });
            },

            getAllProfiles: function (idObjetoConteudo) {
                var dfd = $.Deferred();

                if (isNullOrEmpty(idObjetoConteudo) || idObjetoConteudo < 0)
                    idObjetoConteudo = 0;

                return $.ajax({
                    async: false,
                    cache: false,
                    type: 'GET',
                    dataType: 'json',
                    data: "idObjetoConteudo=" + idObjetoConteudo,
                    headers: managerAuth.getHeaders(),
                    globalError: true,
                    contentType: 'application/json; charset=UTF-8',
                    url: managerAuth.getServiceAddress('LinxFrameworkObjeto', 'Linx.Framework.BV') + '/GetProfilesPermissionLayout',
                    success: function (data, textStatus, response) {
                        dfd.resolve(data);
                    }
                });
            }
        };
    });
