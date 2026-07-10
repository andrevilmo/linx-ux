using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace Linx.Tools
{
    /// <summary>
    /// Domain Key Pair
    /// </summary>
    public struct DomainKeyPair
    {
        public string Value { get; set; }
        public string DisplayName { get; set; }
    }

    /// <summary>
    /// Enum Values Representation.
    /// </summary>
    public class EnumValidationValues
    {
        public System.UInt16 ValueUInt16 { get; set; }
        public System.UInt32 ValueUInt32 { get; set; }
        public System.Int16 ValueInt16 { get; set; }
        public System.Int32 ValueInt32 { get; set; }
        public System.String ValueString { get; set; }
        public System.Char ValueChar { get; set; }
        public System.Byte ValueByte { get; set; }
        public string Name { get; set; }

        public EnumValidationValues(Byte value, string name)
        {
            this.ValueByte = value;
            this.Name = name;
        }

        public EnumValidationValues(char value, string name)
        {
            this.ValueChar = value;
            this.Name = name;
        }

        public EnumValidationValues(string value, string name)
        {
            this.ValueString = value;
            this.Name = name;
        }

        public EnumValidationValues(System.UInt16 value, string name)
        {
            this.ValueUInt16 = value;
            this.Name = name;
        }

        public EnumValidationValues(System.UInt32 value, string name)
        {
            this.ValueUInt32 = value;
            this.Name = name;
        }

        public EnumValidationValues(System.Int16 value, string name)
        {
            this.ValueInt16 = value;
            this.Name = name;
        }

        public EnumValidationValues(System.Int32 value, string name)
        {
            this.ValueInt32 = value;
            this.Name = name;
        }

        public static List<EnumValidationValues> LoadValues(Dictionary<string, string> domainValues, string typeName)
        {
            List<EnumValidationValues> enumValues = new List<EnumValidationValues>();

            if (domainValues != null && domainValues.Count > 0)
            {
                switch (typeName.ToLower())
                {
                    case "char":
                        enumValues.Add(new EnumValidationValues(' ', ""));
                        foreach (string key in domainValues.Keys)
                        {
                            enumValues.Add(new EnumValidationValues(key[0], domainValues[key].Translate()));
                        }
                        break;
                    case "string":
                        enumValues.Add(new EnumValidationValues("", ""));
                        foreach (string key in domainValues.Keys)
                        {
                            enumValues.Add(new EnumValidationValues(key.Trim(), domainValues[key].Translate()));
                        }
                        break;
                    case "byte":
                        enumValues.Add(new EnumValidationValues((System.Byte)0, ""));
                        foreach (string key in domainValues.Keys)
                        {
                            enumValues.Add(new EnumValidationValues(System.Byte.Parse(key), domainValues[key].Translate()));
                        }
                        break;
                    case "uint16":
                        enumValues.Add(new EnumValidationValues((System.UInt16)0, ""));
                        foreach (string key in domainValues.Keys)
                        {
                            enumValues.Add(new EnumValidationValues(System.UInt16.Parse(key), domainValues[key].Translate()));
                        }
                        break;
                    case "uint32":
                        enumValues.Add(new EnumValidationValues((System.UInt32)0, ""));
                        foreach (string key in domainValues.Keys)
                        {
                            enumValues.Add(new EnumValidationValues(System.UInt32.Parse(key), domainValues[key].Translate()));
                        }
                        break;
                    case "int16":
                        enumValues.Add(new EnumValidationValues((System.Int16)0, ""));
                        foreach (string key in domainValues.Keys)
                        {
                            enumValues.Add(new EnumValidationValues(System.Int16.Parse(key), domainValues[key].Translate()));
                        }
                        break;
                    case "int32":
                        enumValues.Add(new EnumValidationValues((System.Int32)0, ""));
                        foreach (string key in domainValues.Keys)
                        {
                            enumValues.Add(new EnumValidationValues(System.Int32.Parse(key), domainValues[key].Translate()));
                        }
                        break;
                    default:
                        break;
                }
            }
            return enumValues;
        }

        public static string GetValuePath(string typeName)
        {
            string valuePath = string.Empty;
            switch (typeName)
            {
                case "char":
                    valuePath = "ValueChar";
                    break;
                case "string":
                    valuePath = "ValueString";
                    break;
                case "byte":
                    valuePath = "ValueByte";
                    break;
                case "uint16":
                    valuePath = "ValueUInt16";
                    break;
                case "uint32":
                    valuePath = "ValueUInt32";
                    break;
                case "int16":
                    valuePath = "ValueInt16";
                    break;
                case "int32":
                    valuePath = "ValueInt32";
                    break;
                default:
                    break;
            }

            return valuePath;
        }
    }
}
