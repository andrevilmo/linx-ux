define(['durandal/app', 'pkg_linx-appconfigmanager-bv-spa/services/SelfHostContext', 'plugins/router', 'plugins/widget', 'managers/__auth', 'viewmodels/shared/modal', 'viewmodels/shared/modal2', 'managers/brand', 'managers/predefinedFilters', 'services/logger', 'viewmodels/shared/modalMultimidia', 'common', 'pkg_linx-appconfigmanager-bv-spa/viewmodels/WizardInstalacaoCustom', 'pkg_linx-appconfigmanager-bv-spa/viewmodels/WizardInstalacaoComplement', 'viewmodels/shared/modalCustomSearch'],
function (app, dataContextFn, router, widget, managerAuth, modal, modal2, managerBrand, managerPredefined, logger, modalMultimidia, common, customFn, complementFn, modalCustomSearch) {
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
    var custom = ((typeof customFn === 'function') ? customFn() : customFn);
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
    var isBusy = ko.observable(false);
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
    // KO Subscription isBusy
    isBusy.subscribe(function (newValue) {
        if ($(".page-container").html() == undefined || $(".page-container").html().length == 0)
            return;
        if (newValue) {
            common.showProcess('#main');
        }
        else {
            common.closeProcess('#main');
        }
    });
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
      vm.WizardInstalacao = getVM;
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
                      setSecurity(uiSettings.toolbarSettings.canAddNew, uiSettings.toolbarSettings.canClear, uiSettings.toolbarSettings.canCustomSearch, uiSettings.toolbarSettings.canDelete, uiSettings.toolbarSettings.canEdit, uiSettings.toolbarSettings.canLayout, uiSettings.toolbarSettings.canNavigate, uiSettings.toolbarSettings.canPrint, uiSettings.toolbarSettings.canSearch, uiSettings.toolbarSettings.canExport);
                      hideToolbar(uiSettings.toolbarSettings.removeDataToolbar);
                  }
                  filteredEntities = [];
                  clear();
                  if ((typeof uiSettings.querySetters === 'object')) {
                      for (var field in uiSettings.querySetters) {
                          setAbsoluteValue(currentDataItem(), field, uiSettings.querySetters[field]);
                      }
                  }
                  query(true);
              }
              else {
                  setSecurity(uiSettings.canAddNew, uiSettings.canClear, uiSettings.canCustomSearch, uiSettings.canDelete, uiSettings.canEdit, uiSettings.canLayout, uiSettings.canNavigate, uiSettings.canPrint, uiSettings.canSearch, uiSettings.canExport);
                  hideToolbar(uiSettings.removeDataToolbar);
                  if ((typeof settings.parentVM === 'object') && settings.parentVM != null) {
                      parentVM = settings.parentVM;
                      parentVM.WizardInstalacao = getVM;
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
                      if ($.inArray('WizardInstalacao', parentVM.internalUIs) === -1) parentVM.internalUIs.push('WizardInstalacao');
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
        setSecurity(false, false, false, false, false, false, false, false, false, false);
        if (managerAuth.shellMode == 'PROD') {
           managerAuth.getFormAccess('linx-appconfigmanager-bv-spa-WizardInstalacao', function (data) {
              if (data && !data.AcessoTotal) {
                  setSecurity(data.Incluir, true, data.PesquisaEspecial, data.Excluir, data.Alterar, data.Layout, true, data.Imprimir, data.Pesquisar, true);
              }
           }, logger);
        }
    };
    
    var getVM = function () {
        return vm;
    };
    
    var binding = function () {
        vm.showProcessing('Inicializando...');
        return { cacheViews: false };
    };
    
    var bindingComplete = function () {
        return true;
    };
    var attached = function(view, parent) {
        return true;
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
            app.showMessage('Versão de formulário incompatível com a versão de ambiente [' + managerAuth.shellVersion + '].', 'Formulário: WizardInstalacao', ['Ok']);
            return false;
        }
        return true;
    };
    var deactivate = function() {
       document.removeEventListener(dataContext.contextUpdtEvt, contextDataUpdateHandler, false);
    };
    var compositionComplete = function() {
        $('#WizardInstalacao_wizWizard_001599f169e147a793babdef29848042').on('shown.bs.tab', function (e) { vm.notifyInnerElements($(e.target.hash)); });

    complement.renderWizardInstalacao_wizWizard_001599f169e147a793babdef29848042(vm);


        navigationByPage(hasMainTopDataGrid());
        dataBind();
        vm.closeProcessing();
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
        custom.afterViewInitializing({ viewModel: vm });
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
        if (isNullOrEmpty(entityName)) entityName = 'SetupConfig';
        var property = dataContext.getEntityProperty(entityName, propertyName);
        if(property != null)
            return property.maxLength;
        else
            return 0;
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
    
    var visibleColumns = 'Produto,IdLoja,CodigoLoja,NomeLoja,WindowsAuthentication,Server,Database,UserName,Password,UrlLocal,URLServico,UsuarioServico,SenhaServico';
    
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
            var cacheElements = dataContext.getEntities('SetupConfig', [dataContext.breeze.EntityState.Added]);
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
                   var customFilter = uiSettings.ownerReference['BeforeGet' + uiSettings.lookupName + 'Query']();
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
       if (dataView().length > 1 && !isNullOrEmpty(complement) && (typeof complement.selectedItems === 'function'))
           result = complement.selectedItems(false);
       if ((dataView().length == 1 || !navigationByPage() || isNullOrEmpty(complement) || (typeof complement.selectedItems !== 'function') || (uiSettings && uiSettings.allowMultiSelectionInSearch === false)) && result.length == 0)
           result.push(currentDataItem());
       return result;
    };
    var exportData = function (forceAdd, isExcelDataSource) {
        if (forceAdd)
            require(['viewmodels/shared/addCustomExport'],
                function(addCustomExport){ addCustomExport.showModal(vm, null, 'SetupConfig', getVisiblePropertiesForExcel('dataView'), null, true, isExcelDataSource); } );
        else
            require(['viewmodels/shared/customExport'],
                function(modalExport){ modalExport.showModal(vm, 'SetupConfig', getVisiblePropertiesForExcel('dataView'), null, { canAdd: true, canEdit: true, canDel: true }, isExcelDataSource); } );
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
        }
    };
    var refreshCurrentData = function () {
        if (navigationByPage()) {
           var refreshIndexedData = function (currentIndex) {
                 if (currentIndex < dataView().length) {
                     if (currentIndex == 0) vm.showProcessing('Atualizando informações...');
                     dataView()[currentIndex].refreshData(true).fin(function () { refreshIndexedData(currentIndex + 1); });
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
        return currentDataItem().refreshData().fin(complete);
    
        function complete() {
            currentDataItem.notifySubscribers();
            vm.closeProcessing();
        }
    }
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
        var e = { cancel: false, jEntitySearch: eSearch, viewModel: vm };
        custom.beforeQuerying(e);
        if (e.cancel) return 'Error';
    
        eSearch = e.jEntitySearch;
       translatedJEntitySearch = common.translateSearch(dataContext, eSearch);
        if (!isNullOrEmpty(customSearchResult.searchDefinition)) eSearch += customSearchResult.searchDefinition;
        return eSearch;
    }
    var queryInnerUIs = function (parentEntity, parentTypeName) {
       if (status() === 'C') return;
       commitInternalUIsData();
       for (var idx = 0; idx < vm.internalUIs.length; idx++) { var innerVM = vm[vm.internalUIs[idx]](); if ((!isNullOrEmpty(parentTypeName) && innerVM.getParentSelectorDataName() === parentTypeName) || (!isNullOrEmpty(parentEntity) && innerVM.getParentSelectorDataName() === parentEntity.typeName)) innerVM.dataToolbar.query(false, parentEntity); }
    };
    var addNewToInnerUI = function (parentEntity, uiName) {
       setTimeout(function () {
           for (var idx = 0; idx < vm.internalUIs.length; idx++) { var innerVM = vm[vm.internalUIs[idx]](); if (innerVM.getParentSelectorDataName() === parentEntity.typeName && (isNullOrEmpty(uiName) || innerVM.viewName === uiName)) innerVM.dataToolbar.addNew(parentEntity); }
       }, 1000);
    };
    var removeInnerDataUIs = function (parentEntity) {
       for (var idx = 0; idx < vm.internalUIs.length; idx++) { var innerVM = vm[vm.internalUIs[idx]](); if (!isNullOrEmpty(parentEntity) && innerVM.getParentSelectorDataName() === parentEntity.typeName) innerVM.removeParentRelatedItems(); }
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
    var getRelatedElementsInCache = function () {
        if (parentEntityRelated != null && pageSize() === 0 && isChildVM()) {
           var cacheElements = dataContext.getEntities('SetupConfig');
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
       return (parentVM != null && uiSettings != null && (typeof uiSettings.parentFieldsRelation !== 'undefined') && (typeof uiSettings.detailFieldsRelation !== 'undefined') && uiSettings.parentFieldsRelation.length == uiSettings.detailFieldsRelation.length) && !isLookup();
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
    
    var query = function (lookupInitializing, parentEntity, quickSearchJExpression) {
        if (isProcessing) return;
        isProcessing = true;
        if (isChildVM() && (uiSettings.canAddNew || uiSettings.canEdit || uiSettings.canDelete))
           status(parentVM.status());
        if (lookupInitializing === true && uiSettings && uiSettings.modalForm && (typeof uiSettings.modalForm.hide === 'function')) uiSettings.modalForm.hide(true);
        if (!isNullOrEmpty(parentEntity) && !isNullOrEmpty(parentEntity.typeName))
           parentEntityRelated = parentEntity;
        else
           parentEntityRelated = null;
        if ((isNullOrEmpty(parentEntityRelated) || (status() === 'C' && (parentEntityRelated != null && parentEntityRelated.isAdded()))) && isChildVM()) { dataContext.clearAll(); if (isNullOrEmpty(parentEntityRelated)) { currentDataItem(null); querySucceeded({ results: [] }); return complete(); } }
        if ((status() !== 'C' || (parentEntityRelated != null && parentEntityRelated.isAdded())) && getRelatedElementsInCache() >= 0) { querySucceeded({ results: dataView() }); return complete(); }
        if (freeEntityForQuerying == null && isChildVM()) freeEntityForQuerying = dataContext.createFreeEntity('SetupConfig');
        filteredEntities = (status() === 'C' && !isChildVM() ? currentDataItem().getCurrentElements() : []);
        if (uiSettings != null && uiSettings.noSearch) { dataView([currentDataItem()]); status('Q'); refreshToolbar(); return complete(); }
        lastJEntitySearch = (isNullOrEmpty(quickSearchJExpression) ? '' : quickSearchJExpression) + getQueryFilter((isChildVM() ? freeEntityForQuerying : currentDataItem()));
        if (lastJEntitySearch === 'Error')
            return complete();
        vm.showProcessing('Pesquisando informações...');
        var hasError = true;
        if (status() === 'C') { for(var idx = 0; idx < filteredEntities.length; idx++) { dataContext.detachEntity(filteredEntities[idx]); } }
        return dataContext.getSetupConfigByEntitySearchNoAssociations(lastJEntitySearch, 0, pageSize(), (pageSize() > 0), (status() !== 'C' && pageSize() === 0 && isChildVM()), status() !== 'E', sortInfo).then(querySucceeded).fin(complete);
    
        function complete() {
            isProcessing = false;
            vm.closeProcessing();
            if (hasError === true && lookupInitializing === true && isLookup() && (parentVM != null)) {
               parentVM.UI_Close_Click();
            }
            else if (hasError === true) {
               clear();
            }
        }
    
        function querySucceeded(data) {
            if (vm.status() !== 'E') { for (var idx = 0; idx < data.results.length; idx++) { dataContext.initializePOCO(data.results[idx], 'SetupConfig'); } }
            hasError = false;
            dataView(data.results);
            if (dataView().length === 0 && ((parentVM == null) || isLookup())) {
                vm.closeProcessing();
                if (isLookup() && (parentVM != null) && lookupInitializing === true) {
                   uiSettings.ownerReference.clearLookUp(uiSettings.lookupName);
                   app.showMessage('A informação de Lookup [' + uiSettings.ownerReference.getDisplayName(uiSettings.fieldToSearch) + '] não foi encontrada!', 'Informação', ['Ok']);
                   parentVM.UI_Close_Click();
                   return;
                }
                else  {
                   app.showMessage('Nenhum registro foi encontrado!', 'Informação', ['Ok']);
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
            goToIndex(0);
            custom.afterQuerying({ dataItems: dataView(), viewModel: vm });
            if (isLookup() && (parentVM != null) && (dataView().length === 1) && lookupInitializing === true) {
               if (uiSettings.lookupInfo.isMultiSelection === true && (typeof currentDataItem().IsSelected === 'function')) currentDataItem().IsSelected(true);
               parentVM.UI_selectOption('Ok');
               return;
            }
            if (lookupInitializing === true && uiSettings.modalForm && (typeof uiSettings.modalForm.hide === 'function')) uiSettings.modalForm.hide(false);
            dataBind((isChildVM() ? '' : 'dataView'));
            if (common.getGridMode() == 'G' && !vm.navigationByPage() && (viewType() === 'Main') && !isChildVM() && dataView().length > 1 && (parentVM == null))
                dataToolbar.viewInfo();
            if (dataView().length == 0) vm.closeProcessing();
        }
    };
    function goToIndex(index) {
        if (dataView().length === 0) { currentDataIndex(0); currentDataItem(null); return true; }
        if (index < 0) { index = 0; }
        else if (index >= dataView().length) { index = dataView().length - 1; }
        currentDataIndex(index);
        var oldValue = currentDataItem();
        currentDataItem(dataView()[index]);
        if (status() !== 'C' && currentDataItem() !== null && oldValue !== currentDataItem()) {
           currentDataItem().fillDetails();
        }
        custom.afterSelecting({ selectedItem: currentDataItem(), viewModel: vm });
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
                if (currentElement) { currentElement(viewSource()[idx]); currentElement().fillDetails(); } else { goToIndex(idx); }
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
        return dataContext.getSetupConfigByEntitySearchNoAssociations(lastJEntitySearch, curPage * pageSize(), pageSize(), false, false, status() !== 'E', sortInfo).then(querySucceeded).fin(complete);
    
        function complete() {
            vm.closeProcessing();
        }
    
        function querySucceeded(data) {
            if (vm.status() !== 'E') { for (var idx = 0; idx < data.results.length; idx++) { dataContext.initializePOCO(data.results[idx], 'SetupConfig'); } }
            dataView(data.results);
            currentPage(curPage);
            goToIndex((goLast ? dataView().length : 0));
            dataBind('dataView');
        }
    };
    //#region Client Events
    var OnWizardStepChanged = function (oldIndex, newIndex, id) {
    //alert(oldIndex);
//alert(newIndex);

if(oldIndex == 0  && newIndex == 1){
	vm.dataToolbar.isBusy(true);
	$.ajax({
                url: vm.getServiceAddress('LinxAppConfigManagerSelfHost/LoadData'),
                dataType: 'json',
                cache: false,
                contentType: false,
                processData: false,
                type: 'GET',
                error: function (jqXHR, textStatus, errorThrown) {
                    vm.dataToolbar.isBusy(false);
                    app.showMessage('Erro ao tentar requisitar um serviço do barramento', 'Alerta', ['OK']);
                },
                success: function (data) {
					
					if(data.Resultado){
						setAbsoluteValue(currentDataItem(), 'Produto', data.Produto)
						setAbsoluteValue(currentDataItem(), 'IdLoja', data.IdLoja)
						setAbsoluteValue(currentDataItem(), 'CodigoLoja', data.CodigoLoja)
						setAbsoluteValue(currentDataItem(), 'NomeLoja', data.NomeLoja)
						setAbsoluteValue(currentDataItem(), 'Database', data.Database)
						setAbsoluteValue(currentDataItem(), 'Password', data.SenhaDB)
						setAbsoluteValue(currentDataItem(), 'Server', data.ServidorDB)
						setAbsoluteValue(currentDataItem(), 'UserName', data.UsuarioDB)
						setAbsoluteValue(currentDataItem(), 'WindowsAuthentication', data.AutenticacaoWindows)
						// --
						setAbsoluteValue(currentDataItem(), 'URLServico', data.URLBus)
						setAbsoluteValue(currentDataItem(), 'UrlLocal', data.UrlLocal)
						setAbsoluteValue(currentDataItem(), 'UsuarioServico', data.UsuarioBUS)
						setAbsoluteValue(currentDataItem(), 'SenhaServico', data.SenhaBUS)
						if (getAbsoluteValue(currentDataItem().WindowsAuthentication)) {
						    setAbsoluteValue(currentDataItem(), 'UserName', "")
						    setAbsoluteValue(currentDataItem(), 'Password', "")
							 setVisible(vm, 'WizardInstalacao_lblUserName', false);
							 setVisible(vm, 'WizardInstalacao_tbUserName', false);
							 setVisible(vm, 'WizardInstalacao_lblPassword', false);
							 setVisible(vm, 'WizardInstalacao_tbPassword', false);
						}else {
							 setVisible(vm, 'WizardInstalacao_lblUserName', true);
							 setVisible(vm, 'WizardInstalacao_tbUserName', true);
							 setVisible(vm, 'WizardInstalacao_lblPassword', true);
							 setVisible(vm, 'WizardInstalacao_tbPassword', true);
						}
					}
					else{
						app.showMessage(data.MensagemErro[0], 'Alerta', ['OK']);
					}
					
					vm.dataToolbar.isBusy(false);
                }
            });
	
}

if(oldIndex == 1 && newIndex == 2){
	
//materializa o controle combo
//$lx(vm,'#WizardSelfHostSetup_cmbAplicacoes').igCombo();	

}


/*data: JSON.stringify({"IdLoja": vm.currentDataItem().IdLoja(),"CodigoLoja":vm.currentDataItem().CodigoLoja(),"NomeLoja":vm.currentDataItem().NomeLoja(),"DataBase":vm.currentDataItem().Database(),"UserNameDB":vm.currentDataItem().UserName(),"PasswordDB":vm.currentDataItem().Password(),"Server":vm.currentDataItem().Server(),"WinAuth":vm.currentDataItem().WindowsAuthentication(),"URLServico":vm.currentDataItem().URLServico(),"UserServico":vm.currentDataItem().UsuarioServico(),"PasswordServico":vm.currentDataItem().SenhaServico(),"ApplicationId": getUIControlValue(vmControlName(vm, '#cmbAplicacoes'))}),*/

if(oldIndex == 3  && newIndex == 4){
	$('.button-submit').hide();
	var url;
	vm.dataToolbar.isBusy(true);
	$.ajax({
                url: vm.getServiceAddress('LinxAppConfigManagerSelfHost/SaveData'),
                dataType: 'json',
				data: JSON.stringify({"IdLoja": vm.currentDataItem().IdLoja(),"CodigoLoja":vm.currentDataItem().CodigoLoja(),"NomeLoja":vm.currentDataItem().NomeLoja(),"DataBase":vm.currentDataItem().Database(),"UserNameDB":vm.currentDataItem().UserName(),"PasswordDB":vm.currentDataItem().Password(),"Server":vm.currentDataItem().Server(),"WinAuth":vm.currentDataItem().WindowsAuthentication(),"URLServico":vm.currentDataItem().URLServico(),"UserServico":vm.currentDataItem().UsuarioServico(),"PasswordServico":vm.currentDataItem().SenhaServico(),"ApplicationId": getUIControlValue(vmControlName(vm, '#cmbAplicacoes')), "Produto": vm.currentDataItem().Produto(), "UrlLocal": vm.currentDataItem().UrlLocal()}),
                cache: false,
                contentType: 'application/json; charset=utf-8',
                processData: false,
                type: 'POST',
                error: function (jqXHR, textStatus, errorThrown) {
                    vm.dataToolbar.isBusy(false);
                    app.showMessage(errorThrown, 'Alerta', ['OK']);
                },
                success: function (data) {
					if(data.Resultado){
						app.showMessage(data.Mensagem,'Alerta',['OK']);
						//WizardInstalacao_cntInputQRCODE
						
						$lx(vm.viewModel, "#WizardInstalacao_cntInputQRCODE").html("<div align='center'><img src='" + data.Url + "'/></div>");
					}
					else
						app.showMessage('Erro na gravação do Config: ' + data.Mensagem,'Alerta',['OK']);
					vm.dataToolbar.isBusy(false);
                }
            });
}
    }
    var TestaConnBD_Click = function () {
    var flag = true;
var _errorList = [];

if(vm.currentDataItem().Produto() == "Linx POS"){
	if(vm.currentDataItem().WindowsAuthentication() == false || isNullOrEmpty(vm.currentDataItem().WindowsAuthentication())){
			if(isNullOrEmpty(vm.currentDataItem().UserName())){
				flag = false;
				_errorList.push("Informe o [Usuário do Banco]");
			}
			if(isNullOrEmpty(vm.currentDataItem().Password())){
				flag = false;
				_errorList.push("Informe a [Senha do Banco]");
			}
		}else{
			
			if(!isNullOrEmpty(vm.currentDataItem().UserName())){
				flag = false;
				_errorList.push("Você optou pela autenticação Windows. Não informe [Usuário do Banco]");
			}
			if(!isNullOrEmpty(vm.currentDataItem().Password())){
				flag = false;
				_errorList.push("Você optou pela autenticação Windows. Não informe a [Senha do Banco]");
			}
		}
		
		if(isNullOrEmpty(vm.currentDataItem().Server())){
			flag = false;
			_errorList.push("Informe o [Servidor do Banco]");
		}
}else{
		if(isNullOrEmpty(vm.currentDataItem().IdLoja()) || vm.currentDataItem().IdLoja() == 0){
			flag = false;
			_errorList.push("[ID Loja] não encontrado");
		}
		if(isNullOrEmpty(vm.currentDataItem().Database())){
			flag = false;
			_errorList.push("[O Banco de dados] não foi encontrado");
		}
	}
	
	if (!flag) {
		app.showMessage('', 'Atenção');
		$('.messageBox .message').append('<p>Corrija os erros abaixos para verificar a conexão:</p>');
		$(_errorList).each(function (i, e) {
			$('.messageBox .message').append('<p> - ' + e + '</p>');
		});
		_errorList.splice(0,_errorList.length);
	}else{
		vm.dataToolbar.isBusy(true);
			$.ajax({
						url: vm.getServiceAddress('LinxAppConfigManagerSelfHost/TesteConnDB'),
						dataType: 'json',
						data: JSON.stringify({"CodigoLoja":vm.currentDataItem().CodigoLoja(),"NomeLoja":vm.currentDataItem().NomeLoja(),"DataBase":vm.currentDataItem().Database(),"UserNameDB":vm.currentDataItem().UserName(),"PasswordDB":vm.currentDataItem().Password(),"Server":vm.currentDataItem().Server(),"WinAuth":vm.currentDataItem().WindowsAuthentication(),"URLServico":vm.currentDataItem().URLServico(),"UserServico":vm.currentDataItem().UsuarioServico(),"PasswordServico":vm.currentDataItem().SenhaServico(), "Produto":vm.currentDataItem().Produto()}),
						cache: false,
						contentType: 'application/json; charset=utf-8',
						processData: false,
						type: 'POST',
						error: function (jqXHR, textStatus, errorThrown) {
							vm.dataToolbar.isBusy(false);
							if(jqXHR.statusText != ''){
								app.showMessage('Erro na requisição do Serviço [TesteConnDB]', 'Alerta', ['OK']);
							}
						},
						success: function (data) {
							if(data == true)
								app.showMessage('Teste de conexão realizado com sucesso!','Alerta',['OK']);
							else
								app.showMessage('Falha no teste de conexão','Alerta',['OK']);
							vm.dataToolbar.isBusy(false);
						}
					});
	}
    }
    var TestaConnBus_Click = function () {
    //para pegar as informações do combo via macro
//var a = getUIControlValue(vmControlName(vm, '#cmbAplicacoes'));

vm.dataToolbar.isBusy(true);
	$.ajax({
                url: vm.getServiceAddress('LinxAppConfigManagerSelfHost/TesteServiceBUS'),
                data: 'servicebus=' + vm.currentDataItem().URLServico() + '&user=' + vm.currentDataItem().UsuarioServico() + '&password=' + vm.currentDataItem().SenhaServico(),
                cache: false,
                contentType: 'application/json; charset=utf-8',
                processData: false,
                type: 'GET',
                error: function (jqXHR, textStatus, errorThrown) {
                    vm.dataToolbar.isBusy(false);
                    app.showMessage(errorThrown, 'Alerta', ['OK']);
                },
                success: function (data) {
					if(data.Resultado){
						createCombo(vmControlName(vm, '#cmbAplicacoes'), data.AppResultBase, 'DescricaoAplicacao', 'UidAplicacao')
						app.showMessage('Teste do barramento realizado com sucesso!','Alerta',['OK']);
					}
					else
						app.showMessage(data.Mensagem,'Alerta',['OK']);
					vm.dataToolbar.isBusy(false);
                }
            });
    }
    var OnWizardStepChanging = function (oldIndex, newIndex, id) {
    //Criticas
var flag = true;
var _errorList = [];
var obj = [];
if(oldIndex == 0  && newIndex == 1){
	edit();
	//$lx(vm,'#WizardSelfHostSetup_cmbAplicacoes').igCombo();
	createCombo(vmControlName(vm, '#cmbAplicacoes'), obj, 'DescricaoAplicacao', 'UidAplicacao')
}

if(oldIndex == 1  && newIndex == 2){
	
	if(vm.currentDataItem().Produto() == "Linx POS"){
		if(isNullOrEmpty(vm.currentDataItem().CodigoLoja())){
			flag = false;
			_errorList.push("Informe o [Código da Loja]");
		}
		if(isNullOrEmpty(vm.currentDataItem().NomeLoja())){
			flag = false;
			_errorList.push("Informe o [Nome da Loja]");
		}
		if(isNullOrEmpty(vm.currentDataItem().Database())){
			flag = false;
			_errorList.push("Informe o [Banco de Dados]");
		}
		
		if(vm.currentDataItem().WindowsAuthentication() == false || isNullOrEmpty(vm.currentDataItem().WindowsAuthentication())){
			if(isNullOrEmpty(vm.currentDataItem().UserName())){
				flag = false;
				_errorList.push("Informe o [Usuário do Banco]");
			}
			if(isNullOrEmpty(vm.currentDataItem().Password())){
				flag = false;
				_errorList.push("Informe a [Senha do Banco]");
			}
		}else{
			
			if(!isNullOrEmpty(vm.currentDataItem().UserName())){
				flag = false;
				_errorList.push("Você optou pela autenticação Windows. Não informe [Usuário do Banco]");
			}
			if(!isNullOrEmpty(vm.currentDataItem().Password())){
				flag = false;
				_errorList.push("Você optou pela autenticação Windows. Não informe a [Senha do Banco]");
			}
		}
		
		if(isNullOrEmpty(vm.currentDataItem().Server())){
			flag = false;
			_errorList.push("Informe o [Servidor do Banco]");
		}
	} 
	else{
		
		if(isNullOrEmpty(vm.currentDataItem().IdLoja())){
			flag = false;
			_errorList.push("[ID da Loja] não identificado");
		}
		
		if(isNullOrEmpty(vm.currentDataItem().Database())){
			flag = false;
			_errorList.push("[Banco de dados] não identificado");
		}
	}
	
	
	if (!flag) {
		app.showMessage('', 'Atenção');
		$('.messageBox .message').append('<p>Não foi possível ir para a próxima etapa. Para continuar, corrija os erros abaixo:</p>');
		$(_errorList).each(function (i, e) {
			$('.messageBox .message').append('<p> - ' + e + '</p>');
		});
		_errorList.splice(0,_errorList.length);
		return false;
	}
}

if(oldIndex == 2  && newIndex == 3){
	
if(isNullOrEmpty(vm.currentDataItem().URLServico())){
		flag = false;
		_errorList.push("Informe a [URL do Serviço]");
	}
	if(isNullOrEmpty(vm.currentDataItem().UsuarioServico())){
		flag = false;
		_errorList.push("Informe o [Usuário do Serviço]");
	}
	if(isNullOrEmpty(vm.currentDataItem().SenhaServico())){
		flag = false;
		_errorList.push("Informe a [Senha do Serviço]");
	}
	
	//var valueAppId = $lx(vm,'#WizardSelfHostSetup_cmbAplicacoes').igCombo('value');
	var valueAppId = getUIControlValue(vmControlName(vm, '#cmbAplicacoes'));
	if(valueAppId == null || valueAppId.length == 0){
		flag = false;
		_errorList.push("Verifique a conexão e selecione uma [Aplicação]");
	}
		
	if (!flag) {
		app.showMessage('', 'Atenção');
		$('.messageBox .message').append('<p>Não foi possível ir para a próxima etapa. Para continuar, corrija os erros abaixo:</p>');
		$(_errorList).each(function (i, e) {
			$('.messageBox .message').append('<p> - ' + e + '</p>');
		});
		_errorList.splice(0,_errorList.length);
		return false;
	}
}

return true;
    }
    var ResetService_Click = function () {
    vm.dataToolbar.isBusy(true);
$.ajax({
		url: vm.getServiceAddress('LinxAppConfigManagerSelfHost/ResetService'),
		dataType: 'json',
		cache: false,
		contentType: 'application/json; charset=utf-8',
		processData: false,
		type: 'GET',
		error: function (jqXHR, textStatus, errorThrown) {
			vm.dataToolbar.isBusy(false);
			app.showMessage(errorThrown, 'Alerta', ['OK']);
		},
		success: function (data) {
			setTimeout(function(){
				if(data.Resultado){
					app.showMessage(data.Mensagem,'Alerta',['OK']);
				}
				else {
					app.showMessage('Erro na reinicialização do serviço: ' + data.Mensagem,'Alerta',['OK']);
				}
				vm.dataToolbar.isBusy(false);
			}, 20000);					
		}
});
    }
    //#endregion Client Events
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
        parentEntityRelated = null;
        var e = { cancel: false, viewModel: vm };
        custom.beforeClearing(e);
        if (e.cancel) return;
        isBusy(true);
        if (restoreLastFilter(status() === 'C')) return clearComplete({ results: dataView() }, true);
        else return dataContext.clearSetupConfig(getBandeiraRede(), clearComplete);
    
        function clearComplete(data, holdRanges) {
            dataForUndo = [];
            dataView(data.results);
            if (holdRanges != true) vm.entitySearchRange.clear();
            if (typeof noBindingReport === 'boolean' && noBindingReport === true) { pageCount(1); currentPage(0); goToIndex(0); return; }
            pageCount(1);
            totalItemCount(data.results.length);
            currentPage(0);
            lastStatus = 'C';
            status('C');
            goToIndex(0);
            adjustFormView();
            dataBind();
            isBusy(false);
            clearInnerUIs();
            custom.afterClearing({ dataItem: data.results, viewModel: vm });
        }
    };
    var hasChanges = ko.computed(function () {
            return dataContext.hasChanges();
    });
    var hasInternalUIsValidationErrors = function () {
        for (var idx = 0; idx < vm.internalUIs.length; idx++) { var innerVM = vm[vm.internalUIs[idx]](); if (innerVM.status() === 'E' && innerVM.getDataContext().hasValidationErrors()) return true; }
        return false;
    };
    var hasInternalUIsSavingErrors = function () {
        for (var idx = 0; idx < vm.internalUIs.length; idx++) { var innerVM = vm[vm.internalUIs[idx]](); if (innerVM.status() === 'E' && !innerVM.onSavingValidation()) return true; }
        return false;
    };
    var commitInternalUIsData = function () {
        for (var idx = 0; idx < vm.internalUIs.length; idx++) { var innerVM = vm[vm.internalUIs[idx]](); !innerVM.dataBind('', true); }
        return false;
    };
    var onSavingValidation = function (changes) {
        if (!changes) changes = dataContext.getChanges();
        if (changes.length === 0) { return true; }
        for (var idxChange = 0; idxChange < changes.length; idxChange++) {
            var entity = changes[idxChange];
            if (typeof entity.OnSaving == 'function') {
               if (!entity.OnSaving()) { return false; }
            }
        }
        return true;
    }
    var save = function (isExclusion, externalSaveSucceeded) {
        if (typeof isExclusion !== 'boolean') isExclusion = false;
        if (isExclusion) { enableDataTrack(false, false); }
        var indexForUndoAction = currentDataIndex();
        if (isExclusion) { removeItem(); }
        dataBind('', true);
        commitInternalUIsData();
        var changes = dataContext.getChanges();
        if (!onSavingValidation(changes)) { if (isExclusion) return undo(indexForUndoAction); else return; }
        if (hasInternalUIsSavingErrors()) { if (isExclusion) return undo(indexForUndoAction); else return; }
        var e = { cancel: false, viewModel: vm };
        custom.beforeSaving(e);
        if (e.cancel) { if (isExclusion) return undo(indexForUndoAction); else return; }
        if (hasInternalUIsValidationErrors() || dataContext.hasValidationErrors()) { if (isExclusion) return undo(indexForUndoAction); else return dataBind(); }
        isSaving(true);
        vm.showProcessing('Salvando informações...');
        if (!isExclusion && currentDataItem()) { currentDataItem().checkForSendingAllRowsToServer(); }
        return dataContext.saveChanges(saveSucceeded, saveFailed).fin(complete);
    
        function complete() {
            vm.closeProcessing();
            isSaving(false);
        }
    
        function saveFailed(error) {
            if (isChildVM()) parentVM.dataToolbar.edit(true);
            if (isExclusion) return undo(indexForUndoAction); else return dataBind();
        }
    
        function saveSucceeded(saveResult) {
            if (dataView().length === 0 && !isChildVM()) return clear();
            lastStatus = 'Q';
            status('Q');
            if (dataView().length > 0) goToIndex(currentDataIndex());
            custom.afterSaving({ viewModel: vm });
            for (var idxChange = 0; idxChange < changes.length; idxChange++) {
                var entity = changes[idxChange];
                if (entity.isUnchanged() && (typeof entity.TableMedia == 'function') && !isNullOrEmpty(entity.TableMedia())) { entity.TableMedia(null); entity.entityAspect.setUnchanged(); }
                if (typeof entity.OnSaved == 'function') {
                   entity.OnSaved();
                }
            }
            if (isChildVM())
            {
                dataContext.clearAll();
                query(false, parentEntityRelated);
            }
            else {
                saveInnerUIs();
            }
            if (typeof externalSaveSucceeded == 'function') {
                externalSaveSucceeded();
            }
            dataBind();
        }
    };
    var dataForUndo = []
    var undo = function (indexForUndoAction) {
        var e = { cancel: false, viewModel: vm };
        custom.beforeCancelEdition(e);
        if (e.cancel) return;
        dataContext.cancelChanges();
        if ((typeof indexForUndoAction) === 'number' && !navigationByPage() && !isChildVM()) lastStatus = 'Q';
        if (lastStatus === 'C' || dataForUndo.length == 0) {
            clear();
        } else {
            dataView(dataForUndo);
            dataForUndo = [];
            status(lastStatus);
            goToIndex(((typeof indexForUndoAction) === 'number' ? indexForUndoAction : currentDataIndex()));
            custom.afterCancelEdition({ viewModel: vm });
            dataBind();
            undoInnerUIs();
        }
    };
    var print = function () {
        var e = { cancel: false, viewModel: vm };
        custom.beforePrinting(e);
        if (e.cancel) return false;
        custom.afterPrinting({ viewModel: vm });
        return true;
    };
    var acceptChanges = function () {
        if (!navigationByPage() && !isChildVM()) dataContext.acceptChanges();
    };
    var edit = function (noClearInnerUIs) {
        if (status() === 'E') return;
        if (!canAddChangeEntity()) return;
        acceptChanges();
        var e = { cancel: false, viewModel: vm };
        custom.beforeEditing(e);
        if (e.cancel) return;
        lastStatus = status();
        status('E');
        if (!noClearInnerUIs) clearInnerUIs();
        goToIndex(currentDataIndex());
        if (lastStatus === 'Q') dataForUndo = [].concat(dataView());
        //Enabling data track
        enableDataTrack(navigationByPage(), true);
        custom.afterEditing({ viewModel: vm });
        editInnerUIs();
    };
    var enableDataTrack = function (all, convertDetails) {
        adjustFormView();
        if (!all) {
           if (!isNullOrEmpty(currentDataItem()) && currentDataItem().isPOCO) {
               dataView()[currentDataIndex()] = dataContext.createEntity(currentDataItem().typeName, currentDataItem().getPrimitiveDTO(), true);
               if (convertDetails) { currentDataItem().enableDetailsDataTack(dataView()[currentDataIndex()]); }
           }
        } else {
           for (var idx = 0; idx < dataView().length; idx++) {
               var entity = dataView()[idx];
               if (entity.isPOCO)  {
                   dataView()[idx] = dataContext.createEntity(entity.typeName, entity.getPrimitiveDTO(), true);
                   if (convertDetails) entity.enableDetailsDataTack(dataView()[idx]);
               }
           }
        }
        if (dataView().length > 0) currentDataItem(dataView()[currentDataIndex()]);
        dataBind();
    };
    var setBandeiraRede = function () {
    };
    
    var createSetupConfig = function() {
        dataBind('dataView', true);
        var entity = dataContext.createSetupConfig();
        adjustExternalParentRelation(entity);
        entity.setBandeiraRede(getBandeiraRede());
        entity.setGpecon(getGpecon());
        dataView.push(entity);
        return entity;
    };
    
    var createAndNotifySetupConfig = function() {
        var entity = createSetupConfig();
        notifyPresentation('');
        return entity;
    };
    var notifyPresentation = function(dataSourceName) {
          return dataContext.notifyPresentation(dataSourceName);
    };
    
    var notifyInnerElements = function (element) {
        if (element)
        {
            dataBind('', true);
            try{ $(window).trigger('resize'); } catch(e){ console.log(e); }
            var innerElements = element.find("table");
            if (innerElements.length > 0 && (vm.dataSource.length > 0 || vm.internalUIs.length > 0)) {
                for (var idx = 0; idx < innerElements.length; idx++) {
                    if($(innerElements[idx]).parents('.tab-pane')[0].className.contains('active')) {
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
        else if (!isNullOrEmpty(vm.currentBrands) && vm.currentBrands.indexOf(',') === -1) return parseInt(vm.currentBrands);
        else return 0;
    };
    var getCurrentBrands = function() {
        if (uiSettings != null && uiSettings.lookupInfo && uiSettings.lookupInfo.vm  && uiSettings.lookupInfo.vm.hasBrand && (typeof uiSettings.lookupInfo.vm.getCurrentBrands === 'function')) return uiSettings.lookupInfo.vm.getCurrentBrands();
        else if (parentVM != null && parentVM.hasBrand && (typeof parentVM.getCurrentBrands === 'function')) return parentVM.getCurrentBrands();
        else return (isNullOrEmpty(vm.currentBrands) ? '0' : vm.currentBrands);
    };
    var showProcessing = function(message) {
        currentActivityInformation(message);
        if (isBusy() === true) isBusy.notifySubscribers(); else isBusy(true);
    };
    var closeProcessing = function() {
        currentActivityInformation('');
        if (isBusy() === false) isBusy.notifySubscribers(); else isBusy(false);
    };
    var getGpecon = function() {
        if (!isNullOrEmpty(managerAuth.idGpecon)) return parseInt(managerAuth.idGpecon);
        else return 0;
    };
    var deleteEntity = function (entity, isMultiSelection) {
        var e = { cancel: false, entityTypeName: entity.typeName, viewModel: vm };
        custom.beforeRemovingChild(e);
        if (e.cancel) return false;
        var selectedEntities = []
        if (isMultiSelection && !isNullOrEmpty(complement) && (typeof complement.selectedItems === 'function'))
            selectedEntities = complement.selectedItems(false);
        if (selectedEntities.length > 0) {
           for (var idx = 0; idx < selectedEntities.length; idx++) {
               var selectedEntity = selectedEntities[idx];
               if (typeof selectedEntity.OnDeleting == 'function') {
                   if (!selectedEntity.OnDeleting()) return false;
               }
               removeInnerDataUIs(selectedEntity);
               dataContext.deleteEntity(selectedEntity);
               if (entity.typeName == vm.rootDataTypeName) dataView.remove(selectedEntity);
               if (typeof selectedEntity.OnDeleted == 'function') {
                   selectedEntity.OnDeleted();
               }
           }
           if (typeof complement.clearSelectedItems === 'function') complement.clearSelectedItems();
           dataBind((entity.typeName == vm.rootDataTypeName ? 'dataView' : entity.typeName + 'List'));
           return false;
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
        }
        custom.afterRemovingChild({ entityTypeName: entity.typeName, viewModel: vm });
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
        if (lastStatus === 'C' && status() === 'Q' && !navigationByPage() && !isChildVM()) clear();
        if (parentEntity != null && (typeof parentEntity === 'object') && !isNullOrEmpty(parentEntity.typeName))
           parentEntityRelated = parentEntity;
        if (!canAddChangeEntity()) return;
        acceptChanges();
        var e = { cancel: false, viewModel: vm };
        custom.beforeAdding(e);
        if (e.cancel) return;
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
        goToItem(createSetupConfig());
        custom.afterAdding({ viewModel: vm });
        editInnerUIs();
        dataBind();
    };
    var remove = function () {
        acceptChanges();
        var e = { cancel: false, viewModel: vm };
        custom.beforeRemoving(e);
        if (e.cancel) return;
        app.showMessage('Deseja realmente excluir o registro selecionado?', 'Alerta', ['Yes', 'No'])
            .then(function (selectedOption) {
                if (selectedOption === 'Yes') {
                    if (!navigationByPage() && !isChildVM()) { dataForUndo = [].concat(dataView()); save(true); } else { removeItem(); }
                }
                return selectedOption;
             });
    };
    var removeParentRelatedItems = function () {
        for (var idx = 0; idx < dataView().length; idx++) { deleteEntity(dataView()[idx]); }
        dataView([]);
        goToIndex(0);
        dataBind();
    }
    var removeItem = function () {
        if (deleteEntity(currentDataItem()) === false) return false;
        var index = dataView.indexOf(currentDataItem());
        dataView.remove(currentDataItem());
        if (dataView().length > 0) {
            if (status() !== 'E') {
                lastStatus = status();
                status('E');
            }
            if (index > 0) { goToIndex(index-1); }
            else { goToIndex(0); }
            dataBind();
            custom.afterRemoving({ viewModel: vm });
        }
        else {
            goToIndex(0);
            dataBind();
        }
    };
    var goFirst = function () {
        var e = { cancel: false, viewModel: vm };
        custom.beforeGoingFirst(e);
        if (e.cancel) return;
        var item;
        if (navigationByPage() || (viewType() === 'Secundary') || (!(pageCount() === 1 || pageSize() === 0 || currentPage() === 0))) {
            item = refresh(0, false);
        } else {
            item = goToIndex(0);
        }
        custom.afterGoingFirst({ viewModel: vm });
        return item;
    };
    var goBack = function () {
        var e = { cancel: false, viewModel: vm };
        custom.beforeGoingPrevious(e);
        if (e.cancel) return;
        var item;
        if (navigationByPage() || (viewType() === 'Secundary') || (!(pageCount() === 1 || pageSize() === 0 || currentPage() === 0) && currentDataIndex() === 0)) {
            item = refresh(currentPage()-1, !navigationByPage());
        } else {
            item = goToIndex(currentDataIndex()-1);
        }
        custom.afterGoingPrevious({ viewModel: vm });
        return item;
    };
    var goForward = function () {
        var e = { cancel: false, viewModel: vm };
        custom.beforeGoingNext(e);
        if (e.cancel) return;
        var item;
        if (navigationByPage() || (viewType() === 'Secundary') || (!(pageCount() === 1 || pageSize() === 0 || currentPage() === (pageCount()-1)) && currentDataIndex() === (dataView().length-1))) {
            item = refresh(currentPage()+1, false);
        } else {
            item = goToIndex(currentDataIndex()+1);
        }
        custom.afterGoingNext({ viewModel: vm });
        return item;
    };
    var goLast = function() {
        var e = { cancel: false, viewModel: vm };
        custom.beforeGoingLast(e);
        if (e.cancel) return;
        var item;
        if (!navigationByPage() && (viewType() === 'Main') && (pageCount() === 1 || pageSize() === 0 || currentPage() === (pageCount()-1))) {
            item = goToIndex(dataView().length-1);
        } else {
            item = refresh(pageCount()-1, !navigationByPage() && (viewType() === 'Main'));
        }
        custom.afterGoingLast({ viewModel: vm });
        return item;
    };
    //Databar enable control
    var _canRefreshData = true, _canQuickSearch = true, _canAddNew = false, _canClear = false, _canCustomSearch = false, _canDelete = false, _canEdit = false, _canLayout = false, _canNavigate = false, _canPrint = false, _canSearch = false, _canExport = false;
    var setSecurity = function(pCanAddNew, pCanClear, pCanCustomSearch, pCanDelete, pCanEdit, pCanLayout, pCanNavigate, pCanPrint, pCanSearch, pCanExport) {
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
       refreshToolbar();
    };
    var refreshToolbar = function() {
       status.notifySubscribers();
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
    var canUndo = ko.computed(function () { return status() === 'E' && _canEdit && !isChildVM(); });
    var canNavigate = ko.computed(function () { return  (!canUndo() && !canQuery() && (dataView().length > 1 || pageCount() > 1) && _canNavigate); });
    var canPrint = ko.computed(function () { return ['C', 'Q'].indexOf(status()) >= 0 && _canPrint && !isChildVM(); });
    var canSave = ko.computed(function () {
           return !isSaving() && status() === 'E' && _canEdit && !isChildVM();
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
    var changeFormView = function () {
    }
    var canViewInfo = ko.computed(function () {
        return false;
    });
    var importPhoto = function () {
        require(['viewmodels/shared/modalMultimidiaBatch'], function (modalMultimidiaBatch) {
            modalMultimidiaBatch.showModal(dataContext).then(function (r, data) { });
        });
    };
    
    var entitySearchRange = {
        predefinedFilters: ko.observableArray(managerPredefined.predefinedFilters),
    
    };
    entitySearchRange.clear = function(){
    
    };
    
    function deleteGrid(element, cName, cDataItem_listItem, isMultiSelect) {
       var element = element;
       var cName = cName;
       var dataItem_ListItem = cDataItem_listItem.split(';');
       var currentdataItem = dataItem_ListItem[0];
       var currentlistItem = dataItem_ListItem[1];
       $(element).igGridUpdating('endEdit');
       var row = null;
       if(isMultiSelect){
       if($(element).igGrid('selectedRows').length > 0) row = $(element).igGrid('selectedRows')[0];
       } else { row = $(element).igGrid('selectedRow'); }
       var entity = findElementByKey(eval(currentlistItem), 'RowDataId', isNullOrEmpty(row) ? 0 : row.id);
       if (isNullOrEmpty(entity)) {
           app.showMessage('Nenhum registro selecionado!', 'Informação', ['Ok']);
           return;
       }
       if (deleteEntity(entity, isMultiSelect)) {
           eval(currentlistItem)['remove'](entity);
           $(element).igGridUpdating('deleteRow', row.id);
           var rows = $(element).igGrid('rows');
           if (rows.length > 0) {
               if (row.index > 0) $(element).igGridSelection('selectRow', (row.index - 1));
               else $(element).igGridSelection('selectRow', row.index);
               var ui = $(element).data('igGridUpdating');
               selectGridCurrentItem(goToKey, 'RowDataId', ui.grid.activeRow(), eval(currentdataItem), eval(currentlistItem));
           }
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
    
       var getSelectedIndex = function () {
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
    
       $('.ui-dialog:has(#' + $('#dialog' + cName + '').attr('id') + ')').empty().remove();
       if (getSelectedIndex() == -1){
          app.showMessage('Registro não selecionado!', 'Informação', ['Ok']);
          return;
       }
    
       configEditor(element, currentlistItem);
    
       function checkDisableControl() {
           var columns = $(element).igGridUpdating('option', 'columnSettings');
           columns.forEach(function (entry, index) {
               if (entry.fieldTplDisabled) {
                   var controlTemplate = $('[id^="' + $lx(vm, '#div').selector.replace('#', '') + '"][id$="_' + entry.columnKey + 'Template"]');
                   $(controlTemplate).append('<div style="position: absolute;top:0;left:0;width: 100%;height:100%;z-index:2;opacity:0.4;filter: alpha(opacity = 50)"></div>');
               };
           });
       }
    
       function showAndHideColumnsEditor() {
           if (vm.status() !== 'C') {
               var colunas = $(element).igGrid('option', 'columns');
               colunas.forEach(function (entry, index) {
                   if (entry.hidden && entry.key !== 'RowDataId') {
                       var control = $('#WizardInstalacao_div' + (!dataView ? '' : '' + dataView + '_') + entry.key + 'Template');
                       if (!control.hasClass('hide') && !control.hasClass('onlyEditor'))
                           control.addClass('hide');
                   } else if (entry.key !== 'RowDataId') {
                       var control = $('#WizardInstalacao_div' + (!dataView ? '' : '' + dataView + '_') + entry.key + 'Template');
                       if (control.hasClass('hide'))
                           control.removeClass('hide');
                   }
               });
           }
       }
    
       if (vm.status() !== 'E') {
           $('#addReg' + cName + '').hide();
           $('#delReg' + cName + '').hide();
       }
       else {
           $('#addReg' + cName + '').show();
           $('#delReg' + cName + '').show();
       }
    
       var pk_id = getSelectedIndex() + 1;
       var ds = ui.grid.dataSource;
       var columns = ui.grid.options.columns;
    
       var hasPaging = $.grep($(element).igGrid('option', 'features'), function (e){
            return e.name == 'Paging'; 
       });
       fillLabels(pk_id);
    
       function fillLabels(current) {
           checkDisableControl();
           showAndHideColumnsEditor();
           var totalGrid = ui.grid.options.dataSource.count();
    
           if (hasPaging.length > 0) {
               var totalCurrentPage = totalGrid;
               var currentPage = $(element).igGridPaging('pageIndex') + 1;
               var pageIndex = $(element).igGridPaging('pageIndex');
               var pageSize = $(element).igGridPaging('pageSize');
               if (totalGrid / pageSize > currentPage)
                   totalCurrentPage = (currentPage * ui.grid.dataSource.dataView().length);
               if (currentPage > 1)
                   current = (pageIndex * pageSize) + current;
               $('label#currentNumber' + cName + '').html(current + ' - ' + totalCurrentPage);
           }
           else
               $('label#currentNumber' + cName + '').html((current == 0 ? totalGrid : current));
           $('label#totalNumber' + cName + '').html(totalGrid);
       }
       function updateGrid(grd, pk) {
           if (pk > 0) {
               var propUpdate = 0;
               for (i = 1; i < columns.length; ++i) {
                   if (columns[i].key.indexOf('Multi') < 0) {
                       propUpdate = getAbsoluteValue(eval(currentdataItem + '()')['' + columns[i].key + '']);
                       if (grd[columns[i].key] != propUpdate) {
                           grd[columns[i].key] = propUpdate;
                           $(element).igGridUpdating('updateRow', grd['RowDataId'], grd);
                       }
                   }
               }
           }
       }
       function updateTemplate(pk, step) {
           if (step == 1) {
               if (hasPaging.length == 0)
                   pk = pk - 2;
               $(element).igGridSelection('selectRow', pk);
               gridTrData = ui.grid.dataSource.dataView()[pk];
           }
           else if (step == 2) {
               $(element).igGridSelection('selectRow', pk);
               gridTrData = ui.grid.dataSource.dataView()[pk];
           }
           updateFieldsTemplate(gridTrData['RowDataId']);
       }
       function updateFieldsTemplate(grd) {
           if (vm.goToKey && 'RowDataId' && grd) {
               vm.goToKey('RowDataId', grd, eval(currentdataItem), eval(currentlistItem));
           }
       }
       $.fn['backReg' + cName + ''] = function () {
           if (hasPaging.length > 0) {
               gridTrData = ui.grid.dataSource.dataView()[getSelectedIndex()];
               if (getSelectedIndex() > 0) {
                   pk_id = getSelectedIndex() - 1;
                   $(element).igGridSelection('clearSelection');
                   updateGrid(gridTrData, pk_id);
                   updateTemplate(pk_id, 1);
                   fillLabels(pk_id + 1);
               }
           }
           else{
               gridTrData = ui.grid.dataSource.dataView()[pk_id - 1];
               updateGrid(gridTrData, pk_id);
               $(element).igGridSelection('clearSelection');
               if (pk_id > 1) {
                   updateTemplate(pk_id, 1);
                   pk_id = pk_id - 1;
               }
               else
                   $(element).igGridSelection('selectRow', pk_id - 1);
               fillLabels(pk_id);
           }
       }
       $.fn['nextReg' + cName + ''] = function () {
           if (hasPaging.length > 0) {
               gridTrData = ui.grid.dataSource.dataView()[getSelectedIndex()];
               pk_id = getSelectedIndex() + 1;
               if (ui.grid.dataSource.dataView().length > pk_id) {
                   $(element).igGridSelection('clearSelection');
                   updateGrid(gridTrData, pk_id);
                   updateTemplate(pk_id, 2);
                   pk_id = pk_id + 1;
               }
               else
                   $(element).igGridSelection('selectRow', pk_id - 1);
           }
           else{
               gridTrData = ui.grid.dataSource.dataView()[pk_id - 1];
               updateGrid(gridTrData, pk_id);
               var totalGrid = ui.grid.options.dataSource.count();
               $(element).igGridSelection('clearSelection');
               if (totalGrid > pk_id) {
                   updateTemplate(pk_id, 2);
                   pk_id = pk_id + 1;
               }
               else
                   $(element).igGridSelection('selectRow', pk_id - 1);
               }
          fillLabels(pk_id);
       }
       $.fn['addReg' + cName + ''] = function () {
           eval('vm.createAndNotify' + entityName);
           pk_id = ui.grid.options.dataSource.count();
           if (hasPaging.length > 0)
               gridTrData = ui.grid.dataSource.dataView()[0];
           else
               gridTrData = ui.grid.dataSource.dataView()[pk_id - 1];
           updateFieldsTemplate(gridTrData['RowDataId']);
           fillLabels(pk_id);
       }
       $.fn['delReg' + cName + ''] = function () {
           gridTrData = ui.grid.dataSource.dataView()[pk_id - 1];
           var entity = findElementByKey(eval(currentlistItem), 'RowDataId', gridTrData['RowDataId']);
           if (entity) {
               vm.deleteEntity(entity);
               eval(currentlistItem).remove(entity);
               vm.notifyPresentation(currentlistItem);
           }
           $(element).igGridUpdating('deleteRow', gridTrData['RowDataId']);
           var totalGrid = ui.grid.options.dataSource.count();
           if (totalGrid === 0) return restartGrid(element, cName, isEditorWithinGrid);
           if ((pk_id - 1) == totalGrid) {
               gridTrData = ui.grid.dataSource.dataView()[totalGrid - 1];
               $(element).igGridSelection('selectRow', totalGrid - 1);
               pk_id = pk_id - 1;
           }
           else {
               gridTrData = ui.grid.dataSource.dataView()[pk_id - 1];
               $(element).igGridSelection('selectRow', pk_id - 1);
           }
           updateFieldsTemplate(gridTrData['RowDataId']);
           fillLabels(pk_id);
       }
       $.fn['okReg' + cName + ''] = function () {
           if (pk_id === 0) pk_id = 1;
           if (pk_id > ui.grid.dataSource.dataView().length) pk_id = ui.grid.dataSource.dataView().length;
           gridTrData = ui.grid.dataSource.dataView()[pk_id - 1];
           updateGrid(gridTrData, pk_id);
           $(element + '_EditorBtn').attr('title', 'Alterar edição para modo Template');
           return restartGrid(element, cName, isEditorWithinGrid);
       }
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
           $('#dialog' + cName + '').appendTo($(element + '_ContentDLG'));
           $('#dialog' + cName + '').show();
       }
    
       return false;
    };
       function configEditor(element, currentlistItem){
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
               $(element).igGridUpdating('option', 'editMode', 'rowedittemplate');
               $(element).igGridUpdating('option', 'startEditTriggers', 'dblclick,F2');
               $('.fa.fa-th').addClass('fa fa-list-alt').removeClass('fa-th');
               $(element + '_EditorBtn').attr('title', 'Alterar edição para modo Célula');
           }
           else {
               $(element).igGridUpdating('option', 'editMode', 'cell');
               $(element).igGridUpdating('option', 'startEditTriggers', 'click');
               $('.fa.fa-list-alt').addClass('fa fa-th').removeClass('fa-list-alt');
               $(element + '_EditorBtn').attr('title', 'Alterar edição para modo Template');
           }
    };
       function restartGrid(element, cName, isEditorWithinGrid) {
           $(element).igGridUpdating('option', 'editMode', 'cell');
           $(element).igGridUpdating('option', 'startEditTriggers', 'click');
           $('.fa.fa-list-alt').addClass('fa fa-th').removeClass('fa-list-alt');
           $(element).attr('title', 'Alterar edição para modo Template');
           if (isEditorWithinGrid) {
               if (cName.indexOf('dialog') > -1)
                   $(cName).attr('style', 'display: none !important;');
               else
                   $('#dialog' + cName + '').attr('style', 'display: none !important;');
    
                $(element + '_ContentDLG').next().removeClass('hide');
                $(element + '_container').parent().removeClass('hide');
           }
           else
               $('#dialog' + cName + '').dialog('close');
       }
    
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
            title: function() { return (uiSettings && uiSettings.displayName ? uiSettings.displayName : 'Configuração Inicial'); }
        };
    
    var vm = {
            dataView: dataView,
            custom: custom,
            viewName: 'WizardInstalacao',
            currentDataItem: currentDataItem,
            exportDataDetails: exportDataDetails,
            openEditor: openEditor,
            deleteGrid: deleteGrid,
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
            addNewToInnerUI: addNewToInnerUI,
            getDataFromInnerUI: getDataFromInnerUI,
            queryInnerUIs: queryInnerUIs,
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
            managerAuth: managerAuth,
            rootBmTypeName: '',
            rootDataTypeName: 'SetupConfig',
            rootNamespace: 'Linx.AppConfigManager.BV.SelfHost',
            setSecurity: setSecurity,
            isReportComposition: isReportComposition,
            refreshToolbar: refreshToolbar,
            refreshCurrentBind: refreshCurrentBind,
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
            OnWizardStepChanged: OnWizardStepChanged,
            TestaConnBD_Click: TestaConnBD_Click,
            TestaConnBus_Click: TestaConnBus_Click,
            OnWizardStepChanging: OnWizardStepChanging,
            ResetService_Click: ResetService_Click,
            createSetupConfig: createSetupConfig,
            createAndNotifySetupConfig: createAndNotifySetupConfig,
            deleteEntity: deleteEntity,
            currentBrands: managerBrand.BRANDS_VM.length > 0 ? managerBrand.BRANDS_VM[0].id : '',
            brands: managerBrand.BRANDS_VM,
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
            __moduleId__: 'pkg_linx-appconfigmanager-bv-spa/viewmodels/WizardInstalacao'
        };
    
    dataContext.setCurrentViewModel(vm);
    return vm;
}

return vmInstance;
});
