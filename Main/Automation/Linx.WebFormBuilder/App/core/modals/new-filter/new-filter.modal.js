(function(_) {
  'use strict';

  angular
    .module('FormBuilder')
    .controller('newFilterController', function($modalInstance, builder) {
      var vm = this;

      vm.newFilter = {};

      vm.create = function() {
        vm.newFilter.name = vm.filterName;

        vm.data = builder.createFromTemplate(vm.newFilter.name, 'filter', vm.newFilter);

        $modalInstance.close(vm.data);
      };

      vm.cancel = function() {
        $modalInstance.dismiss('cancel');
      };
    });
})(_);
