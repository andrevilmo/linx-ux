																																							


    /* jshint ignore:start */

    var name = namespace.common.buildNameSpace('factories.ClientErpDataDomainsFactory');

    var domainsFactory = function () {

		var dataDomain = {
			domains: [],    
			registerDomains: function () {

						dataDomain.domains['TipoMensagem'] = [];
						dataDomain.domains['TipoMensagem'][0] = { id: 3, name: 'Erro' };
						dataDomain.domains['TipoMensagem'][1] = { id: 1, name: 'Informação' };
						dataDomain.domains['TipoMensagem'][2] = { id: 4, name: 'Sucesso' };
						dataDomain.domains['TipoMensagem'][3] = { id: 2, name: 'Alerta' };
						dataDomain.domains['TIPO_OPERACAO'] = [];
						dataDomain.domains['TIPO_OPERACAO'][0] = { id: 'I', name: 'Inserção' };
						dataDomain.domains['TIPO_OPERACAO'][1] = { id: 'E', name: 'Alteração' };
						dataDomain.domains['TIPO_OPERACAO'][2] = { id: 'D', name: 'Exclusão' };
						dataDomain.domains['TipoTransacao'] = [];
						dataDomain.domains['TipoTransacao'][0] = { id: 7, name: 'Assistente' };
						dataDomain.domains['TipoTransacao'][1] = { id: 8, name: 'Dashboard' };
						dataDomain.domains['TipoTransacao'][2] = { id: 2, name: 'ERP' };
						dataDomain.domains['TipoTransacao'][3] = { id: 6, name: 'ERP App' };
						dataDomain.domains['TipoTransacao'][4] = { id: 4, name: 'Excel' };
						dataDomain.domains['TipoTransacao'][5] = { id: 3, name: 'Loja' };
						dataDomain.domains['TipoTransacao'][6] = { id: 5, name: 'Mobile' };
						dataDomain.domains['TipoTransacao'][7] = { id: 1, name: 'Todos' };
						dataDomain.domains['RegraAcesso'] = [];
						dataDomain.domains['RegraAcesso'][0] = { id: 1, name: 'Acesso Bloqueado' };
						dataDomain.domains['RegraAcesso'][1] = { id: 2, name: 'Acesso Total' };
						dataDomain.domains['RegraAcesso'][2] = { id: 13, name: 'Acesso por Transação' };
						dataDomain.domains['RegraAcesso'][3] = { id: 5, name: 'Alterar' };
						dataDomain.domains['RegraAcesso'][4] = { id: 12, name: 'Criar Pesquisa' };
						dataDomain.domains['RegraAcesso'][5] = { id: 10, name: 'Criar Relatório' };
						dataDomain.domains['RegraAcesso'][6] = { id: 6, name: 'Excluir' };
						dataDomain.domains['RegraAcesso'][7] = { id: 9, name: 'Exportar' };
						dataDomain.domains['RegraAcesso'][8] = { id: 8, name: 'Imprimir' };
						dataDomain.domains['RegraAcesso'][9] = { id: 4, name: 'Incluir' };
						dataDomain.domains['RegraAcesso'][10] = { id: 11, name: 'Layout' };
						dataDomain.domains['RegraAcesso'][11] = { id: 7, name: 'Pesquisa Especial' };
						dataDomain.domains['RegraAcesso'][12] = { id: 3, name: 'Pesquisar' };
						dataDomain.domains['RegraAcesso'][13] = { id: 99, name: 'Regra Transação' };
						dataDomain.domains['RegraAcessoColuna'] = [];
						dataDomain.domains['RegraAcessoColuna'][0] = { id: 1, name: 'Acesso Bloqueado' };
						dataDomain.domains['RegraAcessoColuna'][1] = { id: 2, name: 'Acesso Total' };
						dataDomain.domains['RegraAcessoColuna'][2] = { id: 4, name: 'Alterar' };
						dataDomain.domains['RegraAcessoColuna'][3] = { id: 5, name: 'Pesquisar' };
						dataDomain.domains['RegraAcessoColuna'][4] = { id: 99, name: 'Regra Transação' };
						dataDomain.domains['RegraAcessoColuna'][5] = { id: 3, name: 'Visualizar' };
						dataDomain.domains['TipoObjeto'] = [];
						dataDomain.domains['TipoObjeto'][0] = { id: 1, name: 'BO' };
						dataDomain.domains['TipoObjeto'][1] = { id: 3, name: 'Campo' };
						dataDomain.domains['TipoObjeto'][2] = { id: 10, name: 'Filtro' };
						dataDomain.domains['TipoObjeto'][3] = { id: 9, name: 'Layout' };
						dataDomain.domains['TipoObjeto'][4] = { id: 6, name: 'Relatório' };
						dataDomain.domains['TipoObjeto'][5] = { id: 5, name: 'Stored Procedure' };
						dataDomain.domains['TipoObjeto'][6] = { id: 8, name: 'Template de ação de Workflow' };
						dataDomain.domains['TipoObjeto'][7] = { id: 2, name: 'Transação' };
						dataDomain.domains['TipoObjeto'][8] = { id: 4, name: 'Trigger' };
						dataDomain.domains['TipoObjeto'][9] = { id: 11, name: 'Extensão (Objeto de entrada)' };
						dataDomain.domains['TipoObjeto'][10] = { id: 7, name: 'Workflow' };
						dataDomain.domains['TipoValidacaoParametro'] = [];
						dataDomain.domains['TipoValidacaoParametro'][0] = { id: 8, name: 'Sem Validação' };
						dataDomain.domains['TipoValidacaoParametro'][1] = { id: 2, name: 'Validação Contra Tabela (Combo)' };
						dataDomain.domains['TipoValidacaoParametro'][2] = { id: 3, name: 'Validação Contra Faixa' };
						dataDomain.domains['TipoValidacaoParametro'][3] = { id: 4, name: 'Validação Contra Objeto CRM' };
						dataDomain.domains['TipoValidacaoParametro'][4] = { id: 1, name: 'Validação Contra Tabela (Valida)' };
						dataDomain.domains['TipoValorParametro'] = [];
						dataDomain.domains['TipoValorParametro'][0] = { id: 2, name: 'Caractere' };
						dataDomain.domains['TipoValorParametro'][1] = { id: 3, name: 'Data' };
						dataDomain.domains['TipoValorParametro'][2] = { id: 4, name: 'Lógico' };
						dataDomain.domains['TipoValorParametro'][3] = { id: 1, name: 'Numérico' };
						dataDomain.domains['TipoValorParametro'][4] = { id: 5, name: 'Senha' };
						dataDomain.domains['TipoDocumento'] = [];
						dataDomain.domains['TipoDocumento'][0] = { id: 3, name: 'Detalhe/Estampa' };
						dataDomain.domains['TipoDocumento'][1] = { id: 4, name: '360°' };
						dataDomain.domains['TipoDocumento'][2] = { id: 2, name: 'Matriz Para Transformação' };
						dataDomain.domains['TipoDocumento'][3] = { id: 1, name: 'Normal' };
						dataDomain.domains['TipoDocumento'][4] = { id: 5, name: 'Vídeos' };
						dataDomain.domains['TipoExtensao'] = [];
						dataDomain.domains['TipoExtensao'][0] = { id: 1, name: 'JPEG' };
						dataDomain.domains['TipoExtensao'][1] = { id: 2, name: 'JPG' };
						dataDomain.domains['TipoExtensao'][2] = { id: 3, name: 'PNG' };
						dataDomain.domains['TipoExtensao'][3] = { id: 4, name: 'WMV' };
						dataDomain.domains['TipoArquivo'] = [];
						dataDomain.domains['TipoArquivo'][0] = { id: 'E', name: 'Excel' };
						dataDomain.domains['TipoArquivo'][1] = { id: 'T', name: 'Text' };
						dataDomain.domains['TipoArquivo'][2] = { id: 'G', name: 'Todos' };
						dataDomain.domains['TipoArquivo'][3] = { id: 'X', name: 'XML' };
						dataDomain.domains['TipoDado'] = [];
						dataDomain.domains['TipoDado'][0] = { id: 'BLN', name: 'Boolean' };
						dataDomain.domains['TipoDado'][1] = { id: 'BYT', name: 'Byte' };
						dataDomain.domains['TipoDado'][2] = { id: 'DTE', name: 'Date' };
						dataDomain.domains['TipoDado'][3] = { id: 'DEC', name: 'Decimal' };
						dataDomain.domains['TipoDado'][4] = { id: 'DBL', name: 'Double' };
						dataDomain.domains['TipoDado'][5] = { id: 'INT', name: 'Integer' };
						dataDomain.domains['TipoDado'][6] = { id: 'LNG', name: 'Long' };
						dataDomain.domains['TipoDado'][7] = { id: 'POS', name: 'PositiveInteger' };
						dataDomain.domains['TipoDado'][8] = { id: 'STR', name: 'String' };
						dataDomain.domains['TipoDado'][9] = { id: 'TME', name: 'Time' };
						dataDomain.domains['TipoLog'] = [];
						dataDomain.domains['TipoLog'][0] = { id: 2, name: 'Geração de Arquivo' };
						dataDomain.domains['TipoLog'][1] = { id: 3, name: 'Importação de Layout' };
						dataDomain.domains['TipoLog'][2] = { id: 1, name: 'Leitura de Arquivo' };
						dataDomain.domains['FormatoData'] = [];
						dataDomain.domains['FormatoData'][0] = { id: 1, name: 'AAAAMMDD' };
						dataDomain.domains['FormatoData'][1] = { id: 4, name: 'AAMMDD' };
						dataDomain.domains['FormatoData'][2] = { id: 5, name: 'DDMMAA' };
						dataDomain.domains['FormatoData'][3] = { id: 2, name: 'DDMMAAAA' };
						dataDomain.domains['FormatoData'][4] = { id: 6, name: 'MMDDAA' };
						dataDomain.domains['FormatoData'][5] = { id: 3, name: 'MMDDAAAA' };
						dataDomain.domains['TipoLayout'] = [];
						dataDomain.domains['TipoLayout'][0] = { id: 1, name: 'Layout do Sistema' };
						dataDomain.domains['TipoLayout'][1] = { id: 2, name: 'Layout do Usuário' };
						dataDomain.domains['PosicaoDaTransacao'] = [];
						dataDomain.domains['PosicaoDaTransacao'][0] = { id: 5, name: 'Painel Inferior' };
						dataDomain.domains['PosicaoDaTransacao'][1] = { id: 6, name: 'Painel Flutuante' };
						dataDomain.domains['PosicaoDaTransacao'][2] = { id: 2, name: 'Painel à Esquerda' };
						dataDomain.domains['PosicaoDaTransacao'][3] = { id: 1, name: 'Página' };
						dataDomain.domains['PosicaoDaTransacao'][4] = { id: 4, name: 'Painel à Direita' };
						dataDomain.domains['PosicaoDaTransacao'][5] = { id: 3, name: 'Painel Superior' };
						dataDomain.domains['TipoLayoutDependente'] = [];
						dataDomain.domains['TipoLayoutDependente'][0] = { id: 6, name: 'Grade de Dados em Baixo/Formulário em Cima' };
						dataDomain.domains['TipoLayoutDependente'][1] = { id: 2, name: 'Formulário' };
						dataDomain.domains['TipoLayoutDependente'][2] = { id: 7, name: 'Padrão' };
						dataDomain.domains['TipoLayoutDependente'][3] = { id: 1, name: 'Grade de Dados' };
						dataDomain.domains['TipoLayoutDependente'][4] = { id: 3, name: 'Grade de Dados à Esquerda/Formulário à Direita' };
						dataDomain.domains['TipoLayoutDependente'][5] = { id: 5, name: 'Grade de Dados à Direita/Formulário à Esquerda' };
						dataDomain.domains['TipoLayoutDependente'][6] = { id: 4, name: 'Grade de Dados em Cima/Formulário em Baixo' };
						dataDomain.domains['IdAplicativo'] = [];
						dataDomain.domains['IdAplicativo'][0] = { id: 10, name: 'Carga Dados CRM' };
						dataDomain.domains['IdAplicativo'][1] = { id: 14, name: 'Ensemble' };
						dataDomain.domains['IdAplicativo'][2] = { id: 5, name: 'CRM Mobile' };
						dataDomain.domains['IdAplicativo'][3] = { id: 6, name: 'ETL' };
						dataDomain.domains['IdAplicativo'][4] = { id: 8, name: 'Excel' };
						dataDomain.domains['IdAplicativo'][5] = { id: 7, name: 'Mobile' };
						dataDomain.domains['IdAplicativo'][6] = { id: 3, name: 'POS' };
						dataDomain.domains['IdAplicativo'][7] = { id: 13, name: 'Linx Shop' };
						dataDomain.domains['IdAplicativo'][8] = { id: 1, name: 'UX' };
						dataDomain.domains['IdAplicativo'][9] = { id: 9, name: 'Sites Loyalty' };
						dataDomain.domains['IdAplicativo'][10] = { id: 12, name: 'Serviço de Mídias' };
						dataDomain.domains['IdAplicativo'][11] = { id: 11, name: 'MID' };
						dataDomain.domains['IdAplicativo'][12] = { id: 15, name: 'Linx Services' };
						dataDomain.domains['UsoMultimidia'] = [];
						dataDomain.domains['UsoMultimidia'][0] = { id: 1, name: 'Catálogo' };
						dataDomain.domains['UsoMultimidia'][1] = { id: 2, name: 'Detalhe' };
						dataDomain.domains['UsoMultimidia'][2] = { id: 9, name: 'Look View' };
						dataDomain.domains['UsoMultimidia'][3] = { id: 8, name: 'Matriz Mínima' };
						dataDomain.domains['UsoMultimidia'][4] = { id: 3, name: 'Miniatura' };
						dataDomain.domains['UsoMultimidia'][5] = { id: 5, name: 'Zoom Ampliado' };
						dataDomain.domains['UsoMultimidia'][6] = { id: 4, name: 'Zoom de Lente' };
						dataDomain.domains['TipoFiltro'] = [];
						dataDomain.domains['TipoFiltro'][0] = { id: 2, name: 'Filtro BM' };
						dataDomain.domains['TipoFiltro'][1] = { id: 1, name: 'Filtro BV' };
						dataDomain.domains['TipoFiltro'][2] = { id: 4, name: 'Filtro Temporário' };
						dataDomain.domains['TipoFiltro'][3] = { id: 3, name: 'Filtro UI' };
						dataDomain.domains['FilterOperator'] = [];
						dataDomain.domains['FilterOperator'][0] = { id: 'BETWEEN', name: 'Between' };
						dataDomain.domains['FilterOperator'][1] = { id: '>', name: '>' };
						dataDomain.domains['FilterOperator'][2] = { id: '>=', name: '>=' };
						dataDomain.domains['FilterOperator'][3] = { id: 'IN', name: 'In' };
						dataDomain.domains['FilterOperator'][4] = { id: '=', name: '=' };
						dataDomain.domains['FilterOperator'][5] = { id: 'IS NOT NULL', name: 'Not Null' };
						dataDomain.domains['FilterOperator'][6] = { id: 'IS NULL', name: 'Null' };
						dataDomain.domains['FilterOperator'][7] = { id: '<', name: '<' };
						dataDomain.domains['FilterOperator'][8] = { id: '<=', name: '<=' };
						dataDomain.domains['FilterOperator'][9] = { id: 'LIKE', name: 'Like' };
						dataDomain.domains['FilterOperator'][10] = { id: 'NOT BETWEEN', name: 'Not Between' };
						dataDomain.domains['FilterOperator'][11] = { id: '!=', name: '!=' };
						dataDomain.domains['FilterOperator'][12] = { id: 'NOT IN', name: 'Not In' };
						dataDomain.domains['FilterOperator'][13] = { id: 'NOT LIKE', name: 'Not Like' };
						dataDomain.domains['FilterCondition'] = [];
						dataDomain.domains['FilterCondition'][0] = { id: '&&', name: 'And' };
						dataDomain.domains['FilterCondition'][1] = { id: '!', name: 'Not' };
						dataDomain.domains['FilterCondition'][2] = { id: '||', name: 'Or' };
						dataDomain.domains['TipoVerboHttp'] = [];
						dataDomain.domains['TipoVerboHttp'][0] = { id: 6, name: 'Copy' };
						dataDomain.domains['TipoVerboHttp'][1] = { id: 5, name: 'Delete' };
						dataDomain.domains['TipoVerboHttp'][2] = { id: 1, name: 'Get' };
						dataDomain.domains['TipoVerboHttp'][3] = { id: 7, name: 'Head' };
						dataDomain.domains['TipoVerboHttp'][4] = { id: 9, name: 'Link' };
						dataDomain.domains['TipoVerboHttp'][5] = { id: 8, name: 'Options' };
						dataDomain.domains['TipoVerboHttp'][6] = { id: 4, name: 'Patch' };
						dataDomain.domains['TipoVerboHttp'][7] = { id: 2, name: 'Post' };
						dataDomain.domains['TipoVerboHttp'][8] = { id: 11, name: 'Purge' };
						dataDomain.domains['TipoVerboHttp'][9] = { id: 3, name: 'Put' };
						dataDomain.domains['TipoVerboHttp'][10] = { id: 10, name: 'Unlink' };
						dataDomain.domains['TipoProcedimento'] = [];
						dataDomain.domains['TipoProcedimento'][0] = { id: 2, name: 'Função' };
						dataDomain.domains['TipoProcedimento'][1] = { id: 1, name: 'Procedure' };
						dataDomain.domains['OrigemValorParametro'] = [];
						dataDomain.domains['OrigemValorParametro'][0] = { id: 1, name: 'Informação da Origem' };
						dataDomain.domains['OrigemValorParametro'][1] = { id: 2, name: 'Parâmetro do Sistema' };
						dataDomain.domains['TamanhoApresentacao'] = [];
						dataDomain.domains['TamanhoApresentacao'][0] = { id: 2, name: 'Double' };
						dataDomain.domains['TamanhoApresentacao'][1] = { id: 3, name: 'Double-Down' };
						dataDomain.domains['TamanhoApresentacao'][2] = { id: 1, name: 'Normal' };
						dataDomain.domains['CorFundo'] = [];
						dataDomain.domains['CorFundo'][0] = { id: 8, name: 'Fundo Laranja' };
						dataDomain.domains['CorFundo'][1] = { id: 10, name: 'Fundo Roxo' };
						dataDomain.domains['CorFundo'][2] = { id: 7, name: 'Laranja' };
						dataDomain.domains['CorFundo'][3] = { id: 9, name: 'Roxo' };
						dataDomain.domains['LX_PFJ_FISICA_JURIDICA'] = [];
						dataDomain.domains['LX_PFJ_FISICA_JURIDICA'][0] = { id: 1, name: 'Pessoa Física' };
						dataDomain.domains['LX_PFJ_FISICA_JURIDICA'][1] = { id: 2, name: 'Pessoa Jurídica' };
						dataDomain.domains['LxTipoLogradouro'] = [];
						dataDomain.domains['LxTipoLogradouro'][0] = { id: 1, name: 'Aeroporto' };
						dataDomain.domains['LxTipoLogradouro'][1] = { id: 2, name: 'Alameda' };
						dataDomain.domains['LxTipoLogradouro'][2] = { id: 3, name: 'Apartamento' };
						dataDomain.domains['LxTipoLogradouro'][3] = { id: 4, name: 'Avenida' };
						dataDomain.domains['LxTipoLogradouro'][4] = { id: 5, name: 'Beco' };
						dataDomain.domains['LxTipoLogradouro'][5] = { id: 6, name: 'Bloco' };
						dataDomain.domains['LxTipoLogradouro'][6] = { id: 7, name: 'Caminho' };
						dataDomain.domains['LxTipoLogradouro'][7] = { id: 8, name: 'Escadinha' };
						dataDomain.domains['LxTipoLogradouro'][8] = { id: 9, name: 'Estação' };
						dataDomain.domains['LxTipoLogradouro'][9] = { id: 10, name: 'Estrada' };
						dataDomain.domains['LxTipoLogradouro'][10] = { id: 11, name: 'Fazenda' };
						dataDomain.domains['LxTipoLogradouro'][11] = { id: 12, name: 'Fortaleza' };
						dataDomain.domains['LxTipoLogradouro'][12] = { id: 13, name: 'Galeria' };
						dataDomain.domains['LxTipoLogradouro'][13] = { id: 14, name: 'Ladeira' };
						dataDomain.domains['LxTipoLogradouro'][14] = { id: 15, name: 'Largo' };
						dataDomain.domains['LxTipoLogradouro'][15] = { id: 17, name: 'Parque' };
						dataDomain.domains['LxTipoLogradouro'][16] = { id: 16, name: 'Praça' };
						dataDomain.domains['LxTipoLogradouro'][17] = { id: 18, name: 'Praia' };
						dataDomain.domains['LxTipoLogradouro'][18] = { id: 19, name: 'Quadra' };
						dataDomain.domains['LxTipoLogradouro'][19] = { id: 20, name: 'Quilômetro' };
						dataDomain.domains['LxTipoLogradouro'][20] = { id: 21, name: 'Quinta' };
						dataDomain.domains['LxTipoLogradouro'][21] = { id: 22, name: 'Rodovia' };
						dataDomain.domains['LxTipoLogradouro'][22] = { id: 23, name: 'Rua' };
						dataDomain.domains['LxTipoLogradouro'][23] = { id: 24, name: 'Super Quadra' };
						dataDomain.domains['LxTipoLogradouro'][24] = { id: 25, name: 'Travessa' };
						dataDomain.domains['LxTipoLogradouro'][25] = { id: 26, name: 'Viaduto' };
						dataDomain.domains['LxTipoLogradouro'][26] = { id: 27, name: 'Vila' };
						dataDomain.domains['TipoMidia'] = [];
						dataDomain.domains['TipoMidia'][0] = { id: 3, name: 'Documento' };
						dataDomain.domains['TipoMidia'][1] = { id: 1, name: 'Imagem' };
						dataDomain.domains['TipoMidia'][2] = { id: 4, name: 'Outros' };
						dataDomain.domains['TipoMidia'][3] = { id: 2, name: 'Vídeo' };
						dataDomain.domains['ParametroHierarquia'] = [];
						dataDomain.domains['ParametroHierarquia'][0] = { id: 100, name: 'Obrigatório' };
						dataDomain.domains['ParametroHierarquia'][1] = { id: 1, name: 'Variação Nível 1' };
						dataDomain.domains['ParametroHierarquia'][2] = { id: 2, name: 'Variação Nível 2' };
						dataDomain.domains['ParametroHierarquia'][3] = { id: 3, name: 'Variação Nível 3' };
						dataDomain.domains['ParametroHierarquia'][4] = { id: 4, name: 'Variação Nível 4' };
						dataDomain.domains['ParametroHierarquia'][5] = { id: 5, name: 'Variação Nível 5' };
						dataDomain.domains['TipoAutenticador'] = [];
						dataDomain.domains['TipoAutenticador'][0] = { id: 1, name: 'Facebook' };
						dataDomain.domains['TipoAutenticador'][1] = { id: 2, name: 'Google+' };
						dataDomain.domains['TipoAutenticador'][2] = { id: 4, name: 'Linx' };
						dataDomain.domains['TipoAutenticador'][3] = { id: 3, name: 'Microsoft Sign In' };
						dataDomain.domains['TipoServidor'] = [];
						dataDomain.domains['TipoServidor'][0] = { id: 2, name: 'Oracle' };
						dataDomain.domains['TipoServidor'][1] = { id: 3, name: 'SQLite' };
						dataDomain.domains['TipoServidor'][2] = { id: 1, name: 'SQL Server' };
						dataDomain.domains['TipoConteudoObjeto'] = [];
						dataDomain.domains['TipoConteudoObjeto'][0] = { id: 3, name: 'Configuração de Exportação para Excel' };
						dataDomain.domains['TipoConteudoObjeto'][1] = { id: 4, name: 'Configuração de Exportação para Report' };
						dataDomain.domains['TipoConteudoObjeto'][2] = { id: 6, name: 'Gravação de Layout para Grid' };
						dataDomain.domains['TipoConteudoObjeto'][3] = { id: 1, name: 'Layout' };
						dataDomain.domains['TipoConteudoObjeto'][4] = { id: 2, name: 'Mídia' };
						dataDomain.domains['TipoConteudoObjeto'][5] = { id: 5, name: 'Gravação de Layout para Pivot Table' };
					},
			getItems: function (domainName, valuesFilter) {
				var items = dataDomain.domains[domainName];
				if (valuesFilter && valuesFilter != '' && items && items.length > 0) {
					var sourceItems = items;
					items = [];
					for (var i = 0; i < sourceItems.length; i++) {
						if ((',' + valuesFilter + ',').indexOf(',' + sourceItems[i].id.toString() + ',') > -1) {
							items.push(sourceItems[i]);
						}
					}
				}
				return (items && items.length > 0 ? items : []);
			},
			getName: function (domainName, value) {
				var name = '';
				var domainItems = this.getItems(domainName);
				if (domainItems) {
					for (var i in domainItems) {
						if (domainItems[i].id == value) {
							name = domainItems[i].name;
							break;
						}
					}
				}
				return name;
			},
			getId: function (domainName, name) {
				var id = '';
				var domainItems = this.getItems(domainName);
				if (domainItems) {
					for (var i in domainItems) {
						if (domainItems[i].name == name) {
							id = domainItems[i].id;
							break;
						}
					}
				}
				return id;
			}
		};

		dataDomain.registerDomains();
		return dataDomain;
    };
	
	module.exports = function(appModule) {
		appModule.factory(name, [domainsFactory]);
	};

	/* jshint ignore:end */