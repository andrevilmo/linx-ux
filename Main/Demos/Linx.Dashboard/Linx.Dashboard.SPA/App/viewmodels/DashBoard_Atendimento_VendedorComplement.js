define(['managers/__auth'], function (managerAuth) {
var complementCtor = function() {
    var complement = {
    isAutomatic: true
    , renderpivota0dbc8b2328b4924b8df255a721b6bedDashBoard_Atendimento_Vendedor_pivotLjvAtendimentoVendedor: function(vm) {
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
        if ($('#pivota0dbc8b2-328b-4924-b8df-255a721b6bedDashBoard_Atendimento_Vendedor_pivotLjvAtendimentoVendedor').is(':visible') && (currentStatus != vm.status() || currentPage != vm.dataToolbar.currentPage() || !commitData)) {
             currentStatus = vm.status();
             currentPage = vm.dataToolbar.currentPage();
             if (currentStatus && currentStatus.toLowerCase() == 'c') {
                  jEntitySearchPivotRelationship = '';
             }
        if(vm != null && vm.dataView != null ) {
             arrayData = unwrapObservableArray(vm.dataView, vm);
        }
        if(pivot == null) {
        $('#pivota0dbc8b2-328b-4924-b8df-255a721b6bedDashBoard_Atendimento_Vendedor_pivotLjvAtendimentoVendedor #fm-fields-view .fm-ui-btn:contains(\'OK\')')
             .live('mouseup', function () {
         });
        
        $('#pivota0dbc8b2-328b-4924-b8df-255a721b6bedDashBoard_Atendimento_Vendedor_pivotLjvAtendimentoVendedor #fm-toolbar-row .fm-ui-btn:contains(\'OK\')')
           .live('mouseup', function () {
               setTimeout(function () { filterPivotRelationship() }, 1);
        });
             $('#pivota0dbc8b2-328b-4924-b8df-255a721b6bedDashBoard_Atendimento_Vendedor_pivotLjvAtendimentoVendedor .btn-toggle-toolbar').die('click');
             $('#pivota0dbc8b2-328b-4924-b8df-255a721b6bedDashBoard_Atendimento_Vendedor_pivotLjvAtendimentoVendedor .btn-toggle-toolbar').live('click', function(){ 
                  var toolbar = pivot.getToolbarInstanceByPivotName('pivota0dbc8b2-328b-4924-b8df-255a721b6bedDashBoard_Atendimento_Vendedor_pivotLjvAtendimentoVendedor')
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
                return {'DataAtendimento': (isNullOrEmpty(item.DataAtendimento) ? '' : Globalize.format(getUTCDate(item.DataAtendimento), 'MM/dd/yyyy')), 'IdAtendimento': (isNullOrEmpty(item.IdAtendimento) ? '' : item.IdAtendimento.toString()), 'NomeVendedor': (isNullOrEmpty(item.NomeVendedor) ? '' : item.NomeVendedor.toString()), 'VendedorApelido': (isNullOrEmpty(item.VendedorApelido) ? '' : item.VendedorApelido.toString()), 'ValorCupomFiscal': isNullOrEmpty(item.ValorCupomFiscal) ? 0 : item.ValorCupomFiscal, 'ValorComissao': isNullOrEmpty(item.ValorComissao) ? 0 : item.ValorComissao};
            });
        
            var structure = {};
            structure.DataAtendimento = { type:'date string', caption: 'Data Atendimento', dimensionUniqueName: 'ctrlLjvAtendimentoVendedor', dimensionCaption: 'Atendimento Vendedor'  };
            structure.IdAtendimento = { type:'string', caption: 'Qtde. Atendimento', dimensionUniqueName: 'ctrlLjvAtendimentoVendedor', dimensionCaption: 'Atendimento Vendedor'  };
            structure.NomeVendedor = { type:'string', caption: 'Nome Vendedor', dimensionUniqueName: 'ctrlLjvAtendimentoVendedor', dimensionCaption: 'Atendimento Vendedor'  };
            structure.VendedorApelido = { type:'string', caption: 'Vendedor Apelido', dimensionUniqueName: 'ctrlLjvAtendimentoVendedor', dimensionCaption: 'Atendimento Vendedor'  };
            structure.ValorCupomFiscal = { type:'number', caption: 'Valor Cupom Fiscal' }; 
            structure.ValorComissao = { type:'number', caption: 'Valor Comissao' }; 
        if (pivotContext.report) {
            pivotContext.report.data = [structure].concat(data);
            pivot.setReport(pivotContext.report);
        } else {
            var report = {
                dataSourceType: 'json',
                data: [structure].concat(data),
                configuratorActive: false,
                viewType: 'charts',
                chartType: 'pie',
                showHeaders: false,
                fitGridlines: false,
                showGrandTotals: 'on',
                showChartsWarning: false,
                datePattern: 'dd/MM/yyyy',
                dateTimePattern: 'dd/MM/yyyy HH:mm:ss',
                rows: [{ uniqueName: 'NomeVendedor' }],
                columns: [{ uniqueName: '[Measures]' }],
                measures: [{ uniqueName: 'IdAtendimento' }],
            };
        
            pivot.setReport(report);
            pivot.setTopX("NomeVendedor", 10, "IdAtendimento");
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
                name: 'ValorCupomFiscal',
                current: pivot.getFormat('ValorCupomFiscal')
            });
        
            formatMeasures.push({
                name: 'ValorComissao',
                current: pivot.getFormat('ValorComissao')
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
                 currencySymbol: 'R$ ',
                 decimalPlaces : '2',
                 decimalSeparator: ',',
                 thousandsSeparator: '.',
                 name: 'ValorCupomFiscal'
             }, 'ValorCupomFiscal');
             pivot.setFormat({
                 currencySymbol: 'R$ ',
                 decimalPlaces : '2',
                 decimalSeparator: ',',
                 thousandsSeparator: '.',
                 name: 'ValorComissao'
             }, 'ValorComissao');
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
        var toolbarInstance = pivot.getToolbarInstanceByPivotName('pivota0dbc8b2-328b-4924-b8df-255a721b6bedDashBoard_Atendimento_Vendedor_pivotLjvAtendimentoVendedor');
        
             if (toolbarInstance) {
                 toolbarInstance.setPivotName('LjvAtendimentoVendedor');
                 toolbarInstance.setReportFiles(vm.layoutFiles);
                 toolbarInstance.setGetSelectedLayoutContent(vm.getDataContext().getSelectedLayoutContent);
                 toolbarInstance.setProjectName(vm.layoutFiles[0].projectName);
                 toolbarInstance.setViewName(vm.viewName);
                 pivot.prefixNameLayout = 'LjvAtendimentoVendedor';
                 pivot.pivotAdapterLayout = 'LjvAtendimentoVendedor';
             }
        
             vm.layoutFiles.forEach(function(file) {
                 if (file.selected && file.layoutFullName.indexOf('.xml') > 0 && file.layoutFullName.indexOf('LjvAtendimentoVendedor') > 0)
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
             $('#pivota0dbc8b2-328b-4924-b8df-255a721b6bedDashBoard_Atendimento_Vendedor_pivotLjvAtendimentoVendedor').append('<div class="plus-actions"><ul>'+ listItems +'</ul></div>');
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
             var containerId = 'pivota0dbc8b2-328b-4924-b8df-255a721b6bedDashBoard_Atendimento_Vendedor_pivotLjvAtendimentoVendedor';
        
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
    vm.addDataSource({ key: 'pivota0dbc8b2-328b-4924-b8df-255a721b6bedDashBoard_Atendimento_Vendedor_pivotLjvAtendimentoVendedor', name: 'dataView', itemsSource: itemsSource });
}


    };
    return complement;
}

return complementCtor;
});
