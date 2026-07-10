(function(_) {
  'use strict';

  angular.module('FormBuilder')
    .service('projectService', projectService);

  projectService.$inject = ['$http', '$q', 'file'];

  function projectService($http, $q, file) {

    var service = {
      saveProject: saveProject,
      getProjects: getProjects,
      clearAll: clearAll,
      removeProject: removeProject,
      checkAndSave: checkAndSave
    };

    return service;

    function saveProject(project) {
      var projects = getProjects();
      projects.push(project);
      localStorage.setItem('projects', JSON.stringify(projects));
    }

    function getProjects() {
      var projectsString = localStorage.getItem('projects');
      projectsString = projectsString || "[]";

      return JSON.parse(projectsString);
    }

    function clearAll() {
      localStorage.removeItem('projects');
    }

    function removeProject(index) {
      var projects = getProjects();
      projects.splice(index, 1);
      localStorage.setItem('projects', JSON.stringify(projects));
    }

    function checkAndSave(path) {
      var deferred = $q.defer();

      var projectsList = getProjects();
      var projectExists = _.findWhere(projectsList, {
        path: path
      });

      if (!projectExists) {
        file.read(path, 'utf8').then(function(projectJSON) {
          projectJSON = JSON.parse(projectJSON);
          projectJSON.path = path;
          saveProject(projectJSON);

          deferred.resolve();
        }, function(error) {
          deferred.reject(error);
        });
      } else {
        deferred.resolve();
      }

      return deferred.promise;
    }
  }

})(_);
