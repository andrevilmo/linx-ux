/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/
define(['./linx-spa-cadastrousuarioexterno_custom_pt-br.js'],
	function (custom){
		var result = {};

       result.languageFile = function () {
       	return 'pt-br';
       }

result.objectLanguage_CadastroUsuarioExterno = function () {
           var langResult = {
               Name: 'CadastroUsuarioExterno', Items: [

	 {Name: "CadastroUsuarioExterno_gbTcsUsuarioExterno", DisplayName: "TcsUsuarioExterno", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroUsuarioExterno_tbNomeUsuario", DisplayName: "Usuário", ColumnSpan: 9, Visible: true, Key: "NomeUsuario"},
	 {Name: "CadastroUsuarioExterno_tbCnpjCpf", DisplayName: "CPF / CNPJ", ColumnSpan: 6, Visible: true, Key: "CnpjCpf"},
	 {Name: "CadastroUsuarioExterno_tbEmail", DisplayName: "Email", ColumnSpan: 9, Visible: true, Key: "Email"},
	 {Name: "CadastroUsuarioExterno_tbFoneCelular", DisplayName: "Celular", ColumnSpan: 6, Visible: true, Key: "FoneCelular"},
	 {Name: "CadastroUsuarioExterno_dtDataCadastro", DisplayName: "Data Cadastro", ColumnSpan: 3, Visible: true, Key: "DataCadastro"},
	 {Name: "CadastroUsuarioExterno_tbIdentidadeExterna", DisplayName: "ID Externo", ColumnSpan: 9, Visible: true, Key: "IdentidadeExterna"},
	 {Name: "CadastroUsuarioExterno_cmbLxTipoAutenticador", DisplayName: "Autenticador", ColumnSpan: 6, Visible: true, Key: "LxTipoAutenticador"},
	 {Name: "CadastroUsuarioExterno_tbIdDispositivo", DisplayName: "ID Dispositivo", ColumnSpan: 9, Visible: true, Key: "IdDispositivo"},
	 {Name: "CadastroUsuarioExterno_ckInativo", DisplayName: "Inativo", ColumnSpan: 3, Visible: true, Key: "Inativo"},]},               ]
           }
           langResult.Items.concat(custom.getCustomTranslation().Items);
           return langResult;
        };
       return result;
  }
)

