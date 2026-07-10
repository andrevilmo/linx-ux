(function(_) {
  'use strict';

  angular
    .module('FormBuilder')
    .controller('newViewController', function($modalInstance, builder) {
      var vm = this;

      vm.newView = {};
      vm.newController = {};

      vm.create = function() {
        vm.newView.name = vm.screenName;
        vm.newView.url = vm.urlMetadata;
        vm.newView.hasController = vm.generateController;



        vm.data = builder.createNewView(vm.newView.name);
        
        if(vm.newView.hasController)
        {        
          vm.newController.name = vm.controllerName;
          vm.newController.route = vm.controllerRoute;

          builder.createFromTemplate(vm.newController.name, 'controller', vm.newController);
        }
        
        $modalInstance.close(vm.data);
      };


      vm.cancel = function() {
        $modalInstance.dismiss('cancel');
      };
    });
})(_);
