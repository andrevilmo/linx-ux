			


namespace Linx.Demo.BM.Migrations
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

    internal sealed class Configuration : DbMigrationsConfiguration<BMDTesteFrame>
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
            //Add Primary Keys
            _tableMigrator.PrimaryKeys["dbo.CIDADE"] = "PK_CIDADE";
            _tableMigrator.PrimaryKeys["dbo.CODIGO_FISCAL"] = "PK_CODIGO_FISCAL";
            _tableMigrator.PrimaryKeys["dbo.ESTADO"] = "PK_ESTADO";
            _tableMigrator.PrimaryKeys["dbo.FORMA_PAGAMENTO"] = "PK_FORMA_PAGAMENTO";
            _tableMigrator.PrimaryKeys["dbo.LOJA"] = "PK_LOJA";
            _tableMigrator.PrimaryKeys["dbo.MARCAS"] = "PK_MARCAS";
            _tableMigrator.PrimaryKeys["dbo.PAIS"] = "PK_PAIS";
            _tableMigrator.PrimaryKeys["dbo.PRODUTO"] = "PK_PRODUTO";
            _tableMigrator.PrimaryKeys["dbo.REGIAO_MARCA"] = "PK_REGIAO_MARCA";
            _tableMigrator.PrimaryKeys["dbo.REPRESENTANTE_MARCA"] = "PK_REPRESENTANTE_MARCA";
            _tableMigrator.PrimaryKeys["dbo.VENDA"] = "PK_VENDA";
            _tableMigrator.PrimaryKeys["dbo.VENDA_ITEM"] = "PK_VENDA_ITEM";
            _tableMigrator.PrimaryKeys["dbo.VENDEDOR"] = "PK_VENDEDOR";
            _tableMigrator.PrimaryKeys["dbo.VENDATESTE"] = "PK_VENDATESTE";
            _tableMigrator.PrimaryKeys["dbo.VENDA_ITEM_TESTE"] = "PK_VENDA_ITEM_TESTE";
            //Add Nullables
            _tableMigrator.Nullables.Add("dbo.ESTADO.ID_PAIS");
            _tableMigrator.Nullables.Add("dbo.LOJA.ID_ESTADO");
            _tableMigrator.Nullables.Add("dbo.LOJA.ID_CIDADE");
            _tableMigrator.Nullables.Add("dbo.MARCAS.ID_REPRESENTANTE");
            _tableMigrator.Nullables.Add("dbo.MARCAS.ID_REGIAO");
            _tableMigrator.Nullables.Add("dbo.VENDA.ID_FORMA_PAGAMENTO");
            _tableMigrator.Nullables.Add("dbo.VENDA.ID_VENDEDOR");
            _tableMigrator.Nullables.Add("dbo.VENDEDOR.ID_FORMA_PAGAMENTO");
            //Add Foreign Keys
            _tableMigrator.Fks.Add("dbo.PAIS.ID_PAIS.dbo.ESTADO.ID_PAIS", "FK_ESTADO_PAIS,false");
            _tableMigrator.Fks.Add("dbo.CIDADE.ID_CIDADE.dbo.LOJA.ID_CIDADE", "FK_LOJA_CIDADE,false");
            _tableMigrator.Fks.Add("dbo.ESTADO.ID_ESTADO.dbo.LOJA.ID_ESTADO", "FK_LOJA_ESTADO,false");
            _tableMigrator.Fks.Add("dbo.PAIS.ID_PAIS.dbo.LOJA.ID_PAIS", "FK_LOJA_PAIS,false");
            _tableMigrator.Fks.Add("dbo.LOJA.ID_LOJA.dbo.MARCAS.ID_LOJA", "FK_MARCAS_LOJA,false");
            _tableMigrator.Fks.Add("dbo.REGIAO_MARCA.ID_REGIAO_MARCA.dbo.MARCAS.ID_REGIAO", "FK_MARCAS_REGIAO_MARCA,false");
            _tableMigrator.Fks.Add("dbo.REPRESENTANTE_MARCA.ID_REPRESENTANTE.dbo.MARCAS.ID_REPRESENTANTE", "FK_MARCAS_REPRESENTANTE_MARCA,false");
            _tableMigrator.Fks.Add("dbo.FORMA_PAGAMENTO.ID_FORMA_PAGAMENTO.dbo.VENDA.ID_FORMA_PAGAMENTO", "FK_VENDA_FORMA_PAGAMENTO1,false");
            _tableMigrator.Fks.Add("dbo.LOJA.ID_LOJA.dbo.VENDA.ID_LOJA", "FK_VENDA_LOJA,false");
            _tableMigrator.Fks.Add("dbo.VENDEDOR.ID_VENDEDOR.dbo.VENDA.ID_VENDEDOR", "FK_VENDA_VENDEDOR,false");
            _tableMigrator.Fks.Add("dbo.VENDA.ID_VENDA.dbo.VENDA_ITEM.ID_VENDA", "FK_VENDA_ITEM_VENDA,false");
            _tableMigrator.Fks.Add("dbo.FORMA_PAGAMENTO.ID_FORMA_PAGAMENTO.dbo.VENDEDOR.ID_FORMA_PAGAMENTO", "FK_VENDEDOR_FORMA_PAGAMENTO,false");
            _tableMigrator.Fks.Add("dbo.VENDATESTE.ID_VENDA_TESTE.dbo.VENDA_ITEM_TESTE.ID_VENDA_TESTE", "FK_VENDA_ITEM_TESTE_VENDATESTE,false");

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
