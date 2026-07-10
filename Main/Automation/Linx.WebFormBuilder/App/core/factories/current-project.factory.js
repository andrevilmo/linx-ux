(function(_) {
	'use strict';

	angular
		.module('FormBuilder')
		.factory('currentProject',['$rootScope','$modal', currentProject]);

	//directoryWatcher.$inject = [];

	function currentProject($rootScope,$modal) {

	    require('ipc').on('checklist', function () {
	        $rootScope.modalInstance = $modal.open({
	            templateUrl: 'app/core/modals/checklist/checklist.view.html',
	            controller: 'checklistModalController',
	            size: 'sm',

	        });

	    });


		var urlProjectFile = '';
		var urlPathProject = '';
		var currentFile = '';
		var templatesBasePath = '\\builder\\defaults\\default';
		var userModulesBasePath = '\\app\\js\\';
		var userJsonBasePath = '\\builder\\';
		var viewsSubPath = '\\app\\views\\';
		var cssSubPath = '\\app\\css\\';
		var treeViewState = { treeView: null, expandedNodes: [], mode: {} };

		var service = {
			urlProjectFile: urlProjectFile,
			urlPathProject: urlPathProject,
			currentFile: currentFile,
			templatePath: getTemplatePath,
			modulePath: getNewModulePath,
			jsonFolderPath: getNewJsonFolderPath,
			jsonPath: getNewJsonPath,
			viewFolderPath: getViewFolderPath,
			viewPath: getNewViewPath,
			cssFolderPath: getCssFolderPath,
            cssPath: getNewCssPath,
			getTreeViewState: getTreeViewState,
			setTreeViewState: setTreeViewState,
			getCurrentFileName: getCurrentFileName
		};

		return service;

		function getTemplatePath(type) {
			var path = service.urlPathProject + templatesBasePath + type + '.js';
			return path;
		}

		function getCurrentFileName() {
			var fileName = "";
			if(service.currentFile) {
				var currentFileName = service.currentFile.split('\\').pop();
				fileName = currentFileName.substring(0, currentFileName.lastIndexOf('.'));
			}
			return fileName;
		}

		function getNewModulePath(filename, type) {
			var complement = type === 'factory' ? 'factories\\' : type + 's\\';
			var path = service.urlPathProject + userModulesBasePath + complement + filename + '.js';
			return path;
		}

		function getNewJsonFolderPath(type) {
			var path = service.urlPathProject + userJsonBasePath + type;
			return path;
		}

		function getNewJsonPath(filename, containingFolder) {
			var path = containingFolder + '\\' + filename + '.json';
			return path;
		}

		function getNewViewPath(filename) {
			var path = service.viewFolderPath() + filename + '.html';
			return path;
		}

		function getViewFolderPath() {
			var path = service.urlPathProject + viewsSubPath;
			return path;
		}

		function getNewCssPath(filename) {
		    var path = service.cssFolderPath() + filename + '.css';
		    return path;
		}

		function getCssFolderPath() {
		    var path = service.urlPathProject + cssSubPath;
		    return path;
		}

		function setTreeViewState(treeView){
		    treeViewState = treeView;
    }

		function getTreeViewState() {
		    return treeViewState;
		}
	}
})(_);
