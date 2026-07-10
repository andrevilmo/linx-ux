/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/
define(['./linx-spa-exemploautocomplete_custom_pt-br.js'],
	function (custom){
		var result = {};

       result.languageFile = function () {
       	return 'pt-br';
       }

result.objectLanguage_ExemploAutoComplete = function () {
           var langResult = {
               Name: 'ExemploAutoComplete', Items: [

	 {Name: 'ExemploAutoComplete_gbTbnmcompleto', DisplayName: 'Tbnmcompleto', ColumnSpan: 12, Visible: true, Items: [
	 {Name: 'ExemploAutoComplete_ntxIdCliente', DisplayName: 'Id Cliente', ColumnSpan: 3, Visible: true, Key: 'IdCliente'},
	 {Name: 'ExemploAutoComplete_lUpIdNome', DisplayName: 'Id Nome', ColumnSpan: 3, Visible: true, LookUpName: 'LookUpTbnome', Key: 'IdNome'},
	 {Name: 'ExemploAutoComplete_lUpidnomeMeio', DisplayName: 'id nomeMeio', ColumnSpan: 3, Visible: true, LookUpName: 'LookUpTbnmmeio', Key: 'idnomeMeio'},
	 {Name: 'ExemploAutoComplete_lUpIdSobrenome', DisplayName: 'Id Sobrenome', ColumnSpan: 3, Visible: true, LookUpName: 'LookUpTbsobrenm', Key: 'IdSobrenome'},
	 {Name: 'ExemploAutoComplete_lUpNome', DisplayName: 'Nome', ColumnSpan: 3, Visible: true, LookUpName: 'LookUpTbnome', Key: 'Nome'},
	 {Name: 'ExemploAutoComplete_lUpNomedomeio', DisplayName: 'Nome do Meio', ColumnSpan: 3, Visible: true, LookUpName: 'LookUpTbnmmeio', Key: 'Nomedomeio'},
	 {Name: 'ExemploAutoComplete_lUpSobreNome', DisplayName: 'Sobre Nome', ColumnSpan: 3, Visible: true, LookUpName: 'LookUpTbsobrenm', Key: 'SobreNome'},
	 {Name: 'ExemploAutoComplete_tbNomeCompleto', DisplayName: 'NomeCompleto', ColumnSpan: 5, Visible: true, Key: 'NomeCompleto'},]},               ]
           }
           langResult.Items.concat(custom.getCustomTranslation().Items);
           return langResult;
        };
       return result;
  }
)

