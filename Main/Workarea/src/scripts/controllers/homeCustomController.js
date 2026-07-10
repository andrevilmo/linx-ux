'use strict';

function HomeCustomController() {
	var vm = this;
	vm.init = init;
	vm.nome = 'HomeCustomController';

	function init() {
		console.log('homeCustomController init()');
	}
}

module.exports = function(appModule) {
	appModule.controller('homeCustomController', HomeCustomController);
};