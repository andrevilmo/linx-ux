/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/
define(['./linx-spa-consultalogerros_custom_pt-br.js'],
	function (custom){
		var result = {};

       result.languageFile = function () {
       	return 'pt-br';
       }

result.objectLanguage_ConsultaLogErros = function () {
           var langResult = {
               Name: 'ConsultaLogErros', Items: [

	 {Name: "ConsultaLogErros_gbTcsLogErrosDash", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "ConsultaLogErros_gbGroupBox_b8f266f849874696af6b2b21705c72a5", DisplayName: "Log de Erros", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "ConsultaLogErros_dtDataErro", DisplayName: "Data", ColumnSpan: 3, Visible: true, Key: "DataErro"},
	 {Name: "ConsultaLogErros_tbNomeAcao", DisplayName: "Ação", ColumnSpan: 12, Visible: true, Key: "NomeAcao"},
	 {Name: "ConsultaLogErros_tbNomeControlador", DisplayName: "Controlador", ColumnSpan: 12, Visible: true, Key: "NomeControlador"},
	 {Name: "ConsultaLogErros_tbMetodoHttp", DisplayName: "Método Http", ColumnSpan: 12, Visible: true, Key: "MetodoHttp"},
	 {Name: "ConsultaLogErros_lUpNomeUsuario", DisplayName: "Usuário", ColumnSpan: 12, Visible: true, LookUpName: "LookUpTcsUsuarioAutenticacao", Key: "NomeUsuario"},
	 {Name: "ConsultaLogErros_lUpNomeAutenticacao", DisplayName: "Nome Autenticação", ColumnSpan: 12, Visible: true, LookUpName: "LookUpTcsUsuarioAutenticacao", Key: "NomeAutenticacao"},
	 {Name: "ConsultaLogErros_lUpDescricaoAmbiente", DisplayName: "Ambiente", ColumnSpan: 12, Visible: true, LookUpName: "LookUpTcsAmbiente", Key: "DescricaoAmbiente"},
	 {Name: "ConsultaLogErros_lUpDescricaoAplicacao", DisplayName: "Aplicação", ColumnSpan: 12, Visible: true, LookUpName: "LookUpTcsAplicacao", Key: "DescricaoAplicacao"},
	 {Name: "ConsultaLogErros_lUpNomeEmpresa", DisplayName: "Empresa", ColumnSpan: 12, Visible: true, LookUpName: "LookUpTcsEmpresaAutenticacao", Key: "NomeEmpresa"},
	 {Name: "ConsultaLogErros_lUpGpecon", DisplayName: "Grupo Econômico", ColumnSpan: 12, Visible: true, LookUpName: "LookUpGpecon", Key: "Gpecon"},
	 {Name: "ConsultaLogErros_tbNomeServidor", DisplayName: "Servidor", ColumnSpan: 12, Visible: true, Key: "NomeServidor"},
	 {Name: "ConsultaLogErros_tbUsuarioWindows", DisplayName: "Usuário Servidor", ColumnSpan: 12, Visible: true, Key: "UsuarioWindows"},]},
	 {Name: "ConsultaLogErros_tcTcsLogErrosDashTabControl", DisplayName: "TcsLogErrosDash", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "ConsultaLogErros_tiTcsLogErrosTabItem", DisplayName: "Banco de Dados", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "ConsultaLogErros_gbGroupBox_5bceff6b3fb54366baaa622bbb556baa", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "ConsultaLogErros_dGridTcsLogErros", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "ConsultaLogErros_dtTcsLogErros_DataErro", Name: "ConsultaLogErros_dGridTcsLogErros_DataErro", DisplayName: "Data", ColumnSpan: 3, Visible: true, Key: "DataErro"},
	 {Id: "ConsultaLogErros_tbTcsLogErros_NomeAcao", Name: "ConsultaLogErros_dGridTcsLogErros_NomeAcao", DisplayName: "Ação", ColumnSpan: 9, Visible: true, Key: "NomeAcao"},
	 {Id: "ConsultaLogErros_tbTcsLogErros_NomeControlador", Name: "ConsultaLogErros_dGridTcsLogErros_NomeControlador", DisplayName: "Controlador", ColumnSpan: 9, Visible: true, Key: "NomeControlador"},
	 {Id: "ConsultaLogErros_tbTcsLogErros_EnderecoWeb", Name: "ConsultaLogErros_dGridTcsLogErros_EnderecoWeb", DisplayName: "Endereço Web", ColumnSpan: 9, Visible: true, Key: "EnderecoWeb"},
	 {Id: "ConsultaLogErros_tbTcsLogErros_MetodoHttp", Name: "ConsultaLogErros_dGridTcsLogErros_MetodoHttp", DisplayName: "Método Http", ColumnSpan: 2, Visible: true, Key: "MetodoHttp"},
	 {Id: "ConsultaLogErros_tbTcsLogErros_NomeUsuario", Name: "ConsultaLogErros_dGridTcsLogErros_NomeUsuario", DisplayName: "Usuário", ColumnSpan: 9, Visible: true, Key: "NomeUsuario"},
	 {Id: "ConsultaLogErros_tbTcsLogErros_NomeAutenticacao", Name: "ConsultaLogErros_dGridTcsLogErros_NomeAutenticacao", DisplayName: "Nome Autenticação", ColumnSpan: 9, Visible: true, Key: "NomeAutenticacao"},
	 {Id: "ConsultaLogErros_tbTcsLogErros_DescricaoAmbiente", Name: "ConsultaLogErros_dGridTcsLogErros_DescricaoAmbiente", DisplayName: "Ambiente", ColumnSpan: 9, Visible: true, Key: "DescricaoAmbiente"},
	 {Id: "ConsultaLogErros_tbAplicação", Name: "ConsultaLogErros_dGridTcsLogErros_DescricaoAplicacao", DisplayName: "Aplicação", ColumnSpan: 9, Visible: true, Key: "DescricaoAplicacao"},
	 {Id: "ConsultaLogErros_tbTcsLogErros_Empresa", Name: "ConsultaLogErros_dGridTcsLogErros_Empresa", DisplayName: "Empresa", ColumnSpan: 9, Visible: true, Key: "Empresa"},
	 {Id: "ConsultaLogErros_tbTcsLogErros_Gpecon", Name: "ConsultaLogErros_dGridTcsLogErros_Gpecon", DisplayName: "Grupo Econômico", ColumnSpan: 9, Visible: true, Key: "Gpecon"},
	 {Id: "ConsultaLogErros_tbServidor", Name: "ConsultaLogErros_dGridTcsLogErros_NomeServidor", DisplayName: "Servidor", ColumnSpan: 9, Visible: true, Key: "NomeServidor"},
	 {Id: "ConsultaLogErros_tbUsuário Servidor", Name: "ConsultaLogErros_dGridTcsLogErros_UsuarioWindows", DisplayName: "Usuário Servidor", ColumnSpan: 9, Visible: true, Key: "UsuarioWindows"},]},]},
	 {Name: "ConsultaLogErros_gbGroupBox_2277086a561642039f356d5f963ee022", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "ConsultaLogErros_edTcsLogErros_MensagemExcecao", DisplayName: "Exceção", ColumnSpan: 12, Visible: true, Key: "MensagemExcecao"},
	 {Name: "ConsultaLogErros_edTcsLogErros_MensagemExcecaoInterna", DisplayName: "Exceção Interna", ColumnSpan: 12, Visible: true, Key: "MensagemExcecaoInterna"},
	 {Name: "ConsultaLogErros_edTcsLogErros_PilhaExcecao", DisplayName: "Pilha Exceção", ColumnSpan: 12, Visible: true, Key: "PilhaExcecao"},]},]},
	 {Name: "ConsultaLogErros_tiLogFileTabItem", DisplayName: "Arquivo", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "ConsultaLogErros_gbGroupBox_1004712e8c124aff85dfa2ca1b8cef33", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "ConsultaLogErros_btnDeleteAll", DisplayName: "Apagar Aquivos de Log", ColumnSpan: 12, Visible: true, Key: ""},]},
	 {Name: "ConsultaLogErros_gbGroupBox_eb6a453795344884a4a31d11b8c1e4de", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "ConsultaLogErros_dGridLogFile", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "ConsultaLogErros_tbLogFile_FileName", Name: "ConsultaLogErros_dGridLogFile_FileName", DisplayName: "", ColumnSpan: 9, Visible: true, Key: "FileName"},
	 {Id: "ConsultaLogErros_tbLogFile_Download", Name: "ConsultaLogErros_dGridLogFile_Download", DisplayName: "", ColumnSpan: 9, Visible: true, Key: "Download"},]},]},]},]},]},               ]
           }
           langResult.Items.concat(custom.getCustomTranslation().Items);
           return langResult;
        };
       return result;
  }
)

