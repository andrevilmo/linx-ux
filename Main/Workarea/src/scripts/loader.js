'use strict';

function LoaderApp() {}

LoaderApp.prototype.loadComponents = function(app) {
	this.providers(app);
	this.configs(app);
	this.runners(app);
	this.directives(app);
	this.controllers(app);
	this.services(app);
	this.factories(app);
	this.constants(app);
	this.filters(app);
	this.values(app);
	this.styles(app);
	this.ngTemplates();
	this.zipFiles();
};

LoaderApp.prototype.ngTemplates = function() {
	var path = require('path');

	var $templateCache = angular.element(document).injector().get('$templateCache');

	var context = require.context('../templates', true, /NgTemplate.html/);

	context.keys().forEach(function(item) {
		var key = path.basename(item);
		$templateCache.put(key, context(item));
	});

	context = require.context('../templates', true, /.sample/);

	context.keys().forEach(function(item) {
		var key = path.basename(item);
		$templateCache.put(key, context(item));
	});
};

LoaderApp.prototype.translates = function() {
	var translates = [];

	var translatesData = require.context('../data/locales', false, /.json/);
	requireTranslate(translatesData, translates);

	var translatesTemplates = require.context('../templates/locales', false, /.json/);
	requireTranslate(translatesTemplates, translates);

	function requireTranslate(context, translates) {
		context.keys().forEach(function(item) {

			var start = item.indexOf('_');
			var end = item.indexOf('.json');
			var localeId = item.substring(start + 1, end);

			translates.push({
				key: localeId,
				value: context(item)
			});
		});
	}

	return translates;
};

LoaderApp.prototype.configs = function(app) {
	var context = require.context('./configs', false, /Config.js/);
	this.requireContext(context, app);
};

LoaderApp.prototype.constants = function(app) {
	var context = require.context('./constants', false, /Constant.js/);
	this.requireContext(context, app);
};

LoaderApp.prototype.services = function(app) {
	var context = require.context('./services', false, /Service.js/);
	this.requireContext(context, app);
};

LoaderApp.prototype.controllers = function(app) {
	var context = require.context('./controllers', false, /Controller.js/);
	this.requireContext(context, app);
};

LoaderApp.prototype.directives = function(app) {
	var context = require.context('./directives', true, /Directive.js/);
	this.requireContext(context, app);
};

LoaderApp.prototype.factories = function(app) {
	var context = require.context('./factories', false, /Factory.js/);
	this.requireContext(context, app);
};

LoaderApp.prototype.filters = function(app) {
	var context = require.context('./filters', false, /Filter.js/);
	this.requireContext(context, app);
};

LoaderApp.prototype.runners = function(app) {
	var context = require.context('./runners', false, /Run.js/);
	this.requireContext(context, app);
};

LoaderApp.prototype.values = function(app) {
	var context = require.context('./values', false, /Value.js/);
	this.requireContext(context, app);
};

LoaderApp.prototype.styles = function(app) {
	var context = require.context('../styles', false, /Style.scss/);
	context.keys().forEach(function(item) {
		context(item);
	});
};

LoaderApp.prototype.providers = function(app) {
	var context = require.context('./providers', false, /Provider.js/);
	this.requireContext(context, app);
};

LoaderApp.prototype.requireContext = function(context, app) {
	context.keys().forEach(function(item) {
		context(item)(app);
	});
};

LoaderApp.prototype.zipFiles = function() {
	var context = require.context('file-loader?name=./assets/zip/[name].[ext]!../resources', true, /.zip/);

	context.keys().forEach(function(item) {
		context(item);
	});
};

module.exports = new LoaderApp();