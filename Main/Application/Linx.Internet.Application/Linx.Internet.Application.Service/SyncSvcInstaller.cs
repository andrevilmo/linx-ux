using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace Linx.Internet.Application.Service
{
    [RunInstaller(true)]
    public class SyncSvcInstaller : Installer
    {
        public static string SVC_NAME = "LinxUXLIAService";
        public static string SVC_DISPLAYNAME = "LinxUX Application Service";
        public static string SVC_DESC = "";

        public SyncSvcInstaller()
        {
            string[] args = Environment.GetCommandLineArgs();

            foreach (string arg in args)
            {
                if (arg.StartsWith("-sname:", StringComparison.InvariantCultureIgnoreCase))
                {
                    string[] value = arg.Split(':');
                    SVC_NAME = value[1];
                }

                if (arg.StartsWith("-sdisplayname:", StringComparison.InvariantCultureIgnoreCase))
                {
                    string[] value = arg.Split(':');
                    SVC_DISPLAYNAME = value[1];
                }
            }


            Installers.Clear();

            ServiceInstaller serviceInstaller = new ServiceInstaller();
            serviceInstaller.StartType = ServiceStartMode.Automatic;
            serviceInstaller.ServiceName = SVC_NAME;
            serviceInstaller.DisplayName = SVC_DISPLAYNAME;
            serviceInstaller.Description = SVC_DESC;

            //serviceInstaller.ServicesDependedOn = new string[] { "SENS", "COMSysApp" };

            Installers.Add(serviceInstaller);

            ServiceProcessInstaller processInstaller = new ServiceProcessInstaller();
            processInstaller.Account = ServiceAccount.LocalSystem;
            processInstaller.Password = null;
            processInstaller.Username = null;
            
            Installers.Add(processInstaller);
        }

        protected override void OnAfterInstall(IDictionary savedState)
        {
            ServiceController controller = null;
            ServiceController[] controllers = ServiceController.GetServices();
            for (int i = 0; i < controllers.Length; i++)
            {
                if (controllers[i].ServiceName == SVC_NAME)
                {
                    controller = controllers[i];
                    break;
                }
            }
            if (controller == null)
                return;

            
            // if the service is not active, start it
            if (controller.Status != ServiceControllerStatus.Running)
            {
                string[] args = { "-install" };
                controller.Start(args);
            }
        }

        public static void Install(bool undo, string[] args)
        {
            try
            {
                Console.WriteLine(undo ? "uninstalling" : "installing");
                using (AssemblyInstaller inst = new AssemblyInstaller(typeof(Program).Assembly, args))
                {
                    IDictionary state = new Hashtable();
                    inst.UseNewContext = true;
                    try
                    {
                        if (undo)
                        {
                            inst.Uninstall(state);
                        }
                        else
                        {
                            inst.Install(state);
                            inst.Commit(state);
                        }
                    }
                    catch
                    {
                        try
                        {
                            inst.Rollback(state);
                        }
                        catch { }
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }
    }
}