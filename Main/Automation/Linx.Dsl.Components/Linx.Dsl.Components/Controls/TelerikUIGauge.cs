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
    public partial class TelerikUIGauge : UserControl
    {
        private List<GaugeTypeTelerik> gaugesType;
        private GaugeTelerikDTO gaugeInfo = new GaugeTelerikDTO();
        private List<ScalePositionTelerik> lstScalePosition = new List<ScalePositionTelerik>();
        private LayoutElement _gaugeLayout { get; set; }
        private bool disableUpdateLayoutGauge = false;

        public LayoutElement GaugeLayout
        {
            get { return _gaugeLayout; }
            set
            {
                if (_gaugeLayout != value)
                {
                    _gaugeLayout = value;
                    DeserializeInfoGauge();
                }
            }
        }

        //private Dictionary<string, string> _adapterFields;

        public TelerikUIGauge()
        {
            InitializeComponent();
            LoadGaugeInfo();
        }

        #region GaugeInfo

        private void UpdateLayoutElementInfoGauge()
        {
            if (!disableUpdateLayoutGauge && _gaugeLayout != null)
            {
                _gaugeLayout.InternalDefinition = Linx.Tools.SerializationManager<GaugeTelerikDTO>.ObjectToString(gaugeInfo);
                _gaugeLayout.ScriptDefinition = Linx.Dsl.Components.Common.GaugeBuilderTelerik.BuilderGaugeJS(gaugeInfo, Linx.Dsl.Components.Enums.LibEnum.Telerik);
            }
        }

        private void LoadGaugeInfo()
        {
            gaugesType = new List<GaugeTypeTelerik>();

            gaugesType.Add(new GaugeTypeTelerik() { Type = "", Description = "" });
            gaugesType.Add(new GaugeTypeTelerik() { Type = "radial", Description = "Radial" });
            gaugesType.Add(new GaugeTypeTelerik() { Type = "semicircle", Description = "Semi-Circle" });
            gaugesType.Add(new GaugeTypeTelerik() { Type = "semicircleleft", Description = "Semi-Circle Left" });


            cmbGaugeType.DataSource = gaugesType;
            cmbGaugeType.DisplayMember = "Description";

            //Label Position
            lstScalePosition.Add(new ScalePositionTelerik() { Description = "", Value = "" });
            lstScalePosition.Add(new ScalePositionTelerik() { Description = "Outside", Value = "outside" });
            lstScalePosition.Add(new ScalePositionTelerik() { Description = "Inside", Value = "inside" });


            //Legenda
            cmbPosition.DataSource = lstScalePosition;
            cmbPosition.DisplayMember = "Description";

        }

        private void cmbGaugeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            GaugeTypeTelerik gaugeType = cmbGaugeType.SelectedItem as GaugeTypeTelerik;
            gaugeInfo.GaugeType = gaugeType.Description;

            if (gaugeType.Type.ToLower() == "radial")
            {
                gaugeInfo.StartAngle = "0";
                gaugeInfo.EndAngle = "180";
            }
            else if (gaugeType.Type.ToLower() == "semicircle")
            {
                gaugeInfo.StartAngle = "-30";
                gaugeInfo.EndAngle = "210";
            }
            else
            {
                gaugeInfo.StartAngle = "-90";
                gaugeInfo.EndAngle = "90";
            }

            //
            UpdateLayoutElementInfoGauge();
        }

        private void cmbPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScalePositionTelerik scalePosition = cmbPosition.SelectedItem as ScalePositionTelerik;
            gaugeInfo.Position = scalePosition != null ? scalePosition.Value : string.Empty;

            UpdateLayoutElementInfoGauge();
        }

        private void txtLabelFormat_TextChanged(object sender, EventArgs e)
        {
            gaugeInfo.FormatLabel = txtLabelFormat.Text;
            UpdateLayoutElementInfoGauge();
        }


        private void DeserializeInfoGauge()
        {
            
            //if (_gaugeLayout == null)
            //    return;

            disableUpdateLayoutGauge = true;

            GaugeTelerikDTO gauge = new GaugeTelerikDTO();
            gaugeInfo = new GaugeTelerikDTO();


            //ControlClearGauge();

            if (!_gaugeLayout.IsNull() && !_gaugeLayout.InternalDefinition.IsNullOrEmpty())
                gauge = Linx.Tools.SerializationManager<GaugeTelerikDTO>.StringToObject(_gaugeLayout.InternalDefinition);

            if (gauge.GaugeType.IsNullOrEmpty())
            {
                disableUpdateLayoutGauge = false;
                cmbGaugeType.Text = gaugesType[0].Description;
                //return;
            }

            if (gauge.Position.IsNullOrEmpty())
            {
                cmbPosition.Text = lstScalePosition[0].Description;
            }

            GaugeTypeTelerik gauges = gaugesType.Where(i => i.Description == gauge.GaugeType).FirstOrDefault();
            ScalePositionTelerik position = lstScalePosition.Where(i => i.Value == gauge.Position).FirstOrDefault();

            cmbGaugeType.SelectedItem = gauges;
            cmbPosition.SelectedItem = position;
            txtLabelFormat.Text = gauge.FormatLabel;

            gaugeInfo.Properties = gauge.Properties;

            disableUpdateLayoutGauge = false;
        }

        private void ControlClearGauge()
        {
            cmbGaugeType.SelectedItem = null;
            cmbPosition.SelectedItem = null;
            txtLabelFormat.Text = string.Empty;

        }

        #endregion

    }

    
    public class GaugeTypeTelerik
    {
        public String Description { get; set; }
        public String Type { get; set; }
    }

    public class ScalePositionTelerik
    {
        public String Description { get; set; }
        public String Value { get; set; }
    }


}
