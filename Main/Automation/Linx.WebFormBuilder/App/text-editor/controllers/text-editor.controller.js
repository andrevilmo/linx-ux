(function() {
  angular
    .module('FormBuilder')
    .controller('TextEditorController', TextEditorController);

  TextEditorController.$inject = ['$scope', '$rootScope', 'currentProject', 'file', 'toaster'];

  function TextEditorController($scope, $rootScope, currentProject, file, toaster) {
    var vm = this;

    vm.title = currentProject.currentFile || '';

    vm.file = '';

    if (vm.title.indexOf('.html') != -1) {
      vm.mode = 'html';
    }

    if (vm.title.indexOf('.js') != -1) {
      vm.mode = 'javascript';
    }

    if (vm.title.indexOf('.css') != -1 || vm.title.indexOf('.scss') != -1) {
      vm.mode = 'css';
    }

    // The ui-ace option
    vm.aceOption = {
        mode: vm.mode,
        require: ['ace/ext/language_tools'],
        advanced: {
            enableSnippets: true,
            enableBasicAutocompletion: true,
            enableLiveAutocompletion: true,
        },
        firstLineNumber: 1,
        onChange: aceChanged,
        onLoad: function (_ace) {
           //_ace.setTheme("ace/theme/terminal");
            // HACK to have the ace instance in the scope...
            $scope.modeChanged = function () {
                _ace.getSession().setMode("ace/mode/" + vm.mode);
            };
            vm.isLoad = true;
        }
    };

    function init() {
      file.read(currentProject.urlPathProject + "\\" + currentProject.currentFile, "utf8").then(function (text) {
        vm.file = text;
      });
      vm.changed = false;
    }

    init();

    var saveFile = $rootScope.$on('saveFile', saveChanges);

    vm.save = saveChanges;

    function saveChanges() {
      file.save(currentProject.urlPathProject + "\\" + currentProject.currentFile, vm.file).then(function (returnProjectSaved) {
          toaster.pop('success', "Arquivo", "Arquivo salvo com sucesso");
          vm.title = currentProject.currentFile || '';
      });
    }

    function aceChanged () {
      if(vm.isLoad) {
        vm.isLoad = false;
        return;
      }
      currentProject.fileChanged = true;
      vm.title = currentProject.currentFile + "*";
    }

    $scope.$on('$destroy', saveFile);
  }
})();
