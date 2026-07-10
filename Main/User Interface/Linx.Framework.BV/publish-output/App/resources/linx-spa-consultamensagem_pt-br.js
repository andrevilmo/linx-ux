/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/
define(['./linx-spa-consultamensagem_custom_pt-br.js'],
	function (custom){
		var result = {};

       result.languageFile = function () {
       	return 'pt-br';
       }

result.objectLanguage_ConsultaMensagem = function () {
           var langResult = {
               Name: 'ConsultaMensagem', Items: [

	 {Name: "ConsultaMensagem_gbTcsMensagemConsulta", DisplayName: "Mensagem", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "ConsultaMensagem_gbGroupBox_2c9a396924094ab391b7c2d3da3604bc", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "ConsultaMensagem_tbTitulo", DisplayName: "Título", ColumnSpan: 12, Visible: true, Key: "Titulo"},
	 {Name: "ConsultaMensagem_cmbLxTipoMensagem", DisplayName: "Tipo Mensagem", ColumnSpan: 6, Visible: true, Key: "LxTipoMensagem"},
	 {Name: "ConsultaMensagem_dtCriacao", DisplayName: "Criação", ColumnSpan: 3, Visible: true, Key: "Criacao"},
	 {Name: "ConsultaMensagem_dtEnvio", DisplayName: "Envio", ColumnSpan: 3, Visible: true, Key: "Envio"},
	 {Name: "ConsultaMensagem_lUpNomeEmpresa", DisplayName: "Empresa", ColumnSpan: 6, Visible: true, LookUpName: "LookUpTcsEmpresaAutenticacaoC", Key: "NomeEmpresa"},
	 {Name: "ConsultaMensagem_lUpNomeUsuario", DisplayName: "Emitente", ColumnSpan: 6, Visible: true, LookUpName: "LookUpTcsUsuarioAutenticacaoC", Key: "NomeUsuario"},]},
	 {Name: "ConsultaMensagem_tcTabControl_eb3f696999dd454ba38a842c72390b0d", DisplayName: "TabControl", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "ConsultaMensagem_tiTabItem_2430f69f10e540d9bec00108a95052af", DisplayName: "Corpo", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "ConsultaMensagem_gbGroupBox_8584be30d85d4437a293c038494e42df", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "ConsultaMensagem_divCorpo", DisplayName: "", ColumnSpan: 12, Visible: true, Key: ""},]},]},
	 {Name: "ConsultaMensagem_tiTabItem_d6c33d4e05584d5ca00acdbc8261dfff", DisplayName: "Destinatários", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "ConsultaMensagem_gbGroupBox_0e1ace88b6554234ad34aab931a0479f", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "ConsultaMensagem_dGridTcsMensagemConsultaLog", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "ConsultaMensagem_lUpTcsMensagemConsultaLog_NomeUsuario", Name: "ConsultaMensagem_dGridTcsMensagemConsultaLog_NomeUsuario", DisplayName: "Usuário", ColumnSpan: 9, Visible: true, LookUpName: "LookUpTcsUsuarioAutenticacaoCL", Key: "NomeUsuario"},
	 {Id: "ConsultaMensagem_dtTcsMensagemConsultaLog_Entregue", Name: "ConsultaMensagem_dGridTcsMensagemConsultaLog_Entregue", DisplayName: "Entregue", ColumnSpan: 3, Visible: true, Key: "Entregue"},
	 {Id: "ConsultaMensagem_dtTcsMensagemConsultaLog_Lida", Name: "ConsultaMensagem_dGridTcsMensagemConsultaLog_Lida", DisplayName: "Lida", ColumnSpan: 3, Visible: true, Key: "Lida"},
	 {Id: "ConsultaMensagem_dtTcsMensagemConsultaLog_Dispensada", Name: "ConsultaMensagem_dGridTcsMensagemConsultaLog_Dispensada", DisplayName: "Dispensada", ColumnSpan: 3, Visible: true, Key: "Dispensada"},]},]},]},]},]},               ]
           }
           langResult.Items.concat(custom.getCustomTranslation().Items);
           return langResult;
        };
       return result;
  }
)

