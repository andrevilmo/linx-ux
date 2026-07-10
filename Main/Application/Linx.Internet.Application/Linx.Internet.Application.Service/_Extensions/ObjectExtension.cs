using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Linx.Internet.Application.Service
{
    public static class ObjectExtension
    {

        public static bool IsTypeOf(this object reference, string typeName)
        {
            return (
                        reference.GetType().Name == typeName ||
                        reference.GetType().BaseType.Name == typeName ||
                        reference.GetType().GetInterfaces().Where(e => e.Name == typeName).Count() > 0
                    );
        }

        public static bool IsNullOrEmpty(this object instance)
        {
            bool isNullOrEmpty = false;

            try
            {
                if (instance == null)
                    isNullOrEmpty = true;
                else
                {
                    string typeName = instance.GetType().Name.ToLower();
                    switch (typeName)
                    {
                        case "string":
                            isNullOrEmpty = String.IsNullOrEmpty(((String)instance));
                            break;
                        case "char":
                            isNullOrEmpty = (!((System.Nullable<char>)instance).HasValue || ((char)instance) == ' ');
                            break;
                        case "byte":
                            isNullOrEmpty = (!((System.Nullable<byte>)instance).HasValue || ((byte)instance) == 0);
                            break;
                        case "int16":
                            isNullOrEmpty = (!((System.Nullable<System.Int16>)instance).HasValue || ((System.Int16)instance) == 0);
                            break;
                        case "int32":
                            isNullOrEmpty = (!((System.Nullable<System.Int32>)instance).HasValue || ((System.Int32)instance) == 0);
                            break;
                        case "int64":
                            isNullOrEmpty = (!((System.Nullable<System.Int64>)instance).HasValue || ((System.Int64)instance) == 0);
                            break;
                        case "sbyte":
                            isNullOrEmpty = (!((System.Nullable<sbyte>)instance).HasValue || ((sbyte)instance) == 0);
                            break;
                        case "uint16":
                            isNullOrEmpty = (!((System.Nullable<System.UInt16>)instance).HasValue || ((System.UInt16)instance) == 0);
                            break;
                        case "uint32":
                            isNullOrEmpty = (!((System.Nullable<System.UInt32>)instance).HasValue || ((System.UInt32)instance) == 0);
                            break;
                        case "uint64":
                            isNullOrEmpty = (!((System.Nullable<System.UInt64>)instance).HasValue || ((System.UInt64)instance) == 0);
                            break;
                        case "single":
                            isNullOrEmpty = (!((System.Nullable<System.Single>)instance).HasValue || ((System.Single)instance) == 0);
                            break;
                        case "double":
                            isNullOrEmpty = (!((System.Nullable<System.Double>)instance).HasValue || ((System.Double)instance) == 0);
                            break;
                        case "decimal":
                            isNullOrEmpty = (!((System.Nullable<System.Decimal>)instance).HasValue || ((System.Decimal)instance) == 0);
                            break;
                        case "datetime":
                            isNullOrEmpty = (!((System.Nullable<DateTime>)instance).HasValue || ((DateTime)instance) == (new DateTime()));
                            break;
                        case "guid":
                            isNullOrEmpty = (!((System.Nullable<Guid>)instance).HasValue || ((Guid)instance) == Guid.Empty);
                            break;
                        case "bool":
                            isNullOrEmpty = (!((System.Nullable<bool>)instance).HasValue || ((bool)instance) == false);
                            break;
                        case "boolean":
                            isNullOrEmpty = (!((System.Nullable<bool>)instance).HasValue || ((bool)instance) == false);
                            break;
                        default:
                            break;
                    }
                }

            }
            catch
            {
                isNullOrEmpty = false;
            }

            return isNullOrEmpty;
        }

        public static bool IsNull(this object instance)
        {
            bool isNull = false;

            try
            {
                if (instance == null)
                    isNull = true;
                else
                {
                    string typeName = instance.GetType().Name.ToLower();
                    switch (typeName)
                    {
                        case "char":
                            isNull = (!((System.Nullable<char>)instance).HasValue);
                            break;
                        case "byte":
                            isNull = (!((System.Nullable<byte>)instance).HasValue);
                            break;
                        case "int16":
                            isNull = (!((System.Nullable<System.Int16>)instance).HasValue);
                            break;
                        case "int32":
                            isNull = (!((System.Nullable<System.Int32>)instance).HasValue);
                            break;
                        case "int64":
                            isNull = (!((System.Nullable<System.Int64>)instance).HasValue);
                            break;
                        case "sbyte":
                            isNull = (!((System.Nullable<sbyte>)instance).HasValue);
                            break;
                        case "uint16":
                            isNull = (!((System.Nullable<System.UInt16>)instance).HasValue);
                            break;
                        case "uint32":
                            isNull = (!((System.Nullable<System.UInt32>)instance).HasValue);
                            break;
                        case "uint64":
                            isNull = (!((System.Nullable<System.UInt64>)instance).HasValue);
                            break;
                        case "single":
                            isNull = (!((System.Nullable<System.Single>)instance).HasValue);
                            break;
                        case "double":
                            isNull = (!((System.Nullable<System.Double>)instance).HasValue);
                            break;
                        case "decimal":
                            isNull = (!((System.Nullable<System.Decimal>)instance).HasValue);
                            break;
                        case "datetime":
                            isNull = (!((System.Nullable<DateTime>)instance).HasValue);
                            break;
                        case "guid":
                            isNull = (!((System.Nullable<Guid>)instance).HasValue);
                            break;
                        default:
                            break;
                    }
                }

            }
            catch
            {
                isNull = false;
            }

            return isNull;
        }

    }
}



