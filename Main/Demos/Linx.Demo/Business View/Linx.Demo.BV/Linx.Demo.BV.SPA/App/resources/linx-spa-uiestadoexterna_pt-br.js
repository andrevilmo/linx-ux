/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/
define(['./linx-spa-uiestadoexterna_custom_pt-br.js'],
	function (custom){
		var result = {};

       result.languageFile = function () {
       	return 'pt-br';
       }

result.objectLanguage_UIEstadoExterna = function () {
           var langResult = {
               Name: 'UIEstadoExterna', Items: [

	 {Name: 'UIEstadoExterna_dGridEstado', DisplayName: 'DataGrid', ColumnSpan: 0, Visible: true, Items: [
	 {Id: 'UIEstadoExterna_cmbComboboxEstado', Name: 'UIEstadoExterna_dGridEstado_ComboboxEstado', DisplayName: 'Combobox Estado', ColumnSpan: 0, Visible: true, Key: 'ComboboxEstado'},
	 {Id: 'UIEstadoExterna_dtDateTimeEstado', Name: 'UIEstadoExterna_dGridEstado_DateTimeEstado', DisplayName: 'Date Time Estado', ColumnSpan: 0, Visible: true, Key: 'DateTimeEstado'},
	 {Id: 'UIEstadoExterna_ntxDecimalEstado', Name: 'UIEstadoExterna_dGridEstado_DecimalEstado', DisplayName: 'Decimal Estado', ColumnSpan: 0, Visible: true, Key: 'DecimalEstado'},
	 {Id: 'UIEstadoExterna_ntxIdEstado', Name: 'UIEstadoExterna_dGridEstado_IdEstado', DisplayName: 'Id Estado', ColumnSpan: 0, Visible: true, Key: 'IdEstado'},
	 {Id: 'UIEstadoExterna_lUpIdPais', Name: 'UIEstadoExterna_dGridEstado_IdPais', DisplayName: 'Id Pais', ColumnSpan: 0, Visible: true, Key: 'IdPais'},
	 {Id: 'UIEstadoExterna_tbNomeEstado', Name: 'UIEstadoExterna_dGridEstado_NomeEstado', DisplayName: 'Nome Estado', ColumnSpan: 0, Visible: true, Key: 'NomeEstado'},]},               ]
           }
           langResult.Items.concat(custom.getCustomTranslation().Items);
           return langResult;
        };
       return result;
  }
)

