	


    /* jshint ignore:start */

    var name = namespace.common.buildNameSpace('factories.ClientErpDataDomainsFactory');

    var domainsFactory = function () {

		var dataDomain = {
			domains: [],    
			registerDomains: function () {

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