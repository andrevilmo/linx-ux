angular
  .module('FormBuilder')
  .controller('returnHomeController', ['$modalInstance','$timeout', '$state','$rootScope', 'previewService', returnHomeController]);

function returnHomeController($modalInstance, $timeout, $state, $rootScope, previewService) {
  var vm = this;

  vm.confirm = function(){
    $modalInstance.dismiss();
    $rootScope.$emit('saveFile');
    $timeout(function () {
      $state.go('welcome');
    }, 1000);
  }

  vm.cancel = function(){
    $modalInstance.dismiss();
      $state.go('welcome');
  }
}
