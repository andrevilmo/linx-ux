/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/
define(['./linx-spa-cadastrousuariolocalperfil_custom_pt-br.js'],
	function (custom){
		var result = {};

       result.languageFile = function () {
       	return 'pt-br';
       }

result.objectLanguage_CadastroUsuarioLocalPerfil = function () {
           var langResult = {
               Name: 'CadastroUsuarioLocalPerfil', Items: [

	 {Name: "CadastroUsuarioLocalPerfil_gbTcsUsuarioAutenticacaoPerfil", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroUsuarioLocalPerfil_cntCustomContainer_0099841a4f844165915a02c7af734ba2", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroUsuarioLocalPerfil_tbNomeUsuario", DisplayName: "Usuário", ColumnSpan: 6, Visible: true, Key: "NomeUsuario"},
	 {Name: "CadastroUsuarioLocalPerfil_tbNomeEmpresa", DisplayName: "Empresa", ColumnSpan: 9, Visible: true, Key: "NomeEmpresa"},]},
	 {Name: "CadastroUsuarioLocalPerfil_cntGroupBox_f79f54ca30794705a4c5964a4e76a2e8", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroUsuarioLocalPerfil_dGridExpander_e4f439559d84476db94e9b74fdcf41bd", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroUsuarioLocalPerfil_lUpDescPerfil", Name: "CadastroUsuarioLocalPerfil_dGridExpander_e4f439559d84476db94e9b74fdcf41bd_DescPerfil", DisplayName: "Perfil", ColumnSpan: 9, Visible: true, LookUpName: "LookUpTcsPerfil", Key: "DescPerfil"},]},]},]},               ]
           }
           langResult.Items.concat(custom.getCustomTranslation().Items);
           return langResult;
        };
       return result;
  }
)

