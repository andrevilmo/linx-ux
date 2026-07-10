/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/
define(['./linx-spa-cadastrotransacao_custom_pt-br.js'],
	function (custom){
		var result = {};

       result.languageFile = function () {
       	return 'pt-br';
       }

result.objectLanguage_CadastroTransacao = function () {
           var langResult = {
               Name: 'CadastroTransacao', Items: [

	 {Name: "CadastroTransacao_gbTcsTransacao", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroTransacao_gbGroupBox_3cbeac23d550457dbd5b063aa0455a10", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroTransacao_gbGroupBox_c496c3e1c5ac481a81f2eeb7f94c3287", DisplayName: "Transação", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroTransacao_cntCustomContainer_e3b628a0df9649129445f515066be89e", DisplayName: "", ColumnSpan: 6, Visible: true, Items: [
	 {Name: "CadastroTransacao_tbCodTransacao", DisplayName: "Código", ColumnSpan: 2, Visible: true, Key: "CodTransacao"},
	 {Name: "CadastroTransacao_lblCustomControl1223134453", DisplayName: "", ColumnSpan: 10, Visible: true, Key: ""},
	 {Name: "CadastroTransacao_tbNomeCurto", DisplayName: "Descrição", ColumnSpan: 6, Visible: true, Key: "NomeCurto"},
	 {Name: "CadastroTransacao_tbDescTransacao", DisplayName: "Descrição Detalhada", ColumnSpan: 12, Visible: true, Key: "DescTransacao"},
	 {Name: "CadastroTransacao_tbClasseNome", DisplayName: "Formulário / Url", ColumnSpan: 12, Visible: true, Key: "ClasseNome"},]},
	 {Name: "CadastroTransacao_cntCustomContainer_53ce9647ebdd4410875d5d3a80ea06e4", DisplayName: "", ColumnSpan: 6, Visible: true, Items: [
	 {Name: "CadastroTransacao_ckInativo", DisplayName: "Inativo", ColumnSpan: 12, Visible: true, Key: "Inativo"},
	 {Name: "CadastroTransacao_lUpDescObjeto", DisplayName: "Classe BO", ColumnSpan: 12, Visible: true, LookUpName: "LookUpTcsObjetoTransacao", Key: "DescObjeto"},
	 {Name: "CadastroTransacao_cmbLxTipoTransacao", DisplayName: "Tipo Transação", ColumnSpan: 12, Visible: true, Key: "LxTipoTransacao"},]},]},
	 {Name: "CadastroTransacao_gbGroupBox_5d42782ba34a492c9cb9394afce249a9", DisplayName: "Apresentação", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroTransacao_gbGroupBox_e06161cd891944f2bf4b00112f84d8d3", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroTransacao_tbIcone", DisplayName: "Ícone", ColumnSpan: 6, Visible: true, Key: "Icone"},]},]},
	 {Name: "CadastroTransacao_gbGroupBox_7ed24c23064546c8a999a32dcbee949e", DisplayName: "Tags", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroTransacao_CustomControl455346109", DisplayName: "", ColumnSpan: 8, Visible: true, Key: ""},]},]},
	 {Name: "CadastroTransacao_gbTransacaoMenu", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroTransacao_tcTcsTransacaoTabControl", DisplayName: "Transações", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroTransacao_tiTcsTransacaoMenuChildTabItem", DisplayName: "Menu", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroTransacao_dGridTcsTransacaoMenuChild", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroTransacao_lUpTcsTransacaoMenuChild_DescModulo", Name: "CadastroTransacao_dGridTcsTransacaoMenuChild_DescModulo", DisplayName: "Módulo", ColumnSpan: 8, Visible: true, LookUpName: "LookUpTcsTransacaoMenuChildTcsModuloMenu", Key: "DescModulo"},
	 {Id: "CadastroTransacao_tbTcsTransacaoMenuChild_DescAplicativo", Name: "CadastroTransacao_dGridTcsTransacaoMenuChild_DescAplicativo", DisplayName: "Aplicativo", ColumnSpan: 8, Visible: true, Key: "DescAplicativo"},
	 {Id: "CadastroTransacao_lUpTcsTransacaoMenuChild_DescModuloMenu", Name: "CadastroTransacao_dGridTcsTransacaoMenuChild_DescModuloMenu", DisplayName: "Menu", ColumnSpan: 9, Visible: true, LookUpName: "LookUpTcsTransacaoMenuChildTcsModuloMenu", Key: "DescModuloMenu"},
	 {Id: "CadastroTransacao_tbTcsTransacaoMenuChild_OrdemNavegacao", Name: "CadastroTransacao_dGridTcsTransacaoMenuChild_OrdemNavegacao", DisplayName: "Ordem", ColumnSpan: 1, Visible: true, Key: "OrdemNavegacao"},
	 {Id: "CadastroTransacao_ckTcsTransacaoMenuChild_Inativo", Name: "CadastroTransacao_dGridTcsTransacaoMenuChild_Inativo", DisplayName: "Inativo", ColumnSpan: 3, Visible: true, Key: "Inativo"},]},]},
	 {Name: "CadastroTransacao_tiTcsTransacaoDependenteTabItem", DisplayName: "Transações Dependentes", ColumnSpan: 12, Visible: false, Items: [
	 {Name: "CadastroTransacao_gbGroupBox_8eb748db382040858b5e16a2e14859ca", DisplayName: "Controles de Exibição", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroTransacao_ckTcsTransacaoDependente_PossuiToolbar", DisplayName: "Barra de Ferramentas", ColumnSpan: 6, Visible: true, Key: "PossuiToolbar"},
	 {Name: "CadastroTransacao_ckTcsTransacaoDependente_PossuiVisaoTabular", DisplayName: "Seletor de Visões", ColumnSpan: 6, Visible: true, Key: "PossuiVisaoTabular"},]},
	 {Name: "CadastroTransacao_gbGroupBox_1dcc817cb1874474886dfd09446420b4", DisplayName: "Funções da Barra de Ferramentas", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroTransacao_ckTcsTransacaoDependente_MostraBotaoAdicao", DisplayName: "Adição", ColumnSpan: 3, Visible: true, Key: "MostraBotaoAdicao"},
	 {Name: "CadastroTransacao_ckTcsTransacaoDependente_MostraBotaoEdicao", DisplayName: "Edição", ColumnSpan: 3, Visible: true, Key: "MostraBotaoEdicao"},
	 {Name: "CadastroTransacao_ckTcsTransacaoDependente_MostraBotaoExclusao", DisplayName: "Exclusão", ColumnSpan: 3, Visible: true, Key: "MostraBotaoExclusao"},
	 {Name: "CadastroTransacao_ckTcsTransacaoDependente_MostraBotaoImpressao", DisplayName: "Impressão", ColumnSpan: 5, Visible: true, Key: "MostraBotaoImpressao"},
	 {Name: "CadastroTransacao_ckTcsTransacaoDependente_MostraBotaoLayout", DisplayName: "Layout", ColumnSpan: 3, Visible: true, Key: "MostraBotaoLayout"},
	 {Name: "CadastroTransacao_ckTcsTransacaoDependente_MostraBotaoLimpa", DisplayName: "Limpa", ColumnSpan: 3, Visible: true, Key: "MostraBotaoLimpa"},
	 {Name: "CadastroTransacao_ckTcsTransacaoDependente_MostraBotaoNavegacao", DisplayName: "Navegação", ColumnSpan: 5, Visible: true, Key: "MostraBotaoNavegacao"},
	 {Name: "CadastroTransacao_ckTcsTransacaoDependente_MostraBotaoPesquisa", DisplayName: "Pesquisa", ColumnSpan: 3, Visible: true, Key: "MostraBotaoPesquisa"},
	 {Name: "CadastroTransacao_ckTcsTransacaoDependente_MostraBotaoPesquisaEsp", DisplayName: "Pesquisa Especial", ColumnSpan: 6, Visible: true, Key: "MostraBotaoPesquisaEsp"},]},
	 {Name: "CadastroTransacao_cntCustomContainer_eab2f1ce66c244fd8e85cd1577b14471", DisplayName: "New Group", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroTransacao_dGridTcsTransacaoDependente", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroTransacao_lUpTcsTransacaoDependente_DescTransacao", Name: "CadastroTransacao_dGridTcsTransacaoDependente_DescTransacao", DisplayName: "Transação", ColumnSpan: 9, Visible: true, LookUpName: "LookUpTcsTransacaoDependente", Key: "DescTransacao"},
	 {Id: "CadastroTransacao_lUpTcsTransacaoDependente_ClasseNome", Name: "CadastroTransacao_dGridTcsTransacaoDependente_ClasseNome", DisplayName: "Formulário", ColumnSpan: 9, Visible: true, LookUpName: "LookUpTcsTransacaoDependente", Key: "ClasseNome"},
	 {Id: "CadastroTransacao_cmbTcsTransacaoDependente_LxPosicaoDaTransacao", Name: "CadastroTransacao_dGridTcsTransacaoDependente_LxPosicaoDaTransacao", DisplayName: "Posição", ColumnSpan: 6, Visible: true, Key: "LxPosicaoDaTransacao"},
	 {Id: "CadastroTransacao_cmbTcsTransacaoDependente_LxTipoLayout", Name: "CadastroTransacao_dGridTcsTransacaoDependente_LxTipoLayout", DisplayName: "Tipo do Layout", ColumnSpan: 6, Visible: true, Key: "LxTipoLayout"},
	 {Id: "CadastroTransacao_ckTcsTransacaoDependente_Visivel", Name: "CadastroTransacao_dGridTcsTransacaoDependente_Visivel", DisplayName: "Visível", ColumnSpan: 3, Visible: true, Key: "Visivel"},
	 {Id: "CadastroTransacao_ckTcsTransacaoDependente_CompartilhaBoPrincipal", Name: "CadastroTransacao_dGridTcsTransacaoDependente_CompartilhaBoPrincipal", DisplayName: "Compartilha BO Principal", ColumnSpan: 8, Visible: true, Key: "CompartilhaBoPrincipal"},
	 {Id: "CadastroTransacao_ckTcsTransacaoDependente_UsaFiltrosDoBoPrincipal", Name: "CadastroTransacao_dGridTcsTransacaoDependente_UsaFiltrosDoBoPrincipal", DisplayName: "Usa Filtros do BO Principal", ColumnSpan: 9, Visible: true, Key: "UsaFiltrosDoBoPrincipal"},
	 {Id: "CadastroTransacao_ckTcsTransacaoDependente_ExecutaPesquisa", Name: "CadastroTransacao_dGridTcsTransacaoDependente_ExecutaPesquisa", DisplayName: "Sempre Executa Pesquisa", ColumnSpan: 8, Visible: true, Key: "ExecutaPesquisa"},
	 {Id: "CadastroTransacao_tbTcsTransacaoDependente_PropriedadesDoMestre", Name: "CadastroTransacao_dGridTcsTransacaoDependente_PropriedadesDoMestre", DisplayName: "Propriedades do Mestre", ColumnSpan: 9, Visible: true, Key: "PropriedadesDoMestre"},
	 {Id: "CadastroTransacao_tbTcsTransacaoDependente_PropriedadesDoDetalhe", Name: "CadastroTransacao_dGridTcsTransacaoDependente_PropriedadesDoDetalhe", DisplayName: "Propriedades do Detalhe", ColumnSpan: 9, Visible: true, Key: "PropriedadesDoDetalhe"},]},]},]},]},]},]},               ]
           }
           langResult.Items.concat(custom.getCustomTranslation().Items);
           return langResult;
        };
       return result;
  }
)

