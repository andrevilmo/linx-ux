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
            USER_NAME: managerAuth.apelido,
            MODULES: [],
            MODULES_PLAIN: [],
            REPORTS: [],

           
            ///////////////////////
            // method: loadModules()
            ///////////////////////
            loadModules: function () {
                var that = this;
                var dfd = $.Deferred();

                var cacheKey = common.getCachePrefix('API', 'LinxFrameworkModulo/fullmodules');
                var cacheObj = $.ezstorage.get(cacheKey);

                var cacheKeyHash = common.getCachePrefix('HASH', 'LinxFrameworkModulo/fullmodules');
                var cacheValueHash = $.ezstorage.get(cacheKeyHash);

                if (cacheValueHash == null || cacheObj == null) {
                    system.log('Main: Loading Modules and Menus...');

                    // dados vazio
                    if (cacheObj == null)
                        cacheValueHash = null;
                    else
                        // contem dados mas o "hash" expirou, forca chamar a api novamente
                        cacheValueHash = cacheObj.hash;

                    return $.ajax({
                        type: 'GET',
                        message: "buscando modulos...",
                        messageUser: "Acesso aos módulos/menus/transações configurados",
                        globalError: true,
                        url: managerAuth.getServiceAddress('LinxFrameworkModulo/fullmodules?cacheHash=' + cacheValueHash),
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

                            //that.configureClassType(obj.value);
                            that.createBreadCrumb(obj.value, null, null, '');
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
            createBreadCrumb: function (modules, parentBreadCrumb, parentModule, parentDisplayName) {
                if (modules.length == 0)
                    return;

                for (var i = 0; i < modules.length; i++) {
                    var item = modules[i];

                    // tratammento BreadCrumb
                    if (modules[i].BreadCrumb == null) {
                        modules[i].BreadCrumb = new Array();

                        if (parentBreadCrumb == null) {
                            parentBreadCrumb = new Array(new BreadCrumbItem({
                                order: 0,
                                moduleKey: '',
                                displayName: 'Módulos',
                                urlRoute: ''
                            }));
                        }

                        //// adiciona os itens ja existentes
                        var y = 0;
                        for (y = 0; y < parentBreadCrumb.length; y++) {
                            modules[i].BreadCrumb.push(parentBreadCrumb[y])
                        }

                        // adiciona o item atual
                        if (parentModule != null) {
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
                        this.createBreadCrumb(item.Menus, modules[i].BreadCrumb, modules[i], (parentDisplayName + '/' + modules[i].FriendlyUrl))
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
                    item.lxIsModuleFavorite = (item.Id.toString() == "00000000-0000-0000-0000-000000000000" ? true : false);

                    if (item.IsTransaction == true) {
                        if (item.Type != 4) {
                            //item.UrlRoute = "transaction-" + item.Id.toString();
                            item.UrlRoute = ChangeSpecialCaracters(parentDisplayName + item.FriendlyUrl);
                        }
                    }
                    else {
                        //item.UrlRoute = "menu-" + item.Id.toString();
                        item.UrlRoute = ChangeSpecialCaracters(parentDisplayName + item.FriendlyUrl);
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

                    if (parentId != "00000000-0000-0000-0000-000000000000") {
                        this.MODULES_PLAIN.push(item);
                    }

                    // existem menus
                    if (item.Menus.length > 0) {
                        if (item.Id.toString() == "00000000-0000-0000-0000-000000000000")
                            this.createModulesTable(item.Menus, level, item.Id.toString(), '')
                        else
                            this.createModulesTable(item.Menus, level, item.Id.toString(), item.UrlRoute + '/')
                    }
                }
            },

            /////////////////////////
            // method: buildModuleFavorites()
            /////////////////////////
            buildModuleFavorites: function () {
                var moduleFavorite = this.MODULES_PLAIN[0];
                var moduleFavoriteResult = [];

                if (moduleFavorite.Id.toString() != "00000000-0000-0000-0000-000000000000")
                    return;

                for (var m = 0; m < moduleFavorite.Menus.length; m++) {
                    var menuFavorite = moduleFavorite.Menus[m];

                    // varre todos os modulos
                    for (var i = 1; i < this.MODULES_PLAIN.length; i++) {
                        var item = this.MODULES_PLAIN[i];

                        if (isNullOrEmpty(menuFavorite.Midia) && menuFavorite.Id.toLowerCase() == item.Id.toLowerCase()) {
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

                            if (menuFavorite.Id.toLowerCase() == menu.Id.toLowerCase())
                            {
                                // copia a instancia principal por causa do link recorrente
                                //moduleFavoriteResult.push(menu);
                                moduleFavorite.Menus[m] = menu;
                                break;
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
                        message: "buscando relatórios...",
                        messageUser: "Acesso aos relatórios configurados",
                        globalError: true,
                        url: managerAuth.getServiceAddress('LinxReportAccessReportAccess/GetTelerikReportsFullList?cacheHash=' + cacheValueHash),
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
                    message: "gravando favorito...",
                    messageUser: "Gravando favorito",

                    type: 'post',
                    contentType: "application/json",
                    data: JSON.stringify({
                        Id: id,
                        UidModule: uidModule,
                        IsTransaction: isTransaction
                    }),
                    url: managerAuth.getServiceAddress('LinxFrameworkModulo/AddUserFavorite'),
                    async: true,
                    cache: false,
                    success: function (data, textStatus, response) {
                        var cacheKey = common.getCachePrefix('API', 'LinxFrameworkModulo/fullmodules');;
                        var cacheKeyHash = common.getCachePrefix('HASH', 'LinxFrameworkModulo/fullmodules');

                        $.sessionStorage.remove(cacheKey);
                        $.localStorage.remove(cacheKeyHash);

                        objRef.IsFavorite = true;
                        objRef.UidModule = uidModule;

                        that.MODULES[0].Menus.push(objRef);
                       
                        that.MODULES_PLAIN.push(objRef);

                        $(that).trigger('moduleChanged'); 
                        
                        
                        

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
                    message: "gravando favorito...",
                    messageUser: "Gravando favorito",

                    type: 'post',
                    contentType: "application/json",
                    data: JSON.stringify({
                        Id: id,
                        UidModule: uidModule,
                        IsTransaction: isTransaction
                    }),
                    url: managerAuth.getServiceAddress('LinxFrameworkModulo/DeleteUserFavorite'),
                    async: true,
                    cache: false,
                    success: function (data, textStatus, response) {
                        var cacheKey = common.getCachePrefix('API', 'LinxFrameworkModulo/fullmodules');
                        var cacheKeyHash = common.getCachePrefix('HASH', 'LinxFrameworkModulo/fullmodules');

                        $.sessionStorage.remove(cacheKey);
                        $.localStorage.remove(cacheKeyHash);

                        objRef.IsFavorite = false;

                        var item;
                        ko.utils.arrayForEach(that.MODULES[0].Menus, function (obj) {
                            if (obj.Id == id) {
                                item = obj;
                            }
                        });
                        ko.utils.arrayRemoveItem(that.MODULES[0].Menus, item);


                        ko.utils.arrayForEach(that.MODULES, function (v) {
                            if (v.Id == id) {
                                v.IsFavorite = false;
                            }
                        });

                        that._removeFavInModulesPlain(objRef);
                        $(that).trigger('moduleChanged');

                        return dfd.promise();
                    }
                });
            },

            _removeFavInModulesPlain: function (id) {
                var item;
                ko.utils.arrayForEach(this.MODULES_PLAIN, function (obj) {
                    if (obj.Id == id && obj.IsFavorite) {
                        item = obj;
                    }
                });
                ko.utils.arrayRemoveItem(this.MODULES_PLAIN, item);
                ko.utils.arrayForEach(this.MODULES_PLAIN, function (v) {
                    if (v.Id == id) {
                        v.IsFavorite = false;
                    }
                });
            }

        };
    });
