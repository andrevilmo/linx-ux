/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/
define(['./linx-spa-uipaifilhaleft_custom_pt-br.js'],
	function (custom){
		var result = {};

       result.languageFile = function () {
       	return 'pt-br';
       }

result.objectLanguage_UIPaiFilhaLeft = function () {
           var langResult = {
               Name: 'UIPaiFilhaLeft', Items: [

	 {Name: 'UIPaiFilhaLeft_gbLoja', DisplayName: 'Loja', ColumnSpan: 12, Visible: true, Items: [
	 {Name: 'UIPaiFilhaLeft_ntxBigIntLoja', DisplayName: 'Big Int Loja', ColumnSpan: 8, Visible: true, Key: 'BigIntLoja'},
	 {Name: 'UIPaiFilhaLeft_ckBitLoja', DisplayName: 'Bit Loja', ColumnSpan: 3, Visible: true, Key: 'BitLoja'},
	 {Name: 'UIPaiFilhaLeft_cmbComboboxLoja', DisplayName: 'Combobox Loja', ColumnSpan: 6, Visible: true, Key: 'ComboboxLoja'},
	 {Name: 'UIPaiFilhaLeft_dtDatetimeLoja', DisplayName: 'Datetime Loja', ColumnSpan: 3, Visible: true, Key: 'DatetimeLoja'},
	 {Name: 'UIPaiFilhaLeft_lUpIdCidade', DisplayName: 'Id Cidade', ColumnSpan: 5, Visible: true, LookUpName: 'LookUpCidade', Key: 'IdCidade'},
	 {Name: 'UIPaiFilhaLeft_lUpIdEstado', DisplayName: 'Id Estado', ColumnSpan: 5, Visible: true, LookUpName: 'LookUpCidade', Key: 'IdEstado'},
	 {Name: 'UIPaiFilhaLeft_lUpIdPais', DisplayName: 'Id Pais', ColumnSpan: 5, Visible: true, LookUpName: 'LookUpCidade', Key: 'IdPais'},
	 {Name: 'UIPaiFilhaLeft_ntxDecimalLoja', DisplayName: 'Decimal Loja', ColumnSpan: 5, Visible: true, Key: 'DecimalLoja'},
	 {Name: 'UIPaiFilhaLeft_lUpNomeCidade', DisplayName: 'Nome Cidade', ColumnSpan: 9, Visible: true, LookUpName: 'LookUpCidade', Key: 'NomeCidade'},
	 {Name: 'UIPaiFilhaLeft_ntxIdLoja', DisplayName: 'Id Loja', ColumnSpan: 5, Visible: true, Key: 'IdLoja'},
	 {Name: 'UIPaiFilhaLeft_ntxIntLoja', DisplayName: 'Int Loja', ColumnSpan: 5, Visible: true, Key: 'IntLoja'},
	 {Name: 'UIPaiFilhaLeft_ntxSmallIntLoja', DisplayName: 'Small Int Loja', ColumnSpan: 2, Visible: true, Key: 'SmallIntLoja'},
	 {Name: 'UIPaiFilhaLeft_tbGuidLoja', DisplayName: 'Guid Loja', ColumnSpan: 8, Visible: true, Key: 'GuidLoja'},
	 {Name: 'UIPaiFilhaLeft_lUpStringEstado', DisplayName: 'Nome Estado', ColumnSpan: 9, Visible: true, LookUpName: 'LookUpCidade', Key: 'StringEstado'},
	 {Name: 'UIPaiFilhaLeft_tbStringLoja', DisplayName: 'String Loja', ColumnSpan: 9, Visible: true, Key: 'StringLoja'},
	 {Name: 'UIPaiFilhaLeft_lUpStringPais', DisplayName: 'Nome Pais', ColumnSpan: 9, Visible: true, LookUpName: 'LookUpCidade', Key: 'StringPais'},]},
	 {Name: 'UIPaiFilhaLeft_tcLojaTabControl', DisplayName: 'Loja', ColumnSpan: 12, Visible: true, Items: [
	 {Name: 'UIPaiFilhaLeft_tiVendaTabItem', DisplayName: 'Venda', ColumnSpan: 12, Visible: true, Items: [
	 {Name: 'UIPaiFilhaLeft_dGridVenda', DisplayName: 'DataGrid', ColumnSpan: 12, Visible: true, Items: [
	 {Id: 'UIPaiFilhaLeft_ntxVenda_BigIntVenda', Name: 'UIPaiFilhaLeft_dGridVenda_BigIntVenda', DisplayName: 'Big Int Venda', ColumnSpan: 8, Visible: true, Key: 'BigIntVenda'},
	 {Id: 'UIPaiFilhaLeft_ckVenda_BitVenda', Name: 'UIPaiFilhaLeft_dGridVenda_BitVenda', DisplayName: 'Bit Venda', ColumnSpan: 5, Visible: true, Key: 'BitVenda'},
	 {Id: 'UIPaiFilhaLeft_cmbVenda_ComboboxVenda', Name: 'UIPaiFilhaLeft_dGridVenda_ComboboxVenda', DisplayName: 'Combobox Venda', ColumnSpan: 6, Visible: true, Key: 'ComboboxVenda'},
	 {Id: 'UIPaiFilhaLeft_dtVenda_DatetimeVenda', Name: 'UIPaiFilhaLeft_dGridVenda_DatetimeVenda', DisplayName: 'Datetime Venda', ColumnSpan: 3, Visible: true, Key: 'DatetimeVenda'},
	 {Id: 'UIPaiFilhaLeft_lUpVenda_IdVendedor', Name: 'UIPaiFilhaLeft_dGridVenda_IdVendedor', DisplayName: 'Id Vendedor', ColumnSpan: 5, Visible: true, LookUpName: 'LookUpVendedor', Key: 'IdVendedor'},
	 {Id: 'UIPaiFilhaLeft_ntxVenda_DecimalVenda', Name: 'UIPaiFilhaLeft_dGridVenda_DecimalVenda', DisplayName: 'Decimal Venda', ColumnSpan: 5, Visible: true, Key: 'DecimalVenda'},
	 {Id: 'UIPaiFilhaLeft_ntxVenda_IdLoja', Name: 'UIPaiFilhaLeft_dGridVenda_IdLoja', DisplayName: 'Id Loja', ColumnSpan: 5, Visible: true, Key: 'IdLoja'},
	 {Id: 'UIPaiFilhaLeft_ntxVenda_IdVenda', Name: 'UIPaiFilhaLeft_dGridVenda_IdVenda', DisplayName: 'Id Venda', ColumnSpan: 5, Visible: true, Key: 'IdVenda'},
	 {Id: 'UIPaiFilhaLeft_ntxVenda_IntVenda', Name: 'UIPaiFilhaLeft_dGridVenda_IntVenda', DisplayName: 'Int Venda', ColumnSpan: 5, Visible: true, Key: 'IntVenda'},
	 {Id: 'UIPaiFilhaLeft_ntxVenda_SmallIntVenda', Name: 'UIPaiFilhaLeft_dGridVenda_SmallIntVenda', DisplayName: 'Small Int Venda', ColumnSpan: 2, Visible: true, Key: 'SmallIntVenda'},
	 {Id: 'UIPaiFilhaLeft_tbVenda_StringLoja', Name: 'UIPaiFilhaLeft_dGridVenda_StringLoja', DisplayName: 'String Loja', ColumnSpan: 9, Visible: true, Key: 'StringLoja'},
	 {Id: 'UIPaiFilhaLeft_tbVenda_StringVendedor', Name: 'UIPaiFilhaLeft_dGridVenda_StringVendedor', DisplayName: 'String Vendedor', ColumnSpan: 9, Visible: true, Key: 'StringVendedor'},
	 {Id: 'UIPaiFilhaLeft_tbVenda_StringVenda', Name: 'UIPaiFilhaLeft_dGridVenda_StringVenda', DisplayName: 'String Venda', ColumnSpan: 9, Visible: true, Key: 'StringVenda'},]},]},
	 {Name: 'UIPaiFilhaLeft_tiVendaItemTabItem', DisplayName: 'VendaItem', ColumnSpan: 12, Visible: true, Items: [
	 {Name: 'UIPaiFilhaLeft_dGridVendaItem', DisplayName: 'DataGrid', ColumnSpan: 12, Visible: true, Items: [
	 {Id: 'UIPaiFilhaLeft_ntxVendaItem_IdVenda', Name: 'UIPaiFilhaLeft_dGridVendaItem_IdVenda', DisplayName: 'Id Venda', ColumnSpan: 5, Visible: true, Key: 'IdVenda'},
	 {Id: 'UIPaiFilhaLeft_ntxVendaItem_IdVendaItem', Name: 'UIPaiFilhaLeft_dGridVendaItem_IdVendaItem', DisplayName: 'Id Venda Item', ColumnSpan: 5, Visible: true, Key: 'IdVendaItem'},
	 {Id: 'UIPaiFilhaLeft_cmbVendaItem_ComboboxVendaItem', Name: 'UIPaiFilhaLeft_dGridVendaItem_ComboboxVendaItem', DisplayName: 'Combobox Venda Item', ColumnSpan: 1, Visible: true, Key: 'ComboboxVendaItem'},
	 {Id: 'UIPaiFilhaLeft_dtVendaItem_DatetimeVendaItem', Name: 'UIPaiFilhaLeft_dGridVendaItem_DatetimeVendaItem', DisplayName: 'Datetime Venda Item', ColumnSpan: 3, Visible: true, Key: 'DatetimeVendaItem'},
	 {Id: 'UIPaiFilhaLeft_ntxVendaItem_DecimalVendaItem', Name: 'UIPaiFilhaLeft_dGridVendaItem_DecimalVendaItem', DisplayName: 'Decimal Venda Item', ColumnSpan: 5, Visible: true, Key: 'DecimalVendaItem'},
	 {Id: 'UIPaiFilhaLeft_tbVendaItem_StringVendaItem', Name: 'UIPaiFilhaLeft_dGridVendaItem_StringVendaItem', DisplayName: 'String Venda Item', ColumnSpan: 9, Visible: true, Key: 'StringVendaItem'},]},]},]},               ]
           }
           langResult.Items.concat(custom.getCustomTranslation().Items);
           return langResult;
        };
       return result;
  }
)

