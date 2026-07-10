/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/
define(['./linx-spa-cadastroaplicativo_custom_pt-br.js'],
	function (custom){
		var result = {};

       result.languageFile = function () {
       	return 'pt-br';
       }

result.objectLanguage_CadastroAplicativo = function () {
           var langResult = {
               Name: 'CadastroAplicativo', Items: [

	 {Name: "CadastroAplicativo_gbTcsAplicativo", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroAplicativo_gbGroupBox_c25fbe2e13704a0cb100f720c19c2acb", DisplayName: "Aplicativo", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroAplicativo_tbIdTcsAplicativo", DisplayName: "ID Aplicativo", ColumnSpan: 2, Visible: true, Key: "IdTcsAplicativo"},
	 {Name: "CadastroAplicativo_tbDescricaoAplicativo", DisplayName: "Descrição", ColumnSpan: 10, Visible: true, Key: "DescricaoAplicativo"},]},
	 {Name: "CadastroAplicativo_tcTcsAplicativoTabControl", DisplayName: "TcsAplicativo", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroAplicativo_tiTcsAplicativoConexaoTabItem", DisplayName: "Providers - BM", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroAplicativo_dGridTcsAplicativoConexao", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroAplicativo_lUpTcsAplicativoConexao_NomeConexao", Name: "CadastroAplicativo_dGridTcsAplicativoConexao_NomeConexao", DisplayName: "Nome Provider BM", ColumnSpan: 9, Visible: true, LookUpName: "LookUpTcsConexaoDb", Key: "NomeConexao"},]},]},
	 {Name: "CadastroAplicativo_tiTcsAplicacaoTabItem", DisplayName: "Aplicações Relacionadas", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroAplicativo_dGridTcsAplicacao", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroAplicativo_lUpTcsAplicacao_DescricaoAplicacao", Name: "CadastroAplicativo_dGridTcsAplicacao_DescricaoAplicacao", DisplayName: "Aplicação", ColumnSpan: 9, Visible: true, LookUpName: "LookUpTcsAplicacao", Key: "DescricaoAplicacao"},
	 {Id: "CadastroAplicativo_ckTcsAplicacao_EmDesenvolvimento", Name: "CadastroAplicativo_dGridTcsAplicacao_EmDesenvolvimento", DisplayName: "Em Desenvolvimento", ColumnSpan: 9, Visible: true, Key: "EmDesenvolvimento"},
	 {Id: "CadastroAplicativo_tbTcsAplicacao_Url", Name: "CadastroAplicativo_dGridTcsAplicacao_Url", DisplayName: "Url", ColumnSpan: 9, Visible: true, Key: "Url"},
	 {Id: "CadastroAplicativo_tbTcsAplicacao_UrlWorkArea", Name: "CadastroAplicativo_dGridTcsAplicacao_UrlWorkArea", DisplayName: "Url Work Area", ColumnSpan: 9, Visible: true, Key: "UrlWorkArea"},]},]},]},]},               ]
           }
           langResult.Items.concat(custom.getCustomTranslation().Items);
           return langResult;
        };
       return result;
  }
)

