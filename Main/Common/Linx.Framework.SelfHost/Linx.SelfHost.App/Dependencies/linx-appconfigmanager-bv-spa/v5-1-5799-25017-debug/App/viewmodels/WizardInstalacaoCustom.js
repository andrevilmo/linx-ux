
/* Do not remove this line - File Generation id '1cac1a87-c063-4d71-ba30-9ec3ca3ac3df' - Do not remove this line */
define(['durandal/app', 'services/logger'],
function (app, logger) {
    
    //#region Customize button events [***>]
    var TestaConnBD_Click = function (e) { /* e = { viewModel: object } */ 
    };
    var TestaConnBus_Click = function (e) { /* e = { viewModel: object } */ 
    };
    var ResetService_Click = function (e) { /* e = { viewModel: object } */ 
    };

//***|Dont remove or change this line
    //#endregion customize button events [***<]
    //#region ViewModel Methods Customize
    var afterViewInitializing = function (e) { /* e = { viewModel: viewModel } */
    };
    var afterSelecting = function (e) { /* e = { selectedItem: entity, viewModel: viewModel } */
    };
    var beforeGettingLookup = function (e) { /* e = { cancel: boolean, lookupName: string, jEntitySearch: string, entity: entity, viewModel: viewModel } */
    };
    var afterGettingLookup = function (e) { /* e = { lookupName: string, entity: entity, viewModel: viewModel, userConfirm: bool } */
    };
    //#endregion ViewModel Methods Customize
    //#region Toolbar Methods Customize
    var beforeClearing = function (e) { /* e = { cancel: boolean, viewModel: viewModel } */
    };
    var afterClearing = function (e) { /* e = { dataItem: object, viewModel: viewModel } */
    };
    var beforeQuerying = function (e) { /* e = { cancel: boolean, jEntitySearch: string, viewModel: viewModel } */
    };
    var afterQuerying = function (e) { /* e = { dataItems: [], viewModel: viewModel } */
    };
    var beforeSaving = function (e) { /* e = { cancel: boolean, viewModel: viewModel } */
    };
    var afterSaving = function (e) { /* e = { viewModel: viewModel} */
    };
    var beforeAdding = function (e) { /* e = { cancel: boolean, viewModel: viewModel } */
    };
    var afterAdding = function (e) { /* e = { viewModel: viewModel } */
    };
    var beforeGoingFirst = function (e) { /* e = { cancel: boolean, viewModel: viewModel } */
    };
    var afterGoingFirst = function (e) { /* e = { viewModel: viewModel } */
    };
    var beforeGoingPrevious = function (e) { /* e = { cancel: boolean, viewModel: viewModel } */
    };
    var afterGoingPrevious = function (e) { /* e = { viewModel: viewModel } */
    };
    var beforeGoingNext = function (e) { /* e = { cancel: boolean, viewModel: viewModel } */
    };
    var afterGoingNext = function (e) { /* e = { viewModel: viewModel } */
    };
    var beforeGoingLast = function (e) { /* e = { cancel: boolean, viewModel: viewModel } */
    };
    var afterGoingLast = function (e) { /* e = { viewModel: viewModel } */
    };
    var beforeRemoving = function (e) { /* e = { cancel: boolean, viewModel: viewModel } */
    };
    var afterRemoving = function (e) { /* e = { viewModel: viewModel } */
    };
    var beforeEditing = function (e) { /* e = { cancel: boolean, viewModel: viewModel } */
    };
    var afterEditing = function (e) { /* e = { viewModel: viewModel } */
    };
    var beforeCancelEdition = function (e) { /* e = { cancel: boolean, viewModel: viewModel } */
    };
    var afterCancelEdition = function (e) { /* e = { viewModel: viewModel } */
    };
    var beforePrinting = function (e) { /* e = { cancel: boolean, viewModel: viewModel } */
    };
    var afterPrinting = function (e) { /* e = { viewModel: viewModel } */
    };
    var beforeAddingChild = function (e) { /* e = { cancel: boolean, entityTypeName: string, viewModel: viewModel } */
    };
    var afterAddingChild = function (e) { /* e = { entityTypeName: string, viewModel: viewModel } */
    };
    var beforeRemovingChild = function (e) { /* e = { cancel: boolean, entityTypeName: string, viewModel: viewModel } */
    };
    var afterRemovingChild = function (e) { /* e = { entityTypeName: string, viewModel: viewModel } */
    };
    //#endregion Toolbar Methods Customize
    //#region Wizard Methods Customize
    var afterWizardInitializing = function () {
    };
    var beforeWizardStepChanging = function (e) { /* e = { oldIndex: number based zero, newIndex: number based zero, cancel: boolean, viewModel: viewModel, id: controlName } */
    };
    var afterWizardStepChanging = function (e) { /* e = { oldIndex: number based zero, newIndex: number based zero, viewModel: viewModel, id: controlName } */
    };
    var beforeWizardFinalizing = function (e) { /* e = { cancel: boolean, viewModel: viewModel, id: controlName } */
    };
    var afterWizardFinalizing = function (e) { /* e = { viewModel: viewModel, id: controlName } */
    };
    //#endregion Wizard Methods Customize
    
    var customCtor = function() {
        var custom = {
            //begin custom buttons
            TestaConnBD_Click: TestaConnBD_Click,
            TestaConnBus_Click: TestaConnBus_Click,
                ResetService_Click: ResetService_Click,
//end custom buttons - do not remove this line
            //viewModel
            afterViewInitializing: afterViewInitializing,
            afterSelecting: afterSelecting,
            beforeGettingLookup: beforeGettingLookup,
            afterGettingLookup: afterGettingLookup,
            //toolbar
            beforeQuerying: beforeQuerying,
            afterQuerying: afterQuerying,
            beforeClearing: beforeClearing,
            afterClearing: afterClearing,
            beforeSaving: beforeSaving,
            afterSaving: afterSaving,
            beforeAdding: beforeAdding,
            afterAdding: afterAdding,
            beforeGoingFirst: beforeGoingFirst,
            afterGoingFirst: afterGoingFirst,
            beforeGoingPrevious: beforeGoingPrevious,
            afterGoingPrevious: afterGoingPrevious,
            beforeGoingNext: beforeGoingNext,
            afterGoingNext: afterGoingNext,
            beforeGoingLast: beforeGoingLast,
            afterGoingLast: afterGoingLast,
            beforeRemoving: beforeRemoving,
            afterRemoving: afterRemoving,
            beforeEditing: beforeEditing,
            afterEditing: afterEditing,
            beforeCancelEdition: beforeCancelEdition,
            afterCancelEdition: afterCancelEdition,
            beforePrinting: beforePrinting,
            afterPrinting: afterPrinting,
            beforeAddingChild: beforeAddingChild,
            afterAddingChild: afterAddingChild,
            beforeRemovingChild: beforeRemovingChild,
            afterRemovingChild: afterRemovingChild,
            //wizard
            afterWizardInitializing: afterWizardInitializing,
            beforeWizardStepChanging: beforeWizardStepChanging,
            afterWizardStepChanging: afterWizardStepChanging,
            beforeWizardFinalizing: beforeWizardFinalizing,
            afterWizardFinalizing: afterWizardFinalizing
        };
        return custom;
    }
    
    return customCtor;
});

