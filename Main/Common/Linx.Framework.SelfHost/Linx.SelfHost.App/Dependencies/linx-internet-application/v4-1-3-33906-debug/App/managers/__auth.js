
define(
    function () {

        return {
            META_ROOT: $('meta[name=linx-internet-application-root]').attr("content"),
            META_MODULE_ID: $('meta[name=linx-internet-application-module-id]').attr("content"),
            META_HASH: $('meta[name=linx-internet-application-hash]').attr("content"),
            SHELL_VERSION: $('meta[name=linx-internet-application-version]').attr("content"),
            SHELL_BUILD_DATE: $('meta[name=linx-internet-application-date-version]').attr("content"),
            YEAR: 2015,
            traceMode: $('meta[name=linx-internet-application-trace-mode]').attr("content").bool(),
            
  "sessionID": "ygk4ckbpaeha2y1fgxaoe1ga",
  "serviceBus": "http://localhost:1710/",
  "imageServiceBus": "",
  "portal": "http://localhost:8172/Account/Login",
  "configCheckVersion": false,
  "handleErrorJavascript": false,
  "flexMonsterLicenseKey": "Z56H-502195-590V-624R-2C72-732R",
  "shellMode": "SETUP",
  "isShellDevMode": false,
  "isShellProdMode": false,
  "isShellSetupMode": true,
  "compilationMode": "debug",
  "isDebugMode": true,
  "profilerEnabled": true,
  "isAuthenticated": true,
  "applicationId": "",
  "authenticatedUser": "",
  "userId": "",
  "companyId": "",
  "accessGroupId": "",
  "tokenId": "",
  "loginUrl": "",
  "transaction": "",
  "environmentId": "",
  "economicGroupId": "",
  "companyName": "",
  "economicGroupName": "",
  "idGpecon": "",
  "shellVersion": "4.1.3",
  "nomeUsuario": "",
  "apelido": "",
  "autenticacaoWindows": false,
  "administrador": false,
  "expiracao": false,
  "customSearch": true,
  "startUrl": ""
,

            ///////////////////////
            // method: getServiceAddress()
            ///////////////////////
            getServiceAddress: function getServiceAddress(api) {
                return this.serviceBus + (api.length > 0 && this.serviceBus.length > 0 && api[0] !== '/' && this.serviceBus[this.serviceBus.length - 1] !== '/' ? '/' : '') + api;
            },

            getFormAccess: function (formName, successCallback, logger) {

                $.ajax({
                    type: 'GET',
                    globalError: true,
                    url: this.getServiceAddress('LinxFrameworkTransacao') + '/GetTransactionAccess?transaction=' + formName,
                    dataType: 'json',
                    cache: false,
                    error: function (jqXHR, textStatus, errorThrown) {
                        if (logger) {
                            var msg = 'Falha na checagem de autorização do formulário [' + formName + '].';
                            logger.logError(msg, errorThrown, 'GET Fail', true);
                        }
                    },
                    success: function (data) {
                        if (successCallback)
                            successCallback(data);
                    }
                });

            },

            ///////////////////////
            // method: getHeaders()
            ///////////////////////
            getHeaders: function() {
                return {
                    'Application': this.applicationId,
                    'CurrentCompany': this.companyId,
                    'AuthorizationToken': this.tokenId,
                    'CurrentUser': this.userId,
                    'AccessGroup': this.accessGroupId,
                    'Environment' : this.environmentId,
                    'EconomicGroup' : this.economicGroupId,
                    'SessionID': this.sessionID
                };
            },

            ///////////////////////
            // method: buildRootUrl()
            ///////////////////////
            buildRoot: function(url) {
                return this.META_ROOT + url;
            },

            ///////////////////////
            // method: buildRootUrl()
            ///////////////////////
            buildRootUrl: function(url) {
                return this.META_ROOT + this.META_MODULE_ID + '/' + url;
            },

            ///////////////////////
            // method: buildUrl()
            ///////////////////////
            buildUrl: function(url, width, heigth) {
                if (url.indexOf('http') > -1)
                    return url;

                width = convertToString(width);
                heigth = convertToString(heigth);

                var paramWidth = (width.length > 0 ? "?width=" + width : '');
                var paramHeigth = (heigth.length > 0 ? "&heigth=" + heigth : '');
                
                return this.buildRootUrl(url + paramWidth + paramHeigth);
            }
        };
    });
