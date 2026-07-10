define(['durandal/system', 'durandal/app', 'services/logger', 'plugins/router', 'plugins/dialog', 'viewmodels/shared/modal', 'common'],
    function (system, app, logger, router, dialog, modal, common) {
        var selectedNodeText = ko.observable('');

        var vm = {
            activate: activate,
            router: router,
            compositionComplete: compositionComplete,
            count: 0,
            selectedNodeText: selectedNodeText
        };
        return vm;

        function activate() {
            return true;
        }

        function compositionComplete() {
        };

    });