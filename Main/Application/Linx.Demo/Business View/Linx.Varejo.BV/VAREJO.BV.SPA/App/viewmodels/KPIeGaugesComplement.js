define(['managers/__auth', 'managers/user'], function (managerAuth, managerUser) {
var complementCtor = function() {
    var complement = {
    isAutomatic: true
    , renderKPIeGauges_dGridVendaItem: function(vm) {
    var dadosGaugeBigIntVendaItem = vm.getKPIGridGaugeGrid(function(ranges, min, max) { complement.renderKPIeGauges_dGridVendaItem(vm);  });
    var getDataSource = function() {
        var source = null;
        try {
            source = vm.currentDataItem().VendaItemList;
        }
        catch (e) { console.log(e); }
        return isNullOrEmpty(source) ? ko.observableArray([]) : source;
    };
    var dataSourceIsLoaded = function() {
        var isLoaded = false;
        try {
            isLoaded = (vm.currentDataItem().VendaItemIsLoaded === true || vm.currentDataItem().VendaItemList().length > 0);
        }
        catch (e) {
            isLoaded = true;
        }
        return isLoaded;
    }
    $('#KPIeGauges_dGridVendaItem_headers').live('focus  keydown', function (evt) {
        var keyCode = window.event ? evt.which : evt.keyCode;
        if (keyCode === 9) {
            var cols = $('#KPIeGauges_dGridVendaItem').igGrid('option', 'columns');
            var dataView = $('#KPIeGauges_dGridVendaItem').data('igGrid').dataSource._dataView
            if (dataView.length === 0) return;
            var firstRow = dataView[0].RowDataId;
            clear = vm.status() === 'C';
            if (vm.status() === 'C')
                $('#KPIeGauges_dGridVendaItem').igGridUpdating('startEdit', firstRow, 0);
            else {
                var entity = findElementByKey(getDataSource(), 'RowDataId', firstRow);
                var indexColumn = 0;
                cols.some(function (entry) {
                    if (entry.key !== 'RowDataId' && !entry.hidden) {
                        if (verifyCanEditCol(entry.key, clear, entity)) {
                            $('#KPIeGauges_dGridVendaItem').igGridUpdating('startEdit', firstRow, indexColumn);
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
       if($('#KPIeGauges_dGridVendaItem').data('igGrid') === undefined) return '';
       var cols = $('#KPIeGauges_dGridVendaItem').igGrid('option', 'columns');
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
      if (!grid) grid = $('#KPIeGauges_dGridVendaItem');
      return ((!grid[0] || (!forceCreating && grid.parent().width() <= 0)) && !$('#dialogVendaItem').is(':visible'));
    }
    var refreshData = true;
    var itemsSource = { isElementHided: isElementHided, getVisibleColumns: getVisibleColumns, containerId: 'KPIeGauges_dGridVendaItem_container', dataBind: function (commitData, forceCreating) {
       var grid = $('#KPIeGauges_dGridVendaItem');
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
           $('#KPIeGauges_dGridVendaItem_groupbyarea').addClass('hide');
       }
       if (grid.igGridUpdating('isEditing')) {
            grid.igGridUpdating('endEdit', true);
       }
       if (execFillDetais) {
         grid.igGrid("option", "dataSource", []);
         vm.currentDataItem().fillDetails(false, 'VendaItem');
         return;
       }
       grid.igGrid("option", "dataSource", unwrapObservableArray(getDataSource(), vm));
       var rows = grid.igGrid('allRows');
       if (rows.length > 0) {
         var verticalContainer = grid.igGrid('scrollContainer');
         var isSelected = false;
         if (vm.currentDataItem().currentVendaItem() != null)
         {
           for(var idx = 0; idx < rows.length; idx++)
           {
             if (rows[idx].dataset.id == getAbsoluteValue(vm.currentDataItem().currentVendaItem().RowDataId))
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
         if ($('#dialogVendaItem').is(':visible')) {
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
               $('label#currentNumberVendaItem').html(current + ' - ' + totalCurrentPage);
            }
            else
               $('label#currentNumberVendaItem').html(1);
            $('label#totalNumberVendaItem').html(totalGrid);
        }
       } else {
           $('label#currentNumberVendaItem').html(0);
           $('label#totalNumberVendaItem').html(0);
       }
    }};
    var valueGrouBy = -1;
    var deletedIndex = -1;
    function verifyCanEditCol(column, clear, entity){
        switch(column){
            case 'BigIntVendaItem': { canEditing = clear || vm.enabledForEditing(); break;}
            case 'BigIntVendaItemKpiInfo': { canEditing = clear; break;}
            case 'BitVendaItem': { canEditing = clear || vm.enabledForEditing(); break;}
            case 'ComboboxVendaItem': { canEditing = clear || vm.enabledForEditing(); break;}
            case 'DatetimeVendaItem': { canEditing = clear || vm.enabledForEditing(); break;}
            case 'DecimalVendaItem': { canEditing = clear || vm.enabledForEditing(); break;}
            case 'IdVenda': { canEditing = clear || vm.enabledForEditing(); break;}
            case 'IdVendaItem': { canEditing = clear || (entity && entity.isAdded()); break;}
            case 'IntVendaItem': { canEditing = clear || vm.enabledForEditing(); break;}
            case 'SmallIntVendaItem': { canEditing = clear || vm.enabledForEditing(); break;}
            case 'StringVendaItem': { canEditing = clear || vm.enabledForEditing(); break;}
        }
        return canEditing;
    };
    function makeGauge(val, record, field, solid, sufix) {
        var row = 0, value = 0;
        if (record.RowDataId > 0) {
            row = record.RowDataId;
            value = record[field];
            if (solid) {
                 var descValue = record[field + (isNullOrEmpty(sufix) ? "" : sufix)];
                 return "<div id='c" + row + field + sufix + "' style='color:black;text-align:" + (isNullOrEmpty(sufix) ? "right" : "center") + ";background-color:" + vm.getKpiColor(eval(eval("dadosGauge" + field).ranges), value) + ";'><strong" + (isNullOrEmpty(sufix) ? " style='margin-right: 5px;'" : "") + ">" + descValue + "</strong></div>";
            }
            else
                 return "<div id='g" + row + field + "' class='gauge' style='width:400px;height:20px;'></div> <script id='scriptg" + row + field + "'>$('#g" + row + field + "').kendoLinearGauge( {gaugeArea: {background: 'transparent', width:230}, pointer: { value: " + value + ", color: '#8B8386', shape: 'arrow' }, scale: { vertical: false ,line:{visible: false}, labels: {visible: false}, min: " + eval("dadosGauge" + field).min + ", max: " + eval("dadosGauge" + field).max + ", ranges: " + eval("dadosGauge" + field).ranges + " } });</script>"
        }
        return '';
    }
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
                $('#KPIeGauges_dGridVendaItem_LayoutBtn').igPopover('hide');
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
                          showMultimidia(entity, e, table, key, vm.KPIeGauges());
                     }
                 }
                 if (typeof vm.OnGridClientClick === 'function') {
                     entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowKey);
                     vm.OnGridClientClick('KPIeGauges_dGridVendaItem', ui.colKey, entity);
                 }
                 var grid = $('#KPIeGauges_dGridVendaItem');
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
                if ($('.gauge').length) {
                    var x = document.getElementsByClassName('gauge');
                    for (var i = 0; i < x.length; i++)
                        eval(document.getElementById('script' + x[i].id).innerHTML);
                }
            },
            columns: [
                { key: 'RowDataId', headerText: 'RowDataId', width: '50px', dataType: 'number', hidden: true },
                { key: 'BigIntVendaItem', headerText: 'Big Int Venda Item', width: '270px', dataType: 'number', columnCssClass: 'ellipsis', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'BigIntVendaItemKpiInfo', headerText: 'Big Int Venda Item (KPI)', width: '348px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null, formatter: function (val, record) { return makeGauge(val, record, 'BigIntVendaItem', true, 'KpiInfo'); } },
                { key: 'BigIntVendaItemKpiInfo', headerText: 'Big Int Venda Item (Gauge)', width: '348px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null, formatter: function (val, record) { return makeGauge(val, record, 'BigIntVendaItem'); } },
                { key: 'BitVendaItem', headerText: 'Bit Venda Item', width: '218px', dataType: 'bool', columnCssClass: 'ellipsis', format: 'checkbox', hidden: false, unbound: false, group: null   },
                { key: 'ComboboxVendaItem', headerText: 'Combobox Venda Item', width: '283px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null  , formatter: function (val, record) { return  vm.dataDomains.getName('LX_VENDA_ITEM', val);} },
                { key: 'DatetimeVendaItem', headerText: 'Datetime Venda Item', width: '283px', dataType: 'date', columnCssClass: 'ellipsis', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null   },
                { key: 'DecimalVendaItem', headerText: 'Decimal Venda Item', width: '270px', dataType: 'number', columnCssClass: 'ellipsis', format: '0.00', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'IdVenda', headerText: 'Id Venda', width: '140px', dataType: 'number', columnCssClass: 'ellipsis', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'IdVendaItem', headerText: 'Id Venda Item', width: '205px', dataType: 'number', columnCssClass: 'ellipsis', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'IntVendaItem', headerText: 'Int Venda Item', width: '218px', dataType: 'number', columnCssClass: 'ellipsis', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'SmallIntVendaItem', headerText: 'Small Int Venda Item', width: '296px', dataType: 'number', columnCssClass: 'ellipsis', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'StringVendaItem', headerText: 'String Venda Item', width: '400px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   }
            ],
            features: [
                        { name: 'Sorting', type: 'local', caseSensitive: true,
                          columnSorting: function (evt, ui) { 
                              $.grep(ui.owner.grid._visibleColumnsArray, function (e) { 
                                  if (e.key === ui.columnKey && e.dataType === 'string') 
                                      return $('#KPIeGauges_dGridVendaItem').igGridSorting('option', 'caseSensitive', false); 
                                  else if (e.key === ui.columnKey) 
                                      return $('#KPIeGauges_dGridVendaItem').igGridSorting('option', 'caseSensitive', true); 
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
                        { name: 'Tooltips', columnSettings:[{ columnKey: "BigIntVendaItem", allowTooltips: true },{ columnKey: "BigIntVendaItemKpiInfo", allowTooltips: true },{ columnKey: "BigIntVendaItemKpiInfo", allowTooltips: true },{ columnKey: "BitVendaItem", allowTooltips: true },{ columnKey: "ComboboxVendaItem", allowTooltips: true },{ columnKey: "DatetimeVendaItem", allowTooltips: true },{ columnKey: "DecimalVendaItem", allowTooltips: true },{ columnKey: "IdVenda", allowTooltips: true },{ columnKey: "IdVendaItem", allowTooltips: true },{ columnKey: "IntVendaItem", allowTooltips: true },{ columnKey: "SmallIntVendaItem", allowTooltips: true },{ columnKey: "StringVendaItem", allowTooltips: true }] },
                        { name: 'Resizing' }, 
                        { name: 'Hiding', 
                            columnHidden: function (evt, ui) {
                               showMultimidiaLazy('#KPIeGauges_dGridVendaItem');
                            },
                            columnShown: function (evt, ui) {
                               showMultimidiaLazy('#KPIeGauges_dGridVendaItem');
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
                          columnSettings: [{ columnKey: 'DatetimeVendaItem', editorType: 'datepicker', editorOptions: {valueChanged: function(evt, ui){if (typeof vm.OnPropertyChangeDataGrid === 'function') {vm.OnPropertyChangeDataGrid('KPIeGauges_dGridVendaItem', 'DatetimeVendaItem', ui.oldValue, ui.value);}}, minValue: new Date(1900, 0, 1), datepickerOptions: { changeMonth: true, changeYear: true }  } }, { columnKey: 'ComboboxVendaItem', editorType: 'combo', editorOptions: {   selectionChanged: function (evt, ui) {   var val = null;   if (ui.items != null && ui.items.length > 0) { val = ui.items[0].data['id']; }  updateEntity('ComboboxVendaItem', val, false); },  mode: 'dropdown', dropDownOnFocus: true,  dataSource: vm.dataDomains.getItems('LX_VENDA_ITEM', ''),  textKey: 'name', valueKey: 'id', enableClearButton: false }}, { columnKey: "BigIntVendaItem" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('KPIeGauges_dGridVendaItem', 'BigIntVendaItem', ui.oldValue, ui.value);}},  maxLength: 24, maxValue: null, minValue: 0, dataMode: 'long' } }, { columnKey: "DecimalVendaItem" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('KPIeGauges_dGridVendaItem', 'DecimalVendaItem', ui.oldValue, ui.value);}},  maxLength: 14, maxValue: 99999999999.99, minValue: 0, dataMode: 'decimal', minDecimals: 2, maxDecimals: 2 } }, { columnKey: "IdVenda" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('KPIeGauges_dGridVendaItem', 'IdVenda', ui.oldValue, ui.value);}},  maxLength: 12, maxValue: null, minValue: 0, dataMode: 'int' } }, { columnKey: "IdVendaItem" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('KPIeGauges_dGridVendaItem', 'IdVendaItem', ui.oldValue, ui.value);}},  maxLength: 12, maxValue: null, dataMode: 'int' } }, { columnKey: "IntVendaItem" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('KPIeGauges_dGridVendaItem', 'IntVendaItem', ui.oldValue, ui.value);}},  maxLength: 12, maxValue: null, minValue: 0, dataMode: 'int' } }, { columnKey: "SmallIntVendaItem" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('KPIeGauges_dGridVendaItem', 'SmallIntVendaItem', ui.oldValue, ui.value);}},  maxLength: 6, maxValue: null, minValue: 0, dataMode: 'short' } }, { columnKey: "StringVendaItem" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('KPIeGauges_dGridVendaItem', 'StringVendaItem', ui.oldValue, ui.value);}}, maxLength: 50 } }],
                          rowDeleting: function (evt, ui) {
                              deletedIndex = ui.element.context.rowIndex;
                              var entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowID);
                              if (entity) {
                                  vm.deleteEntity(entity);
                              }
                          },
                          rowDeleted: function (evt, ui) {
                              var grid = $('#KPIeGauges_dGridVendaItem');
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
                              var columns = $('#KPIeGauges_dGridVendaItem').igGridUpdating('option', 'columnSettings');
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
                                         vm.dataCombo.fillDataCombos(lookUpName, ui.columnKey, vm.currentDataItem().currentVendaItem(), function (result) {
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
                vm.OnDataGridCreated('KPIeGauges_dGridVendaItem');
            }
            var selectionrowselectionchanged = null, selectedRowId = -1;
            selectionrowselectionchanged = function (evt, ui) {
                if ((ui.owner.grid.selectedRow() && typeof ui.owner.grid.selectedRow().id !== 'undefined') || (ui.owner.grid.selectedRows() && ui.owner.grid.selectedRows().length > 0)) { 
                    if (isNullOrEmpty(ui.owner.selectedRows())|| ui.selectedRows.length <= 1) {
                        $(document).undelegate('#KPIeGauges_dGridVendaItem', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                        ui.owner.clearSelection();
                        ui.owner.selectRow(ui.row.index);
                        selectedRowId = ui.row.id;
                        $(document).delegate ('#KPIeGauges_dGridVendaItem', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                    }
                    selectGridCurrentItem(vm.goToKey, 'RowDataId', ui, vm.currentDataItem().currentVendaItem, getDataSource()); 
                 } 
            };
            $(document).delegate('#KPIeGauges_dGridVendaItem', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
            $('#KPIeGauges_dGridVendaItem > tbody tr').live('focus', function(evt) {
                var grid = $('#KPIeGauges_dGridVendaItem'), row = $(this).closest('tr'), id = parseInt(row.attr('data-id'), 10);
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
        vm.addDataSource({ key: 'KPIeGauges_dGridVendaItem', name: 'VendaItemList', itemsSource: itemsSource });
    }


, renderscyKPIeGauges_dGrid: function(vm) {
    var getDataSource = function() {
        var source = null;
        try {
            source = vm.dataView;
        }
        catch (e) { console.log(e); }
        return isNullOrEmpty(source) ? ko.observableArray([]) : source;
    };
    $('#scyKPIeGauges_dGrid_headers').live('focus  keydown', function (evt) {
        var keyCode = window.event ? evt.which : evt.keyCode;
        if (keyCode === 9) {
            var cols = $('#scyKPIeGauges_dGrid').igGrid('option', 'columns');
            var dataView = $('#scyKPIeGauges_dGrid').data('igGrid').dataSource._dataView
            if (dataView.length === 0) return;
            var firstRow = dataView[0].RowDataId;
            clear = vm.status() === 'C';
            if (vm.status() === 'C')
                $('#scyKPIeGauges_dGrid').igGridUpdating('startEdit', firstRow, 0);
            else {
                var entity = findElementByKey(getDataSource(), 'RowDataId', firstRow);
                var indexColumn = 0;
                cols.some(function (entry) {
                    if (entry.key !== 'RowDataId' && !entry.hidden) {
                        if (verifyCanEditCol(entry.key, clear, entity)) {
                            $('#scyKPIeGauges_dGrid').igGridUpdating('startEdit', firstRow, indexColumn);
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
       if($('#scyKPIeGauges_dGrid').data('igGrid') === undefined) return '';
       var cols = $('#scyKPIeGauges_dGrid').igGrid('option', 'columns');
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
      if (!grid) grid = $('#scyKPIeGauges_dGrid');
      return ((!grid[0] || (!forceCreating && grid.parent().width() <= 0)) && !$('#dialog').is(':visible'));
    }
    var refreshData = true;
    var itemsSource = { isElementHided: isElementHided, getVisibleColumns: getVisibleColumns, containerId: 'scyKPIeGauges_dGrid_container', dataBind: function (commitData, forceCreating) {
       var grid = $('#scyKPIeGauges_dGrid');
       if (started && typeof grid.data('igGridUpdating') === 'undefined') { started = false; }
       if (commitData && started) {
           if (grid.igGridUpdating('isEditing')) {
               grid.igGrid('commit');
           }
           return;
       }
       if (forceCreating && started && !refreshData) return;
       var isHided = isElementHided(grid, forceCreating);
       refreshData = !forceCreating;
       if (refreshData && !isHided) refreshData = false;
       if (isHided) return;
       if (!started) {
           createDataGrid(grid);
           started = true;
           commitData = false;
           $('#scyKPIeGauges_dGrid_groupbyarea').addClass('hide');
       }
       if (grid.igGridUpdating('isEditing')) {
            grid.igGridUpdating('endEdit', true);
       }
       grid.data('igGridSorting')._shouldFireColumnSorted = false;
       grid.igGrid("option", "dataSource", unwrapObservableArray(getDataSource(), vm));
       grid.data('igGridSorting')._shouldFireColumnSorted = true;
       var rows = grid.igGrid('allRows');
       if (rows.length > 0) {
         var verticalContainer = grid.igGrid('scrollContainer');
         var isSelected = false;
         if (vm.currentDataItem() != null)
         {
           for(var idx = 0; idx < rows.length; idx++)
           {
             if (rows[idx].dataset.id == getAbsoluteValue(vm.currentDataItem().RowDataId))
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
         $(grid.selector + '_container').focus();
         if ($('#dialog').is(':visible')) {
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
               $('label#currentNumber').html(current + ' - ' + totalCurrentPage);
            }
            else
               $('label#currentNumber').html(1);
            $('label#totalNumber').html(totalGrid);
        }
       } else {
           $('label#currentNumber').html(0);
           $('label#totalNumber').html(0);
       }
    }};
    var valueGrouBy = -1;
    var deletedIndex = -1;
    function verifyCanEditCol(column, clear, entity){
        switch(column){
            case 'IdVenda': { canEditing = clear; break;}
            case 'BigIntVenda': { canEditing = clear; break;}
            case 'BigIntVendaKpiInfo': { canEditing = clear; break;}
        }
        return canEditing;
    };
    function makeGauge(val, record, field, solid, sufix) {
        var row = 0, value = 0;
        if (record.RowDataId > 0) {
            row = record.RowDataId;
            value = record[field];
            if (solid) {
                 var descValue = record[field + (isNullOrEmpty(sufix) ? "" : sufix)];
                 return "<div id='c" + row + field + sufix + "' style='color:black;text-align:" + (isNullOrEmpty(sufix) ? "right" : "center") + ";background-color:" + vm.getKpiColor(eval(eval("dadosGauge" + field).ranges), value) + ";'><strong" + (isNullOrEmpty(sufix) ? " style='margin-right: 5px;'" : "") + ">" + descValue + "</strong></div>";
            }
            else
                 return "<div id='g" + row + field + "' class='gauge' style='width:400px;height:20px;'></div> <script id='scriptg" + row + field + "'>$('#g" + row + field + "').kendoLinearGauge( {gaugeArea: {background: 'transparent', width:230}, pointer: { value: " + value + ", color: '#8B8386', shape: 'arrow' }, scale: { vertical: false ,line:{visible: false}, labels: {visible: false}, min: " + eval("dadosGauge" + field).min + ", max: " + eval("dadosGauge" + field).max + ", ranges: " + eval("dadosGauge" + field).ranges + " } });</script>"
        }
        return '';
    }
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
                $('#scyKPIeGauges_dGrid_LayoutBtn').igPopover('hide');
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
        grid.igGrid({ height: (vm.isDependentVM() ? getGridHeightSuggested() * 0.7 : $(window).height() * 0.85), width: '100%',
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
                          showMultimidia(entity, e, table, key, vm.KPIeGauges());
                     }
                 }
                 if (typeof vm.OnGridClientClick === 'function') {
                     entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowKey);
                     vm.OnGridClientClick('scyKPIeGauges_dGrid', ui.colKey, entity);
                 }
                 var grid = $('#scyKPIeGauges_dGrid');
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
                if ($('.gauge').length) {
                    var x = document.getElementsByClassName('gauge');
                    for (var i = 0; i < x.length; i++)
                        eval(document.getElementById('script' + x[i].id).innerHTML);
                }
            },
            columns: [
                { key: 'RowDataId', headerText: 'RowDataId', width: '50px', dataType: '', hidden: true },
                { key: 'IdVenda', headerText: 'Id Venda', width: '140px', dataType: 'number', columnCssClass: 'ellipsis', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'BigIntVenda', headerText: 'Big Int Venda', width: '250px', dataType: 'number', columnCssClass: 'ellipsis', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'BigIntVendaKpiInfo', headerText: 'Big Int Venda (KPI)', width: '283px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   }
            ],
            features: [
                        { name: 'Sorting', type: 'local', caseSensitive: true,
                          columnSorting: function (evt, ui) { 
                              $.grep(ui.owner.grid._visibleColumnsArray, function (e) { 
                                  if (e.key === ui.columnKey && e.dataType === 'string') 
                                      return $('#scyKPIeGauges_dGrid').igGridSorting('option', 'caseSensitive', false); 
                                  else if (e.key === ui.columnKey) 
                                      return $('#scyKPIeGauges_dGrid').igGridSorting('option', 'caseSensitive', true); 
                              }); 
                          } 
                          , columnSorted: function (event, args) { if (!isNullOrEmpty(args.columnKey) && !isNullOrEmpty(args.direction)) { vm.sortData(args.columnKey + ' ' + args.direction); } } },
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
                        { name: 'Tooltips', columnSettings:[{ columnKey: "IdVenda", allowTooltips: true },{ columnKey: "BigIntVenda", allowTooltips: true },{ columnKey: "BigIntVendaKpiInfo", allowTooltips: true }] },
                        { name: 'Resizing' }, 
                        { name: 'Hiding', 
                            columnHidden: function (evt, ui) {
                               showMultimidiaLazy('#scyKPIeGauges_dGrid');
                            },
                            columnShown: function (evt, ui) {
                               showMultimidiaLazy('#scyKPIeGauges_dGrid');
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
                          columnSettings: [{ columnKey: "IdVenda" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('scyKPIeGauges_dGrid', 'IdVenda', ui.oldValue, ui.value);}},  maxLength: 12, maxValue: null, dataMode: 'int' } }, { columnKey: "BigIntVenda" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('scyKPIeGauges_dGrid', 'BigIntVenda', ui.oldValue, ui.value);}},  maxLength: 24, maxValue: null, minValue: 0, dataMode: 'long' } }, { columnKey: "BigIntVendaKpiInfo" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('scyKPIeGauges_dGrid', 'BigIntVendaKpiInfo', ui.oldValue, ui.value);}}, maxLength: 0 } }],
                          rowDeleting: function (evt, ui) {
                              deletedIndex = ui.element.context.rowIndex;
                              var entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowID);
                              if (entity) {
                                  vm.deleteEntity(entity);
                              }
                          },
                          rowDeleted: function (evt, ui) {
                              var grid = $('#scyKPIeGauges_dGrid');
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
                              var columns = $('#scyKPIeGauges_dGrid').igGridUpdating('option', 'columnSettings');
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
                                         vm.dataCombo.fillDataCombos(lookUpName, ui.columnKey, vm.currentDataItem(), function (result) {
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
                vm.OnDataGridCreated('scyKPIeGauges_dGrid');
            }
            var selectionrowselectionchanged = null, selectedRowId = -1;
            selectionrowselectionchanged = function (evt, ui) {
                if ((ui.owner.grid.selectedRow() && typeof ui.owner.grid.selectedRow().id !== 'undefined') || (ui.owner.grid.selectedRows() && ui.owner.grid.selectedRows().length > 0)) { 
                    if (isNullOrEmpty(ui.owner.selectedRows())|| ui.selectedRows.length <= 1) {
                        $(document).undelegate('#scyKPIeGauges_dGrid', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                        ui.owner.clearSelection();
                        ui.owner.selectRow(ui.row.index);
                        selectedRowId = ui.row.id;
                        $(document).delegate ('#scyKPIeGauges_dGrid', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                    }
                    selectGridCurrentItem(vm.goToKey, 'RowDataId', ui); 
                 } 
            };
            $(document).delegate('#scyKPIeGauges_dGrid', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
            $('#scyKPIeGauges_dGrid > tbody tr').live('focus', function(evt) {
                var grid = $('#scyKPIeGauges_dGrid'), row = $(this).closest('tr'), id = parseInt(row.attr('data-id'), 10);
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
            grid.delegate('.ui-iggrid-activerow', 'dblclick', function (e) {
                if (vm.KPIeGauges().status() === 'Q') vm.KPIeGauges().dataToolbar.viewInfo();
            });
        }
        vm.addDataSource({ key: 'scyKPIeGauges_dGrid', name: 'dataView', itemsSource: itemsSource });
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
