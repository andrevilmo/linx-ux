using EnvDTE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linx.Tools;
using Linx.Builder.Resources;
using System.Text.RegularExpressions;
using System.IO;
using System.Windows.Forms;
using Linx.EntityAdapterDesigner.CustomizedCode.Util;

namespace Linx.EntityAdapterDesigner.CustomizedCode.Apps.ClientErp
{
    public class ClientErpCodeGen
    {

        private const string patternForDateTimeConstructor = @"^new(\s+)DateTime(\s*)\(\d{4},(\s*)\d{2},(\s*)\d{2}\)$";
        private const string patternForLinxParameter = @"^\[(\w+)\]$";


        private EntityAdapterDesignerRoot _designerRoot;
        public ClientErpCodeGen(EntityAdapterDesignerRoot designerRoot)
        {
            _designerRoot = designerRoot;
        }

        #region ClientErp project Functions

        public static string GetClientErpName(Project project)
        {
            return project.Name + ".ClientErp";
        }


        #endregion


        #region ClientErp MVVM Core

        #endregion

        #region ClientErp Core

        /// <summary>
        /// Generating Model code.
        /// </summary>
        /// <param name="api"></param>
        /// <param name="codeBuilder"></param>
        /// <param name="contextName"></param>
        public void GenerateClientErpDataServiceApiCode(WebApiController api, Linx.Tools.CodeBuilder codeBuilder, string contextName)
        {
            string apiName = api.Name;
            string appName = this._designerRoot.GetAppName();
            string entitiesRef = "dataView";


            codeBuilder.IncreaseIndent();
            codeBuilder.AddLine("/* jshint ignore:start */");
            codeBuilder.AddLine("'use strict';");
            codeBuilder.AddLine();

            codeBuilder.AddLine("var name = '" + appName + "_" + contextName + "';");

            codeBuilder.AddLine();
            codeBuilder.AddLine("var dependencies = [");
            codeBuilder.AddLine("        '$log',");
            codeBuilder.AddLine("        'UUIDFactory',");
            codeBuilder.AddLine("        'httpFactory',");
            codeBuilder.AddLine("        'commonFactory',");
            codeBuilder.AddLine("        'dialogFactory',");
            codeBuilder.AddLine("        'messengeFactory',");
            codeBuilder.AddLine("        'shellManagerService',");
            codeBuilder.AddLine("        '" + appName + "_ClientErpDataDomainsFactory'");

            codeBuilder.AddLine("];");
            codeBuilder.AddLine();

            codeBuilder.AddLine("var serviceAPI = function ($log, uuid, httpFactory, common, dialog, messenger, shellManagerService, dataDomains) {");
            codeBuilder.AddLine("   var ctrContext = function () { return new dataContextConstructor($log, uuid, httpFactory, common, dialog, messenger, shellManagerService, dataDomains); };");
            codeBuilder.AddLine("   return ctrContext;");
            codeBuilder.AddLine("}");

            codeBuilder.AddLine("var dataContextConstructor = function ($log, uuid, httpFactory, common, dialog, messenger, shellManagerService, dataDomains) {");

            codeBuilder.IncreaseIndent();
            codeBuilder.AddLine("var getServiceAddress = function(apiPart) {");
            codeBuilder.AddLine("   var serviceBus = shellManagerService.getServiceUrl(apiPart, businessAssemblyName);");
            codeBuilder.AddLine("   return serviceBus;");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("var getNewGuid = function() {");
            codeBuilder.AddLine("   return uuid.newguid();");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("var getDataFeedUrl = function() {");
            codeBuilder.AddLine("   var baseApi = getDataServiceUrl();");
            codeBuilder.AddLine("   return common.strLeft(baseApi, baseApi.length - 1) + 'OData/';");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("var getDataServiceUrl = function (reset) {");
            codeBuilder.AddLine("   var baseApi = (!reset && dataService && !common.isNullOrEmpty(dataService.serviceName) ? dataService.serviceName : getServiceAddress(controllerName));");
            codeBuilder.AddLine("   return baseApi + (common.strRight(baseApi, 1) == '/' ? '' : '/');");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("var setServiceBusUrl = function (url) {");
            codeBuilder.AddLine("   if (dataService) { dataService.serviceName = (common.isNullOrEmpty(url) ? getDataServiceUrl(true) : url + (common.strRight(url, 1) == '/' ? '' : '/') + controllerName + '/'); }");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("var initializePOCO = function(ownerReference, entityName) {");
            codeBuilder.AddLine("   if (ownerReference) { eval(entityName + 'Initializer(ownerReference);'); }");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("var getPivotLayouts = function (params, success, error) {");
            codeBuilder.AddLine("   return httpFactory.httpGet(getDataServiceUrl, getServiceAddress('linxframeworkobjeto') + '/GetPivotLayouts?' +");
            codeBuilder.AddLine("                                                               'rootNameSpace=' + params.rootNamespace +");
            codeBuilder.AddLine("                                                               '&viewName=' + params.viewName +");
            codeBuilder.AddLine("                                                               '&pivotName=' + params.pivotName +");
            codeBuilder.AddLine("                                                               '&pivotDataSource=' + params.pivotDataSource, success, error);");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("var getSelectedLayoutContent = function (params, success, error) {");
            codeBuilder.AddLine("    return httpFactory.httpGet(getDataServiceUrl, getServiceAddress('linxframeworkobjeto') + '/GetPivotLayout?uidObjetoConteudo=' + params.uidObjetoConteudo, success, error);");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("var businessAssemblyName = '" + _designerRoot.GetAssemblyName() + "';");
            codeBuilder.AddLine("var controllerName = '" + api.GetRoutePrefix() + "';");
            codeBuilder.AddLine("var dataService = { serviceName: getDataServiceUrl(true) /*WebApi Service Address*/ };");

            codeBuilder.AddLine("var enableChangeTrack = true;");
            codeBuilder.AddLine("var entityPropChanged = function(entity, propName, oldVal, newVal) {");
            codeBuilder.AddLine("    if (!enableChangeTrack) return true;");
            codeBuilder.AddLine("    var result = true;");
            codeBuilder.AddLine("    if ((typeof entity.OnPropertyChanged == 'function') && oldVal !== newVal)");
            codeBuilder.AddLine("        result = (entity.OnPropertyChanged(propName, oldVal, newVal) !== false);");
            codeBuilder.AddLine("    if (result && ['U', 'I', 'D'].indexOf(entity.ChangeState) < 0) { entity.createOriginal(propName, oldVal); entity.ChangeState = 'U'; if (entity.setParentAsModified) entity.setParentAsModified(); }");
            codeBuilder.AddLine("    return result;");
            codeBuilder.AddLine("}");

            codeBuilder.AddLine("//#region Metadata Info");
            var propNames = GetJsonMetadata(codeBuilder);
            codeBuilder.AddLine("var lookUpProperties = [];");
            codeBuilder.AddLine("//#endregion Metadata Info");

            string parameterNames = string.Join(",", _designerRoot.GetUsedParameterNames());
            #region the Class that manipulate parameters
            codeBuilder.AddLine("//#region dataParameters");
            codeBuilder.AddLine("var dataParameters = {");
            codeBuilder.AddLine("    isLoaded: false,");
            codeBuilder.AddLine("    parameters: [],");
            codeBuilder.AddLine("    registerParameters: function (parameterList, complete) {");
            codeBuilder.AddLine("        if (parameterList !== '') {");
            codeBuilder.AddLine("            var variation = '{TBC_GRUPO_ECONOMICO|' + shellManagerService.getEconomicGroupId().toString() + '|TCS_USUARIO|' + shellManagerService.getUserUid() + (dataBusiness != null && dataBusiness.getBandeiraRede() > 0 ? '|TBC_BANDEIRA_REDE|' + dataBusiness.getBandeiraRede().toString() : '') + '}';");
            codeBuilder.AddLine("            var error = function (gEr) {");
            codeBuilder.AddLine("                var msg = 'Os seguintes parâmetros não foram pesquisados: [' + parameterList + ']';");
            codeBuilder.AddLine("                dialog.showAlert(msg, 'Alerta');");
            codeBuilder.AddLine("                dataParameters.isLoaded = true;");
            codeBuilder.AddLine("            };");
            codeBuilder.AddLine("            var success = function (data) {");
            codeBuilder.AddLine("                var parametersName = '';");
            codeBuilder.AddLine("                var parameters = data.split('#');");
            codeBuilder.AddLine("                for (var idx in parameters) {");
            codeBuilder.AddLine("                    var values = parameters[idx].split('|');");
            codeBuilder.AddLine("                    var pName = values[0];");
            codeBuilder.AddLine("                    var pValue = values[1];");
            codeBuilder.AddLine("                    dataParameters.parameters[pName] = pValue;");
            codeBuilder.AddLine("                }");
            codeBuilder.AddLine("                dataParameters.isLoaded = true;");
            codeBuilder.AddLine("                if (complete) complete();");
            codeBuilder.AddLine("            };");
            codeBuilder.AddLine("            httpFactory.httpGet(getDataServiceUrl, getServiceAddress('LinxFrameworkParametro') + '/GetParameterValue?serializedParameterList=' + common.stringReplace(parameterList, '{}', variation), success, error);");
            codeBuilder.AddLine("        }");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("//#endregion dataParameters");
            #endregion the Class that manipulate parameters

            codeBuilder.AddLine("//#region Classes Map");
            codeBuilder.AddLine("var sequences = [];");
            codeBuilder.AddLine("var resetSequence = function(entityName) {");
            codeBuilder.AddLine("    sequences[entityName] = 0;");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var getSequence = function(entityName) {");
            codeBuilder.AddLine("    if ((typeof sequences[entityName]) === 'undefined') resetSequence(entityName);");
            codeBuilder.AddLine("    return (++sequences[entityName]);");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("//#region Classes Map");
            codeBuilder.AddLine("var sequences = [];");
            codeBuilder.AddLine("var getNextSequence = function(entityName) {");
            codeBuilder.AddLine("    if (!sequences[entityName]) resetSequence(entityName);");
            codeBuilder.AddLine("    var sequence = sequences[entityName];");
            codeBuilder.AddLine("    sequences[entityName]++;");
            codeBuilder.AddLine("    return sequence;");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var resetSequence = function(entityName) {");
            codeBuilder.AddLine("    sequences[entityName] = 0;");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var getSequence = function(entityName) {");
            codeBuilder.AddLine("    if (!sequences[entityName]) resetSequence(entityName);");
            codeBuilder.AddLine("    return sequences[entityName];");
            codeBuilder.AddLine("};");
            //Verify Validators and DataType

            //Metadata
            Action<EntityAdapter> createMetadata = null;
            Dictionary<string, List<string>> entityKeysForDefault = new Dictionary<string, List<string>>();

            createMetadata = (entity) =>
            {
                var topParent = entity.GetTopParent();
                var details = entity.GetAllInheritanceSourceEntityAdapters();
                bool hasIdentity = entity.HasIdentityPrimaryKey();
                entityKeysForDefault.Add(entity.Name, new List<string>());

                bool hasDynamicPK = entity.HasDynamicPrimaryKey();
                if (hasDynamicPK)
                {
                    entityKeysForDefault[entity.Name].Add("EntityUniqueKey:Guid");
                }

                var classDomainExtenders = new List<EntityAdapterAttribute>();
                bool hasLookUps = false;
                string classProperties = String.Empty, queryRequiredProperties = String.Empty;
                string lookUpPropertiesDef = "";
                var lookUpsPropInfo = entity.GetAllLookUpPropertiesInfo(true);
                string keyFilterForRefreshing = String.Empty;
                Action<EntityAdapterAttribute, bool> genPropertyDefinitions = (prop, removePartOfKey) =>
                {
                    classProperties += (classProperties.IsNullOrEmpty() ? "" : ", ") + "'" + prop.Name + "'";
                    if (prop is EntityAdapterProperty && ((EntityAdapterProperty)prop).IsRequiredBeforeSearching)
                        queryRequiredProperties += (queryRequiredProperties.IsNullOrEmpty() ? "" : ",") + prop.Name + ": '" + prop.DisplayName + "'";

                    ///////////////////
                    bool isPK = !removePartOfKey && (!hasDynamicPK && (entity.IsIndependentKey(prop) || ((prop is EntityAdapterProperty) && entity.IsPrimaryKey(((EntityAdapterProperty)prop)))));

                    if (isPK)
                    {
                        keyFilterForRefreshing += (keyFilterForRefreshing.IsNullOrEmpty() ? "'" : " + ';") + prop.Name + "#==#" + Linx.Tools.EntitySearch.ParseJDataType(prop.Datatype) + "' + ownerReference." + prop.Name + ".toString()";
                        entityKeysForDefault[entity.Name].Add(prop.Name + ":" + prop.Datatype);
                    }
                    
                    if (!prop.DomainName.IsNullOrEmpty())
                    {
                        classDomainExtenders.Add(prop);
                    }

                    if (lookUpsPropInfo.ContainsKey(prop.Name))
                    {
                        hasLookUps = true;
                        lookUpPropertiesDef += (lookUpPropertiesDef.IsNullOrEmpty() ? "" : ", ") + prop.Name + ": '" + LookUpAdapter.GetLookUpName(lookUpsPropInfo[prop.Name]) + "'";
                    }
                };
                foreach (var attribute in entity.GetAllInheritanceAttributes())
                {
                    genPropertyDefinitions(attribute, false);
                }
                foreach (var attribute in entity.GetExtraParentRelationKey())
                {
                    genPropertyDefinitions(attribute, true);
                }
                if (!keyFilterForRefreshing.IsNullOrEmpty())
                {
                    keyFilterForRefreshing = "var filterByKey = '" + entity.Name + "{' + " + keyFilterForRefreshing + " + '}';";
                }

                lookUpPropertiesDef = "lookUpProperties['" + entity.Name + "'] = {" + lookUpPropertiesDef + "};";
                //Lookup Properties
                codeBuilder.AddLine(lookUpPropertiesDef);

                //Add Extenders
                codeBuilder.AddLine("var " + entity.Name + "Initializer = function (ownerReference) {");

                codeBuilder.AddLine("   ownerReference.RowDataId = getNextSequence('" + entity.Name + "');");

                if (!entity.IsReadOnly)
                {
                    var dtProperties = entity.GetAllInheritanceAttributes().Where(e => e.Datatype.ToLower().Contains("datetime")).ToArray();
                    if (propNames.ContainsKey(entity.Name))
                    {
                        codeBuilder.AddLine("    //Start Property Definitions");
                        foreach (var prop in propNames[entity.Name])
                        {
                            bool isDT = (dtProperties.FirstOrDefault(e => e.Name == prop) != null);
                            string privName = "_" + prop.ToCamelCase();
                            codeBuilder.AddLine("    var " + privName + " = " + (isDT ? "(ownerReference." + prop + " === null ? null : new Date(" : "") + "ownerReference." + prop + (isDT ? "))" : "") + ";");
                            codeBuilder.AddLine("    Object.defineProperty(ownerReference, '" + prop + "', {");
                            codeBuilder.AddLine("      get: function() { return " + privName + "; },");

                            string domainSetter = "";
                            var domProp = classDomainExtenders.FirstOrDefault(e => e.Name == prop);
                            if (domProp != null)
                            {
                                domainSetter = " else { " + privName + "Name = (dataDomains.getName('" + domProp.DomainName + "', newValue)); }";
                            }
                            else
                            {
                                domProp = classDomainExtenders.FirstOrDefault(e => (e.Name + "Name") == prop);
                                if (domProp != null)
                                {
                                    domainSetter = " else { " + privName.Left(privName.Length - 4) + " = (dataDomains.getId('" + domProp.DomainName + "', newValue)); }";
                                }
                            }
                                                        
                            codeBuilder.AddLine("      set: function(newValue) { var oldValue = " + privName + "; " + privName + " = newValue; if (!entityPropChanged(ownerReference, '" + prop + "', oldValue, newValue)) { " + privName + " = oldValue; }" + domainSetter + " }");
                            codeBuilder.AddLine("    });");
                        }
                        codeBuilder.AddLine("    //End Property Definitions");
                    }
                }

                foreach (var detail in details)
                {
                    codeBuilder.AddLine("   ownerReference.current" + detail.Name + " = null;");
                }

                codeBuilder.AddLine("   ownerReference.setRemovedLookupFields = function(removedFields) {");
                codeBuilder.AddLine("       for (var idxLUp in entitylookUps[ownerReference.typeName]) {");
                codeBuilder.AddLine("           var hasKeyValue = false;");
                codeBuilder.AddLine("           var luName = entitylookUps[ownerReference.typeName][idxLUp];");
                codeBuilder.AddLine("           var luMeta = metadataInfo[luName];");
                codeBuilder.AddLine("           for (var idxProp in luMeta) {");
                codeBuilder.AddLine("               var prop = luMeta[idxProp];");
                codeBuilder.AddLine("               if (!common.isNullOrEmpty(prop.relatedKey) && prop.isPartOfKey) {");
                codeBuilder.AddLine("                   hasKeyValue = !common.isNullOrEmpty(ownerReference[prop.relatedKey]);");
                codeBuilder.AddLine("                   break;");
                codeBuilder.AddLine("               }");
                codeBuilder.AddLine("           }");
                codeBuilder.AddLine("           if (hasKeyValue) {");
                codeBuilder.AddLine("               for (var idxProp in luMeta) {");
                codeBuilder.AddLine("                   var prop = luMeta[idxProp];");
                codeBuilder.AddLine("                   if (!common.isNullOrEmpty(prop.relatedKey) && !prop.isPartOfKey) {");
                codeBuilder.AddLine("                       removedFields.push(prop.relatedKey);");
                codeBuilder.AddLine("                   }");
                codeBuilder.AddLine("               }");
                codeBuilder.AddLine("           }");
                codeBuilder.AddLine("       }");
                codeBuilder.AddLine("   }");

                codeBuilder.AddLine("   ownerReference.getJExpression = function(listFilterRange, removedFields, noDetails) {");
                codeBuilder.AddLine("       if (ownerReference.excludedFilters && ownerReference.excludedFilters.length > 0) { if (removedFields instanceof Array) removedFields = removedFields.concat(ownerReference.excludedFilters); else removedFields = ownerReference.excludedFilters; }");
                if (entity.EnableLookupOptimizationForQBE && !entity.IsDashboardFilter)
                    codeBuilder.AddLine("       ownerReference.setRemovedLookupFields(removedFields);");
                codeBuilder.AddLine("       var jExpression = common.getJEntityExpression(ownerReference, dialog, listFilterRange, removedFields);");
                codeBuilder.AddLine("       if (jExpression === 'Error') return jExpression;");
                //Getting jExpression from Details
                foreach (var detail in details.Where(e => e.EnableQBE))
                {
                    var parentLink = detail.GetParentLinkRelation();
                    codeBuilder.AddLine("       if (noDetails !== true && ownerReference." + (detail.Name + "List") + " && ownerReference." + (detail.Name + "List") + ".length > 0) {");
                    codeBuilder.AddLine("         var detailExpr = ownerReference." + (detail.Name + "List") + "[0].getJExpression(listFilterRange" + (parentLink == null || parentLink.DetailKeyFields.IsNullOrEmpty() ? "" : ", ['" + parentLink.DetailKeyFields.Replace(" ", "").Replace(",", "','") + "']") + ");");
                    codeBuilder.AddLine("         if (detailExpr === 'Error') return detailExpr;");
                    codeBuilder.AddLine("         jExpression += detailExpr;");
                    codeBuilder.AddLine("       }");
                }

                codeBuilder.AddLine("       return jExpression;");
                codeBuilder.AddLine("  };");

                codeBuilder.AddLine("   ownerReference.createOriginal = function(propertyName, oldValue) {");
                codeBuilder.AddLine("       ownerReference.original = ownerReference.getPrimitiveDTO();");
                codeBuilder.AddLine("       if (propertyName) ownerReference.original[propertyName] = oldValue;");
                codeBuilder.AddLine("   }");

                codeBuilder.AddLine("   ownerReference.restoreOriginal = function() {");
                codeBuilder.AddLine("       if (!common.isNullOrEmpty(ownerReference.original)) {");
                codeBuilder.AddLine("          enableChangeTrack = false;");
                codeBuilder.AddLine("          var properties = metadataInfo[ownerReference.typeName];");
                codeBuilder.AddLine("          for (var i = 0; i < properties.length; i++) {");
                codeBuilder.AddLine("              var propertyName = properties[i].key;");
                codeBuilder.AddLine("              if ((typeof ownerReference.original[propertyName]) !== 'undefined') ownerReference[propertyName] = ownerReference.original[propertyName];");
                codeBuilder.AddLine("          }");
                codeBuilder.AddLine("          delete ownerReference.original;");
                codeBuilder.AddLine("          enableChangeTrack = true;");
                codeBuilder.AddLine("       } else if(ownerReference.ChangeState === 'D') ownerReference.ChangeState = 'U';");
                codeBuilder.AddLine("   }");

                codeBuilder.AddLine("   ownerReference.getValidationErrors = function(propertyName) {");
                codeBuilder.AddLine("       var errors = [];");
                codeBuilder.AddLine("       if (!dataBusiness.canReportErrors) return errors;");
                codeBuilder.AddLine("       if (!ownerReference.ChangeState || ['I', 'U'].indexOf(ownerReference.ChangeState) < 0) return errors;");
                codeBuilder.AddLine("       var properties = metadataInfo[ownerReference.typeName];");
                codeBuilder.AddLine("       for (var i = 0; i < properties.length; i++) {");
                codeBuilder.AddLine("           var prop = properties[i];");
                codeBuilder.AddLine("           if (common.isNullOrEmpty(propertyName) || prop.key == propertyName) {");
                codeBuilder.AddLine("               if (prop.isRequired === true && !prop.isPartOfKey && common.isNullOrEmpty(ownerReference[prop.key])) errors.push('O campo [' + prop.headerText + (managerAuth.shellMode=='DEV' ? ' (' + ownerReference.typeName + '.' + prop.key + ')' : '') + '] é requerido.');");
                codeBuilder.AddLine("               if (prop.validateMaxLength === true && prop.maxLength > 0 && !common.isNullOrEmpty(ownerReference[prop.key]) && ownerReference[prop.key].length > prop.maxLength) errors.push('O campo [' + prop.headerText + (managerAuth.shellMode=='DEV' ? ' (' + ownerReference.typeName + '.' + prop.key + ')' : '') + '] permite no máximo ' + prop.maxLength.toString() + ' caractere(s).');");
                codeBuilder.AddLine("           }");
                codeBuilder.AddLine("       }");

                if (details.Count > 0)
                {
                    foreach (var detail in details)
                    {
                        codeBuilder.AddLine("       if (common.isNullOrEmpty(propertyName)) {");
                        codeBuilder.AddLine("           for (var i = 0; i < ownerReference." + detail.Name + "List().length; i++) {");
                        codeBuilder.AddLine("               var detail = ownerReference." + detail.Name + "List()[i];");
                        codeBuilder.AddLine("               errors = errors.concat(detail.getValidationErrors());");
                        codeBuilder.AddLine("           }");
                        codeBuilder.AddLine("       }");
                    }
                }

                codeBuilder.AddLine("       return errors;");
                codeBuilder.AddLine("   }");

                codeBuilder.AddLine("   ownerReference.getPrimitiveDTO = function(loadDetails) {");
                codeBuilder.AddLine("       var command = '';");
                codeBuilder.AddLine("       var properties = metadataInfo[ownerReference.typeName];");
                codeBuilder.AddLine("       for (var i = 0; i < properties.length; i++) {");
                codeBuilder.AddLine("           command += (command === '' ? '' : ', ') + properties[i].key + ': ownerReference.' + properties[i].key;");
                codeBuilder.AddLine("           if (properties[i].isDomain && properties[i].key.length > 4) command += (command === '' ? '' : ', ') + common.strLeft(properties[i].key, properties[i].key.length - 4) + ': ownerReference.' + common.strLeft(properties[i].key, properties[i].key.length - 4);");
                codeBuilder.AddLine("       }");
                codeBuilder.AddLine("       var result = {};");
                codeBuilder.AddLine("       eval('result = { ' + command + ' };');");

                if (details.Count > 0)
                {
                    codeBuilder.AddLine("       if (loadDetails) {");
                    foreach (var det in details)
                    {
                        codeBuilder.AddLine("           result." + det.Name + "List = [];");
                        codeBuilder.AddLine("           var sourceList = ownerReference." + det.Name + "List;");
                        codeBuilder.AddLine("           if (sourceList && sourceList.length > 0) {");
                        codeBuilder.AddLine("               for (var i = 0; i < sourceList.length; i++) {");
                        codeBuilder.AddLine("                   if (['U', 'I', 'D'].indexOf(sourceList[i].ChangeState) >= 0) result." + det.Name + "List.push(sourceList[i].getPrimitiveDTO(sourceList[i].ChangeState != 'D'));");
                        codeBuilder.AddLine("               }");
                        codeBuilder.AddLine("           }");
                    }
                    codeBuilder.AddLine("       }");
                }

                codeBuilder.AddLine("       return result;");
                codeBuilder.AddLine("   };");

                codeBuilder.AddLine("   ownerReference.getAllDetailChanges = function() {");
                codeBuilder.AddLine("       var result = [];");

                if (details.Count > 0)
                {
                    foreach (var det in details)
                    {
                        codeBuilder.AddLine("       var _" + det.Name + "List = ownerReference." + det.Name + "List;");
                        codeBuilder.AddLine("       if (_" + det.Name + "List && _" + det.Name + "List.length > 0) {");
                        codeBuilder.AddLine("           for (var i = 0; i < _" + det.Name + "List.length; i++) {");
                        codeBuilder.AddLine("               var detail = _" + det.Name + "List[i];");
                        codeBuilder.AddLine("               if (['U', 'I', 'D'].indexOf(detail.ChangeState) >= 0) {");
                        codeBuilder.AddLine("                   result.push(detail);");
                        codeBuilder.AddLine("                   result = result.concat(detail.getAllDetailChanges());");
                        codeBuilder.AddLine("               }");
                        codeBuilder.AddLine("           }");
                        codeBuilder.AddLine("       }");
                    }
                }

                codeBuilder.AddLine("       return result;");
                codeBuilder.AddLine("   };");

                codeBuilder.AddLine("   ownerReference.copyDataFrom = function(originData, copyDetails) {");
                codeBuilder.AddLine("       enableChangeTrack = false;");
                codeBuilder.AddLine("       var properties = metadataInfo[ownerReference.typeName];");
                codeBuilder.AddLine("       for (var i = 0; i < properties.length; i++) {");
                codeBuilder.AddLine("            ownerReference[properties[i].key] = originData[properties[i].key];");
                codeBuilder.AddLine("       }");

                if (details.Count > 0)
                {
                    codeBuilder.AddLine("       if (copyDetails) {");
                    foreach (var detail in details)
                    {
                        codeBuilder.AddLine("           if (ownerReference." + detail.Name + "List && originData." + detail.Name + "List) {");
                        codeBuilder.AddLine("               var toList = ownerReference." + detail.Name + "List;");
                        codeBuilder.AddLine("               var fromList = originData." + detail.Name + "List;");
                        codeBuilder.AddLine("               for (var idxElem = toList.length - 1; idxElem >= 0; idxElem--) {");
                        codeBuilder.AddLine("                  if (toList[idxElem].ChangeState === 'D') toList.splice(idxElem, 1);");
                        codeBuilder.AddLine("               }");

                        codeBuilder.AddLine("               for (var idxElem = toList.length - 1; idxElem >= 0; idxElem--) {");
                        codeBuilder.AddLine("                      if (toList[idxElem].ChangeState !== 'N') {");

                        var tmpKey = detail.GetTemporaryKey();
                        if (!tmpKey.IsNull())
                        {
                            codeBuilder.AddLine("                           var fromObj = [];");
                            codeBuilder.AddLine("                           if (toList[idxElem].ChangeState == 'I') {");
                            codeBuilder.AddLine("                               fromObj = _.where(fromList, { Temporary" + tmpKey.Name + ": toList[idxElem]['" + tmpKey.Name + "'] });");
                            codeBuilder.AddLine("                           } else {");
                            codeBuilder.AddLine("                               fromObj = _.where(fromList, { " + String.Join(",", detail.GetPrimaryKeys().Select(e => e + ": toList[idxElem]['" + e + "']")) + " });");
                            codeBuilder.AddLine("                           }");
                        }
                        else codeBuilder.AddLine("                           var fromObj = _.where(fromList, { " + String.Join(",", detail.GetPrimaryKeys().Select(e => e + ": toList[idxElem]['" + e + "']")) + " });");

                        codeBuilder.AddLine("                           if (fromObj.length > 0) toList[idxElem].copyDataFrom(fromObj[0], true);");

                        codeBuilder.AddLine("                      }");

                        codeBuilder.AddLine("               }");

                        codeBuilder.AddLine("           }");

                    }
                    codeBuilder.AddLine("       }");
                }
                codeBuilder.AddLine("       enableChangeTrack = true;");
                codeBuilder.AddLine("   };");

                codeBuilder.AddLine("      ownerReference.refreshData = function(noWait, succeeded) {");

                if (entity.ExistsClientEvent("OnDataRefreshing"))
                    codeBuilder.AddLine("       if (!ownerReference.OnDataRefreshing()) { dataBusiness.closeProcessing(); if (succeeded) { succeeded({results: [ownerReference]}); } return { then: function(thenMethod) { if (thenMethod) thenMethod(); }, fin: function(finMethod) { if (finMethod) finMethod(); } }; }");

                if (!keyFilterForRefreshing.IsNullOrEmpty())
                {
                    codeBuilder.AddLine("         " + keyFilterForRefreshing);
                    codeBuilder.AddLine("         return dataContext.get" + entity.Name + "ByEntitySearchNoAssociations(filterByKey, 0, 0, false, '', querySucceeded);");
                    codeBuilder.AddLine("         function querySucceeded(data) {");

                    codeBuilder.AddLine("            if (data.results.length > 0) {  for (var idx = 0; idx < data.results.length; idx++) { ownerReference.copyDataFrom(data.results[idx]); } }");

                    codeBuilder.AddLine("            if (succeeded) { succeeded(data); }");

                    codeBuilder.AddLine("            if (data.results.length == 0) { return; }");

                    codeBuilder.AddLine("            if (!noWait || ownerReference.atLeastOneDetailLoaded()) { ownerReference.fillDetails(true, '', noWait); }");

                    if (entity.ExistsClientEvent("OnDataRefreshed"))
                        codeBuilder.AddLine("            ownerReference.OnDataRefreshed();");

                    codeBuilder.AddLine("       }");
                }
                else
                {
                    codeBuilder.AddLine("       if (succeeded) { succeeded({results: [ownerReference]}); }");
                    codeBuilder.AddLine("       return { then: function(thenMethod) { if (thenMethod) thenMethod(); }, fin: function(finMethod) { if (finMethod) finMethod(); } }; ");
                }

                codeBuilder.AddLine("      }");

                codeBuilder.AddLine("   ownerReference.isAdded = function() { return ownerReference.ChangeState === 'I'; };");
                codeBuilder.AddLine("   ownerReference.isDeleted = function() { return ownerReference.ChangeState === 'D'; };");
                codeBuilder.AddLine("   ownerReference.isModified = function() { return ownerReference.ChangeState === 'U'; };");
                codeBuilder.AddLine("   ownerReference.isDetached = function() { return false; };");
                codeBuilder.AddLine("   ownerReference.isUnchanged = function() { return ownerReference.ChangeState === 'N'; };");
                codeBuilder.AddLine("   ownerReference.setModified = function() { ownerReference.ChangeState = 'U'; };");
                codeBuilder.AddLine("   ownerReference.setUnchanged = function() { ownerReference.ChangeState = 'N'; };");
                codeBuilder.AddLine("   ownerReference.serverDataType = [];");
                foreach (var prop in entity.GetAllInheritanceAttributes())
                {
                    codeBuilder.AddLine("   ownerReference.serverDataType['" + prop.Name + "'] = '" + Linx.Tools.EntitySearch.ParseJDataType(prop.Datatype) + "';");
                }

                codeBuilder.AddLine("   ownerReference.typeName = '" + entity.Name + "';");
                codeBuilder.AddLine("   ownerReference.isPrimaryKey = function(propertyName) {");
                codeBuilder.AddLine("       var keys = [ " + String.Join(",", entity.GetAllInheritanceAttributes().Where(e => e is EntityAdapterProperty && entity.IsPrimaryKey((EntityAdapterProperty)e)).Select(e => "'" + e.Name + "'").ToArray()) + " ];");
                codeBuilder.AddLine("       return keys.indexOf(propertyName) >= 0;");
                codeBuilder.AddLine("   }");
                codeBuilder.AddLine("   ownerReference.getDisplayName = function(propertyName) {");
                codeBuilder.AddLine("      var property = getEntityProperty(ownerReference.typeName, propertyName);");
                codeBuilder.AddLine("      return (property != null ? property.headerText : propertyName);");
                codeBuilder.AddLine("   }");
                codeBuilder.AddLine("   ownerReference.setDisplayName = function(propertyName, displayName) {");
                codeBuilder.AddLine("      var property = getEntityProperty(ownerReference.typeName, propertyName);");
                codeBuilder.AddLine("      if (property != null) property.headerText = displayName;");
                codeBuilder.AddLine("   }");

                #region Bandeira Rede
                codeBuilder.AddLine("   ownerReference.setBandeiraRede = function (idBandeiraRede) {");

                if (entity.HasBrand())
                {
                    codeBuilder.AddLine("       if (idBandeiraRede >= 0) ownerReference.IdBandeiraRede = idBandeiraRede;");
                }

                Action<EntityAdapter> setDetailBrandStructure = null;
                setDetailBrandStructure = (detElement) =>
                {
                    if (detElement.HasBrand() && detElement.ForceBrandFilter)
                    {
                        codeBuilder.AddLine("       if (idBandeiraRede >= 0 && ownerReference." + detElement.Name + "List.length > 0) ownerReference." + detElement.Name + "List[0].setBandeiraRede(idBandeiraRede);");
                    }
                    detElement.SourceEntityAdapters.ToList().ForEach(e => setDetailBrandStructure(e));
                };
                entity.SourceEntityAdapters.ToList().ForEach(e => setDetailBrandStructure(e));

                codeBuilder.AddLine("   };");
                #endregion

                #region Gpecon
                codeBuilder.AddLine("   ownerReference.setGpecon = function (idGpecon) {");
                if (entity.HasGpecon())
                {
                    codeBuilder.AddLine("       if (idGpecon > 0) ownerReference.IdGpecon = idGpecon;");
                }
                codeBuilder.AddLine("   };");
                #endregion

                if (hasLookUps)
                {
                    codeBuilder.AddLine("   //#region Lookup Extended Methods");
                    GenerateClientErpLookupExecuting(entity, codeBuilder, entitiesRef);
                    codeBuilder.AddLine("   //#endregion Lookup Extended Methods");
                }

                #region setDefaults
                //Direct properties
                codeBuilder.AddLine("   ownerReference.setDefaults = function () {");

                Action<EntityAdapterAttribute> createDefault = (prop) =>
                {
                    codeBuilder.AddLine("       ownerReference." + prop.Name + " = " + GenerateClientErpCodeForDefaultValueAndParameters(prop) + ";");
                };

                entity.GetAllInheritanceProperties().Where(p => !p.DefaultValue.IsNullOrEmpty()).Foreach(createDefault);
                entity.GetAllInheritancePublicationProperties().Where(p => !p.DefaultValue.IsNullOrEmpty()).Foreach(createDefault);

                //Lookup properties
                Action<EntityAdapterAttribute> createDefaultLookup = (prop) =>
                {
                    codeBuilder.AddLine("       ownerReference.executeLookUp('" + prop.GetLookUpName() + "','" + prop.Name + "', null, dataBusiness, " + GenerateClientErpCodeForDefaultValueAndParameters(prop) + ", null);");
                };

                entity.GetAllInheritanceProperties().Where(p => p.IsFK && !p.DefaultValue.IsNullOrEmpty()).Foreach(createDefaultLookup);
                entity.GetAllInheritancePublicationProperties().Where(p => p.IsFK && !p.DefaultValue.IsNullOrEmpty()).Foreach(createDefaultLookup);

                //Set auxiliary parent key replacement.
                if (entity.TargetEntityAdapter != null)
                {
                    foreach (var keyReplacement in entity.GetNoParentKeyRelations())
                    {
                        codeBuilder.AddLine("       ownerReference." + keyReplacement.Key + " = ownerReference." + entity.TargetEntityAdapter.Name + "." + keyReplacement.Value + ";");
                    }
                }

                codeBuilder.AddLine("   };");
                #endregion

                #region delete
                codeBuilder.AddLine("   ownerReference.delete = function() {");
                codeBuilder.AddLine("       if (ownerReference.setParentAsModified) ownerReference.setParentAsModified();");
                if (entity.TargetEntityAdapter != null)
                {
                    codeBuilder.AddLine("       var parent = ownerReference." + entity.TargetEntityAdapter.Name + ";");
                }

                foreach (var detail in details)
                {
                    codeBuilder.AddLine("       if (!common.isNullOrEmpty(ownerReference." + detail.Name + "List) && ownerReference." + detail.Name + "List.length > 0) {");
                    codeBuilder.AddLine("          var details = [].concat(ownerReference." + detail.Name + "List);");
                    codeBuilder.AddLine("          for (var idx = 0; idx < details.length; idx++) {");
                    codeBuilder.AddLine("            details[idx].delete();");
                    codeBuilder.AddLine("          }");
                    codeBuilder.AddLine("       }");
                }


                codeBuilder.AddLine("       if (ownerReference.ChangeState == 'I') {");
                codeBuilder.AddLine("           if (parent && parent." + entity.Name + "List) { ");
                codeBuilder.AddLine("               var idx = parent." + entity.Name + "List.indexOf(ownerReference); ");
                codeBuilder.AddLine("               if (idx >= 0) parent." + entity.Name + "List.splice(idx, 1); ");
                codeBuilder.AddLine("           }");
                codeBuilder.AddLine("           else {");
                codeBuilder.AddLine("               var idx = dataBusiness." + entitiesRef + ".indexOf(ownerReference);");
                codeBuilder.AddLine("               if (idx >= 0) dataBusiness." + entitiesRef + ".splice(idx, 1);");
                codeBuilder.AddLine("           }");
                if (entity.TargetEntityAdapter != null)
                    codeBuilder.AddLine("           delete ownerReference." + entity.TargetEntityAdapter.Name + ";");
                codeBuilder.AddLine("       }");
                codeBuilder.AddLine("       else {");
                codeBuilder.AddLine("           if (ownerReference.ChangeState == 'N') { ownerReference.createOriginal(); }");
                codeBuilder.AddLine("           ownerReference.ChangeState = 'D'; // mark for deletion");
                codeBuilder.AddLine("       }");



                if (entity.TargetEntityAdapter != null)
                {
                    codeBuilder.AddLine("       if (parent && (typeof parent.setCurrentDetails === 'function') && parent." + entity.Name + "List && parent." + entity.Name + "List.length == 0) parent.setCurrentDetails('" + entity.Name + "');");
                }

                codeBuilder.AddLine("   };");
                #endregion

                #region setParentAsModified
                codeBuilder.AddLine("   ownerReference.setParentAsModified = function() {");
                if (entity.TargetEntityAdapter != null)
                {
                    codeBuilder.AddLine("   var parent = ownerReference." + entity.TargetEntityAdapter.Name + ";");
                    codeBuilder.AddLine("   if (parent) {");
                    codeBuilder.AddLine("       if (parent.isUnchanged()) {");
                    codeBuilder.AddLine("           parent.setModified(); ");
                    codeBuilder.AddLine("       }");
                    codeBuilder.AddLine("       parent.setParentAsModified();");
                    codeBuilder.AddLine("   }");

                }
                codeBuilder.AddLine("   };");
                #endregion

                #region getParent/getSelfList
                codeBuilder.AddLine("   ownerReference.getParent = function() {");
                if (entity.TargetEntityAdapter != null)
                {
                    codeBuilder.AddLine("       return ownerReference." + entity.TargetEntityAdapter.Name + ";");
                }
                else
                    codeBuilder.AddLine("       return null;");
                codeBuilder.AddLine("   };");
                codeBuilder.AddLine("   ownerReference.getSelfList = function() {");
                if (entity.TargetEntityAdapter != null)
                {
                    codeBuilder.AddLine("       var parent = ownerReference.getParent();");
                    codeBuilder.AddLine("       if (!common.isNullOrEmpty(parent)) {");
                    codeBuilder.AddLine("           return parent." + entity.Name + "List;");
                    codeBuilder.AddLine("       } else { return null; }");
                }
                else
                    codeBuilder.AddLine("       return dataBusiness." + entitiesRef + ";");
                codeBuilder.AddLine("   };");
                #endregion

                codeBuilder.AddLine("   ownerReference.Namespace = '" + _designerRoot.GetContextNamespace() + "';");
                codeBuilder.AddLine("   ownerReference.myProperties = [ " + classProperties + " ];");
                codeBuilder.AddLine("   ownerReference.queryRequiredProperties = { " + queryRequiredProperties + " };");

                string excludedFilters = "";
                foreach (var prop in entity.GetAllInheritanceAttributes().Where(e => e.RemoveFilterFromClientLayer))
                {
                    excludedFilters += (excludedFilters.IsNullOrEmpty() ? "" : ", ") + "'" + prop.Name + "'";
                }
                codeBuilder.AddLine("   ownerReference.excludedFilters = [" + excludedFilters + "];");

                codeBuilder.AddLine("   ownerReference.getCurrentElements = function() {");
                codeBuilder.AddLine("       var result = [ ownerReference ];");

                foreach (var detail in details)
                {
                    codeBuilder.AddLine("   if (!common.isNullOrEmpty(ownerReference.current" + detail.Name + ")) { result = result.concat(ownerReference.current" + detail.Name + ".getCurrentElements()); }");
                }

                codeBuilder.AddLine("       return result;");
                codeBuilder.AddLine("   };");

                #region SendAllRowsOnSubmitting
                codeBuilder.AddLine("   ownerReference.checkForSendingAllRowsToServer = function() {");
                var entitiesForSendingChanges = entity.GetSourceEntityAdapters().Where(e => e.SendAllRowsOnSubmitting).ToArray();
                if (entitiesForSendingChanges.Length > 0)
                {
                    codeBuilder.AddLine("      if (ownerReference.isUnchanged()) {");
                    codeBuilder.AddLine("          ownerReference.entityAspect.setModified();");
                    codeBuilder.AddLine("      }");
                    foreach (var detail in entitiesForSendingChanges)
                    {
                        codeBuilder.AddLine("      for (var idx = 0; idx < ownerReference." + detail.Name + "List.length; idx++) {");
                        codeBuilder.AddLine("          if (ownerReference." + detail.Name + "List[idx].isUnchanged()) {");
                        codeBuilder.AddLine("              ownerReference." + detail.Name + "List[idx].entityAspect.setModified();");
                        codeBuilder.AddLine("          }");
                        codeBuilder.AddLine("          ownerReference." + detail.Name + "List[idx].checkForSendingAllRowsToServer();");
                        codeBuilder.AddLine("      }");
                    }
                }
                codeBuilder.AddLine("   };");
                #endregion

                //Generate where clause for details
                foreach (var detail in details)
                {
                    detail.GenerateJsWhereDetailRelationMethod(codeBuilder, "ownerReference", "common", false);
                }
                foreach (var detail in details)
                {
                    codeBuilder.AddLine("   ownerReference." + detail.Name + "IsLoaded = false;");
                }

                codeBuilder.AddLine("   ownerReference.detailsLoaded = function() {");
                if (details.Count == 0)
                    codeBuilder.AddLine("       return true;");
                else
                {
                    string detailsLoadTest = "";
                    foreach (var detail in details)
                    {
                        detailsLoadTest += (detailsLoadTest.IsNullOrEmpty() ? "" : " && ") + "ownerReference." + detail.Name + "IsLoaded";
                    }
                    codeBuilder.AddLine("       return " + detailsLoadTest + ";");
                }
                codeBuilder.AddLine("   }");

                codeBuilder.AddLine("   ownerReference.atLeastOneDetailLoaded = function() {");
                if (details.Count == 0)
                    codeBuilder.AddLine("       return true;");
                else
                {
                    string detailsLoadTest = "";
                    foreach (var detail in details)
                    {
                        detailsLoadTest += (detailsLoadTest.IsNullOrEmpty() ? "" : " || ") + "ownerReference." + detail.Name + "IsLoaded";
                    }
                    codeBuilder.AddLine("       return " + detailsLoadTest + ";");
                }
                codeBuilder.AddLine("   }");


                codeBuilder.AddLine("   ownerReference.adjustDetailsLoaded = function(value) {");
                if (details.Count > 0)
                {
                    foreach (var detail in details)
                    {
                        codeBuilder.AddLine("       ownerReference." + detail.Name + "IsLoaded = value;");
                        codeBuilder.AddLine("       if (value === false)");
                        codeBuilder.AddLine("           ownerReference." + detail.Name + "List([]);");
                    }
                }
                codeBuilder.AddLine("   }");

                codeBuilder.AddLine("   ownerReference.fillDetails = function(force, detailName, noWait, callback, customParentRelation) {");
                codeBuilder.AddLine("      if (typeof force === 'undefined') force = false;");
                if (entity.ExistsClientEvent("OnSelected"))
                {
                    codeBuilder.AddLine("      ownerReference.OnSelected();");
                }

                if (details.Count > 0)
                {
                    codeBuilder.AddLine("      if (ownerReference.isAdded()) {");
                    foreach (var detail in details)
                    {
                        codeBuilder.AddLine("        ownerReference." + detail.Name + "IsLoaded = true;");
                    }
                    codeBuilder.AddLine("      }");

                    string detailsRemoteTest = "";
                    foreach (var detail in details.Where(e => !e.LoadDataOnlyIfVisible))
                    {
                        detailsRemoteTest += (detailsRemoteTest.IsNullOrEmpty() ? "" : " && ") + "_" + detail.Name + "RemoteComplete";
                        codeBuilder.AddLine("      var _" + detail.Name + "RemoteComplete = false;");
                    }
                    if (detailsRemoteTest.IsNullOrEmpty())
                        detailsRemoteTest = "true";

                    foreach (var detail in details.Where(e => !e.LoadDataOnlyIfVisible))
                    {
                        codeBuilder.AddLine("      var detachList_" + detail.Name + " = [];");
                        codeBuilder.AddLine("      if (force) {");
                        codeBuilder.AddLine("           if (common.isNullOrEmpty(detailName) || detailName == '" + detail.Name + "') ownerReference." + detail.Name + "IsLoaded = false;");
                        codeBuilder.AddLine("           if ((common.isNullOrEmpty(detailName) || detailName == '" + detail.Name + "') && ownerReference." + (detail.Name + "List") + " && ownerReference." + (detail.Name + "List") + ".length > 0) {");
                        codeBuilder.AddLine("                 ownerReference." + detail.Name + "List = [];");
                        codeBuilder.AddLine("           }");
                        codeBuilder.AddLine("      }");
                        codeBuilder.AddLine();

                        var parentLink = detail.GetParentLinkRelation();

                        codeBuilder.AddLine("      if (!ownerReference." + detail.Name + "IsLoaded) {");

                        codeBuilder.AddLine("        //Load " + (detail.Name + "List"));
                        codeBuilder.AddLine("        if (" + (detail.LoadDataOnlyIfVisible ? "(force && common.isNullOrEmpty(detailName))" : "common.isNullOrEmpty(detailName)") + " || detailName === '" + detail.Name + "') {");

                        codeBuilder.AddLine("          ownerReference." + detail.Name + "IsLoaded = true;");
                        codeBuilder.AddLine("          _" + detail.Name + "RemoteComplete = (ownerReference." + (detail.Name + "List") + " && ownerReference." + (detail.Name + "List") + ".length > 0);");

                        codeBuilder.AddLine("          if ((force || !ownerReference." + (detail.Name + "List") + " || ownerReference." + (detail.Name + "List") + ".length === 0)" + (parentLink.RemoveFieldIfEmpty ? "" : detail.GetJsTestDetailRelation("ownerReference", "common")) + ") {");
                        codeBuilder.AddLine("            var navQuery = 'Get" + detail.Name + "ByEntitySearchNoAssociations?$inlinecount=none';");

                        var orderBy = detail.GetOrderByCommand();
                        if (!orderBy.IsNullOrEmpty())
                            codeBuilder.AddLine("            navQuery += '&$orderby=" + orderBy + "';");

                        detail.GetJsWhereDetailRelation(codeBuilder, "ownerReference", "navQuery");
                        codeBuilder.AddLine(";");
                        codeBuilder.AddLine("            dataBusiness.showProcessing();");
                        codeBuilder.AddLine("            httpFactory.httpGet(getDataServiceUrl, navQuery,");
                        codeBuilder.AddLine("                function (data) {");
                        codeBuilder.AddLine("                   for (var idx = 0; idx < data.results.length; idx++) {");
                        codeBuilder.AddLine("                       initializePOCO(data.results[idx], '" + detail.Name + "'); ");
                        codeBuilder.AddLine("                       data.results[idx]." + entity.Name + " = ownerReference; ");
                        codeBuilder.AddLine("                   } ");
                        codeBuilder.AddLine("                   ownerReference." + detail.Name + "List = data.results; ");
                        codeBuilder.AddLine("                   ownerReference.setCurrentDetails('" + detail.Name + "');");
                        if (entity.ExistsClientEvent("OnDetailSearched"))
                        {
                            codeBuilder.AddLine("                   ownerReference.OnDetailSearched('" + detail.Name + "');");
                        }

                        codeBuilder.AddLine("                   dataBusiness.closeProcessing();");
                        codeBuilder.AddLine("                   _" + detail.Name + "RemoteComplete = true;");
                        codeBuilder.AddLine("                   if (callback && (!common.isNullOrEmpty(detailName) || (" + detailsRemoteTest + "))) { callback(); }");
                        if (entity.ExistsClientEvent("OnAllDetailsSearched"))
                            codeBuilder.AddLine("               if ((!common.isNullOrEmpty(detailName) || (" + detailsRemoteTest + "))) { ownerReference.OnAllDetailsSearched(); }");
                        codeBuilder.AddLine("                }, ");
                        codeBuilder.AddLine("                function (error) {");
                        codeBuilder.AddLine("                    dataBusiness.closeProcessing();");
                        codeBuilder.AddLine("                    queryFailed(error);");
                        codeBuilder.AddLine("                });");
                        codeBuilder.AddLine("          } else { ownerReference.setCurrentDetails('" + detail.Name + "'); }");
                        codeBuilder.AddLine("        } else { if (!ownerReference." + detail.Name + "IsLoaded && ownerReference." + (detail.Name + "List") + " && ownerReference." + (detail.Name + "List") + ".length > 0) { ownerReference." + detail.Name + "IsLoaded = true; } }");
                        codeBuilder.AddLine("      } else { ");
                        codeBuilder.AddLine("        if (common.isNullOrEmpty(detailName) || detailName == '" + detail.Name + "') {");
                        codeBuilder.AddLine("           ownerReference.setCurrentDetails('" + detail.Name + "');");
                        codeBuilder.AddLine("        }");
                        codeBuilder.AddLine("        _" + detail.Name + "RemoteComplete = true;");
                        codeBuilder.AddLine("      }");
                    }
                    codeBuilder.AddLine("      if (callback && ((!common.isNullOrEmpty(detailName) && (eval('_' + detailName + 'RemoteComplete && ownerReference.' + detailName + 'IsLoaded') == true)) || (common.isNullOrEmpty(detailName) && (" + detailsRemoteTest + ")))) { callback(); }");
                    if (entity.ExistsClientEvent("OnAllDetailsSearched"))
                        codeBuilder.AddLine("      if (((!common.isNullOrEmpty(detailName) && (eval('_' + detailName + 'RemoteComplete && ownerReference.' + detailName + 'IsLoaded') == true)) || (common.isNullOrEmpty(detailName) && (" + detailsRemoteTest + ")))) { ownerReference.OnAllDetailsSearched(); }");
                }
                else
                {
                    codeBuilder.AddLine("      if (callback) { callback(); }");
                    if (entity.ExistsClientEvent("OnAllDetailsSearched"))
                        codeBuilder.AddLine("      ownerReference.OnAllDetailsSearched();");
                }

                codeBuilder.AddLine("   };");
                codeBuilder.AddLine("   //Select first element as a current item of each detail");
                codeBuilder.AddLine("   ownerReference.setCurrentDetails = function(detailName, clearing) {");
                foreach (var detail in details)
                {
                    codeBuilder.AddLine("      if ((common.isNullOrEmpty(detailName) || detailName === '" + detail.Name + "')) {");

                    codeBuilder.AddLine("           if (ownerReference." + (detail.Name + "List") + ".length > 0) { ownerReference.current" + detail.Name + " = ownerReference." + (detail.Name + "List") + "[0]; if (clearing == null || clearing === false) ownerReference.current" + detail.Name + ".fillDetails(); }");
                    codeBuilder.AddLine("           else { ownerReference.current" + detail.Name + " = null; }");

                    codeBuilder.AddLine("      }");

                }
                codeBuilder.AddLine("   };");

                //Add Client Events
                if (entity.EntityAdapterClientEvented.Count > 0)
                {
                    codeBuilder.AddLine("//#region Client Events");
                    foreach (var cliEvent in entity.EntityAdapterClientEvented)
                    {
                        codeBuilder.AddLine("   ownerReference." + cliEvent.Name + " = function (" + String.Join(", ", cliEvent.Parameters.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries).Select(e => e.Right(" "))) + ") {");
                        codeBuilder.AddLine(cliEvent.MacroScript.IsNullOrEmpty() ? (cliEvent.ReturnType.ToLower().Contains("void") ? "" : "return " + GetClientErpDefaultValueByType(cliEvent.ReturnType, true) + ";") : MacroEngineHelper.ReplaceMacros(cliEvent.MacroScript, MacroOutputType.JavaScriptMobile, _designerRoot) + (cliEvent.ReturnType.ToLower().Contains("bool") ? "\r\nreturn " + this.GetClientErpDefaultValueByType(cliEvent.ReturnType, true) + ";" : ""));
                        codeBuilder.AddLine("   }");
                    }
                    codeBuilder.AddLine("//#endregion Client Events");
                }

                if (details.Count > 0)
                {
                    codeBuilder.AddLine("//#region Adjust details already loaded for a POCO reference");
                    foreach (var detail in details)
                    {
                        codeBuilder.AddLine("   if ((typeof ownerReference." + detail.Name + "List === 'function') && ownerReference." + detail.Name + "List.length > 0) {");
                        codeBuilder.AddLine("        for(var idx = 0; idx < ownerReference." + detail.Name + "List.length; idx++) {  " + detail.Name + "Initializer(ownerReference." + detail.Name + "List[idx], true); }");
                        codeBuilder.AddLine("   }");
                    }
                    codeBuilder.AddLine("//#endregion Adjust details already loaded for a POCO reference");
                }

                codeBuilder.AddLine("};");

                details.ForEach(e => createMetadata(e));
            };

            _designerRoot.EntityAdapters.Where(e => e.TargetEntityAdapter == null).ToList().ForEach(e => createMetadata(e));

            codeBuilder.AddLine("//#endregion Classes Map");

            codeBuilder.AddLine("//#region Context Definition");

            //Clear\Get Methods
            string queryMethods = GenerateDataServiceJsQueryActions(codeBuilder);

            //Event Definition

            codeBuilder.AddLine();
            codeBuilder.AddLine("var cancelChanges = function(dataForUndo) {");
            codeBuilder.AddLine("    if (dataForUndo && dataForUndo.length > 0) {");
            codeBuilder.AddLine("        dataForUndo.forEach(function(e) { e.restoreOriginal(); } ); ");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine();

            codeBuilder.AddLine();
            codeBuilder.AddLine("var getEntityProperty = function (entityName, propertyName) {");
            codeBuilder.AddLine("    for (var i = 0; i < metadataInfo[entityName].length; i++) {");
            codeBuilder.AddLine("        if (metadataInfo[entityName][i].key === propertyName)");
            codeBuilder.AddLine("            return metadataInfo[entityName][i];");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("    return null;");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("var getViewInfo = function (entityName) {");
            codeBuilder.AddLine("    var result = [];");
            codeBuilder.AddLine("    if (metadataInfo[entityName])");
            codeBuilder.AddLine("    {");
            codeBuilder.AddLine("        var viewInfoElements = metadataInfo[entityName];");
            codeBuilder.AddLine("        for (var i = 0; i < viewInfoElements.length; i++) {");
            codeBuilder.AddLine("            var selectedElement = viewInfoElements[i];");
            codeBuilder.AddLine("            if (!selectedElement.hidden && (selectedElement.dataType === 'string' || selectedElement.dataType === 'number'))");
            codeBuilder.AddLine("            {");
            codeBuilder.AddLine("                if (common.strLeft(selectedElement.key, 3) === 'Cod' || common.strLeft(selectedElement.key, 2) === 'Id' || common.strLeft(selectedElement.key, 6) === 'Numero' || common.strLeft(selectedElement.key, 6) === 'Number') {");
            codeBuilder.AddLine("                    result.push(selectedElement);");
            codeBuilder.AddLine("                }");
            codeBuilder.AddLine("            }");
            codeBuilder.AddLine("        }");
            codeBuilder.AddLine("        for (var i = 0; i < viewInfoElements.length; i++) {");
            codeBuilder.AddLine("            var selectedElement = viewInfoElements[i];");
            codeBuilder.AddLine("            if (!selectedElement.hidden && (selectedElement.dataType === 'string')) {");
            codeBuilder.AddLine("                if (common.strLeft(selectedElement.key, 4) === 'Nome' || common.strLeft(selectedElement.key, 4) === 'Name' || common.strLeft(selectedElement.key, 4) === 'Desc' || common.strLeft(selectedElement.key, 6) === 'Titulo' || common.strLeft(selectedElement.key, 5) === 'Title') {");
            codeBuilder.AddLine("                    result.push(selectedElement);");
            codeBuilder.AddLine("                }");
            codeBuilder.AddLine("            }");
            codeBuilder.AddLine("        }");
            codeBuilder.AddLine("        for (var i = 0; i < viewInfoElements.length; i++) {");
            codeBuilder.AddLine("            var selectedElement = viewInfoElements[i];");
            codeBuilder.AddLine("            if (!selectedElement.hidden && selectedElement.dataType === 'date')");
            codeBuilder.AddLine("            {                ");
            codeBuilder.AddLine("               result.push(selectedElement);");
            codeBuilder.AddLine("            }");
            codeBuilder.AddLine("        }");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("    return result;");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine();
            codeBuilder.AddLine("var hasValidationErrors = function(savingData) {");
            codeBuilder.AddLine("    for (var idx = 0; idx < savingData.length; idx++) {");
            codeBuilder.AddLine("        var entity = savingData[idx];");
            codeBuilder.AddLine("        if (entity.ChangeState && entity.getValidationErrors && ['I', 'U'].indexOf(entity.ChangeState) >=0) {");
            codeBuilder.AddLine("           var errors = entity.getValidationErrors();");
            codeBuilder.AddLine("           if (errors.length > 0) {");
            codeBuilder.AddLine("                dialog.showAlert('Campos obrigatórios não estão preenchidos.', errors);");
            codeBuilder.AddLine("                return true;");
            codeBuilder.AddLine("            }");
            codeBuilder.AddLine("        }");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("    return false;");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine();
            codeBuilder.AddLine("var saveChanges = function(saveSucceeded, saveFailed, fin) {");

            codeBuilder.AddLine("    var dataForSaving = JSON.stringify(_.map(dataBusiness.getDataForSaving(), function(entity){ return entity.getPrimitiveDTO(entity.ChangeState != 'D'); }));");
            codeBuilder.AddLine("    return httpFactory.httpPost(getDataServiceUrl, 'Save' + dataBusiness.rootDataTypeName,");
            codeBuilder.AddLine("       dataForSaving,");
            codeBuilder.AddLine("       function (response) {");
            codeBuilder.AddLine("              success(response);");
            codeBuilder.AddLine("       },");
            codeBuilder.AddLine("       function (error) {");
            codeBuilder.AddLine("              failed({ message: error.message });");
            codeBuilder.AddLine("       }");
            codeBuilder.AddLine("    );");
            codeBuilder.AddLine();
            codeBuilder.AddLine("    function success(result) {");
            codeBuilder.AddLine("        if (fin) fin();");
            codeBuilder.AddLine("        if (result.length > 0) {");
            codeBuilder.AddLine("           for (var idx = 0; idx < result.length; idx++) { dataContext.initializePOCO(result[idx], dataBusiness.rootDataTypeName); }");
            codeBuilder.AddLine("        }");
            codeBuilder.AddLine("        if (saveSucceeded)");
            codeBuilder.AddLine("            saveSucceeded(result);");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine();
            codeBuilder.AddLine("    function failed(error) {");
            codeBuilder.AddLine("        if (fin) fin();");
            codeBuilder.AddLine("        if (saveFailed)");
            codeBuilder.AddLine("            saveFailed(error);");
            codeBuilder.AddLine("        var msg = (!common.isNullOrEmpty(error.message) ? error.message : error.Message);");
            codeBuilder.AddLine("        dialog.showAlert('Falha ao salvar informações.', [ msg ]);");
            codeBuilder.AddLine("        error.message = msg;");
            codeBuilder.AddLine("        throw error;");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("};");

            foreach (var entity in _designerRoot.EntityAdapters)
            {
                codeBuilder.AddLine();
                codeBuilder.AddLine("var createEmpty" + entity.Name + " = function() {");
                codeBuilder.AddLine("    var entity = {};");
                codeBuilder.AddLine("    for (var idx = 0; idx < metadataInfo['" + entity.Name + "'].length; idx++) { ");
                codeBuilder.AddLine("        var prop = metadataInfo['" + entity.Name + "'][idx]; ");
                codeBuilder.AddLine("        entity[prop.key] = prop.defaultValue;");
                codeBuilder.AddLine("    }");
                codeBuilder.AddLine("    dataContext.initializePOCO(entity, '" + entity.Name + "');");
                codeBuilder.AddLine("    return entity;");
                codeBuilder.AddLine("}");

                codeBuilder.AddLine();
                codeBuilder.AddLine("var create" + entity.Name + " = function(" + (entity.TargetEntityAdapter == null ? "" : "parent") + ") {");

                //Generate Key default values
                string structDefaults = (entity.TargetEntityAdapter == null ? "" : entity.TargetEntityAdapter.Name + ": parent");
                if (entityKeysForDefault.ContainsKey(entity.Name))
                {
                    foreach (string keyRef in entityKeysForDefault[entity.Name])
                    {
                        string defaultValue = "(-1 * getSequence('" + entity.Name + "'))";
                        if (keyRef.Right(":").ToLower().Contains("guid"))
                            defaultValue = "getNewGuid()";
                        else if (keyRef.Right(":").ToLower().Contains("datetime"))
                            defaultValue = "common.getCurrentDate()";
                        else if (keyRef.Right(":").ToLower().Contains("string"))
                            defaultValue = defaultValue + ".toString()";

                        structDefaults += (structDefaults.IsNullOrEmpty() ? "" : ", ") + keyRef.Left(":") + ": " + defaultValue;
                    }
                }

                //Generate bollean default values
                foreach (var prop in entity.GetAllInheritanceAttributes().Where(e => e.Datatype.ToLower().Contains("bool")))
                {
                    structDefaults += (structDefaults.IsNullOrEmpty() ? "" : ", ") + prop.Name + ": false";
                }

                codeBuilder.AddLine("    //Create entity instance");
                codeBuilder.AddLine("    enableChangeTrack = false;");
                codeBuilder.AddLine("    var defaultVals = { " + structDefaults + " };");
                codeBuilder.AddLine("    var entityType = '" + entity.Name + "';");
                codeBuilder.AddLine("    var entity = {};");
                codeBuilder.AddLine("    for (var idx = 0; idx < metadataInfo['" + entity.Name + "'].length; idx++) { ");
                codeBuilder.AddLine("        var prop = metadataInfo['" + entity.Name + "'][idx]; ");
                codeBuilder.AddLine("        if ((typeof defaultVals[prop.key]) !== 'undefined') entity[prop.key] = defaultVals[prop.key];");
                codeBuilder.AddLine("        else  entity[prop.key] = prop.defaultValue;");
                codeBuilder.AddLine("    }");

                codeBuilder.AddLine("    dataContext.initializePOCO(entity, '" + entity.Name + "');");
                if (entity.TargetEntityAdapter != null)
                {
                    codeBuilder.AddLine("    entity." + entity.TargetEntityAdapter.Name + " = parent;");
                    foreach (var rel in entity.GetAllParentKeystAssociation(true))
                    {
                        codeBuilder.AddLine("    entity." + rel.Key + " = parent." + rel.Value + ";");
                    }
                }

                codeBuilder.AddLine("    entity.setDefaults();");
                codeBuilder.AddLine("    entity.ChangeState = 'I';");

                if (entity.TargetEntityAdapter != null)
                {
                    codeBuilder.AddLine("    if (noCurrent !== true) pparent.current" + entity.Name + " = entity;");
                    codeBuilder.AddLine("    if (parent && parent." + entity.Name + "List) parent." + entity.Name + "List.push(entity);");
                    codeBuilder.AddLine("    if (parent && (typeof parent.setCurrentDetails === 'function') && parent." + entity.Name + "List && parent." + entity.Name + "List.length == 0) parent.setCurrentDetails('" + entity.Name + "');");
                    codeBuilder.AddLine("    if (entity.setParentAsModified) entity.setParentAsModified();");
                }

                codeBuilder.AddLine("    enableChangeTrack = true;");
                codeBuilder.AddLine("    return entity;");
                codeBuilder.AddLine("};");
            }

            codeBuilder.AddLine();
            codeBuilder.AddLine("var deleteEntity = function (entity) {");
            codeBuilder.AddLine("    entity.delete();");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine();
            codeBuilder.AddLine("var executeQuery = function (getMethod, jEntitySearch, order, skip, take, noTracking, qSucceeded, qFin, qFailed) {");
            codeBuilder.AddLine("    var query = getMethod + '?$inlinecount=allpages';");

            codeBuilder.AddLine("    if (!common.isNullOrEmpty(query))");
            codeBuilder.AddLine("       query += '&$orderby=' + order;");
            codeBuilder.AddLine();
            codeBuilder.AddLine("    if (take > 0)");
            codeBuilder.AddLine("       query += '&$top=' + take.toString() + '&$skip=' + skip.toString();");
            codeBuilder.AddLine();
            codeBuilder.AddLine("    if ((typeof jEntitySearch !== 'undefined') && jEntitySearch !== null)");
            codeBuilder.AddLine("        query += '&jEntitySearch=' + jEntitySearch;");
            codeBuilder.AddLine();
            codeBuilder.AddLine("    return httpFactory.httpGet(getDataServiceUrl, query, localQuerySucceeded, localQueryFailed);");
            codeBuilder.AddLine();
            codeBuilder.AddLine("    function localQuerySucceeded(data) {");
            codeBuilder.AddLine("        if (qSucceeded)");
            codeBuilder.AddLine("            qSucceeded(data);");
            codeBuilder.AddLine("        if (qFin)");
            codeBuilder.AddLine("            qFin();");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine();
            codeBuilder.AddLine("    function localQueryFailed(error) {");
            codeBuilder.AddLine("        if (qFin)");
            codeBuilder.AddLine("            qFin();");
            codeBuilder.AddLine("        if (qFailed)");
            codeBuilder.AddLine("            qFailed(error);");
            codeBuilder.AddLine("        else");
            codeBuilder.AddLine("            queryFailed(error);");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("};");

           


            #region exportToExcel
            codeBuilder.AddLine("var exportToExcel = function(entityName, jEntitySearch, translatedJEntitySearch, complete, columnsVisible) {");
            codeBuilder.AddLine("    var info = jQuery.grep(dataExportInfo[dataBusiness.rootDataTypeName], function (item, i) { return (item.name === entityName); });");
            codeBuilder.AddLine("    if (info == null || info.length === 0) {");
            codeBuilder.AddLine("        dialog.showMessage('Exportação não permitida!', 'Alerta', ['Ok']);");
            codeBuilder.AddLine("        return;");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("    httpFactory.httpPost(getDataServiceUrl, getServiceAddress(info[0].actionExport),");
            codeBuilder.AddLine("       JSON.stringify([jEntitySearch, translatedJEntitySearch, columnsVisible]),");
            codeBuilder.AddLine("       function (response) {");
            codeBuilder.AddLine("              saveExcelBlob(entityName + '.xlsx', response);");
            codeBuilder.AddLine("              if(complete) complete();");
            codeBuilder.AddLine("       },");
            codeBuilder.AddLine("       function (error) {");
            codeBuilder.AddLine("              dialog.showAlert(error.message, 'Erro na exportação');");
            codeBuilder.AddLine("              if(complete) complete();");
            codeBuilder.AddLine("       }");
            codeBuilder.AddLine("    );");
            codeBuilder.AddLine("};");
            #endregion

            #region exportReportDataSource
            codeBuilder.AddLine("var exportReportDataSource = function(complete) {");
            codeBuilder.AddLine("    httpFactory.httpGet(getDataServiceUrl, getServiceAddress(\"" + api.GetRoutePrefix() + "/GetReportDataSource\"),");
            codeBuilder.AddLine("       function (response) {");
            codeBuilder.AddLine("              saveExcelBlob('datasource.ldsx', response);");
            codeBuilder.AddLine("              if(complete) complete();");
            codeBuilder.AddLine("       },");
            codeBuilder.AddLine("       function (error) {");
            codeBuilder.AddLine("              dialog.showAlert(error.message, 'Erro na exportação do data source');");
            codeBuilder.AddLine("              if(complete) complete();");
            codeBuilder.AddLine("       }");
            codeBuilder.AddLine("    );");
            codeBuilder.AddLine("};");
            #endregion

            #region exportTemplateReport
            codeBuilder.AddLine("var exportTemplateReport = function(reportPath, complete) {");
            codeBuilder.AddLine("    getServiceAddress(\"" + api.GetRoutePrefix() + "/GetTemplateReport?reportPath=\" + reportPath,");
            codeBuilder.AddLine("       function (response) {");
            codeBuilder.AddLine("                 saveExcelBlob(reportPath + '.lrtx', response);");
            codeBuilder.AddLine("                 if(complete) complete();");
            codeBuilder.AddLine("       },");
            codeBuilder.AddLine("       function (jqXHR, textStatus, errorThrown) {");
            codeBuilder.AddLine("                 alert('" + "Erro na exportação do data source".Translate() + "');");
            codeBuilder.AddLine("                 if(complete) complete();");
            codeBuilder.AddLine("       }");
            codeBuilder.AddLine("    );");
            codeBuilder.AddLine("};");
            #endregion

            #region exportToReport
            codeBuilder.AddLine("var exportToReport = function(reportName, entityName, jEntitySearch, translatedJEntitySearch, complete, columnsVisible, exportMedia) {");
            codeBuilder.AddLine("    var info = jQuery.grep(dataExportInfo[dataBusiness.rootDataTypeName], function (item, i) { return (item.name === entityName);});");
            codeBuilder.AddLine("    if (info == null || info.length === 0) {");
            codeBuilder.AddLine("        dialog.showMessage('Erro na exportação', 'Alerta', ['Ok']);");
            codeBuilder.AddLine("        return;");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("    httpFactory.httpPost(getDataServiceUrl, getServiceAddress(info[0].actionReport),");
            codeBuilder.AddLine("       JSON.stringify([ reportName, jEntitySearch, translatedJEntitySearch, columnsVisible, getServiceAddress(''), exportMedia ]),");
            codeBuilder.AddLine("       function (response) {");
            codeBuilder.AddLine("              saveExcelBlob(entityName + '.lrtx', response);");
            codeBuilder.AddLine("              if(complete) complete();");
            codeBuilder.AddLine("       },");
            codeBuilder.AddLine("       function (error) {");
            codeBuilder.AddLine("              dialog.showAlert(error.message, 'Erro na exportação');");
            codeBuilder.AddLine("              if(complete) complete();");
            codeBuilder.AddLine("       }");
            codeBuilder.AddLine("    );");
            codeBuilder.AddLine("};");
            #endregion

            codeBuilder.AddLine();

            codeBuilder.AddLine();
            codeBuilder.AddLine("function acceptChanges() {");
            codeBuilder.AddLine("}");

            codeBuilder.AddLine();

            codeBuilder.AddLine("//#region Internal methods");

            codeBuilder.AddLine();
            codeBuilder.AddLine("function queryFailed(error) {");
            codeBuilder.AddLine("    dataBusiness.closeProcessing();");
            codeBuilder.AddLine("}");
            codeBuilder.AddLine();

            codeBuilder.AddLine("function loadParameters() {");
            if (!parameterNames.IsNullOrEmpty())
                codeBuilder.AddLine(" dataParameters.registerParameters('" + parameterNames + "');");
            else
                codeBuilder.AddLine(" dataParameters.isLoaded = true;");
            codeBuilder.AddLine("}");
            codeBuilder.AddLine();

            codeBuilder.AddLine("//#endregion Internal methods");

            codeBuilder.AddLine("var dataBusiness = null;");
            codeBuilder.AddLine("var extendedDataBusiness = null;");
            codeBuilder.AddLine("var dataContext = {");
            codeBuilder.AddLine("        dataForUpdate: '',");
            codeBuilder.AddLine("        acceptChanges: acceptChanges,");
            codeBuilder.AddLine("        getPivotLayouts: getPivotLayouts,");
            codeBuilder.AddLine("        getSelectedLayoutContent: getSelectedLayoutContent,");
            codeBuilder.AddLine("        getServiceAddress: getServiceAddress,");
            codeBuilder.AddLine("        getDataFeedUrl: getDataFeedUrl,");
            codeBuilder.AddLine("        getDataServiceUrl: getDataServiceUrl,");
            codeBuilder.AddLine("        setServiceBusUrl: setServiceBusUrl,");
            codeBuilder.AddLine("        initializePOCO: initializePOCO,");
            codeBuilder.AddLine("        shellManagerService: shellManagerService,");
            codeBuilder.AddLine("        hasDataFeed: " + api.IsDataService.ToString().ToLower() + ",");
            codeBuilder.AddLine("        getNewGuid: getNewGuid,");
            codeBuilder.AddLine("        metadataInfo: metadataInfo,");
            codeBuilder.AddLine("        dataExportInfo: dataExportInfo,");
            codeBuilder.AddLine("        entityNames: entityNames,");
            codeBuilder.AddLine("        lookUpNames: lookUpNames,");
            codeBuilder.AddLine("        lookUpProperties: lookUpProperties,");
            codeBuilder.AddLine("        cancelChanges: cancelChanges,");
            codeBuilder.AddLine("        saveChanges: saveChanges,");
            codeBuilder.AddLine("        hasValidationErrors: hasValidationErrors,");
            codeBuilder.AddLine("        getEntityProperty: getEntityProperty,");
            codeBuilder.AddLine("        getViewInfo: getViewInfo,");
            foreach (var entity in _designerRoot.EntityAdapters)
            {
                codeBuilder.AddLine("        create" + entity.Name + ": create" + entity.Name + ",");
            }
            codeBuilder.AddLine("        deleteEntity: deleteEntity,");
            codeBuilder.AddLine("        executeQuery: executeQuery,");
            codeBuilder.AddLine("        sharedData: [],");
            codeBuilder.AddLine("        dataDomains: dataDomains,");
            codeBuilder.AddLine("        dataParameters: dataParameters,");
            codeBuilder.AddLine("        loadParameters: loadParameters,");
            codeBuilder.AddLine("        exportToExcel: exportToExcel,");
            codeBuilder.AddLine("        exportToReport: exportToReport,");
            codeBuilder.AddLine("        exportReportDataSource: exportReportDataSource,");
            codeBuilder.AddLine("        exportTemplateReport: exportTemplateReport,");
            codeBuilder.AddLine("        businessAssemblyName: businessAssemblyName,");
            codeBuilder.AddLine("        controllerName: controllerName,");
            codeBuilder.AddLine("        getResultsCombo: getResultsCombo,");
            codeBuilder.AddLine("        setCurrentDataBusiness: function(curDataBusiness) { dataBusiness = curDataBusiness; extendedDataBusiness = curDataBusiness.getExtendedDataBusiness(); },");
            codeBuilder.AddLine(queryMethods);

            codeBuilder.AddLine("};");

            //parameterTitle{variationKey1|variationValue1|variationKey2|variationValue2|...|variationKeyN|variationValueN},parameterTitleN
            codeBuilder.AddLine("loadParameters();");

            codeBuilder.AddLine("return dataContext;");

            codeBuilder.AddLine("//#endregion Context Definition");

            codeBuilder.DecreaseIndent();
            codeBuilder.AddLine("};");
            codeBuilder.AddLine();
            codeBuilder.DecreaseIndent();
            codeBuilder.AddLine("    module.exports = function(appModule) {");
            codeBuilder.AddLine("        appModule.service(name, dependencies.concat(serviceAPI));");
            codeBuilder.AddLine("    };");
            codeBuilder.AddLine("    /* jshint ignore:end */");

        }

        //
        /// <summary>
        /// Generate Factory Code.
        /// </summary>
        /// <param name="clService"></param>
        /// <param name="codeBuilder"></param>
        public void GenerateClientErpDataFactoryCode(ClientLocalService clService, Linx.Tools.CodeBuilder codeBuilder)
        {
            var uiEntityAdapter = clService.EntityAdapter;
            string NamespacePkg = _designerRoot.GetNamespace(this._designerRoot.GetEadProject()).Replace(".", "-").ToLower();
            string serviceName = GetClientErpDataServiceApiName(), viewModel = clService.Name, masterEntityName = (uiEntityAdapter.IsNull() ? "" : uiEntityAdapter.Name), entitiesRef = "dataView", packageName = "pkg_";
            bool hasDataContext = !uiEntityAdapter.IsNull();
            bool hasDetails = !uiEntityAdapter.IsNull() && uiEntityAdapter.ShowDetailsLoadProcess && uiEntityAdapter.GetAllInheritanceSourceEntityAdapters().Count > 0;
            bool hasBrand = !uiEntityAdapter.IsNull() && uiEntityAdapter.HasBrand(true);
            Dictionary<string, string> injectors = new Dictionary<string, string>();
            string serviceParameters = "";
            string appName = this._designerRoot.GetAppName();
            string factoryName = this.GetClientErpDataFactoryName(clService), extendedFactoryName = this.GetClientErpDataFactoryName(clService, true);


            codeBuilder.IncreaseIndent();
            codeBuilder.AddLine("/* jshint ignore:start */");
            codeBuilder.AddLine("'use strict';");

            codeBuilder.AddLine();

            codeBuilder.AddLine("var name = '" + appName + "_" + factoryName + "';");

            codeBuilder.AddLine();
            codeBuilder.AddLine("var dependencies = [");
            codeBuilder.AddLine("        '$state',");
            codeBuilder.AddLine("        '$log',");
            codeBuilder.AddLine("        '$rootScope',");

            codeBuilder.AddLine("        'commonFactory',");
            codeBuilder.AddLine("        'busyFactory',");
            codeBuilder.AddLine("        'dialogFactory',");
            codeBuilder.AddLine("        'messengeFactory',");
            codeBuilder.AddLine("        'shellManagerService',");

            //Check injectors
            if (!clService.ComponentInjection.IsNullOrEmpty())
            {
                var libs = clService.ComponentInjection.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var lib in libs)
                {
                    var key = lib.Left("#").Trim();
                    var value = lib.Right("#").Trim();
                    if (!key.IsNullOrEmpty() && !value.IsNullOrEmpty() && !injectors.ContainsKey(key))
                        injectors.Add(key, value);
                }
            }

            //Add extended factory reference
            codeBuilder.AddLine("        '" + appName + "_" + extendedFactoryName + "',");

            codeBuilder.AddLine("        '" + appName + "_" + serviceName + "'" + (injectors.Count > 0 ? "," : ""));

            //Add injectors
            if (injectors.Count > 0)
            {
                var keys = injectors.Keys.ToArray();
                for (int idx = 0; idx < keys.Length; idx++)
                {
                    string key = keys[idx];
                    codeBuilder.AddLine("        '" + injectors[key] + "'" + (idx < keys.Length - 1 ? "," : ""));
                    serviceParameters += ", " + key;
                }
            }

            codeBuilder.AddLine("];");
            codeBuilder.AddLine();

            codeBuilder.AddLine("var dataBusinessFactory = function ($state, $log, $rootScope, common, busy, dialog, messenger, shellManagerService, extendedDataBusiness, dataContextConstructor" + serviceParameters + ") {");

            codeBuilder.IncreaseIndent();

            #region Declarations
            codeBuilder.AddLine();

            codeBuilder.AddLine("var dataContext = new dataContextConstructor();");
            codeBuilder.AddLine("var customSearch = function () { ");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var translatedJEntitySearch = '';");
            codeBuilder.AddLine("var customSearchResult = { searchDefinition: '', serializedSearch: '', translatedSearch: '' };");
            codeBuilder.AddLine("var sortInfo = '';");
            codeBuilder.AddLine("var currentSettings = null;");
            codeBuilder.AddLine("var registeredUIs = [];");

            codeBuilder.AddLine("var viewClosed = false;");
            codeBuilder.AddLine("var lastJEntitySearch = null;");
            codeBuilder.AddLine("var lastStatus = '';");

            codeBuilder.AddLine("var _status = 'N';");

            codeBuilder.AddLine("var status = function (value) {");
            codeBuilder.AddLine("    if (typeof value !== 'undefined')");
            codeBuilder.AddLine("        _status = value;");
            codeBuilder.AddLine("    return _status;");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var _isDependentVM = false;");
            codeBuilder.AddLine("var isDependentVM = function (value) {");
            codeBuilder.AddLine("    if (typeof value !== 'undefined')");
            codeBuilder.AddLine("        _isDependentVM = value;");
            codeBuilder.AddLine("    return _isDependentVM;");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var _transactionNumberControl = ('00000000');");
            codeBuilder.AddLine("var transactionNumberControl = function (value) {");
            codeBuilder.AddLine("    if (typeof value !== 'undefined')");
            codeBuilder.AddLine("        _transactionNumberControl = value;");
            codeBuilder.AddLine("    return _transactionNumberControl;");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var _navigationByPage = false;");
            codeBuilder.AddLine("var navigationByPage = function (value) {");
            codeBuilder.AddLine("    if (typeof value !== 'undefined')");
            codeBuilder.AddLine("        _navigationByPage = value;");
            codeBuilder.AddLine("    return _navigationByPage;");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var _hasMainTopDataGrid = false;");
            codeBuilder.AddLine("var hasMainTopDataGrid = function (value) {");
            codeBuilder.AddLine("    if (typeof value !== 'undefined')");
            codeBuilder.AddLine("        _hasMainTopDataGrid = value;");
            codeBuilder.AddLine("    return _hasMainTopDataGrid;");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var _currentDataIndex = 0;");
            codeBuilder.AddLine("var currentDataIndex = function (value) {");
            codeBuilder.AddLine("    if (typeof value !== 'undefined')");
            codeBuilder.AddLine("        _currentDataIndex = value;");
            codeBuilder.AddLine("    return _currentDataIndex;");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var _currentDataItem = null;");
            codeBuilder.AddLine("var currentDataItem = function (value) {");
            codeBuilder.AddLine("    if (typeof value !== 'undefined')");
            codeBuilder.AddLine("        _currentDataItem = value;");
            codeBuilder.AddLine("    return _currentDataItem;");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var _currentPage = 0;");
            codeBuilder.AddLine("var currentPage = function (value) {");
            codeBuilder.AddLine("    if (typeof value !== 'undefined')");
            codeBuilder.AddLine("        _currentPage = value;");
            codeBuilder.AddLine("    return _currentPage;");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var _pageCount = 0;");
            codeBuilder.AddLine("var pageCount = function (value) {");
            codeBuilder.AddLine("    if (typeof value !== 'undefined')");
            codeBuilder.AddLine("        _pageCount = value;");
            codeBuilder.AddLine("    return _pageCount;");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var _pageSize = " + (clService.PageSize < 0 ? 0 : clService.PageSize).ToString() + ";");
            codeBuilder.AddLine("var pageSize = function (value) {");
            codeBuilder.AddLine("    if (typeof value !== 'undefined')");
            codeBuilder.AddLine("        _pageSize = value;");
            codeBuilder.AddLine("    return _pageSize;");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var _totalItemCount = 0;");
            codeBuilder.AddLine("var totalItemCount = function (value) {");
            codeBuilder.AddLine("    if (typeof value !== 'undefined')");
            codeBuilder.AddLine("        _totalItemCount = value;");
            codeBuilder.AddLine("    return _totalItemCount;");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var _isSaving = false;");
            codeBuilder.AddLine("var isSaving = function (value) {");
            codeBuilder.AddLine("    if (typeof value !== 'undefined')");
            codeBuilder.AddLine("        _isSaving = value;");
            codeBuilder.AddLine("    return _isSaving;");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("var " + entitiesRef + " = [];");

            codeBuilder.AddLine("var showDataFeedUrl = function() {");
            codeBuilder.AddLine("    if (extendedDataBusiness.OnToolbarAction && !extendedDataBusiness.OnToolbarAction('ShowFeed')) return;");
            codeBuilder.AddLine("    dialog.showAlert(dataContext.getDataFeedUrl(), 'Endereço do serviço');");
            codeBuilder.AddLine("};");

            //LastSearchFilter

            codeBuilder.AddLine("var lastSearchFilter = function () {");
            codeBuilder.AddLine("    if (extendedDataBusiness.OnToolbarAction && !extendedDataBusiness.OnToolbarAction('ShowCurrentFilter')) return;");
            codeBuilder.AddLine("    var filterTranslation = getTranslatedFilter();");
            codeBuilder.AddLine("    dialog.showAlert((common.isNullOrEmpty(filterTranslation) ? 'Pesquisa sem filtros.' : filterTranslation), 'Filtros da pesquisa');");
            codeBuilder.AddLine("}");
            #endregion

            #region currentRecord
            codeBuilder.AddLine("var currentRecord = function () {");
            codeBuilder.AddLine("    if (pageSize() === 0) return currentDataIndex();");
            codeBuilder.AddLine("    else return (currentPage() * pageSize()) + currentDataIndex();");
            codeBuilder.AddLine("};");
            #endregion

            #region totalRecords
            codeBuilder.AddLine("var totalRecords = function () {");
            codeBuilder.AddLine("    if (pageSize() === 0) return " + entitiesRef + ".length;");
            codeBuilder.AddLine();
            codeBuilder.AddLine("    var recordCount = 0;");
            codeBuilder.AddLine("    if (currentPage() === 0) {");
            codeBuilder.AddLine("        if (pageCount() <= 1) {");
            codeBuilder.AddLine("             recordCount = " + entitiesRef + ".length;");
            codeBuilder.AddLine("        } else {");
            codeBuilder.AddLine("             recordCount = totalItemCount() - pageSize() +  " + entitiesRef + ".length;");
            codeBuilder.AddLine("        }");
            codeBuilder.AddLine("    } else if (currentPage() === (pageCount() - 1)) {");
            codeBuilder.AddLine("        recordCount = (pageSize() * (pageCount() - 1)) + " + entitiesRef + ".length;");
            codeBuilder.AddLine("    } else {");
            codeBuilder.AddLine("        recordCount = pageSize() * (currentPage() + 1);");
            codeBuilder.AddLine("        recordCount += totalItemCount() - (pageSize() * (currentPage() + 2));");
            codeBuilder.AddLine("        recordCount += " + entitiesRef + ".length;");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("    return recordCount;");
            codeBuilder.AddLine("};");
            #endregion
            #region currentFormattedRecord
            codeBuilder.AddLine("var currentFormattedRecord = function () {");
            codeBuilder.AddLine("    if (totalRecords() === 0) return '0';");
            codeBuilder.AddLine("    else return (currentRecord()+1).toString();");
            codeBuilder.AddLine("};");
            #endregion
            #region currentRecordInfo
            codeBuilder.AddLine("var currentRecordInfo = function () { var totalR = totalRecords(); if (totalR === 0) { return '0/0'; } else { return currentFormattedRecord() + '/' + totalR.toString(); } };");
            #endregion

            #region Form Events
            codeBuilder.AddLine("//#region Form Events");
            codeBuilder.AddLine();

            codeBuilder.AddLine("var started = false;");
            codeBuilder.AddLine("var parentService = null;");
            codeBuilder.AddLine("var uiSettings = null;");
            codeBuilder.AddLine("var filteredEntities = [];");


            var quickSearchProperties = clService.EntityAdapter.GetAllInheritanceProperties().Where(e => e.QuickSearchIndex >= 0).OrderBy(e => e.QuickSearchIndex).ToArray();
            bool hasQuickSearch = quickSearchProperties.Where(e => e.Datatype.ToLower().Contains("string")).Count() > 0;
            codeBuilder.AddLine("//#region quick search");
            if (hasQuickSearch)
            {
                codeBuilder.AddLine("var executeQuickSearch = function (quickSearchTerm, jExpr, page, queryCallback, propertiesSelection) {");
                if (hasQuickSearch)
                {
                    codeBuilder.AddLine("    return dataContext.get" + clService.EntityAdapter.Name + "QuickSearch(quickSearchTerm, jExpr, page, queryCallback, propertiesSelection);");
                }
                codeBuilder.AddLine("}");

                codeBuilder.AddLine("var selectQuickSearch = function (quickSearchTerm, jExpr, selectedItem, editMode, queryCallback) {");
                codeBuilder.AddLine("    //Clear before query");
                codeBuilder.AddLine("    filteredEntities = [];");
                codeBuilder.AddLine("    clear();");
                codeBuilder.AddLine();
                codeBuilder.AddLine("    var quickSearchJExpression = '" + uiEntityAdapter.Name + "{';");
                codeBuilder.AddLine("    if (common.isNullOrEmpty(selectedItem)) //Select all");
                codeBuilder.AddLine("    {");
                string separator = "";
                codeBuilder.AddLine("       quickSearchJExpression += '(;';");
                foreach (var prop in quickSearchProperties)
                {
                    if (prop.Datatype.ToLower().Contains("string"))
                    {
                        codeBuilder.AddLine("       quickSearchJExpression += '" + separator + prop.Name + "#Like#" + Linx.Tools.EntitySearch.ParseJDataType(prop.Datatype) + "' + '%' + quickSearchTerm + '%';");
                        separator = ";||#";
                    }
                }
                codeBuilder.AddLine("       quickSearchJExpression += ';)' + (common.isNullOrEmpty(jExpr) ? '' : ';&&;' + jExpr);");
                codeBuilder.AddLine("    }");
                codeBuilder.AddLine("    else { //Select only one element");
                separator = "";
                foreach (var prop in quickSearchProperties)
                {
                    if (prop.Datatype.ToLower().Contains("datetime"))
                    {
                        codeBuilder.AddLine("       if (!common.isNullOrEmpty(selectedItem." + prop.Name + ")) {");
                        codeBuilder.AddLine("           var value = common.getUTCDate(new Date(selectedItem." + prop.Name + "));");
                        codeBuilder.AddLine("           quickSearchJExpression += '" + separator + prop.Name + "#>=#" + Linx.Tools.EntitySearch.ParseJDataType(prop.Datatype) + "' + value.getUTCFullYear().toString() + '-' + (value.getUTCMonth() + 1).toString() + '-' + value.getUTCDate().toString() + ' 00:00:00.000;" + prop.Name + "#<=#" + Linx.Tools.EntitySearch.ParseJDataType(prop.Datatype) + "' + value.getUTCFullYear().toString() + '-' + (value.getUTCMonth() + 1).toString() + '-' + value.getUTCDate().toString() + ' 23:59:59.999';");
                        codeBuilder.AddLine("       }");
                        separator = ";";
                    }
                    else
                    {
                        codeBuilder.AddLine("       if (!common.isNullOrEmpty(selectedItem." + prop.Name + ")) {");
                        codeBuilder.AddLine("           quickSearchJExpression += '" + separator + prop.Name + "#==#" + Linx.Tools.EntitySearch.ParseJDataType(prop.Datatype) + "' + selectedItem." + prop.Name + ".toString()" + (prop.Datatype.ToLower().Contains("bool") ? ".toLowerCase()" : "") + ";");
                        codeBuilder.AddLine("       }");
                        separator = ";";
                    }
                }

                codeBuilder.AddLine("       quickSearchJExpression += (common.isNullOrEmpty(jExpr) ? '' : ';' + jExpr);");

                codeBuilder.AddLine("    }");
                codeBuilder.AddLine("    quickSearchJExpression += '}';");

                codeBuilder.AddLine("    _canQuickSearch = false;");
                codeBuilder.AddLine("    _canQuickSearch = true;");
                codeBuilder.AddLine("    dataToolbar.query(quickSearchJExpression, function () { if (editMode) { edit();  } if (queryCallback) { queryCallback(); } });");


                string qsFormatSelect = String.Join(" | ", quickSearchProperties.Select(e => (e.Datatype.ToLower().Contains("datetime") ? "Globalize.format(common.getUTCDate(new Date(selectedItem." + e.Name + ")), '" + e.DataFormatString + "')" : "selectedItem." + e.Name)));
                codeBuilder.AddLine("    return " + qsFormatSelect + ";");
                codeBuilder.AddLine("}");
                codeBuilder.AddLine();

            }

            codeBuilder.AddLine("//#endregion ");

            codeBuilder.AddLine();
            codeBuilder.AddLine("var adjustModuleSecurity = function () {");
            codeBuilder.AddLine("    parentService = null;");
            codeBuilder.AddLine("    uiSettings = null;");
            codeBuilder.AddLine("    isDependentVM(false);");
            codeBuilder.AddLine("    setSecurity(true, true, true, true, true, true, true, true, true, true);");
            codeBuilder.AddLine("    if (shellManagerService.shellMode == 'PROD') {");
            codeBuilder.AddLine("       shellManagerService.getFormAccess('" + NamespacePkg + "-" + clService.Name + "', function (data) {");
            codeBuilder.AddLine("          if (data && !data.AcessoTotal) {");
            codeBuilder.AddLine("              setSecurity(data.Incluir, true, data.PesquisaEspecial, data.Excluir, data.Alterar, data.Layout, true, data.Imprimir, data.Pesquisar, data.Exportar);");
            codeBuilder.AddLine("          }");
            codeBuilder.AddLine("       }, null);");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("};");
            
            codeBuilder.AddLine("var initService = function() {");
            codeBuilder.AddLine("  if (!started) { started = true; clear(); }");
            codeBuilder.AddLine("    if (extendedDataBusiness.OnInit) extendedDataBusiness.OnInit();");
            codeBuilder.AddLine("  return true;");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("//#endregion");
            #endregion

            #region getMaxLength
            codeBuilder.AddLine("var getMaxLength = function(entityName, propertyName){");
            codeBuilder.AddLine("    if (common.isNullOrEmpty(entityName)) entityName = '" + masterEntityName + "';");
            codeBuilder.AddLine("    var property = dataContext.getEntityProperty(entityName, propertyName);");
            codeBuilder.AddLine("    if(property != null)");
            codeBuilder.AddLine("        return property.maxLength;");
            codeBuilder.AddLine("    else");
            codeBuilder.AddLine("        return 0;");
            codeBuilder.AddLine("};");
            #endregion

            #region KPIs
            foreach (var entity in _designerRoot.EntityAdapters.Where(e => e.DerivedEntityAdapters.Count == 0).ToList())
            {
                foreach (var kpiName in entity.GetAllInheritanceAttributes().Where(e => !e.KpiName.IsNullOrEmpty()).Select(d => d.KpiName).Distinct())
                {
                    codeBuilder.AddLine("var get" + kpiName + "Ranges = function (succeeded) {");
                    codeBuilder.AddLine("    if (dataBusiness.kpi" + kpiName + " == null) dataContext.get" + kpiName + "Ranges(querySucceeded);");
                    codeBuilder.AddLine("    else if (succeeded) succeeded(dataBusiness.kpi" + kpiName + ".ranges, dataBusiness.kpi" + kpiName + ".min, dataBusiness.kpi" + kpiName + ".max);");
                    codeBuilder.AddLine("    return true;");
                    codeBuilder.AddLine();
                    codeBuilder.AddLine("    function querySucceeded(data) {");
                    codeBuilder.AddLine("        dataBusiness.kpi" + kpiName + " = { ranges: [], min: 0, max: 0 };");
                    codeBuilder.AddLine("        for (var r in data.results) {");
                    codeBuilder.AddLine("            dataBusiness.kpi" + kpiName + ".ranges.push({ from: data.results[r].StartValue, to: data.results[r].EndValue, color: data.results[r].Color });");
                    codeBuilder.AddLine("            if (dataBusiness.kpi" + kpiName + ".min > data.results[r].StartValue) dataBusiness.kpi" + kpiName + ".min = data.results[r].StartValue;");
                    codeBuilder.AddLine("            if (dataBusiness.kpi" + kpiName + ".max < data.results[r].EndValue) dataBusiness.kpi" + kpiName + ".max = data.results[r].EndValue;");
                    codeBuilder.AddLine("        }");
                    codeBuilder.AddLine("        if (succeeded) succeeded(dataBusiness.kpi" + kpiName + ".ranges, dataBusiness.kpi" + kpiName + ".min, dataBusiness.kpi" + kpiName + ".max);");
                    codeBuilder.AddLine("    }");
                    codeBuilder.AddLine("};");

                    codeBuilder.AddLine("var get" + kpiName + "GaugeGrid = function (succeeded) {");
                    codeBuilder.AddLine("   if (dataBusiness.kpi" + kpiName + " == null) {");
                    codeBuilder.AddLine("       get" + kpiName + "Ranges(succeeded);");
                    codeBuilder.AddLine("   }");
                    codeBuilder.AddLine("   if (dataBusiness.kpi" + kpiName + " != null) {");
                    codeBuilder.AddLine("       var ranges = '';");
                    codeBuilder.AddLine("       for (var i in dataBusiness.kpi" + kpiName + ".ranges) {");
                    codeBuilder.AddLine("           ranges += '{\"from\" : \"' + dataBusiness.kpi" + kpiName + ".ranges[i].from + '\", \"to\" : \"' + dataBusiness.kpi" + kpiName + ".ranges[i].to + '\" , \"color\" : \"' + dataBusiness.kpi" + kpiName + ".ranges[i].color + '\" }';");
                    codeBuilder.AddLine("           if (i < dataBusiness.kpi" + kpiName + ".ranges.length - 1)");
                    codeBuilder.AddLine("               ranges += \",\";");
                    codeBuilder.AddLine("       }");
                    codeBuilder.AddLine("       ranges = \"[\" + ranges + \"]\";");
                    codeBuilder.AddLine("       var obj = {min: dataBusiness.kpi" + kpiName + ".min, max: dataBusiness.kpi" + kpiName + ".max, ranges: ranges };");
                    codeBuilder.AddLine("       return obj;");
                    codeBuilder.AddLine("    }");
                    codeBuilder.AddLine("   else {");
                    codeBuilder.AddLine("       return {min: 0, max: 0, ranges: \"[]\" };");
                    codeBuilder.AddLine("    }");
                    codeBuilder.AddLine("};");
                }
            }
            #endregion

            #region loadDataView
            codeBuilder.AddLine("var loadDataView = function () {");
            codeBuilder.AddLine("};");
            #endregion

            #region getParentSelectorDataName
            codeBuilder.AddLine("var getParentSelectorDataName = function () {");
            codeBuilder.AddLine("   return ((typeof uiSettings === 'object') ? uiSettings.parentSelectorDataName : '');");
            codeBuilder.AddLine("};");
            #endregion

            #region getJExpression
            codeBuilder.AddLine("var getJExpression = function (currentDI) {");
            codeBuilder.AddLine("    if (typeof currentDI === 'undefined') currentDI = currentDataItem();");
            codeBuilder.AddLine("    return currentDI.getJExpression(dataBusiness.entitySearchRange, [], false);");
            codeBuilder.AddLine("};");
            #endregion

            #region exportData
            codeBuilder.AddLine("var exportData = function (forceAdd) {");
            codeBuilder.AddLine("    if (extendedDataBusiness.OnToolbarAction && !extendedDataBusiness.OnToolbarAction('Export')) return;");
            codeBuilder.AddLine("};");
            #endregion

            #region exportDataDetails (Children)
            codeBuilder.AddLine("var exportDataDetails = function (entity, detailName) {");
            codeBuilder.AddLine("};");
            #endregion

            #region finalizeCombo
            codeBuilder.AddLine("var finalizeCombo = function (current, itens, lookupName) {");
            codeBuilder.AddLine("   dataContext['finalizeAll' + lookupName](current, itens, '', '');");
            codeBuilder.AddLine("};");
            #endregion

            #region clearCombo
            codeBuilder.AddLine("var clearCombo = function (current, lookupName) {");
            codeBuilder.AddLine("   dataContext['clear' + lookupName](current);");
            codeBuilder.AddLine("};");
            #endregion

            #region getDataCombo
            codeBuilder.AddLine("var dataCombo = {");
            codeBuilder.AddLine("    combos: [],");
            codeBuilder.AddLine("    getItems: function (comboName, valuesFilter) {");
            codeBuilder.AddLine("        var items = dataCombo.combos[comboName];");
            codeBuilder.AddLine("        if (!common.isNullOrEmpty(valuesFilter) && items && items.length > 0) {");
            codeBuilder.AddLine("            for (var i = items.length - 1; i >= 0; i--) {");
            codeBuilder.AddLine("                if ((',' + valuesFilter + ',').indexOf(',' + items[i].id + ',') === -1) {");
            codeBuilder.AddLine("                    items.removeAt(i);");
            codeBuilder.AddLine("                }");
            codeBuilder.AddLine("            }");
            codeBuilder.AddLine("        }");
            codeBuilder.AddLine("        return (items && items.length > 0 ? items : []);");
            codeBuilder.AddLine("    },");
            codeBuilder.AddLine("    fillDataCombos: function (lookupName, fieldName, current, complete) {");
            codeBuilder.AddLine("        dataContext.getResultsCombo(lookupName, fieldName, current, function (result) {");
            codeBuilder.AddLine("            dataCombo.combos[lookupName] = result;");
            codeBuilder.AddLine("            if (complete) complete();");
            codeBuilder.AddLine("        });");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("};");
            #endregion

            #region refreshCurrentData
            codeBuilder.AddLine("var refreshCurrentData = function () {");
            codeBuilder.AddLine("    if (extendedDataBusiness.OnToolbarAction && !extendedDataBusiness.OnToolbarAction('Refresh')) return;");
            codeBuilder.AddLine("    if (navigationByPage()) {");
            codeBuilder.AddLine("       var refreshIndexedData = function (currentIndex) {");
            codeBuilder.AddLine("             if (currentIndex < " + entitiesRef + ".length) {");
            codeBuilder.AddLine("                 if (currentIndex == 0) dataBusiness.showProcessing('Atualizando informações...');");
            codeBuilder.AddLine("                 " + entitiesRef + "[currentIndex].refreshData().fin(function () { refreshIndexedData(currentIndex + 1); });");
            codeBuilder.AddLine("             }");
            codeBuilder.AddLine("             else {");
            codeBuilder.AddLine("                 dataBusiness.closeProcessing();");
            codeBuilder.AddLine("             }");
            codeBuilder.AddLine("       };");
            codeBuilder.AddLine("       if (" + entitiesRef + ".length > 0) {");
            codeBuilder.AddLine("            refreshIndexedData(0);");
            codeBuilder.AddLine("       }");
            codeBuilder.AddLine("       return;");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("    dataBusiness.showProcessing('Atualizando informações...');");
            codeBuilder.AddLine("    return currentDataItem().refreshData(false, complete);");
            codeBuilder.AddLine();
            codeBuilder.AddLine("    function complete() {");
            codeBuilder.AddLine("        dataBusiness.closeProcessing();");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("}");
            #endregion

            #region getQueryFilter

            codeBuilder.AddLine("var getTranslatedFilter = function () {");
            codeBuilder.AddLine("    return translatedJEntitySearch + (common.isNullOrEmpty(translatedJEntitySearch) || common.isNullOrEmpty(customSearchResult.translatedSearch) ? '' : ' e ') + customSearchResult.translatedSearch;");
            codeBuilder.AddLine("}");

            codeBuilder.AddLine("var getQueryFilter = function (currentDI) {");
            codeBuilder.AddLine("    if (typeof currentDI === 'undefined') currentDI = currentDataItem();");
            codeBuilder.AddLine("    currentDI.setBandeiraRede(getBandeiraRede());");
            codeBuilder.AddLine("    var eSearch = getJExpression(currentDI);");
            codeBuilder.AddLine("    if (eSearch === 'Error')");
            codeBuilder.AddLine("       return 'Error';");

            codeBuilder.AddLine("    if (extendedDataBusiness.OnSearching) {");
            codeBuilder.AddLine("       var extraFilter = extendedDataBusiness.OnSearching();");
            codeBuilder.AddLine("       if (extraFilter === 'Error')");
            codeBuilder.AddLine("          return 'Error';");
            codeBuilder.AddLine("       if (!common.isNullOrEmpty(extraFilter)) eSearch += extraFilter;");
            codeBuilder.AddLine("    }");

            if (uiEntityAdapter.HasBrand(true))
            {
                codeBuilder.AddLine("    if (dataBusiness.getBandeiraRede() === 0) {");
                if (uiEntityAdapter.HasBrand())
                    codeBuilder.AddLine("       eSearch += '" + uiEntityAdapter.Name + "{IdBandeiraRede#' + (!common.isNullOrEmpty(dataBusiness.currentBrands) ? 'In#S' : '==#I') + dataBusiness.getCurrentBrands() + '}';");
                foreach (var detail in uiEntityAdapter.GetAllSourceEntityAdapters())
                {
                    if (detail.HasBrand() && detail.ForceBrandFilter)
                        codeBuilder.AddLine("       eSearch += '" + detail.Name + "{IdBandeiraRede#' + (!common.isNullOrEmpty(dataBusiness.currentBrands) ? 'In#S' : '==#I') + dataBusiness.getCurrentBrands() + '}';");
                }
                codeBuilder.AddLine("    }");
            }

            codeBuilder.AddLine("   translatedJEntitySearch = common.translateSearch(dataContext, eSearch);");

            codeBuilder.AddLine("    if (!common.isNullOrEmpty(customSearchResult.searchDefinition)) eSearch += customSearchResult.searchDefinition;");

            codeBuilder.AddLine("    return eSearch;");
            codeBuilder.AddLine("}");
            #endregion

            #region syncStatus
            codeBuilder.AddLine("var setStatus = function (st) {");
            codeBuilder.AddLine("  status(st);");
            codeBuilder.AddLine("  goToIndex(currentDataIndex());");
            codeBuilder.AddLine("};");
            #endregion
            codeBuilder.AddLine("var removedEntities = [];");
            codeBuilder.AddLine("var dataCache = []; //Initialize data cache");
            codeBuilder.AddLine("var syncDataCache = function () {");
            codeBuilder.AddLine("    " + entitiesRef + ".forEach(function (element) {");
            codeBuilder.AddLine("        if (element.ChangeState && dataCache.indexOf(element) < 0) { dataCache.push(element); }");
            codeBuilder.AddLine("    });");
            codeBuilder.AddLine("}");

            codeBuilder.AddLine("var getDataForSaving = function () {");
            codeBuilder.AddLine("    var result = [];");
            codeBuilder.AddLine("    dataCache = [];");
            codeBuilder.AddLine("    if (preserveDataCurrentState()) {");
            codeBuilder.AddLine("       syncDataCache();");
            codeBuilder.AddLine("       result = dataCache;");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("    else {");
            codeBuilder.AddLine("       result = " + entitiesRef + ";");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("    return _.filter(result, function (e) { return (['U', 'I', 'D'].indexOf(e.ChangeState) >= 0); }).concat(removedEntities);");
            codeBuilder.AddLine("}");

            codeBuilder.AddLine("var getAllChanges = function () {");
            codeBuilder.AddLine("    var details = [];");
            codeBuilder.AddLine("    var changes = getDataForSaving();");
            codeBuilder.AddLine("    for (var idx = 0; idx < changes.length; idx++) {");
            codeBuilder.AddLine("       details = details.concat(changes[idx].getAllDetailChanges());");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("    if (details.length > 0)");
            codeBuilder.AddLine("         return changes.concat(details);");
            codeBuilder.AddLine("    else return changes;");
            codeBuilder.AddLine("}");

            codeBuilder.AddLine("var getAddedEntities = function () {");
            codeBuilder.AddLine("    var result = [];");
            codeBuilder.AddLine("    if (preserveDataCurrentState()) {");
            codeBuilder.AddLine("       syncDataCache();");
            codeBuilder.AddLine("       result = dataCache;");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("    else {");
            codeBuilder.AddLine("       result = " + entitiesRef + ";");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("    return _.filter(result, function (e) { return (e.ChangeState == 'I'); });");
            codeBuilder.AddLine("}");

            codeBuilder.AddLine("var allowMultiSelectionInSearch = function () {");
            codeBuilder.AddLine("   if ((typeof uiSettings.allowMultiSelectionInSearch !== 'undefined')) return uiSettings.allowMultiSelectionInSearch;");
            codeBuilder.AddLine("   else return true;");
            codeBuilder.AddLine("}");

            codeBuilder.AddLine("var isProcessing = false;");

            #region query
            codeBuilder.AddLine("function restoreLastFilter(clearFilters) {");
            codeBuilder.AddLine("        if (clearFilters) filteredEntities = [];");
            codeBuilder.AddLine("        if (filteredEntities.length === 0) return false;");
            codeBuilder.AddLine("        //Set Current Details");
            codeBuilder.AddLine("        for(var idx = 0; idx < filteredEntities.length; idx++) { filteredEntities[idx].setCurrentDetails(null); }");
            codeBuilder.AddLine("        " + entitiesRef + " = [filteredEntities[0]];");
            codeBuilder.AddLine("        if (clearFilters) filteredEntities = [];");
            codeBuilder.AddLine("        return true;");
            codeBuilder.AddLine("}");
            codeBuilder.AddLine();
            codeBuilder.AddLine("function adjustNavigationByPage(isNavByPage) {");
            codeBuilder.AddLine("    navigationByPage(isNavByPage);");
            codeBuilder.AddLine("}");

            codeBuilder.AddLine();
            codeBuilder.AddLine("var preserveDataCurrentState = function () {");
            codeBuilder.AddLine("   return (status() !== 'C' && pageSize() === 0);");
            codeBuilder.AddLine("}");

            codeBuilder.AddLine();
            codeBuilder.AddLine("var query = function (quickSearchJExpression, externalQueryCallBack, noMessages, noDetails) {");
            codeBuilder.AddLine("    if (extendedDataBusiness.OnToolbarAction && !extendedDataBusiness.OnToolbarAction('Query')) return;");
            codeBuilder.AddLine("    if (isProcessing) return;");
            codeBuilder.AddLine("    isProcessing = true;");
            codeBuilder.AddLine("    filteredEntities = (status() === 'C' ? currentDataItem().getCurrentElements() : []);");
            codeBuilder.AddLine("    if (uiSettings != null && uiSettings.noSearch) { " + entitiesRef + " = [currentDataItem()]; status('Q'); return complete(); }");
            codeBuilder.AddLine("    lastJEntitySearch = ((typeof quickSearchJExpression !== 'string') || common.isNullOrEmpty(quickSearchJExpression) ? getQueryFilter(currentDataItem()) : quickSearchJExpression);");
            codeBuilder.AddLine("    if (lastJEntitySearch === 'Error')");
            codeBuilder.AddLine("        return complete();");
            codeBuilder.AddLine("    dataBusiness.showProcessing('Pesquisando informações...');");
            codeBuilder.AddLine("    var hasError = true;");
            codeBuilder.AddLine("    return dataContext.get" + masterEntityName + "ByEntitySearchNoAssociations(lastJEntitySearch, 0, pageSize(), (pageSize() > 0), sortInfo, querySucceeded, complete);");
            codeBuilder.AddLine();
            codeBuilder.AddLine("    function complete() {");
            codeBuilder.AddLine("        isProcessing = false;");
            codeBuilder.AddLine("        dataBusiness.closeProcessing();");

            codeBuilder.AddLine("        if (hasError === true) {");
            codeBuilder.AddLine("           clear();");
            codeBuilder.AddLine("        }");

            codeBuilder.AddLine("    }");
            codeBuilder.AddLine();
            codeBuilder.AddLine("    function querySucceeded(data) {");
            codeBuilder.AddLine("        if (dataBusiness.status() !== 'E') { for (var idx = 0; idx < data.results.length; idx++) { dataContext.initializePOCO(data.results[idx], '" + masterEntityName + "'); } }");
            codeBuilder.AddLine("        hasError = false;");
            codeBuilder.AddLine("        " + entitiesRef + " = data.results;");
            codeBuilder.AddLine("        if (" + entitiesRef + ".length === 0 && ((parentService == null))) {");
            codeBuilder.AddLine("            dataBusiness.closeProcessing();");
            codeBuilder.AddLine("            if (!noMessages) {");
            codeBuilder.AddLine("               messenger.warning('Nenhum registro foi encontrado!');");
            codeBuilder.AddLine("               //Restore clear state");
            codeBuilder.AddLine("               if (restoreLastFilter()) {");
            codeBuilder.AddLine("                  pageCount(1);");
            codeBuilder.AddLine("                  totalItemCount(1);");
            codeBuilder.AddLine("                  currentPage(0);");
            codeBuilder.AddLine("                  status('C');");
            codeBuilder.AddLine("                  goToIndex(0);");
            codeBuilder.AddLine("               }");
            codeBuilder.AddLine("               else {");
            codeBuilder.AddLine("                  clear();");
            codeBuilder.AddLine("               }");
            codeBuilder.AddLine("               return true;");
            codeBuilder.AddLine("            }");
            codeBuilder.AddLine("        }");
            codeBuilder.AddLine("        pageCount( (pageSize() > 0 ? Math.ceil((data.inlineCount ? data.inlineCount : " + entitiesRef + ".length) / pageSize()) : 1) );");
            codeBuilder.AddLine("        totalItemCount((data.inlineCount ? data.inlineCount : " + entitiesRef + ".length));");
            codeBuilder.AddLine("        currentPage(0);");

            codeBuilder.AddLine("        status('Q');");
            codeBuilder.AddLine("        goToIndex(0, noDetails);");

            codeBuilder.AddLine("        if (" + entitiesRef + ".length == 0) dataBusiness.closeProcessing();");
            codeBuilder.AddLine("        if (extendedDataBusiness.OnSearched) extendedDataBusiness.OnSearched();");
            codeBuilder.AddLine("        if (typeof externalQueryCallBack === 'function') externalQueryCallBack();");

            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("};");
            #endregion query
            #region goToIndex
            codeBuilder.AddLine("function goToIndex(index, noDetails) {");
            codeBuilder.AddLine("    if (" + entitiesRef + ".length === 0) { currentDataIndex(0); currentDataItem(null); return true; }");
            codeBuilder.AddLine("    if (index < 0) { index = 0; }");
            codeBuilder.AddLine("    else if (index >= " + entitiesRef + ".length) { index = " + entitiesRef + ".length - 1; }");

            codeBuilder.AddLine("    if (extendedDataBusiness.OnNavigating && status() !== 'C' && currentDataItem() !== null && currentDataItem() !== " + entitiesRef + "[index]) { if (!extendedDataBusiness.OnNavigating(currentDataIndex(), index)) return; }");

            codeBuilder.AddLine("    currentDataIndex(index);");
            codeBuilder.AddLine("    var oldValue = currentDataItem();");
            codeBuilder.AddLine("    currentDataItem(" + entitiesRef + "[index]);");
            codeBuilder.AddLine("    if (status() !== 'C' && currentDataItem() !== null && oldValue !== currentDataItem()) {");

            codeBuilder.AddLine("       if (!noDetails) currentDataItem().fillDetails();");
            codeBuilder.AddLine("       if (extendedDataBusiness.OnNavigated) extendedDataBusiness.OnNavigated(index);");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("}");
            #endregion
            #region goToItem
            codeBuilder.AddLine("function goToItem(item) {");
            codeBuilder.AddLine("        goToIndex(" + entitiesRef + ".indexOf(item));");
            codeBuilder.AddLine("}");
            #endregion
            #region goToKey
            codeBuilder.AddLine("function goToKey(primaryKey, value, currentElement, viewSource) {");
            codeBuilder.AddLine("    if (!viewSource) viewSource = " + entitiesRef + ";");
            codeBuilder.AddLine("    for (var idx = 0; idx < viewSource().length; idx++) {");
            codeBuilder.AddLine("        var dataValue = viewSource()[idx][primaryKey];");
            codeBuilder.AddLine("        if (typeof dataValue === 'function') dataValue = dataValue();");
            codeBuilder.AddLine("        if (dataValue == value) {");
            codeBuilder.AddLine("            if (currentElement) { currentElement(viewSource()[idx]); currentElement().fillDetails(); } else { goToIndex(idx); }");
            codeBuilder.AddLine("            break;");
            codeBuilder.AddLine("        }");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("}");
            #endregion
            #region refresh
            codeBuilder.AddLine("var sortData = function (sortDef) {");
            codeBuilder.AddLine("    if (status() === 'Q' && pageCount() > 1 && sortInfo != sortDef) {");
            codeBuilder.AddLine("       sortInfo = sortDef;");
            codeBuilder.AddLine("       refresh(0, false);");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("var refresh = function (curPage, goLast, callback) {");
            codeBuilder.AddLine("    dataBusiness.showProcessing('Pesquisando informações...');");
            codeBuilder.AddLine("    return dataContext.get" + masterEntityName + "ByEntitySearchNoAssociations(lastJEntitySearch, curPage * pageSize(), pageSize(), false, sortInfo, querySucceeded, complete);");
            codeBuilder.AddLine();
            codeBuilder.AddLine("    function complete() {");
            codeBuilder.AddLine("        dataBusiness.closeProcessing();");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine();
            codeBuilder.AddLine("    function querySucceeded(data) {");
            codeBuilder.AddLine("        if (dataBusiness.status() !== 'E') { for (var idx = 0; idx < data.results.length; idx++) { dataContext.initializePOCO(data.results[idx], '" + masterEntityName + "'); } }");
            codeBuilder.AddLine("        " + entitiesRef + " = data.results;");
            codeBuilder.AddLine("        currentPage(curPage);");
            codeBuilder.AddLine("        goToIndex((goLast ? " + entitiesRef + ".length : 0));");
            codeBuilder.AddLine("        if (callback) callback();");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("};");
            #endregion
            #region clear


            //Add Client Properties
            var clientProperties = clService.ServiceClientProperties.Where(e => !e.Exposed).ToList();
            if (clientProperties.Count > 0)
            {
                codeBuilder.AddLine("//#region Client Private Fields");

                foreach (var cliProp in clientProperties)
                {
                    codeBuilder.AddLine("var " + cliProp.Name + " = " + (cliProp.DefaultValue.IsNullOrEmpty() ? "null" : cliProp.DefaultValue) + ";");
                }

                codeBuilder.AddLine("//#endregion Client Private Fields");
            }


            codeBuilder.AddLine("var clearByUser = function (force) {");
            codeBuilder.AddLine("    if (force != true && !common.isNullOrEmpty(customSearchResult.searchDefinition)) {");
            codeBuilder.AddLine("        dialog.showMessage({ body: 'Deseja limpar a pesquisa avançada?', buttons: 'yesno', defaultButton: 2 })");
            codeBuilder.AddLine("        .then(function (ret) {");
            codeBuilder.AddLine("            if (ret === 'yes') {");
            codeBuilder.AddLine("                customSearchResult.searchDefinition = '';");
            codeBuilder.AddLine("                customSearchResult.serializedSearch = '';");
            codeBuilder.AddLine("                customSearchResult.translatedSearch = '';");
            codeBuilder.AddLine("            }");
            codeBuilder.AddLine("            return clear();");
            codeBuilder.AddLine("         });");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("    else return clear(false, force);");
            codeBuilder.AddLine("}");

            codeBuilder.AddLine("var clear = function (noBindingReport, force) {");
            codeBuilder.AddLine("    if (extendedDataBusiness.OnToolbarAction && !extendedDataBusiness.OnToolbarAction('Clear')) return;");
            codeBuilder.AddLine("    if (extendedDataBusiness.OnClearing) { if (!extendedDataBusiness.OnClearing()) return; }");
            codeBuilder.AddLine("    if (restoreLastFilter((status() === 'C') || (typeof force === 'boolean' && force === true))) return clearComplete({ results: " + entitiesRef + " }, true);");
            codeBuilder.AddLine("    else return dataContext.clear" + masterEntityName + "(getBandeiraRede(), clearComplete);");
            codeBuilder.AddLine();
            codeBuilder.AddLine("    function clearComplete(data, holdRanges) {");
            codeBuilder.AddLine("        dataForUndo = [];");
            codeBuilder.AddLine("        dataCache = []; //Initialize data cache");
            codeBuilder.AddLine("        removedEntities = []; //Initialize removeds");
            codeBuilder.AddLine("        " + entitiesRef + " = data.results;");
            codeBuilder.AddLine("        if (holdRanges != true) dataBusiness.entitySearchRange.clear();");
            codeBuilder.AddLine("        if (typeof noBindingReport === 'boolean' && noBindingReport === true) { pageCount(1); currentPage(0); goToIndex(0); return; }");
            codeBuilder.AddLine("        pageCount(1);");
            codeBuilder.AddLine("        totalItemCount(data.results.length);");
            codeBuilder.AddLine("        currentPage(0);");
            codeBuilder.AddLine("        lastStatus = 'C';");
            codeBuilder.AddLine("        status('C');");
            codeBuilder.AddLine("        goToIndex(0);");

            codeBuilder.AddLine("        if (extendedDataBusiness.OnCleared) { extendedDataBusiness.OnCleared(); }");

            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("};");
            #endregion
            #region hasChanges
            codeBuilder.AddLine("var hasChanges = function () {");
            codeBuilder.AddLine("        return (getAllChanges().length > 0);");
            codeBuilder.AddLine("};");
            #endregion
            #region save
            codeBuilder.AddLine("var onSavingValidation = function (changes) {");

            codeBuilder.AddLine("    if (!changes) changes = getAllChanges();");

            codeBuilder.AddLine("    if (changes.length === 0) { return true; }");

            codeBuilder.AddLine("    if (extendedDataBusiness.OnSaving) { if (!extendedDataBusiness.OnSaving(changes)) { return false; } }");

            codeBuilder.AddLine("    for (var idxChange = 0; idxChange < changes.length; idxChange++) {");
            codeBuilder.AddLine("        var entity = changes[idxChange];");
            codeBuilder.AddLine("        if (typeof entity.OnSaving == 'function') {");
            codeBuilder.AddLine("           if (!entity.OnSaving()) { return false; }");
            codeBuilder.AddLine("        }");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("    return true;");
            codeBuilder.AddLine("}");

            codeBuilder.AddLine("var saveAndContinue = function (externalSaveSucceeded) {");
            codeBuilder.AddLine("   save(false, function() {");
            codeBuilder.AddLine("        edit();");
            codeBuilder.AddLine("        if (typeof externalSaveSucceeded == 'function') {");
            codeBuilder.AddLine("            externalSaveSucceeded();");
            codeBuilder.AddLine("        }");
            codeBuilder.AddLine("   });");
            codeBuilder.AddLine("}");

            codeBuilder.AddLine("var save = function (isExclusion, externalSaveSucceeded) {");
            codeBuilder.AddLine("    if (typeof isExclusion !== 'boolean') isExclusion = false;");
            codeBuilder.AddLine("    if (!isExclusion && extendedDataBusiness.OnToolbarAction && !extendedDataBusiness.OnToolbarAction('Save')) return;");
            codeBuilder.AddLine("    var indexForUndoAction = currentDataIndex();");
            codeBuilder.AddLine("    if (isExclusion) { removeItem(); }");
            codeBuilder.AddLine("    var changes = getAllChanges();");

            codeBuilder.AddLine("    if (!onSavingValidation(changes)) { if (isExclusion) return undo(indexForUndoAction); else return; }");

            codeBuilder.AddLine("    if (dataContext.hasValidationErrors(changes)) { if (isExclusion) return undo(indexForUndoAction); else return; }");
            codeBuilder.AddLine("    isSaving(true);");
            codeBuilder.AddLine("    dataBusiness.showProcessing('Salvando informações...');");
            codeBuilder.AddLine("    if (!isExclusion && currentDataItem()) { currentDataItem().checkForSendingAllRowsToServer(); }");
            codeBuilder.AddLine("    return dataContext.saveChanges(saveSucceeded, saveFailed, complete);");
            codeBuilder.AddLine();
            codeBuilder.AddLine("    function complete() {");
            codeBuilder.AddLine("        dataBusiness.closeProcessing();");
            codeBuilder.AddLine("        isSaving(false);");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine();
            codeBuilder.AddLine("    function saveFailed(error) {");
            codeBuilder.AddLine("        if (isExclusion) return undo(indexForUndoAction); else return;");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine();
            codeBuilder.AddLine("    function saveSucceeded(saveResult) {");

            codeBuilder.AddLine("        dataForUndo = [];");
            codeBuilder.AddLine("        dataCache = []; //Initialize data cache");
            codeBuilder.AddLine("        removedEntities = []; //Initialize removeds");
            codeBuilder.AddLine("        var toList = " + entitiesRef + ";");
            codeBuilder.AddLine("        var fromList = saveResult.results;");
            codeBuilder.AddLine("        for (var idxElem = toList.length - 1; idxElem >= 0; idxElem--) {");
            codeBuilder.AddLine("           if (toList[idxElem].ChangeState === 'D') toList.splice(idxElem, 1);");
            codeBuilder.AddLine("        }");
            codeBuilder.AddLine("        for (var idxElem = toList.length - 1; idxElem >= 0; idxElem--) {");


            codeBuilder.AddLine("                if (toList[idxElem].ChangeState !== 'N') {");

            var tmpKey = uiEntityAdapter.GetTemporaryKey();
            if (!tmpKey.IsNull())
            {
                codeBuilder.AddLine("                   var fromObj = [];");
                codeBuilder.AddLine("                   if (toList[idxElem].ChangeState == 'I') {");
                codeBuilder.AddLine("                       fromObj = _.where(fromList, { Temporary" + tmpKey.Name + ": toList[idxElem]['" + tmpKey.Name + "'] });");
                codeBuilder.AddLine("                   } else {");
                codeBuilder.AddLine("                       fromObj = _.where(fromList, { " + String.Join(",", uiEntityAdapter.GetPrimaryKeys().Select(e => e + ": toList[idxElem]['" + e + "']")) + " });");
                codeBuilder.AddLine("                   }");
            }
            else codeBuilder.AddLine("                           var fromObj = _.where(fromList, { " + String.Join(",", uiEntityAdapter.GetPrimaryKeys().Select(e => e + ": toList[idxElem]['" + e + "']")) + " });");

            codeBuilder.AddLine("                   if (fromObj.length > 0) toList[idxElem].copyDataFrom(fromObj[0], true);");

            codeBuilder.AddLine("                }");


            codeBuilder.AddLine("        }");




            codeBuilder.AddLine("        if (" + entitiesRef + ".length === 0) return clear();");
            codeBuilder.AddLine("        lastStatus = 'Q';");
            codeBuilder.AddLine("        status('Q');");
            codeBuilder.AddLine("        if (" + entitiesRef + ".length > 0) goToIndex(currentDataIndex());");

            codeBuilder.AddLine("        for (var idxChange = 0; idxChange < changes.length; idxChange++) {");
            codeBuilder.AddLine("            var entity = changes[idxChange];");
            codeBuilder.AddLine("            if (entity.isUnchanged() && (typeof entity.TableMedia == 'function') && !common.isNullOrEmpty(entity.TableMedia())) { entity.TableMedia(null); entity.entityAspect.setUnchanged(); }");

            codeBuilder.AddLine("            if (typeof entity.OnSaved == 'function') {");
            codeBuilder.AddLine("               entity.OnSaved();");
            codeBuilder.AddLine("            }");
            codeBuilder.AddLine("        }");

            codeBuilder.AddLine("        if (extendedDataBusiness.OnSaved) { extendedDataBusiness.OnSaved(changes); }");

            codeBuilder.AddLine("        if (typeof externalSaveSucceeded == 'function') {");
            codeBuilder.AddLine("            externalSaveSucceeded();");
            codeBuilder.AddLine("        }");

            if (uiEntityAdapter.RequeryAfterSave)
            {
                codeBuilder.AddLine("        currentDataItem().refreshData();");
            }
            else if (uiEntityAdapter.RequeryDetailsAfterSave)
            {
                codeBuilder.AddLine("        currentDataItem().fillDetails(true, '');");
            }
            else
            {
                foreach (var detail in uiEntityAdapter.SourceEntityAdapters.Where(e => e.RequeryAfterSave))
                {
                    codeBuilder.AddLine("        currentDataItem().fillDetails(true, '" + detail.Name + "');");
                }
            }

            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("};");
            #endregion
            #region undo
            codeBuilder.AddLine("var dataForUndo = []");
            codeBuilder.AddLine("var undo = function (indexForUndoAction) {");
            codeBuilder.AddLine("    if (extendedDataBusiness.OnToolbarAction && !extendedDataBusiness.OnToolbarAction('Undo')) return;");

            codeBuilder.AddLine("    if (extendedDataBusiness.OnCancelling) { if (!extendedDataBusiness.OnCancelling()) return; }");

            codeBuilder.AddLine("    dataContext.cancelChanges();");
            codeBuilder.AddLine("    if ((typeof indexForUndoAction) === 'number' && !navigationByPage()) lastStatus = 'Q';");
            codeBuilder.AddLine("    if (lastStatus === 'C' || dataForUndo.length == 0) {");
            codeBuilder.AddLine("        clear();");
            codeBuilder.AddLine("    } else {");
            codeBuilder.AddLine("        " + entitiesRef + " = dataForUndo;");
            codeBuilder.AddLine("        dataForUndo = [];");
            codeBuilder.AddLine("        dataCache = []; //Initialize data cache");
            codeBuilder.AddLine("        removedEntities = []; //Initialize removeds");
            codeBuilder.AddLine("        status(lastStatus);");
            codeBuilder.AddLine("        goToIndex(((typeof indexForUndoAction) === 'number' ? indexForUndoAction : currentDataIndex()));");

            codeBuilder.AddLine("        if (extendedDataBusiness.OnCancelled)  { extendedDataBusiness.OnCancelled(); }");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("};");
            #endregion
            #region print
            codeBuilder.AddLine("var print = function () {");
            codeBuilder.AddLine("    if (extendedDataBusiness.OnToolbarAction && !extendedDataBusiness.OnToolbarAction('Report')) return;");
            codeBuilder.AddLine("    if (extendedDataBusiness.OnPrinting) { if (!extendedDataBusiness.OnPrinting()) return false; }");

            codeBuilder.AddLine("    if (extendedDataBusiness.OnPrinted) { extendedDataBusiness.OnPrinted(); }");
            codeBuilder.AddLine("    return true;");
            codeBuilder.AddLine("};");
            #endregion
            #region acceptChanges
            codeBuilder.AddLine("var acceptChanges = function () {");
            codeBuilder.AddLine("    if (!navigationByPage()) dataContext.acceptChanges();");
            codeBuilder.AddLine("};");
            #endregion
            #region edit
            codeBuilder.AddLine("var edit = function () {");
            codeBuilder.AddLine("    if (status() === 'E') return;");
            codeBuilder.AddLine("    if (extendedDataBusiness.OnToolbarAction && !extendedDataBusiness.OnToolbarAction('Edit')) return;");
            codeBuilder.AddLine("    if (!canAddChangeEntity()) return;");
            codeBuilder.AddLine("    acceptChanges();");
            codeBuilder.AddLine("    if (extendedDataBusiness.OnEditing) { if (!extendedDataBusiness.OnEditing()) return; }");

            codeBuilder.AddLine("    lastStatus = status();");
            codeBuilder.AddLine("    status('E');");
            codeBuilder.AddLine("    goToIndex(currentDataIndex());");
            codeBuilder.AddLine("    if (lastStatus === 'Q') dataForUndo = [].concat(" + entitiesRef + ");");
            codeBuilder.AddLine("    if (extendedDataBusiness.OnEdited) { extendedDataBusiness.OnEdited(); }");
            codeBuilder.AddLine("};");

            #endregion
            #region setBandeiraRede
            codeBuilder.AddLine("var setBandeiraRede = function () {");
            if (hasBrand)
            {
                codeBuilder.AddLine("   if (getBandeiraRede() > 0) dataContext.loadParameters();");
            }
            codeBuilder.AddLine("};");
            #endregion

            foreach (var entity in uiEntityAdapter.GetCompleteHierarchy())
            {
                #region create
                codeBuilder.AddLine();
                codeBuilder.AddLine("var create" + entity.Name + " = function(" + (entity.TargetEntityAdapter == null ? "" : "parent") + ") {");

                codeBuilder.AddLine("    var entity = dataContext.create" + entity.Name + "(" + (entity.TargetEntityAdapter == null ? "" : "parent") + ");");


                codeBuilder.AddLine("    entity.setBandeiraRede(getBandeiraRede());");
                codeBuilder.AddLine("    entity.setGpecon(getGpecon());");

                if (entity.ExistsClientEvent("OnAdding"))
                {
                    codeBuilder.AddLine("    if (typeof entity.OnAdding == 'function') {");
                    codeBuilder.AddLine("        if (!entity.OnAdding()) { dataContext.deleteEntity(entity); return; }");
                    codeBuilder.AddLine("    }");
                }


                if (entity.TargetEntityAdapter == null)
                {
                    codeBuilder.AddLine("    " + entitiesRef + ".push(entity);");
                }

                if (entity.ExistsClientEvent("OnAdded"))
                {
                    codeBuilder.AddLine("    if (typeof entity.OnAdded == 'function') {");
                    codeBuilder.AddLine("        entity.OnAdded();");
                    codeBuilder.AddLine("    }");
                }

                if (entity.TargetEntityAdapter != null)
                    codeBuilder.AddLine("   if (!common.isNullOrEmpty(parent)) { parent.current" + entity.Name + " = entity; entity.fillDetails(); } ");

                codeBuilder.AddLine("    return entity;");
                codeBuilder.AddLine("};");
                #endregion
                #region createAndNotify
                codeBuilder.AddLine();
                codeBuilder.AddLine("var createAndNotify" + entity.Name + " = function(" + (entity.TargetEntityAdapter == null ? "" : "parent") + ") {");

                codeBuilder.AddLine("    var entity = create" + entity.Name + "(" + (entity.TargetEntityAdapter == null ? "" : "parent") + ");");

                codeBuilder.AddLine("    return entity;");
                codeBuilder.AddLine("};");
                #endregion
            }

            codeBuilder.AddLine("var getBandeiraRede = function() {");
            codeBuilder.AddLine("    if (parentService != null && (typeof parentService.getBandeiraRede === 'function')) return parentService.getBandeiraRede();");
            codeBuilder.AddLine("    else if (!common.isNullOrEmpty(dataBusiness.currentBrands) && dataBusiness.currentBrands.indexOf(',') === -1) return parseInt(dataBusiness.currentBrands);");
            codeBuilder.AddLine("    else return 0;");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("var getCurrentBrands = function() {");
            codeBuilder.AddLine("    if (parentService != null && parentService.hasBrand && (typeof parentService.getCurrentBrands === 'function')) return parentService.getCurrentBrands();");
            codeBuilder.AddLine("    else return (common.isNullOrEmpty(dataBusiness.currentBrands) ? '0' : dataBusiness.currentBrands);");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("var showProcessing = function(message) {");
            codeBuilder.AddLine("    busy.showMessage(message);");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("var closeProcessing = function() {");
            codeBuilder.AddLine("    busy.hideMessage()");
            codeBuilder.AddLine("};");


            codeBuilder.AddLine("var getGpecon = function() {");
            codeBuilder.AddLine("    return shellManagerService.getEconomicGroupId();");
            codeBuilder.AddLine("};");
            #endregion
            #region deleteEntity
            codeBuilder.AddLine("var deleteEntity = function (entity, isMultiSelection) {");

            codeBuilder.AddLine("    var selectedEntities = []");
            codeBuilder.AddLine("    if (isMultiSelection && !common.isNullOrEmpty(complement) && (typeof complement.selectedItems === 'function'))");
            codeBuilder.AddLine("        selectedEntities = complement.selectedItems(false);");

            codeBuilder.AddLine("    if (selectedEntities.length > 0) {");
            codeBuilder.AddLine("       for (var idx = 0; idx < selectedEntities.length; idx++) {");
            codeBuilder.AddLine("           var selectedEntity = selectedEntities[idx];");
            codeBuilder.AddLine("           if (typeof selectedEntity.OnDeleting == 'function') {");
            codeBuilder.AddLine("               if (!selectedEntity.OnDeleting()) return false;");
            codeBuilder.AddLine("           }");
            codeBuilder.AddLine("           dataContext.deleteEntity(selectedEntity);");
            codeBuilder.AddLine("           if (entity.typeName == dataBusiness.rootDataTypeName) " + entitiesRef + ".remove(selectedEntity);");
            codeBuilder.AddLine("           if (typeof selectedEntity.OnDeleted == 'function') {");
            codeBuilder.AddLine("               selectedEntity.OnDeleted();");
            codeBuilder.AddLine("           }");
            codeBuilder.AddLine("           if (selectedEntity.ChangeState == 'D') removedEntities.push(selectedEntity);");
            codeBuilder.AddLine("       }");
            codeBuilder.AddLine("       if (typeof complement.clearSelectedItems === 'function') complement.clearSelectedItems();");
            codeBuilder.AddLine("       return false;");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("    else {");
            codeBuilder.AddLine("       if (typeof entity.OnDeleting == 'function') {");
            codeBuilder.AddLine("           if (!entity.OnDeleting()) return false;");
            codeBuilder.AddLine("       }");
            codeBuilder.AddLine("       dataContext.deleteEntity(entity);");
            codeBuilder.AddLine("       if (typeof entity.OnDeleted == 'function') {");
            codeBuilder.AddLine("           entity.OnDeleted();");
            codeBuilder.AddLine("       }");

            codeBuilder.AddLine("       if (entity.typeName === dataBusiness.rootDataTypeName && typeof isMultiSelection !== 'undefined') {");
            codeBuilder.AddLine("           if (entity.ChangeState == 'D') removedEntities.push(entity);");
            codeBuilder.AddLine("       }");

            codeBuilder.AddLine("    }");

            codeBuilder.AddLine("    return true;");

            codeBuilder.AddLine("};");
            #endregion
            #region addNew

            codeBuilder.AddLine("var canAddChangeEntity = function () {");
            if (hasBrand)
            {
                codeBuilder.AddLine("   if (getBandeiraRede() === 0) {");
                codeBuilder.AddLine("       dialog.showAlert('Bandeira/Rede precisa ser selecionada.', 'Alerta');");
                codeBuilder.AddLine("       return false;");
                codeBuilder.AddLine("   } else {");
                codeBuilder.AddLine("       return true;");
                codeBuilder.AddLine("   }");
            }
            else
                codeBuilder.AddLine("   return true;");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("var addNew = function () {");

            codeBuilder.AddLine("    if (!dataContext.dataParameters.isLoaded) {");
            codeBuilder.AddLine("       setTimeout(function () {");
            codeBuilder.AddLine("           addNew();");
            codeBuilder.AddLine("       }, 1000);");
            codeBuilder.AddLine("       return;");
            codeBuilder.AddLine("    }");

            codeBuilder.AddLine("    if (lastStatus === 'C' && status() === 'Q' && !navigationByPage()) clear();");

            codeBuilder.AddLine("    if (extendedDataBusiness.OnToolbarAction && !extendedDataBusiness.OnToolbarAction('Add')) return;");
            codeBuilder.AddLine("    if (!canAddChangeEntity()) return;");
            codeBuilder.AddLine("    acceptChanges();");

            codeBuilder.AddLine("    if (status() === 'C') {");
            codeBuilder.AddLine("        " + entitiesRef + " = [];");
            codeBuilder.AddLine("    }");

            codeBuilder.AddLine("    if (status() === 'Q') {");
            codeBuilder.AddLine("       dataForUndo = [].concat(" + entitiesRef + ");");
            codeBuilder.AddLine("    }");

            codeBuilder.AddLine("    if (status() !== 'E') {");
            codeBuilder.AddLine("        lastStatus = status();");
            codeBuilder.AddLine("        status('E');");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("    goToItem(create" + masterEntityName + "());");

            codeBuilder.AddLine("};");
            #endregion
            #region remove
            codeBuilder.AddLine("var remove = function () {");
            codeBuilder.AddLine("    if (extendedDataBusiness.OnToolbarAction && !extendedDataBusiness.OnToolbarAction('Delete')) return;");
            codeBuilder.AddLine("    acceptChanges();");

            codeBuilder.AddLine("    dialog.showMessage({ body: 'Deseja realmente excluir o registro selecionado?', buttons: 'yesno', defaultButton: 2 })");
            codeBuilder.AddLine("        .then(function (ret) {");
            codeBuilder.AddLine("            if (ret === 'yes') {");
            codeBuilder.AddLine("                if (!navigationByPage()) { dataForUndo = [].concat(" + entitiesRef + "); save(true); } else { removeItem(); }");
            codeBuilder.AddLine("            }");
            codeBuilder.AddLine("         });");
            codeBuilder.AddLine("};");
            #endregion
            #region removeItem
            codeBuilder.AddLine("var removeParentRelatedItems = function () {");
            codeBuilder.AddLine("    for (var idx = 0; idx < " + entitiesRef + ".length; idx++) { deleteEntity(" + entitiesRef + "[idx]); }");
            codeBuilder.AddLine("    " + entitiesRef + " = [];");
            codeBuilder.AddLine("    goToIndex(0);");
            codeBuilder.AddLine("}");
            codeBuilder.AddLine("var removeItem = function () {");
            codeBuilder.AddLine("    if (deleteEntity(currentDataItem()) === false) return false;");
            codeBuilder.AddLine("    var index = " + entitiesRef + ".indexOf(currentDataItem());");
            codeBuilder.AddLine("    if (currentDataItem().ChangeState == 'D') removedEntities.push(currentDataItem());");
            codeBuilder.AddLine("    " + entitiesRef + ".splice(index, 1);");
            codeBuilder.AddLine("    if (" + entitiesRef + ".length > 0) {");
            codeBuilder.AddLine("        if (status() !== 'E') {");
            codeBuilder.AddLine("            lastStatus = status();");
            codeBuilder.AddLine("            status('E');");
            codeBuilder.AddLine("        }");
            codeBuilder.AddLine("        if (index > 0) { goToIndex(index-1); }");
            codeBuilder.AddLine("        else { goToIndex(0); }");

            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("    else {");
            codeBuilder.AddLine("        goToIndex(0);");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("};");
            #endregion
            #region goFirst
            codeBuilder.AddLine("var goFirst = function (callback) {");
            codeBuilder.AddLine("    if (extendedDataBusiness.OnToolbarAction && !extendedDataBusiness.OnToolbarAction('First')) return;");

            codeBuilder.AddLine("    var item;");
            codeBuilder.AddLine("    if (navigationByPage() || (!(pageCount() === 1 || pageSize() === 0 || currentPage() === 0))) {");
            codeBuilder.AddLine("        item = refresh(0, false, callback);");
            codeBuilder.AddLine("    } else {");
            codeBuilder.AddLine("        item = goToIndex(0);");
            codeBuilder.AddLine("        if (callback) callback();");
            codeBuilder.AddLine("    }");

            codeBuilder.AddLine("    return item;");
            codeBuilder.AddLine("};");
            #endregion
            #region goBack
            codeBuilder.AddLine("var goBack = function (callback) {");
            codeBuilder.AddLine("    if (extendedDataBusiness.OnToolbarAction && !extendedDataBusiness.OnToolbarAction('Back')) return;");

            codeBuilder.AddLine("    var item;");
            codeBuilder.AddLine("    if (navigationByPage() || (!(pageCount() === 1 || pageSize() === 0 || currentPage() === 0) && currentDataIndex() === 0)) {");
            codeBuilder.AddLine("        item = refresh(currentPage()-1, !navigationByPage(), callback);");
            codeBuilder.AddLine("    } else {");
            codeBuilder.AddLine("        item = goToIndex(currentDataIndex()-1);");
            codeBuilder.AddLine("        if (callback) callback();");
            codeBuilder.AddLine("    }");

            codeBuilder.AddLine("    return item;");
            codeBuilder.AddLine("};");
            #endregion
            #region goForward
            codeBuilder.AddLine("var goForward = function (callback) {");
            codeBuilder.AddLine("    if (extendedDataBusiness.OnToolbarAction && !extendedDataBusiness.OnToolbarAction('Next')) return;");

            codeBuilder.AddLine("    var item;");
            codeBuilder.AddLine("    if (navigationByPage() || (!(pageCount() === 1 || pageSize() === 0 || currentPage() === (pageCount()-1)) && currentDataIndex() === (" + entitiesRef + ".length-1))) {");
            codeBuilder.AddLine("        item = refresh(currentPage()+1, false, callback);");
            codeBuilder.AddLine("    } else {");
            codeBuilder.AddLine("        item = goToIndex(currentDataIndex()+1);");
            codeBuilder.AddLine("        if (callback) callback();");
            codeBuilder.AddLine("    }");

            codeBuilder.AddLine("    return item;");
            codeBuilder.AddLine("};");
            #endregion
            #region goLast
            codeBuilder.AddLine("var goLast = function(callback) {");
            codeBuilder.AddLine("    if (extendedDataBusiness.OnToolbarAction && !extendedDataBusiness.OnToolbarAction('Last')) return;");

            codeBuilder.AddLine("    var item;");
            codeBuilder.AddLine("    if (!navigationByPage() && (pageCount() === 1 || pageSize() === 0 || currentPage() === (pageCount()-1))) {");
            codeBuilder.AddLine("        item = goToIndex(" + entitiesRef + ".length-1);");
            codeBuilder.AddLine("        if (callback) callback();");
            codeBuilder.AddLine("    } else {");
            codeBuilder.AddLine("        item = refresh(pageCount()-1, !navigationByPage(), callback);");
            codeBuilder.AddLine("    }");

            codeBuilder.AddLine("    return item;");
            codeBuilder.AddLine("};");
            #endregion

            #region toolbar functions
            codeBuilder.AddLine("//Databar enable control");
            codeBuilder.AddLine("var _canRefreshData = true, _canQuickSearch = true, _canAddNew = true, _canClear = true, _canCustomSearch = true" +
                ", _canDelete = true, _canEdit = true" +
                ", _canLayout = true, _canNavigate = true" +
                ", _canPrint = true, _canSearch = true, _canExport = true;");
            codeBuilder.AddLine("var setSecurity = function(pCanAddNew, pCanClear, pCanCustomSearch, pCanDelete, pCanEdit, pCanLayout, pCanNavigate, pCanPrint, pCanSearch, pCanExport) {");
            codeBuilder.AddLine("   _canAddNew = pCanAddNew;");
            codeBuilder.AddLine("   _canClear = pCanClear;");
            codeBuilder.AddLine("   _canCustomSearch = pCanCustomSearch;");
            codeBuilder.AddLine("   _canDelete = pCanDelete;");
            codeBuilder.AddLine("   _canEdit = pCanEdit;");
            codeBuilder.AddLine("   _canLayout = pCanLayout;");
            codeBuilder.AddLine("   _canNavigate = pCanNavigate;");
            codeBuilder.AddLine("   _canPrint = pCanPrint;");
            codeBuilder.AddLine("   _canSearch = pCanSearch;");
            codeBuilder.AddLine("   _canExport = pCanExport;");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("var isReportComposition = function (reportName) {");
            codeBuilder.AddLine("    if (!common.isNullOrEmpty(reportName))");
            codeBuilder.AddLine("    {");
            codeBuilder.AddLine("        for (var idx in dataContext.entityNames)");
            codeBuilder.AddLine("        {");
            codeBuilder.AddLine("            if (" + (clService.EntityAdapter.IsDashboardFilter ? "" : "dataContext.entityNames[idx].indexOf('ParentComposition') > -1 && ") + "reportName.indexOf(dataBusiness.rootNamespace + '.' + dataContext.entityNames[idx]) > -1)");
            codeBuilder.AddLine("	            return true;");
            codeBuilder.AddLine("        }");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("    return false;");
            codeBuilder.AddLine("}");

            codeBuilder.AddLine("var canGoFirst = (function () { return (status() === 'Q') && _canNavigate && ((!navigationByPage() && currentRecord() > 0) || (navigationByPage() && currentPage() > 0)); });");
            codeBuilder.AddLine("var canGoBack = (function () { return (status() === 'Q') && _canNavigate && ((!navigationByPage() && currentRecord() > 0) || (navigationByPage() && currentPage() > 0)); });");
            codeBuilder.AddLine("var canGoForward = (function () { return (status() === 'Q') && _canNavigate && ((!navigationByPage() && currentRecord() < (totalRecords()-1)) || (navigationByPage() && currentPage() < (pageCount()-1))); });");
            codeBuilder.AddLine("var canGoLast = (function () { return (status() === 'Q') && _canNavigate && ((!navigationByPage() && currentRecord() < (totalRecords()-1)) || (navigationByPage() && currentPage() < (pageCount()-1))); });");
            codeBuilder.AddLine("var canViewInfo = (function () { return (!hasMainTopDataGrid() && status() !== 'E' && totalRecords() > 0); });");
            codeBuilder.AddLine("var canClear = (function () { return ['C', 'Q'].indexOf(status()) >= 0 && _canClear; });");
            codeBuilder.AddLine("var canExport = (function () { return (status() === 'Q' || status() === 'C') && _canExport; });");
            codeBuilder.AddLine("var canGridExport = (function () { return status() === 'Q' && _canExport; });");
            codeBuilder.AddLine("var canQuery = (function () { return status() === 'C' && _canSearch; });");
            codeBuilder.AddLine("var canCustomSearch = (function () { return status() === 'C' && _canCustomSearch; });");
            codeBuilder.AddLine("var canQuickSearch = (function () { return " + (hasQuickSearch ? "(status() === 'Q' || status() === 'C') && _canQuickSearch && _canClear && _canSearch;" : "false;") + " });");
            codeBuilder.AddLine("var hasDataFeed = (function () { return status() === 'C' && _canSearch && dataContext.hasDataFeed && parentService == null; });");
            codeBuilder.AddLine("var canAddNew = (function () { return " + (uiEntityAdapter.IsOlap() ? "false;" : "((['Q', 'C'].indexOf(status()) >= 0) || (status() === 'E' && navigationByPage())) && _canAddNew;") + " });");
            codeBuilder.AddLine("var canRemove = (function () { return " + (uiEntityAdapter.IsOlap() ? "false;" : "(" + entitiesRef + ".length > 0) && ((!navigationByPage() && status() === 'Q')) && _canDelete;") + " });");
            codeBuilder.AddLine("var canEdit = (function () { return " + (uiEntityAdapter.IsOlap() ? "false;" : "status() === 'Q' && _canEdit;") + " });");
            codeBuilder.AddLine("var canRefreshCurrentData = (function () { return " + (uiEntityAdapter.HasDynamicPrimaryKey() || uiEntityAdapter.IsOlap() ? "false;" : "status() === 'Q' && _canSearch && _canRefreshData;") + " });");
            codeBuilder.AddLine("var canUndo = (function () { return status() === 'E' && _canEdit; });");
            codeBuilder.AddLine("var canNavigate = (function () { return  (!canUndo() && !canQuery() && (" + entitiesRef + ".length > 1 || pageCount() > 1) && _canNavigate); });");
            codeBuilder.AddLine("var canPrint = (function () { return ['C', 'Q'].indexOf(status()) >= 0 && _canPrint; });");
            
            codeBuilder.AddLine("var canSave = (function () {");
            codeBuilder.AddLine("       return !isSaving() && status() === 'E' && _canEdit;");
            codeBuilder.AddLine("});");

            codeBuilder.AddLine("var enabledForEditing = (function () {");
            codeBuilder.AddLine("        return ['E', 'C'].indexOf(status()) >= 0;");
            codeBuilder.AddLine("});");

            codeBuilder.AddLine("var isEditable = function () {");
            codeBuilder.AddLine("    return _canEdit;");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("var navigateTo = function (viewName) {");
            codeBuilder.AddLine("    $state.go(viewName);");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("var viewInfo = function () {");
            codeBuilder.AddLine("    if (extendedDataBusiness.OnToolbarAction && !extendedDataBusiness.OnToolbarAction('TableView')) return;");
            codeBuilder.AddLine("    //changeFormView();");
            codeBuilder.AddLine("};");
            #endregion

            #region entitySearhRange

            var listPropertyDateTimeTextBoxForFilterRange = new List<string>();
            var listPropertyNumericTextBoxForFilterRange = new List<string>();
            var listPropertyForLookupFilterRange = new List<string>();
            foreach (var ea in uiEntityAdapter.GetCompleteHierarchy())
            {
                foreach (var p in ea.GetAllInheritanceAttributes())
                {
                    if (p.DisplayControl == DisplayControlType.NumericTextBox)
                        listPropertyNumericTextBoxForFilterRange.Add(ea.Name + p.Name);
                    else if (p.DisplayControl == DisplayControlType.DateTimeTextBox)
                        listPropertyDateTimeTextBoxForFilterRange.Add(ea.Name + p.Name);
                    else if (p.DisplayControl == DisplayControlType.LookUpTextBox)
                        listPropertyForLookupFilterRange.Add(ea.Name + p.Name);
                }
            }

            codeBuilder.AddLine();

            codeBuilder.AddLine(string.Join("\r\n", listPropertyDateTimeTextBoxForFilterRange.Select(p => string.Format("    var _{0}_typeRange = 'R'; var _{0}_begin = null; var _{0}_end = null; var _{0}_predefFilter = null; var _{0}_predefFilterName = null; var _{0}_predefValue = null;", p))
               .Union(listPropertyNumericTextBoxForFilterRange.Select(p => string.Format("    var _{0}_begin = null; var _{0}_end = null;", p)))
               .Union(listPropertyForLookupFilterRange.Select(p => string.Format("    var _{0} = null;", p)))
               ));

            codeBuilder.AddLine("var entitySearchRange = {");
            codeBuilder.AddLine("    predefinedFilters: [],");
            codeBuilder.AddLine("    loadPredefinedFilters: function () {");
            codeBuilder.AddLine("        if (entitySearchRange.predefinedFilters.length == 0) {");
            codeBuilder.AddLine("           //Load Here");
            codeBuilder.AddLine("        }");
            codeBuilder.AddLine("    },");

            codeBuilder.AddLine(string.Join(",\r\n", listPropertyDateTimeTextBoxForFilterRange.Select(p => string.Format("    {0}_typeRange: function (value) {{ if (typeof value !== 'undefined') _{0}_typeRange = value; return _{0}_typeRange; }}, {0}_predefFilter: function (value) {{ if (typeof value !== 'undefined') _{0}_predefFilter = value; return _{0}_predefFilter; }}, {0}_predefFilterName: function (value) {{ if (typeof value !== 'undefined') _{0}_predefFilterName = value; return _{0}_predefFilterName; }}, {0}_predefValue: function (value) {{ if (typeof value !== 'undefined') _{0}_predefValue = value; return _{0}_predefValue; }}, {0}_begin: function (value) {{ if (typeof value !== 'undefined') _{0}_begin = value; return _{0}_begin; }}, {0}_end: function (value) {{ if (typeof value !== 'undefined') _{0}_end = value; return _{0}_end; }}", p))
                .Union(listPropertyNumericTextBoxForFilterRange.Select(p => string.Format("        {0}_begin: function (value) {{ if (typeof value !== 'undefined') _{0}_begin = value; return _{0}_begin; }}, {0}_end: function (value) {{ if (typeof value !== 'undefined') _{0}_end = value; return _{0}_end; }}", p)))
                .Union(listPropertyForLookupFilterRange.Select(p => string.Format("        {0}: function (value) {{ if (typeof value !== 'undefined') _{0} = value; return _{0}; }}", p)))
                ));

            codeBuilder.AddLine("};");
            codeBuilder.AddLine("entitySearchRange.clear = function(){");
            codeBuilder.AddLine(string.Join("\r\n",
                listPropertyDateTimeTextBoxForFilterRange.Select(p => string.Format("        entitySearchRange.{0}_typeRange('R'); entitySearchRange.{0}_begin(null); entitySearchRange.{0}_end(null); entitySearchRange.{0}_predefFilter(null); entitySearchRange.{0}_predefValue(null);", p))
                .Union(listPropertyNumericTextBoxForFilterRange.Select(p => string.Format("        entitySearchRange.{0}_begin(null); entitySearchRange.{0}_end(null);", p)))
                .Union(listPropertyForLookupFilterRange.Select(p => string.Format("        entitySearchRange.{0}(null);", p)))
                ));

            codeBuilder.AddLine("};");

            foreach (var p in listPropertyNumericTextBoxForFilterRange)
            {
                codeBuilder.AddLine("entitySearchRange.has_[0] = function(){ return (entitySearchRange.[0]_begin() != null || entitySearchRange.[0]_end() != null); };".Replace("[0]", p));
            }

            foreach (var p in listPropertyDateTimeTextBoxForFilterRange)
            {
                codeBuilder.AddLine("entitySearchRange.has_[prop] = function(){ return (entitySearchRange.[prop]_typeRange() == 'R' && (entitySearchRange.[prop]_begin() != null || entitySearchRange.[prop]_end() != null) || (entitySearchRange.[prop]_typeRange() == 'P' && !common.isNullOrEmpty(entitySearchRange.[prop]_predefFilter()))); };".Replace("[prop]", p));
            }

            #endregion


            #region grid Template
            codeBuilder.AddLine();

            codeBuilder.AddLine("function openEditor(element, cName, cDataItem_listItem, dataV_parentName, entityName) {");
            codeBuilder.AddLine("   return false;");
            codeBuilder.AddLine("};");

            #endregion

            #region expose dataToolbar methods
            codeBuilder.AddLine();
            codeBuilder.AddLine("var dataToolbar = {");
            codeBuilder.AddLine("        goFirst: goFirst,");
            codeBuilder.AddLine("        goBack: goBack,");
            codeBuilder.AddLine("        goForward: goForward,");
            codeBuilder.AddLine("        goLast: goLast,");
            codeBuilder.AddLine("        adjustNavigationByPage: adjustNavigationByPage,");
            codeBuilder.AddLine("        query: query,");
            codeBuilder.AddLine("        customSearch: customSearch,");
            codeBuilder.AddLine("        customSearchResult: customSearchResult,");
            codeBuilder.AddLine("        refreshCurrentData: refreshCurrentData,");
            codeBuilder.AddLine("        exportData: exportData,");
            codeBuilder.AddLine("        undo: undo,");
            codeBuilder.AddLine("        save: save,");
            codeBuilder.AddLine("        saveAndContinue: saveAndContinue,");
            codeBuilder.AddLine("        addNew: addNew,");
            codeBuilder.AddLine("        remove: remove,");
            codeBuilder.AddLine("        refresh: refresh,");
            codeBuilder.AddLine("        clear: clearByUser,");
            codeBuilder.AddLine("        print: print,");
            codeBuilder.AddLine("        showDataFeedUrl: showDataFeedUrl,");
            codeBuilder.AddLine("        edit: edit,");
            codeBuilder.AddLine("        viewInfo: viewInfo,");
            codeBuilder.AddLine("        lastSearchFilter: lastSearchFilter");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("//Extending dataToolbar object");
            codeBuilder.AddLine("Object.defineProperty(dataToolbar, 'canQuickSearch', { get: canQuickSearch });");
            codeBuilder.AddLine("Object.defineProperty(dataToolbar, 'canClear', { get: canClear });");
            codeBuilder.AddLine("Object.defineProperty(dataToolbar, 'canQuery', { get: canQuery });");
            codeBuilder.AddLine("Object.defineProperty(dataToolbar, 'canCustomSearch', { get: canCustomSearch });");
            codeBuilder.AddLine("Object.defineProperty(dataToolbar, 'canRefreshCurrentData', { get: canRefreshCurrentData });");
            codeBuilder.AddLine("Object.defineProperty(dataToolbar, 'canNavigate', { get: canNavigate });");
            codeBuilder.AddLine("Object.defineProperty(dataToolbar, 'canGoFirst', { get: canGoFirst });");
            codeBuilder.AddLine("Object.defineProperty(dataToolbar, 'canGoBack', { get: canGoBack });");
            codeBuilder.AddLine("Object.defineProperty(dataToolbar, 'canGoForward', { get: canGoForward });");
            codeBuilder.AddLine("Object.defineProperty(dataToolbar, 'canGoLast', { get: canGoLast });");
            codeBuilder.AddLine("Object.defineProperty(dataToolbar, 'canViewInfo', { get: canViewInfo });");
            codeBuilder.AddLine("Object.defineProperty(dataToolbar, 'currentRecordInfo', { get: currentRecordInfo });");
            codeBuilder.AddLine("Object.defineProperty(dataToolbar, 'canAddNew', { get: canAddNew });");
            codeBuilder.AddLine("Object.defineProperty(dataToolbar, 'canEdit', { get: canEdit });");
            codeBuilder.AddLine("Object.defineProperty(dataToolbar, 'canRemove', { get: canRemove });");
            codeBuilder.AddLine("Object.defineProperty(dataToolbar, 'canSave', { get: canSave });");
            codeBuilder.AddLine("Object.defineProperty(dataToolbar, 'canUndo', { get: canUndo });");
            codeBuilder.AddLine("Object.defineProperty(dataToolbar, 'canPrint', { get: canPrint });");
            codeBuilder.AddLine("Object.defineProperty(dataToolbar, 'canExport', { get: canExport });");
            codeBuilder.AddLine("Object.defineProperty(dataToolbar, 'hasDataFeed', { get: hasDataFeed });");
            codeBuilder.AddLine("Object.defineProperty(dataToolbar, 'canGridExport', { get: canGridExport });");
            #endregion

            #region expose service methods
            codeBuilder.AddLine();
            codeBuilder.AddLine("var dataBusiness = {");
            codeBuilder.AddLine("        " + entitiesRef + ": " + entitiesRef + ",");

            codeBuilder.AddLine("        viewName: '" + viewModel + "',");
            codeBuilder.AddLine("        getDataForSaving: getDataForSaving,");
            codeBuilder.AddLine("        currentDataItem: currentDataItem,");
            codeBuilder.AddLine("        currentDataIndex: currentDataIndex,");
            codeBuilder.AddLine("        goToDataItem: goToItem,");
            codeBuilder.AddLine("        goToDataIndex: goToIndex,");
            codeBuilder.AddLine("        exportDataDetails: exportDataDetails,");
            codeBuilder.AddLine("        openEditor: openEditor,");
            codeBuilder.AddLine("        navigationByPage: navigationByPage,");
            codeBuilder.AddLine("        hasMainTopDataGrid: hasMainTopDataGrid,");
            codeBuilder.AddLine("        dataShared: [],");
            codeBuilder.AddLine("        hasChanges: hasChanges,");
            codeBuilder.AddLine("        isSaving: isSaving,");
            codeBuilder.AddLine("        dataToolbar: dataToolbar,");
            codeBuilder.AddLine("        getDataContext: function() { return dataContext; },");
            codeBuilder.AddLine("        getExtendedDataBusiness: function() { return extendedDataBusiness; },");
            codeBuilder.AddLine("        getParentSelectorDataName: getParentSelectorDataName,");
            codeBuilder.AddLine("        getMaxLength: getMaxLength,");
            codeBuilder.AddLine("        status: status,");
            codeBuilder.AddLine("        removeParentRelatedItems: removeParentRelatedItems,");
            codeBuilder.AddLine("        onSavingValidation: onSavingValidation,");
            codeBuilder.AddLine("        goToKey: goToKey,");
            codeBuilder.AddLine("        //Service Events");
            codeBuilder.AddLine("        finalizeCombo: finalizeCombo,");
            codeBuilder.AddLine("        dataCombo: dataCombo,");
            codeBuilder.AddLine("        clearCombo: clearCombo,");
            codeBuilder.AddLine("        dataDomains: dataContext.dataDomains,");
            codeBuilder.AddLine("        //End Service Events");
            codeBuilder.AddLine("        lookUpProperties: dataContext.lookUpProperties,");
            codeBuilder.AddLine("        metadataInfo: dataContext.metadataInfo,");
            codeBuilder.AddLine("        dataExportInfo: dataContext.dataExportInfo,");
            codeBuilder.AddLine("        entityNames: dataContext.entityNames,");
            codeBuilder.AddLine("        lookUpNames: dataContext.lookUpNames,");
            codeBuilder.AddLine("        shellManagerService: dataContext.shellManagerService,");
            codeBuilder.AddLine("        rootBmTypeName: '" + clService.EntityAdapter.PrimaryEntity + "',");
            codeBuilder.AddLine("        rootDataTypeName: '" + masterEntityName + "',");
            codeBuilder.AddLine("        rootNamespace: '" + _designerRoot.GetContextNamespace() + "',");
            codeBuilder.AddLine("        setSecurity: setSecurity,");
            codeBuilder.AddLine("        isReportComposition: isReportComposition,");
            codeBuilder.AddLine("        getServiceAddress: dataContext.getServiceAddress,");
            codeBuilder.AddLine("        getBandeiraRede: getBandeiraRede,");
            codeBuilder.AddLine("        getCurrentBrands: getCurrentBrands,");
            codeBuilder.AddLine("        setBandeiraRede: setBandeiraRede,");
            codeBuilder.AddLine("        entitySearchRange: entitySearchRange,");
            codeBuilder.AddLine("        showProcessing: showProcessing,");
            codeBuilder.AddLine("        closeProcessing: closeProcessing,");
            codeBuilder.AddLine("        isDependentVM: isDependentVM,");
            codeBuilder.AddLine("        allowMultiSelectionInSearch: allowMultiSelectionInSearch,");
            codeBuilder.AddLine("        transactionNumberControl: transactionNumberControl,");
            if (hasQuickSearch)
            {
                codeBuilder.AddLine("        executeQuickSearch: executeQuickSearch,");
                codeBuilder.AddLine("        selectQuickSearch: selectQuickSearch,");
            }

            foreach (var entity in uiEntityAdapter.GetCompleteHierarchy())
            {
                codeBuilder.AddLine("        create" + entity.Name + ": create" + entity.Name + ",");
                codeBuilder.AddLine("        createAndNotify" + entity.Name + ": createAndNotify" + entity.Name + ",");
            }

            foreach (var entity in _designerRoot.EntityAdapters.Where(e => e.DerivedEntityAdapters.Count == 0).ToList())
            {
                foreach (var kpiName in entity.GetAllInheritanceAttributes().Where(e => !e.KpiName.IsNullOrEmpty()).Select(d => d.KpiName).Distinct())
                {
                    codeBuilder.AddLine("        get" + kpiName + "Ranges: get" + kpiName + "Ranges,");
                    codeBuilder.AddLine("        kpi" + kpiName + ": null,");
                    codeBuilder.AddLine("        get" + kpiName + "GaugeGrid: get" + kpiName + "GaugeGrid,");
                }
            }

            codeBuilder.AddLine("        deleteEntity: deleteEntity,");
            codeBuilder.AddLine("        currentBrands: shellManagerService.getBrands(), ");
            codeBuilder.AddLine("        brands: [],");
            codeBuilder.AddLine("        hasBrand: " + hasBrand.ToString().ToLower() + ",");
            codeBuilder.AddLine("        controllerName: dataContext.controllerName,");
            codeBuilder.AddLine("        getJExpression: getJExpression,");
            codeBuilder.AddLine("        getQueryFilter: getQueryFilter,");
            codeBuilder.AddLine("        getTranslatedFilter: getTranslatedFilter,");
            codeBuilder.AddLine("        sortData: sortData,");
            codeBuilder.AddLine("        lastJEntitySearch: function () { return lastJEntitySearch; },");
            codeBuilder.AddLine("        isEditable: isEditable,");
            codeBuilder.AddLine("        setStatus: setStatus,");
            codeBuilder.AddLine("        common: common,");
            codeBuilder.AddLine("        navigateTo: navigateTo,");
            codeBuilder.AddLine("        __moduleId__: '" + packageName + "/controllers/" + clService.Name + "'");


            codeBuilder.AddLine("};");

            //Exposing Properties
            var exposedProperties = clService.ServiceClientProperties.Where(e => e.Exposed).ToArray();
            if (exposedProperties.Length > 0)
            {
                codeBuilder.AddLine("//#region DataBusiness Properties");
                codeBuilder.AddLine("Object.defineProperties(dataBusiness, {");
                string separator = "  ";
                foreach (var prop in exposedProperties)
                {
                    codeBuilder.AddLine("       " + separator + "\"" + prop.Name + "\": { value: " + (prop.DefaultValue.IsNullOrEmpty() ? "null" : prop.DefaultValue) + ", writable: true }");
                    separator = ", ";
                }
                codeBuilder.AddLine("});");
                codeBuilder.AddLine("//#endregion DataBusiness Properties");
            }

            #endregion
            //Extending enabledForEditing and disabledForEditing  object
            codeBuilder.AddLine("Object.defineProperty(dataBusiness, 'enabledForEditing', {");
            codeBuilder.AddLine("    get: enabledForEditing");
            codeBuilder.AddLine("});");
            codeBuilder.AddLine("Object.defineProperty(dataBusiness, 'disabledForEditing', {");
            codeBuilder.AddLine("    get: function(){ return !enabledForEditing(); }");
            codeBuilder.AddLine("});");

            codeBuilder.AddLine();
            codeBuilder.AddLine("dataContext.setCurrentDataBusiness(dataBusiness);");
            codeBuilder.AddLine("extendedDataBusiness.setCurrentDataBusiness(dataBusiness);");

            codeBuilder.AddLine("initService();");
            codeBuilder.AddLine();
            codeBuilder.AddLine("return dataBusiness;");
            codeBuilder.DecreaseIndent();
            codeBuilder.AddLine("};");
            codeBuilder.AddLine();

            codeBuilder.AddLine("module.exports = function(appModule) {");
            codeBuilder.AddLine("   appModule.factory(name, dependencies.concat(dataBusinessFactory));");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("/* jshint ignore:end */");

        }

        /// <summary>
        /// Generate Extended Factory Code.
        /// </summary>
        /// <param name="clService"></param>
        /// <param name="codeBuilder"></param>
        /// <param name="isResource"></param>
        public void GenerateClientErpDataFactoryExtendedCode(ClientLocalService clService, Linx.Tools.CodeBuilder codeBuilder)
        {
            string appName = this._designerRoot.GetAppName();
            string extendexFactoryName = this.GetClientErpDataFactoryName(clService, true);
            Dictionary<string, string> injectors = new Dictionary<string, string>();
            string serviceParameters = "";


            //Check injectors
            if (!clService.ComponentInjection.IsNullOrEmpty())
            {
                var libs = clService.ComponentInjection.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var lib in libs)
                {
                    var key = lib.Left("#").Trim();
                    var value = lib.Right("#").Trim();
                    if (!key.IsNullOrEmpty() && !value.IsNullOrEmpty() && !injectors.ContainsKey(key))
                        injectors.Add(key, value);
                }
            }

            codeBuilder.IncreaseIndent();
            codeBuilder.AddLine("/* jshint ignore:start */");
            codeBuilder.AddLine("'use strict';");

            codeBuilder.AddLine();

            codeBuilder.AddLine("var name = '" + appName + "_" + extendexFactoryName + "';");

            codeBuilder.AddLine();
            codeBuilder.AddLine("var dependencies = [");
            codeBuilder.AddLine("        '$state',");
            codeBuilder.AddLine("        '$log',");
            codeBuilder.AddLine("        '$rootScope',");

            codeBuilder.AddLine("        'commonFactory',");
            codeBuilder.AddLine("        'dialogFactory',");
            codeBuilder.AddLine("        'messengeFactory',");
            codeBuilder.AddLine("        'shellManagerService'" + (injectors.Count > 0 ? "," : ""));


            //Add injectors
            if (injectors.Count > 0)
            {
                var keys = injectors.Keys.ToArray();
                for (int idx = 0; idx < keys.Length; idx++)
                {
                    string key = keys[idx];
                    codeBuilder.AddLine("        '" + injectors[key] + "'" + (idx < keys.Length - 1 ? "," : ""));
                    serviceParameters += ", " + key;
                }
            }

            codeBuilder.AddLine("];");
            codeBuilder.AddLine();

            codeBuilder.AddLine("var extendedDataBusinessFactory = function ($state, $log, $rootScope, common, dialog, messenger, shellManagerService" + serviceParameters + ") {");

            codeBuilder.IncreaseIndent();

            //Add Client Events    
            codeBuilder.AddLine("//#region Client Events");
            var serviceClientEvents = clService.GetClientEvents();
            var serviceClientEventsCompl = (serviceClientEvents.Count == 0 ? clService.GetClientEventNames() : clService.GetClientEventNames().Where(e => !serviceClientEvents.Any(ev => ev.Name == e))).ToArray();
            if (serviceClientEvents.Count > 0)
            {
                codeBuilder.AddLine("//#region Client Messages");
                foreach (var cliEvent in serviceClientEvents.Where(e => e.IsOutputMessage))
                {
                    codeBuilder.AddLine("var " + cliEvent.Name + " = function (" + String.Join(", ", cliEvent.Parameters.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries).Select(e => e.Right(" "))) + ") {");
                    codeBuilder.AddLine("$rootScope.$broadcast('" + this._designerRoot.GetAppName() + "_" + this.GetClientErpDataFactoryName(clService) + "::" + cliEvent.Name + "', { " + String.Join(", ", cliEvent.Parameters.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries).Select(e => e.Right(" ") + ": " + e.Right(" "))) + " });");
                    codeBuilder.AddLine("}");
                }

                codeBuilder.AddLine("//#endregion Client Messages");

                foreach (var cliEvent in serviceClientEvents.Where(e => !e.IsOutputMessage))
                {
                    codeBuilder.AddLine("var " + cliEvent.Name + " = function (" + String.Join(", ", cliEvent.Parameters.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries).Select(e => e.Right(" "))) + ") {");
                    codeBuilder.AddLine(cliEvent.MacroScript.IsNullOrEmpty() ? (cliEvent.ReturnType.ToLower().Contains("void") ? "" : "return " + this.GetClientErpDefaultValueByType(cliEvent.ReturnType, true) + ";") : MacroEngineHelper.ReplaceMacros(cliEvent.MacroScript, MacroOutputType.JavaScriptMobile, _designerRoot) + (cliEvent.ReturnType.ToLower().Contains("bool") ? "\r\nreturn " + this.GetClientErpDefaultValueByType(cliEvent.ReturnType, true) + ";" : ""));
                    codeBuilder.AddLine("}");
                }
            }

            foreach (var eventName in serviceClientEventsCompl)
            {
                string eventConfig = clService.GetClientEventDefinition(eventName);
                string returnType = eventConfig.Left(" | ");
                string parameters = eventConfig.Right(" | ");

                codeBuilder.AddLine("var " + eventName + " = function (" + String.Join(", ", parameters.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries).Select(e => e.Right(" "))) + ") {");
                codeBuilder.AddLine((returnType.ToLower().Contains("void") ? "" : "return " + this.GetClientErpDefaultValueByType(returnType, true) + ";"));
                codeBuilder.AddLine("}");
            }

            codeBuilder.AddLine("//#endregion Client Events");


            #region expose service methods
            codeBuilder.AddLine();
            codeBuilder.AddLine("var dataBusiness = null;");
            codeBuilder.AddLine("var extendedDataBusiness = {");

            //Exposing Events/Methods
            serviceClientEventsCompl = clService.GetClientEventNames().ToArray();
            foreach (var eventName in serviceClientEventsCompl)
            {
                codeBuilder.AddLine("        " + eventName + ": " + eventName + ",");
            }

            foreach (var evt in serviceClientEvents.Where(e => e.Exposed && !serviceClientEventsCompl.Contains(e.Name)))
            {
                codeBuilder.AddLine("        " + evt.Name + ": " + evt.Name + ",");
            }

            codeBuilder.AddLine("     setCurrentDataBusiness: function(curDataBusiness) { dataBusiness = curDataBusiness; }");
            codeBuilder.AddLine("};");
            #endregion

            codeBuilder.AddLine();
            codeBuilder.AddLine("return extendedDataBusiness;");
            codeBuilder.DecreaseIndent();
            codeBuilder.AddLine("};");
            codeBuilder.AddLine();

            codeBuilder.AddLine("module.exports = function(appModule) {");
            codeBuilder.AddLine("   appModule.factory(name, dependencies.concat(extendedDataBusinessFactory));");
            codeBuilder.AddLine("};");

            codeBuilder.AddLine("/* jshint ignore:end */");


        }

        /// <summary>
        /// Generate Controller Code.
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="codeBuilder"></param>

        #region Auxiliary Methods

        /// <summary>
        /// ClientErp Context Name.
        /// </summary>
        /// <returns></returns>
        public string GetClientErpDataServiceApiName()
        {
            return _designerRoot.GetDirectContextName() + "ClientErpService";
        }

        /// <summary>
        /// ClientErp Controller Name.
        /// </summary>
        /// <param name="clService"></param>
        /// <returns></returns>
        public string GetClientErpDataFactoryName(ClientLocalService clService, bool isExtended = false)
        {
            if (clService == null)
                return "";
            else
                return clService.Name + (isExtended ? "Extended" : "") + "ClientErpFactory";
        }

        public ProjectItem GetResourceFile(string name)
        {
            string folderName = "ClientResources";
            Project current = this._designerRoot.GetEadProject();

            if (!current.IsNull())
            {
                ProjectItem folder = this._designerRoot.GetProjectItemByName(current, folderName);
                if (folder == null)
                    folder = current.ProjectItems.AddFolder(folderName, Constants.vsProjectItemKindPhysicalFolder);

                if (folder != null)
                {
                    return this._designerRoot.GetProjectItemByName(current, name + ".res", true);
                }
            }

            return null;
        }


        /// <summary>
        /// Generate lookup finalizers
        /// </summary>
        /// <param name="codeBuilder"></param>
        /// <returns></returns>
        private void GenerateLookUpJsFunctions(Linx.Tools.CodeBuilder codeBuilder, string entitiesRef)
        {
            List<string> usedLookUps = new List<string>();

            codeBuilder.AddLine();
            codeBuilder.AddLine("//#region LookUps Finalizers");

            foreach (var entity in _designerRoot.EntityAdapters)
            {
                foreach (var lookUp in entity.GetAllLookUpsInfo(true))
                {
                    if (usedLookUps.Contains(lookUp.Name))
                        continue;

                    //Add Used Lookup
                    usedLookUps.Add(lookUp.Name);

                    codeBuilder.AddLine("   ownerReference.execute" + lookUp.Name + " = function (lookupProperty, entityProperty, pageSkip, pageSize, queryCallback) {");
                    codeBuilder.AddLine("       if (!lookupProperty) { if (queryCallback) queryCallback(true, [], 0); return null; }");

                    codeBuilder.AddLine("       if (common.isNullOrEmpty(entityProperty)) entityProperty = lookupProperty;");

                    codeBuilder.AddLine("       var valueToSearch = ownerReference[entityProperty];");
                    codeBuilder.AddLine("       var extraFilters = '';");
                    codeBuilder.AddLine("       if (ownerReference.canGetClientFilter('" + lookUp.Name + "')) {");
                    codeBuilder.AddLine("           if (typeof ownerReference['BeforeGet" + lookUp.Name + "Query'] == 'function') {");
                    codeBuilder.AddLine("               var customFilter = ownerReference['BeforeGet" + lookUp.Name + "Query']();");
                    codeBuilder.AddLine("               if (customFilter === 'Error') { if (queryCallback) queryCallback(true, [], 0); return null; }");
                    codeBuilder.AddLine("               if (!common.isNullOrEmpty(customFilter)) { extraFilters = (common.isNullOrEmpty(extraFilters) ? '' : extraFilters + ';') + customFilter; }");
                    codeBuilder.AddLine("           }");
                    codeBuilder.AddLine("           if (typeof ownerReference['getSubQueryFilterFrom" + lookUp.Name + "'] == 'function') {");
                    codeBuilder.AddLine("               var customFilter = ownerReference['getSubQueryFilterFrom" + lookUp.Name + "'](lookupProperty);");
                    codeBuilder.AddLine("               if (customFilter === 'Error') { if (queryCallback) queryCallback(true, [], 0); return null; }");
                    codeBuilder.AddLine("               if (!common.isNullOrEmpty(customFilter)) { extraFilters = (common.isNullOrEmpty(extraFilters) ? '' : extraFilters + ';') + customFilter; }");
                    codeBuilder.AddLine("           }");
                    codeBuilder.AddLine("       }");
                    codeBuilder.AddLine("       var completeExpression = common.getLookUpJEntityExpression('" + lookUp.Name + "', ownerReference, lookupProperty, valueToSearch, extraFilters, entityProperty, dialog);");
                    codeBuilder.AddLine("       if (completeExpression === 'Error') { if (queryCallback) queryCallback(true, [], 0); return null; }");

                    codeBuilder.AddLine("       var callbackSucceeded = function (data) { if (queryCallback) queryCallback(false, data.results, data.inlineCount); };");
                    codeBuilder.AddLine("       var callbackFailed = function (error) { if (queryCallback) queryCallback(true, [], 0); queryFailed(error); };");
                    codeBuilder.AddLine("       return dataContext.get" + lookUp.Name + "ByEntitySearch(completeExpression, lookupProperty, pageSkip, pageSize, 'asc', callbackSucceeded, function() { }, callbackFailed);");
                    codeBuilder.AddLine("   };");

                    codeBuilder.AddLine();
                    codeBuilder.AddLine("   ownerReference.finalize" + lookUp.Name + " = function (lookupProperty, entityProperty, selectedElements) {");

                    codeBuilder.AddLine("       if (!selectedElements)");
                    codeBuilder.AddLine("           return;");

                    codeBuilder.AddLine("       if ((typeof selectedElements.length) === 'undefined') {");
                    codeBuilder.AddLine("           selectedElements = [selectedElements];");
                    codeBuilder.AddLine("       }");

                    codeBuilder.AddLine();
                    codeBuilder.AddLine("       //Mount query list for QBE");
                    codeBuilder.AddLine("       if (dataBusiness && dataBusiness.status() == 'C' && selectedElements != null && selectedElements.length > 1) {");
                    codeBuilder.AddLine("           var results = '';");
                    codeBuilder.AddLine("           for (var index = 0; index < selectedElements.length; index++) {");
                    codeBuilder.AddLine("               results += (index == 0 ? '' : ',') + selectedElements[index][lookupProperty].toString().trim();");
                    codeBuilder.AddLine("           }");
                    codeBuilder.AddLine("           results = '[' + results + ']';");
                    codeBuilder.AddLine("           ownerReference[entityProperty] = results;");
                    codeBuilder.AddLine("           dataBusiness.entitySearchRange[ownerReference.typeName + entityProperty](results);");
                    codeBuilder.AddLine("           return;");
                    codeBuilder.AddLine("       }");
                    codeBuilder.AddLine();

                    codeBuilder.AddLine("       var replaceTo = ownerReference;");

                    var parentLink = entity.GetParentLinkRelation();

                    if (lookUp.CheckExistence)
                    {
                        codeBuilder.AddLine("       var originalRow = replaceTo;");
                        codeBuilder.AddLine("       var hasDisconsideredElement = false;");
                    }

                    if ((lookUp.IsMultiSelection || lookUp.CheckExistence) && !parentLink.IsNull())
                    {
                        codeBuilder.AddLine("       var parent = replaceTo." + parentLink.TargetEntityAdapter.Name + ";");
                    }

                    codeBuilder.AddLine("       for (var i = 0; i < selectedElements.length; i++)");
                    codeBuilder.AddLine("       {");
                    codeBuilder.AddLine("           var selectedElement = selectedElements[i];");


                    if (lookUp.CheckExistence)
                    {
                        string cmopareExpression = "";
                        foreach (var prop in lookUp.Properties.Where(e => e.IsPrimaryKey))
                        {
                            cmopareExpression += (cmopareExpression.IsNullOrEmpty() ? "" : " && ") + "curList[idx]." + prop.EntityPropertyRelated + " === selectedElement['" + prop.Name + "']";
                        }
                        if (!cmopareExpression.IsNullOrEmpty())
                        {
                            codeBuilder.AddLine("           var isInvalidElement = false;");
                            codeBuilder.AddLine("           var curList = " + (parentLink.IsNull() ? "dataBusiness." + entitiesRef + "" : "parent." + entity.Name + "List") + ";");
                            codeBuilder.AddLine("           for (var idx = 0; idx < curList.length; idx++) {");
                            codeBuilder.AddLine("              if (curList[idx] != originalRow && " + cmopareExpression + ") {");
                            codeBuilder.AddLine("                  isInvalidElement = true;");
                            codeBuilder.AddLine("                  hasDisconsideredElement = true;");
                            codeBuilder.AddLine("                  break;");
                            codeBuilder.AddLine("              }");
                            codeBuilder.AddLine("           }");
                            codeBuilder.AddLine("           if (isInvalidElement) continue;");
                        }
                    }

                    if (lookUp.IsMultiSelection)
                    {
                        codeBuilder.AddLine("           if (i !== 0" + (lookUp.CheckExistence ? " && !(hasDisconsideredElement && i == 1)" : "") + ") {");
                        codeBuilder.AddLine("               replaceTo = dataBusiness.create" + entity.Name + "(" + (parentLink.IsNull() ? "" : "parent") + ");");
                        codeBuilder.AddLine("           }");
                    }

                    var properties = entity.GetAllInheritanceAttributes();
                    foreach (var prop in lookUp.Properties.Where(e => !e.EntityPropertyRelated.IsNullOrEmpty()))
                    {
                        var relatedProp = properties.FirstOrDefault(e => e.Name == prop.EntityPropertyRelated);
                        if (relatedProp != null)
                        {
                            codeBuilder.AddLine("           if (selectedElement.hasOwnProperty('" + prop.Name + "') && (replaceTo.hasOwnProperty('" + prop.EntityPropertyRelated + "') || replaceTo.__proto__.hasOwnProperty('" + prop.EntityPropertyRelated + "')))");
                            codeBuilder.AddLine("           {");
                            codeBuilder.AddLine("               replaceTo." + prop.EntityPropertyRelated + " = selectedElement." + prop.Name + ";");
                            codeBuilder.AddLine("           }");
                            codeBuilder.AddLine("           else if (replaceTo.hasOwnProperty('" + prop.EntityPropertyRelated + "') || replaceTo.__proto__.hasOwnProperty('" + prop.EntityPropertyRelated + "')) {");
                            codeBuilder.AddLine("               replaceTo." + prop.EntityPropertyRelated + " = " + this.GetClientErpDefaultValueByType(relatedProp.Datatype + (relatedProp.IsNullable() ? "?" : "")) + ";");
                            codeBuilder.AddLine("           }");
                        }
                    }

                    if (entity.ExistsClientEvent("OnLookedUp" + lookUp.Name))
                    {
                        codeBuilder.AddLine("           if (typeof replaceTo.OnLookedUp" + lookUp.Name + " == 'function') {");
                        codeBuilder.AddLine("               replaceTo.OnLookedUp" + lookUp.Name + "(selectedElement);");
                        codeBuilder.AddLine("           }");
                    }

                    codeBuilder.AddLine("       }");

                    if (lookUp.CheckExistence)
                    {
                        codeBuilder.AddLine("       if (hasDisconsideredElement) {");
                        codeBuilder.AddLine("          dialog.showAlert('Itens que já estão sendo utilizados foram desconsiderados nessa seleção!', 'Informação');");
                        codeBuilder.AddLine("       }");
                    }

                    codeBuilder.AddLine("   };");

                    codeBuilder.AddLine();
                    codeBuilder.AddLine("   ownerReference.clear" + lookUp.Name + " = function () {");

                    foreach (var prop in lookUp.Properties.Where(e => !e.EntityPropertyRelated.IsNullOrEmpty()))
                    {
                        var relatedProp = entity.GetAllInheritanceAttributes().FirstOrDefault(e => e.Name == prop.EntityPropertyRelated);
                        if (relatedProp != null)
                            codeBuilder.AddLine("       ownerReference." + relatedProp.Name + " = " + this.GetClientErpDefaultValueByType(relatedProp.Datatype + (relatedProp.IsNullable() ? "?" : "")) + ";");
                    }

                    codeBuilder.AddLine("   }");

                }
            }


            codeBuilder.AddLine("//#endregion");
        }

        /// <summary>
        /// Get Default Value.
        /// </summary>
        /// <param name="dataType"></param>
        /// <param name="invertBoolean"></param>
        /// <returns></returns>
        private string GetClientErpDefaultValueByType(string dataType, bool invertBoolean = false)
        {
            var defaultValue = "null";
            if (!dataType.Contains("Nullable<") && !dataType.Contains("?"))
            {
                dataType = dataType.RemoveNullDefinition();
                if (dataType.InList(new string[] { "byte", "int16", "int32", "int", "long", "short", "int64", "sbyte", "uint16", "uint32", "uint64", "single", "double", "decimal" }))
                    defaultValue = "0";
                else if (dataType.Contains("datetime"))
                    defaultValue = "common.getCurrentDate()";
                else if (dataType.Contains("bool"))
                    defaultValue = invertBoolean ? "true" : "false";
                else
                    defaultValue = "''";
            }

            return defaultValue;
        }

        /// <summary>
        /// Generate JS query methods.
        /// </summary>
        /// <param name="codeBuilder"></param>
        /// <returns></returns>
        private string GenerateDataServiceJsQueryActions(Linx.Tools.CodeBuilder codeBuilder)
        {
            string result = String.Empty, orderBy;
            codeBuilder.AddLine();
            codeBuilder.AddLine("//#region Get LookUps");
            List<string> lookUps = new List<string>();

            foreach (var lookUp in _designerRoot.LookUpAdapters)
            {
                result += (result.IsNullOrEmpty() ? String.Empty : ",\r\n") + "        get" + lookUp.Name + "ByEntitySearch: get" + lookUp.Name + "ByEntitySearch";
                codeBuilder.AddLine();
                codeBuilder.AddLine("var get" + lookUp.Name + "ByEntitySearch = function (jEntitySearch, propertyName, skip, take, direction, qSucceeded, qFin, qFailed) {");
                codeBuilder.AddLine("    var query = 'Get" + lookUp.Name + "ByEntitySearch?';");
                //required parameters
                codeBuilder.AddLine("    query += 'jEntitySearch=' + jEntitySearch;");
                codeBuilder.AddLine("    query += '&propertyName=' + propertyName;");
                //odata parameters
                codeBuilder.AddLine("    query += '&$inlinecount=allpages&$orderby=' + propertyName + (direction === 'descending' ? ' desc' : ' asc');");
                codeBuilder.AddLine();
                codeBuilder.AddLine("    if (take > 0)");
                codeBuilder.AddLine("       query += '&$skip=' + skip.toString() + '&$top=' + take.toString();");
                codeBuilder.AddLine();
                codeBuilder.AddLine("    return httpFactory.httpGet(getDataServiceUrl, query, localQuerySucceeded, localQueryFailed);");
                codeBuilder.AddLine();
                codeBuilder.AddLine("    function localQuerySucceeded(data) {");
                codeBuilder.AddLine("        if (qSucceeded)");
                codeBuilder.AddLine("            qSucceeded(data);");
                codeBuilder.AddLine("        if (qFin)");
                codeBuilder.AddLine("            qFin();");
                codeBuilder.AddLine("    }");
                codeBuilder.AddLine();
                codeBuilder.AddLine("    function localQueryFailed(error) {");
                codeBuilder.AddLine("        if (qFin)");
                codeBuilder.AddLine("            qFin();");
                codeBuilder.AddLine("        if (qFailed)");
                codeBuilder.AddLine("            qFailed(error);");
                codeBuilder.AddLine("        else");
                codeBuilder.AddLine("            queryFailed(error);");
                codeBuilder.AddLine("    }");
                codeBuilder.AddLine("};");
                lookUps.Add(lookUp.Name);
            }


            List<PublicationStructure> publishers = new List<PublicationStructure>();
            if (_designerRoot.PublisherAutoReference != null)
                publishers.Add(_designerRoot.PublisherAutoReference);
            publishers.AddRange(_designerRoot.Subscriptions.Where(e => e.Publisher != null).Select(e => e.Publisher));

            if (publishers.Count > 0)
                codeBuilder.AddLine("var lookUpExternalManagers = [];");

            foreach (var pub in publishers)
            {
                foreach (var entity in pub.Entities.Where(e => _designerRoot.EntityAdapterRepresentations.Any(ent => ent.TargetEntityAdapterName == e.Name)))
                {
                    foreach (string lookUpName in entity.Properties.Where(e => !e.LookUpInfo.IsNullOrEmpty()).Select(e => LookUpAdapter.GetLookUpName(e.LookUpInfo)).Distinct().Where(e => !lookUps.Contains(e)))
                    {
                        result += (result.IsNullOrEmpty() ? String.Empty : ",\r\n") + "        get" + lookUpName + "ByEntitySearch: get" + lookUpName + "ByEntitySearch";
                        codeBuilder.AddLine();
                        codeBuilder.AddLine("var get" + lookUpName + "ByEntitySearch = function (jEntitySearch, order, skip, take, direction, successCallback, errorCallback) {");
                        codeBuilder.AddLine("    var query = 'Get" + lookUpName + "ByEntitySearch?$inlinecount=allpages';");
                        codeBuilder.AddLine("    query += '&$orderby=' + order + (direction === 'descending' ? ' desc' : ' asc');");
                        codeBuilder.AddLine();
                        codeBuilder.AddLine("    if ((typeof jEntitySearch !== 'undefined') && jEntitySearch !== null)");
                        codeBuilder.AddLine("        query += '&jEntitySearch=' + jEntitySearch;");
                        codeBuilder.AddLine();
                        codeBuilder.AddLine("    if (take > 0)");
                        codeBuilder.AddLine("       query += '&$skip=' + skip.toString() + '&$top=' + take.toString();");
                        codeBuilder.AddLine();

                        codeBuilder.AddLine("    return httpFactory.httpGet(getDataServiceUrl, query, successCallback, errorCallback);");
                        codeBuilder.AddLine("};");
                        lookUps.Add(lookUpName);
                    }
                }
            }

            codeBuilder.AddLine("//#endregion");

            codeBuilder.AddLine("//#region Get KPI Ranges");
            foreach (var entity in _designerRoot.EntityAdapters.Where(e => e.DerivedEntityAdapters.Count == 0).ToList())
            {
                foreach (var kpiName in entity.GetAllInheritanceAttributes().Where(e => !e.KpiName.IsNullOrEmpty()).Select(d => d.KpiName).Distinct())
                {
                    codeBuilder.AddLine();

                    result += (result.IsNullOrEmpty() ? String.Empty : ",\r\n") + "        get" + kpiName + "Ranges: get" + kpiName + "Ranges";
                    codeBuilder.AddLine();
                    codeBuilder.AddLine("var get" + kpiName + "Ranges = function (qSucceeded, qFin, qFailed) {");
                    codeBuilder.AddLine("    var query = 'Get" + kpiName + "Ranges';");
                    codeBuilder.AddLine("    return httpFactory.httpGet(getDataServiceUrl, query, localQuerySucceeded, localQueryFailed);");
                    codeBuilder.AddLine();
                    codeBuilder.AddLine("    function localQuerySucceeded(data) {");
                    codeBuilder.AddLine("        if (qSucceeded)");
                    codeBuilder.AddLine("            qSucceeded(data);");
                    codeBuilder.AddLine("        if (qFin)");
                    codeBuilder.AddLine("            qFin();");
                    codeBuilder.AddLine("    }");
                    codeBuilder.AddLine();
                    codeBuilder.AddLine("    function localQueryFailed(error) {");
                    codeBuilder.AddLine("        if (qFin)");
                    codeBuilder.AddLine("            qFin();");
                    codeBuilder.AddLine("        if (qFailed)");
                    codeBuilder.AddLine("            qFailed(error);");
                    codeBuilder.AddLine("        else");
                    codeBuilder.AddLine("            queryFailed(error);");
                    codeBuilder.AddLine("    }");
                    codeBuilder.AddLine("};");

                    codeBuilder.AddLine();
                }
            }
            codeBuilder.AddLine("//#endregion");
            codeBuilder.AddLine();

            codeBuilder.AddLine("//#region Get Combo LookUp");
            codeBuilder.AddLine("var getResultsCombo = function (lookupName, fieldName, current, callback) {");
            codeBuilder.AddLine("    eval('if (current.execute' + lookupName + ') { current.execute' + lookupName + '(fieldName, fieldName, 0, -1, function (hasError, resultsArray, inlineCount) {  if (callback) callback(resultsArray); }); }');");
            codeBuilder.AddLine("};");
            codeBuilder.AddLine("//#endregion Get Combo LookUp");

            codeBuilder.AddLine();
            codeBuilder.AddLine("//#region Get Business Entities");

            result += (result.IsNullOrEmpty() ? String.Empty : ",\r\n") + "                getBmEntityProperties: getBmEntityProperties";
            codeBuilder.AddLine();
            codeBuilder.AddLine("var getBmEntityProperties = function (entityName, parentDataPath, qSucceeded, qFin, qFailed) {");
            codeBuilder.AddLine("    return httpFactory.httpGet(getDataServiceUrl, 'GetBmEntityProperties?entityName=' + entityName + '&parentDataPath=' + parentDataPath, localQuerySucceeded, localQueryFailed);");
            codeBuilder.AddLine();
            codeBuilder.AddLine("    function localQuerySucceeded(data) {");
            codeBuilder.AddLine("        if (qSucceeded)");
            codeBuilder.AddLine("            qSucceeded(data);");
            codeBuilder.AddLine("        if (qFin)");
            codeBuilder.AddLine("            qFin();");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine();
            codeBuilder.AddLine("    function localQueryFailed(error) {");
            codeBuilder.AddLine("        if (qFin)");
            codeBuilder.AddLine("            qFin();");
            codeBuilder.AddLine("        if (qFailed)");
            codeBuilder.AddLine("            qFailed(error);");
            codeBuilder.AddLine("        else");
            codeBuilder.AddLine("            queryFailed(error);");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("};");

            foreach (var entity in _designerRoot.EntityAdapters)
            {
                orderBy = entity.GetOrderByCommand();
                result += (result.IsNullOrEmpty() ? String.Empty : ",\r\n") + "                clear" + entity.Name + ": clear" + entity.Name;
                codeBuilder.AddLine();
                codeBuilder.AddLine("var clear" + entity.Name + " = function (idBandeiraRede, complete) {");

                if (entity.EnableQBE)
                {
                    Action<EntityAdapter, EntityAdapter> createEmptyStructure = null;
                    createEmptyStructure = (parentElement, detElement) =>
                    {
                        if (detElement.EnableQBE)
                        {
                            var parentLink = detElement.GetParentLinkRelation();
                            if (!parentLink.IsDashboard)
                            {
                                codeBuilder.AddLine("    var ref" + detElement.Name + " = createEmpty" + detElement.Name + "();");
                                codeBuilder.AddLine("    ref" + parentElement.Name + "." + detElement.Name + "List = [ref" + detElement.Name + "];");
                                codeBuilder.AddLine("    ref" + parentElement.Name + ".current" + detElement.Name + " = ref" + detElement.Name + ";");

                                detElement.SourceEntityAdapters.ToList().ForEach(e => createEmptyStructure(detElement, e));
                            }
                        }
                    };

                    codeBuilder.AddLine("    enableChangeTrack = false;");
                    codeBuilder.AddLine("    var ref" + entity.Name + " = createEmpty" + entity.Name + "();");
                    entity.SourceEntityAdapters.ToList().ForEach(e => createEmptyStructure(entity, e));
                    codeBuilder.AddLine("    if (complete) complete({ results: [ ref" + entity.Name + " ] });");
                    codeBuilder.AddLine("    enableChangeTrack = true;");
                }
                else
                    codeBuilder.AddLine("    if (complete) complete({ results: [] });");

                codeBuilder.AddLine("    return true;");
                codeBuilder.AddLine("};");


                result += (result.IsNullOrEmpty() ? String.Empty : ",\r\n") + "                get" + entity.Name + ": get" + entity.Name;
                codeBuilder.AddLine();
                codeBuilder.AddLine("var get" + entity.Name + " = function (jEntitySearch, qSucceeded, qFin, qFailed) {");
                codeBuilder.AddLine("    var query = 'Get" + entity.Name + "?$inlinecount=none';");
                if (!orderBy.IsNullOrEmpty())
                    codeBuilder.AddLine("    query += '&$orderby=' + order;");
                codeBuilder.AddLine("    ;");
                codeBuilder.AddLine();

                codeBuilder.AddLine("    if ((typeof jEntitySearch !== 'undefined') && jEntitySearch !== null)");
                codeBuilder.AddLine("        query += '&jEntitySearch=' + jEntitySearch;");
                codeBuilder.AddLine();
                codeBuilder.AddLine("    return httpFactory.httpGet(getDataServiceUrl, query, localQuerySucceeded, localQueryFailed);");
                codeBuilder.AddLine();
                codeBuilder.AddLine("    function localQuerySucceeded(data) {");
                codeBuilder.AddLine("        if (qSucceeded)");
                codeBuilder.AddLine("            qSucceeded(data);");
                codeBuilder.AddLine("        if (qFin)");
                codeBuilder.AddLine("            qFin();");
                codeBuilder.AddLine("    }");
                codeBuilder.AddLine();
                codeBuilder.AddLine("    function localQueryFailed(error) {");
                codeBuilder.AddLine("        if (qFin)");
                codeBuilder.AddLine("            qFin();");
                codeBuilder.AddLine("        if (qFailed)");
                codeBuilder.AddLine("            qFailed(error);");
                codeBuilder.AddLine("        else");
                codeBuilder.AddLine("            queryFailed(error);");
                codeBuilder.AddLine("    }");
                codeBuilder.AddLine("};");

                result += (result.IsNullOrEmpty() ? String.Empty : ",\r\n") + "                get" + entity.Name + "ByEntitySearchNoAssociations: get" + entity.Name + "ByEntitySearchNoAssociations";
                codeBuilder.AddLine();
                codeBuilder.AddLine("var get" + entity.Name + "ByEntitySearchNoAssociations = function (jEntitySearch, skip, take, returnInlineCount, orderByDef, qSucceeded, qFin, qFailed) {");
                codeBuilder.AddLine("    var query = 'Get" + entity.Name + "ByEntitySearchNoAssociations?$inlinecount=' + (returnInlineCount ? 'allpages' : 'none');");
                if (!orderBy.IsNullOrEmpty())
                    codeBuilder.AddLine("    query += '&$orderby=' + (common.isNullOrEmpty(orderByDef) ? '" + orderBy + "' : orderByDef);");
                codeBuilder.AddLine();
                codeBuilder.AddLine("    if ((typeof jEntitySearch !== 'undefined') && jEntitySearch !== null)");
                codeBuilder.AddLine("        query += '&jEntitySearch=' + jEntitySearch;");
                codeBuilder.AddLine("    if (take > 0)");
                codeBuilder.AddLine("       query += '&$skip=' + skip.toString() + '&$top=' + take.toString();");
                codeBuilder.AddLine();
                codeBuilder.AddLine("    return httpFactory.httpGet(getDataServiceUrl, query, localQuerySucceeded, localQueryFailed);");
                codeBuilder.AddLine();
                codeBuilder.AddLine("    function localQuerySucceeded(data) {");
                codeBuilder.AddLine("        if (qSucceeded)");
                codeBuilder.AddLine("            qSucceeded(data);");
                codeBuilder.AddLine("        if (qFin)");
                codeBuilder.AddLine("            qFin();");
                codeBuilder.AddLine("    }");
                codeBuilder.AddLine();
                codeBuilder.AddLine("    function localQueryFailed(error) {");
                codeBuilder.AddLine("        if (qFin)");
                codeBuilder.AddLine("            qFin();");
                codeBuilder.AddLine("        if (qFailed)");
                codeBuilder.AddLine("            qFailed(error);");
                codeBuilder.AddLine("        else");
                codeBuilder.AddLine("            queryFailed(error);");
                codeBuilder.AddLine("    }");
                codeBuilder.AddLine("};");

                //Aqui:

                var quickSearchProperties = entity.GetAllInheritanceProperties().Where(e => e.QuickSearchIndex >= 0).OrderBy(e => e.QuickSearchIndex).ToArray();
                bool hasQuickSearch = quickSearchProperties.Where(e => e.Datatype.ToLower().Contains("string")).Count() > 0;
                if (hasQuickSearch)
                {
                    result += (result.IsNullOrEmpty() ? String.Empty : ",\r\n") + "                get" + entity.Name + "QuickSearch: get" + entity.Name + "QuickSearch";
                    codeBuilder.AddLine();
                    codeBuilder.AddLine("var last" + entity.Name + "QuickSearchTerm = '';");
                    codeBuilder.AddLine("var last" + entity.Name + "QuickSearchResultCount = -1;");
                    codeBuilder.AddLine("var get" + entity.Name + "QuickSearch = function (quickSearchTerm, jExpr, page, queryCallback, propertiesSelection) {");
                    codeBuilder.AddLine("    if (common.isNullOrEmpty(quickSearchTerm) || quickSearchTerm.length < 4) {");
                    codeBuilder.AddLine("        last" + entity.Name + "QuickSearchTerm = '';");
                    codeBuilder.AddLine("        last" + entity.Name + "QuickSearchResultCount = -1;");
                    codeBuilder.AddLine("        if (queryCallback) queryCallback(false, [], 0);");
                    codeBuilder.AddLine("        return;");
                    codeBuilder.AddLine("    }");
                    codeBuilder.AddLine("    else {");
                    codeBuilder.AddLine("        if (last" + entity.Name + "QuickSearchResultCount = 0 && last" + entity.Name + "QuickSearchTerm.length > 2 && last" + entity.Name + "QuickSearchTerm.length < quickSearchTerm.length && last" + entity.Name + "QuickSearchTerm == quickSearchTerm.strLeft(last" + entity.Name + "QuickSearchTerm.length))");
                    codeBuilder.AddLine("        {");
                    codeBuilder.AddLine("            if (queryCallback) queryCallback(false, [], 0);");
                    codeBuilder.AddLine("            return;");
                    codeBuilder.AddLine("        }");
                    codeBuilder.AddLine("    }");
                    codeBuilder.AddLine();
                    codeBuilder.AddLine("    var query = 'Get" + entity.Name + "QuickSearch?$inlinecount=allpages';");
                    codeBuilder.AddLine("    query += '&q=' + quickSearchTerm + '&page=' + page.toString() + '&jExpr=' + jExpr + '&propertiesSelection=' + propertiesSelection;");
                    codeBuilder.AddLine();
                    codeBuilder.AddLine("    return httpFactory.httpGet(getDataServiceUrl, query, localQuerySucceeded, localQueryFailed);");
                    codeBuilder.AddLine();
                    codeBuilder.AddLine("    function localQuerySucceeded(data) {");
                    codeBuilder.AddLine("        last" + entity.Name + "QuickSearchTerm = quickSearchTerm;");
                    codeBuilder.AddLine("        last" + entity.Name + "QuickSearchResultCount = data.results.length;");
                    codeBuilder.AddLine("        if (queryCallback) queryCallback(false, data.results, data.inlineCount);");
                    codeBuilder.AddLine("    }");
                    codeBuilder.AddLine();
                    codeBuilder.AddLine("    function localQueryFailed(error) {");
                    codeBuilder.AddLine("        if (queryCallback) queryCallback(true, [], 0); queryFailed(error);");
                    codeBuilder.AddLine("    }");
                    codeBuilder.AddLine("};");
                }

            }

            codeBuilder.AddLine("//#endregion");

            return result;

        }

        /// <summary>
        /// Get all custom method's buttons for the UI
        /// </summary>
        /// <param name="ui"></param>
        /// <returns></returns>
        private IEnumerable<string> GetButtonNamesByUI(EntityAdapterUserInterface ui)
        {
            List<string> methodList = new List<string>();

            Action<LayoutContainer> finder = null;
            finder = (container) =>
            {
                foreach (LayoutControlV2 control in container.Controls.Where(e => e is LayoutControlV2 && e.ClassName.ToLower() == "button").Select(e => (LayoutControlV2)e))
                {
                    methodList.Add(control.GetControlName("") + "_Click");
                }
                container.Controls.Where(e => e is LayoutContainer).Cast<LayoutContainer>().Foreach(finder);
            };

            ui.LayoutDefinition.Containers.ForEach(finder);

            return methodList;
        }

        /// <summary>
        /// Generate button method
        /// </summary>
        /// <param name="method"></param>
        /// <param name="putClientErpceInStarts"></param>
        /// <returns></returns>
        private string GenerateButtonMethodForUI(string method, bool putClientErpceInStarts)
        {
            return (putClientErpceInStarts ? "    " : "") + "var " + method + " = function (e) { /* e = { viewModel: object } */ \r\n    };\r\n";
        }

        private List<string> CreateMetaDataInfo(EntityAdapter entity, Linx.Tools.CodeBuilder codeBuilder, Dictionary<string, string> lookupKeys, Dictionary<string, string> lookupVisbleColumns, bool byParentComposition = false)
        {
            List<string> propNames = new List<string>();
            codeBuilder.AddLine("entityNames.push('" + entity.Name + (byParentComposition ? "ParentComposition" : "") + "');");
            codeBuilder.AddLine("metadataInfo['" + entity.Name + (byParentComposition ? "ParentComposition" : "") + "'] = [");
            var properties = entity.GetAllInheritanceAttributes(byParentComposition);
            if (properties.Count > 0)
            {
                bool hasDynamicPK = entity.HasDynamicPrimaryKey();
                if (hasDynamicPK)
                {
                    codeBuilder.AddLine("    { key: 'EntityUniqueKey', maxLength: 0, isPartOfKey: true, headerText: 'EntityUniqueKey', width: '50px', dataType: '" + Linx.Builder.Resources.HtmlCodeGen.GetPropDataType("Guid", "") + "', format: '', hidden: true, unbound: false, group: null, defaultValue: '' },");
                    propNames.Add("EntityUniqueKey");
                }

                if (entity.TargetEntityAdapter != null && entity.TargetEntityAdapter.HasDynamicPrimaryKey())
                {
                    codeBuilder.AddLine("    { key: 'EntityParentUniqueKey', maxLength: 0, isPartOfKey: false, headerText: 'EntityUniqueKey', width: '50px', dataType: '" + Linx.Builder.Resources.HtmlCodeGen.GetPropDataType("Guid", "") + "', format: '', hidden: true, unbound: false, group: null, defaultValue: '' },");
                    propNames.Add("EntityParentUniqueKey");
                }

                bool isRequired;
                string validators;
                int precision;
                for (int cIndex = 0; cIndex < properties.Count; cIndex++)
                {
                    var property = properties[cIndex];
                    var isDomain = !property.DomainName.IsNullOrEmpty();
                    int ctrlWidth = HtmlCodeGen.GetElementWidth(property.DisplayControl.ToString(), property.Datatype, property.DisplayName, property.DataFormatString, true, property.Precision);
                    string lookupPropertyName = "", lookupVisibleColumns = "";
                    if (lookupKeys.ContainsKey(property.Name))
                        lookupPropertyName = lookupKeys[property.Name];
                    if (lookupVisbleColumns.ContainsKey(property.Name))
                        lookupVisibleColumns = lookupVisbleColumns[property.Name];

                    bool isPK = (!hasDynamicPK && property is EntityAdapterProperty && entity.IsPrimaryKey((EntityAdapterProperty)property));

                    //Defining validators
                    if (property.RemoveValidations)
                        validators = "";
                    else
                    {
                        isRequired = (!property.DomainName.IsNullOrEmpty() || !property.IsNull || property.IsCompulsory);
                        if (isRequired && ((!property.IsZeroNotAllowed && property.IsNumeric()) || property.Datatype.ToLower().Contains("bool")) && !isDomain && !isPK)
                            isRequired = false;

                        validators = (isRequired ? "isRequired: true" : String.Empty);
                        precision = LayoutStructureExtensions.GetPrecision(property.Precision);
                        validators += (validators.IsNullOrEmpty() ? String.Empty : ", ") + "maxLength: " + precision.ToString() + ((!isDomain && precision > 0 && property.Datatype.ToLower().Contains("string")) ? ", validateMaxLength: true" : "");
                        if (!validators.IsNullOrEmpty())
                            validators = ", " + validators;
                    }
                    /////////////////

                    codeBuilder.AddLine("    { key: '" + property.Name + "', isDomain: " + (isDomain).ToString().ToLower() + ", domainName: '" + property.DomainName + "', lookupPropertyName: '" + lookupPropertyName + "', lookupVisibleColumns: '" + lookupVisibleColumns + "'" + validators + ", isPartOfKey: " + isPK.ToString().ToLower() + ", headerText: '" + (property.DisplayName.IsNullOrEmpty() ? property.Name : property.DisplayName) + "', width: '" + ctrlWidth.ToString() + "px', dataType: '" + HtmlCodeGen.GetPropDataType(property.Datatype, "") + "', format: '" + Linx.Builder.Resources.HtmlCodeGen.GetFormatDataType(property.Datatype, "", property.DataFormatString) + "', hidden: " + (!property.IsBrowsable).ToString().ToLower() + ", unbound: false, group: null, defaultValue: " + GetClientErpDefaultValueByType(property.Datatype) + " },");
                    propNames.Add(property.Name);
                    if (isDomain)
                    {
                        codeBuilder.AddLine("    { key: '" + property.Name + "Name', isDomain: true, domainName: '" + property.DomainName + "', lookupPropertyName: '" + lookupPropertyName + "', lookupVisibleColumns: '" + lookupVisibleColumns + "', maxLength: 0, isPartOfKey: false, headerText: '" + (property.DisplayName.IsNullOrEmpty() ? property.Name : property.DisplayName) + " (Name)', width: '0px', dataType: '" + HtmlCodeGen.GetPropDataType("string", "") + "', format: '', hidden: true, unbound: false, group: null, defaultValue: '' },");
                        propNames.Add(property.Name + "Name");
                    }
                }

                if (entity.HasEnabledMedias())
                {
                    codeBuilder.AddLine("    { key: 'TableMedia', isDomain: false, isRequired: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: '', width: '0px', dataType: '" + HtmlCodeGen.GetPropDataType("string", "") + "', format: '', hidden: true, unbound: false, group: null, defaultValue: null },");
                    propNames.Add("TableMedia");
                }

                codeBuilder.AddLine("    { key: 'ChangeState', isDomain: false, domainName: '', lookupPropertyName: '', lookupVisibleColumns: '', maxLength: 0, isPartOfKey: false, headerText: '', width: '0px', dataType: '" + HtmlCodeGen.GetPropDataType("string", "") + "', format: '', hidden: true, unbound: false, group: null, defaultValue: 'N' }");

            }
            codeBuilder.AddLine("];");

            return propNames;
        }

        private void CreateDataExportInfo(EntityAdapter entity, Linx.Tools.CodeBuilder codeBuilder)
        {
            var api = _designerRoot.WebApiControllers.FirstOrDefault(e => e.SynchronizedWithDomainService);
            if (api == null)
                return;

            codeBuilder.AddLine("dataExportInfo['" + entity.Name + "'] = [ ");

            Action<EntityAdapter> expData = null;
            expData = (e) =>
            {
                var suffix = ((entity != e && e.TargetEntityAdapter != null && e.IsParentCompositionAllowed()) ? "ParentComposition" : "");
                codeBuilder.AddLine("    " + (entity != e ? ", " : "") + "{ name: '" + e.Name + "', canExportMedia: " + (!e.HasDynamicPrimaryKey()).ToString().ToLower() + " , canExportReport: " + (!e.IsDashboardFilter && (entity == e || (e.TargetEntityAdapter != null && (e.IsParentCompositionAllowed() || e.TargetEntityAdapter.IsDashboardFilter)))).ToString().ToLower() + ", actionExport: '" + api.GetRoutePrefix() + "/Get" + e.Name + suffix + "ToExcel', actionReport: '" + api.GetRoutePrefix() + "/Get" + e.Name + suffix + "ToReportXml', actionFeed: '" + api.GetRoutePrefix() + "OData/" + e.Name + suffix + "', actionName: '" + api.GetRoutePrefix() + "/Get" + e.Name + suffix + "ByEntitySearchNoAssociations', display: '" + (String.IsNullOrWhiteSpace(e.DisplayName) ? e.Name : e.DisplayName) + "',  metaData: function() { return metadataInfo['" + e.Name + suffix + "']; } }");
                e.SourceEntityAdapters.ForEach(d => expData(d));
            };

            expData(entity);

            codeBuilder.AddLine("];");
        }

        /// <summary>
        /// Creating JSon Metadata
        /// </summary>
        /// <param name="codeBuilder"></param>
        private Dictionary<string, List<string>> GetJsonMetadata(Linx.Tools.CodeBuilder codeBuilder)
        {
            Dictionary<string, List<string>> propNames = new Dictionary<string, List<string>>();
            codeBuilder.AddLine("var metadataInfo = [];");
            codeBuilder.AddLine("var dataExportInfo = [];");
            codeBuilder.AddLine("var entityNames = [];");
            codeBuilder.AddLine("var lookUpNames = [];");
            codeBuilder.AddLine("var entitylookUps = [];");
            //Entities
            foreach (var entity in _designerRoot.EntityAdapters)
            {
                Dictionary<string, string> lookupKeys = new Dictionary<string, string>();
                Dictionary<string, string> lookupVisbleColumns = new Dictionary<string, string>();
                foreach (var lookup in entity.GetAllLookUpsInfo(true))
                {
                    foreach (var prop in lookup.Properties)
                    {
                        if (!prop.EntityPropertyRelated.IsNullOrEmpty() && !lookupKeys.ContainsKey(prop.EntityPropertyRelated))
                        {
                            lookupKeys.Add(prop.EntityPropertyRelated, prop.Name);
                            lookupVisbleColumns.Add(prop.EntityPropertyRelated, lookup.GetQueryGroupColumns(prop.Name));
                        }
                    }
                }
                propNames.Add(entity.Name, this.CreateMetaDataInfo(entity, codeBuilder, lookupKeys, lookupVisbleColumns));
                if (entity.TargetEntityAdapter != null && entity.IsParentCompositionAllowed())
                    this.CreateMetaDataInfo(entity, codeBuilder, lookupKeys, lookupVisbleColumns, true);
                this.CreateDataExportInfo(entity, codeBuilder);


                //Lookups            
                codeBuilder.AddLine("entitylookUps.push('" + entity.Name + "');");
                codeBuilder.AddLine("entitylookUps['" + entity.Name + "'] = [];");
                foreach (var lookUp in entity.GetAllLookUpsInfo(false))
                {
                    codeBuilder.AddLine("entitylookUps['" + entity.Name + "'].push('" + lookUp.Name + "');");
                    codeBuilder.AddLine("lookUpNames.push('" + lookUp.Name + "');");
                    codeBuilder.AddLine("metadataInfo['" + lookUp.Name + "'] = [");
                    var properties = lookUp.Properties.OrderBy(e => e.Order.ToString().PadLeft(4) + e.DisplayName).ToList();
                    for (int cIndex = 0; cIndex < properties.Count; cIndex++)
                    {
                        var property = properties[cIndex];

                        if (!lookupKeys.Values.Contains(property.Name))
                            property.EntityPropertyRelated = "";

                        int ctrlWidth = HtmlCodeGen.GetElementWidth(DisplayControlType.TextBox.ToString(), property.Datatype, property.DisplayName, property.GetDataFormatString(), true, property.Precision);
                        codeBuilder.AddLine("    { key: '" + property.Name + "', relatedKey: '" + property.EntityPropertyRelated + "', maxLength: " + LayoutStructureExtensions.GetPrecision(property.Precision).ToString() + ", isPartOfKey: " + property.IsPrimaryKey.ToString().ToLower() + ", headerText: '" + (property.DisplayName.IsNullOrEmpty() ? property.Name : property.DisplayName) + "', width: '" + ctrlWidth.ToString() + "px', dataType: '" + Linx.Builder.Resources.HtmlCodeGen.GetPropDataType(property.Datatype, "") + "', format: '" + Linx.Builder.Resources.HtmlCodeGen.GetFormatDataType(property.Datatype, "", property.GetDataFormatString()) + "', hidden: " + (!property.IsBrowsable || !property.DomainName.IsNullOrEmpty()).ToString().ToLower() + ", unbound: false, group: null }" + (cIndex == properties.Count - 1 && !(property.IsBrowsable && !property.DomainName.IsNullOrEmpty()) ? String.Empty : ","));

                        if (property.IsBrowsable && !property.DomainName.IsNullOrEmpty())
                        {
                            ctrlWidth = HtmlCodeGen.GetElementWidth(DisplayControlType.ComboBox.ToString(), property.Datatype, property.DisplayName, property.GetDataFormatString(), true, property.Precision);
                            codeBuilder.AddLine("    { key: '" + property.Name + "Name', relatedKey: '" + (property.EntityPropertyRelated.IsNullOrEmpty() ? "" : property.EntityPropertyRelated + "Name") + "', maxLength: " + LayoutStructureExtensions.GetPrecision("255:0").ToString() + ", isPartOfKey: false, headerText: '" + (property.DisplayName.IsNullOrEmpty() ? property.Name : property.DisplayName) + "', width: '" + ctrlWidth.ToString() + "px', dataType: '" + Linx.Builder.Resources.HtmlCodeGen.GetPropDataType("string", "") + "', format: '" + Linx.Builder.Resources.HtmlCodeGen.GetFormatDataType("string", "", "") + "', hidden: false, unbound: false, group: null }" + (cIndex == properties.Count - 1 ? String.Empty : ","));
                        }
                    }
                    codeBuilder.AddLine("];");
                }
            }

            return propNames;
        }

        /// <summary>
        /// Generate Code for default value and parameters
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        private string GenerateClientErpCodeForDefaultValueAndParameters(EntityAdapterAttribute property)
        {
            string defaultValue = (property is EntityAdapterProperty ? ((EntityAdapterProperty)property).DefaultValue : (property is EntityAdapterPublicationProperty ? ((EntityAdapterPublicationProperty)property).DefaultValue : ""));

            if (defaultValue.IsNullOrEmpty())
                return String.Empty;

            Func<string> toValue = () =>
            {
                if (Regex.IsMatch(defaultValue, patternForLinxParameter))
                    return string.Format("dataParameters.parameters['{0}']", defaultValue.Extract("[", "]"));
                else if (property.Datatype.ToLower().Contains("guid") && defaultValue.Contains("Guid.NewGuid"))
                    return "getNewGuid()";
                else if (Regex.IsMatch(defaultValue, patternForDateTimeConstructor))
                    return defaultValue.Replace("DateTime", "Date").Replace(")", ",0,0,0)");
                else if (property.Datatype.ToLower().Contains("datetime") && defaultValue.Contains("DateTime.Now"))
                    return "";
                else
                    return defaultValue;
            };

            string valueToReturn = string.Empty;
            string dataType = property.Datatype.ToLower();
            if (dataType.Contains("datetime"))
            {
                string fixedValue = toValue();
                valueToReturn = (fixedValue.IsNullOrEmpty() ? "common.getCurrentDate()" : "new Date(" + fixedValue + ")");
            }
            else valueToReturn = toValue();

            return valueToReturn;
        }

        /// <summary>
        /// Generate methods for executing lookups
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="codeBuilder"></param>
        private void GenerateClientErpLookupExecuting(EntityAdapter entity, Linx.Tools.CodeBuilder codeBuilder, string entitiesRef)
        {

            codeBuilder.AddLine("   ownerReference.getLookupPropertyName = function(propertyName) {");
            codeBuilder.AddLine("      var property = getEntityProperty(ownerReference.typeName, propertyName);");
            codeBuilder.AddLine("      return (property != null && !common.isNullOrEmpty(property.lookupPropertyName) ? property.lookupPropertyName : propertyName);");
            codeBuilder.AddLine("   }");
            codeBuilder.AddLine("   ownerReference.getLookupVisibleColumns = function(propertyName) {");
            codeBuilder.AddLine("      var property = getEntityProperty(ownerReference.typeName, propertyName);");
            codeBuilder.AddLine("      return (property != null ? property.lookupVisibleColumns : '');");
            codeBuilder.AddLine("   }");


            codeBuilder.AddLine("   ownerReference.getLookupDisplay = function (lookupName) {");
            codeBuilder.AddLine("       var displayName = '';");
            foreach (var lookUp in entity.LookUpAdapters)
            {
                if (!lookUp.DisplayName.IsNullOrEmpty())
                {
                    codeBuilder.AddLine("       if (lookupName === '" + lookUp.Name + "') {");
                    codeBuilder.AddLine("           displayName = ' de " + (lookUp.DisplayName.Contains("Look Up ") ? lookUp.DisplayName.Right("Look Up ").Proper() : lookUp.DisplayName) + "';");
                    codeBuilder.AddLine("       }");
                }
            }
            codeBuilder.AddLine("       return 'Seleção' + displayName;");
            codeBuilder.AddLine("   };");
            codeBuilder.AddLine();

            var entityProperties = entity.GetAllInheritanceAttributes();
            foreach (var lookup in entity.GetAllLookUpsInfo(true))
            {
                codeBuilder.AddLine("   ownerReference.getSubQueryFilterFrom" + lookup.Name + " = function (propertyName) {");

                codeBuilder.AddLine("       var filter = '';");
                foreach (var prop in lookup.Properties.Where(e => !e.EntityPropertyRelated.IsNullOrEmpty()))
                {
                    var cliFilters = lookup.GetSubQueryClientFilters(prop.EntityPropertyRelated).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    if (cliFilters.Length > 0 || !prop.DependencyProperty.IsNullOrEmpty())
                    {
                        codeBuilder.AddLine("       if (propertyName === '" + prop.EntityPropertyRelated + "') {");

                        if (!prop.DependencyProperty.IsNullOrEmpty())
                        {
                            codeBuilder.AddLine("           if (dataBusiness.status() === 'E' && common.isNullOrEmpty(ownerReference." + prop.DependencyProperty + ")) {");
                            codeBuilder.AddLine("               dialog.showAlert('O campo [' + ownerReference.getDisplayName('" + prop.DependencyProperty + "') + '] precisa ser informado.', 'Alerta');");
                            codeBuilder.AddLine("               return 'Error';");
                            codeBuilder.AddLine("           }");
                        }

                        foreach (var relation in cliFilters)
                        {
                            string lProp = relation.Left("="), rProp = relation.Right("=");
                            var relationProp = lookup.Properties.FirstOrDefault(e => e.Name == lProp);
                            if (relationProp != null && entityProperties.Any(e => e.Name == rProp))
                            {
                                var dType = Linx.Tools.EntitySearch.ParseJDataType(relationProp.Datatype);
                                codeBuilder.AddLine("           var _" + lProp + " = this." + rProp + ";");
                                codeBuilder.AddLine("           if (!common.isNullOrEmpty(_" + lProp + ")) { filter += (filter === '' ? '' : ';') + '" + lProp + "' + (_" + lProp + ".toString().indexOf('[') > -1 ? '#In#S' : '#==#" + dType + "')" + ("SCGT".Contains(dType) ? " + (_" + lProp + ".toString().indexOf('[') > -1 ? '" + dType + ",' : '')" : "") + " + _" + lProp + ".toString().replace('[', '').replace(']', ''); }");

                            }
                        }

                        codeBuilder.AddLine("       }");
                    }
                }
                codeBuilder.AddLine("       return filter;");
                codeBuilder.AddLine("   }");
            }

            codeBuilder.AddLine("   ownerReference.canGetClientFilter = function (lookupName) {");
            string clearList = "";
            foreach (var lookup in entity.GetAllLookUpsInfo(true))
            {
                if (!lookup.ApplyClientFilterOnClear)
                    clearList += (clearList == "" ? "" : ", ") + "'" + lookup.Name + "'";
            }
            if (!clearList.IsNullOrEmpty())
            {
                codeBuilder.AddLine("       return !(dataBusiness.status() == 'C' && [" + clearList + "].indexOf(lookupName) >= 0);");
            }
            else
                codeBuilder.AddLine("       return true;");
            codeBuilder.AddLine("   }");

            codeBuilder.AddLine();
            codeBuilder.AddLine("   ownerReference.hasValidClientFilter = function (lookupName, lookupInfo) {");
            codeBuilder.AddLine("       var checkClientFilter = '';");
            codeBuilder.AddLine("       if (typeof ownerReference['BeforeGet' + lookupName + 'Query'] == 'function') {");
            codeBuilder.AddLine("           checkClientFilter = ownerReference['BeforeGet' + lookupName + 'Query']();");
            codeBuilder.AddLine("           if (checkClientFilter === 'Error') { return false; }");
            codeBuilder.AddLine("       }");
            codeBuilder.AddLine("       return true;");
            codeBuilder.AddLine("   }");

            //Lookup access functions
            GenerateLookUpJsFunctions(codeBuilder, entitiesRef);
        }

        #endregion


        public ProjectItem foldeApps { get; set; }

        public Tools.CodeBuilder codeBuilderAppStart { get; set; }
    }
}
