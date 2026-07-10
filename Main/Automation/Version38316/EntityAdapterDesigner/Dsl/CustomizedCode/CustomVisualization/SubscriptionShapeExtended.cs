using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Linx.EntityAdapterDesigner.CustomizedCode;
using System.Windows.Forms;

namespace Linx.EntityAdapterDesigner
{
    public partial class SubscriptionShape
    {
        public override void OnDoubleClick(Microsoft.VisualStudio.Modeling.Diagrams.DiagramPointEventArgs e)
        {
            foreach (var element in e.DiagramHitTestInfo.HitDiagramItem.RepresentedElements)
            {
                if (element is SubscriptionShape)
                {
                    var subscription = (Subscription)((SubscriptionShape)element).ModelElement;
                    var publisher = subscription.Publisher;
                    if (publisher != null)
                    {
                        publisher.Update();
                        FormPublicationViewer preview = new FormPublicationViewer() { DataStructure = publisher };
                        preview.ShowDialog();
                    }
                    else {
                        MessageBox.Show(string.Format("Unable to load publisher. Make sure the path is correct.\nPath: '{0}'", subscription.BusinessObjectPath), "Publisher Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                break;
            }
            base.OnDoubleClick(e);
        }
    }
}
