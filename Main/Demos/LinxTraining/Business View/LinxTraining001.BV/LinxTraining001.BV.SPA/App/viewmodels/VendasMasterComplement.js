define(['managers/__auth'], function (managerAuth) {
var complementCtor = function() {
    var complement = {
    isAutomatic: true
    , renderVendasMaster_pChartPivotDrillDownChart_292335414f8f4609bb6587a1a58d8f8f: function(vm) {
    var transposeCheckBox = $("#transposeVendasMaster_pChartPivotDrillDownChart_292335414f8f4609bb6587a1a58d8f8f"),
    chart = $("#olapChartVendasMaster_pChartPivotDrillDownChart_292335414f8f4609bb6587a1a58d8f8f");
    var hasValue = function (value) {
            return value !== undefined && value !== null && value.count() > 0;
        },
    getCellData = function (rowIndex, columnIndex, columnCount, cells) {
        var cellOrdinal = (rowIndex * columnCount) + columnIndex;
        if (!hasValue(cells)) {
            return 0;
        }
        for (var index = 0; index < cells.count(); index++) {
            var cell = cells.item(index);
            if (cell.cellOrdinal() == cellOrdinal) {
               if (!isNaN(Number(cell.value()))) {
                   if (cell.value().indexOf('.') > -1) {
                       var cellFormat = cell.value().replace('.', '');
                       return new Number(parseInt(cellFormat));
                   }
                   else
                       return new Number(parseInt(cell.value()));
               }
               else if (cell.value().indexOf('.') > -1) {
                   var cellFormat = cell.value().replace('.', '');
                   return new Number(parseInt(cellFormat));
               } else
                return new Number(parseInt(cell.value()));
            }
        }
        return 0;
    },
    updateChart = function (tableView, transpose) {
    var columnHeaders,
        rowHeaders,
        cells = tableView.resultCells(),
        dataArray = [],
        series = [],
        rowHeaderIndex,
        columnHeaderIndex,
        ds,
        headerCell,
        columnCount,
        rowCount,
        data;
    
    if (transpose) {
        columnHeaders = tableView.rowHeaders(),
        rowHeaders = tableView.columnHeaders()
    }
    else {
        columnHeaders = tableView.columnHeaders(),
        rowHeaders = tableView.rowHeaders()
    }
    
    if (!hasValue(cells) && !hasValue(rowHeaders) && !hasValue(columnHeaders)) {
        var dataDefault = [{ 'caption': '', 'col0': 0 }];
        chart.igDataChart({
            height: '500px', width: '100%', dataSource: dataDefault, series: series,
            axes: [{ name: 'xAxis', type: 'categoryX', label: 'caption' },
            { name: 'yAxis', type: 'numericY' }],
            series: [{
                 name: 'series0', dataSource: dataDefault, title: 'caption', type: 'column', xAxis: 'xAxis', yAxis: 'yAxis', valueMemberPath: 'col0'
            }]
        });
        return;
    }
    else
       chart.igDataChart('destroy');
    
    if (!hasValue(rowHeaders)) {
        rowHeaders = [{ caption: function () { return ''; } }];
    }
    
    if (!hasValue(columnHeaders)) {
        columnHeaders = [{ caption: function () { return ''; } }];
    }
    
    for (rowHeaderIndex = 0; rowHeaderIndex < rowHeaders.count(); rowHeaderIndex++) {
        headerCell = rowHeaders.item(rowHeaderIndex);
        columnCount = columnHeaders.count();
        rowCount = rowHeaders.count();
        data = { caption: headerCell.caption() };
        var value;
        for (columnHeaderIndex = 0; columnHeaderIndex < columnCount; columnHeaderIndex++) {
            if (transpose) {
                value = getCellData(columnHeaderIndex, rowHeaderIndex, rowCount, cells, transpose)
            }
            else {
                value = getCellData(rowHeaderIndex, columnHeaderIndex, columnCount, cells, transpose)
            }
            data['col' + columnHeaderIndex] = value;
        }
    
        dataArray[rowHeaderIndex] = data;
    };
    
    for (columnHeaderIndex = 0; columnHeaderIndex < columnHeaders.count(); columnHeaderIndex++) {
        series[columnHeaderIndex] = {
            name: 'series' + columnHeaderIndex,
            title: columnHeaders.item(columnHeaderIndex).caption(),
            type: 'lineseries',
            xAxis: 'xAxis',
            yAxis: 'yAxis',
            showTooltip: true,
            valueMemberPath: 'col' + columnHeaderIndex
        };
    };
    
    ds = new $.ig.DataSource({ dataSource: dataArray });
    
    chart.igDataChart({
        height: '500px',
        width: '100%',
        dataSource: ds,
        series: series,
        legend: { element: 'olapChartLegendVendasMaster_pChartPivotDrillDownChart_292335414f8f4609bb6587a1a58d8f8f' },
        axes: [{
            name: 'xAxis',
            type: 'categoryX',
            label: 'caption'
        },
        {
            name: 'yAxis',
            type: 'numericY'
        }],
        horizontalZoomable: false,
        verticalZoomable: false,
        windowResponse: 'immediate'
    });
    };
    var bindingUpdate = function () {
        var pivotView = vm.dataShared['99'];
        if (!pivotView || pivotView.data().igPivotView == null) return;
        var pivotGrid = pivotView.igPivotView('pivotGrid');
        if (!pivotGrid) return;
        pivotGrid.element.igPivotGrid({
            pivotGridRendered: function () {
                updateChart(pivotGrid._tableView, transposeCheckBox.is(':checked')); }
        });
        transposeCheckBox.click(function () {
            updateChart(pivotGrid._tableView, transposeCheckBox.is(':checked'));
        });
        vm.dataSource.removeItem(itemsSource);
        itemsSource = null;
    };
    var itemsSource = { key: 'VendasMaster_pChartPivotDrillDownChart_292335414f8f4609bb6587a1a58d8f8f', name: 'dataView', itemsSource: { dataBind: bindingUpdate } };
    vm.addDataSource(itemsSource);
}


, renderscyVendasMaster_dGrid: function(vm) {
    var getDataSource = function() {
        var source = null;
        try {
            source = vm.dataView;
        }
        catch (e) { }
        return (isNullOrEmpty(source) ? ko.observableArray([]) : source);
    }
    $('#scyVendasMaster_dGrid_headers').live('focus  keydown', function (evt) {
        var keyCode = (window.event) ? evt.which : evt.keyCode;
        if (keyCode == 9) {
            var cols = $('#scyVendasMaster_dGrid').igGrid('option', 'columns');
            var firstRow = $('#scyVendasMaster_dGrid').data('igGrid').dataSource._dataView[0].RowDataId;
            clear = vm.status() === 'C';
            if (vm.status() == 'C')
                $('#scyVendasMaster_dGrid').igGridUpdating('startEdit', firstRow, 0);
            else {
                var entity = findElementByKey(getDataSource(), 'RowDataId', firstRow);
                var indexColumn = 0;
                cols.some(function (entry) {
                    if (entry.key != 'RowDataId' && !entry.hidden) {
                        if (verifyCanEditCol(entry.key, clear, entity)) {
                            $('#scyVendasMaster_dGrid').igGridUpdating('startEdit', firstRow, indexColumn);
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
       if($('#scyVendasMaster_dGrid').data('igGrid') == undefined) return '';
       var cols = $('#scyVendasMaster_dGrid').igGrid('option', 'columns');
       if (cols) {
         for (var idx = 0; idx < cols.length; idx++) {
             if (cols[idx].hidden != true) visibleColumns += (visibleColumns == '' ? '' : ',') + cols[idx].key;
         }
       }
       return visibleColumns;
    }
    var started = false;
    var currentRow = null;
    var updateEntity = function (columnKey, value, execDataBind) {
        if(value && Array.isArray(value) && value.length === 0) value = null;
        var entity = findElementByKey(getDataSource(), 'RowDataId', currentRow);
        if (entity != null && (typeof value) != 'undefined' && getAbsoluteValue(entity[columnKey]) != value) {
            setAbsoluteValue(entity, columnKey, value);
            if (execDataBind) itemsSource.dataBind(false);
        }
    };
    var itemsSource = { getVisibleColumns: getVisibleColumns, containerId: 'scyVendasMaster_dGrid_container', dataBind: function (commitData, force) {
       var grid = $('#scyVendasMaster_dGrid');
       if (started && (typeof grid.data('igGridUpdating') === 'undefined')) { started = false; }
       if (commitData && started) {
           if (grid.igGridUpdating('isEditing')) {
               grid.igGrid('commit');
           }
           return;
       }
       if ((!grid[0] || (!force && grid.parent().width() <= 0)) && !$('#dialog').is(':visible') ) return;
       if (!started) {
           createDataGrid(grid);
           started = true;
           commitData = false;
           $('#scyVendasMaster_dGrid_groupbyarea').addClass('hide');
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
         	    return e.name == 'Paging';
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
       }
    }};
    var valueGrouBy = -1;
    var deletedIndex = -1;
    function verifyCanEditCol(column, clear, entity){
        switch(column){
            case 'ValorTotal': { canEditing = clear; break;}
            case 'Data': { canEditing = clear; break;}
            case 'IDVendas': { canEditing = clear; break;}
            case 'IDClientes': { canEditing = clear; break;}
            case 'Nome': { canEditing = clear; break;}
            case 'Origem': { canEditing = clear; break;}
            case 'VendaVip': { canEditing = clear; break;}
        }
        return canEditing;
    };
    function createDataGrid(grid) {
        grid.igGrid({ height: (vm.isDependentVM() ? (getGridHeightSuggested() * 0.7) : $(window).height() * 0.85), width: '100%',
            dataSource: [],
            primaryKey: 'RowDataId',
            autoGenerateColumns: false,
            autofitLastColumn: false,
            dataSourceType: 'json',
            renderCheckboxes: true,
            autoCommit: true,
            cellClick: function(evt, ui) {
                 if (ui.cellElement && ui.cellElement.childNodes[0] && ui.cellElement.childNodes[0].childNodes[1]) {
                     var e = ui.cellElement.childNodes[0].childNodes[1];
                     if (e && e.tagName == 'IMG' && vm.status() != 'C')
                     {
                          var entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowKey);
                          var key = e.attributes['key'].value;
                          var table = e.attributes['tableName'].value;
                          showMultimidia(entity, e, table, key, vm.VendasMaster());
                     }
                 }
                 if (typeof vm.OnGridClientClick === 'function') {
                     var entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowKey);
                     vm.OnGridClientClick('scyVendasMaster_dGrid', ui.colKey, entity);
                 }
            },
            enableUTCDates: true,
            featureChooserIconDisplay: 'always',
            dataRendered: function(evt, ui) { 
            },
            columns: [
                { key: 'RowDataId', headerText: 'RowDataId', width: '50px', dataType: '', hidden: true },
                { key: 'ValorTotal', headerText: 'ValorTotal', width: '166px', dataType: 'number', format: '0.00', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'Data', headerText: 'Data', width: '120px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null   },
                { key: 'IDVendas', headerText: 'ID Vendas', width: '153px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'IDClientes', headerText: 'ID Clientes', width: '271px', dataType: 'string', format: '', hidden: false, unbound: false, group: null   },
                { key: 'Nome', headerText: 'Nome', width: '421px', dataType: 'string', format: '', hidden: false, unbound: false, group: null   },
                { key: 'Origem', headerText: 'Origem', width: '200px', dataType: 'string', format: '', hidden: false, unbound: false, group: null  , formatter: function (val, record) { return  vm.dataDomains.getName('LXOrigem', val);} },
                { key: 'VendaVip', headerText: 'VendaVip', width: '140px', dataType: 'bool', format: 'checkbox', hidden: false, unbound: false, group: null   }
            ],
            features: [
                        { name: 'Sorting', type: 'local', caseSensitive: true,
                          columnSorting: function (evt, ui) { 
                              $.grep(ui.owner.grid._visibleColumnsArray, function (e) { 
                                  if (e.key == ui.columnKey && e.dataType == 'string') 
                                      return $('#scyVendasMaster_dGrid').igGridSorting('option', 'caseSensitive', false); 
                                  else if (e.key == ui.columnKey) 
                                      return $('#scyVendasMaster_dGrid').igGridSorting('option', 'caseSensitive', true); 
                              }); 
                          } 
                          , columnSorted: function (event, args) { if (!isNullOrEmpty(args.columnKey) && !isNullOrEmpty(args.direction)) { vm.sortData(args.columnKey + ' ' + args.direction); } } },
                        { name: 'Filtering', mode: 'advanced', filterDropDownItemIcons: false, filterDropDownWidth: 200, allowFiltering: true, type: 'local', renderFC: false, renderFilterButton: true, 
                              dataFiltered: function (evt, ui) {
                              var columnsFilters = [];
                              $.each(ui.owner._currentAdvancedExpressions, function(i, item){ columnsFilters.push(item.fieldName); });
                              var cols = $('#' + ui.owner.grid.element[0].id + '_container  .ui-iggrid-headertable th');
                              cols.each(function (i, item) {
                                  var name = item.id.substr(ui.owner.grid.element[0].id.length + 1);
                                  var filter = $(item).find('span.ui-icon-search')
                                  if (columnsFilters.contains(name)) {
                                      if (!filter.hasClass('grid-column-researched'))
                                          filter.addClass('grid-column-researched');
                                  } else {
                                      if (filter.hasClass('grid-column-researched'))
                                          filter.removeClass('grid-column-researched')
                                  }
                              });
                            }
                        },
                        { name: 'Selection', mode: 'row',
                          rowSelectionChanged: function(evt, ui) {
                             if ((ui.owner.grid.selectedRow() && typeof ui.owner.grid.selectedRow().id !== 'undefined') || (ui.owner.grid.selectedRows() && ui.owner.grid.selectedRows().length > 0)) { 
                                 if (grid.igGridSelection('selectedRows') == null || ui.selectedRows.length <= 1) {
                                     grid.igGridSelection('clearSelection');
                                     grid.igGridSelection('selectRow', ui.row.index);
                                 }
                                 selectGridCurrentItem(vm.goToKey, 'RowDataId', ui); } 
                          },
                        },
                        { name: 'Tooltips', columnSettings:[{ columnKey: "ValorTotal", allowTooltips: true },{ columnKey: "Data", allowTooltips: true },{ columnKey: "IDVendas", allowTooltips: true },{ columnKey: "IDClientes", allowTooltips: false },{ columnKey: "Nome", allowTooltips: false },{ columnKey: "Origem", allowTooltips: true },{ columnKey: "VendaVip", allowTooltips: true }] },
                        { name: 'Resizing' }, 
                        { name: 'Hiding', 
                            columnHidden: function (evt, ui) {
                               showMultimidiaLazy('#scyVendasMaster_dGrid');
                            },
                            columnShown: function (evt, ui) {
                               showMultimidiaLazy('#scyVendasMaster_dGrid');
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
                          columnSettings: [{ columnKey: "IDClientes", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpClientes", isNullable: false, custom: vm.custom, vm: vm, verifyCanEditCol: verifyCanEditCol, allowMultiSelectionInSearch:true, validateOnClearState:false, maxValue:null, maxLength: 12, defaultValue: '' } }, { columnKey: "Nome", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpClientes", isNullable: false, custom: vm.custom, vm: vm, verifyCanEditCol: verifyCanEditCol, allowMultiSelectionInSearch:true, validateOnClearState:false, maxValue:null, maxLength: 40, defaultValue: '' } }, { columnKey: 'Data', editorType: 'datepicker', editorOptions: {valueChanged: function(evt, ui){if (typeof vm.OnPropertyChangeDataGrid == 'function') {vm.OnPropertyChangeDataGrid('scyVendasMaster_dGrid', 'Data', ui.oldValue, ui.value);}}, minValue: new Date(1900, 0, 1), datepickerOptions: { changeMonth: true, changeYear: true }  } }, { columnKey: 'Origem', editorType: 'combo', editorOptions: {   selectionChanged: function (evt, ui) {   var val = null;   if (ui.items != null && ui.items.length > 0) { val = ui.items[0].data['id']; }  updateEntity('Origem', val, false); },  mode: 'dropdown', dropDownOnFocus: true,  dataSource: vm.dataDomains.getItems('LXOrigem', ''),  textKey: 'name', valueKey: 'id', enableClearButton: false }}, { columnKey: "ValorTotal" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid == 'function'){vm.OnPropertyChangeDataGrid('scyVendasMaster_dGrid', 'ValorTotal', ui.oldValue, ui.value);}},  maxLength: 11, maxValue: 99999999.99, minValue: 0, dataMode: 'decimal', minDecimals: 2, maxDecimals: 2 } }, { columnKey: "IDVendas" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid == 'function'){vm.OnPropertyChangeDataGrid('scyVendasMaster_dGrid', 'IDVendas', ui.oldValue, ui.value);}},  maxLength: 12, maxValue: null, minValue: 0, dataMode: 'int' } }],
                          rowDeleting: function (evt, ui) {
                              deletedIndex = ui.element.context.rowIndex;
                              var entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowID);
                              if (entity) {
                                  vm.deleteEntity(entity);
                              }
                          },
                          rowDeleted: function (evt, ui) {
                              var grid = $('#scyVendasMaster_dGrid');
                              var rows = grid.igGrid('allRows');
                              if (rows.length > 0)
                              {
                                  if (deletedIndex < 0) deletedIndex = 0;
                                  else if (rows.length <= deletedIndex) deletedIndex = (rows.length-1);
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
                              if (!canEditing && vm.status() != 'C') {
                                  var isDesc = grid.igGridSorting('option', 'columnSettings').filter(function (el) {
                                      var desc = el.currentSortDirection;
                                      if (desc != undefined) return desc.indexOf('desc') > -1;
                                  });
                                  var canEditingOneField = false;
                                  var columnsVisible = ui.owner.grid._visibleColumnsArray;
                                  var rowId = ui.rowID, colId = ui.columnIndex;
                                  var colIndexVisible = 0;
                                  for (var i = 0; i < ui.owner.grid._visibleColumnsArray.length; i++) {
                                      var nameColumn = ui.owner.grid._visibleColumnsArray[i].key;
                                      canEditingOneField = (canEditingOneField == true ? canEditingOneField : verifyCanEditCol(nameColumn, clear, entity));
                                      if (nameColumn == ui.columnKey) colIndexVisible = i;
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
                                              if (indexColumn >= ui.owner.grid._visibleColumnsArray.length) {
                                                  (isDesc.length ? rowId-- : rowId++);
                                                  rowIndex++;
                                                  grid.igGridSelection('clearSelection');
                                                  indexColumn = 0;
                                              }
                                          }
                                      }
                                  }
                              }
                              return canEditing;
                          },
                          editCellStarted: function(evt, ui){
                              var lstRefreshDados = null;
                              var columns = $('#scyVendasMaster_dGrid').igGridUpdating('option', 'columnSettings');
                              var currentCol = null;
                              currentRow = ui.rowID;
                              columns.forEach(function (entry, index) {
                                 if (entry.columnKey == ui.columnKey) currentCol = entry;
                                 if (currentCol != null) return false;
                              });
                              if (currentCol !== null && currentCol.hasOwnProperty('editorType') && currentCol.editorType === 'combo') {
                                 var lookUpName = $(ui.editor).igCombo('option', 'inputName');
                                 if (lookUpName !== null) {
                                     lstRefreshDados = vm.dataCombo.getItems(lookUpName, '');
                                     if (lstRefreshDados.length == 0)
                                         vm.dataCombo.fillDataCombos(lookUpName, ui.columnKey, vm.currentDataItem(), function (result) {
                                             ui.owner.endEdit(false, false);
                                             setTimeout(function () { ui.owner.startEdit(ui.rowID, ui.columnKey, true); }, 100);
                                         });
                                     else {
                                         $(ui.editor).igCombo('option', 'dataSource', lstRefreshDados);
                                         $(ui.editor).one('igcombodatabound', function () { setTimeout(function () { $(ui.editor).igCombo('openDropDown'); }, 10); });
                                     }
                                 }
                              }
                          },
                          editCellEnded: function(evt, ui) {
                              currentRow = ui.rowID;
                              updateEntity(ui.columnKey, ui.value, !ui.update);
                              currentRow = null;
                          },
                        }
                    ]
            });
            if ((typeof vm.OnDataGridCreated == 'function')){
                vm.OnDataGridCreated('scyVendasMaster_dGrid');
            }
            grid.delegate('.ui-iggrid-activerow', 'dblclick', function (e) {
                if (vm.VendasMaster().status() === 'Q') vm.VendasMaster().dataToolbar.viewInfo();
            });
        }
        vm.addDataSource({ key: 'scyVendasMaster_dGrid', name: 'dataView', itemsSource: itemsSource });
    }


    };
    return complement;
}

return complementCtor;
});
