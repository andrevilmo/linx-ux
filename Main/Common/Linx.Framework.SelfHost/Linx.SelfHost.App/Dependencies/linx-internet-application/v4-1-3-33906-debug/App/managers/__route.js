
define(
    function () {
        return {
            MODULES_ASSEMBLY: [{"route":"linx-appconfigmanager-bv-spa(/:action)","moduleId":"viewmodels/menusdev","title":"Linx.AppConfigManager.BV.SPA [v5.1.5799.25017 - 17/11/2015 13:53]","nav":true,"hash":"#linx-appconfigmanager-bv-spa","type":"menu-assembly","lxAssemblyName":"Linx.AppConfigManager.BV.SPA","lxModule":"linx-appconfigmanager-bv-spa","lxTransaction":"","lxCount":2,"lxTransactionTitle":"","lxShellCompiledVersion":"4.1.3","lxExtractModule":"tools/extractfiles?modulename=linx-appconfigmanager-bv-spa","lxExtractView":"tools/extractfiles?modulename=linx-appconfigmanager-bv-spa","lxDownloadModule":"tools/downloadmodules?modulename=linx-appconfigmanager-bv-spa","BreadCrumb":[{"order":0,"moduleKey":"","displayName":"Setup Mode","urlRoute":""}]},{"route":"linx-appconfigmanager-bv-spa-qrcodepage","moduleId":"pkg_linx-appconfigmanager-bv-spa/viewmodels/QrcodePage","title":"QrcodePage","titleVersion":"QrcodePage","nav":true,"type":"transaction-assembly","lxAssemblyName":"Linx.AppConfigManager.BV.SPA","lxModule":"linx-appconfigmanager-bv-spa","lxTransaction":"QrcodePage.js","lxCount":0,"lxTransactionTitle":"QrcodePage","lxShellCompiledVersion":"4.1.3","lxExtractModule":"tools/extractfiles?modulename=linx-appconfigmanager-bv-spa","lxExtractView":"tools/extractfiles?modulename=linx-appconfigmanager-bv-spa&viewname=qrcodepage","lxDownloadModule":"tools/downloadmodules?modulename=linx-appconfigmanager-bv-spa","BreadCrumb":[{"order":0,"moduleKey":"","displayName":"Setup Mode","urlRoute":""},{"order":1,"moduleKey":"","displayName":"Linx.AppConfigManager.BV.SPA [v5.1.5799.25017 - 17/11/2015 13:53]","urlRoute":"#linx-appconfigmanager-bv-spa"}]},{"route":"linx-appconfigmanager-bv-spa-wizardinstalacao","moduleId":"pkg_linx-appconfigmanager-bv-spa/viewmodels/WizardInstalacao","title":"WizardInstalacao","titleVersion":"WizardInstalacao","nav":true,"type":"transaction-assembly","lxAssemblyName":"Linx.AppConfigManager.BV.SPA","lxModule":"linx-appconfigmanager-bv-spa","lxTransaction":"WizardInstalacao.js","lxCount":0,"lxTransactionTitle":"WizardInstalacao","lxShellCompiledVersion":"4.1.3","lxExtractModule":"tools/extractfiles?modulename=linx-appconfigmanager-bv-spa","lxExtractView":"tools/extractfiles?modulename=linx-appconfigmanager-bv-spa&viewname=wizardinstalacao","lxDownloadModule":"tools/downloadmodules?modulename=linx-appconfigmanager-bv-spa","BreadCrumb":[{"order":0,"moduleKey":"","displayName":"Setup Mode","urlRoute":""},{"order":1,"moduleKey":"","displayName":"Linx.AppConfigManager.BV.SPA [v5.1.5799.25017 - 17/11/2015 13:53]","urlRoute":"#linx-appconfigmanager-bv-spa"}]}],
            MODULES_VERSION: [{"moduleUId":"5222e56d-3e2b-4337-9c38-6ddd0f572911","moduleId":"pkg_linx-appconfigmanager-bv-spa","moduleName":"linx-appconfigmanager-bv-spa","assemblyName":"Linx.AppConfigManager.BV.SPA","assemblyType":"debug","assemblyVersion":"5.1.5799.25017","assemblyVersionFormated":"v5.1.5799.25017-debug","requireId":"v5-1-5799-25017-debug","shellAssemblyVersion":"4.1.3","buildDate":"17/11/2015 13:53","CRC32":"9ABA6263"},{"moduleUId":"9D90FBBC-F519-473A-999B-565082D7D276","moduleId":"pkg_linx-internet-application","moduleName":"linx-internet-application","assemblyName":"Linx.Internet.Application","assemblyType":"debug","assemblyVersion":"4.1.3.33906","assemblyVersionFormated":"v4.1.3.33906-debug","requireId":"v4-1-3-33906-debug","shellAssemblyVersion":"4.1.3.33906","buildDate":"10/11/2015 18:50","CRC32":"BCDDEAC2"}],
            MODULES_PKG: [{"moduleName":"linx-appconfigmanager-bv-spa","requireId":"pkg_linx-appconfigmanager-bv-spa","files":["pkg_linx-appconfigmanager-bv-spa/services/DataDomains","pkg_linx-appconfigmanager-bv-spa/services/QrcodePageContext","pkg_linx-appconfigmanager-bv-spa/services/SelfHostContext","pkg_linx-appconfigmanager-bv-spa/viewmodels/QrcodePage","pkg_linx-appconfigmanager-bv-spa/viewmodels/WizardInstalacao","pkg_linx-appconfigmanager-bv-spa/viewmodels/WizardInstalacaoComplement","pkg_linx-appconfigmanager-bv-spa/viewmodels/WizardInstalacaoCustom","text!pkg_linx-appconfigmanager-bv-spa/views/QrcodePage.html","text!pkg_linx-appconfigmanager-bv-spa/views/WizardInstalacao.html"]}],


            ///////////////////////
            // method: createTransactionRoutes()
            ///////////////////////
            createTransactionRoutes: function () {
                return new Array(this.MODULES_ASSEMBLY);
            },

            ///////////////////////
            // method: searchByModuleId()
            ///////////////////////
            searchByModuleId: function (moduleId) {
                // rotas por assembly
                for (var i = 0; i < this.MODULES_ASSEMBLY.length; i++) {
                    var item = this.MODULES_ASSEMBLY[i];

                    if (item.route === undefined || item.route.toLowerCase() == moduleId.toLowerCase()) {
                        return item;
                    }
                }
                return null;
            },

            ///////////////////////
            // method: searchPkgByModuleName()
            ///////////////////////
            searchPkgByModuleName: function (moduleName) {
                // packages
                for (var i = 0; i < this.MODULES_PKG.length; i++) {
                    var item = this.MODULES_PKG[i];

                    if (item.moduleName == moduleName) {
                        return item;
                    }
                }
                return null;
            }

        };
    });
