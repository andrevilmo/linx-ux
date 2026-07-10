//Exposing jQuery and Knoskcout for Durandal 
define('jquery', function () { return jQuery; });
define('knockout', ko);

define(['jquery', 'knockout', 'durandal/system', 'durandal/app', 'durandal/viewLocator', 'breeze', 'services/logger', 'plugins/router', 'managers/user', 'managers/__auth', 'managers/brand', 'plugins/widget', 'managers/error', 'common', 'managers/predefinedFilters'],
    function ($, ko, system, app, viewLocator, breeze, logger, router, managerUser, managerAuth, managerBrand, widget, managerError, common, managerPredefined) {
        //>>excludeStart("build", true);
        // Enable debug message to show in the console 
        system.debug(managerAuth.isDebugMode);
        //>>excludeEnd("build");

        app.title = 'Linx UX';

        app.configurePlugins({
            router: true,
            dialog: true,
            widget: true
        });

        app.start().then(function () {
            // executa a consistencia da versao do browser
            $.reject({
                reject: {
                    all: false,
                    msie: 10,
                    chrome: 31,
                    safari: 7, // Apple Safari  
                    firefox: 28, // Mozilla Firefox  
                    opera: true, // Opera  
                    konqueror: true, // Konqueror (Linux)  
                    unknown: false // Everything else  
                },
                display: ['chrome', 'firefox', 'msie', 'safari'], // Displays only firefox, chrome, and opera
                close: false,
                imagePath: managerAuth.buildRootUrl("lib/jquery/plugins/jreject/images/"),
                browserInfo: { // Settings for which browsers to display
                    chrome: {
                        // Text below the icon
                        text: 'Google Chrome 31+',
                        // URL For icon/text link
                        url: 'http://www.google.com/chrome/'
                        // (Optional) Use "allow" to customized when to show this option
                        // Example: to show chrome only for IE users
                        // allow: { all: false, msie: true }
                    },
                    firefox: {
                        text: 'Mozilla Firefox 28+',
                        url: 'http://www.mozilla.com/firefox/'
                    },
                    safari: {
                        text: 'Safari 7+',
                        url: 'http://www.apple.com/safari/download/'
                    },
                    opera: {
                        text: 'Opera',
                        url: 'http://www.opera.com/download/'
                    },
                    msie: {
                        text: 'Internet Explorer 11+',
                        url: 'http://www.microsoft.com/windows/Internet-explorer/'
                    }
                },

                // Pop-up Window Text
                header: 'Seu browser não atende os requisitos mínimos',

                paragraph1: 'Browser instalado [' + $.browser.className + ' ' + $.browser.name + ' ' + $.browser.version + ' ' + $.browser.versionX + ' ' + $.os.name + ' ' + navigator.platform.toLowerCase() + ']',

                paragraph2: 'Basta clicar nos ícones para acessar a página de download',

                beforeReject: function () {
                },
                onFail: function () { // passou pelos requisitos de browser
                    activate();
                }

            });
        });

        function activate() {
            configError();

            $(document).ajaxStart(function () {
                $("#divCurrentActivityInformation").text("aguarde...");
            });

            $(document).ajaxSend(function (event, jqxhr, settings) {
                if (settings.message == null)
                    $("#divCurrentActivityInformation").text("aguarde...");
                else
                    $("#divCurrentActivityInformation").text(settings.message);
            });

            $(document).ajaxComplete(function () {
            });

            $(document).ajaxStop(function () {
                $("#divCurrentActivityInformation").text("");
            });

            $(document).ajaxError(function (event, jqxhr, settings, thrownError) {
                if (settings.globalError == null)
                    return;

                var msg = '';

                if (settings.messageUser != null) {
                    msg = settings.messageUser + '\r\n';
                }

                var descricao = '-';

                if (!isNullOrEmpty(jqxhr.responseJSON) && !isNullOrEmpty(jqxhr.responseJSON.ExceptionMessage)) {
                    descricao = jqxhr.responseJSON.ExceptionMessage;
                }
                else {
                    if (isNullOrEmpty(thrownError) == false) {
                        descricao = thrownError + '&nbsp;[' + jqxhr.status + ']';;
                    }
                    else {
                        if (isNullOrEmpty(jqxhr.statusText) == false) {
                            descricao = jqxhr.statusText + '&nbsp;[' + jqxhr.status + ']';
                        }
                    }
                }

                var url = (isNullOrEmpty(settings.url) == true ? ' - ' : settings.url)

                msg += '\r\n' + '<b>Descrição:</b>&nbsp;' + descricao + '\r\n';
                msg += '\r\n' + "<b>Url:</b>&nbsp;<a href='" + url + "' target='_blank'>" + url + "</a>";

                $('body').removeClass("page-quick-sidebar-open");
                $('.closeSidebarClick').hide();

                common.closeProcess('#main');
                common.closeProcess();

                app.showMessage(msg, 'Requisição inválida', ['Ok']);
            });

            // configuracao de todas as chamadas AJAX pelo breeze
            var ajaxAdapter = breeze.config.getAdapterInstance("ajax");
            ajaxAdapter.defaultSettings = {
                beforeSend: function (xhr, settings) {
                    if (settings.url.length >= 2048) {
                        xhr.abort('Foi excedido o limite de caracteres para a pesquisa!');
                        return false;
                    }

                    xhr.setRequestHeader('Application', managerAuth.applicationId);
                    xhr.setRequestHeader('CurrentCompany', managerAuth.companyId);
                    xhr.setRequestHeader('AuthorizationToken', managerAuth.tokenId);
                    xhr.setRequestHeader('CurrentUser', managerAuth.userId);
                    xhr.setRequestHeader('AccessGroup', managerAuth.accessGroupId);
                    xhr.setRequestHeader('EconomicGroup', managerAuth.economicGroupId);
                    xhr.setRequestHeader('Environment', managerAuth.environmentId);
                    xhr.setRequestHeader('SessionID', managerAuth.sessionID);

                    if (router.activeInstruction().config != null) {
                        if (router.activeInstruction().config.currentData != null) {
                            xhr.setRequestHeader('TransactionInfo', router.activeInstruction().config.currentData.Module);
                        }
                    }

                    return xhr;
                }

            };

            // configuracao de todas as chamadas AJAX pelo jquery
            $.ajaxSetup({
                headers: managerAuth.getHeaders(),
                beforeSend: function (xhr, settings) {
                    if (settings.url.length >= 2048) {
                        xhr.abort('Foi excedido o limite de caracteres para a pesquisa!');
                        return false;
                    }

                    return xhr;
                }

            });

            if (!widget.moduleStarted) {
                var oldconvertKindToModulePath = widget.convertKindToModulePath;
                widget.convertKindToModulePath = function (kind) {
                    if (kind.indexOf('pkg_') === 0) return kind;
                    else return oldconvertKindToModulePath(kind);
                };

                var oldconvertKindToViewPath = widget.convertKindToViewPath;
                widget.convertKindToViewPath = function (kind) {
                    if (kind.indexOf('pkg_') === 0) return kind.replace('/viewmodels/', '/views/');
                    else return oldconvertKindToViewPath(kind);
                };
                widget.moduleStarted = true;
            }


            //Replace 'viewmodels' in the moduleId with 'views' to locate the view.
            //Look for partial views in a 'views' folder in the root.
            viewLocator.useConvention('viewmodels', 'views');

            expurgoStorage();

            if (managerAuth.isShellSetupMode) {
                app.setRoot('viewmodels/shell', 'entrance');
                return;
            }

            if (globalDataParameters.registerParameters(system, managerAuth, logger, "OlapServerUri{TCS_USUARIO|" + managerAuth.userId + "},OlapDataBaseName{TCS_USUARIO|" + managerAuth.userId + "},BANDEIRA_REDE_PADRAO{TCS_USUARIO|" + managerAuth.userId + "},REPORTING_SERVICES_URL{},TCS_LOGO_PADRAO{},TCS_NOME_EMPRESA{},SHELL_NOME_TEMA{TCS_USUARIO|" + managerAuth.userId + "},SHELL_FLAG_RESULTADO_TABULAR{TCS_USUARIO|" + managerAuth.userId + "},SHELL_FLAG_ULTIMO_FILTRO{TCS_USUARIO|" + managerAuth.userId + "},SHELL_FLAG_BARRA_NAVEGACAO{TCS_USUARIO|" + managerAuth.userId + "},SHELL_URL_INICIAL{TCS_USUARIO|" + managerAuth.userId + "}").then(function () {

                if (managerAuth.isShellDevMode) {
                    managerBrand.loadBrands().then(function () {
                        managerUser.loadReports().then(function () {

                            showParameters();
                            app.setRoot('viewmodels/shell', 'entrance');

                        });
                    });
            }

            else {
                    managerUser.loadModules().then(function () {
                        managerBrand.loadBrands().then(function () {
                            managerUser.loadReports().then(function () {

                                showParameters();
                                app.setRoot('viewmodels/shell', 'entrance');

                            });
                        });
                    });
            }

            }));


            //load predefined Filters
            managerPredefined.load(null, null);
        }

        function configError() {
            if (managerAuth.handleErrorJavascript == false)
                return;

            //
            // configuração para tratamento de erro sobre o console
            //
            var original = window.console
            function handle(method, args) {
                if (typeof original == 'undefined')
                    return;

                if (original == null)
                    return;

                var message = Array.prototype.slice.apply(args).join(' ')

                if (method == 'warn') {
                    app.trigger('shell:log', 'warn', message, '');
                }

                if (method == 'error') {
                    app.trigger('shell:log', 'error', message, '');
                }

                // do sneaky stuff
                if (original)
                    original[method](message)
            }
            window.console = {
                log: function () {
                    handle('log', arguments)
                },
                warn: function () {
                    handle('warn', arguments)
                },
                error: function () {
                    handle('error', arguments)
                }
            }

            //
            // configuração para tratamento de erro sobre o durandal
            //
            system.error = function (err) {
                var e = null;
                if (err instanceof Error) {
                    e = err;
                }
                else {
                    e = new Error(err)
                }

                console.error(e);

                app.trigger('shell:log', 'error', e.message, e.stack);
            };
            managerError.registerManager();
        }

        function showParameters() {
            system.log('');
            system.log('*** INICIO: PARAMETROS ***');

            for (var paramName in globalDataParameters.parameters) {
                system.log(paramName + ': ' + globalDataParameters.parameters[paramName]);
            }
            system.log('*** FIM: PARAMETROS ***');
            system.log('');

        }

        function expurgoStorage() {
            system.log('');
            system.log('Main: Expurgo das informações...');
            system.log('');
            var localKeys = $.localStorage.keys()

            for (var key in localKeys) {
                var value = localKeys[key]
                if (value.indexOf(managerAuth.META_MODULE_ID) == -1)
                    $.localStorage.remove(value)
            }

            var sessionKeys = $.sessionStorage.keys()
            for (var key in sessionKeys) {
                var value = sessionKeys[key]
                if (value.indexOf(managerAuth.META_MODULE_ID) == -1)
                    $.sessionStorage.remove(value)
            }
        }
    });

