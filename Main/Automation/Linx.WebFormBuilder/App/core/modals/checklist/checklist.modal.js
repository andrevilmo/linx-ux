
angular.module('FormBuilder')
     .controller("checklistModalController", ['$scope', '$rootScope', function ($scope, $rootScope) {
         $scope.listItem = [];
         $scope.getItem = function () {

             if (localStorage.getItem('checklist')) {
                 $scope.listItem = JSON.parse(localStorage.getItem('checklist'))
             } else {
                 $scope.listItem = []
             }



         }
         $scope.getItem();

         $scope.setItem = function () {
             if ($scope.newTodo != null && $scope.newTodo != "") {

                 $scope.listItem.push({ value: $scope.newTodo, checked: false })
                 localStorage.setItem('checklist', JSON.stringify($scope.listItem));
                 $scope.newTodo = "";
             }
         };

         $scope.refreshItem = function () {
             localStorage.setItem('checklist', JSON.stringify($scope.listItem));
         };
         $scope.clear = function () {
             $scope.listItem = []
             localStorage.setItem('checklist', JSON.stringify($scope.listItem));
         }

         $scope.clearComplete = function () {
             $scope.listItem = $scope.listItem.filter(function (el) {
                 return !el.checked;
             })
             localStorage.setItem('checklist', JSON.stringify($scope.listItem));
         }
         $scope.coutComplete = function () {
             return $scope.listItem.filter(function (el) {
                 return el.checked;
             }).length
         }

         $scope.close = function () {
             localStorage.setItem('checklist', JSON.stringify($scope.listItem));
             $rootScope.modalInstance.dismiss();
         }

         $scope.removeItem = function (item) {
             $scope.listItem= $scope.listItem.filter(function (el) {
                 return el != item;
             });
             localStorage.setItem('checklist', JSON.stringify($scope.listItem));
         }
         $scope.setActive = function (obj) {
             obj.checked = false;
         }


     }]);