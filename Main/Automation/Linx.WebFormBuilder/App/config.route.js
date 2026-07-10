(function () {
    'use strict';

    var app = angular.module('FormBuilder');

    app.config(function ($stateProvider, $urlRouterProvider) {

        $urlRouterProvider.otherwise('/welcome');

        $stateProvider
            .state('welcome', {
                url: '/welcome',
                templateUrl: 'app/core/views/welcome.view.html'
            })
            .state('select-ProjectType', {
                url: '/select-ProjectType',
                templateUrl: 'app/core/views/select-ProjectType.view.html'
            })
            .state('new-project', {
                url: '/new-project',
                templateUrl: 'app/core/views/new-project.view.html'
            })
            .state('git-project', {
                url: '/git-project',
                templateUrl: 'app/core/views/git-project.view.html'
            })
            .state('formbuilder', {
                url: '/formbuilder',
                templateUrl: 'app/core/views/builder.view.html',
                abstract: true
            })
            .state('formbuilder.view', {
                url: '/view',
                views: {
                    "main@formbuilder": {
                        templateUrl: 'app/screen-builder/views/screen-builder.view.html'
                    }
                }
            })
            .state('formbuilder.js', {
                url: '/js',
                views: {
                    "main@formbuilder": {
                        templateUrl: 'app/script-builder/views/script-builder.view.html'
                    }
                }
            })
            .state('formbuilder.text', {
                url: '/text',
                views: {
                    "main@formbuilder": {
                        templateUrl: 'app/text-editor/views/text-editor.view.html'
                    }
                }
            })
            .state('formbuilder.preview', {
                url: '/preview',
                views: {
                    "main@formbuilder": {
                        templateUrl: 'app/screen-preview/view/preview.view.html'
                    }
                }
            });
    });

})();
