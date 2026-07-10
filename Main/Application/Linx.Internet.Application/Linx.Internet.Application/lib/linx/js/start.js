if (window.require) {
    window.nodeRequire = window.require;
    delete window.require;
}

String.prototype.bool = function () {
    return (/^true$/i).test(this);
};

//(function ($) {
//    $.fn.oldReady = $.fn.ready;
//    $.fn.ready = function (fn) {
//        return $.fn.oldReady(function () { try { if (fn) fn.apply($, arguments); } catch (e) { } });
//    }
//})(jQuery);


var Start = function () {
    ///////////////////
    // private methods
    ///////////////////
    var getStatus = function () {
        var appCache = window.applicationCache;
        switch (appCache.status) {
            case appCache.UNCACHED: // UNCACHED == 0
                return 'UNCACHED';
                break;
            case appCache.IDLE: // IDLE == 1
                return 'IDLE';
                break;
            case appCache.CHECKING: // CHECKING == 2
                return 'CHECKING';
                break;
            case appCache.DOWNLOADING: // DOWNLOADING == 3
                return 'DOWNLOADING';
                break;
            case appCache.UPDATEREADY:  // UPDATEREADY == 4
                return 'UPDATEREADY';
                break;
            case appCache.OBSOLETE: // OBSOLETE == 5
                return 'OBSOLETE';
                break;
            default:
                return 'UKNOWN CACHE STATUS';
                break;
        };
    };

    var execRestart = function (message) {
        showMessage('versão atualizada com sucesso, reiniciando...');
        window.location.reload();
    }

    var showMessage = function (message) {
        console.log('LIA: ' + message);
        $("#divCurrentActivityInformation").text(message);
    }

    var showMessageProgress = function (e) {
        var msg = '';
        if (e.originalEvent.total) {
            var perc = Math.floor((e.originalEvent.loaded / e.originalEvent.total) * 100);
            //msg = 'downloading ' + e.originalEvent.loaded + ' / ' + e.originalEvent.total;
            msg = 'downloading ' + perc + '%';
        }
        $("#divCurrentActivityInformation").text(msg);

        //if (e.originalEvent.total == e.originalEvent.loaded) {
        //    execRestart();
        //}
    }

    // dynamically load any javascript file.
    var loadJS = function (src, callback) {
        var s = document.createElement('script');
        s.src = src;
        s.async = true;
        s.onreadystatechange = s.onload = function () {
            var state = s.readyState;
            if (!callback.done && (!state || /loaded|complete/.test(state))) {
                callback.done = true;
                callback();
            }
        };
        document.getElementsByTagName('head')[0].appendChild(s);
    }

    ///////////////////
    // public methods
    ///////////////////
    return {
        appOffLine: $('meta[name=linx-internet-application-offline]').attr("content").bool(),
        appMin: $('meta[name=linx-internet-application-min]').attr("content").bool(),
        appMetaRoot: $('meta[name=linx-internet-application-root]').attr("content"),
        appMetaModuleId: $('meta[name=linx-internet-application-root]').attr("content") + $('meta[name=linx-internet-application-module-id]').attr("content"),

        //main function to initiate the module
        init: function () {
            var appCache = window.applicationCache;
            if (!appCache) {
                console.log('LIA: offline cache is not supported.');
                Start.loadCore();
                return;
            }

            var status = getStatus();
            console.log('LIA: ' + status);
            if (status == "UNCACHED" || status == "UKNOWN CACHE STATUS") {
                Start.loadCore();
                return;
            }

            var appCache = window.applicationCache;
            $(appCache).bind('checking', function () {
                console.log('*EVENT* checking');
                showMessage('verificando versão...');
            });

            $(appCache).bind('noupdate', function () {
                console.log('*EVENT* noupdate');
                showMessage('');
                Start.loadCore();
            });

            $(appCache).bind('updateready', function () {
                // cache atualizado
                console.log('*EVENT* updateready');
                appCache.swapCache();
                execRestart();
            });

            $(appCache).bind('cached', function () {
                // primeiro cache
                console.log('*EVENT* cached');
                Start.loadCore();
            });

            $(appCache).bind('progress', function (e) {
                showMessageProgress(e);
            });

            $(appCache).bind('error', function () {
                console.log('*EVENT* onerror');
                execRestart();
            });
        },

        loadCore: function () {
            if (Start.appMin == true) {
                // download core
                loadJS(Start.appMetaModuleId + '/scripts/core.js', function () {
                    loadJS(Start.appMetaModuleId + '/lib/requirejs/require.js', function () {
                        loadJS(Start.appMetaModuleId + '/lib/linx/js/config-require.js', function () {
                            Start.goMain();
                        });
                    });
                });
            }
            else {
                loadJS(Start.appMetaModuleId + '/lib/requirejs/require.js', function () {
                    loadJS(Start.appMetaModuleId + '/lib/linx/js/config-require.js', function () {
                        Start.goMain();
                    });
                });
            }
        },

        goMain: function () {
            // config require
            require(['json!../../../routes.json', 'json!../../../config.json'], function (routesJson, configJson) {
                requirejs.routesJson = routesJson;

                var cacheObj = $.ezstorage.get('Hash_Login');
                //debugger;

                if (configJson.loginMode === 'POSUX' && !cacheObj) {
                    // limpa a url caso contenha hash, ex: #nome-tela
                    if (window.location.hash.length > 0) {
                        window.location.href = window.location.origin + window.location.pathname;
                        return;
                    }

                    requirejs.config({
                        baseUrl: $('meta[name=linx-internet-application-root]').attr("content") + $('meta[name=linx-internet-application-module-id]').attr("content") + "/AppLogin"
                    });
                }

                require(['base32'], function (base32) {

                    // inicia a aplicacao
                    require(['managers/__auth'], function (managerAuth) {

                        requirejs.config({
                            packages: routesJson.REQUIRE_PACKAGES
                        });

                        // inicia a aplicacao modo LOGIN
                        if (managerAuth.isLoginPOSUXMode) {
                            var cacheObj = $.ezstorage.get('Hash_Login');
                            if (cacheObj) {
                                var cacheValue = base32.decode(cacheObj);
                                cacheValue = cacheValue.split("||");

                                if (cacheValue.count() == 6) {
                                    managerAuth.idVendedor = cacheValue[0];
                                    managerAuth.nomeVendedor = cacheValue[1];
                                    managerAuth.idLoja = cacheValue[2];
                                    managerAuth.indicaGerente = cacheValue[3];
                                    managerAuth.indicaOperadorCaixa = cacheValue[4];
                                    managerAuth.idFilialPfj = cacheValue[5];
                                }
                            }
                            require(['main'], function () { });
                            return;
                        }

                        if (managerAuth.isShellSetupMode == true || managerAuth.isShellDevMode == true) {
                            var startUrl = configJson.startUrl; //$('meta[name=linx-internet-application-start-url]').attr("content")
                            if (window.location.hash === "" && startUrl.length > 0) {
                                var queryString = (window.location.search === '' ? '' : window.location.search)
                                window.location.href = $('meta[name=linx-internet-application-root]').attr("content") + queryString + startUrl;
                            }
                        }

                        // verifica se o usuario esta autenticado
                        if (managerAuth.isAuthenticated == false) {
                            $("#divCurrentActivityInformation").html('Usuário sem autenticação! redirecionando...');
                            //$("#divCurrentActivityInformation").html('usuario sem autenticação! <a href="' + managerAuth.portal + '">[Portal]</a>');
                            window.location.href = managerAuth.portal;
                            return;
                        }
                        else {
                            if (managerAuth.transaction.length > 0) {
                                window.location.href = $('meta[name=linx-internet-application-root]').attr("content") + "#" + managerAuth.transaction;
                            }
                        }

                        // inicia a aplicacao
                        require(['main'], function () { });
                    });
                });
            });
        }
    };
}();
//$(window).error(function (e) {
//    e.preventDefault();
//});

jQuery(document).ready(function () {
    if (Start.appOffLine == false) {
        Start.loadCore();
    }
    else {
        Start.init(); // init current page
    }
});


