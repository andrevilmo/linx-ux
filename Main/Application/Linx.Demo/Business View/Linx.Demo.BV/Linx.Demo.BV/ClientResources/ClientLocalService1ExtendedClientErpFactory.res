    /* jshint ignore:start */
    'use strict';
    
    var name = 'Demo_ClientLocalService1ExtendedClientErpFactory';
    
    var dependencies = [
            '$state',
            '$log',
            '$rootScope',
            'commonFactory',
            'dialogFactory',
            'messengeFactory',
            'shellManagerService'
    ];
    
    var extendedDataBusinessFactory = function ($state, $log, $rootScope, common, dialog, messenger, shellManagerService) {
        //#region Client Events
        var OnInit = function () {
        
        }
        var OnClearing = function () {
        return true;
        }
        var OnCleared = function () {
        
        }
        var OnSearching = function () {
        return '';
        }
        var OnSearched = function () {
        
        }
        var OnEditing = function () {
        return true;
        }
        var OnEdited = function () {
        
        }
        var OnPrinting = function () {
        return true;
        }
        var OnPrinted = function () {
        
        }
        var OnCancelling = function () {
        return true;
        }
        var OnCancelled = function () {
        
        }
        var OnSaving = function (changes) {
        return true;
        }
        var OnSaved = function (changes) {
        
        }
        var OnToolbarAction = function (action) {
        return true;
        }
        var OnReporting = function (reportName) {
        return '';
        }
        //#endregion Client Events
        
        var dataBusiness = null;
        var extendedDataBusiness = {
                OnInit: OnInit,
                OnClearing: OnClearing,
                OnCleared: OnCleared,
                OnSearching: OnSearching,
                OnSearched: OnSearched,
                OnEditing: OnEditing,
                OnEdited: OnEdited,
                OnPrinting: OnPrinting,
                OnPrinted: OnPrinted,
                OnCancelling: OnCancelling,
                OnCancelled: OnCancelled,
                OnSaving: OnSaving,
                OnSaved: OnSaved,
                OnToolbarAction: OnToolbarAction,
                OnReporting: OnReporting,
             setCurrentDataBusiness: function(curDataBusiness) { dataBusiness = curDataBusiness; }
        };
        
        return extendedDataBusiness;
    };
    
    module.exports = function(appModule) {
       appModule.factory(name, dependencies.concat(extendedDataBusinessFactory));
    };
    /* jshint ignore:end */
