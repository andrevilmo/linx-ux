/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/
define(['./linx-spa-administracaocache_custom_pt-br.js'],
	function (custom){
		var result = {};

       result.languageFile = function () {
       	return 'pt-br';
       }

result.objectLanguage_AdministracaoCache = function () {
           var langResult = {
               Name: 'AdministracaoCache', Items: [

	 {Name: "AdministracaoCache_gbTcsUsuarioAutenticacao", DisplayName: "Limpeza de Cache", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "AdministracaoCache_gbGroupBox_eb85cf739833465b908545f18da5e0f8", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "AdministracaoCache_ckGeral", DisplayName: "Cache Completo", ColumnSpan: 6, Visible: true, Key: "Geral"},]},
	 {Name: "AdministracaoCache_gbGroupBox_d73419e62700462d8d466f6b7844af42", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "AdministracaoCache_gbGroupBox_8c041599610e44cca4a41134fc4beec9", DisplayName: "Nível de Aplicação", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "AdministracaoCache_ckConexao", DisplayName: "Conexões", ColumnSpan: 12, Visible: true, Key: "Conexao"},
	 {Name: "AdministracaoCache_ckRelatorio", DisplayName: "Relatórios", ColumnSpan: 12, Visible: true, Key: "Relatorio"},]},
	 {Name: "AdministracaoCache_gbGroupBox_b64bd4714f3b454aab8161624db43421", DisplayName: "Nível de Usuário", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "AdministracaoCache_ckModulo", DisplayName: "Módulos / Menus / Transações", ColumnSpan: 12, Visible: true, Key: "Modulo"},
	 {Name: "AdministracaoCache_ckBandeiraRede", DisplayName: "Bandeira / Rede / Filiais / Grupo Econômico", ColumnSpan: 12, Visible: true, Key: "BandeiraRede"},
	 {Name: "AdministracaoCache_lUpNomeUsuario", DisplayName: "Usuário", ColumnSpan: 6, Visible: true, LookUpName: "LookUpTcsUsuarioAutenticacao", Key: "NomeUsuario"},]},]},
	 {Name: "AdministracaoCache_gbGroupBox_771b8bcb94774d54a4b27c0e4e14b368", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "AdministracaoCache_lblCustomControl283638062", DisplayName: "", ColumnSpan: 4, Visible: true, Key: ""},
	 {Name: "AdministracaoCache_btnClearCache", DisplayName: "Executar", ColumnSpan: 4, Visible: true, Key: ""},
	 {Name: "AdministracaoCache_lblCustomControl283636125", DisplayName: "", ColumnSpan: 4, Visible: true, Key: ""},]},]},               ]
           }
           langResult.Items.concat(custom.getCustomTranslation().Items);
           return langResult;
        };
       return result;
  }
)

