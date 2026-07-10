(function(_) {
  'use strict';

  angular
    .module('FormBuilder')
    .service('generatorJsService', function($q, $filter, currentProject) {
      var vm = this;
      var fs = require('fs');

      vm.getTemplate = function(type) {
        var deferred = $q.defer();

        var path = currentProject.templatePath(type);

        fs.readFile(path, "utf8", function(err, html) {
          if (err) {
            deferred.reject(false);
          }

          deferred.resolve(html);
        });

        return deferred.promise;
      };

      vm.getInjections = function(injectionsArray) {
        var stringInjections = "'$scope'";

        _.each(injectionsArray, function (value) {

          stringInjections += ",'" + value + "'";

        });

        return {
          headerInjection: stringInjections,
          constructorInjection: stringInjections.replace(/'/g, "")
        };
      };

      vm.getVariables = function(variablesArray) {
        var deferred = $q.defer();

        var variables = '';

        _.each(variablesArray, function (value) {
          variables += '\t\t\tvm.' + value.name + ' = ' + value.defaultValue + ';\n';
        });

        deferred.resolve(variables);

        return deferred.promise;
      };

      vm.getMethods = function(methodsArray) {
        var deferred = $q.defer();

        var method = '';

        _.each(methodsArray, function (value) {

          method += '\t\t\t';

          var args = value.args || '';

          switch (value.type) {
            case 'automatica':
              method += 'var ' + value.name + ' = (function(){\n';
              method += '\t\t\t' + value.fn + '\n';
              method += '\t\t\t})();\n';
              break;
            case 'escopo':

              method += 'vm.' + value.name + ' = function(' + args + ') {\n';
              method += '\t\t\t' + value.fn + '\n';
              method += '\t\t\t};\n';
              break;
            case 'simples':
              method += 'function ' + value.name + '(' + args + ') {\n';
              method += '\t\t\t' + value.fn + '\n';
              method += '\t\t\t}\n';
              break;
          }

        });

        deferred.resolve(method);

        return deferred.promise;
      };

      vm.generateScript = function(options) {
        var deferred = $q.defer();

        var template = vm.getTemplate(options.type),
            injections = vm.getInjections(options.injectionsArray),
            variables = vm.getVariables(options.variablesArray),
            methods = vm.getMethods(options.methods);

        $q.all([template, injections, variables, methods]).then(function (result) {
           var template = Handlebars.compile(result[0]);

           var binder = {
             constructorInjection: result[1].constructorInjection,
             headerInjection: new Handlebars.SafeString(result[1].headerInjection),
             variables: new Handlebars.SafeString(result[2]),
             method: new Handlebars.SafeString(result[3])
           };

           deferred.resolve(template(binder));
         });

         return deferred.promise;
      };
    });

})(_);
