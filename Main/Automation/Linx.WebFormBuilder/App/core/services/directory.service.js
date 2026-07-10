(function() {
	'use strict';

	angular
		.module('FormBuilder')
		.service('directory', ['$q', directory]);

	function directory($q) {
		var fs = require('fs');

		this.readdir = function(path) {
			var  deferred = $q.defer();
			fs.readdir(path, function(err, files){
				if(err){
					deferred.reject(err);
				} else {
					deferred.resolve(files);
				}
			});
			return deferred.promise;
		};

		this.create = function(path) {
			var deferred = $q.defer();

			fs.mkdir(path, function(err) {
				if (err) {
					deferred.reject(false);
				}

				deferred.resolve(true);
			});

			return deferred.promise;
		};

		this.existsSync = function(path) {
			return fs.existsSync(path);
		};

		this.createIfNotExist = function (path) {
		    var deferred = $q.defer();

		    if (!fs.existsSync(path)) {		        

		        fs.mkdir(path, function (err) {
		            if (err) {
		                deferred.reject(false);
		            }

		            deferred.resolve(true);
		        });
		    } else {
		        deferred.reject({
		            status: false,
		            message: "Pasta já existe!"
		        });
		    }


		    return deferred.promise;
		};


		this.mkdirSync = function(path) {
			fs.mkdirSync(path);
		};
	}
})();
