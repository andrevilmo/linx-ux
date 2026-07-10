using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linx.License.Client;
using Linx.Tools;
using System.Runtime.InteropServices;

namespace Linx.License.Proxy.Test
{
    class Program
    {
        static string logFile = @"c:\temp\license_times.txt";
        static void Main(string[] args)
        {
            string clientId = "65161419000170";
            string licenseId = "4";
            string publicKey = "LINX174557892000";
            System.IO.File.WriteAllText(logFile, "");

            Licensecing instance = new Licensecing(true);
   
            AccessLicense(clientId, licenseId, publicKey, instance);
            

            Console.ReadLine();
            
            //#endregion

        }

        static void AccessLicense(string clientId, string licenseId, string publicKey, Licensecing instance)
        {            
            int totalIterations = 1;
            
            #region Check License

            Console.WriteLine("Checking license...");
            //Data Definition            
            var data = @"
             {
             ""IdLicenca"" : " + licenseId + @",
             ""IdCliente"": """ + clientId + @""",
             ""Usuario"" : ""usuarioTeste1""
            }
            ";
            //Get new token (Expires in two minutes)
            string token = instance.GenerateToken(publicKey);
            //Encrypt data
            string encryptData = instance.Encrypt(data, token);
            //Verify license
            string result = "", dataResult = "";
            
            for (int i = 0; i < totalIterations; i++)
            {
                var currentStart = DateTime.Now;
                result = instance.Validar(encryptData);
                var currentEnd = DateTime.Now;
                System.IO.File.AppendAllText(logFile, String.Format("Vilid License: End process number {0} in {1} milliseconds.\r\n", i, currentEnd.Subtract(currentStart).TotalMilliseconds));
                dataResult = instance.Decrypt(result, token);
                if (dataResult.Contains("\"Valor\":false"))
                    break;
            }
            //Decrypt license result
            Console.WriteLine(dataResult);
            Console.WriteLine("End!");


            #endregion

            //#region Save LOG

            Console.WriteLine("Saving log...");
            data = System.IO.File.ReadAllText("c:\\temp\\Jsontest.txt"); 

            //Data Definition            
            //data = @"
            //     {
            //       ""IdProduto"" : 3,
            //       ""IdCliente"": """ + clientId + @""",
            //       ""Usuario"" : ""usuarioTeste1"",
            //       ""IdUsuario"" : null,
            //       ""NomeAutenticacao"" : null,    
            //       ""CodigoFilial"" : """",
            //       ""NomeFilial"" : """",
            //       ""IndicaLoja"" : false,
            //       ""IdLinx"" : null,
            //       ""Detalhes"" : [ ""Ale1"", ""Ale2"" ]
            //    }";
            //Get new token (Expires in two minutes)
            token = instance.GenerateToken(publicKey);
            //Encrypt data
            encryptData = instance.Encrypt(data, token);
            for (int i = 0; i < totalIterations; i++)
            {
                var currentStart = DateTime.Now;
                //Save Log
                result = instance.SalvarLog(encryptData);
                var currentEnd = DateTime.Now;
                System.IO.File.AppendAllText(logFile, String.Format("Save Log: End process number {0} in {1} milliseconds.\r\n", i, currentEnd.Subtract(currentStart).TotalMilliseconds));
                dataResult = instance.Decrypt(result, token);
                if (dataResult.Contains("\"Valor\":false"))
                    break;
            }
            Console.WriteLine(dataResult);
            Console.WriteLine("End!");

            //#endregion

            //#region Remove License

            Console.WriteLine("Removing license...");
            //Data Definition            
            data = @"
             {
             ""IdLicenca"" : " + licenseId + @",
             ""IdCliente"": """ + clientId + @""",
             ""Usuario"" : ""usuarioTeste1""
            }
            ";
            //Get new token (Expires in two minutes)
            token = instance.GenerateToken(publicKey);
            //Encrypt data
            encryptData = instance.Encrypt(data, token);


            for (int i = 0; i < totalIterations; i++)
            {
                var currentStart = DateTime.Now;
                //Remove License
                result = instance.Remover(encryptData);
                var currentEnd = DateTime.Now;
                System.IO.File.AppendAllText(logFile, String.Format("Remove License: End process number {0} in {1} milliseconds.\r\n", i, currentEnd.Subtract(currentStart).TotalMilliseconds));
                dataResult = instance.Decrypt(result, token);
                if (dataResult.Contains("\"Valor\":false"))
                    break;
            }

            Console.WriteLine(dataResult);
            Console.WriteLine("End!");
        }
    }
}
