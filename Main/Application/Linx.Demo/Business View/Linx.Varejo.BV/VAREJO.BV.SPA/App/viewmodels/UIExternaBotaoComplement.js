define(['managers/__auth', 'managers/user'], function (managerAuth, managerUser) {
var complementCtor = function() {
    var complement = {
    isAutomatic: true
    , vm: null
, selectedCollection: { }
, currentPage: 0
, selectedItems: function(firstIfNoItem) {
    var result = [];
    complement.saveSelection();
    for (var propName in complement.selectedCollection)
    {
        result = result.concat(complement.selectedCollection[propName]);
    }
    if (result.length == 0 && firstIfNoItem)
        result = complement.selectedCurrentItems(true);
    return result;
}
, saveSelection: function() {
    if (complement.vm.status() === 'C') { complement.currentPage = 0; complement.selectedCollection = {}; return; }
    var pageProp = 'Page' + complement.currentPage.toString();
    complement.selectedCollection[pageProp] = complement.selectedCurrentItems();
    complement.currentPage = complement.vm.dataToolbar.currentPage();
}
, selectedCurrentItems: function (firstIfNoItem, isSavingData) {
      var grid = $('#UIExternaBotao_dGridLoja');
      var selectedItems = [];
      var ds = grid.data().igGrid.dataSource.dataView();
      var rows = grid.igGridSelection("selectedRows");
      var dataList = this.vm.dataView;
      if (rows && rows.length == 0 && firstIfNoItem) {
          var entity = (isSavingData ? findElementByKey(dataList, 'RowDataId', ds[0].RowDataId) : ds[0]);
          if (entity) selectedItems.push(entity);
      }
      else if (rows && rows.length > 0) {
          $.each(rows, function (index, value) {
              var entity = (isSavingData ? findElementByKey(dataList, 'RowDataId', ds[value.index].RowDataId) : ds[value.index]);
              if (entity) selectedItems.push(entity);
          });
      }
      return selectedItems;
}
, clearSelectedItems: function () {
      var grid = $('#UIExternaBotao_dGridLoja');
      grid.igGridSelection('clearSelection');
}
, renderUIExternaBotao_dGridLoja: function(vm) {
    var self = this;
    self.vm = vm;
    if (!vm.hasMainTopDataGrid()) vm.hasMainTopDataGrid(true);
    var getDataSource = function() {
        var source = null;
        try {
            source = vm.dataView;
        }
        catch (e) { console.log(e); }
        return isNullOrEmpty(source) ? ko.observableArray([]) : source;
    };
    $('#UIExternaBotao_dGridLoja_headers').live('focus  keydown', function (evt) {
        var keyCode = window.event ? evt.which : evt.keyCode;
        if (keyCode === 9) {
            var cols = $('#UIExternaBotao_dGridLoja').igGrid('option', 'columns');
            var dataView = $('#UIExternaBotao_dGridLoja').data('igGrid').dataSource._dataView
            if (dataView.length === 0) return;
            var firstRow = dataView[0].RowDataId;
            clear = vm.status() === 'C';
            if (vm.status() === 'C')
                $('#UIExternaBotao_dGridLoja').igGridUpdating('startEdit', firstRow, 0);
            else {
                var entity = findElementByKey(getDataSource(), 'RowDataId', firstRow);
                var indexColumn = 0;
                cols.some(function (entry) {
                    if (entry.key !== 'RowDataId' && !entry.hidden) {
                        if (verifyCanEditCol(entry.key, clear, entity)) {
                            $('#UIExternaBotao_dGridLoja').igGridUpdating('startEdit', firstRow, indexColumn);
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
       if($('#UIExternaBotao_dGridLoja').data('igGrid') === undefined) return '';
       var cols = $('#UIExternaBotao_dGridLoja').igGrid('option', 'columns');
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
      if (!grid) grid = $('#UIExternaBotao_dGridLoja');
      return ((!grid[0] || (!forceCreating && grid.parent().width() <= 0)) && !$('#dialogLoja').is(':visible'));
    }
    var refreshData = true;
    var itemsSource = { isElementHided: isElementHided, getVisibleColumns: getVisibleColumns, containerId: 'UIExternaBotao_dGridLoja_container', dataBind: function (commitData, forceCreating) {
       var grid = $('#UIExternaBotao_dGridLoja');
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
           $('#UIExternaBotao_dGridLoja_groupbyarea').addClass('hide');
       }
       if (grid.igGridUpdating('isEditing')) {
            grid.igGridUpdating('endEdit', true);
       }
       var selectedRows = complement.selectedItems();
       grid.igGridSelection('clearSelection');
       grid.data('igGridSorting')._shouldFireColumnSorted = false;
       grid.igGrid("option", "dataSource", unwrapObservableArray(getDataSource(), vm));
       grid.data('igGridSorting')._shouldFireColumnSorted = true;
       var rows = grid.igGrid('allRows');
       if (rows.length > 0) {
         if (selectedRows.length > 0) {
             var dataView = grid.data().igGrid.dataSource.dataView();
             if (dataView.length > 0) {
                 $.each(selectedRows, function (index, item) {
                    var idxFound = findIndexByKey(dataView, 'RowDataId', getAbsoluteValue(item['RowDataId']))
                    if (idxFound < 0) idxFound = findIndexByKey(dataView, 'IdLoja', getAbsoluteValue(item['IdLoja']))
                    if (idxFound >= 0) grid.igGridSelection("selectRow", idxFound);
                 });
             }
         }
         if ($('#dialogLoja').is(':visible')) {
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
               $('label#currentNumberLoja').html(current + ' - ' + totalCurrentPage);
            }
            else
               $('label#currentNumberLoja').html(1);
            $('label#totalNumberLoja').html(totalGrid);
        }
       } else {
           $('label#currentNumberLoja').html(0);
           $('label#totalNumberLoja').html(0);
       }
    }};
    var valueGrouBy = -1;
    var deletedIndex = -1;
    function verifyCanEditCol(column, clear, entity){
        switch(column){
            case 'BigIntLoja': { canEditing = clear || vm.enabledForEditing(); break;}
            case 'BitLoja': { canEditing = clear || vm.enabledForEditing(); break;}
            case 'ComboboxLoja': { canEditing = clear || vm.enabledForEditing(); break;}
            case 'DatetimeLoja': { canEditing = clear || vm.enabledForEditing(); break;}
            case 'DecimalLoja': { canEditing = clear || vm.enabledForEditing(); break;}
            case 'IdLoja': { canEditing = clear || (entity && entity.isAdded()); break;}
            case 'IntLoja': { canEditing = clear || vm.enabledForEditing(); break;}
            case 'SmallIntLoja': { canEditing = clear || vm.enabledForEditing(); break;}
            case 'StringLoja': { canEditing = clear || vm.enabledForEditing(); break;}
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
                $('#UIExternaBotao_dGridLoja_LayoutBtn').igPopover('hide');
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
                          showMultimidia(entity, e, table, key, vm.UIExternaBotao());
                     }
                 }
                 if (typeof vm.OnGridClientClick === 'function') {
                     entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowKey);
                     vm.OnGridClientClick('UIExternaBotao_dGridLoja', ui.colKey, entity);
                 }
                 var grid = $('#UIExternaBotao_dGridLoja');
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
                { key: 'BigIntLoja', headerText: 'Big Int Loja', width: '250px', dataType: 'number', columnCssClass: 'ellipsis', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'BitLoja', headerText: 'Bit Loja', width: '140px', dataType: 'bool', columnCssClass: 'ellipsis', format: 'checkbox', hidden: false, unbound: false, group: null   },
                { key: 'ComboboxLoja', headerText: 'Combobox Loja', width: '205px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null  , formatter: function (val, record) { return  vm.dataDomains.getName('DomainComZeroPai', val);} },
                { key: 'DatetimeLoja', headerText: 'Datetime Loja', width: '205px', dataType: 'date', columnCssClass: 'ellipsis', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null   },
                { key: 'DecimalLoja', headerText: 'Decimal Loja', width: '192px', dataType: 'number', columnCssClass: 'ellipsis', format: '0.00', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'IdLoja', headerText: 'Id Loja', width: '130px', dataType: 'number', columnCssClass: 'ellipsis', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'IntLoja', headerText: 'Int Loja', width: '140px', dataType: 'number', columnCssClass: 'ellipsis', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'SmallIntLoja', headerText: 'Small Int Loja', width: '218px', dataType: 'number', columnCssClass: 'ellipsis', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'StringLoja', headerText: 'String Loja', width: '400px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   }
            ],
            features: [
                        { name: 'Sorting', type: 'local', caseSensitive: true,
                          columnSorting: function (evt, ui) { 
                              $.grep(ui.owner.grid._visibleColumnsArray, function (e) { 
                                  if (e.key === ui.columnKey && e.dataType === 'string') 
                                      return $('#UIExternaBotao_dGridLoja').igGridSorting('option', 'caseSensitive', false); 
                                  else if (e.key === ui.columnKey) 
                                      return $('#UIExternaBotao_dGridLoja').igGridSorting('option', 'caseSensitive', true); 
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
                        { name: 'Selection', mode: 'row', multipleSelection: vm.allowMultiSelectionInSearch()},
                        { name: 'RowSelectors', enableCheckBoxes: vm.allowMultiSelectionInSearch(), enableRowNumbering: false, checkBoxStateChanged: function(evt, ui){ 
                           if ((typeof vm.OnDataGridRowChecked === 'function')){
                               vm.OnDataGridRowChecked('UIExternaBotao_dGridLoja', self.selectedItems());
                           }
                           var selectedRows = grid.igGridSelection('selectedRows');
                           var selectedRow = ui.owner.grid.selectedRow();
                           var dataViewLength = ui.grid.dataSource.dataView().length;
                           if ((selectedRows.length == dataViewLength) || (selectedRow == null && selectedRows.length > 0)){
                               rowId = [];
                               rowId['id'] = 1;
                               selectGridCurrentItem(vm.goToKey, 'RowDataId', rowId);
                           } else if(ui.owner.grid.selectedRow() != null)
                               selectGridCurrentItem(vm.goToKey, 'RowDataId', ui);
                            }
                        },
                        { name: 'Tooltips', columnSettings:[{ columnKey: "BigIntLoja", allowTooltips: true },{ columnKey: "BitLoja", allowTooltips: true },{ columnKey: "ComboboxLoja", allowTooltips: true },{ columnKey: "DatetimeLoja", allowTooltips: true },{ columnKey: "DecimalLoja", allowTooltips: true },{ columnKey: "IdLoja", allowTooltips: true },{ columnKey: "IntLoja", allowTooltips: true },{ columnKey: "SmallIntLoja", allowTooltips: true },{ columnKey: "StringLoja", allowTooltips: true }] },
                        { name: 'Resizing' }, 
                        { name: 'Hiding', 
                            columnHidden: function (evt, ui) {
                               showMultimidiaLazy('#UIExternaBotao_dGridLoja');
                            },
                            columnShown: function (evt, ui) {
                               showMultimidiaLazy('#UIExternaBotao_dGridLoja');
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
                          columnSettings: [{ columnKey: 'DatetimeLoja', editorType: 'datepicker', editorOptions: {valueChanged: function(evt, ui){if (typeof vm.OnPropertyChangeDataGrid === 'function') {vm.OnPropertyChangeDataGrid('UIExternaBotao_dGridLoja', 'DatetimeLoja', ui.oldValue, ui.value);}}, minValue: new Date(1900, 0, 1), datepickerOptions: { changeMonth: true, changeYear: true }  } }, { columnKey: 'ComboboxLoja', editorType: 'combo', editorOptions: {   selectionChanged: function (evt, ui) {   var val = null;   if (ui.items != null && ui.items.length > 0) { val = ui.items[0].data['id']; }  updateEntity('ComboboxLoja', val, false); },  mode: 'dropdown', dropDownOnFocus: true,  dataSource: vm.dataDomains.getItems('DomainComZeroPai', ''),  textKey: 'name', valueKey: 'id', enableClearButton: false }}, { columnKey: "BigIntLoja" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('UIExternaBotao_dGridLoja', 'BigIntLoja', ui.oldValue, ui.value);}},  maxLength: 24, maxValue: null, minValue: 0, dataMode: 'long' } }, { columnKey: "DecimalLoja" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('UIExternaBotao_dGridLoja', 'DecimalLoja', ui.oldValue, ui.value);}},  maxLength: 14, maxValue: 99999999999.99, minValue: 0, dataMode: 'decimal', minDecimals: 2, maxDecimals: 2 } }, { columnKey: "IdLoja" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('UIExternaBotao_dGridLoja', 'IdLoja', ui.oldValue, ui.value);}},  maxLength: 12, maxValue: null, dataMode: 'int' } }, { columnKey: "IntLoja" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('UIExternaBotao_dGridLoja', 'IntLoja', ui.oldValue, ui.value);}},  maxLength: 12, maxValue: null, minValue: 0, dataMode: 'int' } }, { columnKey: "SmallIntLoja" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('UIExternaBotao_dGridLoja', 'SmallIntLoja', ui.oldValue, ui.value);}},  maxLength: 6, maxValue: null, minValue: 0, dataMode: 'short' } }, { columnKey: "StringLoja" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('UIExternaBotao_dGridLoja', 'StringLoja', ui.oldValue, ui.value);}}, maxLength: 50 } }],
                          rowDeleting: function (evt, ui) {
                              deletedIndex = ui.element.context.rowIndex;
                              var entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowID);
                              if (entity) {
                                  vm.deleteEntity(entity);
                              }
                          },
                          rowDeleted: function (evt, ui) {
                              var grid = $('#UIExternaBotao_dGridLoja');
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
                              var columns = $('#UIExternaBotao_dGridLoja').igGridUpdating('option', 'columnSettings');
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
                vm.OnDataGridCreated('UIExternaBotao_dGridLoja');
            }
            var selectionrowselectionchanged = null, selectedRowId = -1;
            selectionrowselectionchanged = function (evt, ui) {
                if ((ui.owner.grid.selectedRow() && typeof ui.owner.grid.selectedRow().id !== 'undefined') || (ui.owner.grid.selectedRows() && ui.owner.grid.selectedRows().length > 0)) { 
                    selectGridCurrentItem(vm.goToKey, 'RowDataId', ui); 
                 } 
                 if ((typeof vm.OnDataGridRowChecked === 'function')){ vm.OnDataGridRowChecked('UIExternaBotao_dGridLoja', self.selectedItems()); }
            };
            $(document).delegate('#UIExternaBotao_dGridLoja', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
        }
        vm.addDataSource({ key: 'UIExternaBotao_dGridLoja', name: 'dataView', itemsSource: itemsSource });
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
