using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Text.RegularExpressions;
using Linx.Tools;
using Linx.Builder.Resources;
using Microsoft.VisualStudio.Modeling.Integration;

namespace Linx.BusinessDataModelDesigner.CustomizedCode.CustomExtension
{
    public class BusinessDataModelGen
    {
        public static void GenerateCode(BusinessDataModelDesignerRoot rootDesigner)
        {
            List<ModelBusAdapter> adapters = rootDesigner.GetModelAdapterss();
            List<BusinessDataModelDesignerRoot> models = new List<BusinessDataModelDesignerRoot>() { rootDesigner };
            models.AddRange(adapters.Select(e => e.GetModelRoot<BusinessDataModelDesignerRoot>()));

            try
            {
                List<string> entities = new List<string>();
                List<string> contexts = new List<string>();
                string contextEventsName = rootDesigner.GetOperationalEventsClassName();
                string contextStartEventsName = rootDesigner.GetStartEventsClassName();
                var modelClasses = rootDesigner.GetModelClasses(models);

                var contextBody = GetContextCode(rootDesigner, models, modelClasses);
                var contextFileName = rootDesigner.UpdateContextTemplate(contextBody, rootDesigner.GetDataContextName());
                contexts.Add(contextFileName.ToLower());

                var contextConfigBody = GetContextConfig(rootDesigner);
                var contextConfigName = rootDesigner.UpdateContextTemplate(contextConfigBody, rootDesigner.GetDataContextName() + "Config");
                contexts.Add(contextConfigName.ToLower());
                WebApiController dataService = rootDesigner.CheckWebApiDataServices(rootDesigner.GetDataContextName());

                foreach (var entity in modelClasses)
                {
                    var entityCode = GetEntityCode(rootDesigner, entity, modelClasses);
                    var entityFileName = rootDesigner.UpdateEntityTemplate(entityCode, entity.Name);
                    entities.Add(entityFileName.ToLower());

                    //Customization
                    List<string> ops = new List<string>();
                    foreach (var clsType in modelClasses.Where(e => e.Name == entity.Name))
                    {
                        foreach (var op in clsType.Operations)
                        {
                            if (!ops.Contains(op.Name))
                            {
                                var cutomEntityCode = GetEntityCustomCode(rootDesigner, op);
                                rootDesigner.UpdateEntityTemplate(cutomEntityCode, entity.Name, op.Name);
                                ops.Add(op.Name);
                            }
                        }
                    }

                    if (dataService != null)
                    {
                        var routeCode = GetEntityRoute(rootDesigner, entity);
                        var routeFileName = rootDesigner.UpdateFileTemplate(routeCode, "router" + entity.Name + ".CodeGen.js", dataService.ProjectSuffix, BusinessDataModelDesignerRoot.EntityFolderRoutes);
                        entities.Add(routeFileName.ToLower());
                    }
                }

                var packageCode = GetPackageCode(rootDesigner);
                rootDesigner.UpdateFileTemplate(packageCode, "package.json");

                string projPrefix = "";
                if (dataService != null)
                {
                    var serverCode = GetServerCode(rootDesigner, dataService.ProjectSuffix);
                    rootDesigner.UpdateFileTemplate(serverCode, "server.CodeGen.js");

                    rootDesigner.UpdateFileTemplate("node_modules/\r\n.vscode/\r\nscript.sql", ".gitignore");

                    var apiCode = GetServiceCode(rootDesigner, modelClasses, dataService.GetRoutePrefix());
                    rootDesigner.UpdateFileTemplate(apiCode, "service.CodeGen.js", dataService.ProjectSuffix);
                }

                rootDesigner.ValidContextFiles(contexts, entities, projPrefix);
            }
            catch (Exception exep)
            {
                throw exep;
            }
            finally
            {
                //Release model bus adapters
                foreach (var modelBus in adapters)
                {
                    modelBus.Dispose();
                }
            }

        }


        private static string GetServerCode(BusinessDataModelDesignerRoot rootDesigner, string projPrefix)
        {
            Linx.Tools.CodeBuilder codeBuilder = new Tools.CodeBuilder();

            string serviceRef = "./" + projPrefix + "/service.CodeGen.js";
            codeBuilder.AddLine("var service = require('" + serviceRef + "');");
            codeBuilder.AddLine("service.start();");

            return codeBuilder.GetBody();
        }

        private static string GetServiceCode(BusinessDataModelDesignerRoot rootDesigner, List<ModelClass> modelClasses, string contextName)
        {
            Linx.Tools.CodeBuilder codeBuilder = new Tools.CodeBuilder();

            string contextRef = "../" + BusinessDataModelDesignerRoot.ModelFolderName + "/" + BusinessDataModelDesignerRoot.ContextFolderName + "/" + rootDesigner.GetDataContextName() + ".CodeGen.js";
            codeBuilder.AddLine("var dbClass = require('" + contextRef + "');");
            codeBuilder.AddLine("var express = require('express');");
            codeBuilder.AddLine("var bodyParser = require('body-parser');");
            codeBuilder.AddLine("var _ = require('underscore');");
            codeBuilder.AddLine("var app = express();");
            codeBuilder.AddLine("var PORT = process.env.PORT || 3000;");
            codeBuilder.AddLine("app.use(bodyParser.json());");


            codeBuilder.AddLine();
            codeBuilder.AddLine("//Add routes for entities");
            foreach (var classType in modelClasses)
            {
                codeBuilder.AddLine("app.use('/" + contextName + "', require('./" + BusinessDataModelDesignerRoot.EntityFolderRoutes + "/router" + classType.Name + ".CodeGen.js')(express, dbClass, _));");
            }
            codeBuilder.AddLine();

            codeBuilder.AddLine("app.get('/" + contextName + "', function (request, response) {");
            codeBuilder.AddLine("   response.set('Content-Type', 'text/xml');");
            codeBuilder.AddLine("   response.send(");

            codeBuilder.AddLine("   '   <service xmlns=\"http://www.w3.org/2007/app\" xmlns:atom=\"http://www.w3.org/2005/Atom\" xml:base=\"http://service/" + contextName + "\">'+");
            codeBuilder.AddLine("   '       <workspace>'+");
            codeBuilder.AddLine("   '       <atom:title type=\"text\">Default</atom:title>'+");

            foreach (var classType in modelClasses)
            {
                codeBuilder.AddLine("   '       <collection href=\"" + classType.Name + "\">'+");
                codeBuilder.AddLine("   '           <atom:title type=\"text\" help=\"" + classType.Name + "_HELP\">" + classType.Name + "</atom:title>'+");
                codeBuilder.AddLine("   '       </collection>'+");
            }

            codeBuilder.AddLine("   '       </workspace>'+");
            codeBuilder.AddLine("   '   </service>'");
            codeBuilder.AddLine("   );");
            codeBuilder.AddLine("});");

            codeBuilder.AddLine();

            codeBuilder.AddLine("module.exports = {");
            codeBuilder.AddLine("   start: function() {");
            codeBuilder.AddLine("          app.listen(PORT, function() {");
            codeBuilder.AddLine("              console.log('Express listening on http://localhost:' + PORT + '/" + contextName + "');");
            codeBuilder.AddLine("          });");
            codeBuilder.AddLine("   }");
            codeBuilder.AddLine("};");

            return codeBuilder.GetBody();
        }

        private static string GetPackageCode(BusinessDataModelDesignerRoot rootDesigner)
        {
            Linx.Tools.CodeBuilder codeBuilder = new Tools.CodeBuilder();

            codeBuilder.AddLine("{");
            codeBuilder.AddLine("  \"name\": \"" + rootDesigner.GetDataContextName() + "\",");
            codeBuilder.AddLine("  \"version\": \"1.0.0\",");
            codeBuilder.AddLine("  \"description\": \"\",");
            codeBuilder.AddLine("  \"main\": \"server.CodeGen.js\",");
            codeBuilder.AddLine("  \"scripts\": {");
            codeBuilder.AddLine("    \"test\": \"echo \\\"Error: no test specified\\\" && exit 1\",");
            codeBuilder.AddLine("    \"start\": \"node server.CodeGen.js\"");
            codeBuilder.AddLine("  },");
            codeBuilder.AddLine("  \"author\": \"Administrator\",");
            codeBuilder.AddLine("  \"license\": \"ISC\",");
            codeBuilder.AddLine("  \"dependencies\": {");
            codeBuilder.AddLine("    \"body-parser\": \"^1.14.2\",");
            codeBuilder.AddLine("    \"express\": \"^4.13.3\",");
            codeBuilder.AddLine("    \"sequelize\": \"^3.23.3\",");

            if (rootDesigner.GetDefaultProvider() == Provider.MySQL)
                codeBuilder.AddLine("    \"mysql\": \"^2.10.2\",");

            if (rootDesigner.GetDefaultProvider() == Provider.SQLite)
                codeBuilder.AddLine("    \"sqlite\": \"0.0.4\",");

            if (rootDesigner.GetDefaultProvider() == Provider.PostgreSQL)
                codeBuilder.AddLine("    \"pg\": \"^4.5.5\",");

            codeBuilder.AddLine("    \"tedious\": \"^1.13.2\",");
            codeBuilder.AddLine("    \"underscore\": \"^1.8.3\"");
            codeBuilder.AddLine("  }");
            codeBuilder.AddLine("}");

            return codeBuilder.GetBody();
        }

        private static string GetContextCode(BusinessDataModelDesignerRoot rootDesigner, List<BusinessDataModelDesignerRoot> models, List<ModelClass> modelClasses)
        {
            Linx.Tools.CodeBuilder codeBuilder = new Tools.CodeBuilder();

            codeBuilder.AddLine("var dbClass = function (isolationLevel) {");
            codeBuilder.IncreaseIndent();
            codeBuilder.AddLine("var Sequelize = require('sequelize');");
            codeBuilder.AddLine("var config = require('./" + rootDesigner.GetDataContextName() + "Config.CodeGen.js');");
            codeBuilder.AddLine("config.settings.dialectOptions.isolationLevel = isolationLevel;");
            codeBuilder.AddLine("config.settings.dialectOptions.connectionIsolationLevel = isolationLevel;");
            codeBuilder.AddLine("var sequelize = new Sequelize(config.dbName, config.userName, config.password, config.settings);");
            codeBuilder.AddLine();
            codeBuilder.AddLine("var db = { sequelize: sequelize, Sequelize: Sequelize };");
            codeBuilder.AddLine();
            foreach (var classType in modelClasses)
            {
                codeBuilder.AddLine("db." + classType.Name + " = sequelize.import('../" + BusinessDataModelDesignerRoot.EntityFolderName + "/" + classType.Name + ".CodeGen.js');");
            }
            codeBuilder.AddLine();

            var bView = modelClasses.FirstOrDefault(e => e.Kind == ClassKind.ModelView);
            if (bView != null)
            {
                codeBuilder.AddLine("//Manipulating the internal query mechanism");
                codeBuilder.AddLine("//--Save the original version of query selection");
                codeBuilder.AddLine("var entityDefinition = db." + bView.Name + ";");
                codeBuilder.AddLine("entityDefinition.QueryInterface.QueryGenerator.originalSelectQuery =  entityDefinition.QueryInterface.QueryGenerator.selectQuery;");
                codeBuilder.AddLine("//--Change query selection for the business view");
                codeBuilder.AddLine("entityDefinition.QueryInterface.QueryGenerator.selectQuery = function (tableName, options, model) {");
                codeBuilder.AddLine("    var query = options.model.QueryInterface.QueryGenerator.originalSelectQuery(tableName, options, model);");
                codeBuilder.AddLine("    if (typeof options.model.getQueryDefinition === 'function') {");
                codeBuilder.AddLine("         var queryView = options.model.getQueryDefinition();");
                codeBuilder.AddLine("         var quoteTableReplace = options.model.QueryInterface.QueryGenerator.quoteTable(tableName)");
                codeBuilder.AddLine("         query = query.replace(quoteTableReplace, queryView);");
                codeBuilder.AddLine("    }");
                codeBuilder.AddLine("    return query;");
                codeBuilder.AddLine("};");

            }

            codeBuilder.AddLine("//Navigations");
            foreach (var classType in modelClasses)
            {
                classType.GetForeignKeyProperties(models, codeBuilder);
                classType.GetForeignKeyCollecions(models, codeBuilder);
            }

            codeBuilder.AddLine("return db;");
            codeBuilder.DecreaseIndent();
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("module.exports = dbClass;");

            return codeBuilder.GetBody();
        }

        private static string GetContextConfig(BusinessDataModelDesignerRoot rootDesigner)
        {
            Provider defaultProvider = rootDesigner.GetDefaultProvider();
            string dialect = "";
            switch (defaultProvider)
            {
                case Provider.SQLServer:
                    dialect = "mssql";
                    break;
                case Provider.MySQL:
                    dialect = "mysql";
                    break;
                case Provider.SQLite:
                    dialect = "sqlite";
                    break;
                case Provider.PostgreSQL:
                    dialect = "postgres";
                    break;
                default:
                    break;
            }

            string connectionString = rootDesigner.GetConfigConnectionString();
            string host = (defaultProvider != Provider.SQLite ? "'" + connectionString.Extract("Data Source=", ";") + "'" : "null");
            string instanceName = "null";

            if (defaultProvider == Provider.SQLServer && host.Contains("\\"))
            {
                instanceName = "'" + host.Right("\\");
                host = host.Left("\\") + "'";
            }

            string dbName = connectionString.Extract("Initial Catalog=", ";");
            dbName = (dbName.IsNullOrEmpty() ? "null" : "'" + dbName + "'");
            bool integrated = connectionString.Extract("Integrated Security=", ";") == "SSPI";
            string storage = (defaultProvider == Provider.SQLite ? "'" + connectionString.Extract("Data Source=", ";").Replace("\\", "/") + "'" : "null");

            string userName = connectionString.Extract("User ID=", ";");
            userName = (integrated || userName.IsNullOrEmpty() ? "null" : "'" + userName + "'");
            string password = connectionString.Extract("Password=", ";");
            password = (integrated || password.IsNullOrEmpty() ? "null" : "'" + password + "'");

            Linx.Tools.CodeBuilder codeBuilder = new Tools.CodeBuilder();

            codeBuilder.AddLine("module.exports = {");
            codeBuilder.AddLine("   dbName: " + dbName + ",");
            codeBuilder.AddLine("   userName: " + userName + ",");
            codeBuilder.AddLine("   password: " + password + ",");
            codeBuilder.AddLine("   settings: {");
            codeBuilder.AddLine("       /*logging: function (cmd) { console.log(cmd); },*/");
            codeBuilder.AddLine("       dialect: '" + dialect + "',");
            codeBuilder.AddLine("       pool: {");
            codeBuilder.AddLine("           max: 5,");
            codeBuilder.AddLine("           min: 0,");
            codeBuilder.AddLine("           idle: 50000");
            codeBuilder.AddLine("       },");
            codeBuilder.AddLine("       storage: " + storage + ",");
            codeBuilder.AddLine("       host: " + host + ",");
            //timeout: 100000
            codeBuilder.AddLine("       port: '',");
            codeBuilder.AddLine("       dialectOptions: {");
            if (defaultProvider == Provider.SQLServer)
            {
                codeBuilder.AddLine("        	instanceName: " + instanceName + ",");
            }
            codeBuilder.AddLine("        	connectTimeout: 10000,");
            codeBuilder.AddLine("        	requestTimeout: 50000");
            codeBuilder.AddLine("       }");
            codeBuilder.AddLine("  }");
            codeBuilder.AddLine("};");

            return codeBuilder.GetBody();
        }

        private static string GetEntityCustomCode(BusinessDataModelDesignerRoot rootDesigner, ClassOperation op)
        {
            Linx.Tools.CodeBuilder codeBuilder = new Tools.CodeBuilder();
            codeBuilder.AddLine("module.exports = function () {");

            codeBuilder.IncreaseIndent();
            codeBuilder.AddLine("return {");
            codeBuilder.IncreaseIndent();
            codeBuilder.AddLine(op.Name + ": function (" + String.Join(", ", op.Parameters.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries).Select(e => e.Trim().Right(" ").Trim())) + ") {");
            codeBuilder.AddLine();
            codeBuilder.AddLine("}");
            codeBuilder.DecreaseIndent();
            codeBuilder.AddLine("};");
            codeBuilder.DecreaseIndent();

            codeBuilder.AddLine("};");

            return codeBuilder.GetBody();
        }

        private static string GetEntityCode(BusinessDataModelDesignerRoot rootDesigner, ModelClass classType, List<ModelClass> modelClasses)
        {
            Linx.Tools.CodeBuilder codeBuilder = new Tools.CodeBuilder();

            List<string> ops = new List<string>();
            string customFolderName = rootDesigner.GetDataEntitytCustomFolderName(classType.Name);
            foreach (var clsType in modelClasses.Where(e => e.Name == classType.Name))
            {
                foreach (var op in clsType.Operations)
                {
                    if (!ops.Contains(op.Name))
                    {
                        codeBuilder.AddLine("var _" + op.Name + " = require('./" + customFolderName + "/" + op.Name + ".js')();");
                        ops.Add(op.Name);
                    }
                }
            }


            codeBuilder.AddLine("module.exports = function(sequelize, DataTypes) {");
            Dictionary<string, string> modelViewMaps = new Dictionary<string, string>();

            if (classType.Kind == ClassKind.ModelView && !classType.ModelViewDbSets.IsNullOrEmpty())
            {
                string updTables = "'" + classType.ModelViewDbSets.Replace(" ", "").Replace(",", "', '") + "'";
                codeBuilder.AddLine("	var tablesMap = [" + updTables + "];");
                string tableName = "", propName;
                var properties = classType.GetAllAttributes().Where(e => e.ModelViewSource.Left("(").Occurs(".") == 1 && updTables.Contains("'" + e.ModelViewSource.Left(".") + "'")).OrderBy(e => e.ModelViewSource.Left("(")).ToArray();

                if (properties.Length > 0)
                {
                    foreach (var prop in properties)
                    {
                        propName = prop.ModelViewSource.Left("(").Right(".");
                        if (prop.ModelViewSource.Left(".") != tableName)
                        {
                            tableName = prop.ModelViewSource.Left(".");
                            codeBuilder.AddLine("	var _" + tableName + " = sequelize.import('./" + tableName + ".CodeGen.js');");
                            modelViewMaps.Add(tableName, "");
                        }

                        modelViewMaps[tableName] += (modelViewMaps[tableName].IsNullOrEmpty() ? "" : ", ") + propName + ": \"" + prop.Name + "\"";
                    }

                    codeBuilder.AddLine("	var propMap = {");
                    string separator = "";
                    foreach (var map in modelViewMaps)
                    {
                        codeBuilder.AddLine("	    " + separator + map.Key + ": {" + map.Value + "}");
                        if (separator.IsNullOrEmpty())
                            separator = ", ";
                    }
                    codeBuilder.AddLine("	};");
                }

            }


            codeBuilder.AddLine("	var entityDefinition = sequelize.define('" + classType.Name + "', {");

            for (int idxA = 0; idxA < classType.Attributes.Count; idxA++)
            {
                ModelAttribute attribute = classType.Attributes[idxA];

                if (attribute.InStudy)
                    continue;

                var domainValues = attribute.GetDomainValues();
                var lookupInfo = attribute.GetAllLookUpInfo(modelClasses);
                if (!lookupInfo.IsNullOrEmpty())
                { }

                codeBuilder.AddLine("		" + attribute.Name + ": {");

                if (domainValues.IsNullOrEmpty())
                {
                    codeBuilder.AddLine("			type: DataTypes." + attribute.ColumnType + ",");
                }
                else
                {
                    codeBuilder.AddLine("			type: DataTypes.ENUM(" + domainValues + "),");
                }

                codeBuilder.AddLine("			primaryKey: " + (attribute.IsPrimaryKey && !attribute.IsNotMapped() && !classType.NotMapped).ToString().ToLower() + ",");

                codeBuilder.AddLine("			field: '" + attribute.GetColumnName() + "',");

                codeBuilder.AddLine("			autoIncrement: " + ((attribute.IsPrimaryKey || attribute.IsIdentityDB()) && (attribute.IsIdentity && attribute.ForeignKey.IsNullOrEmpty())).ToString().ToLower() + ",");

                codeBuilder.AddLine("			allowNull: " + (attribute.IsNullable || rootDesigner.RemoveRequiredAttributes || attribute.IsNotMapped() || classType.NotMapped).ToString().ToLower() + ",");
                if (!attribute.DefaultValue.IsNullOrEmpty())
                    codeBuilder.AddLine("			defaultValue: " + attribute.DefaultValue + ",");

                codeBuilder.AddLine("			validate: {");
                if (attribute.DataType == ModelDataType.String || attribute.DataType == ModelDataType.StringChar)
                    codeBuilder.AddLine("				len: [1, " + attribute.MaxLength + "]");
                codeBuilder.AddLine("			}");
                codeBuilder.AddLine("		}" + (idxA < (classType.Attributes.Count - 1) ? "," : ""));

            }

            codeBuilder.AddLine("	},");
            codeBuilder.AddLine("	{");

            codeBuilder.AddLine("		classMethods: {");


            //Get Class Custom Methods  
            var sepr = "  ";
            foreach (var op in ops)
            {
                codeBuilder.AddLine("		    " + sepr + op + ": _" + op + "." + op);
                if (sepr == "  ") sepr = ", ";
            }


            if (classType.Kind == ClassKind.ModelView)
            {
                codeBuilder.AddLine("		    " + sepr + "getQueryDefinition: function() {");
                codeBuilder.AddLine(classType.GetBusinessViewLinqDefinition("		        ", rootDesigner, (rootDesigner.GetDefaultProvider() == Provider.PostgreSQL ? "\"" : "")));
                codeBuilder.AddLine("		    }");


                codeBuilder.AddLine("		    , create: function (dataView) {");
                codeBuilder.AddLine("		        return new Promise(function (success, reject) {");

                if (modelViewMaps.Count > 0)
                {

                    codeBuilder.AddLine("		           function createTable (idx) {");
                    codeBuilder.AddLine("		                var tableName = tablesMap[idx];");
                    codeBuilder.AddLine("		                var pm = propMap[tableName];");
                    codeBuilder.AddLine("		                var body = {};");
                    codeBuilder.AddLine("		                for (var propName in pm) {");
                    codeBuilder.AddLine("		                    body[propName] = dataView[pm[propName]]");
                    codeBuilder.AddLine("		                }");
                    codeBuilder.AddLine("		                //Create table");
                    codeBuilder.AddLine("		                var tableClass = eval('_' + tableName);");
                    codeBuilder.AddLine("		                tableClass.create(body).then(function (data) {");
                    codeBuilder.AddLine("		                    //Restore data	");
                    codeBuilder.AddLine("		                    for (var propName in pm) {");
                    codeBuilder.AddLine("		                      dataView[pm[propName]] = data[propName];");
                    codeBuilder.AddLine("		                    }");
                    codeBuilder.AddLine("		                    if (tablesMap.length == (idx + 1)) success(dataView); else createTable(idx + 1);");
                    codeBuilder.AddLine("		                    return true;");
                    codeBuilder.AddLine("		                }");
                    codeBuilder.AddLine("		                , reject);");
                    codeBuilder.AddLine("		           };");
                    codeBuilder.AddLine("		           createTable(0);");
                }
                else
                    codeBuilder.AddLine("		           reject(new Error('[" + classType.Name + "] is read only!'));");


                codeBuilder.AddLine("		        });");
                codeBuilder.AddLine("		    }");



            }
            codeBuilder.AddLine("		},");
            codeBuilder.AddLine("		instanceMethods: {");

            if (classType.Kind == ClassKind.ModelView)
            {
                codeBuilder.AddLine("		    update: function (dataView) {");
                if (modelViewMaps.Count > 0) codeBuilder.AddLine("		        var self = this;");
                codeBuilder.AddLine("		        return new Promise(function (success, reject) {");


                if (modelViewMaps.Count > 0)
                {
                    codeBuilder.AddLine("		           function updateTable (idx) {");
                    codeBuilder.AddLine("		                var tableName = tablesMap[idx];");
                    codeBuilder.AddLine("		                var pm = propMap[tableName];");
                    codeBuilder.AddLine("		                var body = {}; ");
                    codeBuilder.AddLine("		                var targetValues = {};");
                    codeBuilder.AddLine("		                for (var propName in pm) {");
                    codeBuilder.AddLine("		                    body[propName] = dataView[pm[propName]];");
                    codeBuilder.AddLine("		                    targetValues[propName] = self[pm[propName]];");
                    codeBuilder.AddLine("		                }");
                    codeBuilder.AddLine("		                //Create entity in memory");
                    codeBuilder.AddLine("		                var tableClass = eval('_' + tableName);");
                    codeBuilder.AddLine("		                var target = tableClass.build(targetValues, { isNewRecord: false });");
                    codeBuilder.AddLine("		                //Set with no changes");
                    codeBuilder.AddLine("		                for (var propName in pm) {");
                    codeBuilder.AddLine("		                    target.changed(propName, false);");
                    codeBuilder.AddLine("		                }");
                    codeBuilder.AddLine("		                //Update origin");
                    codeBuilder.AddLine("		                target.update(body).then(function () {");
                    codeBuilder.AddLine("		                    //Restore data");
                    codeBuilder.AddLine("		                    for (var propName in pm) {");
                    codeBuilder.AddLine("		                        self[pm[propName]] = target[propName];");
                    codeBuilder.AddLine("		                    }");
                    codeBuilder.AddLine("		                    if (tablesMap.length == (idx + 1)) success(); else updateTable(idx + 1);");
                    codeBuilder.AddLine("		                    return true;");
                    codeBuilder.AddLine("		                }).catch(reject);");
                    codeBuilder.AddLine("		           };");
                    codeBuilder.AddLine("		           updateTable(0);");

                }
                else
                    codeBuilder.AddLine("		           reject(new Error('[" + classType.Name + "] is read only!'));");

                codeBuilder.AddLine("		        });");
                codeBuilder.AddLine("		    },");
                codeBuilder.AddLine("		    destroy: function () {");
                if (modelViewMaps.Count > 0) codeBuilder.AddLine("		        var self = this;");
                codeBuilder.AddLine("		        return new Promise(function (success, reject) {");

                if (modelViewMaps.Count > 0)
                {
                    codeBuilder.AddLine("		           function destroyTable (idx) {");
                    codeBuilder.AddLine("		                var tableName = tablesMap[idx];");
                    codeBuilder.AddLine("		                var pm = propMap[tableName];");
                    codeBuilder.AddLine("		                var targetValues = {};");
                    codeBuilder.AddLine("		                for (var propName in pm) {");
                    codeBuilder.AddLine("		                	targetValues[propName] = self[pm[propName]];");
                    codeBuilder.AddLine("		                }");
                    codeBuilder.AddLine("		                //Create entity in memory");
                    codeBuilder.AddLine("		                var tableClass = eval('_' + tableName);");
                    codeBuilder.AddLine("		                var target = tableClass.build(targetValues, { isNewRecord: false });");
                    codeBuilder.AddLine("		                target.destroy().then(function () {");
                    codeBuilder.AddLine("		                    if (idx == 0) success(); else destroyTable(idx - 1);");
                    codeBuilder.AddLine("		                    return true;");
                    codeBuilder.AddLine("		                })");
                    codeBuilder.AddLine("		                .catch(reject);");
                    codeBuilder.AddLine("		           };");
                    codeBuilder.AddLine("		           destroyTable(tablesMap.length - 1);");

                }
                else
                    codeBuilder.AddLine("		           reject(new Error('[" + classType.Name + "] is read only!'));");

                codeBuilder.AddLine("		        });");
                codeBuilder.AddLine("		    }");

            }


            codeBuilder.AddLine("		},");

            codeBuilder.AddLine("		hooks: {");
            codeBuilder.AddLine("		    beforeFind: function (options, fn) {");
            codeBuilder.AddLine("		        fn(null, options);");
            codeBuilder.AddLine("		    }");
            codeBuilder.AddLine("		},");

            codeBuilder.AddLine("		timestamps: false, // don't add the timestamp attributes (updatedAt, createdAt)");
            codeBuilder.AddLine("		freezeTableName: true, // disable the modification of table names");
            codeBuilder.AddLine("		tableName: '" + classType.GetTableName(true) + "', // define the table's name");
            if (rootDesigner.GetDefaultProvider().In(Provider.SQLServer, Provider.PostgreSQL))
                codeBuilder.AddLine("		schema: '" + classType.Schema + "', // define the table's schema");

            codeBuilder.AddLine("		indexes: [ ");

            for (int idx = 0; idx < classType.ModelIndexes.Count; idx++)
            {
                var index = classType.ModelIndexes[idx];
                codeBuilder.AddLine("		    { name: '" + index.Name + "', unique: " + index.IsUnique.ToString().ToLower() + ", fields: [ '" + index.Properties.Replace(",", "','") + "' ] }" + (idx < (classType.ModelIndexes.Count - 1) ? "," : ""));
            }

            codeBuilder.AddLine("		]");


            codeBuilder.AddLine("	});");

            codeBuilder.AddLine("	return entityDefinition;");

            codeBuilder.AddLine("};");

            return codeBuilder.GetBody();
        }


        private static string GetEntityRoute(BusinessDataModelDesignerRoot rootDesigner, ModelClass classType)
        {
            Linx.Tools.CodeBuilder codeBuilder = new Tools.CodeBuilder();
            codeBuilder.AddLine("module.exports = function (express, dbClass, _) {");
            codeBuilder.AddLine("   var router = new express.Router();");


            string pkName = classType.GetPrimaryKeyName();
            if (!pkName.IsNullOrEmpty())
            {
                codeBuilder.AddLine("   router.route('/" + classType.Name + "/:id')");
                codeBuilder.AddLine("   .get(function(request, response) {");
                codeBuilder.AddLine("       var db = new dbClass(1); //READ UNCOMMITED");
                codeBuilder.AddLine("       var id = request.params.id;");
                codeBuilder.AddLine("       var whereDef = { " + pkName + ": id };");
                codeBuilder.AddLine("       db." + classType.Name + ".findOne({ where: whereDef }).then(function (data) {");
                codeBuilder.AddLine("               if (data) {");
                codeBuilder.AddLine("   	            response.json(data);");
                codeBuilder.AddLine("               }");
                codeBuilder.AddLine("               else ");
                codeBuilder.AddLine("   	            response.status(404).json({ alert: '" + classType.Name + "." + pkName + " = ' + id + ' was not found!' });");
                codeBuilder.AddLine("           }");
                codeBuilder.AddLine("       ).catch(function (e) {");
                codeBuilder.AddLine("           response.status(400).json({ message: e.message });");
                codeBuilder.AddLine("       });	");
                codeBuilder.AddLine("   })");
                codeBuilder.AddLine("   .delete(function (request, response) {");
                codeBuilder.AddLine("       var db = new dbClass(2); //READ COMMITED");
                codeBuilder.AddLine("       var id = request.params.id;");

                codeBuilder.AddLine("      if (_.isUndefined(id)) {");
                codeBuilder.AddLine("          return response.status(400).json({ \"error\": \"The primary key was not informed!\" });");
                codeBuilder.AddLine("      }");
                codeBuilder.AddLine("      var data = db." + classType.Name + ".build({ " + pkName + ": id }, { isNewRecord: false });");

                codeBuilder.AddLine("      db.sequelize.transaction({ autocommit: false, isolationLevel: db.Sequelize.Transaction.ISOLATION_LEVELS.READ_COMMITTED }).then(function (t) {");

                codeBuilder.AddLine("           data.destroy().then(function () {");
                codeBuilder.AddLine("               t.commit();");
                codeBuilder.AddLine("               return response.json({ info: 'Element ' + id + ' was deleted!'});");
                codeBuilder.AddLine("           })");
                codeBuilder.AddLine("           .catch(function (e) {");
                codeBuilder.AddLine("               t.rollback();");
                codeBuilder.AddLine("               response.status(400).json({ message: e.message });");
                codeBuilder.AddLine("           });");

                codeBuilder.AddLine("      });");

                codeBuilder.AddLine("   });");
                codeBuilder.AddLine();
            }

            codeBuilder.AddLine("   router.route('/" + classType.Name + "_HELP').get(function(request, response) {");
            codeBuilder.AddLine("      var db = new dbClass(1); //READ UNCOMMITED");
            string entityMeta = "\"" + String.Join("\", \"", classType.Attributes.Where(e => !e.InStudy).Select(e => e.Name + "\": \"" + e.GetDataType())) + "\"";
            string primaryKey = classType.GetPrimaryKeyName();
            if (primaryKey.IsNullOrEmpty())
                primaryKey = "ID_" + classType.Name;
            string strPropName = classType.Attributes.Where(e => !e.InStudy && e.GetDataType().ToLower().Contains("string")).Select(e => e.Name).FirstOrDefault() ?? "DESCRIPTION";
            codeBuilder.AddLine("      response.json({ DataStructure: {" + entityMeta + "}, GET_ById: '/" + classType.Name + "/33', GET_ByExample: '/" + classType.Name + "?" + primaryKey + "=33&" + strPropName + "=%a%', GET_ByOData: '/" + classType.Name + "?$filter=" + primaryKey + " eq 1&$select=" + primaryKey + "," + strPropName + "&$expand=' + Object.keys(db." + classType.Name + ".associations) + '&$inlinecount=allpages&$skip=0&$top=100&$orderby=" + strPropName + "', DELETE: '/" + classType.Name + "/33', POST: '/" + classType.Name + "', PUT: '/" + classType.Name + "'  });");

            codeBuilder.AddLine("   });");
            codeBuilder.AddLine();

            codeBuilder.AddLine("   router.route('/" + classType.Name + "')");
            codeBuilder.AddLine("   .get(function (request, response) {");
            codeBuilder.AddLine("      var db = new dbClass(1); //READ UNCOMMITED");
            codeBuilder.AddLine("      var queryParams = request.query;");
            codeBuilder.AddLine("      var whereDef = '';");
            codeBuilder.AddLine("      var whereParams = [];");
            codeBuilder.AddLine("      var attributes = undefined;");
            codeBuilder.AddLine("      var include = undefined;");
            codeBuilder.AddLine("      var orderby = undefined;");
            codeBuilder.AddLine("      var skip = undefined;");
            codeBuilder.AddLine("      var top = undefined;");
            codeBuilder.AddLine("      var inlinecount = false;");

            codeBuilder.AddLine("      if (queryParams.hasOwnProperty('$filter')) {");
            codeBuilder.AddLine("         whereDef = queryParams.$filter;");
            codeBuilder.AddLine("         whereDef = whereDef.replace(/%20/g, ' ').replace(/%27/g, \"'\").replace(/ eq /g, ' = ').replace(/ ne /g, ' != ').replace(/ gt /g, ' > ').replace(/ ge /g, ' >= ').replace(/ lt /g, ' < ').replace(/ le /g, ' <= ');");
            codeBuilder.AddLine("      }");
            codeBuilder.AddLine();
            codeBuilder.AddLine("      if (queryParams.hasOwnProperty('$select')) {");
            codeBuilder.AddLine("         attributes = [];");
            codeBuilder.AddLine("         queryParams.$select.split(',').forEach(function (e) { attributes.push(e.trim()); });");
            codeBuilder.AddLine("      }");
            codeBuilder.AddLine();
            codeBuilder.AddLine("      if (queryParams.hasOwnProperty('$expand')) {");
            codeBuilder.AddLine("         include = [];");
            codeBuilder.AddLine("         queryParams.$expand.split(',').forEach(function (e) { include.push(db.VENDA.associations[e.trim()]); });");
            codeBuilder.AddLine("      }");
            codeBuilder.AddLine();
            codeBuilder.AddLine("      if (queryParams.hasOwnProperty('$orderby')) {");
            codeBuilder.AddLine("         orderby = [];");
            codeBuilder.AddLine("         queryParams.$orderby.split(',').forEach(function (e) { var parts = e.trim().split(' '); if (parts.length === 1) { parts.push('ASC'); } orderby.push( parts ); });");
            codeBuilder.AddLine("      }");
            codeBuilder.AddLine();
            codeBuilder.AddLine("      if (queryParams.hasOwnProperty('$skip')) {");
            codeBuilder.AddLine("         skip = eval(queryParams.$skip);");
            codeBuilder.AddLine("      }");
            codeBuilder.AddLine();
            codeBuilder.AddLine("      if (queryParams.hasOwnProperty('$top')) {");
            codeBuilder.AddLine("         top = eval(queryParams.$top);");
            codeBuilder.AddLine("      }");
            codeBuilder.AddLine();
            codeBuilder.AddLine("      if (queryParams.hasOwnProperty('$inlinecount')) {");
            codeBuilder.AddLine("         inlinecount = (queryParams.$inlinecount === 'allpages');");
            codeBuilder.AddLine("      }");

            for (int idxA = 0; idxA < classType.Attributes.Count; idxA++)
            {
                ModelAttribute attribute = classType.Attributes[idxA];

                if (attribute.InStudy)
                    continue;

                if (attribute.GetDataType().ToLower().Contains("string"))
                {
                    codeBuilder.AddLine("      if (queryParams.hasOwnProperty('" + attribute.Name + "') && queryParams." + attribute.Name + ".length > 0) {");
                    codeBuilder.AddLine("          whereDef += (whereDef === '' ? '' : ' AND ') + '" + attribute.GetColumnName() + "' + (queryParams." + attribute.Name + ".indexOf('%') < 0 ? ' = ' : ' LIKE ') + '?';");
                    codeBuilder.AddLine("          whereParams.push(queryParams." + attribute.Name + ");");
                    codeBuilder.AddLine("      }");
                }
                else
                {
                    codeBuilder.AddLine("      if (queryParams.hasOwnProperty('" + attribute.Name + "')) {");
                    codeBuilder.AddLine("          whereDef += (whereDef === '' ? '' : ' AND ') + '" + attribute.GetColumnName() + " = ?';");
                    codeBuilder.AddLine("          whereParams.push(queryParams." + attribute.Name + ");");
                    codeBuilder.AddLine("      }");
                }

            }

            codeBuilder.AddLine("      var whereExpr = (whereDef === '' ? undefined : [ whereDef ].concat(whereParams));");
            codeBuilder.AddLine("      db." + classType.Name + ".findAll({ where: whereExpr, attributes: attributes, include: include, offset: skip, limit: top, order: orderby })");
            codeBuilder.AddLine("      .then(function (rows) {");
            codeBuilder.AddLine("          if (!rows || rows.length === 0) {");
            codeBuilder.AddLine("              response.status(404).json( { information: 'No record found!' } );");
            codeBuilder.AddLine("          }");
            codeBuilder.AddLine("          else {");
            codeBuilder.AddLine("              var odataResponse = { \"odata.metadata\": \"http://service/" + rootDesigner.GetDataContextName() + "/$metadata#" + classType.Name + "\" };");
            codeBuilder.AddLine("              if (inlinecount) { odataResponse[\"odata.count\"] = rows.length; }");
            codeBuilder.AddLine("              odataResponse.value = rows;");
            codeBuilder.AddLine("              response.json(odataResponse);");
            codeBuilder.AddLine("          }");
            codeBuilder.AddLine("      })");
            codeBuilder.AddLine("      .catch (function (e) {");
            codeBuilder.AddLine("          response.status(500).json({ message: e.message });");
            codeBuilder.AddLine("      });");
            codeBuilder.AddLine("   })");

            codeBuilder.AddLine("   .post(function (request, response) {");
            codeBuilder.AddLine("      var db = new dbClass(2); //READ COMMITED");
            string propLIst = "'" + String.Join("', '", classType.Attributes.Where(e => !e.InStudy).Select(e => e.Name)) + "'";
            codeBuilder.AddLine("      var body = _.pick(request.body, " + propLIst + ");");

            codeBuilder.AddLine("      db.sequelize.transaction({ autocommit: false, isolationLevel: db.Sequelize.Transaction.ISOLATION_LEVELS.READ_COMMITTED }).then(function (t) {");

            codeBuilder.AddLine("           db." + classType.Name + ".create(body).then(function (data) {");
            codeBuilder.AddLine("               t.commit();");
            codeBuilder.AddLine("               return response.json(data);");
            codeBuilder.AddLine("           }");
            codeBuilder.AddLine("           , function (e) {");
            codeBuilder.AddLine("               t.rollback();");
            codeBuilder.AddLine("               response.status(400).json({ message: e.message });");
            codeBuilder.AddLine("           });");

            codeBuilder.AddLine("      });");

            codeBuilder.AddLine("   })");
            if (!pkName.IsNullOrEmpty())
            {
                codeBuilder.AddLine("   .put(function (request, response) {");
                codeBuilder.AddLine("      var db = new dbClass(2); //READ COMMITED");
                codeBuilder.AddLine("      var body = _.pick(request.body, " + propLIst + ");");

                codeBuilder.AddLine("      if (_.isUndefined(body." + pkName + ")) {");
                codeBuilder.AddLine("          return response.status(400).json({ \"error\": \"The property " + pkName + " was not informed!\" });");
                codeBuilder.AddLine("      }");

                codeBuilder.AddLine("      var id = body." + pkName + ";");
                codeBuilder.AddLine("      var whereDef = { " + pkName + ": id };");
                codeBuilder.AddLine("      db." + classType.Name + ".findOne({ where: whereDef }).then(function (data) {");
                codeBuilder.AddLine("              if (data) {");

                codeBuilder.AddLine("                db.sequelize.transaction({ autocommit: false, isolationLevel: db.Sequelize.Transaction.ISOLATION_LEVELS.READ_COMMITTED }).then(function (t) {");

                codeBuilder.AddLine("   	            data.update(body).then(function () {");
                codeBuilder.AddLine("   	                t.commit();");
                codeBuilder.AddLine("   	                response.json(data);");
                codeBuilder.AddLine("   	            })");
                codeBuilder.AddLine("   	            .catch(function (e) {");
                codeBuilder.AddLine("   	                t.rollback();");
                codeBuilder.AddLine("   	                response.status(400).json({ message: e.message });");
                codeBuilder.AddLine("   	            });");

                codeBuilder.AddLine("                });");

                codeBuilder.AddLine("              }");
                codeBuilder.AddLine("              else ");
                codeBuilder.AddLine("   	            response.status(404).json({ alert: '" + classType.Name + "." + pkName + " = ' + id + ' was not found!' });");
                codeBuilder.AddLine("              return true;");
                codeBuilder.AddLine("        }");
                codeBuilder.AddLine("      ).catch(function (e) {");
                codeBuilder.AddLine("          response.status(400).json({ message: e.message });");
                codeBuilder.AddLine("      });	");
                codeBuilder.AddLine("   })");
            }
            codeBuilder.AddLine(";");

            codeBuilder.AddLine();
            codeBuilder.AddLine("   return router;");
            codeBuilder.AddLine("};");

            return codeBuilder.GetBody();
        }

    }
}
