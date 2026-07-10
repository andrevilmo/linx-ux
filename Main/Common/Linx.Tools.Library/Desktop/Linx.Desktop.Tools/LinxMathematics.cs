// -----------------------------------------------------------------------
// <copyright file="LinxMath.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Linx.Mathematics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Execute Combinatory Analysis Routines.
    /// </summary>
    public class CombinatoryAnalysis
    {
        public static List<List<T>> CombineElements<T>(List<T> elementsForDistributing, int groupingElements)
        {
            List<List<T>> result = new List<List<T>>();
            int endIndex = (elementsForDistributing.Count - groupingElements);
            if (endIndex <= 0)
                result.Add(elementsForDistributing);
            else
            {
                List<T> innerCombination = new List<T>();
                AddCombinations<T>(elementsForDistributing, 0, endIndex, null, groupingElements, result);
            }
            return result;
        }

        private static void AddCombinations<T>(List<T> elementsForDistributing, int startIndex, int endIndex, List<T> combinationBuffer, int groupingElements, List<List<T>> result)
        {
            for (int idx = startIndex; idx <= endIndex; idx++)
            {
                List<T> innerCombination = (combinationBuffer == null ? new List<T>() : combinationBuffer.ToList());
                innerCombination.Add(elementsForDistributing[idx]);
                if (innerCombination.Count == groupingElements) //Stop condition
                {
                    result.Add(innerCombination);
                }
                else
                {
                    AddCombinations<T>(elementsForDistributing, idx + 1, (endIndex + 1), innerCombination, groupingElements, result);
                }
            }
        }

    }
}
