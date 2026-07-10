(function() {
  'use strict';

  angular
    .module('FormBuilder')
    .directive('variableEditor', [variableEditor]);

  function variableEditor() {
    var directive = {
      restrict: 'E',
      templateUrl: 'app/script-builder/directivies/variable-editor/variable-editor.directive.html',
      controller: variableEditorController,
      controllerAs: 'vm',
      bindToController: true,
      scope: {
        variablesList: '='
      }
    };

    return directive;
  }

  variableEditorController.$inject = ['$scope'];

  function variableEditorController($scope) {
    var vm = this;

    vm.addVar = function() {
      var trimmedName = vm.varName.trim();
      var variable = {
        name: trimmedName,
        defaultValue: vm.defaultValue
      };

      if (vm.variablesList.indexOf(trimmedName) < 0) {
        vm.variablesList.push(variable);
      } else {
        alert('Nome de variável em uso.');
      }
    };

    vm.deleteVar = function(index) {
      vm.variablesList.splice(index, 1);
    };
  }
})();
