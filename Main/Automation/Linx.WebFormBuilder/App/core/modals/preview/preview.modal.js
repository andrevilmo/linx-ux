angular
  .module('FormBuilder')
  .controller('previewModalController', ['$rootScope', '$scope', 'previewFactory', 'mode', '$modalInstance', 'currentProject', previewModalController]);

function previewModalController($rootScope, $scope, previewFactory, mode, $modalInstance, currentProject) {
  var vm = this;

  var exec = require('child_process').exec;

  vm.cancel = cancel;
  vm.notifyNpm = '';
  vm.notifyBower = '';
  vm.notifyGulp = '';

  previewFactory.execute(mode).then(function(result) {
    $modalInstance.dismiss();
    $rootScope.$broadcast('previewResult', false);
  }, function(info) {
    var deleteFolderPath = currentProject.urlPathProject + info;
    if (info === '\\app\\lib') {
      exec('bower cache clean');
    }
    if (info === '\\node_modules') {
      exec('npm cache clean');
    }
    deleteFolderRecursive(deleteFolderPath);
    $rootScope.$broadcast('previewResult', true);
  }, function(notify) {
    if (notify.type === 'npm') {
      vm.notifyNpm += notify.message;
      scrollToEnd('npmdiv');
    } else if (notify.type === 'bower') {
      vm.notifyBower += notify.message;
      scrollToEnd('bowerdiv');
    } else {
      vm.notifyGulp += notify.message;
      scrollToEnd('gulpdiv');
    }
  });

  function deleteFolderRecursive(path) {
    var fs = require('fs');
    if (fs.existsSync(path)) {
      fs.readdirSync(path).forEach(function(file, index) {
        var curPath = path + "/" + file;
        if (fs.lstatSync(curPath).isDirectory()) {
          deleteFolderRecursive(curPath);
        } else {
          fs.unlinkSync(curPath);
        }
      });
      fs.rmdirSync(path);
    }
  }

  function cancel() {
    $rootScope.$broadcast('previewResult', true);
    $modalInstance.dismiss();
  }

  function scrollToEnd(elem) {
    var elemDiv = document.getElementById(elem);
    if (!!elemDiv) {
      elemDiv.scrollTop = elemDiv.scrollHeight;
    }
  }
}
