		


namespace LinxTraining003.BM.Migrations
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

    internal sealed class Configuration : DbMigrationsConfiguration<ModelAle>
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
            createIndexOperation = new CreateIndexOperation() { Table = "dbo.VENDA", IsUnique = false, Name = "IX_ID_CLIENTE" };
            createIndexOperation.Columns.Add("ID_CLIENTE");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "dbo.VENDA_ITEM", IsUnique = false, Name = "IX_ID_PRODUTO" };
            createIndexOperation.Columns.Add("ID_PRODUTO");
            _tableMigrator.Indexes.Add(createIndexOperation);
            createIndexOperation = new CreateIndexOperation() { Table = "dbo.VENDA_ITEM", IsUnique = false, Name = "IX_ID_VENDA" };
            createIndexOperation.Columns.Add("ID_VENDA");
            _tableMigrator.Indexes.Add(createIndexOperation);
            //Add Primary Keys
            _tableMigrator.PrimaryKeys["dbo.CLIENTE"] = "PK_dbo.CLIENTE";
            _tableMigrator.PrimaryKeys["dbo.PRODUTO"] = "PK_dbo.PRODUTO";
            _tableMigrator.PrimaryKeys["dbo.VENDA"] = "PK_dbo.VENDA";
            _tableMigrator.PrimaryKeys["dbo.VENDA_ITEM"] = "PK_dbo.VENDA_ITEM";
            //Add Foreign Keys
            _tableMigrator.Fks.Add("dbo.CLIENTE.ID_CLIENTE.dbo.VENDA.ID_CLIENTE", "FK_VENDA_34bc2f,false");
            _tableMigrator.Fks.Add("dbo.PRODUTO.ID_PRODUTO.dbo.VENDA_ITEM.ID_PRODUTO", "FK_VENDA_ITEM_7beda7,false");
            _tableMigrator.Fks.Add("dbo.VENDA.ID_VENDA.dbo.VENDA_ITEM.ID_VENDA", "FK_VENDA_ITEM_0998a9,false");

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
