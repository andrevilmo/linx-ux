/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/
define(['./linx-spa-exemploautocompleteteste_custom_pt-br.js'],
	function (custom){
		var result = {};

       result.languageFile = function () {
       	return 'pt-br';
       }

result.objectLanguage_ExemploAutoCompleteTeste = function () {
           var langResult = {
               Name: 'ExemploAutoCompleteTeste', Items: [

	 {Name: 'ExemploAutoCompleteTeste_gbTbnmcompleto', DisplayName: 'Tbnmcompleto', ColumnSpan: 12, Visible: true, Items: [
	 {Name: 'ExemploAutoCompleteTeste_ntxidNomeCompleto', DisplayName: 'id Nome Completo', ColumnSpan: 2, Visible: true, Key: 'idNomeCompleto'},
	 {Name: 'ExemploAutoCompleteTeste_lUpIdNome', DisplayName: 'Id Nome', ColumnSpan: 2, Visible: true, LookUpName: 'LookUpTbnome', Key: 'IdNome'},
	 {Name: 'ExemploAutoCompleteTeste_lUpidnomeMeio', DisplayName: 'id nomeMeio', ColumnSpan: 2, Visible: true, LookUpName: 'LookUpTbnmmeio', Key: 'idnomeMeio'},
	 {Name: 'ExemploAutoCompleteTeste_lUpIdSobrenome', DisplayName: 'Id Sobrenome', ColumnSpan: 2, Visible: true, LookUpName: 'LookUpTbsobrenm', Key: 'IdSobrenome'},
	 {Name: 'ExemploAutoCompleteTeste_lUpIdCliente', DisplayName: 'Id Cliente', ColumnSpan: 3, Visible: true, Key: 'IdCliente'},
	 {Name: 'ExemploAutoCompleteTeste_lUpNome', DisplayName: 'Nome', ColumnSpan: 3, Visible: true, LookUpName: 'LookUpTbnome', Key: 'Nome'},
	 {Name: 'ExemploAutoCompleteTeste_lUpNomedomeio', DisplayName: 'Nome do Meio', ColumnSpan: 3, Visible: true, LookUpName: 'LookUpTbnmmeio', Key: 'Nomedomeio'},
	 {Name: 'ExemploAutoCompleteTeste_lUpSobreNome', DisplayName: 'Sobre Nome', ColumnSpan: 3, Visible: true, LookUpName: 'LookUpTbsobrenm', Key: 'SobreNome'},
	 {Name: 'ExemploAutoCompleteTeste_tbNomeCompleto', DisplayName: 'NomeCompleto', ColumnSpan: 3, Visible: true, Key: 'NomeCompleto'},]},               ]
           }
           langResult.Items.concat(custom.getCustomTranslation().Items);
           return langResult;
        };
       return result;
  }
)

