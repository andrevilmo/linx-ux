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
    entityNames.push('Cliente');
    metadataInfo['Cliente'] = [
        { key: 'BigIntCliente', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 24, isPartOfKey: false, headerText: 'Big Int Cliente', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'BitCliente', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Bit Cliente', width: '179px', dataType: 'bool', format: 'checkbox', hidden: false, unbound: false, group: null },
        { key: 'ComboboxCliente', isQbeZero: false, isDomain: true, domainName: 'LX_CLIENTE', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 3, isPartOfKey: false, headerText: 'Combobox Cliente', width: '244px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'ComboboxClienteName', isDomain: true, domainName: 'LX_CLIENTE', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Combobox Cliente (Name)', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null },
        { key: 'DatetimeCliente', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Datetime Cliente', width: '244px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null },
        { key: 'DecimalCliente', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 18, isPartOfKey: false, headerText: 'Decimal Cliente', width: '231px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'GuidCliente', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 36, isPartOfKey: false, headerText: 'Guid Cliente', width: '250px', dataType: 'string', format: '', hidden: true, unbound: false, group: null },
        { key: 'IdCliente', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 12, isPartOfKey: true, headerText: 'Id Cliente', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IdEstado', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: 'IdEstado', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Id Estado', width: '271px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IdPais', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: 'IdPais', lookupVisibleColumns: 'IdPais,StringPais', maxLength: 12, isPartOfKey: false, headerText: 'Id Pais', width: '271px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IntCliente', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Int Cliente', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'SmallIntCliente', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 6, isPartOfKey: false, headerText: 'Small Int Cliente', width: '257px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'StringCliente', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'String Cliente', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null },
        { key: 'StringEstado', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: 'StringEstado', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'String Estado', width: '421px', dataType: 'string', format: '', hidden: false, unbound: false, group: null },
        { key: 'StringPais', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: 'StringPais', lookupVisibleColumns: 'IdPais,StringPais', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'String Pais', width: '421px', dataType: 'string', format: '', hidden: false, unbound: false, group: null },
        { key: 'ChangeState', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: '', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null }
    ];
    dataExportInfo['Cliente'] = [ 
        { name: 'Cliente', canExportMedia: true , canExportReport: true, actionExport: 'LinxDemoModalExterna/GetClienteToExcel', actionReport: 'LinxDemoModalExterna/GetClienteToReportXml', actionFeed: 'LinxDemoModalExternaOData/Cliente', actionName: 'LinxDemoModalExterna/GetClienteByEntitySearchNoAssociations', display: 'Cliente',  metaData: function() { return metadataInfo['Cliente']; } }
    ];
    entitylookUps.push('Cliente');
    entitylookUps['Cliente'] = [];
    entitylookUps['Cliente'].push('LookUpEstado');
    lookUpNames.push('LookUpEstado');
    metadataInfo['LookUpEstado'] = [
        { key: 'IdEstado', relatedKey: 'IdEstado', maxLength: 10, isPartOfKey: true, headerText: 'Id Estado', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IdPais', relatedKey: 'IdPais', maxLength: 10, isPartOfKey: false, headerText: 'Id Pais', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'StringPais', relatedKey: 'StringPais', maxLength: 50, isPartOfKey: false, headerText: 'String Pais', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null },
        { key: 'StringEstado', relatedKey: 'StringEstado', maxLength: 50, isPartOfKey: false, headerText: 'String Estado', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null }
    ];
    entityNames.push('Venda');
    metadataInfo['Venda'] = [
        { key: 'BigIntVenda', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 24, isPartOfKey: false, headerText: 'Big Int Venda', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'BitVenda', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Bit Venda', width: '153px', dataType: 'bool', format: 'checkbox', hidden: false, unbound: false, group: null },
        { key: 'ComboboxVenda', isQbeZero: false, isDomain: true, domainName: 'LX_VENDA', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 3, isPartOfKey: false, headerText: 'Combobox Venda', width: '218px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'ComboboxVendaName', isDomain: true, domainName: 'LX_VENDA', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Combobox Venda (Name)', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null },
        { key: 'DatetimeVenda', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Datetime Venda', width: '218px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null },
        { key: 'DecimalVenda', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 18, isPartOfKey: false, headerText: 'Decimal Venda', width: '205px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'GuidVenda', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 36, isPartOfKey: false, headerText: 'Guid Venda', width: '250px', dataType: 'string', format: '', hidden: true, unbound: false, group: null },
        { key: 'IdCliente', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: 'IdCliente', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Id Cliente', width: '271px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IdLoja', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: 'IdLoja', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Id Loja', width: '271px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IdVenda', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 12, isPartOfKey: true, headerText: 'Id Venda', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IntVenda', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Int Venda', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'SmallIntVenda', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 6, isPartOfKey: false, headerText: 'Small Int Venda', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'StringLoja', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: 'StringLoja', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'String Loja', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null },
        { key: 'StringVenda', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'String Venda', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null },
        { key: 'ChangeState', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: '', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null }
    ];
    dataExportInfo['Venda'] = [ 
        { name: 'Venda', canExportMedia: true , canExportReport: true, actionExport: 'LinxDemoModalExterna/GetVendaToExcel', actionReport: 'LinxDemoModalExterna/GetVendaToReportXml', actionFeed: 'LinxDemoModalExternaOData/Venda', actionName: 'LinxDemoModalExterna/GetVendaByEntitySearchNoAssociations', display: 'Venda',  metaData: function() { return metadataInfo['Venda']; } }
        , { name: 'VendaItem', canExportMedia: true , canExportReport: true, actionExport: 'LinxDemoModalExterna/GetVendaItemParentCompositionToExcel', actionReport: 'LinxDemoModalExterna/GetVendaItemParentCompositionToReportXml', actionFeed: 'LinxDemoModalExternaOData/VendaItemParentComposition', actionName: 'LinxDemoModalExterna/GetVendaItemParentCompositionByEntitySearchNoAssociations', display: 'VendaItem',  metaData: function() { return metadataInfo['VendaItemParentComposition']; } }
    ];
    entitylookUps.push('Venda');
    entitylookUps['Venda'] = [];
    entitylookUps['Venda'].push('LookUpCliente');
    lookUpNames.push('LookUpCliente');
    metadataInfo['LookUpCliente'] = [
        { key: 'IdCliente', relatedKey: 'IdCliente', maxLength: 10, isPartOfKey: true, headerText: 'Id Cliente', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null }
    ];
    entitylookUps['Venda'].push('LookUpLoja');
    lookUpNames.push('LookUpLoja');
    metadataInfo['LookUpLoja'] = [
        { key: 'IdLoja', relatedKey: 'IdLoja', maxLength: 10, isPartOfKey: true, headerText: 'Id Loja', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'StringLoja', relatedKey: 'StringLoja', maxLength: 50, isPartOfKey: false, headerText: 'String Loja', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null }
    ];
    entityNames.push('VendaItem');
    metadataInfo['VendaItem'] = [
        { key: 'BigIntVendaItem', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 24, isPartOfKey: false, headerText: 'Big Int Venda Item', width: '270px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'BitVendaItem', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Bit Venda Item', width: '218px', dataType: 'bool', format: 'checkbox', hidden: false, unbound: false, group: null },
        { key: 'ComboboxVendaItem', isQbeZero: false, isDomain: true, domainName: 'LX_VENDA_ITEM', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 3, isPartOfKey: false, headerText: 'Combobox Venda Item', width: '283px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'ComboboxVendaItemName', isDomain: true, domainName: 'LX_VENDA_ITEM', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Combobox Venda Item (Name)', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null },
        { key: 'DatetimeVendaItem', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Datetime Venda Item', width: '283px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null },
        { key: 'DecimalVendaItem', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 13, isPartOfKey: false, headerText: 'Decimal Venda Item', width: '270px', dataType: 'number', format: '0.00', hidden: false, unbound: false, group: null },
        { key: 'GuidVendaItem', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 36, isPartOfKey: false, headerText: 'Guid Venda Item', width: '250px', dataType: 'string', format: '', hidden: true, unbound: false, group: null },
        { key: 'IdCliente', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Id Cliente', width: '271px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IdVenda', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Id Venda', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IdVendaItem', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 12, isPartOfKey: true, headerText: 'Id Venda Item', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IntVenda', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Int Venda', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IntVendaItem', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Int Venda Item', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'SmallIntVendaItem', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 6, isPartOfKey: false, headerText: 'Small Int Venda Item', width: '296px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'StringVendaItem', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'String Venda Item', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null },
        { key: 'ChangeState', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: '', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null }
    ];
    entityNames.push('VendaItemParentComposition');
    metadataInfo['VendaItemParentComposition'] = [
        { key: 'BigIntVendaItem', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 24, isPartOfKey: false, headerText: 'Big Int Venda Item', width: '270px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'BitVendaItem', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Bit Venda Item', width: '218px', dataType: 'bool', format: 'checkbox', hidden: false, unbound: false, group: null },
        { key: 'ComboboxVendaItem', isQbeZero: false, isDomain: true, domainName: 'LX_VENDA_ITEM', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 3, isPartOfKey: false, headerText: 'Combobox Venda Item', width: '283px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'ComboboxVendaItemName', isDomain: true, domainName: 'LX_VENDA_ITEM', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Combobox Venda Item (Name)', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null },
        { key: 'DatetimeVendaItem', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Datetime Venda Item', width: '283px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null },
        { key: 'DecimalVendaItem', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 13, isPartOfKey: false, headerText: 'Decimal Venda Item', width: '270px', dataType: 'number', format: '0.00', hidden: false, unbound: false, group: null },
        { key: 'GuidVendaItem', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 36, isPartOfKey: false, headerText: 'Guid Venda Item', width: '250px', dataType: 'string', format: '', hidden: true, unbound: false, group: null },
        { key: 'IdCliente', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Id Cliente', width: '271px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IdVenda', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Id Venda', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IdVendaItem', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 12, isPartOfKey: true, headerText: 'Id Venda Item', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IntVenda', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Int Venda', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IntVendaItem', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Int Venda Item', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'SmallIntVendaItem', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 6, isPartOfKey: false, headerText: 'Small Int Venda Item', width: '296px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'StringVendaItem', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'String Venda Item', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null },
        { key: 'BigIntVenda', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 24, isPartOfKey: false, headerText: 'Big Int Venda', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'BitVenda', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Bit Venda', width: '153px', dataType: 'bool', format: 'checkbox', hidden: false, unbound: false, group: null },
        { key: 'ComboboxVenda', isQbeZero: false, isDomain: true, domainName: 'LX_VENDA', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 3, isPartOfKey: false, headerText: 'Combobox Venda', width: '218px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'ComboboxVendaName', isDomain: true, domainName: 'LX_VENDA', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Combobox Venda (Name)', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null },
        { key: 'DatetimeVenda', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Datetime Venda', width: '218px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null },
        { key: 'DecimalVenda', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 18, isPartOfKey: false, headerText: 'Decimal Venda', width: '205px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'GuidVenda', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 36, isPartOfKey: false, headerText: 'Guid Venda', width: '250px', dataType: 'string', format: '', hidden: true, unbound: false, group: null },
        { key: 'IdLoja', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Id Loja', width: '271px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'SmallIntVenda', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 6, isPartOfKey: false, headerText: 'Small Int Venda', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'StringLoja', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'String Loja', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null },
        { key: 'StringVenda', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'String Venda', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null },
        { key: 'ChangeState', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: '', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null }
    ];
    dataExportInfo['VendaItem'] = [ 
        { name: 'VendaItem', canExportMedia: true , canExportReport: true, actionExport: 'LinxDemoModalExterna/GetVendaItemToExcel', actionReport: 'LinxDemoModalExterna/GetVendaItemToReportXml', actionFeed: 'LinxDemoModalExternaOData/VendaItem', actionName: 'LinxDemoModalExterna/GetVendaItemByEntitySearchNoAssociations', display: 'VendaItem',  metaData: function() { return metadataInfo['VendaItem']; } }
    ];
    entitylookUps.push('VendaItem');
    entitylookUps['VendaItem'] = [];
    entityNames.push('FormaPagamento');
    metadataInfo['FormaPagamento'] = [
        { key: 'BigIntFormaPagamento', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 24, isPartOfKey: false, headerText: 'Big Int Forma Pagamento', width: '335px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'BitFormaPagamento', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Bit Forma Pagamento', width: '283px', dataType: 'bool', format: 'checkbox', hidden: false, unbound: false, group: null },
        { key: 'ComboboxFormaPagamento', isQbeZero: false, isDomain: true, domainName: 'LX_FORMA_PAGAMENTO', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 3, isPartOfKey: false, headerText: 'Combobox Forma Pagamento', width: '348px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'ComboboxFormaPagamentoName', isDomain: true, domainName: 'LX_FORMA_PAGAMENTO', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Combobox Forma Pagamento (Name)', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null },
        { key: 'DatetimeFormaPagamento', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Datetime Forma Pagamento', width: '348px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null },
        { key: 'DecimalFormaPagamento', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 13, isPartOfKey: false, headerText: 'Decimal Forma Pagamento', width: '335px', dataType: 'number', format: '0.00', hidden: false, unbound: false, group: null },
        { key: 'GuidFormaPagamento', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 36, isPartOfKey: false, headerText: 'Guid Forma Pagamento', width: '296px', dataType: 'string', format: '', hidden: true, unbound: false, group: null },
        { key: 'IdFormaPagamento', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 12, isPartOfKey: true, headerText: 'Id Forma Pagamento', width: '270px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IdVenda', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: 'IdVenda', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Id Venda', width: '271px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IntFormaPagamento', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Int Forma Pagamento', width: '283px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'SmallIntFormaPagamento', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 6, isPartOfKey: false, headerText: 'Small Int Forma Pagamento', width: '361px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'StringFormaPagamento', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'String Forma Pagamento', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null },
        { key: 'ChangeState', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: '', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null }
    ];
    dataExportInfo['FormaPagamento'] = [ 
        { name: 'FormaPagamento', canExportMedia: true , canExportReport: true, actionExport: 'LinxDemoModalExterna/GetFormaPagamentoToExcel', actionReport: 'LinxDemoModalExterna/GetFormaPagamentoToReportXml', actionFeed: 'LinxDemoModalExternaOData/FormaPagamento', actionName: 'LinxDemoModalExterna/GetFormaPagamentoByEntitySearchNoAssociations', display: 'FormaPagamento',  metaData: function() { return metadataInfo['FormaPagamento']; } }
    ];
    entitylookUps.push('FormaPagamento');
    entitylookUps['FormaPagamento'] = [];
    entitylookUps['FormaPagamento'].push('LookUpVenda');
    lookUpNames.push('LookUpVenda');
    metadataInfo['LookUpVenda'] = [
        { key: 'IdVenda', relatedKey: 'IdVenda', maxLength: 10, isPartOfKey: true, headerText: 'Id Venda', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null }
    ];
    entityNames.push('Loja');
    metadataInfo['Loja'] = [
        { key: 'BigIntLoja', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 24, isPartOfKey: false, headerText: 'Big Int Loja', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'BitLoja', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Bit Loja', width: '140px', dataType: 'bool', format: 'checkbox', hidden: false, unbound: false, group: null },
        { key: 'ComboboxLoja', isQbeZero: false, isDomain: true, domainName: 'LX_LOJA', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 3, isPartOfKey: false, headerText: 'Combobox Loja', width: '205px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'ComboboxLojaName', isDomain: true, domainName: 'LX_LOJA', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Combobox Loja (Name)', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null },
        { key: 'DatetimeLoja', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Datetime Loja', width: '205px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null },
        { key: 'DecimalLoja', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 13, isPartOfKey: false, headerText: 'Decimal Loja', width: '192px', dataType: 'number', format: '0.00', hidden: false, unbound: false, group: null },
        { key: 'GuidLoja', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 36, isPartOfKey: false, headerText: 'Guid Loja', width: '250px', dataType: 'string', format: '', hidden: true, unbound: false, group: null },
        { key: 'IdLoja', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 12, isPartOfKey: true, headerText: 'Id Loja', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IntLoja', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Int Loja', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'SmallIntLoja', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 6, isPartOfKey: false, headerText: 'Small Int Loja', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'StringLoja', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'String Loja', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null },
        { key: 'ChangeState', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: '', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null }
    ];
    dataExportInfo['Loja'] = [ 
        { name: 'Loja', canExportMedia: true , canExportReport: true, actionExport: 'LinxDemoModalExterna/GetLojaToExcel', actionReport: 'LinxDemoModalExterna/GetLojaToReportXml', actionFeed: 'LinxDemoModalExternaOData/Loja', actionName: 'LinxDemoModalExterna/GetLojaByEntitySearchNoAssociations', display: 'Loja',  metaData: function() { return metadataInfo['Loja']; } }
    ];
    entitylookUps.push('Loja');
    entitylookUps['Loja'] = [];
    entityNames.push('Estado');
    metadataInfo['Estado'] = [
        { key: 'BigIntEstado', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 24, isPartOfKey: false, headerText: 'Big Int Estado', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'BitEstado', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Bit Estado', width: '166px', dataType: 'bool', format: 'checkbox', hidden: false, unbound: false, group: null },
        { key: 'ComboboxEstado', isQbeZero: false, isDomain: true, domainName: 'LX_ESTADO', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 3, isPartOfKey: false, headerText: 'Combobox Estado', width: '231px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'ComboboxEstadoName', isDomain: true, domainName: 'LX_ESTADO', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Combobox Estado (Name)', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null },
        { key: 'DatetimeEstado', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Datetime Estado', width: '231px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null },
        { key: 'DecimalEstado', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 13, isPartOfKey: false, headerText: 'Decimal Estado', width: '218px', dataType: 'number', format: '0.00', hidden: false, unbound: false, group: null },
        { key: 'GuidEstado', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 36, isPartOfKey: false, headerText: 'Guid Estado', width: '250px', dataType: 'string', format: '', hidden: true, unbound: false, group: null },
        { key: 'IdEstado', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 12, isPartOfKey: true, headerText: 'Id Estado', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IdPais', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: 'IdPais', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Id Pais', width: '271px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IntEstado', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Int Estado', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'SmallIntEstado', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 6, isPartOfKey: false, headerText: 'Small Int Estado', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'StringEstado', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'String Estado', width: '421px', dataType: 'string', format: '', hidden: false, unbound: false, group: null },
        { key: 'StringPais', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: 'StringPais', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'String Pais', width: '421px', dataType: 'string', format: '', hidden: false, unbound: false, group: null },
        { key: 'ChangeState', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: '', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null }
    ];
    dataExportInfo['Estado'] = [ 
        { name: 'Estado', canExportMedia: true , canExportReport: true, actionExport: 'LinxDemoModalExterna/GetEstadoToExcel', actionReport: 'LinxDemoModalExterna/GetEstadoToReportXml', actionFeed: 'LinxDemoModalExternaOData/Estado', actionName: 'LinxDemoModalExterna/GetEstadoByEntitySearchNoAssociations', display: 'Estado',  metaData: function() { return metadataInfo['Estado']; } }
    ];
    entitylookUps.push('Estado');
    entitylookUps['Estado'] = [];
    entitylookUps['Estado'].push('LookUpPais');
    lookUpNames.push('LookUpPais');
    metadataInfo['LookUpPais'] = [
        { key: 'IdPais', relatedKey: 'IdPais', maxLength: 10, isPartOfKey: true, headerText: 'Id Pais', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'StringPais', relatedKey: 'StringPais', maxLength: 50, isPartOfKey: false, headerText: 'String Pais', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null }
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
    
    // Configure Cliente data type
    metadataStore.addEntityType({
    shortName: "Cliente",
    namespace: "Linx.Demo.BV.ModalExterna",
    autoGeneratedKeyType: AutoGeneratedKeyType.Identity,
    dataProperties: {
    BigIntCliente: { dataType: DataType.Int64, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,BitCliente: { dataType: DataType.Boolean, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,ComboboxCliente: { dataType: DataType.Byte, isNullable: false, isPartOfKey: false, validators: [ Validator.hasValueValidator]  }
    ,ComboboxClienteName: { dataType: DataType.String, isNullable: false, isPartOfKey: false, validators: [] }
    ,DatetimeCliente: { dataType: DataType.DateTime, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,DecimalCliente: { dataType: DataType.Decimal, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,GuidCliente: { dataType: DataType.Guid, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,IdCliente: { dataType: DataType.Int32, isNullable: false, isPartOfKey: true, validators: [ Validator.hasValueValidator]  }
    ,IdEstado: { dataType: DataType.Int32, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,IdPais: { dataType: DataType.Int32, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,IntCliente: { dataType: DataType.Int32, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,SmallIntCliente: { dataType: DataType.Int16, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,StringCliente: { dataType: DataType.String, maxLength: 50, isNullable: true, isPartOfKey: false, validators: [ Validator.maxLength( {maxLength: 50})]  }
    ,StringEstado: { dataType: DataType.String, maxLength: 50, isNullable: true, isPartOfKey: false, validators: [ Validator.maxLength( {maxLength: 50})]  }
    ,StringPais: { dataType: DataType.String, maxLength: 50, isNullable: true, isPartOfKey: false, validators: [ Validator.maxLength( {maxLength: 50})]  }
    ,ChangeState: { dataType: DataType.String, isNullable: true, isPartOfKey: false, validators: [] }
                    },
    navigationProperties: {
    // Returns collections of details and associates with Parent
                          }
    });
    lookUpProperties['Cliente'] = {IdEstado: 'LookUpEstado', IdPais: 'LookUpEstado', StringEstado: 'LookUpEstado', StringPais: 'LookUpEstado'};
    var ClienteInitializer = function (ownerReference, isPOCO) {
       ownerReference.RowDataId = (isPOCO === true ? getNextSequence('Cliente') : ko.observable(getNextSequence('Cliente')));
        //Start Property Definitions
        var _bigIntCliente = ownerReference.BigIntCliente;
        Object.defineProperty(ownerReference, 'BigIntCliente', {
          get: function() { return _bigIntCliente; },
          set: function(newValue) { var oldValue = _bigIntCliente; _bigIntCliente = newValue; if (!entityPropChanged(ownerReference, 'BigIntCliente', oldValue, newValue)) { _bigIntCliente = oldValue; } }
        });
        var _bitCliente = ownerReference.BitCliente;
        Object.defineProperty(ownerReference, 'BitCliente', {
          get: function() { return _bitCliente; },
          set: function(newValue) { var oldValue = _bitCliente; _bitCliente = newValue; if (!entityPropChanged(ownerReference, 'BitCliente', oldValue, newValue)) { _bitCliente = oldValue; } }
        });
        var _comboboxCliente = ownerReference.ComboboxCliente;
        Object.defineProperty(ownerReference, 'ComboboxCliente', {
          get: function() { return _comboboxCliente; },
          set: function(newValue) { var oldValue = _comboboxCliente; _comboboxCliente = newValue; if (!entityPropChanged(ownerReference, 'ComboboxCliente', oldValue, newValue)) { _comboboxCliente = oldValue; } else { _comboboxClienteName = (dataDomains.getName('LX_CLIENTE', newValue)); } }
        });
        var _comboboxClienteName = ownerReference.ComboboxClienteName;
        Object.defineProperty(ownerReference, 'ComboboxClienteName', {
          get: function() { return _comboboxClienteName; },
          set: function(newValue) { var oldValue = _comboboxClienteName; _comboboxClienteName = newValue; if (!entityPropChanged(ownerReference, 'ComboboxClienteName', oldValue, newValue)) { _comboboxClienteName = oldValue; } else { _comboboxCliente = (dataDomains.getId('LX_CLIENTE', newValue)); } }
        });
        var _datetimeCliente = ownerReference.DatetimeCliente;
        Object.defineProperty(ownerReference, 'DatetimeCliente', {
          get: function() { return _datetimeCliente; },
          set: function(newValue) { var oldValue = _datetimeCliente; _datetimeCliente = newValue; if (!entityPropChanged(ownerReference, 'DatetimeCliente', oldValue, newValue)) { _datetimeCliente = oldValue; } }
        });
        var _decimalCliente = ownerReference.DecimalCliente;
        Object.defineProperty(ownerReference, 'DecimalCliente', {
          get: function() { return _decimalCliente; },
          set: function(newValue) { var oldValue = _decimalCliente; _decimalCliente = newValue; if (!entityPropChanged(ownerReference, 'DecimalCliente', oldValue, newValue)) { _decimalCliente = oldValue; } }
        });
        var _guidCliente = ownerReference.GuidCliente;
        Object.defineProperty(ownerReference, 'GuidCliente', {
          get: function() { return _guidCliente; },
          set: function(newValue) { var oldValue = _guidCliente; _guidCliente = newValue; if (!entityPropChanged(ownerReference, 'GuidCliente', oldValue, newValue)) { _guidCliente = oldValue; } }
        });
        var _idCliente = ownerReference.IdCliente;
        Object.defineProperty(ownerReference, 'IdCliente', {
          get: function() { return _idCliente; },
          set: function(newValue) { var oldValue = _idCliente; _idCliente = newValue; if (!entityPropChanged(ownerReference, 'IdCliente', oldValue, newValue)) { _idCliente = oldValue; } }
        });
        var _idEstado = ownerReference.IdEstado;
        Object.defineProperty(ownerReference, 'IdEstado', {
          get: function() { return _idEstado; },
          set: function(newValue) { var oldValue = _idEstado; _idEstado = newValue; if (!entityPropChanged(ownerReference, 'IdEstado', oldValue, newValue)) { _idEstado = oldValue; } }
        });
        var _idPais = ownerReference.IdPais;
        Object.defineProperty(ownerReference, 'IdPais', {
          get: function() { return _idPais; },
          set: function(newValue) { var oldValue = _idPais; _idPais = newValue; if (!entityPropChanged(ownerReference, 'IdPais', oldValue, newValue)) { _idPais = oldValue; } }
        });
        var _intCliente = ownerReference.IntCliente;
        Object.defineProperty(ownerReference, 'IntCliente', {
          get: function() { return _intCliente; },
          set: function(newValue) { var oldValue = _intCliente; _intCliente = newValue; if (!entityPropChanged(ownerReference, 'IntCliente', oldValue, newValue)) { _intCliente = oldValue; } }
        });
        var _smallIntCliente = ownerReference.SmallIntCliente;
        Object.defineProperty(ownerReference, 'SmallIntCliente', {
          get: function() { return _smallIntCliente; },
          set: function(newValue) { var oldValue = _smallIntCliente; _smallIntCliente = newValue; if (!entityPropChanged(ownerReference, 'SmallIntCliente', oldValue, newValue)) { _smallIntCliente = oldValue; } }
        });
        var _stringCliente = ownerReference.StringCliente;
        Object.defineProperty(ownerReference, 'StringCliente', {
          get: function() { return _stringCliente; },
          set: function(newValue) { var oldValue = _stringCliente; _stringCliente = newValue; if (!entityPropChanged(ownerReference, 'StringCliente', oldValue, newValue)) { _stringCliente = oldValue; } }
        });
        var _stringEstado = ownerReference.StringEstado;
        Object.defineProperty(ownerReference, 'StringEstado', {
          get: function() { return _stringEstado; },
          set: function(newValue) { var oldValue = _stringEstado; _stringEstado = newValue; if (!entityPropChanged(ownerReference, 'StringEstado', oldValue, newValue)) { _stringEstado = oldValue; } }
        });
        var _stringPais = ownerReference.StringPais;
        Object.defineProperty(ownerReference, 'StringPais', {
          get: function() { return _stringPais; },
          set: function(newValue) { var oldValue = _stringPais; _stringPais = newValue; if (!entityPropChanged(ownerReference, 'StringPais', oldValue, newValue)) { _stringPais = oldValue; } }
        });
        //End Property Definitions
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
       ownerReference.isAdded = (isPOCO === true ? function() { return ownerReference.ChangeState === 'I'; } : function() {
           return ownerReference.entityAspect.entityState === breeze.EntityState.Added;
       });
       ownerReference.isDeleted = (isPOCO === true ? function() { return ownerReference.ChangeState === 'D'; } : function() {
           return ownerReference.entityAspect.entityState === breeze.EntityState.Deleted;
       });
       ownerReference.isModified = (isPOCO === true ? function() { return ownerReference.ChangeState === 'U'; } : function() {
           return ownerReference.entityAspect.entityState === breeze.EntityState.Modified;
       });
       ownerReference.isDetached = (isPOCO === true ? function() { return false; } : function() {
           return ownerReference.entityAspect.entityState === breeze.EntityState.Detached;
       });
       ownerReference.isUnchanged = (isPOCO === true ? function() { return ownerReference.ChangeState === 'N'; } : function() {
           return ownerReference.entityAspect.entityState === breeze.EntityState.Unchanged;
       });
       ownerReference.setModified = (isPOCO === true ? function() { ownerReference.ChangeState = 'U'; } : function() {
           ownerReference.entityAspect.setModified();
       });
       ownerReference.setUnchanged = (isPOCO === true ? function() { ownerReference.ChangeState = 'N'; } : function() {
           ownerReference.entityAspect.setUnchanged();
       });
       ownerReference.serverDataType = [];
       ownerReference.serverDataType['BigIntCliente'] = 'L';
       ownerReference.serverDataType['BitCliente'] = 'B';
       ownerReference.serverDataType['ComboboxCliente'] = 'Y';
       ownerReference.serverDataType['DatetimeCliente'] = 'T';
       ownerReference.serverDataType['DecimalCliente'] = 'D';
       ownerReference.serverDataType['GuidCliente'] = 'G';
       ownerReference.serverDataType['IdCliente'] = 'I';
       ownerReference.serverDataType['IdEstado'] = 'I';
       ownerReference.serverDataType['IdPais'] = 'I';
       ownerReference.serverDataType['IntCliente'] = 'I';
       ownerReference.serverDataType['SmallIntCliente'] = 'H';
       ownerReference.serverDataType['StringCliente'] = 'S';
       ownerReference.serverDataType['StringEstado'] = 'S';
       ownerReference.serverDataType['StringPais'] = 'S';
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
       //#region Lookup Extended Methods
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
           return 'Seleção' + displayName;
       };
    
       ownerReference.getSpecializedLookup = function (lookupName, lookupInfo, fieldToSearch, valueToSearch, ownerReference, allowMultiSelectionInSearch) {
           var specializedLookup = '';
           if (lookupName === 'LookUpEstado') {
               specializedLookup = { moduleName: 'pkg_linx-demo-bv-spa/viewmodels/LookUpExterna', uiSettings: { modalForm: modal, fieldToSearch: fieldToSearch, valueToSearch: valueToSearch, lookupInfo: lookupInfo, lookupName: lookupName, ownerReference: ownerReference, removeDataToolbar: false, shareParentBO: false, useFilterFromParent: false, parentSelectorDataName: '', canClear: true, canSearch: true, canAddNew: false, canEdit: false, canDelete: false, canCustomSearch: true, canPrint: false, canLayout: false, canNavigate: true, allowMultiSelectionInSearch: allowMultiSelectionInSearch, applyFilterToParent: false, noSearch: false, parentFieldsRelation: [], detailFieldsRelation: [] } 
               };
           }
           return specializedLookup;
       };
    
       ownerReference.getSubQueryFilterFromLookUpEstado = function (propertyName) {
           var filter = '';
           if (propertyName === 'IdEstado') {
               var _IdPais = getAbsoluteValue(this.IdPais);
               if (!isNullOrEmpty(_IdPais)) { filter += (filter === '' ? '' : ';') + 'IdPais' + (_IdPais.toString().indexOf('[') > -1 ? '#In#S' : '#==#I') + _IdPais.toString().replaceAll('[', '').replaceAll(']', ''); }
               var _StringPais = getAbsoluteValue(this.StringPais);
               if (!isNullOrEmpty(_StringPais)) { filter += (filter === '' ? '' : ';') + 'StringPais' + (_StringPais.toString().indexOf('[') > -1 ? '#In#S' : '#==#S') + (_StringPais.toString().indexOf('[') > -1 ? 'S,' : '') + _StringPais.toString().replaceAll('[', '').replaceAll(']', ''); }
           }
           if (propertyName === 'StringEstado') {
               var _IdPais = getAbsoluteValue(this.IdPais);
               if (!isNullOrEmpty(_IdPais)) { filter += (filter === '' ? '' : ';') + 'IdPais' + (_IdPais.toString().indexOf('[') > -1 ? '#In#S' : '#==#I') + _IdPais.toString().replaceAll('[', '').replaceAll(']', ''); }
               var _StringPais = getAbsoluteValue(this.StringPais);
               if (!isNullOrEmpty(_StringPais)) { filter += (filter === '' ? '' : ';') + 'StringPais' + (_StringPais.toString().indexOf('[') > -1 ? '#In#S' : '#==#S') + (_StringPais.toString().indexOf('[') > -1 ? 'S,' : '') + _StringPais.toString().replaceAll('[', '').replaceAll(']', ''); }
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
           if (ownerReference.ChangeState == 'I') {
               if (parent && (typeof parent.ClienteList === 'function')) { 
                   parent.ClienteList.remove(ownerReference); 
               }
               else {
                   vm.dataView.remove(ownerReference);
               }
           }
           else {
               if (ownerReference.ChangeState == 'N') { ownerReference.createOriginal(); }
               ownerReference.ChangeState = 'D'; // mark for deletion
           }
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
       ownerReference.myProperties = [ 'BigIntCliente','BitCliente','ComboboxCliente','DatetimeCliente','DecimalCliente','GuidCliente','IdCliente','IdEstado','IdPais','IntCliente','SmallIntCliente','StringCliente','StringEstado','StringPais' ];
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
           ownerReference.ComboboxClienteName.subscribe(
               function (newValue) {
                   if (newValue == null) { ownerReference.ComboboxClienteName(''); return; }
                   var value = (dataDomains.getId('LX_CLIENTE', newValue));
                   if (value != ownerReference.ComboboxCliente()) {
                       ownerReference.ComboboxCliente(value);
                   }
            });
    
           ownerReference.ComboboxCliente.subscribe(
           function (newValue) {
                   if (newValue == null) { ownerReference.ComboboxCliente(0); return; }
                   var value = dataDomains.getName('LX_CLIENTE', newValue);
                   if (value != ownerReference.ComboboxClienteName()) {
                       ownerReference.ComboboxClienteName(value);
               }
           });
       }
    //#endregion Extended Domain Names
    };
    metadataStore.registerEntityTypeCtor("Cliente", null, ClienteInitializer);
    
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
    ,GuidVenda: { dataType: DataType.Guid, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,IdCliente: { dataType: DataType.Int32, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,IdLoja: { dataType: DataType.Int32, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,IdVenda: { dataType: DataType.Int32, isNullable: false, isPartOfKey: true, validators: [ Validator.hasValueValidator]  }
    ,IntVenda: { dataType: DataType.Int32, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,SmallIntVenda: { dataType: DataType.Int16, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,StringLoja: { dataType: DataType.String, maxLength: 50, isNullable: true, isPartOfKey: false, validators: [ Validator.maxLength( {maxLength: 50})]  }
    ,StringVenda: { dataType: DataType.String, maxLength: 50, isNullable: true, isPartOfKey: false, validators: [ Validator.maxLength( {maxLength: 50})]  }
    ,ChangeState: { dataType: DataType.String, isNullable: true, isPartOfKey: false, validators: [] }
                    },
    navigationProperties: {
    // Returns collections of details and associates with Parent
    VendaItemList: { entityTypeName: "VendaItem:#Linx.Demo.BV.ModalExterna", isScalar: false, invForeignKeyNames: ["IdVenda"], associationName: "FK_Venda_VendaItem" }
                          }
    });
    lookUpProperties['Venda'] = {IdCliente: 'LookUpCliente', IdLoja: 'LookUpLoja', StringLoja: 'LookUpLoja'};
    var VendaInitializer = function (ownerReference, isPOCO) {
       ownerReference.RowDataId = (isPOCO === true ? getNextSequence('Venda') : ko.observable(getNextSequence('Venda')));
        //Start Property Definitions
        var _bigIntVenda = ownerReference.BigIntVenda;
        Object.defineProperty(ownerReference, 'BigIntVenda', {
          get: function() { return _bigIntVenda; },
          set: function(newValue) { var oldValue = _bigIntVenda; _bigIntVenda = newValue; if (!entityPropChanged(ownerReference, 'BigIntVenda', oldValue, newValue)) { _bigIntVenda = oldValue; } }
        });
        var _bitVenda = ownerReference.BitVenda;
        Object.defineProperty(ownerReference, 'BitVenda', {
          get: function() { return _bitVenda; },
          set: function(newValue) { var oldValue = _bitVenda; _bitVenda = newValue; if (!entityPropChanged(ownerReference, 'BitVenda', oldValue, newValue)) { _bitVenda = oldValue; } }
        });
        var _comboboxVenda = ownerReference.ComboboxVenda;
        Object.defineProperty(ownerReference, 'ComboboxVenda', {
          get: function() { return _comboboxVenda; },
          set: function(newValue) { var oldValue = _comboboxVenda; _comboboxVenda = newValue; if (!entityPropChanged(ownerReference, 'ComboboxVenda', oldValue, newValue)) { _comboboxVenda = oldValue; } else { _comboboxVendaName = (dataDomains.getName('LX_VENDA', newValue)); } }
        });
        var _comboboxVendaName = ownerReference.ComboboxVendaName;
        Object.defineProperty(ownerReference, 'ComboboxVendaName', {
          get: function() { return _comboboxVendaName; },
          set: function(newValue) { var oldValue = _comboboxVendaName; _comboboxVendaName = newValue; if (!entityPropChanged(ownerReference, 'ComboboxVendaName', oldValue, newValue)) { _comboboxVendaName = oldValue; } else { _comboboxVenda = (dataDomains.getId('LX_VENDA', newValue)); } }
        });
        var _datetimeVenda = ownerReference.DatetimeVenda;
        Object.defineProperty(ownerReference, 'DatetimeVenda', {
          get: function() { return _datetimeVenda; },
          set: function(newValue) { var oldValue = _datetimeVenda; _datetimeVenda = newValue; if (!entityPropChanged(ownerReference, 'DatetimeVenda', oldValue, newValue)) { _datetimeVenda = oldValue; } }
        });
        var _decimalVenda = ownerReference.DecimalVenda;
        Object.defineProperty(ownerReference, 'DecimalVenda', {
          get: function() { return _decimalVenda; },
          set: function(newValue) { var oldValue = _decimalVenda; _decimalVenda = newValue; if (!entityPropChanged(ownerReference, 'DecimalVenda', oldValue, newValue)) { _decimalVenda = oldValue; } }
        });
        var _guidVenda = ownerReference.GuidVenda;
        Object.defineProperty(ownerReference, 'GuidVenda', {
          get: function() { return _guidVenda; },
          set: function(newValue) { var oldValue = _guidVenda; _guidVenda = newValue; if (!entityPropChanged(ownerReference, 'GuidVenda', oldValue, newValue)) { _guidVenda = oldValue; } }
        });
        var _idCliente = ownerReference.IdCliente;
        Object.defineProperty(ownerReference, 'IdCliente', {
          get: function() { return _idCliente; },
          set: function(newValue) { var oldValue = _idCliente; _idCliente = newValue; if (!entityPropChanged(ownerReference, 'IdCliente', oldValue, newValue)) { _idCliente = oldValue; } }
        });
        var _idLoja = ownerReference.IdLoja;
        Object.defineProperty(ownerReference, 'IdLoja', {
          get: function() { return _idLoja; },
          set: function(newValue) { var oldValue = _idLoja; _idLoja = newValue; if (!entityPropChanged(ownerReference, 'IdLoja', oldValue, newValue)) { _idLoja = oldValue; } }
        });
        var _idVenda = ownerReference.IdVenda;
        Object.defineProperty(ownerReference, 'IdVenda', {
          get: function() { return _idVenda; },
          set: function(newValue) { var oldValue = _idVenda; _idVenda = newValue; if (!entityPropChanged(ownerReference, 'IdVenda', oldValue, newValue)) { _idVenda = oldValue; } }
        });
        var _intVenda = ownerReference.IntVenda;
        Object.defineProperty(ownerReference, 'IntVenda', {
          get: function() { return _intVenda; },
          set: function(newValue) { var oldValue = _intVenda; _intVenda = newValue; if (!entityPropChanged(ownerReference, 'IntVenda', oldValue, newValue)) { _intVenda = oldValue; } }
        });
        var _smallIntVenda = ownerReference.SmallIntVenda;
        Object.defineProperty(ownerReference, 'SmallIntVenda', {
          get: function() { return _smallIntVenda; },
          set: function(newValue) { var oldValue = _smallIntVenda; _smallIntVenda = newValue; if (!entityPropChanged(ownerReference, 'SmallIntVenda', oldValue, newValue)) { _smallIntVenda = oldValue; } }
        });
        var _stringLoja = ownerReference.StringLoja;
        Object.defineProperty(ownerReference, 'StringLoja', {
          get: function() { return _stringLoja; },
          set: function(newValue) { var oldValue = _stringLoja; _stringLoja = newValue; if (!entityPropChanged(ownerReference, 'StringLoja', oldValue, newValue)) { _stringLoja = oldValue; } }
        });
        var _stringVenda = ownerReference.StringVenda;
        Object.defineProperty(ownerReference, 'StringVenda', {
          get: function() { return _stringVenda; },
          set: function(newValue) { var oldValue = _stringVenda; _stringVenda = newValue; if (!entityPropChanged(ownerReference, 'StringVenda', oldValue, newValue)) { _stringVenda = oldValue; } }
        });
        //End Property Definitions
       ownerReference.currentVendaItem = ko.observable(null);
       //Adjust details for a POCO reference
       if (isPOCO === true) {
           ownerReference.VendaItemList = ko.observableArray(ownerReference.VendaItemList);
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
           if (noDetails !== true && ownerReference.VendaItemList && ownerReference.VendaItemList().length > 0) {
             var detailExpr = ownerReference.VendaItemList()[0].getJExpression(listFilterRange, ['IdVenda']);
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
                   for (var i = 0; i < ownerReference.VendaItemList().length; i++) {
                       var detail = ownerReference.VendaItemList()[i];
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
               result.VendaItemList = [];
               var sourceList = getAbsoluteValue(ownerReference.VendaItemList);
               if (sourceList && sourceList.length > 0) {
                   for (var i = 0; i < sourceList.length; i++) {
                       if (['U', 'I', 'D'].indexOf(sourceList[i].ChangeState) >= 0) result.VendaItemList.push(sourceList[i].getPrimitiveDTO(sourceList[i].ChangeState != 'D'));
                   }
               }
           }
           return result;
       };
       ownerReference.getAllDetailChanges = function() {
           var result = [];
           var _VendaItemList = getAbsoluteValue(ownerReference.VendaItemList);
           if (_VendaItemList && _VendaItemList.length > 0) {
               for (var i = 0; i < _VendaItemList.length; i++) {
                   var detail = _VendaItemList[i];
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
               if (ownerReference.VendaItemList && originData.VendaItemList) {
                   var toList = getAbsoluteValue(ownerReference.VendaItemList);
                   var fromList = getAbsoluteValue(originData.VendaItemList);
                   for (var idxElem = toList.length - 1; idxElem >= 0; idxElem--) {
                      if (toList[idxElem].ChangeState === 'D') toList.splice(idxElem, 1);
                   }
                   for (var idxElem = toList.length - 1; idxElem >= 0; idxElem--) {
                          if (toList[idxElem].ChangeState !== 'N') {
                               var fromObj = _.where(fromList, { IdVendaItem: toList[idxElem]['IdVendaItem'] });
                               if (fromObj.length > 0) toList[idxElem].copyDataFrom(fromObj[0], true);
                          }
                   }
               }
           }
       enableChangeTrack = true;
       };
          ownerReference.commitDetailsVisualPendings = function() {
              vm.dataBind('VendaItemList', true);
              if (ownerReference.currentVendaItem()) ownerReference.currentVendaItem().commitDetailsVisualPendings();
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
       ownerReference.isAdded = (isPOCO === true ? function() { return ownerReference.ChangeState === 'I'; } : function() {
           return ownerReference.entityAspect.entityState === breeze.EntityState.Added;
       });
       ownerReference.isDeleted = (isPOCO === true ? function() { return ownerReference.ChangeState === 'D'; } : function() {
           return ownerReference.entityAspect.entityState === breeze.EntityState.Deleted;
       });
       ownerReference.isModified = (isPOCO === true ? function() { return ownerReference.ChangeState === 'U'; } : function() {
           return ownerReference.entityAspect.entityState === breeze.EntityState.Modified;
       });
       ownerReference.isDetached = (isPOCO === true ? function() { return false; } : function() {
           return ownerReference.entityAspect.entityState === breeze.EntityState.Detached;
       });
       ownerReference.isUnchanged = (isPOCO === true ? function() { return ownerReference.ChangeState === 'N'; } : function() {
           return ownerReference.entityAspect.entityState === breeze.EntityState.Unchanged;
       });
       ownerReference.setModified = (isPOCO === true ? function() { ownerReference.ChangeState = 'U'; } : function() {
           ownerReference.entityAspect.setModified();
       });
       ownerReference.setUnchanged = (isPOCO === true ? function() { ownerReference.ChangeState = 'N'; } : function() {
           ownerReference.entityAspect.setUnchanged();
       });
       ownerReference.serverDataType = [];
       ownerReference.serverDataType['BigIntVenda'] = 'L';
       ownerReference.serverDataType['BitVenda'] = 'B';
       ownerReference.serverDataType['ComboboxVenda'] = 'Y';
       ownerReference.serverDataType['DatetimeVenda'] = 'T';
       ownerReference.serverDataType['DecimalVenda'] = 'D';
       ownerReference.serverDataType['GuidVenda'] = 'G';
       ownerReference.serverDataType['IdCliente'] = 'I';
       ownerReference.serverDataType['IdLoja'] = 'I';
       ownerReference.serverDataType['IdVenda'] = 'I';
       ownerReference.serverDataType['IntVenda'] = 'I';
       ownerReference.serverDataType['SmallIntVenda'] = 'H';
       ownerReference.serverDataType['StringLoja'] = 'S';
       ownerReference.serverDataType['StringVenda'] = 'S';
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
       //#region Lookup Extended Methods
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
           if (lookupName === 'LookUpCliente') {
               displayName = ' de Cliente';
           }
           if (lookupName === 'LookUpLoja') {
               displayName = ' de Loja';
           }
           return 'Seleção' + displayName;
       };
    
       ownerReference.getSpecializedLookup = function (lookupName, lookupInfo, fieldToSearch, valueToSearch, ownerReference, allowMultiSelectionInSearch) {
           var specializedLookup = '';
           if (lookupName === 'LookUpLoja') {
               specializedLookup = { moduleName: 'pkg_linx-demo-bv-spa/viewmodels/UILookUpDentroOutraUI', uiSettings: { modalForm: modal, fieldToSearch: fieldToSearch, valueToSearch: valueToSearch, lookupInfo: lookupInfo, lookupName: lookupName, ownerReference: ownerReference, removeDataToolbar: false, shareParentBO: false, useFilterFromParent: false, parentSelectorDataName: '', canClear: true, canSearch: true, canAddNew: false, canEdit: false, canDelete: false, canCustomSearch: true, canPrint: false, canLayout: false, canNavigate: true, allowMultiSelectionInSearch: allowMultiSelectionInSearch, applyFilterToParent: false, noSearch: false, parentFieldsRelation: [], detailFieldsRelation: [] } 
               };
           }
           return specializedLookup;
       };
    
       ownerReference.getSubQueryFilterFromLookUpCliente = function (propertyName) {
           var filter = '';
           return filter;
       }
       ownerReference.getSubQueryFilterFromLookUpLoja = function (propertyName) {
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
           if (!isNullOrEmpty(ownerReference.VendaItemList()) && ownerReference.VendaItemList().length > 0) {
              var details = [].concat(ownerReference.VendaItemList());
              for (var idx = 0; idx < details.length; idx++) {
                details[idx].delete();
              }
           }
           if (ownerReference.ChangeState == 'I') {
               if (parent && (typeof parent.VendaList === 'function')) { 
                   parent.VendaList.remove(ownerReference); 
               }
               else {
                   vm.dataView.remove(ownerReference);
               }
           }
           else {
               if (ownerReference.ChangeState == 'N') { ownerReference.createOriginal(); }
               ownerReference.ChangeState = 'D'; // mark for deletion
           }
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
       ownerReference.myProperties = [ 'BigIntVenda','BitVenda','ComboboxVenda','DatetimeVenda','DecimalVenda','GuidVenda','IdCliente','IdLoja','IdVenda','IntVenda','SmallIntVenda','StringLoja','StringVenda' ];
       ownerReference.queryRequiredProperties = {  };
       ownerReference.excludedFilters = [];
       ownerReference.getCurrentElements = function() {
           var result = [ ownerReference ];
       if (!isNullOrEmpty(ownerReference.currentVendaItem())) { result = result.concat(ownerReference.currentVendaItem().getCurrentElements()); }
           return result;
       };
       ownerReference.checkForSendingAllRowsToServer = function() {
       };
       ownerReference.GetJsWhereDetailRelationForVendaItem = function(customParentRelation) {
       return 'VendaItem{' + (!isNullOrEmpty(customParentRelation) ? customParentRelation : 'IdVenda#==#' + ownerReference.serverDataType['IdVenda'] + getAbsoluteValue(ownerReference.IdVenda).toString()) + '}';    
       }
       ownerReference.VendaItemIsLoaded = false;
       ownerReference.detailsLoaded = function() {
           return ownerReference.VendaItemIsLoaded;
       }
       ownerReference.atLeastOneDetailLoaded = function() {
           return ownerReference.VendaItemIsLoaded;
       }
       ownerReference.adjustDetailsLoaded = function(value) {
           ownerReference.VendaItemIsLoaded = value;
           if (value === false && ownerReference.isPOCO)
               ownerReference.VendaItemList([]);
       }
       ownerReference.fillDetails = function(force, detailName, noInnerUIs, noWait, callback, customParentRelation) {
          if (typeof force === 'undefined') force = false;
          if (force) vm.clearInnerUIs(ownerReference);
          if (!noInnerUIs) vm.queryInnerUIs(ownerReference);
          if (ownerReference.isAdded()) {
            ownerReference.VendaItemIsLoaded = true;
          }
          var _VendaItemRemoteComplete = false;
          var detachList_VendaItem = [];
          if (force) {
               if (isNullOrEmpty(detailName) || detailName == 'VendaItem') ownerReference.VendaItemIsLoaded = false;
               if ((isNullOrEmpty(detailName) || detailName == 'VendaItem') && ownerReference.VendaItemList && ownerReference.VendaItemList().length > 0) {
                   if (ownerReference.isPOCO) {
                       ownerReference.VendaItemList([]);
                   } else {
                       var detailList = ownerReference.VendaItemList();
                       for (var idx = detailList.length - 1; idx >= 0; idx--) {
                           detachList_VendaItem.push(detailList[idx]);
                       }
                   }
               }
          }
    
          if (!ownerReference.VendaItemIsLoaded) {
            //Load VendaItemList
            if (isNullOrEmpty(detailName) || detailName === 'VendaItem') {
              ownerReference.VendaItemIsLoaded = true;
              _VendaItemRemoteComplete = (ownerReference.VendaItemList && ownerReference.VendaItemList().length > 0);
              if ((force || !ownerReference.VendaItemList || ownerReference.VendaItemList().length === 0) && (!isNullOrEmpty(getAbsoluteValue(ownerReference.IdVenda)))) {
                var navQuery = EntityQuery.from('GetVendaItemByEntitySearchNoAssociations').noTracking(ownerReference.isPOCO === true)
                .orderBy('IdVendaItem asc')
                    .withParameters({ jEntitySearch: ownerReference.GetJsWhereDetailRelationForVendaItem(customParentRelation) })    ;
                if (!vm.dataToolbar._noBusyLoading) vm.showProcessing('Pesquisando detalhes...');
                manager.executeQuery(navQuery).then(function (data) { if (ownerReference.isPOCO) { for (var idx = 0; idx < data.results.length; idx++) { initializePOCO(data.results[idx], 'VendaItem'); data.results[idx].Venda = ko.observable(ownerReference); } ownerReference.VendaItemList(data.results); } 
                   if (!ownerReference.isPOCO && detachList_VendaItem.length > 0)
                   {
                       for (var idx = 0; idx < detachList_VendaItem.length; idx++)
                       {
                           if (!data.results.contains(detachList_VendaItem[idx]))
                               detachEntity(detachList_VendaItem[idx]);
                           else {
                               if (force && detachList_VendaItem[idx].atLeastOneDetailLoaded())
                                   detachList_VendaItem[idx].fillDetails(force, '', false, noWait);
                           }
                       }
                   }
                   ownerReference.setCurrentDetails('VendaItem'); notifyPresentation('VendaItemList');
                   _VendaItemRemoteComplete = true;
                   if (callback && (!isNullOrEmpty(detailName) || (_VendaItemRemoteComplete))) { callback(); }
                }).fail(queryFailed).fin(function() { if (!vm.dataToolbar._noBusyLoading) vm.closeProcessing(); });
              } else { ownerReference.setCurrentDetails('VendaItem'); notifyPresentation('VendaItemList'); }
            } else { _VendaItemRemoteComplete = true; if (!ownerReference.VendaItemIsLoaded && ownerReference.VendaItemList && ownerReference.VendaItemList().length > 0) { ownerReference.VendaItemIsLoaded = true; ownerReference.setCurrentDetails('VendaItem'); } }
          } else { 
            if (isNullOrEmpty(detailName) || detailName == 'VendaItem') {
               notifyPresentation('VendaItemList');
               ownerReference.setCurrentDetails('VendaItem');
            }
            _VendaItemRemoteComplete = true;
          }
          if (callback && ((!isNullOrEmpty(detailName) && (eval('_' + detailName + 'RemoteComplete && ownerReference.' + detailName + 'IsLoaded') == true)) || (isNullOrEmpty(detailName) && (_VendaItemRemoteComplete)))) { callback(); }
       };
       //Select first element as a current item of each detail
       ownerReference.setCurrentDetails = function(detailName, clearing) {
          if ((isNullOrEmpty(detailName) || detailName === 'VendaItem')) {
               if (ownerReference.VendaItemList().length > 0) { ownerReference.currentVendaItem(ownerReference.VendaItemList()[0]); if (clearing == null || clearing === false) ownerReference.currentVendaItem().fillDetails(); }
               else { ownerReference.currentVendaItem(null); ownerReference.notifyEmptyDetails('VendaItem'); }
          }
       };
       ownerReference.notifyEmptyDetails = function(detailName) {
          if (detailName === 'VendaItem') {
               notifyPresentation('VendaItemList');
               vm.queryInnerUIs(null, 'VendaItem');
          }
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
    //#region Adjust details already loaded for a POCO reference
       if (isPOCO === true) {
           if ((typeof ownerReference.VendaItemList === 'function') && ownerReference.VendaItemList().length > 0) {
                for(var idx = 0; idx < ownerReference.VendaItemList().length; idx++) { VendaItemInitializer(ownerReference.VendaItemList()[idx], isPOCO); }
           }
       }
    //#endregion Adjust details already loaded for a POCO reference
    };
    metadataStore.registerEntityTypeCtor("Venda", null, VendaInitializer);
    
    // Configure VendaItem data type
    metadataStore.addEntityType({
    shortName: "VendaItem",
    namespace: "Linx.Demo.BV.ModalExterna",
    autoGeneratedKeyType: AutoGeneratedKeyType.Identity,
    dataProperties: {
    BigIntVendaItem: { dataType: DataType.Int64, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,BitVendaItem: { dataType: DataType.Boolean, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,ComboboxVendaItem: { dataType: DataType.Byte, isNullable: false, isPartOfKey: false, validators: [ Validator.hasValueValidator]  }
    ,ComboboxVendaItemName: { dataType: DataType.String, isNullable: false, isPartOfKey: false, validators: [] }
    ,DatetimeVendaItem: { dataType: DataType.DateTime, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,DecimalVendaItem: { dataType: DataType.Decimal, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,GuidVendaItem: { dataType: DataType.Guid, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,IdCliente: { dataType: DataType.Int32, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,IdVenda: { dataType: DataType.Int32, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,IdVendaItem: { dataType: DataType.Int32, isNullable: false, isPartOfKey: true, validators: [ Validator.hasValueValidator]  }
    ,IntVenda: { dataType: DataType.Int32, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,IntVendaItem: { dataType: DataType.Int32, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,SmallIntVendaItem: { dataType: DataType.Int16, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,StringVendaItem: { dataType: DataType.String, maxLength: 50, isNullable: true, isPartOfKey: false, validators: [ Validator.maxLength( {maxLength: 50})]  }
    ,ChangeState: { dataType: DataType.String, isNullable: true, isPartOfKey: false, validators: [] }
                    },
    navigationProperties: {
    // Returns a single parent and associates with Details
    Venda: { entityTypeName: "Venda:#Linx.Demo.BV.ModalExterna", isScalar: true, foreignKeyNames: ["IdVenda"], associationName: "FK_Venda_VendaItem" }
    // Returns collections of details and associates with Parent
                          }
    });
    lookUpProperties['VendaItem'] = {};
    var VendaItemInitializer = function (ownerReference, isPOCO) {
       ownerReference.RowDataId = (isPOCO === true ? getNextSequence('VendaItem') : ko.observable(getNextSequence('VendaItem')));
        //Start Property Definitions
        var _bigIntVendaItem = ownerReference.BigIntVendaItem;
        Object.defineProperty(ownerReference, 'BigIntVendaItem', {
          get: function() { return _bigIntVendaItem; },
          set: function(newValue) { var oldValue = _bigIntVendaItem; _bigIntVendaItem = newValue; if (!entityPropChanged(ownerReference, 'BigIntVendaItem', oldValue, newValue)) { _bigIntVendaItem = oldValue; } }
        });
        var _bitVendaItem = ownerReference.BitVendaItem;
        Object.defineProperty(ownerReference, 'BitVendaItem', {
          get: function() { return _bitVendaItem; },
          set: function(newValue) { var oldValue = _bitVendaItem; _bitVendaItem = newValue; if (!entityPropChanged(ownerReference, 'BitVendaItem', oldValue, newValue)) { _bitVendaItem = oldValue; } }
        });
        var _comboboxVendaItem = ownerReference.ComboboxVendaItem;
        Object.defineProperty(ownerReference, 'ComboboxVendaItem', {
          get: function() { return _comboboxVendaItem; },
          set: function(newValue) { var oldValue = _comboboxVendaItem; _comboboxVendaItem = newValue; if (!entityPropChanged(ownerReference, 'ComboboxVendaItem', oldValue, newValue)) { _comboboxVendaItem = oldValue; } else { _comboboxVendaItemName = (dataDomains.getName('LX_VENDA_ITEM', newValue)); } }
        });
        var _comboboxVendaItemName = ownerReference.ComboboxVendaItemName;
        Object.defineProperty(ownerReference, 'ComboboxVendaItemName', {
          get: function() { return _comboboxVendaItemName; },
          set: function(newValue) { var oldValue = _comboboxVendaItemName; _comboboxVendaItemName = newValue; if (!entityPropChanged(ownerReference, 'ComboboxVendaItemName', oldValue, newValue)) { _comboboxVendaItemName = oldValue; } else { _comboboxVendaItem = (dataDomains.getId('LX_VENDA_ITEM', newValue)); } }
        });
        var _datetimeVendaItem = ownerReference.DatetimeVendaItem;
        Object.defineProperty(ownerReference, 'DatetimeVendaItem', {
          get: function() { return _datetimeVendaItem; },
          set: function(newValue) { var oldValue = _datetimeVendaItem; _datetimeVendaItem = newValue; if (!entityPropChanged(ownerReference, 'DatetimeVendaItem', oldValue, newValue)) { _datetimeVendaItem = oldValue; } }
        });
        var _decimalVendaItem = ownerReference.DecimalVendaItem;
        Object.defineProperty(ownerReference, 'DecimalVendaItem', {
          get: function() { return _decimalVendaItem; },
          set: function(newValue) { var oldValue = _decimalVendaItem; _decimalVendaItem = newValue; if (!entityPropChanged(ownerReference, 'DecimalVendaItem', oldValue, newValue)) { _decimalVendaItem = oldValue; } }
        });
        var _guidVendaItem = ownerReference.GuidVendaItem;
        Object.defineProperty(ownerReference, 'GuidVendaItem', {
          get: function() { return _guidVendaItem; },
          set: function(newValue) { var oldValue = _guidVendaItem; _guidVendaItem = newValue; if (!entityPropChanged(ownerReference, 'GuidVendaItem', oldValue, newValue)) { _guidVendaItem = oldValue; } }
        });
        var _idCliente = ownerReference.IdCliente;
        Object.defineProperty(ownerReference, 'IdCliente', {
          get: function() { return _idCliente; },
          set: function(newValue) { var oldValue = _idCliente; _idCliente = newValue; if (!entityPropChanged(ownerReference, 'IdCliente', oldValue, newValue)) { _idCliente = oldValue; } }
        });
        var _idVenda = ownerReference.IdVenda;
        Object.defineProperty(ownerReference, 'IdVenda', {
          get: function() { return _idVenda; },
          set: function(newValue) { var oldValue = _idVenda; _idVenda = newValue; if (!entityPropChanged(ownerReference, 'IdVenda', oldValue, newValue)) { _idVenda = oldValue; } }
        });
        var _idVendaItem = ownerReference.IdVendaItem;
        Object.defineProperty(ownerReference, 'IdVendaItem', {
          get: function() { return _idVendaItem; },
          set: function(newValue) { var oldValue = _idVendaItem; _idVendaItem = newValue; if (!entityPropChanged(ownerReference, 'IdVendaItem', oldValue, newValue)) { _idVendaItem = oldValue; } }
        });
        var _intVenda = ownerReference.IntVenda;
        Object.defineProperty(ownerReference, 'IntVenda', {
          get: function() { return _intVenda; },
          set: function(newValue) { var oldValue = _intVenda; _intVenda = newValue; if (!entityPropChanged(ownerReference, 'IntVenda', oldValue, newValue)) { _intVenda = oldValue; } }
        });
        var _intVendaItem = ownerReference.IntVendaItem;
        Object.defineProperty(ownerReference, 'IntVendaItem', {
          get: function() { return _intVendaItem; },
          set: function(newValue) { var oldValue = _intVendaItem; _intVendaItem = newValue; if (!entityPropChanged(ownerReference, 'IntVendaItem', oldValue, newValue)) { _intVendaItem = oldValue; } }
        });
        var _smallIntVendaItem = ownerReference.SmallIntVendaItem;
        Object.defineProperty(ownerReference, 'SmallIntVendaItem', {
          get: function() { return _smallIntVendaItem; },
          set: function(newValue) { var oldValue = _smallIntVendaItem; _smallIntVendaItem = newValue; if (!entityPropChanged(ownerReference, 'SmallIntVendaItem', oldValue, newValue)) { _smallIntVendaItem = oldValue; } }
        });
        var _stringVendaItem = ownerReference.StringVendaItem;
        Object.defineProperty(ownerReference, 'StringVendaItem', {
          get: function() { return _stringVendaItem; },
          set: function(newValue) { var oldValue = _stringVendaItem; _stringVendaItem = newValue; if (!entityPropChanged(ownerReference, 'StringVendaItem', oldValue, newValue)) { _stringVendaItem = oldValue; } }
        });
        //End Property Definitions
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
             var filterByKey = 'VendaItem{' + 'IdVendaItem#==#I' + getAbsoluteValue(ownerReference.IdVendaItem).toString() + '}';
             if (!ownerReference.isPOCO && ownerReference.entityAspect && !ownerReference.isDetached() && !ownerReference.isUnchanged()) ownerReference.entityAspect.setUnchanged();
             return dataContext.getVendaItemByEntitySearchNoAssociations(filterByKey, 0, 0, false, true, ownerReference.isPOCO === true).then(querySucceeded);
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
       ownerReference.isAdded = (isPOCO === true ? function() { return ownerReference.ChangeState === 'I'; } : function() {
           return ownerReference.entityAspect.entityState === breeze.EntityState.Added;
       });
       ownerReference.isDeleted = (isPOCO === true ? function() { return ownerReference.ChangeState === 'D'; } : function() {
           return ownerReference.entityAspect.entityState === breeze.EntityState.Deleted;
       });
       ownerReference.isModified = (isPOCO === true ? function() { return ownerReference.ChangeState === 'U'; } : function() {
           return ownerReference.entityAspect.entityState === breeze.EntityState.Modified;
       });
       ownerReference.isDetached = (isPOCO === true ? function() { return false; } : function() {
           return ownerReference.entityAspect.entityState === breeze.EntityState.Detached;
       });
       ownerReference.isUnchanged = (isPOCO === true ? function() { return ownerReference.ChangeState === 'N'; } : function() {
           return ownerReference.entityAspect.entityState === breeze.EntityState.Unchanged;
       });
       ownerReference.setModified = (isPOCO === true ? function() { ownerReference.ChangeState = 'U'; } : function() {
           ownerReference.entityAspect.setModified();
       });
       ownerReference.setUnchanged = (isPOCO === true ? function() { ownerReference.ChangeState = 'N'; } : function() {
           ownerReference.entityAspect.setUnchanged();
       });
       ownerReference.serverDataType = [];
       ownerReference.serverDataType['BigIntVendaItem'] = 'L';
       ownerReference.serverDataType['BitVendaItem'] = 'B';
       ownerReference.serverDataType['ComboboxVendaItem'] = 'Y';
       ownerReference.serverDataType['DatetimeVendaItem'] = 'T';
       ownerReference.serverDataType['DecimalVendaItem'] = 'D';
       ownerReference.serverDataType['GuidVendaItem'] = 'G';
       ownerReference.serverDataType['IdCliente'] = 'I';
       ownerReference.serverDataType['IdVenda'] = 'I';
       ownerReference.serverDataType['IdVendaItem'] = 'I';
       ownerReference.serverDataType['IntVenda'] = 'I';
       ownerReference.serverDataType['IntVendaItem'] = 'I';
       ownerReference.serverDataType['SmallIntVendaItem'] = 'H';
       ownerReference.serverDataType['StringVendaItem'] = 'S';
       ownerReference.typeName = 'VendaItem';
       ownerReference.isPrimaryKey = function(propertyName) {
           var keys = [ 'IdVendaItem' ];
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
           var parent = getAbsoluteValue(ownerReference.Venda);
           if (ownerReference.ChangeState == 'I') {
               if (parent && (typeof parent.VendaItemList === 'function')) { 
                   parent.VendaItemList.remove(ownerReference); 
               }
               else {
                   vm.dataView.remove(ownerReference);
               }
               delete ownerReference.Venda;
           }
           else {
               if (ownerReference.ChangeState == 'N') { ownerReference.createOriginal(); }
               ownerReference.ChangeState = 'D'; // mark for deletion
           }
           if (parent && (typeof parent.setCurrentDetails === 'function') && (typeof parent.VendaItemList === 'function') && parent.VendaItemList().length == 0) parent.setCurrentDetails('VendaItem');
       };
       ownerReference.setParentAsModified = function() {
       var parent = getAbsoluteValue(ownerReference.Venda);
       if (parent) {
           if (parent.isUnchanged()) {
               parent.setModified(); 
           }
           parent.setParentAsModified();
       }
       };
       ownerReference.getParent = function() {
           return getAbsoluteValue(ownerReference.Venda);
       };
       ownerReference.getSelfList = function() {
           var parent = ownerReference.getParent();
           if (!isNullOrEmpty(parent)) {
               return getAbsoluteValue(parent.VendaItemList);
           } else { return null; }
       };
       ownerReference.namespace = 'Linx.Demo.BV.ModalExterna';
       ownerReference.myProperties = [ 'BigIntVendaItem','BitVendaItem','ComboboxVendaItem','DatetimeVendaItem','DecimalVendaItem','GuidVendaItem','IdCliente','IdVenda','IdVendaItem','IntVenda','IntVendaItem','SmallIntVendaItem','StringVendaItem' ];
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
           ownerReference.ComboboxVendaItemName.subscribe(
               function (newValue) {
                   if (newValue == null) { ownerReference.ComboboxVendaItemName(''); return; }
                   var value = (dataDomains.getId('LX_VENDA_ITEM', newValue));
                   if (value != ownerReference.ComboboxVendaItem()) {
                       ownerReference.ComboboxVendaItem(value);
                   }
            });
    
           ownerReference.ComboboxVendaItem.subscribe(
           function (newValue) {
                   if (newValue == null) { ownerReference.ComboboxVendaItem(0); return; }
                   var value = dataDomains.getName('LX_VENDA_ITEM', newValue);
                   if (value != ownerReference.ComboboxVendaItemName()) {
                       ownerReference.ComboboxVendaItemName(value);
               }
           });
       }
    //#endregion Extended Domain Names
    };
    metadataStore.registerEntityTypeCtor("VendaItem", null, VendaItemInitializer);
    
    // Configure FormaPagamento data type
    metadataStore.addEntityType({
    shortName: "FormaPagamento",
    namespace: "Linx.Demo.BV.ModalExterna",
    autoGeneratedKeyType: AutoGeneratedKeyType.Identity,
    dataProperties: {
    BigIntFormaPagamento: { dataType: DataType.Int64, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,BitFormaPagamento: { dataType: DataType.Boolean, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,ComboboxFormaPagamento: { dataType: DataType.Byte, isNullable: false, isPartOfKey: false, validators: [ Validator.hasValueValidator]  }
    ,ComboboxFormaPagamentoName: { dataType: DataType.String, isNullable: false, isPartOfKey: false, validators: [] }
    ,DatetimeFormaPagamento: { dataType: DataType.DateTime, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,DecimalFormaPagamento: { dataType: DataType.Decimal, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,GuidFormaPagamento: { dataType: DataType.Guid, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,IdFormaPagamento: { dataType: DataType.Int32, isNullable: false, isPartOfKey: true, validators: [ Validator.hasValueValidator]  }
    ,IdVenda: { dataType: DataType.Int32, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,IntFormaPagamento: { dataType: DataType.Int32, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,SmallIntFormaPagamento: { dataType: DataType.Int16, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,StringFormaPagamento: { dataType: DataType.String, maxLength: 50, isNullable: true, isPartOfKey: false, validators: [ Validator.maxLength( {maxLength: 50})]  }
    ,ChangeState: { dataType: DataType.String, isNullable: true, isPartOfKey: false, validators: [] }
                    },
    navigationProperties: {
    // Returns collections of details and associates with Parent
                          }
    });
    lookUpProperties['FormaPagamento'] = {IdVenda: 'LookUpVenda'};
    var FormaPagamentoInitializer = function (ownerReference, isPOCO) {
       ownerReference.RowDataId = (isPOCO === true ? getNextSequence('FormaPagamento') : ko.observable(getNextSequence('FormaPagamento')));
        //Start Property Definitions
        var _bigIntFormaPagamento = ownerReference.BigIntFormaPagamento;
        Object.defineProperty(ownerReference, 'BigIntFormaPagamento', {
          get: function() { return _bigIntFormaPagamento; },
          set: function(newValue) { var oldValue = _bigIntFormaPagamento; _bigIntFormaPagamento = newValue; if (!entityPropChanged(ownerReference, 'BigIntFormaPagamento', oldValue, newValue)) { _bigIntFormaPagamento = oldValue; } }
        });
        var _bitFormaPagamento = ownerReference.BitFormaPagamento;
        Object.defineProperty(ownerReference, 'BitFormaPagamento', {
          get: function() { return _bitFormaPagamento; },
          set: function(newValue) { var oldValue = _bitFormaPagamento; _bitFormaPagamento = newValue; if (!entityPropChanged(ownerReference, 'BitFormaPagamento', oldValue, newValue)) { _bitFormaPagamento = oldValue; } }
        });
        var _comboboxFormaPagamento = ownerReference.ComboboxFormaPagamento;
        Object.defineProperty(ownerReference, 'ComboboxFormaPagamento', {
          get: function() { return _comboboxFormaPagamento; },
          set: function(newValue) { var oldValue = _comboboxFormaPagamento; _comboboxFormaPagamento = newValue; if (!entityPropChanged(ownerReference, 'ComboboxFormaPagamento', oldValue, newValue)) { _comboboxFormaPagamento = oldValue; } else { _comboboxFormaPagamentoName = (dataDomains.getName('LX_FORMA_PAGAMENTO', newValue)); } }
        });
        var _comboboxFormaPagamentoName = ownerReference.ComboboxFormaPagamentoName;
        Object.defineProperty(ownerReference, 'ComboboxFormaPagamentoName', {
          get: function() { return _comboboxFormaPagamentoName; },
          set: function(newValue) { var oldValue = _comboboxFormaPagamentoName; _comboboxFormaPagamentoName = newValue; if (!entityPropChanged(ownerReference, 'ComboboxFormaPagamentoName', oldValue, newValue)) { _comboboxFormaPagamentoName = oldValue; } else { _comboboxFormaPagamento = (dataDomains.getId('LX_FORMA_PAGAMENTO', newValue)); } }
        });
        var _datetimeFormaPagamento = ownerReference.DatetimeFormaPagamento;
        Object.defineProperty(ownerReference, 'DatetimeFormaPagamento', {
          get: function() { return _datetimeFormaPagamento; },
          set: function(newValue) { var oldValue = _datetimeFormaPagamento; _datetimeFormaPagamento = newValue; if (!entityPropChanged(ownerReference, 'DatetimeFormaPagamento', oldValue, newValue)) { _datetimeFormaPagamento = oldValue; } }
        });
        var _decimalFormaPagamento = ownerReference.DecimalFormaPagamento;
        Object.defineProperty(ownerReference, 'DecimalFormaPagamento', {
          get: function() { return _decimalFormaPagamento; },
          set: function(newValue) { var oldValue = _decimalFormaPagamento; _decimalFormaPagamento = newValue; if (!entityPropChanged(ownerReference, 'DecimalFormaPagamento', oldValue, newValue)) { _decimalFormaPagamento = oldValue; } }
        });
        var _guidFormaPagamento = ownerReference.GuidFormaPagamento;
        Object.defineProperty(ownerReference, 'GuidFormaPagamento', {
          get: function() { return _guidFormaPagamento; },
          set: function(newValue) { var oldValue = _guidFormaPagamento; _guidFormaPagamento = newValue; if (!entityPropChanged(ownerReference, 'GuidFormaPagamento', oldValue, newValue)) { _guidFormaPagamento = oldValue; } }
        });
        var _idFormaPagamento = ownerReference.IdFormaPagamento;
        Object.defineProperty(ownerReference, 'IdFormaPagamento', {
          get: function() { return _idFormaPagamento; },
          set: function(newValue) { var oldValue = _idFormaPagamento; _idFormaPagamento = newValue; if (!entityPropChanged(ownerReference, 'IdFormaPagamento', oldValue, newValue)) { _idFormaPagamento = oldValue; } }
        });
        var _idVenda = ownerReference.IdVenda;
        Object.defineProperty(ownerReference, 'IdVenda', {
          get: function() { return _idVenda; },
          set: function(newValue) { var oldValue = _idVenda; _idVenda = newValue; if (!entityPropChanged(ownerReference, 'IdVenda', oldValue, newValue)) { _idVenda = oldValue; } }
        });
        var _intFormaPagamento = ownerReference.IntFormaPagamento;
        Object.defineProperty(ownerReference, 'IntFormaPagamento', {
          get: function() { return _intFormaPagamento; },
          set: function(newValue) { var oldValue = _intFormaPagamento; _intFormaPagamento = newValue; if (!entityPropChanged(ownerReference, 'IntFormaPagamento', oldValue, newValue)) { _intFormaPagamento = oldValue; } }
        });
        var _smallIntFormaPagamento = ownerReference.SmallIntFormaPagamento;
        Object.defineProperty(ownerReference, 'SmallIntFormaPagamento', {
          get: function() { return _smallIntFormaPagamento; },
          set: function(newValue) { var oldValue = _smallIntFormaPagamento; _smallIntFormaPagamento = newValue; if (!entityPropChanged(ownerReference, 'SmallIntFormaPagamento', oldValue, newValue)) { _smallIntFormaPagamento = oldValue; } }
        });
        var _stringFormaPagamento = ownerReference.StringFormaPagamento;
        Object.defineProperty(ownerReference, 'StringFormaPagamento', {
          get: function() { return _stringFormaPagamento; },
          set: function(newValue) { var oldValue = _stringFormaPagamento; _stringFormaPagamento = newValue; if (!entityPropChanged(ownerReference, 'StringFormaPagamento', oldValue, newValue)) { _stringFormaPagamento = oldValue; } }
        });
        //End Property Definitions
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
             var filterByKey = 'FormaPagamento{' + 'IdFormaPagamento#==#I' + getAbsoluteValue(ownerReference.IdFormaPagamento).toString() + '}';
             if (!ownerReference.isPOCO && ownerReference.entityAspect && !ownerReference.isDetached() && !ownerReference.isUnchanged()) ownerReference.entityAspect.setUnchanged();
             return dataContext.getFormaPagamentoByEntitySearchNoAssociations(filterByKey, 0, 0, false, true, ownerReference.isPOCO === true).then(querySucceeded);
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
       ownerReference.isAdded = (isPOCO === true ? function() { return ownerReference.ChangeState === 'I'; } : function() {
           return ownerReference.entityAspect.entityState === breeze.EntityState.Added;
       });
       ownerReference.isDeleted = (isPOCO === true ? function() { return ownerReference.ChangeState === 'D'; } : function() {
           return ownerReference.entityAspect.entityState === breeze.EntityState.Deleted;
       });
       ownerReference.isModified = (isPOCO === true ? function() { return ownerReference.ChangeState === 'U'; } : function() {
           return ownerReference.entityAspect.entityState === breeze.EntityState.Modified;
       });
       ownerReference.isDetached = (isPOCO === true ? function() { return false; } : function() {
           return ownerReference.entityAspect.entityState === breeze.EntityState.Detached;
       });
       ownerReference.isUnchanged = (isPOCO === true ? function() { return ownerReference.ChangeState === 'N'; } : function() {
           return ownerReference.entityAspect.entityState === breeze.EntityState.Unchanged;
       });
       ownerReference.setModified = (isPOCO === true ? function() { ownerReference.ChangeState = 'U'; } : function() {
           ownerReference.entityAspect.setModified();
       });
       ownerReference.setUnchanged = (isPOCO === true ? function() { ownerReference.ChangeState = 'N'; } : function() {
           ownerReference.entityAspect.setUnchanged();
       });
       ownerReference.serverDataType = [];
       ownerReference.serverDataType['BigIntFormaPagamento'] = 'L';
       ownerReference.serverDataType['BitFormaPagamento'] = 'B';
       ownerReference.serverDataType['ComboboxFormaPagamento'] = 'Y';
       ownerReference.serverDataType['DatetimeFormaPagamento'] = 'T';
       ownerReference.serverDataType['DecimalFormaPagamento'] = 'D';
       ownerReference.serverDataType['GuidFormaPagamento'] = 'G';
       ownerReference.serverDataType['IdFormaPagamento'] = 'I';
       ownerReference.serverDataType['IdVenda'] = 'I';
       ownerReference.serverDataType['IntFormaPagamento'] = 'I';
       ownerReference.serverDataType['SmallIntFormaPagamento'] = 'H';
       ownerReference.serverDataType['StringFormaPagamento'] = 'S';
       ownerReference.typeName = 'FormaPagamento';
       ownerReference.isPrimaryKey = function(propertyName) {
           var keys = [ 'IdFormaPagamento' ];
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
       //#region Lookup Extended Methods
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
           if (lookupName === 'LookUpVenda') {
               displayName = ' de Venda';
           }
           return 'Seleção' + displayName;
       };
    
       ownerReference.getSpecializedLookup = function (lookupName, lookupInfo, fieldToSearch, valueToSearch, ownerReference, allowMultiSelectionInSearch) {
           var specializedLookup = '';
           return specializedLookup;
       };
    
       ownerReference.getSubQueryFilterFromLookUpVenda = function (propertyName) {
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
           if (ownerReference.ChangeState == 'I') {
               if (parent && (typeof parent.FormaPagamentoList === 'function')) { 
                   parent.FormaPagamentoList.remove(ownerReference); 
               }
               else {
                   vm.dataView.remove(ownerReference);
               }
           }
           else {
               if (ownerReference.ChangeState == 'N') { ownerReference.createOriginal(); }
               ownerReference.ChangeState = 'D'; // mark for deletion
           }
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
       ownerReference.myProperties = [ 'BigIntFormaPagamento','BitFormaPagamento','ComboboxFormaPagamento','DatetimeFormaPagamento','DecimalFormaPagamento','GuidFormaPagamento','IdFormaPagamento','IdVenda','IntFormaPagamento','SmallIntFormaPagamento','StringFormaPagamento' ];
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
           ownerReference.ComboboxFormaPagamentoName.subscribe(
               function (newValue) {
                   if (newValue == null) { ownerReference.ComboboxFormaPagamentoName(''); return; }
                   var value = (dataDomains.getId('LX_FORMA_PAGAMENTO', newValue));
                   if (value != ownerReference.ComboboxFormaPagamento()) {
                       ownerReference.ComboboxFormaPagamento(value);
                   }
            });
    
           ownerReference.ComboboxFormaPagamento.subscribe(
           function (newValue) {
                   if (newValue == null) { ownerReference.ComboboxFormaPagamento(0); return; }
                   var value = dataDomains.getName('LX_FORMA_PAGAMENTO', newValue);
                   if (value != ownerReference.ComboboxFormaPagamentoName()) {
                       ownerReference.ComboboxFormaPagamentoName(value);
               }
           });
       }
    //#endregion Extended Domain Names
    };
    metadataStore.registerEntityTypeCtor("FormaPagamento", null, FormaPagamentoInitializer);
    
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
    ,GuidLoja: { dataType: DataType.Guid, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,IdLoja: { dataType: DataType.Int32, isNullable: false, isPartOfKey: true, validators: [ Validator.hasValueValidator]  }
    ,IntLoja: { dataType: DataType.Int32, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,SmallIntLoja: { dataType: DataType.Int16, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,StringLoja: { dataType: DataType.String, maxLength: 50, isNullable: true, isPartOfKey: false, validators: [ Validator.maxLength( {maxLength: 50})]  }
    ,ChangeState: { dataType: DataType.String, isNullable: true, isPartOfKey: false, validators: [] }
                    },
    navigationProperties: {
    // Returns collections of details and associates with Parent
                          }
    });
    lookUpProperties['Loja'] = {};
    var LojaInitializer = function (ownerReference, isPOCO) {
       ownerReference.RowDataId = (isPOCO === true ? getNextSequence('Loja') : ko.observable(getNextSequence('Loja')));
        //Start Property Definitions
        var _bigIntLoja = ownerReference.BigIntLoja;
        Object.defineProperty(ownerReference, 'BigIntLoja', {
          get: function() { return _bigIntLoja; },
          set: function(newValue) { var oldValue = _bigIntLoja; _bigIntLoja = newValue; if (!entityPropChanged(ownerReference, 'BigIntLoja', oldValue, newValue)) { _bigIntLoja = oldValue; } }
        });
        var _bitLoja = ownerReference.BitLoja;
        Object.defineProperty(ownerReference, 'BitLoja', {
          get: function() { return _bitLoja; },
          set: function(newValue) { var oldValue = _bitLoja; _bitLoja = newValue; if (!entityPropChanged(ownerReference, 'BitLoja', oldValue, newValue)) { _bitLoja = oldValue; } }
        });
        var _comboboxLoja = ownerReference.ComboboxLoja;
        Object.defineProperty(ownerReference, 'ComboboxLoja', {
          get: function() { return _comboboxLoja; },
          set: function(newValue) { var oldValue = _comboboxLoja; _comboboxLoja = newValue; if (!entityPropChanged(ownerReference, 'ComboboxLoja', oldValue, newValue)) { _comboboxLoja = oldValue; } else { _comboboxLojaName = (dataDomains.getName('LX_LOJA', newValue)); } }
        });
        var _comboboxLojaName = ownerReference.ComboboxLojaName;
        Object.defineProperty(ownerReference, 'ComboboxLojaName', {
          get: function() { return _comboboxLojaName; },
          set: function(newValue) { var oldValue = _comboboxLojaName; _comboboxLojaName = newValue; if (!entityPropChanged(ownerReference, 'ComboboxLojaName', oldValue, newValue)) { _comboboxLojaName = oldValue; } else { _comboboxLoja = (dataDomains.getId('LX_LOJA', newValue)); } }
        });
        var _datetimeLoja = ownerReference.DatetimeLoja;
        Object.defineProperty(ownerReference, 'DatetimeLoja', {
          get: function() { return _datetimeLoja; },
          set: function(newValue) { var oldValue = _datetimeLoja; _datetimeLoja = newValue; if (!entityPropChanged(ownerReference, 'DatetimeLoja', oldValue, newValue)) { _datetimeLoja = oldValue; } }
        });
        var _decimalLoja = ownerReference.DecimalLoja;
        Object.defineProperty(ownerReference, 'DecimalLoja', {
          get: function() { return _decimalLoja; },
          set: function(newValue) { var oldValue = _decimalLoja; _decimalLoja = newValue; if (!entityPropChanged(ownerReference, 'DecimalLoja', oldValue, newValue)) { _decimalLoja = oldValue; } }
        });
        var _guidLoja = ownerReference.GuidLoja;
        Object.defineProperty(ownerReference, 'GuidLoja', {
          get: function() { return _guidLoja; },
          set: function(newValue) { var oldValue = _guidLoja; _guidLoja = newValue; if (!entityPropChanged(ownerReference, 'GuidLoja', oldValue, newValue)) { _guidLoja = oldValue; } }
        });
        var _idLoja = ownerReference.IdLoja;
        Object.defineProperty(ownerReference, 'IdLoja', {
          get: function() { return _idLoja; },
          set: function(newValue) { var oldValue = _idLoja; _idLoja = newValue; if (!entityPropChanged(ownerReference, 'IdLoja', oldValue, newValue)) { _idLoja = oldValue; } }
        });
        var _intLoja = ownerReference.IntLoja;
        Object.defineProperty(ownerReference, 'IntLoja', {
          get: function() { return _intLoja; },
          set: function(newValue) { var oldValue = _intLoja; _intLoja = newValue; if (!entityPropChanged(ownerReference, 'IntLoja', oldValue, newValue)) { _intLoja = oldValue; } }
        });
        var _smallIntLoja = ownerReference.SmallIntLoja;
        Object.defineProperty(ownerReference, 'SmallIntLoja', {
          get: function() { return _smallIntLoja; },
          set: function(newValue) { var oldValue = _smallIntLoja; _smallIntLoja = newValue; if (!entityPropChanged(ownerReference, 'SmallIntLoja', oldValue, newValue)) { _smallIntLoja = oldValue; } }
        });
        var _stringLoja = ownerReference.StringLoja;
        Object.defineProperty(ownerReference, 'StringLoja', {
          get: function() { return _stringLoja; },
          set: function(newValue) { var oldValue = _stringLoja; _stringLoja = newValue; if (!entityPropChanged(ownerReference, 'StringLoja', oldValue, newValue)) { _stringLoja = oldValue; } }
        });
        //End Property Definitions
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
       ownerReference.isAdded = (isPOCO === true ? function() { return ownerReference.ChangeState === 'I'; } : function() {
           return ownerReference.entityAspect.entityState === breeze.EntityState.Added;
       });
       ownerReference.isDeleted = (isPOCO === true ? function() { return ownerReference.ChangeState === 'D'; } : function() {
           return ownerReference.entityAspect.entityState === breeze.EntityState.Deleted;
       });
       ownerReference.isModified = (isPOCO === true ? function() { return ownerReference.ChangeState === 'U'; } : function() {
           return ownerReference.entityAspect.entityState === breeze.EntityState.Modified;
       });
       ownerReference.isDetached = (isPOCO === true ? function() { return false; } : function() {
           return ownerReference.entityAspect.entityState === breeze.EntityState.Detached;
       });
       ownerReference.isUnchanged = (isPOCO === true ? function() { return ownerReference.ChangeState === 'N'; } : function() {
           return ownerReference.entityAspect.entityState === breeze.EntityState.Unchanged;
       });
       ownerReference.setModified = (isPOCO === true ? function() { ownerReference.ChangeState = 'U'; } : function() {
           ownerReference.entityAspect.setModified();
       });
       ownerReference.setUnchanged = (isPOCO === true ? function() { ownerReference.ChangeState = 'N'; } : function() {
           ownerReference.entityAspect.setUnchanged();
       });
       ownerReference.serverDataType = [];
       ownerReference.serverDataType['BigIntLoja'] = 'L';
       ownerReference.serverDataType['BitLoja'] = 'B';
       ownerReference.serverDataType['ComboboxLoja'] = 'Y';
       ownerReference.serverDataType['DatetimeLoja'] = 'T';
       ownerReference.serverDataType['DecimalLoja'] = 'D';
       ownerReference.serverDataType['GuidLoja'] = 'G';
       ownerReference.serverDataType['IdLoja'] = 'I';
       ownerReference.serverDataType['IntLoja'] = 'I';
       ownerReference.serverDataType['SmallIntLoja'] = 'H';
       ownerReference.serverDataType['StringLoja'] = 'S';
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
           if (ownerReference.ChangeState == 'I') {
               if (parent && (typeof parent.LojaList === 'function')) { 
                   parent.LojaList.remove(ownerReference); 
               }
               else {
                   vm.dataView.remove(ownerReference);
               }
           }
           else {
               if (ownerReference.ChangeState == 'N') { ownerReference.createOriginal(); }
               ownerReference.ChangeState = 'D'; // mark for deletion
           }
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
       ownerReference.myProperties = [ 'BigIntLoja','BitLoja','ComboboxLoja','DatetimeLoja','DecimalLoja','GuidLoja','IdLoja','IntLoja','SmallIntLoja','StringLoja' ];
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
                   var value = (dataDomains.getId('LX_LOJA', newValue));
                   if (value != ownerReference.ComboboxLoja()) {
                       ownerReference.ComboboxLoja(value);
                   }
            });
    
           ownerReference.ComboboxLoja.subscribe(
           function (newValue) {
                   if (newValue == null) { ownerReference.ComboboxLoja(0); return; }
                   var value = dataDomains.getName('LX_LOJA', newValue);
                   if (value != ownerReference.ComboboxLojaName()) {
                       ownerReference.ComboboxLojaName(value);
               }
           });
       }
    //#endregion Extended Domain Names
    };
    metadataStore.registerEntityTypeCtor("Loja", null, LojaInitializer);
    
    // Configure Estado data type
    metadataStore.addEntityType({
    shortName: "Estado",
    namespace: "Linx.Demo.BV.ModalExterna",
    autoGeneratedKeyType: AutoGeneratedKeyType.Identity,
    dataProperties: {
    BigIntEstado: { dataType: DataType.Int64, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,BitEstado: { dataType: DataType.Boolean, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,ComboboxEstado: { dataType: DataType.Byte, isNullable: false, isPartOfKey: false, validators: [ Validator.hasValueValidator]  }
    ,ComboboxEstadoName: { dataType: DataType.String, isNullable: false, isPartOfKey: false, validators: [] }
    ,DatetimeEstado: { dataType: DataType.DateTime, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,DecimalEstado: { dataType: DataType.Decimal, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,GuidEstado: { dataType: DataType.Guid, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,IdEstado: { dataType: DataType.Int32, isNullable: false, isPartOfKey: true, validators: [ Validator.hasValueValidator]  }
    ,IdPais: { dataType: DataType.Int32, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,IntEstado: { dataType: DataType.Int32, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,SmallIntEstado: { dataType: DataType.Int16, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,StringEstado: { dataType: DataType.String, maxLength: 50, isNullable: true, isPartOfKey: false, validators: [ Validator.maxLength( {maxLength: 50})]  }
    ,StringPais: { dataType: DataType.String, maxLength: 50, isNullable: true, isPartOfKey: false, validators: [ Validator.maxLength( {maxLength: 50})]  }
    ,ChangeState: { dataType: DataType.String, isNullable: true, isPartOfKey: false, validators: [] }
                    },
    navigationProperties: {
    // Returns collections of details and associates with Parent
                          }
    });
    lookUpProperties['Estado'] = {IdPais: 'LookUpPais', StringPais: 'LookUpPais'};
    var EstadoInitializer = function (ownerReference, isPOCO) {
       ownerReference.RowDataId = (isPOCO === true ? getNextSequence('Estado') : ko.observable(getNextSequence('Estado')));
        //Start Property Definitions
        var _bigIntEstado = ownerReference.BigIntEstado;
        Object.defineProperty(ownerReference, 'BigIntEstado', {
          get: function() { return _bigIntEstado; },
          set: function(newValue) { var oldValue = _bigIntEstado; _bigIntEstado = newValue; if (!entityPropChanged(ownerReference, 'BigIntEstado', oldValue, newValue)) { _bigIntEstado = oldValue; } }
        });
        var _bitEstado = ownerReference.BitEstado;
        Object.defineProperty(ownerReference, 'BitEstado', {
          get: function() { return _bitEstado; },
          set: function(newValue) { var oldValue = _bitEstado; _bitEstado = newValue; if (!entityPropChanged(ownerReference, 'BitEstado', oldValue, newValue)) { _bitEstado = oldValue; } }
        });
        var _comboboxEstado = ownerReference.ComboboxEstado;
        Object.defineProperty(ownerReference, 'ComboboxEstado', {
          get: function() { return _comboboxEstado; },
          set: function(newValue) { var oldValue = _comboboxEstado; _comboboxEstado = newValue; if (!entityPropChanged(ownerReference, 'ComboboxEstado', oldValue, newValue)) { _comboboxEstado = oldValue; } else { _comboboxEstadoName = (dataDomains.getName('LX_ESTADO', newValue)); } }
        });
        var _comboboxEstadoName = ownerReference.ComboboxEstadoName;
        Object.defineProperty(ownerReference, 'ComboboxEstadoName', {
          get: function() { return _comboboxEstadoName; },
          set: function(newValue) { var oldValue = _comboboxEstadoName; _comboboxEstadoName = newValue; if (!entityPropChanged(ownerReference, 'ComboboxEstadoName', oldValue, newValue)) { _comboboxEstadoName = oldValue; } else { _comboboxEstado = (dataDomains.getId('LX_ESTADO', newValue)); } }
        });
        var _datetimeEstado = ownerReference.DatetimeEstado;
        Object.defineProperty(ownerReference, 'DatetimeEstado', {
          get: function() { return _datetimeEstado; },
          set: function(newValue) { var oldValue = _datetimeEstado; _datetimeEstado = newValue; if (!entityPropChanged(ownerReference, 'DatetimeEstado', oldValue, newValue)) { _datetimeEstado = oldValue; } }
        });
        var _decimalEstado = ownerReference.DecimalEstado;
        Object.defineProperty(ownerReference, 'DecimalEstado', {
          get: function() { return _decimalEstado; },
          set: function(newValue) { var oldValue = _decimalEstado; _decimalEstado = newValue; if (!entityPropChanged(ownerReference, 'DecimalEstado', oldValue, newValue)) { _decimalEstado = oldValue; } }
        });
        var _guidEstado = ownerReference.GuidEstado;
        Object.defineProperty(ownerReference, 'GuidEstado', {
          get: function() { return _guidEstado; },
          set: function(newValue) { var oldValue = _guidEstado; _guidEstado = newValue; if (!entityPropChanged(ownerReference, 'GuidEstado', oldValue, newValue)) { _guidEstado = oldValue; } }
        });
        var _idEstado = ownerReference.IdEstado;
        Object.defineProperty(ownerReference, 'IdEstado', {
          get: function() { return _idEstado; },
          set: function(newValue) { var oldValue = _idEstado; _idEstado = newValue; if (!entityPropChanged(ownerReference, 'IdEstado', oldValue, newValue)) { _idEstado = oldValue; } }
        });
        var _idPais = ownerReference.IdPais;
        Object.defineProperty(ownerReference, 'IdPais', {
          get: function() { return _idPais; },
          set: function(newValue) { var oldValue = _idPais; _idPais = newValue; if (!entityPropChanged(ownerReference, 'IdPais', oldValue, newValue)) { _idPais = oldValue; } }
        });
        var _intEstado = ownerReference.IntEstado;
        Object.defineProperty(ownerReference, 'IntEstado', {
          get: function() { return _intEstado; },
          set: function(newValue) { var oldValue = _intEstado; _intEstado = newValue; if (!entityPropChanged(ownerReference, 'IntEstado', oldValue, newValue)) { _intEstado = oldValue; } }
        });
        var _smallIntEstado = ownerReference.SmallIntEstado;
        Object.defineProperty(ownerReference, 'SmallIntEstado', {
          get: function() { return _smallIntEstado; },
          set: function(newValue) { var oldValue = _smallIntEstado; _smallIntEstado = newValue; if (!entityPropChanged(ownerReference, 'SmallIntEstado', oldValue, newValue)) { _smallIntEstado = oldValue; } }
        });
        var _stringEstado = ownerReference.StringEstado;
        Object.defineProperty(ownerReference, 'StringEstado', {
          get: function() { return _stringEstado; },
          set: function(newValue) { var oldValue = _stringEstado; _stringEstado = newValue; if (!entityPropChanged(ownerReference, 'StringEstado', oldValue, newValue)) { _stringEstado = oldValue; } }
        });
        var _stringPais = ownerReference.StringPais;
        Object.defineProperty(ownerReference, 'StringPais', {
          get: function() { return _stringPais; },
          set: function(newValue) { var oldValue = _stringPais; _stringPais = newValue; if (!entityPropChanged(ownerReference, 'StringPais', oldValue, newValue)) { _stringPais = oldValue; } }
        });
        //End Property Definitions
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
       ownerReference.isAdded = (isPOCO === true ? function() { return ownerReference.ChangeState === 'I'; } : function() {
           return ownerReference.entityAspect.entityState === breeze.EntityState.Added;
       });
       ownerReference.isDeleted = (isPOCO === true ? function() { return ownerReference.ChangeState === 'D'; } : function() {
           return ownerReference.entityAspect.entityState === breeze.EntityState.Deleted;
       });
       ownerReference.isModified = (isPOCO === true ? function() { return ownerReference.ChangeState === 'U'; } : function() {
           return ownerReference.entityAspect.entityState === breeze.EntityState.Modified;
       });
       ownerReference.isDetached = (isPOCO === true ? function() { return false; } : function() {
           return ownerReference.entityAspect.entityState === breeze.EntityState.Detached;
       });
       ownerReference.isUnchanged = (isPOCO === true ? function() { return ownerReference.ChangeState === 'N'; } : function() {
           return ownerReference.entityAspect.entityState === breeze.EntityState.Unchanged;
       });
       ownerReference.setModified = (isPOCO === true ? function() { ownerReference.ChangeState = 'U'; } : function() {
           ownerReference.entityAspect.setModified();
       });
       ownerReference.setUnchanged = (isPOCO === true ? function() { ownerReference.ChangeState = 'N'; } : function() {
           ownerReference.entityAspect.setUnchanged();
       });
       ownerReference.serverDataType = [];
       ownerReference.serverDataType['BigIntEstado'] = 'L';
       ownerReference.serverDataType['BitEstado'] = 'B';
       ownerReference.serverDataType['ComboboxEstado'] = 'Y';
       ownerReference.serverDataType['DatetimeEstado'] = 'T';
       ownerReference.serverDataType['DecimalEstado'] = 'D';
       ownerReference.serverDataType['GuidEstado'] = 'G';
       ownerReference.serverDataType['IdEstado'] = 'I';
       ownerReference.serverDataType['IdPais'] = 'I';
       ownerReference.serverDataType['IntEstado'] = 'I';
       ownerReference.serverDataType['SmallIntEstado'] = 'H';
       ownerReference.serverDataType['StringEstado'] = 'S';
       ownerReference.serverDataType['StringPais'] = 'S';
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
       //#region Lookup Extended Methods
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
           if (lookupName === 'LookUpPais') {
               displayName = ' de Pais';
           }
           return 'Seleção' + displayName;
       };
    
       ownerReference.getSpecializedLookup = function (lookupName, lookupInfo, fieldToSearch, valueToSearch, ownerReference, allowMultiSelectionInSearch) {
           var specializedLookup = '';
           return specializedLookup;
       };
    
       ownerReference.getSubQueryFilterFromLookUpPais = function (propertyName) {
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
           if (ownerReference.ChangeState == 'I') {
               if (parent && (typeof parent.EstadoList === 'function')) { 
                   parent.EstadoList.remove(ownerReference); 
               }
               else {
                   vm.dataView.remove(ownerReference);
               }
           }
           else {
               if (ownerReference.ChangeState == 'N') { ownerReference.createOriginal(); }
               ownerReference.ChangeState = 'D'; // mark for deletion
           }
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
       ownerReference.myProperties = [ 'BigIntEstado','BitEstado','ComboboxEstado','DatetimeEstado','DecimalEstado','GuidEstado','IdEstado','IdPais','IntEstado','SmallIntEstado','StringEstado','StringPais' ];
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
                   var value = (dataDomains.getId('LX_ESTADO', newValue));
                   if (value != ownerReference.ComboboxEstado()) {
                       ownerReference.ComboboxEstado(value);
                   }
            });
    
           ownerReference.ComboboxEstado.subscribe(
           function (newValue) {
                   if (newValue == null) { ownerReference.ComboboxEstado(0); return; }
                   var value = dataDomains.getName('LX_ESTADO', newValue);
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
    
    var getLookUpVendaByEntitySearch = function (jEntitySearch, order, skip, take, direction, lookupField) {
        var query = EntityQuery.from('GetLookUpVendaByEntitySearch').noTracking(true);
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
    
    var getLookUpPaisByEntitySearch = function (jEntitySearch, order, skip, take, direction, lookupField) {
        var query = EntityQuery.from('GetLookUpPaisByEntitySearch').noTracking(true);
        query = (direction === 'descending' ? query.orderByDesc(order) : query.orderBy(order));
    
        if ((typeof jEntitySearch !== 'undefined') && jEntitySearch !== null)
            query = query.withParameters({ propertyName: (isNullOrEmpty(lookupField) ? order : lookupField), jEntitySearch: jEntitySearch });
    
        if (take > 0)
           query = query.skip(skip).take(take);
        query = query.inlineCount(true);
    
        return manager.executeQuery(query)
        .fail(queryFailed);
    };
    var lookUpExternalManagers = [];
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
    
    var clearVenda = function (idBandeiraRede, complete) {
        clearAll();
        resetSequence('Venda');
        var refVenda = manager.createEntity('Venda', {}, breeze.EntityState.Unchanged);
        resetSequence('VendaItem');
        var refVendaItem = manager.createEntity('VendaItem', {}, breeze.EntityState.Unchanged);
        refVenda.currentVendaItem(refVendaItem);
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
    
    var clearVendaItem = function (idBandeiraRede, complete) {
        clearAll();
        resetSequence('VendaItem');
        var refVendaItem = manager.createEntity('VendaItem', {}, breeze.EntityState.Unchanged);
        if (complete) complete({ results: [ refVendaItem ] });
        return true;
    };
    
    var getVendaItem = function (predicate, preserveCurrentState, noTracking) {
        if (!preserveCurrentState) clearAll();
        var query = EntityQuery.from('GetVendaItem').noTracking(noTracking)
        .orderBy('IdVendaItem asc')
        ;
    
        if ((typeof predicate !== 'undefined') && predicate !== null)
            query = query.where(predicate);
    
        return manager.executeQuery(query)
        .fail(queryFailed);
    };
    
    var getVendaItemByEntitySearchNoAssociations = function (jEntitySearch, skip, take, returnInlineCount, preserveCurrentState, noTracking, orderByDef) {
        if (!preserveCurrentState) clearAll();
        var query = EntityQuery.from('GetVendaItemByEntitySearchNoAssociations').noTracking(noTracking)
        .orderBy((isNullOrEmpty(orderByDef) ? 'IdVendaItem asc' : orderByDef))
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
    
    var clearFormaPagamento = function (idBandeiraRede, complete) {
        clearAll();
        resetSequence('FormaPagamento');
        var refFormaPagamento = manager.createEntity('FormaPagamento', {}, breeze.EntityState.Unchanged);
        if (complete) complete({ results: [ refFormaPagamento ] });
        return true;
    };
    
    var getFormaPagamento = function (predicate, preserveCurrentState, noTracking) {
        if (!preserveCurrentState) clearAll();
        var query = EntityQuery.from('GetFormaPagamento').noTracking(noTracking)
        .orderBy('IdFormaPagamento asc')
        ;
    
        if ((typeof predicate !== 'undefined') && predicate !== null)
            query = query.where(predicate);
    
        return manager.executeQuery(query)
        .fail(queryFailed);
    };
    
    var getFormaPagamentoByEntitySearchNoAssociations = function (jEntitySearch, skip, take, returnInlineCount, preserveCurrentState, noTracking, orderByDef) {
        if (!preserveCurrentState) clearAll();
        var query = EntityQuery.from('GetFormaPagamentoByEntitySearchNoAssociations').noTracking(noTracking)
        .orderBy((isNullOrEmpty(orderByDef) ? 'IdFormaPagamento asc' : orderByDef))
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
            if (propertyName === '' || propertyName === 'IdPais') {
               if (selectedElement.hasOwnProperty('IdPais') && replaceTo.hasOwnProperty('IdPais'))
               {
                   setAbsoluteValue(replaceTo, 'IdPais', getAbsoluteValue(selectedElement['IdPais']));
               }
               else if (replaceTo.hasOwnProperty('IdPais')) {
                   setAbsoluteValue(replaceTo, 'IdPais', null);
               }
            }
            if (propertyName === '' || propertyName === 'StringPais') {
               if (selectedElement.hasOwnProperty('StringPais') && replaceTo.hasOwnProperty('StringPais'))
               {
                   setAbsoluteValue(replaceTo, 'StringPais', getAbsoluteValue(selectedElement['StringPais']));
               }
               else if (replaceTo.hasOwnProperty('StringPais')) {
                   setAbsoluteValue(replaceTo, 'StringPais', null);
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
        setAbsoluteValue(replaceTo, 'IdPais', null);
        setAbsoluteValue(replaceTo, 'StringPais', null);
        setAbsoluteValue(replaceTo, 'StringEstado', null);
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
                   setAbsoluteValue(replaceTo, 'IdCliente', null);
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
        setAbsoluteValue(replaceTo, 'IdCliente', null);
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
            if (propertyName === '' || propertyName === 'StringLoja') {
               if (selectedElement.hasOwnProperty('StringLoja') && replaceTo.hasOwnProperty('StringLoja'))
               {
                   setAbsoluteValue(replaceTo, 'StringLoja', getAbsoluteValue(selectedElement['StringLoja']));
               }
               else if (replaceTo.hasOwnProperty('StringLoja')) {
                   setAbsoluteValue(replaceTo, 'StringLoja', null);
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
        setAbsoluteValue(replaceTo, 'StringLoja', null);
        isClearingLookup(false);
        //Trigger context data update event
        if (replaceTo.isPOCO) vm.refreshCurrentBind();
        setTimeout(function () {document.dispatchEvent(dataUpdateEvent);}, 100);
    }
     var finalizeAllLookUpVenda = function (replaceTo, selectedElements, propertyName, lookupInfo) {
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
            if (propertyName === '' || propertyName === 'IdVenda') {
               if (selectedElement.hasOwnProperty('IdVenda') && replaceTo.hasOwnProperty('IdVenda'))
               {
                   setAbsoluteValue(replaceTo, 'IdVenda', getAbsoluteValue(selectedElement['IdVenda']));
               }
               else if (replaceTo.hasOwnProperty('IdVenda')) {
                   setAbsoluteValue(replaceTo, 'IdVenda', null);
               }
            }
            if (replaceTo.validatedlookupsArray && !replaceTo.validatedlookupsArray.contains('LookUpVenda'))
                replaceTo.validatedlookupsArray.push('LookUpVenda');
        }
        //Trigger context data update event
        if (replaceTo.isPOCO) vm.refreshCurrentBind();
        document.dispatchEvent(dataUpdateEvent);
        isFinalizingLookup(false);
    };
    
    function clearLookUpVenda(replaceTo) {
        if (!replaceTo)
            return;
        isClearingLookup(true);
        setAbsoluteValue(replaceTo, 'IdVenda', null);
        isClearingLookup(false);
        //Trigger context data update event
        if (replaceTo.isPOCO) vm.refreshCurrentBind();
        setTimeout(function () {document.dispatchEvent(dataUpdateEvent);}, 100);
    }
     var finalizeAllLookUpPais = function (replaceTo, selectedElements, propertyName, lookupInfo) {
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
            if (propertyName === '' || propertyName === 'IdPais') {
               if (selectedElement.hasOwnProperty('IdPais') && replaceTo.hasOwnProperty('IdPais'))
               {
                   setAbsoluteValue(replaceTo, 'IdPais', getAbsoluteValue(selectedElement['IdPais']));
               }
               else if (replaceTo.hasOwnProperty('IdPais')) {
                   setAbsoluteValue(replaceTo, 'IdPais', null);
               }
            }
            if (propertyName === '' || propertyName === 'StringPais') {
               if (selectedElement.hasOwnProperty('StringPais') && replaceTo.hasOwnProperty('StringPais'))
               {
                   setAbsoluteValue(replaceTo, 'StringPais', getAbsoluteValue(selectedElement['StringPais']));
               }
               else if (replaceTo.hasOwnProperty('StringPais')) {
                   setAbsoluteValue(replaceTo, 'StringPais', null);
               }
            }
            if (replaceTo.validatedlookupsArray && !replaceTo.validatedlookupsArray.contains('LookUpPais'))
                replaceTo.validatedlookupsArray.push('LookUpPais');
        }
        //Trigger context data update event
        if (replaceTo.isPOCO) vm.refreshCurrentBind();
        document.dispatchEvent(dataUpdateEvent);
        isFinalizingLookup(false);
    };
    
    function clearLookUpPais(replaceTo) {
        if (!replaceTo)
            return;
        isClearingLookup(true);
        setAbsoluteValue(replaceTo, 'IdPais', null);
        setAbsoluteValue(replaceTo, 'StringPais', null);
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
    
    var saveChangesFake = function (transactionID, saveSucceeded) {
        var dataEntities = _.map(vm.getDataForSaving(), function (entity) { return entity.getPrimitiveDTO(entity.ChangeState != 'D'); });
        var dataForSaving = {
            TransactionID: transactionID,
            ComponentName: vm.__moduleId__,
            DataList: dataEntities,
            RelationInfo: vm.getViewMapInfo()
        };
        return $.ajax({
            type: 'POST',
            crossDomain: true,
            url: getServiceAddress('LinxDemoModalExterna/Save' + vm.rootDataTypeName + 'InCache'),
            globalError: false,
            contentType: 'application/json',
            async: true,
            cache: false,
            data: JSON.stringify(dataForSaving),
            success: function (response) {
                if (saveSucceeded)
                    saveSucceeded(response);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                failed({ message: jqXHR.responseJSON.ExceptionMessage });
            }
        });
        function failed(error) {
            var msg = error.message.replace('Fail by saving data:', '');
            showModalAlert('Falha ao salvar informações.', [msg]);
            error.message = msg;
            throw error;
        }
    };
    
    var submitAllChanges = function (transactionId, saveSucceeded, failed, completed) {
        return $.ajax({
            type: 'GET',
            crossDomain: true,
            url: getServiceAddress('LinxDemoModalExterna/submitAllChanges?transactionID=' + transactionId),
            globalError: false,
            contentType: 'application/json',
            async: true,
            cache: false,
            success: function (response) { if (typeof saveSucceeded === 'function') saveSucceeded(response); },
            error: function (jqXHR, textStatus, errorThrown) { failed(jqXHR.responseJSON);}
        });
    }
    var cancelAllChanges = function (transactionId, saveSucceeded, failed) {
        return $.ajax({
            type: 'GET',
            crossDomain: true,
            url: getServiceAddress('LinxDemoModalExterna/CancelAllChanges?transactionID=' + transactionId),
            globalError: false,
            contentType: 'application/json',
            async: true,
            cache: false,
            success: function (response) { if (saveSucceeded) saveSucceeded(response); },
            error: function (jqXHR, textStatus, errorThrown) { failed(jqXHR.responseJSON); }
        });
    }
    
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
    
    var createCliente = function() {
        //Create entity instance
        enableChangeTrack = false;
        var defaultVals = { IdCliente: (-1 * getSequence('Cliente')), BitCliente: false };
        var entityType = manager.metadataStore.getEntityType('Cliente');
        var entity = {};
        for (var idx = 0; idx < entityType.dataProperties.length; idx++) { 
            var prop = entityType.dataProperties[idx]; 
            if ((typeof defaultVals[prop.name]) !== 'undefined') entity[prop.name] = defaultVals[prop.name];
            else  entity[prop.name] = prop.defaultValue;
        }
        dataContext.initializePOCO(entity, 'Cliente');
        entity.setDefaults();
        setAbsoluteValue(entity, 'ChangeState', 'I');
        if (typeof entity.OnAdding == 'function') {
            if (!entity.OnAdding()) { dataContext.deleteEntity(entity); return; }
        }
        enableChangeTrack = true;
        return entity;
    };
    
    var createVenda = function() {
        //Create entity instance
        enableChangeTrack = false;
        var defaultVals = { IdVenda: (-1 * getSequence('Venda')), BitVenda: false };
        var entityType = manager.metadataStore.getEntityType('Venda');
        var entity = {};
        for (var idx = 0; idx < entityType.dataProperties.length; idx++) { 
            var prop = entityType.dataProperties[idx]; 
            if ((typeof defaultVals[prop.name]) !== 'undefined') entity[prop.name] = defaultVals[prop.name];
            else  entity[prop.name] = prop.defaultValue;
        }
        dataContext.initializePOCO(entity, 'Venda');
        entity.setDefaults();
        setAbsoluteValue(entity, 'ChangeState', 'I');
        if (typeof entity.OnAdding == 'function') {
            if (!entity.OnAdding()) { dataContext.deleteEntity(entity); return; }
        }
        enableChangeTrack = true;
        return entity;
    };
    
    var createVendaItem = function(parent, noCurrent) {
        //Create entity instance
        enableChangeTrack = false;
        var defaultVals = { Venda: parent, IdVendaItem: (-1 * getSequence('VendaItem')), BitVendaItem: false };
        var entityType = manager.metadataStore.getEntityType('VendaItem');
        var entity = {};
        for (var idx = 0; idx < entityType.dataProperties.length; idx++) { 
            var prop = entityType.dataProperties[idx]; 
            if ((typeof defaultVals[prop.name]) !== 'undefined') entity[prop.name] = defaultVals[prop.name];
            else  entity[prop.name] = prop.defaultValue;
        }
        dataContext.initializePOCO(entity, 'VendaItem');
        setAbsoluteValue(entity, 'Venda', parent);
        setAbsoluteValue(entity, 'IdVenda', getAbsoluteValue(parent.IdVenda));
        entity.setDefaults();
        setAbsoluteValue(entity, 'ChangeState', 'I');
        if (typeof entity.OnAdding == 'function') {
            if (!entity.OnAdding()) { dataContext.deleteEntity(entity); return; }
        }
        if (noCurrent !== true) parent.currentVendaItem(entity);
        if (parent && (typeof parent.VendaItemList === 'function')) parent.VendaItemList().push(entity);
        if (parent && (typeof parent.setCurrentDetails === 'function') && (typeof parent.VendaItemList === 'function') && parent.VendaItemList().length == 0) parent.setCurrentDetails('VendaItem');
        if (entity.setParentAsModified) entity.setParentAsModified();
        enableChangeTrack = true;
        return entity;
    };
    
    var createFormaPagamento = function() {
        //Create entity instance
        enableChangeTrack = false;
        var defaultVals = { IdFormaPagamento: (-1 * getSequence('FormaPagamento')), BitFormaPagamento: false };
        var entityType = manager.metadataStore.getEntityType('FormaPagamento');
        var entity = {};
        for (var idx = 0; idx < entityType.dataProperties.length; idx++) { 
            var prop = entityType.dataProperties[idx]; 
            if ((typeof defaultVals[prop.name]) !== 'undefined') entity[prop.name] = defaultVals[prop.name];
            else  entity[prop.name] = prop.defaultValue;
        }
        dataContext.initializePOCO(entity, 'FormaPagamento');
        entity.setDefaults();
        setAbsoluteValue(entity, 'ChangeState', 'I');
        if (typeof entity.OnAdding == 'function') {
            if (!entity.OnAdding()) { dataContext.deleteEntity(entity); return; }
        }
        enableChangeTrack = true;
        return entity;
    };
    
    var createLoja = function() {
        //Create entity instance
        enableChangeTrack = false;
        var defaultVals = { IdLoja: (-1 * getSequence('Loja')), BitLoja: false };
        var entityType = manager.metadataStore.getEntityType('Loja');
        var entity = {};
        for (var idx = 0; idx < entityType.dataProperties.length; idx++) { 
            var prop = entityType.dataProperties[idx]; 
            if ((typeof defaultVals[prop.name]) !== 'undefined') entity[prop.name] = defaultVals[prop.name];
            else  entity[prop.name] = prop.defaultValue;
        }
        dataContext.initializePOCO(entity, 'Loja');
        entity.setDefaults();
        setAbsoluteValue(entity, 'ChangeState', 'I');
        if (typeof entity.OnAdding == 'function') {
            if (!entity.OnAdding()) { dataContext.deleteEntity(entity); return; }
        }
        enableChangeTrack = true;
        return entity;
    };
    
    var createEstado = function() {
        //Create entity instance
        enableChangeTrack = false;
        var defaultVals = { IdEstado: (-1 * getSequence('Estado')), BitEstado: false };
        var entityType = manager.metadataStore.getEntityType('Estado');
        var entity = {};
        for (var idx = 0; idx < entityType.dataProperties.length; idx++) { 
            var prop = entityType.dataProperties[idx]; 
            if ((typeof defaultVals[prop.name]) !== 'undefined') entity[prop.name] = defaultVals[prop.name];
            else  entity[prop.name] = prop.defaultValue;
        }
        dataContext.initializePOCO(entity, 'Estado');
        entity.setDefaults();
        setAbsoluteValue(entity, 'ChangeState', 'I');
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
                    saveURL(managerAuth.serviceBus + response.substr(1), entityName + '.xlsx');
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
            saveChangesFake: saveChangesFake,
            submitAllChanges: submitAllChanges,
            cancelAllChanges: cancelAllChanges,
            getChanges: getChanges,
            hasValidationErrors: hasValidationErrors,
            getEntityProperty: getEntityProperty,
            getViewInfo: getViewInfo,
            createEntity: createEntity,
            notifyPresentation: notifyPresentation,
            createFreeEntity: createFreeEntity,
            createCliente: createCliente,
            createVenda: createVenda,
            createVendaItem: createVendaItem,
            createFormaPagamento: createFormaPagamento,
            createLoja: createLoja,
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
                getLookUpClienteByEntitySearch: getLookUpClienteByEntitySearch,
            getLookUpVendaByEntitySearch: getLookUpVendaByEntitySearch,
            getLookUpEstadoByEntitySearch: getLookUpEstadoByEntitySearch,
            getLookUpLojaByEntitySearch: getLookUpLojaByEntitySearch,
            getLookUpPaisByEntitySearch: getLookUpPaisByEntitySearch,
            getBmEntityProperties: getBmEntityProperties,
            clearCliente: clearCliente,
            getCliente: getCliente,
            getClienteByEntitySearchNoAssociations: getClienteByEntitySearchNoAssociations,
            clearVenda: clearVenda,
            getVenda: getVenda,
            getVendaByEntitySearchNoAssociations: getVendaByEntitySearchNoAssociations,
            clearVendaItem: clearVendaItem,
            getVendaItem: getVendaItem,
            getVendaItemByEntitySearchNoAssociations: getVendaItemByEntitySearchNoAssociations,
            clearFormaPagamento: clearFormaPagamento,
            getFormaPagamento: getFormaPagamento,
            getFormaPagamentoByEntitySearchNoAssociations: getFormaPagamentoByEntitySearchNoAssociations,
            clearLoja: clearLoja,
            getLoja: getLoja,
            getLojaByEntitySearchNoAssociations: getLojaByEntitySearchNoAssociations,
            clearEstado: clearEstado,
            getEstado: getEstado,
            getEstadoByEntitySearchNoAssociations: getEstadoByEntitySearchNoAssociations,
                finalizeAllLookUpEstado: finalizeAllLookUpEstado,
            clearLookUpEstado: clearLookUpEstado,
            finalizeAllLookUpCliente: finalizeAllLookUpCliente,
            clearLookUpCliente: clearLookUpCliente,
            finalizeAllLookUpLoja: finalizeAllLookUpLoja,
            clearLookUpLoja: clearLookUpLoja,
            finalizeAllLookUpVenda: finalizeAllLookUpVenda,
            clearLookUpVenda: clearLookUpVenda,
            finalizeAllLookUpPais: finalizeAllLookUpPais,
            clearLookUpPais: clearLookUpPais
        };
    loadParameters();
    return dataContext;
    //#endregion Context Definition
}
return result;
});
