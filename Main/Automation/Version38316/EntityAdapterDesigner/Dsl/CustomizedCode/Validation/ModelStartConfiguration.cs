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
using DslModeling = global::Microsoft.VisualStudio.Modeling;

namespace Linx.EntityAdapterDesigner
{

    [RuleOn(typeof(DslModeling::ModelElement), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class ModelElementStartConfiguration : AddRule
    {
        public override void ElementAdded(ElementAddedEventArgs e)
        {
            EntityAdapterDesignerRoot root = e.ModelElement.GetPropertyValue("EntityAdapterDesignerRoot") as EntityAdapterDesignerRoot;

            if (root != null && root.IsInPresentationDesigner() && !this.IsPresentationElement(e.ModelElement))
                throw new Exception("This element cannot be inserted into a Presentation Designer!!!");

            base.ElementAdded(e);
        }

        private bool IsPresentationElement(DslModeling::ModelElement element)
        {
            return element is Subscription || element is EntityAdapterUserInterface || element is EntityAdapterUserInterfaceReferencesSubscription
                    || element is EntityAdapterDesignerRootHasSubscriptions || element is EntityAdapterDesignerRootHasEntityAdapterUserInterfaces;
        }
    }

    [RuleOn(typeof(OlapCatalog), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class OlapCatalogStartConfiguration : AddRule
    {
        public override void ElementAdded(ElementAddedEventArgs e)
        {
            OlapCatalog catalog = e.ModelElement as OlapCatalog;

            if (catalog != null && catalog.Server.IsNullOrEmpty())
            {
                catalog.Config();
            }
        }
    }

    [RuleOn(typeof(EntityAdapterRepresentation), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class EntityAdapterRepresentationStartConfiguration : AddRule
    {
        public override void ElementAdded(ElementAddedEventArgs e)
        {
            EntityAdapterRepresentation repre = e.ModelElement as EntityAdapterRepresentation;

            if (repre != null && repre.EntityAdapterDesignerRoot.GetDTE() != null && (!repre.BusinessObject.IsNullOrEmpty() && !File.Exists(repre.BusinessObject)))
            {
                //Correct reference
                string serviceBusPath = repre.EntityAdapterDesignerRoot.GetFullPath("Linx.Web.Service.Bus");
                if (System.IO.Directory.Exists(serviceBusPath))
                {
                    string assemblyFile = Path.Combine(serviceBusPath, "bin\\" + Path.GetFileName(repre.BusinessObject));
                    if (System.IO.File.Exists(assemblyFile))
                    {
                        using (Transaction transaction =
                       repre.Store.TransactionManager.BeginTransaction("Changing Representation file path."))
                        {
                            repre.BusinessObject = assemblyFile;
                            transaction.Commit();
                        }
                    }
                }
            }

            base.ElementAdded(e);
        }
    }

    [RuleOn(typeof(Subscription), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class SubscriptionStartConfiguration : AddRule
    {
        public override void ElementAdded(ElementAddedEventArgs e)
        {
            Subscription subscription = e.ModelElement as Subscription;

            if (subscription != null && (subscription.BusinessObjectPath.IsNullOrEmpty() || !File.Exists(subscription.BusinessObjectPath)))
            {
                //Correct reference
                if (!subscription.BusinessObjectPath.IsNullOrEmpty())
                {
                    string serviceBusPath = subscription.EntityAdapterDesignerRoot.GetFullPath("Linx.Web.Service.Bus");
                    if (System.IO.Directory.Exists(serviceBusPath))
                    {
                        string assemblyFile = Path.Combine(serviceBusPath, "bin\\" + Path.GetFileName(subscription.BusinessObjectPath));
                        if (System.IO.File.Exists(assemblyFile))
                        {
                            using (Transaction transaction =
                           subscription.Store.TransactionManager.BeginTransaction("Changing Publisher file path."))
                            {
                                subscription.Name = Path.GetFileNameWithoutExtension(assemblyFile).Replace(".", " ").Replace("_", " ").Proper().Replace(" ", String.Empty);
                                subscription.BusinessObjectPath = assemblyFile;
                                subscription.Title = Path.GetFileNameWithoutExtension(assemblyFile).Replace(".", " ").Replace("_", " ");
                                transaction.Commit();
                            }
                        }
                    }
                }

                if (subscription.BusinessObjectPath.IsNullOrEmpty() || !File.Exists(subscription.BusinessObjectPath))
                {
                    OpenFileDialog fileDlg = new OpenFileDialog();
                    fileDlg.CheckFileExists = true;
                    fileDlg.FileName = "";
                    fileDlg.Filter = "Assembly only|*.dll";
                    fileDlg.Title = "Select the Publisher Business Object (Assembly File).";
                    fileDlg.InitialDirectory = Path.Combine(subscription.EntityAdapterDesignerRoot.GetFullPath("Linx.Web.Service.Bus"), "bin");

                    if (fileDlg.ShowDialog() == DialogResult.OK)
                    {
                        if (!File.Exists(fileDlg.FileName))
                            throw new Exception("Publisher file does not exists!!!");
                        else if (Path.GetExtension(fileDlg.FileName).ToLower() != ".dll")
                            throw new Exception("Publisher extension is invalid!!!");
                    }
                    else
                        throw new Exception("Operation cancelled!!!");

                    if (subscription.EntityAdapterDesignerRoot.GetAssemblyName() == Path.GetFileNameWithoutExtension(fileDlg.FileName))
                        throw new Exception("The selected assembly represents this BO. Each BO subscribes its publications automatically. Operation cancelled!!!");

                    string publisherName = Path.GetFileNameWithoutExtension(fileDlg.FileName).Replace(".", " ").Replace("_", " ").Proper().Replace(" ", String.Empty);
                    if (subscription.EntityAdapterDesignerRoot.Subscriptions.Where(p => p.Name == publisherName).Count() > 0)
                        throw new Exception("This Publisher is already being used!!!");

                    using (Transaction transaction =
                               subscription.Store.TransactionManager.BeginTransaction("Changing Publisher file path."))
                    {
                        subscription.Name = publisherName;
                        subscription.BusinessObjectPath = fileDlg.FileName;
                        subscription.Title = Path.GetFileNameWithoutExtension(fileDlg.FileName).Replace(".", " ").Replace("_", " ");
                        transaction.Commit();
                    }
                }
            }

            if (!subscription.IsNull() && subscription.Publisher.IsNull() && System.IO.File.Exists(subscription.BusinessObjectPath))
            {
                using (Transaction transaction =
                           subscription.Store.TransactionManager.BeginTransaction("Changing Publisher Structure."))
                {
                    subscription.Publisher = new CustomizedCode.PublicationStructure(subscription.BusinessObjectPath);
                    transaction.Commit();
                }
            }

            base.ElementAdded(e);
        }
    }

    [RuleOn(typeof(EntityDataModel), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class EdmStartConfiguration : AddRule
    {
        public override void ElementAdded(ElementAddedEventArgs e)
        {
            EntityDataModel edm = e.ModelElement as EntityDataModel;

            if (edm.EntityAdapterDesignerRoot.EntityDataModels.Count > 1)
                throw new Exception("Already exists one EntityDataModel in this context!!!");

            edm.AddNewEdmReference();

            base.ElementAdded(e);
        }


    }

    [RuleOn(typeof(DomainServiceExtension), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class DomainServiceExtensionDeleteConfiguration : DeleteRule
    {
        public override void ElementDeleted(ElementDeletedEventArgs e)
        {
            base.ElementDeleted(e);
        }
    }


    [RuleOn(typeof(EntityAdapterUserInterface), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class EntityAdapterUserInterfaceDeletingConfiguration : DeletingRule
    {
        public override void ElementDeleting(ElementDeletingEventArgs e)
        {
            EntityAdapterUserInterface ui = e.ModelElement as EntityAdapterUserInterface;
            if (ui != null)
            {
                if (MessageBox.Show("Do you really want to delete the user interface '" + ui.Name + "'?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    throw new Exception("Operation cancelled!!!");
                }
                ui.DeleteSpaSourceFiles();
                //ui.DeleteMobileSourceFiles();
                if (ui.IsDefault)
                {
                    if (!ui.EntityAdapter.IsNull())
                    {
                        EntityAdapterUserInterface firstUI = ui.EntityAdapter.EntityAdapterUserInterfaces.Where(u => u != ui).FirstOrDefault();
                        if (!firstUI.IsNull())
                        {
                            ui.IsDefault = false;
                            firstUI.IsDefault = true;
                        }
                    }
                }
            }
            base.ElementDeleting(e);
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

    [RuleOn(typeof(EntityAdapterReferencesEntityDataModel), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class EdmLinkStartConfiguration : AddRule
    {
        public override void ElementAdded(ElementAddedEventArgs e)
        {
            EntityAdapterReferencesEntityDataModel link = e.ModelElement as EntityAdapterReferencesEntityDataModel;

            if (link != null)
            {
                //if (link.EntityAdapter.TargetEntityAdapter != null)
                //    throw new Exception("This entity is a detail!!!");

                if (link.EntityAdapter.BaseEntityAdapter != null)
                    throw new Exception("This entity is a derived class!!!");

                if (link.EntityAdapter.GetEntityAdapterRepresentation() != null)
                    throw new Exception("This entity is connected to a Representation Graph!!!");
            }
            base.ElementAdded(e);
        }

    }


    [RuleOn(typeof(EntityInstanceReferencesEntityOwners), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class EntityInstanceReferencesEntityOwnersStartConfiguration : AddRule
    {
        public override void ElementAdded(ElementAddedEventArgs e)
        {
            EntityInstanceReferencesEntityOwners link = e.ModelElement as EntityInstanceReferencesEntityOwners;

            if (link != null)
            {
                if (link.Name.IsNullOrEmpty())
                {
                    link.Name = link.SourceEntityAdapter.Name;
                }
            }
            base.ElementAdded(e);
        }
    }

    [RuleOn(typeof(EntityCollectionReferencesEntityOwners), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class EntityCollectionReferencesEntityOwnersStartConfiguration : AddRule
    {
        public override void ElementAdded(ElementAddedEventArgs e)
        {
            EntityCollectionReferencesEntityOwners link = e.ModelElement as EntityCollectionReferencesEntityOwners;

            if (link != null)
            {
                if (link.Name.IsNullOrEmpty())
                {
                    link.Name = link.SourceEntityAdapter.Name + "List";
                }
            }
            base.ElementAdded(e);
        }
    }


    [RuleOn(typeof(EntityAdapterRepresentationReferencesTargetEntityAdapterRepresentation), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class EntityAdapterRepresentationReferencesTargetEntityAdapterRepresentationStartConfiguration : AddRule
    {
        public override void ElementAdded(ElementAddedEventArgs e)
        {
            EntityAdapterRepresentationReferencesTargetEntityAdapterRepresentation link = e.ModelElement as EntityAdapterRepresentationReferencesTargetEntityAdapterRepresentation;

            if (link != null)
            {
                if (link.SourceProperties.IsNullOrEmpty() || link.TargetProperties.IsNullOrEmpty())
                {
                    if (CustomizedCode.FormEntityJoinRelation.IsValid(link))
                    {
                        CustomizedCode.FormEntityJoinRelation frmEditRelation = new CustomizedCode.FormEntityJoinRelation(link);
                        frmEditRelation.ShowDialog();
                    }
                    else
                        throw new Exception("Verify if the two representations are correctly defined!");
                }
            }
            base.ElementAdded(e);
        }

    }


    [RuleOn(typeof(EntityAdapterReferencesEntityAdapterRepresentation), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class EntityAdapterRepresentationLinkStartConfiguration : AddRule
    {
        public override void ElementAdded(ElementAddedEventArgs e)
        {
            EntityAdapterReferencesEntityAdapterRepresentation link = e.ModelElement as EntityAdapterReferencesEntityAdapterRepresentation;

            if (link != null)
            {
                if (link.EntityAdapter.BaseEntityAdapter != null)
                    throw new Exception("This entity is a derived class!!!");

                if (link.EntityAdapter.EntityDataModel != null)
                    throw new Exception("This entity is connected to a EDM!!!");

            }
            base.ElementAdded(e);
        }

    }


    [RuleOn(typeof(EntityAdapterRepresentationReferencesTargetEntityAdapterRepresentation), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class RepresentationLinkStartConfiguration : AddRule
    {
        public override void ElementAdded(ElementAddedEventArgs e)
        {
            EntityAdapterRepresentationReferencesTargetEntityAdapterRepresentation link = e.ModelElement as EntityAdapterRepresentationReferencesTargetEntityAdapterRepresentation;

            if (link != null)
            {
                if (link.TargetEntityAdapterRepresentation == link.SourceEntityAdapterRepresentation)
                    throw new Exception("Auto relation is not allowed!!!");

                //Check cyclic relation
                EntityAdapterRepresentation target = link.TargetEntityAdapterRepresentation.TargetEntityAdapterRepresentation;
                while (target != null)
                {
                    if (target == link.SourceEntityAdapterRepresentation)
                        throw new Exception("Cyclic relation is not allowed!!!");
                    target = target.TargetEntityAdapterRepresentation;
                }

            }
            base.ElementAdded(e);
        }
    }


    [RuleOn(typeof(EntityAdapterReferencesTargetEntityAdapter), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class EntityLinkStartConfiguration : AddRule
    {
        public override void ElementAdded(ElementAddedEventArgs e)
        {
            EntityAdapterReferencesTargetEntityAdapter link = e.ModelElement as EntityAdapterReferencesTargetEntityAdapter;

            if (link != null)
            {
                if (link.TargetEntityAdapter == link.SourceEntityAdapter)
                    throw new Exception("Auto parent relation is not allowed!!!");

                if (link.TargetEntityAdapter.TargetEntityAdapter == link.SourceEntityAdapter)
                    throw new Exception("Cyclic parent relation is not allowed!!!");

                var parentSource = link.SourceEntityAdapter.GetBaseLinkRelation();
                if (parentSource != null && ((link.TargetEntityAdapter == parentSource.SourceEntityAdapter && link.SourceEntityAdapter == parentSource.TargetEntityAdapter) || (link.TargetEntityAdapter == parentSource.TargetEntityAdapter && link.SourceEntityAdapter == parentSource.SourceEntityAdapter)))
                    throw new Exception("There is already a inheritance relationship between these entities!!!");

                var parentTarget = link.TargetEntityAdapter.GetBaseLinkRelation();
                if (parentTarget != null && ((link.TargetEntityAdapter == parentTarget.SourceEntityAdapter && link.SourceEntityAdapter == parentTarget.TargetEntityAdapter) || (link.TargetEntityAdapter == parentTarget.TargetEntityAdapter && link.SourceEntityAdapter == parentTarget.SourceEntityAdapter)))
                    throw new Exception("There is already a inheritance relationship between these entities!!!");

                if (link.TargetEntityAdapter.LocalEntityAdapter != null)
                    throw new Exception("The master entity cannot be a local view!!!");
            }
            base.ElementAdded(e);
        }
    }

    [RuleOn(typeof(EntityAdapterReferencesBaseEntityAdapter), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class EntityLinkBaseStartConfiguration : AddRule
    {
        public override void ElementAdded(ElementAddedEventArgs e)
        {
            EntityAdapterReferencesBaseEntityAdapter link = e.ModelElement as EntityAdapterReferencesBaseEntityAdapter;

            if (link != null)
            {
                if (link.TargetEntityAdapter == link.SourceEntityAdapter)
                    throw new Exception("Auto derivation is not allowed!!!");

                if (link.TargetEntityAdapter.BaseEntityAdapter == link.SourceEntityAdapter)
                    throw new Exception("Cyclic derivation is not allowed!!!");

                if (link.SourceEntityAdapter.EntityDataModel != null)
                    throw new Exception("This entity already is linked to the Entity Data Model!!!");

                var parentLink = link.SourceEntityAdapter.GetParentLinkRelation();
                if (parentLink != null && ((link.TargetEntityAdapter == parentLink.SourceEntityAdapter && link.SourceEntityAdapter == parentLink.TargetEntityAdapter) || (link.TargetEntityAdapter == parentLink.TargetEntityAdapter && link.SourceEntityAdapter == parentLink.SourceEntityAdapter)))
                    throw new Exception("There is already a Master/Detail relationship between these entities!!!");

                parentLink = link.TargetEntityAdapter.GetParentLinkRelation();
                if (parentLink != null && ((link.TargetEntityAdapter == parentLink.SourceEntityAdapter && link.SourceEntityAdapter == parentLink.TargetEntityAdapter) || (link.TargetEntityAdapter == parentLink.TargetEntityAdapter && link.SourceEntityAdapter == parentLink.SourceEntityAdapter)))
                    throw new Exception("There is already a Master/Detail relationship between these entities!!!");

                var localEntityLink = EntityAdapterReferencesLocalEntityAdapter.GetLinkToLocalEntityAdapter(link.SourceEntityAdapter);
                if (localEntityLink != null && ((link.TargetEntityAdapter == localEntityLink.SourceEntityAdapter && link.SourceEntityAdapter == localEntityLink.TargetEntityAdapter) || (link.TargetEntityAdapter == localEntityLink.TargetEntityAdapter && link.SourceEntityAdapter == localEntityLink.SourceEntityAdapter)))
                    throw new Exception("There is already a local relationship between these entities!!!");

                localEntityLink = EntityAdapterReferencesLocalEntityAdapter.GetLinkToLocalEntityAdapter(link.TargetEntityAdapter);
                if (localEntityLink != null && ((link.TargetEntityAdapter == localEntityLink.SourceEntityAdapter && link.SourceEntityAdapter == localEntityLink.TargetEntityAdapter) || (link.TargetEntityAdapter == localEntityLink.TargetEntityAdapter && link.SourceEntityAdapter == localEntityLink.SourceEntityAdapter)))
                    throw new Exception("There is already a local relationship between these entities!!!");

                if (link.TargetEntityAdapter.LocalEntityAdapter != null || link.SourceEntityAdapter.LocalEntityAdapter != null)
                    throw new Exception("There is a local relationship with one these elements!!!");

            }

            EntityAdapter derivedEntity = link.SourceEntityAdapter;
            EntityAdapter baseEntity = link.TargetEntityAdapter;
            base.ElementAdded(e);
            derivedEntity.AdjustColorShape();
            derivedEntity.UpdateBaseClassInfo();
            derivedEntity.RemoveInheritanceConflicts();
        }
    }


    [RuleOn(typeof(EntityAdapterReferencesBaseEntityAdapter), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class EntityLinkBaseDeleteConfiguration : DeleteRule
    {
        public override void ElementDeleted(ElementDeletedEventArgs e)
        {
            EntityAdapterReferencesBaseEntityAdapter link = e.ModelElement as EntityAdapterReferencesBaseEntityAdapter;
            EntityAdapter source = link.SourceEntityAdapter;
            base.ElementDeleted(e);
            source.AdjustColorShape();
        }
    }

    [RuleOn(typeof(LookUpAdapterReferencesBaseLookUpAdapter), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class LookUpLinkBaseStartConfiguration : AddRule
    {
        public override void ElementAdded(ElementAddedEventArgs e)
        {
            LookUpAdapterReferencesBaseLookUpAdapter link = e.ModelElement as LookUpAdapterReferencesBaseLookUpAdapter;

            if (link != null)
            {
                if (link.TargetLookUpAdapter == link.SourceLookUpAdapter)
                    throw new Exception("Auto derivation is not allowed!!!");

                if (link.TargetLookUpAdapter.BaseLookUpAdapter == link.SourceLookUpAdapter)
                    throw new Exception("cyclic derivation is not allowed!!!");
            }

            LookUpAdapter source = link.SourceLookUpAdapter;
            base.ElementAdded(e);
            source.AdjustColorShape();
            source.UpdateBaseClassInfo();
            source.RemoveInheritanceConflicts();
        }
    }

    [RuleOn(typeof(LookUpAdapterReferencesBaseLookUpAdapter), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class LookUpLinkBaseDeleteConfiguration : DeleteRule
    {
        public override void ElementDeleted(ElementDeletedEventArgs e)
        {
            LookUpAdapterReferencesBaseLookUpAdapter link = e.ModelElement as LookUpAdapterReferencesBaseLookUpAdapter;
            LookUpAdapter source = link.SourceLookUpAdapter;
            base.ElementDeleted(e);
            source.AdjustColorShape();
        }
    }


    [RuleOn(typeof(UserInterfaceReferencesBaseUserInterface), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class UserInterfaceLinkBaseStartConfiguration : AddRule
    {
        public override void ElementAdded(ElementAddedEventArgs e)
        {
            UserInterfaceReferencesBaseUserInterface link = e.ModelElement as UserInterfaceReferencesBaseUserInterface;

            if (link != null)
            {
                if (link.TargetEntityAdapterUserInterface == link.SourceEntityAdapterUserInterface)
                    throw new Exception("Auto derivation is not allowed!!!");

                if (link.TargetEntityAdapterUserInterface.BaseUserInterface == link.SourceEntityAdapterUserInterface)
                    throw new Exception("cyclic derivation is not allowed!!!");
            }

            EntityAdapterUserInterface derivedEntity = link.SourceEntityAdapterUserInterface;
            EntityAdapterUserInterface baseEntity = link.TargetEntityAdapterUserInterface;
            base.ElementAdded(e);
            derivedEntity.AdjustColorShape();

            if (!e.ModelElement.Store.TransactionManager.CurrentTransaction.IsSerializing && !derivedEntity.LayoutContent.IsNullOrEmpty())
            {
                derivedEntity.LayoutContent = String.Empty;
            }

        }
    }


    [RuleOn(typeof(UserInterfaceReferencesBaseUserInterface), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class UserInterfaceLinkBaseDeleteConfiguration : DeleteRule
    {
        public override void ElementDeleted(ElementDeletedEventArgs e)
        {
            UserInterfaceReferencesBaseUserInterface link = e.ModelElement as UserInterfaceReferencesBaseUserInterface;
            EntityAdapterUserInterface source = link.SourceEntityAdapterUserInterface;
            base.ElementDeleted(e);
            source.AdjustColorShape();
        }
    }


    [RuleOn(typeof(EntityAdapterReferencesLocalEntityAdapter), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class EntityLinkLocalStartConfiguration : AddRule
    {
        public override void ElementAdded(ElementAddedEventArgs e)
        {
            EntityAdapterReferencesLocalEntityAdapter link = e.ModelElement as EntityAdapterReferencesLocalEntityAdapter;

            if (link != null)
            {
                if (link.TargetEntityAdapter == link.SourceEntityAdapter)
                    throw new Exception("Auto relation is not allowed!!!");

                if (link.TargetEntityAdapter.LocalEntityAdapter == link.SourceEntityAdapter)
                    throw new Exception("cyclic relation is not allowed!!!");

                if (link.SourceEntityAdapter.EntityDataModel != null)
                    throw new Exception("This entity already is linked to the Entity Data Model!!!");

                if (link.TargetEntityAdapter.TargetEntityAdapter != null)
                {
                    var parentLink = link.SourceEntityAdapter.GetParentLinkRelation();
                    if (parentLink != null && ((link.TargetEntityAdapter == parentLink.SourceEntityAdapter && link.SourceEntityAdapter == parentLink.TargetEntityAdapter) || (link.TargetEntityAdapter == parentLink.TargetEntityAdapter && link.SourceEntityAdapter == parentLink.SourceEntityAdapter)))
                        throw new Exception("There is already a Master/Detail relationship between these entities!!!");

                    parentLink = link.TargetEntityAdapter.GetParentLinkRelation();
                    if (parentLink != null && ((link.TargetEntityAdapter == parentLink.SourceEntityAdapter && link.SourceEntityAdapter == parentLink.TargetEntityAdapter) || (link.TargetEntityAdapter == parentLink.TargetEntityAdapter && link.SourceEntityAdapter == parentLink.SourceEntityAdapter)))
                        throw new Exception("There is already a Master/Detail relationship between these entities!!!");
                }

                var inheritanceEntityLink = EntityAdapterReferencesBaseEntityAdapter.GetLinkToBaseEntityAdapter(link.SourceEntityAdapter);
                if (inheritanceEntityLink != null && ((link.TargetEntityAdapter == inheritanceEntityLink.SourceEntityAdapter && link.SourceEntityAdapter == inheritanceEntityLink.TargetEntityAdapter) || (link.TargetEntityAdapter == inheritanceEntityLink.TargetEntityAdapter && link.SourceEntityAdapter == inheritanceEntityLink.SourceEntityAdapter)))
                    throw new Exception("There is already an inheritance relationship between these entities!!!");

                inheritanceEntityLink = EntityAdapterReferencesBaseEntityAdapter.GetLinkToBaseEntityAdapter(link.TargetEntityAdapter);
                if (inheritanceEntityLink != null && ((link.TargetEntityAdapter == inheritanceEntityLink.SourceEntityAdapter && link.SourceEntityAdapter == inheritanceEntityLink.TargetEntityAdapter) || (link.TargetEntityAdapter == inheritanceEntityLink.TargetEntityAdapter && link.SourceEntityAdapter == inheritanceEntityLink.SourceEntityAdapter)))
                    throw new Exception("There is already an inheritance relationship between these entities!!!");

                if (link.TargetEntityAdapter.BaseEntityAdapter != null || link.SourceEntityAdapter.BaseEntityAdapter != null)
                    throw new Exception("There is an inheritance relationship with one these elements!!!");

            }

            EntityAdapter source = link.SourceEntityAdapter;
            base.ElementAdded(e);
            source.UpdateLocalEntityInfo();
            source.AdjustColorShape();
        }
    }

    [RuleOn(typeof(EntityAdapterReferencesLocalEntityAdapter), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class EntityLinkLocalDeleteConfiguration : DeleteRule
    {
        public override void ElementDeleted(ElementDeletedEventArgs e)
        {
            EntityAdapterReferencesLocalEntityAdapter link = e.ModelElement as EntityAdapterReferencesLocalEntityAdapter;
            EntityAdapter source = link.SourceEntityAdapter;
            base.ElementDeleted(e);
            source.AdjustColorShape();
        }
    }


    [RuleOn(typeof(EntityAdapterUserInterface), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class EntityAdapterUserInterfaceStartConfiguration : AddRule
    {
        public override void ElementAdded(ElementAddedEventArgs e)
        {
            EntityAdapterUserInterface element = e.ModelElement as EntityAdapterUserInterface;

            if (element != null)
            {
                element.CheckSize();

                if (element.Subscription == null && element.EntityAdapter == null && element.IsDefault)
                    element.IsDefault = false;

                if (element.SolutionName.IsNullOrEmpty())
                {
                    element.SolutionName = element.EntityAdapterDesignerRoot.GetSolutionName();
                }
                else
                {
                    string solitioName = element.EntityAdapterDesignerRoot.GetSolutionName();
                    if (element.SolutionName != solitioName)
                        element.SolutionName = solitioName;
                }

                if (element.PageSize == 0)
                    element.PageSize = 100;

                if (!element.EntityAdapter.IsNull())
                    element.EntityAdapter.CheckDefaultUserInterface();
            }
            base.ElementAdded(e);
        }
    }


    [RuleOn(typeof(GenericOperation), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class GenericOperationStartConfiguration : AddRule
    {
        public override void ElementAdded(ElementAddedEventArgs e)
        {
            base.ElementAdded(e);

            GenericOperation operation = e.ModelElement as GenericOperation;

            if (operation != null)
            {
                if (operation.OverloadName.IsNullOrEmpty())
                {
                    operation.OverloadName = operation.Name;
                }
            }
        }
    }

    [RuleOn(typeof(EntityAdapterUserInterfaceReferencesEntityAdapter), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class EntityAdapterReferencesEntityAdapterUserInterfacesStartConfiguration : AddRule
    {
        public override void ElementAdded(ElementAddedEventArgs e)
        {
            base.ElementAdded(e);

            EntityAdapterUserInterfaceReferencesEntityAdapter uiLink = e.ModelElement as EntityAdapterUserInterfaceReferencesEntityAdapter;

            if (uiLink != null)
            {
                if (uiLink.EntityAdapterUserInterface.Subscription != null)
                    throw new Exception("This interface is already connected to a subscription!!!");

                if (!uiLink.EntityAdapter.IsNull())
                    uiLink.EntityAdapter.CheckDefaultUserInterface();

                if (!e.ModelElement.Store.TransactionManager.CurrentTransaction.IsSerializing && !uiLink.EntityAdapterUserInterface.LayoutContent.IsNullOrEmpty())
                {
                    uiLink.EntityAdapterUserInterface.LayoutContent = String.Empty;
                }
            }
        }
    }


    [RuleOn(typeof(EntityAdapterUserInterfaceReferencesSubscription), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class EntityAdapterReferencesSubscriptionStartConfiguration : AddRule
    {
        public override void ElementAdded(ElementAddedEventArgs e)
        {
            base.ElementAdded(e);

            EntityAdapterUserInterfaceReferencesSubscription uiLink = e.ModelElement as EntityAdapterUserInterfaceReferencesSubscription;

            if (uiLink != null)
            {
                if (uiLink.EntityAdapterUserInterface.EntityAdapter != null)
                    throw new Exception("This interface is already connected to a view!!!");
            }
        }
    }

    [RuleOn(typeof(RepositoryImplementationReferencesRepositoryInterface), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class RepositoryImplementationReferencesRepositoryInterfaceStartConfiguration : AddRule
    {
        public override void ElementAdded(ElementAddedEventArgs e)
        {
            base.ElementAdded(e);

            RepositoryImplementationReferencesRepositoryInterface implLink = e.ModelElement as RepositoryImplementationReferencesRepositoryInterface;

            if (implLink != null && implLink.RepositoryInterface != null)
            {
                implLink.RepositoryInterface.CheckDefaultImplementation();
            }
        }
    }


    [RuleOn(typeof(GenericOperation), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class GenericOperationChangeConfiguration : ChangeRule
    {
        public override void ElementPropertyChanged(ElementPropertyChangedEventArgs e)
        {
            if (e.DomainProperty.Name == "Name" && !e.OldValue.IsNullOrEmpty() && !e.NewValue.IsNullOrEmpty())
            {
                GenericOperation operation = e.ModelElement as GenericOperation;
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


    [RuleOn(typeof(EntityAdapterProperty), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class EntityAdapterPropertyAddConfiguration : AddRule
    {
        public override void ElementAdded(ElementAddedEventArgs e)
        {
            EntityAdapterProperty property = e.ModelElement as EntityAdapterProperty;
            if (property != null && !property.Datatype.ToLower().Contains("string") && !property.Precision.IsNullOrEmpty() && (property.Precision + ":").Left(":").Length >= 3)
            {
                string precision = property.Precision + ":";
                property.Precision = precision.Left(":").Left(2) + ":" + precision.Left(":").Substring(2);
                if (!property.Datatype.Contains("[]") && (property.Datatype.ToLower().Contains("decimal") || property.Datatype.ToLower().Contains("float") || property.Datatype.ToLower().Contains("double")))
                    property.DataFormatString = "N" + (property.Precision.Right(":").IsNullOrEmpty() ? "0" : property.Precision.Right(":"));
            }

            base.ElementAdded(e);
        }
    }


    [RuleOn(typeof(EntityAdapterProperty), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class EntityAdapterPropertyChangeConfiguration : ChangeRule
    {
        public override void ElementPropertyChanged(ElementPropertyChangedEventArgs e)
        {
            if (e.DomainProperty.Name == "IgnoreMetaData")
            {
                EntityAdapterProperty property = e.ModelElement as EntityAdapterProperty;
                if (property != null && property.IgnoreMetaData && !property.EntityAdapter.EnableMetaDataFilter)
                {
                    property.EntityAdapter.EnableMetaDataFilter = true;
                }
            }
            base.ElementPropertyChanged(e);
        }
    }

    [RuleOn(typeof(LookUpProperty), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class LookUpPropertyPropertyAddConfiguration : AddRule
    {
        public override void ElementAdded(ElementAddedEventArgs e)
        {
            LookUpProperty property = e.ModelElement as LookUpProperty;
            if (property != null && !property.Datatype.ToLower().Contains("string") && !property.Precision.IsNullOrEmpty() && (property.Precision + ":").Left(":").Length >= 3)
            {
                string precision = property.Precision + ":";
                property.Precision = precision.Left(":").Left(2) + ":" + precision.Left(":").Substring(2);
                if (!property.Datatype.Contains("[]") && (property.Datatype.ToLower().Contains("decimal") || property.Datatype.ToLower().Contains("float") || property.Datatype.ToLower().Contains("double")))
                    property.DataFormatString = "N" + (property.Precision.Right(":").IsNullOrEmpty() ? "0" : property.Precision.Right(":"));
            }

            base.ElementAdded(e);
        }
    }


    [RuleOn(typeof(EntityAdapter), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class EntityAdapterAddConfiguration : AddRule
    {
        public override void ElementAdded(ElementAddedEventArgs e)
        {
            EntityAdapter entity = e.ModelElement as EntityAdapter;
            if (entity != null)
            {
                entity.AdjustColorShape();
            }

            base.ElementAdded(e);
        }
    }


    [RuleOn(typeof(LookUpAdapter), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class LookUpAdapterAddConfiguration : AddRule
    {
        public override void ElementAdded(ElementAddedEventArgs e)
        {
            LookUpAdapter lookUp = e.ModelElement as LookUpAdapter;
            if (lookUp != null)
            {
                lookUp.AdjustColorShape();
            }

            base.ElementAdded(e);
        }
    }

    [RuleOn(typeof(EntityAdapterAttribute), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class EntityAdapterAttributeChangeConfiguration : ChangeRule
    {
        public override void ElementPropertyChanged(ElementPropertyChangedEventArgs e)
        {
            if (e.DomainProperty.Name == "KpiName" && !e.OldValue.Equals(e.NewValue))
            {
                EntityAdapterAttribute attribute = e.ModelElement as EntityAdapterAttribute;
                if (attribute != null)
                {
                    if (attribute.KpiRelatedAttribute.IsNullOrEmpty())
                    {
                        if (!e.OldValue.IsNullOrEmpty())
                            attribute.RemoveKpiPartner();
                        if (!e.NewValue.IsNullOrEmpty())
                            attribute.CreateKpiPartner();
                    }
                }
            }
            else if (e.DomainProperty.Name == "DenormalizedDataInfo" && !e.OldValue.Equals(e.NewValue))
            {
                EntityAdapterProperty attribute = e.ModelElement as EntityAdapterProperty;
                if (attribute != null && attribute.Name != "NormalizedKey")
                {
                    if (attribute.EntityAdapter.BaseEntityAdapter != null && !attribute.DenormalizedDataInfo.IsNullOrEmpty())
                        throw new Exception("Denormalized information cannot be set over derived Business Views!!!");

                    var normalizedAttribute = attribute.EntityAdapter.EntityAdapterProperties.Where(p => p != attribute && p.Name != "NormalizedKey" && !p.DenormalizedDataInfo.IsNullOrEmpty()).FirstOrDefault();
                    if (!e.NewValue.IsNullOrEmpty())
                    {
                        attribute.CreateNormalizedKey();
                    }
                    else if (normalizedAttribute != null)
                    {
                        normalizedAttribute.CreateNormalizedKey();
                    }
                    else
                        attribute.RemoveNormalizedKey();
                }
            }
            else if (e.DomainProperty.Name == "Name" && !e.OldValue.IsNullOrEmpty() && !e.NewValue.IsNullOrEmpty() && !e.OldValue.Equals(e.NewValue))
            {
                EntityAdapterAttribute attribute = e.ModelElement as EntityAdapterAttribute;
                if (attribute != null)
                {
                    if (!attribute.KpiName.IsNullOrEmpty() && attribute.KpiRelatedAttribute.IsNullOrEmpty())
                    {
                        attribute.RemoveKpiPartner(e.OldValue.ToString());
                        attribute.CreateKpiPartner();
                    }
                }
            }
        }
    }

    [RuleOn(typeof(EntityAdapterAttribute), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class EntityAdapterAttributeDeleteConfiguration : DeletingRule
    {
        public override void ElementDeleting(ElementDeletingEventArgs e)
        {
            EntityAdapterAttribute attribute = e.ModelElement as EntityAdapterAttribute;
            if (attribute != null && attribute.KpiRelatedAttribute.IsNullOrEmpty())
                attribute.RemoveKpiPartner();

            base.ElementDeleting(e);
        }
    }

    [RuleOn(typeof(EntityAdapter), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class EntityAdapterChangeConfiguration : ChangeRule
    {
        public override void ElementPropertyChanged(ElementPropertyChangedEventArgs e)
        {
            if (e.OldValue != e.NewValue)
            {
                EntityAdapter entity = e.ModelElement as EntityAdapter;

                switch (e.DomainProperty.Name)
                {
                    case "Name":
                        entity.AdjustDependentResources(e.OldValue as string, e.NewValue as string);
                        break;
                    case "QueryReturnType":
                        //Verify return consistence
                        entity.AdjustQueryReturnType();

                        //Adjust Base Class
                        if (entity.BaseEntityAdapter != null && entity.BaseEntityAdapter.QueryReturnType != entity.QueryReturnType)
                            entity.BaseEntityAdapter.QueryReturnType = entity.QueryReturnType;

                        //Adjust Derived Classes
                        foreach (var derived in entity.DerivedEntityAdapters)
                        {
                            if (derived.QueryReturnType != entity.QueryReturnType)
                                derived.QueryReturnType = entity.QueryReturnType;
                        }
                        break;
                    case "IsDashboardFilter":
                        //Verify return consistence
                        entity.AdjustQueryReturnType();

                        //Adjust Base Class
                        if (entity.BaseEntityAdapter != null && entity.BaseEntityAdapter.QueryReturnType != entity.QueryReturnType)
                            entity.BaseEntityAdapter.QueryReturnType = entity.QueryReturnType;

                        //Adjust Derived Classes
                        foreach (var derived in entity.DerivedEntityAdapters)
                        {
                            if (derived.QueryReturnType != entity.QueryReturnType)
                                derived.QueryReturnType = entity.QueryReturnType;
                        }
                        break;
                    case "IsReadOnly":
                        //Adjust Base Class
                        if (entity.BaseEntityAdapter != null && entity.BaseEntityAdapter.IsReadOnly != entity.IsReadOnly)
                            entity.BaseEntityAdapter.IsReadOnly = entity.IsReadOnly;

                        //Adjust Derived Classes
                        foreach (var derived in entity.DerivedEntityAdapters)
                        {
                            if (derived.IsReadOnly != entity.IsReadOnly)
                                derived.IsReadOnly = entity.IsReadOnly;
                        }
                        break;
                    case "RequeryDetailsAfterSave":
                        //Adjust Base Class
                        if (entity.BaseEntityAdapter != null && entity.BaseEntityAdapter.RequeryDetailsAfterSave != entity.RequeryDetailsAfterSave)
                            entity.BaseEntityAdapter.RequeryDetailsAfterSave = entity.RequeryDetailsAfterSave;

                        //Adjust Derived Classes
                        foreach (var derived in entity.DerivedEntityAdapters)
                        {
                            if (derived.RequeryDetailsAfterSave != entity.RequeryDetailsAfterSave)
                                derived.RequeryDetailsAfterSave = entity.RequeryDetailsAfterSave;
                        }
                        break;
                    case "SizeGridConfigurations":
                        //Adjust Base Class
                        if (entity.BaseEntityAdapter != null && entity.BaseEntityAdapter.SizeGridConfigurations != entity.SizeGridConfigurations)
                            entity.BaseEntityAdapter.SizeGridConfigurations = entity.SizeGridConfigurations;

                        //Adjust Derived Classes
                        foreach (var derived in entity.DerivedEntityAdapters)
                        {
                            if (derived.SizeGridConfigurations != entity.SizeGridConfigurations)
                                derived.SizeGridConfigurations = entity.SizeGridConfigurations;
                        }
                        break;
                    case "CustomBaseType":
                        entity.AdjustColorShape();
                        break;

                    case "BusinessExtension":
                        if (e.NewValue is BusinessExtensions)
                        {
                            BusinessExtensions be = (BusinessExtensions)e.NewValue;

                            if (entity.BaseEntityAdapter != null && be == BusinessExtensions.SKU)
                                throw new Exception("Business Extensions cannot be set over derived Business Views!!!");

                            switch (be)
                            {
                                case BusinessExtensions.None:
                                    entity.RemoveSKU();
                                    break;
                                case BusinessExtensions.SKU:
                                    entity.CreateSKU();
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }


    //[RuleOn(typeof(EntityAdapterProperty), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    //internal sealed class EntityAdapterPropertyChangeConfiguration : ChangeRule
    //{
    //    public override void ElementPropertyChanged(ElementPropertyChangedEventArgs e)
    //    {
    //        if (e.DomainProperty.Name.InList("Name", "DisplayName", "DataFormatString", "Precision", "DomainName") && !e.OldValue.IsNullOrEmpty() && !e.NewValue.IsNullOrEmpty() && !e.OldValue.Equals(e.NewValue))
    //        {

    //            EntityAdapterProperty property = e.ModelElement as EntityAdapterProperty;
    //            if (property != null && property.EntityAdapter != null)
    //            {
    //                string edmKey;
    //                foreach (LookUpAdapter lookUp in property.EntityAdapter.LookUpAdapters.Where(l => !l.IsCustomized))
    //                {
    //                    foreach (LookUpProperty prop in lookUp.LookUpProperties.Where(p => !p.IsCustomized))
    //                    {
    //                        edmKey = lookUp.RelationName + "." + ("#" + prop.EdmKey).Right("#" + lookUp.EntitySource + ".");
    //                        if (property.EntityAdapter.IsTheSameEdmKey(property.EdmKey, edmKey))
    //                        {
    //                            prop.DisplayName = property.DisplayName;
    //                            prop.Name = property.Name;
    //                            prop.EntityPropertyRelated = property.Name;
    //                            prop.DataFormatString = property.DataFormatString;
    //                            prop.Precision = property.Precision;
    //                            prop.DomainName = property.DomainName;
    //                            prop.KpiName = property.KpiName;
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //        base.ElementPropertyChanged(e);
    //    }
    //}


    //[RuleOn(typeof(EntityAdapterPublicationProperty), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    //internal sealed class EntityAdapterPublicationPropertyChangeConfiguration : ChangeRule
    //{
    //    public override void ElementPropertyChanged(ElementPropertyChangedEventArgs e)
    //    {
    //        EntityAdapterPublicationProperty property = e.ModelElement as EntityAdapterPublicationProperty;
    //        if (property != null)
    //        {
    //            if (property.IsCustomized)
    //                property.IsCustomized = false;

    //            if (e.DomainProperty.Name.InList("Name", "DisplayName", "DataFormatString", "Precision", "DomainName") && !e.OldValue.IsNullOrEmpty() && !e.NewValue.IsNullOrEmpty() && !e.OldValue.Equals(e.NewValue))
    //            {
    //                string edmKey;
    //                foreach (LookUpAdapter lookUp in property.EntityAdapter.LookUpAdapters.Where(l => !l.IsCustomized))
    //                {
    //                    foreach (LookUpProperty prop in lookUp.LookUpProperties.Where(p => !p.IsCustomized))
    //                    {
    //                        edmKey = lookUp.RelationName + "." + ("#" + prop.EdmKey).Right("#" + lookUp.EntitySource + ".");
    //                        if (property.EntityAdapter.IsTheSameEdmKey(property.EdmKey, edmKey))
    //                        {
    //                            prop.DisplayName = property.DisplayName;
    //                            prop.Name = property.Name;
    //                            prop.EntityPropertyRelated = property.Name;
    //                            prop.DataFormatString = property.DataFormatString;
    //                            prop.Precision = property.Precision;
    //                            prop.DomainName = property.DomainName;
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //        base.ElementPropertyChanged(e);
    //    }
    //}



    [RuleOn(typeof(EntityAdapterUserInterface), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class EntityAdapterUserInterfaceChangeConfiguration : ChangeRule
    {
        public override void ElementPropertyChanged(ElementPropertyChangedEventArgs e)
        {
            EntityAdapterUserInterface userInterface = e.ModelElement as EntityAdapterUserInterface;
            if (userInterface != null)
            {
                if (e.DomainProperty.Name == "IsDefault")
                {
                    if (userInterface.EntityAdapter != null)
                    {

                        if (e.OldValue != null && !e.OldValue.Equals(e.NewValue) && ((bool)e.NewValue) && userInterface.EntityAdapter.EntityAdapterUserInterfaces.Count > 1)
                        {
                            foreach (EntityAdapterUserInterface ui in userInterface.EntityAdapter.EntityAdapterUserInterfaces)
                            {
                                if (ui != userInterface && ui.IsDefault)
                                    ui.IsDefault = false;
                            }
                        }

                        userInterface.EntityAdapter.CheckDefaultUserInterface();
                    }
                }

                //Change File Name
                if (e.DomainProperty.Name == "Name")
                {
                    userInterface.RenameSpaSourceFiles(e.OldValue as string);
                    //userInterface.RenameMobileSourceFiles(e.OldValue as string);
                }

                //Change File Name
                if (e.DomainProperty.Name == "VisualType")
                {
                    switch (userInterface.VisualType)
                    {
                        case InterfaceType.Web:
                            //userInterface.DeleteMobileSourceFiles();                            
                            break;
                        case InterfaceType.Mobile:
                            userInterface.DeleteSpaSourceFiles();
                            break;
                        default:
                            break;
                    }
                }
            }

            base.ElementPropertyChanged(e);
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
                if (e.DomainProperty.Name == "Name")
                {
                    webApi.RenameSourceFiles(e.OldValue as string);
                }
            }

            base.ElementPropertyChanged(e);
        }
    }


    [RuleOn(typeof(RepositoryImplementation), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class RepositoryImplementationChangeConfiguration : ChangeRule
    {
        public override void ElementPropertyChanged(ElementPropertyChangedEventArgs e)
        {
            if (e.DomainProperty.Name.InList("IsDefault"))
            {
                RepositoryImplementation currentImpl = e.ModelElement as RepositoryImplementation;
                if (currentImpl != null && currentImpl.RepositoryInterface != null)
                {
                    if (e.OldValue != null && !e.OldValue.Equals(e.NewValue) && ((bool)e.NewValue) && currentImpl.RepositoryInterface.RepositoryImplementations.Count > 1)
                    {
                        foreach (RepositoryImplementation impl in currentImpl.RepositoryInterface.RepositoryImplementations)
                        {
                            if (impl != currentImpl && impl.IsDefault)
                                impl.IsDefault = false;
                        }
                    }

                    currentImpl.RepositoryInterface.CheckDefaultImplementation();
                }
            }

            base.ElementPropertyChanged(e);
        }
    }


    [RuleOn(typeof(Workflow), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class WorkflowChangeConfiguration : ChangeRule
    {
        public override void ElementPropertyChanged(ElementPropertyChangedEventArgs e)
        {
            if (e.DomainProperty.Name == "Name" && !e.NewValue.IsNullOrEmpty())
            {
                Workflow wf = e.ModelElement as Workflow;
                if (wf != null)
                {
                    string oldName = e.OldValue as string, newName = e.NewValue as string;
                    if (wf.Display == oldName || wf.Display.IsNullOrEmpty())
                    {
                        wf.Display = newName;
                    }
                }
            }
            base.ElementPropertyChanged(e);
        }
    }

    [RuleOn(typeof(RepositoryInterface), FireTime = TimeToFire.TopLevelCommit, InitiallyDisabled = false)]
    internal sealed class RepositoryInterfaceChangeConfiguration : ChangeRule
    {
        public override void ElementPropertyChanged(ElementPropertyChangedEventArgs e)
        {
            if (e.DomainProperty.Name.InList("IsExtension"))
            {
                RepositoryInterface intrf = e.ModelElement as RepositoryInterface;
                if (intrf != null)
                {
                    if (e.OldValue != null && !e.OldValue.Equals(e.NewValue) && ((bool)e.NewValue) && intrf.EntityAdapterDesignerRoot.RepositoryInterfaces.Any(i => i != intrf && i.IsExtension))
                    {
                        throw new Exception("Already exists an extension interface on this surface!!!");
                    }
                }
            }
            base.ElementPropertyChanged(e);
        }
    }


    /// <summary>  
    /// /// Domain model class allows extra reflective elements such as rules to be added  
    /// /// </summary>  
    public partial class EntityAdapterDesignerDomainModel
    {
        protected override Type[] GetCustomDomainModelTypes()
        {
            return new System.Type[] { typeof(ModelElementStartConfiguration), 
                typeof(EdmStartConfiguration), 
                typeof(EntityLinkStartConfiguration), 
                typeof(RepresentationLinkStartConfiguration), 
                typeof(EdmLinkStartConfiguration), 
                typeof(EntityAdapterRepresentationLinkStartConfiguration), 
                typeof(DomainServiceExtensionDeleteConfiguration), 
                typeof(GenericOperationStartConfiguration), 
                typeof(GenericOperationChangeConfiguration), 
                typeof(EntityAdapterReferencesEntityAdapterUserInterfacesStartConfiguration), 
                typeof(EntityAdapterUserInterfaceStartConfiguration), 
                typeof(EntityAdapterUserInterfaceDeletingConfiguration), 
                typeof(SubscriptionStartConfiguration), 
                typeof(EntityAdapterRepresentationStartConfiguration),
                typeof(EntityAdapterUserInterfaceChangeConfiguration), 
                typeof(EntityAdapterPropertyAddConfiguration), 
                typeof(EntityAdapterPropertyChangeConfiguration),
                typeof(LookUpPropertyPropertyAddConfiguration), 
                typeof(WorkflowChangeConfiguration), 
                typeof(EntityAdapterAttributeChangeConfiguration), 
                typeof(EntityAdapterAttributeDeleteConfiguration), 
                typeof(EntityLinkBaseStartConfiguration), 
                typeof(EntityLinkBaseDeleteConfiguration), 
                typeof(LookUpLinkBaseStartConfiguration), 
                typeof(LookUpLinkBaseDeleteConfiguration), 
                typeof(EntityAdapterChangeConfiguration), 
                typeof(EntityLinkLocalStartConfiguration), 
                typeof(EntityLinkLocalDeleteConfiguration), 
                typeof(UserInterfaceLinkBaseDeleteConfiguration), 
                typeof(UserInterfaceLinkBaseStartConfiguration), 
                typeof(EntityAdapterRepresentationReferencesTargetEntityAdapterRepresentationStartConfiguration), 
                typeof(RepositoryImplementationChangeConfiguration), 
                typeof(RepositoryImplementationReferencesRepositoryInterfaceStartConfiguration), 
                typeof(EntityInstanceReferencesEntityOwnersStartConfiguration), 
                typeof(EntityCollectionReferencesEntityOwnersStartConfiguration), 
                typeof(EntityAdapterReferencesSubscriptionStartConfiguration), 
                typeof(LookUpAdapterAddConfiguration), 
                typeof(EntityAdapterAddConfiguration), 
                typeof(OlapCatalogStartConfiguration), 
                typeof(RepositoryInterfaceChangeConfiguration), 
                typeof(WebApiControllerChangeConfiguration), 
                typeof(WebApiControllerDeletingConfiguration) };
        }
    }
}
