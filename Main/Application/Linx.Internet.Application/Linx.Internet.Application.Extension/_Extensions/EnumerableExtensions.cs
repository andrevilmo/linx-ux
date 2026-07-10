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
    using System.Collections.ObjectModel;

    /// <summary>
    /// Provides extension methods for the <see cref="IEnumerable{T}" /> type.
    /// </summary>
    public static class EnumerableExtensions
    {
        #region Methods
        /// <summary>
        /// Performs the given <see cref="Action{T}" /> on each item in the enumerable.
        /// </summary>
        /// <typeparam name="T">The type of item in the enumerable.</typeparam>
        /// <param name="items">The enumerable of items.</param>
        /// <param name="action">The action to perform on each item.</param>
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            Throw.IfArgumentNull(items, "items");
            Throw.IfArgumentNull(action, "action");

            foreach (T item in items)
            {
                action(item);
            }
        }

        /// <summary>
        /// Metodo ToList{T} Convert para Lista
        /// </summary>
        /// <typeparam name="T">Parametro tipo Generico</typeparam>
        /// <param name="enumerableList">Parametro tipo IEnumerable</param>
        /// <returns>Retorna uma Lista relacionada com o Parametro T </returns>
        public static List<T> ToList<T>(this IEnumerable enumerableList)
        {
            if (enumerableList != null)
            {
                ////create an emtpy observable collection object
                List<T> observableCollection = new List<T>();

                ////loop through all the records and add to observable collection object
                foreach (var item in enumerableList)
                {
                    observableCollection.Add((T)item);
                }

                ////return the populated observable collection
                return observableCollection;
            }

            return null;
        }
        #endregion
    }
}