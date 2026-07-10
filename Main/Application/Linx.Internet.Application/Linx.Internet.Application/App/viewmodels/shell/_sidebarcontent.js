define(['services/logger', 'plugins/router', 'durandal/app', 'knockout', 'managers/user', 'managers/__auth', 'managers/window', 'common', 'managers/brand', 'viewmodels/shell/_menu', 'viewmodels/shell/_header'],
    function (logger, router, app, ko, managerUser, managerAuth, managerWindow, common, managerBrand, _menu, _header) {
        function format(sourceData) {
            return sourceData.html_select2;
        }

        var vm = {
            activate: activate,
            attached: attached,
            beforeBind: beforeBind,
            afterBind: afterBind,
            canDeactivate: canDeactivate,
            canActivate: canActivate,
            deactivate: deactivate,
            compositionComplete: compositionComplete,

            UICloseAll: UICloseAll,
            UIClose: UIClose,
            UIClearStorage: UIClearStorage,
            UILogOut: UILogOut,
            UIChangeEnvironment: UIChangeEnvironment,
            UIChangePassword: UIChangePassword,
            UIChangeTheme: UIChangeTheme,
            UIClearHome: UIClearHome,
            SupportRequest: SupportRequest,
            UIReauthenticate: UIReauthenticate,

            router: router,
            managerUser: managerUser,
            managerAuth: managerAuth,
            managerWindow: managerWindow,
            managerBrand: managerBrand,

            paginaInicial: ko.observable(""),

            //Propriedades para tradução da sidebar
            sair: ko.observable("Sair"),
            ambientes: ko.observable("Ambientes"),
            alterarSenha: ko.observable("Alterar Senha"),
            reAutenticar: ko.observable("Re-Autenticar"),
            temas: ko.observable("Temas"),
            grpEcon: ko.observable("Grupo Econômico"),
            empresa: ko.observable("Empresa"),
            redePadrao: ko.observable("Rede Padrão"),
            cache: ko.observable("Cache"),
            dados: ko.observable("Dados"),
            limpar: ko.observable("Limpar"),
            suporte: ko.observable("Suporte"),
            gerarUrlSuporte: ko.observable("Gerar Url para Suporte"),
            urlSuporte: ko.observable("Url Suporte"),
            configuracao: ko.observable("Configuração"),
            titlePaginaInicial: ko.observable("Página Inicial"),
            resultadoTabular: ko.observable("Resultado Tabular"),
            manterUltimoFiltro: ko.observable("Manter Último Filtro"),
            esconderAssistentes: ko.observable("Esconder Assistentes"),
            idioma: ko.observable("Idioma"),
            availableLanguages: ko.observableArray([
                { id: "pt-br", name: "Português" },
                { id: "en-us", name: "Inglês" },
                { id: "es-es", name: "Espanhol" },
            ]),
            //

            UIRefresh: UIRefresh,
            setLangMisc: setLangMisc,

            afterRenderTable: afterRenderTable,

            selectedLanguage: ko.observable(),

            selectionChanged: function (event) {
                var selLang = event.selectedLanguage();
                common.saveIdioma(selLang);
                UIRefresh();
                setLangMisc(selLang);
            }

            //lxDownloadModule: '',
            //lxExtractView: '',

        };

        setLangMisc(common.getIdioma());

        return vm;

        //#region Internal Methods
        function canActivate() {
            //alert('_header canActivate');
            return true;
        }

        function canDeactivate() {
            //alert('canDeactivate');
            return true;
        }

        function beforeBind() {
            //alert('beforeBind');
            return true;
        }

        function afterBind() {
            //alert('afterBind');
            return true;
        }

        function attached() {
            //alert(router.activeInstruction().config.data);
            return true;
        }

        function deactivate() {
            //alert('deactivate');
            return true;
        }

        function activate() {
            var value = common.getStartPage();
            vm.paginaInicial(value);
            return true;
        }

        function compositionComplete() {
            $("[id='chkTrace']").bootstrapSwitch('state', (managerAuth.traceMode));

            if (managerAuth.profilerEnabled == false) {
                $("[id='chkTrace']").on('switchChange.bootstrapSwitch', function (event, state) {

                    if (state) {
                        closeSideBar();

                        app.showMessage('Impossível habilitar o trace!<BR><BR>1. Configure a chave <b>Shell.MiniProfiler.Enabled</b> para "true" no web.config <br> <br> 2. Configure a chave <b>MiniProfiler.Enabled</b> para "true" no web.config do serviço.<BR>', 'Linx UX', ['Reiniciar', 'Cancelar']).then(function (dialogResult) {
                            if (dialogResult != "Cancelar") {
                                common.showProcessFull();
                                window.location.reload();
                            }
                        });

                        $("[id='chkTrace']").bootstrapSwitch('state', false);
                    }
                });
            }
            else {
                $("[id='chkTrace']").on('switchChange.bootstrapSwitch', function (event, state) {
                    var url = '';

                    if (state)
                        url = (window.location.origin + window.location.pathname + "?tracemode=on" + window.location.search.replace("?", "&") + window.location.hash);
                    else
                        url = (window.location.origin + window.location.pathname + window.location.hash);

                    closeSideBar();
                    common.showProcessFull();
                    window.location.href = url;
                });
            }

            $("[id='chkLastFilterMode']").bootstrapSwitch('state', common.getLastFilterMode());

            $("[id='chkLastFilterMode']").on('switchChange.bootstrapSwitch', function (event, state) {
                common.saveLastFilterMode(state);
            });

            $("[id='chkHideWizards']").on('switchChange.bootstrapSwitch', function (event, state) {
                common.saveHideWizards(state);
                if (_header.activeMode() == 0) {
                    _header.changeMode((_menu.dashboardTransactions().length > 0 ? 1 : 2));
                };
                UIRefresh();
            });

            $("[id='chkHideWizards']").bootstrapSwitch('state', common.getHideWizards());

            $("[id='chkBarraNavegacao']").bootstrapSwitch('state', common.getBarraNavegacao());

            vm.selectedLanguage(common.getIdioma());

            $("[id='chkBarraNavegacao']").on('switchChange.bootstrapSwitch', function (event, state) {
                common.saveBarraNavegacao(state);
                UIRefresh();
            });

            $("[id='chkGridMode']").bootstrapSwitch('state', (common.getGridMode() == 'G'));

            $("[id='chkGridMode']").on('switchChange.bootstrapSwitch', function (event, state) {
                common.saveGridMode(state)
            });

            router.on('saveStartPage:changed').then(function (newValue) {
                vm.paginaInicial(newValue);
            });

            QuickSidebar.init(); // Handles quick sidebar toggler
        }

        function afterRenderTable(element, data) {
            var sel = "#brand_" + data.IdTcsAmbiente;

            $(sel).attr('index', data.IdTcsAmbiente);

            $(sel).editable({
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
                value: managerBrand.getDefaultBrand(data.IdTcsAmbiente),
                url: '',
                source: managerBrand.getBrandVM(data.IdTcsAmbiente),
                title: 'Bandeira/Rede:',
                placement: 'left',
                onblur: 'submit',
                highlight: false,
                showbuttons: false,
                emptytext: managerBrand.getDefaultBrand(data.IdTcsAmbiente),
                mode: 'inline',

                error: function (data) {
                },

                success: function (response, newValue) {
                    managerBrand.saveDefaultBrand($(this).attr('index'), newValue);
                },

                validate: function (value) {
                    if ($.trim(value) == '')
                        return 'Seleção obrigatória!';
                },

                display: function (value, sourceData) {
                    if (!value) {
                        $(this).empty();
                        return;
                    }

                    $(this).html(managerBrand.searchBrandsVM(value, $(this).attr('index')).html);
                }

            });


        }

        // Method: UICloseAll()
        function UICloseAll() {
            managerWindow.closeAll();
        };
        //#endregion

        // Method: UIClose()
        function UIClose(id) {
            managerWindow.close(id);
        };
        //#endregion

        // Method: UIRefresh()
        function UIRefresh() {
            var currentModule = router.activeItem().__moduleId__;

            requirejs.undef(currentModule)
            requirejs.undef(currentModule + 'Complement')
            requirejs.undef('text!' + currentModule.replace('viewmodels', 'views') + '.html')

            router.deactivate();
            router.activate();
        }
        // #endregion

        // Method: UIClearStorage()
        function UIClearStorage() {

            common.showProcess('#main');

            //Cache Geral
            $.ajax({
                type: 'POST',
                globalError: true,
                message: "Limpando cache",
                messageUser: "Limpando cache",
                headers: managerAuth.getHeaders(managerAuth.loginInfo.IdTcsAmbienteDefault),
                url: managerAuth.getServiceAddress('LinxFrameworkUtilitarios', 'Linx.Framework.BV') + '/CleanCache',
                data: JSON.stringify({
                    UidUsuario: managerAuth.loginInfo.UidUsuario,
                    BandeiraRede: true,
                    Modulo: true,
                    Conexao: false,
                    Geral: false,
                    Relatorio: false
                }),
                contentType: "application/json",
                async: true,
                cache: false,

                error: function (jqXHR, textStatus, errorThrown) {
                    common.closeProcess('#main');
                },

                success: function (data) {
                    $.localStorage.removeAll();
                    $.sessionStorage.removeAll();
                    window.location.reload();
                    common.closeProcess('#main');
                }
            });

        };
        //#endregion

        function SupportRequest() {
            common.showProcess('#main');

            //Cache Geral
            $.ajax({
                type: 'POST',
                globalError: true,
                message: "Requisitando Url para suporte",
                messageUser: "Requisitando Url para suporte",
                headers: managerAuth.getHeaders(managerAuth.loginInfo.IdTcsAmbienteDefault),
                url: managerAuth.getServiceAddress('LinxFrameworkUsuarioAutorizacao', 'Linx.Framework.BV') + '/SupportRequest',
                data: JSON.stringify({
                    UidUsuario: managerAuth.loginInfo.UidUsuario,
                    IdTcsAmbiente: managerAuth.loginInfo.IdTcsAmbienteDefault,
                    UrlPortal: managerAuth.loginUrl
                }),
                contentType: "application/json",
                async: true,
                cache: false,

                error: function (jqXHR, textStatus, errorThrown) {
                    common.closeProcess('#main');
                },

                success: function (data) {
                    common.closeProcess('#main');
                    closeSideBar();

                    require(['viewmodels/shared/modalSupportRequestUrl'], function (modalSupportRequest) {
                        modalSupportRequest.show(data);
                    });
                }
            });
        };

        // Method: UILogOut()
        function UILogOut() {
            if (managerAuth.isLoginPOSUXMode) {
                $.ezstorage.remove('Hash_Login');
                window.location.href = window.location.origin + window.location.pathname;
            }
            else {
                $.sessionStorage.removeAll();
                window.location.href = managerAuth.buildRoot('logoff');
            }
        };
        //#endregion

        function UIChangeEnvironment() {
            $.sessionStorage.removeAll();
            window.location.href = managerAuth.buildRoot('ChangeEnvironment');
        }

        // Method: UIChangePassword()
        function UIChangePassword() {
            closeSideBar();

            require(['viewmodels/shared/modalChangePassword'], function (modalChangePassword) {
                modalChangePassword.show(true);
            });

        };
        //#endregion

        // Method: UIChangeTheme()
        function UIChangeTheme(color) {
            common.saveTheme(color);
        };
        //#endregion

        // Method: UIClearHome()
        function UIClearHome(color) {
            common.saveStartPage('');
        };
        //#UIClearHome

        //Method: setLangMisc()
        function setLangMisc(lang) {
            if (lang != "pt-br") {
                require(['viewmodels/shared/languages_sidebar/sidebar_' + lang],
                    function (main) {
                        var label = main.langPropsSidebar();
                        vm.sair(label.sair);
                        vm.ambientes(label.ambientes);
                        vm.alterarSenha(label.alterarSenha);
                        vm.reAutenticar(label.reAutenticar);
                        vm.temas(label.temas)
                        vm.grpEcon(label.grpEcon);
                        vm.empresa(label.empresa);
                        vm.redePadrao(label.redePadrao);
                        vm.cache(label.cache);
                        vm.dados(label.dados);
                        vm.limpar(label.limpar);
                        vm.suporte(label.suporte);
                        vm.gerarUrlSuporte(label.gerarUrlSuporte);
                        vm.urlSuporte(label.urlSuporte);
                        vm.configuracao(label.configuracao);
                        vm.titlePaginaInicial(label.titlePaginaInicial);
                        vm.resultadoTabular(label.resultadoTabular);
                        vm.manterUltimoFiltro(label.manterUltimoFiltro);
                        vm.esconderAssistentes(label.esconderAssistentes);
                        vm.idioma(label.idioma);
                        vm.availableLanguages(label.availableLanguages);
                        vm.selectedLanguage(lang);
                    });
            } else
                setDefaultLang();
        };

        function setDefaultLang() {
            vm.sair("Sair");
            vm.ambientes("Ambientes");
            vm.alterarSenha("Alterar Senha");
            vm.reAutenticar("Re-Autenticar");
            vm.temas("Temas");
            vm.grpEcon("Grupo Econômico");
            vm.empresa("Empresa");
            vm.redePadrao("Rede Padrão");
            vm.cache("Cache");
            vm.dados("Dados");
            vm.limpar("Limpar");
            vm.suporte("Suporte");
            vm.gerarUrlSuporte("Gerar Url para Suporte");
            vm.urlSuporte("Url Suporte");
            vm.configuracao("Configuração");
            vm.titlePaginaInicial("Página Inicial");
            vm.resultadoTabular("Resultado Tabular");
            vm.manterUltimoFiltro("Manter Último Filtro");
            vm.esconderAssistentes("Esconder Assistentes");
            vm.idioma("Idioma");
            vm.availableLanguages([
                { id: "pt-br", name: "Português" },
                { id: "en-us", name: "Inglês" },
                { id: "es-es", name: "Espanhol" }
            ]);
        };

        // Method: UIReauthenticate()
        function UIReauthenticate() {
            closeSideBar();
            app.showMessage('Não é necessário realizar nova autenticação nas demais abas.       \nClique em Atualizar ou Ctrl + r para recarrega-las.', 'Atenção', ['Ok']).then(function () {
                var transaction = (router.activeInstruction().config != null && router.activeInstruction().config.currentData != null && router.activeInstruction().config.currentData.UrlRoute != null) ? router.activeInstruction().config.currentData.UrlRoute : "";
                var userUid = managerAuth.loginInfo.UidUsuario;
                var environmentId = managerAuth.loginInfo.IdTcsAmbienteDefault;

                $.ajax({
                    type: 'POST',
                    messageUser: "",
                    url: managerAuth.buildRoot('UpdateReauthenticationInfo'),
                    data: JSON.stringify({
                        info: transaction + "||" + userUid + "||" + environmentId
                    }),

                    contentType: "application/json",
                    async: true,
                    cache: false,
                    error: function (jqXHR, textStatus, errorThrown) {

                    },

                    success: function (data) {
                        window.location.href = managerAuth.buildRoot('Reauthenticate');
                    }
                });

            });
        };

        function closeSideBar() {
            $('body').removeClass("page-quick-sidebar-open");
            $('.closeSidebarClick').hide();
        }
    });