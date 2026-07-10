define(['services/logger', 'durandal/app', 'knockout', 'managers/brand', 'managers/__auth'],
    function (logger, app, ko, managerBrand, managerAuth) {

        //////////////////////
        // class: VM
        //////////////////////
        var VM = function () {
            var self = this;
            self.currentSettings;
            self.hasBrand = ko.observable(false);
            self.ImgRede = ko.observable('');

            self.isVisible = ko.computed(function () {
                return (self.hasBrand() && self.ImgRede().length > 0);
            });


            // Method: activate()
            this.activate = function (settings) {
                self.currentSettings = settings;
                self.hasBrand(self.currentSettings.vm.hasBrand);

                var vm = null;
                if ((self.currentSettings && self.currentSettings.vm && self.currentSettings.vm.currentDataItem()) && isNullOrEmpty(self.currentSettings.vm.currentDataItem().IdBandeiraRede) == false) {
                    var vm = managerBrand.searchBrandsVM(getAbsoluteValue(self.currentSettings.vm.currentDataItem().IdBandeiraRede));
                }
                self.ImgRede(vm == null ? '' : vm.html);

                self.currentSettings.managerAuth = managerAuth;
            };

            // Method: compositionComplete()
            this.compositionComplete = function () {

                // KO subscribe "currentDataItem"
                if (self.currentSettings.vm.hasBrand == true && !self.currentSettings.vm.currentDataItem.isPOCO) {
                    self.currentSettings.vm.currentDataItem.subscribe(function (newValue) {

                        if (newValue && isNullOrEmpty(newValue.IdBandeiraRede) == false) {
                            var vm = managerBrand.searchBrandsVM(getAbsoluteValue(newValue.IdBandeiraRede));
                            self.ImgRede(vm == null ? '' : vm.html);
                        }

                    });
                }

            };

        };

        return VM;
    });

