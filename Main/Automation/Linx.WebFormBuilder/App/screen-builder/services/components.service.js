(function () {
    'use strict';

    angular.module('FormBuilder')
        .service('componentsService', function ($http, $q) {

            this.getComponents = function() {
                var deferred = $q.defer();

                /*
                    Futuramente alterar para uma Web API trazendo as informações
                    do banco de dados
                */
                $http.get('app/screen-builder/resource/component-resource.js')
                    .success(function (data) {
                        deferred.resolve(angular.fromJson(data));
                    })
                    .catch(function () {
                        deferred.reject('Erro ao obter componentes.');
                    });

                return deferred.promise;
            };

        });

})();
