define(['managers/__auth'], function (managerAuth) {
var complementCtor = function() {
    var complement = {
    isAutomatic: true
    , renderpivotc838a61b68234f13922bce5061094d01ViewTestePivot_pivotLjvAtendimento: function(vm) {
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
        if ($('#pivotc838a61b-6823-4f13-922b-ce5061094d01ViewTestePivot_pivotLjvAtendimento').is(':visible') && (currentStatus != vm.status() || currentPage != vm.dataToolbar.currentPage() || !commitData)) {
             currentStatus = vm.status();
             currentPage = vm.dataToolbar.currentPage();
             if (currentStatus && currentStatus.toLowerCase() == 'c') {
                  jEntitySearchPivotRelationship = '';
             }
        if(vm != null && vm.dataView != null ) {
             arrayData = unwrapObservableArray(vm.dataView, vm);
        }
        if(pivot == null) {
        $('#pivotc838a61b-6823-4f13-922b-ce5061094d01ViewTestePivot_pivotLjvAtendimento #fm-fields-view .fm-ui-btn:contains(\'OK\')')
             .live('mouseup', function () {
         });
        
        $('#pivotc838a61b-6823-4f13-922b-ce5061094d01ViewTestePivot_pivotLjvAtendimento #fm-toolbar-row .fm-ui-btn:contains(\'OK\')')
           .live('mouseup', function () {
               setTimeout(function () { filterPivotRelationship() }, 1);
        });
             $('#pivotc838a61b-6823-4f13-922b-ce5061094d01ViewTestePivot_pivotLjvAtendimento .btn-toggle-toolbar').die('click');
             $('#pivotc838a61b-6823-4f13-922b-ce5061094d01ViewTestePivot_pivotLjvAtendimento .btn-toggle-toolbar').live('click', function(){ 
                  var toolbar = pivot.getToolbarInstanceByPivotName('pivotc838a61b-6823-4f13-922b-ce5061094d01ViewTestePivot_pivotLjvAtendimento')
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
                return {'CodBandeiraRede': (isNullOrEmpty(item.CodBandeiraRede) ? '' : item.CodBandeiraRede.toString()), 'CodigoFilial': (isNullOrEmpty(item.CodigoFilial) ? '' : item.CodigoFilial.toString()), 'CodLoja': (isNullOrEmpty(item.CodLoja) ? '' : item.CodLoja.toString()), 'DataAtendimento': (isNullOrEmpty(item.DataAtendimento) ? '' : Globalize.format(getUTCDate(item.DataAtendimento), 'MM/dd/yyyy')), 'DescBandeiraRede': (isNullOrEmpty(item.DescBandeiraRede) ? '' : item.DescBandeiraRede.toString()), 'DescLoja': (isNullOrEmpty(item.DescLoja) ? '' : item.DescLoja.toString()), 'IdBandeiraRede': (isNullOrEmpty(item.IdBandeiraRede) ? '' : item.IdBandeiraRede.toString()), 'IdFilialPfj': (isNullOrEmpty(item.IdFilialPfj) ? '' : item.IdFilialPfj.toString()), 'IdGpecon': (isNullOrEmpty(item.IdGpecon) ? '' : item.IdGpecon.toString()), 'IdLoja': (isNullOrEmpty(item.IdLoja) ? '' : item.IdLoja.toString()), 'ValorCupomFiscal': isNullOrEmpty(item.ValorCupomFiscal) ? 0 : item.ValorCupomFiscal, 'ValorDescontoSubtotal': isNullOrEmpty(item.ValorDescontoSubtotal) ? 0 : item.ValorDescontoSubtotal, 'QtdeAtendimento': isNullOrEmpty(item.QtdeAtendimento) ? 0 : item.QtdeAtendimento, 'TicketMedio': isNullOrEmpty(item.TicketMedio) ? 0 : item.TicketMedio, 'ValorTotal': isNullOrEmpty(item.ValorTotal) ? 0 : item.ValorTotal};
            });
        
            var structure = {};
            structure.CodBandeiraRede = { type:'string', caption: 'Cod. Bandeira Rede', dimensionUniqueName: 'ctrlLjvAtendimento', dimensionCaption: 'LjvAtendimento'  };
            structure.CodigoFilial = { type:'string', caption: 'Codigo Filial', dimensionUniqueName: 'ctrlLjvAtendimento', dimensionCaption: 'LjvAtendimento'  };
            structure.CodLoja = { type:'string', caption: 'Cod. Loja', dimensionUniqueName: 'ctrlLjvAtendimento', dimensionCaption: 'LjvAtendimento'  };
            structure.DataAtendimento = { type:'date string', caption: 'Data Atendimento', dimensionUniqueName: 'ctrlLjvAtendimento', dimensionCaption: 'LjvAtendimento'  };
            structure.DescBandeiraRede = { type:'string', caption: 'Bandeira Rede', dimensionUniqueName: 'ctrlLjvAtendimento', dimensionCaption: 'LjvAtendimento'  };
            structure.DescLoja = { type:'string', caption: 'Loja', dimensionUniqueName: 'ctrlLjvAtendimento', dimensionCaption: 'LjvAtendimento'  };
            structure.IdBandeiraRede = { type:'string', caption: 'Id Bandeira Rede', dimensionUniqueName: 'ctrlLjvAtendimento', dimensionCaption: 'LjvAtendimento'  };
            structure.IdFilialPfj = { type:'string', caption: 'Id Filial Pfj', dimensionUniqueName: 'ctrlLjvAtendimento', dimensionCaption: 'LjvAtendimento'  };
            structure.IdGpecon = { type:'string', caption: 'Id Gpecon', dimensionUniqueName: 'ctrlLjvAtendimento', dimensionCaption: 'LjvAtendimento'  };
            structure.IdLoja = { type:'string', caption: 'Id Loja', dimensionUniqueName: 'ctrlLjvAtendimento', dimensionCaption: 'LjvAtendimento'  };
            structure.ValorCupomFiscal = { type:'number', caption: 'Valor Cupom Fiscal' }; 
            structure.ValorDescontoSubtotal = { type:'number', caption: 'Valor Desconto Subtotal' }; 
            structure.QtdeAtendimento = { type:'number', caption: 'Qtde Atendimento' }; 
            structure.TicketMedio = { type:'number', caption: 'Ticket Médio' }; 
            structure.ValorTotal = { type:'number', caption: 'Valor Total' }; 
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
                rows: [{ uniqueName: 'DescLoja' }],
                columns: [{ uniqueName: '[Measures]' }],
                measures: [{ uniqueName: 'ValorCupomFiscal' }],
            };
        
            pivot.setReport(report);
            pivot.setTopX("DescLoja", 3 ,"ValorCupomFiscal");
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
                name: 'ValorDescontoSubtotal',
                current: pivot.getFormat('ValorDescontoSubtotal')
            });
        
            formatMeasures.push({
                name: 'QtdeAtendimento',
                current: pivot.getFormat('QtdeAtendimento')
            });
        
            formatMeasures.push({
                name: 'TicketMedio',
                current: pivot.getFormat('TicketMedio')
            });
        
            formatMeasures.push({
                name: 'ValorTotal',
                current: pivot.getFormat('ValorTotal')
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
                 decimalPlaces : '2',
                 decimalSeparator: ',',
                 thousandsSeparator: '.',
                 name: 'ValorCupomFiscal'
             }, 'ValorCupomFiscal');
             pivot.setFormat({
                 decimalPlaces : '2',
                 decimalSeparator: ',',
                 thousandsSeparator: '.',
                 name: 'ValorDescontoSubtotal'
             }, 'ValorDescontoSubtotal');
             pivot.setFormat({
                 decimalSeparator: ',',
                 thousandsSeparator: '.',
                 name: 'QtdeAtendimento'
             }, 'QtdeAtendimento');
             pivot.setFormat({
                 decimalSeparator: ',',
                 thousandsSeparator: '.',
                 name: 'TicketMedio'
             }, 'TicketMedio');
             pivot.setFormat({
                 decimalPlaces : '2',
                 decimalSeparator: ',',
                 thousandsSeparator: '.',
                 name: 'ValorTotal'
             }, 'ValorTotal');
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
        var toolbarInstance = pivot.getToolbarInstanceByPivotName('pivotc838a61b-6823-4f13-922b-ce5061094d01ViewTestePivot_pivotLjvAtendimento');
        
             if (toolbarInstance) {
                 toolbarInstance.setPivotName('LjvAtendimento');
                 toolbarInstance.setReportFiles(vm.layoutFiles);
                 toolbarInstance.setGetSelectedLayoutContent(vm.getDataContext().getSelectedLayoutContent);
                 toolbarInstance.setProjectName(vm.layoutFiles[0].projectName);
                 toolbarInstance.setViewName(vm.viewName);
                 pivot.prefixNameLayout = 'LjvAtendimento';
                 pivot.pivotAdapterLayout = 'LjvAtendimento';
             }
        
             vm.layoutFiles.forEach(function(file) {
                 if (file.selected && file.layoutFullName.indexOf('.xml') > 0 && file.layoutFullName.indexOf('LjvAtendimento') > 0)
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
             $('#pivotc838a61b-6823-4f13-922b-ce5061094d01ViewTestePivot_pivotLjvAtendimento').append('<div class="plus-actions"><ul>'+ listItems +'</ul></div>');
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
             var containerId = 'pivotc838a61b-6823-4f13-922b-ce5061094d01ViewTestePivot_pivotLjvAtendimento';
        
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
    vm.addDataSource({ key: 'pivotc838a61b-6823-4f13-922b-ce5061094d01ViewTestePivot_pivotLjvAtendimento', name: 'dataView', itemsSource: itemsSource });
}


    };
    return complement;
}

return complementCtor;
});
