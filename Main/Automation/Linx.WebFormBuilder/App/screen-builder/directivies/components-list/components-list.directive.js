(function () {
    'use strict';

    angular.module('FormBuilder')
           .directive('lxListComponents', lxListComponents);

    function lxListComponents() {
        var directive = {
            restrict: 'EA',
            templateUrl: 'app/screen-builder/directivies/components-list/components-list.directive.html',
            controller: ListComponentsController,
            controllerAs: 'vm',
            scope: {
                flagToggle: '=',
                listComponents: '='
            },
            bindToController: true
        };

        return directive;
    };

    //ListComponentsController.$inject = [];
    function ListComponentsController($scope) {
        var vm = this;

        vm.toggle = function () {
            vm.flagToggle = vm.flagToggle ? false : true;
        }

        $scope.$watchCollection('vm.listComponents', function (oldValue, newValue) {
            vm.groupTypes = [];
            //Aqui você faz seu group-by
            if (oldValue.length > 0) {
                var groups = _.groupBy(oldValue, 'groupType');
                //_.each(groups, function (value) {
                //    vm.groupTypes.push(value);
                //});

                vm.groups = groups;

                vm.types = [];

                for (var item in groups) {

                    var currentType = { type: item, items: [] };

                    for (var i = 0; groups[item].length > i; i++) {
                        currentType.items.push(groups[item][i]);
                    }
                    vm.types.push(currentType);
                }
            }
        });
    }
})();