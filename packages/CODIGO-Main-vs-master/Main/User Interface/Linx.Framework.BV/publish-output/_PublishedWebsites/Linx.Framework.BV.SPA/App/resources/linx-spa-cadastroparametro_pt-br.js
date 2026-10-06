/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/
define(['./linx-spa-cadastroparametro_custom_pt-br.js'],
	function (custom){
		var result = {};

       result.languageFile = function () {
       	return 'pt-br';
       }

result.objectLanguage_CadastroParametro = function () {
           var langResult = {
               Name: 'CadastroParametro', Items: [

	 {Name: "CadastroParametro_gbTcsParametro05149c7cde9245c69b9e2745678608cb", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroParametro_gbGroupBox_cc1d81f02c754f13b35e1beac7752c33", DisplayName: "Informações do Parâmetro", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroParametro_tbTituloParametro", DisplayName: "Título", ColumnSpan: 12, Visible: true, Key: "TituloParametro"},
	 {Name: "CadastroParametro_tbDescParametro", DisplayName: "Descrição", ColumnSpan: 12, Visible: true, Key: "DescParametro"},
	 {Name: "CadastroParametro_lUpDescGrupoParametro", DisplayName: "Grupo", ColumnSpan: 12, Visible: true, LookUpName: "LookUpTcsParametroGrupoAutorizacao", Key: "DescGrupoParametro"},
	 {Name: "CadastroParametro_lUpIdTcsAplicativo", DisplayName: "Aplicativo", ColumnSpan: 2, Visible: true, LookUpName: "LookUpTcsAplicativo", Key: "IdTcsAplicativo"},
	 {Name: "CadastroParametro_lUpDescricaoAplicativo", DisplayName: "Descrição", ColumnSpan: 10, Visible: true, LookUpName: "LookUpTcsAplicativo", Key: "DescricaoAplicativo"},
	 {Name: "CadastroParametro_cmbLxDatatypeParametro", DisplayName: "Tipo do Dado", ColumnSpan: 12, Visible: true, Key: "LxDatatypeParametro"},
	 {Name: "CadastroParametro_cmbLxTipoValidacaoParametro", DisplayName: "Tipo Validação", ColumnSpan: 12, Visible: true, Key: "LxTipoValidacaoParametro"},
	 {Name: "CadastroParametro_ntxNivelAcesso", DisplayName: "Nível Acesso Visualização", ColumnSpan: 12, Visible: true, Key: "NivelAcesso"},
	 {Name: "CadastroParametro_ntxNivelAcessoEdicao", DisplayName: "Nível Acesso Edição", ColumnSpan: 12, Visible: true, Key: "NivelAcessoEdicao"},
	 {Name: "CadastroParametro_ckPermiteVariacaoPorEntidade", DisplayName: "Permite Variação por Entidade", ColumnSpan: 12, Visible: true, Key: "PermiteVariacaoPorEntidade"},
	 {Name: "CadastroParametro_ckIndicaEnviaPdv", DisplayName: "Envia PDV", ColumnSpan: 12, Visible: true, Key: "IndicaEnviaPdv"},
	 {Name: "CadastroParametro_edObsParametro", DisplayName: "Obs", ColumnSpan: 12, Visible: true, Key: "ObsParametro"},]},
	 {Name: "CadastroParametro_gbvalorPadrao", DisplayName: "Valor Padrão", ColumnSpan: 6, Visible: true, Items: [
	 {Name: "CadastroParametro_cntValorParametro", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroParametro_tbValorParametro", DisplayName: "", ColumnSpan: 12, Visible: true, Key: "ValorParametro"},]},
	 {Name: "CadastroParametro_cntValorParametroData", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroParametro_dtValorParametroData", DisplayName: "", ColumnSpan: 12, Visible: true, Key: "ValorParametroData"},]},
	 {Name: "CadastroParametro_cntValorParametroBool", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroParametro_ckValorParametroBool", DisplayName: "Verdadeiro", ColumnSpan: 12, Visible: true, Key: "ValorParametroBool"},]},
	 {Name: "CadastroParametro_cntValorParametroMascara", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroParametro_tbValorParametroMascara", DisplayName: "", ColumnSpan: 12, Visible: true, Key: "ValorParametro"},]},]},
	 {Name: "CadastroParametro_gbvariacaoParametro", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroParametro_tcTcsParametroTabControl", DisplayName: "Parâmetros", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroParametro_tiTcsParametroTabelaSelecaoTabItem", DisplayName: "Variação", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroParametro_cntCustomContainer_137d41fb66424aaeb2135bed5bda1264", DisplayName: "New Group", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroParametro_dGridTcsParametroTabelaSelecao", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroParametro_lUpTcsParametroTabelaSelecao_NomeTabela", Name: "CadastroParametro_dGridTcsParametroTabelaSelecao_NomeTabela", DisplayName: "Nome Tabela", ColumnSpan: 9, Visible: true, LookUpName: "LookUpTcsTabelaAutorizacaoSelecao", Key: "NomeTabela"},
	 {Id: "CadastroParametro_lUpTcsParametroTabelaSelecao_DescTabela", Name: "CadastroParametro_dGridTcsParametroTabelaSelecao_DescTabela", DisplayName: "Descrição", ColumnSpan: 9, Visible: true, LookUpName: "LookUpTcsTabelaAutorizacaoSelecao", Key: "DescTabela"},
	 {Id: "CadastroParametro_cmbTcsParametroTabelaSelecao_LxParametroHierarquia", Name: "CadastroParametro_dGridTcsParametroTabelaSelecao_LxParametroHierarquia", DisplayName: "Hierarquia", ColumnSpan: 6, Visible: true, Key: "LxParametroHierarquia"},]},]},]},
	 {Name: "CadastroParametro_tiTcsParametroValorUsuarioTabItem", DisplayName: "Usuário", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroParametro_dGridValorUsuario", DisplayName: "Grid Usuário", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroParametro_lUpNomeUsuario", Name: "CadastroParametro_dGridValorUsuario_NomeUsuario", DisplayName: "Nome Usuário", ColumnSpan: 9, Visible: true, LookUpName: "LookTcsParametroUsuario", Key: "NomeUsuario"},
	 {Id: "CadastroParametro_tbTcsParametroValorUsuario_ValorParametro", Name: "CadastroParametro_dGridValorUsuario_ValorParametro", DisplayName: "Valor", ColumnSpan: 9, Visible: true, Key: "ValorParametro"},
	 {Id: "CadastroParametro_ckTcsParametroValorUsuario_ValorParametroBool", Name: "CadastroParametro_dGridValorUsuario_ValorParametroBool", DisplayName: "Valor", ColumnSpan: 9, Visible: true, Key: "ValorParametroBool"},]},]},
	 {Name: "CadastroParametro_tiTcsParametroValorRedeTabItem", DisplayName: "Rede", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroParametro_dGridTcsParametroValorRede", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroParametro_lUpTcsParametroValorRede_CodBandeiraRede", Name: "CadastroParametro_dGridTcsParametroValorRede_CodBandeiraRede", DisplayName: "Código Bandeira / Rede", ColumnSpan: 8, Visible: true, LookUpName: "LookUpParametroRede", Key: "CodBandeiraRede"},
	 {Id: "CadastroParametro_lUpTcsParametroValorRede_DescBandeiraRede", Name: "CadastroParametro_dGridTcsParametroValorRede_DescBandeiraRede", DisplayName: "Bandeira / Rede", ColumnSpan: 9, Visible: true, LookUpName: "LookUpParametroRede", Key: "DescBandeiraRede"},
	 {Id: "CadastroParametro_tbTcsParametroValorRede_ValorParametro", Name: "CadastroParametro_dGridTcsParametroValorRede_ValorParametro", DisplayName: "Valor", ColumnSpan: 9, Visible: true, Key: "ValorParametro"},
	 {Id: "CadastroParametro_ckTcsParametroValorRede_ValorParametroBool", Name: "CadastroParametro_dGridTcsParametroValorRede_ValorParametroBool", DisplayName: " Valor", ColumnSpan: 9, Visible: true, Key: "ValorParametroBool"},]},]},
	 {Name: "CadastroParametro_tiTcsParametroValorGpeconTabItem", DisplayName: "Grupo Econômico", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroParametro_dGridTcsParametroValorGpecon", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroParametro_lUpTcsParametroValorGpecon_IdGpecon", Name: "CadastroParametro_dGridTcsParametroValorGpecon_IdGpecon", DisplayName: "Código Grupo Econômico", ColumnSpan: 5, Visible: true, LookUpName: "LookUpParametroGpecon", Key: "IdGpecon"},
	 {Id: "CadastroParametro_lUpTcsParametroValorGpecon_DescGrupoEconomico", Name: "CadastroParametro_dGridTcsParametroValorGpecon_DescGrupoEconomico", DisplayName: "Grupo Econômico", ColumnSpan: 9, Visible: true, LookUpName: "LookUpParametroGpecon", Key: "DescGrupoEconomico"},
	 {Id: "CadastroParametro_tbTcsParametroValorGpecon_ValorParametro", Name: "CadastroParametro_dGridTcsParametroValorGpecon_ValorParametro", DisplayName: "Valor", ColumnSpan: 9, Visible: true, Key: "ValorParametro"},
	 {Id: "CadastroParametro_ckTcsParametroValorGpecon_ValorParametroBool", Name: "CadastroParametro_dGridTcsParametroValorGpecon_ValorParametroBool", DisplayName: " Valor", ColumnSpan: 9, Visible: true, Key: "ValorParametroBool"},]},]},
	 {Name: "CadastroParametro_tiTcsParametroValorFilialTabItem", DisplayName: "Filial", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroParametro_dGridTcsParametroValorFilial", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroParametro_lUpTcsParametroValorFilial_CodigoFilial", Name: "CadastroParametro_dGridTcsParametroValorFilial_CodigoFilial", DisplayName: "Código Filial", ColumnSpan: 6, Visible: true, LookUpName: "LookUpParametroFilial", Key: "CodigoFilial"},
	 {Id: "CadastroParametro_lUpTcsParametroValorFilial_NomeFilial", Name: "CadastroParametro_dGridTcsParametroValorFilial_NomeFilial", DisplayName: "Filial", ColumnSpan: 9, Visible: true, LookUpName: "LookUpParametroFilial", Key: "NomeFilial"},
	 {Id: "CadastroParametro_tbTcsParametroValorFilial_ValorParametro", Name: "CadastroParametro_dGridTcsParametroValorFilial_ValorParametro", DisplayName: "Valor", ColumnSpan: 9, Visible: true, Key: "ValorParametro"},
	 {Id: "CadastroParametro_ckTcsParametroValorFilial_ValorParametroBool", Name: "CadastroParametro_dGridTcsParametroValorFilial_ValorParametroBool", DisplayName: "Valor", ColumnSpan: 9, Visible: true, Key: "ValorParametroBool"},]},]},
	 {Name: "CadastroParametro_tiTcsParametroValorLjvLojaTabItem", DisplayName: "Loja", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroParametro_dGridTcsParametroValorLjvLoja", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroParametro_lUpTcsParametroValorLjvLoja_CodLoja", Name: "CadastroParametro_dGridTcsParametroValorLjvLoja_CodLoja", DisplayName: "Código Loja", ColumnSpan: 6, Visible: true, LookUpName: "LookUpParametroLoja", Key: "CodLoja"},
	 {Id: "CadastroParametro_lUpTcsParametroValorLjvLoja_DescLoja", Name: "CadastroParametro_dGridTcsParametroValorLjvLoja_DescLoja", DisplayName: "Loja", ColumnSpan: 9, Visible: true, LookUpName: "LookUpParametroLoja", Key: "DescLoja"},
	 {Id: "CadastroParametro_tbTcsParametroValorLjvLoja_ValorParametro", Name: "CadastroParametro_dGridTcsParametroValorLjvLoja_ValorParametro", DisplayName: "Valor", ColumnSpan: 9, Visible: true, Key: "ValorParametro"},
	 {Id: "CadastroParametro_ckTcsParametroValorLjvLoja_ValorParametroBool", Name: "CadastroParametro_dGridTcsParametroValorLjvLoja_ValorParametroBool", DisplayName: "Valor", ColumnSpan: 9, Visible: true, Key: "ValorParametroBool"},]},]},
	 {Name: "CadastroParametro_tiTcsParametroValorVariacaoGenericaTabItem", DisplayName: "Demais Variações", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroParametro_dGridTcsParametroValorVariacaoGenerica", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroParametro_lUpTcsParametroValorVariacaoGenerica_NomeTabela", Name: "CadastroParametro_dGridTcsParametroValorVariacaoGenerica_NomeTabela", DisplayName: "Nome Tabela", ColumnSpan: 8, Visible: true, LookUpName: "LookUpTcsTabelaAutorizacaoC", Key: "NomeTabela"},
	 {Id: "CadastroParametro_tbTcsParametroValorVariacaoGenerica_ChaveSelecao", Name: "CadastroParametro_dGridTcsParametroValorVariacaoGenerica_ChaveSelecao", DisplayName: "Chave", ColumnSpan: 9, Visible: true, Key: "ChaveSelecao"},
	 {Id: "CadastroParametro_tbTcsParametroValorVariacaoGenerica_ValorParametro", Name: "CadastroParametro_dGridTcsParametroValorVariacaoGenerica_ValorParametro", DisplayName: "Valor", ColumnSpan: 9, Visible: true, Key: "ValorParametro"},
	 {Id: "CadastroParametro_ckTcsParametroValorVariacaoGenerica_ValorParametroBool", Name: "CadastroParametro_dGridTcsParametroValorVariacaoGenerica_ValorParametroBool", DisplayName: " Valor", ColumnSpan: 9, Visible: true, Key: "ValorParametroBool"},]},]},]},]},]},               ]
           }
           langResult.Items.concat(custom.getCustomTranslation().Items);
           return langResult;
        };
       return result;
  }
)

