define(['managers/__auth'], function (managerAuth) {
var complementCtor = function() {
    var complement = {
    isAutomatic: true
    , renderscyTelaProduto_dGrid: function(vm) {
    var getDataSource = function() {
        var source = null;
        try {
            source = vm.dataView;
        }
        catch (e) { }
        return (isNullOrEmpty(source) ? ko.observableArray([]) : source);
    }
    $('#scyTelaProduto_dGrid_headers').live('focus  keydown', function (evt) {
        var keyCode = (window.event) ? evt.which : evt.keyCode;
        if (keyCode == 9) {
            var cols = $('#scyTelaProduto_dGrid').igGrid('option', 'columns');
            var firstRow = $('#scyTelaProduto_dGrid').data('igGrid').dataSource._dataView[0].RowDataId;
            clear = vm.status() === 'C';
            if (vm.status() == 'C')
                $('#scyTelaProduto_dGrid').igGridUpdating('startEdit', firstRow, 0);
            else {
                var entity = findElementByKey(getDataSource(), 'RowDataId', firstRow);
                var indexColumn = 0;
                cols.some(function (entry) {
                    if (entry.key != 'RowDataId' && !entry.hidden) {
                        if (verifyCanEditCol(entry.key, clear, entity)) {
                            $('#scyTelaProduto_dGrid').igGridUpdating('startEdit', firstRow, indexColumn);
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
       if($('#scyTelaProduto_dGrid').data('igGrid') == undefined) return '';
       var cols = $('#scyTelaProduto_dGrid').igGrid('option', 'columns');
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
    var itemsSource = { getVisibleColumns: getVisibleColumns, containerId: 'scyTelaProduto_dGrid_container', dataBind: function (commitData, force) {
       var grid = $('#scyTelaProduto_dGrid');
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
           $('#scyTelaProduto_dGrid_groupbyarea').addClass('hide');
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
            case 'Class': { canEditing = clear; break;}
            case 'Color': { canEditing = clear; break;}
            case 'DaysToManufacture': { canEditing = clear; break;}
            case 'ProductSubcategoryID': { canEditing = clear; break;}
            case 'UnitMeasureCode': { canEditing = clear; break;}
            case 'UnitMeasureCode1': { canEditing = clear; break;}
            case 'DiscontinuedDate': { canEditing = clear; break;}
            case 'FinishedGoodsFlag': { canEditing = clear; break;}
            case 'ProductModelID': { canEditing = clear; break;}
            case 'ListPrice': { canEditing = clear; break;}
            case 'MakeFlag': { canEditing = clear; break;}
            case 'ModifiedDate': { canEditing = clear; break;}
            case 'Name': { canEditing = clear; break;}
            case 'ProductID': { canEditing = clear; break;}
            case 'ProductLine': { canEditing = clear; break;}
            case 'ProductNumber': { canEditing = clear; break;}
            case 'ReorderPoint': { canEditing = clear; break;}
            case 'SafetyStockLevel': { canEditing = clear; break;}
            case 'SellEndDate': { canEditing = clear; break;}
            case 'SellStartDate': { canEditing = clear; break;}
            case 'Size': { canEditing = clear; break;}
            case 'StandardCost': { canEditing = clear; break;}
            case 'Style': { canEditing = clear; break;}
            case 'Weight': { canEditing = clear; break;}
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
                          showMultimidia(entity, e, table, key, vm.TelaProduto());
                     }
                 }
                 if (typeof vm.OnGridClientClick === 'function') {
                     var entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowKey);
                     vm.OnGridClientClick('scyTelaProduto_dGrid', ui.colKey, entity);
                 }
            },
            enableUTCDates: true,
            featureChooserIconDisplay: 'always',
            dataRendered: function(evt, ui) { 
            },
            columns: [
                { key: 'RowDataId', headerText: 'RowDataId', width: '50px', dataType: '', hidden: true },
                { key: 'Class', headerText: 'Class', width: '101px', dataType: 'string', format: '', hidden: false, unbound: false, group: null   },
                { key: 'Color', headerText: 'Color', width: '150px', dataType: 'string', format: '', hidden: false, unbound: false, group: null   },
                { key: 'DaysToManufacture', headerText: 'DaysToManufacture', width: '257px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'ProductSubcategoryID', headerText: 'ProductSubcategoryIDOWERTJERTBEWRGKBEWRKTGBEWKRBTGKEWRTBGKELWRG', width: '855px', dataType: 'string', format: '', hidden: false, unbound: false, group: null   },
                { key: 'UnitMeasureCode', headerText: 'UnitMeasureCodeLIJASKDFKHASDF', width: '413px', dataType: 'string', format: '', hidden: false, unbound: false, group: null   },
                { key: 'UnitMeasureCode1', headerText: 'UnitMeasureCode1', width: '244px', dataType: 'string', format: '', hidden: false, unbound: false, group: null   },
                { key: 'DiscontinuedDate', headerText: 'DiscontinuedDate', width: '244px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null   },
                { key: 'FinishedGoodsFlag', headerText: 'FinishedGoodsFlag', width: '257px', dataType: 'bool', format: 'checkbox', hidden: false, unbound: false, group: null   },
                { key: 'ProductModelID', headerText: 'ProductModelID', width: '218px', dataType: 'string', format: '', hidden: false, unbound: false, group: null   },
                { key: 'ListPrice', headerText: 'ListPrice', width: '200px', dataType: 'number', format: '0.0000', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'MakeFlag', headerText: 'MakeFlag', width: '140px', dataType: 'bool', format: 'checkbox', hidden: false, unbound: false, group: null   },
                { key: 'ModifiedDate', headerText: 'ModifiedDate', width: '192px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null   },
                { key: 'Name', headerText: 'Name', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null   },
                { key: 'ProductID', headerText: 'ProductID', width: '153px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'ProductLine', headerText: 'ProductLine', width: '179px', dataType: 'string', format: '', hidden: false, unbound: false, group: null   },
                { key: 'ProductNumber', headerText: 'ProductNumber', width: '250px', dataType: 'string', format: '', hidden: false, unbound: false, group: null   },
                { key: 'ReorderPoint', headerText: 'ReorderPoint', width: '192px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'SafetyStockLevel', headerText: 'SafetyStockLevel', width: '244px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'SellEndDate', headerText: 'SellEndDate', width: '179px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null   },
                { key: 'SellStartDate', headerText: 'SellStartDate', width: '205px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null   },
                { key: 'Size', headerText: 'Size', width: '88px', dataType: 'string', format: '', hidden: false, unbound: false, group: null   },
                { key: 'StandardCost', headerText: 'StandardCost', width: '200px', dataType: 'number', format: '0.0000', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'Style', headerText: 'Style', width: '101px', dataType: 'string', format: '', hidden: false, unbound: false, group: null   },
                { key: 'Weight', headerText: 'Weight', width: '114px', dataType: 'number', format: '0.00', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  }
            ],
            features: [
                        { name: 'Sorting', type: 'local', caseSensitive: true,
                          columnSorting: function (evt, ui) { 
                              $.grep(ui.owner.grid._visibleColumnsArray, function (e) { 
                                  if (e.key == ui.columnKey && e.dataType == 'string') 
                                      return $('#scyTelaProduto_dGrid').igGridSorting('option', 'caseSensitive', false); 
                                  else if (e.key == ui.columnKey) 
                                      return $('#scyTelaProduto_dGrid').igGridSorting('option', 'caseSensitive', true); 
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
                        { name: 'Tooltips', columnSettings:[{ columnKey: "Class", allowTooltips: true },{ columnKey: "Color", allowTooltips: true },{ columnKey: "DaysToManufacture", allowTooltips: true },{ columnKey: "ProductSubcategoryID", allowTooltips: false },{ columnKey: "UnitMeasureCode", allowTooltips: false },{ columnKey: "UnitMeasureCode1", allowTooltips: false },{ columnKey: "DiscontinuedDate", allowTooltips: true },{ columnKey: "FinishedGoodsFlag", allowTooltips: true },{ columnKey: "ProductModelID", allowTooltips: false },{ columnKey: "ListPrice", allowTooltips: true },{ columnKey: "MakeFlag", allowTooltips: true },{ columnKey: "ModifiedDate", allowTooltips: true },{ columnKey: "Name", allowTooltips: true },{ columnKey: "ProductID", allowTooltips: true },{ columnKey: "ProductLine", allowTooltips: true },{ columnKey: "ProductNumber", allowTooltips: true },{ columnKey: "ReorderPoint", allowTooltips: true },{ columnKey: "SafetyStockLevel", allowTooltips: true },{ columnKey: "SellEndDate", allowTooltips: true },{ columnKey: "SellStartDate", allowTooltips: true },{ columnKey: "Size", allowTooltips: true },{ columnKey: "StandardCost", allowTooltips: true },{ columnKey: "Style", allowTooltips: true },{ columnKey: "Weight", allowTooltips: true }] },
                        { name: 'Resizing' }, 
                        { name: 'Hiding', 
                            columnHidden: function (evt, ui) {
                               showMultimidiaLazy('#scyTelaProduto_dGrid');
                            },
                            columnShown: function (evt, ui) {
                               showMultimidiaLazy('#scyTelaProduto_dGrid');
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
                          columnSettings: [{ columnKey: "ProductSubcategoryID", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpProductSubcategory", isNullable: true, custom: vm.custom, vm: vm, verifyCanEditCol: verifyCanEditCol, allowMultiSelectionInSearch:true, validateOnClearState:false, maxValue:2147483647, maxLength: 12, defaultValue: null } }, { columnKey: "UnitMeasureCode", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpUnitMeasure", isNullable: true, custom: vm.custom, vm: vm, verifyCanEditCol: verifyCanEditCol, allowMultiSelectionInSearch:true, validateOnClearState:false, maxValue:null, maxLength: 3, defaultValue: '' } }, { columnKey: "UnitMeasureCode1", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpUnitMeasure1", isNullable: true, custom: vm.custom, vm: vm, verifyCanEditCol: verifyCanEditCol, allowMultiSelectionInSearch:true, validateOnClearState:false, maxValue:null, maxLength: 3, defaultValue: '' } }, { columnKey: "ProductModelID", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpProductModel", isNullable: true, custom: vm.custom, vm: vm, verifyCanEditCol: verifyCanEditCol, allowMultiSelectionInSearch:true, validateOnClearState:false, maxValue:2147483647, maxLength: 12, defaultValue: null } }, { columnKey: 'DiscontinuedDate', editorType: 'datepicker', editorOptions: {valueChanged: function(evt, ui){if (typeof vm.OnPropertyChangeDataGrid == 'function') {vm.OnPropertyChangeDataGrid('scyTelaProduto_dGrid', 'DiscontinuedDate', ui.oldValue, ui.value);}}, minValue: new Date(1900, 0, 1), datepickerOptions: { changeMonth: true, changeYear: true }  } }, { columnKey: 'ModifiedDate', editorType: 'datepicker', editorOptions: {valueChanged: function(evt, ui){if (typeof vm.OnPropertyChangeDataGrid == 'function') {vm.OnPropertyChangeDataGrid('scyTelaProduto_dGrid', 'ModifiedDate', ui.oldValue, ui.value);}}, minValue: new Date(1900, 0, 1), datepickerOptions: { changeMonth: true, changeYear: true }  } }, { columnKey: 'SellEndDate', editorType: 'datepicker', editorOptions: {valueChanged: function(evt, ui){if (typeof vm.OnPropertyChangeDataGrid == 'function') {vm.OnPropertyChangeDataGrid('scyTelaProduto_dGrid', 'SellEndDate', ui.oldValue, ui.value);}}, minValue: new Date(1900, 0, 1), datepickerOptions: { changeMonth: true, changeYear: true }  } }, { columnKey: 'SellStartDate', editorType: 'datepicker', editorOptions: {valueChanged: function(evt, ui){if (typeof vm.OnPropertyChangeDataGrid == 'function') {vm.OnPropertyChangeDataGrid('scyTelaProduto_dGrid', 'SellStartDate', ui.oldValue, ui.value);}}, minValue: new Date(1900, 0, 1), datepickerOptions: { changeMonth: true, changeYear: true }  } }, { columnKey: "Class" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid == 'function'){vm.OnPropertyChangeDataGrid('scyTelaProduto_dGrid', 'Class', ui.oldValue, ui.value);}}, maxLength: 2 } }, { columnKey: "Color" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid == 'function'){vm.OnPropertyChangeDataGrid('scyTelaProduto_dGrid', 'Color', ui.oldValue, ui.value);}}, maxLength: 15 } }, { columnKey: "DaysToManufacture" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid == 'function'){vm.OnPropertyChangeDataGrid('scyTelaProduto_dGrid', 'DaysToManufacture', ui.oldValue, ui.value);}},  maxLength: 12, maxValue: null, minValue: 0, dataMode: 'int' } }, { columnKey: "ListPrice" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid == 'function'){vm.OnPropertyChangeDataGrid('scyTelaProduto_dGrid', 'ListPrice', ui.oldValue, ui.value);}},  maxLength: 20, maxValue: 999999999999999.9999, minValue: 0, dataMode: 'decimal', minDecimals: 4, maxDecimals: 4 } }, { columnKey: "Name" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid == 'function'){vm.OnPropertyChangeDataGrid('scyTelaProduto_dGrid', 'Name', ui.oldValue, ui.value);}}, maxLength: 50 } }, { columnKey: "ProductID" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid == 'function'){vm.OnPropertyChangeDataGrid('scyTelaProduto_dGrid', 'ProductID', ui.oldValue, ui.value);}},  maxLength: 12, maxValue: null, minValue: 0, dataMode: 'int' } }, { columnKey: "ProductLine" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid == 'function'){vm.OnPropertyChangeDataGrid('scyTelaProduto_dGrid', 'ProductLine', ui.oldValue, ui.value);}}, maxLength: 2 } }, { columnKey: "ProductNumber" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid == 'function'){vm.OnPropertyChangeDataGrid('scyTelaProduto_dGrid', 'ProductNumber', ui.oldValue, ui.value);}}, maxLength: 25 } }, { columnKey: "ReorderPoint" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid == 'function'){vm.OnPropertyChangeDataGrid('scyTelaProduto_dGrid', 'ReorderPoint', ui.oldValue, ui.value);}},  maxLength: 6, maxValue: null, minValue: 0, dataMode: 'short' } }, { columnKey: "SafetyStockLevel" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid == 'function'){vm.OnPropertyChangeDataGrid('scyTelaProduto_dGrid', 'SafetyStockLevel', ui.oldValue, ui.value);}},  maxLength: 6, maxValue: null, minValue: 0, dataMode: 'short' } }, { columnKey: "Size" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid == 'function'){vm.OnPropertyChangeDataGrid('scyTelaProduto_dGrid', 'Size', ui.oldValue, ui.value);}}, maxLength: 5 } }, { columnKey: "StandardCost" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid == 'function'){vm.OnPropertyChangeDataGrid('scyTelaProduto_dGrid', 'StandardCost', ui.oldValue, ui.value);}},  maxLength: 20, maxValue: 999999999999999.9999, minValue: 0, dataMode: 'decimal', minDecimals: 4, maxDecimals: 4 } }, { columnKey: "Style" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid == 'function'){vm.OnPropertyChangeDataGrid('scyTelaProduto_dGrid', 'Style', ui.oldValue, ui.value);}}, maxLength: 2 } }, { columnKey: "Weight" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid == 'function'){vm.OnPropertyChangeDataGrid('scyTelaProduto_dGrid', 'Weight', ui.oldValue, ui.value);}},  maxLength: 9, maxValue: 999999.99, minValue: 0, dataMode: 'decimal', minDecimals: 2, maxDecimals: 2 } }],
                          rowDeleting: function (evt, ui) {
                              deletedIndex = ui.element.context.rowIndex;
                              var entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowID);
                              if (entity) {
                                  vm.deleteEntity(entity);
                              }
                          },
                          rowDeleted: function (evt, ui) {
                              var grid = $('#scyTelaProduto_dGrid');
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
                              var columns = $('#scyTelaProduto_dGrid').igGridUpdating('option', 'columnSettings');
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
                vm.OnDataGridCreated('scyTelaProduto_dGrid');
            }
            grid.delegate('.ui-iggrid-activerow', 'dblclick', function (e) {
                if (vm.TelaProduto().status() === 'Q') vm.TelaProduto().dataToolbar.viewInfo();
            });
        }
        vm.addDataSource({ key: 'scyTelaProduto_dGrid', name: 'dataView', itemsSource: itemsSource });
    }


    };
    return complement;
}

return complementCtor;
});
