'use strict';

module.exports = function(tpl) {
	var ngModule = require('./routesConfig')(tpl);
	return ngModule;
};