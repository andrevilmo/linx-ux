using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Linx.Tools;
using Microsoft.VisualStudio.Modeling;

namespace Linx.EntityAdapterDesigner.CustomizedCode
{
    public partial class FormEntityJoinRelation : Form
    {
        EntityAdapterRepresentation _sourceEntity;
        EntityAdapterRepresentation _targetEntity;
        EntityAdapterRepresentationReferencesTargetEntityAdapterRepresentation _relation;
        List<EntityJoinRelation> _relationPropertyList = new List<EntityJoinRelation>();


        public static bool IsValid(EntityAdapterRepresentationReferencesTargetEntityAdapterRepresentation relation)
        {
            var _sourceEntity = relation.SourceEntityAdapterRepresentation;
            var _targetEntity = relation.TargetEntityAdapterRepresentation;

            return !(_sourceEntity == null || _targetEntity == null || _sourceEntity.BusinessObject.IsNullOrEmpty() || _targetEntity.BusinessObject.IsNullOrEmpty());
        }

        public FormEntityJoinRelation()
        {
            InitializeComponent();
        }

        public FormEntityJoinRelation(EntityAdapterRepresentationReferencesTargetEntityAdapterRepresentation relation)
            : this()
        {
            this._sourceEntity = relation.SourceEntityAdapterRepresentation;
            this._targetEntity = relation.TargetEntityAdapterRepresentation;
            this._relation = relation;
            this.LodData();
        }

        PublicationEntity pubSourceEntity, pubTargetEntity;
        private void LodData()
        {
            if (_targetEntity != null && _sourceEntity != null)
            {
                //Prepare combos with properties
                pubSourceEntity = _sourceEntity.EntityAdapterDesignerRoot.GetPublishedEntityByRef(_sourceEntity.BusinessObject, _sourceEntity.TargetNameSpace, _sourceEntity.TargetEntityAdapterName);
                pubTargetEntity = _targetEntity.EntityAdapterDesignerRoot.GetPublishedEntityByRef(_targetEntity.BusinessObject, _targetEntity.TargetNameSpace, _targetEntity.TargetEntityAdapterName);
                
                this.PrepareDataSource();
                                
                if (pubSourceEntity != null)
                {
                    this.dataGridViewComboSourceProperty.HeaderText = pubSourceEntity.Name;
                    foreach (var property in pubSourceEntity.Properties.OrderBy(e => e.Name))
                    {
                        this.dataGridViewComboSourceProperty.Items.Add(property.Name);
                    }
                }

                if (pubTargetEntity != null)
                {
                    this.dataGridViewComboTargetProperty.HeaderText = pubTargetEntity.Name;
                    foreach (var property in pubTargetEntity.Properties.OrderBy(e => e.Name))
                    {
                        this.dataGridViewComboTargetProperty.Items.Add(property.Name);
                    }
                }

            }
        }


        private void PrepareDataSource()
        {
            if (this._relation != null)
            {
                _relationPropertyList.Clear();

                //Suggest relation
                string sourceProperties = this._relation.SourceProperties, targetProperties = this._relation.TargetProperties;
                if (sourceProperties.IsNullOrEmpty() || targetProperties.IsNullOrEmpty())
                {
                    sourceProperties = String.Empty;
                    targetProperties = String.Empty;
                    foreach (var sourceProp in pubSourceEntity.Properties)
                    {
                        var targetProp = pubTargetEntity.Properties.Where(e => e.Name == sourceProp.Name).FirstOrDefault();
                        if (targetProp != null)
                        {
                            sourceProperties += (sourceProperties.IsNullOrEmpty() ? "" : ",") + sourceProp.Name;
                            targetProperties += (targetProperties.IsNullOrEmpty() ? "" : ",") + targetProp.Name;
                        }
                    }
                }

                if (!sourceProperties.IsNullOrEmpty() && !targetProperties.IsNullOrEmpty())
                {
                    string[] sources = sourceProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    string[] targets = targetProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    if (sources.Length > 0 && sources.Length == targets.Length)
                    {
                        for (int idx = 0; idx < sources.Length; idx++)
                        {
                            if (pubSourceEntity.Properties.Where(e => e.Name == sources[idx].Trim()).Count() > 0 && pubTargetEntity.Properties.Where(e => e.Name == targets[idx].Trim()).Count() > 0)
                                _relationPropertyList.Add(new EntityJoinRelation() { SourceProperty = sources[idx].Trim(), TargetProperty = targets[idx].Trim() });
                        }
                    }
                }
                entityJoinRelationBindingSource.DataSource = _relationPropertyList;
            }
        }

        private void btnApplyOrder_Click(object sender, EventArgs e)
        {
            this.ApplyChanges();
        }

        private void ApplyChanges()
        {            
            if (_relation != null)
            {
                if (_relationPropertyList.Where(e => e.TargetProperty.IsNullOrEmpty() || e.SourceProperty.IsNullOrEmpty()).Count() > 0)
                {
                    MessageBox.Show(null, "All relations must have the two values (Source and Target)!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string sourceProperties = String.Empty, targetProperties = String.Empty;                
                foreach (var elementRelation in _relationPropertyList)
                {
                    sourceProperties += (sourceProperties.IsNullOrEmpty() ? String.Empty : ",") + elementRelation.SourceProperty;
                    targetProperties += (targetProperties.IsNullOrEmpty() ? String.Empty : ",") + elementRelation.TargetProperty;
                }

                if (_relation.SourceProperties != sourceProperties || _relation.TargetProperties != targetProperties)
                {
                    using (Transaction transaction = _relation.Store.TransactionManager.BeginTransaction("UpdatePropertyRelation"))
                    {
                        _relation.SourceProperties = sourceProperties;
                        _relation.TargetProperties = targetProperties;
                        transaction.Commit();
                    }
                }
            }
            this.Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listEntities_DoubleClick(object sender, EventArgs e)
        {
            this.ApplyChanges();
        }

    }

    public class EntityJoinRelation
    {
        public string SourceProperty { get; set; }
        public string TargetProperty { get; set; }
    }

}
