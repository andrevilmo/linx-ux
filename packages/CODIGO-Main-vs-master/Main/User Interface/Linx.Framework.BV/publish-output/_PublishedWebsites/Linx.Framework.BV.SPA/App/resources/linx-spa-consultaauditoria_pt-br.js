/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/
define(['./linx-spa-consultaauditoria_custom_pt-br.js'],
	function (custom){
		var result = {};

       result.languageFile = function () {
       	return 'pt-br';
       }

result.objectLanguage_ConsultaAuditoria = function () {
           var langResult = {
               Name: 'ConsultaAuditoria', Items: [

	 {Name: "ConsultaAuditoria_gbAdtAuditoria", DisplayName: "Informações Gerais", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "ConsultaAuditoria_tbIdAdtAuditoria", DisplayName: "Id Adt Auditoria", ColumnSpan: 2, Visible: true, Key: "IdAdtAuditoria"},
	 {Name: "ConsultaAuditoria_dtDataHora", DisplayName: "Data Hora", ColumnSpan: 4, Visible: true, Key: "DataHora"},
	 {Name: "ConsultaAuditoria_edAssemblyName", DisplayName: "Assembly Name", ColumnSpan: 6, Visible: true, Key: "AssemblyName"},
	 {Name: "ConsultaAuditoria_edConnectionString", DisplayName: "Connection String", ColumnSpan: 6, Visible: true, Key: "ConnectionString"},
	 {Name: "ConsultaAuditoria_lUpIdUsuario", DisplayName: "Id Usuario", ColumnSpan: 2, Visible: true, LookUpName: "LookUpTcsUsuario", Key: "IdUsuario"},
	 {Name: "ConsultaAuditoria_lUpEmail", DisplayName: "Email", ColumnSpan: 9, Visible: false, LookUpName: "LookUpTcsUsuario", Key: "Email"},
	 {Name: "ConsultaAuditoria_lUpNomeUsuario", DisplayName: "Nome Usuario", ColumnSpan: 4, Visible: true, LookUpName: "LookUpTcsUsuario", Key: "NomeUsuario"},]},
	 {Name: "ConsultaAuditoria_gbExpander_8e2773cc416f400cbcc3ebd4b2206627", DisplayName: "Registros", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "ConsultaAuditoria_dGridAdtAuditoriaItem", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "ConsultaAuditoria_tbAdtAuditoriaItem_IdAdtAuditoriaItem", Name: "ConsultaAuditoria_dGridAdtAuditoriaItem_IdAdtAuditoriaItem", DisplayName: "Id Adt Auditoria Item", ColumnSpan: 8, Visible: false, Key: "IdAdtAuditoriaItem"},
	 {Id: "ConsultaAuditoria_cmbAdtAuditoriaItem_TipoOperacao", Name: "ConsultaAuditoria_dGridAdtAuditoriaItem_TipoOperacao", DisplayName: "Tipo Operacao", ColumnSpan: 2, Visible: true, Key: "TipoOperacao"},
	 {Id: "ConsultaAuditoria_tbAdtAuditoriaItem_SchemaTabela", Name: "ConsultaAuditoria_dGridAdtAuditoriaItem_SchemaTabela", DisplayName: "Schema Tabela", ColumnSpan: 3, Visible: true, Key: "SchemaTabela"},
	 {Id: "ConsultaAuditoria_tbAdtAuditoriaItem_NomeTabela", Name: "ConsultaAuditoria_dGridAdtAuditoriaItem_NomeTabela", DisplayName: "Nome Tabela", ColumnSpan: 7, Visible: true, Key: "NomeTabela"},
	 {Id: "ConsultaAuditoria_lUpAdtAuditoriaItem_IdAdtAuditoria", Name: "ConsultaAuditoria_dGridAdtAuditoriaItem_IdAdtAuditoria", DisplayName: "Id Adt Auditoria", ColumnSpan: 6, Visible: false, Key: "IdAdtAuditoria"},]},]},
	 {Name: "ConsultaAuditoria_gbExpander_471ab544826341a0a9f839429b5ede95", DisplayName: "Detalhe", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "ConsultaAuditoria_euiExternalUI_854ebd27fdec4195bbdddbc25ed4aca8", DisplayName: "", ColumnSpan: 12, Visible: true, },]},               ]
           }
           langResult.Items.concat(custom.getCustomTranslation().Items);
           return langResult;
        };
       return result;
  }
)

