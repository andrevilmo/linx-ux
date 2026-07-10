#region Using directives

using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.ConstrainedExecution;

#endregion

//
// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
//
[assembly: AssemblyTitle(@"")]
[assembly: AssemblyDescription(@"")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany(@"Linx")]
[assembly: AssemblyProduct(@"BusinessModelDesigner")]
[assembly: AssemblyCopyright("")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: System.Resources.NeutralResourcesLanguage("en")]

//
// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers 
// by using the '*' as shown below:

[assembly: AssemblyVersion(@"1.0.0.0")]
[assembly: ComVisible(false)]
[assembly: CLSCompliant(false)]
[assembly: ReliabilityContract(Consistency.MayCorruptProcess, Cer.None)]

//
// Make the Dsl project internally visible to the DslPackage assembly
//
[assembly: InternalsVisibleTo(@"Linx.BusinessModelDesigner.DslPackage, PublicKey=0024000004800000940000000602000000240000525341310004000001000100FDBD8F3A24C791191354BA14FA7E10417DEFDEF42F77A08B9124E2E4F2444014941CE17E48780541D7089D6F9989862963077D0A45D24B83AEAC0EADC20BD54C7E500B21C785C7C364E2C43AAD25F879E75BBD292ABFD4AFFB7B02A0244B4B807F73B3DB5267B1B0A74B2D2010A5499FAC2958951DAC9C9BEA8EC23ADEDDB6EE")]