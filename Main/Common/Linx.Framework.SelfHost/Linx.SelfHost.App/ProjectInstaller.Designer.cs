namespace Linx.SelfHost.App
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
            this.SelfHostAppFRWServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.SelfHostAppFRWServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // SelfHostAppFRWServiceProcessInstaller
            // 
            this.SelfHostAppFRWServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.NetworkService;
            this.SelfHostAppFRWServiceProcessInstaller.Password = null;
            this.SelfHostAppFRWServiceProcessInstaller.Username = null;
            // 
            // SelfHostAppFRWServiceInstaller
            // 
            this.SelfHostAppFRWServiceInstaller.Description = "Linx Framework SelfHostApp";
            this.SelfHostAppFRWServiceInstaller.DisplayName = "Linx Framework SelfHostApp";
            this.SelfHostAppFRWServiceInstaller.ServiceName = "LinxOmniSelfHostAppFRW";
            this.SelfHostAppFRWServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.SelfHostAppFRWServiceProcessInstaller,
            this.SelfHostAppFRWServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller SelfHostAppFRWServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller SelfHostAppFRWServiceInstaller;
    }
}