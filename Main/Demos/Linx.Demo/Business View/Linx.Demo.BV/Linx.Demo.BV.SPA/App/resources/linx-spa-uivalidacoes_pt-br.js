/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/
define(['./linx-spa-uivalidacoes_custom_pt-br.js'],
	function (custom){
		var result = {};

       result.languageFile = function () {
       	return 'pt-br';
       }

result.objectLanguage_UIValidacoes = function () {
           var langResult = {
               Name: 'UIValidacoes', Items: [

	 {Name: 'UIValidacoes_gbPais', DisplayName: 'Pais', ColumnSpan: 0, Visible: true, Items: [
	 {Name: 'UIValidacoes_cmbComboboxPais', DisplayName: 'Combobox Pais', ColumnSpan: 0, Visible: true, Key: 'ComboboxPais'},
	 {Name: 'UIValidacoes_dtDateTimePais', DisplayName: 'Date Time Pais', ColumnSpan: 0, Visible: true, Key: 'DateTimePais'},
	 {Name: 'UIValidacoes_ntxDecimalPais', DisplayName: 'Decimal Pais', ColumnSpan: 0, Visible: true, Key: 'DecimalPais'},
	 {Name: 'UIValidacoes_ntxIdPais', DisplayName: 'Id Pais', ColumnSpan: 0, Visible: true, Key: 'IdPais'},
	 {Name: 'UIValidacoes_tbNomePais', DisplayName: 'Nome Pais', ColumnSpan: 0, Visible: true, Key: 'NomePais'},
	 {Name: 'UIValidacoes_PAIS.IdPais', DisplayName: 'Media (PAIS)', ColumnSpan: 0, Visible: true, Key: 'IdPais'},]},
	 {Name: 'UIValidacoes_tcPaisTabControl', DisplayName: 'Estado', ColumnSpan: 0, Visible: true, Items: [
	 {Name: 'UIValidacoes_tiEstadoTabItem', DisplayName: 'Estado', ColumnSpan: 0, Visible: true, Items: [
	 {Name: 'UIValidacoes_dGridEstado', DisplayName: 'DataGrid', ColumnSpan: 0, Visible: true, Items: [
	 {Id: 'UIValidacoes_cmbEstado_ComboboxEstado', Name: 'UIValidacoes_dGridEstado_ComboboxEstado', DisplayName: 'Combobox Estado', ColumnSpan: 0, Visible: true, Key: 'ComboboxEstado'},
	 {Id: 'UIValidacoes_dtEstado_DateTimeEstado', Name: 'UIValidacoes_dGridEstado_DateTimeEstado', DisplayName: 'Date Time Estado', ColumnSpan: 0, Visible: true, Key: 'DateTimeEstado'},
	 {Id: 'UIValidacoes_ntxEstado_DecimalEstado', Name: 'UIValidacoes_dGridEstado_DecimalEstado', DisplayName: 'Decimal Estado', ColumnSpan: 0, Visible: true, Key: 'DecimalEstado'},
	 {Id: 'UIValidacoes_ntxEstado_IdEstado', Name: 'UIValidacoes_dGridEstado_IdEstado', DisplayName: 'Id Estado', ColumnSpan: 0, Visible: true, Key: 'IdEstado'},
	 {Id: 'UIValidacoes_lUpEstado_IdPais', Name: 'UIValidacoes_dGridEstado_IdPais', DisplayName: 'Id Pais', ColumnSpan: 0, Visible: true, Key: 'IdPais'},
	 {Id: 'UIValidacoes_tbEstado_NomeEstado', Name: 'UIValidacoes_dGridEstado_NomeEstado', DisplayName: 'Nome Estado', ColumnSpan: 0, Visible: true, Key: 'NomeEstado'},
	 {Id: 'UIValidacoes_Estado_ESTADO.IdEstado', Name: 'UIValidacoes_dGridEstado_ESTADO.IdEstado', DisplayName: 'Media (ESTADO)', ColumnSpan: 0, Visible: true, Key: 'IdEstado'},]},]},]},               ]
           }
           langResult.Items.concat(custom.getCustomTranslation().Items);
           return langResult;
        };
       return result;
  }
)

