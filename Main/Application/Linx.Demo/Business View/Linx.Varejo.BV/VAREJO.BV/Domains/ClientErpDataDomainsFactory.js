					


    /* jshint ignore:start */

    var name = namespace.common.buildNameSpace('factories.ClientErpDataDomainsFactory');

    var domainsFactory = function () {

		var dataDomain = {
			domains: [],    
			registerDomains: function () {

						dataDomain.domains['LX_LOJA'] = [];
						dataDomain.domains['LX_LOJA'][0] = { id: 1, name: 'LOJA 1' };
						dataDomain.domains['LX_LOJA'][1] = { id: 2, name: 'LOJA 2' };
						dataDomain.domains['LX_LOJA'][2] = { id: 3, name: 'LOJA 3' };
						dataDomain.domains['LX_ESTADO'] = [];
						dataDomain.domains['LX_ESTADO'][0] = { id: 1, name: 'ESTADO 1' };
						dataDomain.domains['LX_ESTADO'][1] = { id: 2, name: 'ESTADO 2' };
						dataDomain.domains['LX_ESTADO'][2] = { id: 3, name: 'ESTADO 3' };
						dataDomain.domains['LX_ESTADO'][3] = { id: 4, name: 'ESTADO 4' };
						dataDomain.domains['LX_PAIS'] = [];
						dataDomain.domains['LX_PAIS'][0] = { id: 1, name: 'PAIS 1' };
						dataDomain.domains['LX_PAIS'][1] = { id: 2, name: 'PAIS 2' };
						dataDomain.domains['LX_PAIS'][2] = { id: 3, name: 'PAIS 3' };
						dataDomain.domains['LX_VENDA'] = [];
						dataDomain.domains['LX_VENDA'][0] = { id: 1, name: 'VENDA 1' };
						dataDomain.domains['LX_VENDA'][1] = { id: 2, name: 'VENDA 2' };
						dataDomain.domains['LX_VENDA'][2] = { id: 3, name: 'VENDA 3' };
						dataDomain.domains['LX_VENDEDOR'] = [];
						dataDomain.domains['LX_VENDEDOR'][0] = { id: 1, name: 'VENDEDOR 1' };
						dataDomain.domains['LX_VENDEDOR'][1] = { id: 2, name: 'VENDEDOR 2' };
						dataDomain.domains['LX_VENDEDOR'][2] = { id: 3, name: 'VENDEDOR 3' };
						dataDomain.domains['LX_FORMA_PAGAMENTO'] = [];
						dataDomain.domains['LX_FORMA_PAGAMENTO'][0] = { id: 1, name: 'FORMA PAGAMENTO 1' };
						dataDomain.domains['LX_FORMA_PAGAMENTO'][1] = { id: 2, name: 'FORMA PAGAMENTO 2' };
						dataDomain.domains['LX_FORMA_PAGAMENTO'][2] = { id: 3, name: 'FORMA PAGAMENTO 3' };
						dataDomain.domains['LX_CLIENTE'] = [];
						dataDomain.domains['LX_CLIENTE'][0] = { id: 1, name: 'CLIENTE 1' };
						dataDomain.domains['LX_CLIENTE'][1] = { id: 2, name: 'CLIENTE 2' };
						dataDomain.domains['LX_CLIENTE'][2] = { id: 3, name: 'CLIENTE 3' };
						dataDomain.domains['LX_VENDA_ITEM'] = [];
						dataDomain.domains['LX_VENDA_ITEM'][0] = { id: 1, name: 'VENDA ITEM 1' };
						dataDomain.domains['LX_VENDA_ITEM'][1] = { id: 2, name: 'VENDA ITEM 2' };
						dataDomain.domains['LX_VENDA_ITEM'][2] = { id: 3, name: 'VENDA ITEM 3' };
						dataDomain.domains['LX_VENDA_PAI'] = [];
						dataDomain.domains['LX_VENDA_PAI'][0] = { id: 'A', name: 'VENDA 1' };
						dataDomain.domains['LX_VENDA_PAI'][1] = { id: 'B', name: 'VENDA 2' };
						dataDomain.domains['LX_VENDA_PAI'][2] = { id: 'C', name: 'VENDA 3' };
						dataDomain.domains['LX_VENDA_FILHA'] = [];
						dataDomain.domains['LX_VENDA_FILHA'][0] = { id: 'A', name: 'VENDA ITEM 1' };
						dataDomain.domains['LX_VENDA_FILHA'][1] = { id: 'B', name: 'VENDA ITEM 2' };
						dataDomain.domains['LX_VENDA_FILHA'][2] = { id: 'C', name: 'VENDA ITEM 3' };
						dataDomain.domains['LX_VENDA_ATACADO'] = [];
						dataDomain.domains['LX_VENDA_ATACADO'][0] = { id: 1, name: 'VENDA 1' };
						dataDomain.domains['LX_VENDA_ATACADO'][1] = { id: 2, name: 'VENDA 2' };
						dataDomain.domains['LX_VENDA_ATACADO'][2] = { id: 3, name: 'VENDA 3' };
					},
			getItems: function (domainName, valuesFilter) {
				var items = dataDomain.domains[domainName];
				if (valuesFilter && valuesFilter != '' && items && items.length > 0) {
					var sourceItems = items;
					items = [];
					for (var i = 0; i < sourceItems.length; i++) {
						if ((',' + valuesFilter + ',').indexOf(',' + sourceItems[i].id.toString() + ',') > -1) {
							items.push(sourceItems[i]);
						}
					}
				}
				return (items && items.length > 0 ? items : []);
			},
			getName: function (domainName, value) {
				var name = '';
				var domainItems = this.getItems(domainName);
				if (domainItems) {
					for (var i in domainItems) {
						if (domainItems[i].id == value) {
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
    };
	
	module.exports = function(appModule) {
		appModule.factory(name, [domainsFactory]);
	};

	/* jshint ignore:end */