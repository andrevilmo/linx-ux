(function () {
    'use strict';

    var controllerId = 'PreviewController';

    angular
        .module('FormBuilder')
        .controller(controllerId, ['$scope', '$rootScope', '$state', 'currentProject', 'previewService', '$sce', PreviewController]);

    function PreviewController($scope, $rootScope, $state, currentProject, previewService, $sce) {
        var vm = this;

        vm.title = 'Preview';

        var baseUrl = 'http://localhost:8100/';
        vm.urlPreview = $sce.trustAsResourceUrl(baseUrl + "?x=" + (new Date()).getTime());
    }
})();
