(function(_) {
  'use strict';

  angular
    .module('FormBuilder')
    .controller('newControllerController', function($modalInstance, builder) {
      var vm = this;

      vm.newController = {};

      vm.create = function() {
        vm.newController.name = vm.controllerName;
        vm.newController.route = vm.controllerRoute;

        vm.data =  builder.createFromTemplate(vm.newController.name, 'controller', vm.newController);

        $modalInstance.close(vm.data);
      };

      vm.cancel = function() {
        $modalInstance.dismiss('cancel');
      };
    });
})(_);
