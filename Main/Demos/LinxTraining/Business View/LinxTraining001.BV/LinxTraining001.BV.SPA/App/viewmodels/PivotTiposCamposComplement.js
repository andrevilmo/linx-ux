define(['managers/__auth'], function (managerAuth) {
var complementCtor = function() {
    var complement = {
    isAutomatic: true
    , renderPivotTiposCampos_pivotTiposCamposView: function(vm) {
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
        if ($('#PivotTiposCampos_pivotTiposCamposView').is(':visible') && (currentStatus != vm.status() || currentPage != vm.dataToolbar.currentPage() || !commitData) ) {
             currentStatus = vm.status();
             currentPage = vm.dataToolbar.currentPage();
             if (currentStatus && currentStatus.toLowerCase() == 'c') {
                  jEntitySearchPivotRelationship = '';
             }
        if(vm != null && vm.dataView != null ) {
             arrayData = unwrapObservableArray(vm.dataView, vm);
        }
        if(pivot == null) {
        $('#PivotTiposCampos_pivotTiposCamposView #fm-fields-view .fm-ui-btn:contains(\'OK\')')
             .live('mouseup', function () {
         });
        
        $('#PivotTiposCampos_pivotTiposCamposView #fm-toolbar-row .fm-ui-btn:contains(\'OK\')')
           .live('mouseup', function () {
               setTimeout(function () { filterPivotRelationship() }, 1);
        });
             $('#PivotTiposCampos_pivotTiposCamposView .btn-toggle-toolbar').die('click');
             $('#PivotTiposCampos_pivotTiposCamposView .btn-toggle-toolbar').live('click', function(){ 
                  var toolbar = pivot.getToolbarInstanceByPivotName('PivotTiposCampos_pivotTiposCamposView')
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
                return {'Boolean': (isNullOrEmpty(item.Boolean) ? '' : item.Boolean.toString()), 'Byte': (isNullOrEmpty(item.Byte) ? '' : item.Byte.toString()), 'DateTime': (isNullOrEmpty(item.DateTime) ? '' : Globalize.format(getUTCDate(item.DateTime), 'MM/dd/yyyy')), 'Decimal': (isNullOrEmpty(item.Decimal) ? '' : item.Decimal.toString()), 'IDTiposCampos': (isNullOrEmpty(item.IDTiposCampos) ? '' : item.IDTiposCampos.toString()), 'Int': (isNullOrEmpty(item.Int) ? '' : item.Int.toString()), 'Long': (isNullOrEmpty(item.Long) ? '' : item.Long.toString()), 'Short': (isNullOrEmpty(item.Short) ? '' : item.Short.toString()), 'String': (isNullOrEmpty(item.String) ? '' : item.String.toString()), 'StringChar': (isNullOrEmpty(item.StringChar) ? '' : item.StringChar.toString()), 'StringText': (isNullOrEmpty(item.StringText) ? '' : item.StringText.toString())};
            });
        
            var structure = {};
            structure.Boolean = { type:'string', caption: 'Boolean', dimensionUniqueName: 'ctrlTiposCamposView', dimensionCaption: 'FlatPivotGrid'  };
            structure.Byte = { type:'string', caption: 'Byte', dimensionUniqueName: 'ctrlTiposCamposView', dimensionCaption: 'FlatPivotGrid'  };
            structure.DateTime = { type:'date string', caption: 'DateTime', dimensionUniqueName: 'ctrlTiposCamposView', dimensionCaption: 'FlatPivotGrid'  };
            structure.Decimal = { type:'string', caption: 'Decimal', dimensionUniqueName: 'ctrlTiposCamposView', dimensionCaption: 'FlatPivotGrid'  };
            structure.IDTiposCampos = { type:'string', caption: 'ID TiposCampos', dimensionUniqueName: 'ctrlTiposCamposView', dimensionCaption: 'FlatPivotGrid'  };
            structure.Int = { type:'string', caption: 'Int', dimensionUniqueName: 'ctrlTiposCamposView', dimensionCaption: 'FlatPivotGrid'  };
            structure.Long = { type:'string', caption: 'Long', dimensionUniqueName: 'ctrlTiposCamposView', dimensionCaption: 'FlatPivotGrid'  };
            structure.Short = { type:'string', caption: 'Short', dimensionUniqueName: 'ctrlTiposCamposView', dimensionCaption: 'FlatPivotGrid'  };
            structure.String = { type:'string', caption: 'String', dimensionUniqueName: 'ctrlTiposCamposView', dimensionCaption: 'FlatPivotGrid'  };
            structure.StringChar = { type:'string', caption: 'StringChar', dimensionUniqueName: 'ctrlTiposCamposView', dimensionCaption: 'FlatPivotGrid'  };
            structure.StringText = { type:'string', caption: 'StringText', dimensionUniqueName: 'ctrlTiposCamposView', dimensionCaption: 'FlatPivotGrid'  };
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
                showChartsWarning: false,
                datePattern: 'dd/MM/yyyy',
                dateTimePattern: 'dd/MM/yyyy HH:mm:ss'
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
                            values: [item.caption],
                        });
                });
        }
        
        var setFilterByReport = function (filters, reportItem) {
            if (reportItem.length)
                reportItem.forEach(function (item) {
                    if (item.filter && item.filter.members.length && !filters.some(function (filter) { return item.uniqueName == filter.key })) {
                        var currentItem = {
                            values: [],
                            key: item.uniqueName,
                            negation: item.filter.negation,
                        };
                        item.filter.members.forEach(function (filter) {
                            var value = filter.split('.')[1].replace('[', '').replace(']', '');
                            currentItem.values.push(value);
                        });
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
                    var operator = (item.negation) ? '!=' : '==';
                    if (item.values.length > 1)
                        operator = (item.negation) ? '!In' : 'In';
                    jEntitySearch += '&&#' + item.key + '#' + operator + '#S';
                    item.values.forEach(function (value) {
                        jEntitySearch += value + ',';
                    });
                    jEntitySearch = jEntitySearch.substring(0, jEntitySearch.lastIndexOf(','));
                });
            return jEntitySearch;
        };
        
        var filterPivotRelationship = function (cell) {
           if ((cell && cell.type != 'value') || isNaN(cell.value) || cell.isTotal) return false;
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
        var toolbarInstance = pivot.getToolbarInstanceByPivotName('PivotTiposCampos_pivotTiposCamposView');
        
             if (toolbarInstance) {
                 toolbarInstance.setPivotName('TiposCamposView');
                 toolbarInstance.setReportFiles(vm.layoutFiles);
                 toolbarInstance.setGetSelectedLayoutContent(vm.getDataContext().getSelectedLayoutContent);
                 toolbarInstance.setProjectName(vm.layoutFiles[0].projectName);
                 pivot.prefixNameLayout = 'TiposCamposView';
                 pivot.pivotAdapterLayout = 'TiposCamposView';
             }
        
             vm.layoutFiles.forEach(function(file) {
                 if (file.selected && file.layoutFullName.indexOf('.xml') > 0 && file.layoutFullName.indexOf('TiposCamposView') > 0)
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
             $('#PivotTiposCampos_pivotTiposCamposView').append('<div class="plus-actions"><ul>'+ listItems +'</ul></div>');
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
             var containerId = 'PivotTiposCampos_pivotTiposCamposView';
        
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
    vm.addDataSource({ key: 'PivotTiposCampos_pivotTiposCamposView', name: 'dataView', itemsSource: itemsSource });
}

, renderPivotTiposCampos_pivotTiposCamposFilhaView: function(vm) {
    var pivot = null;
    var arrayData = [];
    var currentStatus = '';
    var currentPage = undefined;
    var app = require('durandal/app');
    var jEntitySearchPivotRelationship = '';
    var dataSourceIsLoaded = function() {
        var isLoaded = false;
        try {
            isLoaded = (vm.currentDataItem().TiposCamposFilhaViewIsLoaded === true || vm.currentDataItem().TiposCamposFilhaViewList().length > 0);
        }
        catch (e) {
            isLoaded = true;
        }
        return isLoaded;
    }
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
        if ($('#PivotTiposCampos_pivotTiposCamposFilhaView').is(':visible') && (currentStatus != vm.status() || currentPage != vm.dataToolbar.currentPage() || !commitData) ) {
             currentStatus = vm.status();
             currentPage = vm.dataToolbar.currentPage();
             if (currentStatus && currentStatus.toLowerCase() == 'c') {
                  jEntitySearchPivotRelationship = '';
             }
           if ((vm.status() != 'C' && vm.status() != 'I') && !dataSourceIsLoaded()) {
             vm.currentDataItem().fillDetails(false, 'TiposCamposFilhaView');
             return;
           }
        if(vm != null && vm.currentDataItem() != null && vm.currentDataItem().TiposCamposFilhaViewList != null ) {
             arrayData = unwrapObservableArray(vm.currentDataItem().TiposCamposFilhaViewList, vm);
        }
        if(pivot == null) {
        $('#PivotTiposCampos_pivotTiposCamposFilhaView #fm-fields-view .fm-ui-btn:contains(\'OK\')')
             .live('mouseup', function () {
         });
        
        $('#PivotTiposCampos_pivotTiposCamposFilhaView #fm-toolbar-row .fm-ui-btn:contains(\'OK\')')
           .live('mouseup', function () {
               setTimeout(function () { filterPivotRelationship() }, 1);
        });
             $('#PivotTiposCampos_pivotTiposCamposFilhaView .btn-toggle-toolbar').die('click');
             $('#PivotTiposCampos_pivotTiposCamposFilhaView .btn-toggle-toolbar').live('click', function(){ 
                  var toolbar = pivot.getToolbarInstanceByPivotName('PivotTiposCampos_pivotTiposCamposFilhaView')
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
                return {'Boolean': (isNullOrEmpty(item.Boolean) ? '' : item.Boolean.toString()), 'DateTime': (isNullOrEmpty(item.DateTime) ? '' : Globalize.format(getUTCDate(item.DateTime), 'MM/dd/yyyy')), 'Decimal': (isNullOrEmpty(item.Decimal) ? '' : item.Decimal.toString()), 'IDTiposCampos': (isNullOrEmpty(item.IDTiposCampos) ? '' : item.IDTiposCampos.toString()), 'IDTiposCamposFilha': (isNullOrEmpty(item.IDTiposCamposFilha) ? '' : item.IDTiposCamposFilha.toString()), 'Int': (isNullOrEmpty(item.Int) ? '' : item.Int.toString()), 'Long': (isNullOrEmpty(item.Long) ? '' : item.Long.toString()), 'Short': (isNullOrEmpty(item.Short) ? '' : item.Short.toString()), 'String': (isNullOrEmpty(item.String) ? '' : item.String.toString()), 'StringChar': (isNullOrEmpty(item.StringChar) ? '' : item.StringChar.toString()), 'StringText': (isNullOrEmpty(item.StringText) ? '' : item.StringText.toString())};
            });
        
            var structure = {};
            structure.Boolean = { type:'string', caption: 'Boolean', dimensionUniqueName: 'ctrlTiposCamposFilhaView', dimensionCaption: 'FlatPivotGrid'  };
            structure.DateTime = { type:'date string', caption: 'DateTime', dimensionUniqueName: 'ctrlTiposCamposFilhaView', dimensionCaption: 'FlatPivotGrid'  };
            structure.Decimal = { type:'string', caption: 'Decimal', dimensionUniqueName: 'ctrlTiposCamposFilhaView', dimensionCaption: 'FlatPivotGrid'  };
            structure.IDTiposCampos = { type:'string', caption: 'ID TiposCampos', dimensionUniqueName: 'ctrlTiposCamposFilhaView', dimensionCaption: 'FlatPivotGrid'  };
            structure.IDTiposCamposFilha = { type:'string', caption: 'ID TiposCamposFilha', dimensionUniqueName: 'ctrlTiposCamposFilhaView', dimensionCaption: 'FlatPivotGrid'  };
            structure.Int = { type:'string', caption: 'Int', dimensionUniqueName: 'ctrlTiposCamposFilhaView', dimensionCaption: 'FlatPivotGrid'  };
            structure.Long = { type:'string', caption: 'Long', dimensionUniqueName: 'ctrlTiposCamposFilhaView', dimensionCaption: 'FlatPivotGrid'  };
            structure.Short = { type:'string', caption: 'Short', dimensionUniqueName: 'ctrlTiposCamposFilhaView', dimensionCaption: 'FlatPivotGrid'  };
            structure.String = { type:'string', caption: 'String', dimensionUniqueName: 'ctrlTiposCamposFilhaView', dimensionCaption: 'FlatPivotGrid'  };
            structure.StringChar = { type:'string', caption: 'StringChar', dimensionUniqueName: 'ctrlTiposCamposFilhaView', dimensionCaption: 'FlatPivotGrid'  };
            structure.StringText = { type:'string', caption: 'StringText', dimensionUniqueName: 'ctrlTiposCamposFilhaView', dimensionCaption: 'FlatPivotGrid'  };
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
                showChartsWarning: false,
                datePattern: 'dd/MM/yyyy',
                dateTimePattern: 'dd/MM/yyyy HH:mm:ss'
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
                            values: [item.caption],
                        });
                });
        }
        
        var setFilterByReport = function (filters, reportItem) {
            if (reportItem.length)
                reportItem.forEach(function (item) {
                    if (item.filter && item.filter.members.length && !filters.some(function (filter) { return item.uniqueName == filter.key })) {
                        var currentItem = {
                            values: [],
                            key: item.uniqueName,
                            negation: item.filter.negation,
                        };
                        item.filter.members.forEach(function (filter) {
                            var value = filter.split('.')[1].replace('[', '').replace(']', '');
                            currentItem.values.push(value);
                        });
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
                    var operator = (item.negation) ? '!=' : '==';
                    if (item.values.length > 1)
                        operator = (item.negation) ? '!In' : 'In';
                    jEntitySearch += '&&#' + item.key + '#' + operator + '#S';
                    item.values.forEach(function (value) {
                        jEntitySearch += value + ',';
                    });
                    jEntitySearch = jEntitySearch.substring(0, jEntitySearch.lastIndexOf(','));
                });
            return jEntitySearch;
        };
        
        var filterPivotRelationship = function (cell) {
           if ((cell && cell.type != 'value') || isNaN(cell.value) || cell.isTotal) return false;
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
        var toolbarInstance = pivot.getToolbarInstanceByPivotName('PivotTiposCampos_pivotTiposCamposFilhaView');
        
             if (toolbarInstance) {
                 toolbarInstance.setPivotName('TiposCamposFilhaView');
                 toolbarInstance.setReportFiles(vm.layoutFiles);
                 toolbarInstance.setGetSelectedLayoutContent(vm.getDataContext().getSelectedLayoutContent);
                 toolbarInstance.setProjectName(vm.layoutFiles[0].projectName);
                 pivot.prefixNameLayout = 'TiposCamposFilhaView';
                 pivot.pivotAdapterLayout = 'TiposCamposFilhaView';
             }
        
             vm.layoutFiles.forEach(function(file) {
                 if (file.selected && file.layoutFullName.indexOf('.xml') > 0 && file.layoutFullName.indexOf('TiposCamposFilhaView') > 0)
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
             $('#PivotTiposCampos_pivotTiposCamposFilhaView').append('<div class="plus-actions"><ul>'+ listItems +'</ul></div>');
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
             var containerId = 'PivotTiposCampos_pivotTiposCamposFilhaView';
        
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
    vm.addDataSource({ key: 'PivotTiposCampos_pivotTiposCamposFilhaView', name: 'TiposCamposFilhaViewList', itemsSource: itemsSource });
}


    };
    return complement;
}

return complementCtor;
});
