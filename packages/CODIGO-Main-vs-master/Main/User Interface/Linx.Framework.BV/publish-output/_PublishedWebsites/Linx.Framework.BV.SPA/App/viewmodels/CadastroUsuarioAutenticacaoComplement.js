define(['managers/__auth', 'managers/user'], function (managerAuth, managerUser) {
    var complementCtor = function() {
        var complement = {
            isAutomatic: true
            , ChangedBrandCadastroUsuarioAutenticacao_dGridTcsUsuarioAcesso: function(vm, decimals, reset) {
                var i, format = '0.'.concat('0'.repeat(decimals)), grd =$('#CadastroUsuarioAutenticacao_dGridTcsUsuarioAcesso').data('igGrid'),
                    grdUpd = $('#CadastroUsuarioAutenticacao_dGridTcsUsuarioAcesso').data('igGridUpdating');
                if(isNull(grd) || isNull(grdUpd)) return;
                for (i = 0; i < grd.options.columns.length; i++) {
                }
                for (i = 0; i < grdUpd.options.columnSettings.length; i++) {
                }
                grd.dataBind();
            }
            , renderCadastroUsuarioAutenticacao_dGridTcsUsuarioAcesso: function(vm) {
                var getDataSource = function() {
                    var source = null;
                    try {
                        source = vm.currentDataItem().TcsUsuarioAcessoList;
                    }
                    catch (e) { }
                    return isNullOrEmpty(source) ? ko.observableArray([]) : source;
                };
                var dataSourceIsLoaded = function() {
                    var isLoaded = false;
                    try {
                        isLoaded = (vm.currentDataItem().TcsUsuarioAcessoIsLoaded === true || vm.currentDataItem().TcsUsuarioAcessoList().length > 0);
                    }
                    catch (e) {
                        isLoaded = true;
                    }
                    return isLoaded;
                }
                $('#CadastroUsuarioAutenticacao_dGridTcsUsuarioAcesso_headers').live('focus  keydown', function (evt) {
                    var keyCode = window.event ? evt.which : evt.keyCode;
                    if (keyCode === 9) {
                        var cols = $('#CadastroUsuarioAutenticacao_dGridTcsUsuarioAcesso').igGrid('option', 'columns');
                        var dataView = $('#CadastroUsuarioAutenticacao_dGridTcsUsuarioAcesso').data('igGrid').dataSource._dataView
                        if (dataView.length === 0) return;
                        var firstRow = dataView[0].RowDataId;
                        clear = vm.status() === 'C';
                        if (vm.status() === 'C')
                            $('#CadastroUsuarioAutenticacao_dGridTcsUsuarioAcesso').igGridUpdating('startEdit', firstRow, 0, true);
                        else {
                            var entity = findElementByKey(getDataSource(), 'RowDataId', firstRow);
                            var indexColumn = 0;
                            cols.some(function (entry) {
                                if (entry.key !== 'RowDataId' && !entry.hidden) {
                                    if (verifyCanEditCol(entry.key, clear, entity)) {
                                        $('#CadastroUsuarioAutenticacao_dGridTcsUsuarioAcesso').igGridUpdating('startEdit', firstRow, indexColumn, true);
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
                   if($('#CadastroUsuarioAutenticacao_dGridTcsUsuarioAcesso').data('igGrid') === undefined) return '';
                   var cols = $('#CadastroUsuarioAutenticacao_dGridTcsUsuarioAcesso').igGrid('option', 'columns');
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
                  if (!grid) grid = $('#CadastroUsuarioAutenticacao_dGridTcsUsuarioAcesso');
                  return ((!grid[0] || (!forceCreating && grid.parent().width() <= 0)) && !$('#dialogTcsUsuarioAcesso').is(':visible'));
                }
                var refreshData = true;
                var itemsSource = { isElementHided: isElementHided, getVisibleColumns: getVisibleColumns, containerId: 'CadastroUsuarioAutenticacao_dGridTcsUsuarioAcesso_container', dataBind: function (commitData, forceCreating) {
                   var grid = $('#CadastroUsuarioAutenticacao_dGridTcsUsuarioAcesso');
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
                       $('#CadastroUsuarioAutenticacao_dGridTcsUsuarioAcesso_groupbyarea').addClass('hide');
                   }
                   if (grid.igGridUpdating('isEditing')) {
                        grid.igGridUpdating('endEdit', true);
                   }
                   if (execFillDetais) {
                     grid.igGrid("option", "dataSource", []);
                     vm.currentDataItem().fillDetails(false, 'TcsUsuarioAcesso');
                     return;
                   }
                   grid.igGrid("option", "dataSource", unwrapObservableArray(getDataSource(), vm));
                   var rows = grid.igGrid('allRows');
                   if (rows.length > 0) {
                     var verticalContainer = grid.igGrid('scrollContainer');
                     var isSelected = false;
                     if (vm.currentDataItem().currentTcsUsuarioAcesso() != null)
                     {
                       for(var idx = 0; idx < rows.length; idx++)
                       {
                         if (rows[idx].dataset.id == getAbsoluteValue(vm.currentDataItem().currentTcsUsuarioAcesso().RowDataId))
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
                     if ($('#dialogTcsUsuarioAcesso').is(':visible')) {
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
                           $('label#currentNumberTcsUsuarioAcesso').html(current + ' - ' + totalCurrentPage);
                        }
                        else
                           $('label#currentNumberTcsUsuarioAcesso').html(1);
                        $('label#totalNumberTcsUsuarioAcesso').html(totalGrid);
                    }
                   } else {
                       $('label#currentNumberTcsUsuarioAcesso').html(0);
                       $('label#totalNumberTcsUsuarioAcesso').html(0);
                   }
                }};
                var valueGrouBy = -1;
                var deletedIndex = -1;
                function verifyCanEditCol(column, clear, entity){
                    switch(column){
                        case 'DescricaoAmbiente': { canEditing = clear || (entity && entity.isAdded()); break;}
                        case 'IndicaAcessoPadrao': { canEditing = clear || vm.enabledForEditing(); break;}
                        case 'IndicaAdministrador': { canEditing = clear || vm.enabledForEditing(); break;}
                        case 'IndicaMultiGpecon': { canEditing = clear || vm.enabledForEditing(); break;}
                        case 'DescricaoAplicacao': { canEditing = clear; break;}
                        case 'EmDesenvolvimento': { canEditing = clear; break;}
                        case 'DescricaoAmbienteRelacionado': { canEditing = clear || vm.enabledForEditing(); break;}
                        case 'NomeEmpresaAmbienteRelacionado': { canEditing = clear; break;}
                        case 'DescricaoAplicacaoAmbienteRelacionado': { canEditing = clear; break;}
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
                            $('#CadastroUsuarioAutenticacao_dGridTcsUsuarioAcesso_LayoutBtn').igPopover('hide');
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
                                      showMultimidia(entity, e, table, key, vm.CadastroUsuarioAutenticacao());
                                 }
                             }
                             if (typeof vm.OnGridClientClick === 'function') {
                                 entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowKey);
                                 vm.OnGridClientClick('CadastroUsuarioAutenticacao_dGridTcsUsuarioAcesso', ui.colKey, entity);
                             }
                             if (vm.status() != 'Q') {
                                 var grid = $('#CadastroUsuarioAutenticacao_dGridTcsUsuarioAcesso');
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
                            { key: 'DescricaoAmbiente', headerText: vm.getLayoutHeaderGrid('CadastroUsuarioAutenticacao_lUpTcsUsuarioAcesso_DescricaoAmbiente'), width: '421px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'IndicaAcessoPadrao', headerText: vm.getLayoutHeaderGrid('CadastroUsuarioAutenticacao_ckTcsUsuarioAcesso_IndicaAcessoPadrao'), width: '205px', dataType: 'bool', columnCssClass: 'ellipsis', format: 'checkbox', hidden: false, unbound: false, group: null   },
                            { key: 'IndicaAdministrador', headerText: vm.getLayoutHeaderGrid('CadastroUsuarioAutenticacao_ckTcsUsuarioAcesso_IndicaAdministrador'), width: '205px', dataType: 'bool', columnCssClass: 'ellipsis', format: 'checkbox', hidden: false, unbound: false, group: null   },
                            { key: 'IndicaMultiGpecon', headerText: vm.getLayoutHeaderGrid('CadastroUsuarioAutenticacao_ckTcsUsuarioAcesso_IndicaMultiGpecon'), width: '309px', dataType: 'bool', columnCssClass: 'ellipsis', format: 'checkbox', hidden: false, unbound: false, group: null   },
                            { key: 'DescricaoAplicacao', headerText: vm.getLayoutHeaderGrid('CadastroUsuarioAutenticacao_lUpTcsUsuarioAcesso_DescricaoAplicacao'), width: '421px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'EmDesenvolvimento', headerText: vm.getLayoutHeaderGrid('CadastroUsuarioAutenticacao_ckTcsUsuarioAcesso_EmDesenvolvimento'), width: '270px', dataType: 'bool', columnCssClass: 'ellipsis', format: 'checkbox', hidden: false, unbound: false, group: null   },
                            { key: 'DescricaoAmbienteRelacionado', headerText: vm.getLayoutHeaderGrid('CadastroUsuarioAutenticacao_lUpTcsUsuarioAcesso_DescricaoAmbienteRelacionado'), width: '421px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'NomeEmpresaAmbienteRelacionado', headerText: vm.getLayoutHeaderGrid('CadastroUsuarioAutenticacao_tbTcsUsuarioAcesso_NomeEmpresaAmbienteRelacionado'), width: '400px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'DescricaoAplicacaoAmbienteRelacionado', headerText: vm.getLayoutHeaderGrid('CadastroUsuarioAutenticacao_tbTcsUsuarioAcesso_DescricaoAplicacaoAmbienteRelacionado'), width: '426px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   }
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
                        
                                        var dataView = $('#CadastroUsuarioAutenticacao_dGridTcsUsuarioAcesso').data('igGrid').dataSource;
                                        if (dataView.settings.filtering.expressions.length <= 0)
                                            dataView._filteredData = [];
                        
                                        var col = ui.owner._dialogCurrentColumn;
                        
                                        var divDialog = $('#' + dgl[0].id).find('.ui-iggrid-filterdialogaddcondition').find('span')[0];
                        
                                        var scriptHtml = '<div id="' + divDinamica + '">';
                                        scriptHtml += '  <script>';
                                        scriptHtml += '    var newCol = "' + col + '";';
                                        scriptHtml += '    var newGrid = $("#CadastroUsuarioAutenticacao_dGridTcsUsuarioAcesso");';
                                        scriptHtml += '    var listFilter = [];';
                                        scriptHtml += '    function hideColumn(){';
                                        scriptHtml += '     if ($("#showHideColumn_CadastroUsuarioAutenticacao_dGridTcsUsuarioAcesso")["0"].innerHTML.indexOf("Ocultar") >= 0) {';
                                        scriptHtml += '        $("#showHideColumn_CadastroUsuarioAutenticacao_dGridTcsUsuarioAcesso")["0"].innerHTML = "Mostrar Coluna";';
                                        scriptHtml += '        newGrid.igGridHiding("hideColumn", newCol);';
                                        scriptHtml += '     }';
                                        scriptHtml += '     else{';
                                        scriptHtml += '        $("#showHideColumn_CadastroUsuarioAutenticacao_dGridTcsUsuarioAcesso")["0"].innerHTML = "Ocultar Coluna";';
                                        scriptHtml += '        newGrid.igGridHiding("showColumn", newCol);';
                                        scriptHtml += '     }';
                                        scriptHtml += '    }';
                                        scriptHtml += '    function updateHideButton(){';
                                        scriptHtml += '         var column = $.grep(newGrid.igGrid("option", "columns"), function (element, index) { return element.key == newCol });';
                                        scriptHtml += '         if (column.length > 0){';
                                        scriptHtml += '             $("#showHideColumn_CadastroUsuarioAutenticacao_dGridTcsUsuarioAcesso")["0"].innerHTML = column[0].hidden ? "Mostrar Coluna" : "Ocultar Coluna"';
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
                                        scriptHtml += '              <div id="comboFields_CadastroUsuarioAutenticacao_dGridTcsUsuarioAcesso"></div>';
                                        scriptHtml += '              <script>';
                                        scriptHtml += '                  var columns = newGrid.igGrid("option", "columns");';
                                        scriptHtml += '                  $("#comboFields_CadastroUsuarioAutenticacao_dGridTcsUsuarioAcesso").igCombo({ dataSource: columns, mode : "dropdown", valueKey: "key", textKey: "headerText", selectionChanging: function (evt, ui) {';
                                        scriptHtml += '                      newCol = ui.items["0"].data.key;';
                                        scriptHtml += '                      updateHideButton()';
                                        scriptHtml += '                  }});';
                                        scriptHtml += '                  $("#comboFields_CadastroUsuarioAutenticacao_dGridTcsUsuarioAcesso").igCombo("value", newCol);';
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
                                        scriptHtml += '              <i class="fa fa-eye-slash" aria-hidden="true" style="margin-right: 5px;"></i><a onclick="hideColumn()" style="cursor: pointer" id="showHideColumn_CadastroUsuarioAutenticacao_dGridTcsUsuarioAcesso">Ocultar Coluna</a>';
                                        scriptHtml += '          </div>';
                                        scriptHtml += '          <br>';
                                        scriptHtml += '      </div>';
                                        scriptHtml += '  </div>';
                        
                                        $(scriptHtml).insertBefore(divDialog);
                                   },
                             },
                                    { name: 'Selection', mode: 'row'
                                    }, 
                                    { name: 'Tooltips', columnSettings:[{ columnKey: "DescricaoAmbiente", allowTooltips: false },{ columnKey: "IndicaAcessoPadrao", allowTooltips: true },{ columnKey: "IndicaAdministrador", allowTooltips: true },{ columnKey: "IndicaMultiGpecon", allowTooltips: true },{ columnKey: "DescricaoAplicacao", allowTooltips: false },{ columnKey: "EmDesenvolvimento", allowTooltips: true },{ columnKey: "DescricaoAmbienteRelacionado", allowTooltips: false },{ columnKey: "NomeEmpresaAmbienteRelacionado", allowTooltips: true },{ columnKey: "DescricaoAplicacaoAmbienteRelacionado", allowTooltips: true }] },
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
                                      columnSettings: [{ columnKey: "DescricaoAmbiente", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpTcsAmbiente", isNullable: false, custom: vm.custom, vm: vm, verifyCanEditCol: verifyCanEditCol, allowMultiSelectionInSearch:true, activateAutoComplete: false, autoCompleteMaxResults: 7, validateOnClearState:false, maxValue:null, maxLength: 250, defaultValue: '' } }, { columnKey: "DescricaoAplicacao", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpTcsAmbiente", isNullable: false, custom: vm.custom, vm: vm, verifyCanEditCol: verifyCanEditCol, allowMultiSelectionInSearch:true, activateAutoComplete: false, autoCompleteMaxResults: 7, validateOnClearState:false, maxValue:null, maxLength: 60, defaultValue: '' } }, { columnKey: "DescricaoAmbienteRelacionado", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpTcsAmbiente1", isNullable: true, custom: vm.custom, vm: vm, verifyCanEditCol: verifyCanEditCol, allowMultiSelectionInSearch:true, activateAutoComplete: false, autoCompleteMaxResults: 7, validateOnClearState:false, maxValue:null, maxLength: 250, defaultValue: '' } }, { columnKey: "NomeEmpresaAmbienteRelacionado" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('CadastroUsuarioAutenticacao_dGridTcsUsuarioAcesso', 'NomeEmpresaAmbienteRelacionado', ui.oldValue, ui.value);}}, maxLength: 250 } }, { columnKey: "DescricaoAplicacaoAmbienteRelacionado" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('CadastroUsuarioAutenticacao_dGridTcsUsuarioAcesso', 'DescricaoAplicacaoAmbienteRelacionado', ui.oldValue, ui.value);}}, maxLength: 60 } }],
                                      rowDeleting: function (evt, ui) {
                                          deletedIndex = ui.element.context.rowIndex;
                                          var entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowID);
                                          if (entity) {
                                              vm.deleteEntity(entity);
                                          }
                                      },
                                      rowDeleted: function (evt, ui) {
                                          var grid = $('#CadastroUsuarioAutenticacao_dGridTcsUsuarioAcesso');
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
                                          var columns = $('#CadastroUsuarioAutenticacao_dGridTcsUsuarioAcesso').igGridUpdating('option', 'columnSettings');
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
                                                     vm.dataCombo.fillDataCombos(lookUpName, ui.columnKey, vm.currentDataItem().currentTcsUsuarioAcesso(), function (result) {
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
                            vm.OnDataGridCreated('CadastroUsuarioAutenticacao_dGridTcsUsuarioAcesso');
                        }
                        var selectionrowselectionchanged = null, selectedRowId = -1;
                        selectionrowselectionchanged = function (evt, ui) {
                            if ((ui.owner.grid.selectedRow() && typeof ui.owner.grid.selectedRow().id !== 'undefined') || (ui.owner.grid.selectedRows() && ui.owner.grid.selectedRows().length > 0)) { 
                                if (isNullOrEmpty(ui.owner.selectedRows())|| ui.selectedRows.length <= 1) {
                                    $(document).undelegate('#CadastroUsuarioAutenticacao_dGridTcsUsuarioAcesso', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                                    ui.owner.clearSelection();
                                    ui.owner.selectRow(ui.row.index);
                                    if (vm.status() === 'Q'){
                                        var gridCell = ui.owner.grid;
                                        grid.find('div.borderCell').remove();
                                        //$(gridCell.cellAt(-1, ui.owner._rowIndex)).append(" < div class='borderCell' style='z-index:100; border: 1px solid #849fd9 !important;'></div>");
                                    }
                                    selectedRowId = ui.row.id;
                                    $(document).delegate ('#CadastroUsuarioAutenticacao_dGridTcsUsuarioAcesso', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                                }
                                selectGridCurrentItem(vm.goToKey, 'RowDataId', ui, vm.currentDataItem().currentTcsUsuarioAcesso, getDataSource()); 
                             } 
                        };
                        $(document).delegate('#CadastroUsuarioAutenticacao_dGridTcsUsuarioAcesso', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                        $('#CadastroUsuarioAutenticacao_dGridTcsUsuarioAcesso > tbody tr').live('focus', function(evt) {
                            var grid = $('#CadastroUsuarioAutenticacao_dGridTcsUsuarioAcesso'), row = $(this).closest('tr'), id = parseInt(row.attr('data-id'), 10);
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
                    vm.addDataSource({ key: 'CadastroUsuarioAutenticacao_dGridTcsUsuarioAcesso', name: 'TcsUsuarioAcessoList', itemsSource: itemsSource });
                }
            
            , ChangedBrandCadastroUsuarioAutenticacao_dGridTcsIdentidadeExterna: function(vm, decimals, reset) {
                var i, format = '0.'.concat('0'.repeat(decimals)), grd =$('#CadastroUsuarioAutenticacao_dGridTcsIdentidadeExterna').data('igGrid'),
                    grdUpd = $('#CadastroUsuarioAutenticacao_dGridTcsIdentidadeExterna').data('igGridUpdating');
                if(isNull(grd) || isNull(grdUpd)) return;
                for (i = 0; i < grd.options.columns.length; i++) {
                }
                for (i = 0; i < grdUpd.options.columnSettings.length; i++) {
                }
                grd.dataBind();
            }
            , renderCadastroUsuarioAutenticacao_dGridTcsIdentidadeExterna: function(vm) {
                var getDataSource = function() {
                    var source = null;
                    try {
                        source = vm.currentDataItem().TcsIdentidadeExternaList;
                    }
                    catch (e) { }
                    return isNullOrEmpty(source) ? ko.observableArray([]) : source;
                };
                var dataSourceIsLoaded = function() {
                    var isLoaded = false;
                    try {
                        isLoaded = (vm.currentDataItem().TcsIdentidadeExternaIsLoaded === true || vm.currentDataItem().TcsIdentidadeExternaList().length > 0);
                    }
                    catch (e) {
                        isLoaded = true;
                    }
                    return isLoaded;
                }
                $('#CadastroUsuarioAutenticacao_dGridTcsIdentidadeExterna_headers').live('focus  keydown', function (evt) {
                    var keyCode = window.event ? evt.which : evt.keyCode;
                    if (keyCode === 9) {
                        var cols = $('#CadastroUsuarioAutenticacao_dGridTcsIdentidadeExterna').igGrid('option', 'columns');
                        var dataView = $('#CadastroUsuarioAutenticacao_dGridTcsIdentidadeExterna').data('igGrid').dataSource._dataView
                        if (dataView.length === 0) return;
                        var firstRow = dataView[0].RowDataId;
                        clear = vm.status() === 'C';
                        if (vm.status() === 'C')
                            $('#CadastroUsuarioAutenticacao_dGridTcsIdentidadeExterna').igGridUpdating('startEdit', firstRow, 0, true);
                        else {
                            var entity = findElementByKey(getDataSource(), 'RowDataId', firstRow);
                            var indexColumn = 0;
                            cols.some(function (entry) {
                                if (entry.key !== 'RowDataId' && !entry.hidden) {
                                    if (verifyCanEditCol(entry.key, clear, entity)) {
                                        $('#CadastroUsuarioAutenticacao_dGridTcsIdentidadeExterna').igGridUpdating('startEdit', firstRow, indexColumn, true);
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
                   if($('#CadastroUsuarioAutenticacao_dGridTcsIdentidadeExterna').data('igGrid') === undefined) return '';
                   var cols = $('#CadastroUsuarioAutenticacao_dGridTcsIdentidadeExterna').igGrid('option', 'columns');
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
                  if (!grid) grid = $('#CadastroUsuarioAutenticacao_dGridTcsIdentidadeExterna');
                  return ((!grid[0] || (!forceCreating && grid.parent().width() <= 0)) && !$('#dialogTcsIdentidadeExterna').is(':visible'));
                }
                var refreshData = true;
                var itemsSource = { isElementHided: isElementHided, getVisibleColumns: getVisibleColumns, containerId: 'CadastroUsuarioAutenticacao_dGridTcsIdentidadeExterna_container', dataBind: function (commitData, forceCreating) {
                   var grid = $('#CadastroUsuarioAutenticacao_dGridTcsIdentidadeExterna');
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
                       $('#CadastroUsuarioAutenticacao_dGridTcsIdentidadeExterna_groupbyarea').addClass('hide');
                   }
                   if (grid.igGridUpdating('isEditing')) {
                        grid.igGridUpdating('endEdit', true);
                   }
                   if (execFillDetais) {
                     grid.igGrid("option", "dataSource", []);
                     vm.currentDataItem().fillDetails(false, 'TcsIdentidadeExterna');
                     return;
                   }
                   grid.igGrid("option", "dataSource", unwrapObservableArray(getDataSource(), vm));
                   var rows = grid.igGrid('allRows');
                   if (rows.length > 0) {
                     var verticalContainer = grid.igGrid('scrollContainer');
                     var isSelected = false;
                     if (vm.currentDataItem().currentTcsIdentidadeExterna() != null)
                     {
                       for(var idx = 0; idx < rows.length; idx++)
                       {
                         if (rows[idx].dataset.id == getAbsoluteValue(vm.currentDataItem().currentTcsIdentidadeExterna().RowDataId))
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
                     if ($('#dialogTcsIdentidadeExterna').is(':visible')) {
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
                           $('label#currentNumberTcsIdentidadeExterna').html(current + ' - ' + totalCurrentPage);
                        }
                        else
                           $('label#currentNumberTcsIdentidadeExterna').html(1);
                        $('label#totalNumberTcsIdentidadeExterna').html(totalGrid);
                    }
                   } else {
                       $('label#currentNumberTcsIdentidadeExterna').html(0);
                       $('label#totalNumberTcsIdentidadeExterna').html(0);
                   }
                }};
                var valueGrouBy = -1;
                var deletedIndex = -1;
                function verifyCanEditCol(column, clear, entity){
                    switch(column){
                        case 'IdentidadeExterna': { canEditing = clear || vm.enabledForEditing(); break;}
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
                            $('#CadastroUsuarioAutenticacao_dGridTcsIdentidadeExterna_LayoutBtn').igPopover('hide');
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
                                      showMultimidia(entity, e, table, key, vm.CadastroUsuarioAutenticacao());
                                 }
                             }
                             if (typeof vm.OnGridClientClick === 'function') {
                                 entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowKey);
                                 vm.OnGridClientClick('CadastroUsuarioAutenticacao_dGridTcsIdentidadeExterna', ui.colKey, entity);
                             }
                             if (vm.status() != 'Q') {
                                 var grid = $('#CadastroUsuarioAutenticacao_dGridTcsIdentidadeExterna');
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
                            { key: 'IdentidadeExterna', headerText: vm.getLayoutHeaderGrid('CadastroUsuarioAutenticacao_tbTcsIdentidadeExterna_IdentidadeExterna'), width: '400px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   }
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
                        
                                        var dataView = $('#CadastroUsuarioAutenticacao_dGridTcsIdentidadeExterna').data('igGrid').dataSource;
                                        if (dataView.settings.filtering.expressions.length <= 0)
                                            dataView._filteredData = [];
                        
                                        var col = ui.owner._dialogCurrentColumn;
                        
                                        var divDialog = $('#' + dgl[0].id).find('.ui-iggrid-filterdialogaddcondition').find('span')[0];
                        
                                        var scriptHtml = '<div id="' + divDinamica + '">';
                                        scriptHtml += '  <script>';
                                        scriptHtml += '    var newCol = "' + col + '";';
                                        scriptHtml += '    var newGrid = $("#CadastroUsuarioAutenticacao_dGridTcsIdentidadeExterna");';
                                        scriptHtml += '    var listFilter = [];';
                                        scriptHtml += '    function hideColumn(){';
                                        scriptHtml += '     if ($("#showHideColumn_CadastroUsuarioAutenticacao_dGridTcsIdentidadeExterna")["0"].innerHTML.indexOf("Ocultar") >= 0) {';
                                        scriptHtml += '        $("#showHideColumn_CadastroUsuarioAutenticacao_dGridTcsIdentidadeExterna")["0"].innerHTML = "Mostrar Coluna";';
                                        scriptHtml += '        newGrid.igGridHiding("hideColumn", newCol);';
                                        scriptHtml += '     }';
                                        scriptHtml += '     else{';
                                        scriptHtml += '        $("#showHideColumn_CadastroUsuarioAutenticacao_dGridTcsIdentidadeExterna")["0"].innerHTML = "Ocultar Coluna";';
                                        scriptHtml += '        newGrid.igGridHiding("showColumn", newCol);';
                                        scriptHtml += '     }';
                                        scriptHtml += '    }';
                                        scriptHtml += '    function updateHideButton(){';
                                        scriptHtml += '         var column = $.grep(newGrid.igGrid("option", "columns"), function (element, index) { return element.key == newCol });';
                                        scriptHtml += '         if (column.length > 0){';
                                        scriptHtml += '             $("#showHideColumn_CadastroUsuarioAutenticacao_dGridTcsIdentidadeExterna")["0"].innerHTML = column[0].hidden ? "Mostrar Coluna" : "Ocultar Coluna"';
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
                                        scriptHtml += '              <div id="comboFields_CadastroUsuarioAutenticacao_dGridTcsIdentidadeExterna"></div>';
                                        scriptHtml += '              <script>';
                                        scriptHtml += '                  var columns = newGrid.igGrid("option", "columns");';
                                        scriptHtml += '                  $("#comboFields_CadastroUsuarioAutenticacao_dGridTcsIdentidadeExterna").igCombo({ dataSource: columns, mode : "dropdown", valueKey: "key", textKey: "headerText", selectionChanging: function (evt, ui) {';
                                        scriptHtml += '                      newCol = ui.items["0"].data.key;';
                                        scriptHtml += '                      updateHideButton()';
                                        scriptHtml += '                  }});';
                                        scriptHtml += '                  $("#comboFields_CadastroUsuarioAutenticacao_dGridTcsIdentidadeExterna").igCombo("value", newCol);';
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
                                        scriptHtml += '              <i class="fa fa-eye-slash" aria-hidden="true" style="margin-right: 5px;"></i><a onclick="hideColumn()" style="cursor: pointer" id="showHideColumn_CadastroUsuarioAutenticacao_dGridTcsIdentidadeExterna">Ocultar Coluna</a>';
                                        scriptHtml += '          </div>';
                                        scriptHtml += '          <br>';
                                        scriptHtml += '      </div>';
                                        scriptHtml += '  </div>';
                        
                                        $(scriptHtml).insertBefore(divDialog);
                                   },
                             },
                                    { name: 'Selection', mode: 'row'
                                    }, 
                                    { name: 'Tooltips', columnSettings:[{ columnKey: "IdentidadeExterna", allowTooltips: true }] },
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
                                      columnSettings: [{ columnKey: "IdentidadeExterna" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('CadastroUsuarioAutenticacao_dGridTcsIdentidadeExterna', 'IdentidadeExterna', ui.oldValue, ui.value);}}, maxLength: 250 } }],
                                      rowDeleting: function (evt, ui) {
                                          deletedIndex = ui.element.context.rowIndex;
                                          var entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowID);
                                          if (entity) {
                                              vm.deleteEntity(entity);
                                          }
                                      },
                                      rowDeleted: function (evt, ui) {
                                          var grid = $('#CadastroUsuarioAutenticacao_dGridTcsIdentidadeExterna');
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
                                          var columns = $('#CadastroUsuarioAutenticacao_dGridTcsIdentidadeExterna').igGridUpdating('option', 'columnSettings');
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
                                                     vm.dataCombo.fillDataCombos(lookUpName, ui.columnKey, vm.currentDataItem().currentTcsIdentidadeExterna(), function (result) {
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
                            vm.OnDataGridCreated('CadastroUsuarioAutenticacao_dGridTcsIdentidadeExterna');
                        }
                        var selectionrowselectionchanged = null, selectedRowId = -1;
                        selectionrowselectionchanged = function (evt, ui) {
                            if ((ui.owner.grid.selectedRow() && typeof ui.owner.grid.selectedRow().id !== 'undefined') || (ui.owner.grid.selectedRows() && ui.owner.grid.selectedRows().length > 0)) { 
                                if (isNullOrEmpty(ui.owner.selectedRows())|| ui.selectedRows.length <= 1) {
                                    $(document).undelegate('#CadastroUsuarioAutenticacao_dGridTcsIdentidadeExterna', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                                    ui.owner.clearSelection();
                                    ui.owner.selectRow(ui.row.index);
                                    if (vm.status() === 'Q'){
                                        var gridCell = ui.owner.grid;
                                        grid.find('div.borderCell').remove();
                                        //$(gridCell.cellAt(-1, ui.owner._rowIndex)).append(" < div class='borderCell' style='z-index:100; border: 1px solid #849fd9 !important;'></div>");
                                    }
                                    selectedRowId = ui.row.id;
                                    $(document).delegate ('#CadastroUsuarioAutenticacao_dGridTcsIdentidadeExterna', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                                }
                                selectGridCurrentItem(vm.goToKey, 'RowDataId', ui, vm.currentDataItem().currentTcsIdentidadeExterna, getDataSource()); 
                             } 
                        };
                        $(document).delegate('#CadastroUsuarioAutenticacao_dGridTcsIdentidadeExterna', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                        $('#CadastroUsuarioAutenticacao_dGridTcsIdentidadeExterna > tbody tr').live('focus', function(evt) {
                            var grid = $('#CadastroUsuarioAutenticacao_dGridTcsIdentidadeExterna'), row = $(this).closest('tr'), id = parseInt(row.attr('data-id'), 10);
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
                    vm.addDataSource({ key: 'CadastroUsuarioAutenticacao_dGridTcsIdentidadeExterna', name: 'TcsIdentidadeExternaList', itemsSource: itemsSource });
                }
            
            , ChangedBrandCadastroUsuarioAutenticacao_dGridTcsUsuarioGpecon: function(vm, decimals, reset) {
                var i, format = '0.'.concat('0'.repeat(decimals)), grd =$('#CadastroUsuarioAutenticacao_dGridTcsUsuarioGpecon').data('igGrid'),
                    grdUpd = $('#CadastroUsuarioAutenticacao_dGridTcsUsuarioGpecon').data('igGridUpdating');
                if(isNull(grd) || isNull(grdUpd)) return;
                for (i = 0; i < grd.options.columns.length; i++) {
                }
                for (i = 0; i < grdUpd.options.columnSettings.length; i++) {
                }
                grd.dataBind();
            }
            , renderCadastroUsuarioAutenticacao_dGridTcsUsuarioGpecon: function(vm) {
                var getDataSource = function() {
                    var source = null;
                    try {
                        source = vm.currentDataItem().TcsUsuarioGpeconList;
                    }
                    catch (e) { }
                    return isNullOrEmpty(source) ? ko.observableArray([]) : source;
                };
                var dataSourceIsLoaded = function() {
                    var isLoaded = false;
                    try {
                        isLoaded = (vm.currentDataItem().TcsUsuarioGpeconIsLoaded === true || vm.currentDataItem().TcsUsuarioGpeconList().length > 0);
                    }
                    catch (e) {
                        isLoaded = true;
                    }
                    return isLoaded;
                }
                $('#CadastroUsuarioAutenticacao_dGridTcsUsuarioGpecon_headers').live('focus  keydown', function (evt) {
                    var keyCode = window.event ? evt.which : evt.keyCode;
                    if (keyCode === 9) {
                        var cols = $('#CadastroUsuarioAutenticacao_dGridTcsUsuarioGpecon').igGrid('option', 'columns');
                        var dataView = $('#CadastroUsuarioAutenticacao_dGridTcsUsuarioGpecon').data('igGrid').dataSource._dataView
                        if (dataView.length === 0) return;
                        var firstRow = dataView[0].RowDataId;
                        clear = vm.status() === 'C';
                        if (vm.status() === 'C')
                            $('#CadastroUsuarioAutenticacao_dGridTcsUsuarioGpecon').igGridUpdating('startEdit', firstRow, 0, true);
                        else {
                            var entity = findElementByKey(getDataSource(), 'RowDataId', firstRow);
                            var indexColumn = 0;
                            cols.some(function (entry) {
                                if (entry.key !== 'RowDataId' && !entry.hidden) {
                                    if (verifyCanEditCol(entry.key, clear, entity)) {
                                        $('#CadastroUsuarioAutenticacao_dGridTcsUsuarioGpecon').igGridUpdating('startEdit', firstRow, indexColumn, true);
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
                   if($('#CadastroUsuarioAutenticacao_dGridTcsUsuarioGpecon').data('igGrid') === undefined) return '';
                   var cols = $('#CadastroUsuarioAutenticacao_dGridTcsUsuarioGpecon').igGrid('option', 'columns');
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
                  if (!grid) grid = $('#CadastroUsuarioAutenticacao_dGridTcsUsuarioGpecon');
                  return ((!grid[0] || (!forceCreating && grid.parent().width() <= 0)) && !$('#dialogTcsUsuarioGpecon').is(':visible'));
                }
                var refreshData = true;
                var itemsSource = { isElementHided: isElementHided, getVisibleColumns: getVisibleColumns, containerId: 'CadastroUsuarioAutenticacao_dGridTcsUsuarioGpecon_container', dataBind: function (commitData, forceCreating) {
                   var grid = $('#CadastroUsuarioAutenticacao_dGridTcsUsuarioGpecon');
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
                       $('#CadastroUsuarioAutenticacao_dGridTcsUsuarioGpecon_groupbyarea').addClass('hide');
                   }
                   if (grid.igGridUpdating('isEditing')) {
                        grid.igGridUpdating('endEdit', true);
                   }
                   if (execFillDetais) {
                     grid.igGrid("option", "dataSource", []);
                     vm.currentDataItem().fillDetails(false, 'TcsUsuarioGpecon');
                     return;
                   }
                   grid.igGrid("option", "dataSource", unwrapObservableArray(getDataSource(), vm));
                   var rows = grid.igGrid('allRows');
                   if (rows.length > 0) {
                     var verticalContainer = grid.igGrid('scrollContainer');
                     var isSelected = false;
                     if (vm.currentDataItem().currentTcsUsuarioGpecon() != null)
                     {
                       for(var idx = 0; idx < rows.length; idx++)
                       {
                         if (rows[idx].dataset.id == getAbsoluteValue(vm.currentDataItem().currentTcsUsuarioGpecon().RowDataId))
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
                     if ($('#dialogTcsUsuarioGpecon').is(':visible')) {
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
                           $('label#currentNumberTcsUsuarioGpecon').html(current + ' - ' + totalCurrentPage);
                        }
                        else
                           $('label#currentNumberTcsUsuarioGpecon').html(1);
                        $('label#totalNumberTcsUsuarioGpecon').html(totalGrid);
                    }
                   } else {
                       $('label#currentNumberTcsUsuarioGpecon').html(0);
                       $('label#totalNumberTcsUsuarioGpecon').html(0);
                   }
                }};
                var valueGrouBy = -1;
                var deletedIndex = -1;
                function verifyCanEditCol(column, clear, entity){
                    switch(column){
                        case 'IdLinx': { canEditing = clear || (entity && entity.isAdded()); break;}
                        case 'NomeEmpresa': { canEditing = clear || (entity && entity.isAdded()); break;}
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
                            $('#CadastroUsuarioAutenticacao_dGridTcsUsuarioGpecon_LayoutBtn').igPopover('hide');
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
                                      showMultimidia(entity, e, table, key, vm.CadastroUsuarioAutenticacao());
                                 }
                             }
                             if (typeof vm.OnGridClientClick === 'function') {
                                 entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowKey);
                                 vm.OnGridClientClick('CadastroUsuarioAutenticacao_dGridTcsUsuarioGpecon', ui.colKey, entity);
                             }
                             if (vm.status() != 'Q') {
                                 var grid = $('#CadastroUsuarioAutenticacao_dGridTcsUsuarioGpecon');
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
                            { key: 'IdLinx', headerText: vm.getLayoutHeaderGrid('CadastroUsuarioAutenticacao_lUpTcsUsuarioGpecon_IdLinx'), width: '300px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'NomeEmpresa', headerText: vm.getLayoutHeaderGrid('CadastroUsuarioAutenticacao_lUpTcsUsuarioGpecon_NomeEmpresa'), width: '400px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   }
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
                        
                                        var dataView = $('#CadastroUsuarioAutenticacao_dGridTcsUsuarioGpecon').data('igGrid').dataSource;
                                        if (dataView.settings.filtering.expressions.length <= 0)
                                            dataView._filteredData = [];
                        
                                        var col = ui.owner._dialogCurrentColumn;
                        
                                        var divDialog = $('#' + dgl[0].id).find('.ui-iggrid-filterdialogaddcondition').find('span')[0];
                        
                                        var scriptHtml = '<div id="' + divDinamica + '">';
                                        scriptHtml += '  <script>';
                                        scriptHtml += '    var newCol = "' + col + '";';
                                        scriptHtml += '    var newGrid = $("#CadastroUsuarioAutenticacao_dGridTcsUsuarioGpecon");';
                                        scriptHtml += '    var listFilter = [];';
                                        scriptHtml += '    function hideColumn(){';
                                        scriptHtml += '     if ($("#showHideColumn_CadastroUsuarioAutenticacao_dGridTcsUsuarioGpecon")["0"].innerHTML.indexOf("Ocultar") >= 0) {';
                                        scriptHtml += '        $("#showHideColumn_CadastroUsuarioAutenticacao_dGridTcsUsuarioGpecon")["0"].innerHTML = "Mostrar Coluna";';
                                        scriptHtml += '        newGrid.igGridHiding("hideColumn", newCol);';
                                        scriptHtml += '     }';
                                        scriptHtml += '     else{';
                                        scriptHtml += '        $("#showHideColumn_CadastroUsuarioAutenticacao_dGridTcsUsuarioGpecon")["0"].innerHTML = "Ocultar Coluna";';
                                        scriptHtml += '        newGrid.igGridHiding("showColumn", newCol);';
                                        scriptHtml += '     }';
                                        scriptHtml += '    }';
                                        scriptHtml += '    function updateHideButton(){';
                                        scriptHtml += '         var column = $.grep(newGrid.igGrid("option", "columns"), function (element, index) { return element.key == newCol });';
                                        scriptHtml += '         if (column.length > 0){';
                                        scriptHtml += '             $("#showHideColumn_CadastroUsuarioAutenticacao_dGridTcsUsuarioGpecon")["0"].innerHTML = column[0].hidden ? "Mostrar Coluna" : "Ocultar Coluna"';
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
                                        scriptHtml += '              <div id="comboFields_CadastroUsuarioAutenticacao_dGridTcsUsuarioGpecon"></div>';
                                        scriptHtml += '              <script>';
                                        scriptHtml += '                  var columns = newGrid.igGrid("option", "columns");';
                                        scriptHtml += '                  $("#comboFields_CadastroUsuarioAutenticacao_dGridTcsUsuarioGpecon").igCombo({ dataSource: columns, mode : "dropdown", valueKey: "key", textKey: "headerText", selectionChanging: function (evt, ui) {';
                                        scriptHtml += '                      newCol = ui.items["0"].data.key;';
                                        scriptHtml += '                      updateHideButton()';
                                        scriptHtml += '                  }});';
                                        scriptHtml += '                  $("#comboFields_CadastroUsuarioAutenticacao_dGridTcsUsuarioGpecon").igCombo("value", newCol);';
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
                                        scriptHtml += '              <i class="fa fa-eye-slash" aria-hidden="true" style="margin-right: 5px;"></i><a onclick="hideColumn()" style="cursor: pointer" id="showHideColumn_CadastroUsuarioAutenticacao_dGridTcsUsuarioGpecon">Ocultar Coluna</a>';
                                        scriptHtml += '          </div>';
                                        scriptHtml += '          <br>';
                                        scriptHtml += '      </div>';
                                        scriptHtml += '  </div>';
                        
                                        $(scriptHtml).insertBefore(divDialog);
                                   },
                             },
                                    { name: 'Selection', mode: 'row'
                                    }, 
                                    { name: 'Tooltips', columnSettings:[{ columnKey: "IdLinx", allowTooltips: false },{ columnKey: "NomeEmpresa", allowTooltips: false }] },
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
                                      columnSettings: [{ columnKey: "IdLinx", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpTcsEmpresaAutenticacao", isNullable: false, custom: vm.custom, vm: vm, verifyCanEditCol: verifyCanEditCol, allowMultiSelectionInSearch:true, activateAutoComplete: false, autoCompleteMaxResults: 7, validateOnClearState:false, maxValue:2147483647, maxLength: 0, defaultValue: 0 } }, { columnKey: "NomeEmpresa", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpTcsEmpresaAutenticacao", isNullable: false, custom: vm.custom, vm: vm, verifyCanEditCol: verifyCanEditCol, allowMultiSelectionInSearch:true, activateAutoComplete: false, autoCompleteMaxResults: 7, validateOnClearState:false, maxValue:null, maxLength: 250, defaultValue: '' } }],
                                      rowDeleting: function (evt, ui) {
                                          deletedIndex = ui.element.context.rowIndex;
                                          var entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowID);
                                          if (entity) {
                                              vm.deleteEntity(entity);
                                          }
                                      },
                                      rowDeleted: function (evt, ui) {
                                          var grid = $('#CadastroUsuarioAutenticacao_dGridTcsUsuarioGpecon');
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
                                          var columns = $('#CadastroUsuarioAutenticacao_dGridTcsUsuarioGpecon').igGridUpdating('option', 'columnSettings');
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
                                                     vm.dataCombo.fillDataCombos(lookUpName, ui.columnKey, vm.currentDataItem().currentTcsUsuarioGpecon(), function (result) {
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
                            vm.OnDataGridCreated('CadastroUsuarioAutenticacao_dGridTcsUsuarioGpecon');
                        }
                        var selectionrowselectionchanged = null, selectedRowId = -1;
                        selectionrowselectionchanged = function (evt, ui) {
                            if ((ui.owner.grid.selectedRow() && typeof ui.owner.grid.selectedRow().id !== 'undefined') || (ui.owner.grid.selectedRows() && ui.owner.grid.selectedRows().length > 0)) { 
                                if (isNullOrEmpty(ui.owner.selectedRows())|| ui.selectedRows.length <= 1) {
                                    $(document).undelegate('#CadastroUsuarioAutenticacao_dGridTcsUsuarioGpecon', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                                    ui.owner.clearSelection();
                                    ui.owner.selectRow(ui.row.index);
                                    if (vm.status() === 'Q'){
                                        var gridCell = ui.owner.grid;
                                        grid.find('div.borderCell').remove();
                                        //$(gridCell.cellAt(-1, ui.owner._rowIndex)).append(" < div class='borderCell' style='z-index:100; border: 1px solid #849fd9 !important;'></div>");
                                    }
                                    selectedRowId = ui.row.id;
                                    $(document).delegate ('#CadastroUsuarioAutenticacao_dGridTcsUsuarioGpecon', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                                }
                                selectGridCurrentItem(vm.goToKey, 'RowDataId', ui, vm.currentDataItem().currentTcsUsuarioGpecon, getDataSource()); 
                             } 
                        };
                        $(document).delegate('#CadastroUsuarioAutenticacao_dGridTcsUsuarioGpecon', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                        $('#CadastroUsuarioAutenticacao_dGridTcsUsuarioGpecon > tbody tr').live('focus', function(evt) {
                            var grid = $('#CadastroUsuarioAutenticacao_dGridTcsUsuarioGpecon'), row = $(this).closest('tr'), id = parseInt(row.attr('data-id'), 10);
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
                    vm.addDataSource({ key: 'CadastroUsuarioAutenticacao_dGridTcsUsuarioGpecon', name: 'TcsUsuarioGpeconList', itemsSource: itemsSource });
                }
            
            
            , ChangedBrandscyCadastroUsuarioAutenticacao_dGrid: function(vm, decimals, reset) {
                var i, format = '0.'.concat('0'.repeat(decimals)), grd =$('#scyCadastroUsuarioAutenticacao_dGrid').data('igGrid'),
                    grdUpd = $('#scyCadastroUsuarioAutenticacao_dGrid').data('igGridUpdating');
                if(isNull(grd) || isNull(grdUpd)) return;
                for (i = 0; i < grd.options.columns.length; i++) {
                }
                for (i = 0; i < grdUpd.options.columnSettings.length; i++) {
                }
                grd.dataBind();
            }
            , renderscyCadastroUsuarioAutenticacao_dGrid: function(vm) {
                var getDataSource = function() {
                    var source = null;
                    try {
                        source = vm.dataView;
                    }
                    catch (e) { }
                    return isNullOrEmpty(source) ? ko.observableArray([]) : source;
                };
                $('#scyCadastroUsuarioAutenticacao_dGrid_headers').live('focus  keydown', function (evt) {
                    var keyCode = window.event ? evt.which : evt.keyCode;
                    if (keyCode === 9) {
                        var cols = $('#scyCadastroUsuarioAutenticacao_dGrid').igGrid('option', 'columns');
                        var dataView = $('#scyCadastroUsuarioAutenticacao_dGrid').data('igGrid').dataSource._dataView
                        if (dataView.length === 0) return;
                        var firstRow = dataView[0].RowDataId;
                        clear = vm.status() === 'C';
                        if (vm.status() === 'C')
                            $('#scyCadastroUsuarioAutenticacao_dGrid').igGridUpdating('startEdit', firstRow, 0, true);
                        else {
                            var entity = findElementByKey(getDataSource(), 'RowDataId', firstRow);
                            var indexColumn = 0;
                            cols.some(function (entry) {
                                if (entry.key !== 'RowDataId' && !entry.hidden) {
                                    if (verifyCanEditCol(entry.key, clear, entity)) {
                                        $('#scyCadastroUsuarioAutenticacao_dGrid').igGridUpdating('startEdit', firstRow, indexColumn, true);
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
                   if($('#scyCadastroUsuarioAutenticacao_dGrid').data('igGrid') === undefined) return '';
                   var cols = $('#scyCadastroUsuarioAutenticacao_dGrid').igGrid('option', 'columns');
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
                  if (!grid) grid = $('#scyCadastroUsuarioAutenticacao_dGrid');
                  return ((!grid[0] || (!forceCreating && grid.parent().width() <= 0)) && !$('#dialog').is(':visible'));
                }
                var refreshData = true;
                var itemsSource = { isElementHided: isElementHided, getVisibleColumns: getVisibleColumns, containerId: 'scyCadastroUsuarioAutenticacao_dGrid_container', dataBind: function (commitData, forceCreating) {
                   var grid = $('#scyCadastroUsuarioAutenticacao_dGrid');
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
                       $('#scyCadastroUsuarioAutenticacao_dGrid_groupbyarea').addClass('hide');
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
                        case 'NomeAutenticacao': { canEditing = clear; break;}
                        case 'NomeCurtoUsuario': { canEditing = clear; break;}
                        case 'Email': { canEditing = clear; break;}
                        case 'NomeEmpresa': { canEditing = clear; break;}
                        case 'AutenticacaoWindows': { canEditing = clear; break;}
                        case 'IndicaAcessoSuporte': { canEditing = clear; break;}
                        case 'VigenciaInicial': { canEditing = clear; break;}
                        case 'VigenciaFinal': { canEditing = clear; break;}
                        case 'DataExpiracaoSenha': { canEditing = clear; break;}
                        case 'DataCadastro': { canEditing = clear; break;}
                        case 'DataAlteracao': { canEditing = clear; break;}
                        case 'Inativo': { canEditing = clear; break;}
                        case 'LxPfjFisicaJuridica': { canEditing = clear; break;}
                        case 'CnpjCpf': { canEditing = clear; break;}
                        case 'InscrEstadualRg': { canEditing = clear; break;}
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
                            $('#scyCadastroUsuarioAutenticacao_dGrid_LayoutBtn').igPopover('hide');
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
                                      showMultimidia(entity, e, table, key, vm.CadastroUsuarioAutenticacao());
                                 }
                             }
                             if (typeof vm.OnGridClientClick === 'function') {
                                 entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowKey);
                                 vm.OnGridClientClick('scyCadastroUsuarioAutenticacao_dGrid', ui.colKey, entity);
                             }
                             if (vm.status() != 'Q') {
                                 var grid = $('#scyCadastroUsuarioAutenticacao_dGrid');
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
                            { key: 'NomeUsuario', headerText: vm.getLayoutDisplayName('CadastroUsuarioAutenticacao_tbNomeUsuario'), width: '400px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'NomeAutenticacao', headerText: vm.getLayoutDisplayName('CadastroUsuarioAutenticacao_tbNomeAutenticacao'), width: '400px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'NomeCurtoUsuario', headerText: vm.getLayoutDisplayName('CadastroUsuarioAutenticacao_tbNomeCurtoUsuario'), width: '400px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'Email', headerText: vm.getLayoutDisplayName('CadastroUsuarioAutenticacao_tbEmail'), width: '400px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'NomeEmpresa', headerText: vm.getLayoutDisplayName('CadastroUsuarioAutenticacao_lUpNomeEmpresa'), width: '421px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'AutenticacaoWindows', headerText: vm.getLayoutDisplayName('CadastroUsuarioAutenticacao_ckAutenticacaoWindows'), width: '400px', dataType: 'bool', columnCssClass: 'ellipsis', format: 'checkbox', hidden: false, unbound: false, group: null   },
                            { key: 'IndicaAcessoSuporte', headerText: vm.getLayoutDisplayName('CadastroUsuarioAutenticacao_ckIndicaAcessoSuporte'), width: '361px', dataType: 'bool', columnCssClass: 'ellipsis', format: 'checkbox', hidden: false, unbound: false, group: null   },
                            { key: 'VigenciaInicial', headerText: vm.getLayoutDisplayName('CadastroUsuarioAutenticacao_dtVigenciaInicial'), width: '244px', dataType: 'date', columnCssClass: 'ellipsis', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null   },
                            { key: 'VigenciaFinal', headerText: vm.getLayoutDisplayName('CadastroUsuarioAutenticacao_dtVigenciaFinal'), width: '218px', dataType: 'date', columnCssClass: 'ellipsis', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null   },
                            { key: 'DataExpiracaoSenha', headerText: vm.getLayoutDisplayName('CadastroUsuarioAutenticacao_dtDataExpiracaoSenha'), width: '231px', dataType: 'date', columnCssClass: 'ellipsis', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null   },
                            { key: 'DataCadastro', headerText: vm.getLayoutDisplayName('CadastroUsuarioAutenticacao_dtDataCadastro'), width: '140px', dataType: 'date', columnCssClass: 'ellipsis', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null   },
                            { key: 'DataAlteracao', headerText: vm.getLayoutDisplayName('CadastroUsuarioAutenticacao_dtDataAlteracao'), width: '153px', dataType: 'date', columnCssClass: 'ellipsis', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null   },
                            { key: 'Inativo', headerText: vm.getLayoutDisplayName('CadastroUsuarioAutenticacao_ckInativo'), width: '127px', dataType: 'bool', columnCssClass: 'ellipsis', format: 'checkbox', hidden: false, unbound: false, group: null   },
                            { key: 'LxPfjFisicaJuridica', headerText: vm.getLayoutDisplayName('CadastroUsuarioAutenticacao_cmbLxPfjFisicaJuridica'), width: '348px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null  , formatter: function (val, record) { return  vm.dataDomains.getName('LX_PFJ_FISICA_JURIDICA', val);} },
                            { key: 'CnpjCpf', headerText: vm.getLayoutDisplayName('CadastroUsuarioAutenticacao_mskCnpjCpf'), width: '200px', dataType: 'string', columnCssClass: 'ellipsis', hidden: false, formatter: function(val) { return (val == null ? '' : val.toString()).mask('###.###.###-##') } },
                            { key: 'InscrEstadualRg', headerText: vm.getLayoutDisplayName('CadastroUsuarioAutenticacao_tbInscrEstadualRg'), width: '296px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'LxTipoLogradouro', headerText: vm.getLayoutDisplayName('CadastroUsuarioAutenticacao_cmbLxTipoLogradouro'), width: '231px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null  , formatter: function (val, record) { return  vm.dataDomains.getName('LxTipoLogradouro', val);} },
                            { key: 'Logradouro', headerText: vm.getLayoutDisplayName('CadastroUsuarioAutenticacao_tbLogradouro'), width: '400px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'Numero', headerText: vm.getLayoutDisplayName('CadastroUsuarioAutenticacao_tbNumero'), width: '400px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'Complemento', headerText: vm.getLayoutDisplayName('CadastroUsuarioAutenticacao_tbComplemento'), width: '400px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'Cep', headerText: vm.getLayoutDisplayName('CadastroUsuarioAutenticacao_tbCEP'), width: '100px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'Bairro', headerText: vm.getLayoutDisplayName('CadastroUsuarioAutenticacao_tbBairro'), width: '400px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'Municipio', headerText: vm.getLayoutDisplayName('CadastroUsuarioAutenticacao_tbMunicipio'), width: '400px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'Uf', headerText: vm.getLayoutDisplayName('CadastroUsuarioAutenticacao_tbUf'), width: '70px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'ObsEndereco', headerText: vm.getLayoutDisplayName('CadastroUsuarioAutenticacao_tbObsEndereco'), width: '400px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'FoneFixo', headerText: vm.getLayoutDisplayName('CadastroUsuarioAutenticacao_tbFoneFixo'), width: '200px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'Ramal', headerText: vm.getLayoutDisplayName('CadastroUsuarioAutenticacao_tbRamal'), width: '101px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'FoneCelular', headerText: vm.getLayoutDisplayName('CadastroUsuarioAutenticacao_tbFoneCelular'), width: '200px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   }
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
                        
                                        var dataView = $('#scyCadastroUsuarioAutenticacao_dGrid').data('igGrid').dataSource;
                                        if (dataView.settings.filtering.expressions.length <= 0)
                                            dataView._filteredData = [];
                        
                                        var col = ui.owner._dialogCurrentColumn;
                        
                                        var divDialog = $('#' + dgl[0].id).find('.ui-iggrid-filterdialogaddcondition').find('span')[0];
                        
                                        var scriptHtml = '<div id="' + divDinamica + '">';
                                        scriptHtml += '  <script>';
                                        scriptHtml += '    var newCol = "' + col + '";';
                                        scriptHtml += '    var newGrid = $("#scyCadastroUsuarioAutenticacao_dGrid");';
                                        scriptHtml += '    var listFilter = [];';
                                        scriptHtml += '    function hideColumn(){';
                                        scriptHtml += '     if ($("#showHideColumn_scyCadastroUsuarioAutenticacao_dGrid")["0"].innerHTML.indexOf("Ocultar") >= 0) {';
                                        scriptHtml += '        $("#showHideColumn_scyCadastroUsuarioAutenticacao_dGrid")["0"].innerHTML = "Mostrar Coluna";';
                                        scriptHtml += '        newGrid.igGridHiding("hideColumn", newCol);';
                                        scriptHtml += '     }';
                                        scriptHtml += '     else{';
                                        scriptHtml += '        $("#showHideColumn_scyCadastroUsuarioAutenticacao_dGrid")["0"].innerHTML = "Ocultar Coluna";';
                                        scriptHtml += '        newGrid.igGridHiding("showColumn", newCol);';
                                        scriptHtml += '     }';
                                        scriptHtml += '    }';
                                        scriptHtml += '    function updateHideButton(){';
                                        scriptHtml += '         var column = $.grep(newGrid.igGrid("option", "columns"), function (element, index) { return element.key == newCol });';
                                        scriptHtml += '         if (column.length > 0){';
                                        scriptHtml += '             $("#showHideColumn_scyCadastroUsuarioAutenticacao_dGrid")["0"].innerHTML = column[0].hidden ? "Mostrar Coluna" : "Ocultar Coluna"';
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
                                        scriptHtml += '              <div id="comboFields_scyCadastroUsuarioAutenticacao_dGrid"></div>';
                                        scriptHtml += '              <script>';
                                        scriptHtml += '                  var columns = newGrid.igGrid("option", "columns");';
                                        scriptHtml += '                  $("#comboFields_scyCadastroUsuarioAutenticacao_dGrid").igCombo({ dataSource: columns, mode : "dropdown", valueKey: "key", textKey: "headerText", selectionChanging: function (evt, ui) {';
                                        scriptHtml += '                      newCol = ui.items["0"].data.key;';
                                        scriptHtml += '                      updateHideButton()';
                                        scriptHtml += '                  }});';
                                        scriptHtml += '                  $("#comboFields_scyCadastroUsuarioAutenticacao_dGrid").igCombo("value", newCol);';
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
                                        scriptHtml += '              <i class="fa fa-eye-slash" aria-hidden="true" style="margin-right: 5px;"></i><a onclick="hideColumn()" style="cursor: pointer" id="showHideColumn_scyCadastroUsuarioAutenticacao_dGrid">Ocultar Coluna</a>';
                                        scriptHtml += '          </div>';
                                        scriptHtml += '          <br>';
                                        scriptHtml += '      </div>';
                                        scriptHtml += '  </div>';
                        
                                        $(scriptHtml).insertBefore(divDialog);
                                   },
                             },
                                    { name: 'Selection', mode: 'row'
                                    }, 
                                    { name: 'Tooltips', columnSettings:[{ columnKey: "NomeUsuario", allowTooltips: true },{ columnKey: "NomeAutenticacao", allowTooltips: true },{ columnKey: "NomeCurtoUsuario", allowTooltips: true },{ columnKey: "Email", allowTooltips: true },{ columnKey: "NomeEmpresa", allowTooltips: false },{ columnKey: "AutenticacaoWindows", allowTooltips: true },{ columnKey: "IndicaAcessoSuporte", allowTooltips: true },{ columnKey: "VigenciaInicial", allowTooltips: true },{ columnKey: "VigenciaFinal", allowTooltips: true },{ columnKey: "DataExpiracaoSenha", allowTooltips: true },{ columnKey: "DataCadastro", allowTooltips: true },{ columnKey: "DataAlteracao", allowTooltips: true },{ columnKey: "Inativo", allowTooltips: true },{ columnKey: "LxPfjFisicaJuridica", allowTooltips: true },{ columnKey: "CnpjCpf", allowTooltips: true },{ columnKey: "InscrEstadualRg", allowTooltips: true },{ columnKey: "LxTipoLogradouro", allowTooltips: true },{ columnKey: "Logradouro", allowTooltips: true },{ columnKey: "Numero", allowTooltips: true },{ columnKey: "Complemento", allowTooltips: true },{ columnKey: "Cep", allowTooltips: true },{ columnKey: "Bairro", allowTooltips: true },{ columnKey: "Municipio", allowTooltips: true },{ columnKey: "Uf", allowTooltips: true },{ columnKey: "ObsEndereco", allowTooltips: true },{ columnKey: "FoneFixo", allowTooltips: true },{ columnKey: "Ramal", allowTooltips: true },{ columnKey: "FoneCelular", allowTooltips: true }] },
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
                                      columnSettings: [{ columnKey: "NomeEmpresa", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpTcsUsuarioEmpresaAutenticacao", isNullable: false, custom: vm.custom, vm: vm, verifyCanEditCol: verifyCanEditCol, allowMultiSelectionInSearch:true, activateAutoComplete: false, autoCompleteMaxResults: 7, validateOnClearState:false, maxValue:null, maxLength: 250, defaultValue: '' } }, { columnKey: 'VigenciaInicial', editorType: 'datepicker', editorOptions: {valueChanged: function(evt, ui){if (typeof vm.OnPropertyChangeDataGrid === 'function') {vm.OnPropertyChangeDataGrid('scyCadastroUsuarioAutenticacao_dGrid', 'VigenciaInicial', ui.oldValue, ui.value);}}, minValue: new Date(1900, 0, 1), datepickerOptions: { changeMonth: true, changeYear: true }  } }, { columnKey: 'VigenciaFinal', editorType: 'datepicker', editorOptions: {valueChanged: function(evt, ui){if (typeof vm.OnPropertyChangeDataGrid === 'function') {vm.OnPropertyChangeDataGrid('scyCadastroUsuarioAutenticacao_dGrid', 'VigenciaFinal', ui.oldValue, ui.value);}}, minValue: new Date(1900, 0, 1), datepickerOptions: { changeMonth: true, changeYear: true }  } }, { columnKey: 'DataExpiracaoSenha', editorType: 'datepicker', editorOptions: {valueChanged: function(evt, ui){if (typeof vm.OnPropertyChangeDataGrid === 'function') {vm.OnPropertyChangeDataGrid('scyCadastroUsuarioAutenticacao_dGrid', 'DataExpiracaoSenha', ui.oldValue, ui.value);}}, minValue: new Date(1900, 0, 1), datepickerOptions: { changeMonth: true, changeYear: true }  } }, { columnKey: 'DataCadastro', editorType: 'datepicker', editorOptions: {valueChanged: function(evt, ui){if (typeof vm.OnPropertyChangeDataGrid === 'function') {vm.OnPropertyChangeDataGrid('scyCadastroUsuarioAutenticacao_dGrid', 'DataCadastro', ui.oldValue, ui.value);}}, minValue: new Date(1900, 0, 1), datepickerOptions: { changeMonth: true, changeYear: true }  } }, { columnKey: 'DataAlteracao', editorType: 'datepicker', editorOptions: {valueChanged: function(evt, ui){if (typeof vm.OnPropertyChangeDataGrid === 'function') {vm.OnPropertyChangeDataGrid('scyCadastroUsuarioAutenticacao_dGrid', 'DataAlteracao', ui.oldValue, ui.value);}}, minValue: new Date(1900, 0, 1), datepickerOptions: { changeMonth: true, changeYear: true }  } }, { columnKey: 'LxPfjFisicaJuridica', editorType: 'combo', editorOptions: {   selectionChanged: function (evt, ui) {   var val = null;   if (ui.items != null && ui.items.length > 0) { val = ui.items[0].data['id']; }  updateEntity('LxPfjFisicaJuridica', val, false); },  mode: 'dropdown', dropDownOnFocus: true,  dataSource: vm.dataDomains.getItems('LX_PFJ_FISICA_JURIDICA', ''),  textKey: 'name', valueKey: 'id', enableClearButton: true }}, { columnKey: 'LxTipoLogradouro', editorType: 'combo', editorOptions: {   selectionChanged: function (evt, ui) {   var val = null;   if (ui.items != null && ui.items.length > 0) { val = ui.items[0].data['id']; }  updateEntity('LxTipoLogradouro', val, false); },  mode: 'dropdown', dropDownOnFocus: true,  dataSource: vm.dataDomains.getItems('LxTipoLogradouro', ''),  textKey: 'name', valueKey: 'id', enableClearButton: true }}, { columnKey: "NomeUsuario" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('scyCadastroUsuarioAutenticacao_dGrid', 'NomeUsuario', ui.oldValue, ui.value);}}, maxLength: 250 } }, { columnKey: "NomeAutenticacao" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('scyCadastroUsuarioAutenticacao_dGrid', 'NomeAutenticacao', ui.oldValue, ui.value);}}, maxLength: 250 } }, { columnKey: "NomeCurtoUsuario" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('scyCadastroUsuarioAutenticacao_dGrid', 'NomeCurtoUsuario', ui.oldValue, ui.value);}}, maxLength: 60 } }, { columnKey: "Email" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('scyCadastroUsuarioAutenticacao_dGrid', 'Email', ui.oldValue, ui.value);}}, maxLength: 250 } }, { columnKey: "CnpjCpf", editorOptions: { type: "mask", inputMask: "###.###.###-##", dataMode: 0 } }, { columnKey: "InscrEstadualRg" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('scyCadastroUsuarioAutenticacao_dGrid', 'InscrEstadualRg', ui.oldValue, ui.value);}}, maxLength: 20 } }, { columnKey: "Logradouro" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('scyCadastroUsuarioAutenticacao_dGrid', 'Logradouro', ui.oldValue, ui.value);}}, maxLength: 60 } }, { columnKey: "Numero" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('scyCadastroUsuarioAutenticacao_dGrid', 'Numero', ui.oldValue, ui.value);}}, maxLength: 60 } }, { columnKey: "Complemento" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('scyCadastroUsuarioAutenticacao_dGrid', 'Complemento', ui.oldValue, ui.value);}}, maxLength: 60 } }, { columnKey: "Cep" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('scyCadastroUsuarioAutenticacao_dGrid', 'Cep', ui.oldValue, ui.value);}}, maxLength: 10 } }, { columnKey: "Bairro" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('scyCadastroUsuarioAutenticacao_dGrid', 'Bairro', ui.oldValue, ui.value);}}, maxLength: 60 } }, { columnKey: "Municipio" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('scyCadastroUsuarioAutenticacao_dGrid', 'Municipio', ui.oldValue, ui.value);}}, maxLength: 60 } }, { columnKey: "Uf" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('scyCadastroUsuarioAutenticacao_dGrid', 'Uf', ui.oldValue, ui.value);}}, maxLength: 4 } }, { columnKey: "ObsEndereco" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('scyCadastroUsuarioAutenticacao_dGrid', 'ObsEndereco', ui.oldValue, ui.value);}}, maxLength: 250 } }, { columnKey: "FoneFixo" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('scyCadastroUsuarioAutenticacao_dGrid', 'FoneFixo', ui.oldValue, ui.value);}}, maxLength: 20 } }, { columnKey: "Ramal" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('scyCadastroUsuarioAutenticacao_dGrid', 'Ramal', ui.oldValue, ui.value);}}, maxLength: 6 } }, { columnKey: "FoneCelular" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('scyCadastroUsuarioAutenticacao_dGrid', 'FoneCelular', ui.oldValue, ui.value);}}, maxLength: 20 } }],
                                      rowDeleting: function (evt, ui) {
                                          deletedIndex = ui.element.context.rowIndex;
                                          var entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowID);
                                          if (entity) {
                                              vm.deleteEntity(entity);
                                          }
                                      },
                                      rowDeleted: function (evt, ui) {
                                          var grid = $('#scyCadastroUsuarioAutenticacao_dGrid');
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
                                          var columns = $('#scyCadastroUsuarioAutenticacao_dGrid').igGridUpdating('option', 'columnSettings');
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
                            vm.OnDataGridCreated('scyCadastroUsuarioAutenticacao_dGrid');
                        }
                        var selectionrowselectionchanged = null, selectedRowId = -1;
                        selectionrowselectionchanged = function (evt, ui) {
                            if ((ui.owner.grid.selectedRow() && typeof ui.owner.grid.selectedRow().id !== 'undefined') || (ui.owner.grid.selectedRows() && ui.owner.grid.selectedRows().length > 0)) { 
                                if (isNullOrEmpty(ui.owner.selectedRows())|| ui.selectedRows.length <= 1) {
                                    $(document).undelegate('#scyCadastroUsuarioAutenticacao_dGrid', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                                    ui.owner.clearSelection();
                                    ui.owner.selectRow(ui.row.index);
                                    if (vm.status() === 'Q'){
                                        var gridCell = ui.owner.grid;
                                        grid.find('div.borderCell').remove();
                                        //$(gridCell.cellAt(-1, ui.owner._rowIndex)).append(" < div class='borderCell' style='z-index:100; border: 1px solid #849fd9 !important;'></div>");
                                    }
                                    selectedRowId = ui.row.id;
                                    $(document).delegate ('#scyCadastroUsuarioAutenticacao_dGrid', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                                }
                                selectGridCurrentItem(vm.goToKey, 'RowDataId', ui); 
                             } 
                        };
                        $(document).delegate('#scyCadastroUsuarioAutenticacao_dGrid', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                        $('#scyCadastroUsuarioAutenticacao_dGrid > tbody tr').live('focus', function(evt) {
                            var grid = $('#scyCadastroUsuarioAutenticacao_dGrid'), row = $(this).closest('tr'), id = parseInt(row.attr('data-id'), 10);
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
                            if (vm.CadastroUsuarioAutenticacao().status() === 'Q') vm.CadastroUsuarioAutenticacao().dataToolbar.viewInfo();
                        });
                    }
                    vm.addDataSource({ key: 'scyCadastroUsuarioAutenticacao_dGrid', name: 'dataView', itemsSource: itemsSource });
                }
            
        };
        
        return complement;
    }
    
    return complementCtor;
});
