/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/
define(['./linx-spa-configuracaoinicialusuario_custom_pt-br.js'],
	function (custom){
		var result = {};

       result.languageFile = function () {
       	return 'pt-br';
       }

result.objectLanguage_ConfiguracaoInicialUsuario = function () {
           var langResult = {
               Name: 'ConfiguracaoInicialUsuario', Items: [

	 {Name: "ConfiguracaoInicialUsuario_gbCustomContainer_185c4ad6b7f842098fbc87f20c9b34f3", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "ConfiguracaoInicialUsuario_cntTcsUsuarioConfiguracao", DisplayName: "TcsUsuarioConfiguracao", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "ConfiguracaoInicialUsuario_lUpIdLinx", DisplayName: "Id Linx", ColumnSpan: 2, Visible: true, LookUpName: "LookUpTcsEmpresaAutenticacao", Key: "IdLinx"},
	 {Name: "ConfiguracaoInicialUsuario_lUpNomeEmpresa", DisplayName: "Empresa", ColumnSpan: 9, Visible: true, LookUpName: "LookUpTcsEmpresaAutenticacao", Key: "NomeEmpresa"},
	 {Name: "ConfiguracaoInicialUsuario_lUpNomeUsuario", DisplayName: "Usuário", ColumnSpan: 7, Visible: true, LookUpName: "LookUpTcsUsuarioAutenticacao", Key: "NomeUsuario"},
	 {Name: "ConfiguracaoInicialUsuario_tbNomeAutenticacao", DisplayName: "Usuário Autenticação", ColumnSpan: 4, Visible: true, Key: "NomeAutenticacao"},
	 {Name: "ConfiguracaoInicialUsuario_lblCustomControl16785296", DisplayName: "", ColumnSpan: 3, Visible: true, Key: ""},
	 {Name: "ConfiguracaoInicialUsuario_btnCustomControl167994734", DisplayName: "Processar", ColumnSpan: 5, Visible: true, Key: ""},]},
	 {Name: "ConfiguracaoInicialUsuario_tcTcsUsuarioConfiguracaoTabControl", DisplayName: "TcsUsuarioConfiguracao", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "ConfiguracaoInicialUsuario_tiTcsUsuarioConfiguracaoAcessoTabItem", DisplayName: "Ambientes", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "ConfiguracaoInicialUsuario_dGridTcsUsuarioConfiguracaoAcesso", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "ConfiguracaoInicialUsuario_ckTcsUsuarioConfiguracaoAcesso_Selecionado", Name: "ConfiguracaoInicialUsuario_dGridTcsUsuarioConfiguracaoAcesso_Selecionado", DisplayName: "Selecionado", ColumnSpan: 1, Visible: true, Key: "Selecionado"},
	 {Id: "ConfiguracaoInicialUsuario_lUpTcsUsuarioConfiguracaoAcesso_DescricaoAmbiente", Name: "ConfiguracaoInicialUsuario_dGridTcsUsuarioConfiguracaoAcesso_DescricaoAmbiente", DisplayName: "Ambiente", ColumnSpan: 9, Visible: true, Key: "DescricaoAmbiente"},
	 {Id: "ConfiguracaoInicialUsuario_lUpTcsUsuarioConfiguracaoAcesso_DescricaoAplicacao", Name: "ConfiguracaoInicialUsuario_dGridTcsUsuarioConfiguracaoAcesso_DescricaoAplicacao", DisplayName: "Aplicação", ColumnSpan: 9, Visible: true, Key: "DescricaoAplicacao"},
	 {Id: "ConfiguracaoInicialUsuario_lUpTcsUsuarioConfiguracaoAcesso_DescricaoAplicativo", Name: "ConfiguracaoInicialUsuario_dGridTcsUsuarioConfiguracaoAcesso_DescricaoAplicativo", DisplayName: "Aplicativo", ColumnSpan: 9, Visible: true, Key: "DescricaoAplicativo"},
	 {Id: "ConfiguracaoInicialUsuario_ckTcsUsuarioConfiguracaoAcesso_IndicaAcessoPadrao", Name: "ConfiguracaoInicialUsuario_dGridTcsUsuarioConfiguracaoAcesso_IndicaAcessoPadrao", DisplayName: "Acesso Padrão", ColumnSpan: 8, Visible: true, Key: "IndicaAcessoPadrao"},
	 {Id: "ConfiguracaoInicialUsuario_ckTcsUsuarioConfiguracaoAcesso_IndicaAdministrador", Name: "ConfiguracaoInicialUsuario_dGridTcsUsuarioConfiguracaoAcesso_IndicaAdministrador", DisplayName: "Administrador", ColumnSpan: 8, Visible: true, Key: "IndicaAdministrador"},
	 {Id: "ConfiguracaoInicialUsuario_ckTcsUsuarioConfiguracaoAcesso_IndicaMultiGpecon", Name: "ConfiguracaoInicialUsuario_dGridTcsUsuarioConfiguracaoAcesso_IndicaMultiGpecon", DisplayName: "Multi Gpecon", ColumnSpan: 8, Visible: true, Key: "IndicaMultiGpecon"},
	 {Id: "ConfiguracaoInicialUsuario_tbTcsUsuarioConfiguracaoAcesso_UidAplicacao", Name: "ConfiguracaoInicialUsuario_dGridTcsUsuarioConfiguracaoAcesso_UidAplicacao", DisplayName: "Uid Aplicacao", ColumnSpan: 8, Visible: true, Key: "UidAplicacao"},]},]},]},]},               ]
           }
           langResult.Items.concat(custom.getCustomTranslation().Items);
           return langResult;
        };
       return result;
  }
)

