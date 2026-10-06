define(['managers/__auth', 'managers/user'], function (managerAuth, managerUser) {
    var complementCtor = function() {
        var complement = {
            isAutomatic: true
            , ChangedBrandConsultaMensagem_dGridTcsMensagemConsultaLog: function(vm, decimals, reset) {
                var i, format = '0.'.concat('0'.repeat(decimals)), grd =$('#ConsultaMensagem_dGridTcsMensagemConsultaLog').data('igGrid'),
                    grdUpd = $('#ConsultaMensagem_dGridTcsMensagemConsultaLog').data('igGridUpdating');
                if(isNull(grd) || isNull(grdUpd)) return;
                for (i = 0; i < grd.options.columns.length; i++) {
                }
                for (i = 0; i < grdUpd.options.columnSettings.length; i++) {
                }
                grd.dataBind();
            }
            , renderConsultaMensagem_dGridTcsMensagemConsultaLog: function(vm) {
                var getDataSource = function() {
                    var source = null;
                    try {
                        source = vm.currentDataItem().TcsMensagemConsultaLogList;
                    }
                    catch (e) { }
                    return isNullOrEmpty(source) ? ko.observableArray([]) : source;
                };
                var dataSourceIsLoaded = function() {
                    var isLoaded = false;
                    try {
                        isLoaded = (vm.currentDataItem().TcsMensagemConsultaLogIsLoaded === true || vm.currentDataItem().TcsMensagemConsultaLogList().length > 0);
                    }
                    catch (e) {
                        isLoaded = true;
                    }
                    return isLoaded;
                }
                $('#ConsultaMensagem_dGridTcsMensagemConsultaLog_headers').live('focus  keydown', function (evt) {
                    var keyCode = window.event ? evt.which : evt.keyCode;
                    if (keyCode === 9) {
                        var cols = $('#ConsultaMensagem_dGridTcsMensagemConsultaLog').igGrid('option', 'columns');
                        var dataView = $('#ConsultaMensagem_dGridTcsMensagemConsultaLog').data('igGrid').dataSource._dataView
                        if (dataView.length === 0) return;
                        var firstRow = dataView[0].RowDataId;
                        clear = vm.status() === 'C';
                        if (vm.status() === 'C')
                            $('#ConsultaMensagem_dGridTcsMensagemConsultaLog').igGridUpdating('startEdit', firstRow, 0, true);
                        else {
                            var entity = findElementByKey(getDataSource(), 'RowDataId', firstRow);
                            var indexColumn = 0;
                            cols.some(function (entry) {
                                if (entry.key !== 'RowDataId' && !entry.hidden) {
                                    if (verifyCanEditCol(entry.key, clear, entity)) {
                                        $('#ConsultaMensagem_dGridTcsMensagemConsultaLog').igGridUpdating('startEdit', firstRow, indexColumn, true);
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
                   if($('#ConsultaMensagem_dGridTcsMensagemConsultaLog').data('igGrid') === undefined) return '';
                   var cols = $('#ConsultaMensagem_dGridTcsMensagemConsultaLog').igGrid('option', 'columns');
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
                  if (!grid) grid = $('#ConsultaMensagem_dGridTcsMensagemConsultaLog');
                  return ((!grid[0] || (!forceCreating && grid.parent().width() <= 0)) && !$('#dialogTcsMensagemConsultaLog').is(':visible'));
                }
                var refreshData = true;
                var itemsSource = { isElementHided: isElementHided, getVisibleColumns: getVisibleColumns, containerId: 'ConsultaMensagem_dGridTcsMensagemConsultaLog_container', dataBind: function (commitData, forceCreating) {
                   var grid = $('#ConsultaMensagem_dGridTcsMensagemConsultaLog');
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
                       $('#ConsultaMensagem_dGridTcsMensagemConsultaLog_groupbyarea').addClass('hide');
                   }
                   if (grid.igGridUpdating('isEditing')) {
                        grid.igGridUpdating('endEdit', true);
                   }
                   if (execFillDetais) {
                     grid.igGrid("option", "dataSource", []);
                     vm.currentDataItem().fillDetails(false, 'TcsMensagemConsultaLog');
                     return;
                   }
                   grid.igGrid("option", "dataSource", unwrapObservableArray(getDataSource(), vm));
                   var rows = grid.igGrid('allRows');
                   if (rows.length > 0) {
                     var verticalContainer = grid.igGrid('scrollContainer');
                     var isSelected = false;
                     if (vm.currentDataItem().currentTcsMensagemConsultaLog() != null)
                     {
                       for(var idx = 0; idx < rows.length; idx++)
                       {
                         if (rows[idx].dataset.id == getAbsoluteValue(vm.currentDataItem().currentTcsMensagemConsultaLog().RowDataId))
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
                     if ($('#dialogTcsMensagemConsultaLog').is(':visible')) {
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
                           $('label#currentNumberTcsMensagemConsultaLog').html(current + ' - ' + totalCurrentPage);
                        }
                        else
                           $('label#currentNumberTcsMensagemConsultaLog').html(1);
                        $('label#totalNumberTcsMensagemConsultaLog').html(totalGrid);
                    }
                   } else {
                       $('label#currentNumberTcsMensagemConsultaLog').html(0);
                       $('label#totalNumberTcsMensagemConsultaLog').html(0);
                   }
                }};
                var valueGrouBy = -1;
                var deletedIndex = -1;
                function verifyCanEditCol(column, clear, entity){
                    switch(column){
                        case 'NomeUsuario': { canEditing = clear; break;}
                        case 'Entregue': { canEditing = clear; break;}
                        case 'Lida': { canEditing = clear; break;}
                        case 'Dispensada': { canEditing = clear; break;}
                    }
                    return canEditing;
                };
                function createDataGrid(grid) {
                    var gridId = grid[0].id;
                    vm.gridSaveStates[gridId] = {
                        savedLayouts: typeof vm.gridSaveStates[gridId] === 'object' ? vm.gridSaveStates[gridId].savedLayouts: ko.observableArray([]),
                        currentLayout: typeof vm.gridSaveStates[gridId] === 'object' ? vm.gridSaveStates[gridId].currentLayout : ko.observable({ Id: 0 }),
                        currentLayoutId: typeof vm.gridSaveStates[gridId] === 'object' ? vm.gridSaveStates[gridId].currentLayoutId : ko.observable(0),
                        __applyLayout: function (jsonContent) {
                            this.gridSaveStates.returnToSavedState(jsonContent);
                            vm.dataToolbar.isBusy(false);
                            this.closePopover();
                        },
                        closePopover: function () {
                            $('#ConsultaMensagem_dGridTcsMensagemConsultaLog_LayoutBtn').igPopover('hide');
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
                                        _this.loadLayouts(true).then(function () {
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
                        loadLayouts: function (force) {
                            var dfd = $.Deferred(), _this = this;
                            if (force || _this.savedLayouts().length === 0) {
                                 managerUser.getAllGridLayouts(vm.__moduleId__, gridId).then(function (results) {
                                     _this.savedLayouts(results);
                                     _this.savedLayouts.splice(0, 0, _this.defaultLayout);
                                     dfd.resolve();
                                 });
                            } else {
                                 dfd.resolve();
                            }
                            return dfd;
                        },
                        deleteLayout: function () {
                            var _this = this;
                            return vm.app.showMessage('Deseja realmente excluir o Layout [' + _this.currentLayout().NomeLayout + ']?', 'Alerta', ['Yes', 'No'])
                            .then(function (selectedOption) {
                                if (selectedOption === 'Yes') {
                                    managerUser.deleteGridLayout(_this.currentLayout().Id, _this.currentLayout().Modulo, _this.currentLayout().NomeObjeto).then(function () {
                                        vm.app.showMessage('Excluido com sucesso!', 'Alerta');
                                        _this.loadLayouts(true).then(function () {
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
                                      showMultimidia(entity, e, table, key, vm.ConsultaMensagem());
                                 }
                             }
                             if (typeof vm.OnGridClientClick === 'function') {
                                 entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowKey);
                                 vm.OnGridClientClick('ConsultaMensagem_dGridTcsMensagemConsultaLog', ui.colKey, entity);
                             }
                             if (vm.status() != 'Q') {
                                 var grid = $('#ConsultaMensagem_dGridTcsMensagemConsultaLog');
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
                            setTimeout(function() { $('#' + ui.owner.id() + '_headers>thead>tr>th').each(function(i, item) { if (item.attributes['aria-label']) { item.attributes['title'].value = item.attributes['aria-label'].value; } }); 
                            if (vm.gridSaveStates[ui.owner.id()].currentLayout().Id !== 0) {
                                 vm.gridSaveStates[ui.owner.id()].applyLayout(vm.gridSaveStates[ui.owner.id()].currentLayout());
                            }
                            }, 500);
                            $('.ui-icon-gear').remove();
                        },
                        dataRendered: function(evt, ui) { 
                        },
                        columns: [
                            { key: 'RowDataId', headerText: 'RowDataId', width: '50px', dataType: 'number', hidden: true },
                            { key: 'NomeUsuario', headerText: vm.getLayoutHeaderGrid('ConsultaMensagem_lUpTcsMensagemConsultaLog_NomeUsuario'), width: '400px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'Entregue', headerText: vm.getLayoutHeaderGrid('ConsultaMensagem_dtTcsMensagemConsultaLog_Entregue'), width: '150px', dataType: 'date', columnCssClass: 'ellipsis', format: 'dd/MM/yyyy HH:mm', hidden: false, unbound: false, group: null   },
                            { key: 'Lida', headerText: vm.getLayoutHeaderGrid('ConsultaMensagem_dtTcsMensagemConsultaLog_Lida'), width: '150px', dataType: 'date', columnCssClass: 'ellipsis', format: 'dd/MM/yyyy HH:mm', hidden: false, unbound: false, group: null   },
                            { key: 'Dispensada', headerText: vm.getLayoutHeaderGrid('ConsultaMensagem_dtTcsMensagemConsultaLog_Dispensada'), width: '150px', dataType: 'date', columnCssClass: 'ellipsis', format: 'dd/MM/yyyy HH:mm', hidden: false, unbound: false, group: null   }
                        ],
                        features: [
                                    { name: 'Sorting', type: 'local', caseSensitive: false, unsortedColumnTooltip: '', sortedColumnTooltip: '',
                                      columnSorting: function (evt, ui) { }
                                      , customSortFunction: function (data, fields, direction) { return gridFunctions.sort(data, fields, direction); }
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
                        
                                        var dataView = $('#ConsultaMensagem_dGridTcsMensagemConsultaLog').data('igGrid').dataSource;
                                        if (dataView.settings.filtering.expressions.length <= 0)
                                            dataView._filteredData = [];
                        
                                        var col = ui.owner._dialogCurrentColumn;
                        
                                        var divDialog = $('#' + dgl[0].id).find('.ui-iggrid-filterdialogaddcondition').find('span')[0];
                        
                                        var scriptHtml = '<div id="' + divDinamica + '">';
                                        scriptHtml += '  <script>';
                                        scriptHtml += '    var newCol = "' + col + '";';
                                        scriptHtml += '    var newGrid = $("#ConsultaMensagem_dGridTcsMensagemConsultaLog");';
                                        scriptHtml += '    var listFilter = [];';
                                        scriptHtml += '    function hideColumn(){';
                                        scriptHtml += '     if ($("#showHideColumn_ConsultaMensagem_dGridTcsMensagemConsultaLog")["0"].innerHTML.indexOf("Ocultar") >= 0) {';
                                        scriptHtml += '        $("#showHideColumn_ConsultaMensagem_dGridTcsMensagemConsultaLog")["0"].innerHTML = "Mostrar Coluna";';
                                        scriptHtml += '        newGrid.igGridHiding("hideColumn", newCol);';
                                        scriptHtml += '     }';
                                        scriptHtml += '     else{';
                                        scriptHtml += '        $("#showHideColumn_ConsultaMensagem_dGridTcsMensagemConsultaLog")["0"].innerHTML = "Ocultar Coluna";';
                                        scriptHtml += '        newGrid.igGridHiding("showColumn", newCol);';
                                        scriptHtml += '     }';
                                        scriptHtml += '    }';
                                        scriptHtml += '    function updateHideButton(){';
                                        scriptHtml += '         var column = $.grep(newGrid.igGrid("option", "columns"), function (element, index) { return element.key == newCol });';
                                        scriptHtml += '         if (column.length > 0){';
                                        scriptHtml += '             $("#showHideColumn_ConsultaMensagem_dGridTcsMensagemConsultaLog")["0"].innerHTML = column[0].hidden ? "Mostrar Coluna" : "Ocultar Coluna"';
                                        scriptHtml += '         }';
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
                                        scriptHtml += '                     break;';
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
                                        scriptHtml += '  <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">';
                                        scriptHtml += '     <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">';
                                        scriptHtml += '          <div  style="margin-left: 5px" >';
                                        scriptHtml += '              <div>Propriedade:</div>';
                                        scriptHtml += '              <div id="comboFields_ConsultaMensagem_dGridTcsMensagemConsultaLog"></div>';
                                        scriptHtml += '              <script>';
                                        scriptHtml += '                  var columns = newGrid.igGrid("option", "columns");';
                                        scriptHtml += '                  $("#comboFields_ConsultaMensagem_dGridTcsMensagemConsultaLog").igCombo({ dataSource: columns, mode : "dropdown", valueKey: "key", textKey: "headerText", selectionChanging: function (evt, ui) {';
                                        scriptHtml += '                      newCol = ui.items["0"].data.key;';
                                        scriptHtml += '                      updateHideButton()';
                                        scriptHtml += '                  }});';
                                        scriptHtml += '                  $("#comboFields_ConsultaMensagem_dGridTcsMensagemConsultaLog").igCombo("value", newCol);';
                                        scriptHtml += '              </script>';
                                        scriptHtml += '          </div>';
                                        scriptHtml += '      </div>';
                                        scriptHtml += '     <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">';
                                        scriptHtml += '          <div style="margin-left: 5px; margin-top: 5px">';
                                        scriptHtml += '              <i class="fa fa-sort-alpha-asc" aria-hidden="true" style="margin-right: 5px;"></i><a onclick="orderColumn(1)" style="cursor: pointer">Ordem Crescente</a>';
                                        scriptHtml += '          </div>';
                                        scriptHtml += '          <div style="margin-left: 5px; margin-top: 5px">';
                                        scriptHtml += '              <i class="fa fa-sort-alpha-desc" aria-hidden="true" style="margin-right: 5px;"></i><a onclick="orderColumn(2)" style="cursor: pointer">Ordem Decrescente</a>';
                                        scriptHtml += '          </div>';
                                        scriptHtml += '          <div style="margin-left: 5px; margin-top: 5px">';
                                        scriptHtml += '              <i class="fa fa-eye-slash" aria-hidden="true" style="margin-right: 5px;"></i><a onclick="hideColumn()" style="cursor: pointer" id="showHideColumn_ConsultaMensagem_dGridTcsMensagemConsultaLog">Ocultar Coluna</a>';
                                        scriptHtml += '          </div>';
                                        scriptHtml += '          <br>';
                                        scriptHtml += '      </div>';
                                        scriptHtml += '  </div>';
                        
                                        $(scriptHtml).insertBefore(divDialog);
                                   },
                             },
                                    { name: 'Selection', mode: 'row'
                                    }, 
                                    { name: 'Tooltips', columnSettings:[{ columnKey: "NomeUsuario", allowTooltips: false },{ columnKey: "Entregue", allowTooltips: true },{ columnKey: "Lida", allowTooltips: true },{ columnKey: "Dispensada", allowTooltips: true }] },
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
                                      columnSettings: [{ columnKey: "NomeUsuario", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpTcsUsuarioAutenticacaoCL", isNullable: false, custom: vm.custom, vm: vm, verifyCanEditCol: verifyCanEditCol, allowMultiSelectionInSearch:true, activateAutoComplete: false, autoCompleteMaxResults: 7, validateOnClearState:false, maxValue:null, maxLength: 250, defaultValue: '' } }, { columnKey: 'Entregue', editorType: 'datepicker', editorOptions: {valueChanged: function(evt, ui){if (typeof vm.OnPropertyChangeDataGrid === 'function') {vm.OnPropertyChangeDataGrid('ConsultaMensagem_dGridTcsMensagemConsultaLog', 'Entregue', ui.oldValue, ui.value);}}, minValue: new Date(1900, 0, 1), datepickerOptions: { changeMonth: true, changeYear: true }  } }, { columnKey: 'Lida', editorType: 'datepicker', editorOptions: {valueChanged: function(evt, ui){if (typeof vm.OnPropertyChangeDataGrid === 'function') {vm.OnPropertyChangeDataGrid('ConsultaMensagem_dGridTcsMensagemConsultaLog', 'Lida', ui.oldValue, ui.value);}}, minValue: new Date(1900, 0, 1), datepickerOptions: { changeMonth: true, changeYear: true }  } }, { columnKey: 'Dispensada', editorType: 'datepicker', editorOptions: {valueChanged: function(evt, ui){if (typeof vm.OnPropertyChangeDataGrid === 'function') {vm.OnPropertyChangeDataGrid('ConsultaMensagem_dGridTcsMensagemConsultaLog', 'Dispensada', ui.oldValue, ui.value);}}, minValue: new Date(1900, 0, 1), datepickerOptions: { changeMonth: true, changeYear: true }  } }],
                                      rowDeleting: function (evt, ui) {
                                          deletedIndex = ui.element.context.rowIndex;
                                          var entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowID);
                                          if (entity) {
                                              vm.deleteEntity(entity);
                                          }
                                      },
                                      rowDeleted: function (evt, ui) {
                                          var grid = $('#ConsultaMensagem_dGridTcsMensagemConsultaLog');
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
                                          var columns = $('#ConsultaMensagem_dGridTcsMensagemConsultaLog').igGridUpdating('option', 'columnSettings');
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
                                                     vm.dataCombo.fillDataCombos(lookUpName, ui.columnKey, vm.currentDataItem().currentTcsMensagemConsultaLog(), function (result) {
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
                            vm.OnDataGridCreated('ConsultaMensagem_dGridTcsMensagemConsultaLog');
                        }
                        var selectionrowselectionchanged = null, selectedRowId = -1;
                        selectionrowselectionchanged = function (evt, ui) {
                            if ((ui.owner.grid.selectedRow() && typeof ui.owner.grid.selectedRow().id !== 'undefined') || (ui.owner.grid.selectedRows() && ui.owner.grid.selectedRows().length > 0)) { 
                                if (isNullOrEmpty(ui.owner.selectedRows())|| ui.selectedRows.length <= 1) {
                                    $(document).undelegate('#ConsultaMensagem_dGridTcsMensagemConsultaLog', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                                    ui.owner.clearSelection();
                                    ui.owner.selectRow(ui.row.index);
                                    if (vm.status() === 'Q'){
                                        var gridCell = ui.owner.grid;
                                        grid.find('div.borderCell').remove();
                                        //$(gridCell.cellAt(-1, ui.owner._rowIndex)).append(" < div class='borderCell' style='z-index:100; border: 1px solid #849fd9 !important;'></div>");
                                    }
                                    selectedRowId = ui.row.id;
                                    $(document).delegate ('#ConsultaMensagem_dGridTcsMensagemConsultaLog', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                                }
                                selectGridCurrentItem(vm.goToKey, 'RowDataId', ui, vm.currentDataItem().currentTcsMensagemConsultaLog, getDataSource()); 
                             } 
                        };
                        $(document).delegate('#ConsultaMensagem_dGridTcsMensagemConsultaLog', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                        $('#ConsultaMensagem_dGridTcsMensagemConsultaLog > tbody tr').live('focus', function(evt) {
                            var grid = $('#ConsultaMensagem_dGridTcsMensagemConsultaLog'), row = $(this).closest('tr'), id = parseInt(row.attr('data-id'), 10);
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
                    vm.addDataSource({ key: 'ConsultaMensagem_dGridTcsMensagemConsultaLog', name: 'TcsMensagemConsultaLogList', itemsSource: itemsSource });
                }
            
            
            , ChangedBrandscyConsultaMensagem_dGrid: function(vm, decimals, reset) {
                var i, format = '0.'.concat('0'.repeat(decimals)), grd =$('#scyConsultaMensagem_dGrid').data('igGrid'),
                    grdUpd = $('#scyConsultaMensagem_dGrid').data('igGridUpdating');
                if(isNull(grd) || isNull(grdUpd)) return;
                for (i = 0; i < grd.options.columns.length; i++) {
                }
                for (i = 0; i < grdUpd.options.columnSettings.length; i++) {
                }
                grd.dataBind();
            }
            , renderscyConsultaMensagem_dGrid: function(vm) {
                var getDataSource = function() {
                    var source = null;
                    try {
                        source = vm.dataView;
                    }
                    catch (e) { }
                    return isNullOrEmpty(source) ? ko.observableArray([]) : source;
                };
                $('#scyConsultaMensagem_dGrid_headers').live('focus  keydown', function (evt) {
                    var keyCode = window.event ? evt.which : evt.keyCode;
                    if (keyCode === 9) {
                        var cols = $('#scyConsultaMensagem_dGrid').igGrid('option', 'columns');
                        var dataView = $('#scyConsultaMensagem_dGrid').data('igGrid').dataSource._dataView
                        if (dataView.length === 0) return;
                        var firstRow = dataView[0].RowDataId;
                        clear = vm.status() === 'C';
                        if (vm.status() === 'C')
                            $('#scyConsultaMensagem_dGrid').igGridUpdating('startEdit', firstRow, 0, true);
                        else {
                            var entity = findElementByKey(getDataSource(), 'RowDataId', firstRow);
                            var indexColumn = 0;
                            cols.some(function (entry) {
                                if (entry.key !== 'RowDataId' && !entry.hidden) {
                                    if (verifyCanEditCol(entry.key, clear, entity)) {
                                        $('#scyConsultaMensagem_dGrid').igGridUpdating('startEdit', firstRow, indexColumn, true);
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
                   if($('#scyConsultaMensagem_dGrid').data('igGrid') === undefined) return '';
                   var cols = $('#scyConsultaMensagem_dGrid').igGrid('option', 'columns');
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
                  if (!grid) grid = $('#scyConsultaMensagem_dGrid');
                  return ((!grid[0] || (!forceCreating && grid.parent().width() <= 0)) && !$('#dialog').is(':visible'));
                }
                var refreshData = true;
                var itemsSource = { isElementHided: isElementHided, getVisibleColumns: getVisibleColumns, containerId: 'scyConsultaMensagem_dGrid_container', dataBind: function (commitData, forceCreating) {
                   var grid = $('#scyConsultaMensagem_dGrid');
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
                       $('#scyConsultaMensagem_dGrid_groupbyarea').addClass('hide');
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
                        case 'Titulo': { canEditing = clear; break;}
                        case 'LxTipoMensagem': { canEditing = clear; break;}
                        case 'Criacao': { canEditing = clear; break;}
                        case 'Envio': { canEditing = clear; break;}
                        case 'NomeEmpresa': { canEditing = clear; break;}
                        case 'NomeUsuario': { canEditing = clear; break;}
                    }
                    return canEditing;
                };
                function createDataGrid(grid) {
                    var gridId = grid[0].id;
                    vm.gridSaveStates[gridId] = {
                        savedLayouts: typeof vm.gridSaveStates[gridId] === 'object' ? vm.gridSaveStates[gridId].savedLayouts: ko.observableArray([]),
                        currentLayout: typeof vm.gridSaveStates[gridId] === 'object' ? vm.gridSaveStates[gridId].currentLayout : ko.observable({ Id: 0 }),
                        currentLayoutId: typeof vm.gridSaveStates[gridId] === 'object' ? vm.gridSaveStates[gridId].currentLayoutId : ko.observable(0),
                        __applyLayout: function (jsonContent) {
                            this.gridSaveStates.returnToSavedState(jsonContent);
                            vm.dataToolbar.isBusy(false);
                            this.closePopover();
                        },
                        closePopover: function () {
                            $('#scyConsultaMensagem_dGrid_LayoutBtn').igPopover('hide');
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
                                        _this.loadLayouts(true).then(function () {
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
                        loadLayouts: function (force) {
                            var dfd = $.Deferred(), _this = this;
                            if (force || _this.savedLayouts().length === 0) {
                                 managerUser.getAllGridLayouts(vm.__moduleId__, gridId).then(function (results) {
                                     _this.savedLayouts(results);
                                     _this.savedLayouts.splice(0, 0, _this.defaultLayout);
                                     dfd.resolve();
                                 });
                            } else {
                                 dfd.resolve();
                            }
                            return dfd;
                        },
                        deleteLayout: function () {
                            var _this = this;
                            return vm.app.showMessage('Deseja realmente excluir o Layout [' + _this.currentLayout().NomeLayout + ']?', 'Alerta', ['Yes', 'No'])
                            .then(function (selectedOption) {
                                if (selectedOption === 'Yes') {
                                    managerUser.deleteGridLayout(_this.currentLayout().Id, _this.currentLayout().Modulo, _this.currentLayout().NomeObjeto).then(function () {
                                        vm.app.showMessage('Excluido com sucesso!', 'Alerta');
                                        _this.loadLayouts(true).then(function () {
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
                                      showMultimidia(entity, e, table, key, vm.ConsultaMensagem());
                                 }
                             }
                             if (typeof vm.OnGridClientClick === 'function') {
                                 entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowKey);
                                 vm.OnGridClientClick('scyConsultaMensagem_dGrid', ui.colKey, entity);
                             }
                             if (vm.status() != 'Q') {
                                 var grid = $('#scyConsultaMensagem_dGrid');
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
                            setTimeout(function() { $('#' + ui.owner.id() + '_headers>thead>tr>th').each(function(i, item) { if (item.attributes['aria-label']) { item.attributes['title'].value = item.attributes['aria-label'].value; } }); 
                            if (vm.gridSaveStates[ui.owner.id()].currentLayout().Id !== 0) {
                                 vm.gridSaveStates[ui.owner.id()].applyLayout(vm.gridSaveStates[ui.owner.id()].currentLayout());
                            }
                            }, 500);
                            $('.ui-icon-gear').remove();
                        },
                        dataRendered: function(evt, ui) { 
                        },
                        columns: [
                            { key: 'RowDataId', headerText: 'RowDataId', width: '50px', dataType: '', hidden: true },
                            { key: 'Titulo', headerText: vm.getLayoutDisplayName('ConsultaMensagem_tbTitulo'), width: '400px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'LxTipoMensagem', headerText: vm.getLayoutDisplayName('ConsultaMensagem_cmbLxTipoMensagem'), width: '244px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null  , formatter: function (val, record) { return  vm.dataDomains.getName('TipoMensagem', val);} },
                            { key: 'Criacao', headerText: vm.getLayoutDisplayName('ConsultaMensagem_dtCriacao'), width: '127px', dataType: 'date', columnCssClass: 'ellipsis', format: 'dd/MM/yyyy HH:mm', hidden: false, unbound: false, group: null   },
                            { key: 'Envio', headerText: vm.getLayoutDisplayName('ConsultaMensagem_dtEnvio'), width: '120px', dataType: 'date', columnCssClass: 'ellipsis', format: 'dd/MM/yyyy HH:mm', hidden: false, unbound: false, group: null   },
                            { key: 'NomeEmpresa', headerText: vm.getLayoutDisplayName('ConsultaMensagem_lUpNomeEmpresa'), width: '400px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'NomeUsuario', headerText: vm.getLayoutDisplayName('ConsultaMensagem_lUpNomeUsuario'), width: '400px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   }
                        ],
                        features: [
                                    { name: 'Sorting', type: 'local', caseSensitive: false, unsortedColumnTooltip: '', sortedColumnTooltip: '',
                                      columnSorting: function (evt, ui) { }
                                      , customSortFunction: function (data, fields, direction) { return gridFunctions.sort(data, fields, direction); }
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
                        
                                        var dataView = $('#scyConsultaMensagem_dGrid').data('igGrid').dataSource;
                                        if (dataView.settings.filtering.expressions.length <= 0)
                                            dataView._filteredData = [];
                        
                                        var col = ui.owner._dialogCurrentColumn;
                        
                                        var divDialog = $('#' + dgl[0].id).find('.ui-iggrid-filterdialogaddcondition').find('span')[0];
                        
                                        var scriptHtml = '<div id="' + divDinamica + '">';
                                        scriptHtml += '  <script>';
                                        scriptHtml += '    var newCol = "' + col + '";';
                                        scriptHtml += '    var newGrid = $("#scyConsultaMensagem_dGrid");';
                                        scriptHtml += '    var listFilter = [];';
                                        scriptHtml += '    function hideColumn(){';
                                        scriptHtml += '     if ($("#showHideColumn_scyConsultaMensagem_dGrid")["0"].innerHTML.indexOf("Ocultar") >= 0) {';
                                        scriptHtml += '        $("#showHideColumn_scyConsultaMensagem_dGrid")["0"].innerHTML = "Mostrar Coluna";';
                                        scriptHtml += '        newGrid.igGridHiding("hideColumn", newCol);';
                                        scriptHtml += '     }';
                                        scriptHtml += '     else{';
                                        scriptHtml += '        $("#showHideColumn_scyConsultaMensagem_dGrid")["0"].innerHTML = "Ocultar Coluna";';
                                        scriptHtml += '        newGrid.igGridHiding("showColumn", newCol);';
                                        scriptHtml += '     }';
                                        scriptHtml += '    }';
                                        scriptHtml += '    function updateHideButton(){';
                                        scriptHtml += '         var column = $.grep(newGrid.igGrid("option", "columns"), function (element, index) { return element.key == newCol });';
                                        scriptHtml += '         if (column.length > 0){';
                                        scriptHtml += '             $("#showHideColumn_scyConsultaMensagem_dGrid")["0"].innerHTML = column[0].hidden ? "Mostrar Coluna" : "Ocultar Coluna"';
                                        scriptHtml += '         }';
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
                                        scriptHtml += '                     break;';
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
                                        scriptHtml += '  <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">';
                                        scriptHtml += '     <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">';
                                        scriptHtml += '          <div  style="margin-left: 5px" >';
                                        scriptHtml += '              <div>Propriedade:</div>';
                                        scriptHtml += '              <div id="comboFields_scyConsultaMensagem_dGrid"></div>';
                                        scriptHtml += '              <script>';
                                        scriptHtml += '                  var columns = newGrid.igGrid("option", "columns");';
                                        scriptHtml += '                  $("#comboFields_scyConsultaMensagem_dGrid").igCombo({ dataSource: columns, mode : "dropdown", valueKey: "key", textKey: "headerText", selectionChanging: function (evt, ui) {';
                                        scriptHtml += '                      newCol = ui.items["0"].data.key;';
                                        scriptHtml += '                      updateHideButton()';
                                        scriptHtml += '                  }});';
                                        scriptHtml += '                  $("#comboFields_scyConsultaMensagem_dGrid").igCombo("value", newCol);';
                                        scriptHtml += '              </script>';
                                        scriptHtml += '          </div>';
                                        scriptHtml += '      </div>';
                                        scriptHtml += '     <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">';
                                        scriptHtml += '          <div style="margin-left: 5px; margin-top: 5px">';
                                        scriptHtml += '              <i class="fa fa-sort-alpha-asc" aria-hidden="true" style="margin-right: 5px;"></i><a onclick="orderColumn(1)" style="cursor: pointer">Ordem Crescente</a>';
                                        scriptHtml += '          </div>';
                                        scriptHtml += '          <div style="margin-left: 5px; margin-top: 5px">';
                                        scriptHtml += '              <i class="fa fa-sort-alpha-desc" aria-hidden="true" style="margin-right: 5px;"></i><a onclick="orderColumn(2)" style="cursor: pointer">Ordem Decrescente</a>';
                                        scriptHtml += '          </div>';
                                        scriptHtml += '          <div style="margin-left: 5px; margin-top: 5px">';
                                        scriptHtml += '              <i class="fa fa-eye-slash" aria-hidden="true" style="margin-right: 5px;"></i><a onclick="hideColumn()" style="cursor: pointer" id="showHideColumn_scyConsultaMensagem_dGrid">Ocultar Coluna</a>';
                                        scriptHtml += '          </div>';
                                        scriptHtml += '          <br>';
                                        scriptHtml += '      </div>';
                                        scriptHtml += '  </div>';
                        
                                        $(scriptHtml).insertBefore(divDialog);
                                   },
                             },
                                    { name: 'Selection', mode: 'row'
                                    }, 
                                    { name: 'Tooltips', columnSettings:[{ columnKey: "Titulo", allowTooltips: true },{ columnKey: "LxTipoMensagem", allowTooltips: true },{ columnKey: "Criacao", allowTooltips: true },{ columnKey: "Envio", allowTooltips: true },{ columnKey: "NomeEmpresa", allowTooltips: false },{ columnKey: "NomeUsuario", allowTooltips: false }] },
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
                                      columnSettings: [{ columnKey: "NomeEmpresa", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpTcsEmpresaAutenticacaoC", isNullable: false, custom: vm.custom, vm: vm, verifyCanEditCol: verifyCanEditCol, allowMultiSelectionInSearch:true, activateAutoComplete: false, autoCompleteMaxResults: 7, validateOnClearState:false, maxValue:null, maxLength: 250, defaultValue: '' } }, { columnKey: "NomeUsuario", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpTcsUsuarioAutenticacaoC", isNullable: false, custom: vm.custom, vm: vm, verifyCanEditCol: verifyCanEditCol, allowMultiSelectionInSearch:true, activateAutoComplete: false, autoCompleteMaxResults: 7, validateOnClearState:false, maxValue:null, maxLength: 250, defaultValue: '' } }, { columnKey: 'Criacao', editorType: 'datepicker', editorOptions: {valueChanged: function(evt, ui){if (typeof vm.OnPropertyChangeDataGrid === 'function') {vm.OnPropertyChangeDataGrid('scyConsultaMensagem_dGrid', 'Criacao', ui.oldValue, ui.value);}}, minValue: new Date(1900, 0, 1), datepickerOptions: { changeMonth: true, changeYear: true }  } }, { columnKey: 'Envio', editorType: 'datepicker', editorOptions: {valueChanged: function(evt, ui){if (typeof vm.OnPropertyChangeDataGrid === 'function') {vm.OnPropertyChangeDataGrid('scyConsultaMensagem_dGrid', 'Envio', ui.oldValue, ui.value);}}, minValue: new Date(1900, 0, 1), datepickerOptions: { changeMonth: true, changeYear: true }  } }, { columnKey: 'LxTipoMensagem', editorType: 'combo', editorOptions: {   selectionChanged: function (evt, ui) {   var val = null;   if (ui.items != null && ui.items.length > 0) { val = ui.items[0].data['id']; }  updateEntity('LxTipoMensagem', val, false); },  mode: 'dropdown', dropDownOnFocus: true,  dataSource: vm.dataDomains.getItems('TipoMensagem', ''),  textKey: 'name', valueKey: 'id', enableClearButton: false }}, { columnKey: "Titulo" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('scyConsultaMensagem_dGrid', 'Titulo', ui.oldValue, ui.value);}}, maxLength: 100 } }],
                                      rowDeleting: function (evt, ui) {
                                          deletedIndex = ui.element.context.rowIndex;
                                          var entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowID);
                                          if (entity) {
                                              vm.deleteEntity(entity);
                                          }
                                      },
                                      rowDeleted: function (evt, ui) {
                                          var grid = $('#scyConsultaMensagem_dGrid');
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
                                          var columns = $('#scyConsultaMensagem_dGrid').igGridUpdating('option', 'columnSettings');
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
                            vm.OnDataGridCreated('scyConsultaMensagem_dGrid');
                        }
                        var selectionrowselectionchanged = null, selectedRowId = -1;
                        selectionrowselectionchanged = function (evt, ui) {
                            if ((ui.owner.grid.selectedRow() && typeof ui.owner.grid.selectedRow().id !== 'undefined') || (ui.owner.grid.selectedRows() && ui.owner.grid.selectedRows().length > 0)) { 
                                if (isNullOrEmpty(ui.owner.selectedRows())|| ui.selectedRows.length <= 1) {
                                    $(document).undelegate('#scyConsultaMensagem_dGrid', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                                    ui.owner.clearSelection();
                                    ui.owner.selectRow(ui.row.index);
                                    if (vm.status() === 'Q'){
                                        var gridCell = ui.owner.grid;
                                        grid.find('div.borderCell').remove();
                                        //$(gridCell.cellAt(-1, ui.owner._rowIndex)).append(" < div class='borderCell' style='z-index:100; border: 1px solid #849fd9 !important;'></div>");
                                    }
                                    selectedRowId = ui.row.id;
                                    $(document).delegate ('#scyConsultaMensagem_dGrid', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                                }
                                selectGridCurrentItem(vm.goToKey, 'RowDataId', ui); 
                             } 
                        };
                        $(document).delegate('#scyConsultaMensagem_dGrid', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                        $('#scyConsultaMensagem_dGrid > tbody tr').live('focus', function(evt) {
                            var grid = $('#scyConsultaMensagem_dGrid'), row = $(this).closest('tr'), id = parseInt(row.attr('data-id'), 10);
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
                            if (vm.ConsultaMensagem().status() === 'Q') vm.ConsultaMensagem().dataToolbar.viewInfo();
                        });
                    }
                    vm.addDataSource({ key: 'scyConsultaMensagem_dGrid', name: 'dataView', itemsSource: itemsSource });
                }
            
        };
        
        return complement;
    }
    
    return complementCtor;
});
