define(['managers/__auth', 'managers/user'], function (managerAuth, managerUser) {
    var complementCtor = function() {
        var complement = {
            isAutomatic: true
            , ChangedBrandExemplos_GRIDs_com_macro_dGridvendas: function(vm, decimals, reset) {
                var i, format = '0.'.concat('0'.repeat(decimals)), grd =$('#Exemplos_GRIDs_com_macro_dGridvendas').data('igGrid'),
                    grdUpd = $('#Exemplos_GRIDs_com_macro_dGridvendas').data('igGridUpdating');
                if(isNull(grd) || isNull(grdUpd)) return;
                for (i = 0; i < grd.options.columns.length; i++) {
                }
                for (i = 0; i < grdUpd.options.columnSettings.length; i++) {
                }
                grd.dataBind();
            }
            , renderExemplos_GRIDs_com_macro_dGridvendas: function(vm) {
                if (!vm.hasMainTopDataGrid()) vm.hasMainTopDataGrid(true);
                var getDataSource = function() {
                    var source = null;
                    try {
                        source = vm.dataView;
                    }
                    catch (e) { }
                    return isNullOrEmpty(source) ? ko.observableArray([]) : source;
                };
                $('#Exemplos_GRIDs_com_macro_dGridvendas_headers').live('focus  keydown', function (evt) {
                    var keyCode = window.event ? evt.which : evt.keyCode;
                    if (keyCode === 9) {
                        var cols = $('#Exemplos_GRIDs_com_macro_dGridvendas').igGrid('option', 'columns');
                        var dataView = $('#Exemplos_GRIDs_com_macro_dGridvendas').data('igGrid').dataSource._dataView
                        if (dataView.length === 0) return;
                        var firstRow = dataView[0].RowDataId;
                        clear = vm.status() === 'C';
                        if (vm.status() === 'C')
                            $('#Exemplos_GRIDs_com_macro_dGridvendas').igGridUpdating('startEdit', firstRow, 0, true);
                        else {
                            var entity = findElementByKey(getDataSource(), 'RowDataId', firstRow);
                            var indexColumn = 0;
                            cols.some(function (entry) {
                                if (entry.key !== 'RowDataId' && !entry.hidden) {
                                    if (verifyCanEditCol(entry.key, clear, entity)) {
                                        $('#Exemplos_GRIDs_com_macro_dGridvendas').igGridUpdating('startEdit', firstRow, indexColumn, true);
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
                   if($('#Exemplos_GRIDs_com_macro_dGridvendas').data('igGrid') === undefined) return '';
                   var cols = $('#Exemplos_GRIDs_com_macro_dGridvendas').igGrid('option', 'columns');
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
                  if (!grid) grid = $('#Exemplos_GRIDs_com_macro_dGridvendas');
                  return ((!grid[0] || (!forceCreating && grid.parent().width() <= 0)) && !$('#dialogVenda').is(':visible'));
                }
                var refreshData = true;
                var itemsSource = { isElementHided: isElementHided, getVisibleColumns: getVisibleColumns, containerId: 'Exemplos_GRIDs_com_macro_dGridvendas_container', dataBind: function (commitData, forceCreating) {
                   var grid = $('#Exemplos_GRIDs_com_macro_dGridvendas');
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
                       $('#Exemplos_GRIDs_com_macro_dGridvendas_groupbyarea').addClass('hide');
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
                     if ($('#dialogVenda').is(':visible')) {
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
                           $('label#currentNumberVenda').html(current + ' - ' + totalCurrentPage);
                        }
                        else
                           $('label#currentNumberVenda').html(1);
                        $('label#totalNumberVenda').html(totalGrid);
                    }
                   } else {
                       $('label#currentNumberVenda').html(0);
                       $('label#totalNumberVenda').html(0);
                   }
                }};
                var valueGrouBy = -1;
                var deletedIndex = -1;
                function verifyCanEditCol(column, clear, entity){
                    switch(column){
                        case 'IdCliente': { canEditing = clear || vm.enabledForEditing(); break;}
                        case 'IdVenda': { canEditing = clear || (entity && entity.isAdded()); break;}
                        case 'StringVenda': { canEditing = clear || vm.enabledForEditing(); break;}
                        case 'ComboboxVenda': { canEditing = clear || vm.enabledForEditing(); break;}
                        case 'IntVenda': { canEditing = clear || vm.enabledForEditing(); break;}
                        case 'SmallIntVenda': { canEditing = clear || vm.enabledForEditing(); break;}
                        case 'DatetimeVenda': { canEditing = clear || vm.enabledForEditing(); break;}
                        case 'DecimalVenda': { canEditing = clear || vm.enabledForEditing(); break;}
                        case 'StringLoja': { canEditing = clear || vm.enabledForEditing(); break;}
                        case 'StringVendedor': { canEditing = clear || vm.enabledForEditing(); break;}
                        case '': { canEditing = true; break;}
                        case 'IdCidade': { canEditing = clear; break;}
                        case 'NomeCidade': { canEditing = clear; break;}
                    }
                    return canEditing;
                };
                function makeGauge(val, record, field, solid, sufix) {
                    var row = 0, value = 0;
                    if (record.RowDataId > 0) {
                        row = record.RowDataId;
                        value = record[field];
                        if (solid) {
                             var descValue = record[field + (isNullOrEmpty(sufix) ? "" : sufix)];
                             return "<div id='c" + row + field + sufix + "' style='color:black;text-align:" + (isNullOrEmpty(sufix) ? "right" : "center") + ";background-color:" + vm.getKpiColor(eval(eval("dadosGauge" + field).ranges), value) + ";'><strong" + (isNullOrEmpty(sufix) ? " style='margin-right: 5px;'" : "") + ">" + descValue + "</strong></div>";
                        }
                        else
                             return "<div id='g" + row + field + "' class='gauge' style='width:400px;height:20px;'></div> <script id='scriptg" + row + field + "'>$('#g" + row + field + "').kendoLinearGauge( {gaugeArea: {background: 'transparent', width:230}, pointer: { value: " + value + ", color: '#8B8386', shape: 'arrow' }, scale: { vertical: false ,line:{visible: false}, labels: {visible: false}, min: " + eval("dadosGauge" + field).min + ", max: " + eval("dadosGauge" + field).max + ", ranges: " + eval("dadosGauge" + field).ranges + " } });</script>"
                    }
                    return '';
                }
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
                            $('#Exemplos_GRIDs_com_macro_dGridvendas_LayoutBtn').igPopover('hide');
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
                        cellClick: function(evt, ui) {
                             if (ui.cellElement && ui.cellElement.childNodes[0] && ui.cellElement.childNodes[0].childNodes[1]) {
                                 var entity = null, e = ui.cellElement.childNodes[0].childNodes[1];
                                 if (e && e.tagName === 'IMG' && vm.status() !== 'C')
                                 {
                                      entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowKey);
                                      var key = e.attributes['key'].value;
                                      var table = e.attributes['tableName'].value;
                                      showMultimidia(entity, e, table, key, vm.Exemplos_GRIDs_com_macro());
                                 }
                             }
                             if (typeof vm.OnGridClientClick === 'function') {
                                 entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowKey);
                                 vm.OnGridClientClick('Exemplos_GRIDs_com_macro_dGridvendas', ui.colKey, entity);
                             }
                             if (vm.status() != 'Q') {
                                 var grid = $('#Exemplos_GRIDs_com_macro_dGridvendas');
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
                            if ($('.gauge').length) {
                                var x = document.getElementsByClassName('gauge');
                                for (var i = 0; i < x.length; i++)
                                    eval(document.getElementById('script' + x[i].id).innerHTML);
                            }
                        },
                        columns: [
                            { key: 'RowDataId', headerText: 'RowDataId', width: '50px', dataType: 'number', hidden: true },
                            { key: 'IdCliente', headerText: vm.getLayoutHeaderGrid('IdCliente'), width: '250px', dataType: 'number', columnCssClass: 'ellipsis', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                            { key: 'IdVenda', headerText: vm.getLayoutHeaderGrid('IdVenda'), width: '250px', dataType: 'number', columnCssClass: 'ellipsis', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                            { key: 'StringVenda', headerText: vm.getLayoutHeaderGrid('StringVenda'), width: '400px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'ComboboxVenda', headerText: vm.getLayoutHeaderGrid('ComboboxVenda'), width: '218px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null  , formatter: function (val, record) { return  vm.dataDomains.getName('LX_VENDA', val);} },
                            { key: 'IntVenda', headerText: vm.getLayoutHeaderGrid('IntVenda'), width: '250px', dataType: 'number', columnCssClass: 'ellipsis', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                            { key: 'SmallIntVenda', headerText: vm.getLayoutHeaderGrid('SmallIntVenda'), width: '250px', dataType: 'number', columnCssClass: 'ellipsis', format: 'int', hidden: false, unbound: false, group: null   },
                            { key: 'DatetimeVenda', headerText: vm.getLayoutHeaderGrid('DatetimeVenda'), width: '218px', dataType: 'date', columnCssClass: 'ellipsis', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null   },
                            { key: 'DecimalVenda', headerText: vm.getLayoutHeaderGrid('DecimalVenda'), width: '205px', dataType: 'number', columnCssClass: 'ellipsis', format: '0.00', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                            { key: 'StringLoja', headerText: vm.getLayoutHeaderGrid('StringLoja'), width: '400px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'StringVendedor', headerText: vm.getLayoutHeaderGrid('StringVendedor'), width: '400px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: '', headerText: vm.getLayoutHeaderGrid(''), width: '231px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: '', headerText: vm.getLayoutHeaderGrid(''), width: '374px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'IdCidade', headerText: vm.getLayoutHeaderGrid('IdCidade'), width: '271px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'NomeCidade', headerText: vm.getLayoutHeaderGrid('NomeCidade'), width: '400px', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   }
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
                        
                                        var dataView = $('#Exemplos_GRIDs_com_macro_dGridvendas').data('igGrid').dataSource;
                                        if (dataView.settings.filtering.expressions.length <= 0)
                                            dataView._filteredData = [];
                        
                                        var listGrid = '';
                                        var col = ui.owner._dialogCurrentColumn;
                        
                                        var reloadList = function (col) {
                                             var grid = $('#Exemplos_GRIDs_com_macro_dGridvendas');
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
                                        scriptHtml += '    var newGrid = $("#Exemplos_GRIDs_com_macro_dGridvendas");';
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
                                        scriptHtml += '            $("#Exemplos_GRIDs_com_macro_dGridvendas_container_dialog_list").html(newList)';
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
                                    { name: 'Tooltips', columnSettings:[{ columnKey: "IdCliente", allowTooltips: true },{ columnKey: "IdVenda", allowTooltips: true },{ columnKey: "StringVenda", allowTooltips: true },{ columnKey: "ComboboxVenda", allowTooltips: true },{ columnKey: "IntVenda", allowTooltips: true },{ columnKey: "SmallIntVenda", allowTooltips: true },{ columnKey: "DatetimeVenda", allowTooltips: true },{ columnKey: "DecimalVenda", allowTooltips: true },{ columnKey: "StringLoja", allowTooltips: false },{ columnKey: "StringVendedor", allowTooltips: false },{ columnKey: "", allowTooltips: true },{ columnKey: "", allowTooltips: true },{ columnKey: "IdCidade", allowTooltips: false },{ columnKey: "NomeCidade", allowTooltips: false }] },
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
                                      columnSettings: [{ columnKey: "StringLoja", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpLoja", isNullable: true, custom: vm.custom, vm: vm, verifyCanEditCol: verifyCanEditCol, allowMultiSelectionInSearch:true, activateAutoComplete: true, autoCompleteMaxResults: 4, validateOnClearState:false, maxValue:null, maxLength: 50, defaultValue: '' } }, { columnKey: "StringVendedor", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpVendedor", isNullable: true, custom: vm.custom, vm: vm, verifyCanEditCol: verifyCanEditCol, allowMultiSelectionInSearch:true, activateAutoComplete: false, autoCompleteMaxResults: 7, validateOnClearState:false, maxValue:null, maxLength: 50, defaultValue: '' } }, { columnKey: "IdCidade", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpLoja", isNullable: true, custom: vm.custom, vm: vm, verifyCanEditCol: verifyCanEditCol, allowMultiSelectionInSearch:true, activateAutoComplete: false, autoCompleteMaxResults: 7, validateOnClearState:false, maxValue:2147483647, maxLength: 10, defaultValue: null } }, { columnKey: "NomeCidade", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpLoja", isNullable: true, custom: vm.custom, vm: vm, verifyCanEditCol: verifyCanEditCol, allowMultiSelectionInSearch:true, activateAutoComplete: true, autoCompleteMaxResults: 4, validateOnClearState:false, maxValue:null, maxLength: 50, defaultValue: '' } }, { columnKey: 'DatetimeVenda', editorType: 'datepicker', editorOptions: {valueChanged: function(evt, ui){if (typeof vm.OnPropertyChangeDataGrid === 'function') {vm.OnPropertyChangeDataGrid('Exemplos_GRIDs_com_macro_dGridvendas', 'DatetimeVenda', ui.oldValue, ui.value);}}, minValue: new Date(1900, 0, 1), datepickerOptions: { changeMonth: true, changeYear: true }  } }, { columnKey: 'ComboboxVenda', editorType: 'combo', editorOptions: {   selectionChanged: function (evt, ui) {   var val = null;   if (ui.items != null && ui.items.length > 0) { val = ui.items[0].data['id']; }  updateEntity('ComboboxVenda', val, false); },  mode: 'dropdown', dropDownOnFocus: true,  dataSource: vm.dataDomains.getItems('LX_VENDA', ''),  textKey: 'name', valueKey: 'id', enableClearButton: false }}, { columnKey: "IdCliente" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('Exemplos_GRIDs_com_macro_dGridvendas', 'IdCliente', ui.oldValue, ui.value);}},  maxLength: 10, maxValue: null, minValue: 0, dataMode: 'int' } }, { columnKey: "IdVenda" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('Exemplos_GRIDs_com_macro_dGridvendas', 'IdVenda', ui.oldValue, ui.value);}},  maxLength: 10, maxValue: null, dataMode: 'int' } }, { columnKey: "StringVenda" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('Exemplos_GRIDs_com_macro_dGridvendas', 'StringVenda', ui.oldValue, ui.value);}}, maxLength: 50 } }, { columnKey: "IntVenda" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('Exemplos_GRIDs_com_macro_dGridvendas', 'IntVenda', ui.oldValue, ui.value);}},  maxLength: 10, maxValue: null, minValue: 0, dataMode: 'int' } }, { columnKey: "SmallIntVenda" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('Exemplos_GRIDs_com_macro_dGridvendas', 'SmallIntVenda', ui.oldValue, ui.value);}}, maxLength: 5 } }, { columnKey: "DecimalVenda" , editorType: 'numeric', editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('Exemplos_GRIDs_com_macro_dGridvendas', 'DecimalVenda', ui.oldValue, ui.value);}},  maxLength: 14, maxValue: 99999999999.99, minValue: 0, dataMode: 'decimal', minDecimals: 2, maxDecimals: 2 } }, { columnKey: "" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('Exemplos_GRIDs_com_macro_dGridvendas', '', ui.oldValue, ui.value);}}, maxLength: 0 } }, { columnKey: "" , editorOptions: {valueChanged: function(evt, ui){if(typeof vm.OnPropertyChangeDataGrid === 'function'){vm.OnPropertyChangeDataGrid('Exemplos_GRIDs_com_macro_dGridvendas', '', ui.oldValue, ui.value);}}, maxLength: 0 } }],
                                      rowDeleting: function (evt, ui) {
                                          deletedIndex = ui.element.context.rowIndex;
                                          var entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowID);
                                          if (entity) {
                                              vm.deleteEntity(entity);
                                          }
                                      },
                                      rowDeleted: function (evt, ui) {
                                          var grid = $('#Exemplos_GRIDs_com_macro_dGridvendas');
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
                                          var columns = $('#Exemplos_GRIDs_com_macro_dGridvendas').igGridUpdating('option', 'columnSettings');
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
                            vm.OnDataGridCreated('Exemplos_GRIDs_com_macro_dGridvendas');
                        }
                        var selectionrowselectionchanged = null, selectedRowId = -1;
                        selectionrowselectionchanged = function (evt, ui) {
                            if ((ui.owner.grid.selectedRow() && typeof ui.owner.grid.selectedRow().id !== 'undefined') || (ui.owner.grid.selectedRows() && ui.owner.grid.selectedRows().length > 0)) { 
                                if (isNullOrEmpty(ui.owner.selectedRows())|| ui.selectedRows.length <= 1) {
                                    $(document).undelegate('#Exemplos_GRIDs_com_macro_dGridvendas', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                                    ui.owner.clearSelection();
                                    ui.owner.selectRow(ui.row.index);
                                    if (vm.status() === 'Q'){
                                        var gridCell = ui.owner.grid;
                                        grid.find('div.borderCell').remove();
                                        //$(gridCell.cellAt(-1, ui.owner._rowIndex)).append(" < div class='borderCell' style='z-index:100; border: 1px solid #849fd9 !important;'></div>");
                                    }
                                    selectedRowId = ui.row.id;
                                    $(document).delegate ('#Exemplos_GRIDs_com_macro_dGridvendas', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                                }
                                selectGridCurrentItem(vm.goToKey, 'RowDataId', ui); 
                             } 
                        };
                        $(document).delegate('#Exemplos_GRIDs_com_macro_dGridvendas', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                        $('#Exemplos_GRIDs_com_macro_dGridvendas > tbody tr').live('focus', function(evt) {
                            var grid = $('#Exemplos_GRIDs_com_macro_dGridvendas'), row = $(this).closest('tr'), id = parseInt(row.attr('data-id'), 10);
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
                    vm.addDataSource({ key: 'Exemplos_GRIDs_com_macro_dGridvendas', name: 'dataView', itemsSource: itemsSource });
                }
            
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
                var pageProp = 'Page0';
                complement.selectedCollection[pageProp] = complement.selectedCurrentItems();
                complement.currentPage = complement.vm.dataToolbar.currentPage();
            }
            , selectedCurrentItems: function (firstIfNoItem, isSavingData) {
                  var grid = $('#Exemplos_GRIDs_com_macro_dGridVendaItens');
                  var selectedItems = [];
                  var ds = grid.data().igGrid.dataSource.dataView();
                  var rows = grid.igGridSelection("selectedRows");
                  if (rows && rows.length == 0 && firstIfNoItem) {
                      var dataList = this.vm.currentDataItem().VendaItemList;
                      var entity = (isSavingData ? findElementByKey(dataList, 'RowDataId', ds[0].RowDataId) : ds[0]);
                      if (entity) selectedItems.push(entity);
                  }
                  else if (rows && rows.length > 0) {
                      var dataList = this.vm.currentDataItem().VendaItemList;
                      $.each(rows, function (index, value) {
                          var entity = (isSavingData ? findElementByKey(dataList, 'RowDataId', ds[value.index].RowDataId) : ds[value.index]);
                          if (entity) selectedItems.push(entity);
                      });
                  }
                  return selectedItems;
            }
            , clearSelectedItems: function () {
                  var grid = $('#Exemplos_GRIDs_com_macro_dGridVendaItens');
                  grid.igGridSelection('clearSelection');
            }
            , renderExemplos_GRIDs_com_macro_dGridVendaItens: function(vm) {
                var self = this;
                self.vm = vm;
                var source = null;
                var schema = [{ name: 'RowDataId', type: 'number' }, { name: 'IdVenda', type: 'number' }, { name: 'ComboboxVendaItemName', type: 'string' }, { name: 'DatetimeVendaItem', type: 'date' }, { name: 'SmallIntVendaItem', type: 'number' }, { name: 'IntVendaItem', type: 'number' }, { name: 'BitVendaItem', type: 'bool' }, { name: 'BigIntVendaItem', type: 'number' }, { name: 'DecimalVendaItem', type: 'number' }, { name: 'IdVendaItem', type: 'number' }, { name: 'StringVendaItem', type: 'string' }];
                var getDataSource = function() {
                    try {
                        source = new $.ig.JSONDataSource({
                            dataSource: unwrapObservableArray(vm.currentDataItem().VendaItemList, vm),
                            schema: { fields: schema },
                            filtering: { type: 'local'}
                        }).dataBind();
                    }
                    catch (e) { }
                    return isNullOrEmpty(source) ? ko.observableArray([]) : source;
                };
                var dataSourceIsLoaded = function() {
                    var isLoaded = false;
                    try {
                        isLoaded = (vm.currentDataItem().VendaItemIsLoaded === true || vm.currentDataItem().VendaItemList().length > 0);
                    }
                    catch (e) {
                        isLoaded = true;
                    }
                    return isLoaded;
                }
                var getVisibleColumns = function(metaDataControl) {
                   if (metaDataControl) return '';
                   var visibleColumns = '';
                   if($('#Exemplos_GRIDs_com_macro_dGridVendaItens').data('igGrid') === undefined) return '';
                   var cols = $('#Exemplos_GRIDs_com_macro_dGridVendaItens').igGrid('option', 'columns');
                   if (cols) {
                     for (var idx = 0; idx < cols.length; idx++) {
                         if (cols[idx].hidden !== true) visibleColumns += (visibleColumns === '' ? '' : ',') + cols[idx].key;
                     }
                   }
                   return visibleColumns;
                };
                var started = false;
                var isElementHided = function (grid, forceCreating) {
                  if (!grid) grid = $('#Exemplos_GRIDs_com_macro_dGridVendaItens');
                  return ((!grid[0] || (!forceCreating && grid.parent().width() <= 0)) && !$('#dialogVendaItem').is(':visible'));
                }
                var refreshData = true;
                var itemsSource = { isElementHided: isElementHided, getVisibleColumns: getVisibleColumns, containerId: 'Exemplos_GRIDs_com_macro_dGridVendaItens_container', dataBind: function (commitData, forceCreating) {
                   var grid = $('#Exemplos_GRIDs_com_macro_dGridVendaItens');
                   if (started && grid.children().length === 0) { started = false; }
                   if (commitData && started) {
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
                       $('#Exemplos_GRIDs_com_macro_dGridVendaItens_groupbyarea').addClass('hide');
                   }
                   if (execFillDetais) {
                     grid.igGrid("option", "dataSource", []);
                     vm.currentDataItem().fillDetails(false, 'VendaItem');
                     return;
                   }
                   var selectedRows = complement.selectedItems();
                   grid.igGridSelection('clearSelection');
                   grid.data('igGrid')._loadingIndicator.show();
                   setTimeout(function () {
                       grid.igGrid("option", "dataSource", getDataSource());
                       var totalGrid = source.dataView().length;
                       if (totalGrid > 0) {
                         if (selectedRows.length > 0) {
                             var dataView = grid.data().igGrid.dataSource.dataView();
                             if (dataView.length > 0) {
                                 $.each(selectedRows, function (index, item) {
                                    var idxFound = findIndexByKey(dataView, 'RowDataId', getAbsoluteValue(item['RowDataId']))
                                    if (idxFound < 0) idxFound = findIndexByKey(dataView, 'IdVendaItem', getAbsoluteValue(item['IdVendaItem']))
                                    if (idxFound >= 0) grid.igGridSelection("selectRow", idxFound);
                                 });
                             }
                         }
                         if ($('#dialogVendaItem').is(':visible')) {
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
                               $('label#currentNumberVendaItem').html(current + ' - ' + totalCurrentPage);
                            }
                            else
                               $('label#currentNumberVendaItem').html(1);
                            $('label#totalNumberVendaItem').html(totalGrid);
                        }
                       } else {
                           $('label#currentNumberVendaItem').html(0);
                           $('label#totalNumberVendaItem').html(0);
                       }
                       grid.data('igGrid')._loadingIndicator.hide();
                   }, 10);
                }};
                var valueGrouBy = -1;
                var deletedIndex = -1;
                function createDataGrid(grid) {
                    var gridId = grid[0].id;
                    grid.igGrid({ height: (getGridHeightSuggested() * 0.75)+'px', width: '100%',
                        dataSource: [],
                        primaryKey: 'RowDataId',
                        autoGenerateColumns: false,
                        autofitLastColumn: true,
                        dataSourceType: 'json',
                        renderCheckboxes: true,
                        autoCommit: true,
                        rowVirtualization: true,
                        virtualizationMode: "fixed",
                        avgRowHeight: 27,
                        autoAdjustHeight: false,
                        cellClick: function(evt, ui) {
                             if (typeof vm.OnGridClientClick === 'function') {
                                 entity = findElementByKey(vm.currentDataItem().VendaItemList, 'RowDataId', ui.rowKey);
                                 vm.OnGridClientClick('Exemplos_GRIDs_com_macro_dGridVendaItens', ui.colKey, entity);
                             }
                        },
                        enableUTCDates: true,
                        featureChooserIconDisplay: 'none',
                        dataRendered: function(evt, ui) { 
                           $('th.ui-iggrid-rowselector-class').unbind('click');
                        },
                        columns: [
                            { key: 'RowDataId', headerText: 'RowDataId', width: '50px', dataType: 'number', hidden: true },
                            { key: 'IdVenda', headerText: 'Id Venda', headerCssClass: 'header-line-break', width: '100pxpx', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   },
                            { key: 'ComboboxVendaItemName', headerText: 'Combobox Venda Item', headerCssClass: 'header-line-break', width: '130px', dataType: 'string', columnCssClass: 'ellipsis', hidden: false, unbound: false, group: null },
                            { key: 'DatetimeVendaItem', headerText: 'Datetime Venda Item', headerCssClass: 'header-line-break', width: '130px', dataType: 'date', columnCssClass: 'ellipsis', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null },
                            { key: 'SmallIntVendaItem', headerText: 'Small Int Venda Item', headerCssClass: 'header-line-break', width: '100pxpx', dataType: 'number', columnCssClass: 'ellipsis', format: 'int', hidden: false, unbound: false, group: null   },
                            { key: 'IntVendaItem', headerText: 'Int Venda Item', headerCssClass: 'header-line-break', width: '100pxpx', dataType: 'number', columnCssClass: 'ellipsis', format: 'int', hidden: false, unbound: false, group: null, formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }},
                            { key: 'BitVendaItem', headerText: 'Bit Venda Item', headerCssClass: 'header-line-break', width: '90pxpx', dataType: 'bool', columnCssClass: 'ellipsis', format: 'checkbox', hidden: false, unbound: false, group: null   },
                            { key: 'BigIntVendaItem', headerText: 'Big Int Venda Item', headerCssClass: 'header-line-break', width: '130pxpx', dataType: 'number', columnCssClass: 'ellipsis', format: 'int', hidden: false, unbound: false, group: null   },
                            { key: 'DecimalVendaItem', headerText: 'Decimal Venda Item', headerCssClass: 'header-line-break', width: '140pxpx', dataType: 'number', columnCssClass: 'ellipsis', format: '0.00', hidden: false, unbound: false, group: null, formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }},
                            { key: 'IdVendaItem', headerText: 'Id Venda Item', headerCssClass: 'header-line-break', width: '120pxpx', dataType: 'number', columnCssClass: 'ellipsis', format: 'int', hidden: false, unbound: false, group: null, formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }},
                            { key: 'StringVendaItem', headerText: 'String Venda Item', headerCssClass: 'header-line-break', width: '200pxpx', dataType: 'string', columnCssClass: 'ellipsis', format: '', hidden: false, unbound: false, group: null   }
                        ],
                        features: [
                                    { name: 'Sorting', type: 'local', caseSensitive: true,
                                      columnSorting: function (evt, ui) { 
                                          $.grep(ui.owner.grid._visibleColumnsArray, function (e) { 
                                              if (e.key === ui.columnKey && e.dataType === 'string') 
                                                  return $('#Exemplos_GRIDs_com_macro_dGridVendaItens').igGridSorting('option', 'caseSensitive', false); 
                                              else if (e.key === ui.columnKey) 
                                                  return $('#Exemplos_GRIDs_com_macro_dGridVendaItens').igGridSorting('option', 'caseSensitive', true); 
                                          }); 
                                      } 
                        },
                                    { name: 'Selection', mode: 'row', multipleSelection: vm.allowMultiSelectionInSearch()},
                                    { name: 'RowSelectors', enableCheckBoxes: vm.allowMultiSelectionInSearch(), enableRowNumbering: false, rowSelectorColumnWidth: 40, checkBoxStateChanged: function(evt, ui){ 
                                       if ((typeof vm.OnDataGridRowChecked === 'function')){
                                           vm.OnDataGridRowChecked('Exemplos_GRIDs_com_macro_dGridVendaItens', self.selectedItems());
                                       }
                                       var selectedRows = grid.igGridSelection('selectedRows');
                                       var selectedRow = ui.owner.grid.selectedRow();
                                       var dataViewLength = ui.grid.dataSource.dataView().length;
                                       if ((selectedRows.length == dataViewLength) || (selectedRow == null && selectedRows.length > 0)){
                                           rowId = [];
                                           rowId['id'] = 1;
                                           selectLightGridCurrentItem(vm.goToKey, 'RowDataId', rowId, vm.currentDataItem().currentVendaItem, vm.currentDataItem().VendaItemList);
                                       } else if(ui.owner.grid.selectedRow() != null)
                                           selectLightGridCurrentItem(vm.goToKey, 'RowDataId', ui, vm.currentDataItem().currentVendaItem, vm.currentDataItem().VendaItemList);
                                        }, 
                                        checkBoxStateChanging: function (evt, ui) { isFiredFromCheckbox = true; }
                                    },
                                    { name: 'Tooltips', columnSettings:[{ columnKey: "IdVenda", allowTooltips: false },{ columnKey: "ComboboxVendaItem", allowTooltips: true },{ columnKey: "DatetimeVendaItem", allowTooltips: true },{ columnKey: "SmallIntVendaItem", allowTooltips: true },{ columnKey: "IntVendaItem", allowTooltips: true },{ columnKey: "BitVendaItem", allowTooltips: true },{ columnKey: "BigIntVendaItem", allowTooltips: true },{ columnKey: "DecimalVendaItem", allowTooltips: true },{ columnKey: "IdVendaItem", allowTooltips: true },{ columnKey: "StringVendaItem", allowTooltips: true }] },
                                    { name: 'Resizing' }, 
                                    { name: 'MultiColumnHeaders' }
                                    ,{ name: 'ColumnMoving', addMovingDropdown: false }
                        
                        
                                   ,{ name: 'Updating', horizontalMoveOnEnter: true,
                                       enableDataDirtyException: false, 
                                       generatePrimaryKeyValue: function(evt, ui){  },
                                       enableDeleteRow: false,
                                       enableAddRow: false,
                                       startEditTriggers: 'click',
                                       editMode: 'none',
                                       rowEditDialogContainment: 'window',
                                       showReadonlyEditors: false,
                                       showDoneCancelButtons: false,
                                    }
                                ]
                        });
                        if ((typeof vm.OnDataGridCreated === 'function')){
                            vm.OnDataGridCreated('Exemplos_GRIDs_com_macro_dGridVendaItens');
                        }
                        var selectionrowselectionchanged = null, selectedRowId = -1;
                        selectionrowselectionchanged = function (evt, ui) {
                            if ((ui.owner.grid.selectedRow() && typeof ui.owner.grid.selectedRow().id !== 'undefined') || (ui.owner.grid.selectedRows() && ui.owner.grid.selectedRows().length > 0)) { 
                                selectLightGridCurrentItem(vm.goToKey, 'RowDataId', ui, vm.currentDataItem().currentVendaItem, vm.currentDataItem().VendaItemList); 
                             } 
                             if ((typeof vm.OnDataGridRowChecked === 'function')){ vm.OnDataGridRowChecked('Exemplos_GRIDs_com_macro_dGridVendaItens', self.selectedItems()); }
                        };
                        $(document).delegate('#Exemplos_GRIDs_com_macro_dGridVendaItens', 'iggridselectionrowselectionchanged', selectionrowselectionchanged);
                    }
                    vm.addDataSource({ key: 'Exemplos_GRIDs_com_macro_dGridVendaItens', name: 'VendaItemList', itemsSource: itemsSource });
                }
            
        };
        
        return complement;
    }
    
    return complementCtor;
});
