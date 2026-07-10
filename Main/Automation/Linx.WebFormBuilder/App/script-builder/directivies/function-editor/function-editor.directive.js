(function(_) {
  'use strict';

  angular
    .module('FormBuilder')
    .directive('functionEditor', functionEditor);

  function functionEditor() {
    var directive = {
      restrict: 'E',
      templateUrl: 'app/script-builder/directivies/function-editor/function-editor.directive.html',
      controller: functionEditorController,
      controllerAs: 'vm',
      bindToController: true,
      scope: {
        functionList: '='
      }
    };

    return directive;
  }

  functionEditorController.$inject = ['$scope'];

  function functionEditorController($scope) {
    var vm = this;

    (function init() {
      vm.functionType = 'automatica';
    })();

    vm.addNewFunction = function() {
      if (isValidFnName()) {
        var objFunction = {
          name: vm.functionName,
          type: vm.functionType,
          fn: ''
        };
        vm.functionList.push(objFunction);
      } else {
        alert('Função "' + vm.functionName + '" já existe.');
      }
      console.log(vm.functionList);
    };

    vm.aceOption = {
      mode: 'javascript',
      require: ['ace/ext/language_tools'],
      advanced: {
        enableSnippets: true,
        enableBasicAutocompletion: true,
        enableLiveAutocompletion: true,
      },
      onLoad: function(_ace) {
        // HACK to have the ace instance in the scope...
        $scope.modeChanged = function() {
          _ace.getSession().setMode("ace/mode/javascript");
        };
      }
    };

    vm.removeFn = function(index) {
      vm.functionList.splice(index, 1);
    };

    function isValidFnName() {
      var lista = [];
      var aux = vm.functionList;

      lista = _.pluck(aux, 'name');
      return !_.contains(lista, vm.functionName);
    }
  }
})(_);
