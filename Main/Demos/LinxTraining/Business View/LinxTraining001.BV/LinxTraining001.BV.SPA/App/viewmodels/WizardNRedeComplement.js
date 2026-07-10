define(['managers/__auth'], function (managerAuth) {
var complementCtor = function() {
    var complement = {
    isAutomatic: true
    , renderWizardNRede_wizWizard_d71b4906824a4788a656b686296fe25d: function(vm) {
    $('#WizardNRede_wizWizard_d71b4906824a4788a656b686296fe25d').bootstrapWizard({
        'nextSelector': '.button-next',
        'previousSelector': '.button-previous',
        onInit: function (tab, navigation, index) {
            $('#WizardNRede_wizWizard_d71b4906824a4788a656b686296fe25d').find('.button-previous').hide();
    		 $('#WizardNRede_wizWizard_d71b4906824a4788a656b686296fe25d .button-submit').click(function () {
    			 if((typeof vm.OnWizardFinalizing == 'function')) {
    			    if (!vm.OnWizardFinalizing('WizardNRede_wizWizard_d71b4906824a4788a656b686296fe25d')) return false;
    			 }
    			 if((typeof vm.OnWizardFinalized == 'function')) {
    			    vm.OnWizardFinalized('WizardNRede_wizWizard_d71b4906824a4788a656b686296fe25d');
    			 }
    			 if(vm.custom) {
    		        var e = { cancel: false, viewModel: vm };
    			    vm.custom.beforeWizardFinalizing(e); //custom Finalizing
    			    if(e.cancel) return false;
    			    vm.custom.afterWizardFinalizing({viewModel: vm, id: 'WizardNRede_wizWizard_d71b4906824a4788a656b686296fe25d'}); //custom Finalized
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
    		    if (!vm.OnWizardStepChanging(tab.index(), index, 'WizardNRede_wizWizard_d71b4906824a4788a656b686296fe25d')) return false;
    		 }
    		 var e = { oldIndex: tab.index(), newIndex: index, cancel: false, viewModel: vm, id: 'WizardNRede_wizWizard_d71b4906824a4788a656b686296fe25d'};
    		 if(vm.custom) vm.custom.beforeWizardStepChanging(e); //custom Step changing
    		 if(e.cancel) return false;
    		 wizardStepChange('WizardNRede_wizWizard_d71b4906824a4788a656b686296fe25d',  navigation, index);
    		 if((typeof vm.OnWizardStepChanged == 'function')) {
    		    vm.OnWizardStepChanged(tab.index(), index, 'WizardNRede_wizWizard_d71b4906824a4788a656b686296fe25d');
    		 }
    		 if(vm.custom) vm.custom.afterWizardStepChanging({ oldIndex: tab.index(), newIndex: index, viewModel: vm, id: 'WizardNRede_wizWizard_d71b4906824a4788a656b686296fe25d'}); //custom Step changed
        },
        onNext: function (tab, navigation, index) {
    		 if((typeof vm.OnWizardStepChanging == 'function')) {
    		    if (!vm.OnWizardStepChanging(tab.index(), index, 'WizardNRede_wizWizard_d71b4906824a4788a656b686296fe25d')) return false;
    		 }
    		 var e = { oldIndex: tab.index(), newIndex: index, cancel: false, viewModel: vm, id: 'WizardNRede_wizWizard_d71b4906824a4788a656b686296fe25d'};
    		 if(vm.custom) vm.custom.beforeWizardStepChanging(e); //custom Step changing
    		 if(e.cancel) return false;
    		 wizardStepChange('WizardNRede_wizWizard_d71b4906824a4788a656b686296fe25d',  navigation, index);
    		 if((typeof vm.OnWizardStepChanged == 'function')) {
    		    vm.OnWizardStepChanged(tab.index(), index, 'WizardNRede_wizWizard_d71b4906824a4788a656b686296fe25d');
    		 }
    		 if(vm.custom) vm.custom.afterWizardStepChanging({ oldIndex: tab.index(), newIndex: index, viewModel: vm, id: 'WizardNRede_wizWizard_d71b4906824a4788a656b686296fe25d'}); //custom Step changed
        },
        onTabShow: function (tab, navigation, index) {
            var total = navigation.find('li').length;
            var current = index + 1;
            var $percent = (current / total) * 100;
            $('#WizardNRede_wizWizard_d71b4906824a4788a656b686296fe25d').find('.progress-bar').css({
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
