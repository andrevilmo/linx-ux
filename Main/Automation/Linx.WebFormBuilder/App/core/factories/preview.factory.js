(function() {
  'use strict';

  angular
    .module('FormBuilder')
    .service('previewFactory', ['$q', '$rootScope', 'currentProject', '$location', 'toaster', previewFactory]);

  function previewFactory($q, $rootScope, currentProject, $location, toaster) {

    var child = {};
    var pids = [];
    var isPreviewing = false;

    this.getPreviewState = function() {
      return isPreviewing;
    };

    this.stop = function() {
      pids.forEach(function(pid) {
        console.log('Killing task pid: ' + pid);

        var exec = require('child_process').exec;
        exec('taskkill /PID ' + pid + ' /F /T');
      });
      isPreviewing = false;
    };

    this.execute = function(mode) {
      var deferred = $q.defer(),
          promisseNpm = $q.defer(),
          promisseBower = $q.defer();

      pids = [];

      executeNpm().then(function(result) {
        promisseNpm.resolve(result);
      }, function(error) {
        promisseNpm.reject('\\node_modules');
      }, function(notify) {
        var retorno = {
          type: 'npm',
          message: notify
        };

        deferred.notify(retorno);
      });

      executeBower().then(function(result) {
        promisseBower.resolve(result);
      }, function(error) {
        promisseBower.reject('\\app\\lib');
      }, function(notify) {
        var retorno = {
          type: 'bower',
          message: notify
        };

        deferred.notify(retorno);
      });

      $q.all([promisseNpm.promise, promisseBower.promise]).then(function(results) {

        executeGulp(mode).then(function(result) {
          deferred.resolve(result);
        }, function(error) {
          deferred.reject('\\www');
        }, function(notify) {
          var retorno = {
            type: 'gulp',
            message: notify
          };

          deferred.notify(retorno);
        });
      }, function(error) {
        deferred.reject(error);
      });

      return deferred.promise;
    };

    function executeNpm() {
      var deferred = $q.defer();

      var exec = require('child_process').exec;

      child = exec('npm install -d', { cwd: currentProject.urlPathProject, maxBuffer: 1024 * 1000 });

      console.log('\nNPM pid: ' + child.pid);
      pids.push(child.pid);

      child.stdout.on('data', function(data) {
        deferred.notify(data);
      });

      child.stderr.on('data', function(data) {
        deferred.notify(data);
      });

      child.on('close', function(code) {
        if (code) {
          deferred.reject(code);
        } else {
          deferred.resolve(code);
        }
      });

      return deferred.promise;
    }

    function executeBower() {
      var deferred = $q.defer();

      var bower = require('bower');

      bower
        .commands
        .install(undefined, undefined, { cwd: currentProject.urlPathProject })
        .on('log', function(log) {
          deferred.notify(log.message + '\n');
        })
        .on('error', function(error) {
          deferred.reject(error + '\n');
        })
        .on('end', function(results) {
          deferred.resolve(results + '\n');
        });

      return deferred.promise;
    }

    function executeGulp(mode) {
      var deferred = $q.defer();

      if (mode == "builder") {
        executeBuilder().then(function(result) {
          isPreviewing = true;
          deferred.resolve(result);
        }, function(error) {
          deferred.reject(error);
        }, function(notify) {
          deferred.notify(notify);
        });
      } else {
        executeBrowser().then(function(result) {
          isPreviewing = true;
          deferred.resolve(result);
        }, function(error) {
          deferred.reject(error);
        }, function(notify) {
          deferred.notify(notify);
        });
      }

      return deferred.promise;
    }

    function executeBuilder() {
      var deferred = $q.defer();

      var exec = require('child_process').exec;

      child = exec('cd /d ' + currentProject.urlPathProject + ' & gulp dev & ionic serve -b --address localhost -p 8080', {
        maxBuffer: 1024 * 1000
      });

      console.log('ExecBuilder pid: ' + child.pid);
      pids.push(child.pid);

      child.stdout.on('data', function(data) {
        deferred.notify(data);

        //Quando o ionic serve está ligado o processo não é liberado então precisamos retornar a promisse =/
        if (data.indexOf('Running dev server') != -1) {
          $location.path('/formbuilder/preview');
          $rootScope.$apply();
          deferred.resolve(true);
        }
      });

      child.stderr.on('data', function(data) {
        deferred.notify(data);
      });

      child.on('close', function(code) {
        if (code) {
          deferred.reject(code);
        } else {
          deferred.resolve(code);
        }
      });

      return deferred.promise;
    }

    function executeBrowser() {
      var deferred = $q.defer();

      var exec = require('child_process').exec;

      child = exec('cd /d ' + currentProject.urlPathProject + ' & gulp serveDev', {
        maxBuffer: 1024 * 1000
      });

      console.log('ExecBrowser pid: ' + child.pid);
      pids.push(child.pid);

      child.stdout.on('data', function(data) {
        deferred.notify(data);

        //Quando o ionic serve está ligado o processo não é liberado então precisamos retornar a promisse =/
        if (data.indexOf('Running dev server') != -1) {
          deferred.resolve(true);
        }
      });

      child.stderr.on('data', function(data) {
        deferred.notify(data);
      });

      child.on('close', function(code) {
        if (code) {
          deferred.reject(code);
        } else {
          deferred.resolve(code);
        }
      });

      return deferred.promise;
    }
  }

})();
