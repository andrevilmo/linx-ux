(function () {
    'use strict';

    angular
        .module('FormBuilder')
        .service('file', ['$q', '$filter', file]);

    function file($q, $filter) {
        var fs = require('fs');
        var vm = this;

        this.save = function (path, file) {
            var deferred = $q.defer();
            fs.writeFile(path, file, function (err) {
                if (err) {
                    deferred.reject(false);
                }
                deferred.resolve(true);
            });
            return deferred.promise;
        };

        this.read = function (path, encoding) {
            var deferred = $q.defer();

            fs.readFile(path, encoding, function (err, data) {
                if (err) {
                    deferred.reject(err);
                }

                deferred.resolve(data);
            });

            return deferred.promise;
        };

        this.exists = function (path) {
            var deferred = $q.defer();
            fs.exists(path, function (exists) {
                deferred.resolve(exists);
            });
            return deferred.promise;
        };

        this.remove = removeFile;

        function removeFile (path) {
          var deferred = $q.defer();
          var promises = [];

          if(fs.existsSync(path) && fs.lstatSync(path).isFile()) {
            fs.unlink(path, function (err) {
              if (err) {
                deferred.reject(false);
              }
              deferred.resolve(true);
            });
          } else {
            fs.readdirSync(path).forEach(function(file, index){
              promises.push(removeFile(path + "/" + file));
            });

            $q.all(promises).then(function() {
              fs.rmdir(path, function(err) {
                if (err) {
                  deferred.reject(false);
                }
                deferred.resolve(true);
              });
            });
          }

          return deferred.promise;
        }
    }
})();
