/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/
define(['./linx-spa-cadastrotransacaotag_custom_pt-br.js'],
	function (custom){
		var result = {};

       result.languageFile = function () {
       	return 'pt-br';
       }

result.objectLanguage_CadastroTransacaoTag = function () {
           var langResult = {
               Name: 'CadastroTransacaoTag', Items: [

	 {Name: "CadastroTransacaoTag_gbTcsTransacaoTag", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroTransacaoTag_gbGroupBox_bf6e59e1bb3c48dd85524806d6e0e8a3", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroTransacaoTag_lUpCodTransacao", DisplayName: "Código", ColumnSpan: 1, Visible: true, LookUpName: "LookUpTcsTransacao", Key: "CodTransacao"},
	 {Name: "CadastroTransacaoTag_lUpDescTransacao", DisplayName: "Transação", ColumnSpan: 5, Visible: true, LookUpName: "LookUpTcsTransacao", Key: "DescTransacao"},]},
	 {Name: "CadastroTransacaoTag_gbGroupBox_3d53a22226864440a338a7df937b1290", DisplayName: "Tags", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroTransacaoTag_CustomControl530884531", DisplayName: "", ColumnSpan: 12, Visible: true, Key: ""},]},]},               ]
           }
           langResult.Items.concat(custom.getCustomTranslation().Items);
           return langResult;
        };
       return result;
  }
)

