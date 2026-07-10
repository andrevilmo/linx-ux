using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Validation;
using Linx.Tools;
using Microsoft.VisualStudio.Modeling.Immutability;

namespace Linx.BusinessDataModelDesigner
{
    /// <summary>  
    /// Add a hard constraint to StateElement to prevent its "Name" property from being empty.  
    /// </summary>  
    public partial class NamedElement
    {

        public void ValidUniqueNames(IEnumerable<NamedElement> types, ValidationContext context)
        {
            Dictionary<string, NamedElement> elementNames = new Dictionary<string, NamedElement>();

            //Check Entities
            foreach (NamedElement element in types)
            {
                if (element is ModelClass)
                {
                    element.ValidUniqueNames(((ModelClass)element).Attributes.ToList(), context);
                    element.ValidUniqueNames(((ModelClass)element).Operations.ToList(), context);
                    element.ValidUniqueNames(((ModelClass)element).ModelIndexes.ToList(), context);
                }

                if (element is ModelInterface)
                {
                    element.ValidUniqueNames(((ModelInterface)element).Operations.ToList(), context);
                }

                if (elementNames.ContainsKey(element.Name))
                {
                    string description = String.Format(CultureInfo.CurrentCulture, "Element name '{0}' is used more than once. Check the following elements: {1}.", element.Name, element.Name + ", " + elementNames[element.Name].Name);
                    context.LogError(description, "Unique Name Error", element, elementNames[element.Name]);
                }
                elementNames[element.Name] = element;
            }
        }

        public void ValidDomains(IEnumerable<NamedElement> types, ValidationContext context)
        {
            //Check Domain informations
            foreach (DomainView element in types.Where(e => e is DomainView).Select(e => e as DomainView).ToArray())
            {
                string inconsistenceInfo = element.GetInconsistenceInfo();
                if (!inconsistenceInfo.IsNullOrEmpty())
                {
                    string description = String.Format(CultureInfo.CurrentCulture, "The DomainView '{0}' has the following inconsistency: {1}.", element.Name, inconsistenceInfo);
                    context.LogError(description, "DomainView Error", element);
                }
            }
        }

        public void ValidPrimaryKeys(IEnumerable<NamedElement> types, ValidationContext context)
        {
            //Check Domain informations
            foreach (ModelClass element in types.Where(e => e is ModelClass).Select(e => e as ModelClass).ToArray())
            {
                if (element.Attributes.Any(e => e.IsPrimaryKey && e.IsNullable))
                {
                    string description = String.Format(CultureInfo.CurrentCulture, "The entity '{0}' has nullable primary key(s).", element.Name);
                    context.LogError(description, "PrimaryKey Error", element);
                }
            }
        }

        public void ValidUniqueTables(IEnumerable<ModelClass> types, ValidationContext context)
        {
            Dictionary<string, ModelClass> elementNames = new Dictionary<string, ModelClass>();
            string tableKeyName;
            //Check Entities
            foreach (ModelClass element in types)
            {
                ValidUniqueColumns(element.GetAllInheritanceAttributes(), context);
                tableKeyName = element.Table.IsNullOrEmpty() ? element.Name : element.Table;
                if (elementNames.ContainsKey(tableKeyName))
                {
                    string errorElements = String.Empty;

                    string description = String.Format(CultureInfo.CurrentCulture, "The TableName '{0}' is used more than once. Check the following elements: {1}.", tableKeyName, element.Name + ", " + elementNames[tableKeyName].Name);
                    context.LogError(description, "Unique TableName Error", element, elementNames[tableKeyName]);
                }
                elementNames[tableKeyName] = element;
            }
        }

        public void ValidSharedTables(IEnumerable<ModelClass> types, ValidationContext context)
        {
            //Check Entities
            foreach (ModelClass element in types)
            {
                if (element.SuperclassSh != null && (!element.PrimaryKeyColumnMap.IsNullOrEmpty() || !element.PrimaryKeyConstraintName.IsNullOrEmpty()))
                {
                    string description = String.Format(CultureInfo.CurrentCulture, "The entity '{0}' is a SharedTable, therefore it is not allowed associate a value to the following properties: PrimaryKeyColumnMap, PrimaryKeyConstraintName.", element.Name);
                    context.LogError(description, "Shared Table Error", element);
                }
            }
        }

        public void ValidDataContextName(BusinessDataModelDesignerRoot root, ValidationContext context)
        {
            if (root.DataContextName == "BusinessDataModel" && root.DbProviders.Count > 0)
                context.LogError("The property 'DataContextName' of this designer needs a correct business name.", "DataContextName Error", root);
        }

        public void ValidInheritanceProperties(IEnumerable<ModelClass> types, ValidationContext context)
        {
            //Check Entities
            foreach (ModelClass element in types.Where(e => e.Superclass != null || e.SuperclassSh != null))
            {
                if (element.HasRedundantInheritanceProperties())
                {
                    string description = String.Format(CultureInfo.CurrentCulture, "The entity '{0}' has duplicated properties with its base entity.", element.Name);
                    context.LogError(description, "Inheritance Property Error", element);
                }
                else if (!(element is ReferenceModelClass)) element.AdjustInheritedSchema();
            }
        }

        public void ValidDiscriminators(IEnumerable<ModelClass> types, ValidationContext context)
        {
            //Check Entities
            List<string> elementsWithError = new List<string>();
            foreach (ModelClass element in types.Where(e => e.SuperclassSh != null))
            {
                if (elementsWithError.Contains(element.SuperclassSh.Name))
                    continue;

                bool hasError = false;
                string discriminator = String.Empty;
                List<string> discriminatorValues = new List<string>();
                List<ModelElement> errorElements = new List<ModelElement>();
                var links = GeneralizationSh.GetLinksToSubclassesSh(element.SuperclassSh).ToArray();
                foreach (var link in links)
                {
                    if (discriminator.IsNullOrEmpty())
                        discriminator = link.Discriminator.Left("=");
                    if (discriminator != link.Discriminator.Left("=") || discriminatorValues.Contains(link.Discriminator.Right("=")))
                    {
                        string description = String.Format(CultureInfo.CurrentCulture, "The entity '{0}' has errors over its discriminator. Check if only one discriminator property is used and if all the used values are distinct.", element.SuperclassSh.Name);
                        errorElements.Add(element.SuperclassSh);
                        errorElements.AddRange(links);
                        foreach (var lnk in links)
                            errorElements.Add(lnk.SubclassSh);
                        context.LogError(description, "Discriminator Error", errorElements.ToArray());
                        hasError = true;
                        elementsWithError.Add(element.SuperclassSh.Name);
                        break;
                    }
                    discriminatorValues.Add(link.Discriminator.Right("="));
                }

                //Correct Discriminator
                if (!hasError && !discriminator.IsNullOrEmpty())
                {
                    var discriminatorAttr = element.SuperclassSh.Attributes.FirstOrDefault(e => (e.ColumnName.IsNullOrEmpty() ? e.Name : e.ColumnName) == discriminator && !e.NotMapped);
                    if (discriminatorAttr != null)
                    {
                        string description = String.Format(CultureInfo.CurrentCulture, "The entity '{0}' has errors over its discriminator. Check if the property '{1}' is marked as NotMapped.", element.SuperclassSh.Name, discriminatorAttr.Name);
                        context.LogError(description, "Discriminator Error", element.SuperclassSh);
                        elementsWithError.Add(element.SuperclassSh.Name);
                    }
                }
            }
        }

        public void ValidUniqueColumns(IEnumerable<ModelAttribute> attributes, ValidationContext context)
        {
            Dictionary<string, ModelAttribute> elementNames = new Dictionary<string, ModelAttribute>();
            string columnKeyName;
            //Check Entities
            foreach (ModelAttribute element in attributes)
            {
                columnKeyName = element.ColumnName.IsNullOrEmpty() ? element.Name : element.ColumnName;
                if (elementNames.ContainsKey(columnKeyName))
                {
                    string description = String.Format(CultureInfo.CurrentCulture, "The ColumnName '{0}' is used more than once. Check the following elements: {1}.", columnKeyName, element.Name + ", " + elementNames[columnKeyName].Name);
                    context.LogError(description, "Unique ColumnName Error", element, elementNames[columnKeyName]);
                }
                elementNames[columnKeyName] = element;
            }
        }

        public void ValidModelViews(IEnumerable<NamedElement> types, ValidationContext context)
        {
            //Check ModelViews
            foreach (ModelClass element in types.Where(e => e is ModelClass && ((ModelClass)e).Kind == ClassKind.ModelView))
            {
                if (element.GetLinksToMultipleAssociations().Count > 0 || element.GetLinksToSourceModelClasses().Count > 0 || element.GetLinksToTargetModelClasses().Count > 0 || element.GetLinkToMultipleAssociation() != null || element.GetLinkToSuperclass() != null || element.GetLinkToSuperclassSh() != null)
                    context.LogError("The ModelView [" + element.Name + "] has invalid associations.", "Model View Error", element);
            }            
        }

        public void ValidReferemces(IEnumerable<NamedElement> types, ValidationContext context)
        {
            //Check Entities
            bool hasError;
            foreach (ModelClass element in types.Where(e => e is ModelClass))
            {
                var inconsistentIndexes = element.GetInconsistentIndexes();
                if (inconsistentIndexes.Count > 0)
                {
                    foreach (var index in inconsistentIndexes)
                    {
                        string description = String.Format(CultureInfo.CurrentCulture, "The index '{0}' has invalid columns.", index.Name);
                        context.LogError(description, "Index Error", index);
                    }
                }

                if (!(element is ReferenceModelClass))
                {
                    foreach (var attribute in element.Attributes.Where(a => !a.ForeignKey.IsNullOrEmpty() && element.GetLinks(a.ForeignKey.Left(".")).Count == 0))
                    {
                        string description = String.Format(CultureInfo.CurrentCulture, "The foreign key '{0}' has no associated relation.", attribute.Name);
                        context.LogError(description, "Foreign Key Error", attribute);
                    }
                }

                var redundantAssociations = element.GetRedundantAssociations();
                if (redundantAssociations.Count > 0)
                {
                    foreach (var redundance in redundantAssociations)
                    {
                        string description = String.Format(CultureInfo.CurrentCulture, "The attribute '{0}' of class '{1}' has redundant associations.", redundance.Key.Name, element.Name);
                        List<ModelElement> errors = new List<ModelElement>() { redundance.Key };
                        errors.AddRange(redundance.Value);
                        context.LogError(description, "Redundant Association Error", errors.ToArray());
                    }
                }

                hasError = false;
                foreach (var link in Association.GetLinksToTargetModelClasses(element))
                {
                    if (link.GetTargetAttributes().Length == 0)
                    {
                        hasError = true;
                        string description = String.Format(CultureInfo.CurrentCulture, "The association from '{0}' to '{1}' is corrupted. Try to save again to check if the problem was solved.", link.SourceModelClass.Name, link.TargetModelClass.Name);
                        context.LogError(description, "Association Error", link.SourceModelClass, link.TargetModelClass);
                        if (link.SourceModelClass is ReferenceModelClass)
                            link.SourceModelClass.SetLocks(Locks.None);
                        link.Remove();
                        if (link.SourceModelClass is ReferenceModelClass)
                            link.SourceModelClass.SetLocks(Locks.Properties);
                    }
                }

                //Try to correct errors
                if (hasError && element is ReferenceModelClass)
                {
                    ((ReferenceModelClass)element).UpdateFromExternalReference();
                }

                foreach (var link in MultipleAssociationOrigin.GetLinksToMultipleAssociations(element))
                {
                    if (link.GetTargetAttributeElements().Count() == 0)
                    {
                        string description = String.Format(CultureInfo.CurrentCulture, "The association between '{0}' and '{1}' is corrupted.", link.OriginType.Name, link.MultipleAssociation.Name);
                        context.LogError(description, "Association Error", link);
                    }
                }
            }
        }

        public void ValidStructures(IEnumerable<NamedElement> types, ValidationContext context)
        {
            //Check Entities
            foreach (ModelClass element in types.Where(e => e is ModelClass))
            {
                if (element.Superclass != null || element.SuperclassSh != null)
                {
                    var attribute = element.Attributes.FirstOrDefault(e => e.IsPrimaryKey);
                    if (attribute != null)
                    {
                        string description = String.Format(CultureInfo.CurrentCulture, "Element '{0}' is a subclass and therefore cannot have primary key definitions.", element.Name);
                        context.LogError(description, "Primary Key Error", element);
                    }
                }
            }
        }

        public void ValidForeignKeys(IEnumerable<NamedElement> types, ValidationContext context)
        {
            //Check Entities
            foreach (ModelClass element in types.Where(e => e is ModelClass))
            {
                //Single Associations
                foreach (var link in Association.GetLinksToTargetModelClasses(element))
                {
                    link.CorrectRelationInfo();
                    if (link.HasRelationError())
                    {
                        string description = String.Format(CultureInfo.CurrentCulture, "The foreign key on '{0}' is different (Count, Order, DataType or inconsistent 'ForeignKey' property value) from origin primary key on '{1}'.", link.TargetModelClass.Name, link.SourceModelClass.Name);
                        context.LogError(description, "Foreign Key Error", link.TargetModelClass, link.SourceModelClass);
                    }
                }
                //Multiple Associations
                foreach (var link in MultipleAssociationOrigin.GetLinksToMultipleAssociations(element))
                {
                    link.CorrectRelationInfo();
                    if (link.HasRelationError())
                    {
                        string description = String.Format(CultureInfo.CurrentCulture, "The foreign key on '{0}' is different (Count, Order, DataType or inconsistent 'ForeignKey' property value) from origin primary key on '{1}'.", link.MultipleAssociation.TargetType.Name, link.OriginType.Name);
                        context.LogError(description, "Foreign Key Error", link.MultipleAssociation.TargetType, link.OriginType);
                    }
                }
            }
        }

        /// <summary>  
        /// Value handler for the NamedElement.Name domain property.  
        /// </summary>  
        internal sealed partial class NamePropertyHandler : DomainPropertyValueHandler<NamedElement, global::System.String>
        {
            protected override void OnValueChanging(NamedElement element, string oldValue, string newValue)
            {
                if (!element.Store.InUndoRedoOrRollback)
                {
                    if (string.IsNullOrEmpty(newValue))
                    {
                        throw new ArgumentOutOfRangeException("Name", "Name cannot be empty or null.");
                    }
                }
                base.OnValueChanging(element, oldValue, newValue);
            }
        }
    }
}
