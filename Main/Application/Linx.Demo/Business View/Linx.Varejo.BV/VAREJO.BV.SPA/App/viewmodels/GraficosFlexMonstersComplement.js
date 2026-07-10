define(['managers/__auth', 'managers/user'], function (managerAuth, managerUser) {
var complementCtor = function() {
    var complement = {
    isAutomatic: true
    , renderpivotf099f286c61c485b91d23071b82aeb92GraficosFlexMonsters_pivotVendaItem: function(vm) {
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
        if ($('#pivotf099f286-c61c-485b-91d2-3071b82aeb92GraficosFlexMonsters_pivotVendaItem').is(':visible') && (currentStatus != vm.status() || currentPage != vm.dataToolbar.currentPage() || !commitData)) {
             currentStatus = vm.status();
             currentPage = vm.dataToolbar.currentPage();
             if (currentStatus && currentStatus.toLowerCase() == 'c') {
                  jEntitySearchPivotRelationship = '';
             }
        if(vm != null && vm.dataView != null ) {
             arrayData = unwrapObservableArray(vm.dataView, vm);
        }
        if(pivot == null) {
        $('#pivotf099f286-c61c-485b-91d2-3071b82aeb92GraficosFlexMonsters_pivotVendaItem #fm-fields-view .fm-ui-btn:contains(\'OK\')')
             .live('mouseup', function () {
         });
        
        $('#pivotf099f286-c61c-485b-91d2-3071b82aeb92GraficosFlexMonsters_pivotVendaItem #fm-toolbar-row .fm-ui-btn:contains(\'OK\')')
           .live('mouseup', function () {
               setTimeout(function () { filterPivotRelationship() }, 1);
        });
             $('#pivotf099f286-c61c-485b-91d2-3071b82aeb92GraficosFlexMonsters_pivotVendaItem .btn-toggle-toolbar').die('click');
             $('#pivotf099f286-c61c-485b-91d2-3071b82aeb92GraficosFlexMonsters_pivotVendaItem .btn-toggle-toolbar').live('click', function(){ 
                  var toolbar = pivot.getToolbarInstanceByPivotName('pivotf099f286-c61c-485b-91d2-3071b82aeb92GraficosFlexMonsters_pivotVendaItem')
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
                return {'BigIntVendaItem': isNullOrEmpty(item.BigIntVendaItem) ? 0 : item.BigIntVendaItem, 'BitVendaItem': (isNullOrEmpty(item.BitVendaItem) ? '' : item.BitVendaItem.toString()), 'ComboboxVendaItem': (isNullOrEmpty(item.ComboboxVendaItemName) ? '' : item.ComboboxVendaItemName.toString()), 'DatetimeVendaItem': (isNullOrEmpty(item.DatetimeVendaItem) ? '' : Globalize.format(getUTCDate(item.DatetimeVendaItem), 'MM/dd/yyyy')), 'DecimalVendaItem': (isNullOrEmpty(item.DecimalVendaItem) ? '' : item.DecimalVendaItem.toString()), 'IdVenda': (isNullOrEmpty(item.IdVenda) ? '' : item.IdVenda.toString()), 'IdVendaItem': (isNullOrEmpty(item.IdVendaItem) ? '' : item.IdVendaItem.toString()), 'IntVendaItem': (isNullOrEmpty(item.IntVendaItem) ? '' : item.IntVendaItem.toString()), 'SmallIntVendaItem': (isNullOrEmpty(item.SmallIntVendaItem) ? '' : item.SmallIntVendaItem.toString()), 'StringVendaItem': (isNullOrEmpty(item.StringVendaItem) ? '' : item.StringVendaItem.toString()), 'BigIntVendaItemKpiInfo': (isNullOrEmpty(item.BigIntVendaItemKpiInfo) ? '' : item.BigIntVendaItemKpiInfo.toString())};
            });
        
            var structure = {};
            structure.BigIntVendaItem = { type:'number', caption: 'Big Int Venda Item' }; 
            structure.BitVendaItem = { type:'string', caption: 'Bit Venda Item', dimensionUniqueName: 'ctrlVendaItem', dimensionCaption: ''  };
            structure.ComboboxVendaItem = { type:'string', caption: 'Combobox Venda Item', dimensionUniqueName: 'ctrlVendaItem', dimensionCaption: ''  };
            structure.DatetimeVendaItem = { type:'date string', caption: 'Datetime Venda Item', dimensionUniqueName: 'ctrlVendaItem', dimensionCaption: ''  };
            structure.DecimalVendaItem = { type:'string', caption: 'Decimal Venda Item', dimensionUniqueName: 'ctrlVendaItem', dimensionCaption: ''  };
            structure.IdVenda = { type:'string', caption: 'Id Venda', dimensionUniqueName: 'ctrlVendaItem', dimensionCaption: ''  };
            structure.IdVendaItem = { type:'string', caption: 'Id Venda Item', dimensionUniqueName: 'ctrlVendaItem', dimensionCaption: ''  };
            structure.IntVendaItem = { type:'string', caption: 'Int Venda Item', dimensionUniqueName: 'ctrlVendaItem', dimensionCaption: ''  };
            structure.SmallIntVendaItem = { type:'string', caption: 'Small Int Venda Item', dimensionUniqueName: 'ctrlVendaItem', dimensionCaption: ''  };
            structure.StringVendaItem = { type:'string', caption: 'String Venda Item', dimensionUniqueName: 'ctrlVendaItem', dimensionCaption: ''  };
            structure.BigIntVendaItemKpiInfo = { type:'string', caption: 'Big Int Venda Item (KPI)', dimensionUniqueName: 'ctrlVendaItem', dimensionCaption: ''  };
        if (pivotContext.report) {
            pivotContext.report.data = [structure].concat(data);
            pivot.setReport(pivotContext.report);
        } else {
            var report = {
                dataSourceType: 'json',
                data: [structure].concat(data),
                configuratorActive: false,
                viewType: 'grid_charts',
                chartType: 'bar',
                chartPosition: 'left',
                showHeaders: false,
                fitGridlines: false,
                showGrandTotals: 'off',
                showChartsWarning: false,
                datePattern: 'dd/MM/yyyy',
                dateTimePattern: 'dd/MM/yyyy HH:mm:ss',
                rows: [{ uniqueName: 'Mes' }],
                columns: [{ uniqueName: 'Ano' }],
                measures: [{ uniqueName: 'Valor' }],
            };
               report.columns.push({uniqueName: '[Measures]'})
        
            pivot.setReport(report);
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
        var toolbarInstance = pivot.getToolbarInstanceByPivotName('pivotf099f286-c61c-485b-91d2-3071b82aeb92GraficosFlexMonsters_pivotVendaItem');
        
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
             listItems += '<li><i class="fa fa-angle-down btn-toggle-toolbar" title="expandir toolbar"></i></li>';
             $('#pivotf099f286-c61c-485b-91d2-3071b82aeb92GraficosFlexMonsters_pivotVendaItem').append('<div class="plus-actions"><ul>'+ listItems +'</ul></div>');
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
             var height = '500';
             var type = 'html5';
             var withToolbar = true;
             var isLayoutToolbar = false;
             var path = managerAuth.META_ROOT + managerAuth.META_MODULE_ID + '/lib/flexmonster/';
             var containerId = 'pivotf099f286-c61c-485b-91d2-3071b82aeb92GraficosFlexMonsters_pivotVendaItem';
        
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
    vm.addDataSource({ key: 'pivotf099f286-c61c-485b-91d2-3071b82aeb92GraficosFlexMonsters_pivotVendaItem', name: 'dataView', itemsSource: itemsSource });
}

, renderpivot31acee23e1864c76a03e55c35af308c5GraficosFlexMonsters_pivotCopy_612517062_VendaItemPagedList: function(vm) {
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
        if ($('#pivot31acee23-e186-4c76-a03e-55c35af308c5GraficosFlexMonsters_pivotCopy_612517062_VendaItemPagedList').is(':visible') && (currentStatus != vm.status() || currentPage != vm.dataToolbar.currentPage() || !commitData)) {
             currentStatus = vm.status();
             currentPage = vm.dataToolbar.currentPage();
             if (currentStatus && currentStatus.toLowerCase() == 'c') {
                  jEntitySearchPivotRelationship = '';
             }
        if(vm != null && vm.dataView != null ) {
             arrayData = unwrapObservableArray(vm.dataView, vm);
        }
        if(pivot == null) {
        $('#pivot31acee23-e186-4c76-a03e-55c35af308c5GraficosFlexMonsters_pivotCopy_612517062_VendaItemPagedList #fm-fields-view .fm-ui-btn:contains(\'OK\')')
             .live('mouseup', function () {
         });
        
        $('#pivot31acee23-e186-4c76-a03e-55c35af308c5GraficosFlexMonsters_pivotCopy_612517062_VendaItemPagedList #fm-toolbar-row .fm-ui-btn:contains(\'OK\')')
           .live('mouseup', function () {
               setTimeout(function () { filterPivotRelationship() }, 1);
        });
             $('#pivot31acee23-e186-4c76-a03e-55c35af308c5GraficosFlexMonsters_pivotCopy_612517062_VendaItemPagedList .btn-toggle-toolbar').die('click');
             $('#pivot31acee23-e186-4c76-a03e-55c35af308c5GraficosFlexMonsters_pivotCopy_612517062_VendaItemPagedList .btn-toggle-toolbar').live('click', function(){ 
                  var toolbar = pivot.getToolbarInstanceByPivotName('pivot31acee23-e186-4c76-a03e-55c35af308c5GraficosFlexMonsters_pivotCopy_612517062_VendaItemPagedList')
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
                return {'Ano': (isNullOrEmpty(item.Ano) ? '' : item.Ano.toString()), 'BigIntVendaItem': isNullOrEmpty(item.BigIntVendaItem) ? 0 : item.BigIntVendaItem, 'BitVendaItem': (isNullOrEmpty(item.BitVendaItem) ? '' : item.BitVendaItem.toString()), 'ComboboxVendaItem': (isNullOrEmpty(item.ComboboxVendaItemName) ? '' : item.ComboboxVendaItemName.toString()), 'DatetimeVendaItem': (isNullOrEmpty(item.DatetimeVendaItem) ? '' : Globalize.format(getUTCDate(item.DatetimeVendaItem), 'MM/dd/yyyy')), 'DecimalVendaItem': (isNullOrEmpty(item.DecimalVendaItem) ? '' : item.DecimalVendaItem.toString()), 'GuidVendaItem': (isNullOrEmpty(item.GuidVendaItem) ? '' : item.GuidVendaItem.toString()), 'IdVenda': (isNullOrEmpty(item.IdVenda) ? '' : item.IdVenda.toString()), 'IdVendaItem': (isNullOrEmpty(item.IdVendaItem) ? '' : item.IdVendaItem.toString()), 'IntVendaItem': (isNullOrEmpty(item.IntVendaItem) ? '' : item.IntVendaItem.toString()), 'Mes': (isNullOrEmpty(item.Mes) ? '' : item.Mes.toString()), 'SmallIntVendaItem': (isNullOrEmpty(item.SmallIntVendaItem) ? '' : item.SmallIntVendaItem.toString()), 'StringVendaItem': (isNullOrEmpty(item.StringVendaItem) ? '' : item.StringVendaItem.toString()), 'Valor': isNullOrEmpty(item.Valor) ? 0 : item.Valor};
            });
        
            var structure = {};
            structure.Ano = { type:'string', caption: 'Ano', dimensionUniqueName: 'ctrlCopy_612517062_VendaItemPagedList', dimensionCaption: ''  };
            structure.BigIntVendaItem = { type:'number', caption: 'Big Int Venda Item' }; 
            structure.BitVendaItem = { type:'string', caption: 'Bit Venda Item', dimensionUniqueName: 'ctrlCopy_612517062_VendaItemPagedList', dimensionCaption: ''  };
            structure.ComboboxVendaItem = { type:'string', caption: 'Combobox Venda Item', dimensionUniqueName: 'ctrlCopy_612517062_VendaItemPagedList', dimensionCaption: ''  };
            structure.DatetimeVendaItem = { type:'date string', caption: 'Datetime Venda Item', dimensionUniqueName: 'ctrlCopy_612517062_VendaItemPagedList', dimensionCaption: ''  };
            structure.DecimalVendaItem = { type:'string', caption: 'Decimal Venda Item', dimensionUniqueName: 'ctrlCopy_612517062_VendaItemPagedList', dimensionCaption: ''  };
            structure.GuidVendaItem = { type:'string', caption: 'Guid Venda Item', dimensionUniqueName: 'ctrlCopy_612517062_VendaItemPagedList', dimensionCaption: ''  };
            structure.IdVenda = { type:'string', caption: 'Id Venda', dimensionUniqueName: 'ctrlCopy_612517062_VendaItemPagedList', dimensionCaption: ''  };
            structure.IdVendaItem = { type:'string', caption: 'Id Venda Item', dimensionUniqueName: 'ctrlCopy_612517062_VendaItemPagedList', dimensionCaption: ''  };
            structure.IntVendaItem = { type:'string', caption: 'Int Venda Item', dimensionUniqueName: 'ctrlCopy_612517062_VendaItemPagedList', dimensionCaption: ''  };
            structure.Mes = { type:'string', caption: 'Mes', dimensionUniqueName: 'ctrlCopy_612517062_VendaItemPagedList', dimensionCaption: ''  };
            structure.SmallIntVendaItem = { type:'string', caption: 'Small Int Venda Item', dimensionUniqueName: 'ctrlCopy_612517062_VendaItemPagedList', dimensionCaption: ''  };
            structure.StringVendaItem = { type:'string', caption: 'String Venda Item', dimensionUniqueName: 'ctrlCopy_612517062_VendaItemPagedList', dimensionCaption: ''  };
            structure.Valor = { type:'number', caption: 'Valor' }; 
        if (pivotContext.report) {
            pivotContext.report.data = [structure].concat(data);
            pivot.setReport(pivotContext.report);
        } else {
            var report = {
                dataSourceType: 'json',
                data: [structure].concat(data),
                configuratorActive: false,
                viewType: 'grid_charts',
                chartType: 'line',
                chartPosition: 'left',
                showHeaders: false,
                fitGridlines: false,
                showGrandTotals: 'on',
                showChartsWarning: false,
                datePattern: 'dd/MM/yyyy',
                dateTimePattern: 'dd/MM/yyyy HH:mm:ss',
                rows: [{ uniqueName: 'Mes' }],
                columns: [{ uniqueName: '[Measures]' }],
                measures: [{ uniqueName: 'Valor' }],
            };
        
            pivot.setReport(report);
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
                name: 'Valor',
                current: pivot.getFormat('Valor')
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
                 name: 'Valor'
             }, 'Valor');
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
        var toolbarInstance = pivot.getToolbarInstanceByPivotName('pivot31acee23-e186-4c76-a03e-55c35af308c5GraficosFlexMonsters_pivotCopy_612517062_VendaItemPagedList');
        
             if (toolbarInstance) {
                 toolbarInstance.setPivotName('Copy_612517062_VendaItemPagedList');
                 toolbarInstance.setReportFiles(vm.layoutFiles);
                 toolbarInstance.setGetSelectedLayoutContent(vm.getDataContext().getSelectedLayoutContent);
                 toolbarInstance.setProjectName(vm.layoutFiles[0].projectName);
                 toolbarInstance.setViewName(vm.viewName);
                 pivot.prefixNameLayout = 'Copy_612517062_VendaItemPagedList';
                 pivot.pivotAdapterLayout = 'VendaItem';
             }
        
             vm.layoutFiles.forEach(function(file) {
                 if (file.selected && file.layoutFullName.indexOf('.xml') > 0 && file.layoutFullName.indexOf('Copy_612517062_VendaItemPagedList') > 0)
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
             listItems += '<li><i class="fa fa-angle-down btn-toggle-toolbar" title="expandir toolbar"></i></li>';
             $('#pivot31acee23-e186-4c76-a03e-55c35af308c5GraficosFlexMonsters_pivotCopy_612517062_VendaItemPagedList').append('<div class="plus-actions"><ul>'+ listItems +'</ul></div>');
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
             var height = '500';
             var type = 'html5';
             var withToolbar = true;
             var isLayoutToolbar = false;
             var path = managerAuth.META_ROOT + managerAuth.META_MODULE_ID + '/lib/flexmonster/';
             var containerId = 'pivot31acee23-e186-4c76-a03e-55c35af308c5GraficosFlexMonsters_pivotCopy_612517062_VendaItemPagedList';
        
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
    vm.addDataSource({ key: 'pivot31acee23-e186-4c76-a03e-55c35af308c5GraficosFlexMonsters_pivotCopy_612517062_VendaItemPagedList', name: 'dataView', itemsSource: itemsSource });
}

, renderpivot913a276f49fa4d689f64e3acfe76cbd4GraficosFlexMonsters_pivotCopy_612534828_VendaItemPagedList: function(vm) {
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
        if ($('#pivot913a276f-49fa-4d68-9f64-e3acfe76cbd4GraficosFlexMonsters_pivotCopy_612534828_VendaItemPagedList').is(':visible') && (currentStatus != vm.status() || currentPage != vm.dataToolbar.currentPage() || !commitData)) {
             currentStatus = vm.status();
             currentPage = vm.dataToolbar.currentPage();
             if (currentStatus && currentStatus.toLowerCase() == 'c') {
                  jEntitySearchPivotRelationship = '';
             }
        if(vm != null && vm.dataView != null ) {
             arrayData = unwrapObservableArray(vm.dataView, vm);
        }
        if(pivot == null) {
        $('#pivot913a276f-49fa-4d68-9f64-e3acfe76cbd4GraficosFlexMonsters_pivotCopy_612534828_VendaItemPagedList #fm-fields-view .fm-ui-btn:contains(\'OK\')')
             .live('mouseup', function () {
         });
        
        $('#pivot913a276f-49fa-4d68-9f64-e3acfe76cbd4GraficosFlexMonsters_pivotCopy_612534828_VendaItemPagedList #fm-toolbar-row .fm-ui-btn:contains(\'OK\')')
           .live('mouseup', function () {
               setTimeout(function () { filterPivotRelationship() }, 1);
        });
             $('#pivot913a276f-49fa-4d68-9f64-e3acfe76cbd4GraficosFlexMonsters_pivotCopy_612534828_VendaItemPagedList .btn-toggle-toolbar').die('click');
             $('#pivot913a276f-49fa-4d68-9f64-e3acfe76cbd4GraficosFlexMonsters_pivotCopy_612534828_VendaItemPagedList .btn-toggle-toolbar').live('click', function(){ 
                  var toolbar = pivot.getToolbarInstanceByPivotName('pivot913a276f-49fa-4d68-9f64-e3acfe76cbd4GraficosFlexMonsters_pivotCopy_612534828_VendaItemPagedList')
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
                return {'Ano': (isNullOrEmpty(item.Ano) ? '' : item.Ano.toString()), 'BigIntVendaItem': isNullOrEmpty(item.BigIntVendaItem) ? 0 : item.BigIntVendaItem, 'BitVendaItem': (isNullOrEmpty(item.BitVendaItem) ? '' : item.BitVendaItem.toString()), 'ComboboxVendaItem': (isNullOrEmpty(item.ComboboxVendaItemName) ? '' : item.ComboboxVendaItemName.toString()), 'DatetimeVendaItem': (isNullOrEmpty(item.DatetimeVendaItem) ? '' : Globalize.format(getUTCDate(item.DatetimeVendaItem), 'MM/dd/yyyy')), 'DecimalVendaItem': (isNullOrEmpty(item.DecimalVendaItem) ? '' : item.DecimalVendaItem.toString()), 'GuidVendaItem': (isNullOrEmpty(item.GuidVendaItem) ? '' : item.GuidVendaItem.toString()), 'IdVenda': (isNullOrEmpty(item.IdVenda) ? '' : item.IdVenda.toString()), 'IdVendaItem': (isNullOrEmpty(item.IdVendaItem) ? '' : item.IdVendaItem.toString()), 'IntVendaItem': (isNullOrEmpty(item.IntVendaItem) ? '' : item.IntVendaItem.toString()), 'Mes': (isNullOrEmpty(item.Mes) ? '' : item.Mes.toString()), 'SmallIntVendaItem': (isNullOrEmpty(item.SmallIntVendaItem) ? '' : item.SmallIntVendaItem.toString()), 'StringVendaItem': (isNullOrEmpty(item.StringVendaItem) ? '' : item.StringVendaItem.toString()), 'Valor': isNullOrEmpty(item.Valor) ? 0 : item.Valor};
            });
        
            var structure = {};
            structure.Ano = { type:'string', caption: 'Ano', dimensionUniqueName: 'ctrlCopy_612534828_VendaItemPagedList', dimensionCaption: ''  };
            structure.BigIntVendaItem = { type:'number', caption: 'Big Int Venda Item' }; 
            structure.BitVendaItem = { type:'string', caption: 'Bit Venda Item', dimensionUniqueName: 'ctrlCopy_612534828_VendaItemPagedList', dimensionCaption: ''  };
            structure.ComboboxVendaItem = { type:'string', caption: 'Combobox Venda Item', dimensionUniqueName: 'ctrlCopy_612534828_VendaItemPagedList', dimensionCaption: ''  };
            structure.DatetimeVendaItem = { type:'date string', caption: 'Datetime Venda Item', dimensionUniqueName: 'ctrlCopy_612534828_VendaItemPagedList', dimensionCaption: ''  };
            structure.DecimalVendaItem = { type:'string', caption: 'Decimal Venda Item', dimensionUniqueName: 'ctrlCopy_612534828_VendaItemPagedList', dimensionCaption: ''  };
            structure.GuidVendaItem = { type:'string', caption: 'Guid Venda Item', dimensionUniqueName: 'ctrlCopy_612534828_VendaItemPagedList', dimensionCaption: ''  };
            structure.IdVenda = { type:'string', caption: 'Id Venda', dimensionUniqueName: 'ctrlCopy_612534828_VendaItemPagedList', dimensionCaption: ''  };
            structure.IdVendaItem = { type:'string', caption: 'Id Venda Item', dimensionUniqueName: 'ctrlCopy_612534828_VendaItemPagedList', dimensionCaption: ''  };
            structure.IntVendaItem = { type:'string', caption: 'Int Venda Item', dimensionUniqueName: 'ctrlCopy_612534828_VendaItemPagedList', dimensionCaption: ''  };
            structure.Mes = { type:'string', caption: 'Mes', dimensionUniqueName: 'ctrlCopy_612534828_VendaItemPagedList', dimensionCaption: ''  };
            structure.SmallIntVendaItem = { type:'string', caption: 'Small Int Venda Item', dimensionUniqueName: 'ctrlCopy_612534828_VendaItemPagedList', dimensionCaption: ''  };
            structure.StringVendaItem = { type:'string', caption: 'String Venda Item', dimensionUniqueName: 'ctrlCopy_612534828_VendaItemPagedList', dimensionCaption: ''  };
            structure.Valor = { type:'number', caption: 'Valor' }; 
        if (pivotContext.report) {
            pivotContext.report.data = [structure].concat(data);
            pivot.setReport(pivotContext.report);
        } else {
            var report = {
                dataSourceType: 'json',
                data: [structure].concat(data),
                configuratorActive: false,
                viewType: 'grid_charts',
                chartType: 'scatter',
                chartPosition: 'left',
                showHeaders: false,
                fitGridlines: false,
                showGrandTotals: 'on',
                showChartsWarning: false,
                datePattern: 'dd/MM/yyyy',
                dateTimePattern: 'dd/MM/yyyy HH:mm:ss',
                rows: [{ uniqueName: 'Mes' }],
                columns: [{ uniqueName: '[Measures]' }],
                measures: [{ uniqueName: 'Valor' }],
            };
        
            pivot.setReport(report);
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
                name: 'Valor',
                current: pivot.getFormat('Valor')
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
                 name: 'Valor'
             }, 'Valor');
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
        var toolbarInstance = pivot.getToolbarInstanceByPivotName('pivot913a276f-49fa-4d68-9f64-e3acfe76cbd4GraficosFlexMonsters_pivotCopy_612534828_VendaItemPagedList');
        
             if (toolbarInstance) {
                 toolbarInstance.setPivotName('Copy_612534828_VendaItemPagedList');
                 toolbarInstance.setReportFiles(vm.layoutFiles);
                 toolbarInstance.setGetSelectedLayoutContent(vm.getDataContext().getSelectedLayoutContent);
                 toolbarInstance.setProjectName(vm.layoutFiles[0].projectName);
                 toolbarInstance.setViewName(vm.viewName);
                 pivot.prefixNameLayout = 'Copy_612534828_VendaItemPagedList';
                 pivot.pivotAdapterLayout = 'VendaItem';
             }
        
             vm.layoutFiles.forEach(function(file) {
                 if (file.selected && file.layoutFullName.indexOf('.xml') > 0 && file.layoutFullName.indexOf('Copy_612534828_VendaItemPagedList') > 0)
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
             listItems += '<li><i class="fa fa-angle-down btn-toggle-toolbar" title="expandir toolbar"></i></li>';
             $('#pivot913a276f-49fa-4d68-9f64-e3acfe76cbd4GraficosFlexMonsters_pivotCopy_612534828_VendaItemPagedList').append('<div class="plus-actions"><ul>'+ listItems +'</ul></div>');
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
             var height = '500';
             var type = 'html5';
             var withToolbar = true;
             var isLayoutToolbar = false;
             var path = managerAuth.META_ROOT + managerAuth.META_MODULE_ID + '/lib/flexmonster/';
             var containerId = 'pivot913a276f-49fa-4d68-9f64-e3acfe76cbd4GraficosFlexMonsters_pivotCopy_612534828_VendaItemPagedList';
        
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
    vm.addDataSource({ key: 'pivot913a276f-49fa-4d68-9f64-e3acfe76cbd4GraficosFlexMonsters_pivotCopy_612534828_VendaItemPagedList', name: 'dataView', itemsSource: itemsSource });
}

, renderpivot4327eb3b2c614bbebc853dcbba1bab52GraficosFlexMonsters_pivotCopy_612556078_VendaItemPagedList: function(vm) {
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
        if ($('#pivot4327eb3b-2c61-4bbe-bc85-3dcbba1bab52GraficosFlexMonsters_pivotCopy_612556078_VendaItemPagedList').is(':visible') && (currentStatus != vm.status() || currentPage != vm.dataToolbar.currentPage() || !commitData)) {
             currentStatus = vm.status();
             currentPage = vm.dataToolbar.currentPage();
             if (currentStatus && currentStatus.toLowerCase() == 'c') {
                  jEntitySearchPivotRelationship = '';
             }
        if(vm != null && vm.dataView != null ) {
             arrayData = unwrapObservableArray(vm.dataView, vm);
        }
        if(pivot == null) {
        $('#pivot4327eb3b-2c61-4bbe-bc85-3dcbba1bab52GraficosFlexMonsters_pivotCopy_612556078_VendaItemPagedList #fm-fields-view .fm-ui-btn:contains(\'OK\')')
             .live('mouseup', function () {
         });
        
        $('#pivot4327eb3b-2c61-4bbe-bc85-3dcbba1bab52GraficosFlexMonsters_pivotCopy_612556078_VendaItemPagedList #fm-toolbar-row .fm-ui-btn:contains(\'OK\')')
           .live('mouseup', function () {
               setTimeout(function () { filterPivotRelationship() }, 1);
        });
             $('#pivot4327eb3b-2c61-4bbe-bc85-3dcbba1bab52GraficosFlexMonsters_pivotCopy_612556078_VendaItemPagedList .btn-toggle-toolbar').die('click');
             $('#pivot4327eb3b-2c61-4bbe-bc85-3dcbba1bab52GraficosFlexMonsters_pivotCopy_612556078_VendaItemPagedList .btn-toggle-toolbar').live('click', function(){ 
                  var toolbar = pivot.getToolbarInstanceByPivotName('pivot4327eb3b-2c61-4bbe-bc85-3dcbba1bab52GraficosFlexMonsters_pivotCopy_612556078_VendaItemPagedList')
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
                return {'Ano': (isNullOrEmpty(item.Ano) ? '' : item.Ano.toString()), 'BigIntVendaItem': isNullOrEmpty(item.BigIntVendaItem) ? 0 : item.BigIntVendaItem, 'BitVendaItem': (isNullOrEmpty(item.BitVendaItem) ? '' : item.BitVendaItem.toString()), 'ComboboxVendaItem': (isNullOrEmpty(item.ComboboxVendaItemName) ? '' : item.ComboboxVendaItemName.toString()), 'DatetimeVendaItem': (isNullOrEmpty(item.DatetimeVendaItem) ? '' : Globalize.format(getUTCDate(item.DatetimeVendaItem), 'MM/dd/yyyy')), 'DecimalVendaItem': (isNullOrEmpty(item.DecimalVendaItem) ? '' : item.DecimalVendaItem.toString()), 'GuidVendaItem': (isNullOrEmpty(item.GuidVendaItem) ? '' : item.GuidVendaItem.toString()), 'IdVenda': (isNullOrEmpty(item.IdVenda) ? '' : item.IdVenda.toString()), 'IdVendaItem': (isNullOrEmpty(item.IdVendaItem) ? '' : item.IdVendaItem.toString()), 'IntVendaItem': (isNullOrEmpty(item.IntVendaItem) ? '' : item.IntVendaItem.toString()), 'Mes': (isNullOrEmpty(item.Mes) ? '' : item.Mes.toString()), 'SmallIntVendaItem': (isNullOrEmpty(item.SmallIntVendaItem) ? '' : item.SmallIntVendaItem.toString()), 'StringVendaItem': (isNullOrEmpty(item.StringVendaItem) ? '' : item.StringVendaItem.toString()), 'Valor': isNullOrEmpty(item.Valor) ? 0 : item.Valor};
            });
        
            var structure = {};
            structure.Ano = { type:'string', caption: 'Ano', dimensionUniqueName: 'ctrlCopy_612556078_VendaItemPagedList', dimensionCaption: ''  };
            structure.BigIntVendaItem = { type:'number', caption: 'Big Int Venda Item' }; 
            structure.BitVendaItem = { type:'string', caption: 'Bit Venda Item', dimensionUniqueName: 'ctrlCopy_612556078_VendaItemPagedList', dimensionCaption: ''  };
            structure.ComboboxVendaItem = { type:'string', caption: 'Combobox Venda Item', dimensionUniqueName: 'ctrlCopy_612556078_VendaItemPagedList', dimensionCaption: ''  };
            structure.DatetimeVendaItem = { type:'date string', caption: 'Datetime Venda Item', dimensionUniqueName: 'ctrlCopy_612556078_VendaItemPagedList', dimensionCaption: ''  };
            structure.DecimalVendaItem = { type:'string', caption: 'Decimal Venda Item', dimensionUniqueName: 'ctrlCopy_612556078_VendaItemPagedList', dimensionCaption: ''  };
            structure.GuidVendaItem = { type:'string', caption: 'Guid Venda Item', dimensionUniqueName: 'ctrlCopy_612556078_VendaItemPagedList', dimensionCaption: ''  };
            structure.IdVenda = { type:'string', caption: 'Id Venda', dimensionUniqueName: 'ctrlCopy_612556078_VendaItemPagedList', dimensionCaption: ''  };
            structure.IdVendaItem = { type:'string', caption: 'Id Venda Item', dimensionUniqueName: 'ctrlCopy_612556078_VendaItemPagedList', dimensionCaption: ''  };
            structure.IntVendaItem = { type:'string', caption: 'Int Venda Item', dimensionUniqueName: 'ctrlCopy_612556078_VendaItemPagedList', dimensionCaption: ''  };
            structure.Mes = { type:'string', caption: 'Mes', dimensionUniqueName: 'ctrlCopy_612556078_VendaItemPagedList', dimensionCaption: ''  };
            structure.SmallIntVendaItem = { type:'string', caption: 'Small Int Venda Item', dimensionUniqueName: 'ctrlCopy_612556078_VendaItemPagedList', dimensionCaption: ''  };
            structure.StringVendaItem = { type:'string', caption: 'String Venda Item', dimensionUniqueName: 'ctrlCopy_612556078_VendaItemPagedList', dimensionCaption: ''  };
            structure.Valor = { type:'number', caption: 'Valor' }; 
        if (pivotContext.report) {
            pivotContext.report.data = [structure].concat(data);
            pivot.setReport(pivotContext.report);
        } else {
            var report = {
                dataSourceType: 'json',
                data: [structure].concat(data),
                configuratorActive: false,
                viewType: 'grid_charts',
                chartType: 'pie',
                chartPosition: 'left',
                showHeaders: false,
                fitGridlines: false,
                showGrandTotals: 'on',
                showChartsWarning: false,
                datePattern: 'dd/MM/yyyy',
                dateTimePattern: 'dd/MM/yyyy HH:mm:ss',
                rows: [{ uniqueName: 'Mes' }],
                columns: [{ uniqueName: '[Measures]' }],
                measures: [{ uniqueName: 'Valor' }],
            };
        
            pivot.setReport(report);
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
                name: 'Valor',
                current: pivot.getFormat('Valor')
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
                 name: 'Valor'
             }, 'Valor');
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
        var toolbarInstance = pivot.getToolbarInstanceByPivotName('pivot4327eb3b-2c61-4bbe-bc85-3dcbba1bab52GraficosFlexMonsters_pivotCopy_612556078_VendaItemPagedList');
        
             if (toolbarInstance) {
                 toolbarInstance.setPivotName('Copy_612556078_VendaItemPagedList');
                 toolbarInstance.setReportFiles(vm.layoutFiles);
                 toolbarInstance.setGetSelectedLayoutContent(vm.getDataContext().getSelectedLayoutContent);
                 toolbarInstance.setProjectName(vm.layoutFiles[0].projectName);
                 toolbarInstance.setViewName(vm.viewName);
                 pivot.prefixNameLayout = 'Copy_612556078_VendaItemPagedList';
                 pivot.pivotAdapterLayout = 'VendaItem';
             }
        
             vm.layoutFiles.forEach(function(file) {
                 if (file.selected && file.layoutFullName.indexOf('.xml') > 0 && file.layoutFullName.indexOf('Copy_612556078_VendaItemPagedList') > 0)
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
             listItems += '<li><i class="fa fa-angle-down btn-toggle-toolbar" title="expandir toolbar"></i></li>';
             $('#pivot4327eb3b-2c61-4bbe-bc85-3dcbba1bab52GraficosFlexMonsters_pivotCopy_612556078_VendaItemPagedList').append('<div class="plus-actions"><ul>'+ listItems +'</ul></div>');
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
             var height = '500';
             var type = 'html5';
             var withToolbar = true;
             var isLayoutToolbar = false;
             var path = managerAuth.META_ROOT + managerAuth.META_MODULE_ID + '/lib/flexmonster/';
             var containerId = 'pivot4327eb3b-2c61-4bbe-bc85-3dcbba1bab52GraficosFlexMonsters_pivotCopy_612556078_VendaItemPagedList';
        
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
    vm.addDataSource({ key: 'pivot4327eb3b-2c61-4bbe-bc85-3dcbba1bab52GraficosFlexMonsters_pivotCopy_612556078_VendaItemPagedList', name: 'dataView', itemsSource: itemsSource });
}

, renderpivot7f7e3959de82442294e28ba60be5f421GraficosFlexMonsters_pivotCopy_612565250_VendaItemPagedList: function(vm) {
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
        if ($('#pivot7f7e3959-de82-4422-94e2-8ba60be5f421GraficosFlexMonsters_pivotCopy_612565250_VendaItemPagedList').is(':visible') && (currentStatus != vm.status() || currentPage != vm.dataToolbar.currentPage() || !commitData)) {
             currentStatus = vm.status();
             currentPage = vm.dataToolbar.currentPage();
             if (currentStatus && currentStatus.toLowerCase() == 'c') {
                  jEntitySearchPivotRelationship = '';
             }
        if(vm != null && vm.dataView != null ) {
             arrayData = unwrapObservableArray(vm.dataView, vm);
        }
        if(pivot == null) {
        $('#pivot7f7e3959-de82-4422-94e2-8ba60be5f421GraficosFlexMonsters_pivotCopy_612565250_VendaItemPagedList #fm-fields-view .fm-ui-btn:contains(\'OK\')')
             .live('mouseup', function () {
         });
        
        $('#pivot7f7e3959-de82-4422-94e2-8ba60be5f421GraficosFlexMonsters_pivotCopy_612565250_VendaItemPagedList #fm-toolbar-row .fm-ui-btn:contains(\'OK\')')
           .live('mouseup', function () {
               setTimeout(function () { filterPivotRelationship() }, 1);
        });
             $('#pivot7f7e3959-de82-4422-94e2-8ba60be5f421GraficosFlexMonsters_pivotCopy_612565250_VendaItemPagedList .btn-toggle-toolbar').die('click');
             $('#pivot7f7e3959-de82-4422-94e2-8ba60be5f421GraficosFlexMonsters_pivotCopy_612565250_VendaItemPagedList .btn-toggle-toolbar').live('click', function(){ 
                  var toolbar = pivot.getToolbarInstanceByPivotName('pivot7f7e3959-de82-4422-94e2-8ba60be5f421GraficosFlexMonsters_pivotCopy_612565250_VendaItemPagedList')
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
                return {'Ano': (isNullOrEmpty(item.Ano) ? '' : item.Ano.toString()), 'BigIntVendaItem': isNullOrEmpty(item.BigIntVendaItem) ? 0 : item.BigIntVendaItem, 'BitVendaItem': (isNullOrEmpty(item.BitVendaItem) ? '' : item.BitVendaItem.toString()), 'ComboboxVendaItem': (isNullOrEmpty(item.ComboboxVendaItemName) ? '' : item.ComboboxVendaItemName.toString()), 'DatetimeVendaItem': (isNullOrEmpty(item.DatetimeVendaItem) ? '' : Globalize.format(getUTCDate(item.DatetimeVendaItem), 'MM/dd/yyyy')), 'DecimalVendaItem': (isNullOrEmpty(item.DecimalVendaItem) ? '' : item.DecimalVendaItem.toString()), 'GuidVendaItem': (isNullOrEmpty(item.GuidVendaItem) ? '' : item.GuidVendaItem.toString()), 'IdVenda': (isNullOrEmpty(item.IdVenda) ? '' : item.IdVenda.toString()), 'IdVendaItem': (isNullOrEmpty(item.IdVendaItem) ? '' : item.IdVendaItem.toString()), 'IntVendaItem': (isNullOrEmpty(item.IntVendaItem) ? '' : item.IntVendaItem.toString()), 'Mes': (isNullOrEmpty(item.Mes) ? '' : item.Mes.toString()), 'SmallIntVendaItem': (isNullOrEmpty(item.SmallIntVendaItem) ? '' : item.SmallIntVendaItem.toString()), 'StringVendaItem': (isNullOrEmpty(item.StringVendaItem) ? '' : item.StringVendaItem.toString()), 'Valor': isNullOrEmpty(item.Valor) ? 0 : item.Valor};
            });
        
            var structure = {};
            structure.Ano = { type:'string', caption: 'Ano', dimensionUniqueName: 'ctrlCopy_612565250_VendaItemPagedList', dimensionCaption: ''  };
            structure.BigIntVendaItem = { type:'number', caption: 'Big Int Venda Item' }; 
            structure.BitVendaItem = { type:'string', caption: 'Bit Venda Item', dimensionUniqueName: 'ctrlCopy_612565250_VendaItemPagedList', dimensionCaption: ''  };
            structure.ComboboxVendaItem = { type:'string', caption: 'Combobox Venda Item', dimensionUniqueName: 'ctrlCopy_612565250_VendaItemPagedList', dimensionCaption: ''  };
            structure.DatetimeVendaItem = { type:'date string', caption: 'Datetime Venda Item', dimensionUniqueName: 'ctrlCopy_612565250_VendaItemPagedList', dimensionCaption: ''  };
            structure.DecimalVendaItem = { type:'string', caption: 'Decimal Venda Item', dimensionUniqueName: 'ctrlCopy_612565250_VendaItemPagedList', dimensionCaption: ''  };
            structure.GuidVendaItem = { type:'string', caption: 'Guid Venda Item', dimensionUniqueName: 'ctrlCopy_612565250_VendaItemPagedList', dimensionCaption: ''  };
            structure.IdVenda = { type:'string', caption: 'Id Venda', dimensionUniqueName: 'ctrlCopy_612565250_VendaItemPagedList', dimensionCaption: ''  };
            structure.IdVendaItem = { type:'string', caption: 'Id Venda Item', dimensionUniqueName: 'ctrlCopy_612565250_VendaItemPagedList', dimensionCaption: ''  };
            structure.IntVendaItem = { type:'string', caption: 'Int Venda Item', dimensionUniqueName: 'ctrlCopy_612565250_VendaItemPagedList', dimensionCaption: ''  };
            structure.Mes = { type:'string', caption: 'Mes', dimensionUniqueName: 'ctrlCopy_612565250_VendaItemPagedList', dimensionCaption: ''  };
            structure.SmallIntVendaItem = { type:'string', caption: 'Small Int Venda Item', dimensionUniqueName: 'ctrlCopy_612565250_VendaItemPagedList', dimensionCaption: ''  };
            structure.StringVendaItem = { type:'string', caption: 'String Venda Item', dimensionUniqueName: 'ctrlCopy_612565250_VendaItemPagedList', dimensionCaption: ''  };
            structure.Valor = { type:'number', caption: 'Valor' }; 
        if (pivotContext.report) {
            pivotContext.report.data = [structure].concat(data);
            pivot.setReport(pivotContext.report);
        } else {
            var report = {
                dataSourceType: 'json',
                data: [structure].concat(data),
                configuratorActive: false,
                viewType: 'grid_charts',
                chartType: 'bar_stack',
                chartPosition: 'left',
                showHeaders: false,
                fitGridlines: false,
                showGrandTotals: 'on',
                showChartsWarning: false,
                datePattern: 'dd/MM/yyyy',
                dateTimePattern: 'dd/MM/yyyy HH:mm:ss',
                rows: [{ uniqueName: 'Mes' }],
                columns: [{ uniqueName: 'Ano' }],
                measures: [{ uniqueName: 'Valor' }],
            };
               report.columns.push({uniqueName: '[Measures]'})
        
            pivot.setReport(report);
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
                name: 'Valor',
                current: pivot.getFormat('Valor')
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
                 name: 'Valor'
             }, 'Valor');
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
        var toolbarInstance = pivot.getToolbarInstanceByPivotName('pivot7f7e3959-de82-4422-94e2-8ba60be5f421GraficosFlexMonsters_pivotCopy_612565250_VendaItemPagedList');
        
             if (toolbarInstance) {
                 toolbarInstance.setPivotName('Copy_612565250_VendaItemPagedList');
                 toolbarInstance.setReportFiles(vm.layoutFiles);
                 toolbarInstance.setGetSelectedLayoutContent(vm.getDataContext().getSelectedLayoutContent);
                 toolbarInstance.setProjectName(vm.layoutFiles[0].projectName);
                 toolbarInstance.setViewName(vm.viewName);
                 pivot.prefixNameLayout = 'Copy_612565250_VendaItemPagedList';
                 pivot.pivotAdapterLayout = 'VendaItem';
             }
        
             vm.layoutFiles.forEach(function(file) {
                 if (file.selected && file.layoutFullName.indexOf('.xml') > 0 && file.layoutFullName.indexOf('Copy_612565250_VendaItemPagedList') > 0)
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
             listItems += '<li><i class="fa fa-angle-down btn-toggle-toolbar" title="expandir toolbar"></i></li>';
             $('#pivot7f7e3959-de82-4422-94e2-8ba60be5f421GraficosFlexMonsters_pivotCopy_612565250_VendaItemPagedList').append('<div class="plus-actions"><ul>'+ listItems +'</ul></div>');
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
             var height = '500';
             var type = 'html5';
             var withToolbar = true;
             var isLayoutToolbar = false;
             var path = managerAuth.META_ROOT + managerAuth.META_MODULE_ID + '/lib/flexmonster/';
             var containerId = 'pivot7f7e3959-de82-4422-94e2-8ba60be5f421GraficosFlexMonsters_pivotCopy_612565250_VendaItemPagedList';
        
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
    vm.addDataSource({ key: 'pivot7f7e3959-de82-4422-94e2-8ba60be5f421GraficosFlexMonsters_pivotCopy_612565250_VendaItemPagedList', name: 'dataView', itemsSource: itemsSource });
}

, renderpivot683bc16d4adc4b5a851cdbbd555495abGraficosFlexMonsters_pivotCopy_612581937_VendaItemPagedList: function(vm) {
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
        if ($('#pivot683bc16d-4adc-4b5a-851c-dbbd555495abGraficosFlexMonsters_pivotCopy_612581937_VendaItemPagedList').is(':visible') && (currentStatus != vm.status() || currentPage != vm.dataToolbar.currentPage() || !commitData)) {
             currentStatus = vm.status();
             currentPage = vm.dataToolbar.currentPage();
             if (currentStatus && currentStatus.toLowerCase() == 'c') {
                  jEntitySearchPivotRelationship = '';
             }
        if(vm != null && vm.dataView != null ) {
             arrayData = unwrapObservableArray(vm.dataView, vm);
        }
        if(pivot == null) {
        $('#pivot683bc16d-4adc-4b5a-851c-dbbd555495abGraficosFlexMonsters_pivotCopy_612581937_VendaItemPagedList #fm-fields-view .fm-ui-btn:contains(\'OK\')')
             .live('mouseup', function () {
         });
        
        $('#pivot683bc16d-4adc-4b5a-851c-dbbd555495abGraficosFlexMonsters_pivotCopy_612581937_VendaItemPagedList #fm-toolbar-row .fm-ui-btn:contains(\'OK\')')
           .live('mouseup', function () {
               setTimeout(function () { filterPivotRelationship() }, 1);
        });
             $('#pivot683bc16d-4adc-4b5a-851c-dbbd555495abGraficosFlexMonsters_pivotCopy_612581937_VendaItemPagedList .btn-toggle-toolbar').die('click');
             $('#pivot683bc16d-4adc-4b5a-851c-dbbd555495abGraficosFlexMonsters_pivotCopy_612581937_VendaItemPagedList .btn-toggle-toolbar').live('click', function(){ 
                  var toolbar = pivot.getToolbarInstanceByPivotName('pivot683bc16d-4adc-4b5a-851c-dbbd555495abGraficosFlexMonsters_pivotCopy_612581937_VendaItemPagedList')
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
                return {'Ano': (isNullOrEmpty(item.Ano) ? '' : item.Ano.toString()), 'BigIntVendaItem': isNullOrEmpty(item.BigIntVendaItem) ? 0 : item.BigIntVendaItem, 'BitVendaItem': (isNullOrEmpty(item.BitVendaItem) ? '' : item.BitVendaItem.toString()), 'ComboboxVendaItem': (isNullOrEmpty(item.ComboboxVendaItemName) ? '' : item.ComboboxVendaItemName.toString()), 'DatetimeVendaItem': (isNullOrEmpty(item.DatetimeVendaItem) ? '' : Globalize.format(getUTCDate(item.DatetimeVendaItem), 'MM/dd/yyyy')), 'DecimalVendaItem': (isNullOrEmpty(item.DecimalVendaItem) ? '' : item.DecimalVendaItem.toString()), 'GuidVendaItem': (isNullOrEmpty(item.GuidVendaItem) ? '' : item.GuidVendaItem.toString()), 'IdVenda': (isNullOrEmpty(item.IdVenda) ? '' : item.IdVenda.toString()), 'IdVendaItem': (isNullOrEmpty(item.IdVendaItem) ? '' : item.IdVendaItem.toString()), 'IntVendaItem': (isNullOrEmpty(item.IntVendaItem) ? '' : item.IntVendaItem.toString()), 'Mes': (isNullOrEmpty(item.Mes) ? '' : item.Mes.toString()), 'SmallIntVendaItem': (isNullOrEmpty(item.SmallIntVendaItem) ? '' : item.SmallIntVendaItem.toString()), 'StringVendaItem': (isNullOrEmpty(item.StringVendaItem) ? '' : item.StringVendaItem.toString()), 'Valor': isNullOrEmpty(item.Valor) ? 0 : item.Valor};
            });
        
            var structure = {};
            structure.Ano = { type:'string', caption: 'Ano', dimensionUniqueName: 'ctrlCopy_612581937_VendaItemPagedList', dimensionCaption: ''  };
            structure.BigIntVendaItem = { type:'number', caption: 'Big Int Venda Item' }; 
            structure.BitVendaItem = { type:'string', caption: 'Bit Venda Item', dimensionUniqueName: 'ctrlCopy_612581937_VendaItemPagedList', dimensionCaption: ''  };
            structure.ComboboxVendaItem = { type:'string', caption: 'Combobox Venda Item', dimensionUniqueName: 'ctrlCopy_612581937_VendaItemPagedList', dimensionCaption: ''  };
            structure.DatetimeVendaItem = { type:'date string', caption: 'Datetime Venda Item', dimensionUniqueName: 'ctrlCopy_612581937_VendaItemPagedList', dimensionCaption: ''  };
            structure.DecimalVendaItem = { type:'string', caption: 'Decimal Venda Item', dimensionUniqueName: 'ctrlCopy_612581937_VendaItemPagedList', dimensionCaption: ''  };
            structure.GuidVendaItem = { type:'string', caption: 'Guid Venda Item', dimensionUniqueName: 'ctrlCopy_612581937_VendaItemPagedList', dimensionCaption: ''  };
            structure.IdVenda = { type:'string', caption: 'Id Venda', dimensionUniqueName: 'ctrlCopy_612581937_VendaItemPagedList', dimensionCaption: ''  };
            structure.IdVendaItem = { type:'string', caption: 'Id Venda Item', dimensionUniqueName: 'ctrlCopy_612581937_VendaItemPagedList', dimensionCaption: ''  };
            structure.IntVendaItem = { type:'string', caption: 'Int Venda Item', dimensionUniqueName: 'ctrlCopy_612581937_VendaItemPagedList', dimensionCaption: ''  };
            structure.Mes = { type:'string', caption: 'Mes', dimensionUniqueName: 'ctrlCopy_612581937_VendaItemPagedList', dimensionCaption: ''  };
            structure.SmallIntVendaItem = { type:'string', caption: 'Small Int Venda Item', dimensionUniqueName: 'ctrlCopy_612581937_VendaItemPagedList', dimensionCaption: ''  };
            structure.StringVendaItem = { type:'string', caption: 'String Venda Item', dimensionUniqueName: 'ctrlCopy_612581937_VendaItemPagedList', dimensionCaption: ''  };
            structure.Valor = { type:'number', caption: 'Valor' }; 
        if (pivotContext.report) {
            pivotContext.report.data = [structure].concat(data);
            pivot.setReport(pivotContext.report);
        } else {
            var report = {
                dataSourceType: 'json',
                data: [structure].concat(data),
                configuratorActive: false,
                viewType: 'grid_charts',
                chartType: 'bar_line',
                chartPosition: 'left',
                showHeaders: false,
                fitGridlines: false,
                showGrandTotals: 'on',
                showChartsWarning: false,
                datePattern: 'dd/MM/yyyy',
                dateTimePattern: 'dd/MM/yyyy HH:mm:ss',
                rows: [{ uniqueName: 'Mes' }],
                columns: [{ uniqueName: '[Measures]' }],
                measures: [
                     { uniqueName: 'BigIntVendaItem' },
                     { uniqueName: 'Valor' },
                ],
            };
        
            pivot.setReport(report);
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
                name: 'Valor',
                current: pivot.getFormat('Valor')
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
                 name: 'Valor'
             }, 'Valor');
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
        var toolbarInstance = pivot.getToolbarInstanceByPivotName('pivot683bc16d-4adc-4b5a-851c-dbbd555495abGraficosFlexMonsters_pivotCopy_612581937_VendaItemPagedList');
        
             if (toolbarInstance) {
                 toolbarInstance.setPivotName('Copy_612581937_VendaItemPagedList');
                 toolbarInstance.setReportFiles(vm.layoutFiles);
                 toolbarInstance.setGetSelectedLayoutContent(vm.getDataContext().getSelectedLayoutContent);
                 toolbarInstance.setProjectName(vm.layoutFiles[0].projectName);
                 toolbarInstance.setViewName(vm.viewName);
                 pivot.prefixNameLayout = 'Copy_612581937_VendaItemPagedList';
                 pivot.pivotAdapterLayout = 'VendaItem';
             }
        
             vm.layoutFiles.forEach(function(file) {
                 if (file.selected && file.layoutFullName.indexOf('.xml') > 0 && file.layoutFullName.indexOf('Copy_612581937_VendaItemPagedList') > 0)
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
             listItems += '<li><i class="fa fa-angle-down btn-toggle-toolbar" title="expandir toolbar"></i></li>';
             $('#pivot683bc16d-4adc-4b5a-851c-dbbd555495abGraficosFlexMonsters_pivotCopy_612581937_VendaItemPagedList').append('<div class="plus-actions"><ul>'+ listItems +'</ul></div>');
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
             var height = '500';
             var type = 'html5';
             var withToolbar = true;
             var isLayoutToolbar = false;
             var path = managerAuth.META_ROOT + managerAuth.META_MODULE_ID + '/lib/flexmonster/';
             var containerId = 'pivot683bc16d-4adc-4b5a-851c-dbbd555495abGraficosFlexMonsters_pivotCopy_612581937_VendaItemPagedList';
        
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
    vm.addDataSource({ key: 'pivot683bc16d-4adc-4b5a-851c-dbbd555495abGraficosFlexMonsters_pivotCopy_612581937_VendaItemPagedList', name: 'dataView', itemsSource: itemsSource });
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
