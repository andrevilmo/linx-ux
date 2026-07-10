//------------------------------------------------------------------------------
//  Creation date: 14/06/2017 14:20:47
//  User name: marcos.cerqueira
//------------------------------------------------------------------------------
//  Linx AppBuilder: 2.0.42
//  Linx AppBuilder Designer: 1.0.69
//  Linx AppBuilder Service: 1.0.70
//------------------------------------------------------------------------------

'use strict';

function NavbarDirective() {

    var directive = {
        bindToController: true,
        template: require('./navbarTemplate.html'),
        controller: require('./navbarController'),
        controllerAs: 'vm',
        restrict: 'AE',
        scope: {}
    };

    return directive;
}

module.exports = function(appModule) {
    appModule.directive('navbar', NavbarDirective);
};