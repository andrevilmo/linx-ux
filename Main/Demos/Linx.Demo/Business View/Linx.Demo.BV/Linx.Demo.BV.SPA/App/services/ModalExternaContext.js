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
       return getServiceAddress('LinxDemoModalExternaOData');
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
    var controllerName = 'LinxDemoModalExterna';
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
    entityNames.push('Loja');
    metadataInfo['Loja'] = [
        { key: 'BigIntLoja', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 24, isPartOfKey: false, headerText: 'Big Int Loja', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'BitLoja', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Bit Loja', width: '140px', dataType: 'bool', format: 'checkbox', hidden: false, unbound: false, group: null },
        { key: 'ComboboxLoja', isQbeZero: false, isDomain: true, domainName: 'LX_COMBOBOX_LOJA', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 3, isPartOfKey: false, headerText: 'Combobox Loja', width: '205px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'ComboboxLojaName', isDomain: true, domainName: 'LX_COMBOBOX_LOJA', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Combobox Loja (Name)', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null },
        { key: 'DatetimeLoja', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Datetime Loja', width: '205px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null },
        { key: 'DecimalLoja', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 13, isPartOfKey: false, headerText: 'Decimal Loja', width: '192px', dataType: 'number', format: '0.00', hidden: false, unbound: false, group: null },
        { key: 'IdCidade', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: 'IdCidade', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Id Cidade', width: '271px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IdEstado', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: 'IdEstado', lookupVisibleColumns: 'IdEstado,IdPais', maxLength: 12, isPartOfKey: false, headerText: 'Id Estado', width: '271px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IdLoja', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 12, isPartOfKey: true, headerText: 'Id Loja', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IdPais', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: 'IdPais', lookupVisibleColumns: 'IdPais', maxLength: 12, isPartOfKey: false, headerText: 'Id Pais', width: '271px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IntLoja', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Int Loja', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'SmallIntLoja', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 6, isPartOfKey: false, headerText: 'Small Int Loja', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null }
    ];
    dataExportInfo['Loja'] = [ 
        { name: 'Loja', canExportMedia: true , canExportReport: true, actionExport: 'LinxDemoModalExterna/GetLojaToExcel', actionReport: 'LinxDemoModalExterna/GetLojaToReportXml', actionFeed: 'LinxDemoModalExternaOData/Loja', actionName: 'LinxDemoModalExterna/GetLojaByEntitySearchNoAssociations', display: 'Loja',  metaData: function() { return metadataInfo['Loja']; } }
    ];
    entitylookUps.push('Loja');
    entitylookUps['Loja'] = [];
    entitylookUps['Loja'].push('LookUpCidade');
    lookUpNames.push('LookUpCidade');
    metadataInfo['LookUpCidade'] = [
        { key: 'IdCidade', relatedKey: 'IdCidade', maxLength: 10, isPartOfKey: true, headerText: 'Id Cidade', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IdEstado', relatedKey: 'IdEstado', maxLength: 10, isPartOfKey: false, headerText: 'Id Estado', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IdPais', relatedKey: 'IdPais', maxLength: 10, isPartOfKey: false, headerText: 'Id Pais', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null }
    ];
    entityNames.push('Venda');
    metadataInfo['Venda'] = [
        { key: 'BigIntVenda', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 24, isPartOfKey: false, headerText: 'Big Int Venda', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'BitVenda', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Bit Venda', width: '153px', dataType: 'bool', format: 'checkbox', hidden: false, unbound: false, group: null },
        { key: 'ComboboxVenda', isQbeZero: false, isDomain: true, domainName: 'LX_VENDA', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 3, isPartOfKey: false, headerText: 'Combobox Venda', width: '218px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'ComboboxVendaName', isDomain: true, domainName: 'LX_VENDA', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Combobox Venda (Name)', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null },
        { key: 'DatetimeVenda', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Datetime Venda', width: '218px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null },
        { key: 'DecimalVenda', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 13, isPartOfKey: false, headerText: 'Decimal Venda', width: '205px', dataType: 'number', format: '0.00', hidden: false, unbound: false, group: null },
        { key: 'IdLoja', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: 'IdLoja', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Id Loja', width: '271px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IdVenda', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 12, isPartOfKey: true, headerText: 'Id Venda', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IdVendedor', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: 'IdVendedor', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Id Vendedor', width: '271px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IntVenda', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Int Venda', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'SmallIntVenda', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 6, isPartOfKey: false, headerText: 'Small Int Venda', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null }
    ];
    dataExportInfo['Venda'] = [ 
        { name: 'Venda', canExportMedia: true , canExportReport: true, actionExport: 'LinxDemoModalExterna/GetVendaToExcel', actionReport: 'LinxDemoModalExterna/GetVendaToReportXml', actionFeed: 'LinxDemoModalExternaOData/Venda', actionName: 'LinxDemoModalExterna/GetVendaByEntitySearchNoAssociations', display: 'Venda',  metaData: function() { return metadataInfo['Venda']; } }
    ];
    entitylookUps.push('Venda');
    entitylookUps['Venda'] = [];
    entitylookUps['Venda'].push('LookUpLoja');
    lookUpNames.push('LookUpLoja');
    metadataInfo['LookUpLoja'] = [
        { key: 'IdLoja', relatedKey: 'IdLoja', maxLength: 10, isPartOfKey: true, headerText: 'Id Loja', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null }
    ];
    entitylookUps['Venda'].push('LookUpVendedor');
    lookUpNames.push('LookUpVendedor');
    metadataInfo['LookUpVendedor'] = [
        { key: 'IdVendedor', relatedKey: 'IdVendedor', maxLength: 10, isPartOfKey: true, headerText: 'Id Vendedor', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null }
    ];
    entityNames.push('Pais');
    metadataInfo['Pais'] = [
        { key: 'ComboboxPais', isQbeZero: false, isDomain: true, domainName: 'LX_COMBOBOX_PAIS', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 3, isPartOfKey: false, headerText: 'Combobox Pais', width: '205px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'ComboboxPaisName', isDomain: true, domainName: 'LX_COMBOBOX_PAIS', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Combobox Pais (Name)', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null },
        { key: 'DecimalPais', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 13, isPartOfKey: false, headerText: 'Decimal Pais', width: '192px', dataType: 'number', format: '0.00', hidden: false, unbound: false, group: null },
        { key: 'IdPais', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 12, isPartOfKey: true, headerText: 'Id Pais', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'StringPais', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'String Pais', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null }
    ];
    dataExportInfo['Pais'] = [ 
        { name: 'Pais', canExportMedia: true , canExportReport: true, actionExport: 'LinxDemoModalExterna/GetPaisToExcel', actionReport: 'LinxDemoModalExterna/GetPaisToReportXml', actionFeed: 'LinxDemoModalExternaOData/Pais', actionName: 'LinxDemoModalExterna/GetPaisByEntitySearchNoAssociations', display: 'Pais',  metaData: function() { return metadataInfo['Pais']; } }
    ];
    entitylookUps.push('Pais');
    entitylookUps['Pais'] = [];
    entityNames.push('Estado');
    metadataInfo['Estado'] = [
        { key: 'ComboboxEstado', isQbeZero: false, isDomain: true, domainName: 'LX_COMBOBOX_ESTADO', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 3, isPartOfKey: false, headerText: 'Combobox Estado', width: '231px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'ComboboxEstadoName', isDomain: true, domainName: 'LX_COMBOBOX_ESTADO', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Combobox Estado (Name)', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null },
        { key: 'DatetimeEstado', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Datetime Estado', width: '231px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null },
        { key: 'DecimalEstado', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 13, isPartOfKey: false, headerText: 'Decimal Estado', width: '218px', dataType: 'number', format: '0.00', hidden: false, unbound: false, group: null },
        { key: 'IdEstado', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 12, isPartOfKey: true, headerText: 'Id Estado', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IdPais', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Id Pais', width: '271px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'StringEstado', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'String Estado', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null }
    ];
    dataExportInfo['Estado'] = [ 
        { name: 'Estado', canExportMedia: true , canExportReport: true, actionExport: 'LinxDemoModalExterna/GetEstadoToExcel', actionReport: 'LinxDemoModalExterna/GetEstadoToReportXml', actionFeed: 'LinxDemoModalExternaOData/Estado', actionName: 'LinxDemoModalExterna/GetEstadoByEntitySearchNoAssociations', display: 'Estado',  metaData: function() { return metadataInfo['Estado']; } }
    ];
    entitylookUps.push('Estado');
    entitylookUps['Estado'] = [];
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
    
    // Configure Loja data type
    metadataStore.addEntityType({
    shortName: "Loja",
    namespace: "Linx.Demo.BV.ModalExterna",
    autoGeneratedKeyType: AutoGeneratedKeyType.Identity,
    dataProperties: {
    BigIntLoja: { dataType: DataType.Int64, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,BitLoja: { dataType: DataType.Boolean, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,ComboboxLoja: { dataType: DataType.Byte, isNullable: false, isPartOfKey: false, validators: [ Validator.hasValueValidator]  }
    ,ComboboxLojaName: { dataType: DataType.String, isNullable: false, isPartOfKey: false, validators: [] }
    ,DatetimeLoja: { dataType: DataType.DateTime, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,DecimalLoja: { dataType: DataType.Decimal, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,IdCidade: { dataType: DataType.Int32, isNullable: false, isPartOfKey: false, validators: [ ]  }
    ,IdEstado: { dataType: DataType.Int32, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,IdLoja: { dataType: DataType.Int32, isNullable: false, isPartOfKey: true, validators: [ Validator.hasValueValidator]  }
    ,IdPais: { dataType: DataType.Int32, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,IntLoja: { dataType: DataType.Int32, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,SmallIntLoja: { dataType: DataType.Int16, isNullable: true, isPartOfKey: false, validators: [ ]  }
                    },
    navigationProperties: {
    // Returns collections of details and associates with Parent
                          }
    });
    lookUpProperties['Loja'] = {IdCidade: 'LookUpCidade', IdEstado: 'LookUpCidade', IdPais: 'LookUpCidade'};
    var LojaInitializer = function (ownerReference, isPOCO) {
       ownerReference.RowDataId = (isPOCO === true ? getNextSequence('Loja') : ko.observable(getNextSequence('Loja')));
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
             var filterByKey = 'Loja{' + 'IdLoja#==#I' + getAbsoluteValue(ownerReference.IdLoja).toString() + '}';
             if (!ownerReference.isPOCO && ownerReference.entityAspect && !ownerReference.isDetached() && !ownerReference.isUnchanged()) ownerReference.entityAspect.setUnchanged();
             return dataContext.getLojaByEntitySearchNoAssociations(filterByKey, 0, 0, false, true, ownerReference.isPOCO === true).then(querySucceeded);
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
       ownerReference.serverDataType['BigIntLoja'] = 'L';
       ownerReference.serverDataType['BitLoja'] = 'B';
       ownerReference.serverDataType['ComboboxLoja'] = 'Y';
       ownerReference.serverDataType['DatetimeLoja'] = 'T';
       ownerReference.serverDataType['DecimalLoja'] = 'D';
       ownerReference.serverDataType['IdCidade'] = 'I';
       ownerReference.serverDataType['IdEstado'] = 'I';
       ownerReference.serverDataType['IdLoja'] = 'I';
       ownerReference.serverDataType['IdPais'] = 'I';
       ownerReference.serverDataType['IntLoja'] = 'I';
       ownerReference.serverDataType['SmallIntLoja'] = 'H';
       ownerReference.typeName = 'Loja';
       ownerReference.isPrimaryKey = function(propertyName) {
           var keys = [ 'IdLoja' ];
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
               if (typeof cacheElements[idxR].Loja !== 'function') { return; }
               else  if (cacheElements[idxR].Loja() != ownerReference) { cacheElements[idxR].Loja(ownerReference); }
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
               if (lookupName === 'LookUpCidade') {
                   displayName = ' de Cidade';
               }
               return 'Seleção' + displayName;
           };
        
           ownerReference.getSpecializedLookup = function (lookupName, lookupInfo, fieldToSearch, valueToSearch, ownerReference, allowMultiSelectionInSearch) {
               var specializedLookup = '';
               return specializedLookup;
           };
        
           ownerReference.getSubQueryFilterFromLookUpCidade = function (propertyName) {
               var filter = '';
               if (propertyName === 'IdCidade') {
                   var _IdEstado = getAbsoluteValue(this.IdEstado);
                   if (!isNullOrEmpty(_IdEstado)) { filter += (filter === '' ? '' : ';') + 'IdEstado' + (_IdEstado.toString().indexOf('[') > -1 ? '#In#S' : '#==#I') + _IdEstado.toString().replaceAll('[', '').replaceAll(']', ''); }
                   var _IdPais = getAbsoluteValue(this.IdPais);
                   if (!isNullOrEmpty(_IdPais)) { filter += (filter === '' ? '' : ';') + 'IdPais' + (_IdPais.toString().indexOf('[') > -1 ? '#In#S' : '#==#I') + _IdPais.toString().replaceAll('[', '').replaceAll(']', ''); }
               }
               if (propertyName === 'IdEstado') {
                   var _IdPais = getAbsoluteValue(this.IdPais);
                   if (!isNullOrEmpty(_IdPais)) { filter += (filter === '' ? '' : ';') + 'IdPais' + (_IdPais.toString().indexOf('[') > -1 ? '#In#S' : '#==#I') + _IdPais.toString().replaceAll('[', '').replaceAll(']', ''); }
               }
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
       ownerReference.namespace = 'Linx.Demo.BV.ModalExterna';
       ownerReference.myProperties = [ 'BigIntLoja','BitLoja','ComboboxLoja','DatetimeLoja','DecimalLoja','IdCidade','IdEstado','IdLoja','IdPais','IntLoja','SmallIntLoja' ];
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
           ownerReference.ComboboxLojaName.subscribe(
               function (newValue) {
                   if (newValue == null) { ownerReference.ComboboxLojaName(''); return; }
                   var value = (dataDomains.getId('LX_COMBOBOX_LOJA', newValue));
                   if (value != ownerReference.ComboboxLoja()) {
                       ownerReference.ComboboxLoja(value);
                   }
            });
    
           ownerReference.ComboboxLoja.subscribe(
           function (newValue) {
                   if (newValue == null) { ownerReference.ComboboxLoja(0); return; }
                   var value = dataDomains.getName('LX_COMBOBOX_LOJA', newValue);
                   if (value != ownerReference.ComboboxLojaName()) {
                       ownerReference.ComboboxLojaName(value);
               }
           });
       }
    //#endregion Extended Domain Names
    };
    metadataStore.registerEntityTypeCtor("Loja", null, LojaInitializer);
    
    // Configure Venda data type
    metadataStore.addEntityType({
    shortName: "Venda",
    namespace: "Linx.Demo.BV.ModalExterna",
    autoGeneratedKeyType: AutoGeneratedKeyType.Identity,
    dataProperties: {
    BigIntVenda: { dataType: DataType.Int64, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,BitVenda: { dataType: DataType.Boolean, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,ComboboxVenda: { dataType: DataType.Byte, isNullable: false, isPartOfKey: false, validators: [ Validator.hasValueValidator]  }
    ,ComboboxVendaName: { dataType: DataType.String, isNullable: false, isPartOfKey: false, validators: [] }
    ,DatetimeVenda: { dataType: DataType.DateTime, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,DecimalVenda: { dataType: DataType.Decimal, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,IdLoja: { dataType: DataType.Int32, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,IdVenda: { dataType: DataType.Int32, isNullable: false, isPartOfKey: true, validators: [ Validator.hasValueValidator]  }
    ,IdVendedor: { dataType: DataType.Int32, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,IntVenda: { dataType: DataType.Int32, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,SmallIntVenda: { dataType: DataType.Int16, isNullable: true, isPartOfKey: false, validators: [ ]  }
                    },
    navigationProperties: {
    // Returns collections of details and associates with Parent
                          }
    });
    lookUpProperties['Venda'] = {IdLoja: 'LookUpLoja', IdVendedor: 'LookUpVendedor'};
    var VendaInitializer = function (ownerReference, isPOCO) {
       ownerReference.RowDataId = (isPOCO === true ? getNextSequence('Venda') : ko.observable(getNextSequence('Venda')));
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
             var filterByKey = 'Venda{' + 'IdVenda#==#I' + getAbsoluteValue(ownerReference.IdVenda).toString() + '}';
             if (!ownerReference.isPOCO && ownerReference.entityAspect && !ownerReference.isDetached() && !ownerReference.isUnchanged()) ownerReference.entityAspect.setUnchanged();
             return dataContext.getVendaByEntitySearchNoAssociations(filterByKey, 0, 0, false, true, ownerReference.isPOCO === true).then(querySucceeded);
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
       ownerReference.serverDataType['BigIntVenda'] = 'L';
       ownerReference.serverDataType['BitVenda'] = 'B';
       ownerReference.serverDataType['ComboboxVenda'] = 'Y';
       ownerReference.serverDataType['DatetimeVenda'] = 'T';
       ownerReference.serverDataType['DecimalVenda'] = 'D';
       ownerReference.serverDataType['IdLoja'] = 'I';
       ownerReference.serverDataType['IdVenda'] = 'I';
       ownerReference.serverDataType['IdVendedor'] = 'I';
       ownerReference.serverDataType['IntVenda'] = 'I';
       ownerReference.serverDataType['SmallIntVenda'] = 'H';
       ownerReference.typeName = 'Venda';
       ownerReference.isPrimaryKey = function(propertyName) {
           var keys = [ 'IdVenda' ];
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
               if (typeof cacheElements[idxR].Venda !== 'function') { return; }
               else  if (cacheElements[idxR].Venda() != ownerReference) { cacheElements[idxR].Venda(ownerReference); }
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
               if (lookupName === 'LookUpLoja') {
                   displayName = ' de Loja';
               }
               if (lookupName === 'LookUpVendedor') {
                   displayName = ' de Vendedor';
               }
               return 'Seleção' + displayName;
           };
        
           ownerReference.getSpecializedLookup = function (lookupName, lookupInfo, fieldToSearch, valueToSearch, ownerReference, allowMultiSelectionInSearch) {
               var specializedLookup = '';
               return specializedLookup;
           };
        
           ownerReference.getSubQueryFilterFromLookUpLoja = function (propertyName) {
               var filter = '';
               return filter;
           }
           ownerReference.getSubQueryFilterFromLookUpVendedor = function (propertyName) {
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
       ownerReference.namespace = 'Linx.Demo.BV.ModalExterna';
       ownerReference.myProperties = [ 'BigIntVenda','BitVenda','ComboboxVenda','DatetimeVenda','DecimalVenda','IdLoja','IdVenda','IdVendedor','IntVenda','SmallIntVenda' ];
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
           ownerReference.ComboboxVendaName.subscribe(
               function (newValue) {
                   if (newValue == null) { ownerReference.ComboboxVendaName(''); return; }
                   var value = (dataDomains.getId('LX_VENDA', newValue));
                   if (value != ownerReference.ComboboxVenda()) {
                       ownerReference.ComboboxVenda(value);
                   }
            });
    
           ownerReference.ComboboxVenda.subscribe(
           function (newValue) {
                   if (newValue == null) { ownerReference.ComboboxVenda(0); return; }
                   var value = dataDomains.getName('LX_VENDA', newValue);
                   if (value != ownerReference.ComboboxVendaName()) {
                       ownerReference.ComboboxVendaName(value);
               }
           });
       }
    //#endregion Extended Domain Names
    };
    metadataStore.registerEntityTypeCtor("Venda", null, VendaInitializer);
    
    // Configure Pais data type
    metadataStore.addEntityType({
    shortName: "Pais",
    namespace: "Linx.Demo.BV.ModalExterna",
    autoGeneratedKeyType: AutoGeneratedKeyType.Identity,
    dataProperties: {
    ComboboxPais: { dataType: DataType.Byte, isNullable: false, isPartOfKey: false, validators: [ Validator.hasValueValidator]  }
    ,ComboboxPaisName: { dataType: DataType.String, isNullable: false, isPartOfKey: false, validators: [] }
    ,DecimalPais: { dataType: DataType.Decimal, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,IdPais: { dataType: DataType.Int32, isNullable: false, isPartOfKey: true, validators: [ Validator.hasValueValidator]  }
    ,StringPais: { dataType: DataType.String, maxLength: 50, isNullable: true, isPartOfKey: false, validators: [ Validator.maxLength( {maxLength: 50})]  }
                    },
    navigationProperties: {
    // Returns collections of details and associates with Parent
                          }
    });
    lookUpProperties['Pais'] = {};
    var PaisInitializer = function (ownerReference, isPOCO) {
       ownerReference.RowDataId = (isPOCO === true ? getNextSequence('Pais') : ko.observable(getNextSequence('Pais')));
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
       ownerReference.namespace = 'Linx.Demo.BV.ModalExterna';
       ownerReference.myProperties = [ 'ComboboxPais','DecimalPais','IdPais','StringPais' ];
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
    };
    metadataStore.registerEntityTypeCtor("Pais", null, PaisInitializer);
    
    // Configure Estado data type
    metadataStore.addEntityType({
    shortName: "Estado",
    namespace: "Linx.Demo.BV.ModalExterna",
    autoGeneratedKeyType: AutoGeneratedKeyType.Identity,
    dataProperties: {
    ComboboxEstado: { dataType: DataType.Byte, isNullable: false, isPartOfKey: false, validators: [ Validator.hasValueValidator]  }
    ,ComboboxEstadoName: { dataType: DataType.String, isNullable: false, isPartOfKey: false, validators: [] }
    ,DatetimeEstado: { dataType: DataType.DateTime, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,DecimalEstado: { dataType: DataType.Decimal, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,IdEstado: { dataType: DataType.Int32, isNullable: false, isPartOfKey: true, validators: [ Validator.hasValueValidator]  }
    ,IdPais: { dataType: DataType.Int32, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,StringEstado: { dataType: DataType.String, maxLength: 50, isNullable: true, isPartOfKey: false, validators: [ Validator.maxLength( {maxLength: 50})]  }
                    },
    navigationProperties: {
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
       ownerReference.serverDataType['DatetimeEstado'] = 'T';
       ownerReference.serverDataType['DecimalEstado'] = 'D';
       ownerReference.serverDataType['IdEstado'] = 'I';
       ownerReference.serverDataType['IdPais'] = 'I';
       ownerReference.serverDataType['StringEstado'] = 'S';
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
       ownerReference.namespace = 'Linx.Demo.BV.ModalExterna';
       ownerReference.myProperties = [ 'ComboboxEstado','DatetimeEstado','DecimalEstado','IdEstado','IdPais','StringEstado' ];
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
    //#endregion Classes Map
    //#region Context Definition
    
    //#region Get LookUps
    
    var getLookUpCidadeByEntitySearch = function (jEntitySearch, order, skip, take, direction, lookupField) {
        var query = EntityQuery.from('GetLookUpCidadeByEntitySearch').noTracking(true);
        query = (direction === 'descending' ? query.orderByDesc(order) : query.orderBy(order));
    
        if ((typeof jEntitySearch !== 'undefined') && jEntitySearch !== null)
            query = query.withParameters({ propertyName: (isNullOrEmpty(lookupField) ? order : lookupField), jEntitySearch: jEntitySearch });
    
        if (take > 0)
           query = query.skip(skip).take(take);
        query = query.inlineCount(true);
    
        return manager.executeQuery(query)
        .fail(queryFailed);
    };
    
    var getLookUpLojaByEntitySearch = function (jEntitySearch, order, skip, take, direction, lookupField) {
        var query = EntityQuery.from('GetLookUpLojaByEntitySearch').noTracking(true);
        query = (direction === 'descending' ? query.orderByDesc(order) : query.orderBy(order));
    
        if ((typeof jEntitySearch !== 'undefined') && jEntitySearch !== null)
            query = query.withParameters({ propertyName: (isNullOrEmpty(lookupField) ? order : lookupField), jEntitySearch: jEntitySearch });
    
        if (take > 0)
           query = query.skip(skip).take(take);
        query = query.inlineCount(true);
    
        return manager.executeQuery(query)
        .fail(queryFailed);
    };
    
    var getLookUpVendedorByEntitySearch = function (jEntitySearch, order, skip, take, direction, lookupField) {
        var query = EntityQuery.from('GetLookUpVendedorByEntitySearch').noTracking(true);
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
    
    var clearLoja = function (idBandeiraRede, complete) {
        clearAll();
        resetSequence('Loja');
        var refLoja = manager.createEntity('Loja', {}, breeze.EntityState.Unchanged);
        if (complete) complete({ results: [ refLoja ] });
        return true;
    };
    
    var getLoja = function (predicate, preserveCurrentState, noTracking) {
        if (!preserveCurrentState) clearAll();
        var query = EntityQuery.from('GetLoja').noTracking(noTracking)
        .orderBy('IdLoja asc')
        ;
    
        if ((typeof predicate !== 'undefined') && predicate !== null)
            query = query.where(predicate);
    
        return manager.executeQuery(query)
        .fail(queryFailed);
    };
    
    var getLojaByEntitySearchNoAssociations = function (jEntitySearch, skip, take, returnInlineCount, preserveCurrentState, noTracking, orderByDef) {
        if (!preserveCurrentState) clearAll();
        var query = EntityQuery.from('GetLojaByEntitySearchNoAssociations').noTracking(noTracking)
        .orderBy((isNullOrEmpty(orderByDef) ? 'IdLoja asc' : orderByDef))
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
    
    var clearVenda = function (idBandeiraRede, complete) {
        clearAll();
        resetSequence('Venda');
        var refVenda = manager.createEntity('Venda', {}, breeze.EntityState.Unchanged);
        if (complete) complete({ results: [ refVenda ] });
        return true;
    };
    
    var getVenda = function (predicate, preserveCurrentState, noTracking) {
        if (!preserveCurrentState) clearAll();
        var query = EntityQuery.from('GetVenda').noTracking(noTracking)
        .orderBy('IdVenda asc')
        ;
    
        if ((typeof predicate !== 'undefined') && predicate !== null)
            query = query.where(predicate);
    
        return manager.executeQuery(query)
        .fail(queryFailed);
    };
    
    var getVendaByEntitySearchNoAssociations = function (jEntitySearch, skip, take, returnInlineCount, preserveCurrentState, noTracking, orderByDef) {
        if (!preserveCurrentState) clearAll();
        var query = EntityQuery.from('GetVendaByEntitySearchNoAssociations').noTracking(noTracking)
        .orderBy((isNullOrEmpty(orderByDef) ? 'IdVenda asc' : orderByDef))
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
    //#endregion
    
    // Create the data update event.
    var dataUpdateEvent = document.createEvent('Event');
    // Define that the event name is 'ModalExternaContext_DataUpdate'.
    var contextUpdtEvt = 'ModalExternaContext_DataUpdate_' + getNewGuid();
    dataUpdateEvent.initEvent(contextUpdtEvt, true, true);
    
    //#region LookUps Finalizers
     var finalizeAllLookUpCidade = function (replaceTo, selectedElements, propertyName, lookupInfo) {
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
            if (propertyName === '' || propertyName === 'IdCidade') {
               if (selectedElement.hasOwnProperty('IdCidade') && replaceTo.hasOwnProperty('IdCidade'))
               {
                   setAbsoluteValue(replaceTo, 'IdCidade', getAbsoluteValue(selectedElement['IdCidade']));
               }
               else if (replaceTo.hasOwnProperty('IdCidade')) {
                   setAbsoluteValue(replaceTo, 'IdCidade', 0);
               }
            }
            if (propertyName === '' || propertyName === 'IdEstado') {
               if (selectedElement.hasOwnProperty('IdEstado') && replaceTo.hasOwnProperty('IdEstado'))
               {
                   setAbsoluteValue(replaceTo, 'IdEstado', getAbsoluteValue(selectedElement['IdEstado']));
               }
               else if (replaceTo.hasOwnProperty('IdEstado')) {
                   setAbsoluteValue(replaceTo, 'IdEstado', null);
               }
            }
            if (propertyName === '' || propertyName === 'IdPais') {
               if (selectedElement.hasOwnProperty('IdPais') && replaceTo.hasOwnProperty('IdPais'))
               {
                   setAbsoluteValue(replaceTo, 'IdPais', getAbsoluteValue(selectedElement['IdPais']));
               }
               else if (replaceTo.hasOwnProperty('IdPais')) {
                   setAbsoluteValue(replaceTo, 'IdPais', null);
               }
            }
            if (replaceTo.validatedlookupsArray && !replaceTo.validatedlookupsArray.contains('LookUpCidade'))
                replaceTo.validatedlookupsArray.push('LookUpCidade');
        }
        //Trigger context data update event
        if (replaceTo.isPOCO) vm.refreshCurrentBind();
        document.dispatchEvent(dataUpdateEvent);
        isFinalizingLookup(false);
    };
    
    function clearLookUpCidade(replaceTo) {
        if (!replaceTo)
            return;
        isClearingLookup(true);
        setAbsoluteValue(replaceTo, 'IdCidade', 0);
        setAbsoluteValue(replaceTo, 'IdEstado', null);
        setAbsoluteValue(replaceTo, 'IdPais', null);
        isClearingLookup(false);
        //Trigger context data update event
        if (replaceTo.isPOCO) vm.refreshCurrentBind();
        setTimeout(function () {document.dispatchEvent(dataUpdateEvent);}, 100);
    }
     var finalizeAllLookUpLoja = function (replaceTo, selectedElements, propertyName, lookupInfo) {
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
            if (propertyName === '' || propertyName === 'IdLoja') {
               if (selectedElement.hasOwnProperty('IdLoja') && replaceTo.hasOwnProperty('IdLoja'))
               {
                   setAbsoluteValue(replaceTo, 'IdLoja', getAbsoluteValue(selectedElement['IdLoja']));
               }
               else if (replaceTo.hasOwnProperty('IdLoja')) {
                   setAbsoluteValue(replaceTo, 'IdLoja', null);
               }
            }
            if (replaceTo.validatedlookupsArray && !replaceTo.validatedlookupsArray.contains('LookUpLoja'))
                replaceTo.validatedlookupsArray.push('LookUpLoja');
        }
        //Trigger context data update event
        if (replaceTo.isPOCO) vm.refreshCurrentBind();
        document.dispatchEvent(dataUpdateEvent);
        isFinalizingLookup(false);
    };
    
    function clearLookUpLoja(replaceTo) {
        if (!replaceTo)
            return;
        isClearingLookup(true);
        setAbsoluteValue(replaceTo, 'IdLoja', null);
        isClearingLookup(false);
        //Trigger context data update event
        if (replaceTo.isPOCO) vm.refreshCurrentBind();
        setTimeout(function () {document.dispatchEvent(dataUpdateEvent);}, 100);
    }
     var finalizeAllLookUpVendedor = function (replaceTo, selectedElements, propertyName, lookupInfo) {
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
            if (propertyName === '' || propertyName === 'IdVendedor') {
               if (selectedElement.hasOwnProperty('IdVendedor') && replaceTo.hasOwnProperty('IdVendedor'))
               {
                   setAbsoluteValue(replaceTo, 'IdVendedor', getAbsoluteValue(selectedElement['IdVendedor']));
               }
               else if (replaceTo.hasOwnProperty('IdVendedor')) {
                   setAbsoluteValue(replaceTo, 'IdVendedor', null);
               }
            }
            if (replaceTo.validatedlookupsArray && !replaceTo.validatedlookupsArray.contains('LookUpVendedor'))
                replaceTo.validatedlookupsArray.push('LookUpVendedor');
        }
        //Trigger context data update event
        if (replaceTo.isPOCO) vm.refreshCurrentBind();
        document.dispatchEvent(dataUpdateEvent);
        isFinalizingLookup(false);
    };
    
    function clearLookUpVendedor(replaceTo) {
        if (!replaceTo)
            return;
        isClearingLookup(true);
        setAbsoluteValue(replaceTo, 'IdVendedor', null);
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
               url: getServiceAddress('LinxDemoModalExterna/Save' + vm.rootDataTypeName),
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
    
    var createLoja = function() {
        //Create entity instance
        enableChangeTrack = false;
        var entity = createEntity('Loja', { BitLoja: false });
        entity.setDefaults();
        if (typeof entity.OnAdding == 'function') {
            if (!entity.OnAdding()) { dataContext.deleteEntity(entity); return; }
        }
        enableChangeTrack = true;
        return entity;
    };
    
    var createVenda = function() {
        //Create entity instance
        enableChangeTrack = false;
        var entity = createEntity('Venda', { BitVenda: false });
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
    
    var createEstado = function() {
        //Create entity instance
        enableChangeTrack = false;
        var entity = createEntity('Estado');
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
           url: getServiceAddress("LinxDemoModalExterna/GetReportDataSource"),
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
           url: getServiceAddress("LinxDemoModalExterna/GetTemplateReport"),
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
            createLoja: createLoja,
            createVenda: createVenda,
            createPais: createPais,
            createEstado: createEstado,
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
                getLookUpCidadeByEntitySearch: getLookUpCidadeByEntitySearch,
            getLookUpLojaByEntitySearch: getLookUpLojaByEntitySearch,
            getLookUpVendedorByEntitySearch: getLookUpVendedorByEntitySearch,
            getBmEntityProperties: getBmEntityProperties,
            clearLoja: clearLoja,
            getLoja: getLoja,
            getLojaByEntitySearchNoAssociations: getLojaByEntitySearchNoAssociations,
            clearVenda: clearVenda,
            getVenda: getVenda,
            getVendaByEntitySearchNoAssociations: getVendaByEntitySearchNoAssociations,
            clearPais: clearPais,
            getPais: getPais,
            getPaisByEntitySearchNoAssociations: getPaisByEntitySearchNoAssociations,
            clearEstado: clearEstado,
            getEstado: getEstado,
            getEstadoByEntitySearchNoAssociations: getEstadoByEntitySearchNoAssociations,
                finalizeAllLookUpCidade: finalizeAllLookUpCidade,
            clearLookUpCidade: clearLookUpCidade,
            finalizeAllLookUpLoja: finalizeAllLookUpLoja,
            clearLookUpLoja: clearLookUpLoja,
            finalizeAllLookUpVendedor: finalizeAllLookUpVendedor,
            clearLookUpVendedor: clearLookUpVendedor
        };
    loadParameters();
    return dataContext;
    //#endregion Context Definition
}
return result;
});
