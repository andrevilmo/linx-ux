define(['durandal/system', 'durandal/app', 'plugins/router', 'services/logger', 'managers/__auth', 'managers/__route', 'common', 'managers/routes', 'knockout'],
    function (system, app, router, logger, managerAuth, managerRoute, common, managerRoutes, ko) {
        var connection = $.hubConnection(managerAuth.META_ROOT + 'signalr', { useDefaultPath: false });
        var versionHubProxy = connection.createHubProxy('versionHub');
        var showNewShell = false;
        var showNewModules = false;

        return {
            STATUS: ko.observable('Desconectado'),
            tryingToReconnect: false,

            ///////////////////////
            // method: run()
            ///////////////////////
            run: function () {
                var self = this;
                console.log('Try connect...');
                self.STATUS('Tentando..');

                connection.start()
                    .done(function () {
                        self.STATUS('Conectado');
                        console.log('ClientId [' + connection.id + '] Now connected.');
                    });
            },

            ///////////////////////
            // method: registerClientCheckVersion()
            ///////////////////////
            registerClientCheckVersion: function () {

                versionHubProxy.on('clientCheckVersion', function (serverModules) {
                    if (showNewShell == true) { // || showNewModules == true) {
                        console.log('ClientId [' + connection.id + '] Show message ...');
                        return;
                    }

                    console.log('ClientId [' + connection.id + '] Check current version...');
                    var newModules = [];

                    for (var i = 0; serverModules.length > i; i++) {
                        var serverModule = serverModules[i];

                        // verifica se o modulo e o proprio shell
                        if (serverModule.assemblyVersion == serverModule.shellAssemblyVersion) {

                            if (managerAuth.SHELL_VERSION != serverModule.assemblyVersionFormated) {

                                console.log('ClientId [' + connection.id + '] New shell version found!');
                                showNewShell = true;
                                app.showMessage('<b>Nova versão encontrada, será necessário recarregar a pagina atual!</b><BR><BR>' + serverModule.assemblyVersionFormated + ' :: ' + serverModule.buildDate, 'Linx UX', ['Reiniciar', 'Cancelar']).then(function (dialogResult) {
                                    showNewShell = false;
                                    if (dialogResult != "Cancelar") {
                                        common.showProcessFull();
                                        window.onbeforeunload = null;
                                        window.location.reload();
                                    }
                                });

                                return;
                            }

                        }
                        else if (managerAuth.isShellDevMode == true || managerAuth.isShellSetupMode == true) {

                            for (var y = 0; managerRoute.MODULES_VERSION.length > y; y++) {
                                var clientModule = managerRoute.MODULES_VERSION[y];

                                if (clientModule.moduleUId == serverModule.moduleUId &&
                                    (clientModule.CRC32 != serverModule.CRC32 || clientModule.assemblyVersionFormated != serverModule.assemblyVersionFormated)) {

                                    console.log('ClientId [' + connection.id + '] New module "' + serverModule.moduleName + ' >>> ' + serverModule.assemblyVersionFormated + ' :: ' + serverModule.buildDate + '" version found!');
                                    newModules.push(serverModule);
                                    break;
                                }
                            }

                        }
                    }
                    
                    // novos modulos encontrados
                    if (newModules.length > 0)
                    {
                        var msg = '';
                        var packages = [];

                        for (var i = 0; newModules.length > i; i++) {
                            var newModule = newModules[i];
                            msg += '<BR><b>' + newModule.moduleName + '</b><BR>' + newModule.assemblyVersionFormated + ' :: ' + newModule.buildDate + '<BR>';

                            // remove os arquivos da memoria
                            var pkg = managerRoute.searchPkgByModuleName(newModule.moduleName);
                            for (var y = 0; pkg.files.length > y; y++) {
                                // retira da memoria
                                requirejs.undef(pkg.files[y]);
                            }

                            // adiciona a nova referencia
                            packages.push({
                                name: newModule.moduleId,
                                main: "main",
                                location: $('meta[name=linx-internet-application-root]').attr("content") + newModule.moduleName + "/" + newModule.requireId + "/App"
                            });
                        }

                        // adiciona a nova referencia no requirejs
                        requirejs.config({
                            packages: packages
                        });

                        managerRoutes.reloadRoutesFromServer();

                        if (managerRoutes.registerAll()) {
                            showNewModules = true;

                            app.showMessage('Nova versão encontrada dos seguintes modulos:</b><BR>' + msg, 'Linx UX', ['OK']).then(function (dialogResult) {
                                showNewModules = false;

                                // atualiza as referencias
                                managerRoute.MODULES_VERSION = serverModules;

                                // reload na tela atual
                                router.deactivate();
                                router.activate();
                            });
                        }

                    }

                });

            },

            ///////////////////////
            // method: registerClientFileChanged()
            ///////////////////////
            registerClientFileChanged: function () {

                versionHubProxy.on('clientFileChanged', function (moduleName, pkgName, fileName, displayFileName) {
                    console.log('ClientId [' + connection.id + '] File changed "' + moduleName + fileName + '"...');

                    // remove os arquivos da memoria
                    var pkg = managerRoute.searchPkgByModuleName(moduleName);
                    if (pkg != null) {
                        for (var y = 0; pkg.files.length > y; y++) {
                            // retira da memoria
                            requirejs.undef(pkg.files[y]);
                        }

                        // reload na tela atual
                        router.deactivate();
                        router.activate();
                    }
                    toastr.options = {
                        "closeButton": true,
                        "debug": false,
                        "newestOnTop": true,
                        "progressBar": true,
                        "positionClass": "toast-top-right",
                        "preventDuplicates": true,
                        "onclick": null,
                        "showDuration": "300",
                        "hideDuration": "1000",
                        "timeOut": "5000",
                        "extendedTimeOut": "1000",
                        "showEasing": "swing",
                        "hideEasing": "linear",
                        "showMethod": "fadeIn",
                        "hideMethod": "fadeOut"
                    }
                    toastr.info('Módulo: ' + moduleName + ' alterado!')

                });

            },

            ///////////////////////
            // method: init()
            ///////////////////////
            init: function () {
                var self = this;
                connection.qs = { 'clientVersion': managerAuth.SHELL_VERSION };
                connection.logging = false;

                connection.error(function (error) {
                    console.log('ClientId [' + connection.id + '] ' + error)
                    self.STATUS('Desconectado');
                });

                connection.reconnecting(function () {
                    self.STATUS('Tentando');
                });

                connection.reconnected(function () {
                    self.STATUS('Conectado..');
                });

                connection.disconnected(function () {
                    console.log('ClientId [' + connection.id + '] Disconnected.');
                    //self.STATUS('Desconectado');

                    setTimeout(function () {
                        self.run();
                    }, 5000); // Restart connection after 5 seconds.
                });

                this.registerClientCheckVersion();
                this.registerClientFileChanged();
                //this.registerServerValidadeModules();

                this.run();
            }
        };
    });
