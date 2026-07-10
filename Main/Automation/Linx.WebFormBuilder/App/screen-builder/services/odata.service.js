(function () {
    'use strict';

    angular.module('FormBuilder')
        .service('odataService', function ($http, $q) {

            this.getMetaData = function (url) {
                var deferred = $q.defer();

                $http.get(url)
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
