(function(_) {
  'use strict';

  angular
    .module('FormBuilder')
    .controller('saveChangeController', function($modalInstance, builder) {
      var vm = this;

      vm.confirm = function() {
        $modalInstance.close(true);
      };

      vm.cancel = function() {
        //$modalInstance.dismiss('cancel');
        $modalInstance.close(false);
      };
    });
})(_);
