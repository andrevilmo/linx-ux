namespace Linx.BusinessModelDesigner.CustomizedCode.DatabaseScriptGenerator
{
    public class ScriptGeneratorSqlServer : ScriptGeneratorBase
    {        
        #region Constructors
        public ScriptGeneratorSqlServer() : this(null) { }
        public ScriptGeneratorSqlServer(ScriptGeneratorOptions options) : base(options, ScriptGeneratorType.SqlServer) { }
        #endregion
    }
}
