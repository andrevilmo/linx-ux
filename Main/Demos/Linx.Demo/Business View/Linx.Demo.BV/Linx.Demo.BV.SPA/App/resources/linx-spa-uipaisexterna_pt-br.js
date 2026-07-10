/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/
define(['./linx-spa-uipaisexterna_custom_pt-br.js'],
	function (custom){
		var result = {};

       result.languageFile = function () {
       	return 'pt-br';
       }

result.objectLanguage_UIPaisExterna = function () {
           var langResult = {
               Name: 'UIPaisExterna', Items: [

	 {Name: 'UIPaisExterna_gbPais', DisplayName: 'Pais', ColumnSpan: 0, Visible: true, Items: [
	 {Name: 'UIPaisExterna_cmbComboboxPais', DisplayName: 'Combobox Pais', ColumnSpan: 0, Visible: true, Key: 'ComboboxPais'},
	 {Name: 'UIPaisExterna_dtDateTimePais', DisplayName: 'Date Time Pais', ColumnSpan: 0, Visible: true, Key: 'DateTimePais'},
	 {Name: 'UIPaisExterna_ntxDecimalPais', DisplayName: 'Decimal Pais', ColumnSpan: 0, Visible: true, Key: 'DecimalPais'},
	 {Name: 'UIPaisExterna_ntxIdPais', DisplayName: 'Id Pais', ColumnSpan: 0, Visible: true, Key: 'IdPais'},
	 {Name: 'UIPaisExterna_tbNomePais', DisplayName: 'Nome Pais', ColumnSpan: 0, Visible: true, Key: 'NomePais'},]},               ]
           }
           langResult.Items.concat(custom.getCustomTranslation().Items);
           return langResult;
        };
       return result;
  }
)

