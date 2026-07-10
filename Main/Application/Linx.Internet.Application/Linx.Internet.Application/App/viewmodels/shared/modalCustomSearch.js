define(['durandal/app', 'plugins/dialog', 'knockout', 'services/logger', 'managers/__auth', 'common', 'services/CustomSearchDataDomains', 'viewmodels/shared/modalCustomSearchSaveDialog', 'viewmodels/shared/modalCustomSearchTreeView',
    'viewmodels/shared/modalCustomSearchUserParameters', 'managers/predefinedFilters'],
    function (app, dialog, ko, logger, managerAuth, common, dataDomains, saveDialog, modalTreeView, modalUserParameters, managerPredefined) {

        var customSearch = function (vm, uiDataContext, filterOnly) {
            var _this = this;

            //Durandal Methods
            this.compositionComplete = function () {

                $('#edtDescFiltro').editable({
                    type: 'text',
                    mode: 'popup',
                    value: '',
                    placement: 'bottom',
                    success: function (response, newValue) {
                    }
                })

                if (_this.isFilterOnly) {
                    _this.divVisibility("divFiltros", false);
                    _this.divVisibility("divEdicao", true);
                }
            };

            this.activate = function () {
                //Filtros predefinidos
                vm.showProcessing('Carregando filtros predefinidos');
                managerPredefined.load(vm, function (data) { _this.afterLoadPredefinedFilters(data) }, false);
            };

            this.afterLoadPredefinedFilters = function (data) {
                var tcsFiltroWhere = 'TcsFiltro{NomeEntidadeBm#In#S"' + _this.nomeEntidadeBM + '","' + _this.nomeEntidadeBV + '"}';
                _this.predefinedFilters(data);

                vm.closeProcessing();

                $.ajax({
                    type: 'GET',
                    messageUser: "Carregando parâmetros",
                    globalError: true,
                    headers: managerAuth.getHeaders(),
                    url: managerAuth.getServiceAddress('LinxFrameworkFiltro', 'Linx.Framework.BV') + '/LoadParameters',
                    data: {},
                    dataType: 'json',
                    async: true,
                    cache: false,

                    error: function (jqXHR, textStatus, errorThrown) {
                        vm.closeProcessing();
                    },

                    success: function (data) {
                        _this.parameterList.push({ id: 'noValue', text: '', dataType: 'All' });

                        for (var i = 0; i < data.length; i++) {
                            var item = data[i];
                            _this.parameterList.push({ id: item.TituloParametro, text: item.TituloParametro, dataType: item.DataType });
                        }
                        vm.closeProcessing();

                        if (!_this.isFilterOnly) {
                            vm.showProcessing('Buscando filtros');

                            // Filtros disponíveis
                            $.ajax({
                                type: 'GET',
                                messageUser: "Buscando filtros",
                                globalError: true,
                                headers: managerAuth.getHeaders(),
                                url: managerAuth.getServiceAddress('LinxFrameworkFiltro', 'Linx.Framework.BV') + '/GetTcsFiltroByEntitySearchNoAssociations',
                                data: {
                                    jEntitySearch: tcsFiltroWhere
                                },
                                dataType: 'json',
                                async: true,
                                cache: false,

                                error: function (jqXHR, textStatus, errorThrown) {
                                    vm.closeProcessing();
                                },

                                success: function (data) {
                                    for (var i = 0; i < data.length; i++) {
                                        var item = data[i];
                                        var filtroTraduzido = item.LxTipoFiltro == 1 ? _this.translateSearch(item.ComandoFiltro) : _this.translateBMSearch(item.ComandoSerializado.replace(/\[#\]/g, '"'));
                                        _this.tcsFiltro.push({ id: item.IdFiltro, idFiltro: 1, text: _this.getFilterText(item.DescFiltro, item.LxTipoFiltro), disabled: false, descFiltro: item.DescFiltro, comandoFiltro: item.ComandoFiltro, comandoSerializado: item.ComandoSerializado.replace(/\[#\]/g, '"'), uidObjeto: item.UidObjeto, uidUsuario: item.UidUsuario, lxTipoFiltro: item.LxTipoFiltro, nomeEntidadeBm: item.NomeEntidadeBm, comandoFiltroAnterior: item.ComandoFiltro, filtroTraduzido: filtroTraduzido, parametros: item.Parametros });
                                    }
                                    _this.loadSearches();
                                    vm.closeProcessing();
                                }
                            });
                        }
                        else {
                            _this.updateDivEdicaoButtons(1, 2);
                        }

                    }
                });
            }

            //Select2
            //tableFiltros - afterRender
            this.afterRenderTable = function (element, data) {
                var uid = data.uidLinha;
                var sel = '#select_' + uid;

                $(sel).select2({
                    placeholder: "Filtros",
                    openOnEnter: true,
                    width: "off",
                    escapeMarkup: function (m) {
                        return m;
                    },
                    data: _this.tcsFiltro()
                });

                //adiciona indice para identificar qual linha será editada
                $(sel).attr('index', uid);

                //adiciona onChange ao item
                $(sel).on("change", _this.onChangeSelectFilters);

                $(sel).select2("val", "");
            };

            //select2_filtro - onChange
            this.onChangeSelectFilters = function (e) {
                if (!e.added)
                    return;

                var index = $(e.currentTarget).attr('index');
                var item = _this.getfilterList(index);

                if (!isNullOrEmpty(item)) {
                    item.comandoFiltro = e.added.comandoFiltro;
                    item.descFiltro = e.added.descFiltro;
                    item.uidFiltro = e.added.id;
                    item.comandoSerializado = e.added.comandoSerializado;
                    item.idFiltro = e.added.idFiltro;
                    item.lxTipoFiltro = e.added.lxTipoFiltro;
                    item.filtroTraduzido = e.added.filtroTraduzido;
                    item.parametros = e.added.parametros;

                    if (!isNullOrEmpty(item.parametros)) {
                        var items = item.parametros.split(";");
                        for (var i = 0; i < items.length; i++) {
                            var values = items[i].split("][");
                            item.parametroValor.push({ name: values[0], value: '', description: values[1], operator: values[2], uidLine: Math.uuid(), operatorId: values[3], queryDataType: values[4] });
                        }
                    }

                    _this.updateDivFiltrosControls(item);
                    _this.updateTranslatedSearch(item.uidLinha, item.filtroTraduzido);
                }
            };

            //select2_filtro update value
            this.updateSelect2Value = function (uidLinha, idFiltro, uidFiltro, comandoFiltro, comandoSerializado, lxTipoFiltro, filtroTraduzido) {
                var sel = '#select_' + uidLinha;
                $(sel).select2("val", uidFiltro);
                $(sel).select2("enable", idFiltro > 0 && idFiltro != 99);
                _this.updateTranslatedSearch(uidLinha, filtroTraduzido);
            };

            //tableEdicao
            this.afterRenderTableEditor = function (element, data) {
                var uid = data.uidLine;

                //input type
                var sel = "#input_type_" + uid;

                $(sel).editable({
                    inputclass: 'form-control input-large',
                    source: [
                        { value: 1, text: 'Expressão' },
                        { value: 2, text: 'Parâmetro do usuário' },
                        { value: 3, text: 'Tabela de parâmetros' },
                        { value: 4, text: 'Condições pré-definidas' }
                    ],
                    type: 'select',
                    url: '',
                    title: '',
                    placement: 'bottom',
                    onblur: 'submit',
                    highlight: false,
                    showbuttons: false,
                    emptytext: "<i class='fa fa-wrench'></i>",
                    value: 1,
                    error: function (data) {
                    },

                    success: function (response, newValue) {
                        var searchCondition = _this.getSearchCondition(uid);
                        var oldValue = searchCondition.inputType();

                        searchCondition.inputType(newValue);

                        searchCondition.availableOperators(_this.getOperators(searchCondition.queryDataType(), searchCondition.inputType() == "4"));

                        _this.updateInputContentInfo(searchCondition);

                        switch (newValue) {
                            case "1":
                                if (searchCondition.queryDataType() == "B") {
                                    _this.onChangeSelectBoolean(searchCondition);
                                }
                                else {
                                    searchCondition.currentValue("");
                                }
                                break;
                            case "2":
                                searchCondition.currentValue("");
                                //searchCondition.currentValue('Parâmetro do Usuário ' + _this.getUserParameterCount());
                                break;
                            case "3":
                                _this.onChangeSelectParametro(searchCondition);
                                break;
                            case "4":
                                searchCondition.currentValue("");
                                _this.onChangeSelectPredefinido(searchCondition);
                                break;
                        }

                        switch (oldValue) {
                            case "1":
                                break;
                            case "2":
                                searchCondition.userParameterValue = "";
                                break;
                            case "3":
                                searchCondition.parameterValue("");
                                break;
                            case "4":
                                searchCondition.predefinedFilter("");
                                searchCondition.predefinedFilterValue("");
                                break;
                        }

                    },

                    validate: function (value) {
                    },

                    display: function (value, sourceData) {
                        $(this).html("<i class='fa fa-wrench'></i>");
                    }

                });
                $(sel).attr('index', uid);
            };

            //input_value - change
            this.onChangeInputValue = function (data) {
                _this.removeErrorClass('#input_value_' + data.uidLine);
                _this.validateSearchConditionInputValue(data);
            }

            //select_boolean - change
            this.onChangeSelectBoolean = function (data) {
                if (!data)
                    return;

                if (data.queryDataType() == "B") {
                    var sel = "#select_boolean_" + data.uidLine;
                    data.currentValue($(sel).val());
                }
            }

            //select_operator - change
            this.onChangeSelectOperator = function (data) {
                if (!data.operator())
                    return;

                _this.updateInputContentInfo(data);
                _this.removeErrorClass('#cmbOperator_' + data.uidLine);
                _this.removeErrorClass('#input_value_' + data.uidLine);

                if (data.operator().hasValue && (data.isCustomSearch() || data.queryDataType() != "T")) {
                    $('#input_type_' + data.uidLine).editable('enable');
                }
                else {
                    $('#input_type_' + data.uidLine).editable('setValue', "1");
                    $('#input_type_' + data.uidLine).editable('disable');
                }
            }

            //select_parametro - change
            this.onChangeSelectParametro = function (data) {
                var value = $('#select_parametro_' + data.uidLine).val();

                if (data.inputType() != 3 || !value || isNullOrEmpty(value))
                    return;

                data.currentValue(value);
                _this.removeErrorClass('#select_parametro_' + data.uidLine);
            }

            //select_predefinido - change
            this.onChangeSelectPredefinido = function (data) {

                var value = $('#select_predefinido_' + data.uidLine).val();

                if (data.inputType() != 4 || !value || isNullOrEmpty(value))
                    return;

                data.currentValue(value);
                data.predefinedFilter(_this.getPredefinedFilter(value));

                if (!data.predefinedFilter().hasValue) {
                    data.predefinedFilterValue('');
                }

                _this.removeErrorClass('#select_predefinido_' + data.uidLine);
                _this.removeErrorClass('#input_predefinido_' + data.uidLine);
            }

            this.onChangeInputPredefinido = function (data) {
                _this.removeErrorClass('#input_predefinido_' + data.uidLine);
            }

            //select2_adapter/column update value
            this.updateSelect2ValueEditor = function (searchCondition) {
                var sel;

                //input Type
                $('#input_type_' + searchCondition.uidLine).editable('setValue', searchCondition.inputType());

                //se é pesquisa
                if (searchCondition.isCustomSearch()) {
                    $('#input_type_' + searchCondition.uidLine).editable('disable');
                }
                else {
                    $('#input_type_' + searchCondition.uidLine).editable('enable');
                }

                //Boolean
                if (searchCondition.queryDataType() == "B" && searchCondition.inputType() == 1) {
                    sel = '#select_boolean_' + searchCondition.uidLine;
                    $(sel).val(searchCondition.currentValue());
                }

                //Filtro pré-definido
                if (searchCondition.inputType() == 4) {
                    sel = '#select_predefinido_' + searchCondition.uidLine;
                    $(sel).val(searchCondition.currentValue());
                }
            };

            //data
            this.nomeEntidadeBM = vm.rootBmTypeName;

            this.nomeEntidadeBV = vm.rootDataTypeName;

            this.uidObjeto = "00000000-0000-0000-0000-000000000000" //"01034539-D3DE-4EF2-A5C5-2D226E4FC680";

            this.uidUsuario = managerAuth.loginInfo.UidUsuario;

            this.isFilterOnly = filterOnly;

            this.hasErrors = ko.observable(false);

            var searchCondition = function () {
                this.entityDescription = ko.observable("...");
                this.entity = ko.observable("...");
                this.entityPath = ko.observable("");
                this.availableOperators = ko.observableArray([]);
                this.queryDataType = ko.observable("");
                this.predefinedFilters = ko.observableArray([]);
                this.predefinedFilterValue = ko.observable("");
                this.predefinedFilter = ko.observable("");
                this.parameters = ko.observableArray([]);
                this.operator = ko.observable("");
                this.condition = ko.observable("");
                this.currentValue = ko.observable("");
                this.lParameter = ko.observable("");
                this.rParameter = ko.observable("");
                this.uidLine = Math.uuid();
                this.inputType = ko.observable("1");
                this.parameterValue = ko.observable("");
                this.userParameterValue = "";

                this.enableCurrentValue = function () {
                    if (this.operator() && this.operator().hasValue) { return true; }
                    else {
                        this.currentValue("");
                        this.predefinedFilter("");
                        this.predefinedFilterValue("");
                        this.inputType("1");
                        this.parameterValue("");
                        this.userParameterValue = "";
                        return false;
                    }
                }

                this.isBoolean = function () {
                    return (this.queryDataType() == "B");
                }

                this.isCustomSearch = function () {
                    return (strLeft(this.entity(), 4).toUpperCase() == "*SID");
                }

                this.isNullOperator = function () {
                    return this.operator().id.toUpperCase().contains("NULL");
                }

                this.isEmptyOperator = function () {
                    return this.operator().id.toUpperCase() == "EMPTY";
                }

                this.index = function () {
                    return _this.searchConditions.indexOf(this);
                }

            };

            this.tcsFiltro = ko.observableArray([])

            this.filterList = ko.observableArray([]);

            this.searchConditions = ko.observableArray([new searchCondition()]);

            this.operators = dataDomains.getItems('FilterOperator');

            this.editedFilterListItem = ko.observable();

            this.idSearchCondition = 0;

            this.predefinedFilters = ko.observableArray([]);

            this.parameterList = ko.observableArray([]);

            this.currentComandoFiltro = "";

            this.currentComandoSerializado = "";

            this.currentParametros = "";

            this.currentParametroValor = [];

            //Gets
            this.getOperators = function (dataType, predefinedOnly) {
                var operators = [];
                var index;
                var counter = 0;

                for (index = 0; index < _this.operators.length; ++index) {
                    if (_this.operators[index].availableTypes.contains(dataType) && (predefinedOnly ? _this.operators[index].allowedInPredefined : true)) {
                        operators[counter] = _this.operators[index];
                        counter++;
                    }
                }
                return operators;
            };

            this.getfilterList = function (uuid) {
                var index;
                for (index = 0; index < _this.filterList().length; ++index) {
                    if (_this.filterList()[index].uidLinha == uuid)
                        return _this.filterList()[index];
                };
                return null;
            }

            this.getSearchCondition = function (uid) {
                var index;
                for (index = 0; index < _this.searchConditions().length; ++index) {
                    if (_this.searchConditions()[index].uidLine == uid)
                        return _this.searchConditions()[index];
                };
                return null;
            }

            this.getFilterOperator = function (operator) {
                var index;
                for (index = 0; index < _this.operators.length; ++index) {
                    if (_this.operators[index].id == operator)
                        return _this.operators[index];
                };
                return null;
            };

            this.getSearchConditionIdFiltro = function () {
                if (_this.idSearchCondition == 0) {
                    var index;
                    var idFiltro = 0;
                    for (index = 0; index < _this.filterList().length; ++index) {
                        if (_this.filterList()[index].idFiltro < idFiltro)
                            idFiltro = _this.filterList()[index].idFiltro;
                    };
                    _this.idSearchCondition = idFiltro - 1;
                }
                else {
                    _this.idSearchCondition = _this.idSearchCondition - 1;
                }

                return _this.idSearchCondition;
            };

            this.getTcsFiltro = function (uidFiltro) {
                var index;
                for (index = 0; index < _this.tcsFiltro().length; ++index) {
                    if (_this.tcsFiltro()[index].id == uidFiltro)
                        return _this.tcsFiltro()[index];
                };
                return null;
            };

            this.getSerializedSearchConditions = function () {
                _this.hasErrors(false);
                var serializedString = "";

                _this.currentComandoFiltro = "";
                _this.currentComandoSerializado = "";
                _this.currentParametros = "";
                _this.currentParametroValor = [];

                if (_this.searchConditions().length == 0)
                    return "";

                var search = ko.observableArray([]);
                var rParameters = 0;
                var lParameters = 0;

                for (index = 0; index < _this.searchConditions().length; ++index) {
                    var current = _this.searchConditions()[index];

                    _this.validateSearchCondition(current)

                    if (!_this.hasErrors()) {
                        search.push({ condition: current.condition(), lParameter: current.lParameter(), entity: current.entity(), entityDescription: current.entityDescription(), operator: current.operator().id, queryDataType: current.queryDataType(), currentValue: current.currentValue(), predefinedValue: current.predefinedFilterValue(), rParameter: current.rParameter(), path: current.entityPath(), inputType: current.inputType() });

                        //Condition
                        serializedString = serializedString + (serializedString.length > 0 ? ';' + current.condition() : '');

                        //Left Parameter
                        for (var i = 0; i < current.lParameter().trim().length; i++) {
                            serializedString = serializedString + (serializedString.length > 0 ? ';' : '') + '(';
                            lParameters++;
                        }

                        var predefinedFilter = _this.getPredefinedFilter(current.currentValue());
                        serializedString = serializedString + (serializedString.length > 0 ? ';' : '') + current.entity();

                        if (current.isCustomSearch()) {
                            continue;
                        }

                        if (current.isNullOperator()) {
                            serializedString = serializedString + '#' + (current.operator().id === "== null" ? "==" : "!=") + "#Snull";
                        }
                        else {
                            serializedString = serializedString + '#' + (current.isEmptyOperator() ? "==" : current.operator().id) + '#' + current.queryDataType();

                            switch (current.inputType()) {
                                case "1":
                                    serializedString = serializedString + current.currentValue();
                                    break;
                                case "2":
                                    serializedString = serializedString + "$UserParam$" + current.currentValue();

                                    var items = $.grep(_this.currentParametroValor, function (element, index) { return element.name == current.currentValue() });
                                    if (items.count() > 0) {
                                        serializedString = "";
                                        app.showMessage("Encontradas condições com o mesmo nome de Parâmetro de usuário.\n\nPor favor verifique. ", 'Atenção', ['Ok']);
                                        return;
                                    }
                                    _this.currentParametros = _this.currentParametros + (isNullOrEmpty(_this.currentParametros) ? "" : ";") + current.currentValue() + "][" + current.entityDescription() + "][" + current.operator().name + "][" + current.operator().id + "][" + current.queryDataType();
                                    _this.currentParametroValor.push({ name: current.currentValue(), value: current.userParameterValue, description: current.entityDescription(), operator: current.operator().name, uidLine: Math.uuid(), operatorId: current.operator().id, queryDataType: current.queryDataType() });
                                    break;
                                case "3":
                                    serializedString = serializedString + '$Param$' + current.currentValue();
                                    break;
                                case "4":
                                    serializedString = serializedString + '$' + current.currentValue() + '$' + (predefinedFilter && predefinedFilter.hasValue ? current.predefinedFilterValue() : '');
                                    break;
                            }
                        }

                        //Right Parameter
                        for (var i = 0; i < current.rParameter().trim().length; i++) {
                            serializedString = serializedString + ';)';
                            rParameters++;
                        }
                    }
                }

                if (_this.hasErrors()) {
                    serializedString = "";
                    app.showMessage("Uma ou mais condições apresentam erro (campos em vermelho).\n\nPor favor verifique. ", 'Atenção', ['Ok']);
                }
                else {
                    if (lParameters != rParameters) {
                        serializedString = "";
                        app.showMessage("A quantidade de parênteses está inconsistente.\n\n" + lParameters + " à esquerda e " + rParameters + " à direita.", "Atenção", ['Ok']);
                    }
                }

                if (!isNullOrEmpty(serializedString)) {
                    serializedString = '*{' + serializedString + '}';
                }

                _this.currentComandoFiltro = serializedString;
                _this.currentComandoSerializado = JSON.stringify(search());
            };

            this.getPredefinedFilters = function (dataType) {
                var filters = [];
                var index;
                var counter = 0;

                if (jQuery.inArray(dataType, ["S", "C", "T"]) >= 0) {
                    var itemDataType = dataType == "T" ? "DateTime" : "String";

                    for (index = 0; index < _this.predefinedFilters().length; ++index) {
                        if (_this.predefinedFilters()[index].dataType == itemDataType || _this.predefinedFilters()[index].dataType == "All") {
                            filters[counter] = _this.predefinedFilters()[index];
                            counter++;
                        }
                    }
                }
                return filters;
            }

            this.getPredefinedFilter = function (predefinedFilter) {
                var index;
                for (index = 0; index < _this.predefinedFilters().length; ++index) {
                    if (_this.predefinedFilters()[index].id == predefinedFilter)
                        return _this.predefinedFilters()[index];
                };
                return null;
            }

            this.getParameters = function (dataType) {
                var parameters = [];
                var index;
                var counter = 0;

                var itemDataType = jQuery.inArray(dataType, ["S", "C"]) >= 0 ? "S" : jQuery.inArray(dataType, ["L", "H", "I", "Y", "D", "F"]) >= 0 ? "I" : dataType;

                for (index = 0; index < _this.parameterList().length; ++index) {
                    if (_this.parameterList()[index].dataType == itemDataType || _this.parameterList()[index].dataType == "All") {
                        parameters[counter] = _this.parameterList()[index];
                        counter++;
                    }
                }
                return parameters;
            }

            this.getFilterText = function (descFiltro, lxTipoFiltro) {
                return descFiltro + " - (" + (lxTipoFiltro == 1 ? "BV" : "BM") + ")";
            }

            this.getUserParameterCount = function () {
                var items = $.grep(_this.searchConditions(), function (element, index) { return element.inputType() == "2" });
                return items.count();
            }

            //buttons
            //divFiltros
            this.addFilterListItem = function () {
                _this.addFilterList("&&", "", 0, "", "", " ", " ", "", 0, 2, "");
            }

            this.clearFilterList = function () {
                _this.filterList.remove(function (item) { return item.idFiltro != 99 })

                //removes temporary filters
                _this.tcsFiltro.remove(function (item) { return item.idFiltro < 0 });
                _this.idSearchCondition = 0;
            }

            this.removeFilterListItem = function (line) {

                if (line.idFiltro < 0) {
                    var current = _this.getTcsFiltro(line.uidFiltro)
                    _this.tcsFiltro.remove(current);
                    _this.idSearchCondition = 0;
                }
                _this.filterList.remove(line);
            }

            this.editFilterListItem = function (line) {
                //divs
                _this.divVisibility("divFiltros", false);
                _this.divVisibility("divEdicao", true);
                //buttons
                _this.updateDivEdicaoButtons(line.idFiltro, line.lxTipoFiltro);
                //
                _this.editedFilterListItem(line);
                //
                _this.divVisibility("divTranslatedSearch", line.lxTipoFiltro == 1);
                _this.divVisibility("divTableSearch", line.lxTipoFiltro == 2);

                if (line.lxTipoFiltro == 1) {
                    $("#pTranslatedSearch").text(_this.translateSearch(line.comandoFiltro));
                }
                else {
                    _this.loadSearchConditions(line);
                }
                //
                $('#edtDescFiltro').editable('setValue', line.descFiltro);
                $('#edtDescFiltro').editable(line.idFiltro > 0 ? 'enable' : 'disable');
            }

            this.ok = function () {
                //remove condições em branco
                _this.filterList.remove(function (item) { return isNullOrEmpty(item.comandoFiltro) })

                var searches = $.grep(_this.filterList(), function (element, index) { return !isNullOrEmpty(element.parametros) });

                if (searches.count() > 0) {

                    modalUserParameters.show(_this, searches).then(function (success) {
                        if (success) {
                            _this.closeSearch();
                        }
                    });
                }
                else {
                    _this.closeSearch();
                }
            }

            this.cancel = function () { dialog.close(this); }

            this.saveFilter = function (line) {
                saveDialog.show().then(function (response) {

                    if (isNullOrEmpty(response))
                        return;

                    //_this.addFilterList("&&", line.comandoFiltro, Math.uuid(), response, "", "", "", "", 1, 1, _this.translateSearch(line.comandoFiltro));
                    _this.addFilterList("&&", line.comandoFiltro, 1, response, "", "", "", "", 1, 1, _this.translateSearch(line.comandoFiltro));
                    _this.saveTcsFiltro(_this.filterList()[_this.filterList().length - 1], _this.insertFilterCallback, 'Added');
                });
            }

            //divEdicao
            this.saveSearch = function () {
                _this.getSerializedSearchConditions();

                if (isNullOrEmpty(_this.currentComandoFiltro))
                    return;

                saveDialog.show().then(function (response) {

                    if (isNullOrEmpty(response))
                        return;

                    _this.editedFilterListItem().comandoFiltro = _this.currentComandoFiltro;
                    _this.editedFilterListItem().comandoSerializado = _this.currentComandoSerializado;
                    _this.editedFilterListItem().descFiltro = response;
                    //_this.editedFilterListItem().uidFiltro = _this.editedFilterListItem().uidLinha;
                    _this.editedFilterListItem().uidFiltro = -1;
                    _this.editedFilterListItem().idFiltro = 1;
                    _this.editedFilterListItem().lxTipoFiltro = 2;
                    _this.editedFilterListItem().filtroTraduzido = _this.translateBMSearch(_this.editedFilterListItem().comandoSerializado);
                    _this.editedFilterListItem().parametros = null;
                    _this.saveTcsFiltro(_this.editedFilterListItem(), _this.insertFilterCallback, 'Added')
                });
            }

            this.deleteSearch = function () {
                app.showMessage('Confirma exclusão do filtro?', 'Alerta', ['Yes', 'No'])
                    .then(function (selectedOption) {
                        if (selectedOption === 'Yes') {
                            _this.saveTcsFiltro(_this.editedFilterListItem(), _this.deleteFilterCallback, 'Deleted')
                        }
                    });
            }

            this.addSearchCondition = function () {
                _this.searchConditions.push(new searchCondition());
            };

            this.addSearchConditionAt = function (line) {
                var index = _this.searchConditions.indexOf(line);
                if (index >= 0)
                    _this.searchConditions.splice(index, 0, new searchCondition());
            }

            this.removeSearchConditionAt = function (line) {
                _this.searchConditions.remove(line);
            }

            this.okEditor = function () {

                if (_this.editedFilterListItem().lxTipoFiltro == 2) {

                    _this.getSerializedSearchConditions();

                    if (isNullOrEmpty(_this.currentComandoFiltro)) {
                        return;
                    };

                    if (_this.isFilterOnly) {
                        dialog.close(this, _this.currentComandoFiltro);
                        return;
                    };

                    _this.editedFilterListItem().comandoFiltro = _this.currentComandoFiltro;
                    _this.editedFilterListItem().comandoSerializado = _this.currentComandoSerializado;
                    _this.editedFilterListItem().parametros = _this.currentParametros;
                    _this.editedFilterListItem().parametroValor = _this.currentParametroValor;

                }

                _this.editedFilterListItem().filtroTraduzido = (_this.editedFilterListItem().lxTipoFiltro == 1 ? _this.translateSearch(_this.editedFilterListItem().comandoFiltro) : _this.translateBMSearch(_this.editedFilterListItem().comandoSerializado));
                _this.editedFilterListItem().descFiltro = $('#edtDescFiltro').editable('getValue').edtDescFiltro;

                if (_this.editedFilterListItem().idFiltro == 0) {
                    var idFiltro = _this.getSearchConditionIdFiltro();
                    _this.editedFilterListItem().idFiltro = idFiltro;
                    //_this.editedFilterListItem().uidFiltro = _this.editedFilterListItem().uidLinha;
                    _this.editedFilterListItem().uidFiltro = idFiltro;
                    _this.addTempTcsFiltro(idFiltro, _this.currentComandoFiltro, _this.currentComandoSerializado, _this.editedFilterListItem().uidLinha, _this.editedFilterListItem, _this.currentParametros);
                    _this.updateSelect2Value(_this.editedFilterListItem().uidLinha, idFiltro, _this.editedFilterListItem().uidFiltro, _this.currentComandoFiltro, _this.currentComandoSerializado, _this.editedFilterListItem().lxTipoFiltro, _this.editedFilterListItem().filtroTraduzido);
                }
                else {
                    var currentTcsFiltro = _this.getTcsFiltro(_this.editedFilterListItem().uidFiltro);
                    currentTcsFiltro.comandoFiltroAnterior = currentTcsFiltro.comandoFiltro;
                    currentTcsFiltro.comandoFiltro = _this.editedFilterListItem().comandoFiltro;
                    currentTcsFiltro.comandoSerializado = _this.editedFilterListItem().comandoSerializado;
                    currentTcsFiltro.filtroTraduzido = _this.editedFilterListItem().filtroTraduzido;

                    if (currentTcsFiltro.idFiltro > 0 && (currentTcsFiltro.comandoFiltro != currentTcsFiltro.comandoFiltroAnterior || currentTcsFiltro.descFiltro != _this.editedFilterListItem().descFiltro)) {
                        currentTcsFiltro.descFiltro = _this.editedFilterListItem().descFiltro;
                        currentTcsFiltro.text = _this.getFilterText(currentTcsFiltro.descFiltro, currentTcsFiltro.lxTipoFiltro);

                        _this.saveTcsFiltro(_this.editedFilterListItem(), _this.updateFilterCallback, 'Added');
                        return;
                    }
                    else {
                        _this.updateSelect2Value(_this.editedFilterListItem().uidLinha, _this.editedFilterListItem().idFiltro, _this.editedFilterListItem().uidFiltro, _this.editedFilterListItem().comandoFiltro, _this.editedFilterListItem().comandoSerializado, _this.editedFilterListItem().lxTipoFiltro, _this.editedFilterListItem().filtroTraduzido);
                    }
                }
                //divs
                _this.divVisibility("divEdicao", false);
                _this.divVisibility("divFiltros", true);
            }

            this.cancelEditor = function () {

                if (_this.isFilterOnly) {
                    dialog.close(this, "");
                }
                else {

                    //divs
                    _this.divVisibility("divEdicao", false);
                    _this.divVisibility("divFiltros", true);
                    //clear items
                    _this.searchConditions.removeAll();
                };
            }

            //
            this.closeSearch = function () {

                vm.dataToolbar.customSearchResult.searchDefinition = "";
                vm.dataToolbar.customSearchResult.serializedSearch = "";
                vm.dataToolbar.hasCustomSearches(false);

                var fullSearch = "";
                var translatedSearch = "";
                var searchList = ko.observableArray([]);
                var lParameters = 0;
                var rParameters = 0;

                //remove condições em branco
                //_this.filterList.remove(function (item) { return isNullOrEmpty(item.comandoFiltro) })

                for (var i = 0; i < _this.filterList().length; i++) {
                    var searchItem = _this.filterList()[i];

                    if (searchItem.idFiltro == 99) {
                        continue;
                    }

                    var search = "";

                    searchList.push({ condition: searchItem.condition, lParameter: searchItem.lParameter, rParameter: searchItem.rParameter, idFiltro: searchItem.idFiltro, descFiltro: (searchItem.idFiltro < 0 ? searchItem.descFiltro : ""), uidFiltro: searchItem.uidFiltro, comandoFiltro: (searchItem.idFiltro < 0 ? searchItem.comandoFiltro : ""), comandoSerializado: (searchItem.idFiltro < 0 ? searchItem.comandoSerializado : ""), lxTipoFiltro: searchItem.lxTipoFiltro, parametros: searchItem.parametros, parametroValor: searchItem.parametroValor })

                    //condition
                    if (isNullOrEmpty(fullSearch) && searchItem.condition != "&&") {
                        app.showMessage('O operador da primeira condição deve ser "E"', 'Atenção', ['Ok']);
                        return;
                    }

                    search = search + searchItem.condition + "{}";

                    if (!isNullOrEmpty(fullSearch)) {
                        translatedSearch = translatedSearch + (searchItem.condition == "&&" ? " e " : " ou ");
                    }

                    //Left Parameter
                    for (var p = 0; p < searchItem.lParameter.trim().length; p++) {
                        search = search + '({}';
                        lParameters++;
                        translatedSearch = translatedSearch + '(';
                    }

                    //Search
                    if (searchItem.idFiltro > 0 && searchItem.lxTipoFiltro == 2 && searchItem.parametroValor.count() == 0) {
                        search = search + "SID{" + searchItem.uidFiltro + "}";
                    }
                    else {
                        var comandoFiltro = searchItem.comandoFiltro;

                        for (var ii = 0; ii < searchItem.parametroValor.length; ii++) {
                            var item = searchItem.parametroValor[ii];
                            comandoFiltro = comandoFiltro.replace("$UserParam$" + item.name, item.value);
                        }
                        search = search + "({}" + comandoFiltro + "){}";
                    }

                    translatedSearch = translatedSearch + (searchItem.lxTipoFiltro == 1 ? common.translateSearch(uiDataContext, searchItem.comandoFiltro) : _this.translateBMSearch(searchItem.comandoSerializado, searchItem));

                    //Right Parameter
                    for (var p = 0; p < searchItem.rParameter.trim().length; p++) {
                        search = search + '){}';
                        rParameters++;
                        translatedSearch = translatedSearch + ')';
                    }

                    fullSearch = fullSearch + search;
                }

                if (lParameters != rParameters) {
                    app.showMessage("A quantidade de parênteses está inconsistente.\n\n" + lParameters + " à esquerda e " + rParameters + " à direita.", "Atenção", ['Ok']);
                    return;
                }

                vm.dataToolbar.customSearchResult.searchDefinition = fullSearch;
                vm.dataToolbar.customSearchResult.serializedSearch = JSON.stringify(searchList());
                vm.dataToolbar.customSearchResult.translatedSearch = translatedSearch;
                vm.dataToolbar.hasCustomSearches(!isNullOrEmpty(fullSearch));

                app.trigger("shell:customSearch:change");

                dialog.close(this);
            }

            this.saveTcsFiltro = function (entity, callback, entityState) {
                var entidade = '"ComandoFiltro":"' + entity.comandoFiltro + '", "DescFiltro":"' + entity.descFiltro + '", "IdFiltro":"' + entity.uidFiltro + '", "IndicaUsoLinx":"false", "LxTipoFiltro":"' + entity.lxTipoFiltro + '", \
                                "NomeEntidadeBm":"' + (entity.lxTipoFiltro == 1 ? _this.nomeEntidadeBV : _this.nomeEntidadeBM) + '", "UidUsuario":"' + _this.uidUsuario + '", "UidObjeto":"' + _this.uidObjeto + '", \
                                "ComandoSerializado":"' + entity.comandoSerializado.replace(/"/g, '[#]') + '", "Parametros":' + (isNullOrEmpty(entity.parametros) ? 'null' : '"' + entity.parametros + '"') + ', ';

                var data = '    {\
                        "entities": \
                        [ \
                            { ' + entidade + ' \
                                "entityAspect": \
                                { \
                                    "entityTypeName":"TcsFiltro:#Linx.Framework.BV.Filtro", \
                                    "entityState":"' + entityState + '", \
                                    "originalValuesMap":{}, \
                                    "autoGeneratedKey":{"propertyName":"IdFiltro","autoGeneratedKeyType":"Identity"} \
                                }\
                            },\
                        ], \
                        "saveOptions":{} \
                    } ';


                vm.showProcessing('Atualizando filtro');

                $.ajax({
                    type: 'POST',
                    messageUser: "Atualizando Filtro",
                    globalError: true,
                    url: managerAuth.getServiceAddress('LinxFrameworkFiltro', 'Linx.Framework.BV') + '/SaveChanges',
                    headers: managerAuth.getHeaders(),
                    data: data,
                    dataType: 'json',
                    async: true,
                    cache: false,
                    contentType: "application/json",

                    error: function (jqXHR, textStatus, errorThrown) {
                        vm.closeProcessing();
                    },

                    success: function (data) {
                        vm.closeProcessing();
                        if (callback)
                            callback(data, entity);

                    }
                });
            };

            this.divVisibility = function (divName, visible) {

                var visibility = visible ? "visible" : "hidden";
                var display = visible ? "" : "none";

                switch (divName) {
                    case "divEdicao":
                        $("#divEdicao").css("visibility", visibility).css("display", display);
                        $("#btnOkEditor").css("visibility", visibility).css("display", display);
                        $("#btnCancelEditor").css("visibility", visibility).css("display", display);
                        break;

                    case "divFiltros":
                        $("#divFiltros").css("visibility", visibility).css("display", display);
                        $("#btnOk").css("visibility", visibility).css("display", display);
                        $("#btnCancel").css("visibility", visibility).css("display", display);
                        break;

                    case "divTranslatedSearch":
                        $("#divTranslatedSearch").css("visibility", visibility).css("display", display);
                        break;

                    case "divTableSearch":
                        $("#divTableSearch").css("visibility", visibility).css("display", display);
                        break;
                }
            };

            this.insertFilterCallback = function (data, entity) {
                entity.idFiltro = 1;
                entity.uidFiltro = data.Entities[0].IdFiltro;

                _this.tcsFiltro.remove(function (item) { return item.id == entity.uidFiltro });
                _this.tcsFiltro.push({ id: entity.uidFiltro, idFiltro: entity.idFiltro, text: _this.getFilterText(entity.descFiltro, entity.lxTipoFiltro), disabled: false, descFiltro: entity.descFiltro, comandoFiltro: entity.comandoFiltro, uidObjeto: _this.uidObjeto, uidUsuario: _this.uidUsuario, lxTipoFiltro: 2, nomeEntidadeBm: _this.nomeEntidadeBM, comandoFiltroAnterior: entity.comandoFiltro, comandoSerializado: entity.comandoSerializado, filtroTraduzido: entity.filtroTraduzido, parametros: entity.parametros });
                _this.updateSelect2Value(entity.uidLinha, entity.idFiltro, entity.uidFiltro, entity.comandoFiltro, entity.comandoSerializado, entity.lxTipoFiltro, entity.filtroTraduzido);
                _this.updateDivFiltrosControls(entity);
                _this.updateDivEdicaoButtons(entity.idFiltro, entity.lxTipoFiltro);

                $('#edtDescFiltro').editable('setValue', entity.descFiltro);
                $('#edtDescFiltro').editable(entity.idFiltro > 0 ? 'enable' : 'disable');
            };

            this.updateFilterCallback = function (data, entity) {
                _this.filterList().forEach(function (filter) {
                    if (filter.uidFiltro == entity.uidFiltro) {
                        filter.descFiltro = entity.descFiltro;
                        filter.comandoFiltro = entity.comandoFiltro;
                        filter.comandoSerializado = entity.comandoSerializado;
                        filter.filtroTraduzido = entity.filtroTraduzido;
                        _this.updateSelect2Value(filter.uidLinha, filter.idFiltro, filter.uidFiltro, filter.comandoFiltro, filter.comandoSerializado, filter.lxTipoFiltro, filter.filtroTraduzido);
                    }
                })
                //divs
                _this.divVisibility("divEdicao", false);
                _this.divVisibility("divFiltros", true);
            }

            this.deleteFilterCallback = function (data, entity) {
                var currentTcsFiltro = _this.getTcsFiltro(entity.uidFiltro);
                _this.tcsFiltro.remove(currentTcsFiltro);
                _this.filterList.remove(function (item) { return item.uidFiltro == entity.uidFiltro });

                _this.divVisibility("divEdicao", false);
                _this.divVisibility("divFiltros", true);
            }

            this.addTempTcsFiltro = function (idFiltro, comandoFiltro, comandoSerializado, uidLinha, editedFilterListItem, parametros) {
                var descFiltro = 'Filtro temporário ' + Math.abs(idFiltro) + ' - (BM - não salvo)';
                _this.tcsFiltro.push({ id: uidLinha, idFiltro: idFiltro, text: descFiltro, disabled: true, descFiltro: descFiltro, comandoFiltro: comandoFiltro, uidObjeto: _this.uidObjeto, uidUsuario: _this.uidUsuario, lxTipoFiltro: 2, nomeEntidadeBm: _this.nomeEntidadeBM, comandoFiltroAnterior: comandoFiltro, comandoSerializado: comandoSerializado, filtroTraduzido: _this.translateBMSearch(comandoSerializado), parametros: parametros });
                //
                if (editedFilterListItem) {
                    editedFilterListItem().descFiltro = descFiltro;
                }
            };

            this.addFilterList = function (condition, comandoFiltro, uidFiltro, descFiltro, parametros, comandoSerializado, lParameter, rParameter, idFiltro, lxTipoFiltro, filtroTraduzido) {
                _this.filterList.push({ condition: condition, comandoFiltro: comandoFiltro, uidFiltro: uidFiltro, uidLinha: Math.uuid(), descFiltro: descFiltro, parametros: parametros, comandoSerializado: comandoSerializado, idFiltro: idFiltro, lParameter: lParameter, rParameter: rParameter, lxTipoFiltro: lxTipoFiltro, filtroTraduzido: filtroTraduzido, parametroValor: [] });
                var entity = _this.filterList()[_this.filterList().length - 1];
                _this.updateDivFiltrosControls(entity);
            };

            this.loadSearchConditions = function (line) {
                //clear items
                _this.searchConditions.removeAll();

                if (line.idFiltro == 0 || line.comandoSerializado == "") {
                    _this.addSearchCondition();
                    return;
                }

                var filtros = JSON.parse(line.comandoSerializado);

                filtros.forEach(function (filtro) {
                    _this.searchConditions.push(new searchCondition());
                    var rowIndex = _this.searchConditions().length - 1;
                    //
                    _this.searchConditions()[rowIndex].availableOperators(_this.getOperators(filtro.queryDataType, filtro.inputType == "4"));
                    _this.searchConditions()[rowIndex].predefinedFilters(_this.getPredefinedFilters(filtro.queryDataType));
                    _this.searchConditions()[rowIndex].parameters(_this.getParameters(filtro.queryDataType));
                    //
                    _this.searchConditions()[rowIndex].condition(filtro.condition);
                    _this.searchConditions()[rowIndex].lParameter(filtro.lParameter);
                    _this.searchConditions()[rowIndex].entity(filtro.entity);
                    _this.searchConditions()[rowIndex].entityDescription(filtro.entityDescription);
                    _this.searchConditions()[rowIndex].entityPath(filtro.path);
                    _this.searchConditions()[rowIndex].queryDataType(filtro.queryDataType);
                    _this.searchConditions()[rowIndex].operator(_this.getFilterOperator(filtro.operator));
                    _this.searchConditions()[rowIndex].inputType(filtro.inputType);
                    _this.searchConditions()[rowIndex].currentValue(filtro.currentValue);

                    switch (filtro.inputType) {
                        case "1":
                            break;

                        case "2":
                            var parameterValue = $.grep(line.parametroValor, function (element, index) { return element.name == filtro.currentValue });
                            if (parameterValue.count() > 0) {
                                _this.searchConditions()[rowIndex].userParameterValue = parameterValue[0].value;
                            }
                            break;

                        case "3":
                            _this.searchConditions()[rowIndex].parameterValue(filtro.currentValue);
                            break;
                        case "4":
                            _this.searchConditions()[rowIndex].predefinedFilter(_this.getPredefinedFilter(filtro.currentValue));
                            _this.searchConditions()[rowIndex].predefinedFilterValue(filtro.predefinedValue);
                            break;

                    }

                    _this.searchConditions()[rowIndex].rParameter(filtro.rParameter);
                    //update
                    _this.updateSelect2ValueEditor(_this.searchConditions()[rowIndex]);
                    _this.updateInputContentInfo(_this.searchConditions()[rowIndex]);
                });
            };

            this.loadSearches = function () {

                _this.filterList.removeAll();

                //Filtros da UI
                var search = vm.getJExpression(vm.currentDataItem());
                if (!isNullOrEmpty(search)) {

                    if (!isNullOrEmpty(common.translateSearch(uiDataContext, search))) {
                        var descFiltro = "Filtros da transação -  UI";
                        //var uidFiltro = Math.uuid();
                        var uidFiltro = -9999;
                        var filtroTraduzido = _this.translateSearch(search);
                        _this.tcsFiltro.push({ id: uidFiltro, idFiltro: 99, text: descFiltro, disabled: true, descFiltro: descFiltro, comandoFiltro: search, comandoSerializado: "", uidObjeto: _this.uidObjeto, uidUsuario: _this.uidUsuario, lxTipoFiltro: 1, nomeEntidadeBm: _this.NomeEntidadeBV, comandoFiltroAnterior: search, filtroTraduzido: filtroTraduzido, parametros: null });
                        _this.addFilterList("&&", search, uidFiltro, descFiltro, "", "", "", "", 99, 1, filtroTraduzido);
                        var filterItem = _this.filterList()[_this.filterList().length - 1];
                        _this.updateSelect2Value(filterItem.uidLinha, filterItem.idFiltro, filterItem.uidFiltro, filterItem.comandoFiltro, filterItem.comandoSerializado, filterItem.lxTipoFiltro, filtroTraduzido);
                    }
                }

                if (isNullOrEmpty(vm.dataToolbar.customSearchResult.searchDefinition)) {
                    _this.addFilterListItem();
                    return;
                }

                var searchList = JSON.parse(vm.dataToolbar.customSearchResult.serializedSearch);

                for (var i = 0; i < searchList.length; i++) {
                    var search = searchList[i];

                    if (search.idFiltro < 0) {
                        _this.addTempTcsFiltro(search.idFiltro, search.comandoFiltro, search.comandoSerializado, search.uidFiltro, null, search.parametros);
                    }

                    var tcsFiltro = _this.getTcsFiltro(search.uidFiltro);

                    _this.addFilterList(search.condition, tcsFiltro.comandoFiltro, tcsFiltro.id, tcsFiltro.descFiltro, tcsFiltro.parametros, tcsFiltro.comandoSerializado, search.lParameter, search.rParameter, search.idFiltro, search.lxTipoFiltro, tcsFiltro.filtroTraduzido)
                    filterItem = _this.filterList()[_this.filterList().length - 1];

                    for (var ii = 0; ii < search.parametroValor.count() ; ii++) {
                        var item = search.parametroValor[ii];
                        filterItem.parametroValor.push({ name: item.name, value: item.value, description: item.description, operator: item.operator, uidLine: Math.uuid(), operatorId: item.operatorId, queryDataType: item.queryDataType });
                    }

                    _this.updateSelect2Value(filterItem.uidLinha, filterItem.idFiltro, filterItem.uidFiltro, filterItem.comandoFiltro, filterItem.comandoSerializado, filterItem.lxTipoFiltro, filterItem.filtroTraduzido);
                }
            };

            this.contentInfo = function (object, title, inputType, operatorId, queryDataType) {
                if (inputType != "1") {
                    $(object).popover('destroy');
                    return;
                }

                var content = "";

                switch (operatorId.toUpperCase()) {
                    case "IN":
                    case "!IN":
                        content = "valor1, valor2, valor3, ...";
                        break;

                    case "LIKE":
                    case "!LIKE":
                        content = "valor% ou %valor ou %valor%";
                        break;

                    default:
                };

                if (isNullOrEmpty(content)) {
                    switch (queryDataType) {
                        case 'G':
                            content = "00000000-0000-0000-0000-000000000000";
                            break

                        case 'T':
                            content = '01/01/1900 (dd/mm/aaaa)';
                            break;

                        case 'D':
                        case 'F':
                            content = "999.999.999,99";
                            break;

                        case 'H': //Short / int16
                        case 'I': //Int / int32 
                        case 'L': //Long / int64
                        case 'Y': //Byte
                            content = "999.999.999";
                            break;
                    }
                }

                $(object).popover('destroy');

                if (content != "") {
                    content = "Ex.: " + content;
                    $(object).popover({ trigger: 'focus', title: title, content: content });
                }
            }

            this.updateInputContentInfo = function (searchCondition) {
                var sel = '#input_value_' + searchCondition.uidLine;
                var title = searchCondition.operator().name;

                _this.contentInfo(sel, title, searchCondition.inputType(), searchCondition.operator().id, searchCondition.queryDataType());
            };

            this.addErrorClass = function (container, showMessage, errorMessage) {
                _this.hasErrors(true);

                $(container).addClass("customSearch-error-class-container");
                $('#label_' + $(container).attr('id')).remove();

                if (showMessage) {
                    var label = document.createElement('label')
                    label.htmlFor = "id";
                    label.appendChild(document.createTextNode(isNullOrEmpty(errorMessage) ? 'Campo obrigatório !' : errorMessage));
                    label.className = "customSearch-error-class-label";
                    label.id = 'label_' + $(container).attr('id');
                    $(container).parent().append(label);
                }
            };

            this.removeErrorClass = function (container) {
                $(container).removeClass("customSearch-error-class-container");
                $('#label_' + $(container).attr('id')).remove();
            };

            this.validateSearchCondition = function (searchCondition) {
                //entidade
                if (isNullOrEmpty(searchCondition.entity()) || searchCondition.entity() === "...") {
                    _this.addErrorClass('#btnShowTree_' + searchCondition.uidLine);
                }

                if (searchCondition.isCustomSearch()) {
                    return;
                }

                //operador
                if (isNullOrEmpty(searchCondition.operator())) {
                    _this.addErrorClass('#cmbOperator_' + searchCondition.uidLine);
                };
                //valor
                if (!searchCondition.operator() || searchCondition.operator().hasValue) {
                    switch (searchCondition.inputType()) {

                        case "1": // expressão
                        case "2": // parâmetro do usuário
                            if (isNullOrEmpty(searchCondition.currentValue())) {
                                _this.addErrorClass('#input_value_' + searchCondition.uidLine);
                                return;
                            }
                            break;

                        case "3": // parâmetro Linx
                            if (isNullOrEmpty(searchCondition.currentValue()) || searchCondition.currentValue() == 'noValue') {
                                _this.addErrorClass('#select_parametro_' + searchCondition.uidLine);
                                return;
                            }
                            break;

                        case "4": // filtro pré definido
                            if (isNullOrEmpty(searchCondition.currentValue()) || searchCondition.currentValue() == 'noValue') {
                                _this.addErrorClass('#select_predefinido_' + searchCondition.uidLine);
                                return;
                            }

                            if (searchCondition.predefinedFilter().hasValue && isNullOrEmpty(searchCondition.predefinedFilterValue())) {
                                _this.addErrorClass('#input_predefinido_' + searchCondition.uidLine);
                                return;
                            }
                            break;
                    }
                }
                _this.validateSearchConditionInputValue(searchCondition)
            };

            this.validateInputValue = function (object, operatorId, queryDataType, value) {
                //regras do operador
                switch (operatorId.toUpperCase()) {
                    case 'LIKE':
                    case '!LIKE':
                        //procura por %
                        if (!(/%+/).test(value)) {
                            _this.addErrorClass(object);
                        };
                        return;
                        break;

                    case 'IN':
                    case '!IN':
                        if (isNullOrEmpty(value)) {
                            _this.addErrorClass(object);
                        }
                        return;
                        break;

                    case '==':
                    case '!=':
                    case '>':
                    case '<':
                    case '>=':
                    case '<=':
                        if (isNullOrEmpty(value)) {
                            _this.addErrorClass(object);
                            return;
                        }
                        break;
                }

                switch (queryDataType) {
                    case 'G': //Guid
                        if (!(/^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$/).test(value)) {
                            _this.addErrorClass(object);
                            return;
                        }
                        break;

                    case 'D': //Decimal
                    case 'F'://Float
                        if (!(/(?:\d*[\.\,])?\d+/).test(value)) {
                            _this.addErrorClass(object);
                            return;
                        }
                        break;

                    case 'H': //Short / int16
                    case 'I': //Int / int32 
                    case 'L': //Long / int64
                    case 'Y': //Byte
                        if (!(/^-?\d*$/).test(value)) {
                            _this.addErrorClass(object);
                            return;
                        }
                        break;

                    case 'T': //DateTime -> datas a partir de 01/01/1900 (somente data)
                        if (!(/^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$/).test(value)) {
                            _this.addErrorClass(object);
                            return;
                        }
                        break;
                }
            }

            this.validateSearchConditionInputValue = function (searchCondition) {

                if (!searchCondition.operator() || !searchCondition.operator().hasValue || searchCondition.inputType() != 1) {
                    return;
                }
                _this.validateInputValue('#input_value_' + searchCondition.uidLine, searchCondition.operator().id, searchCondition.queryDataType(), searchCondition.currentValue());
            }

            this.showTreeView = function (data) {
                modalTreeView.show(vm, uiDataContext.controllerName, isNullOrEmpty(data.entityPath()) ? _this.nomeEntidadeBM : data.entityPath()).then(function (response, response1) {
                    if (response && response != null) {

                        data.entityDescription(response.original.text);

                        if (data.entity() === response.original.id) {
                            return;
                        }

                        data.entity(response.original.id);
                        data.entityPath(response1);
                        data.availableOperators(_this.getOperators(response.original.dataType, false));
                        data.queryDataType(response.original.dataType);
                        data.operator("");
                        data.currentValue("");
                        data.inputType("1");
                        $('#input_type_' + data.uidLine).editable('setValue', "1");

                        //se é pesquisa ou Datatype
                        if (data.isCustomSearch()) {
                            $('#input_type_' + data.uidLine).editable('disable');
                        }
                        else {
                            $('#input_type_' + data.uidLine).editable('enable');
                        }


                        if (data.queryDataType() == "B") {
                            _this.onChangeSelectBoolean(data);
                        }

                        data.predefinedFilters(_this.getPredefinedFilters(data.queryDataType()));
                        data.parameters(_this.getParameters(data.queryDataType()));

                        _this.removeErrorClass('#btnShowTree_' + data.uidLine);
                    }
                });
            }

            this.updateTranslatedSearch = function (uidLinha, filtroTraduzido) {
                var sel = '#btn_translate_' + uidLinha;
                $(sel).popover('destroy');
                $(sel).popover({ trigger: 'hover', title: 'Filtro da pesquisa', content: filtroTraduzido, placement: 'bottom' });
            }

            this.translateSearch = function (pesquisa) {
                return common.translateSearch(uiDataContext, pesquisa);
            }

            this.translateBMSearch = function (comandoSerializado, search) {
                var filtros = JSON.parse(comandoSerializado);
                var pesquisa = "";

                filtros.forEach(function (filtro) {
                    //condition
                    if (!isNullOrEmpty(pesquisa)) {
                        pesquisa = pesquisa + (filtro.condition == "&&" ? " e " : " ou ");
                    }
                    //lParameter
                    pesquisa = pesquisa + filtro.lParameter.trim();
                    //entityDescription
                    pesquisa = pesquisa + '[' + filtro.entityDescription + '] ';
                    //operator
                    if (strLeft(filtro.entity, 4).toUpperCase() != "*SID") {
                        pesquisa = pesquisa + dataDomains.getName('FilterOperator', filtro.operator) + ' ';
                    }
                    //currentValue
                    switch (filtro.inputType) {
                        case "1":
                            if (!isNullOrEmpty(filtro.currentValue.trim())) {
                                pesquisa = pesquisa + (filtro.queryDataType == "B" ? (filtro.currentValue.toUpperCase() == "TRUE" ? "verdadeiro" : "falso") : filtro.currentValue);
                            }
                            break;

                        case "2":
                            var pesquisaAux = "Parâmetro do Usuário [" + filtro.currentValue.trim() + "]";

                            if (search && search.parametroValor) {
                                var parameter = $.grep(search.parametroValor, function (element, index) { return element.name == filtro.currentValue });
                                if (parameter.count() > 0) {
                                    pesquisaAux = parameter[0].value;
                                }
                            }
                            pesquisa = pesquisa + pesquisaAux;
                            break;

                        case "3":
                            pesquisa = pesquisa + "Parâmetro [" + filtro.currentValue.trim() + "]";
                            break;

                        case "4":
                            var predefined = _this.getPredefinedFilter(filtro.currentValue);
                            pesquisa = pesquisa + (predefined.hasValue ? predefined.text.replace('(x)', filtro.predefinedValue) : predefined.text);
                            break;
                    }
                    //rParameter
                    pesquisa = pesquisa + filtro.rParameter.trim();
                });
                return pesquisa.trim();
            }

            this.updateDivFiltrosControls = function (entity) {

                if (entity.idFiltro == 99) {
                    $('#cmbFCondition_' + entity.uidLinha).css("visibility", "hidden").css("display", "none");
                    $('#cmbFLeftParameter_' + entity.uidLinha).css("visibility", "hidden").css("display", "none");
                    $('#cmbFRightParameter_' + entity.uidLinha).css("visibility", "hidden").css("display", "none");
                    $('#btn_remove_' + entity.uidLinha).attr('disabled', true);
                    $('#btn_edit_' + entity.uidLinha).css("visibility", "hidden").css("display", "none");
                }
                else {
                    $('#btn_saveFilter_' + entity.uidLinha).css("visibility", "hidden").css("display", "none");
                    $('#btn_edit_' + entity.uidLinha).css("visibility", "visible").css("display", "");
                }
            }

            this.updateDivEdicaoButtons = function (idFiltro, lxTipoFiltro) {
                $('#btnSaveSearch').attr('disabled', idFiltro == 1);
                $('#btnDeleteSearch').attr('disabled', idFiltro != 1);
                $('#btnAddSearchLine').attr('disabled', lxTipoFiltro == 1);
            }
        };

        customSearch.show = function (vm, uiDataContext, filterOnly) {
            return dialog.show(new customSearch(vm, uiDataContext, filterOnly));
        };

        return customSearch;
    });

