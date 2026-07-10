define([], function () {


    var dataDomain = {
        domains: [],
        registerDomains: function () {

            dataDomain.domains['TipoFiltro'] = [];
            dataDomain.domains['TipoFiltro'][0] = { id: '1', name: 'Filtro BV' };
            dataDomain.domains['TipoFiltro'][1] = { id: '2', name: 'Filtro BM' };

            dataDomain.domains['FilterOperator'] = [];
            dataDomain.domains['FilterOperator'][0] = { id: 'BETWEEN', name: 'Between', hasValue: true, availableTypes: '', allowedInPredefined: false };
            dataDomain.domains['FilterOperator'][1] = { id: '>', name: '>', hasValue: true, availableTypes: 'LHIYDTFSC', allowedInPredefined: false };
            dataDomain.domains['FilterOperator'][2] = { id: '>=', name: '>=', hasValue: true, availableTypes: 'LHIYDTFSC', allowedInPredefined: false };
            dataDomain.domains['FilterOperator'][3] = { id: 'In', name: 'In', hasValue: true, availableTypes: 'LHIYDSCFG', allowedInPredefined: false };
            dataDomain.domains['FilterOperator'][4] = { id: '==', name: '=', hasValue: true, availableTypes: 'LHIYDSCBGFT', allowedInPredefined: true };
            dataDomain.domains['FilterOperator'][5] = { id: '!= null', name: 'Not Null', hasValue: false, availableTypes: 'LHIYDSCTF', allowedInPredefined: false };
            dataDomain.domains['FilterOperator'][6] = { id: '== null', name: 'Null', hasValue: false, availableTypes: 'LHIYDSCTF', allowedInPredefined: false };
            dataDomain.domains['FilterOperator'][7] = { id: '<', name: '<', hasValue: true, availableTypes: 'LHIYDTFSC', allowedInPredefined: false };
            dataDomain.domains['FilterOperator'][8] = { id: '<=', name: '<=', hasValue: true, availableTypes: 'LHIYDTFSC', allowedInPredefined: false };
            dataDomain.domains['FilterOperator'][9] = { id: 'Like', name: 'Like', hasValue: true, availableTypes: 'SC', allowedInPredefined: false };
            dataDomain.domains['FilterOperator'][10] = { id: 'NOT BETWEEN', name: 'Not Between', hasValue: true, availableTypes: '', allowedInPredefined: false };
            dataDomain.domains['FilterOperator'][11] = { id: '!=', name: '!=', hasValue: true, availableTypes: 'LHIYDSCTBGF', allowedInPredefined: false };
            dataDomain.domains['FilterOperator'][12] = { id: '!In', name: 'Not In', hasValue: true, availableTypes: 'LHIYDSCFG', allowedInPredefined: false };
            dataDomain.domains['FilterOperator'][13] = { id: '!Like', name: 'Not Like', hasValue: true, availableTypes: 'SC', allowedInPredefined: false };
            dataDomain.domains['FilterOperator'][14] = { id: 'Empty', name: 'Vazio', hasValue: false, availableTypes: 'SC', allowedInPredefined: false };

            dataDomain.domains['FilterCondition'] = [];
            dataDomain.domains['FilterCondition'][0] = { id: '&&', name: 'And' };
            dataDomain.domains['FilterCondition'][1] = { id: '!', name: 'Not' };
            dataDomain.domains['FilterCondition'][2] = { id: '||', name: 'Or' };
        },
        getItems: function (domainName) {
            var items = dataDomain.domains[domainName];
            return (items ? items : []);
        },
        getName: function (domainName, value) {
            var name = '';
            var domainItems = this.getItems(domainName);
            if (domainItems) {
                for (var i in domainItems) {
                    if (domainItems[i].id === value) {
                        name = domainItems[i].name;
                        break;
                    }
                }
            }
            return name;
        },
        getId: function (domainName, name) {
            var id = '';
            var domainItems = this.getItems(domainName);
            if (domainItems) {
                for (var i in domainItems) {
                    if (domainItems[i].name === name) {
                        id = domainItems[i].id;
                        break;
                    }
                }
            }
            return id;
        }
    };

    dataDomain.registerDomains();
    return dataDomain;

});