using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.DomainServices.Server;

namespace Linx.Framework.BV.Modulo
{
    public partial class ModuloDomainService
    {
        /// <summary>
        /// Reads VERSAO from LX_TCS.TCS_VERSAO.
        /// Ignores plain numbers and values with non-numeric/non-dot characters,
        /// compares valid dotted versions and returns the highest original VERSAO string.
        /// </summary>
        [Invoke]
        public string GetHighestReleaseVersion()
        {
            List<string> versions = this.DbContext.TCS_VERSAO
                .Select(v => v.VERSAO)
                .ToList();

            return SelectHighestVersion(versions);
        }

        internal static string SelectHighestVersion(IEnumerable<string> rawVersions)
        {
            string highest = null;
            int[] highestParts = null;

            foreach (string raw in rawVersions)
            {
                if (string.IsNullOrWhiteSpace(raw))
                {
                    continue;
                }

                string candidate = raw.Trim();

                // Do not strip/transform — ignore invalid rows entirely
                if (!IsValidVersionCandidate(candidate))
                {
                    continue;
                }

                int[] parts = SplitVersionParts(candidate);
                if (highestParts == null || CompareVersionParts(parts, highestParts) > 0)
                {
                    highestParts = parts;
                    highest = candidate;
                }
            }

            return highest ?? string.Empty;
        }

        /// <summary>
        /// Valid VERSAO: digits and dots only, with at least one dotted segment (ex: 5.2.7).
        /// Ignored: plain numbers (ex: 20250822) and any non-numeric/non-dot characters.
        /// </summary>
        private static bool IsValidVersionCandidate(string candidate)
        {
            // Ignore rows with characters that are not numeric and not dot
            for (int i = 0; i < candidate.Length; i++)
            {
                char c = candidate[i];
                if (!char.IsDigit(c) && c != '.')
                {
                    return false;
                }
            }

            // Ignore plain numbers (no dots)
            if (candidate.IndexOf('.') < 0)
            {
                return false;
            }

            // Must be digit segments separated by dots (no leading/trailing/empty dots)
            string[] segments = candidate.Split('.');
            if (segments.Length < 2)
            {
                return false;
            }

            for (int i = 0; i < segments.Length; i++)
            {
                if (segments[i].Length == 0 || !segments[i].All(char.IsDigit))
                {
                    return false;
                }
            }

            return true;
        }

        private static int[] SplitVersionParts(string version)
        {
            return version
                .Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(part =>
                {
                    int value;
                    return int.TryParse(part, out value) ? value : 0;
                })
                .ToArray();
        }

        private static int CompareVersionParts(int[] left, int[] right)
        {
            int length = Math.Max(left.Length, right.Length);
            for (int i = 0; i < length; i++)
            {
                int leftPart = i < left.Length ? left[i] : 0;
                int rightPart = i < right.Length ? right[i] : 0;
                if (leftPart != rightPart)
                {
                    return leftPart.CompareTo(rightPart);
                }
            }

            return 0;
        }
    }
}
