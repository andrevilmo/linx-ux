(function(_) {
  'use strict';

  angular
    .module('FormBuilder')
    .controller('newFactoryController', function($modalInstance, builder) {
      var vm = this;

      vm.newFactory = {};

      vm.create = function() {
        vm.newFactory.name = vm.factoryName;
        vm.newFactory.url = vm.urlFactory;
        vm.newFactory.type = vm.type;

        if (vm.newFactory.type === 'factoryExtern')
            vm.data = builder.createNewFactory(vm.newFactory.name, 'factory', vm.newFactory.url);
        else
            vm.data = builder.createFromTemplate(vm.newFactory.name, 'factory', {
            name: vm.newFactory.name
          });

        $modalInstance.close(vm.data);
      };

      vm.cancel = function() {
        $modalInstance.dismiss('cancel');
      };
    });
})(_);
