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
       return getServiceAddress('LinxDemoExAutocompleteOData');
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
    var controllerName = 'LinxDemoExAutocomplete';
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
    entityNames.push('Tbnmcompleto');
    metadataInfo['Tbnmcompleto'] = [
        { key: 'IdCliente', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: 'IdCliente', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Id Cliente', width: '271px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IdNome', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: 'IdNome', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Id Nome', width: '271px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'idNomeCompleto', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 10, isPartOfKey: true, headerText: 'id Nome Completo', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'idnomeMeio', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: 'idnomeMeio', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'id nomeMeio', width: '271px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IdSobrenome', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: 'IdSobrenome', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Id Sobrenome', width: '271px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'Nome', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: 'Nome', lookupVisibleColumns: '', maxLength: 100, validateMaxLength: true, isPartOfKey: false, headerText: 'Nome', width: '421px', dataType: 'string', format: '', hidden: false, unbound: false, group: null },
        { key: 'NomeCompleto', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 100, validateMaxLength: true, isPartOfKey: false, headerText: 'NomeCompleto', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null },
        { key: 'Nomedomeio', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: 'Nomedomeio', lookupVisibleColumns: '', isRequired: true, maxLength: 100, validateMaxLength: true, isPartOfKey: false, headerText: 'Nome do Meio', width: '421px', dataType: 'string', format: '', hidden: false, unbound: false, group: null },
        { key: 'SobreNome', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: 'SobreNome', lookupVisibleColumns: '', isRequired: true, maxLength: 100, validateMaxLength: true, isPartOfKey: false, headerText: 'Sobre Nome', width: '421px', dataType: 'string', format: '', hidden: false, unbound: false, group: null }
    ];
    dataExportInfo['Tbnmcompleto'] = [ 
        { name: 'Tbnmcompleto', canExportMedia: true , canExportReport: true, actionExport: 'LinxDemoExAutocomplete/GetTbnmcompletoToExcel', actionReport: 'LinxDemoExAutocomplete/GetTbnmcompletoToReportXml', actionFeed: 'LinxDemoExAutocompleteOData/Tbnmcompleto', actionName: 'LinxDemoExAutocomplete/GetTbnmcompletoByEntitySearchNoAssociations', display: 'Tbnmcompleto',  metaData: function() { return metadataInfo['Tbnmcompleto']; } }
    ];
    entitylookUps.push('Tbnmcompleto');
    entitylookUps['Tbnmcompleto'] = [];
    entitylookUps['Tbnmcompleto'].push('LookUpTbnmmeio');
    lookUpNames.push('LookUpTbnmmeio');
    metadataInfo['LookUpTbnmmeio'] = [
        { key: 'idnomeMeio', relatedKey: 'idnomeMeio', maxLength: 10, isPartOfKey: true, headerText: 'id nomeMeio', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'Nomedomeio', relatedKey: 'Nomedomeio', maxLength: 100, isPartOfKey: false, headerText: 'Nomedomeio', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null },
        { key: 'IdNome', relatedKey: 'IdNome', maxLength: 10, isPartOfKey: false, headerText: 'Id Nome', width: '250px', dataType: 'number', format: 'int', hidden: true, unbound: false, group: null }
    ];
    entitylookUps['Tbnmcompleto'].push('LookUpTbnome');
    lookUpNames.push('LookUpTbnome');
    metadataInfo['LookUpTbnome'] = [
        { key: 'IdNome', relatedKey: 'IdNome', maxLength: 10, isPartOfKey: true, headerText: 'Id Nome', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'Nome', relatedKey: 'Nome', maxLength: 100, isPartOfKey: false, headerText: 'Nome', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null }
    ];
    entitylookUps['Tbnmcompleto'].push('LookUpTbsobrenm');
    lookUpNames.push('LookUpTbsobrenm');
    metadataInfo['LookUpTbsobrenm'] = [
        { key: 'IdSobrenome', relatedKey: 'IdSobrenome', maxLength: 10, isPartOfKey: true, headerText: 'Id Sobrenome', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'SobreNome', relatedKey: 'SobreNome', maxLength: 100, isPartOfKey: false, headerText: 'SobreNome', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null },
        { key: 'IdNome', relatedKey: 'LookupKey1', maxLength: 10, isPartOfKey: false, headerText: 'Id Nome', width: '250px', dataType: 'number', format: 'int', hidden: true, unbound: false, group: null },
        { key: 'idnomeMeio', relatedKey: 'idnomeMeio', maxLength: 10, isPartOfKey: false, headerText: 'id nomeMeio', width: '250px', dataType: 'number', format: 'int', hidden: true, unbound: false, group: null }
    ];
    entitylookUps['Tbnmcompleto'].push('LookUpCliente');
    lookUpNames.push('LookUpCliente');
    metadataInfo['LookUpCliente'] = [
        { key: 'IdCliente', relatedKey: 'IdCliente', maxLength: 10, isPartOfKey: true, headerText: 'Id Cliente', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IdCliente2', relatedKey: 'IdCliente2', maxLength: 10, isPartOfKey: true, headerText: 'Id Cliente', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null }
    ];
    entityNames.push('TesteCkbView');
    metadataInfo['TesteCkbView'] = [
        { key: 'IdQualquer', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 0, isPartOfKey: true, headerText: 'Id Qualquer', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'NaoObrigatorio', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 1, isPartOfKey: false, headerText: 'NaoObrigatorio', width: '218px', dataType: 'bool', format: 'checkbox', hidden: false, unbound: false, group: null },
        { key: 'Obrigatorio', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 1, isPartOfKey: false, headerText: 'Obrigatorio', width: '179px', dataType: 'bool', format: 'checkbox', hidden: false, unbound: false, group: null }
    ];
    dataExportInfo['TesteCkbView'] = [ 
        { name: 'TesteCkbView', canExportMedia: true , canExportReport: true, actionExport: 'LinxDemoExAutocomplete/GetTesteCkbViewToExcel', actionReport: 'LinxDemoExAutocomplete/GetTesteCkbViewToReportXml', actionFeed: 'LinxDemoExAutocompleteOData/TesteCkbView', actionName: 'LinxDemoExAutocomplete/GetTesteCkbViewByEntitySearchNoAssociations', display: 'TesteCkbView',  metaData: function() { return metadataInfo['TesteCkbView']; } }
    ];
    entitylookUps.push('TesteCkbView');
    entitylookUps['TesteCkbView'] = [];
    entityNames.push('Cliente');
    metadataInfo['Cliente'] = [
        { key: 'ComboboxCliente', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 3, isPartOfKey: false, headerText: 'Combobox Cliente', width: '244px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'DatetimeCliente', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Datetime Cliente', width: '244px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null },
        { key: 'DecimalCliente', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 13, isPartOfKey: false, headerText: 'Decimal Cliente', width: '231px', dataType: 'number', format: '0.00', hidden: false, unbound: false, group: null },
        { key: 'IdCliente', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 10, isPartOfKey: true, headerText: 'Id Cliente', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IdEstado', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: 'IdEstado', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Id Estado', width: '271px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IntCliente', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Int Cliente', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'SmallIntCliente', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 5, isPartOfKey: false, headerText: 'Small Int Cliente', width: '257px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'StringCliente', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: 'NomeCompleto', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'String Cliente', width: '421px', dataType: 'string', format: '', hidden: false, unbound: false, group: null },
        { key: 'StringEstado', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: 'StringEstado', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'String Estado', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null },
        { key: 'IdNmCompleto', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: 'idNomeCompleto', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Id nome completo', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null }
    ];
    dataExportInfo['Cliente'] = [ 
        { name: 'Cliente', canExportMedia: true , canExportReport: true, actionExport: 'LinxDemoExAutocomplete/GetClienteToExcel', actionReport: 'LinxDemoExAutocomplete/GetClienteToReportXml', actionFeed: 'LinxDemoExAutocompleteOData/Cliente', actionName: 'LinxDemoExAutocomplete/GetClienteByEntitySearchNoAssociations', display: 'Cliente',  metaData: function() { return metadataInfo['Cliente']; } }
    ];
    entitylookUps.push('Cliente');
    entitylookUps['Cliente'] = [];
    entitylookUps['Cliente'].push('LookUpEstado');
    lookUpNames.push('LookUpEstado');
    metadataInfo['LookUpEstado'] = [
        { key: 'IdEstado', relatedKey: 'IdEstado', maxLength: 10, isPartOfKey: true, headerText: 'Id Estado', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'StringEstado', relatedKey: 'StringEstado', maxLength: 50, isPartOfKey: false, headerText: 'String Estado', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null }
    ];
    entitylookUps['Cliente'].push('LkpTbnmcompleto');
    lookUpNames.push('LkpTbnmcompleto');
    metadataInfo['LkpTbnmcompleto'] = [
        { key: 'idNomeCompleto', relatedKey: 'IdNmCompleto', maxLength: 0, isPartOfKey: false, headerText: 'idNomeCompleto', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'NomeCompleto', relatedKey: 'StringCliente', maxLength: 0, isPartOfKey: false, headerText: 'NomeCompleto', width: '250px', dataType: 'string', format: '', hidden: false, unbound: false, group: null }
    ];
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
    
    // Configure Tbnmcompleto data type
    metadataStore.addEntityType({
    shortName: "Tbnmcompleto",
    namespace: "Linx.Demo.BV.ExAutocomplete",
    autoGeneratedKeyType: AutoGeneratedKeyType.Identity,
    dataProperties: {
    IdCliente: { dataType: DataType.Int32, isNullable: false, isPartOfKey: false, validators: [ ]  }
    ,IdNome: { dataType: DataType.Int32, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,idNomeCompleto: { dataType: DataType.Int32, isNullable: false, isPartOfKey: true, validators: [ Validator.hasValueValidator]  }
    ,idnomeMeio: { dataType: DataType.Int32, isNullable: false, isPartOfKey: false, validators: [ ]  }
    ,IdSobrenome: { dataType: DataType.Int32, isNullable: false, isPartOfKey: false, validators: [ ]  }
    ,Nome: { dataType: DataType.String, maxLength: 100, isNullable: true, isPartOfKey: false, validators: [ Validator.maxLength( {maxLength: 100})]  }
    ,NomeCompleto: { dataType: DataType.String, maxLength: 100, isNullable: true, isPartOfKey: false, validators: [ Validator.maxLength( {maxLength: 100})]  }
    ,Nomedomeio: { dataType: DataType.String, maxLength: 100, isNullable: false, isPartOfKey: false, validators: [ Validator.hasValueValidator, Validator.maxLength( {maxLength: 100})]  }
    ,SobreNome: { dataType: DataType.String, maxLength: 100, isNullable: false, isPartOfKey: false, validators: [ Validator.hasValueValidator, Validator.maxLength( {maxLength: 100})]  }
                    },
    navigationProperties: {
    // Returns collections of details and associates with Parent
                          }
    });
    lookUpProperties['Tbnmcompleto'] = {IdCliente: 'LookUpCliente', IdNome: 'LookUpTbnome', idnomeMeio: 'LookUpTbnmmeio', IdSobrenome: 'LookUpTbsobrenm', Nome: 'LookUpTbnome', Nomedomeio: 'LookUpTbnmmeio', SobreNome: 'LookUpTbsobrenm'};
    var TbnmcompletoInitializer = function (ownerReference, isPOCO) {
       ownerReference.RowDataId = (isPOCO === true ? getNextSequence('Tbnmcompleto') : ko.observable(getNextSequence('Tbnmcompleto')));
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
             var filterByKey = 'Tbnmcompleto{' + 'idNomeCompleto#==#I' + getAbsoluteValue(ownerReference.idNomeCompleto).toString() + '}';
             if (!ownerReference.isPOCO && ownerReference.entityAspect && !ownerReference.isDetached() && !ownerReference.isUnchanged()) ownerReference.entityAspect.setUnchanged();
             return dataContext.getTbnmcompletoByEntitySearchNoAssociations(filterByKey, 0, 0, false, true, ownerReference.isPOCO === true).then(querySucceeded);
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
       ownerReference.serverDataType['IdCliente'] = 'I';
       ownerReference.serverDataType['IdNome'] = 'I';
       ownerReference.serverDataType['idNomeCompleto'] = 'I';
       ownerReference.serverDataType['idnomeMeio'] = 'I';
       ownerReference.serverDataType['IdSobrenome'] = 'I';
       ownerReference.serverDataType['Nome'] = 'S';
       ownerReference.serverDataType['NomeCompleto'] = 'S';
       ownerReference.serverDataType['Nomedomeio'] = 'S';
       ownerReference.serverDataType['SobreNome'] = 'S';
       ownerReference.typeName = 'Tbnmcompleto';
       ownerReference.isPrimaryKey = function(propertyName) {
           var keys = [ 'idNomeCompleto' ];
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
               if (typeof cacheElements[idxR].Tbnmcompleto !== 'function') { return; }
               else  if (cacheElements[idxR].Tbnmcompleto() != ownerReference) { cacheElements[idxR].Tbnmcompleto(ownerReference); }
           }
       }
       //#region Lookup Extended Methods
       if (isPOCO !== true) {
           ownerReference.getLookupPropertyName = function(propertyName) {
              var property = getEntityProperty(ownerReference.typeName, propertyName);
              return (property != null && !isNullOrEmpty(property.lookupPropertyName) ? property.lookupPropertyName : propertyName);
           }
           ownerReference.getLookupVisibleColumns = function(propertyName) {
              var property = getEntityProperty(ownerReference.typeName, propertyName);
              return (property != null ? property.lookupVisibleColumns : '');
           }
           ownerReference.getLookUpClientFilterExpressions = function (lookupName, lookupInfo) {
               return '';
           };
        
           ownerReference.getLookupDisplay = function (lookupName) {
               var displayName = '';
               if (lookupName === 'LookUpTbnmmeio') {
                   displayName = ' de Tbnmmeio';
               }
               if (lookupName === 'LookUpTbnome') {
                   displayName = ' de Tbnome';
               }
               if (lookupName === 'LookUpTbsobrenm') {
                   displayName = ' de Tbsobrenm';
               }
               if (lookupName === 'LookUpCliente') {
                   displayName = ' de Cliente';
               }
               return 'Seleção' + displayName;
           };
        
           ownerReference.getSpecializedLookup = function (lookupName, lookupInfo, fieldToSearch, valueToSearch, ownerReference, allowMultiSelectionInSearch) {
               var specializedLookup = '';
               return specializedLookup;
           };
        
           ownerReference.getSubQueryFilterFromLookUpTbnmmeio = function (propertyName) {
               var filter = '';
               return filter;
           }
           ownerReference.getSubQueryFilterFromLookUpTbnome = function (propertyName) {
               var filter = '';
               return filter;
           }
           ownerReference.getSubQueryFilterFromLookUpTbsobrenm = function (propertyName) {
               var filter = '';
               return filter;
           }
           ownerReference.getSubQueryFilterFromLookUpCliente = function (propertyName) {
               var filter = '';
               return filter;
           }
           ownerReference.canGetClientFilter = function (lookupName) {
               return true;
           }
           ownerReference.validatedlookupsArray = [];
           ownerReference.internalLookupSearch = function (lookupName, fieldToSearch, operation, querySucceeded, lookupInfo, valueToSearch, beforeGettingLookup, referencefield) {
               if (!lookupName || !fieldToSearch) { console.warn('lookupName or fieldToSearch is Empty!'); querySucceeded(null); return lookupInfo; }
               if (isNullOrEmpty(lookupInfo.lastJEntityExpression)) {
                   if (isNullOrEmpty(referencefield)) referencefield = fieldToSearch;
                   if ((typeof valueToSearch) === 'undefined')
                       valueToSearch = getAbsoluteValue(ownerReference[referencefield]);
                   var extraFilters = '';
                   if (ownerReference.canGetClientFilter(lookupName)) {
                       extraFilters = ownerReference.getLookUpClientFilterExpressions(lookupName, lookupInfo);
                       dataContext.lastClientFilterExpressions[lookupName] = extraFilters;
                       if (extraFilters === 'Error') { querySucceeded(null); return lookupInfo; }
                       if (typeof ownerReference['BeforeGet' + lookupName + 'Query'] == 'function') {
                           var customFilter = ownerReference['BeforeGet' + lookupName + 'Query'](fieldToSearch, lookupInfo);
                           if (customFilter === 'Error') { querySucceeded(null); return lookupInfo; }
                           if (!isNullOrEmpty(customFilter)) { extraFilters = (isNullOrEmpty(extraFilters) ? '' : extraFilters + ';') + customFilter; }
                       }
                       if (typeof ownerReference['getSubQueryFilterFrom' + lookupName] == 'function') {
                           var customFilter = ownerReference['getSubQueryFilterFrom' + lookupName](referencefield);
                           if (customFilter === 'Error') { querySucceeded(null); return lookupInfo; }
                           if (!isNullOrEmpty(customFilter)) { extraFilters = (isNullOrEmpty(extraFilters) ? '' : extraFilters + ';') + customFilter; }
                       }
                   }
                   var completeExpression = getLookUpJEntityExpression(lookupName, ownerReference, fieldToSearch, valueToSearch, extraFilters, referencefield, app, lookupInfo.vm.useLikeCommandAsDefault);
                   if (completeExpression === 'Error') { querySucceeded(null); return lookupInfo; }
                   lookupInfo.lastJEntityExpression = completeExpression;
               }
               switch (operation) {
                   case 'F':
                       lookupInfo.pageSkip = 0;
                       break;
                   case 'B':
                       lookupInfo.pageSkip = lookupInfo.pageSkip - 1;
                       break;
                   case 'N':
                       lookupInfo.pageSkip = lookupInfo.pageSkip + 1;
                       break;
                   case 'L':
                       lookupInfo.pageSkip = lookupInfo.totalPages();
                       break;
                   default:
               }
        
               var e = { cancel: false, lookupName: lookupName, jEntitySearch: lookupInfo.lastJEntityExpression, entity: ownerReference, viewModel: lookupInfo.vm };
               if (beforeGettingLookup) beforeGettingLookup(e);
               if (e.cancel) { querySucceeded(null); return lookupInfo; }
               if(lookupInfo.lastJEntityExpression !== e.jEntitySearch)
                   lookupInfo.lastJEntityExpression = e.jEntitySearch;
               if (lookupInfo.vm) lookupInfo.vm.dataToolbar.isBusy(true);
               var returnQueryResult = function (data) { lookupInfo.totalRecords = (isNullOrEmpty(data.inlineCount) ? data.results.length : data.inlineCount); querySucceeded(data); };
               eval('dataContext.get' + lookupName + 'ByEntitySearch(lookupInfo.lastJEntityExpression, (isNullOrEmpty(lookupInfo.fieldToSort) ? fieldToSearch : lookupInfo.fieldToSort), lookupInfo.pageSize*lookupInfo.pageSkip, lookupInfo.pageSize, lookupInfo.sortDirection, fieldToSearch).then(returnQueryResult).fail(queryFailed)').fin(function(){ if (lookupInfo.vm) lookupInfo.vm.dataToolbar.isBusy(false); });
               return lookupInfo;
           };
        
           ownerReference.hasValidClientFilter = function (lookupName, lookupInfo) {
               var checkClientFilter = ownerReference.getLookUpClientFilterExpressions(lookupName, lookupInfo);
               if (checkClientFilter === 'Error') { return false; }
               if (typeof ownerReference['BeforeGet' + lookupName + 'Query'] == 'function') {
                   checkClientFilter = ownerReference['BeforeGet' + lookupName + 'Query']('', lookupInfo);
                   if (checkClientFilter === 'Error') { return false; }
               }
               return true;
           }
        
           ownerReference.executeLookUp = function (lookupName, fieldToSearch, beforeGettingLookup, vm, valueToSearch, finished, comboCallBack, allowMultiSelectionInSearch) {
               if (!lookupName || !fieldToSearch) { console.warn('lookupName or fieldToSearch is Empty!'); if (finished) finished(false, null); return; }
               var lookupFieldName = ownerReference.getLookupPropertyName(fieldToSearch);
               vm.dataBind('', true);
               var lookupInfo = new lookupInformation();
               lookupInfo.visibleColumns = ownerReference.getLookupVisibleColumns(fieldToSearch);
               lookupInfo.vm = vm;
               lookupInfo.isMultiSelection = lookupName.in([]);
               var specializedLookup = ownerReference.getSpecializedLookup(lookupName, lookupInfo, lookupFieldName, valueToSearch, ownerReference, allowMultiSelectionInSearch);
               if (isNullOrEmpty(specializedLookup)) {
                       ownerReference.internalLookupSearch(lookupName, lookupFieldName, 'F',
                           function querySucceeded(data) {
                               if (typeof ownerReference['OnLoading' + lookupName + 'Query'] == 'function') {
                                   ownerReference['OnLoading' + lookupName + 'Query'](data);
                               }
                               if ((typeof comboCallBack) === 'function') {
                                   return comboCallBack(data ? data.results : null);
                               }
                               else if (data == null || data.results == null || data.results.length == 0) {
                                   if (finished) finished(false, null);
                                   ownerReference.clearLookUp(lookupName);
                                   if (data != null) app.showMessage('A informação de Lookup [' + ownerReference.getDisplayName(fieldToSearch) + '] não foi encontrada!', 'Informação', ['Ok']);
                                   return;
                               }
                               lookupInfo.totalRecords = (isNullOrEmpty(data.inlineCount) ? data.results.length : data.inlineCount);
                               showLookUp(dataContext, ownerReference, ownerReference.getLookupDisplay(lookupName), lookupName, lookupFieldName, ownerReference.internalLookupSearch, lookupInfo, 
                                   function (confirm, values) {
                                       var results = '';
                                       if (values != null && values.length > 1) {
                                           $.each(values, function (index, item) { results += (index == 0 ? '' : ',') + item[lookupFieldName].toString().trim() });
                                           results = '[' + results + ']';
                                           ownerReference[fieldToSearch](results);
                                           if (vm.entitySearchRange[ownerReference.typeName + fieldToSearch] === undefined)
                                               vm.entitySearchRange[ownerReference.typeName + fieldToSearch] = ko.observable(results);
                                           else vm.entitySearchRange[ownerReference.typeName + fieldToSearch](results);
                                           document.dispatchEvent(dataUpdateEvent);
                                       }
                                       if (finished) finished(confirm, results);
                                   }, data.results, allowMultiSelectionInSearch);
                       }, lookupInfo, valueToSearch, beforeGettingLookup, fieldToSearch);
               }
               else {
                       var currentModal = (modal.inUse ? modal2 : modal);
                       if (currentModal.inUse === false) {
                           //Check Client Validations
                           if (!ownerReference.hasValidClientFilter(lookupName, lookupInfo)) { if (finished) finished(false); return; }
                           //Show External Lookup
                           currentModal.showModal(specializedLookup.moduleName, specializedLookup.uiSettings, ownerReference.getLookupDisplay(lookupName), ['Ok', 'Cancelar'], 'large').then(function (r, data) {
                           if (r == 'Ok') {
                               if (!currentModal.internalUIs || currentModal.internalUIs.length != 1) { if (finished) finished(false); return; }
                               var lookupVM = currentModal[currentModal.internalUIs[0]]; 
                               if (!lookupVM) return; 
                               if (typeof lookupVM == 'function') lookupVM = lookupVM(); 
                               currentModal[currentModal.internalUIs[0]] = null; 
                               var selectedItems = lookupVM.getSpecializedLookupItems(); 
                               if (vm.status() == 'C' && selectedItems != null && selectedItems.length > 1) { 
                                   var results = '';
                                   $.each(selectedItems, function (index, item) { results += (index == 0 ? '' : ',') + (typeof item[lookupFieldName] == 'function' ? item[lookupFieldName]() : item[lookupFieldName]).toString().trim() });
                                   results = '[' + results + ']'
                                   ownerReference[fieldToSearch](results);
                                   if (vm.entitySearchRange[ownerReference.typeName + fieldToSearch] === undefined)
                                       vm.entitySearchRange[ownerReference.typeName + fieldToSearch] = ko.observable(results);
                                   else vm.entitySearchRange[ownerReference.typeName + fieldToSearch](results);
                                   document.dispatchEvent(dataUpdateEvent);
                                   if (finished) finished(true, results);
                               }
                               else if (selectedItems.length > 0) { dataContext['finalizeAll' + lookupName](ownerReference, selectedItems, '', lookupInfo); }
                           }
                           if (finished) finished(r === 'Ok');
                       });
                   }
                   if (finished) finished(false);
               }
           };
           ownerReference.clearLookUp = function (lookupName) {
               return eval('dataContext.clear' + lookupName + '(ownerReference)');
           };
       }
       //#endregion Lookup Extended Methods
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
       ownerReference.namespace = 'Linx.Demo.BV.ExAutocomplete';
       ownerReference.myProperties = [ 'IdCliente','IdNome','idNomeCompleto','idnomeMeio','IdSobrenome','Nome','NomeCompleto','Nomedomeio','SobreNome' ];
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
    metadataStore.registerEntityTypeCtor("Tbnmcompleto", null, TbnmcompletoInitializer);
    
    // Configure TesteCkbView data type
    metadataStore.addEntityType({
    shortName: "TesteCkbView",
    namespace: "Linx.Demo.BV.ExAutocomplete",
    autoGeneratedKeyType: AutoGeneratedKeyType.Identity,
    dataProperties: {
    IdQualquer: { dataType: DataType.Int32, isNullable: false, isPartOfKey: true, validators: [ Validator.hasValueValidator]  }
    ,NaoObrigatorio: { dataType: DataType.Boolean, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,Obrigatorio: { dataType: DataType.Boolean, isNullable: true, isPartOfKey: false, validators: [ ]  }
                    },
    navigationProperties: {
    // Returns collections of details and associates with Parent
                          }
    });
    lookUpProperties['TesteCkbView'] = {};
    var TesteCkbViewInitializer = function (ownerReference, isPOCO) {
       ownerReference.RowDataId = (isPOCO === true ? getNextSequence('TesteCkbView') : ko.observable(getNextSequence('TesteCkbView')));
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
             var filterByKey = 'TesteCkbView{' + 'IdQualquer#==#I' + getAbsoluteValue(ownerReference.IdQualquer).toString() + '}';
             if (!ownerReference.isPOCO && ownerReference.entityAspect && !ownerReference.isDetached() && !ownerReference.isUnchanged()) ownerReference.entityAspect.setUnchanged();
             return dataContext.getTesteCkbViewByEntitySearchNoAssociations(filterByKey, 0, 0, false, true, ownerReference.isPOCO === true).then(querySucceeded);
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
       ownerReference.serverDataType['IdQualquer'] = 'I';
       ownerReference.serverDataType['NaoObrigatorio'] = 'B';
       ownerReference.serverDataType['Obrigatorio'] = 'B';
       ownerReference.typeName = 'TesteCkbView';
       ownerReference.isPrimaryKey = function(propertyName) {
           var keys = [ 'IdQualquer' ];
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
               if (typeof cacheElements[idxR].TesteCkbView !== 'function') { return; }
               else  if (cacheElements[idxR].TesteCkbView() != ownerReference) { cacheElements[idxR].TesteCkbView(ownerReference); }
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
       ownerReference.namespace = 'Linx.Demo.BV.ExAutocomplete';
       ownerReference.myProperties = [ 'IdQualquer','NaoObrigatorio','Obrigatorio' ];
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
    metadataStore.registerEntityTypeCtor("TesteCkbView", null, TesteCkbViewInitializer);
    
    // Configure Cliente data type
    metadataStore.addEntityType({
    shortName: "Cliente",
    namespace: "Linx.Demo.BV.ExAutocomplete",
    autoGeneratedKeyType: AutoGeneratedKeyType.Identity,
    dataProperties: {
    ComboboxCliente: { dataType: DataType.Byte, isNullable: false, isPartOfKey: false, validators: [ ]  }
    ,DatetimeCliente: { dataType: DataType.DateTime, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,DecimalCliente: { dataType: DataType.Decimal, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,IdCliente: { dataType: DataType.Int32, isNullable: false, isPartOfKey: true, validators: [ Validator.hasValueValidator]  }
    ,IdEstado: { dataType: DataType.Int32, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,IntCliente: { dataType: DataType.Int32, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,SmallIntCliente: { dataType: DataType.Int16, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,StringCliente: { dataType: DataType.String, maxLength: 50, isNullable: true, isPartOfKey: false, validators: [ Validator.maxLength( {maxLength: 50})]  }
    ,StringEstado: { dataType: DataType.String, maxLength: 50, isNullable: true, isPartOfKey: false, validators: [ Validator.maxLength( {maxLength: 50})]  }
    ,IdNmCompleto: { dataType: DataType.Int32, isNullable: false, isPartOfKey: false, validators: [ ]  }
                    },
    navigationProperties: {
    // Returns collections of details and associates with Parent
                          }
    });
    lookUpProperties['Cliente'] = {IdEstado: 'LookUpEstado', StringCliente: 'LkpTbnmcompleto', StringEstado: 'LookUpEstado', IdNmCompleto: 'LkpTbnmcompleto'};
    var ClienteInitializer = function (ownerReference, isPOCO) {
       ownerReference.RowDataId = (isPOCO === true ? getNextSequence('Cliente') : ko.observable(getNextSequence('Cliente')));
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
             var filterByKey = 'Cliente{' + 'IdCliente#==#I' + getAbsoluteValue(ownerReference.IdCliente).toString() + '}';
             if (!ownerReference.isPOCO && ownerReference.entityAspect && !ownerReference.isDetached() && !ownerReference.isUnchanged()) ownerReference.entityAspect.setUnchanged();
             return dataContext.getClienteByEntitySearchNoAssociations(filterByKey, 0, 0, false, true, ownerReference.isPOCO === true).then(querySucceeded);
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
       ownerReference.serverDataType['ComboboxCliente'] = 'Y';
       ownerReference.serverDataType['DatetimeCliente'] = 'T';
       ownerReference.serverDataType['DecimalCliente'] = 'D';
       ownerReference.serverDataType['IdCliente'] = 'I';
       ownerReference.serverDataType['IdEstado'] = 'I';
       ownerReference.serverDataType['IntCliente'] = 'I';
       ownerReference.serverDataType['SmallIntCliente'] = 'H';
       ownerReference.serverDataType['StringCliente'] = 'S';
       ownerReference.serverDataType['StringEstado'] = 'S';
       ownerReference.serverDataType['IdNmCompleto'] = 'I';
       ownerReference.typeName = 'Cliente';
       ownerReference.isPrimaryKey = function(propertyName) {
           var keys = [ 'IdCliente' ];
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
               if (typeof cacheElements[idxR].Cliente !== 'function') { return; }
               else  if (cacheElements[idxR].Cliente() != ownerReference) { cacheElements[idxR].Cliente(ownerReference); }
           }
       }
       //#region Lookup Extended Methods
       if (isPOCO !== true) {
           ownerReference.getLookupPropertyName = function(propertyName) {
              var property = getEntityProperty(ownerReference.typeName, propertyName);
              return (property != null && !isNullOrEmpty(property.lookupPropertyName) ? property.lookupPropertyName : propertyName);
           }
           ownerReference.getLookupVisibleColumns = function(propertyName) {
              var property = getEntityProperty(ownerReference.typeName, propertyName);
              return (property != null ? property.lookupVisibleColumns : '');
           }
           ownerReference.getLookUpClientFilterExpressions = function (lookupName, lookupInfo) {
               return '';
           };
        
           ownerReference.getLookupDisplay = function (lookupName) {
               var displayName = '';
               if (lookupName === 'LookUpEstado') {
                   displayName = ' de Estado';
               }
               if (lookupName === 'LkpTbnmcompleto') {
                   displayName = ' de Montar nome completo';
               }
               return 'Seleção' + displayName;
           };
        
           ownerReference.getSpecializedLookup = function (lookupName, lookupInfo, fieldToSearch, valueToSearch, ownerReference, allowMultiSelectionInSearch) {
               var specializedLookup = '';
               if (lookupName === 'LkpTbnmcompleto') {
                   specializedLookup = { moduleName: 'pkg_linx-demo-bv-spa/viewmodels/ExemploAutoComplete', uiSettings: { modalForm: modal, fieldToSearch: fieldToSearch, valueToSearch: valueToSearch, lookupInfo: lookupInfo, lookupName: lookupName, ownerReference: ownerReference, removeDataToolbar: false, shareParentBO: false, useFilterFromParent: false, parentSelectorDataName: '', canClear: true, canSearch: true, canAddNew: false, canEdit: false, canDelete: false, canCustomSearch: true, canPrint: false, canLayout: false, canNavigate: true, allowMultiSelectionInSearch: allowMultiSelectionInSearch, applyFilterToParent: false, noSearch: false, parentFieldsRelation: [], detailFieldsRelation: [] } 
                   };
               }
               return specializedLookup;
           };
        
           ownerReference.getSubQueryFilterFromLookUpEstado = function (propertyName) {
               var filter = '';
               return filter;
           }
           ownerReference.getSubQueryFilterFromLkpTbnmcompleto = function (propertyName) {
               var filter = '';
               return filter;
           }
           ownerReference.canGetClientFilter = function (lookupName) {
               return true;
           }
           ownerReference.validatedlookupsArray = [];
           ownerReference.internalLookupSearch = function (lookupName, fieldToSearch, operation, querySucceeded, lookupInfo, valueToSearch, beforeGettingLookup, referencefield) {
               if (!lookupName || !fieldToSearch) { console.warn('lookupName or fieldToSearch is Empty!'); querySucceeded(null); return lookupInfo; }
               if (isNullOrEmpty(lookupInfo.lastJEntityExpression)) {
                   if (isNullOrEmpty(referencefield)) referencefield = fieldToSearch;
                   if ((typeof valueToSearch) === 'undefined')
                       valueToSearch = getAbsoluteValue(ownerReference[referencefield]);
                   var extraFilters = '';
                   if (ownerReference.canGetClientFilter(lookupName)) {
                       extraFilters = ownerReference.getLookUpClientFilterExpressions(lookupName, lookupInfo);
                       dataContext.lastClientFilterExpressions[lookupName] = extraFilters;
                       if (extraFilters === 'Error') { querySucceeded(null); return lookupInfo; }
                       if (typeof ownerReference['BeforeGet' + lookupName + 'Query'] == 'function') {
                           var customFilter = ownerReference['BeforeGet' + lookupName + 'Query'](fieldToSearch, lookupInfo);
                           if (customFilter === 'Error') { querySucceeded(null); return lookupInfo; }
                           if (!isNullOrEmpty(customFilter)) { extraFilters = (isNullOrEmpty(extraFilters) ? '' : extraFilters + ';') + customFilter; }
                       }
                       if (typeof ownerReference['getSubQueryFilterFrom' + lookupName] == 'function') {
                           var customFilter = ownerReference['getSubQueryFilterFrom' + lookupName](referencefield);
                           if (customFilter === 'Error') { querySucceeded(null); return lookupInfo; }
                           if (!isNullOrEmpty(customFilter)) { extraFilters = (isNullOrEmpty(extraFilters) ? '' : extraFilters + ';') + customFilter; }
                       }
                   }
                   var completeExpression = getLookUpJEntityExpression(lookupName, ownerReference, fieldToSearch, valueToSearch, extraFilters, referencefield, app, lookupInfo.vm.useLikeCommandAsDefault);
                   if (completeExpression === 'Error') { querySucceeded(null); return lookupInfo; }
                   lookupInfo.lastJEntityExpression = completeExpression;
               }
               switch (operation) {
                   case 'F':
                       lookupInfo.pageSkip = 0;
                       break;
                   case 'B':
                       lookupInfo.pageSkip = lookupInfo.pageSkip - 1;
                       break;
                   case 'N':
                       lookupInfo.pageSkip = lookupInfo.pageSkip + 1;
                       break;
                   case 'L':
                       lookupInfo.pageSkip = lookupInfo.totalPages();
                       break;
                   default:
               }
        
               var e = { cancel: false, lookupName: lookupName, jEntitySearch: lookupInfo.lastJEntityExpression, entity: ownerReference, viewModel: lookupInfo.vm };
               if (beforeGettingLookup) beforeGettingLookup(e);
               if (e.cancel) { querySucceeded(null); return lookupInfo; }
               if(lookupInfo.lastJEntityExpression !== e.jEntitySearch)
                   lookupInfo.lastJEntityExpression = e.jEntitySearch;
               if (lookupInfo.vm) lookupInfo.vm.dataToolbar.isBusy(true);
               var returnQueryResult = function (data) { lookupInfo.totalRecords = (isNullOrEmpty(data.inlineCount) ? data.results.length : data.inlineCount); querySucceeded(data); };
               eval('dataContext.get' + lookupName + 'ByEntitySearch(lookupInfo.lastJEntityExpression, (isNullOrEmpty(lookupInfo.fieldToSort) ? fieldToSearch : lookupInfo.fieldToSort), lookupInfo.pageSize*lookupInfo.pageSkip, lookupInfo.pageSize, lookupInfo.sortDirection, fieldToSearch).then(returnQueryResult).fail(queryFailed)').fin(function(){ if (lookupInfo.vm) lookupInfo.vm.dataToolbar.isBusy(false); });
               return lookupInfo;
           };
        
           ownerReference.hasValidClientFilter = function (lookupName, lookupInfo) {
               var checkClientFilter = ownerReference.getLookUpClientFilterExpressions(lookupName, lookupInfo);
               if (checkClientFilter === 'Error') { return false; }
               if (typeof ownerReference['BeforeGet' + lookupName + 'Query'] == 'function') {
                   checkClientFilter = ownerReference['BeforeGet' + lookupName + 'Query']('', lookupInfo);
                   if (checkClientFilter === 'Error') { return false; }
               }
               return true;
           }
        
           ownerReference.executeLookUp = function (lookupName, fieldToSearch, beforeGettingLookup, vm, valueToSearch, finished, comboCallBack, allowMultiSelectionInSearch) {
               if (!lookupName || !fieldToSearch) { console.warn('lookupName or fieldToSearch is Empty!'); if (finished) finished(false, null); return; }
               var lookupFieldName = ownerReference.getLookupPropertyName(fieldToSearch);
               vm.dataBind('', true);
               var lookupInfo = new lookupInformation();
               lookupInfo.visibleColumns = ownerReference.getLookupVisibleColumns(fieldToSearch);
               lookupInfo.vm = vm;
               lookupInfo.isMultiSelection = lookupName.in([]);
               var specializedLookup = ownerReference.getSpecializedLookup(lookupName, lookupInfo, lookupFieldName, valueToSearch, ownerReference, allowMultiSelectionInSearch);
               if (isNullOrEmpty(specializedLookup)) {
                       ownerReference.internalLookupSearch(lookupName, lookupFieldName, 'F',
                           function querySucceeded(data) {
                               if (typeof ownerReference['OnLoading' + lookupName + 'Query'] == 'function') {
                                   ownerReference['OnLoading' + lookupName + 'Query'](data);
                               }
                               if ((typeof comboCallBack) === 'function') {
                                   return comboCallBack(data ? data.results : null);
                               }
                               else if (data == null || data.results == null || data.results.length == 0) {
                                   if (finished) finished(false, null);
                                   ownerReference.clearLookUp(lookupName);
                                   if (data != null) app.showMessage('A informação de Lookup [' + ownerReference.getDisplayName(fieldToSearch) + '] não foi encontrada!', 'Informação', ['Ok']);
                                   return;
                               }
                               lookupInfo.totalRecords = (isNullOrEmpty(data.inlineCount) ? data.results.length : data.inlineCount);
                               showLookUp(dataContext, ownerReference, ownerReference.getLookupDisplay(lookupName), lookupName, lookupFieldName, ownerReference.internalLookupSearch, lookupInfo, 
                                   function (confirm, values) {
                                       var results = '';
                                       if (values != null && values.length > 1) {
                                           $.each(values, function (index, item) { results += (index == 0 ? '' : ',') + item[lookupFieldName].toString().trim() });
                                           results = '[' + results + ']';
                                           ownerReference[fieldToSearch](results);
                                           if (vm.entitySearchRange[ownerReference.typeName + fieldToSearch] === undefined)
                                               vm.entitySearchRange[ownerReference.typeName + fieldToSearch] = ko.observable(results);
                                           else vm.entitySearchRange[ownerReference.typeName + fieldToSearch](results);
                                           document.dispatchEvent(dataUpdateEvent);
                                       }
                                       if (finished) finished(confirm, results);
                                   }, data.results, allowMultiSelectionInSearch);
                       }, lookupInfo, valueToSearch, beforeGettingLookup, fieldToSearch);
               }
               else {
                       var currentModal = (modal.inUse ? modal2 : modal);
                       if (currentModal.inUse === false) {
                           //Check Client Validations
                           if (!ownerReference.hasValidClientFilter(lookupName, lookupInfo)) { if (finished) finished(false); return; }
                           //Show External Lookup
                           currentModal.showModal(specializedLookup.moduleName, specializedLookup.uiSettings, ownerReference.getLookupDisplay(lookupName), ['Ok', 'Cancelar'], 'large').then(function (r, data) {
                           if (r == 'Ok') {
                               if (!currentModal.internalUIs || currentModal.internalUIs.length != 1) { if (finished) finished(false); return; }
                               var lookupVM = currentModal[currentModal.internalUIs[0]]; 
                               if (!lookupVM) return; 
                               if (typeof lookupVM == 'function') lookupVM = lookupVM(); 
                               currentModal[currentModal.internalUIs[0]] = null; 
                               var selectedItems = lookupVM.getSpecializedLookupItems(); 
                               if (vm.status() == 'C' && selectedItems != null && selectedItems.length > 1) { 
                                   var results = '';
                                   $.each(selectedItems, function (index, item) { results += (index == 0 ? '' : ',') + (typeof item[lookupFieldName] == 'function' ? item[lookupFieldName]() : item[lookupFieldName]).toString().trim() });
                                   results = '[' + results + ']'
                                   ownerReference[fieldToSearch](results);
                                   if (vm.entitySearchRange[ownerReference.typeName + fieldToSearch] === undefined)
                                       vm.entitySearchRange[ownerReference.typeName + fieldToSearch] = ko.observable(results);
                                   else vm.entitySearchRange[ownerReference.typeName + fieldToSearch](results);
                                   document.dispatchEvent(dataUpdateEvent);
                                   if (finished) finished(true, results);
                               }
                               else if (selectedItems.length > 0) { dataContext['finalizeAll' + lookupName](ownerReference, selectedItems, '', lookupInfo); }
                           }
                           if (finished) finished(r === 'Ok');
                       });
                   }
                   if (finished) finished(false);
               }
           };
           ownerReference.clearLookUp = function (lookupName) {
               return eval('dataContext.clear' + lookupName + '(ownerReference)');
           };
       }
       //#endregion Lookup Extended Methods
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
       ownerReference.namespace = 'Linx.Demo.BV.ExAutocomplete';
       ownerReference.myProperties = [ 'ComboboxCliente','DatetimeCliente','DecimalCliente','IdCliente','IdEstado','IntCliente','SmallIntCliente','StringCliente','StringEstado','IdNmCompleto' ];
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
    metadataStore.registerEntityTypeCtor("Cliente", null, ClienteInitializer);
    //#endregion Classes Map
    //#region Context Definition
    
    //#region Get LookUps
    
    var getLookUpTbnmmeioByEntitySearch = function (jEntitySearch, order, skip, take, direction, lookupField) {
        var query = EntityQuery.from('GetLookUpTbnmmeioByEntitySearch').noTracking(true);
        query = (direction === 'descending' ? query.orderByDesc(order) : query.orderBy(order));
    
        if ((typeof jEntitySearch !== 'undefined') && jEntitySearch !== null)
            query = query.withParameters({ propertyName: (isNullOrEmpty(lookupField) ? order : lookupField), jEntitySearch: jEntitySearch });
    
        if (take > 0)
           query = query.skip(skip).take(take);
        query = query.inlineCount(true);
    
        return manager.executeQuery(query)
        .fail(queryFailed);
    };
    
    var getLookUpTbnomeByEntitySearch = function (jEntitySearch, order, skip, take, direction, lookupField) {
        var query = EntityQuery.from('GetLookUpTbnomeByEntitySearch').noTracking(true);
        query = (direction === 'descending' ? query.orderByDesc(order) : query.orderBy(order));
    
        if ((typeof jEntitySearch !== 'undefined') && jEntitySearch !== null)
            query = query.withParameters({ propertyName: (isNullOrEmpty(lookupField) ? order : lookupField), jEntitySearch: jEntitySearch });
    
        if (take > 0)
           query = query.skip(skip).take(take);
        query = query.inlineCount(true);
    
        return manager.executeQuery(query)
        .fail(queryFailed);
    };
    
    var getLookUpTbsobrenmByEntitySearch = function (jEntitySearch, order, skip, take, direction, lookupField) {
        var query = EntityQuery.from('GetLookUpTbsobrenmByEntitySearch').noTracking(true);
        query = (direction === 'descending' ? query.orderByDesc(order) : query.orderBy(order));
    
        if ((typeof jEntitySearch !== 'undefined') && jEntitySearch !== null)
            query = query.withParameters({ propertyName: (isNullOrEmpty(lookupField) ? order : lookupField), jEntitySearch: jEntitySearch });
    
        if (take > 0)
           query = query.skip(skip).take(take);
        query = query.inlineCount(true);
    
        return manager.executeQuery(query)
        .fail(queryFailed);
    };
    
    var getLookUpEstadoByEntitySearch = function (jEntitySearch, order, skip, take, direction, lookupField) {
        var query = EntityQuery.from('GetLookUpEstadoByEntitySearch').noTracking(true);
        query = (direction === 'descending' ? query.orderByDesc(order) : query.orderBy(order));
    
        if ((typeof jEntitySearch !== 'undefined') && jEntitySearch !== null)
            query = query.withParameters({ propertyName: (isNullOrEmpty(lookupField) ? order : lookupField), jEntitySearch: jEntitySearch });
    
        if (take > 0)
           query = query.skip(skip).take(take);
        query = query.inlineCount(true);
    
        return manager.executeQuery(query)
        .fail(queryFailed);
    };
    
    var getLkpTbnmcompletoByEntitySearch = function (jEntitySearch, order, skip, take, direction, lookupField) {
        var query = EntityQuery.from('GetLkpTbnmcompletoByEntitySearch').noTracking(true);
        query = (direction === 'descending' ? query.orderByDesc(order) : query.orderBy(order));
    
        if ((typeof jEntitySearch !== 'undefined') && jEntitySearch !== null)
            query = query.withParameters({ propertyName: (isNullOrEmpty(lookupField) ? order : lookupField), jEntitySearch: jEntitySearch });
    
        if (take > 0)
           query = query.skip(skip).take(take);
        query = query.inlineCount(true);
    
        return manager.executeQuery(query)
        .fail(queryFailed);
    };
    
    var getLookUpClienteByEntitySearch = function (jEntitySearch, order, skip, take, direction, lookupField) {
        var query = EntityQuery.from('GetLookUpClienteByEntitySearch').noTracking(true);
        query = (direction === 'descending' ? query.orderByDesc(order) : query.orderBy(order));
    
        if ((typeof jEntitySearch !== 'undefined') && jEntitySearch !== null)
            query = query.withParameters({ propertyName: (isNullOrEmpty(lookupField) ? order : lookupField), jEntitySearch: jEntitySearch });
    
        if (take > 0)
           query = query.skip(skip).take(take);
        query = query.inlineCount(true);
    
        return manager.executeQuery(query)
        .fail(queryFailed);
    };
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
    
    var clearTbnmcompleto = function (idBandeiraRede, complete) {
        clearAll();
        resetSequence('Tbnmcompleto');
        var refTbnmcompleto = manager.createEntity('Tbnmcompleto', {}, breeze.EntityState.Unchanged);
        if (complete) complete({ results: [ refTbnmcompleto ] });
        return true;
    };
    
    var getTbnmcompleto = function (predicate, preserveCurrentState, noTracking) {
        if (!preserveCurrentState) clearAll();
        var query = EntityQuery.from('GetTbnmcompleto').noTracking(noTracking)
        .orderBy('idNomeCompleto asc')
        ;
    
        if ((typeof predicate !== 'undefined') && predicate !== null)
            query = query.where(predicate);
    
        return manager.executeQuery(query)
        .fail(queryFailed);
    };
    
    var getTbnmcompletoByEntitySearchNoAssociations = function (jEntitySearch, skip, take, returnInlineCount, preserveCurrentState, noTracking, orderByDef) {
        if (!preserveCurrentState) clearAll();
        var query = EntityQuery.from('GetTbnmcompletoByEntitySearchNoAssociations').noTracking(noTracking)
        .orderBy((isNullOrEmpty(orderByDef) ? 'idNomeCompleto asc' : orderByDef))
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
    
    var clearTesteCkbView = function (idBandeiraRede, complete) {
        clearAll();
        resetSequence('TesteCkbView');
        var refTesteCkbView = manager.createEntity('TesteCkbView', {}, breeze.EntityState.Unchanged);
        if (complete) complete({ results: [ refTesteCkbView ] });
        return true;
    };
    
    var getTesteCkbView = function (predicate, preserveCurrentState, noTracking) {
        if (!preserveCurrentState) clearAll();
        var query = EntityQuery.from('GetTesteCkbView').noTracking(noTracking)
        .orderBy('IdQualquer asc')
        ;
    
        if ((typeof predicate !== 'undefined') && predicate !== null)
            query = query.where(predicate);
    
        return manager.executeQuery(query)
        .fail(queryFailed);
    };
    
    var getTesteCkbViewByEntitySearchNoAssociations = function (jEntitySearch, skip, take, returnInlineCount, preserveCurrentState, noTracking, orderByDef) {
        if (!preserveCurrentState) clearAll();
        var query = EntityQuery.from('GetTesteCkbViewByEntitySearchNoAssociations').noTracking(noTracking)
        .orderBy((isNullOrEmpty(orderByDef) ? 'IdQualquer asc' : orderByDef))
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
    
    var clearCliente = function (idBandeiraRede, complete) {
        clearAll();
        resetSequence('Cliente');
        var refCliente = manager.createEntity('Cliente', {}, breeze.EntityState.Unchanged);
        if (complete) complete({ results: [ refCliente ] });
        return true;
    };
    
    var getCliente = function (predicate, preserveCurrentState, noTracking) {
        if (!preserveCurrentState) clearAll();
        var query = EntityQuery.from('GetCliente').noTracking(noTracking)
        .orderBy('IdCliente asc')
        ;
    
        if ((typeof predicate !== 'undefined') && predicate !== null)
            query = query.where(predicate);
    
        return manager.executeQuery(query)
        .fail(queryFailed);
    };
    
    var getClienteByEntitySearchNoAssociations = function (jEntitySearch, skip, take, returnInlineCount, preserveCurrentState, noTracking, orderByDef) {
        if (!preserveCurrentState) clearAll();
        var query = EntityQuery.from('GetClienteByEntitySearchNoAssociations').noTracking(noTracking)
        .orderBy((isNullOrEmpty(orderByDef) ? 'IdCliente asc' : orderByDef))
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
    // Define that the event name is 'ExAutocompleteContext_DataUpdate'.
    var contextUpdtEvt = 'ExAutocompleteContext_DataUpdate_' + getNewGuid();
    dataUpdateEvent.initEvent(contextUpdtEvt, true, true);
    
    //#region LookUps Finalizers
     var finalizeAllLookUpTbnmmeio = function (replaceTo, selectedElements, propertyName, lookupInfo) {
        if (!replaceTo || !selectedElements)
            return;
        if (!Array.isArray(selectedElements)) {
            selectedElements = [selectedElements];
        }
        isFinalizingLookup(true);
        if (!propertyName)
            propertyName = '';
        var isUsedOriginalRow = false;
        for (var i = 0; i < selectedElements.length; i++)
        {
            var selectedElement = selectedElements[i];
            if (propertyName === '' || propertyName === 'idnomeMeio') {
               if (selectedElement.hasOwnProperty('idnomeMeio') && replaceTo.hasOwnProperty('idnomeMeio'))
               {
                   setAbsoluteValue(replaceTo, 'idnomeMeio', getAbsoluteValue(selectedElement['idnomeMeio']));
               }
               else if (replaceTo.hasOwnProperty('idnomeMeio')) {
                   setAbsoluteValue(replaceTo, 'idnomeMeio', 0);
               }
            }
            if (propertyName === '' || propertyName === 'Nomedomeio') {
               if (selectedElement.hasOwnProperty('Nomedomeio') && replaceTo.hasOwnProperty('Nomedomeio'))
               {
                   setAbsoluteValue(replaceTo, 'Nomedomeio', getAbsoluteValue(selectedElement['Nomedomeio']));
               }
               else if (replaceTo.hasOwnProperty('Nomedomeio')) {
                   setAbsoluteValue(replaceTo, 'Nomedomeio', '');
               }
            }
            if (replaceTo.validatedlookupsArray && !replaceTo.validatedlookupsArray.contains('LookUpTbnmmeio'))
                replaceTo.validatedlookupsArray.push('LookUpTbnmmeio');
        }
        //Trigger context data update event
        if (replaceTo.isPOCO) vm.refreshCurrentBind();
        document.dispatchEvent(dataUpdateEvent);
        isFinalizingLookup(false);
    };
    
    function clearLookUpTbnmmeio(replaceTo) {
        if (!replaceTo)
            return;
        isClearingLookup(true);
        setAbsoluteValue(replaceTo, 'idnomeMeio', 0);
        setAbsoluteValue(replaceTo, 'Nomedomeio', '');
        isClearingLookup(false);
        //Trigger context data update event
        if (replaceTo.isPOCO) vm.refreshCurrentBind();
        setTimeout(function () {document.dispatchEvent(dataUpdateEvent);}, 100);
    }
     var finalizeAllLookUpTbnome = function (replaceTo, selectedElements, propertyName, lookupInfo) {
        if (!replaceTo || !selectedElements)
            return;
        if (!Array.isArray(selectedElements)) {
            selectedElements = [selectedElements];
        }
        isFinalizingLookup(true);
        if (!propertyName)
            propertyName = '';
        var isUsedOriginalRow = false;
        for (var i = 0; i < selectedElements.length; i++)
        {
            var selectedElement = selectedElements[i];
            if (propertyName === '' || propertyName === 'IdNome') {
               if (selectedElement.hasOwnProperty('IdNome') && replaceTo.hasOwnProperty('IdNome'))
               {
                   setAbsoluteValue(replaceTo, 'IdNome', getAbsoluteValue(selectedElement['IdNome']));
               }
               else if (replaceTo.hasOwnProperty('IdNome')) {
                   setAbsoluteValue(replaceTo, 'IdNome', null);
               }
            }
            if (propertyName === '' || propertyName === 'Nome') {
               if (selectedElement.hasOwnProperty('Nome') && replaceTo.hasOwnProperty('Nome'))
               {
                   setAbsoluteValue(replaceTo, 'Nome', getAbsoluteValue(selectedElement['Nome']));
               }
               else if (replaceTo.hasOwnProperty('Nome')) {
                   setAbsoluteValue(replaceTo, 'Nome', null);
               }
            }
            if (replaceTo.validatedlookupsArray && !replaceTo.validatedlookupsArray.contains('LookUpTbnome'))
                replaceTo.validatedlookupsArray.push('LookUpTbnome');
        }
        //Trigger context data update event
        if (replaceTo.isPOCO) vm.refreshCurrentBind();
        document.dispatchEvent(dataUpdateEvent);
        isFinalizingLookup(false);
    };
    
    function clearLookUpTbnome(replaceTo) {
        if (!replaceTo)
            return;
        isClearingLookup(true);
        setAbsoluteValue(replaceTo, 'IdNome', null);
        setAbsoluteValue(replaceTo, 'Nome', null);
        isClearingLookup(false);
        //Trigger context data update event
        if (replaceTo.isPOCO) vm.refreshCurrentBind();
        setTimeout(function () {document.dispatchEvent(dataUpdateEvent);}, 100);
    }
     var finalizeAllLookUpTbsobrenm = function (replaceTo, selectedElements, propertyName, lookupInfo) {
        if (!replaceTo || !selectedElements)
            return;
        if (!Array.isArray(selectedElements)) {
            selectedElements = [selectedElements];
        }
        isFinalizingLookup(true);
        if (!propertyName)
            propertyName = '';
        var isUsedOriginalRow = false;
        for (var i = 0; i < selectedElements.length; i++)
        {
            var selectedElement = selectedElements[i];
            if (propertyName === '' || propertyName === 'IdSobrenome') {
               if (selectedElement.hasOwnProperty('IdSobrenome') && replaceTo.hasOwnProperty('IdSobrenome'))
               {
                   setAbsoluteValue(replaceTo, 'IdSobrenome', getAbsoluteValue(selectedElement['IdSobrenome']));
               }
               else if (replaceTo.hasOwnProperty('IdSobrenome')) {
                   setAbsoluteValue(replaceTo, 'IdSobrenome', 0);
               }
            }
            if (propertyName === '' || propertyName === 'SobreNome') {
               if (selectedElement.hasOwnProperty('SobreNome') && replaceTo.hasOwnProperty('SobreNome'))
               {
                   setAbsoluteValue(replaceTo, 'SobreNome', getAbsoluteValue(selectedElement['SobreNome']));
               }
               else if (replaceTo.hasOwnProperty('SobreNome')) {
                   setAbsoluteValue(replaceTo, 'SobreNome', '');
               }
            }
            if (replaceTo.validatedlookupsArray && !replaceTo.validatedlookupsArray.contains('LookUpTbsobrenm'))
                replaceTo.validatedlookupsArray.push('LookUpTbsobrenm');
        }
        //Trigger context data update event
        if (replaceTo.isPOCO) vm.refreshCurrentBind();
        document.dispatchEvent(dataUpdateEvent);
        isFinalizingLookup(false);
    };
    
    function clearLookUpTbsobrenm(replaceTo) {
        if (!replaceTo)
            return;
        isClearingLookup(true);
        setAbsoluteValue(replaceTo, 'IdSobrenome', 0);
        setAbsoluteValue(replaceTo, 'SobreNome', '');
        isClearingLookup(false);
        //Trigger context data update event
        if (replaceTo.isPOCO) vm.refreshCurrentBind();
        setTimeout(function () {document.dispatchEvent(dataUpdateEvent);}, 100);
    }
     var finalizeAllLookUpCliente = function (replaceTo, selectedElements, propertyName, lookupInfo) {
        if (!replaceTo || !selectedElements)
            return;
        if (!Array.isArray(selectedElements)) {
            selectedElements = [selectedElements];
        }
        isFinalizingLookup(true);
        if (!propertyName)
            propertyName = '';
        var isUsedOriginalRow = false;
        for (var i = 0; i < selectedElements.length; i++)
        {
            var selectedElement = selectedElements[i];
            if (propertyName === '' || propertyName === 'IdCliente') {
               if (selectedElement.hasOwnProperty('IdCliente') && replaceTo.hasOwnProperty('IdCliente'))
               {
                   setAbsoluteValue(replaceTo, 'IdCliente', getAbsoluteValue(selectedElement['IdCliente']));
               }
               else if (replaceTo.hasOwnProperty('IdCliente')) {
                   setAbsoluteValue(replaceTo, 'IdCliente', 0);
               }
            }
            if (replaceTo.validatedlookupsArray && !replaceTo.validatedlookupsArray.contains('LookUpCliente'))
                replaceTo.validatedlookupsArray.push('LookUpCliente');
        }
        //Trigger context data update event
        if (replaceTo.isPOCO) vm.refreshCurrentBind();
        document.dispatchEvent(dataUpdateEvent);
        isFinalizingLookup(false);
    };
    
    function clearLookUpCliente(replaceTo) {
        if (!replaceTo)
            return;
        isClearingLookup(true);
        setAbsoluteValue(replaceTo, 'IdCliente', 0);
        isClearingLookup(false);
        //Trigger context data update event
        if (replaceTo.isPOCO) vm.refreshCurrentBind();
        setTimeout(function () {document.dispatchEvent(dataUpdateEvent);}, 100);
    }
     var finalizeAllLookUpEstado = function (replaceTo, selectedElements, propertyName, lookupInfo) {
        if (!replaceTo || !selectedElements)
            return;
        if (!Array.isArray(selectedElements)) {
            selectedElements = [selectedElements];
        }
        isFinalizingLookup(true);
        if (!propertyName)
            propertyName = '';
        var isUsedOriginalRow = false;
        for (var i = 0; i < selectedElements.length; i++)
        {
            var selectedElement = selectedElements[i];
            if (propertyName === '' || propertyName === 'IdEstado') {
               if (selectedElement.hasOwnProperty('IdEstado') && replaceTo.hasOwnProperty('IdEstado'))
               {
                   setAbsoluteValue(replaceTo, 'IdEstado', getAbsoluteValue(selectedElement['IdEstado']));
               }
               else if (replaceTo.hasOwnProperty('IdEstado')) {
                   setAbsoluteValue(replaceTo, 'IdEstado', null);
               }
            }
            if (propertyName === '' || propertyName === 'StringEstado') {
               if (selectedElement.hasOwnProperty('StringEstado') && replaceTo.hasOwnProperty('StringEstado'))
               {
                   setAbsoluteValue(replaceTo, 'StringEstado', getAbsoluteValue(selectedElement['StringEstado']));
               }
               else if (replaceTo.hasOwnProperty('StringEstado')) {
                   setAbsoluteValue(replaceTo, 'StringEstado', null);
               }
            }
            if (replaceTo.validatedlookupsArray && !replaceTo.validatedlookupsArray.contains('LookUpEstado'))
                replaceTo.validatedlookupsArray.push('LookUpEstado');
        }
        //Trigger context data update event
        if (replaceTo.isPOCO) vm.refreshCurrentBind();
        document.dispatchEvent(dataUpdateEvent);
        isFinalizingLookup(false);
    };
    
    function clearLookUpEstado(replaceTo) {
        if (!replaceTo)
            return;
        isClearingLookup(true);
        setAbsoluteValue(replaceTo, 'IdEstado', null);
        setAbsoluteValue(replaceTo, 'StringEstado', null);
        isClearingLookup(false);
        //Trigger context data update event
        if (replaceTo.isPOCO) vm.refreshCurrentBind();
        setTimeout(function () {document.dispatchEvent(dataUpdateEvent);}, 100);
    }
     var finalizeAllLkpTbnmcompleto = function (replaceTo, selectedElements, propertyName, lookupInfo) {
        if (!replaceTo || !selectedElements)
            return;
        if (!Array.isArray(selectedElements)) {
            selectedElements = [selectedElements];
        }
        isFinalizingLookup(true);
        if (!propertyName)
            propertyName = '';
        var isUsedOriginalRow = false;
        for (var i = 0; i < selectedElements.length; i++)
        {
            var selectedElement = selectedElements[i];
            if (propertyName === '' || propertyName === 'IdNmCompleto') {
               if (selectedElement.hasOwnProperty('idNomeCompleto') && replaceTo.hasOwnProperty('IdNmCompleto'))
               {
                   setAbsoluteValue(replaceTo, 'IdNmCompleto', getAbsoluteValue(selectedElement['idNomeCompleto']));
               }
               else if (replaceTo.hasOwnProperty('IdNmCompleto')) {
                   setAbsoluteValue(replaceTo, 'IdNmCompleto', 0);
               }
            }
            if (propertyName === '' || propertyName === 'StringCliente') {
               if (selectedElement.hasOwnProperty('NomeCompleto') && replaceTo.hasOwnProperty('StringCliente'))
               {
                   setAbsoluteValue(replaceTo, 'StringCliente', getAbsoluteValue(selectedElement['NomeCompleto']));
               }
               else if (replaceTo.hasOwnProperty('StringCliente')) {
                   setAbsoluteValue(replaceTo, 'StringCliente', null);
               }
            }
            if (replaceTo.validatedlookupsArray && !replaceTo.validatedlookupsArray.contains('LkpTbnmcompleto'))
                replaceTo.validatedlookupsArray.push('LkpTbnmcompleto');
        }
        //Trigger context data update event
        if (replaceTo.isPOCO) vm.refreshCurrentBind();
        document.dispatchEvent(dataUpdateEvent);
        isFinalizingLookup(false);
    };
    
    function clearLkpTbnmcompleto(replaceTo) {
        if (!replaceTo)
            return;
        isClearingLookup(true);
        setAbsoluteValue(replaceTo, 'IdNmCompleto', 0);
        setAbsoluteValue(replaceTo, 'StringCliente', null);
        isClearingLookup(false);
        //Trigger context data update event
        if (replaceTo.isPOCO) vm.refreshCurrentBind();
        setTimeout(function () {document.dispatchEvent(dataUpdateEvent);}, 100);
    }
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
               url: getServiceAddress('LinxDemoExAutocomplete/Save' + vm.rootDataTypeName),
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
    
    var createTbnmcompleto = function() {
        //Create entity instance
        enableChangeTrack = false;
        var entity = createEntity('Tbnmcompleto');
        entity.setDefaults();
        if (typeof entity.OnAdding == 'function') {
            if (!entity.OnAdding()) { dataContext.deleteEntity(entity); return; }
        }
        enableChangeTrack = true;
        return entity;
    };
    
    var createTesteCkbView = function() {
        //Create entity instance
        enableChangeTrack = false;
        var entity = createEntity('TesteCkbView', { NaoObrigatorio: false, Obrigatorio: false });
        entity.setDefaults();
        if (typeof entity.OnAdding == 'function') {
            if (!entity.OnAdding()) { dataContext.deleteEntity(entity); return; }
        }
        enableChangeTrack = true;
        return entity;
    };
    
    var createCliente = function() {
        //Create entity instance
        enableChangeTrack = false;
        var entity = createEntity('Cliente');
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
           url: getServiceAddress("LinxDemoExAutocomplete/GetReportDataSource"),
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
           url: getServiceAddress("LinxDemoExAutocomplete/GetTemplateReport"),
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
            createTbnmcompleto: createTbnmcompleto,
            createTesteCkbView: createTesteCkbView,
            createCliente: createCliente,
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
                getLookUpTbnmmeioByEntitySearch: getLookUpTbnmmeioByEntitySearch,
            getLookUpTbnomeByEntitySearch: getLookUpTbnomeByEntitySearch,
            getLookUpTbsobrenmByEntitySearch: getLookUpTbsobrenmByEntitySearch,
            getLookUpEstadoByEntitySearch: getLookUpEstadoByEntitySearch,
            getLkpTbnmcompletoByEntitySearch: getLkpTbnmcompletoByEntitySearch,
            getLookUpClienteByEntitySearch: getLookUpClienteByEntitySearch,
            getBmEntityProperties: getBmEntityProperties,
            clearTbnmcompleto: clearTbnmcompleto,
            getTbnmcompleto: getTbnmcompleto,
            getTbnmcompletoByEntitySearchNoAssociations: getTbnmcompletoByEntitySearchNoAssociations,
            clearTesteCkbView: clearTesteCkbView,
            getTesteCkbView: getTesteCkbView,
            getTesteCkbViewByEntitySearchNoAssociations: getTesteCkbViewByEntitySearchNoAssociations,
            clearCliente: clearCliente,
            getCliente: getCliente,
            getClienteByEntitySearchNoAssociations: getClienteByEntitySearchNoAssociations,
                finalizeAllLookUpTbnmmeio: finalizeAllLookUpTbnmmeio,
            clearLookUpTbnmmeio: clearLookUpTbnmmeio,
            finalizeAllLookUpTbnome: finalizeAllLookUpTbnome,
            clearLookUpTbnome: clearLookUpTbnome,
            finalizeAllLookUpTbsobrenm: finalizeAllLookUpTbsobrenm,
            clearLookUpTbsobrenm: clearLookUpTbsobrenm,
            finalizeAllLookUpCliente: finalizeAllLookUpCliente,
            clearLookUpCliente: clearLookUpCliente,
            finalizeAllLookUpEstado: finalizeAllLookUpEstado,
            clearLookUpEstado: clearLookUpEstado,
            finalizeAllLkpTbnmcompleto: finalizeAllLkpTbnmcompleto,
            clearLkpTbnmcompleto: clearLkpTbnmcompleto
        };
    loadParameters();
    return dataContext;
    //#endregion Context Definition
}
return result;
});
