    /* jshint ignore:start */
    'use strict';
    
    var name = 'Demo_ClientLocalService2ClientErpFactory';
    
    var dependencies = [
            '$state',
            '$log',
            '$rootScope',
            'commonFactory',
            'busyFactory',
            'dialogFactory',
            'messengeFactory',
            'shellManagerService',
            'Demo_ClientLocalService2ExtendedClientErpFactory',
            'Demo_PaiFilhaClientErpService'
    ];
    
    var dataBusinessFactory = function ($state, $log, $rootScope, common, busy, dialog, messenger, shellManagerService, extendedDataBusiness, dataContextConstructor) {
        
        var dataContext = new dataContextConstructor();
        var customSearch = function () { 
        };
        var translatedJEntitySearch = '';
        var customSearchResult = { searchDefinition: '', serializedSearch: '', translatedSearch: '' };
        var sortInfo = '';
        var currentSettings = null;
        var registeredUIs = [];
        var viewClosed = false;
        var lastJEntitySearch = null;
        var lastStatus = '';
        var _status = 'N';
        var status = function (value) {
            if (typeof value !== 'undefined')
                _status = value;
            return _status;
        };
        var _isDependentVM = false;
        var isDependentVM = function (value) {
            if (typeof value !== 'undefined')
                _isDependentVM = value;
            return _isDependentVM;
        };
        var _transactionNumberControl = ('00000000');
        var transactionNumberControl = function (value) {
            if (typeof value !== 'undefined')
                _transactionNumberControl = value;
            return _transactionNumberControl;
        };
        var _navigationByPage = false;
        var navigationByPage = function (value) {
            if (typeof value !== 'undefined')
                _navigationByPage = value;
            return _navigationByPage;
        };
        var _hasMainTopDataGrid = false;
        var hasMainTopDataGrid = function (value) {
            if (typeof value !== 'undefined')
                _hasMainTopDataGrid = value;
            return _hasMainTopDataGrid;
        };
        var _currentDataIndex = 0;
        var currentDataIndex = function (value) {
            if (typeof value !== 'undefined')
                _currentDataIndex = value;
            return _currentDataIndex;
        };
        var _currentDataItem = null;
        var currentDataItem = function (value) {
            if (typeof value !== 'undefined')
                _currentDataItem = value;
            return _currentDataItem;
        };
        var _currentPage = 0;
        var currentPage = function (value) {
            if (typeof value !== 'undefined')
                _currentPage = value;
            return _currentPage;
        };
        var _pageCount = 0;
        var pageCount = function (value) {
            if (typeof value !== 'undefined')
                _pageCount = value;
            return _pageCount;
        };
        var _pageSize = 100;
        var pageSize = function (value) {
            if (typeof value !== 'undefined')
                _pageSize = value;
            return _pageSize;
        };
        var _totalItemCount = 0;
        var totalItemCount = function (value) {
            if (typeof value !== 'undefined')
                _totalItemCount = value;
            return _totalItemCount;
        };
        var _isSaving = false;
        var isSaving = function (value) {
            if (typeof value !== 'undefined')
                _isSaving = value;
            return _isSaving;
        };
        var dataView = [];
        var showDataFeedUrl = function() {
            if (extendedDataBusiness.OnToolbarAction && !extendedDataBusiness.OnToolbarAction('ShowFeed')) return;
            dialog.showAlert(dataContext.getDataFeedUrl(), 'Endereço do serviço');
        };
        var lastSearchFilter = function () {
            if (extendedDataBusiness.OnToolbarAction && !extendedDataBusiness.OnToolbarAction('ShowCurrentFilter')) return;
            var filterTranslation = getTranslatedFilter();
            dialog.showAlert((common.isNullOrEmpty(filterTranslation) ? 'Pesquisa sem filtros.' : filterTranslation), 'Filtros da pesquisa');
        }
        var currentRecord = function () {
            if (pageSize() === 0) return currentDataIndex();
            else return (currentPage() * pageSize()) + currentDataIndex();
        };
        var totalRecords = function () {
            if (pageSize() === 0) return dataView.length;
        
            var recordCount = 0;
            if (currentPage() === 0) {
                if (pageCount() <= 1) {
                     recordCount = dataView.length;
                } else {
                     recordCount = totalItemCount() - pageSize() +  dataView.length;
                }
            } else if (currentPage() === (pageCount() - 1)) {
                recordCount = (pageSize() * (pageCount() - 1)) + dataView.length;
            } else {
                recordCount = pageSize() * (currentPage() + 1);
                recordCount += totalItemCount() - (pageSize() * (currentPage() + 2));
                recordCount += dataView.length;
            }
            return recordCount;
        };
        var currentFormattedRecord = function () {
            if (totalRecords() === 0) return '0';
            else return (currentRecord()+1).toString();
        };
        var currentRecordInfo = function () { var totalR = totalRecords(); if (totalR === 0) { return '0/0'; } else { return currentFormattedRecord() + '/' + totalR.toString(); } };
        //#region Form Events
        
        var started = false;
        var parentService = null;
        var uiSettings = null;
        var filteredEntities = [];
        //#region quick search
        //#endregion 
        
        var adjustModuleSecurity = function () {
            parentService = null;
            uiSettings = null;
            isDependentVM(false);
            setSecurity(true, true, true, true, true, true, true, true, true, true);
            if (shellManagerService.shellMode == 'PROD') {
               shellManagerService.getFormAccess('linx-demo-bv-ClientLocalService2', function (data) {
                  if (data && !data.AcessoTotal) {
                      setSecurity(data.Incluir, true, data.PesquisaEspecial, data.Excluir, data.Alterar, data.Layout, true, data.Imprimir, data.Pesquisar, data.Exportar);
                  }
               }, null);
            }
        };
        var initService = function() {
          if (!started) { started = true; clear(); }
            if (extendedDataBusiness.OnInit) extendedDataBusiness.OnInit();
          return true;
        };
        //#endregion
        var getMaxLength = function(entityName, propertyName){
            if (common.isNullOrEmpty(entityName)) entityName = 'Loja';
            var property = dataContext.getEntityProperty(entityName, propertyName);
            if(property != null)
                return property.maxLength;
            else
                return 0;
        };
        var loadDataView = function () {
        };
        var getParentSelectorDataName = function () {
           return ((typeof uiSettings === 'object') ? uiSettings.parentSelectorDataName : '');
        };
        var getJExpression = function (currentDI) {
            if (typeof currentDI === 'undefined') currentDI = currentDataItem();
            return currentDI.getJExpression(dataBusiness.entitySearchRange, [], false);
        };
        var exportData = function (forceAdd) {
            if (extendedDataBusiness.OnToolbarAction && !extendedDataBusiness.OnToolbarAction('Export')) return;
        };
        var exportDataDetails = function (entity, detailName) {
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
                if (!common.isNullOrEmpty(valuesFilter) && items && items.length > 0) {
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
            if (extendedDataBusiness.OnToolbarAction && !extendedDataBusiness.OnToolbarAction('Refresh')) return;
            if (navigationByPage()) {
               var refreshIndexedData = function (currentIndex) {
                     if (currentIndex < dataView.length) {
                         if (currentIndex == 0) dataBusiness.showProcessing('Atualizando informações...');
                         dataView[currentIndex].refreshData().fin(function () { refreshIndexedData(currentIndex + 1); });
                     }
                     else {
                         dataBusiness.closeProcessing();
                     }
               };
               if (dataView.length > 0) {
                    refreshIndexedData(0);
               }
               return;
            }
            dataBusiness.showProcessing('Atualizando informações...');
            return currentDataItem().refreshData(false, complete);
        
            function complete() {
                dataBusiness.closeProcessing();
            }
        }
        var getTranslatedFilter = function () {
            return translatedJEntitySearch + (common.isNullOrEmpty(translatedJEntitySearch) || common.isNullOrEmpty(customSearchResult.translatedSearch) ? '' : ' e ') + customSearchResult.translatedSearch;
        }
        var getQueryFilter = function (currentDI) {
            if (typeof currentDI === 'undefined') currentDI = currentDataItem();
            currentDI.setBandeiraRede(getBandeiraRede());
            var eSearch = getJExpression(currentDI);
            if (eSearch === 'Error')
               return 'Error';
            if (extendedDataBusiness.OnSearching) {
               var extraFilter = extendedDataBusiness.OnSearching();
               if (extraFilter === 'Error')
                  return 'Error';
               if (!common.isNullOrEmpty(extraFilter)) eSearch += extraFilter;
            }
           translatedJEntitySearch = common.translateSearch(dataContext, eSearch);
            if (!common.isNullOrEmpty(customSearchResult.searchDefinition)) eSearch += customSearchResult.searchDefinition;
            return eSearch;
        }
        var setStatus = function (st) {
          status(st);
          goToIndex(currentDataIndex());
        };
        var removedEntities = [];
        var dataCache = []; //Initialize data cache
        var syncDataCache = function () {
            dataView.forEach(function (element) {
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
               result = dataView;
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
               result = dataView;
            }
            return _.filter(result, function (e) { return (e.ChangeState == 'I'); });
        }
        var allowMultiSelectionInSearch = function () {
           if ((typeof uiSettings.allowMultiSelectionInSearch !== 'undefined')) return uiSettings.allowMultiSelectionInSearch;
           else return true;
        }
        var isProcessing = false;
        function restoreLastFilter(clearFilters) {
                if (clearFilters) filteredEntities = [];
                if (filteredEntities.length === 0) return false;
                //Set Current Details
                for(var idx = 0; idx < filteredEntities.length; idx++) { filteredEntities[idx].setCurrentDetails(null); }
                dataView = [filteredEntities[0]];
                if (clearFilters) filteredEntities = [];
                return true;
        }
        
        function adjustNavigationByPage(isNavByPage) {
            navigationByPage(isNavByPage);
        }
        
        var preserveDataCurrentState = function () {
           return (status() !== 'C' && pageSize() === 0);
        }
        
        var query = function (quickSearchJExpression, externalQueryCallBack, noMessages, noDetails) {
            if (extendedDataBusiness.OnToolbarAction && !extendedDataBusiness.OnToolbarAction('Query')) return;
            if (isProcessing) return;
            isProcessing = true;
            filteredEntities = (status() === 'C' ? currentDataItem().getCurrentElements() : []);
            if (uiSettings != null && uiSettings.noSearch) { dataView = [currentDataItem()]; status('Q'); return complete(); }
            lastJEntitySearch = ((typeof quickSearchJExpression !== 'string') || common.isNullOrEmpty(quickSearchJExpression) ? getQueryFilter(currentDataItem()) : quickSearchJExpression);
            if (lastJEntitySearch === 'Error')
                return complete();
            dataBusiness.showProcessing('Pesquisando informações...');
            var hasError = true;
            return dataContext.getLojaByEntitySearchNoAssociations(lastJEntitySearch, 0, pageSize(), (pageSize() > 0), sortInfo, querySucceeded, complete);
        
            function complete() {
                isProcessing = false;
                dataBusiness.closeProcessing();
                if (hasError === true) {
                   clear();
                }
            }
        
            function querySucceeded(data) {
                if (dataBusiness.status() !== 'E') { for (var idx = 0; idx < data.results.length; idx++) { dataContext.initializePOCO(data.results[idx], 'Loja'); } }
                hasError = false;
                dataView = data.results;
                if (dataView.length === 0 && ((parentService == null))) {
                    dataBusiness.closeProcessing();
                    if (!noMessages) {
                       messenger.warning('Nenhum registro foi encontrado!');
                       //Restore clear state
                       if (restoreLastFilter()) {
                          pageCount(1);
                          totalItemCount(1);
                          currentPage(0);
                          status('C');
                          goToIndex(0);
                       }
                       else {
                          clear();
                       }
                       return true;
                    }
                }
                pageCount( (pageSize() > 0 ? Math.ceil((data.inlineCount ? data.inlineCount : dataView.length) / pageSize()) : 1) );
                totalItemCount((data.inlineCount ? data.inlineCount : dataView.length));
                currentPage(0);
                status('Q');
                goToIndex(0, noDetails);
                if (dataView.length == 0) dataBusiness.closeProcessing();
                if (extendedDataBusiness.OnSearched) extendedDataBusiness.OnSearched();
                if (typeof externalQueryCallBack === 'function') externalQueryCallBack();
            }
        };
        function goToIndex(index, noDetails) {
            if (dataView.length === 0) { currentDataIndex(0); currentDataItem(null); return true; }
            if (index < 0) { index = 0; }
            else if (index >= dataView.length) { index = dataView.length - 1; }
            if (extendedDataBusiness.OnNavigating && status() !== 'C' && currentDataItem() !== null && currentDataItem() !== dataView[index]) { if (!extendedDataBusiness.OnNavigating(currentDataIndex(), index)) return; }
            currentDataIndex(index);
            var oldValue = currentDataItem();
            currentDataItem(dataView[index]);
            if (status() !== 'C' && currentDataItem() !== null && oldValue !== currentDataItem()) {
               if (!noDetails) currentDataItem().fillDetails();
               if (extendedDataBusiness.OnNavigated) extendedDataBusiness.OnNavigated(index);
            }
        }
        function goToItem(item) {
                goToIndex(dataView.indexOf(item));
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
        var refresh = function (curPage, goLast, callback) {
            dataBusiness.showProcessing('Pesquisando informações...');
            return dataContext.getLojaByEntitySearchNoAssociations(lastJEntitySearch, curPage * pageSize(), pageSize(), false, sortInfo, querySucceeded, complete);
        
            function complete() {
                dataBusiness.closeProcessing();
            }
        
            function querySucceeded(data) {
                if (dataBusiness.status() !== 'E') { for (var idx = 0; idx < data.results.length; idx++) { dataContext.initializePOCO(data.results[idx], 'Loja'); } }
                dataView = data.results;
                currentPage(curPage);
                goToIndex((goLast ? dataView.length : 0));
                if (callback) callback();
            }
        };
        var clearByUser = function (force) {
            if (force != true && !common.isNullOrEmpty(customSearchResult.searchDefinition)) {
                dialog.showMessage({ body: 'Deseja limpar a pesquisa avançada?', buttons: 'yesno', defaultButton: 2 })
                .then(function (ret) {
                    if (ret === 'yes') {
                        customSearchResult.searchDefinition = '';
                        customSearchResult.serializedSearch = '';
                        customSearchResult.translatedSearch = '';
                    }
                    return clear();
                 });
            }
            else return clear(false, force);
        }
        var clear = function (noBindingReport, force) {
            if (extendedDataBusiness.OnToolbarAction && !extendedDataBusiness.OnToolbarAction('Clear')) return;
            if (extendedDataBusiness.OnClearing) { if (!extendedDataBusiness.OnClearing()) return; }
            if (restoreLastFilter((status() === 'C') || (typeof force === 'boolean' && force === true))) return clearComplete({ results: dataView }, true);
            else return dataContext.clearLoja(getBandeiraRede(), clearComplete);
        
            function clearComplete(data, holdRanges) {
                dataForUndo = [];
                dataCache = []; //Initialize data cache
                removedEntities = []; //Initialize removeds
                dataView = data.results;
                if (holdRanges != true) dataBusiness.entitySearchRange.clear();
                if (typeof noBindingReport === 'boolean' && noBindingReport === true) { pageCount(1); currentPage(0); goToIndex(0); return; }
                pageCount(1);
                totalItemCount(data.results.length);
                currentPage(0);
                lastStatus = 'C';
                status('C');
                goToIndex(0);
                if (extendedDataBusiness.OnCleared) { extendedDataBusiness.OnCleared(); }
            }
        };
        var hasChanges = function () {
                return (getAllChanges().length > 0);
        };
        var onSavingValidation = function (changes) {
            if (!changes) changes = getAllChanges();
            if (changes.length === 0) { return true; }
            if (extendedDataBusiness.OnSaving) { if (!extendedDataBusiness.OnSaving(changes)) { return false; } }
            for (var idxChange = 0; idxChange < changes.length; idxChange++) {
                var entity = changes[idxChange];
                if (typeof entity.OnSaving == 'function') {
                   if (!entity.OnSaving()) { return false; }
                }
            }
            return true;
        }
        var saveAndContinue = function (externalSaveSucceeded) {
           save(false, function() {
                edit();
                if (typeof externalSaveSucceeded == 'function') {
                    externalSaveSucceeded();
                }
           });
        }
        var save = function (isExclusion, externalSaveSucceeded) {
            if (typeof isExclusion !== 'boolean') isExclusion = false;
            if (!isExclusion && extendedDataBusiness.OnToolbarAction && !extendedDataBusiness.OnToolbarAction('Save')) return;
            var indexForUndoAction = currentDataIndex();
            if (isExclusion) { removeItem(); }
            var changes = getAllChanges();
            if (!onSavingValidation(changes)) { if (isExclusion) return undo(indexForUndoAction); else return; }
            if (dataContext.hasValidationErrors(changes)) { if (isExclusion) return undo(indexForUndoAction); else return; }
            isSaving(true);
            dataBusiness.showProcessing('Salvando informações...');
            if (!isExclusion && currentDataItem()) { currentDataItem().checkForSendingAllRowsToServer(); }
            return dataContext.saveChanges(saveSucceeded, saveFailed, complete);
        
            function complete() {
                dataBusiness.closeProcessing();
                isSaving(false);
            }
        
            function saveFailed(error) {
                if (isExclusion) return undo(indexForUndoAction); else return;
            }
        
            function saveSucceeded(saveResult) {
                dataForUndo = [];
                dataCache = []; //Initialize data cache
                removedEntities = []; //Initialize removeds
                var toList = dataView;
                var fromList = saveResult.results;
                for (var idxElem = toList.length - 1; idxElem >= 0; idxElem--) {
                   if (toList[idxElem].ChangeState === 'D') toList.splice(idxElem, 1);
                }
                for (var idxElem = toList.length - 1; idxElem >= 0; idxElem--) {
                        if (toList[idxElem].ChangeState !== 'N') {
                                   var fromObj = _.where(fromList, { IdLoja: toList[idxElem]['IdLoja'] });
                           if (fromObj.length > 0) toList[idxElem].copyDataFrom(fromObj[0], true);
                        }
                }
                if (dataView.length === 0) return clear();
                lastStatus = 'Q';
                status('Q');
                if (dataView.length > 0) goToIndex(currentDataIndex());
                for (var idxChange = 0; idxChange < changes.length; idxChange++) {
                    var entity = changes[idxChange];
                    if (entity.isUnchanged() && (typeof entity.TableMedia == 'function') && !common.isNullOrEmpty(entity.TableMedia())) { entity.TableMedia(null); entity.entityAspect.setUnchanged(); }
                    if (typeof entity.OnSaved == 'function') {
                       entity.OnSaved();
                    }
                }
                if (extendedDataBusiness.OnSaved) { extendedDataBusiness.OnSaved(changes); }
                if (typeof externalSaveSucceeded == 'function') {
                    externalSaveSucceeded();
                }
            }
        };
        var dataForUndo = []
        var undo = function (indexForUndoAction) {
            if (extendedDataBusiness.OnToolbarAction && !extendedDataBusiness.OnToolbarAction('Undo')) return;
            if (extendedDataBusiness.OnCancelling) { if (!extendedDataBusiness.OnCancelling()) return; }
            dataContext.cancelChanges();
            if ((typeof indexForUndoAction) === 'number' && !navigationByPage()) lastStatus = 'Q';
            if (lastStatus === 'C' || dataForUndo.length == 0) {
                clear();
            } else {
                dataView = dataForUndo;
                dataForUndo = [];
                dataCache = []; //Initialize data cache
                removedEntities = []; //Initialize removeds
                status(lastStatus);
                goToIndex(((typeof indexForUndoAction) === 'number' ? indexForUndoAction : currentDataIndex()));
                if (extendedDataBusiness.OnCancelled)  { extendedDataBusiness.OnCancelled(); }
            }
        };
        var print = function () {
            if (extendedDataBusiness.OnToolbarAction && !extendedDataBusiness.OnToolbarAction('Report')) return;
            if (extendedDataBusiness.OnPrinting) { if (!extendedDataBusiness.OnPrinting()) return false; }
            if (extendedDataBusiness.OnPrinted) { extendedDataBusiness.OnPrinted(); }
            return true;
        };
        var acceptChanges = function () {
            if (!navigationByPage()) dataContext.acceptChanges();
        };
        var edit = function () {
            if (status() === 'E') return;
            if (extendedDataBusiness.OnToolbarAction && !extendedDataBusiness.OnToolbarAction('Edit')) return;
            if (!canAddChangeEntity()) return;
            acceptChanges();
            if (extendedDataBusiness.OnEditing) { if (!extendedDataBusiness.OnEditing()) return; }
            lastStatus = status();
            status('E');
            goToIndex(currentDataIndex());
            if (lastStatus === 'Q') dataForUndo = [].concat(dataView);
            if (extendedDataBusiness.OnEdited) { extendedDataBusiness.OnEdited(); }
        };
        var setBandeiraRede = function () {
        };
        
        var createLoja = function() {
            var entity = dataContext.createLoja();
            entity.setBandeiraRede(getBandeiraRede());
            entity.setGpecon(getGpecon());
            dataView.push(entity);
            return entity;
        };
        
        var createAndNotifyLoja = function() {
            var entity = createLoja();
            return entity;
        };
        
        var createVendedor = function(parent) {
            var entity = dataContext.createVendedor(parent);
            entity.setBandeiraRede(getBandeiraRede());
            entity.setGpecon(getGpecon());
           if (!common.isNullOrEmpty(parent)) { parent.currentVendedor = entity; entity.fillDetails(); } 
            return entity;
        };
        
        var createAndNotifyVendedor = function(parent) {
            var entity = createVendedor(parent);
            return entity;
        };
        var getBandeiraRede = function() {
            if (parentService != null && (typeof parentService.getBandeiraRede === 'function')) return parentService.getBandeiraRede();
            else if (!common.isNullOrEmpty(dataBusiness.currentBrands) && dataBusiness.currentBrands.indexOf(',') === -1) return parseInt(dataBusiness.currentBrands);
            else return 0;
        };
        var getCurrentBrands = function() {
            if (parentService != null && parentService.hasBrand && (typeof parentService.getCurrentBrands === 'function')) return parentService.getCurrentBrands();
            else return (common.isNullOrEmpty(dataBusiness.currentBrands) ? '0' : dataBusiness.currentBrands);
        };
        var showProcessing = function(message) {
            busy.showMessage(message);
        };
        var closeProcessing = function() {
            busy.hideMessage()
        };
        var getGpecon = function() {
            return shellManagerService.getEconomicGroupId();
        };
        var deleteEntity = function (entity, isMultiSelection) {
            var selectedEntities = []
            if (isMultiSelection && !common.isNullOrEmpty(complement) && (typeof complement.selectedItems === 'function'))
                selectedEntities = complement.selectedItems(false);
            if (selectedEntities.length > 0) {
               for (var idx = 0; idx < selectedEntities.length; idx++) {
                   var selectedEntity = selectedEntities[idx];
                   if (typeof selectedEntity.OnDeleting == 'function') {
                       if (!selectedEntity.OnDeleting()) return false;
                   }
                   dataContext.deleteEntity(selectedEntity);
                   if (entity.typeName == dataBusiness.rootDataTypeName) dataView.remove(selectedEntity);
                   if (typeof selectedEntity.OnDeleted == 'function') {
                       selectedEntity.OnDeleted();
                   }
                   if (selectedEntity.ChangeState == 'D') removedEntities.push(selectedEntity);
               }
               if (typeof complement.clearSelectedItems === 'function') complement.clearSelectedItems();
               return false;
            }
            else {
               if (typeof entity.OnDeleting == 'function') {
                   if (!entity.OnDeleting()) return false;
               }
               dataContext.deleteEntity(entity);
               if (typeof entity.OnDeleted == 'function') {
                   entity.OnDeleted();
               }
               if (entity.typeName === dataBusiness.rootDataTypeName && typeof isMultiSelection !== 'undefined') {
                   if (entity.ChangeState == 'D') removedEntities.push(entity);
               }
            }
            return true;
        };
        var canAddChangeEntity = function () {
           return true;
        };
        var addNew = function () {
            if (!dataContext.dataParameters.isLoaded) {
               setTimeout(function () {
                   addNew();
               }, 1000);
               return;
            }
            if (lastStatus === 'C' && status() === 'Q' && !navigationByPage()) clear();
            if (extendedDataBusiness.OnToolbarAction && !extendedDataBusiness.OnToolbarAction('Add')) return;
            if (!canAddChangeEntity()) return;
            acceptChanges();
            if (status() === 'C') {
                dataView = [];
            }
            if (status() === 'Q') {
               dataForUndo = [].concat(dataView);
            }
            if (status() !== 'E') {
                lastStatus = status();
                status('E');
            }
            goToItem(createLoja());
        };
        var remove = function () {
            if (extendedDataBusiness.OnToolbarAction && !extendedDataBusiness.OnToolbarAction('Delete')) return;
            acceptChanges();
            dialog.showMessage({ body: 'Deseja realmente excluir o registro selecionado?', buttons: 'yesno', defaultButton: 2 })
                .then(function (ret) {
                    if (ret === 'yes') {
                        if (!navigationByPage()) { dataForUndo = [].concat(dataView); save(true); } else { removeItem(); }
                    }
                 });
        };
        var removeParentRelatedItems = function () {
            for (var idx = 0; idx < dataView.length; idx++) { deleteEntity(dataView[idx]); }
            dataView = [];
            goToIndex(0);
        }
        var removeItem = function () {
            if (deleteEntity(currentDataItem()) === false) return false;
            var index = dataView.indexOf(currentDataItem());
            if (currentDataItem().ChangeState == 'D') removedEntities.push(currentDataItem());
            dataView.splice(index, 1);
            if (dataView.length > 0) {
                if (status() !== 'E') {
                    lastStatus = status();
                    status('E');
                }
                if (index > 0) { goToIndex(index-1); }
                else { goToIndex(0); }
            }
            else {
                goToIndex(0);
            }
        };
        var goFirst = function (callback) {
            if (extendedDataBusiness.OnToolbarAction && !extendedDataBusiness.OnToolbarAction('First')) return;
            var item;
            if (navigationByPage() || (!(pageCount() === 1 || pageSize() === 0 || currentPage() === 0))) {
                item = refresh(0, false, callback);
            } else {
                item = goToIndex(0);
                if (callback) callback();
            }
            return item;
        };
        var goBack = function (callback) {
            if (extendedDataBusiness.OnToolbarAction && !extendedDataBusiness.OnToolbarAction('Back')) return;
            var item;
            if (navigationByPage() || (!(pageCount() === 1 || pageSize() === 0 || currentPage() === 0) && currentDataIndex() === 0)) {
                item = refresh(currentPage()-1, !navigationByPage(), callback);
            } else {
                item = goToIndex(currentDataIndex()-1);
                if (callback) callback();
            }
            return item;
        };
        var goForward = function (callback) {
            if (extendedDataBusiness.OnToolbarAction && !extendedDataBusiness.OnToolbarAction('Next')) return;
            var item;
            if (navigationByPage() || (!(pageCount() === 1 || pageSize() === 0 || currentPage() === (pageCount()-1)) && currentDataIndex() === (dataView.length-1))) {
                item = refresh(currentPage()+1, false, callback);
            } else {
                item = goToIndex(currentDataIndex()+1);
                if (callback) callback();
            }
            return item;
        };
        var goLast = function(callback) {
            if (extendedDataBusiness.OnToolbarAction && !extendedDataBusiness.OnToolbarAction('Last')) return;
            var item;
            if (!navigationByPage() && (pageCount() === 1 || pageSize() === 0 || currentPage() === (pageCount()-1))) {
                item = goToIndex(dataView.length-1);
                if (callback) callback();
            } else {
                item = refresh(pageCount()-1, !navigationByPage(), callback);
            }
            return item;
        };
        //Databar enable control
        var _canRefreshData = true, _canQuickSearch = true, _canAddNew = true, _canClear = true, _canCustomSearch = true, _canDelete = true, _canEdit = true, _canLayout = true, _canNavigate = true, _canPrint = true, _canSearch = true, _canExport = true;
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
        };
        var isReportComposition = function (reportName) {
            if (!common.isNullOrEmpty(reportName))
            {
                for (var idx in dataContext.entityNames)
                {
                    if (dataContext.entityNames[idx].indexOf('ParentComposition') > -1 && reportName.indexOf(dataBusiness.rootNamespace + '.' + dataContext.entityNames[idx]) > -1)
        	            return true;
                }
            }
            return false;
        }
        var canGoFirst = (function () { return (status() === 'Q') && _canNavigate && ((!navigationByPage() && currentRecord() > 0) || (navigationByPage() && currentPage() > 0)); });
        var canGoBack = (function () { return (status() === 'Q') && _canNavigate && ((!navigationByPage() && currentRecord() > 0) || (navigationByPage() && currentPage() > 0)); });
        var canGoForward = (function () { return (status() === 'Q') && _canNavigate && ((!navigationByPage() && currentRecord() < (totalRecords()-1)) || (navigationByPage() && currentPage() < (pageCount()-1))); });
        var canGoLast = (function () { return (status() === 'Q') && _canNavigate && ((!navigationByPage() && currentRecord() < (totalRecords()-1)) || (navigationByPage() && currentPage() < (pageCount()-1))); });
        var canViewInfo = (function () { return (!hasMainTopDataGrid() && status() !== 'E' && totalRecords() > 0); });
        var canClear = (function () { return ['C', 'Q'].indexOf(status()) >= 0 && _canClear; });
        var canExport = (function () { return (status() === 'Q' || status() === 'C') && _canExport; });
        var canGridExport = (function () { return status() === 'Q' && _canExport; });
        var canQuery = (function () { return status() === 'C' && _canSearch; });
        var canCustomSearch = (function () { return status() === 'C' && _canCustomSearch; });
        var canQuickSearch = (function () { return false; });
        var hasDataFeed = (function () { return status() === 'C' && _canSearch && dataContext.hasDataFeed && parentService == null; });
        var canAddNew = (function () { return ((['Q', 'C'].indexOf(status()) >= 0) || (status() === 'E' && navigationByPage())) && _canAddNew; });
        var canRemove = (function () { return (dataView.length > 0) && ((!navigationByPage() && status() === 'Q')) && _canDelete; });
        var canEdit = (function () { return status() === 'Q' && _canEdit; });
        var canRefreshCurrentData = (function () { return status() === 'Q' && _canSearch && _canRefreshData; });
        var canUndo = (function () { return status() === 'E' && _canEdit; });
        var canNavigate = (function () { return  (!canUndo() && !canQuery() && (dataView.length > 1 || pageCount() > 1) && _canNavigate); });
        var canPrint = (function () { return ['C', 'Q'].indexOf(status()) >= 0 && _canPrint; });
        var canSave = (function () {
               return !isSaving() && status() === 'E' && _canEdit;
        });
        var enabledForEditing = (function () {
                return ['E', 'C'].indexOf(status()) >= 0;
        });
        var isEditable = function () {
            return _canEdit;
        };
        var navigateTo = function (viewName) {
            $state.go(viewName);
        };
        var viewInfo = function () {
            if (extendedDataBusiness.OnToolbarAction && !extendedDataBusiness.OnToolbarAction('TableView')) return;
            //changeFormView();
        };
        
            var _LojaDatetimeLoja_typeRange = 'R'; var _LojaDatetimeLoja_begin = null; var _LojaDatetimeLoja_end = null; var _LojaDatetimeLoja_predefFilter = null; var _LojaDatetimeLoja_predefFilterName = null; var _LojaDatetimeLoja_predefValue = null;
    var _VendedorDatetimeVendedor_typeRange = 'R'; var _VendedorDatetimeVendedor_begin = null; var _VendedorDatetimeVendedor_end = null; var _VendedorDatetimeVendedor_predefFilter = null; var _VendedorDatetimeVendedor_predefFilterName = null; var _VendedorDatetimeVendedor_predefValue = null;
    var _LojaBigIntLoja_begin = null; var _LojaBigIntLoja_end = null;
    var _LojaComboboxLoja_begin = null; var _LojaComboboxLoja_end = null;
    var _LojaDecimalLoja_begin = null; var _LojaDecimalLoja_end = null;
    var _LojaIdLoja_begin = null; var _LojaIdLoja_end = null;
    var _LojaIntLoja_begin = null; var _LojaIntLoja_end = null;
    var _LojaSmallIntLoja_begin = null; var _LojaSmallIntLoja_end = null;
    var _VendedorComboboxVendedor_begin = null; var _VendedorComboboxVendedor_end = null;
    var _VendedorDecimalVendedor_begin = null; var _VendedorDecimalVendedor_end = null;
    var _VendedorIdVendedor_begin = null; var _VendedorIdVendedor_end = null;
    var _VendedorIntVendedor_begin = null; var _VendedorIntVendedor_end = null;
    var _VendedorSmallIntVendedor_begin = null; var _VendedorSmallIntVendedor_end = null;
    var _VendedorIdLoja = null;
        var entitySearchRange = {
            predefinedFilters: [],
            loadPredefinedFilters: function () {
                if (entitySearchRange.predefinedFilters.length == 0) {
                   //Load Here
                }
            },
            LojaDatetimeLoja_typeRange: function (value) { if (typeof value !== 'undefined') _LojaDatetimeLoja_typeRange = value; return _LojaDatetimeLoja_typeRange; }, LojaDatetimeLoja_predefFilter: function (value) { if (typeof value !== 'undefined') _LojaDatetimeLoja_predefFilter = value; return _LojaDatetimeLoja_predefFilter; }, LojaDatetimeLoja_predefFilterName: function (value) { if (typeof value !== 'undefined') _LojaDatetimeLoja_predefFilterName = value; return _LojaDatetimeLoja_predefFilterName; }, LojaDatetimeLoja_predefValue: function (value) { if (typeof value !== 'undefined') _LojaDatetimeLoja_predefValue = value; return _LojaDatetimeLoja_predefValue; }, LojaDatetimeLoja_begin: function (value) { if (typeof value !== 'undefined') _LojaDatetimeLoja_begin = value; return _LojaDatetimeLoja_begin; }, LojaDatetimeLoja_end: function (value) { if (typeof value !== 'undefined') _LojaDatetimeLoja_end = value; return _LojaDatetimeLoja_end; },
    VendedorDatetimeVendedor_typeRange: function (value) { if (typeof value !== 'undefined') _VendedorDatetimeVendedor_typeRange = value; return _VendedorDatetimeVendedor_typeRange; }, VendedorDatetimeVendedor_predefFilter: function (value) { if (typeof value !== 'undefined') _VendedorDatetimeVendedor_predefFilter = value; return _VendedorDatetimeVendedor_predefFilter; }, VendedorDatetimeVendedor_predefFilterName: function (value) { if (typeof value !== 'undefined') _VendedorDatetimeVendedor_predefFilterName = value; return _VendedorDatetimeVendedor_predefFilterName; }, VendedorDatetimeVendedor_predefValue: function (value) { if (typeof value !== 'undefined') _VendedorDatetimeVendedor_predefValue = value; return _VendedorDatetimeVendedor_predefValue; }, VendedorDatetimeVendedor_begin: function (value) { if (typeof value !== 'undefined') _VendedorDatetimeVendedor_begin = value; return _VendedorDatetimeVendedor_begin; }, VendedorDatetimeVendedor_end: function (value) { if (typeof value !== 'undefined') _VendedorDatetimeVendedor_end = value; return _VendedorDatetimeVendedor_end; },
        LojaBigIntLoja_begin: function (value) { if (typeof value !== 'undefined') _LojaBigIntLoja_begin = value; return _LojaBigIntLoja_begin; }, LojaBigIntLoja_end: function (value) { if (typeof value !== 'undefined') _LojaBigIntLoja_end = value; return _LojaBigIntLoja_end; },
        LojaComboboxLoja_begin: function (value) { if (typeof value !== 'undefined') _LojaComboboxLoja_begin = value; return _LojaComboboxLoja_begin; }, LojaComboboxLoja_end: function (value) { if (typeof value !== 'undefined') _LojaComboboxLoja_end = value; return _LojaComboboxLoja_end; },
        LojaDecimalLoja_begin: function (value) { if (typeof value !== 'undefined') _LojaDecimalLoja_begin = value; return _LojaDecimalLoja_begin; }, LojaDecimalLoja_end: function (value) { if (typeof value !== 'undefined') _LojaDecimalLoja_end = value; return _LojaDecimalLoja_end; },
        LojaIdLoja_begin: function (value) { if (typeof value !== 'undefined') _LojaIdLoja_begin = value; return _LojaIdLoja_begin; }, LojaIdLoja_end: function (value) { if (typeof value !== 'undefined') _LojaIdLoja_end = value; return _LojaIdLoja_end; },
        LojaIntLoja_begin: function (value) { if (typeof value !== 'undefined') _LojaIntLoja_begin = value; return _LojaIntLoja_begin; }, LojaIntLoja_end: function (value) { if (typeof value !== 'undefined') _LojaIntLoja_end = value; return _LojaIntLoja_end; },
        LojaSmallIntLoja_begin: function (value) { if (typeof value !== 'undefined') _LojaSmallIntLoja_begin = value; return _LojaSmallIntLoja_begin; }, LojaSmallIntLoja_end: function (value) { if (typeof value !== 'undefined') _LojaSmallIntLoja_end = value; return _LojaSmallIntLoja_end; },
        VendedorComboboxVendedor_begin: function (value) { if (typeof value !== 'undefined') _VendedorComboboxVendedor_begin = value; return _VendedorComboboxVendedor_begin; }, VendedorComboboxVendedor_end: function (value) { if (typeof value !== 'undefined') _VendedorComboboxVendedor_end = value; return _VendedorComboboxVendedor_end; },
        VendedorDecimalVendedor_begin: function (value) { if (typeof value !== 'undefined') _VendedorDecimalVendedor_begin = value; return _VendedorDecimalVendedor_begin; }, VendedorDecimalVendedor_end: function (value) { if (typeof value !== 'undefined') _VendedorDecimalVendedor_end = value; return _VendedorDecimalVendedor_end; },
        VendedorIdVendedor_begin: function (value) { if (typeof value !== 'undefined') _VendedorIdVendedor_begin = value; return _VendedorIdVendedor_begin; }, VendedorIdVendedor_end: function (value) { if (typeof value !== 'undefined') _VendedorIdVendedor_end = value; return _VendedorIdVendedor_end; },
        VendedorIntVendedor_begin: function (value) { if (typeof value !== 'undefined') _VendedorIntVendedor_begin = value; return _VendedorIntVendedor_begin; }, VendedorIntVendedor_end: function (value) { if (typeof value !== 'undefined') _VendedorIntVendedor_end = value; return _VendedorIntVendedor_end; },
        VendedorSmallIntVendedor_begin: function (value) { if (typeof value !== 'undefined') _VendedorSmallIntVendedor_begin = value; return _VendedorSmallIntVendedor_begin; }, VendedorSmallIntVendedor_end: function (value) { if (typeof value !== 'undefined') _VendedorSmallIntVendedor_end = value; return _VendedorSmallIntVendedor_end; },
        VendedorIdLoja: function (value) { if (typeof value !== 'undefined') _VendedorIdLoja = value; return _VendedorIdLoja; }
        };
        entitySearchRange.clear = function(){
                entitySearchRange.LojaDatetimeLoja_typeRange('R'); entitySearchRange.LojaDatetimeLoja_begin(null); entitySearchRange.LojaDatetimeLoja_end(null); entitySearchRange.LojaDatetimeLoja_predefFilter(null); entitySearchRange.LojaDatetimeLoja_predefValue(null);
        entitySearchRange.VendedorDatetimeVendedor_typeRange('R'); entitySearchRange.VendedorDatetimeVendedor_begin(null); entitySearchRange.VendedorDatetimeVendedor_end(null); entitySearchRange.VendedorDatetimeVendedor_predefFilter(null); entitySearchRange.VendedorDatetimeVendedor_predefValue(null);
        entitySearchRange.LojaBigIntLoja_begin(null); entitySearchRange.LojaBigIntLoja_end(null);
        entitySearchRange.LojaComboboxLoja_begin(null); entitySearchRange.LojaComboboxLoja_end(null);
        entitySearchRange.LojaDecimalLoja_begin(null); entitySearchRange.LojaDecimalLoja_end(null);
        entitySearchRange.LojaIdLoja_begin(null); entitySearchRange.LojaIdLoja_end(null);
        entitySearchRange.LojaIntLoja_begin(null); entitySearchRange.LojaIntLoja_end(null);
        entitySearchRange.LojaSmallIntLoja_begin(null); entitySearchRange.LojaSmallIntLoja_end(null);
        entitySearchRange.VendedorComboboxVendedor_begin(null); entitySearchRange.VendedorComboboxVendedor_end(null);
        entitySearchRange.VendedorDecimalVendedor_begin(null); entitySearchRange.VendedorDecimalVendedor_end(null);
        entitySearchRange.VendedorIdVendedor_begin(null); entitySearchRange.VendedorIdVendedor_end(null);
        entitySearchRange.VendedorIntVendedor_begin(null); entitySearchRange.VendedorIntVendedor_end(null);
        entitySearchRange.VendedorSmallIntVendedor_begin(null); entitySearchRange.VendedorSmallIntVendedor_end(null);
        entitySearchRange.VendedorIdLoja(null);
        };
        entitySearchRange.has_LojaBigIntLoja = function(){ return (entitySearchRange.LojaBigIntLoja_begin() != null || entitySearchRange.LojaBigIntLoja_end() != null); };
        entitySearchRange.has_LojaComboboxLoja = function(){ return (entitySearchRange.LojaComboboxLoja_begin() != null || entitySearchRange.LojaComboboxLoja_end() != null); };
        entitySearchRange.has_LojaDecimalLoja = function(){ return (entitySearchRange.LojaDecimalLoja_begin() != null || entitySearchRange.LojaDecimalLoja_end() != null); };
        entitySearchRange.has_LojaIdLoja = function(){ return (entitySearchRange.LojaIdLoja_begin() != null || entitySearchRange.LojaIdLoja_end() != null); };
        entitySearchRange.has_LojaIntLoja = function(){ return (entitySearchRange.LojaIntLoja_begin() != null || entitySearchRange.LojaIntLoja_end() != null); };
        entitySearchRange.has_LojaSmallIntLoja = function(){ return (entitySearchRange.LojaSmallIntLoja_begin() != null || entitySearchRange.LojaSmallIntLoja_end() != null); };
        entitySearchRange.has_VendedorComboboxVendedor = function(){ return (entitySearchRange.VendedorComboboxVendedor_begin() != null || entitySearchRange.VendedorComboboxVendedor_end() != null); };
        entitySearchRange.has_VendedorDecimalVendedor = function(){ return (entitySearchRange.VendedorDecimalVendedor_begin() != null || entitySearchRange.VendedorDecimalVendedor_end() != null); };
        entitySearchRange.has_VendedorIdVendedor = function(){ return (entitySearchRange.VendedorIdVendedor_begin() != null || entitySearchRange.VendedorIdVendedor_end() != null); };
        entitySearchRange.has_VendedorIntVendedor = function(){ return (entitySearchRange.VendedorIntVendedor_begin() != null || entitySearchRange.VendedorIntVendedor_end() != null); };
        entitySearchRange.has_VendedorSmallIntVendedor = function(){ return (entitySearchRange.VendedorSmallIntVendedor_begin() != null || entitySearchRange.VendedorSmallIntVendedor_end() != null); };
        entitySearchRange.has_LojaDatetimeLoja = function(){ return (entitySearchRange.LojaDatetimeLoja_typeRange() == 'R' && (entitySearchRange.LojaDatetimeLoja_begin() != null || entitySearchRange.LojaDatetimeLoja_end() != null) || (entitySearchRange.LojaDatetimeLoja_typeRange() == 'P' && !common.isNullOrEmpty(entitySearchRange.LojaDatetimeLoja_predefFilter()))); };
        entitySearchRange.has_VendedorDatetimeVendedor = function(){ return (entitySearchRange.VendedorDatetimeVendedor_typeRange() == 'R' && (entitySearchRange.VendedorDatetimeVendedor_begin() != null || entitySearchRange.VendedorDatetimeVendedor_end() != null) || (entitySearchRange.VendedorDatetimeVendedor_typeRange() == 'P' && !common.isNullOrEmpty(entitySearchRange.VendedorDatetimeVendedor_predefFilter()))); };
        
        function openEditor(element, cName, cDataItem_listItem, dataV_parentName, entityName) {
           return false;
        };
        
        var dataToolbar = {
                goFirst: goFirst,
                goBack: goBack,
                goForward: goForward,
                goLast: goLast,
                adjustNavigationByPage: adjustNavigationByPage,
                query: query,
                customSearch: customSearch,
                customSearchResult: customSearchResult,
                refreshCurrentData: refreshCurrentData,
                exportData: exportData,
                undo: undo,
                save: save,
                saveAndContinue: saveAndContinue,
                addNew: addNew,
                remove: remove,
                refresh: refresh,
                clear: clearByUser,
                print: print,
                showDataFeedUrl: showDataFeedUrl,
                edit: edit,
                viewInfo: viewInfo,
                lastSearchFilter: lastSearchFilter
        };
        //Extending dataToolbar object
        Object.defineProperty(dataToolbar, 'canQuickSearch', { get: canQuickSearch });
        Object.defineProperty(dataToolbar, 'canClear', { get: canClear });
        Object.defineProperty(dataToolbar, 'canQuery', { get: canQuery });
        Object.defineProperty(dataToolbar, 'canCustomSearch', { get: canCustomSearch });
        Object.defineProperty(dataToolbar, 'canRefreshCurrentData', { get: canRefreshCurrentData });
        Object.defineProperty(dataToolbar, 'canNavigate', { get: canNavigate });
        Object.defineProperty(dataToolbar, 'canGoFirst', { get: canGoFirst });
        Object.defineProperty(dataToolbar, 'canGoBack', { get: canGoBack });
        Object.defineProperty(dataToolbar, 'canGoForward', { get: canGoForward });
        Object.defineProperty(dataToolbar, 'canGoLast', { get: canGoLast });
        Object.defineProperty(dataToolbar, 'canViewInfo', { get: canViewInfo });
        Object.defineProperty(dataToolbar, 'currentRecordInfo', { get: currentRecordInfo });
        Object.defineProperty(dataToolbar, 'canAddNew', { get: canAddNew });
        Object.defineProperty(dataToolbar, 'canEdit', { get: canEdit });
        Object.defineProperty(dataToolbar, 'canRemove', { get: canRemove });
        Object.defineProperty(dataToolbar, 'canSave', { get: canSave });
        Object.defineProperty(dataToolbar, 'canUndo', { get: canUndo });
        Object.defineProperty(dataToolbar, 'canPrint', { get: canPrint });
        Object.defineProperty(dataToolbar, 'canExport', { get: canExport });
        Object.defineProperty(dataToolbar, 'hasDataFeed', { get: hasDataFeed });
        Object.defineProperty(dataToolbar, 'canGridExport', { get: canGridExport });
        
        var dataBusiness = {
                dataView: dataView,
                viewName: 'ClientLocalService2',
                getDataForSaving: getDataForSaving,
                currentDataItem: currentDataItem,
                currentDataIndex: currentDataIndex,
                goToDataItem: goToItem,
                goToDataIndex: goToIndex,
                exportDataDetails: exportDataDetails,
                openEditor: openEditor,
                navigationByPage: navigationByPage,
                hasMainTopDataGrid: hasMainTopDataGrid,
                dataShared: [],
                hasChanges: hasChanges,
                isSaving: isSaving,
                dataToolbar: dataToolbar,
                getDataContext: function() { return dataContext; },
                getExtendedDataBusiness: function() { return extendedDataBusiness; },
                getParentSelectorDataName: getParentSelectorDataName,
                getMaxLength: getMaxLength,
                status: status,
                removeParentRelatedItems: removeParentRelatedItems,
                onSavingValidation: onSavingValidation,
                goToKey: goToKey,
                //Service Events
                finalizeCombo: finalizeCombo,
                dataCombo: dataCombo,
                clearCombo: clearCombo,
                dataDomains: dataContext.dataDomains,
                //End Service Events
                lookUpProperties: dataContext.lookUpProperties,
                metadataInfo: dataContext.metadataInfo,
                dataExportInfo: dataContext.dataExportInfo,
                entityNames: dataContext.entityNames,
                lookUpNames: dataContext.lookUpNames,
                shellManagerService: dataContext.shellManagerService,
                rootBmTypeName: 'LOJA',
                rootDataTypeName: 'Loja',
                rootNamespace: 'Linx.Demo.BV.PaiFilha',
                setSecurity: setSecurity,
                isReportComposition: isReportComposition,
                getServiceAddress: dataContext.getServiceAddress,
                getBandeiraRede: getBandeiraRede,
                getCurrentBrands: getCurrentBrands,
                setBandeiraRede: setBandeiraRede,
                entitySearchRange: entitySearchRange,
                showProcessing: showProcessing,
                closeProcessing: closeProcessing,
                isDependentVM: isDependentVM,
                allowMultiSelectionInSearch: allowMultiSelectionInSearch,
                transactionNumberControl: transactionNumberControl,
                createLoja: createLoja,
                createAndNotifyLoja: createAndNotifyLoja,
                createVendedor: createVendedor,
                createAndNotifyVendedor: createAndNotifyVendedor,
                deleteEntity: deleteEntity,
                currentBrands: shellManagerService.getBrands(), 
                brands: [],
                hasBrand: false,
                controllerName: dataContext.controllerName,
                getJExpression: getJExpression,
                getQueryFilter: getQueryFilter,
                getTranslatedFilter: getTranslatedFilter,
                sortData: sortData,
                lastJEntitySearch: function () { return lastJEntitySearch; },
                isEditable: isEditable,
                setStatus: setStatus,
                common: common,
                navigateTo: navigateTo,
                __moduleId__: 'pkg_/controllers/ClientLocalService2'
        };
        Object.defineProperty(dataBusiness, 'enabledForEditing', {
            get: enabledForEditing
        });
        Object.defineProperty(dataBusiness, 'disabledForEditing', {
            get: function(){ return !enabledForEditing(); }
        });
        
        dataContext.setCurrentDataBusiness(dataBusiness);
        extendedDataBusiness.setCurrentDataBusiness(dataBusiness);
        initService();
        
        return dataBusiness;
    };
    
    module.exports = function(appModule) {
       appModule.factory(name, dependencies.concat(dataBusinessFactory));
    };
    /* jshint ignore:end */
