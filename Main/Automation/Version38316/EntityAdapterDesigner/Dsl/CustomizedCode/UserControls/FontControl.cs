using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Linx.EntityAdapterDesigner.CustomizedCode.UserControls
{
    public partial class FontControl : UserControl
    {
       
        public event FontControlChangedEventHandler FontPropertyChanged;

        public FontControl()
        {
            InitializeComponent();

            //load Combos
            Style.Items.AddRange(Enum.GetNames(typeof(Linx.Tools.FontForegroundStyle)));
        }

  
        private void HandleFontPropertyChanged(FontProperties property)
        {
            if (FontPropertyChanged != null)
            {
                FontPropertyChanged(this, new FontControlEventArgs(property));
            }
        }

        private void Style_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.HandleFontPropertyChanged(FontProperties.Style);
        }
                             
        private void Highlight_CheckedChanged(object sender, EventArgs e)
        {
            this.HandleFontPropertyChanged(FontProperties.Highlight);
        }
             
        private void bold_CheckedChanged(object sender, EventArgs e)
        {
            this.HandleFontPropertyChanged(FontProperties.Bold);
        }
    }

    public enum FontProperties
    {
        Bold,
        Style,
        Highlight
    }

    public class FontControlEventArgs : EventArgs
    {
        public FontProperties Property { get; set; }

        public FontControlEventArgs(FontProperties fontProperty) : base()
        {
            this.Property = fontProperty;
        }
    }

    public delegate void FontControlChangedEventHandler(FontControl sender, FontControlEventArgs e);
}
