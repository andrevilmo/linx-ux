/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/
define(['./linx-spa-cadastromodulogrupo_custom_pt-br.js'],
	function (custom){
		var result = {};

       result.languageFile = function () {
       	return 'pt-br';
       }

result.objectLanguage_CadastroModuloGrupo = function () {
           var langResult = {
               Name: 'CadastroModuloGrupo', Items: [

	 {Name: "CadastroModuloGrupo_gbTcsModuloGrupo", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroModuloGrupo_gbGroupBox_f6b8fd2d90a642b08e99fd6278c88cf9", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroModuloGrupo_tbIdGrupoModulo", DisplayName: "Id", ColumnSpan: 2, Visible: true, Key: "IdGrupoModulo"},
	 {Name: "CadastroModuloGrupo_tbDescGrupoModulo", DisplayName: "Descrição", ColumnSpan: 10, Visible: true, Key: "DescGrupoModulo"},
	 {Name: "CadastroModuloGrupo_lUpDescricaoAplicativo", DisplayName: "Aplicativo", ColumnSpan: 12, Visible: true, LookUpName: "LookUpTcsAplicativo", Key: "DescricaoAplicativo"},]},
	 {Name: "CadastroModuloGrupo_tcTcsModuloGrupoTabControl", DisplayName: "Grupo de Módulos", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroModuloGrupo_tiTcsModuloDoGrupoDetalheTabItem", DisplayName: "Módulo", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroModuloGrupo_dGridTcsModuloDoGrupoDetalhe", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroModuloGrupo_lUpTcsModuloDoGrupoDetalhe_DescModulo", Name: "CadastroModuloGrupo_dGridTcsModuloDoGrupoDetalhe_DescModulo", DisplayName: "Módulo", ColumnSpan: 9, Visible: true, LookUpName: "LookUpTcsModuloDoGrupoDetalhe", Key: "DescModulo"},]},]},]},]},               ]
           }
           langResult.Items.concat(custom.getCustomTranslation().Items);
           return langResult;
        };
       return result;
  }
)

