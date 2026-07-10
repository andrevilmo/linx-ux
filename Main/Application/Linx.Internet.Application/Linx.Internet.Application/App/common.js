define(['managers/__auth', 'services/CustomSearchDataDomains', 'plugins/router', 'managers/predefinedFilters'], function (managerAuth, customSearchDomain, router, managerPredefined) {
    return {
        ///////////////////////
        // method: getUrlNoImage()
        ///////////////////////
        getUrlNoImage: function () {
            return managerAuth.buildUrl('lib/linx/img/no-image.png');
        },

        ///////////////////////
        // method: getUrlLoadingImage()
        ///////////////////////
        getUrlLoadingImage: function () {
            return managerAuth.buildUrl('lib/metronic/img/loading-spinner-grey.gif');
        },

        processStack: {},

        ///////////////////////
        // method: showProcess()
        ///////////////////////
        showProcess: function (name) {

            if (this.processStack[name] && this.processStack[name] >= 0)
                this.processStack[name]++;
            else
                this.processStack[name] = 1;

            if (this.processStack[name] > 1)
                return;

            if (isNullOrEmpty(name) == true) {
                $.blockUI({
                    message: '<div><img src="' + managerAuth.buildRootUrl('lib/metronic/img/loading-spinner-grey.gif?' + managerAuth.META_HASH) + '"/></div>',
                    css: {
                        border: 'none',
                        backgroundColor: 'transparent',
                    },
                    overlayCSS: { opacity: .2 },
                    baseZ: 10001
                });
            }
            else {
                $(name).block({
                    message: '<div><img src="' + managerAuth.buildRootUrl('lib/metronic/img/loading-spinner-grey.gif?' + managerAuth.META_HASH) + '"/></div>',
                    css: {
                        border: 'none',
                        backgroundColor: 'transparent',
                    },
                    overlayCSS: { opacity: .2 },
                    baseZ: 9999
                });
            }
        },

        showProcessFull: function (message) {
            if (!message) {
                message = '';
            }

            $.blockUI({
                message: message,
                css: {
                    border: 'none',
                    backgroundColor: 'transparent',
                },
                overlayCSS: { opacity: .2 },
                baseZ: 10001
            });
        },

        ///////////////////////
        // method: closeProcess()
        ///////////////////////
        closeProcess: function (name) {

            if (this.processStack[name] && this.processStack[name] > 0)
                this.processStack[name]--;
            else
                this.processStack[name] = 0;

            if (this.processStack[name] > 0)
                return;

            if (isNullOrEmpty(name) == true) {
                $.unblockUI();
            }
            else {
                $(name).unblock()
            }
        },

        ///////////////////////
        // method: showProcess()
        ///////////////////////
        showModalReport: function (name, canGoBack, urlSrc) {
            $(name).fancybox({
                //parent: '#applicationHost',
                //maxWidth: 800,
                //maxHeight: 600,
                href: urlSrc,
                fitToView: false,
                width: '95%',
                height: '95%',
                autoSize: false,
                openEffect: 'none',
                closeEffect: 'none',
                scrollOutside: 'false',
                iframe: {
                    preload: true,
                    scrolling: 'false'
                },
                tpl: {
                    closeBtn: '<a title="Close" class="fancybox-item fancybox-close" href="javascript:;" onclick="jQuery.fancybox.close()"></a>',
                },
                beforeLoad: function () {
                    $("body").css("overflow", "")
                    return;
                },
                afterShow: function () {
                    $(".fancybox-inner").css("overflow", "")
                },
                afterClose: function () {
                    $("body").css("overflow", "auto");

                    if (canGoBack == true)
                        window.history.back();
                    return;
                }
            });
        },

        ///////////////////////
        // method: formatFileSize()
        ///////////////////////
        formatFileSize: function (bytes) {
            if (typeof bytes !== 'number') {
                return '';
            }
            if (bytes >= 1000000000) {
                return (bytes / 1000000000).toFixed(2) + ' GB';
            }
            if (bytes >= 1000000) {
                return (bytes / 1000000).toFixed(2) + ' MB';
            }
            return (bytes / 1000).toFixed(2) + ' KB';
        },


        ///////////////////////
        // method: getCachePrefix() 
        ///////////////////////
        getCachePrefix: function (typeKey, key) {
            return managerAuth.META_ROOT + managerAuth.META_MODULE_ID + '__' + managerAuth.getEnvironmentId() + "__" + managerAuth.loginInfo.UidUsuario + '__' + typeKey + '__' + key;
        },

        ///////////////////////
        // method: getCachePrefixEnvironment() 
        ///////////////////////
        getCachePrefixEnvironment: function (typeKey, key, environmentId) {
            return managerAuth.META_ROOT + managerAuth.META_MODULE_ID + '__' + environmentId + "__" + managerAuth.loginInfo.UidUsuario + '__' + typeKey + '__' + key;
        },

        ///////////////////////
        // method: getCachePrefixGlobal() 
        ///////////////////////
        getCachePrefixGlobal: function (typeKey, key) {
            return managerAuth.META_ROOT + managerAuth.META_MODULE_ID + '__' + typeKey + '__' + key;
        },

        ///////////////////////
        // method: getGridMode() 
        //  F = formulario, G = Grid
        ///////////////////////
        getGridMode: function () {
            if (managerAuth.isShellDevMode == true || managerAuth.isShellSetupMode == true) {
                var cacheObj = $.ezstorage.get(this.getCachePrefix('CONFIG', 'chkGridMode'));

                if (cacheObj == null)
                    return 'F'; // default

                var value = false

                if (((typeof cacheObj) === 'boolean'))
                    value = cacheObj;
                else {
                    value = cacheObj.bool();
                }

                if (value == true)
                    return 'G';
                else
                    return 'F';
            }
            else {
                var parameterValue = managerAuth.getParameter("SHELL_FLAG_RESULTADO_TABULAR");
                if (isNull(parameterValue)) {
                    console.warn("Parametro 'SHELL_FLAG_RESULTADO_TABULAR' não cadastrado!")
                    return 'F'; // default
                }

                if (parameterValue.bool() == true)
                    return 'G';
                else
                    return 'F';
            }
        },

        ///////////////////////
        // method: saveGridMode() 
        //  F = formulario, G = Grid
        ///////////////////////
        saveGridMode: function (value) {
            if (managerAuth.isShellDevMode == true || managerAuth.isShellSetupMode == true) {
                $.ezstorage.set(
                    this.getCachePrefix('CONFIG', 'chkGridMode'),
                    value,
                    { persist: true }
                )
            }
            else {
                this.saveParameter("SHELL_FLAG_RESULTADO_TABULAR", "TCS_USUARIO", managerAuth.loginInfo.UidUsuario, value.toString(), managerAuth.loginInfo.IdTcsAmbienteDefault);
                managerAuth.setParameter("SHELL_FLAG_RESULTADO_TABULAR", value.toString());
            }
        },

        ///////////////////////
        // method: getLastFilterMode() 
        //  true = ligado, false = desligado
        ///////////////////////
        getLastFilterMode: function () {
            if (managerAuth.isShellDevMode == true || managerAuth.isShellSetupMode == true) {
                var cacheObj = $.ezstorage.get(this.getCachePrefix('CONFIG', 'chkLastFilterMode'));

                if (cacheObj == null)
                    return true; // default

                return cacheObj;
            }
            else {
                var parameterValue = managerAuth.getParameter("SHELL_FLAG_ULTIMO_FILTRO");
                if (isNull(parameterValue)) {
                    console.warn("Parametro 'SHELL_FLAG_ULTIMO_FILTRO' não cadastrado!")
                    return false;
                }

                return parameterValue.bool();
            }
        },

        ///////////////////////
        // method: saveLastFilterMode() 
        //  true = ligado, false = desligado
        ///////////////////////
        saveLastFilterMode: function (value) {
            if (managerAuth.isShellDevMode == true || managerAuth.isShellSetupMode == true) {
                $.ezstorage.set(
                    this.getCachePrefix('CONFIG', 'chkLastFilterMode'),
                    value,
                    { persist: true }
                )
            }
            else {
                this.saveParameter("SHELL_FLAG_ULTIMO_FILTRO", "TCS_USUARIO", managerAuth.loginInfo.UidUsuario, value.toString(), managerAuth.loginInfo.IdTcsAmbienteDefault);
                managerAuth.setParameter("SHELL_FLAG_ULTIMO_FILTRO", value);
            }
        },


        ///////////////////////
        // method: getHideWizard() 
        //  true = escondido, false = visivel
        ///////////////////////
        getHideWizards: function () {
            //if (managerAuth.isShellDevMode == true || managerAuth.isShellSetupMode == true) {
            var cacheObj = $.ezstorage.get(this.getCachePrefix('CONFIG', 'chkHideWizards'));

            if (cacheObj == null)
                return false; // default

            return cacheObj;
            //}
            //else {
            //    var parameterValue = managerAuth.getParameter("SHELL_FLAG_ESCONDE_ASSISTENTES");
            //    if (isNull(parameterValue)) {
            //        console.warn("Parametro 'SHELL_FLAG_ESCONDE_ASSISTENTES' não cadastrado!")
            //        return false;
            //    }

            //    return parameterValue.bool();
            //}
        },

        ///////////////////////
        // method: saveLastFilterMode() 
        //  true = escondido, false = visivel
        ///////////////////////
        saveHideWizards: function (value) {
            //if (managerAuth.isShellDevMode == true || managerAuth.isShellSetupMode == true) {
            $.ezstorage.set(
                this.getCachePrefix('CONFIG', 'chkHideWizards'),
                value,
                { persist: true }
            );
            //}
            //else {
            //    this.saveParameter("SHELL_FLAG_ESCONDE_ASSISTENTES", "TCS_USUARIO", managerAuth.loginInfo.UidUsuario, value.toString(), managerAuth.loginInfo.IdTcsAmbienteDefault);
            //    managerAuth.setParameter("SHELL_FLAG_ESCONDE_ASSISTENTES", value);
            //}
        },

        ///////////////////////
        // method: getIdioma()
        // retorna a sigla do idioma
        ///////////////////////
        getIdioma: function () {
            var cacheObj = $.ezstorage.get(this.getCachePrefix('CONFIG', 'cmbIdioma'));

            if (cacheObj == null)
                return "pt-br"; // default

            return cacheObj;
        },

        ///////////////////////
        // method: saveIdioma()
        // salva a sigla do idioma
        ///////////////////////
        saveIdioma: function (value) {
            $.ezstorage.set(
                this.getCachePrefix('CONFIG', 'cmbIdioma'),
                value,
                { persist: true }
            );
        },


        ///////////////////////
        // method: getBarraNavegacao() 
        //  true = ligado, false = desligado
        ///////////////////////
        getBarraNavegacao: function () {

            return true;
            //if (managerAuth.isShellDevMode == true || managerAuth.isShellSetupMode == true) {
            //    var cacheObj = $.ezstorage.get(this.getCachePrefix('CONFIG', 'chkBarraNavegacao'));

            //    if (cacheObj == null)
            //        return true; // default

            //    return cacheObj;
            //}
            //else {
            //    var parameterValue = managerAuth.getParameter("SHELL_FLAG_BARRA_NAVEGACAO");
            //    if (isNull(parameterValue)) {
            //        console.warn("Parametro 'SHELL_FLAG_BARRA_NAVEGACAO' nao cadastrado!")
            //        return true;
            //    }

            //    return parameterValue.bool();
            //}
        },

        ///////////////////////
        // method: saveLastFilterMode() 
        //  true = ligado, false = desligado
        ///////////////////////
        saveBarraNavegacao: function (value) {
            if (managerAuth.isShellDevMode == true || managerAuth.isShellSetupMode == true) {
                $.ezstorage.set(
                    this.getCachePrefix('CONFIG', 'chkBarraNavegacao'),
                    value,
                    { persist: true }
                )
            }
            else {
                this.saveParameter("SHELL_FLAG_BARRA_NAVEGACAO", "TCS_USUARIO", managerAuth.loginInfo.UidUsuario, value.toString(), managerAuth.loginInfo.IdTcsAmbienteDefault);
                managerAuth.setParameter("SHELL_FLAG_BARRA_NAVEGACAO", value.toString());
            }
        },

        ///////////////////////
        // method: restoreTheme() 
        ///////////////////////
        restoreTheme: function () {
            var color = $.cookie('style_color');

            if (isNullOrEmpty(color)) {
                var parameterValue = managerAuth.getParameter("SHELL_NOME_TEMA");
                color = convertToString(parameterValue);

                if (color == '')
                    color = 'default';

                this.saveTheme(color);
            }
            else {
                // forca o tema default
                if (color != 'default')
                    this.saveTheme('default');
            }


        },

        ///////////////////////
        // method: saveTheme() 
        // color:  default, orange, black
        ///////////////////////
        saveTheme: function (color) {
            var path = $('meta[name=linx-internet-application-root]').attr("content") + $('meta[name=linx-internet-application-module-id]').attr("content") + "/lib/";
            color = color.toLowerCase();

            // troca o tema no html
            $('#style_color').attr("href", path + "theme-css-" + color + ".css");

            // grava no cookie
            $.cookie('style_color', color);

            // grava como parametro no modo prod
            if (managerAuth.isShellProdMode == true) {
                this.saveParameter("SHELL_NOME_TEMA", "TCS_USUARIO", managerAuth.loginInfo.UidUsuario, color, managerAuth.loginInfo.IdTcsAmbienteDefault);
                managerAuth.setParameter("SHELL_NOME_TEMA", color);
            }
        },

        ///////////////////////
        // method: getStartPage() 
        ///////////////////////
        getStartPage: function () {

            //if (managerAuth.transaction.length > 0)
            //    return "#" + managerAuth.transaction

            var value = managerAuth.getParameter("SHELL_URL_INICIAL");

            if (isNullOrEmpty(value))
                return ''
            else
                return value
        },

        ///////////////////////
        // method: saveStartPage() 
        ///////////////////////
        saveStartPage: function (value) {
            var dfd = $.Deferred();

            return this.saveParameter("SHELL_URL_INICIAL", "TCS_USUARIO", managerAuth.loginInfo.UidUsuario, value, managerAuth.loginInfo.IdTcsAmbienteDefault).then(function () {
                managerAuth.setParameter("SHELL_URL_INICIAL", value);
                router.trigger('saveStartPage:changed', value)
                dfd.resolve();
            });
        },

        ///////////////////////
        // method: saveParameter()
        ///////////////////////
        saveParameter: function (nameParameter, tableName, tableValue, valueParameter, idTcsAmbiente) {

            if (managerAuth.isLoginPOSUXMode) {
                console.log('saveParameter - LoginPOSUX')
                return;
            }

            var self = this;
            var dfd = $.Deferred();

            return $.ajax({
                globalError: true,
                message: "Gravando configuracão",
                messageUser: "Gravando configuracão",
                headers: managerAuth.getHeaders(idTcsAmbiente),
                type: 'post',
                contentType: "application/json",
                data: JSON.stringify({
                    TituloParametro: nameParameter,
                    NomeTabela: tableName,
                    ChaveVariacao: tableValue,
                    ValorVariacao: valueParameter
                }),
                url: managerAuth.getServiceAddress('LinxFrameworkParametro', 'Linx.Framework.BV') + '/SetParameterValue',
                async: true,
                cache: false,
                success: function (data, textStatus, response) {
                    var cacheKey = self.getCachePrefixEnvironment('API', 'GetParameterValue', managerAuth.loginInfo.CacheKey);

                    $.sessionStorage.remove(cacheKey);
                    $.localStorage.remove(cacheKey);

                    dfd.promise();
                }
            });
        },

        translateSearch: function (dataContext, lastJEntitySearch, app) {
            var translatedSearch = "";
            var entitySearch = lastJEntitySearch;

            var predefinedFilters = managerPredefined.predefinedFilters;

            while (!isNullOrEmpty(entitySearch)) {
                var hasCondition = false;
                var searchLine = strLeft(entitySearch, entitySearch.indexOf("}") + 1)
                var entity = strLeft(searchLine, searchLine.indexOf("{"));
                var conditions = (entity == 'LinqValidProperties' ? '' : strExtract(searchLine, "{", "}").split(";"));
                var search = "";

                for (var i = 0; i < conditions.length; i++) {
                    var item = conditions[i];
                    if (!isNullOrEmpty(item) && item.indexOf("#") >= 0) {
                        var condition = strLeft(item, item.indexOf("#"));
                        if (jQuery.inArray(condition, ["&&", "||", "(", ")"]) > 0) {
                            item = item.replace(condition + '#', '');
                        }
                        else
                            condition = "&&";

                        var column = strLeft(item, item.indexOf("#"));
                        var columnInfo = this.getColumnInfo(dataContext, entity, column);
                        var columnName = columnInfo[0] == "" ? column : columnInfo[0];
                        var operator = strExtract(item, "#", "#");
                        var operatorName = customSearchDomain.getName('FilterOperator', operator);
                        var value = item.slice(item.lastIndexOf("#") + 2).replace("}", "");

                        try { value = decode(value) }
                        catch (e) { }

                        switch (columnInfo[1]) {
                            case 'bool':
                                value = value == 'true' ? 'verdadeiro' : 'falso';
                                break;

                            case 'date':
                                if (value.startsWith('$')) {
                                    var filter = strExtract(value, '$', '$');
                                    var filterValue = value.slice(value.lastIndexOf('$') + 1);
                                    var predefinedItem = $.grep(predefinedFilters, function (element, index) { return element.id == filter });
                                    if (predefinedItem.count() > 0) {
                                        value = predefinedItem[0].text.replace('(x)', filterValue);
                                    }
                                }
                                else {
                                    value = new Date(parseDate(value).getTime() - parseDate(value).getTimezoneOffset() * 60000).toLocaleString();
                                }
                                break;
                            default:

                        }

                        if (columnInfo[2]) {
                            value = dataContext.dataDomains.getName(columnInfo[3], value)
                        }

                        if (operator.toUpperCase() === 'IN')
                            value = "(" + value + ")";

                        search = search + (!isNullOrEmpty(search) ? this.getTranslatedCondition(condition) : "") + '[' + columnName + '] ' + operatorName + ' ' + value;
                    }
                }
                entitySearch = entitySearch.replace(searchLine, "");

                if (!isNullOrEmpty(search))
                    translatedSearch = translatedSearch + (!isNullOrEmpty(translatedSearch) ? "\ne\n" : "") + /*entity + ' onde:\n\n' +*/ '(' + search + ')';
            }

            if (app) {
                if (isNullOrEmpty(translatedSearch)) {
                    translatedSearch = "Pesquisa sem filtros.";
                }
                app.showMessage(translatedSearch, 'Filtros da pesquisa');
            }
            return translatedSearch;
        },

        getColumnInfo: function (dataContext, entityName, columnName) {
            var info = [];
            info[0] = "";
            info[1] = "";
            info[2] = false;
            info[3] = "";

            if (dataContext.metadataInfo[entityName] != undefined) {
                for (var i = 0; i < dataContext.metadataInfo[entityName].count() ; i++) {
                    if (dataContext.metadataInfo[entityName][i].key === columnName || dataContext.metadataInfo[entityName][i].key === columnName + 'Name') {
                        info[0] = dataContext.metadataInfo[entityName][i].headerText;
                        info[1] = dataContext.metadataInfo[entityName][i].dataType;
                        info[2] = dataContext.metadataInfo[entityName][i].isDomain;
                        info[3] = dataContext.metadataInfo[entityName][i].domainName;
                        break;
                    }
                }
            }
            return info;
        },

        getTranslatedCondition: function (condition) {
            switch (condition.trim()) {
                case "(":
                    return " ( ";
                    break;

                case ")":
                    return " ) ";
                    break;

                case "&&":
                    return " e ";
                    break

                case "||":
                    return " ou ";
                    break;
            }

            return "";
        },

        getTransactionCode: function () {
            if (managerAuth.isShellDevMode == true || managerAuth.isShellSetupMode == true) {
                return '';
            }

            var code = router.activeInstruction().config.currentData ? router.activeInstruction().config.currentData.TransactionCode : null;

            if (isNullOrEmpty(code))
                return ''
            else
                return code.trim();
        },

        setWindowMessage: function (enable) {
            if (managerAuth.isShellDevMode == true || managerAuth.isShellSetupMode == true) {
                return;
            }

            if (enable == true) {
                window.onbeforeunload = function () {
                    if (window.ignoreCloseConfirmation) {
                        window.ignoreCloseConfirmation = null;
                        return null;
                    }
                    else {
                        return document.title;
                    }
                };

                this.restoreTheme();
            }
            else {
                window.onbeforeunload = null;
            }

        },

        go: function (hash, queryString) {
            var url = '';

            if (managerAuth.isShellDevMode == true || managerAuth.isShellSetupMode == true) {
                url = hash;
            }
            else {
                for (var r = 0; r < router.routes.length; r++) {
                    var currentRoute = router.routes[r];

                    if (isNullOrEmpty(currentRoute.lxHash) == false) {
                        if (currentRoute.lxHash.toLowerCase() == hash.toLowerCase()) {
                            url = currentRoute.hash;
                            break;
                        }
                    }

                }
            }


            if (isNullOrEmpty(queryString) == false) {
                url += '?' + queryString;
            }
            router.navigate(url)
        },

        getExceptionDescription: function (ex, arrayToReplace) {
            if (!Array.isArray(arrayToReplace)) arrayToReplace = [arrayToReplace];
            var msg = ex.ExceptionMessage;
            while (ex.hasOwnProperty('InnerException') && ex.InnerException != null && ex.InnerException.ExceptionMessage) {
                msg += '<br/>   ' + ex.InnerException.ExceptionMessage;
                ex = ex.InnerException;
            }
            arrayToReplace.forEach(function (s) { msg = msg.replace(s, ''); })
            return msg;
        }


    }
});
