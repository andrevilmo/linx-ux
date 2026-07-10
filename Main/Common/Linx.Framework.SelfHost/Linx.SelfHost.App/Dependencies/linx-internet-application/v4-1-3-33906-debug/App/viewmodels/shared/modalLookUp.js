define(['plugins/dialog', 'knockout', 'services/logger', 'managers/__auth', 'common', 'breeze', 'durandal/app'],
    function (dialog, ko, logger, managerAuth, common, breeze, app) {
        var getNewGuid = function () {
            return breeze.core.getUuid();
        };
        var getMetadata = function (vm) {
            if (vm && vm.lookupInfo && vm.lookupInfo.visibleColumns && !isNullOrEmpty(vm.lookupInfo.visibleColumns)) {
                var vCols = vm.lookupInfo.visibleColumns.split(',');
                var metadata = [];
                $.each(vm.metadata, function (i, item) {
                    if (vCols.indexOf(item.key) >= 0) metadata.push(item);
                });
                if (metadata.length == 0)
                    return vm.metadata;
                else
                    return metadata;

            } else {
                return vm.metadata;
            }
        }
        var __UI_btnConfirm_Click = function () {
            var grid = vm.tableLookup();
            var selectedItems = [];

            if (grid.data('igGrid') === undefined) {
                dialog.close(this, { cancel: false, selectedItems: selectedItems });
                return;
            }

            var ds = grid.data().igGrid.dataSource.dataView();

            //get selected row
            var multiSelect = grid.igGridSelection("option", "multipleSelection");
            if (multiSelect) {
                var rows = grid.igGridSelection("selectedRows");

                if (rows && rows.length > 0) {
                    $.each(rows, function (index, value) {
                        selectedItems.push(ds[value.index]);
                    });
                }
            } else {

                var selectedRow = grid.igGrid("selectedRow");
                if (selectedRow === null) {
                    selectedRow = grid.igGrid("activeRow");
                }
                if (selectedRow !== null) {
                    selectedItems.push(ds[selectedRow.index]);
                }
            }

            if (selectedItems.length === 0)
                app.showMessage('Nenhuma informação foi selecionada!', 'Alerta', ['Ok']);
            else
                dialog.close(this, { cancel: false, selectedItems: selectedItems });
        };

        var vm = {
            lookupInfo: null,
            allowMultiSelectionInSearch: true,
            title: ko.observable('Title'),
            moduleInfo: ko.observable('null'),
            caption: ko.observable('0-0'),
            tableId: ko.observable(),
            tableLookup: function () { return $('#' + vm.tableId()); },
            lookupName: '',
            fieldToSearch: '',
            internalLookupSearch: null,
            metadata: null,
            dataSource: [],
            // state
            canGoFirst: ko.observable(false),
            canGoBack: ko.observable(false),
            canGoNext: ko.observable(false),
            canGoLast: ko.observable(false),
            btnsVisible: ko.observable(false),
            // events click
            UI_btnCancel_Click: function () {
                dialog.close(this, { cancel: true });
            },
            UI_btnConfirm_Click: __UI_btnConfirm_Click,
            UI_btnFirst_Click: function () {
                common.showProcess('.modal-body');
                vm.lookupInfo = vm.internalLookupSearch(vm.lookupName, vm.fieldToSearch, 'F', vm.changeState, vm.lookupInfo);
            },
            UI_btnBack_Click: function () {
                common.showProcess('.modal-body');
                vm.lookupInfo = vm.internalLookupSearch(vm.lookupName, vm.fieldToSearch, 'B', vm.changeState, vm.lookupInfo);
            },
            UI_btnNext_Click: function () {
                common.showProcess('.modal-body');
                vm.lookupInfo = vm.internalLookupSearch(vm.lookupName, vm.fieldToSearch, 'N', vm.changeState, vm.lookupInfo);
            },
            UI_btnLast_Click: function () {
                common.showProcess('.modal-body');
                vm.lookupInfo = vm.internalLookupSearch(vm.lookupName, vm.fieldToSearch, 'L', vm.changeState, vm.lookupInfo);
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
                common.showProcess('.modal-body');
                vm.createGrid();


                setTimeout(function () {
                    $(vm.tableLookup().selector + '_container').focus();
                }, 500);
            },

            showModal: function (title, lookupName, metadata, fieldToSearch, internalLookupSearch, lookupInfo, dataSource, allowMultiSelectionInSearch) {
                vm.title = title;
                vm.lookupName = lookupName;
                vm.fieldToSearch = fieldToSearch;
                vm.internalLookupSearch = internalLookupSearch;
                vm.lookupInfo = lookupInfo;
                vm.metadata = metadata;
                vm.dataSource = dataSource;
                vm.allowMultiSelectionInSearch = !(isNullOrEmpty(allowMultiSelectionInSearch) || !allowMultiSelectionInSearch);
                vm.moduleInfo(lookupInfo.vm.__moduleId__);
                vm.tableId('lookupTable_' + getNewGuid().replaceAll('-', ''));

                return dialog.show(this);
            },

            changeState: function (data) {
                if (data != null) {
                    vm.tableLookup().igGrid('option', 'dataSource', data.results);
                }
                vm.btnsVisible(vm.lookupInfo.totalRecords > vm.lookupInfo.pageSize);
                vm.caption(vm.lookupInfo.getCurrentDisplay());
                vm.canGoFirst(vm.lookupInfo.pageSkip !== 0);
                vm.canGoBack(vm.lookupInfo.pageSkip !== 0);
                vm.canGoNext(vm.lookupInfo.pageSkip !== vm.lookupInfo.totalPages());
                vm.canGoLast(vm.lookupInfo.pageSkip !== vm.lookupInfo.totalPages());

                common.closeProcess('.modal-body');
            },
            sortHandler: function (event, args) {
                if (vm.lookupInfo.totalRecords > vm.lookupInfo.pageSize) {
                    vm.lookupInfo.fieldToSort = args.columnKey;
                    vm.lookupInfo.sortDirection = args.direction;
                    vm.UI_btnFirst_Click();
                }
            },
            createGrid: function () {

                vm.tableLookup().igGrid({
                    width: '100%',
                    height: '300px',
                    autoGenerateColumns: false,
                    dataSource: vm.dataSource,
                    columns: getMetadata(this),
                    autofitLastColumn: false,
                    enableUTCDates: true,
                    features: [
                        { name: 'Sorting', type: 'local', caseSensitive: true, columnSorted: vm.sortHandler },
                        { name: 'Selection', multipleSelection: ((vm.lookupInfo.isMultiSelection && vm.allowMultiSelectionInSearch && vm.lookupInfo.vm.status() == 'E') || (vm.lookupInfo.vm.status() == 'C' && vm.allowMultiSelectionInSearch)) },
                        { name: "Resizing", allowDoubleClickToResize: true },
                        { name: 'RowSelectors', enableCheckBoxes: ((vm.lookupInfo.isMultiSelection && vm.allowMultiSelectionInSearch && vm.lookupInfo.vm.status() == 'E') || (vm.lookupInfo.vm.status() == 'C' && vm.allowMultiSelectionInSearch)), enableRowNumbering: false }
                    ]
                });

                vm.tableLookup().delegate('.ui-iggrid-activerow', 'dblclick', function (e) {
                    vm.UI_btnConfirm_Click();
                });

                $(vm.tableLookup().selector + '_container').parent().keyup(function (e) {
                    if (e.keyCode == 13)
                        vm.UI_btnConfirm_Click();
                });

                vm.changeState(null)
            }
        }

        return vm;
    });