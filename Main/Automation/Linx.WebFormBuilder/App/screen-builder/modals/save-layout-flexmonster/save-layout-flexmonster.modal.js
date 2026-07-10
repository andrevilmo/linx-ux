(function () {
  'use strict';
  angular.module('FormBuilder').controller('flexmonsterLayoutController', FlexmonsterLayoutController);

  function FlexmonsterLayoutController($scope, $modalInstance, component, flexmonsterLayoutService, toaster) {
    $scope.toaster = toaster;
    $scope.flexmonsterLayoutService = flexmonsterLayoutService;

    $scope.component = {
      options: {
        measures: [],
        hierarchies: [],
        type: 'chart',
        chartType: 'bar',
        withToolbar: true,
        webFormBuilder: true,
        pivot: component.options.pivot,
        width: component.options.width,
        height: component.options.height,
        odataEntity: component.options.odataEntity,
        layoutPath: flexmonsterLayoutService.getPath(),
        label: "Flexmonster - " + component.options.pivot.caption,
        fileName: (component.options.layoutSelected) ? component.options.layoutSelected.split('\\').pop().replace('.xml', '') : "",
        layoutSelected: (component.options.layoutSelected) ? component.options.layoutSelected.split('\\').pop() : ""
      }
    };

    if(component.options.odataEntity) {
        var odataEntity = component.options
            .odataEntites.filter(function(item){ return item.ClassName == component.options.odataEntity });

        if(odataEntity && odataEntity.length) {
          odataEntity[0].Properties.forEach(function(item) {
            var currentItem = { uniqueName:item.Name, caption: item.Caption };
            if(item.IsMeasure)
              $scope.component.options.measures.push(currentItem);
            else
              $scope.component.options.hierarchies.push(currentItem);
          });
        }
    }

    $scope.ok = function () {
      if($scope.component.options.fileName) {
        var containerId = 'container-layout';
        $scope.flexmonsterLayoutService.saveLayout(containerId, $scope.component.options.fileName).then(
          function(report) {

            component.options.viewType = report.viewType;
            component.options.chartType = report.chartType;

            if(report.viewType == 'charts')
              component.options.slice = $scope.flexmonsterLayoutService.getSlice(report);

            $modalInstance.close(component);

            flexmonster.instances = 0;
            FlexmonsterLoader.instances = [];
            toaster.success('Arquivo salvo com sucesso.');
          },
          function(error) {
            toaster.error("Ocorreu um erro ao salvar o arquivo: " + err.message);
          });

      } else {
        toaster.error("preencha o nome do arquivo");
      }
    };

    $scope.cancel = function () {
      flexmonster.instances = 0;
      FlexmonsterLoader.instances = [];
      $modalInstance.dismiss('cancel');
    };
  };
})();
