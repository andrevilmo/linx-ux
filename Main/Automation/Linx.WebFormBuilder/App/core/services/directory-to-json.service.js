(function () {
    'use strict';

    angular
      .module('FormBuilder')
      .service('directoryToJsonService', directoryToJsonService);

    //directoryToJsonService.$inject = [];

    function directoryToJsonService() {
      var fs = require('fs');
      var path = require('path');

      var service = {
          execute: dirTree
      };

      return service;

      function dirTree(filename) {
          try { var stats = fs.lstatSync(filename),
              info = {
                  path: filename,
                  name: path.basename(filename)
              };

          if (stats.isDirectory() && filename.indexOf("node_modules") == - 1) {
              info.type = "directory";
              info.children = fs.readdirSync(filename).map(function(child) {
                  return dirTree(filename + '/' + child);
              });
          } else {
              info.type = "file";
          }
          return info;
        }
        catch(err){
          console.log("Pasta Vazia!");
        }
    }
  }
})();
