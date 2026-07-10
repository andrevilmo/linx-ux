(function () { 
    'use strict';

    var controllerId = 'NewProjectCtrl';
    angular.module('FormBuilder')
        .controller(controllerId, ['$scope', '$rootScope', '$timeout', 'projectService', '$location', 'directory', 'template', 'file', 'currentProject', 'toaster', NewProjectCtrl]);

    function NewProjectCtrl($scope, $rootScope, $timeout, projectService, $location, directory, template, file, currentProject, toaster) {
        var vm = this;
        var logFactory;
        vm.loader = false;
        vm.directory = "";
        vm.selectedProjectAction = {
            id: "",
            name: "Selecione"
        };

        vm.projectType = [{
            id: 'blank',
            name: 'Projeto Blank'
        }, {
            id: 'blankBreezeCore',
            name: 'Projeto Blank Breeze Core'
        }];

        vm.exit = function () {
            $location.path('/select-ProjectType');
        };

        vm.setType = function (projectType) {
            vm.selectedProjectAction = projectType;
        };

        vm.save = function () {
            var msgs = [];
            if (!vm.projectName) {
                msgs.push({
                    type: 'error',
                    title: '',
                    text: 'O nome do projeto é obrigatório'
                });
            }

            if (!vm.author) {
                msgs.push({
                    type: 'error',
                    title: '',
                    text: 'O nome do autor é obrigatório'
                });
            }

            if (!vm.directory) {
                msgs.push({
                    type: 'error',
                    title: '',
                    text: 'O diretorio do projeto é obrigatório'
                });
            }

            if (!vm.selectedProjectAction || !vm.selectedProjectAction.id) {
                msgs.push({
                    type: 'error',
                    title: '',
                    text: 'O tipo do projeto é obrigatório'
                });
            }

            if (!navigator.onLine) {
                msgs.push({
                    type: 'warning',
                    title: 'Sem conexão',
                    text: 'Nenhuma conexão com a internet foi detectada, favor tentar novamente.'
                });
            }

            if (msgs.length) {
                msgs.forEach(function (item) {
                    toaster.pop(item.type, item.title, item.text);
                });
                return;
            }

            vm.loader = true;

            var path = vm.directory + '\\' + vm.projectName;

            var project = {
                name: vm.projectName,
                author: vm.author,
                path: path //Não tem necessidade de ser gravado
            };

            
         
            

           directory.createIfNotExist(path).then(function (sucessDirectory) {
                logFactory = $scope.log;
                logFactory.open();
                logFactory.config = {
                    cancel: template.cancel
                }

                if (sucessDirectory) {
                    logFactory.appendText({ id: 'git', text: "Criado pasta do projeto com sucesso!" });
                    logFactory.appendText({ id: 'git', text: "\nInciando download de projeto...\n" });

                    template.download(path, vm.selectedProjectAction.id, null).then(function (sucess) {
                        if (sucess) {                          

                            file.save(path + '\\' + vm.projectName + '.lxproj', JSON.stringify(project));
                            project.path = path + '\\' + vm.projectName + '.lxproj';

                            projectService.saveProject(project);

                            currentProject.urlProjectFile = project.path;
                            currentProject.urlPathProject = path;


                            $timeout(function () {
                                vm.loader = false;
                                $location.path("/formbuilder/view");
                                logFactory.close();
                            }, 3000);

                        } else {
                            toaster.pop('error', "GitClone", "Download não realizado");
                        }

                    }, function (error) {
                        toaster.pop('error', "GitClone", "Download não realizado");
                        vm.loader = false;

                    }, function (data) {
                        logFactory.appendText({ id: 'git', text: data });
                    });

                }
                else {
                    toaster.pop('error', "Criando Pasta", "Pasta do projeto não criada");
                    vm.loader = false;
                }

           }, function (obj) {
               toaster.pop('error', "Criando Pasta", obj.message);
               vm.loader = false;
           });

        };
        vm.folder = function () {
            var remote = require("remote");
            var selectDirectory = remote.getGlobal("selectDirectory");

            selectDirectory(function (directory) {

                if (directory) {
                    vm.directory = directory[0];
                    $scope.$apply();
                }
            });
        };

    }

})();
