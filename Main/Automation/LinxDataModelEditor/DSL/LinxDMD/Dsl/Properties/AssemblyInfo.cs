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
[assembly: AssemblyProduct(@"Linx.BusinessDataModelDesigner.Dsl")]
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
[assembly: CLSCompliant(true)]
[assembly: ReliabilityContract(Consistency.MayCorruptProcess, Cer.None)]

//
// Make the Dsl project internally visible to the DslPackage assembly
//
[assembly: InternalsVisibleTo(@"Linx.BusinessDataModelDesigner.DslPackage, PublicKey=33340000048000009400000006020000002400005253413100040000010001008B393D8B2670B1A4EE439DAB72CAC50DC04F0CC29E00D10F2A343CBA99A6EDF0C5BE8FD567E4C320B968FFA5C55CA1E8233BC2932AF3907128C4967EBB126B15D928D3EB4060BD2CF49A6ABC0D3FD2685BE7DEC6C44EFEAE8856B1FEE3A9A17DCED8EF753D2E50DCAD8638B12DB2480850B671176E1F0168E87540E58E4849AE")]