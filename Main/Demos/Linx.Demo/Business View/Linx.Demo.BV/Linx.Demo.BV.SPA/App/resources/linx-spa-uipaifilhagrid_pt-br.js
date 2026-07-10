/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/
define(['./linx-spa-uipaifilhagrid_custom_pt-br.js'],
	function (custom){
		var result = {};

       result.languageFile = function () {
       	return 'pt-br';
       }

result.objectLanguage_UIPaiFilhaGrid = function () {
           var langResult = {
               Name: 'UIPaiFilhaGrid', Items: [

	 {Name: 'UIPaiFilhaGrid_dGridLoja', DisplayName: 'DataGrid', ColumnSpan: 12, Visible: true, Items: [
	 {Id: 'UIPaiFilhaGrid_ntxBigIntLoja', Name: 'UIPaiFilhaGrid_dGridLoja_BigIntLoja', DisplayName: 'Big Int Loja', ColumnSpan: 8, Visible: true, Key: 'BigIntLoja'},
	 {Id: 'UIPaiFilhaGrid_ckBitLoja', Name: 'UIPaiFilhaGrid_dGridLoja_BitLoja', DisplayName: 'Bit Loja', ColumnSpan: 3, Visible: true, Key: 'BitLoja'},
	 {Id: 'UIPaiFilhaGrid_cmbComboboxLoja', Name: 'UIPaiFilhaGrid_dGridLoja_ComboboxLoja', DisplayName: 'Combobox Loja', ColumnSpan: 6, Visible: true, Key: 'ComboboxLoja'},
	 {Id: 'UIPaiFilhaGrid_dtDatetimeLoja', Name: 'UIPaiFilhaGrid_dGridLoja_DatetimeLoja', DisplayName: 'Datetime Loja', ColumnSpan: 3, Visible: true, Key: 'DatetimeLoja'},
	 {Id: 'UIPaiFilhaGrid_lUpIdCidade', Name: 'UIPaiFilhaGrid_dGridLoja_IdCidade', DisplayName: 'Id Cidade', ColumnSpan: 5, Visible: true, LookUpName: 'LookUpCidade', Key: 'IdCidade'},
	 {Id: 'UIPaiFilhaGrid_lUpIdEstado', Name: 'UIPaiFilhaGrid_dGridLoja_IdEstado', DisplayName: 'Id Estado', ColumnSpan: 5, Visible: true, LookUpName: 'LookUpCidade', Key: 'IdEstado'},
	 {Id: 'UIPaiFilhaGrid_lUpIdPais', Name: 'UIPaiFilhaGrid_dGridLoja_IdPais', DisplayName: 'Id Pais', ColumnSpan: 5, Visible: true, LookUpName: 'LookUpCidade', Key: 'IdPais'},
	 {Id: 'UIPaiFilhaGrid_ntxDecimalLoja', Name: 'UIPaiFilhaGrid_dGridLoja_DecimalLoja', DisplayName: 'Decimal Loja', ColumnSpan: 5, Visible: true, Key: 'DecimalLoja'},
	 {Id: 'UIPaiFilhaGrid_lUpNomeCidade', Name: 'UIPaiFilhaGrid_dGridLoja_NomeCidade', DisplayName: 'Nome Cidade', ColumnSpan: 9, Visible: true, LookUpName: 'LookUpCidade', Key: 'NomeCidade'},
	 {Id: 'UIPaiFilhaGrid_ntxIdLoja', Name: 'UIPaiFilhaGrid_dGridLoja_IdLoja', DisplayName: 'Id Loja', ColumnSpan: 5, Visible: true, Key: 'IdLoja'},
	 {Id: 'UIPaiFilhaGrid_ntxIntLoja', Name: 'UIPaiFilhaGrid_dGridLoja_IntLoja', DisplayName: 'Int Loja', ColumnSpan: 5, Visible: true, Key: 'IntLoja'},
	 {Id: 'UIPaiFilhaGrid_ntxSmallIntLoja', Name: 'UIPaiFilhaGrid_dGridLoja_SmallIntLoja', DisplayName: 'Small Int Loja', ColumnSpan: 2, Visible: true, Key: 'SmallIntLoja'},
	 {Id: 'UIPaiFilhaGrid_tbGuidLoja', Name: 'UIPaiFilhaGrid_dGridLoja_GuidLoja', DisplayName: 'Guid Loja', ColumnSpan: 8, Visible: true, Key: 'GuidLoja'},
	 {Id: 'UIPaiFilhaGrid_lUpStringEstado', Name: 'UIPaiFilhaGrid_dGridLoja_StringEstado', DisplayName: 'Nome Estado', ColumnSpan: 9, Visible: true, LookUpName: 'LookUpCidade', Key: 'StringEstado'},
	 {Id: 'UIPaiFilhaGrid_tbStringLoja', Name: 'UIPaiFilhaGrid_dGridLoja_StringLoja', DisplayName: 'String Loja', ColumnSpan: 9, Visible: true, Key: 'StringLoja'},
	 {Id: 'UIPaiFilhaGrid_lUpStringPais', Name: 'UIPaiFilhaGrid_dGridLoja_StringPais', DisplayName: 'Nome Pais', ColumnSpan: 9, Visible: true, LookUpName: 'LookUpCidade', Key: 'StringPais'},]},
	 {Name: 'UIPaiFilhaGrid_tcLojaTabControl', DisplayName: 'Loja', ColumnSpan: 12, Visible: true, Items: [
	 {Name: 'UIPaiFilhaGrid_tiVendaTabItem', DisplayName: 'Venda', ColumnSpan: 12, Visible: true, Items: [
	 {Name: 'UIPaiFilhaGrid_dGridVenda', DisplayName: 'DataGrid', ColumnSpan: 12, Visible: true, Items: [
	 {Id: 'UIPaiFilhaGrid_ckVenda_BitVenda', Name: 'UIPaiFilhaGrid_dGridVenda_BitVenda', DisplayName: 'Bit Venda', ColumnSpan: 5, Visible: true, Key: 'BitVenda'},
	 {Id: 'UIPaiFilhaGrid_cmbVenda_ComboboxVenda', Name: 'UIPaiFilhaGrid_dGridVenda_ComboboxVenda', DisplayName: 'Combobox Venda', ColumnSpan: 6, Visible: true, Key: 'ComboboxVenda'},
	 {Id: 'UIPaiFilhaGrid_dtVenda_DatetimeVenda', Name: 'UIPaiFilhaGrid_dGridVenda_DatetimeVenda', DisplayName: 'Datetime Venda', ColumnSpan: 3, Visible: true, Key: 'DatetimeVenda'},
	 {Id: 'UIPaiFilhaGrid_lUpVenda_IdVendedor', Name: 'UIPaiFilhaGrid_dGridVenda_IdVendedor', DisplayName: 'Id Vendedor', ColumnSpan: 5, Visible: true, LookUpName: 'LookUpVendedor', Key: 'IdVendedor'},
	 {Id: 'UIPaiFilhaGrid_ntxVenda_DecimalVenda', Name: 'UIPaiFilhaGrid_dGridVenda_DecimalVenda', DisplayName: 'Decimal Venda', ColumnSpan: 5, Visible: true, Key: 'DecimalVenda'},
	 {Id: 'UIPaiFilhaGrid_ntxVenda_IdLoja', Name: 'UIPaiFilhaGrid_dGridVenda_IdLoja', DisplayName: 'Id Loja', ColumnSpan: 5, Visible: true, Key: 'IdLoja'},
	 {Id: 'UIPaiFilhaGrid_ntxVenda_IdVenda', Name: 'UIPaiFilhaGrid_dGridVenda_IdVenda', DisplayName: 'Id Venda', ColumnSpan: 5, Visible: true, Key: 'IdVenda'},
	 {Id: 'UIPaiFilhaGrid_ntxVenda_IntVenda', Name: 'UIPaiFilhaGrid_dGridVenda_IntVenda', DisplayName: 'Int Venda', ColumnSpan: 5, Visible: true, Key: 'IntVenda'},
	 {Id: 'UIPaiFilhaGrid_ntxVenda_SmallIntVenda', Name: 'UIPaiFilhaGrid_dGridVenda_SmallIntVenda', DisplayName: 'Small Int Venda', ColumnSpan: 2, Visible: true, Key: 'SmallIntVenda'},
	 {Id: 'UIPaiFilhaGrid_tbVenda_StringVendedor', Name: 'UIPaiFilhaGrid_dGridVenda_StringVendedor', DisplayName: 'String Vendedor', ColumnSpan: 9, Visible: true, Key: 'StringVendedor'},
	 {Id: 'UIPaiFilhaGrid_tbVenda_StringVenda', Name: 'UIPaiFilhaGrid_dGridVenda_StringVenda', DisplayName: 'String Venda', ColumnSpan: 9, Visible: true, Key: 'StringVenda'},]},]},
	 {Name: 'UIPaiFilhaGrid_tiVendaItemTabItem', DisplayName: 'VendaItem', ColumnSpan: 12, Visible: true, Items: [
	 {Name: 'UIPaiFilhaGrid_dGridVendaItem', DisplayName: 'DataGrid', ColumnSpan: 12, Visible: true, Items: [
	 {Id: 'UIPaiFilhaGrid_ntxVendaItem_IdVenda', Name: 'UIPaiFilhaGrid_dGridVendaItem_IdVenda', DisplayName: 'Id Venda', ColumnSpan: 5, Visible: true, Key: 'IdVenda'},
	 {Id: 'UIPaiFilhaGrid_ntxVendaItem_IdVendaItem', Name: 'UIPaiFilhaGrid_dGridVendaItem_IdVendaItem', DisplayName: 'Id Venda Item', ColumnSpan: 5, Visible: true, Key: 'IdVendaItem'},
	 {Id: 'UIPaiFilhaGrid_cmbVendaItem_ComboboxVendaItem', Name: 'UIPaiFilhaGrid_dGridVendaItem_ComboboxVendaItem', DisplayName: 'Combobox Venda Item', ColumnSpan: 1, Visible: true, Key: 'ComboboxVendaItem'},
	 {Id: 'UIPaiFilhaGrid_dtVendaItem_DatetimeVendaItem', Name: 'UIPaiFilhaGrid_dGridVendaItem_DatetimeVendaItem', DisplayName: 'Datetime Venda Item', ColumnSpan: 3, Visible: true, Key: 'DatetimeVendaItem'},
	 {Id: 'UIPaiFilhaGrid_ntxVendaItem_DecimalVendaItem', Name: 'UIPaiFilhaGrid_dGridVendaItem_DecimalVendaItem', DisplayName: 'Decimal Venda Item', ColumnSpan: 5, Visible: true, Key: 'DecimalVendaItem'},
	 {Id: 'UIPaiFilhaGrid_tbVendaItem_StringVendaItem', Name: 'UIPaiFilhaGrid_dGridVendaItem_StringVendaItem', DisplayName: 'String Venda Item', ColumnSpan: 9, Visible: true, Key: 'StringVendaItem'},]},]},]},               ]
           }
           langResult.Items.concat(custom.getCustomTranslation().Items);
           return langResult;
        };
       return result;
  }
)

