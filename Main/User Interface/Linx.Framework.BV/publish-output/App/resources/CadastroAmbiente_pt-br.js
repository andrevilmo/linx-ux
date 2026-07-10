var languageFile = function(){
    return 'pt-br';
}

var objectLanguage = function () {
   return {
      Name: 'CadastroAmbiente', Items: [

	 {Name: 'CadastroAmbiente_gbTcsAmbiente', DisplayName: '', ColumnSpan: 12, Visible: true, Items: [
	 {Name: 'CadastroAmbiente_gbGroupBox_2cae6c2fce9240158ae3bfd908223023', DisplayName: 'Ambiente', ColumnSpan: 12, Visible: true, Items: [
	 {Name: 'CadastroAmbiente_tbIdTcsAmbiente', DisplayName: 'Ambiente', ColumnSpan: 2, Visible: true},
	 {Name: 'CadastroAmbiente_tbDescricaoAmbiente', DisplayName: 'Ambiente', ColumnSpan: 10, Visible: true},
	 {Name: 'CadastroAmbiente_lUpNomeEmpresa', DisplayName: 'Empresa (Id Linx)', ColumnSpan: 12, Visible: true},
	 {Name: 'CadastroAmbiente_ckEmDesenvolvimento', DisplayName: 'Em Desenvolvimento', ColumnSpan: 12, Visible: true},
	 {Name: 'CadastroAmbiente_lUpDescricaoAplicacao', DisplayName: 'Aplicação', ColumnSpan: 12, Visible: true},
	 {Name: 'CadastroAmbiente_lUpDescricaoAplicativo', DisplayName: 'Aplicativo', ColumnSpan: 12, Visible: true},
	 {Name: 'CadastroAmbiente_tbUrlWorkArea', DisplayName: 'Url Work Area', ColumnSpan: 9, Visible: true},]},
	 {Name: 'CadastroAmbiente_tcTcsAmbienteTabControl', DisplayName: 'TcsAmbiente', ColumnSpan: 12, Visible: true, Items: [
	 {Name: 'CadastroAmbiente_tiTcsAmbienteConexaoTabItem', DisplayName: 'Providers', ColumnSpan: 12, Visible: true, Items: [
	 {Name: 'CadastroAmbiente_dGridTcsAmbienteConexao', DisplayName: 'DataGrid', ColumnSpan: 12, Visible: true, Items: [
	 {Id: 'CadastroAmbiente_lUpTcsAmbienteConexao_NomeConexao', Name: 'CadastroAmbiente_dGridTcsAmbienteConexao_NomeConexao', DisplayName: 'Nome Provider BM', ColumnSpan: 9, Visible: true},
	 {Id: 'CadastroAmbiente_lUpTcsAmbienteConexao_DescricaoBancoServidor', Name: 'CadastroAmbiente_dGridTcsAmbienteConexao_DescricaoBancoServidor', DisplayName: 'Conexão Banco/Servidor', ColumnSpan: 9, Visible: true},
	 {Id: 'CadastroAmbiente_cmbTcsAmbienteConexao_LxTipoServidor', Name: 'CadastroAmbiente_dGridTcsAmbienteConexao_LxTipoServidor', DisplayName: 'Tipo Servidor', ColumnSpan: 6, Visible: true},
	 {Id: 'CadastroAmbiente_tbTcsAmbienteConexao_NomeServidor', Name: 'CadastroAmbiente_dGridTcsAmbienteConexao_NomeServidor', DisplayName: 'Servidor', ColumnSpan: 9, Visible: true},
	 {Id: 'CadastroAmbiente_tbTcsAmbienteConexao_NomeBanco', Name: 'CadastroAmbiente_dGridTcsAmbienteConexao_NomeBanco', DisplayName: 'Banco de Dados', ColumnSpan: 9, Visible: true},]},]},
	 {Name: 'CadastroAmbiente_tiTcsAmbienteUsuarioAcessoTabItem', DisplayName: 'Usuários', ColumnSpan: 12, Visible: true, Items: [
	 {Name: 'CadastroAmbiente_dGridTcsAmbienteUsuarioAcesso', DisplayName: 'DataGrid', ColumnSpan: 12, Visible: true, Items: [
	 {Id: 'CadastroAmbiente_lUpTcsAmbienteUsuarioAcesso_NomeUsuario', Name: 'CadastroAmbiente_dGridTcsAmbienteUsuarioAcesso_NomeUsuario', DisplayName: 'Usuário', ColumnSpan: 9, Visible: true},
	 {Id: 'CadastroAmbiente_lUpTcsAmbienteUsuarioAcesso_NomeEmpresa', Name: 'CadastroAmbiente_dGridTcsAmbienteUsuarioAcesso_NomeEmpresa', DisplayName: 'Grupo Econômico', ColumnSpan: 9, Visible: true},
	 {Id: 'CadastroAmbiente_ckTcsAmbienteUsuarioAcesso_IndicaAdministrador', Name: 'CadastroAmbiente_dGridTcsAmbienteUsuarioAcesso_IndicaAdministrador', DisplayName: 'Administrador', ColumnSpan: 5, Visible: true},
	 {Id: 'CadastroAmbiente_ckTcsAmbienteUsuarioAcesso_IndicaMultiGpecon', Name: 'CadastroAmbiente_dGridTcsAmbienteUsuarioAcesso_IndicaMultiGpecon', DisplayName: 'Multi Grupo Econômico', ColumnSpan: 8, Visible: true},
	 {Id: 'CadastroAmbiente_lUpTcsAmbienteUsuarioAcesso_DescricaoAmbienteRelacionado', Name: 'CadastroAmbiente_dGridTcsAmbienteUsuarioAcesso_DescricaoAmbienteRelacionado', DisplayName: 'Ambiente Relacionado', ColumnSpan: 9, Visible: true},
	 {Id: 'CadastroAmbiente_tbTcsAmbienteUsuarioAcesso_NomeEmpresaAmbienteRelacionado', Name: 'CadastroAmbiente_dGridTcsAmbienteUsuarioAcesso_NomeEmpresaAmbienteRelacionado', DisplayName: 'Empresa Ambiente Relacionado', ColumnSpan: 9, Visible: true},
	 {Id: 'CadastroAmbiente_tbTcsAmbienteUsuarioAcesso_DescricaoAplicacaoAmbienteRelacionado', Name: 'CadastroAmbiente_dGridTcsAmbienteUsuarioAcesso_DescricaoAplicacaoAmbienteRelacionado', DisplayName: 'Aplicação Ambiente Relacionado', ColumnSpan: 9, Visible: true},]},]},
	 {Name: 'CadastroAmbiente_tiTcsAmbienteServicoExcecaoTabItem', DisplayName: 'Serviços', ColumnSpan: 12, Visible: true, Items: [
	 {Name: 'CadastroAmbiente_dGridTcsAmbienteServicoExcecao', DisplayName: 'DataGrid', ColumnSpan: 12, Visible: true, Items: [
	 {Id: 'CadastroAmbiente_lUpTcsAmbienteServicoExcecao_NomeServico', Name: 'CadastroAmbiente_dGridTcsAmbienteServicoExcecao_NomeServico', DisplayName: 'Nome Serviço', ColumnSpan: 9, Visible: true},
	 {Id: 'CadastroAmbiente_tbTcsAmbienteServicoExcecao_Url', Name: 'CadastroAmbiente_dGridTcsAmbienteServicoExcecao_Url', DisplayName: 'Url Alternativa', ColumnSpan: 9, Visible: true},]},]},]},]},   ]};
};

