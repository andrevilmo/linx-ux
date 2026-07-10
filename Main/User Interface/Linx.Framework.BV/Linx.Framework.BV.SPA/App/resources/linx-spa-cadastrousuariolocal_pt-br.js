/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/
define(['./linx-spa-cadastrousuariolocal_custom_pt-br.js'],
	function (custom){
		var result = {};

       result.languageFile = function () {
       	return 'pt-br';
       }

result.objectLanguage_CadastroUsuarioLocal = function () {
           var langResult = {
               Name: 'CadastroUsuarioLocal', Items: [

	 {Name: "CadastroUsuarioLocal_gbTcsUsuarioAutenticacao", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroUsuarioLocal_gbCustomContainer_e77f5282cafd4e43a76f1d47254adbaa", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroUsuarioLocal_lblCustomControl6170546", DisplayName: "", ColumnSpan: 10, Visible: true, Key: ""},
	 {Name: "CadastroUsuarioLocal_btnRetroceder", DisplayName: "", ColumnSpan: 1, Visible: true, Key: ""},
	 {Name: "CadastroUsuarioLocal_btnAvancar", DisplayName: "", ColumnSpan: 1, Visible: true, Key: ""},]},
	 {Name: "CadastroUsuarioLocal_tcTabControl_e088c8f171a7432b91ea7c4677cbbc0e", DisplayName: "TabControl", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroUsuarioLocal_tiUsuario", DisplayName: "Usuário", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroUsuarioLocal_gbGroupBox_d8e00b0a8c5e48efaae8165f0a7a009a", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroUsuarioLocal_gbCustomContainer_c6366698bd76405a98b50d8fc2b7c79c", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroUsuarioLocal_gbGroupBox_1927a4b5924b4278a4149959828d64ae", DisplayName: "", ColumnSpan: 6, Visible: true, Items: [
	 {Name: "CadastroUsuarioLocal_tbNomeUsuario", DisplayName: "Nome", ColumnSpan: 12, Visible: true, Key: "NomeUsuario"},
	 {Name: "CadastroUsuarioLocal_tbNomeAutenticacao", DisplayName: "Usuário Autenticação", ColumnSpan: 12, Visible: true, Key: "NomeAutenticacao"},
	 {Name: "CadastroUsuarioLocal_tbNomeCurtoUsuario", DisplayName: "Apelido", ColumnSpan: 12, Visible: true, Key: "NomeCurtoUsuario"},
	 {Name: "CadastroUsuarioLocal_tbEmail", DisplayName: "Email", ColumnSpan: 12, Visible: true, Key: "Email"},
	 {Name: "CadastroUsuarioLocal_ckInativo", DisplayName: "Inativo", ColumnSpan: 6, Visible: true, Key: "Inativo"},]},
	 {Name: "CadastroUsuarioLocal_gbGroupBox_294d36395b1f414997b597e9a47dd1b7", DisplayName: "", ColumnSpan: 2, Visible: true, Items: [
	 {Name: "CadastroUsuarioLocal_dtVigenciaInicial", DisplayName: "Vigência Inicial", ColumnSpan: 12, Visible: true, Key: "VigenciaInicial"},
	 {Name: "CadastroUsuarioLocal_dtVigenciaFinal", DisplayName: "Vigência Final", ColumnSpan: 12, Visible: true, Key: "VigenciaFinal"},
	 {Name: "CadastroUsuarioLocal_dtDataExpiracaoSenha", DisplayName: "Expiração Senha", ColumnSpan: 12, Visible: true, Key: "DataExpiracaoSenha"},
	 {Name: "CadastroUsuarioLocal_dtDataCadastro", DisplayName: "Cadastro", ColumnSpan: 12, Visible: true, Key: "DataCadastro"},
	 {Name: "CadastroUsuarioLocal_dtDataAlteracao", DisplayName: "Alteração", ColumnSpan: 12, Visible: true, Key: "DataAlteracao"},]},]},
	 {Name: "CadastroUsuarioLocal_gbUserPasswordGroupBox", DisplayName: "Senha Usuário", ColumnSpan: 12, Visible: false, Items: [
	 {Name: "CadastroUsuarioLocal_tbConfirmacaoUsuario", DisplayName: "Senha", ColumnSpan: 6, Visible: true, Key: "ConfirmacaoUsuario"},
	 {Name: "CadastroUsuarioLocal_lblCustomControl4499703", DisplayName: "", ColumnSpan: 6, Visible: true, Key: ""},
	 {Name: "CadastroUsuarioLocal_tbConfirmacaoUsuario1", DisplayName: "Confirmação", ColumnSpan: 6, Visible: true, Key: "ConfirmacaoUsuario1"},]},]},]},
	 {Name: "CadastroUsuarioLocal_tiDados", DisplayName: "Dados", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroUsuarioLocal_gbCustomContainer_248f7e26b9b640b8ae9b543cf503d367", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroUsuarioLocal_gbGroupBox_cb8535f544794df597d576d728b56350", DisplayName: "Cadastro", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroUsuarioLocal_cmbLxPfjFisicaJuridica", DisplayName: "Pessoa Física / Juridíca", ColumnSpan: 6, Visible: true, Key: "LxPfjFisicaJuridica"},
	 {Name: "CadastroUsuarioLocal_mskCnpjCpf", DisplayName: "CPF/CNPJ", ColumnSpan: 6, Visible: true, Key: "CnpjCpf"},
	 {Name: "CadastroUsuarioLocal_tbInscrEstadualRg", DisplayName: "Inscr. Estadual / RG", ColumnSpan: 6, Visible: true, Key: "InscrEstadualRg"},]},
	 {Name: "CadastroUsuarioLocal_gbGroupBox_cb2e59617f0b4ec08aec5f5bcfc3ea03", DisplayName: "Endereço", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroUsuarioLocal_cmbLxTipoLogradouro", DisplayName: "Tipo Logradouro", ColumnSpan: 6, Visible: true, Key: "LxTipoLogradouro"},
	 {Name: "CadastroUsuarioLocal_tbLogradouro", DisplayName: "Logradouro", ColumnSpan: 8, Visible: true, Key: "Logradouro"},
	 {Name: "CadastroUsuarioLocal_tbNumero", DisplayName: "Número", ColumnSpan: 4, Visible: true, Key: "Numero"},
	 {Name: "CadastroUsuarioLocal_tbComplemento", DisplayName: "Complemento", ColumnSpan: 9, Visible: true, Key: "Complemento"},
	 {Name: "CadastroUsuarioLocal_tbCEP", DisplayName: "CEP", ColumnSpan: 3, Visible: true, Key: "Cep"},
	 {Name: "CadastroUsuarioLocal_tbBairro", DisplayName: "Bairro", ColumnSpan: 9, Visible: true, Key: "Bairro"},
	 {Name: "CadastroUsuarioLocal_tbMunicipio", DisplayName: "Município", ColumnSpan: 9, Visible: true, Key: "Municipio"},
	 {Name: "CadastroUsuarioLocal_tbUf", DisplayName: "UF", ColumnSpan: 1, Visible: true, Key: "Uf"},
	 {Name: "CadastroUsuarioLocal_tbObsEndereco", DisplayName: "Obs. Endereço", ColumnSpan: 9, Visible: true, Key: "ObsEndereco"},]},
	 {Name: "CadastroUsuarioLocal_gbGroupBox_f95f77e96c3c47f7811aec5b4d05a1e9", DisplayName: "Telefones", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroUsuarioLocal_tbFoneFixo", DisplayName: "Fixo / Ramal", ColumnSpan: 6, Visible: true, Key: "FoneFixo"},
	 {Name: "CadastroUsuarioLocal_tbRamal", DisplayName: "Ramal", ColumnSpan: 2, Visible: true, Key: "Ramal"},
	 {Name: "CadastroUsuarioLocal_tbFoneCelular", DisplayName: "Móvel", ColumnSpan: 6, Visible: true, Key: "FoneCelular"},]},]},]},
	 {Name: "CadastroUsuarioLocal_tiAcessos", DisplayName: "Ambientes", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroUsuarioLocal_gbCustomContainer_205717a2eaf84c9c8dc7ba28538be154", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroUsuarioLocal_dGridTcsUsuarioAutenticacaoAcesso", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroUsuarioLocal_lUpTcsUsuarioAutenticacaoAcesso_DescricaoAmbiente", Name: "CadastroUsuarioLocal_dGridTcsUsuarioAutenticacaoAcesso_DescricaoAmbiente", DisplayName: "Ambiente", ColumnSpan: 9, Visible: true, LookUpName: "LookUpTcsAmbiente2", Key: "DescricaoAmbiente"},
	 {Id: "CadastroUsuarioLocal_tbTcsUsuarioAutenticacaoAcesso_DescricaoAplicativo", Name: "CadastroUsuarioLocal_dGridTcsUsuarioAutenticacaoAcesso_DescricaoAplicativo", DisplayName: "Aplicativo", ColumnSpan: 9, Visible: true, Key: "DescricaoAplicativo"},
	 {Id: "CadastroUsuarioLocal_tbTcsUsuarioAutenticacaoAcesso_NomeEmpresa", Name: "CadastroUsuarioLocal_dGridTcsUsuarioAutenticacaoAcesso_NomeEmpresa", DisplayName: "Empresa", ColumnSpan: 9, Visible: true, Key: "NomeEmpresa"},
	 {Id: "CadastroUsuarioLocal_ckTcsUsuarioAutenticacaoAcesso_IndicaAcessoPadrao", Name: "CadastroUsuarioLocal_dGridTcsUsuarioAutenticacaoAcesso_IndicaAcessoPadrao", DisplayName: "Acesso Padrão", ColumnSpan: 6, Visible: true, Key: "IndicaAcessoPadrao"},
	 {Id: "CadastroUsuarioLocal_lUpTcsUsuarioAutenticacaoAcesso_DescricaoAmbienteRelacionado", Name: "CadastroUsuarioLocal_dGridTcsUsuarioAutenticacaoAcesso_DescricaoAmbienteRelacionado", DisplayName: "Ambiente Relacionado", ColumnSpan: 9, Visible: true, LookUpName: "LookUpTcsAmbiente2Relacionado", Key: "DescricaoAmbienteRelacionado"},
	 {Id: "CadastroUsuarioLocal_tbTcsUsuarioAutenticacaoAcesso_Perfil", Name: "CadastroUsuarioLocal_dGridTcsUsuarioAutenticacaoAcesso_Perfil", DisplayName: "", ColumnSpan: 8, Visible: true, Key: "Perfil"},]},]},]},]},]},               ]
           }
           langResult.Items.concat(custom.getCustomTranslation().Items);
           return langResult;
        };
       return result;
  }
)

