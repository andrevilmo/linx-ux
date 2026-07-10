define([
        'appModule'
], function (module) {
    'use strict';
    
    var name = 'Demo_clientLocalService1Factory';
    
    var dependencies = [
            '$state',
            '$log',
            '$rootScope',
            'commonFactory',
            'dialogFactory',
            'messengerFactory',
            'authService',
            'Demo_clientLocalService1ExtendedFactory',
            'Demo_paiFilhaService'
    ];
    
    var dataBusinessFactory = function ($state, $log, $rootScope, common, dialog, messenger, authService, extendedDataBusiness, dataContextConstructor) {
        
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
        var _isBusy = false;
        var isBusy = function (value) {
            if (typeof value !== 'undefined') {
                _isBusy = value;
                if (_isBusy)
                    $rootScope.$broadcast('loading:show', 'Aguarde...');
                else
                    $rootScope.$broadcast('loading:hide');
            }
            return _isBusy;
        };
        var _currentActivityInformation = '';
        var currentActivityInformation = function (value) {
            if (typeof value !== 'undefined')
                _currentActivityInformation = value;
            return _currentActivityInformation;
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
        var _dataView = [];
        var dataView = function (value) {
            if (typeof value !== 'undefined')
                _dataView = value;
            return _dataView;
        };
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
        };
        var currentFormattedRecord = function () {
            if (totalRecords() === 0) return '0';
            else return (currentRecord()+1).toString();
        };
        var currentRecordInfo = function () { var totalR = totalRecords(); if (totalR === 0) { return '0/0'; } else { return currentFormattedRecord() + '/' + totalR.toString(); } };
        var contextDataUpdateHandler = function (e) {
            dataBind(dataContext.dataForUpdate);
        };
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
            if (authService.shellMode == 'PROD') {
               authService.getFormAccess('linx-demo-bv-ClientLocalService1', function (data) {
                  if (data && !data.AcessoTotal) {
                      setSecurity(data.Incluir, true, data.PesquisaEspecial, data.Excluir, data.Alterar, data.Layout, true, data.Imprimir, data.Pesquisar, true);
                  }
               }, null);
            }
        };
        var initService = function() {
          if (!started) { started = true; clear(); } else { refreshToolbar(); }
            if (extendedDataBusiness.OnInit) extendedDataBusiness.OnInit();
          return true;
        };
        //#endregion
        var getMaxLength = function(entityName, propertyName){
            if (common.isNullOrEmpty(entityName)) entityName = 'Cliente';
            var property = dataContext.getEntityProperty(entityName, propertyName);
            if(property != null)
                return property.maxLength;
            else
                return 0;
        };
        var dataBind = function (dataName, commitData) {
        };
        var loadDataView = function () {
        };
        var getInnerJExpression = function () {
            if (!uiSettings.applyFilterToParent || common.isNullOrEmpty(currentDataItem())) return '';
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
            return '---' + currentDataItem().Namespace + '.' + currentDataItem().typeName + '|' + uiSettings.parentSelectorDataName + '|' + parentFieldsRelation + '|' + detailFieldsRelation + ':::' + jExp;
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
                     if (currentIndex < dataView().length) {
                         if (currentIndex == 0) dataBusiness.showProcessing('Atualizando informações...');
                         dataView()[currentIndex].refreshData().fin(function () { refreshIndexedData(currentIndex + 1); });
                     }
                     else {
                         dataBusiness.closeProcessing();
                         dataBind();
                     }
               };
               if (dataView().length > 0) {
                    refreshIndexedData(0);
               }
               return;
            }
            dataBusiness.showProcessing('Atualizando informações...');
            return currentDataItem().refreshData().fin(complete);
        
            function complete() {
            }
        }
        var getTranslatedFilter = function () {
            return translatedJEntitySearch + (common.isNullOrEmpty(translatedJEntitySearch) || common.isNullOrEmpty(customSearchResult.translatedSearch) ? '' : ' e ') + customSearchResult.translatedSearch;
        }
        var getQueryFilter = function (currentDI) {
            if (typeof currentDI === 'undefined') currentDI = currentDataItem();
            dataBind('', true);
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
        var allowMultiSelectionInSearch = function () {
           if ((typeof uiSettings.allowMultiSelectionInSearch !== 'undefined')) return uiSettings.allowMultiSelectionInSearch;
           else return true;
        }
        var freeEntityForQuerying = null;
        var isProcessing = false;
        function restoreLastFilter(clearFilters) {
                if (clearFilters) filteredEntities = [];
                if (filteredEntities.length === 0) return false;
                dataContext.clearAll();
                //Attach Elements
                for(var idx = 0; idx < filteredEntities.length; idx++) { dataContext.attachEntity(filteredEntities[idx]); }
                //Set Current Details
                for(var idx = 0; idx < filteredEntities.length; idx++) { filteredEntities[idx].setCurrentDetails(null); }
                dataView([filteredEntities[0]]);
                if (clearFilters) filteredEntities = [];
                return true;
        }
        
        function adjustNavigationByPage(isNavByPage) {
            navigationByPage(isNavByPage);
            dataBind();
        }
        
        var query = function (quickSearchJExpression, externalQueryCallBack, noMessages, noDetails) {
            if (extendedDataBusiness.OnToolbarAction && !extendedDataBusiness.OnToolbarAction('Query')) return;
            if (isProcessing) return;
            isProcessing = true;
            filteredEntities = (status() === 'C' ? currentDataItem().getCurrentElements() : []);
            if (uiSettings != null && uiSettings.noSearch) { dataView([currentDataItem()]); status('Q'); refreshToolbar(); return complete(); }
            lastJEntitySearch = ((typeof quickSearchJExpression !== 'string') || common.isNullOrEmpty(quickSearchJExpression) ? getQueryFilter(currentDataItem()) : quickSearchJExpression);
            if (lastJEntitySearch === 'Error')
                return complete();
            dataBusiness.showProcessing('Pesquisando informações...');
            var hasError = true;
            if (status() === 'C') { for(var idx = 0; idx < filteredEntities.length; idx++) { dataContext.detachEntity(filteredEntities[idx]); } }
            return dataContext.getClienteByEntitySearchNoAssociations(lastJEntitySearch, 0, pageSize(), (pageSize() > 0), false, status() !== 'E', sortInfo, querySucceeded, complete);
        
            function complete() {
                isProcessing = false;
                if (hasError === true) {
                   clear();
                }
            }
        
            function querySucceeded(data) {
                if (dataBusiness.status() !== 'E') { for (var idx = 0; idx < data.results.length; idx++) { dataContext.initializePOCO(data.results[idx], 'Cliente'); } }
                hasError = false;
                dataView(data.results);
                if (dataView().length === 0 && ((parentService == null))) {
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
                          dataBind();
                          isBusy(false);
                       }
                       else {
                          clear();
                       }
                       return true;
                    }
                }
                pageCount( (pageSize() > 0 ? Math.ceil((data.inlineCount ? data.inlineCount : dataView().length) / pageSize()) : 1) );
                totalItemCount((data.inlineCount ? data.inlineCount : dataView().length));
                currentPage(0);
                status('Q');
                goToIndex(0, noDetails);
                dataBind('dataView');
                if (dataView().length == 0) dataBusiness.closeProcessing();
                if (extendedDataBusiness.OnSearched) extendedDataBusiness.OnSearched();
                if (typeof externalQueryCallBack === 'function') externalQueryCallBack();
            }
        };
        function goToIndex(index, noDetails) {
            if (dataView().length === 0) { currentDataIndex(0); currentDataItem(null); return true; }
            if (index < 0) { index = 0; }
            else if (index >= dataView().length) { index = dataView().length - 1; }
            if (extendedDataBusiness.OnNavigating && status() !== 'C' && currentDataItem() !== null && currentDataItem() !== dataView()[index]) { if (!extendedDataBusiness.OnNavigating(currentDataIndex(), index)) return; }
            currentDataIndex(index);
            var oldValue = currentDataItem();
            currentDataItem(dataView()[index]);
            if (status() !== 'C' && currentDataItem() !== null && oldValue !== currentDataItem()) {
               if (!noDetails) currentDataItem().fillDetails();
               if (extendedDataBusiness.OnNavigated) extendedDataBusiness.OnNavigated(index);
            }
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
        var refresh = function (curPage, goLast, callback) {
            dataBusiness.showProcessing('Pesquisando informações...');
            return dataContext.getClienteByEntitySearchNoAssociations(lastJEntitySearch, curPage * pageSize(), pageSize(), false, false, status() !== 'E', sortInfo, querySucceeded, complete);
        
            function complete() {
            }
        
            function querySucceeded(data) {
                if (dataBusiness.status() !== 'E') { for (var idx = 0; idx < data.results.length; idx++) { dataContext.initializePOCO(data.results[idx], 'Cliente'); } }
                dataView(data.results);
                currentPage(curPage);
                goToIndex((goLast ? dataView().length : 0));
                dataBind('dataView');
                if (callback) callback();
            }
        };
        var clearByUser = function (force) {
            if (force != true && !common.isNullOrEmpty(customSearchResult.searchDefinition)) {
                dialog.showMessage('Deseja limpar a pesquisa avançada?', 'Alerta', 'Sim', 'Não')
                .then(function (yesResponse) {
                    if (yesResponse) {
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
            isBusy(true);
            if (restoreLastFilter((status() === 'C') || (typeof force === 'boolean' && force === true))) return clearComplete({ results: dataView() }, true);
            else return dataContext.clearCliente(getBandeiraRede(), clearComplete);
        
            function clearComplete(data, holdRanges) {
                dataForUndo = [];
                dataView(data.results);
                if (holdRanges != true) dataBusiness.entitySearchRange.clear();
                if (typeof noBindingReport === 'boolean' && noBindingReport === true) { pageCount(1); currentPage(0); goToIndex(0); return; }
                pageCount(1);
                totalItemCount(data.results.length);
                currentPage(0);
                lastStatus = 'C';
                status('C');
                goToIndex(0);
                dataBind();
                isBusy(false);
                if (extendedDataBusiness.OnCleared) { extendedDataBusiness.OnCleared(); }
            }
        };
        var hasChanges = function () {
                return dataContext.hasChanges();
        };
        var onSavingValidation = function (changes) {
            if (!changes) changes = dataContext.getChanges();
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
            if (isExclusion) { enableDataTrack(false, false); }
            if (!isExclusion && extendedDataBusiness.OnToolbarAction && !extendedDataBusiness.OnToolbarAction('Save')) return;
            var indexForUndoAction = currentDataIndex();
            if (isExclusion) { removeItem(); }
            dataBind('', true);
            var changes = dataContext.getChanges();
            if (!onSavingValidation(changes)) { if (isExclusion) return undo(indexForUndoAction); else return; }
            if (dataContext.hasValidationErrors()) { if (isExclusion) return undo(indexForUndoAction); else return dataBind(); }
            isSaving(true);
            dataBusiness.showProcessing('Salvando informações...');
            if (!isExclusion && currentDataItem()) { currentDataItem().checkForSendingAllRowsToServer(); }
            return dataContext.saveChanges(saveSucceeded, complete, saveFailed);
        
            function complete() {
                dataBusiness.closeProcessing();
                isSaving(false);
            }
        
            function saveFailed(error) {
                if (isExclusion) return undo(indexForUndoAction); else return dataBind();
            }
        
            function saveSucceeded(saveResult) {
                if (dataView().length === 0) return clear();
                lastStatus = 'Q';
                status('Q');
                if (dataView().length > 0) goToIndex(currentDataIndex());
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
                dataBind();
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
                dataView(dataForUndo);
                dataForUndo = [];
                status(lastStatus);
                goToIndex(((typeof indexForUndoAction) === 'number' ? indexForUndoAction : currentDataIndex()));
                dataBind();
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
            if (lastStatus === 'Q') dataForUndo = [].concat(dataView());
            //Enabling data track
            enableDataTrack(navigationByPage(), true);
            if (extendedDataBusiness.OnEdited) { extendedDataBusiness.OnEdited(); }
        };
        var enableDataTrack = function (all, convertDetails) {
            if (!all) {
               if (!common.isNullOrEmpty(currentDataItem()) && currentDataItem().isPOCO) {
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
        
        var createCliente = function() {
            dataBind('dataView', true);
            var entity = dataContext.createCliente();
            entity.setBandeiraRede(getBandeiraRede());
            entity.setGpecon(getGpecon());
            dataView().push(entity);
            return entity;
        };
        
        var createAndNotifyCliente = function() {
            var entity = createCliente();
            return entity;
        };
        
        var createVenda = function(parent) {
            dataBind('VendaList', true);
            var entity = dataContext.createVenda(parent);
            entity.setBandeiraRede(getBandeiraRede());
            entity.setGpecon(getGpecon());
           if (!common.isNullOrEmpty(parent)) { parent.currentVenda = entity; entity.fillDetails(); } 
            return entity;
        };
        
        var createAndNotifyVenda = function(parent) {
            var entity = createVenda(parent);
            return entity;
        };
        
        var createVendaItem = function(parent) {
            dataBind('VendaItemList', true);
            var entity = dataContext.createVendaItem(parent);
            entity.setBandeiraRede(getBandeiraRede());
            entity.setGpecon(getGpecon());
           if (!common.isNullOrEmpty(parent)) { parent.currentVendaItem = entity; entity.fillDetails(); } 
            return entity;
        };
        
        var createAndNotifyVendaItem = function(parent) {
            var entity = createVendaItem(parent);
            return entity;
        };
        
        var createVendaAtacado = function(parent) {
            dataBind('VendaAtacadoList', true);
            var entity = dataContext.createVendaAtacado(parent);
            entity.setBandeiraRede(getBandeiraRede());
            entity.setGpecon(getGpecon());
           if (!common.isNullOrEmpty(parent)) { parent.currentVendaAtacado = entity; entity.fillDetails(); } 
            return entity;
        };
        
        var createAndNotifyVendaAtacado = function(parent) {
            var entity = createVendaAtacado(parent);
            return entity;
        };
        var createEntity = function(entityName, initialValues) {
            var entity = dataContext.createEntity(entityName, initialValues);
            entity.setBandeiraRede(getBandeiraRede());
            entity.setGpecon(getGpecon());
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
            currentActivityInformation(message);
            if (isBusy() === false) isBusy(true);
        };
        var closeProcessing = function() {
            currentActivityInformation('');
            if (isBusy() === true) isBusy(false);
        };
        var getGpecon = function() {
            if (authService.userInfo.currentEnvironment && !common.isNullOrEmpty(authService.userInfo.currentEnvironment.IdLinxGpecon.toString())) return parseInt(authService.userInfo.currentEnvironment.IdLinxGpecon.toString());
            else return 0;
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
               }
               if (typeof complement.clearSelectedItems === 'function') complement.clearSelectedItems();
               dataBind((entity.typeName == dataBusiness.rootDataTypeName ? 'dataView' : entity.typeName + 'List'));
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
                dataContext.clearAll();
                dataView([]);
            }
            if (status() === 'Q') {
               dataForUndo = [].concat(dataView());
               if (navigationByPage()) enableDataTrack(true, true);
            }
            if (status() !== 'E') {
                lastStatus = status();
                status('E');
            }
            goToItem(createCliente());
            dataBind();
        };
        var remove = function () {
            if (extendedDataBusiness.OnToolbarAction && !extendedDataBusiness.OnToolbarAction('Delete')) return;
            acceptChanges();
            dialog.showMessage('Deseja realmente excluir o registro selecionado?', 'Alerta', 'Sim', 'Não')
                .then(function (yesResponse) {
                    if (yesResponse) {
                        if (!navigationByPage()) { dataForUndo = [].concat(dataView()); save(true); } else { removeItem(); }
                    }
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
            var index = dataView().indexOf(currentDataItem());
            dataView().splice(index, 1);
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
            if (navigationByPage() || (!(pageCount() === 1 || pageSize() === 0 || currentPage() === (pageCount()-1)) && currentDataIndex() === (dataView().length-1))) {
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
                item = goToIndex(dataView().length-1);
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
           refreshToolbar();
        };
        var refreshToolbar = function() {
        }
        var refreshCurrentBind = function() {
        }
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
        var canClear = (function () { return ['C', 'Q'].indexOf(status()) >= 0 && _canClear; });
        var canExport = (function () { return (status() === 'Q' || status() === 'C') && _canExport; });
        var canGridExport = (function () { return status() === 'Q' && _canExport; });
        var canQuery = (function () { return status() === 'C' && _canSearch; });
        var canCustomSearch = (function () { return status() === 'C' && _canCustomSearch; });
        var canQuickSearch = (function () { return false; });
        var hasDataFeed = (function () { return status() === 'C' && _canSearch && dataContext.hasDataFeed && parentService == null; });
        var canAddNew = (function () { return ((['Q', 'C'].indexOf(status()) >= 0) || (status() === 'E' && navigationByPage())) && _canAddNew; });
        var canRemove = (function () { return (dataView().length > 0) && ((!navigationByPage() && status() === 'Q')) && _canDelete; });
        var canEdit = (function () { return status() === 'Q' && _canEdit; });
        var canRefreshCurrentData = (function () { return status() === 'Q' && _canSearch && _canRefreshData; });
        var canUndo = (function () { return status() === 'E' && _canEdit; });
        var canNavigate = (function () { return  (!canUndo() && !canQuery() && (dataView().length > 1 || pageCount() > 1) && _canNavigate); });
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
        
            var _ClienteDatetimeCliente_typeRange = 'R'; var _ClienteDatetimeCliente_begin = null; var _ClienteDatetimeCliente_end = null; var _ClienteDatetimeCliente_predefFilter = null; var _ClienteDatetimeCliente_predefFilterName = null; var _ClienteDatetimeCliente_predefValue = null;
    var _VendaDatetimeVenda_typeRange = 'R'; var _VendaDatetimeVenda_begin = null; var _VendaDatetimeVenda_end = null; var _VendaDatetimeVenda_predefFilter = null; var _VendaDatetimeVenda_predefFilterName = null; var _VendaDatetimeVenda_predefValue = null;
    var _VendaItemDatetimeVendaItem_typeRange = 'R'; var _VendaItemDatetimeVendaItem_begin = null; var _VendaItemDatetimeVendaItem_end = null; var _VendaItemDatetimeVendaItem_predefFilter = null; var _VendaItemDatetimeVendaItem_predefFilterName = null; var _VendaItemDatetimeVendaItem_predefValue = null;
    var _VendaAtacadoDatetimeVendaAtacado_typeRange = 'R'; var _VendaAtacadoDatetimeVendaAtacado_begin = null; var _VendaAtacadoDatetimeVendaAtacado_end = null; var _VendaAtacadoDatetimeVendaAtacado_predefFilter = null; var _VendaAtacadoDatetimeVendaAtacado_predefFilterName = null; var _VendaAtacadoDatetimeVendaAtacado_predefValue = null;
    var _ClienteBigIntCliente_begin = null; var _ClienteBigIntCliente_end = null;
    var _ClienteDecimalCliente_begin = null; var _ClienteDecimalCliente_end = null;
    var _ClienteIdCliente_begin = null; var _ClienteIdCliente_end = null;
    var _ClienteIntCliente_begin = null; var _ClienteIntCliente_end = null;
    var _ClienteSmallIntCliente_begin = null; var _ClienteSmallIntCliente_end = null;
    var _VendaBigIntVenda_begin = null; var _VendaBigIntVenda_end = null;
    var _VendaDecimalVenda_begin = null; var _VendaDecimalVenda_end = null;
    var _VendaIdCliente_begin = null; var _VendaIdCliente_end = null;
    var _VendaIdVenda_begin = null; var _VendaIdVenda_end = null;
    var _VendaIntVenda_begin = null; var _VendaIntVenda_end = null;
    var _VendaSmallIntVenda_begin = null; var _VendaSmallIntVenda_end = null;
    var _VendaItemBigIntVendaItem_begin = null; var _VendaItemBigIntVendaItem_end = null;
    var _VendaItemDecimalVendaItem_begin = null; var _VendaItemDecimalVendaItem_end = null;
    var _VendaItemIdVenda_begin = null; var _VendaItemIdVenda_end = null;
    var _VendaItemIdVendaItem_begin = null; var _VendaItemIdVendaItem_end = null;
    var _VendaItemIntVendaItem_begin = null; var _VendaItemIntVendaItem_end = null;
    var _VendaItemSmallIntVendaItem_begin = null; var _VendaItemSmallIntVendaItem_end = null;
    var _VendaAtacadoBigIntVendaAtacado_begin = null; var _VendaAtacadoBigIntVendaAtacado_end = null;
    var _VendaAtacadoDecimalVendaAtacado_begin = null; var _VendaAtacadoDecimalVendaAtacado_end = null;
    var _VendaAtacadoIdCliente_begin = null; var _VendaAtacadoIdCliente_end = null;
    var _VendaAtacadoIdVendaAtacado_begin = null; var _VendaAtacadoIdVendaAtacado_end = null;
    var _VendaAtacadoIntVendaAtacado_begin = null; var _VendaAtacadoIntVendaAtacado_end = null;
    var _VendaAtacadoSmallIntVendaAtacado_begin = null; var _VendaAtacadoSmallIntVendaAtacado_end = null;
    var _ClienteIdEstado = null;
    var _VendaIdLoja = null;
        var entitySearchRange = {
            predefinedFilters: [],
            loadPredefinedFilters: function () {
                if (entitySearchRange.predefinedFilters.length == 0) {
                   //Load Here
                }
            },
            ClienteDatetimeCliente_typeRange: function (value) { if (typeof value !== 'undefined') _ClienteDatetimeCliente_typeRange = value; return _ClienteDatetimeCliente_typeRange; }, ClienteDatetimeCliente_predefFilter: function (value) { if (typeof value !== 'undefined') _ClienteDatetimeCliente_predefFilter = value; return _ClienteDatetimeCliente_predefFilter; }, ClienteDatetimeCliente_predefFilterName: function (value) { if (typeof value !== 'undefined') _ClienteDatetimeCliente_predefFilterName = value; return _ClienteDatetimeCliente_predefFilterName; }, ClienteDatetimeCliente_predefValue: function (value) { if (typeof value !== 'undefined') _ClienteDatetimeCliente_predefValue = value; return _ClienteDatetimeCliente_predefValue; }, ClienteDatetimeCliente_begin: function (value) { if (typeof value !== 'undefined') _ClienteDatetimeCliente_begin = value; return _ClienteDatetimeCliente_begin; }, ClienteDatetimeCliente_end: function (value) { if (typeof value !== 'undefined') _ClienteDatetimeCliente_end = value; return _ClienteDatetimeCliente_end; },
    VendaDatetimeVenda_typeRange: function (value) { if (typeof value !== 'undefined') _VendaDatetimeVenda_typeRange = value; return _VendaDatetimeVenda_typeRange; }, VendaDatetimeVenda_predefFilter: function (value) { if (typeof value !== 'undefined') _VendaDatetimeVenda_predefFilter = value; return _VendaDatetimeVenda_predefFilter; }, VendaDatetimeVenda_predefFilterName: function (value) { if (typeof value !== 'undefined') _VendaDatetimeVenda_predefFilterName = value; return _VendaDatetimeVenda_predefFilterName; }, VendaDatetimeVenda_predefValue: function (value) { if (typeof value !== 'undefined') _VendaDatetimeVenda_predefValue = value; return _VendaDatetimeVenda_predefValue; }, VendaDatetimeVenda_begin: function (value) { if (typeof value !== 'undefined') _VendaDatetimeVenda_begin = value; return _VendaDatetimeVenda_begin; }, VendaDatetimeVenda_end: function (value) { if (typeof value !== 'undefined') _VendaDatetimeVenda_end = value; return _VendaDatetimeVenda_end; },
    VendaItemDatetimeVendaItem_typeRange: function (value) { if (typeof value !== 'undefined') _VendaItemDatetimeVendaItem_typeRange = value; return _VendaItemDatetimeVendaItem_typeRange; }, VendaItemDatetimeVendaItem_predefFilter: function (value) { if (typeof value !== 'undefined') _VendaItemDatetimeVendaItem_predefFilter = value; return _VendaItemDatetimeVendaItem_predefFilter; }, VendaItemDatetimeVendaItem_predefFilterName: function (value) { if (typeof value !== 'undefined') _VendaItemDatetimeVendaItem_predefFilterName = value; return _VendaItemDatetimeVendaItem_predefFilterName; }, VendaItemDatetimeVendaItem_predefValue: function (value) { if (typeof value !== 'undefined') _VendaItemDatetimeVendaItem_predefValue = value; return _VendaItemDatetimeVendaItem_predefValue; }, VendaItemDatetimeVendaItem_begin: function (value) { if (typeof value !== 'undefined') _VendaItemDatetimeVendaItem_begin = value; return _VendaItemDatetimeVendaItem_begin; }, VendaItemDatetimeVendaItem_end: function (value) { if (typeof value !== 'undefined') _VendaItemDatetimeVendaItem_end = value; return _VendaItemDatetimeVendaItem_end; },
    VendaAtacadoDatetimeVendaAtacado_typeRange: function (value) { if (typeof value !== 'undefined') _VendaAtacadoDatetimeVendaAtacado_typeRange = value; return _VendaAtacadoDatetimeVendaAtacado_typeRange; }, VendaAtacadoDatetimeVendaAtacado_predefFilter: function (value) { if (typeof value !== 'undefined') _VendaAtacadoDatetimeVendaAtacado_predefFilter = value; return _VendaAtacadoDatetimeVendaAtacado_predefFilter; }, VendaAtacadoDatetimeVendaAtacado_predefFilterName: function (value) { if (typeof value !== 'undefined') _VendaAtacadoDatetimeVendaAtacado_predefFilterName = value; return _VendaAtacadoDatetimeVendaAtacado_predefFilterName; }, VendaAtacadoDatetimeVendaAtacado_predefValue: function (value) { if (typeof value !== 'undefined') _VendaAtacadoDatetimeVendaAtacado_predefValue = value; return _VendaAtacadoDatetimeVendaAtacado_predefValue; }, VendaAtacadoDatetimeVendaAtacado_begin: function (value) { if (typeof value !== 'undefined') _VendaAtacadoDatetimeVendaAtacado_begin = value; return _VendaAtacadoDatetimeVendaAtacado_begin; }, VendaAtacadoDatetimeVendaAtacado_end: function (value) { if (typeof value !== 'undefined') _VendaAtacadoDatetimeVendaAtacado_end = value; return _VendaAtacadoDatetimeVendaAtacado_end; },
        ClienteBigIntCliente_begin: function (value) { if (typeof value !== 'undefined') _ClienteBigIntCliente_begin = value; return _ClienteBigIntCliente_begin; }, ClienteBigIntCliente_end: function (value) { if (typeof value !== 'undefined') _ClienteBigIntCliente_end = value; return _ClienteBigIntCliente_end; },
        ClienteDecimalCliente_begin: function (value) { if (typeof value !== 'undefined') _ClienteDecimalCliente_begin = value; return _ClienteDecimalCliente_begin; }, ClienteDecimalCliente_end: function (value) { if (typeof value !== 'undefined') _ClienteDecimalCliente_end = value; return _ClienteDecimalCliente_end; },
        ClienteIdCliente_begin: function (value) { if (typeof value !== 'undefined') _ClienteIdCliente_begin = value; return _ClienteIdCliente_begin; }, ClienteIdCliente_end: function (value) { if (typeof value !== 'undefined') _ClienteIdCliente_end = value; return _ClienteIdCliente_end; },
        ClienteIntCliente_begin: function (value) { if (typeof value !== 'undefined') _ClienteIntCliente_begin = value; return _ClienteIntCliente_begin; }, ClienteIntCliente_end: function (value) { if (typeof value !== 'undefined') _ClienteIntCliente_end = value; return _ClienteIntCliente_end; },
        ClienteSmallIntCliente_begin: function (value) { if (typeof value !== 'undefined') _ClienteSmallIntCliente_begin = value; return _ClienteSmallIntCliente_begin; }, ClienteSmallIntCliente_end: function (value) { if (typeof value !== 'undefined') _ClienteSmallIntCliente_end = value; return _ClienteSmallIntCliente_end; },
        VendaBigIntVenda_begin: function (value) { if (typeof value !== 'undefined') _VendaBigIntVenda_begin = value; return _VendaBigIntVenda_begin; }, VendaBigIntVenda_end: function (value) { if (typeof value !== 'undefined') _VendaBigIntVenda_end = value; return _VendaBigIntVenda_end; },
        VendaDecimalVenda_begin: function (value) { if (typeof value !== 'undefined') _VendaDecimalVenda_begin = value; return _VendaDecimalVenda_begin; }, VendaDecimalVenda_end: function (value) { if (typeof value !== 'undefined') _VendaDecimalVenda_end = value; return _VendaDecimalVenda_end; },
        VendaIdCliente_begin: function (value) { if (typeof value !== 'undefined') _VendaIdCliente_begin = value; return _VendaIdCliente_begin; }, VendaIdCliente_end: function (value) { if (typeof value !== 'undefined') _VendaIdCliente_end = value; return _VendaIdCliente_end; },
        VendaIdVenda_begin: function (value) { if (typeof value !== 'undefined') _VendaIdVenda_begin = value; return _VendaIdVenda_begin; }, VendaIdVenda_end: function (value) { if (typeof value !== 'undefined') _VendaIdVenda_end = value; return _VendaIdVenda_end; },
        VendaIntVenda_begin: function (value) { if (typeof value !== 'undefined') _VendaIntVenda_begin = value; return _VendaIntVenda_begin; }, VendaIntVenda_end: function (value) { if (typeof value !== 'undefined') _VendaIntVenda_end = value; return _VendaIntVenda_end; },
        VendaSmallIntVenda_begin: function (value) { if (typeof value !== 'undefined') _VendaSmallIntVenda_begin = value; return _VendaSmallIntVenda_begin; }, VendaSmallIntVenda_end: function (value) { if (typeof value !== 'undefined') _VendaSmallIntVenda_end = value; return _VendaSmallIntVenda_end; },
        VendaItemBigIntVendaItem_begin: function (value) { if (typeof value !== 'undefined') _VendaItemBigIntVendaItem_begin = value; return _VendaItemBigIntVendaItem_begin; }, VendaItemBigIntVendaItem_end: function (value) { if (typeof value !== 'undefined') _VendaItemBigIntVendaItem_end = value; return _VendaItemBigIntVendaItem_end; },
        VendaItemDecimalVendaItem_begin: function (value) { if (typeof value !== 'undefined') _VendaItemDecimalVendaItem_begin = value; return _VendaItemDecimalVendaItem_begin; }, VendaItemDecimalVendaItem_end: function (value) { if (typeof value !== 'undefined') _VendaItemDecimalVendaItem_end = value; return _VendaItemDecimalVendaItem_end; },
        VendaItemIdVenda_begin: function (value) { if (typeof value !== 'undefined') _VendaItemIdVenda_begin = value; return _VendaItemIdVenda_begin; }, VendaItemIdVenda_end: function (value) { if (typeof value !== 'undefined') _VendaItemIdVenda_end = value; return _VendaItemIdVenda_end; },
        VendaItemIdVendaItem_begin: function (value) { if (typeof value !== 'undefined') _VendaItemIdVendaItem_begin = value; return _VendaItemIdVendaItem_begin; }, VendaItemIdVendaItem_end: function (value) { if (typeof value !== 'undefined') _VendaItemIdVendaItem_end = value; return _VendaItemIdVendaItem_end; },
        VendaItemIntVendaItem_begin: function (value) { if (typeof value !== 'undefined') _VendaItemIntVendaItem_begin = value; return _VendaItemIntVendaItem_begin; }, VendaItemIntVendaItem_end: function (value) { if (typeof value !== 'undefined') _VendaItemIntVendaItem_end = value; return _VendaItemIntVendaItem_end; },
        VendaItemSmallIntVendaItem_begin: function (value) { if (typeof value !== 'undefined') _VendaItemSmallIntVendaItem_begin = value; return _VendaItemSmallIntVendaItem_begin; }, VendaItemSmallIntVendaItem_end: function (value) { if (typeof value !== 'undefined') _VendaItemSmallIntVendaItem_end = value; return _VendaItemSmallIntVendaItem_end; },
        VendaAtacadoBigIntVendaAtacado_begin: function (value) { if (typeof value !== 'undefined') _VendaAtacadoBigIntVendaAtacado_begin = value; return _VendaAtacadoBigIntVendaAtacado_begin; }, VendaAtacadoBigIntVendaAtacado_end: function (value) { if (typeof value !== 'undefined') _VendaAtacadoBigIntVendaAtacado_end = value; return _VendaAtacadoBigIntVendaAtacado_end; },
        VendaAtacadoDecimalVendaAtacado_begin: function (value) { if (typeof value !== 'undefined') _VendaAtacadoDecimalVendaAtacado_begin = value; return _VendaAtacadoDecimalVendaAtacado_begin; }, VendaAtacadoDecimalVendaAtacado_end: function (value) { if (typeof value !== 'undefined') _VendaAtacadoDecimalVendaAtacado_end = value; return _VendaAtacadoDecimalVendaAtacado_end; },
        VendaAtacadoIdCliente_begin: function (value) { if (typeof value !== 'undefined') _VendaAtacadoIdCliente_begin = value; return _VendaAtacadoIdCliente_begin; }, VendaAtacadoIdCliente_end: function (value) { if (typeof value !== 'undefined') _VendaAtacadoIdCliente_end = value; return _VendaAtacadoIdCliente_end; },
        VendaAtacadoIdVendaAtacado_begin: function (value) { if (typeof value !== 'undefined') _VendaAtacadoIdVendaAtacado_begin = value; return _VendaAtacadoIdVendaAtacado_begin; }, VendaAtacadoIdVendaAtacado_end: function (value) { if (typeof value !== 'undefined') _VendaAtacadoIdVendaAtacado_end = value; return _VendaAtacadoIdVendaAtacado_end; },
        VendaAtacadoIntVendaAtacado_begin: function (value) { if (typeof value !== 'undefined') _VendaAtacadoIntVendaAtacado_begin = value; return _VendaAtacadoIntVendaAtacado_begin; }, VendaAtacadoIntVendaAtacado_end: function (value) { if (typeof value !== 'undefined') _VendaAtacadoIntVendaAtacado_end = value; return _VendaAtacadoIntVendaAtacado_end; },
        VendaAtacadoSmallIntVendaAtacado_begin: function (value) { if (typeof value !== 'undefined') _VendaAtacadoSmallIntVendaAtacado_begin = value; return _VendaAtacadoSmallIntVendaAtacado_begin; }, VendaAtacadoSmallIntVendaAtacado_end: function (value) { if (typeof value !== 'undefined') _VendaAtacadoSmallIntVendaAtacado_end = value; return _VendaAtacadoSmallIntVendaAtacado_end; },
        ClienteIdEstado: function (value) { if (typeof value !== 'undefined') _ClienteIdEstado = value; return _ClienteIdEstado; },
        VendaIdLoja: function (value) { if (typeof value !== 'undefined') _VendaIdLoja = value; return _VendaIdLoja; }
        };
        entitySearchRange.clear = function(){
                entitySearchRange.ClienteDatetimeCliente_typeRange('R'); entitySearchRange.ClienteDatetimeCliente_begin(null); entitySearchRange.ClienteDatetimeCliente_end(null); entitySearchRange.ClienteDatetimeCliente_predefFilter(null); entitySearchRange.ClienteDatetimeCliente_predefValue(null);
        entitySearchRange.VendaDatetimeVenda_typeRange('R'); entitySearchRange.VendaDatetimeVenda_begin(null); entitySearchRange.VendaDatetimeVenda_end(null); entitySearchRange.VendaDatetimeVenda_predefFilter(null); entitySearchRange.VendaDatetimeVenda_predefValue(null);
        entitySearchRange.VendaItemDatetimeVendaItem_typeRange('R'); entitySearchRange.VendaItemDatetimeVendaItem_begin(null); entitySearchRange.VendaItemDatetimeVendaItem_end(null); entitySearchRange.VendaItemDatetimeVendaItem_predefFilter(null); entitySearchRange.VendaItemDatetimeVendaItem_predefValue(null);
        entitySearchRange.VendaAtacadoDatetimeVendaAtacado_typeRange('R'); entitySearchRange.VendaAtacadoDatetimeVendaAtacado_begin(null); entitySearchRange.VendaAtacadoDatetimeVendaAtacado_end(null); entitySearchRange.VendaAtacadoDatetimeVendaAtacado_predefFilter(null); entitySearchRange.VendaAtacadoDatetimeVendaAtacado_predefValue(null);
        entitySearchRange.ClienteBigIntCliente_begin(null); entitySearchRange.ClienteBigIntCliente_end(null);
        entitySearchRange.ClienteDecimalCliente_begin(null); entitySearchRange.ClienteDecimalCliente_end(null);
        entitySearchRange.ClienteIdCliente_begin(null); entitySearchRange.ClienteIdCliente_end(null);
        entitySearchRange.ClienteIntCliente_begin(null); entitySearchRange.ClienteIntCliente_end(null);
        entitySearchRange.ClienteSmallIntCliente_begin(null); entitySearchRange.ClienteSmallIntCliente_end(null);
        entitySearchRange.VendaBigIntVenda_begin(null); entitySearchRange.VendaBigIntVenda_end(null);
        entitySearchRange.VendaDecimalVenda_begin(null); entitySearchRange.VendaDecimalVenda_end(null);
        entitySearchRange.VendaIdCliente_begin(null); entitySearchRange.VendaIdCliente_end(null);
        entitySearchRange.VendaIdVenda_begin(null); entitySearchRange.VendaIdVenda_end(null);
        entitySearchRange.VendaIntVenda_begin(null); entitySearchRange.VendaIntVenda_end(null);
        entitySearchRange.VendaSmallIntVenda_begin(null); entitySearchRange.VendaSmallIntVenda_end(null);
        entitySearchRange.VendaItemBigIntVendaItem_begin(null); entitySearchRange.VendaItemBigIntVendaItem_end(null);
        entitySearchRange.VendaItemDecimalVendaItem_begin(null); entitySearchRange.VendaItemDecimalVendaItem_end(null);
        entitySearchRange.VendaItemIdVenda_begin(null); entitySearchRange.VendaItemIdVenda_end(null);
        entitySearchRange.VendaItemIdVendaItem_begin(null); entitySearchRange.VendaItemIdVendaItem_end(null);
        entitySearchRange.VendaItemIntVendaItem_begin(null); entitySearchRange.VendaItemIntVendaItem_end(null);
        entitySearchRange.VendaItemSmallIntVendaItem_begin(null); entitySearchRange.VendaItemSmallIntVendaItem_end(null);
        entitySearchRange.VendaAtacadoBigIntVendaAtacado_begin(null); entitySearchRange.VendaAtacadoBigIntVendaAtacado_end(null);
        entitySearchRange.VendaAtacadoDecimalVendaAtacado_begin(null); entitySearchRange.VendaAtacadoDecimalVendaAtacado_end(null);
        entitySearchRange.VendaAtacadoIdCliente_begin(null); entitySearchRange.VendaAtacadoIdCliente_end(null);
        entitySearchRange.VendaAtacadoIdVendaAtacado_begin(null); entitySearchRange.VendaAtacadoIdVendaAtacado_end(null);
        entitySearchRange.VendaAtacadoIntVendaAtacado_begin(null); entitySearchRange.VendaAtacadoIntVendaAtacado_end(null);
        entitySearchRange.VendaAtacadoSmallIntVendaAtacado_begin(null); entitySearchRange.VendaAtacadoSmallIntVendaAtacado_end(null);
        entitySearchRange.ClienteIdEstado(null);
        entitySearchRange.VendaIdLoja(null);
        };
        entitySearchRange.has_ClienteBigIntCliente = function(){ return (entitySearchRange.ClienteBigIntCliente_begin() != null || entitySearchRange.ClienteBigIntCliente_end() != null); };
        entitySearchRange.has_ClienteDecimalCliente = function(){ return (entitySearchRange.ClienteDecimalCliente_begin() != null || entitySearchRange.ClienteDecimalCliente_end() != null); };
        entitySearchRange.has_ClienteIdCliente = function(){ return (entitySearchRange.ClienteIdCliente_begin() != null || entitySearchRange.ClienteIdCliente_end() != null); };
        entitySearchRange.has_ClienteIntCliente = function(){ return (entitySearchRange.ClienteIntCliente_begin() != null || entitySearchRange.ClienteIntCliente_end() != null); };
        entitySearchRange.has_ClienteSmallIntCliente = function(){ return (entitySearchRange.ClienteSmallIntCliente_begin() != null || entitySearchRange.ClienteSmallIntCliente_end() != null); };
        entitySearchRange.has_VendaBigIntVenda = function(){ return (entitySearchRange.VendaBigIntVenda_begin() != null || entitySearchRange.VendaBigIntVenda_end() != null); };
        entitySearchRange.has_VendaDecimalVenda = function(){ return (entitySearchRange.VendaDecimalVenda_begin() != null || entitySearchRange.VendaDecimalVenda_end() != null); };
        entitySearchRange.has_VendaIdCliente = function(){ return (entitySearchRange.VendaIdCliente_begin() != null || entitySearchRange.VendaIdCliente_end() != null); };
        entitySearchRange.has_VendaIdVenda = function(){ return (entitySearchRange.VendaIdVenda_begin() != null || entitySearchRange.VendaIdVenda_end() != null); };
        entitySearchRange.has_VendaIntVenda = function(){ return (entitySearchRange.VendaIntVenda_begin() != null || entitySearchRange.VendaIntVenda_end() != null); };
        entitySearchRange.has_VendaSmallIntVenda = function(){ return (entitySearchRange.VendaSmallIntVenda_begin() != null || entitySearchRange.VendaSmallIntVenda_end() != null); };
        entitySearchRange.has_VendaItemBigIntVendaItem = function(){ return (entitySearchRange.VendaItemBigIntVendaItem_begin() != null || entitySearchRange.VendaItemBigIntVendaItem_end() != null); };
        entitySearchRange.has_VendaItemDecimalVendaItem = function(){ return (entitySearchRange.VendaItemDecimalVendaItem_begin() != null || entitySearchRange.VendaItemDecimalVendaItem_end() != null); };
        entitySearchRange.has_VendaItemIdVenda = function(){ return (entitySearchRange.VendaItemIdVenda_begin() != null || entitySearchRange.VendaItemIdVenda_end() != null); };
        entitySearchRange.has_VendaItemIdVendaItem = function(){ return (entitySearchRange.VendaItemIdVendaItem_begin() != null || entitySearchRange.VendaItemIdVendaItem_end() != null); };
        entitySearchRange.has_VendaItemIntVendaItem = function(){ return (entitySearchRange.VendaItemIntVendaItem_begin() != null || entitySearchRange.VendaItemIntVendaItem_end() != null); };
        entitySearchRange.has_VendaItemSmallIntVendaItem = function(){ return (entitySearchRange.VendaItemSmallIntVendaItem_begin() != null || entitySearchRange.VendaItemSmallIntVendaItem_end() != null); };
        entitySearchRange.has_VendaAtacadoBigIntVendaAtacado = function(){ return (entitySearchRange.VendaAtacadoBigIntVendaAtacado_begin() != null || entitySearchRange.VendaAtacadoBigIntVendaAtacado_end() != null); };
        entitySearchRange.has_VendaAtacadoDecimalVendaAtacado = function(){ return (entitySearchRange.VendaAtacadoDecimalVendaAtacado_begin() != null || entitySearchRange.VendaAtacadoDecimalVendaAtacado_end() != null); };
        entitySearchRange.has_VendaAtacadoIdCliente = function(){ return (entitySearchRange.VendaAtacadoIdCliente_begin() != null || entitySearchRange.VendaAtacadoIdCliente_end() != null); };
        entitySearchRange.has_VendaAtacadoIdVendaAtacado = function(){ return (entitySearchRange.VendaAtacadoIdVendaAtacado_begin() != null || entitySearchRange.VendaAtacadoIdVendaAtacado_end() != null); };
        entitySearchRange.has_VendaAtacadoIntVendaAtacado = function(){ return (entitySearchRange.VendaAtacadoIntVendaAtacado_begin() != null || entitySearchRange.VendaAtacadoIntVendaAtacado_end() != null); };
        entitySearchRange.has_VendaAtacadoSmallIntVendaAtacado = function(){ return (entitySearchRange.VendaAtacadoSmallIntVendaAtacado_begin() != null || entitySearchRange.VendaAtacadoSmallIntVendaAtacado_end() != null); };
        entitySearchRange.has_ClienteDatetimeCliente = function(){ return (entitySearchRange.ClienteDatetimeCliente_typeRange() == 'R' && (entitySearchRange.ClienteDatetimeCliente_begin() != null || entitySearchRange.ClienteDatetimeCliente_end() != null) || (entitySearchRange.ClienteDatetimeCliente_typeRange() == 'P' && !common.isNullOrEmpty(entitySearchRange.ClienteDatetimeCliente_predefFilter()))); };
        entitySearchRange.has_VendaDatetimeVenda = function(){ return (entitySearchRange.VendaDatetimeVenda_typeRange() == 'R' && (entitySearchRange.VendaDatetimeVenda_begin() != null || entitySearchRange.VendaDatetimeVenda_end() != null) || (entitySearchRange.VendaDatetimeVenda_typeRange() == 'P' && !common.isNullOrEmpty(entitySearchRange.VendaDatetimeVenda_predefFilter()))); };
        entitySearchRange.has_VendaItemDatetimeVendaItem = function(){ return (entitySearchRange.VendaItemDatetimeVendaItem_typeRange() == 'R' && (entitySearchRange.VendaItemDatetimeVendaItem_begin() != null || entitySearchRange.VendaItemDatetimeVendaItem_end() != null) || (entitySearchRange.VendaItemDatetimeVendaItem_typeRange() == 'P' && !common.isNullOrEmpty(entitySearchRange.VendaItemDatetimeVendaItem_predefFilter()))); };
        entitySearchRange.has_VendaAtacadoDatetimeVendaAtacado = function(){ return (entitySearchRange.VendaAtacadoDatetimeVendaAtacado_typeRange() == 'R' && (entitySearchRange.VendaAtacadoDatetimeVendaAtacado_begin() != null || entitySearchRange.VendaAtacadoDatetimeVendaAtacado_end() != null) || (entitySearchRange.VendaAtacadoDatetimeVendaAtacado_typeRange() == 'P' && !common.isNullOrEmpty(entitySearchRange.VendaAtacadoDatetimeVendaAtacado_predefFilter()))); };
        
        function openEditor(element, cName, cDataItem_listItem, dataV_parentName, entityName) {
           return false;
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
                lastSearchFilter: lastSearchFilter
            };
        
        var dataBusiness = {
                dataView: dataView,
                viewName: 'ClientLocalService1',
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
                enabledForEditing: enabledForEditing,
                dataToolbar: dataToolbar,
                getDataContext: function() { return dataContext; },
                getExtendedDataBusiness: function() { return extendedDataBusiness; },
                getParentSelectorDataName: getParentSelectorDataName,
                getMaxLength: getMaxLength,
                status: status,
                removeParentRelatedItems: removeParentRelatedItems,
                onSavingValidation: onSavingValidation,
                goToKey: goToKey,
                dataBind: dataBind,
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
                authService: dataContext.authService,
                rootBmTypeName: 'CLIENTE',
                rootDataTypeName: 'Cliente',
                rootNamespace: 'Linx.Demo.BV.PaiFilha',
                setSecurity: setSecurity,
                isReportComposition: isReportComposition,
                refreshToolbar: refreshToolbar,
                refreshCurrentBind: refreshCurrentBind,
                createEntity: createEntity,
                getServiceAddress: dataContext.getServiceAddress,
                getAccessGroup: dataContext.getAccessGroup,
                httpGet: dataContext.httpGet,
                httpPost: dataContext.httpPost,
                getBandeiraRede: getBandeiraRede,
                getCurrentBrands: getCurrentBrands,
                setBandeiraRede: setBandeiraRede,
                entitySearchRange: entitySearchRange,
                currentActivityInformation: currentActivityInformation,
                showProcessing: showProcessing,
                closeProcessing: closeProcessing,
                isDependentVM: isDependentVM,
                getInnerJExpression: getInnerJExpression,
                allowMultiSelectionInSearch: allowMultiSelectionInSearch,
                transactionNumberControl: transactionNumberControl,
                createCliente: createCliente,
                createAndNotifyCliente: createAndNotifyCliente,
                createVenda: createVenda,
                createAndNotifyVenda: createAndNotifyVenda,
                createVendaItem: createVendaItem,
                createAndNotifyVendaItem: createAndNotifyVendaItem,
                createVendaAtacado: createVendaAtacado,
                createAndNotifyVendaAtacado: createAndNotifyVendaAtacado,
                deleteEntity: deleteEntity,
                currentBrands: '', 
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
                __moduleId__: 'pkg_linx-demo-bv/controllers/ClientLocalService1'
        };
        
        dataContext.setCurrentDataBusiness(dataBusiness);
        extendedDataBusiness.setCurrentDataBusiness(dataBusiness);
        initService();
        
        return dataBusiness;
    };
    
    module.factory(name, dependencies.concat(dataBusinessFactory));
});
