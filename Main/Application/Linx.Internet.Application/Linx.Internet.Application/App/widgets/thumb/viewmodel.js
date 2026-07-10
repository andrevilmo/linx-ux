define(['services/logger', 'durandal/app', 'knockout'],
    function (logger, app, ko) {

        //////////////////////
        // class: VM
        //////////////////////
        var VM = function () {
            var self = this;
            self.currentSettings;

            // Method: activate()
            this.activate = function (settings) {
                self.currentSettings = settings;
            };

            // Method: compositionComplete()
            this.compositionComplete = function () {
                var _currentBrand = $('#brands');
            };

        };

        return VM;
    });


