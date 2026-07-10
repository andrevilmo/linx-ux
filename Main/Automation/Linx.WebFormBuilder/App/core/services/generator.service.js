(function (_) {
    'use strict';

    /*
        Injetar o serviço de manipulção de arquivo
    */
    angular.module('FormBuilder')
        .service('generatorService', function ($q, $filter) {
            var vm = this;
            var fs = require('fs');

            vm.getDefaultHtml = function (formProperties) {
                var deferred = $q.defer();

                fs.readFile("C:\\Linx Workspace\\Linx Framework\\Dev\\Binary\\WebFormBuilder\\resources\\default_app\\app\\core\\resources\\defaultTemplate.html", "utf8", function (err, html) {

                    Handlebars.registerHelper('ifvalue', function (conditional, options) {
                        if (options.hash.value === conditional) {
                            return options.fn(this);
                        } else {
                            return options.inverse(this);
                        }
                    });

                    var template = Handlebars.compile(html);

                    html = template(formProperties);

                    deferred.resolve(html);
                });

                return deferred.promise;
            };

            vm.generateForm = function (screenJson) {
                var deferred = $q.defer();

                var formHtml = '';

                var rows = _.groupBy(screenJson, 'row');

                _.each(rows, function (value) {

                    formHtml += '\t\t\t\t<div class="row">\n';

                    _.each($filter('orderBy')(value, 'col'), function (component) {

                        var source = fs.readFileSync("C:\\Linx Workspace\\Linx Framework\\Dev\\Binary\\WebFormBuilder\\resources\\default_app\\app\\core\\resources\\Components\\" + component.options.template, "utf8");
                        var template = Handlebars.compile(source);

                        if (component.options.displayColumns && component.options.displayFields) {
                            component.options.displayColumnsSafed = new Handlebars.SafeString(component.options.displayColumns);
                            component.options.displayNameColumnsSafed = new Handlebars.SafeString(component.options.displayNameColumns);
                        }

                        //Verificar se a correção funcionou
                        if (component.options.odata && component.options.odata.LookUpDisplayColumns && component.options.odata.LookUpColumns) {
                            component.options.odata.LookUpDisplayColumnsSafed = new Handlebars.SafeString(component.options.odata.LookUpDisplayColumns);
                            component.options.odata.LookUpColumnsSafed = new Handlebars.SafeString(component.options.odata.LookUpColumns);
                        }

                        //Componente de html (trocar nome do parametro)
                        if (component.options.html)
                        {
                            component.options.htmlSafed = new Handlebars.SafeString(component.options.html);
                        }

                        if (component.options.template == "DivBind.html") {
                            component.options.dataBind = new Handlebars.SafeString("{{ vm.dataBusiness.currentDataItem()." + component.options.odata.Name + "}}");
                        }

                        if (component.options.ngclass)
                        {
                          component.options.ngclass =  new Handlebars.SafeString(component.options.ngclass);
                        }

                        if(component.options.template == "LinxFlexmonster.html") {
                            component.options.pivotOptions = vm.getFlexmonsterOptions(component.options);
                        }

                        formHtml += '\t\t\t\t\t<div class="col">\n';
                        formHtml += '\t\t\t\t\t\t' + template(component) + '\n';
                        formHtml += '\t\t\t\t\t</div>\n';
                    });

                    formHtml += '\t\t\t\t</div>\n';
                });

                deferred.resolve(formHtml);

                return deferred.promise;
            };

            vm.getFlexmonsterOptions = function(componentOptions) {              
              var flexmonsterOptions = {
                measures: [],
                hierarchies: [],
                slice: componentOptions.slice,
                pivot: componentOptions.pivot,
                width: componentOptions.width,
                height: componentOptions.height,
                type: componentOptions.viewType,
                chartType: componentOptions.chartType,
                withToolbar: componentOptions.withToolbar,
                layoutSelected: componentOptions.layoutSelected
              };

              if(componentOptions.odataEntites && componentOptions.odataEntites.some(function(item){ return item.ClassName == componentOptions.odataEntity; })) {
                 var odataEntity = componentOptions
                  .odataEntites.filter(function(item){ return item.ClassName == componentOptions.odataEntity; });

                odataEntity[0].Properties.forEach(function(item) {
                  var currentItem = { uniqueName:item.Name, caption: item.Caption };
                  if(item.IsMeasure)
                    flexmonsterOptions.measures.push(currentItem);
                  else
                    flexmonsterOptions.hierarchies.push(currentItem);
                });
              }

              return new Handlebars.SafeString(JSON.stringify(flexmonsterOptions));
            };

            vm.generateHtml = function (screenJson, formProperties) {
                var deferred = $q.defer();

                vm.getDefaultHtml(formProperties)
                .then(function (template) {

                    vm.generateForm(screenJson)
                    .then(function (formHtml) {
                        formHtml = template.replace("@@FORM", formHtml);
                        deferred.resolve(formHtml);
                    }, function (err) {
                        deferred.reject('Erro ao gerar o formulário.');
                    });

                }, function (data) {
                    deferred.reject('Erro ao gerar HTML.');
                });

                return deferred.promise;
            };

        });

})(_);
