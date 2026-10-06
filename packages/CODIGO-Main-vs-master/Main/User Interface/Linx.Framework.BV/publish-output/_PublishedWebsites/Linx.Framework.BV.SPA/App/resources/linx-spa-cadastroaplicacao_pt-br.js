/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/
define(['./linx-spa-cadastroaplicacao_custom_pt-br.js'],
	function (custom){
		var result = {};

       result.languageFile = function () {
       	return 'pt-br';
       }

result.objectLanguage_CadastroAplicacao = function () {
           var langResult = {
               Name: 'CadastroAplicacao', Items: [

	 {Name: "CadastroAplicacao_gbTcsAplicacao", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroAplicacao_gbGroupBox_6c45bf2b3f6542f6b38ce39ee9682942", DisplayName: "Aplicação", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroAplicacao_tbUidAplicacao", DisplayName: "Aplicação", ColumnSpan: 4, Visible: true, Key: "UidAplicacao"},
	 {Name: "CadastroAplicacao_tbDescricaoAplicacao", DisplayName: "", ColumnSpan: 8, Visible: true, Key: "DescricaoAplicacao"},
	 {Name: "CadastroAplicacao_lUpDescricaoAplicativo", DisplayName: "Aplicativo", ColumnSpan: 12, Visible: true, LookUpName: "LookUpTcsAplicativo", Key: "DescricaoAplicativo"},
	 {Name: "CadastroAplicacao_ckEmDesenvolvimento", DisplayName: "Em Desenvolvimento", ColumnSpan: 12, Visible: true, Key: "EmDesenvolvimento"},
	 {Name: "CadastroAplicacao_tbUrlWorkArea", DisplayName: "Url Work Area", ColumnSpan: 9, Visible: true, Key: "UrlWorkArea"},
	 {Name: "CadastroAplicacao_tbUrl", DisplayName: "Url", ColumnSpan: 12, Visible: true, Key: "Url"},]},
	 {Name: "CadastroAplicacao_tcTcsAplicacaoTabControl", DisplayName: "TcsAplicacao", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroAplicacao_tiTcsAplicacaoVersaoHistoricoTabItem", DisplayName: "Versão Histórico", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroAplicacao_dGridTcsAplicacaoVersaoHistorico", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroAplicacao_tbTcsAplicacaoVersaoHistorico_Versao", Name: "CadastroAplicacao_dGridTcsAplicacaoVersaoHistorico_Versao", DisplayName: "Versão", ColumnSpan: 8, Visible: true, Key: "Versao"},
	 {Id: "CadastroAplicacao_dtTcsAplicacaoVersaoHistorico_DataAtualizacao", Name: "CadastroAplicacao_dGridTcsAplicacaoVersaoHistorico_DataAtualizacao", DisplayName: "Data Atualização", ColumnSpan: 9, Visible: true, Key: "DataAtualizacao"},]},]},
	 {Name: "CadastroAplicacao_tiTcsAmbienteTabItem", DisplayName: "Ambientes", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroAplicacao_dGridTcsAmbiente", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroAplicacao_lUpTcsAmbiente_DescricaoAmbiente", Name: "CadastroAplicacao_dGridTcsAmbiente_DescricaoAmbiente", DisplayName: "Ambiente", ColumnSpan: 9, Visible: true, LookUpName: "LookUpTcsAmbiente", Key: "DescricaoAmbiente"},
	 {Id: "CadastroAplicacao_lUpTcsAmbiente_NomeEmpresa", Name: "CadastroAplicacao_dGridTcsAmbiente_NomeEmpresa", DisplayName: "Empresa", ColumnSpan: 9, Visible: true, LookUpName: "LookUpTcsEmpresaAutenticacao", Key: "NomeEmpresa"},
	 {Id: "CadastroAplicacao_tbTcsAmbiente_IdLinx", Name: "CadastroAplicacao_dGridTcsAmbiente_IdLinx", DisplayName: "Id Linx", ColumnSpan: 5, Visible: true, Key: "IdLinx"},]},]},]},]},               ]
           }
           langResult.Items.concat(custom.getCustomTranslation().Items);
           return langResult;
        };
       return result;
  }
)

