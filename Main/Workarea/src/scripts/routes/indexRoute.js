//------------------------------------------------------------------------------
//  Auto Generated
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
//------------------------------------------------------------------------------
//  Creation date: 14/06/2017 12:26:26
//  User name: marcos.cerqueira
//------------------------------------------------------------------------------
//  Linx AppBuilder: 2.0.42
//  Linx AppBuilder Designer: 1.0.69
//  Linx AppBuilder Service: 1.0.70
//------------------------------------------------------------------------------

'use strict';

function Routing($stateProvider) {

    $stateProvider
        .state('Workarea.index', {
            type: 'transaction',
            url: '^/',
            displayName: 'index',
            views: {
                '': {
                    templateProvider: function($q) {
                        return $q(function(resolve) {
                            require.ensure([], function(require) {
                                var template = require('../../templates/layouts/masterLayoutTemplate.html');
                                resolve(template);
                            }, "workarea");
                        });
                    }
                },
                'container@Workarea.index': {
                    controller: 'indexController as vm',
                    templateProvider: function($q) {
                        return $q(function(resolve) {
                            require.ensure([], function(require) {
                                var template = require('../../templates/indexTemplate.html');
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