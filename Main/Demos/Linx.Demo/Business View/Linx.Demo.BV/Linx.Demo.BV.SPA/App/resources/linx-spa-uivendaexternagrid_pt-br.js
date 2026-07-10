/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/
define(['./linx-spa-uivendaexternagrid_custom_pt-br.js'],
	function (custom){
		var result = {};

       result.languageFile = function () {
       	return 'pt-br';
       }

result.objectLanguage_UIVendaexternaGRID = function () {
           var langResult = {
               Name: 'UIVendaexternaGRID', Items: [

	 {Name: 'UIVendaexternaGRID_dGridVenda', DisplayName: 'DataGrid', ColumnSpan: 0, Visible: true, Items: [
	 {Id: 'UIVendaexternaGRID_ntxBigIntVenda', Name: 'UIVendaexternaGRID_dGridVenda_BigIntVenda', DisplayName: 'Big Int Venda', ColumnSpan: 0, Visible: true, Key: 'BigIntVenda'},
	 {Id: 'UIVendaexternaGRID_ckBitVenda', Name: 'UIVendaexternaGRID_dGridVenda_BitVenda', DisplayName: 'Bit Venda', ColumnSpan: 0, Visible: true, Key: 'BitVenda'},
	 {Id: 'UIVendaexternaGRID_cmbComboboxVenda', Name: 'UIVendaexternaGRID_dGridVenda_ComboboxVenda', DisplayName: 'Combobox Venda', ColumnSpan: 0, Visible: true, Key: 'ComboboxVenda'},
	 {Id: 'UIVendaexternaGRID_dtDatetimeVenda', Name: 'UIVendaexternaGRID_dGridVenda_DatetimeVenda', DisplayName: 'Datetime Venda', ColumnSpan: 0, Visible: true, Key: 'DatetimeVenda'},
	 {Id: 'UIVendaexternaGRID_lUpIdVendedor', Name: 'UIVendaexternaGRID_dGridVenda_IdVendedor', DisplayName: 'Id Vendedor', ColumnSpan: 0, Visible: true, LookUpName: 'LookUpVendedor', Key: 'IdVendedor'},
	 {Id: 'UIVendaexternaGRID_ntxDecimalVenda', Name: 'UIVendaexternaGRID_dGridVenda_DecimalVenda', DisplayName: 'Decimal Venda', ColumnSpan: 0, Visible: true, Key: 'DecimalVenda'},
	 {Id: 'UIVendaexternaGRID_lUpIdFormaPagamento', Name: 'UIVendaexternaGRID_dGridVenda_IdFormaPagamento', DisplayName: 'Id Forma Pagamento', ColumnSpan: 0, Visible: true, LookUpName: 'LookUpFormaPagamento', Key: 'IdFormaPagamento'},
	 {Id: 'UIVendaexternaGRID_lUpIdLoja', Name: 'UIVendaexternaGRID_dGridVenda_IdLoja', DisplayName: 'Id Loja', ColumnSpan: 0, Visible: true, LookUpName: 'LookUpLoja', Key: 'IdLoja'},
	 {Id: 'UIVendaexternaGRID_ntxIdVenda', Name: 'UIVendaexternaGRID_dGridVenda_IdVenda', DisplayName: 'Id Venda', ColumnSpan: 0, Visible: true, Key: 'IdVenda'},
	 {Id: 'UIVendaexternaGRID_ntxIntVenda', Name: 'UIVendaexternaGRID_dGridVenda_IntVenda', DisplayName: 'Int Venda', ColumnSpan: 0, Visible: true, Key: 'IntVenda'},
	 {Id: 'UIVendaexternaGRID_ntxSmallIntVenda', Name: 'UIVendaexternaGRID_dGridVenda_SmallIntVenda', DisplayName: 'Small Int Venda', ColumnSpan: 0, Visible: true, Key: 'SmallIntVenda'},
	 {Id: 'UIVendaexternaGRID_ntxValorTotal', Name: 'UIVendaexternaGRID_dGridVenda_ValorTotal', DisplayName: 'Valor Total', ColumnSpan: 0, Visible: true, Key: 'ValorTotal'},]},               ]
           }
           langResult.Items.concat(custom.getCustomTranslation().Items);
           return langResult;
        };
       return result;
  }
)

