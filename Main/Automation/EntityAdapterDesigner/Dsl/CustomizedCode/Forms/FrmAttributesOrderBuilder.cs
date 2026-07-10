using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Linx.Tools;
using Linx.Builder.Resources;  

namespace Linx.EntityAdapterDesigner.CustomCode
{

    public partial class FrmAttributesOrderBuilder : Form
    {
        private EntityAdapter entity;
        public EntityAdapter Entity
        {
            get { return entity; }
            set
            {
                if (value != entity)
                {
                    entity = value;
                    this.LoadAttributes();
                }
            }
        }

        private void LoadAttributes()
        {
            ListViewGroup objectGroup;
            string groupCode, groupHeader;
            List<EntityAttribute> attributesList = this.Entity.EntityAdapterProperties.OrderBy(e => e.DisplayOrder).Select(e => new EntityAttribute { Name = e.Name, Key = e.EdmKey, Order = e.DisplayOrder, IsFormula = false, Group = e.GroupName }).ToList();
            attributesList.AddRange(this.Entity.EntityAdapterFormulas.OrderBy(e => e.DisplayOrder).Select(e => new EntityAttribute { Name = e.Name, Key = e.Name, Order = e.DisplayOrder, IsFormula = true, Group = e.GroupName }).ToList());
            attributesList = attributesList.OrderBy(e => e.Order).ToList();

            var groupsList = attributesList.Select(e => e.Group).Distinct().OrderBy(e => e);

            foreach (string group in groupsList)
            {
                if (group.IsNullOrEmpty())
                {
                    groupCode = "0001";
                    groupHeader = "";
                }
                else
                {
                    groupCode = group.Left("::").Trim();
                    groupHeader = (group.Right("::") + "||").Left("||").Trim();
                    if (groupCode.IsNullOrEmpty())
                    {
                        groupCode = "0001";
                        groupHeader = "";
                    }
                }
                objectGroup = this.lstOrder.Groups[groupCode];
                if (objectGroup == null)
                {
                    objectGroup = this.lstOrder.Groups.Add(groupCode, (groupHeader + "||").Left("||"));
                    objectGroup.Tag = groupCode + "::" + groupHeader;
                }
                else if (objectGroup.Header.IsNullOrEmpty() && !groupHeader.IsNullOrEmpty())
                {
					objectGroup.Header = (groupHeader + "||").Left("||");
                    objectGroup.Tag = groupCode + "::" + groupHeader;
                }


            }

            foreach (EntityAttribute attribute in attributesList)
            {
                var item = this.lstOrder.Items.Add(attribute.Name, (attribute.IsFormula ? 1 : 0));

                //Assign Group
                groupCode = (attribute.Group.IsNullOrEmpty() ? "" : attribute.Group.Left("::").Trim());
                if (groupCode.IsNullOrEmpty())
                    groupCode = "0001";
                item.Group = this.lstOrder.Groups[groupCode];
            }
        }

        public FrmAttributesOrderBuilder()
        {
            InitializeComponent();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btApply_Click(object sender, EventArgs e)
        {
            this.ApplyOrder();
            this.Close();
        }

        private void ApplyOrder()
        {
            EntityAdapterProperty property;
            EntityAdapterFormula formula;
			int index = 0;
			
            //Update new order	
			foreach (ListViewGroup group in this.lstOrder.Groups)
			{
				foreach (ListViewItem item in this.lstOrder.Items)
				{
					if (group.Name == item.Group.Name)
					{
						property = this.Entity.EntityAdapterProperties.Where(e => e.Name == item.Text).FirstOrDefault();
						if (property != null)
						{
							property.DisplayOrder = index;
							property.GroupName = item.Group.Tag.ToString();
							index++;
						}
						else
						{
							formula = this.Entity.EntityAdapterFormulas.Where(e => e.Name == item.Text).FirstOrDefault();
							if (formula != null)
							{
								formula.DisplayOrder = index;
								formula.GroupName = item.Group.Tag.ToString();
								index++;
							}
						}
					}
				}
			}

            //Adjust Order by UIDisplayOrder
            var propertiesList = this.Entity.EntityAdapterProperties.OrderBy(e => e.DisplayOrder).ToList();
            for (int propIndex = 0; propIndex < propertiesList.Count; propIndex++)
            {
                this.Entity.EntityAdapterProperties.Move(propertiesList[propIndex], propIndex);
            }

            var formulasList = this.Entity.EntityAdapterFormulas.OrderBy(e => e.DisplayOrder).ToList();
            for (int fIndex = 0; fIndex < formulasList.Count; fIndex++)
            {
                this.Entity.EntityAdapterFormulas.Move(formulasList[fIndex], fIndex);
            }
        }

        private void FrmAttributesOrderBuilder_Load(object sender, EventArgs e)
        {

        }

        private void linkToNewGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAttributeGroup attrGroup = new FrmAttributeGroup();
            attrGroup.Groups = this.lstOrder.Groups;
            if (this.lstOrder.SelectedItems.Count > 0)
                attrGroup.ListItem = this.lstOrder.SelectedItems[0];
            attrGroup.ShowDialog();
        }

        private void alterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lstOrder.SelectedItems.Count > 0)
            {
                FrmAttributeGroup attrGroup = new FrmAttributeGroup();
                attrGroup.Group = this.lstOrder.SelectedItems[0].Group;
                attrGroup.ShowDialog();
            }
        }


    }

    public class EntityAttribute
    {
        public string Name { get; set; }
        public string Key { get; set; }
        public int Order { get; set; }
        public bool IsFormula { get; set; }
        public string Group { get; set; }
    }
}
