(function () {
    'use strict';

    var controllerId = 'WelcomeController';
    angular.module('FormBuilder').controller(controllerId, ['$scope', 'projectService', 'file', '$location', 'currentProject', '$modal', 'toaster', '$rootScope', WelcomeController]);

    function WelcomeController($scope, projectService, file, $location, currentProject, $modal, toaster,$rootScope) {

     

     
      
      
      
 
        var vm = this;


        vm.loader = false;
        vm.projects = projectService.getProjects();

        vm.selectProject = function () {
            var remote = require("remote");
            var selectFile = remote.getGlobal("selectFile");

            selectFile(function (file) {
                if (file) {
                    vm.openProject(file[0]);
                }
            });
        };

        
        vm.openProject = function (path) {
            vm.loader = true;

            projectService.checkAndSave(path).then(function () {
                vm.loader = false;

                currentProject.urlPathProject = path.substring(0, path.lastIndexOf("\\"));
                currentProject.urlProjectFile = path;

                $location.path("/formbuilder/view");
            }, function (error) {
                toaster.pop('error', 'Erro ao verificar projeto no localStorage.');
                vm.loader = false;
            });
        };

        vm.clearRecentsProjects = function () {
            projectService.clearAll();
            vm.projects = [];
        };

        vm.removeProject = function (index) {
            projectService.removeProject(index);
            vm.projects.splice(index, 1);
        };
    }
})();
