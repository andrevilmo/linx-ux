define(['plugins/dialog', 'knockout', 'durandal/app', 'managers/__auth', 'managers/user', 'breeze'],
    function (dialog, ko, app, managerAuth, managerUser, breeze) {

        var saveDialog = function (vm, toolbarInstance, layoutInfo, isNewLayout) {
            var _this = this;

            this.selectedLocal = ko.observable("server");
            this.nomeLayout = ko.observable("");
            this.canEditNomeLayout = ko.observable(true);

            this.compositionComplete = function () {

                vm.showProcessing('Atualizando Permissões...');

                //Usuários
                managerUser.getAllUserPermission(toolbarInstance.idObjetoConteudo).then(function (data) {
                    var temp = [];
                    var selected = "";
                    data.forEach(function (item) {
                        temp.push({ id: item.IdUsuario, text: item.NomeUsuario });

                        if (item.Selected) {
                            selected = selected + (isNullOrEmpty(selected) ? "" : ",") + item.IdUsuario;
                        }

                    });

                    $("#cboUsuarios").select2({
                        data: temp,
                        tags: true,
                        placeholder: "Selecione os Usuários."
                    });

                    $("#cboUsuarios").val(selected).trigger('change');

                    vm.closeProcessing();
                });

                vm.showProcessing('Atualizando Permissões...');

                //Perfis
                managerUser.getAllProfiles(toolbarInstance.idObjetoConteudo).then(function (data) {
                    var temp = [];
                    var selected = ""
                    data.forEach(function (item) {
                        temp.push({ id: item.IdPerfil, text: item.NomePerfil });

                        if (item.Selected) {
                            selected = selected + (isNullOrEmpty(selected) ? "" : ",") + item.IdPerfil;
                        }
                    });

                    $("#cboPerfis").select2({
                        data: temp,
                        tags: true,
                        placeholder: "Selecione os Perfis."
                    });

                    $("#cboPerfis").val(selected).trigger('change');

                    vm.closeProcessing();
                });

                _this.canEditNomeLayout(isNewLayout);
                if (!isNewLayout) {
                    _this.nomeLayout(toolbarInstance.nomeLayout);
                }
            };

            this.save = function () {

                if (isNullOrEmpty(_this.nomeLayout())) {
                    app.showMessage("Informe o nome do layout!", "Alerta");
                    return;
                }

                if (_this.selectedLocal() === "local") {
                    toolbarInstance.pivot.save({ filename: toolbarInstance.pivot.prefixNameLayout + '_' + _this.nomeLayout() + '.json', destination: 'file', reportType: 'json' });
                    _this.cancel();
                    return;
                }

                vm.showProcessing('Salvando layout remoto...');

                var content = toolbarInstance.pivot.getReport();
                content.dataSource.data = null;

                var entity = {
                    UId: breeze.core.getUuid(),
                    RootNameSpace: toolbarInstance.projectName,
                    ViewName: vm.viewName,
                    PivotName: layoutInfo.pivotName,
                    PivotDataSource: layoutInfo.pivotDataSource,
                    LayoutName: _this.nomeLayout(),
                    Selected: false,
                    Id: (isNewLayout ? 0 : toolbarInstance.idObjetoConteudo),
                    Users: $("#cboUsuarios").val(),
                    Profiles: $("#cboPerfis").val(),
                    Content: JSON.stringify(content)
                };

                $.ajax({
                    async: true,
                    cache: false,
                    type: 'POST',
                    dataType: 'json',
                    globalError: true,
                    headers: managerAuth.getHeaders(),
                    data: JSON.stringify(entity),
                    contentType: 'application/json; charset=UTF-8',
                    url: managerAuth.getServiceAddress('LinxFrameworkObjeto', 'Linx.Framework.BV') + '/SavePivotLayout',
                    success: function (data) {
                        toolbarInstance.idObjetoConteudo = data;
                        toolbarInstance.nomeLayout = _this.nomeLayout();
                        toolbarInstance.pivot.isUserLayout = true;
                        toolbarInstance.pivot.currentLayout = data;
                        vm.getLayoutsFiles();
                        dialog.close(_this);
                        app.showMessage("Layout salvo com sucesso.", "Informação");
                    },
                    error: function (error) {
                    },
                    complete: function () {
                        vm.closeProcessing();
                    }
                });
            };

            this.cancel = function () {
                dialog.close(_this);
            };

            this.onChangeSelectLocal = function () {
                if (this.selectedLocal() === 'server') {
                    $("#divPermissions").css("visibility", "visible").css("display", "");
                }
                else {
                    $("#divPermissions").css("visibility", "hidden").css("display", "none");
                }
            };
        };

        saveDialog.show = function (vm, toolbarInstance, layoutInfo, isNewLayout) {
            return dialog.show(new saveDialog(vm, toolbarInstance, layoutInfo, isNewLayout));
        };

        return saveDialog;
    });