(function(_) {
  'use strict';

  angular
    .module('FormBuilder')
    .controller('newModalController', function($modalInstance, builder) {
      var vm = this;

      vm.newModal = {};

      vm.create = function() {
        vm.newModal.name = vm.modalName;

        builder.createFromTemplate(vm.newModal.name, 'modal');

        $modalInstance.close(true);
      };

      vm.cancel = function() {
        $modalInstance.dismiss('cancel');
      };
    });
})(_);
