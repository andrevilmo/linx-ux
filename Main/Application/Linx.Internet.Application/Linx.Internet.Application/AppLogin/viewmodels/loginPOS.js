define(['durandal/system', 'durandal/app', 'services/logger', 'plugins/router', 'plugins/dialog', 'viewmodels/shared/modal', 'common', 'jsSHA', 'knockout', 'managers/__auth', 'base32'],
    function (system, app, logger, router, dialog, modal, common, jsSHA, ko, managerAuth, base32) {
        var vm = function () {
            var _this = this;
            this.ljvVendedor = ko.observableArray([]);

            this.activate = function () {
                var currentDate = new Date();
                var filtroWhere = 'LjvVendedor{Inativo#==#Bfalse;&&;DataAtivacao#<=#T' + formatUTCDateToString(currentDate) + ';&&;(;DataDesativacao#==#null;||;DataDesativacao#>=#T' + formatUTCDateToString(currentDate) + ' ;);}';

                require(['json!../../../routes.json', 'json!../../../config.json'], function (routesJson, configJson) {
                    var serviceBus = configJson['serviceBus'];

                    common.showProcess();

                    // Filtros disponíveis
                    $.ajax({
                        type: 'GET',
                        message: "Buscando Vendedores",
                        messageUser: "Buscando Vendedores",
                        globalError: true,
                        url: serviceBus + 'LinxFrameworkLojaVendedor/GetLjvVendedorByEntitySearchNoAssociations',
                        data: { jEntitySearch: filtroWhere },
                        dataType: 'json',
                        async: true,
                        cache: false,

                        error: function (jqXHR, textStatus, errorThrown) {
                            common.closeProcess();
                        },

                        success: function (data) {
                            for (var i = 0; i < data.length; i++) {
                                var item = data[i];
                                _this.ljvVendedor.push({ id: item.IdVendedor, text: item.NomeVendedor + ' - (' + item.CodVendedor + ')', disabled: false, idVendedor: item.IdVendedor, codVendedor: item.CodVendedor, idLoja: item.IdLoja, indicaGerente: item.IndicaGerente, indicaOperadorCaixa: item.IndicaOperadorCaixa, nomeVendedor: item.NomeVendedor, hash: item.Hash, idFilialPfj : item.IdFilialPfj });
                            }
                            common.closeProcess();
                        }
                    });
                });
            }

            this.compositionComplete = function () {
                var sel = "#select_vendedor";

                $(sel).select2({
                    placeholder: "Vendedor",
                    openOnEnter: true,
                    width: "off",
                    escapeMarkup: function (m) {
                        return m;
                    },
                    data: _this.ljvVendedor()
                });

                $(sel).select2("val", "");
            };

            this.ok = function () {
                var pinVendedor = common.getPinHash($('#pinVendedor').val());
                var idVendedor = $('#select_vendedor').select2("val");
                var vendedor = $.grep(_this.ljvVendedor(), function (element, index) { return element.idVendedor == idVendedor });

                if (vendedor.count() > 0) {
                    var item = vendedor[0];
                    if (pinVendedor == item.hash) {
                        var enconded = base32.encode(item.idVendedor + '||' + item.nomeVendedor + '||' + item.idLoja + '||' + item.indicaGerente + '||' + item.indicaOperadorCaixa + "||" + item.idFilialPfj, false).replace(/=/g, '');
                        $.ezstorage.set('Hash_Login', enconded);
                        //window.location.reload();
                        window.location.href = window.location.origin + window.location.pathname;
                    }
                    else {
                        app.showMessage("PIN inválido !", "Atenção");
                    }
                }
            };
        }

        return vm;
    });