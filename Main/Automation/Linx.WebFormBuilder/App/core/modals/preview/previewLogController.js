angular
  .module('FormBuilder')
  .controller('previewLogController', ['$scope', 'mode', '$modalInstance', previewLogController]);

function previewLogController($scope, mode, $modalInstance) {
    $scope.tabs = mode.panes;
    $scope.cancel = mode.cancel;
 
}
