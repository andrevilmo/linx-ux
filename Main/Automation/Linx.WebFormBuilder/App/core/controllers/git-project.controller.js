(function() {
  'use strict';

  angular
    .module('FormBuilder')
    .controller('GitProjectController', GitProjectController);

  GitProjectController.$inject = ['$scope', 'projectService', 'template', '$location', 'directory', 'currentProject'];

  function GitProjectController($scope, projectService, template, $location, directory, currentProject) {
    var vm = this;

    var logFactory;

    vm.loader = false;
    vm.gitDirectory = "";

    vm.exit = function() {
      $location.path('/select-ProjectType');
    };

    vm.downloadGitProject = function() {
      logFactory = $scope.log;
      vm.loader = true;

      logFactory.open();

      logFactory.config = {
        cancel: template.cancel
      };

      template.download(vm.gitDirectory, null, vm.urlGitProject).then(function(sucessTemplate) {
          var pattern = /\w+(?=\.git\b)/;

          var projectName = pattern.exec(vm.urlGitProject)[0];
          var projectDirectory = vm.gitDirectory + '\\' + projectName;

          directory.readdir(projectDirectory).then(function(files) {

            vm.projectExec = files.filter(function(item) {
              return item.indexOf('.lxproj') > 0;
            });

            if (vm.projectExec.length === 0) {

              logFactory.appendText({
                id: 'git',
                text: "\nO seu projeto não contem extensão .lxproj"
              });

              $location.path("/formbuilder");
              vm.loader = false;

              return false;
            }

            var projectFilePath = projectDirectory + '\\' + vm.projectExec[0];

            var project = {
              name: projectName,
              path: projectFilePath
            };

            projectService.saveProject(project);

            currentProject.urlProjectFile = projectFilePath;
            currentProject.urlPathProject = projectDirectory;

            vm.loader = false;
            logFactory.close();

            $location.path("/formbuilder/view");
          });
        }, function(error) {
          logFactory.appendText({
            id: 'git',
            text: "\nDownload não realizado"
          });

          vm.loader = false;
        },
        function(notify) {
          logFactory.appendText({
            id: 'git',
            text: notify
          });
        });
    };

    vm.folder = function() {
      var remote = require("remote");
      var selectDirectory = remote.getGlobal("selectDirectory");

      selectDirectory(function(directory) {
        if (directory) {
          vm.gitDirectory = directory[0];
          $scope.$apply();
        }
      });
    };
  }

})();
