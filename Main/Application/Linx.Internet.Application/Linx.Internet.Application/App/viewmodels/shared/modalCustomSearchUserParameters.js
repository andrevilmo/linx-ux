define(['durandal/app', 'plugins/dialog', 'knockout', 'services/logger', 'managers/__auth', 'common'],
    function (app, dialog, ko, logger, managerAuth, common) {

        var customSearchParameters = function (vm, searches) {
            var _this = this;
            this.customSearchVm = vm;

            this.searches = searches;

            this.isLoading = true;

            //Durandal Methods
            this.compositionComplete = function () {
                _this.isLoading = false;
            };

            this.activate = function () {

            };

            //buttons
            this.ok = function () {

                _this.customSearchVm.hasErrors(false);

                for (index = 0; index < _this.searches.length; ++index) {
                    var item = _this.searches[index];
                    for (var ii = 0; ii < item.parametroValor.length; ii++) {
                        var parameter = item.parametroValor[ii];
                        _this.customSearchVm.validateInputValue('#input_value_param_' + parameter.uidLine, parameter.operatorId, parameter.queryDataType, parameter.value);
                    }
                };

                if (_this.customSearchVm.hasErrors()) {
                    app.showMessage("Uma ou mais condições apresentam erro (campos em vermelho).\n\nPor favor verifique. ", 'Atenção', ['Ok']);
                }
                else{
                    dialog.close(this, true);
                }
            };

            this.cancel = function () {
                dialog.close(this, false);
            }

            this.afterRenderParametroValor = function (element, data) {
                var object = '#input_value_param_' + data.uidLine;
                _this.customSearchVm.contentInfo(object, data.operator, 1, data.operatorId, data.queryDataType);
            }

            this.onChangeInputValue = function (data) {
                _this.customSearchVm.removeErrorClass('#input_value_param_' + data.uidLine);
                if (!_this.isLoading) {
                    _this.customSearchVm.validateInputValue('#input_value_param_' + data.uidLine, data.operatorId, data.queryDataType, data.value);
                }
            }
        };

        customSearchParameters.show = function (vm, nodePath) {
            return dialog.show(new customSearchParameters(vm, nodePath));
        };

        return customSearchParameters;
    });

