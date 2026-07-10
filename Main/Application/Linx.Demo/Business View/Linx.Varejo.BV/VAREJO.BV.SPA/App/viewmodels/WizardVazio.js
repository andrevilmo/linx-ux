define(['durandal/app', 'plugins/router', 'plugins/widget', 'managers/__auth', 'viewmodels/shared/modal', 'viewmodels/shared/modal2', 'managers/brand', 'managers/predefinedFilters', 'services/logger', 'viewmodels/shared/modalMultimidia', 'common', 'pkg_varejo-bv-spa/viewmodels/WizardVazioComplement', 'viewmodels/shared/modalCustomSearch'],
function (app, router, widget, managerAuth, modal, modal2, managerBrand, managerPredefined, logger, modalMultimidia, common, complementFn, modalCustomSearch) {
var vms = [];
var vmInstance = function () {
    var activeRoute = document.URL;
    if (activeRoute.indexOf('?') >= 0)
        activeRoute = activeRoute.substring(0, activeRoute.indexOf('?'));
    if (vms[activeRoute])
        return vms[activeRoute];
    else {
        var vm = vmConstructor();
        vms[activeRoute] = vm;
        return vm;
    }
}
var vmConstructor = function () {
    
        var isDependentVM = ko.observable(false);
        var transactionNumberControl = ko.observable('00000000');
        var hideToolbar = ko.observable(true);
        var complement = ((typeof complementFn === 'function') ? complementFn() : null);
        
        var notifyInnerElements = function (element, isExpander) {
            if (element)
            {
                try{ $(window).trigger('resize'); } catch(e){ console.log(e); }
                var innerElements = element.find("table");
                if (innerElements.length > 0 && (vm.dataSource.length > 0 || vm.internalUIs.length > 0)) {
                    for (var idx = 0; idx < innerElements.length; idx++) {
                        if($(innerElements[idx]).parents('.tab-pane').hasClass('active') || isExpander) {
                            for (var db in vm.dataSource) { if (vm.dataSource[db].key == innerElements[idx].id) vm.dataSource[db].itemsSource.dataBind(false, true); }
                            //Notifying inner UIs
                            for (var idxUI = 0; idxUI < vm.internalUIs.length; idxUI++) {
                               var innerVM = vm[vm.internalUIs[idxUI]]();
                               for (var db in innerVM.dataSource) {
                                   if (innerVM.dataSource[db].key == innerElements[idx].id)
                                       innerVM.dataSource[db].itemsSource.dataBind(false, true);
                               }
                            }
                        }
                    }
                }
            }
        };
        
        var _isBusy = false;
        var isBusy = function isBusy(value) {
            if (typeof value === 'undefined') {
                return _isBusy;
            } else {
                _isBusy = value;
                if ($(".page-container").html() == undefined || $(".page-container").html().length == 0)
                return;
                if (value) { common.showProcess('#main'); }
                else { common.closeProcess('#main'); }
            }
        };
        
        var bindingComplete = function () {
            return true;
        };
        
        var activate = function (settings, querystring) {
          if ((typeof settings === 'object') && (settings != null) && settings.objectQuery) {
              isDependentVM(false);
          }
          else {
              if ((typeof settings === 'object') && (settings != null) && settings.uiSettings) {
                  isDependentVM(true);
              }
          }
          vm.WizardVazio = getVM;
        };
        
        var getVM = function () {
            return vm;
        };
        
        var compositionComplete = function () {
            $('#WizardVazio_wizWizard').on('shown.bs.tab', function (e) { vm.notifyInnerElements($(e.target.hash)); });

    complement.renderWizardVazio_wizWizard(vm);


            return true;
        };
        
        var vm = { 
            dataShared: [],
            viewName: 'WizardVazio',
            hideToolbar: hideToolbar,
            isDependentVM: isDependentVM,
            bindingComplete: bindingComplete,
            activate: activate,
            compositionComplete: compositionComplete,
            notifyInnerElements: notifyInnerElements,
            transactionNumberControl: transactionNumberControl,
            dataToolbar: { title: function () { return ''; }, isBusy: isBusy, canCustomSearch: function () { return false; } },
            currentDataItem: function() { return null; },
            setBandeiraRede: function() { },
            status: ko.observable('N'),
            internalUIs: [],
            dataSource: [],
            dataBind: function(dataName, commitData) { },
            managerAuth: managerAuth,
            __moduleId__: 'pkg_varejo-bv-spa/viewmodels/WizardVazio'
        };
        return vm;
    }
    
    return vmInstance;
});
