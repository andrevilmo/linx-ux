/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/
define(['./linx-spa-cadastroservico_custom_pt-br.js'],
	function (custom){
		var result = {};

       result.languageFile = function () {
       	return 'pt-br';
       }

result.objectLanguage_CadastroServico = function () {
           var langResult = {
               Name: 'CadastroServico', Items: [

	 {Name: "CadastroServico_dGridTcsServico", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroServico_tbNomeServico", Name: "CadastroServico_dGridTcsServico_NomeServico", DisplayName: "Nome Serviço / Controlador", ColumnSpan: 9, Visible: true, Key: "NomeServico"},]},               ]
           }
           langResult.Items.concat(custom.getCustomTranslation().Items);
           return langResult;
        };
       return result;
  }
)

