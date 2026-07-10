/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/
define(['./linx-spa-marcasfamosas_custom_pt-br.js'],
	function (custom){
		var result = {};

       result.languageFile = function () {
       	return 'pt-br';
       }

result.objectLanguage_MarcasFamosas = function () {
           var langResult = {
               Name: 'MarcasFamosas', Items: [

	 {Name: 'MarcasFamosas_gbTesteCkbView', DisplayName: 'TesteCkbView', ColumnSpan: 12, Visible: true, Items: [
	 {Name: 'MarcasFamosas_ntxIdQualquer', DisplayName: 'Id Qualquer', ColumnSpan: 2, Visible: true, Key: 'IdQualquer'},
	 {Name: 'MarcasFamosas_ckNaoObrigatorio', DisplayName: 'NaoObrigatorio', ColumnSpan: 3, Visible: true, Key: 'NaoObrigatorio'},
	 {Name: 'MarcasFamosas_ckObrigatorio', DisplayName: 'Obrigatorio', ColumnSpan: 3, Visible: true, Key: 'Obrigatorio'},]},               ]
           }
           langResult.Items.concat(custom.getCustomTranslation().Items);
           return langResult;
        };
       return result;
  }
)

