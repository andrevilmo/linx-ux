define(['plugins/dialog', 'durandal/app', 'knockout', 'services/logger', 'managers/__auth', 'managers/user', 'common', 'viewmodels/shared/addCustomExport', 'plugins/router'],
    function (dialog, app, ko, logger, managerAuth, managerUser, common, addCustomExport, router) {
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
        var currentDataItem = ko.observable(null);

        var vm = {
            isExcelDataSource: ko.observable(true),
            isLoaded: false,
            sourceVM: null,
            dataView: [],
            currentDataItem: currentDataItem,
            title: ko.observable('Configuração de Exportação e Fonte de Dados'),
            selectedCustomExport: ko.observable(null),
            reportTemplates: ko.observableArray([]),
            selectedTemplateRpt: ko.observable(null),
            customExport: [],
            suggestedEntity: null,
            visibleColumns: null,
            filterCondictions: '',
            canAddVisible: ko.observable(true),
            canEditVisible: ko.observable(true),
            canDelVisible: ko.observable(true),
            addNewConfiguration: function () {
                var _this = this;
                addCustomExport.closeCallback = function (executeRefresh) {
                    if (executeRefresh)
                        _this.getData()
                };
                addCustomExport.showModal(this.sourceVM, null, this.suggestedEntity, this.visibleColumns, this.filterCondictions, false, this.isExcelDataSource());
            },
            deleteConfiguration: function () {
                var _this = this;

                return app.showMessage('Deseja realmente excluir a fonte [' + _this.currentDataItem().Name + ']?', 'Alerta', ['Yes', 'No'])
                .then(function (selectedOption) {
                    if (selectedOption === 'Yes') {

                        //delete
                        $.ajax({
                            type: 'DELETE',
                            messageUser: "Excluindo a Configuração de Exportação",
                            contentType: 'application/json; charset=UTF-8',
                            url: managerAuth.getServiceAddress('linxframeworkobjeto/DeleteConfiguracaoExportacao') + '?uidConfiguracaoExportacao=' + _this.currentDataItem().Uid,
                            async: true,
                            cache: false,
                            error: function (jqXHR, textStatus, errorThrown) {
                                isBusy(false);
                                app.showMessage(jqXHR.responseText, 'Error');
                            },
                            success: function (data) {
                                _this.getData()
                            }
                        });
                        _this.getData()

                    }
                    return selectedOption;
                });



            },
            editConfiguration: function () {
                var _this = this;
                addCustomExport.closeCallback = function (executeRefresh) {
                    if (executeRefresh)
                        _this.getData()
                };
                addCustomExport.showModal(this.sourceVM, currentDataItem(), null, null, null, false, this.isExcelDataSource());
            },
            canEditConfiguration: ko.computed(function () {
                return currentDataItem() != null;
            }),
            templateDownload_Click: function (reportName) {
                if (!isNullOrEmpty(reportName)) {
                    var _this = this;
                    isBusy(true);
                    _this.sourceVM.getDataContext().exportTemplateReport(reportName, function () {
                        isBusy(false);
                    });
                }
            },
            getColumnsAndDisplay: function (columnJoineds) {
                var _this = this;
                var select = [];
                var fieldColumns = columnJoineds.split(',');
                var metadata = jQuery.grep(_this.sourceVM.dataExportInfo[_this.sourceVM.rootDataTypeName], function (item, i) { return (item.name === _this.currentDataItem().Adapter); });

                $.each(fieldColumns, function (i, item) {
                    var _col = jQuery.grep(metadata[0].metaData(), function (propItem, i) { return (propItem.key === item); });
                    var display = isNullOrEmpty(_col) ? item : _col[0].headerText;
                    select.push('[' + item + ':' + display + ']');
                });

                return select.join(',');
            },
            simpleExport_Click: function () {
                if (!this.hasSelectedItem()) return;
                var _this = this;
                isBusy(true);
                _this.sourceVM.getDataContext().exportToExcel(this.currentDataItem().Adapter, this.currentDataItem().JEntitySearch, this.currentDataItem().TranslatedJEntitySearch, function () {
                    isBusy(false);
                }, _this.getColumnsAndDisplay(_this.currentDataItem().Columns));
            },
            customReportExport_Click: function () {
                if (!this.hasSelectedItem()) return;
                if (!this.canExportReport()) return;
                var _this = this;
                isBusy(true);

                _this.sourceVM.getDataContext().exportToReport(this.currentDataItem().Name, this.currentDataItem().Adapter, this.currentDataItem().JEntitySearch, this.currentDataItem().TranslatedJEntitySearch, function () {
                    isBusy(false);
                }, _this.getColumnsAndDisplay(_this.currentDataItem().Columns), _this.currentDataItem().ExportMedia);
            },
            dataSourceExport_Click: function () {
                isBusy(true);
                this.sourceVM.getDataContext().exportReportDataSource(function () {
                    isBusy(false);
                });
            },
            hasSelectedItem: function (noThrowMessage) {
                if (isNullOrEmpty(this.currentDataItem())) {
                    if (!noThrowMessage) app.showMessage('Selecione uma entidade a ser exportada.', 'Exportação', ['Ok']);
                    return false;
                }
                return true;
            },
            txtExport_Click: function () {
            },
            defaultUrl_Click: function () {
                app.showMessage(this.sourceVM.getDataContext().getDataFeedUrl(), 'Endereço do serviço', ['Ok']);
            },
            customUrl_Click: function () {
                var _this = this, selectProjection = '';
                if (!this.hasSelectedItem()) return;

                var metadata = $.grep(_this.sourceVM.dataExportInfo[_this.sourceVM.rootDataTypeName], function (item, i) { return item.name === _this.currentDataItem().Adapter });
                if (isNullOrEmpty(metadata) || metadata.length === 0) {
                    app.showMessage('Não foi possível obter as informações da entidade selecionada, verifique se a mesma existe na aplicação.', 'Erro ao obter informações do Grupo de Dados', ['OK']);
                    return;
                }

                if (_this.currentDataItem().Columns.length > 0)
                    selectProjection = '&$select=' + this.currentDataItem().Columns;
                var jEntitySearch = '?jEntitySearch=' + (isNullOrEmpty(this.currentDataItem().JEntitySearch) ? 'null' : encode(this.currentDataItem().JEntitySearch));

                app.showMessage(_this.sourceVM.getDataContext().getServiceAddress(metadata[0].actionName) + jEntitySearch + selectProjection, 'Endereço do serviço', ['Ok']);
            },
            cancel_Click: function () {
                dialog.close(this, { cancel: true });
            },
            canExportReport: function () {
                var _this = this;
                var info = jQuery.grep(_this.sourceVM.dataExportInfo[_this.sourceVM.rootDataTypeName], function (item, i) { return (item.name === _this.currentDataItem().Adapter); });
                if (info == null || info.length === 0) {
                    app.showMessage('Não foi possível obter informações do Grupo de Dados.', 'Alerta', ['Ok']);
                    return false;
                }

                if (!info[0].canExportReport) {
                    app.showMessage('Esta informação não está apta para ser exportada para relatório.\nEscolha outro Grupo de Dados.', 'Alerta', ['Ok']);
                    return false;
                }

                return true;
            },
            activate: function () {
            },
            canActivate: function () {
                return true;
            },
            canDeactivate: function () {
                return true;
            },
            grid: function () {
                return $('#exportTemplateTable');
            },
            compositionComplete: function () {
                this.createGrid();

                this.getData();
            },
            getParentFullName: function () {
                return this.sourceVM.rootNamespace + '.' + this.sourceVM.rootDataTypeName;
            },
            getData: function () {
                isBusy(true);
                var _this = this;

                _this.currentDataItem(null);
                _this.grid().igGridSelection('clearSelection');

                var callback = function (result) {
                    _this.dataView = result;
                    _this.grid().igGrid('option', 'dataSource', _this.dataView);
                    isBusy(false);
                };

                $.ajax({
                    messageUser: "Buscando as Configurações de Exportação",
                    contentType: 'application/json; charset=UTF-8',
                    url: managerAuth.getServiceAddress('linxframeworkobjeto/GetConfiguracaoExportacao') + '?isExcel=' + (this.isExcelDataSource() ? 'true' : 'false') + '&parentFullName=' + this.getParentFullName(),
                    async: true,
                    cache: false,
                    error: function (jqXHR, textStatus, errorThrown) {
                        isBusy(false);
                        app.showMessage(jqXHR.responseText, 'Error');
                    },
                    success: function (data) {
                        isBusy(false);
                        callback(data);
                    }
                });


                this.getReports();

            },
            getReports: function () {
                var templates = [];
                for (var i = 0; i < router.routes.length; i++) {
                    var record = router.routes[i];
                    if (record.type != "transaction-report" || record.currentData == null) continue;
                    if ((record.currentData.NomeRelatorio.indexOf(this.sourceVM.controllerName) > -1 ||
                        record.currentData.NomeRelatorio.indexOf(this.sourceVM.rootNamespace + "." + this.sourceVM.rootDataTypeName) > -1 ||
                        ((typeof this.sourceVM.isReportComposition === 'function') && this.sourceVM.isReportComposition(record.currentData.NomeRelatorio))) &&
                        record.currentData.CaminhoRelatorio.endsWith('.trdx')) {
                        templates.push({ ReportTitle: record.title, ReportPath: record.currentData.CaminhoRelatorio });
                    }
                }

                this.reportTemplates(templates);
            },
            findItem: function (name) {
                var item = jQuery.grep(this.sourceVM.dataExportInfo[this.sourceVM.rootDataTypeName], function (n, i) {
                    return (n.name === name);
                });
                return item.length > 0 ? item[0] : null;
            },
            createGrid: function () {
                var _this = this;
                _this.grid().igGrid({
                    width: '100%', height: '320px',
                    primaryKey: 'Uid',
                    dataSource: [],
                    columns: [
                        { headerText: 'Id', key: 'Uid', dataType: 'string', width: '1px', hidden: true },
                        { headerText: 'Nome', key: 'Name', dataType: 'string', width: '120px' },
                        { headerText: 'Adapter', key: 'Adapter', dataType: 'string', width: '80px', hidden: true },
                        {
                            headerText: 'Grupo de Dados', key: 'AdapterF', dataType: 'string', width: '160px', unbound: true, formula: function getAdapterDescription(data, grid) {
                                var metadata = _this.findItem(data["Adapter"]);
                                return (isNullOrEmpty(metadata) || metadata.length === 0) ? 'Não encontrado' : metadata.display
                            }
                        },
                        { headerText: 'Filtro Utilizado', key: 'TranslatedJEntitySearch', dataType: 'string', width: '200px' },
                        { headerText: 'Colunas', key: 'Columns', dataType: 'string', width: '120px', hidden: true },
                        {
                            headerText: 'Colunas', key: 'ColumnsF', dataType: 'string', width: '460px', unbound: true, formula: function getColumnsDescription(data, grid) {
                                var columns = data['Columns'];
                                var metadata = _this.findItem(data["Adapter"]);
                                if (isNullOrEmpty(metadata) || metadata.length === 0) return columns;
                                var _cols = columns.split(',');

                                var findCol = function (colName) {
                                    var _found = jQuery.grep(metadata.metaData(), function (n, i) {
                                        return (n.key === colName);
                                    });
                                    return (isNullOrEmpty(_found) || _found.length === 0) ? colName : _found[0].headerText;
                                };


                                for (var i = 0; i < _cols.length; i++)
                                    _cols[i] = findCol(_cols[i]);


                                return _cols.join();
                            }
                        }
                    ],
                    features: [
                        {
                            name: 'Selection', mode: 'row',
                            rowSelectionChanged: function (evt, ui) {
                                var id = ui.owner.grid.selectedRow().id
                                if (typeof id !== 'undefined') {
                                    $.each(_this.dataView, function (i, item) {
                                        if (item.Uid == id)
                                            _this.currentDataItem(item);
                                    });

                                }
                            },
                        },
                        { name: 'Tooltips', columnSettings: [{ columnKey: 'JEntitySearch', allowTooltips: false }] },
                        { name: 'Resizing' }
                    ]
                });
            },
            showModal: function (sourceVM, suggestedEntity, visibleColumns, filterCondictions, options, isExcel) {
                if (options) {
                    this.canAddVisible(options.canAdd);
                    this.canEditVisible(options.canEdit);
                    this.canDelVisible(options.canDel);
                }
                this.sourceVM = sourceVM;
                this.suggestedEntity = suggestedEntity;
                this.visibleColumns = visibleColumns;
                this.filterCondictions = filterCondictions;
                this.currentDataItem(null);
                this.isExcelDataSource(isExcel);

                return dialog.show(this);
            }
        }

        return vm;
    });