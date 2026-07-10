							


namespace Linx.Framework.Autorizacao.BM.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Model;
    using System.Data.Entity.Migrations.Sql;
    using System.Diagnostics;
    using System.Linq;
    using Linx.Tools;
	using System.Data.Entity.SqlServer;

    internal sealed class Configuration : DbMigrationsConfiguration<AutorizacaoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;    
			AutomaticMigrationDataLossAllowed = false;
            SetSqlGenerator("System.Data.SqlClient", new SqlMigrator());   
			    
        }
    }

	internal sealed class SqlTableMigrator
    {
        private List<CreateIndexOperation> _indexes = new List<CreateIndexOperation>();
        public List<CreateIndexOperation> Indexes { get { return _indexes; } }

        private Dictionary<string, string> _fks = new Dictionary<string, string>();
        public Dictionary<string, string> Fks { get { return _fks; } }

        private Dictionary<string, string> _defauls = new Dictionary<string, string>();
        public Dictionary<string, string> Defauls { get { return _defauls; } }

		private List<string> _nullables = new List<string>();
        public List<string> Nullables { get { return _nullables; } }

		private Dictionary<string, string> _primaryKeys = new Dictionary<string, string>();
        public Dictionary<string, string> PrimaryKeys { get { return _primaryKeys; } }
		
        private List<string> _views = new List<string>();
        public List<string> Views { get { return _views; } }


        public void AdjustFK(AddForeignKeyOperation addForeignKeyOperation)
        {
            string key = addForeignKeyOperation.PrincipalTable + "." +
                String.Join(".", addForeignKeyOperation.PrincipalColumns.OrderBy(e => e).ToArray()) + "." +                
                addForeignKeyOperation.DependentTable + "." +
                String.Join(".", addForeignKeyOperation.DependentColumns.OrderBy(e => e).ToArray());

            if (Fks.ContainsKey(key))
            {
                string value = Fks[key];
                addForeignKeyOperation.Name = value.Left(",");
                addForeignKeyOperation.CascadeDelete = value.Right(",") == "true";
            }
        }

    }

	internal sealed class SqlMigrator : SqlServerMigrationSqlGenerator
    {
        private SqlTableMigrator _tableMigrator;
        public SqlMigrator()
            : base()
        {
            _tableMigrator = new SqlTableMigrator();
            CreateIndexOperation createIndexOperation;
            //Add Indexes
            createIndexOperation = new CreateIndexOperation() { Table = "LX_TCS.TCS_APLICACAO", IsUnique = true, Name = "XAK1_TCS_APLICACAO" };
            createIndexOperation.Columns.Add("DESCRICAO_APLICACAO");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_TCS.TCS_APLICACAO", IsUnique = true, Name = "XAK2_TCS_APLICACAO" };
            createIndexOperation.Columns.Add("UID_APLICACAO");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_TCS.TCS_CONEXAO_DB", IsUnique = true, Name = "XAK_TCS_CONEXAO_DB" };
            createIndexOperation.Columns.Add("NOME_CONEXAO");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_TCS.TCS_EMPRESA_AUTENTICACAO", IsUnique = true, Name = "XAK1_TCS_EMPRESA_AUTENTICACAO" };
            createIndexOperation.Columns.Add("NOME_EMPRESA");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_TCS.TCS_EMPRESA_AUTENTICACAO", IsUnique = true, Name = "XAK2_TCS_EMPRESA_AUTENTICACAO" };
            createIndexOperation.Columns.Add("UID_EMPRESA");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_TCS.TCS_USUARIO_ACESSO", IsUnique = true, Name = "XAK1_TCS_USUARIO_ACESSO" };
            createIndexOperation.Columns.Add("ID_TCS_AMBIENTE");
            createIndexOperation.Columns.Add("ID_USUARIO");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_TCS.TCS_USUARIO_AUTENTICACAO", IsUnique = true, Name = "XAK1_TCS_USUARIO_AUTENTICACAO" };
            createIndexOperation.Columns.Add("NOME_AUTENTICACAO");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_TCS.TCS_USUARIO_AUTENTICACAO", IsUnique = true, Name = "XAK2_TCS_USUARIO_AUTENTICACAO" };
            createIndexOperation.Columns.Add("UID_USUARIO");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_TCS.TCS_AMBIENTE", IsUnique = true, Name = "XAK1_TCS_AMBIENTE" };
            createIndexOperation.Columns.Add("DESCRICAO_AMBIENTE");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_TCS.TCS_BANCO_SERVIDOR", IsUnique = true, Name = "XAK1_TCS_BANCO_SERVIDOR" };
            createIndexOperation.Columns.Add("DESCRICAO_BANCO_SERVIDOR");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_TCS.TCS_SERVICO", IsUnique = true, Name = "XAK1_TCS_SERVICO" };
            createIndexOperation.Columns.Add("NOME_SERVICO");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_TCS.TCS_APLICATIVO_CONEXAO", IsUnique = true, Name = "XAK1_TCS_APLICATIVO_CONEXAO" };
            createIndexOperation.Columns.Add("ID_CONEXAO_DB");
            createIndexOperation.Columns.Add("ID_TCS_APLICATIVO");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_TCS.TCS_AMBIENTE_SERVICO_EXCECAO", IsUnique = true, Name = "XAK1_TCS_AMBIENTE_SERVICO_E" };
            createIndexOperation.Columns.Add("ID_TCS_AMBIENTE");
            createIndexOperation.Columns.Add("ID_TCS_SERVICO");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_TCS.TCS_APLICATIVO", IsUnique = true, Name = "XAK1_TCS_APLICATIVO" };
            createIndexOperation.Columns.Add("DESCRICAO_APLICATIVO");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_TCS.TCS_AMBIENTE_CONEXAO", IsUnique = true, Name = "XAK1_TCS_AMBIENTE_CONEXAO" };
            createIndexOperation.Columns.Add("ID_TCS_AMBIENTE");
            createIndexOperation.Columns.Add("ID_TCS_APLICATIVO_CONEXAO");
            createIndexOperation.Columns.Add("ID_TCS_BANCO_SERVIDOR");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_TCS.TCS_MENSAGEM", IsUnique = false, Name = "XIE1_TCS_MENSAGEM" };
            createIndexOperation.Columns.Add("ENVIO");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_TCS.TCS_MENSAGEM_LOG", IsUnique = false, Name = "XIE1_TCS_MENSAGEM_LOG" };
            createIndexOperation.Columns.Add("ENTREGUE");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_TCS.TCS_MENSAGEM_LOG", IsUnique = false, Name = "XIE2_TCS_MENSAGEM_LOG" };
            createIndexOperation.Columns.Add("DISPENSADA");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_TCS.TCS_CARGA_ORIGEM", IsUnique = true, Name = "XAK1_TCS_CARGA_ORIGEM" };
            createIndexOperation.Columns.Add("SERVIDOR_ORIGEM");
            createIndexOperation.Columns.Add("BANCO_ORIGEM");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_TCS.TCS_PROCEDIMENTO_ORQUESTRADOR", IsUnique = true, Name = "XAK1_TCS_PROC_ORQUESTRADOR" };
            createIndexOperation.Columns.Add("ID_PROCESSO_ORQUESTRADOR");
            createIndexOperation.Columns.Add("ORDEM_EXECUCAO");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_TCS.TCS_PARAMETRO_VALIDO_AUT", IsUnique = true, Name = "XAK1_TCS_PARAMETRO_VALIDO_AUT" };
            createIndexOperation.Columns.Add("ID_PARAMETRO");
            createIndexOperation.Columns.Add("VALOR_PARAMETRO");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_TCS.TCS_PARAMETRO_AUTORIZACAO", IsUnique = true, Name = "XAK1_TCS_PARAMETRO" };
            createIndexOperation.Columns.Add("TITULO_PARAMETRO");
            createIndexOperation.Columns.Add("ID_TCS_APLICATIVO");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_TCS.TCS_TABELA_AUTORIZACAO", IsUnique = true, Name = "XAK1_TCS_TABELA" };
            createIndexOperation.Columns.Add("DESC_TABELA");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_TCS.TCS_TABELA_AUTORIZACAO", IsUnique = true, Name = "XAK2_TCS_TABELA" };
            createIndexOperation.Columns.Add("NOME_TABELA");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_TCS.TCS_PARAMETRO_GRUPO_AUT", IsUnique = true, Name = "XAK1_TCS_PARAMETRO_GRUPO_AUT" };
            createIndexOperation.Columns.Add("DESC_GRUPO_PARAMETRO");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_TCS.TCS_PARAMETRO_TRANSACAO", IsUnique = true, Name = "XAK1_TCS_PARAM_TRANSACAO" };
            createIndexOperation.Columns.Add("ID_PARAMETRO");
            createIndexOperation.Columns.Add("ID_TRANSACAO");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_TCS.TCS_TRANSACAO_AUTORIZACAO", IsUnique = true, Name = "XAK1_TCS_TRANSACAO_AUTORIZACAO" };
            createIndexOperation.Columns.Add("COD_TRANSACAO");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_TCS.TCS_TRANSACAO_AUTORIZACAO", IsUnique = true, Name = "XAK2_TCS_TRANSACAO_AUTORIZACAO" };
            createIndexOperation.Columns.Add("CLASSE_NOME");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_TCS.TCS_OBJETO_AUTORIZACAO", IsUnique = true, Name = "XAK1_TCS_OBJETO_AUTORIZACAO" };
            createIndexOperation.Columns.Add("DESC_OBJETO");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_TCS.TCS_OBJETO_AUTORIZACAO", IsUnique = true, Name = "XAK2_TCS_OBJETO_AUTORIZACAO" };
            createIndexOperation.Columns.Add("CLASSE_NOME");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_TCS.TCS_TRANSACAO_MENU_AUTORIZACAO", IsUnique = true, Name = "XAK1_TCS_TRANSACAO_MENU_AUT" };
            createIndexOperation.Columns.Add("ID_TRANSACAO");
            createIndexOperation.Columns.Add("ID_MODULO_MENU");
            _tableMigrator.Indexes.Add(createIndexOperation);
            //Add Primary Keys
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_IDENTIDADE_EXTERNA"] = "XPK_TCS_IDENTIDADE_EXTERNA";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_APLICACAO"] = "XPK_TCS_APLICACAO";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_CONEXAO_DB"] = "XPK_TCS_CONEXAO_DB";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_EMPRESA_AUTENTICACAO"] = "XPK_TCS_EMPRESA_AUTENTICACAO";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_USUARIO_ACESSO"] = "XPK_TCS_USUARIO_ACESSO";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_USUARIO_AUTENTICACAO"] = "XPK_TCS_USUARIO_AUTENTICACAO";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_BANCO_SERVIDOR"] = "XPK_TCS_BANCO_SERVIDOR";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_APLICACAO_VERSAO_HIST"] = "XPK_TCS_APLICACAO_VERSAO_HIST";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_SERVICO"] = "XPK_TCS_SERVICO";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_APLICATIVO_CONEXAO"] = "XPK_TCS_APLICATIVO_CONEXAO";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_AMBIENTE_SERVICO_EXCECAO"] = "XPK_TCS_AMBIENTE_SERVICO_EX";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_APLICATIVO"] = "XPK_TCS_APLICATIVO";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_SUPORTE_ACESSO_LOG"] = "XPK_TCS_SUPORTE_ACESSO_LOG";
            _tableMigrator.PrimaryKeys["LX_DOC.DOC_MULTIMIDIA_TABELA_AUT"] = "XPK_DOC_MULTIMIDIA_TABELA_A";
            _tableMigrator.PrimaryKeys["LX_DOC.DOC_MULTIMIDIA_AUTORIZACAO"] = "XPK_DOC_MULTIMIDIA_AUT";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_CARGA_ORIGEM"] = "XPK_TCS_CARGA_ORIGEM";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_EMPRESA_PROC_ORQUESTRADOR"] = "XPK_TCS_EMPRESA_PROC_ORQUESTRADOR";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_PROCEDIMENTO_PARAMETRO"] = "XPK_TCS_PROCEDIMENTO_PARAMETRO";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_PROCEDIMENTO_ORQUESTRADOR"] = "XPK_TCS_PROCEDIMENTO_ORQUESTRADOR";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_SERVICO_INTEGRACAO"] = "XPK_TCS_SERVICO_INTEGRACAO";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_PROCESSO_ORQUESTRADOR"] = "XPK_TCS_PROCESSO_ORQUESTRADOR";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_PARAMETRO_VALIDO_AUT"] = "XPK_TCS_PARAMETRO_VALIDO_AUT";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_PARAMETRO_AUTORIZACAO"] = "XPK_TCS_PARAMETRO_AUTORIZACAO";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_PARAMETRO_TABELA_SEL_AUT"] = "XPK_TCS_PARAM_TABELA_SEL_AUT";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_TABELA_AUTORIZACAO"] = "XPKTCS_TABELA_AUTORIZACAO";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_PARAMETRO_GRUPO_AUT"] = "XPK_TCS_PARAMETRO_GRUPO_AUT";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_TRANSACAO_AUTORIZACAO"] = "XPK_TCS_TRANSACAO_AUTORIZACAO";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_LAYOUT_AUTORIZACAO"] = "XPKTCS_LAYOUT_AUTORIZACAO";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_MODULO_AUTORIZACAO"] = "XPKTCS_MODULO_AUTORIZACAO";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_MODULO_MENU_AUTORIZACAO"] = "XPK_TCS_MODULO_MENU_AUTORIZACAO";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_OBJETO_AUTORIZACAO"] = "XPK_TCS_OBJETO_AUTORIZACAO";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_OBJETO_CONTEUDO_AUT"] = "XPKTCS_OBJETO_CONTEUDO_AUT";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_TRANSACAO_DEPENDENTE_AUT"] = "XPK_TCS_TRANSACAO_DEPENDENTE_AUT";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_TRANSACAO_MENU_AUTORIZACAO"] = "XPKTCS_TRANSACAO_MENU_AUTORIZACAO";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_EMPRESA_MODULO"] = "XPK_TCS_EMPRESA_MODULO";
            //Add Defaults
            _tableMigrator.Defauls.Add("LX_TCS.TCS_APLICACAO.EM_DESENVOLVIMENTO", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_USUARIO_ACESSO.INDICA_ADMINISTRADOR", "0");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_USUARIO_ACESSO.INDICA_MULTI_GPECON", "0");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_USUARIO_ACESSO.INDICA_ACESSO_PADRAO", "0");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_USUARIO_AUTENTICACAO.INATIVO", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_USUARIO_AUTENTICACAO.DATA_CADASTRO", "(getdate())");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_USUARIO_AUTENTICACAO.DATA_ALTERACAO", "(getdate())");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_USUARIO_AUTENTICACAO.AUTENTICACAO_WINDOWS", "0");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_USUARIO_AUTENTICACAO.INDICA_ACESSO_SUPORTE", "0");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_SUPORTE_ACESSO_LOG.ACESSO_EXPIRADO", "((0))");
            _tableMigrator.Defauls.Add("LX_DOC.DOC_MULTIMIDIA_TABELA_AUT.ORDEM_APRESENTACAO", "((0))");
            _tableMigrator.Defauls.Add("LX_DOC.DOC_MULTIMIDIA_AUTORIZACAO.DATA_CRIACAO", "GETDATE()");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_CARGA_ORIGEM.RETORNA_ATU_CLIENTE_VAREJO", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_PROCESSO_ORQUESTRADOR.INTERVALO_EXECUCAO", "((1800))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_PARAMETRO_AUTORIZACAO.INDICA_ENVIA_PDV", "0");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_PARAMETRO_AUTORIZACAO.PERMITE_VARIACAO_POR_ENTIDADE", "0");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_PARAMETRO_AUTORIZACAO.NIVEL_ACESSO", "0");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_PARAMETRO_AUTORIZACAO.NIVEL_ACESSO_EDICAO", "0");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_TABELA_AUTORIZACAO.TABELA_AUTORIZACAO", "0");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_TRANSACAO_AUTORIZACAO.INATIVO", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_LAYOUT_AUTORIZACAO.LAYOUT_PADRAO", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_LAYOUT_AUTORIZACAO.INATIVO", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_LAYOUT_AUTORIZACAO.POSSUI_FILTRO", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_MODULO_AUTORIZACAO.ORDEM_NAVEGACAO", "((1))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_MODULO_AUTORIZACAO.INATIVO", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_MODULO_MENU_AUTORIZACAO.ORDEM_NAVEGACAO", "((1))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_TRANSACAO_DEPENDENTE_AUT.POSSUI_TOOLBAR", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_TRANSACAO_DEPENDENTE_AUT.POSSUI_VISAO_TABULAR", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_TRANSACAO_DEPENDENTE_AUT.COMPARTILHA_BO_PRINCIPAL", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_TRANSACAO_DEPENDENTE_AUT.VISIVEL", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_TRANSACAO_DEPENDENTE_AUT.USA_FILTROS_DO_BO_PRINCIPAL", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_TRANSACAO_DEPENDENTE_AUT.EXECUTA_PESQUISA", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_TRANSACAO_DEPENDENTE_AUT.MOSTRA_BOTAO_LIMPA", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_TRANSACAO_DEPENDENTE_AUT.MOSTRA_BOTAO_PESQUISA", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_TRANSACAO_DEPENDENTE_AUT.MOSTRA_BOTAO_ADICAO", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_TRANSACAO_DEPENDENTE_AUT.MOSTRA_BOTAO_EDICAO", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_TRANSACAO_DEPENDENTE_AUT.MOSTRA_BOTAO_EXCLUSAO", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_TRANSACAO_DEPENDENTE_AUT.MOSTRA_BOTAO_PESQUISA_ESP", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_TRANSACAO_DEPENDENTE_AUT.MOSTRA_BOTAO_LAYOUT", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_TRANSACAO_DEPENDENTE_AUT.MOSTRA_BOTAO_NAVEGACAO", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_TRANSACAO_DEPENDENTE_AUT.MOSTRA_BOTAO_IMPRESSAO", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_TRANSACAO_MENU_AUTORIZACAO.ORDEM_NAVEGACAO", "((1))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_TRANSACAO_MENU_AUTORIZACAO.INATIVO", "((0))");
            //Add Nullables
            _tableMigrator.Nullables.Add("LX_TCS.TCS_USUARIO_ACESSO.ID_TCS_AMBIENTE_RELACIONADO");
            _tableMigrator.Nullables.Add("LX_TCS.TCS_SUPORTE_ACESSO_LOG.ID_USUARIO_SUPORTE");
            _tableMigrator.Nullables.Add("LX_DOC.DOC_MULTIMIDIA_AUTORIZACAO.UID_DOC_BASE_GERADOR");
            _tableMigrator.Nullables.Add("LX_TCS.TCS_SERVICO_INTEGRACAO.ID_PROCESSO_ORQUESTRADOR");
            _tableMigrator.Nullables.Add("LX_TCS.TCS_PARAMETRO_AUTORIZACAO.UID_TABELA_VALIDA");
            _tableMigrator.Nullables.Add("LX_TCS.TCS_TABELA_AUTORIZACAO.ID_TRANSACAO");
            _tableMigrator.Nullables.Add("LX_TCS.TCS_TRANSACAO_AUTORIZACAO.ID_MODULO_BASE");
            _tableMigrator.Nullables.Add("LX_TCS.TCS_MODULO_MENU_AUTORIZACAO.ID_MODULO_MENU_SUPERIOR");
            _tableMigrator.Nullables.Add("LX_TCS.TCS_LOG_ERROS.ID_APLICACAO");
            _tableMigrator.Nullables.Add("LX_TCS.TCS_LOG_ERROS.ID_TCS_AMBIENTE");
            _tableMigrator.Nullables.Add("LX_TCS.TCS_LOG_ERROS.ID_USUARIO");
            _tableMigrator.Nullables.Add("LX_TCS.TCS_LOG_ERROS.ID_LINX_EMPRESA");
            _tableMigrator.Nullables.Add("LX_TCS.TCS_LOG_ERROS.ID_LINX_GPECON");
            //Add Foreign Keys
            _tableMigrator.Fks.Add("LX_TCS.TCS_USUARIO_AUTENTICACAO.ID_USUARIO.LX_TCS.TCS_IDENTIDADE_EXTERNA.ID_USUARIO", "FK_TCS_IDENTIDADE_EXT_853598E7,true");
            _tableMigrator.Fks.Add("LX_TCS.TCS_APLICATIVO.ID_TCS_APLICATIVO.LX_TCS.TCS_APLICACAO.ID_TCS_APLICATIVO", "FK_TCS_APLICACAO_58B6DB76,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_USUARIO_AUTENTICACAO.ID_USUARIO.LX_TCS.TCS_USUARIO_ACESSO.ID_USUARIO", "FK_TCS_USUARIO_ACESSO_78FD2D23,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_AMBIENTE.ID_TCS_AMBIENTE.LX_TCS.TCS_USUARIO_ACESSO.ID_TCS_AMBIENTE", "FK_TCS_USUARIO_ACESSO_386F41DD,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_AMBIENTE.ID_TCS_AMBIENTE.LX_TCS.TCS_USUARIO_ACESSO.ID_TCS_AMBIENTE_RELACIONADO", "FK_TCS_USUARIO_ACESSO_5CCDA447,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_EMPRESA_AUTENTICACAO.ID_LINX.LX_TCS.TCS_USUARIO_AUTENTICACAO.ID_LINX_GPECON", "FK_TCS_USUARIO_AUTENT_28E30C9D,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_APLICACAO.ID_APLICACAO.LX_TCS.TCS_AMBIENTE.ID_APLICACAO", "FK_TCS_AMBIENTE_A4C45954,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_EMPRESA_AUTENTICACAO.ID_LINX.LX_TCS.TCS_AMBIENTE.ID_LINX", "FK_TCS_AMBIENTE_9A151A87,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_EMPRESA_AUTENTICACAO.ID_LINX.LX_TCS.TCS_EMPRESA_GPECON.ID_LINX", "FK_TCS_EMPRESA_GPECON_1456ED31,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_EMPRESA_AUTENTICACAO.ID_LINX.LX_TCS.TCS_EMPRESA_GPECON.ID_LINX_GPECON", "FK_TCS_EMPRESA_GPECON_B8680E15,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_APLICACAO.ID_APLICACAO.LX_TCS.TCS_APLICACAO_VERSAO_HIST.ID_APLICACAO", "FK_TCS_APLICACAO_VERS_E38F40A6,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_CONEXAO_DB.ID_CONEXAO_DB.LX_TCS.TCS_APLICATIVO_CONEXAO.ID_CONEXAO_DB", "FK_TCS_APLICATIVO_CON_C8402E86,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_APLICATIVO.ID_TCS_APLICATIVO.LX_TCS.TCS_APLICATIVO_CONEXAO.ID_TCS_APLICATIVO", "FK_TCS_APLICATIVO_CON_BE885031,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_AMBIENTE.ID_TCS_AMBIENTE.LX_TCS.TCS_AMBIENTE_SERVICO_EXCECAO.ID_TCS_AMBIENTE", "FK_TCS_AMBIENTE_SERVI_D6ADC656,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_SERVICO.ID_TCS_SERVICO.LX_TCS.TCS_AMBIENTE_SERVICO_EXCECAO.ID_TCS_SERVICO", "FK_TCS_AMBIENTE_SERVI_21E0D9DB,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_AMBIENTE.ID_TCS_AMBIENTE.LX_TCS.TCS_AMBIENTE_CONEXAO.ID_TCS_AMBIENTE", "FK_TCS_AMBIENTE_CONEX_8DA2BE6F,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_BANCO_SERVIDOR.ID_TCS_BANCO_SERVIDOR.LX_TCS.TCS_AMBIENTE_CONEXAO.ID_TCS_BANCO_SERVIDOR", "FK_TCS_AMBIENTE_CONEX_9F6CA3CF,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_APLICATIVO_CONEXAO.ID_TCS_APLICATIVO_CONEXAO.LX_TCS.TCS_AMBIENTE_CONEXAO.ID_TCS_APLICATIVO_CONEXAO", "FK_TCS_AMBIENTE_CONEX_072F430A,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_USUARIO_ACESSO.ID_TCS_USUARIO_ACESSO.LX_TCS.TCS_SUPORTE_ACESSO_LOG.ID_TCS_USUARIO_ACESSO", "FK_TCS_SUPORTE_ACESSO_FE320831,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_USUARIO_AUTENTICACAO.ID_USUARIO.LX_TCS.TCS_SUPORTE_ACESSO_LOG.ID_USUARIO_SUPORTE", "FK_TCS_SUPORTE_ACESSO_AB55EA03,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_USUARIO_AUTENTICACAO.ID_USUARIO.LX_TCS.TCS_SUPORTE_ACESSO_LOG.ID_USUARIO_ACESSO", "FK_TCS_SUPORTE_ACESSO_7C378AC8,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_USUARIO_AUTENTICACAO.ID_USUARIO.LX_TCS.TCS_MENSAGEM.ID_USUARIO_EMISSOR", "FK_TCS_MENSAGEM_039F1A67,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_EMPRESA_AUTENTICACAO.ID_LINX.LX_TCS.TCS_MENSAGEM.ID_LINX", "FK_TCS_MENSAGEM_4C367BC2,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_USUARIO_AUTENTICACAO.ID_USUARIO.LX_TCS.TCS_MENSAGEM_LOG.ID_USUARIO", "FK_TCS_MENSAGEM_LOG_C60CA243,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_MENSAGEM.ID_TCS_MENSAGEM.LX_TCS.TCS_MENSAGEM_LOG.ID_TCS_MENSAGEM", "FK_TCS_MENSAGEM_LOG_649DD859,false");
            _tableMigrator.Fks.Add("LX_DOC.DOC_MULTIMIDIA_AUTORIZACAO.UID_DOCUMENTO.LX_DOC.DOC_MULTIMIDIA_TABELA_AUT.UID_DOCUMENTO", "XFK_DOC_MULTIMIDIA_TABELA_1,true");
            _tableMigrator.Fks.Add("LX_DOC.DOC_MULTIMIDIA_AUTORIZACAO.UID_DOCUMENTO.LX_DOC.DOC_MULTIMIDIA_AUTORIZACAO.UID_DOC_BASE_GERADOR", "XFK_DOC_MULTIMIDIA_1,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_EMPRESA_AUTENTICACAO.ID_LINX.LX_TCS.TCS_CARGA_ORIGEM.ID_LINX", "FK_TCS_CARGA_ORIGEM_70CCE0C1,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_USUARIO_AUTENTICACAO.ID_USUARIO.LX_TCS.TCS_CARGA_ORIGEM.ID_USUARIO", "FK_TCS_CARGA_ORIGEM_B6F4509C,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_EMPRESA_AUTENTICACAO.ID_LINX.LX_TCS.TCS_EMPRESA_PROC_ORQUESTRADOR.ID_LINX", "FK_TCS_EMPRESA_PROC_O_A0037AF3,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_PROCESSO_ORQUESTRADOR.ID_PROCESSO_ORQUESTRADOR.LX_TCS.TCS_EMPRESA_PROC_ORQUESTRADOR.ID_PROCESSO_ORQUESTRADOR", "FK_TCS_EMPRESA_PROC_O_60C72666,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_PROCEDIMENTO_ORQUESTRADOR.ID_PROCEDIMENTO_ORQUESTRADOR.LX_TCS.TCS_PROCEDIMENTO_PARAMETRO.ID_PROC_ORQUESTRADOR", "FK_TCS_PROCEDIMENTO_P_F37AA845,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_PROCESSO_ORQUESTRADOR.ID_PROCESSO_ORQUESTRADOR.LX_TCS.TCS_PROCEDIMENTO_ORQUESTRADOR.ID_PROCESSO_ORQUESTRADOR", "FK_TCS_PROCEDIMENTO_O_4B840B69,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_EMPRESA_AUTENTICACAO.ID_LINX.LX_TCS.TCS_SERVICO_INTEGRACAO.ID_LINX", "FK_TCS_SERVICO_INTEGR_EF5B6405,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_USUARIO_AUTENTICACAO.ID_USUARIO.LX_TCS.TCS_SERVICO_INTEGRACAO.ID_USUARIO", "FK_TCS_SERVICO_INTEGR_6462F8F1,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_PROCESSO_ORQUESTRADOR.ID_PROCESSO_ORQUESTRADOR.LX_TCS.TCS_SERVICO_INTEGRACAO.ID_PROCESSO_ORQUESTRADOR", "FK_TCS_SERVICO_INTEGR_58CBBA91,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_PARAMETRO_AUTORIZACAO.ID_PARAMETRO.LX_TCS.TCS_PARAMETRO_VALIDO_AUT.ID_PARAMETRO", "FK_TCS_PARAMETRO_VALI_6A830AAD,true");
            _tableMigrator.Fks.Add("LX_TCS.TCS_TABELA_AUTORIZACAO.UID_TABELA.LX_TCS.TCS_PARAMETRO_AUTORIZACAO.UID_TABELA_VALIDA", "FK_TCS_PARAMETRO_AUTO_D69B64C5,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_PARAMETRO_GRUPO_AUT.ID_GRUPO_PARAMETRO.LX_TCS.TCS_PARAMETRO_AUTORIZACAO.ID_GRUPO_PARAMETRO", "FK_TCS_PARAMETRO_AUTO_C63DC279,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_APLICATIVO.ID_TCS_APLICATIVO.LX_TCS.TCS_PARAMETRO_AUTORIZACAO.ID_TCS_APLICATIVO", "FK_TCS_PARAMETRO_AUTO_0628A280,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_PARAMETRO_AUTORIZACAO.ID_PARAMETRO.LX_TCS.TCS_PARAMETRO_TABELA_SEL_AUT.ID_PARAMETRO", "FK_TCS_PARAMETRO_TABE_2C25B8D1,true");
            _tableMigrator.Fks.Add("LX_TCS.TCS_TABELA_AUTORIZACAO.UID_TABELA.LX_TCS.TCS_PARAMETRO_TABELA_SEL_AUT.UID_TABELA", "FK_TCS_PARAMETRO_TABE_539C310F,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_TRANSACAO_AUTORIZACAO.ID_TRANSACAO.LX_TCS.TCS_TABELA_AUTORIZACAO.ID_TRANSACAO", "FK_TCS_TABELA_AUTORIZ_39E6C3DD,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_PARAMETRO_AUTORIZACAO.ID_PARAMETRO.LX_TCS.TCS_PARAMETRO_TRANSACAO.ID_PARAMETRO", "FK_TCS_PARAMETRO_TRAN_191DE98F,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_TRANSACAO_AUTORIZACAO.ID_TRANSACAO.LX_TCS.TCS_PARAMETRO_TRANSACAO.ID_TRANSACAO", "FK_TCS_PARAMETRO_TRAN_007CAD9F,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_OBJETO_CONTEUDO_AUT.ID_OBJETO_CONTEUDO.LX_TCS.TCS_LAYOUT_AUTORIZACAO.ID_OBJETO_CONTEUDO", "FK_TCS_LAYOUT_AUTORIZ_87A35E3E,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_APLICATIVO.ID_TCS_APLICATIVO.LX_TCS.TCS_MODULO_AUTORIZACAO.ID_TCS_APLICATIVO", "FK_TCS_MODULO_AUTORIZ_E42BCD66,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_MODULO_AUTORIZACAO.ID_MODULO.LX_TCS.TCS_MODULO_MENU_AUTORIZACAO.ID_MODULO", "FK_TCS_MODULO_MENU_AU_B68ECC25,true");
            _tableMigrator.Fks.Add("LX_TCS.TCS_MODULO_MENU_AUTORIZACAO.ID_MODULO_MENU.LX_TCS.TCS_MODULO_MENU_AUTORIZACAO.ID_MODULO_MENU_SUPERIOR", "FK_TCS_MODULO_MENU_AU_BC3B10B8,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_OBJETO_AUTORIZACAO.ID_OBJETO.LX_TCS.TCS_OBJETO_CONTEUDO_AUT.ID_OBJETO", "FK_TCS_OBJETO_CONTEUD_0589E99A,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_MODULO_AUTORIZACAO.ID_MODULO.LX_TCS.TCS_TRANSACAO_AUTORIZACAO.ID_MODULO_BASE", "FK_TCS_TRANSACAO_AUTO_C207AB46,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_OBJETO_AUTORIZACAO.ID_OBJETO.LX_TCS.TCS_TRANSACAO_AUTORIZACAO.ID_OBJETO", "FK_TCS_TRANSACAO_AUTO_823F905D,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_TRANSACAO_AUTORIZACAO.ID_TRANSACAO.LX_TCS.TCS_TRANSACAO_DEPENDENTE_AUT.ID_TRANSACAO", "FK_TCS_TRANSACAO_DEPE_0B34AECA,true");
            _tableMigrator.Fks.Add("LX_TCS.TCS_TRANSACAO_AUTORIZACAO.ID_TRANSACAO.LX_TCS.TCS_TRANSACAO_DEPENDENTE_AUT.ID_TRANSACAO_RELACIONADA", "FK_TCS_TRANSACAO_DEPE_CB696855,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_MODULO_MENU_AUTORIZACAO.ID_MODULO_MENU.LX_TCS.TCS_TRANSACAO_MENU_AUTORIZACAO.ID_MODULO_MENU", "FK_TCS_TRANSACAO_MENU_8E6EAB8A,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_TRANSACAO_AUTORIZACAO.ID_TRANSACAO.LX_TCS.TCS_TRANSACAO_MENU_AUTORIZACAO.ID_TRANSACAO", "FK_TCS_TRANSACAO_MENU_D0529F4B,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_MODULO_AUTORIZACAO.ID_MODULO.LX_TCS.TCS_EMPRESA_MODULO.ID_MODULO", "FK_TCS_EMPRESA_MODULO_1C349F54,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_EMPRESA_AUTENTICACAO.ID_LINX.LX_TCS.TCS_EMPRESA_MODULO.ID_LINX", "FK_TCS_EMPRESA_MODULO_4FD22BCF,true");
            _tableMigrator.Fks.Add("LX_TCS.TCS_AMBIENTE.ID_TCS_AMBIENTE.LX_TCS.TCS_LOG_ERROS.ID_TCS_AMBIENTE", "FK_TCS_LOG_ERROS_94AC6EA5,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_APLICACAO.ID_APLICACAO.LX_TCS.TCS_LOG_ERROS.ID_APLICACAO", "FK_TCS_LOG_ERROS_5014B22F,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_EMPRESA_AUTENTICACAO.ID_LINX.LX_TCS.TCS_LOG_ERROS.ID_LINX_EMPRESA", "FK_TCS_LOG_ERROS_68A1DCB3,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_EMPRESA_AUTENTICACAO.ID_LINX.LX_TCS.TCS_LOG_ERROS.ID_LINX_GPECON", "FK_TCS_LOG_ERROS_6FCBFCE2,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_USUARIO_AUTENTICACAO.ID_USUARIO.LX_TCS.TCS_LOG_ERROS.ID_USUARIO", "FK_TCS_LOG_ERROS_A02BDD08,false");

        }
    
            
		protected override void Generate(CreateIndexOperation createIndexOperation)
        {
            using (var writer = Writer())
            {
                writer.Write("CREATE ");

                if (createIndexOperation.IsUnique)
                {
                    writer.Write("UNIQUE ");
                }

                object isClustered;
                createIndexOperation.AnonymousArguments.TryGetValue("IsClustered", out isClustered);

                if (isClustered is bool && (bool)isClustered)
                {
                    writer.Write("CLUSTERED ");
                }
                else
                    writer.Write("NONCLUSTERED ");

                writer.Write("INDEX ");
                writer.Write(Quote(createIndexOperation.Name));
                writer.Write(" ON ");
                writer.Write(Name(createIndexOperation.Table));
                writer.Write("(");
                
                writer.Write(string.Join(", ", createIndexOperation.Columns.Select(c => (c.ToUpper().Right(5) == " DESC" ? Quote(c.Left(c.Length - 5)) + " DESC" : Quote(c)))));

                writer.Write(")");
                Statement(writer);
            }
        }

        public override IEnumerable<MigrationStatement> Generate(IEnumerable<MigrationOperation> migrationOperations, string providerManifestToken)
        {
            var operations = new List<MigrationOperation>();

			//Copy all operations           
            foreach (var db in migrationOperations)
            {
                if (db is UpdateDatabaseOperation)
                {
                    foreach (var mg in ((UpdateDatabaseOperation)db).Migrations)
                    {                        
                        operations.AddRange(mg.Operations);
                    }
                }
            }



			//Adjusting elements
            foreach (var op in operations.ToArray())
            {
                if (op is AddForeignKeyOperation)
                {
                    AdjustForeignKey(((AddForeignKeyOperation)op));                    
                }                
                if (op is CreateTableOperation)
                {
						AdjustTable(((CreateTableOperation)op), operations);                    
                }
            }

            var statements = base.Generate(operations, providerManifestToken).ToList();


            if (statements.Count > 0)
            {
				string headerInfo = "-- Script SQLServer was generated by Linx Systems\r\n" +
									"-- Company home page: http://www.linx.com.br\r\n" +
									String.Format("-- Script date {0:d/M/yyyy HH:mm:ss}", DateTime.Now) + "\r\n";
				var first = statements.First();
				first.Sql = headerInfo + first.Sql;
                var migHistory = statements.FirstOrDefault(e => e.Sql.Contains("__MigrationHistory"));
                if (migHistory != null)
                    statements.Remove(migHistory);
                
				//Adjust NONCLUSTERED PrimaryKeys
                foreach (var sql in statements.Where(e => e.Sql.Contains("__NC__] PRIMARY KEY ")))
                {
                    sql.Sql = sql.Sql.Replace("__NC__] PRIMARY KEY ", "] PRIMARY KEY NONCLUSTERED ");
                }
            }
           
            return statements;
        }

        private void AdjustTable(CreateTableOperation createTableOperation, List<MigrationOperation> migrationOperations)
        {
            if (_tableMigrator.Views.Contains(createTableOperation.Name))
            {
                migrationOperations.Remove(createTableOperation);
                migrationOperations.Add(new SqlOperation("/*SQL View " + createTableOperation.Name + " was ignored.*/"));
            }
            else
            {
                if (createTableOperation.Name != "dbo.__MigrationHistory")
                {
					//Adjust Primary Key	
					if (_tableMigrator.PrimaryKeys.ContainsKey(createTableOperation.Name))				
						createTableOperation.PrimaryKey.Name = _tableMigrator.PrimaryKeys[createTableOperation.Name];

                    //Defaults
                    foreach (var column in createTableOperation.Columns)
                    {
                        if (_tableMigrator.Defauls.ContainsKey(createTableOperation.Name + "." + column.Name))
                            column.DefaultValueSql = _tableMigrator.Defauls[createTableOperation.Name + "." + column.Name];

						if (column.IsNullable != null && !column.IsNullable.Value && _tableMigrator.Nullables.Contains(createTableOperation.Name + "." + column.Name))
                            column.IsNullable = true;
                    }

                    //Add Indexes
                    foreach (var index in _tableMigrator.Indexes.Where(e => e.Table == createTableOperation.Name))
                    {
                        migrationOperations.Add(index);
                    }
                }
                else
                {
                    migrationOperations.Remove(createTableOperation);
                }
            }
        }
               

        private void AdjustForeignKey(AddForeignKeyOperation addForeignKeyOperation)
        {
            _tableMigrator.AdjustFK(addForeignKeyOperation);
        }
        
    }

}
