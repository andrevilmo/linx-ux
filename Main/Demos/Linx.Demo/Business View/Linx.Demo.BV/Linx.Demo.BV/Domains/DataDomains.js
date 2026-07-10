							


define(['managers/__auth'], function (managerAuth) {


var dataDomain = {
    domains: [],    
    registerDomains: function () {
		var self = this;

		dataDomain.domains['LX_COMBOBOX_LOJA'] = [];
		dataDomain.domains['LX_COMBOBOX_LOJA'][0] = { id: 1, name: 'LOJA1' };
		dataDomain.domains['LX_COMBOBOX_LOJA'][1] = { id: 2, name: 'LOJA2' };
		dataDomain.domains['LX_COMBOBOX_LOJA'][2] = { id: 3, name: 'LOJA3' };
		dataDomain.domains['LX_COMBOBOX_LOJA'][3] = { id: 4, name: 'LOJA4' };
		dataDomain.domains['LX_VENDA_ITEM'] = [];
		dataDomain.domains['LX_VENDA_ITEM'][0] = { id: 1, name: 'VENDA_ITEM1' };
		dataDomain.domains['LX_VENDA_ITEM'][1] = { id: 2, name: 'VENDA_ITEM2' };
		dataDomain.domains['LX_VENDA_ITEM'][2] = { id: 3, name: 'VENDA_ITEM3' };
		dataDomain.domains['LX_VENDA_ITEM'][3] = { id: 4, name: 'VENDA_ITEM4' };
		dataDomain.domains['LX_COMBOBOX_CIDADE'] = [];
		dataDomain.domains['LX_COMBOBOX_CIDADE'][0] = { id: 1, name: 'CIDADE1' };
		dataDomain.domains['LX_COMBOBOX_CIDADE'][1] = { id: 2, name: 'CIDADE2' };
		dataDomain.domains['LX_COMBOBOX_CIDADE'][2] = { id: 3, name: 'CIDADE3' };
		dataDomain.domains['LX_COMBOBOX_ESTADO'] = [];
		dataDomain.domains['LX_COMBOBOX_ESTADO'][0] = { id: 1, name: 'ESTADO1' };
		dataDomain.domains['LX_COMBOBOX_ESTADO'][1] = { id: 2, name: 'ESTADO2' };
		dataDomain.domains['LX_COMBOBOX_ESTADO'][2] = { id: 3, name: 'ESTADO3' };
		dataDomain.domains['LX_COMBOBOX_ESTADO'][3] = { id: 4, name: 'ESTADO4' };
		dataDomain.domains['LX_COMBOBOX_PAIS'] = [];
		dataDomain.domains['LX_COMBOBOX_PAIS'][0] = { id: 1, name: 'PAIS1' };
		dataDomain.domains['LX_COMBOBOX_PAIS'][1] = { id: 2, name: 'PAIS2' };
		dataDomain.domains['LX_COMBOBOX_PAIS'][2] = { id: 3, name: 'PAIS3' };
		dataDomain.domains['LX_COMBOBOX_MARCA'] = [];
		dataDomain.domains['LX_COMBOBOX_MARCA'][0] = { id: 1, name: 'MARCA1' };
		dataDomain.domains['LX_COMBOBOX_MARCA'][1] = { id: 2, name: 'MARCA2' };
		dataDomain.domains['LX_COMBOBOX_MARCA'][2] = { id: 3, name: 'MARCA3' };
		dataDomain.domains['LX_REPRESENTANTE'] = [];
		dataDomain.domains['LX_REPRESENTANTE'][0] = { id: 1, name: 'REPRESENTANTE1' };
		dataDomain.domains['LX_REPRESENTANTE'][1] = { id: 2, name: 'REPRESENTANTE2' };
		dataDomain.domains['LX_REGIAO'] = [];
		dataDomain.domains['LX_REGIAO'][0] = { id: 1, name: 'REGIAO1' };
		dataDomain.domains['LX_REGIAO'][1] = { id: 2, name: 'REGIAO2' };
		dataDomain.domains['LX_REGIAO'][2] = { id: 3, name: 'REGIAO3' };
		dataDomain.domains['LX_VENDA'] = [];
		dataDomain.domains['LX_VENDA'][0] = { id: 1, name: 'VENDA1' };
		dataDomain.domains['LX_VENDA'][1] = { id: 2, name: 'VENDA2' };
		dataDomain.domains['LX_VENDA'][2] = { id: 3, name: 'VENDA3' };
		dataDomain.domains['LX_FORMA_PAGAMENTO'] = [];
		dataDomain.domains['LX_FORMA_PAGAMENTO'][0] = { id: 1, name: 'PAGAMENTO1' };
		dataDomain.domains['LX_FORMA_PAGAMENTO'][1] = { id: 2, name: 'PAGAMENTO2' };
		dataDomain.domains['LX_FORMA_PAGAMENTO'][2] = { id: 3, name: 'PAGAMENTO3' };
		dataDomain.domains['LX_VENDEDOR'] = [];
		dataDomain.domains['LX_VENDEDOR'][0] = { id: 1, name: 'VENDEDOR1' };
		dataDomain.domains['LX_VENDEDOR'][1] = { id: 2, name: 'VENDEDOR2' };
		dataDomain.domains['LX_VENDEDOR'][2] = { id: 3, name: 'VENDEDOR3' };
		dataDomain.domains['LX_CODIGO_FISCAL'] = [];
		dataDomain.domains['LX_CODIGO_FISCAL'][0] = { id: 1, name: 'FISCAL1' };
		dataDomain.domains['LX_CODIGO_FISCAL'][1] = { id: 2, name: 'FISCAL2' };
		dataDomain.domains['LX_CODIGO_FISCAL'][2] = { id: 3, name: 'FISCAL3' };
		dataDomain.domains['LX_PRODUTO'] = [];
		dataDomain.domains['LX_PRODUTO'][0] = { id: 1, name: 'PRODUTO1' };
		dataDomain.domains['LX_PRODUTO'][1] = { id: 2, name: 'PRODUTO2' };
		dataDomain.domains['LX_PRODUTO'][2] = { id: 3, name: 'PRODUTO3' };
    },
	getDomainValues: function (method, success) {
	    return $.ajax({
	        type: 'GET',
	        dataType: 'json',
            messageUser: 'Busca de valores de domínio.',
            contentType: 'application/json; charset=UTF-8',
            headers: managerAuth.getHeaders(),
            url: managerAuth.getServiceAddress(method),
            async: true,
            cache: false,
            success: success
        });
    },
    getItems: function (domainName, valuesFilter) {
        var items = dataDomain.domains[domainName];
        if (!isNullOrEmpty(valuesFilter) && items && items.length > 0) {
            var sourceItems = items;
            items = [];
            for (var i = 0; i < sourceItems.length; i++) {
                if ((',' + valuesFilter + ',').indexOf(',' + sourceItems[i].id + ',') > -1) {
                    items.push(sourceItems[i]);
			    }
            }
		}
        return (items && items.length > 0 ? items : []);
    },
    getName: function (domainName, value) {
        var name = '';
		if (value != null && value !== '') {
			var domainItems = this.getItems(domainName);
			if (domainItems) {
			    for (var i in domainItems) {
			        if (domainItems[i].id == value) {
			            name = domainItems[i].name;
			            break;
			        }
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
                if (domainItems[i].name == name) {
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