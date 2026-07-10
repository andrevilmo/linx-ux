(function(_) {
  'use strict';

  var controllerId = 'formBuilder';

  angular
    .module('FormBuilder')
    .controller(
      controllerId, ['$rootScope', '$scope', '$filter', '$modal', '$sce', 'componentsService', 'generatorService', 'file', 'formPropertiesDefault', 'projectVariables', 'currentProject', 'toaster',
        formBuilder
      ]
    );

  function formBuilder($rootScope, $scope, $filter, $modal, $sce, componentsService, generatorService, file, formPropertiesDefault, projectVariables, currentProject, toaster) {
    /* jshint validthis: true */
    var vm = this;

    /*
        Visual variables (toggles, preview)
    */
    vm.classe = ['col-lg-7', 'col-md-7'];
    vm.toggleComponents = false;
    vm.toggleOdata = false;

    /*
        Components (web, mobile, active)
    */
    vm.activeComponents = [];

    /*
        screen properties relative for example titles and toolbar visibility
    */
    vm.formProperties = formPropertiesDefault;

    /*
        list of components to drag and drop
        data in Data/components.json
    */
    vm.listComponents = [];

    var saveFile = $rootScope.$on('saveFile', function() {
      save();
    });

    //TENTAR ARRANCAR
    $scope.$watch('vm.toggleComponents', function(value) {
      vm.toggle('');
    });

    vm.toggle = function(objToggle) {
      switch (objToggle) {
        case 'components':
          vm.toggleComponents = vm.toggleComponents ? false : true;
          break;
        case 'odata':
          vm.toggleOdata = vm.toggleOdata ? false : true;
          break;
      }

      if (vm.toggleComponents && vm.toggleOdata) {
        vm.classe = ['content-semi-full'];
        return;
      }

      if (vm.toggleComponents) {
        vm.classe = ['content-semi-half'];
        return;
      }

      if (vm.toggleOdata) {
        vm.classe = ['content-semi-half-plus'];
        return;
      }

      vm.classe = ['col-lg-7', 'col-md-7'];
    };

    function save() {

      var pathJson = currentProject.urlPathProject + "\\" + currentProject.currentFile;
      //console.log(pathJson);

      //Screen JSON
      var screenFile = JSON.stringify({
        screenComponents: vm.activeComponents,
        screenProperties: vm.formProperties
      });

      //Saving JSON
      file.save(pathJson, screenFile).then(function(returnProjectSaved) {
        toaster.pop('success', "Metadado", "Arquivo salvo com sucesso");
      });

      //Extract a name of view file
      var pathHtml = pathJson.split("\\").pop().replace('.json', '');
      pathHtml = currentProject.viewPath(pathHtml);

      //Saving view
      generatorService.generateHtml(vm.activeComponents, vm.formProperties).then(function(generatedHtml) {

        file.save(pathHtml, generatedHtml).then(function(returnHtmlSaved) {
          toaster.pop('success', "View", "Arquivo salvo com sucesso");
        });

      });

    }

    //Initializer
    function init() {
      //vm.enabledPreview = projectVariables.urlPreview == undefined ? true : false;

      componentsService.getComponents().then(function(data) {
        vm.listComponents = $filter('orderBy')(data, 'title', false);
      }, function(error) {
        console.log('Erro ao obter componentes: ' + error);
      });

      if (currentProject.currentFile) {
        file.read(currentProject.urlPathProject + "\\" + currentProject.currentFile, "utf8").then(function(project) {
          project = JSON.parse(project);

          if (project.screenComponents) {
            vm.activeComponents = project.screenComponents;
            vm.formProperties = project.screenProperties;
          }
        });
      }
    }

    init();

    $scope.$on('$destroy', saveFile);

  }

})(_);
