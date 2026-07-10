/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/
define(['./linx-spa-cadastroobjetoautorizacao_custom_pt-br.js'],
	function (custom){
		var result = {};

       result.languageFile = function () {
       	return 'pt-br';
       }

result.objectLanguage_CadastroObjetoAutorizacao = function () {
           var langResult = {
               Name: 'CadastroObjetoAutorizacao', Items: [

	 {Name: "CadastroObjetoAutorizacao_gbTcsObjetoAutorizacao", DisplayName: "Detalhes do Objeto", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroObjetoAutorizacao_gbGroupBox_da1ffb4c602347b092abcbcce6a769ba", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroObjetoAutorizacao_tbClasseNome", DisplayName: "Classe", ColumnSpan: 12, Visible: true, Key: "ClasseNome"},
	 {Name: "CadastroObjetoAutorizacao_tbDescObjeto", DisplayName: "Descrição", ColumnSpan: 12, Visible: true, Key: "DescObjeto"},
	 {Name: "CadastroObjetoAutorizacao_cmbLxTipoObjeto", DisplayName: "Tipo Objeto", ColumnSpan: 12, Visible: true, Key: "LxTipoObjeto"},]},
	 {Name: "CadastroObjetoAutorizacao_tcTcsObjetoAutorizacaoTabControl", DisplayName: "TcsObjetoAutorizacao", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroObjetoAutorizacao_tiTcsTransacaoAutorizacaoChildTabItem", DisplayName: "Transação", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroObjetoAutorizacao_dGridTcsTransacaoAutorizacaoChild", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroObjetoAutorizacao_tbTcsTransacaoAutorizacaoChild_CodTransacao", Name: "CadastroObjetoAutorizacao_dGridTcsTransacaoAutorizacaoChild_CodTransacao", DisplayName: "Código", ColumnSpan: 3, Visible: true, Key: "CodTransacao"},
	 {Id: "CadastroObjetoAutorizacao_tbTcsTransacaoAutorizacaoChild_DescTransacao", Name: "CadastroObjetoAutorizacao_dGridTcsTransacaoAutorizacaoChild_DescTransacao", DisplayName: "Descrição", ColumnSpan: 9, Visible: true, Key: "DescTransacao"},
	 {Id: "CadastroObjetoAutorizacao_tbTcsTransacaoAutorizacaoChild_ClasseNome", Name: "CadastroObjetoAutorizacao_dGridTcsTransacaoAutorizacaoChild_ClasseNome", DisplayName: "Classe", ColumnSpan: 9, Visible: true, Key: "ClasseNome"},
	 {Id: "CadastroObjetoAutorizacao_ckTcsTransacaoAutorizacaoChild_Inativo", Name: "CadastroObjetoAutorizacao_dGridTcsTransacaoAutorizacaoChild_Inativo", DisplayName: "Inativo", ColumnSpan: 3, Visible: true, Key: "Inativo"},
	 {Id: "CadastroObjetoAutorizacao_cmbTcsTransacaoAutorizacaoChild_LxTipoTransacao", Name: "CadastroObjetoAutorizacao_dGridTcsTransacaoAutorizacaoChild_LxTipoTransacao", DisplayName: "Tipo Transação", ColumnSpan: 6, Visible: true, Key: "LxTipoTransacao"},]},]},
	 {Name: "CadastroObjetoAutorizacao_tiTcsObjetoAutorizacaoLayoutTabItem", DisplayName: "Layouts", ColumnSpan: 12, Visible: false, Items: [
	 {Name: "CadastroObjetoAutorizacao_dGridTcsObjetoConteudoAutorizacao", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroObjetoAutorizacao_tbTcsObjetoConteudoAutorizacao_DescLayout", Name: "CadastroObjetoAutorizacao_dGridTcsObjetoConteudoAutorizacao_DescLayout", DisplayName: "Descrição", ColumnSpan: 9, Visible: true, Key: "DescLayout"},
	 {Id: "CadastroObjetoAutorizacao_cmbTcsObjetoConteudoAutorizacao_LxTipoLayout", Name: "CadastroObjetoAutorizacao_dGridTcsObjetoConteudoAutorizacao_LxTipoLayout", DisplayName: "Tipo", ColumnSpan: 6, Visible: true, Key: "LxTipoLayout"},
	 {Id: "CadastroObjetoAutorizacao_cmbTcsObjetoConteudoAutorizacao_LxConteudoObjeto", Name: "CadastroObjetoAutorizacao_dGridTcsObjetoConteudoAutorizacao_LxConteudoObjeto", DisplayName: "Tipo Conteúdo", ColumnSpan: 6, Visible: true, Key: "LxConteudoObjeto"},
	 {Id: "CadastroObjetoAutorizacao_ckTcsObjetoConteudoAutorizacao_Inativo", Name: "CadastroObjetoAutorizacao_dGridTcsObjetoConteudoAutorizacao_Inativo", DisplayName: "Inativo", ColumnSpan: 3, Visible: true, Key: "Inativo"},
	 {Id: "CadastroObjetoAutorizacao_ckTcsObjetoConteudoAutorizacao_LayoutPadrao", Name: "CadastroObjetoAutorizacao_dGridTcsObjetoConteudoAutorizacao_LayoutPadrao", DisplayName: "Padrão", ColumnSpan: 3, Visible: true, Key: "LayoutPadrao"},
	 {Id: "CadastroObjetoAutorizacao_ckTcsObjetoConteudoAutorizacao_PossuiFiltro", Name: "CadastroObjetoAutorizacao_dGridTcsObjetoConteudoAutorizacao_PossuiFiltro", DisplayName: "Possui Filtro", ColumnSpan: 5, Visible: true, Key: "PossuiFiltro"},
	 {Id: "CadastroObjetoAutorizacao_tbTcsObjetoConteudoAutorizacao_Idioma", Name: "CadastroObjetoAutorizacao_dGridTcsObjetoConteudoAutorizacao_Idioma", DisplayName: "Idioma", ColumnSpan: 5, Visible: true, Key: "Idioma"},
	 {Id: "CadastroObjetoAutorizacao_dtTcsObjetoConteudoAutorizacao_UltAtualizacao", Name: "CadastroObjetoAutorizacao_dGridTcsObjetoConteudoAutorizacao_UltAtualizacao", DisplayName: "Última Atualização", ColumnSpan: 3, Visible: true, Key: "UltAtualizacao"},
	 {Id: "CadastroObjetoAutorizacao_edTcsObjetoConteudoAutorizacao_Detalhes", Name: "CadastroObjetoAutorizacao_dGridTcsObjetoConteudoAutorizacao_Detalhes", DisplayName: "Detalhes", ColumnSpan: 9, Visible: true, Key: "Detalhes"},]},]},]},]},               ]
           }
           langResult.Items.concat(custom.getCustomTranslation().Items);
           return langResult;
        };
       return result;
  }
)

