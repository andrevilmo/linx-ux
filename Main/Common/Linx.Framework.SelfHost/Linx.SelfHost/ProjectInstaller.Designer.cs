namespace Linx.SelfHost
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SelfHostFRWServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.SelfHostFRWServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // SelfHostFRWServiceProcessInstaller
            // 
            this.SelfHostFRWServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.SelfHostFRWServiceProcessInstaller.Password = null;
            this.SelfHostFRWServiceProcessInstaller.Username = null;
            // 
            // SelfHostFRWServiceInstaller
            // 
            this.SelfHostFRWServiceInstaller.Description = "Linx Framework SelfHost";
            this.SelfHostFRWServiceInstaller.DisplayName = "Linx Framework SelfHost";
            this.SelfHostFRWServiceInstaller.ServiceName = "LinxOmniSelfHostFRW";
            this.SelfHostFRWServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.SelfHostFRWServiceProcessInstaller,
            this.SelfHostFRWServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller SelfHostFRWServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller SelfHostFRWServiceInstaller;
    }
}