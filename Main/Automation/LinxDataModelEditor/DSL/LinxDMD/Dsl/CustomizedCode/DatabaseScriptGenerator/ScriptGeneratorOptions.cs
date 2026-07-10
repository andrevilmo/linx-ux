namespace Linx.BusinessDataModelDesigner.CustomizedCode.DatabaseScriptGenerator
{
    public class ScriptGeneratorOptions
    {
        public ScriptGeneratorOptions()
        {

            this.CreateDatabase = true;
            this.CheckDatabaseExists = true;

            this.CreateSchema = true;
            this.CheckSchemaExists = true;

            this.CheckTableExists = true;

            this.DropForeignKeys = true;
            this.DropIndexes = true;

            this.InsertTabs = false;
            this.TabSpace = 3;
        }

        public bool CreateDatabase { get; set; }
        public bool CheckDatabaseExists { get; set; }
        public bool CreateSchema { get; set; }
        public bool CheckSchemaExists { get; set; }
        public bool CheckTableExists { get; set; }

        public bool DropForeignKeys { get; set; }
        public bool DropIndexes { get; set; }

        public bool InsertTabs { get; set; }
        public byte TabSpace { get; set; }
    }
}