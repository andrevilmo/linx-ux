/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/
define(['./linx-spa-exautocomplete_custom_pt-br.js'],
	function (custom){
		var result = {};

       result.languageFile = function () {
       	return 'pt-br';
       }

result.objectLanguage_ExAutoComplete = function () {
           var langResult = {
               Name: 'ExAutoComplete', Items: [

	 {Name: 'ExAutoComplete_gbTbnmcompleto', DisplayName: 'Tbnmcompleto', ColumnSpan: 12, Visible: true, Items: [
	 {Name: 'ExAutoComplete_ntxidNomeCompleto', DisplayName: 'id Nome Completo', ColumnSpan: 8, Visible: true, Key: 'idNomeCompleto'},
	 {Name: 'ExAutoComplete_tbNomeCompleto', DisplayName: 'NomeCompleto', ColumnSpan: 9, Visible: true, Key: 'NomeCompleto'},
	 {Name: 'ExAutoComplete_lUpIdNome', DisplayName: 'Id Nome', ColumnSpan: 8, Visible: true, LookUpName: 'LookUpTbnome', Key: 'IdNome'},
	 {Name: 'ExAutoComplete_lUpidnomeMeio', DisplayName: 'id nomeMeio', ColumnSpan: 8, Visible: true, LookUpName: 'LookUpTbnmmeio', Key: 'idnomeMeio'},
	 {Name: 'ExAutoComplete_lUpIdSobrenome', DisplayName: 'Id Sobrenome', ColumnSpan: 8, Visible: true, LookUpName: 'LookUpTbsobrenm', Key: 'IdSobrenome'},
	 {Name: 'ExAutoComplete_lUpNome', DisplayName: 'Nome', ColumnSpan: 9, Visible: true, LookUpName: 'LookUpTbnome', Key: 'Nome'},
	 {Name: 'ExAutoComplete_lUpNomedomeio', DisplayName: 'Nome do Meio', ColumnSpan: 9, Visible: true, LookUpName: 'LookUpTbnmmeio', Key: 'Nomedomeio'},
	 {Name: 'ExAutoComplete_lUpSobreNome', DisplayName: 'Sobre Nome', ColumnSpan: 9, Visible: true, LookUpName: 'LookUpTbsobrenm', Key: 'SobreNome'},
	 {Name: 'ExAutoComplete_tbIdCliente2', DisplayName: 'Id Cliente', ColumnSpan: 8, Visible: true, Key: 'IdCliente2'},]},               ]
           }
           langResult.Items.concat(custom.getCustomTranslation().Items);
           return langResult;
        };
       return result;
  }
)

