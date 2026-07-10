/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/
define(['./linx-spa-perfilfranquia_custom_pt-br.js'],
	function (custom){
		var result = {};

       result.languageFile = function () {
       	return 'pt-br';
       }

result.objectLanguage_PerfilFranquia = function () {
           var langResult = {
               Name: 'PerfilFranquia', Items: [

	 {Name: "PerfilFranquia_gbTcsPerfilb162e82f59c5469d81881a40a7b2b9f6", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "PerfilFranquia_gbGroupBox_4fa922da0e014c4082a3acedb7f1640c", DisplayName: "Informações do Perfil", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "PerfilFranquia_tbIdPerfil", DisplayName: "Id", ColumnSpan: 1, Visible: true, Key: "IdPerfil"},
	 {Name: "PerfilFranquia_tbDescPerfil", DisplayName: "Descrição", ColumnSpan: 3, Visible: true, Key: "DescPerfil"},
	 {Name: "PerfilFranquia_ckInativo", DisplayName: "Inativo", ColumnSpan: 6, Visible: true, Key: "Inativo"},]},
	 {Name: "PerfilFranquia_gbExpander_4ecf554f5e81411882a771df50c9e16d", DisplayName: "Bandeira / Rede", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "PerfilFranquia_dGridTcsPerfilBandeiraRede", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "PerfilFranquia_lUpTcsPerfilBandeiraRede_DescBandeiraRede", Name: "PerfilFranquia_dGridTcsPerfilBandeiraRede_DescBandeiraRede", DisplayName: "Bandeira / Rede", ColumnSpan: 12, Visible: true, LookUpName: "LookUpTbcBandeiraRede", Key: "DescBandeiraRede"},]},]},
	 {Name: "PerfilFranquia_tcTcsPerfilTabControl", DisplayName: "TcsPerfil", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "PerfilFranquia_tiTcsPerfilRegraModuloTabItem", DisplayName: "Módulo", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "PerfilFranquia_dGridTcsPerfilRegraModulo", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "PerfilFranquia_lUpTcsPerfilRegraModulo_DescModulo", Name: "PerfilFranquia_dGridTcsPerfilRegraModulo_DescModulo", DisplayName: "Módulo", ColumnSpan: 8, Visible: true, LookUpName: "LookUpTcsPerfilRegraModulo", Key: "DescModulo"},
	 {Id: "PerfilFranquia_tbTcsPerfilRegraModulo_Origem", Name: "PerfilFranquia_dGridTcsPerfilRegraModulo_Origem", DisplayName: "Origem", ColumnSpan: 8, Visible: true, Key: "Origem"},
	 {Id: "PerfilFranquia_tbTcsPerfilRegraModulo_DescAplicativo", Name: "PerfilFranquia_dGridTcsPerfilRegraModulo_DescAplicativo", DisplayName: "Aplicativo", ColumnSpan: 8, Visible: true, Key: "DescAplicativo"},
	 {Id: "PerfilFranquia_lUpTcsPerfilRegraModulo_Acesso", Name: "PerfilFranquia_dGridTcsPerfilRegraModulo_Acesso", DisplayName: "Regra Acesso", ColumnSpan: 4, Visible: true, LookUpName: "LookUpLxRegraAcessoModulo", Key: "Acesso"},]},]},
	 {Name: "PerfilFranquia_tiTcsPerfilRegraTransacaoTabItem", DisplayName: "Transação", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "PerfilFranquia_dGridTcsPerfilRegraTransacao", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "PerfilFranquia_lUpTcsPerfilRegraTransacao_DescTransacao", Name: "PerfilFranquia_dGridTcsPerfilRegraTransacao_DescTransacao", DisplayName: "Transação", ColumnSpan: 9, Visible: true, LookUpName: "LookUpTcsPerfilRegraTransacao", Key: "DescTransacao"},
	 {Id: "PerfilFranquia_tbTcsPerfilRegraTransacao_Origem", Name: "PerfilFranquia_dGridTcsPerfilRegraTransacao_Origem", DisplayName: "Origem", ColumnSpan: 8, Visible: true, Key: "Origem"},
	 {Id: "PerfilFranquia_lUpTcsPerfilRegraTransacao_Acesso", Name: "PerfilFranquia_dGridTcsPerfilRegraTransacao_Acesso", DisplayName: "Regra Acesso", ColumnSpan: 4, Visible: true, LookUpName: "LookupLxRegraAcessoTransacao", Key: "Acesso"},]},]},
	 {Name: "PerfilFranquia_tiTcsPerfilFilialTabItem", DisplayName: "Filial", ColumnSpan: 12, Visible: false, Items: [
	 {Name: "PerfilFranquia_dGridTcsPerfilFilial", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "PerfilFranquia_lUpTcsPerfilFilial_CodigoFilial", Name: "PerfilFranquia_dGridTcsPerfilFilial_CodigoFilial", DisplayName: "Código Filial", ColumnSpan: 6, Visible: true, LookUpName: "LookUpTbcFilial", Key: "CodigoFilial"},
	 {Id: "PerfilFranquia_lUpTcsPerfilFilial_NomeFilial", Name: "PerfilFranquia_dGridTcsPerfilFilial_NomeFilial", DisplayName: "Nome Fantasia", ColumnSpan: 9, Visible: true, LookUpName: "LookUpTbcFilial", Key: "NomeFilial"},]},]},
	 {Name: "PerfilFranquia_tiTcsUsuarioPerfilTabItem", DisplayName: "Usuários Vinculados", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "PerfilFranquia_dGridTcsUsuarioPerfil", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "PerfilFranquia_lUpTcsUsuarioPerfil_NomeUsuario", Name: "PerfilFranquia_dGridTcsUsuarioPerfil_NomeUsuario", DisplayName: "Usuário", ColumnSpan: 9, Visible: true, LookUpName: "LookUpTcsUsuario", Key: "NomeUsuario"},]},]},]},]},               ]
           }
           langResult.Items.concat(custom.getCustomTranslation().Items);
           return langResult;
        };
       return result;
  }
)

