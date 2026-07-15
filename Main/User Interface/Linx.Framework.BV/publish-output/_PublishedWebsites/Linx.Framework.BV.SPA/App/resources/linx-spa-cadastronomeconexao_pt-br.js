/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/
define(['./linx-spa-cadastronomeconexao_custom_pt-br.js'],
	function (custom){
		var result = {};

       result.languageFile = function () {
       	return 'pt-br';
       }

result.objectLanguage_CadastroNomeConexao = function () {
           var langResult = {
               Name: 'CadastroNomeConexao', Items: [

	 {Name: "CadastroNomeConexao_dGridTcsConexaoDb", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroNomeConexao_tbNomeConexao", Name: "CadastroNomeConexao_dGridTcsConexaoDb_NomeConexao", DisplayName: "Nome Provider BM", ColumnSpan: 9, Visible: true, Key: "NomeConexao"},]},               ]
           }
           langResult.Items.concat(custom.getCustomTranslation().Items);
           return langResult;
        };
       return result;
  }
)

