define(['plugins/dialog', 'knockout'],
    function (dialog, ko) {

        var saveDialog = function () {
            var _this = this;

            this.input = ko.observable('');

            this.ok = function () {
                if (isNullOrEmpty(_this.input()))
                    return false;

                dialog.close(_this, _this.input());
            }

            this.cancel = function () {
                dialog.close(_this, "");
            }
         };

        saveDialog.show = function () {
            return dialog.show(new saveDialog());
        };

        return saveDialog;
    });

