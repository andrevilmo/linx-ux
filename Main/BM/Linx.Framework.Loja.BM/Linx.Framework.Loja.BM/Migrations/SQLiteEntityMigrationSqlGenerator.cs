using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Migrations.Sql;
using System.Data.Entity.Migrations.Utilities;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Linx.Data.Migration
{
    /// <sumary>
    /// SQLiteEntityMigrationSqlGenerator 
    /// </sumary>
    public class SQLiteEntityMigrationSqlGenerator : MigrationSqlGenerator
    {
        #region Constants

        private const string pstrDefaultDateTime = "yyyy-MM-dd hh:mm:ss";
        private const int pintDefaultStringMaxLength = 255;
        private const int pintDefaultPrecisaoNumerica = 10;
        private const byte pbytDefaultPrecisaoTempo = 7;
        private const byte pintDefaultEscala = 0;
        

        #endregion

        #region Instancias

        private DbProviderManifest pprovProviderManifest;
        private List<MigrationStatement> plstCommands;
        private Dictionary<string, List<string>> foreignKeys = new Dictionary<string, List<string>>();

        #endregion

        #region Generate Script for SQLite.

        /// <summary>
        /// Convert the operations list into SQLite commands.
        /// </summary>
        /// <param name="lstOperacoesMigrations">Operações a serem convertidas</param>
        /// <param name="strManifestoProvider">Representa o Encoding do SQLite</param>
        /// <returns>Uma lista de comandos SQLite</returns>
        public override IEnumerable<MigrationStatement> Generate(
            IEnumerable<MigrationOperation> lstOperacoesMigrations, string strManifestoProvider)
        {
            plstCommands = new List<MigrationStatement>();

            InicializaServicosProvider(strManifestoProvider);

            foreach (dynamic ldynOperacao in lstOperacoesMigrations)
            {
                if (ldynOperacao is AddForeignKeyOperation)
                    PrepareForeignkeys((AddForeignKeyOperation)ldynOperacao);
            }
            
            //Generate thhe whole script
            GenerateCommands(lstOperacoesMigrations);
            
            return plstCommands;

        }

        #endregion

        #region Generating commands

        /// <summary>
        /// Generate SQL for an operation <see cref="CreateTableOperation" />.
        /// </summary>
        /// <param name="taleOperation"></param>
        protected virtual void Generate(CreateTableOperation taleOperation)
        {
            using (var ltextWriter = Writer())
            {
                CreateTableScript(taleOperation, ltextWriter);

                ComandoSQL(ltextWriter);
            }
        }


        /// <summary>
        /// Prepare foreign keys for table script generating.
        /// </summary>
        /// <param name="fkOperation"></param>
        private void PrepareForeignkeys(AddForeignKeyOperation fkOperation)
        {
            if (!foreignKeys.ContainsKey(fkOperation.DependentTable))
                foreignKeys.Add(fkOperation.DependentTable, new List<string>());

            foreignKeys[fkOperation.DependentTable].Add("FOREIGN KEY (" + String.Join(", ", fkOperation.DependentColumns) + ") REFERENCES " + RemoveDBO(fkOperation.PrincipalTable) + " (" + String.Join(", ", fkOperation.PrincipalColumns) + ")" + (fkOperation.CascadeDelete ? " ON DELETE CASCADE" : ""));
        }

        /// <summary>
        /// Generate SQL for an operation <see cref="AddForeignKeyOperation" />.
        /// </summary>
        /// <param name="opeChaveEstrangeira"> The operation to produce SQL for. </param>
        protected virtual void Generate(AddForeignKeyOperation fkOperation)
        {
            //throw new NotSupportedException();
        }

        /// <summary>
        /// Generate SQL for an operation <see cref="DropForeignKeyOperation" />.
        /// </summary>
        /// <param name="dropForeignKeyOperation"> The operation to produce SQL for. </param>
        protected virtual void Generate(DropForeignKeyOperation fkOperation)
        {
            //throw new NotSupportedException();
            
        }

        /// <summary>
        /// Generate SQL for an operation <see cref="CreateIndexOperation" />.
        /// </summary>
        /// <param name="indexIndex"> The operation to produce SQL for. </param>
        protected virtual void Generate(CreateIndexOperation indexIndex)
        {
            using (var ltextWriter = Writer())
            {
                ltextWriter.Write("CREATE ");

                if (indexIndex.IsUnique)
                    ltextWriter.Write(" UNIQUE ");

                ltextWriter.Write("INDEX ");
                ltextWriter.Write(RemoveDBO(indexIndex.Table) + "_" + indexIndex.Name);
                ltextWriter.Write(" ON ");
                ltextWriter.Write(RemoveDBO(indexIndex.Table));
                ltextWriter.Write("(");

                for (int lintCount = 0; lintCount < indexIndex.Columns.Count; lintCount++)
                {
                    var lstrDadosColuna = indexIndex.Columns[lintCount];

                    ltextWriter.Write(lstrDadosColuna);

                    if (lintCount < indexIndex.Columns.Count - 1)
                        ltextWriter.WriteLine(",");
                }

                ltextWriter.Write(")");

                ComandoSQL(ltextWriter);
            }
        }

        /// <summary>
        /// Generate SQL for an operation <see cref="DropIndexOperation" />.
        /// </summary>
        /// <param name="opeDropIndex"> The operation to produce SQL for. </param>
        protected virtual void Generate(DropIndexOperation opeDropIndex)
        {
            using (var ltextWriter = Writer())
            {
                ltextWriter.Write("DROP INDEX ");
                ltextWriter.Write(opeDropIndex.Name);

                ComandoSQL(ltextWriter);
            }
        }

        /// <summary>
        /// Generate SQL for an operation <see cref="AddPrimaryKeyOperation" />.
        /// </summary>
        /// <param name="opeAdicionaPrimaryKey"> The operation to produce SQL for. </param>
        protected virtual void Generate(AddPrimaryKeyOperation opeAdicionaPrimaryKey)
        {
            using (var ltextWriter = Writer())
            {
                ltextWriter.Write("ALTER TABLE ");
                ltextWriter.Write(RemoveDBO(opeAdicionaPrimaryKey.Table));
                ltextWriter.Write(" ADD CONSTRAINT ");
                ltextWriter.Write(opeAdicionaPrimaryKey.Name);
                ltextWriter.Write(" PRIMARY KEY ");
                ltextWriter.Write("(");

                for (int li = 0; li < opeAdicionaPrimaryKey.Columns.Count; li++)
                {
                    var lstrDadosColuna = opeAdicionaPrimaryKey.Columns[li];

                    ltextWriter.Write(lstrDadosColuna);

                    if (li < opeAdicionaPrimaryKey.Columns.Count - 1)
                        ltextWriter.WriteLine(",");
                }

                ltextWriter.Write(")");

                ComandoSQL(ltextWriter);
            }
        }

        /// <summary>
        /// Generate SQL for an operation <see cref="DropPrimaryKeyOperation" />.
        /// </summary>
        /// <param name="opeDropPrimaryKey"> The operation to produce SQL for. </param>
        protected virtual void Generate(DropPrimaryKeyOperation opeDropPrimaryKey)
        {
            using (var ltextWriter = Writer())
            {
                ltextWriter.Write("ALTER TABLE ");
                ltextWriter.Write(RemoveDBO(opeDropPrimaryKey.Table));
                ltextWriter.Write(" DROP CONSTRAINT ");
                ltextWriter.Write(opeDropPrimaryKey.Name);

                ComandoSQL(ltextWriter);
            }
        }

        /// <summary>
        /// Generate SQL for an operation <see cref="AddColumnOperation" />.
        /// </summary>
        /// <param name="opeAdicionaColuna"> The operation to produce SQL for. </param>
        protected virtual void Generate(AddColumnOperation opeAdicionaColuna)
        {
            using (var ltextWriter = Writer())
            {
                ltextWriter.Write("ALTER TABLE ");
                ltextWriter.Write(RemoveDBO(opeAdicionaColuna.Table));
                ltextWriter.Write(" ADD ");

                var lcmColuna = opeAdicionaColuna.Column;

                Generate(lcmColuna, ltextWriter, null);

                if ((lcmColuna.IsNullable != null)
                    && !lcmColuna.IsNullable.Value
                    && (lcmColuna.DefaultValue == null)
                    && (string.IsNullOrWhiteSpace(lcmColuna.DefaultValueSql))
                    && !lcmColuna.IsIdentity
                    && !lcmColuna.IsTimestamp
                    && !lcmColuna.StoreType.Equals("rowversion", StringComparison.InvariantCultureIgnoreCase)
                    && !lcmColuna.StoreType.Equals("timestamp", StringComparison.InvariantCultureIgnoreCase))
                {
                    ltextWriter.Write(" DEFAULT ");

                    if (lcmColuna.Type == PrimitiveTypeKind.DateTime)
                    {
                        ltextWriter.Write(Generate(DateTime.Parse("1900-01-01 00:00:00", CultureInfo.InvariantCulture)));
                    }
                    else
                    {
                        ltextWriter.Write(Generate((dynamic)lcmColuna.ClrDefaultValue));
                    }
                }

                ComandoSQL(ltextWriter);
            }
        }

        /// <summary>
        /// Generate SQL for an operation <see cref="DropColumnOperation" />.
        /// </summary>
        /// <param name="opeRemoveColuna"> The operation to produce SQL for. </param>
        protected virtual void Generate(DropColumnOperation opeRemoveColuna)
        {
            using (var ltextWriter = Writer())
            {
                ltextWriter.Write("ALTER TABLE ");
                ltextWriter.Write(RemoveDBO(opeRemoveColuna.Table));
                ltextWriter.Write(" DROP COLUMN ");
                ltextWriter.Write(opeRemoveColuna.Name);

                ComandoSQL(ltextWriter);
            }
        }

        /// <summary>
        /// Generate SQL for an operation <see cref="AlterColumnOperation" />.
        /// </summary>
        /// <param name="opeAlteraColuna"> The operation to produce SQL for. </param>
        protected virtual void Generate(AlterColumnOperation opeAlteraColuna)
        {
            var lcmColuna = opeAlteraColuna.Column;

            using (var ltextWriter = Writer())
            {
                ltextWriter.Write("ALTER TABLE ");
                ltextWriter.Write(RemoveDBO(opeAlteraColuna.Table));
                ltextWriter.Write(" ALTER COLUMN ");
                ltextWriter.Write(lcmColuna.Name);
                ltextWriter.Write(" ");
                ltextWriter.Write(CreateColumnType(lcmColuna));

                if ((lcmColuna.IsNullable != null)
                    && !lcmColuna.IsNullable.Value)
                {
                    ltextWriter.Write(" NOT NULL");
                }

                ComandoSQL(ltextWriter);
            }

            if ((lcmColuna.DefaultValue == null) && string.IsNullOrWhiteSpace(lcmColuna.DefaultValueSql))
                return;

            using (var ltextWriter = Writer())
            {
                ltextWriter.Write("ALTER TABLE ");
                ltextWriter.Write(RemoveDBO(opeAlteraColuna.Table));
                ltextWriter.Write(" ALTER COLUMN ");
                ltextWriter.Write(lcmColuna.Name);
                ltextWriter.Write(" DROP DEFAULT");

                ComandoSQL(ltextWriter);
            }

            using (var ltextWriter = Writer())
            {
                ltextWriter.Write("ALTER TABLE ");
                ltextWriter.Write(RemoveDBO(opeAlteraColuna.Table));
                ltextWriter.Write(" ALTER COLUMN ");
                ltextWriter.Write(lcmColuna.Name);
                ltextWriter.Write(" SET DEFAULT ");
                ltextWriter.Write(
                    (lcmColuna.DefaultValue != null)
                        ? Generate((dynamic)lcmColuna.DefaultValue)
                        : lcmColuna.DefaultValueSql
                    );

                ComandoSQL(ltextWriter);
            }
        }

        /// <summary>
        /// Generate SQL for an operation <see cref="DropTableOperation" />.
        /// </summary>
        /// <param name="opeDropTable"> The operation to produce SQL for. </param>
        protected virtual void Generate(DropTableOperation opeDropTable)
        {
            using (var ltextWriter = Writer())
            {
                ltextWriter.Write("DROP TABLE ");
                ltextWriter.Write(RemoveDBO(opeDropTable.Name));

                ComandoSQL(ltextWriter);
            }
        }

        /// <summary>
        /// Generate SQL for an operation <see cref="SqlOperation" />.
        /// </summary>
        /// <param name="opeSQL"> The operation to produce SQL for. </param>
        protected virtual void Generate(SqlOperation opeSQL)
        {
            AddNewSQL(opeSQL.Sql, opeSQL.SuppressTransaction);
        }

        /// <summary>
        /// Generate SQL for an operation <see cref="RenameColumnOperation" />.
        /// </summary>
        /// <param name="opeRenomearColuna"> The operation to produce SQL for. </param>
        protected virtual void Generate(RenameColumnOperation opeRenomearColuna)
        {
            // Inicialmente não suportada
            //throw new NotSupportedException();
        }

        /// <summary>
        /// Generate SQL for an operation <see cref="RenameTableOperation" />.
        /// </summary>
        /// <param name="opeRenameTable"> The operation to produce SQL for. </param>
        protected virtual void Generate(RenameTableOperation opeRenameTable)
        {
            //throw new NotSupportedException();
        }

        /// <summary>
        /// Generate SQL for an operation <see cref="MoveTableOperation" />.
        /// </summary>
        /// <param name="opeMoveTable"> The operation to produce SQL for. </param>
        protected virtual void Generate(MoveTableOperation opeMoveTable)
        {
            //throw new NotSupportedException();
        }

        /// <summary>
        /// Generate data for a column
        /// </summary>
        /// <param name="cmDataColumn"></param>
        /// <param name="textWriter"></param>
        /// <param name="primaryKeyOperation"></param>
        private void Generate(ColumnModel cmDataColumn, IndentedTextWriter textWriter, PrimaryKeyOperation primaryKeyOperation)
        {
            textWriter.Write(cmDataColumn.Name);
            textWriter.Write(" ");
            bool isPrimaryKey = false;

            if (primaryKeyOperation != null)
                isPrimaryKey = primaryKeyOperation.Columns.Contains(cmDataColumn.Name);
            
            if (isPrimaryKey)
            {
                if ((cmDataColumn.Type == PrimitiveTypeKind.Int16) ||
                    (cmDataColumn.Type == PrimitiveTypeKind.Int32))
                    textWriter.Write(" INTEGER ");
                else
                    textWriter.Write(CreateColumnType(cmDataColumn));

                if (cmDataColumn.IsIdentity)
                {
                    textWriter.Write(" PRIMARY KEY AUTOINCREMENT ");
                }
            }
            else
            {
                textWriter.Write(CreateColumnType(cmDataColumn));

                if ((cmDataColumn.IsNullable != null)
                    && !cmDataColumn.IsNullable.Value)
                {
                    textWriter.Write(" NOT NULL");
                }

                if (cmDataColumn.DefaultValue != null)
                {
                    textWriter.Write(" DEFAULT ");
                    textWriter.Write(Generate((dynamic)cmDataColumn.DefaultValue));
                }
                else if (!string.IsNullOrWhiteSpace(cmDataColumn.DefaultValueSql))
                {
                    textWriter.Write(" DEFAULT ");
                    textWriter.Write(cmDataColumn.DefaultValueSql);
                }
            }
        }

        /// <summary>
        /// Generate SQL for an operation <see cref="HistoryOperation" />.
        /// </summary>
        /// <param name="opeHistorico"> The operation to produce SQL for. </param>
        protected virtual void Generate(HistoryOperation opeHistorico)
        {
            // It was removed because it does not currently use the Migration
            
        }

        private string GetValue(DbPropertyExpression prop, DbConstantExpression val)
        {
            var dbCastExpression = val.CastTo(prop.Property.TypeUsage);
            var value = dbCastExpression.ToString();
            return value;
        }

        /// <summary>
        /// Generate a default value for ByteArray.
        /// </summary>
        /// <param name="bytDefaultValue"> The value to be set. </param>
        /// <returns> SQL representing the default value. </returns>
        protected virtual string Generate(byte[] bytDefaultValue)
        {
            var lstrbHexString = new StringBuilder();

            foreach (var lbtByte in bytDefaultValue)
                lstrbHexString.Append(lbtByte.ToString("X2", CultureInfo.InvariantCulture));

            return "x'" + lstrbHexString + "'";
        }

        /// <summary>
        /// Generate a default value for Booleano.
        /// </summary>
        /// <param name="blnDefaultValue"> The value to be set. </param>
        /// <returns> SQL representing the default value. </returns>
        protected virtual string Generate(bool blnDefaultValue)
        {
            return blnDefaultValue ? "1" : "0";
        }

        /// <summary>
        /// Generate a default value for DateTime.
        /// </summary>
        /// <param name="dtmDefaultValue"> The value to be set. </param>
        /// <returns> SQL representing the default value. </returns>
        protected virtual string Generate(DateTime dtmDefaultValue)
        {
            return "'" + dtmDefaultValue.ToString(pstrDefaultDateTime, CultureInfo.InvariantCulture) + "'";
        }

        /// <summary>
        /// Generate a default value for DateTimeOffSet.
        /// </summary>
        /// <param name="dtfDefaultValue"> The value to be set. </param>
        /// <returns> SQL representing the default value. </returns>
        protected virtual string Generate(DateTimeOffset dtfDefaultValue)
        {
            return "'" + dtfDefaultValue.ToString(pstrDefaultDateTime, CultureInfo.InvariantCulture) + "'";
        }

        /// <summary>
        /// Generate a default value for Guid.
        /// </summary>
        /// <param name="guidDefaultValue"> The value to be set. </param>
        /// <returns> SQL representing the default value. </returns>
        protected virtual string Generate(Guid guidDefaultValue)
        {
            return "'" + guidDefaultValue + "'";
        }

        /// <summary>
        /// Generate a default value for String.
        /// </summary>
        /// <param name="strDefaultValue"> The value to be set. </param>
        /// <returns> SQL representing the default value. </returns>
        protected virtual string Generate(string strDefaultValue)
        {
            return "'" + strDefaultValue + "'";
        }

        /// <summary>
        /// Generate a default value for TimeSpan.
        /// </summary>
        /// <param name="tsDefaultValue"> The value to be set. </param>
        /// <returns> SQL representing the default value. </returns>
        protected virtual string Generate(TimeSpan tsDefaultValue)
        {
            return "'" + tsDefaultValue + "'";
        }

        /// <summary>
        /// Generate a default value for object.
        /// </summary>
        /// <param name="objDefaultValue"> The value to be set. </param>
        /// <returns> SQL representing the default value. </returns>
        protected virtual string Generate(object objDefaultValue)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}", objDefaultValue);
        }

        #endregion

        #region Auxiliary methods

        /// <summary>
        ///  Create a column type.
        /// </summary>
        /// <returns> SQL representing the data type. </returns>
        protected virtual string CreateColumnType(ColumnModel modelColumn)
        {
            return modelColumn.IsTimestamp ? "rowversion" : CeatePropertyType(modelColumn);
        }

        /// <summary>
        /// Contruct property type.
        /// </summary>
        /// <param name="modelProperty"></param>
        /// <returns></returns>
        private string CeatePropertyType(ColumnModel modelProperty)
        {
            var lstrOriginalStoreTypeName = modelProperty.StoreType;

            if (string.IsNullOrWhiteSpace(lstrOriginalStoreTypeName))
            {
                var ltypeUsage = pprovProviderManifest.GetStoreType(modelProperty.TypeUsage).EdmType;
                lstrOriginalStoreTypeName = ltypeUsage.Name;
            }

            var lstrStoreTypeName = lstrOriginalStoreTypeName;

            const string lstrSufixoMax = "(max)";

            if (lstrStoreTypeName.EndsWith(lstrSufixoMax, StringComparison.Ordinal))
                lstrStoreTypeName = lstrStoreTypeName.Substring(0, lstrStoreTypeName.Length - lstrSufixoMax.Length) + lstrSufixoMax;

            switch (lstrOriginalStoreTypeName.ToLowerInvariant())
            {
                case "decimal":
                case "numeric":
                    lstrStoreTypeName += "(" + (modelProperty.Precision ?? pintDefaultPrecisaoNumerica)
                                     + ", " + (modelProperty.Scale ?? pintDefaultEscala) + ")";
                    break;
                case "datetime":
                case "time":
                    lstrStoreTypeName += "(" + (modelProperty.Precision ?? pbytDefaultPrecisaoTempo) + ")";
                    break;
                case "blob":
                case "varchar2":
                case "varchar":
                case "char":
                case "nvarchar":
                case "nvarchar2":
                    lstrStoreTypeName += "(" + (modelProperty.MaxLength ?? pintDefaultStringMaxLength) + ")";
                    break;
            }

            return lstrStoreTypeName;
        }

        /// <summary>
        /// Add new SQL command into commands list.
        /// </summary>
        /// <param name="SQLCommand"></param>
        /// <param name="removeTransactionControl"></param>
        protected void AddNewSQL(string SQLCommand, bool removeTransactionControl = false)
        {
            plstCommands.Add(new MigrationStatement
            {
                Sql = SQLCommand + ";",
                SuppressTransaction = removeTransactionControl
            });
        }

        /// <summary>
        ///     Adds a new Statement to be executed against the database.
        /// </summary>
        /// <param name="writer"> The writer containing the SQL to be executed. </param>
        protected void ComandoSQL(IndentedTextWriter writer)
        {
            AddNewSQL(writer.InnerWriter.ToString());
        }

        /// <summary>
        /// Generate an object <see cref="IndentedTextWriter" /> for creating SQL commands.
        /// </summary>
        /// <returns> An empty text writer to use for SQL generation. </returns>
        protected static IndentedTextWriter Writer()
        {
            return new IndentedTextWriter(new StringWriter(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Remove .dbo from Default Migration.
        /// </summary>
        /// <param name="strTexto"></param>
        /// <returns></returns>
        private static string RemoveDBO(string strTexto)
        {
            return strTexto.Replace("dbo.", string.Empty).Replace("DBO.", string.Empty);
        }

        /// <summary>
        /// Generate migration commands.
        /// </summary>
        /// <param name="lstOperacoesMigrations"></param>
        private void GenerateCommands(IEnumerable<MigrationOperation> lstOperacoesMigrations)
        {
            foreach (dynamic ldynOperacao in lstOperacoesMigrations)
                Generate(ldynOperacao);
        }

        /// <summary>
        /// Start SQLite provider
        /// </summary>
        /// <param name="strManifestoProvider"></param>
        private void InicializaServicosProvider(string strManifestoProvider)
        {
            using (var lconConexao = CreateConnection())
            {
                pprovProviderManifest = DbProviderServices
                    .GetProviderServices(lconConexao)
                    .GetProviderManifest(strManifestoProvider);
            }
        }

        /// <summary>
        /// Create SQLiteConnection <see cref="SQLiteConnection" />.
        /// </summary>
        /// <returns> </returns>
        protected virtual DbConnection CreateConnection()
        {
            return new SQLiteConnection();
        }

        /// <summary>
        /// Create Table command.
        /// </summary>
        /// <param name="tableOperation"></param>
        /// <param name="textWriter"></param>
        private void CreateTableScript(CreateTableOperation tableOperation, IndentedTextWriter textWriter)
        {
            textWriter.WriteLine("CREATE TABLE " + RemoveDBO(tableOperation.Name) + " (");
            textWriter.Indent++;

            for (int i = 0; i < tableOperation.Columns.Count; i++)
            {
                ColumnModel lcmDadosColuna = tableOperation.Columns.ToList()[i];
                Generate(lcmDadosColuna, textWriter, tableOperation.PrimaryKey);

                if (i < tableOperation.Columns.Count - 1)
                    textWriter.WriteLine(",");
            }

            //Generating foreign keys
            if (foreignKeys.ContainsKey(tableOperation.Name))
            {
                var fkeys = foreignKeys[tableOperation.Name];
                if (fkeys.Count > 0)
                {
                    textWriter.WriteLine(",");
                    for (int fk_idx = 0; fk_idx < fkeys.Count; fk_idx++)
                    {
                        textWriter.Write(fkeys[fk_idx]);
                        if (fk_idx < fkeys.Count - 1)
                            textWriter.WriteLine(",");
                    }
                }
            }
            
            textWriter.WriteLine();            
            textWriter.Indent--;
            textWriter.Write(")");
        }

        #endregion

    }
}