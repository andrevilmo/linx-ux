// -----------------------------------------------------------------------
// <copyright file="EnumerableExtensions.cs" company="Linx Sistemas">
// Copyright (c) Linx Sistemas. All rights reserved.
// </copyright>
// ----------------------------------------------------------------------
namespace Linx.Internet.Application
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;

    public static class CollectionExtensions
    {
        public static Dictionary<TKey, dynamic> ToDictionary<TKey, TValue>(this NameValueCollection col)
        {
            var dict = new Dictionary<TKey, dynamic>();
            var keyConverter = TypeDescriptor.GetConverter(typeof(TKey));
            var valueConverter = TypeDescriptor.GetConverter(typeof(TValue));

            foreach (string name in col)
            {
                TKey key = (TKey)keyConverter.ConvertFromString(name);
                TValue value = (TValue)valueConverter.ConvertFromString(col[name]);
                dict.Add(key, value);
            }

            return dict;
        }


        public static IDictionary<string, string> ToDictionary(this NameValueCollection source)
        {
            return source.AllKeys.ToDictionary(k => k, k =>
                {
                    string stringValue = source[k];

                    //Nullable<bool> boolValue = stringValue.GetTypedValue<Nullable<bool>>(null);
                    //if (boolValue.HasValue)
                    //    return boolValue.Value;

                    //Nullable<int> intValue = stringValue.GetTypedValue<Nullable<int>>(null);
                    //if (boolValue.HasValue)
                    //    return intValue;

                    return stringValue;
                }
            );
        }
    }
}