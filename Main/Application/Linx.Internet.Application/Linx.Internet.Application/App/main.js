//Exposing jQuery and Knoskcout for Durandal 
define('jquery', function () { return jQuery; });
define('knockout', ko);

define(['jquery', 'knockout', 'durandal/system', 'durandal/app', 'durandal/viewLocator', 'breeze', 'services/logger', 'plugins/router', 'managers/__auth', 'managers/brand', 'plugins/widget', 'managers/error', 'common', 'managers/predefinedFilters',
    'managers/authTOTP', 'base32'],
    function ($, ko, system, app, viewLocator, breeze, logger, router, managerAuth, managerBrand, widget, managerError, common, managerPredefined, managerAuthTOTP, base32) {
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

            if (managerAuth.isLoginPOSUXMode) {
                getFingerPrint().then(function (data) {
                    authenticate(data).then(function () {
                        ajaxSetup();
                        loadServices();
                    })
                });
            }
            else {
                ajaxSetup();
                if (managerAuth.isShellProdMode && managerAuth.expiracao) {
                    startPasswordChangeFlow();
                }
                else {
                    loadServices();
                }
            }
        }

        function isPasswordChangeAllowedRequest(url) {
            if (!url) {
                return false;
            }

            var requestUrl = url.toLowerCase();
            return requestUrl.indexOf('changeuserpassword') >= 0
                || requestUrl.indexOf('updateexpiration') >= 0
                || requestUrl.indexOf('logoffforpasswordchange') >= 0;
        }

        function startPasswordChangeFlow() {
            require(['viewmodels/shared/modalChangePassword'], function (modalChangePassword) {
                modalChangePassword.performLogoffForPasswordChange().then(function () {
                    managerAuth.passwordChangeOnlyMode = true;
                    app.setRoot('viewmodels/shell', 'entrance');
                });
            });
        }

        function getFingerPrint() {
            var dfd = $.Deferred();

            $.ajax({
                type: 'GET',
                message: "Validando dispositivo",
                messageUser: "Validando dispositivo",
                globalError: true,
                url: managerAuth.serviceBus + 'LinxFrameworkAutorizacao/GetLocalBusFingerPrint',
                data: {},
                dataType: 'json',
                async: true,
                cache: false,

                error: function (jqXHR, textStatus, errorThrown) {
                    dfd.resolve(null);
                },

                success: function (data) {
                    dfd.resolve(data);
                }
            });

            return dfd.promise();
        }

        function authenticate(data) {
            var dfd = $.Deferred();

            if (!data) {
                return null;
            }

            var localDateTime = moment.utc();
            var retorno = base32.decode(data).split('||');
            var serverFingerPrint = retorno[0];
            var serverDateTime = moment(retorno[1]);
            var serverSyncDiff = serverDateTime.diff(localDateTime);
            var deviceId = '00000000000000000000000000000000';
            var encondedSecret = base32.encode(managerAuthTOTP.getOTP(serverFingerPrint, deviceId, serverSyncDiff));

            $.ajax({
                type: 'GET',
                message: "Validando acesso",
                messageUser: "Validando acesso",
                globalError: true,
                url: managerAuth.serviceBus + 'LinxFrameworkAutorizacao/AuthenticateLocalBus',
                data: {
                    deviceId: deviceId,
                    encodedSecret: encondedSecret
                },
                dataType: 'json',
                async: true,
                cache: false,

                error: function (jqXHR, textStatus, errorThrown) {
                    dfd.resolve();
                },

                success: function (data) {

                    if (!data.Headers || !data.Headers[1]) {
                        app.showMessage('Headers de autenticação não encontrados.', 'Atenção', ['Ok']).then(function () { window.location.reload(); });
                        dfd.resolve(false);
                        return;
                    }

                    var ambientes = [];
                    ambientes.push({ IdTcsAmbiente: data.Headers[6], UidEmpresa: data.Headers[1], DescricaoEmpresa: '', DescricaoAmbiente: '', UidAplicacao: data.Headers[8], Token: data.Headers[2], IdTcsAplicativo: 0, DescricaoAplicativo: '', UrlAplicativo: '', IndicaAdministrador: false, IndicaMultiGpecon: false });
                    managerAuth.loginInfo = ({ UidUsuario: data.Headers[3], NomeUsuario: managerAuth.nomeVendedor, NomeCurtoUsuario: managerAuth.nomeVendedor, AutenticacaoWindows: false, DataExpiracaoSenha: null, IdLinxGrupoEconomico: 0, UidGrupoEconomico: data.Headers[5], DescricaoGrupoEconomico: '', IdTcsAmbienteDefault: data.Headers[6], CacheKey: data.Headers[6], Ambientes: ambientes });
                    managerAuth.userId = managerAuth.loginInfo.UidUsuario;
                    managerAuth.economicGroupId = managerAuth.loginInfo.UidGrupoEconomico;
                    managerAuth.idGpecon = managerAuth.loginInfo.IdLinxGrupoEconomico;

                    dfd.resolve(true);
                }
            });

            return dfd.promise();
        }

        function updateAuthentication() {
            var dfd = $.Deferred();

            common.showProcess();

            $.ajax({
                type: 'GET',
                message: "Autenticando",
                messageUser: "Autenticando",
                globalError: true,
                url: managerAuth.serviceBus + 'LinxFrameworkAutorizacao/UpdateAuthentication',
                data: { parameters: "" },
                dataType: 'json',
                async: true,
                cache: false,

                error: function (jqXHR, textStatus, errorThrown) {
                    dfd.resolve(false);
                },

                success: function (success) {
                    common.closeProcess();
                    if (success) {
                        getFingerPrint().then(function (data) {
                            authenticate(data).then(function (authenticated) {
                                dfd.resolve(authenticated);
                            })
                        });
                    }
                }
            });
            return dfd.promise();
        }

        function ajaxSetup() {
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
                //ToDo:
                //settings.type === "POST" && settings.url.endsWith("/SaveChanges")
                if (!settings.globalError || (settings.type === "POST" &&
                    !settings.url.substring(settings.url.lastIndexOf("/")).contains('SavePivotLayout') &&
                    !settings.url.substring(settings.url.lastIndexOf("/")).contains('SaveConfiguracaoExportacao') &&
                    settings.url.substring(settings.url.lastIndexOf("/")).contains("/Save")))
                    return;

                var msg = '';

                if (settings.messageUser != null) {
                    msg = settings.messageUser + '\r\n';
                }

                var descricao = '-';

                if (!isNullOrEmpty(jqxhr.responseJSON) && !isNullOrEmpty(jqxhr.responseJSON.ExceptionMessage)) {
                    descricao = jqxhr.responseJSON.ExceptionMessage;
                    descricao = innerExceptionMessages(jqxhr.responseJSON.InnerException, descricao, 0);
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


                //Alterado conforme Tasks 46021(FW)/ 46058
                //var url = (isNullOrEmpty(settings.url) == true ? ' - ' : settings.url)
                //msg += '\r\n' + '<b>Descrição:</b>&nbsp;' + descricao + '\r\n';
                //msg += '\r\n' + "<b>Url:</b>&nbsp;<a href='" + url + "' target='_blank'>" + url + "</a>";
                msg += '\r\n' + descricao + '\r\n';

                $('body').removeClass("page-quick-sidebar-open");
                $('.closeSidebarClick').hide();

                common.closeProcess('#main');
                common.closeProcess();
                // $("#divCurrentActivityInformation").text("");

                if (managerAuth.isLoginPOSUXMode && !isNullOrEmpty(jqxhr.responseJSON) && !isNullOrEmpty(jqxhr.responseJSON.ExceptionMessage) && jqxhr.responseJSON.ExceptionMessage.contains("ERRAUT016")) {
                    app.showMessage('Sessão expirada. \r\nNecessário efetuar uma nova autenticação.', 'Informação', ['Ok']).then(function () {

                        updateAuthentication().then(function (authenticated) {
                            if (authenticated) {
                                //app.showMessage('Autenticação efetuada com sucesso.', 'Informação', ['Ok']).then(function () {
                                var currentModule = router.activeItem().__moduleId__;
                                requirejs.undef(currentModule)
                                requirejs.undef(currentModule + 'Complement')
                                requirejs.undef('text!' + currentModule.replace('viewmodels', 'views') + '.html')
                                router.deactivate();
                                router.activate();
                                //})
                            }
                            else {
                                app.showMessage('Não foi possível efetuar a autenticação.', 'Erro', ['Ok'])
                            }
                        })
                    });
                }
                else {
                    app.showMessage(msg, 'Requisição inválida', ['Ok'])
                }
            });

            // configuracao de todas as chamadas AJAX pelo breeze
            var ajaxAdapter = breeze.config.getAdapterInstance("ajax");
            ajaxAdapter.defaultSettings = {
                beforeSend: function (xhr, settings) {
                    if (managerAuth.passwordChangeOnlyMode && !isPasswordChangeAllowedRequest(settings.url)) {
                        settings.globalError = false;
                        xhr.abort();
                        return false;
                    }

                    if (settings.url.length >= 2048) {
                        xhr.abort('Foi excedido o limite de caracteres para a pesquisa!');
                        return false;
                    }

                    settings.globalError = true;

                    xhr.setRequestHeader('SessionID', managerAuth.sessionID);
                    xhr.setRequestHeader('CurrentUser', managerAuth.loginInfo.UidUsuario);
                    xhr.setRequestHeader('EconomicGroup', managerAuth.loginInfo.UidGrupoEconomico);
                    xhr.setRequestHeader("LoginMode", managerAuth.loginMode);
                    xhr.setRequestHeader("Branch", managerAuth.idFilialPfj);

                    if (router.activeInstruction().config != null && router.activeInstruction().config.currentData != null) {
                        xhr.setRequestHeader('TransactionInfo', router.activeInstruction().config.currentData.Module);

                        var environmentInfo = managerAuth.getEnvironmentInfo(router.activeInstruction().config.currentData.IdTcsAmbiente);

                        if (environmentInfo) {
                            xhr.setRequestHeader('Application', environmentInfo.UidAplicacao);
                            xhr.setRequestHeader('CurrentCompany', environmentInfo.UidEmpresa);
                            xhr.setRequestHeader('AuthorizationToken', environmentInfo.Token);
                            xhr.setRequestHeader('Environment', environmentInfo.IdTcsAmbiente);
                        }
                    }
                    else if (managerAuth.shellMode == 'DEV' || managerAuth.shellMode == "SETUP") {
                        xhr.setRequestHeader('Application', managerAuth.loginInfo.Ambientes[0].UidAplicacao);
                        xhr.setRequestHeader('CurrentCompany', managerAuth.loginInfo.Ambientes[0].UidEmpresa);
                        xhr.setRequestHeader('AuthorizationToken', managerAuth.loginInfo.Ambientes[0].Token);
                        xhr.setRequestHeader('Environment', managerAuth.loginInfo.Ambientes[0].IdTcsAmbiente);
                    }
                    return xhr;
                },
                success: function (event, xhr, settings) {
                    if (event.value) {
                        event["Results"] = event.value;
                        delete event.value;
                    }
                    if (event["@odata.count"]) {
                        event["InlineCount"] = event["@odata.count"];
                        delete event["@odata.count"];
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {

                }
            };

            // configuracao de todas as chamadas AJAX pelo jquery
            $.ajaxSetup({
                beforeSend: function (xhr, settings) {
                    if (managerAuth.passwordChangeOnlyMode && !isPasswordChangeAllowedRequest(settings.url)) {
                        settings.globalError = false;
                        xhr.abort();
                        return false;
                    }

                    if (settings.url.length >= 2048) {
                        xhr.abort('Foi excedido o limite de caracteres para a pesquisa!');
                        return false;
                    }

                    if (!settings.headers) {
                        var headers = managerAuth.getHeaders();
                        xhr.setRequestHeader("Application", headers.Application);
                        xhr.setRequestHeader("AuthorizationToken", headers.AuthorizationToken);
                        xhr.setRequestHeader("CurrentCompany", headers.CurrentCompany);
                        xhr.setRequestHeader("CurrentUser", headers.CurrentUser);
                        xhr.setRequestHeader("EconomicGroup", headers.EconomicGroup);
                        xhr.setRequestHeader("Environment", headers.Environment);
                        xhr.setRequestHeader("SessionID", headers.SessionID);
                        xhr.setRequestHeader("LoginMode", headers.LoginMode);
                        xhr.setRequestHeader("Branch", headers.Branch);
                    }
                    return xhr;
                }

            });

        }

        function innerExceptionMessages(innerException, message, level) {
            if (level < 3 && !isNullOrEmpty(innerException)) {
                level = level + 1;
                message = message + '\r\n' + '&emsp;'.repeat(level) + '->' + innerException.ExceptionMessage;
                message = innerExceptionMessages(innerException.InnerException, message, level);
            }

            return message;
        }

        function loadServices() {

            require(['managers/user', 'managers/message'], function (managerUser, managerMessage) {

                //No FrameworkBV
                //if (managerAuth.isShellDevMode) {
                //    app.setRoot('viewmodels/shell', 'entrance');
                //    return;
                //}

                managerUser.loadServices().then(function () {
                    globalDataParameters.registerParameters(system, managerAuth, logger, "OlapServerUri{TCS_USUARIO|" + managerAuth.loginInfo.UidUsuario + "},OlapDataBaseName{TCS_USUARIO|" + managerAuth.loginInfo.UidUsuario + "},BANDEIRA_REDE_PADRAO{TCS_USUARIO|" + managerAuth.loginInfo.UidUsuario + "},REPORTING_SERVICES_URL{},TCS_LOGO_PADRAO{},TCS_NOME_EMPRESA{},SHELL_NOME_TEMA{TCS_USUARIO|" + managerAuth.loginInfo.UidUsuario + "},SHELL_FLAG_RESULTADO_TABULAR{TCS_USUARIO|" + managerAuth.loginInfo.UidUsuario + "},SHELL_FLAG_ULTIMO_FILTRO{TCS_USUARIO|" + managerAuth.loginInfo.UidUsuario + "},SHELL_FLAG_BARRA_NAVEGACAO{TCS_USUARIO|" + managerAuth.loginInfo.UidUsuario + "},SHELL_URL_INICIAL{TCS_USUARIO|" + managerAuth.loginInfo.UidUsuario + "}").then(function () {

                        managerPredefined.load(null, null);

                        if (managerAuth.isShellDevMode) {
                            managerBrand.loadBrands().then(function () {
                                managerUser.loadReports().then(function () {
                                    showParameters();
                                    app.setRoot('viewmodels/shell', 'entrance');
                                })
                            });
                        }
                        else {
                            managerUser.loadModules().then(function () {
                                managerBrand.loadBrands().then(function () {
                                    managerUser.loadReports().then(function () {
                                        managerMessage.start();
                                        showParameters();
                                        app.setRoot('viewmodels/shell', 'entrance');
                                    });
                                });
                            });
                        }
                    })
                });
            });
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

            for (var i = 0; i < managerAuth.loginInfo.Ambientes.length; i++) {
                system.log('-' + managerAuth.loginInfo.Ambientes[i].DescricaoAplicativo);
                for (var ii = 0; ii < managerAuth.loginInfo.Ambientes[i].Parametros.length; ii++) {
                    var item = managerAuth.loginInfo.Ambientes[i].Parametros[ii];
                    system.log('    - ' + item.TituloParametro + ': ' + item.ValorParametro);
                }
                system.log('');
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
                if (!value == "Hash_Login" && value.indexOf(managerAuth.META_MODULE_ID) == -1)
                    $.localStorage.remove(value)
            }

            var sessionKeys = $.sessionStorage.keys()
            for (var key in sessionKeys) {
                var value = sessionKeys[key]
                if (!value == "Hash_Login" && value.indexOf(managerAuth.META_MODULE_ID) == -1)
                    $.sessionStorage.remove(value)
            }
        }
    });

