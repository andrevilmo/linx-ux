using System;
using Microsoft.VisualStudio.Modeling;
using Microsoft.VisualStudio.Modeling.Integration;
using Microsoft.VisualStudio.Modeling.Integration.Picker;
using Microsoft.VisualStudio.Modeling.Validation;
using System.Linq;
using Linx.Tools;
using Microsoft.VisualStudio.Modeling.Immutability;


namespace Linx.BusinessDataModelDesigner
{
    partial class ReferenceModelClass
    {
        private ModelBusReference _modelClassReference;
        public ModelBusReference ModelClassReference
        {
            get
            {
                if (_modelClassReference == null && !this.ExternalReference.IsNullOrEmpty() && System.IO.File.Exists(this.ExternalReference))
                {
                    //Model Bus
                    var modelBus = this.BusinessDataModelDesignerRoot.GetModelBus();
                    // Get an adapterManager for the target DSL:
                    ModelBusAdapterManager manager = BusinessDataModelDesignerRoot.GetModelBusManager<ModelBusAdapterManager>(modelBus);
                    var modelReference = manager.CreateReference(this.ExternalReference);
                    using (ModelBusAdapter modelAdapter = manager.CreateAdapter(modelReference))
                    {
                        var modelRoot = modelAdapter.GetModelRoot<BusinessDataModelDesignerRoot>();
                        var element = modelRoot.Types.Where(e => e is ModelClass && e.Name == this.Name).FirstOrDefault() as ModelClass;
                        if (element != null)
                        {
                            // Create a reference to the target model:
                            _modelClassReference = modelAdapter.GetElementReference(element);
                        }
                    }
                }

                return _modelClassReference;
            }
            set
            {
                if (_modelClassReference != value)
                {
                    _modelClassReference = value;
                    if (_modelClassReference != null)
                    {
                        this.ExternalReference = this.ModelClassReference.GetReferenceFile();
                    }
                    else
                        this.ExternalReference = "";
                }
            }
        }

        public string GetReferenceProjectInfoValue()
        {
            if (this.ModelClassReference != null && this.ModelClassReference.AdapterReference != null)
            {
                string path = this.ModelClassReference.GetReferenceFile();
                return (path.IsNullOrEmpty() ? String.Empty : "<<" + ("\\" + System.IO.Path.GetDirectoryName(path)).Right("\\") + ">>");
            }
            else
                return String.Empty;
        }

        public string GetReferenceInfoValue()
        {
            return (this.ModelClassReference == null ? String.Empty : "<<" + this.ModelClassReference.ModelDisplayName + ".lxdm" + ">>");
        }

        public void AlertError()
        {
            this.HasReferenceError = true;
        }

        public void ReleaseError()
        {
            this.HasReferenceError = false;
        }

        private void SetColor(System.Drawing.Color color)
        {
            var shape = this.GetPresentation<ReferenceModelClassShape>();
            if (shape != null)
                shape.OutlineColor = color;
        }

        #region Update Link References

        public void ConvertToModelCLass()
        {
            this.BusinessDataModelDesignerRoot.ConvertToModelCLass(this);
        }

        public bool UpdateLinksReference(ModelClass modelClassInstance)
        {
            if (this.BusinessDataModelDesignerRoot == null || this.BusinessDataModelDesignerRoot.IsLocked)
                return false;

            bool hasChanges = false;
            if (modelClassInstance != null)
            {
                if (UpdateLinkToSuperclass(modelClassInstance))
                    hasChanges = true;
                if (UpdateLinkToSuperclassSh(modelClassInstance))
                    hasChanges = true;
                if (UpdatLinksToTargetModelClasses(modelClassInstance))
                    hasChanges = true;
                if (UpdatLinkToMultipleAssociation(modelClassInstance))
                    hasChanges = true;
                if (UpdatLinksToMultipleAssociations(modelClassInstance))
                    hasChanges = true;
            }

            return hasChanges;
        }

        private bool UpdateLinkToSuperclass(ModelClass modelClassInstance)
        {
            bool hasChanges = false;
            var link = modelClassInstance.GetLinkToSuperclass();
            var thisLink = this.GetLinkToSuperclass();

            //Check for deleting 
            if ((link == null && thisLink != null) || (link != null && thisLink != null && link.Superclass.Name != thisLink.Superclass.Name))
            {
                thisLink.Delete();
                hasChanges = true;
            }

            //Update super class
            if (link != null)
            {
                var superClass = this.BusinessDataModelDesignerRoot.Types.Where(e => e is ReferenceModelClass && e.Name == link.Superclass.Name).FirstOrDefault() as ReferenceModelClass;
                if (superClass != null)
                {
                    if (!superClass.Subclasses.Contains(this))
                    {
                        superClass.Subclasses.Add(this);
                        hasChanges = true;
                    }
                    thisLink = this.GetLinkToSuperclass();
                    if (thisLink != null && !thisLink.EqualsInstanceFrom(link))
                    {
                        thisLink.CopyInstanceFrom(link);
                        hasChanges = true;
                    }
                }
            }

            return hasChanges;
        }

        private bool UpdateLinkToSuperclassSh(ModelClass modelClassInstance)
        {
            bool hasChanges = false;
            var link = modelClassInstance.GetLinkToSuperclassSh();
            var thisLink = this.GetLinkToSuperclassSh();

            //Check for deleting 
            if ((link == null && thisLink != null) || (link != null && thisLink != null && link.SuperclassSh.Name != thisLink.SuperclassSh.Name))
            {
                thisLink.Delete();
                hasChanges = true;
            }

            //Update super class
            if (link != null)
            {
                var superClass = this.BusinessDataModelDesignerRoot.Types.Where(e => e is ReferenceModelClass && e.Name == link.SuperclassSh.Name).FirstOrDefault() as ReferenceModelClass;
                if (superClass != null)
                {
                    if (!superClass.SubclassesSh.Contains(this))
                    {
                        superClass.SubclassesSh.Add(this);
                        hasChanges = true;
                    }
                    thisLink = this.GetLinkToSuperclassSh();
                    if (thisLink != null && !thisLink.EqualsInstanceFrom(link))
                    {
                        hasChanges = true;
                        thisLink.CopyInstanceFrom(link);
                    }
                }
            }
            return hasChanges;
        }

        private bool UpdatLinksToTargetModelClasses(ModelClass modelClassInstance)
        {
            bool hasChanges = false;
            var links = modelClassInstance.GetLinksToTargetModelClasses();

            foreach (var link in links)
            {
                //Update multiple association targets
                var targetClass = this.BusinessDataModelDesignerRoot.Types.Where(e => e is ReferenceModelClass && e.Name == link.TargetModelClass.Name).FirstOrDefault() as ReferenceModelClass;
                if (targetClass != null)
                {
                    var refLinks = Association.GetLinks(this, targetClass);
                    var thisLink = refLinks.Where(e => e.IdReference == link.Id || (!e.ForeignKeyConstraintName.IsNullOrEmpty() && e.ForeignKeyConstraintName == link.ForeignKeyConstraintName)).FirstOrDefault();
                    if (thisLink == null)
                        thisLink = refLinks.Where(e => e.GetTargetProperties() == link.GetTargetProperties()).FirstOrDefault();

                    if (thisLink == null)
                    {
                        thisLink = new Association(this, targetClass);
                        hasChanges = true;
                    }

                    if (thisLink.IdReference.IsNullOrEmpty() || (thisLink.IdReference != link.Id && thisLink.IdReference != link.IdReference) || !thisLink.EqualsInstanceFrom(link, "IdReference"))
                    {
                        hasChanges = true;
                        thisLink.CopyInstanceFrom(link);
                        if (link.IdReference.IsNullOrEmpty())
                            thisLink.IdReference = link.Id;
                        else
                            thisLink.IdReference = link.IdReference;
                    }
                }
            }
            return hasChanges;
        }

        private bool UpdatLinkToMultipleAssociation(ModelClass modelClassInstance)
        {
            bool hasChanges = false;
            var link = modelClassInstance.GetLinkToMultipleAssociation();
            var thisLink = this.GetLinkToMultipleAssociation();

            //Check for deleting 
            if ((link == null && thisLink != null) || (link != null && thisLink != null && link.MultipleAssociation.Id != thisLink.MultipleAssociation.IdReference))
            {
                thisLink.Delete();
                hasChanges = true;
            }

            //Update multiple association target
            if (link != null)
            {
                var multAssociation = this.BusinessDataModelDesignerRoot.Types.Where(e => e is MultipleAssociation && ((MultipleAssociation)e).IdReference == link.MultipleAssociation.Id).FirstOrDefault() as MultipleAssociation;
                if (multAssociation == null)
                {
                    hasChanges = true;
                    multAssociation = new MultipleAssociation(this.Partition);
                    this.BusinessDataModelDesignerRoot.Types.Add(multAssociation);
                    multAssociation.IdReference = link.MultipleAssociation.Id;
                    multAssociation.TargetType = this;
                }
                else if (multAssociation.TargetType != this)
                {
                    multAssociation.TargetType = this;
                    hasChanges = true;
                }
            }
            return hasChanges;
        }

        private bool UpdatLinksToMultipleAssociations(ModelClass modelClassInstance)
        {
            bool hasChanges = false;
            var links = modelClassInstance.GetLinksToMultipleAssociations();

            //Check for removing inconsistences
            var thisLinks = this.GetLinksToMultipleAssociations().Where(e => !e.MultipleAssociation.IdReference.IsNullOrEmpty()).ToList();
            foreach (var thisLink in thisLinks.Where(e => !links.Any(p => p.MultipleAssociation.Id == e.MultipleAssociation.IdReference)).ToArray())
            {
                thisLink.Delete();
                hasChanges = true;
            }

            foreach (var link in links)
            {
                //Update multiple association origins
                var multAssociation = this.BusinessDataModelDesignerRoot.Types.Where(e => e is MultipleAssociation && ((MultipleAssociation)e).IdReference == link.MultipleAssociation.Id).FirstOrDefault() as MultipleAssociation;
                if (multAssociation == null)
                {
                    hasChanges = true;
                    multAssociation = new MultipleAssociation(this.Partition);
                    this.BusinessDataModelDesignerRoot.Types.Add(multAssociation);
                    multAssociation.IdReference = link.MultipleAssociation.Id;
                    multAssociation.OriginTypes.Add(this);
                }
                else if (!multAssociation.OriginTypes.Contains(this))
                {
                    multAssociation.OriginTypes.Add(this);
                    hasChanges = true;
                }
            }
            return hasChanges;
        }

        #endregion

        public bool HasChanges(ModelClass modelClassInstance)
        {
            if (modelClassInstance != null)
            {
                if (!this.EqualsInstanceFrom(modelClassInstance))
                    return true;

                if (this.Attributes.Count != modelClassInstance.Attributes.Count || this.Operations.Count != modelClassInstance.Operations.Count || this.ModelIndexes.Count != modelClassInstance.ModelIndexes.Count)
                    return true;

                for (int idx = 0; idx < this.Attributes.Count; idx++)
                {
                    if (!this.Attributes[idx].EqualsInstanceFrom(modelClassInstance.Attributes[idx]))
                        return true;
                }

                for (int idx = 0; idx < this.Operations.Count; idx++)
                {
                    if (!this.Operations[idx].EqualsInstanceFrom(modelClassInstance.Operations[idx]))
                        return true;
                }

                for (int idx = 0; idx < this.ModelIndexes.Count; idx++)
                {
                    if (!this.ModelIndexes[idx].EqualsInstanceFrom(modelClassInstance.ModelIndexes[idx]))
                        return true;
                }
            }
            return false;
        }

        public void SetStructuralLocks(Locks lockValue)
        {
            //CLass
            foreach (var attr in this.Attributes)
            {
                attr.SetLocks(lockValue);
            }
            foreach (var op in this.Operations)
            {
                op.SetLocks(lockValue);
            }
            foreach (var idx in this.ModelIndexes)
            {
                idx.SetLocks(lockValue);
            }
            this.SetLocks(lockValue);

            //Links
            foreach (var link in Association.GetLinksToSourceModelClasses(this).Where(e => e.SourceModelClass is ReferenceModelClass))
            {
                link.SetLocks(lockValue);
            }
            foreach (var link in Association.GetLinksToTargetModelClasses(this).Where(e => e.TargetModelClass is ReferenceModelClass))
            {
                link.SetLocks(lockValue);
            }
            foreach (var link in MultipleAssociationOrigin.GetLinksToMultipleAssociations(this))
            {
                link.SetLocks(lockValue);
            }
            var linkMa = MultipleAssociationTarget.GetLinkToMultipleAssociation(this);
            if (linkMa != null)
            {
                linkMa.SetLocks(lockValue);
            }
            var linkSup = Generalization.GetLinkToSuperclass(this);
            if (linkSup != null && linkSup.Superclass is ReferenceModelClass)
            {
                linkSup.SetLocks(lockValue);
            }
            var linkSupH = GeneralizationSh.GetLinkToSuperclassSh(this);
            if (linkSupH != null && linkSupH.SuperclassSh is ReferenceModelClass)
            {
                linkSupH.SetLocks(lockValue);
            }
            foreach (var link in Generalization.GetLinksToSubclasses(this).Where(e => e.Subclass is ReferenceModelClass))
            {
                link.SetLocks(lockValue);
            }
            foreach (var link in GeneralizationSh.GetLinksToSubclassesSh(this).Where(e => e.SubclassSh is ReferenceModelClass))
            {
                link.SetLocks(lockValue);
            }
        }


        public void UpdateFromExternalReference()
        {
            if (this.ModelClassReference == null)
                return;

            IModelBus modelBus = this.GetModelBus();

            if (modelBus != null)
            {
                ModelBusAdapterManager manager = BusinessDataModelDesignerRoot.GetModelBusManager<ModelBusAdapterManager>(modelBus);
                //Update All Links
                using (Transaction transaction =
                                               this.Store.TransactionManager.BeginTransaction("Update External References."))
                {
                    bool hasChanges = false;
                    ModelBusReference modelReference = this.ModelClassReference;

                    if (modelReference == null)
                        return;

                    using (ModelBusAdapter modelAdapter = manager.CreateAdapter(modelReference))
                    {
                        if (modelAdapter != null)
                        {
                            BusinessDataModelDesignerRoot modelRoot = modelAdapter.GetPropertyValue("ModelRoot") as BusinessDataModelDesignerRoot;
                            if (modelRoot != null)
                            {
                                //Clear locks
                                this.SetStructuralLocks(Microsoft.VisualStudio.Modeling.Immutability.Locks.None);

                                if (this.UpdateClassReference(modelRoot.Types.FirstOrDefault(e => e is ModelClass && e.Name == this.Name) as ModelClass))
                                    hasChanges = true;

                                if (this.UpdateLinksReference(modelRoot.Types.FirstOrDefault(e => e is ModelClass && e.Name == this.Name) as ModelClass))
                                    hasChanges = true;

                                //Add locks
                                this.SetStructuralLocks(Microsoft.VisualStudio.Modeling.Immutability.Locks.Properties);
                            }
                        }
                    }


                    if (hasChanges)
                        transaction.Commit();
                    else
                        transaction.Rollback();
                }
            }
        }

        public bool UpdateClassReference(ModelClass modelClassInstance)
        {
            if (this.BusinessDataModelDesignerRoot == null || this.BusinessDataModelDesignerRoot.IsLocked || modelClassInstance == null)
                return false;

            if (!HasChanges(modelClassInstance))
                return false;

            //Copy class properties
            this.CopyInstanceFrom(modelClassInstance);
            //this.Kind = modelClassInstance.Kind;
            //this.Modifier = modelClassInstance.Modifier;
            //Copy attributes

            var pks = this.Attributes.Where(p => p.IsPrimaryKey).ToArray();
            ModelAttribute oldPrimaryKey = (pks.Length == 1 ? pks[0] : null);
            foreach (var element in this.Attributes.Where(e => !modelClassInstance.Attributes.Any(p => p.Name == e.Name)).ToList())
            {
                element.Delete();
            }
            for (int idx = 0; idx < modelClassInstance.Attributes.Count; idx++)
            {
                ModelAttribute attr = modelClassInstance.Attributes[idx];
                ModelAttribute element = this.Attributes.Where(e => e.Name == attr.Name).FirstOrDefault();
                if (element == null)
                    element = (ModelAttribute)this.Attributes.AddNew();
                element.CopyInstanceFrom(attr);
                if (element.IsPrimaryKey && oldPrimaryKey != null)
                {
                    element.UpdateRelations(oldPrimaryKey.Name, true);
                }
                //element.DataType = attr.DataType;
                this.Attributes.Move(element, idx);
            }

            ////Delete inconsistent operations
            //foreach (var element in this.Operations.Where(e => !modelClassInstance.Operations.Any(p => p.Name == e.Name)).ToList())
            //{
            //    element.Delete();
            //}
            ////Copy operations
            //for (int idx = 0; idx < modelClassInstance.Operations.Count; idx++)
            //{
            //    ClassOperation op = modelClassInstance.Operations[idx];
            //    ClassOperation element = this.Operations.Where(e => e.Name == op.Name).FirstOrDefault();
            //    if (element == null)
            //        element = (ClassOperation)this.Operations.AddNew();
            //    element.CopyInstanceFrom(op);
            //    //element.Concurrency = op.Concurrency;
            //    //element.Access = op.Access;
            //    this.Operations.Move(element, idx);
            //}

            //Delete inconsistent indexes
            foreach (var element in this.ModelIndexes.Where(e => !modelClassInstance.ModelIndexes.Any(p => p.Name == e.Name)).ToList())
            {
                element.Delete();
            }

            //Copy indexes
            for (int idx = 0; idx < modelClassInstance.ModelIndexes.Count; idx++)
            {
                ModelIndex index = modelClassInstance.ModelIndexes[idx];
                ModelIndex element = this.ModelIndexes.Where(e => e.Name == index.Name).FirstOrDefault();
                if (element == null)
                    element = (ModelIndex)this.ModelIndexes.AddNew();
                element.CopyInstanceFrom(index);
                this.ModelIndexes.Move(element, idx);
            }

            return true;
        }
    }
}
