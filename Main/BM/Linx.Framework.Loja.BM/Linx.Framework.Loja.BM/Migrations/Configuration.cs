			


namespace Linx.Framework.Loja.BM.Migrations
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
	using Linx.Data.Migration;

    internal sealed class Configuration : DbMigrationsConfiguration<ConectorPos>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;    
			AutomaticMigrationDataLossAllowed = false;
            SetSqlGenerator("System.Data.SQLite", new SqlMigrator());   
			    
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

	internal sealed class SqlMigrator : SQLiteEntityMigrationSqlGenerator
    {
        private SqlTableMigrator _tableMigrator;
        public SqlMigrator()
            : base()
        {
            _tableMigrator = new SqlTableMigrator();
            CreateIndexOperation createIndexOperation;
            //Add Indexes
            createIndexOperation = new CreateIndexOperation() { Table = "dbo.LJV_PARAMETRO", IsUnique = true, Name = "XAK1LJV_PARAMETRO" };
            createIndexOperation.Columns.Add("TITULO_PARAMETRO");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "dbo.LJV_LOJA", IsUnique = true, Name = "XAK1LJV_LOJA" };
            createIndexOperation.Columns.Add("COD_LOJA");
            _tableMigrator.Indexes.Add(createIndexOperation);
            //Add Defaults
            _tableMigrator.Defauls.Add("dbo.LJV_MODULO.INATIVO", "((0))");
            _tableMigrator.Defauls.Add("dbo.LJV_MODULO.ORDEM_NAVEGACAO", "((1))");
            _tableMigrator.Defauls.Add("dbo.LJV_MODULO_MENU.ORDEM_NAVEGACAO", "((1))");
            _tableMigrator.Defauls.Add("dbo.LJV_TRANSACAO_MENU.ORDEM_NAVEGACAO", "((1))");
            _tableMigrator.Defauls.Add("dbo.LJV_TRANSACAO_MENU.INATIVO", "((0))");
            _tableMigrator.Defauls.Add("dbo.LJV_TRANSACAO.INATIVO", "((0))");
            _tableMigrator.Defauls.Add("dbo.LJV_PARAMETRO.TITULO_PARAMETRO", "' '");
            _tableMigrator.Defauls.Add("dbo.LJV_VENDEDOR.COD_VENDEDOR", "0");
            _tableMigrator.Defauls.Add("dbo.LJV_VENDEDOR.INATIVO", "0");
            _tableMigrator.Defauls.Add("dbo.LJV_VENDEDOR.INDICA_GERENTE", "0");
            _tableMigrator.Defauls.Add("dbo.LJV_VENDEDOR.INDICA_OPERADOR_CAIXA", "0");
            _tableMigrator.Defauls.Add("dbo.LJV_VENDEDOR.DATA_ATIVACAO", "DATETIME('now', 'localtime')");
            _tableMigrator.Defauls.Add("dbo.LJV_VENDEDOR.DATA_DESATIVACAO", "DATETIME('now', 'localtime')");
            _tableMigrator.Defauls.Add("dbo.LJV_LOJA.LX_TIPO_LOGRADOURO", "' '");
            _tableMigrator.Defauls.Add("dbo.LJV_LOJA.COMPLEMENTO", "' '");
            _tableMigrator.Defauls.Add("dbo.LJV_LOJA.INATIVO", "0");
            //Add Nullables
            _tableMigrator.Nullables.Add("dbo.LJV_MODULO_MENU.ID_MODULO_MENU_SUPERIOR");
            _tableMigrator.Nullables.Add("dbo.LJV_VENDEDOR.ID_SUPERVISOR");
            //Add Foreign Keys
            _tableMigrator.Fks.Add("dbo.LJV_MODULO.ID_MODULO.dbo.LJV_MODULO_MENU.ID_MODULO", "FK_LJV_MODULO_MENU_B19F6A11,false");
            _tableMigrator.Fks.Add("dbo.LJV_MODULO_MENU.ID_MODULO_MENU.dbo.LJV_MODULO_MENU.ID_MODULO_MENU_SUPERIOR", "FK_LJV_MODULO_MENU_137E3A99,false");
            _tableMigrator.Fks.Add("dbo.LJV_MODULO_MENU.ID_MODULO_MENU.dbo.LJV_TRANSACAO_MENU.ID_MODULO_MENU", "FK_LJV_TRANSACAO_MENU_E40F2204,false");
            _tableMigrator.Fks.Add("dbo.LJV_TRANSACAO.ID_TRANSACAO.dbo.LJV_TRANSACAO_MENU.ID_TRANSACAO", "FK_LJV_TRANSACAO_MENU_A40025AB,false");
            _tableMigrator.Fks.Add("dbo.LJV_VENDEDOR.ID_VENDEDOR.dbo.LJV_VENDEDOR.ID_SUPERVISOR", "LJV_VENDEDOR1,false");
            _tableMigrator.Fks.Add("dbo.LJV_LOJA.ID_LOJA.dbo.LJV_VENDEDOR.ID_LOJA", "LJV_VENDEDOR0,false");

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
				string headerInfo = "-- Script SQLite was generated by Linx Systems\r\n" +
									"-- Company home page: http://www.linx.com.br\r\n" +
									String.Format("-- Script date {0:d/M/yyyy HH:mm:ss}", DateTime.Now) + "\r\n";
				var first = statements.First();
				first.Sql = headerInfo + first.Sql;
                var migHistory = statements.FirstOrDefault(e => e.Sql.Contains("__MigrationHistory"));
                if (migHistory != null)
                    statements.Remove(migHistory);
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
