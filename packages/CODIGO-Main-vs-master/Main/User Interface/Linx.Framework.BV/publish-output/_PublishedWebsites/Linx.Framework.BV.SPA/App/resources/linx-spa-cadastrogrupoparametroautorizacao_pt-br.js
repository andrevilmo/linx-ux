/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/
define(['./linx-spa-cadastrogrupoparametroautorizacao_custom_pt-br.js'],
	function (custom){
		var result = {};

       result.languageFile = function () {
       	return 'pt-br';
       }

result.objectLanguage_CadastroGrupoParametroAutorizacao = function () {
           var langResult = {
               Name: 'CadastroGrupoParametroAutorizacao', Items: [

	 {Name: "CadastroGrupoParametroAutorizacao_gbCustomContainer_ac966a32cddd482bbbf9db003d2a46df", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroGrupoParametroAutorizacao_gbGroupBox_270d26a877c5457990a39ce04af034bb", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroGrupoParametroAutorizacao_tbDescGrupoParametro", DisplayName: "Descrição", ColumnSpan: 6, Visible: true, Key: "DescGrupoParametro"},]},
	 {Name: "CadastroGrupoParametroAutorizacao_tcTcsParametroGrupoAutorizacaoTabControl", DisplayName: "TcsParametroGrupoAutorizacao", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroGrupoParametroAutorizacao_tiTcsParametroAutorizacaoTabItem", DisplayName: "Parâmetros", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroGrupoParametroAutorizacao_dGridTcsParametroAutorizacao", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroGrupoParametroAutorizacao_tbTcsParametroAutorizacaoGrupo_TituloParametro", Name: "CadastroGrupoParametroAutorizacao_dGridTcsParametroAutorizacao_TituloParametro", DisplayName: "Título", ColumnSpan: 9, Visible: true, Key: "TituloParametro"},
	 {Id: "CadastroGrupoParametroAutorizacao_tbTcsParametroAutorizacaoGrupo_DescParametro", Name: "CadastroGrupoParametroAutorizacao_dGridTcsParametroAutorizacao_DescParametro", DisplayName: "Descrição", ColumnSpan: 9, Visible: true, Key: "DescParametro"},]},]},]},]},               ]
           }
           langResult.Items.concat(custom.getCustomTranslation().Items);
           return langResult;
        };
       return result;
  }
)

