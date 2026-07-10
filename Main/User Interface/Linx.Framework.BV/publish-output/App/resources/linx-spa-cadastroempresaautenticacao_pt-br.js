/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/
define(['./linx-spa-cadastroempresaautenticacao_custom_pt-br.js'],
	function (custom){
		var result = {};

       result.languageFile = function () {
       	return 'pt-br';
       }

result.objectLanguage_CadastroEmpresaAutenticacao = function () {
           var langResult = {
               Name: 'CadastroEmpresaAutenticacao', Items: [

	 {Name: "CadastroEmpresaAutenticacao_gbTcsEmpresaAutenticacao", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroEmpresaAutenticacao_gbGroupBox_bc9d3794dbfe4ec3a31a8d65dbd6b9dc", DisplayName: "Empresa", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroEmpresaAutenticacao_tbIdLinx", DisplayName: "ID Linx", ColumnSpan: 2, Visible: true, Key: "IdLinx"},
	 {Name: "CadastroEmpresaAutenticacao_tbUidEmpresa", DisplayName: "Uid Empresa", ColumnSpan: 2, Visible: true, Key: "UidEmpresa"},
	 {Name: "CadastroEmpresaAutenticacao_mskCnpjCpf", DisplayName: "Cnpj", ColumnSpan: 2, Visible: true, Key: "CnpjCpf"},
	 {Name: "CadastroEmpresaAutenticacao_tbNomeEmpresa", DisplayName: "Empresa", ColumnSpan: 9, Visible: true, Key: "NomeEmpresa"},]},
	 {Name: "CadastroEmpresaAutenticacao_tcTcsEmpresaAutenticacaoTabControl", DisplayName: "TcsEmpresaAutenticacao", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroEmpresaAutenticacao_tiTcsEmpresaModuloTabItem", DisplayName: "Módulos Permitidos", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroEmpresaAutenticacao_dGridTcsEmpresaModulo", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroEmpresaAutenticacao_lUpTcsEmpresaModulo_DescModulo", Name: "CadastroEmpresaAutenticacao_dGridTcsEmpresaModulo_DescModulo", DisplayName: "Módulo", ColumnSpan: 9, Visible: true, LookUpName: "LookUpTcsModuloAutorizacao", Key: "DescModulo"},
	 {Id: "CadastroEmpresaAutenticacao_tbTcsEmpresaModulo_DescricaoAplicativo", Name: "CadastroEmpresaAutenticacao_dGridTcsEmpresaModulo_DescricaoAplicativo", DisplayName: "Aplicativo", ColumnSpan: 9, Visible: true, Key: "DescricaoAplicativo"},]},]},
	 {Name: "CadastroEmpresaAutenticacao_tiTcsEmpresaGpeconTabItem", DisplayName: "Empresa / Grupo Econômico", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroEmpresaAutenticacao_dGridTcsEmpresaGpecon", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroEmpresaAutenticacao_lUpTcsEmpresaGpecon_IdLinxGpecon", Name: "CadastroEmpresaAutenticacao_dGridTcsEmpresaGpecon_IdLinxGpecon", DisplayName: "Id Linx Empresa / Grupo Econômico", ColumnSpan: 5, Visible: true, LookUpName: "LookUpTcsEmpresaAutenticacaoGpecon", Key: "IdLinxGpecon"},
	 {Id: "CadastroEmpresaAutenticacao_lUpTcsEmpresaGpecon_GrupoEconomico", Name: "CadastroEmpresaAutenticacao_dGridTcsEmpresaGpecon_GrupoEconomico", DisplayName: "Empresa / Grupo Econômico", ColumnSpan: 9, Visible: true, LookUpName: "LookUpTcsEmpresaAutenticacaoGpecon", Key: "GrupoEconomico"},]},]},
	 {Name: "CadastroEmpresaAutenticacao_tiTcsAmbienteTabItem", DisplayName: "Ambientes", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroEmpresaAutenticacao_dGridTcsAmbiente", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroEmpresaAutenticacao_lUpTcsAmbiente_DescricaoAmbiente", Name: "CadastroEmpresaAutenticacao_dGridTcsAmbiente_DescricaoAmbiente", DisplayName: "Ambiente", ColumnSpan: 9, Visible: true, LookUpName: "LookUpTcsAmbiente", Key: "DescricaoAmbiente"},
	 {Id: "CadastroEmpresaAutenticacao_lUpTcsAmbiente_DescricaoAplicacao", Name: "CadastroEmpresaAutenticacao_dGridTcsAmbiente_DescricaoAplicacao", DisplayName: "Aplicação", ColumnSpan: 9, Visible: true, LookUpName: "LookUpTcsAplicacao", Key: "DescricaoAplicacao"},
	 {Id: "CadastroEmpresaAutenticacao_ckTcsAmbiente_EmDesenvolvimento", Name: "CadastroEmpresaAutenticacao_dGridTcsAmbiente_EmDesenvolvimento", DisplayName: "Em Desenvolvimento", ColumnSpan: 9, Visible: true, Key: "EmDesenvolvimento"},]},]},
	 {Name: "CadastroEmpresaAutenticacao_tiTcsUsuarioAutenticacaoTabItem", DisplayName: "Usuários", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroEmpresaAutenticacao_dGridTcsUsuarioAutenticacao", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroEmpresaAutenticacao_lUpNome", Name: "CadastroEmpresaAutenticacao_dGridTcsUsuarioAutenticacao_NomeUsuario", DisplayName: "Nome Usuário", ColumnSpan: 9, Visible: true, LookUpName: "LookUpTcsUsuarioAutenticacao", Key: "NomeUsuario"},
	 {Id: "CadastroEmpresaAutenticacao_tbTcsUsuarioAutenticacao_NomeAutenticacao", Name: "CadastroEmpresaAutenticacao_dGridTcsUsuarioAutenticacao_NomeAutenticacao", DisplayName: "Usuário Autenticação", ColumnSpan: 9, Visible: true, Key: "NomeAutenticacao"},]},]},]},]},               ]
           }
           langResult.Items.concat(custom.getCustomTranslation().Items);
           return langResult;
        };
       return result;
  }
)

