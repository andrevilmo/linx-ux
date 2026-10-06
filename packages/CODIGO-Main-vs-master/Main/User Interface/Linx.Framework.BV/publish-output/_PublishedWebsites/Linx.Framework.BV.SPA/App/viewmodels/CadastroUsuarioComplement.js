define(['managers/__auth', 'managers/user'], function (managerAuth, managerUser) {
    var complementCtor = function() {
        var complement = {
            isAutomatic: true
            , ChangedBrandCadastroUsuario_dGridTcsUsuarioPerfil: function(vm, decimals, reset) {
                var i, format = '0.'.concat('0'.repeat(decimals)), grd =$('#CadastroUsuario_dGridTcsUsuarioPerfil').data('igGrid'),
                    grdUpd = $('#CadastroUsuario_dGridTcsUsuarioPerfil').data('igGridUpdating');
                if(isNull(grd) || isNull(grdUpd)) return;
                for (i = 0; i < grd.options.columns.length; i++) {
                }
                for (i = 0; i < grdUpd.options.columnSettings.length; i++) {
                }
                grd.dataBind();
            }
            , renderCadastroUsuario_dGridTcsUsuarioPerfil: function(vm) {
                var getDataSource = function() {
                    var source = null;
                    try {
                        source = vm.currentDataItem().TcsUsuarioPerfilList;
                    }
                    catch (e) { }
                    return isNullOrEmpty(source) ? ko.observableArray([]) : source;
                };
                var dataSourceIsLoaded = function() {
                    var isLoaded = false;
                    try {
                        isLoaded = (vm.currentDataItem().TcsUsuarioPerfilIsLoaded === true || vm.currentDataItem().TcsUsuarioPerfilList().length > 0);
                    }
                    catch (e) {
                        isLoaded = true;
                    }
                    return isLoaded;
                }
                $('#CadastroUsuario_dGridTcsUsuarioPerfil_headers').live('focus  keydown', function (evt) {
                    var keyCode = window.event ? evt.which : evt.keyCode;
                    if (keyCode === 9) {
                        var cols = $('#CadastroUsuario_dGridTcsUsuarioPerfil').igGrid('option', 'columns');
                        var dataView = $('#CadastroUsuario_dGridTcsUsuarioPerfil').data('igGrid').dataSource._dataView
                        if (dataView.length === 0) return;
                        var firstRow = dataView[0].RowDataId;
                        clear = vm.status() === 'C';
                        if (vm.status() === 'C')
                            $('#CadastroUsuario_dGridTcsUsuarioPerfil').igGridUpdating('startEdit', firstRow, 0, true);
                        else {
                            var entity = findElementByKey(getDataSource(), 'RowDataId', firstRow);
                            var indexColumn = 0;
                            cols.some(function (entry) {
                                if (entry.key !== 'RowDataId' && !entry.hidden) {
                                    if (verifyCanEditCol(entry.key, clear, entity)) {
                                        $('#CadastroUsuario_dGridTcsUsuarioPerfil').igGridUpdating('startEdit', firstRow, indexColumn, true);
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
                   if($('#CadastroUsuario_dGridTcsUsuarioPerfil').data('igGrid') === undefined) return '';
                   var cols = $('#CadastroUsuario_dGridTcsUsuarioPerfil').igGrid('option', 'columns');
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
                  if (!grid) grid = $('#CadastroUsuario_dGridTcsUsuarioPerfil');
                  return ((!grid[0] || (!forceCreating && grid.parent().width() <= 0)) && !$('#dialogTcsUsuarioPerfil').is(':visible'));
                }
                var refreshData = true;
                var itemsSource = { isElementHided: isElementHided, getVisibleColumns: getVisibleColumns, containerId: 'CadastroUsuario_dGridTcsUsuarioPerfil_container', dataBind: function (commitData, forceCreating) {
                   var grid = $('#CadastroUsuario_dGridTcsUsuarioPerfil');
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
                       $('#CadastroUsuario_dGridTcsUsuarioPerfil_groupbyarea').addClass('hide');
                   }
                   if (grid.igGridUpdating('isEditing')) {
                        grid.igGridUpdating('endEdit', true);
                   }
                   if (execFillDetais) {
                     grid.igGrid("option", "dataSource", []);
                     vm.currentDataItem().fillDetails(false, 'TcsUsuarioPerfil');
                     return;
                   }
                   grid.igGrid("option", "dataSource", unwrapObservableArray(getDataSource(), vm));
                   var rows = grid.igGrid('allRows');
                   if (rows.length > 0) {
                     var verticalContainer = grid.igGrid('scrollContainer');
                     var isSelected = false;
                     if (vm.currentDataItem().currentTcsUsuarioPerfil() != null)
                     {
                       for(var idx = 0; idx < rows.length; idx++)
                       {
                         if (rows[idx].dataset.id == getAbsoluteValue(vm.currentDataItem().currentTcsUsuarioPerfil().RowDataId))
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
                     if ($('#dialogTcsUsuarioPerfil').is(':visible')) {
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
                           $('label#currentNumberTcsUsuarioPerfil').html(current + ' - ' + totalCurrentPage);
                        }
                        else
                           $('label#currentNumberTcsUsuarioPerfil').html(1);
                        $('label#totalNumberTcsUsuarioPerfil').html(totalGrid);
                    }
                   } else {
                       $('label#currentNumberTcsUsuarioPerfil').html(0);
                       $('label#totalNumberTcsUsuarioPerfil').html(0);
                   }
                }};
                var valueGrouBy = -1;
                var deletedIndex = -1;
                function verifyCanEditCol(column, clear, entity){
                    switch(column){
                        case 'DescPerfil': { canEditing = clear || (entity && entity.isAdded()); break;}
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
                            $('#CadastroUsuario_dGridTcsUsuarioPerfil_LayoutBtn').igPopover('hide');
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
                                      showMultimidia(entity, e, table, key, vm.CadastroUsuario());
                                 }
                             }
                             if (typeof vm.OnGridClientClick === 'function') {
                                 entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowKey);
                                 vm.OnGridClientClick('CadastroUsuario_dGridTcsUsuarioPerfil', ui.colKey, entity);
                             }
                             if (vm.status() != 'Q') {
                                 var grid = $('#CadastroUsuario_dGridTcsUsuarioPerfil');
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
                            { key: 'DescPerfil', headerText: vm.getLayoutHeaderGrid('CadastroUsuario_lUpTcsUsuarioPerfil_DescPerfil'), width: '421px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   }
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
                        
                                        var dataView = $('#CadastroUsuario_dGridTcsUsuarioPerfil').data('igGrid').dataSource;
                                        if (dataView.settings.filtering.expressions.length <= 0)
                                            dataView._filteredData = [];
                        
                                        var col = ui.owner._dialogCurrentColumn;
                        
                                        var divDialog = $('#' + dgl[0].id).find('.ui-iggrid-filterdialogaddcondition').find('span')[0];
                        
                                        var scriptHtml = '<div id="' + divDinamica + '">';
                                        scriptHtml += '  <script>';
                                        scriptHtml += '    var newCol = "' + col + '";';
                                        scriptHtml += '    var newGrid = $("#CadastroUsuario_dGridTcsUsuarioPerfil");';
                                        scriptHtml += '    var listFilter = [];';
                                        scriptHtml += '    function hideColumn(){';
                                        scriptHtml += '     if ($("#showHideColumn_CadastroUsuario_dGridTcsUsuarioPerfil")["0"].innerHTML.indexOf("Ocultar") >= 0) {';
                                        scriptHtml += '        $("#showHideColumn_CadastroUsuario_dGridTcsUsuarioPerfil")["0"].innerHTML = "Mostrar Coluna";';
                                        scriptHtml += '        newGrid.igGridHiding("hideColumn", newCol);';
                                        scriptHtml += '     }';
                                        scriptHtml += '     else{';
                                        scriptHtml += '        $("#showHideColumn_CadastroUsuario_dGridTcsUsuarioPerfil")["0"].innerHTML = "Ocultar Coluna";';
                                        scriptHtml += '        newGrid.igGridHiding("showColumn", newCol);';
                                        scriptHtml += '     }';
                                        scriptHtml += '    }';
                                        scriptHtml += '    function updateHideButton(){';
                                        scriptHtml += '         var column = $.grep(newGrid.igGrid("option", "columns"), function (element, index) { return element.key == newCol });';
                                        scriptHtml += '         if (column.length > 0){';
                                        scriptHtml += '             $("#showHideColumn_CadastroUsuario_dGridTcsUsuarioPerfil")["0"].innerHTML = column[0].hidden ? "Mostrar Coluna" : "Ocultar Coluna"';
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
                                        scriptHtml += '              <div id="comboFields_CadastroUsuario_dGridTcsUsuarioPerfil"></div>';
                                        scriptHtml += '              <script>';
                                        scriptHtml += '                  var columns = newGrid.igGrid("option", "columns");';
                                        scriptHtml += '                  $("#comboFields_CadastroUsuario_dGridTcsUsuarioPerfil").igCombo({ dataSource: columns, mode : "dropdown", valueKey: "key", textKey: "headerText", selectionChanging: function (evt, ui) {';
                                        scriptHtml += '                      newCol = ui.items["0"].data.key;';
                                        scriptHtml += '                      updateHideButton()';
                                        scriptHtml += '                  }});';
                                        scriptHtml += '                  $("#comboFields_CadastroUsuario_dGridTcsUsuarioPerfil").igCombo("value", newCol);';
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
                                        scriptHtml += '              <i class="fa fa-eye-slash" aria-hidden="true" style="margin-right: 5px;"></i><a onclick="hideColumn()" style="cursor: pointer" id="showHideColumn_CadastroUsuario_dGridTcsUsuarioPerfil">Ocultar Coluna</a>';
                                        scriptHtml += '          </div>';
                                        scriptHtml += '          <br>';
                                        scriptHtml += '      </div>';
                                        scriptHtml += '  </div>';
                        
                                        $(scriptHtml).insertBefore(divDialog);
                                   },
                             },
                                    { name: 'Selection', mode: 'row'
                                    }, 
                                    { name: 'Tooltips', columnSettings:[{ columnKey: "DescPerfil", allowTooltips: false }] },
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
                                      columnSettings: [{ columnKey: "DescPerfil", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpTcsPerfil", isNullable: false, custom: vm.custom, vm: vm, verifyCanEditCol: verifyCanEditCol, allowMultiSelectionInSearch:true, activateAutoComplete: false, autoCompleteMaxResults: 7, validateOnClearState:false, maxValue:null, maxLength: 60, defaultValue: '' } }],
                                      rowDeleting: function (evt, ui) {
                                          deletedIndex = ui.element.context.rowIndex;
                                          var entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowID);
                                          if (entity) {
                                              vm.deleteEntity(entity);
                                          }
                                      },
                                      rowDeleted: function (evt, ui) {
                                          var grid = $('#CadastroUsuario_dGridTcsUsuarioPerfil');
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
                                          var columns = $('#CadastroUsuario_dGridTcsUsuarioPerfil').igGridUpdating('option', 'columnSettings');
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
                                                     vm.dataCombo.fillDataCombos(lookUpName, ui.columnKey, vm.currentDataItem().currentTcsUsuarioPerfil(), function (result) {
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
                            vm.OnDataGridCreated('CadastroUsuario_dGridTcsUsuarioPerfil');
                        }
                        var selectionrowselectionchanged = null, selectedRowId = -1;
                        selectionrowselectionchanged = function (evt, ui) {
                            if ((ui.owner.grid.selectedRow() && typeof ui.owner.grid.selectedRow().id !== 'undefined') || (ui.owner.grid.selectedRows() && ui.owner.grid.selectedRows().length > 0)) { 
                                if (isNullOrEmpty(ui.owner.selectedRows())|| ui.selectedRows.length <= 1) {
                                    $(document).undelegate('#CadastroUsuario_dGridTcsUsuarioPerfil', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                                    ui.owner.clearSelection();
                                    ui.owner.selectRow(ui.row.index);
                                    if (vm.status() === 'Q'){
                                        var gridCell = ui.owner.grid;
                                        grid.find('div.borderCell').remove();
                                        //$(gridCell.cellAt(-1, ui.owner._rowIndex)).append(" < div class='borderCell' style='z-index:100; border: 1px solid #849fd9 !important;'></div>");
                                    }
                                    selectedRowId = ui.row.id;
                                    $(document).delegate ('#CadastroUsuario_dGridTcsUsuarioPerfil', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                                }
                                selectGridCurrentItem(vm.goToKey, 'RowDataId', ui, vm.currentDataItem().currentTcsUsuarioPerfil, getDataSource()); 
                             } 
                        };
                        $(document).delegate('#CadastroUsuario_dGridTcsUsuarioPerfil', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                        $('#CadastroUsuario_dGridTcsUsuarioPerfil > tbody tr').live('focus', function(evt) {
                            var grid = $('#CadastroUsuario_dGridTcsUsuarioPerfil'), row = $(this).closest('tr'), id = parseInt(row.attr('data-id'), 10);
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
                    vm.addDataSource({ key: 'CadastroUsuario_dGridTcsUsuarioPerfil', name: 'TcsUsuarioPerfilList', itemsSource: itemsSource });
                }
            
            , ChangedBrandCadastroUsuario_dGridTcsUsuarioRegraModulo: function(vm, decimals, reset) {
                var i, format = '0.'.concat('0'.repeat(decimals)), grd =$('#CadastroUsuario_dGridTcsUsuarioRegraModulo').data('igGrid'),
                    grdUpd = $('#CadastroUsuario_dGridTcsUsuarioRegraModulo').data('igGridUpdating');
                if(isNull(grd) || isNull(grdUpd)) return;
                for (i = 0; i < grd.options.columns.length; i++) {
                }
                for (i = 0; i < grdUpd.options.columnSettings.length; i++) {
                }
                grd.dataBind();
            }
            , renderCadastroUsuario_dGridTcsUsuarioRegraModulo: function(vm) {
                var getDataSource = function() {
                    var source = null;
                    try {
                        source = vm.currentDataItem().TcsUsuarioRegraModuloList;
                    }
                    catch (e) { }
                    return isNullOrEmpty(source) ? ko.observableArray([]) : source;
                };
                var dataSourceIsLoaded = function() {
                    var isLoaded = false;
                    try {
                        isLoaded = (vm.currentDataItem().TcsUsuarioRegraModuloIsLoaded === true || vm.currentDataItem().TcsUsuarioRegraModuloList().length > 0);
                    }
                    catch (e) {
                        isLoaded = true;
                    }
                    return isLoaded;
                }
                $('#CadastroUsuario_dGridTcsUsuarioRegraModulo_headers').live('focus  keydown', function (evt) {
                    var keyCode = window.event ? evt.which : evt.keyCode;
                    if (keyCode === 9) {
                        var cols = $('#CadastroUsuario_dGridTcsUsuarioRegraModulo').igGrid('option', 'columns');
                        var dataView = $('#CadastroUsuario_dGridTcsUsuarioRegraModulo').data('igGrid').dataSource._dataView
                        if (dataView.length === 0) return;
                        var firstRow = dataView[0].RowDataId;
                        clear = vm.status() === 'C';
                        if (vm.status() === 'C')
                            $('#CadastroUsuario_dGridTcsUsuarioRegraModulo').igGridUpdating('startEdit', firstRow, 0, true);
                        else {
                            var entity = findElementByKey(getDataSource(), 'RowDataId', firstRow);
                            var indexColumn = 0;
                            cols.some(function (entry) {
                                if (entry.key !== 'RowDataId' && !entry.hidden) {
                                    if (verifyCanEditCol(entry.key, clear, entity)) {
                                        $('#CadastroUsuario_dGridTcsUsuarioRegraModulo').igGridUpdating('startEdit', firstRow, indexColumn, true);
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
                   if($('#CadastroUsuario_dGridTcsUsuarioRegraModulo').data('igGrid') === undefined) return '';
                   var cols = $('#CadastroUsuario_dGridTcsUsuarioRegraModulo').igGrid('option', 'columns');
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
                  if (!grid) grid = $('#CadastroUsuario_dGridTcsUsuarioRegraModulo');
                  return ((!grid[0] || (!forceCreating && grid.parent().width() <= 0)) && !$('#dialogTcsUsuarioRegraModulo').is(':visible'));
                }
                var refreshData = true;
                var itemsSource = { isElementHided: isElementHided, getVisibleColumns: getVisibleColumns, containerId: 'CadastroUsuario_dGridTcsUsuarioRegraModulo_container', dataBind: function (commitData, forceCreating) {
                   var grid = $('#CadastroUsuario_dGridTcsUsuarioRegraModulo');
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
                       $('#CadastroUsuario_dGridTcsUsuarioRegraModulo_groupbyarea').addClass('hide');
                   }
                   if (grid.igGridUpdating('isEditing')) {
                        grid.igGridUpdating('endEdit', true);
                   }
                   if (execFillDetais) {
                     grid.igGrid("option", "dataSource", []);
                     vm.currentDataItem().fillDetails(false, 'TcsUsuarioRegraModulo');
                     return;
                   }
                   grid.igGrid("option", "dataSource", unwrapObservableArray(getDataSource(), vm));
                   var rows = grid.igGrid('allRows');
                   if (rows.length > 0) {
                     var verticalContainer = grid.igGrid('scrollContainer');
                     var isSelected = false;
                     if (vm.currentDataItem().currentTcsUsuarioRegraModulo() != null)
                     {
                       for(var idx = 0; idx < rows.length; idx++)
                       {
                         if (rows[idx].dataset.id == getAbsoluteValue(vm.currentDataItem().currentTcsUsuarioRegraModulo().RowDataId))
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
                     if ($('#dialogTcsUsuarioRegraModulo').is(':visible')) {
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
                           $('label#currentNumberTcsUsuarioRegraModulo').html(current + ' - ' + totalCurrentPage);
                        }
                        else
                           $('label#currentNumberTcsUsuarioRegraModulo').html(1);
                        $('label#totalNumberTcsUsuarioRegraModulo').html(totalGrid);
                    }
                   } else {
                       $('label#currentNumberTcsUsuarioRegraModulo').html(0);
                       $('label#totalNumberTcsUsuarioRegraModulo').html(0);
                   }
                }};
                var valueGrouBy = -1;
                var deletedIndex = -1;
                function verifyCanEditCol(column, clear, entity){
                    switch(column){
                        case 'DescModulo': { canEditing = clear || (entity && entity.isAdded()); break;}
                        case 'DescAplicativo': { canEditing = clear; break;}
                        case 'LxRegraAcessoModulo': { canEditing = clear || vm.enabledForEditing(); break;}
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
                            $('#CadastroUsuario_dGridTcsUsuarioRegraModulo_LayoutBtn').igPopover('hide');
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
                                      showMultimidia(entity, e, table, key, vm.CadastroUsuario());
                                 }
                             }
                             if (typeof vm.OnGridClientClick === 'function') {
                                 entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowKey);
                                 vm.OnGridClientClick('CadastroUsuario_dGridTcsUsuarioRegraModulo', ui.colKey, entity);
                             }
                             if (vm.status() != 'Q') {
                                 var grid = $('#CadastroUsuario_dGridTcsUsuarioRegraModulo');
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
                            { key: 'DescModulo', headerText: vm.getLayoutHeaderGrid('CadastroUsuario_lUpTcsUsuarioRegraModulo_DescModulo'), width: '421px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'DescAplicativo', headerText: vm.getLayoutHeaderGrid('CadastroUsuario_tbTcsUsuarioRegraModulo_DescAplicativo'), width: '400px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'LxRegraAcessoModulo', headerText: vm.getLayoutHeaderGrid('CadastroUsuario_cmbTcsUsuarioRegraModulo_LxRegraAcessoModulo'), width: '283px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null  , formatter: function (val, record) { return  vm.dataDomains.getName('RegraAcesso', val);} }
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
                        
                                        var dataView = $('#CadastroUsuario_dGridTcsUsuarioRegraModulo').data('igGrid').dataSource;
                                        if (dataView.settings.filtering.expressions.length <= 0)
                                            dataView._filteredData = [];
                        
                                        var col = ui.owner._dialogCurrentColumn;
                        
                                        var divDialog = $('#' + dgl[0].id).find('.ui-iggrid-filterdialogaddcondition').find('span')[0];
                        
                                        var scriptHtml = '<div id="' + divDinamica + '">';
                                        scriptHtml += '  <script>';
                                        scriptHtml += '    var newCol = "' + col + '";';
                                        scriptHtml += '    var newGrid = $("#CadastroUsuario_dGridTcsUsuarioRegraModulo");';
                                        scriptHtml += '    var listFilter = [];';
                                        scriptHtml += '    function hideColumn(){';
                                        scriptHtml += '     if ($("#showHideColumn_CadastroUsuario_dGridTcsUsuarioRegraModulo")["0"].innerHTML.indexOf("Ocultar") >= 0) {';
                                        scriptHtml += '        $("#showHideColumn_CadastroUsuario_dGridTcsUsuarioRegraModulo")["0"].innerHTML = "Mostrar Coluna";';
                                        scriptHtml += '        newGrid.igGridHiding("hideColumn", newCol);';
                                        scriptHtml += '     }';
                                        scriptHtml += '     else{';
                                        scriptHtml += '        $("#showHideColumn_CadastroUsuario_dGridTcsUsuarioRegraModulo")["0"].innerHTML = "Ocultar Coluna";';
                                        scriptHtml += '        newGrid.igGridHiding("showColumn", newCol);';
                                        scriptHtml += '     }';
                                        scriptHtml += '    }';
                                        scriptHtml += '    function updateHideButton(){';
                                        scriptHtml += '         var column = $.grep(newGrid.igGrid("option", "columns"), function (element, index) { return element.key == newCol });';
                                        scriptHtml += '         if (column.length > 0){';
                                        scriptHtml += '             $("#showHideColumn_CadastroUsuario_dGridTcsUsuarioRegraModulo")["0"].innerHTML = column[0].hidden ? "Mostrar Coluna" : "Ocultar Coluna"';
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
                                        scriptHtml += '              <div id="comboFields_CadastroUsuario_dGridTcsUsuarioRegraModulo"></div>';
                                        scriptHtml += '              <script>';
                                        scriptHtml += '                  var columns = newGrid.igGrid("option", "columns");';
                                        scriptHtml += '                  $("#comboFields_CadastroUsuario_dGridTcsUsuarioRegraModulo").igCombo({ dataSource: columns, mode : "dropdown", valueKey: "key", textKey: "headerText", selectionChanging: function (evt, ui) {';
                                        scriptHtml += '                      newCol = ui.items["0"].data.key;';
                                        scriptHtml += '                      updateHideButton()';
                                        scriptHtml += '                  }});';
                                        scriptHtml += '                  $("#comboFields_CadastroUsuario_dGridTcsUsuarioRegraModulo").igCombo("value", newCol);';
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
                                        scriptHtml += '              <i class="fa fa-eye-slash" aria-hidden="true" style="margin-right: 5px;"></i><a onclick="hideColumn()" style="cursor: pointer" id="showHideColumn_CadastroUsuario_dGridTcsUsuarioRegraModulo">Ocultar Coluna</a>';
                                        scriptHtml += '          </div>';
                                        scriptHtml += '          <br>';
                                        scriptHtml += '      </div>';
                                        scriptHtml += '  </div>';
                        
                                        $(scriptHtml).insertBefore(divDialog);
                                   },
                             },
                                    { name: 'Selection', mode: 'row'
                                    }, 
                                    { name: 'Tooltips', columnSettings:[{ columnKey: "DescModulo", allowTooltips: false },{ columnKey: "DescAplicativo", allowTooltips: true },{ columnKey: "LxRegraAcessoModulo", allowTooltips: true }] },
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
                                      columnSettings: [{ columnKey: "DescModulo", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpTcsUsuarioRegraModulo", isNullable: false, custom: vm.custom, vm: vm, verifyCanEditCol: verifyCanEditCol, allowMultiSelectionInSearch:true, activateAutoComplete: false, autoCompleteMaxResults: 7, validateOnClearState:false, maxValue:null, maxLength: 60, defaultValue: '' } }, { columnKey: 'LxRegraAcessoModulo', editorType: 'combo', editorOptions: {   selectionChanged: function (evt, ui) {   var val = null;   if (ui.items != null && ui.items.length > 0) { val = ui.items[0].data['id']; }  updateEntity('LxRegraAcessoModulo', val, false); },  mode: 'dropdown', dropDownOnFocus: true,  dataSource: vm.dataDomains.getItems('RegraAcesso', ''),  textKey: 'name', valueKey: 'id', enableClearButton: false }}, { columnKey: "DescAplicativo" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('CadastroUsuario_dGridTcsUsuarioRegraModulo', 'DescAplicativo', ui.oldValue, ui.value);}}, maxLength: 250 } }],
                                      rowDeleting: function (evt, ui) {
                                          deletedIndex = ui.element.context.rowIndex;
                                          var entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowID);
                                          if (entity) {
                                              vm.deleteEntity(entity);
                                          }
                                      },
                                      rowDeleted: function (evt, ui) {
                                          var grid = $('#CadastroUsuario_dGridTcsUsuarioRegraModulo');
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
                                          var columns = $('#CadastroUsuario_dGridTcsUsuarioRegraModulo').igGridUpdating('option', 'columnSettings');
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
                                                     vm.dataCombo.fillDataCombos(lookUpName, ui.columnKey, vm.currentDataItem().currentTcsUsuarioRegraModulo(), function (result) {
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
                            vm.OnDataGridCreated('CadastroUsuario_dGridTcsUsuarioRegraModulo');
                        }
                        var selectionrowselectionchanged = null, selectedRowId = -1;
                        selectionrowselectionchanged = function (evt, ui) {
                            if ((ui.owner.grid.selectedRow() && typeof ui.owner.grid.selectedRow().id !== 'undefined') || (ui.owner.grid.selectedRows() && ui.owner.grid.selectedRows().length > 0)) { 
                                if (isNullOrEmpty(ui.owner.selectedRows())|| ui.selectedRows.length <= 1) {
                                    $(document).undelegate('#CadastroUsuario_dGridTcsUsuarioRegraModulo', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                                    ui.owner.clearSelection();
                                    ui.owner.selectRow(ui.row.index);
                                    if (vm.status() === 'Q'){
                                        var gridCell = ui.owner.grid;
                                        grid.find('div.borderCell').remove();
                                        //$(gridCell.cellAt(-1, ui.owner._rowIndex)).append(" < div class='borderCell' style='z-index:100; border: 1px solid #849fd9 !important;'></div>");
                                    }
                                    selectedRowId = ui.row.id;
                                    $(document).delegate ('#CadastroUsuario_dGridTcsUsuarioRegraModulo', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                                }
                                selectGridCurrentItem(vm.goToKey, 'RowDataId', ui, vm.currentDataItem().currentTcsUsuarioRegraModulo, getDataSource()); 
                             } 
                        };
                        $(document).delegate('#CadastroUsuario_dGridTcsUsuarioRegraModulo', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                        $('#CadastroUsuario_dGridTcsUsuarioRegraModulo > tbody tr').live('focus', function(evt) {
                            var grid = $('#CadastroUsuario_dGridTcsUsuarioRegraModulo'), row = $(this).closest('tr'), id = parseInt(row.attr('data-id'), 10);
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
                    vm.addDataSource({ key: 'CadastroUsuario_dGridTcsUsuarioRegraModulo', name: 'TcsUsuarioRegraModuloList', itemsSource: itemsSource });
                }
            
            , ChangedBrandCadastroUsuario_dGridTcsUsuarioRegraTransacao: function(vm, decimals, reset) {
                var i, format = '0.'.concat('0'.repeat(decimals)), grd =$('#CadastroUsuario_dGridTcsUsuarioRegraTransacao').data('igGrid'),
                    grdUpd = $('#CadastroUsuario_dGridTcsUsuarioRegraTransacao').data('igGridUpdating');
                if(isNull(grd) || isNull(grdUpd)) return;
                for (i = 0; i < grd.options.columns.length; i++) {
                }
                for (i = 0; i < grdUpd.options.columnSettings.length; i++) {
                }
                grd.dataBind();
            }
            , renderCadastroUsuario_dGridTcsUsuarioRegraTransacao: function(vm) {
                var getDataSource = function() {
                    var source = null;
                    try {
                        source = vm.currentDataItem().TcsUsuarioRegraTransacaoList;
                    }
                    catch (e) { }
                    return isNullOrEmpty(source) ? ko.observableArray([]) : source;
                };
                var dataSourceIsLoaded = function() {
                    var isLoaded = false;
                    try {
                        isLoaded = (vm.currentDataItem().TcsUsuarioRegraTransacaoIsLoaded === true || vm.currentDataItem().TcsUsuarioRegraTransacaoList().length > 0);
                    }
                    catch (e) {
                        isLoaded = true;
                    }
                    return isLoaded;
                }
                $('#CadastroUsuario_dGridTcsUsuarioRegraTransacao_headers').live('focus  keydown', function (evt) {
                    var keyCode = window.event ? evt.which : evt.keyCode;
                    if (keyCode === 9) {
                        var cols = $('#CadastroUsuario_dGridTcsUsuarioRegraTransacao').igGrid('option', 'columns');
                        var dataView = $('#CadastroUsuario_dGridTcsUsuarioRegraTransacao').data('igGrid').dataSource._dataView
                        if (dataView.length === 0) return;
                        var firstRow = dataView[0].RowDataId;
                        clear = vm.status() === 'C';
                        if (vm.status() === 'C')
                            $('#CadastroUsuario_dGridTcsUsuarioRegraTransacao').igGridUpdating('startEdit', firstRow, 0, true);
                        else {
                            var entity = findElementByKey(getDataSource(), 'RowDataId', firstRow);
                            var indexColumn = 0;
                            cols.some(function (entry) {
                                if (entry.key !== 'RowDataId' && !entry.hidden) {
                                    if (verifyCanEditCol(entry.key, clear, entity)) {
                                        $('#CadastroUsuario_dGridTcsUsuarioRegraTransacao').igGridUpdating('startEdit', firstRow, indexColumn, true);
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
                   if($('#CadastroUsuario_dGridTcsUsuarioRegraTransacao').data('igGrid') === undefined) return '';
                   var cols = $('#CadastroUsuario_dGridTcsUsuarioRegraTransacao').igGrid('option', 'columns');
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
                  if (!grid) grid = $('#CadastroUsuario_dGridTcsUsuarioRegraTransacao');
                  return ((!grid[0] || (!forceCreating && grid.parent().width() <= 0)) && !$('#dialogTcsUsuarioRegraTransacao').is(':visible'));
                }
                var refreshData = true;
                var itemsSource = { isElementHided: isElementHided, getVisibleColumns: getVisibleColumns, containerId: 'CadastroUsuario_dGridTcsUsuarioRegraTransacao_container', dataBind: function (commitData, forceCreating) {
                   var grid = $('#CadastroUsuario_dGridTcsUsuarioRegraTransacao');
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
                       $('#CadastroUsuario_dGridTcsUsuarioRegraTransacao_groupbyarea').addClass('hide');
                   }
                   if (grid.igGridUpdating('isEditing')) {
                        grid.igGridUpdating('endEdit', true);
                   }
                   if (execFillDetais) {
                     grid.igGrid("option", "dataSource", []);
                     vm.currentDataItem().fillDetails(false, 'TcsUsuarioRegraTransacao');
                     return;
                   }
                   grid.igGrid("option", "dataSource", unwrapObservableArray(getDataSource(), vm));
                   var rows = grid.igGrid('allRows');
                   if (rows.length > 0) {
                     var verticalContainer = grid.igGrid('scrollContainer');
                     var isSelected = false;
                     if (vm.currentDataItem().currentTcsUsuarioRegraTransacao() != null)
                     {
                       for(var idx = 0; idx < rows.length; idx++)
                       {
                         if (rows[idx].dataset.id == getAbsoluteValue(vm.currentDataItem().currentTcsUsuarioRegraTransacao().RowDataId))
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
                     if ($('#dialogTcsUsuarioRegraTransacao').is(':visible')) {
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
                           $('label#currentNumberTcsUsuarioRegraTransacao').html(current + ' - ' + totalCurrentPage);
                        }
                        else
                           $('label#currentNumberTcsUsuarioRegraTransacao').html(1);
                        $('label#totalNumberTcsUsuarioRegraTransacao').html(totalGrid);
                    }
                   } else {
                       $('label#currentNumberTcsUsuarioRegraTransacao').html(0);
                       $('label#totalNumberTcsUsuarioRegraTransacao').html(0);
                   }
                }};
                var valueGrouBy = -1;
                var deletedIndex = -1;
                function verifyCanEditCol(column, clear, entity){
                    switch(column){
                        case 'DescTransacao': { canEditing = clear || (entity && entity.isAdded()); break;}
                        case 'ClasseNome': { canEditing = clear || vm.enabledForEditing(); break;}
                        case 'LxRegraAcessoTransacao': { canEditing = clear || vm.enabledForEditing(); break;}
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
                            $('#CadastroUsuario_dGridTcsUsuarioRegraTransacao_LayoutBtn').igPopover('hide');
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
                                      showMultimidia(entity, e, table, key, vm.CadastroUsuario());
                                 }
                             }
                             if (typeof vm.OnGridClientClick === 'function') {
                                 entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowKey);
                                 vm.OnGridClientClick('CadastroUsuario_dGridTcsUsuarioRegraTransacao', ui.colKey, entity);
                             }
                             if (vm.status() != 'Q') {
                                 var grid = $('#CadastroUsuario_dGridTcsUsuarioRegraTransacao');
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
                            { key: 'DescTransacao', headerText: vm.getLayoutHeaderGrid('CadastroUsuario_lUpTcsUsuarioRegraTransacao_DescTransacao'), width: '421px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'ClasseNome', headerText: vm.getLayoutHeaderGrid('CadastroUsuario_lUpTcsUsuarioRegraTransacao_ClasseNome'), width: '421px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'LxRegraAcessoTransacao', headerText: vm.getLayoutHeaderGrid('CadastroUsuario_cmbTcsUsuarioRegraTransacao_LxRegraAcessoTransacao'), width: '322px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null  , formatter: function (val, record) { return  vm.dataDomains.getName('RegraAcesso', val);} }
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
                        
                                        var dataView = $('#CadastroUsuario_dGridTcsUsuarioRegraTransacao').data('igGrid').dataSource;
                                        if (dataView.settings.filtering.expressions.length <= 0)
                                            dataView._filteredData = [];
                        
                                        var col = ui.owner._dialogCurrentColumn;
                        
                                        var divDialog = $('#' + dgl[0].id).find('.ui-iggrid-filterdialogaddcondition').find('span')[0];
                        
                                        var scriptHtml = '<div id="' + divDinamica + '">';
                                        scriptHtml += '  <script>';
                                        scriptHtml += '    var newCol = "' + col + '";';
                                        scriptHtml += '    var newGrid = $("#CadastroUsuario_dGridTcsUsuarioRegraTransacao");';
                                        scriptHtml += '    var listFilter = [];';
                                        scriptHtml += '    function hideColumn(){';
                                        scriptHtml += '     if ($("#showHideColumn_CadastroUsuario_dGridTcsUsuarioRegraTransacao")["0"].innerHTML.indexOf("Ocultar") >= 0) {';
                                        scriptHtml += '        $("#showHideColumn_CadastroUsuario_dGridTcsUsuarioRegraTransacao")["0"].innerHTML = "Mostrar Coluna";';
                                        scriptHtml += '        newGrid.igGridHiding("hideColumn", newCol);';
                                        scriptHtml += '     }';
                                        scriptHtml += '     else{';
                                        scriptHtml += '        $("#showHideColumn_CadastroUsuario_dGridTcsUsuarioRegraTransacao")["0"].innerHTML = "Ocultar Coluna";';
                                        scriptHtml += '        newGrid.igGridHiding("showColumn", newCol);';
                                        scriptHtml += '     }';
                                        scriptHtml += '    }';
                                        scriptHtml += '    function updateHideButton(){';
                                        scriptHtml += '         var column = $.grep(newGrid.igGrid("option", "columns"), function (element, index) { return element.key == newCol });';
                                        scriptHtml += '         if (column.length > 0){';
                                        scriptHtml += '             $("#showHideColumn_CadastroUsuario_dGridTcsUsuarioRegraTransacao")["0"].innerHTML = column[0].hidden ? "Mostrar Coluna" : "Ocultar Coluna"';
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
                                        scriptHtml += '              <div id="comboFields_CadastroUsuario_dGridTcsUsuarioRegraTransacao"></div>';
                                        scriptHtml += '              <script>';
                                        scriptHtml += '                  var columns = newGrid.igGrid("option", "columns");';
                                        scriptHtml += '                  $("#comboFields_CadastroUsuario_dGridTcsUsuarioRegraTransacao").igCombo({ dataSource: columns, mode : "dropdown", valueKey: "key", textKey: "headerText", selectionChanging: function (evt, ui) {';
                                        scriptHtml += '                      newCol = ui.items["0"].data.key;';
                                        scriptHtml += '                      updateHideButton()';
                                        scriptHtml += '                  }});';
                                        scriptHtml += '                  $("#comboFields_CadastroUsuario_dGridTcsUsuarioRegraTransacao").igCombo("value", newCol);';
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
                                        scriptHtml += '              <i class="fa fa-eye-slash" aria-hidden="true" style="margin-right: 5px;"></i><a onclick="hideColumn()" style="cursor: pointer" id="showHideColumn_CadastroUsuario_dGridTcsUsuarioRegraTransacao">Ocultar Coluna</a>';
                                        scriptHtml += '          </div>';
                                        scriptHtml += '          <br>';
                                        scriptHtml += '      </div>';
                                        scriptHtml += '  </div>';
                        
                                        $(scriptHtml).insertBefore(divDialog);
                                   },
                             },
                                    { name: 'Selection', mode: 'row'
                                    }, 
                                    { name: 'Tooltips', columnSettings:[{ columnKey: "DescTransacao", allowTooltips: false },{ columnKey: "ClasseNome", allowTooltips: false },{ columnKey: "LxRegraAcessoTransacao", allowTooltips: true }] },
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
                                      columnSettings: [{ columnKey: "DescTransacao", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpTcsUsuarioRegraTransacao", isNullable: false, custom: vm.custom, vm: vm, verifyCanEditCol: verifyCanEditCol, allowMultiSelectionInSearch:true, activateAutoComplete: false, autoCompleteMaxResults: 7, validateOnClearState:false, maxValue:null, maxLength: 60, defaultValue: '' } }, { columnKey: "ClasseNome", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpTcsUsuarioRegraTransacao", isNullable: false, custom: vm.custom, vm: vm, verifyCanEditCol: verifyCanEditCol, allowMultiSelectionInSearch:true, activateAutoComplete: false, autoCompleteMaxResults: 7, validateOnClearState:false, maxValue:null, maxLength: 60, defaultValue: '' } }, { columnKey: 'LxRegraAcessoTransacao', editorType: 'combo', editorOptions: {   selectionChanged: function (evt, ui) {   var val = null;   if (ui.items != null && ui.items.length > 0) { val = ui.items[0].data['id']; }  updateEntity('LxRegraAcessoTransacao', val, false); },  mode: 'dropdown', dropDownOnFocus: true,  dataSource: vm.dataDomains.getItems('RegraAcesso', ''),  textKey: 'name', valueKey: 'id', enableClearButton: false }}],
                                      rowDeleting: function (evt, ui) {
                                          deletedIndex = ui.element.context.rowIndex;
                                          var entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowID);
                                          if (entity) {
                                              vm.deleteEntity(entity);
                                          }
                                      },
                                      rowDeleted: function (evt, ui) {
                                          var grid = $('#CadastroUsuario_dGridTcsUsuarioRegraTransacao');
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
                                          var columns = $('#CadastroUsuario_dGridTcsUsuarioRegraTransacao').igGridUpdating('option', 'columnSettings');
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
                                                     vm.dataCombo.fillDataCombos(lookUpName, ui.columnKey, vm.currentDataItem().currentTcsUsuarioRegraTransacao(), function (result) {
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
                            vm.OnDataGridCreated('CadastroUsuario_dGridTcsUsuarioRegraTransacao');
                        }
                        var selectionrowselectionchanged = null, selectedRowId = -1;
                        selectionrowselectionchanged = function (evt, ui) {
                            if ((ui.owner.grid.selectedRow() && typeof ui.owner.grid.selectedRow().id !== 'undefined') || (ui.owner.grid.selectedRows() && ui.owner.grid.selectedRows().length > 0)) { 
                                if (isNullOrEmpty(ui.owner.selectedRows())|| ui.selectedRows.length <= 1) {
                                    $(document).undelegate('#CadastroUsuario_dGridTcsUsuarioRegraTransacao', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                                    ui.owner.clearSelection();
                                    ui.owner.selectRow(ui.row.index);
                                    if (vm.status() === 'Q'){
                                        var gridCell = ui.owner.grid;
                                        grid.find('div.borderCell').remove();
                                        //$(gridCell.cellAt(-1, ui.owner._rowIndex)).append(" < div class='borderCell' style='z-index:100; border: 1px solid #849fd9 !important;'></div>");
                                    }
                                    selectedRowId = ui.row.id;
                                    $(document).delegate ('#CadastroUsuario_dGridTcsUsuarioRegraTransacao', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                                }
                                selectGridCurrentItem(vm.goToKey, 'RowDataId', ui, vm.currentDataItem().currentTcsUsuarioRegraTransacao, getDataSource()); 
                             } 
                        };
                        $(document).delegate('#CadastroUsuario_dGridTcsUsuarioRegraTransacao', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                        $('#CadastroUsuario_dGridTcsUsuarioRegraTransacao > tbody tr').live('focus', function(evt) {
                            var grid = $('#CadastroUsuario_dGridTcsUsuarioRegraTransacao'), row = $(this).closest('tr'), id = parseInt(row.attr('data-id'), 10);
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
                    vm.addDataSource({ key: 'CadastroUsuario_dGridTcsUsuarioRegraTransacao', name: 'TcsUsuarioRegraTransacaoList', itemsSource: itemsSource });
                }
            
            , ChangedBrandCadastroUsuario_dGridTcsUsuarioRegraColuna: function(vm, decimals, reset) {
                var i, format = '0.'.concat('0'.repeat(decimals)), grd =$('#CadastroUsuario_dGridTcsUsuarioRegraColuna').data('igGrid'),
                    grdUpd = $('#CadastroUsuario_dGridTcsUsuarioRegraColuna').data('igGridUpdating');
                if(isNull(grd) || isNull(grdUpd)) return;
                for (i = 0; i < grd.options.columns.length; i++) {
                }
                for (i = 0; i < grdUpd.options.columnSettings.length; i++) {
                }
                grd.dataBind();
            }
            , renderCadastroUsuario_dGridTcsUsuarioRegraColuna: function(vm) {
                var getDataSource = function() {
                    var source = null;
                    try {
                        source = vm.currentDataItem().TcsUsuarioRegraColunaList;
                    }
                    catch (e) { }
                    return isNullOrEmpty(source) ? ko.observableArray([]) : source;
                };
                var dataSourceIsLoaded = function() {
                    var isLoaded = false;
                    try {
                        isLoaded = (vm.currentDataItem().TcsUsuarioRegraColunaIsLoaded === true || vm.currentDataItem().TcsUsuarioRegraColunaList().length > 0);
                    }
                    catch (e) {
                        isLoaded = true;
                    }
                    return isLoaded;
                }
                $('#CadastroUsuario_dGridTcsUsuarioRegraColuna_headers').live('focus  keydown', function (evt) {
                    var keyCode = window.event ? evt.which : evt.keyCode;
                    if (keyCode === 9) {
                        var cols = $('#CadastroUsuario_dGridTcsUsuarioRegraColuna').igGrid('option', 'columns');
                        var dataView = $('#CadastroUsuario_dGridTcsUsuarioRegraColuna').data('igGrid').dataSource._dataView
                        if (dataView.length === 0) return;
                        var firstRow = dataView[0].RowDataId;
                        clear = vm.status() === 'C';
                        if (vm.status() === 'C')
                            $('#CadastroUsuario_dGridTcsUsuarioRegraColuna').igGridUpdating('startEdit', firstRow, 0, true);
                        else {
                            var entity = findElementByKey(getDataSource(), 'RowDataId', firstRow);
                            var indexColumn = 0;
                            cols.some(function (entry) {
                                if (entry.key !== 'RowDataId' && !entry.hidden) {
                                    if (verifyCanEditCol(entry.key, clear, entity)) {
                                        $('#CadastroUsuario_dGridTcsUsuarioRegraColuna').igGridUpdating('startEdit', firstRow, indexColumn, true);
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
                   if($('#CadastroUsuario_dGridTcsUsuarioRegraColuna').data('igGrid') === undefined) return '';
                   var cols = $('#CadastroUsuario_dGridTcsUsuarioRegraColuna').igGrid('option', 'columns');
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
                  if (!grid) grid = $('#CadastroUsuario_dGridTcsUsuarioRegraColuna');
                  return ((!grid[0] || (!forceCreating && grid.parent().width() <= 0)) && !$('#dialogTcsUsuarioRegraColuna').is(':visible'));
                }
                var refreshData = true;
                var itemsSource = { isElementHided: isElementHided, getVisibleColumns: getVisibleColumns, containerId: 'CadastroUsuario_dGridTcsUsuarioRegraColuna_container', dataBind: function (commitData, forceCreating) {
                   var grid = $('#CadastroUsuario_dGridTcsUsuarioRegraColuna');
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
                       $('#CadastroUsuario_dGridTcsUsuarioRegraColuna_groupbyarea').addClass('hide');
                   }
                   if (grid.igGridUpdating('isEditing')) {
                        grid.igGridUpdating('endEdit', true);
                   }
                   if (execFillDetais) {
                     grid.igGrid("option", "dataSource", []);
                     vm.currentDataItem().fillDetails(false, 'TcsUsuarioRegraColuna');
                     return;
                   }
                   grid.igGrid("option", "dataSource", unwrapObservableArray(getDataSource(), vm));
                   var rows = grid.igGrid('allRows');
                   if (rows.length > 0) {
                     var verticalContainer = grid.igGrid('scrollContainer');
                     var isSelected = false;
                     if (vm.currentDataItem().currentTcsUsuarioRegraColuna() != null)
                     {
                       for(var idx = 0; idx < rows.length; idx++)
                       {
                         if (rows[idx].dataset.id == getAbsoluteValue(vm.currentDataItem().currentTcsUsuarioRegraColuna().RowDataId))
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
                     if ($('#dialogTcsUsuarioRegraColuna').is(':visible')) {
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
                           $('label#currentNumberTcsUsuarioRegraColuna').html(current + ' - ' + totalCurrentPage);
                        }
                        else
                           $('label#currentNumberTcsUsuarioRegraColuna').html(1);
                        $('label#totalNumberTcsUsuarioRegraColuna').html(totalGrid);
                    }
                   } else {
                       $('label#currentNumberTcsUsuarioRegraColuna').html(0);
                       $('label#totalNumberTcsUsuarioRegraColuna').html(0);
                   }
                }};
                var valueGrouBy = -1;
                var deletedIndex = -1;
                function verifyCanEditCol(column, clear, entity){
                    switch(column){
                        case 'DescTransacao': { canEditing = clear || (entity && entity.isAdded()); break;}
                        case 'ClasseNome': { canEditing = clear || vm.enabledForEditing(); break;}
                        case 'TransacaoColuna': { canEditing = clear || vm.enabledForEditing(); break;}
                        case 'LxRegraAcessoColuna': { canEditing = clear || vm.enabledForEditing(); break;}
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
                            $('#CadastroUsuario_dGridTcsUsuarioRegraColuna_LayoutBtn').igPopover('hide');
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
                                      showMultimidia(entity, e, table, key, vm.CadastroUsuario());
                                 }
                             }
                             if (typeof vm.OnGridClientClick === 'function') {
                                 entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowKey);
                                 vm.OnGridClientClick('CadastroUsuario_dGridTcsUsuarioRegraColuna', ui.colKey, entity);
                             }
                             if (vm.status() != 'Q') {
                                 var grid = $('#CadastroUsuario_dGridTcsUsuarioRegraColuna');
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
                            { key: 'DescTransacao', headerText: vm.getLayoutHeaderGrid('CadastroUsuario_lUpTcsUsuarioRegraColuna_DescTransacao'), width: '271px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: true, unbound: false, group: null   },
                            { key: 'ClasseNome', headerText: vm.getLayoutHeaderGrid('CadastroUsuario_lUpTcsUsuarioRegraColuna_ClasseNome'), width: '271px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: true, unbound: false, group: null   },
                            { key: 'TransacaoColuna', headerText: vm.getLayoutHeaderGrid('CadastroUsuario_tbTcsUsuarioRegraColuna_TransacaoColuna'), width: '400px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: true, unbound: false, group: null   },
                            { key: 'LxRegraAcessoColuna', headerText: vm.getLayoutHeaderGrid('CadastroUsuario_cmbTcsUsuarioRegraColuna_LxRegraAcessoColuna'), width: '283px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: true, unbound: false, group: null  , formatter: function (val, record) { return  vm.dataDomains.getName('RegraAcessoColuna', val);} }
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
                        
                                        var dataView = $('#CadastroUsuario_dGridTcsUsuarioRegraColuna').data('igGrid').dataSource;
                                        if (dataView.settings.filtering.expressions.length <= 0)
                                            dataView._filteredData = [];
                        
                                        var col = ui.owner._dialogCurrentColumn;
                        
                                        var divDialog = $('#' + dgl[0].id).find('.ui-iggrid-filterdialogaddcondition').find('span')[0];
                        
                                        var scriptHtml = '<div id="' + divDinamica + '">';
                                        scriptHtml += '  <script>';
                                        scriptHtml += '    var newCol = "' + col + '";';
                                        scriptHtml += '    var newGrid = $("#CadastroUsuario_dGridTcsUsuarioRegraColuna");';
                                        scriptHtml += '    var listFilter = [];';
                                        scriptHtml += '    function hideColumn(){';
                                        scriptHtml += '     if ($("#showHideColumn_CadastroUsuario_dGridTcsUsuarioRegraColuna")["0"].innerHTML.indexOf("Ocultar") >= 0) {';
                                        scriptHtml += '        $("#showHideColumn_CadastroUsuario_dGridTcsUsuarioRegraColuna")["0"].innerHTML = "Mostrar Coluna";';
                                        scriptHtml += '        newGrid.igGridHiding("hideColumn", newCol);';
                                        scriptHtml += '     }';
                                        scriptHtml += '     else{';
                                        scriptHtml += '        $("#showHideColumn_CadastroUsuario_dGridTcsUsuarioRegraColuna")["0"].innerHTML = "Ocultar Coluna";';
                                        scriptHtml += '        newGrid.igGridHiding("showColumn", newCol);';
                                        scriptHtml += '     }';
                                        scriptHtml += '    }';
                                        scriptHtml += '    function updateHideButton(){';
                                        scriptHtml += '         var column = $.grep(newGrid.igGrid("option", "columns"), function (element, index) { return element.key == newCol });';
                                        scriptHtml += '         if (column.length > 0){';
                                        scriptHtml += '             $("#showHideColumn_CadastroUsuario_dGridTcsUsuarioRegraColuna")["0"].innerHTML = column[0].hidden ? "Mostrar Coluna" : "Ocultar Coluna"';
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
                                        scriptHtml += '              <div id="comboFields_CadastroUsuario_dGridTcsUsuarioRegraColuna"></div>';
                                        scriptHtml += '              <script>';
                                        scriptHtml += '                  var columns = newGrid.igGrid("option", "columns");';
                                        scriptHtml += '                  $("#comboFields_CadastroUsuario_dGridTcsUsuarioRegraColuna").igCombo({ dataSource: columns, mode : "dropdown", valueKey: "key", textKey: "headerText", selectionChanging: function (evt, ui) {';
                                        scriptHtml += '                      newCol = ui.items["0"].data.key;';
                                        scriptHtml += '                      updateHideButton()';
                                        scriptHtml += '                  }});';
                                        scriptHtml += '                  $("#comboFields_CadastroUsuario_dGridTcsUsuarioRegraColuna").igCombo("value", newCol);';
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
                                        scriptHtml += '              <i class="fa fa-eye-slash" aria-hidden="true" style="margin-right: 5px;"></i><a onclick="hideColumn()" style="cursor: pointer" id="showHideColumn_CadastroUsuario_dGridTcsUsuarioRegraColuna">Ocultar Coluna</a>';
                                        scriptHtml += '          </div>';
                                        scriptHtml += '          <br>';
                                        scriptHtml += '      </div>';
                                        scriptHtml += '  </div>';
                        
                                        $(scriptHtml).insertBefore(divDialog);
                                   },
                             },
                                    { name: 'Selection', mode: 'row'
                                    }, 
                                    { name: 'Tooltips', columnSettings:[{ columnKey: "DescTransacao", allowTooltips: false },{ columnKey: "ClasseNome", allowTooltips: false },{ columnKey: "TransacaoColuna", allowTooltips: true },{ columnKey: "LxRegraAcessoColuna", allowTooltips: true }] },
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
                                      columnSettings: [{ columnKey: "DescTransacao", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpTcsUsuarioRegraColuna", isNullable: false, custom: vm.custom, vm: vm, verifyCanEditCol: verifyCanEditCol, allowMultiSelectionInSearch:true, activateAutoComplete: false, autoCompleteMaxResults: 7, validateOnClearState:false, maxValue:null, maxLength: 0, defaultValue: '' } }, { columnKey: "ClasseNome", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpTcsUsuarioRegraColuna", isNullable: false, custom: vm.custom, vm: vm, verifyCanEditCol: verifyCanEditCol, allowMultiSelectionInSearch:true, activateAutoComplete: false, autoCompleteMaxResults: 7, validateOnClearState:false, maxValue:null, maxLength: 25, defaultValue: '' } }, { columnKey: 'LxRegraAcessoColuna', editorType: 'combo', editorOptions: {   selectionChanged: function (evt, ui) {   var val = null;   if (ui.items != null && ui.items.length > 0) { val = ui.items[0].data['id']; }  updateEntity('LxRegraAcessoColuna', val, false); },  mode: 'dropdown', dropDownOnFocus: true,  dataSource: vm.dataDomains.getItems('RegraAcessoColuna', ''),  textKey: 'name', valueKey: 'id', enableClearButton: false }}, { columnKey: "TransacaoColuna" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('CadastroUsuario_dGridTcsUsuarioRegraColuna', 'TransacaoColuna', ui.oldValue, ui.value);}}, maxLength: 250 } }],
                                      rowDeleting: function (evt, ui) {
                                          deletedIndex = ui.element.context.rowIndex;
                                          var entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowID);
                                          if (entity) {
                                              vm.deleteEntity(entity);
                                          }
                                      },
                                      rowDeleted: function (evt, ui) {
                                          var grid = $('#CadastroUsuario_dGridTcsUsuarioRegraColuna');
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
                                          var columns = $('#CadastroUsuario_dGridTcsUsuarioRegraColuna').igGridUpdating('option', 'columnSettings');
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
                                                     vm.dataCombo.fillDataCombos(lookUpName, ui.columnKey, vm.currentDataItem().currentTcsUsuarioRegraColuna(), function (result) {
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
                            vm.OnDataGridCreated('CadastroUsuario_dGridTcsUsuarioRegraColuna');
                        }
                        var selectionrowselectionchanged = null, selectedRowId = -1;
                        selectionrowselectionchanged = function (evt, ui) {
                            if ((ui.owner.grid.selectedRow() && typeof ui.owner.grid.selectedRow().id !== 'undefined') || (ui.owner.grid.selectedRows() && ui.owner.grid.selectedRows().length > 0)) { 
                                if (isNullOrEmpty(ui.owner.selectedRows())|| ui.selectedRows.length <= 1) {
                                    $(document).undelegate('#CadastroUsuario_dGridTcsUsuarioRegraColuna', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                                    ui.owner.clearSelection();
                                    ui.owner.selectRow(ui.row.index);
                                    if (vm.status() === 'Q'){
                                        var gridCell = ui.owner.grid;
                                        grid.find('div.borderCell').remove();
                                        //$(gridCell.cellAt(-1, ui.owner._rowIndex)).append(" < div class='borderCell' style='z-index:100; border: 1px solid #849fd9 !important;'></div>");
                                    }
                                    selectedRowId = ui.row.id;
                                    $(document).delegate ('#CadastroUsuario_dGridTcsUsuarioRegraColuna', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                                }
                                selectGridCurrentItem(vm.goToKey, 'RowDataId', ui, vm.currentDataItem().currentTcsUsuarioRegraColuna, getDataSource()); 
                             } 
                        };
                        $(document).delegate('#CadastroUsuario_dGridTcsUsuarioRegraColuna', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                        $('#CadastroUsuario_dGridTcsUsuarioRegraColuna > tbody tr').live('focus', function(evt) {
                            var grid = $('#CadastroUsuario_dGridTcsUsuarioRegraColuna'), row = $(this).closest('tr'), id = parseInt(row.attr('data-id'), 10);
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
                    vm.addDataSource({ key: 'CadastroUsuario_dGridTcsUsuarioRegraColuna', name: 'TcsUsuarioRegraColunaList', itemsSource: itemsSource });
                }
            
            , ChangedBrandCadastroUsuario_dGridTcsUsuarioBandeiraRede: function(vm, decimals, reset) {
                var i, format = '0.'.concat('0'.repeat(decimals)), grd =$('#CadastroUsuario_dGridTcsUsuarioBandeiraRede').data('igGrid'),
                    grdUpd = $('#CadastroUsuario_dGridTcsUsuarioBandeiraRede').data('igGridUpdating');
                if(isNull(grd) || isNull(grdUpd)) return;
                for (i = 0; i < grd.options.columns.length; i++) {
                }
                for (i = 0; i < grdUpd.options.columnSettings.length; i++) {
                }
                grd.dataBind();
            }
            , renderCadastroUsuario_dGridTcsUsuarioBandeiraRede: function(vm) {
                var getDataSource = function() {
                    var source = null;
                    try {
                        source = vm.currentDataItem().TcsUsuarioBandeiraRedeList;
                    }
                    catch (e) { }
                    return isNullOrEmpty(source) ? ko.observableArray([]) : source;
                };
                var dataSourceIsLoaded = function() {
                    var isLoaded = false;
                    try {
                        isLoaded = (vm.currentDataItem().TcsUsuarioBandeiraRedeIsLoaded === true || vm.currentDataItem().TcsUsuarioBandeiraRedeList().length > 0);
                    }
                    catch (e) {
                        isLoaded = true;
                    }
                    return isLoaded;
                }
                $('#CadastroUsuario_dGridTcsUsuarioBandeiraRede_headers').live('focus  keydown', function (evt) {
                    var keyCode = window.event ? evt.which : evt.keyCode;
                    if (keyCode === 9) {
                        var cols = $('#CadastroUsuario_dGridTcsUsuarioBandeiraRede').igGrid('option', 'columns');
                        var dataView = $('#CadastroUsuario_dGridTcsUsuarioBandeiraRede').data('igGrid').dataSource._dataView
                        if (dataView.length === 0) return;
                        var firstRow = dataView[0].RowDataId;
                        clear = vm.status() === 'C';
                        if (vm.status() === 'C')
                            $('#CadastroUsuario_dGridTcsUsuarioBandeiraRede').igGridUpdating('startEdit', firstRow, 0, true);
                        else {
                            var entity = findElementByKey(getDataSource(), 'RowDataId', firstRow);
                            var indexColumn = 0;
                            cols.some(function (entry) {
                                if (entry.key !== 'RowDataId' && !entry.hidden) {
                                    if (verifyCanEditCol(entry.key, clear, entity)) {
                                        $('#CadastroUsuario_dGridTcsUsuarioBandeiraRede').igGridUpdating('startEdit', firstRow, indexColumn, true);
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
                   if($('#CadastroUsuario_dGridTcsUsuarioBandeiraRede').data('igGrid') === undefined) return '';
                   var cols = $('#CadastroUsuario_dGridTcsUsuarioBandeiraRede').igGrid('option', 'columns');
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
                  if (!grid) grid = $('#CadastroUsuario_dGridTcsUsuarioBandeiraRede');
                  return ((!grid[0] || (!forceCreating && grid.parent().width() <= 0)) && !$('#dialogTcsUsuarioBandeiraRede').is(':visible'));
                }
                var refreshData = true;
                var itemsSource = { isElementHided: isElementHided, getVisibleColumns: getVisibleColumns, containerId: 'CadastroUsuario_dGridTcsUsuarioBandeiraRede_container', dataBind: function (commitData, forceCreating) {
                   var grid = $('#CadastroUsuario_dGridTcsUsuarioBandeiraRede');
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
                       $('#CadastroUsuario_dGridTcsUsuarioBandeiraRede_groupbyarea').addClass('hide');
                   }
                   if (grid.igGridUpdating('isEditing')) {
                        grid.igGridUpdating('endEdit', true);
                   }
                   if (execFillDetais) {
                     grid.igGrid("option", "dataSource", []);
                     vm.currentDataItem().fillDetails(false, 'TcsUsuarioBandeiraRede');
                     return;
                   }
                   grid.igGrid("option", "dataSource", unwrapObservableArray(getDataSource(), vm));
                   var rows = grid.igGrid('allRows');
                   if (rows.length > 0) {
                     var verticalContainer = grid.igGrid('scrollContainer');
                     var isSelected = false;
                     if (vm.currentDataItem().currentTcsUsuarioBandeiraRede() != null)
                     {
                       for(var idx = 0; idx < rows.length; idx++)
                       {
                         if (rows[idx].dataset.id == getAbsoluteValue(vm.currentDataItem().currentTcsUsuarioBandeiraRede().RowDataId))
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
                     if ($('#dialogTcsUsuarioBandeiraRede').is(':visible')) {
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
                           $('label#currentNumberTcsUsuarioBandeiraRede').html(current + ' - ' + totalCurrentPage);
                        }
                        else
                           $('label#currentNumberTcsUsuarioBandeiraRede').html(1);
                        $('label#totalNumberTcsUsuarioBandeiraRede').html(totalGrid);
                    }
                   } else {
                       $('label#currentNumberTcsUsuarioBandeiraRede').html(0);
                       $('label#totalNumberTcsUsuarioBandeiraRede').html(0);
                   }
                }};
                var valueGrouBy = -1;
                var deletedIndex = -1;
                function verifyCanEditCol(column, clear, entity){
                    switch(column){
                        case 'DescBandeiraRede': { canEditing = clear || (entity && entity.isAdded()); break;}
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
                            $('#CadastroUsuario_dGridTcsUsuarioBandeiraRede_LayoutBtn').igPopover('hide');
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
                                      showMultimidia(entity, e, table, key, vm.CadastroUsuario());
                                 }
                             }
                             if (typeof vm.OnGridClientClick === 'function') {
                                 entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowKey);
                                 vm.OnGridClientClick('CadastroUsuario_dGridTcsUsuarioBandeiraRede', ui.colKey, entity);
                             }
                             if (vm.status() != 'Q') {
                                 var grid = $('#CadastroUsuario_dGridTcsUsuarioBandeiraRede');
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
                            { key: 'DescBandeiraRede', headerText: vm.getLayoutHeaderGrid('CadastroUsuario_lUpTcsUsuarioBandeiraRede_DescBandeiraRede'), width: '421px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   }
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
                        
                                        var dataView = $('#CadastroUsuario_dGridTcsUsuarioBandeiraRede').data('igGrid').dataSource;
                                        if (dataView.settings.filtering.expressions.length <= 0)
                                            dataView._filteredData = [];
                        
                                        var col = ui.owner._dialogCurrentColumn;
                        
                                        var divDialog = $('#' + dgl[0].id).find('.ui-iggrid-filterdialogaddcondition').find('span')[0];
                        
                                        var scriptHtml = '<div id="' + divDinamica + '">';
                                        scriptHtml += '  <script>';
                                        scriptHtml += '    var newCol = "' + col + '";';
                                        scriptHtml += '    var newGrid = $("#CadastroUsuario_dGridTcsUsuarioBandeiraRede");';
                                        scriptHtml += '    var listFilter = [];';
                                        scriptHtml += '    function hideColumn(){';
                                        scriptHtml += '     if ($("#showHideColumn_CadastroUsuario_dGridTcsUsuarioBandeiraRede")["0"].innerHTML.indexOf("Ocultar") >= 0) {';
                                        scriptHtml += '        $("#showHideColumn_CadastroUsuario_dGridTcsUsuarioBandeiraRede")["0"].innerHTML = "Mostrar Coluna";';
                                        scriptHtml += '        newGrid.igGridHiding("hideColumn", newCol);';
                                        scriptHtml += '     }';
                                        scriptHtml += '     else{';
                                        scriptHtml += '        $("#showHideColumn_CadastroUsuario_dGridTcsUsuarioBandeiraRede")["0"].innerHTML = "Ocultar Coluna";';
                                        scriptHtml += '        newGrid.igGridHiding("showColumn", newCol);';
                                        scriptHtml += '     }';
                                        scriptHtml += '    }';
                                        scriptHtml += '    function updateHideButton(){';
                                        scriptHtml += '         var column = $.grep(newGrid.igGrid("option", "columns"), function (element, index) { return element.key == newCol });';
                                        scriptHtml += '         if (column.length > 0){';
                                        scriptHtml += '             $("#showHideColumn_CadastroUsuario_dGridTcsUsuarioBandeiraRede")["0"].innerHTML = column[0].hidden ? "Mostrar Coluna" : "Ocultar Coluna"';
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
                                        scriptHtml += '              <div id="comboFields_CadastroUsuario_dGridTcsUsuarioBandeiraRede"></div>';
                                        scriptHtml += '              <script>';
                                        scriptHtml += '                  var columns = newGrid.igGrid("option", "columns");';
                                        scriptHtml += '                  $("#comboFields_CadastroUsuario_dGridTcsUsuarioBandeiraRede").igCombo({ dataSource: columns, mode : "dropdown", valueKey: "key", textKey: "headerText", selectionChanging: function (evt, ui) {';
                                        scriptHtml += '                      newCol = ui.items["0"].data.key;';
                                        scriptHtml += '                      updateHideButton()';
                                        scriptHtml += '                  }});';
                                        scriptHtml += '                  $("#comboFields_CadastroUsuario_dGridTcsUsuarioBandeiraRede").igCombo("value", newCol);';
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
                                        scriptHtml += '              <i class="fa fa-eye-slash" aria-hidden="true" style="margin-right: 5px;"></i><a onclick="hideColumn()" style="cursor: pointer" id="showHideColumn_CadastroUsuario_dGridTcsUsuarioBandeiraRede">Ocultar Coluna</a>';
                                        scriptHtml += '          </div>';
                                        scriptHtml += '          <br>';
                                        scriptHtml += '      </div>';
                                        scriptHtml += '  </div>';
                        
                                        $(scriptHtml).insertBefore(divDialog);
                                   },
                             },
                                    { name: 'Selection', mode: 'row'
                                    }, 
                                    { name: 'Tooltips', columnSettings:[{ columnKey: "DescBandeiraRede", allowTooltips: false }] },
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
                                      columnSettings: [{ columnKey: "DescBandeiraRede", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpTbcBandeiraRede", isNullable: false, custom: vm.custom, vm: vm, verifyCanEditCol: verifyCanEditCol, allowMultiSelectionInSearch:true, activateAutoComplete: false, autoCompleteMaxResults: 7, validateOnClearState:false, maxValue:null, maxLength: 60, defaultValue: '' } }],
                                      rowDeleting: function (evt, ui) {
                                          deletedIndex = ui.element.context.rowIndex;
                                          var entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowID);
                                          if (entity) {
                                              vm.deleteEntity(entity);
                                          }
                                      },
                                      rowDeleted: function (evt, ui) {
                                          var grid = $('#CadastroUsuario_dGridTcsUsuarioBandeiraRede');
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
                                          var columns = $('#CadastroUsuario_dGridTcsUsuarioBandeiraRede').igGridUpdating('option', 'columnSettings');
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
                                                     vm.dataCombo.fillDataCombos(lookUpName, ui.columnKey, vm.currentDataItem().currentTcsUsuarioBandeiraRede(), function (result) {
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
                            vm.OnDataGridCreated('CadastroUsuario_dGridTcsUsuarioBandeiraRede');
                        }
                        var selectionrowselectionchanged = null, selectedRowId = -1;
                        selectionrowselectionchanged = function (evt, ui) {
                            if ((ui.owner.grid.selectedRow() && typeof ui.owner.grid.selectedRow().id !== 'undefined') || (ui.owner.grid.selectedRows() && ui.owner.grid.selectedRows().length > 0)) { 
                                if (isNullOrEmpty(ui.owner.selectedRows())|| ui.selectedRows.length <= 1) {
                                    $(document).undelegate('#CadastroUsuario_dGridTcsUsuarioBandeiraRede', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                                    ui.owner.clearSelection();
                                    ui.owner.selectRow(ui.row.index);
                                    if (vm.status() === 'Q'){
                                        var gridCell = ui.owner.grid;
                                        grid.find('div.borderCell').remove();
                                        //$(gridCell.cellAt(-1, ui.owner._rowIndex)).append(" < div class='borderCell' style='z-index:100; border: 1px solid #849fd9 !important;'></div>");
                                    }
                                    selectedRowId = ui.row.id;
                                    $(document).delegate ('#CadastroUsuario_dGridTcsUsuarioBandeiraRede', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                                }
                                selectGridCurrentItem(vm.goToKey, 'RowDataId', ui, vm.currentDataItem().currentTcsUsuarioBandeiraRede, getDataSource()); 
                             } 
                        };
                        $(document).delegate('#CadastroUsuario_dGridTcsUsuarioBandeiraRede', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                        $('#CadastroUsuario_dGridTcsUsuarioBandeiraRede > tbody tr').live('focus', function(evt) {
                            var grid = $('#CadastroUsuario_dGridTcsUsuarioBandeiraRede'), row = $(this).closest('tr'), id = parseInt(row.attr('data-id'), 10);
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
                    vm.addDataSource({ key: 'CadastroUsuario_dGridTcsUsuarioBandeiraRede', name: 'TcsUsuarioBandeiraRedeList', itemsSource: itemsSource });
                }
            
            , ChangedBrandCadastroUsuario_dGridTcsUsuarioFilial: function(vm, decimals, reset) {
                var i, format = '0.'.concat('0'.repeat(decimals)), grd =$('#CadastroUsuario_dGridTcsUsuarioFilial').data('igGrid'),
                    grdUpd = $('#CadastroUsuario_dGridTcsUsuarioFilial').data('igGridUpdating');
                if(isNull(grd) || isNull(grdUpd)) return;
                for (i = 0; i < grd.options.columns.length; i++) {
                }
                for (i = 0; i < grdUpd.options.columnSettings.length; i++) {
                }
                grd.dataBind();
            }
            , renderCadastroUsuario_dGridTcsUsuarioFilial: function(vm) {
                var getDataSource = function() {
                    var source = null;
                    try {
                        source = vm.currentDataItem().TcsUsuarioFilialList;
                    }
                    catch (e) { }
                    return isNullOrEmpty(source) ? ko.observableArray([]) : source;
                };
                var dataSourceIsLoaded = function() {
                    var isLoaded = false;
                    try {
                        isLoaded = (vm.currentDataItem().TcsUsuarioFilialIsLoaded === true || vm.currentDataItem().TcsUsuarioFilialList().length > 0);
                    }
                    catch (e) {
                        isLoaded = true;
                    }
                    return isLoaded;
                }
                $('#CadastroUsuario_dGridTcsUsuarioFilial_headers').live('focus  keydown', function (evt) {
                    var keyCode = window.event ? evt.which : evt.keyCode;
                    if (keyCode === 9) {
                        var cols = $('#CadastroUsuario_dGridTcsUsuarioFilial').igGrid('option', 'columns');
                        var dataView = $('#CadastroUsuario_dGridTcsUsuarioFilial').data('igGrid').dataSource._dataView
                        if (dataView.length === 0) return;
                        var firstRow = dataView[0].RowDataId;
                        clear = vm.status() === 'C';
                        if (vm.status() === 'C')
                            $('#CadastroUsuario_dGridTcsUsuarioFilial').igGridUpdating('startEdit', firstRow, 0, true);
                        else {
                            var entity = findElementByKey(getDataSource(), 'RowDataId', firstRow);
                            var indexColumn = 0;
                            cols.some(function (entry) {
                                if (entry.key !== 'RowDataId' && !entry.hidden) {
                                    if (verifyCanEditCol(entry.key, clear, entity)) {
                                        $('#CadastroUsuario_dGridTcsUsuarioFilial').igGridUpdating('startEdit', firstRow, indexColumn, true);
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
                   if($('#CadastroUsuario_dGridTcsUsuarioFilial').data('igGrid') === undefined) return '';
                   var cols = $('#CadastroUsuario_dGridTcsUsuarioFilial').igGrid('option', 'columns');
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
                  if (!grid) grid = $('#CadastroUsuario_dGridTcsUsuarioFilial');
                  return ((!grid[0] || (!forceCreating && grid.parent().width() <= 0)) && !$('#dialogTcsUsuarioFilial').is(':visible'));
                }
                var refreshData = true;
                var itemsSource = { isElementHided: isElementHided, getVisibleColumns: getVisibleColumns, containerId: 'CadastroUsuario_dGridTcsUsuarioFilial_container', dataBind: function (commitData, forceCreating) {
                   var grid = $('#CadastroUsuario_dGridTcsUsuarioFilial');
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
                       $('#CadastroUsuario_dGridTcsUsuarioFilial_groupbyarea').addClass('hide');
                   }
                   if (grid.igGridUpdating('isEditing')) {
                        grid.igGridUpdating('endEdit', true);
                   }
                   if (execFillDetais) {
                     grid.igGrid("option", "dataSource", []);
                     vm.currentDataItem().fillDetails(false, 'TcsUsuarioFilial');
                     return;
                   }
                   grid.igGrid("option", "dataSource", unwrapObservableArray(getDataSource(), vm));
                   var rows = grid.igGrid('allRows');
                   if (rows.length > 0) {
                     var verticalContainer = grid.igGrid('scrollContainer');
                     var isSelected = false;
                     if (vm.currentDataItem().currentTcsUsuarioFilial() != null)
                     {
                       for(var idx = 0; idx < rows.length; idx++)
                       {
                         if (rows[idx].dataset.id == getAbsoluteValue(vm.currentDataItem().currentTcsUsuarioFilial().RowDataId))
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
                     if ($('#dialogTcsUsuarioFilial').is(':visible')) {
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
                           $('label#currentNumberTcsUsuarioFilial').html(current + ' - ' + totalCurrentPage);
                        }
                        else
                           $('label#currentNumberTcsUsuarioFilial').html(1);
                        $('label#totalNumberTcsUsuarioFilial').html(totalGrid);
                    }
                   } else {
                       $('label#currentNumberTcsUsuarioFilial').html(0);
                       $('label#totalNumberTcsUsuarioFilial').html(0);
                   }
                }};
                var valueGrouBy = -1;
                var deletedIndex = -1;
                function verifyCanEditCol(column, clear, entity){
                    switch(column){
                        case 'CodigoFilial': { canEditing = clear || (entity && entity.isAdded()); break;}
                        case 'NomeFilial': { canEditing = clear || (entity && entity.isAdded()); break;}
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
                            $('#CadastroUsuario_dGridTcsUsuarioFilial_LayoutBtn').igPopover('hide');
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
                                      showMultimidia(entity, e, table, key, vm.CadastroUsuario());
                                 }
                             }
                             if (typeof vm.OnGridClientClick === 'function') {
                                 entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowKey);
                                 vm.OnGridClientClick('CadastroUsuario_dGridTcsUsuarioFilial', ui.colKey, entity);
                             }
                             if (vm.status() != 'Q') {
                                 var grid = $('#CadastroUsuario_dGridTcsUsuarioFilial');
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
                            { key: 'CodigoFilial', headerText: vm.getLayoutHeaderGrid('CadastroUsuario_lUpTcsUsuarioFilial_CodigoFilial'), width: '205px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'NomeFilial', headerText: vm.getLayoutHeaderGrid('CadastroUsuario_lUpTcsUsuarioFilial_NomeFilial'), width: '421px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   }
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
                        
                                        var dataView = $('#CadastroUsuario_dGridTcsUsuarioFilial').data('igGrid').dataSource;
                                        if (dataView.settings.filtering.expressions.length <= 0)
                                            dataView._filteredData = [];
                        
                                        var col = ui.owner._dialogCurrentColumn;
                        
                                        var divDialog = $('#' + dgl[0].id).find('.ui-iggrid-filterdialogaddcondition').find('span')[0];
                        
                                        var scriptHtml = '<div id="' + divDinamica + '">';
                                        scriptHtml += '  <script>';
                                        scriptHtml += '    var newCol = "' + col + '";';
                                        scriptHtml += '    var newGrid = $("#CadastroUsuario_dGridTcsUsuarioFilial");';
                                        scriptHtml += '    var listFilter = [];';
                                        scriptHtml += '    function hideColumn(){';
                                        scriptHtml += '     if ($("#showHideColumn_CadastroUsuario_dGridTcsUsuarioFilial")["0"].innerHTML.indexOf("Ocultar") >= 0) {';
                                        scriptHtml += '        $("#showHideColumn_CadastroUsuario_dGridTcsUsuarioFilial")["0"].innerHTML = "Mostrar Coluna";';
                                        scriptHtml += '        newGrid.igGridHiding("hideColumn", newCol);';
                                        scriptHtml += '     }';
                                        scriptHtml += '     else{';
                                        scriptHtml += '        $("#showHideColumn_CadastroUsuario_dGridTcsUsuarioFilial")["0"].innerHTML = "Ocultar Coluna";';
                                        scriptHtml += '        newGrid.igGridHiding("showColumn", newCol);';
                                        scriptHtml += '     }';
                                        scriptHtml += '    }';
                                        scriptHtml += '    function updateHideButton(){';
                                        scriptHtml += '         var column = $.grep(newGrid.igGrid("option", "columns"), function (element, index) { return element.key == newCol });';
                                        scriptHtml += '         if (column.length > 0){';
                                        scriptHtml += '             $("#showHideColumn_CadastroUsuario_dGridTcsUsuarioFilial")["0"].innerHTML = column[0].hidden ? "Mostrar Coluna" : "Ocultar Coluna"';
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
                                        scriptHtml += '              <div id="comboFields_CadastroUsuario_dGridTcsUsuarioFilial"></div>';
                                        scriptHtml += '              <script>';
                                        scriptHtml += '                  var columns = newGrid.igGrid("option", "columns");';
                                        scriptHtml += '                  $("#comboFields_CadastroUsuario_dGridTcsUsuarioFilial").igCombo({ dataSource: columns, mode : "dropdown", valueKey: "key", textKey: "headerText", selectionChanging: function (evt, ui) {';
                                        scriptHtml += '                      newCol = ui.items["0"].data.key;';
                                        scriptHtml += '                      updateHideButton()';
                                        scriptHtml += '                  }});';
                                        scriptHtml += '                  $("#comboFields_CadastroUsuario_dGridTcsUsuarioFilial").igCombo("value", newCol);';
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
                                        scriptHtml += '              <i class="fa fa-eye-slash" aria-hidden="true" style="margin-right: 5px;"></i><a onclick="hideColumn()" style="cursor: pointer" id="showHideColumn_CadastroUsuario_dGridTcsUsuarioFilial">Ocultar Coluna</a>';
                                        scriptHtml += '          </div>';
                                        scriptHtml += '          <br>';
                                        scriptHtml += '      </div>';
                                        scriptHtml += '  </div>';
                        
                                        $(scriptHtml).insertBefore(divDialog);
                                   },
                             },
                                    { name: 'Selection', mode: 'row'
                                    }, 
                                    { name: 'Tooltips', columnSettings:[{ columnKey: "CodigoFilial", allowTooltips: false },{ columnKey: "NomeFilial", allowTooltips: false }] },
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
                                      columnSettings: [{ columnKey: "CodigoFilial", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpTbcFilial", isNullable: true, custom: vm.custom, vm: vm, verifyCanEditCol: verifyCanEditCol, allowMultiSelectionInSearch:true, activateAutoComplete: false, autoCompleteMaxResults: 7, validateOnClearState:false, maxValue:null, maxLength: 18, defaultValue: '' } }, { columnKey: "NomeFilial", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpTbcFilial", isNullable: true, custom: vm.custom, vm: vm, verifyCanEditCol: verifyCanEditCol, allowMultiSelectionInSearch:true, activateAutoComplete: false, autoCompleteMaxResults: 7, validateOnClearState:false, maxValue:null, maxLength: 60, defaultValue: '' } }],
                                      rowDeleting: function (evt, ui) {
                                          deletedIndex = ui.element.context.rowIndex;
                                          var entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowID);
                                          if (entity) {
                                              vm.deleteEntity(entity);
                                          }
                                      },
                                      rowDeleted: function (evt, ui) {
                                          var grid = $('#CadastroUsuario_dGridTcsUsuarioFilial');
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
                                          var columns = $('#CadastroUsuario_dGridTcsUsuarioFilial').igGridUpdating('option', 'columnSettings');
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
                                                     vm.dataCombo.fillDataCombos(lookUpName, ui.columnKey, vm.currentDataItem().currentTcsUsuarioFilial(), function (result) {
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
                            vm.OnDataGridCreated('CadastroUsuario_dGridTcsUsuarioFilial');
                        }
                        var selectionrowselectionchanged = null, selectedRowId = -1;
                        selectionrowselectionchanged = function (evt, ui) {
                            if ((ui.owner.grid.selectedRow() && typeof ui.owner.grid.selectedRow().id !== 'undefined') || (ui.owner.grid.selectedRows() && ui.owner.grid.selectedRows().length > 0)) { 
                                if (isNullOrEmpty(ui.owner.selectedRows())|| ui.selectedRows.length <= 1) {
                                    $(document).undelegate('#CadastroUsuario_dGridTcsUsuarioFilial', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                                    ui.owner.clearSelection();
                                    ui.owner.selectRow(ui.row.index);
                                    if (vm.status() === 'Q'){
                                        var gridCell = ui.owner.grid;
                                        grid.find('div.borderCell').remove();
                                        //$(gridCell.cellAt(-1, ui.owner._rowIndex)).append(" < div class='borderCell' style='z-index:100; border: 1px solid #849fd9 !important;'></div>");
                                    }
                                    selectedRowId = ui.row.id;
                                    $(document).delegate ('#CadastroUsuario_dGridTcsUsuarioFilial', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                                }
                                selectGridCurrentItem(vm.goToKey, 'RowDataId', ui, vm.currentDataItem().currentTcsUsuarioFilial, getDataSource()); 
                             } 
                        };
                        $(document).delegate('#CadastroUsuario_dGridTcsUsuarioFilial', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                        $('#CadastroUsuario_dGridTcsUsuarioFilial > tbody tr').live('focus', function(evt) {
                            var grid = $('#CadastroUsuario_dGridTcsUsuarioFilial'), row = $(this).closest('tr'), id = parseInt(row.attr('data-id'), 10);
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
                    vm.addDataSource({ key: 'CadastroUsuario_dGridTcsUsuarioFilial', name: 'TcsUsuarioFilialList', itemsSource: itemsSource });
                }
            
            , ChangedBrandCadastroUsuario_dGridTcsUsuarioLayout: function(vm, decimals, reset) {
                var i, format = '0.'.concat('0'.repeat(decimals)), grd =$('#CadastroUsuario_dGridTcsUsuarioLayout').data('igGrid'),
                    grdUpd = $('#CadastroUsuario_dGridTcsUsuarioLayout').data('igGridUpdating');
                if(isNull(grd) || isNull(grdUpd)) return;
                for (i = 0; i < grd.options.columns.length; i++) {
                }
                for (i = 0; i < grdUpd.options.columnSettings.length; i++) {
                }
                grd.dataBind();
            }
            , renderCadastroUsuario_dGridTcsUsuarioLayout: function(vm) {
                var getDataSource = function() {
                    var source = null;
                    try {
                        source = vm.currentDataItem().TcsUsuarioLayoutList;
                    }
                    catch (e) { }
                    return isNullOrEmpty(source) ? ko.observableArray([]) : source;
                };
                var dataSourceIsLoaded = function() {
                    var isLoaded = false;
                    try {
                        isLoaded = (vm.currentDataItem().TcsUsuarioLayoutIsLoaded === true || vm.currentDataItem().TcsUsuarioLayoutList().length > 0);
                    }
                    catch (e) {
                        isLoaded = true;
                    }
                    return isLoaded;
                }
                $('#CadastroUsuario_dGridTcsUsuarioLayout_headers').live('focus  keydown', function (evt) {
                    var keyCode = window.event ? evt.which : evt.keyCode;
                    if (keyCode === 9) {
                        var cols = $('#CadastroUsuario_dGridTcsUsuarioLayout').igGrid('option', 'columns');
                        var dataView = $('#CadastroUsuario_dGridTcsUsuarioLayout').data('igGrid').dataSource._dataView
                        if (dataView.length === 0) return;
                        var firstRow = dataView[0].RowDataId;
                        clear = vm.status() === 'C';
                        if (vm.status() === 'C')
                            $('#CadastroUsuario_dGridTcsUsuarioLayout').igGridUpdating('startEdit', firstRow, 0, true);
                        else {
                            var entity = findElementByKey(getDataSource(), 'RowDataId', firstRow);
                            var indexColumn = 0;
                            cols.some(function (entry) {
                                if (entry.key !== 'RowDataId' && !entry.hidden) {
                                    if (verifyCanEditCol(entry.key, clear, entity)) {
                                        $('#CadastroUsuario_dGridTcsUsuarioLayout').igGridUpdating('startEdit', firstRow, indexColumn, true);
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
                   if($('#CadastroUsuario_dGridTcsUsuarioLayout').data('igGrid') === undefined) return '';
                   var cols = $('#CadastroUsuario_dGridTcsUsuarioLayout').igGrid('option', 'columns');
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
                  if (!grid) grid = $('#CadastroUsuario_dGridTcsUsuarioLayout');
                  return ((!grid[0] || (!forceCreating && grid.parent().width() <= 0)) && !$('#dialogTcsUsuarioLayout').is(':visible'));
                }
                var refreshData = true;
                var itemsSource = { isElementHided: isElementHided, getVisibleColumns: getVisibleColumns, containerId: 'CadastroUsuario_dGridTcsUsuarioLayout_container', dataBind: function (commitData, forceCreating) {
                   var grid = $('#CadastroUsuario_dGridTcsUsuarioLayout');
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
                       $('#CadastroUsuario_dGridTcsUsuarioLayout_groupbyarea').addClass('hide');
                   }
                   if (grid.igGridUpdating('isEditing')) {
                        grid.igGridUpdating('endEdit', true);
                   }
                   if (execFillDetais) {
                     grid.igGrid("option", "dataSource", []);
                     vm.currentDataItem().fillDetails(false, 'TcsUsuarioLayout');
                     return;
                   }
                   grid.igGrid("option", "dataSource", unwrapObservableArray(getDataSource(), vm));
                   var rows = grid.igGrid('allRows');
                   if (rows.length > 0) {
                     var verticalContainer = grid.igGrid('scrollContainer');
                     var isSelected = false;
                     if (vm.currentDataItem().currentTcsUsuarioLayout() != null)
                     {
                       for(var idx = 0; idx < rows.length; idx++)
                       {
                         if (rows[idx].dataset.id == getAbsoluteValue(vm.currentDataItem().currentTcsUsuarioLayout().RowDataId))
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
                     if ($('#dialogTcsUsuarioLayout').is(':visible')) {
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
                           $('label#currentNumberTcsUsuarioLayout').html(current + ' - ' + totalCurrentPage);
                        }
                        else
                           $('label#currentNumberTcsUsuarioLayout').html(1);
                        $('label#totalNumberTcsUsuarioLayout').html(totalGrid);
                    }
                   } else {
                       $('label#currentNumberTcsUsuarioLayout').html(0);
                       $('label#totalNumberTcsUsuarioLayout').html(0);
                   }
                }};
                var valueGrouBy = -1;
                var deletedIndex = -1;
                function verifyCanEditCol(column, clear, entity){
                    switch(column){
                        case 'DescLayout': { canEditing = clear || (entity && entity.isAdded()); break;}
                        case 'Detalhes': { canEditing = clear; break;}
                        case 'Inativo': { canEditing = clear; break;}
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
                            $('#CadastroUsuario_dGridTcsUsuarioLayout_LayoutBtn').igPopover('hide');
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
                                      showMultimidia(entity, e, table, key, vm.CadastroUsuario());
                                 }
                             }
                             if (typeof vm.OnGridClientClick === 'function') {
                                 entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowKey);
                                 vm.OnGridClientClick('CadastroUsuario_dGridTcsUsuarioLayout', ui.colKey, entity);
                             }
                             if (vm.status() != 'Q') {
                                 var grid = $('#CadastroUsuario_dGridTcsUsuarioLayout');
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
                            { key: 'DescLayout', headerText: vm.getLayoutHeaderGrid('CadastroUsuario_lUpTcsUsuarioLayout_DescLayout'), width: '421px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'Detalhes', headerText: vm.getLayoutHeaderGrid('CadastroUsuario_edTcsUsuarioLayout_Detalhes'), width: '400px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'Inativo', headerText: vm.getLayoutHeaderGrid('CadastroUsuario_ckTcsUsuarioLayout_Inativo'), width: '127px', dataType: 'bool', columnCssClass: 'ellipsis', format: 'checkbox', hidden: false, unbound: false, group: null   }
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
                        
                                        var dataView = $('#CadastroUsuario_dGridTcsUsuarioLayout').data('igGrid').dataSource;
                                        if (dataView.settings.filtering.expressions.length <= 0)
                                            dataView._filteredData = [];
                        
                                        var col = ui.owner._dialogCurrentColumn;
                        
                                        var divDialog = $('#' + dgl[0].id).find('.ui-iggrid-filterdialogaddcondition').find('span')[0];
                        
                                        var scriptHtml = '<div id="' + divDinamica + '">';
                                        scriptHtml += '  <script>';
                                        scriptHtml += '    var newCol = "' + col + '";';
                                        scriptHtml += '    var newGrid = $("#CadastroUsuario_dGridTcsUsuarioLayout");';
                                        scriptHtml += '    var listFilter = [];';
                                        scriptHtml += '    function hideColumn(){';
                                        scriptHtml += '     if ($("#showHideColumn_CadastroUsuario_dGridTcsUsuarioLayout")["0"].innerHTML.indexOf("Ocultar") >= 0) {';
                                        scriptHtml += '        $("#showHideColumn_CadastroUsuario_dGridTcsUsuarioLayout")["0"].innerHTML = "Mostrar Coluna";';
                                        scriptHtml += '        newGrid.igGridHiding("hideColumn", newCol);';
                                        scriptHtml += '     }';
                                        scriptHtml += '     else{';
                                        scriptHtml += '        $("#showHideColumn_CadastroUsuario_dGridTcsUsuarioLayout")["0"].innerHTML = "Ocultar Coluna";';
                                        scriptHtml += '        newGrid.igGridHiding("showColumn", newCol);';
                                        scriptHtml += '     }';
                                        scriptHtml += '    }';
                                        scriptHtml += '    function updateHideButton(){';
                                        scriptHtml += '         var column = $.grep(newGrid.igGrid("option", "columns"), function (element, index) { return element.key == newCol });';
                                        scriptHtml += '         if (column.length > 0){';
                                        scriptHtml += '             $("#showHideColumn_CadastroUsuario_dGridTcsUsuarioLayout")["0"].innerHTML = column[0].hidden ? "Mostrar Coluna" : "Ocultar Coluna"';
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
                                        scriptHtml += '              <div id="comboFields_CadastroUsuario_dGridTcsUsuarioLayout"></div>';
                                        scriptHtml += '              <script>';
                                        scriptHtml += '                  var columns = newGrid.igGrid("option", "columns");';
                                        scriptHtml += '                  $("#comboFields_CadastroUsuario_dGridTcsUsuarioLayout").igCombo({ dataSource: columns, mode : "dropdown", valueKey: "key", textKey: "headerText", selectionChanging: function (evt, ui) {';
                                        scriptHtml += '                      newCol = ui.items["0"].data.key;';
                                        scriptHtml += '                      updateHideButton()';
                                        scriptHtml += '                  }});';
                                        scriptHtml += '                  $("#comboFields_CadastroUsuario_dGridTcsUsuarioLayout").igCombo("value", newCol);';
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
                                        scriptHtml += '              <i class="fa fa-eye-slash" aria-hidden="true" style="margin-right: 5px;"></i><a onclick="hideColumn()" style="cursor: pointer" id="showHideColumn_CadastroUsuario_dGridTcsUsuarioLayout">Ocultar Coluna</a>';
                                        scriptHtml += '          </div>';
                                        scriptHtml += '          <br>';
                                        scriptHtml += '      </div>';
                                        scriptHtml += '  </div>';
                        
                                        $(scriptHtml).insertBefore(divDialog);
                                   },
                             },
                                    { name: 'Selection', mode: 'row'
                                    }, 
                                    { name: 'Tooltips', columnSettings:[{ columnKey: "DescLayout", allowTooltips: false },{ columnKey: "Detalhes", allowTooltips: true },{ columnKey: "Inativo", allowTooltips: true }] },
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
                                      columnSettings: [{ columnKey: "DescLayout", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpTcsLayout", isNullable: false, custom: vm.custom, vm: vm, verifyCanEditCol: verifyCanEditCol, allowMultiSelectionInSearch:true, activateAutoComplete: false, autoCompleteMaxResults: 7, validateOnClearState:false, maxValue:null, maxLength: 60, defaultValue: '' } }, { columnKey: "Detalhes" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('CadastroUsuario_dGridTcsUsuarioLayout', 'Detalhes', ui.oldValue, ui.value);}}, maxLength: 500 } }],
                                      rowDeleting: function (evt, ui) {
                                          deletedIndex = ui.element.context.rowIndex;
                                          var entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowID);
                                          if (entity) {
                                              vm.deleteEntity(entity);
                                          }
                                      },
                                      rowDeleted: function (evt, ui) {
                                          var grid = $('#CadastroUsuario_dGridTcsUsuarioLayout');
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
                                          var columns = $('#CadastroUsuario_dGridTcsUsuarioLayout').igGridUpdating('option', 'columnSettings');
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
                                                     vm.dataCombo.fillDataCombos(lookUpName, ui.columnKey, vm.currentDataItem().currentTcsUsuarioLayout(), function (result) {
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
                            vm.OnDataGridCreated('CadastroUsuario_dGridTcsUsuarioLayout');
                        }
                        var selectionrowselectionchanged = null, selectedRowId = -1;
                        selectionrowselectionchanged = function (evt, ui) {
                            if ((ui.owner.grid.selectedRow() && typeof ui.owner.grid.selectedRow().id !== 'undefined') || (ui.owner.grid.selectedRows() && ui.owner.grid.selectedRows().length > 0)) { 
                                if (isNullOrEmpty(ui.owner.selectedRows())|| ui.selectedRows.length <= 1) {
                                    $(document).undelegate('#CadastroUsuario_dGridTcsUsuarioLayout', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                                    ui.owner.clearSelection();
                                    ui.owner.selectRow(ui.row.index);
                                    if (vm.status() === 'Q'){
                                        var gridCell = ui.owner.grid;
                                        grid.find('div.borderCell').remove();
                                        //$(gridCell.cellAt(-1, ui.owner._rowIndex)).append(" < div class='borderCell' style='z-index:100; border: 1px solid #849fd9 !important;'></div>");
                                    }
                                    selectedRowId = ui.row.id;
                                    $(document).delegate ('#CadastroUsuario_dGridTcsUsuarioLayout', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                                }
                                selectGridCurrentItem(vm.goToKey, 'RowDataId', ui, vm.currentDataItem().currentTcsUsuarioLayout, getDataSource()); 
                             } 
                        };
                        $(document).delegate('#CadastroUsuario_dGridTcsUsuarioLayout', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                        $('#CadastroUsuario_dGridTcsUsuarioLayout > tbody tr').live('focus', function(evt) {
                            var grid = $('#CadastroUsuario_dGridTcsUsuarioLayout'), row = $(this).closest('tr'), id = parseInt(row.attr('data-id'), 10);
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
                    vm.addDataSource({ key: 'CadastroUsuario_dGridTcsUsuarioLayout', name: 'TcsUsuarioLayoutList', itemsSource: itemsSource });
                }
            
            
            , ChangedBrandscyCadastroUsuario_dGrid: function(vm, decimals, reset) {
                var i, format = '0.'.concat('0'.repeat(decimals)), grd =$('#scyCadastroUsuario_dGrid').data('igGrid'),
                    grdUpd = $('#scyCadastroUsuario_dGrid').data('igGridUpdating');
                if(isNull(grd) || isNull(grdUpd)) return;
                for (i = 0; i < grd.options.columns.length; i++) {
                }
                for (i = 0; i < grdUpd.options.columnSettings.length; i++) {
                }
                grd.dataBind();
            }
            , renderscyCadastroUsuario_dGrid: function(vm) {
                var getDataSource = function() {
                    var source = null;
                    try {
                        source = vm.dataView;
                    }
                    catch (e) { }
                    return isNullOrEmpty(source) ? ko.observableArray([]) : source;
                };
                $('#scyCadastroUsuario_dGrid_headers').live('focus  keydown', function (evt) {
                    var keyCode = window.event ? evt.which : evt.keyCode;
                    if (keyCode === 9) {
                        var cols = $('#scyCadastroUsuario_dGrid').igGrid('option', 'columns');
                        var dataView = $('#scyCadastroUsuario_dGrid').data('igGrid').dataSource._dataView
                        if (dataView.length === 0) return;
                        var firstRow = dataView[0].RowDataId;
                        clear = vm.status() === 'C';
                        if (vm.status() === 'C')
                            $('#scyCadastroUsuario_dGrid').igGridUpdating('startEdit', firstRow, 0, true);
                        else {
                            var entity = findElementByKey(getDataSource(), 'RowDataId', firstRow);
                            var indexColumn = 0;
                            cols.some(function (entry) {
                                if (entry.key !== 'RowDataId' && !entry.hidden) {
                                    if (verifyCanEditCol(entry.key, clear, entity)) {
                                        $('#scyCadastroUsuario_dGrid').igGridUpdating('startEdit', firstRow, indexColumn, true);
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
                   if($('#scyCadastroUsuario_dGrid').data('igGrid') === undefined) return '';
                   var cols = $('#scyCadastroUsuario_dGrid').igGrid('option', 'columns');
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
                  if (!grid) grid = $('#scyCadastroUsuario_dGrid');
                  return ((!grid[0] || (!forceCreating && grid.parent().width() <= 0)) && !$('#dialog').is(':visible'));
                }
                var refreshData = true;
                var itemsSource = { isElementHided: isElementHided, getVisibleColumns: getVisibleColumns, containerId: 'scyCadastroUsuario_dGrid_container', dataBind: function (commitData, forceCreating) {
                   var grid = $('#scyCadastroUsuario_dGrid');
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
                       $('#scyCadastroUsuario_dGrid_groupbyarea').addClass('hide');
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
                        case 'NomeUsuario': { canEditing = clear; break;}
                        case 'NomeUsuarioCopia': { canEditing = clear; break;}
                        case 'LxPfjFisicaJuridica': { canEditing = clear; break;}
                        case 'CnpjCpf': { canEditing = clear; break;}
                        case 'InscrEstadualRg': { canEditing = clear; break;}
                        case 'DataAlteracao': { canEditing = clear; break;}
                        case 'DataCadastro': { canEditing = clear; break;}
                        case 'LxTipoLogradouro': { canEditing = clear; break;}
                        case 'Logradouro': { canEditing = clear; break;}
                        case 'Numero': { canEditing = clear; break;}
                        case 'Complemento': { canEditing = clear; break;}
                        case 'Cep': { canEditing = clear; break;}
                        case 'Bairro': { canEditing = clear; break;}
                        case 'Municipio': { canEditing = clear; break;}
                        case 'Uf': { canEditing = clear; break;}
                        case 'ObsEndereco': { canEditing = clear; break;}
                        case 'FoneFixo': { canEditing = clear; break;}
                        case 'Ramal': { canEditing = clear; break;}
                        case 'FoneCelular': { canEditing = clear; break;}
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
                            $('#scyCadastroUsuario_dGrid_LayoutBtn').igPopover('hide');
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
                                      showMultimidia(entity, e, table, key, vm.CadastroUsuario());
                                 }
                             }
                             if (typeof vm.OnGridClientClick === 'function') {
                                 entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowKey);
                                 vm.OnGridClientClick('scyCadastroUsuario_dGrid', ui.colKey, entity);
                             }
                             if (vm.status() != 'Q') {
                                 var grid = $('#scyCadastroUsuario_dGrid');
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
                            { key: 'NomeUsuario', headerText: vm.getLayoutDisplayName('CadastroUsuario_tbNomeUsuario'), width: '400px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'NomeUsuarioCopia', headerText: vm.getLayoutDisplayName('CadastroUsuario_lUpNomeUsuarioCopia'), width: '421px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: true, unbound: false, group: null   },
                            { key: 'LxPfjFisicaJuridica', headerText: vm.getLayoutDisplayName('CadastroUsuario_cmbLxPfjFisicaJuridica'), width: '348px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null  , formatter: function (val, record) { return  vm.dataDomains.getName('LX_PFJ_FISICA_JURIDICA', val);} },
                            { key: 'CnpjCpf', headerText: vm.getLayoutDisplayName('CadastroUsuario_mskCnpjCpf'), width: '200px', dataType: 'string', columnCssClass: 'ellipsis', hidden: false, formatter: function(val) { return (val == null ? '' : val.toString()).mask('###.###.###-##') } },
                            { key: 'InscrEstadualRg', headerText: vm.getLayoutDisplayName('CadastroUsuario_tbInscrEstadualRg'), width: '296px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'DataAlteracao', headerText: vm.getLayoutDisplayName('CadastroUsuario_dtDataAlteracao'), width: '153px', dataType: 'date', columnCssClass: 'ellipsis', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null   },
                            { key: 'DataCadastro', headerText: vm.getLayoutDisplayName('CadastroUsuario_dtDataCadastro'), width: '140px', dataType: 'date', columnCssClass: 'ellipsis', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null   },
                            { key: 'LxTipoLogradouro', headerText: vm.getLayoutDisplayName('CadastroUsuario_cmbLxTipoLogradouro'), width: '231px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null  , formatter: function (val, record) { return  vm.dataDomains.getName('LxTipoLogradouro', val);} },
                            { key: 'Logradouro', headerText: vm.getLayoutDisplayName('CadastroUsuario_tbLogradouro'), width: '400px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'Numero', headerText: vm.getLayoutDisplayName('CadastroUsuario_tbNumero'), width: '400px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'Complemento', headerText: vm.getLayoutDisplayName('CadastroUsuario_tbComplemento'), width: '400px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'Cep', headerText: vm.getLayoutDisplayName('CadastroUsuario_tbCep'), width: '100px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'Bairro', headerText: vm.getLayoutDisplayName('CadastroUsuario_tbBairro'), width: '400px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'Municipio', headerText: vm.getLayoutDisplayName('CadastroUsuario_tbMunicipio'), width: '400px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'Uf', headerText: vm.getLayoutDisplayName('CadastroUsuario_tbUf'), width: '70px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'ObsEndereco', headerText: vm.getLayoutDisplayName('CadastroUsuario_tbObsEndereco'), width: '400px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'FoneFixo', headerText: vm.getLayoutDisplayName('CadastroUsuario_tbFoneFixo'), width: '200px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'Ramal', headerText: vm.getLayoutDisplayName('CadastroUsuario_tbRamal'), width: '101px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'FoneCelular', headerText: vm.getLayoutDisplayName('CadastroUsuario_tbFoneCelular'), width: '200px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   }
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
                        
                                        var dataView = $('#scyCadastroUsuario_dGrid').data('igGrid').dataSource;
                                        if (dataView.settings.filtering.expressions.length <= 0)
                                            dataView._filteredData = [];
                        
                                        var col = ui.owner._dialogCurrentColumn;
                        
                                        var divDialog = $('#' + dgl[0].id).find('.ui-iggrid-filterdialogaddcondition').find('span')[0];
                        
                                        var scriptHtml = '<div id="' + divDinamica + '">';
                                        scriptHtml += '  <script>';
                                        scriptHtml += '    var newCol = "' + col + '";';
                                        scriptHtml += '    var newGrid = $("#scyCadastroUsuario_dGrid");';
                                        scriptHtml += '    var listFilter = [];';
                                        scriptHtml += '    function hideColumn(){';
                                        scriptHtml += '     if ($("#showHideColumn_scyCadastroUsuario_dGrid")["0"].innerHTML.indexOf("Ocultar") >= 0) {';
                                        scriptHtml += '        $("#showHideColumn_scyCadastroUsuario_dGrid")["0"].innerHTML = "Mostrar Coluna";';
                                        scriptHtml += '        newGrid.igGridHiding("hideColumn", newCol);';
                                        scriptHtml += '     }';
                                        scriptHtml += '     else{';
                                        scriptHtml += '        $("#showHideColumn_scyCadastroUsuario_dGrid")["0"].innerHTML = "Ocultar Coluna";';
                                        scriptHtml += '        newGrid.igGridHiding("showColumn", newCol);';
                                        scriptHtml += '     }';
                                        scriptHtml += '    }';
                                        scriptHtml += '    function updateHideButton(){';
                                        scriptHtml += '         var column = $.grep(newGrid.igGrid("option", "columns"), function (element, index) { return element.key == newCol });';
                                        scriptHtml += '         if (column.length > 0){';
                                        scriptHtml += '             $("#showHideColumn_scyCadastroUsuario_dGrid")["0"].innerHTML = column[0].hidden ? "Mostrar Coluna" : "Ocultar Coluna"';
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
                                        scriptHtml += '              <div id="comboFields_scyCadastroUsuario_dGrid"></div>';
                                        scriptHtml += '              <script>';
                                        scriptHtml += '                  var columns = newGrid.igGrid("option", "columns");';
                                        scriptHtml += '                  $("#comboFields_scyCadastroUsuario_dGrid").igCombo({ dataSource: columns, mode : "dropdown", valueKey: "key", textKey: "headerText", selectionChanging: function (evt, ui) {';
                                        scriptHtml += '                      newCol = ui.items["0"].data.key;';
                                        scriptHtml += '                      updateHideButton()';
                                        scriptHtml += '                  }});';
                                        scriptHtml += '                  $("#comboFields_scyCadastroUsuario_dGrid").igCombo("value", newCol);';
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
                                        scriptHtml += '              <i class="fa fa-eye-slash" aria-hidden="true" style="margin-right: 5px;"></i><a onclick="hideColumn()" style="cursor: pointer" id="showHideColumn_scyCadastroUsuario_dGrid">Ocultar Coluna</a>';
                                        scriptHtml += '          </div>';
                                        scriptHtml += '          <br>';
                                        scriptHtml += '      </div>';
                                        scriptHtml += '  </div>';
                        
                                        $(scriptHtml).insertBefore(divDialog);
                                   },
                             },
                                    { name: 'Selection', mode: 'row'
                                    }, 
                                    { name: 'Tooltips', columnSettings:[{ columnKey: "NomeUsuario", allowTooltips: true },{ columnKey: "NomeUsuarioCopia", allowTooltips: false },{ columnKey: "LxPfjFisicaJuridica", allowTooltips: true },{ columnKey: "CnpjCpf", allowTooltips: true },{ columnKey: "InscrEstadualRg", allowTooltips: true },{ columnKey: "DataAlteracao", allowTooltips: true },{ columnKey: "DataCadastro", allowTooltips: true },{ columnKey: "LxTipoLogradouro", allowTooltips: true },{ columnKey: "Logradouro", allowTooltips: true },{ columnKey: "Numero", allowTooltips: true },{ columnKey: "Complemento", allowTooltips: true },{ columnKey: "Cep", allowTooltips: true },{ columnKey: "Bairro", allowTooltips: true },{ columnKey: "Municipio", allowTooltips: true },{ columnKey: "Uf", allowTooltips: true },{ columnKey: "ObsEndereco", allowTooltips: true },{ columnKey: "FoneFixo", allowTooltips: true },{ columnKey: "Ramal", allowTooltips: true },{ columnKey: "FoneCelular", allowTooltips: true }] },
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
                                      columnSettings: [{ columnKey: "NomeUsuarioCopia", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpTcsUsuario", isNullable: true, custom: vm.custom, vm: vm, verifyCanEditCol: verifyCanEditCol, allowMultiSelectionInSearch:true, activateAutoComplete: false, autoCompleteMaxResults: 7, validateOnClearState:false, maxValue:null, maxLength: 250, defaultValue: '' } }, { columnKey: 'DataAlteracao', editorType: 'datepicker', editorOptions: {valueChanged: function(evt, ui){if (typeof vm.OnPropertyChangeDataGrid === 'function') {vm.OnPropertyChangeDataGrid('scyCadastroUsuario_dGrid', 'DataAlteracao', ui.oldValue, ui.value);}}, minValue: new Date(1900, 0, 1), datepickerOptions: { changeMonth: true, changeYear: true }  } }, { columnKey: 'DataCadastro', editorType: 'datepicker', editorOptions: {valueChanged: function(evt, ui){if (typeof vm.OnPropertyChangeDataGrid === 'function') {vm.OnPropertyChangeDataGrid('scyCadastroUsuario_dGrid', 'DataCadastro', ui.oldValue, ui.value);}}, minValue: new Date(1900, 0, 1), datepickerOptions: { changeMonth: true, changeYear: true }  } }, { columnKey: 'LxPfjFisicaJuridica', editorType: 'combo', editorOptions: {   selectionChanged: function (evt, ui) {   var val = null;   if (ui.items != null && ui.items.length > 0) { val = ui.items[0].data['id']; }  updateEntity('LxPfjFisicaJuridica', val, false); },  mode: 'dropdown', dropDownOnFocus: true,  dataSource: vm.dataDomains.getItems('LX_PFJ_FISICA_JURIDICA', ''),  textKey: 'name', valueKey: 'id', enableClearButton: true }}, { columnKey: 'LxTipoLogradouro', editorType: 'combo', editorOptions: {   selectionChanged: function (evt, ui) {   var val = null;   if (ui.items != null && ui.items.length > 0) { val = ui.items[0].data['id']; }  updateEntity('LxTipoLogradouro', val, false); },  mode: 'dropdown', dropDownOnFocus: true,  dataSource: vm.dataDomains.getItems('LxTipoLogradouro', ''),  textKey: 'name', valueKey: 'id', enableClearButton: true }}, { columnKey: "NomeUsuario" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('scyCadastroUsuario_dGrid', 'NomeUsuario', ui.oldValue, ui.value);}}, maxLength: 250 } }, { columnKey: "CnpjCpf", editorOptions: { type: "mask", inputMask: "###.###.###-##", dataMode: 0 } }, { columnKey: "InscrEstadualRg" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('scyCadastroUsuario_dGrid', 'InscrEstadualRg', ui.oldValue, ui.value);}}, maxLength: 20 } }, { columnKey: "Logradouro" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('scyCadastroUsuario_dGrid', 'Logradouro', ui.oldValue, ui.value);}}, maxLength: 60 } }, { columnKey: "Numero" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('scyCadastroUsuario_dGrid', 'Numero', ui.oldValue, ui.value);}}, maxLength: 60 } }, { columnKey: "Complemento" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('scyCadastroUsuario_dGrid', 'Complemento', ui.oldValue, ui.value);}}, maxLength: 60 } }, { columnKey: "Cep" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('scyCadastroUsuario_dGrid', 'Cep', ui.oldValue, ui.value);}}, maxLength: 10 } }, { columnKey: "Bairro" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('scyCadastroUsuario_dGrid', 'Bairro', ui.oldValue, ui.value);}}, maxLength: 60 } }, { columnKey: "Municipio" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('scyCadastroUsuario_dGrid', 'Municipio', ui.oldValue, ui.value);}}, maxLength: 60 } }, { columnKey: "Uf" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('scyCadastroUsuario_dGrid', 'Uf', ui.oldValue, ui.value);}}, maxLength: 4 } }, { columnKey: "ObsEndereco" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('scyCadastroUsuario_dGrid', 'ObsEndereco', ui.oldValue, ui.value);}}, maxLength: 250 } }, { columnKey: "FoneFixo" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('scyCadastroUsuario_dGrid', 'FoneFixo', ui.oldValue, ui.value);}}, maxLength: 20 } }, { columnKey: "Ramal" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('scyCadastroUsuario_dGrid', 'Ramal', ui.oldValue, ui.value);}}, maxLength: 6 } }, { columnKey: "FoneCelular" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('scyCadastroUsuario_dGrid', 'FoneCelular', ui.oldValue, ui.value);}}, maxLength: 20 } }],
                                      rowDeleting: function (evt, ui) {
                                          deletedIndex = ui.element.context.rowIndex;
                                          var entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowID);
                                          if (entity) {
                                              vm.deleteEntity(entity);
                                          }
                                      },
                                      rowDeleted: function (evt, ui) {
                                          var grid = $('#scyCadastroUsuario_dGrid');
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
                                          var columns = $('#scyCadastroUsuario_dGrid').igGridUpdating('option', 'columnSettings');
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
                            vm.OnDataGridCreated('scyCadastroUsuario_dGrid');
                        }
                        var selectionrowselectionchanged = null, selectedRowId = -1;
                        selectionrowselectionchanged = function (evt, ui) {
                            if ((ui.owner.grid.selectedRow() && typeof ui.owner.grid.selectedRow().id !== 'undefined') || (ui.owner.grid.selectedRows() && ui.owner.grid.selectedRows().length > 0)) { 
                                if (isNullOrEmpty(ui.owner.selectedRows())|| ui.selectedRows.length <= 1) {
                                    $(document).undelegate('#scyCadastroUsuario_dGrid', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                                    ui.owner.clearSelection();
                                    ui.owner.selectRow(ui.row.index);
                                    if (vm.status() === 'Q'){
                                        var gridCell = ui.owner.grid;
                                        grid.find('div.borderCell').remove();
                                        //$(gridCell.cellAt(-1, ui.owner._rowIndex)).append(" < div class='borderCell' style='z-index:100; border: 1px solid #849fd9 !important;'></div>");
                                    }
                                    selectedRowId = ui.row.id;
                                    $(document).delegate ('#scyCadastroUsuario_dGrid', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                                }
                                selectGridCurrentItem(vm.goToKey, 'RowDataId', ui); 
                             } 
                        };
                        $(document).delegate('#scyCadastroUsuario_dGrid', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                        $('#scyCadastroUsuario_dGrid > tbody tr').live('focus', function(evt) {
                            var grid = $('#scyCadastroUsuario_dGrid'), row = $(this).closest('tr'), id = parseInt(row.attr('data-id'), 10);
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
                            if (vm.CadastroUsuario().status() === 'Q') vm.CadastroUsuario().dataToolbar.viewInfo();
                        });
                    }
                    vm.addDataSource({ key: 'scyCadastroUsuario_dGrid', name: 'dataView', itemsSource: itemsSource });
                }
            
        };
        
        return complement;
    }
    
    return complementCtor;
});
