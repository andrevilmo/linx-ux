'use strict';

function Routing($stateProvider) {
    $stateProvider
        .state('Workarea.home', {
            type: 'module',
            url: '/home',
            displayName: 'Home',
            views: {
                '': {
                    templateProvider: function($q) {
                        return $q(function(resolve) {
                            require.ensure([], function(require) {
                                var template = require(WP_HOST + '/src/templates/layoutTemplate.html');
                                resolve(template);
                            }, "workarea");
                        });
                    }
                },
                'container@Workarea.home': {
                    controller: 'homeController as vm',
                    templateProvider: function($q) {
                        return $q(function(resolve) {
                            require.ensure([], function(require) {
                                var template = require('../../templates/homeTemplate.html');
                                resolve(template);
                            }, "workarea");
                        });
                    }
                }
            }
        });

}

module.exports = function(appModule) {
    appModule.config(Routing);
};