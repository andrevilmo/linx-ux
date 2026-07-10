define(['durandal/app', 'plugins/dialog', 'knockout', 'services/logger', 'managers/__auth', 'common'],
    function (app, dialog, ko, logger, managerAuth, common) {

        var modalSupportRequestUrl = function (url) {
            var _this = this;

            this.url = ko.observable(url);

            //Durandal Methods
            this.compositionComplete = function () {
                var btn = document.getElementById('btnCopiar');
                new Clipboard(btn);
            };

            this.activate = function () {
            };

            //buttons
            this.ok = function () {
                dialog.close(this);
            }
        };

        modalSupportRequestUrl.show = function (url) {
            return dialog.show(new modalSupportRequestUrl(url));
        };

        return modalSupportRequestUrl;
    });

