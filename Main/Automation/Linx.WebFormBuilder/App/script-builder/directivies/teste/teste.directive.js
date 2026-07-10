(function(_) {
    'use strict';

    angular.module('FormBuilder')
           .directive('lxTeste', [lxTeste]);

    function lxTeste() {
        var directive = {
            restrict: 'EA',
            templateUrl: 'app/script-builder/directivies/teste/teste.directive.html',
            controller: lxTesteCtrl,
            controllerAs: 'vm',
            bindToController: true
        };

        return directive;
  }

  //lxTesteCtrl.$inject = [];

  function lxTesteCtrl() {
    var vm = this;

    vm.nome = 'heitor';

  }
})(_);