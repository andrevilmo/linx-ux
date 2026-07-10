define(['durandal/app', 'pkg_varejo-bv-spa/services/ExternasContext', 'plugins/router', 'plugins/widget', 'managers/__auth', 'viewmodels/shared/modal', 'viewmodels/shared/modal2', 'managers/brand', 'managers/predefinedFilters', 'services/logger', 'viewmodels/shared/modalMultimidia', 'common', 'pkg_varejo-bv-spa/viewmodels/UIExternaTabComplement', 'viewmodels/shared/modalCustomSearch'],
function (app, dataContextFn, router, widget, managerAuth, modal, modal2, managerBrand, managerPredefined, logger, modalMultimidia, common, complementFn, modalCustomSearch) {
var vms = [];
var vmInstance = function () {
    var activeRoute = document.URL;
    if (activeRoute.indexOf('?') >= 0)
        activeRoute = activeRoute.substring(0, activeRoute.indexOf('?'));
    if (vms[activeRoute])
        return vms[activeRoute];
    else {
        var vm = vmConstructor();
        vms[activeRoute] = vm;
        return vm;
    }
}
var vmConstructor = function () {
    
    var customSearch = function () { 
        modalCustomSearch.show(vm, dataContext);
    };
    var translatedJEntitySearch = '';
    var customSearchResult = { searchDefinition: '', serializedSearch: '', translatedSearch: '' };
    var hasCustomSearches = ko.observable(false);
    var sortInfo = '';
    var currentSettings = null;
    var registeredUIs = [];
    var dataContext = dataContextFn();
    var complement = ((typeof complementFn === 'function') ? complementFn() : null);
    var viewClosed = false;
    var lastJEntitySearch = null;
    var lastStatus = '';
    var status = ko.observable('N');
    var hideToolbar = ko.observable(false);
    var isDependentVM = ko.observable(false);
    var transactionNumberControl = ko.observable('00000000');
    var navigationByPage = ko.observable(false);
    var viewType = ko.observable('Main');
    var hasMainTopDataGrid = ko.observable(false);
    var currentDataIndex = ko.observable(0);
    var currentDataItem = ko.observable();
    var currentActivityInformation = ko.observable('');
    var currentPage = ko.observable(0);
    var pageCount = ko.observable(0);
    var pageSize = ko.observable(100);
    var totalItemCount = ko.observable(0);
    var isSaving = ko.observable(false);
    var dataView = ko.observableArray([]);
    var dataSource = [];
    var showDataFeedUrl = function() {
        app.showMessage(dataContext.getDataFeedUrl(), 'Endereço do serviço', ['Ok']);
    };
    var lastSearchFilter = function () {
        var filterTranslation = getTranslatedFilter();
        app.showMessage((isNullOrEmpty(filterTranslation) ? 'Pesquisa sem filtros.' : filterTranslation), 'Filtros da pesquisa');
    }
    var registerUI = function (name, viewPath, settings) {
        registeredUIs.push(name);
        registeredUIs[name] = {
            uiName: viewPath,
            uiSettings: settings
        };
    }
    
    var showRegisteredUI = function (name, elementName) {
        var ctrl = $('#' + elementName);
        var bindingContext = ko.contextFor(ctrl[0]);
        var uiSelected = registeredUIs[name];
        if (uiSelected.length == 0){
            console.warn('Não foi encontrado o elemento [' + elementName + ']');
            return;
        }
        var settings = {
            kind: uiSelected.uiName,
            parentVM: vm,
            uiSettings: uiSelected.uiSettings
        };
        var ext;
        ctrlName = elementName + "_" + name;
        if ($('#' + ctrlName).length == 0)
            ext = ctrl.append("<div id='" + elementName + "_" + name + "' />");
        else
            ext = $('#' + ctrlName);
        widget.create(ext[0], settings, bindingContext, true);
    };
    var currentRecord = ko.computed(function () {
        if (pageSize() === 0) return currentDataIndex();
        else return (currentPage() * pageSize()) + currentDataIndex();
    });
    
    var _isBusy = false;
    var isBusy = function isBusy(value) {
        if (typeof value === 'undefined') {
            return _isBusy;
        } else {
            _isBusy = value;
            if ($(".page-container").html() == undefined || $(".page-container").html().length == 0)
            return;
            if (value) { common.showProcess('#main'); }
            else { common.closeProcess('#main'); }
        }
    };
    var totalRecords = ko.computed(function () {
        if (pageSize() === 0) return dataView().length;
    
        var recordCount = 0;
        if (currentPage() === 0) {
            if (pageCount() <= 1) {
                 recordCount = dataView().length;
            } else {
                 recordCount = totalItemCount() - pageSize() +  dataView().length;
            }
        } else if (currentPage() === (pageCount() - 1)) {
            recordCount = (pageSize() * (pageCount() - 1)) + dataView().length;
        } else {
            recordCount = pageSize() * (currentPage() + 1);
            recordCount += totalItemCount() - (pageSize() * (currentPage() + 2));
            recordCount += dataView().length;
        }
        return recordCount;
    });
    var currentFormattedRecord = ko.computed(function () {
        if (totalRecords() === 0) return '0';
        else return (currentRecord()+1).toString();
    });
    var currentRecordInfo = ko.computed(function () { var totalR = totalRecords(); if (totalR === 0) { return '0/0'; } else { return currentFormattedRecord() + '/' + totalR.toString(); } });
    var contextDataUpdateHandler = function (e) {
        dataBind(dataContext.dataForUpdate);
    };
    //#region Durandal Events
    
    var started = false;
    var parentVM = null;
    var uiSettings = null;
    var filteredEntities = [];
    //#region quick search
    var quickSearch = function () {
    }
    //#endregion 
    var activate = function (settings, querystring) {
      if (typeof common.getTransactionCode === 'function') transactionNumberControl(common.getTransactionCode());
      vm.UIExternaTab = getVM;
      if ((typeof settings === 'object') && (settings != null)) {
          currentSettings = settings;
      }
      if ((typeof settings === 'object') && (settings != null) && settings.objectQuery) {
          isDependentVM(false);
          parentVM = null;
          filteredEntities = [];
          clear();
          var fieldProperty, value;
          if (!isNullOrEmpty(settings.objectQuery)) {
              $.each(settings.objectQuery.split(';'), function (idxElement, element) {
                  var idx = element.indexOf(':');
                  if (idx >= 0) {
                      field = element.slice(0, idx).trim();
                      value = element.slice(idx + 1, element.length);
                      setAbsoluteValue(currentDataItem(), field, value);
                  }
              });
          }
          if (settings.executeQuery == 'true')
              query(true);
          if (window.location.hash)
              history.replaceState(undefined, undefined, window.location.hash.substring(0, window.location.hash.indexOf('?')))
      }
      else {
          if ((typeof settings === 'object') && (settings != null) && settings.uiSettings) {
              uiSettings = settings.uiSettings;
              isDependentVM(true);
              parentVM = null;
              if (uiSettings.executeQuery === true) {
                  if (uiSettings.toolbarSettings) {
                      setSecurity(uiSettings.toolbarSettings.canAddNew, uiSettings.toolbarSettings.canClear, uiSettings.toolbarSettings.canCustomSearch, uiSettings.toolbarSettings.canDelete, uiSettings.toolbarSettings.canEdit, uiSettings.toolbarSettings.canLayout, uiSettings.toolbarSettings.canNavigate, uiSettings.toolbarSettings.canPrint, uiSettings.toolbarSettings.canSearch, uiSettings.toolbarSettings.canExport, uiSettings.toolbarSettings.noBusyLoading);
                      hideToolbar(uiSettings.toolbarSettings.removeDataToolbar);
                  }
                  filteredEntities = [];
                  if (settings.parentVM) { settings.parentVM.internalUIs = [ 'UIExternaTab' ]; settings.parentVM.UIExternaTab = getVM; }
                  clear();
                  if ((typeof uiSettings.querySetters === 'object')) {
                      for (var field in uiSettings.querySetters) {
                           if (field.indexOf('entitySearchRange') >= 0){
                              setAbsoluteValue(vm.entitySearchRange, field.split('.')[1], uiSettings.querySetters[field]);
                           }
                           else {
                               setAbsoluteValue(currentDataItem(), field, uiSettings.querySetters[field]);
                           }
                      }
                  }
                  query(true);
              }
              else {
                  setSecurity(uiSettings.canAddNew, uiSettings.canClear, uiSettings.canCustomSearch, uiSettings.canDelete, uiSettings.canEdit, uiSettings.canLayout, uiSettings.canNavigate, uiSettings.canPrint, uiSettings.canSearch, uiSettings.canExport, uiSettings.noBusyLoading);
                  hideToolbar(uiSettings.removeDataToolbar);
                  if ((typeof settings.parentVM === 'object') && settings.parentVM != null) {
                      parentVM = settings.parentVM;
                      parentVM.UIExternaTab = getVM;
                      if (isLookup()) { 
                          parentVM.internalUIs = [];
                          filteredEntities = [];
                          clear();
                          if (!isNullOrEmpty(uiSettings.valueToSearch)) {
                              if (typeof currentDataItem()[uiSettings.fieldToSearch] === 'function') {
                                  currentDataItem()[uiSettings.fieldToSearch](uiSettings.valueToSearch);
                                  query(true);
                              }
                          }
                      }
                      if ($.inArray('UIExternaTab', parentVM.internalUIs) === -1) parentVM.internalUIs.push('UIExternaTab');
                  }
              }
          }
          else {
              app.on('shell:close:all').then(function () {
                  viewClosed = true;
                  filteredEntities = [];
                  clear();
              });
              if (viewClosed == true){
                  viewClosed = false;
                  loadDataView();
              }
              adjustModuleSecurity();
          }
      }
      if (isChildVM() && (!_canNavigate || hideToolbar() || _canAddNew || _canDelete || _canEdit))
           pageSize(0);
      document.addEventListener(dataContext.contextUpdtEvt, contextDataUpdateHandler, false);
      if (!started) { started = true; clear(); } else { viewType('Main'); refreshToolbar(); }
      //Call OnLoadedChildUI Event
      if (isChildVM() && !isLookup()) {
        if (typeof parentVM.OnLoadedChildUI === 'function')
            parentVM.OnLoadedChildUI(vm);
      }
    };
    
    var adjustModuleSecurity = function () {
        parentVM = null;
        uiSettings = null;
        isDependentVM(false);
        setSecurity(true, true, true, true, true, true, true, true, true, true);
        managerAuth.getFormAccess('varejo-bv-spa-UIExternaTab', function (data) {
           if (data && !data.AcessoTotal) {
              setSecurity(data.Incluir, true, data.PesquisaEspecial, data.Excluir, data.Alterar, data.Layout, true, data.Imprimir, data.Pesquisar, data.Exportar);
           }
        }, logger);
    };
    
    var getVM = function () {
        return vm;
    };
    
    var binding = function () {
        if (!isChildVM()) vm.showProcessing('Inicializando...');
        return { cacheViews: false };
    };
    
    var bindingComplete = function () {
        return true;
    };
    var attached = function(view, parent) {
    };
    var canDeactivate = function () {
        if (require('plugins/dialog').isOpen())
            return false;
        try {
            var dlg =  $('.toolbar-dialog-template:visible')[0].id;
            if ($('#' + dlg).dialog('isOpen'))
                return false;
        } catch (e) {}
        if (status() === 'E') {
            return app.showMessage('Deseja realmente sair e cancelar o trabalho corrente?', 'Alerta', ['Yes', 'No'])
                .then(function (selectedOption) {
                    if (selectedOption === 'Yes') {
                       undo();
                   }
                   return selectedOption;
              });
      }
      return true;
    };
    var canActivate = function() {
        var data = router.activeInstruction().config;
        if (data.lxShellCompiledVersion != managerAuth.shellVersion) {
            app.showMessage('Versão de formulário incompatível com a versão de ambiente [' + managerAuth.shellVersion + '].', 'Formulário: UIExternaTab', ['Ok']);
            return false;
        }
        return true;
    };
    var deactivate = function() {
       document.removeEventListener(dataContext.contextUpdtEvt, contextDataUpdateHandler, false);
    };
    var compositionComplete = function() {
        $('#UIExternaTab_gbVenda').one('shown.bs.collapse', function (e) { vm.notifyInnerElements($(e.currentTarget), true); });

    complement.renderscyUIExternaTab_dGrid(vm);


        if (!hasMainTopDataGrid() && isChildVM()) removeFormViewControl();
        navigationByPage(hasMainTopDataGrid());
        dataBind();
        if (!isChildVM()) { vm.closeProcessing(); }
        try{ $(window).trigger('resize'); } catch(e){ console.log(e); }
        //Form startup routine
        if (currentSettings != null)
        {
            if (!isNullOrEmpty(currentSettings.action))
            {
                if (currentSettings.action.toLowerCase() == 'new')
                {
                    if (dataToolbar.canAddNew())
                    {
                        dataToolbar.addNew();
                    }
                }
            }
        }
        scrollMainTop();
        return true;
    };
    var detached = function (view) {
       if (viewClosed == true)
       {
          $(view).empty();
          $(view).remove();
          view = null;
       }
    };
    //#endregion
    var getMaxLength = function(entityName, propertyName){
        if (isNullOrEmpty(entityName)) entityName = 'Venda';
        var property = dataContext.getEntityProperty(entityName, propertyName);
        if(property != null)
            return property.maxLength;
        else
            return 0;
    };
    var isDataSourceHided = function (dataName) {
        if (vm.dataSource.length > 0 && vms[document.URL] === vm) {
           for (var db in vm.dataSource) { if (vm.dataSource[db].name === dataName && (typeof vm.dataSource[db].itemsSource.isElementHided === 'function')) { return vm.dataSource[db].itemsSource.isElementHided(); } }
        }
        return false;
    };
    var dataBind = function (dataName, commitData) {
        if (vm.dataSource.length > 0 && vms[document.URL] === vm) {
           for (var db in vm.dataSource) { if (!dataName || dataName === '' || vm.dataSource[db].name === dataName) { vm.dataSource[db].itemsSource.dataBind(commitData); } }
        }
    };
    var getVisibleProperties = function (dataName) {
        if (vm.dataSource.length > 0) {
            for (var db in vm.dataSource) { if (vm.dataSource[db].name === dataName && (typeof vm.dataSource[db].itemsSource.getVisibleColumns === 'function')) { return 'LinqValidProperties{LinqValidProperties#==#S' + vm.dataSource[db].itemsSource.getVisibleColumns(true) + '}'; } }
        }
        return '';
    };
    
    var visibleColumns = 'BigIntVenda,BitVenda,ComboboxVenda,DatetimeVenda,DecimalVenda,IdCliente,IdLoja,IdVenda,IntVenda,SmallIntVenda,StringVenda';
    
    var getVisiblePropertiesForExcel = function (dataName) {
        if (vm.dataSource.length > 0) {
            for (var db in vm.dataSource) { if (vm.dataSource[db].name === dataName && (typeof vm.dataSource[db].itemsSource.getVisibleColumns === 'function')) { return vm.dataSource[db].itemsSource.getVisibleColumns(); } }
        }
        return dataName === 'dataView' ? visibleColumns : '';
    };
    
    var addDataSource = function (dsElement) {
        if (!dsElement.key) return;
        var foundElement = null;
        for (var ds in vm.dataSource) { if (vm.dataSource[ds].key === dsElement.key) { foundElement = vm.dataSource[ds]; break; } }
        if (foundElement === null) { vm.dataSource.push(dsElement); } else { foundElement.itemsSource = dsElement.itemsSource; }
    };
    var loadDataView = function () {
    
    };
    var getInnerJExpression = function () {
        if (!uiSettings.applyFilterToParent || isNullOrEmpty(currentDataItem())) return '';
        dataBind('', true);
        var parentFieldsRelation = '';
        var detailFieldsRelation = '';
        if (uiSettings != null && uiSettings.parentFieldsRelation.length == uiSettings.detailFieldsRelation.length) {
          for (var idx = 0; idx < uiSettings.parentFieldsRelation.length; idx++) {
             parentFieldsRelation += (parentFieldsRelation == '' ? '' : ',') + uiSettings.parentFieldsRelation[idx];
             detailFieldsRelation += (detailFieldsRelation == '' ? '' : ',') + uiSettings.detailFieldsRelation[idx];
          }
        }
        var jExp = getQueryFilter(currentDataItem());
        if (jExp === 'Error') return 'Error';
        return '---' + currentDataItem().namespace + '.' + currentDataItem().typeName + '|' + uiSettings.parentSelectorDataName + '|' + parentFieldsRelation + '|' + detailFieldsRelation + ':::' + jExp;
    };
    var clearInnerUIs = function (parentEntity) {
       for (var idx = 0; idx < vm.internalUIs.length; idx++) { var innerVM = vm[vm.internalUIs[idx]](); if (isNullOrEmpty(parentEntity) || innerVM.getParentSelectorDataName() === parentEntity.typeName) innerVM.dataToolbar.clear(); }
    };
    var replaceInnerUIsKeys = function (parentEntity, parentPropertyName, oldValue, newValue) {
       for (var idx = 0; idx < vm.internalUIs.length; idx++) { var innerVM = vm[vm.internalUIs[idx]](); if (innerVM.getParentSelectorDataName() === parentEntity.typeName) innerVM.replaceKeyFromParent(parentPropertyName, oldValue, newValue); }
    };
    var replaceKeyFromParent = function (parentPropertyName, oldValue, newValue) {
        if (parentEntityRelated != null && isChildVM() && uiSettings.detailFieldsRelation.length == 1 && uiSettings.parentFieldsRelation.length == 1 && uiSettings.parentFieldsRelation[0] === parentPropertyName) {
            dataBind('dataView', true);
            var cacheElements = getAddedEntities();
            for (var idxR = 0; idxR < cacheElements.length; idxR++) {
                if (getAbsoluteValue(cacheElements[idxR][uiSettings.detailFieldsRelation[0]]) == oldValue) setAbsoluteValue(cacheElements[idxR], uiSettings.detailFieldsRelation[0], newValue);
            }
        }
    };
    var getInnerJExpressions = function () {
       var innerFilters = '';
       for (var idx = 0; idx < vm.internalUIs.length; idx++) { var eSearch = vm[vm.internalUIs[idx]]().getInnerJExpression(); if (eSearch === 'Error') return 'Error';  if (eSearch.indexOf('#') >= 0) innerFilters += eSearch; }
       return innerFilters;
    };
    var getParentSelectorDataName = function () {
       return ((typeof uiSettings === 'object') ? uiSettings.parentSelectorDataName : '');
    };
    var validParentSelectorDataCondition = function (data) {
       return ((typeof uiSettings === 'object') && !isNullOrEmpty(uiSettings.parentSelectorDataCondition) ? eval(uiSettings.parentSelectorDataCondition) : true);
    };
    var getJExpression = function (currentDI) {
        if (typeof currentDI === 'undefined') currentDI = currentDataItem();
        if (parentEntityRelated != null && isChildVM()) {
           for (var idx = 0; idx < uiSettings.parentFieldsRelation.length; idx++) { setAbsoluteValue(currentDI, uiSettings.detailFieldsRelation[idx], getAbsoluteValue(parentEntityRelated[uiSettings.parentFieldsRelation[idx]])); }
        }
        var extraFilters = '';
        if (isLookup()) {
             extraFilters = uiSettings.ownerReference.getLookUpClientFilterExpressions(uiSettings.lookupName, uiSettings.lookupInfo);
             if (extraFilters === 'Error') return extraFilters;
             if (typeof uiSettings.ownerReference['BeforeGet' + uiSettings.lookupName + 'Query'] == 'function') {
                   var customFilter = uiSettings.ownerReference['BeforeGet' + uiSettings.lookupName + 'Query']('', uiSettings.lookupInfo);
                   if (customFilter === 'Error') return null;
                   if (!isNullOrEmpty(customFilter)) { extraFilters = (isNullOrEmpty(extraFilters) ? '' : extraFilters + ';') + customFilter; }
             }
             if (!isNullOrEmpty(extraFilters)) extraFilters = currentDI.typeName + '{' + extraFilters + '}';
        }
        var innerExps = getInnerJExpressions();
        if (innerExps === 'Error') return 'Error';
        return currentDI.getJExpression(vm.entitySearchRange, [], (parentEntityRelated != null)) + extraFilters + innerExps;
    };
    var getSpecializedLookupItems = function () {
       var result = [];
       if (dataView().length > 1 && !isNullOrEmpty(complement) && (typeof complement.selectedCurrentItems === 'function'))
           result = complement.selectedCurrentItems(false, true);
       if ((dataView().length == 1 || !navigationByPage() || isNullOrEmpty(complement) || (typeof complement.selectedItems !== 'function') || (uiSettings && uiSettings.allowMultiSelectionInSearch === false)) && result.length == 0)
           result.push(currentDataItem());
       return result;
    };
    var exportData = function (forceAdd, isExcelDataSource) {
        if (forceAdd)
            require(['viewmodels/shared/addCustomExport'],
                function(addCustomExport){ addCustomExport.showModal(vm, null, 'Venda', getVisiblePropertiesForExcel('dataView'), null, true, isExcelDataSource); } );
        else
            require(['viewmodels/shared/customExport'],
                function(modalExport){ modalExport.showModal(vm, 'Venda', getVisiblePropertiesForExcel('dataView'), null, { canAdd: true, canEdit: true, canDel: true }, isExcelDataSource); } );
    };
    var exportDataDetails = function (entity, detailName, isExcelDataSource) {
        require(['viewmodels/shared/addCustomExport'], function(addCustomExport){
             addCustomExport.showModal(vm, null, detailName, getVisiblePropertiesForExcel(detailName + 'List'), entity['GetJsWhereDetailRelationFor' + detailName](), true, isExcelDataSource); } 
        );
    };
    var finalizeCombo = function (current, itens, lookupName) {
       dataContext['finalizeAll' + lookupName](current, itens, '', '');
    };
    var clearCombo = function (current, lookupName) {
       dataContext['clear' + lookupName](current);
    };
    var dataCombo = {
        combos: [],
        getItems: function (comboName, valuesFilter) {
            var items = dataCombo.combos[comboName];
            if (!isNullOrEmpty(valuesFilter) && items && items.length > 0) {
                for (var i = items.length - 1; i >= 0; i--) {
                    if ((',' + valuesFilter + ',').indexOf(',' + items[i].id + ',') === -1) {
                        items.removeAt(i);
                    }
                }
            }
            return (items && items.length > 0 ? items : []);
        },
        fillDataCombos: function (lookupName, fieldName, current, complete) {
            dataContext.getResultsCombo(lookupName, fieldName, current, function (result) {
                dataCombo.combos[lookupName] = result;
                if (complete) complete();
            });
        },
        isFilterChanged: function (lookupName, current) {
            return dataContext.clientFilterHasModified(lookupName, current);
        }
    };
    var refreshCurrentData = function () {
        if (navigationByPage()) {
           var refreshIndexedData = function (currentIndex) {
                 if (currentIndex < dataView().length) {
                     if (currentIndex == 0) vm.showProcessing('Atualizando informações...');
                     dataView()[currentIndex].refreshData(true, function (data) { if (data.results.length == 0) { app.showMessage('A informação a ser atualizada não está mais presente na base de dados!', 'Alerta', ['Ok']); vm.closeProcessing(); return; } refreshIndexedData(currentIndex + 1); });
                 }
                 else {
                     vm.closeProcessing();
                     dataBind();
                 }
           };
           if (dataView().length > 0) {
                refreshIndexedData(0);
           }
           return;
        }
        vm.showProcessing('Atualizando informações...');
        return currentDataItem().refreshData(false, complete);
    
        function complete(data) {
            if (data.results.length == 0) { app.showMessage('A informação a ser atualizada não está mais presente na base de dados!', 'Alerta', ['Ok']); vm.closeProcessing(); return; }
            currentDataItem.notifySubscribers();
            vm.closeProcessing();
        }
    }
    var _pendingRefresh = false;
    var lazyRefreshBinding = function () {
       if (!_pendingRefresh) {
           _pendingRefresh = true;
           setTimeout(function () { currentDataItem.notifySubscribers(); _pendingRefresh = false; }, 500);
       }
    };
    var getTranslatedFilter = function () {
        return translatedJEntitySearch + (isNullOrEmpty(translatedJEntitySearch) || isNullOrEmpty(customSearchResult.translatedSearch) ? '' : ' e ') + customSearchResult.translatedSearch;
    }
    var getQueryFilter = function (currentDI) {
        if (typeof currentDI === 'undefined') currentDI = currentDataItem();
        dataBind('', true);
        currentDI.setBandeiraRede(getBandeiraRede());
        eSearch = getJExpression(currentDI);
        if (eSearch === 'Error')
           return 'Error';
       translatedJEntitySearch = common.translateSearch(dataContext, eSearch);
        if (!isNullOrEmpty(customSearchResult.searchDefinition)) eSearch += customSearchResult.searchDefinition;
        return eSearch;
    }
    var queryInnerUIs = function (parentEntity, parentTypeName) {
       if (status() === 'C') return;
       commitInternalUIsData();
       for (var idx = 0; idx < vm.internalUIs.length; idx++) { var innerVM = vm[vm.internalUIs[idx]](); if ((!isNullOrEmpty(parentTypeName) && innerVM.getParentSelectorDataName() === parentTypeName) || (!isNullOrEmpty(parentEntity) && innerVM.getParentSelectorDataName() === parentEntity.typeName)) { if (isNullOrEmpty(parentEntity) || innerVM.validParentSelectorDataCondition(parentEntity)) innerVM.dataToolbar.query(false, parentEntity); else if (innerVM.status() === 'Q') innerVM.clear();  } }
    };
    var addNewToInnerUI = function (parentEntity, uiName) {
       setTimeout(function () {
           for (var idx = 0; idx < vm.internalUIs.length; idx++) { var innerVM = vm[vm.internalUIs[idx]](); if (innerVM.getParentSelectorDataName() === parentEntity.typeName && (isNullOrEmpty(uiName) || innerVM.viewName === uiName)) innerVM.dataToolbar.addNew(parentEntity); }
       }, 1000);
    };
    var removeInnerDataUIs = function (parentEntity) {
       for (var idx = 0; idx < vm.internalUIs.length; idx++) { var innerVM = vm[vm.internalUIs[idx]](); if (!isNullOrEmpty(parentEntity) && innerVM.getParentSelectorDataName() === parentEntity.typeName) innerVM.removeParentRelatedItems(parentEntity); }
    };
    var getDataFromInnerUI = function (uiName) {
       for (var idx = 0; idx < vm.internalUIs.length; idx++) { var innerVM = vm[vm.internalUIs[idx]](); if (innerVM.viewName === uiName) return innerVM.currentDataItem(); }
    };
    var saveInnerUIs = function () {
      var vmsForSaving = [];
      var saveInnerUI = function (currentIndex) {
            if (currentIndex < vmsForSaving.length)
                vmsForSaving[currentIndex].dataToolbar.save(false, function () { saveInnerUI(currentIndex + 1); });
      };
      for (var idx = 0; idx < vm.internalUIs.length; idx++) {
          var innerVM = vm[vm.internalUIs[idx]]();
          if (innerVM.status() === 'E') vmsForSaving.push(innerVM);
      }
      if (vmsForSaving.length > 0) {
           saveInnerUI(0);
      }
    };
    var undoInnerUIs = function () {
      for (var idx = 0; idx < vm.internalUIs.length; idx++) { var innerVM = vm[vm.internalUIs[idx]](); if (innerVM.status() === 'E') innerVM.dataToolbar.undo(); }
      if (status() === 'Q' && !isNullOrEmpty(currentDataItem())) {
           for (var idx = 0; idx < vm.internalUIs.length; idx++) { var innerVM = vm[vm.internalUIs[idx]](); innerVM.dataToolbar.clear(); }
           currentDataItem().fillDetails();
      }
    };
    var editInnerUIs = function () {
      for (var idx = 0; idx < vm.internalUIs.length; idx++) { var innerVM = vm[vm.internalUIs[idx]](); if (innerVM.isEditable()) innerVM.dataToolbar.edit(); } 
    };
    var setStatus = function (st) {
      status(st);
      goToIndex(currentDataIndex());
    };
    var dataCache = []; //Initialize data cache
    var syncDataCache = function () {
        dataView().forEach(function (element) {
            if (element.ChangeState && dataCache.indexOf(element) < 0) { dataCache.push(element); }
        });
    }
    var getDataForSaving = function () {
        var result = [];
        dataCache = [];
        if (preserveDataCurrentState()) {
           syncDataCache();
           result = dataCache;
        }
        else {
           result = dataView();
        }
        return _.filter(result, function (e) { return (['U', 'I', 'D'].indexOf(e.ChangeState) >= 0); }).concat(removedEntities);
    }
    var getAllChanges = function () {
        var details = [];
        var changes = getDataForSaving();
        for (var idx = 0; idx < changes.length; idx++) {
           details = details.concat(changes[idx].getAllDetailChanges());
        }
        if (details.length > 0)
             return changes.concat(details);
        else return changes;
    }
    var getAddedEntities = function () {
        var result = [];
        if (preserveDataCurrentState()) {
           syncDataCache();
           result = dataCache;
        }
        else {
           result = dataView();
        }
        return _.filter(result, function (e) { return (e.ChangeState == 'I'); });
    }
    var getRelatedElementsInCache = function () {
        if (parentEntityRelated != null && preserveDataCurrentState()) {
           syncDataCache();
           var cacheElements = dataCache;
           var result = [];
           var relationExpr = '';
           for (var idx = 0; idx < uiSettings.parentFieldsRelation.length; idx++) { relationExpr += (relationExpr === '' ? '' : ' && ') + 'getAbsoluteValue(cacheElements[idxR][uiSettings.detailFieldsRelation[' + idx.toString() + ']]) === getAbsoluteValue(parentEntityRelated[uiSettings.parentFieldsRelation[' + idx.toString() + ']])'; }
           for (var idxR = 0; idxR < cacheElements.length; idxR++) {
               if (eval(relationExpr)) { result.push(cacheElements[idxR]); }
           }
           dataView(result);
           return (dataView().length > 0 ? 0 : (parentEntityRelated.isAdded() ? 0 : -1));
        }
        return -1;
    };
    var isChildVM = function () {
       return (parentVM != null && uiSettings != null && !isNullOrEmpty(uiSettings.parentSelectorDataName) && (typeof uiSettings.parentFieldsRelation !== 'undefined') && (typeof uiSettings.detailFieldsRelation !== 'undefined') && uiSettings.parentFieldsRelation.length == uiSettings.detailFieldsRelation.length) && !isLookup();
    }
    var isLookup = function () {
       return (uiSettings != null && (typeof uiSettings.lookupInfo === 'object'));
    };
    var allowMultiSelectionInSearch = function () {
       if (isLookup() && (typeof uiSettings.allowMultiSelectionInSearch !== 'undefined')) return uiSettings.allowMultiSelectionInSearch;
       else return true;
    };
    var parentEntityRelated = null;
    var freeEntityForQuerying = null;
    var isProcessing = false;
    var adjustExternalParentRelation = function (selectedElement) {
        if (isNullOrEmpty(selectedElement)) selectedElement = currentDataItem();
        if (parentEntityRelated != null && isChildVM() && (uiSettings.canAddNew || uiSettings.canEdit || uiSettings.canDelete)) {
            for (var idx = 0; idx < uiSettings.parentFieldsRelation.length; idx++) { setAbsoluteValue(selectedElement, uiSettings.detailFieldsRelation[idx], getAbsoluteValue(parentEntityRelated[uiSettings.parentFieldsRelation[idx]])); }
        }
    };
    var openingExternalUIFromGrid = function (externalUIName, qbeSearch) {
       return qbeSearch;
    }
    function restoreLastFilter(clearFilters) {
            if (isChildVM()) { filteredEntities = []; return false; }
            if (clearFilters || !common.getLastFilterMode()) filteredEntities = [];
            if (filteredEntities.length === 0) return false;
            dataContext.clearAll();
            //Attach Elements
            for(var idx = 0; idx < filteredEntities.length; idx++) { dataContext.attachEntity(filteredEntities[idx]); }
            //Set Current Details
            for(var idx = 0; idx < filteredEntities.length; idx++) { filteredEntities[idx].setCurrentDetails(null, true); }
            dataView([filteredEntities[0]]);
            if (clearFilters) filteredEntities = [];
            return true;
    }
    
    function adjustNavigationByPage(isNavByPage) {
        navigationByPage(isNavByPage);
        dataBind();
    }
    
    var preserveDataCurrentState = function () {
       return (status() !== 'C' && pageSize() === 0 && isChildVM());
    }
    
    var detachFilteredEntities = function (clear) {
        if (filteredEntities.length > 0) {
            for (var idx = 0; idx < filteredEntities.length; idx++) {
                dataContext.detachEntity(filteredEntities[idx]);
            }
            if (clear) filteredEntities = [];
        }
    }
    
    var query = function (lookupInitializing, parentEntity, quickSearchJExpression, externalQueryCallBack, noMessages, noDetails) {
        if (isProcessing) return;
        isProcessing = true;
        vm.canReportErrors = false;
        if (lookupInitializing === true && uiSettings && uiSettings.modalForm && (typeof uiSettings.modalForm.hide === 'function')) uiSettings.modalForm.hide(true);
        if (!isNullOrEmpty(parentEntity) && !isNullOrEmpty(parentEntity.typeName))
           parentEntityRelated = parentEntity;
        else
           parentEntityRelated = null;
        if ((isNullOrEmpty(parentEntityRelated) || (status() === 'C' && (parentEntityRelated != null && parentEntityRelated.isAdded()))) && isChildVM()) { dataContext.clearAll(); dataCache = []; if (isNullOrEmpty(parentEntityRelated)) { currentDataItem(null); querySucceeded({ results: [] }); return complete(); } }
        if ((status() !== 'C' || (parentEntityRelated != null && parentEntityRelated.isAdded())) && getRelatedElementsInCache() >= 0) { querySucceeded({ results: dataView() }); return complete(); }
        if (freeEntityForQuerying == null && isChildVM()) freeEntityForQuerying = dataContext.createFreeEntity('Venda');
        if (status() === 'C' && !isNullOrEmpty(currentDataItem()) && currentDataItem().getCurrentElements) {
            filteredEntities = currentDataItem().getCurrentElements();
            if (isChildVM())
                detachFilteredEntities(true);
        }
        else
            filteredEntities = [];
        if (uiSettings != null && uiSettings.noSearch) { dataView([currentDataItem()]); status('Q'); refreshToolbar(); return complete(); }
        lastJEntitySearch = (isNullOrEmpty(quickSearchJExpression) ? '' : quickSearchJExpression) + getQueryFilter((isChildVM() ? freeEntityForQuerying : currentDataItem()));
        if (lastJEntitySearch === 'Error')
            return complete();
        var hasError = true;
        if (status() === 'C') { detachFilteredEntities(); }
        if (!preserveDataCurrentState()) dataCache = [];
        if (isChildVM() && (uiSettings.canAddNew || uiSettings.canEdit || uiSettings.canDelete))
           status(parentVM.status());
        if (!_noBusyLoading) vm.showProcessing('Pesquisando informações...');
        return dataContext.getVendaByEntitySearchNoAssociations(lastJEntitySearch, 0, pageSize(), (pageSize() > 0), preserveDataCurrentState(), status() !== 'E', sortInfo).then(querySucceeded).fin(complete);
    
        function complete() {
            isProcessing = false;
            if (!_noBusyLoading) vm.closeProcessing();
            if (hasError === true && lookupInitializing === true && isLookup() && (parentVM != null)) {
               parentVM.UI_Close_Click();
            }
            else if (hasError === true) {
               clear();
            }
        }
    
        function querySucceeded(data) {
            if (vm.status() !== 'E') { for (var idx = 0; idx < data.results.length; idx++) { dataContext.initializePOCO(data.results[idx], 'Venda'); } }
            hasError = false;
            dataView(data.results);
            if (dataView().length === 0 && (parentVM == null || (parentVM != null && uiSettings != null && isNullOrEmpty(uiSettings.parentSelectorDataName)) || isLookup())) {
                if (isLookup() && (parentVM != null) && lookupInitializing === true) {
                   uiSettings.ownerReference.clearLookUp(uiSettings.lookupName);
                   app.showMessage('A informação de Lookup [' + uiSettings.ownerReference.getDisplayName(uiSettings.fieldToSearch) + '] não foi encontrada!', 'Informação', ['Ok']);
                   parentVM.UI_Close_Click();
                   return;
                }
                else  {
                   if (!noMessages) { app.showMessage('Nenhum registro foi encontrado!', 'Informação', ['Ok']); }
                   refreshToolbar();
                }
                if (restoreLastFilter()) {
                   pageCount(1);
                   totalItemCount(1);
                   currentPage(0);
                   status('C');
                   goToIndex(0);
                   dataBind();
                   isBusy(false);
                }
                else {
                   clear();
                }
                return true;
            }
            pageCount( (pageSize() > 0 ? Math.ceil((data.inlineCount ? data.inlineCount : dataView().length) / pageSize()) : 1) );
            totalItemCount((data.inlineCount ? data.inlineCount : dataView().length));
            currentPage(0);
            if (!(isChildVM() && (uiSettings.canAddNew || uiSettings.canEdit || uiSettings.canDelete)))
               status('Q');
            clearInnerUIs();
            goToIndex(0, noDetails);
            if (isLookup() && (parentVM != null) && (dataView().length === 1) && lookupInitializing === true) {
               if (uiSettings.lookupInfo.isMultiSelection === true && (typeof currentDataItem().IsSelected === 'function')) currentDataItem().IsSelected(true);
               parentVM.UI_selectOption('Ok');
               return;
            }
            if (lookupInitializing === true && uiSettings.modalForm && (typeof uiSettings.modalForm.hide === 'function')) uiSettings.modalForm.hide(false);
            dataBind((isChildVM() ? '' : 'dataView'));
            if (common.getGridMode() == 'G' && !vm.navigationByPage() && (viewType() === 'Main') && !isChildVM() && dataView().length > 1 && (parentVM == null))
                dataToolbar.viewInfo();
            if (typeof externalQueryCallBack === 'function') externalQueryCallBack();
        }
    };
    function goToIndex(index, noDetails) {
        if (dataView().length === 0) { currentDataIndex(0); currentDataItem(null); return true; }
        if (index < 0) { index = 0; }
        else if (index >= dataView().length) { index = dataView().length - 1; }
        currentDataIndex(index);
        var oldValue = currentDataItem();
        currentDataItem(dataView()[index]);
        if (status() !== 'C' && currentDataItem() !== null && oldValue !== currentDataItem()) {
           if (!noDetails) currentDataItem().fillDetails();
        }
        resizeToolbar();
    }
    function goToItem(item) {
            goToIndex(dataView().indexOf(item));
    }
    function goToKey(primaryKey, value, currentElement, viewSource) {
        if (!viewSource) viewSource = dataView;
        for (var idx = 0; idx < viewSource().length; idx++) {
            var dataValue = viewSource()[idx][primaryKey];
            if (typeof dataValue === 'function') dataValue = dataValue();
            if (dataValue == value) {
                if (currentElement && currentElement()) { currentElement().commitDetailsVisualPendings(); currentElement(viewSource()[idx]); currentElement().fillDetails(); } else { if (currentDataItem()) { currentDataItem().commitDetailsVisualPendings(); } goToIndex(idx); }
                break;
            }
        }
    }
    var sortData = function (sortDef) {
        if (status() === 'Q' && pageCount() > 1 && sortInfo != sortDef) {
           sortInfo = sortDef;
           refresh(0, false);
        }
    };
    var refresh = function (curPage, goLast) {
        vm.showProcessing('Pesquisando informações...');
        return dataContext.getVendaByEntitySearchNoAssociations(lastJEntitySearch, curPage * pageSize(), pageSize(), false, false, status() !== 'E', sortInfo).then(querySucceeded).fin(complete);
    
        function complete() {
            vm.closeProcessing();
        }
    
        function querySucceeded(data) {
            if (vm.status() !== 'E') { for (var idx = 0; idx < data.results.length; idx++) { dataContext.initializePOCO(data.results[idx], 'Venda'); } }
            dataView(data.results);
            currentPage(curPage);
            goToIndex((goLast ? dataView().length : 0));
            dataBind('dataView');
        }
    };
    var clearByUser = function () {
        if (!isNullOrEmpty(customSearchResult.searchDefinition)) {
            app.showMessage('Deseja limpar a pesquisa avançada?', 'Alerta', ['Yes', 'No'])
            .then(function (selectedOption) {
                if (selectedOption === 'Yes') {
                    customSearchResult.searchDefinition = '';
                    customSearchResult.serializedSearch = '';
                    customSearchResult.translatedSearch = '';
                    hasCustomSearches(false);
                }
                return clear();
             });
        }
        else return clear();
    }
    var clear = function (noBindingReport) {
        if (uiSettings && parentVM && uiSettings.noSearch === true && parentVM.status() !== 'C') return;
        vm.canReportErrors = false;
        parentEntityRelated = null;
        isBusy(true);
        lastStatus = status();
        status('C');
        if (restoreLastFilter(lastStatus === 'C')) return clearComplete({ results: dataView() }, true);
        else return dataContext.clearVenda(getBandeiraRede(), clearComplete);
    
        function clearComplete(data, holdRanges) {
            dataForUndo = [];
            dataCache = []; //Initialize data cache
            removedEntities = []; //Initialize removeds
            dataView(data.results);
            if (holdRanges != true) vm.entitySearchRange.clear();
            if (typeof noBindingReport === 'boolean' && noBindingReport === true) { pageCount(1); currentPage(0); goToIndex(0); return; }
            pageCount(1);
            totalItemCount(data.results.length);
            lastStatus = 'C';
            currentPage(0);
            goToIndex(0);
            adjustFormView();
            dataBind();
            isBusy(false);
            hideButtonsEditorTemplate();
            clearInnerUIs();
            scrollMainTop();
        }
    };
    var hasChanges = ko.computed(function () {
            return dataContext.hasChanges();
    });
    var hasInternalUIsValidationErrors = function () {
        for (var idx = 0; idx < vm.internalUIs.length; idx++) { var innerVM = vm[vm.internalUIs[idx]](); if (innerVM.status() === 'E' && innerVM.hasValidationErrors()) return true; }
        return false;
    };
    var hasInternalUIsSavingErrors = function () {
        for (var idx = 0; idx < vm.internalUIs.length; idx++) { var innerVM = vm[vm.internalUIs[idx]](); if (innerVM.status() === 'E' && !innerVM.onSavingValidation()) return true; }
        return false;
    };
    var commitInternalUIsData = function () {
        for (var idx = 0; idx < vm.internalUIs.length; idx++) { var innerVM = vm[vm.internalUIs[idx]](); innerVM.dataBind('', true); }
    };
    var onSavingValidation = function (changes) {
        if (!changes) changes = getAllChanges();
        if (changes.length === 0) { return true; }
        for (var idxChange = 0; idxChange < changes.length; idxChange++) {
            var entity = changes[idxChange];
            if (typeof entity.OnSaving == 'function') {
               if (!entity.OnSaving()) { return false; }
            }
        }
        return true;
    }
    var hasValidationErrors = function () {
       vm.canReportErrors = true;
       return dataContext.hasValidationErrors(dataView());
    }
    var saveFakeInnerUIs = function (transactionID, saveCompleteCallback) {
        var vmsForSaving = [];
        var saveFakeInnerUI = function (currentIndex) {
            if (currentIndex < vmsForSaving.length)
                vmsForSaving[currentIndex].dataToolbar.save(false, function () {}, transactionID,  function () {}, function () { currentIndex ++; saveFakeInnerUI(currentIndex); });
            else if(saveCompleteCallback) saveCompleteCallback();
        };
        for (var idx = 0; idx < vm.internalUIs.length; idx++) {
            var innerVM = vm[vm.internalUIs[idx]]();
            if (innerVM.status() === 'E' && innerVM.getAllChanges().length > 0) vmsForSaving.push(innerVM);
        }
        saveFakeInnerUI(0);
    };
    
    var getTransactionID = function () {
        if (isNullOrEmpty(vm.transactionID))
            vm.transactionID = dataContext.getNewGuid();
        return vm.transactionID;
    }
    
    var getViewMapInfo = function () {
       if (!isChildVM() || isNullOrEmpty(parentVM)) return '';
       return 'ViewNameParent:'+parentVM.__moduleId__+
           ';EntityNameParent:' + parentEntityRelated.typeName + ';FieldsParent:' + uiSettings.parentFieldsRelation.join(',') +
           ';Fields:'+uiSettings.detailFieldsRelation.join(',')+';'
    }
    
    var saveFake = function (transactionID, externalSaveSucceeded, saveCompleteCallback, internalUiCallback) {
        return dataContext.saveChangesFake(transactionID, saveSucceeded)
        function saveSucceeded(saveResult) {
            vm.closeProcessing();
            saveFakeInnerUIs(transactionID, function () { if (typeof saveCompleteCallback === 'function') { saveCompleteCallback(); } });
            if (typeof externalSaveSucceeded === 'function' && parentVM == null && vm.internalUIs.length > 0) externalSaveSucceeded();
            if (typeof internalUiCallback === 'function') internalUiCallback();
        }
    }
    
    var submitAllChanges = function (saveFailed) {
        var transactionId = getTransactionID();
        vm.showProcessing('Salvando informações...');
        isSaving(true);
        return dataContext.submitAllChanges(transactionId, saveSucceeded, failed, completed)
        function saveSucceeded(saveResult) {
            vm.saveSuccessInnerUIs(saveResult, completed);
        }
        function completed(){
            vm.canReportErrors = false;
            vm.closeProcessing();
            isSaving(false);
        }
        function failed(error) {
            if(typeof completed === 'function') completed();
            showModalAlert('Houve uma falha ao salvar a transação.' , [common.getExceptionDescription(error, ['Exception has been thrown by the target of an invocation.<br/>   ', 'Fail by saving data:'])]);
            dataContext.cancelAllChanges(transactionId, function(success){
               if (saveFailed) saveFailed();
            }, function(error){
                throw error;
            });
        }
    }
    
    var saveSuccessInnerUIs = function (saveResults, completed) {
        var saveResult = saveResults[vm.__moduleId__];
        if(saveResult) {
           saveSucceeded(saveResult);
        }
        for (var idx = 0; idx < vm.internalUIs.length; idx++) { var innerVM = vm[vm.internalUIs[idx]](); innerVM.saveSuccessInnerUIs(saveResults); }
        if(typeof completed === 'function') completed();
    }
    
    var save = function (isExclusion, externalSaveSucceeded, transactionId, saveCompleteCallback, internalUiCallback) {
        if (typeof isExclusion !== 'boolean') isExclusion = false;
        if (isExclusion) { enableDataTrack(false, false); }
        var indexForUndoAction = currentDataIndex();
        if (isExclusion) { removeItem(); }
        commitInternalUIsData();
        dataBind('', true);
        vm.changes = getAllChanges();
        if (!onSavingValidation(vm.changes)) { if (isExclusion) return undo(indexForUndoAction); else return; }
        if (hasInternalUIsSavingErrors()) { if (isExclusion) return undo(indexForUndoAction); else return; }
        if (hasInternalUIsValidationErrors() || hasValidationErrors()) { if (isExclusion) return undo(indexForUndoAction); else { refreshToolbar(); return dataBind(); } }
        isSaving(true);
        if (!isExclusion && currentDataItem() && currentDataItem().checkForSendingAllRowsToServer) { currentDataItem().checkForSendingAllRowsToServer(); }
        vm.showProcessing('Salvando informações...');
        if (isNullOrEmpty(transactionId) && parentVM == null && vm.internalUIs.length > 0){
            transactionId = getTransactionID();
            saveCompleteCallback = function(){ if(!isChildVM()) dataToolbar.submitAllChanges(saveFailed); }
        }
        if (!isNullOrEmpty(transactionId)){
            try{ dataToolbar.saveFake(transactionId, externalSaveSucceeded, saveCompleteCallback, internalUiCallback); }
            catch(e) { showModalAlert('Houve uma falha ao salvar as informações.', [e.message]); }
            return;
        }
        return dataContext.saveChanges(saveSucceeded, saveFailed, complete, true);
    
        function complete() {
            vm.canReportErrors = false;
            vm.closeProcessing();
            isSaving(false);
        }
    
        function saveFailed(error) {
            if (isChildVM()) parentVM.dataToolbar.edit(true);
            if (isExclusion) return undo(indexForUndoAction); else return dataBind();
        }
    };
    
        function saveSucceeded(saveResult) {
            dataForUndo = [];
            dataCache = []; //Initialize data cache
            removedEntities = []; //Initialize removeds
            var toList = dataView();
            var fromList = saveResult;
            for (var idxElem = toList.length - 1; idxElem >= 0; idxElem--) {
               if (toList[idxElem].ChangeState === 'D') toList.splice(idxElem, 1);
            }
            for (var idxElem = toList.length - 1; idxElem >= 0; idxElem--) {
                    if (toList[idxElem].ChangeState !== 'N') {
                       var fromObj = [];
                       if (toList[idxElem].ChangeState == 'I') {
                           fromObj = _.where(fromList, { TemporaryIdVenda: toList[idxElem]['IdVenda'] });
                       } else {
                           fromObj = _.where(fromList, { IdVenda: toList[idxElem]['IdVenda'] });
                       }
                       if (fromObj.length > 0) { toList[idxElem].copyDataFrom(fromObj[0], true); }
                    }
            }
            if (dataView().length === 0 && !isChildVM()) return clear();
            if (dataView().length > 0) goToIndex(currentDataIndex());
            for (var idxChange = 0; idxChange < vm.changes.length; idxChange++) {
                var entity = vm.changes[idxChange];
                if (entity.isUnchanged() && !isNullOrEmpty(getAbsoluteValue(entity.TableMedia))) { setAbsoluteValue(entity, 'TableMedia', null); entity.setUnchanged(); }
                if (typeof entity.OnSaved == 'function') {
                   entity.OnSaved();
                }
            }
            //if (isChildVM())
            //{
            //   dataContext.clearAll();
            //   query(false, parentEntityRelated);
            //}
            lastStatus = 'Q';
            status('Q');
            refreshToolbar();
            if (typeof externalSaveSucceeded == 'function') {
                externalSaveSucceeded();
            }
            dataBind();
            resizeToolbar();
        }
    var dataForUndo = [];
    var undo = function (indexForUndoAction) {
        vm.canReportErrors = false;
        dataContext.cancelChanges(dataForUndo);
        if ((typeof indexForUndoAction) === 'number' && !navigationByPage() && !isChildVM()) lastStatus = 'Q';
        if (lastStatus === 'C' || dataForUndo.length == 0) {
            clear();
        } else {
            dataView(dataForUndo);
            dataForUndo = [];
            hideButtonsEditorTemplate();
            dataCache = []; //Initialize data cache
            removedEntities = []; //Initialize removeds
            status(lastStatus);
            var parentList = dataView();
            for (var idx = 0; idx < parentList.length; idx++) {
                if (['U', 'I', 'D'].indexOf(parentList[idx].ChangeState) >= 0) { parentList[idx].restoreOriginal(); parentList[idx].adjustDetailsLoaded(false); }
            }
            goToIndex(((typeof indexForUndoAction) === 'number' ? indexForUndoAction : currentDataIndex()));
            dataBind();
            undoInnerUIs();
        }
    };
    var hideButtonsEditorTemplate = function () {
       if ($('.addReg').is(':visible')) {
           $('.addReg :visible').each(function (index) {
               $('.addReg').hide();
               $('.delReg').hide();
           });
       }
    };
    var showButtonsEditorTemplate = function () {
       if ($('.toolbar-dialog-template').is(':visible')) {
           $('.toolbar-dialog-template :visible').parent().find('button.addReg').show();
           $('.toolbar-dialog-template :visible').parent().find('button.delReg').show();
       }
    };
    var print = function () {
        return true;
    };
    var acceptChanges = function () {
        if (!navigationByPage() && !isChildVM()) dataContext.acceptChanges();
    };
    var edit = function (noClearInnerUIs) {
        if (status() === 'E') { refreshToolbar(); return; }
        if (!canAddChangeEntity()) return;
        acceptChanges();
        lastStatus = status();
        status('E');
        if (!noClearInnerUIs) clearInnerUIs();
        goToIndex(currentDataIndex());
        if (lastStatus === 'Q') dataForUndo = [].concat(dataView());
        //Enabling data track
        enableDataTrack(navigationByPage() || isChildVM(), true);
        editInnerUIs();
        showButtonsEditorTemplate();
    };
    var enableDataTrack = function (all, convertDetails) {
        adjustFormView();
    };
    var setBandeiraRede = function () {
    };
    
    var createVenda = function() {
        dataBind('dataView', true);
        var entity = dataContext.createVenda();
        if(!entity) return null;
        adjustExternalParentRelation(entity);
        entity.setBandeiraRede(getBandeiraRede());
        entity.setGpecon(getGpecon());
        dataView.push(entity);
        return entity;
    };
    
    var createAndNotifyVenda = function() {
        var entity = createVenda();
        notifyPresentation('');
        return entity;
    };
    var notifyPresentation = function(dataSourceName) {
          return dataContext.notifyPresentation(dataSourceName);
    };
    
    var notifyInnerElements = function (element, isExpander) {
        if (element)
        {
            dataBind('', true);
            try{ $(window).trigger('resize'); } catch(e){ console.log(e); }
            var innerElements = element.find("table");
            if (innerElements.length > 0 && (vm.dataSource.length > 0 || vm.internalUIs.length > 0)) {
                for (var idx = 0; idx < innerElements.length; idx++) {
                    if($(innerElements[idx]).parents('.tab-pane').hasClass('active') || isExpander) {
                        for (var db in vm.dataSource) { if (vm.dataSource[db].key == innerElements[idx].id) vm.dataSource[db].itemsSource.dataBind(false, true); }
                        //Notifying inner UIs
                        for (var idxUI = 0; idxUI < vm.internalUIs.length; idxUI++) {
                           var innerVM = vm[vm.internalUIs[idxUI]]();
                           for (var db in innerVM.dataSource) {
                               if (innerVM.dataSource[db].key == innerElements[idx].id)
                                   innerVM.dataSource[db].itemsSource.dataBind(false, true);
                           }
                        }
                    }
                }
            }
        }
    };
    var createEntity = function(entityName, initialValues) {
        var entity = dataContext.createEntity(entityName, initialValues);
        entity.setBandeiraRede(getBandeiraRede());
        entity.setGpecon(getGpecon());
        return entity;
    };
    var getBandeiraRede = function() {
        if (uiSettings != null && uiSettings.lookupInfo && uiSettings.lookupInfo.vm && (typeof uiSettings.lookupInfo.vm.getBandeiraRede === 'function')) return uiSettings.lookupInfo.vm.getBandeiraRede();
        else if (parentVM != null && (typeof parentVM.getBandeiraRede === 'function')) return parentVM.getBandeiraRede();
        else if (uiSettings != null && uiSettings.parentUI && uiSettings.parentUI.vm && (typeof uiSettings.parentUI.vm.getBandeiraRede === 'function')) return uiSettings.parentUI.vm.getBandeiraRede();
        else if (!isNullOrEmpty(vm.currentBrands) && vm.currentBrands.indexOf(',') === -1) return parseInt(vm.currentBrands);
        else return 0;
    };
    var getCurrentBrands = function() {
        if (uiSettings != null && uiSettings.lookupInfo && uiSettings.lookupInfo.vm  && uiSettings.lookupInfo.vm.hasBrand && (typeof uiSettings.lookupInfo.vm.getCurrentBrands === 'function')) return uiSettings.lookupInfo.vm.getCurrentBrands();
        else if (parentVM != null && parentVM.hasBrand && (typeof parentVM.getCurrentBrands === 'function')) return parentVM.getCurrentBrands();
        else if (uiSettings != null && uiSettings.parentUI && uiSettings.parentUI.vm  && uiSettings.parentUI.vm.hasBrand && (typeof uiSettings.parentUI.vm.getCurrentBrands === 'function')) return uiSettings.parentUI.vm.getCurrentBrands();
        else return (isNullOrEmpty(vm.currentBrands) ? '0' : vm.currentBrands);
    };
    var showProcessing = function(message) {
        currentActivityInformation(message);
        isBusy(true);
    };
    var closeProcessing = function() {
        currentActivityInformation('');
        isBusy(false);
    };
    var getGpecon = function() {
        if (!isNullOrEmpty(managerAuth.loginInfo.IdLinxGrupoEconomico)) return parseInt(managerAuth.loginInfo.IdLinxGrupoEconomico);
        else return 0;
    };
    var deleteEntity = function (entity, isMultiSelection) {
        var selectedEntities = []
        if (isMultiSelection && !isNullOrEmpty(complement) && (typeof complement.selectedItems === 'function'))
            selectedEntities = complement.selectedCurrentItems(false, true);
        if (selectedEntities.length > 0) {
           for (var idx = 0; idx < selectedEntities.length; idx++) {
               var selectedEntity = selectedEntities[idx];
               if (typeof selectedEntity.OnDeleting == 'function') {
                   if (!selectedEntity.OnDeleting()) return false;
               }
               removeInnerDataUIs(selectedEntity);
               dataContext.deleteEntity(selectedEntity);
               if (selectedEntity.typeName == vm.rootDataTypeName) {
                   if (selectedEntity.ChangeState == 'D') removedEntities.push(selectedEntity);
                   dataView.remove(selectedEntity);
               }
               if (typeof selectedEntity.OnDeleted == 'function') {
                   selectedEntity.OnDeleted();
               }
           }
           if (typeof complement.clearSelectedItems === 'function') complement.clearSelectedItems();
               return true;
        }
        else {
           if (typeof entity.OnDeleting == 'function') {
               if (!entity.OnDeleting()) return false;
           }
           removeInnerDataUIs(entity);
           dataContext.deleteEntity(entity);
           if (typeof entity.OnDeleted == 'function') {
               entity.OnDeleted();
           }
           if (entity.typeName === vm.rootDataTypeName && typeof isMultiSelection !== 'undefined') {
               if (entity.ChangeState == 'D') removedEntities.push(entity);
           }
        }
        return true;
    };
    var canAddChangeEntity = function () {
       return true;
    };
    var addNew = function (parentEntity) {
        if (!dataContext.dataParameters.isLoaded) {
           setTimeout(function () {
               addNew(parentEntity);
           }, 1000);
           return;
        }
        if (status() === 'Q' && !navigationByPage() && !isChildVM()) clear();
        if (parentEntity != null && (typeof parentEntity === 'object') && !isNullOrEmpty(parentEntity.typeName))
           parentEntityRelated = parentEntity;
        if (!canAddChangeEntity()) return;
        acceptChanges();
        if (status() === 'C') {
            dataContext.clearAll();
            dataView([]);
        }
        if (status() === 'Q') {
           adjustFormView();
           dataForUndo = [].concat(dataView());
           if (navigationByPage()) enableDataTrack(true, true);
        }
        if (status() !== 'E') {
            lastStatus = status();
            status('E');
        }
        goToItem(createVenda());
        editInnerUIs();
        showButtonsEditorTemplate();
        dataBind();
    };
    var remove = function () {
        acceptChanges();
        app.showMessage('Deseja realmente excluir o registro selecionado?', 'Alerta', ['Yes', 'No'])
            .then(function (selectedOption) {
                if (selectedOption === 'Yes') {
                    if (!navigationByPage() && !isChildVM()) { dataForUndo = [].concat(dataView()); save(true); } else { removeItem(); }
                }
                return selectedOption;
             });
    };
    var removeParentRelatedItems = function (parentEntity) {
        var removedIdx = []
        for (var idx = 0; idx < dataView().length; idx++) {
           var isRelated = true;
           if (uiSettings != null && uiSettings.parentFieldsRelation.length == uiSettings.detailFieldsRelation.length) {
               for (var j = 0; j < uiSettings.parentFieldsRelation.length; j++) {
                   if (getAbsoluteValue(dataView()[idx][uiSettings.detailFieldsRelation[j]]) !== getAbsoluteValue(parentEntity[uiSettings.parentFieldsRelation[j]])) {
                       isRelated = false;
                   }
               }
           }
           if (isRelated) {
               deleteEntity(dataView()[idx]);
               removedIdx.push(idx);
           }
        }
        for (var i = removedIdx.length - 1; i >= 0; i--) {
           dataView().splice(removedIdx[i], 1);
        }
        goToIndex(0);
        dataBind();
    }
    var removedEntities = [];
    var removeItem = function () {
        if (deleteEntity(currentDataItem()) === false) return false;
        var index = dataView.indexOf(currentDataItem());
        if (currentDataItem().ChangeState == 'D') removedEntities.push(currentDataItem());
        dataView.remove(currentDataItem());
        if (dataView().length > 0) {
            if (status() !== 'E') {
                lastStatus = status();
                status('E');
            }
            if (index > 0) { goToIndex(index-1); }
            else { goToIndex(0); }
            dataBind();
        }
        else {
            goToIndex(0);
            dataBind();
        }
    };
    var goFirst = function () {
        var item;
        if (navigationByPage() || (viewType() === 'Secundary') || (!(pageCount() === 1 || pageSize() === 0 || currentPage() === 0))) {
            item = refresh(0, false);
        } else {
            item = goToIndex(0);
        }
        return item;
    };
    var goBack = function () {
        var item;
        if (navigationByPage() || (viewType() === 'Secundary') || (!(pageCount() === 1 || pageSize() === 0 || currentPage() === 0) && currentDataIndex() === 0)) {
            item = refresh(currentPage()-1, !navigationByPage());
        } else {
            item = goToIndex(currentDataIndex()-1);
        }
        return item;
    };
    var goForward = function () {
        var item;
        if (navigationByPage() || (viewType() === 'Secundary') || (!(pageCount() === 1 || pageSize() === 0 || currentPage() === (pageCount()-1)) && currentDataIndex() === (dataView().length-1))) {
            item = refresh(currentPage()+1, false);
        } else {
            item = goToIndex(currentDataIndex()+1);
        }
        return item;
    };
    var goLast = function() {
        var item;
        if (!navigationByPage() && (viewType() === 'Main') && (pageCount() === 1 || pageSize() === 0 || currentPage() === (pageCount()-1))) {
            item = goToIndex(dataView().length-1);
        } else {
            item = refresh(pageCount()-1, !navigationByPage() && (viewType() === 'Main'));
        }
        return item;
    };
    //Databar enable control
    var _canRefreshData = true, _canQuickSearch = true, _canAddNew = true, _canClear = true, _canCustomSearch = true, _canDelete = true, _canEdit = true, _canLayout = true, _canNavigate = true, _canPrint = true, _canSearch = true, _canExport = true, _noBusyLoading = false;
    var setSecurity = function(pCanAddNew, pCanClear, pCanCustomSearch, pCanDelete, pCanEdit, pCanLayout, pCanNavigate, pCanPrint, pCanSearch, pCanExport, pNoBusyLoading) {
       _canAddNew = pCanAddNew;
       _canClear = pCanClear;
       _canCustomSearch = pCanCustomSearch;
       _canDelete = pCanDelete;
       _canEdit = pCanEdit;
       _canLayout = pCanLayout;
       _canNavigate = pCanNavigate;
       _canPrint = pCanPrint;
       _canSearch = pCanSearch;
       _canExport = pCanExport;
       _noBusyLoading = pNoBusyLoading
       refreshToolbar();
    };
    var refreshToolbar = function() {
       status.notifySubscribers();
       currentDataItem.notifySubscribers();
       canNavigate.notifySubscribers();
    }
    var refreshCurrentBind = function() {
       currentDataItem.notifySubscribers();
    }
    var isReportComposition = function (reportName) {
        if (!isNullOrEmpty(reportName))
        {
            for (var idx in dataContext.entityNames)
            {
                if (dataContext.entityNames[idx].indexOf('ParentComposition') > -1 && reportName.indexOf(vm.rootNamespace + '.' + dataContext.entityNames[idx]) > -1)
                    return true;
            }
        }
        return false;
    }
    var canGoFirst = ko.computed(function () { return (status() === 'Q' || (status() === 'E' && isChildVM())) && _canNavigate && ((!navigationByPage() && (viewType() === 'Main') && currentRecord() > 0) || ((navigationByPage() || (viewType() === 'Secundary')) && currentPage() > 0)); });
    var canGoBack = ko.computed(function () { return (status() === 'Q' || (status() === 'E' && isChildVM())) && _canNavigate && ((!navigationByPage() && (viewType() === 'Main') && currentRecord() > 0) || ((navigationByPage() || (viewType() === 'Secundary')) && currentPage() > 0)); });
    var canGoForward = ko.computed(function () { return (status() === 'Q' || (status() === 'E' && isChildVM())) && _canNavigate && ((!navigationByPage() && (viewType() === 'Main') && currentRecord() < (totalRecords()-1)) || ((navigationByPage() || (viewType() === 'Secundary')) && currentPage() < (pageCount()-1))); });
    var canGoLast = ko.computed(function () { return (status() === 'Q' || (status() === 'E' && isChildVM())) && _canNavigate && ((!navigationByPage() && (viewType() === 'Main') && currentRecord() < (totalRecords()-1)) || ((navigationByPage() || (viewType() === 'Secundary')) && currentPage() < (pageCount()-1))); });
    var canClear = ko.computed(function () { return ['C', 'Q'].indexOf(status()) >= 0 && _canClear && !isChildVM(); });
    var canExport = ko.computed(function () { return (status() === 'Q' || status() === 'C') && _canExport; });
    var canGridExport = ko.computed(function () { return status() === 'Q' && _canExport; });
    var canQuery = ko.computed(function () { return status() === 'C' && _canSearch && !isChildVM(); });
    var canCustomSearch = ko.computed(function () { return status() === 'C' && _canCustomSearch && !isChildVM(); });
    var canQuickSearch = ko.computed(function () { return false; });
    var hasDataFeed = ko.computed(function () { return status() === 'C' && _canSearch && dataContext.hasDataFeed && parentVM == null && !isChildVM(); });
    var canAddNew = ko.computed(function () { return ((['Q', 'C'].indexOf(status()) >= 0 && !isChildVM()) || (status() === 'E' && (navigationByPage() || isChildVM()))) && _canAddNew; });
    var canRemove = ko.computed(function () { return (dataView().length > 0) && ((!navigationByPage() && !isChildVM() && status() === 'Q') || (status() === 'E' && !navigationByPage() && isChildVM())) && _canDelete; });
    var canEdit = ko.computed(function () { return status() === 'Q' && _canEdit && !isChildVM(); });
    var canRefreshCurrentData = ko.computed(function () { return status() === 'Q' && _canSearch && _canRefreshData && !isChildVM(); });
    var canUndo = ko.computed(function () { return status() === 'E' && (_canEdit || _canAddNew) && !isChildVM(); });
    var canNavigate = ko.computed(function () { return  (!canUndo() && !canQuery() && (dataView().length > 1 || pageCount() > 1) && _canNavigate); });
    var canPrint = ko.computed(function () { return ['C', 'Q'].indexOf(status()) >= 0 && _canPrint && !isChildVM(); });
    var canSave = ko.computed(function () {
           return !isSaving() && status() === 'E' && (_canEdit || _canAddNew) && !isChildVM();
    });
    var enabledForEditing = ko.computed(function () {
            return ['E', 'C'].indexOf(status()) >= 0;
    });
    var isEditable = function () {
        return _canEdit;
    };
    var viewInfo = function () {
        changeFormView();
    };
    var adjustFormView = function () {
        if (!hasMainTopDataGrid() && (status() === 'E' || status() === 'C') && viewType() === 'Secundary') changeFormView();
    }
    var removeFormViewControl = function () {
        var front = $('#UIExternaTab_formViewer_front')[0];
        if (front) front.removeClassName('front');
        var back = $('#UIExternaTab_formViewer_back')[0];
        if (back) { back.removeClassName('back'); back.addClassName('hide'); }
    }
    var changeFormView = function () {
        if (hasMainTopDataGrid() || isChildVM()) return;
        var panel = $('#UIExternaTab_formViewer')[0];
        if (viewType() === 'Main') panel.addClassName('flip');
        else panel.removeClassName('flip');
        if (viewType() === 'Main') viewType('Secundary');
        else viewType('Main');
        if (viewType() === 'Secundary') { dataBind('dataView'); } else { dataBind(); queryInnerUIs(currentDataItem()); };
    }
    var canViewInfo = ko.computed(function () {
        return !hasMainTopDataGrid() && status() !== 'E' && totalRecords() > 0 && !isChildVM();
    });
    var importPhoto = function () {
        require(['viewmodels/shared/modalMultimidiaBatch'], function (modalMultimidiaBatch) {
            modalMultimidiaBatch.showModal(dataContext).then(function (r, data) { });
        });
    };
    
    var entitySearchRange = {
        predefinedFilters: ko.observableArray(managerPredefined.predefinedFilters),
            VendaDatetimeVenda_typeRange: ko.observable('R'), VendaDatetimeVenda_begin: ko.observable(null), VendaDatetimeVenda_end: ko.observable(null), VendaDatetimeVenda_predefFilter: ko.observableArray([]), VendaDatetimeVenda_predefValue: ko.observable(null),
        VendaBigIntVenda_begin: ko.observable(null), VendaBigIntVenda_end: ko.observable(null),
        VendaDecimalVenda_begin: ko.observable(null), VendaDecimalVenda_end: ko.observable(null),
        VendaIdVenda_begin: ko.observable(null), VendaIdVenda_end: ko.observable(null),
        VendaIntVenda_begin: ko.observable(null), VendaIntVenda_end: ko.observable(null),
        VendaSmallIntVenda_begin: ko.observable(null), VendaSmallIntVenda_end: ko.observable(null),
        VendaIdCliente: ko.observable(null),
        VendaIdLoja: ko.observable(null)
    };
    entitySearchRange.clear = function(){
            entitySearchRange.VendaDatetimeVenda_typeRange('R'); entitySearchRange.VendaDatetimeVenda_begin(null); entitySearchRange.VendaDatetimeVenda_end(null); entitySearchRange.VendaDatetimeVenda_predefFilter([]); entitySearchRange.VendaDatetimeVenda_predefValue(null);
        entitySearchRange.VendaBigIntVenda_begin(null); entitySearchRange.VendaBigIntVenda_end(null);
        entitySearchRange.VendaDecimalVenda_begin(null); entitySearchRange.VendaDecimalVenda_end(null);
        entitySearchRange.VendaIdVenda_begin(null); entitySearchRange.VendaIdVenda_end(null);
        entitySearchRange.VendaIntVenda_begin(null); entitySearchRange.VendaIntVenda_end(null);
        entitySearchRange.VendaSmallIntVenda_begin(null); entitySearchRange.VendaSmallIntVenda_end(null);
        entitySearchRange.VendaIdCliente(null);
        entitySearchRange.VendaIdLoja(null);
    };
    entitySearchRange.has_VendaBigIntVenda = ko.computed(function(){ return (entitySearchRange.VendaBigIntVenda_begin() != null || entitySearchRange.VendaBigIntVenda_end() != null); });
    entitySearchRange.has_VendaDecimalVenda = ko.computed(function(){ return (entitySearchRange.VendaDecimalVenda_begin() != null || entitySearchRange.VendaDecimalVenda_end() != null); });
    entitySearchRange.has_VendaIdVenda = ko.computed(function(){ return (entitySearchRange.VendaIdVenda_begin() != null || entitySearchRange.VendaIdVenda_end() != null); });
    entitySearchRange.has_VendaIntVenda = ko.computed(function(){ return (entitySearchRange.VendaIntVenda_begin() != null || entitySearchRange.VendaIntVenda_end() != null); });
    entitySearchRange.has_VendaSmallIntVenda = ko.computed(function(){ return (entitySearchRange.VendaSmallIntVenda_begin() != null || entitySearchRange.VendaSmallIntVenda_end() != null); });
    entitySearchRange.has_VendaDatetimeVenda = ko.computed(function(){ return (entitySearchRange.VendaDatetimeVenda_typeRange() == 'R' && (entitySearchRange.VendaDatetimeVenda_begin() != null || entitySearchRange.VendaDatetimeVenda_end() != null) || (entitySearchRange.VendaDatetimeVenda_typeRange() == 'P' && entitySearchRange.VendaDatetimeVenda_predefFilter().length > 0)); });
    
    function deleteGrid(element, cName, cDataItem_listItem, isMultiSelect) {
       var element = element;
       var cName = cName;
       var dataItem_ListItem = cDataItem_listItem.split(';');
       var currentdataItem = dataItem_ListItem[0];
       var currentlistItem = dataItem_ListItem[1];
       $(element).igGridUpdating('endEdit');
       var selectedRows = [];
       var activeRow = $(element).igGrid('activeRow');
       if (isMultiSelect) { if ($(element).igGrid('selectedRows').length > 0) selectedRows = $(element).igGrid('selectedRows');
       } else { selectedRows.push($(element).igGrid('selectedRow')); }
       if (!activeRow) activeRow = selectedRows[0];
       if (isNullOrEmpty(selectedRows[0])) {
           app.showMessage('Nenhum registro selecionado!', 'Informação', ['Ok']);
           return;
       }
       var entity = findElementByKey(eval(currentlistItem), 'RowDataId', isNullOrEmpty(selectedRows) && selectedRows.length === 0 ? 0 : selectedRows[0].id);
       if (isNullOrEmpty(entity)) {
           app.showMessage('Nenhum registro selecionado!', 'Informação', ['Ok']);
           return;
       }
       if (deleteEntity(entity, isMultiSelect)) {
           if (entity.typeName === vm.rootDataTypeName) {
               eval(currentlistItem)['remove'](entity);
           }
       }
       else { return; }
        if ($(element).data('igGrid')._totalRowCount > 0) {
            for (i = 0; i < selectedRows.length; i++) {
               var selectedRow = selectedRows[i];
               $(element).igGridUpdating('deleteRow', selectedRow.id);
            }
        }
        var rows = $(element).igGrid('rows');
        if (rows.length > 0) {
            var idx = (activeRow.index > rows.length) ? rows.length : (activeRow.index > 0 ? activeRow.index - 1 : activeRow.index);
            $(element).igGridSelection('selectRow', idx);
            var ui = $(element).data('igGridUpdating');
            selectGridCurrentItem(goToKey, 'RowDataId', ui.grid.activeRow(), eval(currentdataItem), eval(currentlistItem));
        }
    };
    function openEditor(element, cName, cDataItem_listItem, dataV_parentName, entityName, isEditorWithinGrid) {
       var element = element;
       var cName = cName;
       var dataItem_ListItem = cDataItem_listItem.split(';');
       var dataView_parentName = dataV_parentName.split(';');
       var currentdataItem = dataItem_ListItem[0];
       var currentlistItem = dataItem_ListItem[1];
       var entityName = entityName
       var dataView = dataView_parentName[0];
       var parentName = dataView_parentName[1];
       var ui = $(element).data('igGridUpdating');
    
       $('.ui-dialog:has(#' + $('#dialog' + cName + '').attr('id') + ')').empty().remove();
       if ($(element).data('igGridGroupBy') !== undefined && $(element).igGridGroupBy('groupByColumns').length !== 0){
          app.showMessage('Não é possível habilitar o editor template com campos agrupados!', 'Informação', ['Ok']);
          return false;
       }
       if (getSelectedIndex(element) == -1){
          app.showMessage('Registro não selecionado!', 'Informação', ['Ok']);
          return false;
       }
    
       configEditor(element, currentdataItem, currentlistItem);
    
       if (vm.status() !== 'E') {
           $('#addReg' + cName + '').hide();
           $('#delReg' + cName + '').hide();
       }
       else {
           $('#addReg' + cName + '').show();
           $('#delReg' + cName + '').show();
       }
    
       var pk_id = getSelectedIndex(element) + 1;
       var ds = ui.grid.dataSource;
       var columns = ui.grid.options.columns;
       fillLabels(pk_id, element, dataView, cName);
    
       $.fn['backReg' + cName + ''] = function () {
           if (hasPaging(element).length > 0) {
               gridTrData = ui.grid.dataSource.dataView()[getSelectedIndex(element)];
               if (getSelectedIndex(element) > 0) {
                   pk_id = getSelectedIndex(element) - 1;
                   $(element).igGridSelection('clearSelection');
                   updateGrid(gridTrData, pk_id, ui, currentdataItem, element);
                   updateTemplate(pk_id, 1, element, ui, currentdataItem, currentlistItem);
                   fillLabels(pk_id + 1, element, dataView, cName);
               }
           }
           else{
               pk_id = getSelectedIndex(element) + 1;
               gridTrData = ui.grid.dataSource.dataView()[pk_id - 1];
               updateGrid(gridTrData, pk_id, ui, currentdataItem, element);
               $(element).igGridSelection('clearSelection');
               if (pk_id > 1) {
                   updateTemplate(pk_id, 1, element, ui, currentdataItem, currentlistItem);
                   pk_id = pk_id - 1;
               }
               else
                   $(element).igGridSelection('selectRow', pk_id - 1);
               fillLabels(pk_id, element, dataView, cName)
           }
       }
       $.fn['nextReg' + cName + ''] = function () {
           if (hasPaging(element).length > 0) {
               gridTrData = ui.grid.dataSource.dataView()[getSelectedIndex(element)];
               pk_id = getSelectedIndex(element) + 1;
               if (ui.grid.dataSource.dataView().length > pk_id) {
                   $(element).igGridSelection('clearSelection');
                   updateGrid(gridTrData, pk_id, ui, currentdataItem, element);
                   updateTemplate(pk_id, 2, element, ui, currentdataItem, currentlistItem);
                   pk_id = pk_id + 1;
               }
               else
                   $(element).igGridSelection('selectRow', pk_id - 1);
           } else {
               pk_id = getSelectedIndex(element) + 1;
               gridTrData = ui.grid.dataSource.dataView()[pk_id - 1];
               updateGrid(gridTrData, pk_id, ui, currentdataItem, element);
               var totalGrid = ui.grid.options.dataSource.count();
               $(element).igGridSelection('clearSelection');
               if (totalGrid > pk_id) {
                   updateTemplate(pk_id, 2, element, ui, currentdataItem, currentlistItem);
                   pk_id = pk_id + 1;
               }
               else
                   $(element).igGridSelection('selectRow', pk_id - 1);
           }
          fillLabels(pk_id, element, dataView, cName);
       }
       $.fn['addReg' + cName + ''] = function () {
          var addedEntity = eval('vm.createAndNotify' + entityName);
          if (addedEntity) {
             var index = 0; var ds = ui.grid.options.dataSource;
             for (index = 0; index < ds.count(); index++) {
                if (addedEntity.RowDataId == ds[index].RowDataId) break;
             }
             updateFieldsTemplate(addedEntity.RowDataId, currentdataItem, currentlistItem);
             fillLabels(index + 1, element, dataView, cName);
          }
       }
       $.fn['delReg' + cName + ''] = function () {
           pk_id = getSelectedIndex(element);
           gridTrData = ui.grid.dataSource.dataView()[pk_id];
           $(element).igGridUpdating('deleteRow', gridTrData['RowDataId']);
           var entity = findElementByKey(eval(currentlistItem), 'RowDataId', gridTrData['RowDataId']);
           if (entity) {
               removeInnerDataUIs(entity);
               dataContext.deleteEntity(entity);
           }
           var totalGrid = ui.grid.options.dataSource.count();
           if (totalGrid === 0) return restartGrid(element, cName, isEditorWithinGrid);
           if (pk_id == totalGrid) {
               gridTrData = ui.grid.dataSource.dataView()[totalGrid - 1];
               $(element).igGridSelection('selectRow', totalGrid - 1);
           }
           else {
               gridTrData = ui.grid.dataSource.dataView()[pk_id];
               $(element).igGridSelection('selectRow', pk_id);
           }
           updateFieldsTemplate(gridTrData['RowDataId'], currentdataItem, currentlistItem);
           fillLabels(pk_id, element, dataView, cName);
       }
       $.fn['okReg' + cName + ''] = function () {
           pk_id = getSelectedIndex(element);
           gridTrData = ui.grid.dataSource.dataView()[pk_id];
           updateGrid(gridTrData, pk_id, ui, currentdataItem, element);
           $(element + '_EditorBtn').attr('title', 'Alterar edição para modo Template');
           return restartGrid(element, cName, isEditorWithinGrid);
       }
       $.fn['clickSelectorGrid'] = function (tb) {
           var table = tb[0].offsetParent.id;
          var removeSpace = $('#' + table).data('param').replace(/\s/g, "");
           var param = removeSpace.split(',');
           selectorEditorTemplate(param[0], parseInt(tb[0].id), param[1], param[2], param[3], param[4]);
       }
       if(currentdataItem && eval(currentdataItem));
           eval(currentdataItem).notifySubscribers();
       if (!isEditorWithinGrid) {
           $('#dialog' + cName + '').dialog({
               modal: true,
               width: '90%',
               height: 700,
               show: { effect: 'drop', direction: 'up' },
               draggable: true,
               closeOnEscape: false,
               resizable: false,
               zIndex: getNew_zIndex()
           });
           $('.ui-widget-overlay.ui-front').css('z-index', getNew_zIndex() - 1);
           $('#dialog' + cName + '').dialog('widget').find('.ui-dialog-titlebar-close').hide();
       }
       else{
           $(element + '_ContentDLG').next().addClass('hide');
           $(element + '_container').parent().addClass('hide');
           $(element + '_ContentDLG').attr('style', 'position: static;height: 350px;');
           $('#dialog' + cName + '').appendTo($(element + '_ContentDLG'));
           $('#dialog' + cName + '').show();
       }
    
       return false;
    };
       function updateGrid(grd, pk, ui, currentdataItem, element) {
           if (pk >= 0 && eval(currentdataItem + '()') !== null) {
               var propUpdate = 0;
               var hasChangeProp = false;
               var columns = ui.grid.options.columns;
               for (i = 1; i < columns.length; ++i) {
                   if (columns[i].key.indexOf('Multi') < 0) {
                       propUpdate = getAbsoluteValue(eval(currentdataItem + '()')['' + columns[i].key + '']);
                       if (grd[columns[i].key] != propUpdate) {
                           grd[columns[i].key] = propUpdate;
                           hasChangeProp = true;
                       }
                   }
               }
               if(hasChangeProp) $(element).igGridUpdating('updateRow', grd['RowDataId'], grd);
           }
       };
       function updateTemplate(pk, step, element, ui, currentdataItem, currentlistItem) {
           if (step == 1) {
               if (hasPaging(element).length == 0)
                   pk = pk - 2;
               $(element).igGridSelection('selectRow', pk);
               gridTrData = ui.grid.dataSource.dataView()[pk];
           }
           else if (step == 2) {
               $(element).igGridSelection('selectRow', pk);
               gridTrData = ui.grid.dataSource.dataView()[pk];
           }
           updateFieldsTemplate(gridTrData['RowDataId'], currentdataItem, currentlistItem);
       };
       function updateFieldsTemplate(grd, currentdataItem, currentlistItem) {
           if (vm.goToKey && 'RowDataId' && grd) {
               vm.goToKey('RowDataId', grd, eval(currentdataItem), eval(currentlistItem));
           }
       };
       function configEditor(element, currentdataItem, currentlistItem){
           var mode = $(element).igGridUpdating('option', 'editMode');
           if (mode == 'cell') {
               var rows = $(element).igGrid('rows');
               if (rows.length === 0) {
                   app.showMessage('Não é possível abrir a edição quando não existir ao menos uma linha na grade!', 'Informação', ['Ok']);
                   return false;
               }
               var row =  $(element).igGrid('selectedRow');
               var isChk = $(element).igGridSelection('selectedRows');
               var rowEntity = 0;
               if (isChk && isChk.length != 0) rowEntity = isChk[0].id;
               var entity = findElementByKey(eval(currentlistItem), 'RowDataId', isNullOrEmpty(row) ? rowEntity : row.id);
               if (rowEntity !== 0)
                   updateFieldsTemplate(entity['RowDataId'], currentdataItem, currentlistItem);
               //$(element).igGridUpdating('option', 'editMode', 'rowedittemplate');
               $(element).igGridUpdating('option', 'startEditTriggers', 'dblclick,F2');
               $('.fa.fa-th').addClass('fa fa-list-alt').removeClass('fa-th');
               $(element + '_EditorBtn').attr('title', 'Alterar edição para modo Célula');
           }
           else {
               //$(element).igGridUpdating('option', 'editMode', 'cell');
               $(element).igGridUpdating('option', 'startEditTriggers', 'click');
               $('.fa.fa-list-alt').addClass('fa fa-th').removeClass('fa-list-alt');
               $(element + '_EditorBtn').attr('title', 'Alterar edição para modo Template');
           }
    };
       function restartGrid(element, cName, isEditorWithinGrid) {
           //$(element).igGridUpdating('option', 'editMode', 'cell');
           $(element).igGridUpdating('option', 'startEditTriggers', 'click');
           $('.fa.fa-list-alt').addClass('fa fa-th').removeClass('fa-list-alt');
           $(element).attr('title', 'Alterar edição para modo Template');
           if (isEditorWithinGrid) {
               if (cName.indexOf('dialog') > -1)
                   $(cName).attr('style', 'display: none !important;');
               else
                   $('#dialog' + cName + '').attr('style', 'display: none !important;');
    
                $(element + '_ContentDLG').attr('style', 'position: relative;height: 1px;');
                $(element + '_ContentDLG').next().removeClass('hide');
                $(element + '_container').parent().removeClass('hide');
           }
           else
               $('#dialog' + cName + '').dialog('close');
       };
       function getSelectedIndex(element) {
           var sIndex = -1;
           if ($(element).data('igGridSelection') && $(element).igGridSelection('option', 'multipleSelection')) {
               var trs = $(element).igGrid('selectedRows');
               if (trs.length > 0) sIndex = trs[0].index;
           } else {
               var tr = $(element).igGrid('selectedRow');
               if (tr != null) sIndex = tr.index;
           }
           return sIndex;
       };
       function fillLabels(current, element, dataView, cName) {
           checkDisableControl(element);
           showAndHideColumnsEditor(element, dataView);
           var ui = $(element).data('igGridUpdating');
           var totalGrid = ui.grid.options.dataSource.count();
           if ($(element).data('igGridSelection') && $(element).igGridSelection('option', 'multipleSelection')) {
               var trs = $(element).igGrid('selectedRows');
               if (trs.length > 0) var currentRow = trs[0].index + 1;
           }
           else
               var currentRow = $(element).igGrid('selectedRow').index + 1;
           if (hasPaging(element).length > 0) {
               var totalCurrentPage = totalGrid;
               var currentPage = $(element).igGridPaging('pageIndex') + 1;
               var pageIndex = $(element).igGridPaging('pageIndex');
               var pageSize = $(element).igGridPaging('pageSize');
               if (totalGrid / pageSize > currentPage)
                   totalCurrentPage = (currentPage * ui.grid.dataSource.dataView().length);
               $('label#currentNumber' + cName + '').html(currentRow + ' - ' + totalCurrentPage);
           }
           else
               $('label#currentNumber' + cName + '').html((current == 0 ? totalGrid : current));
           $('label#totalNumber' + cName + '').html(totalGrid);
       };
       function checkDisableControl(element) {
           var columns = $(element).igGridUpdating('option', 'columnSettings');
           columns.forEach(function (entry, index) {
               if (entry.fieldTplDisabled) {
                   var controlTemplate = $('[id^="' + $lx(vm, '#div').selector.replace('#', '') + '"][id$="_' + entry.columnKey + 'Template"]');
                   $(controlTemplate).append('<div style="position: absolute;top:0;left:0;width: 100%;height:100%;z-index:2;opacity:0.4;filter: alpha(opacity = 50)"></div>');
               };
           });
       };
       function showAndHideColumnsEditor(element, dataView) {
           if (vm.status() !== 'C') {
               var colunas = $(element).igGrid('option', 'columns');
               colunas.forEach(function (entry, index) {
                   if (entry.hidden && entry.key !== 'RowDataId') {
                       var control = $('#UIExternaTab_div' + (!dataView ? '' : '' + dataView + '_') + entry.key + 'Template');
                       if (!control.hasClass('hide') && !control.hasClass('onlyEditor'))
                           control.addClass('hide');
                   } else if (entry.key !== 'RowDataId') {
                       var control = $('#UIExternaTab_div' + (!dataView ? '' : '' + dataView + '_') + entry.key + 'Template');
                       if (control.hasClass('hide'))
                           control.removeClass('hide');
                   }
               });
           }
       };
       function hasPaging(element) {
            return $.grep($(element).igGrid('option', 'features'), function (e) { return e.name == 'Paging'; }); 
       };
       function selectorEditorTemplate(element, pk, cName, cDataItem_listItem, dataV_parentName, entityName) {
           var element = element;
           var dataItem_ListItem = cDataItem_listItem.split(';');
           var dataView_parentName = dataV_parentName.split(';');
           var currentdataItem = dataItem_ListItem[0];
           var currentlistItem = dataItem_ListItem[1];
           var entityName = entityName;
           var dataView = dataView_parentName[0];
           var parentName = dataView_parentName[1];
           var ui = $(element).data('igGridUpdating');
           var verticalContainer = $(element).igGrid('scrollContainer');
           verticalContainer.scrollTop($(element).igGrid('option', 'avgRowHeight') * (pk - 1));
           gridTrData = ui.grid.dataSource.dataView()[pk];
           updateFieldsTemplate(gridTrData['RowDataId'], currentdataItem, currentlistItem);
           updateGrid(gridTrData, pk, ui, currentdataItem, element);
           $(element).igGridSelection('clearSelection');
           $(element).igGridSelection('selectRow', pk);
           if (status() === 'E') notifyPresentation('' + currentlistItem.split('.').pop() + '');
           fillLabels(pk + 1, element, dataView, cName);
           $(element + '_Toggle').slideToggle();
       };
       function loadSeletor(tbGrid, fields, grd, entity) {
           var tbody = $(tbGrid).children('tbody');
           var cols = fields.split(',');
           var list = $(grd).data('igGrid').dataSource.dataView();
           $(tbGrid + ' > tbody > tr').remove();
           var objCols = new Array();
           var metaDataEntity = vm.metadataInfo[entity];
    
           if ($(grd + '_Toggle').is(':hidden')) {
               if (status() !== 'C') {
                   for (j = 0; j < cols.length; j++) {
                       for (var prop in metaDataEntity) {
                           if (metaDataEntity[prop]['key'] == cols[j]) {
                               objCols.push(metaDataEntity[prop]);
                               break;
                           }
                       }
                   }
                   for (i = 0; i < list.length; i++) {
                      var tr = document.createElement('TR');
                      tr.setAttribute('id', i);
                      tr.setAttribute('onclick', '$(this).clickSelectorGrid($(this));');
                      for (j = 0; j < objCols.length; j++) {
                          var td = document.createElement('TD');
                          if (objCols[j].isDomain)
                              var fieldFormat = vm.dataDomains.getName(objCols[j].domainName, list[i][objCols[j].key]);
                          else if (objCols[j].dataType == 'date')
                              var fieldFormat = Globalize.format(getUTCDate(list[i][objCols[j].key]), objCols[j].format);
                          else if (objCols[j].dataType == 'number' && objCols[j].format == 'int')
                              var fieldFormat = Globalize.format(list[i][objCols[j].key], "n0");
                          else
                              var fieldFormat = Globalize.format(list[i][objCols[j].key], (objCols[j].dataType == 'number' ? "n" : objCols[j].format));
                          td.appendChild(document.createTextNode(fieldFormat));
                          tr.appendChild(td);
                      }
                      tbody.append(tr);
                   }
               } else {
                   var tr = document.createElement('TR');
                   var td = document.createElement('TD');
                   td.setAttribute('colspan', '' + cols.length + '');
                   td.style.textAlign = 'center';
                   td.appendChild(document.createTextNode('Modo Pesquisa'));
                   tr.appendChild(td);
                   tbody.append(tr);
               }
           }
       };
    
    var dataToolbar = {
            isBusy: isBusy,
            currentRecordInfo: currentRecordInfo,
            canGoFirst: canGoFirst,
            canGoBack: canGoBack,
            canGoForward: canGoForward,
            canGoLast: canGoLast,
            canClear: canClear,
            canQuickSearch: canQuickSearch,
            canNavigate: canNavigate,
            noBusyLoading: _noBusyLoading,
            currentPage: currentPage,
            quickSearch: quickSearch,
            canExport: canExport,
            canGridExport: canGridExport,
            canQuery: canQuery,
            canCustomSearch: canCustomSearch,
            canRefreshCurrentData: canRefreshCurrentData,
            hasDataFeed: hasDataFeed,
            canAddNew: canAddNew,
            canRemove: canRemove,
            canEdit: canEdit,
            canSave: canSave,
            canUndo: canUndo,
            canPrint: canPrint,
            goFirst: goFirst,
            goBack: goBack,
            goForward: goForward,
            goLast: goLast,
            adjustNavigationByPage: adjustNavigationByPage,
            query: query,
            customSearch: customSearch,
            customSearchResult: customSearchResult,
            hasCustomSearches: hasCustomSearches,
            refreshCurrentData: refreshCurrentData,
            exportData: exportData,
            undo: undo,
            save: save,
            saveFake: saveFake,
            submitAllChanges: submitAllChanges,
            saveSuccessInnerUIs: saveSuccessInnerUIs,
            addNew: addNew,
            remove: remove,
            refresh: refresh,
            clear: clearByUser,
            print: print,
            showDataFeedUrl: showDataFeedUrl,
            edit: edit,
            canViewInfo: canViewInfo,
            viewInfo: viewInfo,
            lastSearchFilter: lastSearchFilter,
            importPhoto: importPhoto,
            title: function() { return (uiSettings && uiSettings.displayName ? uiSettings.displayName : ''); }
        };
    
    if (dataContext.dataDomains) {
        dataContext.dataDomains.refreshData = function () {
            refreshToolbar();
        };
    }
    var vm = {
            useLikeCommandAsDefault: false,
            dataView: dataView,
            viewName: 'UIExternaTab',
            getDataForSaving: getDataForSaving,
            getViewMapInfo: getViewMapInfo,
            saveSuccessInnerUIs: saveSuccessInnerUIs,
            getAddedEntities: getAddedEntities,
            getAllChanges: getAllChanges,
            gridSaveStates: [],
            hasValidationErrors: hasValidationErrors,
            hasInternalUIsValidationErrors: hasInternalUIsValidationErrors,
            canReportErrors: false,
            currentDataItem: currentDataItem,
            exportDataDetails: exportDataDetails,
            openEditor: openEditor,
            deleteGrid: deleteGrid,
            selectorEditorTemplate: selectorEditorTemplate,
            loadSeletor: loadSeletor,
            currentDataIndex: currentDataIndex,
            navigationByPage: navigationByPage,
            hasMainTopDataGrid: hasMainTopDataGrid,
            dataShared: [],
            hasChanges: hasChanges,
            isSaving: isSaving,
            enabledForEditing: enabledForEditing,
            dataToolbar: dataToolbar,
            getDataContext: function() { return dataContext; },
            getParentSelectorDataName: getParentSelectorDataName,
            validParentSelectorDataCondition: validParentSelectorDataCondition,
            addNewToInnerUI: addNewToInnerUI,
            getDataFromInnerUI: getDataFromInnerUI,
            queryInnerUIs: queryInnerUIs,
            clear: clear,
            clearInnerUIs: clearInnerUIs,
            dataSource: dataSource,
            getMaxLength: getMaxLength,
            addDataSource: addDataSource,
            getVisibleProperties: getVisibleProperties,
            status: status,
            removeParentRelatedItems: removeParentRelatedItems,
            onSavingValidation: onSavingValidation,
            goToKey: goToKey,
            getSpecializedLookupItems: getSpecializedLookupItems,
            dataBind: dataBind,
            isDataSourceHided: isDataSourceHided,
            //Durandal Events
            activate: activate,
            binding: binding,
            finalizeCombo: finalizeCombo,
            dataCombo: dataCombo,
            clearCombo: clearCombo,
            dataDomains: dataContext.dataDomains,
            bindingComplete: bindingComplete,
            attached: attached,
            canDeactivate: canDeactivate,
            canActivate: canActivate,
            deactivate: deactivate,
            //End Durandal Events
            compositionComplete: compositionComplete,
            detached: detached,
            app: app,
            lookUpProperties: dataContext.lookUpProperties,
            metadataInfo: dataContext.metadataInfo,
            dataExportInfo: dataContext.dataExportInfo,
            entityNames: dataContext.entityNames,
            lookUpNames: dataContext.lookUpNames,
            getWithBinding: dataContext.getWithBinding,
            managerAuth: managerAuth,
            rootBmTypeName: 'VENDA',
            rootDataTypeName: 'Venda',
            rootNamespace: 'VAREJO.BV.Externas',
            setSecurity: setSecurity,
            isReportComposition: isReportComposition,
            refreshToolbar: refreshToolbar,
            refreshCurrentBind: refreshCurrentBind,
            lazyRefreshBinding: lazyRefreshBinding,
            createEntity: createEntity,
            notifyPresentation: notifyPresentation,
            notifyInnerElements: notifyInnerElements,
            getServiceAddress: dataContext.getServiceAddress,
            getAccessGroup: dataContext.getAccessGroup,
            getBandeiraRede: getBandeiraRede,
            getCurrentBrands: getCurrentBrands,
            setBandeiraRede: setBandeiraRede,
            entitySearchRange: entitySearchRange,
            modalMultimidia: modalMultimidia,
            currentActivityInformation: currentActivityInformation,
            showProcessing: showProcessing,
            closeProcessing: closeProcessing,
            internalUIs: [],
            viewType: viewType,
            hideToolbar: hideToolbar,
            isDependentVM: isDependentVM,
            getInnerJExpression: getInnerJExpression,
            allowMultiSelectionInSearch: allowMultiSelectionInSearch,
            transactionNumberControl: transactionNumberControl,
            createVenda: createVenda,
            createAndNotifyVenda: createAndNotifyVenda,
            deleteEntity: deleteEntity,
            currentBrands: '',
            brands: managerBrand.getBrandVM(),
            hasBrand: false,
            controllerName: dataContext.controllerName,
            getJExpression: getJExpression,
            replaceInnerUIsKeys: replaceInnerUIsKeys,
            replaceKeyFromParent: replaceKeyFromParent,
            getQueryFilter: getQueryFilter,
            getTranslatedFilter: getTranslatedFilter,
            sortData: sortData,
            lastJEntitySearch: function () { return lastJEntitySearch; },
            isEditable: isEditable,
            setStatus: setStatus,
            common: common,
            showRegisteredUI: showRegisteredUI,
            openingExternalUIFromGrid: openingExternalUIFromGrid,
            __moduleId__: 'pkg_varejo-bv-spa/viewmodels/UIExternaTab'
        };
    
    dataContext.setCurrentViewModel(vm);
    return vm;
}

return vmInstance;
});
