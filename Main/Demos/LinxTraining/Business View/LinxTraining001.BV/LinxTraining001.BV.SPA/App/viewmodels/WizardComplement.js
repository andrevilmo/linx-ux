define(['managers/__auth'], function (managerAuth) {
var complementCtor = function() {
    var complement = {
    isAutomatic: true
    , renderWizard_dGridVendaDetalheView: function(vm) {
    var getDataSource = function() {
        var source = null;
        try {
            source = vm.currentDataItem().VendaDetalheViewList;
        }
        catch (e) { }
        return (isNullOrEmpty(source) ? ko.observableArray([]) : source);
    }
    var dataSourceIsLoaded = function() {
        var isLoaded = false;
        try {
            isLoaded = (vm.currentDataItem().VendaDetalheViewIsLoaded === true || vm.currentDataItem().VendaDetalheViewList().length > 0);
        }
        catch (e) {
            isLoaded = true;
        }
        return isLoaded;
    }
    $('#Wizard_dGridVendaDetalheView_headers').live('focus  keydown', function (evt) {
        var keyCode = (window.event) ? evt.which : evt.keyCode;
        if (keyCode == 9) {
            var cols = $('#Wizard_dGridVendaDetalheView').igGrid('option', 'columns');
            var firstRow = $('#Wizard_dGridVendaDetalheView').data('igGrid').dataSource._dataView[0].RowDataId;
            clear = vm.status() === 'C';
            if (vm.status() == 'C')
                $('#Wizard_dGridVendaDetalheView').igGridUpdating('startEdit', firstRow, 0);
            else {
                var entity = findElementByKey(getDataSource(), 'RowDataId', firstRow);
                var indexColumn = 0;
                cols.some(function (entry) {
                    if (entry.key != 'RowDataId' && !entry.hidden) {
                        if (verifyCanEditCol(entry.key, clear, entity)) {
                            $('#Wizard_dGridVendaDetalheView').igGridUpdating('startEdit', firstRow, indexColumn);
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
       if($('#Wizard_dGridVendaDetalheView').data('igGrid') == undefined) return '';
       var cols = $('#Wizard_dGridVendaDetalheView').igGrid('option', 'columns');
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
    var itemsSource = { getVisibleColumns: getVisibleColumns, containerId: 'Wizard_dGridVendaDetalheView_container', dataBind: function (commitData, force) {
       var grid = $('#Wizard_dGridVendaDetalheView');
       if (started && (typeof grid.data('igGridUpdating') === 'undefined')) { started = false; }
       if (commitData && started) {
           if (grid.igGridUpdating('isEditing')) {
               grid.igGrid('commit');
           }
           return;
       }
       if ((!grid[0] || (!force && grid.parent().width() <= 0)) && !$('#dialogVendaDetalheView').is(':visible') ) return;
       if (!started) {
           createDataGrid(grid);
           started = true;
           commitData = false;
           $('#Wizard_dGridVendaDetalheView_groupbyarea').addClass('hide');
       }
       if (grid.igGridUpdating('isEditing')) {
            grid.igGridUpdating('endEdit', true);
       }
       if ((vm.status() != 'C' && vm.status() != 'I') && !dataSourceIsLoaded()) {
         grid.igGrid("option", "dataSource", []);
         vm.currentDataItem().fillDetails(false, 'VendaDetalheView');
         return;
       }
       grid.igGrid("option", "dataSource", unwrapObservableArray(getDataSource(), vm));
       var rows = grid.igGrid('allRows');
       if (rows.length > 0) {
         var verticalContainer = grid.igGrid('scrollContainer');
         var isSelected = false;
         if (vm.currentDataItem().currentVendaDetalheView() != null)
         {
           for(var idx = 0; idx < rows.length; idx++)
           {
             if (rows[idx].dataset.id == getAbsoluteValue(vm.currentDataItem().currentVendaDetalheView().RowDataId))
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
         if ($('#dialogVendaDetalheView').is(':visible')) {
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
         		$('label#currentNumberVendaDetalheView').html(current + ' - ' + totalCurrentPage);
         	}
         	else
         	    $('label#currentNumberVendaDetalheView').html(1);
         	$('label#totalNumberVendaDetalheView').html(totalGrid);
         }
       }
    }};
    var valueGrouBy = -1;
    var deletedIndex = -1;
    function verifyCanEditCol(column, clear, entity){
        switch(column){
            case 'Quantidade': { canEditing = clear || vm.enabledForEditing(); break;}
            case 'Produto': { canEditing = clear || vm.enabledForEditing(); break;}
            case 'Preco': { canEditing = clear || vm.enabledForEditing(); break;}
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
                          showMultimidia(entity, e, table, key, vm.Wizard());
                     }
                 }
                 if (typeof vm.OnGridClientClick === 'function') {
                     var entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowKey);
                     vm.OnGridClientClick('Wizard_dGridVendaDetalheView', ui.colKey, entity);
                 }
            },
            enableUTCDates: true,
            featureChooserIconDisplay: 'always',
            dataRendered: function(evt, ui) { 
            },
            columns: [
                { key: 'RowDataId', headerText: 'RowDataId', width: '50px', dataType: 'number', hidden: true },
                { key: 'Quantidade', headerText: 'Quantidade', width: '166px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'Produto', headerText: 'Produto', width: '200px', dataType: 'string', format: '', hidden: false, unbound: false, group: null  , formatter: function (val, record) { return  vm.dataDomains.getName('ProdutoDomain', val);} },
                { key: 'Preco', headerText: 'Preço', width: '110px', dataType: 'number', format: '0.00', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  }
            ],
            features: [
                        { name: 'Sorting', type: 'local', caseSensitive: true,
                          columnSorting: function (evt, ui) { 
                              $.grep(ui.owner.grid._visibleColumnsArray, function (e) { 
                                  if (e.key == ui.columnKey && e.dataType == 'string') 
                                      return $('#Wizard_dGridVendaDetalheView').igGridSorting('option', 'caseSensitive', false); 
                                  else if (e.key == ui.columnKey) 
                                      return $('#Wizard_dGridVendaDetalheView').igGridSorting('option', 'caseSensitive', true); 
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
                                 selectGridCurrentItem(vm.goToKey, 'RowDataId', ui, vm.currentDataItem().currentVendaDetalheView, getDataSource()); } 
                          },
                        },
                        { name: 'Tooltips', columnSettings:[{ columnKey: "Quantidade", allowTooltips: true },{ columnKey: "Produto", allowTooltips: true },{ columnKey: "Preco", allowTooltips: true }] },
                        { name: 'Resizing' }, 
                        { name: 'Hiding', 
                            columnHidden: function (evt, ui) {
                               showMultimidiaLazy('#Wizard_dGridVendaDetalheView');
                            },
                            columnShown: function (evt, ui) {
                               showMultimidiaLazy('#Wizard_dGridVendaDetalheView');
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
                          columnSettings: [{ columnKey: 'Produto', editorType: 'combo', editorOptions: {   selectionChanged: function (evt, ui) {   var val = null;   if (ui.items != null && ui.items.length > 0) { val = ui.items[0].data['id']; }  updateEntity('Produto', val, false); },  mode: 'dropdown', dropDownOnFocus: true,  dataSource: vm.dataDomains.getItems('ProdutoDomain', ''),  textKey: 'name', valueKey: 'id', enableClearButton: true }}, { columnKey: "Quantidade" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid == 'function'){vm.OnPropertyChangeDataGrid('Wizard_dGridVendaDetalheView', 'Quantidade', ui.oldValue, ui.value);}},  maxLength: 12, maxValue: null, minValue: 0, dataMode: 'int' } }, { columnKey: "Preco" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid == 'function'){vm.OnPropertyChangeDataGrid('Wizard_dGridVendaDetalheView', 'Preco', ui.oldValue, ui.value);}},  maxLength: 11, maxValue: 99999999.99, minValue: 0, dataMode: 'decimal', minDecimals: 2, maxDecimals: 2 } }],
                          rowDeleting: function (evt, ui) {
                              deletedIndex = ui.element.context.rowIndex;
                              var entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowID);
                              if (entity) {
                                  vm.deleteEntity(entity);
                              }
                          },
                          rowDeleted: function (evt, ui) {
                              var grid = $('#Wizard_dGridVendaDetalheView');
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
                              var columns = $('#Wizard_dGridVendaDetalheView').igGridUpdating('option', 'columnSettings');
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
                                         vm.dataCombo.fillDataCombos(lookUpName, ui.columnKey, vm.currentDataItem().currentVendaDetalheView(), function (result) {
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
                vm.OnDataGridCreated('Wizard_dGridVendaDetalheView');
            }
        }
        vm.addDataSource({ key: 'Wizard_dGridVendaDetalheView', name: 'VendaDetalheViewList', itemsSource: itemsSource });
    }


    };
    return complement;
}

return complementCtor;
});
