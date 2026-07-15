/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/
define(['./linx-spa-cadastromoduloautorizacao_custom_pt-br.js'],
	function (custom){
		var result = {};

       result.languageFile = function () {
       	return 'pt-br';
       }

result.objectLanguage_CadastroModuloAutorizacao = function () {
           var langResult = {
               Name: 'CadastroModuloAutorizacao', Items: [

	 {Name: "CadastroModuloAutorizacao_gbTcsModuloAutorizacao", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroModuloAutorizacao_gbGroupBox_c0b8bc792f164f398090696799a2470c", DisplayName: "Módulo", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroModuloAutorizacao_tbNomeCurto", DisplayName: "Descrição", ColumnSpan: 9, Visible: true, Key: "NomeCurto"},
	 {Name: "CadastroModuloAutorizacao_tbDescModulo", DisplayName: "Descrição Detalhada", ColumnSpan: 12, Visible: true, Key: "DescModulo"},
	 {Name: "CadastroModuloAutorizacao_lUpDescricaoAplicativo", DisplayName: "Aplicativo", ColumnSpan: 12, Visible: true, LookUpName: "LookUpTcsAplicativo", Key: "DescricaoAplicativo"},
	 {Name: "CadastroModuloAutorizacao_tbOrdemNavegacao", DisplayName: "Ordem", ColumnSpan: 6, Visible: true, Key: "OrdemNavegacao"},
	 {Name: "CadastroModuloAutorizacao_ckInativo", DisplayName: "Inativo", ColumnSpan: 12, Visible: true, Key: "Inativo"},]},
	 {Name: "CadastroModuloAutorizacao_gbGroupBox_cde924776f874d849b78af2e55a7e3ce", DisplayName: "Apresentação", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroModuloAutorizacao_gbGroupBox_7e5e81ccac754fa89ba3ab3087f62c6d", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroModuloAutorizacao_tbIcone", DisplayName: "Ícone", ColumnSpan: 6, Visible: true, Key: "Icone"},]},]},
	 {Name: "CadastroModuloAutorizacao_gbGroupBox_fd3c96eb987045b89134e4f1a58c3060", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroModuloAutorizacao_gbGroupBox_7e426df72b1e495badb47af2107b0621", DisplayName: "Menus", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroModuloAutorizacao_cntCustomContainer_b5cb1fd9da5f4ad4968c954e86e7bed4", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroModuloAutorizacao_dGridTcsModuloMenuAutorizacao", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroModuloAutorizacao_tbTcsModuloMenuAutorizacao_NomeCurto", Name: "CadastroModuloAutorizacao_dGridTcsModuloMenuAutorizacao_NomeCurto", DisplayName: "Descrição", ColumnSpan: 9, Visible: true, Key: "NomeCurto"},
	 {Id: "CadastroModuloAutorizacao_tbTcsModuloMenuAutorizacao_DescModuloMenu", Name: "CadastroModuloAutorizacao_dGridTcsModuloMenuAutorizacao_DescModuloMenu", DisplayName: "Descrição Detalhada", ColumnSpan: 9, Visible: true, Key: "DescModuloMenu"},
	 {Id: "CadastroModuloAutorizacao_ntxTcsModuloMenuAutorizacao_OrdemNavegacao", Name: "CadastroModuloAutorizacao_dGridTcsModuloMenuAutorizacao_OrdemNavegacao", DisplayName: "Ordem", ColumnSpan: 1, Visible: true, Key: "OrdemNavegacao"},
	 {Id: "CadastroModuloAutorizacao_lUpTcsModuloMenuAutorizacao_DescModuloMenuSuperior", Name: "CadastroModuloAutorizacao_dGridTcsModuloMenuAutorizacao_DescModuloMenuSuperior", DisplayName: "Menu Superior", ColumnSpan: 9, Visible: true, LookUpName: "LookUpModuloMenuSuperior", Key: "DescModuloMenuSuperior"},
	 {Id: "CadastroModuloAutorizacao_tbTcsModuloMenuAutorizacao_Icone", Name: "CadastroModuloAutorizacao_dGridTcsModuloMenuAutorizacao_Icone", DisplayName: "Ícone", ColumnSpan: 9, Visible: false, Key: "Icone"},]},]},
	 {Name: "CadastroModuloAutorizacao_gbGroupBox_d78a1316fb294d509b9a765e8b559964", DisplayName: "Transações", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroModuloAutorizacao_dGridTcsTransacaoMenuAutorizacaoModulo", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroModuloAutorizacao_lUpTcsTransacaoMenuAutorizacaoModulo_DescTransacao", Name: "CadastroModuloAutorizacao_dGridTcsTransacaoMenuAutorizacaoModulo_DescTransacao", DisplayName: "Transação", ColumnSpan: 9, Visible: true, LookUpName: "LookUpTcsTransacaoAutorizacao", Key: "DescTransacao"},
	 {Id: "CadastroModuloAutorizacao_ntxTcsTransacaoMenuAutorizacaoModulo_OrdemNavegacao", Name: "CadastroModuloAutorizacao_dGridTcsTransacaoMenuAutorizacaoModulo_OrdemNavegacao", DisplayName: "Ordem", ColumnSpan: 1, Visible: true, Key: "OrdemNavegacao"},
	 {Id: "CadastroModuloAutorizacao_ckTcsTransacaoMenuAutorizacaoModulo_Inativo", Name: "CadastroModuloAutorizacao_dGridTcsTransacaoMenuAutorizacaoModulo_Inativo", DisplayName: "Inativo", ColumnSpan: 3, Visible: true, Key: "Inativo"},]},]},]},]},]},               ]
           }
           langResult.Items.concat(custom.getCustomTranslation().Items);
           return langResult;
        };
       return result;
  }
)

