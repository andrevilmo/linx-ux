(function () {
    angular
      .module('FormBuilder')
      .service('builder', ['file', 'currentProject', 'directory', '$http', '$q', '$rootScope', builder]);

    function builder(file, currentProject, directory, $http, $q, $rootScope) {
        vm = this;

        this.createFromTemplate = function (filename, type, userInput) {
            var newJsonFolder = currentProject.jsonFolderPath(type);
            file.read(currentProject.templatePath(type), 'utf8').then(function (content) {
                var template = Handlebars.compile(content)(userInput);
                file.save(currentProject.modulePath(filename, type), template).then(function () {
                    console.log('Arquivo ' + type + ' salvo com sucesso.');
                });
            });
            this.createJson(filename, newJsonFolder);
            return currentProject.modulePath(filename, type);
        };

        this.createNewBreezeService = function (filename, type, url) {
            var newJsonFolder = currentProject.jsonFolderPath(type);

            $http.get(url).then(function (response) {
                file.save(currentProject.modulePath(filename, type), response.data).then(function () {
                    console.log('Arquivo ' + type + ' salvo com sucesso.');
                });
            });
            this.createJson(filename, newJsonFolder);
            return currentProject.modulePath(filename, type);
        };

        this.createNewFactory = function (filename, type, url) {
            var newJsonFolder = currentProject.jsonFolderPath(type);

            $http.get(url).then(function (response) {
                file.save(currentProject.modulePath(filename, type), response.data).then(function () {
                    console.log('Arquivo ' + type + ' salvo com sucesso.');
                });
            });
            this.createJson(filename, newJsonFolder);
            return currentProject.modulePath(filename, type);
        };

        this.createNewView = function (filename) {
            file.save(currentProject.viewPath(filename), '').then(function () {
                console.log('Arquivo view salvo com sucesso.');
            });
            this.createJson(filename, currentProject.jsonFolderPath('view'));
            return currentProject.viewPath(filename);
        };
        
        this.createJson = function (filename, folder) {
            directory.create(folder);
            file.save(currentProject.jsonPath(filename, folder), '{}').then(function () {
                console.log('JSON salvo com sucesso.');
            });
        };

        this.createNewCss = function (filename) {
            file.save(currentProject.cssPath(filename), '').then(function () {
                console.log('Arquivo view salvo com sucesso.');
            });
            return currentProject.cssPath(filename);
        };

        /*
          Generate Linx breeze.js logic
          Services, domains and Factories
          url -> base url entites list (Ex: http://localhost:1710/LinxVendasCadastroBaseDS/)
          entities -> selecteds entites to generate
        */
        this.createLinxObject = function (url, entites) {
            var deferred = $q.defer();

            //Service
            var services = function () {
                var def = $q.defer();

                $http.get(url + 'GetClientService').then(function (response) {

                    file.save(currentProject.modulePath(response.data[0], 'service'), response.data[1]).then(function () {
                        //console.log('Arquivo ' + type + ' salvo com sucesso.');
                        def.resolve(true);
                    }, function (err) {
                        def.reject(err);
                    });

                }, function (err) {
                    def.reject(err);
                });

                return def.promise;
            };

            //Domains
            var domains = function () {
                var def = $q.defer();

                $http.get(url + 'GetClientDomains').then(function (response) {

                    file.save(currentProject.modulePath(response.data[0], 'factory'), response.data[1]).then(function () {
                        def.resolve(true);
                    }, function (err) {
                        def.reject(err);
                    });

                }, function (err) {
                    def.reject(err);
                });

                return def.promise;
            };

            //Factories
            //GetClientFactory?entityName=Produto
            var factories = function () {
                var def = $q.defer();

                var promissesHttp = [];

                _.each(entites, function (value) {
                    promissesHttp.push($http.get(url + 'GetClientFactory?entityName=' + value.Name));
                    promissesHttp.push($http.get(url + 'GetClientFactoryCustomEvents?entityName=' + value.Name));
                });

                $q.all(promissesHttp).then(function (retornoHttp) {
                    var promissesFile = [];
                    _.each(retornoHttp, function (value) {                        
                        if (value.data[0].indexOf('ExtendedFactory') < 0) {
                            promissesFile.push(file.save(currentProject.modulePath(value.data[0], 'factory'), value.data[1]));
                        } else {
                            file.exists(currentProject.modulePath(value.data[0], 'factory')).then(function (exists) {
                                if (!exists) {
                                    promissesFile.push(file.save(currentProject.modulePath(value.data[0], 'factory'), value.data[1]));
                                }
                            });
                        }
                    });

                    $q.all(promissesFile).then(function (retornoFile) {
                        def.resolve(true);
                    });
                });

                return def.promise;
            };

            //Finalizando
            $q.all([services(), domains(), factories()]).then(function () {
                deferred.resolve(true);
            });

            return deferred.promise;
        };
    }
})();
