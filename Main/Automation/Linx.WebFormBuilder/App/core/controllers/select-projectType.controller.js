
(function () {
    'use strict';

    var controllerId = 'ProjectTypeController';
    angular.module('FormBuilder')
        .controller(controllerId, ['$scope', '$rootScope', '$timeout', '$location', 'toaster', ProjectTypeController]);

    function ProjectTypeController($scope, $rootScope, $timeout, $location, toaster) {
        var vm = this;

        vm.loader = false;

        vm.welcome = function () {
            $location.path('/welcome');
        };

        vm.openNewProject = function() {
          $location.path('/new-project');
        };

        vm.downloadGitProject = function(){
          $location.path('/git-project');
        };
    }

})();
