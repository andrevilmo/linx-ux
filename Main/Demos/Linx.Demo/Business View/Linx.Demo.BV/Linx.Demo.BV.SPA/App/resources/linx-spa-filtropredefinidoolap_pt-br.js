/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/
define(['./linx-spa-filtropredefinidoolap_custom_pt-br.js'],
	function (custom){
		var result = {};

       result.languageFile = function () {
       	return 'pt-br';
       }

result.objectLanguage_FiltroPreDefinidoOLAP = function () {
           var langResult = {
               Name: 'FiltroPreDefinidoOLAP', Items: [

	 {Name: 'FiltroPreDefinidoOLAP_gbEntityAdapter1', DisplayName: 'EntityAdapter1', ColumnSpan: 0, Visible: true, Items: [
	 {Name: 'FiltroPreDefinidoOLAP_lUpIdBandeiraRede', DisplayName: 'Id Bandeira Rede', ColumnSpan: 0, Visible: true, LookUpName: 'LookUpEntityAdapter1IdBandeiraRede', Key: 'IdBandeiraRede'},
	 {Name: 'FiltroPreDefinidoOLAP_lUpLoja', DisplayName: 'Loja', ColumnSpan: 0, Visible: true, LookUpName: 'LookUpEntityAdapter1Loja', Key: 'Loja'},
	 {Name: 'FiltroPreDefinidoOLAP_lUpCodLoja', DisplayName: 'Cod Loja', ColumnSpan: 0, Visible: true, LookUpName: 'LookUpEntityAdapter1CodLoja', Key: 'CodLoja'},
	 {Name: 'FiltroPreDefinidoOLAP_lUpCliente', DisplayName: 'Cliente', ColumnSpan: 0, Visible: true, LookUpName: 'LookUpEntityAdapter1Cliente', Key: 'Cliente'},
	 {Name: 'FiltroPreDefinidoOLAP_dtData', DisplayName: 'Data', ColumnSpan: 0, Visible: true, Key: 'Data'},
	 {Name: 'FiltroPreDefinidoOLAP_ntxQtdItemBruto', DisplayName: 'Qtd Item Bruto', ColumnSpan: 0, Visible: true, Key: 'QtdItemBruto'},
	 {Name: 'FiltroPreDefinidoOLAP_ntxVlrItemPago', DisplayName: 'Vlr Item Pago', ColumnSpan: 0, Visible: true, Key: 'VlrItemPago'},]},               ]
           }
           langResult.Items.concat(custom.getCustomTranslation().Items);
           return langResult;
        };
       return result;
  }
)

