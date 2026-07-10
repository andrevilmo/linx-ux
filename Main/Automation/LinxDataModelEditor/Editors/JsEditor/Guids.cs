// Guids.cs
// MUST match guids.h
using System;

namespace LinxSistemas.JsEditor
{
    static class GuidList
    {
        public const string guidJsEditorPkgString = "c9b63575-d78a-43ca-b3fe-e48c75ce0b72";
        public const string guidJsEditorCmdSetString = "aecddc5a-1b00-4968-8dbc-b37287f58651";
        public const string guidJsEditorEditorFactoryString = "2284fefa-c37d-4b78-a6c8-4c9955ba43a4";

        public static readonly Guid guidJsEditorCmdSet = new Guid(guidJsEditorCmdSetString);
        public static readonly Guid guidJsEditorEditorFactory = new Guid(guidJsEditorEditorFactoryString);
    };
}