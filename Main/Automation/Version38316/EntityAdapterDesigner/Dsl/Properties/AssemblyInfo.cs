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
[assembly: AssemblyProduct(@"EntityAdapterDesigner")]
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

[assembly: AssemblyVersion("1.0.0.0")]
[assembly: ComVisible(false)]
[assembly: CLSCompliant(false)]
[assembly: ReliabilityContract(Consistency.MayCorruptProcess, Cer.None)]

//
// Make the Dsl project internally visible to the DslPackage assembly
//
[assembly: InternalsVisibleTo(@"Linx.EntityAdapterDesigner.DslPackage, PublicKey=0024000004800000940000000602000000240000525341310004000001000100A9789D41D864612CE34316B5F42CFCD2679C9371BA088798E4B2D43015B8937C84C353819361B5BA43FC0C5287EEE4C8FDA660EE48CE4FE4F0DA00B8C30D79799E20C375DABB7355EAC38E4AE2C767D1FA217CFCC25F3F1ED0EF0EEF60D9D29FDDC8D1CDB9FA67B757D2841754D0C4E93E11CC1066D11F1565E14568CC9C8F89")]