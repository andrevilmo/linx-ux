define(['managers/__auth', 'plugins/router', 'jsSHA'], function (managerAuth, router, jsSHA) {
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

        setWindowMessage: function (enable) {
            if (managerAuth.isShellDevMode == true || managerAuth.isShellSetupMode == true) {
                return;
            }

            if (enable == true) {
                window.onbeforeunload = function () {
                    return document.title;
                };

                this.restoreTheme();
            }
            else {
                window.onbeforeunload = null;
            }

        },

        ///////////////////////
        // method: getBarraNavegacao() 
        //  true = ligado, false = desligado
        ///////////////////////
        getBarraNavegacao: function () {

            return true;
            /* REMOVIDO HENRY 15/12/2016
            if (managerAuth.isShellDevMode == true || managerAuth.isShellSetupMode == true) {
                var cacheObj = $.ezstorage.get(this.getCachePrefix('CONFIG', 'chkBarraNavegacao'));

                if (cacheObj == null)
                    return true; // default

                return cacheObj;
            }
            else {
                var parameterValue = managerAuth.getParameter("SHELL_FLAG_BARRA_NAVEGACAO");
                if (isNull(parameterValue)) {
                    console.warn("Parametro 'SHELL_FLAG_BARRA_NAVEGACAO' nao cadastrado!")
                    return true;
                }

                return parameterValue.bool();
            }*/
        },

        ///////////////////////
        // method: go()
        ///////////////////////
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


        ///////////////////////
        // method: getPinHash()
        ///////////////////////
        getPinHash: function (pin)
        {
            var shaObj = new jsSHA(pin, "TEXT");
            return shaObj.getHMAC("PinLjvVendedorPosManager", "TEXT", "SHA-1", "HEX");

            //Versão 2.0 >
            //var shaObj = new jsSHA("SHA-1", "TEXT");
            //shaObj.setHMACKey("", "TEXT");
            //shaObj.update(pin);
            //return shaObj.getHMAC("HEX");

        }


    }
});