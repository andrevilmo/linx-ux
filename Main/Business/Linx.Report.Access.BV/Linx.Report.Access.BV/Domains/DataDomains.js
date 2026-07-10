	


define(['managers/__auth'], function (managerAuth) {


var dataDomain = {
    domains: [],    
    registerDomains: function () {
		var self = this;

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