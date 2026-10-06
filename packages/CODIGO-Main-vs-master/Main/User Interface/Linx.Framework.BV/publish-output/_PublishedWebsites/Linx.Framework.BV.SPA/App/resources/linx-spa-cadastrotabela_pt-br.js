/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/
define(['./linx-spa-cadastrotabela_custom_pt-br.js'],
	function (custom){
		var result = {};

       result.languageFile = function () {
       	return 'pt-br';
       }

result.objectLanguage_CadastroTabela = function () {
           var langResult = {
               Name: 'CadastroTabela', Items: [

	 {Name: "CadastroTabela_dGridTcsTabelaAutorizacao", DisplayName: "Tabela", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroTabela_tbNomeTabela", Name: "CadastroTabela_dGridTcsTabelaAutorizacao_NomeTabela", DisplayName: "Nome Tabela", ColumnSpan: 9, Visible: true, Key: "NomeTabela"},
	 {Id: "CadastroTabela_tbDescTabela", Name: "CadastroTabela_dGridTcsTabelaAutorizacao_DescTabela", DisplayName: "Descrição", ColumnSpan: 9, Visible: true, Key: "DescTabela"},
	 {Id: "CadastroTabela_ckTabelaAutorizacao", Name: "CadastroTabela_dGridTcsTabelaAutorizacao_TabelaAutorizacao", DisplayName: "Tabela Banco Autorização", ColumnSpan: 8, Visible: true, Key: "TabelaAutorizacao"},]},               ]
           }
           langResult.Items.concat(custom.getCustomTranslation().Items);
           return langResult;
        };
       return result;
  }
)

