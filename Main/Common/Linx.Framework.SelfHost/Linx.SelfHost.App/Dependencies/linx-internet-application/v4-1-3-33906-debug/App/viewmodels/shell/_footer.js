define(['services/logger', 'plugins/router', 'durandal/app', 'knockout', 'managers/user', 'managers/__auth', 'managers/error','managers/hub'],
    function (logger, router, app, ko, managerUser, managerAuth, managerError, managerHub) {
        var vm = {
            activate: activate,
            attached: attached,
            beforeBind: beforeBind,
            afterBind: afterBind,
            canDeactivate: canDeactivate,
            canActivate: canActivate,
            deactivate: deactivate,
            compositionComplete: compositionComplete,

            router: router,
            managerUser: managerUser,
            managerAuth: managerAuth,
            managerError: managerError,
            managerHub: managerHub
        };

        return vm;

        //#region Internal Methods
        function canActivate() {
            //alert('_header canActivate');
            return true;
        }

        function canDeactivate() {
            //alert('canDeactivate');
            return true;
        }

        function beforeBind() {
            //alert('beforeBind');
            return true;
        }

        function afterBind() {
            //alert('afterBind');
            return true;
        }

        function attached() {
            //alert(router.activeInstruction().config.data);
            return true;
        }

        function deactivate() {
            //alert('deactivate');
            return true;
        }

        function activate() {
            return true;
        }

        function compositionComplete() {
            $("[id='divError']").on('click', function () {
                managerError.showEvents('error');
            });

            $("[id='divWarn']").on('click', function () {
                managerError.showEvents('warn');
            });
        }
        //#endregion
    });