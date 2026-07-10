define(['durandal/system', 'durandal/app', 'services/logger', 'managers/__auth'],
    function (system, app, logger, managerAuth) {

        var _predefinedFilters = [];
        var _isLoaded = false;

        var loadItems = function (data) {


            _predefinedFilters.push({ id: 'noValue', text: ' ', hasValue: false, dataType: 'All' });

            for (var i = 0; i < data.length; i++) {
                var item = data[i];
                _predefinedFilters.push({ id: item.Condition, text: item.Description, hasValue: item.HasValue, dataType: item.DataType });
            }
            _isLoaded = true;
        };

        var getCachePrefix = function (typeKey, key) {
            return managerAuth.META_ROOT + managerAuth.META_MODULE_ID + '__' + typeKey + '__' + key;
        };

        return {
            predefinedFilters: _predefinedFilters,
            isLoaded: _isLoaded,
            load: function (vm, callback, force) {
                var _this = this;

                if (_this.isLoaded && !force) {
                    if (callback)
                        callback(_this.predefinedFilters);
                    return;
                }

                if (vm && typeof vm.showProcessing === 'function')
                    vm.showProcessing('Carregando filtros predefinidos');

                var cacheKey = getCachePrefix('API', 'LinxFrameworkFiltro/LoadPredefinedFilters', managerAuth.loginInfo.CacheKey);
                var cacheObj = $.ezstorage.get(cacheKey);

                if (cacheObj == null) {

                    $.ajax({
                        type: 'GET',
                        message: "Carregando filtros predefinidos",
                        messageUser: "Carregando filtros predefinidos",
                        headers: managerAuth.getHeaders(),
                        globalError: true,
                        url: managerAuth.getServiceAddress('LinxFrameworkFiltro', 'Linx.Framework.BV') + '/LoadPredefinedFilters',
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
                            $.ezstorage.set(cacheKey, data, { expires: 90 })

                            loadItems(data);

                            if (callback)
                                callback(_this.predefinedFilters);

                            if (vm && typeof vm.closeProcessing === 'function')
                                vm.closeProcessing();
                        }
                    });
                }
                else {
                    loadItems(cacheObj);

                    if (callback)
                        callback(_this.predefinedFilters);

                    if (vm && typeof vm.closeProcessing === 'function')
                        vm.closeProcessing();
                }
            }
        };
    });
