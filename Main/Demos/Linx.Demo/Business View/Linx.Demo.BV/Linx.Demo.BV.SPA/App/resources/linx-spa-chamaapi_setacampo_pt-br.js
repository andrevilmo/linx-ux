/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/
define(['./linx-spa-chamaapi_setacampo_custom_pt-br.js'],
	function (custom){
		var result = {};

       result.languageFile = function () {
       	return 'pt-br';
       }

result.objectLanguage_ChamaAPI_SetaCampo = function () {
           var langResult = {
               Name: 'ChamaAPI_SetaCampo', Items: [

	 {Name: 'ChamaAPI_SetaCampo_gbProduto', DisplayName: 'Produto', ColumnSpan: 12, Visible: true, Items: [
	 {Name: 'ChamaAPI_SetaCampo_ntxIdProduto', DisplayName: 'Id Produto', ColumnSpan: 2, Visible: true, Key: 'IdProduto'},
	 {Name: 'ChamaAPI_SetaCampo_tbNmProduto', DisplayName: 'Nm Produto', ColumnSpan: 3, Visible: true, Key: 'NmProduto'},
	 {Name: 'ChamaAPI_SetaCampo_tbDescProduto', DisplayName: 'Desc Produto', ColumnSpan: 4, Visible: true, Key: 'DescProduto'},
	 {Name: 'ChamaAPI_SetaCampo_dtDataCadastro', DisplayName: 'Data Cadastro', ColumnSpan: 2, Visible: true, Key: 'DataCadastro'},
	 {Name: 'ChamaAPI_SetaCampo_ntxTipoUnidade', DisplayName: 'Tipo Unidade', ColumnSpan: 1, Visible: true, Key: 'TipoUnidade'},
	 {Name: 'ChamaAPI_SetaCampo_btnGetProduto', DisplayName: 'Get Produto', ColumnSpan: 2, Visible: true, Key: ''},
	 {Name: 'ChamaAPI_SetaCampo_tbDescEstoque', DisplayName: 'Descrição Estoque', ColumnSpan: 4, Visible: true, Key: 'DescEstoque'},
	 {Name: 'ChamaAPI_SetaCampo_tbQtdeEstoque', DisplayName: 'QTDE', ColumnSpan: 2, Visible: true, Key: 'QtdeEstoque'},
	 {Name: 'ChamaAPI_SetaCampo_tbDtEntrada', DisplayName: 'Dt Entrada', ColumnSpan: 3, Visible: true, Key: 'DtEntrada'},]},               ]
           }
           langResult.Items.concat(custom.getCustomTranslation().Items);
           return langResult;
        };
       return result;
  }
)

