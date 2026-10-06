/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/
define(['./linx-spa-cadastroobjeto_custom_pt-br.js'],
	function (custom){
		var result = {};

       result.languageFile = function () {
       	return 'pt-br';
       }

result.objectLanguage_CadastroObjeto = function () {
           var langResult = {
               Name: 'CadastroObjeto', Items: [

	 {Name: "CadastroObjeto_gbTcsObjeto", DisplayName: "Detalhes do Objeto", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroObjeto_gbGroupBox_2e6cd4302b40459f980a4f0ef37c3fd0", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroObjeto_tbClasseNome", DisplayName: "Classe", ColumnSpan: 12, Visible: true, Key: "ClasseNome"},
	 {Name: "CadastroObjeto_tbDescObjeto", DisplayName: "Descrição", ColumnSpan: 12, Visible: true, Key: "DescObjeto"},
	 {Name: "CadastroObjeto_cmbLxTipoObjeto", DisplayName: "Tipo Objeto", ColumnSpan: 12, Visible: true, Key: "LxTipoObjeto"},]},
	 {Name: "CadastroObjeto_tcTcsObjetoTabControl", DisplayName: "TcsObjeto", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroObjeto_tiTcsTransacaoTabItem", DisplayName: "Transação", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroObjeto_dGridTcsTransacao", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroObjeto_tbTcsTransacao_CodTransacao", Name: "CadastroObjeto_dGridTcsTransacao_CodTransacao", DisplayName: "Código", ColumnSpan: 3, Visible: true, Key: "CodTransacao"},
	 {Id: "CadastroObjeto_tbTcsTransacao_DescTransacao", Name: "CadastroObjeto_dGridTcsTransacao_DescTransacao", DisplayName: "Descrição", ColumnSpan: 9, Visible: true, Key: "DescTransacao"},
	 {Id: "CadastroObjeto_tbTcsTransacao_ClasseNome", Name: "CadastroObjeto_dGridTcsTransacao_ClasseNome", DisplayName: "Classe", ColumnSpan: 9, Visible: true, Key: "ClasseNome"},
	 {Id: "CadastroObjeto_ckTcsTransacao_Inativo", Name: "CadastroObjeto_dGridTcsTransacao_Inativo", DisplayName: "Inativo", ColumnSpan: 3, Visible: true, Key: "Inativo"},
	 {Id: "CadastroObjeto_cmbTcsTransacao_LxTipoTransacao", Name: "CadastroObjeto_dGridTcsTransacao_LxTipoTransacao", DisplayName: "Tipo transação", ColumnSpan: 6, Visible: true, Key: "LxTipoTransacao"},]},]},]},]},               ]
           }
           langResult.Items.concat(custom.getCustomTranslation().Items);
           return langResult;
        };
       return result;
  }
)

