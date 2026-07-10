/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/
define(['./linx-spa-uipaifilhaneta_custom_pt-br.js'],
	function (custom){
		var result = {};

       result.languageFile = function () {
       	return 'pt-br';
       }

result.objectLanguage_UIPaiFilhaNeta = function () {
           var langResult = {
               Name: 'UIPaiFilhaNeta', Items: [

	 {Name: 'UIPaiFilhaNeta_gbLoja', DisplayName: 'Loja', ColumnSpan: 12, Visible: true, Items: [
	 {Name: 'UIPaiFilhaNeta_ntxIdLoja', DisplayName: 'Id Loja', ColumnSpan: 2, Visible: true, Key: 'IdLoja'},
	 {Name: 'UIPaiFilhaNeta_ntxIntLoja', DisplayName: 'Int Loja', ColumnSpan: 2, Visible: true, Key: 'IntLoja'},
	 {Name: 'UIPaiFilhaNeta_ntxBigIntLoja', DisplayName: 'Big Int Loja', ColumnSpan: 2, Visible: true, Key: 'BigIntLoja'},
	 {Name: 'UIPaiFilhaNeta_ckBitLoja', DisplayName: 'Bit Loja', ColumnSpan: 2, Visible: true, Key: 'BitLoja'},
	 {Name: 'UIPaiFilhaNeta_lUpIdCidade', DisplayName: 'Id Cidade', ColumnSpan: 2, Visible: true, LookUpName: 'LookUpCidade', Key: 'IdCidade'},
	 {Name: 'UIPaiFilhaNeta_lUpIdEstado', DisplayName: 'Id Estado', ColumnSpan: 2, Visible: true, LookUpName: 'LookUpCidade', Key: 'IdEstado'},
	 {Name: 'UIPaiFilhaNeta_lUpIdPais', DisplayName: 'Id Pais', ColumnSpan: 2, Visible: true, LookUpName: 'LookUpCidade', Key: 'IdPais'},
	 {Name: 'UIPaiFilhaNeta_lUpNomeCidade', DisplayName: 'Nome Cidade', ColumnSpan: 3, Visible: true, LookUpName: 'LookUpCidade', Key: 'NomeCidade'},
	 {Name: 'UIPaiFilhaNeta_lUpStringPais', DisplayName: 'Nome Pais', ColumnSpan: 3, Visible: true, LookUpName: 'LookUpCidade', Key: 'StringPais'},
	 {Name: 'UIPaiFilhaNeta_tbStringLoja', DisplayName: 'String Loja', ColumnSpan: 3, Visible: true, Key: 'StringLoja'},
	 {Name: 'UIPaiFilhaNeta_cmbComboboxLoja', DisplayName: 'Combobox Loja', ColumnSpan: 3, Visible: true, Key: 'ComboboxLoja'},
	 {Name: 'UIPaiFilhaNeta_dtDatetimeLoja', DisplayName: 'Datetime Loja', ColumnSpan: 3, Visible: true, Key: 'DatetimeLoja'},
	 {Name: 'UIPaiFilhaNeta_ntxDecimalLoja', DisplayName: 'Decimal Loja', ColumnSpan: 3, Visible: true, Key: 'DecimalLoja'},
	 {Name: 'UIPaiFilhaNeta_ntxSmallIntLoja', DisplayName: 'Small Int Loja', ColumnSpan: 2, Visible: true, Key: 'SmallIntLoja'},
	 {Name: 'UIPaiFilhaNeta_lUpStringEstado', DisplayName: 'Nome Estado', ColumnSpan: 2, Visible: true, LookUpName: 'LookUpCidade', Key: 'StringEstado'},]},
	 {Name: 'UIPaiFilhaNeta_tcLojaTabControl', DisplayName: 'Loja', ColumnSpan: 12, Visible: true, Items: [
	 {Name: 'UIPaiFilhaNeta_tiVendaTabItem', DisplayName: 'Venda', ColumnSpan: 12, Visible: true, Items: [
	 {Name: 'UIPaiFilhaNeta_dGridExpander_6af03c3127324c9b892b5bb7581ee2fd', DisplayName: '', ColumnSpan: 12, Visible: true, Items: [
	 {Id: 'UIPaiFilhaNeta_dtVenda_DatetimeVenda', Name: 'UIPaiFilhaNeta_dGridExpander_6af03c3127324c9b892b5bb7581ee2fd_DatetimeVenda', DisplayName: 'Datetime Venda', ColumnSpan: 3, Visible: true, Key: 'DatetimeVenda'},
	 {Id: 'UIPaiFilhaNeta_cmbVenda_ComboboxVenda', Name: 'UIPaiFilhaNeta_dGridExpander_6af03c3127324c9b892b5bb7581ee2fd_ComboboxVenda', DisplayName: 'Combobox Venda', ColumnSpan: 6, Visible: true, Key: 'ComboboxVenda'},
	 {Id: 'UIPaiFilhaNeta_ntxVenda_IdVenda', Name: 'UIPaiFilhaNeta_dGridExpander_6af03c3127324c9b892b5bb7581ee2fd_IdVenda', DisplayName: 'Id Venda', ColumnSpan: 8, Visible: true, Key: 'IdVenda'},
	 {Id: 'UIPaiFilhaNeta_lUpVenda_IdVendedor', Name: 'UIPaiFilhaNeta_dGridExpander_6af03c3127324c9b892b5bb7581ee2fd_IdVendedor', DisplayName: 'Id Vendedor', ColumnSpan: 8, Visible: true, LookUpName: 'LookUpVendedor', Key: 'IdVendedor'},
	 {Id: 'UIPaiFilhaNeta_ntxVenda_SmallIntVenda', Name: 'UIPaiFilhaNeta_dGridExpander_6af03c3127324c9b892b5bb7581ee2fd_SmallIntVenda', DisplayName: 'Small Int Venda', ColumnSpan: 8, Visible: true, Key: 'SmallIntVenda'},
	 {Id: 'UIPaiFilhaNeta_tbVenda_StringVendedor', Name: 'UIPaiFilhaNeta_dGridExpander_6af03c3127324c9b892b5bb7581ee2fd_StringVendedor', DisplayName: 'String Vendedor', ColumnSpan: 9, Visible: true, Key: 'StringVendedor'},
	 {Id: 'UIPaiFilhaNeta_ntxVenda_IntVenda', Name: 'UIPaiFilhaNeta_dGridExpander_6af03c3127324c9b892b5bb7581ee2fd_IntVenda', DisplayName: 'Int Venda', ColumnSpan: 8, Visible: true, Key: 'IntVenda'},
	 {Id: 'UIPaiFilhaNeta_tbVenda_StringVenda', Name: 'UIPaiFilhaNeta_dGridExpander_6af03c3127324c9b892b5bb7581ee2fd_StringVenda', DisplayName: 'String Venda', ColumnSpan: 9, Visible: true, Key: 'StringVenda'},
	 {Id: 'UIPaiFilhaNeta_ntxVenda_DecimalVenda', Name: 'UIPaiFilhaNeta_dGridExpander_6af03c3127324c9b892b5bb7581ee2fd_DecimalVenda', DisplayName: 'Decimal Venda', ColumnSpan: 5, Visible: true, Key: 'DecimalVenda'},
	 {Id: 'UIPaiFilhaNeta_ckVenda_BitVenda', Name: 'UIPaiFilhaNeta_dGridExpander_6af03c3127324c9b892b5bb7581ee2fd_BitVenda', DisplayName: 'Bit Venda', ColumnSpan: 5, Visible: true, Key: 'BitVenda'},]},]},
	 {Name: 'UIPaiFilhaNeta_tiVendaItemTabItem', DisplayName: 'VendaItem', ColumnSpan: 12, Visible: true, Items: [
	 {Name: 'UIPaiFilhaNeta_pivotVendaItem', DisplayName: '', ColumnSpan: 12, Visible: true, Items: [
	 {Name: 'UIPaiFilhaNeta_cmbVendaItem_ComboboxVendaItem', DisplayName: 'Combobox Venda Item', ColumnSpan: 6, Visible: true, Key: 'ComboboxVendaItem'},
	 {Name: 'UIPaiFilhaNeta_dtVendaItem_DatetimeVendaItem', DisplayName: 'Datetime Venda Item', ColumnSpan: 3, Visible: true, Key: 'DatetimeVendaItem'},
	 {Name: 'UIPaiFilhaNeta_ntxDecimalVendaItem', DisplayName: 'Decimal Venda Item', ColumnSpan: 5, Visible: true, Key: 'DecimalVendaItem'},
	 {Name: 'UIPaiFilhaNeta_ntxIdVenda', DisplayName: 'Id Venda', ColumnSpan: 8, Visible: true, Key: 'IdVenda'},
	 {Name: 'UIPaiFilhaNeta_ntxIdVendaItem', DisplayName: 'Id Venda Item', ColumnSpan: 8, Visible: true, Key: 'IdVendaItem'},
	 {Name: 'UIPaiFilhaNeta_tbVendaItem_StringVendaItem', DisplayName: 'String Venda Item', ColumnSpan: 9, Visible: true, Key: 'StringVendaItem'},]},]},]},               ]
           }
           langResult.Items.concat(custom.getCustomTranslation().Items);
           return langResult;
        };
       return result;
  }
)

