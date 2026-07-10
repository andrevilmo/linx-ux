define(['services/logger', 'durandal/app', 'knockout', 'managers/brand', 'managers/__auth', 'plugins/router'],
    function (logger, app, ko, managerBrand, managerAuth, router) {

        function format(sourceData) {
            return sourceData.html_select2;
        }

        //////////////////////
        // class: VM
        //////////////////////
        var VM = function () {
            var self = this;
            self.currentSettings;
            self.IdBandeiraRedeDefault = managerBrand.getDefaultBrandId().toString();
            self.Brands = [];

            // Method: activate()
            this.activate = function (settings) {
                self.currentSettings = settings;
                self.currentSettings.managerAuth = managerAuth;
            };

            this.searchBrand = function (pValue) {
                for (var i = 0; i < self.Brands.length; i++) {
                    var item = self.Brands[i];

                    if (item.id === pValue) {
                        return item;
                    }
                }
                return null;
            };

            // Method: compositionComplete()
            this.compositionComplete = function () {

                var viewName = self.currentSettings.vm.__moduleId__.replace('viewmodels', 'views');
                var _currentBrand = $("div[data-view='" + viewName + "'] #brand");

                self.Brands = managerBrand.getBrandVM();

                if (!self.Brands || self.Brands.length === 0) {
                    self.currentSettings.vm.currentBrands(null);
                    self.currentSettings.vm.setBandeiraRede();
                }
                else if (isNullOrEmpty(self.currentSettings.vm.currentBrands()) || self.currentSettings.vm.currentBrands().search(",") > 0) {
                    // existe um bandeira padrao configurada
                    if (self.IdBandeiraRedeDefault > -1)
                        self.currentSettings.vm.currentBrands(self.IdBandeiraRedeDefault);
                    else
                        self.currentSettings.vm.currentBrands(self.Brands[0].id);

                    self.currentSettings.vm.setBandeiraRede();
                }

                setTimeout(function () { resizeToolbar(); }, 300);

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
                    value: self.currentSettings.vm.currentBrands(),
                    url: '',
                    source: self.Brands,
                    title: 'Bandeira/Rede:',
                    placement: 'left',
                    onblur: 'submit',
                    highlight: false,
                    showbuttons: false,

                    error: function (data) {
                    },

                    success: function (response, newValue) {
                        self.currentSettings.vm.currentBrands(newValue);
                        self.currentSettings.vm.setBandeiraRede();
                        setTimeout(function () { resizeToolbar(); }, 10);
                        app.trigger("shell:brand:change", newValue);
                    },

                    validate: function (value) {
                        if ($.trim(value) === '')
                            return 'Seleção obrigatória!';
                    },

                    display: function (value, sourceData) {
                        if (!value) {
                            $(this).html("Rede indefinida!");
                            return;
                        }                        
                        $(this).html(self.searchBrand(value).html);
                    }

                });

                _currentBrand.editable((self.currentSettings.vm.status() === 'C' ? 'enable' : 'disable'));

                // KO subscribe "canQuery"
                self.currentSettings.vm.status.subscribe(function (newValue) {
                    if (isNullOrEmpty(newValue)) newValue = self.currentSettings.vm.status();

                    _currentBrand.editable(newValue === 'C' ? 'enable' : 'disable');
                });
            };

        };

        return VM;
    });

