define(['services/logger', 'durandal/app', 'knockout', 'managers/brand', 'managers/__auth'],
    function (logger, app, ko, managerBrand, managerAuth) {

        function format(sourceData) {
            return sourceData.html_select2;
        }

        //////////////////////
        // class: VM
        //////////////////////
        var VM = function () {
            var self = this;
            self.currentSettings;
            self.IdBandeiraRedeDefault = (isNullOrEmpty(managerBrand.IdBandeiraRedeDefault) == false ? managerBrand.IdBandeiraRedeDefault : -1);

            // Method: activate()
            this.activate = function (settings) {
                self.currentSettings = settings;
                self.currentSettings.managerAuth = managerAuth;
            };


            // Method: compositionComplete()
            this.compositionComplete = function () {

                var viewName = self.currentSettings.vm.__moduleId__.replace('viewmodels', 'views');
                var _currentBrand = $("div[data-view='" + viewName + "'] #brand");

                //var _currentBrand = $('#brand');

                if (!managerBrand.BRANDS_VM || managerBrand.BRANDS_VM.length == 0) {
                    self.currentSettings.vm.currentBrands = null;
                    self.currentSettings.vm.setBandeiraRede();
                }
                else if (isNullOrEmpty(self.currentSettings.vm.currentBrands) || self.currentSettings.vm.currentBrands.search(",") > 0) {

                    // existe um bandeira padrao configurada
                    if (self.IdBandeiraRedeDefault > -1)
                        self.currentSettings.vm.currentBrands = self.IdBandeiraRedeDefault;
                    else
                        self.currentSettings.vm.currentBrands = managerBrand.BRANDS_VM[0].id;

                    self.currentSettings.vm.setBandeiraRede();
                }
                //else if (self.IdBandeiraRedeDefault > -1 && self.currentSettings.vm.status() == 'C') {
                //    self.currentSettings.vm.currentBrands = self.IdBandeiraRedeDefault;
                //    self.IdBandeiraRedeDefault = -1;

                //    self.currentSettings.vm.setBandeiraRede();
                //}
                
                _currentBrand.editable({
                    inputclass: 'form-control input-large select2',
                    select2: {
                        minimumResultsForSearch: -1,
                        allowClear: true,
                        formatResult: format,
                        formatSelection: format,
                        escapeMarkup: function (m) {
                            return m;
                        }
                    },
                    type: 'select2',
                    value: self.currentSettings.vm.currentBrands,
                    url: '',
                    source: managerBrand.BRANDS_VM,
                    title: 'Bandeira/Rede:',
                    placement: 'left',
                    onblur: 'submit',
                    highlight: false,
                    showbuttons: false,

                    error: function (data) {
                    },

                    success: function (response, newValue) {
                        self.currentSettings.vm.currentBrands = newValue;
                        self.currentSettings.vm.setBandeiraRede();
                        app.trigger("shell:brand:change");
                    },

                    validate: function (value) {
                        if ($.trim(value) == '')
                            return 'Seleção obrigatória!';
                    },

                    display: function (value, sourceData) {
                        if (!value) {
                            $(this).html("Rede indefinida!");
                            return;
                        }                        
                        $(this).html(managerBrand.searchBrandsVM(value).html);
                    }

                });

                _currentBrand.editable((self.currentSettings.vm.status() == 'C' ? 'enable' : 'disable'));

                // KO subscribe "canQuery"
                self.currentSettings.vm.status.subscribe(function (newValue) {
                    if (isNullOrEmpty(newValue)) newValue = self.currentSettings.vm.status()

                    _currentBrand.editable((newValue == 'C' ? 'enable' : 'disable'))

                    if (newValue == 'C') {
                        if (self.IdBandeiraRedeDefault > -1) {
                            //self.currentSettings.vm.currentBrands = managerBrand.IdBandeiraRedeDefault;

                            self.currentSettings.vm.setBandeiraRede();
                            _currentBrand.editable('option', 'value', self.currentSettings.vm.currentBrands);
                        }
                    }

                });

                //_currentBrand.editable('option', 'value', 123);
            };

        };

        return VM;
    });

