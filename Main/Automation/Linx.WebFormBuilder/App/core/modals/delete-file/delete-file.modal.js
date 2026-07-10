(function(_) {
  'use strict';

  angular
    .module('FormBuilder')
    .controller('deleteFileController', function($modalInstance, builder) {
      var vm = this;

      vm.confirm = function() {
        $modalInstance.close(true);
      };

      vm.cancel = function() {
        $modalInstance.close(false);
      };
    });
})(_);
