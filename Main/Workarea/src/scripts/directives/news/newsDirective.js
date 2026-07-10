//------------------------------------------------------------------------------
//  Creation date: 29/06/2017 17:17:09
//  User name: marcos.cerqueira
//------------------------------------------------------------------------------
//  Linx AppBuilder: 2.0.42
//  Linx AppBuilder Designer: 1.0.69
//  Linx AppBuilder Service: 1.0.70
//------------------------------------------------------------------------------

'use strict';

function NewsDirective() {

    var directive = {
        bindToController: true,
        template: require('./newsTemplate.html'),
        controller: require('./newsController'),
        controllerAs: 'vm',
        restrict: 'AE',
        scope: {}
    };

    return directive;
}

module.exports = function(appModule) {
    appModule.directive('news', NewsDirective);
};