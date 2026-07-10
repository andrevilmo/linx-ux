// Guids.cs
// MUST match guids.h
using System;

namespace LinxDataModelEditor.AboutBoxPackage
{
    static class GuidList
    {
        public const string guidAboutBoxPackagePkgString = "c0310c0b-7380-46e0-bf12-ace04c584988";
        public const string guidAboutBoxPackageCmdSetString = "123df230-a787-4ae8-bb60-aa98c5309d3a";

        public static readonly Guid guidAboutBoxPackageCmdSet = new Guid(guidAboutBoxPackageCmdSetString);
    };
}