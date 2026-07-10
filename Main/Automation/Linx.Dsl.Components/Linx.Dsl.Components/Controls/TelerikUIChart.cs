using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Linx.Tools;

namespace Linx.Dsl.Components
{
    public partial class TelerikUIChart : UserControl
    {

        private List<ChartGroupTelerik> ChartGroups;
        private ChartTelerikDTO chartInfo = new ChartTelerikDTO();
        private List<LabelPositionTelerik> LstLabelPosition = new List<LabelPositionTelerik>();
        private List<LabelMultiType> LstLabelMultiType = new List<LabelMultiType>();
        private List<LabelRotacaoAxe> LstRotacaoLabelAxe = new List<LabelRotacaoAxe>();
        private List<LabelRotacaoSerie> LstRotacaoLabelSerie = new List<LabelRotacaoSerie>();
        private LayoutElement _chartLayout { get; set; }
        private bool disableUpdateLayout = false;

        public LayoutElement ChartLayout
        {
            get { return _chartLayout; }
            set
            {
                if (_chartLayout != value)
                {
                    _chartLayout = value;
                    Deserialize();
                }
            }
        }

        private Dictionary<string, string> _adapterFields;
        public Dictionary<string, string> AdapterFields
        {
            set
            {
                _adapterFields = value;
                _adapterFields.Add("", "");
                LoadBindingCombos();
            }
        }

        public TelerikUIChart()
        {
            InitializeComponent();
            LoadChartInfo();

            dgvChartProperties.CellValueChanged += dgvChartProperties_CellValueChanged;
            dgvAxesProperties.CellValueChanged += dgvAxesProperties_CellValueChanged;
            dgvSeriesProperties.CellValueChanged += dgvSeriesProperties_CellValueChanged;
        }

        #region ChartInfo

        private void LoadBindingCombos()
        {
            cmbAxeLabel.Items.Clear();
            cmbSeriesValueMemberPath.Items.Clear();

            foreach (KeyValuePair<string, string> item in _adapterFields)
            {
                cmbAxeLabel.Items.Add(item);
                cmbAxeLabel.DisplayMember = "Key";
                cmbSeriesValueMemberPath.Items.Add(item);
                cmbSeriesValueMemberPath.DisplayMember = "Key";
            }

            if (chartInfo.Axes.Count() > 0)
                lstAxes_SelectedIndexChanged(this, null);

            if (chartInfo.Series.Count() > 0)
                lstSeries_SelectedIndexChanged(this, null);
        }

        private void UpdateLayoutElementInfo()
        {
            if (!disableUpdateLayout && _chartLayout != null)
            {
                _chartLayout.InternalDefinition = Linx.Tools.SerializationManager<ChartTelerikDTO>.ObjectToString(chartInfo);
                _chartLayout.ScriptDefinition = Linx.Dsl.Components.Common.ChartBuilderTelerik.BuilderJS(chartInfo, Linx.Dsl.Components.Enums.LibEnum.Telerik);
            }
        }

        private void LoadChartInfo()
        {
            ChartGroups = new List<ChartGroupTelerik>();

            ChartGroupTelerik group = new ChartGroupTelerik() { Group = "Bar and Column Series", HasAxes = true, HasSeries = true, HasMultiType = true };
            group.Types.Add(new ChartTypeTelerik() { Type = "bar", Description = "Bar" });
            group.Types.Add(new ChartTypeTelerik() { Type = "column", Description = "Column" });
            ChartGroups.Add(group);

            group = new ChartGroupTelerik() { Group = "Pie Chart", HasAxes = false, HasSeries = false, HasMemberPath = true, HasSeriesMemberPath = false, HasLabelsPosition = true, HasTitle = true, HasMultiAxis = false };
            group.Types.Add(new ChartTypeTelerik() { Type = "pie", Description = "Pie Chart" });
            ChartGroups.Add(group);

            group = new ChartGroupTelerik() { Group = "Lines", HasAxes = true, HasSeries = true, HasMultiAxis = true, HasMultiType = true };
            group.Types.Add(new ChartTypeTelerik() { Type = "line", Description = "Line" });
            ChartGroups.Add(group);

            group = new ChartGroupTelerik() { Group = "Area", HasAxes = true, HasSeries = true, HasMultiAxis = true, HasMultiType = true };
            group.Types.Add(new ChartTypeTelerik() { Type = "area", Description = "Area" });
            ChartGroups.Add(group);

            group = new ChartGroupTelerik() { Group = "Stacked Series", HasAxes = true, HasSeries = true, HasMultiAxis = false };
            group.Types.Add(new ChartTypeTelerik() { Type = "bar", Description = "Stacked Bar" });
            group.Types.Add(new ChartTypeTelerik() { Type = "column", Description = "Stacked Column" });
            ChartGroups.Add(group);


            cmbChartGroup.DataSource = ChartGroups;
            cmbChartGroup.DisplayMember = "Group";

            cmbChartType.DataSource = ChartGroups;
            cmbChartType.DisplayMember = "Types.Description";

            //Label Position
            LstLabelPosition.Add(new LabelPositionTelerik() { Description = "Top", Value = "top" });
            LstLabelPosition.Add(new LabelPositionTelerik() { Description = "Bottom", Value = "bottom" });
            LstLabelPosition.Add(new LabelPositionTelerik() { Description = "Left", Value = "left" });
            LstLabelPosition.Add(new LabelPositionTelerik() { Description = "Right", Value = "right" });

            //Legenda
            cmbPositionLegend.DataSource = LstLabelPosition;
            cmbPositionLegend.DisplayMember = "Description";

            preencheRotacaoLabelAxe();
            preencheRotacaoLabelSerie();

            //Properties
            dgvChartProperties.AutoGenerateColumns = false;
            dgvAxesProperties.AutoGenerateColumns = false;
            dgvSeriesProperties.AutoGenerateColumns = false;
        }

        private void preencheRotacaoLabelAxe()
        {
            //Rotacao Label
            LstRotacaoLabelAxe.Add(new LabelRotacaoAxe() { Description = "0", Value = "0" });
            LstRotacaoLabelAxe.Add(new LabelRotacaoAxe() { Description = "60", Value = "60" });
            LstRotacaoLabelAxe.Add(new LabelRotacaoAxe() { Description = "-60", Value = "-60" });
            LstRotacaoLabelAxe.Add(new LabelRotacaoAxe() { Description = "90", Value = "90" });
            LstRotacaoLabelAxe.Add(new LabelRotacaoAxe() { Description = "-90", Value = "-90" });

            cmbRotacaoLabelAxe.DataSource = LstRotacaoLabelAxe;
            cmbRotacaoLabelAxe.DisplayMember = "Description";

        }
        private void preencheRotacaoLabelSerie()
        {
            //Rotacao Label
            LstRotacaoLabelSerie.Add(new LabelRotacaoSerie() { Description = "0", Value = "0" });
            LstRotacaoLabelSerie.Add(new LabelRotacaoSerie() { Description = "60", Value = "60" });
            LstRotacaoLabelSerie.Add(new LabelRotacaoSerie() { Description = "-60", Value = "-60" });
            LstRotacaoLabelSerie.Add(new LabelRotacaoSerie() { Description = "90", Value = "90" });
            LstRotacaoLabelSerie.Add(new LabelRotacaoSerie() { Description = "-90", Value = "-90" });

            cmbRotacaoLabelSerie.DataSource = LstRotacaoLabelSerie;
            cmbRotacaoLabelSerie.DisplayMember = "Description";

        }

        private void cmbChartGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChartGroupTelerik chartGroup = cmbChartGroup.SelectedItem as ChartGroupTelerik;
            if (chartGroup != null)
            {
                chartInfo.ChartGroup = chartGroup.Group;

                //Axes
                groupAxes.Enabled = chartGroup.HasAxes;
                if (!chartGroup.HasAxes)
                {
                    chartInfo.Axes.Clear();
                    UpdateLstAxesDataSource(null);
                    txtAxeTitle.Text = string.Empty;
                    ckShowCrossHair.Checked = false;
                    tabAxesInfo.SelectedIndex = 0;

                }

                //Series
                groupSeries.Enabled = chartGroup.HasSeries;
                if (!chartGroup.HasSeries)
                {
                    chartInfo.Series.Clear();
                    UpdateLstSeriesDataSource(null);
                    txtSeriesTitle.Text = string.Empty;
                    txtFormatSerie.Text = string.Empty;
                    ckMultiAxis.Checked = false;
                    ckShowTooltip.Checked = false;
                    tabSeriesInfo.SelectedIndex = 0;
                }

                //Multi Type
                groupMultiType.Enabled = chartGroup.HasMultiType;
                if (!chartGroup.HasMultiType)
                {
                    ckEnableMultType.Checked = false;
                    cmbMultiType.Text = string.Empty;
                }


                //Member Path
                groupMemberPath.Enabled = chartGroup.HasMemberPath;

                if (!chartGroup.HasMemberPath)
                {
                    txtLabelMemberPath.Text = string.Empty;
                    txtValueMemberPath.Text = string.Empty;
                    txtLabelCategoryPath.Text = string.Empty;
                    txtFormatSeriePie.Text = string.Empty;
                }

                //MultiAxis
                ckMultiAxis.Enabled = chartGroup.HasMultiAxis;
                ckMultiAxis.Checked = chartGroup.HasMultiAxis ? ckMultiAxis.Checked : false;

                //Title
                txtTitle.Enabled = chartGroup.HasTitle;
                txtTitle.Text = chartGroup.HasTitle ? txtTitle.Text : string.Empty;

                //Series Member Path
                if (!chartGroup.HasMemberPath)
                {
                    foreach (SerieDTO series in chartInfo.Series)
                    {
                        series.LowMemberPath = string.Empty;
                        series.HighMemberPath = string.Empty;
                    }
                }

                //Chart Type
                cmbChartType.SelectedItem = chartGroup.Types[0];
                chartInfo.ChartType = chartGroup.Types[0].Type;

                //
                UpdateControls();
                UpdateLayoutElementInfo();
            }
        }

        private void cmbChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChartTypeTelerik chartType = cmbChartType.SelectedItem as ChartTypeTelerik;

            if (chartType != null)
            {
                chartInfo.ChartType = chartType.Type;

                foreach (SerieDTO series in chartInfo.Series)
                {
                    series.Type = chartInfo.ChartType;
                }
                UpdateLayoutElementInfo();
            }
        }

        private void txtTitle_TextChanged(object sender, EventArgs e)
        {
            chartInfo.Title = txtTitle.Text;
            UpdateLayoutElementInfo();
        }

        private void cmbPositionLegend_SelectedIndexChanged(object sender, EventArgs e)
        {
            LabelPositionTelerik labelPosition = cmbPositionLegend.SelectedItem as LabelPositionTelerik;
            chartInfo.LegendPosition = labelPosition != null ? labelPosition.Value : string.Empty;
            UpdateLayoutElementInfo();
        }

        private void ckMultiAxis_CheckedChanged(object sender, EventArgs e)
        {
            chartInfo.MultiAxis = ckMultiAxis.Checked;
            UpdateLayoutElementInfo();
        }


        private void txtWidthGrafico_ValueChanged(object sender, EventArgs e)
        {
            chartInfo.Width = Convert.ToInt32(txtWidthGrafico.Value);
            UpdateLayoutElementInfo();
        }

        private void txtHeightGrafico_ValueChanged(object sender, EventArgs e)
        {
            chartInfo.Height = Convert.ToInt32(txtHeightGrafico.Value);
            UpdateLayoutElementInfo();
        }

        private void txtLabelMemberPath_TextChanged(object sender, EventArgs e)
        {
            chartInfo.LabelMemberPath = txtLabelMemberPath.Text;
            UpdateLayoutElementInfo();
        }

        private void txtValueMemberPath_TextChanged(object sender, EventArgs e)
        {
            chartInfo.ValueMemberPath = txtValueMemberPath.Text;
            UpdateLayoutElementInfo();
        }

        private void txtLabelCategoryPath_TextChanged(object sender, EventArgs e)
        {
            chartInfo.Category = txtLabelCategoryPath.Text;
            UpdateLayoutElementInfo();
        }

        private void txtFormatSeriePie_TextChanged(object sender, EventArgs e)
        {
            chartInfo.FormatSeriePie = txtFormatSeriePie.Text;
            UpdateLayoutElementInfo();

        }

        #region Campos Ignite
        //private void txtSubTitle_TextChanged(object sender, EventArgs e)
        //{
        //    chartInfo.SubTitle = txtSubTitle.Text;
        //    UpdateLayoutElementInfo();
        //}

        //private void txtWidth_TextChanged(object sender, EventArgs e)
        //{
        //    chartInfo.Width = txtWidth.Text;
        //    UpdateLayoutElementInfo();
        //}

        //private void txtHeight_TextChanged(object sender, EventArgs e)
        //{
        //    chartInfo.Height = txtHeight.Text;
        //    UpdateLayoutElementInfo();
        //}


        //private void cmbLabelsPosition_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    LabelPosition labelPosition = cmbLabelsPosition.SelectedItem as LabelPosition;
        //    chartInfo.LabelsPosition = labelPosition != null ? labelPosition.Value : string.Empty;
        //    UpdateLayoutElementInfo();
        //}

        //private void ckLegendEnabled_CheckedChanged(object sender, EventArgs e)
        //{
        //    chartInfo.Legend.Enabled = ckLegendEnabled.Checked;

        //    if (!chartInfo.Legend.Enabled)
        //    {
        //        txtLegendWidth.Text = string.Empty;
        //        txtLegendHeight.Text = string.Empty;
        //        cmbLegendType.Text = string.Empty;
        //    }
        //    else
        //    {
        //        if (txtLegendWidth.Text == string.Empty)
        //            txtLegendWidth.Text = "100%";

        //        if (txtLegendHeight.Text == string.Empty)
        //            txtLegendHeight.Text = "100%";
        //    }

        //    txtLegendWidth.Enabled = chartInfo.Legend.Enabled;
        //    txtLegendHeight.Enabled = chartInfo.Legend.Enabled;
        //    cmbLegendType.Enabled = chartInfo.Legend.Enabled;

        //    UpdateLayoutElementInfo();
        //}

        //private void txtLegendWidth_TextChanged(object sender, EventArgs e)
        //{
        //    chartInfo.Legend.Width = txtLegendWidth.Text;
        //    UpdateLayoutElementInfo();
        //}

        //private void txtLegendHeight_TextChanged(object sender, EventArgs e)
        //{
        //    chartInfo.Legend.Height = txtLegendHeight.Text;
        //    UpdateLayoutElementInfo();
        //}

        //private void cmbLegendType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    chartInfo.Legend.Type = cmbLegendType.Text;
        //    UpdateLayoutElementInfo();
        //}
        #endregion

        private void btnAddChartProp_Click(object sender, EventArgs e)
        {
            chartInfo.Properties.Add(new PropertyDTO() { UIDRef = chartInfo.UID, Lib = Linx.Dsl.Components.Enums.LibEnum.Telerik, Key = "New Property" });
            UpdateGridChartProperties();
            dgvChartProperties.BeginEdit(false);
        }

        private void btnRemoveChartProp_Click(object sender, EventArgs e)
        {
            if (dgvChartProperties.CurrentRow == null)
                return;

            PropertyDTO property = dgvChartProperties.CurrentRow.DataBoundItem as PropertyDTO;

            if (property != null)
                chartInfo.Properties.Remove(property);

            UpdateGridChartProperties();
            UpdateLayoutElementInfo();
        }

        void dgvSeriesProperties_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            UpdateLayoutElementInfo();
        }

        void dgvAxesProperties_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            UpdateLayoutElementInfo();
        }

        void dgvChartProperties_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            UpdateLayoutElementInfo();
        }

        private void UpdateGridChartProperties()
        {
            if (!dgvChartProperties.EditingControl.IsNull())
                dgvChartProperties.EndEdit();

            dgvChartProperties.DataSource = null;
            dgvChartProperties.DataSource = new BindingSource { DataSource = chartInfo.Properties };

            if (dgvChartProperties.Rows.Count > 0)
                dgvChartProperties.CurrentCell = dgvChartProperties.Rows[dgvChartProperties.Rows.Count - 1].Cells[0];
        }

        private void UpdateControls()
        {

            //Axes
            txtAxeTitle.Enabled = chartInfo.Axes.Count > 0;
            cmbAxeLabel.Enabled = chartInfo.Axes.Count > 0;


            if (chartInfo.Axes.Count == 0)
                ClearAxeInfo();

            //Series
            ChartGroupTelerik chartGroup = cmbChartGroup.SelectedItem as ChartGroupTelerik;
            ///txtSeriesName.Enabled = chartInfo.Series.Count > 0;
            txtSeriesTitle.Enabled = chartInfo.Series.Count > 0;
            txtFormatSerie.Enabled = chartInfo.Series.Count > 0;
            cmbSeriesValueMemberPath.Enabled = chartInfo.Series.Count > 0;
            groupMultiType.Enabled = (chartInfo.Series.Count > 0 && chartGroup != null && chartGroup.HasMultiType);


            if (chartInfo.Series.Count == 0)
                ClearSeriesInfo();
        }

        private void ClearAxeInfo()
        {
            //txtAxeName.Text = string.Empty;
            txtAxeTitle.Text = string.Empty;
            cmbAxeLabel.Text = string.Empty;
            ckShowCrossHair.Checked = false;
            //cmbAxeType.SelectedItem = null;
            tabAxesInfo.SelectedIndex = 0;
        }

        private void ClearSeriesInfo()
        {

            txtSeriesTitle.Text = string.Empty;
            txtFormatSerie.Text = string.Empty;
            ckShowTooltip.Checked = false;
            tabSeriesInfo.SelectedIndex = 0;
            cmbSeriesValueMemberPath.Text = string.Empty;
        }

        private void Deserialize()
        {
            //if (_chartLayout == null)
            //    return;

            disableUpdateLayout = true;

            ChartTelerikDTO chart = new ChartTelerikDTO();
            chartInfo = new ChartTelerikDTO();

            ControlClear();

            if (!_chartLayout.IsNull() && !_chartLayout.InternalDefinition.IsNullOrEmpty())
                chart = Linx.Tools.SerializationManager<ChartTelerikDTO>.StringToObject(_chartLayout.InternalDefinition);

            if (chart.ChartGroup.IsNullOrEmpty())
            {
                disableUpdateLayout = false;
                cmbChartGroup.Text = ChartGroups[0].Group;
                return;
            }

            if (chart.LegendPosition.IsNullOrEmpty())
            {
                cmbPositionLegend.Text = LstLabelPosition[0].Description;
                //return;
            }
            if (chart.RotacaoAxe.IsNullOrEmpty())
                cmbRotacaoLabelAxe.SelectedItem = LstRotacaoLabelAxe[0].Description;
            if (chart.RotacaoSerie.IsNullOrEmpty())
                cmbRotacaoLabelSerie.SelectedItem = LstRotacaoLabelSerie[0].Description;
            //ChartInfo
            ChartGroupTelerik group = ChartGroups.Where(i => i.Group == chart.ChartGroup).FirstOrDefault();
            ChartTypeTelerik chartType = group.Types.Where(i => i.Type == chart.ChartType).FirstOrDefault();

            cmbChartGroup.SelectedItem = group;
            cmbChartType.SelectedItem = chartType;
            txtTitle.Text = chart.Title;
            cmbPositionLegend.Text = chart.LegendPosition;
            txtFieldSort.Text = chart.SortField;
            txtDirSort.Text = chart.SortDir;
            ckMultiAxis.Checked = chart.MultiAxis;
            txtWidthGrafico.Value = chart.Width;
            txtHeightGrafico.Value = chart.Height;
            txtLabelMemberPath.Text = chart.LabelMemberPath;
            txtValueMemberPath.Text = chart.ValueMemberPath;
            txtLabelCategoryPath.Text = chart.Category;
            txtFormatSeriePie.Text = chart.FormatSeriePie;
            cmbRotacaoLabelAxe.Text = chart.RotacaoAxe.IsNullOrEmpty() ? LstRotacaoLabelAxe[0].Description : chart.RotacaoAxe;
            cmbRotacaoLabelSerie.Text = chart.RotacaoSerie.IsNullOrEmpty() ? LstRotacaoLabelSerie[0].Description : chart.RotacaoSerie;
            chartInfo.Properties = chart.Properties;
            UpdateGridChartProperties();

            //Axes
            chartInfo.Axes = chart.Axes;
            UpdateLstAxesDataSource(null);
            UpdateGridAxeProperties();

            //Series
            chartInfo.Series = chart.Series;
            UpdateLstSeriesDataSource(null);
            UpdateGridSeriesProperties();

            disableUpdateLayout = false;
        }

        private void ControlClear()
        {
            cmbChartGroup.SelectedItem = null;
            cmbChartType.SelectedItem = null;
            txtTitle.Text = string.Empty;
            cmbPositionLegend.SelectedItem = null;
            ckMultiAxis.Checked = false;
            txtWidthGrafico.Value = 0;
            txtHeightGrafico.Value = 0;
            txtLabelMemberPath.Text = string.Empty;
            txtValueMemberPath.Text = string.Empty;
            txtLabelCategoryPath.Text = string.Empty;
            txtFormatSeriePie.Text = string.Empty;
            cmbRotacaoLabelAxe.SelectedItem = null;
            cmbRotacaoLabelSerie.SelectedItem = null;
            UpdateGridChartProperties();

            //Axes
            chartInfo.Axes.Clear();
            UpdateLstAxesDataSource(null);
            UpdateGridAxeProperties();
            ClearAxeInfo();

            //Series
            chartInfo.Series.Clear();
            UpdateLstSeriesDataSource(null);
            UpdateGridSeriesProperties();
            ClearSeriesInfo();
        }

        #endregion

        #region AxeInfo

        private void btnAddAxe_Click(object sender, EventArgs e)
        {
            AxeDTO axe = new AxeDTO() { Name = "New Axe" };
            chartInfo.Axes.Add(axe);
            UpdateLstAxesDataSource(axe);
        }

        private void btnRemoveAxe_Click(object sender, EventArgs e)
        {
            AxeDTO axe = lstAxes.SelectedItem as AxeDTO;

            if (axe != null)
            {
                var series = chartInfo.Series.Where(i => i.xAxis == axe.Name || i.yAxis == axe.Name).ToList();
                series.Foreach(i =>
                {
                    chartInfo.Series.Remove(i);
                });

                UpdateLstSeriesDataSource(null);

                chartInfo.Axes.Remove(axe);
            }
            UpdateLstAxesDataSource(null);
            UpdateLayoutElementInfo();
        }

        private void lstAxes_SelectedIndexChanged(object sender, EventArgs e)
        {
            AxeDTO axe = lstAxes.SelectedItem as AxeDTO;

            if (axe != null)
            {
                //txtAxeName.Text = axe.Name;
                //cmbAxeType.Text = axe.Type;
                txtAxeTitle.Text = axe.Title;
                cmbAxeLabel.Text = axe.Label;
                ckShowCrossHair.Checked = axe.CrossHair;
                chkAxeGrouped.Checked = axe.GroupAxe;
            }
        }

        /// <summary>
        /// Método não usado no gráfico telerik
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void txtAxeName_Leave(object sender, EventArgs e)
        //{
        //    AxeDTO axe = lstAxes.SelectedItem as AxeDTO;

        //    if (axe != null && axe.Name != txtAxeName.Text)
        //    {
        //        string oldName = axe.Name;
        //        axe.Name = txtAxeName.Text;

        //        var xSeries = chartInfo.Series.Where(i => i.xAxis == oldName);
        //        var ySeries = chartInfo.Series.Where(i => i.yAxis == oldName);

        //        //Update series
        //        xSeries.Foreach(i =>
        //            {
        //                i.xAxis = axe.Name;
        //            });

        //        ySeries.Foreach(i =>
        //            {
        //                i.yAxis = axe.Name;
        //            });

        //        UpdateLstAxesDataSource(axe);
        //        UpdateLayoutElementInfo();
        //    }
        //}

        private void txtAxeTitle_TextChanged(object sender, EventArgs e)
        {
            AxeDTO axe = lstAxes.SelectedItem as AxeDTO;

            if (axe != null)
            {
                axe.Title = txtAxeTitle.Text;
                UpdateLayoutElementInfo();
            }

        }

        private void ckShowCrossHair_CheckedChanged(object sender, EventArgs e)
        {
            AxeDTO axe = lstAxes.SelectedItem as AxeDTO;

            if (axe != null)
            {
                axe.CrossHair = ckShowCrossHair.Checked;
                UpdateLayoutElementInfo();
            }
        }

        private void cmbAxeLabel_SelectedIndexChanged(object sender, EventArgs e)
        {
            AxeDTO axe = lstAxes.SelectedItem as AxeDTO;
            var selectedItem = cmbAxeLabel.SelectedItem;

            if (axe != null && selectedItem != null && cmbAxeLabel.Text != "" && axe.Label != cmbAxeLabel.Text)
            {
                string oldLabel = axe.Label;
                axe.Label = ((KeyValuePair<string, string>)selectedItem).Key;

                if (axe.Title == "New Axe" || axe.Title == oldLabel)
                {
                    //txtAxeName.Text = axe.Label;
                    //txtAxeName_Leave(this, null);
                    txtAxeTitle.Text = ((KeyValuePair<string, string>)selectedItem).Value;
                }
                UpdateLayoutElementInfo();
            }
        }

        /// <summary>
        ///Método não usado no gráfico telerik
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void cmbAxeType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    AxeDTO axe = lstAxes.SelectedItem as AxeDTO;

        //    if (axe != null)
        //    {
        //        axe.Type = cmbAxeType.Text;
        //        UpdateLayoutElementInfo();
        //    }
        //}

        private void UpdateLstAxesDataSource(AxeDTO axe)
        {
            lstAxes.DataSource = null;
            lstAxes.DataSource = new BindingSource { DataSource = chartInfo.Axes };
            lstAxes.DisplayMember = "Name";

            //UpdateCmbSeriesAxys();

            if (axe != null)
                lstAxes.SelectedItem = axe;
            else if (chartInfo.Axes.Count > 0)
                lstAxes.SelectedItem = chartInfo.Axes[0];
            else
                lstAxes.SelectedItem = null;

            UpdateControls();
        }

        private void btnAddAxeProp_Click(object sender, EventArgs e)
        {
            AxeDTO axe = lstAxes.SelectedItem as AxeDTO;

            if (axe != null)
            {
                axe.Properties.Add(new PropertyDTO() { UIDRef = axe.UID, Lib = Linx.Dsl.Components.Enums.LibEnum.Telerik, Key = "New Property" });
                UpdateGridAxeProperties();
                dgvAxesProperties.BeginEdit(false);
            }
        }

        private void btnRemoveAxeProp_Click(object sender, EventArgs e)
        {
            if (dgvAxesProperties.CurrentRow == null)
                return;

            PropertyDTO property = dgvAxesProperties.CurrentRow.DataBoundItem as PropertyDTO;
            AxeDTO axe = lstAxes.SelectedItem as AxeDTO;

            if (property != null && axe != null)
            {
                axe.Properties.Remove(property);
                UpdateGridAxeProperties();
                UpdateLayoutElementInfo();
            }
        }

        private void UpdateGridAxeProperties()
        {
            AxeDTO axe = lstAxes.SelectedItem as AxeDTO;

            if (!dgvAxesProperties.EditingControl.IsNull())
                dgvAxesProperties.EndEdit();

            dgvAxesProperties.DataSource = null;

            if (axe != null)
            {
                dgvAxesProperties.DataSource = new BindingSource { DataSource = axe.Properties };
            }

            if (dgvAxesProperties.Rows.Count > 0)
                dgvAxesProperties.CurrentCell = dgvAxesProperties.Rows[dgvAxesProperties.Rows.Count - 1].Cells[0];
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateGridAxeProperties();
        }

        #endregion

        #region SeriesInfo

        private void btnAddSeries_Click(object sender, EventArgs e)
        {
            SerieDTO series = new SerieDTO() { Name = "New Series", Type = chartInfo.ChartType };
            chartInfo.Series.Add(series);
            UpdateLstSeriesDataSource(series);
        }

        private void btnRemoveSeries_Click(object sender, EventArgs e)
        {
            SerieDTO series = lstSeries.SelectedItem as SerieDTO;

            if (series != null)
                chartInfo.Series.Remove(series);

            UpdateLstSeriesDataSource(null);
            UpdateLayoutElementInfo();
        }

        private void UpdateLstSeriesDataSource(SerieDTO series)
        {
            lstSeries.DataSource = null;
            lstSeries.DataSource = new BindingSource { DataSource = chartInfo.Series };
            lstSeries.DisplayMember = "Name";

            if (series != null)
                lstSeries.SelectedItem = series;
            else if (chartInfo.Series.Count > 0)
                lstSeries.SelectedItem = chartInfo.Series[0];
            else
                lstSeries.SelectedItem = null;

            UpdateControls();
        }

        private void txtSeriesTitle_TextChanged(object sender, EventArgs e)
        {
            SerieDTO series = lstSeries.SelectedItem as SerieDTO;

            if (series != null)
            {
                series.Title = txtSeriesTitle.Text;
                UpdateLayoutElementInfo();
            }
        }

        private void txtFormatSerie_TextChanged(object sender, EventArgs e)
        {
            SerieDTO series = lstSeries.SelectedItem as SerieDTO;

            if (series != null)
            {
                series.Format = txtFormatSerie.Text;
                UpdateLayoutElementInfo();
            }
        }

        private void cmbSeriesValueMemberPath_SelectedIndexChanged(object sender, EventArgs e)
        {
            SerieDTO series = lstSeries.SelectedItem as SerieDTO;
            var selectedItem = cmbSeriesValueMemberPath.SelectedItem;

            if (series != null && selectedItem != null && cmbSeriesValueMemberPath.Text != "" && series.ValueMemberPath != cmbSeriesValueMemberPath.Text)
            {
                string oldValueMemberPath = series.ValueMemberPath;
                series.ValueMemberPath = ((KeyValuePair<string, string>)selectedItem).Key;

                if (series.Title == "New Series" || series.Title == oldValueMemberPath)
                {
                    txtSeriesTitle.Text = ((KeyValuePair<string, string>)selectedItem).Value;
                }
                UpdateLayoutElementInfo();
            }
        }

        private void ckShowTooltip_CheckedChanged(object sender, EventArgs e)
        {
            SerieDTO series = lstSeries.SelectedItem as SerieDTO;

            if (series != null)
            {
                series.Tooltip = ckShowTooltip.Checked;
                UpdateLayoutElementInfo();
            }
        }

        private void ckEnableMultType_CheckedChanged(object sender, EventArgs e)
        {
            SerieDTO series = lstSeries.SelectedItem as SerieDTO;

            if (series != null)
            {
                series.EnableMultiType = ckEnableMultType.Checked;
                UpdateLayoutElementInfo();
            }
            if (!ckEnableMultType.Checked)
            {
                cmbMultiType.Text = string.Empty;
                //if (cmbMultiType.Text == string.Empty)
                //    preencheMultiType();
            }


        }

        private void cmbMultiType_SelectedIndexChanged(object sender, EventArgs e)
        {
            preencheMultiType();
            SerieDTO series = lstSeries.SelectedItem as SerieDTO;

            var selectedValue = cmbMultiType.Text;

            if (series != null)
            {
                series.MultiType = selectedValue;
                UpdateLayoutElementInfo();
            }
        }

        private void preencheMultiType()
        {
            if (cmbMultiType.Items.Count < 1)
            {

                LstLabelMultiType.Add(new LabelMultiType() { Description = string.Empty, Value = string.Empty });
                LstLabelMultiType.Add(new LabelMultiType() { Description = "column", Value = "column" });
                LstLabelMultiType.Add(new LabelMultiType() { Description = "bar", Value = "bar" });
                LstLabelMultiType.Add(new LabelMultiType() { Description = "line", Value = "line" });
                LstLabelMultiType.Add(new LabelMultiType() { Description = "area", Value = "area" });

                cmbMultiType.DataSource = LstLabelMultiType;
                cmbMultiType.DisplayMember = "Description";
            }
        }

        private void lstSeries_SelectedIndexChanged(object sender, EventArgs e)
        {
            VerificaCarregamentoMultiType();

            SerieDTO series = lstSeries.SelectedItem as SerieDTO;

            if (series != null)
            {

                txtSeriesTitle.Text = series.Title;
                txtFormatSerie.Text = series.Format;
                ckShowTooltip.Checked = series.Tooltip;
                cmbSeriesValueMemberPath.Text = series.ValueMemberPath;
                ckEnableMultType.Checked = series.EnableMultiType;
                cmbMultiType.Text = series.MultiType;

            }
        }

        private void VerificaCarregamentoMultiType()
        {
            if (chartInfo.ChartType == "bar" || chartInfo.ChartType == "column" || chartInfo.ChartType == "line" || chartInfo.ChartType == "area")
                preencheMultiType();

        }

        private void UpdateGridSeriesProperties()
        {
            SerieDTO series = lstSeries.SelectedItem as SerieDTO;

            dgvSeriesProperties.EndEdit();
            dgvSeriesProperties.DataSource = null;

            if (series != null)
                dgvSeriesProperties.DataSource = new BindingSource { DataSource = series.Properties };

            if (dgvSeriesProperties.Rows.Count > 0)
                dgvSeriesProperties.CurrentCell = dgvSeriesProperties.Rows[dgvSeriesProperties.Rows.Count - 1].Cells[0];
        }

        private void tabSeriesInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateGridSeriesProperties();
        }

        private void btnAddSeriesProp_Click(object sender, EventArgs e)
        {
            SerieDTO series = lstSeries.SelectedItem as SerieDTO;

            if (series != null)
            {
                series.Properties.Add(new PropertyDTO() { UIDRef = series.UID, Lib = Linx.Dsl.Components.Enums.LibEnum.Telerik, Key = "New Property" });
                UpdateGridSeriesProperties();
                dgvSeriesProperties.BeginEdit(false);
            }

        }

        private void btnRemoveSeriesProp_Click(object sender, EventArgs e)
        {
            if (dgvSeriesProperties.CurrentRow == null)
                return;

            PropertyDTO property = dgvSeriesProperties.CurrentRow.DataBoundItem as PropertyDTO;
            SerieDTO series = lstSeries.SelectedItem as SerieDTO;

            if (property != null && series != null)
            {
                series.Properties.Remove(property);
                UpdateGridSeriesProperties();
                UpdateLayoutElementInfo();
            }
        }

        #endregion

        private void cmbRotacaoLabelAxe_SelectedIndexChanged(object sender, EventArgs e)
        {
            LabelRotacaoAxe axesRotacao = cmbRotacaoLabelAxe.SelectedItem as LabelRotacaoAxe;
            chartInfo.RotacaoAxe = axesRotacao != null ? axesRotacao.Value : LstRotacaoLabelAxe[0].Description;
            UpdateLayoutElementInfo();

        }

        private void cmbRotacaoLabelSerie_SelectedIndexChanged(object sender, EventArgs e)
        {
            LabelRotacaoSerie seriesRotacao = cmbRotacaoLabelSerie.SelectedItem as LabelRotacaoSerie;
            chartInfo.RotacaoSerie = seriesRotacao != null ? seriesRotacao.Value : LstRotacaoLabelAxe[0].Description;
            UpdateLayoutElementInfo();
        }

        private void chkAxeGrouped_CheckedChanged(object sender, EventArgs e)
        {
            AxeDTO axe = lstAxes.SelectedItem as AxeDTO;
            var hasGroup = Convert.ToBoolean(lstAxes.Items.Cast<object>().Where(x => Convert.ToBoolean(x.GetPropertyValue("GroupAxe")) == true).Select(x => x.GetPropertyValue("GroupAxe")).FirstOrDefault());
            if (axe != null)
            {
                if (hasGroup && !axe.GroupAxe)
                {
                    axe.GroupAxe = false;
                    chkAxeGrouped.Checked = false;
                }
                else
                    axe.GroupAxe = chkAxeGrouped.Checked;

                UpdateLayoutElementInfo();

            }
        }

        private void txtFieldSort_TextChanged(object sender, EventArgs e)
        {
            chartInfo.SortField = txtFieldSort.Text;
            UpdateLayoutElementInfo();
        }

        private void txtDirSort_TextChanged(object sender, EventArgs e)
        {
            chartInfo.SortDir = txtDirSort.Text;
            UpdateLayoutElementInfo();
        }

        private void groupSort_Leave(object sender, EventArgs e)
        {
            
            string[] arrField = txtFieldSort.Text.Split(';');
            string[] arrDir = txtDirSort.Text.Split(';');

            IEnumerable<string> difArrField = arrField.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();
            IEnumerable<string> difArrDir = arrDir.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();

            if (difArrField.Count() != difArrDir.Count())
            {
                MessageBox.Show("O número de propriedades do Field Sort deve ser o mesmo do Dir Sort.");
                  //  txtFieldSort.Focus(); Retirado o focus do campo para não entrar em loop.
            }
        }

    }

    public class ChartGroupTelerik
    {
        public ChartGroupTelerik()
        {
            Types = new List<ChartTypeTelerik>();
            this.HasMemberPath = false;
            this.HasSeriesMemberPath = false;
            this.HasLabelsPosition = false;
            this.HasTitle = true;
            this.HasSubtitle = false;
            this.HasLegend = false;
            this.HasPositionLegend = false;
            this.HasMultiAxis = true;
            this.HasMultiType = false;
            this.HasRotacaoAxe = false;
            this.HasRotacaoSerie = false;

        }

        public String Group { get; set; }
        public List<ChartTypeTelerik> Types { get; set; }
        public bool HasAxes { get; set; }
        public bool HasSeries { get; set; }
        public bool HasMemberPath { get; set; }
        public bool HasSeriesMemberPath { get; set; }
        public bool HasLabelsPosition { get; set; }
        public bool HasTitle { get; set; }
        public bool HasSubtitle { get; set; }
        public bool HasLegend { get; set; }
        public bool HasPositionLegend { get; set; }
        public bool HasMultiAxis { get; set; }
        public bool HasMultiType { get; set; }
        public bool HasRotacaoAxe { get; set; }
        public bool HasRotacaoSerie { get; set; }
    }

    public class ChartTypeTelerik
    {
        public String Description { get; set; }
        public String Type { get; set; }
    }

    public class LabelPositionTelerik
    {
        public String Description { get; set; }
        public String Value { get; set; }
    }

    public class LabelMultiType
    {
        public String Description { get; set; }
        public String Value { get; set; }
    }
    
    public class LabelRotacaoAxe
    {
        public String Description { get; set; }
        public String Value { get; set; }
    }
    
    public class LabelRotacaoSerie
    {
        public String Description { get; set; }
        public String Value { get; set; }
    }
}
