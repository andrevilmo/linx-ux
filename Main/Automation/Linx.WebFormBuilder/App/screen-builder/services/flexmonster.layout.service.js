(function(){
  'use strict';

  angular.module('FormBuilder').service('flexmonsterLayoutService',['$q','file', 'directory','currentProject', FlexmonsterLayoutService]);
  function FlexmonsterLayoutService($q, file, directory, currentProject) {
    var self = this;

    self.getLayoutFiles = function() {
      var path = self.getPath();

      (directory.existsSync(path) || directory.mkdirSync(path));

      return directory.readdir(path);
    };

    self.saveLayout = function(containerId, fileName, odataEntity) {
      var deferred = $q.defer();
      var fileInfo = self.getFileInfo(containerId, fileName, odataEntity);

      (directory.existsSync(fileInfo.path) || directory.mkdirSync(fileInfo.path));

      getReport(containerId).then(
        function(report) {
          file.save(fileInfo.path + '\\' + fileInfo.name, report).then(
            function(success) {
              var instance = flexmonster.getInstanceByPivotName(containerId);
              deferred.resolve(instance.getReport());
            },
            function(err) {
              deferred.reject(err);
            });
        },
        function(err) {
          deferred.reject(err);
        });

      return deferred.promise;
    };

    self.getPath = function() {
      return currentProject.urlPathProject +
        '\\app\\flexmonster_layouts\\'+ currentProject.getCurrentFileName() + '\\';
    };

    self.getLayoutOptions = function(files) {
      var pageDirectory = currentProject.getCurrentFileName();

      var layoutFiles = files.map(function(item){ return { value: pageDirectory + '\\' + item, label: item  }; });
      layoutFiles.unshift({value: "", label: "selecione..."});

      return layoutFiles;
    };

    self.getFileInfo = function(containerId, fileName, odataEntity) {
      return { path: self.getPath(), name: getFileName(odataEntity, fileName) };
    };

    self.getSlice = function(report) {
      return {
        rows: mapSlice(report.rows),
        columns: mapSlice(report.columns),
        measures: mapSlice(report.measures, true)
      };
    };

    self.getViewType = function(containerId) {
      var instance = flexmonster.getInstanceByPivotName(containerId);

      var options = instance.getOptions();

      return { type: options.viewType, chartType: options.chartType };
    };

    var mapSlice = function(data, isMeasure) {
      var current = [];
      if(data && data.length){
        if(isMeasure) { data = data.filter(function(item) { return item.active; }); }
        current = data.map(function(item) { return { uniqueName: item.uniqueName}; });
      }
      return current;
    };

    var getReport = function(containerId) {
      var deferred = $q.defer();
      var xml2js = require('xml2js');
      var parser = new xml2js.Parser();
      var builder = new xml2js.Builder();
      var instance = flexmonster.getInstanceByPivotName(containerId);

      parser.parseString(instance.getReport('xmlstring'),
        function(err, result) {
          if(err) {
            deferred.reject(err);
          } else if(result.config.params && result.config.params.length) {
            var configuratorActive = getParamByName(result, 'configuratorActive');
            var configuratorButton = getParamByName(result, 'configuratorButton');

            if(configuratorActive) configuratorActive[0]._ = "false";
            if(configuratorButton) configuratorButton[0]._ = "false";

            var xml = builder.buildObject(result);

            deferred.resolve(xml);
          }
      });

      return deferred.promise;
    };

    var getParamByName = function(report, attrName) {
      var param = null;

      if(report &&
        report.config.params &&
        report.config.params.length &&
        report.config.params[0].param &&
        report.config.params[0].param.length) {
        param = report.config.params[0].param.filter(function(item) { return item.$.name == attrName; });
      }

      return param;
    };

    var getFileName = function(odataEntity, fileName) {
      odataEntity = (!odataEntity) ? '' : odataEntity + '_';
      return odataEntity + fileName +'.xml';
    };
  }
})();
