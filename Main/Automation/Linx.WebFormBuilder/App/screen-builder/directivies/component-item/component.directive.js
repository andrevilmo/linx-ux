(function () {
    'use strict';

    var directiveName = 'dtComponent';
    angular.module('FormBuilder').directive(directiveName, component);

    function component() {
        return {
            scope: {
                options: '=options'
            },
            link: function (scope, $sce) {
                scope.template = 'app/screen-builder/directivies/component-item/resources/' + scope.options.template;
            },
            restrict: 'E',
            template: '<div ng-include="template"></div>'
        };
    };

})();