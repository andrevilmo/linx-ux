
(function(_) {
    'use strict';

    angular.module('FormBuilder')
           .directive('lxComponentProperties', lxComponentProperties);

    function lxComponentProperties() {
    var directive = {
            restrict: 'EA',
            templateUrl: 'app/screen-builder/directivies/properties-editor/properties-editor.directive.html',
            controller: ComponentPropertiesController,
            controllerAs: 'vm',
            bindToController: true,
      scope: {
                components: "=",
                listComponents: "=",
                formProperties: "="
            }
        };

        return directive;
  }

  ComponentPropertiesController.$inject = ['$rootScope', '$scope', '$modal', 'formProperties', 'currentProject', 'file', 'flexmonsterLayoutService'];

    function ComponentPropertiesController($rootScope, $scope, $modal, formProperties, currentProject, file, flexmonsterLayoutService) {
        var vm = this;

        vm.odataProperties = [];
        vm.showEditingHtml = false;
    vm.newProperty = '';
    vm.newValue = '';

    var odataGetProperties = $rootScope.$on('odataGetProperties', function(event, odata) {
            vm.odataProperties = odata[0].Properties;
            vm.odataEntites = odata;
        });

    var formPropertiesEdit = $rootScope.$on('formPropertiesEdit', function(event) {
            //Responsavél pela edição das propriedades do formulário
            vm.formFields = [];

            vm.formPropertiesFields = formProperties;

            $scope.$parent.$parent.tabs[2].active = true;
        });

    var odataEditComponent = $rootScope.$on('odataEditComponent', function(event, component) {

            vm.formPropertiesFields = [];

            if (component.options.template == "LinxHtml.html") {
                vm.editingHtmlLabel = "Editar HTML";
                vm.showEditingHtml = true;
            } else {
                vm.showEditingHtml = false;
            }

      vm.editingComponent = _.findWhere(vm.components, {
        id: component.id
      });

      vm.editingFields = _.findWhere(vm.listComponents, {
        template: vm.editingComponent.options.template
      });

            if (vm.editingComponent.options.odataSelected) {
                vm.editingComponent.options['odata.$id'] = vm.editingComponent.options.odata.$id;
            }

      vm.editingFields.properties = _.reject(vm.editingFields.properties, function(item) {
                return item.key == "odata.$id";
            });

            if (component.options.template === "LinxFlexmonster.html") {
              vm.setEditingFieldsFlexmonster();
            } else {
              vm.editingFields.properties.push({
                  "key": "odata.$id",
                  "type": "select",
          "templateOptions": {
                    "label": "Odata",
                    "options": vm.odataProperties,
                    "valueProp": "$id",
                    "labelProp": "Name",
                    "placeholder": ""
                  }
              });
            }

            vm.formFields = vm.editingFields.properties;

            vm.formEvents = vm.editingFields.events;

            //mudar a nomenclatura pois a palavra é reservada
            vm.formClass = vm.editingFields.class;

            $scope.$parent.$parent.tabs[2].active = true;
        });

    $scope.$watch('vm.editingComponent.options.odataSelected', function() {

            if (vm.odataProperties && vm.editingComponent && vm.editingComponent.options) {

        vm.editingComponent.options.odata = _.findWhere(vm.odataProperties, {
                     $id: vm.editingComponent.options.odataSelected
                 });
            }

        }, true);

    vm.deleteComponent = function() {
      vm.components = _.reject(vm.components, function(component) {
                return component.id == vm.editingComponent.id;
            });
        };

    vm.openCodeModal = function() {
          var modalController = (vm.editingComponent.options.template == "LinxFlexmonster.html") ?
        "flexmonsterLayoutController" : "codeModalController";

          var modalView = (vm.editingComponent.options.template == "LinxFlexmonster.html") ?
            "App/screen-builder/modals/save-layout-flexmonster/save-layout-flexmonster.modal.html" : 'App/screen-builder/directivies/html-editor/codeModal.html';

            var modalInstance = $modal.open({
                size: 'lg',
                windowClass: 'codeModal',
                keyboard: false,
                backdrop: 'static',
                templateUrl: modalView,
                controller: modalController,
                resolve: {
          component: function() {
                        return vm.editingComponent;
                    }
                }
            });

      modalInstance.result.then(function(component) {
              vm.editingComponent = component;
        if (component.options.template == "LinxFlexmonster.html") {
                vm.setLayoutOptions();
              }
      }, function() {
                //$log.info('Modal dismissed at: ' + new Date());
            });
        };

    vm.editEvent = function() {
            $rootScope.$emit('componentEventEdit');
    };

        vm.setLayoutOptions = function() {
          flexmonsterLayoutService.getLayoutFiles()
            .then(function(files) {
          var layout = vm.editingFields.properties.filter(function(item) {
            return item.key == 'layoutSelected';
          });
          if (layout && layout.length) {
                  layout[0].templateOptions.options = flexmonsterLayoutService.getLayoutOptions(files);
                } else {
                  vm.editingFields.properties.push({
                    "key": "layoutSelected",
                    "type": "select",
              "templateOptions": {
                      "label": "Layout",
                      "options": flexmonsterLayoutService.getLayoutOptions(files),
                      "valueProp": "value",
                      "labelProp": "label",
                      "placeholder": ""
                    }
                  });
                }
          });
        };

        vm.setOdataOptions = function() {
          if(vm.odataEntites && vm.odataEntites.length) {

            vm.editingComponent.options.odataEntites = vm.odataEntites;
            var currentOdataProperty = vm.editingFields.properties.filter(function(item){ return item.key == 'odataEntity'; });

            if(!currentOdataProperty.length) {
            vm.editingFields.properties.push({
              "key": "odataEntity",
              "type": "select",
          "templateOptions": {
                "label": "Odata",
                "options": vm.odataEntites,
                "valueProp": "ClassName",
                "labelProp": "ClassName",
                "placeholder": ""
              }
            });
            } else {
              currentOdataProperty[0].templateOptions.options = vm.odataEntites;
            }
          }
        };

        vm.setEditingFieldsFlexmonster = function() {
          vm.showEditingHtml = true;
          vm.editingHtmlLabel = "Gerar Layout";

          vm.setOdataOptions();
          vm.setLayoutOptions();
        };

        $scope.$on('$destroy', odataGetProperties);
        $scope.$on('$destroy', formPropertiesEdit);
        $scope.$on('$destroy', odataEditComponent);
  }

})(_);
