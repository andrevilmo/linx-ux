define(['durandal/app', 'pkg_linx-framework-bv-spa/services/ParametroContext', 'plugins/router', 'plugins/widget', 'managers/__auth', 'viewmodels/shared/modal', 'viewmodels/shared/modal2', 'managers/brand', 'managers/predefinedFilters', 'services/logger', 'viewmodels/shared/modalMultimidia', 'common', 'pkg_linx-framework-bv-spa/viewmodels/CadastroParametroComplement', 'viewmodels/shared/modalCustomSearch'],
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
       return {Name: 'CadastroParametro', Items: [

	 {Name: "CadastroParametro_gbTcsParametro05149c7cde9245c69b9e2745678608cb", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroParametro_gbGroupBox_cc1d81f02c754f13b35e1beac7752c33", DisplayName: "Informações do Parâmetro", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroParametro_tbTituloParametro", DisplayName: "Título", ColumnSpan: 12, Visible: true, Key: "TituloParametro"},
	 {Name: "CadastroParametro_tbDescParametro", DisplayName: "Descrição", ColumnSpan: 12, Visible: true, Key: "DescParametro"},
	 {Name: "CadastroParametro_lUpDescGrupoParametro", DisplayName: "Grupo", ColumnSpan: 12, Visible: true, LookUpName: "LookUpTcsParametroGrupoAutorizacao", Key: "DescGrupoParametro"},
	 {Name: "CadastroParametro_lUpIdTcsAplicativo", DisplayName: "Aplicativo", ColumnSpan: 2, Visible: true, LookUpName: "LookUpTcsAplicativo", Key: "IdTcsAplicativo"},
	 {Name: "CadastroParametro_lUpDescricaoAplicativo", DisplayName: "Descrição", ColumnSpan: 10, Visible: true, LookUpName: "LookUpTcsAplicativo", Key: "DescricaoAplicativo"},
	 {Name: "CadastroParametro_cmbLxDatatypeParametro", DisplayName: "Tipo do Dado", ColumnSpan: 12, Visible: true, Key: "LxDatatypeParametro"},
	 {Name: "CadastroParametro_cmbLxTipoValidacaoParametro", DisplayName: "Tipo Validação", ColumnSpan: 12, Visible: true, Key: "LxTipoValidacaoParametro"},
	 {Name: "CadastroParametro_ntxNivelAcesso", DisplayName: "Nível Acesso Visualização", ColumnSpan: 12, Visible: true, Key: "NivelAcesso"},
	 {Name: "CadastroParametro_ntxNivelAcessoEdicao", DisplayName: "Nível Acesso Edição", ColumnSpan: 12, Visible: true, Key: "NivelAcessoEdicao"},
	 {Name: "CadastroParametro_ckPermiteVariacaoPorEntidade", DisplayName: "Permite Variação por Entidade", ColumnSpan: 12, Visible: true, Key: "PermiteVariacaoPorEntidade"},
	 {Name: "CadastroParametro_ckIndicaEnviaPdv", DisplayName: "Envia PDV", ColumnSpan: 12, Visible: true, Key: "IndicaEnviaPdv"},
	 {Name: "CadastroParametro_edObsParametro", DisplayName: "Obs", ColumnSpan: 12, Visible: true, Key: "ObsParametro"},]},
	 {Name: "CadastroParametro_gbvalorPadrao", DisplayName: "Valor Padrão", ColumnSpan: 6, Visible: true, Items: [
	 {Name: "CadastroParametro_cntValorParametro", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroParametro_tbValorParametro", DisplayName: "", ColumnSpan: 12, Visible: true, Key: "ValorParametro"},]},
	 {Name: "CadastroParametro_cntValorParametroData", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroParametro_dtValorParametroData", DisplayName: "", ColumnSpan: 12, Visible: true, Key: "ValorParametroData"},]},
	 {Name: "CadastroParametro_cntValorParametroBool", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroParametro_ckValorParametroBool", DisplayName: "Verdadeiro", ColumnSpan: 12, Visible: true, Key: "ValorParametroBool"},]},
	 {Name: "CadastroParametro_cntValorParametroMascara", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroParametro_tbValorParametroMascara", DisplayName: "", ColumnSpan: 12, Visible: true, Key: "ValorParametro"},]},]},
	 {Name: "CadastroParametro_gbvariacaoParametro", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroParametro_tcTcsParametroTabControl", DisplayName: "Parâmetros", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroParametro_tiTcsParametroTabelaSelecaoTabItem", DisplayName: "Variação", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroParametro_cntCustomContainer_137d41fb66424aaeb2135bed5bda1264", DisplayName: "New Group", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroParametro_dGridTcsParametroTabelaSelecao", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroParametro_lUpTcsParametroTabelaSelecao_NomeTabela", Name: "CadastroParametro_dGridTcsParametroTabelaSelecao_NomeTabela", DisplayName: "Nome Tabela", ColumnSpan: 9, Visible: true, LookUpName: "LookUpTcsTabelaAutorizacaoSelecao", Key: "NomeTabela"},
	 {Id: "CadastroParametro_lUpTcsParametroTabelaSelecao_DescTabela", Name: "CadastroParametro_dGridTcsParametroTabelaSelecao_DescTabela", DisplayName: "Descrição", ColumnSpan: 9, Visible: true, LookUpName: "LookUpTcsTabelaAutorizacaoSelecao", Key: "DescTabela"},
	 {Id: "CadastroParametro_cmbTcsParametroTabelaSelecao_LxParametroHierarquia", Name: "CadastroParametro_dGridTcsParametroTabelaSelecao_LxParametroHierarquia", DisplayName: "Hierarquia", ColumnSpan: 6, Visible: true, Key: "LxParametroHierarquia"},]},]},]},
	 {Name: "CadastroParametro_tiTcsParametroValorUsuarioTabItem", DisplayName: "Usuário", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroParametro_dGridValorUsuario", DisplayName: "Grid Usuário", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroParametro_lUpNomeUsuario", Name: "CadastroParametro_dGridValorUsuario_NomeUsuario", DisplayName: "Nome Usuário", ColumnSpan: 9, Visible: true, LookUpName: "LookTcsParametroUsuario", Key: "NomeUsuario"},
	 {Id: "CadastroParametro_tbTcsParametroValorUsuario_ValorParametro", Name: "CadastroParametro_dGridValorUsuario_ValorParametro", DisplayName: "Valor", ColumnSpan: 9, Visible: true, Key: "ValorParametro"},
	 {Id: "CadastroParametro_ckTcsParametroValorUsuario_ValorParametroBool", Name: "CadastroParametro_dGridValorUsuario_ValorParametroBool", DisplayName: "Valor", ColumnSpan: 9, Visible: true, Key: "ValorParametroBool"},]},]},
	 {Name: "CadastroParametro_tiTcsParametroValorRedeTabItem", DisplayName: "Rede", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroParametro_dGridTcsParametroValorRede", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroParametro_lUpTcsParametroValorRede_CodBandeiraRede", Name: "CadastroParametro_dGridTcsParametroValorRede_CodBandeiraRede", DisplayName: "Código Bandeira / Rede", ColumnSpan: 8, Visible: true, LookUpName: "LookUpParametroRede", Key: "CodBandeiraRede"},
	 {Id: "CadastroParametro_lUpTcsParametroValorRede_DescBandeiraRede", Name: "CadastroParametro_dGridTcsParametroValorRede_DescBandeiraRede", DisplayName: "Bandeira / Rede", ColumnSpan: 9, Visible: true, LookUpName: "LookUpParametroRede", Key: "DescBandeiraRede"},
	 {Id: "CadastroParametro_tbTcsParametroValorRede_ValorParametro", Name: "CadastroParametro_dGridTcsParametroValorRede_ValorParametro", DisplayName: "Valor", ColumnSpan: 9, Visible: true, Key: "ValorParametro"},
	 {Id: "CadastroParametro_ckTcsParametroValorRede_ValorParametroBool", Name: "CadastroParametro_dGridTcsParametroValorRede_ValorParametroBool", DisplayName: " Valor", ColumnSpan: 9, Visible: true, Key: "ValorParametroBool"},]},]},
	 {Name: "CadastroParametro_tiTcsParametroValorGpeconTabItem", DisplayName: "Grupo Econômico", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroParametro_dGridTcsParametroValorGpecon", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroParametro_lUpTcsParametroValorGpecon_IdGpecon", Name: "CadastroParametro_dGridTcsParametroValorGpecon_IdGpecon", DisplayName: "Código Grupo Econômico", ColumnSpan: 5, Visible: true, LookUpName: "LookUpParametroGpecon", Key: "IdGpecon"},
	 {Id: "CadastroParametro_lUpTcsParametroValorGpecon_DescGrupoEconomico", Name: "CadastroParametro_dGridTcsParametroValorGpecon_DescGrupoEconomico", DisplayName: "Grupo Econômico", ColumnSpan: 9, Visible: true, LookUpName: "LookUpParametroGpecon", Key: "DescGrupoEconomico"},
	 {Id: "CadastroParametro_tbTcsParametroValorGpecon_ValorParametro", Name: "CadastroParametro_dGridTcsParametroValorGpecon_ValorParametro", DisplayName: "Valor", ColumnSpan: 9, Visible: true, Key: "ValorParametro"},
	 {Id: "CadastroParametro_ckTcsParametroValorGpecon_ValorParametroBool", Name: "CadastroParametro_dGridTcsParametroValorGpecon_ValorParametroBool", DisplayName: " Valor", ColumnSpan: 9, Visible: true, Key: "ValorParametroBool"},]},]},
	 {Name: "CadastroParametro_tiTcsParametroValorFilialTabItem", DisplayName: "Filial", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroParametro_dGridTcsParametroValorFilial", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroParametro_lUpTcsParametroValorFilial_CodigoFilial", Name: "CadastroParametro_dGridTcsParametroValorFilial_CodigoFilial", DisplayName: "Código Filial", ColumnSpan: 6, Visible: true, LookUpName: "LookUpParametroFilial", Key: "CodigoFilial"},
	 {Id: "CadastroParametro_lUpTcsParametroValorFilial_NomeFilial", Name: "CadastroParametro_dGridTcsParametroValorFilial_NomeFilial", DisplayName: "Filial", ColumnSpan: 9, Visible: true, LookUpName: "LookUpParametroFilial", Key: "NomeFilial"},
	 {Id: "CadastroParametro_tbTcsParametroValorFilial_ValorParametro", Name: "CadastroParametro_dGridTcsParametroValorFilial_ValorParametro", DisplayName: "Valor", ColumnSpan: 9, Visible: true, Key: "ValorParametro"},
	 {Id: "CadastroParametro_ckTcsParametroValorFilial_ValorParametroBool", Name: "CadastroParametro_dGridTcsParametroValorFilial_ValorParametroBool", DisplayName: "Valor", ColumnSpan: 9, Visible: true, Key: "ValorParametroBool"},]},]},
	 {Name: "CadastroParametro_tiTcsParametroValorLjvLojaTabItem", DisplayName: "Loja", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroParametro_dGridTcsParametroValorLjvLoja", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroParametro_lUpTcsParametroValorLjvLoja_CodLoja", Name: "CadastroParametro_dGridTcsParametroValorLjvLoja_CodLoja", DisplayName: "Código Loja", ColumnSpan: 6, Visible: true, LookUpName: "LookUpParametroLoja", Key: "CodLoja"},
	 {Id: "CadastroParametro_lUpTcsParametroValorLjvLoja_DescLoja", Name: "CadastroParametro_dGridTcsParametroValorLjvLoja_DescLoja", DisplayName: "Loja", ColumnSpan: 9, Visible: true, LookUpName: "LookUpParametroLoja", Key: "DescLoja"},
	 {Id: "CadastroParametro_tbTcsParametroValorLjvLoja_ValorParametro", Name: "CadastroParametro_dGridTcsParametroValorLjvLoja_ValorParametro", DisplayName: "Valor", ColumnSpan: 9, Visible: true, Key: "ValorParametro"},
	 {Id: "CadastroParametro_ckTcsParametroValorLjvLoja_ValorParametroBool", Name: "CadastroParametro_dGridTcsParametroValorLjvLoja_ValorParametroBool", DisplayName: "Valor", ColumnSpan: 9, Visible: true, Key: "ValorParametroBool"},]},]},
	 {Name: "CadastroParametro_tiTcsParametroValorVariacaoGenericaTabItem", DisplayName: "Demais Variações", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroParametro_dGridTcsParametroValorVariacaoGenerica", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroParametro_lUpTcsParametroValorVariacaoGenerica_NomeTabela", Name: "CadastroParametro_dGridTcsParametroValorVariacaoGenerica_NomeTabela", DisplayName: "Nome Tabela", ColumnSpan: 8, Visible: true, LookUpName: "LookUpTcsTabelaAutorizacaoC", Key: "NomeTabela"},
	 {Id: "CadastroParametro_tbTcsParametroValorVariacaoGenerica_ChaveSelecao", Name: "CadastroParametro_dGridTcsParametroValorVariacaoGenerica_ChaveSelecao", DisplayName: "Chave", ColumnSpan: 9, Visible: true, Key: "ChaveSelecao"},
	 {Id: "CadastroParametro_tbTcsParametroValorVariacaoGenerica_ValorParametro", Name: "CadastroParametro_dGridTcsParametroValorVariacaoGenerica_ValorParametro", DisplayName: "Valor", ColumnSpan: 9, Visible: true, Key: "ValorParametro"},
	 {Id: "CadastroParametro_ckTcsParametroValorVariacaoGenerica_ValorParametroBool", Name: "CadastroParametro_dGridTcsParametroValorVariacaoGenerica_ValorParametroBool", DisplayName: " Valor", ColumnSpan: 9, Visible: true, Key: "ValorParametroBool"},]},]},]},]},]},       ]};
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
            vm.flattenLayout(ko.observable(flattenObjectByProperty(result.objectLanguage_CadastroParametro(), 'Name'))());
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
        app.showMessage(dataContext.getDataFeedUrl(), 'Endereço do serviço', ['Ok']);
    };
    var lastSearchFilter = function () {
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
      vm.CadastroParametro = getVM;
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
                  if (settings.parentVM) { settings.parentVM.internalUIs = [ 'CadastroParametro' ]; settings.parentVM.CadastroParametro = getVM; }
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
                      parentVM.CadastroParametro = getVM;
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
                      if ($.inArray('CadastroParametro', parentVM.internalUIs) === -1){
                           if (parentVM.internalUIs) {
                               parentVM.internalUIs.push('CadastroParametro');
                           }
                           else {
                               parentVM.internalUIs = ['CadastroParametro'];
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
        setSecurity(false, true, true, false, true, true, true, true, true, true);
        managerAuth.getFormAccess('linx-framework-bv-spa-CadastroParametro', function (data) {
           if (data && !data.AcessoTotal) {
              setSecurity(false, true, data.PesquisaEspecial, false, data.Alterar, data.Layout, true, data.Imprimir, data.Pesquisar, data.Exportar);
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
            app.showMessage('Versão de formulário incompatível com a versão de ambiente [' + managerAuth.shellVersion + '].', 'Formulário: CadastroParametro', ['Ok']);
            return false;
        }
        return true;
    };
    var deactivate = function() {
       document.removeEventListener(dataContext.contextUpdtEvt, contextDataUpdateHandler, false);
    };
    var compositionComplete = function() {
        //changeLanguage();
        $('#CadastroParametro_tcTcsParametroTabControl').on('shown.bs.tab', function (e) { vm.notifyInnerElements($(e.target.hash)); });
    initializeTabControl('#CadastroParametro_tcTcsParametroTabControl');

    complement.renderCadastroParametro_dGridTcsParametroTabelaSelecao(vm);

    complement.renderCadastroParametro_dGridValorUsuario(vm);

    complement.renderCadastroParametro_dGridTcsParametroValorRede(vm);

    complement.renderCadastroParametro_dGridTcsParametroValorGpecon(vm);

    complement.renderCadastroParametro_dGridTcsParametroValorFilial(vm);

    complement.renderCadastroParametro_dGridTcsParametroValorLjvLoja(vm);

    complement.renderCadastroParametro_dGridTcsParametroValorVariacaoGenerica(vm);

    complement.renderscyCadastroParametro_dGrid(vm);


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
        vm.currentBrands.subscribe(function(newValue) {
            newValue = isNull(newValue) ? vm.currentBrands() : newValue;
            var searchedBrands = managerBrand.searchBrandsVM(newValue, managerAuth.getIdTcsAmbiente());
            var reset = (!newValue || searchedBrands.cod === ''), decimals = searchedBrands.decimals;
                                       complement.ChangedBrandCadastroParametro_dGridTcsParametroTabelaSelecao(vm, decimals, reset);
                               complement.ChangedBrandCadastroParametro_dGridValorUsuario(vm, decimals, reset);

                               complement.ChangedBrandCadastroParametro_dGridTcsParametroValorRede(vm, decimals, reset);

                               complement.ChangedBrandCadastroParametro_dGridTcsParametroValorGpecon(vm, decimals, reset);

                               complement.ChangedBrandCadastroParametro_dGridTcsParametroValorFilial(vm, decimals, reset);

                               complement.ChangedBrandCadastroParametro_dGridTcsParametroValorLjvLoja(vm, decimals, reset);

                               complement.ChangedBrandCadastroParametro_dGridTcsParametroValorVariacaoGenerica(vm, decimals, reset);



            vm.brandDecimals(reset || isNull(decimals) ? null : decimals);
            vm.currentDataItem.notifySubscribers();
        });
        vm.currentBrands.notifySubscribers();
        getLayoutFormPadrao(vm);
        return true;
    };
    var detached = function (view) {
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
        if (isNullOrEmpty(entityName)) entityName = 'TcsParametro';
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
    
    var visibleColumns = 'TituloParametro,DescParametro,DescGrupoParametro,IdTcsAplicativo,DescricaoAplicativo,LxDatatypeParametro,LxTipoValidacaoParametro,NivelAcesso,NivelAcessoEdicao,PermiteVariacaoPorEntidade,IndicaEnviaPdv,ObsParametro';
    
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
        if (forceAdd)
            require(['viewmodels/shared/addCustomExport'],
                function(addCustomExport){ addCustomExport.showModal(vm, null, 'TcsParametro', getVisiblePropertiesForExcel('dataView'), null, true, isExcelDataSource); } );
        else
            require(['viewmodels/shared/customExport'],
                function(modalExport){ modalExport.showModal(vm, 'TcsParametro', getVisiblePropertiesForExcel('dataView'), null, { canAdd: true, canEdit: true, canDel: true }, isExcelDataSource); } );
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
    var getQueryFilter = function (currentDI) {
        if (typeof currentDI === 'undefined') currentDI = currentDataItem();
        dataBind('', true);
        currentDI.setBandeiraRede(getBandeiraRede());
        eSearch = getJExpression(currentDI);
        if (eSearch === 'Error')
           return 'Error';
       translatedJEntitySearch = common.translateSearch(dataContext, eSearch);
        if (!isNullOrEmpty(customSearchResult.searchDefinition)) eSearch += customSearchResult.searchDefinition;
        return eSearch;
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
        return dataContext.getEntities('TcsParametro', [dataContext.breeze.EntityState.Added]);
    }
    var getRelatedElementsInCache = function () {
        if (parentEntityRelated != null && preserveDataCurrentState()) {
           var cacheElements = dataContext.getEntities('TcsParametro');
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
        if (freeEntityForQuerying == null && isChildVM()) freeEntityForQuerying = dataContext.createFreeEntity('TcsParametro');
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
        return dataContext.getTcsParametroByEntitySearchNoAssociations(lastJEntitySearch, 0, pageSize(), (pageSize() > 0), preserveDataCurrentState(), status() !== 'E', sortInfo, querySucceeded, complete);
    
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
            if (vm.status() !== 'E') { for (var idx = 0; idx < data.results.length; idx++) { dataContext.initializePOCO(data.results[idx], 'TcsParametro'); } }
            hasError = false;
            dataView(data.results);
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
            pageCount( (pageSize() > 0 ? Math.ceil((data.inlineCount ? data.inlineCount : dataView().length) / pageSize()) : 1) );
            totalItemCount((data.inlineCount ? data.inlineCount : dataView().length));
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
        return dataContext.getTcsParametroByEntitySearchNoAssociations(lastJEntitySearch, curPage * pageSize(), pageSize(), false, false, status() !== 'E', sortInfo, querySucceeded, complete);
    
        function complete() {
            vm.closeProcessing();
        }
    
        function querySucceeded(data) {
            if (vm.status() !== 'E') { for (var idx = 0; idx < data.results.length; idx++) { dataContext.initializePOCO(data.results[idx], 'TcsParametro'); } }
            dataView(data.results);
            currentPage(curPage);
            goToIndex((goLast ? dataView().length : 0));
            dataBind('dataView');
        }
    };
    //#region Client Events
    var OnEdited = function () {
    //se não possui valor padrão e não tem variação obrigatória.
if (vm.currentDataItem().TcsParametroValorList().count() === 0){
	var hierarquia = $.grep(vm.currentDataItem().TcsParametroTabelaSelecaoList(), function(element, index) { return getAbsoluteValue(element.LxParametroHierarquia) === 100 });
	
	if (hierarquia.count() <= 0){
		vm.createAndNotifyTcsParametroValor(vm.currentDataItem());
		
		if (getAbsoluteValue(vm.currentDataItem().LxDatatypeParametro) != 3){
			vm.currentDataItem().TcsParametroValorList()[0].ValorParametroData(getCurrentDate());
		}
		
	}
}

AdjustValorPadrao();
AdjustControls();
$('.ui-iggrid-hiding-hiddencolumnindicator').hide();	

    }
    var OnNavigated = function () {
    AdjustControls();
    }
    var OnCleared = function () {
    AdjustControls();
    }
    var OnLoaded = function () {
    vm.showProcessing('Verificando nível de acesso...');

dataContext.sharedData['NIVELACESSOPARAMETRO'] = 0

$.ajax({
			type: 'GET',
            message: "Verificando nível de acesso...",
            messageUser: "Verificando nível de acesso...",
            globalError: true,
			headers: managerAuth.getHeaders(),
            url: managerAuth.getServiceAddress('LinxFrameworkParametro') + '/GetParameterValue?serializedParameterList=NIVEL_ACESSO_PARAMETRO{TCS_USUARIO|' + managerAuth.loginInfo.UidUsuario + '}' ,
            dataType: 'json',
            cache: false,
            error: function (jqXHR, textStatus, errorThrown) {
						vm.closeProcessing();
                    },
			success: function (data) {
                        var parameters = data.split('|');
						if (parameters.count() == 2){
							dataContext.sharedData['NIVELACESSOPARAMETRO'] = parseInt(parameters[1])
							AdjustControls();
						}
						vm.closeProcessing();
                    }
                });

    }
    var AdjustControls = function () {
    var control = $lx(vm, '#gbvalorPadrao');
if(control.length){
	
	if(status() == "C"){
		//Habilita todas as Tabs novamente
		//Variações
		
      setVisibility(vm, 'gbvariacaoParametro',true);
    
		//Valor Padrão
		
      setVisibility(vm, 'gbvalorPadrao',true);
    
		//Usuário
		
        setTabVisibility(vm, 'TcsParametroValorUsuarioTabItem',  true) ;
    ;
		//Rede
		
        setTabVisibility(vm, 'TcsParametroValorRedeTabItem',  true) ;
    ;
		//Grupo Econômico
		
        setTabVisibility(vm, 'TcsParametroValorGpeconTabItem',  true) ;
    ;
		//Filial
		
        setTabVisibility(vm, 'TcsParametroValorFilialTabItem',  true) ;
    ;		
		//Loja
		
        setTabVisibility(vm, 'TcsParametroValorLjvLojaTabItem',  true) ;
    ;
		//Demais variações
		
        setTabVisibility(vm, 'TcsParametroValorVariacaoGenericaTabItem',  true) ;
    
	}
	else
	{
		//se não possui variação por entidade esconde Tab de Variações
		
      setVisibility(vm, 'gbvariacaoParametro',getAbsoluteValue(vm.currentDataItem().PermiteVariacaoPorEntidade));
    

		if (getAbsoluteValue(vm.currentDataItem().PermiteVariacaoPorEntidade)){
			//Valor padrão
			var hierarquia;
			var gridColumn;
			hierarquia = $.grep(vm.currentDataItem().TcsParametroTabelaSelecaoList(), function(element, index) { return getAbsoluteValue(element.LxParametroHierarquia) === 100 });
			
      setVisibility(vm, 'gbvalorPadrao',hierarquia.count() <= 0);
    
			
			//Variação por usuário
			hierarquia = $.grep(vm.currentDataItem().TcsParametroTabelaSelecaoList(), function(element, index) { return getAbsoluteValue(element.UidTabela).toUpperCase() === "18E96E62-AB34-41E9-99FA-B6F755CAD3D2" });
			
        setTabVisibility(vm, 'TcsParametroValorUsuarioTabItem',  hierarquia.count() > 0) ;
    ;
			
			//Variação por Rede
			hierarquia = $.grep(vm.currentDataItem().TcsParametroTabelaSelecaoList(), function(element, index) { return getAbsoluteValue(element.UidTabela).toUpperCase() === "B24D57B5-BE89-4335-8017-9E581F44E06D" });
			
        setTabVisibility(vm, 'TcsParametroValorRedeTabItem',  hierarquia.count() > 0) ;
    ;
			
			//Variação por Grupo Econômico
			hierarquia = $.grep(vm.currentDataItem().TcsParametroTabelaSelecaoList(), function(element, index) { return getAbsoluteValue(element.UidTabela).toUpperCase() === "E2622A3A-2EA0-46D9-9E50-917A1ACF474A" });
			
        setTabVisibility(vm, 'TcsParametroValorGpeconTabItem',  hierarquia.count() > 0) ;
    ;

			//Variação por Filial
			hierarquia = $.grep(vm.currentDataItem().TcsParametroTabelaSelecaoList(), function(element, index) { return getAbsoluteValue(element.UidTabela).toUpperCase() === "AC554A67-E9F8-4F59-BD6A-BB9762BD20C1" });
			
        setTabVisibility(vm, 'TcsParametroValorFilialTabItem',  hierarquia.count() > 0) ;
    ;		
			
			//Variação por Loja
			hierarquia = $.grep(vm.currentDataItem().TcsParametroTabelaSelecaoList(), function(element, index) { return getAbsoluteValue(element.UidTabela).toUpperCase() === "513CBC3E-FF6A-4B56-BCC5-350C63BF902F" });
			
        setTabVisibility(vm, 'TcsParametroValorLjvLojaTabItem',  hierarquia.count() > 0) ;
    ;
			
			//Demais Variações
			hierarquia = $.grep(vm.currentDataItem().TcsParametroTabelaSelecaoList(), function(element, index) { return jQuery.inArray(getAbsoluteValue(element.UidTabela).toUpperCase(), ["B24D57B5-BE89-4335-8017-9E581F44E06D", "18E96E62-AB34-41E9-99FA-B6F755CAD3D2", "E2622A3A-2EA0-46D9-9E50-917A1ACF474A", "513CBC3E-FF6A-4B56-BCC5-350C63BF902F", "AC554A67-E9F8-4F59-BD6A-BB9762BD20C1"]) < 0});
			
        setTabVisibility(vm, 'TcsParametroValorVariacaoGenericaTabItem',  hierarquia.count() > 0) ;
    
		}
		else {
			
      setVisibility(vm, 'gbvalorPadrao',true);
    
		}
	}
}

//Grids
AdjustGrid('dGridTcsParametroValorRede');
AdjustGrid('dGridValorUsuario');
AdjustGrid('dGridTcsParametroValorGpecon');
AdjustGrid('dGridTcsParametroValorFilial');
AdjustGrid('dGridTcsParametroValorLjvLoja');
AdjustGrid('dGridTcsParametroValorVariacaoGenerica');

//ValorPadrao
AdjustValorPadrao();

var nivelAcessoParametro = dataContext.sharedData['NIVELACESSOPARAMETRO'];
if (nivelAcessoParametro){
	_canEdit = (getAbsoluteValue(vm.currentDataItem().NivelAcessoEdicao) >= nivelAcessoParametro);
	refreshToolbar();
}
    }
    var OnDataGridCreated = function (dataGridName) {
    AdjustGrid(dataGridName);
$('.ui-iggrid-hiding-hiddencolumnindicator').hide();
    }
    var AdjustGrid = function (gridName) {
    var control = $lx(vm, '#' + gridName);

if (!control.length || !control.data('igGrid')){
	return;
	}

var columns = control.igGrid("option", "columns");
	
var gridColumn = $.grep(columns, function (element, index) { return element.key == 'ValorParametro' });

if (gridColumn.count() > 0) {
	
	var dataType = 'string';
	var format = '';
	var textMode = '';
	var formatter = null;
	var lxdatatype = getAbsoluteValue(vm.currentDataItem().LxDatatypeParametro)


	switch (lxdatatype)
	{
		case 1: //Numérico
			dataType = 'string';
			format = '';
			break;
			
		case 2: //Caractere
			dataType = 'string';
			format = '';
			break;
			
		case 3: //Data
			dataType = 'date';
			format = 'dd/MM/yyyy';
			break;
			
		case 4://Lógico
			dataType = 'bool';
			format = 'checkbox';
			break;
				
		case 5://Senha
			datatype = 'string';
			format = '';
			textMode = 'password';
			formatter = function(val) { return (val == null ? '' : '*****************************') };
			break;
	}

	gridColumn[0].dataType = dataType;
	gridColumn[0].format = format;
	gridColumn[0].formatter = formatter;
	control.igGrid("option", "columns", columns);
		
	var columnSettings = control.igGridUpdating("option", "columnSettings");
	gridColumn = $.grep(columnSettings, function (element, index) { return element.columnKey == 'ValorParametro' });
	if (gridColumn.count() > 0){
		gridColumn[0].editorOptions.textMode = textMode;
		$(control).igGridUpdating("option", "columnSettings", columnSettings);
	}
	
	var status = vm.status();

	//Valor Parâmetro
	var condition = (status === 'Q' || (status == 'E' && lxdatatype != 4))
	$(control).igGridHiding((condition ? 'showColumn' : 'hideColumn'), 'ValorParametro');

	//Valor Parâmetro Bool
	condition = (status == 'E' && lxdatatype == 4);
	$(control).igGridHiding((condition ? 'showColumn' : 'hideColumn'), 'ValorParametroBool');
}

$('.ui-iggrid-hiding-hiddencolumnindicator').hide();	
    }
    var OnSaving = function (changes) {
    if (changes.count() > 0 && jQuery.inArray(getAbsoluteValue(vm.currentDataItem().LxDatatypeParametro), [3, 4]) >= 0)  {
	changes.forEach(function (item) {
		if (item.typeName.toUpperCase() != "TCSPARAMETRO" && !item.isDeleted()) {
			if (item.typeName.toUpperCase() == "TCSPARAMETROVALOR"){
				item.ValorParametro(getAbsoluteValue(vm.currentDataItem().LxDatatypeParametro) == 3 ? convertDateToString(item.ValorParametroData()) : item.ValorParametroBool().toString() );
			}
			else
			{
				item.ValorParametro(getAbsoluteValue(vm.currentDataItem().LxDatatypeParametro) == 3 ? convertDateToString(new Date(item.ValorParametro())) : item.ValorParametro());
			}
		}
	});
}

return true;
    }
    var AdjustValorPadrao = function () {
    var control = $lx(vm, '#gbvalorPadrao');
var divVisible = (control.length && !control.hasClass('hide'));

control = $lx(vm, '#divValorParametro')
if(control.length){
	
      setVisibility(vm, 'cntValorParametro',(divVisible && jQuery.inArray(getAbsoluteValue(vm.currentDataItem().LxDatatypeParametro), [0, 1, 2]) >= 0));
    ;
	
      setVisibility(vm, 'cntValorParametroData',(divVisible && getAbsoluteValue(vm.currentDataItem().LxDatatypeParametro) == 3));
    ;
	
      setVisibility(vm, 'cntValorParametroBool',(divVisible && getAbsoluteValue(vm.currentDataItem().LxDatatypeParametro)== 4));
    ;
	
      setVisibility(vm, 'cntValorParametroMascara',(divVisible && getAbsoluteValue(vm.currentDataItem().LxDatatypeParametro) == 5));
    ;
}
    }
    var OnCancelled = function () {
    AdjustValorPadrao();
    }
    var OnSaved = function (changes) {
    AdjustValorPadrao();
//Força o refresh para resolver problema de não excluir a filha
//quando inclui -> salva -> altera -> exclui
//o registro some da UI mas não é removido da base de dados
refreshCurrentData();
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
        parentEntityRelated = null;
        isBusy(true);
        lastStatus = status();
        status('C');
        if (restoreLastFilter(lastStatus === 'C')) return clearComplete({ results: dataView() }, true);
        else return dataContext.clearTcsParametro(getBandeiraRede(), clearComplete);
    
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
    
    var createTcsParametro = function() {
        dataBind('dataView', true);
        var entity = dataContext.createTcsParametro();
        if(!entity) return null;
        adjustExternalParentRelation(entity);
        entity.setBandeiraRede(getBandeiraRede());
        entity.setGpecon(getGpecon());
        dataView.push(entity);
        return entity;
    };
    
    var createAndNotifyTcsParametro = function() {
        var entity = createTcsParametro();
        notifyPresentation('');
        return entity;
    };
    
    var createTcsParametroTabelaSelecao = function(parent, noCurrent) {
        dataBind('TcsParametroTabelaSelecaoList', true);
        var entity = dataContext.createTcsParametroTabelaSelecao(parent, noCurrent);
        if(!entity) return null;
        entity.setBandeiraRede(getBandeiraRede());
        entity.setGpecon(getGpecon());
       if ((noCurrent !== true) && !isNullOrEmpty(parent)) { parent.currentTcsParametroTabelaSelecao(entity); entity.fillDetails(); } 
        return entity;
    };
    
    var createAndNotifyTcsParametroTabelaSelecao = function(parent) {
        var entity = createTcsParametroTabelaSelecao(parent);
        notifyPresentation('TcsParametroTabelaSelecaoList');
        return entity;
    };
    
    var createTcsParametroValor = function(parent, noCurrent) {
        dataBind('TcsParametroValorList', true);
        var entity = dataContext.createTcsParametroValor(parent, noCurrent);
        if(!entity) return null;
        entity.setBandeiraRede(getBandeiraRede());
        entity.setGpecon(getGpecon());
       if ((noCurrent !== true) && !isNullOrEmpty(parent)) { parent.currentTcsParametroValor(entity); entity.fillDetails(); } 
        return entity;
    };
    
    var createAndNotifyTcsParametroValor = function(parent) {
        var entity = createTcsParametroValor(parent);
        notifyPresentation('TcsParametroValorList');
        return entity;
    };
    
    var createTcsParametroValorUsuario = function(parent, noCurrent) {
        dataBind('TcsParametroValorUsuarioList', true);
        var entity = dataContext.createTcsParametroValorUsuario(parent, noCurrent);
        if(!entity) return null;
        entity.setBandeiraRede(getBandeiraRede());
        entity.setGpecon(getGpecon());
        if (typeof entity.OnAdded == 'function') {
            entity.OnAdded();
        }
       if ((noCurrent !== true) && !isNullOrEmpty(parent)) { parent.currentTcsParametroValorUsuario(entity); entity.fillDetails(); } 
        return entity;
    };
    
    var createAndNotifyTcsParametroValorUsuario = function(parent) {
        var entity = createTcsParametroValorUsuario(parent);
        notifyPresentation('TcsParametroValorUsuarioList');
        return entity;
    };
    
    var createTcsParametroValorRede = function(parent, noCurrent) {
        dataBind('TcsParametroValorRedeList', true);
        var entity = dataContext.createTcsParametroValorRede(parent, noCurrent);
        if(!entity) return null;
        entity.setBandeiraRede(getBandeiraRede());
        entity.setGpecon(getGpecon());
        if (typeof entity.OnAdded == 'function') {
            entity.OnAdded();
        }
       if ((noCurrent !== true) && !isNullOrEmpty(parent)) { parent.currentTcsParametroValorRede(entity); entity.fillDetails(); } 
        return entity;
    };
    
    var createAndNotifyTcsParametroValorRede = function(parent) {
        var entity = createTcsParametroValorRede(parent);
        notifyPresentation('TcsParametroValorRedeList');
        return entity;
    };
    
    var createTcsParametroValorGpecon = function(parent, noCurrent) {
        dataBind('TcsParametroValorGpeconList', true);
        var entity = dataContext.createTcsParametroValorGpecon(parent, noCurrent);
        if(!entity) return null;
        entity.setBandeiraRede(getBandeiraRede());
        entity.setGpecon(getGpecon());
        if (typeof entity.OnAdded == 'function') {
            entity.OnAdded();
        }
       if ((noCurrent !== true) && !isNullOrEmpty(parent)) { parent.currentTcsParametroValorGpecon(entity); entity.fillDetails(); } 
        return entity;
    };
    
    var createAndNotifyTcsParametroValorGpecon = function(parent) {
        var entity = createTcsParametroValorGpecon(parent);
        notifyPresentation('TcsParametroValorGpeconList');
        return entity;
    };
    
    var createTcsParametroValorFilial = function(parent, noCurrent) {
        dataBind('TcsParametroValorFilialList', true);
        var entity = dataContext.createTcsParametroValorFilial(parent, noCurrent);
        if(!entity) return null;
        entity.setBandeiraRede(getBandeiraRede());
        entity.setGpecon(getGpecon());
        if (typeof entity.OnAdded == 'function') {
            entity.OnAdded();
        }
       if ((noCurrent !== true) && !isNullOrEmpty(parent)) { parent.currentTcsParametroValorFilial(entity); entity.fillDetails(); } 
        return entity;
    };
    
    var createAndNotifyTcsParametroValorFilial = function(parent) {
        var entity = createTcsParametroValorFilial(parent);
        notifyPresentation('TcsParametroValorFilialList');
        return entity;
    };
    
    var createTcsParametroValorLjvLoja = function(parent, noCurrent) {
        dataBind('TcsParametroValorLjvLojaList', true);
        var entity = dataContext.createTcsParametroValorLjvLoja(parent, noCurrent);
        if(!entity) return null;
        entity.setBandeiraRede(getBandeiraRede());
        entity.setGpecon(getGpecon());
        if (typeof entity.OnAdded == 'function') {
            entity.OnAdded();
        }
       if ((noCurrent !== true) && !isNullOrEmpty(parent)) { parent.currentTcsParametroValorLjvLoja(entity); entity.fillDetails(); } 
        return entity;
    };
    
    var createAndNotifyTcsParametroValorLjvLoja = function(parent) {
        var entity = createTcsParametroValorLjvLoja(parent);
        notifyPresentation('TcsParametroValorLjvLojaList');
        return entity;
    };
    
    var createTcsParametroValorVariacaoGenerica = function(parent, noCurrent) {
        dataBind('TcsParametroValorVariacaoGenericaList', true);
        var entity = dataContext.createTcsParametroValorVariacaoGenerica(parent, noCurrent);
        if(!entity) return null;
        entity.setBandeiraRede(getBandeiraRede());
        entity.setGpecon(getGpecon());
        if (typeof entity.OnAdded == 'function') {
            entity.OnAdded();
        }
       if ((noCurrent !== true) && !isNullOrEmpty(parent)) { parent.currentTcsParametroValorVariacaoGenerica(entity); entity.fillDetails(); } 
        return entity;
    };
    
    var createAndNotifyTcsParametroValorVariacaoGenerica = function(parent) {
        var entity = createTcsParametroValorVariacaoGenerica(parent);
        notifyPresentation('TcsParametroValorVariacaoGenericaList');
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
        goToItem(createTcsParametro());
        editInnerUIs();
        showButtonsEditorTemplate();
        dataBind();
    };
    var remove = function () {
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
        var item;
        if (navigationByPage() || (viewType() === 'Secundary') || (!(pageCount() === 1 || pageSize() === 0 || currentPage() === 0))) {
            item = refresh(0, false);
        } else {
            item = goToIndex(0);
        }
        return item;
    };
    var goBack = function () {
        var item;
        if (navigationByPage() || (viewType() === 'Secundary') || (!(pageCount() === 1 || pageSize() === 0 || currentPage() === 0) && currentDataIndex() === 0)) {
            item = refresh(currentPage()-1, !navigationByPage());
        } else {
            item = goToIndex(currentDataIndex()-1);
        }
        return item;
    };
    var goForward = function () {
        var item;
        if (navigationByPage() || (viewType() === 'Secundary') || (!(pageCount() === 1 || pageSize() === 0 || currentPage() === (pageCount()-1)) && currentDataIndex() === (dataView().length-1))) {
            item = refresh(currentPage()+1, false);
        } else {
            item = goToIndex(currentDataIndex()+1);
        }
        return item;
    };
    var goLast = function() {
        var item;
        if (!navigationByPage() && (viewType() === 'Main') && (pageCount() === 1 || pageSize() === 0 || currentPage() === (pageCount()-1))) {
            item = goToIndex(dataView().length-1);
        } else {
            item = refresh(pageCount()-1, !navigationByPage() && (viewType() === 'Main'));
        }
        return item;
    };
    //Databar enable control
    var _canRefreshData = true, _canQuickSearch = true, _canAddNew = false, _canClear = true, _canCustomSearch = true, _canDelete = false, _canEdit = true, _canLayout = true, _canNavigate = true, _canPrint = true, _canSearch = true, _canExport = true, _noBusyLoading = false;
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
        changeFormView();
    };
    var adjustFormView = function () {
        if (!hasMainTopDataGrid() && (status() === 'E' || status() === 'C') && viewType() === 'Secundary') changeFormView();
    }
    var removeFormViewControl = function () {
        var front = $('#CadastroParametro_formViewer_front')[0];
        if (front) front.removeClassName('front');
        var back = $('#CadastroParametro_formViewer_back')[0];
        if (back) { back.removeClassName('back'); back.addClassName('hide'); }
    }
    var changeFormView = function () {
        if (hasMainTopDataGrid() || isChildVM()) return;
        var panel = $('#CadastroParametro_formViewer')[0];
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
        require(['viewmodels/shared/modalMultimidiaBatch'], function (modalMultimidiaBatch) {
            modalMultimidiaBatch.showModal(dataContext).then(function (r, data) { });
        });
    };
    
    var entitySearchRange = {
        predefinedFilters: ko.observableArray(managerPredefined.predefinedFilters),
            TcsParametroValorValorParametroData_typeRange: ko.observable('R'), TcsParametroValorValorParametroData_begin: ko.observable(null), TcsParametroValorValorParametroData_end: ko.observable(null), TcsParametroValorValorParametroData_predefFilter: ko.observableArray([]), TcsParametroValorValorParametroData_predefValue: ko.observable(null),
        TcsParametroNivelAcesso_begin: ko.observable(null), TcsParametroNivelAcesso_end: ko.observable(null),
        TcsParametroNivelAcessoEdicao_begin: ko.observable(null), TcsParametroNivelAcessoEdicao_end: ko.observable(null),
        TcsParametroDescGrupoParametro: ko.observable(null),
        TcsParametroDescricaoAplicativo: ko.observable(null),
        TcsParametroIdTcsAplicativo: ko.observable(null),
        TcsParametroTabelaSelecaoDescTabela: ko.observable(null),
        TcsParametroTabelaSelecaoNomeTabela: ko.observable(null),
        TcsParametroValorUsuarioNomeUsuario: ko.observable(null),
        TcsParametroValorRedeCodBandeiraRede: ko.observable(null),
        TcsParametroValorRedeDescBandeiraRede: ko.observable(null),
        TcsParametroValorGpeconDescGrupoEconomico: ko.observable(null),
        TcsParametroValorGpeconIdGpecon: ko.observable(null),
        TcsParametroValorFilialCodigoFilial: ko.observable(null),
        TcsParametroValorFilialNomeFilial: ko.observable(null),
        TcsParametroValorLjvLojaCodLoja: ko.observable(null),
        TcsParametroValorLjvLojaDescLoja: ko.observable(null),
        TcsParametroValorVariacaoGenericaNomeTabela: ko.observable(null)
    };
    entitySearchRange.clear = function(){
            entitySearchRange.TcsParametroValorValorParametroData_typeRange('R'); entitySearchRange.TcsParametroValorValorParametroData_begin(null); entitySearchRange.TcsParametroValorValorParametroData_end(null); entitySearchRange.TcsParametroValorValorParametroData_predefFilter([]); entitySearchRange.TcsParametroValorValorParametroData_predefValue(null);
        entitySearchRange.TcsParametroNivelAcesso_begin(null); entitySearchRange.TcsParametroNivelAcesso_end(null);
        entitySearchRange.TcsParametroNivelAcessoEdicao_begin(null); entitySearchRange.TcsParametroNivelAcessoEdicao_end(null);
        entitySearchRange.TcsParametroDescGrupoParametro(null);
        entitySearchRange.TcsParametroDescricaoAplicativo(null);
        entitySearchRange.TcsParametroIdTcsAplicativo(null);
        entitySearchRange.TcsParametroTabelaSelecaoDescTabela(null);
        entitySearchRange.TcsParametroTabelaSelecaoNomeTabela(null);
        entitySearchRange.TcsParametroValorUsuarioNomeUsuario(null);
        entitySearchRange.TcsParametroValorRedeCodBandeiraRede(null);
        entitySearchRange.TcsParametroValorRedeDescBandeiraRede(null);
        entitySearchRange.TcsParametroValorGpeconDescGrupoEconomico(null);
        entitySearchRange.TcsParametroValorGpeconIdGpecon(null);
        entitySearchRange.TcsParametroValorFilialCodigoFilial(null);
        entitySearchRange.TcsParametroValorFilialNomeFilial(null);
        entitySearchRange.TcsParametroValorLjvLojaCodLoja(null);
        entitySearchRange.TcsParametroValorLjvLojaDescLoja(null);
        entitySearchRange.TcsParametroValorVariacaoGenericaNomeTabela(null);
    };
    entitySearchRange.has_TcsParametroNivelAcesso = ko.computed(function(){ return (entitySearchRange.TcsParametroNivelAcesso_begin() != null || entitySearchRange.TcsParametroNivelAcesso_end() != null); });
    entitySearchRange.has_TcsParametroNivelAcessoEdicao = ko.computed(function(){ return (entitySearchRange.TcsParametroNivelAcessoEdicao_begin() != null || entitySearchRange.TcsParametroNivelAcessoEdicao_end() != null); });
    entitySearchRange.has_TcsParametroValorValorParametroData = ko.computed(function(){ return (entitySearchRange.TcsParametroValorValorParametroData_typeRange() == 'R' && (entitySearchRange.TcsParametroValorValorParametroData_begin() != null || entitySearchRange.TcsParametroValorValorParametroData_end() != null) || (entitySearchRange.TcsParametroValorValorParametroData_typeRange() == 'P' && entitySearchRange.TcsParametroValorValorParametroData_predefFilter().length > 0)); });
    
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
                       var control = $('#CadastroParametro_div' + (!dataView ? '' : '' + dataView + '_') + entry.key + 'Template');
                       if (!control.hasClass('hide') && !control.hasClass('onlyEditor'))
                           control.addClass('hide');
                   } else if (entry.key !== 'RowDataId') {
                       var control = $('#CadastroParametro_div' + (!dataView ? '' : '' + dataView + '_') + entry.key + 'Template');
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
            viewName: 'CadastroParametro',
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
            rootBmTypeName: '',
            rootDataTypeName: 'TcsParametro',
            rootNamespace: 'Linx.Framework.BV.Parametro',
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
            AdjustControls: AdjustControls,
            OnDataGridCreated: OnDataGridCreated,
            AdjustGrid: AdjustGrid,
            createTcsParametro: createTcsParametro,
            createAndNotifyTcsParametro: createAndNotifyTcsParametro,
            createTcsParametroTabelaSelecao: createTcsParametroTabelaSelecao,
            createAndNotifyTcsParametroTabelaSelecao: createAndNotifyTcsParametroTabelaSelecao,
            createTcsParametroValor: createTcsParametroValor,
            createAndNotifyTcsParametroValor: createAndNotifyTcsParametroValor,
            createTcsParametroValorUsuario: createTcsParametroValorUsuario,
            createAndNotifyTcsParametroValorUsuario: createAndNotifyTcsParametroValorUsuario,
            createTcsParametroValorRede: createTcsParametroValorRede,
            createAndNotifyTcsParametroValorRede: createAndNotifyTcsParametroValorRede,
            createTcsParametroValorGpecon: createTcsParametroValorGpecon,
            createAndNotifyTcsParametroValorGpecon: createAndNotifyTcsParametroValorGpecon,
            createTcsParametroValorFilial: createTcsParametroValorFilial,
            createAndNotifyTcsParametroValorFilial: createAndNotifyTcsParametroValorFilial,
            createTcsParametroValorLjvLoja: createTcsParametroValorLjvLoja,
            createAndNotifyTcsParametroValorLjvLoja: createAndNotifyTcsParametroValorLjvLoja,
            createTcsParametroValorVariacaoGenerica: createTcsParametroValorVariacaoGenerica,
            createAndNotifyTcsParametroValorVariacaoGenerica: createAndNotifyTcsParametroValorVariacaoGenerica,
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
            __moduleId__: 'pkg_linx-framework-bv-spa/viewmodels/CadastroParametro',
            pivots : pivots
        };
    
    dataContext.setCurrentViewModel(vm);
    return vm;
}

return vmInstance;
});
