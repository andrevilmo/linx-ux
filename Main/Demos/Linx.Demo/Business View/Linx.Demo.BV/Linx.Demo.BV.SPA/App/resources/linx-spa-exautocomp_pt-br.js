/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/
define(['./linx-spa-exautocomp_custom_pt-br.js'],
	function (custom){
		var result = {};

       result.languageFile = function () {
       	return 'pt-br';
       }

result.objectLanguage_ExAutoComp = function () {
           var langResult = {
               Name: 'ExAutoComp', Items: [

	 {Name: 'ExAutoComp_gbTbnmcompleto', DisplayName: 'Tbnmcompleto', ColumnSpan: 12, Visible: true, Items: [
	 {Name: 'ExAutoComp_ntxidNomeCompleto', DisplayName: 'id Nome Completo', ColumnSpan: 8, Visible: true, Key: 'idNomeCompleto'},
	 {Name: 'ExAutoComp_tbNomeCompleto', DisplayName: 'NomeCompleto', ColumnSpan: 9, Visible: true, Key: 'NomeCompleto'},
	 {Name: 'ExAutoComp_lUpIdNome', DisplayName: 'Id Nome', ColumnSpan: 8, Visible: true, LookUpName: 'LookUpTbnome', Key: 'IdNome'},
	 {Name: 'ExAutoComp_lUpidnomeMeio', DisplayName: 'id nomeMeio', ColumnSpan: 8, Visible: true, LookUpName: 'LookUpTbnmmeio', Key: 'idnomeMeio'},
	 {Name: 'ExAutoComp_lUpIdSobrenome', DisplayName: 'Id Sobrenome', ColumnSpan: 8, Visible: true, LookUpName: 'LookUpTbsobrenm', Key: 'IdSobrenome'},
	 {Name: 'ExAutoComp_lUpNome', DisplayName: 'Nome', ColumnSpan: 9, Visible: true, LookUpName: 'LookUpTbnome', Key: 'Nome'},
	 {Name: 'ExAutoComp_lUpNomedomeio', DisplayName: 'Nome do Meio', ColumnSpan: 9, Visible: true, LookUpName: 'LookUpTbnmmeio', Key: 'Nomedomeio'},
	 {Name: 'ExAutoComp_lUpSobreNome', DisplayName: 'Sobre Nome', ColumnSpan: 9, Visible: true, LookUpName: 'LookUpTbsobrenm', Key: 'SobreNome'},
	 {Name: 'ExAutoComp_ntxIdCliente', DisplayName: 'Id__Cliente', ColumnSpan: 8, Visible: true, Key: 'IdCliente'},]},               ]
           }
           langResult.Items.concat(custom.getCustomTranslation().Items);
           return langResult;
        };
       return result;
  }
)

