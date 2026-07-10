//Exposing jQuery and Knoskcout for Durandal 
define('jquery', function () { return jQuery; });
define('knockout', ko);

define(['jquery', 'knockout', 'durandal/system', 'durandal/app', 'durandal/viewLocator', 'breeze', 'services/logger', 'plugins/router', 'managers/__auth', 'plugins/widget', 'managers/error', 'common'],
    function ($, ko, system, app, viewLocator, breeze, logger, router, managerAuth, widget, managerError, common) {
        //>>excludeStart("build", true);
        // Enable debug message to show in the console 
        system.debug(managerAuth.isDebugMode);
        //>>excludeEnd("build");

        app.title = $('meta[name=linx-internet-application-app-title]').attr("content");

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
            });

            $(document).ajaxSend(function (event, jqxhr, settings) {
                if (isNullOrEmpty(settings.message) == false) {
                    $("#divCurrentActivityInformation").text(settings.message + '...');
                }
            });

            $(document).ajaxComplete(function (event, jqxhr, settings) {
                if (isNullOrEmpty(settings.message) == false) {
                    if ((settings.message + '...') == $("#divCurrentActivityInformation").text()) {
                        $("#divCurrentActivityInformation").text('');
                    }
                }
            });

            $(document).ajaxStop(function () {
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
                $("#divCurrentActivityInformation").text("");
                app.showMessage(msg, 'Requisição inválida', ['Ok']);
            });

            //// configuracao de todas as chamadas AJAX pelo breeze
            //var ajaxAdapter = breeze.config.getAdapterInstance("ajax");
            //ajaxAdapter.defaultSettings = {
            //    beforeSend: function (xhr, settings) {
            //        if (settings.url.length >= 2048) {
            //            xhr.abort('Foi excedido o limite de caracteres para a pesquisa!');
            //            return false;
            //        }

            //        xhr.setRequestHeader('SessionID', managerAuth.sessionID);
            //        xhr.setRequestHeader('CurrentUser', managerAuth.loginInfo.UidUsuario);
            //        xhr.setRequestHeader('EconomicGroup', managerAuth.loginInfo.UidGrupoEconomico);

            //        if (router.activeInstruction().config != null && router.activeInstruction().config.currentData != null) {
            //            xhr.setRequestHeader('TransactionInfo', router.activeInstruction().config.currentData.Module);

            //            var environmentInfo = managerAuth.getEnvironmentInfo(router.activeInstruction().config.currentData.IdTcsAmbiente);

            //            if (environmentInfo) {
            //                xhr.setRequestHeader('Application', environmentInfo.UidAplicacao);
            //                xhr.setRequestHeader('CurrentCompany', environmentInfo.UidEmpresa);
            //                xhr.setRequestHeader('AuthorizationToken', environmentInfo.Token);
            //                xhr.setRequestHeader('Environment', environmentInfo.IdTcsAmbiente);
            //            }
            //        }
            //        else if (managerAuth.shellMode == 'DEV' || managerAuth.shellMode == "SETUP") {
            //            xhr.setRequestHeader('Application', managerAuth.loginInfo.Ambientes[0].UidAplicacao);
            //            xhr.setRequestHeader('CurrentCompany', managerAuth.loginInfo.Ambientes[0].UidEmpresa);
            //            xhr.setRequestHeader('AuthorizationToken', managerAuth.loginInfo.Ambientes[0].Token);
            //            xhr.setRequestHeader('Environment', managerAuth.loginInfo.Ambientes[0].IdTcsAmbiente);
            //        }
            //        return xhr;
            //    }
            //};

            //// configuracao de todas as chamadas AJAX pelo jquery
            //$.ajaxSetup({
            //    beforeSend: function (xhr, settings) {
            //        if (settings.url.length >= 2048) {
            //            xhr.abort('Foi excedido o limite de caracteres para a pesquisa!');
            //            return false;
            //        }

            //        if (!settings.headers) {
            //            var headers = managerAuth.getHeaders();
            //            xhr.setRequestHeader("Application", headers.Application);
            //            xhr.setRequestHeader("AuthorizationToken", headers.AuthorizationToken);
            //            xhr.setRequestHeader("CurrentCompany", headers.CurrentCompany);
            //            xhr.setRequestHeader("CurrentUser", headers.CurrentUser);
            //            xhr.setRequestHeader("EconomicGroup", headers.EconomicGroup);
            //            xhr.setRequestHeader("Environment", headers.Environment);
            //            xhr.setRequestHeader("SessionID", headers.SessionID);
            //        }
            //        return xhr;
            //    }

            //});

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

            app.setRoot('viewmodels/shell', 'entrance');
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
    });

