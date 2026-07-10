(function(_) {
  'use strict';

  angular
    .module('FormBuilder')
    .controller('newRunnerController', function($modalInstance, builder) {
      var vm = this;

      vm.newRunner = {};

      vm.create = function() {
        vm.newRunner.name = vm.runnerName;

        vm.data = builder.createFromTemplate(vm.newRunner.name, 'runner', {});

        $modalInstance.close(vm.data);
      };

      vm.cancel = function() {
        $modalInstance.dismiss('cancel');
      };
    });
})(_);
