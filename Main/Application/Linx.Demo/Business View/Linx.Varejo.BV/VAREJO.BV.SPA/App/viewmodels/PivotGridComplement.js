define(['managers/__auth', 'managers/user'], function (managerAuth, managerUser) {
var complementCtor = function() {
    var complement = {
    isAutomatic: true
    , renderpivot13e2caab32de42d09b5bded3f72a9cbaPivotGrid_pivotVendaItem: function(vm) {
    if (!vm.hasMainTopDataGrid()) vm.hasMainTopDataGrid(true);
    var pivot = null;
    var arrayData = [];
    var currentStatus = '';
    var currentPage = undefined;
    var app = require('durandal/app');
    var jEntitySearchPivotRelationship = '';
    var getVisibleRowsColumns = function() {
        var context = [];
        if (pivot) {
            var measures = pivot.getMeasures();
            if (measures && measures.length)
                measures.forEach(function (item) {
                    if (context.indexOf(item.uniqueName) < 0 && item.uniqueName != '[Measures]' && !item.calculated)
                        context.push(item.uniqueName);
                });
        
            var rows = pivot.getRows();
            if (rows && rows.length)
                rows.forEach(function (item) {
                    if (context.indexOf(item.uniqueName) < 0 && item.uniqueName != '[Measures]')
                        context.push(item.uniqueName);
                });
        
            var coluns = pivot.getColumns();
            if (coluns && coluns.length)
                coluns.forEach(function (item) {
                    if (context.indexOf(item.uniqueName) < 0 && item.uniqueName != '[Measures]')
                        context.push(item.uniqueName);
                });
        
            var filters = pivot.getPages();
            if (filters && filters.length)
                filters.forEach(function (item) {
                    if (context.indexOf(item.uniqueName) < 0 && item.uniqueName != '[Measures]')
                        context.push(item.uniqueName);
                });
        }
        
        return context.join(',');
    }
    var getVisibleColumns = function() {
       return '';
    }
    var itemsSource = { getVisibleColumns: getVisibleColumns, dataBind: function (commitData) {
        if ($('#pivot13e2caab-32de-42d0-9b5b-ded3f72a9cbaPivotGrid_pivotVendaItem').is(':visible') && (currentStatus != vm.status() || currentPage != vm.dataToolbar.currentPage() || !commitData)) {
             currentStatus = vm.status();
             currentPage = vm.dataToolbar.currentPage();
             if (currentStatus && currentStatus.toLowerCase() == 'c') {
                  jEntitySearchPivotRelationship = '';
             }
        if(vm != null && vm.dataView != null ) {
             arrayData = unwrapObservableArray(vm.dataView, vm);
        }
        if(pivot == null) {
        $('#pivot13e2caab-32de-42d0-9b5b-ded3f72a9cbaPivotGrid_pivotVendaItem #fm-fields-view .fm-ui-btn:contains(\'OK\')')
             .live('mouseup', function () {
         });
        
        $('#pivot13e2caab-32de-42d0-9b5b-ded3f72a9cbaPivotGrid_pivotVendaItem #fm-toolbar-row .fm-ui-btn:contains(\'OK\')')
           .live('mouseup', function () {
               setTimeout(function () { filterPivotRelationship() }, 1);
        });
             $('#pivot13e2caab-32de-42d0-9b5b-ded3f72a9cbaPivotGrid_pivotVendaItem .btn-toggle-view').die('click');
             $('#pivot13e2caab-32de-42d0-9b5b-ded3f72a9cbaPivotGrid_pivotVendaItem .btn-toggle-view').live('click', function () {
                  var options = pivot.getOptions();
                  if (options.viewType == 'grid') {
                       options.chartType = (options.chartType || 'bar');
                       pivot.showCharts(options.chartType, true);
                  } else { 
                       pivot.showGrid();
                  }
             });
             $('#pivot13e2caab-32de-42d0-9b5b-ded3f72a9cbaPivotGrid_pivotVendaItem .btn-toggle-toolbar').die('click');
             $('#pivot13e2caab-32de-42d0-9b5b-ded3f72a9cbaPivotGrid_pivotVendaItem .btn-toggle-toolbar').live('click', function(){ 
                  var toolbar = pivot.getToolbarInstanceByPivotName('pivot13e2caab-32de-42d0-9b5b-ded3f72a9cbaPivotGrid_pivotVendaItem')
                  if (toolbar) toolbar.toggleToolbar();
                  $(this).addClass(function (i, current) {
                      $(this).removeClass();
                      if (current.indexOf("down") >= 0)
                          current = current.replace("down", "up");
                      else
                          current = current.replace("up", "down");
                      return current;
                  });
             });
        }
        
        var pivotContext = { rows: [], columns: [], pages: [], measures: [], options: null, formats: [], conditions: [], report: null };
        
        var addMeasuresFormulas = function () {
        };
        
        var setReport = function() { 
            var data = arrayData.map(function(item){
                return {'BigIntVendaItem': isNullOrEmpty(item.BigIntVendaItem) ? 0 : item.BigIntVendaItem, 'BitVendaItem': (isNullOrEmpty(item.BitVendaItem) ? '' : item.BitVendaItem.toString()), 'ComboboxVendaItem': isNullOrEmpty(item.ComboboxVendaItem) ? 0 : item.ComboboxVendaItem, 'DatetimeVendaItem': (isNullOrEmpty(item.DatetimeVendaItem) ? '' : Globalize.format(getUTCDate(item.DatetimeVendaItem), 'MM/dd/yyyy')), 'DecimalVendaItem': isNullOrEmpty(item.DecimalVendaItem) ? 0 : item.DecimalVendaItem, 'IdVenda': (isNullOrEmpty(item.IdVenda) ? '' : item.IdVenda.toString()), 'IdVendaItem': (isNullOrEmpty(item.IdVendaItem) ? '' : item.IdVendaItem.toString()), 'IntVendaItem': isNullOrEmpty(item.IntVendaItem) ? 0 : item.IntVendaItem, 'SmallIntVendaItem': isNullOrEmpty(item.SmallIntVendaItem) ? 0 : item.SmallIntVendaItem, 'StringVendaItem': (isNullOrEmpty(item.StringVendaItem) ? '' : item.StringVendaItem.toString()), 'BigIntVendaItemKpiInfo': (isNullOrEmpty(item.BigIntVendaItemKpiInfo) ? '' : item.BigIntVendaItemKpiInfo.toString())};
            });
        
            var structure = {};
            structure.BigIntVendaItem = { type:'number', caption: 'Big Int Venda Item' }; 
            structure.BitVendaItem = { type:'string', caption: 'Bit Venda Item', dimensionUniqueName: 'ctrlVendaItem', dimensionCaption: 'VendaItem'  };
            structure.ComboboxVendaItem = { type:'number', caption: 'Combobox Venda Item' }; 
            structure.DatetimeVendaItem = { type:'date string', caption: 'Datetime Venda Item', dimensionUniqueName: 'ctrlVendaItem', dimensionCaption: 'VendaItem'  };
            structure.DecimalVendaItem = { type:'number', caption: 'Decimal Venda Item' }; 
            structure.IdVenda = { type:'string', caption: 'Id Venda', dimensionUniqueName: 'ctrlVendaItem', dimensionCaption: 'VendaItem'  };
            structure.IdVendaItem = { type:'string', caption: 'Id Venda Item', dimensionUniqueName: 'ctrlVendaItem', dimensionCaption: 'VendaItem'  };
            structure.IntVendaItem = { type:'number', caption: 'Int Venda Item' }; 
            structure.SmallIntVendaItem = { type:'number', caption: 'Small Int Venda Item' }; 
            structure.StringVendaItem = { type:'string', caption: 'String Venda Item', dimensionUniqueName: 'ctrlVendaItem', dimensionCaption: 'VendaItem'  };
            structure.BigIntVendaItemKpiInfo = { type:'string', caption: 'Big Int Venda Item (KPI)', dimensionUniqueName: 'ctrlVendaItem', dimensionCaption: 'VendaItem'  };
        if (pivotContext.report) {
            pivotContext.report.data = [structure].concat(data);
            pivot.setReport(pivotContext.report);
        } else {
            var report = {
                dataSourceType: 'json',
                data: [structure].concat(data),
                configuratorActive: false,
                viewType: 'grid',
                showHeaders: false,
                fitGridlines: false,
                showGrandTotals: 'on',
                showChartsWarning: false,
                datePattern: 'dd/MM/yyyy',
                dateTimePattern: 'dd/MM/yyyy HH:mm:ss',
                rows: [{ uniqueName: 'Ano' }],
                columns: [{ uniqueName: '[Measures]' }],
                measures: [{ uniqueName: 'Valor' }],
            };
        
            pivot.setReport(report);
            pivot.setTopX("Ano", 2, "Valor");
        }
        };
        
        var getFormats = function () {
        
            var measuresFormat = getMeasureFormats();
            var measuresCalculatedFormat = getMeasureCalculated();
        
            return measuresFormat.concat(measuresCalculatedFormat);
        }
        
        var getMeasureCalculated = function(){
        
            var formatMeasures = [];
            var measures = pivot.getMeasures();
        
            if (measures && measures.some(function (item) { return item.calculated; })) {
                var measuresCalculated = measures.filter(function (item) { return item.calculated; });
                measuresCalculated.forEach(function (item) {
                    formatMeasures.push({
                        name: item.uniqueName,
                        current: pivot.getFormat(item.uniqueName)
                    });
                });
            }
        
            return formatMeasures;
        }
        
        var getMeasureFormats = function() {
        
            var formatMeasures = [];
        
            formatMeasures.push({
                name: 'BigIntVendaItem',
                current: pivot.getFormat('BigIntVendaItem')
            });
        
            formatMeasures.push({
                name: 'ComboboxVendaItem',
                current: pivot.getFormat('ComboboxVendaItem')
            });
        
            formatMeasures.push({
                name: 'DecimalVendaItem',
                current: pivot.getFormat('DecimalVendaItem')
            });
        
            formatMeasures.push({
                name: 'IntVendaItem',
                current: pivot.getFormat('IntVendaItem')
            });
        
            formatMeasures.push({
                name: 'SmallIntVendaItem',
                current: pivot.getFormat('SmallIntVendaItem')
            });
        
            return formatMeasures;
        }
        
        var getAllConditions = function () {
            return pivot.getAllConditions();
        };
        
        var setConditions = function () {
            if (pivotContext.conditions.length) {
                pivotContext.conditions.forEach(function (item) {
                    pivot.addCondition(item);
                });
            }
        }
        
        var setFormat = function () {
             if (pivotContext.formats.length) {
                 pivotContext.formats.forEach(function (item) {
                     pivot.setFormat(item.current, item.name);
                 });
             } else {
             pivot.setFormat({
                 decimalSeparator: ',',
                 thousandsSeparator: '.',
                 name: 'BigIntVendaItem'
             }, 'BigIntVendaItem');
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
             pivot.setFormat({
                 decimalSeparator: ',',
                 thousandsSeparator: '.',
                 name: 'IntVendaItem'
             }, 'IntVendaItem');
             pivot.setFormat({
                 decimalSeparator: ',',
                 thousandsSeparator: '.',
                 name: 'SmallIntVendaItem'
             }, 'SmallIntVendaItem');
             }
        };
        
        var setSlice = function () {
             if (pivotContext.rows.length || pivotContext.columns.length || pivotContext.measures.length || pivotContext.pages.length) {
                 pivot.runQuery(pivotContext);
             }
        }
        
        var setOptions = function () {
            if (pivotContext.options != null) {
                 pivot.setOptions(pivotContext.options);
            }
        };
        
        var clearFilters = function (pivotRelationship) {
            var report = pivotRelationship.getReport();
            if (report.columns.length)
                report.columns.forEach(function (item) { if (item.filter) pivotRelationship.clearFilter(item.uniqueName); });
            if (report.rows.length)
                report.rows.forEach(function (item) { if (item.filter) pivotRelationship.clearFilter(item.uniqueName); });
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
        }
        
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
        }
        
        var parseFilters = function (cell) {
            var filterItems = [];
            var report = pivot.getReport();
            if (cell) {
                setFilterByCell(filterItems, cell.rows);
                setFilterByCell(filterItems, cell.columns);
            }
            setFilterByReport(filterItems, report.rows);
            setFilterByReport(filterItems, report.pages);
            setFilterByReport(filterItems, report.columns);
            return filterItems;
        };
        
        var parsejEntitySearch = function (filters) {
            var jEntitySearch = '';
            if (filters && filters.length)
                filters.forEach(function (item) {
                    var separator = item.values.length > 1 ? ',' : '';
                    var type = item.values.length > 1 ? 'S' : (isNaN(item.values[0].split('.')[1].replace('[', '').replace(']', '')) ? 'S' : 'I');
                    var operator = (item.negation) ? '!=' : '==';
                    if (item.values.length > 1)
                        operator = (item.negation) ? '!In' : 'In';
                    if (jEntitySearch != '') jEntitySearch += ';';
                    jEntitySearch += item.key  + '#' + operator + '#' + type;
                    item.values.forEach(function (value) {
                         value = value.split('.')[1].replace('[', '').replace(']', '');
                         jEntitySearch += value + separator;
                    });
                });
            return jEntitySearch;
        };
        
        var filterPivotRelationship = function (cell) {
           if ((cell && cell.type != 'value') || isNaN(cell.value)) return false;
           var dataContext = vm.getDataContext();
           var dataFilter = parseFilters(cell);
           var jEntitySearch = parsejEntitySearch(dataFilter);
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
        
        var onClickPivotCell = function (cell) {
            filterPivotRelationship(cell);
        };
        var setLayouts = function () {
        var toolbarInstance = pivot.getToolbarInstanceByPivotName('pivot13e2caab-32de-42d0-9b5b-ded3f72a9cbaPivotGrid_pivotVendaItem');
        
             if (toolbarInstance) {
                 toolbarInstance.setPivotName('VendaItem');
                 toolbarInstance.setReportFiles(vm.layoutFiles);
                 toolbarInstance.setGetSelectedLayoutContent(vm.getDataContext().getSelectedLayoutContent);
                 toolbarInstance.setProjectName(vm.layoutFiles[0].projectName);
                 toolbarInstance.setViewName(vm.viewName);
                 pivot.prefixNameLayout = 'VendaItem';
                 pivot.pivotAdapterLayout = 'VendaItem';
             }
        
             vm.layoutFiles.forEach(function(file) {
                 if (file.selected && file.layoutFullName.indexOf('.xml') > 0 && file.layoutFullName.indexOf('VendaItem') > 0)
                     pivot.load(file.layoutFullName);
             });
        };
        
        var createPivot = function () {
             pivot.clear();
             setReport();
             addMeasuresFormulas();
             setLayouts();
             setOptions();
             setSlice();
             setFormat();
             pivot.refresh();
             pivot.closeFieldsList();
             addToolbarFooter();
        };
        
        var addToolbarFooter = function () {
             var listItems = '';
             listItems += '<li><i class="fa fa-list btn-toggle-view" title="mudar visão"></i></li>';
             listItems += '<li><i class="fa fa-angle-down btn-toggle-toolbar" title="expandir toolbar"></i></li>';
             $('#pivot13e2caab-32de-42d0-9b5b-ded3f72a9cbaPivotGrid_pivotVendaItem').append('<div class="plus-actions"><ul>'+ listItems +'</ul></div>');
        }
        
        
        var updatePivot = function () {
            pivotContext.rows = pivot.getRows();
            pivotContext.pages = pivot.getPages();
            pivotContext.columns = pivot.getColumns();
            pivotContext.options = pivot.getOptions();
            pivotContext.measures = pivot.getMeasures();
            pivotContext.formats = getFormats();
            pivotContext.report = pivot.getReport();
            pivotContext.conditions = getAllConditions();
            pivot.clear();
        
            setReport();
            addMeasuresFormulas();
            setOptions();
            setSlice();
            setFormat();
            setConditions();
            pivot.refresh();
            pivot.closeFieldsList();
        };
        
        var createInstance = function () {
             var width = '100%';
             var height = '350';
             var type = 'html5';
             var withToolbar = true;
             var isLayoutToolbar = false;
             var path = managerAuth.META_ROOT + managerAuth.META_MODULE_ID + '/lib/flexmonster/';
             var containerId = 'pivot13e2caab-32de-42d0-9b5b-ded3f72a9cbaPivotGrid_pivotVendaItem';
        
             var paramns = {
                  jsCellClickHandler: onClickPivotCell,
                  jsPivotCreationCompleteHandler: createPivot,
                  licenseKey: managerAuth.flexMonsterLicenseKey,
                  configUrl: managerAuth.META_ROOT + managerAuth.META_MODULE_ID + '/lib/flexmonster/report_br.xml',
                  configuratorActive: false,
             }
        
             pivot = flexmonster.embedPivotComponent(path, containerId, width, height, paramns, type, withToolbar, undefined, isLayoutToolbar);
        }
        
             try {
                  if(pivot == null) {
                       createInstance();
                  } else {
                       updatePivot();
                  }
             } catch (e) { }
        }
        
    }
    }
    vm.addDataSource({ key: 'pivot13e2caab-32de-42d0-9b5b-ded3f72a9cbaPivotGrid_pivotVendaItem', name: 'dataView', itemsSource: itemsSource });
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
