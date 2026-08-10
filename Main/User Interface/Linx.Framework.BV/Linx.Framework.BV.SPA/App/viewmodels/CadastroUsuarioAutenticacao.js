define(['durandal/app', 'pkg_linx-framework-bv-spa/services/UsuarioAutorizacaoContext', 'plugins/router', 'plugins/widget', 'managers/__auth', 'viewmodels/shared/modal', 'viewmodels/shared/modal2', 'managers/brand', 'managers/predefinedFilters', 'services/logger', 'viewmodels/shared/modalMultimidia', 'common', 'pkg_linx-framework-bv-spa/viewmodels/CadastroUsuarioAutenticacaoComplement', 'viewmodels/shared/modalCustomSearch'],
function (app, dataContextFn, router, widget, managerAuth, modal, modal2, managerBrand, managerPredefined, logger, modalMultimidia, common, complementFn, modalCustomSearch) {
var vms = [];
var pivots = [];
var vmInstance = function () {
    var activeRoute = document.URL;
    if (activeRoute.indexOf('?') >= 0)
        activeRoute = activeRoute.substring(0, activeRoute.indexOf('?'));
    if (vms[activeRoute])
        return vms[activeRoute];
    else {
        var vm = vmConstructor();
        vms[activeRoute] = vm;
        return vm;
    }
}
var vmConstructor = function () {
    var flattenObjectByProperty = function(obj, name) {
        var flat = {};
        function reduce(obj){
            flat[obj[name]] = $.extend({ }, obj);
            if (flat[obj[name]].Items) delete flat[obj[name]].Items;
            if (obj.Items) obj.Items.forEach(function(item) {
                return reduce(item);
            })
        }
        if (obj.Items) obj.Items.forEach(function(item) {
            reduce(item);
        });
        return flat;
    };
    
    
    var getLayoutColumnSpan = function(name) {
        return controlLayout.getColSpan(vm, name, typeof dialogIsOpen !== "undefined" ? dialogIsOpen : false);
    };
    
    var getLayoutDisplayName = function(name) {
        return controlLayout.getDisplayName(vm, name, typeof dialogIsOpen !== "undefined" ? dialogIsOpen : false);
    };
    
    var getLayoutVisible = function(name) {
        return controlLayout.getVisibility(vm, name, typeof dialogIsOpen !== "undefined" ? dialogIsOpen : false);
    };
    
    var getDimensionUniqueName = function(name) {
        return controlLayout.getDimensionUniqueName(vm, name);
    };
    
    var getLayoutHeaderGrid = function(name) {
        return controlLayout.getGridHeaderDisplayName(vm, name);
    };
    
    var objectLayout = function () {
       return {Name: 'CadastroUsuarioAutenticacao', Items: [

	 {Name: "CadastroUsuarioAutenticacao_gbTcsUsuarioAutenticacao", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroUsuarioAutenticacao_gbGroupBox_d8e00b0a8c5e48efaae8165f0a7a009a", DisplayName: "Usuário", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroUsuarioAutenticacao_gbCustomContainer_c6366698bd76405a98b50d8fc2b7c79c", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroUsuarioAutenticacao_gbGroupBox_1927a4b5924b4278a4149959828d64ae", DisplayName: "", ColumnSpan: 6, Visible: true, Items: [
	 {Name: "CadastroUsuarioAutenticacao_tbNomeUsuario", DisplayName: "Nome", ColumnSpan: 12, Visible: true, Key: "NomeUsuario"},
	 {Name: "CadastroUsuarioAutenticacao_tbNomeAutenticacao", DisplayName: "Usuário Autenticação", ColumnSpan: 12, Visible: true, Key: "NomeAutenticacao"},
	 {Name: "CadastroUsuarioAutenticacao_tbNomeCurtoUsuario", DisplayName: "Apelido", ColumnSpan: 12, Visible: true, Key: "NomeCurtoUsuario"},
	 {Name: "CadastroUsuarioAutenticacao_tbEmail", DisplayName: "Email", ColumnSpan: 12, Visible: true, Key: "Email"},
	 {Name: "CadastroUsuarioAutenticacao_lUpNomeEmpresa", DisplayName: "Empresa / Grupo Econômico", ColumnSpan: 12, Visible: true, LookUpName: "LookUpTcsUsuarioEmpresaAutenticacao", Key: "NomeEmpresa"},
	 {Name: "CadastroUsuarioAutenticacao_ckAutenticacaoWindows", DisplayName: "Utiliza Autenticação Windows", ColumnSpan: 6, Visible: true, Key: "AutenticacaoWindows"},
	 {Name: "CadastroUsuarioAutenticacao_ckIndicaAcessoSuporte", DisplayName: "Permite Acesso de Suporte", ColumnSpan: 6, Visible: true, Key: "IndicaAcessoSuporte"},]},
	 {Name: "CadastroUsuarioAutenticacao_gbGroupBox_294d36395b1f414997b597e9a47dd1b7", DisplayName: "", ColumnSpan: 2, Visible: true, Items: [
	 {Name: "CadastroUsuarioAutenticacao_dtVigenciaInicial", DisplayName: "Vigência Inicial", ColumnSpan: 12, Visible: true, Key: "VigenciaInicial"},
	 {Name: "CadastroUsuarioAutenticacao_dtVigenciaFinal", DisplayName: "Vigência Final", ColumnSpan: 12, Visible: true, Key: "VigenciaFinal"},
	 {Name: "CadastroUsuarioAutenticacao_dtDataExpiracaoSenha", DisplayName: "Expiração Senha", ColumnSpan: 12, Visible: true, Key: "DataExpiracaoSenha"},
	 {Name: "CadastroUsuarioAutenticacao_dtDataCadastro", DisplayName: "Cadastro", ColumnSpan: 12, Visible: true, Key: "DataCadastro"},
	 {Name: "CadastroUsuarioAutenticacao_dtDataAlteracao", DisplayName: "Alteração", ColumnSpan: 12, Visible: true, Key: "DataAlteracao"},
	 {Name: "CadastroUsuarioAutenticacao_ckInativo", DisplayName: "Inativo", ColumnSpan: 12, Visible: true, Key: "Inativo"},
	 {Name: "CadastroUsuarioAutenticacao_ckIndicaUsuarioServico", DisplayName: "Usuário de serviço", ColumnSpan: 12, Visible: true, Key: "IndicaUsuarioServico"},
	 {Name: "CadastroUsuarioAutenticacao_ckBlocked", DisplayName: "Bloqueado", ColumnSpan: 12, Visible: true, Key: "Blocked"},]},]},
	 {Name: "CadastroUsuarioAutenticacao_gbUserPasswordGroupBox", DisplayName: "Senha Usuário", ColumnSpan: 12, Visible: false, Items: [
	 {Name: "CadastroUsuarioAutenticacao_tbConfirmacaoUsuario", DisplayName: "Senha", ColumnSpan: 8, Visible: true, Key: "ConfirmacaoUsuario"},
	 {Name: "CadastroUsuarioAutenticacao_tbConfirmacaoUsuario1", DisplayName: "Confirmação", ColumnSpan: 8, Visible: true, Key: "ConfirmacaoUsuario1"},]},]},
	 {Name: "CadastroUsuarioAutenticacao_tcTcsUsuarioAutenticacaoTabControl", DisplayName: "TcsUsuarioAutenticacao", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroUsuarioAutenticacao_tiTcsUsuarioAcessoTabItem", DisplayName: "Acessos", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroUsuarioAutenticacao_dGridTcsUsuarioAcesso", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroUsuarioAutenticacao_lUpTcsUsuarioAcesso_DescricaoAmbiente", Name: "CadastroUsuarioAutenticacao_dGridTcsUsuarioAcesso_DescricaoAmbiente", DisplayName: "Ambiente", ColumnSpan: 9, Visible: true, LookUpName: "LookUpTcsAmbiente", Key: "DescricaoAmbiente"},
	 {Id: "CadastroUsuarioAutenticacao_ckTcsUsuarioAcesso_IndicaAcessoPadrao", Name: "CadastroUsuarioAutenticacao_dGridTcsUsuarioAcesso_IndicaAcessoPadrao", DisplayName: "Acesso Padrão", ColumnSpan: 5, Visible: true, Key: "IndicaAcessoPadrao"},
	 {Id: "CadastroUsuarioAutenticacao_ckTcsUsuarioAcesso_IndicaAdministrador", Name: "CadastroUsuarioAutenticacao_dGridTcsUsuarioAcesso_IndicaAdministrador", DisplayName: "Administrador", ColumnSpan: 5, Visible: true, Key: "IndicaAdministrador"},
	 {Id: "CadastroUsuarioAutenticacao_ckTcsUsuarioAcesso_IndicaMultiGpecon", Name: "CadastroUsuarioAutenticacao_dGridTcsUsuarioAcesso_IndicaMultiGpecon", DisplayName: "Multi Grupo Econômico", ColumnSpan: 8, Visible: true, Key: "IndicaMultiGpecon"},
	 {Id: "CadastroUsuarioAutenticacao_lUpTcsUsuarioAcesso_DescricaoAplicacao", Name: "CadastroUsuarioAutenticacao_dGridTcsUsuarioAcesso_DescricaoAplicacao", DisplayName: "Aplicação", ColumnSpan: 9, Visible: true, LookUpName: "LookUpTcsAmbiente", Key: "DescricaoAplicacao"},
	 {Id: "CadastroUsuarioAutenticacao_ckTcsUsuarioAcesso_EmDesenvolvimento", Name: "CadastroUsuarioAutenticacao_dGridTcsUsuarioAcesso_EmDesenvolvimento", DisplayName: "Em Desenvolvimento", ColumnSpan: 6, Visible: true, Key: "EmDesenvolvimento"},
	 {Id: "CadastroUsuarioAutenticacao_lUpTcsUsuarioAcesso_DescricaoAmbienteRelacionado", Name: "CadastroUsuarioAutenticacao_dGridTcsUsuarioAcesso_DescricaoAmbienteRelacionado", DisplayName: "Ambiente Relacionado", ColumnSpan: 9, Visible: true, LookUpName: "LookUpTcsAmbiente1", Key: "DescricaoAmbienteRelacionado"},
	 {Id: "CadastroUsuarioAutenticacao_tbTcsUsuarioAcesso_NomeEmpresaAmbienteRelacionado", Name: "CadastroUsuarioAutenticacao_dGridTcsUsuarioAcesso_NomeEmpresaAmbienteRelacionado", DisplayName: "Empresa Ambiente Relacionado", ColumnSpan: 9, Visible: true, Key: "NomeEmpresaAmbienteRelacionado"},
	 {Id: "CadastroUsuarioAutenticacao_tbTcsUsuarioAcesso_DescricaoAplicacaoAmbienteRelacionado", Name: "CadastroUsuarioAutenticacao_dGridTcsUsuarioAcesso_DescricaoAplicacaoAmbienteRelacionado", DisplayName: "Aplicação Ambiente Relacionado", ColumnSpan: 9, Visible: true, Key: "DescricaoAplicacaoAmbienteRelacionado"},]},]},
	 {Name: "CadastroUsuarioAutenticacao_tiTcsIdentidadeExternaTabItem", DisplayName: "Identidade Externa", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroUsuarioAutenticacao_dGridTcsIdentidadeExterna", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroUsuarioAutenticacao_tbTcsIdentidadeExterna_IdentidadeExterna", Name: "CadastroUsuarioAutenticacao_dGridTcsIdentidadeExterna_IdentidadeExterna", DisplayName: "Identidade Externa", ColumnSpan: 9, Visible: true, Key: "IdentidadeExterna"},]},]},
	 {Name: "CadastroUsuarioAutenticacao_tiTcsUsuarioGpeconTabItem", DisplayName: "Grupo Econômico", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroUsuarioAutenticacao_dGridTcsUsuarioGpecon", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroUsuarioAutenticacao_lUpTcsUsuarioGpecon_IdLinx", Name: "CadastroUsuarioAutenticacao_dGridTcsUsuarioGpecon_IdLinx", DisplayName: "Id Linx Empresa / Grupo Econômico", ColumnSpan: 5, Visible: true, LookUpName: "LookUpTcsEmpresaAutenticacao", Key: "IdLinx"},
	 {Id: "CadastroUsuarioAutenticacao_lUpTcsUsuarioGpecon_NomeEmpresa", Name: "CadastroUsuarioAutenticacao_dGridTcsUsuarioGpecon_NomeEmpresa", DisplayName: "Empresa / Grupo Econômico", ColumnSpan: 7, Visible: true, LookUpName: "LookUpTcsEmpresaAutenticacao", Key: "NomeEmpresa"},]},]},
	 {Name: "CadastroUsuarioAutenticacao_tiTabItem_1e8d7de127cb46f193c1aaf8107ff8bc", DisplayName: "Dados", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroUsuarioAutenticacao_gbGroupBox_cb8535f544794df597d576d728b56350", DisplayName: "Cadastro", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroUsuarioAutenticacao_cmbLxPfjFisicaJuridica", DisplayName: "Pessoa Física / Juridíca", ColumnSpan: 6, Visible: true, Key: "LxPfjFisicaJuridica"},
	 {Name: "CadastroUsuarioAutenticacao_mskCnpjCpf", DisplayName: "CPF/CNPJ", ColumnSpan: 6, Visible: true, Key: "CnpjCpf"},
	 {Name: "CadastroUsuarioAutenticacao_tbInscrEstadualRg", DisplayName: "Inscr. Estadual / RG", ColumnSpan: 6, Visible: true, Key: "InscrEstadualRg"},]},
	 {Name: "CadastroUsuarioAutenticacao_gbGroupBox_cb2e59617f0b4ec08aec5f5bcfc3ea03", DisplayName: "Endereço", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroUsuarioAutenticacao_cmbLxTipoLogradouro", DisplayName: "Tipo Logradouro", ColumnSpan: 6, Visible: true, Key: "LxTipoLogradouro"},
	 {Name: "CadastroUsuarioAutenticacao_tbLogradouro", DisplayName: "Logradouro", ColumnSpan: 8, Visible: true, Key: "Logradouro"},
	 {Name: "CadastroUsuarioAutenticacao_tbNumero", DisplayName: "Número", ColumnSpan: 4, Visible: true, Key: "Numero"},
	 {Name: "CadastroUsuarioAutenticacao_tbComplemento", DisplayName: "Complemento", ColumnSpan: 9, Visible: true, Key: "Complemento"},
	 {Name: "CadastroUsuarioAutenticacao_tbCEP", DisplayName: "CEP", ColumnSpan: 3, Visible: true, Key: "Cep"},
	 {Name: "CadastroUsuarioAutenticacao_tbBairro", DisplayName: "Bairro", ColumnSpan: 9, Visible: true, Key: "Bairro"},
	 {Name: "CadastroUsuarioAutenticacao_tbMunicipio", DisplayName: "Município", ColumnSpan: 9, Visible: true, Key: "Municipio"},
	 {Name: "CadastroUsuarioAutenticacao_tbUf", DisplayName: "UF", ColumnSpan: 1, Visible: true, Key: "Uf"},
	 {Name: "CadastroUsuarioAutenticacao_tbObsEndereco", DisplayName: "Obs. Endereço", ColumnSpan: 9, Visible: true, Key: "ObsEndereco"},]},
	 {Name: "CadastroUsuarioAutenticacao_gbGroupBox_f95f77e96c3c47f7811aec5b4d05a1e9", DisplayName: "Telefones", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroUsuarioAutenticacao_tbFoneFixo", DisplayName: "Fixo / Ramal", ColumnSpan: 6, Visible: true, Key: "FoneFixo"},
	 {Name: "CadastroUsuarioAutenticacao_tbRamal", DisplayName: "Ramal", ColumnSpan: 2, Visible: true, Key: "Ramal"},
	 {Name: "CadastroUsuarioAutenticacao_tbFoneCelular", DisplayName: "Móvel", ColumnSpan: 6, Visible: true, Key: "FoneCelular"},]},]},]},]},       ]};
    };
    
    var layoutDesignerOriginal = objectLayout;
    
    var layoutDesigner = ko.observable(objectLayout());
    
    var flattenLayout = ko.observable(flattenObjectByProperty(layoutDesigner(), 'Name'));
    
    var changeLanguage = function() {
        var idioma = common.getIdioma();
        if (idioma.indexOf('pt-br') >= 0)
            return vm.flattenLayout(ko.observable(flattenObjectByProperty(layoutDesigner(), 'Name'))());
    
        var nameProjectSPA = vm.rootNamespace.toLowerCase().split('.')[0] + "-spa-" + vm.viewName.toLowerCase() + "_" + idioma + ".js";
        var fName = managerAuth.pathLanguageResource + nameProjectSPA;
        require([fName],
            function(result) {
            vm.flattenLayout(ko.observable(flattenObjectByProperty(result.objectLanguage_CadastroUsuarioAutenticacao(), 'Name'))());
        }, function (err) {
           console.log('Arquivo de tradução não encontrado!');
       });
    };
    
    var customSearch = function () { 
        modalCustomSearch.show(vm, dataContext);
    };
    var layout = ko.observable();
    var translatedJEntitySearch = '';
    var customSearchResult = { searchDefinition: '', serializedSearch: '', translatedSearch: '' };
    var hasCustomSearches = ko.observable(false);
    var sortInfo = '';
    var currentSettings = null;
    var registeredUIs = [];
    var dataContext = dataContextFn();
    var complement = ((typeof complementFn === 'function') ? complementFn() : null);
    var viewClosed = false;
    var lastJEntitySearch = null;
    var lastStatus = '';
    var status = ko.observable('N');
    var hideToolbar = ko.observable(false);
    var isDependentVM = ko.observable(false);
    var transactionNumberControl = ko.observable('00000000');
    var navigationByPage = ko.observable(false);
    var viewType = ko.observable('Main');
    var hasMainTopDataGrid = ko.observable(false);
    var currentDataIndex = ko.observable(0);
    var currentDataItem = ko.observable();
    var currentActivityInformation = ko.observable('');
    var currentPage = ko.observable(0);
    var pageCount = ko.observable(0);
    var pageSize = ko.observable(100);
    var totalItemCount = ko.observable(0);
    var isSaving = ko.observable(false);
    var dataView = ko.observableArray([]);
    var dataSource = [];
    var brandDecimals = ko.observable(null)
    
    var showDataFeedUrl = function() {
        if (!OnToolbarAction('ShowFeed')) return;
        app.showMessage(dataContext.getDataFeedUrl(), 'Endereço do serviço', ['Ok']);
    };
    var lastSearchFilter = function () {
        if (!OnToolbarAction('ShowCurrentFilter')) return;
        var filterTranslation = getTranslatedFilter();
        app.showMessage((isNullOrEmpty(filterTranslation) ? 'Pesquisa sem filtros.' : filterTranslation), 'Filtros da pesquisa');
    }
    var registerUI = function (name, viewPath, settings) {
        registeredUIs.push(name);
        registeredUIs[name] = {
            uiName: viewPath,
            uiSettings: settings
        };
    }
    
    var showRegisteredUI = function (name, elementName) {
        var ctrl = $('#' + elementName);
        var bindingContext = ko.contextFor(ctrl[0]);
        var uiSelected = registeredUIs[name];
        if (uiSelected.length == 0){
            console.warn('Não foi encontrado o elemento [' + elementName + ']');
            return;
        }
        var settings = {
            kind: uiSelected.uiName,
            parentVM: vm,
            uiSettings: uiSelected.uiSettings
        };
        var ext;
        ctrlName = elementName + "_" + name;
        if ($('#' + ctrlName).length == 0)
            ext = ctrl.append("<div id='" + elementName + "_" + name + "' />");
        else
            ext = $('#' + ctrlName);
        widget.create(ext[0], settings, bindingContext, true);
    };
    var currentRecord = ko.computed(function () {
        if (pageSize() === 0) return currentDataIndex();
        else return (currentPage() * pageSize()) + currentDataIndex();
    });
    
    var _isBusy = false;
    var isBusy = function isBusy(value) {
        if (typeof value === 'undefined') {
            return _isBusy;
        } else {
            _isBusy = value;
            if ($(".page-container").html() == undefined || $(".page-container").html().length == 0)
            return;
            if (value) { common.showProcess('#main'); }
            else { common.closeProcess('#main'); }
        }
    };
    var totalRecords = ko.computed(function () {
        if (pageSize() === 0) return dataView().length;
    
        var recordCount = 0;
        if (currentPage() === 0) {
            if (pageCount() <= 1) {
                 recordCount = dataView().length;
            } else {
                 recordCount = totalItemCount() - pageSize() +  dataView().length;
            }
        } else if (currentPage() === (pageCount() - 1)) {
            recordCount = (pageSize() * (pageCount() - 1)) + dataView().length;
        } else {
            recordCount = pageSize() * (currentPage() + 1);
            recordCount += totalItemCount() - (pageSize() * (currentPage() + 2));
            recordCount += dataView().length;
        }
        return recordCount;
    });
    var currentFormattedRecord = ko.computed(function () {
        if (totalRecords() === 0) return '0';
        else return (currentRecord()+1).toString();
    });
    var currentRecordInfo = ko.computed(function () { var totalR = totalRecords(); if (totalR === 0) { return '0/0'; } else { return currentFormattedRecord() + '/' + totalR.toString(); } });
    var contextDataUpdateHandler = function (e) {
        dataBind(dataContext.dataForUpdate);
    };
    //#region Durandal Events
    
    var started = false;
    var parentVM = null;
    var uiSettings = null;
    var filteredEntities = [];
    //#region quick search
    var quickSearch = function () {
    }
    //#endregion 
    var activate = function (settings, querystring) {
      if (typeof common.getTransactionCode === 'function') transactionNumberControl(common.getTransactionCode());
        OnToolbarAction('Open');
      vm.CadastroUsuarioAutenticacao = getVM;
      //loadLanguage();
      changeLanguage();
      if ((typeof settings === 'object') && (settings != null)) {
          currentSettings = settings;
      }
      if ((typeof settings === 'object') && (settings != null) && settings.objectQuery) {
          isDependentVM(false);
          parentVM = null;
          filteredEntities = [];
          clear();
          var fieldProperty, value;
          if (!isNullOrEmpty(settings.objectQuery)) {
              $.each(settings.objectQuery.split(';'), function (idxElement, element) {
                  var idx = element.indexOf(':');
                  if (idx >= 0) {
                      field = element.slice(0, idx).trim();
                      value = element.slice(idx + 1, element.length);
                      setAbsoluteValue(currentDataItem(), field, value);
                  }
              });
          }
          if (settings.executeQuery == 'true')
              query(true);
          if (window.location.hash)
              history.replaceState(undefined, undefined, window.location.hash.substring(0, window.location.hash.indexOf('?')))
      }
      else {
          if ((typeof settings === 'object') && (settings != null) && settings.uiSettings) {
              uiSettings = settings.uiSettings;
              isDependentVM(true);
              parentVM = null;
              if (uiSettings.executeQuery === true) {
                  if (uiSettings.toolbarSettings) {
                      setSecurity(uiSettings.toolbarSettings.canAddNew, uiSettings.toolbarSettings.canClear, uiSettings.toolbarSettings.canCustomSearch, uiSettings.toolbarSettings.canDelete, uiSettings.toolbarSettings.canEdit, uiSettings.toolbarSettings.canLayout, uiSettings.toolbarSettings.canNavigate, uiSettings.toolbarSettings.canPrint, uiSettings.toolbarSettings.canSearch, uiSettings.toolbarSettings.canExport, uiSettings.toolbarSettings.noBusyLoading);
                      hideToolbar(uiSettings.toolbarSettings.removeDataToolbar);
                  }
                  filteredEntities = [];
                  if (settings.parentVM) { settings.parentVM.internalUIs = [ 'CadastroUsuarioAutenticacao' ]; settings.parentVM.CadastroUsuarioAutenticacao = getVM; }
                  clear();
                  if ((typeof uiSettings.querySetters === 'object')) {
                      for (var field in uiSettings.querySetters) {
                           if (field.indexOf('entitySearchRange') >= 0){
                              setAbsoluteValue(vm.entitySearchRange, field.split('.')[1], uiSettings.querySetters[field]);
                           }
                           else {
                               setAbsoluteValue(currentDataItem(), field, uiSettings.querySetters[field]);
                           }
                      }
                  }
                  query(true);
              }
              else {
                       if (uiSettings.toolbarSettings) {
                           setSecurity(uiSettings.toolbarSettings.canAddNew, uiSettings.toolbarSettings.canClear, uiSettings.toolbarSettings.canCustomSearch, uiSettings.toolbarSettings.canDelete, uiSettings.toolbarSettings.canEdit, uiSettings.toolbarSettings.canLayout, uiSettings.toolbarSettings.canNavigate, uiSettings.toolbarSettings.canPrint, uiSettings.toolbarSettings.canSearch, uiSettings.toolbarSettings.canExport, uiSettings.toolbarSettings.noBusyLoading);
                           hideToolbar(uiSettings.toolbarSettings.removeDataToolbar);
                       }
                       else {
                           setSecurity(uiSettings.canAddNew, uiSettings.canClear, uiSettings.canCustomSearch, uiSettings.canDelete, uiSettings.canEdit, uiSettings.canLayout, uiSettings.canNavigate, uiSettings.canPrint, uiSettings.canSearch, uiSettings.canExport, uiSettings.noBusyLoading);
                           hideToolbar(uiSettings.removeDataToolbar);
                       }
                  if ((typeof settings.parentVM === 'object') && settings.parentVM != null) {
                      parentVM = settings.parentVM;
                      parentVM.CadastroUsuarioAutenticacao = getVM;
                      if (isLookup()) { 
                          parentVM.internalUIs = [];
                          filteredEntities = [];
                          clear();
                          if (!isNullOrEmpty(uiSettings.valueToSearch)) {
                              if (typeof currentDataItem()[uiSettings.fieldToSearch] === 'function') {
                                  currentDataItem()[uiSettings.fieldToSearch](uiSettings.valueToSearch);
                                  query(true);
                              }
                          }
                      }
                      if ($.inArray('CadastroUsuarioAutenticacao', parentVM.internalUIs) === -1){
                           if (parentVM.internalUIs) {
                               parentVM.internalUIs.push('CadastroUsuarioAutenticacao');
                           }
                           else {
                               parentVM.internalUIs = ['CadastroUsuarioAutenticacao'];
                           }
                      }
                  }
              }
          }
          else {
              app.on('shell:close:all').then(function () {
                  viewClosed = true;
                  filteredEntities = [];
                  clear();
              });
              if (viewClosed == true){
                  viewClosed = false;
                  loadDataView();
              }
              adjustModuleSecurity();
          }
      }
      if (isChildVM() && (!_canNavigate || hideToolbar() || _canAddNew || _canDelete || _canEdit))
           pageSize(0);
      document.addEventListener(dataContext.contextUpdtEvt, contextDataUpdateHandler, false);
      if (!started) { started = true; clear(); } else { viewType('Main'); refreshToolbar(); }
      //Call OnLoadedChildUI Event
      if (isChildVM() && !isLookup()) {
        if (typeof parentVM.OnLoadedChildUI === 'function')
            parentVM.OnLoadedChildUI(vm);
      }
    };
    
    var adjustModuleSecurity = function () {
        parentVM = null;
        uiSettings = null;
        isDependentVM(false);
        setSecurity(true, true, true, true, true, true, true, true, true, true);
        managerAuth.getFormAccess('linx-framework-bv-spa-CadastroUsuarioAutenticacao', function (data) {
           if (data && !data.AcessoTotal) {
              setSecurity(data.Incluir, true, data.PesquisaEspecial, data.Excluir, data.Alterar, data.Layout, true, data.Imprimir, data.Pesquisar, data.Exportar);
           }
        }, logger);
    };
    
    var getVM = function () {
        return vm;
    };
    
    var binding = function () {
        if (!isChildVM()) vm.showProcessing('Inicializando...');
        return { cacheViews: false };
    };
    
    var bindingComplete = function () {
        return true;
    };
    var attached = function(view, parent) {
    };
    var canDeactivate = function () {
        if (require('plugins/dialog').isOpen())
            return false;
        try {
            var dlg =  $('.toolbar-dialog-template:visible')[0].id;
            if ($('#' + dlg).dialog('isOpen'))
                return false;
        } catch (e) {}
        if (status() === 'E') {
            return app.showMessage('Deseja realmente sair e cancelar o trabalho corrente?', 'Alerta', ['Yes', 'No'])
                .then(function (selectedOption) {
                    if (selectedOption === 'Yes') {
                       undo();
                   }
                   return selectedOption;
              });
      }
      return true;
    };
    var canActivate = function() {
        var data = router.activeInstruction().config;
        if (data.lxShellCompiledVersion != managerAuth.shellVersion) {
            app.showMessage('Versão de formulário incompatível com a versão de ambiente [' + managerAuth.shellVersion + '].', 'Formulário: CadastroUsuarioAutenticacao', ['Ok']);
            return false;
        }
        return true;
    };
    var deactivate = function() {
       document.removeEventListener(dataContext.contextUpdtEvt, contextDataUpdateHandler, false);
    };
    var compositionComplete = function() {
        //changeLanguage();
        $('#CadastroUsuarioAutenticacao_tcTcsUsuarioAutenticacaoTabControl').on('shown.bs.tab', function (e) { vm.notifyInnerElements($(e.target.hash)); });
    initializeTabControl('#CadastroUsuarioAutenticacao_tcTcsUsuarioAutenticacaoTabControl');

    complement.renderCadastroUsuarioAutenticacao_dGridTcsUsuarioAcesso(vm);

    complement.renderCadastroUsuarioAutenticacao_dGridTcsIdentidadeExterna(vm);

    complement.renderCadastroUsuarioAutenticacao_dGridTcsUsuarioGpecon(vm);

    complement.renderscyCadastroUsuarioAutenticacao_dGrid(vm);


        if (!hasMainTopDataGrid() && isChildVM()) removeFormViewControl();
        navigationByPage(hasMainTopDataGrid());
        dataBind();
        if (!isChildVM()) { vm.closeProcessing(); }
        try{ $(window).trigger('resize'); } catch(e){ console.log(e); }
        //Form startup routine
        if (currentSettings != null)
        {
            if (!isNullOrEmpty(currentSettings.action))
            {
                if (currentSettings.action.toLowerCase() == 'new')
                {
                    if (dataToolbar.canAddNew())
                    {
                        dataToolbar.addNew();
                    }
                }
            }
        }
        OnLoaded();
        scrollMainTop();
        currentDataItem.subscribe(function (item) {
            refreshMembershipBlocked(item);
        });
        vm.currentBrands.subscribe(function(newValue) {
            newValue = isNull(newValue) ? vm.currentBrands() : newValue;
            var searchedBrands = managerBrand.searchBrandsVM(newValue, managerAuth.getIdTcsAmbiente());
            var reset = (!newValue || searchedBrands.cod === ''), decimals = searchedBrands.decimals;
                               complement.ChangedBrandCadastroUsuarioAutenticacao_dGridTcsUsuarioAcesso(vm, decimals, reset);
                           complement.ChangedBrandCadastroUsuarioAutenticacao_dGridTcsIdentidadeExterna(vm, decimals, reset);

                           complement.ChangedBrandCadastroUsuarioAutenticacao_dGridTcsUsuarioGpecon(vm, decimals, reset);



            vm.brandDecimals(reset || isNull(decimals) ? null : decimals);
            vm.currentDataItem.notifySubscribers();
        });
        vm.currentBrands.notifySubscribers();
        getLayoutFormPadrao(vm);
        return true;
    };
    var detached = function (view) {
        OnToolbarAction('Close');
       viewDetached(view, viewClosed);
    };
    //#endregion
    var getDecimalsByData = function getDecimalsByData(data, defaultValue) {
        var decimals = vm.brandDecimals();
        if (!isNull(data)) {
            if (data['IdBandeiraRede'] && getAbsoluteValue(data['IdBandeiraRede']) > 0) {
                var searchedBrands = managerBrand.searchBrandsVM(getAbsoluteValue(data['IdBandeiraRede']), managerAuth.getIdTcsAmbiente());
                decimals = searchedBrands.decimals;
            }
            if (data['NumeroDecimais'] && getAbsoluteValue(data['NumeroDecimais']) > 0)
                decimals = getAbsoluteValue(data['NumeroDecimais']);
        }
        return isNullOrEmpty(decimals) ? defaultValue : decimals;
    };
    var getMaxLength = function(entityName, propertyName){
        if (isNullOrEmpty(entityName)) entityName = 'TcsUsuarioAutenticacao';
        var property = dataContext.getEntityProperty(entityName, propertyName);
        if(property != null)
            return property.maxLength;
        else
            return 0;
    };
    var isDataSourceHided = function (dataName) {
        var url = (document.URL.contains('?') ? document.URL.substring(0, document.URL.indexOf('?')) : document.URL);
        if (vm.dataSource.length > 0 && vms[url] === vm) {
           for (var db in vm.dataSource) { if (vm.dataSource[db].name === dataName && (typeof vm.dataSource[db].itemsSource.isElementHided === 'function')) { return vm.dataSource[db].itemsSource.isElementHided(); } }
        }
        return false;
    };
    var dataBind = function (dataName, commitData) {
        var url = (document.URL.contains('?') ? document.URL.substring(0, document.URL.indexOf('?')) : document.URL);
        if (vm.dataSource.length > 0 && vms[url] === vm) {
           for (var db in vm.dataSource) { if (!dataName || dataName === '' || vm.dataSource[db].name === dataName) { vm.dataSource[db].itemsSource.dataBind(commitData); } }
        }
    };
    var getVisibleProperties = function (dataName) {
        if (vm.dataSource.length > 0) {
            for (var db in vm.dataSource) { if (vm.dataSource[db].name === dataName && (typeof vm.dataSource[db].itemsSource.getVisibleColumns === 'function')) { return 'LinqValidProperties{LinqValidProperties#==#S' + vm.dataSource[db].itemsSource.getVisibleColumns(true) + '}'; } }
        }
        return '';
    };
    
    var visibleColumns = 'NomeUsuario,NomeAutenticacao,NomeCurtoUsuario,Email,NomeEmpresa,AutenticacaoWindows,IndicaAcessoSuporte,IndicaUsuarioServico,VigenciaInicial,VigenciaFinal,DataExpiracaoSenha,DataCadastro,DataAlteracao,Inativo,ConfirmacaoUsuario,ConfirmacaoUsuario1,LxPfjFisicaJuridica,CnpjCpf,InscrEstadualRg,LxTipoLogradouro,Logradouro,Numero,Complemento,Cep,Bairro,Municipio,Uf,ObsEndereco,FoneFixo,Ramal,FoneCelular';
    
    var getVisiblePropertiesForExcel = function (dataName) {
        if (vm.dataSource.length > 0) {
            for (var db in vm.dataSource) {
                if (vm.dataSource[db].name === dataName && (typeof vm.dataSource[db].itemsSource.getVisibleColumns === 'function')) {
                   if (vm.dataSource[db].itemsSource.getVisibleColumns() === "") return visibleColumns;
                   return vm.dataSource[db].itemsSource.getVisibleColumns();
                }
            }
        }
        return dataName === 'dataView' ? visibleColumns : '';
    };
    
    var addDataSource = function (dsElement) {
        if (!dsElement.key) return;
        var foundElement = null;
        for (var ds in vm.dataSource) { if (vm.dataSource[ds].key === dsElement.key) { foundElement = vm.dataSource[ds]; break; } }
        if (foundElement === null) { vm.dataSource.push(dsElement); } else { foundElement.itemsSource = dsElement.itemsSource; }
    };
    var loadDataView = function () {
    
    };
    var getInnerJExpression = function () {
        if (!uiSettings.applyFilterToParent || isNullOrEmpty(currentDataItem())) return '';
        dataBind('', true);
        var parentFieldsRelation = '';
        var detailFieldsRelation = '';
        if (uiSettings != null && uiSettings.parentFieldsRelation.length == uiSettings.detailFieldsRelation.length) {
          for (var idx = 0; idx < uiSettings.parentFieldsRelation.length; idx++) {
             parentFieldsRelation += (parentFieldsRelation == '' ? '' : ',') + uiSettings.parentFieldsRelation[idx];
             detailFieldsRelation += (detailFieldsRelation == '' ? '' : ',') + uiSettings.detailFieldsRelation[idx];
          }
        }
        var jExp = getQueryFilter(currentDataItem());
        if (jExp === 'Error') return 'Error';
        return '---' + currentDataItem().namespace + '.' + currentDataItem().typeName + '|' + uiSettings.parentSelectorDataName + '|' + parentFieldsRelation + '|' + detailFieldsRelation + ':::' + jExp;
    };
    var clearInnerUIs = function (parentEntity) {
       for (var idx = 0; idx < vm.internalUIs.length; idx++) { var innerVM = vm[vm.internalUIs[idx]](); if (isNullOrEmpty(parentEntity) || innerVM.getParentSelectorDataName() === parentEntity.typeName) innerVM.dataToolbar.clear(); }
    };
    var replaceInnerUIsKeys = function (parentEntity, parentPropertyName, oldValue, newValue) {
       for (var idx = 0; idx < vm.internalUIs.length; idx++) { var innerVM = vm[vm.internalUIs[idx]](); if (innerVM.getParentSelectorDataName() === parentEntity.typeName) innerVM.replaceKeyFromParent(parentPropertyName, oldValue, newValue); }
    };
    var replaceKeyFromParent = function (parentPropertyName, oldValue, newValue) {
        if (parentEntityRelated != null && isChildVM() && uiSettings.detailFieldsRelation.length == 1 && uiSettings.parentFieldsRelation.length == 1 && uiSettings.parentFieldsRelation[0] === parentPropertyName) {
            dataBind('dataView', true);
            var cacheElements = getAddedEntities();
            for (var idxR = 0; idxR < cacheElements.length; idxR++) {
                if (getAbsoluteValue(cacheElements[idxR][uiSettings.detailFieldsRelation[0]]) == oldValue) setAbsoluteValue(cacheElements[idxR], uiSettings.detailFieldsRelation[0], newValue);
            }
        }
    };
    var getInnerJExpressions = function () {
       var innerFilters = '';
       for (var idx = 0; idx < vm.internalUIs.length; idx++) { var eSearch = vm[vm.internalUIs[idx]]().getInnerJExpression(); if (eSearch === 'Error') return 'Error';  if (eSearch.indexOf('#') >= 0) innerFilters += eSearch; }
       return innerFilters;
    };
    var getParentSelectorDataName = function () {
       return ((typeof uiSettings === 'object') ? uiSettings.parentSelectorDataName : '');
    };
    var validParentSelectorDataCondition = function (data) {
       return ((typeof uiSettings === 'object') && !isNullOrEmpty(uiSettings.parentSelectorDataCondition) ? eval(uiSettings.parentSelectorDataCondition) : true);
    };
    var getJExpression = function (currentDI) {
        if (typeof currentDI === 'undefined') currentDI = currentDataItem();
        if (parentEntityRelated != null && isChildVM()) {
           for (var idx = 0; idx < uiSettings.parentFieldsRelation.length; idx++) { setAbsoluteValue(currentDI, uiSettings.detailFieldsRelation[idx], getAbsoluteValue(parentEntityRelated[uiSettings.parentFieldsRelation[idx]])); }
        }
        var extraFilters = '';
        if (isLookup()) {
             extraFilters = uiSettings.ownerReference.getLookUpClientFilterExpressions(uiSettings.lookupName, uiSettings.lookupInfo);
             if (extraFilters === 'Error') return extraFilters;
             if (typeof uiSettings.ownerReference['BeforeGet' + uiSettings.lookupName + 'Query'] == 'function') {
                   var customFilter = uiSettings.ownerReference['BeforeGet' + uiSettings.lookupName + 'Query']('', uiSettings.lookupInfo);
                   if (customFilter === 'Error') return null;
                   if (!isNullOrEmpty(customFilter)) { extraFilters = (isNullOrEmpty(extraFilters) ? '' : extraFilters + ';') + customFilter; }
             }
             if (!isNullOrEmpty(extraFilters)) extraFilters = currentDI.typeName + '{' + extraFilters + '}';
        }
        var innerExps = getInnerJExpressions();
        if (innerExps === 'Error') return 'Error';
        return currentDI.getJExpression(vm.entitySearchRange, [], (parentEntityRelated != null)) + extraFilters + innerExps;
    };
    var getSpecializedLookupItems = function () {
       var result = [];
       if (dataView().length > 1 && !isNullOrEmpty(complement) && (typeof complement.selectedCurrentItems === 'function'))
           result = complement.selectedCurrentItems(false, true);
       if ((dataView().length == 1 || !navigationByPage() || isNullOrEmpty(complement) || (typeof complement.selectedItems !== 'function') || (uiSettings && uiSettings.allowMultiSelectionInSearch === false)) && result.length == 0)
           result.push(currentDataItem());
       return result;
    };
    var exportData = function (forceAdd, isExcelDataSource) {
        if (!OnToolbarAction('Export')) return;
        if (forceAdd)
            require(['viewmodels/shared/addCustomExport'],
                function(addCustomExport){ addCustomExport.showModal(vm, null, 'TcsUsuarioAutenticacao', getVisiblePropertiesForExcel('dataView'), null, true, isExcelDataSource); } );
        else
            require(['viewmodels/shared/customExport'],
                function(modalExport){ modalExport.showModal(vm, 'TcsUsuarioAutenticacao', getVisiblePropertiesForExcel('dataView'), null, { canAdd: true, canEdit: true, canDel: true }, isExcelDataSource); } );
    };
    var exportDataDetails = function (entity, detailName, isExcelDataSource) {
        require(['viewmodels/shared/addCustomExport'], function(addCustomExport){
             addCustomExport.showModal(vm, null, detailName, getVisiblePropertiesForExcel(detailName + 'List'), entity['GetJsWhereDetailRelationFor' + detailName](), true, isExcelDataSource); } 
        );
    };
    var customLayout = function() {
        require(['viewmodels/shared/customLayoutForm'],
            function(customLayout) { customLayout.showModal(vm); });
    }
    var finalizeCombo = function (current, itens, lookupName) {
       dataContext['finalizeAll' + lookupName](current, itens, '', '');
    };
    var clearCombo = function (current, lookupName) {
       dataContext['clear' + lookupName](current);
    };
    var dataCombo = {
        combos: [],
        getItems: function (comboName, valuesFilter) {
            var items = dataCombo.combos[comboName];
            if (!isNullOrEmpty(valuesFilter) && items && items.length > 0) {
                for (var i = items.length - 1; i >= 0; i--) {
                    if ((',' + valuesFilter + ',').indexOf(',' + items[i].id + ',') === -1) {
                        items.removeAt(i);
                    }
                }
            }
            return (items && items.length > 0 ? items : []);
        },
        fillDataCombos: function (lookupName, fieldName, current, complete) {
            dataContext.getResultsCombo(lookupName, fieldName, current, function (result) {
                dataCombo.combos[lookupName] = result;
                if (complete) complete();
            });
        },
        isFilterChanged: function (lookupName, current) {
            return dataContext.clientFilterHasModified(lookupName, current);
        }
    };
    var refreshCurrentData = function () {
        if (!OnToolbarAction('Refresh')) return;
        if (navigationByPage()) {
           var refreshIndexedData = function (currentIndex) {
                 if (currentIndex < dataView().length) {
                     if (currentIndex == 0) vm.showProcessing('Atualizando informações...');
                     dataView()[currentIndex].refreshData(true, function (data) { if (data.results.length == 0) { app.showMessage('A informação a ser atualizada não está mais presente na base de dados!', 'Alerta', ['Ok']); vm.closeProcessing(); return; } refreshIndexedData(currentIndex + 1); });
                 }
                 else {
                     vm.closeProcessing();
                     dataBind();
                 }
           };
           if (dataView().length > 0) {
                refreshIndexedData(0);
           }
           return;
        }
        vm.showProcessing('Atualizando informações...');
        return currentDataItem().refreshData(false, complete);
    
        function complete(data) {
            if (data.results.length == 0) { app.showMessage('A informação a ser atualizada não está mais presente na base de dados!', 'Alerta', ['Ok']); vm.closeProcessing(); return; }
            currentDataItem.notifySubscribers();
            vm.closeProcessing();
        }
    }
    var _pendingRefresh = false;
    var lazyRefreshBinding = function () {
       if (!_pendingRefresh) {
           _pendingRefresh = true;
           setTimeout(function () { currentDataItem.notifySubscribers(); _pendingRefresh = false; }, 500);
       }
    };
    var getTranslatedFilter = function () {
        return translatedJEntitySearch + (isNullOrEmpty(translatedJEntitySearch) || isNullOrEmpty(customSearchResult.translatedSearch) ? '' : ' e ') + customSearchResult.translatedSearch;
    }
    // Membership "Bloqueado" is unbound — capture filter in search (C) and apply after OData returns (not in edit E).
    var pendingBlockedFilter = null;
    var getQueryFilter = function (currentDI) {
        if (typeof currentDI === 'undefined') currentDI = currentDataItem();
        dataBind('', true);
        currentDI.setBandeiraRede(getBandeiraRede());
        pendingBlockedFilter = null;
        try {
            // Only filter when Bloqueado is checked (true). Unchecked/false means "no Membership filter".
            if (!isNullOrEmpty(currentDI) && typeof currentDI.Blocked !== 'undefined' && status() !== 'E') {
                if (getAbsoluteValue(currentDI.Blocked) === true)
                    pendingBlockedFilter = true;
            }
        } catch (eBlocked) { pendingBlockedFilter = null; }
        eSearch = getJExpression(currentDI);
        if (eSearch === 'Error')
           return 'Error';
       translatedJEntitySearch = common.translateSearch(dataContext, eSearch);
        if (!isNullOrEmpty(customSearchResult.searchDefinition)) eSearch += customSearchResult.searchDefinition;
        return eSearch;
    }
    var applyPendingBlockedFilter = function (results, done) {
        if (pendingBlockedFilter === null || !(results instanceof Array) || results.length === 0) {
            if (typeof done === 'function') done(results || []);
            return;
        }
        var wantBlocked = pendingBlockedFilter;
        pendingBlockedFilter = null;
        var remaining = results.length;
        var kept = [];
        var finishOne = function () {
            remaining--;
            if (remaining <= 0 && typeof done === 'function') done(kept);
        };
        for (var i = 0; i < results.length; i++) {
            (function (item) {
                ensureBlockedObservable(item);
                var loginName = getAbsoluteValue(item.NomeAutenticacao);
                if (isNullOrEmpty(loginName)) {
                    if (wantBlocked === false) kept.push(item);
                    finishOne();
                    return;
                }
                $.ajax({
                    url: managerAuth.getServiceAddress('LinxFrameworkAutorizacao', 'Linx.Framework.BV') + '/IsMembershipUserLockedOut',
                    data: { userName: loginName },
                    type: 'GET',
                    dataType: 'json'
                }).done(function (locked) {
                    var isLocked = parseMembershipLocked(locked);
                    setAbsoluteValue(item, 'Blocked', isLocked);
                    if (isLocked === wantBlocked) kept.push(item);
                }).fail(function () {
                    if (wantBlocked === false) kept.push(item);
                }).always(finishOne);
            })(results[i]);
        }
    }
    var queryInnerUIs = function (parentEntity, parentTypeName) {
       if (status() === 'C') return;
       commitInternalUIsData();
       for (var idx = 0; idx < vm.internalUIs.length; idx++) { var innerVM = vm[vm.internalUIs[idx]](); if ((!isNullOrEmpty(parentTypeName) && innerVM.getParentSelectorDataName() === parentTypeName) || (!isNullOrEmpty(parentEntity) && innerVM.getParentSelectorDataName() === parentEntity.typeName)) { if (isNullOrEmpty(parentEntity) || innerVM.validParentSelectorDataCondition(parentEntity)) innerVM.dataToolbar.query(false, parentEntity); else if (innerVM.status() === 'Q') innerVM.clear();  } }
    };
    var addNewToInnerUI = function (parentEntity, uiName) {
       setTimeout(function () {
           for (var idx = 0; idx < vm.internalUIs.length; idx++) { var innerVM = vm[vm.internalUIs[idx]](); if (innerVM.getParentSelectorDataName() === parentEntity.typeName && (isNullOrEmpty(uiName) || innerVM.viewName === uiName)) innerVM.dataToolbar.addNew(parentEntity); }
       }, 1000);
    };
    var removeInnerDataUIs = function (parentEntity) {
       for (var idx = 0; idx < vm.internalUIs.length; idx++) { var innerVM = vm[vm.internalUIs[idx]](); if (!isNullOrEmpty(parentEntity) && innerVM.getParentSelectorDataName() === parentEntity.typeName) innerVM.removeParentRelatedItems(parentEntity); }
    };
    var getDataFromInnerUI = function (uiName) {
       for (var idx = 0; idx < vm.internalUIs.length; idx++) { var innerVM = vm[vm.internalUIs[idx]](); if (innerVM.viewName === uiName) return innerVM.currentDataItem(); }
    };
    var saveInnerUIs = function () {
      var vmsForSaving = [];
      var saveInnerUI = function (currentIndex) {
            if (currentIndex < vmsForSaving.length)
                vmsForSaving[currentIndex].dataToolbar.save(false, function () { saveInnerUI(currentIndex + 1); });
      };
      for (var idx = 0; idx < vm.internalUIs.length; idx++) {
          var innerVM = vm[vm.internalUIs[idx]]();
          if (innerVM.status() === 'E') vmsForSaving.push(innerVM);
      }
      if (vmsForSaving.length > 0) {
           saveInnerUI(0);
      }
    };
    var undoInnerUIs = function () {
      for (var idx = 0; idx < vm.internalUIs.length; idx++) { var innerVM = vm[vm.internalUIs[idx]](); if (innerVM.status() === 'E') innerVM.dataToolbar.undo(); }
      if (status() === 'Q' && !isNullOrEmpty(currentDataItem())) {
           for (var idx = 0; idx < vm.internalUIs.length; idx++) { var innerVM = vm[vm.internalUIs[idx]](); innerVM.dataToolbar.clear(); }
           currentDataItem().fillDetails();
      }
    };
    var editInnerUIs = function () {
      for (var idx = 0; idx < vm.internalUIs.length; idx++) { var innerVM = vm[vm.internalUIs[idx]](); if (innerVM.isEditable()) innerVM.dataToolbar.edit(); } 
    };
    var setStatus = function (st) {
      status(st);
      goToIndex(currentDataIndex());
    };
    var getAllChanges = function () {
        return dataContext.getChanges();
    }
    var getAddedEntities = function () {
        return dataContext.getEntities('TcsUsuarioAutenticacao', [dataContext.breeze.EntityState.Added]);
    }
    var getRelatedElementsInCache = function () {
        if (parentEntityRelated != null && preserveDataCurrentState()) {
           var cacheElements = dataContext.getEntities('TcsUsuarioAutenticacao');
           var result = [];
           var relationExpr = '';
           for (var idx = 0; idx < uiSettings.parentFieldsRelation.length; idx++) { relationExpr += (relationExpr === '' ? '' : ' && ') + 'getAbsoluteValue(cacheElements[idxR][uiSettings.detailFieldsRelation[' + idx.toString() + ']]) === getAbsoluteValue(parentEntityRelated[uiSettings.parentFieldsRelation[' + idx.toString() + ']])'; }
           for (var idxR = 0; idxR < cacheElements.length; idxR++) {
               if (eval(relationExpr)) { result.push(cacheElements[idxR]); }
           }
           dataView(result);
           return (dataView().length > 0 ? 0 : (parentEntityRelated.isAdded() ? 0 : -1));
        }
        return -1;
    };
    var isChildVM = function () {
       return (parentVM != null && uiSettings != null && !isNullOrEmpty(uiSettings.parentSelectorDataName) && (typeof uiSettings.parentFieldsRelation !== 'undefined') && (typeof uiSettings.detailFieldsRelation !== 'undefined') && uiSettings.parentFieldsRelation.length == uiSettings.detailFieldsRelation.length) && !isLookup();
    }
    var isLookup = function () {
       return (uiSettings != null && (typeof uiSettings.lookupInfo === 'object'));
    };
    var allowMultiSelectionInSearch = function () {
       if (isLookup() && (typeof uiSettings.allowMultiSelectionInSearch !== 'undefined')) return uiSettings.allowMultiSelectionInSearch;
       else return true;
    };
    var parentEntityRelated = null;
    var freeEntityForQuerying = null;
    var isProcessing = false;
    var adjustExternalParentRelation = function (selectedElement) {
        if (isNullOrEmpty(selectedElement)) selectedElement = currentDataItem();
        if (parentEntityRelated != null && isChildVM() && (uiSettings.canAddNew || uiSettings.canEdit || uiSettings.canDelete)) {
            for (var idx = 0; idx < uiSettings.parentFieldsRelation.length; idx++) { setAbsoluteValue(selectedElement, uiSettings.detailFieldsRelation[idx], getAbsoluteValue(parentEntityRelated[uiSettings.parentFieldsRelation[idx]])); }
        }
    };
    var openingExternalUIFromGrid = function (externalUIName, qbeSearch) {
       return qbeSearch;
    }
    function restoreLastFilter(clearFilters) {
            if (isChildVM()) { filteredEntities = []; return false; }
            if (clearFilters || !common.getLastFilterMode()) filteredEntities = [];
            if (filteredEntities.length === 0) return false;
            dataContext.clearAll();
            //Attach Elements
            for(var idx = 0; idx < filteredEntities.length; idx++) { dataContext.attachEntity(filteredEntities[idx]); }
            //Set Current Details
            for(var idx = 0; idx < filteredEntities.length; idx++) { filteredEntities[idx].setCurrentDetails(null, true); }
            dataView([filteredEntities[0]]);
            if (clearFilters) filteredEntities = [];
            return true;
    }
    
    function adjustNavigationByPage(isNavByPage) {
        navigationByPage(isNavByPage);
        dataBind();
    }
    
    var preserveDataCurrentState = function () {
       return (status() !== 'C' && pageSize() === 0 && isChildVM());
    }
    
    var detachFilteredEntities = function (clear) {
        if (filteredEntities.length > 0) {
            for (var idx = 0; idx < filteredEntities.length; idx++) {
                dataContext.detachEntity(filteredEntities[idx]);
            }
            if (clear) filteredEntities = [];
        }
    }
    
    var query = function (lookupInitializing, parentEntity, quickSearchJExpression, externalQueryCallBack, noMessages, noDetails) {
        if (!OnToolbarAction('Query')) return;
        if (isProcessing) return;
        isProcessing = true;
        vm.canReportErrors = false;
        if (lookupInitializing === true && uiSettings && uiSettings.modalForm && (typeof uiSettings.modalForm.hide === 'function')) uiSettings.modalForm.hide(true);
        if (!isNullOrEmpty(parentEntity) && !isNullOrEmpty(parentEntity.typeName))
           parentEntityRelated = parentEntity;
        else
           parentEntityRelated = null;
        if ((isNullOrEmpty(parentEntityRelated) || (status() === 'C' && (parentEntityRelated != null && parentEntityRelated.isAdded()))) && isChildVM()) { dataContext.clearAll(); if (isNullOrEmpty(parentEntityRelated)) { currentDataItem(null); querySucceeded({ results: [] }); return complete(); } }
        if ((status() !== 'C' || (parentEntityRelated != null && parentEntityRelated.isAdded())) && getRelatedElementsInCache() >= 0) { querySucceeded({ results: dataView() }); return complete(); }
        if (freeEntityForQuerying == null && isChildVM()) freeEntityForQuerying = dataContext.createFreeEntity('TcsUsuarioAutenticacao');
        if (status() === 'C' && !isNullOrEmpty(currentDataItem()) && currentDataItem().getCurrentElements) {
            filteredEntities = currentDataItem().getCurrentElements();
            if (isChildVM())
                detachFilteredEntities(true);
        }
        else
            filteredEntities = [];
        if (uiSettings != null && uiSettings.noSearch) { dataView([currentDataItem()]); status('Q'); refreshToolbar(); return complete(); }
        lastJEntitySearch = (isNullOrEmpty(quickSearchJExpression) ? '' : quickSearchJExpression) + getQueryFilter((isChildVM() ? freeEntityForQuerying : currentDataItem()));
        if (lastJEntitySearch === 'Error')
            return complete();
        var hasError = true;
        if (status() === 'C') { detachFilteredEntities(); }
        if (isChildVM() && (uiSettings.canAddNew || uiSettings.canEdit || uiSettings.canDelete))
           status(parentVM.status());
        if (!_noBusyLoading) vm.showProcessing('Pesquisando informações...');
        return dataContext.getTcsUsuarioAutenticacaoByEntitySearchNoAssociations(lastJEntitySearch, 0, pageSize(), (pageSize() > 0), preserveDataCurrentState(), status() !== 'E', sortInfo, querySucceeded, complete);
    
        function complete() {
            isProcessing = false;
            if (!_noBusyLoading) vm.closeProcessing();
            if (hasError === true && lookupInitializing === true && isLookup() && (parentVM != null)) {
               parentVM.UI_Close_Click();
            }
            else if (hasError === true) {
               clear();
            }
        }
    
        function querySucceeded(data) {
            if (vm.status() !== 'E') { for (var idx = 0; idx < data.results.length; idx++) { dataContext.initializePOCO(data.results[idx], 'TcsUsuarioAutenticacao'); } }
            hasError = false;
            var usedBlockedFilter = (pendingBlockedFilter !== null);
            var finishWithResults = function (results) {
            dataView(results);
            if (dataView().length === 0 && (parentVM == null || (parentVM != null && uiSettings != null && isNullOrEmpty(uiSettings.parentSelectorDataName)) || isLookup())) {
                if (isLookup() && (parentVM != null) && lookupInitializing === true) {
                   uiSettings.ownerReference.clearLookUp(uiSettings.lookupName);
                   app.showMessage('A informação de Lookup [' + uiSettings.ownerReference.getDisplayName(uiSettings.fieldToSearch) + '] não foi encontrada!', 'Informação', ['Ok']);
                   parentVM.UI_Close_Click();
                   return;
                }
                else  {
                   if (!noMessages) { app.showMessage('Nenhum registro foi encontrado!', 'Informação', ['Ok']); }
                   refreshToolbar();
                }
                if (restoreLastFilter()) {
                   pageCount(1);
                   totalItemCount(1);
                   currentPage(0);
                   status('C');
                   goToIndex(0);
                   dataBind();
                   isBusy(false);
                }
                else {
                   clear();
                }
                return true;
            }
            pageCount( (pageSize() > 0 ? Math.ceil(((!usedBlockedFilter && data.inlineCount) ? data.inlineCount : dataView().length) / pageSize()) : 1) );
            totalItemCount((!usedBlockedFilter && data.inlineCount) ? data.inlineCount : dataView().length);
            currentPage(0);
            if (!(isChildVM() && (uiSettings.canAddNew || uiSettings.canEdit || uiSettings.canDelete)))
               status('Q');
            clearInnerUIs();
            goToIndex(0, noDetails);
            if (isLookup() && (parentVM != null) && (dataView().length === 1) && lookupInitializing === true) {
               if (uiSettings.lookupInfo.isMultiSelection === true && (typeof currentDataItem().IsSelected === 'function')) currentDataItem().IsSelected(true);
               parentVM.UI_selectOption('Ok');
               return;
            }
            if (lookupInitializing === true && uiSettings.modalForm && (typeof uiSettings.modalForm.hide === 'function')) uiSettings.modalForm.hide(false);
            dataBind((isChildVM() ? '' : 'dataView'));
            if (common.getGridMode() == 'G' && !vm.navigationByPage() && (viewType() === 'Main') && !isChildVM() && dataView().length > 1 && (parentVM == null))
                dataToolbar.viewInfo();
            if (typeof externalQueryCallBack === 'function') externalQueryCallBack();
            };
            applyPendingBlockedFilter(data.results, finishWithResults);
        }
    };
    function goToIndex(index, noDetails) {
        if (dataView().length === 0) { currentDataIndex(0); currentDataItem(null); return true; }
        if (index < 0) { index = 0; }
        else if (index >= dataView().length) { index = dataView().length - 1; }
        currentDataIndex(index);
        var oldValue = currentDataItem();
        currentDataItem(dataView()[index]);
        if (status() !== 'C' && currentDataItem() !== null && oldValue !== currentDataItem()) {
           if (!noDetails) currentDataItem().fillDetails();
           OnNavigated(index);
        }
        resizeToolbar();
    }
    function goToItem(item) {
            goToIndex(dataView().indexOf(item));
    }
    function goToKey(primaryKey, value, currentElement, viewSource) {
        if (!viewSource) viewSource = dataView;
        var dataFiltered = viewSource().filter(function (item) { return getAbsoluteValue(item[primaryKey]) == value; });
        if (dataFiltered.length > 0) {
            if (currentElement && currentElement()) {
                currentElement().commitDetailsVisualPendings();
                currentElement(dataFiltered[0]);
                currentElement().fillDetails();
            } else {
                if (currentDataItem()) {
                    currentDataItem().commitDetailsVisualPendings();
                }
                goToIndex(viewSource.indexOf(dataFiltered[0]));
            }
        }
    }
    var sortData = function (sortDef) {
        if (status() === 'Q' && pageCount() > 1 && sortInfo != sortDef) {
           sortInfo = sortDef;
           refresh(0, false);
        }
    };
    var refresh = function (curPage, goLast) {
        vm.showProcessing('Pesquisando informações...');
        return dataContext.getTcsUsuarioAutenticacaoByEntitySearchNoAssociations(lastJEntitySearch, curPage * pageSize(), pageSize(), false, false, status() !== 'E', sortInfo, querySucceeded, complete);
    
        function complete() {
            vm.closeProcessing();
        }
    
        function querySucceeded(data) {
            if (vm.status() !== 'E') { for (var idx = 0; idx < data.results.length; idx++) { dataContext.initializePOCO(data.results[idx], 'TcsUsuarioAutenticacao'); } }
            dataView(data.results);
            currentPage(curPage);
            goToIndex((goLast ? dataView().length : 0));
            dataBind('dataView');
        }
    };
    //#region Client Events
    var OnCleared = function () {
    var control = $lx(vm, '#gbUserPasswordGroupBox');
if(control.length){
	
      var control = $lx(vm, '#gbUserPasswordGroupBox')
      if (control.length){

      if (control.hasClass('gbox'))
      control.removeClass("hide");
      var param = false;
      changeValuesItems(vm.layoutDesigner());
      function changeValuesItems(item) {
      if (item.Items) item.Items.forEach(function (item) {
      if (item.Name == control[0].id) {
      vm.flattenLayout()[item.Name].Visible = param;
      hasChange = true;
      }
      else if (item.Items) {
      return changeValuesItems(item);
      }
      });
      }
      if (hasChange) {
      vm.flattenLayout(vm.flattenLayout());
      }
      }
      else if (managerAuth.shellMode == 'DEV')
      console.warn("Controle(gbUserPasswordGroupBox) não encontrado, verifique o nome do mesmo.");

    ;
}

ctrl.removeCustomEnable(vm, 'CadastroUsuarioAutenticacao_lUpNomeEmpresa')
    }
    var OnSaved = function (changes) {
    
      var control = $lx(vm, '#gbUserPasswordGroupBox')
      if (control.length){

      if (control.hasClass('gbox'))
      control.removeClass("hide");
      var param = false;
      changeValuesItems(vm.layoutDesigner());
      function changeValuesItems(item) {
      if (item.Items) item.Items.forEach(function (item) {
      if (item.Name == control[0].id) {
      vm.flattenLayout()[item.Name].Visible = param;
      hasChange = true;
      }
      else if (item.Items) {
      return changeValuesItems(item);
      }
      });
      }
      if (hasChange) {
      vm.flattenLayout(vm.flattenLayout());
      }
      }
      else if (managerAuth.shellMode == 'DEV')
      console.warn("Controle(gbUserPasswordGroupBox) não encontrado, verifique o nome do mesmo.");

    ;
UpdateMask();
    }
    var OnCancelled = function () {
    
      var control = $lx(vm, '#gbUserPasswordGroupBox')
      if (control.length){

      if (control.hasClass('gbox'))
      control.removeClass("hide");
      var param = false;
      changeValuesItems(vm.layoutDesigner());
      function changeValuesItems(item) {
      if (item.Items) item.Items.forEach(function (item) {
      if (item.Name == control[0].id) {
      vm.flattenLayout()[item.Name].Visible = param;
      hasChange = true;
      }
      else if (item.Items) {
      return changeValuesItems(item);
      }
      });
      }
      if (hasChange) {
      vm.flattenLayout(vm.flattenLayout());
      }
      }
      else if (managerAuth.shellMode == 'DEV')
      console.warn("Controle(gbUserPasswordGroupBox) não encontrado, verifique o nome do mesmo.");

    ;
UpdateMask();
    }
    var OnNavigated = function () {
    UpdateMask();
    }
    var UpdateMask = function () {
    var control = $lx(vm, '#mskCnpjCpf');
if(control.length){
	var entity = vm.currentDataItem();
	control.igMaskEditor('option', 'inputMask', getAbsoluteValue(entity.LxPfjFisicaJuridica) === 1 ? '###.###.###-##' : '##.###.###/####-##');
	control.igMaskEditor('option', 'value', getAbsoluteValue(entity.CnpjCpf));
}
    }
    var OnEdited = function () {
    UpdateMask();
    }
    var ValidaCnpjCpf = function () {
    var entity = vm.currentDataItem();

if (isNullOrEmpty(getAbsoluteValue(entity.CnpjCpf))){
	return true;
}

if (getAbsoluteValue(entity.LxPfjFisicaJuridica) === 1) {
	var cpf = getAbsoluteValue(entity.CnpjCpf);
	var numeros, digitos, soma, i, resultado, digitos_iguais;
	digitos_iguais = 1;
    if (cpf.length < 11)
		return false;
		
	for (i = 0; i < cpf.length - 1; i++)
		if (cpf.charAt(i) != cpf.charAt(i + 1)) {
        digitos_iguais = 0;
        break;
        }
        if (!digitos_iguais) {
			numeros = cpf.substring(0, 9);
            digitos = cpf.substring(9);
            soma = 0;
            for (i = 10; i > 1; i--)
				soma += numeros.charAt(10 - i) * i;
                resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
                if (resultado != digitos.charAt(0))
					return false;
					
                numeros = cpf.substring(0, 10);
                soma = 0;
                for (i = 11; i > 1; i--)
					soma += numeros.charAt(11 - i) * i;
                    resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
                    if (resultado != digitos.charAt(1))
                        return false;
                    return true;
                }
                else {
                    return false;
                }
            }
else {
	var cnpj = getAbsoluteValue(entity.CnpjCpf);
    cnpj = cnpj.replace('.','');
    cnpj = cnpj.replace('.','');
    cnpj = cnpj.replace('.','');
    cnpj = cnpj.replace('-','');
    cnpj = cnpj.replace('/','');
    var numeros, digitos, soma, i, resultado, pos, tamanho, digitos_iguais;
    digitos_iguais = 1;
    if (cnpj.length < 14 && cnpj.length < 15)
        return false;
    for (i = 0; i < cnpj.length - 1; i++)
        if (cnpj.charAt(i) != cnpj.charAt(i + 1))
    {
        digitos_iguais = 0;
        break;
    }
    if (!digitos_iguais)
    {
        tamanho = cnpj.length - 2
        numeros = cnpj.substring(0,tamanho);
        digitos = cnpj.substring(tamanho);
        soma = 0;
        pos = tamanho - 7;
        for (i = tamanho; i >= 1; i--)
        {
            soma += numeros.charAt(tamanho - i) * pos--;
            if (pos < 2)
                pos = 9;
        }
        resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
        if (resultado != digitos.charAt(0))
            return false;
        tamanho = tamanho + 1;
        numeros = cnpj.substring(0,tamanho);
        soma = 0;
        pos = tamanho - 7;
        for (i = tamanho; i >= 1; i--)
        {
            soma += numeros.charAt(tamanho - i) * pos--;
            if (pos < 2)
                pos = 9;
        }
        resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
        if (resultado != digitos.charAt(1))
            return false;
        return true;
    }
    else
        return false;
}

    }
    var OnSaving = function (changes) {
    if (!isNullOrEmpty(vm.currentDataItem())){
	if (!ValidaCnpjCpf()){
		var msg = (getAbsoluteValue(vm.currentDataItem().LxPfjFisicaJuridica) === 1) ? "O CPF informado não é válido." : "O CNPJ informado não é válido.";
		app.showMessage((msg).toString(), 'Alerta', ['Ok']);
		return false;
	}


	//Acesso padrão
	if (!isNullOrEmpty(vm.currentDataItem().TcsUsuarioAcessoList())){
		var items = $.grep(vm.currentDataItem().TcsUsuarioAcessoList(), function (element, index) { return getAbsoluteValue(element.IndicaAcessoPadrao) });
		if (items.count() > 1){
			app.showMessage(("Só é permitido um acesso padrão por Usuário.").toString(), 'Alerta', ['Ok']);
			return false;
		}
	}
}
return true;

return true;
    }
    var OnDataGridCreated = function (dataGridName) {
    var control = $lx(vm, '#' + dataGridName);

if (dataGridName !== "CadastroUsuarioAutenticacao_dGridTcsUsuarioAcesso" || !control.length || !control.data('igGrid')){
	return;
}

	$(document).delegate("#CadastroUsuarioAutenticacao_dGridTcsUsuarioAcesso", "iggridupdatingeditcellstarting", function (evt, ui) { 
    if (status() === "C" || ui.columnKey !== "DescricaoAmbienteRelacionado"){
		return true;
	}
	
	return getAbsoluteValue(currentDataItem().currentTcsUsuarioAcesso().IdTcsAplicativo) === 3;
	
});

    }
    var OnToolbarAction = function (action) {
    if (action === 'Undo' || action === 'Add'){
	ctrl.removeCustomEnable(vm, 'CadastroUsuarioAutenticacao_lUpNomeEmpresa')
}
return true;
    }
    var OnLoaded = function () {
    vm.showProcessing('Verificando parâmetro.');

$.ajax({
			type: 'GET',
            message: "Verificando parâmetro.",
            messageUser: "Verificando parâmetro.",
            globalError: false,
			headers: managerAuth.getHeaders(),
            url: managerAuth.getServiceAddress('LinxFrameworkParametro') + '/GetParameterValue?serializedParameterList=PERMITE_MULTI_GPECON_USUARIO{}' ,
            dataType: 'json',
            cache: false,
            error: function (jqXHR, textStatus, errorThrown) {
						
      {
      if('TcsUsuarioGpeconTabItem'.indexOf('ti') > -1)
      var controlTab = $lx(vm, '#TcsUsuarioGpeconTabItem');
      else
      var controlTab = $lx(vm, '#tiTcsUsuarioGpeconTabItem');

      if(controlTab.length){
      if (true) {
      controlTab.show();
      }
      else {
      controlTab.hide();
      controlTab.removeClass('active');
      controlTab.removeClass('active in')
      }
      }
      else if (managerAuth.shellMode == 'DEV')
      console.warn("Controle(TcsUsuarioGpeconTabItem) não encontrado, verifique o nome do mesmo.");
      }
    
						vm.closeProcessing();
                    },
			success: function (data) {
                        var parameters = data.split('|');
						if (parameters.count() == 2 && parameters[1].toLowerCase() == 'true'){
							
      {
      if('TcsUsuarioGpeconTabItem'.indexOf('ti') > -1)
      var controlTab = $lx(vm, '#TcsUsuarioGpeconTabItem');
      else
      var controlTab = $lx(vm, '#tiTcsUsuarioGpeconTabItem');

      if(controlTab.length){
      if (true) {
      controlTab.show();
      }
      else {
      controlTab.hide();
      controlTab.removeClass('active');
      controlTab.removeClass('active in')
      }
      }
      else if (managerAuth.shellMode == 'DEV')
      console.warn("Controle(TcsUsuarioGpeconTabItem) não encontrado, verifique o nome do mesmo.");
      }
    
						}
						else {
							
      {
      if('TcsUsuarioGpeconTabItem'.indexOf('ti') > -1)
      var controlTab = $lx(vm, '#TcsUsuarioGpeconTabItem');
      else
      var controlTab = $lx(vm, '#tiTcsUsuarioGpeconTabItem');

      if(controlTab.length){
      if (false) {
      controlTab.show();
      }
      else {
      controlTab.hide();
      controlTab.removeClass('active');
      controlTab.removeClass('active in')
      }
      }
      else if (managerAuth.shellMode == 'DEV')
      console.warn("Controle(TcsUsuarioGpeconTabItem) não encontrado, verifique o nome do mesmo.");
      }
    
						}
						
						vm.closeProcessing();
                    }
                });

    }
    //#endregion Client Events
    var clearByUser = function () {
        if (!isNullOrEmpty(customSearchResult.searchDefinition)) {
            app.showMessage('Deseja limpar a pesquisa avançada?', 'Alerta', ['Yes', 'No'])
            .then(function (selectedOption) {
                if (selectedOption === 'Yes') {
                    customSearchResult.searchDefinition = '';
                    customSearchResult.serializedSearch = '';
                    customSearchResult.translatedSearch = '';
                    hasCustomSearches(false);
                }
                return clear();
             });
        }
        else return clear();
    }
    var clear = function (noBindingReport) {
        if (uiSettings && parentVM && uiSettings.noSearch === true && parentVM.status() !== 'C') return;
        vm.canReportErrors = false;
        if (!OnToolbarAction('Clear')) return;
        parentEntityRelated = null;
        isBusy(true);
        lastStatus = status();
        status('C');
        if (restoreLastFilter(lastStatus === 'C')) return clearComplete({ results: dataView() }, true);
        else return dataContext.clearTcsUsuarioAutenticacao(getBandeiraRede(), clearComplete);
    
        function clearComplete(data, holdRanges) {
            dataForUndo = [];
            dataView(data.results);
            if (holdRanges != true) vm.entitySearchRange.clear();
            if (typeof noBindingReport === 'boolean' && noBindingReport === true) { pageCount(1); currentPage(0); goToIndex(0); return; }
            pageCount(1);
            totalItemCount(data.results.length);
            lastStatus = 'C';
            currentPage(0);
            goToIndex(0);
            adjustFormView();
            dataBind();
            isBusy(false);
            hideButtonsEditorTemplate();
            clearInnerUIs();
            OnCleared();
            scrollMainTop();
        }
    };
    var hasChanges = ko.computed(function () {
            return dataContext.hasChanges();
    });
    var hasInternalUIsValidationErrors = function () {
        for (var idx = 0; idx < vm.internalUIs.length; idx++) { var innerVM = vm[vm.internalUIs[idx]](); if (innerVM.status() === 'E' && innerVM.hasValidationErrors()) return true; }
        return false;
    };
    var hasInternalUIsSavingErrors = function () {
        for (var idx = 0; idx < vm.internalUIs.length; idx++) { var innerVM = vm[vm.internalUIs[idx]](); if (innerVM.status() === 'E' && !innerVM.onSavingValidation()) return true; }
        return false;
    };
    var commitInternalUIsData = function () {
        for (var idx = 0; idx < vm.internalUIs.length; idx++) { var innerVM = vm[vm.internalUIs[idx]](); innerVM.dataBind('', true); }
    };
    var onSavingValidation = function (changes) {
        if (!changes) changes = getAllChanges();
        if (changes.length === 0) { if (vm.internalUIs.length === 0) { undo(changes) }; return true; }
        if (!OnSaving(changes)) { return false; }
        for (var idxChange = 0; idxChange < changes.length; idxChange++) {
            var entity = changes[idxChange];
            if (typeof entity.OnSaving == 'function') {
               if (!entity.OnSaving()) { return false; }
            }
        }
        return true;
    }
    var hasValidationErrors = function () {
       vm.canReportErrors = true;
       return dataContext.hasValidationErrors();
    }
    var save = function (isExclusion, externalSaveSucceeded, transactionId, saveCompleteCallback, internalUiCallback) {
        if (typeof isExclusion !== 'boolean') isExclusion = false;
        if (isExclusion) { enableDataTrack(false, false); }
        if (!isExclusion && !OnToolbarAction('Save')) return;
        var indexForUndoAction = currentDataIndex();
        if (isExclusion) { removeItem(); }
        commitInternalUIsData();
        dataBind('', true);
        vm.changes = getAllChanges();
        if (!onSavingValidation(vm.changes)) { if (isExclusion) return undo(indexForUndoAction); else return; }
        if (hasInternalUIsSavingErrors()) { if (isExclusion) return undo(indexForUndoAction); else return; }
        if (hasInternalUIsValidationErrors() || hasValidationErrors()) { if (isExclusion) return undo(indexForUndoAction); else { refreshToolbar(); return dataBind(); } }
        isSaving(true);
        if (!isExclusion && currentDataItem() && currentDataItem().checkForSendingAllRowsToServer) { currentDataItem().checkForSendingAllRowsToServer(); }
        vm.showProcessing('Salvando informações...');
        return dataContext.saveChanges(saveSucceeded, saveFailed, complete, false);
    
        function complete() {
            vm.canReportErrors = false;
            vm.closeProcessing();
            isSaving(false);
        }
    
        function saveFailed(error) {
            if (isChildVM()) parentVM.dataToolbar.edit(true);
            if (isExclusion) return undo(indexForUndoAction); else return dataBind();
        }
    };
    
        function saveSucceeded(saveResult) {
            dataForUndo = [];
            if (dataView().length === 0 && !isChildVM()) return clear();
            if (dataView().length > 0) goToIndex(currentDataIndex());
            for (var idxChange = 0; idxChange < vm.changes.length; idxChange++) {
                var entity = vm.changes[idxChange];
                if (entity.isUnchanged() && !isNullOrEmpty(getAbsoluteValue(entity.TableMedia))) { setAbsoluteValue(entity, 'TableMedia', null); entity.setUnchanged(); }
                if (typeof entity.OnSaved == 'function') {
                   entity.OnSaved();
                }
            }
            //if (isChildVM())
            //{
            //   dataContext.clearAll();
            //   query(false, parentEntityRelated);
            //}
            lastStatus = 'Q';
            status('Q');
            refreshToolbar();
            OnSaved(vm.changes);
            if (typeof externalSaveSucceeded == 'function') {
                externalSaveSucceeded();
            }
            dataBind();
            resizeToolbar();
        }
    var dataForUndo = [];
    var undo = function (indexForUndoAction) {
        vm.canReportErrors = false;
        if (!OnToolbarAction('Undo')) return;
        dataContext.cancelChanges();
        if ((typeof indexForUndoAction) === 'number' && !navigationByPage() && !isChildVM()) lastStatus = 'Q';
        if (lastStatus === 'C' || dataForUndo.length == 0) {
            clear();
        } else {
            dataView(dataForUndo);
            dataForUndo = [];
            hideButtonsEditorTemplate();
            status(lastStatus);
            goToIndex(((typeof indexForUndoAction) === 'number' ? indexForUndoAction : currentDataIndex()));
            dataBind();
            undoInnerUIs();
            OnCancelled();
        }
    };
    var hideButtonsEditorTemplate = function () {
       if ($('.addReg').is(':visible')) {
           $('.addReg :visible').each(function (index) {
               $('.addReg').hide();
               $('.delReg').hide();
           });
       }
    };
    var showButtonsEditorTemplate = function () {
       if ($('.toolbar-dialog-template').is(':visible')) {
           $('.toolbar-dialog-template :visible').parent().find('button.addReg').show();
           $('.toolbar-dialog-template :visible').parent().find('button.delReg').show();
       }
    };
    var print = function () {
        if (!OnToolbarAction('Report')) return;
        return true;
    };
    var helper = function () {
        linxHelper(vm.status(), vm.viewName, vm.rootDataTypeName, '"MODAprod","Moda"');
    };
    var acceptChanges = function () {
        if (!navigationByPage() && !isChildVM()) dataContext.acceptChanges();
    };
    var edit = function (noClearInnerUIs) {
        if (status() === 'E') { refreshToolbar(); return; }
        if (!OnToolbarAction('Edit')) return;
        if (!canAddChangeEntity()) return;
        acceptChanges();
        lastStatus = status();
        status('E');
        if (!noClearInnerUIs) clearInnerUIs();
        goToIndex(currentDataIndex());
        if (lastStatus === 'Q') dataForUndo = [].concat(dataView());
        //Enabling data track
        enableDataTrack(navigationByPage() || isChildVM(), true);
        OnEdited();
        editInnerUIs();
        showButtonsEditorTemplate();
    };
    var enableDataTrack = function (all, convertDetails) {
        adjustFormView();
        if (!all) {
           if (!isNullOrEmpty(currentDataItem()) && currentDataItem().isPOCO) {
               dataView()[currentDataIndex()] = dataContext.createEntity(currentDataItem().typeName, currentDataItem().getPrimitiveDTO(), true);
               if (convertDetails) { currentDataItem().enableDetailsDataTack(dataView()[currentDataIndex()]); }
           }
        } else {
           for (var idx = 0; idx < dataView().length; idx++) {
               var entity = dataView()[idx];
               if (entity.isPOCO)  {
                   dataView()[idx] = dataContext.createEntity(entity.typeName, entity.getPrimitiveDTO(), true);
                   if (convertDetails) entity.enableDetailsDataTack(dataView()[idx]);
               }
           }
        }
        if (dataView().length > 0) currentDataItem(dataView()[currentDataIndex()]);
        dataBind();
    };
    var setBandeiraRede = function () {
    };
    
    var createTcsUsuarioAutenticacao = function() {
        dataBind('dataView', true);
        var entity = dataContext.createTcsUsuarioAutenticacao();
        if(!entity) return null;
        adjustExternalParentRelation(entity);
        entity.setBandeiraRede(getBandeiraRede());
        entity.setGpecon(getGpecon());
        dataView.push(entity);
        if (typeof entity.OnAdded == 'function') {
            entity.OnAdded();
        }
        return entity;
    };
    
    var createAndNotifyTcsUsuarioAutenticacao = function() {
        var entity = createTcsUsuarioAutenticacao();
        notifyPresentation('');
        return entity;
    };
    
    var createTcsUsuarioAcesso = function(parent, noCurrent) {
        dataBind('TcsUsuarioAcessoList', true);
        var entity = dataContext.createTcsUsuarioAcesso(parent, noCurrent);
        if(!entity) return null;
        entity.setBandeiraRede(getBandeiraRede());
        entity.setGpecon(getGpecon());
        if (typeof entity.OnAdded == 'function') {
            entity.OnAdded();
        }
       if ((noCurrent !== true) && !isNullOrEmpty(parent)) { parent.currentTcsUsuarioAcesso(entity); entity.fillDetails(); } 
        return entity;
    };
    
    var createAndNotifyTcsUsuarioAcesso = function(parent) {
        var entity = createTcsUsuarioAcesso(parent);
        notifyPresentation('TcsUsuarioAcessoList');
        return entity;
    };
    
    var createTcsIdentidadeExterna = function(parent, noCurrent) {
        dataBind('TcsIdentidadeExternaList', true);
        var entity = dataContext.createTcsIdentidadeExterna(parent, noCurrent);
        if(!entity) return null;
        entity.setBandeiraRede(getBandeiraRede());
        entity.setGpecon(getGpecon());
       if ((noCurrent !== true) && !isNullOrEmpty(parent)) { parent.currentTcsIdentidadeExterna(entity); entity.fillDetails(); } 
        return entity;
    };
    
    var createAndNotifyTcsIdentidadeExterna = function(parent) {
        var entity = createTcsIdentidadeExterna(parent);
        notifyPresentation('TcsIdentidadeExternaList');
        return entity;
    };
    
    var createTcsUsuarioGpecon = function(parent, noCurrent) {
        dataBind('TcsUsuarioGpeconList', true);
        var entity = dataContext.createTcsUsuarioGpecon(parent, noCurrent);
        if(!entity) return null;
        entity.setBandeiraRede(getBandeiraRede());
        entity.setGpecon(getGpecon());
        if (typeof entity.OnAdded == 'function') {
            entity.OnAdded();
        }
       if ((noCurrent !== true) && !isNullOrEmpty(parent)) { parent.currentTcsUsuarioGpecon(entity); entity.fillDetails(); } 
        return entity;
    };
    
    var createAndNotifyTcsUsuarioGpecon = function(parent) {
        var entity = createTcsUsuarioGpecon(parent);
        notifyPresentation('TcsUsuarioGpeconList');
        return entity;
    };
    var notifyPresentation = function(dataSourceName) {
          return dataContext.notifyPresentation(dataSourceName);
    };
    
    var notifyInnerElements = function (element, isExpander) {
        if (element)
        {
            dataBind('', true);
            try{ $(window).trigger('resize'); } catch(e){ console.log(e); }
            var innerElements = element.find("table");
            if (innerElements.length > 0 && (vm.dataSource.length > 0 || vm.internalUIs.length > 0)) {
                for (var idx = 0; idx < innerElements.length; idx++) {
                    if($(innerElements[idx]).parents('.tab-pane').hasClass('active') || isExpander) {
                        for (var db in vm.dataSource) { if (vm.dataSource[db].key == innerElements[idx].id) vm.dataSource[db].itemsSource.dataBind(false, true); }
                        //Notifying inner UIs
                        for (var idxUI = 0; idxUI < vm.internalUIs.length; idxUI++) {
                           var innerVM = vm[vm.internalUIs[idxUI]]();
                           for (var db in innerVM.dataSource) {
                               if (innerVM.dataSource[db].key == innerElements[idx].id)
                                   innerVM.dataSource[db].itemsSource.dataBind(false, true);
                           }
                        }
                    }
                }
            }
        }
    };
    var createEntity = function(entityName, initialValues) {
        var entity = dataContext.createEntity(entityName, initialValues);
        entity.setBandeiraRede(getBandeiraRede());
        entity.setGpecon(getGpecon());
        return entity;
    };
    var getBandeiraRede = function() {
        if (uiSettings != null && uiSettings.lookupInfo && uiSettings.lookupInfo.vm && (typeof uiSettings.lookupInfo.vm.getBandeiraRede === 'function')) return uiSettings.lookupInfo.vm.getBandeiraRede();
        else if (parentVM != null && (typeof parentVM.getBandeiraRede === 'function')) return parentVM.getBandeiraRede();
        else if (uiSettings != null && uiSettings.parentUI && uiSettings.parentUI.vm && (typeof uiSettings.parentUI.vm.getBandeiraRede === 'function')) return uiSettings.parentUI.vm.getBandeiraRede();
        else if (!isNullOrEmpty(vm.currentBrands()) && vm.currentBrands().indexOf(',') === -1) return parseInt(vm.currentBrands());
        else return 0;
    };
    var getCurrentBrands = function() {
        if (uiSettings != null && uiSettings.lookupInfo && uiSettings.lookupInfo.vm  && uiSettings.lookupInfo.vm.hasBrand && (typeof uiSettings.lookupInfo.vm.getCurrentBrands === 'function')) return uiSettings.lookupInfo.vm.getCurrentBrands();
        else if (parentVM != null && parentVM.hasBrand && (typeof parentVM.getCurrentBrands === 'function')) return parentVM.getCurrentBrands();
        else if (uiSettings != null && uiSettings.parentUI && uiSettings.parentUI.vm  && uiSettings.parentUI.vm.hasBrand && (typeof uiSettings.parentUI.vm.getCurrentBrands === 'function')) return uiSettings.parentUI.vm.getCurrentBrands();
        else return (isNullOrEmpty(vm.currentBrands()) ? '0' : vm.currentBrands());
    };
    var showProcessing = function(message) {
        currentActivityInformation(message);
        isBusy(true);
    };
    var closeProcessing = function() {
        currentActivityInformation('');
        isBusy(false);
    };
    var getGpecon = function() {
        if (!isNullOrEmpty(managerAuth.loginInfo.IdLinxGrupoEconomico)) return parseInt(managerAuth.loginInfo.IdLinxGrupoEconomico);
        else return 0;
    };
    var deleteEntity = function (entity, isMultiSelection) {
        var selectedEntities = []
        if (isMultiSelection && !isNullOrEmpty(complement) && (typeof complement.selectedItems === 'function'))
            selectedEntities = complement.selectedCurrentItems(false, true);
        if (selectedEntities.length > 0) {
           for (var idx = 0; idx < selectedEntities.length; idx++) {
               var selectedEntity = selectedEntities[idx];
               if (typeof selectedEntity.OnDeleting == 'function') {
                   if (!selectedEntity.OnDeleting()) return false;
               }
               removeInnerDataUIs(selectedEntity);
               dataContext.deleteEntity(selectedEntity);
               if (selectedEntity.typeName == vm.rootDataTypeName) {
                   dataView.remove(selectedEntity);
               }
               if (typeof selectedEntity.OnDeleted == 'function') {
                   selectedEntity.OnDeleted();
               }
           }
           if (typeof complement.clearSelectedItems === 'function') complement.clearSelectedItems();
               return true;
        }
        else {
           if (typeof entity.OnDeleting == 'function') {
               if (!entity.OnDeleting()) return false;
           }
           removeInnerDataUIs(entity);
           dataContext.deleteEntity(entity);
           if (typeof entity.OnDeleted == 'function') {
               entity.OnDeleted();
           }
        }
        return true;
    };
    var canAddChangeEntity = function () {
       return true;
    };
    var addNew = function (parentEntity) {
        if (!dataContext.dataParameters.isLoaded) {
           setTimeout(function () {
               addNew(parentEntity);
           }, 1000);
           return;
        }
        if (status() === 'Q' && !navigationByPage() && !isChildVM()) clear();
        if (parentEntity != null && (typeof parentEntity === 'object') && !isNullOrEmpty(parentEntity.typeName))
           parentEntityRelated = parentEntity;
        if (!OnToolbarAction('Add')) return;
        if (!canAddChangeEntity()) return;
        acceptChanges();
        if (status() === 'C') {
            dataContext.clearAll();
            dataView([]);
        }
        if (status() === 'Q') {
           adjustFormView();
           dataForUndo = [].concat(dataView());
           if (navigationByPage()) enableDataTrack(true, true);
        }
        if (status() !== 'E') {
            lastStatus = status();
            status('E');
        }
        goToItem(createTcsUsuarioAutenticacao());
        editInnerUIs();
        showButtonsEditorTemplate();
        dataBind();
    };
    var remove = function () {
        if (!OnToolbarAction('Delete')) return;
        acceptChanges();
        app.showMessage('Deseja realmente excluir o registro selecionado?', 'Alerta', ['Yes', 'No'])
            .then(function (selectedOption) {
                if (selectedOption === 'Yes') {
                    if (!navigationByPage() && !isChildVM()) { dataForUndo = [].concat(dataView()); save(true); } else { removeItem(); }
                }
                return selectedOption;
             });
    };
    var removeParentRelatedItems = function (parentEntity) {
        var removedIdx = []
        for (var idx = 0; idx < dataView().length; idx++) {
           var isRelated = true;
           if (uiSettings != null && uiSettings.parentFieldsRelation.length == uiSettings.detailFieldsRelation.length) {
               for (var j = 0; j < uiSettings.parentFieldsRelation.length; j++) {
                   if (getAbsoluteValue(dataView()[idx][uiSettings.detailFieldsRelation[j]]) !== getAbsoluteValue(parentEntity[uiSettings.parentFieldsRelation[j]])) {
                       isRelated = false;
                   }
               }
           }
           if (isRelated) {
               deleteEntity(dataView()[idx]);
               removedIdx.push(idx);
           }
        }
        for (var i = removedIdx.length - 1; i >= 0; i--) {
           dataView().splice(removedIdx[i], 1);
        }
        goToIndex(0);
        dataBind();
    }
    var removeItem = function () {
        if (deleteEntity(currentDataItem()) === false) return false;
        var index = dataView.indexOf(currentDataItem());
        dataView.remove(currentDataItem());
        if (dataView().length > 0) {
            if (status() !== 'E') {
                lastStatus = status();
                status('E');
            }
            if (index > 0) { goToIndex(index-1); }
            else { goToIndex(0); }
            dataBind();
        }
        else {
            goToIndex(0);
            dataBind();
        }
    };
    var goFirst = function () {
        if (!OnToolbarAction('First')) return;
        var item;
        if (navigationByPage() || (viewType() === 'Secundary') || (!(pageCount() === 1 || pageSize() === 0 || currentPage() === 0))) {
            item = refresh(0, false);
        } else {
            item = goToIndex(0);
        }
        return item;
    };
    var goBack = function () {
        if (!OnToolbarAction('Back')) return;
        var item;
        if (navigationByPage() || (viewType() === 'Secundary') || (!(pageCount() === 1 || pageSize() === 0 || currentPage() === 0) && currentDataIndex() === 0)) {
            item = refresh(currentPage()-1, !navigationByPage());
        } else {
            item = goToIndex(currentDataIndex()-1);
        }
        return item;
    };
    var goForward = function () {
        if (!OnToolbarAction('Next')) return;
        var item;
        if (navigationByPage() || (viewType() === 'Secundary') || (!(pageCount() === 1 || pageSize() === 0 || currentPage() === (pageCount()-1)) && currentDataIndex() === (dataView().length-1))) {
            item = refresh(currentPage()+1, false);
        } else {
            item = goToIndex(currentDataIndex()+1);
        }
        return item;
    };
    var goLast = function() {
        if (!OnToolbarAction('Last')) return;
        var item;
        if (!navigationByPage() && (viewType() === 'Main') && (pageCount() === 1 || pageSize() === 0 || currentPage() === (pageCount()-1))) {
            item = goToIndex(dataView().length-1);
        } else {
            item = refresh(pageCount()-1, !navigationByPage() && (viewType() === 'Main'));
        }
        return item;
    };
    //Databar enable control
    var _canRefreshData = true, _canQuickSearch = true, _canAddNew = true, _canClear = true, _canCustomSearch = true, _canDelete = true, _canEdit = true, _canLayout = true, _canNavigate = true, _canPrint = true, _canSearch = true, _canExport = true, _noBusyLoading = false;
    var setSecurity = function(pCanAddNew, pCanClear, pCanCustomSearch, pCanDelete, pCanEdit, pCanLayout, pCanNavigate, pCanPrint, pCanSearch, pCanExport, pNoBusyLoading) {
       _canAddNew = pCanAddNew;
       _canClear = pCanClear;
       _canCustomSearch = pCanCustomSearch;
       _canDelete = pCanDelete;
       _canEdit = pCanEdit;
       _canLayout = pCanLayout;
       _canNavigate = pCanNavigate;
       _canPrint = pCanPrint;
       _canSearch = pCanSearch;
       _canExport = pCanExport;
       _noBusyLoading = pNoBusyLoading
       refreshToolbar();
    };
    var refreshToolbar = function() {
       status.notifySubscribers();
       currentDataItem.notifySubscribers();
       canNavigate.notifySubscribers();
    }
    var refreshCurrentBind = function() {
       currentDataItem.notifySubscribers();
    }
    var isReportComposition = function (reportName) {
        if (!isNullOrEmpty(reportName))
        {
            for (var idx in dataContext.entityNames)
            {
                if (dataContext.entityNames[idx].indexOf('ParentComposition') > -1 && reportName.indexOf(vm.rootNamespace + '.' + dataContext.entityNames[idx]) > -1)
                    return true;
            }
        }
        return false;
    }
    var canGoFirst = ko.computed(function () { return (status() === 'Q' || (status() === 'E' && isChildVM())) && _canNavigate && ((!navigationByPage() && (viewType() === 'Main') && currentRecord() > 0) || ((navigationByPage() || (viewType() === 'Secundary')) && currentPage() > 0)); });
    var canGoBack = ko.computed(function () { return (status() === 'Q' || (status() === 'E' && isChildVM())) && _canNavigate && ((!navigationByPage() && (viewType() === 'Main') && currentRecord() > 0) || ((navigationByPage() || (viewType() === 'Secundary')) && currentPage() > 0)); });
    var canGoForward = ko.computed(function () { return (status() === 'Q' || (status() === 'E' && isChildVM())) && _canNavigate && ((!navigationByPage() && (viewType() === 'Main') && currentRecord() < (totalRecords()-1)) || ((navigationByPage() || (viewType() === 'Secundary')) && currentPage() < (pageCount()-1))); });
    var canGoLast = ko.computed(function () { return (status() === 'Q' || (status() === 'E' && isChildVM())) && _canNavigate && ((!navigationByPage() && (viewType() === 'Main') && currentRecord() < (totalRecords()-1)) || ((navigationByPage() || (viewType() === 'Secundary')) && currentPage() < (pageCount()-1))); });
    var canClear = ko.computed(function () { return ['C', 'Q'].indexOf(status()) >= 0 && _canClear && !isChildVM(); });
    var canExport = ko.computed(function () { return (status() === 'Q' || status() === 'C') && _canExport; });
    var canGridExport = ko.computed(function () { return status() === 'Q' && _canExport; });
    var canQuery = ko.computed(function () { return status() === 'C' && _canSearch && !isChildVM(); });
    var canCustomSearch = ko.computed(function () { return status() === 'C' && _canCustomSearch && !isChildVM(); });
    var canQuickSearch = ko.computed(function () { return false; });
    var hasDataFeed = ko.computed(function () { return status() === 'C' && _canSearch && dataContext.hasDataFeed && parentVM == null && !isChildVM(); });
    var canAddNew = ko.computed(function () { return ((['Q', 'C'].indexOf(status()) >= 0 && !isChildVM()) || (status() === 'E' && (navigationByPage() || isChildVM()))) && _canAddNew; });
    var canRemove = ko.computed(function () { return (dataView().length > 0) && ((!navigationByPage() && !isChildVM() && status() === 'Q') || (status() === 'E' && !navigationByPage() && isChildVM())) && _canDelete; });
    var canEdit = ko.computed(function () { return status() === 'Q' && _canEdit && !isChildVM(); });
    var canRefreshCurrentData = ko.computed(function () { return status() === 'Q' && _canSearch && _canRefreshData && !isChildVM(); });
    var canUndo = ko.computed(function () { return status() === 'E' && (_canEdit || _canAddNew) && !isChildVM(); });
    var canUnlockUser = ko.computed(function () {
        try {
            if (status() !== 'Q' && status() !== 'C')
                return false;
            var item = currentDataItem();
            if (isNullOrEmpty(item) || isEmptyEntityFn(item))
                return false;
            var loginName = getAbsoluteValue(item.NomeAutenticacao);
            return !isNullOrEmpty(loginName);
        }
        catch (e) {
            return false;
        }
    });
    var ensureBlockedObservable = function (entity) {
        if (isNullOrEmpty(entity))
            return;
        if (typeof entity.Blocked === 'undefined')
            entity.Blocked = ko.observable(false);
        else if (typeof entity.Blocked !== 'function')
            entity.Blocked = ko.observable(!!entity.Blocked);
    };
    var parseMembershipLocked = function (locked) {
        if (locked === true || locked === 1)
            return true;
        if (typeof locked === 'string') {
            var normalized = locked.replace(/"/g, '').trim().toLowerCase();
            return normalized === 'true' || normalized === '1';
        }
        return false;
    };
    var refreshMembershipBlocked = function (item) {
        try {
            if (isNullOrEmpty(item) || isEmptyEntityFn(item))
                return;
            ensureBlockedObservable(item);

            var loginName = getAbsoluteValue(item.NomeAutenticacao);
            if (isNullOrEmpty(loginName)) {
                setAbsoluteValue(item, 'Blocked', false);
                return;
            }

            $.ajax({
                type: 'GET',
                headers: managerAuth.getHeaders(managerAuth.loginInfo.IdTcsAmbienteDefault),
                url: managerAuth.getServiceAddress('LinxFrameworkAutorizacao', 'Linx.Framework.BV') + '/IsMembershipUserLockedOut',
                data: { userName: loginName },
                dataType: 'json',
                async: true,
                cache: false,
                error: function () {
                    // Do not clear a previously known lockout state on transient errors
                },
                success: function (locked) {
                    var current = currentDataItem();
                    if (isNullOrEmpty(current) || getAbsoluteValue(current.NomeAutenticacao) !== loginName)
                        return;
                    ensureBlockedObservable(current);
                    setAbsoluteValue(current, 'Blocked', parseMembershipLocked(locked));
                }
            });
        }
        catch (e) {
            // keep UI responsive even if Membership probe fails
        }
    };
    var unlockUser = function () {
        try {
            var item = currentDataItem();
            if (isNullOrEmpty(item) || isEmptyEntityFn(item)) {
                app.showMessage('Selecione um usuário para desbloquear.', 'Atenção', ['Ok']);
                return;
            }
            var loginName = getAbsoluteValue(item.NomeAutenticacao);
            if (isNullOrEmpty(loginName)) {
                app.showMessage('Usuário de autenticação não informado.', 'Atenção', ['Ok']);
                return;
            }

            ensureBlockedObservable(item);
            if (!getAbsoluteValue(item.Blocked)) {
                app.showMessage('Usuário já desbloqueado.', 'Atenção', ['Ok']);
                return;
            }

            app.showMessage(
                'Deseja desbloquear o usuário "' + loginName + '"?',
                'Desbloquear usuário',
                ['Yes', 'No']
            ).then(function (answer) {
                if (answer !== 'Yes')
                    return;

                dataToolbar.isBusy(true);
                $.ajax({
                    type: 'GET',
                    messageUser: 'Desbloqueio de usuário Membership',
                    headers: managerAuth.getHeaders(managerAuth.loginInfo.IdTcsAmbienteDefault),
                    url: managerAuth.getServiceAddress('LinxFrameworkAutorizacao', 'Linx.Framework.BV') + '/UnlockMembershipUser',
                    data: { userName: loginName },
                    dataType: 'json',
                    async: true,
                    cache: false,
                    error: function (jqXHR) {
                        dataToolbar.isBusy(false);
                        var errorMessage = (jqXHR.responseJSON && (jqXHR.responseJSON.ExceptionMessage || jqXHR.responseJSON.Message)) || jqXHR.statusText || 'Erro ao desbloquear usuário.';
                        app.showMessage(errorMessage, 'Atenção', ['Ok']);
                    },
                    success: function () {
                        dataToolbar.isBusy(false);
                        setAbsoluteValue(item, 'Blocked', false);
                        app.showMessage('Usuário "' + loginName + '" desbloqueado com sucesso.', 'Informação', ['Ok']);
                    }
                });
            });
        }
        catch (e) {
            dataToolbar.isBusy(false);
            app.showMessage(e.message || e, 'Atenção', ['Ok']);
        }
    };
    var canNavigate = ko.computed(function () { return  (!canUndo() && !canQuery() && (dataView().length > 1 || pageCount() > 1) && _canNavigate); });
    var canPrint = ko.computed(function () { return ['C', 'Q'].indexOf(status()) >= 0 && _canPrint && !isChildVM(); });
    var canSave = ko.computed(function () {
           return !isSaving() && status() === 'E' && (_canEdit || _canAddNew) && !isChildVM();
    });
    var enabledForEditing = ko.computed(function () {
            return ['E', 'C'].indexOf(status()) >= 0;
    });
    var isEditable = function () {
        return _canEdit;
    };
    var viewInfo = function () {
        if (!OnToolbarAction('TableView')) return;
        changeFormView();
    };
    var adjustFormView = function () {
        if (!hasMainTopDataGrid() && (status() === 'E' || status() === 'C') && viewType() === 'Secundary') changeFormView();
    }
    var removeFormViewControl = function () {
        var front = $('#CadastroUsuarioAutenticacao_formViewer_front')[0];
        if (front) front.removeClassName('front');
        var back = $('#CadastroUsuarioAutenticacao_formViewer_back')[0];
        if (back) { back.removeClassName('back'); back.addClassName('hide'); }
    }
    var changeFormView = function () {
        if (hasMainTopDataGrid() || isChildVM()) return;
        var panel = $('#CadastroUsuarioAutenticacao_formViewer')[0];
        if (panel) {
           if (viewType() === 'Main') panel.addClassName('flip');
           else panel.removeClassName('flip');
        }
        if (viewType() === 'Main') viewType('Secundary');
        else viewType('Main');
        if (viewType() === 'Secundary') { dataBind('dataView'); } else { dataBind(); queryInnerUIs(currentDataItem()); };
    }
    var canViewInfo = ko.computed(function () {
        return !hasMainTopDataGrid() && status() !== 'E' && totalRecords() > 0 && !isChildVM();
    });
    var importPhoto = function () {
        if (!OnToolbarAction('ImportPhoto')) return;
        require(['viewmodels/shared/modalMultimidiaBatch'], function (modalMultimidiaBatch) {
            modalMultimidiaBatch.showModal(dataContext).then(function (r, data) { });
        });
    };
    
    var entitySearchRange = {
        predefinedFilters: ko.observableArray(managerPredefined.predefinedFilters),
            TcsUsuarioAutenticacaoDataAlteracao_typeRange: ko.observable('R'), TcsUsuarioAutenticacaoDataAlteracao_begin: ko.observable(null), TcsUsuarioAutenticacaoDataAlteracao_end: ko.observable(null), TcsUsuarioAutenticacaoDataAlteracao_predefFilter: ko.observableArray([]), TcsUsuarioAutenticacaoDataAlteracao_predefValue: ko.observable(null),
        TcsUsuarioAutenticacaoDataCadastro_typeRange: ko.observable('R'), TcsUsuarioAutenticacaoDataCadastro_begin: ko.observable(null), TcsUsuarioAutenticacaoDataCadastro_end: ko.observable(null), TcsUsuarioAutenticacaoDataCadastro_predefFilter: ko.observableArray([]), TcsUsuarioAutenticacaoDataCadastro_predefValue: ko.observable(null),
        TcsUsuarioAutenticacaoDataExpiracaoSenha_typeRange: ko.observable('R'), TcsUsuarioAutenticacaoDataExpiracaoSenha_begin: ko.observable(null), TcsUsuarioAutenticacaoDataExpiracaoSenha_end: ko.observable(null), TcsUsuarioAutenticacaoDataExpiracaoSenha_predefFilter: ko.observableArray([]), TcsUsuarioAutenticacaoDataExpiracaoSenha_predefValue: ko.observable(null),
        TcsUsuarioAutenticacaoVigenciaFinal_typeRange: ko.observable('R'), TcsUsuarioAutenticacaoVigenciaFinal_begin: ko.observable(null), TcsUsuarioAutenticacaoVigenciaFinal_end: ko.observable(null), TcsUsuarioAutenticacaoVigenciaFinal_predefFilter: ko.observableArray([]), TcsUsuarioAutenticacaoVigenciaFinal_predefValue: ko.observable(null),
        TcsUsuarioAutenticacaoVigenciaInicial_typeRange: ko.observable('R'), TcsUsuarioAutenticacaoVigenciaInicial_begin: ko.observable(null), TcsUsuarioAutenticacaoVigenciaInicial_end: ko.observable(null), TcsUsuarioAutenticacaoVigenciaInicial_predefFilter: ko.observableArray([]), TcsUsuarioAutenticacaoVigenciaInicial_predefValue: ko.observable(null),
        TcsUsuarioAutenticacaoNomeEmpresa: ko.observable(null),
        TcsUsuarioAcessoDescricaoAmbiente: ko.observable(null),
        TcsUsuarioAcessoDescricaoAmbienteRelacionado: ko.observable(null),
        TcsUsuarioAcessoDescricaoAplicacao: ko.observable(null),
        TcsUsuarioGpeconIdLinx: ko.observable(null),
        TcsUsuarioGpeconNomeEmpresa: ko.observable(null)
    };
    entitySearchRange.clear = function(){
            entitySearchRange.TcsUsuarioAutenticacaoDataAlteracao_typeRange('R'); entitySearchRange.TcsUsuarioAutenticacaoDataAlteracao_begin(null); entitySearchRange.TcsUsuarioAutenticacaoDataAlteracao_end(null); entitySearchRange.TcsUsuarioAutenticacaoDataAlteracao_predefFilter([]); entitySearchRange.TcsUsuarioAutenticacaoDataAlteracao_predefValue(null);
        entitySearchRange.TcsUsuarioAutenticacaoDataCadastro_typeRange('R'); entitySearchRange.TcsUsuarioAutenticacaoDataCadastro_begin(null); entitySearchRange.TcsUsuarioAutenticacaoDataCadastro_end(null); entitySearchRange.TcsUsuarioAutenticacaoDataCadastro_predefFilter([]); entitySearchRange.TcsUsuarioAutenticacaoDataCadastro_predefValue(null);
        entitySearchRange.TcsUsuarioAutenticacaoDataExpiracaoSenha_typeRange('R'); entitySearchRange.TcsUsuarioAutenticacaoDataExpiracaoSenha_begin(null); entitySearchRange.TcsUsuarioAutenticacaoDataExpiracaoSenha_end(null); entitySearchRange.TcsUsuarioAutenticacaoDataExpiracaoSenha_predefFilter([]); entitySearchRange.TcsUsuarioAutenticacaoDataExpiracaoSenha_predefValue(null);
        entitySearchRange.TcsUsuarioAutenticacaoVigenciaFinal_typeRange('R'); entitySearchRange.TcsUsuarioAutenticacaoVigenciaFinal_begin(null); entitySearchRange.TcsUsuarioAutenticacaoVigenciaFinal_end(null); entitySearchRange.TcsUsuarioAutenticacaoVigenciaFinal_predefFilter([]); entitySearchRange.TcsUsuarioAutenticacaoVigenciaFinal_predefValue(null);
        entitySearchRange.TcsUsuarioAutenticacaoVigenciaInicial_typeRange('R'); entitySearchRange.TcsUsuarioAutenticacaoVigenciaInicial_begin(null); entitySearchRange.TcsUsuarioAutenticacaoVigenciaInicial_end(null); entitySearchRange.TcsUsuarioAutenticacaoVigenciaInicial_predefFilter([]); entitySearchRange.TcsUsuarioAutenticacaoVigenciaInicial_predefValue(null);
        entitySearchRange.TcsUsuarioAutenticacaoNomeEmpresa(null);
        entitySearchRange.TcsUsuarioAcessoDescricaoAmbiente(null);
        entitySearchRange.TcsUsuarioAcessoDescricaoAmbienteRelacionado(null);
        entitySearchRange.TcsUsuarioAcessoDescricaoAplicacao(null);
        entitySearchRange.TcsUsuarioGpeconIdLinx(null);
        entitySearchRange.TcsUsuarioGpeconNomeEmpresa(null);
    };
    entitySearchRange.has_TcsUsuarioAutenticacaoDataAlteracao = ko.computed(function(){ return (entitySearchRange.TcsUsuarioAutenticacaoDataAlteracao_typeRange() == 'R' && (entitySearchRange.TcsUsuarioAutenticacaoDataAlteracao_begin() != null || entitySearchRange.TcsUsuarioAutenticacaoDataAlteracao_end() != null) || (entitySearchRange.TcsUsuarioAutenticacaoDataAlteracao_typeRange() == 'P' && entitySearchRange.TcsUsuarioAutenticacaoDataAlteracao_predefFilter().length > 0)); });
    entitySearchRange.has_TcsUsuarioAutenticacaoDataCadastro = ko.computed(function(){ return (entitySearchRange.TcsUsuarioAutenticacaoDataCadastro_typeRange() == 'R' && (entitySearchRange.TcsUsuarioAutenticacaoDataCadastro_begin() != null || entitySearchRange.TcsUsuarioAutenticacaoDataCadastro_end() != null) || (entitySearchRange.TcsUsuarioAutenticacaoDataCadastro_typeRange() == 'P' && entitySearchRange.TcsUsuarioAutenticacaoDataCadastro_predefFilter().length > 0)); });
    entitySearchRange.has_TcsUsuarioAutenticacaoDataExpiracaoSenha = ko.computed(function(){ return (entitySearchRange.TcsUsuarioAutenticacaoDataExpiracaoSenha_typeRange() == 'R' && (entitySearchRange.TcsUsuarioAutenticacaoDataExpiracaoSenha_begin() != null || entitySearchRange.TcsUsuarioAutenticacaoDataExpiracaoSenha_end() != null) || (entitySearchRange.TcsUsuarioAutenticacaoDataExpiracaoSenha_typeRange() == 'P' && entitySearchRange.TcsUsuarioAutenticacaoDataExpiracaoSenha_predefFilter().length > 0)); });
    entitySearchRange.has_TcsUsuarioAutenticacaoVigenciaFinal = ko.computed(function(){ return (entitySearchRange.TcsUsuarioAutenticacaoVigenciaFinal_typeRange() == 'R' && (entitySearchRange.TcsUsuarioAutenticacaoVigenciaFinal_begin() != null || entitySearchRange.TcsUsuarioAutenticacaoVigenciaFinal_end() != null) || (entitySearchRange.TcsUsuarioAutenticacaoVigenciaFinal_typeRange() == 'P' && entitySearchRange.TcsUsuarioAutenticacaoVigenciaFinal_predefFilter().length > 0)); });
    entitySearchRange.has_TcsUsuarioAutenticacaoVigenciaInicial = ko.computed(function(){ return (entitySearchRange.TcsUsuarioAutenticacaoVigenciaInicial_typeRange() == 'R' && (entitySearchRange.TcsUsuarioAutenticacaoVigenciaInicial_begin() != null || entitySearchRange.TcsUsuarioAutenticacaoVigenciaInicial_end() != null) || (entitySearchRange.TcsUsuarioAutenticacaoVigenciaInicial_typeRange() == 'P' && entitySearchRange.TcsUsuarioAutenticacaoVigenciaInicial_predefFilter().length > 0)); });
    
    function deleteGrid(element, cName, cDataItem_listItem, isMultiSelect) {
       var element = element;
       var cName = cName;
       var dataItem_ListItem = cDataItem_listItem.split(';');
       var currentdataItem = dataItem_ListItem[0];
       var currentlistItem = dataItem_ListItem[1];
       $(element).igGridUpdating('endEdit');
       var selectedRows = [];
       var activeRow = $(element).igGrid('activeRow');
       if (isMultiSelect) { if ($(element).igGrid('selectedRows').length > 0) selectedRows = $(element).igGrid('selectedRows');
       } else { selectedRows.push($(element).igGrid('selectedRow')); }
       if (!activeRow) activeRow = selectedRows[0];
       if (isNullOrEmpty(selectedRows[0])) {
           app.showMessage('Nenhum registro selecionado!', 'Informação', ['Ok']);
           return;
       }
       var entity = findElementByKey(eval(currentlistItem), 'RowDataId', isNullOrEmpty(selectedRows) && selectedRows.length === 0 ? 0 : selectedRows[0].id);
       if (isNullOrEmpty(entity)) {
           app.showMessage('Nenhum registro selecionado!', 'Informação', ['Ok']);
           return;
       }
       if (deleteEntity(entity, isMultiSelect)) {
           if (entity.typeName === vm.rootDataTypeName) {
               eval(currentlistItem)['remove'](entity);
           }
       }
       else { return; }
        if ($(element).data('igGrid')._totalRowCount > 0) {
            for (i = 0; i < selectedRows.length; i++) {
               var selectedRow = selectedRows[i];
               $(element).igGridUpdating('deleteRow', selectedRow.id);
            }
        }
    };
    function openEditor(element, cName, cDataItem_listItem, dataV_parentName, entityName, isEditorWithinGrid) {
       var element = element;
       var cName = cName;
       var dataItem_ListItem = cDataItem_listItem.split(';');
       var dataView_parentName = dataV_parentName.split(';');
       var currentdataItem = dataItem_ListItem[0];
       var currentlistItem = dataItem_ListItem[1];
       var entityName = entityName
       var dataView = dataView_parentName[0];
       var parentName = dataView_parentName[1];
       var ui = $(element).data('igGridUpdating');
    
       $('.ui-dialog:has(#' + $('#dialog' + cName + '').attr('id') + ')').empty().remove();
       if ($(element).data('igGridGroupBy') !== undefined && $(element).igGridGroupBy('groupByColumns').length !== 0){
          app.showMessage('Não é possível habilitar o editor template com campos agrupados!', 'Informação', ['Ok']);
          return false;
       }
       if (getSelectedIndex(element) == -1){
          app.showMessage('Registro não selecionado!', 'Informação', ['Ok']);
          return false;
       }
    
       configEditor(element, currentdataItem, currentlistItem);
    
       if (vm.status() !== 'E') {
           $('#addReg' + cName + '').hide();
           $('#delReg' + cName + '').hide();
       }
       else {
           $('#addReg' + cName + '').show();
           $('#delReg' + cName + '').show();
       }
    
       dialogIsOpen = true;
       var pk_id = getSelectedIndex(element) + 1;
       var ds = ui.grid.dataSource;
       var columns = ui.grid.options.columns;
       fillLabels(pk_id, element, dataView, cName);
    
       $.fn['backReg' + cName + ''] = function () {
           if (hasPaging(element).length > 0) {
               gridTrData = ui.grid.dataSource.dataView()[getSelectedIndex(element)];
               if (getSelectedIndex(element) > 0) {
                   pk_id = getSelectedIndex(element) - 1;
                   $(element).igGridSelection('clearSelection');
                   updateGrid(gridTrData, pk_id, ui, currentdataItem, element);
                   updateTemplate(pk_id, 1, element, ui, currentdataItem, currentlistItem);
                   fillLabels(pk_id + 1, element, dataView, cName);
               }
           }
           else{
               pk_id = getSelectedIndex(element) + 1;
               gridTrData = ui.grid.dataSource.dataView()[pk_id - 1];
               updateGrid(gridTrData, pk_id, ui, currentdataItem, element);
               $(element).igGridSelection('clearSelection');
               if (pk_id > 1) {
                   updateTemplate(pk_id, 1, element, ui, currentdataItem, currentlistItem);
                   pk_id = pk_id - 1;
               }
               else
                   $(element).igGridSelection('selectRow', pk_id - 1);
               fillLabels(pk_id, element, dataView, cName)
           }
       }
       $.fn['nextReg' + cName + ''] = function () {
           if (hasPaging(element).length > 0) {
               gridTrData = ui.grid.dataSource.dataView()[getSelectedIndex(element)];
               pk_id = getSelectedIndex(element) + 1;
               if (ui.grid.dataSource.dataView().length > pk_id) {
                   $(element).igGridSelection('clearSelection');
                   updateGrid(gridTrData, pk_id, ui, currentdataItem, element);
                   updateTemplate(pk_id, 2, element, ui, currentdataItem, currentlistItem);
                   pk_id = pk_id + 1;
               }
               else
                   $(element).igGridSelection('selectRow', pk_id - 1);
           } else {
               pk_id = getSelectedIndex(element) + 1;
               gridTrData = ui.grid.dataSource.dataView()[pk_id - 1];
               updateGrid(gridTrData, pk_id, ui, currentdataItem, element);
               var totalGrid = (Array.isArray(ui.grid.options.dataSource) ? ui.grid.options.dataSource.count() : ui.grid.options.dataSource.data().length);
               $(element).igGridSelection('clearSelection');
               if (totalGrid > pk_id) {
                   updateTemplate(pk_id, 2, element, ui, currentdataItem, currentlistItem);
                   pk_id = pk_id + 1;
               }
               else
                   $(element).igGridSelection('selectRow', pk_id - 1);
           }
          fillLabels(pk_id, element, dataView, cName);
       }
       $.fn['addReg' + cName + ''] = function () {
          var addedEntity = eval('vm.createAndNotify' + entityName);
          if (addedEntity) {
             var index = 0; var ds = (Array.isArray(ui.grid.options.dataSource) ? ui.grid.options.dataSource : ui.grid.options.dataSource.data());
             for (index = 0; index < ds.count(); index++) {
                if (addedEntity.RowDataId == ds[index].RowDataId) break;
             }
             updateFieldsTemplate(addedEntity.RowDataId, currentdataItem, currentlistItem);
             fillLabels(index + 1, element, dataView, cName);
          }
       }
       $.fn['delReg' + cName + ''] = function () {
           pk_id = getSelectedIndex(element);
           gridTrData = ui.grid.dataSource.dataView()[pk_id];
           var entity = findElementByKey(eval(currentlistItem), 'RowDataId', gridTrData['RowDataId']);
           if (entity) {
               removeInnerDataUIs(entity);
               if (deleteEntity(entity, false) === false) return false;
               $(element).igGridUpdating('deleteRow', gridTrData['RowDataId']);
           }
           var totalGrid = (Array.isArray(ui.grid.options.dataSource) ? ui.grid.options.dataSource : ui.grid.options.dataSource.data()).length;
           if (totalGrid === 0) return restartGrid(element, cName, isEditorWithinGrid);
           if (pk_id == totalGrid) {
               gridTrData = ui.grid.dataSource.dataView()[totalGrid - 1];
               $(element).igGridSelection('selectRow', totalGrid - 1);
           }
           else {
               gridTrData = ui.grid.dataSource.dataView()[pk_id];
               $(element).igGridSelection('selectRow', pk_id);
           }
           updateFieldsTemplate(gridTrData['RowDataId'], currentdataItem, currentlistItem);
           fillLabels(pk_id, element, dataView, cName);
       }
       $.fn['okReg' + cName + ''] = function () {
           pk_id = getSelectedIndex(element);
           gridTrData = ui.grid.dataSource.dataView()[pk_id];
           updateGrid(gridTrData, pk_id, ui, currentdataItem, element);
           $(element + '_EditorBtn').attr('title', 'Alterar edição para modo Template');
           return restartGrid(element, cName, isEditorWithinGrid);
       }
       $.fn['clickSelectorGrid'] = function (tb) {
           var table = tb[0].offsetParent.id;
          var removeSpace = $('#' + table).data('param').replace(/\s/g, "");
           var param = removeSpace.split(',');
           selectorEditorTemplate(param[0], parseInt(tb[0].id), param[1], param[2], param[3], param[4]);
       }
       if(currentdataItem && eval(currentdataItem));
           eval(currentdataItem).notifySubscribers();
       if (!isEditorWithinGrid) {
           $('#dialog' + cName + '').dialog({
               modal: true,
               width: '90%',
               height: 700,
               show: { effect: 'drop', direction: 'up' },
               draggable: true,
               closeOnEscape: false,
               resizable: false,
               zIndex: getNew_zIndex()
           });
           $('.ui-widget-overlay.ui-front').css('z-index', getNew_zIndex() - 1);
           $('#dialog' + cName + '').dialog('widget').find('.ui-dialog-titlebar-close').hide();
       }
       else{
           $(element + '_ContentDLG').next().addClass('hide');
           $(element + '_container').parent().addClass('hide');
           $(element + '_ContentDLG').attr('style', 'position: static;height: 350px;');
           $('#dialog' + cName + '').appendTo($(element + '_ContentDLG'));
           $('#dialog' + cName + '').show();
       }
    
       return false;
    };
       function updateGrid(grd, pk, ui, currentdataItem, element) {
           if (pk >= 0 && eval(currentdataItem + '()') !== null) {
               var propUpdate = 0;
               var hasChangeProp = false;
               var columns = ui.grid.options.columns;
               for (i = 1; i < columns.length; ++i) {
                   if (columns[i].key.indexOf('Multi') < 0) {
                       propUpdate = getAbsoluteValue(eval(currentdataItem + '()')['' + columns[i].key + '']);
                       if (grd[columns[i].key] != propUpdate) {
                           grd[columns[i].key] = propUpdate;
                           hasChangeProp = true;
                       }
                   }
               }
               if(hasChangeProp) $(element).igGridUpdating('updateRow', grd['RowDataId'], grd);
           }
       };
       function updateTemplate(pk, step, element, ui, currentdataItem, currentlistItem) {
           if (step == 1) {
               if (hasPaging(element).length == 0)
                   pk = pk - 2;
               $(element).igGridSelection('selectRow', pk);
               gridTrData = ui.grid.dataSource.dataView()[pk];
           }
           else if (step == 2) {
               $(element).igGridSelection('selectRow', pk);
               gridTrData = ui.grid.dataSource.dataView()[pk];
           }
           updateFieldsTemplate(gridTrData['RowDataId'], currentdataItem, currentlistItem);
       };
       function updateFieldsTemplate(grd, currentdataItem, currentlistItem) {
           if (vm.goToKey && 'RowDataId' && grd) {
               vm.goToKey('RowDataId', grd, eval(currentdataItem), eval(currentlistItem));
           }
       };
       function configEditor(element, currentdataItem, currentlistItem){
           var mode = $(element).igGridUpdating('option', 'editMode');
           if (mode == 'cell') {
               var rows = $(element).igGrid('rows');
               if (rows.length === 0) {
                   app.showMessage('Não é possível abrir a edição quando não existir ao menos uma linha na grade!', 'Informação', ['Ok']);
                   return false;
               }
               var row =  $(element).igGrid('selectedRow');
               var isChk = $(element).igGridSelection('selectedRows');
               var rowEntity = 0;
               if (isChk && isChk.length != 0) rowEntity = isChk[0].id;
               var entity = findElementByKey(eval(currentlistItem), 'RowDataId', isNullOrEmpty(row) ? rowEntity : row.id);
               if (rowEntity !== 0)
                   updateFieldsTemplate(entity['RowDataId'], currentdataItem, currentlistItem);
               //$(element).igGridUpdating('option', 'editMode', 'rowedittemplate');
               $(element).igGridUpdating('option', 'startEditTriggers', 'dblclick,F2');
               $('.fa.fa-th').addClass('fa fa-list-alt').removeClass('fa-th');
               $(element + '_EditorBtn').attr('title', 'Alterar edição para modo Célula');
           }
           else {
               //$(element).igGridUpdating('option', 'editMode', 'cell');
               $(element).igGridUpdating('option', 'startEditTriggers', 'click');
               $('.fa.fa-list-alt').addClass('fa fa-th').removeClass('fa-list-alt');
               $(element + '_EditorBtn').attr('title', 'Alterar edição para modo Template');
           }
    };
       function restartGrid(element, cName, isEditorWithinGrid) {
           //$(element).igGridUpdating('option', 'editMode', 'cell');
           $(element).igGridUpdating('option', 'startEditTriggers', 'click');
           $('.fa.fa-list-alt').addClass('fa fa-th').removeClass('fa-list-alt');
           $(element).attr('title', 'Alterar edição para modo Template');
           if (isEditorWithinGrid) {
               if (cName.indexOf('dialog') > -1)
                   $(cName).attr('style', 'display: none !important;');
               else
                   $('#dialog' + cName + '').attr('style', 'display: none !important;');
    
                $(element + '_ContentDLG').attr('style', 'position: relative;height: 1px;');
                $(element + '_ContentDLG').next().removeClass('hide');
                $(element + '_container').parent().removeClass('hide');
           }
           else
               $('#dialog' + cName + '').dialog('close');
    
           dialogIsOpen = false;
       };
       function getSelectedIndex(element) {
           var sIndex = -1;
           if ($(element).data('igGridSelection') && $(element).igGridSelection('option', 'multipleSelection')) {
               var trs = $(element).igGrid('selectedRows');
               if (trs.length > 0) sIndex = trs[0].index;
           } else {
               var tr = $(element).igGrid('selectedRow');
               if (tr != null) sIndex = tr.index;
           }
           return sIndex;
       };
       function fillLabels(current, element, dataView, cName) {
           checkDisableControl(element);
           showAndHideColumnsEditor(element, dataView);
           var ui = $(element).data('igGridUpdating');
           var totalGrid = (Array.isArray(ui.grid.options.dataSource) ? ui.grid.options.dataSource : ui.grid.options.dataSource.data()).length;
           if ($(element).data('igGridSelection') && $(element).igGridSelection('option', 'multipleSelection')) {
               var trs = $(element).igGrid('selectedRows');
               if (trs.length > 0) var currentRow = trs[0].index + 1;
           }
           else
               var currentRow = $(element).igGrid('selectedRow').index + 1;
           if (hasPaging(element).length > 0) {
               var totalCurrentPage = totalGrid;
               var currentPage = $(element).igGridPaging('pageIndex') + 1;
               var pageIndex = $(element).igGridPaging('pageIndex');
               var pageSize = $(element).igGridPaging('pageSize');
               if (totalGrid / pageSize > currentPage)
                   totalCurrentPage = (currentPage * ui.grid.dataSource.dataView().length);
               $('label#currentNumber' + cName + '').html(currentRow + ' - ' + totalCurrentPage);
           }
           else
               $('label#currentNumber' + cName + '').html((current == 0 ? totalGrid : current));
           $('label#totalNumber' + cName + '').html(totalGrid);
       };
       function checkDisableControl(element) {
           var columns = $(element).igGridUpdating('option', 'columnSettings');
           columns.forEach(function (entry, index) {
               if (entry.fieldTplDisabled) {
                   var controlTemplate = $('[id^="' + $lx(vm, '#div').selector.replace('#', '') + '"][id$="_' + entry.columnKey + 'Template"]');
                   $(controlTemplate).append('<div style="position: absolute;top:0;left:0;width: 100%;height:100%;z-index:2;opacity:0.4;filter: alpha(opacity = 50)"></div>');
               };
           });
       };
       function showAndHideColumnsEditor(element, dataView) {
           if (vm.status() !== 'C') {
               var colunas = $(element).igGrid('option', 'columns');
               colunas.forEach(function (entry, index) {
                   if (entry.hidden && entry.key !== 'RowDataId') {
                       var control = $('#CadastroUsuarioAutenticacao_div' + (!dataView ? '' : '' + dataView + '_') + entry.key + 'Template');
                       if (!control.hasClass('hide') && !control.hasClass('onlyEditor'))
                           control.addClass('hide');
                   } else if (entry.key !== 'RowDataId') {
                       var control = $('#CadastroUsuarioAutenticacao_div' + (!dataView ? '' : '' + dataView + '_') + entry.key + 'Template');
                       if (control.hasClass('hide'))
                           control.removeClass('hide');
                   }
               });
           }
       };
       function hasPaging(element) {
            return $.grep($(element).igGrid('option', 'features'), function (e) { return e.name == 'Paging'; }); 
       };
       function selectorEditorTemplate(element, pk, cName, cDataItem_listItem, dataV_parentName, entityName) {
           var element = element;
           var dataItem_ListItem = cDataItem_listItem.split(';');
           var dataView_parentName = dataV_parentName.split(';');
           var currentdataItem = dataItem_ListItem[0];
           var currentlistItem = dataItem_ListItem[1];
           var entityName = entityName;
           var dataView = dataView_parentName[0];
           var parentName = dataView_parentName[1];
           var ui = $(element).data('igGridUpdating');
           var verticalContainer = $(element).igGrid('scrollContainer');
           verticalContainer.scrollTop($(element).igGrid('option', 'avgRowHeight') * (pk - 1));
           gridTrData = ui.grid.dataSource.dataView()[pk];
           updateFieldsTemplate(gridTrData['RowDataId'], currentdataItem, currentlistItem);
           updateGrid(gridTrData, pk, ui, currentdataItem, element);
           $(element).igGridSelection('clearSelection');
           $(element).igGridSelection('selectRow', pk);
           if (status() === 'E') notifyPresentation('' + currentlistItem.split('.').pop() + '');
           fillLabels(pk + 1, element, dataView, cName);
           $(element + '_Toggle').slideToggle();
       };
       function loadSeletor(tbGrid, fields, grd, entity) {
           var tbody = $(tbGrid).children('tbody');
           var cols = fields.split(',');
           var list = $(grd).data('igGrid').dataSource.dataView();
           $(tbGrid + ' > tbody > tr').remove();
           var objCols = new Array();
           var metaDataEntity = vm.metadataInfo[entity];
    
           if ($(grd + '_Toggle').is(':hidden')) {
               if (status() !== 'C') {
                   for (j = 0; j < cols.length; j++) {
                       for (var prop in metaDataEntity) {
                           if (metaDataEntity[prop]['key'] == cols[j]) {
                               objCols.push(metaDataEntity[prop]);
                               break;
                           }
                       }
                   }
                   for (i = 0; i < list.length; i++) {
                      var tr = document.createElement('TR');
                      tr.setAttribute('id', i);
                      tr.setAttribute('onclick', '$(this).clickSelectorGrid($(this));');
                      for (j = 0; j < objCols.length; j++) {
                          var td = document.createElement('TD');
                          if (objCols[j].isDomain)
                              var fieldFormat = vm.dataDomains.getName(objCols[j].domainName, list[i][objCols[j].key]);
                          else if (objCols[j].dataType == 'date')
                              var fieldFormat = Globalize.format(getUTCDate(list[i][objCols[j].key]), objCols[j].format);
                          else if (objCols[j].dataType == 'number' && objCols[j].format == 'int')
                              var fieldFormat = Globalize.format(list[i][objCols[j].key], "n0");
                          else
                              var fieldFormat = Globalize.format(list[i][objCols[j].key], (objCols[j].dataType == 'number' ? "n" : objCols[j].format));
                          td.appendChild(document.createTextNode(fieldFormat));
                          tr.appendChild(td);
                      }
                      tbody.append(tr);
                   }
               } else {
                   var tr = document.createElement('TR');
                   var td = document.createElement('TD');
                   td.setAttribute('colspan', '' + cols.length + '');
                   td.style.textAlign = 'center';
                   td.appendChild(document.createTextNode('Modo Pesquisa'));
                   tr.appendChild(td);
                   tbody.append(tr);
               }
           }
       };
    
    
    var dataToolbar = {
            isBusy: isBusy,
            currentRecordInfo: currentRecordInfo,
            canGoFirst: canGoFirst,
            canGoBack: canGoBack,
            canGoForward: canGoForward,
            canGoLast: canGoLast,
            canClear: canClear,
            canQuickSearch: canQuickSearch,
            canNavigate: canNavigate,
            noBusyLoading: _noBusyLoading,
            currentPage: currentPage,
            quickSearch: quickSearch,
            canExport: canExport,
            canGridExport: canGridExport,
            canQuery: canQuery,
            canCustomSearch: canCustomSearch,
            canRefreshCurrentData: canRefreshCurrentData,
            hasDataFeed: hasDataFeed,
            canAddNew: canAddNew,
            canRemove: canRemove,
            canEdit: canEdit,
            canSave: canSave,
            canUndo: canUndo,
            canPrint: canPrint,
            canUnlockUser: canUnlockUser,
            unlockUser: unlockUser,
            goFirst: goFirst,
            goBack: goBack,
            goForward: goForward,
            goLast: goLast,
            adjustNavigationByPage: adjustNavigationByPage,
            query: query,
            customSearch: customSearch,
            customSearchResult: customSearchResult,
            hasCustomSearches: hasCustomSearches,
            refreshCurrentData: refreshCurrentData,
            exportData: exportData,
            customLayout: customLayout,
            undo: undo,
            save: save,
            addNew: addNew,
            remove: remove,
            refresh: refresh,
            clear: clearByUser,
            helper: helper,
            print: print,
            showDataFeedUrl: showDataFeedUrl,
            edit: edit,
            canViewInfo: canViewInfo,
            viewInfo: viewInfo,
            lastSearchFilter: lastSearchFilter,
            importPhoto: importPhoto,
            title: function() { return (uiSettings && uiSettings.displayName ? uiSettings.displayName : ''); }
        };
    
    if (dataContext.dataDomains) {
        dataContext.dataDomains.refreshData = function () {
            refreshToolbar();
        };
    }
    var vm = {
            isDashboardFilter: false,
            layout: layout,
            layoutDesigner: layoutDesigner,
            layoutDesignerOriginal: layoutDesignerOriginal,
            flattenLayout: flattenLayout,
            getLayoutColumnSpan: getLayoutColumnSpan,
            getLayoutDisplayName: getLayoutDisplayName,
            getLayoutVisible: getLayoutVisible,
            getLayoutHeaderGrid: getLayoutHeaderGrid,
            getDimensionUniqueName: getDimensionUniqueName,
            flattenObjectByProperty: flattenObjectByProperty,
            currentLayout: ko.observable(),
            useLikeCommandAsDefault: false,
            dataView: dataView,
            viewName: 'CadastroUsuarioAutenticacao',
            getAddedEntities: getAddedEntities,
            getAllChanges: getAllChanges,
            gridSaveStates: [],
            hasValidationErrors: hasValidationErrors,
            hasInternalUIsValidationErrors: hasInternalUIsValidationErrors,
            canReportErrors: false,
            currentDataItem: currentDataItem,
            exportDataDetails: exportDataDetails,
            openEditor: openEditor,
            deleteGrid: deleteGrid,
            selectorEditorTemplate: selectorEditorTemplate,
            loadSeletor: loadSeletor,
            dialogIsOpen: false,
            currentDataIndex: currentDataIndex,
            navigationByPage: navigationByPage,
            hasMainTopDataGrid: hasMainTopDataGrid,
            dataShared: [],
            hasChanges: hasChanges,
            isSaving: isSaving,
            enabledForEditing: enabledForEditing,
            dataToolbar: dataToolbar,
            getDataContext: function() { return dataContext; },
            getParentSelectorDataName: getParentSelectorDataName,
            validParentSelectorDataCondition: validParentSelectorDataCondition,
            addNewToInnerUI: addNewToInnerUI,
            getDataFromInnerUI: getDataFromInnerUI,
            queryInnerUIs: queryInnerUIs,
            clear: clear,
            clearInnerUIs: clearInnerUIs,
            dataSource: dataSource,
            getMaxLength: getMaxLength,
            addDataSource: addDataSource,
            getVisibleProperties: getVisibleProperties,
            status: status,
            removeParentRelatedItems: removeParentRelatedItems,
            onSavingValidation: onSavingValidation,
            goToKey: goToKey,
            getSpecializedLookupItems: getSpecializedLookupItems,
            dataBind: dataBind,
            isDataSourceHided: isDataSourceHided,
            //Durandal Events
            activate: activate,
            binding: binding,
            finalizeCombo: finalizeCombo,
            dataCombo: dataCombo,
            clearCombo: clearCombo,
            dataDomains: dataContext.dataDomains,
            bindingComplete: bindingComplete,
            attached: attached,
            canDeactivate: canDeactivate,
            canActivate: canActivate,
            deactivate: deactivate,
            //End Durandal Events
            compositionComplete: compositionComplete,
            detached: detached,
            app: app,
            lookUpProperties: dataContext.lookUpProperties,
            metadataInfo: dataContext.metadataInfo,
            dataExportInfo: dataContext.dataExportInfo,
            entityNames: dataContext.entityNames,
            lookUpNames: dataContext.lookUpNames,
            getWithBinding: dataContext.getWithBinding,
            managerAuth: managerAuth,
            rootBmTypeName: 'TCS_USUARIO_AUTENTICACAO',
            rootDataTypeName: 'TcsUsuarioAutenticacao',
            rootNamespace: 'Linx.Framework.BV.UsuarioAutorizacao',
            setSecurity: setSecurity,
            isReportComposition: isReportComposition,
            refreshToolbar: refreshToolbar,
            refreshCurrentBind: refreshCurrentBind,
            lazyRefreshBinding: lazyRefreshBinding,
            createEntity: createEntity,
            notifyPresentation: notifyPresentation,
            notifyInnerElements: notifyInnerElements,
            getServiceAddress: dataContext.getServiceAddress,
            getAccessGroup: dataContext.getAccessGroup,
            getBandeiraRede: getBandeiraRede,
            getCurrentBrands: getCurrentBrands,
            setBandeiraRede: setBandeiraRede,
            entitySearchRange: entitySearchRange,
            modalMultimidia: modalMultimidia,
            currentActivityInformation: currentActivityInformation,
            showProcessing: showProcessing,
            closeProcessing: closeProcessing,
            internalUIs: [],
            viewType: viewType,
            hideToolbar: hideToolbar,
            isDependentVM: isDependentVM,
            brandDecimals: brandDecimals,
            getInnerJExpression: getInnerJExpression,
            allowMultiSelectionInSearch: allowMultiSelectionInSearch,
            transactionNumberControl: transactionNumberControl,
            UpdateMask: UpdateMask,
            OnDataGridCreated: OnDataGridCreated,
            createTcsUsuarioAutenticacao: createTcsUsuarioAutenticacao,
            createAndNotifyTcsUsuarioAutenticacao: createAndNotifyTcsUsuarioAutenticacao,
            createTcsUsuarioAcesso: createTcsUsuarioAcesso,
            createAndNotifyTcsUsuarioAcesso: createAndNotifyTcsUsuarioAcesso,
            createTcsIdentidadeExterna: createTcsIdentidadeExterna,
            createAndNotifyTcsIdentidadeExterna: createAndNotifyTcsIdentidadeExterna,
            createTcsUsuarioGpecon: createTcsUsuarioGpecon,
            createAndNotifyTcsUsuarioGpecon: createAndNotifyTcsUsuarioGpecon,
            deleteEntity: deleteEntity,
            currentBrands: ko.observable(null),
            brands: managerBrand.getBrandVM(),
            hasBrand: false,
            controllerName: dataContext.controllerName,
            getJExpression: getJExpression,
            replaceInnerUIsKeys: replaceInnerUIsKeys,
            replaceKeyFromParent: replaceKeyFromParent,
            getQueryFilter: getQueryFilter,
            getTranslatedFilter: getTranslatedFilter,
            sortData: sortData,
            lastJEntitySearch: function () { return lastJEntitySearch; },
            isEditable: isEditable,
            setStatus: setStatus,
            common: common,
            getDecimalsByData: getDecimalsByData,
            showRegisteredUI: showRegisteredUI,
            openingExternalUIFromGrid: openingExternalUIFromGrid,
            __moduleId__: 'pkg_linx-framework-bv-spa/viewmodels/CadastroUsuarioAutenticacao',
            pivots : pivots
        };
    
    dataContext.setCurrentViewModel(vm);
    return vm;
}

return vmInstance;
});
