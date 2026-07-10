define(['plugins/dialog', 'durandal/app', 'knockout', 'services/logger', 'managers/__auth', 'common', 'breeze'],
    function (dialog, app, ko, logger, managerAuth, common, breeze) {

        var move = function (arr, old_index, new_index) {
            if (new_index >= arr.length) {
                var k = new_index - arr.length;
                while ((k--) + 1) {
                    arr.push(undefined);
                }
            }
            arr.splice(new_index, 0, arr.splice(old_index, 1)[0]);
            return arr;
        };
        var availableFields = ko.observableArray([]);
        var availableField = ko.observable(null);
        var selectedFields = ko.observableArray([]);
        var selectedField = ko.observable(null);
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
            saveFilter: ko.observable(false),
            isExcelDataSource: ko.observable(true),
            allowMultipleGpecon: ko.observable(false),
            idObjetoConteudo: null,
            visibleReportExport: ko.observable(true),
            getIdObjetoConteudo: function () {
                return isNullOrEmpty(this.idObjetoConteudo) ? -1 : this.idObjetoConteudo;
            },
            closeCallback: null,
            sourceVM: null,
            lastFilter: ko.observable(''),
            lastFilterTranslated: ko.observable(''),

            title: ko.observable('Nova Configuração Exportação e Fonte de Dados'),
            dataExportInfo: [],
            exportName: ko.observable(''),
            selectedAdapter: ko.observable(null),
            exportMedia: ko.observable(false),
            adapters: [],
            customExport: [],
            adapterAllowMedia: ko.observable(true),
            canOpenConfiguration: ko.observable(false),
            getParentFullName: function () {
                return this.sourceVM.rootNamespace + '.' + this.sourceVM.rootDataTypeName;
            },
            openSavedConfigurations_click: function () {
                var _this = this;
                require(['viewmodels/shared/customExport'],
                    function (modalExport) { modalExport.showModal(_this.sourceVM, null, null, null, { canAdd: false, canEdit: true, canDel: true }, _this.isExcelDataSource()); });
                this.cancel_Click();
            },
            save_Click: function () {
                var _this = this;
                if (isNullOrEmpty(_this.selectedAdapter())) {
                    app.showMessage('Selecione um adapter a ser salvo.', 'Exportação', ['Ok']);
                    return;
                }
                if (isNullOrEmpty(_this.exportName())) {
                    app.showMessage('O nome da exportação não pode ficar em branco.', 'Exportação', ['Ok']);
                    return;
                }
                if (!_this.hasSelectedColumns()) {
                    app.showMessage('Não existem colunas selecionadas. Selecione ao menos uma coluna para prosseguir.', 'Exportação', ['Ok']);
                    return;
                }
                if (!_this.saveFilter()) {
                    _this.lastFilter(null);
                    _this.lastFilterTranslated(null);
                }

                var entity = {
                    Id: _this.getIdObjetoConteudo(),
                    Name: _this.exportName(),
                    Adapter: _this.selectedAdapter(),
                    Columns: _this.getColumns(),
                    JEntitySearch: _this.lastFilter(),
                    TranslatedJEntitySearch: _this.lastFilterTranslated(),
                    ParentFullTypeName: _this.getParentFullName(),
                    ExportMedia: _this.exportMedia(),
                    IsExcelDataSource: _this.isExcelDataSource(),
                    AllowMultipleGpecon: _this.allowMultipleGpecon()
                };

                isBusy(true);

                $.ajax({
                    type: 'POST',
                    messageUser: "Manutenção de Configurações de Exportação",
                    contentType: 'application/json; charset=UTF-8',
                    dataType: 'json',
                    headers: managerAuth.getHeaders(),
                    url: managerAuth.getServiceAddress('linxframeworkobjeto', 'Linx.Framework.BV') + '/SaveConfiguracaoExportacao',
                    async: true,
                    cache: false,
                    globalError: true,
                    data: JSON.stringify(entity),
                    error: function (jqXHR, textStatus, errorThrown) {
                    },
                    success: function (data) {
                        dialog.close(_this, { cancel: true });
                        if (_this.closeCallback && typeof _this.closeCallback == 'function')
                            _this.closeCallback(true);
                    },
                    complete: function (data) {
                        isBusy(false);
                    }
                });
            },
            hasSelectedColumns: function () {
                var fields = selectedFields();

                return fields != null && fields.length > 0;
            },
            getColumnsAndDisplay: function () {
                var select = [];
                var fields = selectedFields();

                if (fields == null || fields.length == 0) {
                    fields = availableFields();
                }

                $.each(fields, function (i, item) {
                    select.push('[' + item.key + ':' + item.headerText + ']');
                });

                return select.join(',');
            },
            getColumns: function () {
                var select = [];
                var fields = selectedFields();

                if (fields == null || fields.length == 0) {
                    fields = availableFields();
                }

                $.each(fields, function (i, item) {
                    select.push(item.key);
                });

                return select.join(',');
            },
            simpleExport_Click: function () {
                var _this = this;
                if (isNullOrEmpty(_this.selectedAdapter())) {
                    app.showMessage('Selecione uma entidade a ser exportada.', 'Exportação', ['Ok']);
                    return;
                }
                if (!_this.hasSelectedColumns()) {
                    app.showMessage('Não existem colunas selecionadas. Selecione ao menos uma coluna para prosseguir.', 'Exportação', ['Ok']);
                    return;
                }
                var lastFilter = _this.lastFilter();
                var lastFilterTranslated = _this.lastFilterTranslated();
                if (isNull(lastFilter)) {

                    if (_this.sourceVM.status() == 'Q') {
                        lastFilter = _this.sourceVM.lastJEntitySearch();
                        lastFilterTranslated = _this.sourceVM.getTranslatedFilter();
                    }
                    else {
                        if (typeof _this.sourceVM.getQueryFilter === 'function') {
                            var queryFilter = _this.sourceVM.getQueryFilter();
                            if (queryFilter === 'Error') {
                                app.showMessage('Não é possível executar a operação solicitada, pois a tela tem filtros que devem ser atendidos.', 'Nova Exportação', ['Ok']);
                                return;
                            }
                            lastFilter = queryFilter;
                            lastFilterTranslated = common.translateSearch(_this.sourceVM.getDataContext(), queryFilter);
                        }
                    }
                }

                isBusy(true);
                _this.sourceVM.getDataContext().exportToExcel(
                    _this.selectedAdapter(),
                    lastFilter,
                    lastFilterTranslated,
                    function () {
                        isBusy(false);
                    },
                    this.getColumnsAndDisplay()
                );
            },
            customReportExport_Click: function () {
                var _this = this;
                if (isNullOrEmpty(_this.selectedAdapter())) {
                    app.showMessage('Selecione uma entidade a ser exportada.', 'Exportação de Relatório', ['Ok']);
                    return;
                }
                if (isNullOrEmpty(_this.exportName())) {
                    app.showMessage('Preencha o nome para o Relatório.', 'Exportação de Relatório', ['Ok']);
                    return;
                }
                if (!_this.hasSelectedColumns()) {
                    app.showMessage('Não existem colunas selecionadas. Selecione ao menos uma coluna para prosseguir.', 'Exportação de Relatório', ['Ok']);
                    return;
                }
                if (!_this.canExportReport()) return;
                isBusy(true);
                _this.sourceVM.getDataContext().exportToReport(_this.exportName(), _this.selectedAdapter(), _this.lastFilter(), _this.lastFilterTranslated(), function () {
                    isBusy(false);
                }, _this.getColumnsAndDisplay(), _this.exportMedia());
            },
            canExportReport: function () {
                var _this = this;
                var info = jQuery.grep(_this.sourceVM.dataExportInfo[_this.sourceVM.rootDataTypeName], function (item, i) { return (item.name === _this.selectedAdapter()); });
                if (info == null || info.length === 0) {
                    app.showMessage('Não foi possível obter informações do adapter.', 'Alerta', ['Ok']);
                    return false;
                }

                if (!info[0].canExportReport) {
                    app.showMessage('Esta informação não está apta para ser exportada para relatório.\nEscolha outro adapter.', 'Alerta', ['Ok']);
                    return false;
                }

                return true;
            },
            dataSourceExport_Click: function () {
                isBusy(true);
                this.sourceVM.getDataContext().exportReportDataSource(function () {
                    isBusy(false);
                });
            },
            txtExport_Click: function () {
            },
            defaultUrl_Click: function () {
                app.showMessage(this.sourceVM.getDataContext().getDataFeedUrl(), 'Endereço do serviço', ['Ok']);
            },
            customUrl_Click: function () {
                var _this = this;
                var complement = '', select = [], metadata = [];
                if (isNullOrEmpty(_this.selectedAdapter())) {
                    app.showMessage('Selecione uma entidade a ser exportada.', 'Exportação', ['Ok']);
                    return;
                }
                if (!_this.hasSelectedColumns()) {
                    app.showMessage('Não existem colunas selecionadas. Selecione ao menos uma coluna para prosseguir.', 'Exportação', ['Ok']);
                    return;
                }
                $.each(selectedFields(), function (i, item) {
                    select.push(item.key);
                });
                if (_this.selectedAdapter() != null) {
                    metadata = $.grep(_this.dataExportInfo, function (item, i) { return item.name === _this.selectedAdapter() });
                    if (isNullOrEmpty(metadata) || metadata.length === 0) {
                        app.showMessage('Não foi possível obter as informações da entidade selecionada, verifique se a mesma existe na aplicação.', 'Erro ao obter informações do Adapter', ['OK']);
                        return;
                    }
                } else {
                    _this.defaultUrl_Click();
                }
                if (select.length > 0) {
                    complement = '&$select=' + select.join(',');
                }
                var jEntitySearch = '?jEntitySearch=\'' + Linx.Base64.encode((isNullOrEmpty(_this.lastFilter()) ? 'null' : _this.lastFilter())) + '\'';

                var action = metadata[0].actionFeed.replace(_this.sourceVM.controllerName, '');
                app.showMessage(_this.sourceVM.getDataContext().getServiceAddress(_this.sourceVM.controllerName) + action + jEntitySearch + complement, 'Endereço do serviço', ['Ok']);
            },
            cancel_Click: function () {
                dialog.close(this, { cancel: true });
                if (this.closeCallback && typeof this.closeCallback == 'function')
                    this.closeCallback(false);
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
            },
            selectColumns: function (selectedColumnsString) {
                var getColumnItem = function (colKey) {
                    var obj = null;
                    var list = availableFields();
                    for (var i = 0; i < list.length; i++) {
                        if (list[i].key === colKey) {
                            if (list[i].isDomain) {
                                obj = list.filter(function (elem) {
                                    return (elem.key == colKey + 'Name' || (elem.key == colKey && elem.key.indexOf("Name") >= 0));
                                });
                            }
                            else
                                obj = list[i];
                            break;
                        }
                    }
                    return (obj !== null && obj.length) ? obj[0] : obj;
                };

                var collist = selectedColumnsString.split(',');
                $.each(collist, function (i, colKey) {
                    col = getColumnItem(colKey.trim());
                    if (col != null) {
                        selectedFields.push(col);
                        availableFields.remove(col);
                    }
                });
            },
            findItem: function (name) {
                if (name && Array.isArray(name))
                    name = name[0];
                var item = jQuery.grep(this.dataExportInfo, function (n, i) {
                    return (n.name === name);
                });
                return item.length > 0 ? item[0] : null;
            },
            selectAdapter: function (selectedAdapter) {
                var item = this.findItem(selectedAdapter);
                if (isNullOrEmpty(item)) {
                    availableFields([]);
                } else {
                    var arrayAvailable = whereInArray(item.metaData(), function (item) { return !item.hidden || (item.isDomain && item.headerText.indexOf('(Name)') > 0); });
                    availableFields(arrayAvailable);

                    this.adapterAllowMedia(item.canExportMedia);
                    this.exportMedia(item.canExportMedia);
                }
                selectedFields([]);
            },
            addItems: function () {
                var selected = availableField();
                if (selected != null && selected.length > 0) {
                    $.each(selected, function (i, item) {
                        selectedFields.push(item);
                        availableFields.remove(item);
                    });
                }
            },
            addAllItems: function () {
                $.each(availableFields(), function (i, item) {
                    selectedFields.push(item);
                });
                availableFields.removeAll()
            },
            removeItems: function () {
                var selected = selectedField();
                if (selected != null && selected.length > 0) {
                    $.each(selected, function (i, item) {
                        availableFields.push(item);
                        selectedFields.remove(item);
                    });
                }
            },
            removeAllItems: function () {
                $.each(selectedFields(), function (i, item) {
                    availableFields.push(item);
                });
                selectedFields.removeAll()
            },
            upItem: function () {
                var array = selectedFields();
                var item = selectedField()[0];
                var index = selectedFields.indexOf(item);

                selectedFields.splice(index, 1);
                selectedFields.splice(index - 1, 0, item)

                selectedField([item]);
            },
            downItem: function () {

                var array = selectedFields();
                var item = selectedField()[0];
                var index = selectedFields.indexOf(item);

                selectedFields.splice(index, 1);
                selectedFields.splice(index + 1, 0, item)

                selectedField([item]);
            },

            canAddItems: ko.computed(function () {
                return availableField() != null && availableField().length > 0;
            }),
            canAddAllItems: ko.computed(function () {
                return availableFields() != null && availableFields().length > 0
            }),
            canRemoveItems: ko.computed(function () {
                return selectedField() != null && selectedField().length > 0;
            }),
            canRemoveAllItems: ko.computed(function () {
                return selectedFields() != null && selectedFields().length > 0
            }),
            canUpItem: ko.computed(function () {
                return selectedField() && selectedField().length > 0 && selectedFields.indexOf(selectedField()[0]) > 0;
            }),
            canDownItem: ko.computed(function () {
                return selectedField() && selectedField().length > 0 && selectedFields.indexOf(selectedField()[0]) < selectedFields().length - 1;
            }),
            availableFields: availableFields,
            availableField: availableField,
            selectedFields: selectedFields,
            selectedField: selectedField,
            multipleGpeconEnabled: ko.observable(false),

            showModal: function (sourceVM, item, suggestedEntity, visibleColumns, filterCondictions, canOpenConfiguration, isExcel) {
                var _this = this;

                _this.isExcelDataSource(isExcel);
                _this.sourceVM = sourceVM;
                _this.saveFilter(false);


                _this.canOpenConfiguration(canOpenConfiguration);
                if (canOpenConfiguration) {
                    isBusy(true);
                    $.ajax({
                        messageUser: "buscando as Configurações de Exportação",
                        contentType: 'application/json; charset=UTF-8',
                        headers: managerAuth.getHeaders(),
                        url: managerAuth.getServiceAddress('linxframeworkobjeto', 'Linx.Framework.BV') + '/GetConfiguracaoExportacao' + '?isExcel=' + (_this.isExcelDataSource() ? 'true' : 'false') + '&parentFullName=' + this.getParentFullName() + "&$inlinecount=allpages",
                        async: true,
                        cache: false,
                        globalError: true,
                        error: function (jqXHR, textStatus, errorThrown) {

                        },
                        success: function (data) {
                            if (data.length > 0)
                                _this.canOpenConfiguration(true);
                        },
                        complete: function (data) {
                            isBusy(false);
                        }
                    });
                }

                _this.adapters.clear();
                _this.dataExportInfo = jQuery.grep(sourceVM.dataExportInfo[sourceVM.rootDataTypeName], function (item, i) { return item.canExportReport; });

                for (var itemKey in _this.dataExportInfo)
                    _this.adapters.push(
                        { name: _this.dataExportInfo[itemKey].name, display: _this.dataExportInfo[itemKey].display });

                _this.selectedAdapter.subscribe(function (value) {
                    _this.selectAdapter(value);
                });

                //getTranslatedFilter
                if (!isNullOrEmpty(item)) {
                    _this.idObjetoConteudo = item.Id;
                    _this.exportName(item.Name);
                    _this.selectedAdapter(item.Adapter);
                    _this.selectAdapter(item.Adapter);
                    _this.lastFilterTranslated(item.TranslatedJEntitySearch);
                    _this.lastFilter(item.JEntitySearch);
                    _this.selectColumns(item.Columns);
                    _this.exportMedia(item.ExportMedia);
                    _this.allowMultipleGpecon(item.AllowMultipleGpecon);
                } else {
                    _this.idObjetoConteudo = null;
                    _this.allowMultipleGpecon(false);

                    if (!isNullOrEmpty(filterCondictions)) {
                        _this.lastFilter(filterCondictions);
                        _this.lastFilterTranslated(common.translateSearch(sourceVM.getDataContext(), filterCondictions));
                        _this.visibleReportExport(false);
                    } else {
                        if (sourceVM.status() == 'Q') {
                            _this.lastFilter(sourceVM.lastJEntitySearch());
                            _this.lastFilterTranslated(sourceVM.getTranslatedFilter());
                        }
                        else {
                            if (typeof sourceVM.getQueryFilter === 'function') {
                                var queryFilter = sourceVM.getQueryFilter();
                                if (queryFilter === 'Error') {
                                    app.showMessage('Não é possível executar a operação solicitada, pois a tela tem filtros que devem ser atendidos.', 'Nova Exportação', ['Ok']);
                                    return;
                                }
                                _this.lastFilter(queryFilter);
                                _this.lastFilterTranslated(common.translateSearch(sourceVM.getDataContext(), queryFilter));
                            }
                        }
                    }
                    _this.exportName("");
                    _this.selectedAdapter("");
                }

                if (!isNullOrEmpty(suggestedEntity)) {
                    _this.selectedAdapter(suggestedEntity);
                    _this.selectAdapter(suggestedEntity);
                    _this.selectColumns(visibleColumns);
                }

                var gpecon = managerAuth.economicGroupId;
                var info = managerAuth.getEnvironmentInfo(managerAuth.getIdTcsAmbiente());
                _this.multipleGpeconEnabled(info != null && managerAuth.economicGroupId == info.UidEmpresa && info.IndicaMultiGpecon);

                return dialog.show(_this);
            }
        };

        vm.lastFilterTranslatedComputed = ko.computed(function lastFilterTranslatedComputedFN() {
            return vm.saveFilter() ? vm.lastFilterTranslated() : '';
        });

        return vm;
    }
);