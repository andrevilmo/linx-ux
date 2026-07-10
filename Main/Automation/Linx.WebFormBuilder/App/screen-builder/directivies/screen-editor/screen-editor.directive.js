(function (_) {
    'use strict';

    angular.module('FormBuilder')
           .directive('lxScreenBuilder', lxScreenBuilder);

    function lxScreenBuilder() {
        var directive =  {
            restrict: 'EA',
            templateUrl: 'app/screen-builder/directivies/screen-editor/screen-editor.directive.html',
            controller: ScreenBuilderController,
            controllerAs: 'vm',
            bindToController: true,
            scope: {
                components: "=",
                listComponents: "="
            }
        };

        return directive;
    }

    ScreenBuilderController.$inject = ['$scope', '$rootScope', 'gridsterOptions', 'currentProject'];

    function ScreenBuilderController($scope, $rootScope, gridsterOptions, currentProject) {
        var vm = this;

        vm.fileName = currentProject.currentFile || 'Documento sem título';

        vm.droppableList = [];
        vm.componentsId = 1;
        vm.gridsterOpts = gridsterOptions.mobileGridster;

        vm.addComponent = function (component) {
            vm.components.push(component);
            setChange();
        };

        vm.removeComponent = function (component) {
            vm.components = _.reject($rootScope.components, function(value) {
                return value.id == component.id;
            });
            setChange();
        };

        vm.OpenProperties = function (component) {
            $rootScope.$emit('odataEditComponent', component);
        };

        vm.FormProperties = function () {
            $rootScope.$emit('formPropertiesEdit');
        };

        vm.GenerateId = function () {
            while (_.findWhere(vm.components, { id: vm.componentsId }))
            {
                vm.componentsId++;
            }

            return vm.componentsId;
        };

        function setChange() {
           currentProject.fileChanged = true;
           vm.fileName = currentProject.currentFile + "*";
        }

        function LoadDefaultsValues(template) {
            var component = _.findWhere(vm.listComponents, { template: template });

            var defaultsComponent = _.filter(component.properties, function (element) {
                return element.defaultValue;
            });

            var objExtensible = {};

            _.each(defaultsComponent, function(value) {
                var propertieName = value.key;

                var mockObj = {};
                mockObj[propertieName] = value.defaultValue;

                objExtensible = _.extend(objExtensible, mockObj);
            });

            return objExtensible;
        }

        $rootScope.$on('odataAddComponent', function(event, odata) {

            var insertedValue = {
                                    id: vm.GenerateId(),
                                    sizeX: 6,
                                    options: {
                                        template: odata.ObjectClass + '.html',
                                        label: odata.Name,
                                        odata: odata
                                    }
                                };

            var defaultsValues = LoadDefaultsValues(odata.ObjectClass + '.html');

            insertedValue.options = _.extend(insertedValue.options, defaultsValues);

            vm.addComponent(insertedValue);
        });

        $scope.$watchCollection('vm.droppableList', function (newValue, oldValue) {
            if (newValue && newValue.length > 0) {

                var insertedValue = {};

                if (newValue[0].template)
                {
                    //componente arrastado com template
                    insertedValue = {
                        id: vm.GenerateId(),
                        sizeX: 6,
                        options: {
                            template: newValue[0].template,
                            label: newValue[0].title + ' ' + vm.componentsId
                        }
                    };
                }
                else
                {
                    insertedValue = {
                        id: vm.GenerateId(),
                        sizeX: 6,
                        options: {
                            template: newValue[0].ObjectClass + '.html',
                            label: newValue[0].Name,
                            odata: newValue[0]
                        }
                    };
                }

                var template = newValue[0].template ? newValue[0].template : newValue[0].ObjectClass + ".html";

                var defaultsValues = LoadDefaultsValues(template);

                insertedValue.options = _.extend(insertedValue.options, defaultsValues);

                vm.addComponent(insertedValue);

                vm.droppableList.splice(0);
            }
        });

        var saveFile = $rootScope.$on('saveFile', function(event, odata) {
          vm.fileName = currentProject.currentFile || 'Documento sem título';
        });

        $scope.$on('$destroy', saveFile);
    }

})(_);
