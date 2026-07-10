(function (_) {
    'use strict';

    angular.module('FormBuilder').controller('codeModalController', function ($scope, $modalInstance, component) {

        $scope.component = component;

        // The modes
        $scope.modes = ['Html'];
        $scope.mode = $scope.modes[0];

        // The ui-ace option
        $scope.aceOption = {
            mode: $scope.mode.toLowerCase(),
            require: ['ace/ext/language_tools'],
            advanced: {
                enableSnippets: true,
                enableBasicAutocompletion: true,
                enableLiveAutocompletion: true,
            },
            onChange: aceChanged,
            onLoad: function (_ace) {
                if (component.options.html)
                    $scope.aceModel = component.options.html;

                // HACK to have the ace instance in the scope...
                $scope.modeChanged = function () {
                    _ace.getSession().setMode("ace/mode/" + $scope.mode.toLowerCase());
                };
                $scope.isLoad = true;
            }
        };

        function aceChanged () {
          if($scope.isLoad) {
            $scope.isLoad = false;
            return;
          }
          currentProject.fileChanged = true;
        }

        $scope.ok = function () {
            component.options.html = $scope.aceModel;
            $modalInstance.close(component);
        };

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    });


})(_);
