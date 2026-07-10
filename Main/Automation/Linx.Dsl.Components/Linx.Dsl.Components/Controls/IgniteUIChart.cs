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
    public partial class IgniteUIChart : UserControl
    {
        private List<ChartGroup> ChartGroups;
        private ChartDTO chartInfo = new ChartDTO();
        private List<LabelPosition> LstLabelPosition = new List<LabelPosition>();
        private LayoutElement _chartLayout { get; set; }
        private bool disableUpdateLayout = false;

        public LayoutElement ChartLayout
        {
            get { return _chartLayout; }
            set
            {
                _chartLayout = value;
                Deserialize();
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

        public IgniteUIChart()
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
                _chartLayout.InternalDefinition = Linx.Tools.SerializationManager<ChartDTO>.ObjectToString(chartInfo);
                _chartLayout.ScriptDefinition = Linx.Dsl.Components.Common.IgniteUIChartBuilder.BuilderJS(chartInfo, Linx.Dsl.Components.Enums.LibEnum.IgniteUI);
            }
        }

        private void LoadChartInfo()
        {
            ChartGroups = new List<ChartGroup>();

            ChartGroup group = new ChartGroup() { Group = "Bar and Column Series", HasAxes = true, HasSeries = true };
            group.Types.Add(new ChartType() { Type = "bar", Description = "Bar" });
            group.Types.Add(new ChartType() { Type = "column", Description = "Column" });
            ChartGroups.Add(group);

            group = new ChartGroup() { Group = "Category Series", HasAxes = true, HasSeries = true };
            group.Types.Add(new ChartType() { Type = "area", Description = "Area" });
            group.Types.Add(new ChartType() { Type = "column", Description = "Column" });
            group.Types.Add(new ChartType() { Type = "line", Description = "Line" });
            group.Types.Add(new ChartType() { Type = "splineArea", Description = "Spline Area" });
            group.Types.Add(new ChartType() { Type = "spline", Description = "Spline" });
            group.Types.Add(new ChartType() { Type = "stepArea", Description = "Step Area" });
            group.Types.Add(new ChartType() { Type = "stepLine", Description = "Step Line" });
            group.Types.Add(new ChartType() { Type = "waterfall", Description = "Waterfall" });
            group.Types.Add(new ChartType() { Type = "point", Description = "Point" });
            ChartGroups.Add(group);

            group = new ChartGroup() { Group = "Range Category Series", HasAxes = true, HasSeries = true, HasMemberPath = false, HasSeriesMemberPath = true };
            group.Types.Add(new ChartType() { Type = "rangeArea", Description = "Range Area" });
            group.Types.Add(new ChartType() { Type = "rangeColumn", Description = "Range Column" });
            ChartGroups.Add(group);

            group = new ChartGroup() { Group = "Stacked Series", HasAxes = true, HasSeries = true };
            group.Types.Add(new ChartType() { Type = "stackedBar", Description = "Stacked Bar" });
            group.Types.Add(new ChartType() { Type = "stacked100Bar", Description = "Stacked 100 Bar" });
            group.Types.Add(new ChartType() { Type = "stackedArea", Description = "Stacked Area" });
            group.Types.Add(new ChartType() { Type = "stackedColumn", Description = "Stacked Column" });
            group.Types.Add(new ChartType() { Type = "stackedLine", Description = "Stacked Line" });
            group.Types.Add(new ChartType() { Type = "stackedSpline", Description = "Stacked Spline" });
            group.Types.Add(new ChartType() { Type = "stackedSplineArea", Description = "Stacked Spline Area" });
            group.Types.Add(new ChartType() { Type = "stacked100Area", Description = "Stacked 100 Area" });
            group.Types.Add(new ChartType() { Type = "stacked100Column", Description = "Stacked 100 Column" });
            group.Types.Add(new ChartType() { Type = "stacked100Line", Description = "Stacked 100 Line" });
            group.Types.Add(new ChartType() { Type = "stacked100Spline", Description = "Stacked 100 Spline" });
            group.Types.Add(new ChartType() { Type = "stacked100SplineArea", Description = "Stacked 100 Spline Area" });
            ChartGroups.Add(group);

            #region Disabled Graphs

            //group = new ChartGroup() { Group = "Scatter Series (Bubble)" };
            //group.Types.Add(new ChartType() { Type = "bubbleChart", Description = "Bubble Chart" });
            //group.Types.Add(new ChartType() { Type = "scatter", Description = "Scatter" });
            //group.Types.Add(new ChartType() { Type = "scatterLine", Description = "Scatter Line" });
            //group.Types.Add(new ChartType() { Type = "scatterSpline", Description = "Scatter Spline" });
            //ChartGroups.Add(group);

            //group = new ChartGroup() { Group = "Financial Series" };
            //group.Types.Add(new ChartType() { Type = "candlestick", Description = "Candlestick" });
            //group.Types.Add(new ChartType() { Type = "oHLC", Description= "OHLC" });
            //ChartGroups.Add(group);

            //group = new ChartGroup() { Group = "Polar Series" };
            //group.Types.Add(new ChartType() { Type = "polarScatter", Description= "Polar Scatter" });
            //group.Types.Add(new ChartType() { Type = "polarLine", Description= "Polar Line" });
            //group.Types.Add(new ChartType() { Type = "polarArea", Description= "Polar Area" });
            //group.Types.Add(new ChartType() { Type = "polarSpline", Description= "Polar Spline" });
            //group.Types.Add(new ChartType() { Type = "polarSplineArea", Description= "Polar Spline Area" });
            //ChartGroups.Add(group);

            //group = new ChartGroup() { Group = "Radial Series" };
            //group.Types.Add(new ChartType() { Type = "radialLine", Description= "Radial Line" });
            //group.Types.Add(new ChartType() { Type = "radialColumn", Description= "Radial Column" });
            //group.Types.Add(new ChartType() { Type = "radialPie", Description= "Radial Pie" });
            //group.Types.Add(new ChartType() { Type = "radialArea", Description= "Radial Area" });
            //ChartGroups.Add(group);

            #endregion

            group = new ChartGroup() { Group = "Pie Chart", HasAxes = false, HasSeries = false, HasMemberPath = true, HasSeriesMemberPath = false, HasLabelsPosition = true, HasTitle = false, HasSubtitle = false, HasLegend = false };
            group.Types.Add(new ChartType() { Type = "pieChart", Description = "Pie Chart" });
            ChartGroups.Add(group);

            group = new ChartGroup() { Group = "Doughnut Chart", HasAxes = false, HasSeries = true, HasMemberPath = true, HasSeriesMemberPath = false, HasLabelsPosition = false };
            group.Types.Add(new ChartType() { Type = "doughnut Chart", Description = "Doughnut Chart" });
            ChartGroups.Add(group);

            cmbChartGroup.DataSource = ChartGroups;
            cmbChartGroup.DisplayMember = "Group";

            cmbChartType.DataSource = ChartGroups;
            cmbChartType.DisplayMember = "Types.Description";

            //Label Position
            LstLabelPosition.Add(new LabelPosition() { Description = "Center", Value = "center" });
            LstLabelPosition.Add(new LabelPosition() { Description = "Inside End", Value = "insideEnd" });
            LstLabelPosition.Add(new LabelPosition() { Description = "Outside End", Value = "outsideEnd" });
            LstLabelPosition.Add(new LabelPosition() { Description = "Best Fit", Value = "bestFit" });

            cmbLabelsPosition.DataSource = LstLabelPosition;
            cmbLabelsPosition.DisplayMember = "Description";

            //Properties
            dgvChartProperties.AutoGenerateColumns = false;
            dgvAxesProperties.AutoGenerateColumns = false;
            dgvSeriesProperties.AutoGenerateColumns = false;
        }

        private void cmbChartGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChartGroup chartGroup = cmbChartGroup.SelectedItem as ChartGroup;
            if (chartGroup != null)
            {
                chartInfo.ChartGroup = chartGroup.Group;

                if (txtWidth.Text == string.Empty)
                    txtWidth.Text = "100%";

                if (txtHeight.Text == string.Empty)
                    txtHeight.Text = "100%";

                //Axes
                groupAxes.Enabled = chartGroup.HasAxes;
                if (!chartGroup.HasAxes)
                {
                    chartInfo.Axes.Clear();
                    UpdateLstAxesDataSource(null);
                    txtAxeName.Text = string.Empty;
                    txtAxeTitle.Text = string.Empty;
                    cmbAxeType.SelectedItem = null;
                    tabAxesInfo.SelectedIndex = 0;
                }

                //Series
                groupSeries.Enabled = chartGroup.HasSeries;
                if (!chartGroup.HasSeries)
                {
                    chartInfo.Series.Clear();
                    UpdateLstSeriesDataSource(null);
                    txtSeriesName.Text = string.Empty;
                    txtSeriesTitle.Text = string.Empty;
                    cmbSeriesxAxys.Text = string.Empty;
                    cmbSeriesyAxys.Text = string.Empty;
                    ckShowTooltip.Checked = false;
                    tabSeriesInfo.SelectedIndex = 0;
                }

                //Member Path
                groupMemberPath.Enabled = chartGroup.HasMemberPath;

                if (!chartGroup.HasMemberPath)
                {
                    txtLabelMemberPath.Text = string.Empty;
                    txtValueMemberPath.Text = string.Empty;
                }

                //Title
                txtTitle.Enabled = chartGroup.HasTitle;
                txtTitle.Text = chartGroup.HasTitle ? txtTitle.Text : string.Empty;

                //Subtitle
                txtSubTitle.Enabled = chartGroup.HasSubtitle;
                txtSubTitle.Text = chartGroup.HasSubtitle ? txtSubTitle.Text : string.Empty;

                //legend
                ckLegendEnabled.Enabled = chartGroup.HasLegend;
                ckLegendEnabled.Checked = chartGroup.HasLegend ? ckLegendEnabled.Checked : false;

                //Series Member Path
                if (!chartGroup.HasMemberPath)
                {
                    foreach (SerieDTO series in chartInfo.Series)
                    {
                        series.LowMemberPath = string.Empty;
                        series.HighMemberPath = string.Empty;
                    }
                }

                //Labels Position
                cmbLabelsPosition.Enabled = chartGroup.HasLabelsPosition;

                if (!chartGroup.HasLabelsPosition)
                    cmbLabelsPosition.SelectedItem = null;

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
            ChartType chartType = cmbChartType.SelectedItem as ChartType;

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

        private void txtSubTitle_TextChanged(object sender, EventArgs e)
        {
            chartInfo.SubTitle = txtSubTitle.Text;
            UpdateLayoutElementInfo();
        }

        private void txtWidth_TextChanged(object sender, EventArgs e)
        {
            chartInfo.Width = txtWidth.Text;
            UpdateLayoutElementInfo();
        }

        private void txtHeight_TextChanged(object sender, EventArgs e)
        {
            chartInfo.Height = txtHeight.Text;
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

        private void cmbLabelsPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            LabelPosition labelPosition = cmbLabelsPosition.SelectedItem as LabelPosition;
            chartInfo.LabelsPosition = labelPosition != null ? labelPosition.Value : string.Empty;
            UpdateLayoutElementInfo();
        }

        private void ckLegendEnabled_CheckedChanged(object sender, EventArgs e)
        {
            chartInfo.Legend.Enabled = ckLegendEnabled.Checked;

            if (!chartInfo.Legend.Enabled)
            {
                txtLegendWidth.Text = string.Empty;
                txtLegendHeight.Text = string.Empty;
                cmbLegendType.Text = string.Empty;
            }
            else
            {
                if (txtLegendWidth.Text == string.Empty)
                    txtLegendWidth.Text = "100%";

                if (txtLegendHeight.Text == string.Empty)
                    txtLegendHeight.Text = "100%";
            }

            txtLegendWidth.Enabled = chartInfo.Legend.Enabled;
            txtLegendHeight.Enabled = chartInfo.Legend.Enabled;
            cmbLegendType.Enabled = chartInfo.Legend.Enabled;

            UpdateLayoutElementInfo();
        }

        private void txtLegendWidth_TextChanged(object sender, EventArgs e)
        {
            chartInfo.Legend.Width = txtLegendWidth.Text;
            UpdateLayoutElementInfo();
        }

        private void txtLegendHeight_TextChanged(object sender, EventArgs e)
        {
            chartInfo.Legend.Height = txtLegendHeight.Text;
            UpdateLayoutElementInfo();
        }

        private void cmbLegendType_SelectedIndexChanged(object sender, EventArgs e)
        {
            chartInfo.Legend.Type = cmbLegendType.Text;
            UpdateLayoutElementInfo();
        }

        private void btnAddChartProp_Click(object sender, EventArgs e)
        {
            chartInfo.Properties.Add(new PropertyDTO() { UIDRef = chartInfo.UID, Lib = Linx.Dsl.Components.Enums.LibEnum.IgniteUI, Key = "New Property" });
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
            //Legend
            txtLegendWidth.Enabled = chartInfo.Legend.Enabled;
            txtLegendHeight.Enabled = chartInfo.Legend.Enabled;
            cmbLegendType.Enabled = chartInfo.Legend.Enabled;

            //Axes
            txtAxeName.Enabled = chartInfo.Axes.Count > 0;
            txtAxeTitle.Enabled = chartInfo.Axes.Count > 0;
            cmbAxeType.Enabled = chartInfo.Axes.Count > 0;
            cmbAxeLabel.Enabled = chartInfo.Axes.Count > 0;

            if (chartInfo.Axes.Count == 0)
                ClearAxeInfo();

            //Series
            ChartGroup chartGroup = cmbChartGroup.SelectedItem as ChartGroup;
            txtSeriesName.Enabled = chartInfo.Series.Count > 0;
            txtSeriesTitle.Enabled = chartInfo.Series.Count > 0;
            cmbSeriesxAxys.Enabled = chartInfo.Series.Count > 0;
            cmbSeriesyAxys.Enabled = chartInfo.Series.Count > 0;
            cmbSeriesValueMemberPath.Enabled = chartInfo.Series.Count > 0;
            groupSeriesMemberPath.Enabled = (chartInfo.Series.Count > 0 && chartGroup != null && chartGroup.HasSeriesMemberPath);

            if (chartInfo.Series.Count == 0)
                ClearSeriesInfo();
        }

        private void ClearAxeInfo()
        {
            txtAxeName.Text = string.Empty;
            txtAxeTitle.Text = string.Empty;
            cmbAxeLabel.Text = string.Empty;
            cmbAxeType.SelectedItem = null;
            tabAxesInfo.SelectedIndex = 0;
        }

        private void ClearSeriesInfo()
        {
            txtSeriesName.Text = string.Empty;
            txtSeriesTitle.Text = string.Empty;
            cmbSeriesxAxys.Text = string.Empty;
            cmbSeriesyAxys.Text = string.Empty;
            ckShowTooltip.Checked = false;
            tabSeriesInfo.SelectedIndex = 0;
            txtSeriesHighMemberPath.Text = string.Empty;
            txtSeriesLowMemberPath.Text = string.Empty;
            cmbSeriesValueMemberPath.Text = string.Empty;
        }

        private void Deserialize()
        {
            disableUpdateLayout = true;

            ChartDTO chart = new ChartDTO();
            chartInfo = new ChartDTO();
            ControlClear();

            if (!_chartLayout.IsNull() && !_chartLayout.InternalDefinition.IsNullOrEmpty())
                chart = Linx.Tools.SerializationManager<ChartDTO>.StringToObject(_chartLayout.InternalDefinition);

            if (chart.ChartGroup.IsNullOrEmpty())
            {
                disableUpdateLayout = false;
                cmbChartGroup.Text = ChartGroups[0].Group;
                return;
            }

            //ChartInfo
            ChartGroup group = ChartGroups.Where(i => i.Group == chart.ChartGroup).FirstOrDefault();
            ChartType chartType = group.Types.Where(i => i.Type == chart.ChartType).FirstOrDefault();

            cmbChartGroup.SelectedItem = group;
            cmbChartType.SelectedItem = chartType;
            txtTitle.Text = chart.Title;
            txtSubTitle.Text = chart.SubTitle;
            txtWidth.Text = chart.Width;
            txtHeight.Text = chart.Height;
            txtLabelMemberPath.Text = chart.LabelMemberPath;
            txtValueMemberPath.Text = chart.ValueMemberPath;
            cmbLabelsPosition.Text = chart.LabelsPosition;
            chartInfo.Properties = chart.Properties;
            UpdateGridChartProperties();
            chartInfo.Legend = chart.Legend == null ? new LegendDTO() : chart.Legend;
            ckLegendEnabled.Checked = chartInfo.Legend.Enabled;
            txtLegendWidth.Text = chartInfo.Legend.Width;
            txtLegendHeight.Text = chartInfo.Legend.Height;
            cmbLegendType.Text = chartInfo.Legend.Type;

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
            txtSubTitle.Text = string.Empty;
            txtWidth.Text = string.Empty;
            txtHeight.Text = string.Empty;
            txtLabelMemberPath.Text = string.Empty;
            txtValueMemberPath.Text = string.Empty;
            cmbLabelsPosition.SelectedItem = null;
            ckLegendEnabled.Checked = false;
            txtLegendWidth.Text = string.Empty;
            txtLegendHeight.Text = string.Empty;
            cmbLegendType.Text = string.Empty;
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
                txtAxeName.Text = axe.Name;
                cmbAxeType.Text = axe.Type;
                txtAxeTitle.Text = axe.Title;
                cmbAxeLabel.Text = axe.Label;
            }
        }

        private void txtAxeName_Leave(object sender, EventArgs e)
        {
            AxeDTO axe = lstAxes.SelectedItem as AxeDTO;

            if (axe != null && axe.Name != txtAxeName.Text)
            {
                string oldName = axe.Name;
                axe.Name = txtAxeName.Text;

                var xSeries = chartInfo.Series.Where(i => i.xAxis == oldName);
                var ySeries = chartInfo.Series.Where(i => i.yAxis == oldName);

                //Update series
                xSeries.Foreach(i =>
                    {
                        i.xAxis = axe.Name;
                    });

                ySeries.Foreach(i =>
                    {
                        i.yAxis = axe.Name;
                    });

                UpdateLstAxesDataSource(axe);
                UpdateLayoutElementInfo();
            }
        }

        private void txtAxeTitle_TextChanged(object sender, EventArgs e)
        {
            AxeDTO axe = lstAxes.SelectedItem as AxeDTO;

            if (axe != null)
            {
                axe.Title = txtAxeTitle.Text;
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

                if (axe.Name == "New Axe" || axe.Name == oldLabel)
                {
                    txtAxeName.Text = axe.Label;
                    txtAxeName_Leave(this, null);
                    txtAxeTitle.Text = ((KeyValuePair<string, string>)selectedItem).Value;
                }
                UpdateLayoutElementInfo();
            }
        }

        private void cmbAxeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            AxeDTO axe = lstAxes.SelectedItem as AxeDTO;

            if (axe != null)
            {
                axe.Type = cmbAxeType.Text;
                UpdateLayoutElementInfo();
            }
        }

        private void UpdateLstAxesDataSource(AxeDTO axe)
        {
            lstAxes.DataSource = null;
            lstAxes.DataSource = new BindingSource { DataSource = chartInfo.Axes };
            lstAxes.DisplayMember = "Name";

            UpdateCmbSeriesAxys();

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
                axe.Properties.Add(new PropertyDTO() { UIDRef = axe.UID, Lib = Linx.Dsl.Components.Enums.LibEnum.IgniteUI, Key = "New Property" });
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

        private void txtSeriesName_Leave(object sender, EventArgs e)
        {
            SerieDTO series = lstSeries.SelectedItem as SerieDTO;

            if (series != null && series.Name != txtSeriesName.Text)
            {
                series.Name = txtSeriesName.Text;
                UpdateLstSeriesDataSource(series);
                UpdateLayoutElementInfo();
            }
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

        private void cmbSeriesValueMemberPath_SelectedIndexChanged(object sender, EventArgs e)
        {
            SerieDTO series = lstSeries.SelectedItem as SerieDTO;
            var selectedItem = cmbSeriesValueMemberPath.SelectedItem;

            if (series != null && selectedItem != null && cmbSeriesValueMemberPath.Text != "" && series.ValueMemberPath != cmbSeriesValueMemberPath.Text)
            {
                string oldValueMemberPath = series.ValueMemberPath;
                series.ValueMemberPath = ((KeyValuePair<string, string>)selectedItem).Key;

                if (series.Name == "New Series" || series.Name == oldValueMemberPath)
                {
                    txtSeriesName.Text = series.ValueMemberPath;
                    txtSeriesName_Leave(this, null);
                    txtSeriesTitle.Text = ((KeyValuePair<string, string>)selectedItem).Value;
                }
                UpdateLayoutElementInfo();
            }

        }

        private void txtSeriesLowMemberPath_TextChanged(object sender, EventArgs e)
        {
            SerieDTO series = lstSeries.SelectedItem as SerieDTO;

            if (series != null)
            {
                series.LowMemberPath = txtSeriesLowMemberPath.Text;
                UpdateLayoutElementInfo();
            }
        }

        private void txtSeriesHighMemberPath_TextChanged(object sender, EventArgs e)
        {
            SerieDTO series = lstSeries.SelectedItem as SerieDTO;

            if (series != null)
            {
                series.HighMemberPath = txtSeriesHighMemberPath.Text;
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

        private void lstSeries_SelectedIndexChanged(object sender, EventArgs e)
        {
            SerieDTO series = lstSeries.SelectedItem as SerieDTO;

            if (series != null)
            {
                txtSeriesName.Text = series.Name;
                txtSeriesTitle.Text = series.Title;
                cmbSeriesxAxys.Text = series.xAxis;
                cmbSeriesyAxys.Text = series.yAxis;
                ckShowTooltip.Checked = series.Tooltip;
                txtSeriesLowMemberPath.Text = series.LowMemberPath;
                txtSeriesHighMemberPath.Text = series.HighMemberPath;
                cmbSeriesValueMemberPath.Text = series.ValueMemberPath;
            }
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
                series.Properties.Add(new PropertyDTO() { UIDRef = series.UID, Lib = Linx.Dsl.Components.Enums.LibEnum.IgniteUI, Key = "New Property" });
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

        private void cmbSeriesxAxys_SelectedIndexChanged(object sender, EventArgs e)
        {
            SerieDTO series = lstSeries.SelectedItem as SerieDTO;
            if (series != null && cmbSeriesxAxys.Text != string.Empty)
            {
                series.xAxis = cmbSeriesxAxys.Text;
                UpdateLayoutElementInfo();
            }
        }

        private void cmbSeriesyAxys_SelectedIndexChanged(object sender, EventArgs e)
        {
            SerieDTO series = lstSeries.SelectedItem as SerieDTO;

            if (series != null && cmbSeriesyAxys.Text != string.Empty)
            {
                series.yAxis = cmbSeriesyAxys.Text;
                UpdateLayoutElementInfo();
            }
        }

        private void UpdateCmbSeriesAxys()
        {
            SerieDTO series = lstSeries.SelectedItem as SerieDTO;

            cmbSeriesxAxys.Items.Clear();
            cmbSeriesyAxys.Items.Clear();

            cmbSeriesxAxys.Items.Add("");
            cmbSeriesyAxys.Items.Add("");

            foreach (AxeDTO axe in chartInfo.Axes)
            {
                cmbSeriesxAxys.Items.Add(axe.Name);
                cmbSeriesyAxys.Items.Add(axe.Name);
            }

            if (series != null)
            {
                cmbSeriesxAxys.Text = series.xAxis;
                cmbSeriesyAxys.Text = series.yAxis;
            }
        }

        #endregion

    }

    public class ChartGroup
    {
        public ChartGroup()
        {
            Types = new List<ChartType>();
            this.HasMemberPath = false;
            this.HasSeriesMemberPath = false;
            this.HasLabelsPosition = false;
            this.HasTitle = true;
            this.HasSubtitle = true;
            this.HasLegend = true;
        }

        public String Group { get; set; }
        public List<ChartType> Types { get; set; }
        public bool HasAxes { get; set; }
        public bool HasSeries { get; set; }
        public bool HasMemberPath { get; set; }
        public bool HasSeriesMemberPath { get; set; }
        public bool HasLabelsPosition { get; set; }
        public bool HasTitle { get; set; }
        public bool HasSubtitle { get; set; }
        public bool HasLegend { get; set; }
    }

    public class ChartType
    {
        public String Description { get; set; }
        public String Type { get; set; }
    }

    public class LabelPosition
    {
        public String Description { get; set; }
        public String Value { get; set; }
    }
}
