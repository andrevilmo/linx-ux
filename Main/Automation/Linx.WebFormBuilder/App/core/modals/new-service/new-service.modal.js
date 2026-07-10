(function(_) {
  'use strict';

  angular
    .module('FormBuilder')
    .controller('newServiceController', function($modalInstance, builder) {
      var vm = this;

      vm.newService = {};

      vm.create = function() {
        vm.newService.name = vm.serviceName;
        vm.newService.url = vm.urlMetadata;
        vm.newService.type = vm.type;

        if (vm.newService.type === 'breeze')
            vm.data = builder.createNewBreezeService(vm.newService.name, 'service', vm.newService.url);
        else
            vm.data = builder.createFromTemplate(vm.newService.name, 'service', vm.newService);

        $modalInstance.close(vm.data);
      };

      vm.cancel = function() {
        $modalInstance.dismiss('cancel');
      };
    });
})(_);
