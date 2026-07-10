define(['services/logger', 'plugins/router', 'durandal/app', 'knockout', 'managers/user', 'managers/__auth', 'managers/window', 'common', 'managers/brand'],
    function (logger, router, app, ko, managerUser, managerAuth, managerWindow, common, managerBrand) {
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
    		UIChangePassword: UIChangePassword,
    		UIChangeTheme: UIChangeTheme,
    		UIClearHome: UIClearHome,

    		router: router,
    		managerUser: managerUser,
    		managerAuth: managerAuth,
    		managerWindow: managerWindow,
    		managerBrand: managerBrand,

    		paginaInicial: ko.observable(""),
    		UIRefresh: UIRefresh,

    		//lxDownloadModule: '',
            //lxExtractView: '',

    	};

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
    	                $('body').removeClass("page-quick-sidebar-open");
    	                $('.closeSidebarClick').hide();

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

    	            $('body').removeClass("page-quick-sidebar-open");
    	            $('.closeSidebarClick').hide();
    	            common.showProcessFull();
    	            window.location.href = url;
    	        });
    	    }

    	    $("[id='chkLastFilterMode']").bootstrapSwitch('state', common.getLastFilterMode());

    	    $("[id='chkLastFilterMode']").on('switchChange.bootstrapSwitch', function (event, state) {
    	        common.saveLastFilterMode(state);
    	    });

    	    $("[id='chkBarraNavegacao']").bootstrapSwitch('state', common.getBarraNavegacao());

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

    	    $("#select2_brandUser").editable({
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
    	        value: managerBrand.IdBandeiraRedeDefault,
    	        url: '',
    	        source: managerBrand.BRANDS_VM.slice(1, managerBrand.BRANDS_VM.length),
    	        title: 'Bandeira/Rede:',
    	        placement: 'left',
    	        onblur: 'submit',
    	        highlight: false,
    	        showbuttons: false,
    	        emptytext: managerBrand.getDefaultBrand(),
                mode: 'inline',

    	        error: function (data) {
    	        },

    	        success: function (response, newValue) {
    	            managerBrand.saveDefaultBrand(newValue);
    	            managerBrand.setDefaultBrand(newValue);
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

    	            $(this).html(managerBrand.searchBrandsVM(value).html);
    	        }

    	    });

    	    QuickSidebar.init(); // Handles quick sidebar toggler
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
    	        message: "limpando cache...",
    	        messageUser: "Limpando cache",

    	        url: managerAuth.getServiceAddress('LinxFrameworkUtilitarios/CleanCache'),
    	        data: JSON.stringify({
    	            UidUsuario: managerAuth.userId,
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

        // Method: UILogOut()
    	function UILogOut() {
    	    $.sessionStorage.removeAll();
    	    window.location.href = managerAuth.buildRoot('logoff');
    	};
        //#endregion

        // Method: UIChangePassword()
    	function UIChangePassword() {
    	    $('body').removeClass("page-quick-sidebar-open");
    	    $('.closeSidebarClick').hide();

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
    });