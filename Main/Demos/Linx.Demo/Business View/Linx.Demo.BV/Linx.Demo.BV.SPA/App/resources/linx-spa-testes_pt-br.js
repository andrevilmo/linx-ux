/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/
define(['./linx-spa-testes_custom_pt-br.js'],
	function (custom){
		var result = {};

       result.languageFile = function () {
       	return 'pt-br';
       }

result.objectLanguage_testes = function () {
           var langResult = {
               Name: 'testes', Items: [

	 {Name: 'testes_gbProduto', DisplayName: 'Produto', ColumnSpan: 12, Visible: true, Items: [
	 {Name: 'testes_dtDataCadastro', DisplayName: 'Data Cadastro', ColumnSpan: 3, Visible: true, Key: 'DataCadastro'},
	 {Name: 'testes_tbDescProduto', DisplayName: 'Desc Produto', ColumnSpan: 9, Visible: true, Key: 'DescProduto'},
	 {Name: 'testes_ntxIdProduto', DisplayName: 'Id Produto', ColumnSpan: 8, Visible: true, Key: 'IdProduto'},
	 {Name: 'testes_tbNmProduto', DisplayName: 'Nm Produto', ColumnSpan: 9, Visible: true, Key: 'NmProduto'},
	 {Name: 'testes_ntxTipoUnidade', DisplayName: 'Tipo Unidade', ColumnSpan: 1, Visible: true, Key: 'TipoUnidade'},
	 {Name: 'testes_btnteste', DisplayName: 'teste', ColumnSpan: 2, Visible: true, Key: ''},]},               ]
           }
           langResult.Items.concat(custom.getCustomTranslation().Items);
           return langResult;
        };
       return result;
  }
)

