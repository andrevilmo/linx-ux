define(['managers/__auth', 'managers/user'], function (managerAuth, managerUser) {
var complementCtor = function() {
    var complement = {
    isAutomatic: true
    , renderWizardVazio_wizWizard: function(vm) {
    $('#WizardVazio_wizWizard').bootstrapWizard({
        'nextSelector': '.button-next',
        'previousSelector': '.button-previous',
        onInit: function (tab, navigation, index) {
            $('#WizardVazio_wizWizard').find('.button-previous').hide();
    		 $('#WizardVazio_wizWizard .button-submit').click(function () {
    			 if((typeof vm.OnWizardFinalizing === 'function')) {
    			    if (!vm.OnWizardFinalizing('WizardVazio_wizWizard')) return false;
    			 }
    			 if((typeof vm.OnWizardFinalized === 'function')) {
    			    vm.OnWizardFinalized('WizardVazio_wizWizard');
    			 }
    			 if(vm.custom) {
    		        var e = { cancel: false, viewModel: vm };
    			    vm.custom.beforeWizardFinalizing(e); //custom Finalizing
    			    if(e.cancel) return false;
    			    vm.custom.afterWizardFinalizing({viewModel: vm, id: 'WizardVazio_wizWizard'}); //custom Finalized
    			 }
    		 }).hide();
    		 if((typeof vm.OnWizardInitializing === 'function')) {
    		     vm.OnWizardInitializing();
    		 }
    		 if(vm.custom) vm.custom.afterWizardInitializing({viewModel: vm});
        },
        onTabClick: function (tab, navigation, index) {
            return false;
        },
        onPrevious: function (tab, navigation, index) {
    		 if((typeof vm.OnWizardStepChanging === 'function')) {
    		    if (!vm.OnWizardStepChanging(tab.index(), index, 'WizardVazio_wizWizard')) return false;
    		 }
    		 var e = { oldIndex: tab.index(), newIndex: index, cancel: false, viewModel: vm, id: 'WizardVazio_wizWizard'};
    		 if(vm.custom) vm.custom.beforeWizardStepChanging(e); //custom Step changing
    		 if(e.cancel) return false;
    		 wizardStepChange('WizardVazio_wizWizard',  navigation, index);
    		 if((typeof vm.OnWizardStepChanged === 'function')) {
    		    vm.OnWizardStepChanged(tab.index(), index, 'WizardVazio_wizWizard');
    		 }
    		 if(vm.custom) vm.custom.afterWizardStepChanging({ oldIndex: tab.index(), newIndex: index, viewModel: vm, id: 'WizardVazio_wizWizard'}); //custom Step changed
        },
        onNext: function (tab, navigation, index) {
    		 if((typeof vm.OnWizardStepChanging === 'function')) {
    		    if (!vm.OnWizardStepChanging(tab.index(), index, 'WizardVazio_wizWizard')) return false;
    		 }
    		 var e = { oldIndex: tab.index(), newIndex: index, cancel: false, viewModel: vm, id: 'WizardVazio_wizWizard'};
    		 if(vm.custom) vm.custom.beforeWizardStepChanging(e); //custom Step changing
    		 if(e.cancel) return false;
    		 wizardStepChange('WizardVazio_wizWizard',  navigation, index);
    		 if((typeof vm.OnWizardStepChanged === 'function')) {
    		    vm.OnWizardStepChanged(tab.index(), index, 'WizardVazio_wizWizard');
    		 }
    		 if(vm.custom) vm.custom.afterWizardStepChanging({ oldIndex: tab.index(), newIndex: index, viewModel: vm, id: 'WizardVazio_wizWizard'}); //custom Step changed
        },
        onTabShow: function (tab, navigation, index) {
            var total = navigation.find('li').length;
            var current = index + 1;
            var $percent = (current / total) * 100;
            $('#WizardVazio_wizWizard').find('.progress-bar').css({
                width: $percent + '%'
            });
        }
    });
}


    };
    complement.changedBrands = function changedBrands(gridName, infoColumns) {
        //infoColumns[] - {columnName: 'Column1', format: '0.00', decimals: 2}
        if (infoColumns !== null) {
            var i, j, grd = $lx(vm, '#' + gridName).data('igGrid'),
                grdUpd = $lx(vm, '#' + gridName).data('igGridUpdating');
            for (i = 0; i < grd.options.columns.length; i++) {
                for (j = 0; j < infoColumns.length; j++) {
                    if (grd.options.columns[i].key === infoColumns[j].columnName)
                        grd.options.columns[i].format = infoColumns[j].format;
                }
            }
            for (i = 0; i < grdUpd.options.columnSettings.length; i++) {
                for (j = 0; j < infoColumns.length; j++) {
                    if (grdUpd.options.columnSettings[i].columnKey === infoColumns[j].columnName) {
                        grdUpd.options.columnSettings[i].editorOptions.minDecimals = infoColumns[j].decimals;
                        grdUpd.options.columnSettings[i].editorOptions.maxDecimals = infoColumns[j].decimals;
                    }
                }
            }
            grd.dataBind();
        }
    };
    
    return complement;
}

return complementCtor;
});
