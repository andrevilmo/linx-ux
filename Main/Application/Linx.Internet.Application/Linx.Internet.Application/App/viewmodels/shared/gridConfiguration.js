define(['plugins/dialog', 'durandal/app', 'knockout', 'services/logger', 'managers/__auth', 'managers/user', 'common', 'plugins/router'],
    function (dialog, app, ko, logger, managerAuth, managerUser, common, router) {
        var isBusy = function (newValue) {
            if ($(".page-container").html() == undefined || $(".page-container").html().length == 0)
                return;
            if (newValue) {
                common.showProcess('#main');
            }
            else {
                common.closeProcess('#main');
            }
        };


        var vm = {
            comboUser: function () { return $('#cboUsersPermission'); },
            comboProfiles: function () { return $('#cboProfilesPermission'); },
            grid: function () { return $('#configColumns'); },
            dfd: null,
            sourceVM: null,
            gridName: '',
            layoutObject: null,
            NomeLayout: ko.observable(null),
            CanEditNomeLayout: ko.observable(true),
            title: ko.observable('Configuração de layout da grid'),
            currentLayout: ko.observable(null),
            canSave: function (validateName) {
                if (validateName && isNullOrEmpty(this.NomeLayout)) {
                    app.showMessage("Preencha o nome do layout!", "Alerta");
                    return false;
                }
                var hiddenColumn = 0;
                for (var i = 0; i < this.layoutObject._columns.length; i++) {
                    if (this.layoutObject._columns[i].hidden)
                        hiddenColumn++;
                }
                if (hiddenColumn === this.layoutObject._columns.length) {
                    app.showMessage("Pelo menos uma coluna tem que estar visível!", "Alerta");
                    return false;
                }

                return true;
            },
            prepareLayout: function () {
                if (this.grid().igGridUpdating('isEditing')) {
                    this.grid().igGridUpdating('endEdit', true);
                }

                this.currentLayout().NomeLayout = this.NomeLayout();

                this.currentLayout().ConteudoJson = JSON.stringify(this.layoutObject);

                this.currentLayout().PermissaoUsuario = this.comboUser().select2('val').join();
                this.currentLayout().PermissaoPerfil = this.comboProfiles().select2('val').join();
            },
            apply_click: function () {
                var _this = this;
                if (!_this.canSave(false)) return;

                _this.prepareLayout();
                _this.currentLayout().Id = -999;
                _this.close(true, _this.currentLayout());
            },
            save_click: function () {
                var _this = this;
                if (!_this.canSave(true)) return;

                _this.prepareLayout();

                managerUser.saveGridLayout(this.currentLayout()).then(
                    function saveSuccess(itemSaved) {
                        app.showMessage("Salvo com sucesso!").then(function () {
                            _this.close(true, itemSaved.Id);
                        });
                    },
                    function saveFail(jqXHR, textStatus, errorThrown) {
                        app.showMessage(jqXHR.responseJSON.ExceptionMessage, 'Erro', ['Ok']);
                    });

            },

            rowSelected: function () {
                var row = this.grid().igGrid('selectedRow');
                this.selectedIndex = row != null ? row.index : -1;
                this.canMoveColumnUp(this.selectedIndex > 0);
                this.canMoveColumnDown(this.selectedIndex >= 0 && this.selectedIndex < this.layoutObject._columns.length - 1);
            },
            selectedIndex: -1,
            canMoveColumnUp: ko.observable(false),
            canMoveColumnDown: ko.observable(false),

            moveColumnUp: function () {
                var _col = this.layoutObject._columns[this.selectedIndex];
                this.layoutObject._columns[this.selectedIndex] = this.layoutObject._columns[this.selectedIndex - 1];
                this.layoutObject._columns[this.selectedIndex - 1] = _col;

                this.grid().igGrid('dataBind');
                this.rowSelected();
            },
            moveColumnDown: function () {
                var _col = this.layoutObject._columns[this.selectedIndex];
                this.layoutObject._columns[this.selectedIndex] = this.layoutObject._columns[this.selectedIndex + 1];
                this.layoutObject._columns[this.selectedIndex + 1] = _col;

                this.grid().igGrid('dataBind');
                this.rowSelected();
            },
            close: function (needRefresh, selectedId) {
                if (needRefresh != null && needRefresh)
                    this.dfd.resolve(true, selectedId);
                else
                    this.dfd.resolve(false);
                dialog.close(this, { cancel: !needRefresh });
            },
            cancel_Click: function () {
                this.close();
            },
            compositionComplete: function () {
                var _this = this;

                _this.getData();
                //populates combos
                var idLayout = _this.currentLayout() ? _this.currentLayout().Id : 0;
                _this.NomeLayout(_this.currentLayout() ? _this.currentLayout().NomeLayout : "");

                managerUser.getAllUserPermission(idLayout).then(function (data) {
                    var temp = [];
                    data.forEach(function (item) {
                        temp.push({ id: item.IdUsuario, text: item.NomeUsuario });
                    })
                    _this.comboUser().select2({
                        data: temp,
                        tags: true,
                        placeholder: "Selecione os Usuários."
                    });
                    if (!isNullOrEmpty(_this.currentLayout().PermissaoUsuario)) {
                        _this.comboUser().select2('val', _this.currentLayout().PermissaoUsuario.split(','));
                    }
                });
                managerUser.getAllProfiles(idLayout).then(function (data) {
                    var temp = [];
                    data.forEach(function (item) {
                        temp.push({ id: item.IdPerfil, text: item.NomePerfil });
                    })
                    _this.comboProfiles().select2({
                        data: temp,
                        tags: true,
                        placeholder: "Selecione os Perfis."
                    });
                    if (!isNullOrEmpty(_this.currentLayout().PermissaoPerfil)) {
                        _this.comboProfiles().select2('val', _this.currentLayout().PermissaoPerfil.split(','));
                    }
                });



            },
            getData: function () {
                isBusy(true);
                var _this = this;
                if (isNull(this.layoutObject))
                    this.layoutObject = JSON.parse(this.gridInfo.gridSaveStates.save());

                this.createGrid();

                isBusy(false);
            },

            createGrid: function () {
                var _this = this;
                _this.grid().igGrid({
                    width: '100%', height: '240px',
                    primaryKey: 'key',
                    renderCheckboxes: true,
                    autoCommit: true,
                    dataSource: this.layoutObject._columns,
                    autofitLastColumn: false,
                    columns: [
                        { headerText: 'Nome coluna', key: 'key', dataType: 'string', width: '30%' },
                        { headerText: 'Texto exibido', key: 'headerText', dataType: 'string', width: '60%' },
                        { headerText: 'Ocultar', key: 'hidden', dataType: 'bool', format: 'checkbox', width: '10%' }
                    ],
                    features: [
                        { name: 'Resizing' },
                        {
                            name: 'Selection', mode: 'row', rowSelectionChanged: function (evt, ui) {
                                _this.rowSelected();
                            }
                        },
                        {
                            name: 'Updating',
                            horizontalMoveOnEnter: true,
                            enableDeleteRow: false,
                            enableAddRow: false,
                            startEditTriggers: 'click',
                            editMode: 'cell',
                            columnSettings: [
                                { columnKey: 'key', readOnly: true },
                                { columnKey: 'headerText', editorType: 'text' },
                                { columnKey: 'hidden', editorType: 'checkbox' },
                            ],
                        }
                    ]
                });
            },
            showModal: function (sourceVM, gridInfo, gridName, saveAs) {
                this.dfd = $.Deferred();
                this.sourceVM = sourceVM;
                this.gridName = gridName;
                this.gridInfo = gridInfo;
                this.currentLayout({ Id: 0, NomeLayout: '', PermissaoPerfil: '', PermissaoUsuario: '' });
                this.CanEditNomeLayout(true);
                this.NomeLayout('');
                this.layoutObject = null;

                if (!saveAs && this.gridInfo.currentLayout() && this.gridInfo.currentLayout().Id > 0) {
                    this.currentLayout(this.gridInfo.currentLayout());
                    this.NomeLayout(this.gridInfo.currentLayout().NomeLayout);
                    this.CanEditNomeLayout(false);
                }
                if (this.gridInfo.currentLayout() && this.gridInfo.currentLayout().ConteudoJson) {
                    this.layoutObject = JSON.parse(this.gridInfo.currentLayout().ConteudoJson);
                    this.currentLayout().PermissaoUsuario = this.gridInfo.currentLayout().PermissaoUsuario;
                    this.currentLayout().PermissaoPerfil = this.gridInfo.currentLayout().PermissaoPerfil;
                    if (saveAs)
                        this.NomeLayout(this.gridInfo.currentLayout().NomeLayout + ' (cópia)');
                }

                if (this.currentLayout().Id <= 0) {
                    this.currentLayout().Modulo = this.sourceVM.__moduleId__;
                    this.currentLayout().NomeObjeto = this.gridName;
                }

                dialog.show(this);
                return this.dfd;
            }
        }

        return vm;
    });