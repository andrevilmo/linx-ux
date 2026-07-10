using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ColorPickerCombo;

namespace Linx.EntityAdapterDesigner.CustomizedCode
{
    public partial class FrmKpiValues : Form
    {
        List<KPIRangeValue> kpiRangeValues = new List<KPIRangeValue>(); 
        KeyPerformanceIndicator kpiView;


        public void LoadColorPickerColumn(ref DataGridView dv)
        {
            ColorPickerColumn colColorPick = new ColorPickerColumn();
            colColorPick.Name = "Color";
            colColorPick.DataPropertyName = "Color";
            colColorPick.HeaderText = "Color";
            colColorPick.DisplayIndex = 2;
            dv.SuspendLayout();
            dv.Columns.Add(colColorPick);
            dv.ResumeLayout();            
        }
        
        public FrmKpiValues(KeyPerformanceIndicator kpiView)
            : this()
        {
            this.kpiView = kpiView;            
            this.LoadValues();
            this.LoadColorPickerColumn(ref this.kPIRangeValueDataGridView);
            this.kPIRangeValueBindingSource.DataSource = kpiRangeValues;
        }

        public void LoadValues()
        {
            kpiRangeValues.Clear();
            if (kpiView != null)
            {                
                foreach (var dValue in kpiView.KpiRangeItems)
                {
                    kpiRangeValues.Add(new KPIRangeValue() { Description = dValue.Description, Name = dValue.Name, StartValue = dValue.StartValue, EndValue = dValue.EndValue, Color = Color.FromArgb(dValue.Alpha, dValue.Red, dValue.Green, dValue.Blue) });  
                }
            }
        }

        public FrmKpiValues()
        {
            InitializeComponent();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void btApply_Click(object sender, EventArgs e)
        {
            if (kpiView != null)
            {
                kpiView.KpiRangeItems.Clear();  
                foreach (var item in kpiRangeValues)
                {
                    kpiView.KpiRangeItems.Add(new KpiRangeItem(kpiView.Store) { Description = item.Description, Name = item.Name, StartValue = item.StartValue, EndValue = item.EndValue, Alpha = item.Color.A, Red = item.Color.R, Green = item.Color.G, Blue = item.Color.B }); 
                }
            }

            this.Close(); 
        }
    }


    public class KPIRangeValue
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double StartValue { get; set; }
        public double EndValue { get; set; }
        public Color Color { get; set; }
    }
}
