//------------------------------------------------------------------------------
//  Creation date: 27/06/2017 20:37:43
//  User name: marcos.cerqueira
//------------------------------------------------------------------------------
//  Linx AppBuilder: 2.0.42
//  Linx AppBuilder Designer: 1.0.69
//  Linx AppBuilder Service: 1.0.70
//------------------------------------------------------------------------------

'use strict';

function DirectorController($scope, $state, $stateParams, imageService) {
    var vm = this;
    vm.title = $state.current.displayName;
    vm.init = init;

    vm.image = imageService;

    function init() {}



}

module.exports = function(appModule) {
    appModule.controller('directorController', DirectorController);
};