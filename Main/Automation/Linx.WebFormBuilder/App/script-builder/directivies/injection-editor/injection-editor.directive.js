(function () {
    'use strict';

    angular
      .module('FormBuilder')
      .directive('injectionEditor', injectionEditor);

    function injectionEditor() {
        var directive = {
            restrict: 'E',
            templateUrl: 'app/script-builder/directivies/injection-editor/injection-editor.directive.html',
            controller: injectionEditorController,
            controllerAs: 'vm',
            bindToController: true,
            scope: {
                injectionList: '='
            }
        };

        return directive;
    }

    injectionEditorController.$inject = ['$scope'];

    function injectionEditorController($scope) {
        var vm = this;

        vm.addInjection = function() {
          var trimmedName = vm.varName.trim();
          if (vm.injectionList.indexOf(trimmedName) < 0) {
            vm.injectionList.push(trimmedName);
          } else {
            alert('Nome de variável em uso.');
          }
        };

        vm.deleteInjection = function(index) {
          vm.injectionList.splice(index, 1);
        };
    }
})();
