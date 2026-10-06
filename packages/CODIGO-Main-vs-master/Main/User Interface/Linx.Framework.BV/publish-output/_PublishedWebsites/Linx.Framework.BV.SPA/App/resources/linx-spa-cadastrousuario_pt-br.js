/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/
define(['./linx-spa-cadastrousuario_custom_pt-br.js'],
	function (custom){
		var result = {};

       result.languageFile = function () {
       	return 'pt-br';
       }

result.objectLanguage_CadastroUsuario = function () {
           var langResult = {
               Name: 'CadastroUsuario', Items: [

	 {Name: "CadastroUsuario_gbTcsUsuario901f4746e7c34bc2a250e8a92c1905b1", DisplayName: "Usuário", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroUsuario_gbGroupBox_80c7842ca176453387b074fe0d4d4a1e", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroUsuario_gbGroupBox_e861b3b6bb124db1b794a698672af9e9", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroUsuario_tbNomeUsuario", DisplayName: "Usuário", ColumnSpan: 6, Visible: true, Key: "NomeUsuario"},]},
	 {Name: "CadastroUsuario_gbgroupCopiaUsuario", DisplayName: "Cópia de usuário", ColumnSpan: 12, Visible: false, Items: [
	 {Name: "CadastroUsuario_lUpNomeUsuarioCopia", DisplayName: "Usuário Cópia", ColumnSpan: 9, Visible: false, LookUpName: "LookUpTcsUsuario", Key: "NomeUsuarioCopia"},
	 {Name: "CadastroUsuario_btnCustomControl4148737", DisplayName: "Cópia", ColumnSpan: 2, Visible: false, Key: ""},]},]},
	 {Name: "CadastroUsuario_tcTcsUsuarioTabControl", DisplayName: "TcsUsuario", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroUsuario_tiTcsUsuarioPerfilTabItem", DisplayName: "Perfil", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroUsuario_dGridTcsUsuarioPerfil", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroUsuario_lUpTcsUsuarioPerfil_DescPerfil", Name: "CadastroUsuario_dGridTcsUsuarioPerfil_DescPerfil", DisplayName: "Perfil", ColumnSpan: 9, Visible: true, LookUpName: "LookUpTcsPerfil", Key: "DescPerfil"},]},]},
	 {Name: "CadastroUsuario_tiTcsUsuarioRegraModuloTabItem", DisplayName: "Módulo", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroUsuario_dGridTcsUsuarioRegraModulo", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroUsuario_lUpTcsUsuarioRegraModulo_DescModulo", Name: "CadastroUsuario_dGridTcsUsuarioRegraModulo_DescModulo", DisplayName: "Módulo", ColumnSpan: 9, Visible: true, LookUpName: "LookUpTcsUsuarioRegraModulo", Key: "DescModulo"},
	 {Id: "CadastroUsuario_tbTcsUsuarioRegraModulo_DescAplicativo", Name: "CadastroUsuario_dGridTcsUsuarioRegraModulo_DescAplicativo", DisplayName: "Aplicativo", ColumnSpan: 9, Visible: true, Key: "DescAplicativo"},
	 {Id: "CadastroUsuario_cmbTcsUsuarioRegraModulo_LxRegraAcessoModulo", Name: "CadastroUsuario_dGridTcsUsuarioRegraModulo_LxRegraAcessoModulo", DisplayName: "Regra Acesso Módulo", ColumnSpan: 6, Visible: true, Key: "LxRegraAcessoModulo"},]},]},
	 {Name: "CadastroUsuario_tiTcsUsuarioRegraTransacaoTabItem", DisplayName: "Transação", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroUsuario_dGridTcsUsuarioRegraTransacao", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroUsuario_lUpTcsUsuarioRegraTransacao_DescTransacao", Name: "CadastroUsuario_dGridTcsUsuarioRegraTransacao_DescTransacao", DisplayName: "Transação", ColumnSpan: 9, Visible: true, LookUpName: "LookUpTcsUsuarioRegraTransacao", Key: "DescTransacao"},
	 {Id: "CadastroUsuario_lUpTcsUsuarioRegraTransacao_ClasseNome", Name: "CadastroUsuario_dGridTcsUsuarioRegraTransacao_ClasseNome", DisplayName: "Código Transação", ColumnSpan: 9, Visible: true, LookUpName: "LookUpTcsUsuarioRegraTransacao", Key: "ClasseNome"},
	 {Id: "CadastroUsuario_cmbTcsUsuarioRegraTransacao_LxRegraAcessoTransacao", Name: "CadastroUsuario_dGridTcsUsuarioRegraTransacao_LxRegraAcessoTransacao", DisplayName: "Regra Acesso Transação", ColumnSpan: 6, Visible: true, Key: "LxRegraAcessoTransacao"},]},]},
	 {Name: "CadastroUsuario_tiTcsUsuarioRegraColunaTabItem", DisplayName: "Coluna", ColumnSpan: 12, Visible: false, Items: [
	 {Name: "CadastroUsuario_dGridTcsUsuarioRegraColuna", DisplayName: "DataGrid", ColumnSpan: 12, Visible: false, Items: [
	 {Id: "CadastroUsuario_lUpTcsUsuarioRegraColuna_DescTransacao", Name: "CadastroUsuario_dGridTcsUsuarioRegraColuna_DescTransacao", DisplayName: "Transação", ColumnSpan: 8, Visible: false, LookUpName: "LookUpTcsUsuarioRegraColuna", Key: "DescTransacao"},
	 {Id: "CadastroUsuario_lUpTcsUsuarioRegraColuna_ClasseNome", Name: "CadastroUsuario_dGridTcsUsuarioRegraColuna_ClasseNome", DisplayName: "Código Transação", ColumnSpan: 8, Visible: false, LookUpName: "LookUpTcsUsuarioRegraColuna", Key: "ClasseNome"},
	 {Id: "CadastroUsuario_tbTcsUsuarioRegraColuna_TransacaoColuna", Name: "CadastroUsuario_dGridTcsUsuarioRegraColuna_TransacaoColuna", DisplayName: "Transação Coluna", ColumnSpan: 9, Visible: false, Key: "TransacaoColuna"},
	 {Id: "CadastroUsuario_cmbTcsUsuarioRegraColuna_LxRegraAcessoColuna", Name: "CadastroUsuario_dGridTcsUsuarioRegraColuna_LxRegraAcessoColuna", DisplayName: "Regra Acesso Coluna", ColumnSpan: 6, Visible: false, Key: "LxRegraAcessoColuna"},]},]},
	 {Name: "CadastroUsuario_tiTcsUsuarioBandeiraRedeTabItem", DisplayName: "Bandeira / Rede", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroUsuario_dGridTcsUsuarioBandeiraRede", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroUsuario_lUpTcsUsuarioBandeiraRede_DescBandeiraRede", Name: "CadastroUsuario_dGridTcsUsuarioBandeiraRede_DescBandeiraRede", DisplayName: "Bandeira / Rede", ColumnSpan: 9, Visible: true, LookUpName: "LookUpTbcBandeiraRede", Key: "DescBandeiraRede"},]},]},
	 {Name: "CadastroUsuario_tiTcsUsuarioFilialTabItem", DisplayName: "Filial", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroUsuario_dGridTcsUsuarioFilial", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroUsuario_lUpTcsUsuarioFilial_CodigoFilial", Name: "CadastroUsuario_dGridTcsUsuarioFilial_CodigoFilial", DisplayName: "Código Filial", ColumnSpan: 6, Visible: true, LookUpName: "LookUpTbcFilial", Key: "CodigoFilial"},
	 {Id: "CadastroUsuario_lUpTcsUsuarioFilial_NomeFilial", Name: "CadastroUsuario_dGridTcsUsuarioFilial_NomeFilial", DisplayName: "Nome Fantasia", ColumnSpan: 9, Visible: true, LookUpName: "LookUpTbcFilial", Key: "NomeFilial"},]},]},
	 {Name: "CadastroUsuario_tiTcsUsuarioLayoutTabItem", DisplayName: "Layouts", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroUsuario_dGridTcsUsuarioLayout", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroUsuario_lUpTcsUsuarioLayout_DescLayout", Name: "CadastroUsuario_dGridTcsUsuarioLayout_DescLayout", DisplayName: "Layout", ColumnSpan: 9, Visible: true, LookUpName: "LookUpTcsLayout", Key: "DescLayout"},
	 {Id: "CadastroUsuario_edTcsUsuarioLayout_Detalhes", Name: "CadastroUsuario_dGridTcsUsuarioLayout_Detalhes", DisplayName: "Detalhes", ColumnSpan: 9, Visible: true, Key: "Detalhes"},
	 {Id: "CadastroUsuario_ckTcsUsuarioLayout_Inativo", Name: "CadastroUsuario_dGridTcsUsuarioLayout_Inativo", DisplayName: "Inativo", ColumnSpan: 3, Visible: true, Key: "Inativo"},]},]},
	 {Name: "CadastroUsuario_tiCopy_360629234_360629234_TabItem_1e8d7de127cb46f193c1aaf8107ff8bc", DisplayName: "Dados", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroUsuario_gbGroupBox_cb8535f544794df597d576d728b56350", DisplayName: "Cadastro", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroUsuario_cmbLxPfjFisicaJuridica", DisplayName: "Pessoa Física / Juridíca", ColumnSpan: 6, Visible: true, Key: "LxPfjFisicaJuridica"},
	 {Name: "CadastroUsuario_mskCnpjCpf", DisplayName: "CPF/CNPJ", ColumnSpan: 6, Visible: true, Key: "CnpjCpf"},
	 {Name: "CadastroUsuario_tbInscrEstadualRg", DisplayName: "Inscr. Estadual / RG", ColumnSpan: 6, Visible: true, Key: "InscrEstadualRg"},
	 {Name: "CadastroUsuario_dtDataAlteracao", DisplayName: "Alteração", ColumnSpan: 3, Visible: true, Key: "DataAlteracao"},
	 {Name: "CadastroUsuario_dtDataCadastro", DisplayName: "Cadastro", ColumnSpan: 3, Visible: true, Key: "DataCadastro"},]},
	 {Name: "CadastroUsuario_gbGroupBox_cb2e59617f0b4ec08aec5f5bcfc3ea03", DisplayName: "Endereço", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroUsuario_cmbLxTipoLogradouro", DisplayName: "Tipo Logradouro", ColumnSpan: 6, Visible: true, Key: "LxTipoLogradouro"},
	 {Name: "CadastroUsuario_tbLogradouro", DisplayName: "Logradouro / Número", ColumnSpan: 8, Visible: true, Key: "Logradouro"},
	 {Name: "CadastroUsuario_tbNumero", DisplayName: "Número", ColumnSpan: 4, Visible: true, Key: "Numero"},
	 {Name: "CadastroUsuario_tbComplemento", DisplayName: "Complemento", ColumnSpan: 9, Visible: true, Key: "Complemento"},
	 {Name: "CadastroUsuario_tbCep", DisplayName: "CEP", ColumnSpan: 3, Visible: true, Key: "Cep"},
	 {Name: "CadastroUsuario_tbBairro", DisplayName: "Bairro", ColumnSpan: 9, Visible: true, Key: "Bairro"},
	 {Name: "CadastroUsuario_tbMunicipio", DisplayName: "Município / UF", ColumnSpan: 9, Visible: true, Key: "Municipio"},
	 {Name: "CadastroUsuario_tbUf", DisplayName: "UF", ColumnSpan: 1, Visible: true, Key: "Uf"},
	 {Name: "CadastroUsuario_tbObsEndereco", DisplayName: "Obs. Endereço", ColumnSpan: 9, Visible: true, Key: "ObsEndereco"},]},
	 {Name: "CadastroUsuario_gbGroupBox_f95f77e96c3c47f7811aec5b4d05a1e9", DisplayName: "Telefones", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroUsuario_tbFoneFixo", DisplayName: "Fixo / Ramal", ColumnSpan: 6, Visible: true, Key: "FoneFixo"},
	 {Name: "CadastroUsuario_tbRamal", DisplayName: "Ramal", ColumnSpan: 2, Visible: true, Key: "Ramal"},
	 {Name: "CadastroUsuario_tbFoneCelular", DisplayName: "Móvel", ColumnSpan: 6, Visible: true, Key: "FoneCelular"},]},]},]},]},               ]
           }
           langResult.Items.concat(custom.getCustomTranslation().Items);
           return langResult;
        };
       return result;
  }
)

