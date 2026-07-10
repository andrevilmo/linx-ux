'use strict';

function HomeController($scope, $controller, $cacheFactory) {
    var vm = this;
    vm.init = init;
    vm.nome = 'HomeController';

    function init() {
        console.log('homeController init()');

        var cache = $cacheFactory('cacheId');

        window.addEventListener('message', {
            handleEvent(e) {
                console.log('Loaded message');
                cache.put("linxData", e.data)
                window.LinxModules = e.data.Modulos;

                window.removeEventListener('message');
            }
        });


    }

    //console.log(angular.module("LinxAppTemplate")._invokeQueue);

    //if ($controllerProvider.has('homeCustomController')) {
    //angular.extend(this, $controller('homeCustomController', { $scope: $scope }));
    //}
}

module.exports = function(appModule) {
    appModule.controller('homeController', HomeController);
};