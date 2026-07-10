/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/
define(['./linx-spa-lkpespecializada_custom_pt-br.js'],
	function (custom){
		var result = {};

       result.languageFile = function () {
       	return 'pt-br';
       }

result.objectLanguage_LkpEspecializada = function () {
           var langResult = {
               Name: 'LkpEspecializada', Items: [

	 {Name: 'LkpEspecializada_gbCliente', DisplayName: 'Cliente', ColumnSpan: 12, Visible: true, Items: [
	 {Name: 'LkpEspecializada_ntxComboboxCliente', DisplayName: 'Combobox Cliente', ColumnSpan: 1, Visible: true, Key: 'ComboboxCliente'},
	 {Name: 'LkpEspecializada_dtDatetimeCliente', DisplayName: 'Datetime Cliente', ColumnSpan: 3, Visible: true, Key: 'DatetimeCliente'},
	 {Name: 'LkpEspecializada_ntxDecimalCliente', DisplayName: 'Decimal Cliente', ColumnSpan: 5, Visible: true, Key: 'DecimalCliente'},
	 {Name: 'LkpEspecializada_ntxIdCliente', DisplayName: 'Id Cliente', ColumnSpan: 8, Visible: true, Key: 'IdCliente'},
	 {Name: 'LkpEspecializada_lUpIdEstado', DisplayName: 'Id Estado', ColumnSpan: 8, Visible: true, LookUpName: 'LookUpEstado', Key: 'IdEstado'},
	 {Name: 'LkpEspecializada_ntxIntCliente', DisplayName: 'Int Cliente', ColumnSpan: 8, Visible: true, Key: 'IntCliente'},
	 {Name: 'LkpEspecializada_tbSmallIntCliente', DisplayName: 'Small Int Cliente', ColumnSpan: 8, Visible: true, Key: 'SmallIntCliente'},
	 {Name: 'LkpEspecializada_tbIdNmCompleto', DisplayName: 'Id nome completo', ColumnSpan: 2, Visible: true, Key: 'IdNmCompleto'},
	 {Name: 'LkpEspecializada_lUpStringCliente', DisplayName: 'String Cliente', ColumnSpan: 9, Visible: true, LookUpName: 'LkpTbnmcompleto', Key: 'StringCliente'},
	 {Name: 'LkpEspecializada_tbStringEstado', DisplayName: 'String Estado', ColumnSpan: 9, Visible: true, Key: 'StringEstado'},]},               ]
           }
           langResult.Items.concat(custom.getCustomTranslation().Items);
           return langResult;
        };
       return result;
  }
)

