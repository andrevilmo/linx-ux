//------------------------------------------------------------------------------
//  Auto Generated
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
//------------------------------------------------------------------------------
//  Creation date: 21/06/2017 11:47:32
//  User name: marcos.cerqueira
//------------------------------------------------------------------------------
//  Linx AppBuilder: 2.0.42
//  Linx AppBuilder Designer: 1.0.69
//  Linx AppBuilder Service: 1.0.70
//------------------------------------------------------------------------------

'use strict';

function Routing($stateProvider) {

    $stateProvider
        .state('Workarea.administrator', {
            type: 'transaction',
            url: '/administrator',
            displayName: 'administrator',
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
                'container@Workarea.administrator': {
                    controller: 'administratorController as vm',
                    templateProvider: function($q) {
                        return $q(function(resolve) {
                            require.ensure([], function(require) {
                                var template = require('../../templates/administratorTemplate.html');
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