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
       return getServiceAddress('LinxDemoMacrosEventosValidacoesOData');
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
    var controllerName = 'LinxDemoMacrosEventosValidacoes';
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
    entityNames.push('Arquivo');
    metadataInfo['Arquivo'] = [
        { key: 'NomeArquivo', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 0, isPartOfKey: true, headerText: 'Nome Arquivo', width: '250px', dataType: 'string', format: '', hidden: false, unbound: false, group: null }
    ];
    dataExportInfo['Arquivo'] = [ 
        { name: 'Arquivo', canExportMedia: true , canExportReport: true, actionExport: 'LinxDemoMacrosEventosValidacoes/GetArquivoToExcel', actionReport: 'LinxDemoMacrosEventosValidacoes/GetArquivoToReportXml', actionFeed: 'LinxDemoMacrosEventosValidacoesOData/Arquivo', actionName: 'LinxDemoMacrosEventosValidacoes/GetArquivoByEntitySearchNoAssociations', display: 'Arquivo',  metaData: function() { return metadataInfo['Arquivo']; } }
    ];
    entitylookUps.push('Arquivo');
    entitylookUps['Arquivo'] = [];
    entityNames.push('Pais');
    metadataInfo['Pais'] = [
        { key: 'ComboboxPais', isQbeZero: false, isDomain: true, domainName: 'LX_COMBOBOX_PAIS', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 3, isPartOfKey: false, headerText: 'Combobox Pais', width: '205px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'ComboboxPaisName', isDomain: true, domainName: 'LX_COMBOBOX_PAIS', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Combobox Pais (Name)', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null },
        { key: 'DatetimePais', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Datetime Pais', width: '205px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null },
        { key: 'DecimalPais', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 13, isPartOfKey: false, headerText: 'Decimal Pais', width: '192px', dataType: 'number', format: '0.00', hidden: false, unbound: false, group: null },
        { key: 'IdPais', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 12, isPartOfKey: true, headerText: 'Id Pais', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'StringPais', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'String Pais', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null }
    ];
    dataExportInfo['Pais'] = [ 
        { name: 'Pais', canExportMedia: true , canExportReport: true, actionExport: 'LinxDemoMacrosEventosValidacoes/GetPaisToExcel', actionReport: 'LinxDemoMacrosEventosValidacoes/GetPaisToReportXml', actionFeed: 'LinxDemoMacrosEventosValidacoesOData/Pais', actionName: 'LinxDemoMacrosEventosValidacoes/GetPaisByEntitySearchNoAssociations', display: 'Pais',  metaData: function() { return metadataInfo['Pais']; } }
        , { name: 'Estado', canExportMedia: true , canExportReport: true, actionExport: 'LinxDemoMacrosEventosValidacoes/GetEstadoParentCompositionToExcel', actionReport: 'LinxDemoMacrosEventosValidacoes/GetEstadoParentCompositionToReportXml', actionFeed: 'LinxDemoMacrosEventosValidacoesOData/EstadoParentComposition', actionName: 'LinxDemoMacrosEventosValidacoes/GetEstadoParentCompositionByEntitySearchNoAssociations', display: 'Estado',  metaData: function() { return metadataInfo['EstadoParentComposition']; } }
    ];
    entitylookUps.push('Pais');
    entitylookUps['Pais'] = [];
    entityNames.push('Estado');
    metadataInfo['Estado'] = [
        { key: 'ComboboxEstado', isQbeZero: false, isDomain: true, domainName: 'LX_COMBOBOX_ESTADO', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 3, isPartOfKey: false, headerText: 'Combobox Estado', width: '231px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'ComboboxEstadoName', isDomain: true, domainName: 'LX_COMBOBOX_ESTADO', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Combobox Estado (Name)', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null },
        { key: 'DecimalEstado', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 13, isPartOfKey: false, headerText: 'Decimal Estado', width: '218px', dataType: 'number', format: '0.00', hidden: false, unbound: false, group: null },
        { key: 'IdEstado', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 12, isPartOfKey: true, headerText: 'Id Estado', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IdPais', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Id Pais', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null }
    ];
    entityNames.push('EstadoParentComposition');
    metadataInfo['EstadoParentComposition'] = [
        { key: 'ComboboxEstado', isQbeZero: false, isDomain: true, domainName: 'LX_COMBOBOX_ESTADO', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 3, isPartOfKey: false, headerText: 'Combobox Estado', width: '231px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'ComboboxEstadoName', isDomain: true, domainName: 'LX_COMBOBOX_ESTADO', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Combobox Estado (Name)', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null },
        { key: 'DecimalEstado', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 13, isPartOfKey: false, headerText: 'Decimal Estado', width: '218px', dataType: 'number', format: '0.00', hidden: false, unbound: false, group: null },
        { key: 'IdEstado', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 12, isPartOfKey: true, headerText: 'Id Estado', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IdPais', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Id Pais', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'ComboboxPais', isQbeZero: false, isDomain: true, domainName: 'LX_COMBOBOX_PAIS', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 3, isPartOfKey: false, headerText: 'Combobox Pais', width: '205px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'ComboboxPaisName', isDomain: true, domainName: 'LX_COMBOBOX_PAIS', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Combobox Pais (Name)', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null },
        { key: 'DatetimePais', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Datetime Pais', width: '205px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null },
        { key: 'DecimalPais', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 13, isPartOfKey: false, headerText: 'Decimal Pais', width: '192px', dataType: 'number', format: '0.00', hidden: false, unbound: false, group: null },
        { key: 'StringPais', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'String Pais', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null }
    ];
    dataExportInfo['Estado'] = [ 
        { name: 'Estado', canExportMedia: true , canExportReport: true, actionExport: 'LinxDemoMacrosEventosValidacoes/GetEstadoToExcel', actionReport: 'LinxDemoMacrosEventosValidacoes/GetEstadoToReportXml', actionFeed: 'LinxDemoMacrosEventosValidacoesOData/Estado', actionName: 'LinxDemoMacrosEventosValidacoes/GetEstadoByEntitySearchNoAssociations', display: 'Estado',  metaData: function() { return metadataInfo['Estado']; } }
    ];
    entitylookUps.push('Estado');
    entitylookUps['Estado'] = [];
    entityNames.push('ValorVendas');
    metadataInfo['ValorVendas'] = [
        { key: 'Cliente', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: 'Cliente', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Cliente', width: '271px', dataType: 'string', format: '', hidden: false, unbound: false, group: null },
        { key: 'CodLoja', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: 'CodLoja', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Cod Loja', width: '271px', dataType: 'string', format: '', hidden: false, unbound: false, group: null },
        { key: 'Data', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: 'Data', lookupVisibleColumns: '', isRequired: true, maxLength: 0, isPartOfKey: false, headerText: 'Data', width: '120px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null },
        { key: 'IdBandeiraRede', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: 'IdBandeiraRede', lookupVisibleColumns: '', isRequired: true, maxLength: 0, isPartOfKey: true, headerText: 'Id Bandeira Rede', width: '271px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'Loja', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: 'Loja', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Loja', width: '271px', dataType: 'string', format: '', hidden: false, unbound: false, group: null },
        { key: 'QtdItemBruto', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 20, isPartOfKey: false, headerText: 'Qtd Item Bruto', width: '218px', dataType: 'number', format: '0.00', hidden: false, unbound: false, group: null },
        { key: 'VlrItemPago', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 20, isPartOfKey: false, headerText: 'Vlr Item Pago', width: '210px', dataType: 'number', format: '0.00', hidden: false, unbound: false, group: null }
    ];
    dataExportInfo['ValorVendas'] = [ 
        { name: 'ValorVendas', canExportMedia: true , canExportReport: true, actionExport: 'LinxDemoMacrosEventosValidacoes/GetValorVendasToExcel', actionReport: 'LinxDemoMacrosEventosValidacoes/GetValorVendasToReportXml', actionFeed: 'LinxDemoMacrosEventosValidacoesOData/ValorVendas', actionName: 'LinxDemoMacrosEventosValidacoes/GetValorVendasByEntitySearchNoAssociations', display: 'ValorVendas',  metaData: function() { return metadataInfo['ValorVendas']; } }
    ];
    entitylookUps.push('ValorVendas');
    entitylookUps['ValorVendas'] = [];
    entitylookUps['ValorVendas'].push('LookUpEntityAdapter1Cliente');
    lookUpNames.push('LookUpEntityAdapter1Cliente');
    metadataInfo['LookUpEntityAdapter1Cliente'] = [
        { key: 'Cliente', relatedKey: 'Cliente', maxLength: 0, isPartOfKey: true, headerText: 'Cliente', width: '250px', dataType: 'string', format: '', hidden: false, unbound: false, group: null }
    ];
    entitylookUps['ValorVendas'].push('LookUpEntityAdapter1CodLoja');
    lookUpNames.push('LookUpEntityAdapter1CodLoja');
    metadataInfo['LookUpEntityAdapter1CodLoja'] = [
        { key: 'CodLoja', relatedKey: 'CodLoja', maxLength: 0, isPartOfKey: true, headerText: 'Cod Loja', width: '250px', dataType: 'string', format: '', hidden: false, unbound: false, group: null }
    ];
    entitylookUps['ValorVendas'].push('LookUpEntityAdapter1Data');
    lookUpNames.push('LookUpEntityAdapter1Data');
    metadataInfo['LookUpEntityAdapter1Data'] = [
        { key: 'Data', relatedKey: 'Data', maxLength: 0, isPartOfKey: true, headerText: 'Data', width: '120px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null }
    ];
    entitylookUps['ValorVendas'].push('LookUpEntityAdapter1IdBandeiraRede');
    lookUpNames.push('LookUpEntityAdapter1IdBandeiraRede');
    metadataInfo['LookUpEntityAdapter1IdBandeiraRede'] = [
        { key: 'IdBandeiraRede', relatedKey: 'IdBandeiraRede', maxLength: 0, isPartOfKey: true, headerText: 'Id Bandeira Rede', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null }
    ];
    entitylookUps['ValorVendas'].push('LookUpEntityAdapter1Loja');
    lookUpNames.push('LookUpEntityAdapter1Loja');
    metadataInfo['LookUpEntityAdapter1Loja'] = [
        { key: 'Loja', relatedKey: 'Loja', maxLength: 0, isPartOfKey: true, headerText: 'Loja', width: '250px', dataType: 'string', format: '', hidden: false, unbound: false, group: null }
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
    
    // Configure Arquivo data type
    metadataStore.addEntityType({
    shortName: "Arquivo",
    namespace: "Linx.Demo.BV.MacrosEventosValidacoes",
    autoGeneratedKeyType: AutoGeneratedKeyType.None,
    dataProperties: {
    NomeArquivo: { dataType: DataType.String, isNullable: false, isPartOfKey: true, validators: [ Validator.hasValueValidator]  }
                    },
    navigationProperties: {
    // Returns collections of details and associates with Parent
                          }
    });
    lookUpProperties['Arquivo'] = {};
    var ArquivoInitializer = function (ownerReference, isPOCO) {
       ownerReference.RowDataId = (isPOCO === true ? getNextSequence('Arquivo') : ko.observable(getNextSequence('Arquivo')));
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
             var filterByKey = 'Arquivo{' + 'NomeArquivo#==#S' + getAbsoluteValue(ownerReference.NomeArquivo).toString() + '}';
             if (!ownerReference.isPOCO && ownerReference.entityAspect && !ownerReference.isDetached() && !ownerReference.isUnchanged()) ownerReference.entityAspect.setUnchanged();
             return dataContext.getArquivoByEntitySearchNoAssociations(filterByKey, 0, 0, false, true, ownerReference.isPOCO === true).then(querySucceeded);
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
       ownerReference.serverDataType['NomeArquivo'] = 'S';
       ownerReference.typeName = 'Arquivo';
       ownerReference.isPrimaryKey = function(propertyName) {
           var keys = [ 'NomeArquivo' ];
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
               if (typeof cacheElements[idxR].Arquivo !== 'function') { return; }
               else  if (cacheElements[idxR].Arquivo() != ownerReference) { cacheElements[idxR].Arquivo(ownerReference); }
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
       ownerReference.namespace = 'Linx.Demo.BV.MacrosEventosValidacoes';
       ownerReference.myProperties = [ 'NomeArquivo' ];
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
    metadataStore.registerEntityTypeCtor("Arquivo", null, ArquivoInitializer);
    
    // Configure Pais data type
    metadataStore.addEntityType({
    shortName: "Pais",
    namespace: "Linx.Demo.BV.MacrosEventosValidacoes",
    autoGeneratedKeyType: AutoGeneratedKeyType.Identity,
    dataProperties: {
    ComboboxPais: { dataType: DataType.Byte, isNullable: false, isPartOfKey: false, validators: [ Validator.hasValueValidator]  }
    ,ComboboxPaisName: { dataType: DataType.String, isNullable: false, isPartOfKey: false, validators: [] }
    ,DatetimePais: { dataType: DataType.DateTime, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,DecimalPais: { dataType: DataType.Decimal, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,IdPais: { dataType: DataType.Int32, isNullable: false, isPartOfKey: true, validators: [ Validator.hasValueValidator]  }
    ,StringPais: { dataType: DataType.String, maxLength: 50, isNullable: true, isPartOfKey: false, validators: [ Validator.maxLength( {maxLength: 50})]  }
    ,TableMedia: { dataType: DataType.String, isNullable: true, isPartOfKey: false, validators: []  }
                    },
    navigationProperties: {
    // Returns collections of details and associates with Parent
    EstadoList: { entityTypeName: "Estado:#Linx.Demo.BV.MacrosEventosValidacoes", isScalar: false, invForeignKeyNames: ["IdPais"], associationName: "FK_Pais_Estado" }
                          }
    });
    lookUpProperties['Pais'] = {};
    var PaisInitializer = function (ownerReference, isPOCO) {
       ownerReference.RowDataId = (isPOCO === true ? getNextSequence('Pais') : ko.observable(getNextSequence('Pais')));
       ownerReference.currentEstado = ko.observable(null);
       //Adjust details for a POCO reference
       if (isPOCO === true) {
           ownerReference.EstadoList = ko.observableArray(ownerReference.EstadoList);
       }
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
           if (noDetails !== true && ownerReference.EstadoList && ownerReference.EstadoList().length > 0) {
             var detailExpr = ownerReference.EstadoList()[0].getJExpression(listFilterRange, ['IdPais']);
             if (detailExpr === 'Error') return detailExpr;
             jExpression += detailExpr;
           }
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
               if (isNullOrEmpty(propertyName)) {
                   for (var i = 0; i < ownerReference.EstadoList().length; i++) {
                       var detail = ownerReference.EstadoList()[i];
                       errors = errors.concat(detail.getValidationErrors());
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
           if (loadDetails) {
               result.EstadoList = [];
               var sourceList = getAbsoluteValue(ownerReference.EstadoList);
               if (sourceList && sourceList.length > 0) {
                   for (var i = 0; i < sourceList.length; i++) {
                       if (['U', 'I', 'D'].indexOf(sourceList[i].ChangeState) >= 0) result.EstadoList.push(sourceList[i].getPrimitiveDTO(sourceList[i].ChangeState != 'D'));
                   }
               }
           }
           return result;
       };
       ownerReference.getAllDetailChanges = function() {
           var result = [];
           var _EstadoList = getAbsoluteValue(ownerReference.EstadoList);
           if (_EstadoList && _EstadoList.length > 0) {
               for (var i = 0; i < _EstadoList.length; i++) {
                   var detail = _EstadoList[i];
                   if (['U', 'I', 'D'].indexOf(detail.ChangeState) >= 0) {
                       result.push(detail);
                       result = result.concat(detail.getAllDetailChanges());
                   }
               }
           }
           return result;
       };
       ownerReference.copyDataFrom = function(originData, copyDetails) {
           enableChangeTrack = false;
           var properties = metadataInfo[ownerReference.typeName];
           for (var i = 0; i < properties.length; i++) {
                setAbsoluteValue(ownerReference, properties[i].key, getAbsoluteValue(originData[properties[i].key]));
           }
           if (copyDetails) {
               if (ownerReference.EstadoList && originData.EstadoList) {
                   var toList = getAbsoluteValue(ownerReference.EstadoList);
                   var fromList = getAbsoluteValue(originData.EstadoList);
                   for (var idxElem = toList.length - 1; idxElem >= 0; idxElem--) {
                      if (toList[idxElem].ChangeState === 'D') toList.splice(idxElem, 1);
                   }
                   for (var idxElem = toList.length - 1; idxElem >= 0; idxElem--) {
                          if (toList[idxElem].ChangeState !== 'N') {
                               var fromObj = _.where(fromList, { IdEstado: toList[idxElem]['IdEstado'] });
                               if (fromObj.length > 0) toList[idxElem].copyDataFrom(fromObj[0], true);
                          }
                   }
               }
           }
       enableChangeTrack = true;
       };
          ownerReference.commitDetailsVisualPendings = function() {
              vm.dataBind('EstadoList', true);
              if (ownerReference.currentEstado()) ownerReference.currentEstado().commitDetailsVisualPendings();
          }
          ownerReference.refreshData = function(noWait, succeeded) {
             var filterByKey = 'Pais{' + 'IdPais#==#I' + getAbsoluteValue(ownerReference.IdPais).toString() + '}';
             if (!ownerReference.isPOCO && ownerReference.entityAspect && !ownerReference.isDetached() && !ownerReference.isUnchanged()) ownerReference.entityAspect.setUnchanged();
             return dataContext.getPaisByEntitySearchNoAssociations(filterByKey, 0, 0, false, true, ownerReference.isPOCO === true).then(querySucceeded);
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
              breezeReference.EstadoIsLoaded = ownerReference.EstadoIsLoaded;
              for (var idx = 0; idx < ownerReference.EstadoList().length; idx++) {
                  var entity = ownerReference.EstadoList()[idx];
                  if (entity.isPOCO)  {
                      var newReference = createEntity(entity.typeName, entity.getPrimitiveDTO(), true);
                      entity.enableDetailsDataTack(newReference);
                  }
              }
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
       ownerReference.serverDataType['ComboboxPais'] = 'Y';
       ownerReference.serverDataType['DatetimePais'] = 'T';
       ownerReference.serverDataType['DecimalPais'] = 'D';
       ownerReference.serverDataType['IdPais'] = 'I';
       ownerReference.serverDataType['StringPais'] = 'S';
       ownerReference.typeName = 'Pais';
       ownerReference.isPrimaryKey = function(propertyName) {
           var keys = [ 'IdPais' ];
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
               if (typeof cacheElements[idxR].Pais !== 'function') { return; }
               else  if (cacheElements[idxR].Pais() != ownerReference) { cacheElements[idxR].Pais(ownerReference); }
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
           if (!isNullOrEmpty(ownerReference.EstadoList()) && ownerReference.EstadoList().length > 0) {
              var details = [].concat(ownerReference.EstadoList());
              for (var idx = 0; idx < details.length; idx++) {
                details[idx].delete();
              }
           }
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
       ownerReference.namespace = 'Linx.Demo.BV.MacrosEventosValidacoes';
       ownerReference.myProperties = [ 'ComboboxPais','DatetimePais','DecimalPais','IdPais','StringPais' ];
       ownerReference.queryRequiredProperties = {  };
       ownerReference.excludedFilters = [];
       ownerReference.getCurrentElements = function() {
           var result = [ ownerReference ];
       if (!isNullOrEmpty(ownerReference.currentEstado())) { result = result.concat(ownerReference.currentEstado().getCurrentElements()); }
           return result;
       };
       ownerReference.checkForSendingAllRowsToServer = function() {
       };
       ownerReference.GetJsWhereDetailRelationForEstado = function(customParentRelation) {
       return 'Estado{' + (!isNullOrEmpty(customParentRelation) ? customParentRelation : 'IdPais#==#' + ownerReference.serverDataType['IdPais'] + getAbsoluteValue(ownerReference.IdPais).toString()) + '}';    
       }
       ownerReference.EstadoIsLoaded = false;
       ownerReference.detailsLoaded = function() {
           return ownerReference.EstadoIsLoaded;
       }
       ownerReference.atLeastOneDetailLoaded = function() {
           return ownerReference.EstadoIsLoaded;
       }
       ownerReference.adjustDetailsLoaded = function(value) {
           ownerReference.EstadoIsLoaded = value;
           if (value === false && ownerReference.isPOCO)
               ownerReference.EstadoList([]);
       }
       ownerReference.fillDetails = function(force, detailName, noInnerUIs, noWait, callback, customParentRelation) {
          if (typeof force === 'undefined') force = false;
          if (force) vm.clearInnerUIs(ownerReference);
          if (!noInnerUIs) vm.queryInnerUIs(ownerReference);
          if (ownerReference.isAdded()) {
            ownerReference.EstadoIsLoaded = true;
          }
          var _EstadoRemoteComplete = false;
          var detachList_Estado = [];
          if (force) {
               if (isNullOrEmpty(detailName) || detailName == 'Estado') ownerReference.EstadoIsLoaded = false;
               if ((isNullOrEmpty(detailName) || detailName == 'Estado') && ownerReference.EstadoList && ownerReference.EstadoList().length > 0) {
                   if (ownerReference.isPOCO) {
                       ownerReference.EstadoList([]);
                   } else {
                       var detailList = ownerReference.EstadoList();
                       for (var idx = detailList.length - 1; idx >= 0; idx--) {
                           detachList_Estado.push(detailList[idx]);
                       }
                   }
               }
          }
    
          if (!ownerReference.EstadoIsLoaded) {
            //Load EstadoList
            if (isNullOrEmpty(detailName) || detailName === 'Estado') {
              ownerReference.EstadoIsLoaded = true;
              _EstadoRemoteComplete = (ownerReference.EstadoList && ownerReference.EstadoList().length > 0);
              if ((force || !ownerReference.EstadoList || ownerReference.EstadoList().length === 0) && (!isNullOrEmpty(getAbsoluteValue(ownerReference.IdPais)))) {
                var navQuery = EntityQuery.from('GetEstadoByEntitySearchNoAssociations').noTracking(ownerReference.isPOCO === true)
                .orderBy('IdEstado asc')
                    .withParameters({ jEntitySearch: ownerReference.GetJsWhereDetailRelationForEstado(customParentRelation) })    ;
                if (!vm.dataToolbar._noBusyLoading) vm.showProcessing('Pesquisando detalhes...');
                manager.executeQuery(navQuery).then(function (data) { if (ownerReference.isPOCO) { for (var idx = 0; idx < data.results.length; idx++) { initializePOCO(data.results[idx], 'Estado'); data.results[idx].Pais = ko.observable(ownerReference); } ownerReference.EstadoList(data.results); } 
                   if (!ownerReference.isPOCO && detachList_Estado.length > 0)
                   {
                       for (var idx = 0; idx < detachList_Estado.length; idx++)
                       {
                           if (!data.results.contains(detachList_Estado[idx]))
                               detachEntity(detachList_Estado[idx]);
                           else {
                               if (force && detachList_Estado[idx].atLeastOneDetailLoaded())
                                   detachList_Estado[idx].fillDetails(force, '', false, noWait);
                           }
                       }
                   }
                   ownerReference.setCurrentDetails('Estado'); notifyPresentation('EstadoList');
                   _EstadoRemoteComplete = true;
                   if (callback && (!isNullOrEmpty(detailName) || (_EstadoRemoteComplete))) { callback(); }
                }).fail(queryFailed).fin(function() { if (!vm.dataToolbar._noBusyLoading) vm.closeProcessing(); });
              } else { ownerReference.setCurrentDetails('Estado'); notifyPresentation('EstadoList'); }
            } else { _EstadoRemoteComplete = true; if (!ownerReference.EstadoIsLoaded && ownerReference.EstadoList && ownerReference.EstadoList().length > 0) { ownerReference.EstadoIsLoaded = true; ownerReference.setCurrentDetails('Estado'); } }
          } else { 
            if (isNullOrEmpty(detailName) || detailName == 'Estado') {
               notifyPresentation('EstadoList');
               ownerReference.setCurrentDetails('Estado');
            }
            _EstadoRemoteComplete = true;
          }
          if (callback && ((!isNullOrEmpty(detailName) && (eval('_' + detailName + 'RemoteComplete && ownerReference.' + detailName + 'IsLoaded') == true)) || (isNullOrEmpty(detailName) && (_EstadoRemoteComplete)))) { callback(); }
       };
       //Select first element as a current item of each detail
       ownerReference.setCurrentDetails = function(detailName, clearing) {
          if ((isNullOrEmpty(detailName) || detailName === 'Estado')) {
               if (ownerReference.EstadoList().length > 0) { ownerReference.currentEstado(ownerReference.EstadoList()[0]); if (clearing == null || clearing === false) ownerReference.currentEstado().fillDetails(); }
               else { ownerReference.currentEstado(null); ownerReference.notifyEmptyDetails('Estado'); }
          }
       };
       ownerReference.notifyEmptyDetails = function(detailName) {
          if (detailName === 'Estado') {
               notifyPresentation('EstadoList');
               vm.queryInnerUIs(null, 'Estado');
          }
       };
    //#region Extended Domain Names
       if (isPOCO !== true) {
           ownerReference.ComboboxPaisName.subscribe(
               function (newValue) {
                   if (newValue == null) { ownerReference.ComboboxPaisName(''); return; }
                   var value = (dataDomains.getId('LX_COMBOBOX_PAIS', newValue));
                   if (value != ownerReference.ComboboxPais()) {
                       ownerReference.ComboboxPais(value);
                   }
            });
    
           ownerReference.ComboboxPais.subscribe(
           function (newValue) {
                   if (newValue == null) { ownerReference.ComboboxPais(0); return; }
                   var value = dataDomains.getName('LX_COMBOBOX_PAIS', newValue);
                   if (value != ownerReference.ComboboxPaisName()) {
                       ownerReference.ComboboxPaisName(value);
               }
           });
       }
    //#endregion Extended Domain Names
    //#region Adjust details already loaded for a POCO reference
       if (isPOCO === true) {
           if ((typeof ownerReference.EstadoList === 'function') && ownerReference.EstadoList().length > 0) {
                for(var idx = 0; idx < ownerReference.EstadoList().length; idx++) { EstadoInitializer(ownerReference.EstadoList()[idx], isPOCO); }
           }
       }
    //#endregion Adjust details already loaded for a POCO reference
    };
    metadataStore.registerEntityTypeCtor("Pais", null, PaisInitializer);
    
    // Configure Estado data type
    metadataStore.addEntityType({
    shortName: "Estado",
    namespace: "Linx.Demo.BV.MacrosEventosValidacoes",
    autoGeneratedKeyType: AutoGeneratedKeyType.Identity,
    dataProperties: {
    ComboboxEstado: { dataType: DataType.Byte, isNullable: false, isPartOfKey: false, validators: [ Validator.hasValueValidator]  }
    ,ComboboxEstadoName: { dataType: DataType.String, isNullable: false, isPartOfKey: false, validators: [] }
    ,DecimalEstado: { dataType: DataType.Decimal, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,IdEstado: { dataType: DataType.Int32, isNullable: false, isPartOfKey: true, validators: [ Validator.hasValueValidator]  }
    ,IdPais: { dataType: DataType.Int32, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,TableMedia: { dataType: DataType.String, isNullable: true, isPartOfKey: false, validators: []  }
                    },
    navigationProperties: {
    // Returns a single parent and associates with Details
    Pais: { entityTypeName: "Pais:#Linx.Demo.BV.MacrosEventosValidacoes", isScalar: true, foreignKeyNames: ["IdPais"], associationName: "FK_Pais_Estado" }
    // Returns collections of details and associates with Parent
                          }
    });
    lookUpProperties['Estado'] = {};
    var EstadoInitializer = function (ownerReference, isPOCO) {
       ownerReference.RowDataId = (isPOCO === true ? getNextSequence('Estado') : ko.observable(getNextSequence('Estado')));
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
             var filterByKey = 'Estado{' + 'IdEstado#==#I' + getAbsoluteValue(ownerReference.IdEstado).toString() + '}';
             if (!ownerReference.isPOCO && ownerReference.entityAspect && !ownerReference.isDetached() && !ownerReference.isUnchanged()) ownerReference.entityAspect.setUnchanged();
             return dataContext.getEstadoByEntitySearchNoAssociations(filterByKey, 0, 0, false, true, ownerReference.isPOCO === true).then(querySucceeded);
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
       ownerReference.serverDataType['ComboboxEstado'] = 'Y';
       ownerReference.serverDataType['DecimalEstado'] = 'D';
       ownerReference.serverDataType['IdEstado'] = 'I';
       ownerReference.serverDataType['IdPais'] = 'I';
       ownerReference.typeName = 'Estado';
       ownerReference.isPrimaryKey = function(propertyName) {
           var keys = [ 'IdEstado' ];
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
               if (typeof cacheElements[idxR].Estado !== 'function') { return; }
               else  if (cacheElements[idxR].Estado() != ownerReference) { cacheElements[idxR].Estado(ownerReference); }
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
           var parent = getAbsoluteValue(ownerReference.Pais);
           if (ownerReference.entityAspect) ownerReference.entityAspect.setDeleted(); // mark for deletion
           if (parent && (typeof parent.setCurrentDetails === 'function') && (typeof parent.EstadoList === 'function') && parent.EstadoList().length == 0) parent.setCurrentDetails('Estado');
       };
       ownerReference.setParentAsModified = function() {
       var parent = getAbsoluteValue(ownerReference.Pais);
       if (parent) {
           if (parent.isUnchanged()) {
               parent.setModified(); 
           }
           parent.setParentAsModified();
       }
       };
       ownerReference.getParent = function() {
           return getAbsoluteValue(ownerReference.Pais);
       };
       ownerReference.getSelfList = function() {
           var parent = ownerReference.getParent();
           if (!isNullOrEmpty(parent)) {
               return getAbsoluteValue(parent.EstadoList);
           } else { return null; }
       };
       ownerReference.namespace = 'Linx.Demo.BV.MacrosEventosValidacoes';
       ownerReference.myProperties = [ 'ComboboxEstado','DecimalEstado','IdEstado','IdPais' ];
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
    //#region Extended Domain Names
       if (isPOCO !== true) {
           ownerReference.ComboboxEstadoName.subscribe(
               function (newValue) {
                   if (newValue == null) { ownerReference.ComboboxEstadoName(''); return; }
                   var value = (dataDomains.getId('LX_COMBOBOX_ESTADO', newValue));
                   if (value != ownerReference.ComboboxEstado()) {
                       ownerReference.ComboboxEstado(value);
                   }
            });
    
           ownerReference.ComboboxEstado.subscribe(
           function (newValue) {
                   if (newValue == null) { ownerReference.ComboboxEstado(0); return; }
                   var value = dataDomains.getName('LX_COMBOBOX_ESTADO', newValue);
                   if (value != ownerReference.ComboboxEstadoName()) {
                       ownerReference.ComboboxEstadoName(value);
               }
           });
       }
    //#endregion Extended Domain Names
    };
    metadataStore.registerEntityTypeCtor("Estado", null, EstadoInitializer);
    
    // Configure ValorVendas data type
    metadataStore.addEntityType({
    shortName: "ValorVendas",
    namespace: "Linx.Demo.BV.MacrosEventosValidacoes",
    autoGeneratedKeyType: AutoGeneratedKeyType.Identity,
    dataProperties: {
    Cliente: { dataType: DataType.String, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,CodLoja: { dataType: DataType.String, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,Data: { dataType: DataType.DateTime, isNullable: false, isPartOfKey: false, defaultValue: '', validators: [ Validator.hasValueValidator]  }
    ,IdBandeiraRede: { dataType: DataType.Int64, isNullable: false, isPartOfKey: true, validators: [ Validator.hasValueValidator]  }
    ,Loja: { dataType: DataType.String, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,QtdItemBruto: { dataType: DataType.Double, isNullable: false, isPartOfKey: false, validators: [ ]  }
    ,VlrItemPago: { dataType: DataType.Double, isNullable: false, isPartOfKey: false, validators: [ ]  }
                    },
    navigationProperties: {
    // Returns collections of details and associates with Parent
                          }
    });
    lookUpProperties['ValorVendas'] = {Cliente: 'LookUpEntityAdapter1Cliente', CodLoja: 'LookUpEntityAdapter1CodLoja', Data: 'LookUpEntityAdapter1Data', IdBandeiraRede: 'LookUpEntityAdapter1IdBandeiraRede', Loja: 'LookUpEntityAdapter1Loja'};
    var ValorVendasInitializer = function (ownerReference, isPOCO) {
       ownerReference.RowDataId = (isPOCO === true ? getNextSequence('ValorVendas') : ko.observable(getNextSequence('ValorVendas')));
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
             var filterByKey = 'ValorVendas{' + 'IdBandeiraRede#==#L' + getAbsoluteValue(ownerReference.IdBandeiraRede).toString() + '}';
             if (!ownerReference.isPOCO && ownerReference.entityAspect && !ownerReference.isDetached() && !ownerReference.isUnchanged()) ownerReference.entityAspect.setUnchanged();
             return dataContext.getValorVendasByEntitySearchNoAssociations(filterByKey, 0, 0, false, true, ownerReference.isPOCO === true).then(querySucceeded);
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
       ownerReference.serverDataType['Cliente'] = 'S';
       ownerReference.serverDataType['CodLoja'] = 'S';
       ownerReference.serverDataType['Data'] = 'T';
       ownerReference.serverDataType['IdBandeiraRede'] = 'L';
       ownerReference.serverDataType['Loja'] = 'S';
       ownerReference.serverDataType['QtdItemBruto'] = 'D';
       ownerReference.serverDataType['VlrItemPago'] = 'D';
       ownerReference.typeName = 'ValorVendas';
       ownerReference.isPrimaryKey = function(propertyName) {
           var keys = [ 'IdBandeiraRede' ];
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
           if (idBandeiraRede >= 0) setAbsoluteValue(ownerReference, 'IdBandeiraRede', idBandeiraRede);
       };
       ownerReference.setGpecon = function (idGpecon) {
       };
       ownerReference.UpdateIndependentRelation = function(detailName) {
           var cacheElements = dataContext.getEntities(detailName);
           for (var idxR = 0; idxR < cacheElements.length; idxR++) {
               if (typeof cacheElements[idxR].ValorVendas !== 'function') { return; }
               else  if (cacheElements[idxR].ValorVendas() != ownerReference) { cacheElements[idxR].ValorVendas(ownerReference); }
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
               if (lookupName === 'LookUpEntityAdapter1IdBandeiraRede') {
                   return ('IdBandeiraRede#' + (lookupInfo.vm.getBandeiraRede() === 0 && !isNullOrEmpty(lookupInfo.vm.getCurrentBrands()) ? 'In#S' : '==#I') + lookupInfo.vm.getCurrentBrands());
               }
               return '';
           };
        
           ownerReference.getLookupDisplay = function (lookupName) {
               var displayName = '';
               if (lookupName === 'LookUpEntityAdapter1Cliente') {
                   displayName = ' de Cliente';
               }
               if (lookupName === 'LookUpEntityAdapter1CodLoja') {
                   displayName = ' de CodLoja';
               }
               if (lookupName === 'LookUpEntityAdapter1Data') {
                   displayName = ' de Data';
               }
               if (lookupName === 'LookUpEntityAdapter1IdBandeiraRede') {
                   displayName = ' de IdBandeiraRede';
               }
               if (lookupName === 'LookUpEntityAdapter1Loja') {
                   displayName = ' de Loja';
               }
               return 'Seleção' + displayName;
           };
        
           ownerReference.getSpecializedLookup = function (lookupName, lookupInfo, fieldToSearch, valueToSearch, ownerReference, allowMultiSelectionInSearch) {
               var specializedLookup = '';
               return specializedLookup;
           };
        
           ownerReference.getSubQueryFilterFromLookUpEntityAdapter1Cliente = function (propertyName) {
               var filter = '';
               return filter;
           }
           ownerReference.getSubQueryFilterFromLookUpEntityAdapter1CodLoja = function (propertyName) {
               var filter = '';
               return filter;
           }
           ownerReference.getSubQueryFilterFromLookUpEntityAdapter1Data = function (propertyName) {
               var filter = '';
               return filter;
           }
           ownerReference.getSubQueryFilterFromLookUpEntityAdapter1IdBandeiraRede = function (propertyName) {
               var filter = '';
               return filter;
           }
           ownerReference.getSubQueryFilterFromLookUpEntityAdapter1Loja = function (propertyName) {
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
       ownerReference.namespace = 'Linx.Demo.BV.MacrosEventosValidacoes';
       ownerReference.myProperties = [ 'Cliente','CodLoja','Data','IdBandeiraRede','Loja','QtdItemBruto','VlrItemPago' ];
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
    metadataStore.registerEntityTypeCtor("ValorVendas", null, ValorVendasInitializer);
    //#endregion Classes Map
    //#region Context Definition
    
    //#region Get LookUps
    
    var getLookUpEntityAdapter1ClienteByEntitySearch = function (jEntitySearch, order, skip, take, direction, lookupField) {
        var query = EntityQuery.from('GetLookUpEntityAdapter1ClienteByEntitySearch').noTracking(true);
        query = (direction === 'descending' ? query.orderByDesc(order) : query.orderBy(order));
    
        if ((typeof jEntitySearch !== 'undefined') && jEntitySearch !== null)
            query = query.withParameters({ propertyName: (isNullOrEmpty(lookupField) ? order : lookupField), jEntitySearch: jEntitySearch });
    
        if (take > 0)
           query = query.skip(skip).take(take);
        query = query.inlineCount(true);
    
        return manager.executeQuery(query)
        .fail(queryFailed);
    };
    
    var getLookUpEntityAdapter1CodLojaByEntitySearch = function (jEntitySearch, order, skip, take, direction, lookupField) {
        var query = EntityQuery.from('GetLookUpEntityAdapter1CodLojaByEntitySearch').noTracking(true);
        query = (direction === 'descending' ? query.orderByDesc(order) : query.orderBy(order));
    
        if ((typeof jEntitySearch !== 'undefined') && jEntitySearch !== null)
            query = query.withParameters({ propertyName: (isNullOrEmpty(lookupField) ? order : lookupField), jEntitySearch: jEntitySearch });
    
        if (take > 0)
           query = query.skip(skip).take(take);
        query = query.inlineCount(true);
    
        return manager.executeQuery(query)
        .fail(queryFailed);
    };
    
    var getLookUpEntityAdapter1DataByEntitySearch = function (jEntitySearch, order, skip, take, direction, lookupField) {
        var query = EntityQuery.from('GetLookUpEntityAdapter1DataByEntitySearch').noTracking(true);
        query = (direction === 'descending' ? query.orderByDesc(order) : query.orderBy(order));
    
        if ((typeof jEntitySearch !== 'undefined') && jEntitySearch !== null)
            query = query.withParameters({ propertyName: (isNullOrEmpty(lookupField) ? order : lookupField), jEntitySearch: jEntitySearch });
    
        if (take > 0)
           query = query.skip(skip).take(take);
        query = query.inlineCount(true);
    
        return manager.executeQuery(query)
        .fail(queryFailed);
    };
    
    var getLookUpEntityAdapter1IdBandeiraRedeByEntitySearch = function (jEntitySearch, order, skip, take, direction, lookupField) {
        var query = EntityQuery.from('GetLookUpEntityAdapter1IdBandeiraRedeByEntitySearch').noTracking(true);
        query = (direction === 'descending' ? query.orderByDesc(order) : query.orderBy(order));
    
        if ((typeof jEntitySearch !== 'undefined') && jEntitySearch !== null)
            query = query.withParameters({ propertyName: (isNullOrEmpty(lookupField) ? order : lookupField), jEntitySearch: jEntitySearch });
    
        if (take > 0)
           query = query.skip(skip).take(take);
        query = query.inlineCount(true);
    
        return manager.executeQuery(query)
        .fail(queryFailed);
    };
    
    var getLookUpEntityAdapter1LojaByEntitySearch = function (jEntitySearch, order, skip, take, direction, lookupField) {
        var query = EntityQuery.from('GetLookUpEntityAdapter1LojaByEntitySearch').noTracking(true);
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
    
    var clearArquivo = function (idBandeiraRede, complete) {
        clearAll();
        resetSequence('Arquivo');
        var refArquivo = manager.createEntity('Arquivo', {}, breeze.EntityState.Unchanged);
        if (complete) complete({ results: [ refArquivo ] });
        return true;
    };
    
    var getArquivo = function (predicate, preserveCurrentState, noTracking) {
        if (!preserveCurrentState) clearAll();
        var query = EntityQuery.from('GetArquivo').noTracking(noTracking)
        .orderBy('NomeArquivo asc')
        ;
    
        if ((typeof predicate !== 'undefined') && predicate !== null)
            query = query.where(predicate);
    
        return manager.executeQuery(query)
        .fail(queryFailed);
    };
    
    var getArquivoByEntitySearchNoAssociations = function (jEntitySearch, skip, take, returnInlineCount, preserveCurrentState, noTracking, orderByDef) {
        if (!preserveCurrentState) clearAll();
        var query = EntityQuery.from('GetArquivoByEntitySearchNoAssociations').noTracking(noTracking)
        .orderBy((isNullOrEmpty(orderByDef) ? 'NomeArquivo asc' : orderByDef))
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
    
    var clearPais = function (idBandeiraRede, complete) {
        clearAll();
        resetSequence('Pais');
        var refPais = manager.createEntity('Pais', {}, breeze.EntityState.Unchanged);
        resetSequence('Estado');
        var refEstado = manager.createEntity('Estado', {}, breeze.EntityState.Unchanged);
        refPais.currentEstado(refEstado);
        if (complete) complete({ results: [ refPais ] });
        return true;
    };
    
    var getPais = function (predicate, preserveCurrentState, noTracking) {
        if (!preserveCurrentState) clearAll();
        var query = EntityQuery.from('GetPais').noTracking(noTracking)
        .orderBy('IdPais asc')
        ;
    
        if ((typeof predicate !== 'undefined') && predicate !== null)
            query = query.where(predicate);
    
        return manager.executeQuery(query)
        .fail(queryFailed);
    };
    
    var getPaisByEntitySearchNoAssociations = function (jEntitySearch, skip, take, returnInlineCount, preserveCurrentState, noTracking, orderByDef) {
        if (!preserveCurrentState) clearAll();
        var query = EntityQuery.from('GetPaisByEntitySearchNoAssociations').noTracking(noTracking)
        .orderBy((isNullOrEmpty(orderByDef) ? 'IdPais asc' : orderByDef))
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
    
    var clearEstado = function (idBandeiraRede, complete) {
        clearAll();
        resetSequence('Estado');
        var refEstado = manager.createEntity('Estado', {}, breeze.EntityState.Unchanged);
        if (complete) complete({ results: [ refEstado ] });
        return true;
    };
    
    var getEstado = function (predicate, preserveCurrentState, noTracking) {
        if (!preserveCurrentState) clearAll();
        var query = EntityQuery.from('GetEstado').noTracking(noTracking)
        .orderBy('IdEstado asc')
        ;
    
        if ((typeof predicate !== 'undefined') && predicate !== null)
            query = query.where(predicate);
    
        return manager.executeQuery(query)
        .fail(queryFailed);
    };
    
    var getEstadoByEntitySearchNoAssociations = function (jEntitySearch, skip, take, returnInlineCount, preserveCurrentState, noTracking, orderByDef) {
        if (!preserveCurrentState) clearAll();
        var query = EntityQuery.from('GetEstadoByEntitySearchNoAssociations').noTracking(noTracking)
        .orderBy((isNullOrEmpty(orderByDef) ? 'IdEstado asc' : orderByDef))
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
    
    var clearValorVendas = function (idBandeiraRede, complete) {
        clearAll();
        resetSequence('ValorVendas');
        var refValorVendas = manager.createEntity('ValorVendas', {}, breeze.EntityState.Unchanged);
        if (complete) complete({ results: [ refValorVendas ] });
        return true;
    };
    
    var getValorVendas = function (predicate, preserveCurrentState, noTracking) {
        if (!preserveCurrentState) clearAll();
        var query = EntityQuery.from('GetValorVendas').noTracking(noTracking)
        .orderBy('IdBandeiraRede asc')
        ;
    
        if ((typeof predicate !== 'undefined') && predicate !== null)
            query = query.where(predicate);
    
        return manager.executeQuery(query)
        .fail(queryFailed);
    };
    
    var getValorVendasByEntitySearchNoAssociations = function (jEntitySearch, skip, take, returnInlineCount, preserveCurrentState, noTracking, orderByDef) {
        if (!preserveCurrentState) clearAll();
        var query = EntityQuery.from('GetValorVendasByEntitySearchNoAssociations').noTracking(noTracking)
        .orderBy((isNullOrEmpty(orderByDef) ? 'IdBandeiraRede asc' : orderByDef))
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
    // Define that the event name is 'MacrosEventosValidacoesContext_DataUpdate'.
    var contextUpdtEvt = 'MacrosEventosValidacoesContext_DataUpdate_' + getNewGuid();
    dataUpdateEvent.initEvent(contextUpdtEvt, true, true);
    
    //#region LookUps Finalizers
     var finalizeAllLookUpEntityAdapter1Cliente = function (replaceTo, selectedElements, propertyName, lookupInfo) {
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
            if (propertyName === '' || propertyName === 'Cliente') {
               if (selectedElement.hasOwnProperty('Cliente') && replaceTo.hasOwnProperty('Cliente'))
               {
                   setAbsoluteValue(replaceTo, 'Cliente', getAbsoluteValue(selectedElement['Cliente']));
               }
               else if (replaceTo.hasOwnProperty('Cliente')) {
                   setAbsoluteValue(replaceTo, 'Cliente', null);
               }
            }
            if (replaceTo.validatedlookupsArray && !replaceTo.validatedlookupsArray.contains('LookUpEntityAdapter1Cliente'))
                replaceTo.validatedlookupsArray.push('LookUpEntityAdapter1Cliente');
        }
        //Trigger context data update event
        if (replaceTo.isPOCO) vm.refreshCurrentBind();
        document.dispatchEvent(dataUpdateEvent);
        isFinalizingLookup(false);
    };
    
    function clearLookUpEntityAdapter1Cliente(replaceTo) {
        if (!replaceTo)
            return;
        isClearingLookup(true);
        setAbsoluteValue(replaceTo, 'Cliente', null);
        isClearingLookup(false);
        //Trigger context data update event
        if (replaceTo.isPOCO) vm.refreshCurrentBind();
        setTimeout(function () {document.dispatchEvent(dataUpdateEvent);}, 100);
    }
     var finalizeAllLookUpEntityAdapter1CodLoja = function (replaceTo, selectedElements, propertyName, lookupInfo) {
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
            if (propertyName === '' || propertyName === 'CodLoja') {
               if (selectedElement.hasOwnProperty('CodLoja') && replaceTo.hasOwnProperty('CodLoja'))
               {
                   setAbsoluteValue(replaceTo, 'CodLoja', getAbsoluteValue(selectedElement['CodLoja']));
               }
               else if (replaceTo.hasOwnProperty('CodLoja')) {
                   setAbsoluteValue(replaceTo, 'CodLoja', null);
               }
            }
            if (replaceTo.validatedlookupsArray && !replaceTo.validatedlookupsArray.contains('LookUpEntityAdapter1CodLoja'))
                replaceTo.validatedlookupsArray.push('LookUpEntityAdapter1CodLoja');
        }
        //Trigger context data update event
        if (replaceTo.isPOCO) vm.refreshCurrentBind();
        document.dispatchEvent(dataUpdateEvent);
        isFinalizingLookup(false);
    };
    
    function clearLookUpEntityAdapter1CodLoja(replaceTo) {
        if (!replaceTo)
            return;
        isClearingLookup(true);
        setAbsoluteValue(replaceTo, 'CodLoja', null);
        isClearingLookup(false);
        //Trigger context data update event
        if (replaceTo.isPOCO) vm.refreshCurrentBind();
        setTimeout(function () {document.dispatchEvent(dataUpdateEvent);}, 100);
    }
     var finalizeAllLookUpEntityAdapter1Data = function (replaceTo, selectedElements, propertyName, lookupInfo) {
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
            if (propertyName === '' || propertyName === 'Data') {
               if (selectedElement.hasOwnProperty('Data') && replaceTo.hasOwnProperty('Data'))
               {
                   setAbsoluteValue(replaceTo, 'Data', getAbsoluteValue(selectedElement['Data']));
               }
               else if (replaceTo.hasOwnProperty('Data')) {
                   setAbsoluteValue(replaceTo, 'Data', getCurrentDate());
               }
            }
            if (replaceTo.validatedlookupsArray && !replaceTo.validatedlookupsArray.contains('LookUpEntityAdapter1Data'))
                replaceTo.validatedlookupsArray.push('LookUpEntityAdapter1Data');
        }
        //Trigger context data update event
        if (replaceTo.isPOCO) vm.refreshCurrentBind();
        document.dispatchEvent(dataUpdateEvent);
        isFinalizingLookup(false);
    };
    
    function clearLookUpEntityAdapter1Data(replaceTo) {
        if (!replaceTo)
            return;
        isClearingLookup(true);
        setAbsoluteValue(replaceTo, 'Data', getCurrentDate());
        isClearingLookup(false);
        //Trigger context data update event
        if (replaceTo.isPOCO) vm.refreshCurrentBind();
        setTimeout(function () {document.dispatchEvent(dataUpdateEvent);}, 100);
    }
     var finalizeAllLookUpEntityAdapter1IdBandeiraRede = function (replaceTo, selectedElements, propertyName, lookupInfo) {
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
            if (propertyName === '' || propertyName === 'IdBandeiraRede') {
               if (selectedElement.hasOwnProperty('IdBandeiraRede') && replaceTo.hasOwnProperty('IdBandeiraRede'))
               {
                   setAbsoluteValue(replaceTo, 'IdBandeiraRede', getAbsoluteValue(selectedElement['IdBandeiraRede']));
               }
               else if (replaceTo.hasOwnProperty('IdBandeiraRede')) {
                   setAbsoluteValue(replaceTo, 'IdBandeiraRede', 0);
               }
            }
            if (replaceTo.validatedlookupsArray && !replaceTo.validatedlookupsArray.contains('LookUpEntityAdapter1IdBandeiraRede'))
                replaceTo.validatedlookupsArray.push('LookUpEntityAdapter1IdBandeiraRede');
        }
        //Trigger context data update event
        if (replaceTo.isPOCO) vm.refreshCurrentBind();
        document.dispatchEvent(dataUpdateEvent);
        isFinalizingLookup(false);
    };
    
    function clearLookUpEntityAdapter1IdBandeiraRede(replaceTo) {
        if (!replaceTo)
            return;
        isClearingLookup(true);
        setAbsoluteValue(replaceTo, 'IdBandeiraRede', 0);
        isClearingLookup(false);
        //Trigger context data update event
        if (replaceTo.isPOCO) vm.refreshCurrentBind();
        setTimeout(function () {document.dispatchEvent(dataUpdateEvent);}, 100);
    }
     var finalizeAllLookUpEntityAdapter1Loja = function (replaceTo, selectedElements, propertyName, lookupInfo) {
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
            if (propertyName === '' || propertyName === 'Loja') {
               if (selectedElement.hasOwnProperty('Loja') && replaceTo.hasOwnProperty('Loja'))
               {
                   setAbsoluteValue(replaceTo, 'Loja', getAbsoluteValue(selectedElement['Loja']));
               }
               else if (replaceTo.hasOwnProperty('Loja')) {
                   setAbsoluteValue(replaceTo, 'Loja', null);
               }
            }
            if (replaceTo.validatedlookupsArray && !replaceTo.validatedlookupsArray.contains('LookUpEntityAdapter1Loja'))
                replaceTo.validatedlookupsArray.push('LookUpEntityAdapter1Loja');
        }
        //Trigger context data update event
        if (replaceTo.isPOCO) vm.refreshCurrentBind();
        document.dispatchEvent(dataUpdateEvent);
        isFinalizingLookup(false);
    };
    
    function clearLookUpEntityAdapter1Loja(replaceTo) {
        if (!replaceTo)
            return;
        isClearingLookup(true);
        setAbsoluteValue(replaceTo, 'Loja', null);
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
               url: getServiceAddress('LinxDemoMacrosEventosValidacoes/Save' + vm.rootDataTypeName),
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
    
    var createArquivo = function() {
        //Create entity instance
        enableChangeTrack = false;
        var entity = createEntity('Arquivo', { NomeArquivo: (-1 * getSequence('Arquivo')).toString() });
        entity.setDefaults();
        if (typeof entity.OnAdding == 'function') {
            if (!entity.OnAdding()) { dataContext.deleteEntity(entity); return; }
        }
        enableChangeTrack = true;
        return entity;
    };
    
    var createPais = function() {
        //Create entity instance
        enableChangeTrack = false;
        var entity = createEntity('Pais');
        entity.setDefaults();
        if (typeof entity.OnAdding == 'function') {
            if (!entity.OnAdding()) { dataContext.deleteEntity(entity); return; }
        }
        enableChangeTrack = true;
        return entity;
    };
    
    var createEstado = function(parent, noCurrent) {
        //Create entity instance
        enableChangeTrack = false;
        var entity = createEntity('Estado', { Pais: parent });
        entity.setDefaults();
        if (typeof entity.OnAdding == 'function') {
            if (!entity.OnAdding()) { dataContext.deleteEntity(entity); return; }
        }
        if (noCurrent !== true) parent.currentEstado(entity);
        if (parent && (typeof parent.setCurrentDetails === 'function') && (typeof parent.EstadoList === 'function') && parent.EstadoList().length == 0) parent.setCurrentDetails('Estado');
        if (entity.setParentAsModified) entity.setParentAsModified();
        enableChangeTrack = true;
        return entity;
    };
    
    var createValorVendas = function() {
        //Create entity instance
        enableChangeTrack = false;
        var entity = createEntity('ValorVendas');
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
           url: getServiceAddress("LinxDemoMacrosEventosValidacoes/GetReportDataSource"),
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
           url: getServiceAddress("LinxDemoMacrosEventosValidacoes/GetTemplateReport"),
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
            createArquivo: createArquivo,
            createPais: createPais,
            createEstado: createEstado,
            createValorVendas: createValorVendas,
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
                getLookUpEntityAdapter1ClienteByEntitySearch: getLookUpEntityAdapter1ClienteByEntitySearch,
            getLookUpEntityAdapter1CodLojaByEntitySearch: getLookUpEntityAdapter1CodLojaByEntitySearch,
            getLookUpEntityAdapter1DataByEntitySearch: getLookUpEntityAdapter1DataByEntitySearch,
            getLookUpEntityAdapter1IdBandeiraRedeByEntitySearch: getLookUpEntityAdapter1IdBandeiraRedeByEntitySearch,
            getLookUpEntityAdapter1LojaByEntitySearch: getLookUpEntityAdapter1LojaByEntitySearch,
            getBmEntityProperties: getBmEntityProperties,
            clearArquivo: clearArquivo,
            getArquivo: getArquivo,
            getArquivoByEntitySearchNoAssociations: getArquivoByEntitySearchNoAssociations,
            clearPais: clearPais,
            getPais: getPais,
            getPaisByEntitySearchNoAssociations: getPaisByEntitySearchNoAssociations,
            clearEstado: clearEstado,
            getEstado: getEstado,
            getEstadoByEntitySearchNoAssociations: getEstadoByEntitySearchNoAssociations,
            clearValorVendas: clearValorVendas,
            getValorVendas: getValorVendas,
            getValorVendasByEntitySearchNoAssociations: getValorVendasByEntitySearchNoAssociations,
                finalizeAllLookUpEntityAdapter1Cliente: finalizeAllLookUpEntityAdapter1Cliente,
            clearLookUpEntityAdapter1Cliente: clearLookUpEntityAdapter1Cliente,
            finalizeAllLookUpEntityAdapter1CodLoja: finalizeAllLookUpEntityAdapter1CodLoja,
            clearLookUpEntityAdapter1CodLoja: clearLookUpEntityAdapter1CodLoja,
            finalizeAllLookUpEntityAdapter1Data: finalizeAllLookUpEntityAdapter1Data,
            clearLookUpEntityAdapter1Data: clearLookUpEntityAdapter1Data,
            finalizeAllLookUpEntityAdapter1IdBandeiraRede: finalizeAllLookUpEntityAdapter1IdBandeiraRede,
            clearLookUpEntityAdapter1IdBandeiraRede: clearLookUpEntityAdapter1IdBandeiraRede,
            finalizeAllLookUpEntityAdapter1Loja: finalizeAllLookUpEntityAdapter1Loja,
            clearLookUpEntityAdapter1Loja: clearLookUpEntityAdapter1Loja
        };
    loadParameters();
    return dataContext;
    //#endregion Context Definition
}
return result;
});
