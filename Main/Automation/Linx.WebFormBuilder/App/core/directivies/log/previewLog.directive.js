//#region [DIRECTIVE] TabSetLog

angular.module('FormBuilder').directive('tabsetLog', ['$modal', function ($modal) {
    return {
        restrict: 'E',
        transclude: true,
        scope: {},
        controller: ["$scope", function ($scope) {
            var modalInstance;
            //#region Log
            $scope.$parent.log = {
                //#region Open
                open: function () {
                    modalInstance = $modal.open({
                        templateUrl: 'app/core/modals/preview/preview.log.html',
                        controller: 'previewLogController as vm',
                        size: 'lg',
                        backdrop: 'static',
                        resolve: {
                            'mode': function () {
                                return {
                                    panes: $scope.panes,
                                    parent: $scope.$parent,
                                    cancel: $scope.$parent.log.cancel
                                };
                            }
                        }
                    });
                    Init();

                },
                //#endregion
                //#region AppendText

                appendText: function (obj) {
                    $scope[obj.id].value += obj.text;
                },
                //#endregion Cancel
                //#region Cancel
                cancel: function () {
                    var cancel = $scope.$parent.log.config.cancel;
                    if (!cancel) return false;
                    cancel();
                    $scope.$parent.log.close();
                },  //#endregion   Cancel
                close: function () {
                    for (var i = 0; i < $scope.panes.length; i++) {
                        var pane = $scope.panes[i];
                        pane.value = "";
                    }

                    modalInstance.dismiss();
                    $scope.$parent.vm.loader = false;
                },

                //#region Config
                config: {
                    cancel: undefined
                },
                //#endregion

                panes: []
            };
            //#endregion log
            //#region Init
            function Init() {
                for (var i = 0; i < $scope.panes.length; i++) {
                    var pane = $scope.panes[i];
                    pane.value = "";
                    $scope[pane.id] = pane;
                }
            }
            //#endregion Init

            $scope.panes = [];

            $scope.select = function (pane) {
                angular.forEach($scope.panes, function (pane) {
                    pane.selected = false;

                });
                pane.selected = true;
            };

            this.addPane = function (pane) {
                if ($scope.panes.length === 0) $scope.select(pane);
                $scope.panes.push(pane);
            };
        }],
        template: '<div ng-transclude>a</div>',
        replace: true
    };
}]).
//#endregion [DIRECTIVE] TabSetLog
//#region [DIRECTIVE] TabLog
directive('tabLog', function () {
    return {
        require: '^tabsetLog',
        restrict: 'E',
        transclude: true,
        scope: {
            title: '@title',
            value: '@',
            id: '@id'
        },
        link: function (scope, element, attrs, tabsCtrl) {
            tabsCtrl.addPane(scope);
        },
        template: '<div class="tab-pane" ng-class="{active: selected}"></div>',
        replace: true
    };
});
//#endregion [DIRECTIVE] TabLog
