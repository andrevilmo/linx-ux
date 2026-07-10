define(['managers/__auth'], function (managerAuth) {
var complementCtor = function() {
    var complement = {
    isAutomatic: true
    , renderTelaClienteVenda123456789012345678901234567890_dGridVendasView: function(vm) {
    var getDataSource = function() {
        var source = null;
        try {
            source = vm.currentDataItem().VendasViewList;
        }
        catch (e) { }
        return (isNullOrEmpty(source) ? ko.observableArray([]) : source);
    }
    var dataSourceIsLoaded = function() {
        var isLoaded = false;
        try {
            isLoaded = (vm.currentDataItem().VendasViewIsLoaded === true || vm.currentDataItem().VendasViewList().length > 0);
        }
        catch (e) {
            isLoaded = true;
        }
        return isLoaded;
    }
    $('#TelaClienteVenda123456789012345678901234567890_dGridVendasView_headers').live('focus  keydown', function (evt) {
        var keyCode = (window.event) ? evt.which : evt.keyCode;
        if (keyCode == 9) {
            var cols = $('#TelaClienteVenda123456789012345678901234567890_dGridVendasView').igGrid('option', 'columns');
            var firstRow = $('#TelaClienteVenda123456789012345678901234567890_dGridVendasView').data('igGrid').dataSource._dataView[0].RowDataId;
            clear = vm.status() === 'C';
            if (vm.status() == 'C')
                $('#TelaClienteVenda123456789012345678901234567890_dGridVendasView').igGridUpdating('startEdit', firstRow, 0);
            else {
                var entity = findElementByKey(getDataSource(), 'RowDataId', firstRow);
                var indexColumn = 0;
                cols.some(function (entry) {
                    if (entry.key != 'RowDataId' && !entry.hidden) {
                        if (verifyCanEditCol(entry.key, clear, entity)) {
                            $('#TelaClienteVenda123456789012345678901234567890_dGridVendasView').igGridUpdating('startEdit', firstRow, indexColumn);
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
       if($('#TelaClienteVenda123456789012345678901234567890_dGridVendasView').data('igGrid') == undefined) return '';
       var cols = $('#TelaClienteVenda123456789012345678901234567890_dGridVendasView').igGrid('option', 'columns');
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
    var itemsSource = { getVisibleColumns: getVisibleColumns, containerId: 'TelaClienteVenda123456789012345678901234567890_dGridVendasView_container', dataBind: function (commitData, force) {
       var grid = $('#TelaClienteVenda123456789012345678901234567890_dGridVendasView');
       if (started && (typeof grid.data('igGridUpdating') === 'undefined')) { started = false; }
       if (commitData && started) {
           if (grid.igGridUpdating('isEditing')) {
               grid.igGrid('commit');
           }
           return;
       }
       if ((!grid[0] || (!force && grid.parent().width() <= 0)) && !$('#dialogVendasView').is(':visible') ) return;
       if (!started) {
           createDataGrid(grid);
           started = true;
           commitData = false;
           $('#TelaClienteVenda123456789012345678901234567890_dGridVendasView_groupbyarea').addClass('hide');
       }
       if (grid.igGridUpdating('isEditing')) {
            grid.igGridUpdating('endEdit', true);
       }
       if ((vm.status() != 'C' && vm.status() != 'I') && !dataSourceIsLoaded()) {
         grid.igGrid("option", "dataSource", []);
         vm.currentDataItem().fillDetails(false, 'VendasView');
         return;
       }
       grid.igGrid("option", "dataSource", unwrapObservableArray(getDataSource(), vm));
       var rows = grid.igGrid('allRows');
       if (rows.length > 0) {
         var verticalContainer = grid.igGrid('scrollContainer');
         var isSelected = false;
         if (vm.currentDataItem().currentVendasView() != null)
         {
           for(var idx = 0; idx < rows.length; idx++)
           {
             if (rows[idx].dataset.id == getAbsoluteValue(vm.currentDataItem().currentVendasView().RowDataId))
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
         if ($('#dialogVendasView').is(':visible')) {
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
         		$('label#currentNumberVendasView').html(current + ' - ' + totalCurrentPage);
         	}
         	else
         	    $('label#currentNumberVendasView').html(1);
         	$('label#totalNumberVendasView').html(totalGrid);
         }
       }
    }};
    var valueGrouBy = -1;
    var deletedIndex = -1;
    function verifyCanEditCol(column, clear, entity){
        switch(column){
            case 'Data': { canEditing = clear || vm.enabledForEditing(); break;}
            case 'IDClientes': { canEditing = clear || vm.enabledForEditing(); break;}
            case 'IDVendas': { canEditing = clear || vm.enabledForEditing(); break;}
            case 'Origem': { canEditing = clear || vm.enabledForEditing(); break;}
            case 'ValorTotal': { canEditing = clear || vm.enabledForEditing(); break;}
            case 'VendaVip': { canEditing = clear || vm.enabledForEditing(); break;}
            case 'IDVendasMulti': { canEditing = false; break;}
        }
        return canEditing;
    };
    function createDataGrid(grid) {
        grid.igGrid({ height: (getGridHeightSuggested() * 0.7), width: '100%',
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
                          showMultimidia(entity, e, table, key, vm.TelaClienteVenda123456789012345678901234567890());
                     }
                 }
                 if (typeof vm.OnGridClientClick === 'function') {
                     var entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowKey);
                     vm.OnGridClientClick('TelaClienteVenda123456789012345678901234567890_dGridVendasView', ui.colKey, entity);
                 }
            },
            enableUTCDates: true,
            featureChooserIconDisplay: 'always',
            dataRendered: function(evt, ui) { 
               showMultimidiaLazy('#TelaClienteVenda123456789012345678901234567890_dGridVendasView');
            },
            columns: [
                { key: 'RowDataId', headerText: 'RowDataId', width: '50px', dataType: 'string', hidden: true },
                { key: 'Data', headerText: 'Data', width: '120px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null   },
                { key: 'IDClientes', headerText: 'ID Clientes', width: '250px', dataType: 'string', format: '', hidden: false, unbound: false, group: null   },
                { key: 'IDVendas', headerText: 'ID Vendas', width: '153px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'Origem', headerText: 'Origem', width: '200px', dataType: 'string', format: '', hidden: false, unbound: false, group: null  , formatter: function (val, record) { return  vm.dataDomains.getName('LXOrigem', val);} },
                { key: 'ValorTotal', headerText: 'ValorTotal', width: '166px', dataType: 'number', format: '0.00', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'VendaVip', headerText: 'VendaVip', width: '140px', dataType: 'bool', format: 'checkbox', hidden: false, unbound: false, group: null   },
                { key: 'IDVendasMulti', headerText: '', width: '85px', dataType: 'string', format: '', hidden: false, unbound: true, group: null, formula: function(data, grid) { var templateName = getTemplateImageName(vm.status(), 'grid'); var entity = findElementByKey(getDataSource(), 'RowDataId', data.RowDataId); var url = loadMultimidiaUrl('Vendas', entity.IDVendas, vm.TelaClienteVenda123456789012345678901234567890()); return "<div class='media-small'>" + ko.renderTemplateX(templateName, vm, { tableName: 'Vendas', key: getAbsoluteValue(entity.IDVendas), vm: vm.TelaClienteVenda123456789012345678901234567890(), url: url })+ "</div>";}}
            ],
            features: [
                        { name: 'Sorting', type: 'local', caseSensitive: true,
                          columnSorting: function (evt, ui) { 
                              $.grep(ui.owner.grid._visibleColumnsArray, function (e) { 
                                  if (e.key == ui.columnKey && e.dataType == 'string') 
                                      return $('#TelaClienteVenda123456789012345678901234567890_dGridVendasView').igGridSorting('option', 'caseSensitive', false); 
                                  else if (e.key == ui.columnKey) 
                                      return $('#TelaClienteVenda123456789012345678901234567890_dGridVendasView').igGridSorting('option', 'caseSensitive', true); 
                              }); 
                          } 
            },
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
                                 selectGridCurrentItem(vm.goToKey, 'RowDataId', ui, vm.currentDataItem().currentVendasView, getDataSource()); } 
                          },
                        },
                        { name: 'Tooltips', columnSettings:[{ columnKey: "Data", allowTooltips: true },{ columnKey: "IDClientes", allowTooltips: true },{ columnKey: "IDVendas", allowTooltips: true },{ columnKey: "Origem", allowTooltips: true },{ columnKey: "ValorTotal", allowTooltips: true },{ columnKey: "VendaVip", allowTooltips: true },{ columnKey: "IDVendas", allowTooltips: true }] },
                        { name: 'Resizing' }, 
                        { name: 'Hiding', 
                            columnHidden: function (evt, ui) {
                               showMultimidiaLazy('#TelaClienteVenda123456789012345678901234567890_dGridVendasView');
                            },
                            columnShown: function (evt, ui) {
                               showMultimidiaLazy('#TelaClienteVenda123456789012345678901234567890_dGridVendasView');
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
                          columnSettings: [{ columnKey: 'Data', editorType: 'datepicker', editorOptions: {valueChanged: function(evt, ui){if (typeof vm.OnPropertyChangeDataGrid == 'function') {vm.OnPropertyChangeDataGrid('TelaClienteVenda123456789012345678901234567890_dGridVendasView', 'Data', ui.oldValue, ui.value);}}, minValue: new Date(1900, 0, 1), datepickerOptions: { changeMonth: true, changeYear: true }  } }, { columnKey: 'Origem', editorType: 'combo', editorOptions: {   selectionChanged: function (evt, ui) {   var val = null;   if (ui.items != null && ui.items.length > 0) { val = ui.items[0].data['id']; }  updateEntity('Origem', val, false); },  mode: 'dropdown', dropDownOnFocus: true,  dataSource: vm.dataDomains.getItems('LXOrigem', ''),  textKey: 'name', valueKey: 'id', enableClearButton: true }}, { columnKey: "IDClientes" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid == 'function'){vm.OnPropertyChangeDataGrid('TelaClienteVenda123456789012345678901234567890_dGridVendasView', 'IDClientes', ui.oldValue, ui.value);}}, maxLength: 36 } }, { columnKey: "IDVendas" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid == 'function'){vm.OnPropertyChangeDataGrid('TelaClienteVenda123456789012345678901234567890_dGridVendasView', 'IDVendas', ui.oldValue, ui.value);}},  maxLength: 12, maxValue: null, minValue: 0, dataMode: 'int' } }, { columnKey: "ValorTotal" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid == 'function'){vm.OnPropertyChangeDataGrid('TelaClienteVenda123456789012345678901234567890_dGridVendasView', 'ValorTotal', ui.oldValue, ui.value);}},  maxLength: 11, maxValue: 99999999.99, minValue: 0, dataMode: 'decimal', minDecimals: 2, maxDecimals: 2 } }],
                          rowDeleting: function (evt, ui) {
                              deletedIndex = ui.element.context.rowIndex;
                              var entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowID);
                              if (entity) {
                                  vm.deleteEntity(entity);
                              }
                          },
                          rowDeleted: function (evt, ui) {
                              var grid = $('#TelaClienteVenda123456789012345678901234567890_dGridVendasView');
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
                              var columns = $('#TelaClienteVenda123456789012345678901234567890_dGridVendasView').igGridUpdating('option', 'columnSettings');
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
                                         vm.dataCombo.fillDataCombos(lookUpName, ui.columnKey, vm.currentDataItem().currentVendasView(), function (result) {
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
                vm.OnDataGridCreated('TelaClienteVenda123456789012345678901234567890_dGridVendasView');
            }
        }
        vm.addDataSource({ key: 'TelaClienteVenda123456789012345678901234567890_dGridVendasView', name: 'VendasViewList', itemsSource: itemsSource });
    }


, renderscyTelaClienteVenda123456789012345678901234567890_dGrid: function(vm) {
    var getDataSource = function() {
        var source = null;
        try {
            source = vm.dataView;
        }
        catch (e) { }
        return (isNullOrEmpty(source) ? ko.observableArray([]) : source);
    }
    $('#scyTelaClienteVenda123456789012345678901234567890_dGrid_headers').live('focus  keydown', function (evt) {
        var keyCode = (window.event) ? evt.which : evt.keyCode;
        if (keyCode == 9) {
            var cols = $('#scyTelaClienteVenda123456789012345678901234567890_dGrid').igGrid('option', 'columns');
            var firstRow = $('#scyTelaClienteVenda123456789012345678901234567890_dGrid').data('igGrid').dataSource._dataView[0].RowDataId;
            clear = vm.status() === 'C';
            if (vm.status() == 'C')
                $('#scyTelaClienteVenda123456789012345678901234567890_dGrid').igGridUpdating('startEdit', firstRow, 0);
            else {
                var entity = findElementByKey(getDataSource(), 'RowDataId', firstRow);
                var indexColumn = 0;
                cols.some(function (entry) {
                    if (entry.key != 'RowDataId' && !entry.hidden) {
                        if (verifyCanEditCol(entry.key, clear, entity)) {
                            $('#scyTelaClienteVenda123456789012345678901234567890_dGrid').igGridUpdating('startEdit', firstRow, indexColumn);
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
       if($('#scyTelaClienteVenda123456789012345678901234567890_dGrid').data('igGrid') == undefined) return '';
       var cols = $('#scyTelaClienteVenda123456789012345678901234567890_dGrid').igGrid('option', 'columns');
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
    var itemsSource = { getVisibleColumns: getVisibleColumns, containerId: 'scyTelaClienteVenda123456789012345678901234567890_dGrid_container', dataBind: function (commitData, force) {
       var grid = $('#scyTelaClienteVenda123456789012345678901234567890_dGrid');
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
           $('#scyTelaClienteVenda123456789012345678901234567890_dGrid_groupbyarea').addClass('hide');
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
            case 'IDClientes': { canEditing = clear; break;}
            case 'Nome': { canEditing = clear; break;}
            case 'Tipo': { canEditing = clear; break;}
            case 'IDClientesMulti': { canEditing = false; break;}
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
                          showMultimidia(entity, e, table, key, vm.TelaClienteVenda123456789012345678901234567890());
                     }
                 }
                 if (typeof vm.OnGridClientClick === 'function') {
                     var entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowKey);
                     vm.OnGridClientClick('scyTelaClienteVenda123456789012345678901234567890_dGrid', ui.colKey, entity);
                 }
            },
            enableUTCDates: true,
            featureChooserIconDisplay: 'always',
            dataRendered: function(evt, ui) { 
               showMultimidiaLazy('#scyTelaClienteVenda123456789012345678901234567890_dGrid');
            },
            columns: [
                { key: 'RowDataId', headerText: 'RowDataId', width: '50px', dataType: '', hidden: true },
                { key: 'IDClientes', headerText: 'ID Clientes', width: '250px', dataType: 'string', format: '', hidden: false, unbound: false, group: null   },
                { key: 'Nome', headerText: 'Nome', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null   },
                { key: 'Tipo', headerText: 'Tipo', width: '200px', dataType: 'string', format: '', hidden: false, unbound: false, group: null  , formatter: function (val, record) { return  vm.dataDomains.getName('LXTipoClientes', val);} },
                { key: 'IDClientesMulti', headerText: '', width: '85px', dataType: 'string', format: '', hidden: false, unbound: true, group: null, formula: function(data, grid) { var templateName = getTemplateImageName(vm.status(), 'grid'); var entity = findElementByKey(getDataSource(), 'RowDataId', data.RowDataId); var url = loadMultimidiaUrl('Clientes', entity.IDClientes, vm.TelaClienteVenda123456789012345678901234567890()); return "<div class='media-small'>" + ko.renderTemplateX(templateName, vm, { tableName: 'Clientes', key: getAbsoluteValue(entity.IDClientes), vm: vm.TelaClienteVenda123456789012345678901234567890(), url: url })+ "</div>";}}
            ],
            features: [
                        { name: 'Sorting', type: 'local', caseSensitive: true,
                          columnSorting: function (evt, ui) { 
                              $.grep(ui.owner.grid._visibleColumnsArray, function (e) { 
                                  if (e.key == ui.columnKey && e.dataType == 'string') 
                                      return $('#scyTelaClienteVenda123456789012345678901234567890_dGrid').igGridSorting('option', 'caseSensitive', false); 
                                  else if (e.key == ui.columnKey) 
                                      return $('#scyTelaClienteVenda123456789012345678901234567890_dGrid').igGridSorting('option', 'caseSensitive', true); 
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
                        { name: 'Tooltips', columnSettings:[{ columnKey: "IDClientes", allowTooltips: true },{ columnKey: "Nome", allowTooltips: true },{ columnKey: "Tipo", allowTooltips: true },{ columnKey: "IDClientes", allowTooltips: true }] },
                        { name: 'Resizing' }, 
                        { name: 'Hiding', 
                            columnHidden: function (evt, ui) {
                               showMultimidiaLazy('#scyTelaClienteVenda123456789012345678901234567890_dGrid');
                            },
                            columnShown: function (evt, ui) {
                               showMultimidiaLazy('#scyTelaClienteVenda123456789012345678901234567890_dGrid');
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
                          columnSettings: [{ columnKey: 'Tipo', editorType: 'combo', editorOptions: {   selectionChanged: function (evt, ui) {   var val = null;   if (ui.items != null && ui.items.length > 0) { val = ui.items[0].data['id']; }  updateEntity('Tipo', val, false); },  mode: 'dropdown', dropDownOnFocus: true,  dataSource: vm.dataDomains.getItems('LXTipoClientes', ''),  textKey: 'name', valueKey: 'id', enableClearButton: true }}, { columnKey: "IDClientes" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid == 'function'){vm.OnPropertyChangeDataGrid('scyTelaClienteVenda123456789012345678901234567890_dGrid', 'IDClientes', ui.oldValue, ui.value);}}, maxLength: 36 } }, { columnKey: "Nome" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid == 'function'){vm.OnPropertyChangeDataGrid('scyTelaClienteVenda123456789012345678901234567890_dGrid', 'Nome', ui.oldValue, ui.value);}}, maxLength: 40 } }],
                          rowDeleting: function (evt, ui) {
                              deletedIndex = ui.element.context.rowIndex;
                              var entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowID);
                              if (entity) {
                                  vm.deleteEntity(entity);
                              }
                          },
                          rowDeleted: function (evt, ui) {
                              var grid = $('#scyTelaClienteVenda123456789012345678901234567890_dGrid');
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
                              var columns = $('#scyTelaClienteVenda123456789012345678901234567890_dGrid').igGridUpdating('option', 'columnSettings');
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
                vm.OnDataGridCreated('scyTelaClienteVenda123456789012345678901234567890_dGrid');
            }
            grid.delegate('.ui-iggrid-activerow', 'dblclick', function (e) {
                if (vm.TelaClienteVenda123456789012345678901234567890().status() === 'Q') vm.TelaClienteVenda123456789012345678901234567890().dataToolbar.viewInfo();
            });
        }
        vm.addDataSource({ key: 'scyTelaClienteVenda123456789012345678901234567890_dGrid', name: 'dataView', itemsSource: itemsSource });
    }


    };
    return complement;
}

return complementCtor;
});
