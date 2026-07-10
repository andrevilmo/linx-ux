(function (_) {
    'use strict';

    angular.module('FormBuilder')
      .directive('lxSolutionExplorer', [SolutionExplorer]);

    function SolutionExplorer() {
        var directive = {
            restrict: 'EA',
            templateUrl: 'app/core/directivies/solution-explorer/solution-explorer.directive.html',
            controller: SolutionExplorerController,
            controllerAs: 'vm',
            bindToController: true
        };

        return directive;
    }

    SolutionExplorerController.$inject = ['$scope', '$rootScope', 'directoryToJsonService', 'currentProject', '$modal', 'toaster', 'file', '$location'];

    function SolutionExplorerController($scope, $rootScope, directoryToJsonService, currentProject, $modal, toaster, file, $location) {
        var vm = this;

        var watchPath = '';
        var complementPath = '';
        var currentDataForTreeView = currentProject.getTreeViewState();

        vm.explorerSelectedMode = 'simple';

        vm.toggleExplorerMode = function (mode) {
            vm.dataForTheTree = {};

            if ($rootScope.openFile === true) {
                mode = currentDataForTreeView.mode;
                $rootScope.openFile = false;
            }

            if (currentDataForTreeView.treeView) {
                vm.dataForTheTree = currentDataForTreeView.treeView;
                vm.expandedNodes = currentDataForTreeView.expandedNodes;
            } else {
                vm.dataForTheTree = [];
            }

            vm.explorerSelectedMode = mode;

            switch (mode) {
                case 'advanced':
                    complementPath = '';
                    break;
                case 'simple':
                    complementPath = '\\app\\';
                    break;
                case 'builder':
                    complementPath = '\\builder\\view\\';
                    break;
                case 'css':
                    complementPath = '\\app\\css\\';
                    break;
                case 'view':
                    complementPath = '\\app\\views\\';
                    break;
                case 'controller':
                    complementPath = '\\app\\js\\controllers\\';
                    break;
            }

            watchPath = currentProject.urlPathProject + complementPath;
            vm.dataForTheTree = directoryToJsonService.execute(watchPath);
        };

        vm.toggleExplorerMode(vm.explorerSelectedMode);

        var oldSelectedValue = {};

        vm.treeOptions = {
            nodeChildren: "children",
            multiSelection: false
        };

        vm.showSelected = function (node, selected, $parentNode, $index) {
            if (_.isEqual(node, oldSelectedValue)) {
                currentProject.setTreeViewState({ treeView: vm.dataForTheTree, expandedNodes: vm.expandedNodes, mode: vm.explorerSelectedMode });
                $rootScope.openFile = true;
                var pathFile = node.path;
                node.path = pathFile.substr(pathFile.indexOf("/") + 1, pathFile.length);
                $rootScope.$emit('openFile', complementPath + node.path.replace("/", "\\"));
                oldSelectedValue = {};
            } else {
                oldSelectedValue = node;
                vm.openFile = false;
            }
        };

        vm.newModal = function (type) {
          var splitted = type.split('-');
          var capitalized = '';

            splitted.forEach(function (word) {
            capitalized += word[0].toUpperCase();
            capitalized += word.substr(1).toLowerCase();
          });

          var modalInstance = $modal.open({
                templateUrl: 'app/core/modals/new-' + type + '/new-' + type + '.modal.html',
              controller: 'new' + capitalized + 'Controller as vm'
          });
            modalInstance.result.then(function (data) {
                if (data) {
                    var filePath = data;
                    filePath = filePath.substr(filePath.lastIndexOf("app"));
                    $rootScope.$emit('openFile', filePath.replace("/", "\\"));
                }                
            });
        };

        vm.removeItem = function (path) {
          var modalInstance = $modal.open({
            templateUrl: 'app/core/modals/delete-file/delete-file.modal.html',
            controller: 'deleteFileController as vm'
          });

          modalInstance.result.then(function (option) {
                if (option) {
                  file.remove(path).then(function () {
                    vm.toggleExplorerMode(vm.explorerSelectedMode);
                    var file = replacePath(path);
                    if (file == currentProject.currentFile) {
                      currentProject.currentFile = null;
                      currentProject.fileChanged = false;
                      $location.path("/formbuilder/view");
                    }
                  });
                }
          });
        }

        function replacePath(path) {
          var file = "\\" + path.substring(path.indexOf('app'));
          var re = new RegExp("/", 'g');
          return file.replace(re, "");
        }

        vm.renameItem = function(node, selected, $parentNode, $index) {
          console.console.log("Não Implementado");
        }
    }
})(_);
