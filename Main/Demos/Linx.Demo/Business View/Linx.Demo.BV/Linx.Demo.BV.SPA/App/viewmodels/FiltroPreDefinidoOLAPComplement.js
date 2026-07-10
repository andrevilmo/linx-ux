define(['managers/__auth', 'managers/user'], function (managerAuth, managerUser) {
    var complementCtor = function() {
        var complement = {
            isAutomatic: true
            , ChangedBrandscyFiltroPreDefinidoOLAP_dGrid: function(vm, decimals, reset) {
                var i, format = '0.'.concat('0'.repeat(decimals)), grd =$('#scyFiltroPreDefinidoOLAP_dGrid').data('igGrid'),
                    grdUpd = $('#scyFiltroPreDefinidoOLAP_dGrid').data('igGridUpdating');
                if(isNull(grd) || isNull(grdUpd)) return;
                for (i = 0; i < grd.options.columns.length; i++) {
                }
                for (i = 0; i < grdUpd.options.columnSettings.length; i++) {
                }
                grd.dataBind();
            }
            , renderscyFiltroPreDefinidoOLAP_dGrid: function(vm) {
                var getDataSource = function() {
                    var source = null;
                    try {
                        source = vm.dataView;
                    }
                    catch (e) { }
                    return isNullOrEmpty(source) ? ko.observableArray([]) : source;
                };
                $('#scyFiltroPreDefinidoOLAP_dGrid_headers').live('focus  keydown', function (evt) {
                    var keyCode = window.event ? evt.which : evt.keyCode;
                    if (keyCode === 9) {
                        var cols = $('#scyFiltroPreDefinidoOLAP_dGrid').igGrid('option', 'columns');
                        var dataView = $('#scyFiltroPreDefinidoOLAP_dGrid').data('igGrid').dataSource._dataView
                        if (dataView.length === 0) return;
                        var firstRow = dataView[0].RowDataId;
                        clear = vm.status() === 'C';
                        if (vm.status() === 'C')
                            $('#scyFiltroPreDefinidoOLAP_dGrid').igGridUpdating('startEdit', firstRow, 0, true);
                        else {
                            var entity = findElementByKey(getDataSource(), 'RowDataId', firstRow);
                            var indexColumn = 0;
                            cols.some(function (entry) {
                                if (entry.key !== 'RowDataId' && !entry.hidden) {
                                    if (verifyCanEditCol(entry.key, clear, entity)) {
                                        $('#scyFiltroPreDefinidoOLAP_dGrid').igGridUpdating('startEdit', firstRow, indexColumn, true);
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
                   if($('#scyFiltroPreDefinidoOLAP_dGrid').data('igGrid') === undefined) return '';
                   var cols = $('#scyFiltroPreDefinidoOLAP_dGrid').igGrid('option', 'columns');
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
                  if (!grid) grid = $('#scyFiltroPreDefinidoOLAP_dGrid');
                  return ((!grid[0] || (!forceCreating && grid.parent().width() <= 0)) && !$('#dialog').is(':visible'));
                }
                var refreshData = true;
                var itemsSource = { isElementHided: isElementHided, getVisibleColumns: getVisibleColumns, containerId: 'scyFiltroPreDefinidoOLAP_dGrid_container', dataBind: function (commitData, forceCreating) {
                   var grid = $('#scyFiltroPreDefinidoOLAP_dGrid');
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
                       $('#scyFiltroPreDefinidoOLAP_dGrid_groupbyarea').addClass('hide');
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
                        case 'IdBandeiraRede': { canEditing = clear; break;}
                        case 'Loja': { canEditing = clear; break;}
                        case 'CodLoja': { canEditing = clear; break;}
                        case 'Cliente': { canEditing = clear; break;}
                        case 'Data': { canEditing = clear; break;}
                        case 'QtdItemBruto': { canEditing = clear; break;}
                        case 'VlrItemPago': { canEditing = clear; break;}
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
                            $('#scyFiltroPreDefinidoOLAP_dGrid_LayoutBtn').igPopover('hide');
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
                                      showMultimidia(entity, e, table, key, vm.FiltroPreDefinidoOLAP());
                                 }
                             }
                             if (typeof vm.OnGridClientClick === 'function') {
                                 entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowKey);
                                 vm.OnGridClientClick('scyFiltroPreDefinidoOLAP_dGrid', ui.colKey, entity);
                             }
                             if (vm.status() != 'Q') {
                                 var grid = $('#scyFiltroPreDefinidoOLAP_dGrid');
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
                            { key: 'IdBandeiraRede', headerText: vm.getLayoutHeaderGrid('IdBandeiraRede'), width: '0px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'Loja', headerText: vm.getLayoutHeaderGrid('Loja'), width: '0px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'CodLoja', headerText: vm.getLayoutHeaderGrid('CodLoja'), width: '0px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'Cliente', headerText: vm.getLayoutHeaderGrid('Cliente'), width: '0px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'Data', headerText: vm.getLayoutHeaderGrid('Data'), width: '0px', dataType: 'date', columnCssClass: 'ellipsis', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null   },
                            { key: 'QtdItemBruto', headerText: vm.getLayoutHeaderGrid('QtdItemBruto'), width: '0px', dataType: 'number', columnCssClass: 'ellipsis', format: '0.00', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                            { key: 'VlrItemPago', headerText: vm.getLayoutHeaderGrid('VlrItemPago'), width: '0px', dataType: 'number', columnCssClass: 'ellipsis', format: '0.00', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  }
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
                        
                                        var dataView = $('#scyFiltroPreDefinidoOLAP_dGrid').data('igGrid').dataSource;
                                        if (dataView.settings.filtering.expressions.length <= 0)
                                            dataView._filteredData = [];
                        
                                        var listGrid = '';
                                        var col = ui.owner._dialogCurrentColumn;
                        
                                        var reloadList = function (col) {
                                             var grid = $('#scyFiltroPreDefinidoOLAP_dGrid');
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
                                        scriptHtml += '    var newGrid = $("#scyFiltroPreDefinidoOLAP_dGrid");';
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
                                        scriptHtml += '            $("#scyFiltroPreDefinidoOLAP_dGrid_container_dialog_list").html(newList)';
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
                                    { name: 'Tooltips', columnSettings:[{ columnKey: "IdBandeiraRede", allowTooltips: false },{ columnKey: "Loja", allowTooltips: false },{ columnKey: "CodLoja", allowTooltips: false },{ columnKey: "Cliente", allowTooltips: false },{ columnKey: "Data", allowTooltips: true },{ columnKey: "QtdItemBruto", allowTooltips: true },{ columnKey: "VlrItemPago", allowTooltips: true }] },
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
                                      columnSettings: [{ columnKey: "IdBandeiraRede", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpEntityAdapter1IdBandeiraRede", isNullable: false, custom: vm.custom, vm: vm, verifyCanEditCol: verifyCanEditCol, allowMultiSelectionInSearch:true, validateOnClearState:false, maxValue:9007199254740992, maxLength: 0, defaultValue: 0 } }, { columnKey: "Loja", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpEntityAdapter1Loja", isNullable: true, custom: vm.custom, vm: vm, verifyCanEditCol: verifyCanEditCol, allowMultiSelectionInSearch:true, validateOnClearState:false, maxValue:null, maxLength: 0, defaultValue: '' } }, { columnKey: "CodLoja", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpEntityAdapter1CodLoja", isNullable: true, custom: vm.custom, vm: vm, verifyCanEditCol: verifyCanEditCol, allowMultiSelectionInSearch:true, validateOnClearState:false, maxValue:null, maxLength: 0, defaultValue: '' } }, { columnKey: "Cliente", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpEntityAdapter1Cliente", isNullable: true, custom: vm.custom, vm: vm, verifyCanEditCol: verifyCanEditCol, allowMultiSelectionInSearch:true, validateOnClearState:false, maxValue:null, maxLength: 0, defaultValue: '' } }, { columnKey: 'Data', editorType: 'datepicker', editorOptions: {valueChanged: function(evt, ui){if (typeof vm.OnPropertyChangeDataGrid === 'function') {vm.OnPropertyChangeDataGrid('scyFiltroPreDefinidoOLAP_dGrid', 'Data', ui.oldValue, ui.value);}}, minValue: new Date(1900, 0, 1), datepickerOptions: { changeMonth: true, changeYear: true }  } }, { columnKey: "QtdItemBruto" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('scyFiltroPreDefinidoOLAP_dGrid', 'QtdItemBruto', ui.oldValue, ui.value);}},  maxLength: 21, maxValue: 999999999999999999.99, minValue: 0, dataMode: 'double', minDecimals: 2, maxDecimals: 2 } }, { columnKey: "VlrItemPago" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('scyFiltroPreDefinidoOLAP_dGrid', 'VlrItemPago', ui.oldValue, ui.value);}},  maxLength: 21, maxValue: 999999999999999999.99, minValue: 0, dataMode: 'double', minDecimals: 2, maxDecimals: 2 } }],
                                      rowDeleting: function (evt, ui) {
                                          deletedIndex = ui.element.context.rowIndex;
                                          var entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowID);
                                          if (entity) {
                                              vm.deleteEntity(entity);
                                          }
                                      },
                                      rowDeleted: function (evt, ui) {
                                          var grid = $('#scyFiltroPreDefinidoOLAP_dGrid');
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
                                          var columns = $('#scyFiltroPreDefinidoOLAP_dGrid').igGridUpdating('option', 'columnSettings');
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
                            vm.OnDataGridCreated('scyFiltroPreDefinidoOLAP_dGrid');
                        }
                        var selectionrowselectionchanged = null, selectedRowId = -1;
                        selectionrowselectionchanged = function (evt, ui) {
                            if ((ui.owner.grid.selectedRow() && typeof ui.owner.grid.selectedRow().id !== 'undefined') || (ui.owner.grid.selectedRows() && ui.owner.grid.selectedRows().length > 0)) { 
                                if (isNullOrEmpty(ui.owner.selectedRows())|| ui.selectedRows.length <= 1) {
                                    $(document).undelegate('#scyFiltroPreDefinidoOLAP_dGrid', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                                    ui.owner.clearSelection();
                                    ui.owner.selectRow(ui.row.index);
                                    if (vm.status() === 'Q'){
                                        var gridCell = ui.owner.grid;
                                        grid.find('div.borderCell').remove();
                                        //$(gridCell.cellAt(-1, ui.owner._rowIndex)).append(" < div class='borderCell' style='z-index:100; border: 1px solid #849fd9 !important;'></div>");
                                    }
                                    selectedRowId = ui.row.id;
                                    $(document).delegate ('#scyFiltroPreDefinidoOLAP_dGrid', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                                }
                                selectGridCurrentItem(vm.goToKey, 'RowDataId', ui); 
                             } 
                        };
                        $(document).delegate('#scyFiltroPreDefinidoOLAP_dGrid', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                        $('#scyFiltroPreDefinidoOLAP_dGrid > tbody tr').live('focus', function(evt) {
                            var grid = $('#scyFiltroPreDefinidoOLAP_dGrid'), row = $(this).closest('tr'), id = parseInt(row.attr('data-id'), 10);
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
                            if (vm.FiltroPreDefinidoOLAP().status() === 'Q') vm.FiltroPreDefinidoOLAP().dataToolbar.viewInfo();
                        });
                    }
                    vm.addDataSource({ key: 'scyFiltroPreDefinidoOLAP_dGrid', name: 'dataView', itemsSource: itemsSource });
                }
            
        };
        
        return complement;
    }
    
    return complementCtor;
});
