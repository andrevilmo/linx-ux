using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Linx.Internet.Application.Framework
{
    public static class Common
    {
        struct _IMAGE_FILE_HEADER
        {
            public ushort Machine;
            public ushort NumberOfSections;
            public uint TimeDateStamp;
            public uint PointerToSymbolTable;
            public uint NumberOfSymbols;
            public ushort SizeOfOptionalHeader;
            public ushort Characteristics;
        };

        public static string IsAssemblyDebugBuild(System.Reflection.Assembly reflectedAssembly)
        {
            string BuildType = string.Empty;
            object[] attribs = reflectedAssembly.GetCustomAttributes(typeof(DebuggableAttribute), false);

            // If the 'DebuggableAttribute' is not found then it is definitely an OPTIMIZED build
            if (attribs.Length > 0)
            {
                // Just because the 'DebuggableAttribute' is found doesn't necessarily mean
                // it's a DEBUG build; we have to check the JIT Optimization flag
                // i.e. it could have the "generate PDB" checked but have JIT Optimization enabled
                DebuggableAttribute debuggableAttribute = attribs[0] as DebuggableAttribute;
                if (debuggableAttribute != null)
                {
                    var IsJITOptimized = !debuggableAttribute.IsJITOptimizerDisabled;
                    BuildType = debuggableAttribute.IsJITOptimizerDisabled ? "debug" : "release";
                }
            }
            else
            {
                BuildType = "release";
            }

            return BuildType;
        }

        public static DateTime GetBuildDateTime(Assembly assembly)
        {
            try
            {
                if (File.Exists(assembly.Location))
                {
                    var buffer = new byte[Math.Max(Marshal.SizeOf(typeof(_IMAGE_FILE_HEADER)), 4)];
                    using (var fileStream = new FileStream(assembly.Location, FileMode.Open, FileAccess.Read))
                    {
                        fileStream.Position = 0x3C;
                        fileStream.Read(buffer, 0, 4);
                        fileStream.Position = BitConverter.ToUInt32(buffer, 0); // COFF header offset
                        fileStream.Read(buffer, 0, 4); // "PE\0\0"
                        fileStream.Read(buffer, 0, buffer.Length);
                    }
                    var pinnedBuffer = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                    try
                    {
                        var coffHeader = (_IMAGE_FILE_HEADER)Marshal.PtrToStructure(pinnedBuffer.AddrOfPinnedObject(), typeof(_IMAGE_FILE_HEADER));

                        return TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1) + new TimeSpan(coffHeader.TimeDateStamp * TimeSpan.TicksPerSecond));
                    }
                    finally
                    {
                        pinnedBuffer.Free();
                    }
                }
            }
            catch
            {
                // proposital para nao retorna erro
            }

            return new DateTime(1900, 1, 1);
        }
    }
}
