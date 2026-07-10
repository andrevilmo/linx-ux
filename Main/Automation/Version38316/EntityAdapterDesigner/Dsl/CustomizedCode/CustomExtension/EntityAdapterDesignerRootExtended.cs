using EnvDTE;
using Linx.Builder.Resources;
using Linx.EntityAdapterDesigner.CustomizedCode;
using Linx.EntityAdapterDesigner.CustomizedCode.Apps.SPA;
using Linx.Tools;
using Microsoft.CSharp;
using Microsoft.TeamFoundation.VersionControl.Client;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Diagrams;
using Microsoft.Win32;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Objects.DataClasses;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using VSLangProj80;
using DslModeling = global::Microsoft.VisualStudio.Modeling;



namespace Linx.EntityAdapterDesigner
{

    public partial class EntityAdapterDesignerRoot
    {
        private SpaCodeGen _spaCodeGen;
        public SpaCodeGen SpaCodeGen { get { if (_spaCodeGen == null) _spaCodeGen = new SpaCodeGen(this); return _spaCodeGen; } }
       
        public CustomizedCode.PublicationStructure PublisherAutoReference { get; set; }

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

            if (this.RefreshIdentityKeysAfterSave)
            {
                result += "\r\n ";
                result += "\r\n " + indent + "//Replace keys from source";
                result += "\r\n " + indent + "foreach (var entityChange in entityChanges)";
                result += "\r\n " + indent + "    entityChange.RefreshKeys();";
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
                MessageBox.Show(excp.GetCompleteMessage(), "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        public static bool IsAutomaticSaving { get; set; }
        /// <summary>
        /// Save all EADs for updating all automatic code.
        /// </summary>
        public void SaveAllDocuments()
        {
            var eadProject = this.GetEadProject();
            if (eadProject != null)
            {
                CleanServiceBus();
                IsAutomaticSaving = true;
                try
                {
                    foreach (ProjectItem item in eadProject.ProjectItems)
                    {
                        if (Path.GetExtension(item.Name).ToLower() == ".ead")
                        {
                            Window window = item.Open(EnvDTE.Constants.vsViewKindDesigner);
                            window.SetFocus();
                            window.Document.Save();
                            if (item.Name != this.DocumentName)
                                window.Close();
                        }
                    }
                }
                catch (Exception excp)
                {
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

        public string GetSoapServiceHelp(string contextName, string indent)
        {
            string svcName = this.GetSvcFile(contextName), contextNameSpace = this.GetServiceNameSpace();
            string firstProp, lastProp, serviceBus = "/" + svcName + "/", comments = String.Empty, pairValues = String.Empty;

            string body = String.Empty;
            body += "\r\n" + indent + "<!DOCTYPE html>";
            body += "\r\n" + indent + "<html>";
            body += "\r\n" + indent + "<body>";

            body += "\r\n" + indent + "<h2 style=\"color: blue;\">";
            body += "\r\n" + indent + "Linx Service Information: Web Reference (" + contextName + ")";
            comments = System.Web.HttpUtility.HtmlEncode(this.GetComments());
            if (!comments.IsNullOrEmpty())
            {
                body += "\r\n" + indent + "<div>";
                body += "\r\n" + indent + "     Comments: " + comments;
                body += "\r\n" + indent + "</div>";
            }
            body += "\r\n" + indent + "</h2>";

            body += "\r\n" + indent + "<h4 style=\"color: green;\">";
            body += "\r\n" + indent + indent + "Important Tip: Add Linx.Tools.dll as a reference to the project.";
            body += "\r\n" + indent + "</h4>";

            body += "\r\n" + indent + "<h4>";
            body += "\r\n" + indent + indent + "Creating Client Context Instance:";
            body += "\r\n" + indent + "</h4>";
            body += "\r\n" + indent + "<div>";
            body += "\r\n" + indent + indent + contextName + "soapClient context = new " + contextName + "soapClient();";
            body += "\r\n" + indent + "</div>";


            body += "\r\n" + indent + "<h4>";
            body += "\r\n" + indent + indent + "Authentication:";
            body += "\r\n" + indent + "</h4>";
            body += "\r\n" + indent + "<div>";
            body += "\r\n" + indent + indent + "Linx.Tools.WebClientHelper.AuthenticateUser(context, serviceBusAddress, userName, password, applicationId);";
            body += "\r\n" + indent + "</div>";

            body += "\r\n" + indent + "<h3>";
            body += "\r\n" + indent + "Business Entities:";
            body += "\r\n" + indent + "</h3>";

            foreach (var entity in this.EntityAdapters.OrderBy(e => e.Name))
            {
                var props = entity.GetAllInheritanceAttributes().Where(e => e.Datatype.ToLower().Contains("int") || e.Datatype.ToLower().Contains("decimal") || e.Datatype.ToLower().Contains("float") || e.Datatype.ToLower().Contains("byte") || e.Datatype.ToLower().Contains("double"));
                if (props.Count() > 0)
                {
                    firstProp = props.FirstOrDefault().Name;
                    lastProp = props.LastOrDefault().Name;
                }
                else
                {
                    firstProp = "Price";
                    lastProp = "TotalValue";
                }
                body += "\r\n" + indent + "<h4>";
                body += "\r\n" + indent + indent + "Entity Name: " + entity.Name + (!entity.DisplayName.IsNullOrEmpty() ? " [Display=" + System.Web.HttpUtility.HtmlEncode(entity.DisplayName) + "]" : "");
                if (entity.TargetEntityAdapter != null)
                {
                    body += "\r\n" + indent + "<div>";
                    body += "\r\n" + indent + indent + "Parent Entity Name: " + entity.TargetEntityAdapter.Name;
                    body += "\r\n" + indent + "</div>";
                }

                comments = System.Web.HttpUtility.HtmlEncode(entity.GetComments());
                if (!comments.IsNullOrEmpty())
                {
                    body += "\r\n" + indent + "<div>";
                    body += "\r\n" + indent + "     Comments: " + comments;
                    body += "\r\n" + indent + "</div>";
                }

                body += "\r\n" + indent + "</h4>";
                body += "\r\n" + indent + "<div>";
                body += "\r\n" + indent + indent + "<b>Properties:</b>";
                body += "\r\n" + indent + "</div>";
                foreach (var attrib in entity.GetAllInheritanceAttributes().OrderBy(e => e.Name))
                {
                    body += "\r\n" + indent + "<div>";
                    body += "\r\n" + indent + "     " + indent + attrib.Name + " (" + attrib.Datatype + ")" + (!attrib.DisplayName.IsNullOrEmpty() && attrib.DisplayName != attrib.Name ? " [Display=" + System.Web.HttpUtility.HtmlEncode(attrib.DisplayName) + "]" : "");
                    body += "\r\n" + indent + "</div>";
                }

                body += "\r\n" + indent + "<h4>";
                body += "\r\n" + indent + indent + "For searching:";
                body += "\r\n" + indent + "</h4>";
                body += "\r\n" + indent + "<div style=\"color: green;\">";
                body += "\r\n" + indent + indent + System.Web.HttpUtility.HtmlEncode("       //Defining filter");
                body += "\r\n" + indent + "</div>";
                body += "\r\n" + indent + "<div>";
                body += "\r\n" + indent + indent + System.Web.HttpUtility.HtmlEncode("       List<EntitySearch> filter = new List<EntitySearch>();");
                body += "\r\n" + indent + "</div>";
                body += "\r\n" + indent + "<div>";
                body += "\r\n" + indent + indent + "       filter.Add(new EntitySearch());";
                body += "\r\n" + indent + "</div>";
                body += "\r\n" + indent + "<div>";
                body += "\r\n" + indent + indent + "       filter[0].Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, \"" + firstProp + "\"));";
                body += "\r\n" + indent + "</div>";
                body += "\r\n" + indent + "<div>";
                body += "\r\n" + indent + indent + "       filter[0].Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, \"==\"));";
                body += "\r\n" + indent + "</div>";
                body += "\r\n" + indent + "<div>";
                body += "\r\n" + indent + indent + "       filter[0].Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, 12345));";
                body += "\r\n" + indent + "</div>";
                body += "\r\n" + indent + "<div style=\"color: green;\">";
                body += "\r\n" + indent + indent + System.Web.HttpUtility.HtmlEncode("       //Searching by filter");
                body += "\r\n" + indent + "</div>";
                body += "\r\n" + indent + "<div>";
                body += "\r\n" + indent + indent + System.Web.HttpUtility.HtmlEncode("       var result = context.Get" + entity.Name + "ByEntitySearchNoAssociations(SerializationManager<List<EntitySearch>>.ObjectToString(filter));");
                body += "\r\n" + indent + "</div>";

                body += "\r\n" + indent + "<h4>";
                body += "\r\n" + indent + indent + "For submiting changes:";
                body += "\r\n" + indent + "</h4>";
                body += "\r\n" + indent + "<div style=\"color: green;\">";
                body += "\r\n" + indent + indent + System.Web.HttpUtility.HtmlEncode("       //Defining EntitySet for inserting/updating/deleting");
                body += "\r\n" + indent + "</div>";
                body += "\r\n" + indent + "<div>";
                body += "\r\n" + indent + indent + System.Web.HttpUtility.HtmlEncode("       List<ChangeSetEntry> changes = new List<ChangeSetEntry>();");
                body += "\r\n" + indent + "</div>";
                body += "\r\n" + indent + "<div style=\"color: green;\">";
                body += "\r\n" + indent + indent + System.Web.HttpUtility.HtmlEncode("       //Insert Operation");
                body += "\r\n" + indent + "</div>";
                body += "\r\n" + indent + "<div>";
                body += "\r\n" + indent + indent + "       " + entity.Name + " insertEntity = new " + entity.Name + "() { Property1=value1, Property2=value2, ... , PropertyN=ValueN };";
                body += "\r\n" + indent + "</div>";
                body += "\r\n" + indent + "<div>";
                body += "\r\n" + indent + indent + "       changes.Add(new ChangeSetEntry() { Entity = insertEntity, Id = 0, Operation = DomainOperation.Insert, HasMemberChanges = true });";
                body += "\r\n" + indent + "</div>";
                body += "\r\n" + indent + "<div style=\"color: green;\">";
                body += "\r\n" + indent + indent + System.Web.HttpUtility.HtmlEncode("       //Update Operation");
                body += "\r\n" + indent + "</div>";
                body += "\r\n" + indent + "<div>";
                body += "\r\n" + indent + indent + "       changes.Add(new ChangeSetEntry() { Entity = updateEntity, OriginalEntity = originalEntity, Id = 1, Operation = DomainOperation.Update, HasMemberChanges = true });";
                body += "\r\n" + indent + "</div>";
                body += "\r\n" + indent + "<div style=\"color: green;\">";
                body += "\r\n" + indent + indent + System.Web.HttpUtility.HtmlEncode("       //Delete Operation");
                body += "\r\n" + indent + "</div>";
                body += "\r\n" + indent + "<div>";
                body += "\r\n" + indent + indent + "       changes.Add(new ChangeSetEntry() { Entity = deleteEntity, Id = 2, Operation = DomainOperation.Delete });";
                body += "\r\n" + indent + "</div>";
                body += "\r\n" + indent + "<div style=\"color: green;\">";
                body += "\r\n" + indent + indent + System.Web.HttpUtility.HtmlEncode("       //Saving Insert/Update/Delete and returning the entities with identity keys for example");
                body += "\r\n" + indent + "</div>";
                body += "\r\n" + indent + "<div>";
                body += "\r\n" + indent + indent + "       var resultEntries = SubmitChanges(changes.ToArray());";
                body += "\r\n" + indent + "</div>";


            }


            body += "\r\n" + indent + "<h3>";
            body += "\r\n" + indent + "Business LookUps:";
            body += "\r\n" + indent + "</h3>";

            foreach (var lookUp in this.LookUpAdapters.OrderBy(e => e.Name))
            {
                var props = lookUp.GetAllInheritanceAttributes().Where(e => e.Datatype.ToLower().Contains("int") || e.Datatype.ToLower().Contains("decimal") || e.Datatype.ToLower().Contains("float") || e.Datatype.ToLower().Contains("byte") || e.Datatype.ToLower().Contains("double"));
                if (props.Count() > 0)
                {
                    firstProp = props.FirstOrDefault().Name;
                    lastProp = props.LastOrDefault().Name;
                }
                else
                {
                    firstProp = "Price";
                    lastProp = "TotalValue";
                }
                body += "\r\n" + indent + "<h4>";
                body += "\r\n" + indent + indent + "LookUp Name: " + lookUp.Name + (!lookUp.DisplayName.IsNullOrEmpty() ? " [Display=" + System.Web.HttpUtility.HtmlEncode(lookUp.DisplayName) + "]" : "");
                body += "\r\n" + indent + "</h4>";
                body += "\r\n" + indent + "<div>";
                body += "\r\n" + indent + indent + "<b>Properties:</b>";
                body += "\r\n" + indent + "</div>";
                foreach (var attrib in lookUp.GetAllInheritanceAttributes().OrderBy(e => e.Name))
                {
                    body += "\r\n" + indent + "<div>";
                    body += "\r\n" + indent + "     " + indent + attrib.Name + " (" + attrib.Datatype + ")" + (!attrib.DisplayName.IsNullOrEmpty() && attrib.DisplayName != attrib.Name ? " [Display=" + System.Web.HttpUtility.HtmlEncode(attrib.DisplayName) + "]" : "");
                    body += "\r\n" + indent + "</div>";
                }

                body += "\r\n" + indent + "<h4>";
                body += "\r\n" + indent + indent + "For searching:";
                body += "\r\n" + indent + "</h4>";
                body += "\r\n" + indent + "<div style=\"color: green;\">";
                body += "\r\n" + indent + indent + System.Web.HttpUtility.HtmlEncode("       //Defining filter");
                body += "\r\n" + indent + "</div>";
                body += "\r\n" + indent + "<div>";
                body += "\r\n" + indent + indent + System.Web.HttpUtility.HtmlEncode("       List<EntitySearch> filter = new List<EntitySearch>();");
                body += "\r\n" + indent + "</div>";
                body += "\r\n" + indent + "<div>";
                body += "\r\n" + indent + indent + "       filter.Add(new EntitySearch());";
                body += "\r\n" + indent + "</div>";
                body += "\r\n" + indent + "<div>";
                body += "\r\n" + indent + indent + "       filter[0].Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Field, \"" + firstProp + "\"));";
                body += "\r\n" + indent + "</div>";
                body += "\r\n" + indent + "<div>";
                body += "\r\n" + indent + indent + "       filter[0].Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Operator, \"==\"));";
                body += "\r\n" + indent + "</div>";
                body += "\r\n" + indent + "<div>";
                body += "\r\n" + indent + indent + "       filter[0].Expressions.Add(new EntitySearchExpression(EntitySearchExpressionName.Value, 12345));";
                body += "\r\n" + indent + "</div>";
                body += "\r\n" + indent + "<div style=\"color: green;\">";
                body += "\r\n" + indent + indent + System.Web.HttpUtility.HtmlEncode("       //Searching by filter");
                body += "\r\n" + indent + "</div>";
                body += "\r\n" + indent + "<div>";
                body += "\r\n" + indent + indent + System.Web.HttpUtility.HtmlEncode("       var result = context.Get" + lookUp.Name + "ByEntitySearch(SerializationManager<List<EntitySearch>>.ObjectToString(filter));");
                body += "\r\n" + indent + "</div>";

            }


            body += "\r\n" + indent + "<h3>";
            body += "\r\n" + indent + "Extension Methods:";
            body += "\r\n" + indent + "</h3>";
            foreach (var extMethods in this.DomainServiceExtensions.OrderBy(e => e.Name))
            {

                body += "\r\n" + indent + "<h4>";
                body += "\r\n" + indent + "Group Name: " + extMethods.Name;
                body += "\r\n" + indent + "<div>";
                body += "\r\n" + indent + indent + "Methods:";
                body += "\r\n" + indent + "</div>";
                body += "\r\n" + indent + "</h4>";

                foreach (var method in extMethods.DomainServiceOperations.OrderBy(e => e.Name))
                {
                    body += "\r\n" + indent + "<div>";
                    body += "\r\n" + indent + indent + "<b>Name:</b> " + method.Name;
                    body += "\r\n" + indent + "</div>";
                }

            }

            body += "\r\n" + indent + "</body>";
            body += "\r\n" + indent + "</html>";


            return body; ;
        }

        public string GetJsonServiceHelp(string contextName, string indent)
        {
            string svcName = this.GetSvcFile(contextName), contextNameSpace = this.GetServiceNameSpace();
            string getInfo, firstProp, lastProp, serviceBus = "/" + svcName + "/json/", comments = String.Empty, pairValues = String.Empty, jsonGetParams, postParams;

            string body = String.Empty;
            body += "\r\n" + indent + "<!DOCTYPE html>";
            body += "\r\n" + indent + "<html>";
            body += "\r\n" + indent + "<body>";

            body += "\r\n" + indent + "<h2 style=\"color: blue;\">";
            body += "\r\n" + indent + "Linx Service Information: JSON (" + contextName + ")";
            comments = System.Web.HttpUtility.HtmlEncode(this.GetComments());
            if (!comments.IsNullOrEmpty())
            {
                body += "\r\n" + indent + "<div>";
                body += "\r\n" + indent + "     Comments: " + comments;
                body += "\r\n" + indent + "</div>";
            }
            body += "\r\n" + indent + "</h2>";


            body += "\r\n" + indent + "<h4 style=\"color: green;\">";
            body += "\r\n" + indent + indent + "Important Tip: This document can be used with Linx.Tools.WebClientHelper library for .Net environment in this way.";
            body += "\r\n" + indent + "</h4>";
            body += "\r\n" + indent + "<div style=\"color: green;\">";
            body += "\r\n" + indent + indent + System.Web.HttpUtility.HtmlEncode("       /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////");
            body += "\r\n" + indent + "</div>";
            body += "\r\n" + indent + "<div style=\"color: green;\">";
            body += "\r\n" + indent + indent + System.Web.HttpUtility.HtmlEncode("       //Authentication");
            body += "\r\n" + indent + "</div>";
            body += "\r\n" + indent + "<div>";
            body += "\r\n" + indent + indent + "Linx.Tools.WebClientHelper.AuthenticateUser(serviceBusAddress, userName, password, applicationId);";
            body += "\r\n" + indent + "</div>";

            body += "\r\n" + indent + "<div style=\"color: green;\">";
            body += "\r\n" + indent + indent + System.Web.HttpUtility.HtmlEncode("       //GET");
            body += "\r\n" + indent + "</div>";
            body += "\r\n" + indent + "<div>";
            body += "\r\n" + indent + indent + "Linx.Tools.WebClientHelper.Get(uriAddress);";
            body += "\r\n" + indent + "</div>";

            body += "\r\n" + indent + "<div style=\"color: green;\">";
            body += "\r\n" + indent + indent + System.Web.HttpUtility.HtmlEncode("       //POST");
            body += "\r\n" + indent + "</div>";
            body += "\r\n" + indent + "<div>";
            body += "\r\n" + indent + indent + "Linx.Tools.WebClientHelper.Post(uriAddress, data);";
            body += "\r\n" + indent + "</div>";
            body += "\r\n" + indent + "<div style=\"color: green;\">";
            body += "\r\n" + indent + indent + System.Web.HttpUtility.HtmlEncode("       /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////");
            body += "\r\n" + indent + "</div>";


            body += "\r\n" + indent + "<h4>";
            body += "\r\n" + indent + indent + "Authentication:";
            body += "\r\n" + indent + "</h4>";
            body += "\r\n" + indent + "<div>";
            body += "\r\n" + indent + indent + "<b>GET:</b> /Linx-Framework-BV-Autorizacao-AutorizacaoDomainService.svc/json/AuthenticateJson?userName=&password=&applicationId=";
            body += "\r\n" + indent + "</div>";
            body += "\r\n" + indent + "<div>";
            body += "\r\n" + indent + indent + "<b>The result means:</b> Key1 = CurrentCompany, Key2 = AuthorizationToken, Key3 = CurrentUser, Key4 = AccessGroup, Key5 = EconomicGroup, Key6 = Environment ";
            body += "\r\n" + indent + "</div>";

            body += "\r\n" + indent + "<h4>";
            body += "\r\n" + indent + indent + "Headers:";
            body += "\r\n" + indent + "</h4>";
            body += "\r\n" + indent + "<div>";
            body += "\r\n" + indent + indent + "<b>Content-Type:</b> application/json; charset=utf-8";
            body += "\r\n" + indent + "</div>";
            body += "\r\n" + indent + "<div>";
            body += "\r\n" + indent + indent + "<b>CurrentCompany:</b> Key1";
            body += "\r\n" + indent + "</div>";
            body += "\r\n" + indent + "<div>";
            body += "\r\n" + indent + indent + "<b>AuthorizationToken:</b> Key2";
            body += "\r\n" + indent + "</div>";
            body += "\r\n" + indent + "<div>";
            body += "\r\n" + indent + indent + "<b>CurrentUser:</b> Key3";
            body += "\r\n" + indent + "</div>";
            body += "\r\n" + indent + "<div>";
            body += "\r\n" + indent + indent + "<b>AccessGroup:</b> Key4";
            body += "\r\n" + indent + "</div>";
            body += "\r\n" + indent + "<div>";
            body += "\r\n" + indent + indent + "<b>EconomicGroup:</b> Key5";
            body += "\r\n" + indent + "</div>";
            body += "\r\n" + indent + "<div>";
            body += "\r\n" + indent + indent + "<b>Environment:</b> Key6";
            body += "\r\n" + indent + "</div>";
            body += "\r\n" + indent + "<div>";
            body += "\r\n" + indent + indent + "<b>Application:</b> applicationId";
            body += "\r\n" + indent + "</div>";
            body += "\r\n" + indent + "<h4>";
            body += "\r\n" + indent + indent + "Submit Operation Values:";
            body += "\r\n" + indent + "</h4>";
            body += "\r\n" + indent + "<div>";
            body += "\r\n" + indent + indent + "Insert = 2, Update = 3, Delete = 4";
            body += "\r\n" + indent + "</div>";



            body += "\r\n" + indent + "<h3>";
            body += "\r\n" + indent + "Business Entities:";
            body += "\r\n" + indent + "</h3>";

            foreach (var entity in this.EntityAdapters.OrderBy(e => e.Name))
            {
                var stringProp = entity.GetAllInheritanceAttributes().Where(e => e.Datatype.ToLower().Contains("string")).FirstOrDefault();
                var props = entity.GetAllInheritanceAttributes().Where(e => e.Datatype.ToLower().Contains("int") || e.Datatype.ToLower().Contains("decimal") || e.Datatype.ToLower().Contains("float") || e.Datatype.ToLower().Contains("byte") || e.Datatype.ToLower().Contains("double"));
                if (props.Count() > 0)
                {
                    firstProp = props.FirstOrDefault().Name;
                    lastProp = props.LastOrDefault().Name;
                }
                else
                {
                    firstProp = "Price";
                    lastProp = "TotalValue";
                }
                body += "\r\n" + indent + "<h4>";
                body += "\r\n" + indent + indent + "Entity Name: " + entity.Name + (!entity.DisplayName.IsNullOrEmpty() ? " [Display=" + System.Web.HttpUtility.HtmlEncode(entity.DisplayName) + "]" : "");
                if (entity.TargetEntityAdapter != null)
                {
                    body += "\r\n" + indent + "<div>";
                    body += "\r\n" + indent + indent + "Parent Entity Name: " + entity.TargetEntityAdapter.Name;
                    body += "\r\n" + indent + "</div>";
                }

                comments = System.Web.HttpUtility.HtmlEncode(entity.GetComments());
                if (!comments.IsNullOrEmpty())
                {
                    body += "\r\n" + indent + "<div>";
                    body += "\r\n" + indent + "     Comments: " + comments;
                    body += "\r\n" + indent + "</div>";
                }

                body += "\r\n" + indent + "</h4>";
                body += "\r\n" + indent + "<div>";
                body += "\r\n" + indent + indent + "<b>Properties:</b>";
                body += "\r\n" + indent + "</div>";
                foreach (var attrib in entity.GetAllInheritanceAttributes().OrderBy(e => e.Name))
                {
                    body += "\r\n" + indent + "<div>";
                    body += "\r\n" + indent + "     " + indent + attrib.Name + " (" + attrib.Datatype + ")" + (!attrib.DisplayName.IsNullOrEmpty() && attrib.DisplayName != attrib.Name ? " [Display=" + System.Web.HttpUtility.HtmlEncode(attrib.DisplayName) + "]" : "");
                    body += "\r\n" + indent + "</div>";
                }

                body += "\r\n" + indent + "<h4>";
                body += "\r\n" + indent + indent + "For searching use the GET verb like this:";
                body += "\r\n" + indent + "</h4>";
                body += "\r\n" + indent + "<div>";
                getInfo = serviceBus + "GetJson" + entity.Name + "NoAssociations?$where=" + firstProp + ">500 and " + firstProp + "<2000 or " + firstProp + "=324" + (stringProp == null ? String.Empty : " or " + stringProp.Name + ".Contains(\"abc\") or " + stringProp.Name + ".StartsWith(\"abc\") or " + stringProp.Name + ".EndsWith(\"abc\") or " + stringProp.Name + "=\"abc\"") + "&$orderby=" + firstProp + " asc, " + lastProp + " desc&$skip=0&$take=10&$includeTotalCount=true";
                body += "\r\n" + indent + indent + "       " + getInfo;
                body += "\r\n" + indent + "</div>";

                body += "\r\n" + indent + "<h4>";
                body += "\r\n" + indent + indent + "For submiting changes use the POST verb like this:";
                body += "\r\n" + indent + "</h4>";
                body += "\r\n" + indent + "<div>";
                body += "\r\n" + indent + indent + "       " + serviceBus + "SubmitChanges";
                body += "\r\n" + indent + "</div>";
                body += "\r\n" + indent + "<div>";
                body += "\r\n" + indent + indent + "       <b>Request Payload:</b>";
                body += "\r\n" + indent + "</div>";
                body += "\r\n" + indent + "<div>";
                body += "\r\n" + indent + indent + "       {\"changeSet\": [";
                body += "\r\n" + indent + "</div>";

                pairValues = entity.GetJsonPairValues();

                //Insert entity
                body += "\r\n" + indent + "<div>";
                body += "\r\n" + indent + indent + "       {\"Id\":\"0\",\"Operation\":2,";
                body += "\r\n" + indent + "</div>";
                body += "\r\n" + indent + "<div>";
                body += "\r\n" + indent + indent + "       \"Entity\":{\"__type\":\"" + entity.Name + ":#" + contextNameSpace + "\"," + pairValues + "}},";
                body += "\r\n" + indent + "</div>";

                //Update entity
                body += "\r\n" + indent + "<div>";
                body += "\r\n" + indent + indent + "       {\"Id\":\"1\",\"Operation\":3,";
                body += "\r\n" + indent + "</div>";
                body += "\r\n" + indent + "<div>";
                body += "\r\n" + indent + indent + "       \"Entity\":{\"__type\":\"" + entity.Name + ":#" + contextNameSpace + "\"," + pairValues + "},";
                body += "\r\n" + indent + "</div>";
                body += "\r\n" + indent + "<div>";
                body += "\r\n" + indent + indent + "       \"OriginalEntity\":{\"__type\":\"" + entity.Name + ":#" + contextNameSpace + "\"," + pairValues + "}},";
                body += "\r\n" + indent + "</div>";

                //Delete entity
                body += "\r\n" + indent + "<div>";
                body += "\r\n" + indent + indent + "       {\"Id\":\"2\",\"Operation\":4,";
                body += "\r\n" + indent + "</div>";
                body += "\r\n" + indent + "<div>";
                body += "\r\n" + indent + indent + "       \"Entity\":{\"__type\":\"" + entity.Name + ":#" + contextNameSpace + "\"," + pairValues + "}}";
                body += "\r\n" + indent + "</div>";

                body += "\r\n" + indent + "<div>";
                body += "\r\n" + indent + indent + "       ]}";
                body += "\r\n" + indent + "</div>";

            }


            body += "\r\n" + indent + "<h3>";
            body += "\r\n" + indent + "Business LookUps:";
            body += "\r\n" + indent + "</h3>";

            foreach (var lookUp in this.LookUpAdapters.OrderBy(e => e.Name))
            {
                var props = lookUp.GetAllInheritanceAttributes().Where(e => e.Datatype.ToLower().Contains("int") || e.Datatype.ToLower().Contains("decimal") || e.Datatype.ToLower().Contains("float") || e.Datatype.ToLower().Contains("byte") || e.Datatype.ToLower().Contains("double"));
                if (props.Count() > 0)
                {
                    firstProp = props.FirstOrDefault().Name;
                    lastProp = props.LastOrDefault().Name;
                }
                else
                {
                    firstProp = "Price";
                    lastProp = "TotalValue";
                }
                body += "\r\n" + indent + "<h4>";
                body += "\r\n" + indent + indent + "LookUp Name: " + lookUp.Name + (!lookUp.DisplayName.IsNullOrEmpty() ? " [Display=" + System.Web.HttpUtility.HtmlEncode(lookUp.DisplayName) + "]" : "");
                body += "\r\n" + indent + "</h4>";
                body += "\r\n" + indent + "<div>";
                body += "\r\n" + indent + indent + "<b>Properties:</b>";
                body += "\r\n" + indent + "</div>";
                foreach (var attrib in lookUp.GetAllInheritanceAttributes().OrderBy(e => e.Name))
                {
                    body += "\r\n" + indent + "<div>";
                    body += "\r\n" + indent + "     " + indent + attrib.Name + " (" + attrib.Datatype + ")" + (!attrib.DisplayName.IsNullOrEmpty() && attrib.DisplayName != attrib.Name ? " [Display=" + System.Web.HttpUtility.HtmlEncode(attrib.DisplayName) + "]" : "");
                    body += "\r\n" + indent + "</div>";
                }

                body += "\r\n" + indent + "<h4>";
                body += "\r\n" + indent + indent + "For searching use the GET verb like this:";
                body += "\r\n" + indent + "</h4>";
                body += "\r\n" + indent + "<div>";
                getInfo = serviceBus + "GetAllJson" + lookUp.Name + "?$where=" + firstProp + ">500 and " + firstProp + "<2000 or " + firstProp + "=324&$orderby=" + firstProp + " asc, " + lastProp + " desc&$skip=0&$take=10&$includeTotalCount=true";
                body += "\r\n" + indent + indent + "       " + getInfo;
                body += "\r\n" + indent + "</div>";

            }


            body += "\r\n" + indent + "<h3>";
            body += "\r\n" + indent + "Extension Methods:";
            body += "\r\n" + indent + "</h3>";
            firstProp = "Price";
            lastProp = "TotalValue";
            foreach (var extMethods in this.DomainServiceExtensions.OrderBy(e => e.Name))
            {

                body += "\r\n" + indent + "<h4>";
                body += "\r\n" + indent + "Group Name: " + extMethods.Name;
                body += "\r\n" + indent + "<div>";
                body += "\r\n" + indent + indent + "Methods:";
                body += "\r\n" + indent + "</div>";
                body += "\r\n" + indent + "</h4>";

                foreach (var method in extMethods.DomainServiceOperations.OrderBy(e => e.Name))
                {
                    jsonGetParams = method.GetJsonGetParams();
                    postParams = method.GetJsonPostParams(indent);
                    body += "\r\n" + indent + "<p>";
                    body += "\r\n" + indent + "<div>";
                    body += "\r\n" + indent + indent + "<b>Name:</b> " + method.Name;
                    body += "\r\n" + indent + "</div>";
                    body += "\r\n" + indent + "<div>";
                    body += "\r\n" + indent + indent + "<b>Return Type:</b> " + method.ReturnType.Replace("<", "[").Replace(">", "]");
                    body += "\r\n" + indent + "</div>";
                    body += "\r\n" + indent + "<div>";
                    getInfo = serviceBus + method.Name;
                    if (method.DomainAttribute == DomainAttributeType.Query)
                    {
                        getInfo += "?" + jsonGetParams + (jsonGetParams.IsNullOrEmpty() ? "" : "&") + "$where=" + firstProp + ">500 and " + firstProp + "<2000 or " + firstProp + "=324&$orderby=" + firstProp + " asc, " + lastProp + " desc&$skip=0&$take=10&$includeTotalCount=true";
                    }
                    else if (!jsonGetParams.IsNullOrEmpty())
                    {
                        getInfo += "?" + jsonGetParams;
                    }

                    if (!postParams.IsNullOrEmpty())
                        getInfo = "<b>POST:</b> " + getInfo;
                    else
                        getInfo = "<b>GET:</b> " + getInfo;

                    body += "\r\n" + indent + indent + "       " + getInfo;
                    body += "\r\n" + indent + "</div>";

                    if (!postParams.IsNullOrEmpty())
                        body += postParams;

                    body += "\r\n" + indent + "</p>";
                }

            }

            body += "\r\n" + indent + "</body>";
            body += "\r\n" + indent + "</html>";


            return body;
        }

        public void VerifyPublisherAutoReference()
        {
            if (PublisherAutoReference == null)
            {
                string serviceBusPath = this.GetFullPath("Linx.Web.Service.Bus");
                if (System.IO.Directory.Exists(serviceBusPath))
                {
                    string assemblyFile = Path.Combine(serviceBusPath, "bin\\" + this.TargetNamespace + ".dll");
                    if (System.IO.File.Exists(assemblyFile))
                        PublisherAutoReference = new CustomizedCode.PublicationStructure(assemblyFile);
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
                            result.Add(this.PublisherAutoReference.BusinessObjectPath + "#" + entity.Namespace + "#" + entity.Name + "#" + entity.EdmName + "#" + entity.EdmEntityName + "#" + entity.IsIQueryable.ToString().ToLower() + "#" + entity.IsUpdatable.ToString().ToLower());
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
                    if (Path.GetFileName(this.PublisherAutoReference.BusinessObjectPath).ToLower() == Path.GetFileName(assembly).ToLower() && entity.Namespace == nameSpace && entity.Name == entityName)
                    {
                        return entity;
                    }
                }
            }

            return null;
        }

        #endregion


        #region Commons

        public List<Linx.EntityAdapterDesigner.CustomizedCode.PublicationDomain> GetAllDomains()
        {
            List<Linx.EntityAdapterDesigner.CustomizedCode.PublicationDomain> result = new List<CustomizedCode.PublicationDomain>();

            //Add all domains by current model
            foreach (var domain in this.DomainViews)
            {
                if (result.Where(e => e.ClassName == domain.Name).Count() == 0)
                {
                    Linx.EntityAdapterDesigner.CustomizedCode.PublicationDomain oDomain = new Linx.EntityAdapterDesigner.CustomizedCode.PublicationDomain() { ClassName = domain.Name };

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
                var domains = GetDomainsByBusinessModel(edm.GetTypes());
                if (domains != null && domains.Length > 0)
                    result.AddRange(domains);
            }

            return result;
        }

        public PublicationDomain[] GetDomainsByBusinessModel(Type[] types)
        {
            List<PublicationDomain> domains = new List<PublicationDomain>();
            foreach (var type in types.Where(t => t.IsClass))
            {
                if (type.Namespace != null && type.Namespace.Right(".") == "Domains")
                {
                    PublicationDomain domain = new PublicationDomain() { ClassName = type.Name, NameSpace = String.Empty };
                    string fPoint;
                    //Add properties
                    foreach (var property in type.GetProperties().OrderBy(e => e.Name))
                    {
                        fPoint = (Linx.Tools.ObjectExtension.GetPropertyOfAttributeType(property, typeof(FunctionalPoint), "FunctionName") as string);
                        if (!fPoint.IsNullOrEmpty())
                            domain.Values.Add(new PublicationDomainProperty() { Name = property.Name, Value = (!fPoint.IsNullOrEmpty() ? fPoint.Extract("Value[", "]") : property.Name), DisplayName = (!fPoint.IsNullOrEmpty() ? fPoint.Extract("DisplayName[", "]") : property.Name) });
                    }
                    domains.Add(domain);
                }
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
            var uis = this.EntityAdapterUserInterfaces.Where(e => e.GeneratingType == DomainGeneratingType.AutomaticLayout && (e.Subscription != null || e.GetDirectEntityAdapter() != null)).ToArray();
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
            ProjectItem item = GetDiagramProjectItem();

            if (!item.IsNull())
                contextName = Path.GetFileNameWithoutExtension(item.Name);

            return contextName;
        }

        public string GetOutputLibFile()
        {
            return GetOutputLibFile(this.GetEadProject());
        }

        public string GetOutputLibFile(Project current)
        {
            return Path.Combine(Path.Combine(Path.GetDirectoryName(current.FullName), @"bin\debug"), (string)current.Properties.Item("AssemblyName").Value + ".dll");
        }

        public string GetAssemblyName()
        {
            return GetAssemblyName(this.GetEadProject());
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

        public void UpdateLibReferences(Project project, string libFolder, bool copyLocal, bool remove = false, bool specificVersion = false)
        {
            string[] slFiles = GetLibraryFiles(libFolder);
            foreach (string file in slFiles)
            {
                UpdateReference(project, file, remove, copyLocal, specificVersion);
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


        public void SetPostBuildEventToServiceBus(Project current = null, bool copyBusiness = true, bool copyHelp = true, bool getLocalReferences = true)
        {
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

                    string postBuildEventCommand = GetServiceBusCopyCommands(current, serviceBusPath + @"\bin", getLocalReferences);
                    if (hasSelfHost)
                    {
                        postBuildEventCommand += GetServiceBusCopyCommands(current, selfHost, getLocalReferences);
                    }

                    if (copyBusiness)
                        postBuildEventCommand += @"xcopy ""$(TargetName).dll"" """ + businessObjectsPath + @""" /Y /R" + "\r\n";

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

                    if (current.Properties.Item("PostBuildEvent").Value.ToString().IsNullOrEmpty() || !current.Properties.Item("PostBuildEvent").Value.ToString().Contains(postBuildEventCommand))
                        current.Properties.Item("PostBuildEvent").Value = postBuildEventCommand;

                }
            }
        }

        public string GetServiceBusCopyCommands(Project project, string outputDir, bool getLocalReferences = true)
        {
            string xCopyCommands = @"xcopy ""$(TargetName).dll"" """ + outputDir + @""" /Y /R" + "\r\n";
            if (getLocalReferences)
            {
                VSLangProj.VSProject vsProject = (VSLangProj.VSProject)project.Object;
                foreach (VSLangProj.Reference reference in vsProject.References)
                {
                    if (reference.CopyLocal)
                        xCopyCommands += @"xcopy ""$(TargetDir)" + reference.Name + @".dll"" """ + outputDir + @""" /Y /R" + "\r\n";
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


        public void CheckSubscriptionUI()
        {
            if (this.Subscriptions.Count == 0 && this.IsInPresentationDesigner())
            {
                var bp = GetEadProject();
                if (bp != null)
                {
                    Subscription subscription = new Subscription(this.Store);
                    string fileName = Path.Combine(Path.Combine(this.GetFullPath("Linx.Web.Service.Bus"), "bin"), bp.Properties.Item("AssemblyName").Value.ToString() + ".dll");
                    string publisherName = Path.GetFileNameWithoutExtension(fileName);
                    //Adjust publisher
                    subscription.Name = publisherName;
                    subscription.BusinessObjectPath = fileName;
                    subscription.Title = Path.GetFileNameWithoutExtension(fileName).Replace(".", " ").Replace("_", " ");
                    //Add new publisher
                    this.Subscriptions.Add(subscription);
                }
            }
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

        public void MoveProjectToSolutionFolder(Project project, string folderName)
        {
            var itemFolder = GetProjectByName(folderName);
            if (itemFolder == null)
            {
                ((EnvDTE100.Solution4)project.DTE.Solution).AddSolutionFolder(folderName);
                itemFolder = GetProjectByName(folderName);
            }

            if (!ExistsProjectItem(itemFolder.ProjectItems, project.Name))
            {
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
            }
        }

        public void UpdateUserInterfaceSolution()
        {
            Project diagramProject = this.GetEadProject();

            //Adjust Business Solution Folders            
            if (diagramProject != null)
            {
                MoveProjectToSolutionFolder(diagramProject, "Business Rules");
            }

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
                    if (webApiDesignerFolder != null)
                        webApiDesignerFolder.AddFromFile(System.IO.Path.Combine(webApiDir, webApiProjectName + ".csproj"));
                    else
                        ((EnvDTE100.Solution4)appDTE.Solution).AddFromFile(System.IO.Path.Combine(webApiDir, webApiProjectName + ".csproj"), false);
                }
                else
                {
                    // Get the location of the project templates
                    string templateName = ((EnvDTE100.Solution4)appDTE.Solution).GetProjectTemplate("Class Library", "CSharp");
                    if (webApiDesignerFolder != null)
                        webApiDesignerFolder.AddFromTemplate(templateName, webApiDir, webApiProjectName);
                    else
                        ((EnvDTE100.Solution4)appDTE.Solution).AddFromTemplate(templateName, webApiDir, webApiProjectName, false);

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
            else
            {
                this.MoveProjectToSolutionFolder(webApiProject, folderName);
            }

            this.UpdateVersion(webApiProject);
            this.RemoveReferencesWithoutFile(webApiProject);

            //Upgrade to last framework version
            UpgradeVersion(webApiProject);

            //Add project reference
            AddProjectReference(webApiProject, eadProject, false);

            //Update library references
            this.AddReference(webApiProject, "System.Net.Http.dll");
            this.AddReference(webApiProject, "System.ServiceModel.DomainServices.Server.dll");
            this.RemoveReference(webApiProject, "System.Data.Entity");
            this.AddReference(webApiProject, "System.Web.dll");
            this.AddReference(webApiProject, "System.ComponentModel.DataAnnotations.dll");
            this.AddReference(webApiProject, "Linx.Tools.dll");
            this.AddReference(webApiProject, "Linx.LinqExtensions.dll");
            this.AddReference(webApiProject, "System.ComponentModel.Composition.dll");
            this.UpdateLibReferences(webApiProject, "Linx.Business.Desktop.Tools", false);
            this.UpdateLibReferences(webApiProject, "Linx.WebApi.Library", false, true, true);
            this.UpdateLibReferences(webApiProject, "Linx.Data.Library", false);
            this.UpdateLibReferences(webApiProject, "Linx.CodeFirst.EF", false);
            if (api.IsDataService)
            {
                this.UpdateLibReferences(webApiProject, "Linx.DataService.Library", false, false, true);
                this.UpdateLibReferences(webApiProject, "Linx.LinxDataService.Library", false, true, false);
            }

            //Set PostBuildEvent
            SetPostBuildEventToServiceBus(webApiProject, false, false, false);

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
                    string templateName = ((EnvDTE100.Solution4)appDTE.Solution).GetProjectTemplate("Class Library", "CSharp");
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

            //Upgrade to last framework version
            UpgradeVersion(webApiClientProject);


            //Update library references            
            this.AddReference(webApiClientProject, "System.ComponentModel.DataAnnotations.dll");
            this.AddReference(webApiClientProject, "Linx.Tools.dll");
            this.AddReference(webApiClientProject, "System.Runtime.Serialization.dll");
            this.UpdateLibReferences(webApiClientProject, "Linx.WebApiClient.Library", false, false, true);


            //Set PostBuildEvent
            SetPostBuildEvent(webApiClientProject, "Linx.WebApiClient");
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
                    string templateName = ((EnvDTE100.Solution4)appDTE.Solution).GetProjectTemplate("Class Library", "CSharp");
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

            //Upgrade to last framework version
            UpgradeVersion(repositoryProject);

            //Add project reference
            AddProjectReference(repositoryProject, eadProject, false);

            //Update library references
            this.AddReference(repositoryProject, "System.ComponentModel.Composition.dll");
            this.AddReference(repositoryProject, "Linx.Tools.dll");
            this.AddReference(repositoryProject, "Linx.LinqExtensions.dll");
            this.UpdateLibReferences(repositoryProject, "Linx.Data.Library", false);
            this.UpdateLibReferences(repositoryProject, "Linx.Business.Desktop.Tools", false);
            //Set PostBuildEvent
            SetPostBuildEventToServiceBus(repositoryProject, false, false, false);

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
                    string templateName = ((EnvDTE100.Solution4)appDTE.Solution).GetProjectTemplate("Class Library", "CSharp");
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
            this.RemoveReferencesWithoutFile(project);

            //Upgrade to last framework version
            UpgradeVersion(project);

            //Add assembly references.
            this.UpdateLibReferences(project, "Linx.Data.Library", false);
            this.AddReference(project, "Linx.Tools.dll");
            this.AddReference(project, "Linx.LinqExtensions.dll");
            this.AddReference(project, "System.Core.dll");
            this.RemoveReference(project, "System.Data.Entity");
            this.AddReference(project, "System.Xml.Linq.dll");
            this.AddReference(project, "Microsoft.TeamFoundation.Client.dll");
            this.AddReference(project, "Microsoft.TeamFoundation.VersionControl.Client.dll");
            this.AddReference(project, "Microsoft.TeamFoundation.VersionControl.Common.dll");
            this.AddReference(project, "System.ServiceModel.DomainServices.Server.dll");
            this.AddReference(project, "System.Data.dll");
            this.AddReference(project, "System.Data.DataSetExtensions.dll");
            this.AddReference(project, "System.ServiceModel.dll");

            this.AddReference(project, "System.ComponentModel.Composition.dll");

            ((VSLangProj.VSProject)project.Object).References.AddProject(eadProject);
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
                MessageBox.Show("'EntityAdapterDirectoryInfo.xml' Internal Error: " + excep.Message, "Problem getting physical files", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return fileElements.ToArray();
        }

        public string GetDirectorySourcePart()
        {
            string dirPart = null;
            if (this.DTEReference != null)
            {
                var envs = GetEnvironments();
                dirPart = envs.FirstOrDefault(e => this.DTEReference.Solution.FullName.ToLower().Contains("\\" + e.Trim().ToLower() + "\\"));
            }
            return (dirPart.IsNullOrEmpty() ? "Dev" : dirPart);
        }

        public string GetDirectoryInfo(string directoryName)
        {
            if (this.DTEReference == null)
                return null;

            string dirPart = GetDirectorySourcePart();
            string result = String.Empty;
            string worksapaceMapedpath = ("Linx Framework\\" + dirPart + "\\Binary").GetWorkspaceMappedPath();
            if (!worksapaceMapedpath.IsNullOrEmpty())
            {
                string dirInfoFile = Path.Combine(worksapaceMapedpath, "Linx Framework\\" + dirPart + "\\Binary\\Library\\Common\\Linx\\Information\\EntityAdapterDirectoryInfo.xml");
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
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show(String.Format("Fail reading the file {0}.", dirInfoFile) + exp.Message, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }

            if (result.IsNullOrEmpty())
                MessageBox.Show(String.Format("The DirectoryInfo [{0}] is not found in the environment {1}!", directoryName, dirPart), "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            return result;
        }

        public string GetSpecializedLookupInfo(string sourceEntityName)
        {
            if (this.DTEReference == null)
                return null;

            string dirPart = GetDirectorySourcePart();
            string result = String.Empty;
            string worksapaceMapedpath = ("Linx Framework\\" + dirPart + "\\Binary").GetWorkspaceMappedPath();
            if (!worksapaceMapedpath.IsNullOrEmpty())
            {
                string luInfoFile = Path.Combine(worksapaceMapedpath, "Linx Framework\\" + dirPart + "\\Binary\\Library\\Common\\Linx\\Information\\SpecializedLookupInfo.xml");
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
                        MessageBox.Show(String.Format("Fail reading the file {0}.", luInfoFile) + exp.Message, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }

            return result;
        }

        public string[] GetEnvironments()
        {
            if (this.DTEReference == null)
                return null;

            string[] result = new string[] { };
            string worksapaceMapedpath = ("Linx Framework").GetWorkspaceMappedPath();
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
            System.Reflection.MemberInfo[] members = edm.EdmInfo.GetType(edm.TargetNamespace + "." + entitySetName).GetMembers();
            string entityKeys = String.Empty;

            //Add members
            foreach (PropertyInfo propInfo in members.Where(m => m.MemberType == MemberTypes.Property).Select(m => (PropertyInfo)m))
            {
                if (!(propInfo.Name.InList("EntityKey", "EntityState"))
                   && !propInfo.PropertyType.Name.InList("EntityCollection`1", "EntityReference`1", "ICollection`1")
                   && !(propInfo.PropertyType.BaseType != null && propInfo.PropertyType.BaseType == typeof(EntityObject)))
                {

                    //Get Attributes of field
                    var attributes = propInfo.GetCustomAttributes<EdmScalarPropertyAttribute>(true);

                    foreach (EdmScalarPropertyAttribute attrib in attributes)
                    {
                        if (attrib.EntityKeyProperty == true)
                        {
                            entityKeys += (entityKeys.IsNullOrEmpty() ? String.Empty : ",") + propInfo.Name;
                            break;
                        }
                    }

                    if (propInfo.GetCustomAttribute<KeyAttribute>() != null)
                    {
                        entityKeys += (entityKeys.IsNullOrEmpty() ? String.Empty : ",") + propInfo.Name;

                    }
                }
            }

            return entityKeys;
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

                    if (System.IO.File.Exists(System.IO.Path.Combine(reportDir, reportProjectName + ".csproj")))
                    {
                        ((EnvDTE100.Solution4)appDTE.Solution).AddFromFile(System.IO.Path.Combine(reportDir, reportProjectName + ".csproj"), false);
                    }
                    else
                    {
                        // Get the location of the project templates
                        string templateName = ((EnvDTE100.Solution4)appDTE.Solution).GetProjectTemplate("Class Library", "CSharp");
                        ((EnvDTE100.Solution4)appDTE.Solution).AddFromTemplate(templateName, reportDir, reportProjectName, false);

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
            if (adapter.TargetEntityAdapter == null || adapter.GetTopParent().IsDashboardFilter || byParentComposition)
            {
                method.Statements.Add(new CodeSnippetStatement("            string entitySearchExpression = String.Empty;"));
                method.Statements.Add(new CodeSnippetStatement("            var jEntitySearch = GetFilterExpression(report, typeof(" + string.Format("{0}", adapter.Name + (byParentComposition ? "ParentComposition" : "")) + "), new string[] {" + (filterReplacement ? adapter.GetAllParentNames() : " ") + "});"));
                method.Statements.Add(new CodeSnippetStatement("            if (!jEntitySearch.IsNullOrEmpty())"));
                method.Statements.Add(new CodeSnippetStatement("            {"));
                method.Statements.Add(new CodeSnippetStatement(string.Format("                entitySearchExpression = Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof({0}), jEntitySearch, false, " + adapter.IsModelViewSource().ToString().ToLower() + ");", adapter.Name + (byParentComposition ? "ParentComposition" : ""))));
                method.Statements.Add(new CodeSnippetStatement("            }"));
                method.Statements.Add(new CodeSnippetStatement("            if (!entitySearchExpression.IsNullOrEmpty() || !jEntitySearch.IsNullOrEmpty())"));
                method.Statements.Add(new CodeSnippetStatement(string.Format("              result = this." + contextFldName + ".Get{0}ByEntitySearchNoAssociations(entitySearchExpression, jEntitySearch).ToList();", adapter.Name + (byParentComposition ? "ParentComposition" : ""))));
                method.Statements.Add(new CodeSnippetStatement("            else"));
                method.Statements.Add(new CodeSnippetStatement(string.Format("              result = this." + contextFldName + ".Get{0}ByEntitySearchNoAssociations(null).ToList();", adapter.Name + (byParentComposition ? "ParentComposition" : ""))));
            }
            else
                method.Statements.Add(new CodeSnippetStatement(string.Format("            result = this." + contextFldName + ".Get{0}ByEntitySearchNoAssociations(null).ToList();", adapter.Name + (byParentComposition ? "ParentComposition" : ""))));

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

            if (reportProject == null)
            {
                string reportProjectName = diagramProject.Name + ".Reports";
                reportProject = GetProjectByName(reportProjectName);
            }

            if (reportProject == null)
                return;

            this.UpdateVersion(reportProject);
            this.RemoveReferencesWithoutFile(reportProject);

            //Upgrade to last framework version
            UpgradeVersion(reportProject);

            //Set PostBuildEvent
            SetPostBuildEventToProxyForReports(reportProject);

            //Add assembly references.
            this.UpdateLibReferences(reportProject, "Linx.RSExtension.Library", false, false, true);
            this.UpdateLibReferences(reportProject, "Linx.Data.Library", false);
            this.UpdateLibReferences(reportProject, "Linx.Business.Desktop.Tools", false);
            this.UpdateLibReferences(reportProject, "Linx.WebApiClient.Library", false, true, true);
            this.AddReference(reportProject, "Linx.Tools.dll");
            this.AddReference(reportProject, "Linx.LinqExtensions.dll");
            this.AddReference(reportProject, "System.Core.dll");
            this.RemoveReference(reportProject, "System.Data.Entity");
            this.AddReference(reportProject, "System.Xml.Linq.dll");
            this.AddReference(reportProject, "Microsoft.TeamFoundation.Client.dll");
            this.AddReference(reportProject, "Microsoft.TeamFoundation.VersionControl.Client.dll");
            this.AddReference(reportProject, "Microsoft.TeamFoundation.VersionControl.Common.dll");
            this.AddReference(reportProject, "System.ServiceModel.DomainServices.Hosting.dll");
            this.AddReference(reportProject, "System.ServiceModel.DomainServices.Server.dll");
            this.AddReference(reportProject, "System.Drawing.dll");
            this.AddReference(reportProject, "System.ComponentModel.DataAnnotations.dll");
            this.AddReference(reportProject, "System.Configuration.dll");
            this.AddReference(reportProject, "System.Data.dll");
            this.AddReference(reportProject, "System.Data.DataSetExtensions.dll");
            this.AddReference(reportProject, "System.Data.Linq.dll");
            this.AddReference(reportProject, "System.Runtime.Serialization.dll");
            this.AddReference(reportProject, "System.Security.dll");
            this.AddReference(reportProject, "System.ServiceModel.dll");
            this.AddReference(reportProject, "System.Transactions.dll");
            this.AddReference(reportProject, "WindowsBase.dll");
            this.AddReference(reportProject, "System.Web.dll");

            ((VSLangProj.VSProject)reportProject.Object).References.AddProject(diagramProject);
            if (this.EntityDataModels.Count > 0)
                this.AddReference(reportProject, this.EntityDataModels[0].Path);

            MoveProjectToSolutionFolder(reportProject, "Business Reports");
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

        public VSLangProj.Reference AddReference(string strAssemblyName, bool copyLocal = false, bool specificVersion = false)
        {
            return this.AddReference(this.GetEadProject(), strAssemblyName, copyLocal, specificVersion);
        }

        public VSLangProj.Reference AddReference(Project project, string strAssemblyName, bool copyLocal = false, bool specificVersion = false)
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
            catch { }
            //catch (Exception exeption)
            //{
            //    MessageBox.Show("Cannot add the assembly \"" + strAssemblyName + "\" to the project!\r\nDetails:\r\n" + exeption.Message, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}

            return reference;
        }


        #region Adjust Version

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

        public string GetAsssemblyShared()
        {
            if (this.DTEReference == null)
                return null;
            string dirPart = GetDirectorySourcePart();
            string worksapaceMapedpath = ("Linx Framework\\" + dirPart + "\\Binary").GetWorkspaceMappedPath();
            if (worksapaceMapedpath.IsNullOrEmpty())
                return null;
            else
            {

                string asssemblySharedFile = Path.Combine(worksapaceMapedpath, "Linx Framework\\" + dirPart + "\\Binary\\Library\\Common\\Linx\\Information\\AssemblyInfoShared.cs");
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
            if (!project.IsNull())
            {
                List<VSLangProj.Reference> references = new List<VSLangProj.Reference>();
                VSLangProj.VSProject vsProject = (VSLangProj.VSProject)project.Object;
                foreach (VSLangProj.Reference reference in vsProject.References)
                {
                    if (reference.Path.IsNullOrEmpty() || !File.Exists(reference.Path))
                        references.Add(reference);
                }

                //Delete inconsistents references
                foreach (VSLangProj.Reference reference in references)
                    reference.Remove();
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


        public static void UpgradeVersion(Project project)
        {
            //Upgrade project to new Framework version if necessary        
            if ((((uint)project.Properties.Item("TargetFramework").Value) <= 0x00040000))
            {
                project.Properties.Item("TargetFrameworkMoniker").Value = (new System.Runtime.Versioning.FrameworkName(".NETFramework", new Version(4, 5))).FullName;
            }
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

        public static string ReadResourceContent(string resourcePath)
        {
            string body = String.Empty;
            //Read template file
            using (Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    body = reader.ReadToEnd();
                }
            }

            return body;
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
        #endregion


        public void AdjustDocumentInfo(string modelFileName)
        {
            if (String.IsNullOrEmpty(this.DocumentPath) || this.DocumentPath != System.IO.Path.GetDirectoryName(modelFileName) || String.IsNullOrEmpty(this.DocumentName) || this.DocumentName != System.IO.Path.GetFileName(modelFileName))
            {
                string oldSpaCtxClassName = this.SpaCodeGen.GetSpaContextName();

                using (Transaction transaction =
                            this.Store.TransactionManager.BeginTransaction("Changing DocumentInfo."))
                {
                    this.DocumentPath = System.IO.Path.GetDirectoryName(modelFileName);
                    this.DocumentName = System.IO.Path.GetFileName(modelFileName);
                    transaction.Commit();
                }

                if (oldSpaCtxClassName != this.SpaCodeGen.GetSpaContextName())
                {
                    this.SpaCodeGen.RenameSpaServiceCode(oldSpaCtxClassName);
                }

            }
        }

        private void CheckVersion()
        {
            string version = "1.0.0.2";
            if (this.Version != version)
            {
                using (Transaction transaction =
                           this.Store.TransactionManager.BeginTransaction("Changing Version."))
                {
                    this.Version = version;
                    foreach (EntityAdapter entity in this.EntityAdapters.Where(e => !e.ParentCompositionEnabled).ToList())
                    {
                        entity.ParentCompositionEnabled = true;
                    }
                    transaction.Commit();
                }
            }
        }

        public void AdjustStructuralInfo()
        {
            this.CheckVersion();

            this.CheckWebApiDataServices(this.GetBusinessControllerName());

            if (this.TargetNamespace.IsNullOrEmpty() || String.IsNullOrEmpty(this.SvcFile) || this.SvcFile != this.GetSvcFile())
            {
                using (Transaction transaction =
                            this.Store.TransactionManager.BeginTransaction("Changing StructuralInfo."))
                {
                    this.AdjustNamespace();
                    this.SvcFile = this.GetSvcFile();
                    this.CheckSubscriptionUI();
                    transaction.Commit();
                }
            }
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
        public ProjectItem GetDiagramProjectItem()
        {
            var eadProject = this.GetEadProject();
            if (eadProject.IsNull())
                throw new Exception(string.Format("The document '{0}' is not in the 'Business View Project'.", this.DocumentName));

            return GetProjectItemByName(eadProject, this.DocumentName);
        }

        /// <summary>
        /// Verify file in Source Control.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool VerifySourceControl(string fileName)
        {
            if (File.Exists(fileName))
                return this.GetEadProject().DTE.VerifySourceControl(fileName);

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

        public void UpdateDataEntityFunctionsTemplate()
        {
            string outputFile = "";
            Project current = this.GetEadProject();
            ProjectItem folder = null, projItem;

            if (current != null)
            {
                string folderName = "Includes";
                string body = ReadResourceContent(@"Linx.EntityAdapterDesigner.Templates.DataEntityFunctionsTemplate.txt");

                folder = this.GetProjectItemByName(current, folderName);
                if (folder == null)
                    folder = current.ProjectItems.AddFolder(folderName, Constants.vsProjectItemKindPhysicalFolder);

                if (folder != null)
                {
                    outputFile = Path.Combine(Path.Combine(this.GetProjectPath(), folderName), "DataEntityFunctions.ttinclude");
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
                body = ReadResourceContent(resourceName);
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
                body = ReadResourceContent(resourceName);
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

        public void UpdateEntityAdapterDynamicModelsTemplate(bool directDiagram = false)
        {
            string outputFile = "";
            Project current = this.GetEadProject();
            ProjectItem folder = null, projItem;

            if (current != null)
            {
                string folderName = "Includes";
                string body = ReadResourceContent(@"Linx.EntityAdapterDesigner.Templates.EntityAdapterDynamicModelsTemplate.txt");
                if (directDiagram)
                    body = body.Replace("fileName='..\\\\", "fileName='");

                folder = this.GetProjectItemByName(current, folderName);
                if (folder == null)
                    folder = current.ProjectItems.AddFolder(folderName, Constants.vsProjectItemKindPhysicalFolder);

                if (folder != null)
                {
                    string templateName = (directDiagram ? "Direct" : String.Empty) + "EntityAdapterDynamicModels.tt";
                    outputFile = Path.Combine(Path.Combine(this.GetProjectPath(), folderName), templateName);
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

        public void UpdateDomainViewsTemplate(string templateNameFrom, string templateNameTo)
        {
            string outputFile = "";
            Project current = this.GetEadProject();
            ProjectItem folder = null, projItem;

            if (current != null)
            {
                string folderName = "Domains";
                string body = ReadResourceContent(@"Linx.EntityAdapterDesigner.Templates." + templateNameFrom + ".txt");

                folder = this.GetProjectItemByName(current, folderName);
                if (folder == null)
                    folder = current.ProjectItems.AddFolder(folderName, Constants.vsProjectItemKindPhysicalFolder);

                if (folder != null)
                {
                    outputFile = Path.Combine(Path.Combine(this.GetProjectPath(), folderName), templateNameTo + ".tt");
                    if (!this.VerifySourceControl(outputFile))
                        return;

                    if (!ExistsProjectItem(folder.ProjectItems, templateNameTo + ".tt"))
                    {
                        File.WriteAllText(outputFile, body);
                        projItem = folder.ProjectItems.AddFromFile(outputFile);
                        projItem.Properties.Item("CustomTool").Value = "TextTemplatingFileGenerator";
                    }
                    else
                    {
                        if (File.ReadAllText(outputFile) != body)
                            File.WriteAllText(outputFile, body);
                        projItem = folder.ProjectItems.Item(templateNameTo + ".tt");
                    }
                    //Run Template
                    ((VSLangProj.VSProjectItem)projItem.Object).RunCustomTool();
                }
            }
        }

        public void UpdateKPIViewsTemplate()
        {
            string outputFile = "";
            Project current = this.GetEadProject();
            ProjectItem folder = null, projItem;

            if (current != null)
            {
                string folderName = "KPIs";
                string body = ReadResourceContent(@"Linx.EntityAdapterDesigner.Templates.KPIViewsTemplate.txt");

                folder = this.GetProjectItemByName(current, folderName);
                if (folder == null)
                    folder = current.ProjectItems.AddFolder(folderName, Constants.vsProjectItemKindPhysicalFolder);

                if (folder != null)
                {
                    outputFile = Path.Combine(Path.Combine(this.GetProjectPath(), folderName), "KPIViews.tt");
                    if (!this.VerifySourceControl(outputFile))
                        return;

                    if (!ExistsProjectItem(folder.ProjectItems, "KPIViews.tt"))
                    {
                        File.WriteAllText(outputFile, body);
                        projItem = folder.ProjectItems.AddFromFile(outputFile);
                        projItem.Properties.Item("CustomTool").Value = "TextTemplatingFileGenerator";
                    }
                    else
                    {
                        if (File.ReadAllText(outputFile) != body)
                            File.WriteAllText(outputFile, body);
                        projItem = folder.ProjectItems.Item("KPIViews.tt");
                    }
                    //Run Template
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

        public void UpdateHelpService(bool isJson = false)
        {
            string folderName = "Help For Accessing", contextName = Path.GetFileNameWithoutExtension(this.DocumentName);
            string outputFile = "", helpFileName = GetHelpFile(contextName, (isJson ? "Json" : ""));
            Project current = this.GetEadProject();
            ProjectItem folder = null, newItem = null;
            string body;

            if (isJson)
                body = this.GetJsonServiceHelp(contextName, "    ");
            else
                body = this.GetSoapServiceHelp(contextName, "    ");

            if (!current.IsNull())
            {
                folder = this.GetProjectItemByName(current, folderName);
                if (folder == null)
                    folder = current.ProjectItems.AddFolder(folderName, Constants.vsProjectItemKindPhysicalFolder);

                if (folder != null)
                {
                    if (this.EnableDocumentation)
                    {
                        outputFile = Path.Combine(Path.Combine(this.GetProjectPath(), folderName), helpFileName);
                        if (!this.VerifySourceControl(outputFile))
                            return;

                        if (!ExistsProjectItem(folder.ProjectItems, helpFileName))
                        {
                            File.WriteAllText(outputFile, body);
                            newItem = folder.ProjectItems.AddFromFile(outputFile);
                            newItem.Properties.Item("CopyToOutputDirectory").Value = 0;
                            newItem.Properties.Item("BuildAction").Value = 0;
                            newItem.Properties.Item("CustomTool").Value = "";
                        }
                        else
                        {
                            if (File.ReadAllText(outputFile) != body)
                                File.WriteAllText(outputFile, body);
                        }
                    }
                    else
                    {
                        var item = GetProjectItemByName(folder.ProjectItems, helpFileName);
                        if (item != null)
                        {
                            item.Delete();
                        }
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

                if (!ExistsProjectItem(current.ProjectItems, "Web.tt"))
                {
                    File.WriteAllText(outputFile, body);
                    newItem = current.ProjectItems.AddFromFile(outputFile);
                    newItem.Properties.Item("CustomTool").Value = "TextTemplatingFileGenerator";
                }
                else
                {
                    if (File.ReadAllText(outputFile) != body)
                        File.WriteAllText(outputFile, body);
                    newItem = current.ProjectItems.Item("Web.tt");
                }
                //Run Template
                ((VSLangProj.VSProjectItem)newItem.Object).RunCustomTool();
            }
        }

        public void UpdateDomainServiceTemplate()
        {
            string outputFile = "";
            ProjectItem newItem = null;
            ProjectItem item = GetDiagramProjectItem();

            if (!item.IsNull())
            {
                string body = ReadResourceContent(@"Linx.EntityAdapterDesigner.Templates.DomainServiceTemplate.txt");
                body = body.Replace("#ContextFileName#", item.Document.Name);
                body = body.Replace("#ContextNamespace#", Path.GetFileNameWithoutExtension(item.Document.Name));

                outputFile = Path.Combine(this.GetProjectPath(), Path.GetFileNameWithoutExtension(item.Name) + ".DomainService.tt");
                if (!this.VerifySourceControl(outputFile))
                    return;

                if (!ExistsProjectItem(item.ProjectItems, Path.GetFileNameWithoutExtension(item.Name) + ".DomainService.tt"))
                {
                    File.WriteAllText(outputFile, body);
                    newItem = item.ProjectItems.AddFromFile(outputFile);
                    newItem.Properties.Item("CustomTool").Value = "TextTemplatingFileGenerator";
                }
                else
                {
                    if (File.ReadAllText(outputFile) != body)
                        File.WriteAllText(outputFile, body);
                    newItem = item.ProjectItems.Item(Path.GetFileNameWithoutExtension(item.Name) + ".DomainService.tt");
                }
                //Run Template
                ((VSLangProj.VSProjectItem)newItem.Object).RunCustomTool();
            }
        }


        public void UpdateFormulasTemplate()
        {
            string outputFile = "";
            ProjectItem newItem = null;
            ProjectItem item = GetDiagramProjectItem();

            if (!item.IsNull())
            {
                string body = ReadResourceContent(@"Linx.EntityAdapterDesigner.Templates.FormulasTemplate.txt");
                body = body.Replace("#ContextFileName#", item.Document.Name);
                body = body.Replace("#ContextNamespace#", Path.GetFileNameWithoutExtension(item.Document.Name));

                outputFile = Path.Combine(this.GetProjectPath(), Path.GetFileNameWithoutExtension(item.Name) + ".Formulas.tt");
                if (!this.VerifySourceControl(outputFile))
                    return;

                if (!ExistsProjectItem(item.ProjectItems, Path.GetFileNameWithoutExtension(item.Name) + ".Formulas.tt"))
                {
                    File.WriteAllText(outputFile, body);
                    newItem = item.ProjectItems.AddFromFile(outputFile);
                    newItem.Properties.Item("CustomTool").Value = "TextTemplatingFileGenerator";
                }
                else
                {
                    if (File.ReadAllText(outputFile) != body)
                        File.WriteAllText(outputFile, body);
                    newItem = item.ProjectItems.Item(Path.GetFileNameWithoutExtension(item.Name) + ".Formulas.tt");
                }
                //Run Template
                ((VSLangProj.VSProjectItem)newItem.Object).RunCustomTool();
            }
        }

        public void UpdateExtendedFiltersTemplate()
        {
            string outputFile = "";
            ProjectItem newItem = null;
            ProjectItem item = GetDiagramProjectItem();

            if (!item.IsNull())
            {
                string body = ReadResourceContent(@"Linx.EntityAdapterDesigner.Templates.ExtendedFiltersTemplate.txt");
                body = body.Replace("#ContextFileName#", item.Document.Name);
                body = body.Replace("#ContextNamespace#", Path.GetFileNameWithoutExtension(item.Document.Name));

                outputFile = Path.Combine(this.GetProjectPath(), Path.GetFileNameWithoutExtension(item.Name) + ".ExtendedFilters.tt");
                if (!this.VerifySourceControl(outputFile))
                    return;

                if (!ExistsProjectItem(item.ProjectItems, Path.GetFileNameWithoutExtension(item.Name) + ".ExtendedFilters.tt"))
                {
                    File.WriteAllText(outputFile, body);
                    newItem = item.ProjectItems.AddFromFile(outputFile);
                    newItem.Properties.Item("CustomTool").Value = "TextTemplatingFileGenerator";
                }
                else
                {
                    if (File.ReadAllText(outputFile) != body)
                        File.WriteAllText(outputFile, body);
                    newItem = item.ProjectItems.Item(Path.GetFileNameWithoutExtension(item.Name) + ".ExtendedFilters.tt");
                }
                //Run Template
                ((VSLangProj.VSProjectItem)newItem.Object).RunCustomTool();
            }
        }

        public void UpdateStateRulesTemplate()
        {
            ProjectItem item = GetDiagramProjectItem();
            if (!item.IsNull())
            {
                var itemRules = GetProjectItemByName(item.ProjectItems, Path.GetFileNameWithoutExtension(item.Name) + ".StateRules.tt");
                if (itemRules != null)
                {
                    itemRules.Delete();
                }
            }
        }

        public void UpdateLookUpsTemplate()
        {
            string outputFile = "";
            ProjectItem newItem = null;
            ProjectItem item = GetDiagramProjectItem();

            if (!item.IsNull())
            {
                string body = ReadResourceContent(@"Linx.EntityAdapterDesigner.Templates.LookUpsTemplate.txt");
                body = body.Replace("#ContextFileName#", item.Document.Name);
                body = body.Replace("#ContextNamespace#", Path.GetFileNameWithoutExtension(item.Document.Name));

                outputFile = Path.Combine(this.GetProjectPath(), Path.GetFileNameWithoutExtension(item.Name) + ".LookUps.tt");
                if (!this.VerifySourceControl(outputFile))
                    return;

                if (!ExistsProjectItem(item.ProjectItems, Path.GetFileNameWithoutExtension(item.Name) + ".LookUps.tt"))
                {
                    File.WriteAllText(outputFile, body);
                    newItem = item.ProjectItems.AddFromFile(outputFile);
                    newItem.Properties.Item("CustomTool").Value = "TextTemplatingFileGenerator";
                }
                else
                {
                    if (File.ReadAllText(outputFile) != body)
                        File.WriteAllText(outputFile, body);
                    newItem = item.ProjectItems.Item(Path.GetFileNameWithoutExtension(item.Name) + ".LookUps.tt");
                }
                //Run Template
                ((VSLangProj.VSProjectItem)newItem.Object).RunCustomTool();
            }
        }

        public void DeleteInconsistentFiles()
        {
            ProjectItem item = GetDiagramProjectItem();
            List<string> deletedList = new List<string>();

            if (!item.IsNull())
            {
                foreach (ProjectItem projectItem in item.ProjectItems)
                {
                    //Check all inconsistences with the design
                    if (!projectItem.Name.Contains(".CustomValidation.") && !projectItem.Name.Contains(".Operations.") && !projectItem.Name.Contains(".Events."))
                    {
                        if (projectItem.Name.Left((Path.GetFileNameWithoutExtension(item.Name) + ".").Length) != (Path.GetFileNameWithoutExtension(item.Name) + "."))
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

        public void GenerateBusinessEvents(bool isShared)
        {
            string outputFile;
            ProjectItem item = GetDiagramProjectItem();
            Linx.Tools.CodeBuilder codeBuilder;

            if (!item.IsNull())
            {
                foreach (EntityAdapter entity in this.EntityAdapters)
                {
                    if (entity.EntityAdapterEvents.Count > 0)
                    {
                        outputFile = Path.Combine(this.GetProjectPath(), Path.GetFileNameWithoutExtension(item.Name) + "." + entity.Name + ".Events" + (isShared ? ".shared" : "") + ".cs");
                        if (!File.Exists(outputFile) || !ExistsProjectItem(item.ProjectItems, Path.GetFileNameWithoutExtension(item.Name) + "." + entity.Name + ".Events" + (isShared ? ".shared" : "") + ".cs"))
                        {
                            if (!this.VerifySourceControl(outputFile))
                                return;

                            RemoveProjectItems(item.ProjectItems, Path.GetFileNameWithoutExtension(item.Name) + "." + entity.Name + ".Events" + (isShared ? ".shared" : "") + ".cs");
                            codeBuilder = new Linx.Tools.CodeBuilder();

                            //Add Events
                            this.GenerateEntityEventsCode(codeBuilder, entity, Path.GetFileNameWithoutExtension(item.Name), isShared);
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

        public string GetWebApiClassODataStartFile(WebApiController api, Project webApiProject = null)
        {
            string outputFile = String.Empty;
            ProjectItem item = null;

            if (webApiProject == null)
                webApiProject = this.GetWebApiProject(api.ProjectSuffix);

            if (webApiProject != null)
                item = GetWebApiAppStartItem(api, webApiProject); ;

            if (!item.IsNull())
                outputFile = Path.Combine(this.GetProjectPath(webApiProject), Path.GetFileNameWithoutExtension(item.Name) + "\\" + api.Name + "ODataStart" + ".cs");

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

        public ProjectItem GetWebApiAppStartItem(WebApiController api, Project webApiProject = null)
        {
            string outputFile = String.Empty;
            ProjectItem item = null;

            if (webApiProject == null)
                webApiProject = this.GetWebApiProject(api.ProjectSuffix);

            if (webApiProject != null)
                item = this.GetProjectItemByName(webApiProject, "App_Start");

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

        public bool WriteFile(string outputFilePath, Linx.Tools.CodeBuilder codeBuilder, ProjectItems projectItems)
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

                projectItems.AddFromFile(outputFilePath);
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
                            outputFile = this.GetWebApiClassODataStartFile(api, webApiProject);
                            codeBuilder = new Linx.Tools.CodeBuilder();
                            this.GenerateWebApiAutomaticODataControllerStartCode(codeBuilder, api, webApiProject, item);

                            if (!File.Exists(outputFile) || !ExistsProjectItem(item.ProjectItems, System.IO.Path.GetFileName(outputFile)))
                            {
                                if (!this.VerifySourceControl(outputFile))
                                    return;

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
                        item = item = this.GetWebApiControllersItem(api, webApiProject);
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

        //public void GenerateWebApiAppStartCode()
        //{
        //    string outputFile, className;
        //    Project eadProject = GetEadProject();

        //    Dictionary<string, string> apis = new Dictionary<string, string>();


        //    if (eadProject != null)
        //    {
        //        Project webApiProject;
        //        ProjectItem item;
        //        Linx.Tools.CodeBuilder codeBuilder;

        //        foreach (var api in this.WebApiControllers)
        //        {
        //            webApiProject = this.GetWebApiProject(api.ProjectSuffix, eadProject);
        //            if (webApiProject != null)
        //            {
        //                item = item = this.GetWebApiAppStartItem(api, webApiProject);
        //                if (!item.IsNull())
        //                {
        //                    codeBuilder = new Linx.Tools.CodeBuilder();
        //                    //Get code
        //                    this.GenerateWebApiAttributeRoutingHttpCode(codeBuilder, api, webApiProject, eadProject);
        //                    string codeBody = codeBuilder.GetBody();

        //                    className = "AttributeRoutingHttp";
        //                    outputFile = this.GetWebApiAppStartClassFile(api, webApiProject, className);

        //                    WriteFile(outputFile, codeBuilder, item.ProjectItems);
        //                }
        //            }
        //        }
        //    }
        //}

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

        public string GenerateKpiInformations(string indent)
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

        public void GenerateBusinessOperations(bool isShared)
        {
            string outputFile;
            ProjectItem item = GetDiagramProjectItem();
            StringBuilder codeBuilder;

            if (!item.IsNull())
            {

                foreach (EntityAdapter entity in this.EntityAdapters)
                {
                    if (entity.EntityAdapterOperations.Count > 0)
                    {
                        outputFile = Path.Combine(this.GetProjectPath(), Path.GetFileNameWithoutExtension(item.Name) + "." + entity.Name + ".Operations" + (isShared ? ".shared" : "") + ".cs");
                        if (!File.Exists(outputFile) || !ExistsProjectItem(item.ProjectItems, Path.GetFileNameWithoutExtension(item.Name) + "." + entity.Name + ".Operations" + (isShared ? ".shared" : "") + ".cs"))
                        {
                            if (!this.VerifySourceControl(outputFile))
                                return;

                            RemoveProjectItems(item.ProjectItems, Path.GetFileNameWithoutExtension(item.Name) + "." + entity.Name + ".Operations" + (isShared ? ".shared" : "") + ".cs");
                            codeBuilder = new StringBuilder();

                            //Add Events
                            this.GenerateEntityOperationsCode(codeBuilder, entity, Path.GetFileNameWithoutExtension(item.Name), isShared);
                            System.IO.File.WriteAllText(outputFile, codeBuilder.ToString());
                            //Add project item.
                            item.ProjectItems.AddFromFile(outputFile);
                        }
                    }
                }
            }
        }

        public void GenerateDomainServiceExtensions()
        {
            string outputFile = "";
            ProjectItem item = GetDiagramProjectItem();
            Linx.Tools.CodeBuilder codeBuilder;

            if (!item.IsNull())
            {
                foreach (DomainServiceExtension domainServiceExt in this.DomainServiceExtensions)
                {
                    if (domainServiceExt.DomainServiceOperations.Count > 0)
                    {
                        outputFile = Path.Combine(this.GetProjectPath(), Path.GetFileNameWithoutExtension(item.Name) + "." + domainServiceExt.Name + ".Operations.cs");
                        if (!File.Exists(outputFile) || !ExistsProjectItem(item.ProjectItems, Path.GetFileNameWithoutExtension(item.Name) + "." + domainServiceExt.Name + ".Operations.cs"))
                        {
                            if (!this.VerifySourceControl(outputFile))
                                return;

                            RemoveProjectItems(item.ProjectItems, Path.GetFileNameWithoutExtension(item.Name) + "." + domainServiceExt.Name + ".Operations.cs");
                            codeBuilder = new Linx.Tools.CodeBuilder();
                            //Create class definition
                            this.GenerateDomainServiceExtensionsCode(codeBuilder, domainServiceExt, Path.GetFileNameWithoutExtension(item.Name));
                            System.IO.File.WriteAllText(outputFile, codeBuilder.ToString());
                            //Add project item.
                            item.ProjectItems.AddFromFile(outputFile);
                        }
                    }
                }
            }
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
            codeBuilder.AddLine("using Linx.LinqExtensions.Query;");
            codeBuilder.AddLine("using Linx.LinqExtensions.Functional;");
            codeBuilder.AddLine("using Linx.LinqExtensions.Expressions;");
            codeBuilder.AddLine("using Linx;");
            codeBuilder.AddLine("using Linx.Tools;");
            codeBuilder.AddLine("using System.Linq;");
            codeBuilder.AddLine("using System.ComponentModel;");
            codeBuilder.AddLine("using System.ComponentModel.DataAnnotations;");
            codeBuilder.AddLine("using System.ServiceModel.DomainServices.Server;");
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
                codeBuilder.AddLine("using System.ServiceModel;");
                codeBuilder.AddLine("using System.Data.Linq.SqlClient;");
                codeBuilder.AddLine("using System.Reflection;");
                codeBuilder.AddLine("using System.Data.Entity.Core.Objects.DataClasses;");
                if (edm != null)
                    codeBuilder.AddLine("using " + edm.TargetNamespace + ";");
                codeBuilder.AddLine("using System.ServiceModel.DomainServices.Hosting;");
                codeBuilder.AddLine("using System.ServiceModel.DomainServices;");
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

        private void GenerateWebApiControllerCode(Linx.Tools.CodeBuilder codeBuilder, WebApiController api, Project webApiProject, Project eadProject)
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
            codeBuilder.AddLine("using System.ComponentModel.Composition;");
            codeBuilder.AddLine("using System.Net;");
            codeBuilder.AddLine("using System.Net.Http;");
            codeBuilder.AddLine("using System.Web.Http;");
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

            codeBuilder.AddLine("using System.Web.Http;");
            codeBuilder.AddLine("using System.Web;");
            codeBuilder.AddLine("using System.Web.Routing;");
            codeBuilder.AddLine("using Newtonsoft.Json.Serialization;");
            codeBuilder.AddLine("using System.Reflection;");
            codeBuilder.AddLine("using System.Net.Http;");
            codeBuilder.AddLine("using System.Web.Http.Controllers;");
            codeBuilder.AddLine("using System.Linq;");
            codeBuilder.AddLine("using System.Web.Http.OData.Builder;");
            codeBuilder.AddLine("using System.Web.Http.OData.Extensions;");
            codeBuilder.AddLine("using System.Web.Http.OData.Routing.Conventions;");
            codeBuilder.AddLine("using System.Web.Http.OData.Routing;");
            codeBuilder.AddLine("using Microsoft.Data.Edm;");

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

        private void GenerateWebApiAutomaticControllerCode(Linx.Tools.CodeBuilder codeBuilder, WebApiController api, Project webApiProject, Project eadProject)
        {
            EntityDataModel edm = api.EntityAdapterDesignerRoot.GetEdm();
            string apiName = api.Name;

            codeBuilder.AddLine("using System;");
            codeBuilder.AddLine("using System.Collections;");
            codeBuilder.AddLine("using System.Collections.Generic;");
            codeBuilder.AddLine("using System.Linq.Expressions;");
            codeBuilder.AddLine("using Linx.Tools;");
            codeBuilder.AddLine("using Linx.Business.Tools;");
            codeBuilder.AddLine("using System.Linq;");
            codeBuilder.AddLine("using System.ComponentModel;");
            codeBuilder.AddLine("using System.ComponentModel.DataAnnotations;");
            codeBuilder.AddLine("using System.ComponentModel.Composition;");
            codeBuilder.AddLine("using System.Net;");
            codeBuilder.AddLine("using System.Net.Http;");
            codeBuilder.AddLine("using System.Web.Http;");
            codeBuilder.AddLine("using Newtonsoft.Json.Linq;");
            codeBuilder.AddLine("using Linx.Data;");
            if (api.IsDataService)
            {
                codeBuilder.AddLine("using System.Web.Http.OData;");
                codeBuilder.AddLine("using Linx.DataService;");
                codeBuilder.AddLine("using Breeze.WebApi2;");
                codeBuilder.AddLine("using Breeze.ContextProvider;");
            }
            if (!(this.EntityAdapters.Count == 0 && this.DomainServiceExtensions.Count == 0))
                codeBuilder.AddLine("using BusinessNS = " + api.EntityAdapterDesignerRoot.GetDirectContextNamespace() + ";");

            var item = GetWebApiControllersItem(api, webApiProject);

            codeBuilder.AddLine("");
            codeBuilder.AddLine("namespace " + webApiProject.Name + "." + item.Name);
            codeBuilder.AddLine("{");

            codeBuilder.IncreaseIndent();

            //Class Definition            
            codeBuilder.AddLine();
            codeBuilder.AddLine("//Examples:");
            codeBuilder.AddLine("// Default Call: http://localhost:1710/" + api.GetRoutePrefix() + "/[ActionName]");
            codeBuilder.AddLine("// Entities Catalog Call: http://localhost:1710/" + api.GetRoutePrefix() + "/GetEntities");
            codeBuilder.AddLine("// Entity MetaData Call: http://localhost:1710/" + api.GetRoutePrefix() + "/GetMetaData?entityName=[EntityName]&allComposition=false");
            codeBuilder.AddLine("// Help Call: http://localhost:1710/HelpController/" + api.Name);
            if (api.IsDataService)
                codeBuilder.AddLine("// Feed OData Call: http://localhost:1710/" + api.GetRoutePrefix() + "OData");
            codeBuilder.AddLine("[RoutePrefix(\"" + api.GetRoutePrefix() + "\")]");
            if (api.IsDataService)
                codeBuilder.AddLine("[Breeze.WebApi2.BreezeController]");

            if (this.EnableAutomaticAuthorization)
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
                codeBuilder.AddLine("public partial class " + apiName + "Controller : ApiController");
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
                codeBuilder.AddLine("private DataServiceRepository<BusinessNS." + dsName + "> repository;");
                codeBuilder.AddLine("public " + apiName + "Controller()" + repositoryBaseRef);
                codeBuilder.AddLine("{");
                codeBuilder.AddLine("    repository = new DataServiceRepository<BusinessNS." + dsName + ">(" + types + ");");
                codeBuilder.AddLine("    repository.Context.IsSecure = true;");
                codeBuilder.AddLine("}");
                codeBuilder.AddLine();
                codeBuilder.AddLine("[Route(\"GetMetaData\"), System.Web.Http.HttpGet()]");
                codeBuilder.AddLine("public List<LinxEntityReferenceInfo> GetMetaData(string entityName = \"\", bool allComposition = false)");
                codeBuilder.AddLine("{");
                codeBuilder.AddLine("    var result = repository.Context.GetMetaDataObject(\"" + api.EntityAdapterDesignerRoot.GetDirectContextNamespace() + ".\" + entityName, false, true);");
                codeBuilder.AddLine("    return (allComposition ? result : result.Where(e => e.ClassName == entityName).ToList());");
                codeBuilder.AddLine("}");
                codeBuilder.AddLine();                                
                codeBuilder.AddLine("[Route(\"GetEntities\"), System.Web.Http.HttpGet()]");
                codeBuilder.AddLine("public object[] GetEntities()");
                codeBuilder.AddLine("{");
                codeBuilder.AddLine("    return new object[] { ");
                                                            
                Action<EntityAdapter, bool, string, int> createMeta = null;
                createMeta = (entity, isDetail, indent, index) => {
                    codeBuilder.AddLine(indent + "                       "+ (index == 0 ? "" : ", ") +"new {");
                    codeBuilder.AddLine(indent + "                           Name = \"" + entity.Name + "\", ListName = \"" + (!isDetail ? "" : entity.Name + "List") + "\"");
                    codeBuilder.AddLine(indent + "                           , Details = new object[] { ");

                    var details = entity.SourceEntityAdapters.ToList();
                    details.ForEach(e => createMeta(e, true, indent + "    ", details.IndexOf(e)));

                    codeBuilder.AddLine(indent + "                           }");
                    codeBuilder.AddLine(indent + "                       }");
                };
                var parents = this.EntityAdapters.Where(e => e.TargetEntityAdapter == null).ToList();
                parents.ForEach(e => createMeta(e, false, "", parents.IndexOf(e)));                           
                codeBuilder.AddLine("    };");
                codeBuilder.AddLine("}");

                codeBuilder.AddLine();
                codeBuilder.AddLine("public Dictionary<string, string> ConvertToDictionary(string valueFlat, string delimiter = \",\", string startDictionary = \"[\", string delimiterDictionary = \":\", string endDictionary = \"]\")");
                codeBuilder.AddLine("{");
                codeBuilder.AddLine("    var list = new Dictionary<string, string>();");
                codeBuilder.AddLine("    var splittedData = valueFlat.Split(new string[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);");
                codeBuilder.AddLine();
                codeBuilder.AddLine("    foreach (var _item in splittedData)");
                codeBuilder.AddLine("    {");
                codeBuilder.AddLine("        var item = !string.IsNullOrEmpty(startDictionary) && !string.IsNullOrEmpty(endDictionary) ? _item.Extract(startDictionary, endDictionary) : _item;");
                codeBuilder.AddLine("        var item_splitted = item.Split(new string[] { delimiterDictionary }, StringSplitOptions.RemoveEmptyEntries);");
                codeBuilder.AddLine();
                codeBuilder.AddLine("        list.Add(item_splitted[0], item_splitted[1]);");
                codeBuilder.AddLine("    }");
                codeBuilder.AddLine();
                codeBuilder.AddLine("    return list;");
                codeBuilder.AddLine("}");


                //Generate domain service code
                this.GenerateDomainServiceActionsToController(codeBuilder, "repository", "repository.Context", (this.EnableAutomaticAuthorization ? apiName + "ControllerAuthorize" : ""));

                codeBuilder.DecreaseIndent();
            }

            codeBuilder.AddLine("}");
            //End Class Definition

            //Generate Feed controller 
            if (api.SynchronizedWithDomainService && api.IsDataService)
                this.GenerateFeedControllerCode(codeBuilder, apiName, (this.EnableAutomaticAuthorization ? apiName + "ControllerAuthorize" : ""));

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

                    codeBuilder.AddLine();
                    if (!authorizeAttribute.IsNullOrEmpty())
                        codeBuilder.AddLine("[" + authorizeAttribute + "]");
                    codeBuilder.AddLine("[EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]");
                    codeBuilder.AddLine("public IQueryable<BusinessNS." + entity.Name + "> Get" + entity.Name + "(" + String.Join(", ", pKeys.Select(p => p.Value + " key" + indexKeys.IndexOf(p.Key).ToString())) + ")");
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
                        codeBuilder.AddLine();
                        if (!authorizeAttribute.IsNullOrEmpty())
                            codeBuilder.AddLine("[" + authorizeAttribute + "]");
                        codeBuilder.AddLine("[EnableQuery(AllowedQueryOptions = System.Web.Http.OData.Query.AllowedQueryOptions.All, AllowedArithmeticOperators = System.Web.Http.OData.Query.AllowedArithmeticOperators.All, AllowedFunctions = System.Web.Http.OData.Query.AllowedFunctions.AllFunctions, AllowedLogicalOperators = System.Web.Http.OData.Query.AllowedLogicalOperators.All)]");
                        codeBuilder.AddLine("public IQueryable<BusinessNS." + entity.Name + "ParentComposition> Get" + entity.Name + "ParentComposition()");
                        codeBuilder.AddLine("{");
                        codeBuilder.AddLine("    return this.Context.Get" + entity.Name + "ParentCompositionByEntitySearchNoAssociations(null).AsQueryable();");
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

        private void GenerateDomainServiceActionsToController(Linx.Tools.CodeBuilder codeBuilder, string repositoryReference, string contextReference, string authorizeAttribute)
        {
            codeBuilder.AddLine();
            codeBuilder.AddLine("[Route(\"GetTemplateReport\"), System.Web.Http.HttpGet()]");
            codeBuilder.AddLine("public string GetTemplateReport(string reportPath)");
            codeBuilder.AddLine("{");
            codeBuilder.AddLine("    var zip = new LinxZip();");
            codeBuilder.AddLine("    zip.AddFile(System.Web.HttpContext.Current.Server.MapPath(\"~/\" + reportPath));");
            //codeBuilder.AddLine("    zip.AddStringContent(\"Readme.txt\", \"Linx Report\");");
            codeBuilder.AddLine("    zip.AddFile(System.Web.HttpContext.Current.Server.MapPath(\"~/bin/" + GetAssemblyName() + ".Reports.dll\"));");
            codeBuilder.AddLine("    zip.AddFile(System.Web.HttpContext.Current.Server.MapPath(\"~/bin/" + GetAssemblyName() + ".dll\"));");
            codeBuilder.AddLine("    return Convert.ToBase64String(zip.GetZipBytes());");
            codeBuilder.AddLine("}");
            codeBuilder.AddLine();
            codeBuilder.AddLine("[Route(\"GetReportDataSource\"), System.Web.Http.HttpGet()]");
            codeBuilder.AddLine("public string GetReportDataSource()");
            codeBuilder.AddLine("{");
            codeBuilder.AddLine("    var zip = new LinxZip();");
            //codeBuilder.AddLine("    zip.AddStringContent(\"Readme.txt\", \"Linx Report\");");
            codeBuilder.AddLine("    zip.AddFile(System.Web.HttpContext.Current.Server.MapPath(\"~/bin/" + GetAssemblyName() + ".Reports.dll\"));");
            codeBuilder.AddLine("    zip.AddFile(System.Web.HttpContext.Current.Server.MapPath(\"~/bin/" + GetAssemblyName() + ".dll\"));");
            codeBuilder.AddLine("    return Convert.ToBase64String(zip.GetZipBytes());");
            codeBuilder.AddLine("}");
            codeBuilder.AddLine();
            codeBuilder.AddLine("[Route(\"GetDomainsInfo\"), System.Web.Http.HttpGet()]");
            codeBuilder.AddLine("public string[] GetDomainsInfo(string domainNames)");
            codeBuilder.AddLine("{");
            codeBuilder.AddLine("    return " + this.TargetNamespace + ".Domains.DomainHelper.GetDomainsInfo(domainNames);");
            codeBuilder.AddLine("}");
            codeBuilder.AddLine();
            codeBuilder.AddLine("#region Get LookUps");
            foreach (var lookUp in this.LookUpAdapters.Where(e => e.EntityAdapter != null && e.EntityAdapter.ExposeAsService))
            {
                codeBuilder.AddLine();
                if (!authorizeAttribute.IsNullOrEmpty())
                    codeBuilder.AddLine("[" + authorizeAttribute + "]");
                codeBuilder.AddLine("[Route(\"GetAll" + lookUp.Name + "\"), System.Web.Http.HttpGet()]");
                codeBuilder.AddLine("public IQueryable<BusinessNS." + lookUp.Name + "> GetAll" + lookUp.Name + "()");
                codeBuilder.AddLine("{");
                codeBuilder.AddLine("    return " + contextReference + ".GetAll" + lookUp.Name + "()" + (lookUp.QueryReturnType == EntityQueryReturnType.IEnumerable ? ".AsQueryable()" : "") + ";");
                codeBuilder.AddLine("}");
                codeBuilder.AddLine();
                if (!authorizeAttribute.IsNullOrEmpty())
                    codeBuilder.AddLine("[" + authorizeAttribute + "]");
                codeBuilder.AddLine("[Route(\"Get" + lookUp.Name + "ByEntitySearch\"), System.Web.Http.HttpGet()]");
                codeBuilder.AddLine("public IQueryable<BusinessNS." + lookUp.Name + "> Get" + lookUp.Name + "ByEntitySearch(string propertyName, string jEntitySearch)");
                codeBuilder.AddLine("{");
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
                    codeBuilder.AddLine("[Route(\"Get" + kpiName + "Ranges\"), System.Web.Http.HttpGet()]");
                    codeBuilder.AddLine("public IEnumerable<KpiRangeItem> Get" + kpiName + "Ranges()");
                    codeBuilder.AddLine("{");
                    codeBuilder.AddLine("    var kpi = new " + this.TargetNamespace + ".KPIs." + kpiName + "();");
                    codeBuilder.AddLine("    Linx.Business.Tools.KpiManager.UpdateKpiInfo(kpi);");
                    codeBuilder.AddLine("    return kpi.Ranges.Values.ToArray();");
                    codeBuilder.AddLine("}");
                    codeBuilder.AddLine();
                }
            }
            codeBuilder.AddLine("#endregion");

            codeBuilder.AddLine();
            codeBuilder.AddLine("#region Get Business Entities");
            GenDSGetBmMetaDataPropertyDefinition(codeBuilder, contextReference);
            foreach (var entity in this.EntityAdapters.Where(e => e.ExposeAsService))
            {
                GenerateAllDSGets(codeBuilder, contextReference, entity, authorizeAttribute, false);
            }
            codeBuilder.AddLine("#endregion");
            codeBuilder.AddLine();
            codeBuilder.AddLine("#region Get Business Entities By Parent Composition");
            foreach (var entity in this.EntityAdapters.Where(e => e.ExposeAsService && e.TargetEntityAdapter != null && e.IsParentCompositionAllowed()))
            {
                GenerateAllDSGets(codeBuilder, contextReference, entity, authorizeAttribute, true);
            }
            codeBuilder.AddLine("#endregion");

            codeBuilder.AddLine();
            codeBuilder.AddLine("#region Save Changes");
            if (!authorizeAttribute.IsNullOrEmpty())
                codeBuilder.AddLine("[" + authorizeAttribute + "]");
            codeBuilder.AddLine("[Route(\"SaveChanges\"), System.Web.Http.HttpPost()]");
            codeBuilder.AddLine("public SaveResult SaveChanges(JObject saveBundle)");
            codeBuilder.AddLine("{");
            codeBuilder.AddLine("    var result = " + repositoryReference + ".SaveChanges(saveBundle);");
            codeBuilder.AddLine("    return result;");
            codeBuilder.AddLine("}");

            codeBuilder.AddLine("#endregion");

        }

        private void GenerateAllDSGets(Linx.Tools.CodeBuilder codeBuilder, string contexReference, EntityAdapter entity, string authorizeAttribute, bool byParentComposition)
        {
            if (!byParentComposition)
            {
                GenDSGetDefinition(codeBuilder, contexReference, entity, false, false, authorizeAttribute, byParentComposition);
                GenDSGetDefinition(codeBuilder, contexReference, entity, false, true, authorizeAttribute, byParentComposition);
                GenDSGetDefinition(codeBuilder, contexReference, entity, true, false, authorizeAttribute, byParentComposition);
                GenDSGetDefinition(codeBuilder, contexReference, entity, false, false, authorizeAttribute, byParentComposition, true);
            }
            GenDSGetDefinition(codeBuilder, contexReference, entity, true, true, authorizeAttribute, byParentComposition);
            GenDSExportToExcelDefinition(codeBuilder, contexReference, entity, authorizeAttribute, byParentComposition);
            GenDSExportToReportXmlDefinition(codeBuilder, contexReference, entity, authorizeAttribute, byParentComposition);
            GenDSGetSampleDefinition(codeBuilder, contexReference, entity, byParentComposition, authorizeAttribute);            
        }

        private void GenDSExportToExcelDefinition(Linx.Tools.CodeBuilder codeBuilder, string contextReference, EntityAdapter entity, string authorizeAttribute = "", bool byParentComposition = false)
        {
            codeBuilder.AddLine("[Route(\"Get" + entity.Name + (byParentComposition ? "ParentComposition" : "") + "ToExcel\"), System.Web.Http.HttpPost()]");
            if (!authorizeAttribute.IsNullOrEmpty())
                codeBuilder.AddLine("[" + authorizeAttribute + "]");
            codeBuilder.AddLine("public string Get" + entity.Name + (byParentComposition ? "ParentComposition" : "") + "ToExcel(string[] parameters)");
            codeBuilder.AddLine("{");

            codeBuilder.AddLine("    string jEntitySearch = parameters[0];");
            codeBuilder.AddLine("    string translatedJEntitySearch = parameters[1];");
            codeBuilder.AddLine("    string columnsDefinition = parameters[2];");
            if (byParentComposition)
            {
                codeBuilder.AddLine("    jEntitySearch = jEntitySearch.Replace(\"" + entity.Name + "{\", \"" + entity.Name + "ParentComposition{\");");
            }
            codeBuilder.AddLine("    var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS." + entity.Name + (byParentComposition ? "ParentComposition" : "") + "), jEntitySearch, false, " + entity.IsModelViewSource().ToString().ToLower() + ");");
            codeBuilder.AddLine("    var entities = " + contextReference + ".Get" + entity.Name + (byParentComposition ? "ParentComposition" : "") + "ByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);");
            codeBuilder.AddLine("    var metadata = " + contextReference + ".GetMetaDataObject(\"" + this.GetContextNamespace() + "." + entity.Name + "\");");
            //treat columns that
            codeBuilder.AddLine("    var columns = this.ConvertToDictionary(columnsDefinition);");
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

            codeBuilder.AddLine("    var excelBytes = ExcelExport.CreateExcelDocumentFile(new ExcelExport.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });");
            codeBuilder.AddLine("    return Convert.ToBase64String(excelBytes);");
            codeBuilder.AddLine("}");
        }

        private void GenDSExportToReportXmlDefinition(Linx.Tools.CodeBuilder codeBuilder, string contextReference, EntityAdapter entity, string authorizeAttribute = "", bool byParentComposition = false)
        {
            codeBuilder.AddLine("[Route(\"Get" + entity.Name + (byParentComposition ? "ParentComposition" : "") + "ToReportXml\"), System.Web.Http.HttpPost()]");
            if (!authorizeAttribute.IsNullOrEmpty())
                codeBuilder.AddLine("[" + authorizeAttribute + "]");
            codeBuilder.AddLine("public string Get" + entity.Name + (byParentComposition ? "ParentComposition" : "") + "ToReportXml(string[] parameters)");
            codeBuilder.AddLine("{");

            codeBuilder.AddLine("    string reportName = parameters[0];");
            codeBuilder.AddLine("    string jEntitySearch = parameters[1];");
            codeBuilder.AddLine("    string translatedJEntitySearch = parameters[2];");
            codeBuilder.AddLine("    string columnsDefinition = parameters[3];");
            codeBuilder.AddLine("    string serviceBusUrl = parameters[4];");
            codeBuilder.AddLine("    bool exportMedia = Convert.ToBoolean(parameters[5]);");

            codeBuilder.AddLine("    var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS." + entity.Name + (byParentComposition ? "ParentComposition" : "") + "), jEntitySearch, false, " + entity.IsModelViewSource().ToString().ToLower() + ");");
            codeBuilder.AddLine("    var entities = " + contextReference + ".Get" + entity.Name + (byParentComposition ? "ParentComposition" : "") + "ByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);");
            codeBuilder.AddLine("    var metadata = " + contextReference + ".GetMetaDataObject(\"" + this.GetContextNamespace() + "." + entity.Name + "\", true);");
            codeBuilder.AddLine("    var columns = this.ConvertToDictionary(columnsDefinition);");
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
            codeBuilder.AddLine("    zip.AddStringContent(reportName + \".trdx\", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = \"" + GetAssemblyName() + ".Reports\", DataSourceFullName = \"" + GetAssemblyName() + ".Reports." + GetContextName() + "DataSource\", DataSourceObject = \"Get" + entity.Name + (byParentComposition ? "ParentComposition" : "") + "\", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl }));");
            codeBuilder.AddLine("    zip.AddFile(System.Web.HttpContext.Current.Server.MapPath(\"~/bin/" + GetAssemblyName() + ".Reports.dll\"));");
            codeBuilder.AddLine("    zip.AddFile(System.Web.HttpContext.Current.Server.MapPath(\"~/bin/" + GetAssemblyName() + ".dll\"));");
            codeBuilder.AddLine("    return Convert.ToBase64String(zip.GetZipBytes());");
            codeBuilder.AddLine("}");
        }

        //--Last Label Version
        //private void GenDSExportToExcelDefinition(Linx.Tools.CodeBuilder codeBuilder, string contextReference, EntityAdapter entity, string authorizeAttribute = "", bool byParentComposition = false)
        //{
        //    codeBuilder.AddLine("[Route(\"Get" + entity.Name + (byParentComposition ? "ParentComposition" : "") + "ToExcel\"), System.Web.Http.HttpGet()]");
        //    if (!authorizeAttribute.IsNullOrEmpty())
        //        codeBuilder.AddLine("[" + authorizeAttribute + "]");
        //    codeBuilder.AddLine("public string Get" + entity.Name + (byParentComposition ? "ParentComposition" : "") + "ToExcel(string jEntitySearch, string translatedJEntitySearch, string columnsDefinition)");
        //    codeBuilder.AddLine("{");
        //    codeBuilder.AddLine("    var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS." + entity.Name + (byParentComposition ? "ParentComposition" : "") + "), jEntitySearch, false, " + entity.IsModelViewSource().ToString().ToLower() + ", " + entity.IsOlap().ToString().ToLower() + ");");
        //    codeBuilder.AddLine("    var entities = " + contextReference + ".Get" + entity.Name + (byParentComposition ? "ParentComposition" : "") + "ByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);");
        //    codeBuilder.AddLine("    var metadata = " + contextReference + ".GetMetaDataObject(\"" + this.GetContextNamespace() + "." + entity.Name + "\");");
        //    codeBuilder.AddLine("    //Verify visible columns");
        //    codeBuilder.AddLine("    if (!columnsDefinition.IsNullOrEmpty())");
        //    codeBuilder.AddLine("    {");
        //    codeBuilder.AddLine("        var visibleColumns = columnsDefinition.Split(\",\".ToCharArray());");
        //    codeBuilder.AddLine("        foreach (var item in metadata[0].Properties)");
        //    codeBuilder.AddLine("        {");
        //    codeBuilder.AddLine("            item.IsBrowsable = visibleColumns.Contains(item.Name);");
        //    codeBuilder.AddLine("            item.Order = item.IsBrowsable ? Array.IndexOf(visibleColumns, item.Name) : -1;");
        //    codeBuilder.AddLine("        }");
        //    codeBuilder.AddLine("    }");

        //    codeBuilder.AddLine("    var excelBytes = ExcelExport.CreateExcelDocumentFile(new ExcelExport.EntitiesToExport { Entities = entities, Metadata = metadata.First(), JExpressionTranslated = translatedJEntitySearch });");
        //    codeBuilder.AddLine("    return Convert.ToBase64String(excelBytes);");
        //    codeBuilder.AddLine("}");
        //}

        //private void GenDSExportToReportXmlDefinition(Linx.Tools.CodeBuilder codeBuilder, string contextReference, EntityAdapter entity, string authorizeAttribute = "", bool byParentComposition = false)
        //{
        //    codeBuilder.AddLine("[Route(\"Get" + entity.Name + (byParentComposition ? "ParentComposition" : "") + "ToReportXml\"), System.Web.Http.HttpGet()]");
        //    if (!authorizeAttribute.IsNullOrEmpty())
        //        codeBuilder.AddLine("[" + authorizeAttribute + "]");
        //    codeBuilder.AddLine("public string Get" + entity.Name + (byParentComposition ? "ParentComposition" : "") + "ToReportXml(string reportName, string jEntitySearch, string translatedJEntitySearch, string columnsDefinition, string serviceBusUrl)");
        //    codeBuilder.AddLine("{");
        //    codeBuilder.AddLine("    var serializedEntitySearch = EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS." + entity.Name + (byParentComposition ? "ParentComposition" : "") + "), jEntitySearch, false, " + entity.IsModelViewSource().ToString().ToLower() + ", " + entity.IsOlap().ToString().ToLower() + ");");
        //    codeBuilder.AddLine("    var entities = " + contextReference + ".Get" + entity.Name + (byParentComposition ? "ParentComposition" : "") + "ByEntitySearchNoAssociations(serializedEntitySearch, jEntitySearch);");
        //    codeBuilder.AddLine("    var metadata = " + contextReference + ".GetMetaDataObject(\"" + this.GetContextNamespace() + "." + entity.Name + "\", true);");
        //    codeBuilder.AddLine("    if (!columnsDefinition.IsNullOrEmpty())");
        //    codeBuilder.AddLine("    {");
        //    codeBuilder.AddLine("        var visibleColumns = columnsDefinition.Split(\",\".ToCharArray());");
        //    codeBuilder.AddLine("        foreach (var item in metadata[0].Properties)");
        //    codeBuilder.AddLine("        {");
        //    codeBuilder.AddLine("            item.IsBrowsable = visibleColumns.Contains(item.Name);");
        //    codeBuilder.AddLine("            item.Order = item.IsBrowsable ? Array.IndexOf(visibleColumns, item.Name) : -1;");
        //    codeBuilder.AddLine("        }");
        //    codeBuilder.AddLine("    }");
        //    codeBuilder.AddLine("    var zip = new LinxZip();");
        //    codeBuilder.AddLine("    zip.AddStringContent(reportName + \".trdx\", ReportExport.CreateXmlReport(new ReportExport.EntitiesToExport{ ReportName = reportName, Metadata = metadata.First(), DataSourceAssembly = \"" + GetAssemblyName() + ".Reports\", DataSourceFullName = \"" + GetAssemblyName() + ".Reports." + GetContextName() + "DataSource\", DataSourceObject = \"Get" + entity.Name + (byParentComposition ? "ParentComposition" : "") + "\", JExpressionTranslated = translatedJEntitySearch, JQueryExpression = jEntitySearch, ServiceBusUrl = serviceBusUrl }));");
        //    codeBuilder.AddLine("    zip.AddFile(System.Web.HttpContext.Current.Server.MapPath(\"~/bin/" + GetAssemblyName() + ".Reports.dll\"));");
        //    codeBuilder.AddLine("    zip.AddFile(System.Web.HttpContext.Current.Server.MapPath(\"~/bin/" + GetAssemblyName() + ".dll\"));");
        //    codeBuilder.AddLine("    return Convert.ToBase64String(zip.GetZipBytes());");
        //    codeBuilder.AddLine("}");
        //}

        private void GenDSGetDefinition(Linx.Tools.CodeBuilder codeBuilder, string contextReference, EntityAdapter entity, bool byEntitySearch = false, bool noAssociations = false, string authorizeAttribute = "", bool byParentComposition = false, bool byQuickSearch = false)
        {
            string methodName = entity.Name + (byQuickSearch ? "QuickSearch" : (byParentComposition ? "ParentComposition" : "") + (byEntitySearch ? "ByEntitySearch" : "") + (noAssociations ? "NoAssociations" : ""));

            var quickSearchProperties = (byQuickSearch ? entity.GetAllInheritanceProperties().Where(e => e.QuickSearchIndex >= 0).OrderBy(e => e.QuickSearchIndex).Select(e => (e.Datatype.ToLower().Contains("string") ? "." : "") + e.Name).ToArray() : new string[] {});

            if (byQuickSearch && quickSearchProperties.Where(e => e.Left(1) == ".").Count() == 0)
                return;

            codeBuilder.AddLine("");
            if (!authorizeAttribute.IsNullOrEmpty())
                codeBuilder.AddLine("[" + authorizeAttribute + "]");
            codeBuilder.AddLine("[Route(\"Get" + methodName + "\"), System.Web.Http.HttpGet()]");
            codeBuilder.AddLine("public IQueryable<" + (byQuickSearch ? "object" : "BusinessNS." + entity.Name + (byParentComposition ? "ParentComposition" : "")) + "> Get" + methodName + "(" + (byQuickSearch ? "string q = \"\", int page = 1" : (byEntitySearch ? "string jEntitySearch" : "")) + ")");
            codeBuilder.AddLine("{");
            if (byQuickSearch)
            {
                codeBuilder.AddLine("    return (");
                codeBuilder.AddLine("                       from r in repository.Context.Get" + entity.Name + "NoAssociations()");
                codeBuilder.AddLine("                       where " + String.Join(", ", quickSearchProperties.Where(e => e.Left(1) == ".").Select(e => "r." + e.Replace(".", "") + ".Contains(q)")));
                codeBuilder.AddLine("                       select new { " + String.Join(", ", quickSearchProperties.Select(e =>  e.Replace(".", "") + " = r." + e.Replace(".", ""))) + " }");
                codeBuilder.AddLine("                      ).Distinct().OrderBy(e => new { " + String.Join(", ", quickSearchProperties.Select(e => "e." + e.Replace(".", ""))) + " }).Take(10).Skip((page - 1) * 10);");
            }
            else
                codeBuilder.AddLine("    return " + contextReference + ".Get" + methodName + "(" + (byEntitySearch ? "Linx.Tools.EntitySearch.ParseFromJEntitySearch(typeof(BusinessNS." + entity.Name + (byParentComposition ? "ParentComposition" : "") + "), jEntitySearch, false, " + entity.IsModelViewSource().ToString().ToLower() + "), jEntitySearch" : "") + ")" + ((!entity.PrimaryEntity.IsNullOrEmpty() || entity.EntityAdapterRepresentation != null ? entity.QueryReturnType : EntityQueryReturnType.IEnumerable) == EntityQueryReturnType.IEnumerable ? ".AsQueryable()" : "") + ";");

            codeBuilder.AddLine("}");
        }

        private void GenDSGetBmMetaDataPropertyDefinition(Linx.Tools.CodeBuilder codeBuilder, string contextReference)
        {
            string methodName = "BmEntityProperties";

            codeBuilder.AddLine("");
            codeBuilder.AddLine("[Route(\"Get" + methodName + "\"), System.Web.Http.HttpGet()]");
            codeBuilder.AddLine("public List<BmMetaDataProperty> Get" + methodName + "(string entityName, string parentDataPath)");
            codeBuilder.AddLine("{");
            codeBuilder.AddLine("    return " + contextReference + ".Get" + methodName + "(entityName, parentDataPath);");
            codeBuilder.AddLine("}");
        }

        private void GenDSGetSampleDefinition(Linx.Tools.CodeBuilder codeBuilder, string contextReference, EntityAdapter entity, bool byParentComposition, string authorizeAttribute = "")
        {
            codeBuilder.AddLine("");
            if (!authorizeAttribute.IsNullOrEmpty())
                codeBuilder.AddLine("[" + authorizeAttribute + "]");
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

        public void CheckCustomValidationClass()
        {
            ProjectItem item = GetDiagramProjectItem();

            if (!item.IsNull())
            {
                foreach (EntityAdapter entity in this.EntityAdapters)
                {
                    if (!entity.CustomValidationMethod.IsNullOrEmpty())
                    {
                        GenerateCustomValidationClass();
                        return;
                    }
                    foreach (EntityAdapterAttribute attribute in entity.GetAllAttributes())
                    {
                        if (!attribute.CustomValidationMethod.IsNullOrEmpty())
                        {
                            GenerateCustomValidationClass();
                            return;
                        }
                    }
                }
            }
        }

        public void GenerateCustomValidationClass()
        {
            string outputFile = "";
            ProjectItem item = GetDiagramProjectItem();
            StringBuilder codeBuilder;

            if (!item.IsNull())
            {
                outputFile = Path.Combine(this.GetProjectPath(), Path.GetFileNameWithoutExtension(item.Name) + ".CustomValidation.shared.cs");
                if (!ExistsProjectItem(item.ProjectItems, Path.GetFileNameWithoutExtension(item.Name) + ".CustomValidation.shared.cs"))
                {
                    if (!this.VerifySourceControl(outputFile))
                        return;

                    codeBuilder = new StringBuilder();
                    //Create class definition
                    this.GenerateCustomValidationCode(codeBuilder, Path.GetFileNameWithoutExtension(item.Name));
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
            codeBuilder.AppendLine("using System.ServiceModel;");
            codeBuilder.AppendLine("using System.Linq.Expressions;");
            codeBuilder.AppendLine("using Linx.LinqExtensions.Query;");
            codeBuilder.AppendLine("using Linx.LinqExtensions.Functional;");
            codeBuilder.AppendLine("using Linx.LinqExtensions.Expressions;");
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

            codeBuilder.IncreaseIndent();
            codeBuilder.IncreaseIndent();
            foreach (EntityAdapter entity in this.EntityAdapters)
            {
                codeBuilder.AddLine("//" + entity.Name);
                codeBuilder.AddLine("void OnSavingChanges" + entity.Name + "User(" + domainService + " context, " + entity.Name + " entity, ChangeOperation changeOperation);");
                codeBuilder.AddLine("void OnSavingContextChanges" + entity.Name + "User(" + domainService + " context, ChangeSetEntry[] entities);");
                codeBuilder.AddLine("void OnSavedChanges" + entity.Name + "User(" + domainService + " context, " + entity.Name + " entity, ChangeOperation changeOperation);");
                codeBuilder.AddLine("void OnSavedContextChanges" + entity.Name + "User(" + domainService + " context, ChangeSetEntry[] entities);");
                codeBuilder.AddLine("void OnTransactedChanges" + entity.Name + "User(" + domainService + " context, " + entity.Name + " entity, ChangeOperation changeOperation);");
                codeBuilder.AddLine("void OnTransactedContextChanges" + entity.Name + "User(" + domainService + " context, ChangeSetEntry[] entities);");
                codeBuilder.AddLine("void OnTransactingChanges" + entity.Name + "User(" + domainService + " context, " + entity.Name + " entity, ChangeOperation changeOperation);");
                codeBuilder.AddLine("void OnTransactingContextChanges" + entity.Name + "User(" + domainService + " context, ChangeSetEntry[] entities);");
                codeBuilder.AddLine("void OnCleared" + entity.Name + "User(" + entity.Name + " entity);");
                codeBuilder.AddLine("void OnSearching" + entity.Name + "User(ref " + (!entity.PrimaryEntity.IsNullOrEmpty() || entity.EntityAdapterRepresentation != null ? entity.QueryReturnType.ToString() : "IEnumerable") + "<" + entity.Name + "> searchDefinition, bool noAssociations, List<EntitySearch> searchList);");
                codeBuilder.AddLine("");
            }
            codeBuilder.DecreaseIndent();
            codeBuilder.DecreaseIndent();
            return codeBuilder.GetBody();
        }

        public void GenerateDomainServiceInterfaceImplementation()
        {
            EnvDTE.Project diagramProject = this.GetEadProject();
            EnvDTE.Project extensionProject = GetProjectByName(diagramProject.Name + ".Extension");
            ProjectItem item = GetDiagramProjectItem();

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

            foreach (EntityAdapter entity in this.EntityAdapters)
            {
                codeBuilder.AddLine("//" + entity.Name);
                codeBuilder.AddLine("#region : " + entity.Name + " Extension");
                codeBuilder.AddLine("");
                codeBuilder.AddLine("public void OnSavingChanges" + entity.Name + "User(" + domainService + " context, " + entity.Name + " entity, ChangeOperation changeOperation)");
                codeBuilder.AddLine("{");
                codeBuilder.AddLine("");
                codeBuilder.AddLine("}");
                codeBuilder.AddLine("");
                codeBuilder.AddLine("public void OnSavingContextChanges" + entity.Name + "User(" + domainService + " context, ChangeSetEntry[] entities)");
                codeBuilder.AddLine("{");
                codeBuilder.AddLine("");
                codeBuilder.AddLine("}");
                codeBuilder.AddLine("");
                codeBuilder.AddLine("public void OnSavedChanges" + entity.Name + "User(" + domainService + " context, " + entity.Name + " entity, ChangeOperation changeOperation)");
                codeBuilder.AddLine("{");
                codeBuilder.AddLine("");
                codeBuilder.AddLine("}");
                codeBuilder.AddLine("");
                codeBuilder.AddLine("public void OnSavedContextChanges" + entity.Name + "User(" + domainService + " context, ChangeSetEntry[] entities)");
                codeBuilder.AddLine("{");
                codeBuilder.AddLine("");
                codeBuilder.AddLine("}");
                codeBuilder.AddLine("");
                codeBuilder.AddLine("public void OnTransactedChanges" + entity.Name + "User(" + domainService + " context, " + entity.Name + " entity, ChangeOperation changeOperation)");
                codeBuilder.AddLine("{");
                codeBuilder.AddLine("");
                codeBuilder.AddLine("}");
                codeBuilder.AddLine("");
                codeBuilder.AddLine("public void OnTransactedContextChanges" + entity.Name + "User(" + domainService + " context, ChangeSetEntry[] entities)");
                codeBuilder.AddLine("{");
                codeBuilder.AddLine("");
                codeBuilder.AddLine("}");
                codeBuilder.AddLine("");
                codeBuilder.AddLine("public void OnTransactingChanges" + entity.Name + "User(" + domainService + " context, " + entity.Name + " entity, ChangeOperation changeOperation)");
                codeBuilder.AddLine("{");
                codeBuilder.AddLine("");
                codeBuilder.AddLine("}");
                codeBuilder.AddLine("");
                codeBuilder.AddLine("public void OnTransactingContextChanges" + entity.Name + "User(" + domainService + " context, ChangeSetEntry[] entities)");
                codeBuilder.AddLine("{");
                codeBuilder.AddLine("");
                codeBuilder.AddLine("}");
                codeBuilder.AddLine("");
                codeBuilder.AddLine("public void OnCleared" + entity.Name + "User(" + entity.Name + " entity)");
                codeBuilder.AddLine("{");
                codeBuilder.AddLine("");
                codeBuilder.AddLine("}");
                codeBuilder.AddLine("");
                codeBuilder.AddLine("public void OnSearching" + entity.Name + "User(ref " + (!entity.PrimaryEntity.IsNullOrEmpty() || entity.EntityAdapterRepresentation != null ? entity.QueryReturnType.ToString() : "IEnumerable") + "<" + entity.Name + "> searchDefinition, bool noAssociations, List<EntitySearch> searchList)");
                codeBuilder.AddLine("{");
                codeBuilder.AddLine("");
                codeBuilder.AddLine("}");
                codeBuilder.AddLine("");
                codeBuilder.AddLine("#endregion");
                codeBuilder.AddLine("");
            }

            foreach (RepositoryMethod method in RepositoryInterfaces.Where(i => i.IsExtension).FirstOrDefault().RepositoryMethods)
            {
                codeBuilder.AddLine("public " + method.ReturnType + " " + method.Name + "(" + method.Parameters.Replace("#", ", ") + ")");
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

        public void GenerateCustomAuthorizationClass()
        {
            string outputFile = "";
            ProjectItem item = GetDiagramProjectItem();
            StringBuilder codeBuilder;

            if (!item.IsNull())
            {
                //Remove inconsistences
                RemoveInconsistentElements(item, ".CustomAuthorization.cs");
                RemoveInconsistentElements(item, ".CustomAuthorizationAuto.cs");

                //Entities
                foreach (EntityAdapter entity in this.EntityAdapters)
                {
                    //Add Automatic Authorization 
                    outputFile = Path.Combine(this.GetProjectPath(), Path.GetFileNameWithoutExtension(item.Name) + "." + entity.Name + ".CustomAuthorizationAuto.cs");

                    if (!this.VerifySourceControl(outputFile))
                        return;

                    //Create class definition
                    codeBuilder = new StringBuilder();
                    this.GenerateCustomAuthorizationAutoCode(codeBuilder, Path.GetFileNameWithoutExtension(item.Name), entity.Name, entity.GetTopParent().Name);
                    System.IO.File.WriteAllText(outputFile, codeBuilder.ToString());

                    if (!ExistsProjectItem(item.ProjectItems, Path.GetFileNameWithoutExtension(item.Name) + "." + entity.Name + ".CustomAuthorizationAuto.cs"))
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
            ProjectItem item = GetDiagramProjectItem();
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
                    var webApiProject = this.GetWebApiProject(((WebApiController)designElement).ProjectSuffix);

                    if (webApiProject == null)
                    {
                        MessageBox.Show("This WebAPI project does not exists! Save the designer before this operation.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    var api = (WebApiController)designElement;
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


        public void OpenCodeElement(string fileName, string className, string elementName)
        {
            ProjectItem item = GetDiagramProjectItem();

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

        public TextSelection OpenGenericOperation(string fileName, GenericOperation targetOperation, string className, ProjectItem item, String insertCommandText)
        {
            TextSelection selection = null;

            if (targetOperation.OverloadName.IsNullOrEmpty())
            {
                MessageBox.Show("Cannot open the operation because the OverloadName property is empty!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return selection;
            }

            if (item.IsNull())
                item = GetDiagramProjectItem();

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

        public TextSelection OpenCustomValidationOperation(GenericOperation targetOperation)
        {
            ProjectItem item = GetDiagramProjectItem();

            if (!item.IsNull())
                return this.OpenGenericOperation(Path.GetFileNameWithoutExtension(item.Name) + ".CustomValidation.shared.cs", targetOperation, Path.GetFileNameWithoutExtension(item.Name) + "CustomValidation");
            else return null;
        }

        public TextSelection OpenDomainServiceOperation(DomainServiceOperation targetOperation)
        {
            ProjectItem item = GetDiagramProjectItem();

            if (!item.IsNull())
                return this.OpenGenericOperation(Path.GetFileNameWithoutExtension(item.Name) + "." + targetOperation.DomainServiceExtension.Name + ".Operations.cs", targetOperation, Path.GetFileNameWithoutExtension(item.Name) + "DomainService");
            else return null;
        }

        public TextSelection OpenStoreQuery(StoreQuery sq, GenericOperation operation)
        {
            ProjectItem item = GetDiagramProjectItem();
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
            var webApiProject = this.GetWebApiProject(((WebApiAction)targetOperation).WebApiController.ProjectSuffix);
            if (!webApiProject.IsNull())
            {
                string classFile = Path.GetFileName(this.GetWebApiClassFile(((WebApiAction)targetOperation).WebApiController, webApiProject));
                ProjectItem item = this.GetWebApiControllersItem(((WebApiAction)targetOperation).WebApiController, webApiProject);
                return this.OpenGenericOperation(classFile, targetOperation, Path.GetFileNameWithoutExtension(classFile), item);
            }
            else return null;
        }

        public TextSelection OpenEntityOperation(EntityAdapterOperation targetOperation)
        {
            ProjectItem item = GetDiagramProjectItem();

            if (!item.IsNull())
                return this.OpenGenericOperation(Path.GetFileNameWithoutExtension(item.Name) + "." + targetOperation.EntityAdapter.Name + ".Operations" + (targetOperation.IsShared ? ".shared" : "") + ".cs", targetOperation, targetOperation.EntityAdapter.Name);
            else return null;
        }

        public TextSelection OpenEntityEvent(EntityAdapterEvent targetEvent)
        {
            ProjectItem item = GetDiagramProjectItem();

            if (!item.IsNull())
                return this.OpenGenericOperation(Path.GetFileNameWithoutExtension(item.Name) + "." + targetEvent.EntityAdapter.Name + ".Events" + (targetEvent.IsShared ? ".shared" : "") + ".cs", targetEvent, targetEvent.EntityAdapter.Name);
            else return null;
        }

        public void OpenClientEntityEvent(EntityAdapterClientEvent targetEvent)
        {
            ProjectItem item = this.SpaCodeGen.GetSpaAppFolder("services");

            if (!item.IsNull())
            {
                this.OpenElementName(item, this.GetContextName() + "Context.js", "ownerReference." + targetEvent.Name + " ", String.Empty);
            }
        }

        public void OpenClientUiEvent(UserInterfaceClientEvent targetEvent)
        {
            ProjectItem item = this.SpaCodeGen.GetSpaAppFolder("viewmodels");

            if (!item.IsNull())
            {
                this.OpenElementName(item, targetEvent.EntityAdapterUserInterface.Name + ".js", "var " + targetEvent.Name + " ", String.Empty);
            }
        }

        #endregion Open Operations


        public Project varrepProject { get; set; }
    }
}
