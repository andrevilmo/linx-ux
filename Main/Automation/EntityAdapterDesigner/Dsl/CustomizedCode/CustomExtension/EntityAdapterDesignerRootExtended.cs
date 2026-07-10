using EnvDTE;
using Linx.Builder.Resources;
using Linx.EntityAdapterDesigner.CustomizedCode;
using Linx.EntityAdapterDesigner.CustomizedCode.Apps.ClientErp;
using Linx.EntityAdapterDesigner.CustomizedCode.Apps.Mobile;
using Linx.EntityAdapterDesigner.CustomizedCode.Apps.SPA;
using Linx.Tools;
using Microsoft.CSharp;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Diagrams;
using Microsoft.Win32;
using NuGet.VisualStudio;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Collections;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using VSLangProj80;
using DslModeling = global::Microsoft.VisualStudio.Modeling;



namespace Linx.EntityAdapterDesigner
{

    public partial class EntityAdapterDesignerRoot : IAditionalInformation
    {
        private SpaCodeGen _spaCodeGen;
        public SpaCodeGen SpaCodeGen { get { if (_spaCodeGen == null) _spaCodeGen = new SpaCodeGen(this); return _spaCodeGen; } }
        private MobileCodeGen _mobileCodeGen;
        public MobileCodeGen MobileCodeGen { get { if (_mobileCodeGen == null) _mobileCodeGen = new MobileCodeGen(this); return _mobileCodeGen; } }
        private ClientErpCodeGen _clientErpCodeGen;
        public ClientErpCodeGen ClientErpCodeGen { get { if (_clientErpCodeGen == null) _clientErpCodeGen = new ClientErpCodeGen(this); return _clientErpCodeGen; } }

        public CustomizedCode.PublicationStructure PublisherAutoReference { get; set; }
        public bool HasStructuralChanges { get; set; }
        public bool InitializedModel { get; set; }
        public bool HasErrors { get; set; }

        public string GetAppName()
        {
            return this.TargetNamespace.Replace("Linx.", "").Replace(".BV", "").Replace(".", "");
        }

        public List<EntityAdapter> GetEntities()
        {
            return this.EntityAdapters.Where(e => e.TargetEntityAdapter == null).ToList();
        }

        public List<EntityAdapter> GetOlapEntities()
        {
            return this.EntityAdapters.Where(e => e.IsOlap()).ToList();
        }

        public IEnumerable<LookUpAdapter> GetOlapLookups()
        {
            return this.EntityAdapters.SelectMany(e => e.LookUpAdapters).Where(l => l.IsOlap());
        }


        public string GetJsonOlapCatalogStrings(string indent, List<string> olaps, string connectionStrings = "")
        {
            if (this.OlapCatalogs.Count > 0)
            {
                foreach (var catalog in this.OlapCatalogs)
                {
                    if (!olaps.Contains(catalog.Name))
                    {
                        olaps.Add(catalog.Name);
                        ConnectionManager connection = catalog.Connection;
                        string connectionString = connection.GetConnectionString().Replace("\\", "\\\\").Replace("\"", "&quot;");
                        if (!connectionString.IsNullOrEmpty())
                            connectionStrings += indent + (connectionStrings.IsNullOrEmpty() ? "" : ", ") + "\"" + connection.Name + "\": \"" + connectionString + "\"";
                    }
                }
            }
            return connectionStrings;
        }

        public string GetOlapCatalogStrings(string indent, List<string> olaps)
        {
            if (this.OlapCatalogs.Count > 0)
            {
                Linx.Tools.CodeBuilder builder = new Linx.Tools.CodeBuilder(indent);
                foreach (var catalog in this.OlapCatalogs)
                {
                    if (!olaps.Contains(catalog.Name))
                    {
                        olaps.Add(catalog.Name);
                        ConnectionManager connection = catalog.Connection;
                        builder.AddLine(connection.GetConnectionConfiguration());
                    }
                }
                return builder.GetBody();
            }
            else
                return String.Empty;
        }

        public Dictionary<string, string> GetAllRepresentationDomainServices()
        {
            string alias = "serviceContext";
            int cntAlias = 0;
            Dictionary<string, string> result = new Dictionary<string, string>();

            //Representations
            foreach (EntityAdapterRepresentation rep in this.EntityAdapterRepresentations.OrderBy(e => e.TargetNameSpace + "#" + e.TargetEdmName))
            {
                if (!result.ContainsKey(rep.TargetNameSpace + "#" + rep.TargetEdmName))
                {
                    result.Add(rep.TargetNameSpace + "#" + rep.TargetEdmName, alias + cntAlias.ToString());
                    cntAlias++;
                }
            }

            //ModelViews
            foreach (var dbsets in this.EntityAdapters.Where(e => e.IsModelView && !e.IsReadOnly && !e.ModelViewDbSets.IsNullOrEmpty() && e.GetCurrentDataModel() == null).Select(e => e.ModelViewDbSets))
            {
                foreach (var dbSet in dbsets.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    string contextType = dbSet.Left("#");
                    var targetEdmName = contextType.Right("|");
                    contextType = contextType.Left("|");
                    var targetNameSpace = contextType.Left(contextType.Length - contextType.Right(".").Length - 1);

                    if (!result.ContainsKey(targetNameSpace + "#" + targetEdmName))
                    {
                        result.Add(targetNameSpace + "#" + targetEdmName, alias + cntAlias.ToString());
                        cntAlias++;
                    }
                }
            }

            return result;
        }

        public string GenerateReplaceDetailsByParent(string indent, EntityAdapter parent = null)
        {
            string result = "";

            if (parent == null)
            {
                foreach (EntityAdapter entity in this.EntityAdapters.Where(e => e.TargetEntityAdapter == null))
                {
                    result += GenerateReplaceDetailsByParent(indent, entity);
                }
            }
            else
            {
                if (parent.SourceEntityAdapters.Count > 0)
                {
                    result += "\r\n " + indent + "if (parent is " + parent.Name + ")";
                    result += "\r\n " + indent + "{";
                    foreach (EntityAdapter entity in parent.SourceEntityAdapters)
                    {
                        result += "\r\n " + indent + "  foreach (" + entity.Name + " entity in ((" + parent.Name + ")parent)." + entity.Name + "List)";
                        result += "\r\n " + indent + "  {";
                        var relations = entity.GetAllParentKeystAssociation(false);
                        foreach (var key in relations.Keys)
                        {
                            result += "\r\n " + indent + "      entity." + key + " = ((" + parent.Name + ")parent)." + relations[key] + ";";
                            if (entity.EntityAdapterRepresentation != null)
                            {
                                var prop = entity.GetAllInheritanceProperties().FirstOrDefault(e => e.Name == key && !e.DataRelationKey.IsNullOrEmpty());
                                if (prop != null)
                                {
                                    result += "\r\n " + indent + "      var entityEntry = entityChanges.FirstOrDefault(e => e.Representation == entity);";
                                    result += "\r\n " + indent + "      if (entityEntry != null)";
                                    result += "\r\n " + indent + "          entityEntry.Entity.SetPropertyValue(\"" + prop.DataRelationKey.Right(".") + "\", entity." + key + ");";
                                }
                            }
                        }
                        result += "\r\n " + indent + "  }";
                    }
                    result += "\r\n " + indent + "}";

                    foreach (EntityAdapter entity in parent.SourceEntityAdapters)
                    {
                        result += GenerateReplaceDetailsByParent(indent, entity);
                    }
                }
            }

            return result;
        }

        public string GenerateSubmitDataForEntityRepresentations(List<EntityAdapter> representedEntitiesForSave, string indent)
        {
            if (representedEntitiesForSave.Count == 0)
                return String.Empty;

            string result = "\r\n ";
            result += "\r\n " + indent + "//Submitting all data changes";

            foreach (EntityAdapter entity in representedEntitiesForSave)
            {
                result += entity.GetRepresentationDomainServiceInstances(indent, true, result);
                result += entity.GetRepresentationDomainServiceSubmitCommands(indent, result);
            }

            return result;
        }


        /// <summary>
        /// Update SPA project from Linx.Internet.Application (version and content)
        /// </summary>
        public void UpdateSpaStructure()
        {
            Cursor.Current = Cursors.WaitCursor;
            System.Threading.Thread.Sleep(1000);
            try
            {
                this.SpaCodeGen.CopyShellFromSpaFolder();
                this.SpaCodeGen.GenerateSpaModuleConfigCode();
            }
            catch (Exception excp)
            {
                CustomizedCode.Helpers.TreatException.LogError(excp);
                MessageBox.Show(excp.GetCompleteMessage(), "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void SaveDocument(ProjectItem item, bool close)
        {
            Window window = item.Open(EnvDTE.Constants.vsViewKindDesigner);
            window.SetFocus();
            window.Document.Save();
            if (close)
                window.Close();
        }

        public static bool IsAutomaticSaving { get; set; }
        /// <summary>
        /// Save all EADs for updating all automatic code.
        /// </summary>
        public void SaveAllDocuments(bool justCurrentDocument = false)
        {
            var eadProject = this.GetEadProject();
            if (eadProject != null)
            {
                IsAutomaticSaving = true;
                try
                {
                    foreach (ProjectItem item in eadProject.ProjectItems)
                    {
                        if (Path.GetExtension(item.Name).ToLower() == ".ead")
                        {
                            if (!justCurrentDocument || item.Name == this.DocumentName)
                            {
                                this.SaveDocument(item, (item.Name != this.DocumentName));
                            }
                        }
                    }
                }
                catch (Exception excp)
                {
                    CustomizedCode.Helpers.TreatException.LogError(excp);
                    MessageBox.Show(excp.GetCompleteMessage(), "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                finally
                {
                    IsAutomaticSaving = false;
                }
            }
        }


        public string GenerateHierarchyForSaving(string indent)
        {
            string result = String.Empty;

            var entities = this.EntityAdapters.Where(e => e.TargetEntityAdapter == null && e.HasDetails());
            if (entities.Count() > 0)
            {
                result += "\r\n ";
                result += "\r\n " + indent + "bool createNewChangeSet = false;";
                result += "\r\n ";
                result += "\r\n " + indent + "//Adjust data hierarchy";
                foreach (EntityAdapter entity in entities)
                {
                    result += "\r\n " + indent + "var _" + entity.Name + "Elements = changeSet.ChangeSetEntries.Where(e => e.Entity is " + entity.Name + " && e.Entity.GetType().Name == \"" + entity.Name + "\" && e.Associations == null && e.OriginalAssociations == null).ToList();";
                    result += "\r\n " + indent + "foreach (var entity in _" + entity.Name + "Elements)";
                    result += "\r\n " + indent + "   if (((" + entity.Name + ")entity.Entity).AdjustHierarchyForSaving(entity, changeSet)) { if (!createNewChangeSet) createNewChangeSet = true; }";
                }
                result += "\r\n ";
                result += "\r\n " + indent + "//Remove inconsistent details";
                foreach (EntityAdapter entity in this.EntityAdapters.Where(e => e.TargetEntityAdapter != null))
                {
                    result += "\r\n " + indent + "foreach(var entry in changeSet.ChangeSetEntries.Where(e => e.Entity is " + entity.Name + " && e.Entity.GetType().Name == \"" + entity.Name + "\" && e.Operation != DomainOperation.None && e.Associations == null && e.OriginalAssociations == null).ToList())";
                    result += "\r\n " + indent + "{";
                    result += "\r\n " + indent + "    entry.Operation = DomainOperation.None;";
                    result += "\r\n " + indent + "    if (!createNewChangeSet) createNewChangeSet = true;";
                    result += "\r\n " + indent + "}";
                }

                result += "\r\n ";
                result += "\r\n " + indent + "if (createNewChangeSet) changeSet = new ChangeSet(changeSet.ChangeSetEntries.Where(e => e.Operation != DomainOperation.None));";
            }

            result += "\r\n " + indent + "return changeSet;";
            result += "\r\n ";

            return result;
        }

        public List<EntityAdapter> GetRepresentedEntitiesForSave()
        {
            List<EntityAdapter> result = new List<EntityAdapter>();

            Action<EntityAdapter> action = null;

            action = (entity) =>
                {
                    if (entity.HasMainTenanceForEntityRepresentations())
                        result.Add(entity);

                    entity.SourceEntityAdapters.ForEach(e => action(e));
                };

            foreach (EntityAdapter entity in this.EntityAdapters.Where(e => e.TargetEntityAdapter == null))
                action(entity);

            return result;
        }

        public EntityDataModel GetEdm()
        {
            return this.EntityDataModels.FirstOrDefault();
        }

        public string GetComments()
        {
            string result = String.Empty;

            foreach (var doc in this.Comments.Where(e => !e.Text.IsNullOrEmpty() && e.EntityAdapters.Count() == 0 && e.EntityDataModels.Count() == 0))
            {
                result += (result.IsNullOrEmpty() ? String.Empty : "\r\n") + doc.Text;
            }

            return result;
        }

        public string GetSvcFile(string contextName = "")
        {
            if (contextName.IsNullOrEmpty())
                contextName = Path.GetFileNameWithoutExtension(this.DocumentName);
            return this.TargetNamespace.Replace(".", "-") + "-" + contextName + "-" + contextName + "DomainService.svc";
        }

        public string GetHelpFile(string contextName = "", string suffix = "")
        {
            if (contextName.IsNullOrEmpty())
                contextName = Path.GetFileNameWithoutExtension(this.DocumentName);
            return this.TargetNamespace.Replace(".", "-") + "-" + contextName + "-" + contextName + "DomainService" + suffix + ".htm";
        }

        public string GetDomainServiceName()
        {
            return Path.GetFileNameWithoutExtension(this.DocumentName) + "DomainService";
        }

        public string GetServiceNameSpace()
        {
            return this.TargetNamespace + "." + Path.GetFileNameWithoutExtension(this.DocumentName);
        }

        public void VerifyPublisherAutoReference()
        {
            if (PublisherAutoReference == null)
            {

                string projectPath = GetProjectPath((this.IsAspNetCore ? this.GetEadCoreProject(this.GetEadProject()) : this.GetEadProject()));
                string metaPath = Path.Combine(projectPath, "Model\\ContextMetadata.json");
                if (System.IO.File.Exists(metaPath))
                {
                    PublisherAutoReference = new CustomizedCode.PublicationStructure(metaPath, this.GetOutputLibFile());
                }

            }
            else
                PublisherAutoReference.Update();
        }

        #region Publication Suport

        public List<string> GetPublishedEntities(bool justParententities = false, Subscription subsc = null)
        {
            List<string> result = new List<string>();

            if (subsc != null)
            {
                foreach (var entity in (justParententities ? subsc.Publisher.Entities.Where(e => !e.CompositionHierarchy.IsNullOrEmpty()) : subsc.Publisher.Entities))
                {
                    result.Add(subsc.BusinessObjectPath + "#" + entity.Namespace + "#" + entity.Name + "#" + entity.EdmName + "#" + entity.EdmEntityName + "#" + entity.IsIQueryable.ToString().ToLower() + "#" + entity.IsUpdatable.ToString().ToLower());
                }
            }
            else
            {
                //Verify external subscriptions
                foreach (var pub in this.Subscriptions)
                {
                    foreach (var entity in (justParententities ? pub.Publisher.Entities.Where(e => !e.CompositionHierarchy.IsNullOrEmpty()) : pub.Publisher.Entities))
                    {
                        result.Add(pub.BusinessObjectPath + "#" + entity.Namespace + "#" + entity.Name + "#" + entity.EdmName + "#" + entity.EdmEntityName + "#" + entity.IsIQueryable.ToString().ToLower() + "#" + entity.IsUpdatable.ToString().ToLower());
                    }
                }

                //Verify auto-subscription
                if (!this.IsInPresentationDesigner())
                {
                    this.VerifyPublisherAutoReference();
                    if (this.PublisherAutoReference != null)
                    {
                        foreach (var entity in this.PublisherAutoReference.Entities)
                        {
                            result.Add(this.PublisherAutoReference.BusinessAssemblyPath + "#" + entity.Namespace + "#" + entity.Name + "#" + entity.EdmName + "#" + entity.EdmEntityName + "#" + entity.IsIQueryable.ToString().ToLower() + "#" + entity.IsUpdatable.ToString().ToLower());
                        }
                    }
                }
            }

            return result;
        }

        public CustomizedCode.PublicationEntity GetPublishedEntityByRef(string assembly, string nameSpace, string entityName)
        {
            //Verify external subscriptions
            foreach (var pub in this.Subscriptions)
            {
                if (pub.Publisher != null)
                {
                    foreach (var entity in pub.Publisher.Entities)
                    {
                        if (Path.GetFileName(pub.BusinessObjectPath).ToLower() == Path.GetFileName(assembly).ToLower() && entity.Namespace == nameSpace && entity.Name == entityName)
                        {
                            return entity;
                        }
                    }
                }
            }

            //Verify auto-subscription
            this.VerifyPublisherAutoReference();
            if (this.PublisherAutoReference != null)
            {
                foreach (var entity in this.PublisherAutoReference.Entities)
                {
                    if (Path.GetFileName(this.PublisherAutoReference.BusinessAssemblyPath).ToLower() == Path.GetFileName(assembly).ToLower() && entity.Namespace == nameSpace && entity.Name == entityName)
                    {
                        return entity;
                    }
                }
            }

            return null;
        }

        #endregion


        #region Commons

        public string[] GetAllMultiSelectionLookups()
        {
            List<string> result = new List<string>();

            foreach (var entity in this.EntityAdapters)
            {
                foreach (var lu in entity.GetAllLookUpsInfo(false).Where(e => e.IsMultiSelection))
                {
                    if (!result.Contains(lu.Name))
                        result.Add(lu.Name);
                }
            }

            return result.ToArray();
        }

        public List<Linx.EntityAdapterDesigner.CustomizedCode.PublicationDomain> GetAllDomains()
        {
            List<Linx.EntityAdapterDesigner.CustomizedCode.PublicationDomain> result = new List<CustomizedCode.PublicationDomain>();

            //Add all domains by current model
            foreach (var domain in this.DomainViews)
            {
                if (result.Where(e => e.ClassName == domain.Name).Count() == 0)
                {
                    Linx.EntityAdapterDesigner.CustomizedCode.PublicationDomain oDomain = new Linx.EntityAdapterDesigner.CustomizedCode.PublicationDomain() { ClassName = domain.Name };
                    if (domain.HasCustomValues)
                    {
                        WebApiController dataService = this.WebApiControllers.Where(e => e.SynchronizedWithDomainService).FirstOrDefault();
                        if (dataService != null)
                            oDomain.ValuesEndpointName = dataService.GetRoutePrefix() + "/GetDomainValues?domainName=" + domain.Name;
                    }

                    foreach (var dValue in domain.DomainValues)
                    {
                        oDomain.Values.Add(new Linx.EntityAdapterDesigner.CustomizedCode.PublicationDomainProperty() { Name = dValue.Name, DisplayName = dValue.DisplayName, Value = dValue.Value });
                    }
                    result.Add(oDomain);
                }
            }

            //Add all domains by Subscriptions
            foreach (var pub in this.Subscriptions)
            {
                if (pub.Publisher != null)
                {
                    foreach (var domain in pub.Publisher.Domains)
                    {
                        if (result.Where(e => e.ClassName == domain.ClassName).Count() == 0)
                            result.Add(domain);
                    }
                }
            }

            //Get all domains from BMs
            foreach (var edm in this.EntityDataModels.Where(e => e.EdmInfo != null && e.EdmInfo.IsDbContext).Select(e => e.EdmInfo).Distinct())
            {
                var domains = GetDomainsByBusinessModel(edm.Metadata.Domains);
                if (domains != null && domains.Length > 0)
                    result.AddRange(domains);
            }

            return result;
        }

        public PublicationDomain[] GetDomainsByBusinessModel(ContextDomain[] ctxDomains)
        {
            List<PublicationDomain> domains = new List<PublicationDomain>();
            foreach (var dmDef in ctxDomains)
            {
                PublicationDomain domain = new PublicationDomain() { ClassName = dmDef.Name, NameSpace = String.Empty };
                //Add properties
                foreach (var dmValue in dmDef.Values.OrderBy(e => e.Name))
                {
                    domain.Values.Add(new PublicationDomainProperty() { Name = dmValue.Name, Value = dmValue.Value, DisplayName = dmValue.DisplayName });
                }
                domains.Add(domain);

            }

            return domains.ToArray();
        }

        public List<PublicationKpi> GetAllKpis()
        {
            PublicationKpi item;
            List<PublicationKpi> result = new List<PublicationKpi>();

            //Add all KPIs by current model
            foreach (var kpi in this.KeyPerformanceIndicators)
            {
                item = new PublicationKpi() { ClassName = kpi.Name, NameSpace = kpi.NameSpace, Description = kpi.Description, ShowType = kpi.ShowType };
                item.KpiRangeItems.AddRange(kpi.KpiRangeItems);
                result.Add(item);
            }

            //Add all KPIs by Subscriptions
            foreach (var pub in this.Subscriptions)
            {
                foreach (var kpi in pub.Publisher.Kpis)
                {
                    if (result.Where(e => e.ClassName == kpi.ClassName).Count() == 0)
                        result.Add(new PublicationKpi() { ClassName = kpi.ClassName, NameSpace = kpi.NameSpace });
                }
            }


            return result;
        }


        public void SaveLayoutDetails()
        {
            //Verify User Interfaces for Generating All Them.
            var uis = this.EntityAdapterUserInterfaces.Where(e => e.GeneratingType == DomainGeneratingType.AutomaticLayout).ToArray();
            if (uis.Length > 0)
            {
                if (uis.Any(e => !e.HasPendingChanges) && (!IsMainWindowVisible() || IsAutomaticSaving || MessageBox.Show("Do you want to generate all user interface files?", "User Interfaces", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                {
                    foreach (var ui in uis)
                    {
                        ui.HasPendingChanges = true;
                    }
                }
            }

            //Generate MVVM Files for SPA
            this.SpaCodeGen.GenerateSpaFiles();

            //Generate MVC Files for Mobile
            this.MobileCodeGen.GenerateMobileFiles();

            //Update all User Interfaces
            foreach (EntityAdapterUserInterface ui in this.EntityAdapterUserInterfaces.Where(e => e.GeneratingType == DomainGeneratingType.AutomaticLayout && e.HasPendingChanges))
            {
                if (ui.IsDefault)
                    SaveConfigurationFromDefaulUI(ui);
                ui.HasPendingChanges = false;
            }
        }


        private void SaveConfigurationFromDefaulUI(EntityAdapterUserInterface defaultUI)
        {
            if (!defaultUI.IsNull() && !defaultUI.EntityAdapter.IsNull())
            {
                if (defaultUI.EntityAdapter.CopyConfigurationFromDefaultUI)
                {
                    if (!defaultUI.LayoutContent.IsNullOrEmpty())
                    {
                        var layout = EntityAdapterUserInterface.GetLayoutDefinition(defaultUI.LayoutContent);
                        layout.CheckVersion();
                        if (!layout.IsNull())
                            ApplyConfigurationFromDefaulUI(layout, defaultUI.EntityAdapter);
                    }
                }
            }
        }

        private void ApplyConfigurationFromDefaulUI(CustomizedLayoutV2 layout, EntityAdapter entity)
        {
            LayoutControlV2 control;
            var container = layout.GetContainerByName(entity.Name + "TabItem");
            if (!container.IsNull())
                entity.DisplayName = container.DisplayName;

            foreach (var property in entity.GetAllAttributes())
            {
                control = layout.GetControlByName(property.Name);

                if (!control.IsNull())
                {
                    property.DisplayName = control.DisplayName;
                    property.Range = control.Range;
                    property.IsEditable = control.IsEditable;
                    if (!entity.IsOlap())
                        property.IsMeasure = control.IsMeasure;
                    property.MeasureFormula = control.MeasureFormula;
                    property.ConnectedAttribute = control.ConnectedAttribute;
                    property.Description = control.ToolTip;
                    if (!control.Mask.IsNullOrEmpty() && control.Mask != property.Mask)
                        property.Mask = control.Mask;
                    if (!control.MaskType.IsNullOrEmpty() && control.MaskType != property.MaskType)
                        property.MaskType = control.MaskType;
                    if (Enum.GetNames(typeof(DisplayControlType)).Where(e => e == control.ClassName).Count() > 0)
                        property.DisplayControl = (DisplayControlType)Enum.Parse(typeof(DisplayControlType), control.ClassName);
                    if (!control.DataFormatString.IsNullOrEmpty() && control.DataFormatString != property.DataFormatString)
                        property.DataFormatString = control.DataFormatString;
                    if (!control.Precision.IsNullOrEmpty() && control.Precision != property.Precision)
                        property.Precision = control.Precision;
                }
            }

            //Adjust details
            foreach (EntityAdapter entitydetail in entity.SourceEntityAdapters)
            {
                ApplyConfigurationFromDefaulUI(layout, entitydetail);
            }
        }


        public string GetProjectPath(Project current = null)
        {
            if (current == null)
                current = this.GetEadProject();
            if (current == null)
                return "";
            else
                return Path.GetDirectoryName(current.FullName);
        }


        public string GetNamespace(Project prj = null)
        {
            if (prj == null)
                prj = this.GetEadProject();

            if (prj == null)
                return "Linx";
            else
                return (string)prj.Properties.Item("DefaultNamespace").Value;
        }

        public void AdjustNamespace()
        {
            //Adjust name space.
            string nameSpace = this.GetNamespace();
            if (this.TargetNamespace != nameSpace)
                this.TargetNamespace = nameSpace;
        }

        public void AdjustContextPath()
        {
            string bmPath = this.GetFullPath("Linx.Business.Models");
            foreach (var edm in this.EntityDataModels)
            {
                if (!edm.Path.IsNullOrEmpty() && (!File.Exists(edm.Path) || (bmPath.ToUpper() != Path.GetDirectoryName(edm.Path).ToUpper())))
                {
                    string contextFilePath = Path.Combine(bmPath, Path.GetFileName(edm.Path));
                    if (File.Exists(contextFilePath))
                    {
                        using (Transaction transaction =
                           this.Store.TransactionManager.BeginTransaction("Adjusting context file path."))
                        {
                            edm.UpdateContextFile(contextFilePath);
                            transaction.Commit();
                        }
                    }
                }
            }
        }

        public string GetBusinessControllerName(string nameSpace = "", string contextName = "")
        {
            if (contextName.IsNullOrEmpty())
                contextName = this.GetDirectContextName();

            if (nameSpace.IsNullOrEmpty())
                nameSpace = this.TargetNamespace;

            string businessPrefix = String.Empty;
            string[] parts = nameSpace.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

            for (int idx = 0; idx < parts.Length - 1; idx++)
            {
                businessPrefix += parts[idx].Proper();
            }

            return businessPrefix + contextName;
        }

        public void AdjustDataService()
        {
            string apiName = this.GetBusinessControllerName();
            WebApiController dataService = this.CheckWebApiDataServices(apiName);

            if (dataService == null)
                return;

            if (!apiName.IsNullOrEmpty() && dataService.Name != apiName)
            {
                dataService.Name = apiName;
            }

            if (dataService.RoutePrefix.IsNullOrEmpty() || dataService.RoutePrefix != "{Name}")
                dataService.RoutePrefix = "{Name}";

            if (!dataService.IsDataService)
                dataService.IsDataService = true;
        }

        public string GetContextNamespace()
        {
            return this.GetNamespace() + "." + GetContextName();
        }

        public string GetDirectContextNamespace()
        {
            return this.TargetNamespace + "." + Path.GetFileNameWithoutExtension(this.DocumentName);
        }

        public string GetDirectContextName()
        {
            return Path.GetFileNameWithoutExtension(this.DocumentName);
        }

        public string GetContextName()
        {
            string contextName = String.Empty;
            ProjectItem item = GetDiagramProjectItem(this.GetEadProject());

            if (!item.IsNull())
                contextName = Path.GetFileNameWithoutExtension(item.Name);

            return contextName;
        }

        public string GetOutputLibFile()
        {
            var project = this.GetEadProject();
            if (this.IsAspNetCore)
                project = this.GetEadCoreProject(project);
            return GetOutputLibFile(project);
        }

        public string GetOutputLibFile(Project current)
        {
            return Path.Combine(Path.Combine(Path.GetDirectoryName(current.FullName), @"bin\debug"), (string)current.Properties.Item("AssemblyName").Value + ".dll");
        }

        public string GetAssemblyName()
        {
            var project = this.GetEadProject();
            if (this.IsAspNetCore)
                project = this.GetEadCoreProject(project);
            return GetAssemblyName(project);
        }

        public string GetAssemblyName(Project current)
        {
            return (current == null ? String.Empty : (string)current.Properties.Item("AssemblyName").Value);
        }

        string GetTemplatePath()
        {
            RegistryKey regKeyTemplatePath;
            string strRegistryRoot, strRegistryPath;
            Project diagramProject = this.GetEadProject();
            EnvDTE.DTE appDTE = diagramProject.DTE;

            strRegistryRoot = appDTE.RegistryRoot;
            strRegistryPath = strRegistryRoot + @"\NewProjectTemplates\TemplateDirs\{DA9FB551-C724-11d0-AE1F-00A0C90FFFC3}\/2";
            regKeyTemplatePath = Registry.LocalMachine.OpenSubKey(strRegistryPath);
            return regKeyTemplatePath.IsNull() ? String.Empty : (string)regKeyTemplatePath.GetValue("TemplatesDir");
        }

        string GetTemplateProjectPath()
        {
            RegistryKey regKeyTemplatePath;
            string strRegistryRoot, strRegistryPath;
            Project diagramProject = this.GetEadProject();
            EnvDTE.DTE appDTE = diagramProject.DTE;

            strRegistryRoot = appDTE.RegistryRoot;
            strRegistryPath = strRegistryRoot + @"\NewProjectTemplates\TemplateDirs\{DA9FB551-C724-11d0-AE1F-00A0C90FFFC3}\/1";
            regKeyTemplatePath = Registry.LocalMachine.OpenSubKey(strRegistryPath);
            return ((string)regKeyTemplatePath.GetValue("TemplatesDir")).Replace("SolutionTemplates", "ProjectTemplates");
        }

        public void UpdateLibReferences(Project project, string libFolder, bool copyLocal, bool remove = false, bool specificVersion = false, bool compareFileLocation = true)
        {
            string[] slFiles = GetLibraryFiles(libFolder);
            foreach (string file in slFiles)
            {
                UpdateReference(project, file, remove, copyLocal, specificVersion, compareFileLocation);
            }
        }

        public void RemoveLibReferences(Project project, string libFolder)
        {
            string[] slFiles = GetLibraryFiles(libFolder);
            foreach (string file in slFiles)
            {
                RemoveReference(project, file);
            }
        }

        public string GetInteractiveFolderPath(string title)
        {
            OpenFileDialog dirDlg = new OpenFileDialog();
            dirDlg.CheckFileExists = false;
            dirDlg.CheckPathExists = true;
            dirDlg.Multiselect = false;
            dirDlg.FileName = "(Get Folder)";
            dirDlg.Filter = "Folders only|*.FOLDER";
            dirDlg.Title = title;

            if (dirDlg.ShowDialog() == DialogResult.OK)
                return Path.GetDirectoryName(dirDlg.FileName);
            else
                return String.Empty;
        }


        public string GetSpecializedLookUp(string lookUpName)
        {
            CustomizedCode.PublicationLookUp lookUp;

            foreach (var subscr in this.Subscriptions)
            {
                if (subscr.Publisher != null)
                {
                    foreach (var entity in subscr.Publisher.Entities)
                    {
                        lookUp = entity.LookUps.Where(e => e.EntityName == lookUpName).FirstOrDefault();
                        if (!lookUp.IsNull())
                            return lookUp.ClassName;
                    }
                }
            }

            this.VerifyPublisherAutoReference();
            if (this.PublisherAutoReference != null)
            {
                foreach (var entity in this.PublisherAutoReference.Entities)
                {
                    lookUp = entity.LookUps.Where(e => e.EntityName == lookUpName).FirstOrDefault();
                    if (!lookUp.IsNull())
                        return lookUp.ClassName;
                }
            }


            return String.Empty;
        }


        public void SetPostBuildEventToServiceBus(Project current = null, bool copyBusiness = true, bool copyHelp = true, bool getLocalReferences = true, bool copyWepApi = false)
        {
            if (this.IsAspNetCore)
                return;

            if (current == null)
                current = this.GetEadProject();
            if (current != null)
            {

                if (this.IsInPresentationDesigner())
                {
                    current.Properties.Item("PostBuildEvent").Value = String.Empty;
                    return;
                }

                string serviceBusPath = this.GetFullPath("Linx.Web.Service.Bus");
                if (serviceBusPath.IsNullOrEmpty())
                    serviceBusPath = GetInteractiveFolderPath("Select the Services Directory:");

                if (!serviceBusPath.IsNullOrEmpty() && Directory.Exists(serviceBusPath))
                {
                    string relativePath = Path.GetDirectoryName(current.FullName).GetRelativePath(serviceBusPath);
                    if (!relativePath.IsNullOrEmpty())
                        serviceBusPath = "$(ProjectDir)" + relativePath;

                    string selfHost = this.GetFullPath("Linx.Self.Host");
                    bool hasSelfHost = Directory.Exists(selfHost);
                    if (hasSelfHost)
                    {
                        relativePath = Path.GetDirectoryName(current.FullName).GetRelativePath(selfHost);
                        if (!relativePath.IsNullOrEmpty())
                            selfHost = "$(ProjectDir)" + relativePath;
                    }

                    string businessObjectsPath = this.GetFullPath("Linx.Business.Objects");
                    if (!businessObjectsPath.IsNullOrEmpty())
                    {
                        relativePath = Path.GetDirectoryName(current.FullName).GetRelativePath(businessObjectsPath);
                        if (!relativePath.IsNullOrEmpty())
                            businessObjectsPath = "$(ProjectDir)" + relativePath;
                    }

                    string webApiPath = this.GetFullPath("Linx.WebApi");
                    if (!webApiPath.IsNullOrEmpty())
                    {
                        relativePath = Path.GetDirectoryName(current.FullName).GetRelativePath(webApiPath);
                        if (!relativePath.IsNullOrEmpty())
                            webApiPath = "$(ProjectDir)" + relativePath;
                    }

                    string postBuildEventCommand = "";
                    if (copyBusiness)
                    {
                        string contextMetadataFile = this.GetContextMetadataFile(current);
                        if (!contextMetadataFile.IsNullOrEmpty())
                        {
                            string assemblyName = GetAssemblyName(current) + ".dll";
                            string metadataFileName = assemblyName + ".meta.json";
                            postBuildEventCommand += @"xcopy """ + "$(ProjectDir)Model\\" + contextMetadataFile + @""" ""."" /Y /R" + "\r\n";
                            postBuildEventCommand += @"del """ + metadataFileName + @"""" + "\r\n";
                            postBuildEventCommand += @"rename """ + contextMetadataFile + @""" """ + metadataFileName + @"""" + "\r\n";
                        }
                    }

                    postBuildEventCommand += GetServiceBusCopyCommands(current, serviceBusPath + @"\bin", getLocalReferences);
                    if (hasSelfHost)
                    {
                        postBuildEventCommand += GetServiceBusCopyCommands(current, selfHost, getLocalReferences);
                    }

                    if (copyBusiness)
                    {
                        postBuildEventCommand += @"xcopy ""$(TargetName).dll*"" """ + businessObjectsPath + @""" /Y /R" + "\r\n";
                    }

                    if (copyWepApi)
                        postBuildEventCommand += @"xcopy ""$(TargetName).dll"" """ + webApiPath + @""" /Y /R" + "\r\n";

                    if (copyHelp && this.GetProjectItemByName(current, "Help For Accessing") != null)
                        postBuildEventCommand += @"xcopy ""$(ProjectDir)Help For Accessing\*.*"" """ + serviceBusPath + @"\Help"" /Y /R" + "\r\n";

                    if (copyBusiness)
                    {
                        postBuildEventCommand += @"cd """ + serviceBusPath + "\"\r\n" +
                        @"XmlConfigMergeConsole ""Web.config"" -m ""$(ProjectDir)Web.config""";

                        if (hasSelfHost)
                        {
                            postBuildEventCommand += @"cd """ + selfHost + "\"\r\n" +
                                @"XmlConfigMergeConsole ""Web.config"" -m ""$(ProjectDir)Web.config""";
                        }
                    }

                    if (current.Properties.Item("PostBuildEvent").Value.ToString().IsNullOrEmpty() || !current.Properties.Item("PostBuildEvent").Value.ToString().StartsWith(postBuildEventCommand))
                        current.Properties.Item("PostBuildEvent").Value = postBuildEventCommand;

                }
            }
        }

        public string GetServiceBusCopyCommands(Project project, string outputDir, bool getLocalReferences = true, string sourceDir = "$(TargetDir)")
        {
            string xCopyCommands = @"xcopy """ + sourceDir + GetAssemblyName(project) + @".dll*"" """ + outputDir + @""" /Y /R" + "\r\n";
            if (getLocalReferences)
            {
                VSLangProj.VSProject vsProject = (VSLangProj.VSProject)project.Object;
                foreach (VSLangProj.Reference reference in vsProject.References)
                {
                    if (reference.CopyLocal)
                        xCopyCommands += @"xcopy """ + sourceDir + reference.Name + @".dll"" """ + outputDir + @""" /Y /R" + "\r\n";
                }
            }
            return xCopyCommands;
        }

        public bool IsInPresentationDesigner()
        {
            Project diagramProject = this.GetEadProject();

            if (diagramProject.IsNull())
                return false;

            Project designerFolder = this.GetProjectByName("Presentation Designer");

            if (designerFolder == null)
                return false;

            return ExistsProjectItem(designerFolder.ProjectItems, diagramProject.Name);
        }

        //SPA Impact
        public WebApiController CheckWebApiDataServices(string apiName)
        {
            if (!this.IsInPresentationDesigner() && this.EntityAdapters.Count > 0)
            {
                WebApiController dataService = this.WebApiControllers.Where(e => e.SynchronizedWithDomainService).FirstOrDefault();
                if (dataService == null)
                {
                    using (Transaction transaction =
                                this.Store.TransactionManager.BeginTransaction("Changing StructuralInfo."))
                    {
                        dataService = new WebApiController(this.Store) { IsDataService = true, Name = (apiName.IsNullOrEmpty() ? "DataService" : apiName), SynchronizedWithDomainService = true, ProjectSuffix = "DS", RoutePrefix = "{Name}", EnableClient = false };
                        this.WebApiControllers.Add(dataService);
                        transaction.Commit();
                    }
                }

                return dataService;
            }

            return null;
        }

        public string GetSolutionName()
        {
            Project diagramProject = this.GetEadProject();
            if (diagramProject == null)
                return String.Empty;
            else
                return Path.GetFileNameWithoutExtension(diagramProject.DTE.Solution.FullName);
        }

        public bool ExistsModelObjects()
        {
            if (this.EntityAdapters.Count > 0)
                return true;

            if (this.EntityDataModels.Count > 0)
                return true;

            if (this.WebApiControllers.Count > 0)
                return true;

            if (this.DomainServiceExtensions.Count > 0)
                return true;

            if (this.DomainViews.Count > 0)
                return true;

            if (this.LookUpAdapters.Count > 0)
                return true;

            if (this.EntityAdapterRepresentations.Count > 0)
                return true;

            if (this.KeyPerformanceIndicators.Count > 0)
                return true;

            //Verifying if exists more then one ead            
            var project = this.GetEadProject();
            foreach (ProjectItem item in project.ProjectItems)
            {
                if (item.Name != this.DocumentName && System.IO.Path.GetExtension(item.Name).ToLower() == ".ead")
                    return true;
            }

            return false;
        }

        public bool MoveProjectToSolutionFolder(Project project, string folderName)
        {
            var itemFolder = GetProjectByName(folderName);
            if (itemFolder == null)
            {
                ((EnvDTE100.Solution4)project.DTE.Solution).AddSolutionFolder(folderName);
                itemFolder = GetProjectByName(folderName);
                
                string projectPath = project.FullName;
                //Remove from solution
                if (project.ParentProjectItem == null)
                    ((EnvDTE100.Solution4)project.DTE.Solution).Remove(project);
                else
                    project.ParentProjectItem.Remove();

                //Add projet to folder
                var designerFolder = itemFolder.Object as EnvDTE80.SolutionFolder;
                if (designerFolder != null)
                    designerFolder.AddFromFile(projectPath);

                return true;
            }

            return false;
        }

        public void UpdateUserInterfaceSolution()
        {
            //Update\Create SPA Project
            this.SpaCodeGen.UpdateSPAProject();
        }

        public string GenerateStoreScriptsCode(string indent, bool isDbContext)
        {
            Tools.CodeBuilder builder = new Tools.CodeBuilder(indent);
            string parameters;

            var scripts = this.StoreScripts.Where(e => e.StoreQueries.Count() > 0).ToList();
            if (scripts.Count > 0)
            {

                foreach (var script in scripts)
                {
                    builder.AddLine("");
                    builder.AddLine("#region Store Scripts: " + script.Name);

                    foreach (var storeQuery in script.StoreQueries)
                    {
                        parameters = String.Empty;
                        foreach (string p in storeQuery.Parameters.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            parameters += ", " + (p.Contains("=") ? p.Left("=") : p).Trim().Right(" ");
                        }
                        builder.AddLine("");
                        builder.AddLine("[Ignore()]");
                        builder.AddLine("public " + storeQuery.QueryReturnType.ToString() + "<" + storeQuery.GenericType + "> " + storeQuery.Name + "(" + storeQuery.Parameters.Replace("#", ", ") + ")");
                        builder.AddLine("{");
                        if (this.EntityDataModels.Count > 0)
                            builder.AddLine("   return this." + (isDbContext ? "DbContext.Database.SqlQuery" : "ObjectContext.ExecuteStoreQuery") + "<" + storeQuery.GenericType + ">(\"" + storeQuery.Command + "\"" + parameters + ").As" + (storeQuery.QueryReturnType == EntityQueryReturnType.IEnumerable ? "Enumerable" : "Queryable") + "();");
                        else
                            builder.AddLine("   return null;");
                        builder.AddLine("}");
                    }

                    builder.AddLine("");
                    builder.AddLine("#endregion");
                }

            }

            return builder.GetBody();
        }

        public void UpdateWebApiProject(Project eadProject, WebApiController api)
        {
            if (this.IsAspNetCore)
                return;

            EnvDTE.DTE appDTE = eadProject.DTE;
            string webApiProjectName = GetWebApiProjectName(api.ProjectSuffix, eadProject);
            Project webApiProject = GetProjectByName(webApiProjectName);
            string folderName = "Web API Controllers";
            EnvDTE80.SolutionFolder webApiDesignerFolder = null;

            if (webApiProject == null)
            {
                string webApiDir = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(eadProject.FullName), "..\\" + webApiProjectName));

                if (!System.IO.Directory.Exists(webApiDir))
                    System.IO.Directory.CreateDirectory(webApiDir);

                var tmpProj = this.GetProjectByName(folderName);
                webApiDesignerFolder = (tmpProj == null ? null : tmpProj.Object) as EnvDTE80.SolutionFolder;
                if (webApiDesignerFolder == null)
                    webApiDesignerFolder = (EnvDTE80.SolutionFolder)((EnvDTE100.Solution4)appDTE.Solution).AddSolutionFolder(folderName).Object;

                if (System.IO.File.Exists(System.IO.Path.Combine(webApiDir, webApiProjectName + ".csproj")))
                {
                    webApiDesignerFolder.AddFromFile(System.IO.Path.Combine(webApiDir, webApiProjectName + ".csproj"));
                }
                else
                {
                    // Get the location of the project templates
                    string templateName = ((EnvDTE100.Solution4)appDTE.Solution).GetProjectTemplate("Class Library (.NET Framework)", "CSharp");
                    webApiDesignerFolder.AddFromTemplate(templateName, webApiDir, webApiProjectName);
                    webApiProject = GetProjectByName(webApiProjectName);

                    //Delete Class1.cs from template project
                    if (ExistsProjectItem(webApiProject.ProjectItems, "Class1.cs"))
                        webApiProject.ProjectItems.Item("Class1.cs").Delete();

                    webApiProject.ProjectItems.AddFolder("App_Start");
                    webApiProject.ProjectItems.AddFolder("Controllers");

                    //Set Assembly Name
                    webApiProject.Properties.Item("AssemblyName").Value = webApiProjectName;
                    //Set Default Namespace
                    webApiProject.Properties.Item("DefaultNamespace").Value = webApiProjectName;
                }
            }

            this.UpdateVersion(webApiProject);
            //this.RemoveReferencesWithoutFile(webApiProject);
            this.AdjustMissingReferences(webApiProject);

            //Add project reference
            AddProjectReference(webApiProject, eadProject, false);

            //Update library references
            string gacPath = this.GetFullPath("Linx.GAC") ?? "";
            this.AddNewReference(webApiProject, "System.Net.Http.dll");
            this.UpdateLibReferences(webApiProject, "Linx.DomainServices", false);
            this.RemoveReference(webApiProject, "System.Data.Entity");
            this.AddNewReference(webApiProject, "System.Web.dll");
            this.AddNewReference(webApiProject, "System.ComponentModel.DataAnnotations.dll");
            this.UpdateReference(webApiProject, Path.Combine(gacPath, "Linx.Tools.dll"));
            this.UpdateReference(webApiProject, Path.Combine(gacPath, "Linx.LinqExtensions.dll"));
            this.AddNewReference(webApiProject, "System.ComponentModel.Composition.dll");
            this.UpdateLibReferences(webApiProject, "Linx.Business.Desktop.Tools", false);
            this.RemoveLibReferences(webApiProject, "Linx.WebApi.Library");
            this.UpdateLibReferences(webApiProject, "Linx.WebApi.Library", false, false, true);
            this.UpdateLibReferences(webApiProject, "Linx.Data.Library", false);
            this.UpdateLibReferences(webApiProject, "Linx.CodeFirst.EF", false);
            if (api.IsDataService)
            {
                this.UpdateLibReferences(webApiProject, "Linx.DataService.Library", false, false, true);
                this.UpdateLibReferences(webApiProject, "Linx.LinxDataService.Library", false, false, false);
            }

            //Set PostBuildEvent
            SetPostBuildEventToServiceBus(webApiProject, false, false, false, true);

            //Upgrade to last framework version
            UpgradeVersion(webApiProject);

            //Generate client
            if (api.EnableClient)
            {
                UpdateWebApiClientProject(eadProject, api.ProjectSuffix);
            }
        }


        public void UpdateWebApiClientProject(Project eadProject, string projectSuffix)
        {
            EnvDTE.DTE appDTE = eadProject.DTE;
            string WebApiClientProjectName = GetWebApiClientProjectName(projectSuffix, eadProject);
            Project webApiClientProject = GetProjectByName(WebApiClientProjectName);

            if (webApiClientProject == null)
            {
                string webApiDir = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(eadProject.FullName), "..\\" + WebApiClientProjectName));
                if (!System.IO.Directory.Exists(webApiDir))
                    System.IO.Directory.CreateDirectory(webApiDir);

                EnvDTE80.SolutionFolder businessDesignerFolder = null;
                string folderName = "Web API Clients";
                var tmpProj = this.GetProjectByName(folderName);
                businessDesignerFolder = (tmpProj == null ? null : tmpProj.Object) as EnvDTE80.SolutionFolder;
                if (businessDesignerFolder == null)
                    businessDesignerFolder = (EnvDTE80.SolutionFolder)((EnvDTE100.Solution4)appDTE.Solution).AddSolutionFolder(folderName).Object;

                if (System.IO.File.Exists(System.IO.Path.Combine(webApiDir, WebApiClientProjectName + ".csproj")))
                {
                    if (businessDesignerFolder != null)
                        businessDesignerFolder.AddFromFile(System.IO.Path.Combine(webApiDir, WebApiClientProjectName + ".csproj"));
                    else
                        ((EnvDTE100.Solution4)appDTE.Solution).AddFromFile(System.IO.Path.Combine(webApiDir, WebApiClientProjectName + ".csproj"), false);

                    webApiClientProject = GetProjectByName(WebApiClientProjectName);
                }
                else
                {
                    // Get the location of the project templates
                    string templateName = ((EnvDTE100.Solution4)appDTE.Solution).GetProjectTemplate("Class Library (.NET Framework)", "CSharp");
                    if (businessDesignerFolder != null)
                        businessDesignerFolder.AddFromTemplate(templateName, webApiDir, WebApiClientProjectName);
                    else
                        ((EnvDTE100.Solution4)appDTE.Solution).AddFromTemplate(templateName, webApiDir, WebApiClientProjectName, false);

                    webApiClientProject = GetProjectByName(WebApiClientProjectName);

                    //Delete Class1.cs from template project
                    if (ExistsProjectItem(webApiClientProject.ProjectItems, "Class1.cs"))
                        webApiClientProject.ProjectItems.Item("Class1.cs").Delete();

                    webApiClientProject.ProjectItems.AddFolder("Model");
                    webApiClientProject.ProjectItems.AddFolder("Controllers");

                    //Set Assembly Name
                    webApiClientProject.Properties.Item("AssemblyName").Value = WebApiClientProjectName;
                    //Set Default Namespace
                    webApiClientProject.Properties.Item("DefaultNamespace").Value = WebApiClientProjectName;
                }

            }

            //Update library references     
            string gacPath = this.GetFullPath("Linx.GAC") ?? "";
            this.AddNewReference(webApiClientProject, "System.ComponentModel.DataAnnotations.dll");
            this.UpdateReference(webApiClientProject, Path.Combine(gacPath, "Linx.Tools.dll"));
            this.AddNewReference(webApiClientProject, "System.Runtime.Serialization.dll");
            this.UpdateLibReferences(webApiClientProject, "Linx.WebApiClient.Library", false, false, true);


            //Set PostBuildEvent
            SetPostBuildEvent(webApiClientProject, "Linx.WebApiClient");

            //Upgrade to last framework version
            UpgradeVersion(webApiClientProject);

        }

        public void UpdateRepositoryProject(Project eadProject, RepositoryImplementation repository)
        {
            EnvDTE.DTE appDTE = eadProject.DTE;
            string repositoryProjectName = GetRepositoryProjectName(repository, eadProject);
            Project repositoryProject = GetProjectByName(repositoryProjectName);

            if (repositoryProject == null)
            {
                string repositoryDir = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(eadProject.FullName), "..\\" + repositoryProjectName));

                if (!System.IO.Directory.Exists(repositoryDir))
                    System.IO.Directory.CreateDirectory(repositoryDir);

                EnvDTE80.SolutionFolder businessDesignerFolder = null;
                string folderName = "Repositories";
                var tmpProj = this.GetProjectByName(folderName);
                businessDesignerFolder = (tmpProj == null ? null : tmpProj.Object) as EnvDTE80.SolutionFolder;
                if (businessDesignerFolder == null)
                    businessDesignerFolder = (EnvDTE80.SolutionFolder)((EnvDTE100.Solution4)appDTE.Solution).AddSolutionFolder(folderName).Object;

                if (System.IO.File.Exists(System.IO.Path.Combine(repositoryDir, repositoryProjectName + ".csproj")))
                {
                    if (businessDesignerFolder != null)
                        businessDesignerFolder.AddFromFile(System.IO.Path.Combine(repositoryDir, repositoryProjectName + ".csproj"));
                    else
                        ((EnvDTE100.Solution4)appDTE.Solution).AddFromFile(System.IO.Path.Combine(repositoryDir, repositoryProjectName + ".csproj"), false);

                    repositoryProject = GetProjectByName(repositoryProjectName);
                }
                else
                {
                    // Get the location of the project templates
                    string templateName = ((EnvDTE100.Solution4)appDTE.Solution).GetProjectTemplate("Class Library (.NET Framework)", "CSharp");
                    if (businessDesignerFolder != null)
                        businessDesignerFolder.AddFromTemplate(templateName, repositoryDir, repositoryProjectName);
                    else
                        ((EnvDTE100.Solution4)appDTE.Solution).AddFromTemplate(templateName, repositoryDir, repositoryProjectName, false);

                    repositoryProject = GetProjectByName(repositoryProjectName);

                    //Delete Class1.cs from template project
                    if (ExistsProjectItem(repositoryProject.ProjectItems, "Class1.cs"))
                        repositoryProject.ProjectItems.Item("Class1.cs").Delete();
                    repositoryProject.ProjectItems.AddFolder("Implementations");

                    //Set Assembly Name
                    repositoryProject.Properties.Item("AssemblyName").Value = repositoryProjectName;
                    //Set Default Namespace
                    repositoryProject.Properties.Item("DefaultNamespace").Value = repositoryProjectName;
                }
            }

            //Add project reference
            AddProjectReference(repositoryProject, eadProject, false);

            //Update library references
            string gacPath = this.GetFullPath("Linx.GAC") ?? "";
            this.AddNewReference(repositoryProject, "System.ComponentModel.Composition.dll");
            this.UpdateReference(repositoryProject, Path.Combine(gacPath, "Linx.Tools.dll"));
            this.UpdateReference(repositoryProject, Path.Combine(gacPath, "Linx.LinqExtensions.dll"));
            this.UpdateLibReferences(repositoryProject, "Linx.Data.Library", false);
            this.UpdateLibReferences(repositoryProject, "Linx.Business.Desktop.Tools", false);
            //Set PostBuildEvent
            SetPostBuildEventToServiceBus(repositoryProject, false, false, false);
            //Upgrade to last framework version
            UpgradeVersion(repositoryProject);
        }

        public void UpdateUserExtensionProject(Project eadProject)
        {
            EnvDTE.DTE appDTE = eadProject.DTE;
            string projectName = eadProject.Name + ".Extension";
            Project project = GetProjectByName(projectName);

            if (project == null)
            {
                string projectDir = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(eadProject.FullName), "..\\" + projectName));

                if (!System.IO.Directory.Exists(projectDir))
                    System.IO.Directory.CreateDirectory(projectDir);

                EnvDTE80.SolutionFolder projectFolder = null;


                if (System.IO.File.Exists(System.IO.Path.Combine(projectDir, projectName + ".csproj")))
                {
                    if (projectFolder != null)
                        projectFolder.AddFromFile(System.IO.Path.Combine(projectDir, projectName + ".csproj"));
                    else
                        ((EnvDTE100.Solution4)appDTE.Solution).AddFromFile(System.IO.Path.Combine(projectDir, projectName + ".csproj"), false);
                }
                else
                {
                    // Get the location of the project templates
                    string templateName = ((EnvDTE100.Solution4)appDTE.Solution).GetProjectTemplate("Class Library (.NET Framework)", "CSharp");
                    if (projectFolder != null)
                        projectFolder.AddFromTemplate(templateName, projectDir, projectName);
                    else
                        ((EnvDTE100.Solution4)appDTE.Solution).AddFromTemplate(templateName, projectDir, projectName, false);

                    project = GetProjectByName(projectName);

                    //Delete Class1.cs from template project
                    if (ExistsProjectItem(project.ProjectItems, "Class1.cs"))
                        project.ProjectItems.Item("Class1.cs").Delete();

                    string nameSpace = this.GetNamespace() + ".Extension";
                    string assemblyName = (string)eadProject.Properties.Item("AssemblyName").Value + ".Extension";

                    //Set Assembly Name
                    project.Properties.Item("AssemblyName").Value = assemblyName;
                    //Set Default Namespace
                    project.Properties.Item("DefaultNamespace").Value = nameSpace;
                }
            }

            this.UpdateVersion(project);
            //this.RemoveReferencesWithoutFile(project);
            this.AdjustMissingReferences(project);

            //Add assembly references.
            string gacPath = this.GetFullPath("Linx.GAC") ?? "";
            this.UpdateLibReferences(project, "Linx.Data.Library", false);
            this.UpdateReference(project, Path.Combine(gacPath, "Linx.Tools.dll"));
            this.UpdateReference(project, Path.Combine(gacPath, "Linx.LinqExtensions.dll"));
            this.AddNewReference(project, "System.Core.dll");
            this.RemoveReference(project, "System.Data.Entity");
            this.AddNewReference(project, "System.Xml.Linq.dll");
            this.UpdateLibReferences(project, "Linx.DomainServices", false);
            this.AddNewReference(project, "System.Data.dll");
            this.AddNewReference(project, "System.Data.DataSetExtensions.dll");
            this.AddNewReference(project, "System.ServiceModel.dll");

            this.AddNewReference(project, "System.ComponentModel.Composition.dll");

            AddProjectReference(project, eadProject, false);

            //Upgrade to last framework version
            UpgradeVersion(project);
        }

        public VSLangProj.Reference AddProjectReference(Project project, Project reference, bool copyLocal)
        {
            VSLangProj.VSProject vsProject = (VSLangProj.VSProject)project.Object;

            VSLangProj.Reference prjReference;
            if (ExistsReference(project, reference.Name))
                prjReference = GetReference(project, reference.Name);
            else
                prjReference = vsProject.References.AddProject(reference);

            if (prjReference != null)
                prjReference.CopyLocal = copyLocal;

            return prjReference;
        }

        public string[] GetLibraryFiles(string directoryName)
        {
            string[] libFiles = new string[] { };

            string dirtLib = this.GetDirectoryInfo(directoryName);
            if (!dirtLib.IsNullOrEmpty())
            {
                dirtLib = dirtLib.Trim().Replace("\r", "").Replace("\n", "");
                if (File.Exists(dirtLib))
                    libFiles = new string[] { dirtLib };
                else
                {
                    if (Directory.Exists(dirtLib))
                    {
                        libFiles = Directory.GetFiles(dirtLib, "*.dll", SearchOption.AllDirectories);
                    }
                    else
                    {
                        libFiles = dirtLib.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(e => e.Trim()).ToArray();
                    }
                    if (libFiles.Length == 0)
                        MessageBox.Show(String.Format("Does not exists assemblies in [{0}] directory!", directoryName), "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
                MessageBox.Show(String.Format("The directory [{0}] is not found!", directoryName), "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);


            return libFiles;
        }

        public string[] GetFileElements(string dirInfoName)
        {
            string[] fileElements = new string[] { };

            string dirtLib = this.GetDirectoryInfo(dirInfoName);
            if (!dirtLib.IsNullOrEmpty())
            {
                fileElements = dirtLib.Trim().Replace("\r", "").Replace("\n", "").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            }

            return fileElements.Select(e => e.Trim()).ToArray();
        }

        public string[] GetPhysicalFiles(string[] fileElementsOrigin, string directoryBase)
        {
            List<string> fileElements = new List<string>();

            try
            {
                foreach (var file in fileElementsOrigin)
                {
                    if (file.Contains("*")) //Get file by pattern
                    {
                        foreach (var fFile in Directory.GetFiles(Path.Combine(directoryBase, Path.GetDirectoryName(file.Replace("/", "\\"))), Path.GetFileName(file.Replace("/", "\\"))))
                        {
                            fileElements.Add(fFile);
                        }
                    }
                    else
                        fileElements.Add(Path.Combine(directoryBase, file.Replace("/", "\\")));
                }
            }
            catch (Exception excep)
            {
                CustomizedCode.Helpers.TreatException.LogError(excep);
                MessageBox.Show("'EntityAdapterDirectoryInfo.xml' Internal Error: " + excep.Message, "Problem getting physical files", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return fileElements.ToArray();
        }

        private string GetDirectoryEnvPart()
        {
            string dirPart = null;
            if (this.DTEReference != null)
            {
                var envs = GetEnvironments();
                dirPart = envs.FirstOrDefault(e => this.DTEReference.Solution.FullName.ToLower().Contains("\\" + e.Trim().ToLower() + "\\"));
            }
            return (dirPart.IsNullOrEmpty() ? "Main" : dirPart);
        }

        public string GetDirectorySourcePart()
        {
            string installedDir = GetInstalledFrameworkPath("information");
            if (installedDir.IsNullOrEmpty())
                return GetDirectoryEnvPart();
            else
                return installedDir;
        }

        public string GetLinxProgramFiles()
        {
            return "C:\\Linx Program Files";
        }

        private string GetInstalledFrameworkPath(string innerPath)
        {
            var installPath = System.IO.Path.Combine(GetLinxProgramFiles(), this.GetDistributorProductName());

            if (Directory.Exists(installPath) && File.Exists(Path.Combine(installPath, "Information\\EntityAdapterDirectoryInfo.xml")))
            {
                if (!innerPath.IsNullOrEmpty())
                    installPath = Path.Combine(installPath, innerPath);

                return installPath;
            }
            else
                return "";
        }

        private string GetLocalFrameworkPath()
        {
            if (!this.DocumentPath.IsNullOrEmpty())
            {
                string localMapPath = Path.Combine(Path.GetPathRoot(this.DocumentPath), "VSTS - GrupoLinx\\Framework");
                if (Directory.Exists(localMapPath))
                    return localMapPath;
            }

            return "";
        }

        public string GetDirectoryInfo(string directoryName)
        {
            string dirPart = String.Empty;
            string result = String.Empty;
            string tfsMiddleDir = String.Empty;
            string worksapaceMapedpath = GetInstalledFrameworkPath("");

            if (worksapaceMapedpath.IsNullOrEmpty())
            {
                dirPart = GetDirectorySourcePart();
                tfsMiddleDir = "Linx Framework\\" + dirPart + "\\Binary";
                worksapaceMapedpath = GetLocalFrameworkPath();
            }

            if (!worksapaceMapedpath.IsNullOrEmpty())
            {
                string dirInfoFile = (tfsMiddleDir.IsNullOrEmpty() ? Path.Combine(Path.Combine(worksapaceMapedpath, "information"), "EntityAdapterDirectoryInfo.xml") : Path.Combine(worksapaceMapedpath, tfsMiddleDir + "\\Library\\Common\\Linx\\Information\\EntityAdapterDirectoryInfo.xml"));
                if (File.Exists(dirInfoFile))
                {
                    try
                    {
                        System.Xml.Linq.XElement xElement = System.Xml.Linq.XElement.Load(dirInfoFile);
                        if (!xElement.IsNull())
                        {
                            System.Xml.Linq.XElement xElementFound = xElement.Elements().Where(e => e.Name == directoryName).FirstOrDefault();
                            result = (xElementFound.IsNull() ? String.Empty : xElementFound.Value.Replace("\n", String.Empty).Replace("\t", String.Empty));
                        }
                        if (tfsMiddleDir.IsNullOrEmpty())
                            result = result.Replace(@"C:\VSTS - GrupoLinx\Framework\Linx Framework\Main\Binary", worksapaceMapedpath);
                    }
                    catch (Exception exp)
                    {
                        CustomizedCode.Helpers.TreatException.LogError(exp);
                        MessageBox.Show(String.Format("Fail reading the file {0}.", dirInfoFile) + exp.Message, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }

            if (result.IsNullOrEmpty())
                MessageBox.Show(String.Format("The DirectoryInfo [{0}] is not found in the environment {1}!", directoryName, dirPart), "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                //Create Directory If Does Not Exist
                if (!Path.HasExtension(result) && !Directory.Exists(result))
                    Directory.CreateDirectory(result);
            }

            return result;
        }

        public string GetSpecializedLookupInfo(string sourceEntityName)
        {
            string dirPart = String.Empty;
            string result = String.Empty;
            string tfsMiddleDir = String.Empty;
            string worksapaceMapedpath = GetInstalledFrameworkPath("information");

            if (worksapaceMapedpath.IsNullOrEmpty())
            {
                dirPart = GetDirectorySourcePart();
                tfsMiddleDir = "Linx Framework\\" + dirPart + "\\Binary";
                worksapaceMapedpath = GetLocalFrameworkPath();
            }

            if (!worksapaceMapedpath.IsNullOrEmpty())
            {
                string luInfoFile = (tfsMiddleDir.IsNullOrEmpty() ? Path.Combine(worksapaceMapedpath, "SpecializedLookupInfo.xml") : Path.Combine(worksapaceMapedpath, tfsMiddleDir + "\\Library\\Common\\Linx\\Information\\SpecializedLookupInfo.xml"));
                if (File.Exists(luInfoFile))
                {
                    try
                    {
                        System.Xml.Linq.XElement xElement = System.Xml.Linq.XElement.Load(luInfoFile);
                        if (!xElement.IsNull())
                        {
                            System.Xml.Linq.XElement xElementFound = xElement.Elements().Where(e => e.Name == sourceEntityName).FirstOrDefault();
                            result = (xElementFound.IsNull() ? String.Empty : xElementFound.Value.Replace("\n", String.Empty).Replace("\t", String.Empty)).Trim();
                        }
                    }
                    catch (Exception exp)
                    {
                        CustomizedCode.Helpers.TreatException.LogError(exp);
                        MessageBox.Show(String.Format("Fail reading the file {0}.", luInfoFile) + exp.Message, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }

            return result;
        }

        public string[] GetEnvironments()
        {
            string[] result = new string[] { };
            string worksapaceMapedpath = null;
            worksapaceMapedpath = GetLocalFrameworkPath();

            if (!worksapaceMapedpath.IsNullOrEmpty())
            {
                string endInfoFile = Path.Combine(worksapaceMapedpath, "Linx Framework\\Environments.xml");
                if (File.Exists(endInfoFile))
                {
                    try
                    {
                        System.Xml.Linq.XElement xElementFound = System.Xml.Linq.XElement.Load(endInfoFile);
                        result = (xElementFound.IsNull() ? String.Empty : xElementFound.Value.Replace("\n", String.Empty).Replace("\t", String.Empty)).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    }
                    catch (Exception exp)
                    {
                        CustomizedCode.Helpers.TreatException.LogError(exp);
                        MessageBox.Show(String.Format("Fail reading the file {0}.", endInfoFile) + exp.Message, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }

            return result;
        }

        public string GetPrimaryKeyByEntity(string entitySetName)
        {
            EntityDataModel edm = this.EntityDataModels.FirstOrDefault();

            if (edm.IsNullOrEmpty())
                return String.Empty;

            List<string> entities = new List<string>();
            var entity = edm.EdmInfo.Metadata.Entities.FirstOrDefault(e => e.Name == entitySetName);
            if (entity != null)
                return String.Join<String>(",", entity.Properties.Where(e => e.IsPrimaryKey()).Select(e => e.Name));
            else
                return String.Empty;
        }

        public string GetFullPath(string directoryName)
        {
            string dirtLib = this.GetDirectoryInfo(directoryName);
            if (!dirtLib.IsNullOrEmpty())
                return dirtLib.Trim();
            else
                return "";
        }

        public void SetPostBuildEvent(Project project, string directoryKeyPath, bool useTargetName = true)
        {
            string directory = GetFullPath(directoryKeyPath);
            if (!directory.IsNullOrEmpty())
            {
                string relativePath = Path.GetDirectoryName(project.FullName).GetRelativePath(directory);
                if (!relativePath.IsNullOrEmpty())
                    directory = "$(ProjectDir)" + relativePath;
                string postBuildEventCommand = @"xcopy ""$" + (useTargetName ? "(TargetName).dll" : "(TargetPath)*") + @""" """ + directory + @""" /y /r";
                if (project.Properties.Item("PostBuildEvent").Value.ToString().IsNullOrEmpty() || !project.Properties.Item("PostBuildEvent").Value.ToString().Contains(postBuildEventCommand))
                    project.Properties.Item("PostBuildEvent").Value = postBuildEventCommand;
            }
        }

        public void UpdateReportProject(bool onlyCheckExistence = false)
        {
            bool isNewProject = false;
            Project diagramProject = this.GetEadProject();
            string reportProjectName = diagramProject.Name + ".Reports";
            string folderName = "Business Reports";
            Project reportProject = GetProjectByName(reportProjectName);

            if (onlyCheckExistence && reportProject != null)
                return;

            EnvDTE.DTE appDTE = diagramProject.DTE;

            //Check Existence
            if (reportProject == null)
            {
                string nameSpace = this.GetNamespace() + ".Reports";
                string assemblyName = (string)diagramProject.Properties.Item("AssemblyName").Value + ".Reports";

                string reportDir = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(diagramProject.FullName), "..\\" + reportProjectName));

                // Add the proxy project to the Solution			
                if (reportProject == null)
                {
                    if (!System.IO.Directory.Exists(reportDir))
                        System.IO.Directory.CreateDirectory(reportDir);

                    var tmpProj = this.GetProjectByName(folderName);
                    EnvDTE80.SolutionFolder businessDesignerFolder = (tmpProj == null ? null : tmpProj.Object) as EnvDTE80.SolutionFolder;
                    if (businessDesignerFolder == null)
                        businessDesignerFolder = (EnvDTE80.SolutionFolder)((EnvDTE100.Solution4)appDTE.Solution).AddSolutionFolder(folderName).Object;

                    if (System.IO.File.Exists(System.IO.Path.Combine(reportDir, reportProjectName + ".csproj")))
                    {
                        businessDesignerFolder.AddFromFile(System.IO.Path.Combine(reportDir, reportProjectName + ".csproj"));
                    }
                    else
                    {
                        // Get the location of the project templates
                        string templateName = ((EnvDTE100.Solution4)appDTE.Solution).GetProjectTemplate("Class Library (.NET Framework)", "CSharp");
                        businessDesignerFolder.AddFromTemplate(templateName, reportDir, reportProjectName);

                        reportProject = GetProjectByName(reportProjectName);

                        //Delete Connect.cs from template project
                        if (ExistsProjectItem(reportProject.ProjectItems, "Connect.cs"))
                            reportProject.ProjectItems.Item("Connect.cs").Delete();

                        //Delete Class1.cs from template project
                        if (ExistsProjectItem(reportProject.ProjectItems, "Class1.cs"))
                            reportProject.ProjectItems.Item("Class1.cs").Delete();

                        //Set up the target application (debugging host) for debugging            
                        ConfigurationManager cfgManager;
                        Configuration configuration;

                        cfgManager = reportProject.ConfigurationManager;
                        for (long index = 1; index <= cfgManager.Count; index++)
                        {
                            configuration = cfgManager.Item(index, "Any CPU");
                            configuration.Properties.Item("OutputPath").Value = @"bin\Debug";
                        }

                        //Set Output Type            
                        reportProject.Properties.Item("OutputType").Value = VSLangProj.prjOutputType.prjOutputTypeLibrary;
                        //Set Assembly Name
                        reportProject.Properties.Item("AssemblyName").Value = assemblyName;
                        //Set Default Namespace
                        reportProject.Properties.Item("DefaultNamespace").Value = nameSpace;

                        isNewProject = true;
                    }
                }
            }

            UpdateReportReferences(diagramProject, reportProject);

            //Upgraded project to last Framework Version            
            if (isNewProject)
            {
                //Save solution
                appDTE.ExecuteCommand("File.SaveAll");
            }
        }

        internal void UpdateReportUtilsFile(Project reportProject, Project diagramProject = null)
        {
            if (this.EntityAdapters.Count == 0)
                return;

            if (reportProject == null && diagramProject != null)
            {
                string reportProjectName = diagramProject.Name + ".Reports";
                reportProject = this.GetProjectByName(reportProjectName);
            }

            if (reportProject == null)
                return;

            string reportNamespace = this.TargetNamespace.ToString() + ".Reports";
            string eadName = this.GetContextName();
            string className = string.Format("{0}DataSource", eadName);
            string fileName = className + ".cs";
            string fileLocation = Path.Combine(Path.Combine(Path.GetDirectoryName(reportProject.FullName), "Utils"), fileName);

            MemoryStream ms = new MemoryStream();
            StreamWriter sw = new StreamWriter(ms);
            CSharpCodeProvider cscp = new CSharpCodeProvider();
            ICodeGenerator codeGenerator = cscp.CreateGenerator(sw);
            CodeGeneratorOptions cgo = new CodeGeneratorOptions();

            System.CodeDom.CodeNamespace ns = new System.CodeDom.CodeNamespace(reportNamespace);
            ns.Imports.Add(new CodeNamespaceImport("System"));
            ns.Imports.Add(new CodeNamespaceImport("System.Linq"));
            ns.Imports.Add(new CodeNamespaceImport("System.Text"));
            ns.Imports.Add(new CodeNamespaceImport("System.Threading.Tasks"));
            ns.Imports.Add(new CodeNamespaceImport("System.Collections.Generic"));
            ns.Imports.Add(new CodeNamespaceImport("Linx.Tools"));
            ns.Imports.Add(new CodeNamespaceImport("Linx.Business.Tools"));
            ns.Imports.Add(new CodeNamespaceImport(this.TargetNamespace.ToString() + "." + eadName));
            ns.Imports.Add(new CodeNamespaceImport("System.Net.Http"));
            ns.Imports.Add(new CodeNamespaceImport("System.Net.Http.Headers"));

            CodeTypeDeclaration reportClass = new CodeTypeDeclaration(className);
            reportClass.IsClass = true;
            reportClass.IsPartial = true;
            reportClass.TypeAttributes = System.Reflection.TypeAttributes.Public;
            ns.Types.Add(reportClass);

            var api = this.WebApiControllers.FirstOrDefault(e => e.SynchronizedWithDomainService);
            CodeMemberField field = new CodeMemberField();
            field.Attributes = MemberAttributes.Private;
            field.Name = "url = \"" + (api != null ? api.GetRoutePrefix() : "") + "/\"";
            field.Type = new CodeTypeReference("System.String");
            reportClass.Members.Add(field);

            field = new CodeMemberField();
            field.Attributes = MemberAttributes.Public;
            string contextFldName = "_" + eadName.ToCamelCase() + "Context";
            field.Name = contextFldName;
            field.Type = new CodeTypeReference(string.Format("{0}.{1}.{1}DomainService", this.TargetNamespace.ToString(), eadName));
            reportClass.Members.Add(field);

            field = new CodeMemberField();
            field.Attributes = MemberAttributes.Private;
            field.Name = "_detailsForLoading";
            field.Type = new CodeTypeReference("System.String[]");
            reportClass.Members.Add(field);

            var property = new CodeMemberProperty();
            property.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            property.Name = "DetailsForLoading";
            property.Type = new CodeTypeReference("System.String[]");
            property.GetStatements.Add(new CodeMethodReturnStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "_detailsForLoading")));
            property.SetStatements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "_detailsForLoading"), new CodePropertySetValueReferenceExpression()));
            reportClass.Members.Add(property);

            GenerateDataSourceGetFilter(reportClass);
            foreach (EntityAdapter adapter in this.EntityAdapters)
            {
                GenerateDataSourceGets(adapter, reportClass, contextFldName, eadName, false);
                if (adapter.TargetEntityAdapter != null && adapter.IsParentCompositionAllowed())
                    GenerateDataSourceGets(adapter, reportClass, contextFldName, eadName, true);
            }
            codeGenerator.GenerateCodeFromNamespace(ns, sw, cgo);
            sw.Flush();
            string body = this.GetString(ms);
            sw.Close();

            this.UpdateProjectItemFile(reportProject, "Utils", fileName, body);
        }

        private void GenerateDataSourceGetFilter(CodeTypeDeclaration reportClass)
        {
            var method = new CodeMemberMethod();
            method.Attributes = MemberAttributes.Private;
            method.Name = "GetFilterExpression";
            //Return type
            method.ReturnType = new CodeTypeReference("System.String");
            //Parameters
            method.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference("Telerik.Reporting.Processing.Report"), "report"));
            method.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference("System.Type"), "entityType"));
            method.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference("System.String[]"), "replacements"));

            method.Statements.Add(new CodeSnippetStatement(""));

            method.Statements.Add(new CodeSnippetStatement("            var filters = LinxReportHelper.GetReportFilters(report);"));

            method.Statements.Add(new CodeSnippetStatement("            //Begin: Telerik filters treatment"));
            method.Statements.Add(new CodeSnippetStatement("            foreach (var filter in filters)"));
            method.Statements.Add(new CodeSnippetStatement("            {"));
            method.Statements.Add(new CodeSnippetStatement("                if (filter.Value.Split('.').GetValue(0).ToString() == \"= Parameters\")"));
            method.Statements.Add(new CodeSnippetStatement("                {"));
            method.Statements.Add(new CodeSnippetStatement("                    var parameterName = filter.Value.Split('.').GetValue(1).ToString();"));
            method.Statements.Add(new CodeSnippetStatement("                    filter.Value = report.Parameters[parameterName].Value.ToString();"));
            method.Statements.Add(new CodeSnippetStatement("                }"));
            method.Statements.Add(new CodeSnippetStatement("            }"));
            method.Statements.Add(new CodeSnippetStatement("            //End: Telerik filters treatment"));

            method.Statements.Add(new CodeSnippetStatement("            //Begin: Adjust translated query"));
            method.Statements.Add(new CodeSnippetStatement("            var preDefinedTranslatedJqueryExpression = (report.Parameters.ContainsKey(\"PreDefinedTranslatedJqueryExpression\") ? report.Parameters[\"PreDefinedTranslatedJqueryExpression\"].Value.ToString() : \"\");"));
            method.Statements.Add(new CodeSnippetStatement("            if (!preDefinedTranslatedJqueryExpression.IsNullOrEmpty())"));
            method.Statements.Add(new CodeSnippetStatement("            {"));
            method.Statements.Add(new CodeSnippetStatement("                var translatedJqueryExpression = (report.Parameters.ContainsKey(\"TranslatedJqueryExpression\") ? report.Parameters[\"TranslatedJqueryExpression\"].Value.ToString() : \"\");"));
            method.Statements.Add(new CodeSnippetStatement("                report.Parameters[\"TranslatedJqueryExpression\"].Value = (translatedJqueryExpression + preDefinedTranslatedJqueryExpression).Replace(\")(\", \" e \");"));
            method.Statements.Add(new CodeSnippetStatement("            }"));
            method.Statements.Add(new CodeSnippetStatement("            //End: Adjust translated query"));

            method.Statements.Add(new CodeSnippetStatement("            var jEntitySearch = LinxReportHelper.ConvertFilterToJExpression(filters, entityType);"));
            method.Statements.Add(new CodeSnippetStatement("            if (report.Parameters.ContainsKey(\"PreDefinedQueryExpression\") && !report.Parameters[\"PreDefinedQueryExpression\"].Value.IsNullOrEmpty())"));
            method.Statements.Add(new CodeSnippetStatement("            {"));
            method.Statements.Add(new CodeSnippetStatement("                jEntitySearch = report.Parameters[\"PreDefinedQueryExpression\"].Value.ToString() + (jEntitySearch ?? \"\");"));
            method.Statements.Add(new CodeSnippetStatement("            }"));
            method.Statements.Add(new CodeSnippetStatement("            if (!report.Parameters[\"JqueryExpression\"].Value.IsNullOrEmpty())"));
            method.Statements.Add(new CodeSnippetStatement("            {"));
            method.Statements.Add(new CodeSnippetStatement("                jEntitySearch = report.Parameters[\"JqueryExpression\"].Value.ToString() + (jEntitySearch ?? \"\");"));
            method.Statements.Add(new CodeSnippetStatement("            }"));
            method.Statements.Add(new CodeSnippetStatement("            //Replace parent composition elements"));
            method.Statements.Add(new CodeSnippetStatement("            if (!jEntitySearch.IsNullOrEmpty() && replacements.Length > 0)"));
            method.Statements.Add(new CodeSnippetStatement("            {"));
            method.Statements.Add(new CodeSnippetStatement("                foreach (string value in replacements)"));
            method.Statements.Add(new CodeSnippetStatement("                {"));
            method.Statements.Add(new CodeSnippetStatement("                    jEntitySearch = jEntitySearch.Replace(value + \"{\", entityType.Name + \"{\");"));
            method.Statements.Add(new CodeSnippetStatement("                }"));
            method.Statements.Add(new CodeSnippetStatement("            }"));
            method.Statements.Add(new CodeSnippetStatement("            "));
            method.Statements.Add(new CodeSnippetStatement("            return jEntitySearch;"));

            reportClass.Members.Add(method);
        }


        private void GenerateDataSourceGets(EntityAdapter adapter, CodeTypeDeclaration reportClass, string contextFldName, string eadName, bool byParentComposition)
        {
            bool filterReplacement = byParentComposition || (adapter.TargetEntityAdapter != null && adapter.TargetEntityAdapter.IsDashboardFilter);
            var method = new CodeMemberMethod();
            method.Attributes = MemberAttributes.Private;
            method.Name = string.Format("GetLocal{0}", adapter.Name + (byParentComposition ? "ParentComposition" : ""));
            //Return type
            method.ReturnType = new CodeTypeReference(string.Format("IEnumerable<{0}>", adapter.Name + (byParentComposition ? "ParentComposition" : "")));
            //Parameters
            method.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference("Telerik.Reporting.Processing.Report"), "report"));

            method.Statements.Add(new CodeSnippetStatement("            IEnumerable<" + adapter.Name + (byParentComposition ? "ParentComposition" : "") + "> result = default(IEnumerable<" + adapter.Name + (byParentComposition ? "ParentComposition" : "") + ">);"));
            method.Statements.Add(new CodeSnippetStatement("            Dictionary<string, string> headers = LinxReportHelper.GetReportHeaders(report.Parameters);"));
            method.Statements.Add(new CodeSnippetStatement("            if (" + contextFldName + " == null) " + contextFldName + String.Format(" = new {0}.{1}.{1}DomainService(headers)", this.TargetNamespace.ToString(), eadName) + " { IsSecure = true };"));

            method.Statements.Add(new CodeSnippetStatement("            string entitySearchExpression = String.Empty;"));
            method.Statements.Add(new CodeSnippetStatement("            var jEntitySearch = GetFilterExpression(report, typeof(" + string.Format("{0}", adapter.Name + (byParentComposition ? "ParentComposition" : "")) + "), new string[] {" + (filterReplacement ? adapter.GetAllParentNames() : " ") + "});"));
            method.Statements.Add(new CodeSnippetStatement("            if (!jEntitySearch.IsNullOrEmpty())"));
            method.Statements.Add(new CodeSnippetStatement("            {"));
            method.Statements.Add(new CodeSnippetStatement(string.Format("                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof({0}), jEntitySearch, false, " + adapter.IsModelViewSource().ToString().ToLower() + ", " + adapter.IsOlap().ToString().ToLower() + ");", adapter.Name + (byParentComposition ? "ParentComposition" : ""))));
            method.Statements.Add(new CodeSnippetStatement("            }"));
            method.Statements.Add(new CodeSnippetStatement("            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())"));
            method.Statements.Add(new CodeSnippetStatement(string.Format("              result = this." + contextFldName + ".Get{0}ByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();", adapter.Name + (byParentComposition ? "ParentComposition" : ""))));
            method.Statements.Add(new CodeSnippetStatement("            else"));
            method.Statements.Add(new CodeSnippetStatement(string.Format("              result = this." + contextFldName + ".Get{0}ByEntitySearchNoAssociations(null).ToList();", adapter.Name + (byParentComposition ? "ParentComposition" : ""))));

            if (!byParentComposition && adapter.HasDetails())
            {
                method.Statements.Add(new CodeSnippetStatement("            if (this.DetailsForLoading != null && this.DetailsForLoading.Length > 0)"));
                method.Statements.Add(new CodeSnippetStatement("            {"));
                method.Statements.Add(new CodeSnippetStatement("               foreach (var entity in result)"));
                method.Statements.Add(new CodeSnippetStatement("               {"));
                method.Statements.Add(new CodeSnippetStatement("                   entity.FillDetails(this." + contextFldName + ", " + (adapter.GetTopParent().IsDashboardFilter ? "entitySearchExpression, jEntitySearch" : "null, null") + ", this.DetailsForLoading);"));
                method.Statements.Add(new CodeSnippetStatement("               }"));
                method.Statements.Add(new CodeSnippetStatement("            }"));
            }

            method.Statements.Add(new CodeSnippetStatement("            return result;"));
            reportClass.Members.Add(method);


            method = new CodeMemberMethod();
            method.Attributes = MemberAttributes.Public;
            method.Name = string.Format("Get{0}", adapter.Name + (byParentComposition ? "ParentComposition" : ""));
            //Return type
            method.ReturnType = new CodeTypeReference(string.Format("IEnumerable<{0}>", adapter.Name + (byParentComposition ? "ParentComposition" : "")));
            //Parameters
            method.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference("System.Object"), "reportItem"));

            method.Statements.Add(new CodeSnippetStatement("            var report = (reportItem is Telerik.Reporting.Processing.Report ? reportItem : ((Telerik.Reporting.Processing.ReportItem)(reportItem)).Report) as Telerik.Reporting.Processing.Report;"));
            method.Statements.Add(new CodeSnippetStatement("            IEnumerable<" + adapter.Name + (byParentComposition ? "ParentComposition" : "") + "> result = default(IEnumerable<" + adapter.Name + (byParentComposition ? "ParentComposition" : "") + ">);"));
            method.Statements.Add(new CodeSnippetStatement("            if (report != null && report.Parameters.ContainsKey(\"CurrentUser\") && !report.Parameters[\"CurrentUser\"].Value.IsNullOrEmpty())"));
            method.Statements.Add(new CodeSnippetStatement("            {"));
            method.Statements.Add(new CodeSnippetStatement("                return " + string.Format("GetLocal{0}", adapter.Name + (byParentComposition ? "ParentComposition" : "")) + "(report);"));
            method.Statements.Add(new CodeSnippetStatement("            }"));
            method.Statements.Add(new CodeSnippetStatement("            else"));
            method.Statements.Add(new CodeSnippetStatement("            {"));
            method.Statements.Add(new CodeSnippetStatement("                using (var client = new HttpClient())"));
            method.Statements.Add(new CodeSnippetStatement("                {"));


            method.Statements.Add(new CodeSnippetStatement("                    var userName = report.Parameters.FirstOrDefault(x => x.Key == \"Username\");"));
            method.Statements.Add(new CodeSnippetStatement("                    var password = report.Parameters.FirstOrDefault(x => x.Key == \"Password\");"));
            method.Statements.Add(new CodeSnippetStatement("                    "));

            method.Statements.Add(new CodeSnippetStatement("                    string serviceBus = (report != null && report.Parameters.ContainsKey(\"ServiceBusUrl\") && !report.Parameters[\"ServiceBusUrl\"].Value.IsNullOrEmpty() ? report.Parameters[\"ServiceBusUrl\"].Value.ToString() : \"http://localhost:1710/\");"));
            method.Statements.Add(new CodeSnippetStatement("                    string serviceAddress = \"" + string.Format("GetSample{0}?details=", adapter.Name + (byParentComposition ? "ParentComposition" : "")) + "\" + String.Join(\"-\", (this.DetailsForLoading ?? new string[] {}));"));
            method.Statements.Add(new CodeSnippetStatement("                    client.BaseAddress = new Uri(serviceBus + (serviceBus.Right(1) == \"/\" ? \"\" : \"/\") +  url);"));
            method.Statements.Add(new CodeSnippetStatement("                    client.DefaultRequestHeaders.Accept.Clear();"));
            method.Statements.Add(new CodeSnippetStatement("                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(\"application/json\"));"));

            method.Statements.Add(new CodeSnippetStatement("                    if (!userName.Key.IsNullOrEmpty() && !password.Key.IsNullOrEmpty())"));
            method.Statements.Add(new CodeSnippetStatement("                    {"));

            method.Statements.Add(new CodeSnippetStatement("                        if (userName.Value.Value.IsNullOrEmpty() || password.Value.Value.IsNullOrEmpty())"));
            method.Statements.Add(new CodeSnippetStatement("                        {"));
            method.Statements.Add(new CodeSnippetStatement("                            report.Exception = new Exception(\"Usuário ou senha não informados.\".Translate());"));
            method.Statements.Add(new CodeSnippetStatement("                            return result;"));
            method.Statements.Add(new CodeSnippetStatement("                        }"));

            method.Statements.Add(new CodeSnippetStatement("                        var jEntitySearch = GetFilterExpression(report, typeof(" + string.Format("{0}", adapter.Name + (byParentComposition ? "ParentComposition" : "")) + "), new string[] {" + (byParentComposition ? adapter.GetAllParentNames() : " ") + "});"));

            method.Statements.Add(new CodeSnippetStatement("                        serviceAddress = \"" + string.Format("Get{0}ByEntitySearchNoAssociations?jEntitySearch=\" + System.Web.HttpUtility.UrlEncode(jEntitySearch);", adapter.Name + (byParentComposition ? "ParentComposition" : ""))));

            method.Statements.Add(new CodeSnippetStatement("                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(\"Basic\", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format(\"{0}:{1}\", userName.Value.Value, password.Value.Value))));"));

            method.Statements.Add(new CodeSnippetStatement("                    }"));
            method.Statements.Add(new CodeSnippetStatement("                    else"));
            method.Statements.Add(new CodeSnippetStatement("                    {"));
            method.Statements.Add(new CodeSnippetStatement("                        if (System.AppDomain.CurrentDomain.FriendlyName.Contains(\"Telerik.ReportDesigner\"))"));
            method.Statements.Add(new CodeSnippetStatement("                        {"));
            method.Statements.Add(new CodeSnippetStatement("                            report.Exception = new Exception(\"Este relatório apenas pode ser visualizado pela aplicação Linx UX.\".Translate());"));
            method.Statements.Add(new CodeSnippetStatement("                            return result;"));
            method.Statements.Add(new CodeSnippetStatement("                        }"));
            method.Statements.Add(new CodeSnippetStatement("                        else"));
            method.Statements.Add(new CodeSnippetStatement("                        {"));
            method.Statements.Add(new CodeSnippetStatement("                            client.DefaultRequestHeaders.Add(\"CurrentUser\", \"Developer\");"));
            method.Statements.Add(new CodeSnippetStatement("                            client.DefaultRequestHeaders.Add(\"Application\", \"A9B8C7D6-E5F4-F4E6-D6C7-B8A9A9B8C7D6\");"));
            method.Statements.Add(new CodeSnippetStatement("                            client.DefaultRequestHeaders.Add(\"CurrentCompany\", \"F27FFC4F-EB6E-4484-91ED-A318A4A394B0\");"));
            method.Statements.Add(new CodeSnippetStatement("                        }"));
            method.Statements.Add(new CodeSnippetStatement("                    }"));

            method.Statements.Add(new CodeSnippetStatement("                "));
            method.Statements.Add(new CodeSnippetStatement("                    HttpResponseMessage response = client.GetAsync(serviceAddress).Result;"));
            method.Statements.Add(new CodeSnippetStatement("                    if (response.IsSuccessStatusCode)"));
            method.Statements.Add(new CodeSnippetStatement("                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<" + adapter.Name + (byParentComposition ? "ParentComposition" : "") + ">>(response.Content.ReadAsStringAsync().Result);"));
            method.Statements.Add(new CodeSnippetStatement("                    else"));
            method.Statements.Add(new CodeSnippetStatement("                    {"));
            method.Statements.Add(new CodeSnippetStatement("                        var responseContent = response.Content.ReadAsStringAsync();"));
            method.Statements.Add(new CodeSnippetStatement("                        responseContent.Wait();"));
            method.Statements.Add(new CodeSnippetStatement("                        dynamic errorMessage = Newtonsoft.Json.Linq.JObject.Parse(responseContent.Result);"));
            method.Statements.Add(new CodeSnippetStatement("                        report.Exception = new Exception((string)errorMessage.ExceptionMessage);"));
            method.Statements.Add(new CodeSnippetStatement("                    }"));
            method.Statements.Add(new CodeSnippetStatement("                }"));
            method.Statements.Add(new CodeSnippetStatement("            }"));
            method.Statements.Add(new CodeSnippetStatement(""));
            method.Statements.Add(new CodeSnippetStatement("            return result;"));

            reportClass.Members.Add(method);
        }

        public void UpdateReportReferences(Project diagramProject = null, Project reportProject = null)
        {
            if (diagramProject == null)
                diagramProject = this.GetEadProject();

            if (diagramProject == null)
                return;

            string reportProjectName = diagramProject.Name + ".Reports";
            if (reportProject == null)
            {
                reportProject = GetProjectByName(reportProjectName);
            }

            if (reportProject == null)
                return;

            this.UpdateVersion(reportProject);
            //this.RemoveReferencesWithoutFile(reportProject);
            this.AdjustMissingReferences(reportProject);

            //Set PostBuildEvent
            SetPostBuildEventToProxyForReports(reportProject);

            //Add assembly references.
            string gacPath = this.GetFullPath("Linx.GAC") ?? "";
            this.UpdateLibReferences(reportProject, "Linx.RSExtension.Library", false, false, true);
            this.UpdateLibReferences(reportProject, "Linx.Data.Library", false);
            this.UpdateLibReferences(reportProject, "Linx.Business.Desktop.Tools", false);
            this.UpdateLibReferences(reportProject, "Linx.WebApiClient.Library", false, false, true);
            this.UpdateReference(reportProject, Path.Combine(gacPath, "Linx.Tools.dll"));
            this.UpdateReference(reportProject, Path.Combine(gacPath, "Linx.LinqExtensions.dll"));
            this.AddNewReference(reportProject, "System.Core.dll");
            this.RemoveReference(reportProject, "System.Data.Entity");
            this.AddNewReference(reportProject, "System.Xml.Linq.dll");

            this.UpdateLibReferences(reportProject, "Linx.DomainServices", false);

            this.AddNewReference(reportProject, "System.Drawing.dll");
            this.AddNewReference(reportProject, "System.ComponentModel.DataAnnotations.dll");
            this.AddNewReference(reportProject, "System.Configuration.dll");
            this.AddNewReference(reportProject, "System.Data.dll");
            this.AddNewReference(reportProject, "System.Data.DataSetExtensions.dll");
            this.AddNewReference(reportProject, "System.Data.Linq.dll");
            this.AddNewReference(reportProject, "System.Runtime.Serialization.dll");
            this.AddNewReference(reportProject, "System.Security.dll");
            this.AddNewReference(reportProject, "System.ServiceModel.dll");
            this.AddNewReference(reportProject, "System.Transactions.dll");
            this.AddNewReference(reportProject, "WindowsBase.dll");
            this.AddNewReference(reportProject, "System.Web.dll");

            AddProjectReference(reportProject, diagramProject, false);
            if (this.EntityDataModels.Count > 0)
                this.AddNewReference(reportProject, this.EntityDataModels[0].Path);

            //Upgrade to last framework version
            UpgradeVersion(reportProject);
        }

        public void UpdateRSReportsProject(bool checkExistence)
        {
            string body;
            Project diagramProject = this.GetEadProject();
            string reportProjectName = diagramProject.Name + ".RSReports";
            Project reportProject = GetProjectByName(reportProjectName);

            //Check Existence
            if (checkExistence && reportProject != null)
            {
                body = GenDataSource(this.TargetNamespace.Replace(".", "").ToString(), diagramProject.Properties.Item("AssemblyGuid").Value.ToString());
                this.UpdateProjectItemFile(reportProject, "", this.TargetNamespace.Replace(".", "").ToString() + ".rds", body);
                return;
            }

            EnvDTE.DTE appDTE = diagramProject.DTE;
            string nameSpace = this.GetNamespace() + ".RSReports";
            string assemblyName = (string)diagramProject.Properties.Item("AssemblyName").Value + ".RSReports";

            string reportDir = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(diagramProject.FullName), "..\\" + reportProjectName));
            System.IO.Directory.CreateDirectory(reportDir);

            // Verify report project structure		
            if (reportProject == null)
            {
                MessageBox.Show(String.Format("The [Report Server Project] with name [{0}] does not exists!", reportProjectName), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            body = GenDataSource(this.TargetNamespace.Replace(".", "").ToString(), diagramProject.Properties.Item("AssemblyGuid").Value.ToString());
            this.UpdateProjectItemFile(reportProject, "", this.TargetNamespace.Replace(".", "").ToString() + ".rds", body);
        }

        private string GenDataSource(string dataSourceName, string dataSourceId)
        {
            MemoryStream ms = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(ms, Encoding.UTF8);
            writer.WriteStartDocument();
            writer.Flush();
            writer.Formatting = Formatting.Indented;
            writer.WriteStartElement("RptDataSource");
            writer.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
            writer.WriteAttributeString("xmlns:xsd", "http://www.w3.org/2001/XMLSchema");
            writer.WriteElementString("Name", dataSourceName);
            writer.WriteStartElement("ConnectionProperties");
            writer.WriteElementString("Extension", dataSourceName);
            writer.WriteElementString("ConnectString", string.Empty);
            writer.WriteElementString("IntegratedSecurity", "true");
            writer.WriteEndElement();
            writer.WriteElementString("DataSourceID", dataSourceId);
            writer.WriteEndElement();
            writer.Flush();
            writer.WriteEndDocument();
            string result = GetString(ms);
            writer.Close();
            return result;
        }

        private string GenDataSourceWebService(string dataSourceName, string dataSourceId, string webServiceAddress)
        {
            MemoryStream ms = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(ms, Encoding.UTF8);
            writer.WriteStartDocument();
            writer.Flush();
            writer.Formatting = Formatting.Indented;
            writer.WriteStartElement("RptDataSource");
            writer.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
            writer.WriteAttributeString("xmlns:xsd", "http://www.w3.org/2001/XMLSchema");
            writer.WriteElementString("Name", dataSourceName);
            writer.WriteStartElement("ConnectionProperties");
            writer.WriteElementString("Extension", "XML");
            writer.WriteElementString("ConnectString", webServiceAddress);
            writer.WriteElementString("IntegratedSecurity", "true");
            writer.WriteEndElement();
            writer.WriteElementString("DataSourceID", dataSourceId);
            writer.WriteEndElement();
            writer.Flush();
            writer.WriteEndDocument();
            string result = GetString(ms);
            writer.Close();
            return result;
        }

        public string GetString(MemoryStream ms)
        {
            ms.Seek(0, SeekOrigin.Begin);
            byte[] jsonBytes = new byte[ms.Length];
            ms.Read(jsonBytes, 0, (int)ms.Length);
            return Encoding.UTF8.GetString(jsonBytes);
        }

        private void SetPostBuildEventToProxyForReports(Project reportProject)
        {
            string serviceBusPath = this.GetFullPath("Linx.Web.Service.Bus");

            string relativePath = Path.GetDirectoryName(reportProject.FullName).GetRelativePath(serviceBusPath);
            if (!relativePath.IsNullOrEmpty())
                serviceBusPath = "$(ProjectDir)" + relativePath;

            string postBuildEventCommand = @"xcopy ""$(TargetDir)*Reports.dll*"" """ + serviceBusPath + @"\bin\"" /Y /R" + "\r\n";

            string selfHost = this.GetFullPath("Linx.Self.Host");
            if (Directory.Exists(selfHost))
            {
                relativePath = Path.GetDirectoryName(reportProject.FullName).GetRelativePath(selfHost);
                if (!relativePath.IsNullOrEmpty())
                    selfHost = "$(ProjectDir)" + relativePath;

                postBuildEventCommand += @"xcopy ""$(TargetDir)*Reports.dll*"" """ + selfHost + @"\"" /Y /R" + "\r\n";
            }

            if (reportProject.Properties.Item("PostBuildEvent").Value.ToString().IsNullOrEmpty() || !reportProject.Properties.Item("PostBuildEvent").Value.ToString().Contains(postBuildEventCommand))
                reportProject.Properties.Item("PostBuildEvent").Value = postBuildEventCommand;
        }

        public VSLangProj.Reference AddNewReference(string strAssemblyName, bool copyLocal = false, bool specificVersion = false)
        {
            return this.AddNewReference(this.GetEadProject(), strAssemblyName, copyLocal, specificVersion);
        }

        public VSLangProj.Reference AddNewReference(Project project, string strAssemblyName, bool copyLocal = false, bool specificVersion = false)
        {
            VSLangProj.Reference reference = null;
            try
            {
                if (!project.IsNull())
                {
                    reference = GetReference(project, strAssemblyName);
                    if (reference == null)
                    {
                        VSLangProj.VSProject vsProject = (VSLangProj.VSProject)project.Object;
                        reference = vsProject.References.Add(strAssemblyName);
                    }

                    if (reference != null)
                    {
                        reference.CopyLocal = copyLocal;
                        if (reference is Reference3)
                            ((Reference3)reference).SpecificVersion = specificVersion;
                    }
                }
            }
            catch (Exception ex)
            {
                CustomizedCode.Helpers.TreatException.LogError(new Exception("Cannot add the assembly \"" + strAssemblyName + "\" to the project!", ex));
            }


            return reference;
        }


        #region Adjust Version

        public string GetAssemblyVersion(Project project)
        {
            var properties = GetProjectItemByName(project, "Properties");
            if (properties != null)
            {
                var itemAssemblyInfo = GetProjectItemByName(properties.ProjectItems, "AssemblyInfoShared.cs");
                if (itemAssemblyInfo != null)
                {
                    string filePath = itemAssemblyInfo.Properties.Item("FullPath").Value.ToString();
                    string version = File.ReadAllText(filePath).Extract("[assembly: AssemblyVersion(\"", "\")]");
                    var assemblyVersion = new AssemblyVersionAttribute(version);
                    int build = DateTime.Today.Subtract(new DateTime(2000, 1, 1)).Days;
                    int revision = (int)DateTime.Now.Subtract(DateTime.Today).TotalSeconds / 2;
                    return assemblyVersion.Version.Replace("*", build.ToString() + "." + revision.ToString());
                }
            }

            return "1.0.0.1";
        }

        public void UpdateVersion(Project project)
        {
            var properties = GetProjectItemByName(project, "Properties");
            if (properties != null)
            {
                var itemAssemblyInfo = GetProjectItemByName(properties.ProjectItems, "AssemblyInfo.cs");
                if (itemAssemblyInfo != null)
                {
                    string body = this.GetAssemblyInfoContent(project);
                    string filePath = itemAssemblyInfo.Properties.Item("FullPath").Value.ToString();
                    if (File.ReadAllText(filePath) != body)
                    {
                        File.WriteAllText(filePath, body);
                    }
                }

                string assemblyShared = this.GetAsssemblyShared();
                if (!assemblyShared.IsNullOrEmpty())
                {
                    var itemAssemblyShared = GetProjectItemByName(properties.ProjectItems, "AssemblyInfoShared.cs");
                    if (itemAssemblyShared != null && Path.GetDirectoryName(assemblyShared).ToLower() != Path.GetDirectoryName(itemAssemblyShared.Properties.Item("FullPath").Value.ToString()).ToLower())
                    {
                        itemAssemblyShared.Remove();
                        itemAssemblyShared = null;
                    }

                    if (itemAssemblyShared == null)
                    {
                        properties.ProjectItems.AddFromFile(assemblyShared);
                    }
                }
            }
        }

        private string GetAssemblyInfoContent(EnvDTE.Project proj)
        {
            string assemblyName = proj.Properties.Item("AssemblyName").Value.ToString();
            Linx.Tools.CodeBuilder builder = new Linx.Tools.CodeBuilder();

            builder.AddLine("using System.Reflection;");
            builder.AddLine("using System.Runtime.CompilerServices;");
            builder.AddLine("using System.Runtime.InteropServices;");
            builder.AddLine();
            builder.AddLine("// General Information about an assembly is controlled through the following");
            builder.AddLine("// set of attributes. Change these attribute values to modify the information");
            builder.AddLine("// associated with an assembly.");
            builder.AddLine("[assembly: AssemblyTitle(\"" + assemblyName + "\")]");
            builder.AddLine("[assembly: AssemblyDescription(\"\")]");
            builder.AddLine("[assembly: AssemblyProduct(\"" + assemblyName + "\")]");
            builder.AddLine("[assembly: AssemblyTrademark(\"\")]");
            builder.AddLine("[assembly: AssemblyCulture(\"\")]");
            builder.AddLine();
            builder.AddLine("// Setting ComVisible to false makes the types in this assembly not visible");
            builder.AddLine("// to COM components.  If you need to access a type in this assembly from");
            builder.AddLine("// COM, set the ComVisible attribute to true on that type.");
            builder.AddLine("[assembly: ComVisible(false)]");
            builder.AddLine();
            builder.AddLine("// The following GUID is for the ID of the typelib if this project is exposed to COM");
            builder.AddLine("[assembly: Guid(\"" + GetProjectGuid(proj).ToString() + "\")]");

            return builder.GetBody();
        }

        public bool HasBusinessControl()
        {
            return (this.GetEdm() != null && this.GetEdm().EdmInfo.Metadata != null && this.GetEdm().EdmInfo.Metadata.AuthorizationEnabled);
        }

        public bool HasMainBusinessFilter(string mainEntity)
        {
            if (!this.HasBusinessControl())
                return true;
            else
            {
                var type = this.GetEdm().EdmInfo.GetTypeByName(mainEntity);
                return (type != null && type.Properties.Any(p => p.Name == "ID_GPECON"));
            }
        }

        public string GetAsssemblyShared()
        {
            if (this.DTEReference == null)
                return null;
            string dirPart = String.Empty;
            string tfsMiddleDir = String.Empty;
            string worksapaceMapedpath = GetInstalledFrameworkPath("information");

            if (worksapaceMapedpath.IsNullOrEmpty())
            {
                dirPart = GetDirectorySourcePart();
                tfsMiddleDir = "Linx Framework\\" + dirPart + "\\Binary";
                worksapaceMapedpath = GetLocalFrameworkPath();
            }

            if (worksapaceMapedpath.IsNullOrEmpty())
                return null;
            else
            {
                string asssemblySharedFile = (tfsMiddleDir.IsNullOrEmpty() ? Path.Combine(worksapaceMapedpath, "AssemblyInfoShared.cs") : Path.Combine(worksapaceMapedpath, tfsMiddleDir + "\\Library\\Common\\Linx\\AssemblyInfoShared\\AssemblyInfoShared.cs"));
                if (File.Exists(asssemblySharedFile))
                {
                    return asssemblySharedFile;
                }
                else
                    return String.Empty;
            }
        }

        #endregion

        public void RemoveReferencesWithoutFile(Project project)
        {
            return;

            //if (!project.IsNull())
            //{
            //    List<VSLangProj.Reference> references = new List<VSLangProj.Reference>();
            //    VSLangProj.VSProject vsProject = (VSLangProj.VSProject)project.Object;
            //    foreach (VSLangProj.Reference reference in vsProject.References)
            //    {
            //        if (reference.Path.IsNullOrEmpty() || !File.Exists(reference.Path))
            //            references.Add(reference);
            //    }

            //    //Delete inconsistents references
            //    foreach (VSLangProj.Reference reference in references)
            //        reference.Remove();
            //}
        }

        /// <summary>
        /// Try to adjust BV & BM missing references
        /// </summary>
        /// <param name="project">Project</param>
        public void AdjustMissingReferences(Project project)
        {
            if (!project.IsNull())
            {
                List<string> references = new List<string>();
                VSLangProj.VSProject vsProject = (VSLangProj.VSProject)project.Object;
                foreach (VSLangProj.Reference reference in vsProject.References)
                {
                    if (reference.Path.IsNullOrEmpty() || !File.Exists(reference.Path))
                    {
                        references.Add(reference.Name);
                    }
                }

                string bmPath = this.GetFullPath("Linx.Business.Models");
                string bvPath = this.GetFullPath("Linx.Business.Objects");

                //Adjust BM inconsistent references
                foreach (string reference in references)
                {
                    
                    if (reference.Right(2) == "BM" || reference.Contains(".BM.") || reference.Right(2) == "BL")
                    {
                        string bmFilePath = Path.Combine(bmPath, reference + ".dll");
                        if (File.Exists(bmFilePath)){
                            this.UpdateReference(project, bmFilePath);
                        }
                    }

                    if (reference.Right(2) == "BV")
                    {
                        string bvFilePath = Path.Combine(bvPath, reference + ".dll");
                        if (File.Exists(bvFilePath))
                        {
                            this.UpdateReference(project, bvFilePath);
                        }
                    }
                }
            }
        }


        public void RemoveReference(string strAssemblyName)
        {
            this.RemoveReference(this.GetEadProject(), strAssemblyName);
        }

        public void RemoveReference(Project project, string strAssemblyName)
        {
            try
            {
                VSLangProj.Reference reference = GetReference(project, strAssemblyName);
                if (reference != null)
                    reference.Remove();
            }
            catch (Exception excep)
            {
                CustomizedCode.Helpers.TreatException.LogError(excep);
                MessageBox.Show("Cannot remove the assembly \"" + strAssemblyName + "\" to the project!\r\nDetails:\r\n" + excep.Message, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public bool ExistsReference(Project project, string strAssemblyName)
        {
            if (!strAssemblyName.IsNullOrEmpty())
            {
                if (strAssemblyName.Right(4).ToLower() != ".dll")
                    strAssemblyName += ".dll";

                VSLangProj.VSProject vsProject = (VSLangProj.VSProject)project.Object;
                foreach (VSLangProj.Reference reference in vsProject.References)
                {
                    if (reference.Name == Path.GetFileNameWithoutExtension(strAssemblyName))
                        return true;
                }
            }

            return false;
        }

        public VSLangProj.Reference GetReference(Project project, string strAssemblyName)
        {
            if (!strAssemblyName.IsNullOrEmpty())
            {
                if (strAssemblyName.Right(4).ToLower() != ".dll")
                    strAssemblyName += ".dll";

                VSLangProj.VSProject vsProject = (VSLangProj.VSProject)project.Object;
                foreach (VSLangProj.Reference reference in vsProject.References)
                {
                    if (reference.Name == Path.GetFileNameWithoutExtension(strAssemblyName))
                        return reference;
                }
            }

            return null;
        }

        public void SyncEdmFilePath()
        {
            foreach (EntityDataModel edm in this.EntityDataModels)
            {
                string edmName = String.Empty;
                VSLangProj.Reference reference = this.GetReference(this.GetEadProject(), Path.GetFileNameWithoutExtension(edm.Path));
                if (!reference.IsNullOrEmpty() && reference.Path != edm.Path)
                    edm.Path = reference.Path;
            }
        }

        /// <summary>
        /// Remove a project item by one standard expression.
        /// </summary>
        /// <param name="items"></param>
        /// <param name="endsWith"></param>
        public static void RemoveProjectItems(ProjectItems items, string endsWith)
        {
            List<ProjectItem> selection = new List<ProjectItem>();

            foreach (ProjectItem item in items)
            {
                if (item.Name.ToLower().EndsWith(endsWith.ToLower()))
                    item.Remove();
            }
        }

        public static bool UpgradeVersion(Project project)
        {
            //Upgrade project to new Framework version if necessary        
            if ((((uint)project.Properties.Item("TargetFramework").Value) != 262406))
            {
                project.Properties.Item("TargetFrameworkMoniker").Value = (new System.Runtime.Versioning.FrameworkName(".NETFramework", new Version(4, 6, 1))).FullName;
                return true;
            }

            return false;
        }

        public static string GetCustomAttributes(string indent, string customAttributes)
        {
            string attributes = String.Empty;
            if (!customAttributes.IsNullOrEmpty())
            {
                foreach (string attribute in customAttributes.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    attributes += "\r\n" + indent + attribute;
                }
            }
            return attributes;
        }

        public static string ReadResourceContent(string resourcePath, Assembly assembly = null)
        {
            if (assembly == null)
                assembly = System.Reflection.Assembly.GetExecutingAssembly();

            return Linx.Tools.AssemblyHelper.ReadResourceContent(resourcePath, assembly);
        }

        public static bool ExistsProjectItem(ProjectItems items, string itemName)
        {
            foreach (ProjectItem item in items)
            {
                if (item.Name == itemName)
                    return true;
            }

            return false;
        }

        public Project GetEadProject()
        {
            return GetEadProject((EnvDTE.DTE)null);
        }

        public Project GetEadProject(EnvDTE.DTE vs)
        {
            Project current = null;
            if (vs == null)
                vs = GetDTE();
            if (vs != null)
            {
                foreach (EnvDTE.Project project in vs.Solution.Projects)
                {
                    current = GetEadProject(project);
                    if (!current.IsNull())
                        break;
                }
            }

            return current;
        }

        private static bool IsEadProject(Project project, string itemPath)
        {
            bool result = false;
            string fullName;

            if (!project.ProjectItems.IsNullOrEmpty())
            {
                foreach (ProjectItem item in project.ProjectItems)
                {
                    if (item.ContainingProject != null && !item.ContainingProject.FullName.IsNullOrEmpty())
                    {
                        fullName = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(item.ContainingProject.FullName), item.Name);
                        if ((fullName.ToLower() == itemPath.ToLower()) || (item.Name.ToLower() == itemPath.ToLower()))
                        {
                            result = true;
                            break;
                        }
                    }
                }
            }

            return result;
        }
      
        private Project GetEadProject(Project project)
        {
            Project current = null;

            
            if (EntityAdapterDesignerRoot.IsEadProject(project, System.IO.Path.Combine(this.DocumentPath, this.DocumentName)))
            {
                current = project;
            }
            else
            {
                if (project.ProjectItems != null && project.ProjectItems.Count > 0)
                {
                    if (!project.ProjectItems.IsNullOrEmpty())
                    {
                        foreach (ProjectItem projItem in project.ProjectItems)
                        {
                            if (projItem.SubProject != null)
                            {
                                current = GetEadProject(projItem.SubProject);
                                if (current != null)
                                    break;
                            }
                        }
                    }
                }
            }

            return current;
        }

        #region DTE Reference
        public EnvDTE.DTE DTEReference { get; set; }
        public EnvDTE.DTE GetDTE()
        {
            return DTEReference;
        }

        public bool IsMainWindowVisible()
        {
            return (DTEReference != null && DTEReference.MainWindow.Visible);
        }

        public T GetPresentation<T>() where T : PresentationElement
        {
            return PresentationViewsSubject.GetPresentation(this).FirstOrDefault() as T;
        }

        public void InstallNuGetPackage(string packageID, string version, Project project = null, string source = "")
        {
            if (project == null)
                project = this.GetEadProject();

            EntityAdapterDesignerDiagram diagram = this.GetPresentation<EntityAdapterDesignerDiagram>();
            var componentModel = (IComponentModel)diagram.GetService(typeof(SComponentModel));
            if (componentModel != null)
            {
                IVsPackageInstaller installerPackage = componentModel.GetService<IVsPackageInstaller>();
                if (installerPackage != null)
                {
                    try
                    {
                        IVsPackageInstallerServices installerServices = componentModel.GetService<IVsPackageInstallerServices>();
                        var installedPackages = installerServices.GetInstalledPackages(project);
                        if (!installedPackages.Any(p => p.Id == packageID && p.VersionString == version))
                        {
                            if (source.IsNullOrEmpty())
                                source = "https://api.nuget.org/v3/index.json";

                            installerPackage.InstallPackage(source, project, packageID, version, true);
                        }
                    }
                    catch (Exception excep)
                    {
                        MessageBox.Show(excep.Message, "Fail on installing the package: " + packageID, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }


        public void ExecuteFile(string exeFilePath, string arguments)
        {
            // Use ProcessStartInfo class
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.FileName = exeFilePath;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = arguments;

            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using statement will close.
                //using (System.Diagnostics.Process exeProcess = System.Diagnostics.Process.Start(startInfo))
                //{
                //    exeProcess.WaitForExit();
                //}

                System.Diagnostics.Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                CustomizedCode.Helpers.TreatException.LogError(ex);
                MessageBox.Show("Fail executing the file [" + startInfo.FileName + "].", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // Log error.
            }
        }


        #endregion


        public void AdjustDocumentInfo(string modelFileName)
        {
            var fileInfo = new FileInfo(modelFileName);

            if (String.IsNullOrEmpty(this.DocumentPath) || this.DocumentPath != fileInfo.DirectoryName || String.IsNullOrEmpty(this.DocumentName) || this.DocumentName != fileInfo.Name)
            {
                string oldSpaCtxClassName = this.SpaCodeGen.GetSpaContextName();
                string oldMobileCtxClassName = this.MobileCodeGen.GetMobileDataServiceApiName();

                using (Transaction transaction =
                            this.Store.TransactionManager.BeginTransaction("Changing DocumentInfo."))
                {
                    
                    this.DocumentPath = fileInfo.DirectoryName;
                    this.DocumentName = fileInfo.Name;
                    transaction.Commit();
                }

                if (oldSpaCtxClassName != this.SpaCodeGen.GetSpaContextName())
                {
                    this.SpaCodeGen.RenameSpaServiceCode(oldSpaCtxClassName);
                }

                if (oldMobileCtxClassName != this.MobileCodeGen.GetMobileDataServiceApiName())
                {
                    this.MobileCodeGen.RenameMobileDataServiceApiCode(oldSpaCtxClassName);
                }
            }
        }

        public void AdjustStructuralInfo()
        {
            this.AdjustContextPath();
            this.CheckWebApiDataServices(this.GetBusinessControllerName());

            if (this.TargetNamespace.IsNullOrEmpty())
            {
                using (Transaction transaction =
                            this.Store.TransactionManager.BeginTransaction("Changing StructuralInfo."))
                {
                    this.AdjustNamespace();
                    transaction.Commit();
                }
            }

            this.InitializedModel = true;
        }


        public Project GetProjectByName(string projectName)
        {
            return GetProjectByName((EnvDTE.DTE)null, projectName);
        }

        public Project GetProjectByName(EnvDTE.DTE vs, string projectName)
        {
            Project current = null;
            if (vs == null)
                vs = GetDTE();
            if (vs != null)
            {
                foreach (EnvDTE.Project project in vs.Solution.Projects)
                {
                    current = GetProjectByName(project, projectName);
                    if (current != null)
                        break;
                }
            }

            return current;
        }

        private Project GetProjectByName(Project project, string projectName)
        {
            Project current = null;

            if (project.Name == projectName)
                current = project;
            else
            {
                if (project.ProjectItems != null && project.ProjectItems.Count > 0)
                {
                    foreach (ProjectItem projItem in project.ProjectItems)
                    {
                        if (projItem.SubProject != null)
                        {
                            current = GetProjectByName(projItem.SubProject, projectName);
                            if (current != null)
                                break;
                        }

                    }
                }
            }

            return current;
        }

        public ProjectItem GetProjectItemByName(Project project, string itemName, bool searchSubItems = false)
        {
            return GetProjectItemByName(project.ProjectItems, itemName, searchSubItems);
        }

        public ProjectItem GetProjectItemByName(ProjectItems items, string itemName, bool searchSubItems = false)
        {
            foreach (ProjectItem item in items)
            {
                if (item.Name.ToLower() == itemName.ToLower())
                    return item;
                else if (searchSubItems)
                {
                    var element = GetProjectItemByName(item.ProjectItems, itemName, searchSubItems);
                    if (element != null)
                        return element;
                }
            }

            return null;
        }

        /// <summary>
        /// Get active window with the current EAD document.
        /// </summary>
        /// <returns></returns>
        public ProjectItem GetDiagramProjectItem(Project eadProject)
        {
            if (eadProject.IsNull())
                throw new Exception(string.Format("The document '{0}' is not in the 'Business View Project'.", this.DocumentName));

            var item = GetProjectItemByName(eadProject, this.DocumentName);
            if (item == null)
                item = GetProjectItemByName(eadProject, Path.GetFileNameWithoutExtension(this.DocumentName));

            return item;
        }

        /// <summary>
        /// Verify file in Source Control.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool VerifySourceControl(string fileName)
        {
            if (File.Exists(fileName))
                return Linx.SourceControl.TfsAccess.VerifySourceControl(this.GetEadProject().DTE, fileName);

            return true;
        }

        /// <summary>
        /// Get entity fullpath.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private string GetEntityPath(EntityAdapter entity)
        {
            if (entity.TargetEntityAdapter == null)
                return entity.Name;
            else
                return GetEntityPath(entity.TargetEntityAdapter) + "." + entity.Name;
        }


        /// <summary>
        /// Returns the EntityRepresentation data by the correspondent property name.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public string GetDataRepresentationKeyBypropertyName(EntityAdapter entity, string propertyName)
        {
            var prop = entity.GetAllInheritanceAttributes().Where(e => e.Name == propertyName).FirstOrDefault();
            if (prop != null)
                return prop.DataRelationKey.Left("#") + "." + prop.DataRelationKey.Right(".");
            else
                return string.Empty;
        }

        /// <summary>
        /// Returns the EDM field path by the correspondent property name.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public string GetEdmkeyByPropertyName(EntityAdapter entity, string propertyName)
        {
            string edmKey = "";
            foreach (EntityAdapterProperty prop in entity.GetAllInheritanceProperties())
            {
                if (prop.Name == propertyName)
                {
                    edmKey = prop.EdmKey;
                    break;
                }
            }
            return edmKey;
        }


        #endregion Commons

        #region Templates

        public void UpdateDataEntityFunctionsTemplate(Project current, bool isNetCore = false)
        {
            string outputFile = "";
            ProjectItem folder = null, projItem;

            if (current != null)
            {
                string folderName = "Includes";
                string body = ReadResourceContent(@"Linx.EntityAdapterDesigner." + (isNetCore ? "CoreTemplates" : "Templates") + ".DataEntityFunctionsTemplate.txt");

                folder = this.GetProjectItemByName(current, folderName);
                if (folder == null)
                    folder = current.ProjectItems.AddFolder(folderName, Constants.vsProjectItemKindPhysicalFolder);

                if (folder != null)
                {
                    outputFile = Path.Combine(Path.Combine(GetProjectPath(current), folderName), "DataEntityFunctions.ttinclude");
                    if (!this.VerifySourceControl(outputFile))
                        return;

                    if (!ExistsProjectItem(folder.ProjectItems, "DataEntityFunctions.ttinclude"))
                    {
                        File.WriteAllText(outputFile, body);
                        projItem = folder.ProjectItems.AddFromFile(outputFile);
                    }
                    else
                    {
                        if (File.ReadAllText(outputFile) != body)
                            File.WriteAllText(outputFile, body);
                    }
                }
            }
        }


        public void SaveProjectTemplateFile(Project current, ProjectItem folder, string fileName, string resourceName)
        {
            string body, outputFile;

            if (current == null)
                return;

            if (folder == null)
            {
                body = AdjustGacPath(ReadResourceContent(resourceName));
                outputFile = Path.Combine(this.GetProjectPath(current), fileName);
                this.VerifySourceControl(outputFile);

                if (!ExistsProjectItem(current.ProjectItems, fileName))
                {
                    File.WriteAllText(outputFile, body);
                    current.ProjectItems.AddFromFile(outputFile);
                }
                else
                {
                    if (File.ReadAllText(outputFile) != body)
                        File.WriteAllText(outputFile, body);
                }
            }
            else
            {
                body = AdjustGacPath(ReadResourceContent(resourceName));
                outputFile = Path.Combine(folder.Properties.Item("FullPath").Value.ToString(), fileName);
                this.VerifySourceControl(outputFile);

                if (!ExistsProjectItem(folder.ProjectItems, fileName))
                {
                    File.WriteAllText(outputFile, body);
                    current.ProjectItems.AddFromFile(outputFile);
                }
                else
                {
                    if (File.ReadAllText(outputFile) != body)
                        File.WriteAllText(outputFile, body);
                }
            }
        }

        public void UpdateEntityAdapterDynamicModelsTemplate(Project current, bool directDiagram = false, Project reference = null)
        {
            string outputFile = "";
            ProjectItem folder = null, projItem;
            string refPath = "", projPath = "";
            if (reference != null)
            {
                projPath = @"..\" + Path.GetFileName(GetProjectPath(reference)) + @"\";
                refPath = @"..\\";
            }

            if (current != null)
            {
                string folderName = "Includes";
                string body = AdjustGacPath(ReadResourceContent(@"Linx.EntityAdapterDesigner.Templates.EntityAdapterDynamicModelsTemplate.txt"));
                if (directDiagram)
                    body = body.Replace("fileName='..\\\\", "fileName='");

                body = body.Replace("#ProjPath#", projPath);
                body = body.Replace("#RefPath#", refPath);

                folder = this.GetProjectItemByName(current, folderName);
                if (folder == null)
                    folder = current.ProjectItems.AddFolder(folderName, Constants.vsProjectItemKindPhysicalFolder);

                if (folder != null)
                {
                    string templateName = (directDiagram ? "Direct" : String.Empty) + "EntityAdapterDynamicModels.tt";
                    outputFile = Path.Combine(Path.Combine(GetProjectPath(current), folderName), templateName);
                    if (!this.VerifySourceControl(outputFile))
                        return;

                    if (!ExistsProjectItem(folder.ProjectItems, templateName))
                    {
                        File.WriteAllText(outputFile, body);
                        projItem = folder.ProjectItems.AddFromFile(outputFile);
                        projItem.Properties.Item("CustomTool").Value = "TextTemplatingFileGenerator";
                    }
                    else
                    {
                        if (File.ReadAllText(outputFile) != body)
                            File.WriteAllText(outputFile, body);
                        projItem = folder.ProjectItems.Item(templateName);
                    }
                    //Run Template
                    ((VSLangProj.VSProjectItem)projItem.Object).RunCustomTool();
                }
            }
        }

        private string AdjustGacPath(string body)
        {
            string gacPath = this.GetFullPath("Linx.GAC") ?? "";
            if (!gacPath.IsNullOrEmpty())
                return body.Replace("#GAC#", gacPath + "\\");
            else
                return body;
        }

        public void UpdateDomainViewsTemplate(Project current, string templateNameFrom, string templateNameTo)
        {
            string outputFile = "";
            ProjectItem folder = null, projItem;

            if (current != null)
            {
                string folderName = "Domains";
                string body = AdjustGacPath(ReadResourceContent(@"Linx.EntityAdapterDesigner.Templates." + templateNameFrom + ".txt"));

                folder = this.GetProjectItemByName(current, folderName);
                if (folder == null)
                    folder = current.ProjectItems.AddFolder(folderName, Constants.vsProjectItemKindPhysicalFolder);

                if (folder != null)
                {
                    outputFile = Path.Combine(Path.Combine(GetProjectPath(current), folderName), templateNameTo + ".tt");
                    if (!this.VerifySourceControl(outputFile))
                        return;

                    bool newFile = false;
                    if (!ExistsProjectItem(folder.ProjectItems, templateNameTo + ".tt"))
                    {
                        File.WriteAllText(outputFile, body);
                        projItem = folder.ProjectItems.AddFromFile(outputFile);
                        projItem.Properties.Item("CustomTool").Value = "TextTemplatingFileGenerator";
                        newFile = true;
                    }
                    else
                    {
                        if (File.ReadAllText(outputFile) != body)
                            File.WriteAllText(outputFile, body);
                        projItem = folder.ProjectItems.Item(templateNameTo + ".tt");
                    }
                    //Run Template
                    if (newFile || !IsAutomaticSaving)
                        ((VSLangProj.VSProjectItem)projItem.Object).RunCustomTool();
                }
            }
        }

        public void UpdateKPIViewsTemplate(Project current)
        {
            string outputFile = "";
            ProjectItem folder = null, projItem;

            if (current != null)
            {
                string folderName = "KPIs";
                string body = AdjustGacPath(ReadResourceContent(@"Linx.EntityAdapterDesigner.Templates.KPIViewsTemplate.txt"));

                folder = this.GetProjectItemByName(current, folderName);
                if (folder == null)
                    folder = current.ProjectItems.AddFolder(folderName, Constants.vsProjectItemKindPhysicalFolder);

                if (folder != null)
                {
                    outputFile = Path.Combine(Path.Combine(GetProjectPath(current), folderName), "KPIViews.tt");
                    if (!this.VerifySourceControl(outputFile))
                        return;

                    bool newFile = false;
                    if (!ExistsProjectItem(folder.ProjectItems, "KPIViews.tt"))
                    {
                        File.WriteAllText(outputFile, body);
                        projItem = folder.ProjectItems.AddFromFile(outputFile);
                        projItem.Properties.Item("CustomTool").Value = "TextTemplatingFileGenerator";
                        newFile = true;
                    }
                    else
                    {
                        if (File.ReadAllText(outputFile) != body)
                            File.WriteAllText(outputFile, body);
                        projItem = folder.ProjectItems.Item("KPIViews.tt");
                    }
                    //Run Template
                    if (newFile || !IsAutomaticSaving)
                        ((VSLangProj.VSProjectItem)projItem.Object).RunCustomTool();
                }
            }
        }

        public void UpdateProjectItemFile(Project current, string folderName, string fileName, string body)
        {
            string outputFile = "";
            ProjectItem projItem, folder = null;
            ProjectItems items = null;

            if (!folderName.IsNullOrEmpty())
            {
                folder = this.GetProjectItemByName(current, folderName);
                if (folder == null)
                    folder = current.ProjectItems.AddFolder(folderName, Constants.vsProjectItemKindPhysicalFolder);
                if (!folder.IsNull())
                    items = folder.ProjectItems;
            }
            else
                items = current.ProjectItems;


            if (!items.IsNull())
            {
                outputFile = Path.Combine(Path.Combine(Path.GetDirectoryName(current.FullName), folderName), fileName);
                if (!this.VerifySourceControl(outputFile))
                    return;

                if (!ExistsProjectItem(items, fileName))
                {
                    File.WriteAllText(outputFile, body);
                    projItem = items.AddFromFile(outputFile);
                    if (Path.GetExtension(fileName).ToLower() == ".tt")
                        projItem.Properties.Item("CustomTool").Value = "TextTemplatingFileGenerator";
                }
                else
                {
                    if (File.ReadAllText(outputFile) != body)
                        File.WriteAllText(outputFile, body);
                    projItem = items.Item(fileName);
                }
                //Run Template
                if (Path.GetExtension(fileName).ToLower() == ".tt")
                    ((VSLangProj.VSProjectItem)projItem.Object).RunCustomTool();
            }

        }

        public string GenGetClientDomains(string indent, bool erp, bool isNetCore = false)
        {
            if (erp)
                return "\r\n " + indent + "return new string[] { \"" + this.GetAppName() + "_ClientErpDataDomainsFactory\", Linx.Tools.AssemblyHelper.ReadResourceContent(\"" + this.GetNamespace() + ".ClientResources.ClientErpDataDomainsFactory.res\", " + (isNetCore ? "System.Reflection.Assembly.GetEntryAssembly()" : "System.Reflection.Assembly.GetExecutingAssembly()") + ") };";
            else
                return "\r\n " + indent + "return new string[] { \"" + this.GetAppName() + "_MobileDataDomains\", Linx.Tools.AssemblyHelper.ReadResourceContent(\"" + this.GetNamespace() + ".ClientResources.MobileDataDomains.res\", " + (isNetCore ? "System.Reflection.Assembly.GetEntryAssembly()" : "System.Reflection.Assembly.GetExecutingAssembly()") + ") };";
        }

        public string GenGetClientService(string indent, bool erp, bool isNetCore = false)
        {
            string ctxClassName = (erp ? this.ClientErpCodeGen.GetClientErpDataServiceApiName() : this.MobileCodeGen.GetMobileDataServiceApiName());
            return "\r\n " + indent + "return new string[] { \"" + this.GetAppName() + "_" + ctxClassName + "\", Linx.Tools.AssemblyHelper.ReadResourceContent(\"" + this.GetNamespace() + ".ClientResources." + ctxClassName + ".res\", " + (isNetCore ? "System.Reflection.Assembly.GetEntryAssembly()" : "System.Reflection.Assembly.GetExecutingAssembly()") + ") };";
        }

        public string GenGetClientFactory(string indent, bool isExtended, bool erp, bool isNetCore = false)
        {
            string body = "";
            foreach (var clService in this.ClientLocalServices)
            {
                //Service
                string className = (erp ? this.ClientErpCodeGen.GetClientErpDataFactoryName(clService, isExtended) : this.MobileCodeGen.GetMobileDataFactoryName(clService, isExtended));
                body += "\r\n " + indent + "if (entityName == \"" + clService.EntityAdapter.Name + "\") return new string[] { \"" + this.GetAppName() + "_" + className + "\", Linx.Tools.AssemblyHelper.ReadResourceContent(\"" + this.GetNamespace() + ".ClientResources." + className + ".res\", " + (isNetCore ? "System.Reflection.Assembly.GetEntryAssembly()" : "System.Reflection.Assembly.GetExecutingAssembly()") + ") };";
            }

            body += "\r\n " + indent + (body.IsNullOrEmpty() ? "" : "else ") + "return new string[] { };";

            return body;
        }

        public void UpdateClientServicesResources(Project current)
        {
            var api = this.WebApiControllers.FirstOrDefault(e => e.SynchronizedWithDomainService);
            if (api == null || this.ClientLocalServices.Count == 0)
                return;

            string folderName = "ClientResources", outputFile = "", fileName;

            if (!current.IsNull())
            {
                string body;
                Tools.CodeBuilder codeBuilder;
                ProjectItem folder = this.GetProjectItemByName(current, folderName);
                if (folder == null)
                    folder = current.ProjectItems.AddFolder(folderName, Constants.vsProjectItemKindPhysicalFolder);

                if (folder != null)
                {
                    //Factory MobileDataDomains.js
                    var dataDomains = this.GetProjectItemByName(current, "MobileDataDomains.js", true);
                    if (dataDomains != null)
                    {
                        string domainsPath = dataDomains.Properties.Item("FullPath").Value.ToString();

                        fileName = "MobileDataDomains.res";
                        outputFile = Path.Combine(Path.Combine(GetProjectPath(current), folderName), fileName);

                        body = File.ReadAllText(domainsPath);

                        //Adjust resource core
                        body = body.Replace("var name = namespace.common.buildNameSpace('factories.MobileDataDomains');", "var name = '" + this.GetAppName() + "_MobileDataDomains';");

                        codeBuilder = new Tools.CodeBuilder();
                        codeBuilder.Add(body);
                        this.WriteFile(outputFile, codeBuilder, folder.ProjectItems, true);
                    }

                    //Factory ClientErpDataDomainsFactory.js
                    dataDomains = this.GetProjectItemByName(current, "ClientErpDataDomainsFactory.js", true);
                    if (dataDomains != null)
                    {
                        string domainsPath = dataDomains.Properties.Item("FullPath").Value.ToString();

                        fileName = "ClientErpDataDomainsFactory.res";
                        outputFile = Path.Combine(Path.Combine(GetProjectPath(current), folderName), fileName);

                        body = File.ReadAllText(domainsPath);

                        //Adjust resource core
                        body = body.Replace("var name = namespace.common.buildNameSpace('factories.ClientErpDataDomainsFactory');", "var name = '" + this.GetAppName() + "_ClientErpDataDomainsFactory';");

                        codeBuilder = new Tools.CodeBuilder();
                        codeBuilder.Add(body);
                        this.WriteFile(outputFile, codeBuilder, folder.ProjectItems, true);
                    }

                    //ODATA Service 
                    //Mobile
                    string ctxClassName = this.MobileCodeGen.GetMobileDataServiceApiName();
                    outputFile = Path.Combine(folder.Properties.Item("FullPath").Value.ToString(), ctxClassName + ".res");
                    codeBuilder = new Linx.Tools.CodeBuilder();
                    this.MobileCodeGen.GenerateMobileDataServiceApiCode(api, codeBuilder, ctxClassName, true);
                    this.WriteFile(outputFile, codeBuilder, folder.ProjectItems, true);
                    //ClientApp
                    ctxClassName = this.ClientErpCodeGen.GetClientErpDataServiceApiName();
                    outputFile = Path.Combine(folder.Properties.Item("FullPath").Value.ToString(), ctxClassName + ".res");
                    codeBuilder = new Linx.Tools.CodeBuilder();
                    this.ClientErpCodeGen.GenerateClientErpDataServiceApiCode(api, codeBuilder, ctxClassName);
                    this.WriteFile(outputFile, codeBuilder, folder.ProjectItems, true);

                    //Business Factories                    
                    foreach (var clService in this.ClientLocalServices)
                    {
                        //Mobile
                        //Factory
                        string className = this.MobileCodeGen.GetMobileDataFactoryName(clService);
                        outputFile = Path.Combine(folder.Properties.Item("FullPath").Value.ToString(), className + ".res");
                        codeBuilder = new Linx.Tools.CodeBuilder();
                        this.MobileCodeGen.GenerateMobileDataFactoryCode(clService, codeBuilder, true);
                        this.WriteFile(outputFile, codeBuilder, folder.ProjectItems, true);
                        //Extended Factory
                        className = this.MobileCodeGen.GetMobileDataFactoryName(clService, true);
                        outputFile = Path.Combine(folder.Properties.Item("FullPath").Value.ToString(), className + ".res");
                        codeBuilder = new Linx.Tools.CodeBuilder();
                        this.MobileCodeGen.GenerateMobileDataFactoryExtendedCode(clService, codeBuilder, true);
                        this.WriteFile(outputFile, codeBuilder, folder.ProjectItems, true);

                        //ClientApp
                        //Factory
                        className = this.ClientErpCodeGen.GetClientErpDataFactoryName(clService);
                        outputFile = Path.Combine(folder.Properties.Item("FullPath").Value.ToString(), className + ".res");
                        codeBuilder = new Linx.Tools.CodeBuilder();
                        this.ClientErpCodeGen.GenerateClientErpDataFactoryCode(clService, codeBuilder);
                        this.WriteFile(outputFile, codeBuilder, folder.ProjectItems, true);
                        //Extended Factory
                        className = this.ClientErpCodeGen.GetClientErpDataFactoryName(clService, true);
                        outputFile = Path.Combine(folder.Properties.Item("FullPath").Value.ToString(), className + ".res");
                        codeBuilder = new Linx.Tools.CodeBuilder();
                        this.ClientErpCodeGen.GenerateClientErpDataFactoryExtendedCode(clService, codeBuilder);
                        this.WriteFile(outputFile, codeBuilder, folder.ProjectItems, true);
                    }

                }
            }
        }

        public void DeleteExtendedEdmTemplate()
        {
            Project current = this.GetEadProject();
            var folder = this.GetProjectItemByName(current, "EDM Extensions");
            if (folder != null)
                folder.Delete();
        }

        public void UpdateAppConfigTemplate()
        {
            string outputFile = "";
            Project current = this.GetEadProject();
            ProjectItem newItem = null;
            string body = ReadResourceContent(@"Linx.EntityAdapterDesigner.Templates.WebConfigTemplate.txt");

            if (!current.IsNull())
            {
                //Remove old version
                if (ExistsProjectItem(current.ProjectItems, "Configurations"))
                {
                    var folder = GetProjectItemByName(current, "Configurations");
                    folder.Delete();
                }

                outputFile = Path.Combine(this.GetProjectPath(), "Web.tt");
                if (!this.VerifySourceControl(outputFile))
                    return;

                bool newFile = false;
                if (!ExistsProjectItem(current.ProjectItems, "Web.tt"))
                {
                    File.WriteAllText(outputFile, body);
                    newItem = current.ProjectItems.AddFromFile(outputFile);
                    newItem.Properties.Item("CustomTool").Value = "TextTemplatingFileGenerator";
                    newFile = true;
                }
                else
                {
                    if (File.ReadAllText(outputFile) != body)
                        File.WriteAllText(outputFile, body);
                    newItem = current.ProjectItems.Item("Web.tt");
                }
                //Run Template
                if (newFile || !IsAutomaticSaving)
                    ((VSLangProj.VSProjectItem)newItem.Object).RunCustomTool();
            }
        }

        public void UpdateDomainServiceTemplate(Project current, Project diagramProject = null, bool isNetCore = false)
        {
            string outputFile = "";
            ProjectItem newItem = null;
            ProjectItem item = GetDiagramProjectItem(current);

            if (!item.IsNull())
            {
                string contextNameSpace = Path.GetFileNameWithoutExtension(this.DocumentName);
                string body = AdjustGacPath(ReadResourceContent(@"Linx.EntityAdapterDesigner." + (isNetCore ? "CoreTemplates" : "Templates") + ".DomainServiceTemplate.txt"));
                body = body.Replace("#ContextFileName#", (item.Name == this.DocumentName ? "" : @"..\..\" + Path.GetFileName(GetProjectPath(diagramProject)) + @"\") + this.DocumentName);
                body = body.Replace("#ContextNamespace#", contextNameSpace);
                if (item.Name != this.DocumentName)
                    body = body.Replace("Includes\\DataEntityFunctions.ttinclude", "..\\Includes\\DataEntityFunctions.ttinclude");

                outputFile = Path.Combine(GetProjectPath(current), (item.Name == this.DocumentName ? "" : item.Name + "\\") + contextNameSpace + ".DomainService.tt");
                if (!this.VerifySourceControl(outputFile))
                    return;

                if (!ExistsProjectItem(item.ProjectItems, contextNameSpace + ".DomainService.tt"))
                {
                    File.WriteAllText(outputFile, body);
                    newItem = item.ProjectItems.AddFromFile(outputFile);
                    newItem.Properties.Item("CustomTool").Value = "TextTemplatingFileGenerator";
                }
                else
                {
                    if (File.ReadAllText(outputFile) != body)
                        File.WriteAllText(outputFile, body);
                    newItem = item.ProjectItems.Item(contextNameSpace + ".DomainService.tt");
                }
                //Run Template
                ((VSLangProj.VSProjectItem)newItem.Object).RunCustomTool();
            }
        }

        public void UpdateFormulasTemplate(Project current, Project diagramProject = null)
        {
            string outputFile = "";
            ProjectItem newItem = null;
            ProjectItem item = GetDiagramProjectItem(current);

            if (!item.IsNull())
            {
                string contextNameSpace = Path.GetFileNameWithoutExtension(this.DocumentName);
                string body = AdjustGacPath(ReadResourceContent(@"Linx.EntityAdapterDesigner.Templates.FormulasTemplate.txt"));
                body = body.Replace("#IsNetCore#", (diagramProject != null).ToString().ToLower());
                body = body.Replace("#ContextFileName#", (item.Name == this.DocumentName ? "" : @"..\..\" + Path.GetFileName(GetProjectPath(diagramProject)) + @"\") + this.DocumentName);
                body = body.Replace("#ContextNamespace#", contextNameSpace);
                if (item.Name != this.DocumentName)
                    body = body.Replace("Includes\\DataEntityFunctions.ttinclude", "..\\Includes\\DataEntityFunctions.ttinclude");

                outputFile = Path.Combine(GetProjectPath(current), (item.Name == this.DocumentName ? "" : item.Name + "\\") + contextNameSpace + ".Formulas.tt");
                if (!this.VerifySourceControl(outputFile))
                    return;

                if (!ExistsProjectItem(item.ProjectItems, contextNameSpace + ".Formulas.tt"))
                {
                    File.WriteAllText(outputFile, body);
                    newItem = item.ProjectItems.AddFromFile(outputFile);
                    newItem.Properties.Item("CustomTool").Value = "TextTemplatingFileGenerator";
                }
                else
                {
                    if (File.ReadAllText(outputFile) != body)
                        File.WriteAllText(outputFile, body);
                    newItem = item.ProjectItems.Item(contextNameSpace + ".Formulas.tt");
                }
                //Run Template
                ((VSLangProj.VSProjectItem)newItem.Object).RunCustomTool();
            }
        }

        public void UpdateExtendedFiltersTemplate(Project current, Project diagramProject = null)
        {
            string outputFile = "";
            ProjectItem newItem = null;
            ProjectItem item = GetDiagramProjectItem(current);

            if (!item.IsNull())
            {
                string contextNameSpace = Path.GetFileNameWithoutExtension(this.DocumentName);
                string body = AdjustGacPath(ReadResourceContent(@"Linx.EntityAdapterDesigner.Templates.ExtendedFiltersTemplate.txt"));
                body = body.Replace("#ContextFileName#", (item.Name == this.DocumentName ? "" : @"..\..\" + Path.GetFileName(GetProjectPath(diagramProject)) + @"\") + this.DocumentName);
                body = body.Replace("#ContextNamespace#", contextNameSpace);
                if (item.Name != this.DocumentName)
                    body = body.Replace("Includes\\DataEntityFunctions.ttinclude", "..\\Includes\\DataEntityFunctions.ttinclude");

                outputFile = Path.Combine(GetProjectPath(current), (item.Name == this.DocumentName ? "" : item.Name + "\\") + contextNameSpace + ".ExtendedFilters.tt");
                if (!this.VerifySourceControl(outputFile))
                    return;

                if (!ExistsProjectItem(item.ProjectItems, contextNameSpace + ".ExtendedFilters.tt"))
                {
                    File.WriteAllText(outputFile, body);
                    newItem = item.ProjectItems.AddFromFile(outputFile);
                    newItem.Properties.Item("CustomTool").Value = "TextTemplatingFileGenerator";
                }
                else
                {
                    if (File.ReadAllText(outputFile) != body)
                        File.WriteAllText(outputFile, body);
                    newItem = item.ProjectItems.Item(contextNameSpace + ".ExtendedFilters.tt");
                }
                //Run Template
                ((VSLangProj.VSProjectItem)newItem.Object).RunCustomTool();
            }
        }

        public void UpdateLookUpsTemplate(Project current, Project diagramProject = null)
        {
            string outputFile = "";
            ProjectItem newItem = null;
            ProjectItem item = GetDiagramProjectItem(current);

            if (!item.IsNull())
            {
                string contextNameSpace = Path.GetFileNameWithoutExtension(this.DocumentName);
                string body = AdjustGacPath(ReadResourceContent(@"Linx.EntityAdapterDesigner.Templates.LookUpsTemplate.txt"));
                body = body.Replace("#ContextFileName#", (item.Name == this.DocumentName ? "" : @"..\..\" + Path.GetFileName(GetProjectPath(diagramProject)) + @"\") + this.DocumentName);
                body = body.Replace("#ContextNamespace#", contextNameSpace);
                if (item.Name != this.DocumentName)
                    body = body.Replace("Includes\\DataEntityFunctions.ttinclude", "..\\Includes\\DataEntityFunctions.ttinclude");

                outputFile = Path.Combine(GetProjectPath(current), (item.Name == this.DocumentName ? "" : item.Name + "\\") + contextNameSpace + ".LookUps.tt");
                if (!this.VerifySourceControl(outputFile))
                    return;

                if (!ExistsProjectItem(item.ProjectItems, contextNameSpace + ".LookUps.tt"))
                {
                    File.WriteAllText(outputFile, body);
                    newItem = item.ProjectItems.AddFromFile(outputFile);
                    newItem.Properties.Item("CustomTool").Value = "TextTemplatingFileGenerator";
                }
                else
                {
                    if (File.ReadAllText(outputFile) != body)
                        File.WriteAllText(outputFile, body);
                    newItem = item.ProjectItems.Item(contextNameSpace + ".LookUps.tt");
                }
                //Run Template
                ((VSLangProj.VSProjectItem)newItem.Object).RunCustomTool();
            }
        }

        public void DeleteInconsistentFiles(Project current)
        {
            ProjectItem item = GetDiagramProjectItem(current);
            List<string> deletedList = new List<string>();

            if (!item.IsNull())
            {
                string contextNameSpace = Path.GetFileNameWithoutExtension(this.DocumentName);
                foreach (ProjectItem projectItem in item.ProjectItems)
                {
                    //Check all inconsistences with the design
                    if (!projectItem.Name.Contains(".CustomValidation.") && !projectItem.Name.Contains(".Operations.") && !projectItem.Name.Contains(".Events."))
                    {
                        if (projectItem.Name.Left((contextNameSpace + ".").Length) != (contextNameSpace + "."))
                            deletedList.Add(projectItem.Name);
                        else
                        {
                            //Check files without related entity
                            if (Path.GetExtension(projectItem.Name).ToLower() == ".cs" && projectItem.Name.Occurs(".") > 2)
                            {
                                var entityName = projectItem.Name.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries)[1];
                                if (!this.EntityAdapters.Any(e => e.Name == entityName))
                                    deletedList.Add(projectItem.Name);
                            }
                        }
                    }
                }
            }

            //Apply deleting
            foreach (string itemName in deletedList)
            {
                item.ProjectItems.Item(itemName).Delete();
            }
        }

        #endregion Templates

        #region Custom Operations Generation

        public void GenerateBusinessEvents(Project current, bool isShared)
        {
            string outputFile;
            ProjectItem item = GetDiagramProjectItem(current);
            Linx.Tools.CodeBuilder codeBuilder;

            if (!item.IsNull())
            {
                string contextNameSpace = Path.GetFileNameWithoutExtension(this.DocumentName);
                foreach (EntityAdapter entity in this.EntityAdapters)
                {
                    if (entity.EntityAdapterEvents.Count > 0)
                    {
                        outputFile = Path.Combine(GetProjectPath(current), (item.Name == this.DocumentName ? "" : item.Name + "\\") + contextNameSpace + "." + entity.Name + ".Events" + (isShared ? ".shared" : "") + ".cs");
                        if (!File.Exists(outputFile) || !ExistsProjectItem(item.ProjectItems, contextNameSpace + "." + entity.Name + ".Events" + (isShared ? ".shared" : "") + ".cs"))
                        {
                            if (!this.VerifySourceControl(outputFile))
                                return;

                            RemoveProjectItems(item.ProjectItems, contextNameSpace + "." + entity.Name + ".Events" + (isShared ? ".shared" : "") + ".cs");
                            codeBuilder = new Linx.Tools.CodeBuilder();

                            //Add Events
                            this.GenerateEntityEventsCode(codeBuilder, entity, contextNameSpace, isShared);
                            System.IO.File.WriteAllText(outputFile, codeBuilder.ToString());
                            //Add project item.
                            item.ProjectItems.AddFromFile(outputFile);
                        }
                    }
                }
            }
        }

        public string GetWebApiClassFile(WebApiController api, Project webApiProject = null, bool isAutomatic = false)
        {
            string outputFile = String.Empty;
            ProjectItem item = null;

            if (webApiProject == null)
                webApiProject = this.GetWebApiProject(api.ProjectSuffix);

            if (webApiProject != null)
                item = GetWebApiControllersItem(api, webApiProject); ;

            if (!item.IsNull())
                outputFile = Path.Combine(this.GetProjectPath(webApiProject), Path.GetFileNameWithoutExtension(item.Name) + "\\" + api.Name + (isAutomatic ? "AutoGen" : "") + ".cs");

            return outputFile;
        }

        public string GetWebApiAppStartClassFile(WebApiController api, Project webApiProject = null, string className = "")
        {
            string outputFile = String.Empty;
            ProjectItem item = null;

            if (webApiProject == null)
                webApiProject = this.GetWebApiProject(api.ProjectSuffix);

            if (webApiProject != null)
                item = GetWebApiAppStartItem(api, webApiProject); ;

            if (!item.IsNull())
                outputFile = Path.Combine(this.GetProjectPath(webApiProject), Path.GetFileNameWithoutExtension(item.Name) + "\\" + className + ".cs");

            return outputFile;
        }

        public ProjectItem GetWebApiControllersItem(WebApiController api, Project webApiProject = null)
        {
            string outputFile = String.Empty;
            ProjectItem item = null;

            if (webApiProject == null)
                webApiProject = this.GetWebApiProject(api.ProjectSuffix);

            if (webApiProject != null)
                item = this.GetProjectItemByName(webApiProject, "Controllers");

            return item;
        }

        private string _appStartFolder = "App_Start";
        public ProjectItem GetWebApiAppStartItem(WebApiController api, Project webApiProject = null)
        {
            string outputFile = String.Empty;
            ProjectItem item = null;

            if (webApiProject == null)
                webApiProject = this.GetWebApiProject(api.ProjectSuffix);

            if (webApiProject != null)
                item = this.GetProjectItemByName(webApiProject, _appStartFolder);

            return item;
        }

        public string[] GetUsedDomainNames()
        {
            List<string> result = new List<string>();

            foreach (var entity in this.EntityAdapters)
            {
                foreach (var attr in entity.GetAllInheritanceAttributes().Where(e => !e.DomainName.IsNullOrEmpty()))
                {
                    result.Add(attr.DomainName);
                }
            }

            return result.ToArray();
        }

        public List<string> GetUsedParameterNames()
        {
            List<string> result = new List<string>();
            foreach (var entity in this.EntityAdapters)
            {
                foreach (var prop in entity.GetAllInheritanceProperties().Where(e => !e.DefaultValue.IsNullOrEmpty() && e.DefaultValue.Contains("[") && e.DefaultValue.Contains("]")))
                {
                    result.Add(prop.DefaultValue.Extract("[", "]") + "{}");
                }
            }
            return result;
        }

        public bool CreateFile(string outputFilePath, ProjectItems projectItems, bool isEmbeddedResource = false)
        {
            string fileName = Path.GetFileName(outputFilePath);
            bool existsInProject = ExistsProjectItem(projectItems, fileName);

            if (existsInProject)
                this.VerifySourceControl(outputFilePath);

            if (!File.Exists(outputFilePath) || !existsInProject)
            {
                if (existsInProject)
                    RemoveProjectItems(projectItems, fileName);

                System.IO.File.WriteAllText(outputFilePath, "");

                var file = projectItems.AddFromFile(outputFilePath);

                if (isEmbeddedResource)
                    file.Properties.Item("ItemType").Value = "EmbeddedResource";
            }
            else
                System.IO.File.WriteAllText(outputFilePath, "");

            return true;
        }

        public bool WriteFile(string outputFilePath, Linx.Tools.BaseCodeBuilder codeBuilder, ProjectItems projectItems, bool isEmbeddedResource = false)
        {
            string fileName = Path.GetFileName(outputFilePath);
            bool existsInProject = ExistsProjectItem(projectItems, fileName);

            if (existsInProject)
                this.VerifySourceControl(outputFilePath);

            if (!File.Exists(outputFilePath) || !existsInProject)
            {
                if (existsInProject)
                    RemoveProjectItems(projectItems, fileName);

                System.IO.File.WriteAllText(outputFilePath, codeBuilder.ToString(), Encoding.UTF8);

                var file = projectItems.AddFromFile(outputFilePath);

                if (isEmbeddedResource)
                    file.Properties.Item("ItemType").Value = "EmbeddedResource";
            }
            else if (System.IO.File.ReadAllText(outputFilePath) != codeBuilder.ToString())
                System.IO.File.WriteAllText(outputFilePath, codeBuilder.ToString(), Encoding.UTF8);

            return true;
        }


        public string ToSeparatedStrList(string value)
        {
            if (!value.IsNullOrEmpty())
            {
                string[] list = value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string result = String.Empty;
                foreach (string part in list)
                {
                    result += (result.IsNullOrEmpty() ? String.Empty : ",") + "\"" + part.Trim() + "\"";
                }
                return result;
            }
            else
                return value;
        }

        public bool ExistsClientEntityEvent(string eventName)
        {
            foreach (var entity in this.EntityAdapters)
            {
                if (entity.ExistsClientEvent(eventName))
                    return true;
            }
            return false;
        }

        public string ToJsDataType(string dataType)
        {
            if (dataType.ToLower().Contains("string"))
                return "String";

            if (dataType.ToLower().Contains("long") || dataType.ToLower().Contains("int64"))
                return "Int64";

            if (dataType.ToLower().Contains("short") || dataType.ToLower().Contains("int16"))
                return "Int16";

            if (dataType.ToLower().Contains("int") || dataType.ToLower().Contains("int32"))
                return "Int32";

            if (dataType.ToLower().Contains("decimal"))
                return "Decimal";

            if (dataType.ToLower().Contains("double"))
                return "Double";

            if (dataType.ToLower().Contains("single"))
                return "Single";

            if (dataType.ToLower().Contains("datetime"))
                return "DateTime";
            else if (dataType.ToLower().Contains("time"))
                return "Time";

            if (dataType.ToLower().Contains("bool"))
                return "Boolean";

            if (dataType.ToLower().Contains("guid"))
                return "Guid";

            if (dataType.ToLower().Contains("byte"))
                return "Byte";

            if (dataType.ToLower().Contains("binary"))
                return "Binary";

            return "None";
        }

        public Guid GetProjectGuid(EnvDTE.Project project)
        {
            Guid projectGuid = Guid.Empty;

            Microsoft.VisualStudio.Shell.Interop.IVsHierarchy hierarchy;

            IServiceProvider serviceProvider = new Microsoft.VisualStudio.Shell.ServiceProvider(project.DTE as Microsoft.VisualStudio.OLE.Interop.IServiceProvider);

            Microsoft.VisualStudio.Shell.Interop.IVsSolution solution = serviceProvider.GetService(typeof(Microsoft.VisualStudio.Shell.Interop.SVsSolution)) as Microsoft.VisualStudio.Shell.Interop.IVsSolution;

            solution.GetProjectOfUniqueName(project.FullName, out hierarchy);

            if (hierarchy != null)
            {
                solution.GetGuidOfProject(hierarchy, out projectGuid);
            }

            return projectGuid;
        }

        public string GetWebApiProjectName(string projectSuffix, Project eadProject)
        {
            return eadProject.Name + ".WebAPI" + (projectSuffix.IsNullOrEmpty() ? String.Empty : "." + projectSuffix);
        }

        public string GetWebApiClientProjectName(string projectSuffix, Project eadProject)
        {
            return GetWebApiProjectName(projectSuffix, eadProject) + ".Client";
        }

        public Project GetWebApiProject(string projectSuffix, Project eadProject = null)
        {
            if (eadProject == null)
                eadProject = GetEadProject();
            if (eadProject != null)
                return this.GetProjectByName(GetWebApiProjectName(projectSuffix, eadProject));
            else
                return null;
        }

        public Project GetWebApiClientProject(WebApiController api, Project eadProject = null)
        {
            if (eadProject == null)
                eadProject = GetEadProject();
            if (eadProject != null)
                return this.GetProjectByName(GetWebApiClientProjectName(api.ProjectSuffix, eadProject));
            else
                return null;
        }

        private void GenerateWebApiAtomODataCode()
        {
            if (this.IsAspNetCore)
                return;

            //temp 
            string outputFile;
            Project eadProject = GetEadProject();

            if (eadProject != null)
            {
                Project webApiProject;
                ProjectItem item;
                Linx.Tools.CodeBuilder codeBuilder;

                foreach (var api in this.WebApiControllers.Where(c => c.IsDataService && c.SynchronizedWithDomainService))
                {
                    webApiProject = this.GetWebApiProject(api.ProjectSuffix, eadProject);

                    if (webApiProject != null)
                    {
                        item = GetWebApiAppStartItem(api, webApiProject);
                        if (!item.IsNull())
                        {
                            outputFile = Path.Combine(this.GetProjectPath(webApiProject), Path.GetFileNameWithoutExtension(item.Name) + "\\" + api.Name + "ODataStart" + ".cs");
                            codeBuilder = new Linx.Tools.CodeBuilder();
                            this.GenerateWebApiAutomaticODataControllerStartCode(codeBuilder, api, webApiProject, item);

                            //Henry - 22/02/2016
                            //Checkout automático no ODataStart.cs do WebApi
                            if (File.Exists(outputFile) && !this.VerifySourceControl(outputFile))
                                return;

                            if (!File.Exists(outputFile) || !ExistsProjectItem(item.ProjectItems, System.IO.Path.GetFileName(outputFile)))
                            {
                                RemoveProjectItems(item.ProjectItems, System.IO.Path.GetFileName(outputFile));
                                //Write code to file
                                System.IO.File.WriteAllText(outputFile, codeBuilder.GetBody());
                                //Add project item.
                                item.ProjectItems.AddFromFile(outputFile);
                            }
                            else
                            {
                                //Write code to file
                                System.IO.File.WriteAllText(outputFile, codeBuilder.GetBody());
                            }
                        }
                    }
                }
            }
        }



        public void GenerateWebApiControllersCode()
        {
            if (this.IsAspNetCore)
                return;

            string outputFile;
            Project eadProject = GetEadProject();

            if (eadProject != null)
            {
                Project webApiProject;
                ProjectItem item;
                Linx.Tools.CodeBuilder codeBuilder;

                foreach (var api in this.WebApiControllers)
                {
                    webApiProject = this.GetWebApiProject(api.ProjectSuffix, eadProject);
                    if (webApiProject != null)
                    {
                        item = this.GetWebApiControllersItem(api, webApiProject);
                        if (!item.IsNull())
                        {
                            api.DeleteWebApiAppStartCode(webApiProject);
                            api.DeleteInconsistentFiles(webApiProject);

                            //Automatic Controller
                            //Get automatic code
                            codeBuilder = new Linx.Tools.CodeBuilder();
                            this.GenerateWebApiAutomaticControllerCode(codeBuilder, api, webApiProject, eadProject);
                            outputFile = this.GetWebApiClassFile(api, webApiProject, true);
                            WriteFile(outputFile, codeBuilder, item.ProjectItems);

                            //Custom Controller
                            outputFile = this.GetWebApiClassFile(api, webApiProject);
                            if (!File.Exists(outputFile) || !ExistsProjectItem(item.ProjectItems, System.IO.Path.GetFileName(outputFile)))
                            {
                                if (!this.VerifySourceControl(outputFile))
                                    return;

                                RemoveProjectItems(item.ProjectItems, System.IO.Path.GetFileName(outputFile));
                                codeBuilder = new Linx.Tools.CodeBuilder();

                                //Add Events
                                this.GenerateWebApiControllerCode(codeBuilder, api, webApiProject, eadProject);
                                System.IO.File.WriteAllText(outputFile, codeBuilder.ToString());
                                //Add project item.
                                item.ProjectItems.AddFromFile(outputFile);
                            }

                        }
                    }
                }
            }
        }

        public void GenerateWebApiClientCode(Project eadProject)
        {
            string outputFile, fileName;

            Project webApiClientProject;
            ProjectItem item;
            Linx.Tools.CodeBuilder codeBuilder;

            List<string> models = new List<string>();
            foreach (var api in this.WebApiControllers.Where(e => e.EnableClient))
            {
                webApiClientProject = this.GetWebApiClientProject(api, eadProject);
                if (webApiClientProject != null)
                {
                    //Getting Model
                    if (!models.Contains(webApiClientProject.Name))
                    {
                        models.Add(webApiClientProject.Name);
                        item = item = this.GetProjectItemByName(webApiClientProject, "Model");
                        if (!item.IsNull())
                        {
                            fileName = Path.GetFileNameWithoutExtension(this.DocumentName) + ".cs";
                            outputFile = Path.Combine(this.GetProjectPath(webApiClientProject), Path.GetFileNameWithoutExtension(item.Name) + "\\" + fileName);

                            //Source control
                            if (!this.VerifySourceControl(outputFile))
                                return;

                            codeBuilder = new Linx.Tools.CodeBuilder();
                            this.GenerateWebApiClientModelCode(codeBuilder, api, webApiClientProject, eadProject);


                            WriteFile(outputFile, codeBuilder, item.ProjectItems);
                        }
                    }

                    //Generate Api Client
                    item = item = this.GetProjectItemByName(webApiClientProject, "Controllers");
                    if (!item.IsNull())
                    {
                        fileName = api.Name + ".cs";
                        outputFile = Path.Combine(this.GetProjectPath(webApiClientProject), Path.GetFileNameWithoutExtension(item.Name) + "\\" + fileName);

                        //Source control
                        if (!this.VerifySourceControl(outputFile))
                            return;

                        codeBuilder = new Linx.Tools.CodeBuilder();
                        this.GenerateWebApiClientCode(codeBuilder, api, webApiClientProject, eadProject, item.Name);

                        WriteFile(outputFile, codeBuilder, item.ProjectItems);
                    }
                }
            }

        }

        private void GenerateWebApiClientModelCode(Linx.Tools.CodeBuilder codeBuilder, WebApiController api, Project webApiProject, Project eadProject)
        {
            string baseIndent = "    ";
            codeBuilder.AddLine("using System;");
            codeBuilder.AddLine("using System.Collections.Generic;");
            codeBuilder.AddLine("using System.Linq;");
            codeBuilder.AddLine("using System.Text;");
            codeBuilder.AddLine("using Linx.Tools;");
            codeBuilder.AddLine("using System.ComponentModel.DataAnnotations;");
            codeBuilder.AddLine("using System.Runtime.Serialization;");
            codeBuilder.AddLine("using System.Xml.Serialization;");
            codeBuilder.AddLine();

            codeBuilder.AddLine("namespace " + eadProject.Name + "." + Path.GetFileNameWithoutExtension(this.DocumentName));
            codeBuilder.AddLine("{");

            codeBuilder.IncreaseIndent();
            foreach (var entity in this.EntityAdapters)
            {
                codeBuilder.AddLine();

                codeBuilder.AddLine("[" + (entity.IsCollectionDataContract ? "CollectionDataContract" : "DataContract") + "(IsReference = false, Name = \"" + (entity.DataContractName.IsNullOrEmpty() ? entity.Name : entity.DataContractName) + "\"" + (entity.DataContractNamespace == "." ? "" : ", Namespace=\"" + entity.DataContractNamespace + "\"") + ")]");
                codeBuilder.AddLine("[Serializable()]");
                codeBuilder.AddLine("public partial class " + entity.Name);
                codeBuilder.AddLine("{");
                codeBuilder.AddLine(entity.GetPropertyDefinitions("contextName", baseIndent + baseIndent, false, new List<string>(), true, true));
                codeBuilder.AddLine("}");
            }

            codeBuilder.AddLine();
            codeBuilder.DecreaseIndent();

            codeBuilder.AddLine("}");
        }

        private void GenerateWebApiClientCode(Linx.Tools.CodeBuilder codeBuilder, WebApiController api, Project webApiProject, Project eadProject, string folderName)
        {
            string baseIndent = "	", apiRoute = api.GetRoutePrefix();

            codeBuilder.AddLine("using System;");
            codeBuilder.AddLine("using System.Collections.Generic;");
            codeBuilder.AddLine("using System.Linq;");
            codeBuilder.AddLine("using System.Text;");
            codeBuilder.AddLine("using Linx.Tools;");
            codeBuilder.AddLine("using System.ComponentModel.DataAnnotations;");
            codeBuilder.AddLine("using System.Net.Http.Headers;");
            codeBuilder.AddLine("using System.Net.Http;");
            codeBuilder.AddLine("using " + eadProject.Name + "." + Path.GetFileNameWithoutExtension(this.DocumentName) + ";");
            codeBuilder.AddLine();

            codeBuilder.AddLine("namespace " + webApiProject.Name + "." + folderName);
            codeBuilder.AddLine("{");

            //Class Definition
            codeBuilder.AddLine();

            codeBuilder.AddLine(baseIndent + "public partial class " + api.Name + " : LinxClientApiController");
            codeBuilder.AddLine(baseIndent + "{");
            codeBuilder.AddLine();
            codeBuilder.AddLine(baseIndent + baseIndent + "public " + api.Name + "(string serviceBusAddress) : base(serviceBusAddress) {  }");

            string commandName, serviceCommand, serviceParam, paramName, dataType;
            foreach (var action in api.WebApiActions.Where(e => e.Access == OperationAccess.Public))
            {
                serviceParam = "";
                serviceCommand = (action.RouteActionName == "." ? action.Name : action.RouteActionName);
                switch (action.HttpVerb)
                {
                    case HttpRouteAttribute.GET:
                        commandName = "GetAsync";
                        break;
                    case HttpRouteAttribute.POST:
                        commandName = "PostAsJsonAsync";
                        break;
                    case HttpRouteAttribute.PUT:
                        commandName = "PutAsJsonAsync";
                        break;
                    case HttpRouteAttribute.DELETE:
                        commandName = "DeleteAsync";
                        break;
                    default:
                        commandName = string.Empty;
                        break;
                }

                if (!action.Parameters.IsNullOrEmpty())
                {
                    if (action.HttpVerb == HttpRouteAttribute.GET || action.HttpVerb == HttpRouteAttribute.DELETE) //Inline params
                    {
                        foreach (string param in action.Parameters.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            if (param.Contains("="))
                            {
                                paramName = param.Left("=").Trim().Right(" ");
                                dataType = param.Left("=").Trim().Left(" ");
                            }
                            else
                            {
                                paramName = param.Right(" ");
                                dataType = param.Left(" ");
                            }

                            serviceCommand += (serviceCommand.Contains("?") ? "&" : "?") + paramName + "=\" + (" + paramName + (dataType.ToLower().Contains("string") ? String.Empty : (dataType.ToLower().Contains("?") || dataType.ToLower().Contains("nullable<") ? " == null ? String.Empty : " + paramName + ".Value" : String.Empty) + ".ToString(" + (dataType.ToLower().Contains("datetime") ? "\"yyyy-MM-ddTHH:mm:ss\"" : (dataType.ToLower().Contains("guid") ? String.Empty : "System.Globalization.CultureInfo.InvariantCulture")) + ")") + ") + \"";
                        }
                    }
                    else //Body params
                    {
                        foreach (string param in action.Parameters.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            if (param.Contains("="))
                                paramName = param.Left("=").Trim().Right(" ");
                            else
                                paramName = param.Right(" ");

                            serviceParam = paramName;
                            break;
                        }
                    }
                }


                codeBuilder.AddLine();
                codeBuilder.AddLine(baseIndent + baseIndent + "public " + action.ReturnType + " " + action.Name + "(" + action.Parameters.Replace("#", ", ") + ")");
                codeBuilder.AddLine(baseIndent + baseIndent + "{");
                if (action.ReturnType != "void")
                    codeBuilder.AddLine(baseIndent + baseIndent + "      " + action.ReturnType + " result = default(" + action.ReturnType + ");");
                codeBuilder.AddLine(baseIndent + baseIndent + "      HttpResponseMessage response = _client." + commandName + "(\"" + apiRoute + "/" + serviceCommand + "\"" + (serviceParam.IsNullOrEmpty() ? String.Empty : ", " + serviceParam) + ").Result;");
                if (action.ReturnType != "void")
                {
                    codeBuilder.AddLine(baseIndent + baseIndent + "      if (response.IsSuccessStatusCode)");
                    codeBuilder.AddLine(baseIndent + baseIndent + "          result = response.Content.ReadAsAsync<" + action.ReturnType + ">().Result;");
                    codeBuilder.AddLine(baseIndent + baseIndent + "      else");
                    codeBuilder.AddLine(baseIndent + baseIndent + "      {");
                    codeBuilder.AddLine(baseIndent + baseIndent + "          var responseContent = response.Content.ReadAsStringAsync();");
                    codeBuilder.AddLine(baseIndent + baseIndent + "          responseContent.Wait();");
                    codeBuilder.AddLine(baseIndent + baseIndent + "          throw new Exception(WebClientHelper.GetResponseErrorMessage(responseContent.Result));");
                    codeBuilder.AddLine(baseIndent + baseIndent + "      }");
                }
                else
                {
                    codeBuilder.AddLine(baseIndent + baseIndent + "      if (!response.IsSuccessStatusCode) ");
                    codeBuilder.AddLine(baseIndent + baseIndent + "      {");
                    codeBuilder.AddLine(baseIndent + baseIndent + "          var responseContent = response.Content.ReadAsStringAsync();");
                    codeBuilder.AddLine(baseIndent + baseIndent + "          responseContent.Wait();");
                    codeBuilder.AddLine(baseIndent + baseIndent + "          throw new Exception(WebClientHelper.GetResponseErrorMessage(responseContent.Result));");
                    codeBuilder.AddLine(baseIndent + baseIndent + "      }");
                }
                if (action.ReturnType != "void")
                    codeBuilder.AddLine(baseIndent + baseIndent + "      return result;");
                codeBuilder.AddLine(baseIndent + baseIndent + "}");
            }

            codeBuilder.AddLine(baseIndent + "}");
            //End Class Definition

            //End Namespace
            codeBuilder.AddLine("}");
        }

        private void GenerateWebApiAttributeRoutingHttpCode(Linx.Tools.CodeBuilder codeBuilder, WebApiController api, Project webApiProject, Project eadProject)
        {
            var item = GetWebApiAppStartItem(api, webApiProject);

            codeBuilder.AddLine("using System.Reflection;");
            codeBuilder.AddLine("using System.Web.Http;");
            codeBuilder.AddLine("using System.Web.Routing;");
            codeBuilder.AddLine("using AttributeRouting.Web.Http.WebHost;");
            codeBuilder.AddLine("using System.Web;");

            codeBuilder.AddLine("");
            codeBuilder.AddLine("[assembly: PreApplicationStartMethod(typeof(" + webApiProject.Name + "." + item.Name + ".AttributeRoutingHttp), \"Start\")]");
            codeBuilder.AddLine("");

            codeBuilder.AddLine("namespace " + webApiProject.Name + "." + item.Name);
            codeBuilder.AddLine("{");

            //Class Definition
            codeBuilder.AddLine("");

            codeBuilder.IncreaseIndent();

            codeBuilder.AddLine("public static class AttributeRoutingHttp");
            codeBuilder.AddLine("{");
            codeBuilder.AddLine("    public static void RegisterRoutes(RouteCollection routes)");
            codeBuilder.AddLine("    {");
            codeBuilder.AddLine("        GlobalConfiguration.Configuration.Routes.MapHttpAttributeRoutes(config =>");
            codeBuilder.AddLine("        {");
            codeBuilder.AddLine("            config.AddRoutesFromAssembly(Assembly.GetExecutingAssembly());");
            codeBuilder.AddLine("            config.AutoGenerateRouteNames = true;");
            codeBuilder.AddLine("            config.UseLowercaseRoutes = true;");
            codeBuilder.AddLine("        });");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("");
            codeBuilder.AddLine("    public static void Start()");
            codeBuilder.AddLine("    {");
            codeBuilder.AddLine("        RegisterRoutes(RouteTable.Routes);");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("}");

            codeBuilder.DecreaseIndent();
            //End Class Definition

            //End Namespace
            codeBuilder.AddLine("}");
        }

        public ProjectItem GetRepositoryImplementationsItem(RepositoryImplementation repository, Project repositoryProject = null)
        {
            string outputFile = String.Empty;
            ProjectItem item = null;

            if (repositoryProject == null)
                repositoryProject = this.GetRepositoryProject(repository);

            if (repositoryProject != null)
                item = this.GetProjectItemByName(repositoryProject, "Implementations");

            return item;
        }

        public string GetRepositoryClassFile(RepositoryImplementation repository, Project repositoryProject = null)
        {
            string outputFile = String.Empty;
            ProjectItem item = null;

            if (repositoryProject == null)
                repositoryProject = this.GetRepositoryProject(repository);

            if (repositoryProject != null)
                item = GetRepositoryImplementationsItem(repository, repositoryProject); ;

            if (!item.IsNull())
                outputFile = Path.Combine(this.GetProjectPath(repositoryProject), Path.GetFileNameWithoutExtension(item.Name) + "\\" + repository.Name + ".cs");

            return outputFile;
        }

        public string GetRepositoryProjectName(RepositoryImplementation repository, Project eadProject)
        {
            return GetRepositoryBaseProjectName(repository.RepositoryInterface, eadProject) + (repository.ProjectSuffix.IsNullOrEmpty() ? String.Empty : "." + repository.ProjectSuffix);
        }

        public string GetRepositoryBaseProjectName(RepositoryInterface repository, Project eadProject)
        {
            return repository.ProjectName.Replace("[BO]", eadProject.Name);
        }

        public Project GetRepositoryProject(RepositoryImplementation repository, Project eadProject = null)
        {
            if (eadProject == null)
                eadProject = GetEadProject();

            if (eadProject != null)
                return this.GetProjectByName(GetRepositoryProjectName(repository, eadProject));
            else
                return null;
        }

        public string GetAddCustomChangesDef(EntityDataModel edm, string indent)
        {
            string body = String.Empty, instances, commands;

            foreach (EntityAdapter entity in this.EntityAdapters.Where(e => e.EntityAdapterRepresentation != null))
            {
                instances = entity.GetRepresentationDomainServiceInstances(indent + "  ", true);
                commands = entity.GetRepresentationDomainServiceCustomCommands(indent + "  ");
                if (!instances.IsNullOrEmpty() && !commands.IsNullOrEmpty())
                {
                    body += "\r\n " + indent + (body.IsNullOrEmpty() ? String.Empty : "else ") + "if (changedEntity is " + entity.Name + ")";
                    body += "\r\n " + indent + "{";
                    body += "\r\n " + indent + "  List<EntityChange> entityChanges = this.GetRepresentations((" + entity.Name + ")changedEntity, originalEntity as " + entity.Name + ", operation);";
                    body += instances;
                    body += commands;
                    body += "\r\n " + indent + "}";
                }

            }

            if (edm != null)
                body += "\r\n " + indent + (body.IsNullOrEmpty() ? String.Empty : "else ") + "changedEntity.ApplyChanges(this." + (edm.EdmInfo.IsDbContext ? "DbContext" : "ObjectContext") + ", originalEntity, operation, null);";

            return body;
        }

        public void GenerateRepositoriesCode()
        {
            string outputFile;
            Project eadProject = GetEadProject();
            ProjectItem item;

            if (eadProject != null)
            {
                Project repositoryProject;
                StringBuilder codeBuilder;

                foreach (var intf in this.RepositoryInterfaces)
                {
                    foreach (var repository in intf.RepositoryImplementations)
                    {
                        repositoryProject = this.GetRepositoryProject(repository, eadProject);
                        if (!repositoryProject.IsNull())
                        {
                            item = this.GetRepositoryImplementationsItem(repository, repositoryProject);
                            if (!item.IsNull())
                            {
                                outputFile = this.GetRepositoryClassFile(repository, repositoryProject);
                                if (!File.Exists(outputFile) || !ExistsProjectItem(item.ProjectItems, repository.Name + ".cs"))
                                {
                                    if (!this.VerifySourceControl(outputFile))
                                        return;

                                    RemoveProjectItems(item.ProjectItems, repository.Name + ".cs");
                                    codeBuilder = new StringBuilder();

                                    //Add Events
                                    this.GenerateRepositoryCode(codeBuilder, repository, repositoryProject);
                                    System.IO.File.WriteAllText(outputFile, codeBuilder.ToString());
                                    //Add project item.
                                    item.ProjectItems.AddFromFile(outputFile);
                                }
                            }

                        }
                    }
                }
            }
        }

        private ProjectItem GetWorkflowFolder(string folderName = "Workflow.Documents")
        {
            Project current = this.GetEadProject();

            if (!current.IsNull())
            {
                ProjectItem folder = this.GetProjectItemByName(current, folderName);
                if (folder == null)
                    folder = current.ProjectItems.AddFolder(folderName, Constants.vsProjectItemKindPhysicalFolder);

                if (folder != null)
                {
                    ProjectItem wfFolder = this.GetProjectItemByName(folder.ProjectItems, this.GetContextName());
                    if (wfFolder == null)
                        wfFolder = folder.ProjectItems.AddFolder(this.GetContextName(), Constants.vsProjectItemKindPhysicalFolder);

                    return wfFolder;
                }
            }

            return null;
        }

        public void GenerateActivities()
        {
            //Operations for Entity Adapters
            foreach (var entity in this.EntityAdapters)
            {
                foreach (var operation in entity.EntityAdapterOperations)
                {
                    if (operation.Workflow == null && operation.IsActivity)
                        CreateWorkflowActivity(operation);
                }
            }

            //Events for Entity Adapters
            foreach (var entity in this.EntityAdapters)
            {
                foreach (var operation in entity.EntityAdapterEvents)
                {
                    if (operation.Workflow == null && operation.IsActivity)
                        CreateWorkflowActivity(operation);
                }
            }

            //Generating for Service Extensions
            foreach (var ext in this.DomainServiceExtensions)
            {
                foreach (var operation in ext.DomainServiceOperations)
                {
                    if (operation.Workflow == null && operation.IsActivity)
                        CreateWorkflowActivity(operation);
                }
            }
        }

        public string GenerateWorkflowInvokers(string indent)
        {
            string body = String.Empty;

            foreach (Workflow wf in this.Workflows)
            {
                if (wf.GenericOperation != null && wf.GenericOperation is DomainServiceOperation)
                    body += "\r\n" + Linx.Builder.Resources.CodeGen.GetWorkflowInvoker(wf.Name, wf.GenericOperation.IsStatic, wf.GenericOperation.Parameters.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries), indent) + "\r\n";
            }

            return body;
        }

        public string GenerateKpiInformations(string indent, bool isNetCore = false)
        {
            string body = String.Empty;

            foreach (EntityAdapter entity in this.EntityAdapters)
            {
                foreach (EntityAdapterFormula formula in entity.EntityAdapterFormulas)
                {
                    if (!formula.Formula.IsNullOrEmpty() && !formula.KpiRelatedAttribute.IsNullOrEmpty())
                    {
                        body += "\r\n";
                        body += "\r\n" + indent + "[Invoke(HasSideEffects = true)]";
                        body += "\r\n" + indent + "public string Get" + formula.Name + "()";
                        body += "\r\n" + indent + "{";
                        if (!isNetCore)
                            body += "\r\n" + indent + "   Linx.Business.Tools.KpiManager.UpdateKpiInfo(" + entity.Name + ".Get" + formula.KpiRelatedAttribute + "KPI());";
                        body += "\r\n" + indent + "   KpiInfo info = new KpiInfo();";
                        body += "\r\n" + indent + "   info.CopyInstanceFrom(" + entity.Name + ".Get" + formula.KpiRelatedAttribute + "KPI());";
                        body += "\r\n" + indent + "   foreach (var element in " + entity.Name + ".Get" + formula.KpiRelatedAttribute + "KPI().Ranges)";
                        body += "\r\n" + indent + "   {";
                        body += "\r\n" + indent + "      KpiRangeItem item = new KpiRangeItem();";
                        body += "\r\n" + indent + "      item.CopyInstanceFrom(element.Value);";
                        body += "\r\n" + indent + "      info.Ranges.Add(element.Key, item);";
                        body += "\r\n" + indent + "   }";
                        body += "\r\n" + indent + "   return Linx.Tools.SerializationManager<KpiInfo>.ObjectToString(info);";
                        body += "\r\n" + indent + "}";
                    }
                }
            }


            return body;
        }

        public void GenerateWorkflows()
        {
            if (this.Workflows.Count > 0)
            {
                string outputFile, wfFileName;
                ProjectItem item = GetWorkflowFolder(), projItem;
                TextSelection textSelection;

                if (!item.IsNull())
                {
                    foreach (Workflow wf in this.Workflows)
                    {
                        //Create activity by operation
                        if (!wf.GenericOperation.IsNull())
                            CreateWorkflowActivity(wf.GenericOperation);

                        wfFileName = wf.Name + ".xaml";
                        outputFile = Path.Combine(item.FileNames[0], wfFileName);
                        if (!File.Exists(outputFile) || !ExistsProjectItem(item.ProjectItems, wfFileName))
                        {
                            if (!this.VerifySourceControl(outputFile))
                                return;

                            RemoveProjectItems(item.ProjectItems, wfFileName);

                            //Get parameters
                            Dictionary<string, string[]> operationInfo = null;
                            if (!wf.GenericOperation.IsNull())
                            {

                                if (wf.GenericOperation is EntityAdapterEvent)
                                    textSelection = this.OpenEntityEvent(wf.GenericOperation as EntityAdapterEvent);
                                else if (wf.GenericOperation is EntityAdapterOperation)
                                    textSelection = this.OpenEntityOperation(wf.GenericOperation as EntityAdapterOperation);
                                else if (wf.GenericOperation is DomainServiceOperation)
                                    textSelection = this.OpenDomainServiceOperation(wf.GenericOperation as DomainServiceOperation);
                                else
                                    textSelection = null;

                                if (!textSelection.IsNull())
                                {
                                    operationInfo = textSelection.GetOperationSignatureInfo(wf.GenericOperation);
                                    textSelection.Parent.Parent.ActiveWindow.Close();
                                }
                            }


                            string templateName = ((EnvDTE100.Solution4)item.DTE.Solution).GetProjectItemTemplate("Activity", "CSharp");
                            projItem = item.ProjectItems.AddFromTemplate(templateName, wfFileName);
                            item.DTE.ActiveDocument.Close();
                            string bodyFile = Linx.Builder.Resources.CodeGen.GetFlowchartActivity(this.GetContextNamespace(), wf.Name, (wf.GenericOperation == null ? String.Empty : wf.GenericOperation.GetParentName() + "_" + wf.GenericOperation.Name), outputFile, (this.EntityDataModels == null ? String.Empty : this.EntityDataModels.First().TargetNamespace), (!operationInfo.IsNull() && operationInfo.Count > 0 ? operationInfo.First().Value : new string[] { }), (!wf.GenericOperation.IsNull() && !wf.GenericOperation.IsStatic ? wf.GenericOperation.GetParentClassName() : ""));
                            File.WriteAllText(outputFile, bodyFile);
                        }

                    }
                }
            }
        }

        public void CreateWorkflowActivity(GenericOperation operation)
        {
            string outputFile, activityFileName, className;
            ProjectItem item = GetWorkflowFolder("Workflow.Activities"), projItem;

            if (!item.IsNull())
            {
                className = operation.GetParentName() + "_" + operation.Name;
                activityFileName = className + ".cs";
                outputFile = Path.Combine(item.FileNames[0], activityFileName);
                if (!File.Exists(outputFile) || !ExistsProjectItem(item.ProjectItems, activityFileName))
                {
                    if (!this.VerifySourceControl(outputFile))
                        return;

                    RemoveProjectItems(item.ProjectItems, activityFileName);

                    string bodyFile = Linx.Builder.Resources.CodeGen.GetActivity(this.GetContextNamespace(), className, operation.GetParentClassName(), operation.IsStatic, operation.Name, operation.Parameters.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries), operation.ReturnType);
                    File.WriteAllText(outputFile, bodyFile);
                    projItem = item.ProjectItems.AddFromFile(outputFile);

                    //Generating Activity Designer.
                    string[] bodies = Linx.Builder.Resources.CodeGen.GetActivityDesigner(this.GetContextNamespace(), className, operation.Parameters.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries), operation.ReturnType, operation.IsStatic);
                    outputFile = Path.Combine(item.FileNames[0], className + "Designer.xaml");
                    if (!this.VerifySourceControl(outputFile))
                        return;
                    File.WriteAllText(outputFile, bodies[0]);
                    ProjectItem pItem = item.ProjectItems.AddFromFile(outputFile);

                    outputFile = Path.Combine(item.FileNames[0], className + "Designer.xaml.cs");
                    if (!this.VerifySourceControl(outputFile))
                        return;

                    File.WriteAllText(outputFile, bodies[1]);
                    pItem.ProjectItems.AddFromFile(outputFile);

                }
            }
        }

        public void GenerateBusinessOperations(Project current, bool isShared)
        {
            string outputFile;
            ProjectItem item = GetDiagramProjectItem(current);
            StringBuilder codeBuilder;

            if (!item.IsNull())
            {
                string contextNameSpace = Path.GetFileNameWithoutExtension(this.DocumentName);
                foreach (EntityAdapter entity in this.EntityAdapters)
                {
                    if (entity.EntityAdapterOperations.Count > 0)
                    {
                        outputFile = Path.Combine(GetProjectPath(current), (item.Name == this.DocumentName ? "" : item.Name + "\\") + contextNameSpace + "." + entity.Name + ".Operations" + (isShared ? ".shared" : "") + ".cs");
                        if (!File.Exists(outputFile) || !ExistsProjectItem(item.ProjectItems, contextNameSpace + "." + entity.Name + ".Operations" + (isShared ? ".shared" : "") + ".cs"))
                        {
                            if (!this.VerifySourceControl(outputFile))
                                return;

                            RemoveProjectItems(item.ProjectItems, contextNameSpace + "." + entity.Name + ".Operations" + (isShared ? ".shared" : "") + ".cs");
                            codeBuilder = new StringBuilder();

                            //Add Events
                            this.GenerateEntityOperationsCode(codeBuilder, entity, contextNameSpace, isShared);
                            System.IO.File.WriteAllText(outputFile, codeBuilder.ToString());
                            //Add project item.
                            item.ProjectItems.AddFromFile(outputFile);
                        }
                    }
                }
            }
        }

        public void GenerateDomainServiceExtensions(Project current)
        {
            string outputFile = "";
            ProjectItem item = GetDiagramProjectItem(current);
            Linx.Tools.CodeBuilder codeBuilder;

            if (!item.IsNull())
            {
                string contextNameSpace = Path.GetFileNameWithoutExtension(this.DocumentName);
                foreach (DomainServiceExtension domainServiceExt in this.DomainServiceExtensions)
                {
                    if (domainServiceExt.DomainServiceOperations.Count > 0)
                    {
                        outputFile = Path.Combine(GetProjectPath(current), (item.Name == this.DocumentName ? "" : item.Name + "\\") + contextNameSpace + "." + domainServiceExt.Name + ".Operations.cs");
                        if (!File.Exists(outputFile) || !ExistsProjectItem(item.ProjectItems, contextNameSpace + "." + domainServiceExt.Name + ".Operations.cs"))
                        {
                            if (!this.VerifySourceControl(outputFile))
                                return;

                            RemoveProjectItems(item.ProjectItems, contextNameSpace + "." + domainServiceExt.Name + ".Operations.cs");
                            codeBuilder = new Linx.Tools.CodeBuilder();
                            //Create class definition
                            this.GenerateDomainServiceExtensionsCode(codeBuilder, domainServiceExt, contextNameSpace);
                            System.IO.File.WriteAllText(outputFile, codeBuilder.ToString());
                            //Add project item.
                            item.ProjectItems.AddFromFile(outputFile);
                        }
                    }
                }
            }
        }


        public string GetDomainViewCustomFile(DomainView domainView, bool fullPath = false, ProjectItem item = null)
        {
            if (item == null)
                item = GetProjectItemByName(this.GetEadProject(), "Domains");
            string fileName = domainView.Name + ".Custom.cs";
            if (fullPath)
                return Path.Combine(item.Properties.Item("FullPath").Value.ToString(), fileName);
            else
                return fileName;
        }

        public void GenerateDomainViewExtension(DomainView domainView, ProjectItem item)
        {
            string outputFile = "", fileName = "";
            Linx.Tools.CodeBuilder codeBuilder;
            if (item == null)
                item = GetProjectItemByName(this.GetEadProject(), "Domains");

            if (!item.IsNull())
            {
                fileName = GetDomainViewCustomFile(domainView, false, item);
                outputFile = GetDomainViewCustomFile(domainView, true, item);
                if (!File.Exists(outputFile) || !ExistsProjectItem(item.ProjectItems, fileName))
                {
                    if (!this.VerifySourceControl(outputFile))
                        return;

                    RemoveProjectItems(item.ProjectItems, fileName);
                    codeBuilder = new Linx.Tools.CodeBuilder();
                    //Create class definition
                    this.GenerateDomainViewCustomCode(codeBuilder, domainView, Path.GetFileNameWithoutExtension(item.Name));
                    System.IO.File.WriteAllText(outputFile, codeBuilder.ToString());
                    //Add project item.
                    item.ProjectItems.AddFromFile(outputFile);
                }
            }
        }

        private void GenerateDomainViewCustomCode(Linx.Tools.CodeBuilder codeBuilder, DomainView domainView, string contextName)
        {
            string baseIndent = "	";

            codeBuilder.AddLine("using System;");
            codeBuilder.AddLine("using System.IO;");
            codeBuilder.AddLine("using System.Collections.Generic;");
            codeBuilder.AddLine("using Linx.Tools;");
            codeBuilder.AddLine("using " + this.TargetNamespace + ";");
            codeBuilder.AddLine();
            codeBuilder.AddLine("namespace " + this.TargetNamespace + ".Domains");
            codeBuilder.AddLine("{");


            //Class Definition
            codeBuilder.AddLine(baseIndent + "");
            codeBuilder.AddLine(baseIndent + "////////////////////////////////////////////////////////////////////////////");
            codeBuilder.AddLine(baseIndent + "////////////////////////// Domain Service Extension ////////////////////////");
            codeBuilder.AddLine(baseIndent + "////////////////////////////////////////////////////////////////////////////");
            codeBuilder.AddLine(baseIndent + "public partial class " + domainView.Name);
            codeBuilder.AddLine(baseIndent + "{");
            codeBuilder.AddLine(baseIndent + "}");
            //End Class Definition


            //End Namespace
            codeBuilder.AddLine("}");
        }



        private void GenerateDomainServiceExtensionsCode(Linx.Tools.CodeBuilder codeBuilder, DomainServiceExtension domainService, string contextName)
        {
            string baseIndent = "	";

            codeBuilder.AddLine("using System;");
            codeBuilder.AddLine("using System.Collections;");
            codeBuilder.AddLine("using System.Collections.Generic;");
            codeBuilder.AddLine("using System.Linq;");
            codeBuilder.AddLine("using System.Text;");
            codeBuilder.AddLine("using Linx.Data;");
            codeBuilder.AddLine("using Linx.Tools;");
            codeBuilder.AddLine("using System.Data.Entity.Core.Objects;");
            codeBuilder.AddLine("using System.ComponentModel;");
            codeBuilder.AddLine("using System.Data.Common;");
            codeBuilder.AddLine("using System.Runtime.Serialization;");
            codeBuilder.AddLine("using System.ServiceModel;");
            codeBuilder.AddLine("using System.ComponentModel.DataAnnotations;");
            codeBuilder.AddLine("using System.ServiceModel.DomainServices.Server;");
            codeBuilder.AddLine("using System.ServiceModel.DomainServices.Hosting;");
            codeBuilder.AddLine("using System.ServiceModel.DomainServices;");
            codeBuilder.AddLine("using Linx;");


            //Add Namespace from Edms
            foreach (EntityDataModel edm in domainService.EntityAdapterDesignerRoot.EntityDataModels)
                codeBuilder.AddLine("using " + edm.TargetNamespace + ";");


            codeBuilder.AddLine("");
            codeBuilder.AddLine("namespace " + domainService.EntityAdapterDesignerRoot.TargetNamespace + "." + contextName);
            codeBuilder.AddLine("{");

            //Class Interface
            codeBuilder.AddLine(baseIndent + "");
            codeBuilder.AddLine(baseIndent + "////////////////////////////////////////////////////////////////////////////");
            codeBuilder.AddLine(baseIndent + "////////////////////////// Domain Service Extension ////////////////////////");
            codeBuilder.AddLine(baseIndent + "////////////////////////////////////////////////////////////////////////////");
            codeBuilder.AddLine(baseIndent + "public partial class " + contextName + "DomainService");
            codeBuilder.AddLine(baseIndent + "{");
            codeBuilder.AddLine(baseIndent + "}");
            //End Class Definition


            //End Namespace
            codeBuilder.AddLine("}");
        }

        private void GenerateEntityEventsCode(Linx.Tools.CodeBuilder codeBuilder, EntityAdapter entity, string contextName, bool isShared)
        {
            EntityDataModel edm = entity.GetCurrentDataModel();

            codeBuilder.AddLine("using System;");
            codeBuilder.AddLine("using System.Collections;");
            codeBuilder.AddLine("using System.Collections.Generic;");
            codeBuilder.AddLine("using System.Linq.Expressions;");
            codeBuilder.AddLine("using Linx;");
            codeBuilder.AddLine("using Linx.Tools;");
            codeBuilder.AddLine("using System.Linq;");
            codeBuilder.AddLine("using System.ComponentModel;");
            codeBuilder.AddLine("using System.ComponentModel.DataAnnotations;");
            codeBuilder.AddLine("using Linx.Data;");

            if (isShared)
            {
                codeBuilder.AddLine("using System.Xml.Serialization;");
            }
            else
            {
                codeBuilder.AddLine("using System.Text;");
                codeBuilder.AddLine("using System.Data.Entity.Core.Objects;");
                codeBuilder.AddLine("using System.Data.Common;");
                codeBuilder.AddLine("using System.Runtime.Serialization;");
                codeBuilder.AddLine("using System.Reflection;");
                if (edm != null)
                    codeBuilder.AddLine("using " + edm.TargetNamespace + ";");
            }

            codeBuilder.AddLine("");
            codeBuilder.AddLine("namespace " + entity.EntityAdapterDesignerRoot.TargetNamespace + "." + contextName);
            codeBuilder.AddLine("{");

            codeBuilder.IncreaseIndent();

            //Class Definition
            codeBuilder.AddLine("");
            codeBuilder.AddLine("////////////////////////////////////////////////////////////////////////////");
            codeBuilder.AddLine("////////////////////////// Business Events Definition //////////////////////");
            codeBuilder.AddLine("////////////////////////////////////////////////////////////////////////////");
            codeBuilder.AddLine("public partial class " + entity.Name);
            codeBuilder.AddLine("{");
            codeBuilder.AddLine("}");
            //End Class Definition

            codeBuilder.DecreaseIndent();

            //End Namespace
            codeBuilder.AddLine("}");
        }

        private void GenerateWebApiControllerCode(Linx.Tools.CodeBuilder codeBuilder, WebApiController api, Project webApiProject, Project eadProject, bool isNetCore = false)
        {
            EntityDataModel edm = api.EntityAdapterDesignerRoot.GetEdm();
            string apiName = api.Name;

            codeBuilder.AddLine("using System;");
            codeBuilder.AddLine("using System.Collections;");
            codeBuilder.AddLine("using System.Collections.Generic;");
            codeBuilder.AddLine("using System.Linq.Expressions;");
            codeBuilder.AddLine("using Linx.Tools;");
            codeBuilder.AddLine("using System.Linq;");
            codeBuilder.AddLine("using System.ComponentModel;");
            codeBuilder.AddLine("using System.ComponentModel.DataAnnotations;");
            codeBuilder.AddLine("using System.Net;");
            codeBuilder.AddLine("using System.Net.Http;");


            if (isNetCore)
            {
                codeBuilder.AddLine("using Microsoft.AspNetCore.OData;");
                codeBuilder.AddLine("using Microsoft.AspNetCore.OData.Extensions;");
                codeBuilder.AddLine("using Microsoft.AspNetCore.Mvc;");
            }
            else
            {
                codeBuilder.AddLine("using System.ComponentModel.Composition;");
                codeBuilder.AddLine("using System.Web.Http;");
            }

            codeBuilder.AddLine("using " + api.EntityAdapterDesignerRoot.GetDirectContextNamespace() + ";");


            var item = GetWebApiControllersItem(api, webApiProject);

            codeBuilder.AddLine("");
            codeBuilder.AddLine("namespace " + webApiProject.Name + "." + item.Name);
            codeBuilder.AddLine("{");

            codeBuilder.IncreaseIndent();

            //Class Definition
            codeBuilder.AddLine("");
            codeBuilder.AddLine("////////////////////////////////////////////////////////////////////////////");
            codeBuilder.AddLine("/////////////////////////// Business Api Controller ////////////////////////");
            codeBuilder.AddLine("////////////////////////////////////////////////////////////////////////////");
            codeBuilder.AddLine("public partial class " + apiName + "Controller");
            codeBuilder.AddLine("{");

            codeBuilder.AddLine("}");
            //End Class Definition

            codeBuilder.DecreaseIndent();

            //End Namespace
            codeBuilder.AddLine("}");
        }

        private void GenerateWebApiAutomaticODataControllerStartCode(Linx.Tools.CodeBuilder codeBuilder, WebApiController api, Project webApiProject, ProjectItem itemAppStart)
        {
            var item = GetWebApiAppStartItem(api, webApiProject);

            codeBuilder.AddLine("using Microsoft.Data.Edm;");
            codeBuilder.AddLine("using Newtonsoft.Json.Serialization;");
            codeBuilder.AddLine("using System.Linq;");
            codeBuilder.AddLine("using System.Net.Http;");
            codeBuilder.AddLine("using System.Reflection;");
            codeBuilder.AddLine("using System.Web;");
            codeBuilder.AddLine("using System.Web.Http;");
            codeBuilder.AddLine("using System.Web.Http.Controllers;");
            codeBuilder.AddLine("using System.Web.Http.OData.Builder;");
            codeBuilder.AddLine("using System.Web.Http.OData.Extensions;");
            codeBuilder.AddLine("using System.Web.Http.OData.Routing;");
            codeBuilder.AddLine("using System.Web.Http.OData.Routing.Conventions;");
            codeBuilder.AddLine("using System.Web.Routing;");


            if (!(this.EntityAdapters.Count == 0 && this.DomainServiceExtensions.Count == 0))
                codeBuilder.AddLine("using BusinessNS = " + api.EntityAdapterDesignerRoot.GetDirectContextNamespace() + ";");

            codeBuilder.AddLine();
            codeBuilder.AddLine("[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(" + webApiProject.Name + "." + item.Name + "." + api.Name + "ODataStart), \"Start\")]");
            codeBuilder.AddLine("");

            codeBuilder.AddLine("namespace " + webApiProject.Name + "." + item.Name);
            codeBuilder.AddLine("{");

            //Class Definition
            codeBuilder.AddLine();

            codeBuilder.IncreaseIndent();

            codeBuilder.AddLine("public static class " + api.Name + "ODataStart");
            codeBuilder.AddLine("{");
            codeBuilder.IncreaseIndent();
            codeBuilder.AddLine("public static void Start()");
            codeBuilder.AddLine("{");

            codeBuilder.AddLine("   var conventions = ODataRoutingConventions.CreateDefault();");
            codeBuilder.AddLine("   conventions.Insert(0, new Linx.DataService.ODataContainmentRoutingConvention(\"" + api.Name + "Feed\"));");
            codeBuilder.AddLine("   GlobalConfiguration.Configuration.Routes.MapODataServiceRoute(");
            codeBuilder.AddLine("       routeName: \"" + api.Name + "ODataRoute\",");
            codeBuilder.AddLine("       routePrefix: \"" + api.GetRoutePrefix() + "OData\",");
            codeBuilder.AddLine("       model: GetEdmModel(), pathHandler: new DefaultODataPathHandler(), routingConventions: conventions");
            codeBuilder.AddLine("       );");
            codeBuilder.AddLine("}");
            codeBuilder.AddLine();
            codeBuilder.AddLine("private static IEdmModel GetEdmModel()");
            codeBuilder.AddLine("{");
            codeBuilder.AddLine("    ODataConventionModelBuilder modelBuilder = new ODataConventionModelBuilder();");

            foreach (var entityAdapter in this.EntityAdapters.Where(e => e.ExposeAsService))
            {
                codeBuilder.AddLine("    modelBuilder.EntitySet<BusinessNS.{0}>(\"{0}\");", entityAdapter.Name);
                if (entityAdapter.TargetEntityAdapter != null && entityAdapter.IsParentCompositionAllowed())
                    codeBuilder.AddLine("    modelBuilder.EntitySet<BusinessNS.{0}>(\"{0}\");", entityAdapter.Name + "ParentComposition");
            }

            codeBuilder.AddLine("    return modelBuilder.GetEdmModel();");
            codeBuilder.AddLine("}");


            codeBuilder.DecreaseIndent();
            codeBuilder.AddLine("}");

            codeBuilder.DecreaseIndent();
            //End Class Definition

            //End Namespace
            codeBuilder.AddLine("}");
        }

        private void GenerateWebApiAutomaticControllerCode(Linx.Tools.CodeBuilder codeBuilder, WebApiController api, Project webApiProject, Project eadProject, bool isNetCore = false)
        {
            EntityDataModel edm = api.EntityAdapterDesignerRoot.GetEdm();
            string apiName = api.Name;

            codeBuilder.AddLine("using Linx.Data;");
            codeBuilder.AddLine("using Linx.LinqExtensions.Dynamic;");
            codeBuilder.AddLine("using Linx.Tools;");
            codeBuilder.AddLine("using Newtonsoft.Json.Linq;");
            codeBuilder.AddLine("using System;");
            codeBuilder.AddLine("using System.Collections;");
            codeBuilder.AddLine("using System.Collections.Generic;");
            codeBuilder.AddLine("using System.ComponentModel;");
            codeBuilder.AddLine("using System.ComponentModel.DataAnnotations;");
            codeBuilder.AddLine("using System.Linq;");
            codeBuilder.AddLine("using System.Linq.Expressions;");
            codeBuilder.AddLine("using System.Net;");
            codeBuilder.AddLine("using System.Net.Http;");


            if (api.IsDataService)
                codeBuilder.AddLine("using Linx.DataService;");

            if (isNetCore)
            {
                codeBuilder.AddLine("using Microsoft.AspNetCore.Mvc;");
                codeBuilder.AddLine("using Microsoft.Extensions.Configuration;");
                codeBuilder.AddLine("using System.Reflection;");
                codeBuilder.AddLine("using Linx.DS.Core.Data;");
                codeBuilder.AddLine("using System.Linq.Dynamic.Core;");
                if (api.IsDataService)
                {
                    codeBuilder.AddLine("using Microsoft.AspNetCore.OData;");
                    codeBuilder.AddLine("using Microsoft.AspNetCore.OData.Extensions;");
                }
            }
            else
            {
                codeBuilder.AddLine("using System.ServiceModel.DomainServices.Server;");
                codeBuilder.AddLine("using Linx.Business.Tools;");
                codeBuilder.AddLine("using System.ComponentModel.Composition;");
                codeBuilder.AddLine("using System.Web.Http;");

                if (api.IsDataService)
                {
                    codeBuilder.AddLine("using Breeze.ContextProvider;");
                    codeBuilder.AddLine("using Breeze.WebApi2;");
                    codeBuilder.AddLine("using System.Web.Http.OData;");
                }
            }
            if (!(this.EntityAdapters.Count == 0 && this.DomainServiceExtensions.Count == 0))
                codeBuilder.AddLine("using BusinessNS = " + api.EntityAdapterDesignerRoot.GetDirectContextNamespace() + ";");

            var item = GetWebApiControllersItem(api, webApiProject);

            codeBuilder.AddLine("");
            codeBuilder.AddLine("namespace " + webApiProject.Name + "." + item.Name);
            codeBuilder.AddLine("{");

            codeBuilder.IncreaseIndent();

            //Class Definition   
            string port = "1710";
            codeBuilder.AddLine();
            codeBuilder.AddLine("//Examples:");
            codeBuilder.AddLine("// Default Call: http://localhost:" + port + "/" + api.GetRoutePrefix() + "/[ActionName]");
            codeBuilder.AddLine("// Security Information Call: http://localhost:" + port + "/" + api.GetRoutePrefix() + "/GetSecurityInfo");
            codeBuilder.AddLine("// Entities Catalog Call: http://localhost:" + port + "/" + api.GetRoutePrefix() + "/GetEntities");
            codeBuilder.AddLine("// Entity MetaData Call: http://localhost:" + port + "/" + api.GetRoutePrefix() + "/GetMetaData?entityName=[EntityName]&allComposition=false");
            codeBuilder.AddLine("// Client Domains Call: http://localhost:" + port + "/" + api.GetRoutePrefix() + "/GetClientDomains?erp=true");
            codeBuilder.AddLine("// Client Service Call: http://localhost:" + port + "/" + api.GetRoutePrefix() + "/GetClientService?erp=true");
            codeBuilder.AddLine("// Client Factory Call: http://localhost:" + port + "/" + api.GetRoutePrefix() + "/GetClientFactory?entityName=[EntityName]&erp=true");
            codeBuilder.AddLine("// Client Factory Custom Events Call: http://localhost:" + port + "/" + api.GetRoutePrefix() + "/GetClientFactoryCustomEvents?entityName=[EntityName]&erp=true");
            
            if (isNetCore)
            {
                codeBuilder.AddLine("// Help Call: http://localhost:" + port + "/swagger/ui/index.html?filter=" + api.GetRoutePrefix());
                if (api.IsDataService)
                {
                    codeBuilder.AddLine("// Feed OData Call: http://localhost:" + port + "/" + api.GetRoutePrefix() + "/$metadata");                    
                    codeBuilder.AddLine("[EnableQuery()]");
                    codeBuilder.AddLine("[Produces(\"application/json\")]");
    }
                codeBuilder.AddLine("[Route(\"" + api.GetRoutePrefix() + "\")]");
            }
            else
            {
                codeBuilder.AddLine("// Help Call: http://localhost:" + port + "/HelpController/" + api.Name);
                if (api.IsDataService)
                    codeBuilder.AddLine("// Feed OData Call: http://localhost:" + port + "/" + api.GetRoutePrefix() + "OData");
                codeBuilder.AddLine("[RoutePrefix(\"" + api.GetRoutePrefix() + "\")]");
                if (api.IsDataService)
                    codeBuilder.AddLine("[Breeze.WebApi2.BreezeController]");
            }

            if (this.EnableAutomaticAuthorization && !isNetCore)
                codeBuilder.AddLine("[ODataBasicAuthenticationFilter]");
            string repositoryBaseRef = String.Empty;


            if (api.RepositoryInterface != null)
            {
                codeBuilder.AddLine("public partial class " + apiName + "Controller : LinxApiController<BusinessNS." + api.RepositoryInterface.Name + ">");
                codeBuilder.AddLine("{");

                var defaultImpl = api.RepositoryInterface.RepositoryImplementations.Where(e => e.IsDefault).FirstOrDefault();
                string repositoryName = defaultImpl == null ? String.Empty : (defaultImpl.RepositoryName.IsNullOrEmpty() ? defaultImpl.Name : defaultImpl.RepositoryName);
                repositoryBaseRef = " : base(\"" + this.GetRepositoryBaseProjectName(api.RepositoryInterface, eadProject) + "\", \"" + repositoryName + "\")";

                if (!api.SynchronizedWithDomainService)
                {
                    codeBuilder.IncreaseIndent();
                    codeBuilder.AddLine("public " + apiName + "Controller()" + repositoryBaseRef + " { }");
                    codeBuilder.DecreaseIndent();
                }
            }
            else
            {
                codeBuilder.AddLine("public partial class " + apiName + "Controller : " + (isNetCore ? "Controller" : "ApiController"));
                codeBuilder.AddLine("{");
            }

            if (api.SynchronizedWithDomainService)
            {
                //Domain Service reference

                string types = String.Empty;
                foreach (string entityName in this.EntityAdapters.Where(e => e.ExposeAsService).Select(e => e.Name).OrderBy(e => e))
                {
                    types += (types.IsNullOrEmpty() ? String.Empty : ", ") + "typeof(BusinessNS." + entityName + ")";
                }

                string dsName = GetDomainServiceName();
                codeBuilder.IncreaseIndent();
                codeBuilder.AddLine("private const int maxObjectExcelReturned = 2300000;");
                codeBuilder.AddLine("private DataServiceRepository<BusinessNS." + dsName + "> _repository = null;");
                codeBuilder.AddLine("private DataServiceRepository<BusinessNS." + dsName + "> repository { get {  if (_repository == null) { _repository = new DataServiceRepository<BusinessNS." + dsName + ">(" + (isNetCore ? "new BusinessNS." + dsName + "() { ServiceProvider = App_Start.ModuleInitializer_" + api.Name + ".ServiceProvider }" + (types.IsNullOrEmpty() ? "" : ", ") : "") + types + "); _repository.Context.IsSecure = true; } return _repository; } }");


                if (isNetCore)
                {
                    if (api.IsDataService)
                        codeBuilder.AddLine("private string _routePrefix = \"" + api.GetRoutePrefix() + "\";");
                    codeBuilder.AddLine("private IConfiguration _config;");
                    codeBuilder.AddLine("public " + apiName + "Controller(IConfiguration config)");
                    codeBuilder.AddLine("{");
                    codeBuilder.AddLine("   _config = config;");
                    codeBuilder.AddLine("}");
                }
                else
                {
                    codeBuilder.AddLine("public " + apiName + "Controller()" + repositoryBaseRef);
                    codeBuilder.AddLine("{ }");
                }

                codeBuilder.AddLine();
                if (isNetCore)
                    codeBuilder.AddLine("[HttpGet(\"AssemblyInfo\")]");
                else
                    codeBuilder.AddLine("[Route(\"AssemblyInfo\"), System.Web.Http.HttpGet()]");
                codeBuilder.AddLine("public object AssemblyInfo()");
                codeBuilder.AddLine("{");
                codeBuilder.AddLine("    return new");
                codeBuilder.AddLine("    {");
                codeBuilder.AddLine("        ApiAssemblyName = typeof(" + apiName + "Controller)" + (isNetCore ? ".GetTypeInfo()" : "") + ".Assembly.FullName,");
                codeBuilder.AddLine("        BusinessAssemblyName = typeof(BusinessNS." + dsName + ")" + (isNetCore ? ".GetTypeInfo()" : "") + ".Assembly.FullName,");
                codeBuilder.AddLine("        ModelAssemblyName = " + (this.GetEdm() == null ? "\"\"" : "repository.Context.GetModelAssemblyName()"));
                codeBuilder.AddLine("    };");
                codeBuilder.AddLine("}");

                codeBuilder.AddLine();
                if (isNetCore)
                    codeBuilder.AddLine("[HttpGet(\"GetClientDomains\")]");
                else
                    codeBuilder.AddLine("[Route(\"GetClientDomains\"), System.Web.Http.HttpGet()]");
                codeBuilder.AddLine("public string[] GetClientDomains(bool erp = false)");
                codeBuilder.AddLine("{");
                codeBuilder.AddLine("    var result = repository.Context.GetClientDomains(erp);");
                codeBuilder.AddLine("    return result;");
                codeBuilder.AddLine("}");

                codeBuilder.AddLine();
                if (isNetCore)
                    codeBuilder.AddLine("[HttpGet(\"GetClientService\")]");
                else
                    codeBuilder.AddLine("[Route(\"GetClientService\"), System.Web.Http.HttpGet()]");
                codeBuilder.AddLine("public string[] GetClientService(bool erp = false)");
                codeBuilder.AddLine("{");
                codeBuilder.AddLine("    var result = repository.Context.GetClientService(erp);");
                codeBuilder.AddLine("    return result;");
                codeBuilder.AddLine("}");

                codeBuilder.AddLine();
                if (isNetCore)
                    codeBuilder.AddLine("[HttpGet(\"GetClientFactory\")]");
                else
                    codeBuilder.AddLine("[Route(\"GetClientFactory\"), System.Web.Http.HttpGet()]");
                codeBuilder.AddLine("public string[] GetClientFactory(string entityName, bool erp = false)");
                codeBuilder.AddLine("{");
                codeBuilder.AddLine("    var result = repository.Context.GetClientFactory(entityName, erp);");
                codeBuilder.AddLine("    return result;");
                codeBuilder.AddLine("}");

                codeBuilder.AddLine();
                if (isNetCore)
                    codeBuilder.AddLine("[HttpGet(\"GetClientFactoryCustomEvents\")]");
                else
                    codeBuilder.AddLine("[Route(\"GetClientFactoryCustomEvents\"), System.Web.Http.HttpGet()]");
                codeBuilder.AddLine("public string[] GetClientFactoryCustomEvents(string entityName, bool erp = false)");
                codeBuilder.AddLine("{");
                codeBuilder.AddLine("    var result = repository.Context.GetClientFactoryCustomEvents(entityName, erp);");
                codeBuilder.AddLine("    return result;");
                codeBuilder.AddLine("}");

                codeBuilder.AddLine();
                if (isNetCore)
                    codeBuilder.AddLine("[HttpGet(\"GetMetaData\")]");
                else
                    codeBuilder.AddLine("[Route(\"GetMetaData\"), System.Web.Http.HttpGet()]");
                codeBuilder.AddLine("public List<LinxEntityReferenceInfo> GetMetaData(string entityName = \"\", bool allComposition = false)");
                codeBuilder.AddLine("{");
                codeBuilder.AddLine("    var result = repository.Context.GetMetaDataObject(\"" + api.EntityAdapterDesignerRoot.GetDirectContextNamespace() + ".\" + entityName, false, true);");
                codeBuilder.AddLine("    return (allComposition ? result : result.Where(e => e.ClassName == entityName).ToList());");
                codeBuilder.AddLine("}");
                codeBuilder.AddLine();
                if (isNetCore)
                    codeBuilder.AddLine("[HttpGet(\"GetSecurityInfo\")]");
                else
                    codeBuilder.AddLine("[Route(\"GetSecurityInfo\"), System.Web.Http.HttpGet()]");
                codeBuilder.AddLine("public string[] GetSecurityInfo()");
                codeBuilder.AddLine("{");
                codeBuilder.AddLine("   return new string[] { \"" + api.EntityAdapterDesignerRoot.TargetNamespace + "\", \"" + api.GetRoutePrefix() + "\", \"" + api.GetRoutePrefix() + "/ActionName\" };");
                codeBuilder.AddLine("}");
                codeBuilder.AddLine();
                if (isNetCore)
                    codeBuilder.AddLine("[HttpGet(\"GetEntities\")]");
                else
                    codeBuilder.AddLine("[Route(\"GetEntities\"), System.Web.Http.HttpGet()]");
                codeBuilder.AddLine("public object[] GetEntities()");
                codeBuilder.AddLine("{");


                var parents = this.EntityAdapters.Where(e => e.TargetEntityAdapter == null && e.ClientLocalServices.Count > 0).ToList();
                if (parents.Count > 0)
                {
                    codeBuilder.AddLine("    return new object[] { ");
                    Action<EntityAdapter, bool, string, int> createMeta = null;
                    createMeta = (entity, isDetail, indent, index) =>
                    {
                        codeBuilder.AddLine(indent + "                       " + (index == 0 ? "" : ", ") + "new {");
                        codeBuilder.AddLine(indent + "                           Name = \"" + entity.Name + "\", ListName = \"" + (!isDetail ? "" : entity.Name + "List") + "\"");
                        codeBuilder.AddLine(indent + "                           , Details = new object[] { ");

                        var details = entity.SourceEntityAdapters.ToList();
                        details.ForEach(e => createMeta(e, true, indent + "    ", details.IndexOf(e)));

                        codeBuilder.AddLine(indent + "                           }");
                        codeBuilder.AddLine(indent + "                       }");
                    };
                    parents.ForEach(e => createMeta(e, false, "", parents.IndexOf(e)));
                    codeBuilder.AddLine("    };");
                }
                else
                {
                    codeBuilder.AddLine("    throw new Exception(\"Não há 'LocalServices' para este serviço.\");");
                }


                codeBuilder.AddLine("}");

                //Generate domain service code
                this.GenerateDomainServiceActionsToController(api, codeBuilder, "repository", "repository.Context", (this.EnableAutomaticAuthorization && !isNetCore ? apiName + "ControllerAuthorize" : ""), isNetCore, api.IsDataService, webApiProject);

                codeBuilder.DecreaseIndent();
            }

            codeBuilder.AddLine("}");
            //End Class Definition

            //Generate Feed controller 
            if (api.SynchronizedWithDomainService && api.IsDataService && !isNetCore)
                this.GenerateFeedControllerCode(codeBuilder, apiName, (this.EnableAutomaticAuthorization ? apiName + "ControllerAuthorize" : ""));

            if (!isNetCore)
            {
                //Authorization Definition
                codeBuilder.AddLine("public partial class " + apiName + "ControllerAuthorizeAttribute : System.Web.Http.AuthorizeAttribute");
                codeBuilder.AddLine("{");
                codeBuilder.AddLine("    protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)");
                codeBuilder.AddLine("    {");
                if (this.EnableAutomaticAuthorization)
                    codeBuilder.AddLine("        return LinxAutorization.CheckAuthorization(actionContext, string.Format(\"{0}#{1}#{1}/{2}\", \"" + api.EntityAdapterDesignerRoot.TargetNamespace + "\", \"" + api.GetRoutePrefix() + "\", actionContext.ActionDescriptor.ActionName));");
                else
                    codeBuilder.AddLine("        return true;");
                codeBuilder.AddLine("    }");
                codeBuilder.AddLine("}");
            }

            //End Namespace
            codeBuilder.DecreaseIndent();
            codeBuilder.AddLine("}");
        }

        private void GenerateFeedControllerCode(Tools.CodeBuilder codeBuilder, string apiName, string authorizeAttribute)
        {
            string contextName = "BusinessNS." + this.GetDomainServiceName();
            codeBuilder.AddLine();
            if (!authorizeAttribute.IsNullOrEmpty())
                codeBuilder.AddLine("[ODataBasicAuthenticationFilter]");
            codeBuilder.AddLine("public partial class " + apiName + "FeedController : ODataController");
            codeBuilder.AddLine("{");
            codeBuilder.IncreaseIndent();
            codeBuilder.AddLine("private " + contextName + " _context;");
            codeBuilder.AddLine("public " + contextName + " Context { get {  if (_context == null) { _context = new " + contextName + "(); _context.IsSecure = true; } return _context; }  }");
            codeBuilder.AddLine();

            codeBuilder.AddLine("#region Get Action to Business Entities");
            codeBuilder.AddLine();

            foreach (var entity in this.EntityAdapters.Where(e => e.ExposeAsService && !e.IsDashboardFilter))
            {
                Dictionary<string, string> pKeys = new Dictionary<string, string>();
                if (entity.HasDynamicPrimaryKey())
                    pKeys.Add("EntityUniqueKey", "System.Guid");
                else
                {
                    foreach (var pKey in entity.EntityAdapterProperties.Where(e => entity.IsPrimaryKey(e)))
                        pKeys.Add(pKey.Name, pKey.Datatype);
                }
                if (pKeys.Count() > 0)
                {
                    var indexKeys = pKeys.Select(k => k.Key).ToList();
                    //GET by Key
                    codeBuilder.AddLine();
                    if (!authorizeAttribute.IsNullOrEmpty())
                        codeBuilder.AddLine("[" + authorizeAttribute + "]");
                    codeBuilder.AddLine("[EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]");
                    codeBuilder.AddLine("public IQueryable<BusinessNS." + entity.Name + "> Get" + entity.Name + "ById(" + String.Join(", ", pKeys.Select(p => "[FromODataUri]" + p.Value + " key" + indexKeys.IndexOf(p.Key).ToString())) + ")");
                    codeBuilder.AddLine("{");
                    if (!entity.HasDynamicPrimaryKey())
                    {
                        codeBuilder.AddLine("    var entity = this.Context.Get" + entity.Name + "ByKey(" + String.Join(", ", pKeys.Select(p => "key" + indexKeys.IndexOf(p.Key).ToString())) + ");");
                        codeBuilder.AddLine("    if (entity != null)");
                        codeBuilder.AddLine("       return (new BusinessNS." + entity.Name + "[] { entity }).AsQueryable();");
                        codeBuilder.AddLine("    else");
                    }
                    codeBuilder.AddLine("       return default(IQueryable<BusinessNS." + entity.Name + ">);");
                    codeBuilder.AddLine("}");
                    //GET by jEntitySearch
                    codeBuilder.AddLine();
                    if (!authorizeAttribute.IsNullOrEmpty())
                        codeBuilder.AddLine("[" + authorizeAttribute + "]");
                    codeBuilder.AddLine("[EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]");
                    codeBuilder.AddLine("public IQueryable<BusinessNS." + entity.Name + "> Get" + entity.Name + "ByEntitySearch([FromODataUri]String jEntitySearch)");
                    codeBuilder.AddLine("{");
                    codeBuilder.AddLine("    if (!jEntitySearch.IsNullOrEmpty())");
                    codeBuilder.AddLine("    {");
                    codeBuilder.AddLine("        jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));");
                    codeBuilder.AddLine("        var entity = this.Context.Get" + entity.Name + "ByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS." + entity.Name + "), jEntitySearch, false, false, false), jEntitySearch);");
                    codeBuilder.AddLine("        if (entity != null) return entity.AsQueryable();");
                    codeBuilder.AddLine("    }");
                    codeBuilder.AddLine("    return default(IQueryable<BusinessNS." + entity.Name + ">);");
                    codeBuilder.AddLine("}");

                    // default GET
                    codeBuilder.AddLine();
                    if (!authorizeAttribute.IsNullOrEmpty())
                        codeBuilder.AddLine("[" + authorizeAttribute + "]");
                    codeBuilder.AddLine("[EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]");
                    codeBuilder.AddLine("public IQueryable<BusinessNS." + entity.Name + "> Get" + entity.Name + "()");
                    codeBuilder.AddLine("{");
                    codeBuilder.AddLine("    return this.Context.Get" + entity.Name + "ByEntitySearchNoAssociations(null).AsQueryable();");
                    codeBuilder.AddLine("}");

                    if (entity.TargetEntityAdapter != null && entity.IsParentCompositionAllowed())
                    {
                        //GET
                        codeBuilder.AddLine();
                        if (!authorizeAttribute.IsNullOrEmpty())
                            codeBuilder.AddLine("[" + authorizeAttribute + "]");
                        codeBuilder.AddLine("[EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]");
                        codeBuilder.AddLine("public IQueryable<BusinessNS." + entity.Name + "ParentComposition> Get" + entity.Name + "ParentComposition()");
                        codeBuilder.AddLine("{");
                        codeBuilder.AddLine("    return this.Context.Get" + entity.Name + "ParentCompositionByEntitySearchNoAssociations(null).AsQueryable();");
                        codeBuilder.AddLine("}");

                        //GET ParentComposition by jEntitySearch
                        codeBuilder.AddLine();
                        if (!authorizeAttribute.IsNullOrEmpty())
                            codeBuilder.AddLine("[" + authorizeAttribute + "]");
                        codeBuilder.AddLine("[EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]");
                        codeBuilder.AddLine("public IQueryable<BusinessNS." + entity.Name + "ParentComposition> Get" + entity.Name + "ParentCompositionByEntitySearch([FromODataUri]String jEntitySearch)");
                        codeBuilder.AddLine("{");
                        codeBuilder.AddLine("    if (!jEntitySearch.IsNullOrEmpty())");
                        codeBuilder.AddLine("    {");
                        codeBuilder.AddLine("        jEntitySearch = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(jEntitySearch));");
                        EntityAdapter target = entity;
                        do
                        {
                            codeBuilder.AddLine("        jEntitySearch = jEntitySearch.Replace(\"" + target.Name + "{\", \"" + entity.Name + "ParentComposition{\");");
                            target = target.TargetEntityAdapter;
                        }
                        while (!target.IsNull());

                        codeBuilder.AddLine("        var entity = this.Context.Get" + entity.Name + "ParentCompositionByEntitySearchNoAssociations(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS." + entity.Name + "ParentComposition), jEntitySearch, false, false, false), jEntitySearch);");
                        codeBuilder.AddLine("        if (entity != null) return entity.AsQueryable();");
                        codeBuilder.AddLine("    }");
                        codeBuilder.AddLine("    return default(IQueryable<BusinessNS." + entity.Name + "ParentComposition>);");
                        codeBuilder.AddLine("}");
                    }

                    if (entity.TargetEntityAdapter != null)
                    {
                        string associationName = entity.TargetEntityAdapter.Name;
                        codeBuilder.AddLine();
                        if (!authorizeAttribute.IsNullOrEmpty())
                            codeBuilder.AddLine("[" + authorizeAttribute + "]");
                        codeBuilder.AddLine("[EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]");
                        codeBuilder.AddLine("public IQueryable<BusinessNS." + associationName + "> Get" + entity.Name + "__" + associationName + "(" + String.Join(", ", pKeys.Select(p => p.Value + " key" + indexKeys.IndexOf(p.Key).ToString())) + ", string navigation)");
                        codeBuilder.AddLine("{");
                        if (!entity.HasDynamicPrimaryKey())
                        {
                            codeBuilder.AddLine("    var entity = this.Context.Get" + entity.Name + "ByKey(" + String.Join(", ", pKeys.Select(p => "key" + indexKeys.IndexOf(p.Key).ToString())) + ");");
                            codeBuilder.AddLine("    if (entity != null && navigation == \"" + associationName + "\")");
                            codeBuilder.AddLine("    {");
                            codeBuilder.AddLine("       entity.LoadParent(_context);");
                            codeBuilder.AddLine("       return (new BusinessNS." + associationName + "[] { entity." + associationName + " }).AsQueryable();");
                            codeBuilder.AddLine("    }");
                            codeBuilder.AddLine("    else");
                        }
                        codeBuilder.AddLine("       return default(IQueryable<BusinessNS." + associationName + ">);");
                        codeBuilder.AddLine("}");
                    }

                    foreach (var associationName in entity.SourceEntityAdapters.Select(e => e.Name))
                    {
                        codeBuilder.AddLine();
                        if (!authorizeAttribute.IsNullOrEmpty())
                            codeBuilder.AddLine("[" + authorizeAttribute + "]");
                        codeBuilder.AddLine("[EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]");
                        codeBuilder.AddLine("public IQueryable<BusinessNS." + associationName + "> Get" + entity.Name + "__" + associationName + "(" + String.Join(", ", pKeys.Select(p => p.Value + " key" + indexKeys.IndexOf(p.Key).ToString())) + ", string navigation)");
                        codeBuilder.AddLine("{");
                        if (!entity.HasDynamicPrimaryKey())
                        {
                            codeBuilder.AddLine("    var entity = this.Context.Get" + entity.Name + "ByKey(" + String.Join(", ", pKeys.Select(p => "key" + indexKeys.IndexOf(p.Key).ToString())) + ");");
                            codeBuilder.AddLine("    if (entity != null && navigation == \"" + associationName + "List\")");
                            codeBuilder.AddLine("    {");
                            codeBuilder.AddLine("       entity.FillDetails(_context, null, null, new string[] { \"" + associationName + "\" });");
                            codeBuilder.AddLine("       return entity." + associationName + "List.AsQueryable();");
                            codeBuilder.AddLine("    }");
                            codeBuilder.AddLine("    else");
                        }
                        codeBuilder.AddLine("       return default(IQueryable<BusinessNS." + associationName + ">);");
                        codeBuilder.AddLine("}");
                    }
                }
            }

            codeBuilder.AddLine("#endregion");
            codeBuilder.AddLine();
            codeBuilder.DecreaseIndent();
            codeBuilder.AddLine("}");
            codeBuilder.AddLine();
        }

        public string GetDefaultByDataType(string dataType)
        {
            return ((dataType.ToLower() == "string" || dataType == "System.String") ? "\"\"" : ((dataType == "DateTime" || dataType == "System.DateTime") ? "System.DateTime.MinValue" : "default(" + dataType + ")"));
        }

        private void GenerateDomainServiceActionsToController(WebApiController api, Linx.Tools.CodeBuilder codeBuilder, string repositoryReference, string contextReference, string authorizeAttribute, bool isNetCore, bool isDataService, Project webApiProject = null)
        {
            string basePath = "~/bin/";
            codeBuilder.AddLine();
            if (isNetCore)
            {
                if (webApiProject != null)
                {
                    string corPrjName = webApiProject.Name;
                    basePath = @"BusinessModules\\" + corPrjName + @"\\bin\\";
                }
                codeBuilder.AddLine("[HttpGet(\"GetTemplateReport\")]");
            }
            else
                codeBuilder.AddLine("[Route(\"GetTemplateReport\"), System.Web.Http.HttpGet()]");
            codeBuilder.AddLine("public string GetTemplateReport(string reportPath)");
            codeBuilder.AddLine("{");
            codeBuilder.AddLine("    var zip = new LinxZip();");
            codeBuilder.AddLine("    zip.AddFile(" + (isNetCore ? _appStartFolder + ".ModuleInitializer_" + api.Name + ".MapPath" : "System.Web.HttpContext.Current.Server.MapPath") + "(\"~/\" + reportPath));");
            //codeBuilder.AddLine("    zip.AddStringContent(\"Readme.txt\", \"Linx Report\");");
            codeBuilder.AddLine("    zip.AddFile(" + (isNetCore ? _appStartFolder + ".ModuleInitializer_" + api.Name + ".MapPath" : "System.Web.HttpContext.Current.Server.MapPath") + "(\"" + basePath + "" + GetAssemblyName() + ".Reports.dll\"));");
            codeBuilder.AddLine("    zip.AddFile(" + (isNetCore ? _appStartFolder + ".ModuleInitializer_" + api.Name + ".MapPath" : "System.Web.HttpContext.Current.Server.MapPath") + "(\"" + basePath + "" + GetAssemblyName() + ".dll\"));");
            codeBuilder.AddLine("    return Convert.ToBase64String(zip.GetZipBytes());");
            codeBuilder.AddLine("}");
            codeBuilder.AddLine();
            if (isNetCore)
                codeBuilder.AddLine("[HttpGet(\"GetReportDataSource\")]");
            else
                codeBuilder.AddLine("[Route(\"GetReportDataSource\"), System.Web.Http.HttpGet()]");
            codeBuilder.AddLine("public string GetReportDataSource()");
            codeBuilder.AddLine("{");
            codeBuilder.AddLine("    var zip = new LinxZip();");
            //codeBuilder.AddLine("    zip.AddStringContent(\"Readme.txt\", \"Linx Report\");");
            codeBuilder.AddLine("    zip.AddFile(" + (isNetCore ? _appStartFolder + ".ModuleInitializer_" + api.Name + ".MapPath" : "System.Web.HttpContext.Current.Server.MapPath") + "(\"" + basePath + "" + GetAssemblyName() + ".Reports.dll\"));");
            codeBuilder.AddLine("    zip.AddFile(" + (isNetCore ? _appStartFolder + ".ModuleInitializer_" + api.Name + ".MapPath" : "System.Web.HttpContext.Current.Server.MapPath") + "(\"" + basePath + "" + GetAssemblyName() + ".dll\"));");
            codeBuilder.AddLine("    return Convert.ToBase64String(zip.GetZipBytes());");
            codeBuilder.AddLine("}");
            codeBuilder.AddLine();
            if (isNetCore)
                codeBuilder.AddLine("[HttpGet(\"GetDomainsInfo\")]");
            else
                codeBuilder.AddLine("[Route(\"GetDomainsInfo\"), System.Web.Http.HttpGet()]");
            codeBuilder.AddLine("public string[] GetDomainsInfo(string domainNames)");
            codeBuilder.AddLine("{");
            codeBuilder.AddLine("    return " + this.TargetNamespace + ".Domains.DomainHelper.GetDomainsInfo(domainNames);");
            codeBuilder.AddLine("}");
            codeBuilder.AddLine();
            if (isNetCore)
                codeBuilder.AddLine("[HttpGet(\"GetDomainValues\")]");
            else
                codeBuilder.AddLine("[Route(\"GetDomainValues\"), System.Web.Http.HttpGet()]");
            codeBuilder.AddLine("public Dictionary<string, string> GetDomainValues(string domainName)");
            codeBuilder.AddLine("{");
            codeBuilder.AddLine("    return " + this.TargetNamespace + ".Domains.DomainHelper.GetDomainValues(domainName);");
            codeBuilder.AddLine("}");
            codeBuilder.AddLine();
            codeBuilder.AddLine("#region Get LookUps");
            foreach (var lookUp in this.LookUpAdapters.Where(e => e.EntityAdapter != null && e.EntityAdapter.ExposeAsService))
            {
                codeBuilder.AddLine();
                if (!authorizeAttribute.IsNullOrEmpty())
                    codeBuilder.AddLine("[" + authorizeAttribute + "]");
                if (isNetCore)
                {
                    if (isDataService)
                        codeBuilder.AddLine("[EnableQuery()]");
                    codeBuilder.AddLine("[HttpGet(\"" + lookUp.Name + "\")]");
                    codeBuilder.AddLine("[HttpGet(\"GetAll" + lookUp.Name + "\")]");
                }
                else
                    codeBuilder.AddLine("[Route(\"GetAll" + lookUp.Name + "\"), System.Web.Http.HttpGet()]");
                codeBuilder.AddLine("public IQueryable<BusinessNS." + lookUp.Name + "> GetAll" + lookUp.Name + "()");
                codeBuilder.AddLine("{");

                if (isNetCore && isDataService)
                    codeBuilder.AddLine("    this.HttpContext.AdjustOdataFeature(App_Start.ModuleInitializer_" + api.Name + ".Model, _routePrefix, \"" + lookUp.Name + "\", App_Start.ModuleInitializer_" + api.Name + ".ServiceProvider);");

                codeBuilder.AddLine("    return " + contextReference + ".GetAll" + lookUp.Name + "()" + (lookUp.QueryReturnType == EntityQueryReturnType.IEnumerable ? ".AsQueryable()" : "") + ";");
                codeBuilder.AddLine("}");
                codeBuilder.AddLine();
                if (!authorizeAttribute.IsNullOrEmpty())
                    codeBuilder.AddLine("[" + authorizeAttribute + "]");
                if (isNetCore)
                {
                    codeBuilder.AddLine("[HttpGet(\"Get" + lookUp.Name + "ByEntitySearch\")]");
                }
                else
                    codeBuilder.AddLine("[Route(\"Get" + lookUp.Name + "ByEntitySearch\"), System.Web.Http.HttpGet()]");
                codeBuilder.AddLine("public IQueryable<BusinessNS." + lookUp.Name + "> Get" + lookUp.Name + "ByEntitySearch(string propertyName, string jEntitySearch)");
                codeBuilder.AddLine("{");

                if (isNetCore && isDataService)
                    codeBuilder.AddLine("    this.HttpContext.AdjustOdataFeature(App_Start.ModuleInitializer_" + api.Name + ".Model, _routePrefix, \"" + lookUp.Name + "\", App_Start.ModuleInitializer_" + api.Name + ".ServiceProvider);");

                codeBuilder.AddLine("    return " + contextReference + ".Get" + lookUp.Name + "ByEntitySearch(propertyName, Linx.Tools.EntitySearch.ParseFromJEntitySearch(jEntitySearch, true))" + (lookUp.QueryReturnType == EntityQueryReturnType.IEnumerable ? ".AsQueryable()" : "") + ";");
                codeBuilder.AddLine("}");
            }
            codeBuilder.AddLine("#endregion");

            codeBuilder.AddLine("#region Get KPI Ranges");
            foreach (var entity in this.EntityAdapters.Where(e => e.ExposeAsService && e.DerivedEntityAdapters.Count == 0).ToList())
            {
                foreach (var kpiName in entity.GetAllInheritanceAttributes().Where(e => !e.KpiName.IsNullOrEmpty()).Select(d => d.KpiName).Distinct())
                {
                    codeBuilder.AddLine();
                    if (!authorizeAttribute.IsNullOrEmpty())
                        codeBuilder.AddLine("[" + authorizeAttribute + "]");
                    if (isNetCore)
                        codeBuilder.AddLine("[HttpGet(\"Get" + kpiName + "Ranges\")]");
                    else
                        codeBuilder.AddLine("[Route(\"Get" + kpiName + "Ranges\"), System.Web.Http.HttpGet()]");
                    codeBuilder.AddLine("public IEnumerable<KpiRangeItem> Get" + kpiName + "Ranges()");
                    codeBuilder.AddLine("{");
                    codeBuilder.AddLine("    var kpi = new " + this.TargetNamespace + ".KPIs." + kpiName + "();");
                    if (!isNetCore)
                        codeBuilder.AddLine("    Linx.Business.Tools.KpiManager.UpdateKpiInfo(kpi);");
                    codeBuilder.AddLine("    return kpi.Ranges.Values.ToArray();");
                    codeBuilder.AddLine("}");
                    codeBuilder.AddLine();
                }
            }
            codeBuilder.AddLine("#endregion");

            codeBuilder.AddLine();
            codeBuilder.AddLine("#region Get Business Entities");
            GenDSGetBmMetaDataPropertyDefinition(codeBuilder, contextReference, isNetCore);
            foreach (var entity in this.EntityAdapters.Where(e => e.ExposeAsService))
            {
                GenerateAllDSGets(api, codeBuilder, contextReference, entity, authorizeAttribute, false, isNetCore, isDataService, webApiProject);
                GenDSAddSearchById(codeBuilder, entity);
                GenDSGetByIdDefinition(codeBuilder, contextReference, entity, authorizeAttribute);
            }
            codeBuilder.AddLine("#endregion");
            codeBuilder.AddLine();
            codeBuilder.AddLine("#region Get Business Entities By Parent Composition");
            foreach (var entity in this.EntityAdapters.Where(e => e.ExposeAsService && e.TargetEntityAdapter != null && e.IsParentCompositionAllowed()))
            {
                GenerateAllDSGets(api, codeBuilder, contextReference, entity, authorizeAttribute, true, isNetCore, isDataService, webApiProject);
            }
            codeBuilder.AddLine("#endregion");

            codeBuilder.AddLine();
            codeBuilder.AddLine("#region Save Changes");
            if (!authorizeAttribute.IsNullOrEmpty())
                codeBuilder.AddLine("[" + authorizeAttribute + "]");
            if (isNetCore)
                codeBuilder.AddLine("[HttpPost(\"SaveChanges\")]");
            else
                codeBuilder.AddLine("[Route(\"SaveChanges\"), System.Web.Http.HttpPost()]");
            codeBuilder.AddLine("public SaveResult SaveChanges(" + (isNetCore ? "[FromBody] " : "") + "JObject saveBundle)");
            codeBuilder.AddLine("{");
            codeBuilder.AddLine("    var result = " + repositoryReference + ".SaveChanges(saveBundle);");
            codeBuilder.AddLine("    " + contextReference + ".Dispose();");
            codeBuilder.AddLine("    return result;");
            codeBuilder.AddLine("}");


            foreach (var entity in this.EntityAdapters.Where(e => e.IsBufferSaving()))
            {
                if (!authorizeAttribute.IsNullOrEmpty())
                    codeBuilder.AddLine("[" + authorizeAttribute + "]");
                if (isNetCore)
                    codeBuilder.AddLine("[HttpPost(\"Save" + entity.Name + "\")]");
                else
                    codeBuilder.AddLine("[Route(\"Save" + entity.Name + "\"), System.Web.Http.HttpPost()]");
                codeBuilder.AddLine("public List<BusinessNS." + entity.Name + "> Save" + entity.Name + "(" + (isNetCore ? "[FromBody] " : "") + "List<BusinessNS." + entity.Name + "> dataList)");
                codeBuilder.AddLine("{");

                codeBuilder.AddLine("    if (dataList != null && dataList.Count > 0)");
                codeBuilder.AddLine("    {");
                codeBuilder.AddLine("        List<ChangeSetEntry> changeSetEntries = new List<ChangeSetEntry>();");
                codeBuilder.AddLine("        foreach (var data in dataList.Where(e => e.ChangeState.InList(\"I\", \"U\", \"D\")).ToArray())");
                codeBuilder.AddLine("        {");
                codeBuilder.AddLine("           if (data.ChangeState == \"D\") data.ResetDetails();");
                codeBuilder.AddLine("           foreach (var entity in data.GetFlatEntities())");
                codeBuilder.AddLine("           {");
                codeBuilder.AddLine("               string state = entity.GetPropertyValue(\"ChangeState\") as string;");
                codeBuilder.AddLine("               if (state.InList(\"I\", \"U\", \"D\"))");
                codeBuilder.AddLine("               {");
                codeBuilder.AddLine("                   var changeOP = (state == \"I\" ? DomainOperation.Insert : (state == \"D\" ? DomainOperation.Delete :  DomainOperation.Update));");
                codeBuilder.AddLine("                   changeSetEntries.Add(new ChangeSetEntry(changeSetEntries.Count, entity, null, changeOP) { HasMemberChanges = (changeOP == DomainOperation.Update) });");
                codeBuilder.AddLine("               }");
                codeBuilder.AddLine("           }");
                codeBuilder.AddLine("           if (data.ChangeState != \"D\") data.ResetDetails();");
                codeBuilder.AddLine("        }");

                codeBuilder.AddLine("        repository.Context.SaveEntities(changeSetEntries, false);");
                codeBuilder.AddLine("    }");

                codeBuilder.AddLine("    " + contextReference + ".Dispose();");

                codeBuilder.AddLine("    //Set return with nochanges");
                codeBuilder.AddLine("    var result = dataList.Where(e => e.ChangeState.InList(\"I\", \"U\", \"N\")).ToList();");
                codeBuilder.AddLine("    foreach (var data in result.ToArray())");
                codeBuilder.AddLine("    {");
                codeBuilder.AddLine("           if (data.ChangeState == \"N\") data.ResetDetails();");
                codeBuilder.AddLine("           else data.ResetChangeState();");
                codeBuilder.AddLine("    }");

                codeBuilder.AddLine("    return result;");
                codeBuilder.AddLine("}");

                #region Methods for saveLazing
                if (!authorizeAttribute.IsNullOrEmpty())
                    codeBuilder.AddLine("[" + authorizeAttribute + "]");
                if (isNetCore)
                    codeBuilder.AddLine("[HttpPost(\"Save" + entity.Name + "InCache\")]");
                else
                    codeBuilder.AddLine("[Route(\"Save" + entity.Name + "InCache\"), System.Web.Http.HttpPost()]");
                codeBuilder.AddLine("public void Save" + entity.Name + "InCache(" + (isNetCore ? "[FromBody] " : "") + "SaveInformation<BusinessNS." + entity.Name + "> saveInfo)");
                codeBuilder.AddLine("{");
                codeBuilder.AddLine("    saveInfo.Validate();");
                codeBuilder.AddLine("    QueueTransaction.SaveTransaction(saveInfo, " + (isNetCore ? "System.Reflection.Assembly.GetEntryAssembly().FullName" : "System.Reflection.Assembly.GetExecutingAssembly().FullName") + ", " + (isNetCore ? "this.GetType().FullName" : "this.ControllerContext") + ", \"Save" + entity.Name + "\");");
                codeBuilder.AddLine("}");

                codeBuilder.AddLine((IsAspNetCore ? "private" : "public") + " List<BusinessNS." + entity.Name + "> Save" + entity.Name + "__ForMEF(string jsonString, string viewMapInfo, List<ChangeTracker> changes)");
                codeBuilder.AddLine("{");
                codeBuilder.AddLine("    List<BusinessNS." + entity.Name + "> dataList = SerializationManager<List<BusinessNS." + entity.Name + ">>.JsonToObject(jsonString);");
                codeBuilder.AddLine("    if (!viewMapInfo.IsNullOrEmpty() && changes.Count > 0)");
                codeBuilder.AddLine("    {");
                codeBuilder.AddLine("        var viewMap = ViewMapHelper.Parse(viewMapInfo);");
                codeBuilder.AddLine("        if(changes.Any(c => c.ComponentName == viewMap.ParentUIView))");
                codeBuilder.AddLine("            dataList = viewMap.ReplaceEntities(dataList, changes.First(c => c.ComponentName == viewMap.ParentUIView).ListReturnedObjects);");
                codeBuilder.AddLine("    }");
                codeBuilder.AddLine("    return Save" + entity.Name + "(dataList);");
                codeBuilder.AddLine("}");
                #endregion
            }

            if (this.EntityAdapters.Any(e => e.IsBufferSaving()))
            {
                codeBuilder.AddLine();
                codeBuilder.AddLine();
                if (!authorizeAttribute.IsNullOrEmpty())
                    codeBuilder.AddLine("[" + authorizeAttribute + "]");
                if (isNetCore)
                    codeBuilder.AddLine("[HttpGet(\"SubmitAllChanges\")]");
                else
                    codeBuilder.AddLine("[Route(\"SubmitAllChanges\"), System.Web.Http.HttpGet()]");
                codeBuilder.AddLine("public Dictionary<string, List<object>> SubmitAllChanges(Guid transactionID)");
                codeBuilder.AddLine("{");
                codeBuilder.AddLine("    var obj = QueueTransaction.GetTransaction(transactionID);");
                codeBuilder.AddLine("    if (obj.IsNull())");
                codeBuilder.AddLine("        throw new ArgumentOutOfRangeException(string.Format(\"Não foi possível localizar o objeto 'QueueTransaction', para ID={0}\", transactionID));");
                codeBuilder.AddLine("    Dictionary<string, List<object>> changes = new Dictionary<string, List<object>>();");
                codeBuilder.AddLine("    var operations = obj.SubmitTansaction();");
                codeBuilder.AddLine("    foreach (var _op in operations) changes.Add(_op.ComponentName, _op.ListReturnedObjects);");
                codeBuilder.AddLine("    return changes;");
                codeBuilder.AddLine("}");
                if (!authorizeAttribute.IsNullOrEmpty())
                    codeBuilder.AddLine("[" + authorizeAttribute + "]");
                if (isNetCore)
                    codeBuilder.AddLine("[HttpGet(\"CancelAllChanges\")]");
                else
                    codeBuilder.AddLine("[Route(\"CancelAllChanges\"), System.Web.Http.HttpGet()]");
                codeBuilder.AddLine("public void CancelAllChanges(Guid transactionID)");
                codeBuilder.AddLine("{");
                codeBuilder.AddLine("    var obj = QueueTransaction.GetTransaction(transactionID);");
                codeBuilder.AddLine("    if (!obj.IsNull())");
                codeBuilder.AddLine("        obj.DeleteCache();");
                codeBuilder.AddLine("}");
            }
            codeBuilder.AddLine("#endregion");

        }

        public bool IsLargeDataMode()
        {
            return this.IsAspNetCore || this.EntityAdapters.Any(e => e.IsLargeDataMode && e.TargetEntityAdapter == null);
        }

        public bool IsBufferSaving()
        {
            return this.EntityAdapters.Any(e => e.IsBufferSaving());
        }

        private void GenerateAllDSGets(WebApiController api, Linx.Tools.CodeBuilder codeBuilder, string contexReference, EntityAdapter entity, string authorizeAttribute, bool byParentComposition, bool isNetCore, bool isDataService, Project webApiProject = null)
        {
            if (!byParentComposition)
            {
                GenDSGetDefinition(api, codeBuilder, contexReference, entity, isNetCore, isDataService, false, false, authorizeAttribute, byParentComposition);
                GenDSGetDefinition(api, codeBuilder, contexReference, entity, isNetCore, isDataService, false, true, authorizeAttribute, byParentComposition);
                GenDSGetDefinition(api, codeBuilder, contexReference, entity, isNetCore, isDataService, true, false, authorizeAttribute, byParentComposition);
                GenDSGetDefinition(api, codeBuilder, contexReference, entity, isNetCore, isDataService, false, false, authorizeAttribute, byParentComposition, true);
            }
            GenDSGetDefinition(api, codeBuilder, contexReference, entity, isNetCore, isDataService, true, true, authorizeAttribute, byParentComposition);
            GenDSExportToExcelDefinition(api, codeBuilder, contexReference, entity, isNetCore, authorizeAttribute, byParentComposition);
            GenDSExportToReportXmlDefinition(api, codeBuilder, contexReference, entity, isNetCore, authorizeAttribute, byParentComposition, webApiProject);
            GenDSGetSampleDefinition(api, codeBuilder, contexReference, entity, isNetCore, isDataService, byParentComposition, authorizeAttribute);
        }

        private void GenDSExportToExcelDefinition(WebApiController api, Linx.Tools.CodeBuilder codeBuilder, string contextReference, EntityAdapter entity, bool isNetCore, string authorizeAttribute = "", bool byParentComposition = false)
        {
            if (isNetCore)
                codeBuilder.AddLine("[HttpPost(\"Get" + entity.Name + (byParentComposition ? "ParentComposition" : "") + "ToExcel\")]");
            else
                codeBuilder.AddLine("[Route(\"Get" + entity.Name + (byParentComposition ? "ParentComposition" : "") + "ToExcel\"), System.Web.Http.HttpPost()]");
            if (!authorizeAttribute.IsNullOrEmpty())
                codeBuilder.AddLine("[" + authorizeAttribute + "]");
            codeBuilder.AddLine("public string Get" + entity.Name + (byParentComposition ? "ParentComposition" : "") + "ToExcel(" + (isNetCore ? "[FromBody] " : "") + "string[] parameters)");
            codeBuilder.AddLine("{");

            codeBuilder.AddLine("    string jEntitySearch = parameters[0];");
            codeBuilder.AddLine("    string translatedJEntitySearch = parameters[1];");
            codeBuilder.AddLine("    string columnsDefinition = parameters[2];");
            codeBuilder.AddLine("    var columns = StringExtension.ConvertToDictionary(columnsDefinition);");

            if (entity.EnableMetaDataFilter)
                codeBuilder.AddLine("    jEntitySearch += \"LinqValidProperties{LinqValidProperties#==#S\" + string.Join(\",\", columns.Keys) + \"}\";");

            if (byParentComposition)
            {
                EntityAdapter target = entity;
                do
                {
                    codeBuilder.AddLine("    jEntitySearch = jEntitySearch.Replace(\"" + target.Name + "{\", \"" + entity.Name + "ParentComposition{\");");
                    target = target.TargetEntityAdapter;
                }
                while (!target.IsNull());

            }
            codeBuilder.AddLine("    var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS." + entity.Name + (byParentComposition ? "ParentComposition" : "") + "), jEntitySearch, false, " + entity.IsModelViewSource().ToString().ToLower() + ", " + entity.IsOlap().ToString().ToLower() + ");");
            codeBuilder.AddLine("    var entities = " + contextReference + ".Get" + entity.Name + (byParentComposition ? "ParentComposition" : "") + "ByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch)" + ((!entity.PrimaryEntity.IsNullOrEmpty() || entity.EntityAdapterRepresentation != null) && entity.QueryReturnType == EntityQueryReturnType.IQueryable ? "" : ".AsQueryable()") + ".OrderBy(\"" + entity.GetOrderByCommand() + "\");");
            codeBuilder.AddLine("    var metadata = " + contextReference + ".GetMetaDataObject(\"" + this.GetContextNamespace() + "." + entity.Name + "\");");
            //treat columns that
            codeBuilder.AddLine("    if (columns.Count > 0)");
            codeBuilder.AddLine("    {");
            codeBuilder.AddLine("        foreach (var item in metadata[0].Properties)");
            codeBuilder.AddLine("        {");
            codeBuilder.AddLine("            item.IsBrowsable = columns.ContainsKey(item.Name);");
            codeBuilder.AddLine("            if (item.IsBrowsable && !columns[item.Name].IsNullOrEmpty())");
            codeBuilder.AddLine("                item.Caption = columns[item.Name];");
            codeBuilder.AddLine("            item.Order = item.IsBrowsable ? Array.IndexOf(columns.Keys.ToArray(), item.Name) : -1;");
            codeBuilder.AddLine("        }");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("    if ((entities.Count() * metadata[0].Properties.Count(p=> p.IsBrowsable)) < maxObjectExcelReturned)");
            codeBuilder.AddLine("    	return Convert.ToBase64String(ExcelExport.CreateExcelDocumentFile(new ExcelExport.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch }));");
            codeBuilder.AddLine("    else");
            codeBuilder.AddLine("       return ExcelExportPagination<BusinessNS." + entity.Name + (byParentComposition ? "ParentComposition" : "") + ">.CreateExcelDocumentFileMapPath(\"" + entity.Name + "\",new ExcelExportPagination<BusinessNS." + entity.Name + (byParentComposition ? "ParentComposition" : "") + ">.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });");
            codeBuilder.AddLine("}");
        }

        private void GenDSExportToReportXmlDefinition(WebApiController api, Linx.Tools.CodeBuilder codeBuilder, string contextReference, EntityAdapter entity, bool isNetCore, string authorizeAttribute = "", bool byParentComposition = false, Project webApiProject = null)
        {
            string basePath = "~/bin/";
            codeBuilder.AddLine();
            if (isNetCore)
            {
                if (webApiProject != null)
                {
                    string corPrjName = webApiProject.Name;
                    basePath = @"BusinessModules\\" + corPrjName + @"\\bin\\";
                }
                codeBuilder.AddLine("[HttpPost(\"Get" + entity.Name + (byParentComposition ? "ParentComposition" : "") + "ToReportXml\")]");
            }
            else
                codeBuilder.AddLine("[Route(\"Get" + entity.Name + (byParentComposition ? "ParentComposition" : "") + "ToReportXml\"), System.Web.Http.HttpPost()]");
            if (!authorizeAttribute.IsNullOrEmpty())
                codeBuilder.AddLine("[" + authorizeAttribute + "]");
            codeBuilder.AddLine("public string Get" + entity.Name + (byParentComposition ? "ParentComposition" : "") + "ToReportXml(" + (isNetCore ? "[FromBody] " : "") + "string[] parameters)");
            codeBuilder.AddLine("{");

            codeBuilder.AddLine("    string reportName = parameters[0];");
            codeBuilder.AddLine("    string jEntitySearch = parameters[1];");
            codeBuilder.AddLine("    string translatedJEntitySearch = parameters[2];");
            codeBuilder.AddLine("    string columnsDefinition = parameters[3];");
            codeBuilder.AddLine("    string serviceBusUrl = parameters[4];");
            codeBuilder.AddLine("    bool exportMedia = Convert.ToBoolean(parameters[5]);");
            codeBuilder.AddLine("    var columns = StringExtension.ConvertToDictionary(columnsDefinition);");

            if (entity.EnableMetaDataFilter)
                codeBuilder.AddLine("    jEntitySearch += \"LinqValidProperties{LinqValidProperties#==#S\" + string.Join(\",\", columns.Keys) + \"}\";");

            codeBuilder.AddLine("    var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS." + entity.Name + (byParentComposition ? "ParentComposition" : "") + "), jEntitySearch, false, " + entity.IsModelViewSource().ToString().ToLower() + ", " + entity.IsOlap().ToString().ToLower() + ");");
            codeBuilder.AddLine("    var entities = " + contextReference + ".Get" + entity.Name + (byParentComposition ? "ParentComposition" : "") + "ByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);");
            codeBuilder.AddLine("    var metadata = " + contextReference + ".GetMetaDataObject(\"" + this.GetContextNamespace() + "." + entity.Name + "\", true);");

            codeBuilder.AddLine("    if (columns.Count > 0)");
            codeBuilder.AddLine("    {");
            codeBuilder.AddLine("        foreach (var item in metadata[0].Properties)");
            codeBuilder.AddLine("        {");
            codeBuilder.AddLine("            item.IsBrowsable = columns.ContainsKey(item.Name);");
            codeBuilder.AddLine("            if (item.IsBrowsable && !columns[item.Name].IsNullOrEmpty())");
            codeBuilder.AddLine("                item.Caption = columns[item.Name];");
            codeBuilder.AddLine("            item.Order = item.IsBrowsable ? Array.IndexOf(columns.Keys.ToArray(), item.Name) : -1;");
            codeBuilder.AddLine("        }");
            codeBuilder.AddLine("    }");
            codeBuilder.AddLine("    var zip = new LinxZip();");
            codeBuilder.AddLine("    zip.AddStringContent(reportName + \".trdx\", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = \"" + GetAssemblyName() + ".Reports\", DataSourceFullName = \"" + GetAssemblyName() + ".Reports." + GetContextName() + "DataSource\", DataSourceObject = \"Get" + entity.Name + (byParentComposition ? "ParentComposition" : "") + "\", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl, HasMedia = exportMedia }));");
            codeBuilder.AddLine("    zip.AddFile(" + (isNetCore ? _appStartFolder + ".ModuleInitializer_" + api.Name + ".MapPath" : "System.Web.HttpContext.Current.Server.MapPath") + "(\"" + basePath + "" + GetAssemblyName() + ".Reports.dll\"));");
            codeBuilder.AddLine("    zip.AddFile(" + (isNetCore ? _appStartFolder + ".ModuleInitializer_" + api.Name + ".MapPath" : "System.Web.HttpContext.Current.Server.MapPath") + "(\"" + basePath + "" + GetAssemblyName() + ".dll\"));");
            codeBuilder.AddLine("    return Convert.ToBase64String(zip.GetZipBytes());");
            codeBuilder.AddLine("}");
        }

        private void GenDSGetDefinition(WebApiController api, Linx.Tools.CodeBuilder codeBuilder, string contextReference, EntityAdapter entity, bool isNetCore, bool isDataService, bool byEntitySearch = false, bool noAssociations = false, string authorizeAttribute = "", bool byParentComposition = false, bool byQuickSearch = false)
        {
            string methodName = entity.Name + (byQuickSearch ? "QuickSearch" : (byParentComposition ? "ParentComposition" : "") + (byEntitySearch ? "ByEntitySearch" : "") + (noAssociations ? "NoAssociations" : ""));

            var quickSearchProperties = (byQuickSearch ? entity.GetAllInheritanceProperties().Where(e => e.QuickSearchIndex >= 0).OrderBy(e => e.QuickSearchIndex).Select(e => (e.Datatype.ToLower().Contains("string") ? "." : "") + e.Name).ToArray() : new string[] { });

            if (byQuickSearch && quickSearchProperties.Where(e => e.Left(1) == ".").Count() == 0)
                return;

            codeBuilder.AddLine("");
            if (!authorizeAttribute.IsNullOrEmpty())
                codeBuilder.AddLine("[" + authorizeAttribute + "]");
            if (isNetCore)
            {
                if (!byEntitySearch && noAssociations && !byQuickSearch)
                    codeBuilder.AddLine("[HttpGet(\"" + entity.Name + (byParentComposition ? "ParentComposition" : "") + "\")]");
                codeBuilder.AddLine("[HttpGet(\"Get" + methodName + "\")]");
            }
            else
                codeBuilder.AddLine("[Route(\"Get" + methodName + "\"), System.Web.Http.HttpGet()]");
            codeBuilder.AddLine("public IQueryable<" + (byQuickSearch ? "object" : "BusinessNS." + entity.Name + (byParentComposition ? "ParentComposition" : "")) + "> Get" + methodName + "(" + (byQuickSearch ? "string q = \"\", int page = 1, string jExpr = \"\", string propertiesSelection = \"\"" : (byEntitySearch ? "string jEntitySearch" : "")) + ")");
            codeBuilder.AddLine("{");

            if (byQuickSearch)
            {
                codeBuilder.AddLine("    string validProperties = \"" + string.Join(",", quickSearchProperties.Select(e => e.Replace(".", ""))) + "\";");
                codeBuilder.AddLine("    if (!propertiesSelection.IsNullOrEmpty())");
                codeBuilder.AddLine("        validProperties = propertiesSelection.Replace(\" \", \"\");");
                codeBuilder.AddLine("    var validPropertiesList = validProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);");
                codeBuilder.AddLine("    var whereProperties = new string[] { " + string.Join(", ", quickSearchProperties.Where(e => e.Left(1) == ".").Select(e => "\"" + e.Replace(".", "") + "\"")) + " };");

                var whereProperties = quickSearchProperties.Where(e => e.Left(1) == ".").Select(e => e.Replace(".", "")).ToArray();
                if (entity.IsOlap() && whereProperties.Length > 1)
                {
                    codeBuilder.AddLine("    return (");
                    codeBuilder.AddLine("                       from r in ");
                    for (int i = 0; i < whereProperties.Length; i++)
                    {
                        codeBuilder.AddLine("                          " + (i > 0 ? ".Union(" : "") + "this.Get" + entity.Name + "ByEntitySearchNoAssociations(\"" + entity.Name + "{\" + (validPropertiesList.Contains(\"" + whereProperties[i] + "\") ? \"" + whereProperties[i] + "#Like#S\" + q + \"%;\" : \"" + whereProperties[i] + "#==#SY75-@@\") + (jExpr.IsNullOrEmpty() ? \"\" : jExpr) + \"}" + "LinqValidProperties{LinqValidProperties#==#S\" + validProperties + \"}" + "\")" + (i > 0 ? ")" : ""));
                    }
                    codeBuilder.AddLine("                       select new { " + String.Join(", ", quickSearchProperties.Select(e => e.Replace(".", "") + " = r." + e.Replace(".", ""))) + " }");
                    codeBuilder.AddLine("                      ).Distinct().Take(10).Skip((page - 1) * 10);");

                }
                else
                {
                    codeBuilder.AddLine("    var whereCondition = String.Join(\"||#\", whereProperties.Where(f => validPropertiesList.Contains(f)).Select(e => e + \"#Like#S\" + q + \"%;\"));");
                    codeBuilder.AddLine("    var jExpression = \"" + entity.Name + "{(;\" + whereCondition + \")\" + (jExpr.IsNullOrEmpty() ? \"\" : \";&&;\" + jExpr) + \"}" + "LinqValidProperties{LinqValidProperties#==#S\" + validProperties + \"}" + "\";");
                    codeBuilder.AddLine("    return (");
                    codeBuilder.AddLine("                       from r in this.Get" + entity.Name + "ByEntitySearchNoAssociations(jExpression)");
                    codeBuilder.AddLine("                       select new { " + String.Join(", ", quickSearchProperties.Select(e => e.Replace(".", "") + " = r." + e.Replace(".", ""))) + " }");
                    codeBuilder.AddLine("                      ).Distinct()" + (entity.IsOlap() ? "" : ".OrderBy(e => new { " + String.Join(", ", quickSearchProperties.Select(e => "e." + e.Replace(".", ""))) + " })") + ".Take(10).Skip((page - 1) * 10);");
                }
            }
            else
            {
                if (isNetCore && isDataService)
                    codeBuilder.AddLine("    this.HttpContext.AdjustOdataFeature(App_Start.ModuleInitializer_" + api.Name + ".Model, _routePrefix, \"" + entity.Name + (byParentComposition ? "ParentComposition" : "") + "\", App_Start.ModuleInitializer_" + api.Name + ".ServiceProvider);");

                codeBuilder.AddLine("    return " + contextReference + ".Get" + methodName + "(" + (byEntitySearch ? "Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS." + entity.Name + (byParentComposition ? "ParentComposition" : "") + "), jEntitySearch, false, " + entity.IsModelViewSource().ToString().ToLower() + ", " + entity.IsOlap().ToString().ToLower() + "), jEntitySearch" : "") + ")" + ((!entity.PrimaryEntity.IsNullOrEmpty() || entity.EntityAdapterRepresentation != null ? entity.QueryReturnType : EntityQueryReturnType.IEnumerable) == EntityQueryReturnType.IEnumerable ? ".AsQueryable()" : "") + ";");
            }

            codeBuilder.AddLine("}");
        }

        private void GenDSGetByIdDefinition(Linx.Tools.CodeBuilder codeBuilder, string contextReference, EntityAdapter entity, string authorizeAttribute = "")
        {
            string methodName = entity.Name + "ByEntitySearchIdNoAssociations";
            codeBuilder.AddLine("");
            if (!authorizeAttribute.IsNullOrEmpty())
            {
                codeBuilder.AddLine("[" + authorizeAttribute + "]");
            }

            codeBuilder.AddLine("[Route(\"Get" + methodName + "\"), System.Web.Http.HttpGet()]");
            codeBuilder.AddLine("public IQueryable<BusinessNS." + entity.Name + "> Get" + methodName + "(Guid entitySearchId)");
            codeBuilder.AddLine("{");
            codeBuilder.AddLine("    return " + contextReference + ".Get" + methodName + "(entitySearchId).AsQueryable();");
            codeBuilder.AddLine("}");
        }

        private void GenDSAddSearchById(Linx.Tools.CodeBuilder codeBuilder, EntityAdapter entity)
        {
            string methodName = "Add" + entity.Name + "EntitySearchId";
            codeBuilder.AddLine("");
            codeBuilder.AddLine("[Route(\"" + methodName + "\"), System.Web.Http.HttpPost()]");
            codeBuilder.AddLine("public Guid " + methodName + "(string[] jEntitySearch)");
            codeBuilder.AddLine("{");
            codeBuilder.AddLine("    return UserServiceHelper.AddEntySearchToCache(Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS." + entity.Name + "), jEntitySearch[0], false, false, false), jEntitySearch[0]);");
            codeBuilder.AddLine("}");
        }

        private void GenDSGetBmMetaDataPropertyDefinition(Linx.Tools.CodeBuilder codeBuilder, string contextReference, bool isNetCore)
        {
            string methodName = "BmEntityProperties";

            codeBuilder.AddLine("");
            if (isNetCore)
                codeBuilder.AddLine("[HttpGet(\"Get" + methodName + "\")]");
            else
                codeBuilder.AddLine("[Route(\"Get" + methodName + "\"), System.Web.Http.HttpGet()]");
            codeBuilder.AddLine("public List<BmMetaDataProperty> Get" + methodName + "(string entityName, string parentDataPath)");
            codeBuilder.AddLine("{");
            codeBuilder.AddLine("    return " + contextReference + ".Get" + methodName + "(entityName, parentDataPath);");
            codeBuilder.AddLine("}");
        }

        private void GenDSGetSampleDefinition(WebApiController api, Linx.Tools.CodeBuilder codeBuilder, string contextReference, EntityAdapter entity, bool isNetCore, bool isDataService, bool byParentComposition, string authorizeAttribute = "")
        {
            codeBuilder.AddLine("");
            if (!authorizeAttribute.IsNullOrEmpty())
                codeBuilder.AddLine("[" + authorizeAttribute + "]");
            if (isNetCore)
            {
                codeBuilder.AddLine("[HttpGet(\"GetSample" + entity.Name + (byParentComposition ? "ParentComposition" : "") + "\")]");
            }
            else
                codeBuilder.AddLine("[Route(\"GetSample" + entity.Name + (byParentComposition ? "ParentComposition" : "") + "\"), System.Web.Http.HttpGet()]");
            codeBuilder.AddLine("public IQueryable<BusinessNS." + entity.Name + (byParentComposition ? "ParentComposition" : "") + "> GetSample" + entity.Name + (byParentComposition ? "ParentComposition" : "") + "(string details)");
            codeBuilder.AddLine("{");
            codeBuilder.AddLine("    var result = " + contextReference + ".Get" + entity.Name + (byParentComposition ? "ParentComposition" : "") + "ByEntitySearchNoAssociations(null).Take(100).ToList();");
            if (!byParentComposition && entity.HasDetails())
            {
                codeBuilder.AddLine("       if (!details.IsNullOrEmpty())");
                codeBuilder.AddLine("       {");
                codeBuilder.AddLine("           foreach(var entity in result)");
                codeBuilder.AddLine("           {");
                codeBuilder.AddLine("               entity.FillDetails(" + contextReference + ", null, null, details.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries), 5);");
                codeBuilder.AddLine("           }");
                codeBuilder.AddLine("       }");
            }
            if (isNetCore && isDataService)
                codeBuilder.AddLine("    this.HttpContext.AdjustOdataFeature(App_Start.ModuleInitializer_" + api.Name + ".Model, _routePrefix, \"" + entity.Name + (byParentComposition ? "ParentComposition" : "") + "\", App_Start.ModuleInitializer_" + api.Name + ".ServiceProvider);");
            codeBuilder.AddLine("    return result.AsQueryable();");
            codeBuilder.AddLine("}");
        }

        private void GenerateRepositoryCode(StringBuilder codeBuilder, RepositoryImplementation repository, Project repositoryProject)
        {
            string baseIndent = "	";
            EntityDataModel edm = repository.EntityAdapterDesignerRoot.GetEdm();

            codeBuilder.AppendLine("using System;");
            codeBuilder.AppendLine("using System.Collections;");
            codeBuilder.AppendLine("using System.Collections.Generic;");
            codeBuilder.AppendLine("using System.Linq.Expressions;");
            codeBuilder.AppendLine("using Linx.Tools;");
            codeBuilder.AppendLine("using System.Linq;");
            codeBuilder.AppendLine("using System.ComponentModel.Composition;");
            codeBuilder.AppendLine("using " + repository.EntityAdapterDesignerRoot.GetDirectContextNamespace() + ";");

            ProjectItem item = this.GetRepositoryImplementationsItem(repository, repositoryProject);

            codeBuilder.AppendLine();
            codeBuilder.AppendLine("namespace " + repositoryProject.Name + "." + item.Name);
            codeBuilder.AppendLine("{");

            //Class Definition
            codeBuilder.AppendLine(baseIndent + "");
            codeBuilder.AppendLine(baseIndent + "////////////////////////////////////////////////////////////////////////////");
            codeBuilder.AppendLine(baseIndent + "////////////////////////////// Business Repository /////////////////////////");
            codeBuilder.AppendLine(baseIndent + "////////////////////////////////////////////////////////////////////////////");
            codeBuilder.AppendLine(baseIndent + "[Export(typeof(" + repository.RepositoryInterface.Name + "))]");
            codeBuilder.AppendLine(baseIndent + "[ExportMetadata(\"RepositoryName\", \"" + (repository.RepositoryName.IsNullOrEmpty() ? repository.Name : repository.RepositoryName) + "\")]");
            codeBuilder.AppendLine(baseIndent + "public partial class " + repository.Name + " : " + repository.RepositoryInterface.Name);
            codeBuilder.AppendLine(baseIndent + "{");
            codeBuilder.AppendLine(baseIndent + "}");
            //End Class Definition

            //End Namespace
            codeBuilder.AppendLine("}");
        }

        private void GenerateEntityOperationsCode(StringBuilder codeBuilder, EntityAdapter entity, string contextName, bool isShared)
        {
            string baseIndent = "	";
            EntityDataModel edm = entity.GetCurrentDataModel();

            codeBuilder.AppendLine("using System;");
            codeBuilder.AppendLine("using System.Collections;");
            codeBuilder.AppendLine("using System.Collections.Generic;");
            codeBuilder.AppendLine("using System.Linq.Expressions;");
            codeBuilder.AppendLine("using Linx.LinqExtensions.Query;");
            codeBuilder.AppendLine("using Linx.LinqExtensions.Functional;");
            codeBuilder.AppendLine("using Linx.LinqExtensions.Expressions;");
            codeBuilder.AppendLine("using Linx;");
            codeBuilder.AppendLine("using Linx.Tools;");
            codeBuilder.AppendLine("using System.Linq;");
            codeBuilder.AppendLine("using System.ComponentModel;");
            codeBuilder.AppendLine("using System.ComponentModel.DataAnnotations;");
            codeBuilder.AppendLine("using System.ServiceModel.DomainServices.Server;");
            codeBuilder.AppendLine("using Linx.Data;");

            if (isShared)
            {
                codeBuilder.AppendLine("using System.Xml.Serialization;");
            }
            else
            {
                codeBuilder.AppendLine("using System.Text;");
                codeBuilder.AppendLine("using System.Data.Entity.Core.Objects;");
                codeBuilder.AppendLine("using System.Data.Common;");
                codeBuilder.AppendLine("using System.Runtime.Serialization;");
                codeBuilder.AppendLine("using System.ServiceModel;");
                codeBuilder.AppendLine("using System.Data.Linq.SqlClient;");
                codeBuilder.AppendLine("using System.Reflection;");
                codeBuilder.AppendLine("using System.Data.Entity.Core.Objects.DataClasses;");
                if (edm != null)
                    codeBuilder.AppendLine("using " + edm.TargetNamespace + ";");
            }

            codeBuilder.AppendLine();
            codeBuilder.AppendLine("namespace " + entity.EntityAdapterDesignerRoot.TargetNamespace + "." + contextName);
            codeBuilder.AppendLine("{");

            //Class Definition
            codeBuilder.AppendLine(baseIndent + "");
            codeBuilder.AppendLine(baseIndent + "////////////////////////////////////////////////////////////////////////////");
            codeBuilder.AppendLine(baseIndent + "//////////////////////// Business Operations Definition ////////////////////");
            codeBuilder.AppendLine(baseIndent + "////////////////////////////////////////////////////////////////////////////");
            codeBuilder.AppendLine(baseIndent + "public partial class " + entity.Name);
            codeBuilder.AppendLine(baseIndent + "{");
            codeBuilder.AppendLine(baseIndent + "}");
            //End Class Definition

            //End Namespace
            codeBuilder.AppendLine("}");
        }

        #endregion Custom Operations Generation

        #region Custom Validation

        public void CheckCustomValidationClass(Project current)
        {
            ProjectItem item = GetDiagramProjectItem(current);

            if (!item.IsNull())
            {
                foreach (EntityAdapter entity in this.EntityAdapters)
                {
                    if (!entity.CustomValidationMethod.IsNullOrEmpty())
                    {
                        GenerateCustomValidationClass(current);
                        return;
                    }
                    foreach (EntityAdapterAttribute attribute in entity.GetAllAttributes())
                    {
                        if (!attribute.CustomValidationMethod.IsNullOrEmpty())
                        {
                            GenerateCustomValidationClass(current);
                            return;
                        }
                    }
                }
            }
        }

        public void GenerateCustomValidationClass(Project current)
        {
            string outputFile = "";
            ProjectItem item = GetDiagramProjectItem(current);
            StringBuilder codeBuilder;

            if (!item.IsNull())
            {
                string contextNameSpace = Path.GetFileNameWithoutExtension(this.DocumentName);
                outputFile = Path.Combine(GetProjectPath(current), (item.Name == this.DocumentName ? "" : item.Name + "\\") + contextNameSpace + ".CustomValidation.shared.cs");
                if (!ExistsProjectItem(item.ProjectItems, contextNameSpace + ".CustomValidation.shared.cs"))
                {
                    if (!this.VerifySourceControl(outputFile))
                        return;

                    codeBuilder = new StringBuilder();
                    //Create class definition
                    this.GenerateCustomValidationCode(codeBuilder, contextNameSpace);
                    System.IO.File.WriteAllText(outputFile, codeBuilder.ToString());
                    //Add project item.
                    item.ProjectItems.AddFromFile(outputFile);

                }
            }
        }


        public void GenerateAlerts()
        {
            if (IsMainWindowVisible()) return;

            string outputFile = "";

            var dte = this.GetDTE();
            if (dte != null)
            {
                string solutionDir = System.IO.Path.GetDirectoryName(dte.Solution.FullName);
                outputFile = Path.Combine(solutionDir, "Alerts.info");
                if (File.Exists(outputFile))
                {
                    Tools.CodeBuilder codeBuilder = new Tools.CodeBuilder();
                    foreach (var entity in this.EntityAdapters.Where(e => e.EntityAdapterUserInterfaces.Count > 0))
                    {
                        if (entity.EntityAdapterClientEvented.Count > 0)
                            codeBuilder.AddLine("The 'Business View' named '" + entity.Name + "' in '" + this.DocumentName + "' has customizations in client events.");

                        foreach (var ui in entity.EntityAdapterUserInterfaces)
                        {
                            if (ui.HasCustomization)
                                codeBuilder.AddLine("The 'User Interface' named '" + ui.Name + "' in '" + this.DocumentName + "' has customizations in the SPA file '" + ui.Name + "Custom.js'.");

                            if (ui.UserInterfaceClientEvented.Count > 0)
                                codeBuilder.AddLine("The 'User Interface' named '" + ui.Name + "' in '" + this.DocumentName + "' has customizations in client events.");
                        }

                    }

                    if (!codeBuilder.IsNullOrEmpty())
                        System.IO.File.AppendAllText(outputFile, codeBuilder.ToString());
                }
            }
        }

        private void GenerateCustomValidationCode(StringBuilder codeBuilder, string contextName)
        {
            string baseIndent = "	";

            codeBuilder.AppendLine("using System;");
            codeBuilder.AppendLine("using System.Collections;");
            codeBuilder.AppendLine("using System.Collections.Generic;");
            codeBuilder.AppendLine("using System.Linq;");
            codeBuilder.AppendLine("using System.Text;");
            codeBuilder.AppendLine("using Linx.Tools;");
            codeBuilder.AppendLine("using System.ComponentModel;");
            codeBuilder.AppendLine("using System.Runtime.Serialization;");
            codeBuilder.AppendLine("using System.Linq.Expressions;");
            codeBuilder.AppendLine("using System.Reflection;");
            codeBuilder.AppendLine("using System.ComponentModel.DataAnnotations;");
            codeBuilder.AppendLine();
            codeBuilder.AppendLine("namespace " + this.TargetNamespace + "." + contextName);
            codeBuilder.AppendLine("{");

            //Class Definition
            codeBuilder.AppendLine(baseIndent + "");
            codeBuilder.AppendLine(baseIndent + "////////////////////////////////////////////////////////////////////////////");
            codeBuilder.AppendLine(baseIndent + "////////////////////////// CustomValidation Definition /////////////////////");
            codeBuilder.AppendLine(baseIndent + "////////////////////////////////////////////////////////////////////////////");
            codeBuilder.AppendLine(baseIndent + "public partial class " + contextName + "CustomValidation");
            codeBuilder.AppendLine(baseIndent + "{");
            codeBuilder.AppendLine(baseIndent + "}");
            //End Class Definition

            //End Namespace
            codeBuilder.AppendLine("}");
        }

        #endregion Custom Validation

        #region Interfaces

        public string GenerateDomainServiceInterface(string domainService)
        {
            Tools.CodeBuilder codeBuilder = new Tools.CodeBuilder();



            Action<string, string, string> generateMethod = (methodName, entityName, parametersInText) =>
            {
                codeBuilder.AddLine("#region method: " + methodName + entityName + "User");
                codeBuilder.AddLine("public virtual bool Has" + methodName + entityName + "User { get { return false; } }");
                codeBuilder.AddLine("public virtual void " + methodName + entityName + "User(" + parametersInText + ") { throw new NotImplementedException(); }");
                codeBuilder.AddLine("#endregion method: " + methodName + entityName + "User");
            };



            codeBuilder.IncreaseIndent();
            foreach (EntityAdapter entity in this.EntityAdapters)
            {
                codeBuilder.AddLine("#region " + entity.Name);
                generateMethod("OnSavingChanges", entity.Name, domainService + " context, " + entity.Name + " entity, ChangeOperation changeOperation");
                generateMethod("OnSavingContextChanges", entity.Name, domainService + " context, ChangeSetEntry[] entities");
                generateMethod("OnSavedChanges", entity.Name, domainService + " context, " + entity.Name + " entity, ChangeOperation changeOperation");
                generateMethod("OnSavedContextChanges", entity.Name, domainService + " context, ChangeSetEntry[] entities");
                generateMethod("OnTransactedChanges", entity.Name, domainService + " context, " + entity.Name + " entity, ChangeOperation changeOperation");
                generateMethod("OnTransactedContextChanges", entity.Name, domainService + " context, ChangeSetEntry[] entities");
                generateMethod("OnTransactingChanges", entity.Name, domainService + " context, " + entity.Name + " entity, ChangeOperation changeOperation");
                generateMethod("OnTransactingContextChanges", entity.Name, domainService + " context, ChangeSetEntry[] entities");
                generateMethod("OnCleared", entity.Name, entity.Name + " entity");
                generateMethod("OnSearching", entity.Name, "ref " + (!entity.PrimaryEntity.IsNullOrEmpty() || entity.EntityAdapterRepresentation != null ? entity.QueryReturnType.ToString() : "IEnumerable") + "<" + entity.Name + "> searchDefinition, bool noAssociations, List<EntitySearch> searchList");
                codeBuilder.AddLine("#endregion");
                codeBuilder.AddLine();
            }
            codeBuilder.DecreaseIndent();


            return codeBuilder.GetBody();
        }

        public void GenerateDomainServiceInterfaceImplementation(EnvDTE.Project diagramProject)
        {
            if (diagramProject == null)
                return;

            EnvDTE.Project extensionProject = GetProjectByName(diagramProject.Name + ".Extension");
            ProjectItem item = GetDiagramProjectItem(diagramProject);

            if (extensionProject.IsNull() || item.IsNull())
                return;

            string outputFile = Path.Combine(this.GetProjectPath(extensionProject), Path.GetFileNameWithoutExtension(item.Name) + "DomainService.UserExtension.cs");

            if (ExistsProjectItem(extensionProject.ProjectItems, Path.GetFileNameWithoutExtension(item.Name) + "DomainService.UserExtension.cs"))
                return;

            if (!this.VerifySourceControl(outputFile))
                return;

            Tools.CodeBuilder codeBuilder = new Tools.CodeBuilder();

            codeBuilder.AddLine("using System;");
            codeBuilder.AddLine("using System.Collections.Generic;");
            codeBuilder.AddLine("using System.Linq;");
            codeBuilder.AddLine("using System.ServiceModel.DomainServices.Server;");
            codeBuilder.AddLine("using System.ComponentModel.Composition;");
            codeBuilder.AddLine("using Linx.Tools;");
            codeBuilder.AddLine("using " + this.TargetNamespace + "." + Path.GetFileNameWithoutExtension(item.Name) + ";");
            codeBuilder.AddLine("");
            codeBuilder.AddLine("namespace " + this.TargetNamespace + "." + Path.GetFileNameWithoutExtension(item.Name) + ".UserExtension");
            codeBuilder.AddLine("{");
            codeBuilder.IncreaseIndent();

            string domainService = Path.GetFileNameWithoutExtension(item.Name) + "DomainService";
            string interfaceName = this.DomainServiceExtensionName();

            codeBuilder.AddLine("[Export(typeof(" + interfaceName + "))]");
            codeBuilder.AddLine("public class " + domainService + "UserExtension : " + interfaceName);
            codeBuilder.AddLine("{");
            codeBuilder.IncreaseIndent();

            foreach (RepositoryMethod method in RepositoryInterfaces.Where(i => i.IsExtension).FirstOrDefault().RepositoryMethods)
            {
                codeBuilder.AddLine("public override " + method.ReturnType + " " + method.Name + "(" + method.Parameters.Replace("#", ", ") + ")");
                codeBuilder.AddLine("{");

                if (method.ReturnType != "void")
                {
                    codeBuilder.IncreaseIndent();
                    codeBuilder.AddLine("return default(" + method.ReturnType + ");");
                    codeBuilder.DecreaseIndent();
                }
                else
                    codeBuilder.AddLine("");

                codeBuilder.AddLine("}");
                codeBuilder.AddLine("");
            }

            codeBuilder.DecreaseIndent();
            codeBuilder.AddLine("}");
            codeBuilder.DecreaseIndent();
            codeBuilder.AddLine("}");

            System.IO.File.WriteAllText(outputFile, codeBuilder.GetBody());
            //Add project item.
            extensionProject.ProjectItems.AddFromFile(outputFile);
        }

        #endregion

        #region Custom Authorization

        public void GenerateCustomAuthorizationClass(Project current)
        {
            string outputFile = "";
            ProjectItem item = GetDiagramProjectItem(current);
            StringBuilder codeBuilder;

            if (!item.IsNull())
            {
                //Remove inconsistences
                RemoveInconsistentElements(item, ".CustomAuthorization.cs");
                RemoveInconsistentElements(item, ".CustomAuthorizationAuto.cs");
                string contextNameSpace = Path.GetFileNameWithoutExtension(this.DocumentName);

                //Entities
                foreach (EntityAdapter entity in this.EntityAdapters)
                {
                    //Add Automatic Authorization 
                    outputFile = Path.Combine(GetProjectPath(current), (item.Name == this.DocumentName ? "" : item.Name + "\\") + contextNameSpace + "." + entity.Name + ".CustomAuthorizationAuto.cs");

                    if (!this.VerifySourceControl(outputFile))
                        return;

                    //Create class definition
                    codeBuilder = new StringBuilder();
                    this.GenerateCustomAuthorizationAutoCode(codeBuilder, contextNameSpace, entity.Name, entity.GetTopParent().Name);
                    System.IO.File.WriteAllText(outputFile, codeBuilder.ToString());

                    if (!ExistsProjectItem(item.ProjectItems, contextNameSpace + "." + entity.Name + ".CustomAuthorizationAuto.cs"))
                        item.ProjectItems.AddFromFile(outputFile);
                }
            }
        }


        private void RemoveInconsistentElements(ProjectItem item, string rightPart)
        {
            //Remove inconsistent elements of automatic authorization
            List<ProjectItem> elementsForRemove = new List<ProjectItem>();
            string entityName;
            foreach (ProjectItem element in item.ProjectItems)
            {
                if (element.Name.Contains(rightPart))
                {
                    entityName = element.Name.Extract(Path.GetFileNameWithoutExtension(item.Name) + ".", rightPart);
                    if (!entityName.IsNullOrEmpty() && this.EntityAdapters.Where(e => e.Name == entityName).Count() == 0)
                        elementsForRemove.Add(element);
                }
            }

            if (elementsForRemove.Count > 0)
            {
                for (int idxElement = elementsForRemove.Count - 1; idxElement >= 0; idxElement--)
                    elementsForRemove[idxElement].Delete();
            }
            /////////////////////////////////////////
        }


        private void GenerateCustomAuthorizationAutoCode(StringBuilder codeBuilder, string contextName, string entityName, string topParent)
        {
            string baseIndent = "	";
            EntityDataModel edm = this.EntityDataModels.FirstOrDefault();

            if (entityName.IsNullOrEmpty())
                return;

            codeBuilder.AppendLine("using System;");
            codeBuilder.AppendLine("using System.Collections;");
            codeBuilder.AppendLine("using System.Collections.Generic;");
            codeBuilder.AppendLine("using System.Linq;");
            codeBuilder.AppendLine("using System.Text;");
            codeBuilder.AppendLine("using Linx.Data;");
            codeBuilder.AppendLine("using Linx.Tools;");
            codeBuilder.AppendLine("using System.Data.Entity.Core.Objects;");
            codeBuilder.AppendLine("using System.ComponentModel;");
            codeBuilder.AppendLine("using System.Data.Common;");
            codeBuilder.AppendLine("using System.Runtime.Serialization;");
            codeBuilder.AppendLine("using System.ServiceModel;");
            codeBuilder.AppendLine("using System.Linq.Expressions;");
            codeBuilder.AppendLine("using Linx.LinqExtensions.Query;");
            codeBuilder.AppendLine("using Linx.LinqExtensions.Functional;");
            codeBuilder.AppendLine("using Linx.LinqExtensions.Expressions;");
            codeBuilder.AppendLine("using System.Data.Linq.SqlClient;");
            codeBuilder.AppendLine("using System.Reflection;");
            codeBuilder.AppendLine("using System.Data.Entity.Core.Objects.DataClasses;");
            codeBuilder.AppendLine("using System.ComponentModel.DataAnnotations;");
            codeBuilder.AppendLine("using System.ServiceModel.Channels;");
            if (edm != null)
                codeBuilder.AppendLine("using " + edm.TargetNamespace + ";");
            codeBuilder.AppendLine();
            codeBuilder.AppendLine("namespace " + this.TargetNamespace + "." + contextName);
            codeBuilder.AppendLine("{");


            if (this.EnableAutomaticAuthorization)
            {

                //Entity Class Automatic Authorization
                codeBuilder.AppendLine(baseIndent + "");
                codeBuilder.AppendLine(baseIndent + "#region Automatic Authorization");
                codeBuilder.AppendLine(baseIndent + "");
                codeBuilder.AppendLine(baseIndent + "//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////");
                codeBuilder.AppendLine(baseIndent + "////////////////////////////Update CustomAuthorization Definition ////////////////////////////////////////////////////");
                codeBuilder.AppendLine(baseIndent + "//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////");
                codeBuilder.AppendLine(baseIndent + "public partial class " + entityName + "UpdateCustomAuthorizationAutoAttribute : AuthorizationAttribute");
                codeBuilder.AppendLine(baseIndent + "{");
                codeBuilder.AppendLine(baseIndent + "");
                codeBuilder.AppendLine(baseIndent + baseIndent + contextName + "DomainService _domainService = null;");
                codeBuilder.AppendLine(baseIndent + baseIndent + "protected override AuthorizationResult IsAuthorized(System.Security.Principal.IPrincipal principal, AuthorizationContext authorizationContext)");
                codeBuilder.AppendLine(baseIndent + baseIndent + "{");
                codeBuilder.AppendLine(baseIndent + baseIndent + "		if (_domainService == null) _domainService = authorizationContext == null ? null : authorizationContext.GetService(typeof(" + contextName + "DomainService)) as " + contextName + "DomainService;");
                codeBuilder.AppendLine(baseIndent + baseIndent + @"		return (_domainService != null && _domainService.IsSecure) ? AuthorizationResult.Allowed : " + (!this.IsTCS() ? "Linx.Business.Tools.LinxAutorization" : this.TargetNamespace + ".LinxBusinessAutorization") + @".ValidateAuthorization(AuthorizationType.Update, """ + this.TargetNamespace + "#" + this.TargetNamespace + "." + contextName + "#" + this.TargetNamespace + "." + contextName + "." + topParent + @""", (_domainService == null ? ServiceHelper.GetHttpHeaders() : _domainService.Headers));");
                codeBuilder.AppendLine(baseIndent + baseIndent + "}");
                codeBuilder.AppendLine(baseIndent + baseIndent + "");
                codeBuilder.AppendLine(baseIndent + baseIndent + "public AuthorizationResult Authorize(" + contextName + "DomainService domainService = null)");
                codeBuilder.AppendLine(baseIndent + baseIndent + "{");
                codeBuilder.AppendLine(baseIndent + baseIndent + @"		_domainService = domainService;");
                codeBuilder.AppendLine(baseIndent + baseIndent + @"		return IsAuthorized(null, null);");
                codeBuilder.AppendLine(baseIndent + baseIndent + "}");
                codeBuilder.AppendLine(baseIndent + "");
                codeBuilder.AppendLine(baseIndent + "}");
                codeBuilder.AppendLine(baseIndent + "");
                codeBuilder.AppendLine(baseIndent + "");

                codeBuilder.AppendLine(baseIndent + "");
                codeBuilder.AppendLine(baseIndent + "//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////");
                codeBuilder.AppendLine(baseIndent + "////////////////////////////Insert CustomAuthorization Definition ////////////////////////////////////////////////////");
                codeBuilder.AppendLine(baseIndent + "//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////");
                codeBuilder.AppendLine(baseIndent + "public partial class " + entityName + "InsertCustomAuthorizationAutoAttribute : AuthorizationAttribute");
                codeBuilder.AppendLine(baseIndent + "{");
                codeBuilder.AppendLine(baseIndent + "");
                codeBuilder.AppendLine(baseIndent + baseIndent + contextName + "DomainService _domainService = null;");
                codeBuilder.AppendLine(baseIndent + baseIndent + "protected override AuthorizationResult IsAuthorized(System.Security.Principal.IPrincipal principal, AuthorizationContext authorizationContext)");
                codeBuilder.AppendLine(baseIndent + baseIndent + "{");
                codeBuilder.AppendLine(baseIndent + baseIndent + "		if (_domainService == null) _domainService = authorizationContext == null ? null : authorizationContext.GetService(typeof(" + contextName + "DomainService)) as " + contextName + "DomainService;");
                codeBuilder.AppendLine(baseIndent + baseIndent + @"		return (_domainService != null && _domainService.IsSecure) ? AuthorizationResult.Allowed : " + (!this.IsTCS() ? "Linx.Business.Tools.LinxAutorization" : this.TargetNamespace + ".LinxBusinessAutorization") + @".ValidateAuthorization(AuthorizationType.Insert, """ + this.TargetNamespace + "#" + this.TargetNamespace + "." + contextName + "#" + this.TargetNamespace + "." + contextName + "." + topParent + @""", (_domainService == null ? ServiceHelper.GetHttpHeaders() : _domainService.Headers));");
                codeBuilder.AppendLine(baseIndent + baseIndent + "}");
                codeBuilder.AppendLine(baseIndent + baseIndent + "");
                codeBuilder.AppendLine(baseIndent + baseIndent + "public AuthorizationResult Authorize(" + contextName + "DomainService domainService = null)");
                codeBuilder.AppendLine(baseIndent + baseIndent + "{");
                codeBuilder.AppendLine(baseIndent + baseIndent + @"		_domainService = domainService;");
                codeBuilder.AppendLine(baseIndent + baseIndent + @"		return IsAuthorized(null, null);");
                codeBuilder.AppendLine(baseIndent + baseIndent + "}");
                codeBuilder.AppendLine(baseIndent + "");
                codeBuilder.AppendLine(baseIndent + "}");
                codeBuilder.AppendLine(baseIndent + "");
                codeBuilder.AppendLine(baseIndent + "");

                codeBuilder.AppendLine(baseIndent + "");
                codeBuilder.AppendLine(baseIndent + "//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////");
                codeBuilder.AppendLine(baseIndent + "////////////////////////////Delete CustomAuthorization Definition ////////////////////////////////////////////////////");
                codeBuilder.AppendLine(baseIndent + "//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////");
                codeBuilder.AppendLine(baseIndent + "public partial class " + entityName + "DeleteCustomAuthorizationAutoAttribute : AuthorizationAttribute");
                codeBuilder.AppendLine(baseIndent + "{");
                codeBuilder.AppendLine(baseIndent + "");
                codeBuilder.AppendLine(baseIndent + baseIndent + contextName + "DomainService _domainService = null;");
                codeBuilder.AppendLine(baseIndent + baseIndent + "protected override AuthorizationResult IsAuthorized(System.Security.Principal.IPrincipal principal, AuthorizationContext authorizationContext)");
                codeBuilder.AppendLine(baseIndent + baseIndent + "{");
                codeBuilder.AppendLine(baseIndent + baseIndent + "		if (_domainService == null) _domainService = authorizationContext == null ? null : authorizationContext.GetService(typeof(" + contextName + "DomainService)) as " + contextName + "DomainService;");
                codeBuilder.AppendLine(baseIndent + baseIndent + @"		return (_domainService != null && _domainService.IsSecure) ? AuthorizationResult.Allowed : " + (!this.IsTCS() ? "Linx.Business.Tools.LinxAutorization" : this.TargetNamespace + ".LinxBusinessAutorization") + @".ValidateAuthorization(AuthorizationType.Delete, """ + this.TargetNamespace + "#" + this.TargetNamespace + "." + contextName + "#" + this.TargetNamespace + "." + contextName + "." + topParent + @""", (_domainService == null ? ServiceHelper.GetHttpHeaders() : _domainService.Headers));");
                codeBuilder.AppendLine(baseIndent + baseIndent + "}");
                codeBuilder.AppendLine(baseIndent + baseIndent + "");
                codeBuilder.AppendLine(baseIndent + baseIndent + "public AuthorizationResult Authorize(" + contextName + "DomainService domainService = null)");
                codeBuilder.AppendLine(baseIndent + baseIndent + "{");
                codeBuilder.AppendLine(baseIndent + baseIndent + @"		_domainService = domainService;");
                codeBuilder.AppendLine(baseIndent + baseIndent + @"		return IsAuthorized(null, null);");
                codeBuilder.AppendLine(baseIndent + baseIndent + "}");
                codeBuilder.AppendLine(baseIndent + "");
                codeBuilder.AppendLine(baseIndent + "}");
                codeBuilder.AppendLine(baseIndent + "");
                codeBuilder.AppendLine(baseIndent + "");

                codeBuilder.AppendLine(baseIndent + "");
                codeBuilder.AppendLine(baseIndent + "//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////");
                codeBuilder.AppendLine(baseIndent + "////////////////////////////Query CustomAuthorization Definition ////////////////////////////////////////////////////");
                codeBuilder.AppendLine(baseIndent + "//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////");
                codeBuilder.AppendLine(baseIndent + "public partial class " + entityName + "QueryCustomAuthorizationAutoAttribute : AuthorizationAttribute");
                codeBuilder.AppendLine(baseIndent + "{");
                codeBuilder.AppendLine(baseIndent + "");
                codeBuilder.AppendLine(baseIndent + baseIndent + contextName + "DomainService _domainService = null;");
                codeBuilder.AppendLine(baseIndent + baseIndent + "protected override AuthorizationResult IsAuthorized(System.Security.Principal.IPrincipal principal, AuthorizationContext authorizationContext)");
                codeBuilder.AppendLine(baseIndent + baseIndent + "{");
                codeBuilder.AppendLine(baseIndent + baseIndent + "		if (_domainService == null) _domainService = authorizationContext == null ? null : authorizationContext.GetService(typeof(" + contextName + "DomainService)) as " + contextName + "DomainService;");
                codeBuilder.AppendLine(baseIndent + baseIndent + @"		return (_domainService != null && _domainService.IsSecure) ? AuthorizationResult.Allowed : " + (!this.IsTCS() ? "Linx.Business.Tools.LinxAutorization" : this.TargetNamespace + ".LinxBusinessAutorization") + @".ValidateAuthorization(AuthorizationType.Query, """ + this.TargetNamespace + "#" + this.TargetNamespace + "." + contextName + "#" + this.TargetNamespace + "." + contextName + "." + topParent + @""", (_domainService == null ? ServiceHelper.GetHttpHeaders() : _domainService.Headers));");
                codeBuilder.AppendLine(baseIndent + baseIndent + "}");
                codeBuilder.AppendLine(baseIndent + baseIndent + "");
                codeBuilder.AppendLine(baseIndent + baseIndent + "public AuthorizationResult Authorize(" + contextName + "DomainService domainService = null)");
                codeBuilder.AppendLine(baseIndent + baseIndent + "{");
                codeBuilder.AppendLine(baseIndent + baseIndent + @"		_domainService = domainService;");
                codeBuilder.AppendLine(baseIndent + baseIndent + @"		return IsAuthorized(null, null);");
                codeBuilder.AppendLine(baseIndent + baseIndent + "}");
                codeBuilder.AppendLine(baseIndent + "");
                codeBuilder.AppendLine(baseIndent + "}");
                codeBuilder.AppendLine(baseIndent + "");
                codeBuilder.AppendLine(baseIndent + "#endregion Automatic Authorization");
                codeBuilder.AppendLine(baseIndent + "");

            }

            //End Namespace
            codeBuilder.AppendLine("}");
        }


        #endregion Custom Authorization

        #region Open any code element.


        public void OpenCodeElement(object designElement)
        {
            Project diagramProject = this.GetEadProject();
            if (this.IsAspNetCore)
                diagramProject = this.GetEadCoreProject(diagramProject);

            ProjectItem item = GetDiagramProjectItem(diagramProject);
            string className, elementName, fileName, attributes = String.Empty;

            if (!item.IsNull())
            {
                if (designElement is EntityAdapterDesignerRoot)
                {
                    className = Path.GetFileNameWithoutExtension(item.Name) + "DomainService";
                    elementName = String.Empty;
                    fileName = Path.GetFileNameWithoutExtension(item.Name) + ".DomainService";
                    if (EntityAdapterDesignerRoot.ExistsProjectItem(item.ProjectItems, fileName + ".tt"))
                    {
                        item = item.ProjectItems.Item(fileName + ".tt");
                        fileName = fileName + ".cs";
                    }
                    else
                    {
                        MessageBox.Show(String.Format("File [{0}] does not exists!", fileName), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else if (designElement is WebApiController)
                {
                    var api = ((WebApiController)designElement);
                    var webApiProject = (this.IsAspNetCore ? this.GetWebApiCoreProject(api.ProjectSuffix) : this.GetWebApiProject(api.ProjectSuffix));

                    if (webApiProject == null)
                    {
                        MessageBox.Show("This WebAPI project does not exists! Save the designer before this operation.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    attributes = "";

                    item = this.GetWebApiControllersItem(api, webApiProject);
                    className = Path.GetFileNameWithoutExtension(this.GetWebApiClassFile(api, webApiProject));
                    fileName = className + "AutoGen.cs";
                    elementName = String.Empty;
                    if (!EntityAdapterDesignerRoot.ExistsProjectItem(item.ProjectItems, fileName))
                    {
                        MessageBox.Show(String.Format("File [{0}] does not exists! Save the designer before this operation.", fileName), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else if (designElement is RepositoryImplementation)
                {
                    var repProject = this.GetRepositoryProject((RepositoryImplementation)designElement);

                    if (repProject == null)
                    {
                        MessageBox.Show("This repository project does not exists! Save the designer before this operation.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    var repository = (RepositoryImplementation)designElement;
                    attributes = "Export(typeof(" + repository.RepositoryInterface.Name + "))";
                    attributes += "#ExportMetadata(\"RepositoryName\", \"" + (repository.RepositoryName.IsNullOrEmpty() ? repository.Name : repository.RepositoryName) + "\")";

                    item = this.GetRepositoryImplementationsItem(repository, repProject);
                    className = Path.GetFileNameWithoutExtension(this.GetRepositoryClassFile(repository, repProject));
                    fileName = className + ".cs";
                    elementName = String.Empty;
                    if (!EntityAdapterDesignerRoot.ExistsProjectItem(item.ProjectItems, fileName))
                    {
                        MessageBox.Show(String.Format("File [{0}] does not exists! Save the designer before this operation.", fileName), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else if (designElement is Workflow)
                {
                    ProjectItem wfFolder = GetWorkflowFolder();
                    if (!wfFolder.IsNull())
                    {
                        string wfFileName = ((Workflow)designElement).Name + ".xaml";
                        ProjectItem wfItem = this.GetProjectItemByName(wfFolder.ProjectItems, wfFileName);
                        if (!wfItem.IsNull())
                        {
                            Window window = wfItem.Open(EnvDTE.Constants.vsViewKindDesigner);
                            window.SetFocus();
                        }
                    }
                    return;
                }
                else if (designElement is DomainView)
                {
                    Project project = this.GetEadProject();
                    if (project.IsNull())
                        return;

                    if (EntityAdapterDesignerRoot.ExistsProjectItem(project.ProjectItems, "Domains"))
                    {
                        item = project.ProjectItems.Item("Domains");
                        if (EntityAdapterDesignerRoot.ExistsProjectItem(item.ProjectItems, "DomainViews.tt"))
                        {
                            item = item.ProjectItems.Item("DomainViews.tt");
                            fileName = "DomainViews.shared.cs";
                        }
                        else
                        {
                            MessageBox.Show(String.Format("File [{0}] does not exists!", "DomainViews.tt"), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show(String.Format("Folder [{0}] does not exists!", "Domains"), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    className = ((DomainView)designElement).Name;
                    elementName = String.Empty;
                }
                else if (designElement is KeyPerformanceIndicator)
                {
                    Project project = this.GetEadProject();
                    if (project.IsNull())
                        return;

                    if (EntityAdapterDesignerRoot.ExistsProjectItem(project.ProjectItems, "KPIs"))
                    {
                        item = project.ProjectItems.Item("KPIs");
                        if (EntityAdapterDesignerRoot.ExistsProjectItem(item.ProjectItems, "KPIViews.tt"))
                        {
                            item = item.ProjectItems.Item("KPIViews.tt");
                            fileName = "KPIViews.shared.cs";
                        }
                        else
                        {
                            MessageBox.Show(String.Format("File [{0}] does not exists!", "KPIViews.tt"), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show(String.Format("Folder [{0}] does not exists!", "KPIs"), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    className = ((KeyPerformanceIndicator)designElement).Name;
                    elementName = String.Empty;
                }
                else if (designElement is DomainServiceExtension)
                {
                    fileName = Path.GetFileNameWithoutExtension(item.Name) + "." + ((DomainServiceExtension)designElement).Name + ".Operations.cs";
                    className = Path.GetFileNameWithoutExtension(item.Name) + "DomainService";
                    elementName = String.Empty;
                }
                else if (designElement is LookUpAdapter || designElement is LookUpProperty)
                {
                    fileName = Path.GetFileNameWithoutExtension(item.Name) + ".LookUps";
                    if (EntityAdapterDesignerRoot.ExistsProjectItem(item.ProjectItems, fileName + ".tt"))
                    {
                        item = item.ProjectItems.Item(fileName + ".tt");
                        fileName = fileName + ".cs";
                    }
                    else
                    {
                        MessageBox.Show(String.Format("File [{0}] does not exists!", fileName), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (designElement is LookUpAdapter)
                    {
                        className = ((LookUpAdapter)designElement).Name;
                        elementName = String.Empty;
                    }
                    else
                    {
                        className = ((LookUpProperty)designElement).LookUpAdapter.Name;
                        elementName = ((LookUpProperty)designElement).Name;
                    }
                }
                else
                {
                    fileName = Path.GetFileNameWithoutExtension(item.Name) + ".DomainService";
                    if (EntityAdapterDesignerRoot.ExistsProjectItem(item.ProjectItems, fileName + ".tt"))
                    {
                        item = item.ProjectItems.Item(fileName + ".tt");
                        fileName = fileName + ".cs";
                    }
                    else
                    {
                        MessageBox.Show(String.Format("File [{0}] does not exists!", fileName), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (designElement is EntityAdapter)
                    {
                        className = ((EntityAdapter)designElement).Name;
                        elementName = String.Empty;
                    }
                    else if (designElement is RepositoryInterface)
                    {
                        className = ((RepositoryInterface)designElement).Name;
                        elementName = String.Empty;
                    }
                    else if (designElement is EntityAdapterProperty)
                    {
                        className = ((EntityAdapterProperty)designElement).EntityAdapter.Name;
                        elementName = ((EntityAdapterProperty)designElement).Name;
                    }
                    else if (designElement is EntityAdapterPublicationProperty)
                    {
                        className = ((EntityAdapterPublicationProperty)designElement).EntityAdapter.Name;
                        elementName = ((EntityAdapterPublicationProperty)designElement).Name;
                    }
                    else if (designElement is EntityAdapterFormula)
                    {
                        className = ((EntityAdapterFormula)designElement).EntityAdapter.Name;
                        elementName = ((EntityAdapterFormula)designElement).Name;
                    }
                    else if (designElement is EntityInstanceReferencesEntityOwners)
                    {
                        className = ((EntityInstanceReferencesEntityOwners)designElement).TargetEntityAdapter.Name;
                        elementName = ((EntityInstanceReferencesEntityOwners)designElement).Name;
                    }
                    else if (designElement is EntityCollectionReferencesEntityOwners)
                    {
                        className = ((EntityCollectionReferencesEntityOwners)designElement).TargetEntityAdapter.Name;
                        elementName = ((EntityCollectionReferencesEntityOwners)designElement).Name;
                    }
                    else if (designElement is EntityAdapterReferencesTargetEntityAdapter)
                    {
                        className = ((EntityAdapterReferencesTargetEntityAdapter)designElement).TargetEntityAdapter.Name;
                        elementName = ((EntityAdapterReferencesTargetEntityAdapter)designElement).SourceEntityAdapter.Name + "List";
                    }
                    else return;
                }

                if (!OpenCodeMember(item, fileName, className, elementName, attributes))
                    MessageBox.Show(String.Format("File [{0}] does not exists!", fileName), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        public bool OpenCodeMember(ProjectItem item, string fileName, string className, string elementName, string attributes)
        {
            if (EntityAdapterDesignerRoot.ExistsProjectItem(item.ProjectItems, fileName))
            {
                Window window = item.ProjectItems.Item(fileName).Open(EnvDTE.Constants.vsViewKindCode);
                window.SetFocus();
                TextSelection selection = ((TextSelection)item.ProjectItems.Item(fileName).Document.Selection);
                selection.MoveToCodeElement(className, elementName, attributes);
                return true;
            }
            else return false;
        }

        public bool OpenElementName(ProjectItem item, string fileName, string elementName, string attributes)
        {
            if (EntityAdapterDesignerRoot.ExistsProjectItem(item.ProjectItems, fileName))
            {
                Window window = item.ProjectItems.Item(fileName).Open(EnvDTE.Constants.vsViewKindCode);
                window.SetFocus();
                TextSelection selection = ((TextSelection)item.ProjectItems.Item(fileName).Document.Selection);
                selection.MoveToElementName(elementName, attributes);
                return true;
            }
            else return false;
        }


        public void OpenCodeElement(string fileName, string className, string elementName, bool isAspNetCore = false)
        {
            Project diagramProject = this.GetEadProject();
            if (isAspNetCore)
                diagramProject = this.GetEadCoreProject(diagramProject);

            ProjectItem item = GetDiagramProjectItem(diagramProject);

            if (!item.IsNull())
            {
                if (!OpenCodeMember(item, fileName, className, elementName, String.Empty))
                    MessageBox.Show(String.Format("File [{0}] does not exists!", fileName), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        #endregion Open any code element.

        #region Open Operations

        public TextSelection OpenGenericOperation(string fileName, GenericOperation targetOperation, string className)
        {
            return OpenGenericOperation(fileName, targetOperation, className, null);
        }

        public TextSelection OpenGenericOperation(string fileName, GenericOperation targetOperation, string className, ProjectItem item)
        {
            return OpenGenericOperation(fileName, targetOperation, className, item, String.Empty);
        }

        public TextSelection OpenGenericOperation(string fileName, GenericOperation targetOperation, string className, ProjectItem item, String insertCommandText, bool isAspNetCore = false)
        {
            TextSelection selection = null;

            if (targetOperation.OverloadName.IsNullOrEmpty())
            {
                MessageBox.Show("Cannot open the operation because the OverloadName property is empty!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return selection;
            }

            if (item.IsNull())
            {
                Project diagramProject = this.GetEadProject();
                if (isAspNetCore)
                    diagramProject = this.GetEadCoreProject(diagramProject);
                item = GetDiagramProjectItem(diagramProject);
            }

            if (!item.IsNull())
            {
                if (EntityAdapterDesignerRoot.ExistsProjectItem(item.ProjectItems, fileName))
                {
                    Window window = item.ProjectItems.Item(fileName).Open(EnvDTE.Constants.vsViewKindCode);
                    window.SetFocus();
                    selection = ((TextSelection)item.ProjectItems.Item(fileName).Document.Selection);
                    selection.OpenOperation(targetOperation, className, insertCommandText);
                }
                else
                    MessageBox.Show(String.Format("File [{0}] does not exists!", fileName), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return selection;
        }

        public TextSelection OpenCustomValidationOperation(GenericOperation targetOperation, bool isAspNetCore = false)
        {
            Project diagramProject = this.GetEadProject();
            if (isAspNetCore)
                diagramProject = this.GetEadCoreProject(diagramProject);

            ProjectItem item = GetDiagramProjectItem(diagramProject);

            if (!item.IsNull())
                return this.OpenGenericOperation(Path.GetFileNameWithoutExtension(item.Name) + ".CustomValidation.shared.cs", targetOperation, Path.GetFileNameWithoutExtension(item.Name) + "CustomValidation");
            else return null;
        }

        public TextSelection OpenDomainServiceOperation(DomainServiceOperation targetOperation, bool isAspNetCore = false)
        {
            Project diagramProject = this.GetEadProject();
            if (isAspNetCore)
                diagramProject = this.GetEadCoreProject(diagramProject);

            ProjectItem item = GetDiagramProjectItem(diagramProject);

            if (!item.IsNull())
                return this.OpenGenericOperation(Path.GetFileNameWithoutExtension(item.Name) + "." + targetOperation.DomainServiceExtension.Name + ".Operations.cs", targetOperation, Path.GetFileNameWithoutExtension(item.Name) + "DomainService");
            else return null;
        }

        public TextSelection OpenStoreQuery(StoreQuery sq, GenericOperation operation, bool isAspNetCore = false)
        {
            Project diagramProject = this.GetEadProject();
            if (isAspNetCore)
                diagramProject = this.GetEadCoreProject(diagramProject);

            ProjectItem item = GetDiagramProjectItem(diagramProject);
            if (!item.IsNull())
            {
                string fileName = Path.GetFileNameWithoutExtension(item.Name) + "." + "DomainService";
                if (ExistsProjectItem(item.ProjectItems, fileName + ".tt"))
                    item = GetProjectItemByName(item.ProjectItems, fileName + ".tt");

                return this.OpenGenericOperation(fileName + ".cs", operation, sq.StoreScript.EntityAdapterDesignerRoot.GetDomainServiceName(), item);
            }
            else return null;
        }

        public TextSelection OpenRepositoryMethod(RepositoryMethod targetOperation)
        {
            RepositoryImplementation repository;
            if (((RepositoryMethod)targetOperation).RepositoryInterface.RepositoryImplementations.Count == 1)
                repository = ((RepositoryMethod)targetOperation).RepositoryInterface.RepositoryImplementations.First();
            else
                repository = ((RepositoryMethod)targetOperation).RepositoryInterface.RepositoryImplementations.Where(e => e.HasFocus).FirstOrDefault();

            if (repository == null)
            {
                MessageBox.Show("Select an implementation before this action!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }

            var repProject = this.GetRepositoryProject(repository);
            if (!repProject.IsNull())
            {
                string classFile = Path.GetFileName(this.GetRepositoryClassFile(repository, repProject));
                ProjectItem item = this.GetRepositoryImplementationsItem(repository, repProject);
                return this.OpenGenericOperation(classFile, targetOperation, Path.GetFileNameWithoutExtension(classFile), item);
            }
            else return null;
        }

        public TextSelection OpenWebApiAction(WebApiAction targetOperation)
        {
            var api = ((WebApiAction)targetOperation).WebApiController;
            var webApiProject = (this.IsAspNetCore ? this.GetWebApiCoreProject(api.ProjectSuffix) : this.GetWebApiProject(api.ProjectSuffix));

            if (!webApiProject.IsNull())
            {
                string classFile = Path.GetFileName(this.GetWebApiClassFile(api, webApiProject));
                ProjectItem item = this.GetWebApiControllersItem(api, webApiProject);
                return this.OpenGenericOperation(classFile, targetOperation, Path.GetFileNameWithoutExtension(classFile), item);
            }
            else return null;
        }

        public TextSelection OpenEntityOperation(EntityAdapterOperation targetOperation)
        {
            Project diagramProject = this.GetEadProject();
            if (this.IsAspNetCore)
                diagramProject = this.GetEadCoreProject(diagramProject);
            ProjectItem item = GetDiagramProjectItem(diagramProject);

            if (!item.IsNull())
                return this.OpenGenericOperation(Path.GetFileNameWithoutExtension(item.Name) + "." + targetOperation.EntityAdapter.Name + ".Operations" + (targetOperation.IsShared ? ".shared" : "") + ".cs", targetOperation, targetOperation.EntityAdapter.Name);
            else return null;
        }

        public TextSelection OpenEntityEvent(EntityAdapterEvent targetEvent)
        {
            Project diagramProject = this.GetEadProject();
            if (this.IsAspNetCore)
                diagramProject = this.GetEadCoreProject(diagramProject);

            ProjectItem item = GetDiagramProjectItem(diagramProject);

            if (!item.IsNull())
                return this.OpenGenericOperation(Path.GetFileNameWithoutExtension(item.Name) + "." + targetEvent.EntityAdapter.Name + ".Events" + (targetEvent.IsShared ? ".shared" : "") + ".cs", targetEvent, targetEvent.EntityAdapter.Name);
            else return null;
        }

        public void OpenClientEntityEvent(EntityAdapterClientEvent targetEvent)
        {
            if (targetEvent.EntityAdapter.ClientLocalServices.Count > 0)
            {
                ProjectItem item = this.MobileCodeGen.GetMobileAppServiceFolder();
                if (!item.IsNull())
                {
                    string factoryName = this.MobileCodeGen.GetMobileDataServiceApiName();
                    this.OpenElementName(item, factoryName + ".js", "var " + targetEvent.Name + " ", String.Empty);
                }
            }
            else
            {
                ProjectItem item = this.SpaCodeGen.GetSpaAppFolder("services");
                if (!item.IsNull())
                {
                    this.OpenElementName(item, this.GetContextName() + "Context.js", "ownerReference." + targetEvent.Name + " ", String.Empty);
                }
            }
        }

        public void OpenClientUiEvent(UserInterfaceClientEvent targetEvent)
        {
            if (targetEvent.EntityAdapterUserInterface.VisualType == InterfaceType.Web)
            {
                ProjectItem item = this.SpaCodeGen.GetSpaAppFolder("viewmodels");
                if (!item.IsNull())
                {
                    this.OpenElementName(item, targetEvent.EntityAdapterUserInterface.Name + ".js", "var " + targetEvent.Name + " ", String.Empty);
                }
            }
            else
            {
                ProjectItem item = this.MobileCodeGen.GetMobileAppControllerFolder();
                if (!item.IsNull())
                {
                    string controllerName = this.MobileCodeGen.GetMobileControllerName(targetEvent.EntityAdapterUserInterface);
                    this.OpenElementName(item, controllerName + ".js", "var " + targetEvent.Name + " ", String.Empty);
                }
            }
        }

        public void OpenClientServiceEvent(ServiceClientEvent targetEvent)
        {
            ProjectItem item = this.MobileCodeGen.GetMobileAppFactoryFolder();
            if (!item.IsNull())
            {
                string serviceName = this.MobileCodeGen.GetMobileDataFactoryName(targetEvent.ClientLocalService, true);
                this.OpenElementName(item, serviceName + ".js", "var " + targetEvent.Name + " ", String.Empty);
            }
        }

        public void OpenClientLocalServiceFile(ClientLocalService service)
        {
            ProjectItem item = this.MobileCodeGen.GetMobileAppFactoryFolder();
            if (!item.IsNull())
            {
                string serviceName = this.MobileCodeGen.GetMobileDataFactoryName(service);
                this.OpenElementName(item, serviceName + ".js", "define([", String.Empty);
            }
        }

        #endregion Open Operations


        public Project varrepProject { get; set; }

        public string GetEnvPart()
        {
            return GetDirectorySourcePart();
        }
    }
}
