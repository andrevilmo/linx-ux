/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/
define(['./linx-spa-exemplos_grids_com_macro_custom_pt-br.js'],
	function (custom){
		var result = {};

       result.languageFile = function () {
       	return 'pt-br';
       }

result.objectLanguage_Exemplos_GRIDs_com_macro = function () {
           var langResult = {
               Name: 'Exemplos_GRIDs_com_macro', Items: [

	 {Name: 'Exemplos_GRIDs_com_macro_dGridvendas', DisplayName: 'Venda', ColumnSpan: 12, Visible: true, Items: [
	 {Id: 'Exemplos_GRIDs_com_macro_ntxIdCliente', Name: 'Exemplos_GRIDs_com_macro_dGridvendas_IdCliente', DisplayName: 'Id Cliente', ColumnSpan: 2, Visible: true, Key: 'IdCliente'},
	 {Id: 'Exemplos_GRIDs_com_macro_ntxIdVenda', Name: 'Exemplos_GRIDs_com_macro_dGridvendas_IdVenda', DisplayName: 'Id Venda', ColumnSpan: 2, Visible: true, Key: 'IdVenda'},
	 {Id: 'Exemplos_GRIDs_com_macro_tbStringVenda', Name: 'Exemplos_GRIDs_com_macro_dGridvendas_StringVenda', DisplayName: 'String Venda', ColumnSpan: 3, Visible: true, Key: 'StringVenda'},
	 {Id: 'Exemplos_GRIDs_com_macro_cmbComboboxVenda', Name: 'Exemplos_GRIDs_com_macro_dGridvendas_ComboboxVenda', DisplayName: 'Combobox Venda', ColumnSpan: 2, Visible: true, Key: 'ComboboxVenda'},
	 {Id: 'Exemplos_GRIDs_com_macro_ntxIntVenda', Name: 'Exemplos_GRIDs_com_macro_dGridvendas_IntVenda', DisplayName: 'Int Venda', ColumnSpan: 2, Visible: true, Key: 'IntVenda'},
	 {Id: 'Exemplos_GRIDs_com_macro_tbSmallIntVenda', Name: 'Exemplos_GRIDs_com_macro_dGridvendas_SmallIntVenda', DisplayName: 'Small Int Venda', ColumnSpan: 2, Visible: true, Key: 'SmallIntVenda'},
	 {Id: 'Exemplos_GRIDs_com_macro_dtDatetimeVenda', Name: 'Exemplos_GRIDs_com_macro_dGridvendas_DatetimeVenda', DisplayName: 'Datetime Venda', ColumnSpan: 3, Visible: true, Key: 'DatetimeVenda'},
	 {Id: 'Exemplos_GRIDs_com_macro_ntxDecimalVenda', Name: 'Exemplos_GRIDs_com_macro_dGridvendas_DecimalVenda', DisplayName: 'Decimal Venda', ColumnSpan: 2, Visible: true, Key: 'DecimalVenda'},
	 {Id: 'Exemplos_GRIDs_com_macro_lUpStringLoja', Name: 'Exemplos_GRIDs_com_macro_dGridvendas_StringLoja', DisplayName: 'String Loja', ColumnSpan: 3, Visible: true, LookUpName: 'LookUpLoja', Key: 'StringLoja'},
	 {Id: 'Exemplos_GRIDs_com_macro_lUpStringVendedor', Name: 'Exemplos_GRIDs_com_macro_dGridvendas_StringVendedor', DisplayName: 'String Vendedor', ColumnSpan: 3, Visible: true, LookUpName: 'LookUpVendedor', Key: 'StringVendedor'},
	 {Id: 'Exemplos_GRIDs_com_macro_btnDesabilitaGrid', Name: 'Exemplos_GRIDs_com_macro_dGridvendas_CustomControl146160484', DisplayName: 'Desabilita_habilita Grid', ColumnSpan: 2, Visible: true, Key: ''},
	 {Id: 'Exemplos_GRIDs_com_macro_btnDesabilitaColuna', Name: 'Exemplos_GRIDs_com_macro_dGridvendas_CustomControl147755828', DisplayName: 'Desabilita_habilita Coluna', ColumnSpan: 2, Visible: true, Key: ''},
	 {Id: 'Exemplos_GRIDs_com_macro_lUpIdCidade', Name: 'Exemplos_GRIDs_com_macro_dGridvendas_IdCidade', DisplayName: 'Id Cidade', ColumnSpan: 1, Visible: true, LookUpName: 'LookUpLoja', Key: 'IdCidade'},
	 {Id: 'Exemplos_GRIDs_com_macro_lUpNomeCidade', Name: 'Exemplos_GRIDs_com_macro_dGridvendas_NomeCidade', DisplayName: 'Nome Cidade', ColumnSpan: 3, Visible: true, LookUpName: 'LookUpLoja', Key: 'NomeCidade'},]},
	 {Name: 'Exemplos_GRIDs_com_macro_tcVendaTabControl', DisplayName: 'Venda', ColumnSpan: 12, Visible: true, Items: [
	 {Name: 'Exemplos_GRIDs_com_macro_tiVendaItemTabItem', DisplayName: 'Itens de Venda', ColumnSpan: 12, Visible: true, Items: [
	 {Name: 'Exemplos_GRIDs_com_macro_dGridVendaItens', DisplayName: '', ColumnSpan: 12, Visible: true, Items: [
	 {Id: 'Exemplos_GRIDs_com_macro_lUpVendaItem_IdVenda', Name: 'Exemplos_GRIDs_com_macro_dGridVendaItens_IdVenda', DisplayName: 'Id Venda', ColumnSpan: 8, Visible: true, Key: 'IdVenda'},
	 {Id: 'Exemplos_GRIDs_com_macro_cmbVendaItem_ComboboxVendaItem', Name: 'Exemplos_GRIDs_com_macro_dGridVendaItens_ComboboxVendaItem', DisplayName: 'Combobox Venda Item', ColumnSpan: 1, Visible: true, Key: 'ComboboxVendaItem'},
	 {Id: 'Exemplos_GRIDs_com_macro_dtVendaItem_DatetimeVendaItem', Name: 'Exemplos_GRIDs_com_macro_dGridVendaItens_DatetimeVendaItem', DisplayName: 'Datetime Venda Item', ColumnSpan: 3, Visible: true, Key: 'DatetimeVendaItem'},
	 {Id: 'Exemplos_GRIDs_com_macro_tbVendaItem_SmallIntVendaItem', Name: 'Exemplos_GRIDs_com_macro_dGridVendaItens_SmallIntVendaItem', DisplayName: 'Small Int Venda Item', ColumnSpan: 8, Visible: true, Key: 'SmallIntVendaItem'},
	 {Id: 'Exemplos_GRIDs_com_macro_ntxVendaItem_IntVendaItem', Name: 'Exemplos_GRIDs_com_macro_dGridVendaItens_IntVendaItem', DisplayName: 'Int Venda Item', ColumnSpan: 8, Visible: true, Key: 'IntVendaItem'},
	 {Id: 'Exemplos_GRIDs_com_macro_tbVendaItem_BitVendaItem', Name: 'Exemplos_GRIDs_com_macro_dGridVendaItens_BitVendaItem', DisplayName: 'Bit Venda Item', ColumnSpan: 8, Visible: true, Key: 'BitVendaItem'},
	 {Id: 'Exemplos_GRIDs_com_macro_tbVendaItem_BigIntVendaItem', Name: 'Exemplos_GRIDs_com_macro_dGridVendaItens_BigIntVendaItem', DisplayName: 'Big Int Venda Item', ColumnSpan: 8, Visible: true, Key: 'BigIntVendaItem'},
	 {Id: 'Exemplos_GRIDs_com_macro_ntxVendaItem_DecimalVendaItem', Name: 'Exemplos_GRIDs_com_macro_dGridVendaItens_DecimalVendaItem', DisplayName: 'Decimal Venda Item', ColumnSpan: 5, Visible: true, Key: 'DecimalVendaItem'},
	 {Id: 'Exemplos_GRIDs_com_macro_ntxVendaItem_IdVendaItem', Name: 'Exemplos_GRIDs_com_macro_dGridVendaItens_IdVendaItem', DisplayName: 'Id Venda Item', ColumnSpan: 8, Visible: true, Key: 'IdVendaItem'},
	 {Id: 'Exemplos_GRIDs_com_macro_tbVendaItem_StringVendaItem', Name: 'Exemplos_GRIDs_com_macro_dGridVendaItens_StringVendaItem', DisplayName: 'String Venda Item', ColumnSpan: 9, Visible: true, Key: 'StringVendaItem'},]},]},]},               ]
           }
           langResult.Items.concat(custom.getCustomTranslation().Items);
           return langResult;
        };
       return result;
  }
)

