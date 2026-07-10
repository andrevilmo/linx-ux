define(['managers/__auth'], function (managerAuth) {
var complementCtor = function() {
    var complement = {
    isAutomatic: true
    , renderTelaNullBV_dGridTiposCamposFilhaView: function(vm) {
    var getDataSource = function() {
        var source = null;
        try {
            source = vm.currentDataItem().TiposCamposFilhaViewList;
        }
        catch (e) { }
        return (isNullOrEmpty(source) ? ko.observableArray([]) : source);
    }
    var dataSourceIsLoaded = function() {
        var isLoaded = false;
        try {
            isLoaded = (vm.currentDataItem().TiposCamposFilhaViewIsLoaded === true || vm.currentDataItem().TiposCamposFilhaViewList().length > 0);
        }
        catch (e) {
            isLoaded = true;
        }
        return isLoaded;
    }
    $('#TelaNullBV_dGridTiposCamposFilhaView_headers').live('focus  keydown', function (evt) {
        var keyCode = (window.event) ? evt.which : evt.keyCode;
        if (keyCode == 9) {
            var cols = $('#TelaNullBV_dGridTiposCamposFilhaView').igGrid('option', 'columns');
            var firstRow = $('#TelaNullBV_dGridTiposCamposFilhaView').data('igGrid').dataSource._dataView[0].RowDataId;
            clear = vm.status() === 'C';
            if (vm.status() == 'C')
                $('#TelaNullBV_dGridTiposCamposFilhaView').igGridUpdating('startEdit', firstRow, 0);
            else {
                var entity = findElementByKey(getDataSource(), 'RowDataId', firstRow);
                var indexColumn = 0;
                cols.some(function (entry) {
                    if (entry.key != 'RowDataId' && !entry.hidden) {
                        if (verifyCanEditCol(entry.key, clear, entity)) {
                            $('#TelaNullBV_dGridTiposCamposFilhaView').igGridUpdating('startEdit', firstRow, indexColumn);
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
       if($('#TelaNullBV_dGridTiposCamposFilhaView').data('igGrid') == undefined) return '';
       var cols = $('#TelaNullBV_dGridTiposCamposFilhaView').igGrid('option', 'columns');
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
    var itemsSource = { getVisibleColumns: getVisibleColumns, containerId: 'TelaNullBV_dGridTiposCamposFilhaView_container', dataBind: function (commitData, force) {
       var grid = $('#TelaNullBV_dGridTiposCamposFilhaView');
       if (started && (typeof grid.data('igGridUpdating') === 'undefined')) { started = false; }
       if (commitData && started) {
           if (grid.igGridUpdating('isEditing')) {
               grid.igGrid('commit');
           }
           return;
       }
       if ((!grid[0] || (!force && grid.parent().width() <= 0)) && !$('#dialogTiposCamposFilhaView').is(':visible') ) return;
       if (!started) {
           createDataGrid(grid);
           started = true;
           commitData = false;
           $('#TelaNullBV_dGridTiposCamposFilhaView_groupbyarea').addClass('hide');
       }
       if (grid.igGridUpdating('isEditing')) {
            grid.igGridUpdating('endEdit', true);
       }
       if ((vm.status() != 'C' && vm.status() != 'I') && !dataSourceIsLoaded()) {
         grid.igGrid("option", "dataSource", []);
         vm.currentDataItem().fillDetails(false, 'TiposCamposFilhaView');
         return;
       }
       grid.igGrid("option", "dataSource", unwrapObservableArray(getDataSource(), vm));
       var rows = grid.igGrid('allRows');
       if (rows.length > 0) {
         var verticalContainer = grid.igGrid('scrollContainer');
         var isSelected = false;
         if (vm.currentDataItem().currentTiposCamposFilhaView() != null)
         {
           for(var idx = 0; idx < rows.length; idx++)
           {
             if (rows[idx].dataset.id == getAbsoluteValue(vm.currentDataItem().currentTiposCamposFilhaView().RowDataId))
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
         if ($('#dialogTiposCamposFilhaView').is(':visible')) {
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
         		$('label#currentNumberTiposCamposFilhaView').html(current + ' - ' + totalCurrentPage);
         	}
         	else
         	    $('label#currentNumberTiposCamposFilhaView').html(1);
         	$('label#totalNumberTiposCamposFilhaView').html(totalGrid);
         }
       }
    }};
    var valueGrouBy = -1;
    var deletedIndex = -1;
    function verifyCanEditCol(column, clear, entity){
        switch(column){
            case 'Boolean': { canEditing = clear || vm.enabledForEditing(); break;}
            case 'Byte': { canEditing = clear || vm.enabledForEditing(); break;}
            case 'ByteArray': { canEditing = clear || vm.enabledForEditing(); break;}
            case 'DateTime': { canEditing = clear || vm.enabledForEditing(); break;}
            case 'DateTimeOffset': { canEditing = clear || vm.enabledForEditing(); break;}
            case 'Decimal': { canEditing = clear || vm.enabledForEditing(); break;}
            case 'IDTiposCampos': { canEditing = clear || vm.enabledForEditing(); break;}
            case 'IDTiposCamposFilha': { canEditing = clear || (entity && entity.isAdded()); break;}
            case 'Int': { canEditing = clear || vm.enabledForEditing(); break;}
            case 'Long': { canEditing = clear || vm.enabledForEditing(); break;}
            case 'Short': { canEditing = clear || vm.enabledForEditing(); break;}
            case 'String': { canEditing = clear || vm.enabledForEditing(); break;}
            case 'StringChar': { canEditing = clear || vm.enabledForEditing(); break;}
            case 'StringText': { canEditing = clear || vm.enabledForEditing(); break;}
            case 'Timestamp': { canEditing = clear || vm.enabledForEditing(); break;}
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
                          showMultimidia(entity, e, table, key, vm.TelaNullBV());
                     }
                 }
                 if (typeof vm.OnGridClientClick === 'function') {
                     var entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowKey);
                     vm.OnGridClientClick('TelaNullBV_dGridTiposCamposFilhaView', ui.colKey, entity);
                 }
            },
            enableUTCDates: true,
            featureChooserIconDisplay: 'always',
            dataRendered: function(evt, ui) { 
            },
            columns: [
                { key: 'RowDataId', headerText: 'RowDataId', width: '50px', dataType: 'number', hidden: true },
                { key: 'Boolean', headerText: 'Boolean', width: '127px', dataType: 'bool', format: 'checkbox', hidden: false, unbound: false, group: null   },
                { key: 'Byte', headerText: 'Byte', width: '88px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'ByteArray', headerText: 'ByteArray', width: '250px', dataType: 'string', format: '', hidden: false, unbound: false, group: null   },
                { key: 'DateTime', headerText: 'DateTime', width: '140px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null   },
                { key: 'DateTimeOffset', headerText: 'DateTimeOffset', width: '218px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null   },
                { key: 'Decimal', headerText: 'Decimal', width: '127px', dataType: 'number', format: '0.00', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'IDTiposCampos', headerText: 'ID TiposCampos', width: '218px', dataType: 'string', format: '', hidden: false, unbound: false, group: null   },
                { key: 'IDTiposCamposFilha', headerText: 'ID TiposCamposFilha', width: '283px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'Int', headerText: 'Int', width: '130px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'Long', headerText: 'Long', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'Short', headerText: 'Short', width: '101px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'String', headerText: 'String', width: '114px', dataType: 'string', format: '', hidden: false, unbound: false, group: null   },
                { key: 'StringChar', headerText: 'StringChar', width: '166px', dataType: 'string', format: '', hidden: false, unbound: false, group: null   },
                { key: 'StringText', headerText: 'StringText', width: '250px', dataType: 'string', format: '', hidden: false, unbound: false, group: null   },
                { key: 'Timestamp', headerText: 'Timestamp', width: '250px', dataType: 'string', format: '', hidden: false, unbound: false, group: null   }
            ],
            features: [
                        { name: 'Sorting', type: 'local', caseSensitive: true,
                          columnSorting: function (evt, ui) { 
                              $.grep(ui.owner.grid._visibleColumnsArray, function (e) { 
                                  if (e.key == ui.columnKey && e.dataType == 'string') 
                                      return $('#TelaNullBV_dGridTiposCamposFilhaView').igGridSorting('option', 'caseSensitive', false); 
                                  else if (e.key == ui.columnKey) 
                                      return $('#TelaNullBV_dGridTiposCamposFilhaView').igGridSorting('option', 'caseSensitive', true); 
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
                                 selectGridCurrentItem(vm.goToKey, 'RowDataId', ui, vm.currentDataItem().currentTiposCamposFilhaView, getDataSource()); } 
                          },
                        },
                        { name: 'Tooltips', columnSettings:[{ columnKey: "Boolean", allowTooltips: true },{ columnKey: "Byte", allowTooltips: true },{ columnKey: "ByteArray", allowTooltips: true },{ columnKey: "DateTime", allowTooltips: true },{ columnKey: "DateTimeOffset", allowTooltips: true },{ columnKey: "Decimal", allowTooltips: true },{ columnKey: "IDTiposCampos", allowTooltips: false },{ columnKey: "IDTiposCamposFilha", allowTooltips: true },{ columnKey: "Int", allowTooltips: true },{ columnKey: "Long", allowTooltips: true },{ columnKey: "Short", allowTooltips: true },{ columnKey: "String", allowTooltips: true },{ columnKey: "StringChar", allowTooltips: true },{ columnKey: "StringText", allowTooltips: true },{ columnKey: "Timestamp", allowTooltips: true }] },
                        { name: 'Resizing' }, 
                        { name: 'Hiding', 
                            columnHidden: function (evt, ui) {
                               showMultimidiaLazy('#TelaNullBV_dGridTiposCamposFilhaView');
                            },
                            columnShown: function (evt, ui) {
                               showMultimidiaLazy('#TelaNullBV_dGridTiposCamposFilhaView');
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
                          columnSettings: [{ columnKey: "IDTiposCampos", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "", isNullable: false, custom: vm.custom, vm: vm, verifyCanEditCol: verifyCanEditCol, allowMultiSelectionInSearch:true, validateOnClearState:false, maxValue:2147483647, maxLength: 12, defaultValue: 0 } }, { columnKey: 'DateTime', editorType: 'datepicker', editorOptions: {valueChanged: function(evt, ui){if (typeof vm.OnPropertyChangeDataGrid == 'function') {vm.OnPropertyChangeDataGrid('TelaNullBV_dGridTiposCamposFilhaView', 'DateTime', ui.oldValue, ui.value);}}, minValue: new Date(1900, 0, 1), datepickerOptions: { changeMonth: true, changeYear: true }  } }, { columnKey: 'DateTimeOffset', editorType: 'datepicker', editorOptions: {valueChanged: function(evt, ui){if (typeof vm.OnPropertyChangeDataGrid == 'function') {vm.OnPropertyChangeDataGrid('TelaNullBV_dGridTiposCamposFilhaView', 'DateTimeOffset', ui.oldValue, ui.value);}}, minValue: new Date(1900, 0, 1), datepickerOptions: { changeMonth: true, changeYear: true }  } }, { columnKey: "Byte" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid == 'function'){vm.OnPropertyChangeDataGrid('TelaNullBV_dGridTiposCamposFilhaView', 'Byte', ui.oldValue, ui.value);}},  maxLength: 3, maxValue: null, minValue: 0, dataMode: 'byte' } }, { columnKey: "ByteArray" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid == 'function'){vm.OnPropertyChangeDataGrid('TelaNullBV_dGridTiposCamposFilhaView', 'ByteArray', ui.oldValue, ui.value);}}, maxLength: 3 } }, { columnKey: "Decimal" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid == 'function'){vm.OnPropertyChangeDataGrid('TelaNullBV_dGridTiposCamposFilhaView', 'Decimal', ui.oldValue, ui.value);}},  maxLength: 11, maxValue: 99999999.99, minValue: 0, dataMode: 'decimal', minDecimals: 2, maxDecimals: 2 } }, { columnKey: "IDTiposCamposFilha" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid == 'function'){vm.OnPropertyChangeDataGrid('TelaNullBV_dGridTiposCamposFilhaView', 'IDTiposCamposFilha', ui.oldValue, ui.value);}},  maxLength: 12, maxValue: null, minValue: 0, dataMode: 'int' } }, { columnKey: "Int" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid == 'function'){vm.OnPropertyChangeDataGrid('TelaNullBV_dGridTiposCamposFilhaView', 'Int', ui.oldValue, ui.value);}},  maxLength: 12, maxValue: null, minValue: 0, dataMode: 'int' } }, { columnKey: "Long" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid == 'function'){vm.OnPropertyChangeDataGrid('TelaNullBV_dGridTiposCamposFilhaView', 'Long', ui.oldValue, ui.value);}},  maxLength: 24, maxValue: null, minValue: 0, dataMode: 'long' } }, { columnKey: "Short" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid == 'function'){vm.OnPropertyChangeDataGrid('TelaNullBV_dGridTiposCamposFilhaView', 'Short', ui.oldValue, ui.value);}},  maxLength: 6, maxValue: null, minValue: 0, dataMode: 'short' } }, { columnKey: "String" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid == 'function'){vm.OnPropertyChangeDataGrid('TelaNullBV_dGridTiposCamposFilhaView', 'String', ui.oldValue, ui.value);}}, maxLength: 10 } }, { columnKey: "StringChar" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid == 'function'){vm.OnPropertyChangeDataGrid('TelaNullBV_dGridTiposCamposFilhaView', 'StringChar', ui.oldValue, ui.value);}}, maxLength: 10 } }, { columnKey: "StringText" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid == 'function'){vm.OnPropertyChangeDataGrid('TelaNullBV_dGridTiposCamposFilhaView', 'StringText', ui.oldValue, ui.value);}}, maxLength: 0 } }, { columnKey: "Timestamp" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid == 'function'){vm.OnPropertyChangeDataGrid('TelaNullBV_dGridTiposCamposFilhaView', 'Timestamp', ui.oldValue, ui.value);}}, maxLength: 3 } }],
                          rowDeleting: function (evt, ui) {
                              deletedIndex = ui.element.context.rowIndex;
                              var entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowID);
                              if (entity) {
                                  vm.deleteEntity(entity);
                              }
                          },
                          rowDeleted: function (evt, ui) {
                              var grid = $('#TelaNullBV_dGridTiposCamposFilhaView');
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
                              var columns = $('#TelaNullBV_dGridTiposCamposFilhaView').igGridUpdating('option', 'columnSettings');
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
                                         vm.dataCombo.fillDataCombos(lookUpName, ui.columnKey, vm.currentDataItem().currentTiposCamposFilhaView(), function (result) {
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
                vm.OnDataGridCreated('TelaNullBV_dGridTiposCamposFilhaView');
            }
        }
        vm.addDataSource({ key: 'TelaNullBV_dGridTiposCamposFilhaView', name: 'TiposCamposFilhaViewList', itemsSource: itemsSource });
    }


, renderscyTelaNullBV_dGrid: function(vm) {
    var getDataSource = function() {
        var source = null;
        try {
            source = vm.dataView;
        }
        catch (e) { }
        return (isNullOrEmpty(source) ? ko.observableArray([]) : source);
    }
    $('#scyTelaNullBV_dGrid_headers').live('focus  keydown', function (evt) {
        var keyCode = (window.event) ? evt.which : evt.keyCode;
        if (keyCode == 9) {
            var cols = $('#scyTelaNullBV_dGrid').igGrid('option', 'columns');
            var firstRow = $('#scyTelaNullBV_dGrid').data('igGrid').dataSource._dataView[0].RowDataId;
            clear = vm.status() === 'C';
            if (vm.status() == 'C')
                $('#scyTelaNullBV_dGrid').igGridUpdating('startEdit', firstRow, 0);
            else {
                var entity = findElementByKey(getDataSource(), 'RowDataId', firstRow);
                var indexColumn = 0;
                cols.some(function (entry) {
                    if (entry.key != 'RowDataId' && !entry.hidden) {
                        if (verifyCanEditCol(entry.key, clear, entity)) {
                            $('#scyTelaNullBV_dGrid').igGridUpdating('startEdit', firstRow, indexColumn);
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
       if($('#scyTelaNullBV_dGrid').data('igGrid') == undefined) return '';
       var cols = $('#scyTelaNullBV_dGrid').igGrid('option', 'columns');
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
    var itemsSource = { getVisibleColumns: getVisibleColumns, containerId: 'scyTelaNullBV_dGrid_container', dataBind: function (commitData, force) {
       var grid = $('#scyTelaNullBV_dGrid');
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
           $('#scyTelaNullBV_dGrid_groupbyarea').addClass('hide');
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
            case 'Boolean': { canEditing = clear; break;}
            case 'Byte': { canEditing = clear; break;}
            case 'ByteArray': { canEditing = clear; break;}
            case 'DateTime': { canEditing = clear; break;}
            case 'DateTimeOffset': { canEditing = clear; break;}
            case 'Decimal': { canEditing = clear; break;}
            case 'IDTiposCampos': { canEditing = clear; break;}
            case 'Int': { canEditing = clear; break;}
            case 'Long': { canEditing = clear; break;}
            case 'Short': { canEditing = clear; break;}
            case 'String': { canEditing = clear; break;}
            case 'StringChar': { canEditing = clear; break;}
            case 'StringText': { canEditing = clear; break;}
            case 'Timestamp': { canEditing = clear; break;}
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
                          showMultimidia(entity, e, table, key, vm.TelaNullBV());
                     }
                 }
                 if (typeof vm.OnGridClientClick === 'function') {
                     var entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowKey);
                     vm.OnGridClientClick('scyTelaNullBV_dGrid', ui.colKey, entity);
                 }
            },
            enableUTCDates: true,
            featureChooserIconDisplay: 'always',
            dataRendered: function(evt, ui) { 
            },
            columns: [
                { key: 'RowDataId', headerText: 'RowDataId', width: '50px', dataType: '', hidden: true },
                { key: 'Boolean', headerText: 'Boolean', width: '127px', dataType: 'bool', format: 'checkbox', hidden: false, unbound: false, group: null   },
                { key: 'Byte', headerText: 'Byte', width: '88px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'ByteArray', headerText: 'ByteArray', width: '250px', dataType: 'string', format: '', hidden: false, unbound: false, group: null   },
                { key: 'DateTime', headerText: 'DateTime', width: '140px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null   },
                { key: 'DateTimeOffset', headerText: 'DateTimeOffset', width: '218px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null   },
                { key: 'Decimal', headerText: 'Decimal', width: '127px', dataType: 'number', format: '0.00', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'IDTiposCampos', headerText: 'ID TiposCampos', width: '218px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'Int', headerText: 'Int', width: '130px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'Long', headerText: 'Long', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'Short', headerText: 'Short', width: '101px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'String', headerText: 'String', width: '114px', dataType: 'string', format: '', hidden: false, unbound: false, group: null   },
                { key: 'StringChar', headerText: 'StringChar', width: '166px', dataType: 'string', format: '', hidden: false, unbound: false, group: null   },
                { key: 'StringText', headerText: 'StringText', width: '250px', dataType: 'string', format: '', hidden: false, unbound: false, group: null   },
                { key: 'Timestamp', headerText: 'Timestamp', width: '250px', dataType: 'string', format: '', hidden: false, unbound: false, group: null   }
            ],
            features: [
                        { name: 'Sorting', type: 'local', caseSensitive: true,
                          columnSorting: function (evt, ui) { 
                              $.grep(ui.owner.grid._visibleColumnsArray, function (e) { 
                                  if (e.key == ui.columnKey && e.dataType == 'string') 
                                      return $('#scyTelaNullBV_dGrid').igGridSorting('option', 'caseSensitive', false); 
                                  else if (e.key == ui.columnKey) 
                                      return $('#scyTelaNullBV_dGrid').igGridSorting('option', 'caseSensitive', true); 
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
                        { name: 'Tooltips', columnSettings:[{ columnKey: "Boolean", allowTooltips: true },{ columnKey: "Byte", allowTooltips: true },{ columnKey: "ByteArray", allowTooltips: true },{ columnKey: "DateTime", allowTooltips: true },{ columnKey: "DateTimeOffset", allowTooltips: true },{ columnKey: "Decimal", allowTooltips: true },{ columnKey: "IDTiposCampos", allowTooltips: true },{ columnKey: "Int", allowTooltips: true },{ columnKey: "Long", allowTooltips: true },{ columnKey: "Short", allowTooltips: true },{ columnKey: "String", allowTooltips: true },{ columnKey: "StringChar", allowTooltips: true },{ columnKey: "StringText", allowTooltips: true },{ columnKey: "Timestamp", allowTooltips: true }] },
                        { name: 'Resizing' }, 
                        { name: 'Hiding', 
                            columnHidden: function (evt, ui) {
                               showMultimidiaLazy('#scyTelaNullBV_dGrid');
                            },
                            columnShown: function (evt, ui) {
                               showMultimidiaLazy('#scyTelaNullBV_dGrid');
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
                          columnSettings: [{ columnKey: 'DateTime', editorType: 'datepicker', editorOptions: {valueChanged: function(evt, ui){if (typeof vm.OnPropertyChangeDataGrid == 'function') {vm.OnPropertyChangeDataGrid('scyTelaNullBV_dGrid', 'DateTime', ui.oldValue, ui.value);}}, minValue: new Date(1900, 0, 1), datepickerOptions: { changeMonth: true, changeYear: true }  } }, { columnKey: 'DateTimeOffset', editorType: 'datepicker', editorOptions: {valueChanged: function(evt, ui){if (typeof vm.OnPropertyChangeDataGrid == 'function') {vm.OnPropertyChangeDataGrid('scyTelaNullBV_dGrid', 'DateTimeOffset', ui.oldValue, ui.value);}}, minValue: new Date(1900, 0, 1), datepickerOptions: { changeMonth: true, changeYear: true }  } }, { columnKey: "Byte" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid == 'function'){vm.OnPropertyChangeDataGrid('scyTelaNullBV_dGrid', 'Byte', ui.oldValue, ui.value);}},  maxLength: 3, maxValue: null, minValue: 0, dataMode: 'byte' } }, { columnKey: "ByteArray" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid == 'function'){vm.OnPropertyChangeDataGrid('scyTelaNullBV_dGrid', 'ByteArray', ui.oldValue, ui.value);}}, maxLength: 3 } }, { columnKey: "Decimal" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid == 'function'){vm.OnPropertyChangeDataGrid('scyTelaNullBV_dGrid', 'Decimal', ui.oldValue, ui.value);}},  maxLength: 11, maxValue: 99999999.99, minValue: 0, dataMode: 'decimal', minDecimals: 2, maxDecimals: 2 } }, { columnKey: "IDTiposCampos" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid == 'function'){vm.OnPropertyChangeDataGrid('scyTelaNullBV_dGrid', 'IDTiposCampos', ui.oldValue, ui.value);}},  maxLength: 12, maxValue: null, minValue: 0, dataMode: 'int' } }, { columnKey: "Int" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid == 'function'){vm.OnPropertyChangeDataGrid('scyTelaNullBV_dGrid', 'Int', ui.oldValue, ui.value);}},  maxLength: 12, maxValue: null, minValue: 0, dataMode: 'int' } }, { columnKey: "Long" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid == 'function'){vm.OnPropertyChangeDataGrid('scyTelaNullBV_dGrid', 'Long', ui.oldValue, ui.value);}},  maxLength: 24, maxValue: null, minValue: 0, dataMode: 'long' } }, { columnKey: "Short" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid == 'function'){vm.OnPropertyChangeDataGrid('scyTelaNullBV_dGrid', 'Short', ui.oldValue, ui.value);}},  maxLength: 6, maxValue: null, minValue: 0, dataMode: 'short' } }, { columnKey: "String" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid == 'function'){vm.OnPropertyChangeDataGrid('scyTelaNullBV_dGrid', 'String', ui.oldValue, ui.value);}}, maxLength: 10 } }, { columnKey: "StringChar" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid == 'function'){vm.OnPropertyChangeDataGrid('scyTelaNullBV_dGrid', 'StringChar', ui.oldValue, ui.value);}}, maxLength: 10 } }, { columnKey: "StringText" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid == 'function'){vm.OnPropertyChangeDataGrid('scyTelaNullBV_dGrid', 'StringText', ui.oldValue, ui.value);}}, maxLength: 0 } }, { columnKey: "Timestamp" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid == 'function'){vm.OnPropertyChangeDataGrid('scyTelaNullBV_dGrid', 'Timestamp', ui.oldValue, ui.value);}}, maxLength: 3 } }],
                          rowDeleting: function (evt, ui) {
                              deletedIndex = ui.element.context.rowIndex;
                              var entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowID);
                              if (entity) {
                                  vm.deleteEntity(entity);
                              }
                          },
                          rowDeleted: function (evt, ui) {
                              var grid = $('#scyTelaNullBV_dGrid');
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
                              var columns = $('#scyTelaNullBV_dGrid').igGridUpdating('option', 'columnSettings');
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
                vm.OnDataGridCreated('scyTelaNullBV_dGrid');
            }
            grid.delegate('.ui-iggrid-activerow', 'dblclick', function (e) {
                if (vm.TelaNullBV().status() === 'Q') vm.TelaNullBV().dataToolbar.viewInfo();
            });
        }
        vm.addDataSource({ key: 'scyTelaNullBV_dGrid', name: 'dataView', itemsSource: itemsSource });
    }


    };
    return complement;
}

return complementCtor;
});
