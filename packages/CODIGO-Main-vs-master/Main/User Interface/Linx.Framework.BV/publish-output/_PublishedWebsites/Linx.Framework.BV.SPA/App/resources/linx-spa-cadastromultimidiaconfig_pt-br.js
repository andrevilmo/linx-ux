/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/
define(['./linx-spa-cadastromultimidiaconfig_custom_pt-br.js'],
	function (custom){
		var result = {};

       result.languageFile = function () {
       	return 'pt-br';
       }

result.objectLanguage_CadastroMultimidiaConfig = function () {
           var langResult = {
               Name: 'CadastroMultimidiaConfig', Items: [

	 {Name: "CadastroMultimidiaConfig_gbCustomContainer_d3069a3cdca647179344a8ac037fb446", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroMultimidiaConfig_dGridDocMultimidiaConfig", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroMultimidiaConfig_lUpDescricaoAplicativo", Name: "CadastroMultimidiaConfig_dGridDocMultimidiaConfig_DescricaoAplicativo", DisplayName: "Aplicativo", ColumnSpan: 8, Visible: true, LookUpName: "LookUpTcsAplicativo", Key: "DescricaoAplicativo"},
	 {Id: "CadastroMultimidiaConfig_cmbLxUsoMultimidia", Name: "CadastroMultimidiaConfig_dGridDocMultimidiaConfig_LxUsoMultimidia", DisplayName: "Tipo de Uso", ColumnSpan: 6, Visible: true, Key: "LxUsoMultimidia"},
	 {Id: "CadastroMultimidiaConfig_ntxDocLargura", Name: "CadastroMultimidiaConfig_dGridDocMultimidiaConfig_DocLargura", DisplayName: "Largura", ColumnSpan: 5, Visible: true, Key: "DocLargura"},
	 {Id: "CadastroMultimidiaConfig_ntxDocAltura", Name: "CadastroMultimidiaConfig_dGridDocMultimidiaConfig_DocAltura", DisplayName: "Altura", ColumnSpan: 5, Visible: true, Key: "DocAltura"},
	 {Id: "CadastroMultimidiaConfig_tbDocDuracao", Name: "CadastroMultimidiaConfig_dGridDocMultimidiaConfig_DocDuracao", DisplayName: "Duração", ColumnSpan: 3, Visible: true, Key: "DocDuracao"},
	 {Id: "CadastroMultimidiaConfig_tbDocFormatoVisualizacao", Name: "CadastroMultimidiaConfig_dGridDocMultimidiaConfig_DocFormatoVisualizacao", DisplayName: "Formato da Visualização", ColumnSpan: 3, Visible: true, Key: "DocFormatoVisualizacao"},
	 {Id: "CadastroMultimidiaConfig_tbDocTamanho", Name: "CadastroMultimidiaConfig_dGridDocMultimidiaConfig_DocTamanho", DisplayName: "Tamanho", ColumnSpan: 3, Visible: true, Key: "DocTamanho"},]},]},               ]
           }
           langResult.Items.concat(custom.getCustomTranslation().Items);
           return langResult;
        };
       return result;
  }
)

