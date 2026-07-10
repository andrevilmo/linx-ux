define(['durandal/system', 'durandal/app', 'services/logger', 'managers/__auth', 'common'],
    function (system, app, logger, managerAuth, common) {
        return {
            predefinedFilters: [],
            isLoaded: false,
            load: function (vm, callback, force) {
                var _this = this;

                if (_this.isLoaded && !force) {
                    if (callback)
                        callback(_this.predefinedFilters);
                    return;
                }

                

                if (vm && typeof vm.showProcessing === 'function')
                    vm.showProcessing('Carregando filtros pré-definidos');

                $.ajax({
                    type: 'GET',
                    messageUser: "Carregando filtros pré-definidos",
                    globalError: true,
                    url: managerAuth.getServiceAddress('LinxFrameworkFiltro/LoadPredefinedFilters'),
                    data: {},
                    dataType: 'json',
                    async: true,
                    cache: false,

                    error: function (jqXHR, textStatus, errorThrown) {
                        if (vm && typeof vm.closeProcessing === 'function')
                            vm.closeProcessing();

                        if (callback)
                            callback(_this.predefinedFilters);
                    },

                    success: function (data) {
                        _this.predefinedFilters.push({ id: 'noValue', text: ' ', hasValue: false, dataType: 'All' });

                        for (var i = 0; i < data.length; i++) {
                            var item = data[i];
                            _this.predefinedFilters.push({ id: item.Condition, text: item.Description, hasValue: item.HasValue, dataType: item.DataType });
                        }

                        _this.isLoaded = true;
                        if (callback)
                            callback(_this.predefinedFilters);

                        if (vm && typeof vm.closeProcessing === 'function')
                            vm.closeProcessing();
                    }
                });

            }
        };
    });
