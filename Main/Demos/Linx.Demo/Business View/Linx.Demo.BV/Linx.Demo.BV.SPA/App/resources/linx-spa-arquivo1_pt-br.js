/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/
define(['./linx-spa-arquivo1_custom_pt-br.js'],
	function (custom){
		var result = {};

       result.languageFile = function () {
       	return 'pt-br';
       }

result.objectLanguage_Arquivo1 = function () {
           var langResult = {
               Name: 'Arquivo1', Items: [

	 {Name: 'Arquivo1_dGridArquivo', DisplayName: 'DataGrid', ColumnSpan: 0, Visible: true, Items: [
	 {Id: 'Arquivo1_tbNomeArquivo', Name: 'Arquivo1_dGridArquivo_NomeArquivo', DisplayName: 'Nome Arquivo', ColumnSpan: 0, Visible: true, Key: 'NomeArquivo'},]},               ]
           }
           langResult.Items.concat(custom.getCustomTranslation().Items);
           return langResult;
        };
       return result;
  }
)

