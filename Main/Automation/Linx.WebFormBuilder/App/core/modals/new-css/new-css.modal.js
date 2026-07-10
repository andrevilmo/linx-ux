(function (_) {
    'use strict';

    angular
      .module('FormBuilder')
      .controller('newCssController', function ($modalInstance, builder) {
          var vm = this;

          vm.newCss = {};

          vm.create = function () {
              vm.newCss.name = vm.cssFileName;

              vm.data = builder.createNewCss(vm.cssFileName, 'css', vm.newCss);

              $modalInstance.close(vm.data);
          };


          vm.cancel = function () {
              $modalInstance.dismiss('cancel');
          };
      });
})(_);
