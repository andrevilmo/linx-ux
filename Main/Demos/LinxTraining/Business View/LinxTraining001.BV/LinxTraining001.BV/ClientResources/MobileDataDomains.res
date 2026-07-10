define([
        'appModule'
], function (module) {
    'use strict';

    var name = 'LinxTraining001_MobileDataDomains';


    var service = function () {

		var dataDomain = {
			domains: [],    
			registerDomains: function () {

						dataDomain.domains['LXOrigem'] = [];
						dataDomain.domains['LXOrigem'][0] = { id: 1, name: 'Internet' };
						dataDomain.domains['LXOrigem'][1] = { id: 2, name: 'Loja Física' };
						dataDomain.domains['LXTipoClientes'] = [];
						dataDomain.domains['LXTipoClientes'][0] = { id: 3, name: 'Fornecedor' };
						dataDomain.domains['LXTipoClientes'][1] = { id: 1, name: 'Pessoa Física' };
						dataDomain.domains['LXTipoClientes'][2] = { id: 2, name: 'Pessoa Jurídica' };
						dataDomain.domains['TstDomainString'] = [];
						dataDomain.domains['TstDomainString'][0] = { id: '01', name: 'String 01' };
						dataDomain.domains['TstDomainString'][1] = { id: '01A', name: 'String 01A' };
						dataDomain.domains['TstDomainString'][2] = { id: '02', name: 'String 02' };
						dataDomain.domains['TstDomainString'][3] = { id: 'A', name: 'String A' };
						dataDomain.domains['TstDomainString'][4] = { id: 'ststdd', name: 'NewString' };
						dataDomain.domains['TstDomainString'][5] = { id: 'sttst', name: 'String Teste' };
						dataDomain.domains['TstDomainString'][6] = { id: 'ValString', name: 'ValString' };
						dataDomain.domains['ProdutoDomain'] = [];
						dataDomain.domains['ProdutoDomain'][0] = { id: 'Item1', name: 'PRODUTO A' };
						dataDomain.domains['ProdutoDomain'][1] = { id: 'Item2', name: 'PRODUTO B' };
						dataDomain.domains['ProdutoDomain'][2] = { id: 'Item3', name: 'PRODUTO C' };
						dataDomain.domains['ProdutoDomain'][3] = { id: 'Item4', name: 'PRODUTO D' };
						dataDomain.domains['DomainString'] = [];
						dataDomain.domains['DomainString'][0] = { id: '05', name: 'String 05' };
						dataDomain.domains['DomainString'][1] = { id: '06', name: 'String 06' };
						dataDomain.domains['DomainString'][2] = { id: 'B', name: 'String B' };
						dataDomain.domains['DomainString'][3] = { id: '01B', name: 'String 01B' };
						dataDomain.domains['DomainString'][4] = { id: 'ststt', name: 'Teste String' };
						dataDomain.domains['tstCombo'] = [];
						dataDomain.domains['tstCombo'][0] = { id: 1, name: 'Teste1' };
						dataDomain.domains['tstCombo'][1] = { id: 2, name: 'Teste2' };
						dataDomain.domains['tstCombo'][2] = { id: 3, name: 'Teste3' };
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
    }

    module.factory(name, [service]);

});