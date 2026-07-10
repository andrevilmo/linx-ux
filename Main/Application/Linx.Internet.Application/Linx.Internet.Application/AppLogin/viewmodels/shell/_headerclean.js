define(['services/logger', 'plugins/router', 'durandal/app', 'knockout', 'managers/user', 'managers/__auth'],
    function (logger, router, app, ko, managerUser, managerAuth) {
        var vm = {
            activate: activate,

            router: router,
            managerUser: managerUser,
            managerAuth: managerAuth,
            parentVM: null
        };

        return vm;

        //#region Internal Methods
        function activate(context) {
            vm.parentVM = context.parentVM;
            return true;
        }
        //#endregion
    });