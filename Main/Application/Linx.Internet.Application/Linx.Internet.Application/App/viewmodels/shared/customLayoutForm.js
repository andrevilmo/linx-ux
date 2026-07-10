define(['plugins/dialog', 'durandal/app', 'knockout', 'services/logger', 'managers/__auth', 'managers/user', 'common', 'viewmodels/shared/layoutEditor', 'plugins/router'],
    function (dialog, app, ko, logger, managerAuth, managerUser, common, layoutEditor, router) {
        var isBusy = function (newValue) {
            if ($(".page-container").html() == undefined || $(".page-container").html().length === 0)
                return;
            if (newValue) {
                common.showProcess('#main');
            }
            else {
                common.closeProcess('#main');
            }
        };

        var vm = {
            isLoaded: false,
            sourceVM: null,
            dataView: [],
            title: ko.observable('Configurações de layouts de formulário'),
            layoutsLoaded: ko.observableArray([{ Id: 0, NomeLayout: 'Layout Padrão' }]),
            currentLayoutId: ko.observable(0),
            currentLayout: ko.observable(),
            layoutOriginal: ko.observable({}),
            layout: ko.observable({}),
            flattenLayout: ko.observable({}),
            applyLayout: function () {
                var vmRoot = this.sourceVM;
                if (vm.currentLayoutId() == 0) {
                    vmRoot.flattenLayout(ko.observable(vmRoot.flattenObjectByProperty(vm.layoutOriginal(), 'Name'))());
                    vmRoot.currentLayout({ Id: 0, NomeLayout: 'Layout Padrão' });
                } else {
                    managerUser.getGridLayout(vm.currentLayoutId()).then(function (result) {
                        if (result !== null) {
                            vmRoot.layout = ko.observable(JSON.parse(result.ConteudoJson));
                            vmRoot.flattenLayout(ko.observable(vmRoot.flattenObjectByProperty(vmRoot.layout(), 'Name'))());
                            vmRoot.currentLayout(result);
                        }
                    });
                }
            },
            cancel_Click: function () {
                dialog.close(this, { cancel: true });
            },   
            ediLayout: function () {
                var vmRoot = this.sourceVM;
                if (vm.currentLayoutId() == 0) {
                    vmRoot.layout = vm.layoutOriginal;
                    vmRoot.flattenLayout(ko.observable(vmRoot.flattenObjectByProperty(vmRoot.layout(), 'Name'))());
                    vmRoot.currentLayout({ Id: 0, NomeLayout: 'Layout Padrão' });
                    layoutEditor.showEditor(vmRoot);
                    vm.cancel_Click();
                } else {
                    managerUser.getGridLayout(vm.currentLayoutId()).then(function (result) {
                        if (result !== null) {
                            vmRoot.layout = ko.observable(JSON.parse(result.ConteudoJson));
                            vmRoot.flattenLayout(ko.observable(vmRoot.flattenObjectByProperty(vmRoot.layout(), 'Name'))());
                            vmRoot.currentLayout(result);
                            layoutEditor.showEditor(vmRoot);
                            vm.cancel_Click();
                        }
                    });
                }
                
            },  
            activate: function () {
            },
            canActivate: function () {
                return true;
            },
            canDeactivate: function () {
                return true;
            },
            compositionComplete: function () {
                this.getData();
            },
            getParentFullName: function () {
                return this.sourceVM.rootNamespace + '.' + this.sourceVM.rootDataTypeName;
            },
            getData: function () {
                isBusy(true);
                var _this = this;
                
                managerUser.getAllGridLayouts(_this.sourceVM.__moduleId__, _this.sourceVM.viewName).then(function (results) {
                    vm.layoutsLoaded(results);
                    vm.layoutsLoaded.splice(0, 0, { Id: 0, NomeLayout: 'Layout Padrão' });
                    isBusy(false);
                });
                
            },
            
            showModal: function (sourceVM) {
                this.sourceVM = sourceVM;
                this.currentLayout = sourceVM.currentLayout;
                this.currentLayoutId(this.currentLayout().Id);
                this.layoutOriginal(sourceVM.layoutDesignerOriginal());

                return dialog.show(this);
            }
        }

        return vm;
    });