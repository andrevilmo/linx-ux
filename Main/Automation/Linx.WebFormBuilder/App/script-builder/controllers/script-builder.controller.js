(function() {
  angular
    .module('FormBuilder')
    .controller('ScriptBuilder', scriptBuilder);

  scriptBuilder.$inject = ['$scope','$rootScope', 'currentProject', 'file', 'generatorJsService'];

  function scriptBuilder($scope, $rootScope, currentProject, file, generatorJsService) {
    var vm = this;

    vm.fileName = currentProject.currentFile || 'Documento sem título';

    vm.variablesList = [];
    vm.injectionsList = [];
    vm.functionList = [];

    var saveFile = $rootScope.$on('saveFile', saveChanges);

    vm.save = saveChanges;

    function saveChanges() {

      var pathJson = currentProject.urlPathProject + "\\" + currentProject.currentFile;

      var fileJson =  JSON.stringify({
        variables: vm.variablesList,
        injections: vm.injectionsList,
        functions: vm.functionList
      });

      file.save(pathJson, fileJson).then(function (returnProjectSaved) {
        console.log(returnProjectSaved);
      });

      var options = {
        type: 'controller',
        injectionsArray: vm.injectionsList,
        variablesArray: vm.variablesList,
        methods: vm.functionList
      };

      generatorJsService.generateScript(options).then(function (result) {
        console.log(result);
      });

    }

    function init() {
      if (currentProject.currentFile) {
        var pathJson = currentProject.urlPathProject + "\\" + currentProject.currentFile;

        file.read(pathJson, "utf8").then(function (code) {
          code = JSON.parse(code);

          if (code.variables) {
              vm.variablesList = code.variables;
              vm.injectionsList = code.injections;
              vm.functionList = code.functions;
          }
        });

      }
    }

    init();

    $scope.$on('$destroy', saveFile);
  }
})();
