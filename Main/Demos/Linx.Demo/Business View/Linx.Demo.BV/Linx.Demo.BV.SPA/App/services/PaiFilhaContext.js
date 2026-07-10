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
       return getServiceAddress('LinxDemoPaiFilhaOData');
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
    var controllerName = 'LinxDemoPaiFilha';
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
        { key: 'BitLoja', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 1, isPartOfKey: false, headerText: 'Bit Loja', width: '140px', dataType: 'bool', format: 'checkbox', hidden: false, unbound: false, group: null },
        { key: 'ComboboxLoja', isQbeZero: false, isDomain: true, domainName: 'LX_COMBOBOX_LOJA', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 3, isPartOfKey: false, headerText: 'Combobox Loja', width: '205px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'ComboboxLojaName', isDomain: true, domainName: 'LX_COMBOBOX_LOJA', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Combobox Loja (Name)', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null },
        { key: 'DatetimeLoja', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Datetime Loja', width: '205px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null },
        { key: 'DecimalLoja', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 13, isPartOfKey: false, headerText: 'Decimal Loja', width: '192px', dataType: 'number', format: '0.00', hidden: false, unbound: false, group: null },
        { key: 'GuidLoja', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 36, isPartOfKey: false, headerText: 'Guid Loja', width: '250px', dataType: 'string', format: '', hidden: false, unbound: false, group: null },
        { key: 'IdCidade', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: 'IdCidade', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Id Cidade', width: '271px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IdEstado', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: 'IdEstado', lookupVisibleColumns: 'IdEstado,IdPais,StringPais,StringEstado', maxLength: 10, isPartOfKey: false, headerText: 'Id Estado', width: '271px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IdLoja', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 12, isPartOfKey: true, headerText: 'Id Loja', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IdPais', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: 'IdPais', lookupVisibleColumns: 'IdPais,StringPais', maxLength: 10, isPartOfKey: false, headerText: 'Id Pais', width: '271px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IntLoja', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Int Loja', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'NomeCidade', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: 'NomeCidade', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'Nome Cidade', width: '421px', dataType: 'string', format: '', hidden: false, unbound: false, group: null },
        { key: 'SmallIntLoja', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 6, isPartOfKey: false, headerText: 'Small Int Loja', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'StringEstado', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: 'StringEstado', lookupVisibleColumns: 'IdEstado,IdPais,StringPais,StringEstado', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'Nome Estado', width: '421px', dataType: 'string', format: '', hidden: false, unbound: false, group: null },
        { key: 'StringLoja', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'String Loja', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null },
        { key: 'StringPais', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: 'StringPais', lookupVisibleColumns: 'IdPais,StringPais', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'Nome Pais', width: '421px', dataType: 'string', format: '', hidden: false, unbound: false, group: null }
    ];
    dataExportInfo['Loja'] = [ 
        { name: 'Loja', canExportMedia: true , canExportReport: true, actionExport: 'LinxDemoPaiFilha/GetLojaToExcel', actionReport: 'LinxDemoPaiFilha/GetLojaToReportXml', actionFeed: 'LinxDemoPaiFilhaOData/Loja', actionName: 'LinxDemoPaiFilha/GetLojaByEntitySearchNoAssociations', display: 'Loja',  metaData: function() { return metadataInfo['Loja']; } }
        , { name: 'Venda', canExportMedia: true , canExportReport: true, actionExport: 'LinxDemoPaiFilha/GetVendaParentCompositionToExcel', actionReport: 'LinxDemoPaiFilha/GetVendaParentCompositionToReportXml', actionFeed: 'LinxDemoPaiFilhaOData/VendaParentComposition', actionName: 'LinxDemoPaiFilha/GetVendaParentCompositionByEntitySearchNoAssociations', display: 'Venda',  metaData: function() { return metadataInfo['VendaParentComposition']; } }
        , { name: 'VendaItem', canExportMedia: true , canExportReport: true, actionExport: 'LinxDemoPaiFilha/GetVendaItemParentCompositionToExcel', actionReport: 'LinxDemoPaiFilha/GetVendaItemParentCompositionToReportXml', actionFeed: 'LinxDemoPaiFilhaOData/VendaItemParentComposition', actionName: 'LinxDemoPaiFilha/GetVendaItemParentCompositionByEntitySearchNoAssociations', display: 'VendaItem',  metaData: function() { return metadataInfo['VendaItemParentComposition']; } }
    ];
    entitylookUps.push('Loja');
    entitylookUps['Loja'] = [];
    entitylookUps['Loja'].push('LookUpCidade');
    lookUpNames.push('LookUpCidade');
    metadataInfo['LookUpCidade'] = [
        { key: 'IdEstado', relatedKey: 'IdEstado', maxLength: 10, isPartOfKey: false, headerText: 'Cod UF', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IdPais', relatedKey: 'IdPais', maxLength: 10, isPartOfKey: false, headerText: 'Cod Pais', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'StringPais', relatedKey: 'StringPais', maxLength: 50, isPartOfKey: false, headerText: 'PAIS', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null },
        { key: 'StringEstado', relatedKey: 'StringEstado', maxLength: 50, isPartOfKey: false, headerText: 'UF', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null },
        { key: 'IdCidade', relatedKey: 'IdCidade', maxLength: 10, isPartOfKey: true, headerText: 'Cod Cidade', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'NomeCidade', relatedKey: 'NomeCidade', maxLength: 50, isPartOfKey: false, headerText: 'Cidade', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null }
    ];
    entityNames.push('Venda');
    metadataInfo['Venda'] = [
        { key: 'BitVenda', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Bit Venda', width: '153px', dataType: 'bool', format: 'checkbox', hidden: false, unbound: false, group: null },
        { key: 'ComboboxVenda', isQbeZero: false, isDomain: true, domainName: 'LX_VENDA', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 3, isPartOfKey: false, headerText: 'Combobox Venda', width: '218px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'ComboboxVendaName', isDomain: true, domainName: 'LX_VENDA', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Combobox Venda (Name)', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null },
        { key: 'DatetimeVenda', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Datetime Venda', width: '218px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null },
        { key: 'DecimalVenda', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 13, isPartOfKey: false, headerText: 'Decimal Venda', width: '205px', dataType: 'number', format: '0.00', hidden: false, unbound: false, group: null },
        { key: 'IdLoja', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Id Loja', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IdVenda', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 10, isPartOfKey: true, headerText: 'Id Venda', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IdVendedor', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: 'IdVendedor', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Id Vendedor', width: '271px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IntVenda', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Int Venda', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'SmallIntVenda', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 5, isPartOfKey: false, headerText: 'Small Int Venda', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'StringVenda', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'String Venda', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null },
        { key: 'StringVendedor', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: 'StringVendedor', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'String Vendedor', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null }
    ];
    entityNames.push('VendaParentComposition');
    metadataInfo['VendaParentComposition'] = [
        { key: 'BitVenda', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Bit Venda', width: '153px', dataType: 'bool', format: 'checkbox', hidden: false, unbound: false, group: null },
        { key: 'ComboboxVenda', isQbeZero: false, isDomain: true, domainName: 'LX_VENDA', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 3, isPartOfKey: false, headerText: 'Combobox Venda', width: '218px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'ComboboxVendaName', isDomain: true, domainName: 'LX_VENDA', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Combobox Venda (Name)', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null },
        { key: 'DatetimeVenda', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Datetime Venda', width: '218px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null },
        { key: 'DecimalVenda', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 13, isPartOfKey: false, headerText: 'Decimal Venda', width: '205px', dataType: 'number', format: '0.00', hidden: false, unbound: false, group: null },
        { key: 'IdLoja', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Id Loja', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IdVenda', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 10, isPartOfKey: true, headerText: 'Id Venda', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IdVendedor', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: 'IdVendedor', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Id Vendedor', width: '271px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IntVenda', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Int Venda', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'SmallIntVenda', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 5, isPartOfKey: false, headerText: 'Small Int Venda', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'StringVenda', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'String Venda', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null },
        { key: 'StringVendedor', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: 'StringVendedor', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'String Vendedor', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null },
        { key: 'BigIntLoja', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 24, isPartOfKey: false, headerText: 'Big Int Loja', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'BitLoja', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 1, isPartOfKey: false, headerText: 'Bit Loja', width: '140px', dataType: 'bool', format: 'checkbox', hidden: false, unbound: false, group: null },
        { key: 'ComboboxLoja', isQbeZero: false, isDomain: true, domainName: 'LX_COMBOBOX_LOJA', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 3, isPartOfKey: false, headerText: 'Combobox Loja', width: '205px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'ComboboxLojaName', isDomain: true, domainName: 'LX_COMBOBOX_LOJA', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Combobox Loja (Name)', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null },
        { key: 'DatetimeLoja', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Datetime Loja', width: '205px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null },
        { key: 'DecimalLoja', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 13, isPartOfKey: false, headerText: 'Decimal Loja', width: '192px', dataType: 'number', format: '0.00', hidden: false, unbound: false, group: null },
        { key: 'GuidLoja', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 36, isPartOfKey: false, headerText: 'Guid Loja', width: '250px', dataType: 'string', format: '', hidden: false, unbound: false, group: null },
        { key: 'IdCidade', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Id Cidade', width: '271px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IdEstado', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Id Estado', width: '271px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IdPais', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Id Pais', width: '271px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IntLoja', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Int Loja', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'NomeCidade', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'Nome Cidade', width: '421px', dataType: 'string', format: '', hidden: false, unbound: false, group: null },
        { key: 'SmallIntLoja', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 6, isPartOfKey: false, headerText: 'Small Int Loja', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'StringEstado', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'Nome Estado', width: '421px', dataType: 'string', format: '', hidden: false, unbound: false, group: null },
        { key: 'StringLoja', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'String Loja', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null },
        { key: 'StringPais', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'Nome Pais', width: '421px', dataType: 'string', format: '', hidden: false, unbound: false, group: null }
    ];
    dataExportInfo['Venda'] = [ 
        { name: 'Venda', canExportMedia: true , canExportReport: true, actionExport: 'LinxDemoPaiFilha/GetVendaToExcel', actionReport: 'LinxDemoPaiFilha/GetVendaToReportXml', actionFeed: 'LinxDemoPaiFilhaOData/Venda', actionName: 'LinxDemoPaiFilha/GetVendaByEntitySearchNoAssociations', display: 'Venda',  metaData: function() { return metadataInfo['Venda']; } }
        , { name: 'VendaItem', canExportMedia: true , canExportReport: true, actionExport: 'LinxDemoPaiFilha/GetVendaItemParentCompositionToExcel', actionReport: 'LinxDemoPaiFilha/GetVendaItemParentCompositionToReportXml', actionFeed: 'LinxDemoPaiFilhaOData/VendaItemParentComposition', actionName: 'LinxDemoPaiFilha/GetVendaItemParentCompositionByEntitySearchNoAssociations', display: 'VendaItem',  metaData: function() { return metadataInfo['VendaItemParentComposition']; } }
    ];
    entitylookUps.push('Venda');
    entitylookUps['Venda'] = [];
    entitylookUps['Venda'].push('LookUpVendedor');
    lookUpNames.push('LookUpVendedor');
    metadataInfo['LookUpVendedor'] = [
        { key: 'IdVendedor', relatedKey: 'IdVendedor', maxLength: 10, isPartOfKey: true, headerText: 'Id Vendedor', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'StringVendedor', relatedKey: 'StringVendedor', maxLength: 50, isPartOfKey: false, headerText: 'String Vendedor', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null }
    ];
    entityNames.push('VendaItem');
    metadataInfo['VendaItem'] = [
        { key: 'ComboboxVendaItem', isQbeZero: false, isDomain: true, domainName: 'LX_VENDA_ITEM', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 3, isPartOfKey: false, headerText: 'Combobox Venda Item', width: '283px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'ComboboxVendaItemName', isDomain: true, domainName: 'LX_VENDA_ITEM', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Combobox Venda Item (Name)', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null },
        { key: 'DatetimeVendaItem', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Datetime Venda Item', width: '283px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null },
        { key: 'DecimalVendaItem', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 13, isPartOfKey: false, headerText: 'Decimal Venda Item', width: '270px', dataType: 'number', format: '0.00', hidden: false, unbound: false, group: null },
        { key: 'IdVenda', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Id Venda', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IdVendaItem', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 10, isPartOfKey: true, headerText: 'Id Venda Item', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'StringVendaItem', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'String Venda Item', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null }
    ];
    entityNames.push('VendaItemParentComposition');
    metadataInfo['VendaItemParentComposition'] = [
        { key: 'ComboboxVendaItem', isQbeZero: false, isDomain: true, domainName: 'LX_VENDA_ITEM', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 3, isPartOfKey: false, headerText: 'Combobox Venda Item', width: '283px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'ComboboxVendaItemName', isDomain: true, domainName: 'LX_VENDA_ITEM', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Combobox Venda Item (Name)', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null },
        { key: 'DatetimeVendaItem', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Datetime Venda Item', width: '283px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null },
        { key: 'DecimalVendaItem', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 13, isPartOfKey: false, headerText: 'Decimal Venda Item', width: '270px', dataType: 'number', format: '0.00', hidden: false, unbound: false, group: null },
        { key: 'IdVenda', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Id Venda', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IdVendaItem', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 10, isPartOfKey: true, headerText: 'Id Venda Item', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'StringVendaItem', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'String Venda Item', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null },
        { key: 'BitVenda', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Bit Venda', width: '153px', dataType: 'bool', format: 'checkbox', hidden: false, unbound: false, group: null },
        { key: 'ComboboxVenda', isQbeZero: false, isDomain: true, domainName: 'LX_VENDA', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 3, isPartOfKey: false, headerText: 'Combobox Venda', width: '218px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'ComboboxVendaName', isDomain: true, domainName: 'LX_VENDA', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Combobox Venda (Name)', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null },
        { key: 'DatetimeVenda', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Datetime Venda', width: '218px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null },
        { key: 'DecimalVenda', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 13, isPartOfKey: false, headerText: 'Decimal Venda', width: '205px', dataType: 'number', format: '0.00', hidden: false, unbound: false, group: null },
        { key: 'IdLoja', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Id Loja', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IdVendedor', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Id Vendedor', width: '271px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IntVenda', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Int Venda', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'SmallIntVenda', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 5, isPartOfKey: false, headerText: 'Small Int Venda', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'StringVenda', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'String Venda', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null },
        { key: 'StringVendedor', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'String Vendedor', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null },
        { key: 'BigIntLoja', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 24, isPartOfKey: false, headerText: 'Big Int Loja', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'BitLoja', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 1, isPartOfKey: false, headerText: 'Bit Loja', width: '140px', dataType: 'bool', format: 'checkbox', hidden: false, unbound: false, group: null },
        { key: 'ComboboxLoja', isQbeZero: false, isDomain: true, domainName: 'LX_COMBOBOX_LOJA', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 3, isPartOfKey: false, headerText: 'Combobox Loja', width: '205px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'ComboboxLojaName', isDomain: true, domainName: 'LX_COMBOBOX_LOJA', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Combobox Loja (Name)', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null },
        { key: 'DatetimeLoja', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Datetime Loja', width: '205px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null },
        { key: 'DecimalLoja', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 13, isPartOfKey: false, headerText: 'Decimal Loja', width: '192px', dataType: 'number', format: '0.00', hidden: false, unbound: false, group: null },
        { key: 'GuidLoja', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 36, isPartOfKey: false, headerText: 'Guid Loja', width: '250px', dataType: 'string', format: '', hidden: false, unbound: false, group: null },
        { key: 'IdCidade', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Id Cidade', width: '271px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IdEstado', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Id Estado', width: '271px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IdPais', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Id Pais', width: '271px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'IntLoja', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Int Loja', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'NomeCidade', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'Nome Cidade', width: '421px', dataType: 'string', format: '', hidden: false, unbound: false, group: null },
        { key: 'SmallIntLoja', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 6, isPartOfKey: false, headerText: 'Small Int Loja', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
        { key: 'StringEstado', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'Nome Estado', width: '421px', dataType: 'string', format: '', hidden: false, unbound: false, group: null },
        { key: 'StringLoja', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'String Loja', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null },
        { key: 'StringPais', isQbeZero: false, isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'Nome Pais', width: '421px', dataType: 'string', format: '', hidden: false, unbound: false, group: null }
    ];
    dataExportInfo['VendaItem'] = [ 
        { name: 'VendaItem', canExportMedia: true , canExportReport: true, actionExport: 'LinxDemoPaiFilha/GetVendaItemToExcel', actionReport: 'LinxDemoPaiFilha/GetVendaItemToReportXml', actionFeed: 'LinxDemoPaiFilhaOData/VendaItem', actionName: 'LinxDemoPaiFilha/GetVendaItemByEntitySearchNoAssociations', display: 'VendaItem',  metaData: function() { return metadataInfo['VendaItem']; } }
    ];
    entitylookUps.push('VendaItem');
    entitylookUps['VendaItem'] = [];
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
    namespace: "Linx.Demo.BV.PaiFilha",
    autoGeneratedKeyType: AutoGeneratedKeyType.Identity,
    dataProperties: {
    BigIntLoja: { dataType: DataType.Int64, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,BitLoja: { dataType: DataType.Boolean, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,ComboboxLoja: { dataType: DataType.Byte, isNullable: false, isPartOfKey: false, validators: [ Validator.hasValueValidator]  }
    ,ComboboxLojaName: { dataType: DataType.String, isNullable: false, isPartOfKey: false, validators: [] }
    ,DatetimeLoja: { dataType: DataType.DateTime, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,DecimalLoja: { dataType: DataType.Decimal, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,GuidLoja: { dataType: DataType.Guid, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,IdCidade: { dataType: DataType.Int32, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,IdEstado: { dataType: DataType.Int32, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,IdLoja: { dataType: DataType.Int32, isNullable: false, isPartOfKey: true, validators: [ Validator.hasValueValidator]  }
    ,IdPais: { dataType: DataType.Int32, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,IntLoja: { dataType: DataType.Int32, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,NomeCidade: { dataType: DataType.String, maxLength: 50, isNullable: true, isPartOfKey: false, validators: [ Validator.maxLength( {maxLength: 50})]  }
    ,SmallIntLoja: { dataType: DataType.Int16, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,StringEstado: { dataType: DataType.String, maxLength: 50, isNullable: true, isPartOfKey: false, validators: [ Validator.maxLength( {maxLength: 50})]  }
    ,StringLoja: { dataType: DataType.String, maxLength: 50, isNullable: true, isPartOfKey: false, validators: [ Validator.maxLength( {maxLength: 50})]  }
    ,StringPais: { dataType: DataType.String, maxLength: 50, isNullable: true, isPartOfKey: false, validators: [ Validator.maxLength( {maxLength: 50})]  }
                    },
    navigationProperties: {
    // Returns collections of details and associates with Parent
    VendaList: { entityTypeName: "Venda:#Linx.Demo.BV.PaiFilha", isScalar: false, invForeignKeyNames: ["IdLoja"], associationName: "FK_Loja_Venda" }
                          }
    });
    lookUpProperties['Loja'] = {IdCidade: 'LookUpCidade', IdEstado: 'LookUpCidade', IdPais: 'LookUpCidade', NomeCidade: 'LookUpCidade', StringEstado: 'LookUpCidade', StringPais: 'LookUpCidade'};
    var LojaInitializer = function (ownerReference, isPOCO) {
       ownerReference.RowDataId = (isPOCO === true ? getNextSequence('Loja') : ko.observable(getNextSequence('Loja')));
       ownerReference.currentVenda = ko.observable(null);
       //Adjust details for a POCO reference
       if (isPOCO === true) {
           ownerReference.VendaList = ko.observableArray(ownerReference.VendaList);
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
           if (noDetails !== true && ownerReference.VendaList && ownerReference.VendaList().length > 0) {
             var detailExpr = ownerReference.VendaList()[0].getJExpression(listFilterRange, ['IdLoja']);
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
                   for (var i = 0; i < ownerReference.VendaList().length; i++) {
                       var detail = ownerReference.VendaList()[i];
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
               result.VendaList = [];
               var sourceList = getAbsoluteValue(ownerReference.VendaList);
               if (sourceList && sourceList.length > 0) {
                   for (var i = 0; i < sourceList.length; i++) {
                       if (['U', 'I', 'D'].indexOf(sourceList[i].ChangeState) >= 0) result.VendaList.push(sourceList[i].getPrimitiveDTO(sourceList[i].ChangeState != 'D'));
                   }
               }
           }
           return result;
       };
       ownerReference.getAllDetailChanges = function() {
           var result = [];
           var _VendaList = getAbsoluteValue(ownerReference.VendaList);
           if (_VendaList && _VendaList.length > 0) {
               for (var i = 0; i < _VendaList.length; i++) {
                   var detail = _VendaList[i];
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
               if (ownerReference.VendaList && originData.VendaList) {
                   var toList = getAbsoluteValue(ownerReference.VendaList);
                   var fromList = getAbsoluteValue(originData.VendaList);
                   for (var idxElem = toList.length - 1; idxElem >= 0; idxElem--) {
                      if (toList[idxElem].ChangeState === 'D') toList.splice(idxElem, 1);
                   }
                   for (var idxElem = toList.length - 1; idxElem >= 0; idxElem--) {
                          if (toList[idxElem].ChangeState !== 'N') {
                               var fromObj = _.where(fromList, { IdVenda: toList[idxElem]['IdVenda'] });
                               if (fromObj.length > 0) toList[idxElem].copyDataFrom(fromObj[0], true);
                          }
                   }
               }
           }
       enableChangeTrack = true;
       };
          ownerReference.commitDetailsVisualPendings = function() {
              vm.dataBind('VendaList', true);
              if (ownerReference.currentVenda()) ownerReference.currentVenda().commitDetailsVisualPendings();
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
              breezeReference.VendaIsLoaded = ownerReference.VendaIsLoaded;
              for (var idx = 0; idx < ownerReference.VendaList().length; idx++) {
                  var entity = ownerReference.VendaList()[idx];
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
       ownerReference.serverDataType['BigIntLoja'] = 'L';
       ownerReference.serverDataType['BitLoja'] = 'B';
       ownerReference.serverDataType['ComboboxLoja'] = 'Y';
       ownerReference.serverDataType['DatetimeLoja'] = 'T';
       ownerReference.serverDataType['DecimalLoja'] = 'D';
       ownerReference.serverDataType['GuidLoja'] = 'G';
       ownerReference.serverDataType['IdCidade'] = 'I';
       ownerReference.serverDataType['IdEstado'] = 'I';
       ownerReference.serverDataType['IdLoja'] = 'I';
       ownerReference.serverDataType['IdPais'] = 'I';
       ownerReference.serverDataType['IntLoja'] = 'I';
       ownerReference.serverDataType['NomeCidade'] = 'S';
       ownerReference.serverDataType['SmallIntLoja'] = 'H';
       ownerReference.serverDataType['StringEstado'] = 'S';
       ownerReference.serverDataType['StringLoja'] = 'S';
       ownerReference.serverDataType['StringPais'] = 'S';
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
                   displayName = ' de CIDADE_UF_PAIS';
               }
               return 'Seleção' + displayName;
           };
        
           ownerReference.getSpecializedLookup = function (lookupName, lookupInfo, fieldToSearch, valueToSearch, ownerReference, allowMultiSelectionInSearch) {
               var specializedLookup = '';
               return specializedLookup;
           };
        
           ownerReference.getSubQueryFilterFromLookUpCidade = function (propertyName) {
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
               if (propertyName === 'IdCidade') {
                   var _IdEstado = getAbsoluteValue(this.IdEstado);
                   if (!isNullOrEmpty(_IdEstado)) { filter += (filter === '' ? '' : ';') + 'IdEstado' + (_IdEstado.toString().indexOf('[') > -1 ? '#In#S' : '#==#I') + _IdEstado.toString().replaceAll('[', '').replaceAll(']', ''); }
                   var _IdPais = getAbsoluteValue(this.IdPais);
                   if (!isNullOrEmpty(_IdPais)) { filter += (filter === '' ? '' : ';') + 'IdPais' + (_IdPais.toString().indexOf('[') > -1 ? '#In#S' : '#==#I') + _IdPais.toString().replaceAll('[', '').replaceAll(']', ''); }
                   var _StringPais = getAbsoluteValue(this.StringPais);
                   if (!isNullOrEmpty(_StringPais)) { filter += (filter === '' ? '' : ';') + 'StringPais' + (_StringPais.toString().indexOf('[') > -1 ? '#In#S' : '#==#S') + (_StringPais.toString().indexOf('[') > -1 ? 'S,' : '') + _StringPais.toString().replaceAll('[', '').replaceAll(']', ''); }
                   var _StringEstado = getAbsoluteValue(this.StringEstado);
                   if (!isNullOrEmpty(_StringEstado)) { filter += (filter === '' ? '' : ';') + 'StringEstado' + (_StringEstado.toString().indexOf('[') > -1 ? '#In#S' : '#==#S') + (_StringEstado.toString().indexOf('[') > -1 ? 'S,' : '') + _StringEstado.toString().replaceAll('[', '').replaceAll(']', ''); }
               }
               if (propertyName === 'NomeCidade') {
                   var _IdEstado = getAbsoluteValue(this.IdEstado);
                   if (!isNullOrEmpty(_IdEstado)) { filter += (filter === '' ? '' : ';') + 'IdEstado' + (_IdEstado.toString().indexOf('[') > -1 ? '#In#S' : '#==#I') + _IdEstado.toString().replaceAll('[', '').replaceAll(']', ''); }
                   var _IdPais = getAbsoluteValue(this.IdPais);
                   if (!isNullOrEmpty(_IdPais)) { filter += (filter === '' ? '' : ';') + 'IdPais' + (_IdPais.toString().indexOf('[') > -1 ? '#In#S' : '#==#I') + _IdPais.toString().replaceAll('[', '').replaceAll(']', ''); }
                   var _StringPais = getAbsoluteValue(this.StringPais);
                   if (!isNullOrEmpty(_StringPais)) { filter += (filter === '' ? '' : ';') + 'StringPais' + (_StringPais.toString().indexOf('[') > -1 ? '#In#S' : '#==#S') + (_StringPais.toString().indexOf('[') > -1 ? 'S,' : '') + _StringPais.toString().replaceAll('[', '').replaceAll(']', ''); }
                   var _StringEstado = getAbsoluteValue(this.StringEstado);
                   if (!isNullOrEmpty(_StringEstado)) { filter += (filter === '' ? '' : ';') + 'StringEstado' + (_StringEstado.toString().indexOf('[') > -1 ? '#In#S' : '#==#S') + (_StringEstado.toString().indexOf('[') > -1 ? 'S,' : '') + _StringEstado.toString().replaceAll('[', '').replaceAll(']', ''); }
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
               lookupInfo.isMultiSelection = lookupName.in(['LookUpVendedor']);
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
           setAbsoluteValue(ownerReference, 'DatetimeLoja', getCurrentDate());
       };
       ownerReference.delete = function() {
           if (ownerReference.isDetached()) {
               app.showMessage('A informação selecionada não pode ser excluída!', 'Alerta', ['Ok']);
               return;
           }
           if (ownerReference.setParentAsModified) ownerReference.setParentAsModified();
           if (!isNullOrEmpty(ownerReference.VendaList()) && ownerReference.VendaList().length > 0) {
              var details = [].concat(ownerReference.VendaList());
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
       ownerReference.namespace = 'Linx.Demo.BV.PaiFilha';
       ownerReference.myProperties = [ 'BigIntLoja','BitLoja','ComboboxLoja','DatetimeLoja','DecimalLoja','GuidLoja','IdCidade','IdEstado','IdLoja','IdPais','IntLoja','NomeCidade','SmallIntLoja','StringEstado','StringLoja','StringPais' ];
       ownerReference.queryRequiredProperties = {  };
       ownerReference.excludedFilters = [];
       ownerReference.getCurrentElements = function() {
           var result = [ ownerReference ];
       if (!isNullOrEmpty(ownerReference.currentVenda())) { result = result.concat(ownerReference.currentVenda().getCurrentElements()); }
           return result;
       };
       ownerReference.checkForSendingAllRowsToServer = function() {
       };
       ownerReference.GetJsWhereDetailRelationForVenda = function(customParentRelation) {
       return 'Venda{' + (!isNullOrEmpty(customParentRelation) ? customParentRelation : 'IdLoja#==#' + ownerReference.serverDataType['IdLoja'] + getAbsoluteValue(ownerReference.IdLoja).toString()) + '}';    
       }
       ownerReference.VendaIsLoaded = false;
       ownerReference.detailsLoaded = function() {
           return ownerReference.VendaIsLoaded;
       }
       ownerReference.atLeastOneDetailLoaded = function() {
           return ownerReference.VendaIsLoaded;
       }
       ownerReference.adjustDetailsLoaded = function(value) {
           ownerReference.VendaIsLoaded = value;
           if (value === false && ownerReference.isPOCO)
               ownerReference.VendaList([]);
       }
       ownerReference.fillDetails = function(force, detailName, noInnerUIs, noWait, callback, customParentRelation) {
          if (typeof force === 'undefined') force = false;
          if (force) vm.clearInnerUIs(ownerReference);
          if (!noInnerUIs) vm.queryInnerUIs(ownerReference);
          if (ownerReference.isAdded()) {
            ownerReference.VendaIsLoaded = true;
          }
          var _VendaRemoteComplete = false;
          var detachList_Venda = [];
          if (force) {
               if (isNullOrEmpty(detailName) || detailName == 'Venda') ownerReference.VendaIsLoaded = false;
               if ((isNullOrEmpty(detailName) || detailName == 'Venda') && ownerReference.VendaList && ownerReference.VendaList().length > 0) {
                   if (ownerReference.isPOCO) {
                       ownerReference.VendaList([]);
                   } else {
                       var detailList = ownerReference.VendaList();
                       for (var idx = detailList.length - 1; idx >= 0; idx--) {
                           detachList_Venda.push(detailList[idx]);
                       }
                   }
               }
          }
    
          if (!ownerReference.VendaIsLoaded) {
            //Load VendaList
            if (isNullOrEmpty(detailName) || detailName === 'Venda') {
              ownerReference.VendaIsLoaded = true;
              _VendaRemoteComplete = (ownerReference.VendaList && ownerReference.VendaList().length > 0);
              if ((force || !ownerReference.VendaList || ownerReference.VendaList().length === 0) && (!isNullOrEmpty(getAbsoluteValue(ownerReference.IdLoja)))) {
                var navQuery = EntityQuery.from('GetVendaByEntitySearchNoAssociations').noTracking(ownerReference.isPOCO === true)
                .orderBy('IdVenda asc')
                    .withParameters({ jEntitySearch: ownerReference.GetJsWhereDetailRelationForVenda(customParentRelation) })    ;
                if (!vm.dataToolbar._noBusyLoading) vm.showProcessing('Pesquisando detalhes...');
                manager.executeQuery(navQuery).then(function (data) { if (ownerReference.isPOCO) { for (var idx = 0; idx < data.results.length; idx++) { initializePOCO(data.results[idx], 'Venda'); data.results[idx].Loja = ko.observable(ownerReference); } ownerReference.VendaList(data.results); } 
                   if (!ownerReference.isPOCO && detachList_Venda.length > 0)
                   {
                       for (var idx = 0; idx < detachList_Venda.length; idx++)
                       {
                           if (!data.results.contains(detachList_Venda[idx]))
                               detachEntity(detachList_Venda[idx]);
                           else {
                               if (force && detachList_Venda[idx].atLeastOneDetailLoaded())
                                   detachList_Venda[idx].fillDetails(force, '', false, noWait);
                           }
                       }
                   }
                   ownerReference.setCurrentDetails('Venda'); notifyPresentation('VendaList');
                   _VendaRemoteComplete = true;
                   if (callback && (!isNullOrEmpty(detailName) || (_VendaRemoteComplete))) { callback(); }
                }).fail(queryFailed).fin(function() { if (!vm.dataToolbar._noBusyLoading) vm.closeProcessing(); });
              } else { ownerReference.setCurrentDetails('Venda'); notifyPresentation('VendaList'); }
            } else { _VendaRemoteComplete = true; if (!ownerReference.VendaIsLoaded && ownerReference.VendaList && ownerReference.VendaList().length > 0) { ownerReference.VendaIsLoaded = true; ownerReference.setCurrentDetails('Venda'); } }
          } else { 
            if (isNullOrEmpty(detailName) || detailName == 'Venda') {
               notifyPresentation('VendaList');
               ownerReference.setCurrentDetails('Venda');
            }
            _VendaRemoteComplete = true;
          }
          if (callback && ((!isNullOrEmpty(detailName) && (eval('_' + detailName + 'RemoteComplete && ownerReference.' + detailName + 'IsLoaded') == true)) || (isNullOrEmpty(detailName) && (_VendaRemoteComplete)))) { callback(); }
       };
       //Select first element as a current item of each detail
       ownerReference.setCurrentDetails = function(detailName, clearing) {
          if ((isNullOrEmpty(detailName) || detailName === 'Venda')) {
               if (ownerReference.VendaList().length > 0) { ownerReference.currentVenda(ownerReference.VendaList()[0]); if (clearing == null || clearing === false) ownerReference.currentVenda().fillDetails(); }
               else { ownerReference.currentVenda(null); ownerReference.notifyEmptyDetails('Venda'); }
          }
       };
       ownerReference.notifyEmptyDetails = function(detailName) {
          if (detailName === 'Venda') {
               notifyPresentation('VendaList');
               vm.queryInnerUIs(null, 'Venda');
               notifyPresentation('VendaItemList');
               vm.queryInnerUIs(null, 'VendaItem');
          }
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
    //#region Adjust details already loaded for a POCO reference
       if (isPOCO === true) {
           if ((typeof ownerReference.VendaList === 'function') && ownerReference.VendaList().length > 0) {
                for(var idx = 0; idx < ownerReference.VendaList().length; idx++) { VendaInitializer(ownerReference.VendaList()[idx], isPOCO); }
           }
       }
    //#endregion Adjust details already loaded for a POCO reference
    };
    metadataStore.registerEntityTypeCtor("Loja", null, LojaInitializer);
    
    // Configure Venda data type
    metadataStore.addEntityType({
    shortName: "Venda",
    namespace: "Linx.Demo.BV.PaiFilha",
    autoGeneratedKeyType: AutoGeneratedKeyType.Identity,
    dataProperties: {
    BitVenda: { dataType: DataType.Boolean, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,ComboboxVenda: { dataType: DataType.Byte, isNullable: false, isPartOfKey: false, validators: [ Validator.hasValueValidator]  }
    ,ComboboxVendaName: { dataType: DataType.String, isNullable: false, isPartOfKey: false, validators: [] }
    ,DatetimeVenda: { dataType: DataType.DateTime, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,DecimalVenda: { dataType: DataType.Decimal, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,IdLoja: { dataType: DataType.Int32, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,IdVenda: { dataType: DataType.Int32, isNullable: false, isPartOfKey: true, validators: [ Validator.hasValueValidator]  }
    ,IdVendedor: { dataType: DataType.Int32, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,IntVenda: { dataType: DataType.Int32, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,SmallIntVenda: { dataType: DataType.Int16, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,StringVenda: { dataType: DataType.String, maxLength: 50, isNullable: true, isPartOfKey: false, validators: [ Validator.maxLength( {maxLength: 50})]  }
    ,StringVendedor: { dataType: DataType.String, maxLength: 50, isNullable: true, isPartOfKey: false, validators: [ Validator.maxLength( {maxLength: 50})]  }
                    },
    navigationProperties: {
    // Returns a single parent and associates with Details
    Loja: { entityTypeName: "Loja:#Linx.Demo.BV.PaiFilha", isScalar: true, foreignKeyNames: ["IdLoja"], associationName: "FK_Loja_Venda" }
    // Returns collections of details and associates with Parent
    ,VendaItemList: { entityTypeName: "VendaItem:#Linx.Demo.BV.PaiFilha", isScalar: false, invForeignKeyNames: ["IdVenda"], associationName: "FK_Venda_VendaItem" }
                          }
    });
    lookUpProperties['Venda'] = {IdVendedor: 'LookUpVendedor', StringVendedor: 'LookUpVendedor'};
    var VendaInitializer = function (ownerReference, isPOCO) {
       ownerReference.RowDataId = (isPOCO === true ? getNextSequence('Venda') : ko.observable(getNextSequence('Venda')));
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
              breezeReference.VendaItemIsLoaded = ownerReference.VendaItemIsLoaded;
              for (var idx = 0; idx < ownerReference.VendaItemList().length; idx++) {
                  var entity = ownerReference.VendaItemList()[idx];
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
       ownerReference.serverDataType['BitVenda'] = 'B';
       ownerReference.serverDataType['ComboboxVenda'] = 'Y';
       ownerReference.serverDataType['DatetimeVenda'] = 'T';
       ownerReference.serverDataType['DecimalVenda'] = 'D';
       ownerReference.serverDataType['IdLoja'] = 'I';
       ownerReference.serverDataType['IdVenda'] = 'I';
       ownerReference.serverDataType['IdVendedor'] = 'I';
       ownerReference.serverDataType['IntVenda'] = 'I';
       ownerReference.serverDataType['SmallIntVenda'] = 'H';
       ownerReference.serverDataType['StringVenda'] = 'S';
       ownerReference.serverDataType['StringVendedor'] = 'S';
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
               if (lookupName === 'LookUpVendedor') {
                   displayName = ' de Vendedor';
               }
               return 'Seleção' + displayName;
           };
        
           ownerReference.getSpecializedLookup = function (lookupName, lookupInfo, fieldToSearch, valueToSearch, ownerReference, allowMultiSelectionInSearch) {
               var specializedLookup = '';
               return specializedLookup;
           };
        
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
               lookupInfo.isMultiSelection = lookupName.in(['LookUpVendedor']);
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
           var parent = getAbsoluteValue(ownerReference.Loja);
           if (!isNullOrEmpty(ownerReference.VendaItemList()) && ownerReference.VendaItemList().length > 0) {
              var details = [].concat(ownerReference.VendaItemList());
              for (var idx = 0; idx < details.length; idx++) {
                details[idx].delete();
              }
           }
           if (ownerReference.entityAspect) ownerReference.entityAspect.setDeleted(); // mark for deletion
           if (parent && (typeof parent.setCurrentDetails === 'function') && (typeof parent.VendaList === 'function') && parent.VendaList().length == 0) parent.setCurrentDetails('Venda');
       };
       ownerReference.setParentAsModified = function() {
       var parent = getAbsoluteValue(ownerReference.Loja);
       if (parent) {
           if (parent.isUnchanged()) {
               parent.setModified(); 
           }
           parent.setParentAsModified();
       }
       };
       ownerReference.getParent = function() {
           return getAbsoluteValue(ownerReference.Loja);
       };
       ownerReference.getSelfList = function() {
           var parent = ownerReference.getParent();
           if (!isNullOrEmpty(parent)) {
               return getAbsoluteValue(parent.VendaList);
           } else { return null; }
       };
       ownerReference.namespace = 'Linx.Demo.BV.PaiFilha';
       ownerReference.myProperties = [ 'BitVenda','ComboboxVenda','DatetimeVenda','DecimalVenda','IdLoja','IdVenda','IdVendedor','IntVenda','SmallIntVenda','StringVenda','StringVendedor' ];
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
    namespace: "Linx.Demo.BV.PaiFilha",
    autoGeneratedKeyType: AutoGeneratedKeyType.Identity,
    dataProperties: {
    ComboboxVendaItem: { dataType: DataType.Byte, isNullable: false, isPartOfKey: false, validators: [ Validator.hasValueValidator]  }
    ,ComboboxVendaItemName: { dataType: DataType.String, isNullable: false, isPartOfKey: false, validators: [] }
    ,DatetimeVendaItem: { dataType: DataType.DateTime, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,DecimalVendaItem: { dataType: DataType.Decimal, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,IdVenda: { dataType: DataType.Int32, isNullable: true, isPartOfKey: false, validators: [ ]  }
    ,IdVendaItem: { dataType: DataType.Int32, isNullable: false, isPartOfKey: true, validators: [ Validator.hasValueValidator]  }
    ,StringVendaItem: { dataType: DataType.String, maxLength: 50, isNullable: true, isPartOfKey: false, validators: [ Validator.maxLength( {maxLength: 50})]  }
                    },
    navigationProperties: {
    // Returns a single parent and associates with Details
    Venda: { entityTypeName: "Venda:#Linx.Demo.BV.PaiFilha", isScalar: true, foreignKeyNames: ["IdVenda"], associationName: "FK_Venda_VendaItem" }
    // Returns collections of details and associates with Parent
                          }
    });
    lookUpProperties['VendaItem'] = {};
    var VendaItemInitializer = function (ownerReference, isPOCO) {
       ownerReference.RowDataId = (isPOCO === true ? getNextSequence('VendaItem') : ko.observable(getNextSequence('VendaItem')));
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
       ownerReference.serverDataType['ComboboxVendaItem'] = 'Y';
       ownerReference.serverDataType['DatetimeVendaItem'] = 'T';
       ownerReference.serverDataType['DecimalVendaItem'] = 'D';
       ownerReference.serverDataType['IdVenda'] = 'I';
       ownerReference.serverDataType['IdVendaItem'] = 'I';
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
       ownerReference.UpdateIndependentRelation = function(detailName) {
           var cacheElements = dataContext.getEntities(detailName);
           for (var idxR = 0; idxR < cacheElements.length; idxR++) {
               if (typeof cacheElements[idxR].VendaItem !== 'function') { return; }
               else  if (cacheElements[idxR].VendaItem() != ownerReference) { cacheElements[idxR].VendaItem(ownerReference); }
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
           var parent = getAbsoluteValue(ownerReference.Venda);
           if (ownerReference.entityAspect) ownerReference.entityAspect.setDeleted(); // mark for deletion
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
       ownerReference.namespace = 'Linx.Demo.BV.PaiFilha';
       ownerReference.myProperties = [ 'ComboboxVendaItem','DatetimeVendaItem','DecimalVendaItem','IdVenda','IdVendaItem','StringVendaItem' ];
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
    //#endregion Classes Map
    //#region Context Definition
    
    //#region Get LookUps
    
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
        resetSequence('Venda');
        var refVenda = manager.createEntity('Venda', {}, breeze.EntityState.Unchanged);
        refLoja.currentVenda(refVenda);
        resetSequence('VendaItem');
        var refVendaItem = manager.createEntity('VendaItem', {}, breeze.EntityState.Unchanged);
        refVenda.currentVendaItem(refVendaItem);
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
    //#endregion
    
    // Create the data update event.
    var dataUpdateEvent = document.createEvent('Event');
    // Define that the event name is 'PaiFilhaContext_DataUpdate'.
    var contextUpdtEvt = 'PaiFilhaContext_DataUpdate_' + getNewGuid();
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
            if (propertyName === '' || propertyName === 'IdCidade') {
               if (selectedElement.hasOwnProperty('IdCidade') && replaceTo.hasOwnProperty('IdCidade'))
               {
                   setAbsoluteValue(replaceTo, 'IdCidade', getAbsoluteValue(selectedElement['IdCidade']));
               }
               else if (replaceTo.hasOwnProperty('IdCidade')) {
                   setAbsoluteValue(replaceTo, 'IdCidade', null);
               }
            }
            if (propertyName === '' || propertyName === 'NomeCidade') {
               if (selectedElement.hasOwnProperty('NomeCidade') && replaceTo.hasOwnProperty('NomeCidade'))
               {
                   setAbsoluteValue(replaceTo, 'NomeCidade', getAbsoluteValue(selectedElement['NomeCidade']));
               }
               else if (replaceTo.hasOwnProperty('NomeCidade')) {
                   setAbsoluteValue(replaceTo, 'NomeCidade', null);
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
        setAbsoluteValue(replaceTo, 'IdEstado', null);
        setAbsoluteValue(replaceTo, 'IdPais', null);
        setAbsoluteValue(replaceTo, 'StringPais', null);
        setAbsoluteValue(replaceTo, 'StringEstado', null);
        setAbsoluteValue(replaceTo, 'IdCidade', null);
        setAbsoluteValue(replaceTo, 'NomeCidade', null);
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
        var parent = getAbsoluteValue(replaceTo.Loja);
        isFinalizingLookup(true);
        if (!propertyName)
            propertyName = '';
        var isUsedOriginalRow = false;
        for (var i = 0; i < selectedElements.length; i++)
        {
            var selectedElement = selectedElements[i];
            if (isUsedOriginalRow) {
                replaceTo = lookupInfo.vm.createVenda(parent);
            }
            else {
                isUsedOriginalRow = true;
            }
            if (propertyName === '' || propertyName === 'IdVendedor') {
               if (selectedElement.hasOwnProperty('IdVendedor') && replaceTo.hasOwnProperty('IdVendedor'))
               {
                   setAbsoluteValue(replaceTo, 'IdVendedor', getAbsoluteValue(selectedElement['IdVendedor']));
               }
               else if (replaceTo.hasOwnProperty('IdVendedor')) {
                   setAbsoluteValue(replaceTo, 'IdVendedor', null);
               }
            }
            if (propertyName === '' || propertyName === 'StringVendedor') {
               if (selectedElement.hasOwnProperty('StringVendedor') && replaceTo.hasOwnProperty('StringVendedor'))
               {
                   setAbsoluteValue(replaceTo, 'StringVendedor', getAbsoluteValue(selectedElement['StringVendedor']));
               }
               else if (replaceTo.hasOwnProperty('StringVendedor')) {
                   setAbsoluteValue(replaceTo, 'StringVendedor', null);
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
        setAbsoluteValue(replaceTo, 'StringVendedor', null);
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
               url: getServiceAddress('LinxDemoPaiFilha/Save' + vm.rootDataTypeName),
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
    
    var createVenda = function(parent, noCurrent) {
        //Create entity instance
        enableChangeTrack = false;
        var entity = createEntity('Venda', { Loja: parent, BitVenda: false });
        entity.setDefaults();
        if (typeof entity.OnAdding == 'function') {
            if (!entity.OnAdding()) { dataContext.deleteEntity(entity); return; }
        }
        if (noCurrent !== true) parent.currentVenda(entity);
        if (parent && (typeof parent.setCurrentDetails === 'function') && (typeof parent.VendaList === 'function') && parent.VendaList().length == 0) parent.setCurrentDetails('Venda');
        if (entity.setParentAsModified) entity.setParentAsModified();
        enableChangeTrack = true;
        return entity;
    };
    
    var createVendaItem = function(parent, noCurrent) {
        //Create entity instance
        enableChangeTrack = false;
        var entity = createEntity('VendaItem', { Venda: parent });
        entity.setDefaults();
        if (typeof entity.OnAdding == 'function') {
            if (!entity.OnAdding()) { dataContext.deleteEntity(entity); return; }
        }
        if (noCurrent !== true) parent.currentVendaItem(entity);
        if (parent && (typeof parent.setCurrentDetails === 'function') && (typeof parent.VendaItemList === 'function') && parent.VendaItemList().length == 0) parent.setCurrentDetails('VendaItem');
        if (entity.setParentAsModified) entity.setParentAsModified();
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
           url: getServiceAddress("LinxDemoPaiFilha/GetReportDataSource"),
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
           url: getServiceAddress("LinxDemoPaiFilha/GetTemplateReport"),
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
            createVendaItem: createVendaItem,
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
                getLookUpVendedorByEntitySearch: getLookUpVendedorByEntitySearch,
            getLookUpCidadeByEntitySearch: getLookUpCidadeByEntitySearch,
            getBmEntityProperties: getBmEntityProperties,
            clearLoja: clearLoja,
            getLoja: getLoja,
            getLojaByEntitySearchNoAssociations: getLojaByEntitySearchNoAssociations,
            clearVenda: clearVenda,
            getVenda: getVenda,
            getVendaByEntitySearchNoAssociations: getVendaByEntitySearchNoAssociations,
            clearVendaItem: clearVendaItem,
            getVendaItem: getVendaItem,
            getVendaItemByEntitySearchNoAssociations: getVendaItemByEntitySearchNoAssociations,
                finalizeAllLookUpCidade: finalizeAllLookUpCidade,
            clearLookUpCidade: clearLookUpCidade,
            finalizeAllLookUpVendedor: finalizeAllLookUpVendedor,
            clearLookUpVendedor: clearLookUpVendedor
        };
    loadParameters();
    return dataContext;
    //#endregion Context Definition
}
return result;
});
