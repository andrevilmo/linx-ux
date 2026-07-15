/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/
define(['./linx-spa-cadastroperfil_custom_pt-br.js'],
	function (custom){
		var result = {};

       result.languageFile = function () {
       	return 'pt-br';
       }

result.objectLanguage_CadastroPerfil = function () {
           var langResult = {
               Name: 'CadastroPerfil', Items: [

	 {Name: "CadastroPerfil_gbTcsPerfilb162e82f59c5469d81881a40a7b2b9f6", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroPerfil_gbGroupBox_4fa922da0e014c4082a3acedb7f1640c", DisplayName: "Informações do Perfil", ColumnSpan: 6, Visible: true, Items: [
	 {Name: "CadastroPerfil_tbIdPerfil", DisplayName: "Id", ColumnSpan: 2, Visible: true, Key: "IdPerfil"},
	 {Name: "CadastroPerfil_tbDescPerfil", DisplayName: "Descrição", ColumnSpan: 8, Visible: true, Key: "DescPerfil"},
	 {Name: "CadastroPerfil_ckInativo", DisplayName: "Inativo", ColumnSpan: 2, Visible: true, Key: "Inativo"},]},
	 {Name: "CadastroPerfil_gbGroupBox_5b51d31fa84b461cb8171550e7a23300", DisplayName: "Grupo Econômico", ColumnSpan: 6, Visible: true, Items: [
	 {Name: "CadastroPerfil_lUpIdGpeconP", DisplayName: "Id", ColumnSpan: 2, Visible: true, LookUpName: "LookUpTbcGrupoEconomico", Key: "IdGpeconP"},
	 {Name: "CadastroPerfil_lUpDescGrupoEconomico", DisplayName: "Descrição", ColumnSpan: 10, Visible: true, LookUpName: "LookUpTbcGrupoEconomico", Key: "DescGrupoEconomico"},]},
	 {Name: "CadastroPerfil_tcTcsPerfilTabControl", DisplayName: "TcsPerfil", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroPerfil_tiTcsUsuarioPerfilTabItem", DisplayName: "Usuário", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroPerfil_dGridTcsUsuarioPerfil", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroPerfil_lUpTcsUsuarioPerfil_NomeUsuario", Name: "CadastroPerfil_dGridTcsUsuarioPerfil_NomeUsuario", DisplayName: "Usuário", ColumnSpan: 9, Visible: true, LookUpName: "LookUpTcsUsuario", Key: "NomeUsuario"},]},]},
	 {Name: "CadastroPerfil_tiTcsPerfilRegraModuloTabItem", DisplayName: "Módulo", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroPerfil_dGridTcsPerfilRegraModulo", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroPerfil_lUpTcsPerfilRegraModulo_DescModulo", Name: "CadastroPerfil_dGridTcsPerfilRegraModulo_DescModulo", DisplayName: "Módulo", ColumnSpan: 8, Visible: true, LookUpName: "LookUpTcsPerfilRegraModulo", Key: "DescModulo"},
	 {Id: "CadastroPerfil_tbTcsPerfilRegraModulo_DescAplicativo", Name: "CadastroPerfil_dGridTcsPerfilRegraModulo_DescAplicativo", DisplayName: "Aplicativo", ColumnSpan: 8, Visible: true, Key: "DescAplicativo"},
	 {Id: "CadastroPerfil_cmbTcsPerfilRegraModulo_LxRegraAcessoModulo", Name: "CadastroPerfil_dGridTcsPerfilRegraModulo_LxRegraAcessoModulo", DisplayName: "Regra Módulo", ColumnSpan: 6, Visible: true, Key: "LxRegraAcessoModulo"},]},]},
	 {Name: "CadastroPerfil_tiTcsPerfilRegraTransacaoTabItem", DisplayName: "Transação", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroPerfil_dGridTcsPerfilRegraTransacao", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroPerfil_lUpTcsPerfilRegraTransacao_DescTransacao", Name: "CadastroPerfil_dGridTcsPerfilRegraTransacao_DescTransacao", DisplayName: "Transação", ColumnSpan: 9, Visible: true, LookUpName: "LookUpTcsPerfilRegraTransacao", Key: "DescTransacao"},
	 {Id: "CadastroPerfil_cmbTcsPerfilRegraTransacao_LxRegraAcessoTransacao", Name: "CadastroPerfil_dGridTcsPerfilRegraTransacao_LxRegraAcessoTransacao", DisplayName: "Regra Acesso Transação", ColumnSpan: 6, Visible: true, Key: "LxRegraAcessoTransacao"},]},]},
	 {Name: "CadastroPerfil_tiTcsPerfilRegraColunaTabItem", DisplayName: "Coluna", ColumnSpan: 12, Visible: false, Items: [
	 {Name: "CadastroPerfil_dGridTcsPerfilRegraColuna", DisplayName: "DataGrid", ColumnSpan: 12, Visible: false, Items: [
	 {Id: "CadastroPerfil_lUpTcsPerfilRegraColuna_DescTransacao", Name: "CadastroPerfil_dGridTcsPerfilRegraColuna_DescTransacao", DisplayName: "Transação", ColumnSpan: 8, Visible: false, LookUpName: "LookUpTcsPerfilRegraColuna", Key: "DescTransacao"},
	 {Id: "CadastroPerfil_tbTcsPerfilRegraColuna_TransacaoColuna", Name: "CadastroPerfil_dGridTcsPerfilRegraColuna_TransacaoColuna", DisplayName: "Transação Coluna", ColumnSpan: 9, Visible: false, Key: "TransacaoColuna"},
	 {Id: "CadastroPerfil_cmbTcsPerfilRegraColuna_LxRegraAcessoColuna", Name: "CadastroPerfil_dGridTcsPerfilRegraColuna_LxRegraAcessoColuna", DisplayName: "Regra Coluna", ColumnSpan: 6, Visible: false, Key: "LxRegraAcessoColuna"},]},]},
	 {Name: "CadastroPerfil_tiTcsPerfilBandeiraRedeTabItem", DisplayName: "Bandeira / Rede", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroPerfil_dGridTcsPerfilBandeiraRede", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroPerfil_lUpTcsPerfilBandeiraRede_DescBandeiraRede", Name: "CadastroPerfil_dGridTcsPerfilBandeiraRede_DescBandeiraRede", DisplayName: "Bandeira / Rede", ColumnSpan: 9, Visible: true, LookUpName: "LookUpTbcBandeiraRede", Key: "DescBandeiraRede"},]},]},
	 {Name: "CadastroPerfil_tiTcsPerfilLayoutTabItem", DisplayName: "Layouts", ColumnSpan: 12, Visible: false, Items: [
	 {Name: "CadastroPerfil_dGridTcsPerfilLayout", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroPerfil_lUpTcsPerfilLayout_DescLayout", Name: "CadastroPerfil_dGridTcsPerfilLayout_DescLayout", DisplayName: "Layout", ColumnSpan: 9, Visible: true, LookUpName: "LookUpTcsLayout", Key: "DescLayout"},
	 {Id: "CadastroPerfil_edTcsPerfilLayout_Detalhes", Name: "CadastroPerfil_dGridTcsPerfilLayout_Detalhes", DisplayName: "Detalhes", ColumnSpan: 9, Visible: true, Key: "Detalhes"},
	 {Id: "CadastroPerfil_ckTcsPerfilLayout_Inativo", Name: "CadastroPerfil_dGridTcsPerfilLayout_Inativo", DisplayName: "Inativo", ColumnSpan: 3, Visible: true, Key: "Inativo"},]},]},
	 {Name: "CadastroPerfil_tiTcsPerfilFilialTabItem", DisplayName: "Filial", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroPerfil_dGridTcsPerfilFilial", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroPerfil_lUpTcsPerfilFilial_CodigoFilial", Name: "CadastroPerfil_dGridTcsPerfilFilial_CodigoFilial", DisplayName: "Código Filial", ColumnSpan: 6, Visible: true, LookUpName: "LookUpTbcFilial", Key: "CodigoFilial"},
	 {Id: "CadastroPerfil_lUpTcsPerfilFilial_NomeFilial", Name: "CadastroPerfil_dGridTcsPerfilFilial_NomeFilial", DisplayName: "Nome Fantasia", ColumnSpan: 9, Visible: true, LookUpName: "LookUpTbcFilial", Key: "NomeFilial"},]},]},]},]},               ]
           }
           langResult.Items.concat(custom.getCustomTranslation().Items);
           return langResult;
        };
       return result;
  }
)

