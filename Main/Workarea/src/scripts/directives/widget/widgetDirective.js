//------------------------------------------------------------------------------
//  Creation date: 13/07/2017 14:18:49
//  User name: marcos.cerqueira
//------------------------------------------------------------------------------
//  Linx AppBuilder: 2.0.42
//  Linx AppBuilder Designer: 1.0.69
//  Linx AppBuilder Service: 1.0.70
//------------------------------------------------------------------------------

'use strict';

function WidgetDirective() {

    var directive = {
        bindToController: true,
        template: require('./widgetTemplate.html'),
        controller: require('./widgetController'),
        controllerAs: 'vm',
        restrict: 'AE',
        scope: {}
    };

    return directive;
}

module.exports = function(appModule) {
    appModule.directive('widget', WidgetDirective);
};