//------------------------------------------------------------------------------
//  Creation date: 14/06/2017 14:30:55
//  User name: marcos.cerqueira
//------------------------------------------------------------------------------
//  Linx AppBuilder: 2.0.42
//  Linx AppBuilder Designer: 1.0.69
//  Linx AppBuilder Service: 1.0.70
//------------------------------------------------------------------------------

'use strict';

function SidebarDirective() {

    var directive = {
        bindToController: true,
        template: require('./sidebarTemplate.html'),
        controller: require('./sidebarController'),
        controllerAs: 'vm',
        restrict: 'AE',
        scope: {}
    };

    return directive;
}

module.exports = function(appModule) {
    appModule.directive('sidebar', SidebarDirective);
};