									


namespace Linx.Framework.ControleSistema.BM.Migrations
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

    internal sealed class Configuration : DbMigrationsConfiguration<ControleSistemaContext>
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
            createIndexOperation = new CreateIndexOperation() { Table = "LX_TCS.TCS_ARQUIVO_GRUPO", IsUnique = true, Name = "XAK1_TCS_ARQUIVO_GRUPO" };
            createIndexOperation.Columns.Add("COD_ARQUIVO_GRUPO");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_TCS.TCS_ARQUIVO", IsUnique = true, Name = "XAK1_TCS_ARQUIVO" };
            createIndexOperation.Columns.Add("COD_ARQUIVO");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_PRD.PRD_SKU_PRODUTO", IsUnique = true, Name = "XAK1_PRD_SKU_PRODUTO" };
            createIndexOperation.Columns.Add("ID_LINX");
            createIndexOperation.Columns.Add("COD_SKU");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_PRD.PRD_SKU_PRODUTO", IsUnique = false, Name = "XIE_1_PRD_SKU_PRODUTO" };
            createIndexOperation.Columns.Add("ID_LINX");
            createIndexOperation.Columns.Add("ID_ARTIGO");
            createIndexOperation.Columns.Add("ID_SKU");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_PRD.PRD_ARTIGO_VARIANTE", IsUnique = true, Name = "XAK1_PRD_ARTIGO_VARIANTE" };
            createIndexOperation.Columns.Add("ID_ARTIGO");
            createIndexOperation.Columns.Add("COD_PRD_VARIANTE");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_PRD.PRD_ARTIGO_VARIANTE_VALOR", IsUnique = true, Name = "XAK1_PRD_ARTIGO_VARIANTE_VALOR" };
            createIndexOperation.Columns.Add("ID_LINX");
            createIndexOperation.Columns.Add("ID_ARTIGO");
            createIndexOperation.Columns.Add("ID_ATRIBUTO_DEFINICAO");
            createIndexOperation.Columns.Add("ID_ATRIBUTO");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_PRD.PRD_ARTIGO", IsUnique = true, Name = "XAK1_PRD_ARTIGO" };
            createIndexOperation.Columns.Add("ID_LINX");
            createIndexOperation.Columns.Add("COD_ARTIGO");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_TCS.TCS_USUARIO", IsUnique = true, Name = "XAK1_TCS_USUARIO" };
            createIndexOperation.Columns.Add("UID_USUARIO");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_TCS.TCS_MODULO", IsUnique = true, Name = "XAK1_TCS_MODULO" };
            createIndexOperation.Columns.Add("DESC_MODULO");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_TCS.TCS_MODULO_DO_GRUPO", IsUnique = true, Name = "XAK1_TCS_MODULO_DO_GRUPO" };
            createIndexOperation.Columns.Add("ID_GRUPO_MODULO");
            createIndexOperation.Columns.Add("ID_MODULO");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_TCS.TCS_OBJETO", IsUnique = true, Name = "XAK1_TCS_OBJETO" };
            createIndexOperation.Columns.Add("DESC_OBJETO");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_TCS.TCS_TRANSACAO", IsUnique = true, Name = "XAK1_TCS_TRANSACAO" };
            createIndexOperation.Columns.Add("COD_TRANSACAO");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_TCS.TCS_TRANSACAO", IsUnique = true, Name = "XAK2_TCS_TRANSACAO" };
            createIndexOperation.Columns.Add("CLASSE_NOME");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_TBC.TBC_BANDEIRA_REDE", IsUnique = true, Name = "XAK1_BANDEIRA_REDE" };
            createIndexOperation.Columns.Add("ID_LINX");
            createIndexOperation.Columns.Add("COD_BANDEIRA_REDE");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_TBC.TBC_PFJ", IsUnique = true, Name = "XAK1_TBC_PFJ" };
            createIndexOperation.Columns.Add("ID_LINX");
            createIndexOperation.Columns.Add("ID_GPECON_FILTRO");
            createIndexOperation.Columns.Add("CODIGO_PFJ");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_TBC.TBC_PFJ", IsUnique = true, Name = "XAK2_TBC_PFJ" };
            createIndexOperation.Columns.Add("ID_LINX");
            createIndexOperation.Columns.Add("ID_GPECON_FILTRO");
            createIndexOperation.Columns.Add("CNPJ_CPF");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_LJV.LJV_LOJA", IsUnique = true, Name = "XAK1_LJV_LOJA" };
            createIndexOperation.Columns.Add("ID_LINX");
            createIndexOperation.Columns.Add("COD_LOJA");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_LJV.LJV_LOJA", IsUnique = true, Name = "XAK2_LJV_LOJA" };
            createIndexOperation.Columns.Add("ID_LINX");
            createIndexOperation.Columns.Add("ID_LOJA");
            createIndexOperation.Columns.Add("ID_STK_DEPOSITO");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_TCS.TCS_USUARIO_FAVORITO", IsUnique = true, Name = "XAK1_TCS_USUARIO_FAVORITO" };
            createIndexOperation.Columns.Add("ID_USUARIO");
            createIndexOperation.Columns.Add("ID_MODULO");
            createIndexOperation.Columns.Add("ID_TRANSACAO");
            createIndexOperation.Columns.Add("ID_MODULO_MENU");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_TCS.TCS_USUARIO_EXTERNO", IsUnique = true, Name = "XAK1_TCS_USUARIO_EXTERNO" };
            createIndexOperation.Columns.Add("ID_LINX");
            createIndexOperation.Columns.Add("IDENTIDADE_EXTERNA");
            createIndexOperation.Columns.Add("ID_DISPOSITIVO");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_LJV.LJV_TERMINAL", IsUnique = true, Name = "XAK1_LJV_TERMINAL" };
            createIndexOperation.Columns.Add("ID_LINX");
            createIndexOperation.Columns.Add("HOSTNAME");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_TCS.TCS_OBJETO_PERMISSAO", IsUnique = true, Name = "XAK1_TCS_OBJETO_PERMISSAO" };
            createIndexOperation.Columns.Add("ID_OBJETO");
            createIndexOperation.Columns.Add("ID_PERFIL");
            createIndexOperation.Columns.Add("ID_USUARIO");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_TCS.TCS_MOEDA_INDICADOR_COTACAO", IsUnique = true, Name = "XAK1_TCS_MOEDA_INDICADOR_COTACAO" };
            createIndexOperation.Columns.Add("ID_LINX");
            createIndexOperation.Columns.Add("ID_MOEDA_INDICADOR");
            createIndexOperation.Columns.Add("ID_MOEDA_COTACAO");
            createIndexOperation.Columns.Add("DATA_COTACAO");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_TCS.TCS_MOEDA_INDICADOR_COTACAO", IsUnique = false, Name = "XIE1_TCS_MOEDA_INDICADOR_COTACAO" };
            createIndexOperation.Columns.Add("ID_MOEDA_INDICADOR");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_TCS.TCS_MOEDA_INDICADOR_COTACAO", IsUnique = false, Name = "XIE2_TCS_MOEDA_INDICADOR_COTACAO" };
            createIndexOperation.Columns.Add("ID_LINX");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "LX_TCS.TCS_PARAMETRO_VALOR", IsUnique = true, Name = "XAK1_TCS_PARAMETRO_VALOR" };
            createIndexOperation.Columns.Add("ID_LINX");
            createIndexOperation.Columns.Add("ID_PARAMETRO");
            createIndexOperation.Columns.Add("UID_TABELA");
            createIndexOperation.Columns.Add("CHAVE_SELECAO");
            _tableMigrator.Indexes.Add(createIndexOperation);
            //Add Primary Keys
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_ARQUIVO_GRUPO"] = "XPK_TCS_ARQUIVO_GRUPO";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_ARQUIVO_GRUPO_VINCULO"] = "XPK_TCS_ARQUIVO_GRUPO_VINCULO";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_ARQUIVO"] = "XPK_TCS_ARQUIVO";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_ARQUIVO_LOG"] = "XPK_TCS_ARQUIVO_LOG";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_ARQUIVO_ITEM"] = "XPK_TCS_ARQUIVO_ITEM";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_ARQUIVO_ITEM_CAMPO"] = "XPK_TCS_ARQUIVO_ITEM_CAMPO";
            _tableMigrator.PrimaryKeys["LX_PRD.PRD_SKU_PRODUTO"] = "XPK_PRD_SKU_PRODUTO";
            _tableMigrator.PrimaryKeys["LX_PRD.PRD_ARTIGO_VARIANTE"] = "XPK_PRD_ARTIGO_VARIANTE";
            _tableMigrator.PrimaryKeys["LX_PRD.PRD_ARTIGO_VARIANTE_VALOR"] = "XPK_PRD_ARTIGO_VARIANTE_VALOR";
            _tableMigrator.PrimaryKeys["LX_PRD.PRD_ARTIGO"] = "XPK_PRD_ARTIGO";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_USUARIO"] = "XPK_TCS_USUARIO";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_FILTRO"] = "XPK_TCS_FILTRO";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_LAYOUT"] = "XPK_TCS_LAYOUT";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_MODULO"] = "XPK_TCS_MODULO";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_MODULO_DO_GRUPO"] = "XPK_TCS_MODULO_DO_GRUPO";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_MODULO_GRUPO"] = "XPK_TCS_MODULO_GRUPO";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_MODULO_MENU"] = "XPK_TCS_MODULO_MENU";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_OBJETO"] = "XPK_TCS_OBJETO";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_OBJETO_CONTEUDO"] = "XPK_TCS_OBJETO_CONTEUDO";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_PERFIL"] = "XPK_TCS_PERFIL";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_PERFIL_REGRA_COLUNA"] = "XPK_TCS_PERFIL_REGRA_COLUNA";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_PERFIL_REGRA_MODULO"] = "XPK_TCS_PERFIL_REGRA_MODULO";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_PERFIL_REGRA_TRANSACAO"] = "XPK_TCS_PERFIL_REGRA_TRANSACAO";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_PERFIL_TRANSACAO_FILTRO"] = "XPK_TCS_PERFIL_TRANSACAO_FILTRO";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_TRANSACAO"] = "XPK_TCS_TRANSACAO";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_TRANSACAO_DEPENDENTE"] = "XPK_TCS_TRANSACAO_DEPENDENTE";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_TRANSACAO_MENU"] = "XPK_TCS_TRANSACAO_MENU";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_USUARIO_PERFIL"] = "XPK_TCS_USUARIO_PERFIL";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_USUARIO_REGRA_COLUNA"] = "XPK_TCS_USUARIO_REGRA_COLUNA";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_USUARIO_REGRA_MODULO"] = "XPK_TCS_USUARIO_REGRA_MODULO";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_USUARIO_REGRA_TRANSACAO"] = "XPK_TCS_USUARIO_REGRA_TRANSACAO";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_USUARIO_TRANSACAO_FILTRO"] = "XPK_TCS_USUARIO_TRANSACAO_FILTRO";
            _tableMigrator.PrimaryKeys["LX_TBC.TBC_FILIAL"] = "XPK_TBC_FILIAL";
            _tableMigrator.PrimaryKeys["LX_TBC.TBC_GRUPO_ECONOMICO"] = "XPK_TBC_GRUPO_ECONOMICO";
            _tableMigrator.PrimaryKeys["LX_TBC.TBC_BANDEIRA_REDE"] = "XPK_TBC_BANDEIRA_REDE";
            _tableMigrator.PrimaryKeys["LX_TBC.TBC_PFJ"] = "XPK_TBC_PFJ";
            _tableMigrator.PrimaryKeys["LX_LJV.LJV_LOJA"] = "XPK_LJV_LOJA";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_USUARIO_EXTERNO"] = "XPK_TCS_USUARIO_EXTERNO";
            _tableMigrator.PrimaryKeys["LX_LJV.LJV_TERMINAL"] = "XPK_LJV_TERMINAL";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_INDICADOR_INDICE"] = "XPK_TCS_INDICADOR_INDICE";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_INDICADOR_MEDIDA"] = "XPK_TCS_INDICADOR_MEDIDA";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_MOEDA_INDICADOR"] = "XPK_TCS_MOEDA_INDICADOR__NC__";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_MOEDA_INDICADOR_COTACAO"] = "XPK_TCS_MOEDA_INDICADOR_COTACAO__NC__";
            _tableMigrator.PrimaryKeys["LX_DOC.DOC_CLASSIFICADOR"] = "XPK_DOC_CLASSIFICADOR";
            _tableMigrator.PrimaryKeys["LX_DOC.DOC_MULTIMIDIA"] = "XPK_DOC_MULTIMIDIA";
            _tableMigrator.PrimaryKeys["LX_DOC.DOC_MULTIMIDIA_CONFIG"] = "XPK_DOC_MULTIMIDIA_CONFIG";
            _tableMigrator.PrimaryKeys["LX_DOC.DOC_MULTIMIDIA_TABELA"] = "XPK_DOC_MULTIMIDIA_TABELA";
            _tableMigrator.PrimaryKeys["LX_TCS.TCS_PARAMETRO_VALOR"] = "XPK_TCS_PARAMETRO_VALOR";
            //Add Defaults
            _tableMigrator.Defauls.Add("LX_TCS.TCS_ARQUIVO_GRUPO_VINCULO.INATIVO", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_ARQUIVO_GRUPO_VINCULO.ORDEM", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_ARQUIVO.INATIVO", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_ARQUIVO_LOG.LX_TIPO_LOG", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_ARQUIVO_ITEM.INDICA_NOTNULL", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_ARQUIVO_ITEM.ORDEM", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_ARQUIVO_ITEM_CAMPO.TAMANHO", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_ARQUIVO_ITEM_CAMPO.DECIMAIS", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_ARQUIVO_ITEM_CAMPO.INDICA_NOTNULL", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_ARQUIVO_ITEM_CAMPO.INDICA_PK", "((0))");
            _tableMigrator.Defauls.Add("LX_PRD.PRD_SKU_PRODUTO.REF_MARCA_FABRICANTE", "(' ')");
            _tableMigrator.Defauls.Add("LX_PRD.PRD_SKU_PRODUTO.MULTIPLO_UNIDADE", "((0))");
            _tableMigrator.Defauls.Add("LX_PRD.PRD_SKU_PRODUTO.DESMEMBRA_ITEM_VENDA", "((0))");
            _tableMigrator.Defauls.Add("LX_PRD.PRD_SKU_PRODUTO.DESMEMBRA_ITEM_FISCAL", "((0))");
            _tableMigrator.Defauls.Add("LX_PRD.PRD_SKU_PRODUTO.DESMEMBRA_ITEM_ESTOQUE", "((0))");
            _tableMigrator.Defauls.Add("LX_PRD.PRD_SKU_PRODUTO.INDICA_SKU_VENDA", "((0))");
            _tableMigrator.Defauls.Add("LX_PRD.PRD_SKU_PRODUTO.INDICA_SKU_COMPRA", "((0))");
            _tableMigrator.Defauls.Add("LX_PRD.PRD_SKU_PRODUTO.INDICA_NUMERO_SERIE", "((0))");
            _tableMigrator.Defauls.Add("LX_PRD.PRD_SKU_PRODUTO.PESO_BRUTO", "((0))");
            _tableMigrator.Defauls.Add("LX_PRD.PRD_SKU_PRODUTO.PESO_LIQUIDO", "((0))");
            _tableMigrator.Defauls.Add("LX_PRD.PRD_SKU_PRODUTO.INATIVO", "((0))");
            _tableMigrator.Defauls.Add("LX_PRD.PRD_SKU_PRODUTO.QTDE_MIN_COMPRA", "((0))");
            _tableMigrator.Defauls.Add("LX_PRD.PRD_SKU_PRODUTO.QTDE_MULTIPLO_COMPRA", "((0))");
            _tableMigrator.Defauls.Add("LX_PRD.PRD_SKU_PRODUTO.QTDE_LOTE_ECONOMICO_COMPRA", "((0))");
            _tableMigrator.Defauls.Add("LX_PRD.PRD_ARTIGO_VARIANTE.INATIVO", "((0))");
            _tableMigrator.Defauls.Add("LX_PRD.PRD_ARTIGO_VARIANTE_VALOR.INATIVO", "((0))");
            _tableMigrator.Defauls.Add("LX_PRD.PRD_ARTIGO.REF_MARCA_FABRICANTE", "(' ')");
            _tableMigrator.Defauls.Add("LX_PRD.PRD_ARTIGO.NUMERO_DECIMAIS", "((0))");
            _tableMigrator.Defauls.Add("LX_PRD.PRD_ARTIGO.VARIA_PRECO_SKU", "((0))");
            _tableMigrator.Defauls.Add("LX_PRD.PRD_ARTIGO.CTRL_ESTOQUE", "((0))");
            _tableMigrator.Defauls.Add("LX_PRD.PRD_ARTIGO.PESO", "((0))");
            _tableMigrator.Defauls.Add("LX_PRD.PRD_ARTIGO.INATIVO", "((0))");
            _tableMigrator.Defauls.Add("LX_PRD.PRD_ARTIGO.DATA_ATUALIZACAO", "(getdate())");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_USUARIO.DATA_CADASTRO", "(getdate())");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_USUARIO.DATA_ALTERACAO", "(getdate())");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_FILTRO.INDICA_USO_LINX", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_FILTRO.INATIVO", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_LAYOUT.LAYOUT_PADRAO", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_LAYOUT.ULT_ATUALIZACAO", "(getdate())");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_LAYOUT.INATIVO", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_LAYOUT.UID_OBJETO_CONTEUDO", "NEWID()");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_MODULO.ORDEM_NAVEGACAO", "((1))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_MODULO.INATIVO", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_MODULO_MENU.ORDEM_NAVEGACAO", "((1))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_PERFIL.INDICA_PERFIL_LINX", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_PERFIL.INATIVO", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_TRANSACAO.INATIVO", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_TRANSACAO_DEPENDENTE.POSSUI_TOOLBAR", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_TRANSACAO_DEPENDENTE.POSSUI_VISAO_TABULAR", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_TRANSACAO_DEPENDENTE.COMPARTILHA_BO_PRINCIPAL", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_TRANSACAO_DEPENDENTE.VISIVEL", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_TRANSACAO_DEPENDENTE.USA_FILTROS_DO_BO_PRINCIPAL", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_TRANSACAO_DEPENDENTE.EXECUTA_PESQUISA", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_LIMPA", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_PESQUISA", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_ADICAO", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_EDICAO", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_EXCLUSAO", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_PESQUISA_ESP", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_LAYOUT", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_NAVEGACAO", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_TRANSACAO_DEPENDENTE.MOSTRA_BOTAO_IMPRESSAO", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_TRANSACAO_MENU.ORDEM_NAVEGACAO", "((1))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_TRANSACAO_MENU.SUGESTAO_LINX", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_TRANSACAO_MENU.INATIVO", "((0))");
            _tableMigrator.Defauls.Add("LX_TBC.TBC_FILIAL.CODIGO_FILIAL", "((0))");
            _tableMigrator.Defauls.Add("LX_TBC.TBC_FILIAL.INDICA_MATRIZ_CONTABIL", "((0))");
            _tableMigrator.Defauls.Add("LX_TBC.TBC_FILIAL.INDICA_LOJA", "((0))");
            _tableMigrator.Defauls.Add("LX_TBC.TBC_FILIAL.INATIVO", "((0))");
            _tableMigrator.Defauls.Add("LX_TBC.TBC_FILIAL.INDICA_HUBFISCAL", "((0))");
            _tableMigrator.Defauls.Add("LX_TBC.TBC_FILIAL.INDICA_HUBFISCAL_ATUALIZADO", "((0))");
            _tableMigrator.Defauls.Add("LX_TBC.TBC_GRUPO_ECONOMICO.INDICA_MOEDA_FORTE", "((0))");
            _tableMigrator.Defauls.Add("LX_TBC.TBC_GRUPO_ECONOMICO.FATOR_CAMBIO", "((1))");
            _tableMigrator.Defauls.Add("LX_TBC.TBC_PFJ.CODIGO_PFJ", "((0))");
            _tableMigrator.Defauls.Add("LX_TBC.TBC_PFJ.DATA_CADASTRO", "(getdate())");
            _tableMigrator.Defauls.Add("LX_TBC.TBC_PFJ.DATA_ALTERACAO", "(getdate())");
            _tableMigrator.Defauls.Add("LX_TBC.TBC_PFJ.INDICA_CLIENTE_LOJA", "((0))");
            _tableMigrator.Defauls.Add("LX_TBC.TBC_PFJ.INDICA_FORNECEDOR", "((0))");
            _tableMigrator.Defauls.Add("LX_TBC.TBC_PFJ.INDICA_TRANSPORTADORA", "((0))");
            _tableMigrator.Defauls.Add("LX_TBC.TBC_PFJ.INDICA_FILIAL", "((0))");
            _tableMigrator.Defauls.Add("LX_TBC.TBC_PFJ.INDICA_ESTRANGEIRO", "((0))");
            _tableMigrator.Defauls.Add("LX_TBC.TBC_PFJ.LX_TIPO_LOGRADOURO", "(' ')");
            _tableMigrator.Defauls.Add("LX_TBC.TBC_PFJ.COMPLEMENTO", "(' ')");
            _tableMigrator.Defauls.Add("LX_TBC.TBC_PFJ.DDI_CELULAR", "(' ')");
            _tableMigrator.Defauls.Add("LX_TBC.TBC_PFJ.DDD_CELULAR", "(' ')");
            _tableMigrator.Defauls.Add("LX_TBC.TBC_PFJ.FONE_CELULAR", "(' ')");
            _tableMigrator.Defauls.Add("LX_TBC.TBC_PFJ.DDI_FIXO", "(' ')");
            _tableMigrator.Defauls.Add("LX_TBC.TBC_PFJ.DDD_FIXO", "(' ')");
            _tableMigrator.Defauls.Add("LX_TBC.TBC_PFJ.FONE_FIXO", "(' ')");
            _tableMigrator.Defauls.Add("LX_LJV.LJV_LOJA.AREA_M2", "((0))");
            _tableMigrator.Defauls.Add("LX_LJV.LJV_LOJA.FATOR_P", "((0))");
            _tableMigrator.Defauls.Add("LX_LJV.LJV_LOJA.FATOR_Q", "((0))");
            _tableMigrator.Defauls.Add("LX_LJV.LJV_LOJA.FATOR_F", "((0))");
            _tableMigrator.Defauls.Add("LX_LJV.LJV_LOJA.FATOR_S", "((0))");
            _tableMigrator.Defauls.Add("LX_LJV.LJV_LOJA.FATOR_W", "((0))");
            _tableMigrator.Defauls.Add("LX_LJV.LJV_LOJA.FATOR_E", "((0))");
            _tableMigrator.Defauls.Add("LX_LJV.LJV_LOJA.INATIVO", "((0))");
            _tableMigrator.Defauls.Add("LX_LJV.LJV_LOJA.DATA_CADASTRO", "(getdate())");
            _tableMigrator.Defauls.Add("LX_LJV.LJV_LOJA.DATA_ATUALIZACAO", "(getdate())");
            _tableMigrator.Defauls.Add("LX_LJV.LJV_LOJA.INDICA_FRANQUIA", "((0))");
            _tableMigrator.Defauls.Add("LX_LJV.LJV_LOJA.CEP", "(' ')");
            _tableMigrator.Defauls.Add("LX_LJV.LJV_LOJA.COMPLEMENTO", "(' ')");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_USUARIO_EXTERNO.DATA_CADASTRO", "(getdate())");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_USUARIO_EXTERNO.INATIVO", "((0))");
            _tableMigrator.Defauls.Add("LX_LJV.LJV_TERMINAL.INDICA_TERMINAL_MESTRE", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_INDICADOR_INDICE.RGB", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_MOEDA_INDICADOR.INDICA_INDICE", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_MOEDA_INDICADOR_COTACAO.COTACAO", "((0))");
            _tableMigrator.Defauls.Add("LX_DOC.DOC_MULTIMIDIA.DATA_CRIACAO", "GETDATE()");
            _tableMigrator.Defauls.Add("LX_DOC.DOC_MULTIMIDIA_CONFIG.DOC_LARGURA", "((0))");
            _tableMigrator.Defauls.Add("LX_DOC.DOC_MULTIMIDIA_CONFIG.DOC_ALTURA", "((0))");
            _tableMigrator.Defauls.Add("LX_DOC.DOC_MULTIMIDIA_TABELA.ORDEM_APRESENTACAO", "((0))");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_PARAMETRO_VALOR.POSSUI_VARIACAO", "0");
            _tableMigrator.Defauls.Add("LX_TCS.TCS_PARAMETRO_VALOR.INATIVO", "0");
            //Add Nullables
            _tableMigrator.Nullables.Add("LX_TCS.TCS_ARQUIVO_ITEM.ID_ARQUIVO_ITEM_PAI");
            _tableMigrator.Nullables.Add("LX_PRD.PRD_SKU_PRODUTO.ID_PRD_VARIANTE");
            _tableMigrator.Nullables.Add("LX_PRD.PRD_SKU_PRODUTO.ID_COD_ORIGEM_MERCADORIA");
            _tableMigrator.Nullables.Add("LX_PRD.PRD_SKU_PRODUTO.ID_AGRUPADOR_REGRA_PRD");
            _tableMigrator.Nullables.Add("LX_PRD.PRD_SKU_PRODUTO.ID_FINALIDADE");
            _tableMigrator.Nullables.Add("LX_PRD.PRD_SKU_PRODUTO.ID_CLASSIF_FISCAL");
            _tableMigrator.Nullables.Add("LX_PRD.PRD_SKU_PRODUTO.ID_CEST");
            _tableMigrator.Nullables.Add("LX_PRD.PRD_ARTIGO_VARIANTE.ID_PRD_VARIANTE_VALOR_01");
            _tableMigrator.Nullables.Add("LX_PRD.PRD_ARTIGO_VARIANTE.ID_PRD_VARIANTE_VALOR_02");
            _tableMigrator.Nullables.Add("LX_PRD.PRD_ARTIGO_VARIANTE.ID_PRD_VARIANTE_VALOR_03");
            _tableMigrator.Nullables.Add("LX_PRD.PRD_ARTIGO_VARIANTE.ID_PRD_VARIANTE_VALOR_04");
            _tableMigrator.Nullables.Add("LX_PRD.PRD_ARTIGO_VARIANTE.ID_PRD_VARIANTE_VALOR_05");
            _tableMigrator.Nullables.Add("LX_PRD.PRD_ARTIGO.ID_GPECON_FILTRO");
            _tableMigrator.Nullables.Add("LX_PRD.PRD_ARTIGO.ID_UNIDADE_MEDIDA");
            _tableMigrator.Nullables.Add("LX_PRD.PRD_ARTIGO.ID_PRD_MERCADOLOGICO_AUX1");
            _tableMigrator.Nullables.Add("LX_PRD.PRD_ARTIGO.ID_PRD_MERCADOLOGICO_AUX2");
            _tableMigrator.Nullables.Add("LX_PRD.PRD_ARTIGO.ID_MARCA_FABRICANTE_ATRIBUTO");
            _tableMigrator.Nullables.Add("LX_TCS.TCS_FILTRO.ID_USUARIO");
            _tableMigrator.Nullables.Add("LX_TCS.TCS_MODULO_MENU.ID_MODULO_MENU_SUPERIOR");
            _tableMigrator.Nullables.Add("LX_TBC.TBC_FILIAL.ID_MATRIZ_CONTABIL_PFJ");
            _tableMigrator.Nullables.Add("LX_TBC.TBC_FILIAL.ID_AGRUPADOR_REGRA_FILIAL");
            _tableMigrator.Nullables.Add("LX_TBC.TBC_FILIAL.ID_CENTRO_RESULTADO");
            _tableMigrator.Nullables.Add("LX_TBC.TBC_GRUPO_ECONOMICO.ID_GPECON_SUPERIOR");
            _tableMigrator.Nullables.Add("LX_TBC.TBC_GRUPO_ECONOMICO.ID_MOEDA_INDICADOR");
            _tableMigrator.Nullables.Add("LX_TBC.TBC_GRUPO_ECONOMICO.ID_PFJ");
            _tableMigrator.Nullables.Add("LX_TBC.TBC_PFJ.ID_GPECON_FILTRO");
            _tableMigrator.Nullables.Add("LX_TBC.TBC_PFJ.ID_INDICADOR_FISCAL_PFJ");
            _tableMigrator.Nullables.Add("LX_TBC.TBC_PFJ.ID_REGIME_TRIBUTARIO");
            _tableMigrator.Nullables.Add("LX_TBC.TBC_PFJ.ID_AGRUPADOR_REGRA_PFJ");
            _tableMigrator.Nullables.Add("LX_TBC.TBC_PFJ.ID_MUNICIPIO");
            _tableMigrator.Nullables.Add("LX_TBC.TBC_PFJ.ID_UF");
            _tableMigrator.Nullables.Add("LX_TBC.TBC_PFJ.ID_CEP");
            _tableMigrator.Nullables.Add("LX_TBC.TBC_PFJ.ID_PAIS");
            _tableMigrator.Nullables.Add("LX_LJV.LJV_LOJA.ID_BANDEIRA_REDE");
            _tableMigrator.Nullables.Add("LX_LJV.LJV_LOJA.ID_REGIAO_COMERCIAL");
            _tableMigrator.Nullables.Add("LX_LJV.LJV_LOJA.ID_CEP");
            _tableMigrator.Nullables.Add("LX_LJV.LJV_LOJA.ID_AGRUPAMENTO_SORTIMENTO");
            _tableMigrator.Nullables.Add("LX_LJV.LJV_LOJA.ID_AGRUPAMENTO_COMERCIAL");
            _tableMigrator.Nullables.Add("LX_LJV.LJV_LOJA.ID_TAB_PRECO_COMPRA");
            _tableMigrator.Nullables.Add("LX_LJV.LJV_LOJA.ID_STK_DEPOSITO");
            _tableMigrator.Nullables.Add("LX_LJV.LJV_LOJA.ID_OPERACAO_FINALIDADE_PRINCIPAL");
            _tableMigrator.Nullables.Add("LX_TCS.TCS_OBJETO_PERMISSAO.ID_PERFIL");
            _tableMigrator.Nullables.Add("LX_TCS.TCS_OBJETO_PERMISSAO.ID_USUARIO");
            _tableMigrator.Nullables.Add("LX_TCS.TCS_MOEDA_INDICADOR_COTACAO.ID_MOEDA_INDICADOR");
            _tableMigrator.Nullables.Add("LX_TCS.TCS_MOEDA_INDICADOR_COTACAO.ID_MOEDA_COTACAO");
            _tableMigrator.Nullables.Add("LX_DOC.DOC_MULTIMIDIA.UID_DOC_BASE_GERADOR");
            //Add Foreign Keys
            _tableMigrator.Fks.Add("LX_TCS.TCS_ARQUIVO_GRUPO.ID_ARQUIVO_GRUPO.LX_TCS.TCS_ARQUIVO_GRUPO_VINCULO.ID_ARQUIVO_GRUPO", "XFK_TCS_ARQUIVO_GRUPO_VINC_2,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_ARQUIVO.ID_ARQUIVO.LX_TCS.TCS_ARQUIVO_GRUPO_VINCULO.ID_ARQUIVO", "XFK_TCS_ARQUIVO_GRUPO_VINC_1,true");
            _tableMigrator.Fks.Add("LX_TCS.TCS_ARQUIVO.ID_ARQUIVO.LX_TCS.TCS_ARQUIVO_LOG.ID_ARQUIVO", "XFK_TCS_ARQUIVO_LOG_1,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_ARQUIVO.ID_ARQUIVO.LX_TCS.TCS_ARQUIVO_ITEM.ID_ARQUIVO", "XFK_TCS_ARQUIVO_ITEM_1,true");
            _tableMigrator.Fks.Add("LX_TCS.TCS_ARQUIVO_ITEM.ID_ARQUIVO_ITEM.LX_TCS.TCS_ARQUIVO_ITEM.ID_ARQUIVO_ITEM_PAI", "XFK_TCS_ARQUIVO_ITEM_2,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_ARQUIVO_ITEM.ID_ARQUIVO_ITEM.LX_TCS.TCS_ARQUIVO_ITEM_CAMPO.ID_ARQUIVO_ITEM", "XFK_TCS_ARQUIVO_ITEM_CAMPO_1,true");
            _tableMigrator.Fks.Add("LX_PRD.PRD_ARTIGO_VARIANTE.ID_PRD_VARIANTE.LX_PRD.PRD_SKU_PRODUTO.ID_PRD_VARIANTE", "XFK_PRD_SKU_PRODUTO_1,false");
            _tableMigrator.Fks.Add("LX_PRD.PRD_ARTIGO.ID_ARTIGO.LX_PRD.PRD_SKU_PRODUTO.ID_ARTIGO", "XFK_PRD_SKU_PRODUTO_2,true");
            _tableMigrator.Fks.Add("LX_PRD.PRD_ARTIGO_VARIANTE_VALOR.ID_PRD_VARIANTE_VALOR.LX_PRD.PRD_ARTIGO_VARIANTE.ID_PRD_VARIANTE_VALOR_05", "XFK_PRD_ARTIGO_VARIANTE_1,false");
            _tableMigrator.Fks.Add("LX_PRD.PRD_ARTIGO_VARIANTE_VALOR.ID_PRD_VARIANTE_VALOR.LX_PRD.PRD_ARTIGO_VARIANTE.ID_PRD_VARIANTE_VALOR_04", "XFK_PRD_ARTIGO_VARIANTE_2,false");
            _tableMigrator.Fks.Add("LX_PRD.PRD_ARTIGO_VARIANTE_VALOR.ID_PRD_VARIANTE_VALOR.LX_PRD.PRD_ARTIGO_VARIANTE.ID_PRD_VARIANTE_VALOR_01", "XFK_PRD_ARTIGO_VARIANTE_4,false");
            _tableMigrator.Fks.Add("LX_PRD.PRD_ARTIGO_VARIANTE_VALOR.ID_PRD_VARIANTE_VALOR.LX_PRD.PRD_ARTIGO_VARIANTE.ID_PRD_VARIANTE_VALOR_02", "XFK_PRD_ARTIGO_VARIANTE_5,false");
            _tableMigrator.Fks.Add("LX_PRD.PRD_ARTIGO_VARIANTE_VALOR.ID_PRD_VARIANTE_VALOR.LX_PRD.PRD_ARTIGO_VARIANTE.ID_PRD_VARIANTE_VALOR_03", "XFK_PRD_ARTIGO_VARIANTE_6,false");
            _tableMigrator.Fks.Add("LX_PRD.PRD_ARTIGO.ID_ARTIGO.LX_PRD.PRD_ARTIGO_VARIANTE.ID_ARTIGO", "XFK_PRD_ARTIGO_VARIANTE_3,false");
            _tableMigrator.Fks.Add("LX_PRD.PRD_ARTIGO.ID_ARTIGO.LX_PRD.PRD_ARTIGO_VARIANTE_VALOR.ID_ARTIGO", "XFK_PRD_ARTIGO_VARIANTE_VALOR_3,true");
            _tableMigrator.Fks.Add("LX_TCS.TCS_USUARIO.ID_USUARIO.LX_ADT.ADT_AUDITORIA.ID_USUARIO", "FK_ADT_AUDITORIA_77049BA9,false");
            _tableMigrator.Fks.Add("LX_ADT.ADT_AUDITORIA_ITEM.ID_ADT_AUDITORIA_ITEM.LX_ADT.ADT_AUDITORIA_ITEM_DETALHE.ID_ADT_AUDITORIA_ITEM", "FK_ADT_AUDITORIA_ITEM_D9C8DF22,false");
            _tableMigrator.Fks.Add("LX_ADT.ADT_AUDITORIA.ID_ADT_AUDITORIA.LX_ADT.ADT_AUDITORIA_ITEM.ID_ADT_AUDITORIA", "FK_ADT_AUDITORIA_ITEM_922199FF,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_USUARIO.ID_USUARIO.LX_TCS.TCS_FILTRO.ID_USUARIO", "XFK_TCS_FILTRO_1,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_OBJETO_CONTEUDO.ID_OBJETO_CONTEUDO.LX_TCS.TCS_LAYOUT.ID_OBJETO_CONTEUDO", "XFK_TCS_LAYOUT_1,true");
            _tableMigrator.Fks.Add("LX_TCS.TCS_MODULO_GRUPO.ID_GRUPO_MODULO.LX_TCS.TCS_MODULO_DO_GRUPO.ID_GRUPO_MODULO", "XFK_TCS_MODULO_DO_GRUPO_1,true");
            _tableMigrator.Fks.Add("LX_TCS.TCS_MODULO.ID_MODULO.LX_TCS.TCS_MODULO_MENU.ID_MODULO", "FK_TCS_MODULO_MENU_BDBFEACA,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_MODULO_MENU.ID_MODULO_MENU.LX_TCS.TCS_MODULO_MENU.ID_MODULO_MENU_SUPERIOR", "XFK_TCS_MODULO_MENU_1,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_PERFIL.ID_PERFIL.LX_TCS.TCS_PERFIL_REGRA_COLUNA.ID_PERFIL", "XFK_TCS_PERFIL_REGRA_COLUNA_1,true");
            _tableMigrator.Fks.Add("LX_TCS.TCS_PERFIL.ID_PERFIL.LX_TCS.TCS_PERFIL_REGRA_MODULO.ID_PERFIL", "XFK_TCS_PERFIL_REGRA_MODULO_1,true");
            _tableMigrator.Fks.Add("LX_TCS.TCS_PERFIL.ID_PERFIL.LX_TCS.TCS_PERFIL_REGRA_TRANSACAO.ID_PERFIL", "XFK_TCS_PERFIL_REGRA_TRAN_1,true");
            _tableMigrator.Fks.Add("LX_TCS.TCS_TRANSACAO.ID_TRANSACAO.LX_TCS.TCS_TRANSACAO_DEPENDENTE.ID_TRANSACAO", "XFK_TCS_TRANSACAO_DEPENDENTE_1,true");
            _tableMigrator.Fks.Add("LX_TCS.TCS_PERFIL.ID_PERFIL.LX_TCS.TCS_USUARIO_PERFIL.ID_PERFIL", "FK_TCS_USUARIO_PERFIL_7CA37F62,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_USUARIO.ID_USUARIO.LX_TCS.TCS_USUARIO_PERFIL.ID_USUARIO", "FK_TCS_USUARIO_PERFIL_4C5E61A7,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_USUARIO.ID_USUARIO.LX_TCS.TCS_USUARIO_REGRA_COLUNA.ID_USUARIO", "XFK_TCS_USUARIO_REGRA_COLUNA_1,true");
            _tableMigrator.Fks.Add("LX_TCS.TCS_USUARIO.ID_USUARIO.LX_TCS.TCS_USUARIO_REGRA_MODULO.ID_USUARIO", "XFK_TCS_USUARIO_REGRA_MODULO_1,true");
            _tableMigrator.Fks.Add("LX_TCS.TCS_USUARIO.ID_USUARIO.LX_TCS.TCS_USUARIO_REGRA_TRANSACAO.ID_USUARIO", "XFK_TCS_USUARIO_REGRA_TRAN_1,true");
            _tableMigrator.Fks.Add("LX_TBC.TBC_FILIAL.ID_FILIAL_PFJ.LX_TBC.TBC_FILIAL.ID_MATRIZ_CONTABIL_PFJ", "XFK_TBC_FILIAL_3,false");
            _tableMigrator.Fks.Add("LX_TBC.TBC_GRUPO_ECONOMICO.ID_GPECON.LX_TBC.TBC_FILIAL.ID_GPECON", "XFK_TBC_FILIAL_2,false");
            _tableMigrator.Fks.Add("LX_TBC.TBC_PFJ.ID_PFJ.LX_TBC.TBC_FILIAL.ID_FILIAL_PFJ", "XFK_TBC_FILIAL_1,false");
            _tableMigrator.Fks.Add("LX_TBC.TBC_GRUPO_ECONOMICO.ID_GPECON.LX_TBC.TBC_GRUPO_ECONOMICO.ID_GPECON_SUPERIOR", "XFK_TBC_GRUPO_ECONOMICO_2,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_USUARIO.ID_USUARIO.LX_TCS.TCS_USUARIO_GPECON.ID_USUARIO", "FK_TCS_USUARIO_GPECON_C14A7C23,false");
            _tableMigrator.Fks.Add("LX_TBC.TBC_GRUPO_ECONOMICO.ID_GPECON.LX_TCS.TCS_USUARIO_GPECON.ID_GPECON", "FK_TCS_USUARIO_GPECON_77A9A1E9,false");
            _tableMigrator.Fks.Add("LX_TBC.TBC_FILIAL.ID_FILIAL_PFJ.LX_LJV.LJV_LOJA.ID_FILIAL_PFJ", "XFK_LJV_LOJA_1,false");
            _tableMigrator.Fks.Add("LX_TBC.TBC_GRUPO_ECONOMICO.ID_GPECON.LX_LJV.LJV_LOJA.ID_GPECON", "XFK_LJV_LOJA_3,false");
            _tableMigrator.Fks.Add("LX_TBC.TBC_BANDEIRA_REDE.ID_BANDEIRA_REDE.LX_LJV.LJV_LOJA.ID_BANDEIRA_REDE", "XFK_LJV_LOJA_6,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_USUARIO.ID_USUARIO.LX_TCS.TCS_USUARIO_FAVORITO.ID_USUARIO", "FK_TCS_USUARIO_FAVORI_443A2096,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_PERFIL.ID_PERFIL.LX_TCS.TCS_PERFIL_FILIAL.ID_PERFIL", "FK_TCS_PERFIL_FILIAL_9A036152,false");
            _tableMigrator.Fks.Add("LX_TBC.TBC_FILIAL.ID_FILIAL_PFJ.LX_TCS.TCS_PERFIL_FILIAL.ID_FILIAL_PFJ", "FK_TCS_PERFIL_FILIAL_3452205E,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_USUARIO.ID_USUARIO.LX_TCS.TCS_USUARIO_FILIAL.ID_USUARIO", "FK_TCS_USUARIO_FILIAL_7BC147CF,false");
            _tableMigrator.Fks.Add("LX_TBC.TBC_FILIAL.ID_FILIAL_PFJ.LX_TCS.TCS_USUARIO_FILIAL.ID_FILIAL_PFJ", "FK_TCS_USUARIO_FILIAL_04DDB9CE,false");
            _tableMigrator.Fks.Add("LX_LJV.LJV_LOJA.ID_LOJA.LX_LJV.LJV_TERMINAL.ID_LOJA", "FK_LJV_TERMINAL_E33A9957,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_PERFIL.ID_PERFIL.LX_TCS.TCS_OBJETO_PERMISSAO.ID_PERFIL", "FK_TCS_OBJETO_PERMISS_6DFC6FC5,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_USUARIO.ID_USUARIO.LX_TCS.TCS_OBJETO_PERMISSAO.ID_USUARIO", "FK_TCS_OBJETO_PERMISS_DAA2C113,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_FILTRO.ID_FILTRO.LX_TCS.TCS_PERFIL_TRANSACAO_FILTRO.ID_FILTRO", "FK_TCS_PERFIL_TRANSAC_AB12E516,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_PERFIL.ID_PERFIL.LX_TCS.TCS_PERFIL_TRANSACAO_FILTRO.ID_PERFIL", "FK_TCS_PERFIL_TRANSAC_B9ECBA9A,true");
            _tableMigrator.Fks.Add("LX_TCS.TCS_USUARIO.ID_USUARIO.LX_TCS.TCS_USUARIO_TRANSACAO_FILTRO.ID_USUARIO", "FK_TCS_USUARIO_TRANSA_CB0EF1FC,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_PERFIL.ID_PERFIL.LX_TCS.TCS_USUARIO_TRANSACAO_FILTRO.ID_PERFIL", "FK_TCS_USUARIO_TRANSA_ED253889,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_FILTRO.ID_FILTRO.LX_TCS.TCS_USUARIO_TRANSACAO_FILTRO.ID_FILTRO", "FK_TCS_USUARIO_TRANSA_93F901A5,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_USUARIO.ID_USUARIO.LX_TCS.TCS_USUARIO_BANDEIRA_REDE.ID_USUARIO", "FK_TCS_USUARIO_BANDEI_E7A2A270,true");
            _tableMigrator.Fks.Add("LX_TBC.TBC_BANDEIRA_REDE.ID_BANDEIRA_REDE.LX_TCS.TCS_USUARIO_BANDEIRA_REDE.ID_BANDEIRA_REDE", "FK_TCS_USUARIO_BANDEI_EC7C0669,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_PERFIL.ID_PERFIL.LX_TCS.TCS_PERFIL_BANDEIRA_REDE.ID_PERFIL", "FK_TCS_PERFIL_BANDEIR_F6D9E261,true");
            _tableMigrator.Fks.Add("LX_TBC.TBC_BANDEIRA_REDE.ID_BANDEIRA_REDE.LX_TCS.TCS_PERFIL_BANDEIRA_REDE.ID_BANDEIRA_REDE", "FK_TCS_PERFIL_BANDEIR_F8CDDE33,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_LAYOUT.ID_OBJETO_CONTEUDO.LX_TCS.TCS_LAYOUT_PERFIL.ID_OBJETO_CONTEUDO", "FK_TCS_LAYOUT_PERFIL_D5B474F9,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_PERFIL.ID_PERFIL.LX_TCS.TCS_LAYOUT_PERFIL.ID_PERFIL", "FK_TCS_LAYOUT_PERFIL_FD1D9E0E,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_LAYOUT.ID_OBJETO_CONTEUDO.LX_TCS.TCS_LAYOUT_USUARIO.ID_OBJETO_CONTEUDO", "FK_TCS_LAYOUT_USUARIO_424C63BB,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_USUARIO.ID_USUARIO.LX_TCS.TCS_LAYOUT_USUARIO.ID_USUARIO", "FK_TCS_LAYOUT_USUARIO_8BC316C0,false");
            _tableMigrator.Fks.Add("LX_TCS.TCS_INDICADOR_MEDIDA.ID_INDICADOR_MEDIDA.LX_TCS.TCS_INDICADOR_INDICE.ID_INDICADOR_MEDIDA", "XFK_TCS_INDICADOR_INDICE_1,true");
            _tableMigrator.Fks.Add("LX_TCS.TCS_MOEDA_INDICADOR.ID_MOEDA_INDICADOR.LX_TCS.TCS_MOEDA_INDICADOR_COTACAO.ID_MOEDA_INDICADOR", "FK_TCS_MOEDA_INDICADO_FB952A51,true");
            _tableMigrator.Fks.Add("LX_TCS.TCS_MOEDA_INDICADOR.ID_MOEDA_INDICADOR.LX_TCS.TCS_MOEDA_INDICADOR_COTACAO.ID_MOEDA_COTACAO", "FK_TCS_MOEDA_INDICADO_89A0D7C3,false");
            _tableMigrator.Fks.Add("LX_DOC.DOC_CLASSIFICADOR.ID_DOC_CLASSIFICADOR.LX_DOC.DOC_MULTIMIDIA.ID_DOC_CLASSIFICADOR", "XFK_DOC_MULTIMIDIA_2,false");
            _tableMigrator.Fks.Add("LX_DOC.DOC_MULTIMIDIA.UID_DOCUMENTO.LX_DOC.DOC_MULTIMIDIA.UID_DOC_BASE_GERADOR", "FK_DOC_MULTIMIDIA_AUT_74096654,false");
            _tableMigrator.Fks.Add("LX_DOC.DOC_MULTIMIDIA.UID_DOCUMENTO.LX_DOC.DOC_MULTIMIDIA_TABELA.UID_DOCUMENTO", "FK_DOC_MULTIMIDIA_TAB_3BB9BE9F,true");

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
