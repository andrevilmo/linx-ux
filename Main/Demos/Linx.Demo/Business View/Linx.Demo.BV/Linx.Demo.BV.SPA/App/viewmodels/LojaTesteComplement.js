define([], function () {
var complementCtor = function() {
    var complement = {
    isAutomatic: true
    , renderscyLojaTeste_dGrid: function(vm) {
    var getDataSource = function() {
        var source = null;
        try {
            source = vm.dataView;
        }
        catch (e) { }
        return (isNullOrEmpty(source) ? ko.observableArray([]) : source);
    }
    var getVisibleColumns = function(metaDataControl) {
       if (metaDataControl) return '';
       var visibleColumns = '';
       if($('#scyLojaTeste_dGrid').data('igGrid') == undefined) return '';
       var cols = $('#scyLojaTeste_dGrid').igGrid('option', 'columns');
       if (cols) {
         for (var idx = 0; idx < cols.length; idx++) {
             if (cols[idx].hidden != true) visibleColumns += (visibleColumns == '' ? '' : ',') + cols[idx].key;
         }
       }
       return visibleColumns;
    }
    var started = false;
    var itemsSource = { getVisibleColumns: getVisibleColumns, containerId: 'scyLojaTeste_dGrid_container', dataBind: function (commitData, force) {
       var grid = $('#scyLojaTeste_dGrid');
       if (started && (typeof grid.data('igGridUpdating') === 'undefined')) { started = false; }
       if (commitData && started) {
           if (grid.igGridUpdating('isEditing')) {
               grid.igGrid('commit');
           }
           return;
       }
       if (!grid[0] || (!force && grid.parent().width() <= 0)) return;
       if (!started) {
           createDataGrid(grid);
           started = true;
           commitData = false;
           $('#scyLojaTeste_dGrid_groupbyarea').addClass('hide');
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
       }
    }};
    var valueGrouBy = -1;
    var deletedIndex = -1;
    function verifyCanEditCol(column, clear, entity){
        switch(column){
            case 'IdBandeiraRede': { canEditing = clear; break;}
            case 'AreaM2': { canEditing = clear; break;}
            case 'Bairro': { canEditing = clear; break;}
            case 'IdFilialPfj': { canEditing = clear; break;}
            case 'IdLojaAgrupamento': { canEditing = clear; break;}
            case 'IdLojaAgrupamento1': { canEditing = clear; break;}
            case 'Cep': { canEditing = clear; break;}
            case 'IdCep': { canEditing = clear; break;}
            case 'IdGpecon': { canEditing = clear; break;}
            case 'IdLjvCanalVenda': { canEditing = clear; break;}
            case 'IdRegiaoComercial': { canEditing = clear; break;}
            case 'Cidade': { canEditing = clear; break;}
            case 'CodLoja': { canEditing = clear; break;}
            case 'Complemento': { canEditing = clear; break;}
            case 'DataAbertura': { canEditing = clear; break;}
            case 'IdTabPreco': { canEditing = clear; break;}
            case 'DataAtualizacao': { canEditing = clear; break;}
            case 'DataCadastro': { canEditing = clear; break;}
            case 'IdStkDeposito': { canEditing = clear; break;}
            case 'DataFechamento': { canEditing = clear; break;}
            case 'DataInativacao': { canEditing = clear; break;}
            case 'DataReativacao': { canEditing = clear; break;}
            case 'Ddd': { canEditing = clear; break;}
            case 'DescLoja': { canEditing = clear; break;}
            case 'DescPais': { canEditing = clear; break;}
            case 'DescUf': { canEditing = clear; break;}
            case 'EnderecoIp': { canEditing = clear; break;}
            case 'FatorE': { canEditing = clear; break;}
            case 'FatorF': { canEditing = clear; break;}
            case 'FatorP': { canEditing = clear; break;}
            case 'FatorQ': { canEditing = clear; break;}
            case 'FatorS': { canEditing = clear; break;}
            case 'FatorW': { canEditing = clear; break;}
            case 'IdLoja': { canEditing = clear; break;}
            case 'Inativo': { canEditing = clear; break;}
            case 'IndicaEcommerce': { canEditing = clear; break;}
            case 'IndicaFranquia': { canEditing = clear; break;}
            case 'IndicaSomenteCrm': { canEditing = clear; break;}
            case 'Latitude': { canEditing = clear; break;}
            case 'Logradouro': { canEditing = clear; break;}
            case 'Longitude': { canEditing = clear; break;}
            case 'Numero': { canEditing = clear; break;}
            case 'Telefone': { canEditing = clear; break;}
            case 'TipoLogradouro': { canEditing = clear; break;}
        }
        return canEditing;
    };
    function createDataGrid(grid) {
        grid.igGrid({ height: (vm.isDependentVM() ? (getGridHeightSuggested() * 0.7) : $(window).height() * 0.85), width: '100%',
            dataSource: [],
            primaryKey: 'RowDataId',
            autoGenerateColumns: false,
            autofitLastColumn: false,
            dataSourceType: 'json',
            renderCheckboxes: true,
            autoCommit: true,
            cellClick: function(evt, ui) {
                 if (ui.cellElement && ui.cellElement.childNodes[0] && ui.cellElement.childNodes[0].childNodes[1]) {
                     var e = ui.cellElement.childNodes[0].childNodes[1];
                     if (e && e.tagName == 'IMG' && vm.status() != 'C')
                     {
                          var entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowKey);
                          var key = e.attributes['key'].value;
                          var table = e.attributes['tableName'].value;
                          showMultimidia(entity, e, table, key, vm.LojaTeste());
                     }
                 }
            },
            enableUTCDates : true,
            featureChooserIconDisplay: 'always',
            dataRendered: function(evt, ui) { 
            },
            columns: [
                { key: 'RowDataId', headerText: 'RowDataId', width: '50px', dataType: '', hidden: true },
                { key: 'IdBandeiraRede', headerText: 'Id Bandeira Rede', width: '208px', dataType: 'string', format: '', hidden: false, unbound: false, group: null   },
                { key: 'AreaM2', headerText: 'Area M2', width: '130px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'Bairro', headerText: 'Bairro', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null   },
                { key: 'IdFilialPfj', headerText: 'Id Filial Pfj', width: '169px', dataType: 'string', format: '', hidden: false, unbound: false, group: null   },
                { key: 'IdLojaAgrupamento', headerText: 'Id Loja Agrupamento', width: '247px', dataType: 'string', format: '', hidden: false, unbound: false, group: null   },
                { key: 'IdLojaAgrupamento1', headerText: 'Id Loja Agrupamento1', width: '260px', dataType: 'string', format: '', hidden: false, unbound: false, group: null   },
                { key: 'Cep', headerText: 'Cep', width: '90px', dataType: 'string', format: '', hidden: false, unbound: false, group: null   },
                { key: 'IdCep', headerText: 'Id Cep', width: '151px', dataType: 'string', format: '', hidden: false, unbound: false, group: null   },
                { key: 'IdGpecon', headerText: 'Id Gpecon', width: '151px', dataType: 'string', format: '', hidden: false, unbound: false, group: null   },
                { key: 'IdLjvCanalVenda', headerText: 'Id Ljv Canal Venda', width: '234px', dataType: 'string', format: '', hidden: false, unbound: false, group: null   },
                { key: 'IdRegiaoComercial', headerText: 'Id Regiao Comercial', width: '247px', dataType: 'string', format: '', hidden: false, unbound: false, group: null   },
                { key: 'Cidade', headerText: 'Cidade', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null   },
                { key: 'CodLoja', headerText: 'Cod Loja', width: '200px', dataType: 'string', format: '', hidden: false, unbound: false, group: null   },
                { key: 'Complemento', headerText: 'Complemento', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null   },
                { key: 'DataAbertura', headerText: 'Data Abertura', width: '169px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null   },
                { key: 'IdTabPreco', headerText: 'Id Tab Preco', width: '156px', dataType: 'string', format: '', hidden: false, unbound: false, group: null   },
                { key: 'DataAtualizacao', headerText: 'Data Atualizacao', width: '208px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null   },
                { key: 'DataCadastro', headerText: 'Data Cadastro', width: '169px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null   },
                { key: 'IdStkDeposito', headerText: 'Id Stk Deposito', width: '195px', dataType: 'string', format: '', hidden: false, unbound: false, group: null   },
                { key: 'DataFechamento', headerText: 'Data Fechamento', width: '195px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null   },
                { key: 'DataInativacao', headerText: 'Data Inativacao', width: '195px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null   },
                { key: 'DataReativacao', headerText: 'Data Reativacao', width: '195px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null   },
                { key: 'Ddd', headerText: 'Ddd', width: '50px', dataType: 'string', format: '', hidden: false, unbound: false, group: null   },
                { key: 'DescLoja', headerText: 'Desc Loja', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null   },
                { key: 'DescPais', headerText: 'Desc Pais', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null   },
                { key: 'DescUf', headerText: 'Desc Uf', width: '91px', dataType: 'string', format: '', hidden: false, unbound: false, group: null   },
                { key: 'EnderecoIp', headerText: 'Endereco Ip', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null   },
                { key: 'FatorE', headerText: 'Fator E', width: '91px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'FatorF', headerText: 'Fator F', width: '91px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'FatorP', headerText: 'Fator P', width: '91px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'FatorQ', headerText: 'Fator Q', width: '91px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'FatorS', headerText: 'Fator S', width: '91px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'FatorW', headerText: 'Fator W', width: '91px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'IdLoja', headerText: 'Id Loja', width: '130px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'Inativo', headerText: 'Inativo', width: '91px', dataType: 'bool', format: 'checkbox', hidden: false, unbound: false, group: null   },
                { key: 'IndicaEcommerce', headerText: 'Indica Ecommerce', width: '208px', dataType: 'bool', format: 'checkbox', hidden: false, unbound: false, group: null   },
                { key: 'IndicaFranquia', headerText: 'Indica Franquia', width: '195px', dataType: 'bool', format: 'checkbox', hidden: false, unbound: false, group: null   },
                { key: 'IndicaSomenteCrm', headerText: 'Indica Somente Crm', width: '234px', dataType: 'bool', format: 'checkbox', hidden: false, unbound: false, group: null   },
                { key: 'Latitude', headerText: 'Latitude', width: '104px', dataType: 'number', format: '0.00000', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'Logradouro', headerText: 'Logradouro', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null   },
                { key: 'Longitude', headerText: 'Longitude', width: '117px', dataType: 'number', format: '0.00000', hidden: false, unbound: false, group: null , formatter: function (val, record) { return formatAndAlignNumber(grid, val, record, this.dataType, this.format); }  },
                { key: 'Numero', headerText: 'Numero', width: '100px', dataType: 'string', format: '', hidden: false, unbound: false, group: null   },
                { key: 'Telefone', headerText: 'Telefone', width: '200px', dataType: 'string', format: '', hidden: false, unbound: false, group: null   },
                { key: 'TipoLogradouro', headerText: 'Tipo Logradouro', width: '195px', dataType: 'string', format: '', hidden: false, unbound: false, group: null   }
            ],
            features: [
                        { name: 'Sorting', type: 'local', caseSensitive: true,
                          columnSorting: function (evt, ui) { 
                              $.grep(ui.owner.grid._visibleColumnsArray, function (e) { 
                                  if (e.key == ui.columnKey && e.dataType == 'string') 
                                      return $('#scyLojaTeste_dGrid').igGridSorting('option', 'caseSensitive', false); 
                                  else if (e.key == ui.columnKey) 
                                      return $('#scyLojaTeste_dGrid').igGridSorting('option', 'caseSensitive', true); 
                              }); 
                          } 
                          , columnSorted: function (event, args) { if (!isNullOrEmpty(args.columnKey) && !isNullOrEmpty(args.direction)) { vm.sortData(args.columnKey + ' ' + args.direction); } } },
                        { name: 'Filtering', mode: 'advanced', filterDropDownItemIcons: false, filterDropDownWidth: 200, allowFiltering: true, type: 'local' },
                        { name: 'Selection', mode: 'row',
                          rowSelectionChanged: function(evt, ui) {
                             if (typeof ui.owner.grid.selectedRow().id !== 'undefined') { selectGridCurrentItem(vm.goToKey, 'RowDataId', ui); } 
                              },
                        },
                        { name: 'Tooltips', columnSettings:[{ columnKey: "IdBandeiraRede", allowTooltips: false },{ columnKey: "AreaM2", allowTooltips: true },{ columnKey: "Bairro", allowTooltips: true },{ columnKey: "IdFilialPfj", allowTooltips: false },{ columnKey: "IdLojaAgrupamento", allowTooltips: false },{ columnKey: "IdLojaAgrupamento1", allowTooltips: false },{ columnKey: "Cep", allowTooltips: true },{ columnKey: "IdCep", allowTooltips: false },{ columnKey: "IdGpecon", allowTooltips: false },{ columnKey: "IdLjvCanalVenda", allowTooltips: false },{ columnKey: "IdRegiaoComercial", allowTooltips: false },{ columnKey: "Cidade", allowTooltips: true },{ columnKey: "CodLoja", allowTooltips: true },{ columnKey: "Complemento", allowTooltips: true },{ columnKey: "DataAbertura", allowTooltips: true },{ columnKey: "IdTabPreco", allowTooltips: false },{ columnKey: "DataAtualizacao", allowTooltips: true },{ columnKey: "DataCadastro", allowTooltips: true },{ columnKey: "IdStkDeposito", allowTooltips: false },{ columnKey: "DataFechamento", allowTooltips: true },{ columnKey: "DataInativacao", allowTooltips: true },{ columnKey: "DataReativacao", allowTooltips: true },{ columnKey: "Ddd", allowTooltips: true },{ columnKey: "DescLoja", allowTooltips: true },{ columnKey: "DescPais", allowTooltips: true },{ columnKey: "DescUf", allowTooltips: true },{ columnKey: "EnderecoIp", allowTooltips: true },{ columnKey: "FatorE", allowTooltips: true },{ columnKey: "FatorF", allowTooltips: true },{ columnKey: "FatorP", allowTooltips: true },{ columnKey: "FatorQ", allowTooltips: true },{ columnKey: "FatorS", allowTooltips: true },{ columnKey: "FatorW", allowTooltips: true },{ columnKey: "IdLoja", allowTooltips: true },{ columnKey: "Inativo", allowTooltips: true },{ columnKey: "IndicaEcommerce", allowTooltips: true },{ columnKey: "IndicaFranquia", allowTooltips: true },{ columnKey: "IndicaSomenteCrm", allowTooltips: true },{ columnKey: "Latitude", allowTooltips: true },{ columnKey: "Logradouro", allowTooltips: true },{ columnKey: "Longitude", allowTooltips: true },{ columnKey: "Numero", allowTooltips: true },{ columnKey: "Telefone", allowTooltips: true },{ columnKey: "TipoLogradouro", allowTooltips: true }] },
                        { name: 'Resizing' }, 
                        { name: 'Hiding', 
                            columnHidden: function (evt, ui) {
                               showMultimidiaLazy('#scyLojaTeste_dGrid');
                            },
                            columnShown: function (evt, ui) {
                               showMultimidiaLazy('#scyLojaTeste_dGrid');
                            }
                        },
                        { name: 'ColumnMoving' }
            
            
                       ,{ name: 'Updating', horizontalMoveOnEnter: true,
                          columnSettings: [{ columnKey: "IdBandeiraRede", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpTbcBandeiraRede", isNullable: true, custom: vm.custom, vm: vm, allowMultiSelectionInSearch:true, validateOnClearState:false, maxValue:2147483647, maxLength: 12, defaultValue: null } }, { columnKey: "IdFilialPfj", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpTbcFilial", isNullable: false, custom: vm.custom, vm: vm, allowMultiSelectionInSearch:true, validateOnClearState:false, maxValue:2147483647, maxLength: 12, defaultValue: 0 } }, { columnKey: "IdLojaAgrupamento", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpAgrupamentoSortimento", isNullable: true, custom: vm.custom, vm: vm, allowMultiSelectionInSearch:true, validateOnClearState:false, maxValue:2147483647, maxLength: 12, defaultValue: null } }, { columnKey: "IdLojaAgrupamento1", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpAgrupamentoComercial", isNullable: true, custom: vm.custom, vm: vm, allowMultiSelectionInSearch:true, validateOnClearState:false, maxValue:2147483647, maxLength: 12, defaultValue: null } }, { columnKey: "IdCep", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpGeoCep", isNullable: true, custom: vm.custom, vm: vm, allowMultiSelectionInSearch:true, validateOnClearState:false, maxValue:2147483647, maxLength: 12, defaultValue: null } }, { columnKey: "IdGpecon", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpTbcGrupoEconomico", isNullable: false, custom: vm.custom, vm: vm, allowMultiSelectionInSearch:true, validateOnClearState:false, maxValue:2147483647, maxLength: 12, defaultValue: 0 } }, { columnKey: "IdLjvCanalVenda", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpLjvCanalVenda", isNullable: false, custom: vm.custom, vm: vm, allowMultiSelectionInSearch:true, validateOnClearState:false, maxValue:2147483647, maxLength: 12, defaultValue: 0 } }, { columnKey: "IdRegiaoComercial", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpTbcRegiaoComercial", isNullable: true, custom: vm.custom, vm: vm, allowMultiSelectionInSearch:true, validateOnClearState:false, maxValue:2147483647, maxLength: 12, defaultValue: null } }, { columnKey: "IdTabPreco", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpPrdTabelaPreco", isNullable: true, custom: vm.custom, vm: vm, allowMultiSelectionInSearch:true, validateOnClearState:false, maxValue:2147483647, maxLength: 12, defaultValue: null } }, { columnKey: "IdStkDeposito", editorProvider: new $.ig.EditorProviderLookUp(), editorOptions: { lookUpName: "LookUpStkDeposito", isNullable: true, custom: vm.custom, vm: vm, allowMultiSelectionInSearch:true, validateOnClearState:false, maxValue:2147483647, maxLength: 12, defaultValue: null } }, { columnKey: 'DataAbertura', editorType: 'datepicker', editorOptions: { minValue: new Date(1900, 0, 1), datepickerOptions: { changeMonth: true, changeYear: true }  } }, { columnKey: 'DataAtualizacao', editorType: 'datepicker', editorOptions: { minValue: new Date(1900, 0, 1), datepickerOptions: { changeMonth: true, changeYear: true }  } }, { columnKey: 'DataCadastro', editorType: 'datepicker', editorOptions: { minValue: new Date(1900, 0, 1), datepickerOptions: { changeMonth: true, changeYear: true }  } }, { columnKey: 'DataFechamento', editorType: 'datepicker', editorOptions: { minValue: new Date(1900, 0, 1), datepickerOptions: { changeMonth: true, changeYear: true }  } }, { columnKey: 'DataInativacao', editorType: 'datepicker', editorOptions: { minValue: new Date(1900, 0, 1), datepickerOptions: { changeMonth: true, changeYear: true }  } }, { columnKey: 'DataReativacao', editorType: 'datepicker', editorOptions: { minValue: new Date(1900, 0, 1), datepickerOptions: { changeMonth: true, changeYear: true }  } }, { columnKey: "AreaM2" , editorType: 'numeric', editorOptions: { maxLength: 12, maxValue: null, minValue: 0, dataMode: 'int' } }, { columnKey: "Bairro" , editorOptions: { maxLength: 100 } }, { columnKey: "Cep" , editorOptions: { maxLength: 9 } }, { columnKey: "Cidade" , editorOptions: { maxLength: 100 } }, { columnKey: "CodLoja" , editorOptions: { maxLength: 20 } }, { columnKey: "Complemento" , editorOptions: { maxLength: 100 } }, { columnKey: "Ddd" , editorOptions: { maxLength: 5 } }, { columnKey: "DescLoja" , editorOptions: { maxLength: 60 } }, { columnKey: "DescPais" , editorOptions: { maxLength: 40 } }, { columnKey: "DescUf" , editorOptions: { maxLength: 2 } }, { columnKey: "EnderecoIp" , editorOptions: { maxLength: 40 } }, { columnKey: "FatorE" , editorType: 'numeric', editorOptions: { maxLength: 3, maxValue: null, minValue: 0, dataMode: 'byte' } }, { columnKey: "FatorF" , editorType: 'numeric', editorOptions: { maxLength: 3, maxValue: null, minValue: 0, dataMode: 'byte' } }, { columnKey: "FatorP" , editorType: 'numeric', editorOptions: { maxLength: 3, maxValue: null, minValue: 0, dataMode: 'byte' } }, { columnKey: "FatorQ" , editorType: 'numeric', editorOptions: { maxLength: 3, maxValue: null, minValue: 0, dataMode: 'byte' } }, { columnKey: "FatorS" , editorType: 'numeric', editorOptions: { maxLength: 3, maxValue: null, minValue: 0, dataMode: 'byte' } }, { columnKey: "FatorW" , editorType: 'numeric', editorOptions: { maxLength: 3, maxValue: null, minValue: 0, dataMode: 'byte' } }, { columnKey: "IdLoja" , editorType: 'numeric', editorOptions: { maxLength: 12, maxValue: null, minValue: 0, dataMode: 'int' } }, { columnKey: "Latitude" , editorType: 'numeric', editorOptions: { maxLength: 9, maxValue: 999.99999, minValue: 0, dataMode: 'decimal', minDecimals: 5, maxDecimals: 5 } }, { columnKey: "Logradouro" , editorOptions: { maxLength: 200 } }, { columnKey: "Longitude" , editorType: 'numeric', editorOptions: { maxLength: 9, maxValue: 999.99999, minValue: 0, dataMode: 'decimal', minDecimals: 5, maxDecimals: 5 } }, { columnKey: "Numero" , editorOptions: { maxLength: 10 } }, { columnKey: "Telefone" , editorOptions: { maxLength: 20 } }, { columnKey: "TipoLogradouro" , editorOptions: { maxLength: 10 } }],
                          rowDeleting: function (evt, ui) {
                              deletedIndex = ui.element.context.rowIndex;
                              var entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowID);
                              if (entity) {
                                  vm.deleteEntity(entity);
                                  vm.dataView.remove(entity);
                              }
                          },
                          rowDeleted: function (evt, ui) {
                              var grid = $('#scyLojaTeste_dGrid');
                              var rows = grid.igGrid('allRows');
                              if (rows.length > 0)
                              {
                                  if (deletedIndex < 0) deletedIndex = 0;
                                  else if (rows.length <= deletedIndex) deletedIndex = (rows.length-1);
                                  grid.igGridSelection('selectRow', deletedIndex);
                                  grid.igGrid('scrollContainer').scrollTop(grid.igGrid('option', 'avgRowHeight') * deletedIndex);
                              }
                          },
                          enableDataDirtyException: false, 
                          generatePrimaryKeyValue: function(evt, ui){  },
                          enableDeleteRow: false,
                          enableAddRow: false,
                          startEditTriggers: 'click',
                          editMode: 'cell', /*cell(atual) ou rowedittemplate(template)*/
                          rowEditDialogContainment: 'window',
                          showReadonlyEditors: false,
                          showDoneCancelButtons: false,
                          editCellStarting: function(evt, ui) { 
                              var entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowID);
                              var canEditing = false, clear = vm.status() === 'C';
                              var cols = $('#scyLojaTeste_dGrid').igGrid('option', 'columns');
                              if(vm.status() == 'E') {
                                  var rowId = ui.rowID;
                                  var nextColumn = ui.columnIndex;
                                  canEditing = verifyCanEditCol(ui.columnKey, clear, entity);
                                  if (!canEditing) return false;
                                  else if (cols[nextColumn].hidden) return false;
                              }
            
                              canEditing = verifyCanEditCol(ui.columnKey, clear, entity);
                              if(!canEditing && vm.status() !== 'Q') {
                                  var rowId = ui.rowID, colId = ui.columnIndex;
                                  if (colId >= grid.igGrid('option', 'columns').length -1){
                                      colId = 1; rowId++;
                                  }
                                  grid.igGridUpdating('startEdit', rowId, colId);
                              }
                              return canEditing;
                          },
                          editCellStarted: function(evt, ui){
                              var lstRefreshDados = null;
                              var columns = $('#scyLojaTeste_dGrid').igGridUpdating('option', 'columnSettings');
                              var currentCol = null;
                              columns.forEach(function (entry, index) {
                                 if (entry.columnKey == ui.columnKey) currentCol = entry;
                                 if (currentCol != null) return false;
                              });
                              if (currentCol !== null && currentCol.hasOwnProperty('editorType') && currentCol.editorType === 'combo') {
                                 var lookUpName = $(ui.editor).igCombo('option', 'inputName');
                                 if (lookUpName !== null) {
                                     lstRefreshDados = vm.dataCombo.getItems('' + lookUpName + '', '');
                                     $(ui.editor).igCombo('option', 'dataSource', lstRefreshDados);
                                 }
                              }
                          },
                          editCellEnded: function(evt, ui) {
                              var entity = findElementByKey(getDataSource(), 'RowDataId', ui.rowID);
                              if (entity != null && (typeof ui.value) != 'undefined' && getAbsoluteValue(entity[ui.columnKey]) != ui.value) {
                                  setAbsoluteValue(entity, ui.columnKey, ui.value);
                                  if (!ui.update) itemsSource.dataBind(false);
                              }
                          },
                        }
                    ]
            });
            if ((typeof vm.OnDataGridCreated == 'function')){
                vm.OnDataGridCreated('scyLojaTeste_dGrid');
            }
            grid.delegate('.ui-iggrid-activerow', 'dblclick', function (e) {
                if (vm.LojaTeste().status() === 'Q') vm.LojaTeste().dataToolbar.viewInfo();
            });
        }
        vm.addDataSource({ key: 'scyLojaTeste_dGrid', name: 'dataView', itemsSource: itemsSource });
    }


    };
    return complement;
}

return complementCtor;
});
