    /* jshint ignore:start */
    'use strict';
    
    var name = 'Demo_PaiFilhaClientErpService';
    
    var dependencies = [
            '$log',
            'UUIDFactory',
            'httpFactory',
            'commonFactory',
            'dialogFactory',
            'messengeFactory',
            'shellManagerService',
            'Demo_ClientErpDataDomainsFactory'
    ];
    
    var serviceAPI = function ($log, uuid, httpFactory, common, dialog, messenger, shellManagerService, dataDomains) {
       var ctrContext = function () { return new dataContextConstructor($log, uuid, httpFactory, common, dialog, messenger, shellManagerService, dataDomains); };
       return ctrContext;
    }
    var dataContextConstructor = function ($log, uuid, httpFactory, common, dialog, messenger, shellManagerService, dataDomains) {
        var getServiceAddress = function(apiPart) {
           var serviceBus = shellManagerService.getServiceUrl(apiPart, businessAssemblyName);
           return serviceBus;
        };
        var getNewGuid = function() {
           return uuid.newguid();
        };
        var getDataFeedUrl = function() {
           var baseApi = getDataServiceUrl();
           return common.strLeft(baseApi, baseApi.length - 1) + 'OData/';
        };
        var getDataServiceUrl = function (reset) {
           var baseApi = (!reset && dataService && !common.isNullOrEmpty(dataService.serviceName) ? dataService.serviceName : getServiceAddress(controllerName));
           return baseApi + (common.strRight(baseApi, 1) == '/' ? '' : '/');
        };
        var setServiceBusUrl = function (url) {
           if (dataService) { dataService.serviceName = (common.isNullOrEmpty(url) ? getDataServiceUrl(true) : url + (common.strRight(url, 1) == '/' ? '' : '/') + controllerName + '/'); }
        };
        var initializePOCO = function(ownerReference, entityName) {
           if (ownerReference) { eval(entityName + 'Initializer(ownerReference);'); }
        };
        var getPivotLayouts = function (params, success, error) {
           return httpFactory.httpGet(getDataServiceUrl, getServiceAddress('linxframeworkobjeto') + '/GetPivotLayouts?' +
                                                                       'rootNameSpace=' + params.rootNamespace +
                                                                       '&viewName=' + params.viewName +
                                                                       '&pivotName=' + params.pivotName +
                                                                       '&pivotDataSource=' + params.pivotDataSource, success, error);
        };
        var getSelectedLayoutContent = function (params, success, error) {
            return httpFactory.httpGet(getDataServiceUrl, getServiceAddress('linxframeworkobjeto') + '/GetPivotLayout?uidObjetoConteudo=' + params.uidObjetoConteudo, success, error);
        };
        var businessAssemblyName = 'Linx.Demo.BV';
        var controllerName = 'LinxDemoPaiFilha';
        var dataService = { serviceName: getDataServiceUrl(true) /*WebApi Service Address*/ };
        var enableChangeTrack = true;
        var entityPropChanged = function(entity, propName, oldVal, newVal) {
            if (!enableChangeTrack) return true;
            var result = true;
            if ((typeof entity.OnPropertyChanged == 'function') && oldVal !== newVal)
                result = (entity.OnPropertyChanged(propName, oldVal, newVal) !== false);
            if (result && ['U', 'I', 'D'].indexOf(entity.ChangeState) < 0) { entity.createOriginal(propName, oldVal); entity.ChangeState = 'U'; if (entity.setParentAsModified) entity.setParentAsModified(); }
            return result;
        }
        //#region Metadata Info
        var metadataInfo = [];
        var dataExportInfo = [];
        var entityNames = [];
        var lookUpNames = [];
        var entitylookUps = [];
        entityNames.push('Cliente');
        metadataInfo['Cliente'] = [
            { key: 'BigIntCliente', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 24, isPartOfKey: false, headerText: 'Big Int Cliente', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'BitCliente', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Bit Cliente', width: '179px', dataType: 'bool', format: 'checkbox', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'ComboboxCliente', isDomain: true, domainName: 'LX_CLIENTE', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 3, isPartOfKey: false, headerText: 'Combobox Cliente', width: '244px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: 0 },
            { key: 'ComboboxClienteName', isDomain: true, domainName: 'LX_CLIENTE', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Combobox Cliente (Name)', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null, defaultValue: '' },
            { key: 'DatetimeCliente', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Datetime Cliente', width: '244px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'DecimalCliente', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 13, isPartOfKey: false, headerText: 'Decimal Cliente', width: '231px', dataType: 'number', format: '0.00', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'GuidCliente', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 36, isPartOfKey: false, headerText: 'Guid Cliente', width: '250px', dataType: 'string', format: '', hidden: true, unbound: false, group: null, defaultValue: null },
            { key: 'IdCliente', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 12, isPartOfKey: true, headerText: 'Id Cliente', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: 0 },
            { key: 'IdEstado', isDomain: false, domainName: '', lookupPropertyName: 'IdEstado', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Id Estado', width: '271px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'IntCliente', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Int Cliente', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'SmallIntCliente', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 6, isPartOfKey: false, headerText: 'Small Int Cliente', width: '257px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'StringCliente', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'String Cliente', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null, defaultValue: '' },
            { key: 'StringEstado', isDomain: false, domainName: '', lookupPropertyName: 'StringEstado', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'String Estado', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'ChangeState', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: '', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null, defaultValue: 'N' }
        ];
        dataExportInfo['Cliente'] = [ 
            { name: 'Cliente', canExportMedia: true , canExportReport: true, actionExport: 'LinxDemoPaiFilha/GetClienteToExcel', actionReport: 'LinxDemoPaiFilha/GetClienteToReportXml', actionFeed: 'LinxDemoPaiFilhaOData/Cliente', actionName: 'LinxDemoPaiFilha/GetClienteByEntitySearchNoAssociations', display: 'Cliente',  metaData: function() { return metadataInfo['Cliente']; } }
            , { name: 'Venda', canExportMedia: true , canExportReport: true, actionExport: 'LinxDemoPaiFilha/GetVendaParentCompositionToExcel', actionReport: 'LinxDemoPaiFilha/GetVendaParentCompositionToReportXml', actionFeed: 'LinxDemoPaiFilhaOData/VendaParentComposition', actionName: 'LinxDemoPaiFilha/GetVendaParentCompositionByEntitySearchNoAssociations', display: 'Venda',  metaData: function() { return metadataInfo['VendaParentComposition']; } }
            , { name: 'VendaItem', canExportMedia: true , canExportReport: true, actionExport: 'LinxDemoPaiFilha/GetVendaItemParentCompositionToExcel', actionReport: 'LinxDemoPaiFilha/GetVendaItemParentCompositionToReportXml', actionFeed: 'LinxDemoPaiFilhaOData/VendaItemParentComposition', actionName: 'LinxDemoPaiFilha/GetVendaItemParentCompositionByEntitySearchNoAssociations', display: 'VendaItem',  metaData: function() { return metadataInfo['VendaItemParentComposition']; } }
            , { name: 'VendaAtacado', canExportMedia: true , canExportReport: true, actionExport: 'LinxDemoPaiFilha/GetVendaAtacadoParentCompositionToExcel', actionReport: 'LinxDemoPaiFilha/GetVendaAtacadoParentCompositionToReportXml', actionFeed: 'LinxDemoPaiFilhaOData/VendaAtacadoParentComposition', actionName: 'LinxDemoPaiFilha/GetVendaAtacadoParentCompositionByEntitySearchNoAssociations', display: 'VendaAtacado',  metaData: function() { return metadataInfo['VendaAtacadoParentComposition']; } }
        ];
        entitylookUps.push('Cliente');
        entitylookUps['Cliente'] = [];
        entitylookUps['Cliente'].push('LookUpEstado');
        lookUpNames.push('LookUpEstado');
        metadataInfo['LookUpEstado'] = [
            { key: 'IdEstado', relatedKey: 'IdEstado', maxLength: 10, isPartOfKey: true, headerText: 'Id Estado', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null },
            { key: 'StringEstado', relatedKey: 'StringEstado', maxLength: 50, isPartOfKey: false, headerText: 'String Estado', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null }
        ];
        entityNames.push('Venda');
        metadataInfo['Venda'] = [
            { key: 'BigIntVenda', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 24, isPartOfKey: false, headerText: 'Big Int Venda', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'BitVenda', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Bit Venda', width: '153px', dataType: 'bool', format: 'checkbox', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'ComboboxVenda', isDomain: true, domainName: 'LX_VENDA', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 3, isPartOfKey: false, headerText: 'Combobox Venda', width: '218px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: 0 },
            { key: 'ComboboxVendaName', isDomain: true, domainName: 'LX_VENDA', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Combobox Venda (Name)', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null, defaultValue: '' },
            { key: 'DatetimeVenda', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Datetime Venda', width: '218px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'DecimalVenda', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 13, isPartOfKey: false, headerText: 'Decimal Venda', width: '205px', dataType: 'number', format: '0.00', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'GuidVenda', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 36, isPartOfKey: false, headerText: 'Guid Venda', width: '250px', dataType: 'string', format: '', hidden: true, unbound: false, group: null, defaultValue: null },
            { key: 'IdCliente', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Id Cliente', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'IdLoja', isDomain: false, domainName: '', lookupPropertyName: 'IdLoja', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Id Loja', width: '271px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'IdVenda', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 12, isPartOfKey: true, headerText: 'Id Venda', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: 0 },
            { key: 'IntVenda', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Int Venda', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'SmallIntVenda', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 6, isPartOfKey: false, headerText: 'Small Int Venda', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'StringVenda', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'String Venda', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null, defaultValue: '' },
            { key: 'ChangeState', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: '', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null, defaultValue: 'N' }
        ];
        entityNames.push('VendaParentComposition');
        metadataInfo['VendaParentComposition'] = [
            { key: 'BigIntVenda', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 24, isPartOfKey: false, headerText: 'Big Int Venda', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'BitVenda', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Bit Venda', width: '153px', dataType: 'bool', format: 'checkbox', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'ComboboxVenda', isDomain: true, domainName: 'LX_VENDA', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 3, isPartOfKey: false, headerText: 'Combobox Venda', width: '218px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: 0 },
            { key: 'ComboboxVendaName', isDomain: true, domainName: 'LX_VENDA', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Combobox Venda (Name)', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null, defaultValue: '' },
            { key: 'DatetimeVenda', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Datetime Venda', width: '218px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'DecimalVenda', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 13, isPartOfKey: false, headerText: 'Decimal Venda', width: '205px', dataType: 'number', format: '0.00', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'GuidVenda', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 36, isPartOfKey: false, headerText: 'Guid Venda', width: '250px', dataType: 'string', format: '', hidden: true, unbound: false, group: null, defaultValue: null },
            { key: 'IdCliente', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Id Cliente', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'IdLoja', isDomain: false, domainName: '', lookupPropertyName: 'IdLoja', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Id Loja', width: '271px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'IdVenda', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 12, isPartOfKey: true, headerText: 'Id Venda', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: 0 },
            { key: 'IntVenda', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Int Venda', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'SmallIntVenda', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 6, isPartOfKey: false, headerText: 'Small Int Venda', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'StringVenda', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'String Venda', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null, defaultValue: '' },
            { key: 'BigIntCliente', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 24, isPartOfKey: false, headerText: 'Big Int Cliente', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'BitCliente', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Bit Cliente', width: '179px', dataType: 'bool', format: 'checkbox', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'ComboboxCliente', isDomain: true, domainName: 'LX_CLIENTE', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 3, isPartOfKey: false, headerText: 'Combobox Cliente', width: '244px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: 0 },
            { key: 'ComboboxClienteName', isDomain: true, domainName: 'LX_CLIENTE', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Combobox Cliente (Name)', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null, defaultValue: '' },
            { key: 'DatetimeCliente', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Datetime Cliente', width: '244px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'DecimalCliente', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 13, isPartOfKey: false, headerText: 'Decimal Cliente', width: '231px', dataType: 'number', format: '0.00', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'GuidCliente', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 36, isPartOfKey: false, headerText: 'Guid Cliente', width: '250px', dataType: 'string', format: '', hidden: true, unbound: false, group: null, defaultValue: null },
            { key: 'IdEstado', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Id Estado', width: '271px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'IntCliente', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Int Cliente', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'SmallIntCliente', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 6, isPartOfKey: false, headerText: 'Small Int Cliente', width: '257px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'StringCliente', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'String Cliente', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null, defaultValue: '' },
            { key: 'StringEstado', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'String Estado', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'ChangeState', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: '', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null, defaultValue: 'N' }
        ];
        dataExportInfo['Venda'] = [ 
            { name: 'Venda', canExportMedia: true , canExportReport: true, actionExport: 'LinxDemoPaiFilha/GetVendaToExcel', actionReport: 'LinxDemoPaiFilha/GetVendaToReportXml', actionFeed: 'LinxDemoPaiFilhaOData/Venda', actionName: 'LinxDemoPaiFilha/GetVendaByEntitySearchNoAssociations', display: 'Venda',  metaData: function() { return metadataInfo['Venda']; } }
            , { name: 'VendaItem', canExportMedia: true , canExportReport: true, actionExport: 'LinxDemoPaiFilha/GetVendaItemParentCompositionToExcel', actionReport: 'LinxDemoPaiFilha/GetVendaItemParentCompositionToReportXml', actionFeed: 'LinxDemoPaiFilhaOData/VendaItemParentComposition', actionName: 'LinxDemoPaiFilha/GetVendaItemParentCompositionByEntitySearchNoAssociations', display: 'VendaItem',  metaData: function() { return metadataInfo['VendaItemParentComposition']; } }
        ];
        entitylookUps.push('Venda');
        entitylookUps['Venda'] = [];
        entitylookUps['Venda'].push('LookUpLoja');
        lookUpNames.push('LookUpLoja');
        metadataInfo['LookUpLoja'] = [
            { key: 'IdLoja', relatedKey: 'IdLoja', maxLength: 10, isPartOfKey: true, headerText: 'Id Loja', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null }
        ];
        entityNames.push('VendaAtacado');
        metadataInfo['VendaAtacado'] = [
            { key: 'BigIntVendaAtacado', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 24, isPartOfKey: false, headerText: 'Big Int Venda Atacado', width: '309px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'BitVendaAtacado', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Bit Venda Atacado', width: '257px', dataType: 'bool', format: 'checkbox', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'ComboboxVendaAtacado', isDomain: true, domainName: 'LX_VENDA_ATACADO', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 3, isPartOfKey: false, headerText: 'Combobox Venda Atacado', width: '322px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: 0 },
            { key: 'ComboboxVendaAtacadoName', isDomain: true, domainName: 'LX_VENDA_ATACADO', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Combobox Venda Atacado (Name)', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null, defaultValue: '' },
            { key: 'DatetimeVendaAtacado', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Datetime Venda Atacado', width: '322px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'DecimalVendaAtacado', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 13, isPartOfKey: false, headerText: 'Decimal Venda Atacado', width: '309px', dataType: 'number', format: '0.00', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'GuidVendaAtacado', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 36, isPartOfKey: false, headerText: 'Guid Venda Atacado', width: '270px', dataType: 'string', format: '', hidden: true, unbound: false, group: null, defaultValue: null },
            { key: 'IdCliente', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Id Cliente', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'IdVendaAtacado', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 12, isPartOfKey: true, headerText: 'Id Venda Atacado', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: 0 },
            { key: 'IntVendaAtacado', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Int Venda Atacado', width: '257px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'SmallIntVendaAtacado', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 6, isPartOfKey: false, headerText: 'Small Int Venda Atacado', width: '335px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'StringVendaAtacado', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'String Venda Atacado', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null, defaultValue: '' },
            { key: 'ChangeState', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: '', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null, defaultValue: 'N' }
        ];
        entityNames.push('VendaAtacadoParentComposition');
        metadataInfo['VendaAtacadoParentComposition'] = [
            { key: 'BigIntVendaAtacado', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 24, isPartOfKey: false, headerText: 'Big Int Venda Atacado', width: '309px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'BitVendaAtacado', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Bit Venda Atacado', width: '257px', dataType: 'bool', format: 'checkbox', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'ComboboxVendaAtacado', isDomain: true, domainName: 'LX_VENDA_ATACADO', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 3, isPartOfKey: false, headerText: 'Combobox Venda Atacado', width: '322px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: 0 },
            { key: 'ComboboxVendaAtacadoName', isDomain: true, domainName: 'LX_VENDA_ATACADO', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Combobox Venda Atacado (Name)', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null, defaultValue: '' },
            { key: 'DatetimeVendaAtacado', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Datetime Venda Atacado', width: '322px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'DecimalVendaAtacado', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 13, isPartOfKey: false, headerText: 'Decimal Venda Atacado', width: '309px', dataType: 'number', format: '0.00', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'GuidVendaAtacado', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 36, isPartOfKey: false, headerText: 'Guid Venda Atacado', width: '270px', dataType: 'string', format: '', hidden: true, unbound: false, group: null, defaultValue: null },
            { key: 'IdCliente', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Id Cliente', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'IdVendaAtacado', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 12, isPartOfKey: true, headerText: 'Id Venda Atacado', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: 0 },
            { key: 'IntVendaAtacado', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Int Venda Atacado', width: '257px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'SmallIntVendaAtacado', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 6, isPartOfKey: false, headerText: 'Small Int Venda Atacado', width: '335px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'StringVendaAtacado', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'String Venda Atacado', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null, defaultValue: '' },
            { key: 'BigIntCliente', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 24, isPartOfKey: false, headerText: 'Big Int Cliente', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'BitCliente', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Bit Cliente', width: '179px', dataType: 'bool', format: 'checkbox', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'ComboboxCliente', isDomain: true, domainName: 'LX_CLIENTE', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 3, isPartOfKey: false, headerText: 'Combobox Cliente', width: '244px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: 0 },
            { key: 'ComboboxClienteName', isDomain: true, domainName: 'LX_CLIENTE', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Combobox Cliente (Name)', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null, defaultValue: '' },
            { key: 'DatetimeCliente', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Datetime Cliente', width: '244px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'DecimalCliente', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 13, isPartOfKey: false, headerText: 'Decimal Cliente', width: '231px', dataType: 'number', format: '0.00', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'GuidCliente', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 36, isPartOfKey: false, headerText: 'Guid Cliente', width: '250px', dataType: 'string', format: '', hidden: true, unbound: false, group: null, defaultValue: null },
            { key: 'IdEstado', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Id Estado', width: '271px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'IntCliente', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Int Cliente', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'SmallIntCliente', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 6, isPartOfKey: false, headerText: 'Small Int Cliente', width: '257px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'StringCliente', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'String Cliente', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null, defaultValue: '' },
            { key: 'StringEstado', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'String Estado', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'ChangeState', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: '', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null, defaultValue: 'N' }
        ];
        dataExportInfo['VendaAtacado'] = [ 
            { name: 'VendaAtacado', canExportMedia: true , canExportReport: true, actionExport: 'LinxDemoPaiFilha/GetVendaAtacadoToExcel', actionReport: 'LinxDemoPaiFilha/GetVendaAtacadoToReportXml', actionFeed: 'LinxDemoPaiFilhaOData/VendaAtacado', actionName: 'LinxDemoPaiFilha/GetVendaAtacadoByEntitySearchNoAssociations', display: 'VendaAtacado',  metaData: function() { return metadataInfo['VendaAtacado']; } }
        ];
        entitylookUps.push('VendaAtacado');
        entitylookUps['VendaAtacado'] = [];
        entityNames.push('VendaItem');
        metadataInfo['VendaItem'] = [
            { key: 'BigIntVendaItem', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 24, isPartOfKey: false, headerText: 'Big Int Venda Item', width: '270px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'BitVendaItem', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Bit Venda Item', width: '218px', dataType: 'bool', format: 'checkbox', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'ComboboxVendaItem', isDomain: true, domainName: 'LX_VENDA_ITEM', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 3, isPartOfKey: false, headerText: 'Combobox Venda Item', width: '283px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: 0 },
            { key: 'ComboboxVendaItemName', isDomain: true, domainName: 'LX_VENDA_ITEM', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Combobox Venda Item (Name)', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null, defaultValue: '' },
            { key: 'DatetimeVendaItem', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Datetime Venda Item', width: '283px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'DecimalVendaItem', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 13, isPartOfKey: false, headerText: 'Decimal Venda Item', width: '270px', dataType: 'number', format: '0.00', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'GuidVendaItem', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 36, isPartOfKey: false, headerText: 'Guid Venda Item', width: '250px', dataType: 'string', format: '', hidden: true, unbound: false, group: null, defaultValue: null },
            { key: 'IdVenda', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Id Venda', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'IdVendaItem', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 12, isPartOfKey: true, headerText: 'Id Venda Item', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: 0 },
            { key: 'IntVendaItem', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Int Venda Item', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'SmallIntVendaItem', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 6, isPartOfKey: false, headerText: 'Small Int Venda Item', width: '296px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'StringVendaItem', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'String Venda Item', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null, defaultValue: '' },
            { key: 'ChangeState', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: '', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null, defaultValue: 'N' }
        ];
        entityNames.push('VendaItemParentComposition');
        metadataInfo['VendaItemParentComposition'] = [
            { key: 'BigIntVendaItem', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 24, isPartOfKey: false, headerText: 'Big Int Venda Item', width: '270px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'BitVendaItem', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Bit Venda Item', width: '218px', dataType: 'bool', format: 'checkbox', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'ComboboxVendaItem', isDomain: true, domainName: 'LX_VENDA_ITEM', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 3, isPartOfKey: false, headerText: 'Combobox Venda Item', width: '283px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: 0 },
            { key: 'ComboboxVendaItemName', isDomain: true, domainName: 'LX_VENDA_ITEM', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Combobox Venda Item (Name)', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null, defaultValue: '' },
            { key: 'DatetimeVendaItem', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Datetime Venda Item', width: '283px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'DecimalVendaItem', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 13, isPartOfKey: false, headerText: 'Decimal Venda Item', width: '270px', dataType: 'number', format: '0.00', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'GuidVendaItem', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 36, isPartOfKey: false, headerText: 'Guid Venda Item', width: '250px', dataType: 'string', format: '', hidden: true, unbound: false, group: null, defaultValue: null },
            { key: 'IdVenda', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Id Venda', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'IdVendaItem', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 12, isPartOfKey: true, headerText: 'Id Venda Item', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: 0 },
            { key: 'IntVendaItem', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Int Venda Item', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'SmallIntVendaItem', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 6, isPartOfKey: false, headerText: 'Small Int Venda Item', width: '296px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'StringVendaItem', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'String Venda Item', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null, defaultValue: '' },
            { key: 'BigIntVenda', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 24, isPartOfKey: false, headerText: 'Big Int Venda', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'BitVenda', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Bit Venda', width: '153px', dataType: 'bool', format: 'checkbox', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'ComboboxVenda', isDomain: true, domainName: 'LX_VENDA', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 3, isPartOfKey: false, headerText: 'Combobox Venda', width: '218px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: 0 },
            { key: 'ComboboxVendaName', isDomain: true, domainName: 'LX_VENDA', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Combobox Venda (Name)', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null, defaultValue: '' },
            { key: 'DatetimeVenda', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Datetime Venda', width: '218px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'DecimalVenda', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 13, isPartOfKey: false, headerText: 'Decimal Venda', width: '205px', dataType: 'number', format: '0.00', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'GuidVenda', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 36, isPartOfKey: false, headerText: 'Guid Venda', width: '250px', dataType: 'string', format: '', hidden: true, unbound: false, group: null, defaultValue: null },
            { key: 'IdCliente', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Id Cliente', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'IdLoja', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Id Loja', width: '271px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'IntVenda', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Int Venda', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'SmallIntVenda', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 6, isPartOfKey: false, headerText: 'Small Int Venda', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'StringVenda', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'String Venda', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null, defaultValue: '' },
            { key: 'BigIntCliente', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 24, isPartOfKey: false, headerText: 'Big Int Cliente', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'BitCliente', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Bit Cliente', width: '179px', dataType: 'bool', format: 'checkbox', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'ComboboxCliente', isDomain: true, domainName: 'LX_CLIENTE', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 3, isPartOfKey: false, headerText: 'Combobox Cliente', width: '244px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: 0 },
            { key: 'ComboboxClienteName', isDomain: true, domainName: 'LX_CLIENTE', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Combobox Cliente (Name)', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null, defaultValue: '' },
            { key: 'DatetimeCliente', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Datetime Cliente', width: '244px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'DecimalCliente', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 13, isPartOfKey: false, headerText: 'Decimal Cliente', width: '231px', dataType: 'number', format: '0.00', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'GuidCliente', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 36, isPartOfKey: false, headerText: 'Guid Cliente', width: '250px', dataType: 'string', format: '', hidden: true, unbound: false, group: null, defaultValue: null },
            { key: 'IdEstado', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Id Estado', width: '271px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'IntCliente', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 12, isPartOfKey: false, headerText: 'Int Cliente', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'SmallIntCliente', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 6, isPartOfKey: false, headerText: 'Small Int Cliente', width: '257px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'StringCliente', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'String Cliente', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null, defaultValue: '' },
            { key: 'StringEstado', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'String Estado', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'ChangeState', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: '', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null, defaultValue: 'N' }
        ];
        dataExportInfo['VendaItem'] = [ 
            { name: 'VendaItem', canExportMedia: true , canExportReport: true, actionExport: 'LinxDemoPaiFilha/GetVendaItemToExcel', actionReport: 'LinxDemoPaiFilha/GetVendaItemToReportXml', actionFeed: 'LinxDemoPaiFilhaOData/VendaItem', actionName: 'LinxDemoPaiFilha/GetVendaItemByEntitySearchNoAssociations', display: 'VendaItem',  metaData: function() { return metadataInfo['VendaItem']; } }
        ];
        entitylookUps.push('VendaItem');
        entitylookUps['VendaItem'] = [];
        entityNames.push('Loja');
        metadataInfo['Loja'] = [
            { key: 'BigIntLoja', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 19, isPartOfKey: false, headerText: 'Big Int Loja', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'BitLoja', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Bit Loja', width: '140px', dataType: 'bool', format: 'checkbox', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'ComboboxLoja', isDomain: true, domainName: 'LX_LOJA', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 3, isPartOfKey: false, headerText: 'Combobox Loja', width: '205px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: 0 },
            { key: 'ComboboxLojaName', isDomain: true, domainName: 'LX_LOJA', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Combobox Loja (Name)', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null, defaultValue: '' },
            { key: 'DatetimeLoja', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Datetime Loja', width: '205px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'DecimalLoja', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 13, isPartOfKey: false, headerText: 'Decimal Loja', width: '192px', dataType: 'number', format: '0.00', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'GuidLoja', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 36, isPartOfKey: false, headerText: 'Guid Loja', width: '250px', dataType: 'string', format: '', hidden: true, unbound: false, group: null, defaultValue: null },
            { key: 'IdLoja', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 10, isPartOfKey: true, headerText: 'Id Loja', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: 0 },
            { key: 'IntLoja', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Int Loja', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'SmallIntLoja', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 5, isPartOfKey: false, headerText: 'Small Int Loja', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'StringLoja', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'String Loja', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null, defaultValue: '' },
            { key: 'ChangeState', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: '', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null, defaultValue: 'N' }
        ];
        dataExportInfo['Loja'] = [ 
            { name: 'Loja', canExportMedia: true , canExportReport: true, actionExport: 'LinxDemoPaiFilha/GetLojaToExcel', actionReport: 'LinxDemoPaiFilha/GetLojaToReportXml', actionFeed: 'LinxDemoPaiFilhaOData/Loja', actionName: 'LinxDemoPaiFilha/GetLojaByEntitySearchNoAssociations', display: 'Loja',  metaData: function() { return metadataInfo['Loja']; } }
            , { name: 'Vendedor', canExportMedia: true , canExportReport: true, actionExport: 'LinxDemoPaiFilha/GetVendedorParentCompositionToExcel', actionReport: 'LinxDemoPaiFilha/GetVendedorParentCompositionToReportXml', actionFeed: 'LinxDemoPaiFilhaOData/VendedorParentComposition', actionName: 'LinxDemoPaiFilha/GetVendedorParentCompositionByEntitySearchNoAssociations', display: 'Vendedor',  metaData: function() { return metadataInfo['VendedorParentComposition']; } }
        ];
        entitylookUps.push('Loja');
        entitylookUps['Loja'] = [];
        entityNames.push('Vendedor');
        metadataInfo['Vendedor'] = [
            { key: 'BitVendedor', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Bit Vendedor', width: '192px', dataType: 'bool', format: 'checkbox', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'ComboboxVendedor', isDomain: true, domainName: 'LX_VENDEDOR', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 3, isPartOfKey: false, headerText: 'Combobox Vendedor', width: '257px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: 0 },
            { key: 'ComboboxVendedorName', isDomain: true, domainName: 'LX_VENDEDOR', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Combobox Vendedor (Name)', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null, defaultValue: '' },
            { key: 'DatetimeVendedor', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Datetime Vendedor', width: '257px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'DecimalVendedor', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 13, isPartOfKey: false, headerText: 'Decimal Vendedor', width: '244px', dataType: 'number', format: '0.00', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'GuidVendedor', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 36, isPartOfKey: false, headerText: 'Guid Vendedor', width: '250px', dataType: 'string', format: '', hidden: true, unbound: false, group: null, defaultValue: null },
            { key: 'IdLoja', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Id Loja', width: '271px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'IdVendedor', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 10, isPartOfKey: true, headerText: 'Id Vendedor', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: 0 },
            { key: 'IntVendedor', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Int Vendedor', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'SmallIntVendedor', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 5, isPartOfKey: false, headerText: 'Small Int Vendedor', width: '270px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'StringVendedor', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'String Vendedor', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null, defaultValue: '' },
            { key: 'ChangeState', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: '', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null, defaultValue: 'N' }
        ];
        entityNames.push('VendedorParentComposition');
        metadataInfo['VendedorParentComposition'] = [
            { key: 'BitVendedor', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Bit Vendedor', width: '192px', dataType: 'bool', format: 'checkbox', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'ComboboxVendedor', isDomain: true, domainName: 'LX_VENDEDOR', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 3, isPartOfKey: false, headerText: 'Combobox Vendedor', width: '257px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: 0 },
            { key: 'ComboboxVendedorName', isDomain: true, domainName: 'LX_VENDEDOR', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Combobox Vendedor (Name)', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null, defaultValue: '' },
            { key: 'DatetimeVendedor', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Datetime Vendedor', width: '257px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'DecimalVendedor', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 13, isPartOfKey: false, headerText: 'Decimal Vendedor', width: '244px', dataType: 'number', format: '0.00', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'GuidVendedor', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 36, isPartOfKey: false, headerText: 'Guid Vendedor', width: '250px', dataType: 'string', format: '', hidden: true, unbound: false, group: null, defaultValue: null },
            { key: 'IdLoja', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Id Loja', width: '271px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'IdVendedor', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 10, isPartOfKey: true, headerText: 'Id Vendedor', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: 0 },
            { key: 'IntVendedor', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Int Vendedor', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'SmallIntVendedor', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 5, isPartOfKey: false, headerText: 'Small Int Vendedor', width: '270px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'StringVendedor', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'String Vendedor', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null, defaultValue: '' },
            { key: 'BigIntLoja', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 19, isPartOfKey: false, headerText: 'Big Int Loja', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'BitLoja', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Bit Loja', width: '140px', dataType: 'bool', format: 'checkbox', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'ComboboxLoja', isDomain: true, domainName: 'LX_LOJA', lookupPropertyName: '', lookupVisibleColumns: '', isRequired: true, maxLength: 3, isPartOfKey: false, headerText: 'Combobox Loja', width: '205px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: 0 },
            { key: 'ComboboxLojaName', isDomain: true, domainName: 'LX_LOJA', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: 'Combobox Loja (Name)', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null, defaultValue: '' },
            { key: 'DatetimeLoja', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Datetime Loja', width: '205px', dataType: 'date', format: 'dd/MM/yyyy', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'DecimalLoja', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 13, isPartOfKey: false, headerText: 'Decimal Loja', width: '192px', dataType: 'number', format: '0.00', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'GuidLoja', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 36, isPartOfKey: false, headerText: 'Guid Loja', width: '250px', dataType: 'string', format: '', hidden: true, unbound: false, group: null, defaultValue: null },
            { key: 'IntLoja', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 10, isPartOfKey: false, headerText: 'Int Loja', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'SmallIntLoja', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 5, isPartOfKey: false, headerText: 'Small Int Loja', width: '250px', dataType: 'number', format: 'int', hidden: false, unbound: false, group: null, defaultValue: null },
            { key: 'StringLoja', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 50, validateMaxLength: true, isPartOfKey: false, headerText: 'String Loja', width: '400px', dataType: 'string', format: '', hidden: false, unbound: false, group: null, defaultValue: '' },
            { key: 'ChangeState', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: '', width: '0px', dataType: 'string', format: '', hidden: true, unbound: false, group: null, defaultValue: 'N' }
        ];
        dataExportInfo['Vendedor'] = [ 
            { name: 'Vendedor', canExportMedia: true , canExportReport: true, actionExport: 'LinxDemoPaiFilha/GetVendedorToExcel', actionReport: 'LinxDemoPaiFilha/GetVendedorToReportXml', actionFeed: 'LinxDemoPaiFilhaOData/Vendedor', actionName: 'LinxDemoPaiFilha/GetVendedorByEntitySearchNoAssociations', display: 'Vendedor',  metaData: function() { return metadataInfo['Vendedor']; } }
        ];
        entitylookUps.push('Vendedor');
        entitylookUps['Vendedor'] = [];
        var lookUpProperties = [];
        //#endregion Metadata Info
        //#region dataParameters
        var dataParameters = {
            isLoaded: false,
            parameters: [],
            registerParameters: function (parameterList, complete) {
                if (parameterList !== '') {
                    var variation = '{TBC_GRUPO_ECONOMICO|' + shellManagerService.getEconomicGroupId().toString() + '|TCS_USUARIO|' + shellManagerService.getUserUid() + (dataBusiness != null && dataBusiness.getBandeiraRede() > 0 ? '|TBC_BANDEIRA_REDE|' + dataBusiness.getBandeiraRede().toString() : '') + '}';
                    var error = function (gEr) {
                        var msg = 'Os seguintes parâmetros não foram pesquisados: [' + parameterList + ']';
                        dialog.showAlert(msg, 'Alerta');
                        dataParameters.isLoaded = true;
                    };
                    var success = function (data) {
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
                    };
                    httpFactory.httpGet(getDataServiceUrl, getServiceAddress('LinxFrameworkParametro') + '/GetParameterValue?serializedParameterList=' + common.stringReplace(parameterList, '{}', variation), success, error);
                }
            }
        };
        //#endregion dataParameters
        //#region Classes Map
        var sequences = [];
        var resetSequence = function(entityName) {
            sequences[entityName] = 0;
        };
        var getSequence = function(entityName) {
            if ((typeof sequences[entityName]) === 'undefined') resetSequence(entityName);
            return (++sequences[entityName]);
        };
        //#region Classes Map
        var sequences = [];
        var getNextSequence = function(entityName) {
            if (!sequences[entityName]) resetSequence(entityName);
            var sequence = sequences[entityName];
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
        lookUpProperties['Cliente'] = {IdEstado: 'LookUpEstado', StringEstado: 'LookUpEstado'};
        var ClienteInitializer = function (ownerReference) {
           ownerReference.RowDataId = getNextSequence('Cliente');
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
            var _datetimeCliente = (ownerReference.DatetimeCliente === null ? null : new Date(ownerReference.DatetimeCliente));
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
            //End Property Definitions
           ownerReference.currentVenda = null;
           ownerReference.currentVendaAtacado = null;
           ownerReference.setRemovedLookupFields = function(removedFields) {
               for (var idxLUp in entitylookUps[ownerReference.typeName]) {
                   var hasKeyValue = false;
                   var luName = entitylookUps[ownerReference.typeName][idxLUp];
                   var luMeta = metadataInfo[luName];
                   for (var idxProp in luMeta) {
                       var prop = luMeta[idxProp];
                       if (!common.isNullOrEmpty(prop.relatedKey) && prop.isPartOfKey) {
                           hasKeyValue = !common.isNullOrEmpty(ownerReference[prop.relatedKey]);
                           break;
                       }
                   }
                   if (hasKeyValue) {
                       for (var idxProp in luMeta) {
                           var prop = luMeta[idxProp];
                           if (!common.isNullOrEmpty(prop.relatedKey) && !prop.isPartOfKey) {
                               removedFields.push(prop.relatedKey);
                           }
                       }
                   }
               }
           }
           ownerReference.getJExpression = function(listFilterRange, removedFields, noDetails) {
               if (ownerReference.excludedFilters && ownerReference.excludedFilters.length > 0) { if (removedFields instanceof Array) removedFields = removedFields.concat(ownerReference.excludedFilters); else removedFields = ownerReference.excludedFilters; }
               ownerReference.setRemovedLookupFields(removedFields);
               var jExpression = common.getJEntityExpression(ownerReference, dialog, listFilterRange, removedFields);
               if (jExpression === 'Error') return jExpression;
               if (noDetails !== true && ownerReference.VendaList && ownerReference.VendaList.length > 0) {
                 var detailExpr = ownerReference.VendaList[0].getJExpression(listFilterRange, ['IdCliente']);
                 if (detailExpr === 'Error') return detailExpr;
                 jExpression += detailExpr;
               }
               if (noDetails !== true && ownerReference.VendaAtacadoList && ownerReference.VendaAtacadoList.length > 0) {
                 var detailExpr = ownerReference.VendaAtacadoList[0].getJExpression(listFilterRange, ['IdCliente']);
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
               if (!common.isNullOrEmpty(ownerReference.original)) {
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
           ownerReference.getValidationErrors = function(propertyName) {
               var errors = [];
               if (!dataBusiness.canReportErrors) return errors;
               if (!ownerReference.ChangeState || ['I', 'U'].indexOf(ownerReference.ChangeState) < 0) return errors;
               var properties = metadataInfo[ownerReference.typeName];
               for (var i = 0; i < properties.length; i++) {
                   var prop = properties[i];
                   if (common.isNullOrEmpty(propertyName) || prop.key == propertyName) {
                       if (prop.isRequired === true && !prop.isPartOfKey && common.isNullOrEmpty(ownerReference[prop.key])) errors.push('O campo [' + prop.headerText + (managerAuth.shellMode=='DEV' ? ' (' + ownerReference.typeName + '.' + prop.key + ')' : '') + '] é requerido.');
                       if (prop.validateMaxLength === true && prop.maxLength > 0 && !common.isNullOrEmpty(ownerReference[prop.key]) && ownerReference[prop.key].length > prop.maxLength) errors.push('O campo [' + prop.headerText + (managerAuth.shellMode=='DEV' ? ' (' + ownerReference.typeName + '.' + prop.key + ')' : '') + '] permite no máximo ' + prop.maxLength.toString() + ' caractere(s).');
                   }
               }
               if (common.isNullOrEmpty(propertyName)) {
                   for (var i = 0; i < ownerReference.VendaList().length; i++) {
                       var detail = ownerReference.VendaList()[i];
                       errors = errors.concat(detail.getValidationErrors());
                   }
               }
               if (common.isNullOrEmpty(propertyName)) {
                   for (var i = 0; i < ownerReference.VendaAtacadoList().length; i++) {
                       var detail = ownerReference.VendaAtacadoList()[i];
                       errors = errors.concat(detail.getValidationErrors());
                   }
               }
               return errors;
           }
           ownerReference.getPrimitiveDTO = function(loadDetails) {
               var command = '';
               var properties = metadataInfo[ownerReference.typeName];
               for (var i = 0; i < properties.length; i++) {
                   command += (command === '' ? '' : ', ') + properties[i].key + ': ownerReference.' + properties[i].key;
                   if (properties[i].isDomain && properties[i].key.length > 4) command += (command === '' ? '' : ', ') + common.strLeft(properties[i].key, properties[i].key.length - 4) + ': ownerReference.' + common.strLeft(properties[i].key, properties[i].key.length - 4);
               }
               var result = {};
               eval('result = { ' + command + ' };');
               if (loadDetails) {
                   result.VendaList = [];
                   var sourceList = ownerReference.VendaList;
                   if (sourceList && sourceList.length > 0) {
                       for (var i = 0; i < sourceList.length; i++) {
                           if (['U', 'I', 'D'].indexOf(sourceList[i].ChangeState) >= 0) result.VendaList.push(sourceList[i].getPrimitiveDTO(sourceList[i].ChangeState != 'D'));
                       }
                   }
                   result.VendaAtacadoList = [];
                   var sourceList = ownerReference.VendaAtacadoList;
                   if (sourceList && sourceList.length > 0) {
                       for (var i = 0; i < sourceList.length; i++) {
                           if (['U', 'I', 'D'].indexOf(sourceList[i].ChangeState) >= 0) result.VendaAtacadoList.push(sourceList[i].getPrimitiveDTO(sourceList[i].ChangeState != 'D'));
                       }
                   }
               }
               return result;
           };
           ownerReference.getAllDetailChanges = function() {
               var result = [];
               var _VendaList = ownerReference.VendaList;
               if (_VendaList && _VendaList.length > 0) {
                   for (var i = 0; i < _VendaList.length; i++) {
                       var detail = _VendaList[i];
                       if (['U', 'I', 'D'].indexOf(detail.ChangeState) >= 0) {
                           result.push(detail);
                           result = result.concat(detail.getAllDetailChanges());
                       }
                   }
               }
               var _VendaAtacadoList = ownerReference.VendaAtacadoList;
               if (_VendaAtacadoList && _VendaAtacadoList.length > 0) {
                   for (var i = 0; i < _VendaAtacadoList.length; i++) {
                       var detail = _VendaAtacadoList[i];
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
                    ownerReference[properties[i].key] = originData[properties[i].key];
               }
               if (copyDetails) {
                   if (ownerReference.VendaList && originData.VendaList) {
                       var toList = ownerReference.VendaList;
                       var fromList = originData.VendaList;
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
                   if (ownerReference.VendaAtacadoList && originData.VendaAtacadoList) {
                       var toList = ownerReference.VendaAtacadoList;
                       var fromList = originData.VendaAtacadoList;
                       for (var idxElem = toList.length - 1; idxElem >= 0; idxElem--) {
                          if (toList[idxElem].ChangeState === 'D') toList.splice(idxElem, 1);
                       }
                       for (var idxElem = toList.length - 1; idxElem >= 0; idxElem--) {
                              if (toList[idxElem].ChangeState !== 'N') {
                                   var fromObj = _.where(fromList, { IdVendaAtacado: toList[idxElem]['IdVendaAtacado'] });
                                   if (fromObj.length > 0) toList[idxElem].copyDataFrom(fromObj[0], true);
                              }
                       }
                   }
               }
               enableChangeTrack = true;
           };
              ownerReference.refreshData = function(noWait, succeeded) {
                 var filterByKey = 'Cliente{' + 'IdCliente#==#I' + ownerReference.IdCliente.toString() + '}';
                 return dataContext.getClienteByEntitySearchNoAssociations(filterByKey, 0, 0, false, '', querySucceeded);
                 function querySucceeded(data) {
                    if (data.results.length > 0) {  for (var idx = 0; idx < data.results.length; idx++) { ownerReference.copyDataFrom(data.results[idx]); } }
                    if (succeeded) { succeeded(data); }
                    if (data.results.length == 0) { return; }
                    if (!noWait || ownerReference.atLeastOneDetailLoaded()) { ownerReference.fillDetails(true, '', noWait); }
               }
              }
           ownerReference.isAdded = function() { return ownerReference.ChangeState === 'I'; };
           ownerReference.isDeleted = function() { return ownerReference.ChangeState === 'D'; };
           ownerReference.isModified = function() { return ownerReference.ChangeState === 'U'; };
           ownerReference.isDetached = function() { return false; };
           ownerReference.isUnchanged = function() { return ownerReference.ChangeState === 'N'; };
           ownerReference.setModified = function() { ownerReference.ChangeState = 'U'; };
           ownerReference.setUnchanged = function() { ownerReference.ChangeState = 'N'; };
           ownerReference.serverDataType = [];
           ownerReference.serverDataType['BigIntCliente'] = 'L';
           ownerReference.serverDataType['BitCliente'] = 'B';
           ownerReference.serverDataType['ComboboxCliente'] = 'Y';
           ownerReference.serverDataType['DatetimeCliente'] = 'T';
           ownerReference.serverDataType['DecimalCliente'] = 'D';
           ownerReference.serverDataType['GuidCliente'] = 'G';
           ownerReference.serverDataType['IdCliente'] = 'I';
           ownerReference.serverDataType['IdEstado'] = 'I';
           ownerReference.serverDataType['IntCliente'] = 'I';
           ownerReference.serverDataType['SmallIntCliente'] = 'H';
           ownerReference.serverDataType['StringCliente'] = 'S';
           ownerReference.serverDataType['StringEstado'] = 'S';
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
              return (property != null && !common.isNullOrEmpty(property.lookupPropertyName) ? property.lookupPropertyName : propertyName);
           }
           ownerReference.getLookupVisibleColumns = function(propertyName) {
              var property = getEntityProperty(ownerReference.typeName, propertyName);
              return (property != null ? property.lookupVisibleColumns : '');
           }
           ownerReference.getLookupDisplay = function (lookupName) {
               var displayName = '';
               if (lookupName === 'LookUpEstado') {
                   displayName = ' de Estado';
               }
               return 'Seleção' + displayName;
           };
        
           ownerReference.getSubQueryFilterFromLookUpEstado = function (propertyName) {
               var filter = '';
               return filter;
           }
           ownerReference.canGetClientFilter = function (lookupName) {
               return true;
           }
        
           ownerReference.hasValidClientFilter = function (lookupName, lookupInfo) {
               var checkClientFilter = '';
               if (typeof ownerReference['BeforeGet' + lookupName + 'Query'] == 'function') {
                   checkClientFilter = ownerReference['BeforeGet' + lookupName + 'Query']();
                   if (checkClientFilter === 'Error') { return false; }
               }
               return true;
           }
        
        //#region LookUps Finalizers
           ownerReference.executeLookUpEstado = function (lookupProperty, entityProperty, pageSkip, pageSize, queryCallback) {
               if (!lookupProperty) { if (queryCallback) queryCallback(true, [], 0); return null; }
               if (common.isNullOrEmpty(entityProperty)) entityProperty = lookupProperty;
               var valueToSearch = ownerReference[entityProperty];
               var extraFilters = '';
               if (ownerReference.canGetClientFilter('LookUpEstado')) {
                   if (typeof ownerReference['BeforeGetLookUpEstadoQuery'] == 'function') {
                       var customFilter = ownerReference['BeforeGetLookUpEstadoQuery']();
                       if (customFilter === 'Error') { if (queryCallback) queryCallback(true, [], 0); return null; }
                       if (!common.isNullOrEmpty(customFilter)) { extraFilters = (common.isNullOrEmpty(extraFilters) ? '' : extraFilters + ';') + customFilter; }
                   }
                   if (typeof ownerReference['getSubQueryFilterFromLookUpEstado'] == 'function') {
                       var customFilter = ownerReference['getSubQueryFilterFromLookUpEstado'](lookupProperty);
                       if (customFilter === 'Error') { if (queryCallback) queryCallback(true, [], 0); return null; }
                       if (!common.isNullOrEmpty(customFilter)) { extraFilters = (common.isNullOrEmpty(extraFilters) ? '' : extraFilters + ';') + customFilter; }
                   }
               }
               var completeExpression = common.getLookUpJEntityExpression('LookUpEstado', ownerReference, lookupProperty, valueToSearch, extraFilters, entityProperty, dialog);
               if (completeExpression === 'Error') { if (queryCallback) queryCallback(true, [], 0); return null; }
               var callbackSucceeded = function (data) { if (queryCallback) queryCallback(false, data.results, data.inlineCount); };
               var callbackFailed = function (error) { if (queryCallback) queryCallback(true, [], 0); queryFailed(error); };
               return dataContext.getLookUpEstadoByEntitySearch(completeExpression, lookupProperty, pageSkip, pageSize, 'asc', callbackSucceeded, function() { }, callbackFailed);
           };
        
           ownerReference.finalizeLookUpEstado = function (lookupProperty, entityProperty, selectedElements) {
               if (!selectedElements)
                   return;
               if ((typeof selectedElements.length) === 'undefined') {
                   selectedElements = [selectedElements];
               }
        
               //Mount query list for QBE
               if (dataBusiness && dataBusiness.status() == 'C' && selectedElements != null && selectedElements.length > 1) {
                   var results = '';
                   for (var index = 0; index < selectedElements.length; index++) {
                       results += (index == 0 ? '' : ',') + selectedElements[index][lookupProperty].toString().trim();
                   }
                   results = '[' + results + ']';
                   ownerReference[entityProperty] = results;
                   dataBusiness.entitySearchRange[ownerReference.typeName + entityProperty](results);
                   return;
               }
        
               var replaceTo = ownerReference;
               for (var i = 0; i < selectedElements.length; i++)
               {
                   var selectedElement = selectedElements[i];
                   if (selectedElement.hasOwnProperty('IdEstado') && (replaceTo.hasOwnProperty('IdEstado') || replaceTo.__proto__.hasOwnProperty('IdEstado')))
                   {
                       replaceTo.IdEstado = selectedElement.IdEstado;
                   }
                   else if (replaceTo.hasOwnProperty('IdEstado') || replaceTo.__proto__.hasOwnProperty('IdEstado')) {
                       replaceTo.IdEstado = null;
                   }
                   if (selectedElement.hasOwnProperty('StringEstado') && (replaceTo.hasOwnProperty('StringEstado') || replaceTo.__proto__.hasOwnProperty('StringEstado')))
                   {
                       replaceTo.StringEstado = selectedElement.StringEstado;
                   }
                   else if (replaceTo.hasOwnProperty('StringEstado') || replaceTo.__proto__.hasOwnProperty('StringEstado')) {
                       replaceTo.StringEstado = null;
                   }
               }
           };
        
           ownerReference.clearLookUpEstado = function () {
               ownerReference.IdEstado = null;
               ownerReference.StringEstado = null;
           }
           ownerReference.executeLookUpLoja = function (lookupProperty, entityProperty, pageSkip, pageSize, queryCallback) {
               if (!lookupProperty) { if (queryCallback) queryCallback(true, [], 0); return null; }
               if (common.isNullOrEmpty(entityProperty)) entityProperty = lookupProperty;
               var valueToSearch = ownerReference[entityProperty];
               var extraFilters = '';
               if (ownerReference.canGetClientFilter('LookUpLoja')) {
                   if (typeof ownerReference['BeforeGetLookUpLojaQuery'] == 'function') {
                       var customFilter = ownerReference['BeforeGetLookUpLojaQuery']();
                       if (customFilter === 'Error') { if (queryCallback) queryCallback(true, [], 0); return null; }
                       if (!common.isNullOrEmpty(customFilter)) { extraFilters = (common.isNullOrEmpty(extraFilters) ? '' : extraFilters + ';') + customFilter; }
                   }
                   if (typeof ownerReference['getSubQueryFilterFromLookUpLoja'] == 'function') {
                       var customFilter = ownerReference['getSubQueryFilterFromLookUpLoja'](lookupProperty);
                       if (customFilter === 'Error') { if (queryCallback) queryCallback(true, [], 0); return null; }
                       if (!common.isNullOrEmpty(customFilter)) { extraFilters = (common.isNullOrEmpty(extraFilters) ? '' : extraFilters + ';') + customFilter; }
                   }
               }
               var completeExpression = common.getLookUpJEntityExpression('LookUpLoja', ownerReference, lookupProperty, valueToSearch, extraFilters, entityProperty, dialog);
               if (completeExpression === 'Error') { if (queryCallback) queryCallback(true, [], 0); return null; }
               var callbackSucceeded = function (data) { if (queryCallback) queryCallback(false, data.results, data.inlineCount); };
               var callbackFailed = function (error) { if (queryCallback) queryCallback(true, [], 0); queryFailed(error); };
               return dataContext.getLookUpLojaByEntitySearch(completeExpression, lookupProperty, pageSkip, pageSize, 'asc', callbackSucceeded, function() { }, callbackFailed);
           };
        
           ownerReference.finalizeLookUpLoja = function (lookupProperty, entityProperty, selectedElements) {
               if (!selectedElements)
                   return;
               if ((typeof selectedElements.length) === 'undefined') {
                   selectedElements = [selectedElements];
               }
        
               //Mount query list for QBE
               if (dataBusiness && dataBusiness.status() == 'C' && selectedElements != null && selectedElements.length > 1) {
                   var results = '';
                   for (var index = 0; index < selectedElements.length; index++) {
                       results += (index == 0 ? '' : ',') + selectedElements[index][lookupProperty].toString().trim();
                   }
                   results = '[' + results + ']';
                   ownerReference[entityProperty] = results;
                   dataBusiness.entitySearchRange[ownerReference.typeName + entityProperty](results);
                   return;
               }
        
               var replaceTo = ownerReference;
               for (var i = 0; i < selectedElements.length; i++)
               {
                   var selectedElement = selectedElements[i];
                   if (selectedElement.hasOwnProperty('IdLoja') && (replaceTo.hasOwnProperty('IdLoja') || replaceTo.__proto__.hasOwnProperty('IdLoja')))
                   {
                       replaceTo.IdLoja = selectedElement.IdLoja;
                   }
                   else if (replaceTo.hasOwnProperty('IdLoja') || replaceTo.__proto__.hasOwnProperty('IdLoja')) {
                       replaceTo.IdLoja = null;
                   }
               }
           };
        
           ownerReference.clearLookUpLoja = function () {
               ownerReference.IdLoja = null;
           }
        //#endregion
           //#endregion Lookup Extended Methods
           ownerReference.setDefaults = function () {
           };
           ownerReference.delete = function() {
               if (ownerReference.setParentAsModified) ownerReference.setParentAsModified();
               if (!common.isNullOrEmpty(ownerReference.VendaList) && ownerReference.VendaList.length > 0) {
                  var details = [].concat(ownerReference.VendaList);
                  for (var idx = 0; idx < details.length; idx++) {
                    details[idx].delete();
                  }
               }
               if (!common.isNullOrEmpty(ownerReference.VendaAtacadoList) && ownerReference.VendaAtacadoList.length > 0) {
                  var details = [].concat(ownerReference.VendaAtacadoList);
                  for (var idx = 0; idx < details.length; idx++) {
                    details[idx].delete();
                  }
               }
               if (ownerReference.ChangeState == 'I') {
                   if (parent && parent.ClienteList) { 
                       var idx = parent.ClienteList.indexOf(ownerReference); 
                       if (idx >= 0) parent.ClienteList.splice(idx, 1); 
                   }
                   else {
                       var idx = dataBusiness.dataView.indexOf(ownerReference);
                       if (idx >= 0) dataBusiness.dataView.splice(idx, 1);
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
               return dataBusiness.dataView;
           };
           ownerReference.Namespace = 'Linx.Demo.BV.PaiFilha';
           ownerReference.myProperties = [ 'BigIntCliente', 'BitCliente', 'ComboboxCliente', 'DatetimeCliente', 'DecimalCliente', 'GuidCliente', 'IdCliente', 'IdEstado', 'IntCliente', 'SmallIntCliente', 'StringCliente', 'StringEstado' ];
           ownerReference.queryRequiredProperties = {  };
           ownerReference.excludedFilters = [];
           ownerReference.getCurrentElements = function() {
               var result = [ ownerReference ];
           if (!common.isNullOrEmpty(ownerReference.currentVenda)) { result = result.concat(ownerReference.currentVenda.getCurrentElements()); }
           if (!common.isNullOrEmpty(ownerReference.currentVendaAtacado)) { result = result.concat(ownerReference.currentVendaAtacado.getCurrentElements()); }
               return result;
           };
           ownerReference.checkForSendingAllRowsToServer = function() {
           };
           ownerReference.GetJsWhereDetailRelationForVenda = function(customParentRelation) {
       return 'Venda{' + (!common.isNullOrEmpty(customParentRelation) ? customParentRelation : 'IdCliente#==#' + ownerReference.serverDataType['IdCliente'] + common.getAbsoluteValue(ownerReference.IdCliente).toString()) + '}';        
           }
           ownerReference.GetJsWhereDetailRelationForVendaAtacado = function(customParentRelation) {
       return 'VendaAtacado{' + (!common.isNullOrEmpty(customParentRelation) ? customParentRelation : 'IdCliente#==#' + ownerReference.serverDataType['IdCliente'] + common.getAbsoluteValue(ownerReference.IdCliente).toString()) + '}';        
           }
           ownerReference.VendaIsLoaded = false;
           ownerReference.VendaAtacadoIsLoaded = false;
           ownerReference.detailsLoaded = function() {
               return ownerReference.VendaIsLoaded && ownerReference.VendaAtacadoIsLoaded;
           }
           ownerReference.atLeastOneDetailLoaded = function() {
               return ownerReference.VendaIsLoaded || ownerReference.VendaAtacadoIsLoaded;
           }
           ownerReference.adjustDetailsLoaded = function(value) {
               ownerReference.VendaIsLoaded = value;
               if (value === false)
                   ownerReference.VendaList([]);
               ownerReference.VendaAtacadoIsLoaded = value;
               if (value === false)
                   ownerReference.VendaAtacadoList([]);
           }
           ownerReference.fillDetails = function(force, detailName, noWait, callback, customParentRelation) {
              if (typeof force === 'undefined') force = false;
              if (ownerReference.isAdded()) {
                ownerReference.VendaIsLoaded = true;
                ownerReference.VendaAtacadoIsLoaded = true;
              }
              var _VendaRemoteComplete = false;
              var _VendaAtacadoRemoteComplete = false;
              var detachList_Venda = [];
              if (force) {
                   if (common.isNullOrEmpty(detailName) || detailName == 'Venda') ownerReference.VendaIsLoaded = false;
                   if ((common.isNullOrEmpty(detailName) || detailName == 'Venda') && ownerReference.VendaList && ownerReference.VendaList.length > 0) {
                         ownerReference.VendaList = [];
                   }
              }
        
              if (!ownerReference.VendaIsLoaded) {
                //Load VendaList
                if (common.isNullOrEmpty(detailName) || detailName === 'Venda') {
                  ownerReference.VendaIsLoaded = true;
                  _VendaRemoteComplete = (ownerReference.VendaList && ownerReference.VendaList.length > 0);
                  if ((force || !ownerReference.VendaList || ownerReference.VendaList.length === 0) && (!common.isNullOrEmpty(common.getAbsoluteValue(ownerReference.IdCliente)))) {
                    var navQuery = 'GetVendaByEntitySearchNoAssociations?$inlinecount=none';
                    navQuery += '&$orderby=IdVenda asc';
                    navQuery += '&jEntitySearch=' + ownerReference.GetJsWhereDetailRelationForVenda(customParentRelation);        ;
                    dataBusiness.showProcessing();
                    httpFactory.httpGet(getDataServiceUrl, navQuery,
                        function (data) {
                           for (var idx = 0; idx < data.results.length; idx++) {
                               initializePOCO(data.results[idx], 'Venda'); 
                               data.results[idx].Cliente = ownerReference; 
                           } 
                           ownerReference.VendaList = data.results; 
                           ownerReference.setCurrentDetails('Venda');
                           dataBusiness.closeProcessing();
                           _VendaRemoteComplete = true;
                           if (callback && (!common.isNullOrEmpty(detailName) || (_VendaRemoteComplete && _VendaAtacadoRemoteComplete))) { callback(); }
                        }, 
                        function (error) {
                            dataBusiness.closeProcessing();
                            queryFailed(error);
                        });
                  } else { ownerReference.setCurrentDetails('Venda'); }
                } else { if (!ownerReference.VendaIsLoaded && ownerReference.VendaList && ownerReference.VendaList.length > 0) { ownerReference.VendaIsLoaded = true; } }
              } else { 
                if (common.isNullOrEmpty(detailName) || detailName == 'Venda') {
                   ownerReference.setCurrentDetails('Venda');
                }
                _VendaRemoteComplete = true;
              }
              var detachList_VendaAtacado = [];
              if (force) {
                   if (common.isNullOrEmpty(detailName) || detailName == 'VendaAtacado') ownerReference.VendaAtacadoIsLoaded = false;
                   if ((common.isNullOrEmpty(detailName) || detailName == 'VendaAtacado') && ownerReference.VendaAtacadoList && ownerReference.VendaAtacadoList.length > 0) {
                         ownerReference.VendaAtacadoList = [];
                   }
              }
        
              if (!ownerReference.VendaAtacadoIsLoaded) {
                //Load VendaAtacadoList
                if (common.isNullOrEmpty(detailName) || detailName === 'VendaAtacado') {
                  ownerReference.VendaAtacadoIsLoaded = true;
                  _VendaAtacadoRemoteComplete = (ownerReference.VendaAtacadoList && ownerReference.VendaAtacadoList.length > 0);
                  if ((force || !ownerReference.VendaAtacadoList || ownerReference.VendaAtacadoList.length === 0) && (!common.isNullOrEmpty(common.getAbsoluteValue(ownerReference.IdCliente)))) {
                    var navQuery = 'GetVendaAtacadoByEntitySearchNoAssociations?$inlinecount=none';
                    navQuery += '&$orderby=IdVendaAtacado asc';
                    navQuery += '&jEntitySearch=' + ownerReference.GetJsWhereDetailRelationForVendaAtacado(customParentRelation);        ;
                    dataBusiness.showProcessing();
                    httpFactory.httpGet(getDataServiceUrl, navQuery,
                        function (data) {
                           for (var idx = 0; idx < data.results.length; idx++) {
                               initializePOCO(data.results[idx], 'VendaAtacado'); 
                               data.results[idx].Cliente = ownerReference; 
                           } 
                           ownerReference.VendaAtacadoList = data.results; 
                           ownerReference.setCurrentDetails('VendaAtacado');
                           dataBusiness.closeProcessing();
                           _VendaAtacadoRemoteComplete = true;
                           if (callback && (!common.isNullOrEmpty(detailName) || (_VendaRemoteComplete && _VendaAtacadoRemoteComplete))) { callback(); }
                        }, 
                        function (error) {
                            dataBusiness.closeProcessing();
                            queryFailed(error);
                        });
                  } else { ownerReference.setCurrentDetails('VendaAtacado'); }
                } else { if (!ownerReference.VendaAtacadoIsLoaded && ownerReference.VendaAtacadoList && ownerReference.VendaAtacadoList.length > 0) { ownerReference.VendaAtacadoIsLoaded = true; } }
              } else { 
                if (common.isNullOrEmpty(detailName) || detailName == 'VendaAtacado') {
                   ownerReference.setCurrentDetails('VendaAtacado');
                }
                _VendaAtacadoRemoteComplete = true;
              }
              if (callback && ((!common.isNullOrEmpty(detailName) && (eval('_' + detailName + 'RemoteComplete && ownerReference.' + detailName + 'IsLoaded') == true)) || (common.isNullOrEmpty(detailName) && (_VendaRemoteComplete && _VendaAtacadoRemoteComplete)))) { callback(); }
           };
           //Select first element as a current item of each detail
           ownerReference.setCurrentDetails = function(detailName, clearing) {
              if ((common.isNullOrEmpty(detailName) || detailName === 'Venda')) {
                   if (ownerReference.VendaList.length > 0) { ownerReference.currentVenda = ownerReference.VendaList[0]; if (clearing == null || clearing === false) ownerReference.currentVenda.fillDetails(); }
                   else { ownerReference.currentVenda = null; }
              }
              if ((common.isNullOrEmpty(detailName) || detailName === 'VendaAtacado')) {
                   if (ownerReference.VendaAtacadoList.length > 0) { ownerReference.currentVendaAtacado = ownerReference.VendaAtacadoList[0]; if (clearing == null || clearing === false) ownerReference.currentVendaAtacado.fillDetails(); }
                   else { ownerReference.currentVendaAtacado = null; }
              }
           };
        //#region Adjust details already loaded for a POCO reference
           if ((typeof ownerReference.VendaList === 'function') && ownerReference.VendaList.length > 0) {
                for(var idx = 0; idx < ownerReference.VendaList.length; idx++) {  VendaInitializer(ownerReference.VendaList[idx], true); }
           }
           if ((typeof ownerReference.VendaAtacadoList === 'function') && ownerReference.VendaAtacadoList.length > 0) {
                for(var idx = 0; idx < ownerReference.VendaAtacadoList.length; idx++) {  VendaAtacadoInitializer(ownerReference.VendaAtacadoList[idx], true); }
           }
        //#endregion Adjust details already loaded for a POCO reference
        };
        lookUpProperties['Venda'] = {IdLoja: 'LookUpLoja'};
        var VendaInitializer = function (ownerReference) {
           ownerReference.RowDataId = getNextSequence('Venda');
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
            var _datetimeVenda = (ownerReference.DatetimeVenda === null ? null : new Date(ownerReference.DatetimeVenda));
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
            var _stringVenda = ownerReference.StringVenda;
            Object.defineProperty(ownerReference, 'StringVenda', {
              get: function() { return _stringVenda; },
              set: function(newValue) { var oldValue = _stringVenda; _stringVenda = newValue; if (!entityPropChanged(ownerReference, 'StringVenda', oldValue, newValue)) { _stringVenda = oldValue; } }
            });
            //End Property Definitions
           ownerReference.currentVendaItem = null;
           ownerReference.setRemovedLookupFields = function(removedFields) {
               for (var idxLUp in entitylookUps[ownerReference.typeName]) {
                   var hasKeyValue = false;
                   var luName = entitylookUps[ownerReference.typeName][idxLUp];
                   var luMeta = metadataInfo[luName];
                   for (var idxProp in luMeta) {
                       var prop = luMeta[idxProp];
                       if (!common.isNullOrEmpty(prop.relatedKey) && prop.isPartOfKey) {
                           hasKeyValue = !common.isNullOrEmpty(ownerReference[prop.relatedKey]);
                           break;
                       }
                   }
                   if (hasKeyValue) {
                       for (var idxProp in luMeta) {
                           var prop = luMeta[idxProp];
                           if (!common.isNullOrEmpty(prop.relatedKey) && !prop.isPartOfKey) {
                               removedFields.push(prop.relatedKey);
                           }
                       }
                   }
               }
           }
           ownerReference.getJExpression = function(listFilterRange, removedFields, noDetails) {
               if (ownerReference.excludedFilters && ownerReference.excludedFilters.length > 0) { if (removedFields instanceof Array) removedFields = removedFields.concat(ownerReference.excludedFilters); else removedFields = ownerReference.excludedFilters; }
               ownerReference.setRemovedLookupFields(removedFields);
               var jExpression = common.getJEntityExpression(ownerReference, dialog, listFilterRange, removedFields);
               if (jExpression === 'Error') return jExpression;
               if (noDetails !== true && ownerReference.VendaItemList && ownerReference.VendaItemList.length > 0) {
                 var detailExpr = ownerReference.VendaItemList[0].getJExpression(listFilterRange, ['IdVenda']);
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
               if (!common.isNullOrEmpty(ownerReference.original)) {
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
           ownerReference.getValidationErrors = function(propertyName) {
               var errors = [];
               if (!dataBusiness.canReportErrors) return errors;
               if (!ownerReference.ChangeState || ['I', 'U'].indexOf(ownerReference.ChangeState) < 0) return errors;
               var properties = metadataInfo[ownerReference.typeName];
               for (var i = 0; i < properties.length; i++) {
                   var prop = properties[i];
                   if (common.isNullOrEmpty(propertyName) || prop.key == propertyName) {
                       if (prop.isRequired === true && !prop.isPartOfKey && common.isNullOrEmpty(ownerReference[prop.key])) errors.push('O campo [' + prop.headerText + (managerAuth.shellMode=='DEV' ? ' (' + ownerReference.typeName + '.' + prop.key + ')' : '') + '] é requerido.');
                       if (prop.validateMaxLength === true && prop.maxLength > 0 && !common.isNullOrEmpty(ownerReference[prop.key]) && ownerReference[prop.key].length > prop.maxLength) errors.push('O campo [' + prop.headerText + (managerAuth.shellMode=='DEV' ? ' (' + ownerReference.typeName + '.' + prop.key + ')' : '') + '] permite no máximo ' + prop.maxLength.toString() + ' caractere(s).');
                   }
               }
               if (common.isNullOrEmpty(propertyName)) {
                   for (var i = 0; i < ownerReference.VendaItemList().length; i++) {
                       var detail = ownerReference.VendaItemList()[i];
                       errors = errors.concat(detail.getValidationErrors());
                   }
               }
               return errors;
           }
           ownerReference.getPrimitiveDTO = function(loadDetails) {
               var command = '';
               var properties = metadataInfo[ownerReference.typeName];
               for (var i = 0; i < properties.length; i++) {
                   command += (command === '' ? '' : ', ') + properties[i].key + ': ownerReference.' + properties[i].key;
                   if (properties[i].isDomain && properties[i].key.length > 4) command += (command === '' ? '' : ', ') + common.strLeft(properties[i].key, properties[i].key.length - 4) + ': ownerReference.' + common.strLeft(properties[i].key, properties[i].key.length - 4);
               }
               var result = {};
               eval('result = { ' + command + ' };');
               if (loadDetails) {
                   result.VendaItemList = [];
                   var sourceList = ownerReference.VendaItemList;
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
               var _VendaItemList = ownerReference.VendaItemList;
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
                    ownerReference[properties[i].key] = originData[properties[i].key];
               }
               if (copyDetails) {
                   if (ownerReference.VendaItemList && originData.VendaItemList) {
                       var toList = ownerReference.VendaItemList;
                       var fromList = originData.VendaItemList;
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
              ownerReference.refreshData = function(noWait, succeeded) {
                 var filterByKey = 'Venda{' + 'IdVenda#==#I' + ownerReference.IdVenda.toString() + '}';
                 return dataContext.getVendaByEntitySearchNoAssociations(filterByKey, 0, 0, false, '', querySucceeded);
                 function querySucceeded(data) {
                    if (data.results.length > 0) {  for (var idx = 0; idx < data.results.length; idx++) { ownerReference.copyDataFrom(data.results[idx]); } }
                    if (succeeded) { succeeded(data); }
                    if (data.results.length == 0) { return; }
                    if (!noWait || ownerReference.atLeastOneDetailLoaded()) { ownerReference.fillDetails(true, '', noWait); }
               }
              }
           ownerReference.isAdded = function() { return ownerReference.ChangeState === 'I'; };
           ownerReference.isDeleted = function() { return ownerReference.ChangeState === 'D'; };
           ownerReference.isModified = function() { return ownerReference.ChangeState === 'U'; };
           ownerReference.isDetached = function() { return false; };
           ownerReference.isUnchanged = function() { return ownerReference.ChangeState === 'N'; };
           ownerReference.setModified = function() { ownerReference.ChangeState = 'U'; };
           ownerReference.setUnchanged = function() { ownerReference.ChangeState = 'N'; };
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
              return (property != null && !common.isNullOrEmpty(property.lookupPropertyName) ? property.lookupPropertyName : propertyName);
           }
           ownerReference.getLookupVisibleColumns = function(propertyName) {
              var property = getEntityProperty(ownerReference.typeName, propertyName);
              return (property != null ? property.lookupVisibleColumns : '');
           }
           ownerReference.getLookupDisplay = function (lookupName) {
               var displayName = '';
               if (lookupName === 'LookUpLoja') {
                   displayName = ' de Loja';
               }
               return 'Seleção' + displayName;
           };
        
           ownerReference.getSubQueryFilterFromLookUpLoja = function (propertyName) {
               var filter = '';
               return filter;
           }
           ownerReference.canGetClientFilter = function (lookupName) {
               return true;
           }
        
           ownerReference.hasValidClientFilter = function (lookupName, lookupInfo) {
               var checkClientFilter = '';
               if (typeof ownerReference['BeforeGet' + lookupName + 'Query'] == 'function') {
                   checkClientFilter = ownerReference['BeforeGet' + lookupName + 'Query']();
                   if (checkClientFilter === 'Error') { return false; }
               }
               return true;
           }
        
        //#region LookUps Finalizers
           ownerReference.executeLookUpEstado = function (lookupProperty, entityProperty, pageSkip, pageSize, queryCallback) {
               if (!lookupProperty) { if (queryCallback) queryCallback(true, [], 0); return null; }
               if (common.isNullOrEmpty(entityProperty)) entityProperty = lookupProperty;
               var valueToSearch = ownerReference[entityProperty];
               var extraFilters = '';
               if (ownerReference.canGetClientFilter('LookUpEstado')) {
                   if (typeof ownerReference['BeforeGetLookUpEstadoQuery'] == 'function') {
                       var customFilter = ownerReference['BeforeGetLookUpEstadoQuery']();
                       if (customFilter === 'Error') { if (queryCallback) queryCallback(true, [], 0); return null; }
                       if (!common.isNullOrEmpty(customFilter)) { extraFilters = (common.isNullOrEmpty(extraFilters) ? '' : extraFilters + ';') + customFilter; }
                   }
                   if (typeof ownerReference['getSubQueryFilterFromLookUpEstado'] == 'function') {
                       var customFilter = ownerReference['getSubQueryFilterFromLookUpEstado'](lookupProperty);
                       if (customFilter === 'Error') { if (queryCallback) queryCallback(true, [], 0); return null; }
                       if (!common.isNullOrEmpty(customFilter)) { extraFilters = (common.isNullOrEmpty(extraFilters) ? '' : extraFilters + ';') + customFilter; }
                   }
               }
               var completeExpression = common.getLookUpJEntityExpression('LookUpEstado', ownerReference, lookupProperty, valueToSearch, extraFilters, entityProperty, dialog);
               if (completeExpression === 'Error') { if (queryCallback) queryCallback(true, [], 0); return null; }
               var callbackSucceeded = function (data) { if (queryCallback) queryCallback(false, data.results, data.inlineCount); };
               var callbackFailed = function (error) { if (queryCallback) queryCallback(true, [], 0); queryFailed(error); };
               return dataContext.getLookUpEstadoByEntitySearch(completeExpression, lookupProperty, pageSkip, pageSize, 'asc', callbackSucceeded, function() { }, callbackFailed);
           };
        
           ownerReference.finalizeLookUpEstado = function (lookupProperty, entityProperty, selectedElements) {
               if (!selectedElements)
                   return;
               if ((typeof selectedElements.length) === 'undefined') {
                   selectedElements = [selectedElements];
               }
        
               //Mount query list for QBE
               if (dataBusiness && dataBusiness.status() == 'C' && selectedElements != null && selectedElements.length > 1) {
                   var results = '';
                   for (var index = 0; index < selectedElements.length; index++) {
                       results += (index == 0 ? '' : ',') + selectedElements[index][lookupProperty].toString().trim();
                   }
                   results = '[' + results + ']';
                   ownerReference[entityProperty] = results;
                   dataBusiness.entitySearchRange[ownerReference.typeName + entityProperty](results);
                   return;
               }
        
               var replaceTo = ownerReference;
               for (var i = 0; i < selectedElements.length; i++)
               {
                   var selectedElement = selectedElements[i];
                   if (selectedElement.hasOwnProperty('IdEstado') && (replaceTo.hasOwnProperty('IdEstado') || replaceTo.__proto__.hasOwnProperty('IdEstado')))
                   {
                       replaceTo.IdEstado = selectedElement.IdEstado;
                   }
                   else if (replaceTo.hasOwnProperty('IdEstado') || replaceTo.__proto__.hasOwnProperty('IdEstado')) {
                       replaceTo.IdEstado = null;
                   }
                   if (selectedElement.hasOwnProperty('StringEstado') && (replaceTo.hasOwnProperty('StringEstado') || replaceTo.__proto__.hasOwnProperty('StringEstado')))
                   {
                       replaceTo.StringEstado = selectedElement.StringEstado;
                   }
                   else if (replaceTo.hasOwnProperty('StringEstado') || replaceTo.__proto__.hasOwnProperty('StringEstado')) {
                       replaceTo.StringEstado = null;
                   }
               }
           };
        
           ownerReference.clearLookUpEstado = function () {
               ownerReference.IdEstado = null;
               ownerReference.StringEstado = null;
           }
           ownerReference.executeLookUpLoja = function (lookupProperty, entityProperty, pageSkip, pageSize, queryCallback) {
               if (!lookupProperty) { if (queryCallback) queryCallback(true, [], 0); return null; }
               if (common.isNullOrEmpty(entityProperty)) entityProperty = lookupProperty;
               var valueToSearch = ownerReference[entityProperty];
               var extraFilters = '';
               if (ownerReference.canGetClientFilter('LookUpLoja')) {
                   if (typeof ownerReference['BeforeGetLookUpLojaQuery'] == 'function') {
                       var customFilter = ownerReference['BeforeGetLookUpLojaQuery']();
                       if (customFilter === 'Error') { if (queryCallback) queryCallback(true, [], 0); return null; }
                       if (!common.isNullOrEmpty(customFilter)) { extraFilters = (common.isNullOrEmpty(extraFilters) ? '' : extraFilters + ';') + customFilter; }
                   }
                   if (typeof ownerReference['getSubQueryFilterFromLookUpLoja'] == 'function') {
                       var customFilter = ownerReference['getSubQueryFilterFromLookUpLoja'](lookupProperty);
                       if (customFilter === 'Error') { if (queryCallback) queryCallback(true, [], 0); return null; }
                       if (!common.isNullOrEmpty(customFilter)) { extraFilters = (common.isNullOrEmpty(extraFilters) ? '' : extraFilters + ';') + customFilter; }
                   }
               }
               var completeExpression = common.getLookUpJEntityExpression('LookUpLoja', ownerReference, lookupProperty, valueToSearch, extraFilters, entityProperty, dialog);
               if (completeExpression === 'Error') { if (queryCallback) queryCallback(true, [], 0); return null; }
               var callbackSucceeded = function (data) { if (queryCallback) queryCallback(false, data.results, data.inlineCount); };
               var callbackFailed = function (error) { if (queryCallback) queryCallback(true, [], 0); queryFailed(error); };
               return dataContext.getLookUpLojaByEntitySearch(completeExpression, lookupProperty, pageSkip, pageSize, 'asc', callbackSucceeded, function() { }, callbackFailed);
           };
        
           ownerReference.finalizeLookUpLoja = function (lookupProperty, entityProperty, selectedElements) {
               if (!selectedElements)
                   return;
               if ((typeof selectedElements.length) === 'undefined') {
                   selectedElements = [selectedElements];
               }
        
               //Mount query list for QBE
               if (dataBusiness && dataBusiness.status() == 'C' && selectedElements != null && selectedElements.length > 1) {
                   var results = '';
                   for (var index = 0; index < selectedElements.length; index++) {
                       results += (index == 0 ? '' : ',') + selectedElements[index][lookupProperty].toString().trim();
                   }
                   results = '[' + results + ']';
                   ownerReference[entityProperty] = results;
                   dataBusiness.entitySearchRange[ownerReference.typeName + entityProperty](results);
                   return;
               }
        
               var replaceTo = ownerReference;
               for (var i = 0; i < selectedElements.length; i++)
               {
                   var selectedElement = selectedElements[i];
                   if (selectedElement.hasOwnProperty('IdLoja') && (replaceTo.hasOwnProperty('IdLoja') || replaceTo.__proto__.hasOwnProperty('IdLoja')))
                   {
                       replaceTo.IdLoja = selectedElement.IdLoja;
                   }
                   else if (replaceTo.hasOwnProperty('IdLoja') || replaceTo.__proto__.hasOwnProperty('IdLoja')) {
                       replaceTo.IdLoja = null;
                   }
               }
           };
        
           ownerReference.clearLookUpLoja = function () {
               ownerReference.IdLoja = null;
           }
        //#endregion
           //#endregion Lookup Extended Methods
           ownerReference.setDefaults = function () {
           };
           ownerReference.delete = function() {
               if (ownerReference.setParentAsModified) ownerReference.setParentAsModified();
               var parent = ownerReference.Cliente;
               if (!common.isNullOrEmpty(ownerReference.VendaItemList) && ownerReference.VendaItemList.length > 0) {
                  var details = [].concat(ownerReference.VendaItemList);
                  for (var idx = 0; idx < details.length; idx++) {
                    details[idx].delete();
                  }
               }
               if (ownerReference.ChangeState == 'I') {
                   if (parent && parent.VendaList) { 
                       var idx = parent.VendaList.indexOf(ownerReference); 
                       if (idx >= 0) parent.VendaList.splice(idx, 1); 
                   }
                   else {
                       var idx = dataBusiness.dataView.indexOf(ownerReference);
                       if (idx >= 0) dataBusiness.dataView.splice(idx, 1);
                   }
                   delete ownerReference.Cliente;
               }
               else {
                   if (ownerReference.ChangeState == 'N') { ownerReference.createOriginal(); }
                   ownerReference.ChangeState = 'D'; // mark for deletion
               }
               if (parent && (typeof parent.setCurrentDetails === 'function') && parent.VendaList && parent.VendaList.length == 0) parent.setCurrentDetails('Venda');
           };
           ownerReference.setParentAsModified = function() {
           var parent = ownerReference.Cliente;
           if (parent) {
               if (parent.isUnchanged()) {
                   parent.setModified(); 
               }
               parent.setParentAsModified();
           }
           };
           ownerReference.getParent = function() {
               return ownerReference.Cliente;
           };
           ownerReference.getSelfList = function() {
               var parent = ownerReference.getParent();
               if (!common.isNullOrEmpty(parent)) {
                   return parent.VendaList;
               } else { return null; }
           };
           ownerReference.Namespace = 'Linx.Demo.BV.PaiFilha';
           ownerReference.myProperties = [ 'BigIntVenda', 'BitVenda', 'ComboboxVenda', 'DatetimeVenda', 'DecimalVenda', 'GuidVenda', 'IdCliente', 'IdLoja', 'IdVenda', 'IntVenda', 'SmallIntVenda', 'StringVenda' ];
           ownerReference.queryRequiredProperties = {  };
           ownerReference.excludedFilters = [];
           ownerReference.getCurrentElements = function() {
               var result = [ ownerReference ];
           if (!common.isNullOrEmpty(ownerReference.currentVendaItem)) { result = result.concat(ownerReference.currentVendaItem.getCurrentElements()); }
               return result;
           };
           ownerReference.checkForSendingAllRowsToServer = function() {
           };
           ownerReference.GetJsWhereDetailRelationForVendaItem = function(customParentRelation) {
       return 'VendaItem{' + (!common.isNullOrEmpty(customParentRelation) ? customParentRelation : 'IdVenda#==#' + ownerReference.serverDataType['IdVenda'] + common.getAbsoluteValue(ownerReference.IdVenda).toString()) + '}';        
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
               if (value === false)
                   ownerReference.VendaItemList([]);
           }
           ownerReference.fillDetails = function(force, detailName, noWait, callback, customParentRelation) {
              if (typeof force === 'undefined') force = false;
              if (ownerReference.isAdded()) {
                ownerReference.VendaItemIsLoaded = true;
              }
              var _VendaItemRemoteComplete = false;
              var detachList_VendaItem = [];
              if (force) {
                   if (common.isNullOrEmpty(detailName) || detailName == 'VendaItem') ownerReference.VendaItemIsLoaded = false;
                   if ((common.isNullOrEmpty(detailName) || detailName == 'VendaItem') && ownerReference.VendaItemList && ownerReference.VendaItemList.length > 0) {
                         ownerReference.VendaItemList = [];
                   }
              }
        
              if (!ownerReference.VendaItemIsLoaded) {
                //Load VendaItemList
                if (common.isNullOrEmpty(detailName) || detailName === 'VendaItem') {
                  ownerReference.VendaItemIsLoaded = true;
                  _VendaItemRemoteComplete = (ownerReference.VendaItemList && ownerReference.VendaItemList.length > 0);
                  if ((force || !ownerReference.VendaItemList || ownerReference.VendaItemList.length === 0) && (!common.isNullOrEmpty(common.getAbsoluteValue(ownerReference.IdVenda)))) {
                    var navQuery = 'GetVendaItemByEntitySearchNoAssociations?$inlinecount=none';
                    navQuery += '&$orderby=IdVendaItem asc';
                    navQuery += '&jEntitySearch=' + ownerReference.GetJsWhereDetailRelationForVendaItem(customParentRelation);        ;
                    dataBusiness.showProcessing();
                    httpFactory.httpGet(getDataServiceUrl, navQuery,
                        function (data) {
                           for (var idx = 0; idx < data.results.length; idx++) {
                               initializePOCO(data.results[idx], 'VendaItem'); 
                               data.results[idx].Venda = ownerReference; 
                           } 
                           ownerReference.VendaItemList = data.results; 
                           ownerReference.setCurrentDetails('VendaItem');
                           dataBusiness.closeProcessing();
                           _VendaItemRemoteComplete = true;
                           if (callback && (!common.isNullOrEmpty(detailName) || (_VendaItemRemoteComplete))) { callback(); }
                        }, 
                        function (error) {
                            dataBusiness.closeProcessing();
                            queryFailed(error);
                        });
                  } else { ownerReference.setCurrentDetails('VendaItem'); }
                } else { if (!ownerReference.VendaItemIsLoaded && ownerReference.VendaItemList && ownerReference.VendaItemList.length > 0) { ownerReference.VendaItemIsLoaded = true; } }
              } else { 
                if (common.isNullOrEmpty(detailName) || detailName == 'VendaItem') {
                   ownerReference.setCurrentDetails('VendaItem');
                }
                _VendaItemRemoteComplete = true;
              }
              if (callback && ((!common.isNullOrEmpty(detailName) && (eval('_' + detailName + 'RemoteComplete && ownerReference.' + detailName + 'IsLoaded') == true)) || (common.isNullOrEmpty(detailName) && (_VendaItemRemoteComplete)))) { callback(); }
           };
           //Select first element as a current item of each detail
           ownerReference.setCurrentDetails = function(detailName, clearing) {
              if ((common.isNullOrEmpty(detailName) || detailName === 'VendaItem')) {
                   if (ownerReference.VendaItemList.length > 0) { ownerReference.currentVendaItem = ownerReference.VendaItemList[0]; if (clearing == null || clearing === false) ownerReference.currentVendaItem.fillDetails(); }
                   else { ownerReference.currentVendaItem = null; }
              }
           };
        //#region Adjust details already loaded for a POCO reference
           if ((typeof ownerReference.VendaItemList === 'function') && ownerReference.VendaItemList.length > 0) {
                for(var idx = 0; idx < ownerReference.VendaItemList.length; idx++) {  VendaItemInitializer(ownerReference.VendaItemList[idx], true); }
           }
        //#endregion Adjust details already loaded for a POCO reference
        };
        lookUpProperties['VendaItem'] = {};
        var VendaItemInitializer = function (ownerReference) {
           ownerReference.RowDataId = getNextSequence('VendaItem');
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
            var _datetimeVendaItem = (ownerReference.DatetimeVendaItem === null ? null : new Date(ownerReference.DatetimeVendaItem));
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
                       if (!common.isNullOrEmpty(prop.relatedKey) && prop.isPartOfKey) {
                           hasKeyValue = !common.isNullOrEmpty(ownerReference[prop.relatedKey]);
                           break;
                       }
                   }
                   if (hasKeyValue) {
                       for (var idxProp in luMeta) {
                           var prop = luMeta[idxProp];
                           if (!common.isNullOrEmpty(prop.relatedKey) && !prop.isPartOfKey) {
                               removedFields.push(prop.relatedKey);
                           }
                       }
                   }
               }
           }
           ownerReference.getJExpression = function(listFilterRange, removedFields, noDetails) {
               if (ownerReference.excludedFilters && ownerReference.excludedFilters.length > 0) { if (removedFields instanceof Array) removedFields = removedFields.concat(ownerReference.excludedFilters); else removedFields = ownerReference.excludedFilters; }
               ownerReference.setRemovedLookupFields(removedFields);
               var jExpression = common.getJEntityExpression(ownerReference, dialog, listFilterRange, removedFields);
               if (jExpression === 'Error') return jExpression;
               return jExpression;
          };
           ownerReference.createOriginal = function(propertyName, oldValue) {
               ownerReference.original = ownerReference.getPrimitiveDTO();
               if (propertyName) ownerReference.original[propertyName] = oldValue;
           }
           ownerReference.restoreOriginal = function() {
               if (!common.isNullOrEmpty(ownerReference.original)) {
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
           ownerReference.getValidationErrors = function(propertyName) {
               var errors = [];
               if (!dataBusiness.canReportErrors) return errors;
               if (!ownerReference.ChangeState || ['I', 'U'].indexOf(ownerReference.ChangeState) < 0) return errors;
               var properties = metadataInfo[ownerReference.typeName];
               for (var i = 0; i < properties.length; i++) {
                   var prop = properties[i];
                   if (common.isNullOrEmpty(propertyName) || prop.key == propertyName) {
                       if (prop.isRequired === true && !prop.isPartOfKey && common.isNullOrEmpty(ownerReference[prop.key])) errors.push('O campo [' + prop.headerText + (managerAuth.shellMode=='DEV' ? ' (' + ownerReference.typeName + '.' + prop.key + ')' : '') + '] é requerido.');
                       if (prop.validateMaxLength === true && prop.maxLength > 0 && !common.isNullOrEmpty(ownerReference[prop.key]) && ownerReference[prop.key].length > prop.maxLength) errors.push('O campo [' + prop.headerText + (managerAuth.shellMode=='DEV' ? ' (' + ownerReference.typeName + '.' + prop.key + ')' : '') + '] permite no máximo ' + prop.maxLength.toString() + ' caractere(s).');
                   }
               }
               return errors;
           }
           ownerReference.getPrimitiveDTO = function(loadDetails) {
               var command = '';
               var properties = metadataInfo[ownerReference.typeName];
               for (var i = 0; i < properties.length; i++) {
                   command += (command === '' ? '' : ', ') + properties[i].key + ': ownerReference.' + properties[i].key;
                   if (properties[i].isDomain && properties[i].key.length > 4) command += (command === '' ? '' : ', ') + common.strLeft(properties[i].key, properties[i].key.length - 4) + ': ownerReference.' + common.strLeft(properties[i].key, properties[i].key.length - 4);
               }
               var result = {};
               eval('result = { ' + command + ' };');
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
                    ownerReference[properties[i].key] = originData[properties[i].key];
               }
               enableChangeTrack = true;
           };
              ownerReference.refreshData = function(noWait, succeeded) {
                 var filterByKey = 'VendaItem{' + 'IdVendaItem#==#I' + ownerReference.IdVendaItem.toString() + '}';
                 return dataContext.getVendaItemByEntitySearchNoAssociations(filterByKey, 0, 0, false, '', querySucceeded);
                 function querySucceeded(data) {
                    if (data.results.length > 0) {  for (var idx = 0; idx < data.results.length; idx++) { ownerReference.copyDataFrom(data.results[idx]); } }
                    if (succeeded) { succeeded(data); }
                    if (data.results.length == 0) { return; }
                    if (!noWait || ownerReference.atLeastOneDetailLoaded()) { ownerReference.fillDetails(true, '', noWait); }
               }
              }
           ownerReference.isAdded = function() { return ownerReference.ChangeState === 'I'; };
           ownerReference.isDeleted = function() { return ownerReference.ChangeState === 'D'; };
           ownerReference.isModified = function() { return ownerReference.ChangeState === 'U'; };
           ownerReference.isDetached = function() { return false; };
           ownerReference.isUnchanged = function() { return ownerReference.ChangeState === 'N'; };
           ownerReference.setModified = function() { ownerReference.ChangeState = 'U'; };
           ownerReference.setUnchanged = function() { ownerReference.ChangeState = 'N'; };
           ownerReference.serverDataType = [];
           ownerReference.serverDataType['BigIntVendaItem'] = 'L';
           ownerReference.serverDataType['BitVendaItem'] = 'B';
           ownerReference.serverDataType['ComboboxVendaItem'] = 'Y';
           ownerReference.serverDataType['DatetimeVendaItem'] = 'T';
           ownerReference.serverDataType['DecimalVendaItem'] = 'D';
           ownerReference.serverDataType['GuidVendaItem'] = 'G';
           ownerReference.serverDataType['IdVenda'] = 'I';
           ownerReference.serverDataType['IdVendaItem'] = 'I';
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
           };
           ownerReference.delete = function() {
               if (ownerReference.setParentAsModified) ownerReference.setParentAsModified();
               var parent = ownerReference.Venda;
               if (ownerReference.ChangeState == 'I') {
                   if (parent && parent.VendaItemList) { 
                       var idx = parent.VendaItemList.indexOf(ownerReference); 
                       if (idx >= 0) parent.VendaItemList.splice(idx, 1); 
                   }
                   else {
                       var idx = dataBusiness.dataView.indexOf(ownerReference);
                       if (idx >= 0) dataBusiness.dataView.splice(idx, 1);
                   }
                   delete ownerReference.Venda;
               }
               else {
                   if (ownerReference.ChangeState == 'N') { ownerReference.createOriginal(); }
                   ownerReference.ChangeState = 'D'; // mark for deletion
               }
               if (parent && (typeof parent.setCurrentDetails === 'function') && parent.VendaItemList && parent.VendaItemList.length == 0) parent.setCurrentDetails('VendaItem');
           };
           ownerReference.setParentAsModified = function() {
           var parent = ownerReference.Venda;
           if (parent) {
               if (parent.isUnchanged()) {
                   parent.setModified(); 
               }
               parent.setParentAsModified();
           }
           };
           ownerReference.getParent = function() {
               return ownerReference.Venda;
           };
           ownerReference.getSelfList = function() {
               var parent = ownerReference.getParent();
               if (!common.isNullOrEmpty(parent)) {
                   return parent.VendaItemList;
               } else { return null; }
           };
           ownerReference.Namespace = 'Linx.Demo.BV.PaiFilha';
           ownerReference.myProperties = [ 'BigIntVendaItem', 'BitVendaItem', 'ComboboxVendaItem', 'DatetimeVendaItem', 'DecimalVendaItem', 'GuidVendaItem', 'IdVenda', 'IdVendaItem', 'IntVendaItem', 'SmallIntVendaItem', 'StringVendaItem' ];
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
           ownerReference.fillDetails = function(force, detailName, noWait, callback, customParentRelation) {
              if (typeof force === 'undefined') force = false;
              if (callback) { callback(); }
           };
           //Select first element as a current item of each detail
           ownerReference.setCurrentDetails = function(detailName, clearing) {
           };
        };
        lookUpProperties['VendaAtacado'] = {};
        var VendaAtacadoInitializer = function (ownerReference) {
           ownerReference.RowDataId = getNextSequence('VendaAtacado');
            //Start Property Definitions
            var _bigIntVendaAtacado = ownerReference.BigIntVendaAtacado;
            Object.defineProperty(ownerReference, 'BigIntVendaAtacado', {
              get: function() { return _bigIntVendaAtacado; },
              set: function(newValue) { var oldValue = _bigIntVendaAtacado; _bigIntVendaAtacado = newValue; if (!entityPropChanged(ownerReference, 'BigIntVendaAtacado', oldValue, newValue)) { _bigIntVendaAtacado = oldValue; } }
            });
            var _bitVendaAtacado = ownerReference.BitVendaAtacado;
            Object.defineProperty(ownerReference, 'BitVendaAtacado', {
              get: function() { return _bitVendaAtacado; },
              set: function(newValue) { var oldValue = _bitVendaAtacado; _bitVendaAtacado = newValue; if (!entityPropChanged(ownerReference, 'BitVendaAtacado', oldValue, newValue)) { _bitVendaAtacado = oldValue; } }
            });
            var _comboboxVendaAtacado = ownerReference.ComboboxVendaAtacado;
            Object.defineProperty(ownerReference, 'ComboboxVendaAtacado', {
              get: function() { return _comboboxVendaAtacado; },
              set: function(newValue) { var oldValue = _comboboxVendaAtacado; _comboboxVendaAtacado = newValue; if (!entityPropChanged(ownerReference, 'ComboboxVendaAtacado', oldValue, newValue)) { _comboboxVendaAtacado = oldValue; } else { _comboboxVendaAtacadoName = (dataDomains.getName('LX_VENDA_ATACADO', newValue)); } }
            });
            var _comboboxVendaAtacadoName = ownerReference.ComboboxVendaAtacadoName;
            Object.defineProperty(ownerReference, 'ComboboxVendaAtacadoName', {
              get: function() { return _comboboxVendaAtacadoName; },
              set: function(newValue) { var oldValue = _comboboxVendaAtacadoName; _comboboxVendaAtacadoName = newValue; if (!entityPropChanged(ownerReference, 'ComboboxVendaAtacadoName', oldValue, newValue)) { _comboboxVendaAtacadoName = oldValue; } else { _comboboxVendaAtacado = (dataDomains.getId('LX_VENDA_ATACADO', newValue)); } }
            });
            var _datetimeVendaAtacado = (ownerReference.DatetimeVendaAtacado === null ? null : new Date(ownerReference.DatetimeVendaAtacado));
            Object.defineProperty(ownerReference, 'DatetimeVendaAtacado', {
              get: function() { return _datetimeVendaAtacado; },
              set: function(newValue) { var oldValue = _datetimeVendaAtacado; _datetimeVendaAtacado = newValue; if (!entityPropChanged(ownerReference, 'DatetimeVendaAtacado', oldValue, newValue)) { _datetimeVendaAtacado = oldValue; } }
            });
            var _decimalVendaAtacado = ownerReference.DecimalVendaAtacado;
            Object.defineProperty(ownerReference, 'DecimalVendaAtacado', {
              get: function() { return _decimalVendaAtacado; },
              set: function(newValue) { var oldValue = _decimalVendaAtacado; _decimalVendaAtacado = newValue; if (!entityPropChanged(ownerReference, 'DecimalVendaAtacado', oldValue, newValue)) { _decimalVendaAtacado = oldValue; } }
            });
            var _guidVendaAtacado = ownerReference.GuidVendaAtacado;
            Object.defineProperty(ownerReference, 'GuidVendaAtacado', {
              get: function() { return _guidVendaAtacado; },
              set: function(newValue) { var oldValue = _guidVendaAtacado; _guidVendaAtacado = newValue; if (!entityPropChanged(ownerReference, 'GuidVendaAtacado', oldValue, newValue)) { _guidVendaAtacado = oldValue; } }
            });
            var _idCliente = ownerReference.IdCliente;
            Object.defineProperty(ownerReference, 'IdCliente', {
              get: function() { return _idCliente; },
              set: function(newValue) { var oldValue = _idCliente; _idCliente = newValue; if (!entityPropChanged(ownerReference, 'IdCliente', oldValue, newValue)) { _idCliente = oldValue; } }
            });
            var _idVendaAtacado = ownerReference.IdVendaAtacado;
            Object.defineProperty(ownerReference, 'IdVendaAtacado', {
              get: function() { return _idVendaAtacado; },
              set: function(newValue) { var oldValue = _idVendaAtacado; _idVendaAtacado = newValue; if (!entityPropChanged(ownerReference, 'IdVendaAtacado', oldValue, newValue)) { _idVendaAtacado = oldValue; } }
            });
            var _intVendaAtacado = ownerReference.IntVendaAtacado;
            Object.defineProperty(ownerReference, 'IntVendaAtacado', {
              get: function() { return _intVendaAtacado; },
              set: function(newValue) { var oldValue = _intVendaAtacado; _intVendaAtacado = newValue; if (!entityPropChanged(ownerReference, 'IntVendaAtacado', oldValue, newValue)) { _intVendaAtacado = oldValue; } }
            });
            var _smallIntVendaAtacado = ownerReference.SmallIntVendaAtacado;
            Object.defineProperty(ownerReference, 'SmallIntVendaAtacado', {
              get: function() { return _smallIntVendaAtacado; },
              set: function(newValue) { var oldValue = _smallIntVendaAtacado; _smallIntVendaAtacado = newValue; if (!entityPropChanged(ownerReference, 'SmallIntVendaAtacado', oldValue, newValue)) { _smallIntVendaAtacado = oldValue; } }
            });
            var _stringVendaAtacado = ownerReference.StringVendaAtacado;
            Object.defineProperty(ownerReference, 'StringVendaAtacado', {
              get: function() { return _stringVendaAtacado; },
              set: function(newValue) { var oldValue = _stringVendaAtacado; _stringVendaAtacado = newValue; if (!entityPropChanged(ownerReference, 'StringVendaAtacado', oldValue, newValue)) { _stringVendaAtacado = oldValue; } }
            });
            //End Property Definitions
           ownerReference.setRemovedLookupFields = function(removedFields) {
               for (var idxLUp in entitylookUps[ownerReference.typeName]) {
                   var hasKeyValue = false;
                   var luName = entitylookUps[ownerReference.typeName][idxLUp];
                   var luMeta = metadataInfo[luName];
                   for (var idxProp in luMeta) {
                       var prop = luMeta[idxProp];
                       if (!common.isNullOrEmpty(prop.relatedKey) && prop.isPartOfKey) {
                           hasKeyValue = !common.isNullOrEmpty(ownerReference[prop.relatedKey]);
                           break;
                       }
                   }
                   if (hasKeyValue) {
                       for (var idxProp in luMeta) {
                           var prop = luMeta[idxProp];
                           if (!common.isNullOrEmpty(prop.relatedKey) && !prop.isPartOfKey) {
                               removedFields.push(prop.relatedKey);
                           }
                       }
                   }
               }
           }
           ownerReference.getJExpression = function(listFilterRange, removedFields, noDetails) {
               if (ownerReference.excludedFilters && ownerReference.excludedFilters.length > 0) { if (removedFields instanceof Array) removedFields = removedFields.concat(ownerReference.excludedFilters); else removedFields = ownerReference.excludedFilters; }
               ownerReference.setRemovedLookupFields(removedFields);
               var jExpression = common.getJEntityExpression(ownerReference, dialog, listFilterRange, removedFields);
               if (jExpression === 'Error') return jExpression;
               return jExpression;
          };
           ownerReference.createOriginal = function(propertyName, oldValue) {
               ownerReference.original = ownerReference.getPrimitiveDTO();
               if (propertyName) ownerReference.original[propertyName] = oldValue;
           }
           ownerReference.restoreOriginal = function() {
               if (!common.isNullOrEmpty(ownerReference.original)) {
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
           ownerReference.getValidationErrors = function(propertyName) {
               var errors = [];
               if (!dataBusiness.canReportErrors) return errors;
               if (!ownerReference.ChangeState || ['I', 'U'].indexOf(ownerReference.ChangeState) < 0) return errors;
               var properties = metadataInfo[ownerReference.typeName];
               for (var i = 0; i < properties.length; i++) {
                   var prop = properties[i];
                   if (common.isNullOrEmpty(propertyName) || prop.key == propertyName) {
                       if (prop.isRequired === true && !prop.isPartOfKey && common.isNullOrEmpty(ownerReference[prop.key])) errors.push('O campo [' + prop.headerText + (managerAuth.shellMode=='DEV' ? ' (' + ownerReference.typeName + '.' + prop.key + ')' : '') + '] é requerido.');
                       if (prop.validateMaxLength === true && prop.maxLength > 0 && !common.isNullOrEmpty(ownerReference[prop.key]) && ownerReference[prop.key].length > prop.maxLength) errors.push('O campo [' + prop.headerText + (managerAuth.shellMode=='DEV' ? ' (' + ownerReference.typeName + '.' + prop.key + ')' : '') + '] permite no máximo ' + prop.maxLength.toString() + ' caractere(s).');
                   }
               }
               return errors;
           }
           ownerReference.getPrimitiveDTO = function(loadDetails) {
               var command = '';
               var properties = metadataInfo[ownerReference.typeName];
               for (var i = 0; i < properties.length; i++) {
                   command += (command === '' ? '' : ', ') + properties[i].key + ': ownerReference.' + properties[i].key;
                   if (properties[i].isDomain && properties[i].key.length > 4) command += (command === '' ? '' : ', ') + common.strLeft(properties[i].key, properties[i].key.length - 4) + ': ownerReference.' + common.strLeft(properties[i].key, properties[i].key.length - 4);
               }
               var result = {};
               eval('result = { ' + command + ' };');
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
                    ownerReference[properties[i].key] = originData[properties[i].key];
               }
               enableChangeTrack = true;
           };
              ownerReference.refreshData = function(noWait, succeeded) {
                 var filterByKey = 'VendaAtacado{' + 'IdVendaAtacado#==#I' + ownerReference.IdVendaAtacado.toString() + '}';
                 return dataContext.getVendaAtacadoByEntitySearchNoAssociations(filterByKey, 0, 0, false, '', querySucceeded);
                 function querySucceeded(data) {
                    if (data.results.length > 0) {  for (var idx = 0; idx < data.results.length; idx++) { ownerReference.copyDataFrom(data.results[idx]); } }
                    if (succeeded) { succeeded(data); }
                    if (data.results.length == 0) { return; }
                    if (!noWait || ownerReference.atLeastOneDetailLoaded()) { ownerReference.fillDetails(true, '', noWait); }
               }
              }
           ownerReference.isAdded = function() { return ownerReference.ChangeState === 'I'; };
           ownerReference.isDeleted = function() { return ownerReference.ChangeState === 'D'; };
           ownerReference.isModified = function() { return ownerReference.ChangeState === 'U'; };
           ownerReference.isDetached = function() { return false; };
           ownerReference.isUnchanged = function() { return ownerReference.ChangeState === 'N'; };
           ownerReference.setModified = function() { ownerReference.ChangeState = 'U'; };
           ownerReference.setUnchanged = function() { ownerReference.ChangeState = 'N'; };
           ownerReference.serverDataType = [];
           ownerReference.serverDataType['BigIntVendaAtacado'] = 'L';
           ownerReference.serverDataType['BitVendaAtacado'] = 'B';
           ownerReference.serverDataType['ComboboxVendaAtacado'] = 'Y';
           ownerReference.serverDataType['DatetimeVendaAtacado'] = 'T';
           ownerReference.serverDataType['DecimalVendaAtacado'] = 'D';
           ownerReference.serverDataType['GuidVendaAtacado'] = 'G';
           ownerReference.serverDataType['IdCliente'] = 'I';
           ownerReference.serverDataType['IdVendaAtacado'] = 'I';
           ownerReference.serverDataType['IntVendaAtacado'] = 'I';
           ownerReference.serverDataType['SmallIntVendaAtacado'] = 'H';
           ownerReference.serverDataType['StringVendaAtacado'] = 'S';
           ownerReference.typeName = 'VendaAtacado';
           ownerReference.isPrimaryKey = function(propertyName) {
               var keys = [ 'IdVendaAtacado' ];
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
           };
           ownerReference.delete = function() {
               if (ownerReference.setParentAsModified) ownerReference.setParentAsModified();
               var parent = ownerReference.Cliente;
               if (ownerReference.ChangeState == 'I') {
                   if (parent && parent.VendaAtacadoList) { 
                       var idx = parent.VendaAtacadoList.indexOf(ownerReference); 
                       if (idx >= 0) parent.VendaAtacadoList.splice(idx, 1); 
                   }
                   else {
                       var idx = dataBusiness.dataView.indexOf(ownerReference);
                       if (idx >= 0) dataBusiness.dataView.splice(idx, 1);
                   }
                   delete ownerReference.Cliente;
               }
               else {
                   if (ownerReference.ChangeState == 'N') { ownerReference.createOriginal(); }
                   ownerReference.ChangeState = 'D'; // mark for deletion
               }
               if (parent && (typeof parent.setCurrentDetails === 'function') && parent.VendaAtacadoList && parent.VendaAtacadoList.length == 0) parent.setCurrentDetails('VendaAtacado');
           };
           ownerReference.setParentAsModified = function() {
           var parent = ownerReference.Cliente;
           if (parent) {
               if (parent.isUnchanged()) {
                   parent.setModified(); 
               }
               parent.setParentAsModified();
           }
           };
           ownerReference.getParent = function() {
               return ownerReference.Cliente;
           };
           ownerReference.getSelfList = function() {
               var parent = ownerReference.getParent();
               if (!common.isNullOrEmpty(parent)) {
                   return parent.VendaAtacadoList;
               } else { return null; }
           };
           ownerReference.Namespace = 'Linx.Demo.BV.PaiFilha';
           ownerReference.myProperties = [ 'BigIntVendaAtacado', 'BitVendaAtacado', 'ComboboxVendaAtacado', 'DatetimeVendaAtacado', 'DecimalVendaAtacado', 'GuidVendaAtacado', 'IdCliente', 'IdVendaAtacado', 'IntVendaAtacado', 'SmallIntVendaAtacado', 'StringVendaAtacado' ];
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
           ownerReference.fillDetails = function(force, detailName, noWait, callback, customParentRelation) {
              if (typeof force === 'undefined') force = false;
              if (callback) { callback(); }
           };
           //Select first element as a current item of each detail
           ownerReference.setCurrentDetails = function(detailName, clearing) {
           };
        };
        lookUpProperties['Loja'] = {};
        var LojaInitializer = function (ownerReference) {
           ownerReference.RowDataId = getNextSequence('Loja');
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
            var _datetimeLoja = (ownerReference.DatetimeLoja === null ? null : new Date(ownerReference.DatetimeLoja));
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
           ownerReference.currentVendedor = null;
           ownerReference.setRemovedLookupFields = function(removedFields) {
               for (var idxLUp in entitylookUps[ownerReference.typeName]) {
                   var hasKeyValue = false;
                   var luName = entitylookUps[ownerReference.typeName][idxLUp];
                   var luMeta = metadataInfo[luName];
                   for (var idxProp in luMeta) {
                       var prop = luMeta[idxProp];
                       if (!common.isNullOrEmpty(prop.relatedKey) && prop.isPartOfKey) {
                           hasKeyValue = !common.isNullOrEmpty(ownerReference[prop.relatedKey]);
                           break;
                       }
                   }
                   if (hasKeyValue) {
                       for (var idxProp in luMeta) {
                           var prop = luMeta[idxProp];
                           if (!common.isNullOrEmpty(prop.relatedKey) && !prop.isPartOfKey) {
                               removedFields.push(prop.relatedKey);
                           }
                       }
                   }
               }
           }
           ownerReference.getJExpression = function(listFilterRange, removedFields, noDetails) {
               if (ownerReference.excludedFilters && ownerReference.excludedFilters.length > 0) { if (removedFields instanceof Array) removedFields = removedFields.concat(ownerReference.excludedFilters); else removedFields = ownerReference.excludedFilters; }
               ownerReference.setRemovedLookupFields(removedFields);
               var jExpression = common.getJEntityExpression(ownerReference, dialog, listFilterRange, removedFields);
               if (jExpression === 'Error') return jExpression;
               if (noDetails !== true && ownerReference.VendedorList && ownerReference.VendedorList.length > 0) {
                 var detailExpr = ownerReference.VendedorList[0].getJExpression(listFilterRange, ['IdLoja']);
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
               if (!common.isNullOrEmpty(ownerReference.original)) {
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
           ownerReference.getValidationErrors = function(propertyName) {
               var errors = [];
               if (!dataBusiness.canReportErrors) return errors;
               if (!ownerReference.ChangeState || ['I', 'U'].indexOf(ownerReference.ChangeState) < 0) return errors;
               var properties = metadataInfo[ownerReference.typeName];
               for (var i = 0; i < properties.length; i++) {
                   var prop = properties[i];
                   if (common.isNullOrEmpty(propertyName) || prop.key == propertyName) {
                       if (prop.isRequired === true && !prop.isPartOfKey && common.isNullOrEmpty(ownerReference[prop.key])) errors.push('O campo [' + prop.headerText + (managerAuth.shellMode=='DEV' ? ' (' + ownerReference.typeName + '.' + prop.key + ')' : '') + '] é requerido.');
                       if (prop.validateMaxLength === true && prop.maxLength > 0 && !common.isNullOrEmpty(ownerReference[prop.key]) && ownerReference[prop.key].length > prop.maxLength) errors.push('O campo [' + prop.headerText + (managerAuth.shellMode=='DEV' ? ' (' + ownerReference.typeName + '.' + prop.key + ')' : '') + '] permite no máximo ' + prop.maxLength.toString() + ' caractere(s).');
                   }
               }
               if (common.isNullOrEmpty(propertyName)) {
                   for (var i = 0; i < ownerReference.VendedorList().length; i++) {
                       var detail = ownerReference.VendedorList()[i];
                       errors = errors.concat(detail.getValidationErrors());
                   }
               }
               return errors;
           }
           ownerReference.getPrimitiveDTO = function(loadDetails) {
               var command = '';
               var properties = metadataInfo[ownerReference.typeName];
               for (var i = 0; i < properties.length; i++) {
                   command += (command === '' ? '' : ', ') + properties[i].key + ': ownerReference.' + properties[i].key;
                   if (properties[i].isDomain && properties[i].key.length > 4) command += (command === '' ? '' : ', ') + common.strLeft(properties[i].key, properties[i].key.length - 4) + ': ownerReference.' + common.strLeft(properties[i].key, properties[i].key.length - 4);
               }
               var result = {};
               eval('result = { ' + command + ' };');
               if (loadDetails) {
                   result.VendedorList = [];
                   var sourceList = ownerReference.VendedorList;
                   if (sourceList && sourceList.length > 0) {
                       for (var i = 0; i < sourceList.length; i++) {
                           if (['U', 'I', 'D'].indexOf(sourceList[i].ChangeState) >= 0) result.VendedorList.push(sourceList[i].getPrimitiveDTO(sourceList[i].ChangeState != 'D'));
                       }
                   }
               }
               return result;
           };
           ownerReference.getAllDetailChanges = function() {
               var result = [];
               var _VendedorList = ownerReference.VendedorList;
               if (_VendedorList && _VendedorList.length > 0) {
                   for (var i = 0; i < _VendedorList.length; i++) {
                       var detail = _VendedorList[i];
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
                    ownerReference[properties[i].key] = originData[properties[i].key];
               }
               if (copyDetails) {
                   if (ownerReference.VendedorList && originData.VendedorList) {
                       var toList = ownerReference.VendedorList;
                       var fromList = originData.VendedorList;
                       for (var idxElem = toList.length - 1; idxElem >= 0; idxElem--) {
                          if (toList[idxElem].ChangeState === 'D') toList.splice(idxElem, 1);
                       }
                       for (var idxElem = toList.length - 1; idxElem >= 0; idxElem--) {
                              if (toList[idxElem].ChangeState !== 'N') {
                                   var fromObj = _.where(fromList, { IdVendedor: toList[idxElem]['IdVendedor'] });
                                   if (fromObj.length > 0) toList[idxElem].copyDataFrom(fromObj[0], true);
                              }
                       }
                   }
               }
               enableChangeTrack = true;
           };
              ownerReference.refreshData = function(noWait, succeeded) {
                 var filterByKey = 'Loja{' + 'IdLoja#==#I' + ownerReference.IdLoja.toString() + '}';
                 return dataContext.getLojaByEntitySearchNoAssociations(filterByKey, 0, 0, false, '', querySucceeded);
                 function querySucceeded(data) {
                    if (data.results.length > 0) {  for (var idx = 0; idx < data.results.length; idx++) { ownerReference.copyDataFrom(data.results[idx]); } }
                    if (succeeded) { succeeded(data); }
                    if (data.results.length == 0) { return; }
                    if (!noWait || ownerReference.atLeastOneDetailLoaded()) { ownerReference.fillDetails(true, '', noWait); }
               }
              }
           ownerReference.isAdded = function() { return ownerReference.ChangeState === 'I'; };
           ownerReference.isDeleted = function() { return ownerReference.ChangeState === 'D'; };
           ownerReference.isModified = function() { return ownerReference.ChangeState === 'U'; };
           ownerReference.isDetached = function() { return false; };
           ownerReference.isUnchanged = function() { return ownerReference.ChangeState === 'N'; };
           ownerReference.setModified = function() { ownerReference.ChangeState = 'U'; };
           ownerReference.setUnchanged = function() { ownerReference.ChangeState = 'N'; };
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
           };
           ownerReference.delete = function() {
               if (ownerReference.setParentAsModified) ownerReference.setParentAsModified();
               if (!common.isNullOrEmpty(ownerReference.VendedorList) && ownerReference.VendedorList.length > 0) {
                  var details = [].concat(ownerReference.VendedorList);
                  for (var idx = 0; idx < details.length; idx++) {
                    details[idx].delete();
                  }
               }
               if (ownerReference.ChangeState == 'I') {
                   if (parent && parent.LojaList) { 
                       var idx = parent.LojaList.indexOf(ownerReference); 
                       if (idx >= 0) parent.LojaList.splice(idx, 1); 
                   }
                   else {
                       var idx = dataBusiness.dataView.indexOf(ownerReference);
                       if (idx >= 0) dataBusiness.dataView.splice(idx, 1);
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
               return dataBusiness.dataView;
           };
           ownerReference.Namespace = 'Linx.Demo.BV.PaiFilha';
           ownerReference.myProperties = [ 'BigIntLoja', 'BitLoja', 'ComboboxLoja', 'DatetimeLoja', 'DecimalLoja', 'GuidLoja', 'IdLoja', 'IntLoja', 'SmallIntLoja', 'StringLoja' ];
           ownerReference.queryRequiredProperties = {  };
           ownerReference.excludedFilters = [];
           ownerReference.getCurrentElements = function() {
               var result = [ ownerReference ];
           if (!common.isNullOrEmpty(ownerReference.currentVendedor)) { result = result.concat(ownerReference.currentVendedor.getCurrentElements()); }
               return result;
           };
           ownerReference.checkForSendingAllRowsToServer = function() {
           };
           ownerReference.GetJsWhereDetailRelationForVendedor = function(customParentRelation) {
       return 'Vendedor{' + (!common.isNullOrEmpty(customParentRelation) ? customParentRelation : 'IdLoja#==#' + ownerReference.serverDataType['IdLoja'] + common.getAbsoluteValue(ownerReference.IdLoja).toString()) + '}';        
           }
           ownerReference.VendedorIsLoaded = false;
           ownerReference.detailsLoaded = function() {
               return ownerReference.VendedorIsLoaded;
           }
           ownerReference.atLeastOneDetailLoaded = function() {
               return ownerReference.VendedorIsLoaded;
           }
           ownerReference.adjustDetailsLoaded = function(value) {
               ownerReference.VendedorIsLoaded = value;
               if (value === false)
                   ownerReference.VendedorList([]);
           }
           ownerReference.fillDetails = function(force, detailName, noWait, callback, customParentRelation) {
              if (typeof force === 'undefined') force = false;
              if (ownerReference.isAdded()) {
                ownerReference.VendedorIsLoaded = true;
              }
              var _VendedorRemoteComplete = false;
              var detachList_Vendedor = [];
              if (force) {
                   if (common.isNullOrEmpty(detailName) || detailName == 'Vendedor') ownerReference.VendedorIsLoaded = false;
                   if ((common.isNullOrEmpty(detailName) || detailName == 'Vendedor') && ownerReference.VendedorList && ownerReference.VendedorList.length > 0) {
                         ownerReference.VendedorList = [];
                   }
              }
        
              if (!ownerReference.VendedorIsLoaded) {
                //Load VendedorList
                if (common.isNullOrEmpty(detailName) || detailName === 'Vendedor') {
                  ownerReference.VendedorIsLoaded = true;
                  _VendedorRemoteComplete = (ownerReference.VendedorList && ownerReference.VendedorList.length > 0);
                  if ((force || !ownerReference.VendedorList || ownerReference.VendedorList.length === 0) && (!common.isNullOrEmpty(common.getAbsoluteValue(ownerReference.IdLoja)))) {
                    var navQuery = 'GetVendedorByEntitySearchNoAssociations?$inlinecount=none';
                    navQuery += '&$orderby=IdVendedor asc';
                    navQuery += '&jEntitySearch=' + ownerReference.GetJsWhereDetailRelationForVendedor(customParentRelation);        ;
                    dataBusiness.showProcessing();
                    httpFactory.httpGet(getDataServiceUrl, navQuery,
                        function (data) {
                           for (var idx = 0; idx < data.results.length; idx++) {
                               initializePOCO(data.results[idx], 'Vendedor'); 
                               data.results[idx].Loja = ownerReference; 
                           } 
                           ownerReference.VendedorList = data.results; 
                           ownerReference.setCurrentDetails('Vendedor');
                           dataBusiness.closeProcessing();
                           _VendedorRemoteComplete = true;
                           if (callback && (!common.isNullOrEmpty(detailName) || (_VendedorRemoteComplete))) { callback(); }
                        }, 
                        function (error) {
                            dataBusiness.closeProcessing();
                            queryFailed(error);
                        });
                  } else { ownerReference.setCurrentDetails('Vendedor'); }
                } else { if (!ownerReference.VendedorIsLoaded && ownerReference.VendedorList && ownerReference.VendedorList.length > 0) { ownerReference.VendedorIsLoaded = true; } }
              } else { 
                if (common.isNullOrEmpty(detailName) || detailName == 'Vendedor') {
                   ownerReference.setCurrentDetails('Vendedor');
                }
                _VendedorRemoteComplete = true;
              }
              if (callback && ((!common.isNullOrEmpty(detailName) && (eval('_' + detailName + 'RemoteComplete && ownerReference.' + detailName + 'IsLoaded') == true)) || (common.isNullOrEmpty(detailName) && (_VendedorRemoteComplete)))) { callback(); }
           };
           //Select first element as a current item of each detail
           ownerReference.setCurrentDetails = function(detailName, clearing) {
              if ((common.isNullOrEmpty(detailName) || detailName === 'Vendedor')) {
                   if (ownerReference.VendedorList.length > 0) { ownerReference.currentVendedor = ownerReference.VendedorList[0]; if (clearing == null || clearing === false) ownerReference.currentVendedor.fillDetails(); }
                   else { ownerReference.currentVendedor = null; }
              }
           };
        //#region Adjust details already loaded for a POCO reference
           if ((typeof ownerReference.VendedorList === 'function') && ownerReference.VendedorList.length > 0) {
                for(var idx = 0; idx < ownerReference.VendedorList.length; idx++) {  VendedorInitializer(ownerReference.VendedorList[idx], true); }
           }
        //#endregion Adjust details already loaded for a POCO reference
        };
        lookUpProperties['Vendedor'] = {};
        var VendedorInitializer = function (ownerReference) {
           ownerReference.RowDataId = getNextSequence('Vendedor');
            //Start Property Definitions
            var _bitVendedor = ownerReference.BitVendedor;
            Object.defineProperty(ownerReference, 'BitVendedor', {
              get: function() { return _bitVendedor; },
              set: function(newValue) { var oldValue = _bitVendedor; _bitVendedor = newValue; if (!entityPropChanged(ownerReference, 'BitVendedor', oldValue, newValue)) { _bitVendedor = oldValue; } }
            });
            var _comboboxVendedor = ownerReference.ComboboxVendedor;
            Object.defineProperty(ownerReference, 'ComboboxVendedor', {
              get: function() { return _comboboxVendedor; },
              set: function(newValue) { var oldValue = _comboboxVendedor; _comboboxVendedor = newValue; if (!entityPropChanged(ownerReference, 'ComboboxVendedor', oldValue, newValue)) { _comboboxVendedor = oldValue; } else { _comboboxVendedorName = (dataDomains.getName('LX_VENDEDOR', newValue)); } }
            });
            var _comboboxVendedorName = ownerReference.ComboboxVendedorName;
            Object.defineProperty(ownerReference, 'ComboboxVendedorName', {
              get: function() { return _comboboxVendedorName; },
              set: function(newValue) { var oldValue = _comboboxVendedorName; _comboboxVendedorName = newValue; if (!entityPropChanged(ownerReference, 'ComboboxVendedorName', oldValue, newValue)) { _comboboxVendedorName = oldValue; } else { _comboboxVendedor = (dataDomains.getId('LX_VENDEDOR', newValue)); } }
            });
            var _datetimeVendedor = (ownerReference.DatetimeVendedor === null ? null : new Date(ownerReference.DatetimeVendedor));
            Object.defineProperty(ownerReference, 'DatetimeVendedor', {
              get: function() { return _datetimeVendedor; },
              set: function(newValue) { var oldValue = _datetimeVendedor; _datetimeVendedor = newValue; if (!entityPropChanged(ownerReference, 'DatetimeVendedor', oldValue, newValue)) { _datetimeVendedor = oldValue; } }
            });
            var _decimalVendedor = ownerReference.DecimalVendedor;
            Object.defineProperty(ownerReference, 'DecimalVendedor', {
              get: function() { return _decimalVendedor; },
              set: function(newValue) { var oldValue = _decimalVendedor; _decimalVendedor = newValue; if (!entityPropChanged(ownerReference, 'DecimalVendedor', oldValue, newValue)) { _decimalVendedor = oldValue; } }
            });
            var _guidVendedor = ownerReference.GuidVendedor;
            Object.defineProperty(ownerReference, 'GuidVendedor', {
              get: function() { return _guidVendedor; },
              set: function(newValue) { var oldValue = _guidVendedor; _guidVendedor = newValue; if (!entityPropChanged(ownerReference, 'GuidVendedor', oldValue, newValue)) { _guidVendedor = oldValue; } }
            });
            var _idLoja = ownerReference.IdLoja;
            Object.defineProperty(ownerReference, 'IdLoja', {
              get: function() { return _idLoja; },
              set: function(newValue) { var oldValue = _idLoja; _idLoja = newValue; if (!entityPropChanged(ownerReference, 'IdLoja', oldValue, newValue)) { _idLoja = oldValue; } }
            });
            var _idVendedor = ownerReference.IdVendedor;
            Object.defineProperty(ownerReference, 'IdVendedor', {
              get: function() { return _idVendedor; },
              set: function(newValue) { var oldValue = _idVendedor; _idVendedor = newValue; if (!entityPropChanged(ownerReference, 'IdVendedor', oldValue, newValue)) { _idVendedor = oldValue; } }
            });
            var _intVendedor = ownerReference.IntVendedor;
            Object.defineProperty(ownerReference, 'IntVendedor', {
              get: function() { return _intVendedor; },
              set: function(newValue) { var oldValue = _intVendedor; _intVendedor = newValue; if (!entityPropChanged(ownerReference, 'IntVendedor', oldValue, newValue)) { _intVendedor = oldValue; } }
            });
            var _smallIntVendedor = ownerReference.SmallIntVendedor;
            Object.defineProperty(ownerReference, 'SmallIntVendedor', {
              get: function() { return _smallIntVendedor; },
              set: function(newValue) { var oldValue = _smallIntVendedor; _smallIntVendedor = newValue; if (!entityPropChanged(ownerReference, 'SmallIntVendedor', oldValue, newValue)) { _smallIntVendedor = oldValue; } }
            });
            var _stringVendedor = ownerReference.StringVendedor;
            Object.defineProperty(ownerReference, 'StringVendedor', {
              get: function() { return _stringVendedor; },
              set: function(newValue) { var oldValue = _stringVendedor; _stringVendedor = newValue; if (!entityPropChanged(ownerReference, 'StringVendedor', oldValue, newValue)) { _stringVendedor = oldValue; } }
            });
            //End Property Definitions
           ownerReference.setRemovedLookupFields = function(removedFields) {
               for (var idxLUp in entitylookUps[ownerReference.typeName]) {
                   var hasKeyValue = false;
                   var luName = entitylookUps[ownerReference.typeName][idxLUp];
                   var luMeta = metadataInfo[luName];
                   for (var idxProp in luMeta) {
                       var prop = luMeta[idxProp];
                       if (!common.isNullOrEmpty(prop.relatedKey) && prop.isPartOfKey) {
                           hasKeyValue = !common.isNullOrEmpty(ownerReference[prop.relatedKey]);
                           break;
                       }
                   }
                   if (hasKeyValue) {
                       for (var idxProp in luMeta) {
                           var prop = luMeta[idxProp];
                           if (!common.isNullOrEmpty(prop.relatedKey) && !prop.isPartOfKey) {
                               removedFields.push(prop.relatedKey);
                           }
                       }
                   }
               }
           }
           ownerReference.getJExpression = function(listFilterRange, removedFields, noDetails) {
               if (ownerReference.excludedFilters && ownerReference.excludedFilters.length > 0) { if (removedFields instanceof Array) removedFields = removedFields.concat(ownerReference.excludedFilters); else removedFields = ownerReference.excludedFilters; }
               ownerReference.setRemovedLookupFields(removedFields);
               var jExpression = common.getJEntityExpression(ownerReference, dialog, listFilterRange, removedFields);
               if (jExpression === 'Error') return jExpression;
               return jExpression;
          };
           ownerReference.createOriginal = function(propertyName, oldValue) {
               ownerReference.original = ownerReference.getPrimitiveDTO();
               if (propertyName) ownerReference.original[propertyName] = oldValue;
           }
           ownerReference.restoreOriginal = function() {
               if (!common.isNullOrEmpty(ownerReference.original)) {
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
           ownerReference.getValidationErrors = function(propertyName) {
               var errors = [];
               if (!dataBusiness.canReportErrors) return errors;
               if (!ownerReference.ChangeState || ['I', 'U'].indexOf(ownerReference.ChangeState) < 0) return errors;
               var properties = metadataInfo[ownerReference.typeName];
               for (var i = 0; i < properties.length; i++) {
                   var prop = properties[i];
                   if (common.isNullOrEmpty(propertyName) || prop.key == propertyName) {
                       if (prop.isRequired === true && !prop.isPartOfKey && common.isNullOrEmpty(ownerReference[prop.key])) errors.push('O campo [' + prop.headerText + (managerAuth.shellMode=='DEV' ? ' (' + ownerReference.typeName + '.' + prop.key + ')' : '') + '] é requerido.');
                       if (prop.validateMaxLength === true && prop.maxLength > 0 && !common.isNullOrEmpty(ownerReference[prop.key]) && ownerReference[prop.key].length > prop.maxLength) errors.push('O campo [' + prop.headerText + (managerAuth.shellMode=='DEV' ? ' (' + ownerReference.typeName + '.' + prop.key + ')' : '') + '] permite no máximo ' + prop.maxLength.toString() + ' caractere(s).');
                   }
               }
               return errors;
           }
           ownerReference.getPrimitiveDTO = function(loadDetails) {
               var command = '';
               var properties = metadataInfo[ownerReference.typeName];
               for (var i = 0; i < properties.length; i++) {
                   command += (command === '' ? '' : ', ') + properties[i].key + ': ownerReference.' + properties[i].key;
                   if (properties[i].isDomain && properties[i].key.length > 4) command += (command === '' ? '' : ', ') + common.strLeft(properties[i].key, properties[i].key.length - 4) + ': ownerReference.' + common.strLeft(properties[i].key, properties[i].key.length - 4);
               }
               var result = {};
               eval('result = { ' + command + ' };');
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
                    ownerReference[properties[i].key] = originData[properties[i].key];
               }
               enableChangeTrack = true;
           };
              ownerReference.refreshData = function(noWait, succeeded) {
                 var filterByKey = 'Vendedor{' + 'IdVendedor#==#I' + ownerReference.IdVendedor.toString() + '}';
                 return dataContext.getVendedorByEntitySearchNoAssociations(filterByKey, 0, 0, false, '', querySucceeded);
                 function querySucceeded(data) {
                    if (data.results.length > 0) {  for (var idx = 0; idx < data.results.length; idx++) { ownerReference.copyDataFrom(data.results[idx]); } }
                    if (succeeded) { succeeded(data); }
                    if (data.results.length == 0) { return; }
                    if (!noWait || ownerReference.atLeastOneDetailLoaded()) { ownerReference.fillDetails(true, '', noWait); }
               }
              }
           ownerReference.isAdded = function() { return ownerReference.ChangeState === 'I'; };
           ownerReference.isDeleted = function() { return ownerReference.ChangeState === 'D'; };
           ownerReference.isModified = function() { return ownerReference.ChangeState === 'U'; };
           ownerReference.isDetached = function() { return false; };
           ownerReference.isUnchanged = function() { return ownerReference.ChangeState === 'N'; };
           ownerReference.setModified = function() { ownerReference.ChangeState = 'U'; };
           ownerReference.setUnchanged = function() { ownerReference.ChangeState = 'N'; };
           ownerReference.serverDataType = [];
           ownerReference.serverDataType['BitVendedor'] = 'B';
           ownerReference.serverDataType['ComboboxVendedor'] = 'Y';
           ownerReference.serverDataType['DatetimeVendedor'] = 'T';
           ownerReference.serverDataType['DecimalVendedor'] = 'D';
           ownerReference.serverDataType['GuidVendedor'] = 'G';
           ownerReference.serverDataType['IdLoja'] = 'I';
           ownerReference.serverDataType['IdVendedor'] = 'I';
           ownerReference.serverDataType['IntVendedor'] = 'I';
           ownerReference.serverDataType['SmallIntVendedor'] = 'H';
           ownerReference.serverDataType['StringVendedor'] = 'S';
           ownerReference.typeName = 'Vendedor';
           ownerReference.isPrimaryKey = function(propertyName) {
               var keys = [ 'IdVendedor' ];
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
           };
           ownerReference.delete = function() {
               if (ownerReference.setParentAsModified) ownerReference.setParentAsModified();
               var parent = ownerReference.Loja;
               if (ownerReference.ChangeState == 'I') {
                   if (parent && parent.VendedorList) { 
                       var idx = parent.VendedorList.indexOf(ownerReference); 
                       if (idx >= 0) parent.VendedorList.splice(idx, 1); 
                   }
                   else {
                       var idx = dataBusiness.dataView.indexOf(ownerReference);
                       if (idx >= 0) dataBusiness.dataView.splice(idx, 1);
                   }
                   delete ownerReference.Loja;
               }
               else {
                   if (ownerReference.ChangeState == 'N') { ownerReference.createOriginal(); }
                   ownerReference.ChangeState = 'D'; // mark for deletion
               }
               if (parent && (typeof parent.setCurrentDetails === 'function') && parent.VendedorList && parent.VendedorList.length == 0) parent.setCurrentDetails('Vendedor');
           };
           ownerReference.setParentAsModified = function() {
           var parent = ownerReference.Loja;
           if (parent) {
               if (parent.isUnchanged()) {
                   parent.setModified(); 
               }
               parent.setParentAsModified();
           }
           };
           ownerReference.getParent = function() {
               return ownerReference.Loja;
           };
           ownerReference.getSelfList = function() {
               var parent = ownerReference.getParent();
               if (!common.isNullOrEmpty(parent)) {
                   return parent.VendedorList;
               } else { return null; }
           };
           ownerReference.Namespace = 'Linx.Demo.BV.PaiFilha';
           ownerReference.myProperties = [ 'BitVendedor', 'ComboboxVendedor', 'DatetimeVendedor', 'DecimalVendedor', 'GuidVendedor', 'IdLoja', 'IdVendedor', 'IntVendedor', 'SmallIntVendedor', 'StringVendedor' ];
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
           ownerReference.fillDetails = function(force, detailName, noWait, callback, customParentRelation) {
              if (typeof force === 'undefined') force = false;
              if (callback) { callback(); }
           };
           //Select first element as a current item of each detail
           ownerReference.setCurrentDetails = function(detailName, clearing) {
           };
        };
        //#endregion Classes Map
        //#region Context Definition
        
        //#region Get LookUps
        
        var getLookUpEstadoByEntitySearch = function (jEntitySearch, propertyName, skip, take, direction, qSucceeded, qFin, qFailed) {
            var query = 'GetLookUpEstadoByEntitySearch?';
            query += 'jEntitySearch=' + jEntitySearch;
            query += '&propertyName=' + propertyName;
            query += '&$inlinecount=allpages&$orderby=' + propertyName + (direction === 'descending' ? ' desc' : ' asc');
        
            if (take > 0)
               query += '&$skip=' + skip.toString() + '&$top=' + take.toString();
        
            return httpFactory.httpGet(getDataServiceUrl, query, localQuerySucceeded, localQueryFailed);
        
            function localQuerySucceeded(data) {
                if (qSucceeded)
                    qSucceeded(data);
                if (qFin)
                    qFin();
            }
        
            function localQueryFailed(error) {
                if (qFin)
                    qFin();
                if (qFailed)
                    qFailed(error);
                else
                    queryFailed(error);
            }
        };
        
        var getLookUpLojaByEntitySearch = function (jEntitySearch, propertyName, skip, take, direction, qSucceeded, qFin, qFailed) {
            var query = 'GetLookUpLojaByEntitySearch?';
            query += 'jEntitySearch=' + jEntitySearch;
            query += '&propertyName=' + propertyName;
            query += '&$inlinecount=allpages&$orderby=' + propertyName + (direction === 'descending' ? ' desc' : ' asc');
        
            if (take > 0)
               query += '&$skip=' + skip.toString() + '&$top=' + take.toString();
        
            return httpFactory.httpGet(getDataServiceUrl, query, localQuerySucceeded, localQueryFailed);
        
            function localQuerySucceeded(data) {
                if (qSucceeded)
                    qSucceeded(data);
                if (qFin)
                    qFin();
            }
        
            function localQueryFailed(error) {
                if (qFin)
                    qFin();
                if (qFailed)
                    qFailed(error);
                else
                    queryFailed(error);
            }
        };
        var lookUpExternalManagers = [];
        //#endregion
        //#region Get KPI Ranges
        //#endregion
        
        //#region Get Combo LookUp
        var getResultsCombo = function (lookupName, fieldName, current, callback) {
            eval('if (current.execute' + lookupName + ') { current.execute' + lookupName + '(fieldName, fieldName, 0, -1, function (hasError, resultsArray, inlineCount) {  if (callback) callback(resultsArray); }); }');
        };
        //#endregion Get Combo LookUp
        
        //#region Get Business Entities
        
        var getBmEntityProperties = function (entityName, parentDataPath, qSucceeded, qFin, qFailed) {
            return httpFactory.httpGet(getDataServiceUrl, 'GetBmEntityProperties?entityName=' + entityName + '&parentDataPath=' + parentDataPath, localQuerySucceeded, localQueryFailed);
        
            function localQuerySucceeded(data) {
                if (qSucceeded)
                    qSucceeded(data);
                if (qFin)
                    qFin();
            }
        
            function localQueryFailed(error) {
                if (qFin)
                    qFin();
                if (qFailed)
                    qFailed(error);
                else
                    queryFailed(error);
            }
        };
        
        var clearCliente = function (idBandeiraRede, complete) {
            enableChangeTrack = false;
            var refCliente = createEmptyCliente();
            var refVenda = createEmptyVenda();
            refCliente.VendaList = [refVenda];
            refCliente.currentVenda = refVenda;
            var refVendaItem = createEmptyVendaItem();
            refVenda.VendaItemList = [refVendaItem];
            refVenda.currentVendaItem = refVendaItem;
            var refVendaAtacado = createEmptyVendaAtacado();
            refCliente.VendaAtacadoList = [refVendaAtacado];
            refCliente.currentVendaAtacado = refVendaAtacado;
            if (complete) complete({ results: [ refCliente ] });
            enableChangeTrack = true;
            return true;
        };
        
        var getCliente = function (jEntitySearch, qSucceeded, qFin, qFailed) {
            var query = 'GetCliente?$inlinecount=none';
            query += '&$orderby=' + order;
            ;
        
            if ((typeof jEntitySearch !== 'undefined') && jEntitySearch !== null)
                query += '&jEntitySearch=' + jEntitySearch;
        
            return httpFactory.httpGet(getDataServiceUrl, query, localQuerySucceeded, localQueryFailed);
        
            function localQuerySucceeded(data) {
                if (qSucceeded)
                    qSucceeded(data);
                if (qFin)
                    qFin();
            }
        
            function localQueryFailed(error) {
                if (qFin)
                    qFin();
                if (qFailed)
                    qFailed(error);
                else
                    queryFailed(error);
            }
        };
        
        var getClienteByEntitySearchNoAssociations = function (jEntitySearch, skip, take, returnInlineCount, orderByDef, qSucceeded, qFin, qFailed) {
            var query = 'GetClienteByEntitySearchNoAssociations?$inlinecount=' + (returnInlineCount ? 'allpages' : 'none');
            query += '&$orderby=' + (common.isNullOrEmpty(orderByDef) ? 'IdCliente asc' : orderByDef);
        
            if ((typeof jEntitySearch !== 'undefined') && jEntitySearch !== null)
                query += '&jEntitySearch=' + jEntitySearch;
            if (take > 0)
               query += '&$skip=' + skip.toString() + '&$top=' + take.toString();
        
            return httpFactory.httpGet(getDataServiceUrl, query, localQuerySucceeded, localQueryFailed);
        
            function localQuerySucceeded(data) {
                if (qSucceeded)
                    qSucceeded(data);
                if (qFin)
                    qFin();
            }
        
            function localQueryFailed(error) {
                if (qFin)
                    qFin();
                if (qFailed)
                    qFailed(error);
                else
                    queryFailed(error);
            }
        };
        
        var clearVenda = function (idBandeiraRede, complete) {
            enableChangeTrack = false;
            var refVenda = createEmptyVenda();
            var refVendaItem = createEmptyVendaItem();
            refVenda.VendaItemList = [refVendaItem];
            refVenda.currentVendaItem = refVendaItem;
            if (complete) complete({ results: [ refVenda ] });
            enableChangeTrack = true;
            return true;
        };
        
        var getVenda = function (jEntitySearch, qSucceeded, qFin, qFailed) {
            var query = 'GetVenda?$inlinecount=none';
            query += '&$orderby=' + order;
            ;
        
            if ((typeof jEntitySearch !== 'undefined') && jEntitySearch !== null)
                query += '&jEntitySearch=' + jEntitySearch;
        
            return httpFactory.httpGet(getDataServiceUrl, query, localQuerySucceeded, localQueryFailed);
        
            function localQuerySucceeded(data) {
                if (qSucceeded)
                    qSucceeded(data);
                if (qFin)
                    qFin();
            }
        
            function localQueryFailed(error) {
                if (qFin)
                    qFin();
                if (qFailed)
                    qFailed(error);
                else
                    queryFailed(error);
            }
        };
        
        var getVendaByEntitySearchNoAssociations = function (jEntitySearch, skip, take, returnInlineCount, orderByDef, qSucceeded, qFin, qFailed) {
            var query = 'GetVendaByEntitySearchNoAssociations?$inlinecount=' + (returnInlineCount ? 'allpages' : 'none');
            query += '&$orderby=' + (common.isNullOrEmpty(orderByDef) ? 'IdVenda asc' : orderByDef);
        
            if ((typeof jEntitySearch !== 'undefined') && jEntitySearch !== null)
                query += '&jEntitySearch=' + jEntitySearch;
            if (take > 0)
               query += '&$skip=' + skip.toString() + '&$top=' + take.toString();
        
            return httpFactory.httpGet(getDataServiceUrl, query, localQuerySucceeded, localQueryFailed);
        
            function localQuerySucceeded(data) {
                if (qSucceeded)
                    qSucceeded(data);
                if (qFin)
                    qFin();
            }
        
            function localQueryFailed(error) {
                if (qFin)
                    qFin();
                if (qFailed)
                    qFailed(error);
                else
                    queryFailed(error);
            }
        };
        
        var clearVendaAtacado = function (idBandeiraRede, complete) {
            enableChangeTrack = false;
            var refVendaAtacado = createEmptyVendaAtacado();
            if (complete) complete({ results: [ refVendaAtacado ] });
            enableChangeTrack = true;
            return true;
        };
        
        var getVendaAtacado = function (jEntitySearch, qSucceeded, qFin, qFailed) {
            var query = 'GetVendaAtacado?$inlinecount=none';
            query += '&$orderby=' + order;
            ;
        
            if ((typeof jEntitySearch !== 'undefined') && jEntitySearch !== null)
                query += '&jEntitySearch=' + jEntitySearch;
        
            return httpFactory.httpGet(getDataServiceUrl, query, localQuerySucceeded, localQueryFailed);
        
            function localQuerySucceeded(data) {
                if (qSucceeded)
                    qSucceeded(data);
                if (qFin)
                    qFin();
            }
        
            function localQueryFailed(error) {
                if (qFin)
                    qFin();
                if (qFailed)
                    qFailed(error);
                else
                    queryFailed(error);
            }
        };
        
        var getVendaAtacadoByEntitySearchNoAssociations = function (jEntitySearch, skip, take, returnInlineCount, orderByDef, qSucceeded, qFin, qFailed) {
            var query = 'GetVendaAtacadoByEntitySearchNoAssociations?$inlinecount=' + (returnInlineCount ? 'allpages' : 'none');
            query += '&$orderby=' + (common.isNullOrEmpty(orderByDef) ? 'IdVendaAtacado asc' : orderByDef);
        
            if ((typeof jEntitySearch !== 'undefined') && jEntitySearch !== null)
                query += '&jEntitySearch=' + jEntitySearch;
            if (take > 0)
               query += '&$skip=' + skip.toString() + '&$top=' + take.toString();
        
            return httpFactory.httpGet(getDataServiceUrl, query, localQuerySucceeded, localQueryFailed);
        
            function localQuerySucceeded(data) {
                if (qSucceeded)
                    qSucceeded(data);
                if (qFin)
                    qFin();
            }
        
            function localQueryFailed(error) {
                if (qFin)
                    qFin();
                if (qFailed)
                    qFailed(error);
                else
                    queryFailed(error);
            }
        };
        
        var clearVendaItem = function (idBandeiraRede, complete) {
            enableChangeTrack = false;
            var refVendaItem = createEmptyVendaItem();
            if (complete) complete({ results: [ refVendaItem ] });
            enableChangeTrack = true;
            return true;
        };
        
        var getVendaItem = function (jEntitySearch, qSucceeded, qFin, qFailed) {
            var query = 'GetVendaItem?$inlinecount=none';
            query += '&$orderby=' + order;
            ;
        
            if ((typeof jEntitySearch !== 'undefined') && jEntitySearch !== null)
                query += '&jEntitySearch=' + jEntitySearch;
        
            return httpFactory.httpGet(getDataServiceUrl, query, localQuerySucceeded, localQueryFailed);
        
            function localQuerySucceeded(data) {
                if (qSucceeded)
                    qSucceeded(data);
                if (qFin)
                    qFin();
            }
        
            function localQueryFailed(error) {
                if (qFin)
                    qFin();
                if (qFailed)
                    qFailed(error);
                else
                    queryFailed(error);
            }
        };
        
        var getVendaItemByEntitySearchNoAssociations = function (jEntitySearch, skip, take, returnInlineCount, orderByDef, qSucceeded, qFin, qFailed) {
            var query = 'GetVendaItemByEntitySearchNoAssociations?$inlinecount=' + (returnInlineCount ? 'allpages' : 'none');
            query += '&$orderby=' + (common.isNullOrEmpty(orderByDef) ? 'IdVendaItem asc' : orderByDef);
        
            if ((typeof jEntitySearch !== 'undefined') && jEntitySearch !== null)
                query += '&jEntitySearch=' + jEntitySearch;
            if (take > 0)
               query += '&$skip=' + skip.toString() + '&$top=' + take.toString();
        
            return httpFactory.httpGet(getDataServiceUrl, query, localQuerySucceeded, localQueryFailed);
        
            function localQuerySucceeded(data) {
                if (qSucceeded)
                    qSucceeded(data);
                if (qFin)
                    qFin();
            }
        
            function localQueryFailed(error) {
                if (qFin)
                    qFin();
                if (qFailed)
                    qFailed(error);
                else
                    queryFailed(error);
            }
        };
        
        var clearLoja = function (idBandeiraRede, complete) {
            enableChangeTrack = false;
            var refLoja = createEmptyLoja();
            var refVendedor = createEmptyVendedor();
            refLoja.VendedorList = [refVendedor];
            refLoja.currentVendedor = refVendedor;
            if (complete) complete({ results: [ refLoja ] });
            enableChangeTrack = true;
            return true;
        };
        
        var getLoja = function (jEntitySearch, qSucceeded, qFin, qFailed) {
            var query = 'GetLoja?$inlinecount=none';
            query += '&$orderby=' + order;
            ;
        
            if ((typeof jEntitySearch !== 'undefined') && jEntitySearch !== null)
                query += '&jEntitySearch=' + jEntitySearch;
        
            return httpFactory.httpGet(getDataServiceUrl, query, localQuerySucceeded, localQueryFailed);
        
            function localQuerySucceeded(data) {
                if (qSucceeded)
                    qSucceeded(data);
                if (qFin)
                    qFin();
            }
        
            function localQueryFailed(error) {
                if (qFin)
                    qFin();
                if (qFailed)
                    qFailed(error);
                else
                    queryFailed(error);
            }
        };
        
        var getLojaByEntitySearchNoAssociations = function (jEntitySearch, skip, take, returnInlineCount, orderByDef, qSucceeded, qFin, qFailed) {
            var query = 'GetLojaByEntitySearchNoAssociations?$inlinecount=' + (returnInlineCount ? 'allpages' : 'none');
            query += '&$orderby=' + (common.isNullOrEmpty(orderByDef) ? 'IdLoja asc' : orderByDef);
        
            if ((typeof jEntitySearch !== 'undefined') && jEntitySearch !== null)
                query += '&jEntitySearch=' + jEntitySearch;
            if (take > 0)
               query += '&$skip=' + skip.toString() + '&$top=' + take.toString();
        
            return httpFactory.httpGet(getDataServiceUrl, query, localQuerySucceeded, localQueryFailed);
        
            function localQuerySucceeded(data) {
                if (qSucceeded)
                    qSucceeded(data);
                if (qFin)
                    qFin();
            }
        
            function localQueryFailed(error) {
                if (qFin)
                    qFin();
                if (qFailed)
                    qFailed(error);
                else
                    queryFailed(error);
            }
        };
        
        var clearVendedor = function (idBandeiraRede, complete) {
            enableChangeTrack = false;
            var refVendedor = createEmptyVendedor();
            if (complete) complete({ results: [ refVendedor ] });
            enableChangeTrack = true;
            return true;
        };
        
        var getVendedor = function (jEntitySearch, qSucceeded, qFin, qFailed) {
            var query = 'GetVendedor?$inlinecount=none';
            query += '&$orderby=' + order;
            ;
        
            if ((typeof jEntitySearch !== 'undefined') && jEntitySearch !== null)
                query += '&jEntitySearch=' + jEntitySearch;
        
            return httpFactory.httpGet(getDataServiceUrl, query, localQuerySucceeded, localQueryFailed);
        
            function localQuerySucceeded(data) {
                if (qSucceeded)
                    qSucceeded(data);
                if (qFin)
                    qFin();
            }
        
            function localQueryFailed(error) {
                if (qFin)
                    qFin();
                if (qFailed)
                    qFailed(error);
                else
                    queryFailed(error);
            }
        };
        
        var getVendedorByEntitySearchNoAssociations = function (jEntitySearch, skip, take, returnInlineCount, orderByDef, qSucceeded, qFin, qFailed) {
            var query = 'GetVendedorByEntitySearchNoAssociations?$inlinecount=' + (returnInlineCount ? 'allpages' : 'none');
            query += '&$orderby=' + (common.isNullOrEmpty(orderByDef) ? 'IdVendedor asc' : orderByDef);
        
            if ((typeof jEntitySearch !== 'undefined') && jEntitySearch !== null)
                query += '&jEntitySearch=' + jEntitySearch;
            if (take > 0)
               query += '&$skip=' + skip.toString() + '&$top=' + take.toString();
        
            return httpFactory.httpGet(getDataServiceUrl, query, localQuerySucceeded, localQueryFailed);
        
            function localQuerySucceeded(data) {
                if (qSucceeded)
                    qSucceeded(data);
                if (qFin)
                    qFin();
            }
        
            function localQueryFailed(error) {
                if (qFin)
                    qFin();
                if (qFailed)
                    qFailed(error);
                else
                    queryFailed(error);
            }
        };
        //#endregion
        
        var cancelChanges = function(dataForUndo) {
            if (dataForUndo && dataForUndo.length > 0) {
                dataForUndo.forEach(function(e) { e.restoreOriginal(); } ); 
            }
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
                        if (common.strLeft(selectedElement.key, 3) === 'Cod' || common.strLeft(selectedElement.key, 2) === 'Id' || common.strLeft(selectedElement.key, 6) === 'Numero' || common.strLeft(selectedElement.key, 6) === 'Number') {
                            result.push(selectedElement);
                        }
                    }
                }
                for (var i = 0; i < viewInfoElements.length; i++) {
                    var selectedElement = viewInfoElements[i];
                    if (!selectedElement.hidden && (selectedElement.dataType === 'string')) {
                        if (common.strLeft(selectedElement.key, 4) === 'Nome' || common.strLeft(selectedElement.key, 4) === 'Name' || common.strLeft(selectedElement.key, 4) === 'Desc' || common.strLeft(selectedElement.key, 6) === 'Titulo' || common.strLeft(selectedElement.key, 5) === 'Title') {
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
        
        var hasValidationErrors = function(savingData) {
            for (var idx = 0; idx < savingData.length; idx++) {
                var entity = savingData[idx];
                if (entity.ChangeState && entity.getValidationErrors && ['I', 'U'].indexOf(entity.ChangeState) >=0) {
                   var errors = entity.getValidationErrors();
                   if (errors.length > 0) {
                        dialog.showAlert('Campos obrigatórios não estão preenchidos.', errors);
                        return true;
                    }
                }
            }
            return false;
        };
        
        var saveChanges = function(saveSucceeded, saveFailed, fin) {
            var dataForSaving = JSON.stringify(_.map(dataBusiness.getDataForSaving(), function(entity){ return entity.getPrimitiveDTO(entity.ChangeState != 'D'); }));
            return httpFactory.httpPost(getDataServiceUrl, 'Save' + dataBusiness.rootDataTypeName,
               dataForSaving,
               function (response) {
                      success(response);
               },
               function (error) {
                      failed({ message: error.message });
               }
            );
        
            function success(result) {
                if (fin) fin();
                if (result.length > 0) {
                   for (var idx = 0; idx < result.length; idx++) { dataContext.initializePOCO(result[idx], dataBusiness.rootDataTypeName); }
                }
                if (saveSucceeded)
                    saveSucceeded(result);
            }
        
            function failed(error) {
                if (fin) fin();
                if (saveFailed)
                    saveFailed(error);
                var msg = (!common.isNullOrEmpty(error.message) ? error.message : error.Message);
                dialog.showAlert('Falha ao salvar informações.', [ msg ]);
                error.message = msg;
                throw error;
            }
        };
        
        var createEmptyCliente = function() {
            var entity = {};
            for (var idx = 0; idx < metadataInfo['Cliente'].length; idx++) { 
                var prop = metadataInfo['Cliente'][idx]; 
                entity[prop.key] = prop.defaultValue;
            }
            dataContext.initializePOCO(entity, 'Cliente');
            return entity;
        }
        
        var createCliente = function() {
            //Create entity instance
            enableChangeTrack = false;
            var defaultVals = { IdCliente: (-1 * getSequence('Cliente')), BitCliente: false };
            var entityType = 'Cliente';
            var entity = {};
            for (var idx = 0; idx < metadataInfo['Cliente'].length; idx++) { 
                var prop = metadataInfo['Cliente'][idx]; 
                if ((typeof defaultVals[prop.key]) !== 'undefined') entity[prop.key] = defaultVals[prop.key];
                else  entity[prop.key] = prop.defaultValue;
            }
            dataContext.initializePOCO(entity, 'Cliente');
            entity.setDefaults();
            entity.ChangeState = 'I';
            enableChangeTrack = true;
            return entity;
        };
        
        var createEmptyVenda = function() {
            var entity = {};
            for (var idx = 0; idx < metadataInfo['Venda'].length; idx++) { 
                var prop = metadataInfo['Venda'][idx]; 
                entity[prop.key] = prop.defaultValue;
            }
            dataContext.initializePOCO(entity, 'Venda');
            return entity;
        }
        
        var createVenda = function(parent) {
            //Create entity instance
            enableChangeTrack = false;
            var defaultVals = { Cliente: parent, IdVenda: (-1 * getSequence('Venda')), BitVenda: false };
            var entityType = 'Venda';
            var entity = {};
            for (var idx = 0; idx < metadataInfo['Venda'].length; idx++) { 
                var prop = metadataInfo['Venda'][idx]; 
                if ((typeof defaultVals[prop.key]) !== 'undefined') entity[prop.key] = defaultVals[prop.key];
                else  entity[prop.key] = prop.defaultValue;
            }
            dataContext.initializePOCO(entity, 'Venda');
            entity.Cliente = parent;
            entity.IdCliente = parent.IdCliente;
            entity.setDefaults();
            entity.ChangeState = 'I';
            if (noCurrent !== true) pparent.currentVenda = entity;
            if (parent && parent.VendaList) parent.VendaList.push(entity);
            if (parent && (typeof parent.setCurrentDetails === 'function') && parent.VendaList && parent.VendaList.length == 0) parent.setCurrentDetails('Venda');
            if (entity.setParentAsModified) entity.setParentAsModified();
            enableChangeTrack = true;
            return entity;
        };
        
        var createEmptyVendaAtacado = function() {
            var entity = {};
            for (var idx = 0; idx < metadataInfo['VendaAtacado'].length; idx++) { 
                var prop = metadataInfo['VendaAtacado'][idx]; 
                entity[prop.key] = prop.defaultValue;
            }
            dataContext.initializePOCO(entity, 'VendaAtacado');
            return entity;
        }
        
        var createVendaAtacado = function(parent) {
            //Create entity instance
            enableChangeTrack = false;
            var defaultVals = { Cliente: parent, IdVendaAtacado: (-1 * getSequence('VendaAtacado')), BitVendaAtacado: false };
            var entityType = 'VendaAtacado';
            var entity = {};
            for (var idx = 0; idx < metadataInfo['VendaAtacado'].length; idx++) { 
                var prop = metadataInfo['VendaAtacado'][idx]; 
                if ((typeof defaultVals[prop.key]) !== 'undefined') entity[prop.key] = defaultVals[prop.key];
                else  entity[prop.key] = prop.defaultValue;
            }
            dataContext.initializePOCO(entity, 'VendaAtacado');
            entity.Cliente = parent;
            entity.IdCliente = parent.IdCliente;
            entity.setDefaults();
            entity.ChangeState = 'I';
            if (noCurrent !== true) pparent.currentVendaAtacado = entity;
            if (parent && parent.VendaAtacadoList) parent.VendaAtacadoList.push(entity);
            if (parent && (typeof parent.setCurrentDetails === 'function') && parent.VendaAtacadoList && parent.VendaAtacadoList.length == 0) parent.setCurrentDetails('VendaAtacado');
            if (entity.setParentAsModified) entity.setParentAsModified();
            enableChangeTrack = true;
            return entity;
        };
        
        var createEmptyVendaItem = function() {
            var entity = {};
            for (var idx = 0; idx < metadataInfo['VendaItem'].length; idx++) { 
                var prop = metadataInfo['VendaItem'][idx]; 
                entity[prop.key] = prop.defaultValue;
            }
            dataContext.initializePOCO(entity, 'VendaItem');
            return entity;
        }
        
        var createVendaItem = function(parent) {
            //Create entity instance
            enableChangeTrack = false;
            var defaultVals = { Venda: parent, IdVendaItem: (-1 * getSequence('VendaItem')), BitVendaItem: false };
            var entityType = 'VendaItem';
            var entity = {};
            for (var idx = 0; idx < metadataInfo['VendaItem'].length; idx++) { 
                var prop = metadataInfo['VendaItem'][idx]; 
                if ((typeof defaultVals[prop.key]) !== 'undefined') entity[prop.key] = defaultVals[prop.key];
                else  entity[prop.key] = prop.defaultValue;
            }
            dataContext.initializePOCO(entity, 'VendaItem');
            entity.Venda = parent;
            entity.IdVenda = parent.IdVenda;
            entity.setDefaults();
            entity.ChangeState = 'I';
            if (noCurrent !== true) pparent.currentVendaItem = entity;
            if (parent && parent.VendaItemList) parent.VendaItemList.push(entity);
            if (parent && (typeof parent.setCurrentDetails === 'function') && parent.VendaItemList && parent.VendaItemList.length == 0) parent.setCurrentDetails('VendaItem');
            if (entity.setParentAsModified) entity.setParentAsModified();
            enableChangeTrack = true;
            return entity;
        };
        
        var createEmptyLoja = function() {
            var entity = {};
            for (var idx = 0; idx < metadataInfo['Loja'].length; idx++) { 
                var prop = metadataInfo['Loja'][idx]; 
                entity[prop.key] = prop.defaultValue;
            }
            dataContext.initializePOCO(entity, 'Loja');
            return entity;
        }
        
        var createLoja = function() {
            //Create entity instance
            enableChangeTrack = false;
            var defaultVals = { IdLoja: (-1 * getSequence('Loja')), BitLoja: false };
            var entityType = 'Loja';
            var entity = {};
            for (var idx = 0; idx < metadataInfo['Loja'].length; idx++) { 
                var prop = metadataInfo['Loja'][idx]; 
                if ((typeof defaultVals[prop.key]) !== 'undefined') entity[prop.key] = defaultVals[prop.key];
                else  entity[prop.key] = prop.defaultValue;
            }
            dataContext.initializePOCO(entity, 'Loja');
            entity.setDefaults();
            entity.ChangeState = 'I';
            enableChangeTrack = true;
            return entity;
        };
        
        var createEmptyVendedor = function() {
            var entity = {};
            for (var idx = 0; idx < metadataInfo['Vendedor'].length; idx++) { 
                var prop = metadataInfo['Vendedor'][idx]; 
                entity[prop.key] = prop.defaultValue;
            }
            dataContext.initializePOCO(entity, 'Vendedor');
            return entity;
        }
        
        var createVendedor = function(parent) {
            //Create entity instance
            enableChangeTrack = false;
            var defaultVals = { Loja: parent, IdVendedor: (-1 * getSequence('Vendedor')), BitVendedor: false };
            var entityType = 'Vendedor';
            var entity = {};
            for (var idx = 0; idx < metadataInfo['Vendedor'].length; idx++) { 
                var prop = metadataInfo['Vendedor'][idx]; 
                if ((typeof defaultVals[prop.key]) !== 'undefined') entity[prop.key] = defaultVals[prop.key];
                else  entity[prop.key] = prop.defaultValue;
            }
            dataContext.initializePOCO(entity, 'Vendedor');
            entity.Loja = parent;
            entity.IdLoja = parent.IdLoja;
            entity.setDefaults();
            entity.ChangeState = 'I';
            if (noCurrent !== true) pparent.currentVendedor = entity;
            if (parent && parent.VendedorList) parent.VendedorList.push(entity);
            if (parent && (typeof parent.setCurrentDetails === 'function') && parent.VendedorList && parent.VendedorList.length == 0) parent.setCurrentDetails('Vendedor');
            if (entity.setParentAsModified) entity.setParentAsModified();
            enableChangeTrack = true;
            return entity;
        };
        
        var deleteEntity = function (entity) {
            entity.delete();
        };
        
        var executeQuery = function (getMethod, jEntitySearch, order, skip, take, noTracking, qSucceeded, qFin, qFailed) {
            var query = getMethod + '?$inlinecount=allpages';
            if (!common.isNullOrEmpty(query))
               query += '&$orderby=' + order;
        
            if (take > 0)
               query += '&$top=' + take.toString() + '&$skip=' + skip.toString();
        
            if ((typeof jEntitySearch !== 'undefined') && jEntitySearch !== null)
                query += '&jEntitySearch=' + jEntitySearch;
        
            return httpFactory.httpGet(getDataServiceUrl, query, localQuerySucceeded, localQueryFailed);
        
            function localQuerySucceeded(data) {
                if (qSucceeded)
                    qSucceeded(data);
                if (qFin)
                    qFin();
            }
        
            function localQueryFailed(error) {
                if (qFin)
                    qFin();
                if (qFailed)
                    qFailed(error);
                else
                    queryFailed(error);
            }
        };
        var exportToExcel = function(entityName, jEntitySearch, translatedJEntitySearch, complete, columnsVisible) {
            var info = jQuery.grep(dataExportInfo[dataBusiness.rootDataTypeName], function (item, i) { return (item.name === entityName); });
            if (info == null || info.length === 0) {
                dialog.showMessage('Exportação não permitida!', 'Alerta', ['Ok']);
                return;
            }
            httpFactory.httpPost(getDataServiceUrl, getServiceAddress(info[0].actionExport),
               JSON.stringify([jEntitySearch, translatedJEntitySearch, columnsVisible]),
               function (response) {
                      saveExcelBlob(entityName + '.xlsx', response);
                      if(complete) complete();
               },
               function (error) {
                      dialog.showAlert(error.message, 'Erro na exportação');
                      if(complete) complete();
               }
            );
        };
        var exportReportDataSource = function(complete) {
            httpFactory.httpGet(getDataServiceUrl, getServiceAddress("LinxDemoPaiFilha/GetReportDataSource"),
               function (response) {
                      saveExcelBlob('datasource.ldsx', response);
                      if(complete) complete();
               },
               function (error) {
                      dialog.showAlert(error.message, 'Erro na exportação do data source');
                      if(complete) complete();
               }
            );
        };
        var exportTemplateReport = function(reportPath, complete) {
            getServiceAddress("LinxDemoPaiFilha/GetTemplateReport?reportPath=" + reportPath,
               function (response) {
                         saveExcelBlob(reportPath + '.lrtx', response);
                         if(complete) complete();
               },
               function (jqXHR, textStatus, errorThrown) {
                         alert('Erro na exportação do data source');
                         if(complete) complete();
               }
            );
        };
        var exportToReport = function(reportName, entityName, jEntitySearch, translatedJEntitySearch, complete, columnsVisible, exportMedia) {
            var info = jQuery.grep(dataExportInfo[dataBusiness.rootDataTypeName], function (item, i) { return (item.name === entityName);});
            if (info == null || info.length === 0) {
                dialog.showMessage('Erro na exportação', 'Alerta', ['Ok']);
                return;
            }
            httpFactory.httpPost(getDataServiceUrl, getServiceAddress(info[0].actionReport),
               JSON.stringify([ reportName, jEntitySearch, translatedJEntitySearch, columnsVisible, getServiceAddress(''), exportMedia ]),
               function (response) {
                      saveExcelBlob(entityName + '.lrtx', response);
                      if(complete) complete();
               },
               function (error) {
                      dialog.showAlert(error.message, 'Erro na exportação');
                      if(complete) complete();
               }
            );
        };
        
        
        function acceptChanges() {
        }
        
        //#region Internal methods
        
        function queryFailed(error) {
            dataBusiness.closeProcessing();
        }
        
        function loadParameters() {
         dataParameters.isLoaded = true;
        }
        
        //#endregion Internal methods
        var dataBusiness = null;
        var extendedDataBusiness = null;
        var dataContext = {
                dataForUpdate: '',
                acceptChanges: acceptChanges,
                getPivotLayouts: getPivotLayouts,
                getSelectedLayoutContent: getSelectedLayoutContent,
                getServiceAddress: getServiceAddress,
                getDataFeedUrl: getDataFeedUrl,
                getDataServiceUrl: getDataServiceUrl,
                setServiceBusUrl: setServiceBusUrl,
                initializePOCO: initializePOCO,
                shellManagerService: shellManagerService,
                hasDataFeed: true,
                getNewGuid: getNewGuid,
                metadataInfo: metadataInfo,
                dataExportInfo: dataExportInfo,
                entityNames: entityNames,
                lookUpNames: lookUpNames,
                lookUpProperties: lookUpProperties,
                cancelChanges: cancelChanges,
                saveChanges: saveChanges,
                hasValidationErrors: hasValidationErrors,
                getEntityProperty: getEntityProperty,
                getViewInfo: getViewInfo,
                createCliente: createCliente,
                createVenda: createVenda,
                createVendaAtacado: createVendaAtacado,
                createVendaItem: createVendaItem,
                createLoja: createLoja,
                createVendedor: createVendedor,
                deleteEntity: deleteEntity,
                executeQuery: executeQuery,
                sharedData: [],
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
                setCurrentDataBusiness: function(curDataBusiness) { dataBusiness = curDataBusiness; extendedDataBusiness = curDataBusiness.getExtendedDataBusiness(); },
                getLookUpEstadoByEntitySearch: getLookUpEstadoByEntitySearch,
        getLookUpLojaByEntitySearch: getLookUpLojaByEntitySearch,
                getBmEntityProperties: getBmEntityProperties,
                clearCliente: clearCliente,
                getCliente: getCliente,
                getClienteByEntitySearchNoAssociations: getClienteByEntitySearchNoAssociations,
                clearVenda: clearVenda,
                getVenda: getVenda,
                getVendaByEntitySearchNoAssociations: getVendaByEntitySearchNoAssociations,
                clearVendaAtacado: clearVendaAtacado,
                getVendaAtacado: getVendaAtacado,
                getVendaAtacadoByEntitySearchNoAssociations: getVendaAtacadoByEntitySearchNoAssociations,
                clearVendaItem: clearVendaItem,
                getVendaItem: getVendaItem,
                getVendaItemByEntitySearchNoAssociations: getVendaItemByEntitySearchNoAssociations,
                clearLoja: clearLoja,
                getLoja: getLoja,
                getLojaByEntitySearchNoAssociations: getLojaByEntitySearchNoAssociations,
                clearVendedor: clearVendedor,
                getVendedor: getVendedor,
                getVendedorByEntitySearchNoAssociations: getVendedorByEntitySearchNoAssociations
        };
        loadParameters();
        return dataContext;
        //#endregion Context Definition
    };
    
    module.exports = function(appModule) {
        appModule.service(name, dependencies.concat(serviceAPI));
    };
    /* jshint ignore:end */
