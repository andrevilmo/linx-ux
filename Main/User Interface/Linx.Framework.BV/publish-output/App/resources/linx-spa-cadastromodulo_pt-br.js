/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/
define(['./linx-spa-cadastromodulo_custom_pt-br.js'],
	function (custom){
		var result = {};

       result.languageFile = function () {
       	return 'pt-br';
       }

result.objectLanguage_CadastroModulo = function () {
           var langResult = {
               Name: 'CadastroModulo', Items: [

	 {Name: "CadastroModulo_gbExpander_36a3b66ca1ad446aa386c381b712fd76", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroModulo_gbGroupBox_f6354350d9694ea3927acfea9bac6f30", DisplayName: "Módulo", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroModulo_tbNomeCurto", DisplayName: "Descrição", ColumnSpan: 9, Visible: true, Key: "NomeCurto"},
	 {Name: "CadastroModulo_tbDescModulo", DisplayName: "Descrição Detalhada", ColumnSpan: 12, Visible: true, Key: "DescModulo"},
	 {Name: "CadastroModulo_tbDescricaoAplicativo", DisplayName: "Aplicativo", ColumnSpan: 12, Visible: true, Key: "DescricaoAplicativo"},
	 {Name: "CadastroModulo_tbOrdemNavegacao", DisplayName: "Ordem", ColumnSpan: 6, Visible: true, Key: "OrdemNavegacao"},
	 {Name: "CadastroModulo_ckInativo", DisplayName: "Inativo", ColumnSpan: 12, Visible: true, Key: "Inativo"},]},
	 {Name: "CadastroModulo_gbTcsModulo", DisplayName: "Apresentação", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroModulo_gbGroupBox_7c5905d8803e461fb260084ab18fb1d3", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroModulo_tbIcone", DisplayName: "Ícone", ColumnSpan: 6, Visible: true, Key: "Icone"},]},]},
	 {Name: "CadastroModulo_tcTcsModuloTabControl", DisplayName: "Módulos", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroModulo_tiTcsModuloMenuTabItem", DisplayName: "Menus", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroModulo_cntCustomContainer_3d3cb14ee7ac48c1b2fecc5d09415426", DisplayName: "New Group", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroModulo_dGridTcsModuloMenu", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroModulo_tbTcsModuloMenu_NomeCurto", Name: "CadastroModulo_dGridTcsModuloMenu_NomeCurto", DisplayName: "Descrição", ColumnSpan: 9, Visible: true, Key: "NomeCurto"},
	 {Id: "CadastroModulo_tbTcsModuloMenu_DescModuloMenu", Name: "CadastroModulo_dGridTcsModuloMenu_DescModuloMenu", DisplayName: "Descrição Detalhada", ColumnSpan: 9, Visible: true, Key: "DescModuloMenu"},
	 {Id: "CadastroModulo_tbTcsModuloMenu_OrdemNavegacao", Name: "CadastroModulo_dGridTcsModuloMenu_OrdemNavegacao", DisplayName: "Ordem", ColumnSpan: 3, Visible: true, Key: "OrdemNavegacao"},
	 {Id: "CadastroModulo_lUpTcsModuloMenu_DescModuloMenuSuperior", Name: "CadastroModulo_dGridTcsModuloMenu_DescModuloMenuSuperior", DisplayName: "Menu Superior", ColumnSpan: 9, Visible: true, LookUpName: "LookUpModuloMenuSuperior", Key: "DescModuloMenuSuperior"},
	 {Id: "CadastroModulo_tbTcsModuloMenu_Icone", Name: "CadastroModulo_dGridTcsModuloMenu_Icone", DisplayName: "Ícone", ColumnSpan: 9, Visible: false, Key: "Icone"},]},]},
	 {Name: "CadastroModulo_gbTransacoes", DisplayName: "Transações", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroModulo_dGridTcsTransacaoMenu", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroModulo_lUpTcsTransacaoMenu_DescTransacao", Name: "CadastroModulo_dGridTcsTransacaoMenu_DescTransacao", DisplayName: "Transação", ColumnSpan: 9, Visible: true, LookUpName: "LookUpTcsTransacaoMenu", Key: "DescTransacao"},
	 {Id: "CadastroModulo_tbTcsTransacaoMenu_OrdemNavegacao", Name: "CadastroModulo_dGridTcsTransacaoMenu_OrdemNavegacao", DisplayName: "Ordem", ColumnSpan: 3, Visible: true, Key: "OrdemNavegacao"},
	 {Id: "CadastroModulo_ckTcsTransacaoMenu_Inativo", Name: "CadastroModulo_dGridTcsTransacaoMenu_Inativo", DisplayName: "Inativo", ColumnSpan: 3, Visible: true, Key: "Inativo"},]},]},]},
	 {Name: "CadastroModulo_tiTcsModuloDoGrupoTabItem", DisplayName: "Grupo", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroModulo_cntCustomContainer_9b718b170b8b445887d8e4b6b12cb09e", DisplayName: "New Group", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroModulo_dGridTcsModuloDoGrupo", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroModulo_lUpTcsModuloDoGrupo_DescGrupoModulo", Name: "CadastroModulo_dGridTcsModuloDoGrupo_DescGrupoModulo", DisplayName: "Descrição", ColumnSpan: 9, Visible: true, LookUpName: "LookUpTcsModuloGrupo", Key: "DescGrupoModulo"},]},]},]},]},]},               ]
           }
           langResult.Items.concat(custom.getCustomTranslation().Items);
           return langResult;
        };
       return result;
  }
)

