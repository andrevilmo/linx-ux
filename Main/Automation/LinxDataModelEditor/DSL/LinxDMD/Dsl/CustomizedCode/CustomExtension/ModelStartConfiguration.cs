using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.Modeling.Diagrams;
using Microsoft.VisualStudio.Modeling.Validation;
using Microsoft.VisualStudio.Modeling;
using System.Globalization;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using Linx.Tools;
using System.Linq;
using System.Configuration;
using Microsoft.VisualStudio.Modeling.Immutability;
using DslModeling = global::Microsoft.VisualStudio.Modeling;
using Linx.BusinessDataModelDesigner.CustomCode;


namespace Linx.BusinessDataModelDesigner
{

    [RuleOn(typeof(DbProvider), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class DbProviderStartConfiguration : AddRule
    {
        public override void ElementAdded(ElementAddedEventArgs e)
        {
            DbProvider catalog = e.ModelElement as DbProvider;
            if (catalog != null && catalog.Server.IsNullOrEmpty())
            {
                if (catalog.ConnectionName.IsNullOrEmpty())
                    catalog.ConnectionName = "Type here the connection name";
                catalog.SetDefaults();
                catalog.BusinessDataModelDesignerRoot.CheckDefaultProvider();
            }

            base.ElementAdded(e);
        }
    }
        
    [RuleOn(typeof(ModelImplementationReferencesModelInterface), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class ModelImplementationReferencesModelInterfaceStartConfiguration : AddRule
    {
        public override void ElementAdded(ElementAddedEventArgs e)
        {
            base.ElementAdded(e);

            ModelImplementationReferencesModelInterface implLink = e.ModelElement as ModelImplementationReferencesModelInterface;

            if (implLink != null && implLink.ModelInterface != null)
            {
                implLink.ModelInterface.CheckDefaultImplementation();
            }
        }
    }

    [RuleOn(typeof(WebApiController), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class WebApiControllerChangeConfiguration : ChangeRule
    {
        public override void ElementPropertyChanged(ElementPropertyChangedEventArgs e)
        {
            WebApiController webApi = e.ModelElement as WebApiController;
            if (webApi != null)
            {
                //Change File Name
                if (e.DomainProperty.Name == "ProjectSuffix")
                {
                    webApi.RenameSourceFiles(e.OldValue as string);
                }
            }

            base.ElementPropertyChanged(e);
        }
    }

    [RuleOn(typeof(WebApiController), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class WebApiControllerDeletingConfiguration : DeletingRule
    {
        public override void ElementDeleting(ElementDeletingEventArgs e)
        {
            WebApiController webApi = e.ModelElement as WebApiController;
            if (webApi != null)
            {
                if (MessageBox.Show("Do you really want to delete the WEB API '" + webApi.Name + "'?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    throw new Exception("Operation cancelled!!!");
                }
                webApi.DeleteSourceFiles();
            }
            base.ElementDeleting(e);
        }
    }

    [RuleOn(typeof(DbProvider), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class DbProviderChangeConfiguration : ChangeRule
    {
        public override void ElementPropertyChanged(ElementPropertyChangedEventArgs e)
        {
            DbProvider catalog = e.ModelElement as DbProvider;
            if (catalog != null && catalog.BusinessDataModelDesignerRoot != null && catalog.BusinessDataModelDesignerRoot.DTEReference != null)
            {
                if (e.DomainProperty.Name == "IsDefault" && e.OldValue != e.NewValue)
                {
                    if (catalog != null)
                    {
                        catalog.BusinessDataModelDesignerRoot.CheckDefaultProvider(catalog);
                    }
                }
                else if (e.DomainProperty.Name == "Type" && e.OldValue != e.NewValue)
                {
                    if (catalog != null)
                    {
                        catalog.SetDefaults();
                    }
                }
            }
            base.ElementPropertyChanged(e);
        }
    }

    [RuleOn(typeof(DbProvider), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class DbProviderdeleteConfiguration : DeletingRule
    {
        public override void ElementDeleting(ElementDeletingEventArgs e)
        {
            DbProvider catalog = e.ModelElement as DbProvider;
            if (catalog != null && catalog.IsDefault)
            {
                catalog.BusinessDataModelDesignerRoot.CheckDefaultProvider(catalog, true);
            }

            base.ElementDeleting(e);
        }
    }

    [RuleOn(typeof(ClassHasAttributes), FireTime = TimeToFire.Inline, InitiallyDisabled = false)]
    internal sealed class ModelAttributeAddConfiguration : AddRule
    {
        public override void ElementAdded(ElementAddedEventArgs e)
        {
            base.ElementAdded(e);

            ClassHasAttributes modelElement = e.ModelElement as ClassHasAttributes;
            if (modelElement != null)
            {
                ReferenceModelClassAddConfiguration.Check(modelElement.ModelClass as ReferenceModelClass);
            }            
        }
    }
    
    [RuleOn(typeof(ModelAttribute), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class ModelAttributePropConfiguration : ChangeRule
    {
        public override void ElementPropertyChanged(ElementPropertyChangedEventArgs e)
        {
            ModelAttribute modelElement = e.ModelElement as ModelAttribute;
            if (modelElement != null && modelElement.IsPrimaryKey)
            {
                if (e.OldValue != e.NewValue)
                {
                    modelElement.UpdateRelations(e.DomainProperty.Name == "Name" ? e.OldValue as String : String.Empty);
                    if (e.DomainProperty.Name == "Name")
                    {
                        if (!modelElement.ColumnName.IsNullOrEmpty() && !e.OldValue.IsNullOrEmpty() && modelElement.ColumnName == e.OldValue.ToString() )
                        {
                            modelElement.ColumnName = e.NewValue.ToString();
                        }
                    }
                }
            }
            base.ElementPropertyChanged(e);
        }
    }

    [RuleOn(typeof(ClassHasOperations), FireTime = TimeToFire.Inline, InitiallyDisabled = false)]
    internal sealed class ClassOperationAddConfiguration : AddRule
    {
        public override void ElementAdded(ElementAddedEventArgs e)
        {
            ClassHasOperations modelElement = e.ModelElement as ClassHasOperations;
            if (modelElement != null)
            {
                ReferenceModelClassAddConfiguration.Check(modelElement.ModelClass as ReferenceModelClass);
            }

            base.ElementAdded(e);
        }
    }

    [RuleOn(typeof(ClassHasOperations), FireTime = TimeToFire.Inline, InitiallyDisabled = false)]
    internal sealed class ClassOperationDeletingConfiguration : DeletingRule
    {
        public override void ElementDeleting(ElementDeletingEventArgs e)
        {
            ClassHasOperations modelElement = e.ModelElement as ClassHasOperations;

            if (modelElement != null)
            {
                ReferenceModelClassAddConfiguration.Check(modelElement.ModelClass as ReferenceModelClass);
            }

            base.ElementDeleting(e);
        }
    }

    [RuleOn(typeof(ClassHasIndexes), FireTime = TimeToFire.Inline, InitiallyDisabled = false)]
    internal sealed class ModelIndexAddConfiguration : AddRule
    {
        public override void ElementAdded(ElementAddedEventArgs e)
        {
            ClassHasIndexes modelElement = e.ModelElement as ClassHasIndexes;
            if (modelElement != null)
            {
                ReferenceModelClassAddConfiguration.Check(modelElement.ModelClass as ReferenceModelClass);
            }

            base.ElementAdded(e);
        }
    }

    [RuleOn(typeof(ModelIndex), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class ModelIndexDeletingConfiguration : DeletingRule
    {
        public override void ElementDeleting(ElementDeletingEventArgs e)
        {
            ModelIndex modelElement = e.ModelElement as ModelIndex;

            if (modelElement != null)
            {
                ReferenceModelClassAddConfiguration.Check(modelElement.ModelClass as ReferenceModelClass);
            }
            
            base.ElementDeleting(e);
        }
    }
    
    [RuleOn(typeof(ReferenceModelClass), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class ReferenceModelClassAddConfiguration : AddRule
    {
        public static void Check(ReferenceModelClass modelElement)
        {
            if (modelElement != null && modelElement.GetLocks() == Locks.Properties)
            {
                throw new Exception("Add action is not allowed with reference classes!");
            }
        }

        public static void Check(ReferenceModelClass source, ReferenceModelClass target)
        {
            if (source != null && source.GetLocks() == Locks.Properties && target != null && target.GetLocks() == Locks.Properties)
            {
                throw new Exception("Add action is not allowed with reference classes!");
            }
        }
    }


    [RuleOn(typeof(ModelClass), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class ModelClassPropConfiguration : ChangeRule
    {
        public override void ElementPropertyChanged(ElementPropertyChangedEventArgs e)
        {
            ModelClass refModelClass = e.ModelElement as ModelClass;
            if (refModelClass != null)
            {
                if (e.OldValue != e.NewValue)
                {
                    switch (e.DomainProperty.Name)
                    {
                        case "Name":
                            refModelClass.UpdateName((string)e.OldValue);
                            break;
                        case "IsFactTable":
                            var shape = refModelClass.GetPresentation<ClassShape>();
                            if (shape != null)
                                shape.CheckDimensionRoutes(refModelClass);
                            break;
                        case "HideAssociations":
                            if (refModelClass.HideAssociations)
                                refModelClass.HideElementAssociations();
                            else
                                refModelClass.ShowElementAssociations();
                            break;
                        case "InStudy":
                            foreach (var pk in refModelClass.GetAllAttributes().Where(p => p.IsPrimaryKey))
                            {
                                pk.UpdateRelations(String.Empty);
                            }
                            break;
                        case "IsValidatable":
                            refModelClass.AdjustValidatableMethod();
                            break;
                        default:
                            break;
                    }
                }
            }

            base.ElementPropertyChanged(e);
        }
    }

    [RuleOn(typeof(ModelClass), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class ModelClassAddConfiguration : AddRule
    {
        public override void ElementAdded(ElementAddedEventArgs e)
        {
            ModelClass element = e.ModelElement as ModelClass;
            element.AddPrimaryKey();

            var shape = element.GetPresentation<ClassShape>();
            if (shape != null)
                shape.CheckDimensionRoutes(element);

            base.ElementAdded(e);
        }
    }


    [RuleOn(typeof(ModelClass), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class ModelClassChangeConfiguration : ChangeRule
    {
        public override void ElementPropertyChanged(ElementPropertyChangedEventArgs e)
        {
            ModelClass mClass = e.ModelElement as ModelClass;
            if (mClass != null)
            {
                //Change File Name
                if (e.DomainProperty.Name == "Kind" && e.OldValue != e.NewValue)
                {
                    if (((ClassKind)e.NewValue) == ClassKind.ModelView)
                        mClass.AdjustAttributesForModelViews();
                    else
                        mClass.AddPrimaryKey();
                }
            }

            base.ElementPropertyChanged(e);
        }
    }

    [RuleOn(typeof(ModelClass), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class ModelClassDeletingConfiguration : DeletingRule
    {
        public override void ElementDeleting(ElementDeletingEventArgs e)
        {
            ModelClass element = e.ModelElement as ModelClass;

            if (element != null)
                element.DeleteForeignKeys();

            base.ElementDeleting(e);
        }
    }

    [RuleOn(typeof(ReferenceModelClass), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class ReferenceModelClassDeletingConfiguration : DeletingRule
    {
        public override void ElementDeleting(ElementDeletingEventArgs e)
        {
            ReferenceModelClass element = e.ModelElement as ReferenceModelClass;

            if (element != null)
            {
                element.SetLocks(Locks.None);
                element.DeleteForeignKeys();
            }

            base.ElementDeleting(e);
        }
    }

    [RuleOn(typeof(ModelAttribute), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class ModelAttributeDeletingConfiguration : DeletingRule
    {
        public override void ElementDeleting(ElementDeletingEventArgs e)
        {
            ModelAttribute modelElement = e.ModelElement as ModelAttribute;

            if (modelElement != null)
            {
                ReferenceModelClassAddConfiguration.Check(modelElement.ModelClass as ReferenceModelClass);
            }

            if (modelElement != null && modelElement.ModelClass != null && !modelElement.ForeignKey.IsNullOrEmpty())
                modelElement.ModelClass.DeleteLink(modelElement.ForeignKey.Left("."));

            base.ElementDeleting(e);
        }
    }
    

    #region Associations Control
    
    [RuleOn(typeof(Association), FireTime = TimeToFire.Inline, InitiallyDisabled = false)]
    internal sealed class AssociationAddConfiguration : AddRule
    {
        public override void ElementAdded(ElementAddedEventArgs e)
        {
            Association modelElement = e.ModelElement as Association;

            if ((modelElement.TargetModelClass != null && modelElement.TargetModelClass.Kind == ClassKind.DatabaseView) || (modelElement.SourceModelClass != null && modelElement.SourceModelClass.Kind == ClassKind.DatabaseView))
            {
                throw new Exception("This action is not allowed with [Database Views]!");
            }
            
            if ((!(modelElement.SourceModelClass is ReferenceModelClass) && modelElement.TargetModelClass is ReferenceModelClass && modelElement.TargetModelClass.GetLocks() == Locks.Properties))
            {
                throw new Exception("This action is not allowed with reference classes!");
            }

            ReferenceModelClassAddConfiguration.Check(modelElement.TargetModelClass as ReferenceModelClass);

            modelElement.UpdatePropertyRelations(String.Empty);

            base.ElementAdded(e);
        }
    }

    [RuleOn(typeof(Association), FireTime = TimeToFire.Inline, InitiallyDisabled = false)]
    internal sealed class AssociationDeleteConfiguration : DeleteRule
    {
        public override void ElementDeleted(ElementDeletedEventArgs e)
        {
            Association modelElement = e.ModelElement as Association;

            if (modelElement != null)
            {
                //ReferenceModelClassAddConfiguration.Check(modelElement.SourceModelClass as ReferenceModelClass, modelElement.TargetModelClass as ReferenceModelClass);
                modelElement.DeletePropertyRelations();
            }

            base.ElementDeleted(e);
        }
    }

    [RuleOn(typeof(Association), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class AssociationPropConfiguration : ChangeRule
    {
        public override void ElementPropertyChanged(ElementPropertyChangedEventArgs e)
        {
            if (e.DomainProperty.Name == "TargetMultiplicity" && e.OldValue != e.NewValue)
            {
                Association modelElement = e.ModelElement as Association;
                modelElement.UpdatePropertyRelations(String.Empty);
            }

            if (e.DomainProperty.Name == "ForeignKeyConstraintName" && e.OldValue != e.NewValue)
            {
                Association modelElement = e.ModelElement as Association;
                modelElement.UpdatePropertyRelations(e.OldValue as string);
            }

            base.ElementPropertyChanged(e);
        }
    }

    [RuleOn(typeof(MultipleAssociationTarget), FireTime = TimeToFire.Inline, InitiallyDisabled = false)]
    internal sealed class MultipleAssociationTargetAddConfiguration : AddRule
    {
        public override void ElementAdded(ElementAddedEventArgs e)
        {
            MultipleAssociationTarget modelElement = e.ModelElement as MultipleAssociationTarget;

            if (modelElement.TargetType != null && modelElement.TargetType.Kind == ClassKind.DatabaseView)
            {
                throw new Exception("This action is not allowed with [Database Views]!");
            }
            
            if (modelElement.TargetType != null && modelElement.TargetType is ReferenceModelClass && modelElement.TargetType.GetLocks() == Locks.Properties && modelElement.MultipleAssociation.OriginTypes.Any(t => !(t is ReferenceModelClass)))
            {
                throw new Exception("This action is not allowed with reference classes!");
            }

            ReferenceModelClassAddConfiguration.Check(modelElement.TargetType as ReferenceModelClass);
            
            modelElement.UpdatePropertyRelations();

            base.ElementAdded(e);
        }
    }

    [RuleOn(typeof(MultipleAssociationTarget), FireTime = TimeToFire.Inline, InitiallyDisabled = false)]
    internal sealed class MultipleAssociationTargetDeleteConfiguration : DeleteRule
    {
        public override void ElementDeleted(ElementDeletedEventArgs e)
        {
            MultipleAssociationTarget modelElement = e.ModelElement as MultipleAssociationTarget;

            ReferenceModelClassAddConfiguration.Check(modelElement.TargetType as ReferenceModelClass);

            modelElement.DeletePropertyRelations();

            base.ElementDeleted(e);
        }
    }

    [RuleOn(typeof(MultipleAssociationOrigin), FireTime = TimeToFire.Inline, InitiallyDisabled = false)]
    internal sealed class MultipleAssociationOriginAddConfiguration : AddRule
    {
        public override void ElementAdded(ElementAddedEventArgs e)
        {
            MultipleAssociationOrigin modelElement = e.ModelElement as MultipleAssociationOrigin;

            if (modelElement.OriginType != null && modelElement.OriginType.Kind == ClassKind.DatabaseView)
            {
                throw new Exception("This action is not allowed with [Database Views]!");
            }

            modelElement.UpdatePropertyRelations(String.Empty);

            base.ElementAdded(e);
        }
    }

    [RuleOn(typeof(MultipleAssociationOrigin), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class MultipleAssociationOriginPropConfiguration : ChangeRule
    {
        public override void ElementPropertyChanged(ElementPropertyChangedEventArgs e)
        {
            if (e.DomainProperty.Name == "ForeignKeyConstraintName" && e.OldValue != e.NewValue)
            {
                MultipleAssociationOrigin modelElement = e.ModelElement as MultipleAssociationOrigin;
                modelElement.UpdatePropertyRelations(e.OldValue as string);
            }

            base.ElementPropertyChanged(e);
        }
    }

    [RuleOn(typeof(MultipleAssociationOrigin), FireTime = TimeToFire.Inline, InitiallyDisabled = false)]
    internal sealed class MultipleAssociationOriginDeleteConfiguration : DeleteRule
    {
        public override void ElementDeleted(ElementDeletedEventArgs e)
        {
            MultipleAssociationOrigin modelElement = e.ModelElement as MultipleAssociationOrigin;
            modelElement.DeletePropertyRelations();

            base.ElementDeleted(e);
        }
    }

    [RuleOn(typeof(Generalization), FireTime = TimeToFire.Inline, InitiallyDisabled = false)]
    internal sealed class GeneralizationAddConfiguration : AddRule
    {
        public override void ElementAdded(ElementAddedEventArgs e)
        {
            Generalization modelElement = e.ModelElement as Generalization;
            
            ReferenceModelClassAddConfiguration.Check(modelElement.Subclass as ReferenceModelClass);

            modelElement.UpdatePropertyRelations();

            base.ElementAdded(e);
        }
    }

    [RuleOn(typeof(Generalization), FireTime = TimeToFire.Inline, InitiallyDisabled = false)]
    internal sealed class GeneralizationDeleteConfiguration : DeleteRule
    {
        public override void ElementDeleted(ElementDeletedEventArgs e)
        {
            Generalization modelElement = e.ModelElement as Generalization;

            //ReferenceModelClassAddConfiguration.Check(modelElement.Superclass as ReferenceModelClass, modelElement.Subclass as ReferenceModelClass);

            modelElement.DeletePropertyRelations();

            base.ElementDeleted(e);
        }
    }


    [RuleOn(typeof(GeneralizationSh), FireTime = TimeToFire.Inline, InitiallyDisabled = false)]
    internal sealed class GeneralizationShAddConfiguration : AddRule
    {
        public override void ElementAdded(ElementAddedEventArgs e)
        {
            GeneralizationSh modelElement = e.ModelElement as GeneralizationSh;

            ReferenceModelClassAddConfiguration.Check(modelElement.SubclassSh as ReferenceModelClass);

            modelElement.UpdatePropertyRelations();

            base.ElementAdded(e);
        }
    }

    [RuleOn(typeof(GeneralizationSh), FireTime = TimeToFire.Inline, InitiallyDisabled = false)]
    internal sealed class GeneralizationShDeleteConfiguration : DeleteRule
    {
        public override void ElementDeleted(ElementDeletedEventArgs e)
        {
            GeneralizationSh modelElement = e.ModelElement as GeneralizationSh;

            //ReferenceModelClassAddConfiguration.Check(modelElement.SuperclassSh as ReferenceModelClass, modelElement.SubclassSh as ReferenceModelClass);

            modelElement.DeletePropertyRelations();

            base.ElementDeleted(e);
        }
    }

    [RuleOn(typeof(Operation), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class OperationStartConfiguration : AddRule
    {
        public override void ElementAdded(ElementAddedEventArgs e)
        {
            base.ElementAdded(e);

            Operation operation = e.ModelElement as Operation;

            if (operation != null)
            {
                if (operation.OverloadName.IsNullOrEmpty())
                {
                    operation.OverloadName = operation.Name;
                }
            }
        }
    }

    [RuleOn(typeof(Operation), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class OperationChangeConfiguration : ChangeRule
    {
        public override void ElementPropertyChanged(ElementPropertyChangedEventArgs e)
        {
            if (e.DomainProperty.Name == "Name" && !e.OldValue.IsNullOrEmpty() && !e.NewValue.IsNullOrEmpty())
            {
                Operation operation = e.ModelElement as Operation;
                if (operation != null)
                {
                    string oldName = e.OldValue as string, newName = e.NewValue as string;
                    if (operation.OverloadName == oldName || operation.OverloadName.IsNullOrEmpty())
                    {
                        operation.OverloadName = newName;
                    }
                }
            }
            base.ElementPropertyChanged(e);
        }
    }


    #endregion


    /// <summary>  
    /// /// Domain model class allows extra reflective elements such as rules to be added  
    /// /// </summary>  
    public partial class BusinessDataModelDesignerDomainModel
    {
        protected override Type[] GetCustomDomainModelTypes()
        {
            return new System.Type[] 
            { 
                typeof(DbProviderStartConfiguration), 
                typeof(DbProviderChangeConfiguration), 
                typeof(DbProviderdeleteConfiguration), 
                typeof(ReferenceModelClassAddConfiguration),
                //typeof(ReferenceModelClassPropConfiguration),
                typeof(ModelAttributeAddConfiguration),
                typeof(ClassOperationAddConfiguration),
                typeof(ClassOperationDeletingConfiguration),
                typeof(ModelIndexAddConfiguration),
                typeof(ModelIndexDeletingConfiguration),
                typeof(ModelClassAddConfiguration),
                typeof(AssociationAddConfiguration),
                typeof(MultipleAssociationTargetAddConfiguration),
                typeof(MultipleAssociationOriginAddConfiguration),
                typeof(MultipleAssociationOriginPropConfiguration),
                typeof(AssociationDeleteConfiguration),
                typeof(ModelAttributePropConfiguration),
                typeof(MultipleAssociationOriginDeleteConfiguration),
                typeof(AssociationPropConfiguration),
                typeof(GeneralizationAddConfiguration),
                typeof(GeneralizationDeleteConfiguration),
                typeof(MultipleAssociationTargetDeleteConfiguration),
                typeof(ModelClassPropConfiguration),
                typeof(ModelClassChangeConfiguration),
                typeof(ModelClassDeletingConfiguration),
                typeof(ReferenceModelClassDeletingConfiguration),
                typeof(ModelAttributeDeletingConfiguration),
                typeof(GeneralizationShAddConfiguration),
                typeof(GeneralizationShDeleteConfiguration),
                typeof(OperationStartConfiguration),
                typeof(OperationChangeConfiguration),
                typeof(ModelImplementationReferencesModelInterfaceStartConfiguration),
                typeof(WebApiControllerChangeConfiguration),
                typeof(WebApiControllerDeletingConfiguration)
            };
        }
    }
}
