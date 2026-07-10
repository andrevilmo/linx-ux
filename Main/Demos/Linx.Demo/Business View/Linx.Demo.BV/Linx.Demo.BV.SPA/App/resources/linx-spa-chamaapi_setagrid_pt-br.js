/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/
define(['./linx-spa-chamaapi_setagrid_custom_pt-br.js'],
	function (custom){
		var result = {};

       result.languageFile = function () {
       	return 'pt-br';
       }

result.objectLanguage_ChamaAPI_SetaGrid = function () {
           var langResult = {
               Name: 'ChamaAPI_SetaGrid', Items: [

	 {Name: 'ChamaAPI_SetaGrid_gbProduto', DisplayName: 'Produto', ColumnSpan: 12, Visible: true, Items: [
	 {Name: 'ChamaAPI_SetaGrid_ntxIdProduto', DisplayName: 'Id Produto', ColumnSpan: 2, Visible: true, Key: 'IdProduto'},
	 {Name: 'ChamaAPI_SetaGrid_tbNmProduto', DisplayName: 'Nm Produto', ColumnSpan: 3, Visible: true, Key: 'NmProduto'},
	 {Name: 'ChamaAPI_SetaGrid_tbDescProduto', DisplayName: 'Desc Produto', ColumnSpan: 4, Visible: true, Key: 'DescProduto'},
	 {Name: 'ChamaAPI_SetaGrid_dtDataCadastro', DisplayName: 'Data Cadastro', ColumnSpan: 3, Visible: true, Key: 'DataCadastro'},
	 {Name: 'ChamaAPI_SetaGrid_ntxTipoUnidade', DisplayName: 'Tipo Unidade', ColumnSpan: 2, Visible: true, Key: 'TipoUnidade'},
	 {Name: 'ChamaAPI_SetaGrid_btnCarregaEstoque', DisplayName: 'Carregar Estoque', ColumnSpan: 2, Visible: true, Key: ''},
	 {Name: 'ChamaAPI_SetaGrid_btnCarregaEstoque2', DisplayName: 'CarregaEstoque 2', ColumnSpan: 2, Visible: true, Key: ''},
	 {Name: 'ChamaAPI_SetaGrid_btnCarregaEstoque3', DisplayName: 'CarregaEstoque 3', ColumnSpan: 2, Visible: true, Key: ''},]},
	 {Name: 'ChamaAPI_SetaGrid_gbExpander_f524ac80d5064adfaef08a710b431310', DisplayName: 'Estoque', ColumnSpan: 12, Visible: true, Items: [
	 {Name: 'ChamaAPI_SetaGrid_dGridestoque', DisplayName: 'Estoque', ColumnSpan: 12, Visible: true, Items: [
	 {Id: 'ChamaAPI_SetaGrid_tbDescEstoque', Name: 'ChamaAPI_SetaGrid_dGridestoque_DescEstoque', DisplayName: 'Descrição Estoque', ColumnSpan: 5, Visible: true, Key: 'DescEstoque'},
	 {Id: 'ChamaAPI_SetaGrid_tbQtdeEstoque', Name: 'ChamaAPI_SetaGrid_dGridestoque_QtdeEstoque', DisplayName: 'QTDE', ColumnSpan: 2, Visible: true, Key: 'QtdeEstoque'},
	 {Id: 'ChamaAPI_SetaGrid_dtDtEntrada', Name: 'ChamaAPI_SetaGrid_dGridestoque_DtEntrada', DisplayName: 'DT Entrada', ColumnSpan: 3, Visible: true, Key: 'DtEntrada'},]},]},               ]
           }
           langResult.Items.concat(custom.getCustomTranslation().Items);
           return langResult;
        };
       return result;
  }
)

