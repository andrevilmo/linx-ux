/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/
define(['./linx-spa-cadastrotransacaoautorizacao_custom_pt-br.js'],
	function (custom){
		var result = {};

       result.languageFile = function () {
       	return 'pt-br';
       }

result.objectLanguage_CadastroTransacaoAutorizacao = function () {
           var langResult = {
               Name: 'CadastroTransacaoAutorizacao', Items: [

	 {Name: "CadastroTransacaoAutorizacao_gbTcsTransacaoAutorizacao", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroTransacaoAutorizacao_gbGroupBox_2301039ed57d4632a47a218a8b7a6154", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroTransacaoAutorizacao_gbGroupBox_18825af1a21c44f796f6417ba7f6b489", DisplayName: "Transação", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroTransacaoAutorizacao_tbCodTransacao", DisplayName: "Código", ColumnSpan: 2, Visible: true, Key: "CodTransacao"},
	 {Name: "CadastroTransacaoAutorizacao_lblCustomControl1221833015", DisplayName: "", ColumnSpan: 10, Visible: true, Key: ""},
	 {Name: "CadastroTransacaoAutorizacao_tbNomeCurto", DisplayName: "Descrição", ColumnSpan: 6, Visible: true, Key: "NomeCurto"},
	 {Name: "CadastroTransacaoAutorizacao_tbDescTransacao", DisplayName: "Descrição Detalhada", ColumnSpan: 12, Visible: true, Key: "DescTransacao"},
	 {Name: "CadastroTransacaoAutorizacao_tbClasseNome", DisplayName: "Formulário / Url", ColumnSpan: 12, Visible: true, Key: "ClasseNome"},
	 {Name: "CadastroTransacaoAutorizacao_ckInativo", DisplayName: "Inativo", ColumnSpan: 12, Visible: true, Key: "Inativo"},
	 {Name: "CadastroTransacaoAutorizacao_lUpDescObjeto", DisplayName: "Classe BO", ColumnSpan: 12, Visible: true, LookUpName: "LookUpTcsObjetoAutorizacao", Key: "DescObjeto"},
	 {Name: "CadastroTransacaoAutorizacao_cmbLxTipoTransacao", DisplayName: "Tipo Transação", ColumnSpan: 12, Visible: true, Key: "LxTipoTransacao"},
	 {Name: "CadastroTransacaoAutorizacao_lUpDescModulo", DisplayName: "Módulo Base", ColumnSpan: 12, Visible: true, LookUpName: "LookUpTcsModuloAutorizacao", Key: "DescModulo"},]},
	 {Name: "CadastroTransacaoAutorizacao_gbGroupBox_098d5baf71d14700908a226c75d1538d", DisplayName: "Apresentação", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroTransacaoAutorizacao_gbGroupBox_048a2d66f0634d4481cf766ca74f3c5d", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroTransacaoAutorizacao_tbIcone", DisplayName: "Ícone", ColumnSpan: 6, Visible: true, Key: "Icone"},]},]},
	 {Name: "CadastroTransacaoAutorizacao_gbGroupBox_a4f0c309e4514843bea82038f093d714", DisplayName: "Tags", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroTransacaoAutorizacao_CustomControl443185546", DisplayName: "", ColumnSpan: 12, Visible: true, Key: ""},]},]},
	 {Name: "CadastroTransacaoAutorizacao_gbGroupBox_b8bc554c82bb4e958f1a2ca53f1afde1", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroTransacaoAutorizacao_tcTcsTransacaoAutorizacaoTabControl", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroTransacaoAutorizacao_tiTcsTransacaoMenuAutorizacaoTabItem", DisplayName: "Menu", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroTransacaoAutorizacao_dGridTcsTransacaoMenuAutorizacao", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroTransacaoAutorizacao_lUpTcsTransacaoMenuAutorizacao_DescModulo", Name: "CadastroTransacaoAutorizacao_dGridTcsTransacaoMenuAutorizacao_DescModulo", DisplayName: "Módulo", ColumnSpan: 9, Visible: true, LookUpName: "LookUpTcsModuloMenuAutorizacao", Key: "DescModulo"},
	 {Id: "CadastroTransacaoAutorizacao_tbTcsTransacaoMenuAutorizacao_DescricaoAplicativo", Name: "CadastroTransacaoAutorizacao_dGridTcsTransacaoMenuAutorizacao_DescricaoAplicativo", DisplayName: "Aplicativo", ColumnSpan: 9, Visible: true, Key: "DescricaoAplicativo"},
	 {Id: "CadastroTransacaoAutorizacao_lUpTcsTransacaoMenuAutorizacao_DescModuloMenu", Name: "CadastroTransacaoAutorizacao_dGridTcsTransacaoMenuAutorizacao_DescModuloMenu", DisplayName: "Menu", ColumnSpan: 9, Visible: true, LookUpName: "LookUpTcsModuloMenuAutorizacao", Key: "DescModuloMenu"},
	 {Id: "CadastroTransacaoAutorizacao_ckTcsTransacaoMenuAutorizacao_Inativo", Name: "CadastroTransacaoAutorizacao_dGridTcsTransacaoMenuAutorizacao_Inativo", DisplayName: "Inativo", ColumnSpan: 3, Visible: true, Key: "Inativo"},
	 {Id: "CadastroTransacaoAutorizacao_ntxTcsTransacaoMenuAutorizacao_OrdemNavegacao", Name: "CadastroTransacaoAutorizacao_dGridTcsTransacaoMenuAutorizacao_OrdemNavegacao", DisplayName: "Ordem", ColumnSpan: 1, Visible: true, Key: "OrdemNavegacao"},
	 {Id: "CadastroTransacaoAutorizacao_tbTcsTransacaoMenuAutorizacao_CodTransacao", Name: "CadastroTransacaoAutorizacao_dGridTcsTransacaoMenuAutorizacao_CodTransacao", DisplayName: "Código", ColumnSpan: 3, Visible: true, Key: "CodTransacao"},]},]},
	 {Name: "CadastroTransacaoAutorizacao_tiTcsTransacaoDependenteAutorizacaoTabItem", DisplayName: "Transações Dependentes", ColumnSpan: 12, Visible: false, Items: [
	 {Name: "CadastroTransacaoAutorizacao_gbGroupBox_e96cc98e8b8b4be5a56aac00176e580c", DisplayName: "Controles de Exibição", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroTransacaoAutorizacao_ckTcsTransacaoDependenteAutorizacao_PossuiToolbar", DisplayName: "Barra de Ferramentas", ColumnSpan: 6, Visible: true, Key: "PossuiToolbar"},
	 {Name: "CadastroTransacaoAutorizacao_ckTcsTransacaoDependenteAutorizacao_PossuiVisaoTabular", DisplayName: "Seletor de Visões", ColumnSpan: 6, Visible: true, Key: "PossuiVisaoTabular"},]},
	 {Name: "CadastroTransacaoAutorizacao_gbGroupBox_ffb11f8b31a44161b9750bc760b26f31", DisplayName: "Funções da Barra de Ferramentas", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroTransacaoAutorizacao_ckTcsTransacaoDependenteAutorizacao_MostraBotaoAdicao", DisplayName: "Adição", ColumnSpan: 3, Visible: true, Key: "MostraBotaoAdicao"},
	 {Name: "CadastroTransacaoAutorizacao_ckTcsTransacaoDependenteAutorizacao_MostraBotaoEdicao", DisplayName: "Edição", ColumnSpan: 3, Visible: true, Key: "MostraBotaoEdicao"},
	 {Name: "CadastroTransacaoAutorizacao_ckTcsTransacaoDependenteAutorizacao_MostraBotaoExclusao", DisplayName: "Exclusão", ColumnSpan: 3, Visible: true, Key: "MostraBotaoExclusao"},
	 {Name: "CadastroTransacaoAutorizacao_ckTcsTransacaoDependenteAutorizacao_MostraBotaoImpressao", DisplayName: "Impressão", ColumnSpan: 5, Visible: true, Key: "MostraBotaoImpressao"},
	 {Name: "CadastroTransacaoAutorizacao_ckTcsTransacaoDependenteAutorizacao_MostraBotaoLayout", DisplayName: "Layout", ColumnSpan: 3, Visible: true, Key: "MostraBotaoLayout"},
	 {Name: "CadastroTransacaoAutorizacao_ckTcsTransacaoDependenteAutorizacao_MostraBotaoLimpa", DisplayName: "Limpa", ColumnSpan: 3, Visible: true, Key: "MostraBotaoLimpa"},
	 {Name: "CadastroTransacaoAutorizacao_ckTcsTransacaoDependenteAutorizacao_MostraBotaoNavegacao", DisplayName: "Navegação", ColumnSpan: 5, Visible: true, Key: "MostraBotaoNavegacao"},
	 {Name: "CadastroTransacaoAutorizacao_ckTcsTransacaoDependenteAutorizacao_MostraBotaoPesquisa", DisplayName: "Pesquisa", ColumnSpan: 3, Visible: true, Key: "MostraBotaoPesquisa"},
	 {Name: "CadastroTransacaoAutorizacao_ckTcsTransacaoDependenteAutorizacao_MostraBotaoPesquisaEsp", DisplayName: "Pesquisa Especial", ColumnSpan: 6, Visible: true, Key: "MostraBotaoPesquisaEsp"},]},
	 {Name: "CadastroTransacaoAutorizacao_cntCustomContainer_498cb77f8407462ca6e6acb29e7b8790", DisplayName: "New Group", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroTransacaoAutorizacao_dGridTcsTransacaoDependenteAutorizacao", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroTransacaoAutorizacao_lUpTcsTransacaoDependenteAutorizacao_DescTransacao", Name: "CadastroTransacaoAutorizacao_dGridTcsTransacaoDependenteAutorizacao_DescTransacao", DisplayName: "Descrição", ColumnSpan: 9, Visible: true, LookUpName: "LookUpTcsTransacaoDependente", Key: "DescTransacao"},
	 {Id: "CadastroTransacaoAutorizacao_lUpTcsTransacaoDependenteAutorizacao_ClasseNome", Name: "CadastroTransacaoAutorizacao_dGridTcsTransacaoDependenteAutorizacao_ClasseNome", DisplayName: "Formulário", ColumnSpan: 9, Visible: true, LookUpName: "LookUpTcsTransacaoDependente", Key: "ClasseNome"},
	 {Id: "CadastroTransacaoAutorizacao_cmbTcsTransacaoDependenteAutorizacao_LxPosicaoDaTransacao", Name: "CadastroTransacaoAutorizacao_dGridTcsTransacaoDependenteAutorizacao_LxPosicaoDaTransacao", DisplayName: "Posição", ColumnSpan: 6, Visible: true, Key: "LxPosicaoDaTransacao"},
	 {Id: "CadastroTransacaoAutorizacao_cmbTcsTransacaoDependenteAutorizacao_LxTipoLayout", Name: "CadastroTransacaoAutorizacao_dGridTcsTransacaoDependenteAutorizacao_LxTipoLayout", DisplayName: "Tipo do Layout", ColumnSpan: 6, Visible: true, Key: "LxTipoLayout"},
	 {Id: "CadastroTransacaoAutorizacao_ckTcsTransacaoDependenteAutorizacao_Visivel", Name: "CadastroTransacaoAutorizacao_dGridTcsTransacaoDependenteAutorizacao_Visivel", DisplayName: "Visível", ColumnSpan: 3, Visible: true, Key: "Visivel"},
	 {Id: "CadastroTransacaoAutorizacao_ckTcsTransacaoDependenteAutorizacao_CompartilhaBoPrincipal", Name: "CadastroTransacaoAutorizacao_dGridTcsTransacaoDependenteAutorizacao_CompartilhaBoPrincipal", DisplayName: "Compartilha BO Principal", ColumnSpan: 8, Visible: true, Key: "CompartilhaBoPrincipal"},
	 {Id: "CadastroTransacaoAutorizacao_ckTcsTransacaoDependenteAutorizacao_ExecutaPesquisa", Name: "CadastroTransacaoAutorizacao_dGridTcsTransacaoDependenteAutorizacao_ExecutaPesquisa", DisplayName: "Sempre Executa Pesquisa", ColumnSpan: 8, Visible: true, Key: "ExecutaPesquisa"},
	 {Id: "CadastroTransacaoAutorizacao_ckTcsTransacaoDependenteAutorizacao_UsaFiltrosDoBoPrincipal", Name: "CadastroTransacaoAutorizacao_dGridTcsTransacaoDependenteAutorizacao_UsaFiltrosDoBoPrincipal", DisplayName: "Usa Filtros do BO Principal", ColumnSpan: 9, Visible: true, Key: "UsaFiltrosDoBoPrincipal"},
	 {Id: "CadastroTransacaoAutorizacao_tbTcsTransacaoDependenteAutorizacao_PropriedadesDoMestre", Name: "CadastroTransacaoAutorizacao_dGridTcsTransacaoDependenteAutorizacao_PropriedadesDoMestre", DisplayName: "Propriedades do Mestre", ColumnSpan: 9, Visible: true, Key: "PropriedadesDoMestre"},
	 {Id: "CadastroTransacaoAutorizacao_tbTcsTransacaoDependenteAutorizacao_PropriedadesDoDetalhe", Name: "CadastroTransacaoAutorizacao_dGridTcsTransacaoDependenteAutorizacao_PropriedadesDoDetalhe", DisplayName: "Propriedades do Detalhe", ColumnSpan: 9, Visible: true, Key: "PropriedadesDoDetalhe"},]},]},]},]},]},]},               ]
           }
           langResult.Items.concat(custom.getCustomTranslation().Items);
           return langResult;
        };
       return result;
  }
)

