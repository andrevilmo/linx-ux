/*substituir o sufixo ex.('pt-br') para o idioma do arquivo customizado*/
define(['./linx-spa-cadastrobancoservidor_custom_pt-br.js'],
	function (custom){
		var result = {};

       result.languageFile = function () {
       	return 'pt-br';
       }

result.objectLanguage_CadastroBancoServidor = function () {
           var langResult = {
               Name: 'CadastroBancoServidor', Items: [

	 {Name: "CadastroBancoServidor_gbTcsBancoServidor", DisplayName: "", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroBancoServidor_gbGroupBox_8d997674c54946deac2cb83504f11afd", DisplayName: "Conexão", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroBancoServidor_tbDescricaoBancoServidor", DisplayName: "Descrição Conexão Banco/Servidor", ColumnSpan: 12, Visible: true, Key: "DescricaoBancoServidor"},
	 {Name: "CadastroBancoServidor_tbNomeServidor", DisplayName: "Servidor", ColumnSpan: 12, Visible: true, Key: "NomeServidor"},
	 {Name: "CadastroBancoServidor_tbNomeBanco", DisplayName: "Banco de Dados", ColumnSpan: 12, Visible: true, Key: "NomeBanco"},
	 {Name: "CadastroBancoServidor_cmbLxTipoServidor", DisplayName: "Tipo Servidor", ColumnSpan: 12, Visible: true, Key: "LxTipoServidor"},
	 {Name: "CadastroBancoServidor_ntxSequencialInicial", DisplayName: "Sequencial Inicial", ColumnSpan: 6, Visible: true, Key: "SequencialInicial"},
	 {Name: "CadastroBancoServidor_ntxIncremento", DisplayName: "Incremento", ColumnSpan: 6, Visible: true, Key: "Incremento"},
	 {Name: "CadastroBancoServidor_edStringConexao", DisplayName: "String Conexão", ColumnSpan: 12, Visible: true, Key: "StringConexao"},]},
	 {Name: "CadastroBancoServidor_tcTcsBancoServidorTabControl", DisplayName: "TcsBancoServidor", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroBancoServidor_tiTcsAmbienteConexaoTabItem", DisplayName: "Providers - BM", ColumnSpan: 12, Visible: true, Items: [
	 {Name: "CadastroBancoServidor_dGridTcsAmbienteConexao", DisplayName: "DataGrid", ColumnSpan: 12, Visible: true, Items: [
	 {Id: "CadastroBancoServidor_lUpTcsAmbienteConexao_NomeConexao", Name: "CadastroBancoServidor_dGridTcsAmbienteConexao_NomeConexao", DisplayName: "Nome Provider BM", ColumnSpan: 9, Visible: true, LookUpName: "LookUpTcsAplicativoConexao", Key: "NomeConexao"},
	 {Id: "CadastroBancoServidor_lUpTcsAmbienteConexao_DescricaoAmbiente", Name: "CadastroBancoServidor_dGridTcsAmbienteConexao_DescricaoAmbiente", DisplayName: "Ambiente", ColumnSpan: 9, Visible: true, LookUpName: "LookUpTcsAmbiente", Key: "DescricaoAmbiente"},
	 {Id: "CadastroBancoServidor_tbTcsAmbienteConexao_DescricaoAplicacao", Name: "CadastroBancoServidor_dGridTcsAmbienteConexao_DescricaoAplicacao", DisplayName: "Aplicação", ColumnSpan: 9, Visible: true, Key: "DescricaoAplicacao"},
	 {Id: "CadastroBancoServidor_tbTcsAmbienteConexao_DescricaoAplicativo", Name: "CadastroBancoServidor_dGridTcsAmbienteConexao_DescricaoAplicativo", DisplayName: "Aplicativo", ColumnSpan: 9, Visible: true, Key: "DescricaoAplicativo"},
	 {Id: "CadastroBancoServidor_lUpTcsAmbienteConexao_NomeEmpresa", Name: "CadastroBancoServidor_dGridTcsAmbienteConexao_NomeEmpresa", DisplayName: "Empresa (Id Linx)", ColumnSpan: 9, Visible: true, LookUpName: "LookUpTcsAmbiente", Key: "NomeEmpresa"},
	 {Id: "CadastroBancoServidor_lUpTcsAmbienteConexao_IdLinx", Name: "CadastroBancoServidor_dGridTcsAmbienteConexao_IdLinx", DisplayName: "(Id Linx)", ColumnSpan: 5, Visible: true, LookUpName: "LookUpTcsAmbiente", Key: "IdLinx"},]},]},]},]},               ]
           }
           langResult.Items.concat(custom.getCustomTranslation().Items);
           return langResult;
        };
       return result;
  }
)

