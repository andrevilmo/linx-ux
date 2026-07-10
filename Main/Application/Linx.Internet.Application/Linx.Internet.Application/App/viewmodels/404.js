define(['durandal/system', 'services/logger', 'plugins/router'],
    function (system, logger, router) {
        var vm = {
            activate: activate,
            router: router

        };
        return vm;

        function activate() {
            return true;
        }
    });