define(['durandal/system', 'durandal/app', 'services/logger', 'plugins/router', 'plugins/dialog', 'viewmodels/shared/modal', 'common', 'managers/__auth', 'managers/user'],
    function (system, app, logger, router, dialog, modal, common, managerAuth, managerUser) {
        var url = "";
        var vm = {
            activate: activate,
            router: router,
            compositionComplete: compositionComplete
        };
        return vm;

        function activate(context) {
            if (!isNullOrEmpty(context)) {
                if (!isNullOrEmpty(context.url)) {
                    url = context.url
                }
            }
            return true;
        }
        function compositionComplete() {
            url = (router.activeInstruction && router.activeInstruction().config && router.activeInstruction().config.url) ? router.activeInstruction().config.url : url;

            var transactionCode = (router.activeInstruction && router.activeInstruction().config ? router.activeInstruction().config.transactionCode : '0000000');
            var environmentId = managerAuth.getIdTcsAmbiente();
            var iframeId = transactionCode + '_' + environmentId;


            var div = document.getElementById('pageContent');
            div.height = "100vh";

            var iframe = document.getElementById(iframeId);

            if (!iframe) {
                iframe = document.createElement('iframe');
                iframe.id = iframeId;
                iframe.sandbox = "allow-scripts allow-popups allow-same-origin allow-top-navigation allow-modals allow-forms  allow-downloads";
                iframe.width = "100%";
                iframe.height = "100%";
                iframe.frameBorder = "0";
                iframe.src = url;
                iframe.className = "embeddedApp lx-iframe";
                div.appendChild(iframe);
            }

            iframe.onload = function () {

                var environmentId = managerAuth.getIdTcsAmbiente();
                var applicativeId = managerAuth.getCurrentIdTcsAplicativo();

                if (!applicativeId) {
                    applicativeId = managerAuth.loginInfo.Ambientes[0].IdTcsAplicativo;
                }

                var cacheKey = common.getCachePrefixEnvironment('API', 'LinxFrameworkRede/GetTbcBandeiraRedeList', managerAuth.loginInfo.CacheKey);
                var cacheRede = $.ezstorage.get(cacheKey);


                cacheKey = common.getCachePrefixGlobal('API', 'LinxReportAccessReportAccess/GetTelerikReportsFullList');
                var cacheReport = $.ezstorage.get(cacheKey);

                cacheKey = common.getCachePrefixEnvironment('API', 'LinxFrameworkAmbiente/GetServicoExcecaoMultiEnvironment', managerAuth.loginInfo.CacheKey);
                var cacheServico = $.ezstorage.get(cacheKey);

                cacheKey = common.getCachePrefixEnvironment('API', 'GetParameterValue', managerAuth.loginInfo.CacheKey);
                var cacheParameters = $.ezstorage.get(cacheKey);

                cacheKey = common.getCachePrefixGlobal('API', 'LinxFrameworkFiltro/LoadPredefinedFilters', managerAuth.loginInfo.CacheKey);
                var cachePredefined = $.ezstorage.get(cacheKey);

                var message = { brands: cacheRede, reports: cacheReport, services: cacheServico, loginInfo: managerAuth.loginInfo, parameters: cacheParameters, predefinedFilters: cachePredefined, environmentId: environmentId, applicativeId: applicativeId };

                if (managerAuth.loginInfo) {
                    message.Modules = managerUser.MODULES;
                    message.UrlBase = managerAuth.serviceBus;
                    message.headers = managerAuth.getHeaders(managerAuth.loginInfo.IdTcsAmbienteDefault);
                }

                setTimeout(function () {
                    iframe.contentWindow.postMessage(message, '*');
                }, 1000);
                setTimeout(function () {
                    iframe.contentWindow.postMessage(message, '*');
                }, 3000);

                function hubMessages(event) {
                    var o = event.data;
                    if (!o || !o.action || !o.details) return;
                    system.log('EmbeddedApp: Command Received... [' + o.action + '=' + !o.details + ']');

                    if (o.action === 'navigate') {
                        system.log('EmbeddedApp: command navigate');
                        router.navigate(o.details);
                    }

                    if (o.action === 'title') {
                        system.log('EmbeddedApp: command setTitle');
                        if (router && router.activeInstruction() && router.activeInstruction().config && router.activeInstruction().config.title) {
                            router.activeInstruction().config.title = o.details;
                            router.activeInstruction.notifySubscribers();
                        }
                    }
                }

                window.addEventListener("message", hubMessages, false);
            }
        }
    });