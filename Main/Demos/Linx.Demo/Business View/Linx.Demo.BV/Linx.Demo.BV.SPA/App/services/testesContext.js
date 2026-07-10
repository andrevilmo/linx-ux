define(['durandal/system', 'pkg_linx-demo-bv-spa/services/DataDomains', 'services/logger', 'breeze', 'durandal/app', 'managers/__auth', 'viewmodels/shared/modal', 'viewmodels/shared/modal2'],
function (system, dataDomains, logger, breeze, app, managerAuth, modal, modal2) {
var result = function () {
    var getPivotLayouts = function (params, success, error) {
        return $.ajax({
            messageUser: 'Busca dos layouts exportados',
            contentType: 'application/json; charset=UTF-8',
            headers: managerAuth.getHeaders(),
            url: getServiceAddress('linxframeworkobjeto') + '/GetPivotLayouts?' +
                                                                   'rootNameSpace=' + params.rootNamespace +
                                                                   '&viewName=' + params.viewName +
                                                                   '&pivotName=' + params.pivotName +
                                                                   '&pivotDataSource=' + params.pivotDataSource,
            async: true,
            cache: false,
            error: error,
            success: success
        });
    };
    
    var getSelectedLayoutContent = function (params, success, error) {
        return $.ajax({
            messageUser: 'Busca de layout selecionado',
            contentType: 'application/json; charset=UTF-8',
            headers: managerAuth.getHeaders(),
            url: getServiceAddress('linxframeworkobjeto') + '/GetPivotLayout?uidObjetoConteudo=' + params.uidObjetoConteudo,
            async: true,
            cache: false,
            error: error,
            success: success
        });
    };
    var deleteLayoutSelected = function(params, success, error) {
        return $.ajax({
            type: 'DELETE',
            messageUser: 'Deletando layout selecionado',
            contentType: 'application/json; charset=UTF-8',
            headers: managerAuth.getHeaders(),
            url: getServiceAddress('linxframeworkobjeto') + '/DeleteLayoutPivot?IdLayout=' + params.idLayout + '&uidUsuario=' + params.idUser,
            async: true,
            cache: false,
            error: error,
            success: success
        });
    };
    var getServiceAddress = function(apiPart) {
       return managerAuth.getServiceAddress(apiPart, businessAssemblyName);
    };
    var getBaseServiceAddress = function(apiPart) {
       return managerAuth.getBaseServiceAddress(apiPart, businessAssemblyName);
    };
    var getAccessGroup = function() {
       return '00000000-0000-0000-0000-000000000000';
    };
    var getNewGuid = function() {
       return breeze.core.getUuid();
    };
    var getDataFeedUrl = function() {
       return getServiceAddress('LinxDemotestesOData');
    };
    var getDataServiceUrl = function () {
       return getServiceAddress(controllerName);
    };
    var setServiceBusUrl = function (url) {
       if (dataService) { dataService.serviceName = (isNullOrEmpty(url) ? getDataServiceUrl() : url + controllerName); }
    };
    var initializePOCO = function(ownerReference, entityName) {
       if (ownerReference && !ownerReference.RowDataId) { eval(entityName + 'Initializer(ownerReference, true);'); }
    };
    var businessAssemblyName = 'Linx.Demo.BV';
    var controllerName = 'LinxDemotestes';
    var dataService = new breeze.DataService({
        serviceName: getDataServiceUrl(),
        hasServerMetadata: false // don't ask the server for metadata
    });
    var manager = new breeze.EntityManager({ dataService: dataService });
    manager.entityChanged.subscribe(function(changeArgs) {
        if (changeArgs.entityAction === breeze.EntityAction.PropertyChange) {
            if ((typeof changeArgs.args.newValue) === 'number' && changeArgs.args.oldValue < 0 && changeArgs.args.newValue > 0 && changeArgs.entity.isPrimaryKey(changeArgs.args.propertyName)) vm.replaceInnerUIsKeys(changeArgs.entity, changeArgs.args.propertyName, changeArgs.args.oldValue, changeArgs.args.newValue);
            if (typeof changeArgs.entity.OnPropertyChanged == 'function')
                changeArgs.entity.OnPropertyChanged(changeArgs.args.propertyName, changeArgs.args.oldValue, changeArgs.args.newValue);
        }
    });
    var enableChangeTrack = true;
    var entityPropChanged = function(entity, propName, oldVal, newVal) {
        if (!enableChangeTrack) return true;
        var result = true;
        if ((typeof entity.OnPropertyChanged == 'function') && oldVal !== newVal)
            result = (entity.OnPropertyChanged(propName, oldVal, newVal) !== false);
        if (result && ['U', 'I', 'D'].indexOf(entity.ChangeState) < 0) { entity.createOriginal(propName, oldVal); entity.ChangeState = 'U'; if (entity.setParentAsModified) entity.setParentAsModified(); }
        if (result && (typeof newVal) === 'number' && oldVal < 0 && newVal > 0 && entity.isPrimaryKey(propName)) vm.replaceInnerUIsKeys(entity, propName, oldVal, newVal);
        return result;
    }
    var metadataStore = manager.metadataStore;
    var EntityQuery = breeze.EntityQuery;
    // Extract Breeze metadata definition types
    var DataType = breeze.DataType;
    var AutoGeneratedKeyType = breeze.AutoGeneratedKeyType;
    var Validator = breeze.Validator;
    Validator.hasValueValidator = new breeze.Validator('hasValueValidator', hasValueValidationFn, { messageTemplate: "'%displayName%' é requerido" });
    //#region Metadata Info
    var metadataInfo = [];
    var dataExportInfo = [];
    var entityNames = [];
    var lookUpNames = [];
    var entitylookUps = [];
    entityNames.push('Produto');
    metadataInfo['Produto'] = [
        { key: 'DataCadastro', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Data Cadastro', width: '205px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null },
        { key: 'DescProduto', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 100, validateMaxLength: true, isPartOfKey: false, headerText: 'Desc Produto', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null },
        { key: 'IdProduto', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 10, isPartOfKey: true, headerText: 'Id Produto', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'NmProduto', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'Nm Produto', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null },
        { key: 'TipoUnidade', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 3, isPartOfKey: false, headerText: 'Tipo Unidade', width: '192px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null }
    ];
    dataExportInfo['Produto'] = [ 
        { name: 'Produto', canExportMedia: true , canExportReport: true, actionExport: 'LinxDemotestes/GetProdutoToExcel', actionReport: 'LinxDemotestes/GetProdutoToReportXml', actionFeed: 'LinxDemotestesOData/Produto', actionName: 'LinxDemotestes/GetProdutoByEntitySearchNoAssociations', display: 'Produto',  metaData: function() { return metadataInfo['Produto']; } }
    ];
    entitylookUps.push('Produto');
    entitylookUps['Produto'] = [];
    var lookUpProperties = [];
    //#endregion Metadata Info
    //#region dataParameters
    var dataParameters = {
        isLoaded: false,
        parameters: [],
        registerParameters: function (parameterList, complete) {
            if (parameterList !== '') {
                var variation = '{TBC_GRUPO_ECONOMICO|' + managerAuth.loginInfo.IdLinxGrupoEconomico.toString() + '|TCS_USUARIO|' + managerAuth.loginInfo.UidUsuario + (vm != null && vm.getBandeiraRede() > 0 ? '|TBC_BANDEIRA_REDE|' + vm.getBandeiraRede().toString() : '') + '}';
                $.ajax({
                    type: 'GET',
                    url: getServiceAddress('LinxFrameworkParametro') + '/GetParameterValue?serializedParameterList=' + stringReplace(parameterList, '{}', variation),
                    dataType: 'json',
                    cache: false,
                    headers: managerAuth.getHeaders(),
                    error: function (jqXHR, textStatus, errorThrown) {
                        var msg = 'Os seguintes parâmetros não foram pesquisados: [' + parameterList + ']';
                        app.showMessage(msg, 'Alerta', ['Ok']);
                        dataParameters.isLoaded = true;
                    },
                    success: function (data) {
                        var parametersName = '';
                        var parameters = data.split('#');
                        for (var idx in parameters) {
                            var values = parameters[idx].split('|');
                            var pName = values[0];
                            var pValue = values[1];
                            dataParameters.parameters[pName] = pValue;
                        }
                        dataParameters.isLoaded = true;
                        if (complete) complete();
                    }
                });
            }
        }
    };
    //#endregion dataParameters
    //#region Classes Map
    var sequences = [];
    var getNextSequence = function(entityName) {
        if (!sequences[entityName]) resetSequence(entityName);
        sequence = sequences[entityName];
        sequences[entityName]++;
        return sequence;
    };
    var resetSequence = function(entityName) {
        sequences[entityName] = 0;
    };
    var getSequence = function(entityName) {
        if (!sequences[entityName]) resetSequence(entityName);
        return sequences[entityName];
    };
    
    // Configure Produto data type
    metadataStore.addEntityType({
    shortName: "Produto",
    namespace: "Linx.Demo.BV.testes",
    autoGeneratedKeyType: AutoGeneratedKeyType.Identity,
    dataProperties: {
    DataCadastro: { dataType: DataType.DateTime, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,DescProduto: { dataType: DataType.String, maxLength: 100, isNullable: true, isPartOfKey: false, validators: [ Validator.maxLength( {maxLength: 100})]  }
    ,IdProduto: { dataType: DataType.Int32, isNullable: false, isPartOfKey: true, validators: [ Validator.hasValueValidator]  }
    ,NmProduto: { dataType: DataType.String, maxLength: 50, isNullable: true, isPartOfKey: false, validators: [ Validator.maxLength( {maxLength: 50})]  }
    ,TipoUnidade: { dataType: DataType.Byte, isNullable: false, isPartOfKey: false, validators: [ ]  }
                    },
    navigationProperties: {
    // Returns collections of details and associates with Parent
                          }
    });
    lookUpProperties['Produto'] = {};
    var ProdutoInitializer = function (ownerReference, isPOCO) {
       ownerReference.RowDataId = (isPOCO === true ? getNextSequence('Produto') : ko.observable(getNextSequence('Produto')));
       ownerReference.setRemovedLookupFields = function(removedFields) {
           for (var idxLUp in entitylookUps[ownerReference.typeName]) {
               var hasKeyValue = false;
               var luName = entitylookUps[ownerReference.typeName][idxLUp];
               var luMeta = metadataInfo[luName];
               for (var idxProp in luMeta) {
                   var prop = luMeta[idxProp];
                   if (!isNullOrEmpty(prop.relatedKey) && prop.isPartOfKey) {
                       hasKeyValue = !isNullOrEmpty(getAbsoluteValue(ownerReference[prop.relatedKey]));
                       break;
                   }
               }
               if (hasKeyValue) {
                   for (var idxProp in luMeta) {
                       var prop = luMeta[idxProp];
                       if (!isNullOrEmpty(prop.relatedKey) && !prop.isPartOfKey) {
                           removedFields.push(prop.relatedKey);
                       }
                   }
               }
           }
       }
       ownerReference.getJExpression = function(listFilterRange, removedFields, noDetails) {
           if (ownerReference.excludedFilters && ownerReference.excludedFilters.length > 0) { if (removedFields instanceof Array) removedFields = removedFields.concat(ownerReference.excludedFilters); else removedFields = ownerReference.excludedFilters; }
           ownerReference.setRemovedLookupFields(removedFields);
           var jExpression = getJEntityExpression(ownerReference, app, listFilterRange, removedFields, vm.useLikeCommandAsDefault, ownerReference.getQbeZeroFields());
           if (jExpression === 'Error') return jExpression;
           return jExpression;
      };
       ownerReference.createOriginal = function(propertyName, oldValue) {
           ownerReference.original = ownerReference.getPrimitiveDTO();
           if (propertyName) ownerReference.original[propertyName] = oldValue;
       }
       ownerReference.restoreOriginal = function() {
           if (!isNullOrEmpty(ownerReference.original)) {
              enableChangeTrack = false;
              var properties = metadataInfo[ownerReference.typeName];
              for (var i = 0; i < properties.length; i++) {
                  var propertyName = properties[i].key;
                  if ((typeof ownerReference.original[propertyName]) !== 'undefined') ownerReference[propertyName] = ownerReference.original[propertyName];
              }
              delete ownerReference.original;
              enableChangeTrack = true;
           } else if(ownerReference.ChangeState === 'D') ownerReference.ChangeState = 'U';
       }
       if (isPOCO === true) {
           ownerReference.getValidationErrors = function(propertyName) {
               var errors = [];
               if (!vm.canReportErrors) return errors;
               if (!ownerReference.ChangeState || ['I', 'U'].indexOf(ownerReference.ChangeState) < 0) return errors;
               var properties = metadataInfo[ownerReference.typeName];
               for (var i = 0; i < properties.length; i++) {
                   var prop = properties[i];
                   if (isNullOrEmpty(propertyName) || prop.key == propertyName) {
                       if (prop.isRequired === true && !prop.isPartOfKey && isNullOrEmpty(ownerReference[prop.key]) && !(prop.isQbeZero === true && ownerReference[prop.key] == 0)) errors.push('O campo [' + prop.headerText + (managerAuth.shellMode=='DEV' ? ' (' + ownerReference.typeName + '.' + prop.key + ')' : '') + '] é requerido.');
                       if (prop.validateMaxLength === true && prop.maxLength > 0 && !isNullOrEmpty(ownerReference[prop.key]) && ownerReference[prop.key].length > prop.maxLength) errors.push('O campo [' + prop.headerText + (managerAuth.shellMode=='DEV' ? ' (' + ownerReference.typeName + '.' + prop.key + ')' : '') + '] permite no máximo ' + prop.maxLength.toString() + ' caractere(s).');
                   }
               }
               return errors;
           }
       }
       ownerReference.getQbeZeroFields = function() {
           var result = [];
           var properties = metadataInfo[ownerReference.typeName];
           for (var i = 0; i < properties.length; i++) {
               if (properties[i].isQbeZero) {
                   result.push(properties[i].key);
               }
           }
           return result;
       }
       ownerReference.getPrimitiveDTO = function(loadDetails) {
           var command = '';
           var properties = metadataInfo[ownerReference.typeName];
           for (var i = 0; i < properties.length; i++) {
               command += (command === '' ? '' : ', ') + properties[i].key + ': getAbsoluteValue(ownerReference.' + properties[i].key + ')';
               if (properties[i].isDomain && properties[i].key.length > 4) command += (command === '' ? '' : ', ') + strLeft(properties[i].key, properties[i].key.length - 4) + ': getAbsoluteValue(ownerReference.' + strLeft(properties[i].key, properties[i].key.length - 4) + ')';
           }
           eval('var result = { ' + command + ' };');
           return result;
       };
       ownerReference.getAllDetailChanges = function() {
           var result = [];
           return result;
       };
       ownerReference.copyDataFrom = function(originData, copyDetails) {
           enableChangeTrack = false;
           var properties = metadataInfo[ownerReference.typeName];
           for (var i = 0; i < properties.length; i++) {
                setAbsoluteValue(ownerReference, properties[i].key, getAbsoluteValue(originData[properties[i].key]));
           }
       enableChangeTrack = true;
       };
          ownerReference.commitDetailsVisualPendings = function() {
          }
          ownerReference.refreshData = function(noWait, succeeded) {
             var filterByKey = 'Produto{' + 'IdProduto#==#I' + getAbsoluteValue(ownerReference.IdProduto).toString() + '}';
             if (!ownerReference.isPOCO && ownerReference.entityAspect && !ownerReference.isDetached() && !ownerReference.isUnchanged()) ownerReference.entityAspect.setUnchanged();
             return dataContext.getProdutoByEntitySearchNoAssociations(filterByKey, 0, 0, false, true, ownerReference.isPOCO === true).then(querySucceeded);
             function querySucceeded(data) {
                if (ownerReference.isPOCO && data.results.length > 0) {  for (var idx = 0; idx < data.results.length; idx++) { ownerReference.copyDataFrom(data.results[idx]); } }
                if (succeeded) { succeeded(data); }
                if (data.results.length == 0) { return; }
                if (!noWait || ownerReference.atLeastOneDetailLoaded()) { vm.clearInnerUIs(ownerReference); ownerReference.fillDetails(true, '', false, noWait); }
           }
          }
       if (isPOCO === true) {
           ownerReference.isPOCO = true;
           ownerReference.enableDetailsDataTack = function(breezeReference) {
              if (breezeReference) breezeReference.setCurrentDetails();
           };
       }
       ownerReference.isAdded = (isPOCO === true ? function() { return false; } : function() {
           return ownerReference.entityAspect.entityState === breeze.EntityState.Added;
       });
       ownerReference.isDeleted = (isPOCO === true ? function() { return false; } : function() {
           return ownerReference.entityAspect.entityState === breeze.EntityState.Deleted;
       });
       ownerReference.isModified = (isPOCO === true ? function() { return false; } : function() {
           return ownerReference.entityAspect.entityState === breeze.EntityState.Modified;
       });
       ownerReference.isDetached = (isPOCO === true ? function() { return false; } : function() {
           return ownerReference.entityAspect.entityState === breeze.EntityState.Detached;
       });
       ownerReference.isUnchanged = (isPOCO === true ? function() { return true; } : function() {
           return ownerReference.entityAspect.entityState === breeze.EntityState.Unchanged;
       });
       ownerReference.setModified = (isPOCO === true ? function() {  } : function() {
           ownerReference.entityAspect.setModified();
       });
       ownerReference.setUnchanged = (isPOCO === true ? function() {  } : function() {
           ownerReference.entityAspect.setUnchanged();
       });
       ownerReference.serverDataType = [];
       ownerReference.serverDataType['DataCadastro'] = 'T';
       ownerReference.serverDataType['DescProduto'] = 'S';
       ownerReference.serverDataType['IdProduto'] = 'I';
       ownerReference.serverDataType['NmProduto'] = 'S';
       ownerReference.serverDataType['TipoUnidade'] = 'Y';
       ownerReference.typeName = 'Produto';
       ownerReference.isPrimaryKey = function(propertyName) {
           var keys = [ 'IdProduto' ];
           return keys.indexOf(propertyName) >= 0;
       }
       ownerReference.getDisplayName = function(propertyName) {
          var property = getEntityProperty(ownerReference.typeName, propertyName);
          return (property != null ? property.headerText : propertyName);
       }
       ownerReference.setDisplayName = function(propertyName, displayName) {
          var property = getEntityProperty(ownerReference.typeName, propertyName);
          if (property != null) property.headerText = displayName;
       }
       ownerReference.setBandeiraRede = function (idBandeiraRede) {
       };
       ownerReference.setGpecon = function (idGpecon) {
       };
       ownerReference.UpdateIndependentRelation = function(detailName) {
           var cacheElements = dataContext.getEntities(detailName);
           for (var idxR = 0; idxR < cacheElements.length; idxR++) {
               if (typeof cacheElements[idxR].Produto !== 'function') { return; }
               else  if (cacheElements[idxR].Produto() != ownerReference) { cacheElements[idxR].Produto(ownerReference); }
           }
       }
       ownerReference.setDefaults = function () {
            //Adjust default value for QBE Zero Properties
            var qbeZeroProperties = ownerReference.getQbeZeroFields();
            for (var i = 0; i < qbeZeroProperties.length; i++) {
                   setAbsoluteValue(ownerReference, qbeZeroProperties[i], 0);
            }
       };
       ownerReference.delete = function() {
           if (ownerReference.isDetached()) {
               app.showMessage('A informação selecionada não pode ser excluída!', 'Alerta', ['Ok']);
               return;
           }
           if (ownerReference.setParentAsModified) ownerReference.setParentAsModified();
           if (ownerReference.entityAspect) ownerReference.entityAspect.setDeleted(); // mark for deletion
       };
       ownerReference.setParentAsModified = function() {
       };
       ownerReference.getParent = function() {
           return null;
       };
       ownerReference.getSelfList = function() {
           return vm.dataView();
       };
       ownerReference.namespace = 'Linx.Demo.BV.testes';
       ownerReference.myProperties = [ 'DataCadastro','DescProduto','IdProduto','NmProduto','TipoUnidade' ];
       ownerReference.queryRequiredProperties = {  };
       ownerReference.excludedFilters = [];
       ownerReference.getCurrentElements = function() {
           var result = [ ownerReference ];
           return result;
       };
       ownerReference.checkForSendingAllRowsToServer = function() {
       };
       ownerReference.detailsLoaded = function() {
           return true;
       }
       ownerReference.atLeastOneDetailLoaded = function() {
           return true;
       }
       ownerReference.adjustDetailsLoaded = function(value) {
       }
       ownerReference.fillDetails = function(force, detailName, noInnerUIs, noWait, callback, customParentRelation) {
          if (typeof force === 'undefined') force = false;
          if (force) vm.clearInnerUIs(ownerReference);
          if (!noInnerUIs) vm.queryInnerUIs(ownerReference);
          if (callback) { callback(); }
       };
       //Select first element as a current item of each detail
       ownerReference.setCurrentDetails = function(detailName, clearing) {
       };
       ownerReference.notifyEmptyDetails = function(detailName) {
       };
    };
    metadataStore.registerEntityTypeCtor("Produto", null, ProdutoInitializer);
    //#endregion Classes Map
    //#region Context Definition
    
    //#region Get LookUps
    //#endregion
    //#region Get KPI Ranges
    //#endregion
    
    //#region Get Combo LookUp
    var getResultsCombo = function (lookupName, fieldName, current, callback) {
        if (typeof current.executeLookUp === 'function') {
           current.executeLookUp(lookupName, fieldName, null, vm, null, null, function (result) {
               if (callback) callback(result);
           });
        }
    };
    var clientFilterHasModified = function clientFilterHasModified(lookupName, current) {
        var lastFilter = dataContext.lastClientFilterExpressions[lookupName];
        if (lastFilter === 'Error') return true;
        var currentFilter = current.getLookUpClientFilterExpressions(lookupName, null);
        return lastFilter != currentFilter;
    };
    //#endregion Get Combo LookUp
    
    //#region Get Business Entities
    
    var getBmEntityProperties = function (entityName, parentDataPath) {
        return manager.executeQuery(EntityQuery.from('GetBmEntityProperties').withParameters({ entityName: entityName, parentDataPath: parentDataPath }).noTracking(true))
        .fail(queryFailed);
    };
    
    var clearProduto = function (idBandeiraRede, complete) {
        clearAll();
        resetSequence('Produto');
        var refProduto = manager.createEntity('Produto', {}, breeze.EntityState.Unchanged);
        if (complete) complete({ results: [ refProduto ] });
        return true;
    };
    
    var getProduto = function (predicate, preserveCurrentState, noTracking) {
        if (!preserveCurrentState) clearAll();
        var query = EntityQuery.from('GetProduto').noTracking(noTracking)
        .orderBy('IdProduto asc')
        ;
    
        if ((typeof predicate !== 'undefined') && predicate !== null)
            query = query.where(predicate);
    
        return manager.executeQuery(query)
        .fail(queryFailed);
    };
    
    var getProdutoByEntitySearchNoAssociations = function (jEntitySearch, skip, take, returnInlineCount, preserveCurrentState, noTracking, orderByDef) {
        if (!preserveCurrentState) clearAll();
        var query = EntityQuery.from('GetProdutoByEntitySearchNoAssociations').noTracking(noTracking)
        .orderBy((isNullOrEmpty(orderByDef) ? 'IdProduto asc' : orderByDef))
        ;
    
        if ((typeof jEntitySearch !== 'undefined') && jEntitySearch !== null)
            query = query.withParameters({ jEntitySearch: jEntitySearch });
        if (take > 0)
           query = query.skip(skip).take(take);
        if (returnInlineCount)
            query = query.inlineCount(true);
    
        return manager.executeQuery(query)
        .fail(queryFailed);
    };
    //#endregion
    
    // Create the data update event.
    var dataUpdateEvent = document.createEvent('Event');
    // Define that the event name is 'testesContext_DataUpdate'.
    var contextUpdtEvt = 'testesContext_DataUpdate_' + getNewGuid();
    dataUpdateEvent.initEvent(contextUpdtEvt, true, true);
    
    //#region LookUps Finalizers
    //#endregion
    
    var cancelChanges = function(dataForUndo) {
        if (dataForUndo && dataForUndo.length > 0) {
            dataForUndo.forEach(function(e) { e.restoreOriginal(); } ); 
        } else {
            manager.rejectChanges();
        }
    };
    
    var hasEmptyRequiredFilters = function() {
        return false;
    };
    
    var getEntityProperty = function (entityName, propertyName) {
        for (var i = 0; i < metadataInfo[entityName].length; i++) {
            if (metadataInfo[entityName][i].key === propertyName)
                return metadataInfo[entityName][i];
        }
        return null;
    };
    var getViewInfo = function (entityName) {
        var result = [];
        if (metadataInfo[entityName])
        {
            var viewInfoElements = metadataInfo[entityName];
            for (var i = 0; i < viewInfoElements.length; i++) {
                var selectedElement = viewInfoElements[i];
                if (!selectedElement.hidden && (selectedElement.dataType === 'string' || selectedElement.dataType === 'number'))
                {
                    if (strLeft(selectedElement.key, 3) === 'Cod' || strLeft(selectedElement.key, 2) === 'Id' || strLeft(selectedElement.key, 6) === 'Numero' || strLeft(selectedElement.key, 6) === 'Number') {
                        result.push(selectedElement);
                    }
                }
            }
            for (var i = 0; i < viewInfoElements.length; i++) {
                var selectedElement = viewInfoElements[i];
                if (!selectedElement.hidden && (selectedElement.dataType === 'string')) {
                    if (strLeft(selectedElement.key, 4) === 'Nome' || strLeft(selectedElement.key, 4) === 'Name' || strLeft(selectedElement.key, 4) === 'Desc' || strLeft(selectedElement.key, 6) === 'Titulo' || strLeft(selectedElement.key, 5) === 'Title') {
                        result.push(selectedElement);
                    }
                }
            }
            for (var i = 0; i < viewInfoElements.length; i++) {
                var selectedElement = viewInfoElements[i];
                if (!selectedElement.hidden && selectedElement.dataType === 'date')
                {                
                   result.push(selectedElement);
                }
            }
        }
        return result;
    };
    
    var getChanges = function() {
       return manager.getEntities(null, [breeze.EntityState.Added, breeze.EntityState.Modified, breeze.EntityState.Deleted]);
    };
    
    var hasValidationErrors = function(savingData) {
        if (savingData instanceof Array) {
           for (var idx = 0; idx < savingData.length; idx++) {
               var entity = savingData[idx];
               if (entity.ChangeState && entity.getValidationErrors && ['I', 'U'].indexOf(entity.ChangeState) >=0) {
                  var errors = entity.getValidationErrors();
                  if (errors.length > 0) {
                       showModalAlert('Campos obrigatórios não estão preenchidos.', errors);
                       return true;
                   }
               }
           }
        }
        else {
           var changes = manager.getEntities(null, [breeze.EntityState.Added, breeze.EntityState.Modified]);
           for (var idxChange = 0; idxChange < changes.length; idxChange++) {
              changes[idxChange].setParentAsModified();
           }
           changes = manager.getEntities(null, [breeze.EntityState.Added, breeze.EntityState.Modified]);
           for (var idxChange = 0; idxChange < changes.length; idxChange++) {
              var entity = changes[idxChange];
              var isOk = entity.entityAspect.validateEntity();
              if (!isOk) {
                  var errors = entity.entityAspect.getValidationErrors();
                  var strErrors = [];
                  for (var idx = 0; idx < errors.length; idx++) {
                      var errorMsg = errors[idx].errorMessage;
                      var propName = strExtract(errorMsg, "'", "'");
                      var propDisplay = entity.getDisplayName(propName);
                      errorMsg = errorMsg.replace("'" + propName + "'", "'" + propDisplay + "'" + (managerAuth.shellMode=='DEV' ? " (" + entity.typeName + "." + propName + ")": ""));
                      strErrors.push(translateError(errorMsg));
                  }
                  showModalAlert('Campos obrigatórios não estão preenchidos.', strErrors);
                  return true;
              }
           }
        }
        return false;
    };
    
    var saveChanges = function(saveSucceeded, saveFailed, fin, saveNoTRack) {
        if (saveNoTRack === true) {
            var dataForSaving = JSON.stringify(_.map(vm.getDataForSaving(), function(entity){ return entity.getPrimitiveDTO(entity.ChangeState != 'D'); }));
            return $.ajax({
               type: 'POST',
               crossDomain: true,
               url: getServiceAddress('LinxDemotestes/Save' + vm.rootDataTypeName),
               globalError: false,
               contentType: 'application/json',
               async: true,
               cache: false,
               data: dataForSaving,
               success: function (response) {
                      success(response);
               },
               error: function (jqXHR, textStatus, errorThrown) {
                      failed({ message: jqXHR.responseJSON.ExceptionMessage });
               }
            });
        }
        else {
            return manager.saveChanges()
                   .fail(failed).then(success);
        }
    
        function success(result) {
            if (fin) fin();
            if (saveNoTRack === true && result.length > 0) {
               for (var idx = 0; idx < result.length; idx++) { dataContext.initializePOCO(result[idx], vm.rootDataTypeName); }
            }
            else if (result != null && result.keyMappings != null && result.keyMappings.length > 0) {
                for (var idx = 0; idx < result.keyMappings.length; idx++) {
                    if (result.keyMappings[idx].realValue == null) {
                       var entity = manager.getEntityByKey(result.keyMappings[idx].entityTypeName, result.keyMappings[idx].tempValue);
                       if (entity) manager.detachEntity(entity);
                    }
                }
                manager.acceptChanges();
            }
            if (saveSucceeded)
                saveSucceeded(result);
        }
    
        function failed(error) {
            if (fin) fin();
            if (error.message.indexOf('Internal Error in key fixup - unable to locate entity') == -1 && error.message.indexOf('An entity with this key is already in the cache:') == -1) {
               if (saveFailed)
                   saveFailed(error);
               var msg = error.message.replace('Fail by saving data:', '');
               showModalAlert('Falha ao salvar informações.', [ msg ]);
               error.message = msg;
               throw error;
           } else {
               manager.acceptChanges();
           }
        }
    };
    
    var acceptChanges = function () {
        return manager.acceptChanges();
    };
    
    var getEntities = function (entityName) {
        return manager.getEntities(entityName);
    };
    
    var notifyPresentation = function(dataSourceName) {
          if (dataContext.dataForUpdate !== '') {
           setTimeout(function () { notifyPresentation(dataSourceName); }, 100);
           return;
          }
          dataContext.dataForUpdate = dataSourceName;
          document.dispatchEvent(dataUpdateEvent);
          dataContext.dataForUpdate = '';
    };
    
    var getEntityInCache = function (entityName, propertiesReference) {
        var keys = [];
        if (!isNullOrEmpty(propertiesReference)) {
            for (var i = 0; i < metadataInfo[entityName].length; i++) {
                if (metadataInfo[entityName][i].isPartOfKey && !isNullOrEmpty(propertiesReference[metadataInfo[entityName][i].key]))
                    keys.push(propertiesReference[metadataInfo[entityName][i].key]);
            }
            if (keys.length == 0)
                return null;
            else {
                return manager.getEntityByKey(entityName, (keys.length == 1 ? keys[0] : keys));
            }
        }
        else
            return null;
    }
    
    var createEntity = function(entityName, initialValues, unchanged) {
        var entity = getEntityInCache(entityName, initialValues);
        if (!isNullOrEmpty(entity))
            entity.entityAspect.entityState == (unchanged === true ? breeze.EntityState.Unchanged : breeze.EntityState.Added);
        else 
            entity = manager.createEntity(entityName, initialValues, (unchanged === true ? breeze.EntityState.Unchanged : breeze.EntityState.Added));
        return entity;
    };
    
    var createFreeEntity = function(entityName) {
       return manager.createEntity(entityName, {}, breeze.EntityState.Detached);
    }
    
    var createProduto = function() {
        //Create entity instance
        enableChangeTrack = false;
        var entity = createEntity('Produto');
        entity.setDefaults();
        if (typeof entity.OnAdding == 'function') {
            if (!entity.OnAdding()) { dataContext.deleteEntity(entity); return; }
        }
        enableChangeTrack = true;
        return entity;
    };
    
    var deleteEntity = function (entity) {
        entity.delete();
    };
    
    var detachEntity = function (entity) {
        if (!entity.isDetached()) entity.entityAspect.setDetached();
    };
    
    var attachEntity = function (entity) {
        manager.attachEntity(entity);
    };
    
    var clearAll = function () {
        manager.rejectChanges();
        manager.clear();
    };
    
    var executeQuery = function (getMethod, jEntitySearch, order, skip, take, noTracking, callBack) {
        var query = EntityQuery.from(getMethod).noTracking(noTracking)
        .orderBy(order);
    
        if ((typeof jEntitySearch !== 'undefined') && jEntitySearch !== null)
            query = query.withParameters({ jEntitySearch: jEntitySearch });
    
        if (take > 0)
           query = query.skip(skip).take(take);
        query = query.inlineCount(true);
    
        return manager.executeQuery(query)
        .fail(queryFailed).then(function (data) {
            if (callBack) {
                callBack(data.results);
            }
        });
    };
    var exportToExcel = function(entityName, jEntitySearch, translatedJEntitySearch, complete, columnsVisible) {
        var info = jQuery.grep(dataExportInfo[vm.rootDataTypeName], function (item, i) { return (item.name === entityName);});
        if (info == null || info.length === 0) {
            app.showMessage('Erro na exportação', 'Alerta', ['Ok']);
            return;
        }
        $.ajax({
           type: 'POST',
           crossDomain: true,
           url: getServiceAddress(info[0].actionExport),
           globalError: true,
           headers: managerAuth.getHeaders(),
           contentType: 'application/json',
           async: true,
           cache: false,
           data: JSON.stringify([jEntitySearch, translatedJEntitySearch, columnsVisible]),
           success: function (response) {
               if (response.startsWith('~/FileDownload/')) {
                    saveURL(getBaseServiceAddress(info[0].actionExport) + response.substr(1), entityName + '.xlsx');
               } else {
                    saveExcelBlob(entityName + '.xlsx', response);
               }
           },
           complete: function (jqXHR, textStatus) {
               if(complete) complete();
           },
           error: function (jqXHR, textStatus, errorThrown) {
           }
        });
    };
    var exportReportDataSource = function(complete) {
        $.ajax({
           type: 'GET',
           crossDomain: true,
           headers: managerAuth.getHeaders(),
           url: getServiceAddress("LinxDemotestes/GetReportDataSource"),
           success: function (response) {
                  saveExcelBlob('datasource.ldsx', response);
           },
           complete: function (jqXHR, textStatus) {
                  if(complete) complete();
           },
           error: function (jqXHR, textStatus, errorThrown) {
                  alert('Erro na exportação do data source');
           }
        });
    };
    var exportTemplateReport = function(reportPath, complete) {
        $.ajax({
           type: 'GET',
           crossDomain: true,
           headers: managerAuth.getHeaders(),
           url: getServiceAddress("LinxDemotestes/GetTemplateReport"),
           data: { reportPath: reportPath },
           success: function (response) {
                  saveExcelBlob(reportPath + '.lrtx', response);
           },
           complete: function (jqXHR, textStatus) {
                  if(complete) complete();
           },
           error: function (jqXHR, textStatus, errorThrown) {
                  alert('Erro na exportação do data source');
           }
        });
    };
    var exportToReport = function(reportName, entityName, jEntitySearch, translatedJEntitySearch, complete, columnsVisible, exportMedia) {
        var info = jQuery.grep(dataExportInfo[vm.rootDataTypeName], function (item, i) { return (item.name === entityName);});
        if (info == null || info.length === 0) {
            app.showMessage('Erro na exportação', 'Alerta', ['Ok']);
            return;
        }
        $.ajax({
           type: 'POST',
           crossDomain: true,
           headers: managerAuth.getHeaders(),
           url: getServiceAddress(info[0].actionReport),
           globalError: true,
           contentType: 'application/json',
           async: true,
           cache: false,
           data: JSON.stringify([ reportName, jEntitySearch, translatedJEntitySearch, columnsVisible, getServiceAddress(''), exportMedia ]),
           success: function (response) {
                  saveExcelBlob(entityName + '.lrtx', response);
           },
           complete: function (jqXHR, textStatus) {
                  if(complete) complete();
           },
           error: function (jqXHR, textStatus, errorThrown) {
           }
        });
    };
    
    var hasChanges = ko.observable(false);
    
    manager.hasChangesChanged.subscribe(function(eventArgs) {
        hasChanges(eventArgs.hasChanges);
    });
    
    //#region Internal methods
    
    function queryFailed(error) {
    }
    
    function log(msg, data) {
        logger.log(msg, data, system.getModuleId(dataContext), true);
    }
    
    function logError(msg, error) {
         logger.logError(msg, error, system.getModuleId(dataContext), true);
    }
    
    function loadParameters() {
     dataParameters.isLoaded = true;
    }
    
    //#endregion Internal methods
    //#region getWithBinding
    var createDTOEmpty = function (entityName) {
        var entityType = manager.metadataStore.getEntityType(entityName);
        var entity = {};
        for (var idx = 0; idx < entityType.dataProperties.length; idx++) {
            var prop = entityType.dataProperties[idx];
            entity[prop.name] = prop.defaultValue;
        }
        for (var idx = 0; idx < entityType.navigationProperties.length; idx++) {
            var navigationName = entityType.navigationProperties[idx].name;
            if (navigationName.endsWith('List'))
                entity['current' + navigationName.replace('List', '')] = null;
        }
        entity.isEmptyEntity = true;
        return entity;
    };
    var getWithBinding = function (binding, entityName) {
        if (typeof binding === 'function' && binding() != null)
            return binding;
        else
            return createDTOEmpty(entityName);
    }
    //#endregion getWithBinding
    
        var vm = null;
        var dataContext = {
            dataForUpdate: '',
            getPivotLayouts: getPivotLayouts,
            getSelectedLayoutContent: getSelectedLayoutContent,
            deleteLayoutSelected: deleteLayoutSelected,
            getServiceAddress: getServiceAddress,
            getBaseServiceAddress: getBaseServiceAddress,
            getDataFeedUrl: getDataFeedUrl,
            getDataServiceUrl: getDataServiceUrl,
            setServiceBusUrl: setServiceBusUrl,
            initializePOCO: initializePOCO,
            getWithBinding: getWithBinding,
            managerAuth: managerAuth,
            hasDataFeed: true,
            getAccessGroup: getAccessGroup,
            getNewGuid: getNewGuid,
            metadataInfo: metadataInfo,
            dataExportInfo: dataExportInfo,
            entityNames: entityNames,
            lookUpNames: lookUpNames,
            lookUpProperties: lookUpProperties,
            metadataStore: metadataStore,
            cancelChanges: cancelChanges,
            saveChanges: saveChanges,
            getChanges: getChanges,
            hasValidationErrors: hasValidationErrors,
            getEntityProperty: getEntityProperty,
            getViewInfo: getViewInfo,
            createEntity: createEntity,
            notifyPresentation: notifyPresentation,
            createFreeEntity: createFreeEntity,
            createProduto: createProduto,
            deleteEntity: deleteEntity,
            acceptChanges: acceptChanges,
            getEntities: getEntities,
            detachEntity: detachEntity,
            attachEntity: attachEntity,
            executeQuery: executeQuery,
            sharedData: [],
            clearAll: clearAll,
            hasChanges: hasChanges,
            dataDomains: dataDomains,
            dataParameters: dataParameters,
            loadParameters: loadParameters,
            exportToExcel: exportToExcel,
            exportToReport: exportToReport,
            exportReportDataSource: exportReportDataSource,
            exportTemplateReport: exportTemplateReport,
            businessAssemblyName: businessAssemblyName,
            controllerName: controllerName,
            getResultsCombo: getResultsCombo,
            clientFilterHasModified: clientFilterHasModified,
            lastClientFilterExpressions: {},
            breeze: breeze,
            contextUpdtEvt: contextUpdtEvt,
            setCurrentViewModel: function(vModel) { vm = vModel; },
                getBmEntityProperties: getBmEntityProperties,
            clearProduto: clearProduto,
            getProduto: getProduto,
            getProdutoByEntitySearchNoAssociations: getProdutoByEntitySearchNoAssociations
        };
    loadParameters();
    return dataContext;
    //#endregion Context Definition
}
return result;
});
