using Linx.Tools;
using System.Management;

namespace Linx.License.Client
{
    internal class Identity
    {
        /// <summary>
        /// Get machine unique ID
        /// </summary>
        /// <returns></returns>
        public string Value()
        {
            string result =
                 DiskId()
                 + "|" + MotherboardId();
            // + "|" + System.Environment.MachineName;

            if (result.Length > 255)
                result = result.Left(255);

            return result;
        }

        /// <summary>
        /// Hardwere identifier
        /// </summary>
        /// <param name="wmiClass"></param>
        /// <param name="wmiProperty"></param>
        /// <param name="wmiMustBeTrue"></param>
        /// <returns></returns>
        private string Identifier(string wmiClass, string wmiProperty, string wmiMustBeTrue)
        {
            string resultado = "";
            ManagementClass mc = new ManagementClass(wmiClass);
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if (mo[wmiMustBeTrue].ToString() == "True")
                {
                    //pega somente o primeiro
                    if (resultado == "")
                    {
                        try
                        {
                            resultado = mo[wmiProperty].ToString();
                            break;
                        }
                        catch
                        {
                        }
                    }
                }
            }
            return resultado;
        }
        /// <summary>
        /// Hardwere identifier
        /// </summary>
        /// <param name="wmiClass"></param>
        /// <param name="wmiProperty"></param>
        /// <returns></returns>
        private string Identifier(string wmiClass, string wmiProperty)
        ///Retorna o identificador do hardware
        {
            string resultado = "";
            ManagementClass mc = new ManagementClass(wmiClass);
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {

                //pega somente o primeiro
                if (resultado == "")
                {
                    try
                    {
                        resultado = mo[wmiProperty].ToString();
                        break;
                    }
                    catch
                    {
                    }
                }

            }
            return resultado;
        }

        /// <summary>
        /// CPU Identity
        /// </summary>
        /// <returns></returns>
        private string CpuId()
        {
            //Usa o primeiro identificador da CPU na ordem de preferencia
            //Não pega todos os identificadores, pois demora muito tempo
            string retVal = Identifier("Win32_Processor", "UniqueId");
            if (retVal == "") //Se não obter o UniqueID, usa o ProcessorID
            {
                retVal = Identifier("Win32_Processor", "ProcessorId");

                if (retVal == "") //Se não pegar o ProcessorId, usa o Name
                {
                    retVal = Identifier("Win32_Processor", "Name");


                    if (retVal == "") //Se não pegar o Name, usa o Manufacturer
                    {
                        retVal = Identifier("Win32_Processor", "Manufacturer");
                    }
                    //Adiciona o clock speed por segurança
                    retVal += Identifier("Win32_Processor", "MaxClockSpeed");
                }
            }

            return retVal;

        }

        /// <summary>
        /// BIOs Identity
        /// </summary>
        /// <returns></returns>
        private string BiosId()
        {
            return Identifier("Win32_BIOS", "Manufacturer")
            + Identifier("Win32_BIOS", "SMBIOSBIOSVersion")
            + Identifier("Win32_BIOS", "IdentificationCode")
            + Identifier("Win32_BIOS", "SerialNumber")
            + Identifier("Win32_BIOS", "ReleaseDate")
            + Identifier("Win32_BIOS", "Version");
        }

        /// <summary>
        /// Disk Identity
        /// </summary>
        /// <returns></returns>
        private string DiskId()
        //ID do principal disco rigido
        {
            //return Identifier("Win32_DiskDrive", "Model")
            //+ Identifier("Win32_DiskDrive", "Manufacturer")
            //+ Identifier("Win32_DiskDrive", "Signature")
            //+ Identifier("Win32_DiskDrive", "TotalHeads");
            return Identifier("Win32_DiskDrive", "SerialNumber");
        }

        /// <summary>
        /// Base Id
        /// </summary>
        /// <returns></returns>
        private string MotherboardId()
        //ID da Motherboard
        {
            return 
            //    Identifier("Win32_BaseBoard", "Model")
            //+ Identifier("Win32_BaseBoard", "Manufacturer")
            //+ Identifier("Win32_BaseBoard", "Name") +
            Identifier("Win32_BaseBoard", "SerialNumber");
        }

        /// <summary>
        /// Video Identitfier
        /// </summary>
        /// <returns></returns>
        private string VideoId()
        //ID do controlador de video primário
        {
            return Identifier("Win32_VideoController", "DriverVersion")
            + Identifier("Win32_VideoController", "Name");
        }

        /// <summary>
        /// MAC ID
        /// </summary>
        /// <returns></returns>
        private string MacId()
        //ID da rede habilitada
        {
            return Identifier("Win32_NetworkAdapterConfiguration", "MACAddress", "IPEnabled");
        }

        /// <summary>
        /// Pack informations
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string Pack(string text)
        //Empacota a string para 8 digitos
        {
            string retVal;
            int x = 0;
            int y = 0;
            foreach (char n in text)
            {
                y++;
                x += (n * y);
            }

            retVal = x.ToString() + "00000000";
            return retVal.Substring(0, 8);
        }
    }
}