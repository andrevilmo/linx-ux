define(['durandal/app', 'plugins/router', 'plugins/widget', 'managers/__auth', 'viewmodels/shared/modal', 'viewmodels/shared/modal2', 'managers/brand', 'managers/predefinedFilters', 'services/logger', 'viewmodels/shared/modalMultimidia', 'common', 'viewmodels/shared/modalCustomSearch'],
function (app, router, widget, managerAuth, modal, modal2, managerBrand, managerPredefined, logger, modalMultimidia, common, modalCustomSearch) {
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
        var bindingComplete = function () {
            return true;
        };
        
        var compositionComplete = function () {
            return true;
        };
        
        var vm = { 
            dataShared: [],
            viewName: 'Faq_UI',
            hideToolbar: hideToolbar,
            isDependentVM: isDependentVM,
            bindingComplete: bindingComplete,
            compositionComplete: compositionComplete,
            __moduleId__: 'pkg_linx-dashboard-spa/viewmodels/Faq_UI'
        };
        return vm;
    }
    
    return vmInstance;
});
