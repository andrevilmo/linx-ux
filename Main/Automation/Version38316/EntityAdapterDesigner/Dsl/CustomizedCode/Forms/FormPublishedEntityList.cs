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
    public partial class FormPublishedEntityList : Form
    {

        public FormPublishedEntityList()
        {
            InitializeComponent();
        }

        EntityAdapterRepresentation _entity;
        public EntityAdapterRepresentation Entity
        {
            get { return _entity; }
            set
            {
                _entity = value;
                if (_entity != null)
                {
                    this.PopulateList(_entity.EntityAdapterDesignerRoot.GetPublishedEntities().OrderBy(e => e));                    
                }
            }
        }

        EntityAdapterUserInterface _userInterface;
        public EntityAdapterUserInterface UserInterface
        {
            get { return _userInterface; }
            set
            {
                _userInterface = value;
                if (_userInterface != null)
                {
                    this.PopulateList(_userInterface.EntityAdapterDesignerRoot.GetPublishedEntities(true, _userInterface.Subscription).OrderBy(e => e));   
                }
            }
        }


        private void PopulateList(IEnumerable<string> entities)
        {
            ListViewGroup group = null;
            string[] parts;
            string currentAssembly = String.Empty;
            foreach (string entityRef in entities)
            {
                parts = entityRef.Split(new char[] { '#' });

                if (parts.Length != 7)
                    continue;

                string assembly = parts[0], nameSpace = parts[1], entityName = parts[2], edmName = parts[3], edmEntityName = parts[4], isIQueryable = parts[5], isUpdatable = parts[6];
                if (assembly != currentAssembly)
                {
                    currentAssembly = assembly;
                    group = new ListViewGroup(System.IO.Path.GetFileNameWithoutExtension(assembly)) { Tag = assembly };
                    this.listEntities.Groups.Add(group);
                }
                var item = this.listEntities.Items.Add(nameSpace.Right(".") + "." + entityName);
                item.Group = group;
                item.Tag = nameSpace + "#" + edmName + "#" + edmEntityName + "#" + isIQueryable + "#" + isUpdatable;
            }
            this.listEntities.Sort();
        }

        private void btnApplyOrder_Click(object sender, EventArgs e)
        {
            this.ApplyChanges();
        }

        private void ApplyChanges()
        {
            this.Close();
            if (_entity != null || _userInterface != null)
            {
                if (this.listEntities.SelectedItems.Count > 0)
                {
                    string entityName = this.listEntities.SelectedItems[0].Text.Right("."), targetNameSpace = String.Empty, targetEdmName = String.Empty, targetEdmEntityName = String.Empty;
                    bool isIQueryable = true, isUpdatable = false;
                    //Get other elements from selected item
                    if (!this.listEntities.SelectedItems[0].Tag.IsNullOrEmpty())
                    {
                        string[] values = (this.listEntities.SelectedItems[0].Tag as string).Split(new char[] { '#' });
                        if (values.Length == 5)
                        {
                            targetNameSpace = values[0];
                            targetEdmName = values[1];
                            targetEdmEntityName = values[2];
                            isIQueryable = (values[3] == "true");
                            isUpdatable = (values[4] == "true");
                        }
                    }

                    if (_entity != null && (_entity.TargetEntityAdapterName != entityName || _entity.TargetNameSpace != targetNameSpace || _entity.TargetEdmName != targetEdmName || _entity.TargetEdmEntityName != targetEdmEntityName || _entity.IsIQueryable != isIQueryable || _entity.IsPublisherUpdatable != isUpdatable))
                    {
                        using (Transaction transaction = _entity.Store.TransactionManager.BeginTransaction("UpdateTargetEntityAdapterName"))
                        {
                            _entity.BusinessObject = this.listEntities.SelectedItems[0].Group.Tag as string;
                            if (_entity.TargetEntityAdapterName != entityName)
                            {
                                string maxValue = _entity.EntityAdapterDesignerRoot.EntityAdapterRepresentations.Where(r => r != _entity && r.Name.StartsWith(entityName + "_Rep")).Select(r => r.Name.Right(entityName + "_Rep")).Max();
                                int repCnt = maxValue.IsNumeric() ? int.Parse(maxValue) : 0;
                                _entity.Name = entityName + "_Rep" + (repCnt + 1).ToString();
                            }
                            _entity.TargetEntityAdapterName = entityName;
                            _entity.TargetNameSpace = targetNameSpace;
                            _entity.TargetEdmName = targetEdmName;
                            _entity.TargetEdmEntityName = targetEdmEntityName;
                            _entity.IsIQueryable = isIQueryable;
                            _entity.IsPublisherUpdatable = isUpdatable;
                            transaction.Commit();
                        }
                    }

                    if (_userInterface != null && (_userInterface.SubscriptionEntityAdapterName != entityName || _userInterface.SubscriptionNameSpace != targetNameSpace))
                    {
                        using (Transaction transaction = _userInterface.Store.TransactionManager.BeginTransaction("UpdateTargetEntityAdapterName"))
                        {
                            _userInterface.SubscriptionEntityAdapterName = entityName;
                            _userInterface.SubscriptionNameSpace = targetNameSpace;                            
                            transaction.Commit();
                        }
                    }

                }
            }
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
}
