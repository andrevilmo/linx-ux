(function(_) {
  'use strict';

  angular
    .module('FormBuilder')
    .controller('newConstantController', function($modalInstance, builder) {
      var vm = this;

      vm.newConstant = {};

      vm.create = function() {
        vm.newConstant.name = vm.constantName;
        vm.newConstant.value = vm.constantValue;

        vm.data = builder.createFromTemplate(vm.constantName, 'constant', vm.newConstant);

        $modalInstance.close(vm.data);
      };

      vm.cancel = function() {
        $modalInstance.dismiss('cancel');
      };
    });
})(_);
