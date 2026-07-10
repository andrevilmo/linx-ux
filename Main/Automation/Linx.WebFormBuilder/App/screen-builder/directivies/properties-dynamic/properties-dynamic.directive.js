(function(_) {
  'use strict';

  angular.module('FormBuilder')
    .directive('lxPropertiesDynamic', lxPropertiesDynamic);

  function lxPropertiesDynamic() {
    var directive = {
      restrict: 'EA',
      templateUrl: 'app/screen-builder/directivies/properties-dynamic/properties-dynamic.directive.html',
      controller: PropertiesDynamicController,
      controllerAs: 'vm',
      bindToController: true,
      scope: {
        components: "="
      }
    };

    return directive;
  }

  PropertiesDynamicController.$inject = ['$scope', '$rootScope'];

  function PropertiesDynamicController($scope, $rootScope) {
    var vm = this;

    vm.showEditingHtml = false;
    vm.newProperty = '';
    vm.newValue = '';

    vm.createNewProperty = function() {
      if (vm.newProperty && vm.newValue) {
        var hasItem = _.where(vm.editingComponent.custom, {property: vm.newProperty});
        if (hasItem.length <= 0){
          vm.editingComponent.custom.push({
            property: vm.newProperty,
            value: vm.newValue
          });
          vm.newProperty = '';
          vm.newValue = '';
        }
      }
    };

    vm.deleteNewProperty = function(index, property) {
      vm.editingComponent.custom.splice(index, 1);
    };

    var editComponent = $rootScope.$on('odataEditComponent', function(event, component) {
      vm.editingComponent = _.findWhere(vm.components, {
        id: component.id
      });

      if (!vm.editingComponent.custom)
        vm.editingComponent.custom = [];
      vm.showEditingHtml = false;
    });

    $scope.$on('$destroy', editComponent);
  }

})(_);
