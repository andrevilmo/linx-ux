(function(_) {
  'use strict';

  angular
    .module('FormBuilder')
    .controller('newLinxObjectController', function($modalInstance, builder, odataService, file, currentProject, lxProjectService, toaster) {
      var vm = this;

      vm.entities = [];

      vm.Urls = [];

      vm.select = function(item) {
        if (item.atualizar) {
          item.selected = true;
        } else {
          item.selected = item.selected === true ? false : true;
        }
      };

      vm.selectUrl = function(url) {

        vm.entities = [];

        vm.urlEntities = "";

        url.selected = url.selected === true ? false : true;

        for (var i = 0; i < vm.Urls.length; i++) {
          if (vm.Urls[i].url != url.url) {
            vm.Urls[i].selected = false;
          } else if (url.selected) {
            vm.urlEntities = url.url + "GetEntities";
          }
        }
      };

      function init() {
        lxProjectService.getOdataEndpoints().then(function(content) {
          vm.Urls = _.map(content, function(item) {
            return {
              url: item,
              selected: false
            };
          });
        });
      }

      init();

      vm.serchEntities = function() {

        odataService.getMetaData(vm.urlEntities).then(function(data) {

          vm.entities = data;

          lxProjectService.getEntitiesByEndpoint(vm.urlEntities).then(function(serviceExists) {
            var _serviceExists = serviceExists;

            if (_serviceExists) {
              vm.entites = _.map(vm.entities, function(item) {
                var entityExist = _.indexOf(_serviceExists, item.Name);
                var itemEntityExist;

                if (entityExist != -1) {
                  itemEntityExist = _serviceExists[entityExist];
                }

                item.selected = false;
                item.false = true;

                if (itemEntityExist) {
                  item.selected = true;
                  item.atualizar = true;
                }

                return item;
              });
            }
          });
        }, function() {
          console.log('Erro ao carregar as entidades');
        });
      };

      vm.generate = function() {

        var selectedsEntites = _.where(vm.entities, {
          selected: true
        });
        var url = vm.urlEntities.replace('GetEntities', '');

        builder.createLinxObject(url, selectedsEntites).then(function(data) {

          lxProjectService.getProjectJSON().then(function(content) {

            var projectJson = content;
            var servicesList = projectJson.services || [];
            var entitiesNames = _.map(selectedsEntites, function(item) {
              return item.Name;
            });
            var currentService = _.findWhere(servicesList, {
              url: url
            }) || {
              url: url,
              entities: entitiesNames
            };

            currentService.entities = entitiesNames;

            servicesList = _.reject(servicesList, function(item) {
              return item.url == currentService.url;
            });

            servicesList.push(currentService);

            projectJson.services = servicesList;

            lxProjectService.saveProjectJSON(JSON.stringify(projectJson)).then(function() {
              toaster.pop('success', "Objeto de negócio", "Objeto criado com sucesso");
              $modalInstance.close();
            });

          });

        }, function(error) {
          $modalInstance.close();
        });
      };

      vm.cancel = function() {
        $modalInstance.dismiss('cancel');
      };
    });
})(_);
