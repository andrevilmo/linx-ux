(function (_) {
    'use strict';

    angular.module('FormBuilder')
           .directive('lxListOdata', lxListOdata);

    function lxListOdata() {
        var directive =  {
            restrict: 'EA',
            templateUrl: 'app/screen-builder/directivies/odata-list/odata-list.directive.html',
            controller: ListOdataController,
            controllerAs: 'vm',
            bindToController: true,
            scope: {
                components: "="
            }
        };

        return directive;
    }

    ListOdataController.$inject = ['$scope', '$rootScope', 'odataService', '$filter'];

    function ListOdataController($scope, $rootScope, odataService, $filter) {
        var vm = this;

        var orderBy = $filter('orderBy');

        vm.message = 'Informe a URL do serviço oData';
        vm.editComponentMessage = 'Clique sobre o componente';
        vm.messageClass = 'alert-info';
        vm.showMessage = true;
        vm.showDetails = false;
        vm.showConfirmation = false;
        vm.entityName = '';
        vm.propertiesData = [];

        $scope.$watchCollection('vm.components', function(newValue, oldValue) {
            if (vm.propertiesData.length > 0 && newValue.length > 0) {
                vm.propertiesData = _.map(vm.propertiesData, function (item) {

                    var component = _.find(vm.components, function (value) {
                        if (value.options.odata) {
                            return value.options.odata.$id === item.$id;
                        }
                    });

                    item.isSelected = component ? true : false;

                    return item;
                });
            }
        });

        vm.createFormBuilder = function (option) {
            vm.showConfirmation = false;

            if (option == "ok") {
                angular.forEach(orderBy(vm.propertiesData, 'Order'), function (value, key) {
                    $rootScope.$emit('odataAddComponent', value);
                });
            }

            vm.showMessage = false;
            vm.showDetails = true;
        };

        vm.search = function () {
           vm.message = "Conectando na URL.....";

            odataService.getMetaData(vm.urlOdata).then(function (data) {
                vm.message = 'Deseja gerar os campos automaticamente?';
                vm.showConfirmation = true;

                /*
                    OData entites list
                */
                vm.entities = data;

                vm.entityName = data[0].DisplayName;
                vm.propertiesData = data[0].Properties;

                $rootScope.$emit('odataGetProperties', data);

            }, function (error) {
                vm.message = 'Informe uma URL válida.';
                vm.messageClass = 'alert-warning';
                vm.showMessage = true;
                vm.showDetails = false;
            });

        };

        vm.init = function() {
            var remote = require("remote");
            var url = remote.getGlobal("urlOdata");

            if (url !== undefined && url !== "")
            {
                vm.urlOdata = url;
                vm.search();
            }
        };

        vm.init();
    }

})(_);
