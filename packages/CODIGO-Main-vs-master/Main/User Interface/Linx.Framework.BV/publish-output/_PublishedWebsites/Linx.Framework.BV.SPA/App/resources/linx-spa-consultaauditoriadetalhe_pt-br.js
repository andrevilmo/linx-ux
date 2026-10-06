/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/
define(['./linx-spa-consultaauditoriadetalhe_custom_pt-br.js'],
	function (custom){
		var result = {};

       result.languageFile = function () {
       	return 'pt-br';
       }

result.objectLanguage_ConsultaAuditoriaDetalhe = function () {
           var langResult = {
               Name: 'ConsultaAuditoriaDetalhe', Items: [

	 {Name: "ConsultaAuditoriaDetalhe_gbAdtAuditoriaItemDetalhe", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "ConsultaAuditoriaDetalhe_tbIdAdtAuditoriaItem", DisplayName: "Id Adt Auditoria Item", ColumnSpan: 8, Visible: false, Key: "IdAdtAuditoriaItem"},
	 {Name: "ConsultaAuditoriaDetalhe_tbIdAdtAuditoriaItemDetalhe", DisplayName: "Id Adt Auditoria Item Detalhe", ColumnSpan: 8, Visible: false, Key: "IdAdtAuditoriaItemDetalhe"},
	 {Name: "ConsultaAuditoriaDetalhe_tbPropriedade", DisplayName: "Propriedade", ColumnSpan: 9, Visible: true, Key: "Propriedade"},
	 {Name: "ConsultaAuditoriaDetalhe_edValorAntigo", DisplayName: "Valor Antigo", ColumnSpan: 6, Visible: true, Key: "ValorAntigo"},
	 {Name: "ConsultaAuditoriaDetalhe_edValorNovo", DisplayName: "Valor Novo", ColumnSpan: 6, Visible: true, Key: "ValorNovo"},]},               ]
           }
           langResult.Items.concat(custom.getCustomTranslation().Items);
           return langResult;
        };
       return result;
  }
)

