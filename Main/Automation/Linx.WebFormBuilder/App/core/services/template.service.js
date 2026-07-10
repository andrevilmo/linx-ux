(function () {
    'use strict';

    function template($q, projectVariables) {

        var exec = require('child_process').exec;
        var child = {};
        var deferred;
        var _path;        
        var _project
        var _type;
        var sucess = false;
        this.download = function (path, type, project) {
            deferred= $q.defer();
            _path = path;
            _project = project;
            _type = type;

            var urlProject;
            var gitCommand = '';

            if (type === 'blank') {
                urlProject = projectVariables.git;
                gitCommand = 'cd..  & git clone --branch v1.0 ' + urlProject + ' ' + path;
            } else if (type === 'blankBreezeCore') {
                urlProject = projectVariables.gitBreezeCore;
                gitCommand = 'cd.. & git clone --branch v1.0 ' + urlProject + ' ' + path;
            } else {
                urlProject = project;
                gitCommand = 'cd ' + path + ' & git clone ' + urlProject;
            }

            child = exec(gitCommand);


            child.stdout.on('data', function (data) {
                deferred.notify(data);

            });

            child.stderr.on('data', function (data) {
                deferred.notify(data);
            });



            child.on('close', function (code) {
                if (code) {
                    deferred.resolve(false);
                    sucess = false;
                } else {
                    deferred.resolve(true);
                    sucess = true;
                   if(type) exec('cd /d ' + path + ' &  rmdir /S /Q ".git"');
                }
            });

            return deferred.promise;
        };

        this.cancel = function () {
            var pathGit = "";
            if (!_type) {
                var pathProj = _project.split('/');
                pathGit = "/"+pathProj[pathProj.length - 1].split('.git')[0];
                var a;
            }

            exec('taskkill /PID ' + child.pid + ' /F /T');

            setTimeout(function myfunction() {              
                if (!sucess) exec('rmdir /S /Q "' + _path + pathGit + '"');
                
            },300);
           
            deferred.resolve(false);

        }
    }

    angular
     .module('FormBuilder')
     .service('template', ['$q', 'projectVariables', template]);

})();
