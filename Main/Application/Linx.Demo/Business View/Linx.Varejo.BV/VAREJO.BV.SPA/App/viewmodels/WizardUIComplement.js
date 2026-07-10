define(['managers/__auth', 'managers/user'], function (managerAuth, managerUser) {
var complementCtor = function() {
    var complement = {
    isAutomatic: true
    , renderWizardUI_wizWizard_3ca1e4bbaa6c4d9dac76c927e968e514: function(vm) {
    $('#WizardUI_wizWizard_3ca1e4bbaa6c4d9dac76c927e968e514').bootstrapWizard({
        'nextSelector': '.button-next',
        'previousSelector': '.button-previous',
        onInit: function (tab, navigation, index) {
            $('#WizardUI_wizWizard_3ca1e4bbaa6c4d9dac76c927e968e514').find('.button-previous').hide();
    		 $('#WizardUI_wizWizard_3ca1e4bbaa6c4d9dac76c927e968e514 .button-submit').click(function () {
    			 if((typeof vm.OnWizardFinalizing === 'function')) {
    			    if (!vm.OnWizardFinalizing('WizardUI_wizWizard_3ca1e4bbaa6c4d9dac76c927e968e514')) return false;
    			 }
    			 if((typeof vm.OnWizardFinalized === 'function')) {
    			    vm.OnWizardFinalized('WizardUI_wizWizard_3ca1e4bbaa6c4d9dac76c927e968e514');
    			 }
    			 if(vm.custom) {
    		        var e = { cancel: false, viewModel: vm };
    			    vm.custom.beforeWizardFinalizing(e); //custom Finalizing
    			    if(e.cancel) return false;
    			    vm.custom.afterWizardFinalizing({viewModel: vm, id: 'WizardUI_wizWizard_3ca1e4bbaa6c4d9dac76c927e968e514'}); //custom Finalized
    			 }
    		 }).hide();
    		 if((typeof vm.OnWizardInitializing === 'function')) {
    		     vm.OnWizardInitializing();
    		 }
    		 if(vm.custom) vm.custom.afterWizardInitializing({viewModel: vm});
        },
        onTabClick: function (tab, navigation, index) {
            return false;
        },
        onPrevious: function (tab, navigation, index) {
    		 if((typeof vm.OnWizardStepChanging === 'function')) {
    		    if (!vm.OnWizardStepChanging(tab.index(), index, 'WizardUI_wizWizard_3ca1e4bbaa6c4d9dac76c927e968e514')) return false;
    		 }
    		 var e = { oldIndex: tab.index(), newIndex: index, cancel: false, viewModel: vm, id: 'WizardUI_wizWizard_3ca1e4bbaa6c4d9dac76c927e968e514'};
    		 if(vm.custom) vm.custom.beforeWizardStepChanging(e); //custom Step changing
    		 if(e.cancel) return false;
    		 wizardStepChange('WizardUI_wizWizard_3ca1e4bbaa6c4d9dac76c927e968e514',  navigation, index);
    		 if((typeof vm.OnWizardStepChanged === 'function')) {
    		    vm.OnWizardStepChanged(tab.index(), index, 'WizardUI_wizWizard_3ca1e4bbaa6c4d9dac76c927e968e514');
    		 }
    		 if(vm.custom) vm.custom.afterWizardStepChanging({ oldIndex: tab.index(), newIndex: index, viewModel: vm, id: 'WizardUI_wizWizard_3ca1e4bbaa6c4d9dac76c927e968e514'}); //custom Step changed
        },
        onNext: function (tab, navigation, index) {
    		 if((typeof vm.OnWizardStepChanging === 'function')) {
    		    if (!vm.OnWizardStepChanging(tab.index(), index, 'WizardUI_wizWizard_3ca1e4bbaa6c4d9dac76c927e968e514')) return false;
    		 }
    		 var e = { oldIndex: tab.index(), newIndex: index, cancel: false, viewModel: vm, id: 'WizardUI_wizWizard_3ca1e4bbaa6c4d9dac76c927e968e514'};
    		 if(vm.custom) vm.custom.beforeWizardStepChanging(e); //custom Step changing
    		 if(e.cancel) return false;
    		 wizardStepChange('WizardUI_wizWizard_3ca1e4bbaa6c4d9dac76c927e968e514',  navigation, index);
    		 if((typeof vm.OnWizardStepChanged === 'function')) {
    		    vm.OnWizardStepChanged(tab.index(), index, 'WizardUI_wizWizard_3ca1e4bbaa6c4d9dac76c927e968e514');
    		 }
    		 if(vm.custom) vm.custom.afterWizardStepChanging({ oldIndex: tab.index(), newIndex: index, viewModel: vm, id: 'WizardUI_wizWizard_3ca1e4bbaa6c4d9dac76c927e968e514'}); //custom Step changed
        },
        onTabShow: function (tab, navigation, index) {
            var total = navigation.find('li').length;
            var current = index + 1;
            var $percent = (current / total) * 100;
            $('#WizardUI_wizWizard_3ca1e4bbaa6c4d9dac76c927e968e514').find('.progress-bar').css({
                width: $percent + '%'
            });
        }
    });
}

, renderWizardUI_dGridVenda: function(vm) {
    var getDataSource = function() {
        var source = null;
        try {
            source = vm.currentDataItem().VendaList;
        }
        catch (e) { console.log(e); }
        return isNullOrEmpty(source) ? ko.observableArray([]) : source;
    };
    var dataSourceIsLoaded = function() {
        var isLoaded = false;
        try {
            isLoaded = (vm.currentDataItem().VendaIsLoaded === true || vm.currentDataItem().VendaList().length > 0);
        }
        catch (e) {
            isLoaded = true;
        }
        return isLoaded;
    }
    $('#WizardUI_dGridVenda_headers').live('focus  keydown', function (evt) {
        var keyCode = window.event ? evt.which : evt.keyCode;
        if (keyCode === 9) {
            var cols = $('#WizardUI_dGridVenda').igGrid('option', 'columns');
            var dataView = $('#WizardUI_dGridVenda').data('igGrid').dataSource._dataView
            if (dataView.length === 0) return;
            var firstRow = dataView[0].RowDataId;
            clear = vm.status() === 'C';
            if (vm.status() === 'C')
                $('#WizardUI_dGridVenda').igGridUpdating('startEdit', firstRow, 0);
            else {
                var entity = findElementByKey(getDataSource(), 'RowDataId', firstRow);
                var indexColumn = 0;
                cols.some(function (entry) {
                    if (entry.key !== 'RowDataId' && !entry.hidden) {
                        if (verifyCanEditCol(entry.key, clear, entity)) {
                            $('#WizardUI_dGridVenda').igGridUpdating('startEdit', firstRow, indexColumn);
                            return true;
                        }
                        indexColumn++;
                    }
                });
            }
        }
    });
    var getVisibleColumns = function(metaDataControl) {
       if (metaDataControl) return '';
       var visibleColumns = '';
       if($('#WizardUI_dGridVenda').data('igGrid') === undefined) return '';
       var cols = $('#WizardUI_dGridVenda').igGrid('option', 'columns');
       if (cols) {
         for (var idx = 0; idx < cols.length; idx++) {
             if (cols[idx].hidden !== true) visibleColumns += (visibleColumns === '' ? '' : ',') + cols[idx].key;
         }
       }
       return visibleColumns;
    };
    var started = false;
    var currentRow = null;
    var updateEntity = function (columnKey, value, execDataBind) {
        if(value && Array.isArray(value) && value.length === 0) value = null;
        var entity = findElementByKey(getDataSource(), 'RowDataId', currentRow);
        if (entity != null && typeof value !== 'undefined' && getAbsoluteValue(entity[columnKey]) !== value) {
            setAbsoluteValue(entity, columnKey, value);
            if (execDataBind) itemsSource.dataBind(false);
        }
    };
    var isElementHided = function (grid, forceCreating) {
      if (!grid) grid = $('#WizardUI_dGridVenda');
      return ((!grid[0] || (!forceCreating && grid.parent().width() <= 0)) && !$('#dialogVenda').is(':visible'));
    }
    var refreshData = true;
    var itemsSource = { isElementHided: isElementHided, getVisibleColumns: getVisibleColumns, containerId: 'WizardUI_dGridVenda_container', dataBind: function (commitData, forceCreating) {
       var grid = $('#WizardUI_dGridVenda');
       if (started && typeof grid.data('igGridUpdating') === 'undefined') { started = false; }
       if (commitData && started) {
           if (grid.igGridUpdating('isEditing')) {
               grid.igGrid('commit');
           }
           return;
       }
       var execFillDetais = ((vm.status() !== 'C' && vm.status() !== 'I') && !dataSourceIsLoaded());
       if (forceCreating && started && !refreshData && !execFillDetais) return;
       var isHided = isElementHided(grid, forceCreating);
       refreshData = !forceCreating;
       if (refreshData && !isHided) refreshData = false;
       if (isHided) return;
       if (!started) {
           createDataGrid(grid);
           started = true;
           commitData = false;
           $('#WizardUI_dGridVenda_groupbyarea').addClass('hide');
       }
       if (grid.igGridUpdating('isEditing')) {
            grid.igGridUpdating('endEdit', true);
       }
       if (execFillDetais) {
         grid.igGrid("option", "dataSource", []);
         vm.currentDataItem().fillDetails(false, 'Venda');
         return;
       }
       grid.igGrid("option", "dataSource", unwrapObservableArray(getDataSource(), vm));
       var rows = grid.igGrid('allRows');
       if (rows.length > 0) {
         var verticalContainer = grid.igGrid('scrollContainer');
         var isSelected = false;
         if (vm.currentDataItem().currentVenda() != null)
         {
           for(var idx = 0; idx < rows.length; idx++)
           {
             if (rows[idx].dataset.id == getAbsoluteValue(vm.currentDataItem().currentVenda().RowDataId))
             {
                grid.igGridSelection('selectRow', idx);
                verticalContainer.scrollTop(grid.igGrid('option', 'avgRowHeight') * idx);
                isSelected = true;
                break;
             }
           }
         }
         if (!isSelected) {
             grid.igGridSelection('selectRow', 0);
             verticalContainer.scrollTop(0);
         }
         if ($('#dialogVenda').is(':visible')) {
            var hasPaging = $.grep(grid.igGrid('option', 'features'), function (e) {
               return e.name === 'Paging';
            });
            var totalGrid = grid.data('igGrid').options.dataSource.length;
            var current = 1;
            if (hasPaging.length > 0) {
               var totalCurrentPage = totalGrid;
               var currentPage = grid.igGridPaging('pageIndex') + 1;
               var pageIndex = grid.igGridPaging('pageIndex');
               var pageSize = grid.igGridPaging('pageSize');
               if (totalGrid / pageSize > currentPage) totalCurrentPage = (1 * grid.igGrid('rows').length);
               if (currentPage > 1) current = (pageIndex * pageSize) + current;
               $('label#currentNumberVenda').html(current + ' - ' + totalCurrentPage);
            }
            else
               $('label#currentNumberVenda').html(1);
            $('label#totalNumberVenda').html(totalGrid);
        }
       } else {
           $('label#currentNumberVenda').html(0);
           $('label#totalNumberVenda').html(0);
       }
    }};
    var valueGrouBy = -1;
    var deletedIndex = -1;
    function verifyCanEditCol(column, clear, entity){
        switch(column){
            case 'BigIntVenda': { canEditing = clear || vm.enabledForEditing(); break;}
            case 'BitVenda': { canEditing = clear || vm.enabledForEditing(); break;}
            case 'ComboboxVenda': { canEditing = clear || vm.enabledForEditing(); break;}
            case 'DatetimeVenda': { canEditing = clear || vm.enabledForEditing(); break;}
            case 'DecimalVenda': { canEditing = clear || vm.enabledForEditing(); break;}
            case 'IdCliente': { canEditing = clear || vm.enabledForEditing(); break;}
            case 'IdLoja': { canEditing = clear || vm.enabledForEditing(); break;}
            case 'IdVenda': { canEditing = clear || (entity && entity.isAdded()); break;}
            case 'IntVenda': { canEditing = clear || vm.enabledForEditing(); break;}
            case 'SmallIntVenda': { canEditing = clear || vm.enabledForEditing(); break;}
            case 'StringVenda': { canEditing = clear || vm.enabledForEditing(); break;}
        }
        return canEditing;
    };
    function createDataGrid(grid) {
        var gridId = grid[0].id;
        vm.gridSaveStates[gridId] = {
            savedLayouts: ko.observableArray([]),
            currentLayout: ko.observable({ Id: 0 }),
            currentLayoutId: ko.observable(0),
            __applyLayout: function (jsonContent) {
                this.gridSaveStates.returnToSavedState(jsonContent);
                vm.dataToolbar.isBusy(false);
                this.closePopover();
            },
            closePopover: function () {
                $('#WizardUI_dGridVenda_LayoutBtn').igPopover('hide');
            },
            applyLayout: function (layoutInfo) {
                var _this = this;
                if (isNull(layoutInfo) && (!_this.currentLayout() || _this.currentLayout().Id === 0)) {
                    vm.app.showMessage('Não existe layout selecionado');
                    return;
                }
                vm.dataToolbar.isBusy(true);
                if (layoutInfo && layoutInfo.ConteudoJson) {
                    _this.__applyLayout(layoutInfo.ConteudoJson)
                }
                else if (_this.currentLayout() && _this.currentLayout().ConteudoJson) {
                    _this.__applyLayout(_this.currentLayout().ConteudoJson)
                } else {
                    managerUser.getGridLayout(_this.currentLayout().Id).then(function (result) {
                        _this.currentLayout(result);
                        var _arr = _this.savedLayouts(); 
                        for (var i = 0 ; i < _arr.length; i++) {
                            if (_arr[i].Id === result.Id)
                                _arr[i] = result;
                        }
                        _this.savedLayouts(_arr);
                        _this.__applyLayout(result.ConteudoJson);
                    });
                }
            },
            openLayoutCustomize: function(saveAs) {
                var _this = this;
                var _open = function () {
                    require(['viewmodels/shared/gridConfiguration'], function (mdl) {
                        _this.closePopover();
                        mdl.showModal(vm, vm.gridSaveStates[gridId], gridId, saveAs).then(function (refreshSource, selectedIdLayout) {
                            _this.loadLayouts().then(function () {
                                if (selectedIdLayout > 0){
                                    _this.savedLayouts().forEach(function (item) {
                                        if (item.Id === selectedIdLayout) { 
                                            _this.currentLayoutId(selectedIdLayout);
                                            _this.currentLayout(item);
                                            _this.applyLayout();
                                        }
                                    });
                                }
                            });
                        });
                    });
                };
                if (this.currentLayout() && this.currentLayout().Id > 0 && isNullOrEmpty(this.currentLayout().ConteudoJson)) {
                    managerUser.getGridLayout(_this.currentLayout().Id).then(function (result) {
                        _this.currentLayout(result);
                        var _arr = _this.savedLayouts();
                        for (var i = 0 ; i < _arr.length; i++) {
                            if (_arr[i].Id === result.Id)
                                _arr[i] = result;
                        }
                        _this.savedLayouts(_arr);
                        _open();
                    });
                } else {
                    _open();
                }
            },
            loadLayouts: function () {
                var dfd = $.Deferred(), _this = this;
                managerUser.getAllGridLayouts(vm.__moduleId__, gridId).then(function (results) {
                    _this.savedLayouts(results);
                    _this.savedLayouts.splice(0, 0, _this.defaultLayout);
                    dfd.resolve();
                });
                return dfd;
            },
            deleteLayout: function () {
                var _this = this;
                return vm.app.showMessage('Deseja realmente excluir o Layout [' + _this.currentLayout().NomeLayout + ']?', 'Alerta', ['Yes', 'No'])
                .then(function (selectedOption) {
                    if (selectedOption === 'Yes') {
                        managerUser.deleteGridLayout(_this.currentLayout().Id, _this.currentLayout().Modulo, _this.currentLayout().NomeObjeto).then(function () {
                            vm.app.showMessage('Excluido com sucesso!', 'Alerta');
                            _this.loadLayouts().then(function () {
                                _this.currentLayoutId(_this.savedLayouts()[0].Id);
                                _this.applyLayout();
                            });;
                        });
                    }
                });
            },
            initialize: function () {
                var _this = this;
                _this.currentLayoutId.subscribe(function (newItem) {
                    _this.currentLayout(null);
                    var _arr = _this.savedLayouts();
                    for (var i = 0 ; i < _arr.length; i++) {
                        if (_arr[i].Id === newItem)
                            _this.currentLayout(_arr[i]);
                    }
                });
                _this.loadLayouts();
            }
        };
        vm.gridSaveStates[gridId].initialize();
        grid.igGrid({ height: (getGridHeightSuggested() * 0.75), width: '100%',
            dataSource: [],
            primaryKey: 'RowDataId',
            autoGenerateColumns: false,
            autofitLastColumn: false,
            dataSourceType: 'json',
            renderCheckboxes: true,
            autoCommit: true,
            cellClick: function(evt, ui) {
                 if (ui.cellElement && ui.cellElement.childNodes[0] && ui.cellElement.childNodes[0].childNodes[1]) {
                     var entity = null, e = ui.cellElement.childNodes[0].childNodes[1];
                     if (e && e.tagName === 'IMG' && vm.status() !== 'C')
                     {
                          entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowKey);
                          var key = e.attributes['key'].value;
                          var table = e.attributes['tableName'].value;
                          showMultimidia(entity, e, table, key, vm.WizardUI());
                     }
                 }
                 if (typeof vm.OnGridClientClick === 'function') {
                     entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowKey);
                     vm.OnGridClientClick('WizardUI_dGridVenda', ui.colKey, entity);
                 }
                 var grid = $('#WizardUI_dGridVenda');
                 var isEditing = grid.igGridUpdating('isEditing');
                 if (!isEditing)
                     grid.igGridUpdating('startEdit', ui.rowKey, (ui.colKey == undefined ? 0 : ui.colKey) , true);
            },
            enableUTCDates: true,
            featureChooserIconDisplay: 'always',
            rendered: function(evt, ui) {
                if (isNull(vm.gridSaveStates[ui.owner.id()].gridSaveStates)) {
                    vm.gridSaveStates[ui.owner.id()].gridSaveStates = gridSaveStates(ui.owner.element, vm);
                    vm.gridSaveStates[ui.owner.id()].defaultLayout = { Id: -1, NomeLayout: "Layout Padrão", ConteudoJson: vm.gridSaveStates[ui.owner.id()].gridSaveStates.save() };
                }
            },
            dataRendered: function(evt, ui) { 
            },
            columns: [
                { key: 'RowDataId', headerText: 'RowDataId', width: '50px', dataType: 'number', hidden: true },
                { key: 'BigIntVenda', headerText: 'Big Int Venda', width: '250px', dataType: 'number', columnCssClass: 'ellipsis', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'BitVenda', headerText: 'Bit Venda', width: '153px', dataType: 'bool', columnCssClass: 'ellipsis', format: 'checkbox', hidden: false, unbound: false, group: null   },
                { key: 'ComboboxVenda', headerText: 'Combobox Venda', width: '218px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null  , formatter: function (val, record) { return  vm.dataDomains.getName('LX_VENDA', val);} },
                { key: 'DatetimeVenda', headerText: 'Datetime Venda', width: '218px', dataType: 'date', columnCssClass: 'ellipsis', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null   },
                { key: 'DecimalVenda', headerText: 'Decimal Venda', width: '205px', dataType: 'number', columnCssClass: 'ellipsis', format: '0.00', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'IdCliente', headerText: 'Id Cliente', width: '166px', dataType: 'number', columnCssClass: 'ellipsis', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'IdLoja', headerText: 'Id Loja', width: '151px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                { key: 'IdVenda', headerText: 'Id Venda', width: '140px', dataType: 'number', columnCssClass: 'ellipsis', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'IntVenda', headerText: 'Int Venda', width: '153px', dataType: 'number', columnCssClass: 'ellipsis', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'SmallIntVenda', headerText: 'Small Int Venda', width: '231px', dataType: 'number', columnCssClass: 'ellipsis', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'StringVenda', headerText: 'String Venda', width: '400px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   }
            ],
            features: [
                        { name: 'Sorting', type: 'local', caseSensitive: true,
                          columnSorting: function (evt, ui) { 
                              $.grep(ui.owner.grid._visibleColumnsArray, function (e) { 
                                  if (e.key === ui.columnKey && e.dataType === 'string') 
                                      return $('#WizardUI_dGridVenda').igGridSorting('option', 'caseSensitive', false); 
                                  else if (e.key === ui.columnKey) 
                                      return $('#WizardUI_dGridVenda').igGridSorting('option', 'caseSensitive', true); 
                              }); 
                          } 
            },
                        { name: 'Filtering', mode: 'advanced', filterDropDownItemIcons: false, filterDropDownWidth: 200, allowFiltering: true, type: 'local', renderFC: false, renderFilterButton: true, 
                              dataFiltered: function (evt, ui) {
                              var columnsFilters = [];
                              $.each(ui.owner._currentAdvancedExpressions, function(i, item){ columnsFilters.push(item.fieldName); });
                              var cols = $('#' + ui.owner.grid.element[0].id + '_container .ui-iggrid-headertable th');
                              cols.each(function (i, item) {
                                  var name = item.id.substr(ui.owner.grid.element[0].id.length + 1);
                                  var filter = $(item).find('span.ui-icon-search');
                                  if (columnsFilters.contains(name)) {
                                      if (!filter.hasClass('grid-column-researched'))
                                          filter.addClass('grid-column-researched');
                                  } else {
                                      if (filter.hasClass('grid-column-researched'))
                                          filter.removeClass('grid-column-researched');
                                  }
                              });
                            }
                        },
                        { name: 'Selection', mode: 'row'},
                        { name: 'Tooltips', columnSettings:[{ columnKey: "BigIntVenda", allowTooltips: true },{ columnKey: "BitVenda", allowTooltips: true },{ columnKey: "ComboboxVenda", allowTooltips: true },{ columnKey: "DatetimeVenda", allowTooltips: true },{ columnKey: "DecimalVenda", allowTooltips: true },{ columnKey: "IdCliente", allowTooltips: true },{ columnKey: "IdLoja", allowTooltips: false },{ columnKey: "IdVenda", allowTooltips: true },{ columnKey: "IntVenda", allowTooltips: true },{ columnKey: "SmallIntVenda", allowTooltips: true },{ columnKey: "StringVenda", allowTooltips: true }] },
                        { name: 'Resizing' }, 
                        { name: 'Hiding', 
                            columnHidden: function (evt, ui) {
                               showMultimidiaLazy('#WizardUI_dGridVenda');
                            },
                            columnShown: function (evt, ui) {
                               showMultimidiaLazy('#WizardUI_dGridVenda');
                            }
                        },
                        { name: 'MultiColumnHeaders' }
                        ,{ name: 'ColumnMoving' }
            
            
                       ,{ name: 'Updating', horizontalMoveOnEnter: true,
                          enableDataDirtyException: false, 
                          generatePrimaryKeyValue: function(evt, ui){  },
                          enableDeleteRow: false,
                          enableAddRow: false,
                          startEditTriggers: 'click',
                          editMode:'cell', /*cell(atual) ou rowedittemplate(template)*/
                          rowEditDialogContainment: 'window',
                          showReadonlyEditors: false,
                          showDoneCancelButtons: false,
                          columnSettings: [{ columnKey: "IdLoja", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpLoja", isNullable: true, custom: vm.custom, vm: vm, verifyCanEditCol: verifyCanEditCol, allowMultiSelectionInSearch:true, validateOnClearState:false, maxValue:2147483647, maxLength: 12, defaultValue: null } }, { columnKey: 'DatetimeVenda', editorType: 'datepicker', editorOptions: {valueChanged: function(evt, ui){if (typeof vm.OnPropertyChangeDataGrid === 'function') {vm.OnPropertyChangeDataGrid('WizardUI_dGridVenda', 'DatetimeVenda', ui.oldValue, ui.value);}}, minValue: new Date(1900, 0, 1), datepickerOptions: { changeMonth: true, changeYear: true }  } }, { columnKey: 'ComboboxVenda', editorType: 'combo', editorOptions: {   selectionChanged: function (evt, ui) {   var val = null;   if (ui.items != null && ui.items.length > 0) { val = ui.items[0].data['id']; }  updateEntity('ComboboxVenda', val, false); },  mode: 'dropdown', dropDownOnFocus: true,  dataSource: vm.dataDomains.getItems('LX_VENDA', ''),  textKey: 'name', valueKey: 'id', enableClearButton: false }}, { columnKey: "BigIntVenda" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('WizardUI_dGridVenda', 'BigIntVenda', ui.oldValue, ui.value);}},  maxLength: 24, maxValue: null, minValue: 0, dataMode: 'long' } }, { columnKey: "DecimalVenda" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('WizardUI_dGridVenda', 'DecimalVenda', ui.oldValue, ui.value);}},  maxLength: 14, maxValue: 99999999999.99, minValue: 0, dataMode: 'decimal', minDecimals: 2, maxDecimals: 2 } }, { columnKey: "IdCliente" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('WizardUI_dGridVenda', 'IdCliente', ui.oldValue, ui.value);}},  maxLength: 12, maxValue: null, minValue: 0, dataMode: 'int' } }, { columnKey: "IdVenda" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('WizardUI_dGridVenda', 'IdVenda', ui.oldValue, ui.value);}},  maxLength: 12, maxValue: null, dataMode: 'int' } }, { columnKey: "IntVenda" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('WizardUI_dGridVenda', 'IntVenda', ui.oldValue, ui.value);}},  maxLength: 12, maxValue: null, minValue: 0, dataMode: 'int' } }, { columnKey: "SmallIntVenda" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('WizardUI_dGridVenda', 'SmallIntVenda', ui.oldValue, ui.value);}},  maxLength: 6, maxValue: null, minValue: 0, dataMode: 'short' } }, { columnKey: "StringVenda" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('WizardUI_dGridVenda', 'StringVenda', ui.oldValue, ui.value);}}, maxLength: 50 } }],
                          rowDeleting: function (evt, ui) {
                              deletedIndex = ui.element.context.rowIndex;
                              var entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowID);
                              if (entity) {
                                  vm.deleteEntity(entity);
                              }
                          },
                          rowDeleted: function (evt, ui) {
                              var grid = $('#WizardUI_dGridVenda');
                              var rows = grid.igGrid('allRows');
                              if (rows.length > 0)
                              {
                                  if (deletedIndex < 0) deletedIndex = 0;
                                  else if (rows.length <= deletedIndex) deletedIndex = rows.length - 1;
                                  grid.igGridSelection('selectRow', deletedIndex);
                                  grid.igGrid('scrollContainer').scrollTop(grid.igGrid('option', 'avgRowHeight') * deletedIndex);
                              }
                          },
                          editCellStarting: function(evt, ui) { 
                              var entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowID);
                              var canEditing = false, clear = vm.status() === 'C';
                              canEditing = verifyCanEditCol(ui.columnKey, clear, entity);
                              grid.igGridSelection('clearSelection');
                              grid.igGridSelection('selectRow', ui.owner._rowIndex);
                              if (!canEditing && vm.status() !== 'C') {
                                  var isDesc = grid.igGridSorting('option', 'columnSettings').filter(function (el) {
                                      var desc = el.currentSortDirection;
                                      if (desc !== undefined) return desc.indexOf('desc') > -1;
                                  });
                                  var canEditingOneField = false;
                                  var columnsVisible = ui.owner.grid._visibleColumnsArray;
                                  var rowId = ui.rowID, colId = ui.columnIndex;
                                  var colIndexVisible = 0;
                                  for (var i = 0; i < ui.owner.grid._visibleColumnsArray.length; i++) {
                                      var nameColumn = ui.owner.grid._visibleColumnsArray[i].key;
                                      canEditingOneField = canEditingOneField === true ? canEditingOneField : verifyCanEditCol(nameColumn, clear, entity);
                                      if (nameColumn === ui.columnKey) colIndexVisible = i;
                                  }
                                  if (canEditingOneField) {
                                      var indexColumn = colIndexVisible;
                                      var rowIndex = ui.owner._rowIndex;
                                      for (; indexColumn < ui.owner.grid._visibleColumnsArray.length;) {
                                          var colNameVisible = ui.owner.grid._visibleColumnsArray[indexColumn].key;
                                          canNewEditing = verifyCanEditCol(colNameVisible, clear, entity);
                                          if (canNewEditing) {
                                              if (ui.owner._rowIndex + 1 >= grid.igGrid('rows').length && ui.owner.grid._visibleColumnsArray.length <= indexColumn) rowId = (isDesc.length ? ui.rowID + ui.owner._rowIndex : ui.rowID - ui.owner._rowIndex);
                                              grid.igGridSelection('selectRow', rowIndex);
                                              grid.igGridUpdating('startEdit', rowId, indexColumn);
                                              break;
                                          }
                                          else {
                                              indexColumn++;
                                              if (event.toString() === '[object KeyboardEvent]') {
                                                 if (indexColumn >= ui.owner.grid._visibleColumnsArray.length) {
                                                     isDesc.length ? rowId-- : rowId++;
                                                     rowIndex++;
                                                     grid.igGridSelection('clearSelection');
                                                     indexColumn = 0;
                                                 }
                                              }
                                          }
                                      }
                                  }
                              }
                              return canEditing;
                          },
                          editCellStarted: function(evt, ui){
                              var lstRefreshDados = null;
                              var columns = $('#WizardUI_dGridVenda').igGridUpdating('option', 'columnSettings');
                              var currentCol = null;
                              currentRow = ui.rowID;
                              columns.forEach(function (entry, index) {
                                 if (entry.columnKey === ui.columnKey) currentCol = entry;
                                 if (currentCol != null) return false;
                              });
                              if (currentCol != null && currentCol.hasOwnProperty('editorType') && currentCol.editorType === 'combo') {
                                 var lookUpName = $(ui.editor).igCombo('option', 'inputName');
                                 if (lookUpName != null) {
                                     lstRefreshDados = vm.dataCombo.getItems(lookUpName, '');
                                     if (lstRefreshDados.length === 0)
                                         vm.dataCombo.fillDataCombos(lookUpName, ui.columnKey, vm.currentDataItem().currentVenda(), function (result) {
                                             ui.owner.endEdit(false, false);
                                             setTimeout(function () { ui.owner.startEdit(ui.rowID, ui.columnKey, true); }, 100);
                                         });
                                     else {
                                         $(ui.editor).igCombo('option', 'dataSource', lstRefreshDados);
                                         $(ui.editor).one('igcombodatabound', function () { setTimeout(function () { $(ui.editor).igCombo('openDropDown'); }, 10); });
                                     }
                                 }
                                 $(ui.editor).igCombo('openDropDown');
                              }
                          },
                          editCellEnded: function(evt, ui) {
                              currentRow = ui.rowID;
                              updateEntity(ui.columnKey, ui.value, !ui.update);
                              currentRow = null;
                          }
                        }
                    ]
            });
            if ((typeof vm.OnDataGridCreated === 'function')){
                vm.OnDataGridCreated('WizardUI_dGridVenda');
            }
            var selectionrowselectionchanged = null, selectedRowId = -1;
            selectionrowselectionchanged = function (evt, ui) {
                if ((ui.owner.grid.selectedRow() && typeof ui.owner.grid.selectedRow().id !== 'undefined') || (ui.owner.grid.selectedRows() && ui.owner.grid.selectedRows().length > 0)) { 
                    if (isNullOrEmpty(ui.owner.selectedRows())|| ui.selectedRows.length <= 1) {
                        $(document).undelegate('#WizardUI_dGridVenda', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                        ui.owner.clearSelection();
                        ui.owner.selectRow(ui.row.index);
                        selectedRowId = ui.row.id;
                        $(document).delegate ('#WizardUI_dGridVenda', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                    }
                    selectGridCurrentItem(vm.goToKey, 'RowDataId', ui, vm.currentDataItem().currentVenda, getDataSource()); 
                 } 
            };
            $(document).delegate('#WizardUI_dGridVenda', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
            $('#WizardUI_dGridVenda > tbody tr').live('focus', function(evt) {
                var grid = $('#WizardUI_dGridVenda'), row = $(this).closest('tr'), id = parseInt(row.attr('data-id'), 10);
                var selectedRows = grid.igGridSelection('option', 'multipleSelection') ? grid.igGridSelection('selectedRows') : [grid.igGridSelection('selectedRow')];
                if (selectedRowId === id) return;
                selectedRowId = id;
                grid.igGridSelection('selectRowById', id);
                grid.trigger('iggridselectionrowselectionchanged', {
                owner: grid.data('igGridSelection'),
                    row: {
                       element: row,
                       index: row.index(),
                       id: id
                    },
                    selectedRows: selectedRows
                 });
            });
        }
        vm.addDataSource({ key: 'WizardUI_dGridVenda', name: 'VendaList', itemsSource: itemsSource });
    }

, renderWizardUI_dGridVendaAtacado: function(vm) {
    var getDataSource = function() {
        var source = null;
        try {
            source = vm.currentDataItem().VendaAtacadoList;
        }
        catch (e) { console.log(e); }
        return isNullOrEmpty(source) ? ko.observableArray([]) : source;
    };
    var dataSourceIsLoaded = function() {
        var isLoaded = false;
        try {
            isLoaded = (vm.currentDataItem().VendaAtacadoIsLoaded === true || vm.currentDataItem().VendaAtacadoList().length > 0);
        }
        catch (e) {
            isLoaded = true;
        }
        return isLoaded;
    }
    $('#WizardUI_dGridVendaAtacado_headers').live('focus  keydown', function (evt) {
        var keyCode = window.event ? evt.which : evt.keyCode;
        if (keyCode === 9) {
            var cols = $('#WizardUI_dGridVendaAtacado').igGrid('option', 'columns');
            var dataView = $('#WizardUI_dGridVendaAtacado').data('igGrid').dataSource._dataView
            if (dataView.length === 0) return;
            var firstRow = dataView[0].RowDataId;
            clear = vm.status() === 'C';
            if (vm.status() === 'C')
                $('#WizardUI_dGridVendaAtacado').igGridUpdating('startEdit', firstRow, 0);
            else {
                var entity = findElementByKey(getDataSource(), 'RowDataId', firstRow);
                var indexColumn = 0;
                cols.some(function (entry) {
                    if (entry.key !== 'RowDataId' && !entry.hidden) {
                        if (verifyCanEditCol(entry.key, clear, entity)) {
                            $('#WizardUI_dGridVendaAtacado').igGridUpdating('startEdit', firstRow, indexColumn);
                            return true;
                        }
                        indexColumn++;
                    }
                });
            }
        }
    });
    var getVisibleColumns = function(metaDataControl) {
       if (metaDataControl) return '';
       var visibleColumns = '';
       if($('#WizardUI_dGridVendaAtacado').data('igGrid') === undefined) return '';
       var cols = $('#WizardUI_dGridVendaAtacado').igGrid('option', 'columns');
       if (cols) {
         for (var idx = 0; idx < cols.length; idx++) {
             if (cols[idx].hidden !== true) visibleColumns += (visibleColumns === '' ? '' : ',') + cols[idx].key;
         }
       }
       return visibleColumns;
    };
    var started = false;
    var currentRow = null;
    var updateEntity = function (columnKey, value, execDataBind) {
        if(value && Array.isArray(value) && value.length === 0) value = null;
        var entity = findElementByKey(getDataSource(), 'RowDataId', currentRow);
        if (entity != null && typeof value !== 'undefined' && getAbsoluteValue(entity[columnKey]) !== value) {
            setAbsoluteValue(entity, columnKey, value);
            if (execDataBind) itemsSource.dataBind(false);
        }
    };
    var isElementHided = function (grid, forceCreating) {
      if (!grid) grid = $('#WizardUI_dGridVendaAtacado');
      return ((!grid[0] || (!forceCreating && grid.parent().width() <= 0)) && !$('#dialogVendaAtacado').is(':visible'));
    }
    var refreshData = true;
    var itemsSource = { isElementHided: isElementHided, getVisibleColumns: getVisibleColumns, containerId: 'WizardUI_dGridVendaAtacado_container', dataBind: function (commitData, forceCreating) {
       var grid = $('#WizardUI_dGridVendaAtacado');
       if (started && typeof grid.data('igGridUpdating') === 'undefined') { started = false; }
       if (commitData && started) {
           if (grid.igGridUpdating('isEditing')) {
               grid.igGrid('commit');
           }
           return;
       }
       var execFillDetais = ((vm.status() !== 'C' && vm.status() !== 'I') && !dataSourceIsLoaded());
       if (forceCreating && started && !refreshData && !execFillDetais) return;
       var isHided = isElementHided(grid, forceCreating);
       refreshData = !forceCreating;
       if (refreshData && !isHided) refreshData = false;
       if (isHided) return;
       if (!started) {
           createDataGrid(grid);
           started = true;
           commitData = false;
           $('#WizardUI_dGridVendaAtacado_groupbyarea').addClass('hide');
       }
       if (grid.igGridUpdating('isEditing')) {
            grid.igGridUpdating('endEdit', true);
       }
       if (execFillDetais) {
         grid.igGrid("option", "dataSource", []);
         vm.currentDataItem().fillDetails(false, 'VendaAtacado');
         return;
       }
       grid.igGrid("option", "dataSource", unwrapObservableArray(getDataSource(), vm));
       var rows = grid.igGrid('allRows');
       if (rows.length > 0) {
         var verticalContainer = grid.igGrid('scrollContainer');
         var isSelected = false;
         if (vm.currentDataItem().currentVendaAtacado() != null)
         {
           for(var idx = 0; idx < rows.length; idx++)
           {
             if (rows[idx].dataset.id == getAbsoluteValue(vm.currentDataItem().currentVendaAtacado().RowDataId))
             {
                grid.igGridSelection('selectRow', idx);
                verticalContainer.scrollTop(grid.igGrid('option', 'avgRowHeight') * idx);
                isSelected = true;
                break;
             }
           }
         }
         if (!isSelected) {
             grid.igGridSelection('selectRow', 0);
             verticalContainer.scrollTop(0);
         }
         if ($('#dialogVendaAtacado').is(':visible')) {
            var hasPaging = $.grep(grid.igGrid('option', 'features'), function (e) {
               return e.name === 'Paging';
            });
            var totalGrid = grid.data('igGrid').options.dataSource.length;
            var current = 1;
            if (hasPaging.length > 0) {
               var totalCurrentPage = totalGrid;
               var currentPage = grid.igGridPaging('pageIndex') + 1;
               var pageIndex = grid.igGridPaging('pageIndex');
               var pageSize = grid.igGridPaging('pageSize');
               if (totalGrid / pageSize > currentPage) totalCurrentPage = (1 * grid.igGrid('rows').length);
               if (currentPage > 1) current = (pageIndex * pageSize) + current;
               $('label#currentNumberVendaAtacado').html(current + ' - ' + totalCurrentPage);
            }
            else
               $('label#currentNumberVendaAtacado').html(1);
            $('label#totalNumberVendaAtacado').html(totalGrid);
        }
       } else {
           $('label#currentNumberVendaAtacado').html(0);
           $('label#totalNumberVendaAtacado').html(0);
       }
    }};
    var valueGrouBy = -1;
    var deletedIndex = -1;
    function verifyCanEditCol(column, clear, entity){
        switch(column){
            case 'BigIntVendaAtacado': { canEditing = clear || vm.enabledForEditing(); break;}
            case 'BitVendaAtacado': { canEditing = clear || vm.enabledForEditing(); break;}
            case 'ComboboxVendaAtacado': { canEditing = clear || vm.enabledForEditing(); break;}
            case 'DatetimeVendaAtacado': { canEditing = clear || vm.enabledForEditing(); break;}
            case 'DecimalVendaAtacado': { canEditing = clear || vm.enabledForEditing(); break;}
            case 'IdCliente': { canEditing = clear || vm.enabledForEditing(); break;}
            case 'IdVendaAtacado': { canEditing = clear || (entity && entity.isAdded()); break;}
            case 'IntVendaAtacado': { canEditing = clear || vm.enabledForEditing(); break;}
            case 'SmallIntVendaAtacado': { canEditing = clear || vm.enabledForEditing(); break;}
            case 'StringVendaAtacado': { canEditing = clear || vm.enabledForEditing(); break;}
        }
        return canEditing;
    };
    function createDataGrid(grid) {
        var gridId = grid[0].id;
        vm.gridSaveStates[gridId] = {
            savedLayouts: ko.observableArray([]),
            currentLayout: ko.observable({ Id: 0 }),
            currentLayoutId: ko.observable(0),
            __applyLayout: function (jsonContent) {
                this.gridSaveStates.returnToSavedState(jsonContent);
                vm.dataToolbar.isBusy(false);
                this.closePopover();
            },
            closePopover: function () {
                $('#WizardUI_dGridVendaAtacado_LayoutBtn').igPopover('hide');
            },
            applyLayout: function (layoutInfo) {
                var _this = this;
                if (isNull(layoutInfo) && (!_this.currentLayout() || _this.currentLayout().Id === 0)) {
                    vm.app.showMessage('Não existe layout selecionado');
                    return;
                }
                vm.dataToolbar.isBusy(true);
                if (layoutInfo && layoutInfo.ConteudoJson) {
                    _this.__applyLayout(layoutInfo.ConteudoJson)
                }
                else if (_this.currentLayout() && _this.currentLayout().ConteudoJson) {
                    _this.__applyLayout(_this.currentLayout().ConteudoJson)
                } else {
                    managerUser.getGridLayout(_this.currentLayout().Id).then(function (result) {
                        _this.currentLayout(result);
                        var _arr = _this.savedLayouts(); 
                        for (var i = 0 ; i < _arr.length; i++) {
                            if (_arr[i].Id === result.Id)
                                _arr[i] = result;
                        }
                        _this.savedLayouts(_arr);
                        _this.__applyLayout(result.ConteudoJson);
                    });
                }
            },
            openLayoutCustomize: function(saveAs) {
                var _this = this;
                var _open = function () {
                    require(['viewmodels/shared/gridConfiguration'], function (mdl) {
                        _this.closePopover();
                        mdl.showModal(vm, vm.gridSaveStates[gridId], gridId, saveAs).then(function (refreshSource, selectedIdLayout) {
                            _this.loadLayouts().then(function () {
                                if (selectedIdLayout > 0){
                                    _this.savedLayouts().forEach(function (item) {
                                        if (item.Id === selectedIdLayout) { 
                                            _this.currentLayoutId(selectedIdLayout);
                                            _this.currentLayout(item);
                                            _this.applyLayout();
                                        }
                                    });
                                }
                            });
                        });
                    });
                };
                if (this.currentLayout() && this.currentLayout().Id > 0 && isNullOrEmpty(this.currentLayout().ConteudoJson)) {
                    managerUser.getGridLayout(_this.currentLayout().Id).then(function (result) {
                        _this.currentLayout(result);
                        var _arr = _this.savedLayouts();
                        for (var i = 0 ; i < _arr.length; i++) {
                            if (_arr[i].Id === result.Id)
                                _arr[i] = result;
                        }
                        _this.savedLayouts(_arr);
                        _open();
                    });
                } else {
                    _open();
                }
            },
            loadLayouts: function () {
                var dfd = $.Deferred(), _this = this;
                managerUser.getAllGridLayouts(vm.__moduleId__, gridId).then(function (results) {
                    _this.savedLayouts(results);
                    _this.savedLayouts.splice(0, 0, _this.defaultLayout);
                    dfd.resolve();
                });
                return dfd;
            },
            deleteLayout: function () {
                var _this = this;
                return vm.app.showMessage('Deseja realmente excluir o Layout [' + _this.currentLayout().NomeLayout + ']?', 'Alerta', ['Yes', 'No'])
                .then(function (selectedOption) {
                    if (selectedOption === 'Yes') {
                        managerUser.deleteGridLayout(_this.currentLayout().Id, _this.currentLayout().Modulo, _this.currentLayout().NomeObjeto).then(function () {
                            vm.app.showMessage('Excluido com sucesso!', 'Alerta');
                            _this.loadLayouts().then(function () {
                                _this.currentLayoutId(_this.savedLayouts()[0].Id);
                                _this.applyLayout();
                            });;
                        });
                    }
                });
            },
            initialize: function () {
                var _this = this;
                _this.currentLayoutId.subscribe(function (newItem) {
                    _this.currentLayout(null);
                    var _arr = _this.savedLayouts();
                    for (var i = 0 ; i < _arr.length; i++) {
                        if (_arr[i].Id === newItem)
                            _this.currentLayout(_arr[i]);
                    }
                });
                _this.loadLayouts();
            }
        };
        vm.gridSaveStates[gridId].initialize();
        grid.igGrid({ height: (getGridHeightSuggested() * 0.75), width: '100%',
            dataSource: [],
            primaryKey: 'RowDataId',
            autoGenerateColumns: false,
            autofitLastColumn: false,
            dataSourceType: 'json',
            renderCheckboxes: true,
            autoCommit: true,
            cellClick: function(evt, ui) {
                 if (ui.cellElement && ui.cellElement.childNodes[0] && ui.cellElement.childNodes[0].childNodes[1]) {
                     var entity = null, e = ui.cellElement.childNodes[0].childNodes[1];
                     if (e && e.tagName === 'IMG' && vm.status() !== 'C')
                     {
                          entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowKey);
                          var key = e.attributes['key'].value;
                          var table = e.attributes['tableName'].value;
                          showMultimidia(entity, e, table, key, vm.WizardUI());
                     }
                 }
                 if (typeof vm.OnGridClientClick === 'function') {
                     entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowKey);
                     vm.OnGridClientClick('WizardUI_dGridVendaAtacado', ui.colKey, entity);
                 }
                 var grid = $('#WizardUI_dGridVendaAtacado');
                 var isEditing = grid.igGridUpdating('isEditing');
                 if (!isEditing)
                     grid.igGridUpdating('startEdit', ui.rowKey, (ui.colKey == undefined ? 0 : ui.colKey) , true);
            },
            enableUTCDates: true,
            featureChooserIconDisplay: 'always',
            rendered: function(evt, ui) {
                if (isNull(vm.gridSaveStates[ui.owner.id()].gridSaveStates)) {
                    vm.gridSaveStates[ui.owner.id()].gridSaveStates = gridSaveStates(ui.owner.element, vm);
                    vm.gridSaveStates[ui.owner.id()].defaultLayout = { Id: -1, NomeLayout: "Layout Padrão", ConteudoJson: vm.gridSaveStates[ui.owner.id()].gridSaveStates.save() };
                }
            },
            dataRendered: function(evt, ui) { 
            },
            columns: [
                { key: 'RowDataId', headerText: 'RowDataId', width: '50px', dataType: 'number', hidden: true },
                { key: 'BigIntVendaAtacado', headerText: 'Big Int Venda Atacado', width: '309px', dataType: 'number', columnCssClass: 'ellipsis', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'BitVendaAtacado', headerText: 'Bit Venda Atacado', width: '257px', dataType: 'bool', columnCssClass: 'ellipsis', format: 'checkbox', hidden: false, unbound: false, group: null   },
                { key: 'ComboboxVendaAtacado', headerText: 'Combobox Venda Atacado', width: '322px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null  , formatter: function (val, record) { return  vm.dataDomains.getName('LX_VENDA_ATACADO', val);} },
                { key: 'DatetimeVendaAtacado', headerText: 'Datetime Venda Atacado', width: '322px', dataType: 'date', columnCssClass: 'ellipsis', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null   },
                { key: 'DecimalVendaAtacado', headerText: 'Decimal Venda Atacado', width: '309px', dataType: 'number', columnCssClass: 'ellipsis', format: '0.00', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'IdCliente', headerText: 'Id Cliente', width: '166px', dataType: 'number', columnCssClass: 'ellipsis', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'IdVendaAtacado', headerText: 'Id Venda Atacado', width: '244px', dataType: 'number', columnCssClass: 'ellipsis', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'IntVendaAtacado', headerText: 'Int Venda Atacado', width: '257px', dataType: 'number', columnCssClass: 'ellipsis', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'SmallIntVendaAtacado', headerText: 'Small Int Venda Atacado', width: '335px', dataType: 'number', columnCssClass: 'ellipsis', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'StringVendaAtacado', headerText: 'String Venda Atacado', width: '400px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   }
            ],
            features: [
                        { name: 'Sorting', type: 'local', caseSensitive: true,
                          columnSorting: function (evt, ui) { 
                              $.grep(ui.owner.grid._visibleColumnsArray, function (e) { 
                                  if (e.key === ui.columnKey && e.dataType === 'string') 
                                      return $('#WizardUI_dGridVendaAtacado').igGridSorting('option', 'caseSensitive', false); 
                                  else if (e.key === ui.columnKey) 
                                      return $('#WizardUI_dGridVendaAtacado').igGridSorting('option', 'caseSensitive', true); 
                              }); 
                          } 
            },
                        { name: 'Filtering', mode: 'advanced', filterDropDownItemIcons: false, filterDropDownWidth: 200, allowFiltering: true, type: 'local', renderFC: false, renderFilterButton: true, 
                              dataFiltered: function (evt, ui) {
                              var columnsFilters = [];
                              $.each(ui.owner._currentAdvancedExpressions, function(i, item){ columnsFilters.push(item.fieldName); });
                              var cols = $('#' + ui.owner.grid.element[0].id + '_container .ui-iggrid-headertable th');
                              cols.each(function (i, item) {
                                  var name = item.id.substr(ui.owner.grid.element[0].id.length + 1);
                                  var filter = $(item).find('span.ui-icon-search');
                                  if (columnsFilters.contains(name)) {
                                      if (!filter.hasClass('grid-column-researched'))
                                          filter.addClass('grid-column-researched');
                                  } else {
                                      if (filter.hasClass('grid-column-researched'))
                                          filter.removeClass('grid-column-researched');
                                  }
                              });
                            }
                        },
                        { name: 'Selection', mode: 'row'},
                        { name: 'Tooltips', columnSettings:[{ columnKey: "BigIntVendaAtacado", allowTooltips: true },{ columnKey: "BitVendaAtacado", allowTooltips: true },{ columnKey: "ComboboxVendaAtacado", allowTooltips: true },{ columnKey: "DatetimeVendaAtacado", allowTooltips: true },{ columnKey: "DecimalVendaAtacado", allowTooltips: true },{ columnKey: "IdCliente", allowTooltips: true },{ columnKey: "IdVendaAtacado", allowTooltips: true },{ columnKey: "IntVendaAtacado", allowTooltips: true },{ columnKey: "SmallIntVendaAtacado", allowTooltips: true },{ columnKey: "StringVendaAtacado", allowTooltips: true }] },
                        { name: 'Resizing' }, 
                        { name: 'Hiding', 
                            columnHidden: function (evt, ui) {
                               showMultimidiaLazy('#WizardUI_dGridVendaAtacado');
                            },
                            columnShown: function (evt, ui) {
                               showMultimidiaLazy('#WizardUI_dGridVendaAtacado');
                            }
                        },
                        { name: 'MultiColumnHeaders' }
                        ,{ name: 'ColumnMoving' }
            
            
                       ,{ name: 'Updating', horizontalMoveOnEnter: true,
                          enableDataDirtyException: false, 
                          generatePrimaryKeyValue: function(evt, ui){  },
                          enableDeleteRow: false,
                          enableAddRow: false,
                          startEditTriggers: 'click',
                          editMode:'cell', /*cell(atual) ou rowedittemplate(template)*/
                          rowEditDialogContainment: 'window',
                          showReadonlyEditors: false,
                          showDoneCancelButtons: false,
                          columnSettings: [{ columnKey: 'DatetimeVendaAtacado', editorType: 'datepicker', editorOptions: {valueChanged: function(evt, ui){if (typeof vm.OnPropertyChangeDataGrid === 'function') {vm.OnPropertyChangeDataGrid('WizardUI_dGridVendaAtacado', 'DatetimeVendaAtacado', ui.oldValue, ui.value);}}, minValue: new Date(1900, 0, 1), datepickerOptions: { changeMonth: true, changeYear: true }  } }, { columnKey: 'ComboboxVendaAtacado', editorType: 'combo', editorOptions: {   selectionChanged: function (evt, ui) {   var val = null;   if (ui.items != null && ui.items.length > 0) { val = ui.items[0].data['id']; }  updateEntity('ComboboxVendaAtacado', val, false); },  mode: 'dropdown', dropDownOnFocus: true,  dataSource: vm.dataDomains.getItems('LX_VENDA_ATACADO', ''),  textKey: 'name', valueKey: 'id', enableClearButton: false }}, { columnKey: "BigIntVendaAtacado" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('WizardUI_dGridVendaAtacado', 'BigIntVendaAtacado', ui.oldValue, ui.value);}},  maxLength: 24, maxValue: null, minValue: 0, dataMode: 'long' } }, { columnKey: "DecimalVendaAtacado" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('WizardUI_dGridVendaAtacado', 'DecimalVendaAtacado', ui.oldValue, ui.value);}},  maxLength: 14, maxValue: 99999999999.99, minValue: 0, dataMode: 'decimal', minDecimals: 2, maxDecimals: 2 } }, { columnKey: "IdCliente" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('WizardUI_dGridVendaAtacado', 'IdCliente', ui.oldValue, ui.value);}},  maxLength: 12, maxValue: null, minValue: 0, dataMode: 'int' } }, { columnKey: "IdVendaAtacado" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('WizardUI_dGridVendaAtacado', 'IdVendaAtacado', ui.oldValue, ui.value);}},  maxLength: 12, maxValue: null, dataMode: 'int' } }, { columnKey: "IntVendaAtacado" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('WizardUI_dGridVendaAtacado', 'IntVendaAtacado', ui.oldValue, ui.value);}},  maxLength: 12, maxValue: null, minValue: 0, dataMode: 'int' } }, { columnKey: "SmallIntVendaAtacado" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('WizardUI_dGridVendaAtacado', 'SmallIntVendaAtacado', ui.oldValue, ui.value);}},  maxLength: 6, maxValue: null, minValue: 0, dataMode: 'short' } }, { columnKey: "StringVendaAtacado" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('WizardUI_dGridVendaAtacado', 'StringVendaAtacado', ui.oldValue, ui.value);}}, maxLength: 50 } }],
                          rowDeleting: function (evt, ui) {
                              deletedIndex = ui.element.context.rowIndex;
                              var entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowID);
                              if (entity) {
                                  vm.deleteEntity(entity);
                              }
                          },
                          rowDeleted: function (evt, ui) {
                              var grid = $('#WizardUI_dGridVendaAtacado');
                              var rows = grid.igGrid('allRows');
                              if (rows.length > 0)
                              {
                                  if (deletedIndex < 0) deletedIndex = 0;
                                  else if (rows.length <= deletedIndex) deletedIndex = rows.length - 1;
                                  grid.igGridSelection('selectRow', deletedIndex);
                                  grid.igGrid('scrollContainer').scrollTop(grid.igGrid('option', 'avgRowHeight') * deletedIndex);
                              }
                          },
                          editCellStarting: function(evt, ui) { 
                              var entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowID);
                              var canEditing = false, clear = vm.status() === 'C';
                              canEditing = verifyCanEditCol(ui.columnKey, clear, entity);
                              grid.igGridSelection('clearSelection');
                              grid.igGridSelection('selectRow', ui.owner._rowIndex);
                              if (!canEditing && vm.status() !== 'C') {
                                  var isDesc = grid.igGridSorting('option', 'columnSettings').filter(function (el) {
                                      var desc = el.currentSortDirection;
                                      if (desc !== undefined) return desc.indexOf('desc') > -1;
                                  });
                                  var canEditingOneField = false;
                                  var columnsVisible = ui.owner.grid._visibleColumnsArray;
                                  var rowId = ui.rowID, colId = ui.columnIndex;
                                  var colIndexVisible = 0;
                                  for (var i = 0; i < ui.owner.grid._visibleColumnsArray.length; i++) {
                                      var nameColumn = ui.owner.grid._visibleColumnsArray[i].key;
                                      canEditingOneField = canEditingOneField === true ? canEditingOneField : verifyCanEditCol(nameColumn, clear, entity);
                                      if (nameColumn === ui.columnKey) colIndexVisible = i;
                                  }
                                  if (canEditingOneField) {
                                      var indexColumn = colIndexVisible;
                                      var rowIndex = ui.owner._rowIndex;
                                      for (; indexColumn < ui.owner.grid._visibleColumnsArray.length;) {
                                          var colNameVisible = ui.owner.grid._visibleColumnsArray[indexColumn].key;
                                          canNewEditing = verifyCanEditCol(colNameVisible, clear, entity);
                                          if (canNewEditing) {
                                              if (ui.owner._rowIndex + 1 >= grid.igGrid('rows').length && ui.owner.grid._visibleColumnsArray.length <= indexColumn) rowId = (isDesc.length ? ui.rowID + ui.owner._rowIndex : ui.rowID - ui.owner._rowIndex);
                                              grid.igGridSelection('selectRow', rowIndex);
                                              grid.igGridUpdating('startEdit', rowId, indexColumn);
                                              break;
                                          }
                                          else {
                                              indexColumn++;
                                              if (event.toString() === '[object KeyboardEvent]') {
                                                 if (indexColumn >= ui.owner.grid._visibleColumnsArray.length) {
                                                     isDesc.length ? rowId-- : rowId++;
                                                     rowIndex++;
                                                     grid.igGridSelection('clearSelection');
                                                     indexColumn = 0;
                                                 }
                                              }
                                          }
                                      }
                                  }
                              }
                              return canEditing;
                          },
                          editCellStarted: function(evt, ui){
                              var lstRefreshDados = null;
                              var columns = $('#WizardUI_dGridVendaAtacado').igGridUpdating('option', 'columnSettings');
                              var currentCol = null;
                              currentRow = ui.rowID;
                              columns.forEach(function (entry, index) {
                                 if (entry.columnKey === ui.columnKey) currentCol = entry;
                                 if (currentCol != null) return false;
                              });
                              if (currentCol != null && currentCol.hasOwnProperty('editorType') && currentCol.editorType === 'combo') {
                                 var lookUpName = $(ui.editor).igCombo('option', 'inputName');
                                 if (lookUpName != null) {
                                     lstRefreshDados = vm.dataCombo.getItems(lookUpName, '');
                                     if (lstRefreshDados.length === 0)
                                         vm.dataCombo.fillDataCombos(lookUpName, ui.columnKey, vm.currentDataItem().currentVendaAtacado(), function (result) {
                                             ui.owner.endEdit(false, false);
                                             setTimeout(function () { ui.owner.startEdit(ui.rowID, ui.columnKey, true); }, 100);
                                         });
                                     else {
                                         $(ui.editor).igCombo('option', 'dataSource', lstRefreshDados);
                                         $(ui.editor).one('igcombodatabound', function () { setTimeout(function () { $(ui.editor).igCombo('openDropDown'); }, 10); });
                                     }
                                 }
                                 $(ui.editor).igCombo('openDropDown');
                              }
                          },
                          editCellEnded: function(evt, ui) {
                              currentRow = ui.rowID;
                              updateEntity(ui.columnKey, ui.value, !ui.update);
                              currentRow = null;
                          }
                        }
                    ]
            });
            if ((typeof vm.OnDataGridCreated === 'function')){
                vm.OnDataGridCreated('WizardUI_dGridVendaAtacado');
            }
            var selectionrowselectionchanged = null, selectedRowId = -1;
            selectionrowselectionchanged = function (evt, ui) {
                if ((ui.owner.grid.selectedRow() && typeof ui.owner.grid.selectedRow().id !== 'undefined') || (ui.owner.grid.selectedRows() && ui.owner.grid.selectedRows().length > 0)) { 
                    if (isNullOrEmpty(ui.owner.selectedRows())|| ui.selectedRows.length <= 1) {
                        $(document).undelegate('#WizardUI_dGridVendaAtacado', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                        ui.owner.clearSelection();
                        ui.owner.selectRow(ui.row.index);
                        selectedRowId = ui.row.id;
                        $(document).delegate ('#WizardUI_dGridVendaAtacado', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                    }
                    selectGridCurrentItem(vm.goToKey, 'RowDataId', ui, vm.currentDataItem().currentVendaAtacado, getDataSource()); 
                 } 
            };
            $(document).delegate('#WizardUI_dGridVendaAtacado', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
            $('#WizardUI_dGridVendaAtacado > tbody tr').live('focus', function(evt) {
                var grid = $('#WizardUI_dGridVendaAtacado'), row = $(this).closest('tr'), id = parseInt(row.attr('data-id'), 10);
                var selectedRows = grid.igGridSelection('option', 'multipleSelection') ? grid.igGridSelection('selectedRows') : [grid.igGridSelection('selectedRow')];
                if (selectedRowId === id) return;
                selectedRowId = id;
                grid.igGridSelection('selectRowById', id);
                grid.trigger('iggridselectionrowselectionchanged', {
                owner: grid.data('igGridSelection'),
                    row: {
                       element: row,
                       index: row.index(),
                       id: id
                    },
                    selectedRows: selectedRows
                 });
            });
        }
        vm.addDataSource({ key: 'WizardUI_dGridVendaAtacado', name: 'VendaAtacadoList', itemsSource: itemsSource });
    }


    };
    complement.changedBrands = function changedBrands(gridName, infoColumns) {
        //infoColumns[] - {columnName: 'Column1', format: '0.00', decimals: 2}
        if (infoColumns !== null) {
            var i, j, grd = $lx(vm, '#' + gridName).data('igGrid'),
                grdUpd = $lx(vm, '#' + gridName).data('igGridUpdating');
            for (i = 0; i < grd.options.columns.length; i++) {
                for (j = 0; j < infoColumns.length; j++) {
                    if (grd.options.columns[i].key === infoColumns[j].columnName)
                        grd.options.columns[i].format = infoColumns[j].format;
                }
            }
            for (i = 0; i < grdUpd.options.columnSettings.length; i++) {
                for (j = 0; j < infoColumns.length; j++) {
                    if (grdUpd.options.columnSettings[i].columnKey === infoColumns[j].columnName) {
                        grdUpd.options.columnSettings[i].editorOptions.minDecimals = infoColumns[j].decimals;
                        grdUpd.options.columnSettings[i].editorOptions.maxDecimals = infoColumns[j].decimals;
                    }
                }
            }
            grd.dataBind();
        }
    };
    
    return complement;
}

return complementCtor;
});
