/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/
define(['./linx-spa-cadastroparametroautorizacao_custom_pt-br.js'],
	function (custom){
		var result = {};

       result.languageFile = function () {
       	return 'pt-br';
       }

result.objectLanguage_CadastroParametroAutorizacao = function () {
           var langResult = {
               Name: 'CadastroParametroAutorizacao', Items: [

	 {Name: "CadastroParametroAutorizacao_gbTcsParametroAutorizacao", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroParametroAutorizacao_gbGroupBox_03ab6b92c1a6418aabe1f7c7b17a32e6", DisplayName: "Informações do Parâmetro", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroParametroAutorizacao_tbTituloParametro", DisplayName: "Título", ColumnSpan: 12, Visible: true, Key: "TituloParametro"},
	 {Name: "CadastroParametroAutorizacao_tbDescParametro", DisplayName: "Descrição", ColumnSpan: 12, Visible: true, Key: "DescParametro"},
	 {Name: "CadastroParametroAutorizacao_lUpDescGrupoParametro", DisplayName: "Grupo", ColumnSpan: 12, Visible: true, LookUpName: "LookUpTcsParametroGrupoAutorizacao", Key: "DescGrupoParametro"},
	 {Name: "CadastroParametroAutorizacao_lUpIdTcsAplicativo", DisplayName: "Aplicativo", ColumnSpan: 2, Visible: true, LookUpName: "LookUpTcsAplicativo", Key: "IdTcsAplicativo"},
	 {Name: "CadastroParametroAutorizacao_lUpDescricaoAplicativo", DisplayName: "Aplicativo", ColumnSpan: 10, Visible: true, LookUpName: "LookUpTcsAplicativo", Key: "DescricaoAplicativo"},
	 {Name: "CadastroParametroAutorizacao_cmbLxDatatypeParametro", DisplayName: "Tipo do Dado", ColumnSpan: 12, Visible: true, Key: "LxDatatypeParametro"},
	 {Name: "CadastroParametroAutorizacao_cmbLxTipoValidacaoParametro", DisplayName: "Tipo Validação", ColumnSpan: 12, Visible: true, Key: "LxTipoValidacaoParametro"},
	 {Name: "CadastroParametroAutorizacao_ntxNivelAcesso", DisplayName: "Nível Acesso Visualização", ColumnSpan: 12, Visible: true, Key: "NivelAcesso"},
	 {Name: "CadastroParametroAutorizacao_ntxNivelAcessoEdicao", DisplayName: "Nível Acesso Edição", ColumnSpan: 12, Visible: true, Key: "NivelAcessoEdicao"},
	 {Name: "CadastroParametroAutorizacao_ckPermiteVariacaoPorEntidade", DisplayName: "Permite Variação por Entidade", ColumnSpan: 12, Visible: true, Key: "PermiteVariacaoPorEntidade"},
	 {Name: "CadastroParametroAutorizacao_ckIndicaEnviaPdv", DisplayName: "Envia PDV", ColumnSpan: 12, Visible: true, Key: "IndicaEnviaPdv"},
	 {Name: "CadastroParametroAutorizacao_edObsParametro", DisplayName: "Obs", ColumnSpan: 12, Visible: true, Key: "ObsParametro"},]},
	 {Name: "CadastroParametroAutorizacao_gbvariacoesPermitidas", DisplayName: "Variação", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroParametroAutorizacao_dGridTcsParametroTabelaSelecaoAutorizacao", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroParametroAutorizacao_lUpTcsParametroTabelaSelecaoAutorizacao_NomeTabela", Name: "CadastroParametroAutorizacao_dGridTcsParametroTabelaSelecaoAutorizacao_NomeTabela", DisplayName: "Nome Tabela", ColumnSpan: 9, Visible: true, LookUpName: "LookUpTcsTabelaAutorizacaoSelecao", Key: "NomeTabela"},
	 {Id: "CadastroParametroAutorizacao_lUpTcsParametroTabelaSelecaoAutorizacao_DescTabela", Name: "CadastroParametroAutorizacao_dGridTcsParametroTabelaSelecaoAutorizacao_DescTabela", DisplayName: "Descrição", ColumnSpan: 9, Visible: true, LookUpName: "LookUpTcsTabelaAutorizacaoSelecao", Key: "DescTabela"},
	 {Id: "CadastroParametroAutorizacao_cmbTcsParametroTabelaSelecaoAutorizacao_LxParametroHierarquia", Name: "CadastroParametroAutorizacao_dGridTcsParametroTabelaSelecaoAutorizacao_LxParametroHierarquia", DisplayName: "Hierarquia", ColumnSpan: 6, Visible: true, Key: "LxParametroHierarquia"},]},]},]},               ]
           }
           langResult.Items.concat(custom.getCustomTranslation().Items);
           return langResult;
        };
       return result;
  }
)

