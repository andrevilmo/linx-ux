define(['managers/__auth', 'managers/user'], function (managerAuth, managerUser) {
    var complementCtor = function() {
        var complement = {
            isAutomatic: true
            , ChangedBrandUIPaiFilhaNeta_dGridExpander_6af03c3127324c9b892b5bb7581ee2fd: function(vm, decimals, reset) {
                var i, format = '0.'.concat('0'.repeat(decimals)), grd =$('#UIPaiFilhaNeta_dGridExpander_6af03c3127324c9b892b5bb7581ee2fd').data('igGrid'),
                    grdUpd = $('#UIPaiFilhaNeta_dGridExpander_6af03c3127324c9b892b5bb7581ee2fd').data('igGridUpdating');
                if(isNull(grd) || isNull(grdUpd)) return;
                for (i = 0; i < grd.options.columns.length; i++) {
                }
                for (i = 0; i < grdUpd.options.columnSettings.length; i++) {
                }
                grd.dataBind();
            }
            , renderUIPaiFilhaNeta_dGridExpander_6af03c3127324c9b892b5bb7581ee2fd: function(vm) {
                var getDataSource = function() {
                    var source = null;
                    try {
                        source = vm.currentDataItem().VendaList;
                    }
                    catch (e) { }
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
                $('#UIPaiFilhaNeta_dGridExpander_6af03c3127324c9b892b5bb7581ee2fd_headers').live('focus  keydown', function (evt) {
                    var keyCode = window.event ? evt.which : evt.keyCode;
                    if (keyCode === 9) {
                        var cols = $('#UIPaiFilhaNeta_dGridExpander_6af03c3127324c9b892b5bb7581ee2fd').igGrid('option', 'columns');
                        var dataView = $('#UIPaiFilhaNeta_dGridExpander_6af03c3127324c9b892b5bb7581ee2fd').data('igGrid').dataSource._dataView
                        if (dataView.length === 0) return;
                        var firstRow = dataView[0].RowDataId;
                        clear = vm.status() === 'C';
                        if (vm.status() === 'C')
                            $('#UIPaiFilhaNeta_dGridExpander_6af03c3127324c9b892b5bb7581ee2fd').igGridUpdating('startEdit', firstRow, 0, true);
                        else {
                            var entity = findElementByKey(getDataSource(), 'RowDataId', firstRow);
                            var indexColumn = 0;
                            cols.some(function (entry) {
                                if (entry.key !== 'RowDataId' && !entry.hidden) {
                                    if (verifyCanEditCol(entry.key, clear, entity)) {
                                        $('#UIPaiFilhaNeta_dGridExpander_6af03c3127324c9b892b5bb7581ee2fd').igGridUpdating('startEdit', firstRow, indexColumn, true);
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
                   if($('#UIPaiFilhaNeta_dGridExpander_6af03c3127324c9b892b5bb7581ee2fd').data('igGrid') === undefined) return '';
                   var cols = $('#UIPaiFilhaNeta_dGridExpander_6af03c3127324c9b892b5bb7581ee2fd').igGrid('option', 'columns');
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
                  if (!grid) grid = $('#UIPaiFilhaNeta_dGridExpander_6af03c3127324c9b892b5bb7581ee2fd');
                  return ((!grid[0] || (!forceCreating && grid.parent().width() <= 0)) && !$('#dialogExpander_6af03c3127324c9b892b5bb7581ee2fd').is(':visible'));
                }
                var refreshData = true;
                var itemsSource = { isElementHided: isElementHided, getVisibleColumns: getVisibleColumns, containerId: 'UIPaiFilhaNeta_dGridExpander_6af03c3127324c9b892b5bb7581ee2fd_container', dataBind: function (commitData, forceCreating) {
                   var grid = $('#UIPaiFilhaNeta_dGridExpander_6af03c3127324c9b892b5bb7581ee2fd');
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
                       $('#UIPaiFilhaNeta_dGridExpander_6af03c3127324c9b892b5bb7581ee2fd_groupbyarea').addClass('hide');
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
                     if ($('#dialogExpander_6af03c3127324c9b892b5bb7581ee2fd').is(':visible')) {
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
                           $('label#currentNumberExpander_6af03c3127324c9b892b5bb7581ee2fd').html(current + ' - ' + totalCurrentPage);
                        }
                        else
                           $('label#currentNumberExpander_6af03c3127324c9b892b5bb7581ee2fd').html(1);
                        $('label#totalNumberExpander_6af03c3127324c9b892b5bb7581ee2fd').html(totalGrid);
                    }
                   } else {
                       $('label#currentNumberExpander_6af03c3127324c9b892b5bb7581ee2fd').html(0);
                       $('label#totalNumberExpander_6af03c3127324c9b892b5bb7581ee2fd').html(0);
                   }
                }};
                var valueGrouBy = -1;
                var deletedIndex = -1;
                function verifyCanEditCol(column, clear, entity){
                    switch(column){
                        case 'DatetimeVenda': { canEditing = clear || vm.enabledForEditing(); break;}
                        case 'ComboboxVenda': { canEditing = clear || vm.enabledForEditing(); break;}
                        case 'IdVenda': { canEditing = clear || (entity && entity.isAdded()); break;}
                        case 'IdVendedor': { canEditing = clear || vm.enabledForEditing(); break;}
                        case 'SmallIntVenda': { canEditing = clear || vm.enabledForEditing(); break;}
                        case 'StringVendedor': { canEditing = clear; break;}
                        case 'IntVenda': { canEditing = clear || vm.enabledForEditing(); break;}
                        case 'StringVenda': { canEditing = clear || vm.enabledForEditing(); break;}
                        case 'DecimalVenda': { canEditing = clear || vm.enabledForEditing(); break;}
                        case 'BitVenda': { canEditing = clear || vm.enabledForEditing(); break;}
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
                            $('#UIPaiFilhaNeta_dGridExpander_6af03c3127324c9b892b5bb7581ee2fd_LayoutBtn').igPopover('hide');
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
                                    mdl.showModal(vm, vm.gridSaveStates[gridId], gridId, saveAs).then(function (refreshSource, selectedLayout) {
                                        _this.loadLayouts().then(function () {
                                            if (typeof selectedLayout === 'object' && selectedLayout != null) {
                                                _this.currentLayoutId(selectedLayout.Id);
                                                _this.currentLayout(selectedLayout);
                                                _this.applyLayout(selectedLayout);
                                            }
                                            if (typeof selectedLayout === 'number' && selectedLayout > 0) {
                                                _this.savedLayouts().forEach(function(item) {
                                                    if (item.Id === selectedLayout) {
                                                        _this.currentLayoutId(selectedLayout);
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
                        rowVirtualization: true,
                        virtualizationMode: "continuous",
                        cellClick: function(evt, ui) {
                             if (ui.cellElement && ui.cellElement.childNodes[0] && ui.cellElement.childNodes[0].childNodes[1]) {
                                 var entity = null, e = ui.cellElement.childNodes[0].childNodes[1];
                                 if (e && e.tagName === 'IMG' && vm.status() !== 'C')
                                 {
                                      entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowKey);
                                      var key = e.attributes['key'].value;
                                      var table = e.attributes['tableName'].value;
                                      showMultimidia(entity, e, table, key, vm.UIPaiFilhaNeta());
                                 }
                             }
                             if (typeof vm.OnGridClientClick === 'function') {
                                 entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowKey);
                                 vm.OnGridClientClick('UIPaiFilhaNeta_dGridExpander_6af03c3127324c9b892b5bb7581ee2fd', ui.colKey, entity);
                             }
                             if (vm.status() != 'Q') {
                                 var grid = $('#UIPaiFilhaNeta_dGridExpander_6af03c3127324c9b892b5bb7581ee2fd');
                                 var isEditing = grid.igGridUpdating('isEditing');
                                 if (!isEditing && ui.colKey != undefined)
                                     grid.igGridUpdating('startEdit', ui.rowKey, ui.colKey, true);
                             }
                        },
                        enableUTCDates: true,
                        featureChooserIconDisplay: 'always',
                        rendered: function(evt, ui) {
                            if (isNull(vm.gridSaveStates[ui.owner.id()].gridSaveStates)) {
                                vm.gridSaveStates[ui.owner.id()].gridSaveStates = gridSaveStates(ui.owner.element, vm);
                                vm.gridSaveStates[ui.owner.id()].defaultLayout = { Id: -1, NomeLayout: "Layout Padrão", ConteudoJson: vm.gridSaveStates[ui.owner.id()].gridSaveStates.save() };
                            }
                            setTimeout(function() { $('#' + ui.owner.id() + '_headers>thead>tr>th').each(function(i, item) { if (item.attributes['aria-label']) { item.attributes['title'].value = item.attributes['aria-label'].value; } }); }, 500);
                            $('.ui-icon-gear').remove();
                        },
                        dataRendered: function(evt, ui) { 
                        },
                        columns: [
                            { key: 'RowDataId', headerText: 'RowDataId', width: '50px', dataType: 'number', hidden: true },
                            { key: 'DatetimeVenda', headerText: vm.getLayoutHeaderGrid('DatetimeVenda'), width: '218px', dataType: 'date', columnCssClass: 'ellipsis', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null   },
                            { key: 'ComboboxVenda', headerText: vm.getLayoutHeaderGrid('ComboboxVenda'), width: '218px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null  , formatter: function (val, record) { return  vm.dataDomains.getName('LX_VENDA', val);} },
                            { key: 'IdVenda', headerText: vm.getLayoutHeaderGrid('IdVenda'), width: '250px', dataType: 'number', columnCssClass: 'ellipsis', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                            { key: 'IdVendedor', headerText: vm.getLayoutHeaderGrid('IdVendedor'), width: '271px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'SmallIntVenda', headerText: vm.getLayoutHeaderGrid('SmallIntVenda'), width: '250px', dataType: 'number', columnCssClass: 'ellipsis', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                            { key: 'StringVendedor', headerText: vm.getLayoutHeaderGrid('StringVendedor'), width: '400px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'IntVenda', headerText: vm.getLayoutHeaderGrid('IntVenda'), width: '250px', dataType: 'number', columnCssClass: 'ellipsis', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                            { key: 'StringVenda', headerText: vm.getLayoutHeaderGrid('StringVenda'), width: '400px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'DecimalVenda', headerText: vm.getLayoutHeaderGrid('DecimalVenda'), width: '205px', dataType: 'number', columnCssClass: 'ellipsis', format: '0.00', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                            { key: 'BitVenda', headerText: vm.getLayoutHeaderGrid('BitVenda'), width: '153px', dataType: 'bool', columnCssClass: 'ellipsis', format: 'checkbox', hidden: false, unbound: false, group: null   }
                        ],
                        features: [
                                    { name: 'Sorting', type: 'local', caseSensitive: true, unsortedColumnTooltip: '', sortedColumnTooltip: '',
                                      columnSorting: function (evt, ui) { 
                                          return false;
                                      } 
                        },
                                    { name: 'Filtering', mode: 'advanced', filterDropDownItemIcons: false, filterDropDownWidth: 200, allowFiltering: true, type: 'local', renderFC: false, renderFilterButton: true, 
                                          dataFiltered: function (evt, ui) {
                                          var columnsFilters = [];
                                          $.each(ui.owner._currentAdvancedExpressions, function(i, item){
                                              if (item.expr != null)
                                                 columnsFilters.push(item.fieldName);
                                          });
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
                                        },
                                    dataFiltering: function (evt, ui) {
                                         if (ui.newExpressions.length == 1) {
                                             if (ui.newExpressions[0].expr == null) return false;
                                         } else {
                                             $.grep(ui.newExpressions, function (e) {
                                                 return e.logic = 'OR';
                                             });
                                         }
                                    },
                                    filterDialogOpening: function (evt, ui) {
                                         var dgl = ui.dialog;
                                         var divDinamica = dgl[0].id + '_din';
                                         if ($('#' + divDinamica).length)
                                             $('#' + divDinamica).remove();
                        
                                        var dataView = $('#UIPaiFilhaNeta_dGridExpander_6af03c3127324c9b892b5bb7581ee2fd').data('igGrid').dataSource;
                                        if (dataView.settings.filtering.expressions.length <= 0)
                                            dataView._filteredData = [];
                        
                                        var listGrid = '';
                                        var col = ui.owner._dialogCurrentColumn;
                        
                                        var reloadList = function (col) {
                                             var grid = $('#UIPaiFilhaNeta_dGridExpander_6af03c3127324c9b892b5bb7581ee2fd');
                                             var dataView = grid.data('igGrid').dataSource;
                                             listGrid = '<span>Propriedade: <b>' + col + '</b></span>';
                                             for (var i = 0; i < dataView._data.length; i++) {
                                                 var isChecked = '';
                                                 var rowId = dataView._data[i]['RowDataId'];
                        
                                                 if (dataView._filteredData != undefined && dataView._filteredData.length >= 1) {
                                                     isChecked = $.grep(dataView._filteredData, function (e) {
                                                         return e.RowDataId == dataView._data[i]['RowDataId'];
                                                     });
                                                 }
                        
                                                 isChecked = isChecked.length ? 'checked' : '';
                                                 listGrid += '<div style="white-space: nowrap;"><input type="checkbox" ' + isChecked + ' style="position:static;opacity:1;height:17px !important;"';
                                                 listGrid += 'onclick = "selRow(this)" value= ' + rowId + ' id= ' + rowId + ' name= ' + rowId + ' />';
                        
                                                 if (grid.igGrid('columnByKey', col).dataType == 'date') {
                                                     if (dataView._data[i][col] != '') {
                                                         if (dataView._data[i][col] == null)
                                                             listGrid += '<span>01/01/1990</span> ';
                                                         else
                                                             listGrid += '<span>' + Globalize.format(getUTCDate(dataView._data[i][col]), 'd') + '</span> ';
                                                     }
                                                 }
                                                 else
                                                     listGrid += '<span>' + dataView._data[i][col] + '</span> ';
                        
                                                 listGrid += '</div>';
                                             }
                        
                                             return listGrid;
                                        };
                        
                                        reloadList(col);
                                        var divDialog = $('#' + dgl[0].id).find('.ui-iggrid-filterdialogaddcondition').find('span')[0];
                        
                                        var scriptHtml = '<div id="' + divDinamica + '">';
                                        scriptHtml += '  <script>';
                                        scriptHtml += '    var newCol = "' + col + '";';
                                        scriptHtml += '    var newGrid = $("#UIPaiFilhaNeta_dGridExpander_6af03c3127324c9b892b5bb7581ee2fd");';
                                        scriptHtml += '    var listFilter = [];'
                                        scriptHtml += '    var reloadList = ' + reloadList + ';';
                                        scriptHtml += '    function hideColumn(){';
                                        scriptHtml += '     if ($("#showHideColumn")["0"].innerHTML.indexOf("Ocultar") >= 0) {';
                                        scriptHtml += '        $("#showHideColumn")["0"].innerHTML = "Mostrar Coluna";';
                                        scriptHtml += '        newGrid.igGridHiding("hideColumn", newCol);';
                                        scriptHtml += '     }';
                                        scriptHtml += '     else{';
                                        scriptHtml += '        $("#showHideColumn")["0"].innerHTML = "Ocultar Coluna";';
                                        scriptHtml += '        newGrid.igGridHiding("showColumn", newCol);';
                                        scriptHtml += '     }';
                                        scriptHtml += '    }';
                                        scriptHtml += '    function orderColumn(dir){';
                                        scriptHtml += '      if(dir == 1){dir = "asc"} else{dir = "desc"}';
                                        scriptHtml += '      if(newGrid.data("igGrid").dataSource._filteredData.length <= 0)';
                                        scriptHtml += '         newGrid.data("igGrid").dataSource._filter = false;';
                                        scriptHtml += '      newGrid.igGridSorting("sortColumn", newCol, dir);';
                                        scriptHtml += '    }';
                                        scriptHtml += '    function selRow(row){';
                                        scriptHtml += '      var list = newGrid.data("igGrid").dataSource;';
                                        scriptHtml += '      var filterFormated = [];';
                                        scriptHtml += '      if(row.checked){';
                                        scriptHtml += '         for (var i = 0; i < list._data.length; i++) {';
                                        scriptHtml += '                 if (list._data[i]["RowDataId"] == row.value){';
                                        scriptHtml += '                     if(list._filteredData != undefined && list.settings.filtering.expressions.length){';
                                        scriptHtml += '                         list._filteredData.push(list._data[i]);';
                                        scriptHtml += '                         listFilter = list._filteredData;';
                                        scriptHtml += '                         for (var p = 0; p < listFilter.length; p++) {';
                                        scriptHtml += '                             var value = listFilter[p]["RowDataId"];';
                                        scriptHtml += '                                  filterFormated.push({fieldName: "RowDataId", expr: parseInt(value) , cond: "equals", logic: "OR"});';
                                        scriptHtml += '                         }';
                                        scriptHtml += '                         newGrid.igGridFiltering("filter", filterFormated);';
                                        scriptHtml += '                     }';
                                        scriptHtml += '                     else{';
                                        scriptHtml += '                         newGrid.igGridFiltering("filter", ([{fieldName: "RowDataId", expr: parseInt(row.value), cond: "equals", logic: "OR"}]));';
                                        scriptHtml += '                     }';
                                        scriptHtml += '                     break;'
                                        scriptHtml += '                 }';
                                        scriptHtml += '             }';
                                        scriptHtml += '      }';
                                        scriptHtml += '      else {';
                                        scriptHtml += '         listFilter = newGrid.data("igGrid").dataSource._filteredData;';
                                        scriptHtml += '         for (var i = 0; i < listFilter.length; i++) {';
                                        scriptHtml += '                 if (listFilter[i]["RowDataId"] == row.value){';
                                        scriptHtml += '                     listFilter.splice(i, 1);';
                                        scriptHtml += '                     for (var p = 0; p < listFilter.length; p++) {';
                                        scriptHtml += '                         var value = listFilter[p]["RowDataId"];';
                                        scriptHtml += '                         filterFormated.push({fieldName: "RowDataId", expr: parseInt(value) , cond: "equals", logic: "OR"});';
                                        scriptHtml += '                     }';
                                        scriptHtml += '                     newGrid.igGridFiltering("filter", filterFormated);';
                                        scriptHtml += '                     break;';
                                        scriptHtml += '                 }';
                                        scriptHtml += '             }';
                                        scriptHtml += '      }';
                                        scriptHtml += '    }';
                                        scriptHtml += '  </script>';
                                        scriptHtml += '  <div  style="margin-left: 5px">';
                                        scriptHtml += '      <div>Propriedade:</div>';
                                        scriptHtml += '      <div id="comboFields"></div>';
                                        scriptHtml += '      <script>';
                                        scriptHtml += '         var columns = newGrid.igGrid("option", "columns");';
                                        scriptHtml += '         $("#comboFields").igCombo({ dataSource: columns, mode : "dropdown", valueKey: "key", textKey: "key", selectionChanging: function (evt, ui) {';
                                        scriptHtml += '            newCol = ui.items["0"].data.key;';
                                        scriptHtml += '            var newList = reloadList(newCol);';
                                        scriptHtml += '            $("#UIPaiFilhaNeta_dGridExpander_6af03c3127324c9b892b5bb7581ee2fd_container_dialog_list").html(newList)';
                                        scriptHtml += '         }});';
                                        scriptHtml += '         $("#comboFields").igCombo("value", newCol);';
                                        scriptHtml += '      </script>';
                                        scriptHtml += '  </div>';
                                        scriptHtml += '  <div style="margin-left: 5px; margin-top: 5px">';
                                        scriptHtml += '      <i class="fa fa-sort-alpha-asc" aria-hidden="true" style="margin-right: 5px;"></i><a onclick="orderColumn(1)" style="cursor: pointer">Ordem Crescente</a>';
                                        scriptHtml += '  </div>';
                                        scriptHtml += '  <div style="margin-left: 5px; margin-top: 5px">';
                                        scriptHtml += '      <i class="fa fa-sort-alpha-desc" aria-hidden="true" style="margin-right: 5px;"></i><a onclick="orderColumn(2)" style="cursor: pointer">Ordem Decrescente</a>';
                                        scriptHtml += '  </div>';
                                        scriptHtml += '  <div style="margin-left: 5px; margin-top: 5px">';
                                        scriptHtml += '      <i class="fa fa-eye-slash" aria-hidden="true" style="margin-right: 5px;"></i><a onclick="hideColumn()" style="cursor: pointer" id="showHideColumn">Ocultar Coluna</a>';
                                        scriptHtml += '  </div>';
                                        scriptHtml += '  <hr/>';
                                        scriptHtml += '  <div style="overflow: auto; max-height:100px" id="' + dgl[0].id + "_list" + '">';
                                        scriptHtml += listGrid;
                                        scriptHtml += '  </div>';
                                        scriptHtml += '  <hr/>';
                                        scriptHtml += '</div>';
                        
                                        $(scriptHtml).insertBefore(divDialog);
                                   },
                             },
                                    { name: 'Selection', mode: 'row'
                                    }, 
                                    { name: 'Tooltips', columnSettings:[{ columnKey: "DatetimeVenda", allowTooltips: true },{ columnKey: "ComboboxVenda", allowTooltips: true },{ columnKey: "IdVenda", allowTooltips: true },{ columnKey: "IdVendedor", allowTooltips: false },{ columnKey: "SmallIntVenda", allowTooltips: true },{ columnKey: "StringVendedor", allowTooltips: true },{ columnKey: "IntVenda", allowTooltips: true },{ columnKey: "StringVenda", allowTooltips: true },{ columnKey: "DecimalVenda", allowTooltips: true },{ columnKey: "BitVenda", allowTooltips: true }] },
                                    { name: 'Resizing' }, 
                                    { name: 'Hiding', 
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
                                      columnSettings: [{ columnKey: "IdVendedor", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpVendedor", isNullable: true, custom: vm.custom, vm: vm, verifyCanEditCol: verifyCanEditCol, allowMultiSelectionInSearch:true, activateAutoComplete: false, autoCompleteMaxResults: 7, validateOnClearState:false, maxValue:2147483647, maxLength: 10, defaultValue: null } }, { columnKey: 'DatetimeVenda', editorType: 'datepicker', editorOptions: {valueChanged: function(evt, ui){if (typeof vm.OnPropertyChangeDataGrid === 'function') {vm.OnPropertyChangeDataGrid('UIPaiFilhaNeta_dGridExpander_6af03c3127324c9b892b5bb7581ee2fd', 'DatetimeVenda', ui.oldValue, ui.value);}}, minValue: new Date(1900, 0, 1), datepickerOptions: { changeMonth: true, changeYear: true }  } }, { columnKey: 'ComboboxVenda', editorType: 'combo', editorOptions: {   selectionChanged: function (evt, ui) {   var val = null;   if (ui.items != null && ui.items.length > 0) { val = ui.items[0].data['id']; }  updateEntity('ComboboxVenda', val, false); },  mode: 'dropdown', dropDownOnFocus: true,  dataSource: vm.dataDomains.getItems('LX_VENDA', ''),  textKey: 'name', valueKey: 'id', enableClearButton: false }}, { columnKey: "IdVenda" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('UIPaiFilhaNeta_dGridExpander_6af03c3127324c9b892b5bb7581ee2fd', 'IdVenda', ui.oldValue, ui.value);}},  maxLength: 10, maxValue: null, dataMode: 'int' } }, { columnKey: "SmallIntVenda" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('UIPaiFilhaNeta_dGridExpander_6af03c3127324c9b892b5bb7581ee2fd', 'SmallIntVenda', ui.oldValue, ui.value);}},  maxLength: 5, maxValue: null, minValue: 0, dataMode: 'short' } }, { columnKey: "StringVendedor" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('UIPaiFilhaNeta_dGridExpander_6af03c3127324c9b892b5bb7581ee2fd', 'StringVendedor', ui.oldValue, ui.value);}}, maxLength: 50 } }, { columnKey: "IntVenda" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('UIPaiFilhaNeta_dGridExpander_6af03c3127324c9b892b5bb7581ee2fd', 'IntVenda', ui.oldValue, ui.value);}},  maxLength: 10, maxValue: null, minValue: 0, dataMode: 'int' } }, { columnKey: "StringVenda" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('UIPaiFilhaNeta_dGridExpander_6af03c3127324c9b892b5bb7581ee2fd', 'StringVenda', ui.oldValue, ui.value);}}, maxLength: 50 } }, { columnKey: "DecimalVenda" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('UIPaiFilhaNeta_dGridExpander_6af03c3127324c9b892b5bb7581ee2fd', 'DecimalVenda', ui.oldValue, ui.value);}},  maxLength: 14, maxValue: 99999999999.99, minValue: 0, dataMode: 'decimal', minDecimals: 2, maxDecimals: 2 } }],
                                      rowDeleting: function (evt, ui) {
                                          deletedIndex = ui.element.context.rowIndex;
                                          var entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowID);
                                          if (entity) {
                                              vm.deleteEntity(entity);
                                          }
                                      },
                                      rowDeleted: function (evt, ui) {
                                          var grid = $('#UIPaiFilhaNeta_dGridExpander_6af03c3127324c9b892b5bb7581ee2fd');
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
                                         if (vm.status() === 'Q'){
                                             var gridCell = ui.owner.grid;
                                             grid.find('div.borderCell').remove();
                                             $(gridCell.cellAt(ui.columnIndex - 1, ui.owner._rowIndex)).append("<div class='borderCell' style='z-index:100; border: 1px solid #849fd9 !important;'></div>");
                                          }
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
                                                          grid.igGridUpdating('startEdit', rowId, indexColumn, true);
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
                                          var columns = $('#UIPaiFilhaNeta_dGridExpander_6af03c3127324c9b892b5bb7581ee2fd').igGridUpdating('option', 'columnSettings');
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
                            vm.OnDataGridCreated('UIPaiFilhaNeta_dGridExpander_6af03c3127324c9b892b5bb7581ee2fd');
                        }
                        var selectionrowselectionchanged = null, selectedRowId = -1;
                        selectionrowselectionchanged = function (evt, ui) {
                            if ((ui.owner.grid.selectedRow() && typeof ui.owner.grid.selectedRow().id !== 'undefined') || (ui.owner.grid.selectedRows() && ui.owner.grid.selectedRows().length > 0)) { 
                                if (isNullOrEmpty(ui.owner.selectedRows())|| ui.selectedRows.length <= 1) {
                                    $(document).undelegate('#UIPaiFilhaNeta_dGridExpander_6af03c3127324c9b892b5bb7581ee2fd', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                                    ui.owner.clearSelection();
                                    ui.owner.selectRow(ui.row.index);
                                    if (vm.status() === 'Q'){
                                        var gridCell = ui.owner.grid;
                                        grid.find('div.borderCell').remove();
                                        //$(gridCell.cellAt(-1, ui.owner._rowIndex)).append(" < div class='borderCell' style='z-index:100; border: 1px solid #849fd9 !important;'></div>");
                                    }
                                    selectedRowId = ui.row.id;
                                    $(document).delegate ('#UIPaiFilhaNeta_dGridExpander_6af03c3127324c9b892b5bb7581ee2fd', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                                }
                                selectGridCurrentItem(vm.goToKey, 'RowDataId', ui, vm.currentDataItem().currentVenda, getDataSource()); 
                             } 
                        };
                        $(document).delegate('#UIPaiFilhaNeta_dGridExpander_6af03c3127324c9b892b5bb7581ee2fd', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                        $('#UIPaiFilhaNeta_dGridExpander_6af03c3127324c9b892b5bb7581ee2fd > tbody tr').live('focus', function(evt) {
                            var grid = $('#UIPaiFilhaNeta_dGridExpander_6af03c3127324c9b892b5bb7581ee2fd'), row = $(this).closest('tr'), id = parseInt(row.attr('data-id'), 10);
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
                    vm.addDataSource({ key: 'UIPaiFilhaNeta_dGridExpander_6af03c3127324c9b892b5bb7581ee2fd', name: 'VendaList', itemsSource: itemsSource });
                }
            
            , renderpivoteddf683f6acd456db79626c004f9b13bUIPaiFilhaNeta_pivotVendaItem: function(vm) {
                var flexmonsterPath = managerAuth.META_ROOT + managerAuth.META_MODULE_ID + '/lib/flexmonster/';
                var pivot = null;
                var arrayData = [];
                var currentStatus = '';
                var currentPage = undefined;
                var app = require('durandal/app');
                var jEntitySearchPivotRelationship = '';
                var dataSourceIsLoaded = function() {
                    var isLoaded = false;
                    try {
                        isLoaded = (vm.currentDataItem().currentVenda().VendaItemIsLoaded === true || vm.currentDataItem().currentVenda().VendaItemList().length > 0);
                    }
                    catch (e) {
                        isLoaded = true;
                    }
                    return isLoaded;
                };
                var getVisibleColumns = function() {
                   return '';
                };
                var itemsSource = { 
                    getVisibleColumns: getVisibleColumns, dataBind: function (commitData) {
                        var currentRelation = vm.currentDataItem().currentVenda().GetJsWhereDetailRelationForVendaItem();
                        if ($('#pivoteddf683f-6acd-456d-b796-26c004f9b13bUIPaiFilhaNeta_pivotVendaItem').is(':visible')) {
                             if (this.lastRelation == currentRelation && vm.isDashboardFilter && vm.status() == 'C') {
                                 arrayData = unwrapObservableArray(vm.currentDataItem().currentVenda().VendaItemList, vm);
                                 this.lastRelation = '';
                             } 
                             else if (this.lastRelation != currentRelation){
                                 this.lastRelation = currentRelation;
                                 currentStatus = vm.status();
                                 currentPage = vm.dataToolbar.currentPage();
                                 if (currentStatus && currentStatus.toLowerCase() == 'c') {
                                      jEntitySearchPivotRelationship = '';
                                 }
                                 if ((vm.status() != 'C' && vm.status() != 'I') && !dataSourceIsLoaded()) {
                                     vm.currentDataItem().currentVenda().fillDetails(false, 'VendaItem');
                                     return;
                                 }
                                 if(vm != null && vm.currentDataItem() != null && vm.currentDataItem().currentVenda() != null && vm.currentDataItem().currentVenda().VendaItemList != null ) {
                                      arrayData = unwrapObservableArray(vm.currentDataItem().currentVenda().VendaItemList, vm);
                                      if (vm.status() == 'C') this.lastRelation = '';
                                 }
                             }
                             else {
                                 return;
                             }
                        if(pivot == null) {
                        $('#pivoteddf683f-6acd-456d-b796-26c004f9b13bUIPaiFilhaNeta_pivotVendaItem #fm-fields-view .fm-ui-btn:contains(\'OK\')')
                             .live('mouseup', function () {
                         });
                        
                            $('#pivoteddf683f-6acd-456d-b796-26c004f9b13bUIPaiFilhaNeta_pivotVendaItem #fm-toolbar-row .fm-ui-btn:contains(\'OK\')')
                               .live('mouseup', function () {
                                   setTimeout(function () { filterPivotRelationship() }, 1);
                            });
                        }
                        
                        var pivotContext = { rows: [], columns: [], pages: [], measures: [], options: null, formats: [], conditions: [], report: null };
                        
                        var addMeasuresFormulas = function () {
                        };
                        
                        var updateData = function() { 
                            var data = arrayData.map(function(item){
                                return {'ComboboxVendaItem': isNullOrEmpty(item.ComboboxVendaItem) ? 0 : item.ComboboxVendaItem, 'DatetimeVendaItem': (isNullOrEmpty(item.DatetimeVendaItem) ? '' : item.DatetimeVendaItem.toString()), 'DecimalVendaItem': isNullOrEmpty(item.DecimalVendaItem) ? 0 : item.DecimalVendaItem, 'IdVenda': (isNullOrEmpty(item.IdVenda) ? '' : item.IdVenda.toString()), 'IdVendaItem': (isNullOrEmpty(item.IdVendaItem) ? '' : item.IdVendaItem.toString()), 'StringVendaItem': (isNullOrEmpty(item.StringVendaItem) ? '' : item.StringVendaItem.toString())};
                            });
                        
                            var structure = {};
                            structure.ComboboxVendaItem = { type:'number', caption: vm.getLayoutDisplayName('UIPaiFilhaNeta_cmbVendaItem_ComboboxVendaItem') }; 
                            structure.DatetimeVendaItem = { type:'string', caption: vm.getLayoutDisplayName('UIPaiFilhaNeta_dtVendaItem_DatetimeVendaItem'), dimensionUniqueName: vm.getDimensionUniqueName('UIPaiFilhaNeta_dtVendaItem_DatetimeVendaItem')};
                            structure.DecimalVendaItem = { type:'number', caption: vm.getLayoutDisplayName('UIPaiFilhaNeta_ntxDecimalVendaItem') }; 
                            structure.IdVenda = { type:'string', caption: vm.getLayoutDisplayName('UIPaiFilhaNeta_ntxIdVenda'), dimensionUniqueName: vm.getDimensionUniqueName('UIPaiFilhaNeta_ntxIdVenda')};
                            structure.IdVendaItem = { type:'string', caption: vm.getLayoutDisplayName('UIPaiFilhaNeta_ntxIdVendaItem'), dimensionUniqueName: vm.getDimensionUniqueName('UIPaiFilhaNeta_ntxIdVendaItem')};
                            structure.StringVendaItem = { type:'string', caption: vm.getLayoutDisplayName('UIPaiFilhaNeta_tbVendaItem_StringVendaItem'), dimensionUniqueName: vm.getDimensionUniqueName('UIPaiFilhaNeta_tbVendaItem_StringVendaItem')};
                        
                            pivot.updateData({ data: [structure].concat(data) });
                        };
                        
                        var getFormats = function () {
                            var measuresFormat = getMeasureFormats();
                            var measuresCalculatedFormat = Flexmonster.getMeasureCalculated(pivot);
                        
                            return measuresFormat.concat(measuresCalculatedFormat);
                        };
                        
                        var getMeasureFormats = function() {
                            var formatMeasures = [];
                            formatMeasures.push({ name: 'ComboboxVendaItem',  current: pivot.getFormat('ComboboxVendaItem') });
                            formatMeasures.push({ name: 'DecimalVendaItem',  current: pivot.getFormat('DecimalVendaItem') });
                        
                            return formatMeasures;
                        };
                        
                        var getAllConditions = function () {
                            return pivot.getAllConditions();
                        };
                        
                        var setConditions = function () {
                            if (pivotContext.conditions.length) {
                                pivotContext.conditions.forEach(function (item) {
                                    pivot.addCondition(item);
                                });
                            }
                        };
                        
                        var setFormat = function () {
                            if (pivotContext.formats.length) {
                                pivotContext.formats.forEach(function (item) {
                                    pivot.setFormat(item.current, item.name);
                                });
                            } else {
                                pivot.setFormat({
                                    decimalSeparator: ',',
                                    thousandsSeparator: '.',
                                    name: 'ComboboxVendaItem'
                                }, 'ComboboxVendaItem');
                                pivot.setFormat({
                                    decimalPlaces : '2',
                                    decimalSeparator: ',',
                                    thousandsSeparator: '.',
                                    name: 'DecimalVendaItem'
                                }, 'DecimalVendaItem');
                            }
                        };
                        
                        var setSlice = function (isCreating) {
                             if (!isCreating){
                                 var slice = {};
                                 slice.rows = [{ uniqueName: 'IdVenda' }];
                                 slice.columns = [{ uniqueName: 'IdVendaItem' }];
                                 slice.measures = [{ uniqueName: 'DecimalVendaItem' }];
                                 slice.rows.push({uniqueName: '[Measures]'});
                                 if (!isNull(slice)) pivot.runQuery(slice);
                             } else if (pivotContext.rows.length || pivotContext.columns.length || pivotContext.measures.length || pivotContext.pages.length) {
                                 pivot.runQuery(pivotContext);
                             }
                        };
                        
                        var setOptions = function () {
                            if (pivotContext.options != null) {
                                 pivot.setOptions(pivotContext.options);
                            }
                        };
                        
                        var setFilterByCell = function (filters, format) {
                            if (format.length)
                                format.forEach(function (item) {
                                    if (!filters.some(function (filter) { return item.hierarchyUniqueName == filter.key }))
                                        filters.push({
                                            negation: false,
                                            key: item.hierarchyUniqueName,
                                            values: [item.hierarchyUniqueName +'.['+ item.caption + ']'],
                                        });
                                });
                        };
                        
                        var setFilterByReport = function (filters, reportItem) {
                            if (reportItem.length)
                                reportItem.forEach(function (item) {
                                    if (item.filter && item.filter.members.length && !filters.some(function (filter) { return item.uniqueName == filter.key })) {
                                        var currentItem = {
                                            values: item.filter.members,
                                            key: item.uniqueName,
                                            negation: item.filter.negation,
                                        };
                                        filters.push(currentItem);
                                    }
                                });
                        };
                        
                        var parseFilters = function (cell) {
                            var filterItems = [];
                            var report = pivot.getReport();
                            if (cell) {
                                setFilterByCell(filterItems, cell.rows);
                                setFilterByCell(filterItems, cell.columns);
                            }
                            setFilterByReport(filterItems, report.slice.rows);
                            setFilterByReport(filterItems, report.slice.pages ? report.slice.pages : []);
                            setFilterByReport(filterItems, report.slice.columns);
                            return filterItems;
                        };
                        
                        var filterPivotRelationship = function (cell) {
                           if ((cell && cell.type != 'value') || isNaN(cell.value)) return false;
                           var dataContext = vm.getDataContext();
                           var dataFilter = parseFilters(cell);
                           var jEntitySearch = Flexmonster.parsejEntitySearch(dataFilter);
                           if (!jEntitySearchPivotRelationship || jEntitySearchPivotRelationship != jEntitySearch) {
                               jEntitySearchPivotRelationship = jEntitySearch;
                               vm.showProcessing('Pesquisando informações...');
                               if (arrayData && arrayData.length > 0) {
                                   arrayData[0].fillDetails(true, '', true, true, function () {
                                       vm.closeProcessing();
                                   }, jEntitySearch);
                               }
                           }
                        };
                        
                        var setLayouts = function () {
                             vm.layoutFiles.forEach(function(file) {
                                 if (file.selected && (file.layoutFullName.indexOf('.xml') > 0 || file.layoutFullName.indexOf('.json') > 0) && file.layoutFullName.indexOf('VendaItem') > 0)
                                     pivot.load(file.layoutFullName);
                             });
                        };
                        
                        var onPivotReady = function () {
                             pivot.clear();
                             updateData();
                             addMeasuresFormulas();
                             setLayouts();
                             setOptions();
                             setSlice(true);
                             setFormat();
                             pivot.refresh();
                             if (typeof vm.OnchangePivotLayoutOnLoad === 'function') {
                                 vm.OnchangePivotLayoutOnLoad(pivot);
                             }
                        };
                        
                        var onBeforeToolbarCreated = function (toolbarInstance) {
                            Flexmonster.initLinxToolbar({
                                toolbarInstance: toolbarInstance,
                                vm: vm,
                                pivotName: 'VendaItem',
                                pivotAdapterLayout: 'VendaItem',
                                tb_layoutToolbar: false,
                                tb_FullScreen: false,
                                tb_ToggleView: false,
                                tb_OpenReport: false
                            });
                        }; 
                        
                        var onCellClick = function (cell) {
                            filterPivotRelationship(cell);
                        };
                        
                        var updatePivot = function () {
                            pivotContext.rows = pivot.getRows();
                            pivotContext.pages = pivot.getPages();
                            pivotContext.columns = pivot.getColumns();
                            pivotContext.options = pivot.getOptions();
                            pivotContext.measures = pivot.getMeasures();
                            pivotContext.formats = getFormats();
                            pivotContext.report = pivot.getReport();
                            pivotContext.conditions = getAllConditions();
                        
                            updateData();
                            addMeasuresFormulas();
                            setOptions();
                            setSlice(false);
                            setFormat();
                            setConditions();
                            pivot.refresh();
                            pivot.closeFieldsList();
                        };
                        
                        var setLanguage = function(lang) {
                            var idioma = lang;
                            if (idioma.indexOf('pt-br') >= 0)
                                return;
                            else {
                                try {
                                    var nameFileLang = managerAuth.META_ROOT + managerAuth.META_MODULE_ID + "/lib/flexmonster/toolbar/language_toolbar/" + idioma + ".js";
                        
                                    var fRef = document.createElement('script');
                                    fRef.setAttribute("type", "text/javascript");
                                    fRef.setAttribute("src", nameFileLang);
                                    document.getElementsByTagName("head")[0].appendChild(fRef);
                                }
                                catch (e)
                                {
                                    console.log("Arquivo de tradução não encontrado[" + idioma + "].");
                                }
                            }
                        };
                        
                        var timeout = 50;
                        var createInstance = function () {
                             var idioma = vm.common.getIdioma();
                             var createPivotInstance = function() {
                                 pivot = new Flexmonster({
                                     container: 'pivoteddf683f-6acd-456d-b796-26c004f9b13bUIPaiFilhaNeta_pivotVendaItem',
                                     componentFolder: flexmonsterPath,
                                     report: flexmonsterPath + 'report_lang/report_' + idioma + '.json',
                                     toolbar: true,
                                     width: '100%',
                                     height: 350,
                                     licenseKey: managerAuth.flexMonsterLicenseKey
                                 });
                                 pivot.on('cellclick', onCellClick);
                                 pivot.on('ready', onPivotReady);
                                 pivot.on('beforetoolbarcreated', onBeforeToolbarCreated);
                             };
                        
                             if (idioma.indexOf('pt-br') >= 0) {
                                 createPivotInstance();
                             } else {
                                 setTimeout(function() {
                                     timeout--;
                                     if (typeof langPropsToolbar == "function" && Object.getOwnPropertyNames(langPropsToolbar()).length > 0) {
                                         if (langToolbar() == idioma) {
                                             return createPivotInstance(idioma);
                                         }
                                         else if (timeout > 0)
                                             createInstance();
                                         else {
                                             vm.common.saveIdioma("pt-br");
                                             $("#cmbIdioma").val("pt-br");
                                             console.log("Erro ao carregar idioma[" + idioma + "]!");
                                             return createPivotInstance();
                                         }
                                     }
                                     else if (timeout > 0)
                                         createInstance();
                                     else {
                                         vm.common.saveIdioma("pt-br");
                                         $("#cmbIdioma").val("pt-br");
                                         console.log("Erro ao carregar idioma[" + idioma + "]!");
                                         return createPivotInstance();
                                     }
                                 }, 100);
                             }
                        };
                        
                             try {
                                 var idioma = vm.common.getIdioma();
                                 if (idioma.indexOf('pt-br') < 0)
                                     setLanguage(idioma);
                                 if (pivot == null)
                                     createInstance();
                                 else
                                     updatePivot();
                             }
                             catch (e) { }
                        }
                        
                    }
                };
                if (vm.addDataSource){ vm.addDataSource({ key: 'pivoteddf683f-6acd-456d-b796-26c004f9b13bUIPaiFilhaNeta_pivotVendaItem', name: 'VendaItemList', itemsSource: itemsSource }); }
                else { itemsSource.dataBind(); }
            }
            
            
            , ChangedBrandscyUIPaiFilhaNeta_dGrid: function(vm, decimals, reset) {
                var i, format = '0.'.concat('0'.repeat(decimals)), grd =$('#scyUIPaiFilhaNeta_dGrid').data('igGrid'),
                    grdUpd = $('#scyUIPaiFilhaNeta_dGrid').data('igGridUpdating');
                if(isNull(grd) || isNull(grdUpd)) return;
                for (i = 0; i < grd.options.columns.length; i++) {
                }
                for (i = 0; i < grdUpd.options.columnSettings.length; i++) {
                }
                grd.dataBind();
            }
            , renderscyUIPaiFilhaNeta_dGrid: function(vm) {
                var getDataSource = function() {
                    var source = null;
                    try {
                        source = vm.dataView;
                    }
                    catch (e) { }
                    return isNullOrEmpty(source) ? ko.observableArray([]) : source;
                };
                $('#scyUIPaiFilhaNeta_dGrid_headers').live('focus  keydown', function (evt) {
                    var keyCode = window.event ? evt.which : evt.keyCode;
                    if (keyCode === 9) {
                        var cols = $('#scyUIPaiFilhaNeta_dGrid').igGrid('option', 'columns');
                        var dataView = $('#scyUIPaiFilhaNeta_dGrid').data('igGrid').dataSource._dataView
                        if (dataView.length === 0) return;
                        var firstRow = dataView[0].RowDataId;
                        clear = vm.status() === 'C';
                        if (vm.status() === 'C')
                            $('#scyUIPaiFilhaNeta_dGrid').igGridUpdating('startEdit', firstRow, 0, true);
                        else {
                            var entity = findElementByKey(getDataSource(), 'RowDataId', firstRow);
                            var indexColumn = 0;
                            cols.some(function (entry) {
                                if (entry.key !== 'RowDataId' && !entry.hidden) {
                                    if (verifyCanEditCol(entry.key, clear, entity)) {
                                        $('#scyUIPaiFilhaNeta_dGrid').igGridUpdating('startEdit', firstRow, indexColumn, true);
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
                   if($('#scyUIPaiFilhaNeta_dGrid').data('igGrid') === undefined) return '';
                   var cols = $('#scyUIPaiFilhaNeta_dGrid').igGrid('option', 'columns');
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
                  if (!grid) grid = $('#scyUIPaiFilhaNeta_dGrid');
                  return ((!grid[0] || (!forceCreating && grid.parent().width() <= 0)) && !$('#dialog').is(':visible'));
                }
                var refreshData = true;
                var itemsSource = { isElementHided: isElementHided, getVisibleColumns: getVisibleColumns, containerId: 'scyUIPaiFilhaNeta_dGrid_container', dataBind: function (commitData, forceCreating) {
                   var grid = $('#scyUIPaiFilhaNeta_dGrid');
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
                       $('#scyUIPaiFilhaNeta_dGrid_groupbyarea').addClass('hide');
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
                        case 'IdLoja': { canEditing = clear; break;}
                        case 'IntLoja': { canEditing = clear; break;}
                        case 'BigIntLoja': { canEditing = clear; break;}
                        case 'BitLoja': { canEditing = clear; break;}
                        case 'IdCidade': { canEditing = clear; break;}
                        case 'IdEstado': { canEditing = clear; break;}
                        case 'IdPais': { canEditing = clear; break;}
                        case 'NomeCidade': { canEditing = clear; break;}
                        case 'StringPais': { canEditing = clear; break;}
                        case 'StringLoja': { canEditing = clear; break;}
                        case 'ComboboxLoja': { canEditing = clear; break;}
                        case 'DatetimeLoja': { canEditing = clear; break;}
                        case 'DecimalLoja': { canEditing = clear; break;}
                        case 'SmallIntLoja': { canEditing = clear; break;}
                        case 'StringEstado': { canEditing = clear; break;}
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
                            $('#scyUIPaiFilhaNeta_dGrid_LayoutBtn').igPopover('hide');
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
                                    mdl.showModal(vm, vm.gridSaveStates[gridId], gridId, saveAs).then(function (refreshSource, selectedLayout) {
                                        _this.loadLayouts().then(function () {
                                            if (typeof selectedLayout === 'object' && selectedLayout != null) {
                                                _this.currentLayoutId(selectedLayout.Id);
                                                _this.currentLayout(selectedLayout);
                                                _this.applyLayout(selectedLayout);
                                            }
                                            if (typeof selectedLayout === 'number' && selectedLayout > 0) {
                                                _this.savedLayouts().forEach(function(item) {
                                                    if (item.Id === selectedLayout) {
                                                        _this.currentLayoutId(selectedLayout);
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
                                      showMultimidia(entity, e, table, key, vm.UIPaiFilhaNeta());
                                 }
                             }
                             if (typeof vm.OnGridClientClick === 'function') {
                                 entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowKey);
                                 vm.OnGridClientClick('scyUIPaiFilhaNeta_dGrid', ui.colKey, entity);
                             }
                             if (vm.status() != 'Q') {
                                 var grid = $('#scyUIPaiFilhaNeta_dGrid');
                                 var isEditing = grid.igGridUpdating('isEditing');
                                 if (!isEditing && ui.colKey != undefined)
                                     grid.igGridUpdating('startEdit', ui.rowKey, ui.colKey, true);
                             }
                        },
                        enableUTCDates: true,
                        featureChooserIconDisplay: 'always',
                        rendered: function(evt, ui) {
                            if (isNull(vm.gridSaveStates[ui.owner.id()].gridSaveStates)) {
                                vm.gridSaveStates[ui.owner.id()].gridSaveStates = gridSaveStates(ui.owner.element, vm);
                                vm.gridSaveStates[ui.owner.id()].defaultLayout = { Id: -1, NomeLayout: "Layout Padrão", ConteudoJson: vm.gridSaveStates[ui.owner.id()].gridSaveStates.save() };
                            }
                            setTimeout(function() { $('#' + ui.owner.id() + '_headers>thead>tr>th').each(function(i, item) { if (item.attributes['aria-label']) { item.attributes['title'].value = item.attributes['aria-label'].value; } }); }, 500);
                            $('.ui-icon-gear').remove();
                        },
                        dataRendered: function(evt, ui) { 
                        },
                        columns: [
                            { key: 'RowDataId', headerText: 'RowDataId', width: '50px', dataType: '', hidden: true },
                            { key: 'IdLoja', headerText: vm.getLayoutHeaderGrid('IdLoja'), width: '250px', dataType: 'number', columnCssClass: 'ellipsis', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                            { key: 'IntLoja', headerText: vm.getLayoutHeaderGrid('IntLoja'), width: '250px', dataType: 'number', columnCssClass: 'ellipsis', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                            { key: 'BigIntLoja', headerText: vm.getLayoutHeaderGrid('BigIntLoja'), width: '250px', dataType: 'number', columnCssClass: 'ellipsis', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                            { key: 'BitLoja', headerText: vm.getLayoutHeaderGrid('BitLoja'), width: '140px', dataType: 'bool', columnCssClass: 'ellipsis', format: 'checkbox', hidden: false, unbound: false, group: null   },
                            { key: 'IdCidade', headerText: vm.getLayoutHeaderGrid('IdCidade'), width: '271px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'IdEstado', headerText: vm.getLayoutHeaderGrid('IdEstado'), width: '271px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'IdPais', headerText: vm.getLayoutHeaderGrid('IdPais'), width: '271px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'NomeCidade', headerText: vm.getLayoutHeaderGrid('NomeCidade'), width: '421px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'StringPais', headerText: vm.getLayoutHeaderGrid('StringPais'), width: '421px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'StringLoja', headerText: vm.getLayoutHeaderGrid('StringLoja'), width: '400px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'ComboboxLoja', headerText: vm.getLayoutHeaderGrid('ComboboxLoja'), width: '205px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null  , formatter: function (val, record) { return  vm.dataDomains.getName('LX_COMBOBOX_LOJA', val);} },
                            { key: 'DatetimeLoja', headerText: vm.getLayoutHeaderGrid('DatetimeLoja'), width: '205px', dataType: 'date', columnCssClass: 'ellipsis', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null   },
                            { key: 'DecimalLoja', headerText: vm.getLayoutHeaderGrid('DecimalLoja'), width: '192px', dataType: 'number', columnCssClass: 'ellipsis', format: '0.00', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                            { key: 'SmallIntLoja', headerText: vm.getLayoutHeaderGrid('SmallIntLoja'), width: '250px', dataType: 'number', columnCssClass: 'ellipsis', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                            { key: 'StringEstado', headerText: vm.getLayoutHeaderGrid('StringEstado'), width: '421px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   }
                        ],
                        features: [
                                    { name: 'Sorting', type: 'local', caseSensitive: true, unsortedColumnTooltip: '', sortedColumnTooltip: '',
                                      columnSorting: function (evt, ui) { 
                                          return false;
                                      } 
                                      , columnSorted: function (event, args) { if (!isNullOrEmpty(args.columnKey) && !isNullOrEmpty(args.direction)) { vm.sortData(args.columnKey + ' ' + args.direction); } } },
                                    { name: 'Filtering', mode: 'advanced', filterDropDownItemIcons: false, filterDropDownWidth: 200, allowFiltering: true, type: 'local', renderFC: false, renderFilterButton: true, 
                                          dataFiltered: function (evt, ui) {
                                          var columnsFilters = [];
                                          $.each(ui.owner._currentAdvancedExpressions, function(i, item){
                                              if (item.expr != null)
                                                 columnsFilters.push(item.fieldName);
                                          });
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
                                        },
                                    dataFiltering: function (evt, ui) {
                                         if (ui.newExpressions.length == 1) {
                                             if (ui.newExpressions[0].expr == null) return false;
                                         } else {
                                             $.grep(ui.newExpressions, function (e) {
                                                 return e.logic = 'OR';
                                             });
                                         }
                                    },
                                    filterDialogOpening: function (evt, ui) {
                                         var dgl = ui.dialog;
                                         var divDinamica = dgl[0].id + '_din';
                                         if ($('#' + divDinamica).length)
                                             $('#' + divDinamica).remove();
                        
                                        var dataView = $('#scyUIPaiFilhaNeta_dGrid').data('igGrid').dataSource;
                                        if (dataView.settings.filtering.expressions.length <= 0)
                                            dataView._filteredData = [];
                        
                                        var listGrid = '';
                                        var col = ui.owner._dialogCurrentColumn;
                        
                                        var reloadList = function (col) {
                                             var grid = $('#scyUIPaiFilhaNeta_dGrid');
                                             var dataView = grid.data('igGrid').dataSource;
                                             listGrid = '<span>Propriedade: <b>' + col + '</b></span>';
                                             for (var i = 0; i < dataView._data.length; i++) {
                                                 var isChecked = '';
                                                 var rowId = dataView._data[i]['RowDataId'];
                        
                                                 if (dataView._filteredData != undefined && dataView._filteredData.length >= 1) {
                                                     isChecked = $.grep(dataView._filteredData, function (e) {
                                                         return e.RowDataId == dataView._data[i]['RowDataId'];
                                                     });
                                                 }
                        
                                                 isChecked = isChecked.length ? 'checked' : '';
                                                 listGrid += '<div style="white-space: nowrap;"><input type="checkbox" ' + isChecked + ' style="position:static;opacity:1;height:17px !important;"';
                                                 listGrid += 'onclick = "selRow(this)" value= ' + rowId + ' id= ' + rowId + ' name= ' + rowId + ' />';
                        
                                                 if (grid.igGrid('columnByKey', col).dataType == 'date') {
                                                     if (dataView._data[i][col] != '') {
                                                         if (dataView._data[i][col] == null)
                                                             listGrid += '<span>01/01/1990</span> ';
                                                         else
                                                             listGrid += '<span>' + Globalize.format(getUTCDate(dataView._data[i][col]), 'd') + '</span> ';
                                                     }
                                                 }
                                                 else
                                                     listGrid += '<span>' + dataView._data[i][col] + '</span> ';
                        
                                                 listGrid += '</div>';
                                             }
                        
                                             return listGrid;
                                        };
                        
                                        reloadList(col);
                                        var divDialog = $('#' + dgl[0].id).find('.ui-iggrid-filterdialogaddcondition').find('span')[0];
                        
                                        var scriptHtml = '<div id="' + divDinamica + '">';
                                        scriptHtml += '  <script>';
                                        scriptHtml += '    var newCol = "' + col + '";';
                                        scriptHtml += '    var newGrid = $("#scyUIPaiFilhaNeta_dGrid");';
                                        scriptHtml += '    var listFilter = [];'
                                        scriptHtml += '    var reloadList = ' + reloadList + ';';
                                        scriptHtml += '    function hideColumn(){';
                                        scriptHtml += '     if ($("#showHideColumn")["0"].innerHTML.indexOf("Ocultar") >= 0) {';
                                        scriptHtml += '        $("#showHideColumn")["0"].innerHTML = "Mostrar Coluna";';
                                        scriptHtml += '        newGrid.igGridHiding("hideColumn", newCol);';
                                        scriptHtml += '     }';
                                        scriptHtml += '     else{';
                                        scriptHtml += '        $("#showHideColumn")["0"].innerHTML = "Ocultar Coluna";';
                                        scriptHtml += '        newGrid.igGridHiding("showColumn", newCol);';
                                        scriptHtml += '     }';
                                        scriptHtml += '    }';
                                        scriptHtml += '    function orderColumn(dir){';
                                        scriptHtml += '      if(dir == 1){dir = "asc"} else{dir = "desc"}';
                                        scriptHtml += '      if(newGrid.data("igGrid").dataSource._filteredData.length <= 0)';
                                        scriptHtml += '         newGrid.data("igGrid").dataSource._filter = false;';
                                        scriptHtml += '      newGrid.igGridSorting("sortColumn", newCol, dir);';
                                        scriptHtml += '    }';
                                        scriptHtml += '    function selRow(row){';
                                        scriptHtml += '      var list = newGrid.data("igGrid").dataSource;';
                                        scriptHtml += '      var filterFormated = [];';
                                        scriptHtml += '      if(row.checked){';
                                        scriptHtml += '         for (var i = 0; i < list._data.length; i++) {';
                                        scriptHtml += '                 if (list._data[i]["RowDataId"] == row.value){';
                                        scriptHtml += '                     if(list._filteredData != undefined && list.settings.filtering.expressions.length){';
                                        scriptHtml += '                         list._filteredData.push(list._data[i]);';
                                        scriptHtml += '                         listFilter = list._filteredData;';
                                        scriptHtml += '                         for (var p = 0; p < listFilter.length; p++) {';
                                        scriptHtml += '                             var value = listFilter[p]["RowDataId"];';
                                        scriptHtml += '                                  filterFormated.push({fieldName: "RowDataId", expr: parseInt(value) , cond: "equals", logic: "OR"});';
                                        scriptHtml += '                         }';
                                        scriptHtml += '                         newGrid.igGridFiltering("filter", filterFormated);';
                                        scriptHtml += '                     }';
                                        scriptHtml += '                     else{';
                                        scriptHtml += '                         newGrid.igGridFiltering("filter", ([{fieldName: "RowDataId", expr: parseInt(row.value), cond: "equals", logic: "OR"}]));';
                                        scriptHtml += '                     }';
                                        scriptHtml += '                     break;'
                                        scriptHtml += '                 }';
                                        scriptHtml += '             }';
                                        scriptHtml += '      }';
                                        scriptHtml += '      else {';
                                        scriptHtml += '         listFilter = newGrid.data("igGrid").dataSource._filteredData;';
                                        scriptHtml += '         for (var i = 0; i < listFilter.length; i++) {';
                                        scriptHtml += '                 if (listFilter[i]["RowDataId"] == row.value){';
                                        scriptHtml += '                     listFilter.splice(i, 1);';
                                        scriptHtml += '                     for (var p = 0; p < listFilter.length; p++) {';
                                        scriptHtml += '                         var value = listFilter[p]["RowDataId"];';
                                        scriptHtml += '                         filterFormated.push({fieldName: "RowDataId", expr: parseInt(value) , cond: "equals", logic: "OR"});';
                                        scriptHtml += '                     }';
                                        scriptHtml += '                     newGrid.igGridFiltering("filter", filterFormated);';
                                        scriptHtml += '                     break;';
                                        scriptHtml += '                 }';
                                        scriptHtml += '             }';
                                        scriptHtml += '      }';
                                        scriptHtml += '    }';
                                        scriptHtml += '  </script>';
                                        scriptHtml += '  <div  style="margin-left: 5px">';
                                        scriptHtml += '      <div>Propriedade:</div>';
                                        scriptHtml += '      <div id="comboFields"></div>';
                                        scriptHtml += '      <script>';
                                        scriptHtml += '         var columns = newGrid.igGrid("option", "columns");';
                                        scriptHtml += '         $("#comboFields").igCombo({ dataSource: columns, mode : "dropdown", valueKey: "key", textKey: "key", selectionChanging: function (evt, ui) {';
                                        scriptHtml += '            newCol = ui.items["0"].data.key;';
                                        scriptHtml += '            var newList = reloadList(newCol);';
                                        scriptHtml += '            $("#scyUIPaiFilhaNeta_dGrid_container_dialog_list").html(newList)';
                                        scriptHtml += '         }});';
                                        scriptHtml += '         $("#comboFields").igCombo("value", newCol);';
                                        scriptHtml += '      </script>';
                                        scriptHtml += '  </div>';
                                        scriptHtml += '  <div style="margin-left: 5px; margin-top: 5px">';
                                        scriptHtml += '      <i class="fa fa-sort-alpha-asc" aria-hidden="true" style="margin-right: 5px;"></i><a onclick="orderColumn(1)" style="cursor: pointer">Ordem Crescente</a>';
                                        scriptHtml += '  </div>';
                                        scriptHtml += '  <div style="margin-left: 5px; margin-top: 5px">';
                                        scriptHtml += '      <i class="fa fa-sort-alpha-desc" aria-hidden="true" style="margin-right: 5px;"></i><a onclick="orderColumn(2)" style="cursor: pointer">Ordem Decrescente</a>';
                                        scriptHtml += '  </div>';
                                        scriptHtml += '  <div style="margin-left: 5px; margin-top: 5px">';
                                        scriptHtml += '      <i class="fa fa-eye-slash" aria-hidden="true" style="margin-right: 5px;"></i><a onclick="hideColumn()" style="cursor: pointer" id="showHideColumn">Ocultar Coluna</a>';
                                        scriptHtml += '  </div>';
                                        scriptHtml += '  <hr/>';
                                        scriptHtml += '  <div style="overflow: auto; max-height:100px" id="' + dgl[0].id + "_list" + '">';
                                        scriptHtml += listGrid;
                                        scriptHtml += '  </div>';
                                        scriptHtml += '  <hr/>';
                                        scriptHtml += '</div>';
                        
                                        $(scriptHtml).insertBefore(divDialog);
                                   },
                             },
                                    { name: 'Selection', mode: 'row'
                                    }, 
                                    { name: 'Tooltips', columnSettings:[{ columnKey: "IdLoja", allowTooltips: true },{ columnKey: "IntLoja", allowTooltips: true },{ columnKey: "BigIntLoja", allowTooltips: true },{ columnKey: "BitLoja", allowTooltips: true },{ columnKey: "IdCidade", allowTooltips: false },{ columnKey: "IdEstado", allowTooltips: false },{ columnKey: "IdPais", allowTooltips: false },{ columnKey: "NomeCidade", allowTooltips: false },{ columnKey: "StringPais", allowTooltips: false },{ columnKey: "StringLoja", allowTooltips: true },{ columnKey: "ComboboxLoja", allowTooltips: true },{ columnKey: "DatetimeLoja", allowTooltips: true },{ columnKey: "DecimalLoja", allowTooltips: true },{ columnKey: "SmallIntLoja", allowTooltips: true },{ columnKey: "StringEstado", allowTooltips: false }] },
                                    { name: 'Resizing' }, 
                                    { name: 'Hiding', 
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
                                      columnSettings: [{ columnKey: "IdCidade", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpCidade", isNullable: true, custom: vm.custom, vm: vm, verifyCanEditCol: verifyCanEditCol, allowMultiSelectionInSearch:true, activateAutoComplete: false, autoCompleteMaxResults: 7, validateOnClearState:false, maxValue:2147483647, maxLength: 10, defaultValue: null } }, { columnKey: "IdEstado", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpCidade", isNullable: true, custom: vm.custom, vm: vm, verifyCanEditCol: verifyCanEditCol, allowMultiSelectionInSearch:true, activateAutoComplete: false, autoCompleteMaxResults: 7, validateOnClearState:false, maxValue:2147483647, maxLength: 10, defaultValue: null } }, { columnKey: "IdPais", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpCidade", isNullable: true, custom: vm.custom, vm: vm, verifyCanEditCol: verifyCanEditCol, allowMultiSelectionInSearch:true, activateAutoComplete: false, autoCompleteMaxResults: 7, validateOnClearState:false, maxValue:2147483647, maxLength: 10, defaultValue: null } }, { columnKey: "NomeCidade", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpCidade", isNullable: true, custom: vm.custom, vm: vm, verifyCanEditCol: verifyCanEditCol, allowMultiSelectionInSearch:true, activateAutoComplete: false, autoCompleteMaxResults: 7, validateOnClearState:false, maxValue:null, maxLength: 50, defaultValue: '' } }, { columnKey: "StringPais", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpCidade", isNullable: true, custom: vm.custom, vm: vm, verifyCanEditCol: verifyCanEditCol, allowMultiSelectionInSearch:true, activateAutoComplete: false, autoCompleteMaxResults: 7, validateOnClearState:false, maxValue:null, maxLength: 50, defaultValue: '' } }, { columnKey: "StringEstado", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpCidade", isNullable: true, custom: vm.custom, vm: vm, verifyCanEditCol: verifyCanEditCol, allowMultiSelectionInSearch:true, activateAutoComplete: false, autoCompleteMaxResults: 7, validateOnClearState:false, maxValue:null, maxLength: 50, defaultValue: '' } }, { columnKey: 'DatetimeLoja', editorType: 'datepicker', editorOptions: {valueChanged: function(evt, ui){if (typeof vm.OnPropertyChangeDataGrid === 'function') {vm.OnPropertyChangeDataGrid('scyUIPaiFilhaNeta_dGrid', 'DatetimeLoja', ui.oldValue, ui.value);}}, minValue: new Date(1900, 0, 1), datepickerOptions: { changeMonth: true, changeYear: true }  } }, { columnKey: 'ComboboxLoja', editorType: 'combo', editorOptions: {   selectionChanged: function (evt, ui) {   var val = null;   if (ui.items != null && ui.items.length > 0) { val = ui.items[0].data['id']; }  updateEntity('ComboboxLoja', val, false); },  mode: 'dropdown', dropDownOnFocus: true,  dataSource: vm.dataDomains.getItems('LX_COMBOBOX_LOJA', ''),  textKey: 'name', valueKey: 'id', enableClearButton: false }}, { columnKey: "IdLoja" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('scyUIPaiFilhaNeta_dGrid', 'IdLoja', ui.oldValue, ui.value);}},  maxLength: 12, maxValue: null, dataMode: 'int' } }, { columnKey: "IntLoja" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('scyUIPaiFilhaNeta_dGrid', 'IntLoja', ui.oldValue, ui.value);}},  maxLength: 12, maxValue: null, minValue: 0, dataMode: 'int' } }, { columnKey: "BigIntLoja" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('scyUIPaiFilhaNeta_dGrid', 'BigIntLoja', ui.oldValue, ui.value);}},  maxLength: 24, maxValue: 9007199254740992, minValue: 0, dataMode: 'long' } }, { columnKey: "StringLoja" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('scyUIPaiFilhaNeta_dGrid', 'StringLoja', ui.oldValue, ui.value);}}, maxLength: 50 } }, { columnKey: "DecimalLoja" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('scyUIPaiFilhaNeta_dGrid', 'DecimalLoja', ui.oldValue, ui.value);}},  maxLength: 14, maxValue: 99999999999.99, minValue: 0, dataMode: 'decimal', minDecimals: 2, maxDecimals: 2 } }, { columnKey: "SmallIntLoja" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('scyUIPaiFilhaNeta_dGrid', 'SmallIntLoja', ui.oldValue, ui.value);}},  maxLength: 6, maxValue: null, minValue: 0, dataMode: 'short' } }],
                                      rowDeleting: function (evt, ui) {
                                          deletedIndex = ui.element.context.rowIndex;
                                          var entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowID);
                                          if (entity) {
                                              vm.deleteEntity(entity);
                                          }
                                      },
                                      rowDeleted: function (evt, ui) {
                                          var grid = $('#scyUIPaiFilhaNeta_dGrid');
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
                                         if (vm.status() === 'Q'){
                                             var gridCell = ui.owner.grid;
                                             grid.find('div.borderCell').remove();
                                             $(gridCell.cellAt(ui.columnIndex - 1, ui.owner._rowIndex)).append("<div class='borderCell' style='z-index:100; border: 1px solid #849fd9 !important;'></div>");
                                          }
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
                                                          grid.igGridUpdating('startEdit', rowId, indexColumn, true);
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
                                          var columns = $('#scyUIPaiFilhaNeta_dGrid').igGridUpdating('option', 'columnSettings');
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
                            vm.OnDataGridCreated('scyUIPaiFilhaNeta_dGrid');
                        }
                        var selectionrowselectionchanged = null, selectedRowId = -1;
                        selectionrowselectionchanged = function (evt, ui) {
                            if ((ui.owner.grid.selectedRow() && typeof ui.owner.grid.selectedRow().id !== 'undefined') || (ui.owner.grid.selectedRows() && ui.owner.grid.selectedRows().length > 0)) { 
                                if (isNullOrEmpty(ui.owner.selectedRows())|| ui.selectedRows.length <= 1) {
                                    $(document).undelegate('#scyUIPaiFilhaNeta_dGrid', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                                    ui.owner.clearSelection();
                                    ui.owner.selectRow(ui.row.index);
                                    if (vm.status() === 'Q'){
                                        var gridCell = ui.owner.grid;
                                        grid.find('div.borderCell').remove();
                                        //$(gridCell.cellAt(-1, ui.owner._rowIndex)).append(" < div class='borderCell' style='z-index:100; border: 1px solid #849fd9 !important;'></div>");
                                    }
                                    selectedRowId = ui.row.id;
                                    $(document).delegate ('#scyUIPaiFilhaNeta_dGrid', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                                }
                                selectGridCurrentItem(vm.goToKey, 'RowDataId', ui); 
                             } 
                        };
                        $(document).delegate('#scyUIPaiFilhaNeta_dGrid', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                        $('#scyUIPaiFilhaNeta_dGrid > tbody tr').live('focus', function(evt) {
                            var grid = $('#scyUIPaiFilhaNeta_dGrid'), row = $(this).closest('tr'), id = parseInt(row.attr('data-id'), 10);
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
                            if (vm.UIPaiFilhaNeta().status() === 'Q') vm.UIPaiFilhaNeta().dataToolbar.viewInfo();
                        });
                    }
                    vm.addDataSource({ key: 'scyUIPaiFilhaNeta_dGrid', name: 'dataView', itemsSource: itemsSource });
                }
            
        };
        
        return complement;
    }
    
    return complementCtor;
});
