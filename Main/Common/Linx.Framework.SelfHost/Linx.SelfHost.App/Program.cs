using System;
using System.Collections.Generic;
using System.Configuration;
using System.Configuration.Install;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Linx.SelfHost.App
{
    static class Program
    {
        static void Main(string[] args)
        {
            if (!System.Environment.UserInteractive)
            {
                ServiceBase.Run(new ServiceBase[] { new SelfHostAppFRWService() });
                return;
            }

            if (args.Length == 0)
                return;

            switch (args[0])
            {
                case "-install":
                    ManagedInstallerClass.InstallHelper(new string[] { Assembly.GetExecutingAssembly().Location });
                    break;
                case "-uninstall":
                    ManagedInstallerClass.InstallHelper(new string[] { "/u", Assembly.GetExecutingAssembly().Location });
                    break;
            }

            //var port = ConfigurationManager.AppSettings["PortSelfHostApp"];
            //StartSelfHost _startSelfHost = new StartSelfHost();
            //_startSelfHost.StartHost(port);
            //Console.WriteLine("OWIN Host Started at http://*:" + port + "/");
            //System.Diagnostics.Process.Start("http://localhost:" + port + "/");
            //Console.WriteLine();
            //Console.ReadLine();

        }

    }
}
