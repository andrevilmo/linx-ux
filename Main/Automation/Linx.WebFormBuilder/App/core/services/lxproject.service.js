(function (_) {
  'use strict';

  /*
    Manipula o arquivo .lxProj do projeto existente
  */
  angular.module('FormBuilder')
    .service('lxProjectService', lxProjectService);

  lxProjectService.$inject = ['$q', '$http', 'file', 'currentProject'];

  function lxProjectService ($q, $http, file, currentProject) {

    var service = {
      getProjectJSON: getProjectJSON,
      saveProjectJSON: saveProjectJSON,
      getOdataEndpoints: getOdataEndpoints,
      getEntitiesByEndpoint: getEntitiesByEndpoint,
      getPropertiesByEntity: getPropertiesByEntity
    };

    return service;

    function getProjectJSON () {
      var deferred = $q.defer();

      file.read(currentProject.urlProjectFile, 'utf8').then(function (data) {
        var projectJson = JSON.parse(data);
        deferred.resolve(projectJson);
      }, function (error) {
        deferred.reject(error);
      });

      return deferred.promise;
    }

    function saveProjectJSON (projectJson) {
      var deferred = $q.defer();

      file.save(currentProject.urlProjectFile, projectJson).then(function () {
        deferred.resolve(projectJson);
      }, function (error) {
        deferred.reject(error);
      });
      return deferred.promise;
    }

    function getOdataEndpoints () {
      var deferred = $q.defer();

      getProjectJSON().then(function (projectObject) {
        var servicesList = projectObject.services || [];

        var endpointList = _.map(servicesList, function (item) {
          return item.url;
        });

        deferred.resolve(endpointList);
      }, function (error) {
        deferred.reject(error);
      });

      return deferred.promise;
    }

    function getEntitiesByEndpoint (endpointUrl) {
      var deferred = $q.defer();

      endpointUrl = endpointUrl.replace('GetEntities', '');

      getProjectJSON().then(function (projectObject) {
        var endpointObject = _.findWhere(projectObject.services, { url: endpointUrl });

        if (endpointObject && endpointObject.entities) {
          deferred.resolve(endpointObject.entities);
        } else {
          deferred.reject('Não existe o endpoint ou não existem entidades no arquivo do projeto.');
        }
      }, function (error) {
        deferred.reject(error);
      });

      return deferred.promise;
    }

    function getPropertiesByEntity (endpointUrl, entityName) {
      var deferred = $q.defer();

      //Espera a url esteja no padrão: (com a barra do final)
      //http://localhost:1710/LinxOmniBusinessViewItem/
      endpointUrl = endpointUrl.replace('GetEntities', '');
      var url =  endpointUrl + 'GetMetaData?entityName=' + entityName + '&allComposition=false';

      $http.get(url)
          .success(function (data) {
              deferred.resolve(angular.fromJson(data));
          })
          .catch(function () {
              deferred.reject('Erro ao obter metadados a partir do endpoint.');
          });

      return deferred.promise;
    }
  }

})(_);
