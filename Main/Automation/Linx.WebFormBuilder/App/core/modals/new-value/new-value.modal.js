(function(_) {
  'use strict';

  angular
    .module('FormBuilder')
    .controller('newValueController', function($modalInstance, builder) {
      var vm = this;

      vm.newValue = {};

      vm.create = function() {
        vm.newValue.name = vm.valueName;
        vm.newValue.value = vm.valueValue;

        vm.data = builder.createFromTemplate(vm.newValue.name, 'value', vm.newValue);

        $modalInstance.close(vm.data);
      };

      vm.cancel = function() {
        $modalInstance.dismiss('cancel');
      };
    });
})(_);
