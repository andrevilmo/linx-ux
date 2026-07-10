(function() {
  'use strict';

  var controllerId = 'BuilderController';

  angular
    .module('FormBuilder')
    .controller(controllerId, ['$scope', '$rootScope', '$state', 'currentProject', 'previewFactory', '$modal', BuilderController]);

  function BuilderController($scope, $rootScope, $state, currentProject, previewFactory, $modal) {
    var vm = this;

    vm.typePreview = {
      type: "Preview"
    };

    vm.isPreview = previewFactory.getPreviewState();

    window.onbeforeunload = function() {
      if (currentProject.fileChanged) {
        saveChange(function() {
          vm.close();
        });
      } else {
        return true;
      }
      return false;
    };

    $scope.$on('previewResult', function(event, hasError) {
      if (hasError) {
        vm.pause();
      } else {
        vm.isPreview = true;
      }
      vm.loader = false;
    });

    var handler = function() {

      vm.save();
    };

    require('ipc').on('saveFile', handler);

    vm.save = function() {
      $rootScope.$emit('saveFile');
      currentProject.fileChanged = false;
    };

    vm.fullScreen = function() {
      var remote = require("remote");

      var isFullScreen = remote.getGlobal("isFullScreen");
      var flagFullScreen = isFullScreen();

      var setFullScreen = remote.getGlobal("setFullScreen");
      setFullScreen(!flagFullScreen);
    };

    vm.preview = function(mode) {

      vm.loader = true;

      if (mode == "Navegador")
        mode = "browser";
      else if (mode == "Desenhador")
        mode = "builder";

      vm.typePreview.type = mode == "browser" ? "Navegador" : "Desenhador";

      var modalInstance = $modal.open({
        templateUrl: 'app/core/modals/preview/preview.modal.html',
        controller: 'previewModalController as vm',
        size: 'lg',
        backdrop: 'static',
        resolve: {
          'mode': function() {
            return mode;
          }
        }
      });
    };

    vm.home = function() {
      saveChange(function() {
        $state.go('welcome');
      });
    };
    /*  var modalInstance = $modal.open({
        templateUrl: 'app/core/modals/save-change/save-change.modal.html',
        controller: 'returnHomeController as vm'
      });*/

    vm.pause = function() {
      vm.isPreview = false;
      vm.loader = false;
      previewFactory.stop();
    };

    vm.close = function() {
      var remote = require("remote");
      var close = remote.getGlobal("close");

      saveChange(function() {
        close();
      });

    };

    vm.minimize = function() {
      var remote = require("remote");
      var minimize = remote.getGlobal("minimize");
      minimize();
    };

    function saveChange(callback) {
      if (currentProject.fileChanged) {
        var modalInstance = $modal.open({
          templateUrl: 'app/core/modals/save-change/save-change.modal.html',
          controller: 'saveChangeController as vm'
        });

        modalInstance.result.then(function(option) {
          if (option) {
            $rootScope.$emit('saveFile');
          }
          currentProject.fileChanged = false;

          if (callback) {
            callback();
          }
        });
      } else {
        if (callback) {
          callback();
        }
      }
    }

    function openFiles(event, file) {
      //gerado
      currentProject.currentFile = file;

      var type = '';

      if (file.indexOf('.js') != -1 ||
        file.indexOf('.css') != -1 ||
        file.indexOf('.scss') != -1 ||
        file.indexOf('.html') != -1 ||
        file.indexOf('.json') != -1) {
        //editor de texto
        type = 'text';
      }

      if (file.indexOf('.json') != -1 && file.indexOf('view') != -1) {
        type = 'view';
        //editor builder
      }

      if (type !== '') {
        $state.transitionTo('formbuilder.' + type, null, {
          'reload': true
        });
      }
    }

    var openFile = $rootScope.$on('openFile', function(event, file) {
      saveChange(function() {
        openFiles(event, file);
      });
    });

    $scope.$on('$destroy', openFile);
    $scope.$on('$destroy', function() {
      require('ipc').removeListener('saveFile', handler);
    });
  }
})();
