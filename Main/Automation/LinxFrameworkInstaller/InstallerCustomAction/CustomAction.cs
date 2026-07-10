using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Deployment.WindowsInstaller;
using System.Windows.Forms;

namespace InstallerCustomAction
{
    public class CustomActions
    {
        [CustomAction]
        public static ActionResult ShowEndMessage(Session session)
        {
            MessageBox.Show("Instalação realizada com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

            return ActionResult.Success;
        }
    }
}
