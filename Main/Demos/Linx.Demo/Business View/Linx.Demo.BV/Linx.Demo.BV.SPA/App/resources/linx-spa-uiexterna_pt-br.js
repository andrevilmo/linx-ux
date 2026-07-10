/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/
define(['./linx-spa-uiexterna_custom_pt-br.js'],
	function (custom){
		var result = {};

       result.languageFile = function () {
       	return 'pt-br';
       }

result.objectLanguage_UIExterna = function () {
           var langResult = {
               Name: 'UIExterna', Items: [

	 {Name: 'UIExterna_gbCustomContainer_6857ba8d00974468904b3b916a35109c', DisplayName: 'New Group', ColumnSpan: 0, Visible: true, Items: [
	 {Name: 'UIExterna_gbLoja', DisplayName: 'Loja', ColumnSpan: 0, Visible: true, Items: [
	 {Name: 'UIExterna_ntxBigIntLoja', DisplayName: 'Big Int Loja', ColumnSpan: 0, Visible: true, Key: 'BigIntLoja'},
	 {Name: 'UIExterna_ckBitLoja', DisplayName: 'Bit Loja', ColumnSpan: 0, Visible: true, Key: 'BitLoja'},
	 {Name: 'UIExterna_cmbComboboxLoja', DisplayName: 'Combobox Loja', ColumnSpan: 0, Visible: true, Key: 'ComboboxLoja'},
	 {Name: 'UIExterna_dtDatetimeLoja', DisplayName: 'Datetime Loja', ColumnSpan: 0, Visible: true, Key: 'DatetimeLoja'},
	 {Name: 'UIExterna_lUpIdCidade', DisplayName: 'Id Cidade', ColumnSpan: 0, Visible: true, LookUpName: 'LookUpCidade', Key: 'IdCidade'},
	 {Name: 'UIExterna_lUpIdPais', DisplayName: 'Id Pais', ColumnSpan: 0, Visible: true, LookUpName: 'LookUpPais', Key: 'IdPais'},
	 {Name: 'UIExterna_ntxDecimalLoja', DisplayName: 'Decimal Loja', ColumnSpan: 0, Visible: true, Key: 'DecimalLoja'},
	 {Name: 'UIExterna_ntxIdLoja', DisplayName: 'Id Loja', ColumnSpan: 0, Visible: true, Key: 'IdLoja'},
	 {Name: 'UIExterna_ntxIntLoja', DisplayName: 'Int Loja', ColumnSpan: 0, Visible: true, Key: 'IntLoja'},
	 {Name: 'UIExterna_tbNomeLoja', DisplayName: 'Nome Loja', ColumnSpan: 0, Visible: true, Key: 'NomeLoja'},
	 {Name: 'UIExterna_ntxSmallIntLoja', DisplayName: 'Small Int Loja', ColumnSpan: 0, Visible: true, Key: 'SmallIntLoja'},
	 {Name: 'UIExterna_btnOpenView', DisplayName: 'Botão Open View', ColumnSpan: 0, Visible: true, Key: ''},]},
	 {Name: 'UIExterna_tcTabControl_0c64bcba91b84ea4a75423aa1e8b4d5f', DisplayName: '', ColumnSpan: 0, Visible: true, Items: [
	 {Name: 'UIExterna_tiTabItem_5f82396cbfe2425f82d6f995a723e5f2', DisplayName: 'UIExternaTabFormulario', ColumnSpan: 0, Visible: true, Items: [
	 {Name: 'UIExterna_euiExternalUI_e05915bcce3a47ff8bf3c55a0770213d', DisplayName: '', ColumnSpan: 0, Visible: true, },]},
	 {Name: 'UIExterna_tiTabItem_7e08a6dbdf9040b2a76820a4d3821af7', DisplayName: 'UIExternaTabGrid', ColumnSpan: 0, Visible: true, Items: [
	 {Name: 'UIExterna_euiExternalUI_04c6ca8591c94064867eee2d614907fb', DisplayName: '', ColumnSpan: 0, Visible: true, },]},
	 {Name: 'UIExterna_tiTabItem_ba3af8bc999946bea3b147a3dd2fb231', DisplayName: 'UIExternaBotão', ColumnSpan: 0, Visible: true, Items: [
	 {Name: 'UIExterna_btnCustomControl3975155', DisplayName: 'UI Externa País', ColumnSpan: 0, Visible: true, Key: ''},]},
	 {Name: 'UIExterna_tiTabItem_a0457705df564677904a46b90e29cc53', DisplayName: 'UIExternaLookUp', ColumnSpan: 0, Visible: true, Items: [
	 {Name: 'UIExterna_lUpIdEstado', DisplayName: 'Id Estado', ColumnSpan: 0, Visible: true, LookUpName: 'LookUpEstado', Key: 'IdEstado'},]},]},]},               ]
           }
           langResult.Items.concat(custom.getCustomTranslation().Items);
           return langResult;
        };
       return result;
  }
)

