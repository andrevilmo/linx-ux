/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/
define(['./linx-spa-uivendaexterna_custom_pt-br.js'],
	function (custom){
		var result = {};

       result.languageFile = function () {
       	return 'pt-br';
       }

result.objectLanguage_UIVendaExterna = function () {
           var langResult = {
               Name: 'UIVendaExterna', Items: [

	 {Name: 'UIVendaExterna_gbVenda', DisplayName: 'Venda', ColumnSpan: 0, Visible: true, Items: [
	 {Name: 'UIVendaExterna_ntxBigIntVenda', DisplayName: 'Big Int Venda', ColumnSpan: 0, Visible: true, Key: 'BigIntVenda'},
	 {Name: 'UIVendaExterna_ckBitVenda', DisplayName: 'Bit Venda', ColumnSpan: 0, Visible: true, Key: 'BitVenda'},
	 {Name: 'UIVendaExterna_cmbComboboxVenda', DisplayName: 'Combobox Venda', ColumnSpan: 0, Visible: true, Key: 'ComboboxVenda'},
	 {Name: 'UIVendaExterna_dtDatetimeVenda', DisplayName: 'Datetime Venda', ColumnSpan: 0, Visible: true, Key: 'DatetimeVenda'},
	 {Name: 'UIVendaExterna_lUpIdVendedor', DisplayName: 'Id Vendedor', ColumnSpan: 0, Visible: true, LookUpName: 'LookUpVendedor', Key: 'IdVendedor'},
	 {Name: 'UIVendaExterna_ntxDecimalVenda', DisplayName: 'Decimal Venda', ColumnSpan: 0, Visible: true, Key: 'DecimalVenda'},
	 {Name: 'UIVendaExterna_lUpIdFormaPagamento', DisplayName: 'Id Forma Pagamento', ColumnSpan: 0, Visible: true, LookUpName: 'LookUpFormaPagamento', Key: 'IdFormaPagamento'},
	 {Name: 'UIVendaExterna_lUpIdLoja', DisplayName: 'Id Loja', ColumnSpan: 0, Visible: true, LookUpName: 'LookUpLoja', Key: 'IdLoja'},
	 {Name: 'UIVendaExterna_ntxIdVenda', DisplayName: 'Id Venda', ColumnSpan: 0, Visible: true, Key: 'IdVenda'},
	 {Name: 'UIVendaExterna_ntxIntVenda', DisplayName: 'Int Venda', ColumnSpan: 0, Visible: true, Key: 'IntVenda'},
	 {Name: 'UIVendaExterna_ntxSmallIntVenda', DisplayName: 'Small Int Venda', ColumnSpan: 0, Visible: true, Key: 'SmallIntVenda'},
	 {Name: 'UIVendaExterna_ntxValorTotal', DisplayName: 'Valor Total', ColumnSpan: 0, Visible: true, Key: 'ValorTotal'},]},               ]
           }
           langResult.Items.concat(custom.getCustomTranslation().Items);
           return langResult;
        };
       return result;
  }
)

