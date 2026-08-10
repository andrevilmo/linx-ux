define(['managers/__auth', 'managers/user'], function (managerAuth, managerUser) {
    var complementCtor = function() {
        var complement = {
            isAutomatic: true
            , ChangedBrandCadastroTransacaoAutorizacao_dGridTcsTransacaoMenuAutorizacao: function(vm, decimals, reset) {
                var i, format = '0.'.concat('0'.repeat(decimals)), grd =$('#CadastroTransacaoAutorizacao_dGridTcsTransacaoMenuAutorizacao').data('igGrid'),
                    grdUpd = $('#CadastroTransacaoAutorizacao_dGridTcsTransacaoMenuAutorizacao').data('igGridUpdating');
                if(isNull(grd) || isNull(grdUpd)) return;
                for (i = 0; i < grd.options.columns.length; i++) {
                }
                for (i = 0; i < grdUpd.options.columnSettings.length; i++) {
                }
                grd.dataBind();
            }
            , renderCadastroTransacaoAutorizacao_dGridTcsTransacaoMenuAutorizacao: function(vm) {
                var getDataSource = function() {
                    var source = null;
                    try {
                        source = vm.currentDataItem().TcsTransacaoMenuAutorizacaoList;
                    }
                    catch (e) { }
                    return isNullOrEmpty(source) ? ko.observableArray([]) : source;
                };
                var dataSourceIsLoaded = function() {
                    var isLoaded = false;
                    try {
                        isLoaded = (vm.currentDataItem().TcsTransacaoMenuAutorizacaoIsLoaded === true || vm.currentDataItem().TcsTransacaoMenuAutorizacaoList().length > 0);
                    }
                    catch (e) {
                        isLoaded = true;
                    }
                    return isLoaded;
                }
                $('#CadastroTransacaoAutorizacao_dGridTcsTransacaoMenuAutorizacao_headers').live('focus  keydown', function (evt) {
                    var keyCode = window.event ? evt.which : evt.keyCode;
                    if (keyCode === 9) {
                        var cols = $('#CadastroTransacaoAutorizacao_dGridTcsTransacaoMenuAutorizacao').igGrid('option', 'columns');
                        var dataView = $('#CadastroTransacaoAutorizacao_dGridTcsTransacaoMenuAutorizacao').data('igGrid').dataSource._dataView
                        if (dataView.length === 0) return;
                        var firstRow = dataView[0].RowDataId;
                        clear = vm.status() === 'C';
                        if (vm.status() === 'C')
                            $('#CadastroTransacaoAutorizacao_dGridTcsTransacaoMenuAutorizacao').igGridUpdating('startEdit', firstRow, 0, true);
                        else {
                            var entity = findElementByKey(getDataSource(), 'RowDataId', firstRow);
                            var indexColumn = 0;
                            cols.some(function (entry) {
                                if (entry.key !== 'RowDataId' && !entry.hidden) {
                                    if (verifyCanEditCol(entry.key, clear, entity)) {
                                        $('#CadastroTransacaoAutorizacao_dGridTcsTransacaoMenuAutorizacao').igGridUpdating('startEdit', firstRow, indexColumn, true);
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
                   if($('#CadastroTransacaoAutorizacao_dGridTcsTransacaoMenuAutorizacao').data('igGrid') === undefined) return '';
                   var cols = $('#CadastroTransacaoAutorizacao_dGridTcsTransacaoMenuAutorizacao').igGrid('option', 'columns');
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
                  if (!grid) grid = $('#CadastroTransacaoAutorizacao_dGridTcsTransacaoMenuAutorizacao');
                  return ((!grid[0] || (!forceCreating && grid.parent().width() <= 0)) && !$('#dialogTcsTransacaoMenuAutorizacao').is(':visible'));
                }
                var refreshData = true;
                var itemsSource = { isElementHided: isElementHided, getVisibleColumns: getVisibleColumns, containerId: 'CadastroTransacaoAutorizacao_dGridTcsTransacaoMenuAutorizacao_container', dataBind: function (commitData, forceCreating) {
                   var grid = $('#CadastroTransacaoAutorizacao_dGridTcsTransacaoMenuAutorizacao');
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
                       $('#CadastroTransacaoAutorizacao_dGridTcsTransacaoMenuAutorizacao_groupbyarea').addClass('hide');
                   }
                   if (grid.igGridUpdating('isEditing')) {
                        grid.igGridUpdating('endEdit', true);
                   }
                   if (execFillDetais) {
                     grid.igGrid("option", "dataSource", []);
                     vm.currentDataItem().fillDetails(false, 'TcsTransacaoMenuAutorizacao');
                     return;
                   }
                   grid.igGrid("option", "dataSource", unwrapObservableArray(getDataSource(), vm));
                   var rows = grid.igGrid('allRows');
                   if (rows.length > 0) {
                     var verticalContainer = grid.igGrid('scrollContainer');
                     var isSelected = false;
                     if (vm.currentDataItem().currentTcsTransacaoMenuAutorizacao() != null)
                     {
                       for(var idx = 0; idx < rows.length; idx++)
                       {
                         if (rows[idx].dataset.id == getAbsoluteValue(vm.currentDataItem().currentTcsTransacaoMenuAutorizacao().RowDataId))
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
                     if ($('#dialogTcsTransacaoMenuAutorizacao').is(':visible')) {
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
                           $('label#currentNumberTcsTransacaoMenuAutorizacao').html(current + ' - ' + totalCurrentPage);
                        }
                        else
                           $('label#currentNumberTcsTransacaoMenuAutorizacao').html(1);
                        $('label#totalNumberTcsTransacaoMenuAutorizacao').html(totalGrid);
                    }
                   } else {
                       $('label#currentNumberTcsTransacaoMenuAutorizacao').html(0);
                       $('label#totalNumberTcsTransacaoMenuAutorizacao').html(0);
                   }
                }};
                var valueGrouBy = -1;
                var deletedIndex = -1;
                function verifyCanEditCol(column, clear, entity){
                    switch(column){
                        case 'DescModulo': { canEditing = clear || (entity && entity.isAdded()); break;}
                        case 'DescricaoAplicativo': { canEditing = clear; break;}
                        case 'DescModuloMenu': { canEditing = clear || (entity && entity.isAdded()); break;}
                        case 'Inativo': { canEditing = clear || vm.enabledForEditing(); break;}
                        case 'OrdemNavegacao': { canEditing = clear || vm.enabledForEditing(); break;}
                        case 'CodTransacao': { canEditing = clear || vm.enabledForEditing(); break;}
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
                            $('#CadastroTransacaoAutorizacao_dGridTcsTransacaoMenuAutorizacao_LayoutBtn').igPopover('hide');
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
                                      showMultimidia(entity, e, table, key, vm.CadastroTransacaoAutorizacao());
                                 }
                             }
                             if (typeof vm.OnGridClientClick === 'function') {
                                 entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowKey);
                                 vm.OnGridClientClick('CadastroTransacaoAutorizacao_dGridTcsTransacaoMenuAutorizacao', ui.colKey, entity);
                             }
                             if (vm.status() != 'Q') {
                                 var grid = $('#CadastroTransacaoAutorizacao_dGridTcsTransacaoMenuAutorizacao');
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
                            { key: 'DescModulo', headerText: vm.getLayoutHeaderGrid('CadastroTransacaoAutorizacao_lUpTcsTransacaoMenuAutorizacao_DescModulo'), width: '421px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'DescricaoAplicativo', headerText: vm.getLayoutHeaderGrid('CadastroTransacaoAutorizacao_tbTcsTransacaoMenuAutorizacao_DescricaoAplicativo'), width: '400px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'DescModuloMenu', headerText: vm.getLayoutHeaderGrid('CadastroTransacaoAutorizacao_lUpTcsTransacaoMenuAutorizacao_DescModuloMenu'), width: '421px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'Inativo', headerText: vm.getLayoutHeaderGrid('CadastroTransacaoAutorizacao_ckTcsTransacaoMenuAutorizacao_Inativo'), width: '127px', dataType: 'bool', columnCssClass: 'ellipsis', format: 'checkbox', hidden: false, unbound: false, group: null   },
                            { key: 'OrdemNavegacao', headerText: vm.getLayoutHeaderGrid('CadastroTransacaoAutorizacao_ntxTcsTransacaoMenuAutorizacao_OrdemNavegacao'), width: '101px', dataType: 'number', columnCssClass: 'ellipsis', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                            { key: 'CodTransacao', headerText: vm.getLayoutHeaderGrid('CadastroTransacaoAutorizacao_tbTcsTransacaoMenuAutorizacao_CodTransacao'), width: '114px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   }
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
                        
                                        var dataView = $('#CadastroTransacaoAutorizacao_dGridTcsTransacaoMenuAutorizacao').data('igGrid').dataSource;
                                        if (dataView.settings.filtering.expressions.length <= 0)
                                            dataView._filteredData = [];
                        
                                        var col = ui.owner._dialogCurrentColumn;
                        
                                        var divDialog = $('#' + dgl[0].id).find('.ui-iggrid-filterdialogaddcondition').find('span')[0];
                        
                                        var scriptHtml = '<div id="' + divDinamica + '">';
                                        scriptHtml += '  <script>';
                                        scriptHtml += '    var newCol = "' + col + '";';
                                        scriptHtml += '    var newGrid = $("#CadastroTransacaoAutorizacao_dGridTcsTransacaoMenuAutorizacao");';
                                        scriptHtml += '    var listFilter = [];';
                                        scriptHtml += '    function hideColumn(){';
                                        scriptHtml += '     if ($("#showHideColumn_CadastroTransacaoAutorizacao_dGridTcsTransacaoMenuAutorizacao")["0"].innerHTML.indexOf("Ocultar") >= 0) {';
                                        scriptHtml += '        $("#showHideColumn_CadastroTransacaoAutorizacao_dGridTcsTransacaoMenuAutorizacao")["0"].innerHTML = "Mostrar Coluna";';
                                        scriptHtml += '        newGrid.igGridHiding("hideColumn", newCol);';
                                        scriptHtml += '     }';
                                        scriptHtml += '     else{';
                                        scriptHtml += '        $("#showHideColumn_CadastroTransacaoAutorizacao_dGridTcsTransacaoMenuAutorizacao")["0"].innerHTML = "Ocultar Coluna";';
                                        scriptHtml += '        newGrid.igGridHiding("showColumn", newCol);';
                                        scriptHtml += '     }';
                                        scriptHtml += '    }';
                                        scriptHtml += '    function updateHideButton(){';
                                        scriptHtml += '         var column = $.grep(newGrid.igGrid("option", "columns"), function (element, index) { return element.key == newCol });';
                                        scriptHtml += '         if (column.length > 0){';
                                        scriptHtml += '             $("#showHideColumn_CadastroTransacaoAutorizacao_dGridTcsTransacaoMenuAutorizacao")["0"].innerHTML = column[0].hidden ? "Mostrar Coluna" : "Ocultar Coluna"';
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
                                        scriptHtml += '              <div id="comboFields_CadastroTransacaoAutorizacao_dGridTcsTransacaoMenuAutorizacao"></div>';
                                        scriptHtml += '              <script>';
                                        scriptHtml += '                  var columns = newGrid.igGrid("option", "columns");';
                                        scriptHtml += '                  $("#comboFields_CadastroTransacaoAutorizacao_dGridTcsTransacaoMenuAutorizacao").igCombo({ dataSource: columns, mode : "dropdown", valueKey: "key", textKey: "headerText", selectionChanging: function (evt, ui) {';
                                        scriptHtml += '                      newCol = ui.items["0"].data.key;';
                                        scriptHtml += '                      updateHideButton()';
                                        scriptHtml += '                  }});';
                                        scriptHtml += '                  $("#comboFields_CadastroTransacaoAutorizacao_dGridTcsTransacaoMenuAutorizacao").igCombo("value", newCol);';
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
                                        scriptHtml += '              <i class="fa fa-eye-slash" aria-hidden="true" style="margin-right: 5px;"></i><a onclick="hideColumn()" style="cursor: pointer" id="showHideColumn_CadastroTransacaoAutorizacao_dGridTcsTransacaoMenuAutorizacao">Ocultar Coluna</a>';
                                        scriptHtml += '          </div>';
                                        scriptHtml += '          <br>';
                                        scriptHtml += '      </div>';
                                        scriptHtml += '  </div>';
                        
                                        $(scriptHtml).insertBefore(divDialog);
                                   },
                             },
                                    { name: 'Selection', mode: 'row'
                                    }, 
                                    { name: 'Tooltips', columnSettings:[{ columnKey: "DescModulo", allowTooltips: false },{ columnKey: "DescricaoAplicativo", allowTooltips: true },{ columnKey: "DescModuloMenu", allowTooltips: false },{ columnKey: "Inativo", allowTooltips: true },{ columnKey: "OrdemNavegacao", allowTooltips: true },{ columnKey: "CodTransacao", allowTooltips: true }] },
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
                                      columnSettings: [{ columnKey: "DescModulo", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpTcsModuloMenuAutorizacao", isNullable: false, custom: vm.custom, vm: vm, verifyCanEditCol: verifyCanEditCol, allowMultiSelectionInSearch:true, activateAutoComplete: false, autoCompleteMaxResults: 7, validateOnClearState:false, maxValue:null, maxLength: 60, defaultValue: '' } }, { columnKey: "DescModuloMenu", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpTcsModuloMenuAutorizacao", isNullable: false, custom: vm.custom, vm: vm, verifyCanEditCol: verifyCanEditCol, allowMultiSelectionInSearch:true, activateAutoComplete: false, autoCompleteMaxResults: 7, validateOnClearState:false, maxValue:null, maxLength: 60, defaultValue: '' } }, { columnKey: "DescricaoAplicativo" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('CadastroTransacaoAutorizacao_dGridTcsTransacaoMenuAutorizacao', 'DescricaoAplicativo', ui.oldValue, ui.value);}}, maxLength: 250 } }, { columnKey: "OrdemNavegacao" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('CadastroTransacaoAutorizacao_dGridTcsTransacaoMenuAutorizacao', 'OrdemNavegacao', ui.oldValue, ui.value);}},  maxLength: 3, maxValue: null, minValue: 0, dataMode: 'byte' } }, { columnKey: "CodTransacao" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('CadastroTransacaoAutorizacao_dGridTcsTransacaoMenuAutorizacao', 'CodTransacao', ui.oldValue, ui.value);}}, maxLength: 10 } }],
                                      rowDeleting: function (evt, ui) {
                                          deletedIndex = ui.element.context.rowIndex;
                                          var entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowID);
                                          if (entity) {
                                              vm.deleteEntity(entity);
                                          }
                                      },
                                      rowDeleted: function (evt, ui) {
                                          var grid = $('#CadastroTransacaoAutorizacao_dGridTcsTransacaoMenuAutorizacao');
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
                                          var columns = $('#CadastroTransacaoAutorizacao_dGridTcsTransacaoMenuAutorizacao').igGridUpdating('option', 'columnSettings');
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
                                                     vm.dataCombo.fillDataCombos(lookUpName, ui.columnKey, vm.currentDataItem().currentTcsTransacaoMenuAutorizacao(), function (result) {
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
                            vm.OnDataGridCreated('CadastroTransacaoAutorizacao_dGridTcsTransacaoMenuAutorizacao');
                        }
                        var selectionrowselectionchanged = null, selectedRowId = -1;
                        selectionrowselectionchanged = function (evt, ui) {
                            if ((ui.owner.grid.selectedRow() && typeof ui.owner.grid.selectedRow().id !== 'undefined') || (ui.owner.grid.selectedRows() && ui.owner.grid.selectedRows().length > 0)) { 
                                if (isNullOrEmpty(ui.owner.selectedRows())|| ui.selectedRows.length <= 1) {
                                    $(document).undelegate('#CadastroTransacaoAutorizacao_dGridTcsTransacaoMenuAutorizacao', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                                    ui.owner.clearSelection();
                                    ui.owner.selectRow(ui.row.index);
                                    if (vm.status() === 'Q'){
                                        var gridCell = ui.owner.grid;
                                        grid.find('div.borderCell').remove();
                                        //$(gridCell.cellAt(-1, ui.owner._rowIndex)).append(" < div class='borderCell' style='z-index:100; border: 1px solid #849fd9 !important;'></div>");
                                    }
                                    selectedRowId = ui.row.id;
                                    $(document).delegate ('#CadastroTransacaoAutorizacao_dGridTcsTransacaoMenuAutorizacao', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                                }
                                selectGridCurrentItem(vm.goToKey, 'RowDataId', ui, vm.currentDataItem().currentTcsTransacaoMenuAutorizacao, getDataSource()); 
                             } 
                        };
                        $(document).delegate('#CadastroTransacaoAutorizacao_dGridTcsTransacaoMenuAutorizacao', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                        $('#CadastroTransacaoAutorizacao_dGridTcsTransacaoMenuAutorizacao > tbody tr').live('focus', function(evt) {
                            var grid = $('#CadastroTransacaoAutorizacao_dGridTcsTransacaoMenuAutorizacao'), row = $(this).closest('tr'), id = parseInt(row.attr('data-id'), 10);
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
                    vm.addDataSource({ key: 'CadastroTransacaoAutorizacao_dGridTcsTransacaoMenuAutorizacao', name: 'TcsTransacaoMenuAutorizacaoList', itemsSource: itemsSource });
                }
            
            , ChangedBrandCadastroTransacaoAutorizacao_dGridTcsTransacaoDependenteAutorizacao: function(vm, decimals, reset) {
                var i, format = '0.'.concat('0'.repeat(decimals)), grd =$('#CadastroTransacaoAutorizacao_dGridTcsTransacaoDependenteAutorizacao').data('igGrid'),
                    grdUpd = $('#CadastroTransacaoAutorizacao_dGridTcsTransacaoDependenteAutorizacao').data('igGridUpdating');
                if(isNull(grd) || isNull(grdUpd)) return;
                for (i = 0; i < grd.options.columns.length; i++) {
                }
                for (i = 0; i < grdUpd.options.columnSettings.length; i++) {
                }
                grd.dataBind();
            }
            , renderCadastroTransacaoAutorizacao_dGridTcsTransacaoDependenteAutorizacao: function(vm) {
                var getDataSource = function() {
                    var source = null;
                    try {
                        source = vm.currentDataItem().TcsTransacaoDependenteAutorizacaoList;
                    }
                    catch (e) { }
                    return isNullOrEmpty(source) ? ko.observableArray([]) : source;
                };
                var dataSourceIsLoaded = function() {
                    var isLoaded = false;
                    try {
                        isLoaded = (vm.currentDataItem().TcsTransacaoDependenteAutorizacaoIsLoaded === true || vm.currentDataItem().TcsTransacaoDependenteAutorizacaoList().length > 0);
                    }
                    catch (e) {
                        isLoaded = true;
                    }
                    return isLoaded;
                }
                $('#CadastroTransacaoAutorizacao_dGridTcsTransacaoDependenteAutorizacao_headers').live('focus  keydown', function (evt) {
                    var keyCode = window.event ? evt.which : evt.keyCode;
                    if (keyCode === 9) {
                        var cols = $('#CadastroTransacaoAutorizacao_dGridTcsTransacaoDependenteAutorizacao').igGrid('option', 'columns');
                        var dataView = $('#CadastroTransacaoAutorizacao_dGridTcsTransacaoDependenteAutorizacao').data('igGrid').dataSource._dataView
                        if (dataView.length === 0) return;
                        var firstRow = dataView[0].RowDataId;
                        clear = vm.status() === 'C';
                        if (vm.status() === 'C')
                            $('#CadastroTransacaoAutorizacao_dGridTcsTransacaoDependenteAutorizacao').igGridUpdating('startEdit', firstRow, 0, true);
                        else {
                            var entity = findElementByKey(getDataSource(), 'RowDataId', firstRow);
                            var indexColumn = 0;
                            cols.some(function (entry) {
                                if (entry.key !== 'RowDataId' && !entry.hidden) {
                                    if (verifyCanEditCol(entry.key, clear, entity)) {
                                        $('#CadastroTransacaoAutorizacao_dGridTcsTransacaoDependenteAutorizacao').igGridUpdating('startEdit', firstRow, indexColumn, true);
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
                   if($('#CadastroTransacaoAutorizacao_dGridTcsTransacaoDependenteAutorizacao').data('igGrid') === undefined) return '';
                   var cols = $('#CadastroTransacaoAutorizacao_dGridTcsTransacaoDependenteAutorizacao').igGrid('option', 'columns');
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
                  if (!grid) grid = $('#CadastroTransacaoAutorizacao_dGridTcsTransacaoDependenteAutorizacao');
                  return ((!grid[0] || (!forceCreating && grid.parent().width() <= 0)) && !$('#dialogTcsTransacaoDependenteAutorizacao').is(':visible'));
                }
                var refreshData = true;
                var itemsSource = { isElementHided: isElementHided, getVisibleColumns: getVisibleColumns, containerId: 'CadastroTransacaoAutorizacao_dGridTcsTransacaoDependenteAutorizacao_container', dataBind: function (commitData, forceCreating) {
                   var grid = $('#CadastroTransacaoAutorizacao_dGridTcsTransacaoDependenteAutorizacao');
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
                       $('#CadastroTransacaoAutorizacao_dGridTcsTransacaoDependenteAutorizacao_groupbyarea').addClass('hide');
                   }
                   if (grid.igGridUpdating('isEditing')) {
                        grid.igGridUpdating('endEdit', true);
                   }
                   if (execFillDetais) {
                     grid.igGrid("option", "dataSource", []);
                     vm.currentDataItem().fillDetails(false, 'TcsTransacaoDependenteAutorizacao');
                     return;
                   }
                   grid.igGrid("option", "dataSource", unwrapObservableArray(getDataSource(), vm));
                   var rows = grid.igGrid('allRows');
                   if (rows.length > 0) {
                     var verticalContainer = grid.igGrid('scrollContainer');
                     var isSelected = false;
                     if (vm.currentDataItem().currentTcsTransacaoDependenteAutorizacao() != null)
                     {
                       for(var idx = 0; idx < rows.length; idx++)
                       {
                         if (rows[idx].dataset.id == getAbsoluteValue(vm.currentDataItem().currentTcsTransacaoDependenteAutorizacao().RowDataId))
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
                     if ($('#dialogTcsTransacaoDependenteAutorizacao').is(':visible')) {
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
                           $('label#currentNumberTcsTransacaoDependenteAutorizacao').html(current + ' - ' + totalCurrentPage);
                        }
                        else
                           $('label#currentNumberTcsTransacaoDependenteAutorizacao').html(1);
                        $('label#totalNumberTcsTransacaoDependenteAutorizacao').html(totalGrid);
                    }
                   } else {
                       $('label#currentNumberTcsTransacaoDependenteAutorizacao').html(0);
                       $('label#totalNumberTcsTransacaoDependenteAutorizacao').html(0);
                   }
                }};
                var valueGrouBy = -1;
                var deletedIndex = -1;
                function verifyCanEditCol(column, clear, entity){
                    switch(column){
                        case 'DescTransacao': { canEditing = clear || vm.enabledForEditing(); break;}
                        case 'ClasseNome': { canEditing = clear || vm.enabledForEditing(); break;}
                        case 'LxPosicaoDaTransacao': { canEditing = clear || vm.enabledForEditing(); break;}
                        case 'LxTipoLayout': { canEditing = clear || vm.enabledForEditing(); break;}
                        case 'Visivel': { canEditing = clear || vm.enabledForEditing(); break;}
                        case 'CompartilhaBoPrincipal': { canEditing = clear || vm.enabledForEditing(); break;}
                        case 'ExecutaPesquisa': { canEditing = clear || vm.enabledForEditing(); break;}
                        case 'UsaFiltrosDoBoPrincipal': { canEditing = clear || vm.enabledForEditing(); break;}
                        case 'PropriedadesDoMestre': { canEditing = clear || vm.enabledForEditing(); break;}
                        case 'PropriedadesDoDetalhe': { canEditing = clear || vm.enabledForEditing(); break;}
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
                            $('#CadastroTransacaoAutorizacao_dGridTcsTransacaoDependenteAutorizacao_LayoutBtn').igPopover('hide');
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
                                      showMultimidia(entity, e, table, key, vm.CadastroTransacaoAutorizacao());
                                 }
                             }
                             if (typeof vm.OnGridClientClick === 'function') {
                                 entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowKey);
                                 vm.OnGridClientClick('CadastroTransacaoAutorizacao_dGridTcsTransacaoDependenteAutorizacao', ui.colKey, entity);
                             }
                             if (vm.status() != 'Q') {
                                 var grid = $('#CadastroTransacaoAutorizacao_dGridTcsTransacaoDependenteAutorizacao');
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
                            { key: 'DescTransacao', headerText: vm.getLayoutHeaderGrid('CadastroTransacaoAutorizacao_lUpTcsTransacaoDependenteAutorizacao_DescTransacao'), width: '421px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'ClasseNome', headerText: vm.getLayoutHeaderGrid('CadastroTransacaoAutorizacao_lUpTcsTransacaoDependenteAutorizacao_ClasseNome'), width: '421px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'LxPosicaoDaTransacao', headerText: vm.getLayoutHeaderGrid('CadastroTransacaoAutorizacao_cmbTcsTransacaoDependenteAutorizacao_LxPosicaoDaTransacao'), width: '200px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null  , formatter: function (val, record) { return  vm.dataDomains.getName('PosicaoDaTransacao', val);} },
                            { key: 'LxTipoLayout', headerText: vm.getLayoutHeaderGrid('CadastroTransacaoAutorizacao_cmbTcsTransacaoDependenteAutorizacao_LxTipoLayout'), width: '218px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null  , formatter: function (val, record) { return  vm.dataDomains.getName('TipoLayoutDependente', val);} },
                            { key: 'Visivel', headerText: vm.getLayoutHeaderGrid('CadastroTransacaoAutorizacao_ckTcsTransacaoDependenteAutorizacao_Visivel'), width: '127px', dataType: 'bool', columnCssClass: 'ellipsis', format: 'checkbox', hidden: false, unbound: false, group: null   },
                            { key: 'CompartilhaBoPrincipal', headerText: vm.getLayoutHeaderGrid('CadastroTransacaoAutorizacao_ckTcsTransacaoDependenteAutorizacao_CompartilhaBoPrincipal'), width: '348px', dataType: 'bool', columnCssClass: 'ellipsis', format: 'checkbox', hidden: false, unbound: false, group: null   },
                            { key: 'ExecutaPesquisa', headerText: vm.getLayoutHeaderGrid('CadastroTransacaoAutorizacao_ckTcsTransacaoDependenteAutorizacao_ExecutaPesquisa'), width: '335px', dataType: 'bool', columnCssClass: 'ellipsis', format: 'checkbox', hidden: false, unbound: false, group: null   },
                            { key: 'UsaFiltrosDoBoPrincipal', headerText: vm.getLayoutHeaderGrid('CadastroTransacaoAutorizacao_ckTcsTransacaoDependenteAutorizacao_UsaFiltrosDoBoPrincipal'), width: '387px', dataType: 'bool', columnCssClass: 'ellipsis', format: 'checkbox', hidden: false, unbound: false, group: null   },
                            { key: 'PropriedadesDoMestre', headerText: vm.getLayoutHeaderGrid('CadastroTransacaoAutorizacao_tbTcsTransacaoDependenteAutorizacao_PropriedadesDoMestre'), width: '400px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'PropriedadesDoDetalhe', headerText: vm.getLayoutHeaderGrid('CadastroTransacaoAutorizacao_tbTcsTransacaoDependenteAutorizacao_PropriedadesDoDetalhe'), width: '400px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   }
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
                        
                                        var dataView = $('#CadastroTransacaoAutorizacao_dGridTcsTransacaoDependenteAutorizacao').data('igGrid').dataSource;
                                        if (dataView.settings.filtering.expressions.length <= 0)
                                            dataView._filteredData = [];
                        
                                        var col = ui.owner._dialogCurrentColumn;
                        
                                        var divDialog = $('#' + dgl[0].id).find('.ui-iggrid-filterdialogaddcondition').find('span')[0];
                        
                                        var scriptHtml = '<div id="' + divDinamica + '">';
                                        scriptHtml += '  <script>';
                                        scriptHtml += '    var newCol = "' + col + '";';
                                        scriptHtml += '    var newGrid = $("#CadastroTransacaoAutorizacao_dGridTcsTransacaoDependenteAutorizacao");';
                                        scriptHtml += '    var listFilter = [];';
                                        scriptHtml += '    function hideColumn(){';
                                        scriptHtml += '     if ($("#showHideColumn_CadastroTransacaoAutorizacao_dGridTcsTransacaoDependenteAutorizacao")["0"].innerHTML.indexOf("Ocultar") >= 0) {';
                                        scriptHtml += '        $("#showHideColumn_CadastroTransacaoAutorizacao_dGridTcsTransacaoDependenteAutorizacao")["0"].innerHTML = "Mostrar Coluna";';
                                        scriptHtml += '        newGrid.igGridHiding("hideColumn", newCol);';
                                        scriptHtml += '     }';
                                        scriptHtml += '     else{';
                                        scriptHtml += '        $("#showHideColumn_CadastroTransacaoAutorizacao_dGridTcsTransacaoDependenteAutorizacao")["0"].innerHTML = "Ocultar Coluna";';
                                        scriptHtml += '        newGrid.igGridHiding("showColumn", newCol);';
                                        scriptHtml += '     }';
                                        scriptHtml += '    }';
                                        scriptHtml += '    function updateHideButton(){';
                                        scriptHtml += '         var column = $.grep(newGrid.igGrid("option", "columns"), function (element, index) { return element.key == newCol });';
                                        scriptHtml += '         if (column.length > 0){';
                                        scriptHtml += '             $("#showHideColumn_CadastroTransacaoAutorizacao_dGridTcsTransacaoDependenteAutorizacao")["0"].innerHTML = column[0].hidden ? "Mostrar Coluna" : "Ocultar Coluna"';
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
                                        scriptHtml += '              <div id="comboFields_CadastroTransacaoAutorizacao_dGridTcsTransacaoDependenteAutorizacao"></div>';
                                        scriptHtml += '              <script>';
                                        scriptHtml += '                  var columns = newGrid.igGrid("option", "columns");';
                                        scriptHtml += '                  $("#comboFields_CadastroTransacaoAutorizacao_dGridTcsTransacaoDependenteAutorizacao").igCombo({ dataSource: columns, mode : "dropdown", valueKey: "key", textKey: "headerText", selectionChanging: function (evt, ui) {';
                                        scriptHtml += '                      newCol = ui.items["0"].data.key;';
                                        scriptHtml += '                      updateHideButton()';
                                        scriptHtml += '                  }});';
                                        scriptHtml += '                  $("#comboFields_CadastroTransacaoAutorizacao_dGridTcsTransacaoDependenteAutorizacao").igCombo("value", newCol);';
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
                                        scriptHtml += '              <i class="fa fa-eye-slash" aria-hidden="true" style="margin-right: 5px;"></i><a onclick="hideColumn()" style="cursor: pointer" id="showHideColumn_CadastroTransacaoAutorizacao_dGridTcsTransacaoDependenteAutorizacao">Ocultar Coluna</a>';
                                        scriptHtml += '          </div>';
                                        scriptHtml += '          <br>';
                                        scriptHtml += '      </div>';
                                        scriptHtml += '  </div>';
                        
                                        $(scriptHtml).insertBefore(divDialog);
                                   },
                             },
                                    { name: 'Selection', mode: 'row'
                                    }, 
                                    { name: 'Tooltips', columnSettings:[{ columnKey: "DescTransacao", allowTooltips: false },{ columnKey: "ClasseNome", allowTooltips: false },{ columnKey: "LxPosicaoDaTransacao", allowTooltips: true },{ columnKey: "LxTipoLayout", allowTooltips: true },{ columnKey: "Visivel", allowTooltips: true },{ columnKey: "CompartilhaBoPrincipal", allowTooltips: true },{ columnKey: "ExecutaPesquisa", allowTooltips: true },{ columnKey: "UsaFiltrosDoBoPrincipal", allowTooltips: true },{ columnKey: "PropriedadesDoMestre", allowTooltips: true },{ columnKey: "PropriedadesDoDetalhe", allowTooltips: true }] },
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
                                      columnSettings: [{ columnKey: "DescTransacao", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpTcsTransacaoDependente", isNullable: false, custom: vm.custom, vm: vm, verifyCanEditCol: verifyCanEditCol, allowMultiSelectionInSearch:true, activateAutoComplete: false, autoCompleteMaxResults: 7, validateOnClearState:false, maxValue:null, maxLength: 60, defaultValue: '' } }, { columnKey: "ClasseNome", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpTcsTransacaoDependente", isNullable: false, custom: vm.custom, vm: vm, verifyCanEditCol: verifyCanEditCol, allowMultiSelectionInSearch:true, activateAutoComplete: false, autoCompleteMaxResults: 7, validateOnClearState:false, maxValue:null, maxLength: 40, defaultValue: '' } }, { columnKey: 'LxPosicaoDaTransacao', editorType: 'combo', editorOptions: {   selectionChanged: function (evt, ui) {   var val = null;   if (ui.items != null && ui.items.length > 0) { val = ui.items[0].data['id']; }  updateEntity('LxPosicaoDaTransacao', val, false); },  mode: 'dropdown', dropDownOnFocus: true,  dataSource: vm.dataDomains.getItems('PosicaoDaTransacao', ''),  textKey: 'name', valueKey: 'id', enableClearButton: false }}, { columnKey: 'LxTipoLayout', editorType: 'combo', editorOptions: {   selectionChanged: function (evt, ui) {   var val = null;   if (ui.items != null && ui.items.length > 0) { val = ui.items[0].data['id']; }  updateEntity('LxTipoLayout', val, false); },  mode: 'dropdown', dropDownOnFocus: true,  dataSource: vm.dataDomains.getItems('TipoLayoutDependente', ''),  textKey: 'name', valueKey: 'id', enableClearButton: false }}, { columnKey: "PropriedadesDoMestre" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('CadastroTransacaoAutorizacao_dGridTcsTransacaoDependenteAutorizacao', 'PropriedadesDoMestre', ui.oldValue, ui.value);}}, maxLength: 250 } }, { columnKey: "PropriedadesDoDetalhe" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('CadastroTransacaoAutorizacao_dGridTcsTransacaoDependenteAutorizacao', 'PropriedadesDoDetalhe', ui.oldValue, ui.value);}}, maxLength: 250 } }],
                                      rowDeleting: function (evt, ui) {
                                          deletedIndex = ui.element.context.rowIndex;
                                          var entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowID);
                                          if (entity) {
                                              vm.deleteEntity(entity);
                                          }
                                      },
                                      rowDeleted: function (evt, ui) {
                                          var grid = $('#CadastroTransacaoAutorizacao_dGridTcsTransacaoDependenteAutorizacao');
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
                                          var columns = $('#CadastroTransacaoAutorizacao_dGridTcsTransacaoDependenteAutorizacao').igGridUpdating('option', 'columnSettings');
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
                                                     vm.dataCombo.fillDataCombos(lookUpName, ui.columnKey, vm.currentDataItem().currentTcsTransacaoDependenteAutorizacao(), function (result) {
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
                            vm.OnDataGridCreated('CadastroTransacaoAutorizacao_dGridTcsTransacaoDependenteAutorizacao');
                        }
                        var selectionrowselectionchanged = null, selectedRowId = -1;
                        selectionrowselectionchanged = function (evt, ui) {
                            if ((ui.owner.grid.selectedRow() && typeof ui.owner.grid.selectedRow().id !== 'undefined') || (ui.owner.grid.selectedRows() && ui.owner.grid.selectedRows().length > 0)) { 
                                if (isNullOrEmpty(ui.owner.selectedRows())|| ui.selectedRows.length <= 1) {
                                    $(document).undelegate('#CadastroTransacaoAutorizacao_dGridTcsTransacaoDependenteAutorizacao', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                                    ui.owner.clearSelection();
                                    ui.owner.selectRow(ui.row.index);
                                    if (vm.status() === 'Q'){
                                        var gridCell = ui.owner.grid;
                                        grid.find('div.borderCell').remove();
                                        //$(gridCell.cellAt(-1, ui.owner._rowIndex)).append(" < div class='borderCell' style='z-index:100; border: 1px solid #849fd9 !important;'></div>");
                                    }
                                    selectedRowId = ui.row.id;
                                    $(document).delegate ('#CadastroTransacaoAutorizacao_dGridTcsTransacaoDependenteAutorizacao', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                                }
                                selectGridCurrentItem(vm.goToKey, 'RowDataId', ui, vm.currentDataItem().currentTcsTransacaoDependenteAutorizacao, getDataSource()); 
                             } 
                        };
                        $(document).delegate('#CadastroTransacaoAutorizacao_dGridTcsTransacaoDependenteAutorizacao', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                        $('#CadastroTransacaoAutorizacao_dGridTcsTransacaoDependenteAutorizacao > tbody tr').live('focus', function(evt) {
                            var grid = $('#CadastroTransacaoAutorizacao_dGridTcsTransacaoDependenteAutorizacao'), row = $(this).closest('tr'), id = parseInt(row.attr('data-id'), 10);
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
                    vm.addDataSource({ key: 'CadastroTransacaoAutorizacao_dGridTcsTransacaoDependenteAutorizacao', name: 'TcsTransacaoDependenteAutorizacaoList', itemsSource: itemsSource });
                }
            
            
            , ChangedBrandscyCadastroTransacaoAutorizacao_dGrid: function(vm, decimals, reset) {
                var i, format = '0.'.concat('0'.repeat(decimals)), grd =$('#scyCadastroTransacaoAutorizacao_dGrid').data('igGrid'),
                    grdUpd = $('#scyCadastroTransacaoAutorizacao_dGrid').data('igGridUpdating');
                if(isNull(grd) || isNull(grdUpd)) return;
                for (i = 0; i < grd.options.columns.length; i++) {
                }
                for (i = 0; i < grdUpd.options.columnSettings.length; i++) {
                }
                grd.dataBind();
            }
            , renderscyCadastroTransacaoAutorizacao_dGrid: function(vm) {
                var getDataSource = function() {
                    var source = null;
                    try {
                        source = vm.dataView;
                    }
                    catch (e) { }
                    return isNullOrEmpty(source) ? ko.observableArray([]) : source;
                };
                $('#scyCadastroTransacaoAutorizacao_dGrid_headers').live('focus  keydown', function (evt) {
                    var keyCode = window.event ? evt.which : evt.keyCode;
                    if (keyCode === 9) {
                        var cols = $('#scyCadastroTransacaoAutorizacao_dGrid').igGrid('option', 'columns');
                        var dataView = $('#scyCadastroTransacaoAutorizacao_dGrid').data('igGrid').dataSource._dataView
                        if (dataView.length === 0) return;
                        var firstRow = dataView[0].RowDataId;
                        clear = vm.status() === 'C';
                        if (vm.status() === 'C')
                            $('#scyCadastroTransacaoAutorizacao_dGrid').igGridUpdating('startEdit', firstRow, 0, true);
                        else {
                            var entity = findElementByKey(getDataSource(), 'RowDataId', firstRow);
                            var indexColumn = 0;
                            cols.some(function (entry) {
                                if (entry.key !== 'RowDataId' && !entry.hidden) {
                                    if (verifyCanEditCol(entry.key, clear, entity)) {
                                        $('#scyCadastroTransacaoAutorizacao_dGrid').igGridUpdating('startEdit', firstRow, indexColumn, true);
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
                   if($('#scyCadastroTransacaoAutorizacao_dGrid').data('igGrid') === undefined) return '';
                   var cols = $('#scyCadastroTransacaoAutorizacao_dGrid').igGrid('option', 'columns');
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
                  if (!grid) grid = $('#scyCadastroTransacaoAutorizacao_dGrid');
                  return ((!grid[0] || (!forceCreating && grid.parent().width() <= 0)) && !$('#dialog').is(':visible'));
                }
                var refreshData = true;
                var itemsSource = { isElementHided: isElementHided, getVisibleColumns: getVisibleColumns, containerId: 'scyCadastroTransacaoAutorizacao_dGrid_container', dataBind: function (commitData, forceCreating) {
                   var grid = $('#scyCadastroTransacaoAutorizacao_dGrid');
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
                       $('#scyCadastroTransacaoAutorizacao_dGrid_groupbyarea').addClass('hide');
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
                        case 'CodTransacao': { canEditing = clear; break;}
                        case 'NomeCurto': { canEditing = clear; break;}
                        case 'ClasseNome': { canEditing = clear; break;}
                        case 'DescObjeto': { canEditing = clear; break;}
                        case 'DescTransacao': { canEditing = clear; break;}
                        case 'Inativo': { canEditing = clear; break;}
                        case 'LxTipoTransacao': { canEditing = clear; break;}
                        case 'Icone': { canEditing = clear; break;}
                        case 'DescModulo': { canEditing = clear; break;}
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
                            $('#scyCadastroTransacaoAutorizacao_dGrid_LayoutBtn').igPopover('hide');
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
                                      showMultimidia(entity, e, table, key, vm.CadastroTransacaoAutorizacao());
                                 }
                             }
                             if (typeof vm.OnGridClientClick === 'function') {
                                 entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowKey);
                                 vm.OnGridClientClick('scyCadastroTransacaoAutorizacao_dGrid', ui.colKey, entity);
                             }
                             if (vm.status() != 'Q') {
                                 var grid = $('#scyCadastroTransacaoAutorizacao_dGrid');
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
                            { key: 'CodTransacao', headerText: vm.getLayoutDisplayName('CadastroTransacaoAutorizacao_tbCodTransacao'), width: '114px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'NomeCurto', headerText: vm.getLayoutDisplayName('CadastroTransacaoAutorizacao_tbNomeCurto'), width: '400px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'ClasseNome', headerText: vm.getLayoutDisplayName('CadastroTransacaoAutorizacao_tbClasseNome'), width: '400px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'DescObjeto', headerText: vm.getLayoutDisplayName('CadastroTransacaoAutorizacao_lUpDescObjeto'), width: '421px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'DescTransacao', headerText: vm.getLayoutDisplayName('CadastroTransacaoAutorizacao_tbDescTransacao'), width: '400px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'Inativo', headerText: vm.getLayoutDisplayName('CadastroTransacaoAutorizacao_ckInativo'), width: '127px', dataType: 'bool', columnCssClass: 'ellipsis', format: 'checkbox', hidden: false, unbound: false, group: null   },
                            { key: 'LxTipoTransacao', headerText: vm.getLayoutDisplayName('CadastroTransacaoAutorizacao_cmbLxTipoTransacao'), width: '218px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null  , formatter: function (val, record) { return  vm.dataDomains.getName('TipoTransacao', val);} },
                            { key: 'Icone', headerText: vm.getLayoutDisplayName('CadastroTransacaoAutorizacao_tbIcone'), width: '400px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'DescModulo', headerText: vm.getLayoutDisplayName('CadastroTransacaoAutorizacao_lUpDescModulo'), width: '421px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   }
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
                        
                                        var dataView = $('#scyCadastroTransacaoAutorizacao_dGrid').data('igGrid').dataSource;
                                        if (dataView.settings.filtering.expressions.length <= 0)
                                            dataView._filteredData = [];
                        
                                        var col = ui.owner._dialogCurrentColumn;
                        
                                        var divDialog = $('#' + dgl[0].id).find('.ui-iggrid-filterdialogaddcondition').find('span')[0];
                        
                                        var scriptHtml = '<div id="' + divDinamica + '">';
                                        scriptHtml += '  <script>';
                                        scriptHtml += '    var newCol = "' + col + '";';
                                        scriptHtml += '    var newGrid = $("#scyCadastroTransacaoAutorizacao_dGrid");';
                                        scriptHtml += '    var listFilter = [];';
                                        scriptHtml += '    function hideColumn(){';
                                        scriptHtml += '     if ($("#showHideColumn_scyCadastroTransacaoAutorizacao_dGrid")["0"].innerHTML.indexOf("Ocultar") >= 0) {';
                                        scriptHtml += '        $("#showHideColumn_scyCadastroTransacaoAutorizacao_dGrid")["0"].innerHTML = "Mostrar Coluna";';
                                        scriptHtml += '        newGrid.igGridHiding("hideColumn", newCol);';
                                        scriptHtml += '     }';
                                        scriptHtml += '     else{';
                                        scriptHtml += '        $("#showHideColumn_scyCadastroTransacaoAutorizacao_dGrid")["0"].innerHTML = "Ocultar Coluna";';
                                        scriptHtml += '        newGrid.igGridHiding("showColumn", newCol);';
                                        scriptHtml += '     }';
                                        scriptHtml += '    }';
                                        scriptHtml += '    function updateHideButton(){';
                                        scriptHtml += '         var column = $.grep(newGrid.igGrid("option", "columns"), function (element, index) { return element.key == newCol });';
                                        scriptHtml += '         if (column.length > 0){';
                                        scriptHtml += '             $("#showHideColumn_scyCadastroTransacaoAutorizacao_dGrid")["0"].innerHTML = column[0].hidden ? "Mostrar Coluna" : "Ocultar Coluna"';
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
                                        scriptHtml += '              <div id="comboFields_scyCadastroTransacaoAutorizacao_dGrid"></div>';
                                        scriptHtml += '              <script>';
                                        scriptHtml += '                  var columns = newGrid.igGrid("option", "columns");';
                                        scriptHtml += '                  $("#comboFields_scyCadastroTransacaoAutorizacao_dGrid").igCombo({ dataSource: columns, mode : "dropdown", valueKey: "key", textKey: "headerText", selectionChanging: function (evt, ui) {';
                                        scriptHtml += '                      newCol = ui.items["0"].data.key;';
                                        scriptHtml += '                      updateHideButton()';
                                        scriptHtml += '                  }});';
                                        scriptHtml += '                  $("#comboFields_scyCadastroTransacaoAutorizacao_dGrid").igCombo("value", newCol);';
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
                                        scriptHtml += '              <i class="fa fa-eye-slash" aria-hidden="true" style="margin-right: 5px;"></i><a onclick="hideColumn()" style="cursor: pointer" id="showHideColumn_scyCadastroTransacaoAutorizacao_dGrid">Ocultar Coluna</a>';
                                        scriptHtml += '          </div>';
                                        scriptHtml += '          <br>';
                                        scriptHtml += '      </div>';
                                        scriptHtml += '  </div>';
                        
                                        $(scriptHtml).insertBefore(divDialog);
                                   },
                             },
                                    { name: 'Selection', mode: 'row'
                                    }, 
                                    { name: 'Tooltips', columnSettings:[{ columnKey: "CodTransacao", allowTooltips: true },{ columnKey: "NomeCurto", allowTooltips: true },{ columnKey: "ClasseNome", allowTooltips: true },{ columnKey: "DescObjeto", allowTooltips: false },{ columnKey: "DescTransacao", allowTooltips: true },{ columnKey: "Inativo", allowTooltips: true },{ columnKey: "LxTipoTransacao", allowTooltips: true },{ columnKey: "Icone", allowTooltips: true },{ columnKey: "DescModulo", allowTooltips: false }] },
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
                                      columnSettings: [{ columnKey: "DescObjeto", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpTcsObjetoAutorizacao", isNullable: false, custom: vm.custom, vm: vm, verifyCanEditCol: verifyCanEditCol, allowMultiSelectionInSearch:true, activateAutoComplete: false, autoCompleteMaxResults: 7, validateOnClearState:false, maxValue:null, maxLength: 400, defaultValue: '' } }, { columnKey: "DescModulo", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpTcsModuloAutorizacao", isNullable: true, custom: vm.custom, vm: vm, verifyCanEditCol: verifyCanEditCol, allowMultiSelectionInSearch:true, activateAutoComplete: false, autoCompleteMaxResults: 7, validateOnClearState:false, maxValue:null, maxLength: 60, defaultValue: '' } }, { columnKey: 'LxTipoTransacao', editorType: 'combo', editorOptions: {   selectionChanged: function (evt, ui) {   var val = null;   if (ui.items != null && ui.items.length > 0) { val = ui.items[0].data['id']; }  updateEntity('LxTipoTransacao', val, false); },  mode: 'dropdown', dropDownOnFocus: true,  dataSource: vm.dataDomains.getItems('TipoTransacao', ''),  textKey: 'name', valueKey: 'id', enableClearButton: false }}, { columnKey: "CodTransacao" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('scyCadastroTransacaoAutorizacao_dGrid', 'CodTransacao', ui.oldValue, ui.value);}}, maxLength: 10 } }, { columnKey: "NomeCurto" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('scyCadastroTransacaoAutorizacao_dGrid', 'NomeCurto', ui.oldValue, ui.value);}}, maxLength: 40 } }, { columnKey: "ClasseNome" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('scyCadastroTransacaoAutorizacao_dGrid', 'ClasseNome', ui.oldValue, ui.value);}}, maxLength: 400 } }, { columnKey: "DescTransacao" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('scyCadastroTransacaoAutorizacao_dGrid', 'DescTransacao', ui.oldValue, ui.value);}}, maxLength: 60 } }, { columnKey: "Icone" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('scyCadastroTransacaoAutorizacao_dGrid', 'Icone', ui.oldValue, ui.value);}}, maxLength: 40 } }],
                                      rowDeleting: function (evt, ui) {
                                          deletedIndex = ui.element.context.rowIndex;
                                          var entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowID);
                                          if (entity) {
                                              vm.deleteEntity(entity);
                                          }
                                      },
                                      rowDeleted: function (evt, ui) {
                                          var grid = $('#scyCadastroTransacaoAutorizacao_dGrid');
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
                                          var columns = $('#scyCadastroTransacaoAutorizacao_dGrid').igGridUpdating('option', 'columnSettings');
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
                            vm.OnDataGridCreated('scyCadastroTransacaoAutorizacao_dGrid');
                        }
                        var selectionrowselectionchanged = null, selectedRowId = -1;
                        selectionrowselectionchanged = function (evt, ui) {
                            if ((ui.owner.grid.selectedRow() && typeof ui.owner.grid.selectedRow().id !== 'undefined') || (ui.owner.grid.selectedRows() && ui.owner.grid.selectedRows().length > 0)) { 
                                if (isNullOrEmpty(ui.owner.selectedRows())|| ui.selectedRows.length <= 1) {
                                    $(document).undelegate('#scyCadastroTransacaoAutorizacao_dGrid', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                                    ui.owner.clearSelection();
                                    ui.owner.selectRow(ui.row.index);
                                    if (vm.status() === 'Q'){
                                        var gridCell = ui.owner.grid;
                                        grid.find('div.borderCell').remove();
                                        //$(gridCell.cellAt(-1, ui.owner._rowIndex)).append(" < div class='borderCell' style='z-index:100; border: 1px solid #849fd9 !important;'></div>");
                                    }
                                    selectedRowId = ui.row.id;
                                    $(document).delegate ('#scyCadastroTransacaoAutorizacao_dGrid', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                                }
                                selectGridCurrentItem(vm.goToKey, 'RowDataId', ui); 
                             } 
                        };
                        $(document).delegate('#scyCadastroTransacaoAutorizacao_dGrid', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                        $('#scyCadastroTransacaoAutorizacao_dGrid > tbody tr').live('focus', function(evt) {
                            var grid = $('#scyCadastroTransacaoAutorizacao_dGrid'), row = $(this).closest('tr'), id = parseInt(row.attr('data-id'), 10);
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
                            if (vm.CadastroTransacaoAutorizacao().status() === 'Q') vm.CadastroTransacaoAutorizacao().dataToolbar.viewInfo();
                        });
                    }
                    vm.addDataSource({ key: 'scyCadastroTransacaoAutorizacao_dGrid', name: 'dataView', itemsSource: itemsSource });
                }
            
        };
        
        return complement;
    }
    
    return complementCtor;
});
