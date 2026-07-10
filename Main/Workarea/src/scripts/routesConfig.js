'use strict';

require('@linx.uxmobile/linx-bootstrap/src/styles/main.scss');

var Loader = require('loader');

module.exports = function(layoutTemplate) {

    var moduleName = 'Workarea';

    var appModule = angular
        .module(moduleName + '.routing', [])
        .config(Routing);

    var packageJson = require("../../package.json");
    packageJson.metadata = require('json-loader!../../projectProperties.form').model.metadata;

    var context = require.context('./routes', false, /Route.js/);
    Loader.requireContext(context, appModule);

    function Routing($urlRouterProvider, $stateProvider) {
        $stateProvider
            .state('Workarea', {
                abstract: true,
                type: 'module',
                url: '/workarea',
                displayName: 'Workarea',
                pkg: packageJson,
                template: '<ui-view/>',
                resolve: {
                    loadModule: function($q, $ocLazyLoad) {
                        return $q(function(resolve) {
                            require.ensure([], function(require) {
                                require('../../moduleVendor.js');

                                var module = require('./ngModule');
                                $ocLazyLoad.load({
                                    name: module.name
                                });
                                resolve(module);
                            }, "workarea");
                        });
                    }
                }
            });
    }

    return appModule;
};