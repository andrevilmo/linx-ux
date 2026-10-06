/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/
define(['./linx-spa-cadastroindicadormedida_custom_pt-br.js'],
	function (custom){
		var result = {};

       result.languageFile = function () {
       	return 'pt-br';
       }

result.objectLanguage_CadastroIndicadorMedida = function () {
           var langResult = {
               Name: 'CadastroIndicadorMedida', Items: [

	 {Name: "CadastroIndicadorMedida_gbTcsIndicadorMedida", DisplayName: "Indicador de Medida", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroIndicadorMedida_gbGroupBox_18a35f2eead34c09a53cb5c0cbd9eb10", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroIndicadorMedida_tbCodIndicadorMedida", DisplayName: "Código", ColumnSpan: 2, Visible: true, Key: "CodIndicadorMedida"},
	 {Name: "CadastroIndicadorMedida_tbDescIndicadorMedida", DisplayName: "Descrição", ColumnSpan: 8, Visible: true, Key: "DescIndicadorMedida"},]},
	 {Name: "CadastroIndicadorMedida_tcTcsIndicadorMedidaTabControl", DisplayName: "Indicador de Medidas", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroIndicadorMedida_tiTcsIndicadorIndiceTabItem", DisplayName: "Faixas", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroIndicadorMedida_dGridTcsIndicadorIndice", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroIndicadorMedida_tbTcsIndicadorIndice_CodIndiceMedida", Name: "CadastroIndicadorMedida_dGridTcsIndicadorIndice_CodIndiceMedida", DisplayName: "Código", ColumnSpan: 6, Visible: true, Key: "CodIndiceMedida"},
	 {Id: "CadastroIndicadorMedida_tbTcsIndicadorIndice_DescIndiceMedida", Name: "CadastroIndicadorMedida_dGridTcsIndicadorIndice_DescIndiceMedida", DisplayName: "Descrição", ColumnSpan: 9, Visible: true, Key: "DescIndiceMedida"},
	 {Id: "CadastroIndicadorMedida_ntxTcsIndicadorIndice_LimiteInferior", Name: "CadastroIndicadorMedida_dGridTcsIndicadorIndice_LimiteInferior", DisplayName: "Limite Inferior", ColumnSpan: 6, Visible: true, Key: "LimiteInferior"},
	 {Id: "CadastroIndicadorMedida_ntxTcsIndicadorIndice_LimiteSuperior", Name: "CadastroIndicadorMedida_dGridTcsIndicadorIndice_LimiteSuperior", DisplayName: "Limite Superior", ColumnSpan: 6, Visible: true, Key: "LimiteSuperior"},
	 {Id: "CadastroIndicadorMedida_TcsIndicadorIndice_Rgb", Name: "CadastroIndicadorMedida_dGridTcsIndicadorIndice_Rgb", DisplayName: "Cor", ColumnSpan: 5, Visible: true, Key: "Rgb"},
	 {Id: "CadastroIndicadorMedida_ckTcsIndicadorIndice_IndicaMedidaAlvo", Name: "CadastroIndicadorMedida_dGridTcsIndicadorIndice_IndicaMedidaAlvo", DisplayName: "Ativo", ColumnSpan: 3, Visible: true, Key: "IndicaMedidaAlvo"},]},]},]},]},               ]
           }
           langResult.Items.concat(custom.getCustomTranslation().Items);
           return langResult;
        };
       return result;
  }
)

