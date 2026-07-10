define(['managers/__auth'], function (managerAuth) {
var complementCtor = function() {
    var complement = {
    isAutomatic: true
    , renderWizardRede_wizWizard_de0ea39f908544a4ada5e73a0e7e9d25: function(vm) {
    $('#WizardRede_wizWizard_de0ea39f908544a4ada5e73a0e7e9d25').bootstrapWizard({
        'nextSelector': '.button-next',
        'previousSelector': '.button-previous',
        onInit: function (tab, navigation, index) {
            $('#WizardRede_wizWizard_de0ea39f908544a4ada5e73a0e7e9d25').find('.button-previous').hide();
    		 $('#WizardRede_wizWizard_de0ea39f908544a4ada5e73a0e7e9d25 .button-submit').click(function () {
    			 if((typeof vm.OnWizardFinalizing == 'function')) {
    			    if (!vm.OnWizardFinalizing('WizardRede_wizWizard_de0ea39f908544a4ada5e73a0e7e9d25')) return false;
    			 }
    			 if((typeof vm.OnWizardFinalized == 'function')) {
    			    vm.OnWizardFinalized('WizardRede_wizWizard_de0ea39f908544a4ada5e73a0e7e9d25');
    			 }
    			 if(vm.custom) {
    		        var e = { cancel: false, viewModel: vm };
    			    vm.custom.beforeWizardFinalizing(e); //custom Finalizing
    			    if(e.cancel) return false;
    			    vm.custom.afterWizardFinalizing({viewModel: vm, id: 'WizardRede_wizWizard_de0ea39f908544a4ada5e73a0e7e9d25'}); //custom Finalized
    			 }
    		 }).hide();
    		 if((typeof vm.OnWizardInitializing == 'function')) {
    		     vm.OnWizardInitializing();
    		 }
    		 if(vm.custom) vm.custom.afterWizardInitializing({viewModel: vm});
        },
        onTabClick: function (tab, navigation, index) {
            return false;
        },
        onPrevious: function (tab, navigation, index) {
    		 if((typeof vm.OnWizardStepChanging == 'function')) {
    		    if (!vm.OnWizardStepChanging(tab.index(), index, 'WizardRede_wizWizard_de0ea39f908544a4ada5e73a0e7e9d25')) return false;
    		 }
    		 var e = { oldIndex: tab.index(), newIndex: index, cancel: false, viewModel: vm, id: 'WizardRede_wizWizard_de0ea39f908544a4ada5e73a0e7e9d25'};
    		 if(vm.custom) vm.custom.beforeWizardStepChanging(e); //custom Step changing
    		 if(e.cancel) return false;
    		 wizardStepChange('WizardRede_wizWizard_de0ea39f908544a4ada5e73a0e7e9d25',  navigation, index);
    		 if((typeof vm.OnWizardStepChanged == 'function')) {
    		    vm.OnWizardStepChanged(tab.index(), index, 'WizardRede_wizWizard_de0ea39f908544a4ada5e73a0e7e9d25');
    		 }
    		 if(vm.custom) vm.custom.afterWizardStepChanging({ oldIndex: tab.index(), newIndex: index, viewModel: vm, id: 'WizardRede_wizWizard_de0ea39f908544a4ada5e73a0e7e9d25'}); //custom Step changed
        },
        onNext: function (tab, navigation, index) {
    		 if((typeof vm.OnWizardStepChanging == 'function')) {
    		    if (!vm.OnWizardStepChanging(tab.index(), index, 'WizardRede_wizWizard_de0ea39f908544a4ada5e73a0e7e9d25')) return false;
    		 }
    		 var e = { oldIndex: tab.index(), newIndex: index, cancel: false, viewModel: vm, id: 'WizardRede_wizWizard_de0ea39f908544a4ada5e73a0e7e9d25'};
    		 if(vm.custom) vm.custom.beforeWizardStepChanging(e); //custom Step changing
    		 if(e.cancel) return false;
    		 wizardStepChange('WizardRede_wizWizard_de0ea39f908544a4ada5e73a0e7e9d25',  navigation, index);
    		 if((typeof vm.OnWizardStepChanged == 'function')) {
    		    vm.OnWizardStepChanged(tab.index(), index, 'WizardRede_wizWizard_de0ea39f908544a4ada5e73a0e7e9d25');
    		 }
    		 if(vm.custom) vm.custom.afterWizardStepChanging({ oldIndex: tab.index(), newIndex: index, viewModel: vm, id: 'WizardRede_wizWizard_de0ea39f908544a4ada5e73a0e7e9d25'}); //custom Step changed
        },
        onTabShow: function (tab, navigation, index) {
            var total = navigation.find('li').length;
            var current = index + 1;
            var $percent = (current / total) * 100;
            $('#WizardRede_wizWizard_de0ea39f908544a4ada5e73a0e7e9d25').find('.progress-bar').css({
                width: $percent + '%'
            });
        }
    });
}


    };
    return complement;
}

return complementCtor;
});
