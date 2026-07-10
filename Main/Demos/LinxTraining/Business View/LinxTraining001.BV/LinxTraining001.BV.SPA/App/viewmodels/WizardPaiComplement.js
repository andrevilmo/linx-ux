define(['managers/__auth'], function (managerAuth) {
var complementCtor = function() {
    var complement = {
    isAutomatic: true
    , renderWizardPai_wizWizard_aa4b3244d62a4a8c943ee0a6a0ba801d: function(vm) {
    $('#WizardPai_wizWizard_aa4b3244d62a4a8c943ee0a6a0ba801d').bootstrapWizard({
        'nextSelector': '.button-next',
        'previousSelector': '.button-previous',
        onInit: function (tab, navigation, index) {
            $('#WizardPai_wizWizard_aa4b3244d62a4a8c943ee0a6a0ba801d').find('.button-previous').hide();
    		 $('#WizardPai_wizWizard_aa4b3244d62a4a8c943ee0a6a0ba801d .button-submit').click(function () {
    			 if((typeof vm.OnWizardFinalizing == 'function')) {
    			    if (!vm.OnWizardFinalizing('WizardPai_wizWizard_aa4b3244d62a4a8c943ee0a6a0ba801d')) return false;
    			 }
    			 if((typeof vm.OnWizardFinalized == 'function')) {
    			    vm.OnWizardFinalized('WizardPai_wizWizard_aa4b3244d62a4a8c943ee0a6a0ba801d');
    			 }
    			 if(vm.custom) {
    		        var e = { cancel: false, viewModel: vm };
    			    vm.custom.beforeWizardFinalizing(e); //custom Finalizing
    			    if(e.cancel) return false;
    			    vm.custom.afterWizardFinalizing({viewModel: vm, id: 'WizardPai_wizWizard_aa4b3244d62a4a8c943ee0a6a0ba801d'}); //custom Finalized
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
    		    if (!vm.OnWizardStepChanging(tab.index(), index, 'WizardPai_wizWizard_aa4b3244d62a4a8c943ee0a6a0ba801d')) return false;
    		 }
    		 var e = { oldIndex: tab.index(), newIndex: index, cancel: false, viewModel: vm, id: 'WizardPai_wizWizard_aa4b3244d62a4a8c943ee0a6a0ba801d'};
    		 if(vm.custom) vm.custom.beforeWizardStepChanging(e); //custom Step changing
    		 if(e.cancel) return false;
    		 wizardStepChange('WizardPai_wizWizard_aa4b3244d62a4a8c943ee0a6a0ba801d',  navigation, index);
    		 if((typeof vm.OnWizardStepChanged == 'function')) {
    		    vm.OnWizardStepChanged(tab.index(), index, 'WizardPai_wizWizard_aa4b3244d62a4a8c943ee0a6a0ba801d');
    		 }
    		 if(vm.custom) vm.custom.afterWizardStepChanging({ oldIndex: tab.index(), newIndex: index, viewModel: vm, id: 'WizardPai_wizWizard_aa4b3244d62a4a8c943ee0a6a0ba801d'}); //custom Step changed
        },
        onNext: function (tab, navigation, index) {
    		 if((typeof vm.OnWizardStepChanging == 'function')) {
    		    if (!vm.OnWizardStepChanging(tab.index(), index, 'WizardPai_wizWizard_aa4b3244d62a4a8c943ee0a6a0ba801d')) return false;
    		 }
    		 var e = { oldIndex: tab.index(), newIndex: index, cancel: false, viewModel: vm, id: 'WizardPai_wizWizard_aa4b3244d62a4a8c943ee0a6a0ba801d'};
    		 if(vm.custom) vm.custom.beforeWizardStepChanging(e); //custom Step changing
    		 if(e.cancel) return false;
    		 wizardStepChange('WizardPai_wizWizard_aa4b3244d62a4a8c943ee0a6a0ba801d',  navigation, index);
    		 if((typeof vm.OnWizardStepChanged == 'function')) {
    		    vm.OnWizardStepChanged(tab.index(), index, 'WizardPai_wizWizard_aa4b3244d62a4a8c943ee0a6a0ba801d');
    		 }
    		 if(vm.custom) vm.custom.afterWizardStepChanging({ oldIndex: tab.index(), newIndex: index, viewModel: vm, id: 'WizardPai_wizWizard_aa4b3244d62a4a8c943ee0a6a0ba801d'}); //custom Step changed
        },
        onTabShow: function (tab, navigation, index) {
            var total = navigation.find('li').length;
            var current = index + 1;
            var $percent = (current / total) * 100;
            $('#WizardPai_wizWizard_aa4b3244d62a4a8c943ee0a6a0ba801d').find('.progress-bar').css({
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
