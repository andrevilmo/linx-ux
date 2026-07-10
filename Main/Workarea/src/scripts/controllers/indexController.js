//------------------------------------------------------------------------------
//  Creation date: 14/06/2017 12:26:26
//  User name: marcos.cerqueira
//------------------------------------------------------------------------------
//  Linx AppBuilder: 2.0.42
//  Linx AppBuilder Designer: 1.0.69
//  Linx AppBuilder Service: 1.0.70
//------------------------------------------------------------------------------

'use strict';

function IndexController($scope, $state, $stateParams, $cacheFactory, $window, $timeout, imageService) {
    var vm = this;
    vm.title = $state.current.displayName;
    vm.init = init;
    vm.isCollapsed = true;
    vm.tab = 1;

    vm.groupNames = [{
        'key': 1,
        'text': 'Vendedor'
    }, {
        'key': 2,
        'text': 'Adminstrador'
    }, {
        'key': 3,
        'text': 'Diretor'
    }];
    vm.groupId = vm.groupNames[0];

    vm.isAdministrator = vm.isSeller = vm.isDirector = false;
    vm.UserName = 'Teste';
    vm.groupName = 'Teste';
    vm.principalModuleId = 0;
    vm.principalModule = null;
    vm.modules = [];

    $window.addEventListener('message', messageHandle);
    vm.redirectByUrl = redirectByUrl;
    vm.navigateByModule = navigateByModule;
    vm.setTab = setTab;
    vm.isSet = isSet;
    vm.image = imageService;
    vm.groupChanged = groupChanged;

    require('../../templates/administratorTemplate.html');
    require('../../templates/SellerTemplate.html');
    require('../../templates/directorTemplate.html');
    var cache = $cacheFactory('cacheId');


    function messageHandle(e) {
        $timeout(function() {
            $window.lxNavigate = function(path) {
                e.source.postMessage({
                    action: 'navigate',
                    details: '#' + path
                }, e.origin);
            };
            $window.setTitle = function(title) {
                e.source.postMessage({
                    action: 'title',
                    details: title
                }, e.origin);
            };


            console.log('Loading message');
            cache.put("linxData", e.data);

            var id = 0;
            if (e.data.loginInfo.UsuarioAutenticacao === 'admin') //'ricardo.muniz')
                id = 3;
            else if (e.data.loginInfo.UsuarioAutenticacao === 'admin')
                id = 2;
            else id = 1;

            vm.groupId = vm.groupNames[id - 1];
            groupChanged(id);


            vm.UserName = e.data.loginInfo.NomeUsuario;
            $window.LinxModules = e.data.Modules;
            getFavoritesIcons(vm.modules);
            vm.allModules = $window.LinxModules;


            $window.removeEventListener('message', messageHandle);
            console.log('Loaded message');
        }, 10);
    }


    function init() {

    }

    function groupChanged(id) {
        vm.isAdministrator = vm.isSeller = vm.isDirector = false;

        if (!id)
            id = vm.groupId.key;
        if (id === 3) //'ricardo.muniz')
            vm.isDirector = true;
        else if (id === 2)
            vm.isAdministrator = true;
        else vm.isSeller = true;

        $window.setTitle(vm.groupNames[id - 1].text);
    }

    function setTab(newTab) {
        vm.tab = newTab;
    }

    function isSet(tabNum) {
        return vm.tab === tabNum;
    }

    function getFavoritesIcons(arrayViews) {
        var recursive = null;
        recursive = function(_ms, classIcon) {
            for (var i = 0; i < _ms.length; i++) {
                var _m = _ms[i];
                classIcon = _ms.ClassIcon ? _m.ClassIcon : classIcon
                vm.principalModule = _m;
                if (_m.UrlRoute !== null)
                    arrayViews.push(_m);
                if (_m.Menus.length > 0) recursive(_m.Menus, classIcon);
            }
        };

        if ($window.LinxModules.length > 0)
            recursive($window.LinxModules[0].Menus);

        return arrayViews;
    }

    function redirectByUrl(url) {
        $window.open(url, '_blank');
    }

    function navigateByModule(module) {
        window.lxNavigate(module.UrlRoute);
    }
}

module.exports = function(appModule) {
    appModule.controller('indexController', IndexController);
};