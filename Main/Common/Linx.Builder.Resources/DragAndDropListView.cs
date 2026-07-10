using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Collections.Generic;
using Linx.Tools;
using System.Linq;


namespace Linx.Builder.Resources
{
    public partial class DragAndDropListView : ListView
    {
        List<ListItemsInfo> backupList = new List<ListItemsInfo>();
        private const string REORDER = "Reorder";

        private bool allowRowReorder = true;
        public bool AllowRowReorder
        {
            get
            {
                return this.allowRowReorder;
            }
            set
            {
                this.allowRowReorder = value;
                base.AllowDrop = value;
            }
        }

        public new SortOrder Sorting
        {
            get
            {
                return SortOrder.None;
            }
            set
            {
                base.Sorting = SortOrder.None;
            }
        }

        public DragAndDropListView()
            : base()
        {
            this.AllowRowReorder = true;
        }

        private void SaveAndClearGroups()
        {
            if (this.Groups.Count > 0)
            {
                backupList.Clear();
                foreach (ListViewItem item in this.Items)
                {
                    if (item.Group != null)
                    {
                        if (item.Group.Tag == null)
                            item.Group.Tag = item.Group.Header + "::" + item.Group.Header;  
                        backupList.Add(new ListItemsInfo(item.Text, item.Group.Tag.ToString().Left("::"), item.Group.Tag.ToString().Right("::")));
                    }
                }
                this.Groups.Clear();
            }
        }

        private void RestoreGroups()
        {
            ListItemsInfo info;
            ListViewGroup group;

            var groups = backupList.Select(e => e.GroupCode + "::" + e.GroupName).Distinct().OrderBy(e => e);
            foreach (string gr in groups)
            {
                group = this.Groups.Add(gr.Left("::"), gr.Right("::"));
                group.Tag = gr;
            }

            for (int index = 0; index < this.Items.Count; index++)
            {
                info = backupList.Where(e => e.Name == this.Items[index].Text).FirstOrDefault();
                if (info != null)
                    this.Items[index].Group = this.Groups[info.GroupCode];                  
            }
        }

        protected override void OnDragDrop(DragEventArgs e)
        {
            base.OnDragDrop(e);
            if (!this.AllowRowReorder)
            {
                return;
            }
            if (base.SelectedItems.Count == 0)
            {
                return;
            }
            Point cp = base.PointToClient(new Point(e.X, e.Y));
            ListViewItem dragToItem = base.GetItemAt(cp.X, cp.Y);
            if (dragToItem == null)
            {
                return;
            }
            int dropIndex = dragToItem.Index;
            if (dropIndex > base.SelectedItems[0].Index)
            {
                dropIndex++;
            }

            //Get target group
            string dragGroupKey = "";
            if (dragToItem.Group != null)
            {
                if (dragToItem.Group.Tag == null)
                    dragToItem.Group.Tag = dragToItem.Group.Header + "::" + dragToItem.Group.Header;
                dragGroupKey = dragToItem.Group.Tag.ToString().Left("::");
            }

            //Save and clear all groups
            this.SaveAndClearGroups();

            //Execute change
            ArrayList insertItems =
                new ArrayList(base.SelectedItems.Count);
            foreach (ListViewItem item in base.SelectedItems)
            {
                insertItems.Add(item.Clone());
            }
            for (int i = insertItems.Count - 1; i >= 0; i--)
            {
                ListViewItem insertItem = (ListViewItem)insertItems[i];
                insertItems[i] = this.Items.Insert(dropIndex, insertItem);
            }
            foreach (ListViewItem removeItem in base.SelectedItems)
            {
                base.Items.Remove(removeItem);
            }

            //Restore groups
            this.RestoreGroups();

            //Set Target Group
            if (!dragGroupKey.IsNullOrEmpty())
            {
                for (int i = insertItems.Count - 1; i >= 0; i--)
                {
                    ((ListViewItem)insertItems[i]).Group = this.Groups[dragGroupKey];
                }
            }
        }

        protected override void OnDragOver(DragEventArgs e)
        {
            if (!this.AllowRowReorder)
            {
                e.Effect = DragDropEffects.None;
                return;
            }
            if (!e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.None;
                return;
            }
            Point cp = base.PointToClient(new Point(e.X, e.Y));
            ListViewItem hoverItem = base.GetItemAt(cp.X, cp.Y);
            if (hoverItem == null)
            {
                e.Effect = DragDropEffects.None;
                return;
            }
            foreach (ListViewItem moveItem in base.SelectedItems)
            {
                if (moveItem.Index == hoverItem.Index)
                {
                    e.Effect = DragDropEffects.None;
                    hoverItem.EnsureVisible();
                    return;
                }
            }
            base.OnDragOver(e);
            String text = (String)e.Data.GetData(REORDER.GetType());
            if (text.CompareTo(REORDER) == 0)
            {
                e.Effect = DragDropEffects.Move;
                hoverItem.EnsureVisible();
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        protected override void OnDragEnter(DragEventArgs e)
        {
            base.OnDragEnter(e);
            if (!this.AllowRowReorder)
            {
                e.Effect = DragDropEffects.None;
                return;
            }
            if (!e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.None;
                return;
            }
            base.OnDragEnter(e);
            String text = (String)e.Data.GetData(REORDER.GetType());
            if (text.CompareTo(REORDER) == 0)
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        protected override void OnItemDrag(ItemDragEventArgs e)
        {
            base.OnItemDrag(e);
            if (!this.AllowRowReorder)
            {
                return;
            }
            base.DoDragDrop(REORDER, DragDropEffects.Move);
        }
    }

    public class ListItemsInfo
    {
        public string Name { get; set; }
        public string GroupCode { get; set; }
        public string GroupName { get; set; }

        public ListItemsInfo(string name, string groupCode, string groupName)
        {
            this.Name = name;
            this.GroupCode = groupCode;
            this.GroupName = groupName;
        }
    }
}


