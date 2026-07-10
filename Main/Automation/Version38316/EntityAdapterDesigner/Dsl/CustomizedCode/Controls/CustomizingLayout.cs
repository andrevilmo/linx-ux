using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Linx.Tools;
using DslModeling = global::Microsoft.VisualStudio.Modeling;
using Linx.EntityAdapterDesigner.CustomizedCode;
using System.Collections;
using System.IO;
using EnvDTE;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Xml.Linq;
using System.Xml;
using System.Xml.Xsl;
using Linx.EntityAdapterDesigner.CustomizedCode.UserControls;
using System.Text.RegularExpressions;


namespace Linx.EntityAdapterDesigner.GeneratedCode.CustomizedCode
{
    public partial class CustomizingLayout : UserControl
    {
        #region Constants
        private const string cTabControl = "TabControl";
        private const string cTabItem = "TabItem";
        private const string cExpander = "Expander";
        private const string cGroupBox = "GroupBox";
        private const string cCustomContainer = "CustomContainer";

        private const string cLookUpTextBox = "LookUpTextBox";

        #endregion

        List<TabPage> tabItems = new List<TabPage>();
        private TreeView selectedTree = null;
        private TreeNode sourceNode;
        EntityAdapterUserInterface currentLayout = null;
        CustomizedLayoutV2 layoutDefinition;

        Cursor _curDrag, _curDragCopy,
                _curDragAfter, _curDragBefore,
                _curDragCopyAfter, _curDragCopyBefore,
                _curDragInsideAfter, _curDragCopyInsideAfter;

        Bitmap _bitmap, _bitmapCopy;
        Icon _icon;
        List<TreeNode> listSelectedNodes = new List<TreeNode>();

        private DropLocation DropPositionFlag;

        private enum DropLocation
        { Up, Down, Inside }

        #region Properties
        public EntityAdapterUserInterface CurrentLayout { get { return currentLayout; } set { currentLayout = value; } }
        #endregion


        #region Cursor

        [DllImport("user32.dll")]
        public static extern IntPtr CreateIconIndirect(ref IconInfo icon);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetIconInfo(IntPtr hIcon, ref IconInfo pIconInfo);


        public struct IconInfo
        {
            public bool fIcon;
            public int xHotspot;
            public int yHotspot;
            public IntPtr hbmMask;
            public IntPtr hbmColor;
        }

        public Cursor CreateCursor(TreeNode tn, String textVariable)
        {

            SizeF size;
            SizeF szInfoText;
            Font f = treeTopGroups.Font;
            Icon icon = Icon.FromHandle(((Bitmap)layoutImageList.Images[tn.ImageIndex]).GetHicon());
            SizeF iconSize = icon.Size;
            Font fnt = new Font("Arial", 7, FontStyle.Italic);
            Boolean DrawMultiselect = false;

            using (Bitmap tmpBmp = new Bitmap(1, 1))
            using (Graphics g = Graphics.FromImage(tmpBmp))
            {
                size = g.MeasureString(tn.Text, f);
                szInfoText = g.MeasureString(textVariable, fnt);
            }

            size.Height = iconSize.Height + 18 + szInfoText.Height + 6;
            size.Width = (size.Width > szInfoText.Width ? size.Width : szInfoText.Width) + iconSize.Width + 8;

            Bitmap bitmap = new Bitmap((int)Math.Ceiling(size.Width),
                (int)Math.Ceiling(size.Height), PixelFormat.Format32bppPArgb);

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

                int y = 22;
                int x = 0;
                Size szBack = new Size(bitmap.Width - 6, bitmap.Height - 24);
                Rectangle rect = new Rectangle(new Point(x, y), szBack);

                SolidBrush sb = new SolidBrush(Color.White);
                Pen pn = new Pen(Color.LightGray);

                g.FillRectangle(sb, rect);
                g.DrawRectangle(pn, rect);

                DrawMultiselect = (listSelectedNodes.Count > 1);
                DefaultCursor.Draw(g, new Rectangle(DefaultCursor.HotSpot, new Size(18, 18)));
                if (DrawMultiselect == false)
                {
                    g.DrawIcon(icon, 3, 25);
                    g.DrawString(tn.Text, f, Brushes.Gray, iconSize.Width + 5, 25);
                    g.DrawString(textVariable, fnt, Brushes.Green, iconSize.Width + 5, 25 + szInfoText.Height + 1);
                }
                else
                {
                    Assembly myAssembly = Assembly.GetExecutingAssembly();
                    Stream myStream = myAssembly.GetManifestResourceStream("Linx.EntityAdapterDesigner.Resources.drag.png");
                    Bitmap bmp = new Bitmap(myStream);

                    g.DrawImage(bmp, 3, 25);
                    g.DrawString(listSelectedNodes.Count + " selected elements", f, Brushes.DarkGray, bmp.Width + 15, 25);
                    g.DrawString(textVariable, fnt, Brushes.Green, 3, 25 + szInfoText.Height + 1);
                    bmp.Dispose();
                    bmp = null;
                }

                sb.Dispose();
                pn.Dispose();
                sb = null;
                pn = null;

            }

            IntPtr ptr = bitmap.GetHicon();
            IconInfo tmp = new IconInfo();
            GetIconInfo(ptr, ref tmp);
            tmp.xHotspot = 0;
            tmp.yHotspot = 0;
            tmp.fIcon = false;
            ptr = CreateIconIndirect(ref tmp);

            icon.Dispose();
            bitmap.Dispose();

            fnt.Dispose();
            fnt = null;

            return new Cursor(ptr);

        }

        public void CreateCursor(TreeNode tn)
        {

            if (_bitmap != null)
                _bitmap.Dispose();

            if (_icon != null)
                _icon.Dispose();

            if (_curDragCopy != null)
                _curDragCopy.Dispose();

            _curDragCopy = null;

            if (_curDrag != null)
                _curDrag.Dispose();

            _curDrag = null;


            _icon = null;
            _bitmap = null;

            SizeF size;
            SizeF szInfoText;
            String textVariable = String.Empty;
            Font f = treeTopGroups.Font;
            _icon = Icon.FromHandle(((Bitmap)layoutImageList.Images[tn.ImageIndex]).GetHicon());
            SizeF iconSize = _icon.Size;
            Font fnt = new Font("Arial", 7, FontStyle.Italic);

            using (Bitmap tmpBmp = new Bitmap(1, 1))
            using (Graphics g = Graphics.FromImage(tmpBmp))
            {
                size = g.MeasureString(tn.Text, f);

                if (DropPositionFlag == DropLocation.Up)
                {
                    textVariable = "Before field ";
                    szInfoText = g.MeasureString("Before field (Hold Ctrl to activate copy mode.)", fnt);
                }
                else
                {
                    textVariable = "After field ";
                    szInfoText = g.MeasureString("After field (Hold Ctrl to activate copy mode.)", fnt);
                }
            }

            size.Height = iconSize.Height + 18 + szInfoText.Height + 6;
            size.Width = (size.Width > szInfoText.Width ? size.Width : szInfoText.Width) + iconSize.Width + 8;

            _bitmap = new Bitmap((int)Math.Ceiling(size.Width),
                (int)Math.Ceiling(size.Height), PixelFormat.Format32bppPArgb);

            _bitmapCopy = new Bitmap((int)Math.Ceiling(size.Width),
                        (int)Math.Ceiling(size.Height), PixelFormat.Format32bppArgb);

            using (Graphics g = Graphics.FromImage(_bitmap))
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

                int y = 22;
                int x = 0;
                Size szBack = new Size(_bitmap.Width - 6, _bitmap.Height - 24);
                Rectangle rect = new Rectangle(new Point(x, y), szBack);

                SolidBrush sb = new SolidBrush(Color.White);
                Pen pn = new Pen(Color.LightGray);

                g.FillRectangle(sb, rect);
                g.DrawRectangle(pn, rect);

                DefaultCursor.Draw(g, new Rectangle(DefaultCursor.HotSpot, new Size(18, 18)));
                g.DrawIcon(_icon, 3, 25);
                g.DrawString(tn.Text, f, Brushes.Gray, iconSize.Width + 5, 25);
                g.DrawString(textVariable + "(Hold Ctrl to activate copy mode.)", fnt, Brushes.Green, iconSize.Width + 5, 25 + szInfoText.Height + 1);

                sb.Dispose();
                pn.Dispose();
                sb = null;
                pn = null;

            }

            using (Graphics gCopy = Graphics.FromImage(_bitmapCopy))
            {
                gCopy.SmoothingMode = SmoothingMode.HighQuality;
                gCopy.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gCopy.CompositingQuality = CompositingQuality.HighQuality;
                gCopy.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

                int y = 22;
                int x = 0;
                Size szBack = new Size(_bitmap.Width - 6, _bitmap.Height - 24);
                Rectangle rect = new Rectangle(new Point(x, y), szBack);

                SolidBrush sb = new SolidBrush(Color.White);
                Pen pn = new Pen(Color.LightGray);

                gCopy.Clear(Color.Transparent);
                gCopy.FillRectangle(sb, rect);
                gCopy.DrawRectangle(pn, rect);

                DefaultCursor.Draw(gCopy, new Rectangle(DefaultCursor.HotSpot, new Size(18, 18)));
                gCopy.DrawIcon(_icon, 3, 25);
                gCopy.DrawString(tn.Text, f, Brushes.DimGray, iconSize.Width + 5, 25);
                gCopy.DrawString(textVariable + "Release mouse button to copy", fnt, Brushes.Red, iconSize.Width + 5, 25 + szInfoText.Height + 1);

                sb.Dispose();
                pn.Dispose();
                sb = null;
                pn = null;
            }

            IntPtr ptr = _bitmap.GetHicon();
            IconInfo tmp = new IconInfo();
            GetIconInfo(ptr, ref tmp);
            tmp.xHotspot = 0;
            tmp.yHotspot = 0;
            tmp.fIcon = false;
            ptr = CreateIconIndirect(ref tmp);

            IntPtr ptrCopy = _bitmapCopy.GetHicon();
            IconInfo tmpCopy = new IconInfo();
            GetIconInfo(ptrCopy, ref tmpCopy);
            tmpCopy.xHotspot = 0;
            tmpCopy.yHotspot = 0;
            tmpCopy.fIcon = false;
            ptrCopy = CreateIconIndirect(ref tmpCopy);

            fnt.Dispose();
            fnt = null;

            _curDragCopy = new Cursor(ptrCopy);
            _curDrag = new Cursor(ptr);

        }

        public ImageAttributes SetImageOpacity(Image image, float opacity)
        {
            try
            {
                Bitmap bmp = new Bitmap(image.Width, image.Height);

                Graphics gfx = Graphics.FromImage(bmp);

                ColorMatrix matrix = new ColorMatrix();

                matrix.Matrix33 = opacity;

                ImageAttributes attributes = new ImageAttributes();

                attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                return attributes;
            }
            catch
            {
                return null;
            }
        }

        #endregion

        public CustomizingLayout()
        {
            InitializeComponent();

            //Get all pages
            foreach (TabPage page in tabInformations.TabPages)
                tabItems.Add(page);

            this.tabInformations.TabPages.Clear();
            this.GiveFeedback += new GiveFeedbackEventHandler(CustomizingLayout_GiveFeedback);

            this.DashboardSizeWidth.Items.AddRange(new string[] { "Small", "Medium", "Large" });

            this.cboGroupStyle.Items.AddRange(Enum.GetNames(typeof(ContainerStyle)));

            this.cmbControlWidth.Items.AddRange(new string[] { "Automatic", "Mini", "ExtraSmall", "Small", "MinMedium", "Medium", "ExtraMedium", "Large", "ExtraLarge" });

            this.cmbMediaWidth.Items.AddRange(Enum.GetNames(typeof(MediaWidth)));
            this.FieldFontControl.FontPropertyChanged += FieldFontControl_FontPropertyChanged;
            this.cboGridWidth.Items.AddRange(Enum.GetNames(typeof(GridSizeWidth)));
            this.cboGridHeight.Items.AddRange(Enum.GetNames(typeof(GridSizeHeight)));
            this.cmbFieldVisibleGridEditor.Items.AddRange(Enum.GetNames(typeof(VisibleFieldGrid)));
        }



        public bool StoreCurrentlayout(bool reset)
        {
            if (!currentLayout.IsNull() && !this.layoutDefinition.IsNull())
            {
                this.AdjustAllAuxiliaryInformations(false);
                if (!reset && this.HasStructureError()) return false;
                this.CreateDatagridDefinitionsV2();
                this.currentLayout.StoreCurrentlayout(this.layoutDefinition, reset);
            }

            return true;
        }

        public bool HasStructureError()
        {
            bool hasError = false;
            int multiSelectorCount = 0;

            if (!this.layoutDefinition.IsNull())
            {
                if (this.layoutDefinition.Containers.Any(e => e.ClassName == cCustomContainer))
                {
                    MessageBox.Show("[ContentControl] cannot be used as a top level container. Replace it by an [Expander] or a [GroupBox].", "UI Inconsistency: [" + this.currentLayout.Name + "]", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return true;
                }

                Action<LayoutElement> verify = null;
                verify = (element) =>
                    {
                        if (!hasError && element is LayoutContainer)
                        {
                            if (element.ClassName == "DataGrid" && ((LayoutContainer)element).EnableMultiSelection)
                                multiSelectorCount++;

                            if (multiSelectorCount > 1)
                            {
                                MessageBox.Show("Only one Multi-Select DataGrid is allowed.", "UI Inconsistency: [" + this.currentLayout.Name + "]", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                hasError = true;
                            }
                            else
                            {
                                //Adjust ComboBox
                                foreach (var domainControl in ((LayoutContainer)element).Controls.Where(e => e is LayoutControlV2 && !((LayoutControlV2)e).DomainName.IsNullOrEmpty() && ((LayoutControlV2)e).ClassName != "ComboBox").ToArray())
                                {
                                    MessageBox.Show(String.Format("There is a domain field that is not used in a Combobox. Verify the element called [{0}].", (domainControl.DisplayName.IsNullOrEmpty() ? domainControl.Name : domainControl.DisplayName)), "UI Inconsistency: [" + this.currentLayout.Name + "]", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    hasError = true;
                                    break;
                                }

                                var combo = ((LayoutContainer)element).Controls.FirstOrDefault(c => c is LayoutControlV2 && c.ClassName == "ComboBox" && !c.BindingPath.IsNullOrEmpty() && ((LayoutControlV2)c).DomainName.IsNullOrEmpty() && ((LayoutControlV2)c).LookUpName.IsNullOrEmpty());
                                if (!hasError && !combo.IsNull())
                                {
                                    MessageBox.Show(string.Format("The combo [{0}] hasn't  lookup or domain information!", (combo.DisplayName.IsNullOrEmpty() ? combo.Name : combo.DisplayName)), "UI Inconsistency: [" + this.currentLayout.Name + "]", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    hasError = true;
                                }

                                if (!hasError)
                                {
                                    var control = ((LayoutContainer)element).Controls.FirstOrDefault(e => e is LayoutControlV2 && !e.BindingPath.IsNullOrEmpty());
                                    var container = ((LayoutContainer)element).Controls.FirstOrDefault(e => e is LayoutContainer);

                                    if (control != null && container != null)
                                    {
                                        MessageBox.Show(String.Format("Controls and Containers cannot be in the same parent container. Verify the element called [{0}].", (control.DisplayName.IsNullOrEmpty() ? control.Name : control.DisplayName)), "UI Inconsistency: [" + this.currentLayout.Name + "]", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        hasError = true;
                                    }

                                    if (!hasError && control != null)
                                    {
                                        Func<LayoutElement, string> getBindingPath = (c) => c.BindingPath.Substring(0, c.BindingPath.LastIndexOf('.'));
                                        var bindingRef = getBindingPath(control);
                                        var controlDiv = ((LayoutContainer)element).Controls.FirstOrDefault(e => !e.BindingPath.IsNullOrEmpty() && getBindingPath(e) != bindingRef);
                                        if (controlDiv != null)
                                        {
                                            MessageBox.Show(String.Format("Controls from different sources cannot be in the same parent container. Verify the elements [{0}] and [{1}].", (control.DisplayName.IsNullOrEmpty() ? control.Name : control.DisplayName), (controlDiv.DisplayName.IsNullOrEmpty() ? controlDiv.Name : controlDiv.DisplayName)), "UI Inconsistency: [" + this.currentLayout.Name + "]", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            hasError = true;
                                        }
                                    }

                                    if (!hasError)
                                        ((LayoutContainer)element).Controls.ForEach(e => verify(e));
                                }
                            }
                        }
                    };
                this.layoutDefinition.Containers.ForEach(e => verify(e));
            }

            return hasError;
        }

        private void CreateDatagridDefinitionsV2()
        {
            //This Layout is generated dynamically, therefore DataGridViewLayout was discontinued.
            this.layoutDefinition.DataGridViewLayout = null;
        }

        public void ShowLayout(DslModeling::ModelElement modelElement)
        {
            this.tabFieldInformations.IsAccessible = this.tabGroupInformations.IsAccessible = false;
            if (modelElement is EntityAdapterUserInterface)
            {
                EntityAdapterUserInterface layout = (EntityAdapterUserInterface)modelElement;
                if (currentLayout != layout)
                {
                    this.RefreshInheritance(layout);
                    this.RefreshOnLoad(layout);
                }
                else
                    this.AdjustAllAuxiliaryInformations(true);
            }
        }


        private void RefreshInheritance(EntityAdapterUserInterface layout)
        {
            if (layout.BaseUserInterface != null)
            {
                this.StoreCurrentlayout(false);
                var baseDef = layout.BaseUserInterface.GetNewLayoutDefinition();
                if (baseDef != null)
                {
                    baseDef.SetDerived();
                    using (Microsoft.VisualStudio.Modeling.Transaction transaction =
                            layout.Store.TransactionManager.BeginTransaction("Change dynamic layout."))
                    {
                        if (!layout.LayoutContent.IsNullOrEmpty())
                            baseDef.MergeLayout(layout.GetNewLayoutDefinition());

                        layout.LayoutContent = Linx.Tools.SerializationManager<CustomizedLayoutV2>.ObjectToJson(baseDef);

                        transaction.Commit();
                    }
                }
            }
        }

        private void RefreshOnLoad(EntityAdapterUserInterface layout)
        {
            Refresh(layout, false);
            this.AdjustAllAuxiliaryInformations(true);
        }


        private void Refresh(EntityAdapterUserInterface layout, bool reset)
        {
            if (layout == null)
                return;

            try
            {
                PublicationEntity entityAdapter = null;
                if (reset)
                {
                    currentLayout = layout;
                    entityAdapter = this.currentLayout.GetEntityAdapter();
                    this.StoreCurrentlayout(true);
                    this.layoutDefinition = new CustomizedLayoutV2() { Name = currentLayout.Name, DataType = (entityAdapter.IsNull() ? currentLayout.SubscriptionEntityAdapterName : entityAdapter.Name) };
                    this.layoutDefinition.CheckVersion();
                }
                else
                {
                    this.StoreCurrentlayout(false);
                    currentLayout = layout;
                    entityAdapter = this.currentLayout.GetEntityAdapter();
                    if (!currentLayout.LayoutContent.IsNullOrEmpty())
                    {
                        this.layoutDefinition = EntityAdapterUserInterface.GetLayoutDefinition(currentLayout.LayoutContent);
                        this.layoutDefinition.CheckVersion();
                    }
                    else
                    {
                        this.layoutDefinition = new CustomizedLayoutV2() { Name = currentLayout.Name, DataType = (entityAdapter.IsNull() ? currentLayout.SubscriptionEntityAdapterName : entityAdapter.Name) };
                        this.layoutDefinition.CheckVersion();
                    }
                }

                //Adjust SizeGridConfigurations
                if (!entityAdapter.IsNull())
                    this.layoutDefinition.SizeGridConfigurations = entityAdapter.SizeGridConfigurations;

                //Reset all controls
                this.layoutDefinition.ResetSync();
                if (entityAdapter != null)
                    this.layoutDefinition.MetaDataKeys = entityAdapter.GetMetadataKeys();

                checkEnableMedias.Checked = this.layoutDefinition.EnableMedias;
                chShowAllLabelsForConnectedFields.Checked = this.layoutDefinition.ShowAllLabelsForConnectedFields;
                checkRemoveDataToolbar.Checked = this.layoutDefinition.RemoveDataToolbar;
                this.ckTopCanClear.Checked = this.layoutDefinition.CanClear;
                this.ckTopCanSearch.Checked = this.layoutDefinition.CanSearch;
                this.ckTopCanAddNew.Checked = this.layoutDefinition.CanAddNew;
                this.ckTopCanEdit.Checked = this.layoutDefinition.CanEdit;
                this.ckTopCanDelete.Checked = this.layoutDefinition.CanDelete;
                this.ckTopCanCustomSearch.Checked = this.layoutDefinition.CanCustomSearch;
                this.ckTopCanPrint.Checked = this.layoutDefinition.CanPrint;
                this.ckTopCanLayout.Checked = this.layoutDefinition.CanLayout;
                this.ckTopCanNavigate.Checked = this.layoutDefinition.CanNavigate;
                this.ckTopCanExport.Checked = this.layoutDefinition.CanExport;

                //Fill Structures
                this.FillGroups();
                this.FillChildren();


                //Remove eliminated fields
                this.layoutDefinition.RemoveNoSyncElements();

                CreateDatagridDefinitionsV2();

                //Fill Trees
                treeTopGroups.Nodes.Clear();
                //this.FillTreeGroups();
                this.FillTreeContainers(null, null);
                //this.FillTreeChildren();
                this.FillRemoveds();

                RefershBasedTree(treeTopGroups);
                RefershBasedTree(treeRemovedItems);
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        public void RefershBasedTree(TreeView tree)
        {
            Action<TreeNode> action = null;
            action = node =>
                {
                    SetColor(node);
                    foreach (TreeNode childNode in node.Nodes)
                        action(childNode);
                };
            foreach (TreeNode childNode in tree.Nodes)
                action(childNode);
        }

        private void SetColor(TreeNode node)
        {
            if (node.Tag != null && node.Tag is LayoutElement)
                node.ForeColor = (((LayoutElement)node.Tag).IsDerived ? Color.Gray : Color.Black);
        }

        private void FillRemoveds()
        {
            this.treeRemovedItems.Nodes[0].Nodes.Clear();
            this.treeRemovedItems.Nodes[1].Nodes.Clear();
            foreach (LayoutControlV2 control in this.layoutDefinition.RemovedLayoutElements.Where(e => e is LayoutControlV2))
            {
                this.AddRemovedControlNode(control);
            }

            foreach (LayoutContainer page in this.layoutDefinition.RemovedLayoutElements.Where(e => e is LayoutContainer))
            {
                this.AddRemovedPageNode(page);
            }
            this.treeRemovedItems.ExpandAll();
        }

        private void FillTreeContainers(List<LayoutContainer> lc, TreeNode nod)
        {
            TreeNode groupNode, newNode;
            if (lc == null)
                lc = layoutDefinition.Containers;

            if (lc.Count > 0)
            {
                foreach (LayoutContainer containerItem in lc)
                {
                    if (nod == null)
                        groupNode = treeTopGroups.Nodes.Add(containerItem.Name, containerItem.DisplayName, GetIconIndexFromClassName(containerItem.ClassName), GetIconIndexFromClassName(containerItem.ClassName));
                    else
                        groupNode = nod.Nodes.Add(containerItem.Name, containerItem.DisplayName, GetIconIndexFromClassName(containerItem.ClassName), GetIconIndexFromClassName(containerItem.ClassName));
                    containerItem.ImageIndex = GetIconIndexFromClassName(containerItem.ClassName);
                    groupNode.Tag = containerItem;
                    //groupNode.NodeFont = new Font("Arial Black", 8, FontStyle.Bold);
                    foreach (var element in containerItem.Controls)
                    {
                        if (element is LayoutContainer)
                            FillTreeContainers(new List<LayoutContainer>() { (LayoutContainer)element }, groupNode);
                        else
                        {
                            element.ImageIndex = GetIconIndexFromClassName(element.ClassName);
                            newNode = groupNode.Nodes.Add(element.Name, element.DisplayName, GetIconIndexFromClassName(element.ClassName), GetIconIndexFromClassName(element.ClassName));
                            newNode.Tag = element;
                        }
                    }
                    groupNode.Expand();
                }
            }

            if (treeTopGroups.Nodes.Count > 0)
            {
                treeTopGroups.SelectedNode = treeTopGroups.Nodes[0];
                treeTopGroups.Select();
                treeTopGroups.Invalidate();
            }
        }

        private void FillGroups()
        {
            bool isRemoved = false; ;
            LayoutContainer lcontainer = null;
            LayoutControlV2 lControl = null;
            PublicationEntity entityAdapter = currentLayout.GetEntityAdapter();
            if (entityAdapter != null)
            {
                string bindingPath = this.GetBindingPath(entityAdapter);
                foreach (var element in entityAdapter.Properties.Where(e => e.IsBrowsable).OrderBy(r => r.DisplayOrder))
                {
                    isRemoved = false;
                    lControl = null;
                    lcontainer = layoutDefinition.GetContainerByControlBindingPath(bindingPath + "." + element.Name);
                    if (lcontainer == null)
                    {
                        lcontainer = layoutDefinition.GetContainerByName(entityAdapter.Name);
                        if (lcontainer == null)
                        {
                            lControl = layoutDefinition.GetRemovedControlByBindingPath(bindingPath + "." + element.Name) as LayoutControlV2;
                            if (lControl == null)
                            {
                                lcontainer = new LayoutContainer() { Name = entityAdapter.Name, DisplayName = entityAdapter.Name, ImageIndex = GetIconIndexFromClassName("Expander"), ColumnCount = 2, ClassName = "Expander" };
                                layoutDefinition.Containers.Add(lcontainer);
                            }
                            else isRemoved = true;
                        }
                    }

                    //Adjust Name and Class 
                    if (!lcontainer.IsNull())
                    {
                        if (lcontainer.Name.IsNullOrEmpty())
                            lcontainer.Name = entityAdapter.Name + Guid.NewGuid().ToString().Replace("-", String.Empty);

                        if (lcontainer.ClassName.IsNullOrEmpty())
                            lcontainer.ClassName = "Expander";
                        if (lcontainer.SizeGridConfigurations != entityAdapter.SizeGridConfigurations)
                            lcontainer.SizeGridConfigurations = entityAdapter.SizeGridConfigurations;
                    }

                    if (lControl == null)
                        lControl = layoutDefinition.GetControlByBindingPath(bindingPath + "." + element.Name);
                    if (lControl == null)
                    {
                        lControl = (LayoutControlV2)layoutDefinition.GetRemovedControlByBindingPath(bindingPath + "." + element.Name);
                        isRemoved = (lControl != null);
                    }
                    if (lControl == null)
                    {
                        lControl = new LayoutControlV2() { Name = element.Name, DataType = element.DataType, SourceViewName = entityAdapter.Name, DisplayName = element.DisplayName, ImageIndex = GetIconIndexFromClassName(element.DisplayControl.ToString()), ClassName = element.DisplayControl.ToString(), ActionEvent = String.Empty, IsCustomized = false, IsDataField = true, IsVisible = element.IsBrowsable, AggregationFunction = element.AggregationFunction.ToString(), IsEditable = element.IsEditable, ConnectedAttribute = element.ConnectedAttribute, DataFormatString = element.DataFormatString, Precision = element.Precision, ToolTip = element.Description, IsMeasure = element.IsMeasure, MeasureFormula = element.MeasureFormula, Mask = element.Mask, MaskType = element.MaskType };
                        lcontainer.Controls.Add(lControl);
                    }
                    else
                    {
                        if (!element.DataFormatString.IsNullOrEmpty() && lControl.DataFormatString.IsNullOrEmpty())
                            lControl.DataFormatString = element.DataFormatString;
                        if (!element.Precision.IsNullOrEmpty() && lControl.Precision.IsNullOrEmpty())
                            lControl.Precision = element.Precision;
                    }

                    if (!lControl.Sync)
                        lControl.Sync = !isRemoved;
                    lControl.IsParentBind = true;
                    if (lControl.BindingPath.IsNullOrEmpty())
                        lControl.BindingPath = bindingPath + "." + element.Name;

                    this.layoutDefinition.AdjustSyncCopies(lControl);
                }

                //Add Multimedia
                this.AddMultimediaControl(null, entityAdapter, null);
            }
        }

        private void AdjustAuxiliaryInformations(LayoutControlV2 lControl, PublicationProperty element, bool isOnLoad)
        {
            //Adjust Auxiliary Informations
            lControl.DataType = element.DataType;
            if (isOnLoad && element.Range != lControl.Range)
                lControl.Range = element.Range;
            lControl.IsPartOfKey = element.IsPrimaryKey;
            lControl.IsNullable = element.IsNullable();
            if (lControl.DomainName != element.DomainName)
                lControl.DomainName = element.DomainName;
            if (lControl.KpiName != element.KpiName)
                lControl.KpiName = element.KpiName;
            string lookUpName = LookUpAdapter.GetLookUpName(element.LookUpInfo);
            if (lControl.LookUpName != lookUpName)
                lControl.LookUpName = lookUpName;

            if (!lControl.LookUpName.IsNullOrEmpty())
            {
                var lookup = this.currentLayout.EntityAdapterDesignerRoot.LookUpAdapters.FirstOrDefault(e => e.Name == lControl.LookUpName);
                if (lookup != null)
                    lControl.HasLookupFilter = lookup.HasAnyClientFilter();
            }
            else
                lControl.HasLookupFilter = false;

            if (element.NoUpdate && !lControl.EditableOnInsert && (lControl.IsEditable || lControl.AlwaysEditable))
            {
                lControl.EditableOnInsert = true;
                lControl.IsEditable = false;
                lControl.AlwaysEditable = false;
            }

            string substProp = LookUpAdapter.GetSubstituteProperties(element.LookUpInfo);
            if (lControl.SubstituteProperties != substProp)
                lControl.SubstituteProperties = substProp;
            lControl.MultiSelection = LookUpAdapter.GetMultiSelectionValue(element.LookUpInfo);
        }

        private void AdjustAllAuxiliaryInformations(bool isOnLoad)
        {
            if (this.layoutDefinition == null)
                return;

            var parentEntity = this.currentLayout.GetEntityAdapter();

            if (parentEntity == null)
                return;

            Action<LayoutElement> ajustInfo = null;
            ajustInfo = (element) =>
            {
                if (element is LayoutContainer)
                {
                    //Adjus DataGrid Editing
                    if (((LayoutContainer)element).ClassName == "DataGrid")
                    {
                        var innerElement = ((LayoutContainer)element).Controls.FirstOrDefault(e => !e.BindingPath.IsNullOrEmpty());
                        if (innerElement != null)
                        {
                            var entity = parentEntity;
                            var parts = (innerElement.BindingPath.Right("DataElement.DataView.")).Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                            if (parts.Length > 0)
                            {
                                if (parts.Length > 1)
                                    entity = GetEntityAdapterByName((parts[parts.Length - 2] + "#").Left("PagedList#"), entity);
                            }
                        }
                    }
                    //Adjust Inner Controls
                    ((LayoutContainer)element).Controls.ForEach(e => ajustInfo(e));
                }
                else if (element is LayoutControlV2)
                {
                    if (!element.BindingPath.IsNullOrEmpty())
                    {
                        var entity = parentEntity;
                        var parts = (element.BindingPath.Right("DataElement.DataView.")).Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length > 0)
                        {
                            if (parts.Length > 1)
                                entity = GetEntityAdapterByName((parts[parts.Length - 2] + "#").Left("PagedList#"), entity);

                            if (entity != null)
                            {
                                var property = entity.Properties.FirstOrDefault(e => e.Name == parts[parts.Length - 1]);
                                if (property != null)
                                    AdjustAuxiliaryInformations((LayoutControlV2)element, property, isOnLoad);
                            }
                        }
                    }
                }
            };
            this.layoutDefinition.Containers.ForEach(e => ajustInfo(e));
        }

        private void FillChildren()
        {
            var entity = currentLayout.GetEntityAdapter();
            if (entity != null)
            {
                FillChildren(entity, layoutDefinition.Containers);
            }
        }

        private string GetBindingPath(PublicationEntity entity)
        {
            if (entity.Parent.IsNull())
                return "DataElement.DataView";
            else
            {
                string bindingPath = entity.Name + "PagedList";
                PublicationEntity parent = entity.Parent;
                while (true)
                {
                    if (parent.Parent.IsNull())
                    {
                        bindingPath = "DataElement.DataView." + bindingPath;
                        break;
                    }
                    bindingPath = parent.Name + "PagedList" + "." + bindingPath;
                    parent = parent.Parent;
                }

                return bindingPath;
            }
        }


        private void FillChildren(PublicationEntity entityParent, IList pages)
        {
            List<PublicationEntity> details = entityParent.Details;
            bool isRemoved = false;
            if (details.IsNull() || details.Count == 0)
                return;

            LayoutContainer newPage;
            LayoutContainer newPageDad;
            LayoutControlV2 lControl;
            LayoutContainer lGroup = null;

            newPageDad = this.layoutDefinition.GetContainerByName(entityParent.Name + "TabControl");
            if (newPageDad == null)
            {
                newPageDad = this.layoutDefinition.GetRemovedContainerByName(entityParent.Name + "TabControl");
                if (newPageDad == null)
                {
                    newPageDad = new LayoutContainer() { Name = entityParent.Name + "TabControl", DisplayName = (entityParent.DisplayName.IsNullOrEmpty() ? entityParent.Name : entityParent.DisplayName), ImageIndex = GetIconIndexFromClassName("TabControl"), ClassName = "TabControl" };
                    pages.Add(newPageDad);
                }
            }

            foreach (PublicationEntity entity in details)
            {
                newPage = this.layoutDefinition.GetContainerByName(entity.Name + "TabItem");
                if (newPage == null)
                {
                    newPage = this.layoutDefinition.GetRemovedContainerByName(entity.Name + "TabItem");
                    if (newPage == null)
                    {
                        newPage = new LayoutContainer() { Name = entity.Name + "TabItem", DisplayName = (entity.DisplayName.IsNullOrEmpty() ? entity.Name : entity.DisplayName), ImageIndex = GetIconIndexFromClassName("TabItem"), ClassName = "TabItem", ParentName = newPageDad.Name };
                        newPageDad.Controls.Add(newPage);
                    }
                }

                lGroup = this.layoutDefinition.GetContainerByName(entity.Name);
                if (lGroup == null)
                {
                    lGroup = this.layoutDefinition.GetContainerByName(entity.Name + "List");
                    if (lGroup == null)
                    {
                        lGroup = this.layoutDefinition.GetRemovedContainerByName(entity.Name);
                        if (lGroup == null)
                        {
                            lGroup = this.layoutDefinition.GetRemovedContainerByName(entity.Name + "List");
                            if (lGroup == null)
                            {
                                lGroup = new LayoutContainer() { Name = entity.Name, ClassName = "DataGrid", DisplayName = "DataGrid", ImageIndex = GetIconIndexFromClassName("DataGrid"), ParentName = newPage.Name };
                                newPage.Controls.Add(lGroup);
                            }
                            else
                            {
                                lGroup.Name = entity.Name;
                                lGroup.ClassName = "DataGrid";
                                lGroup.ImageIndex = GetIconIndexFromClassName("DataGrid");
                                lGroup.ParentName = newPage.Name;
                                newPage.DisplayName = lGroup.DisplayName;
                            }
                        }
                    }
                    else
                    {
                        this.layoutDefinition.RemoveContainerByName(lGroup.Name);
                        lGroup.Name = entity.Name;
                        lGroup.ClassName = "DataGrid";
                        lGroup.ImageIndex = GetIconIndexFromClassName("DataGrid");
                        lGroup.ParentName = newPage.Name;
                        newPage.Controls.Add(lGroup);
                        newPage.DisplayName = lGroup.DisplayName;
                    }
                }

                if (lGroup.IsNull())
                    return;

                //Adjust BindingPath
                if (lGroup.BindingPath.IsNullOrEmpty())
                    lGroup.BindingPath = GetBindingPath(entity);

                //Adjust SizeGridConfigurations
                newPage.SizeGridConfigurations = entity.SizeGridConfigurations;
                lGroup.SizeGridConfigurations = entity.SizeGridConfigurations;

                foreach (var element in entity.Properties.Where(e => e.IsBrowsable).OrderBy(r => r.DisplayOrder))
                {
                    isRemoved = false;
                    lControl = this.layoutDefinition.GetControlByBindingPath(lGroup.BindingPath + "." + element.Name);
                    if (lControl == null)
                    {
                        lControl = (LayoutControlV2)this.layoutDefinition.GetRemovedControlByBindingPath(lGroup.BindingPath + "." + element.Name);
                        isRemoved = (lControl != null);
                    }
                    if (lControl == null)
                        lControl = this.layoutDefinition.GetContainerControlByName(lGroup.Name, element.Name);
                    if (lControl == null)
                    {
                        lControl = (LayoutControlV2)this.layoutDefinition.GetRemovedContainerOrControlByName(lGroup.Name, element.Name);
                        isRemoved = (lControl != null);
                    }
                    if (lControl == null)
                        lControl = this.layoutDefinition.GetContainerControlByName(lGroup.Name + "List", element.Name);
                    if (lControl == null)
                    {
                        lControl = (LayoutControlV2)this.layoutDefinition.GetRemovedContainerOrControlByName(lGroup.Name + "List", element.Name);
                        isRemoved = (lControl != null);
                    }
                    if (lControl == null)
                    {
                        lControl = new LayoutControlV2() { Name = element.Name, DataType = element.DataType, SourceViewName = entity.Name, DisplayName = element.DisplayName, ImageIndex = GetIconIndexFromClassName(element.DisplayControl.ToString()), ClassName = element.DisplayControl.ToString(), ActionEvent = String.Empty, IsCustomized = false, IsDataField = true, IsVisible = element.IsBrowsable, AggregationFunction = element.AggregationFunction.ToString(), IsEditable = element.IsEditable, ConnectedAttribute = element.ConnectedAttribute, DataFormatString = element.DataFormatString, Precision = element.Precision, ToolTip = element.Description, IsMeasure = element.IsMeasure, MeasureFormula = element.MeasureFormula, Mask = element.Mask, MaskType = element.MaskType };
                        //newPage.Controls.Add(lControl);
                        lGroup.Controls.Add(lControl);
                    }
                    else
                    {
                        if (!element.DataFormatString.IsNullOrEmpty() && lControl.DataFormatString.IsNullOrEmpty())
                            lControl.DataFormatString = element.DataFormatString;
                        if (!element.Precision.IsNullOrEmpty() && lControl.Precision.IsNullOrEmpty())
                            lControl.Precision = element.Precision;
                    }
                    lControl.ParentName = lGroup.Name;
                    lControl.Sync = !isRemoved;
                    if (lControl.BindingPath.IsNullOrEmpty())
                        lControl.BindingPath = lGroup.BindingPath + "." + element.Name;

                    this.layoutDefinition.AdjustSyncCopies(lControl);
                }

                //Reset Empty Container
                if (!newPageDad.IsNullOrEmpty())
                    if (newPageDad.Controls.Count == 0)
                        pages.Remove(newPageDad);


                //Add Multimedia
                //this.AddMultimediaControl(entityParent, entity, newPage);
                this.AddMultimediaControl(entityParent, entity, lGroup);

                FillChildren(entity, lGroup.Controls);
            }
        }

        private String GetControlClassNameByType(String TypeName, String FieldName)
        {
            String typeLower = TypeName.ToLower();
            String fieldNameLower = FieldName.ToLower();

            if (fieldNameLower.Contains("id"))
                return cLookUpTextBox;

            else if (fieldNameLower.Contains("lx"))
                return "ComboBox";

            else if (typeLower.Contains("decimal")
                || typeLower.Equals("byte")
                || typeLower.Contains("double")
                || typeLower.Contains("int16")
                || typeLower.Contains("int32")
                || typeLower.Contains("int64")
                || typeLower.Contains("sbyte")
                || typeLower.Contains("float"))

                return "NumericTextBox";

            else if (typeLower.Contains("datetime")
                     || typeLower.Contains("datetimeoffset"))

                return "DateTimeTextBox";

            else if (typeLower.Contains("boolean"))

                return "CheckBox";

            else if (typeLower.Contains("binary")
                || typeLower.Contains("byte[]"))

                return "MultimediaControl";

            else

                return "TextBox";

        }

        private Boolean IsControlContainer(String ClassName)
        {
            return (ClassName == "Expander" || ClassName == "GroupBox" || ClassName == "CustomContainer" || ClassName == "TabItem" || ClassName == "DataGrid" || ClassName == null);
        }

        private void PrepareMenu(TreeNode NodeSelected)
        {
            if (NodeSelected == null)
                return;

            bool isDerived = ((NodeSelected.Tag != null && NodeSelected.Tag is LayoutElement && ((LayoutElement)NodeSelected.Tag).IsDerived));
            tlbbtnRemoveControls.Enabled = !isDerived;
            drpbtnModify.Enabled = !isDerived;
            //tlbbtnAddControls.Enabled = !isDerived;
            tlbMoveUp.Enabled = !isDerived;
            tlbMoveDown.Enabled = !isDerived;
            tabInformations.Enabled = !isDerived;
            btRestoreItem.Enabled = !isDerived;
            btRemoveItem.Enabled = !isDerived;
            mnuCut.Enabled = !isDerived;
            //mnuCopy.Enabled = !isDerived;
            //mnuPaste.Enabled = !isDerived;
            mnuMoveUp.Enabled = !isDerived;
            mnuMoveDown.Enabled = !isDerived;

            mnuConvertToDatagrid.Visible = false;
            convertToFlatPivotGridToolStripMenuItem.Visible = false;
            mnuConvertToExpander.Visible = false;
            mnuConvertToGroupBox.Visible = false;
            mnuConvertToTab.Visible = false;
            mnuConvertToTreeListView.Visible = false;
            mnuConvertToWizard.Visible = false;
            mnuConvertToDockManager.Visible = false;
            mnuContentControl.Visible = false;


            if (treeTopGroups.SelectedNode.Tag is LayoutContainer)
            {
                if (((LayoutContainer)treeTopGroups.SelectedNode.Tag).ClassName == "Expander")
                {
                    mnuContentControl.Visible = true;
                    mnuConvertToGroupBox.Visible = true;
                    mnuConvertToWizard.Visible = true;
                    mnuConvertToDockManager.Visible = true;
                    mnuConvertToTab.Visible = true;
                    mnuConvertToDatagrid.Visible = true;
                    mnuConvertToTreeListView.Visible = true;
                    convertToFlatPivotGridToolStripMenuItem.Visible = true;
                }

                if (((LayoutContainer)treeTopGroups.SelectedNode.Tag).ClassName == "GroupBox")
                {
                    mnuContentControl.Visible = true;
                    mnuConvertToExpander.Visible = true;
                    mnuConvertToWizard.Visible = true;
                    mnuConvertToDockManager.Visible = true;
                    mnuConvertToTab.Visible = true;
                    mnuConvertToDatagrid.Visible = true;
                    mnuConvertToTreeListView.Visible = true;
                    convertToFlatPivotGridToolStripMenuItem.Visible = true;
                }

                if (((LayoutContainer)treeTopGroups.SelectedNode.Tag).ClassName == "CustomContainer")
                {
                    mnuConvertToExpander.Visible = true;
                    mnuConvertToGroupBox.Visible = true;
                    mnuConvertToWizard.Visible = true;
                    mnuConvertToDockManager.Visible = true;
                    mnuConvertToTab.Visible = true;
                    mnuConvertToDatagrid.Visible = true;
                    mnuConvertToTreeListView.Visible = true;
                    convertToFlatPivotGridToolStripMenuItem.Visible = true;

                }

                if (((LayoutContainer)treeTopGroups.SelectedNode.Tag).ClassName == "DataGrid")
                {
                    mnuConvertToTreeListView.Visible = true;
                    mnuContentControl.Visible = true;
                    mnuConvertToDatagrid.Visible = false;
                    mnuConvertToTreeListView.Visible = true;
                    convertToFlatPivotGridToolStripMenuItem.Visible = true;
                }

                if (((LayoutContainer)treeTopGroups.SelectedNode.Tag).ClassName == "FlatPivotGrid")
                {
                    mnuConvertToTreeListView.Visible = true;
                    mnuContentControl.Visible = true;
                    mnuConvertToDatagrid.Visible = true;
                    mnuConvertToTreeListView.Visible = true;
                }

                if (((LayoutContainer)treeTopGroups.SelectedNode.Tag).ClassName == "TreeListView")
                {
                    mnuConvertToDatagrid.Visible = true;
                    mnuConvertToTreeListView.Visible = true;
                    convertToFlatPivotGridToolStripMenuItem.Visible = true;
                }

                if (((LayoutContainer)treeTopGroups.SelectedNode.Tag).ClassName == "WizardControl")
                {
                    mnuWizardItem.Visible = true;
                    mnuConvertToTab.Visible = true;
                }

            }

        }


        private int GetIconIndexFromClassName(String ClassName)
        {
            int codigo;

            switch (ClassName)
            {
                case null:
                    codigo = 0;
                    break;
                case "CheckBox":
                    codigo = 6;
                    break;
                case "Label":
                    codigo = 3;
                    break;
                case "Chart":
                    codigo = 9;
                    break;
                case "Expander":
                    codigo = 26;
                    break;
                case "TabItem":
                    codigo = 13;
                    break;
                case "ComboBox":
                    codigo = 7;
                    break;
                case "DateTimeTextBox":
                    codigo = 10;
                    break;
                case "EditBox":
                    codigo = 15;
                    break;
                case "EconomicGroup":
                    codigo = 14;
                    break;
                case cLookUpTextBox:
                    codigo = 16;
                    break;
                case "MultimediaControl":
                    codigo = 4;
                    break;
                case "NumericTextBox":
                    codigo = 11;
                    break;
                case "TextBox":
                    codigo = 1;
                    break;
                case "Button":
                    codigo = 5;
                    break;
                case "TextBlock":
                    codigo = 3;
                    break;
                case "DataGrid":
                    codigo = 17;
                    break;
                case "TabControl":
                    codigo = 12;
                    break;
                case "MaskedTextBox":
                    codigo = 18;
                    break;
                case "CustomContainer":
                    codigo = 20;
                    break;
                case "TreeListView":
                    codigo = 21;
                    break;
                case "WizardItem":
                    codigo = 22;
                    break;
                case "WizardControl":
                    codigo = 23;
                    break;
                case "KpiBox":
                    codigo = 24;
                    break;
                case "Gauge":
                    codigo = 25;
                    break;
                case "FlatPivotGrid":
                    codigo = 27;
                    break;
                case "OlapPivotGrid":
                    codigo = 28;
                    break;
                case "PivotChart":
                    codigo = 29;
                    break;
                case "PivotDrillDownChart":
                    codigo = 30;
                    break;
                case "DockItem":
                    codigo = 31;
                    break;
                case "DockManager":
                    codigo = 32;
                    break;
                case "GroupBox":
                    codigo = 0;
                    break;
                case "ExternalUI":
                    codigo = 33;
                    break;
                case "RadioButtonGroup":
                    codigo = 34;
                    break;
                case "HtmlViewer":
                    codigo = 35;
                    break;
                case "ChildToolBar":
                    codigo = 36;
                    break;
                case "ColorPicker":
                    codigo = 37;
                    break;
                case "Dashboard":
                    codigo = 38;
                    break;
                default:
                    codigo = 1;
                    break;
            }

            return codigo;
        }

        private void AddMultimediaControl(PublicationEntity entityParent, PublicationEntity entity, LayoutContainer page)
        {
            if (this.layoutDefinition == null || !this.layoutDefinition.EnableMedias)
                return;

            bool isRemoved = false;
            LayoutControlV2 lControl;
            string[] parentArrayEntities = (entityParent == null ? new string[] { } : entityParent.GetMediaKeys());
            String tableName, fieldName, mediaKey, bindingPath;

            foreach (string mPoint in entity.GetMediaKeys())
            {
                if (!mPoint.Contains("DOC_MULTIMIDIA") && !parentArrayEntities.Contains(mPoint))
                {
                    isRemoved = false;
                    tableName = ExtractTable(mPoint);
                    fieldName = ExtractKeys(mPoint);
                    mediaKey = tableName + "." + fieldName;

                    if (page.IsNull())
                        bindingPath = "DataElement.DataView." + fieldName;
                    else
                        bindingPath = page.BindingPath + "." + fieldName;

                    lControl = this.layoutDefinition.GetControlByBindingPath(bindingPath, true);
                    if (lControl == null && !page.IsNull())
                        lControl = this.layoutDefinition.GetContainerControlByName(page.Name, mediaKey);

                    if (lControl == null)
                    {
                        lControl = this.layoutDefinition.GetRemovedControlByBindingPath(bindingPath, true) as LayoutControlV2;
                        if (lControl == null && !page.IsNull())
                            lControl = (LayoutControlV2)this.layoutDefinition.GetRemovedContainerOrControlByName(page.Name, mediaKey);
                        isRemoved = (lControl != null);
                    }

                    if (lControl == null)
                    {
                        lControl = new LayoutControlV2() { Name = mediaKey, DataType = String.Empty, SourceViewName = entity.Name, DisplayName = "Media".Translate() + " (" + tableName + ")", ImageIndex = 4, ClassName = "MultimediaControl", ActionEvent = String.Empty, IsCustomized = false, IsDataField = false, IsVisible = false, AggregationFunction = "None", IsEditable = true, ConnectedAttribute = String.Empty, DataFormatString = String.Empty, Precision = String.Empty, Mask = String.Empty, MaskType = string.Empty };
                        if (page.IsNull())
                        {
                            LayoutContainer group = this.layoutDefinition.GetContainerByControlName(lControl.Name);
                            if (group == null)
                                group = this.layoutDefinition.Containers.FirstOrDefault();
                            if (group != null)
                                group.Controls.Add(lControl);
                        }
                        else
                            page.Controls.Add(lControl);
                    }
                    lControl.Sync = !isRemoved;
                    lControl.HasTemporaryKey = (entity.TemporaryKeyName == fieldName);
                    if (lControl.BindingPath.IsNullOrEmpty())
                        lControl.BindingPath = bindingPath;
                    this.layoutDefinition.AdjustSyncCopies(lControl);
                }
            }

        }

        private string ExtractKeys(string s)
        {
            return s.Right(":").Replace(":", string.Empty);
        }

        private string ExtractTable(string s)
        {
            return s.Left(":");
        }

        private void FillTreeChildren()
        {
            /* TODO: treeChildren.Nodes.Clear();
            FillTreeChildren(this.layoutDefinition.Pages, this.treeChildren.Nodes);
            treeChildren.ExpandAll();
            if (treeChildren.Nodes.Count > 0)
                treeChildren.SelectedNode = treeChildren.Nodes[0]; */

            FillTreeChildren(this.layoutDefinition.Containers, this.treeTopGroups.Nodes);
            treeTopGroups.ExpandAll();
            if (treeTopGroups.Nodes.Count > 0)
                treeTopGroups.SelectedNode = treeTopGroups.Nodes[0];
        }

        private void FillTreeChildren(List<LayoutContainer> pages, TreeNodeCollection parentCollection)
        {
            TreeNode newPageNode, newNode;
            foreach (LayoutContainer page in pages)
            {
                newPageNode = parentCollection.Add(page.Name, page.DisplayName, GetIconIndexFromClassName(page.ClassName), GetIconIndexFromClassName(page.ClassName));
                newPageNode.Tag = page;
                //newPageNode.NodeFont = new Font("Arial Black", 8, FontStyle.Bold);

                foreach (var element in page.Controls)
                {
                    if (element is LayoutControlV2)
                    {
                        element.ImageIndex = GetIconIndexFromClassName(element.ClassName);
                        newNode = newPageNode.Nodes.Add(element.Name, element.DisplayName, GetIconIndexFromClassName(element.ClassName), GetIconIndexFromClassName(element.ClassName));
                        newNode.Tag = element;
                    }
                }
                FillTreeChildren(page.Controls.Where(e => e is LayoutContainer).Select(p => (LayoutContainer)p).ToList<LayoutContainer>(), newPageNode.Nodes);
            }
        }

        //private void UpdateDataLayout(TreeView tree)
        //{
        //    this.layoutDefinition.Containers.Clear();
        //    this.layoutDefinition.Containers.Clear();
        //    foreach (TreeNode node in this.treeTopGroups.Nodes)
        //    {
        //        if (node.Tag is LayoutContainer)
        //        {
        //            ((LayoutContainer)node.Tag).Controls.Clear();
        //            this.layoutDefinition.Containers.Add(((LayoutContainer)node.Tag));

        //            foreach (TreeNode fieldNode in node.Nodes)
        //            {
        //                if (fieldNode.Tag is LayoutControlV2)
        //                {
        //                    ((LayoutContainer)node.Tag).Controls.Add(((LayoutControlV2)fieldNode.Tag));
        //                    //((LayoutControlV2)fieldNode.Tag).PageName = String.Empty;
        //                }
        //            }
        //        }
        //        else if (node.Tag is LayoutContainer)
        //        {
        //            this.layoutDefinition.Containers.Add(((LayoutContainer)node.Tag));
        //            ((LayoutContainer)node.Tag).ParentName = String.Empty;
        //            UpdateDataLayout(((LayoutContainer)node.Tag), node.Nodes);
        //        }
        //    }
        //}


        private void UpdateDataLayout(TreeView tree)
        {
            this.layoutDefinition.Containers.Clear();
            foreach (TreeNode node in tree.Nodes)
                if (node.Tag is LayoutContainer)
                {
                    this.layoutDefinition.Containers.Add((LayoutContainer)node.Tag);
                    GetNodesControls(node);
                }
        }

        private void GetNodesControls(TreeNode node)
        {
            ((LayoutContainer)node.Tag).Controls.Clear();

            foreach (TreeNode childNode in node.Nodes)
            {
                ((LayoutContainer)node.Tag).Controls.Add(((LayoutElement)childNode.Tag));
                ((LayoutElement)childNode.Tag).ParentName = ((LayoutContainer)node.Tag).Name;

                if (childNode.Tag is LayoutContainer)
                    GetNodesControls(childNode);
            }
        }


        private void UpdateDataLayout(LayoutContainer page, TreeNodeCollection nodes)
        {
            page.Controls.Clear();
            foreach (TreeNode fieldNode in nodes)
            {
                if (fieldNode.Tag is LayoutControlV2)
                {
                    page.Controls.Add(((LayoutControlV2)fieldNode.Tag));
                    (((LayoutControlV2)fieldNode.Tag)).ParentName = page.Name;
                }
                else if (fieldNode.Tag is LayoutContainer)
                {
                    page.Controls.Add(((LayoutContainer)fieldNode.Tag));
                    (((LayoutContainer)fieldNode.Tag)).ParentName = page.Name;
                    UpdateDataLayout(((LayoutContainer)fieldNode.Tag), fieldNode.Nodes);
                }
            }
        }


        private void tree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            tree_AfterSelect(sender, new TreeViewEventArgs(e.Node));
        }

        private void DeSelectAllNodes(List<TreeNode> listSelectedNodes)
        {
            foreach (var item in listSelectedNodes)
                NodeSelectionApperance(item, false);
        }

        private void NodeSelectionApperance(TreeNode treeNode, bool markSelected)
        {
            if (!(treeNode.Tag != null && treeNode.Tag is LayoutElement && ((LayoutElement)treeNode.Tag).IsDerived))
            {
                Color backgrnd;
                Color foregrnd;

                if (markSelected == true)
                {
                    backgrnd = SystemColors.Highlight;
                    foregrnd = SystemColors.HighlightText;
                }
                else
                {
                    backgrnd = SystemColors.Window;
                    foregrnd = SystemColors.ControlText;
                }

                treeNode.ForeColor = foregrnd;
                treeNode.BackColor = backgrnd;
            }
        }

        private void tree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            selectedTree = sender as TreeView;

            if (selectedTree != null && !selectedTree.SelectedNode.IsNull() && selectedTree.SelectedNode.Tag is LayoutElement)
            {
                PrepareMenu(selectedTree.SelectedNode);
                this.tabInformations.TabPages.Clear();

                //Adjust common tabs
                string className = ((LayoutElement)selectedTree.SelectedNode.Tag).ClassName;
                this.ckIsVisible.Checked = ((LayoutElement)selectedTree.SelectedNode.Tag).IsVisible;
                this.tabGroupInformations.IsAccessible = (selectedTree.SelectedNode.Tag is LayoutContainer && className != "WizardControl");
                this.tabWizard.IsAccessible = (selectedTree.SelectedNode.Tag is LayoutContainer && className == "WizardControl");
                this.tabFieldInformations.IsAccessible = (selectedTree.SelectedNode.Tag is LayoutControlV2);
                this.tabChart.IsAccessible = (selectedTree.SelectedNode.Tag is LayoutControlV2);
                this.tabMaskedTextBox.IsAccessible = (selectedTree.SelectedNode.Tag is LayoutControlV2);

                //Adjust special tabs
                this.tabChart.IsAccessible = (className == "Chart");
                this.telerikUIGauge1.Visible = (className == "Gauge");
                this.tabMaskedTextBox.IsAccessible = (className == "MaskedTextBox");
                this.tabPivotGrid.IsAccessible = (className.InList("FlatPivotGrid", "OlapPivotGrid"));
                this.tabPivotChart.IsAccessible = (className.InList("PivotChart", "PivotDrillDownChart"));
                this.tabDashboard.Visible = (selectedTree.SelectedNode.Tag is LayoutControlV2 && className == "Dashboard");
                this.tabDashboard.IsAccessible = (selectedTree.SelectedNode.Tag is LayoutControlV2 && className == "Dashboard");
                this.groupPivotCubeName.Visible = (className == "OlapPivotGrid");
                this.ckIsPivotExpanded.Visible = (className == "FlatPivotGrid");
                this.ckIsPivotReadOnly.Visible = (className == "FlatPivotGrid");
                this.ckIsLinqSelectionControl.Visible = (className == "FlatPivotGrid");
                this.ckIsTotalVisible.Visible = (className == "FlatPivotGrid");
                this.ckParentInFrontForColumns.Visible = (className.InList("FlatPivotGrid", "OlapPivotGrid"));
                this.ckParentInFrontForRows.Visible = (className.InList("FlatPivotGrid", "OlapPivotGrid"));
                this.grpPivotChartLayout.Visible = (className == "PivotChart");
                this.cmbPivotChartType.Visible = (className == "PivotChart");
                this.cmbOlapPivotChartType.Visible = (className == "PivotDrillDownChart");
                this.chkRemoveToolbarGrid.Visible = (className.InList("DataGrid", "TreeListView"));
                this.chkEditorTemplateGrid.Visible = (className.InList("DataGrid", "TreeListView"));
                this.chkEditorOnlyTemplate.Visible = (className.InList("DataGrid", "TreeListView"));
                this.ckHasGroupBy.Visible = (className.InList("DataGrid", "TreeListView"));
                this.lblPageSize.Visible = this.numPageSize.Visible = (className == "DataGrid");
                this.ckEnableMultiSelection.Visible = (className == "DataGrid");
                this.ckIsLinqSelectionControl.Visible = (className.InList("DataGrid", "TreeListView", "FlatPivotGrid"));
                this.txGroupByColumns.Visible = (className.InList("DataGrid", "TreeListView"));
                this.lbGroupByColumns.Visible = (className.InList("DataGrid", "TreeListView"));
                this.lbGroupByColumnsHelp.Visible = (className.InList("DataGrid", "TreeListView"));
                this.grpExternalUI.Visible = (className == "ExternalUI");
                this.grdGridDataOptions.Visible = (className == "DataGrid");
                this.grEntityNavigator.Visible = className.Equals("ChildToolBar");


                this.cmbControlWidth.Visible = className != "MultimediaControl";
                this.cmbMediaWidth.Visible = className == "MultimediaControl";

                LayoutElement parent = null;
                if (selectedTree.SelectedNode.Parent != null)
                {
                    parent = selectedTree.SelectedNode.Parent.Tag as LayoutElement;
                }

                //check treeList
                grpTreelistKeys.Visible = (this.tabGroupInformations.IsAccessible && (selectedTree.SelectedNode.Tag is LayoutContainer) && ((LayoutContainer)selectedTree.SelectedNode.Tag).ClassName == "TreeListView");

                //Adjust Tab Visibility
                AdjustTabVisibility(((LayoutElement)selectedTree.SelectedNode.Tag).ClassName);

                bool isDataField = (selectedTree.SelectedNode.Tag is LayoutControlV2) && IsDataField(((LayoutControlV2)selectedTree.SelectedNode.Tag).ClassName);
                this.grAggregationFunction.Visible = isDataField;
                this.grpDomainFilterValues.Visible = isDataField && ((LayoutControlV2)selectedTree.SelectedNode.Tag).ClassName == "ComboBox";
                this.cmbFieldVisibleGridEditor.Visible = (selectedTree.SelectedNode.Tag is LayoutControlV2);
                this.grConnectedAttribute.Visible = isDataField || this.tabDashboard.IsAccessible;
                this.ckIsEditable.Visible = (selectedTree.SelectedNode.Tag is LayoutControlV2) && !((LayoutControlV2)selectedTree.SelectedNode.Tag).IsCustomized;
                this.ckIsEditableOnInsert.Visible = (selectedTree.SelectedNode.Tag is LayoutControlV2) && !((LayoutControlV2)selectedTree.SelectedNode.Tag).IsCustomized;
                this.ckIsPassword.Visible = (selectedTree.SelectedNode.Tag is LayoutControlV2) && ((LayoutControlV2)selectedTree.SelectedNode.Tag).ClassName == "TextBox";
                this.ckAllowEmpty.Visible = className.Equals("RadioButtonGroup");
                this.ckHasFilterRange.Visible = className.InList("NumericTextBox", "DateTimeTextBox");
                this.chkAllowNegativeValue.Visible = className == "NumericTextBox";
                this.ckDisplayRangeDate.Visible = className.InList("NumericTextBox", "DateTimeTextBox");
                this.ckIsExpanded.Visible = (className == "Expander");
                this.ckIsExpanded.Checked = (className == "Expander") && (selectedTree.SelectedNode.Tag as LayoutContainer).IsExpanded;
                this.lblPrecision.Visible = this.txtPrecision.Visible = isDataField;
                this.grDataFormat.Visible = className.InList("NumericTextBox", "DateTimeTextBox", "Dashboard");
                if (this.lblStringFormatHelp.Visible)
                    this.lblStringFormatHelp.Text = (className == "NumericTextBox" ? "Ex: N2 or C2 or #,##0.00 CR;#,##0.00 DB;#,##0.00" : "Ex: d (22/9/2014), D (Segunda-feira, 22 de Setembro de 2014), t (13:49), T (13:49:53), g (22/9/2014 13:49), G (22/9/2014 13:49:53) or dd/MM/yyyy hh:mm:ss");

                if (this.tabGroupInformations.IsAccessible)
                {
                    #region tabGroupInformations
                    var layoutContainer = (LayoutContainer)selectedTree.SelectedNode.Tag;
                    this.txtGroupName.Visible = true;
                    this.lblGName.Visible = true;
                    this.txtGroupName.Text = layoutContainer.DefinedUserName;
                    this.textPrefixG.Text = layoutContainer.GetPrefix();
                    this.lblGroupInternalName.Text = selectedTree.SelectedNode.Name;
                    this.txGroupDisplayName.Text = selectedTree.SelectedNode.Text;
                    this.numGroupColumns.Value = layoutContainer.ColumnCount;
                    this.numGroupColumnSpan.Value = layoutContainer.ColumnSpan;
                    this.numGroupHeight.Value = layoutContainer.Height;
                    this.txtIdNameTreeList.Text = layoutContainer.IdNameTreeListView;
                    this.txtIdParentNameTreeList.Text = layoutContainer.IdParentNameTreeListView;

                    if (className == "PivotChart")
                        this.cmbPivotChartType.SelectedItem = layoutContainer.PivotChartType;
                    if (className == "PivotDrillDownChart")
                        this.cmbOlapPivotChartType.SelectedItem = layoutContainer.PivotChartType;

                    this.txPivotGridName.Text = layoutContainer.PivotGridName;
                    this.txChartMeasures.Text = layoutContainer.ChartMeasures;
                    this.txChartAvailableMeasures.Text = layoutContainer.ChartAvailableMeasures;
                    this.txChartDimensions.Text = layoutContainer.ChartDimensions;
                    this.cmbOlapAxisSource.Text = layoutContainer.OlapAxisSource;
                    this.ckIsPivotExpanded.Checked = layoutContainer.IsPivotExpanded;
                    this.ckIsPivotReadOnly.Checked = layoutContainer.IsPivotReadOnly;
                    this.ckIsLinqSelectionControl.Checked = layoutContainer.IsLinqSelectionControl;
                    this.ckIsTotalVisible.Checked = layoutContainer.IsTotalVisible;
                    this.ckParentInFrontForColumns.Checked = layoutContainer.ParentInFrontForColumns;
                    this.ckParentInFrontForRows.Checked = layoutContainer.ParentInFrontForRows;

                    this.txGroupByColumns.Text = layoutContainer.GroupByColumns;
                    if (this.chkRemoveToolbarGrid.Visible)
                        this.chkRemoveToolbarGrid.Checked = layoutContainer.RemoveDataToolbar;

                    if (this.ckHasGroupBy.Visible)
                        this.ckHasGroupBy.Checked = layoutContainer.HasGroupBy;

                    if (this.numPageSize.Visible)
                        this.numPageSize.Value = layoutContainer.PageSize;
                    if (this.ckEnableMultiSelection.Visible)
                        this.ckEnableMultiSelection.Checked = layoutContainer.EnableMultiSelection;

                    this.rbLabelPositionTop.Visible = this.rbLabelPositionLeft.Visible = this.lblLabelPosition.Visible = !layoutContainer.ClassName.InList("TabControl", "WizardControl", "FlatPivotGrid", "OlapPivotGrid", "PivotChart", "PivotDrillDownChart");
                    this.rbLabelPositionTop.Checked = layoutContainer.LabelPosition == LabelPosition.Top;
                    this.rbLabelPositionLeft.Checked = layoutContainer.LabelPosition == LabelPosition.Left;

                    this.cboGroupStyle.Visible = this.lblGroupStyle.Visible = layoutContainer.ClassName.InList("TabControl", "Expander", "GroupBox");
                    if (this.cboGroupStyle.Visible)
                        this.cboGroupStyle.SelectedItem = layoutContainer.Style.ToString();

                    this.cboGridWidth.Visible = this.lblGridWidth.Visible = layoutContainer.ClassName == "DataGrid";
                    if (this.cboGridWidth.Visible)
                        this.cboGridWidth.SelectedItem = layoutContainer.GridWidth.ToString();

                    this.cboGridHeight.Visible = this.lblGridHeight.Visible = layoutContainer.ClassName == "DataGrid";
                    if (this.cboGridHeight.Visible)
                        this.cboGridHeight.SelectedItem = layoutContainer.GridHeight.ToString();


                    if (this.chkEditorTemplateGrid.Visible)
                        this.chkEditorTemplateGrid.Checked = layoutContainer.IsTemplate;

                    if (this.chkEditorOnlyTemplate.Visible)
                        this.chkEditorOnlyTemplate.Checked = layoutContainer.EditionOnlyTemplate;

                    if (this.grpExternalUI.Visible)
                    {
                        this.txUserInterfaceName.Text = ((LayoutContainer)selectedTree.SelectedNode.Tag).UserInterfaceName;
                        this.txParentFieldsRelation.Text = ((LayoutContainer)selectedTree.SelectedNode.Tag).ParentFieldsRelation;
                        this.txDetailFieldsRelation.Text = ((LayoutContainer)selectedTree.SelectedNode.Tag).DetailFieldsRelation;
                        this.txParentSelectorDataName.Text = ((LayoutContainer)selectedTree.SelectedNode.Tag).ParentSelectorDataName;
                        this.chkRemoveDataToolbar.Checked = ((LayoutContainer)selectedTree.SelectedNode.Tag).RemoveDataToolbar;
                        this.chkShareParentBO.Checked = ((LayoutContainer)selectedTree.SelectedNode.Tag).ShareParentBO;
                        this.chkNoSearch.Checked = ((LayoutContainer)selectedTree.SelectedNode.Tag).NoSearch;
                        this.chkUseFilterFromParent.Checked = ((LayoutContainer)selectedTree.SelectedNode.Tag).UseFilterFromParent;
                        this.chkApplyFilterToParent.Checked = ((LayoutContainer)selectedTree.SelectedNode.Tag).ApplyFilterToParent;
                        this.ckGrCanClear.Checked = ((LayoutContainer)selectedTree.SelectedNode.Tag).CanClear;
                        this.ckGrCanSearch.Checked = ((LayoutContainer)selectedTree.SelectedNode.Tag).CanSearch;
                        this.ckGrCanAddNew.Checked = ((LayoutContainer)selectedTree.SelectedNode.Tag).CanAddNew;
                        this.ckGrCanEdit.Checked = ((LayoutContainer)selectedTree.SelectedNode.Tag).CanEdit;
                        this.ckGrCanDelete.Checked = ((LayoutContainer)selectedTree.SelectedNode.Tag).CanDelete;
                        this.ckGrCanCustomSearch.Checked = ((LayoutContainer)selectedTree.SelectedNode.Tag).CanCustomSearch;
                        this.ckGrCanPrint.Checked = ((LayoutContainer)selectedTree.SelectedNode.Tag).CanPrint;
                        this.ckGrCanExport.Checked = ((LayoutContainer)selectedTree.SelectedNode.Tag).CanExport;
                        this.ckGrCanLayout.Checked = ((LayoutContainer)selectedTree.SelectedNode.Tag).CanLayout;
                        this.ckGrCanNavigate.Checked = ((LayoutContainer)selectedTree.SelectedNode.Tag).CanNavigate;
                    }

                    if (this.grdGridDataOptions.Visible)
                    {
                        this.ckDgCanAddNew.Checked = ((LayoutContainer)selectedTree.SelectedNode.Tag).CanAddNew;
                        this.ckDgCanEdit.Checked = ((LayoutContainer)selectedTree.SelectedNode.Tag).CanEdit;
                        this.ckDgCanDelete.Checked = ((LayoutContainer)selectedTree.SelectedNode.Tag).CanDelete;
                        this.ckDgCanExportGrid.Checked = ((LayoutContainer)selectedTree.SelectedNode.Tag).CanExportGrid;
                    }

                    #endregion
                }


                if (this.tabFieldInformations.IsAccessible)
                {
                    #region tabFieldInformations

                    //Measure control
                    this.grpMeasures.Visible = (selectedTree.SelectedNode.Parent != null && (selectedTree.SelectedNode.Parent.Tag is LayoutContainer && ((LayoutContainer)selectedTree.SelectedNode.Parent.Tag).ClassName.InList("OlapPivotGrid", "FlatPivotGrid")));

                    this.ckAllowMultiSelectionInSearch.Visible = className == cLookUpTextBox;
                    this.ckValidateOnClearState.Visible = className == cLookUpTextBox;

                    var layoutElement = selectedTree.SelectedNode.Tag as LayoutControlV2;

                    this.txDomainFilterValues.Text = ((LayoutControlV2)selectedTree.SelectedNode.Tag).DomainFilterValues;
                    this.lblInternalName.Text = (layoutElement.DefinedUserName.IsNullOrEmpty() ? selectedTree.SelectedNode.Name : (layoutElement.DefinedUserName));
                    this.textPrefixF.Text = layoutElement.GetPrefix();
                    this.txDataGridOrder.Text = layoutElement.DataGridOrder;
                    this.txtRange.Text = layoutElement.Range;
                    this.txFieldDisplayName.Text = selectedTree.SelectedNode.Text;
                    this.txMeasureGroup.Text = layoutElement.Group;
                    this.txMeasureFormula.Text = layoutElement.MeasureFormula;
                    this.cmbDisplayControl.SelectedItem = layoutElement.ClassName;
                    this.ckIsEditable.Checked = layoutElement.IsEditable;
                    this.ckIsEditableOnInsert.Checked = layoutElement.EditableOnInsert;
                    this.cmbFieldVisibleGridEditor.SelectedItem = layoutElement.FieldVisibleGrid.ToString();
                    this.ckIsPassword.Checked = layoutElement.IsPassword;
                    this.ckAllowEmpty.Checked = layoutElement.AllowEmptyOption;
                    this.ckHasFilterRange.Checked = layoutElement.HasFilterRange;
                    this.ckDisplayRangeDate.Checked = layoutElement.DisplayRangeDate;
                    this.chkAllowNegativeValue.Checked = layoutElement.AllowNegativeValue;
                    this.ckAlwaysEditable.Checked = layoutElement.AlwaysEditable;
                    this.ckIsMeasure.Checked = layoutElement.IsMeasure;
                    this.cmbControlWidth.SelectedItem = layoutElement.ControlWidth.ToString();
                    this.cmbMediaWidth.SelectedItem = layoutElement.MediaWidth.ToString();
                    this.txConnectedAttribute.Text = layoutElement.ConnectedAttribute;
                    this.cmbAggregationFunction.SelectedItem = layoutElement.AggregationFunction;
                    this.txtAggregationDescription.Text = layoutElement.AggregationDescription;
                    this.txtTooltip.Text = layoutElement.ToolTip;
                    this.txtSource.Text = layoutElement.BindingPath;
                    this.txtPrecision.Text = layoutElement.Precision;
                    this.txtStringFormat.Text = layoutElement.DataFormatString;

                    this.ckValidateOnClearState.Checked = layoutElement.ValidateOnClearState;
                    this.ckAllowMultiSelectionInSearch.Checked = layoutElement.AllowMultiSelectionInSearch;


                    this.numColumnSpanField.Value = layoutElement.ColumnSpan;
                    this.lblTotalLines.Visible = this.numTotalLines.Visible = className.Equals("EditBox");
                    this.txtRange.Visible = this.lblRange.Visible = this.lblRangeSample.Visible = className.Equals("NumericTextBox");

                    this.numTotalLines.Value = layoutElement.TotalLines;

                    this.txtAggregationDescription.Enabled = !layoutElement.AggregationFunction.Equals("None");

                    if (this.cbEntityNavigator.Visible)
                    {
                        var currentEntityAdapter = currentLayout.GetEntityAdapter();
                        if (currentEntityAdapter != null)
                        {
                            cbEntityNavigator.Items.Clear();
                            LoadRelatedChartEntities(currentEntityAdapter.Details, cbEntityNavigator);
                            for (int i = 0; i < cbEntityNavigator.Items.Count; i++)
                                if (((String)cbEntityNavigator.Items[i]) == layoutElement.BindingPath)
                                {
                                    cbEntityNavigator.SelectedIndex = i;
                                    break;
                                }
                        }
                    }

                    //font
                    FieldFontControl.Style.SelectedItem = ((LayoutElement)selectedTree.SelectedNode.Tag).FontForegroundStyle.ToString();
                    FieldFontControl.Bold.Checked = ((LayoutElement)selectedTree.SelectedNode.Tag).FontBold;
                    FieldFontControl.Highlight.Checked = ((LayoutElement)selectedTree.SelectedNode.Tag).FontBackground == FontBackground.Highlight;


                    #endregion
                }
                else if (this.tabPivotGrid.IsAccessible)
                {
                    this.txPivotColumns.Text = ((LayoutContainer)selectedTree.SelectedNode.Tag).PivotColumns;
                    this.txPivotRows.Text = ((LayoutContainer)selectedTree.SelectedNode.Tag).PivotRows;
                    this.txPivotMeasures.Text = ((LayoutContainer)selectedTree.SelectedNode.Tag).PivotMeasures;
                    this.txPivotCubeName.Text = ((LayoutContainer)selectedTree.SelectedNode.Tag).PivotCube;
                    this.cmbPivotMeasuresLocation.SelectedItem = ((LayoutContainer)selectedTree.SelectedNode.Tag).PivotMeasuresLocation;
                }


                if (this.tabMaskedTextBox.IsAccessible)
                {
                    #region tabMaskedTextBox
                    LayoutControlV2 lc = ((LayoutControlV2)selectedTree.SelectedNode.Tag);

                    txtMask.Text = lc.Mask;
                    txtCulture.Text = lc.MaskCulture;
                    #endregion
                }

                if (this.tabChart.IsAccessible)
                {
                    #region tabChart
                    LayoutControlV2 lc = ((LayoutControlV2)selectedTree.SelectedNode.Tag);

                    cboEntity.Items.Clear();
                    cboEntity.Items.Add("DataElement.DataView");
                    LoadRelatedChartEntities(currentLayout.GetEntityAdapter().Details, cboEntity);
                    for (int i = 0; i < cboEntity.Items.Count; i++)
                        if (((String)cboEntity.Items[i]) == lc.BindingPath)
                        {
                            cboEntity.SelectedIndex = i;
                            break;
                        }

                    //HtmlChart
                    LoadAdapterProperties(lc.BindingPath);
                    //igniteUIChart1.ChartLayout = selectedTree.SelectedNode.Tag as LayoutElement;
                    telerikUIChart1.ChartLayout = selectedTree.SelectedNode.Tag as LayoutElement;
                    #endregion
                }

                if (((LayoutElement)selectedTree.SelectedNode.Tag).ClassName == "Gauge")
                {
                    telerikUIGauge1.GaugeLayout = selectedTree.SelectedNode.Tag as LayoutElement;
                }

                //WizardControl
                if (this.tabWizard.IsAccessible)
                {
                    #region tabWizard
                    this.lblWizardInternalName.Text = selectedTree.SelectedNode.Name;
                    this.txtWizardUserName.Text = ((LayoutContainer)selectedTree.SelectedNode.Tag).DefinedUserName;
                    this.txtWizardDisplayName.Text = ((LayoutContainer)selectedTree.SelectedNode.Tag).DisplayName;
                    this.txtWizardSideBarDisplayName.Text = ((LayoutContainer)selectedTree.SelectedNode.Tag).SideBarDescription;
                    #endregion
                }
                //dashboardControl
                if (this.tabDashboard.IsAccessible)
                {
                    #region tabDashboard
                    setDashboardColor(((LayoutControlV2)selectedTree.SelectedNode.Tag).DashboardBackgroundColorClassName);
                    this.dashboardIconFA.Text = ((LayoutControlV2)selectedTree.SelectedNode.Tag).DashboardIconFAName;
                    this.DashboardSizeWidth.SelectedItem = ((LayoutControlV2)selectedTree.SelectedNode.Tag).DashboardWidth;
                    #endregion
                }
            }
        }

        private PublicationEntity GetEntityAdapterByName(string entityName, PublicationEntity entity = null)
        {
            PublicationEntity result = null;

            if (entity.IsNull())
                entity = this.currentLayout.GetEntityAdapter();

            if (!entity.IsNull())
            {
                if (entity.Name == entityName)
                    result = entity;
                else
                {
                    foreach (PublicationEntity detail in entity.Details)
                    {
                        result = GetEntityAdapterByName(entityName, detail);
                        if (!result.IsNull())
                            break;
                    }
                }
            }

            return result;
        }

        private void LoadRelatedChartEntities(List<PublicationEntity> linkedElementCollection, ComboBox cbo)
        {
            foreach (var item in linkedElementCollection)
            {
                cbo.Items.Add(this.GetBindingPath(item));

                if (item.Details.Count > 0)
                    LoadRelatedChartEntities(item.Details, cbo);
            }
        }

        private void LoadRelatedSpecializedEntities(List<PublicationEntity> linkedElementCollection, ComboBox cbo)
        {
            foreach (var item in linkedElementCollection)
            {
                cbo.Items.Add(item.Name);
                if (item.Details.Count > 0)
                    LoadRelatedSpecializedEntities(item.Details, cbo);
            }
        }

        private void btAddNewGroup_Click(object sender, EventArgs e)
        {
            LayoutContainer lGroup = new LayoutContainer() { Name = "Expander_" + Guid.NewGuid().ToString().Replace("-", String.Empty), DisplayName = "New Group", ImageIndex = 0, ColumnCount = 2, ClassName = "Expander" };
            this.treeTopGroups.SelectedNode = this.treeTopGroups.Nodes.Add(lGroup.Name, lGroup.DisplayName, lGroup.ImageIndex, lGroup.ImageIndex);
            this.treeTopGroups.SelectedNode.Tag = lGroup;
            this.treeTopGroups.Select();
            this.treeTopGroups.Invalidate();
        }

        private void txName_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox && !selectedTree.IsNull() && !selectedTree.SelectedNode.IsNull())
            {
                selectedTree.SelectedNode.Text = ((TextBox)sender).Text;
                if (selectedTree.SelectedNode.Tag is LayoutContainer)
                    ((LayoutContainer)selectedTree.SelectedNode.Tag).DisplayName = ((TextBox)sender).Text;
                else if (selectedTree.SelectedNode.Tag is LayoutContainer)
                    ((LayoutContainer)selectedTree.SelectedNode.Tag).DisplayName = ((TextBox)sender).Text;
                else if (selectedTree.SelectedNode.Tag is LayoutControlV2)
                    ((LayoutControlV2)selectedTree.SelectedNode.Tag).DisplayName = ((TextBox)sender).Text;

                this.selectedTree.Invalidate();
            }
        }


        private void numGroupColumns_ValueChanged(object sender, EventArgs e)
        {
            if (sender is NumericUpDown && !this.treeTopGroups.SelectedNode.IsNull())
            {
                if (this.treeTopGroups.SelectedNode.Tag is LayoutContainer)
                    ((LayoutContainer)this.treeTopGroups.SelectedNode.Tag).ColumnCount = (int)((NumericUpDown)sender).Value;
                if (this.treeTopGroups.SelectedNode.Tag is LayoutContainer)
                    ((LayoutContainer)this.treeTopGroups.SelectedNode.Tag).ColumnCount = (int)((NumericUpDown)sender).Value;
            }
        }

        private void AddRemovedControlNode(LayoutControlV2 element)
        {
            TreeNode newNode = this.treeRemovedItems.Nodes[0].Nodes.Add(element.Name, element.DisplayName, GetIconIndexFromClassName(element.ClassName), GetIconIndexFromClassName(element.ClassName));
            newNode.Tag = element;
        }

        private void AddRemovedPageNode(LayoutContainer pageElement)
        {
            TreeNode newNode;
            TreeNode newPageNode = this.treeRemovedItems.Nodes[1].Nodes.Add(pageElement.Name, pageElement.DisplayName, GetIconIndexFromClassName(pageElement.ClassName), GetIconIndexFromClassName(pageElement.ClassName));
            newPageNode.Tag = pageElement;
            //newPageNode.NodeFont = new Font("Arial Black", 8, FontStyle.Bold);

            //////Generate Sub Tree
            foreach (var element in pageElement.Controls)
            {
                if (element is LayoutControlV2)
                {
                    newNode = newPageNode.Nodes.Add(element.Name, element.DisplayName, element.ImageIndex, element.ImageIndex);
                    newNode.Tag = element;
                }
            }
            FillTreeChildren(pageElement.Controls.Where(e => e is LayoutContainer).Select(p => (LayoutContainer)p).ToList<LayoutContainer>(), newPageNode.Nodes);
        }

        private void btDeleteElement_Click(object sender, EventArgs e)
        {
            if (!this.selectedTree.IsNull())
            {
                if (this.selectedTree.SelectedNode == null)
                {
                    MessageBox.Show("There is no selected element!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                if (MessageBox.Show("Do you really want to delete the selected item?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    PublicationEntity entity = this.currentLayout.GetEntityAdapter();
                    if (listSelectedNodes.Count == 0)
                        listSelectedNodes.Add(this.selectedTree.SelectedNode);

                    foreach (var item in listSelectedNodes)
                    {
                        if (item.Tag is LayoutControlV2)
                        {
                            LayoutControlV2 element = item.Tag as LayoutControlV2;
                            if (!entity.IsNull() && !element.IsCustomized && element.Name.ToString().IndexOf("Copy_") == -1)
                            {
                                this.layoutDefinition.MoveToRemovedControls(element);
                                AddRemovedControlNode(element);
                                this.treeRemovedItems.Nodes[0].ExpandAll();
                                this.treeRemovedItems.Invalidate();
                            }
                            else
                            {
                                this.layoutDefinition.RemoveControl(element);
                            }
                        }
                        else if (item.Tag is LayoutContainer)
                        {
                            LayoutContainer element = item.Tag as LayoutContainer;
                            if (!entity.IsNull())
                            {
                                LayoutContainer pageElement = item.Tag as LayoutContainer;
                                this.layoutDefinition.MoveToRemovedContainer(pageElement);
                                AddRemovedPageNode(pageElement);
                                this.treeRemovedItems.Nodes[1].ExpandAll();
                                this.treeRemovedItems.Invalidate();
                            }
                            else
                                this.layoutDefinition.RemoveContainerByName(element.Name);
                        }
                        this.selectedTree.Nodes.Remove(item);
                        //this.selectedTree.Nodes.Remove(this.selectedTree.SelectedNode);
                        this.selectedTree.Invalidate();

                    }
                    listSelectedNodes.Clear();
                }
            }

        }

        private void btRestoreItem_Click(object sender, EventArgs e)
        {
            if (this.treeRemovedItems.SelectedNode.IsNullOrEmpty())
            {
                MessageBox.Show("There is no selected element!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!(this.treeRemovedItems.SelectedNode.Parent == this.treeRemovedItems.Nodes[0] || this.treeRemovedItems.SelectedNode.Parent == this.treeRemovedItems.Nodes[1]))
            {
                MessageBox.Show("The selected element is not valid to restore. Try restore the elements on second level.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (this.treeRemovedItems.SelectedNode.Tag is LayoutControlV2)
            {
                LayoutControlV2 element = this.treeRemovedItems.SelectedNode.Tag as LayoutControlV2;
                if (this.layoutDefinition.RestoreLayoutElement(element))
                {
                    TreeNodeCollection collection = null;
                    if (!element.ParentName.IsNullOrEmpty())
                    {
                        //TODO: TreeNode parentNode = GetNode(this.treeChildren, element.PageName);
                        TreeNode parentNode = GetNode(this.treeTopGroups, element.ParentName);
                        if (!parentNode.IsNull())
                            collection = parentNode.Nodes;
                    }

                    if (collection.IsNull())
                        collection = this.treeTopGroups.Nodes[0].Nodes;

                    element.ImageIndex = GetIconIndexFromClassName(element.ClassName);
                    TreeNode newNode = collection.Add(element.Name, element.DisplayName, GetIconIndexFromClassName(element.ClassName), GetIconIndexFromClassName(element.ClassName));
                    newNode.Tag = element;
                    this.treeTopGroups.Invalidate();
                    this.treeRemovedItems.Nodes.Remove(this.treeRemovedItems.SelectedNode);
                    this.treeTopGroups.Nodes[0].ExpandAll();
                    this.treeRemovedItems.Invalidate();
                }
                else
                    MessageBox.Show("This element is not valid to restore. Try restore by correct sequence.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }
            else if (this.treeRemovedItems.SelectedNode.Tag is LayoutContainer)
            {
                LayoutContainer pageElement = this.treeRemovedItems.SelectedNode.Tag as LayoutContainer;
                if (this.layoutDefinition.RestoreLayoutElement(pageElement))
                {
                    TreeNodeCollection collection = null;
                    if (!pageElement.ParentName.IsNullOrEmpty())
                    {
                        TreeNode parentNode = GetNode(this.treeTopGroups, pageElement.ParentName);
                        if (!parentNode.IsNull())
                            collection = parentNode.Nodes;
                    }

                    if (collection.IsNull())
                        collection = this.treeTopGroups.Nodes;

                    pageElement.ImageIndex = GetIconIndexFromClassName(pageElement.ClassName);
                    TreeNode newNode, newPageNode = collection.Add(pageElement.Name, pageElement.DisplayName, GetIconIndexFromClassName(pageElement.ClassName), GetIconIndexFromClassName(pageElement.ClassName));
                    newPageNode.Tag = pageElement;
                    //newPageNode.NodeFont = new Font("Arial Black", 8, FontStyle.Bold);

                    //Generate Sub Tree
                    foreach (var element in pageElement.Controls)
                    {
                        if (element is LayoutControlV2)
                        {
                            element.ImageIndex = GetIconIndexFromClassName(element.ClassName);
                            newNode = newPageNode.Nodes.Add(element.Name, element.DisplayName, GetIconIndexFromClassName(element.ClassName), GetIconIndexFromClassName(element.ClassName));
                            newNode.Tag = element;
                        }
                    }
                    FillTreeChildren(pageElement.Controls.Where(z => z is LayoutContainer).Select(p => (LayoutContainer)p).ToList<LayoutContainer>(), newPageNode.Nodes);

                    this.treeTopGroups.ExpandAll();
                    this.treeTopGroups.Invalidate();
                    this.treeRemovedItems.Nodes.Remove(this.treeRemovedItems.SelectedNode);
                    this.treeRemovedItems.Invalidate();
                }
                else
                    MessageBox.Show("This element is not valid to restore. Try restore by correct sequence.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }

            MessageBox.Show("This element is not valid to the restore process!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private TreeNode GetNode(TreeView tree, string nodeName)
        {
            TreeNode result = null;
            Action<TreeNode> treeFinder = null;

            treeFinder = (e) =>
                {
                    if (e.Name == nodeName)
                        result = e;
                    else
                    {
                        foreach (TreeNode node in e.Nodes)
                        {
                            treeFinder(node);
                            if (!result.IsNull())
                                break;
                        }
                    }
                };

            foreach (TreeNode node in tree.Nodes)
            {
                treeFinder(node);
                if (!result.IsNull())
                    break;
            }

            return result;
        }


        private void cmbDisplayControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is ComboBox && !selectedTree.IsNull() && !selectedTree.SelectedNode.IsNull())
            {
                if (sender.Equals(this.cmbDisplayControl) && !this.cmbDisplayControl.SelectedItem.IsNull())
                {
                    if (selectedTree.SelectedNode.Tag is LayoutControlV2)
                    {
                        String strClassName = ((ComboBox)sender).SelectedItem.ToString();
                        ((LayoutControlV2)selectedTree.SelectedNode.Tag).ClassName = strClassName;
                        AdjustTabVisibility(strClassName);
                    }
                }
            }
        }

        private void AdjustTabVisibility(string strClassName)
        {
            //Remove all pages
            for (int idxPage = tabInformations.TabPages.Count - 1; idxPage >= 0; idxPage--)
                tabInformations.TabPages.RemoveAt(idxPage);

            //Add Accessible Pages
            foreach (TabPage tab in tabItems.Where(e => e.IsAccessible))
                tabInformations.TabPages.Add(tab);
        }

        private void ckIsReadOnly_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox && !selectedTree.IsNull() && !selectedTree.SelectedNode.IsNull())
            {
                if (selectedTree.SelectedNode.Tag is LayoutControlV2)
                {
                    ((LayoutControlV2)selectedTree.SelectedNode.Tag).IsEditable = ((CheckBox)sender).Checked;

                    if (((CheckBox)sender).Checked)
                    {
                        if (this.ckIsEditableOnInsert.Checked)
                            this.ckIsEditableOnInsert.Checked = false;
                        if (this.ckAlwaysEditable.Checked)
                            this.ckAlwaysEditable.Checked = false;
                    }
                }
            }
        }

        private void ckAlwaysEditable_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox && !selectedTree.IsNull() && !selectedTree.SelectedNode.IsNull())
            {
                if (selectedTree.SelectedNode.Tag is LayoutControlV2)
                {
                    ((LayoutControlV2)selectedTree.SelectedNode.Tag).AlwaysEditable = ((CheckBox)sender).Checked;
                    if (((CheckBox)sender).Checked)
                    {
                        if (this.ckIsEditable.Checked)
                            this.ckIsEditable.Checked = false;
                        if (this.ckIsEditableOnInsert.Checked)
                            this.ckIsEditableOnInsert.Checked = false;
                    }
                }
            }
        }

        private void txConnectedAttribute_TextChanged(object sender, EventArgs e)
        {
            if (!selectedTree.IsNull() && !selectedTree.SelectedNode.IsNull())
            {
                if (selectedTree.SelectedNode.Tag is LayoutControlV2)
                {
                    if (sender.Equals(this.txConnectedAttribute))
                        ((LayoutControlV2)selectedTree.SelectedNode.Tag).ConnectedAttribute = this.txConnectedAttribute.Text;
                }
            }
        }

        private void cmbAggregationFunction_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is ComboBox && !selectedTree.IsNull() && !selectedTree.SelectedNode.IsNull())
            {
                if (sender.Equals(this.cmbAggregationFunction) && !this.cmbAggregationFunction.SelectedItem.IsNull())
                {
                    if (selectedTree.SelectedNode.Tag is LayoutControlV2)
                    {
                        (selectedTree.SelectedNode.Tag as LayoutControlV2).AggregationFunction = ((ComboBox)sender).SelectedItem.ToString();

                        this.txtAggregationDescription.Enabled = !(selectedTree.SelectedNode.Tag as LayoutControlV2).AggregationFunction.Equals("None");
                    }

                }
            }
        }

        private void txtAggregationDescription_TextChanged(object sender, EventArgs e)
        {
            if (!selectedTree.IsNull() && !selectedTree.SelectedNode.IsNull())
            {
                if (selectedTree.SelectedNode.Tag is LayoutControlV2)
                {
                    if (sender.Equals(this.txtAggregationDescription))
                        (selectedTree.SelectedNode.Tag as LayoutControlV2).AggregationDescription = this.txtAggregationDescription.Text;
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.layoutDefinition = null;
            this.RefreshOnLoad(this.currentLayout);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("All configurations will be removed. Do you really want to reset the current layout?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                this.Refresh(this.currentLayout, true);
        }

        private void ckIsVisible_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox && !selectedTree.IsNull() && !selectedTree.SelectedNode.IsNull())
            {
                if (selectedTree.SelectedNode.Tag is LayoutElement)
                    ((LayoutElement)selectedTree.SelectedNode.Tag).IsVisible = ((CheckBox)sender).Checked;
            }
        }

        private void ckIsExpanded_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox && !selectedTree.IsNull() && !selectedTree.SelectedNode.IsNull())
            {
                if (selectedTree.SelectedNode.Tag is LayoutContainer)
                    (selectedTree.SelectedNode.Tag as LayoutContainer).IsExpanded = (sender as CheckBox).Checked;
            }
        }


        #region Chart Information Events

        private void txtFinancialOpen_TextChanged(object sender, EventArgs e)
        {
            if (tabInformations.SelectedTab == tabChart)
                ((LayoutControlV2)selectedTree.SelectedNode.Tag).OpenValueFieldName = ((TextBox)sender).Text;
        }

        private void txtFinancialClose_TextChanged(object sender, EventArgs e)
        {
            if (tabInformations.SelectedTab == tabChart)
                ((LayoutControlV2)selectedTree.SelectedNode.Tag).CloseValueFieldName = ((TextBox)sender).Text;

        }

        private void txtFinancialLow_TextChanged(object sender, EventArgs e)
        {
            if (tabInformations.SelectedTab == tabChart)
                ((LayoutControlV2)selectedTree.SelectedNode.Tag).LowValueFieldName = ((TextBox)sender).Text;
        }

        private void txtFinancialHigh_TextChanged(object sender, EventArgs e)
        {
            if (tabInformations.SelectedTab == tabChart)
                ((LayoutControlV2)selectedTree.SelectedNode.Tag).HighValueFieldName = ((TextBox)sender).Text;
        }

        private void txtRadialY_TextChanged(object sender, EventArgs e)
        {
            if (tabInformations.SelectedTab == tabChart)
                ((LayoutControlV2)selectedTree.SelectedNode.Tag).YValueFieldName = ((TextBox)sender).Text;
        }

        private void txtRangeLow_TextChanged(object sender, EventArgs e)
        {
            if (tabInformations.SelectedTab == tabChart)
                ((LayoutControlV2)selectedTree.SelectedNode.Tag).LowValueFieldName = ((TextBox)sender).Text;
        }

        private void txtRangeHigh_TextChanged(object sender, EventArgs e)
        {
            if (tabInformations.SelectedTab == tabChart)
                ((LayoutControlV2)selectedTree.SelectedNode.Tag).HighValueFieldName = ((TextBox)sender).Text;

        }

        private void txtBubbleSize_TextChanged(object sender, EventArgs e)
        {
            if (tabInformations.SelectedTab == tabChart)
                ((LayoutControlV2)selectedTree.SelectedNode.Tag).BubbleSizeFieldName = ((TextBox)sender).Text;

        }

        private void txtBubbleY_TextChanged(object sender, EventArgs e)
        {
            if (tabInformations.SelectedTab == tabChart)
                ((LayoutControlV2)selectedTree.SelectedNode.Tag).YValueFieldName = ((TextBox)sender).Text;
        }


        #endregion

        #region DragAndDrop Rotines

        private void tree_DragDrop(object sender, DragEventArgs e)
        {
            TreeNode tabNodeControl;
            TreeView tree = sender as TreeView;
            Point pos = tree.PointToClient(new Point(e.X, e.Y));
            SetDropLocation(e);
            TreeNode targetNode = tree.GetNodeAt(pos);
            TreeNode nodeCopy, pargetParent;
            int LastIndex = 0;
            string nodeName;

            //_bitmap.Dispose();
            //_curDrag.Dispose();
            //_curDrag = null;
            //_bitmap = null;

            if (targetNode != null)
            {


                //if (tree == treeTopGroups && sourceNode.Parent == null && targetNode.Parent != null)
                //{
                //    MessageBox.Show("This information cannot dragged to this local!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}
                if (e.Data.GetDataPresent(typeof(TreeNode)))
                {
                    TreeNode nodSource = (TreeNode)e.Data.GetData(typeof(TreeNode));

                    if ((nodSource.Tag != null && nodSource.Tag is LayoutElement && ((LayoutElement)nodSource.Tag).IsDerived))
                        return;



                    if (nodSource != targetNode && !IsParentNode(nodSource, targetNode))
                    {
                        if (targetNode != null)
                        {
                            if (targetNode.LastNode != null)
                                LastIndex = targetNode.LastNode.Index;
                            else
                                LastIndex = 0;
                        }

                        //Prepare TabControl Container
                        if (targetNode.Tag is LayoutControlV2 && nodSource.Tag is LayoutContainer && nodSource.ImageIndex == GetIconIndexFromClassName("TabItem"))
                        {
                            if (((LayoutContainer)nodSource.Tag).ClassName == "TabItem")
                            {
                                tabNodeControl = CreateTabControl(targetNode);
                                treeTopGroups.Nodes.Remove(nodSource);
                                tabNodeControl.Nodes.Add(nodSource);
                                targetNode.Parent.Nodes.Insert(LastIndex, tabNodeControl);
                                if (nodSource.Parent != null)
                                    if (nodSource.Parent.GetNodeCount(true) == 0)
                                        nodSource.Parent.Remove();
                            }
                        }
                        //Prepare TabControl Container
                        else if (targetNode.Tag is LayoutContainer && nodSource.Tag is LayoutControlV2 && targetNode.ImageIndex == GetIconIndexFromClassName("TabControl"))
                        {
                            if (((LayoutContainer)targetNode.Tag).ClassName == "TabControl")
                            {
                                tabNodeControl = CreateTabItem(targetNode);
                                // Copy process
                                if (e.Effect == DragDropEffects.Copy)
                                {
                                    nodSource = (TreeNode)nodSource.Clone();
                                    nodSource.Tag = (LayoutControlV2)CopyNodeTag(nodSource);
                                    nodSource.Name = ((LayoutControlV2)nodSource.Tag).Name;
                                }
                                else
                                    treeTopGroups.Nodes.Remove(nodSource);
                                tabNodeControl.Nodes.Add(nodSource);
                                targetNode.Nodes.Insert(LastIndex, tabNodeControl);
                                if (nodSource.Parent != null)
                                    if (nodSource.Parent.GetNodeCount(true) == 0)
                                        nodSource.Parent.Remove();
                            }

                        }
                        //Prepare TabControl Container
                        else if (targetNode.Tag is LayoutContainer && nodSource.Tag is LayoutContainer && targetNode.ImageIndex == GetIconIndexFromClassName("TabItem") && nodSource.ImageIndex == GetIconIndexFromClassName("TabItem"))
                        {
                            if (((LayoutContainer)targetNode.Tag).ClassName == "TabItem" && ((LayoutContainer)nodSource.Tag).ClassName == "TabItem")
                            {
                                tabNodeControl = CreateTabControl(targetNode);
                                treeTopGroups.Nodes.Remove(nodSource);
                                tabNodeControl.Nodes.Add(nodSource);
                                targetNode.Nodes.Insert(LastIndex, tabNodeControl);
                                if (nodSource.Parent != null)
                                    if (nodSource.Parent.GetNodeCount(true) == 0)
                                        nodSource.Parent.Remove();
                            }

                        }
                        // TabControl With DataGrid
                        else if (targetNode.Tag is LayoutContainer && nodSource.Tag is LayoutContainer && targetNode.ImageIndex == GetIconIndexFromClassName("TabControl") && nodSource.ImageIndex == GetIconIndexFromClassName("DataGrid"))
                        {
                            if (((LayoutContainer)targetNode.Tag).ClassName == "TabControl" && ((LayoutContainer)nodSource.Tag).ClassName == "DataGrid")
                            {
                                tabNodeControl = CreateTabItem(targetNode);
                                treeTopGroups.Nodes.Remove(nodSource);
                                tabNodeControl.Nodes.Add(nodSource);
                                targetNode.Nodes.Insert(LastIndex, tabNodeControl);
                            }

                        }
                        // DataGrid With DataGrid
                        else if (targetNode.Tag is LayoutContainer && nodSource.Tag is LayoutContainer && targetNode.ImageIndex == GetIconIndexFromClassName("DataGrid") && nodSource.ImageIndex == GetIconIndexFromClassName("DataGrid"))
                        {
                            if (((LayoutContainer)targetNode.Tag).ClassName == "DataGrid" && ((LayoutContainer)nodSource.Tag).ClassName == "DataGrid")
                            {
                                tabNodeControl = CreateTabControlAndTabItem(targetNode);
                                treeTopGroups.Nodes.Remove(nodSource);
                                tabNodeControl.Nodes.Add(nodSource);
                            }

                        }

                        // TabControl With TabControl
                        else if (targetNode.Tag is LayoutContainer && nodSource.Tag is LayoutContainer && targetNode.ImageIndex == GetIconIndexFromClassName("TabControl") && nodSource.ImageIndex == GetIconIndexFromClassName("TabControl"))
                        {
                            tabNodeControl = CreateTabControlAndTabItem(targetNode);
                            treeTopGroups.Nodes.Remove(nodSource);
                            tabNodeControl.Nodes.Add(nodSource);
                        }

                        // DataGrid With Control
                        else if (targetNode.Tag is LayoutControlV2 && nodSource.Tag is LayoutContainer && nodSource.ImageIndex == GetIconIndexFromClassName("DataGrid"))
                        {
                            TreeNode tmpParent = targetNode.Parent;
                            if (tmpParent != null)
                            {
                                if (tmpParent.ImageIndex == GetIconIndexFromClassName("Expander") || tmpParent.ImageIndex == GetIconIndexFromClassName("GroupBox") || tmpParent.ImageIndex == GetIconIndexFromClassName("CustomContainer"))
                                {
                                    treeTopGroups.Nodes.Remove(nodSource);
                                    if (DropPositionFlag == DropLocation.Up)
                                        tmpParent.Nodes.Insert(targetNode.Index, nodSource);
                                    else
                                        tmpParent.Nodes.Insert(targetNode.Index + 1, nodSource);
                                }
                                else if (tmpParent.ImageIndex == GetIconIndexFromClassName("TabControl"))
                                {
                                    tabNodeControl = CreateTabItem(tmpParent);
                                    treeTopGroups.Nodes.Remove(nodSource);
                                    if (DropPositionFlag == DropLocation.Up)
                                        tmpParent.Nodes.Insert(targetNode.Index, nodSource);
                                    else
                                        tmpParent.Nodes.Insert(targetNode.Index + 1, nodSource);
                                }
                                else
                                {
                                    treeTopGroups.Nodes.Remove(nodSource);
                                    if (DropPositionFlag == DropLocation.Up)
                                        tmpParent.Nodes.Insert(targetNode.Index, nodSource);
                                    else
                                        tmpParent.Nodes.Insert(targetNode.Index, nodSource);
                                }
                            }

                        }
                        else if ((targetNode.Parent != null && nodSource.Parent != null) && (targetNode.Tag is LayoutControlV2))
                        {
                            if (IsMultiplesSelect)
                                MoveOrCopyMultiplesNodes(sourceNode.Index, targetNode, (e.Effect == DragDropEffects.Copy));
                            else
                            {
                                if (e.Effect == DragDropEffects.Copy)
                                {
                                    nodSource = (TreeNode)nodSource.Clone();
                                    nodSource.Tag = (LayoutControlV2)CopyNodeTag(nodSource);
                                    nodSource.Name = ((LayoutControlV2)nodSource.Tag).Name;
                                }
                                else
                                    treeTopGroups.Nodes.Remove(nodSource);
                                if (DropPositionFlag == DropLocation.Up)
                                    targetNode.Parent.Nodes.Insert(targetNode.Index, nodSource);
                                else
                                    targetNode.Parent.Nodes.Insert(targetNode.Index + 1, nodSource);
                            }
                        }
                        else if ((nodSource.Parent != null && targetNode.Parent == null) && (targetNode != nodSource))
                        {
                            if (e.Effect == DragDropEffects.Copy)
                            {
                                nodSource = (TreeNode)nodSource.Clone();
                                nodSource.Tag = (LayoutControlV2)CopyNodeTag(nodSource);
                                nodSource.Name = ((LayoutControlV2)nodSource.Tag).Name;
                                if (targetNode.Tag is LayoutContainer || targetNode.Tag is LayoutContainer)
                                    targetNode.Nodes.Insert(0, nodSource);
                                else
                                    targetNode.Parent.Nodes.Add(nodSource);


                            }
                            else
                            {
                                if (IsMultiplesSelect)
                                    MoveOrCopyMultiplesNodes(sourceNode.Index, targetNode, (e.Effect == DragDropEffects.Copy));
                                else
                                {
                                    treeTopGroups.Nodes.Remove(nodSource);
                                    if (targetNode.Tag is LayoutContainer && nodSource.Tag is LayoutContainer)
                                        if (((LayoutContainer)targetNode.Tag).ClassName == "TabControl")
                                            if (DropPositionFlag == DropLocation.Up)
                                                treeTopGroups.Nodes.Insert(targetNode.Index, nodSource);
                                            else
                                                treeTopGroups.Nodes.Insert(targetNode.Index + 1, nodSource);
                                        else
                                            if (DropPositionFlag == DropLocation.Up)
                                                treeTopGroups.Nodes.Insert(targetNode.Index, nodSource);
                                            else
                                                targetNode.Nodes.Insert(0, nodSource);
                                    else
                                        if (DropPositionFlag == DropLocation.Up)
                                            treeTopGroups.Nodes.Insert(targetNode.Index, nodSource);
                                        else
                                            targetNode.Nodes.Insert(0, nodSource);
                                }
                            }
                        }
                        else if (nodSource.Parent == null && (targetNode.Tag is LayoutControlV2))
                        {
                            treeTopGroups.Nodes.Remove(nodSource);
                            targetNode.Parent.Nodes.Insert(targetNode.Nodes.Count, nodSource);
                        }
                        else if (targetNode.Tag is LayoutContainer)
                        {
                            if (IsMultiplesSelect)
                                MoveOrCopyMultiplesNodes(sourceNode.Index, targetNode, (e.Effect == DragDropEffects.Copy));
                            else
                            {
                                if (e.Effect == DragDropEffects.Copy)
                                {
                                    nodSource = (TreeNode)nodSource.Clone();
                                    nodSource.Tag = (LayoutControlV2)CopyNodeTag(nodSource);
                                    nodSource.Name = ((LayoutControlV2)nodSource.Tag).Name;
                                }
                                else
                                {
                                    treeTopGroups.Nodes.Remove(nodSource);
                                }
                                if (targetNode.Tag is LayoutContainer && nodSource.Tag is LayoutContainer)
                                    if (((LayoutContainer)targetNode.Tag).ClassName == "TabControl")
                                        if (DropPositionFlag == DropLocation.Up)
                                            treeTopGroups.Nodes.Insert(targetNode.Index, nodSource);
                                        else
                                            treeTopGroups.Nodes.Insert(targetNode.Index + 1, nodSource);
                                    else
                                        targetNode.Nodes.Insert(0, nodSource);
                                else
                                    if (DropPositionFlag == DropLocation.Up)
                                        treeTopGroups.Nodes.Insert(targetNode.Index, nodSource);
                                    else
                                        targetNode.Nodes.Insert(0, nodSource);
                            }
                        }
                        else
                        {
                            treeTopGroups.Nodes.Remove(nodSource);
                            if (DropPositionFlag == DropLocation.Up)
                                treeTopGroups.Nodes.Insert(targetNode.Index, nodSource);
                            else
                                treeTopGroups.Nodes.Insert(targetNode.Index + 1, nodSource);
                        }
                    }
                }
                else
                {

                    //if (tree == treeChildren)
                    //if (tree == treeTopGroups)
                    //{
                    //    MessageBox.Show("This information cannot dragged to this local!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    return;
                    //}

                    nodeName = sourceNode.Name;
                    sourceNode.Name = "Old" + sourceNode.Name;
                    nodeCopy = new TreeNode(sourceNode.Text, sourceNode.ImageIndex, sourceNode.SelectedImageIndex);
                    nodeCopy.Name = nodeName;
                    nodeCopy.Tag = sourceNode.Tag;

                    if (targetNode.Parent == null)
                        pargetParent = targetNode;
                    else
                        pargetParent = targetNode.Parent;

                    if (sourceNode.Index > targetNode.Index)
                        if (DropPositionFlag == DropLocation.Up)
                            pargetParent.Nodes.Insert(targetNode.Index, nodeCopy);
                        else
                            pargetParent.Nodes.Insert(targetNode.Index + 1, nodeCopy);
                    else
                        if (DropPositionFlag == DropLocation.Up)
                            pargetParent.Nodes.Insert(targetNode.Index + 1, nodeCopy);
                        else
                            pargetParent.Nodes.Insert(targetNode.Index + 2, nodeCopy);

                    sourceNode.Remove();
                }

                //Paint tree
                //tree.Invalidate();

                //Reorder Data
                UpdateDataLayout(tree);
                this.CheckMeasures(targetNode);
            }
        }

        private bool IsParentNode(TreeNode myNode, TreeNode otherNode)
        {
            if (myNode.IsNull() || otherNode.IsNull())
                return false;

            if (myNode.Name == otherNode.Name)
                return true;

            if (otherNode != null)
                return IsParentNode(myNode, otherNode.Parent);

            return false;

        }

        private void ExpanderToTabControl(TreeNode expandTreeNode)
        {
            LayoutContainer lctrl;
            if (expandTreeNode.Tag is LayoutContainer)
                lctrl = expandTreeNode.Tag as LayoutContainer;
            else
                return;

            LayoutContainer pTmp;
            TreeNode newRootNode, newTabItem;

            newRootNode = CreateTabControl(expandTreeNode);
            //((LayoutContainer)newRootNode.Tag).IsGroupConverted = true;
            ((LayoutContainer)newRootNode.Tag).Name = newRootNode.Name;

            newTabItem = CreateTabItem(newRootNode);
            newRootNode.Nodes.Add(newTabItem);
            pTmp = newTabItem.Tag as LayoutContainer;

            pTmp.Name = newTabItem.Name;
            pTmp.DisplayName = lctrl.DisplayName;
            pTmp.ColumnCount = lctrl.ColumnCount;
            //pTmp.IsGroupConverted = true;

            newTabItem.Text = expandTreeNode.Text;

            foreach (TreeNode item in expandTreeNode.Nodes)
                newTabItem.Nodes.Add(item.Clone() as TreeNode);

            if (expandTreeNode.Parent == null)
                treeTopGroups.Nodes.Insert(expandTreeNode.Index, newRootNode);
            else
                expandTreeNode.Parent.Nodes.Insert(expandTreeNode.Index, newRootNode);

            treeTopGroups.Nodes.Remove(expandTreeNode);

            UpdateDataLayout(treeTopGroups);

        }
        private void ExpanderToWizard(TreeNode expandTreeNode)
        {
            LayoutContainer lctrl;
            if (expandTreeNode.Tag is LayoutContainer)
                lctrl = expandTreeNode.Tag as LayoutContainer;
            else
                return;

            LayoutContainer pTmp;
            TreeNode newRootNode, newTabItem;

            newRootNode = CreateWizardControl(expandTreeNode);
            ((LayoutContainer)newRootNode.Tag).Name = newRootNode.Name;

            newTabItem = CreateWizardItem(newRootNode);
            newRootNode.Nodes.Add(newTabItem);
            pTmp = newTabItem.Tag as LayoutContainer;

            pTmp.Name = newTabItem.Name;
            pTmp.DisplayName = lctrl.DisplayName;
            pTmp.ColumnCount = lctrl.ColumnCount;

            newTabItem.Text = expandTreeNode.Text;

            foreach (TreeNode item in expandTreeNode.Nodes)
                newTabItem.Nodes.Add(item.Clone() as TreeNode);

            if (expandTreeNode.Parent == null)
                treeTopGroups.Nodes.Insert(expandTreeNode.Index, newRootNode);
            else
                expandTreeNode.Parent.Nodes.Insert(expandTreeNode.Index, newRootNode);

            treeTopGroups.Nodes.Remove(expandTreeNode);

            UpdateDataLayout(treeTopGroups);

        }

        private void ExpanderToDockManager(TreeNode expandTreeNode)
        {
            LayoutContainer lctrl;
            if (expandTreeNode.Tag is LayoutContainer)
                lctrl = expandTreeNode.Tag as LayoutContainer;
            else
                return;

            LayoutContainer pTmp;
            TreeNode newRootNode, newTabItem;

            newRootNode = CreateDockManager(expandTreeNode);
            ((LayoutContainer)newRootNode.Tag).Name = newRootNode.Name;

            newTabItem = CreateDockItem(newRootNode);
            newRootNode.Nodes.Add(newTabItem);
            pTmp = newTabItem.Tag as LayoutContainer;

            pTmp.Name = newTabItem.Name;
            pTmp.DisplayName = lctrl.DisplayName;
            pTmp.ColumnCount = lctrl.ColumnCount;

            newTabItem.Text = expandTreeNode.Text;

            foreach (TreeNode item in expandTreeNode.Nodes)
                newTabItem.Nodes.Add(item.Clone() as TreeNode);

            if (expandTreeNode.Parent == null)
                treeTopGroups.Nodes.Insert(expandTreeNode.Index, newRootNode);
            else
                expandTreeNode.Parent.Nodes.Insert(expandTreeNode.Index, newRootNode);

            treeTopGroups.Nodes.Remove(expandTreeNode);

            UpdateDataLayout(treeTopGroups);
        }

        private void MoveOrCopyMultiplesNodes(int StartIndex, TreeNode nodTarget, bool IsCopy)
        {
            if (listSelectedNodes.Where(e => e.Tag != null && e.Tag is LayoutElement && ((LayoutElement)e.Tag).IsDerived).Count() == 0)
            {

                if (DropPositionFlag == DropLocation.Up)
                    StartIndex = nodTarget.Index;
                else
                    StartIndex = nodTarget.Index + 1;

                int _counter = 0;

                for (int i = StartIndex; i < listSelectedNodes.Count + StartIndex; i++)
                {
                    if (IsCopy)
                    {
                        if (listSelectedNodes[_counter].Tag is LayoutControlV2)
                        {
                            TreeNode nodNew;
                            nodNew = (TreeNode)listSelectedNodes[_counter].Clone();
                            nodNew.Tag = (LayoutControlV2)CopyNodeTag(listSelectedNodes[_counter]);
                            nodNew.Name = ((LayoutControlV2)nodNew.Tag).Name;
                            nodNew.BackColor = SystemColors.Window;
                            nodNew.ForeColor = SystemColors.WindowText;

                            if (nodTarget.Tag is LayoutControlV2)
                                nodTarget.Parent.Nodes.Insert(i, nodNew);
                            else
                                nodTarget.Nodes.Insert(i, nodNew);
                        }
                    }
                    else
                    {
                        treeTopGroups.Nodes.Remove(listSelectedNodes[_counter]);
                        if (nodTarget.Tag is LayoutControlV2)
                            nodTarget.Parent.Nodes.Insert(i, listSelectedNodes[_counter]);
                        else
                            nodTarget.Nodes.Insert(i, listSelectedNodes[_counter]);
                    }
                    _counter++;
                }
            }
        }
        private LayoutContainer CopyLayoutContainer(LayoutContainer containerOrigin)
        {
            LayoutContainer containerDest = new LayoutContainer();
            containerDest.CopyInstanceFrom(containerOrigin);
            foreach (var ctrl in containerOrigin.Controls)
            {
                if (ctrl is LayoutContainer)
                    containerDest.Controls.Add(CopyLayoutContainer((LayoutContainer)ctrl));
                else
                {
                    var newCtrl = new LayoutElement();
                    newCtrl.CopyInstanceFrom(ctrl);
                    containerDest.Controls.Add(newCtrl);
                }
            }
            return containerDest;
        }
        private LayoutElement CopyNodeTag(TreeNode nodSource)
        {
            LayoutElement lcRet = null;
            if (nodSource.Tag is LayoutControlV2)
            {
                lcRet = new LayoutControlV2();
                LayoutControlV2 lc = (LayoutControlV2)nodSource.Tag;
                lcRet.CopyFrom(lc);
            }
            else if (nodSource.Tag is LayoutContainer)
            {
                lcRet = new LayoutContainer();
                LayoutContainer lc = (LayoutContainer)nodSource.Tag;
                lcRet = CopyLayoutContainer(lc);
            }
            string uniqueLeftWord = "Copy_" + Environment.TickCount.ToString();

            Action<LayoutElement, LayoutContainer> renameCopy = null;
            renameCopy = (element, parent) =>
            {
                if (element != null)
                {
                    element.DefinedUserName = element.Name = uniqueLeftWord + "_" + (element.BindingPath.IsNullOrEmpty() ? ("_" + element.Name).Right("_") : element.BindingPath.Right("."));
                    if (parent != null)
                        element.ParentName = parent.Name;
                    if (element is LayoutContainer)
                        ((LayoutContainer)element).Controls.ForEach(e => renameCopy(e, (LayoutContainer)element));
                }
            };


            renameCopy(lcRet, null);


            return lcRet;
        }


        void CustomizingLayout_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            e.UseDefaultCursors = false;
            if ((e.Effect & DragDropEffects.Copy) == DragDropEffects.Copy)
                if (DropPositionFlag == DropLocation.Up)
                    Cursor.Current = _curDragCopyBefore;
                else if (DropPositionFlag == DropLocation.Down)
                    Cursor.Current = _curDragCopyAfter;
                else
                    Cursor.Current = _curDragCopyInsideAfter;
            else if ((e.Effect & DragDropEffects.Move) == DragDropEffects.Move)
                if (DropPositionFlag == DropLocation.Up)
                    Cursor.Current = _curDragBefore;
                else if (DropPositionFlag == DropLocation.Down)
                    Cursor.Current = _curDragAfter;
                else
                    Cursor.Current = _curDragInsideAfter;

            else
                e.UseDefaultCursors = true;
        }

        private void tree_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(TreeNode)))
            {
                e.Effect = DragDropEffects.None;
                return;
            }
            else
            {
                TreeNode sourceNode = (TreeNode)e.Data.GetData(typeof(TreeNode));

                if ((sourceNode.Tag != null && sourceNode.Tag is LayoutElement && ((LayoutElement)sourceNode.Tag).IsDerived))
                {
                    e.Effect = DragDropEffects.None;
                    return;
                }

                SetDropLocation(e);
                Point pt = treeTopGroups.PointToClient(new Point(e.X, e.Y));
                TreeNode targetNode = treeTopGroups.GetNodeAt(pt);


                //if (targetNode.Tag is LayoutContainer && sourceNode.Tag is LayoutContainer || (e.KeyState & 8) == 8 && (sourceNode.Tag is LayoutContainer || sourceNode.Tag is LayoutContainer))
                //{
                //    e.Effect = DragDropEffects.None;
                //    return;
                //}
                //else
                //{
                if ((e.KeyState & 8) == 8)
                {
                    e.Effect = DragDropEffects.Copy;
                    if (DropPositionFlag == DropLocation.Up)
                        Cursor.Current = _curDragCopyBefore;
                    else
                        Cursor.Current = _curDragCopyAfter;
                }
                else
                {
                    e.Effect = DragDropEffects.Move;
                    if (DropPositionFlag == DropLocation.Up)
                        Cursor.Current = _curDragBefore;
                    else
                        Cursor.Current = _curDragAfter;
                }
                //}
            }

        }

        private void SetDropLocation(DragEventArgs e)
        {

            Point pt = treeTopGroups.PointToClient(new Point(e.X, e.Y));
            TreeNode targetNode = treeTopGroups.GetNodeAt(pt);

            if (targetNode == null) return;

            if (pt.Y > (targetNode.Bounds.Y + (targetNode.Bounds.Height / 2)))
                if (sourceNode.Tag is LayoutContainer && targetNode.Tag is LayoutContainer)
                    DropPositionFlag = DropLocation.Inside;
                else
                    DropPositionFlag = DropLocation.Down;
            else
                DropPositionFlag = DropLocation.Up;

        }

        private void tree_ItemDrag(object sender, ItemDragEventArgs e)
        {

            sourceNode = (TreeNode)e.Item;

            if ((sourceNode.Tag != null && sourceNode.Tag is LayoutElement && ((LayoutElement)sourceNode.Tag).IsDerived))
                return;


            ClearCursors();
            _curDrag = CreateCursor(sourceNode, "Hold Ctrl to activate copy mode.");
            _curDragAfter = CreateCursor(sourceNode, "Move after (Hold Ctrl to activate copy mode.)");
            _curDragBefore = CreateCursor(sourceNode, "Move before (Hold Ctrl to activate copy mode.)");
            _curDragCopy = CreateCursor(sourceNode, "Release mouse button to copy");
            _curDragCopyAfter = CreateCursor(sourceNode, "Copy after (Release mouse button to copy)");
            _curDragCopyBefore = CreateCursor(sourceNode, "Copy before (Release mouse button to copy)");
            _curDragInsideAfter = CreateCursor(sourceNode, "Put inside (Hold Ctrl to activate copy mode.)");
            _curDragCopyInsideAfter = CreateCursor(sourceNode, "Put copy inside (Release mouse button to copy)");

            //CreateCursor(sourceNode);
            DoDragDrop(sourceNode, DragDropEffects.Move | DragDropEffects.Copy);


        }

        private void ClearCursors()
        {
            if (!_curDrag.IsNull())
                _curDrag.Dispose();
            if (!_curDragAfter.IsNull())
                _curDragAfter.Dispose();
            if (!_curDragBefore.IsNull())
                _curDragBefore.Dispose();
            if (!_curDragCopy.IsNull())
                _curDragCopy.Dispose();
            if (!_curDragCopyAfter.IsNull())
                _curDragCopyAfter.Dispose();
            if (!_curDragCopyBefore.IsNull())
                _curDragCopyBefore.Dispose();
            if (!_curDragInsideAfter.IsNull())
                _curDragInsideAfter.Dispose();
            if (!_curDragCopyInsideAfter.IsNull())
                _curDragCopyInsideAfter.Dispose();
        }

        private void treeTopGroups_DragOver(object sender, DragEventArgs e)
        {


            if (sender is TreeView)
            {
                TreeNode sourceNode = (TreeNode)e.Data.GetData(typeof(TreeNode));

                if ((sourceNode.Tag != null && sourceNode.Tag is LayoutElement && ((LayoutElement)sourceNode.Tag).IsDerived))
                {
                    e.Effect = DragDropEffects.None;
                    return;
                }

                SetDropLocation(e);
                Point pt = treeTopGroups.PointToClient(new Point(e.X, e.Y));
                TreeNode targetNode = treeTopGroups.GetNodeAt(pt);


                if (targetNode != null && targetNode.Parent != null)

                    if ((e.KeyState & 8) == 8 &&
                        (e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy && sourceNode.Tag is LayoutControlV2)
                    {
                        e.Effect = DragDropEffects.Copy;
                        if (DropPositionFlag == DropLocation.Up)
                            Cursor.Current = _curDragCopyBefore;
                        else if (DropPositionFlag == DropLocation.Down)
                            Cursor.Current = _curDragCopyAfter;
                        else
                            Cursor.Current = _curDragCopyInsideAfter;
                    }
                    else if ((e.KeyState & 8) != 8 && (e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move)
                    {
                        e.Effect = DragDropEffects.Move;
                        if (DropPositionFlag == DropLocation.Up)
                            Cursor.Current = _curDragBefore;
                        else if (DropPositionFlag == DropLocation.Down)
                            Cursor.Current = _curDragAfter;
                        else
                            Cursor.Current = _curDragInsideAfter;

                    }
                    else
                    {
                        if ((e.KeyState & 8) == 8 &&
                            (e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy && sourceNode.Tag is LayoutControlV2)
                        {
                            e.Effect = DragDropEffects.Copy;
                            if (DropPositionFlag == DropLocation.Up)
                                Cursor.Current = _curDragCopyBefore;
                            else if (DropPositionFlag == DropLocation.Down)
                                Cursor.Current = _curDragCopyAfter;
                            else
                                Cursor.Current = _curDragCopyInsideAfter;

                        }
                        else if ((e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move)
                        {
                            e.Effect = DragDropEffects.Move;
                            if (DropPositionFlag == DropLocation.Up)
                                Cursor.Current = _curDragBefore;
                            else if (DropPositionFlag == DropLocation.Down)
                                Cursor.Current = _curDragAfter;
                            else
                                Cursor.Current = _curDragInsideAfter;
                        }
                        //else
                        //{
                        //    e.Effect = DragDropEffects.None;
                        //    return;
                        //}
                    }
                //}
                treeTopGroups.SelectedNode = targetNode;
            }

        }

        #endregion

        private void btAddField_Click(object sender, EventArgs e)
        {
            if (!this.selectedTree.IsNull())
            {
                LayoutContainer group = null;
                if (this.selectedTree.SelectedNode.Tag is LayoutContainer)
                    group = (LayoutContainer)this.selectedTree.SelectedNode.Tag;

                string name = "CustomControl" + Environment.TickCount.ToString();
                LayoutControlV2 control = new LayoutControlV2() { Name = name, DataType = String.Empty, DisplayName = "New Control", ImageIndex = GetIconIndexFromClassName("Button"), ClassName = "Button", ActionEvent = name + "ActionEvent", IsCustomized = true, IsDataField = false, IsVisible = true, IsEditable = true, AggregationFunction = "None", ChartTitle = String.Empty, AxisXLabelRotation = 0, AxisXTitle = String.Empty, AxisYLabelRotation = 0, AxisYTitle = String.Empty, ChartLegendPosition = 0, LabelFieldName = String.Empty, LegendLabelFieldName = String.Empty, LegendTitle = String.Empty, XCategoryFieldName = String.Empty, YValueFieldName = String.Empty };
                if (!group.IsNull())
                {
                    group.Controls.Add(control);
                    control.ParentName = group.Name;
                }

                TreeNode tn;
                if (treeTopGroups.SelectedNode.Parent != null)
                    tn = this.selectedTree.SelectedNode.Parent.Nodes.Add(control.Name, control.DisplayName, control.ImageIndex, control.ImageIndex);
                else
                    tn = this.selectedTree.SelectedNode.Nodes.Add(control.Name, control.DisplayName, control.ImageIndex, control.ImageIndex);
                tn.Tag = control;
                this.selectedTree.SelectedNode = tn;

                this.selectedTree.Select();
                this.selectedTree.Invalidate();
            }
        }

        private bool IsDataField(string className)
        {
            return className.InList("CheckBox", "RadioButtonGroup", "ComboBox", "EditBox", "EconomicGroup", cLookUpTextBox, "NumericTextBox", "TextBox", "TextBlock", "MaskedTextBox", "KpiBox", "Gauge", "DateTimeTextBox", "ColorPicker");
        }

        private void mnuContextChange_Click(object sender, EventArgs e)
        {
            if (!selectedTree.IsNull() && !selectedTree.SelectedNode.IsNull())
            {
                if (selectedTree.SelectedNode.Tag is LayoutControlV2)
                {
                    LayoutControlV2 lctmp;
                    String strClassName = ((ToolStripItem)sender).Text;
                    lctmp = ((LayoutControlV2)selectedTree.SelectedNode.Tag);
                    lctmp.ClassName = strClassName;
                    lctmp.ImageIndex = GetIconIndexFromClassName(strClassName);
                    lctmp.IsDataField = IsDataField(lctmp.ClassName);

                    if (strClassName == "Gauge")
                        lctmp.IsDataField = false;

                    if (treeTopGroups.SelectedNode != null)
                    {
                        treeTopGroups.SelectedNode.ImageIndex = lctmp.ImageIndex;
                        treeTopGroups.SelectedNode.SelectedImageIndex = lctmp.ImageIndex;
                    }
                    tree_AfterSelect(treeTopGroups, new TreeViewEventArgs(treeTopGroups.SelectedNode));
                }
            }
        }

        private void treeTopGroups_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                treeTopGroups.SelectedNode = treeTopGroups.GetNodeAt(e.X, e.Y);
                PrepareMenu(treeTopGroups.SelectedNode);
                selectedTree = treeTopGroups;
                Point pos = treeTopGroups.PointToScreen(new Point(e.X, e.Y));
                ctxMenuControls.Show(pos);
            }
        }

        private void mnuTabControl_Click(object sender, EventArgs e)
        {
            AddTabControlAndTabItem();
        }

        private void mnuTabItem_Click(object sender, EventArgs e)
        {
            AddTabControlAndTabItem();
        }

        private void AddTabControlAndTabItem()
        {
            TreeNode selNode = treeTopGroups.SelectedNode;

            if (!selNode.IsNull() && selNode.Tag is LayoutContainer)
            {
                if (((LayoutContainer)selNode.Tag).ClassName == cTabItem)
                {
                    selNode.Parent.Nodes.Add(CreateTabItem(selNode.Parent));
                    return;
                }
                else
                    if (((LayoutContainer)selNode.Tag).ClassName == cTabControl)
                    {
                        selNode.Nodes.Add(CreateTabItem(selNode));
                        return;
                    }


            }

            if (selNode.IsNull() || ((LayoutContainer)selNode.Tag).ClassName.InList(cExpander, cGroupBox, cCustomContainer))
            {
                CreateTabControlAndTabItem(selNode);
                return;
            }

            MessageBox.Show("Could not add the TabControl/TabItem.\nPlease try to add in another item.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        #region DockManager Insert Routines
        private TreeNode CreateDockManagerAndDockItem(TreeNode selNode)
        {
            LayoutContainer lpage;
            LayoutContainer lpageItem;

            if (selNode.Tag is LayoutContainer)
            {
                lpage = new LayoutContainer() { Name = "DockManager_" + Guid.NewGuid().ToString().Replace("-", String.Empty), ClassName = "DockManager", DisplayName = "DockManager", ImageIndex = GetIconIndexFromClassName("DockManager"), ParentName = ((LayoutContainer)selNode.Tag).Name };
                lpageItem = new LayoutContainer() { Name = "DockItem_" + Guid.NewGuid().ToString().Replace("-", String.Empty), ClassName = "DockItem", DisplayName = "DockItem", ImageIndex = GetIconIndexFromClassName("DockItem"), ParentName = ((LayoutContainer)selNode.Tag).Name };
            }
            else
            {
                lpage = new LayoutContainer() { Name = "DockManager_" + Guid.NewGuid().ToString().Replace("-", String.Empty), ClassName = "DockManager", DisplayName = "DockManager", ImageIndex = GetIconIndexFromClassName("DockManager"), ParentName = String.Empty };
                lpageItem = new LayoutContainer() { Name = "DockItem_" + Guid.NewGuid().ToString().Replace("-", String.Empty), ClassName = "DockItem", DisplayName = "DockItem", ImageIndex = GetIconIndexFromClassName("DockItem"), ParentName = String.Empty };
            }

            TreeNode tabCtrlNode = selNode.Nodes.Add(lpage.Name, lpage.DisplayName, GetIconIndexFromClassName(lpage.ClassName), GetIconIndexFromClassName(lpage.ClassName));
            tabCtrlNode.Tag = lpage;
            TreeNode tabNode = tabCtrlNode.Nodes.Add(lpageItem.Name, lpageItem.DisplayName, GetIconIndexFromClassName(lpageItem.ClassName), GetIconIndexFromClassName(lpageItem.ClassName));
            tabNode.Tag = lpageItem;
            lpage.Controls.Add(lpageItem);

            if (selNode != null && (selNode.Tag is LayoutContainer))
            {
                ((LayoutContainer)selNode.Tag).Controls.Add(lpage);
                lpage.ParentName = ((LayoutContainer)selNode.Tag).Name;
            }

            return tabNode;
        }

        private TreeNode CreateDockItem(TreeNode selNode)
        {
            LayoutContainer lpageItem = new LayoutContainer();

            if (selNode.Tag is LayoutContainer)
                lpageItem = new LayoutContainer() { Name = "DockItem_" + Guid.NewGuid().ToString().Replace("-", String.Empty), ClassName = "DockItem", DisplayName = "DockItem", ImageIndex = GetIconIndexFromClassName("DockItem"), ParentName = ((LayoutContainer)selNode.Tag).Name };
            else
                lpageItem = new LayoutContainer() { Name = "DockItem_" + Guid.NewGuid().ToString().Replace("-", String.Empty), ClassName = "DockItem", DisplayName = "DockItem", ImageIndex = GetIconIndexFromClassName("DockItem"), ParentName = String.Empty };

            TreeNode tabNode = new TreeNode().Nodes.Add(lpageItem.DisplayName, lpageItem.DisplayName, GetIconIndexFromClassName(lpageItem.ClassName), GetIconIndexFromClassName(lpageItem.ClassName));
            tabNode.Tag = lpageItem;

            if (selNode != null && (selNode.Tag is LayoutContainer))
            {
                ((LayoutContainer)selNode.Tag).Controls.Add(lpageItem);
                lpageItem.ParentName = ((LayoutContainer)selNode.Tag).Name;
            }

            return tabNode;
        }

        private TreeNode CreateDockManager(TreeNode selNode)
        {
            LayoutContainer lpage;

            if (selNode.Tag is LayoutContainer)
                lpage = new LayoutContainer() { Name = "DockManager_" + Guid.NewGuid().ToString().Replace("-", String.Empty), ClassName = "DockManager", DisplayName = "DockManager", ImageIndex = GetIconIndexFromClassName("DockManager"), ParentName = ((LayoutContainer)selNode.Tag).Name };
            else
                lpage = new LayoutContainer() { Name = "DockManager_" + Guid.NewGuid().ToString().Replace("-", String.Empty), ClassName = "DockManager", DisplayName = "DockManager", ImageIndex = GetIconIndexFromClassName("DockManager"), ParentName = string.Empty };

            TreeNode tabCtrlNode = new TreeNode().Nodes.Add(lpage.Name, lpage.DisplayName, GetIconIndexFromClassName(lpage.ClassName), GetIconIndexFromClassName(lpage.ClassName));
            tabCtrlNode.Tag = lpage;

            return tabCtrlNode;
        }
        #endregion

        #region TabControl Insert Routines
        private TreeNode CreateTabControlAndTabItem(TreeNode selNode)
        {
            LayoutContainer lpage;
            LayoutContainer lpageItem;

            if (!selNode.IsNull() && selNode.Tag is LayoutContainer)
            {
                lpage = new LayoutContainer() { Name = "TabControl_" + Guid.NewGuid().ToString().Replace("-", String.Empty), ClassName = "TabControl", DisplayName = "TabControl", ImageIndex = GetIconIndexFromClassName("TabControl"), ParentName = ((LayoutContainer)selNode.Tag).Name };
                lpageItem = new LayoutContainer() { Name = "TabItem_" + Guid.NewGuid().ToString().Replace("-", String.Empty), ClassName = "TabItem", DisplayName = "TabItem", ImageIndex = GetIconIndexFromClassName("TabItem"), ParentName = ((LayoutContainer)selNode.Tag).Name };
            }
            else
            {
                lpage = new LayoutContainer() { Name = "TabControl_" + Guid.NewGuid().ToString().Replace("-", String.Empty), ClassName = "TabControl", DisplayName = "TabControl", ImageIndex = GetIconIndexFromClassName("TabControl"), ParentName = String.Empty };
                lpageItem = new LayoutContainer() { Name = "TabItem_" + Guid.NewGuid().ToString().Replace("-", String.Empty), ClassName = "TabItem", DisplayName = "TabItem", ImageIndex = GetIconIndexFromClassName("TabItem"), ParentName = String.Empty };
            }

            TreeNode tabCtrlNode = GetNodesOrRootNodes(selNode).Add(lpage.Name, lpage.DisplayName, GetIconIndexFromClassName(lpage.ClassName), GetIconIndexFromClassName(lpage.ClassName));
            tabCtrlNode.Tag = lpage;
            TreeNode tabNode = tabCtrlNode.Nodes.Add(lpageItem.Name, lpageItem.DisplayName, GetIconIndexFromClassName(lpageItem.ClassName), GetIconIndexFromClassName(lpageItem.ClassName));
            tabNode.Tag = lpageItem;
            lpage.Controls.Add(lpageItem);

            if (selNode != null && (selNode.Tag is LayoutContainer))
            {
                ((LayoutContainer)selNode.Tag).Controls.Add(lpage);
                lpage.ParentName = ((LayoutContainer)selNode.Tag).Name;
            }

            return tabNode;
        }

        private TreeNodeCollection GetNodesOrRootNodes(TreeNode selectedNode)
        {
            if (selectedNode.IsNull())
                return this.treeTopGroups.Nodes;
            else
                return selectedNode.Nodes;
        }

        private TreeNode CreateTabItem(TreeNode selNode)
        {
            LayoutContainer lpageItem = new LayoutContainer();

            if (selNode.Tag is LayoutContainer)
                lpageItem = new LayoutContainer() { Name = "TabItem_" + Guid.NewGuid().ToString().Replace("-", String.Empty), ClassName = "TabItem", DisplayName = "TabItem", ImageIndex = GetIconIndexFromClassName("TabItem"), ParentName = ((LayoutContainer)selNode.Tag).Name };
            else
                lpageItem = new LayoutContainer() { Name = "TabItem_" + Guid.NewGuid().ToString().Replace("-", String.Empty), ClassName = "TabItem", DisplayName = "TabItem", ImageIndex = GetIconIndexFromClassName("TabItem"), ParentName = String.Empty };

            TreeNode tabNode = new TreeNode().Nodes.Add(lpageItem.DisplayName, lpageItem.DisplayName, GetIconIndexFromClassName(lpageItem.ClassName), GetIconIndexFromClassName(lpageItem.ClassName));
            tabNode.Tag = lpageItem;

            if (selNode != null && (selNode.Tag is LayoutContainer))
            {
                ((LayoutContainer)selNode.Tag).Controls.Add(lpageItem);
                lpageItem.ParentName = ((LayoutContainer)selNode.Tag).Name;
            }

            return tabNode;
        }

        private TreeNode CreateTabControl(TreeNode selNode)
        {
            LayoutContainer lpage;

            if (selNode.Tag is LayoutContainer)
                lpage = new LayoutContainer() { Name = "TabControl_" + Guid.NewGuid().ToString().Replace("-", String.Empty), ClassName = "TabControl", DisplayName = "TabControl", ImageIndex = GetIconIndexFromClassName("TabControl"), ParentName = ((LayoutContainer)selNode.Tag).Name };
            else
                lpage = new LayoutContainer() { Name = "TabControl_" + Guid.NewGuid().ToString().Replace("-", String.Empty), ClassName = "TabControl", DisplayName = "TabControl", ImageIndex = GetIconIndexFromClassName("TabControl"), ParentName = string.Empty };

            TreeNode tabCtrlNode = new TreeNode().Nodes.Add(lpage.Name, lpage.DisplayName, GetIconIndexFromClassName(lpage.ClassName), GetIconIndexFromClassName(lpage.ClassName));
            tabCtrlNode.Tag = lpage;

            return tabCtrlNode;
        }

        #endregion

        #region WizardControl Insert Routines

        private TreeNode CreateWizardControlAndWizardItem(TreeNode selNode)
        {
            LayoutContainer lpage;
            LayoutContainer lpageItem;

            if (!selNode.IsNull() && selNode.Tag is LayoutContainer)
            {
                lpage = new LayoutContainer() { Name = "WizardControl_" + Guid.NewGuid().ToString().Replace("-", String.Empty), ClassName = "WizardControl", DisplayName = "WizardControl", ImageIndex = GetIconIndexFromClassName("WizardControl"), ParentName = ((LayoutContainer)selNode.Tag).Name };
                lpageItem = new LayoutContainer() { Name = "WizardItem_" + Guid.NewGuid().ToString().Replace("-", String.Empty), ClassName = "WizardItem", DisplayName = "WizardItem", ImageIndex = GetIconIndexFromClassName("WizardItem"), ParentName = ((LayoutContainer)selNode.Tag).Name };
            }
            else
            {
                lpage = new LayoutContainer() { Name = "WizardControl_" + Guid.NewGuid().ToString().Replace("-", String.Empty), ClassName = "WizardControl", DisplayName = "WizardControl", ImageIndex = GetIconIndexFromClassName("WizardControl"), ParentName = String.Empty };
                lpageItem = new LayoutContainer() { Name = "WizardItem_" + Guid.NewGuid().ToString().Replace("-", String.Empty), ClassName = "WizardItem", DisplayName = "WizardItem", ImageIndex = GetIconIndexFromClassName("WizardItem"), ParentName = String.Empty };
            }

            TreeNode WizardCtrlNode = GetNodesOrRootNodes(selNode).Add(lpage.Name, lpage.DisplayName, GetIconIndexFromClassName(lpage.ClassName), GetIconIndexFromClassName(lpage.ClassName));
            WizardCtrlNode.Tag = lpage;
            TreeNode WizardNode = WizardCtrlNode.Nodes.Add(lpageItem.Name, lpageItem.DisplayName, GetIconIndexFromClassName(lpageItem.ClassName), GetIconIndexFromClassName(lpageItem.ClassName));
            WizardNode.Tag = lpageItem;
            lpage.Controls.Add(lpageItem);

            if (!selNode.IsNull() && (selNode.Tag is LayoutContainer))
            {
                ((LayoutContainer)selNode.Tag).Controls.Add(lpage);
                lpage.ParentName = ((LayoutContainer)selNode.Tag).Name;
            }

            return WizardNode;
        }

        private TreeNode CreateWizardItem(TreeNode selNode)
        {
            LayoutContainer lpageItem = new LayoutContainer();

            if (selNode.Tag is LayoutContainer)
                lpageItem = new LayoutContainer() { Name = "WizardItem_" + Guid.NewGuid().ToString().Replace("-", String.Empty), ClassName = "WizardItem", DisplayName = "WizardItem", ImageIndex = GetIconIndexFromClassName("WizardItem"), ParentName = ((LayoutContainer)selNode.Tag).Name };
            else
                lpageItem = new LayoutContainer() { Name = "WizardItem_" + Guid.NewGuid().ToString().Replace("-", String.Empty), ClassName = "WizardItem", DisplayName = "WizardItem", ImageIndex = GetIconIndexFromClassName("WizardItem"), ParentName = String.Empty };

            TreeNode tabNode = new TreeNode().Nodes.Add(lpageItem.DisplayName, lpageItem.DisplayName, GetIconIndexFromClassName(lpageItem.ClassName), GetIconIndexFromClassName(lpageItem.ClassName));
            tabNode.Tag = lpageItem;

            if (selNode != null && (selNode.Tag is LayoutContainer))
            {
                ((LayoutContainer)selNode.Tag).Controls.Add(lpageItem);
                lpageItem.ParentName = ((LayoutContainer)selNode.Tag).Name;
            }

            return tabNode;
        }

        private TreeNode CreateWizardControl(TreeNode selNode)
        {
            LayoutContainer lpage;

            if (!selNode.IsNull() && selNode.Tag is LayoutContainer)
                lpage = new LayoutContainer() { Name = "Wizard_" + Guid.NewGuid().ToString().Replace("-", String.Empty), ClassName = "WizardControl", DisplayName = "Wizard", ImageIndex = GetIconIndexFromClassName("WizardControl"), ParentName = ((LayoutContainer)selNode.Tag).Name };
            else
                lpage = new LayoutContainer() { Name = "Wizard_" + Guid.NewGuid().ToString().Replace("-", String.Empty), ClassName = "WizardControl", DisplayName = "Wizard", ImageIndex = GetIconIndexFromClassName("WizardControl"), ParentName = string.Empty };

            TreeNode tabCtrlNode = new TreeNode().Nodes.Add(lpage.Name, lpage.DisplayName, GetIconIndexFromClassName(lpage.ClassName), GetIconIndexFromClassName(lpage.ClassName));
            tabCtrlNode.Tag = lpage;

            return tabCtrlNode;
        }

        #endregion

        private void tlbbtnAddControls_Click(object sender, EventArgs e)
        {
            PrepareMenu(treeTopGroups.SelectedNode);
        }

        private void mnuExpander_Click(object sender, EventArgs e)
        {
            this.AddTreeContainer("Expander");
        }

        private void AddTreeContainer(string className)
        {
            TreeNode selNode = this.treeTopGroups.SelectedNode;
            TreeNodeCollection nodes = (selNode == null ? this.treeTopGroups.Nodes : selNode.Nodes);

            LayoutContainer lGroup = new LayoutContainer() { Name = className + "_" + Guid.NewGuid().ToString().Replace("-", String.Empty), DisplayName = "New Group", ImageIndex = GetIconIndexFromClassName(className), ColumnCount = 2, ClassName = className };

            if (className == "PivotDrillDownChart")
                lGroup.PivotChartType = "ColumnSeries";

            if (selNode != null && !(selNode.Tag is LayoutContainer))
            {
                MessageBox.Show("The selected element is not a container.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (selNode != null && (selNode.Tag is LayoutContainer))
            {
                ((LayoutContainer)selNode.Tag).Controls.Add(lGroup);
                lGroup.ParentName = ((LayoutContainer)selNode.Tag).Name;
            }
            else
            {
                if (this.layoutDefinition != null)
                    this.layoutDefinition.Containers.Add(lGroup);
            }

            this.treeTopGroups.SelectedNode = nodes.Add(lGroup.Name, lGroup.DisplayName, lGroup.ImageIndex, lGroup.ImageIndex);

            this.treeTopGroups.SelectedNode.Tag = lGroup;
            this.treeTopGroups.Select();
            this.treeTopGroups.Invalidate();
        }

        private void treeTopGroups_MouseDown(object sender, MouseEventArgs e)
        {
            Point pt = new Point(e.X, e.Y);
            TreeNode targetNode = treeTopGroups.GetNodeAt(pt);

            if (targetNode == null)
                return;

            if (targetNode.Tag is LayoutControlV2)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left && Control.ModifierKeys == Keys.Control)
                {
                    if (!listSelectedNodes.Contains(targetNode))
                        listSelectedNodes.Add(targetNode);
                    NodeSelectionApperance(targetNode, true);
                }
                else if (e.Button == System.Windows.Forms.MouseButtons.Left && Control.ModifierKeys == Keys.Shift)
                {
                    SelectNodeWithShift(targetNode);
                }
            }

        }

        private void SelectNodeWithShift(TreeNode targetNode)
        {
            TreeNode firstNode = treeTopGroups.SelectedNode;
            int FirstIndex = 0, LastIndex = 0;
            if (firstNode.Parent == targetNode.Parent)
            {
                FirstIndex = (firstNode.Index > targetNode.Index ? targetNode.Index : firstNode.Index);
                LastIndex = (firstNode.Index > targetNode.Index ? firstNode.Index : targetNode.Index);

                DeSelectAllNodes(listSelectedNodes);
                listSelectedNodes.Clear();
                for (int i = FirstIndex; i < LastIndex + 1; i++)
                {
                    listSelectedNodes.Add(firstNode.Parent.Nodes[i]);
                    NodeSelectionApperance(firstNode.Parent.Nodes[i], true);
                }
            }
            else
            {
                DeSelectAllNodes(listSelectedNodes);
                listSelectedNodes.Clear();
            }
        }

        private void treeTopGroups_MouseUp(object sender, MouseEventArgs e)
        {
            Point pt = new Point(e.X, e.Y);
            TreeNode targetNode = treeTopGroups.GetNodeAt(pt);

            if (targetNode == null)
                return;

            if (e.Button == System.Windows.Forms.MouseButtons.Left && (Control.ModifierKeys != Keys.Control && Control.ModifierKeys != Keys.Shift))
            {
                DeSelectAllNodes(listSelectedNodes);
                listSelectedNodes.Clear();
                listSelectedNodes.Add(targetNode);
                NodeSelectionApperance(targetNode, true);
            }
        }

        private bool IsMultiplesSelect
        { get { return ((listSelectedNodes.Count > 1)); } }

        private void mnuConvertToTabControl_Click(object sender, EventArgs e)
        {
            ExpanderToTabControl(treeTopGroups.SelectedNode);
        }

        private void treeTopGroups_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                e.Handled = true;
                tlbbtnRemoveControls.PerformClick();
            }
        }

        private void cboEntity_SelectedIndexChanged(object sender, EventArgs e)
        {
            LayoutControlV2 lc = ((LayoutControlV2)selectedTree.SelectedNode.Tag);
            lblBindingText.Text = cboEntity.Text;
            lc.BindingPath = cboEntity.Text;

            LoadAdapterProperties(lc.BindingPath);
        }

        private void LoadAdapterProperties(string bindingPath)
        {
            if (bindingPath.IsNullOrEmpty())
                return;

            List<PublicationProperty> properties;
            Dictionary<string, string> fields = new Dictionary<string, string>();

            if (bindingPath.Contains("PagedList"))
                properties = currentLayout.GetEntityAdapter().GetDetailByName(bindingPath.Right(".").Replace("PagedList", "")).Properties;
            else
                properties = currentLayout.GetEntityAdapter().Properties;

            foreach (var property in properties)
            {
                fields.Add(property.Name, property.DisplayName);
            }

            //igniteUIChart1.AdapterFields = fields;
            telerikUIChart1.AdapterFields = fields;
        }

        private void txtMask_TextChanged(object sender, EventArgs e)
        {
            if (tabInformations.SelectedTab == tabMaskedTextBox)
                ((LayoutControlV2)selectedTree.SelectedNode.Tag).Mask = this.txtMask.Text;
        }

        private void txtCulture_TextChanged(object sender, EventArgs e)
        {
            if (tabInformations.SelectedTab == tabMaskedTextBox)
                ((LayoutControlV2)selectedTree.SelectedNode.Tag).MaskCulture = this.txtCulture.Text;
        }

        private void mnuContentControl_Click(object sender, EventArgs e)
        {
            LayoutContainer lctrl;
            if (treeTopGroups.SelectedNode.Tag is LayoutContainer)
                lctrl = treeTopGroups.SelectedNode.Tag as LayoutContainer;
            else
                return;

            lctrl.ClassName = "CustomContainer";
            treeTopGroups.SelectedNode.ImageIndex = GetIconIndexFromClassName("CustomContainer");
            treeTopGroups.SelectedNode.SelectedImageIndex = GetIconIndexFromClassName("CustomContainer");

            UpdateDataLayout(treeTopGroups);
        }

        private void txDataGridOrder_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox && !selectedTree.IsNull() && !selectedTree.SelectedNode.IsNull())
            {
                if (selectedTree.SelectedNode.Tag is LayoutControlV2)
                    ((LayoutControlV2)selectedTree.SelectedNode.Tag).DataGridOrder = ((TextBox)sender).Text;

                this.selectedTree.Invalidate();
            }
        }

        private void mnuConvertToDatagrid_Click(object sender, EventArgs e)
        {
            ConvertToElement(treeTopGroups.SelectedNode, "DataGrid");
        }

        private void convertToFlatPivotGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConvertToElement(treeTopGroups.SelectedNode, "FlatPivotGrid");
            CheckMeasures(treeTopGroups.SelectedNode);
        }

        private void CheckMeasures(TreeNode expandTreeNode)
        {
            if (!this.currentLayout.HasOlapSource())
            {
                LayoutContainer lctrl;
                if (expandTreeNode.Tag != null && expandTreeNode.Tag is LayoutContainer && ((LayoutContainer)expandTreeNode.Tag).ClassName == "FlatPivotGrid")
                    lctrl = expandTreeNode.Tag as LayoutContainer;
                else
                    return;

                foreach (var control in lctrl.Controls)
                {
                    if (control is LayoutControlV2)
                        ((LayoutControlV2)control).IsMeasure = IsMeasure(((LayoutControlV2)control).BindingPath);
                }
            }
        }

        private bool IsMeasure(string bindingPath)
        {
            if (!bindingPath.IsNullOrEmpty() && this.CurrentLayout != null && this.CurrentLayout.EntityAdapter != null)
            {
                bindingPath = bindingPath.Replace("DataElement.DataView.", this.CurrentLayout.EntityAdapter.Name + ".").Replace("PagedList.", ".");
                string[] parts = bindingPath.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length > 1)
                {
                    var entity = this.CurrentLayout.EntityAdapter.EntityAdapterDesignerRoot.EntityAdapters.Where(e => e.Name == parts[parts.Length - 2]).FirstOrDefault();
                    if (entity != null)
                    {
                        var item = entity.GetAllInheritanceAttributes().Where(e => e.Name == parts[parts.Length - 1]).FirstOrDefault();
                        if (item != null && !item.IsFK && !item.IsPK && (item.Datatype.ToLower().Contains("int") || item.Datatype.ToLower().Contains("long") || item.Datatype.ToLower().Contains("short") || item.Datatype.ToLower().Contains("decimal") || item.Datatype.ToLower().Contains("float") || item.Datatype.ToLower().Contains("double") || item.Datatype.ToLower().Contains("byte")))
                            return true;
                    }
                }

            }
            return false;
        }

        private void DataGridToTreeListView(TreeNode expandTreeNode)
        {
            LayoutContainer lctrl;
            if (expandTreeNode.Tag is LayoutContainer)
                lctrl = expandTreeNode.Tag as LayoutContainer;
            else
                return;

            lctrl.ClassName = "TreeListView";
            lctrl.ImageIndex = GetIconIndexFromClassName("TreeListView");
            lctrl.DisplayName = "TreeListView";

            expandTreeNode.ImageIndex = lctrl.ImageIndex;
            expandTreeNode.SelectedImageIndex = lctrl.ImageIndex;
            expandTreeNode.Text = lctrl.ClassName;

            UpdateDataLayout(treeTopGroups);

        }

        private void ConvertToElement(TreeNode expandTreeNode, string elementName)
        {
            LayoutContainer lctrl;
            if (expandTreeNode.Tag is LayoutContainer)
                lctrl = expandTreeNode.Tag as LayoutContainer;
            else
                return;

            lctrl.ClassName = elementName;
            lctrl.ImageIndex = GetIconIndexFromClassName(elementName);
            lctrl.DisplayName = elementName;

            expandTreeNode.ImageIndex = lctrl.ImageIndex;
            expandTreeNode.SelectedImageIndex = lctrl.ImageIndex;
            expandTreeNode.Text = lctrl.ClassName;

            tree_AfterSelect(this.treeTopGroups, null);
            UpdateDataLayout(treeTopGroups);
        }

        private void mnuConvertToTreeListView_Click(object sender, EventArgs e)
        {
            DataGridToTreeListView(treeTopGroups.SelectedNode);
        }

        private void txtIdNameTreeList_TextChanged(object sender, EventArgs e)
        {
            LayoutContainer lc = ((LayoutContainer)selectedTree.SelectedNode.Tag);
            lc.IdNameTreeListView = ((TextBox)sender).Text;
        }

        private void txtIdParentNameTreeList_TextChanged(object sender, EventArgs e)
        {
            LayoutContainer lc = ((LayoutContainer)selectedTree.SelectedNode.Tag);
            lc.IdParentNameTreeListView = ((TextBox)sender).Text;
        }

        private void lblInternalName_Leave(object sender, EventArgs e)
        {
            RenameSelectElement(sender);
        }

        private void RenameSelectElement(object sender)
        {
            LayoutElement lc = ((LayoutElement)selectedTree.SelectedNode.Tag);
            lc.DefinedUserName = ((TextBox)sender).Text;
        }

        private void mnuConvertToWizard_Click(object sender, EventArgs e)
        {
            ExpanderToWizard(treeTopGroups.SelectedNode);
        }

        private void mnuWizardItem_Click(object sender, EventArgs e)
        {
            TreeNode selNode = treeTopGroups.SelectedNode;

            if (!selNode.IsNull() && selNode.Tag is LayoutContainer)
            {
                if (((LayoutContainer)selNode.Tag).ClassName == "WizardItem")
                {
                    selNode.Parent.Nodes.Add(CreateWizardItem(selNode.Parent));
                    return;
                }
                else
                    if (((LayoutContainer)selNode.Tag).ClassName == "WizardControl")
                    {
                        selNode.Nodes.Add(CreateWizardItem(selNode));
                        return;
                    }

            }

            CreateWizardControlAndWizardItem(!selNode.IsNull() && selNode.Parent != null && selNode.Parent.Tag is LayoutContainer ? selNode.Parent : null);
        }

        private void mnuConvertToExpander_Click(object sender, EventArgs e)
        {
            TreeNode selNode = treeTopGroups.SelectedNode;
            if (selNode.IsNull())
                return;


            if (selNode.Tag is LayoutContainer)
            {
                if (((LayoutContainer)selNode.Tag).ClassName.InList("CustomContainer", "GroupBox"))
                {
                    ((LayoutContainer)selNode.Tag).ClassName = "Expander";
                    treeTopGroups.SelectedNode.ImageIndex = GetIconIndexFromClassName("Expander");
                    treeTopGroups.SelectedNode.SelectedImageIndex = GetIconIndexFromClassName("Expander");

                    UpdateDataLayout(treeTopGroups);
                }
            }
        }

        private void chkRemoveDatatToolbar_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void mnuAddContentControl_Click(object sender, EventArgs e)
        {
            this.AddTreeContainer("CustomContainer");
        }

        private void addOlapPivotGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.AddTreeContainer("OlapPivotGrid");
        }

        private void txtTooltip_TextChanged(object sender, EventArgs e)
        {
            LayoutControlV2 lc = ((LayoutControlV2)selectedTree.SelectedNode.Tag);
            lc.ToolTip = txtTooltip.Text;
        }

        private void btRemoveItem_Click(object sender, EventArgs e)
        {
            if (this.treeRemovedItems.SelectedNode.IsNullOrEmpty())
            {
                MessageBox.Show("There is no selected element!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Remove selected element?", "Element Removing", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (this.treeRemovedItems.SelectedNode.Tag is LayoutElement)
                {
                    this.layoutDefinition.DeleteRemovedContainerOrControlByName(((LayoutElement)this.treeRemovedItems.SelectedNode.Tag));
                    this.treeRemovedItems.Nodes.Remove(this.treeRemovedItems.SelectedNode);
                    this.treeRemovedItems.Invalidate();
                }
            }
        }

        private int IndexCut = -1;
        private int IndexCopy = -1;
        private TreeNode nodSourcePaste;
        private void mnuCut_Click(object sender, EventArgs e)
        {
            if (this.treeTopGroups.SelectedNode.IsNullOrEmpty())
            {
                MessageBox.Show("There is no selected element!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            IndexCopy = -1;
            IndexCut = treeTopGroups.SelectedNode.Index;
            mnuPaste.Enabled = true;
            nodSourcePaste = treeTopGroups.SelectedNode;
        }

        private void mnuPaste_Click(object sender, EventArgs e)
        {
            if (nodSourcePaste == null)
                return;

            //Adjust IsDerived
            if (nodSourcePaste.Tag is LayoutElement)
            {
                LayoutElement element = (LayoutElement)nodSourcePaste.Tag;
                element.IsDerived = false;
                SetColor(nodSourcePaste);
            }

            nodSourcePaste.Remove();
            IndexCut = -1;
            IndexCopy = -1;
            treeTopGroups.SelectedNode.Nodes.Add(nodSourcePaste);
            UpdateDataLayout(treeTopGroups);
            mnuPaste.Enabled = false;
            this.CheckMeasures(treeTopGroups.SelectedNode);
        }

        private void mnuCopy_Click(object sender, EventArgs e)
        {
            if (this.treeTopGroups.SelectedNode.IsNullOrEmpty())
            {
                MessageBox.Show("There is no selected element!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            IndexCut = -1;
            IndexCopy = treeTopGroups.SelectedNode.Index;
            nodSourcePaste = CopyNode(treeTopGroups.SelectedNode);

            mnuPaste.Enabled = true;
        }

        private TreeNode CopyNode(TreeNode treeNodeSource)
        {
            TreeNode copied = new TreeNode();
            copied.Tag = CopyNodeTag(treeNodeSource);
            copied.Name = ((LayoutElement)copied.Tag).Name;
            copied.ImageIndex = treeNodeSource.ImageIndex;
            copied.Text = treeNodeSource.Text;
            copied.SelectedImageIndex = treeNodeSource.SelectedImageIndex;

            foreach (TreeNode child in treeNodeSource.Nodes)
                copied.Nodes.Add(CopyNode(child));


            return copied;
        }

        private Dictionary<int, TreeNode> nodMatches;
        private int nodCurrentIndex = -1;
        private void FindNameInTree(TreeNode tn, String desc)
        {
            foreach (TreeNode item in tn.Nodes)
            {
                if (item.Name.ToLower().Trim().Replace(" ", "").Contains(desc.ToLower().Trim().Replace(" ", ""))
                    || item.Text.ToLower().Trim().Replace(" ", "").Contains(desc.ToLower().Trim().Replace(" ", ""))
                    || item.ToolTipText.ToLower().Trim().Replace(" ", "").Contains(desc.ToLower().Trim().Replace(" ", "")))
                {
                    nodCurrentIndex++;
                    nodMatches.Add(nodCurrentIndex, item);
                }
                if (item.Nodes.Count > 0)
                    FindNameInTree(item, desc);

            }
        }


        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtSearch.Text.Trim().Length > 0)
                {
                    e.Handled = true;

                    nodCurrentIndex = -1;
                    nodMatches = new Dictionary<int, TreeNode>();

                    foreach (TreeNode item in treeTopGroups.Nodes)
                        FindNameInTree(item, txtSearch.Text);

                    if (nodMatches.Count > 1)
                        lblMatches.Text = nodMatches.Count.ToString() + " found";
                    else if (nodMatches.Count == 1)
                        lblMatches.Text = nodMatches.Count.ToString() + " found";
                    else if (nodMatches.Count < 1)
                        lblMatches.Text = "No matches";

                    if (nodMatches.Count > 0)
                    {
                        nodCurrentIndex = -1;

                        btnNext.Enabled = true;
                        btnPrevious.Enabled = false;
                    }
                    else
                    {
                        nodCurrentIndex = -1;

                        btnNext.Enabled = false;
                        btnPrevious.Enabled = false;
                    }
                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            nodCurrentIndex++;

            if (nodMatches.Keys.Contains(nodCurrentIndex))
            {
                DeSelectAllNodes(new List<TreeNode>() { treeTopGroups.SelectedNode });
                treeTopGroups.SelectedNode = nodMatches[nodCurrentIndex];
                treeTopGroups.SelectedNode.EnsureVisible();
                NodeSelectionApperance(treeTopGroups.SelectedNode, true);
                lblMatches.Text = String.Format("{0} of {1} found", nodCurrentIndex + 1, nodMatches.Count.ToString());
                btnPrevious.Enabled = true;
                treeTopGroups.Focus();
            }
            if (!nodMatches.Keys.Contains(nodCurrentIndex + 1))
            {
                btnNext.Enabled = false;
            }

        }

        private void MoveNode(TreeNode nod, int position)
        {
            int iNod = nod.Index + position;

            TreeNode nodParent = nod.Parent;
            if (nodParent == null)
            {
                nod.Remove();
                treeTopGroups.Nodes.Insert(iNod, nod);
                treeTopGroups.SelectedNode = nod;
                NodeSelectionApperance(treeTopGroups.SelectedNode, true);
                treeTopGroups.Focus();
                UpdateDataLayout(treeTopGroups);
                return;
            }


            if (nodParent.Nodes.Count == iNod || iNod < 0)
                return;

            nod.Remove();
            nodParent.Nodes.Insert(iNod, nod);
            treeTopGroups.SelectedNode = nod;
            NodeSelectionApperance(treeTopGroups.SelectedNode, true);
            treeTopGroups.Focus();
            UpdateDataLayout(treeTopGroups);

        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            nodCurrentIndex--;

            if (nodMatches.Keys.Contains(nodCurrentIndex))
            {
                DeSelectAllNodes(new List<TreeNode>() { treeTopGroups.SelectedNode });
                treeTopGroups.SelectedNode = nodMatches[nodCurrentIndex];
                treeTopGroups.SelectedNode.EnsureVisible();
                NodeSelectionApperance(treeTopGroups.SelectedNode, true);
                lblMatches.Text = String.Format("{0} of {1} found", nodCurrentIndex + 1, nodMatches.Count.ToString());
                btnNext.Enabled = true;
                treeTopGroups.Focus();
            }
            if (!nodMatches.Keys.Contains(nodCurrentIndex - 1))
            {
                btnPrevious.Enabled = false;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            nodMatches = new Dictionary<int, TreeNode>();
            nodCurrentIndex = -1;
            btnNext.Enabled = false;
            btnPrevious.Enabled = false;
            lblMatches.Text = "No matches";

        }

        private void mnuMoveUp_Click(object sender, EventArgs e)
        {
            if (treeTopGroups.SelectedNode != null)
            {
                MoveNode(treeTopGroups.SelectedNode, -1);
            }
        }

        private void mnuMoveDown_Click(object sender, EventArgs e)
        {
            if (treeTopGroups.SelectedNode != null)
            {
                MoveNode(treeTopGroups.SelectedNode, 1);
            }
        }

        private void tlbMoveUp_Click(object sender, EventArgs e)
        {
            if (treeTopGroups.SelectedNode != null)
            {
                MoveNode(treeTopGroups.SelectedNode, -1);
            }
        }

        private void tlbMoveDown_Click(object sender, EventArgs e)
        {
            if (treeTopGroups.SelectedNode != null)
            {
                MoveNode(treeTopGroups.SelectedNode, 1);
            }
        }

        private void chkRadialBar_CheckedChanged(object sender, EventArgs e)
        {
            LayoutControlV2 lc = ((LayoutControlV2)selectedTree.SelectedNode.Tag);
            lc.RadialCheck = ((CheckBox)sender).Checked;
        }

        private void chkStateIndicator_CheckedChanged(object sender, EventArgs e)
        {
            LayoutControlV2 lc = ((LayoutControlV2)selectedTree.SelectedNode.Tag);
            lc.StateIndicatorCheck = ((CheckBox)sender).Checked;
        }

        private void chkNeedle_CheckedChanged(object sender, EventArgs e)
        {
            LayoutControlV2 lc = ((LayoutControlV2)selectedTree.SelectedNode.Tag);
            lc.NeedleCheck = ((CheckBox)sender).Checked;
        }

        private void txtRadialBarName_TextChanged(object sender, EventArgs e)
        {
            LayoutControlV2 lc = ((LayoutControlV2)selectedTree.SelectedNode.Tag);
            lc.GaugeNeedleName = ((TextBox)sender).Text;
        }

        private void txtStateIndicatorName_TextChanged(object sender, EventArgs e)
        {
            LayoutControlV2 lc = ((LayoutControlV2)selectedTree.SelectedNode.Tag);
            lc.GaugeStateIndicatorName = ((TextBox)sender).Text;
        }

        private void txtNeedleName_TextChanged(object sender, EventArgs e)
        {
            LayoutControlV2 lc = ((LayoutControlV2)selectedTree.SelectedNode.Tag);
            lc.GaugeNeedleName = ((TextBox)sender).Text;
        }

        private void chkLinearScale_CheckedChanged(object sender, EventArgs e)
        {
            LayoutControlV2 lc = ((LayoutControlV2)selectedTree.SelectedNode.Tag);
            lc.LinearScaleCheck = ((CheckBox)sender).Checked;
        }

        private void chkLinearBar_CheckedChanged(object sender, EventArgs e)
        {
            LayoutControlV2 lc = ((LayoutControlV2)selectedTree.SelectedNode.Tag);
            lc.LinearBarCheck = ((CheckBox)sender).Checked;
        }

        private void chkMarker_CheckedChanged(object sender, EventArgs e)
        {
            LayoutControlV2 lc = ((LayoutControlV2)selectedTree.SelectedNode.Tag);
            lc.MarkerCheck = ((CheckBox)sender).Checked;
        }

        private void chkRadialScale_CheckedChanged(object sender, EventArgs e)
        {
            LayoutControlV2 lc = ((LayoutControlV2)selectedTree.SelectedNode.Tag);
            lc.RadialScaleCheck = ((CheckBox)sender).Checked;
        }

        private void txtRadialScaleName_TextChanged(object sender, EventArgs e)
        {
            LayoutControlV2 lc = ((LayoutControlV2)selectedTree.SelectedNode.Tag);
            lc.GaugeRadialScaleName = ((TextBox)sender).Text;
        }

        private void txtMarkerName_TextChanged(object sender, EventArgs e)
        {
            LayoutControlV2 lc = ((LayoutControlV2)selectedTree.SelectedNode.Tag);
            lc.GaugeMarkerName = ((TextBox)sender).Text;
        }

        private void txtLinearBarName_TextChanged(object sender, EventArgs e)
        {
            LayoutControlV2 lc = ((LayoutControlV2)selectedTree.SelectedNode.Tag);
            lc.GaugeLinearBarName = ((TextBox)sender).Text;
        }

        private void txtLinearScaleName_TextChanged(object sender, EventArgs e)
        {
            LayoutControlV2 lc = ((LayoutControlV2)selectedTree.SelectedNode.Tag);
            lc.GaugeLinearScaleName = ((TextBox)sender).Text;
        }

        private void txtStringFormat_TextChanged(object sender, EventArgs e)
        {
            LayoutControlV2 lc = ((LayoutControlV2)selectedTree.SelectedNode.Tag);
            lc.DataFormatString = ((TextBox)sender).Text;
        }

        private void txtPrecision_TextChanged(object sender, EventArgs e)
        {
            LayoutControlV2 lc = ((LayoutControlV2)selectedTree.SelectedNode.Tag);
            lc.Precision = ((TextBox)sender).Text;
        }

        private void mnuConvertToGroupBox_Click(object sender, EventArgs e)
        {

            TreeNode selNode = treeTopGroups.SelectedNode;
            if (selNode.IsNull())
                return;

            if (selNode.Tag is LayoutContainer)
            {
                if (((LayoutContainer)selNode.Tag).ClassName.InList("CustomContainer", "Expander"))
                {
                    ((LayoutContainer)selNode.Tag).ClassName = "GroupBox";
                    treeTopGroups.SelectedNode.ImageIndex = GetIconIndexFromClassName("GroupBox");
                    treeTopGroups.SelectedNode.SelectedImageIndex = GetIconIndexFromClassName("GroupBox");

                    UpdateDataLayout(treeTopGroups);
                }
            }
        }


        private void mnuGroupBox_Click(object sender, EventArgs e)
        {
            this.AddTreeContainer("GroupBox");
        }

        private void ckIsMeasure_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox && !selectedTree.IsNull() && !selectedTree.SelectedNode.IsNull())
            {
                if (selectedTree.SelectedNode.Tag is LayoutControlV2)
                    ((LayoutControlV2)selectedTree.SelectedNode.Tag).IsMeasure = ((CheckBox)sender).Checked;
            }
        }

        private void txPivotColumns_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox && !selectedTree.IsNull() && !selectedTree.SelectedNode.IsNull())
            {
                if (selectedTree.SelectedNode.Tag is LayoutContainer)
                    ((LayoutContainer)selectedTree.SelectedNode.Tag).PivotColumns = ((TextBox)sender).Text;
            }
        }

        private void txPivotRows_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox && !selectedTree.IsNull() && !selectedTree.SelectedNode.IsNull())
            {
                if (selectedTree.SelectedNode.Tag is LayoutContainer)
                    ((LayoutContainer)selectedTree.SelectedNode.Tag).PivotRows = ((TextBox)sender).Text;
            }
        }

        private void txPivotFilters_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox && !selectedTree.IsNull() && !selectedTree.SelectedNode.IsNull())
            {
                if (selectedTree.SelectedNode.Tag is LayoutContainer)
                    ((LayoutContainer)selectedTree.SelectedNode.Tag).PivotFilters = ((TextBox)sender).Text;
            }
        }

        private void txPivotMeasures_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox && !selectedTree.IsNull() && !selectedTree.SelectedNode.IsNull())
            {
                if (selectedTree.SelectedNode.Tag is LayoutContainer)
                    ((LayoutContainer)selectedTree.SelectedNode.Tag).PivotMeasures = ((TextBox)sender).Text;
            }
        }

        private void txPivotCubeName_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox && !selectedTree.IsNull() && !selectedTree.SelectedNode.IsNull())
            {
                if (selectedTree.SelectedNode.Tag is LayoutContainer)
                    ((LayoutContainer)selectedTree.SelectedNode.Tag).PivotCube = ((TextBox)sender).Text;
            }
        }

        private void checkEnableMedias_CheckedChanged(object sender, EventArgs e)
        {
            if (this.layoutDefinition != null)
                this.layoutDefinition.EnableMedias = checkEnableMedias.Checked;
        }


        private void addPivotChartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.AddTreeContainer("PivotChart");
        }


        private void cmbPivotChartType_SelectedValueChanged(object sender, EventArgs e)
        {
            if (sender is ComboBox)
            {
                LayoutContainer lc = ((LayoutContainer)selectedTree.SelectedNode.Tag);
                lc.PivotChartType = ((ComboBox)sender).SelectedItem as string;
            }
        }

        private void txPivotGridName_TextChanged(object sender, EventArgs e)
        {
            LayoutContainer lc = ((LayoutContainer)selectedTree.SelectedNode.Tag);
            lc.PivotGridName = this.txPivotGridName.Text;
        }

        private void cmbOlapAxisSource_SelectedValueChanged(object sender, EventArgs e)
        {
            LayoutContainer lc = ((LayoutContainer)selectedTree.SelectedNode.Tag);
            lc.OlapAxisSource = this.cmbOlapAxisSource.SelectedItem as string;
        }

        private void txChartDimensions_TextChanged(object sender, EventArgs e)
        {
            LayoutContainer lc = ((LayoutContainer)selectedTree.SelectedNode.Tag);
            lc.ChartDimensions = this.txChartDimensions.Text;
        }

        private void txChartMeasures_TextChanged(object sender, EventArgs e)
        {
            LayoutContainer lc = ((LayoutContainer)selectedTree.SelectedNode.Tag);
            lc.ChartMeasures = this.txChartMeasures.Text;
        }

        private void addPivotDrillDownChartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.AddTreeContainer("PivotDrillDownChart");
        }

        private void txMeasureGroup_TextChanged(object sender, EventArgs e)
        {
            LayoutControlV2 lc = ((LayoutControlV2)selectedTree.SelectedNode.Tag);
            lc.Group = ((TextBox)sender).Text;
        }

        private void addFlatPivotGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.AddTreeContainer("FlatPivotGrid");
        }

        private void txMeasureFormula_TextChanged(object sender, EventArgs e)
        {
            LayoutControlV2 lc = ((LayoutControlV2)selectedTree.SelectedNode.Tag);
            lc.MeasureFormula = ((TextBox)sender).Text;
        }

        private void ckIsPivotExpanded_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox && !selectedTree.IsNull() && !selectedTree.SelectedNode.IsNull())
            {
                if (selectedTree.SelectedNode.Tag is LayoutContainer)
                    ((LayoutContainer)selectedTree.SelectedNode.Tag).IsPivotExpanded = ((CheckBox)sender).Checked;
            }
        }

        private void ckIsPivotReadOnly_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox && !selectedTree.IsNull() && !selectedTree.SelectedNode.IsNull())
            {
                if (selectedTree.SelectedNode.Tag is LayoutContainer)
                    ((LayoutContainer)selectedTree.SelectedNode.Tag).IsPivotReadOnly = ((CheckBox)sender).Checked;
            }
        }

        private void ckParentInFrontForColumns_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox && !selectedTree.IsNull() && !selectedTree.SelectedNode.IsNull())
            {
                if (selectedTree.SelectedNode.Tag is LayoutContainer)
                    ((LayoutContainer)selectedTree.SelectedNode.Tag).ParentInFrontForColumns = ((CheckBox)sender).Checked;
            }
        }

        private void ckParentInFrontForRows_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox && !selectedTree.IsNull() && !selectedTree.SelectedNode.IsNull())
            {
                if (selectedTree.SelectedNode.Tag is LayoutContainer)
                    ((LayoutContainer)selectedTree.SelectedNode.Tag).ParentInFrontForRows = ((CheckBox)sender).Checked;
            }
        }

        private void mnuDockItem_Click(object sender, EventArgs e)
        {
            TreeNode selNode = treeTopGroups.SelectedNode;
            if (selNode.IsNull())
                return;

            if (selNode.Tag is LayoutContainer)
            {
                if (((LayoutContainer)selNode.Tag).ClassName == "DockItem")
                {
                    selNode.Parent.Nodes.Add(CreateDockItem(selNode.Parent));
                    return;
                }
                else
                    if (((LayoutContainer)selNode.Tag).ClassName == "DockManager")
                    {
                        selNode.Nodes.Add(CreateDockItem(selNode));
                        return;
                    }

            }
            if (selNode.Parent != null)
                if (selNode.Parent.Tag is LayoutContainer)
                    CreateDockManagerAndDockItem(selNode.Parent);
        }

        private void mnuConvertToDockManager_Click(object sender, EventArgs e)
        {
            ExpanderToDockManager(treeTopGroups.SelectedNode);
        }

        private void txUserInterfaceName_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox && !selectedTree.IsNull() && !selectedTree.SelectedNode.IsNull())
            {
                if (selectedTree.SelectedNode.Tag is LayoutContainer)
                    ((LayoutContainer)selectedTree.SelectedNode.Tag).UserInterfaceName = ((TextBox)sender).Text;
            }
        }

        private void addExternalUIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.AddTreeContainer("ExternalUI");
        }

        private void txParentFieldsRelation_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox && !selectedTree.IsNull() && !selectedTree.SelectedNode.IsNull())
            {
                if (selectedTree.SelectedNode.Tag is LayoutContainer)
                    ((LayoutContainer)selectedTree.SelectedNode.Tag).ParentFieldsRelation = ((TextBox)sender).Text;
            }
        }

        private void txDetailFieldsRelation_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox && !selectedTree.IsNull() && !selectedTree.SelectedNode.IsNull())
            {
                if (selectedTree.SelectedNode.Tag is LayoutContainer)
                    ((LayoutContainer)selectedTree.SelectedNode.Tag).DetailFieldsRelation = ((TextBox)sender).Text;
            }
        }

        private void ckRemoveDataToolbar_CheckedChanged(object sender, EventArgs e)
        {
            if (this.layoutDefinition != null)
                this.layoutDefinition.RemoveDataToolbar = checkRemoveDataToolbar.Checked;
        }

        private void chkRemoveViewSwitch_CheckedChanged(object sender, EventArgs e)
        {
            if (this.grpExternalUI.Visible)
            {
                LayoutContainer lc = ((LayoutContainer)selectedTree.SelectedNode.Tag);
                lc.RemoveViewSwitch = ((CheckBox)sender).Checked;
            }
        }

        private void chkRemoveToolbarGrid_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkRemoveToolbarGrid.Visible)
            {
                LayoutContainer lc = ((LayoutContainer)selectedTree.SelectedNode.Tag);
                lc.RemoveDataToolbar = ((CheckBox)sender).Checked;
            }
        }

        private void txParentSelectorDataName_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox && !selectedTree.IsNull() && !selectedTree.SelectedNode.IsNull())
            {
                if (selectedTree.SelectedNode.Tag is LayoutContainer)
                    ((LayoutContainer)selectedTree.SelectedNode.Tag).ParentSelectorDataName = ((TextBox)sender).Text;
            }
        }

        #region HtmlViewer

        private void txtURL_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox && !selectedTree.IsNull() && !selectedTree.SelectedNode.IsNull())
            {
                if (selectedTree.SelectedNode.Tag is LayoutControlV2)
                    ((LayoutControlV2)selectedTree.SelectedNode.Tag).Url = ((TextBox)sender).Text;

                this.selectedTree.Invalidate();
            }
        }

        private void txtHtml_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox && !selectedTree.IsNull() && !selectedTree.SelectedNode.IsNull())
            {
                if (selectedTree.SelectedNode.Tag is LayoutControlV2)
                    ((LayoutControlV2)selectedTree.SelectedNode.Tag).HtmlCode = ((TextBox)sender).Text;

                this.selectedTree.Invalidate();
            }
        }

        #endregion


        private void cbEntityNavigator_SelectedIndexChanged(object sender, EventArgs e)
        {
            LayoutControlV2 lc = (selectedTree.SelectedNode.Tag as LayoutControlV2);
            if (lc != null)
                lc.BindingPath = cbEntityNavigator.Text;
        }

        private void ckAllowEmpty_CheckedChanged(object sender, EventArgs e)
        {
            LayoutControlV2 lc = selectedTree.SelectedNode.Tag as LayoutControlV2;
            CheckBox check = sender as CheckBox;
            if (lc != null && check != null)
                lc.AllowEmptyOption = check.Checked;
        }

        private void txChartAvailableMeasures_TextChanged(object sender, EventArgs e)
        {
            LayoutContainer lc = ((LayoutContainer)selectedTree.SelectedNode.Tag);
            lc.ChartAvailableMeasures = this.txChartAvailableMeasures.Text;
        }

        private void chkShareParentBO_CheckedChanged(object sender, EventArgs e)
        {
            if (this.grpExternalUI.Visible)
            {
                LayoutContainer lc = ((LayoutContainer)selectedTree.SelectedNode.Tag);
                lc.ShareParentBO = ((CheckBox)sender).Checked;
            }
        }

        private void cmbUserInterfaceLayoutType_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.grpExternalUI.Visible && sender is ComboBox && !selectedTree.IsNull() && !selectedTree.SelectedNode.IsNull() && selectedTree.SelectedNode.Tag is LayoutContainer)
            {
                ((LayoutContainer)selectedTree.SelectedNode.Tag).UserInterfaceLayoutType = (UILayouts)Enum.Parse(typeof(Linx.Tools.UILayouts), ((ComboBox)sender).SelectedItem.ToString());
            }
        }


        private void ckIsTotalVisible_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox && !selectedTree.IsNull() && !selectedTree.SelectedNode.IsNull())
            {
                if (selectedTree.SelectedNode.Tag is LayoutContainer)
                    ((LayoutContainer)selectedTree.SelectedNode.Tag).IsTotalVisible = ((CheckBox)sender).Checked;
            }
        }

        private void ckIsLinqSelectionControl_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox && !selectedTree.IsNull() && !selectedTree.SelectedNode.IsNull())
            {
                if (selectedTree.SelectedNode.Tag is LayoutContainer)
                    ((LayoutContainer)selectedTree.SelectedNode.Tag).IsLinqSelectionControl = ((CheckBox)sender).Checked;
            }
        }

        private void cmbPivotMeasuresLocation_SelectedValueChanged(object sender, EventArgs e)
        {
            LayoutContainer lc = ((LayoutContainer)selectedTree.SelectedNode.Tag);
            lc.PivotMeasuresLocation = this.cmbPivotMeasuresLocation.SelectedItem as string;
        }

        private void chkUseFilterFromParent_CheckedChanged(object sender, EventArgs e)
        {
            if (this.grpExternalUI.Visible)
            {
                LayoutContainer lc = ((LayoutContainer)selectedTree.SelectedNode.Tag);
                lc.UseFilterFromParent = ((CheckBox)sender).Checked;
            }
        }

        private void chkApplyFilterToParent_CheckedChanged(object sender, EventArgs e)
        {
            if (this.grpExternalUI.Visible)
            {
                LayoutContainer lc = ((LayoutContainer)selectedTree.SelectedNode.Tag);
                lc.ApplyFilterToParent = ((CheckBox)sender).Checked;
            }
        }

        private void cbTipoGauge_SelectedIndexChanged(object sender, EventArgs e)
        {
            LayoutControlV2 lc = (selectedTree.SelectedNode.Tag as LayoutControlV2);
            ComboBox origem = (sender as ComboBox);
            if (lc != null && origem != null)
            {
                lc.GaugeType = origem.SelectedItem != null ? origem.SelectedItem.ToString() : String.Empty;
            }
        }

        private void txtMinorDivisions_ValueChanged(object sender, EventArgs e)
        {
            LayoutControlV2 lc = (selectedTree.SelectedNode.Tag as LayoutControlV2);
            NumericUpDown origem = (sender as NumericUpDown);
            if (lc != null && origem != null)
            {
                lc.MinorDivisions = (int)origem.Value;
            }
        }

        private void txtMiddleDivisions_ValueChanged(object sender, EventArgs e)
        {
            LayoutControlV2 lc = (selectedTree.SelectedNode.Tag as LayoutControlV2);
            NumericUpDown origem = (sender as NumericUpDown);
            if (lc != null && origem != null)
            {
                lc.MiddleDivisions = (int)origem.Value;
            }
        }


        private void txtMajorDivisions_ValueChanged(object sender, EventArgs e)
        {
            LayoutControlV2 lc = (selectedTree.SelectedNode.Tag as LayoutControlV2);
            NumericUpDown origem = (sender as NumericUpDown);
            if (lc != null && origem != null)
            {
                lc.MajorDivisions = (int)origem.Value;
            }
        }

        private void txtLabelFormat_TextChanged(object sender, EventArgs e)
        {
            LayoutControlV2 lc = (selectedTree.SelectedNode.Tag as LayoutControlV2);
            TextBox origem = (sender as TextBox);
            if (lc != null && origem != null)
            {
                lc.DataFormatString = origem.Text;
            }
        }


        private void TopDataToolbar_Control(object sender, EventArgs e)
        {
            if (this.layoutDefinition != null && sender is CheckBox)
            {
                switch (((CheckBox)sender).Name)
                {
                    case "ckTopCanClear":
                        this.layoutDefinition.CanClear = ((CheckBox)sender).Checked;
                        break;
                    case "ckTopCanSearch":
                        this.layoutDefinition.CanSearch = ((CheckBox)sender).Checked;
                        break;
                    case "ckTopCanAddNew":
                        this.layoutDefinition.CanAddNew = ((CheckBox)sender).Checked;
                        break;
                    case "ckTopCanEdit":
                        this.layoutDefinition.CanEdit = ((CheckBox)sender).Checked;
                        break;
                    case "ckTopCanDelete":
                        this.layoutDefinition.CanDelete = ((CheckBox)sender).Checked;
                        break;
                    case "ckTopCanCustomSearch":
                        this.layoutDefinition.CanCustomSearch = ((CheckBox)sender).Checked;
                        break;
                    case "ckTopCanPrint":
                        this.layoutDefinition.CanPrint = ((CheckBox)sender).Checked;
                        break;
                    case "ckTopCanLayout":
                        this.layoutDefinition.CanLayout = ((CheckBox)sender).Checked;
                        break;
                    case "ckTopCanNavigate":
                        this.layoutDefinition.CanNavigate = ((CheckBox)sender).Checked;
                        break;
                    case "ckTopCanExport":
                        this.layoutDefinition.CanExport = ((CheckBox)sender).Checked;
                        break;
                    default:
                        break;
                }
            }
        }


        private void GrGridDataOption_Control(object sender, EventArgs e)
        {
            if (grdGridDataOptions.Visible && !selectedTree.IsNull() && !selectedTree.SelectedNode.IsNull() && sender is CheckBox && selectedTree.SelectedNode.Tag is LayoutContainer)
            {
                switch (((CheckBox)sender).Name)
                {
                    case "ckDgCanAddNew":
                        ((LayoutContainer)selectedTree.SelectedNode.Tag).CanAddNew = ((CheckBox)sender).Checked;
                        break;
                    case "ckDgCanEdit":
                        ((LayoutContainer)selectedTree.SelectedNode.Tag).CanEdit = ((CheckBox)sender).Checked;
                        break;
                    case "ckDgCanDelete":
                        ((LayoutContainer)selectedTree.SelectedNode.Tag).CanDelete = ((CheckBox)sender).Checked;
                        break;
                    case "ckDgCanExportGrid":
                        ((LayoutContainer)selectedTree.SelectedNode.Tag).CanExportGrid = ((CheckBox)sender).Checked;
                        break;
                    default:
                        break;
                }
            }
        }


        private void GrDataToolbar_Control(object sender, EventArgs e)
        {
            if (grpExternalUI.Visible && !selectedTree.IsNull() && !selectedTree.SelectedNode.IsNull() && sender is CheckBox && selectedTree.SelectedNode.Tag is LayoutContainer)
            {
                switch (((CheckBox)sender).Name)
                {
                    case "ckGrCanClear":
                        ((LayoutContainer)selectedTree.SelectedNode.Tag).CanClear = ((CheckBox)sender).Checked;
                        break;
                    case "ckGrCanSearch":
                        ((LayoutContainer)selectedTree.SelectedNode.Tag).CanSearch = ((CheckBox)sender).Checked;
                        break;
                    case "ckGrCanAddNew":
                        ((LayoutContainer)selectedTree.SelectedNode.Tag).CanAddNew = ((CheckBox)sender).Checked;
                        break;
                    case "ckGrCanEdit":
                        ((LayoutContainer)selectedTree.SelectedNode.Tag).CanEdit = ((CheckBox)sender).Checked;
                        break;
                    case "ckGrCanDelete":
                        ((LayoutContainer)selectedTree.SelectedNode.Tag).CanDelete = ((CheckBox)sender).Checked;
                        break;
                    case "ckGrCanCustomSearch":
                        ((LayoutContainer)selectedTree.SelectedNode.Tag).CanCustomSearch = ((CheckBox)sender).Checked;
                        break;
                    case "ckGrCanPrint":
                        ((LayoutContainer)selectedTree.SelectedNode.Tag).CanPrint = ((CheckBox)sender).Checked;
                        break;
                    case "ckGrCanExport":
                        ((LayoutContainer)selectedTree.SelectedNode.Tag).CanExport = ((CheckBox)sender).Checked;
                        break;
                    case "ckGrCanLayout":
                        ((LayoutContainer)selectedTree.SelectedNode.Tag).CanLayout = ((CheckBox)sender).Checked;
                        break;
                    case "ckGrCanNavigate":
                        ((LayoutContainer)selectedTree.SelectedNode.Tag).CanNavigate = ((CheckBox)sender).Checked;
                        break;
                    default:
                        break;
                }
            }
        }

        private void chkRemoveDataToolbar_CheckedChanged(object sender, EventArgs e)
        {
            if (this.grpExternalUI.Visible)
            {
                LayoutContainer lc = ((LayoutContainer)selectedTree.SelectedNode.Tag);
                lc.RemoveDataToolbar = ((CheckBox)sender).Checked;
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chkNoSearch_CheckedChanged(object sender, EventArgs e)
        {
            if (this.grpExternalUI.Visible)
            {
                LayoutContainer lc = ((LayoutContainer)selectedTree.SelectedNode.Tag);
                lc.NoSearch = ((CheckBox)sender).Checked;
            }
        }

        private void ckIsPassword_CheckedChanged(object sender, EventArgs e)
        {
            LayoutControlV2 lc = selectedTree.SelectedNode.Tag as LayoutControlV2;
            CheckBox check = sender as CheckBox;
            if (lc != null && check != null)
                lc.IsPassword = check.Checked;
        }

        #region tabWizard

        private void ckWizardInitialPageRemoved_Click(object sender, EventArgs e)
        {
            if (sender is CheckBox && !selectedTree.IsNull() && !selectedTree.SelectedNode.IsNull())
            {
                if (selectedTree.SelectedNode.Tag is LayoutContainer)
                    (selectedTree.SelectedNode.Tag as LayoutContainer).RemoveInitialPage = (sender as CheckBox).Checked;
            }
        }

        private void ckWizardFinalPageRemoved_Click(object sender, EventArgs e)
        {
            if (sender is CheckBox && !selectedTree.IsNull() && !selectedTree.SelectedNode.IsNull())
            {
                if (selectedTree.SelectedNode.Tag is LayoutContainer)
                    (selectedTree.SelectedNode.Tag as LayoutContainer).RemoveFinalPage = (sender as CheckBox).Checked;
            }

        }

        private void txtWizardInitialPageDisplay_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox && !selectedTree.IsNull() && !selectedTree.SelectedNode.IsNull())
            {
                if (selectedTree.SelectedNode.Tag is LayoutContainer)
                    ((LayoutContainer)selectedTree.SelectedNode.Tag).InitialPageDisplayName = ((TextBox)sender).Text;
            }
        }

        private void txtWizardFinalPageDisplay_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox && !selectedTree.IsNull() && !selectedTree.SelectedNode.IsNull())
            {
                if (selectedTree.SelectedNode.Tag is LayoutContainer)
                    ((LayoutContainer)selectedTree.SelectedNode.Tag).FinalPageDisplayName = ((TextBox)sender).Text;
            }
        }

        private void txtWizardInitialPageDescription_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox && !selectedTree.IsNull() && !selectedTree.SelectedNode.IsNull())
            {
                if (selectedTree.SelectedNode.Tag is LayoutContainer)
                    ((LayoutContainer)selectedTree.SelectedNode.Tag).InitialPageDescription = ((TextBox)sender).Text;
            }
        }

        private void txtWizardFinalPageDescription_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox && !selectedTree.IsNull() && !selectedTree.SelectedNode.IsNull())
            {
                if (selectedTree.SelectedNode.Tag is LayoutContainer)
                    ((LayoutContainer)selectedTree.SelectedNode.Tag).FinalPageDescription = ((TextBox)sender).Text;
            }
        }

        private void txtWizardSideBarDisplayName_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox && !selectedTree.IsNull() && !selectedTree.SelectedNode.IsNull())
            {
                if (selectedTree.SelectedNode.Tag is LayoutContainer)
                    ((LayoutContainer)selectedTree.SelectedNode.Tag).SideBarDescription = ((TextBox)sender).Text;
            }
        }

        private void txtWizardUserName_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox && !selectedTree.IsNull() && !selectedTree.SelectedNode.IsNull())
            {
                if (selectedTree.SelectedNode.Tag is LayoutContainer)
                    ((LayoutContainer)selectedTree.SelectedNode.Tag).DefinedUserName = ((TextBox)sender).Text;
            }
        }

        private void txtWizardDisplayName_TextChanged(object sender, EventArgs e)
        {

            if (sender is TextBox && !selectedTree.IsNull() && !selectedTree.SelectedNode.IsNull())
            {
                if (selectedTree.SelectedNode.Tag is LayoutContainer)
                    ((LayoutContainer)selectedTree.SelectedNode.Tag).DisplayName = ((TextBox)sender).Text;
            }
        }



        private void ckWizardInitialPageRemoved_CheckedChanged(object sender, EventArgs e)
        {
            LayoutContainer lc = selectedTree.SelectedNode.Tag as LayoutContainer;
            CheckBox check = sender as CheckBox;
            if (lc != null && check != null)
                lc.RemoveInitialPage = check.Checked;
        }

        private void ckWizardFinalPageRemoved_CheckedChanged(object sender, EventArgs e)
        {
            LayoutContainer lc = selectedTree.SelectedNode.Tag as LayoutContainer;
            CheckBox check = sender as CheckBox;
            if (lc != null && check != null)
                lc.RemoveFinalPage = check.Checked;
        }

        #endregion


        private void ckHasGroupBy_CheckedChanged(object sender, EventArgs e)
        {
            txGroupByColumns.Enabled = this.ckHasGroupBy.Visible && this.ckHasGroupBy.Checked;

            if (this.ckHasGroupBy.Visible)
            {
                LayoutContainer lc = ((LayoutContainer)selectedTree.SelectedNode.Tag);
                lc.HasGroupBy = ((CheckBox)sender).Checked;
            }
        }




        private void ckIsEditableOnInsert_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox && !selectedTree.IsNull() && !selectedTree.SelectedNode.IsNull())
            {
                if (selectedTree.SelectedNode.Tag is LayoutControlV2)
                {
                    ((LayoutControlV2)selectedTree.SelectedNode.Tag).EditableOnInsert = ((CheckBox)sender).Checked;
                    if (((CheckBox)sender).Checked)
                    {
                        if (this.ckIsEditable.Checked)
                            this.ckIsEditable.Checked = false;
                        if (this.ckAlwaysEditable.Checked)
                            this.ckAlwaysEditable.Checked = false;
                    }
                }
            }
        }

        #region Daskboard Control
        Color getColor(string colorName)
        {
            if (colorName.IsNullOrEmpty())
                colorName = "blue";
            if (Regex.IsMatch(colorName, @"^rgb\(\s*\d+,\s*\d+,\s*\d+\)$"))
            {
                var values = Regex.Matches(colorName, @"\d+");
                if (values.Count == 3)
                    return Color.FromArgb(255, int.Parse(values[0].Value), int.Parse(values[1].Value), int.Parse(values[2].Value));
                else
                    return Color.Blue;
            }
            else
                return Color.FromName(colorName);
        }

        string getColorText(Color color)
        {
            return string.Format("rgb({0}, {1}, {2})", color.R, color.G, color.B);
        }

        private void dashboardIconFA_TextChanged(object sender, EventArgs e)
        {
            LayoutControlV2 lc = selectedTree.SelectedNode.Tag as LayoutControlV2;
            TextBox textBox = sender as TextBox;
            lc.DashboardIconFAName = textBox.Text;
        }

        void setDashboardColor(string colorName)
        {
            this.DashboardColorName.Text = colorName;
            this.DashboardColorChose.BackColor = getColor(colorName);
        }

        private void ChooseDashboardColor_Click(object sender, EventArgs e)
        {
            LayoutControlV2 lc = selectedTree.SelectedNode.Tag as LayoutControlV2;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                var colorName = getColorText(colorDialog.Color);
                lc.DashboardBackgroundColorClassName = colorName;

                setDashboardColor(colorName);
            }
        }

        private void DashboardSizeWidth_SelectedValueChanged(object sender, EventArgs e)
        {
            ComboBox combobox = sender as ComboBox;
            if (combobox.SelectedIndex == -1) return;

            LayoutControlV2 lc = selectedTree.SelectedNode.Tag as LayoutControlV2;
            lc.DashboardWidth = combobox.SelectedItem.ToString();
        }
        #endregion

        private void txtGroupName_Leave(object sender, EventArgs e)
        {
            RenameSelectElement(sender);
        }

        private void txGroupByColumns_TextChanged(object sender, EventArgs e)
        {
            LayoutContainer lc = ((LayoutContainer)selectedTree.SelectedNode.Tag);
            lc.GroupByColumns = ((TextBox)sender).Text;
        }

        private void txDomainFilterValues_TextChanged(object sender, EventArgs e)
        {
            if (!selectedTree.IsNull() && !selectedTree.SelectedNode.IsNull())
            {
                if (selectedTree.SelectedNode.Tag is LayoutControlV2)
                {
                    if (sender.Equals(this.txDomainFilterValues))
                        ((LayoutControlV2)selectedTree.SelectedNode.Tag).DomainFilterValues = this.txDomainFilterValues.Text;
                }
            }
        }

        private void chkEditorTemplateGrid_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkEditorTemplateGrid.Visible)
            {
                LayoutContainer lc = ((LayoutContainer)selectedTree.SelectedNode.Tag);
                lc.IsTemplate = ((CheckBox)sender).Checked;
            }
        }

        private void numGroupHeight_ValueChanged(object sender, EventArgs e)
        {
            var container = this.treeTopGroups.SelectedNode.Tag as LayoutContainer;
            var value = (int)((NumericUpDown)sender).Value;

            container.Height = value;
        }

        private void numTotalLines_ValueChanged(object sender, EventArgs e)
        {
            var container = this.treeTopGroups.SelectedNode.Tag as LayoutControlV2;
            var value = (int)((NumericUpDown)sender).Value;

            container.TotalLines = value;
        }

        private void rbLabelPosition_CheckedChanged(object sender, EventArgs e)
        {
            var container = this.treeTopGroups.SelectedNode.Tag as LayoutContainer;

            container.LabelPosition = rbLabelPositionTop.Checked ? LabelPosition.Top : LabelPosition.Left;
        }

        private void numColumnSpan_ValueChanged(object sender, EventArgs e)
        {
            var element = this.treeTopGroups.SelectedNode.Tag as LayoutElement;
            var value = (int)((NumericUpDown)sender).Value;

            element.ColumnSpan = value;
        }

        void FieldFontControl_FontPropertyChanged(FontControl sender, FontControlEventArgs e)
        {
            var element = this.treeTopGroups.SelectedNode.Tag as LayoutElement;
            if (element == null) return;
            switch (e.Property)
            {
                case FontProperties.Bold:
                    element.FontBold = sender.Bold.Checked;
                    break;

                case FontProperties.Style:
                    element.FontForegroundStyle = ParseEnum<FontForegroundStyle>((string)sender.Style.SelectedItem);
                    break;
                case FontProperties.Highlight:
                    element.FontBackground = sender.Highlight.Checked ? FontBackground.Highlight : FontBackground.Normal;
                    break;
                default:
                    break;
            }
        }

        T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }

        private void ckHasFilterRange_CheckedChanged(object sender, EventArgs e)
        {
            LayoutControlV2 lc = selectedTree.SelectedNode.Tag as LayoutControlV2;
            CheckBox check = sender as CheckBox;
            if (lc != null && check != null)
                lc.HasFilterRange = check.Checked;
        }

        private void cboGroupStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox combobox = sender as ComboBox;
            if (combobox.SelectedIndex == -1) return;

            LayoutContainer lc = selectedTree.SelectedNode.Tag as LayoutContainer;
            lc.Style = ParseEnum<ContainerStyle>((string)combobox.SelectedItem);
        }

        private void cmbControlWidth_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox combobox = sender as ComboBox;
            if (combobox.SelectedIndex == -1) return;

            LayoutControlV2 lc = selectedTree.SelectedNode.Tag as LayoutControlV2;
            lc.ControlWidth = ParseEnum<ControlWidth>((string)combobox.SelectedItem);
        }

        private void chkAllowNegativeValue_CheckedChanged(object sender, EventArgs e)
        {
            LayoutControlV2 lc = selectedTree.SelectedNode.Tag as LayoutControlV2;
            CheckBox check = sender as CheckBox;
            if (lc != null && check != null)
                lc.AllowNegativeValue = check.Checked;
        }


        private void cmbMediaWidth_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox combobox = sender as ComboBox;
            if (combobox.SelectedIndex == -1) return;

            LayoutControlV2 lc = selectedTree.SelectedNode.Tag as LayoutControlV2;
            lc.MediaWidth = ParseEnum<MediaWidth>((string)combobox.SelectedItem);
        }

        private void cboGridHeight_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox combobox = sender as ComboBox;
            if (combobox.SelectedIndex == -1) return;

            LayoutContainer lc = selectedTree.SelectedNode.Tag as LayoutContainer;
            lc.GridHeight = ParseEnum<GridSizeHeight>((string)combobox.SelectedItem);
        }

        private void cboGridWidth_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox combobox = sender as ComboBox;
            if (combobox.SelectedIndex == -1) return;

            LayoutContainer lc = selectedTree.SelectedNode.Tag as LayoutContainer;
            lc.GridWidth = ParseEnum<GridSizeWidth>((string)combobox.SelectedItem);
        }

        private void ckEnableMultiSelection_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckEnableMultiSelection.Visible)
            {
                LayoutContainer lc = ((LayoutContainer)selectedTree.SelectedNode.Tag);
                lc.EnableMultiSelection = ((CheckBox)sender).Checked;
            }
        }

        private void txtRange_TextChanged(object sender, EventArgs e)
        {
            if (this.txtRange.Visible)
            {
                LayoutControlV2 lc = ((LayoutControlV2)selectedTree.SelectedNode.Tag);
                lc.Range = ((TextBox)sender).Text;
            }
        }

        private void chkEditorOnlyTemplate_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkEditorOnlyTemplate.Visible)
            {
                LayoutContainer lc = ((LayoutContainer)selectedTree.SelectedNode.Tag);
                lc.EditionOnlyTemplate = ((CheckBox)sender).Checked;
            }
        }

        private void cmbFieldVisibleGridEditor_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox combobox = sender as ComboBox;
            if (combobox.SelectedIndex == -1) return;

            LayoutControlV2 lc = selectedTree.SelectedNode.Tag as LayoutControlV2;
            lc.FieldVisibleGrid = ParseEnum<VisibleFieldGrid>((string)combobox.SelectedItem);
        }

        private void chShowAllLabelsForConnectedFields_CheckedChanged(object sender, EventArgs e)
        {
            if (this.layoutDefinition != null)
                this.layoutDefinition.ShowAllLabelsForConnectedFields = chShowAllLabelsForConnectedFields.Checked;
        }

        private void numPageSize_ValueChanged(object sender, EventArgs e)
        {
            if (this.numPageSize.Visible)
            {
                LayoutContainer lc = ((LayoutContainer)selectedTree.SelectedNode.Tag);
                lc.PageSize = (int)this.numPageSize.Value;
            }
        }

        private void ckAllowMultiSelectionInSearch_CheckedChanged(object sender, EventArgs e)
        {
            LayoutControlV2 lc = selectedTree.SelectedNode.Tag as LayoutControlV2;
            CheckBox check = sender as CheckBox;
            if (lc != null && check != null)
                lc.AllowMultiSelectionInSearch = check.Checked;
        }

        private void ckValidateOnClearState_CheckedChanged(object sender, EventArgs e)
        {
            LayoutControlV2 lc = selectedTree.SelectedNode.Tag as LayoutControlV2;
            CheckBox check = sender as CheckBox;
            if (lc != null && check != null)
                lc.ValidateOnClearState = check.Checked;
        }

        private void ckDisplayRangeDate_CheckedChanged(object sender, EventArgs e)
        {
            LayoutControlV2 lc = selectedTree.SelectedNode.Tag as LayoutControlV2;
            CheckBox check = sender as CheckBox;
            if (lc != null && check != null)
                lc.DisplayRangeDate = check.Checked;
        }
    }
}
